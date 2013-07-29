using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Subsonic.Client.Windows;
using Subsonic.Common;

namespace ApiTester
{
    class Program
    {
        static void Main(string[] args)
        {
            var test = new Client("http://archrivalmini.archienet.local:8080/subsonic", "Josh", "This Is My Password!", "ApiTester");

            while (true)
            {
                Thread.Sleep(1000);
            }
        }
    }
}
