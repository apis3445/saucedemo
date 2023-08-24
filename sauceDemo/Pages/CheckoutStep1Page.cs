using System.Threading.Tasks;
using Microsoft.Playwright;
using sauceDemo.Components;

namespace sauceDemo.Pages;

public class CheckoutStep1Page : BasePage
{
    private InputText _firstName;
    private InputText _lastName;
    private InputText _postalCode;
    private Button _continue;

    public string FirstName => _firstName.TextContent();
    public string LastName => _lastName.TextContent();
    public string PostalCode => _postalCode.TextContent();

    public CheckoutStep1Page(IPage page) : base(page)
    {
        _firstName = new InputText(page, "input[data-test='firstName']");
        _lastName = new InputText(page, "input[data-test='lastName']");
        _postalCode = new InputText(page, "input[data-test='postalCode']");
        _continue = new Button(page, "input[data-test='continue']");
    }

    /// <summary>
    /// Set First Name
    /// </summary>
    /// <param name="value">Value for the first name</param>
    /// <returns></returns>
    public async Task SetFirstNameAsync(string value)
    {
        await this._firstName.FillAsync(value);
    }

    /// <summary>
    /// Set Last name
    /// </summary>
    /// <param name="value">Value for the last name</param>
    /// <returns></returns>
    public async Task SetLastNameAsync(string value)
    {
        await this._lastName.FillAsync(value);
    }

    /// <summary>
    /// Set postal code
    /// </summary>
    /// <param name="value">Postal Code</param>
    /// <returns></returns>
    public async Task SetPostalCodeAsync(string value)
    {
        await _postalCode.FillAsync(value);
    }

    /// <summary>
    /// Click in continue button
    /// </summary>
    /// <returns></returns>
    public async Task ClickContinueAsync()
    {
        await _continue.ClickAsync();
        await TakeScreenShootAsync("ContinueCheckout");
    }
}