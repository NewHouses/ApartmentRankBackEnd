using ApartmentRank.App.Interfaces.Infrastructure;
using ApartmentRank.Domain.ValueObjects;
using ApartmentRank.Infrastructure.EnvironmentAccess;
using Newtonsoft.Json.Linq;
using OpenAI_API;
using System.Diagnostics;

namespace ApartmentRank.Infrastructure.Api
{
    public class OpenAIApi : IOpenAIApi
    {
        private OpenAIAPI api = new OpenAIAPI(EnvironmentVariables.OpenAIApiKey);
        private string openAIRequestMessage = "Generate a JSON with allow_pets, terrace, storage_room, community_tax_included, electricity_costs_included (only true if explicitly it says that the electricity costs are include in the price, take into account that mention electrivitydoesn't mean that this is included), gas_costs_included (only true if explicitly it says that the gas or the \r\nheating costs are include in the price, take into account that mention gas doesn't mean that this is included) and water_costs_included (only true if explicitly it says that the water costs are include in the price, take into account that mention water doesn't mean that this is included). (possible results: true, false) from the next apartment description: ";

        public async Task<IEnumerable<ApartmentAttribute>> GetApartmentAttributes(string apartmentDescription)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            while (true)
            {
                try
                {
                    var chat = api.Chat.CreateConversation();
                    chat.AppendSystemMessage(openAIRequestMessage);
                    chat.AppendUserInput(apartmentDescription);
                    var response = await chat.GetResponseFromChatbot();
                    var cleanedResponse = response.Replace("\n", "").Replace("\r", "").Replace(" ", "");
                    var apartmentAttributesJson = JObject.Parse(cleanedResponse.Substring(cleanedResponse.IndexOf('{'), cleanedResponse.IndexOf('}') + 1));
                    var apartmentAttributes = new ApartmentAttribute[] 
                    {
                        new ApartmentAttribute("allow_pets", bool.Parse(apartmentAttributesJson.GetValue("allow_pets").ToString())),
                        new ApartmentAttribute("terrace", bool.Parse(apartmentAttributesJson.GetValue("terrace").ToString())),
                        new ApartmentAttribute("storage_room", bool.Parse(apartmentAttributesJson.GetValue("storage_room").ToString())),
                        new ApartmentAttribute("community_tax_included", bool.Parse(apartmentAttributesJson.GetValue("community_tax_included").ToString())),
                        new ApartmentAttribute("electricity_costs_included", bool.Parse(apartmentAttributesJson.GetValue("electricity_costs_included").ToString())),
                        new ApartmentAttribute("gas_costs_included", bool.Parse(apartmentAttributesJson.GetValue("gas_costs_included").ToString())),
                        new ApartmentAttribute("water_costs_included", bool.Parse(apartmentAttributesJson.GetValue("water_costs_included").ToString()))
                    };

                    return apartmentAttributes;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e.Message);
                    if(stopWatch.ElapsedMilliseconds > 60000)
                    {
                        stopWatch.Stop();
                        Console.WriteLine("Timeout");
                        return Array.Empty<ApartmentAttribute>();
                    }
                }
            }
        }
    }
}
