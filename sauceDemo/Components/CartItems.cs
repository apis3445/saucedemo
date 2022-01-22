using System.Collections.Generic;
using Microsoft.Playwright;

namespace sauceDemo.Components
{
    public class CartItems
    {
        private IPage _page;

        public CartItems(IPage page)
        {
            this._page = page;
        }

        public List<CartItem> Items
        {
            get
            {
                var listCartItems = new List<CartItem>();
                var inventoryItems = this._page.QuerySelectorAllAsync("div.cart_item").Result;
                foreach (var item in inventoryItems)
                {
                    CartItem cartItem = new CartItem(item, "item");
                    listCartItems.Add(cartItem);
                }
                return listCartItems;
            }
        }
    }
}
