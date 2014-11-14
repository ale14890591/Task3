using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    class SetTariffEventArgs : EventArgs
    {
        public Tariff Tariff { get; set; }
        public bool OperationSuccess { get; set; }
        public DateTime StartPeriod { get; set; }

        public SetTariffEventArgs(Tariff t, DateTime d)
        {
            this.Tariff = t;
            this.StartPeriod = d;
        }
    }
}
