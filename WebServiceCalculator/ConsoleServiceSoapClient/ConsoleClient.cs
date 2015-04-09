using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleServiceSoapClient.ServiceReference;

namespace ConsoleServiceSoapClient
{
    class ConsoleClient
    {
        static void Main()
        {
            var  client = new ServiceCalculatorClient();

            var startPoint = new Point() {X = 7, Y = 9};
            var endPoint = new Point() { X = -7, Y = -9 };
            var distance = client.CalculateDistance(startPoint, endPoint);
            Console.WriteLine("distance = " + distance);
        }
    }
}
