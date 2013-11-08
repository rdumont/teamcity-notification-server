using System;
using Newtonsoft.Json;

namespace TeamCityNotifier.NotificationServer.TeamCity
{
    public class ListedBuild : TeamCityEntity, IEquatable<ListedBuild>
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
            get { return ParseDate(this.StartDateString); }
        }

        public bool Equals(ListedBuild other)
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

        public static bool operator ==(ListedBuild leftOperand, ListedBuild rightOperand)
        {
            if (ReferenceEquals(null, leftOperand)) return ReferenceEquals(null, rightOperand);
            return leftOperand.Equals(rightOperand);
        }

        public static bool operator !=(ListedBuild leftOperand, ListedBuild rightOperand)
        {
            return !(leftOperand == rightOperand);
        }
    }
}