using Newtonsoft.Json;

namespace TeamCityNotifier.NotificationServer.TeamCity
{
    public class RunningInfo
    {
        [JsonProperty(PropertyName = "percentageComplete")]
        public int PercentageComplete { get; set; }

        [JsonProperty(PropertyName = "elapsedSeconds")]
        public int ElapsedSeconds { get; set; }

        [JsonProperty(PropertyName = "estimatedTotalSeconds")]
        public int EstimatedTotalSeconds { get; set; }

        [JsonProperty(PropertyName = "currentStageText")]
        public string CurrentStageText { get; set; }

        [JsonProperty(PropertyName = "outDated")]
        public bool OutDated { get; set; }

        [JsonProperty(PropertyName = "probablyHanging")]
        public bool ProbablyHanging { get; set; }
    }
}