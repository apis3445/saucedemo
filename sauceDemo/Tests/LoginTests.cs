using System;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using sauceDemo.Base;
using sauceDemo.Pages;

namespace sauceDemo.Tests
{
    [TestClass]
    public class LoginTests
    {
        private const string genericPassword = "secret_sauce";
        private const string standardUser = "standard_user";
        private string BaseAddress = Environment.GetEnvironmentVariable(Constants.BASE_ADDRESS);
        private IPage page;
        LoginPage loginPage;

        [TestInitialize]
        public async Task Setup()
        {
            PlaywrightDriver playwrightDriver = new PlaywrightDriver();
            page = await playwrightDriver.InitalizePlaywright();
            loginPage = new LoginPage(page);
            await loginPage.Goto();

        }

        [TestMethod]
        [DataRow(standardUser, genericPassword)]
        public async Task Login_WithValidUser_NavigatesToInventoryPageAsync(string user, string password)
        {
            //Arrange
            //Act
            await loginPage.LoginAsync(user, password);
            //Assert
            Assert.AreEqual(BaseAddress + Constants.INVENTORY_PAGE, page.Url);
        }

        [TestMethod]
        public async Task Login_WithLockedUser_ShowsLockedErrorMessageAsync()
        {
            //Arrange
            //Act
            await loginPage.LoginAsync("locked_out_user", genericPassword);
            //Assert
            //To check if the div with error is visible
            Assert.IsTrue(await loginPage.HasError(), "Error is not visible");
            //Only if you want to check the error messsage not only error div
            Assert.AreEqual("Epic sadface: Sorry, this user has been locked out.", await loginPage.GetErrorAsync());
        }

        [TestMethod]
        public async Task Login_WithInvalidUser_ShowsLockedErrorMessage()
        {
            //Arrange
            //Act
            await loginPage.LoginAsync("invalidUser", genericPassword);
            //Assert
            Assert.IsTrue(await loginPage.HasError());
            Assert.AreEqual("Epic sadface: Username and password do not match any user in this service", await loginPage.GetErrorAsync());
        }

        [TestMethod]
        public async Task Logout_FromHomePage_RedirectToLogin()
        {
            //Arrange
            await loginPage.LoginAsync(standardUser, genericPassword);
            InventoryPage inventoryPage = new InventoryPage(page);
            //Act
            await inventoryPage.Logout();
            //Assert
            Assert.AreEqual(BaseAddress, page.Url);
        }

    }
}
