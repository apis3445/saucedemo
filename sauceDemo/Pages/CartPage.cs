using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Playwright;
using NUnit.Framework;
using sauceDemo.Components;

namespace sauceDemo.Pages
{
    public class CartPage : BasePage
    {
        private ILocator _checkoutButton;
        private ILocator _continueShoppingButton;

        public CartPage(IPage page) : base(page)
        {
            _checkoutButton = page.Locator("data-test=checkout");
            _continueShoppingButton = page.Locator("data-test=continue-shopping");
        }

        /// <summary>
        /// Reeturns the list of items. Is list due to can be dynamic, sometimes can return 5, another 15 or higher.
        /// </summary>
        public List<CartItem> Items
        {
            get
            {
                var listCartItems = new CartItems(this.Page);
                return listCartItems.Items;
            }
        }

        //Async due to the framework is async
        public async Task ClickCheckoutAsync()
        {
            await _checkoutButton.ClickAsync();
            //Added screenshot maybe check environment variabl to save in local development or connect
            //to third party report tool like report portal
           await TakeScreenShootAsync("Checkout");
        }


        /// <summary>
        /// Click in continue shopping
        /// </summary>
        /// <returns></returns>
        public async Task ClickContinueShoppingAsync()
        {
            await _continueShoppingButton.ClickAsync();
            await TakeScreenShootAsync("ContinueShopping");
        }

        /// <summary>
        /// Check item to reuse in different test cases
        /// </summary>
        /// <param name="item">item to check</param>
        public void CheckCartItem(string item)
        {
            var cartItem = Items.Find(i => i.Name == item);
            Assert.AreEqual(1, cartItem.Quantity);
            Assert.AreEqual(item, cartItem.Name);
        }

        /// <summary>
        /// Check total of items to reuse in different test cases
        /// </summary>
        /// <param name="total">Total the items in the cart</param>
        public void CheckItemsInCart(int total)
        {
            Assert.AreEqual(total, ItemsInShoppingCart);
        }
    }
}
