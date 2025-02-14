using Tbats.Common.Configs;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Tbats.Content.Items
{
	public class OrichalcumBat : ModItem
	{
        // The Display Name and Tooltip of this item can be edited in the Localization/en-US_Mods.Tbats.hjson file.

        public override void SetDefaults()
		{
			Item.damage = ModContent.GetInstance<ServerConfig>().OrichalcumBatDamage;
			Item.DamageType = DamageClass.Melee;
			Item.width = 48;
			Item.height = 48;
			Item.useTime = 1;
			Item.useAnimation = 75;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 8.25f;
			Item.value = Item.sellPrice(gold:6,silver:93);
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.OrichalcumBar, 40);
            recipe.AddRecipeGroup(RecipeGroupID.Wood, 3);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.Register();
		}
	}
}