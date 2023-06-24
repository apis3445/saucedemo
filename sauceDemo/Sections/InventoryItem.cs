using Microsoft.Playwright;

namespace sauceDemo.Components;

public class InventoryItem : Item
{
    private string _image;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="element">Main element</param>
    /// <param name="type">Type of the image</param>
    public InventoryItem(ILocator element, string type): base(element, type)
    {
        _image = $"img.inventory_{type}_img";
    }

    /// <summary>
    /// Image for the item
    /// </summary>
    public string Image => element.Locator(_image).GetAttributeAsync("src").Result;

}
