using ApartmentRank.App.Interfaces.Infrastructure;
using ApartmentRank.Domain.Services;
using NSubstitute;
using NUnit.Framework;

namespace ApartmentRank.App.Services
{
    public class WhenCallingSearchApartmentsService
    {
        private IIdealistaApi idealistaApi;
        private IOpenAIApi openAiApi;

        [SetUp]
        public void Setup()
        {
            idealistaApi = Substitute.For<IIdealistaApi>();
            openAiApi = Substitute.For<IOpenAIApi>();
            idealistaApi.GetApartmentsJson(Arg.Any<string>()).ReturnsForAnyArgs(new[] { AssumeIdealistaResponsejson() });
            openAiApi.GetApartmentAttributes("¡Estupendo piso en el centro de Vigo! Cuenta con dos habitaciones, un baño completo, cocina independiente y un precioso salón-comedor. Tiene una fantástica terraza cerrada para disfrutar de las vistas a la ría de Vigo. Listo para entrar a vivir, completamente equipado, en una ubicación privilegiada con todos los servicios cerca. Comunidad, agua y calefacción central incluida en el precio. Para más información, no dudes en consultarnos! Estaremos encantados de ayudarle.")
                .Returns(new[] {
            new Domain.ValueObjects.ApartmentAttribute("allow_pets",false),
            new Domain.ValueObjects.ApartmentAttribute("terrace",true),
            new Domain.ValueObjects.ApartmentAttribute("storage_room",false),
            new Domain.ValueObjects.ApartmentAttribute("community_tax_included",true),
            new Domain.ValueObjects.ApartmentAttribute("electricity_costs_included",false),
            new Domain.ValueObjects.ApartmentAttribute("gas_costs_included",true),
            new Domain.ValueObjects.ApartmentAttribute("water_costs_included",true)
        });
            openAiApi.GetApartmentAttributes("Zona Bouzas -Tomas Paredes edificio en piedra con muy buenas calidades, vivienda distribuida en dos dormitorios y dos baños uno con bañera en habitación principal y otro con ducha, dispone de cocina amplia totalmente equipada con una zona de lavadero y una pequeña terraza. Las habitaciones con armarios empotrados hasta el techo. Calefacción individual. Plaza de garaje en primer sotano amplia con acceso directo al piso, trastero en el bajo cubierta. La vivienda se entrega amueblada a excepción de una habitación que se podria negociar, tambien posibilidad de retirar algún mueble. NO se admiten mascotas.")
                .Returns(new[] {
            new Domain.ValueObjects.ApartmentAttribute("allow_pets",false),
            new Domain.ValueObjects.ApartmentAttribute("terrace",true),
            new Domain.ValueObjects.ApartmentAttribute("storage_room",true),
            new Domain.ValueObjects.ApartmentAttribute("community_tax_included",false),
            new Domain.ValueObjects.ApartmentAttribute("electricity_costs_included",false),
            new Domain.ValueObjects.ApartmentAttribute("gas_costs_included",false),
            new Domain.ValueObjects.ApartmentAttribute("water_costs_included",false)
        });
        }

