using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Tbats.Common.Configs;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;

namespace Tbats.Content.Projectiles
{
    public class MidnightBreak : ModProjectile
    {
        public override void SetDefaults()
        {
            Projectile.DamageType = DamageClass.Melee;
            Projectile.width = 18;
            Projectile.height = 26;
            Projectile.aiStyle = 0;
            Projectile.friendly = true;
            Projectile.hostile = false;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 3600;
            Projectile.damage = ModContent.GetInstance<ServerConfig>().MidnightBatDamage / 4;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.frame = 0;
            Projectile.usesLocalNPCImmunity = true;
            Projectile.localNPCHitCooldown = 20;
        }
        
        public override string Texture => "Terraria/Images/Item_" + ItemID.CelestialSigil;
        protected Player Owner => Main.player[Projectile.owner];
        protected float RotationOffset = 0;
        protected float DamageRange = 0;

        public override void OnSpawn(IEntitySource source) {
            Projectile.scale = Projectile.ai[0];
            Projectile.damage = (int)(Projectile.damage * Projectile.scale);
        }

        public override void OnKill(int timeLeft) {
            SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            base.OnKill(timeLeft);
        }
        
        public override bool PreDraw(ref Color lightColor) {
            float minOpacity = 0.5f;
            lightColor.R = Math.Max(lightColor.R, (byte)(255/minOpacity));
            lightColor.G = Math.Max(lightColor.G, (byte)(255/minOpacity));
            lightColor.B = Math.Max(lightColor.B, (byte)(255/minOpacity));
            
            Main.instance.LoadItem(ItemID.CelestialSigil);
            Main.instance.LoadItem(ItemID.FragmentSolar);
            Main.instance.LoadItem(ItemID.NebulaMonolith);
            Main.instance.LoadItem(ItemID.SolarMonolith);
            Main.instance.LoadItem(ItemID.VortexMonolith);
            Main.instance.LoadItem(ItemID.StardustMonolith);

            Texture2D textureCenter = TextureAssets.Item[ItemID.CelestialSigil].Value;
            Texture2D textureCenterBackground = TextureAssets.Item[ItemID.FragmentSolar].Value;
            Texture2D textureFireRing = ModContent.Request<Texture2D>("Terraria/Images/FlameRing", AssetRequestMode.ImmediateLoad).Value;
            Texture2D textureNebula = TextureAssets.Item[ItemID.NebulaMonolith].Value;
            Texture2D textureSolar = TextureAssets.Item[ItemID.SolarMonolith].Value;
            Texture2D textureVortex = TextureAssets.Item[ItemID.VortexMonolith].Value;
            Texture2D textureStardust = TextureAssets.Item[ItemID.StardustMonolith].Value;

            float itemDistance = 40f * Projectile.scale;
            float itemScale = 0.75f * Projectile.scale;
            DamageRange = itemDistance * 2.5f;
            Rectangle fireRingFrame = new Rectangle(0, 0, textureFireRing.Width, textureFireRing.Height / 3);

            Vector2 centerPosition = Projectile.Center - Main.screenPosition;
            Vector2 positionNebula = centerPosition + (Projectile.rotation+ MathHelper.ToRadians( RotationOffset)).ToRotationVector2() * itemDistance;
            Vector2 positionSolar = centerPosition + (Projectile.rotation + MathHelper.ToRadians(90 + RotationOffset)).ToRotationVector2() * itemDistance;
            Vector2 positionVortex = centerPosition + (Projectile.rotation + MathHelper.ToRadians(180 + RotationOffset)).ToRotationVector2() * itemDistance;
            Vector2 positionStardust = centerPosition + (Projectile.rotation + MathHelper.ToRadians(270 + RotationOffset)).ToRotationVector2() * itemDistance;
            
            float rotationNebula = (centerPosition - positionNebula).ToRotation() + MathHelper.ToRadians(90);
            float rotationSolar = (centerPosition - positionSolar).ToRotation() + MathHelper.ToRadians(90);
            float rotationVortex = (centerPosition - positionVortex).ToRotation() + MathHelper.ToRadians(90);
            float rotationStardust = (centerPosition - positionStardust).ToRotation() + MathHelper.ToRadians(90);
            
            Main.spriteBatch.Draw(textureCenterBackground, centerPosition, default, lightColor * (Projectile.Opacity / 2), Projectile.rotation + -2 * RotationOffset, textureCenterBackground.Size() / 2f, itemScale * 4, SpriteEffects.None, 0);
            Main.spriteBatch.Draw(textureCenter, centerPosition, default, lightColor * Projectile.Opacity, Projectile.rotation, textureCenter.Size() / 2f, itemScale, SpriteEffects.None, 0);
            Main.spriteBatch.Draw(textureNebula, positionNebula, default, lightColor * Projectile.Opacity, rotationNebula, textureNebula.Size() / 2f, itemScale, SpriteEffects.None, 0);
            Main.spriteBatch.Draw(textureSolar, positionSolar, default, lightColor * Projectile.Opacity, rotationSolar, textureSolar.Size() / 2f, itemScale, SpriteEffects.None, 0);
            Main.spriteBatch.Draw(textureVortex, positionVortex, default, lightColor * Projectile.Opacity, rotationVortex, textureVortex.Size() / 2f, itemScale, SpriteEffects.None, 0);
            Main.spriteBatch.Draw(textureStardust, positionStardust, default, lightColor * Projectile.Opacity, rotationStardust, textureStardust.Size() / 2f, itemScale, SpriteEffects.None, 0);
            Main.spriteBatch.Draw(textureFireRing, centerPosition, fireRingFrame, lightColor * Projectile.Opacity, Projectile.rotation + -2 * RotationOffset, fireRingFrame.Size() / 2f, DamageRange * 2f / textureFireRing.Width, SpriteEffects.None, 0);

            float lightMultiplier = 2f;
            float lightLimit = 255 / lightMultiplier * Projectile.scale;
            float velocityMultiplier = 6f;
            Lighting.AddLight(Projectile.position + Projectile.velocity * velocityMultiplier,Color.OrangeRed.R/lightLimit,Color.OrangeRed.G/lightLimit,Color.OrangeRed.B/lightLimit);
            Lighting.AddLight(Projectile.position + (Projectile.rotation+ MathHelper.ToRadians(RotationOffset)).ToRotationVector2() * itemDistance + Projectile.velocity * velocityMultiplier,Color.HotPink.R/lightLimit,Color.HotPink.G/lightLimit,Color.HotPink.B/lightLimit);
            Lighting.AddLight(Projectile.position + (Projectile.rotation + MathHelper.ToRadians(90 + RotationOffset)).ToRotationVector2() * itemDistance + Projectile.velocity * velocityMultiplier,Color.OrangeRed.R/lightLimit,Color.OrangeRed.G/lightLimit,Color.OrangeRed.B/lightLimit);
            Lighting.AddLight(Projectile.position + (Projectile.rotation + MathHelper.ToRadians(180 + RotationOffset)).ToRotationVector2() * itemDistance + Projectile.velocity * velocityMultiplier,Color.Cyan.R/lightLimit,Color.Cyan.G/lightLimit,Color.Cyan.B/lightLimit);
            Lighting.AddLight(Projectile.position + (Projectile.rotation + MathHelper.ToRadians(270 + RotationOffset)).ToRotationVector2() * itemDistance + Projectile.velocity * velocityMultiplier,Color.MediumSlateBlue.R/lightLimit,Color.MediumSlateBlue.G/lightLimit,Color.MediumSlateBlue.B/lightLimit);

            // Since we are doing a custom draw, prevent it from normally drawing
            return false;
        }
        
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
        {
            float distance = Vector2.Distance(Projectile.Center, targetHitbox.Center.ToVector2());
            return distance <= DamageRange;
        }

        public override void AI() {
            RotationOffset += 0.75f;
            if (RotationOffset > 360) {
                RotationOffset = 0;
            }
        }
    }
}
