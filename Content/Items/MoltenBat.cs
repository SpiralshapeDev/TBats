using Mono.Cecil;
using System;
using Tbats.Common.Configs;
using Tbats.Content.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Tbats.Content.Items
{
	public class MoltenBat : ModItem
	{
		// The Display Name and Tooltip of this item can be edited in the Localization/en-US_Mods.Tbats.hjson file.

		public override void SetDefaults()
		{
			Item.damage = ModContent.GetInstance<ServerConfig>().MoltenBatDamage;
			Item.DamageType = DamageClass.Melee;
			Item.width = 48;
			Item.height = 48;
			Item.useTime = 125;
			Item.useAnimation = 75;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.knockBack = 8.25f;
			Item.value = Item.sellPrice(gold:5,silver:33);
            Item.UseSound = SoundID.Item1;
            Item.autoReuse = true;
            Item.shoot = ModContent.ProjectileType<living_flame>();
            Item.shootSpeed = 8f;
        }

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.HellstoneBar, 40);
			recipe.AddIngredient(ItemID.Obsidian, 3);
            recipe.AddTile(TileID.Hellforge); 
            recipe.AddTile(TileID.Anvils);
			recipe.Register();
		}

        public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
        {
			target.AddBuff(BuffID.OnFire,damageDone * 10) ;
        }
    }
}