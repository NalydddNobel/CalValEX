using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace CalValEX.Items.Critters
{
    internal class GoldenIsopodItem : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gold Isopod");
        }

        public override void SetDefaults()
        {
            //item.useStyle = 1;
            //item.autoReuse = true;
            //item.useTurn = true;
            //item.useAnimation = 15;
            //item.useTime = 10;
            //item.maxStack = 999;
            //item.consumable = true;
            //item.width = 12;
            //item.height = 12;
            //item.makeNPC = 360;
            //item.noUseGraphic = true;
            //item.bait = 15;

            item.CloneDefaults(ItemID.GlowingSnail);
            Mod mod = ModLoader.GetMod("CalamityMod");
            if (mod == null)
            {
                return;
            }
            if (((bool)mod.Call("GetBossDowned", "polterghast")) || CalValEXConfig.Instance.IsopodBait)
            {
                item.bait = 75;
            }
            else if (!((bool)mod.Call("GetBossDowned", "polterghast")) && !CalValEXConfig.Instance.IsopodBait)
            {
                item.bait = 1;
            }
            item.makeNPC = (short)NPCType<GoldenIsopod>();
            item.value = Item.sellPrice(0, 20, 0, 0);
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
                    tooltipLine.overrideColor = new Color(0, 255, 0); //change the color accordingly to above
                }
            }
        }

        public override void AddRecipes()
        {
            Mod mod = ModLoader.GetMod("FargowiltasSouls");
            if (mod == null)
            {
                return;
            }
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(ModContent.ItemType<IsopodItem>());
                recipe.AddIngredient((ItemID.GoldDust), 500);
                recipe.AddTile(mod.TileType("GoldenDippingVatSheet"));
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}