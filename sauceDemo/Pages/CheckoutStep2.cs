﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Playwright;
using NUnit.Framework;
using sauceDemo.Components;

namespace sauceDemo.Pages
{
    public class CheckoutStep2 : BasePage
    {
        //TODO: Add summary info items

        private string _finishButtonLocator = "data-test=finish";

        public CheckoutStep2(IPage page) : base(page)
        {
        }


        /// <summary>
        /// Items
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

        /// <summary>
        /// Check item in the cart
        /// </summary>
        /// <param name="item">Item to check</param>
        public void CheckCartItem(string item)
        {
            var cartItem = Items.Find(i => i.Name == item);
            Assert.AreEqual(1, cartItem.Quantity);
            Assert.AreEqual(item, cartItem.Name);
        }

        /// <summary>
        /// Click in finish button
        /// </summary>
        /// <returns></returns>
        public async Task CickFinishAsync()
        {
            await Page.ClickAsync(_finishButtonLocator);
            await TakeScreenShootAsync("finishClick");
        }
    }
}
