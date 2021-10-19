using Microsoft.Playwright;

namespace sauceDemo.Pages
{
    public class CheckoutComplete : BasePage
    {
        private string _thanksLocator = "h2.complete-header";

        public CheckoutComplete(IPage page) : base(page)
        {
        }

        /// <summary>
        /// Thanks message
        /// </summary>
        public string Thanks => Page.QuerySelectorAsync(_thanksLocator).Result.TextContentAsync().Result;
    }
}
