
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using UiTests.Utilities;

namespace UiTests.Pages
{
    public class JobDetailsPage : BasePage
    {
        public JobDetailsPage(IWebDriver driver, IConfiguration config) : base(driver, config) {}
        private By ApplyButton => By.CssSelector("a[href*='apply'], button");

        public ApplicationPage StartApplication()
        {
            var btn = Wait.UntilClickable(ApplyButton);
            Driver.SafeClick(btn);
            return new ApplicationPage(Driver, Config);
        }
    }
}
