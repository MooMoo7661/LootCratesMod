using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LootCratesMod.Content.Items;
using Newtonsoft.Json.Converters;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria;
using System.Text.Json.Serialization;
using Microsoft.Xna.Framework;
using Terraria.WorldBuilding;

namespace LootCratesMod.Content.LootDatabase
{
    public class LootUtils : ModSystem
    {
        public static bool[] IsLootCrate = ItemID.Sets.Factory.CreateBoolSet();
        public static bool[] IsLootCrateProjectile = ProjectileID.Sets.Factory.CreateBoolSet();

        public static bool[] IsVariant = ItemID.Sets.Factory.CreateBoolSet();
        public static bool[] CamoVariant = ItemID.Sets.Factory.CreateBoolSet();
        public static bool[] WinterVariant = ItemID.Sets.Factory.CreateBoolSet();
        public static bool[] GoldenVariant = ItemID.Sets.Factory.CreateBoolSet();

        public static int CurrentCommonCrate
        {
            get
            {
                if (!Condition.DownedEarlygameBoss.IsMet() && !Main.hardMode)
                {
                    return ModContent.ItemType<BasicLootCrate>();
                }
                else if (!Main.hardMode)
                {
                    return ModContent.ItemType<BasicLootCrate>();
                }

                return ModContent.ItemType<BasicLootCrate>();
            }
        }

        [JsonConverter(typeof(StringEnumConverter))]
        public enum ChestQuality
        {
            Common,
            Uncommon,
            Rare,
            Epic,
            Legendary
        }

        public enum ChestVariant
        {
            Common,
            Camo,
            WinterCamo,
            Golden
        }

        public static void ChestOpenEffect(Projectile projectile, ChestQuality quality)
        {
            SoundStyle sound = new SoundStyle("LootCratesMod/Content/Sounds/" + quality.ToString() + "Quality");
            SoundEngine.PlaySound(sound);
        }

        public static Color ColorFromRarity(ChestQuality quality)
        {
            switch (quality)
            {
                case ChestQuality.Common: return new Color(138, 138, 235);
                case ChestQuality.Uncommon: return new Color(134, 226, 134);
                case ChestQuality.Rare: return new Color(4, 195, 249);
                case ChestQuality.Epic: return new Color(225, 6, 67);
                case ChestQuality.Legendary: return new Color(221, 152, 1);

                default: return new Color(134, 226, 134);
            }
        }
    }
}
