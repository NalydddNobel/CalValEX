﻿using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using CalValEX.Items.Critters;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria.ID;
using CalValEX.NPCs.Critters;

namespace CalValEX.Items.Critters
{
    public class XerocodileItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Awakened Xerocodile");
        }

        public override void SetDefaults()
        {
            item.useStyle = 1;
            item.autoReuse = true;
            item.useTurn = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.maxStack = 999;
            item.consumable = true;
            item.width = 22;
            item.height = 18;
            item.noUseGraphic = true;
            item.rare = 10;
            item.makeNPC = (short)NPCType<Xerocodile>();
        }
        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
            //rarity 12 (Turquoise) = new Color(0, 255, 200)
            //rarity 13 (Pure Green) = new Color(0, 255, 0)
            //rarity 14 (Dark Blue) = new Color(43, 96, 222)
            //rarity 15 (Violet) = new Color(108, 45, 199)
            //rarity 16 (Hot Pink/Developer) = new Color(255, 0, 255)
            //rarity rainbow (no expert tag on item) = new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB)
            //rarity rare variant = new Color(255, 140, 0)
            //rarity dedicated(patron items) = new Color(139, 0, 0)
            //look at https://calamitymod.gamepedia.com/Rarity to know where to use the colors
            foreach (TooltipLine tooltipLine in tooltips)
            {
                if (tooltipLine.mod == "Terraria" && tooltipLine.Name == "ItemName")
                {
                    tooltipLine.overrideColor = new Color(0, 255, 200); //change the color accordingly to above
                }
            }
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            Mod calamityMod = ModLoader.GetMod("CalamityMod");
            recipe.AddIngredient(calamityMod.ItemType("Xerocodile"), 10);
            recipe.AddIngredient(calamityMod.ItemType("BloodstoneCore"), 1);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}