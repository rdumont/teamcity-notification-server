using System;
using Microsoft.Owin.Hosting;

namespace TeamCityNotifier.NotificationServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var settings = ServerSettings.ReadFrom("config.yaml");

            Console.WriteLine("Port: {0}", settings.Port);

            var url = "http://localhost:" + settings.Port;
            using (WebApp.Start<Startup>(url))
            {
                Console.ReadLine();
            }
        }
    }
}