        [Test]
        public async Task GetScoredApartments()
        {
            var searchApartmentService = new SearchApartmentsService(idealistaApi, openAiApi, new RankingService());

            var apartmentRankResponse = await searchApartmentService.GetScoredApartments(AssumeApartmentRankRequestJson());

            Assert.That(apartmentRankResponse.total, Is.EqualTo(2));
            Assert.That(apartmentRankResponse.apartments.Count(), Is.EqualTo(2));
            var apartments = apartmentRankResponse.apartments.ToArray();
            Assert.That(apartments[0].name, Is.EqualTo("Piso Centro - Areal, Vigo"));
            Assert.That(apartments[0].description, Is.EqualTo("¡Estupendo piso en el centro de Vigo! Cuenta con dos habitaciones, un baño completo, cocina independiente y un precioso salón-comedor. Tiene una fantástica terraza cerrada para disfrutar de las vistas a la ría de Vigo. Listo para entrar a vivir, completamente equipado, en una ubicación privilegiada con todos los servicios cerca. Comunidad, agua y calefacción central incluida en el precio. Para más información, no dudes en consultarnos! Estaremos encantados de ayudarle."));
            Assert.That(apartments[0].imageUrl, Is.EqualTo("https://img3.idealista.com/blur/WEB_LISTING/0/id.pro.es.image.master/e3/54/55/1094146089.jpg"));
            Assert.That(apartments[0].price, Is.EqualTo(800.0));
            Assert.That(apartments[0].propertyType, Is.EqualTo("flat"));
            Assert.That(apartments[0].operation, Is.EqualTo("rent"));
            Assert.That(apartments[0].size, Is.EqualTo(75.0));
            Assert.That(apartments[0].isExterior, Is.EqualTo(true));
            Assert.That(apartments[0].rooms, Is.EqualTo(2));
            Assert.That(apartments[0].bathrooms, Is.EqualTo(1));
            Assert.That(apartments[0].latitude, Is.EqualTo(42.2417661));
            Assert.That(apartments[0].longitude, Is.EqualTo(-8.7223115));
            Assert.That(apartments[0].link, Is.EqualTo("https://www.idealista.com/inmueble/100633847/"));
            Assert.That(apartments[0].status, Is.EqualTo("good"));
            Assert.That(apartments[0].newDevelopment, Is.EqualTo(false));
            Assert.That(apartments[0].parkingSpace.hasParkingSpace, Is.EqualTo(false));
            Assert.That(apartments[0].parkingSpace.isParkingSpaceIncludedInPrice, Is.EqualTo(false));
            var apartmentAttributes = apartments[0].apartmentAttributes.ToArray();
            Assert.That(apartmentAttributes.Count(), Is.EqualTo(7));
            Assert.That(apartmentAttributes[0].name, Is.EqualTo("allow_pets"));
            Assert.That(apartmentAttributes[0].added, Is.EqualTo(false));
            Assert.That(apartmentAttributes[1].name, Is.EqualTo("terrace"));
            Assert.That(apartmentAttributes[1].added, Is.EqualTo(true));
            Assert.That(apartmentAttributes[2].name, Is.EqualTo("storage_room"));
            Assert.That(apartmentAttributes[2].added, Is.EqualTo(false));
            Assert.That(apartmentAttributes[3].name, Is.EqualTo("community_tax_included"));
            Assert.That(apartmentAttributes[3].added, Is.EqualTo(true));
            Assert.That(apartmentAttributes[4].name, Is.EqualTo("electricity_costs_included"));
            Assert.That(apartmentAttributes[4].added, Is.EqualTo(false));
            Assert.That(apartmentAttributes[5].name, Is.EqualTo("gas_costs_included"));
            Assert.That(apartmentAttributes[5].added, Is.EqualTo(true));
            Assert.That(apartmentAttributes[6].name, Is.EqualTo("water_costs_included"));
            Assert.That(apartmentAttributes[6].added, Is.EqualTo(true));
            Assert.That(apartments[0].score, Is.EqualTo(11));

            Assert.That(apartments[1].name, Is.EqualTo("Piso en Calle de Tomás Paredes, 1 Coia, Vigo"));
            Assert.That(apartments[1].description, Is.EqualTo("Zona Bouzas -Tomas Paredes edificio en piedra con muy buenas calidades, vivienda distribuida en dos dormitorios y dos baños uno con bañera en habitación principal y otro con ducha, dispone de cocina amplia totalmente equipada con una zona de lavadero y una pequeña terraza. Las habitaciones con armarios empotrados hasta el techo. Calefacción individual. Plaza de garaje en primer sotano amplia con acceso directo al piso, trastero en el bajo cubierta. La vivienda se entrega amueblada a excepción de una habitación que se podria negociar, tambien posibilidad de retirar algún mueble. NO se admiten mascotas."));
            Assert.That(apartments[1].imageUrl, Is.EqualTo("https://img3.idealista.com/blur/WEB_LISTING/0/id.pro.es.image.master/ec/12/66/1094111367.jpg"));
            Assert.That(apartments[1].price, Is.EqualTo(700.0));
            Assert.That(apartments[1].propertyType, Is.EqualTo("flat"));
            Assert.That(apartments[1].operation, Is.EqualTo("rent"));
            Assert.That(apartments[1].size, Is.EqualTo(72.0));
            Assert.That(apartments[1].isExterior, Is.EqualTo(true));
            Assert.That(apartments[1].rooms, Is.EqualTo(2));
            Assert.That(apartments[1].bathrooms, Is.EqualTo(2));
            Assert.That(apartments[1].latitude, Is.EqualTo(42.2219248));
            Assert.That(apartments[1].longitude, Is.EqualTo(-8.752848));
            Assert.That(apartments[1].link, Is.EqualTo("https://www.idealista.com/inmueble/100632683/"));
            Assert.That(apartments[1].status, Is.EqualTo("good"));
            Assert.That(apartments[1].newDevelopment, Is.EqualTo(false));
            Assert.That(apartments[1].parkingSpace.hasParkingSpace, Is.EqualTo(true));
            Assert.That(apartments[1].parkingSpace.isParkingSpaceIncludedInPrice, Is.EqualTo(true));
            apartmentAttributes = apartments[1].apartmentAttributes.ToArray();
            Assert.That(apartmentAttributes.Count(), Is.EqualTo(7));
            Assert.That(apartmentAttributes[0].name, Is.EqualTo("allow_pets"));
            Assert.That(apartmentAttributes[0].added, Is.EqualTo(false));
            Assert.That(apartmentAttributes[1].name, Is.EqualTo("terrace"));
            Assert.That(apartmentAttributes[1].added, Is.EqualTo(true));
            Assert.That(apartmentAttributes[2].name, Is.EqualTo("storage_room"));
            Assert.That(apartmentAttributes[2].added, Is.EqualTo(true));
            Assert.That(apartmentAttributes[3].name, Is.EqualTo("community_tax_included"));
            Assert.That(apartmentAttributes[3].added, Is.EqualTo(false));
            Assert.That(apartmentAttributes[4].name, Is.EqualTo("electricity_costs_included"));
            Assert.That(apartmentAttributes[4].added, Is.EqualTo(false));
            Assert.That(apartmentAttributes[5].name, Is.EqualTo("gas_costs_included"));
            Assert.That(apartmentAttributes[5].added, Is.EqualTo(false));
            Assert.That(apartmentAttributes[6].name, Is.EqualTo("water_costs_included"));
            Assert.That(apartmentAttributes[6].added, Is.EqualTo(false));
            Assert.That(apartments[1].score, Is.EqualTo(13));
        }

        private string AssumeApartmentRankRequestJson()
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
            var idealistaResponseJsonPath = @"Resources/ApartmentRankRequestTest.json";

            string fullPath = Path.Combine(projectDirectory, idealistaResponseJsonPath);
            return File.ReadAllText(fullPath);
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