using Microsoft.Playwright;

namespace sauceDemo.Components;

public class BaseLocator
{

    public ILocator Locator { get; set; }
    public IPage Page { get; }

    public BaseLocator(IPage page, string locator)
    {
        this.Locator = page.Locator(locator);
        Page = page;
    }

}
