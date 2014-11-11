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
        public event EventHandler<EventArgs> SendEndCallToTerminal;
        public event EventHandler<EventArgs> Tarification;
        public event EventHandler<EventArgs> SendBillToTerminal;

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
            this.PortState = Task3.PortState.Engaged;
            OnSendCallToTerminal(sender, e);
        }

        public void EndCall(object sender, EventArgs e)
        {
            this.PortState = Task3.PortState.Connected;
            OnSendEndCallToTerminal(sender, e);
        }

        public void SetTariff(Tariff t)
        {
            this.Tarification += t.CountDebt;
        }

        public void Tarificate(object sender, EventArgs e)
        {
            this.OnTarification(sender, e);
            this.OnSendBillToTerminal(sender, e);
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

        protected virtual void OnTarification(object sender, EventArgs e)
        {
            var temp = Tarification;
            if (temp != null)
            {
                temp(sender, e);
            }
        }

        protected virtual void OnSendBillToTerminal(object sender, EventArgs e)
        {
            var temp = SendBillToTerminal;
            if (temp != null)
            {
                temp(sender, e);
            }
        }
    }
}
