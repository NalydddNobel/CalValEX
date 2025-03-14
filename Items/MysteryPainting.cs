using CalValEX.Items.Tiles.Paintings;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalValEX.Items
{
    public class MysteryPainting : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mystery Painting");
            Tooltip.SetDefault("<right> for a random painting");
        }

        public override void SetDefaults()
        {
            item.width = 20;
            item.height = 20;
            item.rare = ItemRarityID.Blue;
            item.maxStack = 9999;
        }

        public override bool CanRightClick()
        {
            return true;
        }

        public override void RightClick(Player player)
        {
            int choice = Main.rand.Next(31);
            if (choice == 0)
            {
                player.QuickSpawnItem(ModContent.ItemType<BlazingConflict>());
            }
            else if (choice == 1)
            {
                player.QuickSpawnItem(ModContent.ItemType<CalamiteaTime>());
            }
            else if (choice == 2)
            {
                player.QuickSpawnItem(ModContent.ItemType<CirrusBooze>());
            }
            else if (choice == 3)
            {
                player.QuickSpawnItem(ModContent.ItemType<Clam>());
            }
            else if (choice == 4)
            {
                player.QuickSpawnItem(ModContent.ItemType<CosmicTerror>());
            }
            else if (choice == 5)
            {
                player.QuickSpawnItem(ModContent.ItemType<LostSouls>());
            }
            else if (choice == 6)
            {
                player.QuickSpawnItem(ModContent.ItemType<NotLikeHome>());
            }
            else if (choice == 7)
            {
                player.QuickSpawnItem(ModContent.ItemType<VVanities>());
            }
            else if (choice == 8)
            {
                player.QuickSpawnItem(ModContent.ItemType<Scourgy>());
            }
            else if (choice == 9)
            {
                player.QuickSpawnItem(ModContent.ItemType<SleepingGiant>());
            }
            else if (choice == 10)
            {
                player.QuickSpawnItem(ModContent.ItemType<Snouts>());
            }
            else if (choice == 11)
            {
                player.QuickSpawnItem(ModContent.ItemType<SundayAfternoon>());
            }
            else if (choice == 12)
            {
                player.QuickSpawnItem(ModContent.ItemType<SwearingShroom>());
            }
            else if (choice == 13)
            {
                player.QuickSpawnItem(ModContent.ItemType<TheGreatSandworm>());
            }
            else if (choice == 14)
            {
                player.QuickSpawnItem(ModContent.ItemType<TheYhar>());
            }
            else if (choice == 15)
            {
                player.QuickSpawnItem(ModContent.ItemType<Duality>());
            }
            else if (choice == 16)
            {
                player.QuickSpawnItem(ModContent.ItemType<RogueExtractor>());
            }
            else if (choice == 17)
            {
                player.QuickSpawnItem(ModContent.ItemType<Espelho>());
            }
            else if (choice == 18)
            {
                player.QuickSpawnItem(ModContent.ItemType<DarkMagic>());
            }
            else if (choice == 19)
            {
                player.QuickSpawnItem(ModContent.ItemType<Frozen>());
            }
            else if (choice == 20)
            {
                player.QuickSpawnItem(ModContent.ItemType<UnholyTrip>());
            }
            else if (choice == 21)
            {
                player.QuickSpawnItem(ModContent.ItemType<AccidentalAbominationn>());
            }
            else if (choice == 22)
            {
                player.QuickSpawnItem(ModContent.ItemType<Plague22>());
            }
            else if (choice == 23)
            {
                player.QuickSpawnItem(ModContent.ItemType<CalamityFriends>());
            }
            else if (choice == 24)
            {
                player.QuickSpawnItem(ModContent.ItemType<WilliamPainting>());
            }
            else if (choice == 25)
            {
                player.QuickSpawnItem(ModContent.ItemType<ModIconPainting>());
            }
            else if (choice == 26)
            {
                player.QuickSpawnItem(ModContent.ItemType<CalamityPaint>());
            }
            else if (choice == 27)
            {
                player.QuickSpawnItem(ModContent.ItemType<CalamityPaintRetold>());
            }
            else if (choice == 28)
            {
                player.QuickSpawnItem(ModContent.ItemType<OldModIconPainting>());
            }
            else if (choice == 29)
            {
                player.QuickSpawnItem(ModContent.ItemType<DraedonPopcorn>());
            }
            else
            {
                player.QuickSpawnItem(ModContent.ItemType<WormHeaven>());
            }
        }
    }
}