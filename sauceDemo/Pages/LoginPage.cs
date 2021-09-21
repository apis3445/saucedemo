using System.Threading.Tasks;
using Microsoft.Playwright;

namespace sauceDemo.Pages
{
    public class LoginPage : BasePage
    {
        private string userNameInputLocator = "input[data-test='username']";
        private string passwordInputLocator = "input[data-test='password']";
        private string loginButtonLocator = "input[data-test='login-button']";
        private string errorMessage = "data-test=error";

        public LoginPage(IPage page) : base(page)
        {

        }

        public async Task Goto() => await Page.GotoAsync(Constants.BASE_ADDRESS);

        public async Task SetUserAsync(string user) => await Page.TypeAsync(userNameInputLocator, user);

        public async Task SetPasswordAsync(string password) => await Page.TypeAsync(passwordInputLocator, password);

        public async Task<string> GetErrorAsync()
        {
            return await Page.QuerySelectorAsync(errorMessage).Result.TextContentAsync();
        }

        public async Task ClickLogin()
        {
            await Page.ClickAsync(loginButtonLocator);
            await Page.ScreenshotAsync(new PageScreenshotOptions { Path = "LoginClick.png" });
        }

        public async Task<bool> HasError() =>  await Page.IsVisibleAsync(errorMessage);

        public async Task LoginAsync(string user, string password)
        {
            await SetUserAsync(user);
            await SetPasswordAsync(password);
            await ClickLogin();
        }
    }
}
