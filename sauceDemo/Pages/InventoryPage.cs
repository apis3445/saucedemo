﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Playwright;
using sauceDemo.Components;

namespace sauceDemo.Pages
{
    public class InventoryPage : BasePage
    {
        private string _comboSortLocator = "data-test=product_sort_container";

        public List<string> ItemsName = new List<string>();

        public InventoryPage(IPage page) : base(page)
        {

        }

        /// <summary>
        /// List of items 
        /// </summary>
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

        /// <summary>
        /// Sort items by the value
        /// </summary>
        /// <param name="value">Value/Text to sort the items</param>
        public void SetSort(string value)
        {
            Page.SelectOptionAsync(_comboSortLocator, value);
        }

        /// <summary>
        /// Add item t cart by item name
        /// </summary>
        /// <param name="itemName">Name of the item to add to the cart</param>
        /// <returns></returns>
        public async Task AddToCartByNameAsync(string itemName)
        {
            await Page.ClickAsync($"text={itemName}");
        }

        /// <summary>
        /// Add item by data-test
        /// </summary>
        /// <param name="itemName">Data-test value</param>
        /// <returns></returns>
        public async Task AddToCartByDataTestNameAsync(string itemName)
        {
            string id = $"data-test=add-to-cart-{itemName.ToLowerInvariant().Replace(" ", "-")}";
            await Page.ClickAsync(id);
        }

        /// <summary>
        /// Add items from index 0 to toal
        /// </summary>
        /// <param name="total">Total the items to add</param>
        /// <returns></returns>
        public async Task AddItemsAsync(int total)
        {
            for (int i = 0; i < total; i++)
            {
                await Items[i].Button.ClickAsync();
                ItemsName.Add(Items[i].Name);
            }
        }
    }
}