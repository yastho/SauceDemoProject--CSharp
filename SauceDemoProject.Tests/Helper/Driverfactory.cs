using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace SauceDemoProject.Tests.Helper
{
    public static class DriverFactory
    {
        public static IWebDriver CreateDriver()
        {
            var settings = Config.Settings;

           // new DriverManager().SetUpDriver(new ChromeConfig());

            var options = new ChromeOptions();
            if (settings.Headless)
            {
                options.AddArgument("--headless=new"); //Runs chrome in headless mode
            }
            options.AddArgument("--disable-notifications"); // disables notifications
            options.AddArgument("--no-sandbox");
            options.AddArgument("--disable-dev-shm-usage"); // overcome limited temprory memory problems
            options.AddArgument("--disable-gpu"); // disables GPU acceleration
            options.AddArgument("--window-size=1920,1080");

            //IWebDriver driver = new ChromeDriver(options);
            var service = ChromeDriverService.CreateDefaultService();
 
            service.EnableVerboseLogging = true;
            service.LogPath = "chromedriver.log";

            IWebDriver driver = new ChromeDriver(service, options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(settings.ImplicitWaitSeconds);
           // driver.Manage().Window.Maximize();

            return driver;
        }
    }
}