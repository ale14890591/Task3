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
            //Terminal t = new Terminal();
            //t.StartingCall += t.ShowCallingInfo;
            //t.Call(101);


            Base Operator = new Base();

            Terminal t110 = new Terminal(110); System.Threading.Thread.Sleep(1000);
            Terminal t111 = new Terminal(111); System.Threading.Thread.Sleep(1000);
            Operator.RegisterTerminal(t110,t111);
            t111.ConnectToBase(); System.Threading.Thread.Sleep(1000);
            Terminal t112 = new Terminal(112); System.Threading.Thread.Sleep(1000);
            t112.ConnectToBase(); System.Threading.Thread.Sleep(1000);
            t112.Call(111); System.Threading.Thread.Sleep(1000);
            t110.Call(111); t110.Call(111); t110.Call(111); 
            System.Threading.Thread.Sleep(5000);
            t111.EndCall(); System.Threading.Thread.Sleep(1000);
            t111.Call(112); 
            System.Threading.Thread.Sleep(5000);
            t111.EndCall(); System.Threading.Thread.Sleep(1000);
            t112.Call(100);
            

            Console.ReadKey();
        }
    }
}
