using System;
using System.Threading.Tasks;
using Microsoft.Playwright;

namespace sauceDemo.Base
{
    public class PlaywrightDriver
    {
        public IPage Page { get; set; }
        public IBrowserContext Context { get; set; }

        public async Task<IPage> InitalizePlaywright()
        {
            var browser = await InitBrowserAsync();
            Context = await browser.NewContextAsync();
            Page = await Context.NewPageAsync();
            return Page;
        }

        private async Task<IBrowser> InitBrowserAsync()
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
            return browser;
        }

        public async Task<IPage> InitalizePlaywrightTracing()
        {
            var browser = await InitBrowserAsync();
            Context = await browser.NewContextAsync();
            // Sample for tracing
            await Context.Tracing.StartAsync(new TracingStartOptions
            {
                Screenshots = true,
                Snapshots = true
            });
            Page = await Context.NewPageAsync();
            return Page;
        }
       
    }
}