using Newtonsoft.Json;
using TeamCityNotifier.NotificationServer.TeamCity;

namespace TeamCityNotifier.NotificationServer
{
    public class BuildNotification
    {
        [JsonProperty(PropertyName = "eventType")]
        public string EventType { get; set; }

        [JsonProperty(PropertyName = "build")]
        public FullBuild Build { get; set; }

        public BuildNotification(string eventType, FullBuild build)
        {
            EventType = eventType;
            Build = build;
        }
    }
}