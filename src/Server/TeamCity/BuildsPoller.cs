﻿using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TeamCityNotifier.NotificationServer.TeamCity
{
    public class BuildsPoller
    {
        private readonly TimeSpan _interval;
        private const string RunningBuildsPath = "httpAuth/app/rest/builds?locator=running:true";

        private readonly HttpClient _client;

        public event Action<Build> BuildStarted;
        public event Action<Build> BuildUpdated;
        public event Action<Build> BuildStopped;

        public BuildsPoller(HttpClient httpClient, TimeSpan interval)
        {
            _client = httpClient;
            _interval = interval;
        }

        public BuildsPoller(string serverUrl, string username, string password, TimeSpan interval)
            : this(CreateHttpClient(serverUrl, username, password), interval)
        {
        }

        public async Task StartAsync()
        {
            var previousBuilds = new Build[0];
            while (true)
            {
                var currentBuilds = await GetRunningBuildsAsync();
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

        protected virtual async Task<Build[]> GetRunningBuildsAsync()
        {
            var response = await _client.GetAsync(RunningBuildsPath);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new HttpRequestException("Status code was " + response.StatusCode);
            }

            var builds = await response.Content.ReadAsAsync<RunningBuilds>();
            return builds != null ? builds.Builds.ToArray() : new Build[0];
        }

        protected static HttpClient CreateHttpClient(string serverUrl, string username, string password)
        {
            var client = new HttpClient { BaseAddress = new Uri(serverUrl) };
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (username != null && password != null)
            {
                var authenticationBytes = Encoding.UTF8.GetBytes(username + ":" + password);
                var authentication = Convert.ToBase64String(authenticationBytes);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authentication);
            }

            return client;
        }
    }
}