using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    class Program
    {
        static void Main(string[] args)
        {
            Base Operator = new Base();
            Tariff fee0perInt10inclFree0 = new Tariff("fee0perInt10inclFree0", 0, 10, 0);
            Tariff fee50perInt5inclFree60 = new Tariff("fee50perInt5inclFree60", 50, 5, 60);
            Tariff fee100perInt2inclFree200 = new Tariff("fee100perInt2inclFree200", 100, 2, 200);
            Operator.Tariffs.Add(fee0perInt10inclFree0);
            Operator.Tariffs.Add(fee50perInt5inclFree60);
            Operator.Tariffs.Add(fee100perInt2inclFree200);

            Terminal t110 = new Terminal(110);
            Terminal t111 = new Terminal(111);

            
            for (DateTime d = new DateTime(2000, 01, 01, 00, 00, 00); d <= new DateTime(2000,05,03); d = d.AddDays(1))
            {
                Console.WriteLine(d);
                if (d == new DateTime(2000, 01, 01))
                {
                    Operator.RegisterTerminal(t110, "Alex");
                    t110.SetTariff(fee0perInt10inclFree0, d);
                    t110.ConnectToBase();
                }
                if (d == new DateTime(2000, 01, 03))
                {
                    Operator.RegisterTerminal(t111, "Alex");
                    t111.SetTariff(fee50perInt5inclFree60, d);
                    t111.ConnectToBase();
                }

                DateTime dbeg = d.AddHours(10);
                DateTime dend = dbeg.AddSeconds(45);
                t110.Call(111, dbeg);
                t111.EndCall(dend);
                t111.Call(110, dbeg.AddHours(1));
                t110.EndCall(dend.AddHours(1).AddMinutes(10));

                Operator.TarificationByDate(d);
            }
            
            
            
            
            
            //Terminal t112 = new Terminal(112);
            //t112.ConnectToBase();
            //t112.Call(111);

            //t110.Call(111, new DateTime(2000, 01, 01, 12, 00, 00));
            //t111.EndCall(new DateTime(2000, 01, 01, 12, 10, 00));
            //t111.Call(110, new DateTime(2000, 01, 01, 18, 00, 00));
            //t111.EndCall(new DateTime(2000, 01, 01, 18, 30, 00));
            //t111.Call(110, new DateTime(2000, 01, 01, 20, 00, 00));
            //t111.EndCall(new DateTime(2000, 01, 01, 20, 35, 00));
            //t110.Call(111, new DateTime(2000, 01, 01, 21, 00, 00));
            //t110.EndCall(new DateTime(2000, 01, 01, 21, 15, 00));

            //Operator.TarificateNumber(110);
            //Operator.TarificateNumber(111);
            //Operator.Workplace.Where(x => x.StartPeriod);
            //t111.Call(112); 
            //t111.EndCall(); System.Threading.Thread.Sleep(1000);
            
            Operator.ShowDetailedReport("Alex");
            Console.ReadKey();
        }
    }
}
