using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Playwright;
using sauceDemo.Components;

namespace sauceDemo.Pages
{
    public class InventoryPage : BasePage
    {
        private string comboSort = "data-test=product_sort_container";
        public List<string> itemsName = new List<string>();

        public InventoryPage(IPage page) : base(page)
        {

        }

        public List<InventoryItem> Items
        {
            get
            {
                var listItems = new List<InventoryItem>();
                var inventoryItems = Page.QuerySelectorAllAsync("div.inventory_item").Result;
                foreach (var item in inventoryItems)
                {
                    InventoryItem inventoryItem = new InventoryItem(item,"item");                  
                    listItems.Add(inventoryItem);
                }
                return listItems;
            }
        }

        public void SetSort(string value)
        {
            Page.SelectOptionAsync(comboSort, value);
        }

        public async Task AddToCartByNameAsync(string itemName)
        {
            await Page.ClickAsync($"text={itemName}");
        }

        public async Task AddToCartByIdNameAsync(string itemName)
        {
            string id = $"data-test=add-to-cart-{itemName.ToLowerInvariant().Replace(" ", "-")}";
            await Page.ClickAsync(id);
        }

        public async Task AddItemsAsync(int total)
        {
            for (int i = 0; i < total; i++)
            {
                await Items[i].Button.ClickAsync();
                itemsName.Add(Items[i].Name);
            }
        }
    }
}