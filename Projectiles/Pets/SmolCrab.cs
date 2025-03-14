﻿using Terraria;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace CalValEX.Projectiles.Pets
{
    public class SmolCrab : WalkingPet
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Smol Crab");
            Main.projFrames[projectile.type] = 6; //frames
            Main.projPet[projectile.type] = true;
        }

        public override void SafeSetDefaults()
        {
            projectile.width = 36;
            projectile.height = 40;
            projectile.penetrate = -1;
            projectile.netImportant = true;
            projectile.timeLeft *= 5;
            projectile.friendly = true;
            projectile.ignoreWater = true;
            projectile.tileCollide = true;
            facingLeft = false; //is the sprite facing left? if so, put this to true. if its facing to right keep it false.
            spinRotation = false; //should it spin? if that's the case, set to true. else, leave it false.
            shouldFlip = true;
        }

        //all things should be synchronized. most things vanilla already does for us, however you should sync the things you
        //made yourself as they are not synchronized alone by the server.
        public override void SetPetGravityAndDrag()
        {
            gravity = 0.1f; //needs to be positive for the pet to be pushed down platforms plus for it to have gravity
            drag[0] = 0.92f; //idle drag
            drag[1] = 0.95f; //walking drag
        }

        public override void SetPetDistances()
        {
            distance[0] = 2400f; //teleport
            distance[1] = 560f; //speed increase
            distance[2] = 140f; //when to walk
            distance[3] = 40f; //when to stop walking
            distance[4] = 448f; //when to fly
            distance[5] = 180f; //when to stop flying
        }

        public override void SetPetSpeedsAndInertia()
        {
            speed[0] = 10f; //walking speed
            speed[1] = 12f; //flying speed

            inertia[0] = 20f; //walking inertia
            inertia[1] = 80f; //flight inertia
        }

        public override void SetJumpSpeeds()
        {
            jumpSpeed[0] = -4f; //1 tile above pet
            jumpSpeed[1] = -6f; //2 tiles above pet
            jumpSpeed[2] = -8f; //5 tiles above pet
            jumpSpeed[3] = -7f; //4 tiles above pet
            jumpSpeed[4] = -6.5f; //any other tile number above pet
        }

        public override void SetFrameLimitsAndFrameSpeed()
        {
            idleFrameLimits[0] = 0;
            idleFrameLimits[1] = 3; //what your min idle frame is (start of idle animation)

            walkingFrameLimits[0] = 0; //what your min walking frame is (start of walking animation)
            walkingFrameLimits[1] = 3; //what your max walking frame is (end of walking animation)

            flyingFrameLimits[0] = 4;
            flyingFrameLimits[1] = 5;

            animationSpeed[0] = 9; //idle animation speed
            animationSpeed[1] = 8; //walking animation speed
            animationSpeed[2] = 10; //flying animation speed
            spinRotationSpeedMult = 2.5f; //how fast it should spin
            //put the below to -1 if you dont want a jump animation (so its just gonna continue it's walk animation
            animationSpeed[3] = -1; //jumping animation speed

            jumpFrameLimits[0] = -1; //what your min jump frame is (start of jump animation)
            jumpFrameLimits[1] = -1; //what your max jump frame is (end of jump animation)

            jumpAnimationLength = -1; //how long the jump animation should stay
        }
        int idlecounter = 0;
        int sideflip;
        public override void SafeAI(Player player)
        {
            CalValEXPlayer modPlayer = player.GetModPlayer<CalValEXPlayer>();

            if (player.dead)
                modPlayer.SmolCrab = false;
            if (modPlayer.SmolCrab)
                projectile.timeLeft = 2;
            Player owner = Main.player[projectile.owner];
            Vector2 vectorToOwner = owner.Center - projectile.Center;
            float distanceToOwner = vectorToOwner.Length();

            /* THIS CODE ONLY RUNS AFTER THE MAIN CODE RAN.
             * for custom behaviour, you can check if the projectile is walking or not via projectile.localAI[1]
             * you should make new custom behaviour with numbers higher than 2, or less than 0
             * the next few lines is an example on how to implement this
             *
             * switch ((int)projectile.localAI[1])
             * {
             *     case -1:
             *         break;
             *     case 3:
             *         break;
             * }
             *
             * 0, 1 and 2 are already in use.
             * 0 = idling
             * 1 = walking
             * 2 = flying
             *
             * you can still use these, changing thing inside (however it's not recomended unless you want to add custom behaviour to these)
             */
            sideflip = player.direction == -1 ? 10 : 8;
            if (player.velocity.X == 0 && player.velocity.Y == 0)
            {
                idlecounter++;
            }
            else
            {
                idlecounter = 0;
            }

            if (((idlecounter == 300 && distanceToOwner < 40) || Main.LocalPlayer.HasItem(ModContent.ItemType<Items.PutridShroom>())) && !modPlayer.rockhat && !modPlayer.conejo && !modPlayer.aesthetic)
            {
                projectile.localAI[1] = 3;
            }
            switch ((int)projectile.localAI[1])
             {
                 case 3:

                    projectile.position.X = player.position.X - sideflip;
                    projectile.position.Y = player.position.Y - 40;
                    projectile.frameCounter++;
                    if (projectile.frameCounter >= animationSpeed[0])
                    {
                        projectile.frameCounter = 0;
                        projectile.frame++;
                        if (projectile.frame < idleFrameLimits[0] || projectile.frame > idleFrameLimits[1])
                            projectile.frame = idleFrameLimits[0];
                    }
                    if ((idlecounter <= 0 && !Main.LocalPlayer.HasItem(ModContent.ItemType<Items.PutridShroom>())) || modPlayer.rockhat || modPlayer.conejo || modPlayer.aesthetic)
                    {
                            projectile.localAI[1] = 1;
                    }
                      break;
             }
    }
        public override void PostDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D glowMask = mod.GetTexture("Projectiles/Pets/SmolCrab_Glow");
	    if (CalValEX.month == 12)
	    {
		glowMask = mod.GetTexture("ExtraTextures/ChristmasPets/SmolCrabGlow");
 	    }
	    else
	    {
		glowMask = mod.GetTexture("Projectiles/Pets/SmolCrab_Glow");
 	    }
            Rectangle frame = glowMask.Frame(1, Main.projFrames[projectile.type], 0, projectile.frame);
            frame.Height -= 1;
            float originOffsetX = (glowMask.Width - projectile.width) * 0.5f + projectile.width * 0.5f + drawOriginOffsetX;
            spriteBatch.Draw
            (
                glowMask,
                projectile.position - Main.screenPosition + new Vector2(originOffsetX + drawOffsetX, projectile.height / 2 + projectile.gfxOffY),
                frame,
                Color.White,
                projectile.rotation,
                new Vector2(originOffsetX, projectile.height / 2 - drawOriginOffsetY),
                projectile.scale,
                projectile.spriteDirection == -1 ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
                0f
            );
        }
    }
}