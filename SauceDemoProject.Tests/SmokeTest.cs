// using NUnit.Framework;
// using OpenQA.Selenium;
// using SauceDemoProject.Tests.Helper;
// using SauceDemoProject.Tests.PageObject;

// namespace SauceDemoProject.Tests
// {
//     public class LoginTests
//     {
//         private IWebDriver _driver = null!;
//         private LoginPage _loginPage = null!;

//         [SetUp]
//         public void Setup()
//         {
//             _driver = DriverFactory.CreateDriver();
//             _loginPage = new LoginPage(_driver);
//             _loginPage.NavigateTo(Config.Settings.BaseUrl);
//         }

//         [Test]
//         public void StandardUser_CanLogIn_AndReachInventoryPage()
//         {
//             _loginPage.Login("standard_user", "secret_sauce");

//             Assert.That(_driver.Url, Does.Contain("inventory.html"));
//         }

//         [Test]
//         public void LockedOutUser_SeesErrorMessage()
//         {
//             _loginPage.Login("locked_out_user", "secret_sauce");

//             Assert.That(_loginPage.IsErrorDisplayed(), Is.True);
//             Assert.That(_loginPage.GetErrorMessage(), Does.Contain("locked out"));
//         }

//         [TearDown]
//         public void Teardown()
//         {
//             _driver.Quit();
//             _driver.Dispose();
//         }
//     }
// }
