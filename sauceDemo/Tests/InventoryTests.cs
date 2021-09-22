using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using sauceDemo.Base;
using sauceDemo.Pages;

namespace sauceDemo.Tests
{
    [TestClass]
    public class InventoryTests
    {
        private IPage page;
        LoginPage loginPage;
        InventoryPage inventoryPage;

        private string fixItem = "Sauce Labs Onesie";

        [TestInitialize]
        public async Task Setup()
        {
            PlaywrightDriver playwrightDriver = new PlaywrightDriver();
            page = await playwrightDriver.InitalizePlaywright();
            loginPage = new LoginPage(page);
            await loginPage.Goto();
            await page.Context.ClearCookiesAsync();
            await loginPage.LoginAsync(Constants.STANDARD_USER, Constants.GENERIC_PASSWORD);
            
            inventoryPage = new InventoryPage(page);
        }

        [TestMethod]      
        public void SortProducts_ByLowPrice_SortByLowestPrice()
        {
            //Arrange
            var comparer = new ItemComparer();
            var items = inventoryPage.Items;
            var itemsByPriceDes = items.OrderBy(i => i.Price).ToList(); 
            //Act
            inventoryPage.SetSort("lohi");
            _ = page.WaitForSelectorAsync("div.inventory_list").Result;
            items = inventoryPage.Items;
            //Assert
            Assert.IsTrue(Enumerable.SequenceEqual(itemsByPriceDes, items, comparer));
        }

        [TestMethod]
        public async Task AddItems_FromInventory_ShouldAllItemsAddedToShoppingCart()
        {
            //Arrange
            int total = 3;
            await inventoryPage.AddItemsAsync(total);
            //Assert
            CartPage cartPage = new CartPage(page);
            cartPage.CheckItemsInCart(total);
            await inventoryPage.shopingCartIcon.ClickAsync();
            for (int i = 0; i < total; i++)
            {
                cartPage.CheckCartItem(inventoryPage.itemsName[i]);
            }
        }

        ///Option 1 
        [TestMethod]
        public async Task AddItem_FromInventoryItem_AddItemToShoppingCartAsync()
        {
            //Arrange
            //Act
            await inventoryPage.AddToCartByNameAsync(fixItem);
            InventoryItemPage inventoryItemPage = new InventoryItemPage(page);
            await inventoryItemPage.Item.ClickButtonAsync();
            var name = inventoryItemPage.Item.Name;
            //Assert
            Assert.AreEqual(fixItem, name);
            Assert.AreEqual("Remove", await inventoryItemPage.Item.Button.TextContentAsync());
            await inventoryPage.shopingCartIcon.ClickAsync();
            CartPage cartPage = new CartPage(page);
            cartPage.CheckItemsInCart(1);
            cartPage.CheckCartItem(fixItem);
        }

        ///Option 2
        [TestMethod]
        public async Task AddItem_FromButtonText_AddItemToShoppingCartAsync()
        {
            //Arrange
            //Act
            await inventoryPage.AddToCartByIdNameAsync(fixItem);
            await inventoryPage.shopingCartIcon.ClickAsync();
            //Assert
            CartPage cartPage = new CartPage(page);
            cartPage.CheckItemsInCart(1);
            cartPage.CheckCartItem(fixItem);
        }

       

    }
}
