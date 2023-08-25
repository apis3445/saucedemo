using System.Threading.Tasks;
using Microsoft.Playwright;

namespace sauceDemo.Components;

public class InputText : BaseLocator
{
    public InputText(IPage page, string locator) : base(page, locator)
    {

    }

    /// <summary>
    /// Sends key down, key press, keyup for each character
    /// </summary>
    /// <param name="value">Value to fill</param>
    /// <remarks>Doesn't clear input</remarks>
    /// <returns></returns>
    public async Task TypeAsync(string value)
    {
        await this.Locator.HighlightAsync();
        await this.Locator.TypeAsync(value);
    }

    /// <summary>
    /// Set input value like paste
    /// </summary>
    /// <param name="value">Value to fill</param>
    /// <remarks>Clear input value</remarks>
    /// <returns></returns>
    public async Task FillAsync(string value)
    {
        await this.Locator.HighlightAsync();
        await this.Locator.FillAsync(value);
    }

    /// <summary>
    /// Get the value
    /// </summary>
    /// <returns>Input value</returns>
    public string TextContent()
    {
        this.Locator.HighlightAsync().RunSynchronously();
        return this.Locator.TextContentAsync().Result;
    }



}
