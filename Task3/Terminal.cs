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
        private CallingEventArgs _callArgs = null;

        public Number Number
        {
            get { return _number; }
        }

        public event EventHandler<EventArgs> TryingToConnect;
        public event EventHandler<EventArgs> TryingToDisconnect;
        public event EventHandler<EventArgs> StartingCall;
        public event EventHandler<EventArgs> StartedCall;
        public event EventHandler<EventArgs> FinishCall;



        //construstors
        public Terminal(Base b, int number)
        {
            this._number = new Number(number);
            this._state = TerminalState.Off;
            b.CreatePort(this._number);
            this.TryingToConnect += b.ConnectToPort;
            this.TryingToDisconnect += b.Disconnect;
            this.StartingCall += b.Call;
            
        }





        public bool Connect()
        {
            ConnectingEventArgs args = new ConnectingEventArgs(this._number);
            OnTryingToConnect(this, args);
            if (args.OperationSuccess)
            {
                this._state = TerminalState.On;
            }
            return args.OperationSuccess;
        }

        public bool Disconnect()
        {
            ConnectingEventArgs args = new ConnectingEventArgs(this._number);
            OnTryingToDisconnect(this, args);
            if (args.OperationSuccess)
            {
                this._state = TerminalState.Off;
            }
            return args.OperationSuccess;
        }

        public void Call(int number)
        {
            this._state = TerminalState.Engaged;
            //CallingEventArgs args = new CallingEventArgs(number);
            this._callArgs = new CallingEventArgs(number);
            OnStartingCall(this, this._callArgs);
            if (this._callArgs.RequestResult == RequestResult.ConnectionSuccess)
            {
                this._callArgs.Beg = DateTime.Now;
                OnStartedCall(this, this._callArgs);
            }
            else
            {
                this._state = TerminalState.On;
            }
        }

        public void IncomingCall(object sender, EventArgs e)
        {
            this._state = TerminalState.Engaged;
            bool answer = true;
            if (answer)
            {
                (e as CallingEventArgs).RequestResult = RequestResult.ConnectionSuccess;
                this._state = TerminalState.On;
                this._callArgs = e as CallingEventArgs;
            }
        }

        public void EndCall()
        {
            if (this._state == TerminalState.Engaged)
            {
                OnFinishCall(this, this._callArgs);
            }
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

        protected virtual void OnStartedCall(object sender, EventArgs e)
        {
            var temp = StartedCall;
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

        public void ShowCallingInfo(object sender, EventArgs e)
        {
            if (sender is Terminal && e is CallingEventArgs)
            {
                Console.WriteLine("Calling {0}", (e as CallingEventArgs).DestinationNumber);
                (e as CallingEventArgs).RequestResult++;
            }
        }
    }
}
