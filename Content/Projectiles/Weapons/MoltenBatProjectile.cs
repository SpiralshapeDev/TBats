using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Tbats.Content.Projectiles.Weapons
{
	// This file is a modified version of ExampleCustomSwingSword in the TModloader repo
	public class MoltenBatProjectile : BaseBatProjectile
	{
		protected override void ExecuteStrike() {
			Progress = MathHelper.SmoothStep(0, SWINGRANGE, (1f - UNWIND) * Timer / execTime);
			if (Timer >= execTime) {
				int subProjectile = ModContent.ProjectileType<MoltenSlag>();
				float rotationMouse = (Main.MouseWorld - Owner.MountedCenter).ToRotation();
				IEntitySource source = Projectile.GetSource_FromThis();
				float speed = 8;
				Projectile.NewProjectile(source, Owner.position, rotationMouse.ToRotationVector2() * speed, subProjectile, damage, Projectile.knockBack, Main.myPlayer, Projectile.scale);
				
				CurrentStage = AttackStage.Unwind;
			}
		}
		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			base.OnHitNPC(target, hit, damageDone);
			target.AddBuff(BuffID.OnFire, 10);
		}
	}
}