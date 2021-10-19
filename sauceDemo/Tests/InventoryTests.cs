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
        private IPage _page;
        private LoginPage _loginPage;
        private InventoryPage _inventoryPage;
        private string _fixItem = "Sauce Labs Onesie";

        [TestInitialize]
        public async Task Setup()
        {
            PlaywrightDriver playwrightDriver = new PlaywrightDriver();
            _page = await playwrightDriver.InitalizePlaywright();
            _loginPage = new LoginPage(_page);
            await _loginPage.Goto();
            await _loginPage.LoginAsync(Constants.STANDARD_USER, Constants.GENERIC_PASSWORD);
            
            _inventoryPage = new InventoryPage(_page);
        }

        /// <summary>
        /// As a best practice and with custom code snippet
        //If you want to use this format I attached a code snippet to create the unit test with shortcuts.
        //Unzip the files and copy to
        //%USERPROFILE%\Documents\Visual Studio 2019\Code Snippets\Visual C#\My Code Snippets
        ///After you can write uat from VS and press tab to get a template for async test
        ///[TestMethod]
        //public async Task UoW_InitialCondition_ExpectedResult()
        //{
            //Arrange

            //Act

            //Assert

        //}
        //or ut for sync test case
        /// UoW is unit of work
        /// </summary>
        [TestMethod]      
        public void SortProducts_ByLowPrice_SortByLowestPrice()
        {
            //Arrange
            var comparer = new ItemComparer();
            var items = _inventoryPage.Items;
            var itemsByPriceDes = items.OrderBy(i => i.Price).ToList(); 
            //Act
            _inventoryPage.SetSort("lohi");
            _ = _page.WaitForSelectorAsync("div.inventory_list").Result;
            items = _inventoryPage.Items;
            //Assert
            Assert.IsTrue(Enumerable.SequenceEqual(itemsByPriceDes, items, comparer));
        }

        [TestMethod]
        public async Task AddItems_FromInventory_ShouldAllItemsAddedToShoppingCart()
        {
            //Arrange
            int total = 3;
            await _inventoryPage.AddItemsAsync(total);
            //Assert
            CartPage cartPage = new CartPage(_page);
            cartPage.CheckItemsInCart(total);
            await _inventoryPage.ShopingCartIcon.ClickAsync();
            for (int i = 0; i < total; i++)
            {
                cartPage.CheckCartItem(_inventoryPage.ItemsName[i]);
            }
        }

        ///Option 1 
        [TestMethod]
        public async Task AddItem_FromInventoryItem_AddItemToShoppingCartAsync()
        {
            //Arrange
            //Act
            await _inventoryPage.AddToCartByNameAsync(_fixItem);
            InventoryItemPage inventoryItemPage = new InventoryItemPage(_page);
            await inventoryItemPage.Item.ClickButtonAsync();
            var name = inventoryItemPage.Item.Name;
            //Assert
            Assert.AreEqual(_fixItem, name);
            Assert.AreEqual("Remove", await inventoryItemPage.Item.Button.TextContentAsync());
            await _inventoryPage.ShopingCartIcon.ClickAsync();
            CartPage cartPage = new CartPage(_page);
            cartPage.CheckItemsInCart(1);
            cartPage.CheckCartItem(_fixItem);
        }

        ///Option 2
        [TestMethod]
        public async Task AddItem_FromButtonText_AddItemToShoppingCartAsync()
        {
            //Arrange
            //Act
            await _inventoryPage.AddToCartByDataTestNameAsync(_fixItem);
            await _inventoryPage.ShopingCartIcon.ClickAsync();
            //Assert
            CartPage cartPage = new CartPage(_page);
            cartPage.CheckItemsInCart(1);
            cartPage.CheckCartItem(_fixItem);
        }
    }
}
