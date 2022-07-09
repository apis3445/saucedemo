using System.Threading.Tasks;
using Microsoft.Playwright;
using NUnit.Framework;
using sauceDemo.Base;
using sauceDemo.Pages;

namespace sauceDemo.Tests;

[Parallelizable]
public class LoginTests
{
    private const string _genericPassword = "secret_sauce";
    private const string _standardUser = "standard_user";
    private string _baseAddress = Initialize.BaseAddress;
    private IPage _page;
    private LoginPage _loginPage;
    private IBrowserContext _context;

    [SetUp]
    public async Task Setup()
    {            
        PlaywrightDriver playwrightDriver = new PlaywrightDriver();
        _page = await playwrightDriver.InitalizePlaywrightTracing();
        _context = playwrightDriver.Context;
        _loginPage = new LoginPage(_page);
        await _loginPage.GotoAsync();
    }

    [Test,  Category("Login")]
    [TestCase(_standardUser, _genericPassword)]
    public async Task Login_WithValidUser_NavigatesToProductsPageAsync(string user, string password)
    {
        //Arrange
              
        //Act
        await _loginPage.LoginAsync(user, password);
        //Assert
        Assert.AreEqual(_baseAddress +  Constants.INVENTORY_PAGE, _page.Url, "Main Page for user is not show");
       
    }

    [Test,  Category("Login")]
    public async Task Login_WithInvalidUser_ShowsErrorMessageAsync()
    {
        //Arrange
        //Act
        await _loginPage.LoginAsync("invalidUser", _genericPassword);
        //Assert
        Assert.IsTrue(await _loginPage.HasError());
        Assert.AreEqual("Epic sadface: Username and password do not match any user in this service", await _loginPage.GetErrorAsync(), "Should show username and password error");
    }

    [Test,  Category("Login")]
    public async Task Login_WithLockedUser_ShowsLockedErrorMessageAsync()
    {
        //Arrange
        //Act
        await _loginPage.LoginAsync("locked_out_user", _genericPassword);
        //Assert
        //To check if the div with error is visible
        Assert.IsTrue(await _loginPage.HasError(), "Error is not visible");
        //Only if you want to check the error messsage not only error div
        Assert.AreEqual("Epic sadface: Sorry, this user has been locked out.", await _loginPage.GetErrorAsync(), "Should show locked user");
    }

    [Test,  Category("Login")]
    public async Task Logout_FromHomePage_RedirectToLogin()
    {
        //Arrange
        await _loginPage.LoginAsync(_standardUser, _genericPassword);
        InventoryPage inventoryPage = new InventoryPage(_page);
        //Act
        await inventoryPage.Logout();
        //Assert
        Assert.AreEqual(_baseAddress, _page.Url, "Should redirect to login");
    }

    [TearDown]
    public async Task BaseTearDownAsync()
    {
        string tracePath = System.IO.Path.Combine(TestContext.CurrentContext.TestDirectory, "trace.zip");
        // Stop tracing and export it into a zip archive.
        await _context.Tracing.StopAsync(new TracingStopOptions
        {
            Path = tracePath
        });
        TestContext.AddTestAttachment(tracePath);
        //To open the tracing
        //playwright show-trace trace.zip
    }
}