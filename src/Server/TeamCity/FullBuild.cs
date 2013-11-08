using System;
using Newtonsoft.Json;

namespace TeamCityNotifier.NotificationServer.TeamCity
{
    public class FullBuild : TeamCityEntity
    {
        public int Id { get; set; }

        public string Number { get; set; }

        public string Status { get; set; }

        public string Href { get; set; }

        public string WebUrl { get; set; }

        public string BranchName { get; set; }

        public bool DefaultBranch { get; set; }

        public bool Personal { get; set; }

        public bool Pinned { get; set; }

        public bool Running { get; set; }

        public string StatusText { get; set; }

        public BuildType BuildType { get; set; }

        [JsonProperty(PropertyName = "running-info")]
        public RunningInfo RunningInfo { get; set; }

        [JsonProperty(PropertyName = "startDate")]
        public string StartDateString { get; set; }

        [JsonProperty(PropertyName = "finishDate")]
        public string FinishDateString { get; set; }

        [JsonProperty(PropertyName = "isoStartDate")]
        public DateTimeOffset StartDate
        {
            get { return ParseDate(this.StartDateString); }
        }

        [JsonProperty(PropertyName = "isoFinishDate")]
        public DateTimeOffset FinishDate
        {
            get { return ParseDate(this.FinishDateString); }
        }
    }
}