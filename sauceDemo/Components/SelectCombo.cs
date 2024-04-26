using System.Threading.Tasks;
using Microsoft.Playwright;
using sauceDemo.Base;

namespace sauceDemo.Components;

public class SelectCombo : BaseLocator
{
    public SelectCombo(IPage page, string locator, AnnotationHelper annotationHelper) : base(page, locator, annotationHelper)
    {

    }

    /// <summary>
    /// Select option in the select
    /// </summary>
    /// <param name="value">Value to select</param>
    public async Task SelectOptionAsync(string value) {
        this.AnnotationHelper.AddAnnotation(AnnotationType.Step, "Select the value: '" + value + "'");
        await this.Locator.SelectOptionAsync(value);
    }
}
