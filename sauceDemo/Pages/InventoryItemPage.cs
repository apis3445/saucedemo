using System;
using Microsoft.Playwright;
using sauceDemo.Components;

namespace sauceDemo.Pages
{
    public class InventoryItemPage : BasePage
    {
        public InventoryItemPage(IPage page) : base(page)
        {
        }

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
