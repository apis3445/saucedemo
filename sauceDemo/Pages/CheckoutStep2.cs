using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using sauceDemo.Components;

namespace sauceDemo.Pages
{
    public class CheckoutStep2 : BasePage
    {
        //TODO: Add summary info items

        private string finishButtonLocator = "data-test=finish";

        public CheckoutStep2(IPage page) : base(page)
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

        public void CheckCartItem(string item)
        {
            var cartItem = Items.Find(i => i.Name == item);
            Assert.AreEqual(1, cartItem.Quantity);
            Assert.AreEqual(item, cartItem.Name);
        }

        public async Task CickFinishAsync()
        {
            await Page.ClickAsync(finishButtonLocator);
            await TakeScreenShootAsync("finishClick");
        }
    }
}
