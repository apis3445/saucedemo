using System.Threading.Tasks;
using Microsoft.Playwright;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using sauceDemo.Pages;

namespace sauceDemo.Tests
{
    [TestClass]
    public class ShopTests
    {
        private IBrowser browser;
        private IPage page;
        LoginPage loginPage;
        InventoryPage inventoryPage;

        [TestInitialize]
        public async Task Setup()
        {
            var playwright = await Playwright.CreateAsync();
            browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
            page = await browser.NewPageAsync();
            loginPage = new LoginPage(page);
            await loginPage.Goto();
            await loginPage.LoginAsync(Constants.STANDARD_USER, Constants.GENERIC_PASSWORD);
            inventoryPage = new InventoryPage(page);
        }

        [TestMethod]
        public async Task AddItems_FromInventory_ShouldAllItemsAddedToShoppingCart()
        {
            //Arrange
            int total = 2;
            await inventoryPage.AddItemsAsync(total);
            //Act
            CartPage cartPage = new CartPage(page);
            await inventoryPage.shopingCartIcon.ClickAsync();
            await cartPage.ClickCheckoutAsync();
            CheckoutStep1 checkoutStep1 = new CheckoutStep1(page);
            await checkoutStep1.SetFirstNameAsync("Abigail");
            await checkoutStep1.SetLastNameAsync("Armijo");
            await checkoutStep1.SetPostalCodeAsync("27140");
            await checkoutStep1.ClickContinueAsync();
            CheckoutStep2 checkoutStep2 = new CheckoutStep2(page);
            //Assert
            Assert.AreEqual(total, checkoutStep2.ItemsInShoppingCart);
            for (int i = 0; i < total; i++)
            {
                checkoutStep2.CheckCartItem(inventoryPage.itemsName[i]);
            }
            await checkoutStep2.CickFinishAsync();
            //Additional assert to check complete
            CheckoutComplete checkoutComplete = new CheckoutComplete(page);
            Assert.AreEqual("THANK YOU FOR YOUR ORDER", checkoutComplete.Thanks);
        }
    }
}