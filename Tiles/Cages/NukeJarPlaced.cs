using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ModLoader;
using Terraria.ObjectData;
using CalValEX.Items.Tiles.Cages;

namespace CalValEX.Tiles.Cages
{
    public class NukeJarPlaced : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileLighted[Type] = true;
            Main.tileFrameImportant[Type] = true;
            Main.tileNoAttach[Type] = true;
            Main.tileLavaDeath[Type] = true;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2Top);
            TileObjectData.newTile.Height = 2;
            TileObjectData.newTile.CoordinateHeights = new[] { 16, 16 };
            TileObjectData.newTile.StyleHorizontal = true;

            TileObjectData.newTile.AnchorTop = new AnchorData(AnchorType.SolidTile | AnchorType.SolidSide | AnchorType.SolidBottom, TileObjectData.newTile.Width, 0);
            TileObjectData.newTile.StyleWrapLimit = 111;
            TileObjectData.addTile(Type);
            dustType = -1;
            disableSmartCursor = true;
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Nuclear Fly in a Jar");
            AddMapEntry(new Color(0, 255, 200), name);
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            int style = frameX / 18;
            string item;
            switch (style)
            {
                case 0:
                    item = "NukeJar";
                    break;

                default:
                    return;
            }
            Item.NewItem(i * 16, j * 16, 16, 48, mod.ItemType(item));
        }

        public override void SetSpriteEffects(int i, int j, ref SpriteEffects spriteEffects)
        {
            if (i % 2 == 1)
            {
                spriteEffects = SpriteEffects.FlipHorizontally;
            }
        }

        public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
        {
            Tile tile = Main.tile[i, j];
            // We can support different light colors for different styles here: switch (tile.frameY / 54)
            r = 0.3f;
            g = 1f;
            b = 0.8f;
        }
    }
}