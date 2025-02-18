﻿using MonoMod.Cil;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using CalValEX.Items.Critters;
using CalValEX.Items;

namespace CalValEX.NPCs.Critters
{
    /// <summary>
    /// This file shows off a critter npc. The unique thing about critters is how you can catch them with a bug net.
    /// The important bits are: Main.npcCatchable, npc.catchItem, and item.makeNPC
    /// We will also show off adding an item to an existing RecipeGroup (see ExampleMod.AddRecipeGroups)
    /// </summary>
    public class XerocodileSwim : ModNPC
    {
        public override bool Autoload(ref string name)
        {
            IL.Terraria.Wiring.HitWireSingle += HookStatue;
            return base.Autoload(ref name);
        }

        /// <summary>
        /// Change the following code sequence in Wiring.HitWireSingle
        /// num12 = Utils.SelectRandom(Main.rand, new short[5]
        /// {
        /// 	359,
        /// 	359,
        /// 	359,
        /// 	359,
        /// 	360,
        /// });
        ///
        /// to
        ///
        /// var arr = new short[5]
        /// {
        /// 	359,
        /// 	359,
        /// 	359,
        /// 	359,
        /// 	360,
        /// }
        /// arr = arr.ToList().Add(id).ToArray();
        /// num12 = Utils.SelectRandom(Main.rand, arr);
        ///
        /// </summary>
        /// <param name="il"></param>
        private void HookStatue(ILContext il)
        {
            // obtain a cursor positioned before the first instruction of the method
            // the cursor is used for navigating and modifying the il
            var c = new ILCursor(il);

            // the exact location for this hook is very complex to search for due to the hook instructions not being unique, and buried deep in control flow
            // switch statements are sometimes compiled to if-else chains, and debug builds litter the code with no-ops and redundant locals

            // in general you want to search using structure and function rather than numerical constants which may change across different versions or compile settings
            // using local variable indices is almost always a bad idea

            // we can search for
            // switch (*)
            //   case 56:
            //     Utils.SelectRandom *

            // in general you'd want to look for a specific switch variable, or perhaps the containing switch (type) { case 105:
            // but the generated IL is really variable and hard to match in this case

            // we'll just use the fact that there are no other switch statements with case 56, followed by a SelectRandom

            ILLabel[] targets = null;
            while (c.TryGotoNext(i => i.MatchSwitch(out targets)))
            {
                // some optimising compilers generate a sub so that all the switch cases start at 0
                // ldc.i4.s 51
                // sub
                // switch
                int offset = 0;
                if (c.Prev.MatchSub() && c.Prev.Previous.MatchLdcI4(out offset))
                {
                    ;
                }

                // get the label for case 56: if it exists
                int case56Index = 56 - offset;
                if (case56Index < 0 || case56Index >= targets.Length || !(targets[case56Index] is ILLabel target))
                {
                    continue;
                }

                // move the cursor to case 56:
                c.GotoLabel(target);
                // there's lots of extra checks we could add here to make sure we're at the right spot, such as not encountering any branching instructions
                c.GotoNext(i => i.MatchCall(typeof(Utils), nameof(Utils.SelectRandom)));

                // goto next positions us before the instruction we searched for, so we can insert our array modifying code right here
                c.EmitDelegate<Func<short[], short[]>>(arr =>
                {
                    // resize the array and add our custom snail
                    Array.Resize(ref arr, arr.Length + 1);
                    arr[arr.Length - 1] = (short)npc.type;
                    return arr;
                });

                // hook applied successfully
                return;
            }

            // couldn't find the right place to insert
            throw new Exception("Hook location not found, switch(*) { case 56: ...");
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Xerocodile");
            Main.npcFrameCount[npc.type] = 6;
            Main.npcCatchable[npc.type] = true;
        }

        public override void SetDefaults()
        {
            npc.noGravity = true;
            npc.width = 20;
            npc.height = 24;
            npc.aiStyle = 16;
            npc.damage = 0;
            npc.defense = 0;
            npc.lifeMax = 666;
            npc.HitSound = SoundID.NPCHit50;
            npc.DeathSound = SoundID.NPCDeath54;
            npc.npcSlots = 0.25f;
            npc.catchItem = (short)ItemType<XerocodileItem>();
            npc.lavaImmune = false;
            npc.friendly = true; // We have to add this and CanBeHitByItem/CanBeHitByProjectile because of reasons.
            banner = NPCType<Xerocodile>();
            bannerItem = ItemType<Items.Tiles.Banners.XerocodileBanner>();
            aiType = NPCID.Goldfish;
            animationType = NPCID.Goldfish;
        }
        public override void AI()
        {
            Mod clamMod = ModLoader.GetMod("CalamityMod");
            if (!npc.wet)
            {
                npc.Transform(ModContent.NPCType<Xerocodile>());
            }
            if (!Main.bloodMoon)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, clamMod.ItemType("Xerocodile"), 1, false, 0, false, false);
                npc.active = false;
            }
        }

        public override bool? CanBeHitByItem(Player player, Item item)
        {
            return true;
        }

        public override bool? CanBeHitByProjectile(Projectile projectile)
        {
            return true;
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            Mod clamMod = ModLoader.GetMod("CalamityMod"); //this is to get calamity mod, you have to add 'weakReferences = CalamityMod@1.4.4.4' (without the '') in your build.txt for this to work
            if (clamMod != null)
            {
                if ((bool)clamMod.Call("GetBossDowned", "providence") && Main.bloodMoon && !CalValEXConfig.Instance.CritterSpawns)
                {
                    if (spawnInfo.playerSafe)
                    {
                        return SpawnCondition.WaterCritter.Chance * 0.01f;
                    }
                    else if (!Main.eclipse && !Main.pumpkinMoon && !Main.snowMoon)
                    {
                        return SpawnCondition.OceanMonster.Chance * 0.0015f;
                    }
                }
            }
            return 0f;
        }

        public override void OnCatchNPC(Player player, Item item)
        {
            item.stack = 1;
        }
        public override void NPCLoot()
        {
            Mod clamMod = ModLoader.GetMod("CalamityMod");
            if ((bool)clamMod.Call("GetBossDowned", "yharon"))
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, ModContent.ItemType<Termipebbles>(), 1, false, 0, false, false);
            }
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/Xerocodile"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/Xerocodile2"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/Xerocodile3"), 1f);
            }
        }
    }
}