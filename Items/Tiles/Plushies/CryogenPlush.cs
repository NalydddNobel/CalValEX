﻿using Terraria.ID;
using Terraria.ModLoader;
using CalValEX.Items.Plushies;

namespace CalValEX.Items.Tiles.Plushies
{
    public class CryogenPlush : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Cryogen Plushie (Placeable)");
        }

        public override void SetDefaults()
        {
            item.useStyle = 1;
            item.width = 44;
            item.height = 44;
            item.consumable = true;
            item.UseSound = SoundID.Item1;
            item.rare = 4;
            item.useAnimation = 20;
            item.useTime = 20;
            item.noUseGraphic = true;
            item.noMelee = true;
            item.value = 20;
            item.createTile = mod.TileType("CryogenPlushPlaced");
            item.maxStack = 99;
        }

        public override void AddRecipes()
        {
            Mod CalValEX = ModLoader.GetMod("CalamityMod");
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(ModContent.ItemType<CryogenPlushThrowable>());
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}