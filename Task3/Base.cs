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
        private CallRegistryList _registry = new CallRegistryList();

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
        public CallRegistryList Registry
        {
            get { return _registry; }
            set { _registry = value; }
        }

        

        public void CreatePort(Number number)
        {
            this._workplace.Add(new Port(number));
        }

        public void RegisterTerminal(params Terminal[] terminal)
        {
            foreach (Terminal t in terminal)
            {
                CreatePort(t.Number);
                t.TryingToConnect += ConnectTerminal;
                t.TryingToDisconnect += DisconnectTerminal;
                t.StartingCall += Call;
                t.FinishCall += EndCall;
                t.Register();
                Console.WriteLine("Terminal {0} has been registrated", t.Number);
            }
        }

        public void ConnectTerminal(object sender, EventArgs e)
        {
            Port temp = null;
            temp = this._workplace.Find(x => x.ConnectedTerminalNumber == (sender as Terminal).Number);
            if (temp != null)
            {
                temp.ConnectToTerminal();
                (e as ConnectingEventArgs).OperationSuccess = true;
                temp.SendCallToTerminal += (sender as Terminal).IncomingCall;
                temp.SendEndCallToTerminal += (sender as Terminal).ReceiveEndCall;
                temp.SendBillToTerminal += (sender as Terminal).ReceiveBill;
            }
        }

        public void DisconnectTerminal(object sender, EventArgs e)
        {
            Port temp = null;
            temp = this._workplace.Find(x => x.ConnectedTerminalNumber == (e as ConnectingEventArgs).Number);
            if (temp != null)
            {
                temp.DisconnectFromTerminal();
                (e as ConnectingEventArgs).OperationSuccess = true;
            }
        }

        public void Call(object sender, EventArgs e)
        {
            Port temp = null;
            temp = this._workplace.Find(x => x.ConnectedTerminalNumber.Value == (e as CallingEventArgs).Callee.Value);
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

        public void EndCall(object sender, EventArgs e)
        {
            Port caller = this._workplace.Find(x => x.ConnectedTerminalNumber.Value == (e as CallingEventArgs).Caller.Value);
            Port callee = this._workplace.Find(x => x.ConnectedTerminalNumber.Value == (e as CallingEventArgs).Callee.Value);

            (e as CallingEventArgs).End = DateTime.Now;
            this._registry.Add(sender, e);
            Console.WriteLine("Conversation between {0} and {1} has been finished at {2}, duration {3}", (e as CallingEventArgs).Caller, (e as CallingEventArgs).Callee, (e as CallingEventArgs).End, (e as CallingEventArgs).End - (e as CallingEventArgs).Beg);
            caller.EndCall(sender, e);
            callee.EndCall(sender, e);
        }

        public void TarificateNumber(Number number)
        {
            Port temp = this._workplace.Find(x => x.ConnectedTerminalNumber.Value == number.Value);
            temp.Tarificate(this, new TarificationEventArgs(number));
        }
    }
}
