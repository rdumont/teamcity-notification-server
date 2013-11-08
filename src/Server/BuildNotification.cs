using Newtonsoft.Json;
using TeamCityNotifier.NotificationServer.TeamCity;

namespace TeamCityNotifier.NotificationServer
{
    public class BuildNotification
    {
        [JsonProperty(PropertyName = "eventType")]
        public string EventType { get; set; }

        [JsonProperty(PropertyName = "build")]
        public ListedBuild Build { get; set; }

        public BuildNotification(string eventType, ListedBuild build)
        {
            EventType = eventType;
            Build = build;
        }
    }
}