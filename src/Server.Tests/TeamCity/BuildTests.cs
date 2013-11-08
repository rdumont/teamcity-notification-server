using System;
using NUnit.Framework;
using TeamCityNotifier.NotificationServer.TeamCity;

namespace Server.Tests.TeamCity
{
    [TestFixture]
    class BuildTests
    {
        [Test]
        public void Should_read_start_date()
        {
            // Arrange
            var build = new Build {StartDateString = "20131107T153712-0200"};

            // Act
            var startDate = build.StartDate;

            // Assert
            var expectedDate = new DateTimeOffset(2013, 11, 7, 15, 37, 12, TimeSpan.FromHours(-2));
            Assert.That(startDate, Is.EqualTo(expectedDate));
        }

        [Test]
        public void Should_equate_correctly()
        {
            // Arrange
            var build1 = new Build {Id = 123};
            var build2 = new Build {Id = 123};

            // Act
            var equals = build1 == build2;

            // Assert
            Assert.That(equals, Is.True);
        }
    }
}
