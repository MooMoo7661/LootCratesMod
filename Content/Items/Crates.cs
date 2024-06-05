using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LootCratesMod.Content.LootDatabase;
using LootCratesMod.Content.Projectiles;
using Microsoft.Xna.Framework;
using Mono.Cecil.Mdb;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace LootCratesMod.Content.Items
{
    public class BasicLootCrate : BaseLootCrateItem
    {
        public override int SourceItem { get => ModContent.ItemType<BasicLootCrate>(); }
        public override int CreateProjectile { get => ModContent.ProjectileType<BasicLootCrateProjectile>(); }
    }

    public class CamoBasicLootCrate : BaseLootCrateItem
    {
        public override int SourceItem { get => ModContent.ItemType<CamoBasicLootCrate>(); }
        public override int CreateProjectile { get => ModContent.ProjectileType<CamoBasicLootCrateProjectile>(); }

        public override void SetDefaults()
        {
            base.SetDefaults();
            LootUtils.CamoVariant[Type] = true;
        }
    }
}
