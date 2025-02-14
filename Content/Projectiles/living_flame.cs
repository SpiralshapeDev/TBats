using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Terraria.DataStructures;
using System.Collections.Generic;
using System.Security.Cryptography;
using Tbats.Common.Configs;

namespace Tbats.Content.Projectiles
{
    public class living_flame : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.Equals("Living Flame");
        }
        public override void SetDefaults()
        {
            Projectile.DamageType = DamageClass.Melee;
            Projectile.width = 20;
            Projectile.height = 20;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 3600;
            Projectile.damage = ModContent.GetInstance<ServerConfig>().MoltenBatDamage / 3;
            Projectile.light = 2;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.frame = 0;
            Projectile.position = new Vector2(Projectile.position.X + Main.rand.Next(-5, 5), Projectile.position.Y + 150);
        }
        public override void AI()
        {
            Projectile.rotation += 0.1f;
            int star = Dust.NewDust(new Vector2(Projectile.Center.X + Main.rand.Next(-10, 10), Projectile.Center.Y + Main.rand.Next(-10, 10)), Main.rand.Next(1, 2), Main.rand.Next(1, 2), DustID.LunarOre, 0f, 0f, 0, default(Color), 1f);
            Main.dust[star].noGravity = true;
            Main.dust[star].color = Color.OrangeRed;
            Main.dust[star].velocity *= 0.3f;
            Main.dust[star].scale = (float)Main.rand.Next(100, 125) * 0.013f + 1;
        }

        public override void OnKill(int timeLeft)
        {
            int maxI = Main.rand.Next(8, 15);
            for (int i = 0; i < maxI; i++)
            {
                int death = Dust.NewDust(new Vector2(Projectile.Center.X + Main.rand.Next(-10, 10), Projectile.Center.Y + Main.rand.Next(-10, 10)), Main.rand.Next(1, 2), Main.rand.Next(1, 2), DustID.LunarOre, 0f, 0f, 0, default(Color), 1f);
                Main.dust[death].noGravity = false;
                Main.dust[death].color = Color.OrangeRed;
                Main.dust[death].velocity *= 1f;
                Main.dust[death].scale = (float)Main.rand.Next(100, 125) * 0.013f + 1;
            }
        }
    }
}
