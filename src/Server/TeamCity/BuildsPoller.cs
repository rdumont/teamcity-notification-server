﻿using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TeamCityNotifier.NotificationServer.TeamCity
{
    public class BuildsPoller
    {
        private readonly TimeSpan _interval;

        private readonly RestApiClient _client;

        public event Action<int> BuildStarted;
        public event Action<int> BuildUpdated;
        public event Action<int> BuildFinished;

        public BuildsPoller(RestApiClient client, TimeSpan interval)
        {
            _client = client;
            _interval = interval;
        }

        public async Task StartAsync()
        {
            var previousBuilds = new int[0];
            while (true)
            {
                try
                {
                    var currentBuildObjects = await _client.GetRunningBuildsAsync();
                    var currentBuilds = currentBuildObjects.Select(build => build.Id).ToArray();
                    TriggerBuildChanges(previousBuilds, currentBuilds);
                    previousBuilds = currentBuilds;
                }
                catch (Exception exception)
                {
                    if (!(exception is HttpRequestException))
                        throw;

                    var builder = new StringBuilder();
                    builder.AppendFormat("{0}: {1}", exception.GetType().Name, exception.Message);

                    var inner = exception.InnerException;
                    while (inner != null)
                    {
                        builder.AppendFormat(" ---> {0}: {1}", inner.GetType().Name, inner.Message);
                        inner = inner.InnerException;
                    }
                    builder.AppendLine();
                    Console.WriteLine(builder);
                }

                Thread.Sleep(_interval);
            }
        }

        protected void TriggerBuildChanges(int[] previousBuilds, int[] currentBuilds)
        {
            var startedBuilds = currentBuilds.Except(previousBuilds).ToArray();
            foreach (var build in startedBuilds)
                this.BuildStarted(build);

            var updatedBuilds = currentBuilds.Except(startedBuilds);
            foreach (var build in updatedBuilds)
                this.BuildUpdated(build);

            var finishedBuilds = previousBuilds.Except(currentBuilds);
            foreach (var build in finishedBuilds)
                this.BuildFinished(build);
        }
    }
}