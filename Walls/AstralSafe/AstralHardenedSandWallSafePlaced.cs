﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using CalValEX.Items.Walls.Astral;
using CalValEX.Dusts;

namespace CalValEX.Walls.AstralSafe
{
    public class AstralHardenedSandWallSafePlaced : ModWall
    {
        public override void SetDefaults()
        {
            Main.wallHouse[Type] = true;
            drop = ModContent.ItemType<AstralHardenedSandWall>();
            AddMapEntry(new Color(37, 10, 38));
            dustType = ModContent.DustType<AstralDust>();
        }
    }
}