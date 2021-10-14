# saucedemo

POC to E2E test with Playwright

## Setup Playwright

For installl Playwright for C#

1. Install the Playwright tool with the next command 
```console
  dotnet tool install --global Microsoft.Playwright.CLI 
```
 
 2. Install Playwright (this include the browsers Chromium, Firefox, Webkit)
 ```console
     playwright install
   dotnet tool install --global Microsoft.Playwright.CLI 
```

## Run tests

You can run the test from Windows, MacOS or with Azure Devops Pipeline

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

1. Go to pipelines
2. Right click in the ... button and click in Run Pipeline
3. Change the variables if is needed
4. Click in Run
