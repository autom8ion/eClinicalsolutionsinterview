using System;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace UiTests.Support
{
    public static class DriverFactory
    {
        public static IWebDriver Create(string browser, IConfiguration config)
        {
            bool headless = TryReadBool(config["ui:headless"], true);
            int pageLoadTimeoutSec = TryReadInt(config["ui:pageLoadTimeoutSec"], 30);
            int implicitWaitMs = TryReadInt(config["ui:implicitWaitMs"], 0);

            IWebDriver driver = browser.ToLower() switch
            {
                "chrome"  => CreateChrome(headless),
                "edge"    => CreateEdge(headless),
                _         => throw new ArgumentException($"Unsupported browser: {browser}")
            };

            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(pageLoadTimeoutSec);
            if (implicitWaitMs > 0)
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(implicitWaitMs);

            driver.Manage().Window.Size = new System.Drawing.Size(1536, 960);
            return driver;
        }

        private static ChromeDriver CreateChrome(bool headless)
        {
            try { new DriverManager().SetUpDriver(new ChromeConfig()); } catch {}
            var options = new ChromeOptions();
            if (headless) options.AddArgument("--headless=new");
            options.AddArgument("--no-sandbox");
            options.AddArgument("--disable-gpu");
            options.AddArgument("--window-size=1536,960");
            return new ChromeDriver(options);
        }

        private static EdgeDriver CreateEdge(bool headless)
        {
            try { new DriverManager().SetUpDriver(new EdgeConfig()); } catch { }
            var options = new EdgeOptions();
            if (headless) options.AddArgument("headless=new");
            options.AddArgument("window-size=1536,960");
            return new EdgeDriver(options);
        }
        private static bool TryReadBool(string? value, bool @default)
            => bool.TryParse(value, out var b) ? b : @default;

        private static int TryReadInt(string? value, int @default)
            => int.TryParse(value, out var i) ? i : @default;
    }
}
