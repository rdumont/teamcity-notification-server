using System;

namespace TeamCityNotifier.NotificationServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var settings = ServerSettings.ReadFrom("config.yaml");

            Console.WriteLine("Port: {0}", settings.Port);
        }
    }
}
