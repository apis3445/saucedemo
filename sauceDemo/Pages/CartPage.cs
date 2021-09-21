using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using sauceDemo.Components;

namespace sauceDemo.Pages
{
    public class CartPage : BasePage
    {
        private string continueShoppingButton = "data-test=continue-shopping";
        private string checkoutButton = "data-test=checkout";

        public CartPage(IPage page) : base(page)
        {
        }

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

        public async Task ClickCheckoutAsync()
        {
            await Page.ClickAsync(checkoutButton);
            await Page.ScreenshotAsync(new PageScreenshotOptions { Path = "Checkout.png" });
        }

        public async Task ClickContinueShopping()
        {
            await Page.ClickAsync(continueShoppingButton);
            await Page.ScreenshotAsync(new PageScreenshotOptions { Path = "ContinueShopping.png" });
        }

        public void CheckCartItem(string item)
        {
            var cartItem = Items.Find(i => i.Name == item);
            Assert.AreEqual(1, cartItem.Quantity);
            Assert.AreEqual(item, cartItem.Name);
        }

        public void CheckItemsInCart(int total)
        {
            Assert.AreEqual(total, ItemsInShoppingCart);
        }
    }
}
