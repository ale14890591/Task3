using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    public class Tariff
    {
        public string Name { get; set; }
        public int PeriodFee { get; set; }
        public int CostPerInterval { get; set; }
        public int FreeIcludedIntervals { get; set; }

        public Tariff(string name, int fee, int costPerInt, int freeIncluded)
        {
            this.Name = name;
            this.CostPerInterval = costPerInt;
            this.FreeIcludedIntervals = freeIncluded;
            this.PeriodFee = fee;
        }
    }
}
