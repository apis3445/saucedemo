using Microsoft.Playwright;

namespace sauceDemo.Pages
{
    public class CheckoutComplete : BasePage
    {
        private string thanksLocator = "h2.complete-header";

        public CheckoutComplete(IPage page) : base(page)
        {
        }

        public string Thanks => Page.QuerySelectorAsync(thanksLocator).Result.TextContentAsync().Result;
    }
}
