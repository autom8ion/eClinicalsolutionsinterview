using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;

namespace UITests.Automation.Support
{
    /// <summary>
    /// Scenario-scoped context holding configuration and WebDriver.
    /// </summary>
    public class AutomationContext
    {
        public IConfiguration Config { get; }
        public IWebDriver? Driver { get; set; }
        public string BrowserName { get; set; } = "chrome";   // default

        public string BaseUrl =>
            Config["baseUrl"]
            ?? Config["ui:baseUrl"]
            ?? "https://www.eclinicalsol.com/";

        public bool Headless =>
            ((Config["headless"] ?? Config["ui:headless"] ?? "false")
                .ToLowerInvariant()) == "true";

        public AutomationContext(IConfiguration config)
        {
            Config = config;
        }
    }
}
