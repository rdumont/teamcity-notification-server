using System.Collections.Generic;
using Newtonsoft.Json;

namespace TeamCityNotifier.NotificationServer.TeamCity
{
    public class BuildsCollection
    {
        [JsonProperty(PropertyName = "count")]
        public int Count { get; set; }

        [JsonProperty(PropertyName = "build")]
        public IList<ListedBuild> Builds { get; set; }

        public BuildsCollection()
        {
            this.Builds = new List<ListedBuild>();
        }
    }
}