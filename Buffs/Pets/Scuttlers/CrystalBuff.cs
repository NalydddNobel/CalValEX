using Terraria;
using Terraria.ModLoader;

namespace CalValEX.Buffs.Pets.Scuttlers
{
    public class CrystalBuff : ModBuff
    {
        public override void SetDefaults()
        {
            DisplayName.SetDefault("Crystal Scuttler");
            Description.SetDefault("Still won't let go of its crystal");
            Main.buffNoTimeDisplay[Type] = true;
            Main.vanityPet[Type] = true;
        }

        public override void Update(Player player, ref int buffIndex)
        {
            player.buffTime[buffIndex] = 18000;
            player.GetModPlayer<CalValEXPlayer>().mCry = true;
            bool petProjectileNotSpawned = player.ownedProjectileCounts[ModContent.ProjectileType<Projectiles.Pets.Scuttlers.CrystalPet>()] <= 0;
            if (petProjectileNotSpawned && player.whoAmI == Main.myPlayer)
            {
                Projectile.NewProjectile(player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, ModContent.ProjectileType<Projectiles.Pets.Scuttlers.CrystalPet>(), 0, 0f, player.whoAmI, 0f, 0f);
            }
        }
    }
}