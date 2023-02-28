// See https://aka.ms/new-console-template for more information
using ApartmentRank.Domain.Entities;
using ApartmentRank.Domain.Services;
using ApartmentRank.Domain.ValueObjects;

Console.WriteLine("Benvido o clasificador de apartamentos segundo as súas prefrencias");
Console.WriteLine("Porfavor, a continuación introduza os paartamentos que desexa clasificar co formato.");
var continuar = true;
var apartments = new List<Apartment>();
var id = 0;
while (continuar)
{
    id++;
    var name = "";
    var description = "";
    var hasTwoBathrooms = new ApartmentAttribute("hasTwoBathrooms", false);
    var hasWashMachine = new ApartmentAttribute("hasWashMachine", false);
    var allowPets = new ApartmentAttribute("allowPets", false);

    Console.WriteLine($"\nApartamento {id}");
    Console.WriteLine("Introduza o nome: ");
    name = Console.ReadLine();
    Console.WriteLine("Introduza unha descrición: ");
    description = Console.ReadLine();
    Console.WriteLine("Ten 2 baños?: (Si/Non)");
    if (Console.ReadLine().Equals("Si"))
        hasTwoBathrooms.added = true;

    Console.WriteLine("Ten lavalouzas?: (Si/Non)");
    if (Console.ReadLine().Equals("Si"))
        hasWashMachine.added = true;
    Console.WriteLine("Permite mascotas?: (Si/Non)");
    if (Console.ReadLine().Equals("Si"))
        allowPets.added = true;

    apartments.Add(new Apartment(Guid.NewGuid(), name, description, new[] { hasTwoBathrooms, hasWashMachine, allowPets }));

    Console.WriteLine($"\nApartamento {id} introducido, desexa introducir mais apartamentos? (Si/Non)");
    if (Console.ReadLine().Equals("Non"))
        continuar = false;
}

foreach (var apartment in apartments)
{
    Console.WriteLine($"\n\tApartamento {apartment.name}");
    Console.WriteLine($"\t{apartment.description}");
    Console.WriteLine("\tTen os seguintes atributos:");
    foreach (var atributo in apartment.apartmentAttributes)
    {
        if(atributo.added)
            Console.WriteLine($"\t\t{atributo.Name}");
    }
}

Console.WriteLine("\n\nA continuación puntúe do -5 ó 5 os seguintes atributos");

var hasTwoBathroomsPreference = new Preference(new ApartmentAttribute("hasTwoBathrooms", true), 0);
var hasWashMachinePreference = new Preference(new ApartmentAttribute("hasWashMachine", true), 0);
var allowPetsPreference = new Preference(new ApartmentAttribute("allowPets", true), 0);
Console.WriteLine("Ten 2 baños: ");
hasTwoBathroomsPreference.Score = int.Parse(Console.ReadLine());
Console.WriteLine("Ten lavalouzas: ");
hasWashMachinePreference.Score = int.Parse(Console.ReadLine());
Console.WriteLine("Permite mascotas: ");
allowPetsPreference.Score = int.Parse(Console.ReadLine());

Console.WriteLine("\n\nObrigado, esta é clasificación dos apartamentos segundo a súas preferencias:");

var ranking = new RankingService().OrderByPreferences(apartments, new[] { hasTwoBathroomsPreference, hasWashMachinePreference, allowPetsPreference }).Reverse();
var rank = 0;
foreach(var apartment in ranking)
{
    rank++;
    Console.WriteLine($"\n{rank}# apartamento {apartment.Key.name} con {apartment.Value} puntos");
}


