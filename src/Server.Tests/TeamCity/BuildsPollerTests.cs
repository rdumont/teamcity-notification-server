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
                var previousBuilds = new[]
                {
                    new Build {Id = 123},
                    new Build {Id = 456}
                };
                var currentBuilds = new[]
                {
                    new Build {Id = 987},
                    new Build {Id = 123},
                    new Build {Id = 789}
                };

                var startingIds = new List<int>();
                var updatingIds = new List<int>();
                var stoppingIds = new List<int>();

                var poller = new TestableBuildsPoller();
                poller.BuildStarted += build => startingIds.Add(build.Id);
                poller.BuildUpdated += build => updatingIds.Add(build.Id);
                poller.BuildStopped += build => stoppingIds.Add(build.Id);

                // Act
                poller.PubliclyTriggerBuildChanges(previousBuilds, currentBuilds);

                // Assert
                Assert.That(startingIds, Is.EquivalentTo(new[] {987, 789}));
                Assert.That(updatingIds, Is.EquivalentTo(new[] {123}));
                Assert.That(stoppingIds, Is.EquivalentTo(new[] {456}));
            } 
        }

        class CreateHttpClient
        {
            [Test]
            public void Create_client_with_base_addres_and_accepting_JSON_media_type()
            {
                // Act
                var client = TestableBuildsPoller.PubliclyCreateHttpClient("http://localhost/", null, null);

                // Assert
                Assert.That(client.BaseAddress, Is.EqualTo(new Uri("http://localhost/")));
                Assert.That(client.DefaultRequestHeaders.Accept.Any(), Is.Not.Null);
                Assert.That(client.DefaultRequestHeaders.Accept.First().MediaType, Is.EqualTo("application/json"));
            }

            [TestCase((string)null, "yada")]
            [TestCase("bla", (string)null)]
            [TestCase((string) null, (string)null)]
            public void Should_not_set_authorization_header_for_null_user_or_password(string username, string password)
            {
                // Act
                var client = TestableBuildsPoller.PubliclyCreateHttpClient("http://localhost/", username, password);

                // Assert
                Assert.That(client.DefaultRequestHeaders.Authorization, Is.Null);
            }

            [Test]
            public void Should_set_basic_authorization_header()
            {
                // Act
                var client = TestableBuildsPoller.PubliclyCreateHttpClient("http://localhost/", "foo", "bar");

                // Assert
                var headers = client.DefaultRequestHeaders;
                Assert.That(headers.Authorization, Is.Not.Null);
                Assert.That(headers.Authorization.Scheme, Is.EqualTo("Basic"));

                var token = headers.Authorization.Parameter;
                var userPasswordPair = Encoding.UTF8.GetString(Convert.FromBase64String(token));
                Assert.That(userPasswordPair, Is.EqualTo("foo:bar"));
            }
        }
    }

    public class TestableBuildsPoller : BuildsPoller
    {
        public TestableBuildsPoller() : base(null, TimeSpan.Zero)
        {
        }

        public static HttpClient PubliclyCreateHttpClient(string serverUrl, string username, string password)
        {
            return CreateHttpClient(serverUrl, username, password);
        }

        public void PubliclyTriggerBuildChanges(Build[] previousBuilds, Build[] currentBuilds)
        {
            base.TriggerBuildChanges(previousBuilds, currentBuilds);
        }
    }
}