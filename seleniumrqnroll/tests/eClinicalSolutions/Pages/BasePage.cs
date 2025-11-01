
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using UiTests.Utilities;

namespace UiTests.Pages
{
    public abstract class BasePage
    {
        protected readonly IWebDriver Driver;
        protected readonly IConfiguration Config;
        protected readonly Waiter Wait;

        protected BasePage(IWebDriver driver, IConfiguration config)
        {
            Driver = driver;
            Config = config;
            Wait = new Waiter(driver, config);
        }

        public string BaseUrl => Config["baseUrl"]!;
    }
}
