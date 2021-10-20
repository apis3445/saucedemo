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

        /// <summary>
        /// Go to Login page
        /// </summary>
        /// <returns></returns>
        public async Task Goto() => await Page.GotoAsync(Initialize.BaseAddress);

        /// <summary>
        /// Set the user to login
        /// </summary>
        /// <param name="user">User to login.</param>
        /// <returns></returns>
        public async Task SetUserAsync(string user) => await Page.TypeAsync(_userNameInputLocator, user);

        /// <summary>
        /// Set password to login
        /// </summary>
        /// <param name="password">User password</param>
        /// <returns></returns>
        public async Task SetPasswordAsync(string password) => await Page.TypeAsync(_passwordInputLocator, password);

        /// <summary>
        /// Get error messsage
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetErrorAsync()
        {
            return await Page.QuerySelectorAsync(_errorMessageLocator).Result.TextContentAsync();
        }

        /// <summary>
        /// Click in login button
        /// </summary>
        /// <returns></returns>
        public async Task ClickLogin()
        {
            await Page.ClickAsync(_loginButtonLocator);
            await TakeScreenShootAsync("LoginClick");
        }

        /// <summary>
        /// Returns true if the page has an error messsage
        /// </summary>
        /// <returns></returns>
        public async Task<bool> HasError() =>  await Page.IsVisibleAsync(_errorMessageLocator);

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
            await ClickLogin();
        }
    }
}
