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

        public event EventHandler<EventArgs> TerminalCreation;

        public event EventHandler<EventArgs> TryingToConnect;

        public event EventHandler<EventArgs> StartingCall;
        public event EventHandler<EventArgs> StartedCall;
        public event EventHandler<EventArgs> FinishCall;



        //construstors
        public Terminal()
        {
            OnTerminalCreation(null, null);
        }
        public Terminal(Base baza, int number)
        {
            this.TerminalCreation += baza.CreatePort;
            this._number = new Number(number);
            OnTerminalCreation(null, null);
        }



        public void Connect()
        {
            ConnectingEventArgs args = new ConnectingEventArgs(this._number);
            OnTryingToConnect(this, args);
        }

        public void Disconnect()
        {
        }

        public bool Call(int number)
        {
            CallingEventArgs args = new CallingEventArgs(number) { RequestResult = 0 };
            OnStartingCall(this, args);
            return false;
        }


        protected virtual void OnStartingCall(object sender, EventArgs e)
        {
            var temp = StartingCall;
            if (temp != null)
            {
                temp(sender, e);
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

        protected virtual void OnTerminalCreation(object sender, EventArgs e)
        {
            var temp = TerminalCreation;
            if (temp != null)
            {
                temp(sender, e);
            }
        }






        public void ShowCallingInfo(object sender, EventArgs e)
        {
            if (sender is Terminal && e is CallingEventArgs)
            {
                Console.WriteLine("Calling {0}", (e as CallingEventArgs).Number);
                (e as CallingEventArgs).RequestResult++;
            }
        }
    }
}
