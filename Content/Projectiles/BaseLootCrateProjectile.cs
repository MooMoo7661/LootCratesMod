using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LootCratesMod.Content.LootDatabase;
using Microsoft.Xna.Framework;
using rail;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace LootCratesMod.Content.Projectiles
{
    public abstract class BaseLootCrateProjectile : ModProjectile
    {
        public virtual int Width { get; set; } = 24;
        public virtual int Height { get; set; } = 26;
        public virtual int Frames { get; set; } = 3;
        /// <summary>
        /// Provides a name for the base file.
        /// </summary>
        public abstract string LootFileName { get; }

        private int TimerCap = 180;
        private int timer = 0;
        float oldPos;

        private enum FrameID
        {
            Closed,
            OpenSlight,
            Open
        }

        public override void SetDefaults()
        {
            Projectile.width = Width;
            Projectile.height = Height;
            Main.projFrames[Type] = Frames;
            Projectile.aiStyle = -1;
            Projectile.tileCollide = false;
            LootUtils.IsLootCrateProjectile[Type] = true;
        }

        public override void OnSpawn(IEntitySource source)
        {
            timer = TimerCap;
            oldPos = Projectile.position.X;
            SoundStyle style = new SoundStyle("LootCratesMod/Content/Sounds/Rise_Woosh").WithVolumeScale(0.6f);
            SoundEngine.PlaySound(style);
        }

        /// <summary>
        /// Allows you to add more events for when the loot box is rolled.
        /// <br>Remember to call base, or LootBox.LoadLootBox to actually make the event happen.</br>
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public virtual ChestLootResult Result(string fileName)
        {
            return LootBox.LoadLootBox(ModifyLootFile(fileName));
        }

        /// <summary>
        /// Passes through the given loot file name. Can be overriden to return a different file based on certain conditions.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public virtual string ModifyLootFile(string fileName)
        {
            return fileName;
        }

        public override void AI()
        {
            if (timer > 0)
            {
                timer--;

                if (timer > 35)
                {
                    if (timer % 2 == 0)
                    {
                        oldPos = Projectile.position.X;

                        // bad math for scaling intensity.
                        // In hindsight, I should have just made the counter go from 0 - 180, then that would have fixed some of these math problems....
                        float intensity = Math.Clamp((float)((double)TimerCap / 50 - (double)timer / 50) - 0.45f, 0, 2f);
                        Projectile.position.X += Main.rand.NextFloat(-intensity, intensity);
                    }
                    else Projectile.position.X = oldPos; // reset the position every other frame to prevent gradual movement
                }
                
                // Move upwards at a fixed rate. When 50 or below, it will gradually slow down.
                if (timer > 50)
                {
                    Projectile.frame = 0;
                    if (Projectile.velocity.Y > -0.7f)
                        Projectile.velocity.Y = -0.7f;

                    if (Projectile.velocity.Y > 1f)
                        Projectile.velocity.Y = 1f;
                }
                else
                {
                    Projectile.frame = (int)FrameID.OpenSlight;
                    Projectile.velocity.Y = -0.7f + (0.58f - (timer / 100)) - 0.24f; // slowing

                    if (timer <= 35)
                    {
                        Projectile.position.X = oldPos;

                        // open loot box
                        if (timer == 35)
                        {
                            var result = Result(LootFileName);
                            LootUtils.ChestOpenEffect(Projectile, result.Quality);
                            var text = CombatText.NewText(Projectile.getRect(), LootUtils.ColorFromRarity(result.Quality), result.Name);

                            foreach (var item in result.Loot)
                            {
                                Item.NewItem(Projectile.GetSource_FromThis(), Projectile.Center, item);
                            }
                        }

                        Projectile.frame = (int)FrameID.Open;
                        Projectile.velocity.Y = 0;
                    }

                    if (timer <= 10)
                        Projectile.alpha += 25;
                }
            }
            else
            {
                Projectile.Kill();
            }
        }

    }
}
