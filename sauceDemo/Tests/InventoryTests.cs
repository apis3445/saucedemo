﻿using System.Linq;
using System.Threading.Tasks;
using Microsoft.Playwright;
using NUnit.Framework;
using sauceDemo.Base;
using sauceDemo.Pages;

namespace sauceDemo.Tests
{
    [Parallelizable]
    public class InventoryTests
    {
        private IPage _page;
        private LoginPage _loginPage;
        private InventoryPage _inventoryPage;
        private string _fixItem = "Sauce Labs Onesie";
        

        [SetUp]
        public async Task Setup()
        {
            PlaywrightDriver playwrightDriver = new PlaywrightDriver();
            _page = await playwrightDriver.InitalizePlaywright();
            _loginPage = new LoginPage(_page);
            await _loginPage.GotoAsync();
            await _loginPage.LoginAsync(Constants.STANDARD_USER, Constants.GENERIC_PASSWORD);            
            _inventoryPage = new InventoryPage(_page);
        }

        [Test, Category("Inventory")]
        public async Task SortProducts_ByLowToHighPrice_SortByLowestPriceAsync()
        {
            //Arrange
            var comparer = new ItemComparer();
            var items = _inventoryPage.Items;
            var itemsByPriceDes = items.OrderBy(i => i.Price).ToList(); 
            //Act
            await _inventoryPage.SetSortAsync("lohi");
            items = _inventoryPage.Items;
            //Assert
            Assert.IsTrue(Enumerable.SequenceEqual(itemsByPriceDes, items, comparer),"Items are not sorted by price low to hi");
        }

        [Test, Category("Inventory")]
        public async Task AddItems_FromInventory_ShouldAddItemsToShoppingCart()
        {
            //Arrange
            int total = 3;
            await _inventoryPage.AddItemsAsync(total);
            //Assert
            CartPage cartPage = new CartPage(_page);
            cartPage.CheckItemsInCart(total);
            await _inventoryPage.ClickShoppingCartBadgeAsync();
            for (int i = 0; i < total; i++)
            {
                cartPage.CartItems.CheckCartItem(_inventoryPage.ItemsName[i]);
            }
        }

        /// <summary>
        /// Add the specific product ‘Sauce Labs Onesie’ to the shopping cart
        /// </summary>
        /// <returns></returns>
        /// <remarks>Option 1 by name</remarks>
        [Test, Category("Inventory")]
        public async Task AddProduct_WithSpecificName_ShouldAddProductToShoppingCartAsync()
        {
            //Arrange
            //Act
            await _inventoryPage.AddToCartByNameAsync(_fixItem);
            InventoryItemPage inventoryItemPage = new InventoryItemPage(_page);
            await inventoryItemPage.Item.ClickCartButtonAsync();
            var name = inventoryItemPage.Item.Name;
            //Assert
            Assert.AreEqual(_fixItem, name, "Item name in cart is different");
            Assert.AreEqual("Remove", await inventoryItemPage.Item.CartButton.TextContentAsync(), "Cart button doesn't show Remove text");
            await _inventoryPage.ClickShoppingCartBadgeAsync();
            CartPage cartPage = new CartPage(_page);
            cartPage.CheckItemsInCart(1);
            cartPage.CartItems.CheckCartItem(_fixItem);
        }

        /// <summary>
        /// Add the specific product ‘Sauce Labs Onesie’ to the shopping cart
        /// </summary>
        /// <returns></returns>
        /// <remarks>Option 2 by DataTest</remarks>
        [Test, Category("Inventory")]
        public async Task AddProduct_FromButtonText_ShouldAddProductToShoppingCartAsync()
        {
            //Arrange
            //Act
            await _inventoryPage.AddToCartByDataTestNameAsync(_fixItem);
            await _inventoryPage.ClickShoppingCartBadgeAsync();
            //Assert
            CartPage cartPage = new CartPage(_page);
            cartPage.CheckItemsInCart(1);
            cartPage.CartItems.CheckCartItem(_fixItem);
        }

        /// <summary>
        /// Add the specific product ‘Sauce Labs Onesie’ to the shopping cart
        /// </summary>
        /// <returns></returns>
        /// <remarks>Option 3 by url</remarks>
        [Test, Category("Inventory")]
        public async Task AddProduct_FromUrl_ShouldAddProductToShoppingCartAsync()
        {
            //Arrange
            InventoryItemPage inventoryItemPage = new InventoryItemPage(_page);
            int itemId = 2; //Sauce Labs Onesie has id = 2
            //Go to the page of the product with direct link
            await inventoryItemPage.GotoAsync(itemId);
            //Act
            await inventoryItemPage.Item.ClickCartButtonAsync();
            await _inventoryPage.ClickShoppingCartBadgeAsync();
            //Assert
            CartPage cartPage = new CartPage(_page);
            cartPage.CheckItemsInCart(1);
            cartPage.CartItems.CheckCartItem(_fixItem);
        }
    }
}
