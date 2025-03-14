﻿using Terraria.ID;
using Terraria.ModLoader;
using CalValEX.Items.Plushies;

namespace CalValEX.Items.Tiles.Plushies
{
    public class AstrageldonPlush : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Astrageldon Plushie (Placeable)");
        }

        public override void SetDefaults()
        {
            item.useStyle = 1;
            item.width = 44;
            item.height = 44;
            item.consumable = true;
            item.UseSound = SoundID.Item1;
            item.rare = 10;
            item.useAnimation = 20;
            item.useTime = 20;
            item.noUseGraphic = true;
            item.noMelee = true;
            item.value = 20;
            item.createTile = mod.TileType("AstrageldonPlushPlaced");
            item.maxStack = 99;
        }

        public override void AddRecipes()
        {
            Mod Cata = ModLoader.GetMod("Catalyst");
            if (Cata != null)
            {
                Mod CalValEX = ModLoader.GetMod("CalamityMod");
                {
                    ModRecipe recipe = new ModRecipe(mod);
                    recipe.AddIngredient(ModContent.ItemType<AstrageldonPlushThrowable>());
                    recipe.SetResult(this);
                    recipe.AddRecipe();
                }
            }
        }
    }
}