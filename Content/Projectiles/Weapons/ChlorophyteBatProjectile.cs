using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace Tbats.Content.Projectiles.Weapons
{
	// This file is a modified version of ExampleCustomSwingSword in the TModloader repo
	public class ChlorophyteBatProjectile : BaseBatProjectile
	{
		protected override void ExecuteStrike() {
			Progress = MathHelper.SmoothStep(0, SWINGRANGE, (1f - UNWIND) * Timer / execTime);
			if (Timer >= execTime) {
				int subProjectile = ModContent.ProjectileType<HomingChlorophyte>();
				float rotationMouse = (Main.MouseWorld - Owner.MountedCenter).ToRotation();
				IEntitySource source = Projectile.GetSource_FromThis();
				float speed = 15;
				Projectile.NewProjectile(source, Owner.position, rotationMouse.ToRotationVector2() * speed, subProjectile, damage, Projectile.knockBack, Main.myPlayer, Projectile.scale);
				Projectile.NewProjectile(source, Owner.position, (rotationMouse + MathHelper.ToRadians(25)).ToRotationVector2() * speed, subProjectile, damage, Projectile.knockBack, Main.myPlayer, Projectile.scale);
				Projectile.NewProjectile(source, Owner.position, (rotationMouse + MathHelper.ToRadians(-25)).ToRotationVector2() * speed, subProjectile, damage, Projectile.knockBack, Main.myPlayer, Projectile.scale);
				Projectile.NewProjectile(source, Owner.position, (rotationMouse + MathHelper.ToRadians(12.5f)).ToRotationVector2() * speed, subProjectile, damage, Projectile.knockBack, Main.myPlayer, Projectile.scale);
				Projectile.NewProjectile(source, Owner.position, (rotationMouse + MathHelper.ToRadians(-12.5f)).ToRotationVector2() * speed, subProjectile, damage, Projectile.knockBack, Main.myPlayer, Projectile.scale);

				CurrentStage = AttackStage.Unwind;
			}
		}
	}
}