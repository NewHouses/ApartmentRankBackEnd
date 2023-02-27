using ApartmentRank.App.Services.InfrastructureInterfaces;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace ApartmentRank.Infrastructure.Api
{
    public class IdealistaApi : IIdealistaApi
    {
        public string GetApartmentsJson()
        {
            var apiKey = "tim5hcgo934pkzffpub8g20i9zico8ep"; //TODO: GET the apiKey from variableEnvironment
            var apiKeySecret = "AzKG9tzEtIPt"; //TODO: GET the apiKeySecret from variableEnvironment
            var encodedKey = Base64Encode(apiKey + ":" + apiKeySecret);
            //TODO: Get the accesToken from a mathod which will create it
            var accesToken = "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzY29wZSI6WyJyZWFkIl0sImV4cCI6MTY3NjUyMTMzMiwiYXV0aG9yaXRpZXMiOlsiUk9MRV9QVUJMSUMiXSwianRpIjoiOTc5NTQwZTgtYWUxYy00MTIwLWI5OWUtOTUxYjA3M2FkOGJkIiwiY2xpZW50X2lkIjoidGltNWhjZ285MzRwa3pmZnB1YjhnMjBpOXppY284ZXAifQ.qynQ_ppywuVpmPyTRR3BzgzCnFnnGwSdy2UeK7rRb4E";       

            var client = new RestClient("https://api.idealista.com/3.5/es/search");
            var request = new RestRequest();
            request.AddHeader("Authorization", accesToken);
            request.AddParameter("operation", "rent");
            request.AddParameter("propertyType", "homes");
            request.AddParameter("center", "42.223661,-8.730236");
            request.AddParameter("distance", "15000");
            request.AddParameter("maxItems", 50);
            request.AddParameter("maxPrice", 850);
            request.AddParameter("studio", false);
            request.AddParameter("bedrooms", 2);
            request.AddParameter("furnished", "furnished");
            var response = client.Post(request);
            return (string)JObject.Parse(response.Content);
        }

        private static string Base64Encode(string str)
        {
            byte[] encbuff = System.Text.Encoding.UTF8.GetBytes(str);
            return Convert.ToBase64String(encbuff);
        }
    }
}