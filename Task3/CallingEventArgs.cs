using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    public class CallingEventArgs : EventArgs
    {
        public Number Caller { get; set; }
        public Number Callee { get; set; }
        public RequestResult RequestResult { get; set; }
        public DateTime Beg { get; set; }
        public DateTime End { get; set; }

        public CallingEventArgs(Number from, int to)
        {
            this.Caller = from;
            this.Callee = new Number(to);
            this.RequestResult = Task3.RequestResult.DoesntExist;
        }
    }
}
