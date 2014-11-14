using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    public class TariffPeriod
    {
        public Tariff Tariff { get; set; }
        public DateTime Beg { get; set; }
        public DateTime End { get; set; }

        public TariffPeriod(Tariff tariff, DateTime beg)
        {
            this.Tariff = tariff;
            this.Beg = beg;
        }
    }
}
