using System;

public static class ConfigurationHelper
{

    private static IConfiguration _configuration;

    static ConfigurationHelper()
    {
        string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        _configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{environment}.json", optional: true, reloadOnChange: true)
            .Build();
    }

    public static string GetConfigValue()
    {
        return _configuration["FilePath"];
    }
}
