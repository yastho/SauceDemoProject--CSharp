using OpenQA.Selenium;

namespace SauceDemoProject.Tests.PageObject
{
    public class LoginPage : BasePage
    {
        private readonly By _usernameInput = By.Id("user-name");
        private readonly By _passwordInput = By.Id("password");
        private readonly By _loginButton = By.Id("login-button");
        private readonly By _errorMessage = By.CssSelector("[data-test='error']");

        public LoginPage(IWebDriver driver) : base(driver) { }

        public void NavigateTo(string url)
        {
            Driver.Navigate().GoToUrl(url);
        }

        public void Login(string username, string password)
        {
            Type(_usernameInput, username);
            Type(_passwordInput, password);
            Click(_loginButton);
        }

        public bool IsErrorDisplayed()
        {
            return Driver.FindElements(_errorMessage).Count > 0;
        }

        public string GetErrorMessage()
        {
            return GetText(_errorMessage);
        }
    }
}