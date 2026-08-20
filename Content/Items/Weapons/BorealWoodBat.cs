using System.Linq;
using Microsoft.Xna.Framework;
using Tbats.Common.Configs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Tbats.Content.Projectiles.Weapons;
using Terraria.DataStructures;

namespace Tbats.Content.Items.Weapons
{
    public class BorealWoodBat : ModItem
    {
        public override void SetDefaults()
        {
            Item.damage = ModContent.GetInstance<ServerConfig>().BorealWoodBatDamage;
            Item.DamageType = DamageClass.Melee;
            Item.width = 48;
            Item.height = 48;
            // useTime and useAnimation affect the item tooltip, so don't remove them.
            Item.useTime = 40;
            Item.useAnimation = 40;
            Item.useStyle = ItemUseStyleID.Shoot;
            Item.knockBack = 8.5f;
            Item.value = Item.sellPrice(silver:8, copper:75);
            Item.autoReuse = false;
            Item.noMelee = true;  // Only projectile should deal damage
            Item.noUseGraphic = true; // Only projectile should be rendered
            Item.shoot = ModContent.ProjectileType<BaseBatProjectile>(); // The sword as a projectile
        }
		
        public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
            Item.damage = ModContent.GetInstance<ServerConfig>().BorealWoodBatDamage;
            Projectile.NewProjectile(source, position, velocity, type, damage, knockback, Main.myPlayer);
            return false; // return false to prevent original projectile from being shot
        }
        
        public override bool AllowPrefix(int pre) {
            if (ModContent.GetInstance<Tbats>().BannedPrefixes.Contains(pre))
                return false;
            return base.AllowPrefix(pre);
        }

        public override void AddRecipes()
        {
            Recipe recipe = CreateRecipe();
            recipe.AddIngredient(ItemID.BorealWood, 44);
            recipe.AddTile(TileID.WorkBenches);
            recipe.Register();
        }
    }
}