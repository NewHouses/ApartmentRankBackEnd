using ApartmentRank.Domain.Entities;
using ApartmentRank.Domain.Interfaces.DomainInterfaces;
using ApartmentRank.Domain.Services;
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
        public void WithoutAnyPreferenceGetApartmentRankingInTheSameOrder()
        {
            var apartments = AssumeApartments();
            var preferences = new Preference[] { };

            var apartmentRanking = rankingService.OrderByPreferences(apartments, preferences).ToArray();

            Assert.That(apartmentRanking[0].Key.id, Is.EqualTo(apartments[0].id));
            Assert.That(apartmentRanking[1].Key.id, Is.EqualTo(apartments[1].id));
            Assert.That(apartmentRanking[2].Key.id, Is.EqualTo(apartments[2].id));
        }

        [Test]
        public void GetApartmentRankingOrderByTheGivenPreferences()
        {
            var apartments = AssumeApartments();
            var preferences = new Preference[] {
                new Preference(new ValueObjects.ApartmentAttribute("hasTwoBathrooms", true), 1),
                new Preference(new ValueObjects.ApartmentAttribute("hasWashMachine", true), 1),
                new Preference(new ValueObjects.ApartmentAttribute("allowPets", true), 1)
            };

            var apartmentRanking = rankingService.OrderByPreferences(apartments, preferences).ToArray();

            Assert.That(apartmentRanking[0].Key.id, Is.EqualTo(apartments[1].id));
            Assert.That(apartmentRanking[0].Value, Is.EqualTo(1));
            Assert.That(apartmentRanking[1].Key.id, Is.EqualTo(apartments[0].id));
            Assert.That(apartmentRanking[1].Value, Is.EqualTo(2));
            Assert.That(apartmentRanking[2].Key.id, Is.EqualTo(apartments[2].id));
            Assert.That(apartmentRanking[2].Value, Is.EqualTo(3));
        }

        private static Apartment[] AssumeApartments()
        {
            return new Apartment[] {
                new Apartment(Guid.NewGuid(), "primeiro", "lindo", new ValueObjects.ApartmentAttribute[] {
                    new ValueObjects.ApartmentAttribute("hasTwoBathrooms", true),
                    new ValueObjects.ApartmentAttribute("hasWashMachine", true),
                    new ValueObjects.ApartmentAttribute("allowPets", false)
                }),
                new Apartment(Guid.NewGuid(), "segundo", "feo", new ValueObjects.ApartmentAttribute[] {
                    new ValueObjects.ApartmentAttribute("hasTwoBathrooms", true),
                    new ValueObjects.ApartmentAttribute("hasWashMachine", false),
                    new ValueObjects.ApartmentAttribute("allowPets", false)
                }),
                new Apartment(Guid.NewGuid(), "terceiro", "perfecto", new ValueObjects.ApartmentAttribute[] {
                    new ValueObjects.ApartmentAttribute("hasTwoBathrooms", true),
                    new ValueObjects.ApartmentAttribute("hasWashMachine", true),
                    new ValueObjects.ApartmentAttribute("allowPets", true)
                })
            };
        }
    }
}