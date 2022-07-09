using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Playwright;
using sauceDemo.Components;

namespace sauceDemo.Pages;

public class InventoryPage : BasePage
{
    private ILocator _comboSort;

    public List<string> ItemsName = new List<string>();

    public InventoryPage(IPage page) : base(page)
    {
        _comboSort = page.Locator("data-test=product_sort_container");
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
    public async Task SetSortAsync(string value)
    {
        await _comboSort.SelectOptionAsync(value);
        //wait to reload the list of products
        _ = await Page.WaitForSelectorAsync("div.inventory_list");
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
            await Items[i].CartButton.ClickAsync();
            ItemsName.Add(Items[i].Name);
        }
    }
}