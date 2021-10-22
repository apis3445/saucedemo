using System;
using Microsoft.Playwright;
using NUnit.Framework;

namespace sauceDemo
{
    [SetUpFixture]
    public class Initialize
    {

        public static IPage Page;
        public static string BaseAddress = "https://www.saucedemo.com/";

        [OneTimeSetUp]
        public static void  Setup()
        {
           // BaseAddress = Environment.GetEnvironmentVariable(Constants.BASE_ADDRESS);
        }

        [OneTimeTearDown]
        public static void AssemblyCleanup()
        {
            
        }
    }
}
