using System;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin.Hosting;

namespace TeamCityNotifier.NotificationServer
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync(args).Wait();
        }

        static async Task MainAsync(string[] args)
        {
            var settings = ServerSettings.ReadFrom("config.yaml");

            Console.WriteLine("Port: {0}", settings.Port);

            var url = "http://localhost:" + settings.Port;
            using (WebApp.Start<Startup>(url))
            {
                var teamCityClient = new TeamCity.RestApiClient(settings.TeamCity.Url, settings.TeamCity.Username,
                    settings.TeamCity.Password);
                var poller = new TeamCity.BuildsPoller(teamCityClient, TimeSpan.FromSeconds(1));

                var connectionGroups = GlobalHost.ConnectionManager.GetConnectionContext<BuildsConnection>().Groups;

                poller.BuildStarted += build =>
                {
                    connectionGroups.Send(build.BuildTypeId, new BuildNotification("start", build));
                    Console.WriteLine("Build started: {0}, {1}%", build.BuildTypeId, build.PercentageComplete);
                };

                poller.BuildUpdated += build =>
                {
                    connectionGroups.Send(build.BuildTypeId, new BuildNotification("update", build));
                    Console.WriteLine("Build updated: {0}, {1}%", build.BuildTypeId, build.PercentageComplete);
                };

                poller.BuildStopped += build =>
                {
                    connectionGroups.Send(build.BuildTypeId, new BuildNotification("stop", build));
                    Console.WriteLine("Build stopped: {0}", build.BuildTypeId);
                };

                await poller.StartAsync();
            }
        }
    }
}
