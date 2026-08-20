using Terraria;
using Terraria.ModLoader;

namespace Tbats
{
    public class TbatsPlayer : ModPlayer
    {
        public bool SwingingBat = false;

        public override void ModifyHurt(ref Player.HurtModifiers modifiers)
        {
            if (SwingingBat)
            {
                modifiers.Knockback *= 0f;
            }
        }
    }
}