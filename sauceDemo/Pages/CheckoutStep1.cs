using System;
using System.Threading.Tasks;
using Microsoft.Playwright;

namespace sauceDemo.Pages
{
    public class CheckoutStep1 : BasePage
    {
        private string firstNameInputLocator = "input[data-test='firstName']";
        private string lastNameInputLocator = "input[data-test='lastName']";
        private string postalCodeInputLocator = "input[data-test='postalCode']";
        private string continueButtonLocator = "input[data-test='continue']";

        public CheckoutStep1(IPage page) : base(page)
        {
        }

        public string FirstName =>  Page.TextContentAsync(firstNameInputLocator).Result;
 
        public async Task SetFirstNameAsync(string value)
        {
           await Page.TypeAsync(firstNameInputLocator, value);
        }
        
    
        public string LastName => Page.TextContentAsync(lastNameInputLocator).Result;

        public async Task SetLastNameAsync(string value)
        {
            await Page.TypeAsync(lastNameInputLocator, value);
        }

        public string PostalCode => Page.TextContentAsync(postalCodeInputLocator).Result;

        public async Task SetPostalCodeAsync(string value)
        {
            await Page.TypeAsync(postalCodeInputLocator, value);
        }

        public async Task ClickContinueAsync()
        {
            await Page.ClickAsync(continueButtonLocator);
            await Page.ScreenshotAsync(new PageScreenshotOptions { Path = "Continue.png" });
        }
    }
}
