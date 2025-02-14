using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Terraria.DataStructures;
using System.Collections.Generic;
using Tbats.Common.Configs;
using Terraria.Audio;
using Microsoft.Xna.Framework.Input;

namespace Tbats.Content.Projectiles
{
    public class MidnightBatProjectile : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.Equals("lunar_beam");
        }
        public override void SetDefaults()
        {
            Projectile.DamageType = DamageClass.Melee;
            Projectile.width = 8;
            Projectile.height = 8;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = 100;
            Projectile.timeLeft = 600;
            Projectile.damage = ModContent.GetInstance<ServerConfig>().MidnightBatDamage / 3;
            Projectile.light = 2;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.frame = 0;
            Projectile.rotation = 45;
        }
        public override void OnSpawn(IEntitySource source)
        {
            SoundEngine.PlaySound(new SoundStyle("Tbats/Assets/Audio/Projectiles/MidnightBatSpawn"), Projectile.position);
        }
        public override void OnKill(int timeLeft)
        {
            SoundEngine.PlaySound(new SoundStyle("Tbats/Assets/Audio/Projectiles/MidnightBatDeath"), Projectile.position);
            for (int i = 0; i < 10; i++)
            {
                int deathdust = Dust.NewDust(new Vector2(Projectile.Center.X + Main.rand.Next(-10, 10), Projectile.Center.Y + Main.rand.Next(-10, 10)), Main.rand.Next(1, 2), Main.rand.Next(1, 2), DustID.LunarOre, 0f, 0f, 0, default(Color), 1f);
                Main.dust[deathdust].noGravity = false;
                Main.dust[deathdust].color = Color.LightBlue;
                Main.dust[deathdust].velocity *= 1f;
                Main.dust[deathdust].scale = (float)Main.rand.Next(100, 125) * 0.013f + 0.65f;
            }
        }
        public override void AI()
        {
            int lunar_dust1 = Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y + 12.5f), Main.rand.Next(1, 2), Main.rand.Next(1, 2), DustID.LunarOre, 0f, 0f, 0, default(Color), 1f);
            Main.dust[lunar_dust1].noGravity = true;
            Main.dust[lunar_dust1].color = Color.OrangeRed;
            Main.dust[lunar_dust1].velocity *= 0.3f;
            Main.dust[lunar_dust1].scale = (float)Main.rand.Next(100, 125) * 0.013f + 0.2f;

            int lunar_dust2 = Dust.NewDust(new Vector2(Projectile.Center.X + 8.75f, Projectile.Center.Y + 8.75f), Main.rand.Next(1, 2), Main.rand.Next(1, 2), DustID.LunarOre, 0f, 0f, 0, default(Color), 1f);
            Main.dust[lunar_dust2].noGravity = true;
            Main.dust[lunar_dust2].color = Color.OrangeRed;
            Main.dust[lunar_dust2].velocity *= 0.3f;
            Main.dust[lunar_dust2].scale = (float)Main.rand.Next(100, 125) * 0.013f + 0.2f;

            int lunar_dust3 = Dust.NewDust(new Vector2(Projectile.Center.X - 8.75f, Projectile.Center.Y + 8.75f), Main.rand.Next(1, 2), Main.rand.Next(1, 2), DustID.LunarOre, 0f, 0f, 0, default(Color), 1f);
            Main.dust[lunar_dust3].noGravity = true;
            Main.dust[lunar_dust3].color = Color.DarkSeaGreen;
            Main.dust[lunar_dust3].velocity *= 0.3f;
            Main.dust[lunar_dust3].scale = (float)Main.rand.Next(100, 125) * 0.013f + 0.2f;

            int lunar_dust4 = Dust.NewDust(new Vector2(Projectile.Center.X - 12.5f, Projectile.Center.Y), Main.rand.Next(1, 2), Main.rand.Next(1, 2), DustID.LunarOre, 0f, 0f, 0, default(Color), 1f); ;
            Main.dust[lunar_dust4].noGravity = true;
            Main.dust[lunar_dust4].color = Color.DarkSeaGreen;
            Main.dust[lunar_dust4].velocity *= 0.3f;
            Main.dust[lunar_dust4].scale = (float)Main.rand.Next(100, 125) * 0.013f + 0.2f;

            int lunar_dust5 = Dust.NewDust(new Vector2(Projectile.Center.X + 8.75f, Projectile.Center.Y - 8.75f), Main.rand.Next(1, 2), Main.rand.Next(1, 2), DustID.LunarOre, 0f, 0f, 0, default(Color), 1f);
            Main.dust[lunar_dust5].noGravity = true;
            Main.dust[lunar_dust5].color = Color.LightBlue;
            Main.dust[lunar_dust5].velocity *= 0.3f;
            Main.dust[lunar_dust5].scale = (float)Main.rand.Next(100, 125) * 0.013f + 0.2f;

            int lunar_dust6 = Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y - 12.5f), Main.rand.Next(1, 2), Main.rand.Next(1, 2), DustID.LunarOre, 0f, 0f, 0, default(Color), 1f);
            Main.dust[lunar_dust6].noGravity = true;
            Main.dust[lunar_dust6].color = Color.LightBlue;
            Main.dust[lunar_dust6].velocity *= 0.3f;
            Main.dust[lunar_dust6].scale = (float)Main.rand.Next(100, 125) * 0.013f + 0.2f;

            int lunar_dust7 = Dust.NewDust(new Vector2(Projectile.Center.X + 12.5f, Projectile.Center.Y), Main.rand.Next(1, 2), Main.rand.Next(1, 2), DustID.LunarOre, 0f, 0f, 0, default(Color), 1f);
            Main.dust[lunar_dust7].noGravity = true;
            Main.dust[lunar_dust7].color = Color.HotPink;
            Main.dust[lunar_dust7].velocity *= 0.3f;
            Main.dust[lunar_dust7].scale = (float)Main.rand.Next(100, 125) * 0.013f + 0.2f;

            int lunar_dust8 = Dust.NewDust(new Vector2(Projectile.Center.X + 8.75f, Projectile.Center.Y - 8.75f), Main.rand.Next(1, 2), Main.rand.Next(1, 2), DustID.LunarOre, 0f, 0f, 0, default(Color), 1f);
            Main.dust[lunar_dust8].noGravity = true;
            Main.dust[lunar_dust8].color = Color.HotPink;
            Main.dust[lunar_dust8].velocity *= 0.3f;
            Main.dust[lunar_dust8].scale = (float)Main.rand.Next(100, 125) * 0.013f + 0.2f;

            int middle_lunar_dust_1 = Dust.NewDust(Projectile.Center, Main.rand.Next(1, 2), Main.rand.Next(1, 2), DustID.LunarOre, 0f, 0f, 0, default(Color), 1f);
            Main.dust[middle_lunar_dust_1].noGravity = true;
            Main.dust[middle_lunar_dust_1].color = Color.OrangeRed;
            Main.dust[middle_lunar_dust_1].velocity *= 0.3f;
            Main.dust[middle_lunar_dust_1].scale = (float)Main.rand.Next(100, 125) * 0.013f + 0.5f;

            int middle_lunar_dust_2 = Dust.NewDust(Projectile.Center, Main.rand.Next(1, 2), Main.rand.Next(1, 2), DustID.LunarOre, 0f, 0f, 0, default(Color), 1f);
            Main.dust[middle_lunar_dust_2].noGravity = true;
            Main.dust[middle_lunar_dust_2].color = Color.DarkSeaGreen;
            Main.dust[middle_lunar_dust_2].velocity *= 0.3f;
            Main.dust[middle_lunar_dust_2].scale = (float)Main.rand.Next(100, 125) * 0.013f + 0.5f;

            int middle_lunar_dust_3 = Dust.NewDust(Projectile.Center, Main.rand.Next(1, 2), Main.rand.Next(1, 2), DustID.LunarOre, 0f, 0f, 0, default(Color), 1f);
            Main.dust[middle_lunar_dust_3].noGravity = true;
            Main.dust[middle_lunar_dust_3].color = Color.LightBlue;
            Main.dust[middle_lunar_dust_3].velocity *= 0.3f;
            Main.dust[middle_lunar_dust_3].scale = (float)Main.rand.Next(100, 125) * 0.013f + 0.5f;

            int middle_lunar_dust_4 = Dust.NewDust(Projectile.Center, Main.rand.Next(1, 2), Main.rand.Next(1, 2), DustID.LunarOre, 0f, 0f, 0, default(Color), 1f);
            Main.dust[middle_lunar_dust_4].noGravity = true;
            Main.dust[middle_lunar_dust_4].color = Color.HotPink;
            Main.dust[middle_lunar_dust_4].velocity *= 0.3f;
            Main.dust[middle_lunar_dust_4].scale = (float)Main.rand.Next(100, 125) * 0.013f + 0.5f;
        }
    }
}
