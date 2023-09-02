using System;
namespace sauceDemo.Base
{
	public class ConsoleReporter : IReporter 
	{

        public void PrintAnnotation(Annotation annotation)
        {
            if (annotation.AnnotationType == AnnotationType.Description)
                Console.WriteLine(AnnotationType.Description + ":" + annotation.Description);
            else
                Console.WriteLine(annotation.Description);
        }
    }
}

