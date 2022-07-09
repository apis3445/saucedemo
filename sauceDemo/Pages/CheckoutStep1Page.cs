using System.Threading.Tasks;
using Microsoft.Playwright;
using sauceDemo.Components;

namespace sauceDemo.Pages;

public class CheckoutStep1Page : BasePage
{
    private InputText _firstNameInputLocator;
    private InputText _lastNameInputLocator;
    private InputText _postalCodeInputLocator;
    private Button _continueButtonLocator;

    public string FirstName => _firstNameInputLocator.TextContent();
    public string LastName => _lastNameInputLocator.TextContent();
    public string PostalCode => _postalCodeInputLocator.TextContent();

    public CheckoutStep1Page(IPage page) : base(page)
    {
        _firstNameInputLocator = new InputText(page, "input[data-test='firstName']");
        _lastNameInputLocator = new InputText(page, "input[data-test='lastName']");
        _postalCodeInputLocator = new InputText(page, "input[data-test='postalCode']");
        _continueButtonLocator = new Button(page, "input[data-test='continue']");
    }

    /// <summary>
    /// Set First Name
    /// </summary>
    /// <param name="value">Value for the first name</param>
    /// <returns></returns>
    public async Task SetFirstNameAsync(string value)
    {
        await this._firstNameInputLocator.FillAsync(value);
    }

    /// <summary>
    /// Set Last name
    /// </summary>
    /// <param name="value">Value for the last name</param>
    /// <returns></returns>
    public async Task SetLastNameAsync(string value)
    {
        await this._lastNameInputLocator.FillAsync(value);
    }

    /// <summary>
    /// Set postal code
    /// </summary>
    /// <param name="value">Postal Code</param>
    /// <returns></returns>
    public async Task SetPostalCodeAsync(string value)
    {
        await _postalCodeInputLocator.FillAsync(value);
    }

    /// <summary>
    /// Click in continue button
    /// </summary>
    /// <returns></returns>
    public async Task ClickContinueAsync()
    {
        await _continueButtonLocator.ClickAsync();
        await TakeScreenShootAsync("ContinueCheckout");
    }
}