using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    public class CallRegistryList : List<CallRegistryItem>
    {
        public void Add(object sender, EventArgs e)
        {
            CallRegistryItem temp = new CallRegistryItem();
            temp.Caller = (e as CallingEventArgs).Caller;
            temp.Callee = (e as CallingEventArgs).Callee;
            temp.Beg = (e as CallingEventArgs).Beg;
            temp.End = (e as CallingEventArgs).End;

            this.Add(temp);
        }
    }
}
