using System;
using Newtonsoft.Json;

namespace TeamCityNotifier.NotificationServer.TeamCity
{
    public class FullBuild : TeamCityEntity
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "number")]
        public string Number { get; set; }

        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "href")]
        public string Href { get; set; }

        [JsonProperty(PropertyName = "webUrl")]
        public string WebUrl { get; set; }

        [JsonProperty(PropertyName = "branchName")]
        public string BranchName { get; set; }

        [JsonProperty(PropertyName = "defaultBranch")]
        public bool DefaultBranch { get; set; }

        [JsonProperty(PropertyName = "personal")]
        public bool Personal { get; set; }

        [JsonProperty(PropertyName = "pinned")]
        public bool Pinned { get; set; }

        [JsonProperty(PropertyName = "running")]
        public bool Running { get; set; }

        [JsonProperty(PropertyName = "statusText")]
        public string StatusText { get; set; }

        [JsonProperty(PropertyName = "buildType")]
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