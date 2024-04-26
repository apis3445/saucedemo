using System.Collections.Generic;
using sauceDemo.Components;

namespace sauceDemo.Base;

public class ItemsByPrice
{
    private List<ItemPrice> itemsByPrice;
    public ItemsByPrice(List<InventoryItem> items)
    {
        this.itemsByPrice = new List<ItemPrice>();
        foreach (InventoryItem item in items)
        {
            this.itemsByPrice.Add(new ItemPrice(item.Name, item.Price));
        }
    }

    public List<ItemPrice> Items => this.itemsByPrice;

}

