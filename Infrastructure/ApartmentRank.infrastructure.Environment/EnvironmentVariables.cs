namespace ApartmentRank.Infrastructure.EnvironmentAccess
{
    public class EnvironmentVariables
    {
        public static readonly string? IdealistaApiKey = Environment.GetEnvironmentVariable("IDEALISTA_API_KEY");
        public static readonly string? IdealistaApiKeySecret = Environment.GetEnvironmentVariable("IDEALISTA_API_KEY_SECRET");
        public static readonly string? OpenAIApiKey = Environment.GetEnvironmentVariable("OPEN_AI_API_KEY");
        public static readonly string? ConnectionString = Environment.GetEnvironmentVariable("APARTMENTRANK_CONNECTIONSTRING");
    }
}