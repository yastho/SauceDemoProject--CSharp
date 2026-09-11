using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace SauceDemoProject.Tests.PageObject
{
    public abstract class BasePage
    {
        protected readonly IWebDriver Driver;
        private readonly WebDriverWait _wait;

        protected BasePage(IWebDriver driver)
        {
            Driver = driver;
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        protected IWebElement WaitAndFind(By locator)
        {
            return _wait.Until(d => d.FindElement(locator));
        }

        protected IWebElement WaitUntilClickable(By locator)
        {
            return _wait.Until(d =>
            {
                var element = d.FindElement(locator);
                return (element.Displayed && element.Enabled) ? element : null;
            });
        }
        protected void Click(By locator)
        {
            WaitUntilClickable(locator).Click();
        }

      protected void Type(By locator, string text)
        {
            var element = WaitAndFind(locator);
            element.Clear();
            element.SendKeys(text);
        }

        protected string GetText(By locator)
        {
            return WaitAndFind(locator).Text;
        }
    }
}