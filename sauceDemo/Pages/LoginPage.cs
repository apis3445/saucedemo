using System.Threading.Tasks;
using Microsoft.Playwright;
using sauceDemo.Components;

namespace sauceDemo.Pages;

public class LoginPage : BasePage
{
    private InputText _userName;
    private Password _password;
    private Button _login;
    private ILocator _errorMessage;
    
    public LoginPage(IPage page) : base(page)
    {
        _userName = new InputText(page,"input[data-test='username']");
        _password = new Password(page, "input[data-test='password']");
        _login = new Button(page, "input[data-test='login-button']");
        _errorMessage = page.Locator("data-test=error");
    }

    /// <summary>
    /// Go to Login page
    /// </summary>
    /// <returns></returns>
    public async Task GotoAsync() => await Page.GotoAsync(Initialize.BaseAddress);

    /// <summary>
    /// Get error messsage
    /// </summary>
    /// <returns></returns>
    public async Task<string> GetErrorAsync()
    {
        return await _errorMessage.TextContentAsync();
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
        await this._userName.FillAsync(user);
        await this._password.FillAsync(password);
        await this._login.ClickAsync();
    }
}
