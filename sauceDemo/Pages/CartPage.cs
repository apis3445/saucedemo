using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Playwright;
using NUnit.Framework;
using sauceDemo.Components;

namespace sauceDemo.Pages;

public class CartPage : BasePage
{
    private Button _checkout;
    private Button _continueShopping;
    public CartItems CartItems;

    public CartPage(IPage page) : base(page)
    {
        _checkout = new Button(page, "data-test=checkout", this.annotationHelper);
        _continueShopping = new Button(page, "data-test=continue-shopping", this.annotationHelper);
        CartItems = new CartItems(this.Page);
    }

    /// <summary>
    /// Reeturns the list of items. Is list due to can be dynamic, sometimes can return 5, another 15 or higher.
    /// </summary>
    public List<CartItem> Items
    {
        get
        {
            return CartItems.Items;
        }
    }

    /// <summary>
    /// Click checkout
    /// </summary>
    /// <returns></returns>
    public async Task ClickCheckoutAsync()
    {
        await _checkout.ClickAsync();
        //Added screenshot maybe check environment variabl to save in local development or connect
        //to third party report tool like report portal
       await TakeScreenShootAsync("Checkout");
    }

    /// <summary>
    /// Click in continue shopping
    /// </summary>
    /// <returns></returns>
    public async Task ClickContinueShoppingAsync()
    {
        await _continueShopping.ClickAsync();
        await TakeScreenShootAsync("ContinueShopping");
    }

    /// <summary>
    /// Check total of items to reuse in different test cases
    /// </summary>
    /// <param name="total">Total the items in the cart</param>
    public void CheckItemsInCart(int total)
    {
        Assert.That(total, Is.EqualTo(ItemsInShoppingCart), "Total items in the cart is different");
    }
}
