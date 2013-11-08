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

                poller.BuildStarted += async id =>
                {
                    var build = await teamCityClient.GetBuildAsync(id);
                    await connectionGroups.Send(build.BuildType.Id, new BuildNotification("start", build));
                    Console.WriteLine("{0}: started", build.BuildType.Id);
                };

                poller.BuildUpdated += async id =>
                {
                    var build = await teamCityClient.GetBuildAsync(id);
                    await connectionGroups.Send(build.BuildType.Id, new BuildNotification("update", build));
                    var percentage = build.RunningInfo == null ? 100 : build.RunningInfo.PercentageComplete;
                    Console.WriteLine("{0}: {1}%", build.BuildType.Id, percentage);
                };

                poller.BuildFinished += async id =>
                {
                    var build = await teamCityClient.GetBuildAsync(id);
                    await connectionGroups.Send(build.BuildType.Id, new BuildNotification("finish", build));
                    Console.WriteLine("{0}: finished with {1} - {2}", build.BuildType.Id, build.Status, build.StatusText);
                };

                await poller.StartAsync();
            }
        }
    }
}
