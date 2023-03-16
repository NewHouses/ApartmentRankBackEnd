using ApartmentRank.App.Interfaces.Infrastructure;
using ApartmentRank.Infrastructure.EnvironmentAccess;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Runtime.CompilerServices;

namespace ApartmentRank.Infrastructure.Api
{
    public class IdealistaApi : IIdealistaApi
    {
        public IEnumerable<string> GetApartmentsJson(string request)
        {
            var idealistaRequest = JObject.Parse(request);
            var response = PostApiRequest("https://api.idealista.com/3.5/es/search",
                                           GetOauthToken(),
                                           Array.Empty<(string, string)>(),
                                           new[] {
                                                ("operation", idealistaRequest.GetValue("operation").ToString()),
                                                ("propertyType", idealistaRequest.GetValue("propertyType").ToString()),
                                                ("center", idealistaRequest.GetValue("center").ToString()),
                                                ("distance", idealistaRequest.GetValue("distance").ToString()),
                                                ("maxItems", "50"),
                                                ("numPage", "1"),
                                                ("maxPrice", idealistaRequest.GetValue("maxPrice").ToString()),
                                                ("studio", idealistaRequest.GetValue("studio").ToString()),
                                                ("bedrooms", idealistaRequest.GetValue("bedrooms").ToString()),
                                                ("furnished", idealistaRequest.GetValue("furnished").ToString()),
                                           });
            if(bool.Parse(response.GetValue("paginable").ToString()))
            {
                var totalPages = int.Parse(response.GetValue("totalPages").ToString());
                for(int numPage = 2; numPage <= totalPages; numPage++)
                {
                    yield return PostApiRequest("https://api.idealista.com/3.5/es/search",
                                           GetOauthToken(),
                                           Array.Empty<(string, string)>(),
                                           new[] {
                                                ("operation", idealistaRequest.GetValue("operation").ToString()),
                                                ("propertyType", idealistaRequest.GetValue("propertyType").ToString()),
                                                ("center", idealistaRequest.GetValue("center").ToString()),
                                                ("distance", idealistaRequest.GetValue("distance").ToString()),
                                                ("maxItems", "50"),
                                                ("numPage", numPage.ToString()),
                                                ("maxPrice", idealistaRequest.GetValue("maxPrice").ToString()),
                                                ("studio", idealistaRequest.GetValue("studio").ToString()),
                                                ("bedrooms", idealistaRequest.GetValue("bedrooms").ToString()),
                                                ("furnished", idealistaRequest.GetValue("furnished").ToString()),
                                           }).ToString();
                }
            }

            yield return response.ToString();
        }

        private static string GetOauthToken()
        {
            var response = PostApiRequest("https://api.idealista.com/oauth/token",
                                           GetIdealistEncodedKey(),
                                           new[] {
                                                ("cache-control", "no-cache"),
                                                ("content-type", "application/x-www-form-urlencoded")
                                           },
                                           new[] {
                                                ("grant_type", "client_credentials"),
                                                ("scope", "read")
                                           });

            return $"Bearer { response.GetValue("access_token") }";
        }

        private static JObject PostApiRequest(string apiClient, string authorization, IEnumerable<(string, string)> headers, IEnumerable<(string, string)> parameters)
        {
            var client = new RestClient(apiClient);
            var request = new RestRequest();
            request.AddHeader("Authorization", authorization);
            foreach (var h in headers)
                request.AddHeader(h.Item1, h.Item2);
            foreach (var p in parameters)
                request.AddParameter(p.Item1, p.Item2);

            var response = client.Post(request);
            return JObject.Parse(response.Content);
        }

        private static string GetIdealistEncodedKey()
        {
            var apiKey = EnvironmentVariables.IdealistaApiKey;
            var apiKeySecret = EnvironmentVariables.IdealistaApiKeySecret;
            return $"Basic { Base64Encode(apiKey + ":" + apiKeySecret)}";
        }

        private static string Base64Encode(string str)
        {
            byte[] encbuff = System.Text.Encoding.UTF8.GetBytes(str);
            return Convert.ToBase64String(encbuff);
        }
    }
}