using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    public struct CallRegistryItem
    {
        public Number Caller { get; set; }
        public Number Callee { get; set; }
        public DateTime Beg { get; set; }
        public DateTime End { get; set; }
    }
}
