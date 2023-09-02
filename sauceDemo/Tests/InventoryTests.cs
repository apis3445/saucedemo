using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using sauceDemo.Base;
using sauceDemo.Components;
using sauceDemo.Pages;

namespace sauceDemo.Tests;

[Parallelizable]
public class InventoryTests : BaseTest
{
    
    private string _fixItem = "Sauce Labs Onesie";
    
    [Test, Category("Inventory")]
    public async Task SortProducts_ByLowToHighPrice_SortByLowestPriceAsync()
    {
        //Arrange
        var comparer = new ItemComparer();
        var items = inventoryPage.Items;
        var itemsByPriceDes = items.OrderBy(i => i.Price).ToList();
        var itemsByPrice = new ItemsByPrice(itemsByPriceDes);

        //Act
        await inventoryPage.SetSortAsync("lohi");
        var itemsAfterSort = inventoryPage.Items;
        var currentItems = new ItemsByPrice(itemsAfterSort);

        //Assert
        Assert.IsTrue(Enumerable.SequenceEqual(itemsByPrice.Items, currentItems.Items, comparer),"Items are not sorted by price low to hi");
    }

    [Test, Category("Inventory")]
    public async Task AddItems_FromInventory_ShouldAddItemsToShoppingCart()
    {
        //Arrange
        int total = 3;
        await inventoryPage.AddItemsAsync(total);
        //Assert
        CartPage cartPage = new CartPage(page);
        cartPage.CheckItemsInCart(total);
        await inventoryPage.ClickShoppingCartBadgeAsync();
        for (int i = 0; i < total; i++)
        {
            cartPage.CartItems.CheckCartItem(inventoryPage.ItemsName[i]);
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
        await inventoryPage.AddToCartByNameAsync(_fixItem);
        InventoryItemPage inventoryItemPage = new InventoryItemPage(page);
        await inventoryItemPage.Item.ClickCartButtonAsync();
        var name = inventoryItemPage.Item.Name;
        //Assert
        inventoryItemPage.AssertEqual(_fixItem, name, "Check Item name in cart is: " + _fixItem);
        inventoryItemPage.AssertEqual("Remove", await inventoryItemPage.Item.CartButton.TextContentAsync(), "Check Cart button shows Remove text");
        await inventoryPage.ClickShoppingCartBadgeAsync();
        CartPage cartPage = new CartPage(page);
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
        await inventoryPage.AddToCartByDataTestNameAsync(_fixItem);
        await inventoryPage.ClickShoppingCartBadgeAsync();
        //Assert
        CartPage cartPage = new CartPage(page);
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
        InventoryItemPage inventoryItemPage = new InventoryItemPage(page);
        int itemId = 2; //Sauce Labs Onesie has id = 2
        //Go to the page of the product with direct link
        await inventoryItemPage.GotoAsync(itemId);
        //Act
        await inventoryItemPage.Item.ClickCartButtonAsync();
        await inventoryPage.ClickShoppingCartBadgeAsync();
        //Assert
        CartPage cartPage = new CartPage(page);
        cartPage.CheckItemsInCart(1);
        cartPage.CartItems.CheckCartItem(_fixItem);
    }
}
