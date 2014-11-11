using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    public class Tariff
    {
        public int PeriodFee { get; set; }
        public int CostPerInterval { get; set; }
        public int FreeIcludedIntervals { get; set; }

        public Tariff(int fee, int costPerInt, int freeIncluded)
        {
            this.CostPerInterval = costPerInt;
            this.FreeIcludedIntervals = freeIncluded;
            this.PeriodFee = fee;
        }

        public void CountDebt(object sender, EventArgs e)
        {
            IEnumerable<int> selection = (sender as Base).Registry.Where(x => x.Caller.Value == (e as TarificationEventArgs).Number.Value).Select(y => (y.End - y.Beg).Minutes);
            int paidDuration = 0;
            if(selection.Sum() >= FreeIcludedIntervals)
                paidDuration = selection.Sum() - FreeIcludedIntervals;
            (e as TarificationEventArgs).Sum = CostPerInterval * paidDuration + PeriodFee;
        }
    }
}
