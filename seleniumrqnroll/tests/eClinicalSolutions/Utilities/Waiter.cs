
using System;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace UiTests.Utilities
{
    public class Waiter
    {
        private readonly WebDriverWait _wait;
        public Waiter(IWebDriver driver, IConfiguration config)
        {
            var timeout = double.TryParse(config["explicitWaitSeconds"], out var e) ? e : 15;
            _wait = new WebDriverWait(new SystemClock(), driver, TimeSpan.FromSeconds(timeout), TimeSpan.FromMilliseconds(200));
        }

        public IWebElement UntilVisible(By by) => _wait.Until(ExpectedConditions.ElementIsVisible(by));
        public IWebElement UntilClickable(By by) => _wait.Until(ExpectedConditions.ElementToBeClickable(by));
        public bool UntilTextPresent(By by, string text) => _wait.Until(d => d.FindElement(by).Text.Contains(text, StringComparison.OrdinalIgnoreCase));
        public bool UntilUrlContains(string part) => _wait.Until(d => d.Url.Contains(part, StringComparison.OrdinalIgnoreCase));
    }
}
