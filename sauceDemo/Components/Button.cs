using System.Threading.Tasks;
using Microsoft.Playwright;
using sauceDemo.Base;

namespace sauceDemo.Components;

public class Button : BaseLocator
{
    public Button(IPage page, string locator, AnnotationHelper annotationHelper) : base(page, locator, annotationHelper)
    {
            
    }

    public async Task ClickAsync()
    {
        await this.GetLabelAsync();
        this.AnnotationHelper.AddAnnotation(AnnotationType.Step, "Click in the button: '" + this.label + "'");
        await this.Locator.HighlightAsync();
        await this.Locator.ClickAsync();
    }

    public override async Task<string> GetLabelAsync()
    {
        if (this.label == null)
            label = await this.Locator.GetAttributeAsync("value");
        return label;
    }

}

