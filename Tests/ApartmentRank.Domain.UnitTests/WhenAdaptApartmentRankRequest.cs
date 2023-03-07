using ApartmentRank.Domain.Entities;
using ApartmentRank.Domain.Entities.Idealista;
using ApartmentRank.Domain.Services;
using ApartmentRank.Domain.Services.Factories;
using NUnit.Framework;

namespace ApartmentRank.Domain.UnitTests
{
    public class WhenAdaptApartmentRankRequest
    {
        public class WhenAdaptIdealistaResponse
        {
            [SetUp]
            public void Setup()
            {
            }

            [Test]
            public void ToApartmentRankRequestEntity()
            {
                var apartmentRankPreferencesRequestJson = AssumeApartmentRankRequestJson();

                var apartmentRankRequest = ApartmentRankRequest.FromJson(apartmentRankPreferencesRequestJson);

                Assert.That(apartmentRankRequest.preferences.Count(), Is.EqualTo(2));
                var preferences = apartmentRankRequest.preferences.ToArray();
                Assert.That(preferences[0].apartmentAttribute.name, Is.EqualTo("hasLift"));
                Assert.That(preferences[0].apartmentAttribute.added, Is.EqualTo(true));
                Assert.That(preferences[0].score, Is.EqualTo(1));
                Assert.That(preferences[1].apartmentAttribute.name, Is.EqualTo("allowPets"));
                Assert.That(preferences[1].apartmentAttribute.added, Is.EqualTo(true));
                Assert.That(preferences[1].score, Is.EqualTo(2));
            }

            [Test]
            public void ToIdealistaRequestEntity()
            {
                var apartmentRankPreferencesRequestJson = AssumeApartmentRankRequestJson();

                var idealistaAdapter = new IdealistaAdapterFactory().CreateRequestAdapter(apartmentRankPreferencesRequestJson);
                var idealistaRequest = idealistaAdapter.Convert();

                Assert.That(idealistaRequest.GetType(), Is.EqualTo(new IdealistaRequest().GetType()));
            }

            [Test]
            public void ToIdealistaRequestJson()
            {
                var apartmentRankPreferencesRequestJson = AssumeApartmentRankRequestJson();
                var idealistaConnector = new Connector(new IdealistaAdapterFactory());
                var expectedIdealistaRequestJson = "{}";

                var idealistaRequestJson = idealistaConnector.TransformRequest(apartmentRankPreferencesRequestJson);

                Assert.That(idealistaRequestJson, Is.EqualTo(expectedIdealistaRequestJson));
            }

            private string AssumeApartmentRankRequestJson()
            {
                string workingDirectory = Environment.CurrentDirectory;
                string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
                var idealistaResponseJsonPath = @"Resources/ApartmentRankPreferencesRequestTest.json";

                string fullPath = Path.Combine(projectDirectory, idealistaResponseJsonPath);
                return File.ReadAllText(fullPath);
            }
        }
    }
}
