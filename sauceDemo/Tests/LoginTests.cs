﻿using System;
using System.Threading.Tasks;
using Microsoft.Playwright;
using NUnit.Framework;
using sauceDemo.Base;
using sauceDemo.Pages;

namespace sauceDemo.Tests;

[Parallelizable]
public class LoginTests 
{
    string _genericPassword;
    string _standardUser;
    private string _baseAddress = Initialize.BaseAddress;
    private IPage _page;
    private IBrowserContext _context;
    protected LoginPage loginPage;

    [SetUp]
    public async Task Setup()
    {            
        PlaywrightDriver playwrightDriver = new PlaywrightDriver();
        _page = await playwrightDriver.InitalizePlaywrightTracingAsync();
        _context = playwrightDriver.Context;
        loginPage = new LoginPage(_page);
        loginPage.AddName(TestContext.CurrentContext.Test.Name);
        await loginPage.GotoAsync();

    }

    [Test, Category("Login")]
    [TestCase(TestName = "Login with valid user redirects to products page")]
    public async Task Login_WithValidUser_NavigatesToProductsPageAsync()
    {
        //Arrange
        _genericPassword = Environment.GetEnvironmentVariable("PASSWORD");
        _standardUser = Environment.GetEnvironmentVariable("USER");
        //Act
        await loginPage.LoginAsync(_standardUser, _genericPassword);
        //Assert
        string expectedPage = _baseAddress + Constants.INVENTORY_PAGE;
        loginPage.AssertEqual(expectedPage, _page.Url, "Check URL Page equal to: '" + expectedPage + "'");
    }

    [Test, Category("Login")]
    [TestCase(TestName = "Login with a invalid user shows error message")]
    public async Task Login_WithInvalidUser_ShowsErrorMessageAsync()
    {
        //Arrange
        _genericPassword = Environment.GetEnvironmentVariable("PASSWORD");
        //Act
        await loginPage.LoginAsync("invalidUser", _genericPassword);
        //Assert
        Assert.That(await loginPage.HasError(), Is.True);
        loginPage.AssertEqual("Epic sadface: Username and password do not match any user in this service", await loginPage.GetErrorAsync(), "Should show 'username and password error'");
    }

    [Test, Category("Login")]
    [TestCase(TestName = "Login with a locked user shows locked error message")]
    public async Task Login_WithLockedUser_ShowsLockedErrorMessageAsync()
    {
        //Arrange
        _genericPassword = Environment.GetEnvironmentVariable("PASSWORD");
        //Act
        await loginPage.LoginAsync("locked_out_user", _genericPassword);
        //Assert
        //To check if the div with error is visible
        Assert.That(await loginPage.HasError(), Is.True, "Error is not visible");
        //Only if you want to check the error messsage not only error div
        loginPage.AssertEqual("Epic sadface: Sorry, this user has been locked out.", await loginPage.GetErrorAsync(), "Should show 'locked user'");
    }

    [Test, Category("Login")]
    [TestCase(TestName = "Logout redirects to login")]
    public async Task Logout_FromHomePage_RedirectToLogin()
    {
        //Arrange
        _genericPassword = Environment.GetEnvironmentVariable("PASSWORD");
        _standardUser = Environment.GetEnvironmentVariable("USER");
        await loginPage.GotoAsync();
        await loginPage.LoginAsync(_standardUser, _genericPassword);
        InventoryPage inventoryPage = new InventoryPage(_page);
        //Act
        await inventoryPage.LogoutAsync();
        //Assert
        loginPage.AssertEqual(_baseAddress, _page.Url, "Should redirect to login");
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