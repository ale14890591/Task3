using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    public class CallingEventArgs : EventArgs
    {
        public Number Number { get; set; }
        public RequestResult RequestResult { get; set; } 

        public CallingEventArgs(int number)
        {
            this.Number = new Number(number);
        }
    }
}
