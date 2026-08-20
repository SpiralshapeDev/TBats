using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Tbats.Common.Configs;
using Terraria.Audio;
using Terraria.DataStructures;

namespace Tbats.Content.Projectiles
{
    public class HomingChlorophyte : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.DamageType = DamageClass.Melee;
            Projectile.width = 18;
            Projectile.height = 26;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 600;
            Projectile.damage = ModContent.GetInstance<ServerConfig>().ChlorophyteBatDamage / 10;
            Projectile.light = 0.9f;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.frame = 0;
        }

        public override void OnSpawn(IEntitySource source) {
            Projectile.scale = Projectile.ai[0];
            Projectile.damage = (int)(Projectile.damage * Projectile.scale);
        }

        public override void OnKill(int timeLeft) {
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            for (int i = 0; i < 8; i++)
            {
                int d = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.GreenTorch, 0f, 0f, 0, default, 1.5f);
                Main.dust[d].noGravity = false;
                Main.dust[d].velocity *= 2f;
            }
            base.OnKill(timeLeft);
        }

        public override void AI()
        {
            float homingRangeTiles = 30f;
            float homingRange = homingRangeTiles * 16f; // 16px per tile

            NPC target = FindClosestTarget(homingRange);

            if (target != null)
            {
                Vector2 direction = target.Center - Projectile.Center;
                direction.Normalize();

                Vector2 desiredVelocity = direction * 16;

                float turnSpeed = 0.12f;
                Projectile.velocity = Vector2.Lerp(Projectile.velocity, desiredVelocity, turnSpeed);
            }

            Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90);
        }

        private NPC FindClosestTarget(float maxRange)
        {
            NPC closest = null;
            float closestDist = maxRange;

            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                if (!npc.CanBeChasedBy(Projectile)) { continue; }
                
                float foundDist = Vector2.Distance(Projectile.Center, npc.Center);
                if (closestDist < foundDist) { continue; }
                
                bool lineOfSight = Collision.CanHitLine(
                    Projectile.position, Projectile.width, Projectile.height,
                    npc.position, npc.width, npc.height
                );
                if (!lineOfSight) { continue; }
                
                closestDist = foundDist;
                closest = npc;
            }

            return closest;
        }
    }
}
