using System;
using Microsoft.Playwright;
using System.Threading.Tasks;

namespace sauceDemo.Components
{
	public class Password : BaseLocator
    {
        public Password(IPage page, string locator) : base(page, locator)
        {

        }

        /// <summary>
        /// Set input value like paste
        /// </summary>
        /// <param name="value">Value to fill</param>
        /// <returns></returns>
        public async Task FillAsync(string value)
        {
            await this.Locator.FillAsync(value);
        }
    }
}

