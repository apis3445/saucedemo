using System.Threading.Tasks;
using Microsoft.Playwright;

namespace sauceDemo.Components;

public class Item
{
    protected ILocator element;

    private string _name;
    private string _description;
    private string _price;
    private string _cartButton;

    public Item(ILocator element, string type)
    {
        this.element = element;
        _name = $"div.inventory_{type}_name";
        _description = $"div.inventory_{type}_desc";
        _price = $"div.inventory_{type}_price";
        _cartButton = "button.btn_inventory";
    }

    /// <summary>
    /// Formatted Price 
    /// </summary>
    public string FormatedPrice => element.Locator(_price).TextContentAsync().Result;

    /// <summary>
    /// Item's name
    /// </summary>
    public string Name => element.Locator(_name).TextContentAsync().Result;

    /// <summary>
    /// Item's Description
    /// </summary>
    public string Description => element.Locator(_description).TextContentAsync().Result;

    /// <summary>
    /// Price
    /// </summary>
    public decimal Price => decimal.Parse(FormatedPrice.Replace("$", ""));

    /// <summary>
    /// Button for the item
    /// </summary>
    public ILocator CartButton => element.Locator(_cartButton);

    /// <summary>
    /// Clic
    /// </summary>
    /// <returns></returns>
    public async Task ClickCartButtonAsync()
    {
        await CartButton.ClickAsync();
    }

}