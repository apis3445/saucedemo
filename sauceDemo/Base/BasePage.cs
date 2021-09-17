using System.Threading.Tasks;
using Microsoft.Playwright;

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
    }
}