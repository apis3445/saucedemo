using System.Threading.Tasks;
using Microsoft.Playwright;

namespace sauceDemo.Components
{
    public class Item
    {
        protected IElementHandle element;

        private string _name;
        private string _description;
        private string _price;
        private string _cartButton;

        public Item(IElementHandle element, string type)
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
        public string FormatedPrice => element.QuerySelectorAsync(_price).Result.TextContentAsync().Result;

        /// <summary>
        /// Item's name
        /// </summary>
        public string Name => element.QuerySelectorAsync(_name).Result.TextContentAsync().Result;

        /// <summary>
        /// Item's Description
        /// </summary>
        public string Description => element.QuerySelectorAsync(_description).Result.TextContentAsync().Result;

        /// <summary>
        /// Price
        /// </summary>
        public decimal Price => decimal.Parse(FormatedPrice.Replace("$", ""));

        /// <summary>
        /// Button for the item
        /// </summary>
        public IElementHandle CartButton => element.QuerySelectorAsync(_cartButton).Result;

        /// <summary>
        /// Clic
        /// </summary>
        /// <returns></returns>
        public async Task ClickButtonAsync()
        {
            await CartButton.ClickAsync();
        }

    }
}