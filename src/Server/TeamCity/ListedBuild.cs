using System;
using Newtonsoft.Json;

namespace TeamCityNotifier.NotificationServer.TeamCity
{
    public class ListedBuild : TeamCityEntity, IEquatable<ListedBuild>
    {
        public int Id { get; set; }

        public string Number { get; set; }

        public int PercentageComplete { get; set; }

        public string Status { get; set; }

        public string BuildTypeId { get; set; }

        public string BranchName { get; set; }

        public bool DefaultBranch { get; set; }

        [JsonProperty(PropertyName = "startDate")]
        public string StartDateString { get; set; }

        public string Href { get; set; }

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