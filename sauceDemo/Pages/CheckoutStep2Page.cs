using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Playwright;
using NUnit.Framework;
using sauceDemo.Components;

namespace sauceDemo.Pages
{
    public class CheckoutStep2Page : BasePage
    {
        //TODO: Add summary info items

        private ILocator _finishButton;

        public CheckoutStep2Page(IPage page) : base(page)
        {
            _finishButton = page.Locator("data-test=finish");
        }

        /// <summary>
        /// Cart Items
        /// </summary>
        public List<CartItem> Items
        {
            get
            {
                var listCartItems = new CartItems(this.Page);
                return listCartItems.Items;
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
            await _finishButton.ClickAsync();
            await TakeScreenShootAsync("finishClick");
        }
    }
}
