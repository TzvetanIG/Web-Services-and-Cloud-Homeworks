using System;
using System.Net;
using RestSharp;

namespace DistanceCalculatorRESTClient
{

    class ConsoleClient
    {
        static void Main()
        {
            // Your port can be different
            var client = new RestClient("http://localhost:3420");

            var request = new RestRequest("api/distance", Method.POST);
            request.AddParameter("Point1.X", 1);
            request.AddParameter("Point1.Y", 1);
            request.AddParameter("Point2.X", 2);
            request.AddParameter("Point2.Y", 2);

            var response = client.Execute(request);
            var statusCode = response.StatusCode;

            if (statusCode == 0 )
            {
                Console.WriteLine("Server is not started.");
                Console.WriteLine("Your port can be different.");
                Console.WriteLine("Please run project \"3. DistanceCalculatorRESTService\".");
                return;
            } 

            var result = response.Content;
            Console.WriteLine("distance = " + result);

        }
    }
}
