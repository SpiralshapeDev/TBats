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
	public class PumpkinBat : ModItem
	{
		public override void SetDefaults()
		{
			Item.damage = ModContent.GetInstance<ServerConfig>().PumpkinBatDamage;
			Item.DamageType = DamageClass.Melee;
			Item.width = 48;
			Item.height = 48;
			// useTime and useAnimation affect the item tooltip, so don't remove them.
			Item.useTime = 40;
			Item.useAnimation = 40;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.knockBack = 8.5f;
			Item.value = Item.sellPrice(silver:10, copper:20);
			Item.autoReuse = false;
			Item.noMelee = true;  // Only projectile should deal damage
			Item.noUseGraphic = true; // Only projectile should be rendered
			Item.shoot = ModContent.ProjectileType<BaseBatProjectile>(); // The sword as a projectile
		}
		
		public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback) {
			Item.damage = ModContent.GetInstance<ServerConfig>().PumpkinBatDamage;
			Projectile.NewProjectile(source, position, velocity, type, damage, knockback, Main.myPlayer);
			return false; // return false to prevent original projectile from being shot
		}
		
		public override bool AllowPrefix(int pre) {
			if (ModContent.GetInstance<Tbats>().BannedPrefixes.Contains(pre))
				return false;
			return base.AllowPrefix(pre);
		}
		
		public override bool MeleePrefix() {
			return true; // allows weapon to have melee reforges (ex: Legendary)
		}

		public override void AddRecipes()
		{
			if (Main.halloween)
			{
				Recipe recipe = CreateRecipe();
				recipe.AddIngredient(ItemID.Pumpkin, 40);
				recipe.AddRecipeGroup(RecipeGroupID.Wood, 3);
				recipe.AddTile(TileID.MythrilAnvil);
				recipe.Register();
			}
		}
	}
}