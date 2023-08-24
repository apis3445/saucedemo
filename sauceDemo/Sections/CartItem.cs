using Microsoft.Playwright;

namespace sauceDemo.Components;

public class CartItem: Item
{
    private string _quantity = "div.cart_quantity";

    public CartItem(ILocator element, string type) : base(element, type)
    {
    }

    /// <summary>
    /// Quantity for the item
    /// </summary>
    public decimal Quantity => int.Parse(element.Locator(_quantity).TextContentAsync().Result);

}