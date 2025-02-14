using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Tbats.Common.Configs;

namespace Tbats.Content.Projectiles
{
    public class cholorophyte_dust : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.Equals("cholorophyte_dust");
        }
        public override void SetDefaults()
        {
            Projectile.DamageType = DamageClass.Melee;
            Projectile.width = 18;
            Projectile.height = 26;
            Projectile.aiStyle = 1;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 600;
            Projectile.damage = ModContent.GetInstance<ServerConfig>().ChlorophyteBatDamage / 3;
            Projectile.light = 0.5f;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.frame = 0;
        }

        public override void AI()
        {
            int cholorophyte_dust = Dust.NewDust(Projectile.Center,1,1,DustID.Chlorophyte,0f,0f,0,default(Color),1f);
            Main.dust[cholorophyte_dust].noGravity = true;
            Main.dust[cholorophyte_dust].color = Color.Lime;
            Main.dust[cholorophyte_dust].velocity *= 0.3f;
            Main.dust[cholorophyte_dust].scale = (float)Main.rand.Next(100,135) * 0.013f+1;
        }
    }
}
