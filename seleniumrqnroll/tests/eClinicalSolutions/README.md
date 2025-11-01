
# UiTests — Selenium + Reqnroll + NUnit + Allure

## Requirements
- Reqnroll (Cucumber for .NET) + NUnit runner
- Selenium 4 (Selenium Manager auto-manages drivers)
- Page Objects with `BasePage`
- FluentAssertions helpers
- Element extension methods (double click, scrolling, safe click, typing)
- Centralized waits (`Waiter`)
- `appsettings.json` (browser, baseUrl, waits, headless, artifacts)
- Parallelization via `[assembly: Parallelizable]`
- Allure reporting with screenshots on step/Scenario failure

## Build & run
```bash
dotnet restore
dotnet test eClinicalSolutions.csproj -c Release
```

### Allure report
This uses `Allure.NUnit`. After running tests, generate the report with the Allure CLI if installed:
```bash
# example
allure generate --clean
allure open
```
(Adapters write results to `bin/.../allure-results` by default.)

> **NOTE:** Update CSS selectors in `Pages/*.cs` to match the current eclinicalsol.com careers portal before running.
