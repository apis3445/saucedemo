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
            return browserType switch
            {
                "Chromium" => await playwright.Chromium.LaunchAsync(launchOptions),
                "Firefox" => await playwright.Firefox.LaunchAsync(launchOptions),
                "Webkit" => await playwright.Webkit.LaunchAsync(launchOptions),
                _ => await playwright.Chromium.LaunchAsync(launchOptions)
            };
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