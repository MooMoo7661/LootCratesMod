using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Terraria.Audio;
using Microsoft.Xna.Framework;
using LootCratesMod.Content.LootDatabase;

namespace LootCratesMod.Content.Items
{
    public abstract class BaseLootCrateItem : ModItem
    {
        public virtual int Width { get; } = 24;
        public virtual int Height { get; } = 26;
        public virtual int Rarity { get; } = ItemRarityID.Blue;
        public virtual int Value { get; } = 100;
        public virtual SoundStyle UseSound { get; } = SoundID.Unlock;

        public abstract int SourceItem { get; }
        public abstract int CreateProjectile { get; }

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 5;
            LootUtils.IsLootCrate[Type] = true;
        }

        public override void SetDefaults()
        {
            Item.width = Width;
            Item.height = Height;
            Item.maxStack = Item.CommonMaxStack;
            Item.rare = Rarity;
            Item.useTime = 30;
            Item.useAnimation = 30;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.noUseGraphic = true;
            Item.UseSound = UseSound;
            Item.consumable = true;
            Item.value = Value;
        }

        public override bool? UseItem(Player player)
        {
            
            Projectile.NewProjectile(player.GetSource_ItemUse(new(SourceItem)), player.Center, Vector2.Zero, CreateProjectile, 0, 0, player.whoAmI);
            return true;
        }
    }
}
