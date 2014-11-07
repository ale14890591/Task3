using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    public class Base
    {
        private List<Port> _workplace = new List<Port>();

        public List<Port> Workplace
        {
            get
            {
                return _workplace;
            }
            set
            {
                this._workplace = value;
            }
        }

        

        public void CreatePort(object sender, EventArgs e)
        {
            this._workplace.Add(new Port());
        }

        
    }
}
