using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    public class Port
    {
        private PortState _portState;
        private Number _connectedTerminal;

        public PortState PortState
        {
            get 
            { 
                return _portState; 
            }
            set
            {
                this._portState = value;
            }
        }

        public Number ConnectedTerminalNumber 
        {
            get 
            { 
                return _connectedTerminal; 
            }
            set
            {
                this._connectedTerminal = value;
            }
        }

        public void ConnectToTerminal(object sender, EventArgs e)
        {
            this.PortState = Task3.PortState.Connected;
            this.ConnectedTerminalNumber = (e as ConnectingEventArgs).Number;
        }
    }
}
