using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    public class Contract
    {
        public string Name { get; set; }
        public Number Number { get; set; }
        public List<TariffPeriod> TariffPeriodList = new List<TariffPeriod>();

        public Contract(string name, Number number)
        {
            this.Name = name;
            this.Number = number;
        }
    }
}
