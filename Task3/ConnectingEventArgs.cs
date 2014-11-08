using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    public class ConnectingEventArgs : EventArgs
    {
        public Number Number { get; set; }
        public bool OperationSuccess { get; set; }

        public ConnectingEventArgs(Number number)
        {
            this.Number = number;
            this.OperationSuccess = false;
        }
    }
}
