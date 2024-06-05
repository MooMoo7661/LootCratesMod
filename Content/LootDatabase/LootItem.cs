using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LootCratesMod.Content.LootDatabase
{
    public class LootItem
    {
        public string Type { get; set; }
        public double Chance { get; set; }
        public int MinStack { get; set; }
        public int MaxStack { get; set; }
        public List<string> Alternatives { get; set; }
        public List<double> AlternativesChances { get; set; }
    }
}
