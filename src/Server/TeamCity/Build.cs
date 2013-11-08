using System;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace TeamCityNotifier.NotificationServer.TeamCity
{
    public class Build : IEquatable<Build>
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "number")]
        public string Number { get; set; }

        [JsonProperty(PropertyName = "percentageComplete")]
        public int PercentageComplete { get; set; }

        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }

        [JsonProperty(PropertyName = "buildTypeId")]
        public string BuildTypeId { get; set; }

        [JsonProperty(PropertyName = "branchName")]
        public string BranchName { get; set; }

        [JsonProperty(PropertyName = "defaultBranch")]
        public bool DefaultBranch { get; set; }

        [JsonProperty(PropertyName = "startDate")]
        public string StartDateString { get; set; }

        [JsonProperty(PropertyName = "href")]
        public string Href { get; set; }

        [JsonProperty(PropertyName = "webUrl")]
        public string WebUrl { get; set; }

        [JsonProperty(PropertyName = "isoStartDate")]
        public DateTimeOffset StartDate
        {
            get
            {
                var match = Regex.Match(this.StartDateString,
                    @"(\d{4})(\d{2})(\d{2})T(\d{2})(\d{2})(\d{2})([+-])(\d{2})(\d{2})");
                var groups = match.Groups;
                var isoString = string.Format("{0}-{1}-{2}T{3}:{4}:{5}{6}{7}:{8}", groups[1], groups[2], groups[3],
                    groups[4], groups[5], groups[6], groups[7], groups[8], groups[9]);
                return DateTimeOffset.Parse(isoString);
            }
        }

        public bool Equals(Build other)
        {
            if (ReferenceEquals(null, other)) return false;
            return this.Id.Equals(other.Id);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return -1521134295*this.Id;
            }
        }

        public static bool operator ==(Build leftOperand, Build rightOperand)
        {
            if (ReferenceEquals(null, leftOperand)) return ReferenceEquals(null, rightOperand);
            return leftOperand.Equals(rightOperand);
        }

        public static bool operator !=(Build leftOperand, Build rightOperand)
        {
            return !(leftOperand == rightOperand);
        }
    }
}