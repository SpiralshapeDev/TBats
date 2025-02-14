using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace Tbats.Content
{
    public class TbatsMenu : ModMenu
    {
        private const string menuAssetPath = "Tbats/Assets/Textures/Menu";

        private Asset<Texture2D> logo;

        public override void Load()
        {
            logo = ModContent.Request<Texture2D>($"{menuAssetPath}/Logo");
        }
        public override Asset<Texture2D> Logo => logo;

        public override int Music => MusicLoader.GetMusicSlot(Mod, "Assets/Music/UndyingSlumber");

        public override string DisplayName => "TBats Menu";

        float MousePositionFloatX;
        float MousePositionFloatY;
        public override bool PreDrawLogo(SpriteBatch spriteBatch, ref Vector2 logoDrawCenter, ref float logoRotation, ref float logoScale, ref Color drawColor)
        {
            drawColor = Color.White;

            Vector2 logoDrawPos = new Vector2(Main.screenWidth / 2, 100f);

            Texture2D MenuBG = (Texture2D)ModContent.Request<Texture2D>($"{menuAssetPath}/Background");

            Vector2 zero = Vector2.Zero;
            float width = (float)Main.screenWidth / (float)MenuBG.Width;
            float height = (float)Main.screenHeight / (float)MenuBG.Height;
            MousePositionFloatX = ((Math.Min(Main.screenWidth, Main.MouseScreen.X) - 0) * 100) / (Main.screenWidth - 0) / 100;
            MousePositionFloatY = ((Math.Min(Main.screenHeight, Main.MouseScreen.Y) - 0) * 100) / (Main.screenHeight - 0) / 100;

            if (width != height)
            {
                if (height > width)
                {
                    width = height;
                    zero.X -= ((float)MenuBG.Width * width - (float)Main.screenWidth) * 0.5f;
                }
                else
                {
                    zero.Y -= ((float)MenuBG.Height * width - (float)Main.screenHeight) * 0.5f;
                }
            }
            spriteBatch.Draw(MenuBG, new Vector2(zero.X + MathHelper.Lerp(-98, -82, MousePositionFloatX) + 75, zero.Y + MathHelper.Lerp(-50, -47, MousePositionFloatY) + 15), (Rectangle?)null, Color.White, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
            return true;
        }

    }
}
