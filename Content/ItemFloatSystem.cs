using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LootCratesMod.Content.LootDatabase;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace LootCratesMod.Content
{
    public class ItemFloatSystem : GlobalItem
    {
        public override bool InstancePerEntity => true;
        public bool isCrateItem = false;
        bool pickup = true;
        int timer = 60;

        public override void OnSpawn(Item gItem, IEntitySource source)
        {
            if (source is EntitySource_Parent parent && parent.Entity is Projectile proj && LootUtils.IsLootCrateProjectile[proj.type])
            {
                isCrateItem = true;
                pickup = false;
                gItem.velocity.Y = 0.3f;
            }
        }

        public override bool PreDrawInWorld(Item item, SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
        {
            if (isCrateItem && timer > 0)
            {
                scale = (float)(1 - ((double)timer / 120));
            }

            return true;
        }

        public override void Update(Item item, ref float gravity, ref float maxFallSpeed)
        {
            if (isCrateItem)
            {
                if (timer > 0)
                    timer--;
                if (timer > 0)
                {
                    if (item.velocity.Y > -3f)
                        item.velocity.Y = -0.4f;

                    if (item.velocity.Y > 1f)
                        item.velocity.Y = 1f;
                }
                else
                    pickup = true;

                //if (item.scale < 1)
                //item.scale += 0.05f;
            }
        }

        public override bool CanPickup(Item item, Player player)
        {
            return pickup;   
        }

        public override bool CanStackInWorld(Item destination, Item source)
        {
            return pickup;
        }
    }
}
