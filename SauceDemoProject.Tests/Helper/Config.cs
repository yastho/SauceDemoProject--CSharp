using Microsoft.Extensions.Configuration;

namespace SauceDemoProject.Tests.Helper
{
    public class TestSettings
    {
        public string BaseUrl { get; set; } = string.Empty;
        public string Browser { get; set; } = "Chrome";
        public bool Headless { get; set; }
        public int ImplicitWaitSeconds { get; set; } = 5;
        public int ExplicitWaitSeconds { get; set; } = 10;
    }

    public static class Config
    {
        private static readonly Lazy<TestSettings> _settings = new(LoadSettings);

        public static TestSettings Settings => _settings.Value;

        private static TestSettings LoadSettings()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("settings.json", optional: false, reloadOnChange: false)
                .AddEnvironmentVariables()
                .Build();
            Console.WriteLine($"[DEBUG] Raw config value for TestSettings:Headless = {config["TestSettings:Headless"]}");
            var settings = new TestSettings();
            config.GetSection("TestSettings").Bind(settings);
            return settings;
        }
    }
}