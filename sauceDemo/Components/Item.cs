using System.Threading.Tasks;
using Microsoft.Playwright;

namespace sauceDemo.Components
{
    public class Item
    {
        protected IElementHandle element;

        private string name ;
        private string description;
        private string price;
        private string button;


        public Item(IElementHandle element, string type)
        {
            this.element = element;
            name = $"div.inventory_{type}_name";
            description = $"div.inventory_{type}_desc";
            price = $"div.inventory_{type}_price";
            button = "button.btn_inventory";
        }

        
        public string FormatedPrice => element.QuerySelectorAsync(price).Result.TextContentAsync().Result;

        public string Name => element.QuerySelectorAsync(name).Result.TextContentAsync().Result;

        public string Description => element.QuerySelectorAsync(description).Result.TextContentAsync().Result;

        public decimal Price => decimal.Parse(FormatedPrice.Replace("$", ""));

        public IElementHandle Button => element.QuerySelectorAsync(button).Result;

        public async Task ClickButtonAsync()
        {
            await Button.ClickAsync();
        }

    }
}
