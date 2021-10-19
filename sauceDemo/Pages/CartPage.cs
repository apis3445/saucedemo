using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using sauceDemo.Components;

namespace sauceDemo.Pages
{
    public class CartPage : BasePage
    {
        //Added data-test in private to reuse to click or get test if is needed check some
        //translations in the future.
        private string _continueShoppingButtonLocator = "data-test=continue-shopping";
        private string _checkoutButtonLocator = "data-test=checkout";
        
        public CartPage(IPage page) : base(page)
        {
        }

        /// <summary>
        /// Reeturns the list of items. Is list due to can be dynamic, sometimes can return 5, another 15 or higher.
        /// </summary>
        public List<CartItem> Items
        {
            get
            {
                var listCartItems = new List<CartItem>();
                var inventoryItems = Page.QuerySelectorAllAsync("div.cart_item").Result;
                foreach (var item in inventoryItems)
                {
                    CartItem cartItem = new CartItem(item, "item");
                    listCartItems.Add(cartItem);
                }
                return listCartItems;
            }
        }

        //Async due to the framework is async
        public async Task ClickCheckoutAsync()
        {
            await Page.ClickAsync(_checkoutButtonLocator);
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
            await Page.ClickAsync(_continueShoppingButtonLocator);
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
