using NUnit.Framework;
using Reqnroll;
using SauceDemoProject.Tests.Helper;
using SauceDemoProject.Tests.Hooks;
using SauceDemoProject.Tests.PageObject;

namespace SauceDemoProject.Tests.StepDefinitions
{
    [Binding]
    public class LoginStep
    {
        private LoginPage _loginPage = null!;

        [Given(@"I am on the SauceDemo login page")]
        public void GivenIAmOnTheSauceDemoLoginPage()
        {
            _loginPage = new LoginPage(DriverHooks.Driver);
            _loginPage.NavigateTo(Config.Settings.BaseUrl);
        }

        [When(@"I log in with username ""(.*)"" and password ""(.*)""")]
        public void WhenILogInWithUsernameAndPassword(string username, string password)
        {
            _loginPage.Login(username, password);
        }

        [Then(@"I should be redirected to the inventory page")]
        public void ThenIShouldBeRedirectedToTheInventoryPage()
        {
            Assert.That(DriverHooks.Driver.Url, Does.Contain("inventory.html"));
        }

        [Then(@"I should see an error message containing ""(.*)""")]
        public void ThenIShouldSeeAnErrorMessageContaining(string expectedText)
        {
            Assert.That(_loginPage.IsErrorDisplayed(), Is.True);
            Assert.That(_loginPage.GetErrorMessage(), Does.Contain(expectedText));
        }
    }
}