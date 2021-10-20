using System;
using Microsoft.Playwright;
using NUnit.Framework;

namespace sauceDemo
{
    [SetUpFixture]
    public class Initialize
    {

        public static IPage Page;
        public static string BaseAddress;

        [SetUp]
        public static void  Setup()
        {
            BaseAddress = Environment.GetEnvironmentVariable(Constants.BASE_ADDRESS);
        }

        [TearDown]
        public static void AssemblyCleanup()
        {
            
        }
    }
}
