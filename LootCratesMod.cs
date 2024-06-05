using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.ModLoader;

namespace LootCratesMod
{
	// Please read https://github.com/tModLoader/tModLoader/wiki/Basic-tModLoader-Modding-Guide#mod-skeleton-contents for more information about the various files in a mod.
	public class LootCratesMod : Mod
	{
		public static LootCratesMod Instance;

        public override void Load()
        {
            Instance = this;
        }
    }
}
