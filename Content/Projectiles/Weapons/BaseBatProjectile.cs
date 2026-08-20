using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework.Input;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Tbats.Content.Projectiles.Weapons
{
	// This file is a modified version of ExampleCustomSwingSword in the TModloader repo
	public class BaseBatProjectile : ModProjectile
	{
		protected const float SWINGRANGE = 1.67f * (float)Math.PI; // The angle a swing attack covers (300 deg)
		protected const float FIRSTHALFSWING = 0.45f; // How much of the swing happens before it reaches the target angle (in relation to swingRange)
		protected const float WINDUP = 0.15f; // How far back the player's hand goes when winding their attack (in relation to swingRange)
		protected const float UNWIND = 0.4f; // When should the sword start disappearing

		protected enum AttackStage // What stage of the attack is being executed, see functions found in AI for description
		{
			Prepare,
			Execute,
			Unwind
		}

		protected AttackStage CurrentStage {
			get => (AttackStage)Projectile.localAI[0];
			set {
				Projectile.localAI[0] = (float)value;
				Timer = 0; // reset the timer when the projectile switches states
			}
		}

		protected ref float InitialAngle => ref Projectile.ai[1]; // Angle aimed in (with constraints)
		protected ref float Timer => ref Projectile.ai[2]; // Timer to keep track of progression of each stage
		protected ref float Progress => ref Projectile.localAI[1]; // Position of bat relative to initial angle
		protected ref float Size => ref Projectile.localAI[2]; // Size of bat

		protected float prepTime => 12f / Owner.GetTotalAttackSpeed(Projectile.DamageType);
		protected float execTime => 12f / Owner.GetTotalAttackSpeed(Projectile.DamageType);
		protected float hideTime => 12f / Owner.GetTotalAttackSpeed(Projectile.DamageType);
		protected int dustCooldown = 0;
		protected bool grow = true;
		protected int damage;
		protected Player Owner => Main.player[Projectile.owner];
		public override string Texture => "Tbats/Content/Items/Weapons/WoodBat"; // Default value so TModloader doesn't crash

		public override void SetStaticDefaults() {
			ProjectileID.Sets.HeldProjDoesNotUsePlayerGfxOffY[Type] = true;
			ProjectileID.Sets.AllowsContactDamageFromJellyfish[Type] = true;
		}

		public override void SetDefaults() {
			Projectile.width = 48; // Hitbox width of projectile
			Projectile.height = 48; // Hitbox height of projectile
			Projectile.friendly = true; // Projectile hits enemies
			Projectile.timeLeft = 10000; // Time it takes for projectile to expire
			Projectile.penetrate = -1; // Projectile pierces infinitely
			Projectile.tileCollide = false; // Projectile does not collide with tiles
			Projectile.usesLocalNPCImmunity = true; // Uses local immunity frames
			Projectile.localNPCHitCooldown = -1; // We set this to -1 to make sure the projectile doesn't hit twice
			Projectile.ownerHitCheck = true; // Make sure the owner of the projectile has line of sight to the target (aka can't hit things through tile).
			Projectile.DamageType = DamageClass.Melee; // Projectile is a melee projectile
		}
		
		private static Random random = new Random();
		public static float RandomRange(float min, float max)
		{
			return (float)(random.NextDouble() * (max - min) + min);
		}

		public override void OnSpawn(IEntitySource source) {
			Owner.GetModPlayer<TbatsPlayer>().SwingingBat = true;
			damage = Projectile.damage;
			Projectile.spriteDirection = Main.MouseWorld.X > Owner.MountedCenter.X ? 1 : -1;
			float targetAngle = (Main.MouseWorld - Owner.MountedCenter).ToRotation();
			
			if (Projectile.spriteDirection == 1) {
				// However, we limit the rangle of possible directions so it does not look too ridiculous
				targetAngle = MathHelper.Clamp(targetAngle, (float)-Math.PI * 1 / 3, (float)Math.PI * 1 / 6);
			}
			else {
				if (targetAngle < 0) {
					targetAngle += 2 * (float)Math.PI; // This makes the range continuous for easier operations
				}

				targetAngle = MathHelper.Clamp(targetAngle, (float)Math.PI * 5 / 6, (float)Math.PI * 4 / 3);
			}

			InitialAngle = targetAngle - FIRSTHALFSWING * SWINGRANGE * Projectile.spriteDirection; // Otherwise, we calculate the angle
		}

		public override void SendExtraAI(BinaryWriter writer) {
			// Projectile.spriteDirection for this projectile is derived from the mouse position of the owner in OnSpawn, as such it needs to be synced. spriteDirection is not one of the fields automatically synced over the network. All Projectile.ai slots are used already, so we will sync it manually.
			writer.Write((sbyte)Projectile.spriteDirection);
		}

		public override void ReceiveExtraAI(BinaryReader reader) {
			Projectile.spriteDirection = reader.ReadSByte();
		}

		public override void AI()
		{
			Projectile.damage = (int)(damage * Projectile.scale);
			// Extend use animation until projectile is killed
			Owner.itemAnimation = 2;
			Owner.itemTime = 2;

			// Kill the projectile if the player dies or gets crowd controlled
			if (!Owner.active || Owner.dead || Owner.noItems || Owner.CCed) {
				Owner.GetModPlayer<TbatsPlayer>().SwingingBat = false;
				Projectile.Kill();
				return;
			}

			switch (CurrentStage) {
				case AttackStage.Prepare:
					PrepareStrike();
					break;
				case AttackStage.Execute:
					ExecuteStrike();
					break;
				default:
					UnwindStrike();
					break;
			}

			SetSwordPosition();
			Timer++;
		}

		public override bool PreDraw(ref Color lightColor) {
			// Calculate origin of sword (hilt) based on orientation and offset sword rotation (as sword is angled in its sprite)
			Vector2 origin;
			float rotationOffset;
			SpriteEffects effects;

			if (Projectile.spriteDirection > 0) {
				origin = new Vector2(0, Projectile.height);
				rotationOffset = MathHelper.ToRadians(45f);
				effects = SpriteEffects.None;
			}
			else {
				origin = new Vector2(Projectile.width, Projectile.height);
				rotationOffset = MathHelper.ToRadians(135f);
				effects = SpriteEffects.FlipHorizontally;
			}

			Texture2D texture = TextureAssets.Item[Owner.HeldItem.type].Value;
			
			Main.spriteBatch.Draw(texture, Projectile.Center - Main.screenPosition, default, lightColor * Projectile.Opacity, Projectile.rotation + rotationOffset, origin, Projectile.scale, effects, 0);

			// Since we are doing a custom draw, prevent it from normally drawing
			return false;
		}

		// Find the start and end of the sword and use a line collider to check for collision with enemies
		// Scale expanded by 1.6x to make hitting enemies easier
		public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox) {
			Vector2 start = Owner.MountedCenter;
			Vector2 end = start + Projectile.rotation.ToRotationVector2() * (Projectile.Size.Length() * (Projectile.scale * 1.6f));
			float collisionPoint = 0f;
			return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), start, end, 15f * Projectile.scale, ref collisionPoint);
		}

		// Do a similar collision check for tiles
		public override void CutTiles() {
			Vector2 start = Owner.MountedCenter;
			Vector2 end = start + Projectile.rotation.ToRotationVector2() * (Projectile.Size.Length() * Projectile.scale);
			Utils.PlotTileLine(start, end, 15 * Projectile.scale, DelegateMethods.CutTiles);
		}

		// We make it so that the projectile can only do damage in its release and unwind phases
		public override bool? CanDamage() {
			if (CurrentStage == AttackStage.Prepare)
				return false;
			return base.CanDamage();
		}

		private Dictionary<int, float> originalKnockBackResist = new Dictionary<int, float>();
		
		public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers) {
			// Make knockback go away from player
			modifiers.HitDirectionOverride = target.position.X > Owner.MountedCenter.X ? 1 : -1;
			// Decrease knockback resistance of enemies
			originalKnockBackResist[target.whoAmI] = target.knockBackResist;
			target.knockBackResist += 2; 
		}

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			// Reset knockback resistance of enemies back to default after hit
			if (originalKnockBackResist.TryGetValue(target.whoAmI, out float original))
			{
				target.knockBackResist = original;
				originalKnockBackResist.Remove(target.whoAmI);
			}
		}
		
		// Function to easily set projectile and arm position
		public void SetSwordPosition() {
			Projectile.rotation = InitialAngle + Projectile.spriteDirection * Progress; // Set projectile rotation

			// Set composite arm allows you to set the rotation of the arm and stretch of the front and back arms independently
			Owner.SetCompositeArmFront(true, Player.CompositeArmStretchAmount.Full, Projectile.rotation - MathHelper.ToRadians(90f)); // set arm position (90 degree offset since arm starts lowered)
			Vector2 armPosition = Owner.GetFrontHandPosition(Player.CompositeArmStretchAmount.Full, Projectile.rotation - (float)Math.PI / 2); // get position of hand

			// Adjust the position for reversed gravity.
			if (Owner.gravDir == -1f) {
				Projectile.rotation = 0f - Projectile.rotation;
				armPosition.Y = Owner.Bottom.Y + (Owner.position.Y - armPosition.Y);
			}

			armPosition.Y += Owner.gfxOffY;
			Projectile.Center = armPosition; // Set projectile to arm position
			Projectile.scale = Size * 1.2f; // Slightly scale up the projectile and also take into account melee size modifiers

			Owner.heldProj = Projectile.whoAmI; // set held projectile to this projectile
		}

		// Function facilitating the taking out of the sword
		protected virtual void PrepareStrike()
		{
			Progress = WINDUP * SWINGRANGE * (1f - Timer / prepTime); // Calculates rotation from initial angle
			if (grow) Size = MathHelper.SmoothStep(0.3f, 1, Timer / prepTime); // Make sword slowly increase in size as we prepare to strike until it reaches max
			if (Timer >= prepTime)
			{
				Timer = prepTime;
				dustCooldown += 1;

				// Create dust when sword is fully charged
				if (dustCooldown == 4)
				{
					dustCooldown = 0;
					Vector2 offsetRange = new Vector2(RandomRange(-1f, 1f), RandomRange(-1f, 1f));
					int readyDust = Dust.NewDust(Projectile.Center + offsetRange, Main.rand.Next(1, 2), Main.rand.Next(1, 2), DustID.Gold, 0f, 0f, 0, Color.Gold, RandomRange(1.65f,2.25f));
					Main.dust[readyDust].noGravity = true;
					float offsetRadians = MathHelper.ToRadians(RandomRange(-30f, 30f));
					Main.dust[readyDust].velocity = (Projectile.rotation + offsetRadians).ToRotationVector2() * RandomRange(0f,15f);
					Main.dust[readyDust].velocity += Owner.velocity;
				}
			}

			// If left click is not clicked till time is fully ready, limit size (which limits damage) but still let swing when sufficient time has passed
			// Wait till sufficient time has passed & left click is no longer down
			bool sufficientTimePassed = Timer >= prepTime;
			bool leftClickDown = Mouse.GetState().LeftButton == ButtonState.Pressed;
			if (!leftClickDown && !sufficientTimePassed) { grow = false; }
			if (leftClickDown || !sufficientTimePassed) { return; }
			
			SoundEngine.PlaySound(SoundID.Item1); // Play sword sound here since playing it on spawn is too early
			CurrentStage = AttackStage.Execute; // If attack is over prep time, we go to next stage
		}

		// Function facilitating the first half of the swing
		protected virtual void ExecuteStrike() {
			Progress = MathHelper.SmoothStep(0, SWINGRANGE, (1f - UNWIND) * Timer / execTime);

			if (Timer >= execTime) {
				CurrentStage = AttackStage.Unwind;
			}
		}

		// Function facilitating the latter half of the swing where the sword disappears
		protected virtual void UnwindStrike() {
			Progress = MathHelper.SmoothStep(0, SWINGRANGE, (1f - UNWIND) + UNWIND * Timer / hideTime);
			Size = 1f - MathHelper.SmoothStep(0, 1, Timer / hideTime); // Make sword slowly decrease in size as we end the swing to make a smooth hiding animation

			if (Timer >= hideTime) {
				Owner.GetModPlayer<TbatsPlayer>().SwingingBat = false;
				Projectile.Kill();
			}
		}
	}
}