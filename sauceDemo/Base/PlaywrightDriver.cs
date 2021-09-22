using System;
using System.Threading.Tasks;
using Microsoft.Playwright;

namespace sauceDemo.Base
{


    public class PlaywrightDriver
    {
        public IPage Page { get; set; }

        public async Task<IPage> InitalizePlaywright(string browserType, BrowserTypeLaunchOptions launchOptions)
        {
            var playwright = await Playwright.CreateAsync();
            IBrowser browser = null;
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