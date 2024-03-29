﻿using ApartmentRank.Domain.Entities;
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

                Assert.That(apartmentRankRequest.filter.operation, Is.EqualTo("rent"));
                Assert.That(apartmentRankRequest.filter.propertyType, Is.EqualTo("homes"));
                Assert.That(apartmentRankRequest.filter.center, Is.EqualTo("42.223661,-8.730236"));
                Assert.That(apartmentRankRequest.filter.distance, Is.EqualTo(15000));
                Assert.That(apartmentRankRequest.filter.maxPrice, Is.EqualTo(850));
                Assert.That(apartmentRankRequest.filter.studio, Is.EqualTo(false));
                Assert.That(apartmentRankRequest.filter.bedrooms, Is.EqualTo(2));
                Assert.That(apartmentRankRequest.filter.furnished, Is.EqualTo("furnished"));
                Assert.That(apartmentRankRequest.preferenceTemplate.name, Is.EqualTo("preference-template-1"));
                Assert.That(apartmentRankRequest.preferenceTemplate.preferences.Count(), Is.EqualTo(2));
                var preferences = apartmentRankRequest.preferenceTemplate.preferences.ToArray();
                Assert.That(preferences[0].name, Is.EqualTo("price"));
                Assert.That(preferences[0].score, Is.EqualTo(5));
                Assert.That(preferences[1].name, Is.EqualTo("size"));
                Assert.That(preferences[1].score, Is.EqualTo(3));
                Assert.That(apartmentRankRequest.preferenceTemplate.preferenceAreas.Count(), Is.EqualTo(1));
                var preferenceAreas = apartmentRankRequest.preferenceTemplate.preferenceAreas.ToArray();
                Assert.That(preferenceAreas[0].area.name, Is.EqualTo("A Miñoca"));
                Assert.That(preferenceAreas[0].area.path.Count(), Is.EqualTo(6));
                Assert.That(preferenceAreas[0].score, Is.EqualTo(5));
            }

            [Test]
            public void ToIdealistaRequestEntity()
            {
                var apartmentRankPreferencesRequestJson = AssumeApartmentRankRequestJson();

                var apartmentRankPreferencesRequest = ApartmentRankRequest.FromJson(apartmentRankPreferencesRequestJson);
                var idealistaAdapter = new IdealistaAdapterFactory().CreateRequestAdapter(apartmentRankPreferencesRequest);
                var idealistaRequest = (IdealistaRequest) idealistaAdapter.Convert();

                Assert.That(idealistaRequest.operation, Is.EqualTo("rent"));
                Assert.That(idealistaRequest.propertyType, Is.EqualTo("homes"));
                Assert.That(idealistaRequest.center, Is.EqualTo("42.223661,-8.730236"));
                Assert.That(idealistaRequest.distance, Is.EqualTo(15000));
                Assert.That(idealistaRequest.maxPrice, Is.EqualTo(850));
                Assert.That(idealistaRequest.studio, Is.EqualTo(false));
                Assert.That(idealistaRequest.bedrooms, Is.EqualTo(2));
                Assert.That(idealistaRequest.furnished, Is.EqualTo("furnished"));
            }

            [Test]
            public void ToIdealistaRequestJson()
            {
                var apartmentRankPreferencesRequestJson = AssumeApartmentRankRequestJson();
                var idealistaConnector = new Connector(new IdealistaAdapterFactory());
                var expectedIdealistaRequestJson = "{\"operation\":\"rent\",\"propertyType\":\"homes\",\"center\":\"42.223661,-8.730236\",\"distance\":15000,\"maxPrice\":850,\"studio\":false,\"bedrooms\":2,\"furnished\":\"furnished\"}";
                var apartmentRankPreferencesRequest = ApartmentRankRequest.FromJson(apartmentRankPreferencesRequestJson);

                var idealistaRequestJson = idealistaConnector.TransformRequest(apartmentRankPreferencesRequest);

                Assert.That(idealistaRequestJson, Is.EqualTo(expectedIdealistaRequestJson));
            }

            private string AssumeApartmentRankRequestJson()
            {
                string workingDirectory = Environment.CurrentDirectory;
                string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
                var idealistaResponseJsonPath = @"Resources/ApartmentRankRequestTest.json";

                string fullPath = Path.Combine(projectDirectory, idealistaResponseJsonPath);
                return File.ReadAllText(fullPath);
            }
        }
    }
}
