using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalValEX.Items.Pets
{
    public class OldTennisBall : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Old Tennis Ball");
            Tooltip.SetDefault("'They used to be everywhere at one point.'\n" + "Summons a Pitbull puppy\n" + "Barks when any rare enemies are nearby");
        }

        public override void SetDefaults()
        {
            item.CloneDefaults(ItemID.ZephyrFish);
            item.UseSound = SoundID.NPCHit29;
            item.shoot = mod.ProjectileType("Buppy");
            item.value = Item.sellPrice(0, 1, 0, 0);
            item.rare = 1;
            item.buffType = mod.BuffType("BuppyBuff");
        }

        public override void UseStyle(Player player)
        {
            if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
            {
                player.AddBuff(item.buffType, 3600, true);
            }
        }
    }
}