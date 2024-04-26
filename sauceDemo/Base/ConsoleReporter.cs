using System;
namespace sauceDemo.Base;

public class ConsoleReporter : IReporter
{

    public void PrintAnnotation(Annotation annotation)
    {
        if (annotation.AnnotationType == AnnotationType.Description || annotation.AnnotationType == AnnotationType.Name)
            Console.WriteLine(annotation.AnnotationType + ": " + annotation.Description);
        else
            Console.WriteLine(annotation.Description);
    }
}

