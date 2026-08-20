using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Tbats.Common.Configs;
using Terraria.Audio;
using Terraria.DataStructures;

namespace Tbats.Content.Projectiles
{
    public class MoltenFlame : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.DamageType = DamageClass.Melee;
            Projectile.width = 18;
            Projectile.height = 26;
            Projectile.aiStyle = ProjAIStyleID.Arrow;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 600;
            Projectile.damage = ModContent.GetInstance<ServerConfig>().MoltenBatDamage / 12;
            Projectile.light = 0.9f;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.frame = 0;
        }
        public override string Texture => "Terraria/Images/Item_" + ItemID.LivingFireBlock;
        protected int dustCooldown = 0;

        public override void OnSpawn(IEntitySource source) {
            Projectile.scale = Projectile.ai[0];
            Projectile.damage = (int)(Projectile.damage * Projectile.scale);
        }

        public override void OnKill(int timeLeft) {
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            for (int i = 0; i < 2; i++)
            {
                int d = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.SolarFlare, 0f, 0f, 0, default, 1.5f);
                Main.dust[d].noGravity = false;
                Main.dust[d].velocity *= 0.75f;
            }
            base.OnKill(timeLeft);
        }
        
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            base.OnHitNPC(target, hit, damageDone);
            target.AddBuff(BuffID.OnFire, 10);
        }

        public override void AI() {
            dustCooldown++;
            if (dustCooldown == 4) {
                dustCooldown = 0;
                int d = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Lava, 0f, 0f, 0, default, 1.5f);
                Main.dust[d].noGravity = true;
                Main.dust[d].velocity *= 2f;
                if (Projectile.velocity.Y > 0) {
                    Projectile.velocity.Y *= 1.1f;
                }
            }
            base.AI();
        }
    }
}
