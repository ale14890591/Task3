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
        private Number _connectedTerminalNumber;

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
                return _connectedTerminalNumber; 
            }
            set
            {
                this._connectedTerminalNumber = value;
            }
        }

        public event EventHandler<EventArgs> SendCallToTerminal;

        public Port(Number number)
        {
            this._portState = Task3.PortState.Disconnected;
            this._connectedTerminalNumber = number;
        }

        public void ConnectToTerminal()
        {
            this.PortState = Task3.PortState.Connected;
        }

        public void Disconnect()
        {
            this.PortState = Task3.PortState.Disconnected;
        }

        public void Call(object sender, EventArgs e)
        {
            this.PortState = Task3.PortState.Engaged;
            OnSendCallToTerminal(sender, e);
        }

        protected virtual void OnSendCallToTerminal(object sender, EventArgs e)
        {
            var temp = SendCallToTerminal;
            if (temp != null)
            {
                SendCallToTerminal(sender, e);
            }
        }
    }
}
