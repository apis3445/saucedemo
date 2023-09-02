using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Playwright;
using sauceDemo.Components;

namespace sauceDemo.Pages;

public class CheckoutStep2Page : BasePage
{
   
    private Button _finish;
    public CartItems ListCartItems;

    public CheckoutStep2Page(IPage page) : base(page)
    {
        _finish =new Button(page, "data-test=finish", this.annotationHelper);
        ListCartItems = new CartItems(this.Page);
    }

    /// <summary>
    /// Cart Items
    /// </summary>
    public List<CartItem> Items
    {
        get
        {
            return ListCartItems.Items;
        }
    }

    /// <summary>
    /// Click in finish button
    /// </summary>
    /// <returns></returns>
    public async Task CickFinishAsync()
    {
        await _finish.ClickAsync();
        await TakeScreenShootAsync("finishClick");
    }
}
