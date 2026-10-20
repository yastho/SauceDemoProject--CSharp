using NUnit.Framework;
using Reqnroll;
using SauceDemoProject.Tests.Hooks;
using SauceDemoProject.Tests.PageObject;

namespace SauceDemoProject.Tests.StepDefinitions
{
    [Binding]
    public class CartSteps
    {
        private readonly InventoryPage _inventoryPage;

        public CartSteps()
        {
            _inventoryPage = new InventoryPage(DriverHooks.Driver);
        }

        [When(@"I add ""(.*)"" to the cart")]
        public void WhenIAddToTheCart(string productName)
        {
            _inventoryPage.AddItemToCart(productName);
        }

        [Then(@"the cart badge should show (.*)")]
        public void ThenTheCartBadgeShouldShow(int expectedCount)
        {
            Assert.That(_inventoryPage.GetCartCount(), Is.EqualTo(expectedCount));
        }
    }
}