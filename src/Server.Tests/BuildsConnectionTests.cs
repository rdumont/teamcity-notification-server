using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Moq;
using NUnit.Framework;
using TeamCityNotifier.NotificationServer;

namespace Server.Tests
{
    [TestFixture]
    class BuildsConnectionTests
    {
        [Test]
        public async Task Receiving_one_build_type_id_should_add_connection_to_group()
        {
            // Arrange
            var connection = new TestableBuildsConnection();

            // Act
            await connection.PubliclyOnReceived(Mock.Of<IRequest>(), "abc123", "some_build");

            // Assert
            Assert.That(connection.AddedToGroups, Has.Count.EqualTo(1));
            Assert.That(connection.AddedToGroups[0].Item1, Is.EqualTo("abc123"));
            Assert.That(connection.AddedToGroups[0].Item2, Is.EqualTo("some_build"));
        }

        [Test]
        public async Task Receiving_many_build_type_ids_should_add_connection_to_groups()
        {
            // Arrange
            var connection = new TestableBuildsConnection();

            // Act
            await connection.PubliclyOnReceived(Mock.Of<IRequest>(), "abc123", "some_build,another_one,the_last");

            // Assert
            Assert.That(connection.AddedToGroups, Has.Count.EqualTo(3));
            Assert.That(connection.AddedToGroups[0].Item1, Is.EqualTo("abc123"));
            Assert.That(connection.AddedToGroups[0].Item2, Is.EqualTo("some_build"));
            Assert.That(connection.AddedToGroups[1].Item1, Is.EqualTo("abc123"));
            Assert.That(connection.AddedToGroups[1].Item2, Is.EqualTo("another_one"));
            Assert.That(connection.AddedToGroups[2].Item1, Is.EqualTo("abc123"));
            Assert.That(connection.AddedToGroups[2].Item2, Is.EqualTo("the_last"));
        }
    }

    public class TestableBuildsConnection : BuildsConnection
    {
        public List<Tuple<string, string>> AddedToGroups { get; set; }

        public TestableBuildsConnection()
        {
            this.AddedToGroups = new List<Tuple<string, string>>();
        }

        protected override Task AddToGroups(string connectionId, string buildTypeId)
        {
            var value = new Tuple<string, string>(connectionId, buildTypeId);
            this.AddedToGroups.Add(value);
            return Task.Delay(0);
        }

        public Task PubliclyOnReceived(IRequest request, string connectionId, string buildTypeIds)
        {
            return base.OnReceived(request, connectionId, buildTypeIds);
        }
    }
}