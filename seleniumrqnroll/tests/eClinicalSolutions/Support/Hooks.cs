using System;
using Allure.Commons;
using AngleSharp.Common;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using Reqnroll;
using UITests.Automation.Support;

namespace UiTests.Support
{
    [Binding]
    public class Hooks
    {
        private static readonly AllureLifecycle Allure = AllureLifecycle.Instance;
        private readonly AutomationContext _ctx;
        private readonly ScenarioContext _scenario;
        private readonly IConfiguration _config;
        
        public Hooks(AutomationContext ctx, ScenarioContext scenario, IConfiguration config)
        {
            _ctx = ctx;
            _scenario = scenario;
            _config = config;
        }

        [BeforeScenario(Order = 0)]
        public void BeforeScenario()
        {
            var browser = Environment.GetEnvironmentVariable("BROWSER")
                          ?? (_config["ui:browsers"] ?? "chrome").Split(',')[0].Trim();

            _ctx.BrowserName = browser;
            _ctx.Driver = DriverFactory.Create(browser, _config);

            var baseUrl = _config["ui:baseUrl"] ?? "https://www.eclinicalsol.com/";
            _ctx.Driver!.Navigate().GoToUrl(baseUrl);
        }

        [AfterScenario(Order = 100)]
        public void AfterScenario()
        {
            try
            {
                if (_scenario.TestError != null)
                {
                    AllureHelper.AddTextAttachment("Error", _scenario.TestError.ToString());
                    if (_ctx.Driver is ITakesScreenshot ts)
                    {
                        var snap = ts.GetScreenshot();
                        AllureHelper.AddScreenshot("Failure", snap.AsByteArray);
                    }
                }
            }
            catch { }
            finally
            {
                try { _ctx.Driver?.Quit(); } catch { }
                _ctx.Driver = null;
            }
        }
    }
}
