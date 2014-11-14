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
        private List<Contract> _contractList = new List<Contract>();
        private List<Tariff> _tariffs = new List<Tariff>();

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
        public List<Contract> ContractList
        {
            get { return _contractList; }
            set { _contractList = value; }
        }
        public List<Tariff> Tariffs
        {
            get { return _tariffs; }
            set { _tariffs = value; }
        }
        

        public void CreatePort(Number number)
        {
            this._workplace.Add(new Port(number));
        }

        public void RegisterTerminal(Terminal t, string name)
        {
            this.ContractList.Add(new Contract(name, t.Number));
            CreatePort(t.Number);
            t.TryingToConnect += ConnectTerminal;
            t.TryingToDisconnect += DisconnectTerminal;
            t.StartingCall += Call;
            t.FinishCall += EndCall;
            t.Register();
            t.SetTariffEvent += SetTariff;
            Console.WriteLine("Terminal {0} has been registrated", t.Number);
        }

        public void SetTariff(object sender, EventArgs e)
        {
            Contract temp = null;
            temp = this._contractList.Find(x => x.Number.Value == (sender as Terminal).Number.Value);
            if (temp != null)
            {
                if (temp.TariffPeriodList.Count == 0)
                {
                    temp.TariffPeriodList.Add(new TariffPeriod((e as SetTariffEventArgs).Tariff, (e as SetTariffEventArgs).StartPeriod));
                }
                else
                {
                    temp.TariffPeriodList.Add(new TariffPeriod((e as SetTariffEventArgs).Tariff, (e as SetTariffEventArgs).StartPeriod));
                    temp.TariffPeriodList[temp.TariffPeriodList.Count - 2].End = (e as SetTariffEventArgs).StartPeriod;
                }
            }
        }

        public void ConnectTerminal(object sender, EventArgs e)
        {
            Port temp = null;
            temp = this._workplace.Find(x => x.ConnectedTerminalNumber.Value == (sender as Terminal).Number.Value);
            if (temp != null)
            {
                temp.ConnectToTerminal();
                (e as ConnectingEventArgs).OperationSuccess = true;
                temp.SendCallToTerminal += (sender as Terminal).IncomingCall;
                temp.SendEndCallToTerminal += (sender as Terminal).ReceiveEndCall;
            }
        }

        public void DisconnectTerminal(object sender, EventArgs e)
        {
            Port temp = null;
            temp = this._workplace.Find(x => x.ConnectedTerminalNumber.Value == (e as ConnectingEventArgs).Number.Value);
            if (temp != null)
            {
                temp.DisconnectFromTerminal();
                (e as ConnectingEventArgs).OperationSuccess = true;
            }
        }

        public void Call(object sender, EventArgs e)
        {
            Port caller = this._workplace.Find(x => x.ConnectedTerminalNumber.Value == (sender as Terminal).Number.Value);
            if (caller.PortState == PortState.Connected)
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
            else
            {
                (e as CallingEventArgs).RequestResult = RequestResult.CallerPortIsDisconnected;
            }
        }

        public void EndCall(object sender, EventArgs e)
        {
            Port caller = this._workplace.Find(x => x.ConnectedTerminalNumber.Value == (e as CallingEventArgs).Caller.Value);
            Port callee = this._workplace.Find(x => x.ConnectedTerminalNumber.Value == (e as CallingEventArgs).Callee.Value);

            //(e as CallingEventArgs).End = DateTime.Now;
            TarificationByCall(e as CallingEventArgs);
            this._registry.Add(sender, e);
            Console.WriteLine("Conversation between {0} and {1} has been finished at {2}, duration {3}", (e as CallingEventArgs).Caller, (e as CallingEventArgs).Callee, (e as CallingEventArgs).End, (e as CallingEventArgs).End - (e as CallingEventArgs).Beg);
            caller.EndCall(sender, e);
            callee.EndCall(sender, e);
        }

        //public void TarificationByDate(DateTime d)
        //{
        //    IEnumerable<Contract> temp = null;
        //    temp = this._contractList.Where(x => x.TariffPeriodList[x.TariffPeriodList.Count - 1].Beg.Day == d.Day).
        //        Where(x => x.TariffPeriodList[x.TariffPeriodList.Count - 1].Beg.Year != d.Year || x.TariffPeriodList[x.TariffPeriodList.Count - 1].Beg.Month != d.Month); //.Where(x => x.TariffPeriodList[x.TariffPeriodList.Count - 1].Beg.Year != d.Year);
        //    foreach (Contract c in temp)
        //    {
        //        IEnumerable<int> selection = this.Registry.
        //            Where(x => 
        //                x.Caller.Value == c.Number.Value && 
        //                x.Beg >= d.AddMonths(-1)
        //                ).Select(y => (y.End - y.Beg).Minutes + 1);
        //        int sum = selection.Sum();
        //        int debt = CountDebt(c.TariffPeriodList[c.TariffPeriodList.Count - 1].Tariff, sum);
        //        Console.WriteLine("{0} {1} {2}", c.Number.Value, d, debt);
        //    }
        //}

        public void TarificationByDate(DateTime d)
        {
            IEnumerable<Contract> temp = null;
            temp = this._contractList.Where(x => x.TariffPeriodList[x.TariffPeriodList.Count - 1].Beg.Day == d.Day).
                Where(x => x.TariffPeriodList[x.TariffPeriodList.Count - 1].Beg.Year != d.Year || x.TariffPeriodList[x.TariffPeriodList.Count - 1].Beg.Month != d.Month);
            foreach (Contract c in temp)
            {
                int sum = this._registry.Where(x => x.Caller.Value == c.Number.Value && x.Beg >= d.AddMonths(-1)).Sum(y => y.Cost);
                int debt = sum + c.TariffPeriodList[c.TariffPeriodList.Count - 1].Tariff.PeriodFee;
                Console.WriteLine("{0} {1} {2}", c.Number.Value, d, debt);
                c.TariffPeriodList[c.TariffPeriodList.Count - 1].Tariff.FreeIcludedIntervals = 0;
                c.TariffPeriodList[c.TariffPeriodList.Count - 1].Tariff.FreeIcludedIntervals = 
                    this.Tariffs.Find(x => x.Name == c.TariffPeriodList[c.TariffPeriodList.Count - 1].Tariff.Name).FreeIcludedIntervals;
            }
        }

        public void TarificationByCall(CallingEventArgs e)
        {
            Contract temp = this._contractList.Find(x => x.Number.Value == e.Caller.Value);
            if (temp.TariffPeriodList[temp.TariffPeriodList.Count - 1].Tariff.FreeIcludedIntervals > 0)
            {
                temp.TariffPeriodList[temp.TariffPeriodList.Count - 1].Tariff.FreeIcludedIntervals -= ((e.End - e.Beg).Minutes + 1);
                e.Cost = 0;
            }
            else
            {
                e.Cost = ((e.End - e.Beg).Minutes + 1) * temp.TariffPeriodList[temp.TariffPeriodList.Count - 1].Tariff.CostPerInterval;
            }
        }

        public static int CountDebt(Tariff t, int sum)
        {
            int paidDuration = 0;
            if (sum >= t.FreeIcludedIntervals)
                paidDuration = sum - t.FreeIcludedIntervals;
            return t.CostPerInterval * paidDuration + t.PeriodFee;
        }
    }
}
