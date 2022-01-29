using System.Threading.Tasks;
using Microsoft.Playwright;
using NUnit.Framework;
using sauceDemo.Base;
using sauceDemo.Pages;

namespace sauceDemo.Tests
{
    [Parallelizable]
    public class ShopTests : BaseTest
    {

        [Test, Category("Shop")]
        public async Task AddItemsToShop_CompletePurchsse_ShouldShowsConfirmationPageAsync()
        {
            //Arrange
            int totalItems = 2;
            await inventoryPage.AddItemsAsync(totalItems);
            //Act
            CartPage cartPage = new CartPage(page);
            await inventoryPage.ClickShoppingCartBadgeAsync();
            await cartPage.ClickCheckoutAsync();
            CheckoutStep1Page checkoutStep1 = new CheckoutStep1Page(page);
            await checkoutStep1.SetFirstNameAsync("Abigail");
            await checkoutStep1.SetLastNameAsync("Armijo");
            await checkoutStep1.SetPostalCodeAsync("27140");
            await checkoutStep1.ClickContinueAsync();
            CheckoutStep2Page checkoutStep2 = new CheckoutStep2Page(page);
            //Assert
            Assert.AreEqual(totalItems, checkoutStep2.ItemsInShoppingCart, "Items in the cart are different");
            for (int i = 0; i < totalItems; i++)
            {
                checkoutStep2.ListCartItems.CheckCartItem(inventoryPage.ItemsName[i]);
            }
            await checkoutStep2.CickFinishAsync();
            //Additional assert to check complete
            CheckoutCompletePage checkoutComplete = new CheckoutCompletePage(page);
            Assert.AreEqual("THANK YOU FOR YOUR ORDER", checkoutComplete.Thanks, "Thanks message for the order is not visible");
        }
    }
}