using System.Threading.Tasks;
using Microsoft.Playwright;

namespace sauceDemo.Pages
{
    public class LoginPage : BasePage
    {
        private string _userNameInputLocator = "input[data-test='username']";
        private string _passwordInputLocator = "input[data-test='password']";
        private string _loginButtonLocator = "input[data-test='login-button']";
        private string _errorMessageLocator = "data-test=error";
        
        public LoginPage(IPage page) : base(page)
        {
            
        }

        public async Task Goto() => await Page.GotoAsync(Initialize.BaseAddress);

        public async Task SetUserAsync(string user) => await Page.TypeAsync(_userNameInputLocator, user);

        public async Task SetPasswordAsync(string password) => await Page.TypeAsync(_passwordInputLocator, password);

        public async Task<string> GetErrorAsync()
        {
            return await Page.QuerySelectorAsync(_errorMessageLocator).Result.TextContentAsync();
        }

        public async Task ClickLogin()
        {
            await Page.ClickAsync(_loginButtonLocator);
            await TakeScreenShootAsync("LoginClick");
        }

        public async Task<bool> HasError() =>  await Page.IsVisibleAsync(_errorMessageLocator);

        public async Task LoginAsync(string user, string password)
        {
            await SetUserAsync(user);
            await SetPasswordAsync(password);
            await ClickLogin();
        }
    }
}
