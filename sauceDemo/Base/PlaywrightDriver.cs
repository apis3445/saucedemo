using System;
using System.Threading.Tasks;
using Microsoft.Playwright;

namespace sauceDemo.Base
{
    public class PlaywrightDriver
    {
        public IPage Page { get; set; }

        public async Task<IPage> InitalizePlaywright()
        {
            var playwright = await Playwright.CreateAsync();
            string browserType = Environment.GetEnvironmentVariable(Constants.BROWSER_TYPE);
            BrowserTypeLaunchOptions launchOptions = new BrowserTypeLaunchOptions { Headless = false };
            IBrowser browser;
            switch (browserType)
            {
                case "Chromium":
                    browser = await playwright.Chromium.LaunchAsync(launchOptions);
                    break;
                case "Firefox":
                    browser = await playwright.Firefox.LaunchAsync(launchOptions);
                    break;
                case "Webkit":
                    browser = await playwright.Webkit.LaunchAsync(launchOptions);
                    break;
                default:
                    browser = await playwright.Chromium.LaunchAsync(launchOptions);
                    break;
            }           
            Page = await browser.NewPageAsync();
            return Page;
        }
    }
}