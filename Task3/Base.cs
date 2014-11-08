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
        private CallRegistry _registry = new CallRegistry();

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
        public CallRegistry Registry
        {
            get { return _registry; }
            set { _registry = value; }
        }

        

        public void CreatePort(Number number)
        {
            this._workplace.Add(new Port(number));
        }

        public void ConnectToPort(object sender, EventArgs e)
        {
            Port temp = null;
            temp = this._workplace.Find(x => x.ConnectedTerminalNumber == (sender as Terminal).Number);
            if (temp != null)
            {
                temp.ConnectToTerminal();
                (e as ConnectingEventArgs).OperationSuccess = true;
                temp.SendCallToTerminal += (sender as Terminal).IncomingCall;
            }
        }

        public void Disconnect(object sender, EventArgs e)
        {
            Port temp = null;
            temp = this._workplace.Find(x => x.ConnectedTerminalNumber == (e as ConnectingEventArgs).Number);
            if (temp != null)
            {
                temp.Disconnect();
                (e as ConnectingEventArgs).OperationSuccess = true;
            }
        }

        public void Call(object sender, EventArgs e)
        {
            Port temp = null;//ICompar
            temp = this._workplace.Find(x => x.ConnectedTerminalNumber.Value == (e as CallingEventArgs).DestinationNumber.Value);
            if (temp == null)
            {
                (e as CallingEventArgs).RequestResult = RequestResult.DoesntExist;
            }
            else
            {
                switch (temp.PortState)
                {
                    case PortState.Disconnected:
                        {
                            (e as CallingEventArgs).RequestResult = RequestResult.IsOff;
                            break;
                        }
                    case PortState.Engaged:
                        {
                            (e as CallingEventArgs).RequestResult = RequestResult.IsEngaged;
                            break;
                        }
                    case PortState.Connected:
                        {
                            temp.Call(sender, e);
                            break;
                        }
                }
            }
        }
    }
}
