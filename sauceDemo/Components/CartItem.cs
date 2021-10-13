using Microsoft.Playwright;

namespace sauceDemo.Components
{
    public class CartItem: Item
    {
        private string quantity = "div.cart_quantity";

        public CartItem(IElementHandle element, string type) : base(element, type)
        {
            
        }

        public decimal Quantity => int.Parse(element.QuerySelectorAsync(quantity).Result.TextContentAsync().Result);

    }
}