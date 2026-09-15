using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SauceDemoProject.Tests.Helper
{
    public static class DriverFactory
    {
        public static IWebDriver CreateDriver()
        {
            var settings = Config.Settings;

            var options = new ChromeOptions();
            if (settings.Headless)
            {
                options.AddArgument("--headless=new");
            }
            options.AddArgument("--disable-notifications");
            options.AddArgument("--no-sandbox");
            options.AddArgument("--disable-dev-shm-usage");
            options.AddArgument("--disable-gpu");
            options.AddArgument("--window-size=1920,1080");

            var service = ChromeDriverService.CreateDefaultService();
            service.EnableVerboseLogging = true;
            service.LogPath = "chromedriver.log";

            IWebDriver driver = new ChromeDriver(service, options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(settings.ImplicitWaitSeconds);
            driver.Manage().Window.Maximize();

            return driver;
        }
    }
}