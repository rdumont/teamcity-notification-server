using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using NUnit.Framework;
using TeamCityNotifier.NotificationServer.TeamCity;

namespace Server.Tests.TeamCity
{
    [TestFixture]
    class BuildsPollerTests
    {
        class TriggerBuildChanges
        {
            [Test]
            public void Trigger_build_changes_in_different_conditions()
            {
                // Arrange
                var previousBuilds = new[] {123, 456};
                var currentBuilds = new[] {987, 123, 789};

                var startingIds = new List<int>();
                var updatingIds = new List<int>();
                var stoppingIds = new List<int>();

                var poller = new TestableBuildsPoller();
                poller.BuildStarted += startingIds.Add;
                poller.BuildUpdated += updatingIds.Add;
                poller.BuildFinished += stoppingIds.Add;

                // Act
                poller.PubliclyTriggerBuildChanges(previousBuilds, currentBuilds);

                // Assert
                Assert.That(startingIds, Is.EquivalentTo(new[] {987, 789}));
                Assert.That(updatingIds, Is.EquivalentTo(new[] {123}));
                Assert.That(stoppingIds, Is.EquivalentTo(new[] {456}));
            } 
        }
    }

    public class TestableBuildsPoller : BuildsPoller
    {
        public TestableBuildsPoller() : base(null, TimeSpan.Zero)
        {
        }

        public void PubliclyTriggerBuildChanges(int[] previousBuilds, int[] currentBuilds)
        {
            base.TriggerBuildChanges(previousBuilds, currentBuilds);
        }
    }
}