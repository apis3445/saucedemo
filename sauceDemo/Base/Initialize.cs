using System;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using sauceDemo.Base;

namespace sauceDemo
{
    [TestClass]
    public class Initialize
    {

        public static IPage Page;

        [AssemblyInitialize]
        public static async Task AssemblyInitializeAsync(TestContext context)
        {
            PlaywrightDriver playwrightDriver = new PlaywrightDriver();

            BrowserTypeLaunchOptions launchOptions = new BrowserTypeLaunchOptions { Headless = false };

            Page = await playwrightDriver.InitalizePlaywright(Environment.GetEnvironmentVariable(Constants.BROWSER_TYPE), launchOptions);

        }

        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            
        }
    }
}
