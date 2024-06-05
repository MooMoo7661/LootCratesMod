using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LootCratesMod.Content.Items;
using LootCratesMod.Content.LootDatabase;
using Terraria;
using Terraria.GameContent.ItemDropRules;

namespace LootCratesMod.Content.Projectiles
{
    public class BasicLootCrateProjectile : BaseLootCrateProjectile
    {
        public override string LootFileName { get => "BasicLootCrateData"; }

        public override string ModifyLootFile(string fileName)
        {
            string preString = "Basic/BasicLootCrateData";

            if (!Condition.DownedSkeletron.IsMet())
            {
                return preString + "PreSkeletron";
            }

            return preString;
        }
    }

    public class CamoBasicLootCrateProjectile : BaseLootCrateProjectile
    {
        public override string LootFileName { get => "CamoBasicLootCrateData"; }
    }
}
