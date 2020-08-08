using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using CalValEX;
using CalValEX.Items;
using CalValEX.Items.Hooks;
using Terraria.DataStructures;
using System;
using System.Collections.Generic;

namespace CalValEX.Items.Equips
{
	[AutoloadEquip(EquipType.Wings)]
	public class ScryllianWings : ModItem
	{

		public override void SetStaticDefaults() {
            DisplayName.SetDefault("Scryllian Wings");
		}

		public override void SetDefaults() {
			item.width = 42;
			item.height = 26;
			item.rare = 3;
			item.accessory = true;
			item.value = Item.sellPrice(0, 0, 2, 0);
		}
		public override void UpdateAccessory(Player player, bool hideVisual) {
			player.wingTimeMax = 60;
		}

		public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising,
			ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend) {
			ascentWhenFalling = 0.5f;
			ascentWhenRising = 0.1f;
			maxCanAscendMultiplier = 0.5f;
			maxAscentMultiplier = 1.5f;
			constantAscend = 0.1f;
		}

		public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration) {
			speed = 6.5f;
		}
	}
}
