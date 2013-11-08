using System;
using NUnit.Framework;
using TeamCityNotifier.NotificationServer.TeamCity;

namespace Server.Tests.TeamCity
{
    [TestFixture]
    class TeamCityEntityTests
    {
        [Test]
        public void Parse_team_city_date()
        {
            // Act
            var date = TeamCityEntity.ParseDate("20131107T153712-0200");

            // Assert
            var expectedDate = new DateTimeOffset(2013, 11, 7, 15, 37, 12, TimeSpan.FromHours(-2));
            Assert.That(date, Is.EqualTo(expectedDate));
        }
    }
}