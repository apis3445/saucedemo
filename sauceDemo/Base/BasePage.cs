using System;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace sauceDemo
{
    /// <summary>
    /// Basse Page with common functions to all pages
    /// </summary>
    public class BasePage
    {
        protected IPage Page;
        private string burgerMenuId = "#react-burger-menu-btn";
        private string logoutMenuItem = "#logout_sidebar_link";
        private string shoppingCartBadge = "span.shopping_cart_badge";

        public BasePage(IPage page)
        {
            this.Page = page;
        }

        public async Task ClickMenu() => await Page.ClickAsync(burgerMenuId);

        public async Task Logout()
        {
            await ClickMenu();
            await Page.ClickAsync(logoutMenuItem);
        }

        public IElementHandle shopingCartIcon => Page.QuerySelectorAsync(shoppingCartBadge).Result;

        public int ItemsInShoppingCart
        {
            get
            {
               var element = Page.QuerySelectorAsync(shoppingCartBadge).Result;
               if (element!=null)
                    return int.Parse(element.TextContentAsync().Result);
               else
                    return 0;
            }
        }

        public async Task TakeScreenShootAsync(string name)
        {
            await Page.ScreenshotAsync(new PageScreenshotOptions { Path = name + Guid.NewGuid().ToString() + ".png" });          
        }
    }
}