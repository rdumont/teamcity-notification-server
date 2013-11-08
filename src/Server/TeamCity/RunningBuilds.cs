using System.Collections.Generic;
using Newtonsoft.Json;

namespace TeamCityNotifier.NotificationServer.TeamCity
{
    public class RunningBuilds
    {
        [JsonProperty(PropertyName = "count")]
        public int Count { get; set; }

        [JsonProperty(PropertyName = "build")]
        public IList<Build> Builds { get; set; }

        public RunningBuilds()
        {
            this.Builds = new List<Build>();
        }
    }
}