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
            var preferences = Array.Empty<Preference>();

            var apartmentRanking = rankingService.OrderByPreferences(apartments, preferences).ToArray();

            Assert.That(apartmentRanking[0].Key.id, Is.EqualTo(apartments[1].id));
            Assert.That(apartmentRanking[0].Value, Is.EqualTo(0));
            Assert.That(apartmentRanking[1].Key.id, Is.EqualTo(apartments[0].id));
            Assert.That(apartmentRanking[1].Value, Is.EqualTo(10));
            Assert.That(apartmentRanking[2].Key.id, Is.EqualTo(apartments[2].id));
            Assert.That(apartmentRanking[2].Value, Is.EqualTo(17));
        }


        [Test]
        public void GetApartmentRankingOrderByTheGivenPreferences()
        {
            var apartments = AssumeApartments();
            var preferences = new Preference[] {
                new Preference("price", 1),
                new Preference("hasWashMachine", 1),
                new Preference("allowPets", 1)
            };

            var apartmentRanking = rankingService.OrderByPreferences(apartments, preferences).ToArray();

            Assert.That(apartmentRanking[0].Key.id, Is.EqualTo(apartments[1].id));
            Assert.That(apartmentRanking[0].Value, Is.EqualTo(0));
            Assert.That(apartmentRanking[1].Key.id, Is.EqualTo(apartments[0].id));
            Assert.That(apartmentRanking[1].Value, Is.EqualTo(9));
            Assert.That(apartmentRanking[2].Key.id, Is.EqualTo(apartments[2].id));
            Assert.That(apartmentRanking[2].Value, Is.EqualTo(15));
        }

        [Test]
        public void GetScoredApartmentResponseByTheGivenPreferences()
        {
            var apartments = AssumeApartments();
            var apartmentRankResponse = new ApartmentRankResponse(apartments, apartments.Length);
            var preferences = new Preference[] {
                new Preference("price", 1),
                new Preference("hasWashMachine", 1),
                new Preference("allowPets", 1)
            };

            var scoredApartmentRankResponse = rankingService.GetScoredApartmentRankResponse(apartmentRankResponse, preferences);

            Assert.That(scoredApartmentRankResponse.apartments.Count(), Is.EqualTo(3));
            var scoredApartments = scoredApartmentRankResponse.apartments.ToArray();
            Assert.That(scoredApartments[0].score, Is.EqualTo(9));
            Assert.That(scoredApartments[1].score, Is.EqualTo(0));
            Assert.That(scoredApartments[2].score, Is.EqualTo(15));
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