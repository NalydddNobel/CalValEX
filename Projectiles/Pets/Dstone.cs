﻿using CalValEX.Buffs.LightPets;
using Terraria;
using Terraria.ModLoader;

namespace CalValEX.Projectiles.Pets
{
    public class Dstone : WalkingPet
    {
        float extramultiplier = 0f;
        int lifebonus = 0;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Unhappy Stone");
            Main.projFrames[projectile.type] = 1; //frames
            Main.projPet[projectile.type] = true;
        }

        public override void SafeSetDefaults() //SAFE SET DEFAULTS!!!
        {
            projectile.width = 36;
            projectile.height = 36;
            projectile.ignoreWater = true;
            projectile.tileCollide = true;
            /* you don't need to set these anymore!!
            projectile.penetrate = -1;
            projectile.netImportant = true;
            projectile.timeLeft *= 5;
            projectile.friendly = true;
            */
            //THIS IS NEEDED
            facingLeft = true; //is the sprite facing left? if so, put this to true. if its facing to right keep it false.
            spinRotation = true; //should it spin? if that's the case, set to true. else, leave it false.
            shouldFlip = false; //should the sprite flip? if that's the case, set to true. else, put it to false.
        }

        //these below are not needed. only use what you need to change. most of the time you only need to change SetFrameLimitsAndFrameSpeed().

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
            distance[4] = 548f; //when to fly
            distance[5] = 180f; //when to stop flying
        }

        public override void SetPetSpeedsAndInertia()
        {
            speed[0] = 17f; //walking speed
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
            Player player = Main.LocalPlayer;
            idleFrameLimits[0] = idleFrameLimits[1] = 1; //what your min idle frame is (start of idle animation)

            walkingFrameLimits[0] = walkingFrameLimits[1] = 1;//what your min walking frame is (start of walking animation)
                                                                                               //what your max walking frame is (end of walking animation)

            flyingFrameLimits[0] = flyingFrameLimits[1] = 1; //what your min flying frame is (start of flying animation)

            animationSpeed[0] = 8; //idle animation speed
            animationSpeed[1] = 8; //walking animation speed
            animationSpeed[2] = 10; //flying animation speed
            spinRotationSpeedMult = 5.5f + extramultiplier; //how fast it should spin
            //put the below to -1 if you dont want a jump animation (so its just gonna continue it's walk animation
            animationSpeed[3] = -1; //jumping animation speed

            jumpFrameLimits[0] = -1; //what your min jump frame is (start of jump animation)
            jumpFrameLimits[1] = -1; //what your max jump frame is (end of jump animation)

            jumpAnimationLength = -1; //how long the jump animation should stay
        }

        public override void SafeAI(Player player)
        {
            CalValEXPlayer modPlayer = player.GetModPlayer<CalValEXPlayer>();

            if (player.dead)
                modPlayer.Dstone = false;
            if (modPlayer.Dstone)
                projectile.timeLeft = 2;

            if (player.statLife < (player.statLifeMax2))
            {
                extramultiplier = 0.01f + (float)lifebonus * 0.05f;
                lifebonus = player.statLifeMax2 - player.statLife;
            }
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
        }
    }
}

