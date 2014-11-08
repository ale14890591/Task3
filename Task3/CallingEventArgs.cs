using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    public class CallingEventArgs : EventArgs
    {
        public Number DestinationNumber { get; set; }
        public RequestResult RequestResult { get; set; }
        public DateTime Beg { get; set; }
        public DateTime End { get; set; }

        public CallingEventArgs(int number)
        {
            this.DestinationNumber = new Number(number);
        }
    }
}
