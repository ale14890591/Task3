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

            Terminal t111 = new Terminal(Operator, 111);
            t111.Connect();
            //t111.Disconnect();
            Terminal t112 = new Terminal(Operator, 112);
            t112.Connect();
            t112.Call(111);

            //Console.ReadKey();
        }
    }
}
