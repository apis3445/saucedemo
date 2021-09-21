using Microsoft.Playwright;

namespace sauceDemo.Components
{
    public class InventoryItem : Item
    {
        private string image;

        public InventoryItem(IElementHandle element, string type): base(element, type)
        {
            image = $"img.inventory_{type}_img";
        }

        public string Image => element.QuerySelectorAsync(image).Result.GetAttributeAsync("src").Result;

    }
}
