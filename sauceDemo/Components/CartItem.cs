using System.Collections.Generic;
using Microsoft.Playwright;

namespace sauceDemo.Components
{
    public class CartItem: Item
    {
        private string _quantityLocator = "div.cart_quantity";

        public CartItem(IElementHandle element, string type) : base(element, type)
        {
            
        }

        /// <summary>
        /// Quantity for the item
        /// </summary>
        public decimal Quantity => int.Parse(element.QuerySelectorAsync(_quantityLocator).Result.TextContentAsync().Result);

    }
}