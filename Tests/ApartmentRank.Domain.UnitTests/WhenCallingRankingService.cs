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
            Assert.That(apartmentRanking[1].Value, Is.EqualTo(8));
            Assert.That(apartmentRanking[2].Key.id, Is.EqualTo(apartments[2].id));
            Assert.That(apartmentRanking[2].Value, Is.EqualTo(16));
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
            var preferenceAreas = new PreferenceArea[] {
                new PreferenceArea(new Area("A Miñoca", new LatLong[]
                {
                    new LatLong(42.21814480184685, -8.74379524270523),
                    new LatLong(42.21647618524271, -8.743494835295563),
                    new LatLong(42.2146644944378, -8.741885509886627),
                    new LatLong(42.21582461821853, -8.738023128905182),
                    new LatLong(42.21944788129572, -8.734461155333404),
                    new LatLong(42.220226537887314, -8.735984650053863),
                    new LatLong(42.218542085016004, -8.743451919951324)
                }), 5),
                new PreferenceArea(new Area("Coia", new LatLong[]
                {
                    new LatLong(42.2260222, -8.7274875),
                    new LatLong(42.2260222, -8.7274873),
                    new LatLong(42.2260220, -8.7274875),
                    new LatLong(42.2260220, -8.7274873),
                }), 3)
            };
            var preferenceTemplate = new PreferenceTemplate("PreferenceTemplate-1", preferences, preferenceAreas);

            var apartmentRanking = rankingService.OrderByPreferences(apartments, preferenceTemplate).ToArray();

            Assert.That(apartmentRanking[0].Key.id, Is.EqualTo(apartments[1].id));
            Assert.That(apartmentRanking[0].Value, Is.EqualTo(0));
            Assert.That(apartmentRanking[1].Key.id, Is.EqualTo(apartments[0].id));
            Assert.That(apartmentRanking[1].Value, Is.EqualTo(9));
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
            var preferenceAreas = new PreferenceArea[] {
                new PreferenceArea(new Area("A Miñoca", new LatLong[]
                {
                    new LatLong(42.2187918, -8.749664),
                    new LatLong(42.2108457, -8.7515093),
                    new LatLong(42.2197771, -8.7457158),
                    new LatLong(42.2108457, -8.7325408),
                    new LatLong(42.2215886, -8.7337424),
                    new LatLong(42.2187918, -8.749664)
                }), 5),
                new PreferenceArea(new Area("Coia", new LatLong[]
                {
                    new LatLong(42.2260222, -8.7274875),
                    new LatLong(42.2260222, -8.7274873),
                    new LatLong(42.2260220, -8.7274875),
                    new LatLong(42.2260220, -8.7274873),
                }), 2)
            };
            var preferenceTemplate = new PreferenceTemplate("PreferenceTemplate-1", preferences, preferenceAreas);

            var scoredApartmentRankResponse = rankingService.GetScoredApartmentRankResponse(apartmentRankResponse, preferenceTemplate);

            Assert.That(scoredApartmentRankResponse.apartments.Count(), Is.EqualTo(3));
            var scoredApartments = scoredApartmentRankResponse.apartments.ToArray();
            Assert.That(scoredApartments[0].score, Is.EqualTo(8));
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