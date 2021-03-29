using Terraria.ModLoader;

namespace CalValEX.Items.Tiles.Plants
{
    internal class Anemone : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Potted Anemone");
        }

        public override void SetDefaults()
        {
            item.useStyle = 1;
            item.useTurn = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.autoReuse = true;
            item.maxStack = 99;
            item.consumable = true;
            item.createTile = ModContent.TileType<AnemonePlaced>();
            item.width = 12;
            item.height = 12;
            item.rare = 3;
        }
    }
}