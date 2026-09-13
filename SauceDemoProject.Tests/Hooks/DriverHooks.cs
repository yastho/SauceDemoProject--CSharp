using OpenQA.Selenium;
using Reqnroll;
using SauceDemoProject.Tests.Helper;

namespace SauceDemoProject.Tests.Hooks
{
    [Binding]
    public class DriverHooks
    {
        public static IWebDriver Driver { get; private set; } = null!;

        [BeforeScenario]
        public void InitializeDriver()
        {
            Driver = DriverFactory.CreateDriver();
        }

       [AfterScenario]
    public void QuitDriver()
     {
       Driver?.Quit();
       Driver?.Dispose();
      }
    }
}