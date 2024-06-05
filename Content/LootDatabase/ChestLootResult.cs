using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;
using Terraria.WorldBuilding;
using static LootCratesMod.Content.LootDatabase.LootUtils;

namespace LootCratesMod.Content.LootDatabase
{
    public class ChestLootResult
    {
        public List<Item> Loot;
        public LootUtils.ChestQuality Quality;

        public string Name { get { return Language.GetTextValue("Mods.LootCratesMod." + Quality.ToString()); } }

        public ChestLootResult(List<Item> loot, LootUtils.ChestQuality quality)
        {
            Loot = loot;
            Quality = quality;
        }
    }
}

