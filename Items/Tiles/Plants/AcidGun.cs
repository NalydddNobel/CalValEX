using Terraria.ID;
using Terraria.ModLoader;

namespace CalValEX.Items.Tiles.Plants
{
    public class AcidGun : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Acid Tape Dispenser");
            Tooltip.SetDefault("Places an infinite amount of Sulphurous Vines");
        }

        public override void SetDefaults()
        {
            item.useStyle = 4;
            item.useTurn = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.autoReuse = true;
            item.maxStack = 1;
            item.width = 16;
            item.height = 28;
            item.rare = 4;
            Mod calamityMod = ModLoader.GetMod("CalamityMod");
            if (calamityMod != null)
            {
                item.createTile = (calamityMod.TileType("SulphurousVines"));
            }
        }

        public override void AddRecipes()
        {
            Mod CalValEX = ModLoader.GetMod("CalamityMod");
            {
                ModRecipe recipe = new ModRecipe(mod);
                recipe.AddIngredient(ModLoader.GetMod("CalamityMod").ItemType("SulfuricScale"), 5);
                recipe.AddIngredient((ItemID.VineRope), 50);
                recipe.AddTile(TileID.Anvils);
                recipe.SetResult(this);
                recipe.AddRecipe();
            }
        }
    }
}