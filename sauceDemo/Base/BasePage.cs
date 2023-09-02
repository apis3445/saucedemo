using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Playwright;
using NUnit.Framework;
using sauceDemo.Base;
using static System.Net.Mime.MediaTypeNames;

namespace sauceDemo;

/// <summary>
/// Basse Page with common functions to all pages
/// </summary>
public class BasePage
{
    protected IPage Page;

    private ILocator _burgerMenuId;
    private ILocator _logoutMenuItem;
    private ILocator _shoppingCartBadge;
    private IReporter _reporter;
    protected AnnotationHelper annotationHelper;

    public BasePage(IPage page)
    {
        this.Page = page;
        _shoppingCartBadge = Page.Locator("span.shopping_cart_badge");
        _logoutMenuItem = Page.Locator("#logout_sidebar_link");
        _burgerMenuId = Page.Locator("#react-burger-menu-btn");
        _reporter = new ConsoleReporter();
        annotationHelper = new AnnotationHelper(_reporter);
    }

    /// <summary>
    /// Total the items in the shopping cart
    /// </summary>
    public int ItemsInShoppingCart
    {
        get
        {
           if (_shoppingCartBadge.IsVisibleAsync().Result)
                return int.Parse(_shoppingCartBadge.TextContentAsync().Result);
           else
                return 0;
        }
    }

    /// <summary>
    /// Take screenshot
    /// </summary>
    /// <param name="name">Name of the image to save</param>
    /// <remarks>Can be optional for some oncloud </remarks>
    /// <returns></returns>
    public async Task TakeScreenShootAsync(string name)
    {
        var screenImage = System.IO.Path.Combine(TestContext.CurrentContext.TestDirectory, name + "-" + Guid.NewGuid().ToString() + ".png");
        var imageBytes = await Page.ScreenshotAsync(new PageScreenshotOptions { FullPage = true});
        File.WriteAllBytes(screenImage,imageBytes);
        TestContext.AddTestAttachment(screenImage);  
    }

    /// <summary>
    /// Click in the hamburger menu
    /// </summary>
    /// <returns></returns>
    public async Task ClickMenuAsync() => await _burgerMenuId.ClickAsync();

    /// <summary>
    /// Click in shopping cart badge
    /// </summary>
    /// <returns></returns>
    public async Task ClickShoppingCartBadgeAsync() => await _shoppingCartBadge.ClickAsync();

    /// <summary>
    /// Logout
    /// </summary>
    /// <returns></returns>
    public async Task LogoutAsync()
    {
        await ClickMenuAsync();
        await _logoutMenuItem.ClickAsync();
    }

    public async Task GotoPageAsync(string page)
    {
        this.annotationHelper.AddAnnotation(AnnotationType.Step, "Go to the page: '" + page + "'");
        await Page.GotoAsync(page);
    }

    public void AssertEqual(object expected, object actual, string errorMessage)
    {
        this.annotationHelper.AddAnnotation(AnnotationType.Assert, errorMessage);
        Assert.AreEqual(expected, actual, errorMessage);
    }

    public List<Annotation> GetAnnotations ()
    {
        return this.annotationHelper.GetAnnotations();
    }

}