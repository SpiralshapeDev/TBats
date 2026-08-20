using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Tbats.Common.Configs;
using Terraria.Audio;
using Terraria.DataStructures;

namespace Tbats.Content.Projectiles
{
    public class Meteor : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.DamageType = DamageClass.Melee;
            Projectile.width = 18;
            Projectile.height = 26;
            Projectile.aiStyle = ProjAIStyleID.Boulder;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 3600;
            Projectile.damage = ModContent.GetInstance<ServerConfig>().MeteorBatDamage / 4;
            Projectile.light = 0.9f;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.frame = 0;
        }
        public override string Texture => "Terraria/Images/Item_" + ItemID.Meteorite;
        protected Player Owner => Main.player[Projectile.owner];

        public override void OnSpawn(IEntitySource source) {
            Projectile.scale = Projectile.ai[0];
            Projectile.damage = (int)(Projectile.damage * Projectile.scale);
            Projectile.velocity *= Projectile.scale;
        }

        private void NpcEffect(NPC target) {
            target.AddBuff(BuffID.OnFire,20);
                
            for (int ii = 0; ii < 5; ii++)
            {
                int d = Dust.NewDust(target.position, target.width, target.height, DustID.RedTorch, 0f, 0f, 0, default, 1.5f);
                Main.dust[d].noGravity = false;
                Main.dust[d].velocity *= 2f;
            }
        }
        
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone) {
            NpcEffect(target);
            return;
        }

        public override void OnKill(int timeLeft) {
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            for (int i = 0; i < 15; i++)
            {
                int d = Dust.NewDust(Projectile.position, Projectile.width*2, Projectile.height*2, DustID.Meteorite, 0f, 0f, 0, default, 1.5f);
                Main.dust[d].noGravity = false;
                Main.dust[d].velocity *= 2f;
            }

            float explosionRadius = 80f * Projectile.scale;

            for (int i = 0; i < Main.maxNPCs; i++)
            {
                NPC npc = Main.npc[i];
                if (!npc.active || npc.friendly || npc.dontTakeDamage) { continue; }

                float distance = Vector2.Distance(Projectile.Center, npc.Center);
                if (distance > explosionRadius) { continue; }

                Vector2 hitDirectionVector = npc.Center - Projectile.Center;
                int hitDirection = hitDirectionVector.X > 0 ? 1 : -1;

                NPC.HitInfo hitInfo = new NPC.HitInfo() {
                    Crit = false,
                    Damage = Projectile.damage,
                    DamageType = Projectile.DamageType,
                    HideCombatText = false,
                    HitDirection = hitDirection,
                    InstantKill = false,
                    Knockback = Projectile.knockBack,
                    SourceDamage = Projectile.damage
                };

                npc.StrikeNPC(hitInfo, Owner != Main.player[Main.myPlayer], false);
                NpcEffect(npc);
            }
                
            for (int ii = 0; ii < 15; ii++)
            {
                int d = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.RedTorch, 0f, 0f, 0, default, 1.5f);
                Main.dust[d].noGravity = false;
                Main.dust[d].velocity *= 2f;
            }
            base.OnKill(timeLeft);
        }

        public override void AI() {
            for (int i = 0; i < 1; i++) {
                int d = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.SolarFlare, 0f, 0f, 0, default, 0.75f);
                Main.dust[d].noGravity = false;
                Main.dust[d].velocity *= 1f;
            }

            base.AI();
        }
    }
}
