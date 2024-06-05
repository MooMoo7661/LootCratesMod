using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LootCratesMod.Content.Items;
using LootCratesMod.Content.LootDatabase;
using Terraria;
using Terraria.GameContent.ItemDropRules;
using Terraria.ID;
using Terraria.ModLoader;

namespace LootCratesMod.Content.GlobalClasses
{
    public class NPCLoot : GlobalNPC
    {
        public override void ModifyGlobalLoot(GlobalLoot globalLoot)
        {
            globalLoot.Add(ItemDropRule.Common(LootUtils.CurrentCommonCrate, 2));
        }
    }
}
