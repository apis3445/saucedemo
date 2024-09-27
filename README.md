# saucedemo

POC to E2E test with Playwright

test

## Setup Playwright

Install Playwright for C#

1. Install the Playwright tool with the next command 
```console
  dotnet tool install --global Microsoft.Playwright.CLI 
```
 
 2. Install Playwright (this includes the browsers Chromium, Firefox, Webkit). Execute this command inside the project path. Sample path c:\Projects\sauceDemo
 ```console
   playwright install
 ```

## Run tests

You can run the test from Windows, MacOS or with Azure Devops Pipeline

You can run with different browsers

```console
    dotnet test -s chromium.runsettings
    dotnet test -s webkit.runsettings
    dotnet test -s firefox.runsettings
```

For run only some test, for example run the test that contains AddItems in the name

```console
    dotnet test --filter "Name~AddItems"
```
For get the list of filter: https://docs.microsoft.com/en-us/dotnet/core/testing/selective-unit-tests?pivots=nunit

### Visual Studio (Windows)

1. Select the .runsettings 
2. On Test Explorer select Run

### Visual Studio (Mac)

1. Open Test Panel and Right Click on SauceDemo
2. Select the option Run Test Wih -> Custom Parameters
3. Add the variables 
BASE_ADDRESS : https://www.saucedemo.com/
BROWSER_TYPE : Chromium or Firefox or Webkit

### Azure Devops

Request access to azure devops by mail with a microsoft email

1. Go to pipelines
2. Right click in the ... button and click in Run Pipeline
3. Change the variables if is needed
4. Click in Run

You can run the pipeline and manually change the variable BROWSER_TYPE
to Chromium or Firefox or Webkit to test with different browsers
and BASE_ADDRESS to test with different URLs

You can check the reports in Azure Devops
![image](https://user-images.githubusercontent.com/7475390/139554833-a91faf4d-7419-4f4e-88e4-ed8ef6646a5d.png)

## Run tests in parallel

For run test in parallel you need to add the attribute [Parallelizable] to the Test class

You can setup the number or workers

```console
    dotnet test -- NUnit.NumberOfTestWorkers=2
```
### Reports

For additional reports to Azure Devops

Integration with third party software like saucelabs, browserstack, applitools is only
supported to NodeJS (JS/Typescript)

With NUnit you can integrate with Report Portal

https://reportportal.io/

### Test Generator

To generate a new test

```console
    playwright codegen https://www.saucedemo.com/
```

To emulate an IPhone

 ```console
    playwright codegen --device="iPhone 11" wikipedia.org
```

### Tracing

The test case: Login_WithValidUser_NavigatesToProductsPageAsync includes a tracing sample

This option generates a .zip with more details about the test

More info

https://playwright.dev/dotnet/docs/trace-viewer
https://applitools.com/event/playwright-four-futuristic-features/

To check the trace

```console
    playwright show-trace trace.zip
```

### Add new test

As a best practice you can add a custom snippet to Visual Studio (windows) to generate the
code for the test cases with the next format:

UoW_InitialCondition_ExpectedResult

UoW is Unit of work

You can replace the [TestMethod] to [Test] in the xml file

You can clone the files from this repo
https://gist.github.com/osmyn/906c917653a30864cb52dee02c36c14e

Copy the filess to
%USERPROFILE%\Documents\Visual Studio 2019\Code Snippets\Visual C#\My Code Snippets

After you can write "uat" from VS and press tab to get a template for async test

```cs
[TestMethod]
public async Task UoW_InitialCondition_ExpectedResult()
{
    //Arrange
    
    //Act
    
    //Assert
    
 }
```

Or write "ut" for sync test case

```cs
[TestMethod]
public void UoW_InitialCondition_ExpectedResult()
{
	//Arrange
	$end$

	//Act


	//Assert

}]
```

[![ko-fi](https://ko-fi.com/img/githubbutton_sm.svg)](https://ko-fi.com/U7U1U2V3V)

