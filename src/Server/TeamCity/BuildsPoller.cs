﻿using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TeamCityNotifier.NotificationServer.TeamCity
{
    public class BuildsPoller
    {
        private readonly TimeSpan _interval;

        private readonly RestApiClient _client;

        public event Action<Build> BuildStarted;
        public event Action<Build> BuildUpdated;
        public event Action<Build> BuildStopped;

        public BuildsPoller(RestApiClient client, TimeSpan interval)
        {
            _client = client;
            _interval = interval;
        }

        public async Task StartAsync()
        {
            var previousBuilds = new Build[0];
            while (true)
            {
                var currentBuilds = await _client.GetRunningBuildsAsync();
                TriggerBuildChanges(previousBuilds, currentBuilds);
                previousBuilds = currentBuilds;
                
                Thread.Sleep(_interval);
            }
        }

        protected void TriggerBuildChanges(Build[] previousBuilds, Build[] currentBuilds)
        {
            var startedBuilds = currentBuilds.Except(previousBuilds).ToArray();
            foreach (var build in startedBuilds)
                this.BuildStarted(build);

            var updatedBuilds = currentBuilds.Except(startedBuilds);
            foreach (var build in updatedBuilds)
                this.BuildUpdated(build);

            var stoppedBuilds = previousBuilds.Except(currentBuilds);
            foreach (var build in stoppedBuilds)
                this.BuildStopped(build);
        }
    }
}