using System.Threading.Tasks;
using Microsoft.Playwright;
using sauceDemo.Components;

namespace sauceDemo.Pages;

public class LoginPage : BasePage
{
    private InputText _userNameInput;
    private InputText _passwordInput;
    private Button _loginButton;
    private ILocator _errorMessage;
    
    public LoginPage(IPage page) : base(page)
    {
        _userNameInput = new InputText(page,"input[data-test='username']");
        _passwordInput = new InputText(page, "input[data-test='password']");
        _loginButton = new Button(page, "input[data-test='login-button']");
        _errorMessage = page.Locator("data-test=error");
    }

    /// <summary>
    /// Go to Login page
    /// </summary>
    /// <returns></returns>
    public async Task GotoAsync() => await Page.GotoAsync(Initialize.BaseAddress);

    /// <summary>
    /// Set the user to login
    /// </summary>
    /// <param name="user">User to login.</param>
    /// <returns></returns>
    public async Task SetUserAsync(string user) => await _userNameInput.TypeAsync(user);

    /// <summary>
    /// Set password to login
    /// </summary>
    /// <param name="password">User password</param>
    /// <returns></returns>
    public async Task SetPasswordAsync(string password) => await _passwordInput.TypeAsync(password);

    /// <summary>
    /// Get error messsage
    /// </summary>
    /// <returns></returns>
    public async Task<string> GetErrorAsync()
    {
        return await _errorMessage.TextContentAsync();
    }

    /// <summary>
    /// Click in login button
    /// </summary>
    /// <returns></returns>
    public async Task ClickLoginAsync()
    {
        await _loginButton.ClickAsync();
        await TakeScreenShootAsync("LoginClick");
    }

    /// <summary>
    /// Returns true if the page has an error messsage
    /// </summary>
    /// <returns></returns>
    public async Task<bool> HasError() =>  await _errorMessage.IsVisibleAsync();

    /// <summary>
    /// Login with the user and password
    /// </summary>
    /// <param name="user">User login</param>
    /// <param name="password">User password</param>
    /// <returns></returns>
    public async Task LoginAsync(string user, string password)
    {
        await SetUserAsync(user);
        await SetPasswordAsync(password);
        await ClickLoginAsync();
    }
}
