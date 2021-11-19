using System.Threading.Tasks;
using Microsoft.Playwright;
using sauceDemo.Components;

namespace sauceDemo.Pages
{
    public class InventoryItemPage : BasePage
    {
        private string addToCartButtonLocator = "button[data-test^=add-to-cart]";
        public InventoryItemPage(IPage page) : base(page)
        {
        }

        public async Task ClickAddToCartAsync()
        {
            await Page.ClickAsync(addToCartButtonLocator);
            await TakeScreenShootAsync("AddToCartInventory");
        }

        public async Task GotoAsync(int id) => await Page.GotoAsync(Initialize.BaseAddress + "inventory-item.html?id="+id);
        /// <summary>
        /// Item
        /// </summary>
        public InventoryItem Item
        {
            get
            {
                var element = Page.QuerySelectorAsync("div.inventory_details_container").Result;
                InventoryItem item = new InventoryItem(element,"details");
                return item;
            }
        }
    }
}
