using Microsoft.Playwright;

namespace sauceDemo.Pages
{
    public class CheckoutCompletePage : BasePage
    {
        private ILocator _thanks;

        public CheckoutCompletePage(IPage page) : base(page)
        {
            _thanks = page.Locator("h2.complete-header");
        }

        /// <summary>
        /// Thanks message
        /// </summary>
        public string Thanks => _thanks.TextContentAsync().Result;
    }
}
