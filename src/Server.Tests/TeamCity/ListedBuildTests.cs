using NUnit.Framework;
using TeamCityNotifier.NotificationServer.TeamCity;

namespace Server.Tests.TeamCity
{
    [TestFixture]
    class ListedBuildTests
    {
        [Test]
        public void Should_equate_correctly()
        {
            // Arrange
            var build1 = new ListedBuild {Id = 123};
            var build2 = new ListedBuild {Id = 123};

            // Act
            var equals = build1 == build2;

            // Assert
            Assert.That(equals, Is.True);
        }
    }
}
