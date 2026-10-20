# SauceDemo Automation Framework

UI test automation for [saucedemo.com](https://www.saucedemo.com), built with **C#, Selenium WebDriver, Reqnroll (BDD) and NUnit** on .NET 8. Tests run locally in VS Code and automatically on every push through GitHub Actions.

## Tech Stack

| Tool | Purpose |
|------|---------|
| Selenium WebDriver 4.x | Browser automation (driver is resolved automatically by Selenium Manager) |
| Reqnroll | BDD: Gherkin `.feature` files linked to C# step definitions |
| NUnit | Test runner and assertions |
| Microsoft.Extensions.Configuration | Reads settings from JSON and environment variables |
| GitHub Actions | CI pipeline |

## Project Structure

```
SauceDemoProject/
├── .github/workflows/ci.yml        # CI pipeline
├── .gitignore
├── README.md
└── SauceDemoProject.Tests/
    ├── Feature/                    # Gherkin scenarios
    │   ├── Login.feature
    │   └── Cart.feature
    ├── StepDefinitions/            # Glue between Gherkin and page objects
    │   ├── LoginSteps.cs
    │   └── CartSteps.cs
    ├── PageObject/                 # Page Object Model
    │   ├── BasePage.cs             # Shared waits, click, type, get text
    │   ├── LoginPage.cs
    │   └── InventoryPage.cs
    ├── Hooks/
    │   └── DriverHooks.cs          # Starts and quits the browser per scenario
    ├── Helper/
    │   ├── Config.cs               # Reads settings.json + environment variables
    │   └── DriverFactory.cs        # Builds the configured ChromeDriver
    ├── settings.json               # Base URL, browser, timeouts, headless flag
    └── SauceDemoProject.Tests.csproj
```

## How It Fits Together

1. **Feature files** describe behaviour in plain English (Given / When / Then).
2. **Step definitions** match each step by regex and call page object methods. They contain no Selenium locators.
3. **Page objects** hold the locators and actions for one page each. They inherit shared wait/click/type helpers from `BasePage`.
4. **Hooks** create a fresh browser before each scenario and close it afterwards.
5. **Config** reads `settings.json`, then lets environment variables override it (this is how CI switches on headless mode).

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- Google Chrome
- VS Code with the **C# Dev Kit** and **Reqnroll** extensions

## Setup

```powershell
cd SauceDemoProject.Tests
dotnet restore
dotnet build
```

## Running Tests

Run everything (Chrome opens visibly by default):
```powershell
dotnet test
```

Run headless locally:
```powershell
$env:TestSettings__Headless = "true"
dotnet test
```

## Configuration

`SauceDemoProject.Tests/settings.json`:

```json
{
  "TestSettings": {
    "BaseUrl": "https://www.saucedemo.com",
    "Browser": "Chrome",
    "Headless": false,
    "ImplicitWaitSeconds": 5,
    "ExplicitWaitSeconds": 10
  }
}
```

Any value can be overridden with an environment variable. A double underscore stands in for the nesting, so `TestSettings__Headless=true` overrides `TestSettings:Headless`.

## CI/CD

`.github/workflows/ci.yml` runs on every push and pull request to `main`:

1. Checks out the code
2. Sets up .NET 8
3. Restores and builds the project
4. Runs all tests headless on an Ubuntu runner (`TestSettings__Headless=true`)

Results appear in the **Actions** tab of the repository.

## Current Coverage

| Feature | Scenarios |
|---------|-----------|
| Login | Standard user logs in successfully, locked-out user sees an error |
| Cart | Add a single item and verify the cart badge count |

## Known Issue (Resolved)

CI initially failed with `session not created: Chrome instance exited` because Chrome was launching in visible mode on a runner with no display. See `CI_Chrome_Headless_Issue_Report.md` for the full write-up.

## Roadmap

- More cart scenarios (remove item, add multiple items) and a `CartPage`
- Checkout flow
- Product sorting
- Test data moved out of feature files into JSON
- HTML reporting with screenshots on failure