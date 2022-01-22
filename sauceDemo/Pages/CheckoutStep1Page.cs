using System.Threading.Tasks;
using Microsoft.Playwright;

namespace sauceDemo.Pages
{
    public class CheckoutStep1Page : BasePage
    {
        private string _firstNameInputLocator = "input[data-test='firstName']";
        private string _lastNameInputLocator = "input[data-test='lastName']";
        private string _postalCodeInputLocator = "input[data-test='postalCode']";
        private string _continueButtonLocator = "input[data-test='continue']";

        public string FirstName => Page.TextContentAsync(_firstNameInputLocator).Result;
        public string LastName => Page.TextContentAsync(_lastNameInputLocator).Result;
        public string PostalCode => Page.TextContentAsync(_postalCodeInputLocator).Result;

        public CheckoutStep1Page(IPage page) : base(page)
        {
        }

        /// <summary>
        /// Set First Name
        /// </summary>
        /// <param name="value">Value for the first name</param>
        /// <returns></returns>
        public async Task SetFirstNameAsync(string value)
        {
           await Page.TypeAsync(_firstNameInputLocator, value);
        }

        /// <summary>
        /// Set Last name
        /// </summary>
        /// <param name="value">Value for the last name</param>
        /// <returns></returns>
        public async Task SetLastNameAsync(string value)
        {
            await Page.TypeAsync(_lastNameInputLocator, value);
        }

        /// <summary>
        /// Set postal code
        /// </summary>
        /// <param name="value">Postal Code</param>
        /// <returns></returns>
        public async Task SetPostalCodeAsync(string value)
        {
            await Page.TypeAsync(_postalCodeInputLocator, value);
        }

        /// <summary>
        /// Click in continue button
        /// </summary>
        /// <returns></returns>
        public async Task ClickContinueAsync()
        {
            await Page.ClickAsync(_continueButtonLocator);
            await TakeScreenShootAsync("ContinueCheckout");
        }
    }
}