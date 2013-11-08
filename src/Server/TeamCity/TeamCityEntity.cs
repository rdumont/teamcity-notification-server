using System;
using System.Text.RegularExpressions;

namespace TeamCityNotifier.NotificationServer.TeamCity
{
    public abstract class TeamCityEntity
    {
        public static DateTimeOffset ParseDate(string dateString)
        {
            if (string.IsNullOrWhiteSpace(dateString))
                return default(DateTimeOffset);

            var match = Regex.Match(dateString,
                @"(\d{4})(\d{2})(\d{2})T(\d{2})(\d{2})(\d{2})([+-])(\d{2})(\d{2})");
            var groups = match.Groups;
            var isoString = string.Format("{0}-{1}-{2}T{3}:{4}:{5}{6}{7}:{8}", groups[1], groups[2], groups[3],
                groups[4], groups[5], groups[6], groups[7], groups[8], groups[9]);
            return DateTimeOffset.Parse(isoString);
        }
    }
}
