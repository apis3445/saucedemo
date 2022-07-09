using System.Threading.Tasks;
using Microsoft.Playwright;

namespace sauceDemo.Components;

public class Button : BaseLocator
{
    public Button(IPage page, string locator) : base(page, locator)
    {
            
    }

    public async Task ClickAsync()
    {
        await this.Locator.HighlightAsync();
        await this.Locator.ClickAsync();
    }
}

