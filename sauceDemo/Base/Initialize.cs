using System;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using sauceDemo.Base;

namespace sauceDemo
{
    [TestClass]
    public class Initialize
    {

        public static IPage Page;

        [AssemblyInitialize]
        public static void  AssemblyInitialize(TestContext context)
        {
          
        }

        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            
        }
    }
}
