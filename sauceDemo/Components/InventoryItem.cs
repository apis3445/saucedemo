using Microsoft.Playwright;

namespace sauceDemo.Components
{
    public class InventoryItem : Item
    {
        private string _image;

        public InventoryItem(IElementHandle element, string type): base(element, type)
        {
            _image = $"img.inventory_{type}_img";
        }

        /// <summary>
        /// Image for the item
        /// </summary>
        public string Image => element.QuerySelectorAsync(_image).Result.GetAttributeAsync("src").Result;

    }
}
