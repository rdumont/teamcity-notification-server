using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace TeamCityNotifier.NotificationServer.TeamCity
{
    public class RestApiClient
    {
        private const string RunningBuildsPath = "httpAuth/app/rest/builds?locator=running:true";

        private readonly HttpClient _httpClient;

        public RestApiClient(string serverUrl, string username, string password)
            : this(CreateHttpClient(serverUrl, username, password))
        {
        }

        public RestApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public virtual async Task<ListedBuild[]> GetRunningBuildsAsync()
        {
            var response = await _httpClient.GetAsync(RunningBuildsPath);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new HttpRequestException("Status code was " + response.StatusCode);
            }

            var builds = await response.Content.ReadAsAsync<BuildsCollection>();
            return builds != null ? builds.Builds.ToArray() : new ListedBuild[0];
        }

        public static HttpClient CreateHttpClient(string serverUrl, string username, string password)
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
