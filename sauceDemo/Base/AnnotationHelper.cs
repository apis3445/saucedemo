using System.Collections.Generic;

namespace sauceDemo.Base;

public class AnnotationHelper
{
    private List<Annotation> _annotations;
    private IReporter _reporter;

    public AnnotationHelper(IReporter reporter)
    {
        _annotations = new List<Annotation>();
        _reporter = reporter;
    }

    public void AddAnnotation(AnnotationType annotationType, string description)
    {
        Annotation annotation = new Annotation();
        annotation.Description = description;
        annotation.AnnotationType = annotationType;
        this._annotations.Add(annotation);
        this.PrintAnnotation(annotation);
    }

    public void ClearAnnotations()
    {
        this._annotations.Clear();
    }

    public List<Annotation> GetAnnotations()
    {
        return this._annotations;
    }

    public void PrintAnnotation(Annotation annotation)
    {
        _reporter.PrintAnnotation(annotation);
    }
}

