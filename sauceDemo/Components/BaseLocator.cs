using System.Threading.Tasks;
using Microsoft.Playwright;
using sauceDemo.Base;

namespace sauceDemo.Components;

public class BaseLocator
{
    protected string label;

    protected ILocator Locator { get; set; }
    protected IPage Page { get; }
    protected AnnotationHelper AnnotationHelper { get;  }

    public BaseLocator(IPage page, string locator, AnnotationHelper annotationHelper)
    {
        this.Locator = page.Locator(locator);
        this.Page = page;
        this.AnnotationHelper = annotationHelper;
    }

    public virtual async Task<string> GetLabelAsync()
    {
        if (this.label == null)
            label = await this.Locator.GetAttributeAsync("placeholder");
        return label;
    }
}
