using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using System.Reflection;
using LootCratesMod.Content.Items;
using System.IO;
using Newtonsoft.Json;
using System.Net;
using Terraria.ID;
using static LootCratesMod.Content.LootDatabase.LootUtils;
using Newtonsoft.Json.Converters;
using Humanizer;
using Terraria.WorldBuilding;

namespace LootCratesMod.Content.LootDatabase
{
    public class LootBox
    {
        public Dictionary<ChestQuality, List<LootItem>> Rarities;
        public Dictionary<string, double> RarityChances { get; set; }

        public static ChestLootResult LoadLootBox(string filePath)
        {
            string json = Encoding.UTF8.GetString(LootCratesMod.Instance.GetFileBytes("Content/LootDatabase/Datas/" + filePath + ".json"));
            LootBox box = JsonConvert.DeserializeObject<LootBox>(json);

            List<Item> lootPool = new();
            ChestQuality rarity = RollRarity(box);

            // Gets the list associated with the rolled rarity from the json.
            if (box.Rarities.TryGetValue(rarity, out List<LootItem> rarityItems))
            {
                foreach (var lootItem in rarityItems)
                {
                    // Gets the chance for the item to be added from the json.
                    if (Chance(lootItem.Chance))
                    {
                        // Searches through ItemID for the item, which is given as a string.
                        // The string is the internal item name, not the actual name.
                        int type = ItemID.Search.GetId(lootItem.Type);

                        // Rolls any alternative items, and gets their chance to replace the main item.
                        if (lootItem.Alternatives != null)
                        {
                            for (int i = 0; i < lootItem.Alternatives.Count; i++)
                            {
                                if (Chance(lootItem.AlternativesChances[i]))
                                {
                                    type = ItemID.Search.GetId(lootItem.Alternatives[i]);
                                }
                            }
                        }
                        int max = lootItem.MaxStack;
                        if (max < 1)
                            max += 1;

                        lootPool.Add(new Item(type, Main.rand.Next(lootItem.MinStack, max)));
                    }
                }
            }

            return new ChestLootResult(lootPool, rarity);
        }

        public static ChestQuality RollRarity(LootBox box)
        {
            double roll = Main.rand.NextDouble();
            double cumulativeChance = 0.0;

            foreach (var kvp in box.RarityChances)
            {
                cumulativeChance += kvp.Value;
                if (roll < cumulativeChance)
                {
                    if (Enum.TryParse<ChestQuality>(kvp.Key, out ChestQuality rarity))
                    {
                        return rarity;
                    }
                }
            }

            // If no rarity is selected, default to Common
            return ChestQuality.Common;
        }

        public static bool Chance(double chance)
        {
            // rolls a double between 0 and 1 & checks if the given value is bigger
            return Main.rand.NextDouble() < (double)chance;
        }
    }
}

