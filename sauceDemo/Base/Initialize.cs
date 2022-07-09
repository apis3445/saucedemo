using System;
using Microsoft.Playwright;
using NUnit.Framework;

namespace sauceDemo;

[SetUpFixture]
public class Initialize
{

    public static IPage Page;
    public static string BaseAddress;

    [OneTimeSetUp]
    public static void  Setup()
    {
       BaseAddress = Environment.GetEnvironmentVariable(Constants.BASE_ADDRESS) ?? "https://www.saucedemo.com/";
    }

    [OneTimeTearDown]
    public static void AssemblyCleanup()
    {
        
    }
}
