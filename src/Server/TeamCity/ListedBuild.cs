using System;
using Newtonsoft.Json;

namespace TeamCityNotifier.NotificationServer.TeamCity
{
    public class ListedBuild : TeamCityEntity
    {
        public int Id { get; set; }

        public string Number { get; set; }

        public int PercentageComplete { get; set; }

        public string Status { get; set; }

        public string BuildTypeId { get; set; }

        public string BranchName { get; set; }

        public bool DefaultBranch { get; set; }

        public string StartDateString { get; set; }

        public string Href { get; set; }

        public string WebUrl { get; set; }

        public DateTimeOffset StartDate
        {
            get { return ParseDate(this.StartDateString); }
        }
    }
}