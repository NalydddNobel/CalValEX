﻿using CalValEX.Items.Tiles.Banners;
using MonoMod.Cil;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using CalValEX.Items.Critters;

namespace CalValEX.NPCs.Critters
{
    /// <summary>
    /// This file shows off a critter npc. The unique thing about critters is how you can catch them with a bug net.
    /// The important bits are: Main.npcCatchable, npc.catchItem, and item.makeNPC
    /// We will also show off adding an item to an existing RecipeGroup (see ExampleMod.AddRecipeGroups)
    /// </summary>
    public class Blightolemur : ModNPC
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
            DisplayName.SetDefault("Bleamur");
            Main.npcFrameCount[npc.type] = 5;
            Main.npcCatchable[npc.type] = true;
        }

        public override void SetDefaults()
        {
            npc.width = 38;
            npc.height = 38;
            //npc.aiStyle = 67;
            //npc.damage = 0;
            //npc.defense = 0;
            //npc.lifeMax = 2000;

            //npc.noGravity = true;
            //npc.catchItem = 2007;

            npc.CloneDefaults(NPCID.Squirrel);
            npc.catchItem = (short)ItemType<BlightolemurItem>();
            npc.lavaImmune = false;
            //npc.aiStyle = 0;
            npc.friendly = true; // We have to add this and CanBeHitByItem/CanBeHitByProjectile because of reasons.
            aiType = NPCID.Squirrel;
            animationType = NPCID.Squirrel;
            npc.npcSlots = 0.25f;
            npc.lifeMax = 20;
            for (int i = 0; i < npc.buffImmune.Length; i++)
            {
                npc.buffImmune[(ModLoader.GetMod("CalamityMod").BuffType("AstralInfectionDebuff"))] = false;
            }
            banner = npc.type;
            bannerItem = ItemType<BleamurBanner>();
            npc.HitSound = mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/ViolemurHit");
            npc.DeathSound = mod.GetLegacySoundSlot(SoundType.Custom, "Sounds/ViolemurDeath");
        }

        public override bool? CanBeHitByItem(Player player, Item item)
        {
            return true;
        }

        public override bool? CanBeHitByProjectile(Projectile projectile)
        {
            return true;
        }

        public override void AI()
        {
            if (!CalValEXConfig.Instance.ViolemurDefense)
            {
                npc.dontTakeDamageFromHostiles = true;
                npc.netUpdate = true;
            }
            else
            {
                npc.dontTakeDamageFromHostiles = false;
                npc.netUpdate = true;
            }
        }

        public override float SpawnChance(NPCSpawnInfo spawnInfo)
        {
            Mod clamMod = ModLoader.GetMod("CalamityMod"); //this is to get calamity mod, you have to add 'weakReferences = CalamityMod@1.4.4.4' (without the '') in your build.txt for this to work
            if (clamMod != null)
            {
                if (spawnInfo.player.GetModPlayer<CalValEXPlayer>().ZoneAstral && !CalValEXConfig.Instance.CritterSpawns)
                {
                    if (spawnInfo.playerSafe)
                    {
                        return SpawnCondition.TownCritter.Chance * 0.5f;
                    }
                    else if (!Main.eclipse && !Main.bloodMoon && !Main.pumpkinMoon && !Main.snowMoon)
                    {
                        return 0.15f;
                    }
                }
            }
            return 0f;
        }

        public override void OnCatchNPC(Player player, Item item)
        {
            item.stack = 1;
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)
            {
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/Bleamur"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/Bleamur2"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/Bleamur3"), 1f);
                Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/Bleamur4"), 1f);
            }
        }

        public override void PostDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D minibirbsprite = (ModContent.GetTexture("CalValEX/NPCs/Critters/Blightolemur_Glow"));

            float minibirbframe = 5f / (float)Main.npcFrameCount[npc.type];
            int minibirbheight = (int)(float)((npc.frame.Y / npc.frame.Height) * minibirbframe) * (minibirbsprite.Height / 5);

            Rectangle minibirbsquare = new Rectangle(0, minibirbheight + 5, minibirbsprite.Width, minibirbsprite.Height / 5);
            SpriteEffects minibirbeffects = (npc.direction != -1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            spriteBatch.Draw(minibirbsprite, npc.Center - Main.screenPosition + new Vector2(0f, npc.gfxOffY), minibirbsquare, Color.White, npc.rotation, Utils.Size(minibirbsquare) / 2f, npc.scale, minibirbeffects, 0f);
        }
    }
}