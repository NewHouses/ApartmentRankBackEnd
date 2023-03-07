namespace ApartmentRank.Infrastructure.EnvironmentAccess
{
    public class EnvironmentVariables
    {
        public static readonly string? IdealistaApiKey = Environment.GetEnvironmentVariable("IDEALISTA_API_KEY");
        public static readonly string? IdealistaApiKeySecret = Environment.GetEnvironmentVariable("IDEALISTA_API_KEY_SECRET");
    }
}