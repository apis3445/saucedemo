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
        public static string BaseAddress;

        public static TestContext TestContext { get; private set; }

        [AssemblyInitialize]
        public static void  AssemblyInitialize(TestContext context)
        {
            BaseAddress = Environment.GetEnvironmentVariable(Constants.BASE_ADDRESS);
            TestContext = context;
        }

        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            
        }
    }
}
