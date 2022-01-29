using System;
using System.Threading.Tasks;
using Microsoft.Playwright;
using NUnit.Framework;
using sauceDemo.Pages;

namespace sauceDemo.Base
{
    public class BaseTest
    {
        protected IPage pge;
        protected InventoryPage inventoryPage;

        [SetUp]
        public async Task Setup()
        {
            PlaywrightDriver playwrightDriver = new PlaywrightDriver();
            pge = await playwrightDriver.InitalizePlaywright();
            var _loginPage = new LoginPage(pge);
            await _loginPage.GotoAsync();
            await _loginPage.LoginAsync(Constants.STANDARD_USER, Constants.GENERIC_PASSWORD);
            inventoryPage = new InventoryPage(pge);
        }
    }
}
