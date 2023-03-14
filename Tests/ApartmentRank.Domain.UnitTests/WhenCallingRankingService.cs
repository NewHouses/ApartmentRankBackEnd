using ApartmentRank.Domain.Entities;
using ApartmentRank.Domain.Services;
using ApartmentRank.Domain.Services.Interfaces;
using ApartmentRank.Domain.UnitTests.Builders;
using ApartmentRank.Domain.ValueObjects;
using NUnit.Framework;

namespace ApartmentRank.Domain.UnitTests
{
    public class WhenCallingRankingService
    {
        private IRankingService rankingService;

        [SetUp]
        public void Setup()
        {
            rankingService = new RankingService();
        }


        [Test]
        public void GetApartmentRankingOrderWithoutGivenPreferences()
        {
            var apartments = AssumeApartments();
            var preferenceTemplate = new PreferenceTemplate("EmptyPreferenceTemplate", Array.Empty<Preference>(), Array.Empty<PreferenceArea>());

            var apartmentRanking = rankingService.OrderByPreferences(apartments, preferenceTemplate).ToArray();

            Assert.That(apartmentRanking[0].Key.id, Is.EqualTo(apartments[1].id));
            Assert.That(apartmentRanking[0].Value, Is.EqualTo(0));
            Assert.That(apartmentRanking[1].Key.id, Is.EqualTo(apartments[0].id));
            Assert.That(apartmentRanking[1].Value, Is.EqualTo(12));
            Assert.That(apartmentRanking[2].Key.id, Is.EqualTo(apartments[2].id));
            Assert.That(apartmentRanking[2].Value, Is.EqualTo(21));
        }


        [Test]
        public void GetApartmentRankingOrderByTheGivenPreferences()
        {
            var apartments = AssumeApartments();
            var preferences = new Preference[] {
                new Preference("price", 2),
                new Preference("size", 5),
                new Preference("allowPets", 1)
            };
            var preferenceTemplate = new PreferenceTemplate("PreferenceTemplate-1", preferences, Array.Empty<PreferenceArea>());

            var apartmentRanking = rankingService.OrderByPreferences(apartments, preferenceTemplate).ToArray();

            Assert.That(apartmentRanking[0].Key.id, Is.EqualTo(apartments[1].id));
            Assert.That(apartmentRanking[0].Value, Is.EqualTo(0));
            Assert.That(apartmentRanking[1].Key.id, Is.EqualTo(apartments[0].id));
            Assert.That(apartmentRanking[1].Value, Is.EqualTo(10));
            Assert.That(apartmentRanking[2].Key.id, Is.EqualTo(apartments[2].id));
            Assert.That(apartmentRanking[2].Value, Is.EqualTo(19));
        }

        [Test]
        public void GetScoredApartmentResponseByTheGivenPreferences()
        {
            var apartments = AssumeApartments();
            var apartmentRankResponse = new ApartmentRankResponse(apartments, apartments.Length);
            var preferences = new Preference[] {
                new Preference("price", 2),
                new Preference("size", 5),
                new Preference("allowPets", 1)
            };
            var preferenceTemplate = new PreferenceTemplate("PreferenceTemplate-1", preferences, Array.Empty<PreferenceArea>());

            var scoredApartmentRankResponse = rankingService.GetScoredApartmentRankResponse(apartmentRankResponse, preferenceTemplate);

            Assert.That(scoredApartmentRankResponse.apartments.Count(), Is.EqualTo(3));
            var scoredApartments = scoredApartmentRankResponse.apartments.ToArray();
            Assert.That(scoredApartments[0].score, Is.EqualTo(10));
            Assert.That(scoredApartments[1].score, Is.EqualTo(0));
            Assert.That(scoredApartments[2].score, Is.EqualTo(19));
        }

        private static Apartment[] AssumeApartments()
        {
            return new Apartment[] 
            {
                ApartmentBuilder.BuildGoodApartment(),
                ApartmentBuilder.BuildBadApartment(),
                ApartmentBuilder.BuildPerfectApartment()
            };
        }
    }
}