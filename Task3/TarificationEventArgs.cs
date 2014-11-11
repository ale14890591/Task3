using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    public class TarificationEventArgs : EventArgs
    {
        public Number Number { get; set; }
        public int Sum { get; set; }

        public TarificationEventArgs(Number number)
        {
            this.Number = number;
        }
    }
}
