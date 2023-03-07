using ApartmentRank.App.Interfaces.Infrastructure;
using ApartmentRank.App.Services;
using ApartmentRank.Domain.Services;
using NSubstitute;
using NUnit.Framework;

namespace ApartmentRank.App.Services
{
    public class WhenCallingSearchApartmentsService
    {
        private IIdealistaApi idealistaApi;

        [SetUp]
        public void Setup()
        {
            idealistaApi = Substitute.For<IIdealistaApi>();
            idealistaApi.GetApartmentsJson(Arg.Any<string>()).ReturnsForAnyArgs(AssumeIdealistaResponsejson());
        }

        [Test]
        public void GetScoredApartments()
        {
            var searchApartmentService = new SearchApartmentsService(idealistaApi, new RankingService());
        }

        private string AssumeIdealistaResponsejson()
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            var idealistaResponseJsonPath = @"Resources/IdealistaResponseTest.json";

            string fullPath = Path.Combine(projectDirectory, idealistaResponseJsonPath);
            return File.ReadAllText(fullPath);
        }
    }
}