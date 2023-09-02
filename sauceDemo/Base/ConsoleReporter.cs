using System;
namespace sauceDemo.Base
{
	public class ConsoleReporter : IReporter 
	{

        public void PrintAnnotation(Annotation annotation)
        {
            Console.WriteLine(annotation.Description);
        }
    }
}

