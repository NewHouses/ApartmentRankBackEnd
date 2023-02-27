using ApartmentRank.Domain.Entities.Idealista;
using NUnit.Framework;

namespace ApartmentRank.Domain.UnitTests
{
    public class WhenAdaptIdealistaResponse
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ToIdealistaResponseEntity()
        {
            var idealistaResponseJson = AssumeIdealistaResponsejson();

            var idealistaResponse = IdealistaResponse.FromJson(idealistaResponseJson);

            Assert.That(idealistaResponse.elementList.Count(), Is.EqualTo(2));
            Assert.That(idealistaResponse.total, Is.EqualTo(2));
            var elementList = idealistaResponse.elementList.ToArray();
            Assert.That(elementList[0].thumbnail, Is.EqualTo("https://img3.idealista.com/blur/WEB_LISTING/0/id.pro.es.image.master/e3/54/55/1094146089.jpg"));
            Assert.That(elementList[0].price, Is.EqualTo(800.0));
            Assert.That(elementList[0].propertyType, Is.EqualTo("flat"));
            Assert.That(elementList[0].operation, Is.EqualTo("rent"));
            Assert.That(elementList[0].size, Is.EqualTo(75.0));
            Assert.That(elementList[0].exterior, Is.EqualTo(true));
            Assert.That(elementList[0].rooms, Is.EqualTo(2));
            Assert.That(elementList[0].bathrooms, Is.EqualTo(1));
            Assert.That(elementList[0].latitude, Is.EqualTo(42.2417661));
            Assert.That(elementList[0].longitude, Is.EqualTo(-8.7223115));
            Assert.That(elementList[0].url, Is.EqualTo("https://www.idealista.com/inmueble/100633847/"));
            Assert.That(elementList[0].description, Is.EqualTo("¡Estupendo piso en el centro de Vigo! Cuenta con dos habitaciones, un baño completo, cocina independiente y un precioso salón-comedor. Tiene una fantástica terraza cerrada para disfrutar de las vistas a la ría de Vigo. Listo para entrar a vivir, completamente equipado, en una ubicación privilegiada con todos los servicios cerca. Comunidad, agua y calefacción central incluida en el precio. Para más información, no dudes en consultarnos! Estaremos encantados de ayudarle."));
            Assert.That(elementList[0].status, Is.EqualTo("good"));
            Assert.That(elementList[0].newDevelopment, Is.EqualTo(false));
            Assert.That(elementList[0].hasLift, Is.EqualTo(true));
            Assert.That(elementList[0].parkingSpace, Is.EqualTo(null));
            Assert.That(elementList[0].suggestedTexts.subtitle, Is.EqualTo("Centro - Areal, Vigo"));
            Assert.That(elementList[0].suggestedTexts.title, Is.EqualTo("Piso"));
           
            Assert.That(elementList[1].thumbnail, Is.EqualTo("https://img3.idealista.com/blur/WEB_LISTING/0/id.pro.es.image.master/ec/12/66/1094111367.jpg"));
            Assert.That(elementList[1].price, Is.EqualTo(700.0));
            Assert.That(elementList[1].propertyType, Is.EqualTo("flat"));
            Assert.That(elementList[1].operation, Is.EqualTo("rent"));
            Assert.That(elementList[1].size, Is.EqualTo(72.0));
            Assert.That(elementList[1].exterior, Is.EqualTo(true));
            Assert.That(elementList[1].rooms, Is.EqualTo(2));
            Assert.That(elementList[1].bathrooms, Is.EqualTo(2));
            Assert.That(elementList[1].latitude, Is.EqualTo(42.2219248));
            Assert.That(elementList[1].longitude, Is.EqualTo(-8.752848));
            Assert.That(elementList[1].url, Is.EqualTo("https://www.idealista.com/inmueble/100632683/"));
            Assert.That(elementList[1].description, Is.EqualTo("Zona Bouzas -Tomas Paredes edificio en piedra con muy buenas calidades, vivienda distribuida en dos dormitorios y dos baños uno con bañera en habitación principal y otro con ducha, dispone de cocina amplia totalmente equipada con una zona de lavadero y una pequeña terraza. Las habitaciones con armarios empotrados hasta el techo. Calefacción individual. Plaza de garaje en primer sotano amplia con acceso directo al piso, trastero en el bajo cubierta. La vivienda se entrega amueblada a excepción de una habitación que se podria negociar, tambien posibilidad de retirar algún mueble. NO se admiten mascotas.")); 
            Assert.That(elementList[1].status, Is.EqualTo("good"));
            Assert.That(elementList[1].newDevelopment, Is.EqualTo(false));
            Assert.That(elementList[1].hasLift, Is.EqualTo(true));
            Assert.That(elementList[1].parkingSpace.hasParkingSpace, Is.EqualTo(true));
            Assert.That(elementList[1].parkingSpace.isParkingSpaceIncludedInPrice, Is.EqualTo(true));
            Assert.That(elementList[1].suggestedTexts.subtitle, Is.EqualTo("Coia, Vigo"));
            Assert.That(elementList[1].suggestedTexts.title, Is.EqualTo("Piso en Calle de Tomás Paredes, 1"));
        }

        private string AssumeIdealistaResponsejson()
        {
            string workingDirectory = Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName; 
            var idealistaResponseJsonPath = @"IdealistaResponseTest.json";

            string fullPath = Path.Combine(projectDirectory, idealistaResponseJsonPath);
            return File.ReadAllText(fullPath);
        }
    }
}
