using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    public class Terminal
    {
        private Number _number;
        private TerminalState _state;
        private bool _isRegistrated;
        private CallingEventArgs _callArgs = null;

        public Number Number
        {
            get { return _number; }
        }

        public event EventHandler<EventArgs> TryingToConnect;
        public event EventHandler<EventArgs> TryingToDisconnect;
        public event EventHandler<EventArgs> StartingCall;
        public event EventHandler<EventArgs> FinishCall;



        //construstors
        public Terminal(int number)
        {
            this._number = new Number(number);
            this._state = TerminalState.Off;
            this._isRegistrated = false;
            Console.WriteLine("Terminal {0} created", this.Number);
        }

        public void Register()
        {
            this._isRegistrated = true;
        }

        public void Deregister()
        {
            this._isRegistrated = false;
        }

        public bool ConnectToBase()
        {
            ConnectingEventArgs args = new ConnectingEventArgs(this._number);
            OnTryingToConnect(this, args);
            if (args.OperationSuccess)
            {
                this._state = TerminalState.On;
                Console.WriteLine("Terminal {0} is on", this.Number);
            }
            return args.OperationSuccess;
        }

        public bool DisconnectFromBase()
        {
            ConnectingEventArgs args = new ConnectingEventArgs(this._number);
            OnTryingToDisconnect(this, args);
            if (args.OperationSuccess)
            {
                this._state = TerminalState.Off;
                Console.WriteLine("Terminal {0} is off", this.Number);
            }
            return args.OperationSuccess;
        }

        public void Call(int number)
        {
            if (this._isRegistrated)
            {
                Console.WriteLine("Terminal {0} calls terminal {1}", this.Number, number);
                this._state = TerminalState.Engaged;
                this._callArgs = new CallingEventArgs(this._number, number);
                OnStartingCall(this, this._callArgs);
                switch (this._callArgs.RequestResult)
                {
                    case RequestResult.ConnectionSuccess:
                        {
                            this._callArgs.Beg = DateTime.Now;
                            Console.WriteLine("Conversation between {0} and {1} has started at {2}", this.Number, number, this._callArgs.Beg);
                            break;
                        }
                    case RequestResult.DoesntExist:
                        {
                            Console.WriteLine("Terminal {0} doesn't exist", number);
                            this._state = TerminalState.On;
                            break;
                        }
                    case RequestResult.IsEngaged:
                        {
                            Console.WriteLine("Terminal {0} is engaged", number);
                            this._state = TerminalState.On;
                            break;
                        }
                    case RequestResult.IsOff:
                        {
                            Console.WriteLine("Terminal {0} is off", number);
                            this._state = TerminalState.On;
                            break;
                        }
                    case RequestResult.Rejection:
                        {
                            Console.WriteLine("Terminal {0} has rejected your call", number);
                            this._state = TerminalState.On;
                            break;
                        }
                }
            }
            else
            {
                Console.WriteLine("Terminal {0} isn't registrated yet and can't make a call", this._number);
            }
        }

        public void IncomingCall(object sender, EventArgs e)
        {
            Console.WriteLine("Terminal {0} receives a call from {1}", this.Number, (sender as Terminal).Number);
            this._state = TerminalState.Engaged;

            Random rnd = new Random();
            int ans = rnd.Next(2);

            bool answer = ans == 1 ? true : false;
            if (answer)
            {
                Console.WriteLine("Terminal {0} accepts the call from {1}", this.Number, (sender as Terminal).Number);
                (e as CallingEventArgs).RequestResult = RequestResult.ConnectionSuccess;
                this._callArgs = e as CallingEventArgs;
            }
            else
            {
                Console.WriteLine("Terminal {0} rejects the call from {1}", this.Number, (sender as Terminal).Number);
                (e as CallingEventArgs).RequestResult = RequestResult.Rejection;
                this._callArgs = e as CallingEventArgs;
                this._state = TerminalState.On;
            }
        }

        public void EndCall()
        {
            if (this._state == TerminalState.Engaged)
            {
                Console.WriteLine("Terminal {0} sends end signal to {1}", this.Number, this.Number.Value == this._callArgs.Caller.Value ? this._callArgs.Callee : this._callArgs.Caller);
                OnFinishCall(this, this._callArgs);
            }
        }

        public void ReceiveEndCall(object sender, EventArgs e)
        {
            this._state = TerminalState.On;
            Console.WriteLine("Terminal {0} is off the conversation with {1}", this.Number, this.Number.Value == this._callArgs.Caller.Value ? this._callArgs.Callee : this._callArgs.Caller);
            this._callArgs = null;
        }

        public void ReceiveBill(object sender, EventArgs e)
        {
            Console.WriteLine("You bill for previous period is {0}", (e as TarificationEventArgs).Sum);
        }

        protected virtual void OnTryingToConnect(object sender, EventArgs e)
        {
            var temp = TryingToConnect;
            if (temp != null)
            {
                temp(sender, e);
            }
        }

        protected virtual void OnTryingToDisconnect(object sender, EventArgs e)
        {
            var temp = TryingToDisconnect;
            if (temp != null)
            {
                temp(sender, e);
            }
        }

        protected virtual void OnStartingCall(object sender, EventArgs e)
        {
            var temp = StartingCall;
            if (temp != null)
            {
                temp(sender, e);
            }
        }

        protected virtual void OnFinishCall(object sender, EventArgs e)
        {
            var temp = FinishCall;
            if (temp != null)
            {
                temp(sender, e);
            }
        }
    }
}
