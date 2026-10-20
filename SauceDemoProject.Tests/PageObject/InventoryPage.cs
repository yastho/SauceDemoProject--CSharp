using OpenQA.Selenium;

namespace SauceDemoProject.Tests.PageObject
{
    public class InventoryPage : BasePage
    {
        private readonly By _pageTitle = By.ClassName("title");
        private readonly By _cartBadge = By.ClassName("shopping_cart_badge");

        public InventoryPage(IWebDriver driver) : base(driver) { }

        public bool IsLoaded()
        {
            return GetText(_pageTitle) == "Products";
        }

        public void AddItemToCart(string productName)
        {
            var id = "add-to-cart-" + productName.ToLower().Replace(" ", "-");
            Click(By.Id(id));
        }

        public int GetCartCount()
        {
            var badges = Driver.FindElements(_cartBadge);
            return badges.Count == 0 ? 0 : int.Parse(badges[0].Text);
        }
    }
}