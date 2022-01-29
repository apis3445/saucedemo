using System;
using System.Threading.Tasks;
using Microsoft.Playwright;
using NUnit.Framework;
using sauceDemo.Pages;

namespace sauceDemo.Base
{
    public class BaseTest
    {
        protected IPage page;
        protected InventoryPage inventoryPage;

        [SetUp]
        public async Task Setup()
        {
            PlaywrightDriver playwrightDriver = new PlaywrightDriver();
            page = await playwrightDriver.InitalizePlaywright();
            var loginPage = new LoginPage(page);
            await loginPage.GotoAsync();
            await loginPage.LoginAsync(Constants.STANDARD_USER, Constants.GENERIC_PASSWORD);
            inventoryPage = new InventoryPage(page);
        }
    }
}
