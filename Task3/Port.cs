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
        public DateTime StartPeriod { get; set; }

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
        public event EventHandler<EventArgs> SendEndCallToTerminal;

        public Port(Number number)
        {
            this._portState = Task3.PortState.Disconnected;
            this._connectedTerminalNumber = number;
        }

        public void ConnectToTerminal()
        {
            this.PortState = Task3.PortState.Connected;
        }

        public void DisconnectFromTerminal()
        {
            this.PortState = Task3.PortState.Disconnected;
        }

        public void Call(object sender, EventArgs e)
        {
            if (this._portState == Task3.PortState.Connected)
            {
                this.PortState = Task3.PortState.Engaged;
                OnSendCallToTerminal(sender, e);
                if ((e as CallingEventArgs).RequestResult == RequestResult.Rejection)
                    this._portState = Task3.PortState.Connected;
            }
        }

        public void EndCall(object sender, EventArgs e)
        {
            this.PortState = Task3.PortState.Connected;
            OnSendEndCallToTerminal(sender, e);
        }

        protected virtual void OnSendCallToTerminal(object sender, EventArgs e)
        {
            var temp = SendCallToTerminal;
            if (temp != null)
            {
                temp(sender, e);
            }
        }

        protected virtual void OnSendEndCallToTerminal(object sender, EventArgs e)
        {
            var temp = SendEndCallToTerminal;
            if (temp != null)
            {
                temp(sender, e);
            }
        }
    }
}
