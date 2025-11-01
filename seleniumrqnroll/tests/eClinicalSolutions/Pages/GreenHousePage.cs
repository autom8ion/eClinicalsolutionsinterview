

using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using UiTests.Utilities;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace UiTests.Pages
{
    public class GreenHousePage : BasePage
    {
        public GreenHousePage(IWebDriver driver, IConfiguration config) : base(driver, config) { }

        private By SearchInput => By.Id("keyword-filter");

        private By FirstResult => By.CssSelector("[data-job-id], .job-result a, .search-results a");

        public GreenHousePage SearchJobs(string term)
        {
            // Capture the current window handle before clicking
            var originalWindow = Driver.CurrentWindowHandle;

            // Wait for a new window to open (Greenhouse page)
            var windowWait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            windowWait.Until(d => d.WindowHandles.Count > 1);

            // Switch to the new window
            string newWindowHandle = null;
            foreach (var handle in Driver.WindowHandles)
            {
                if (handle != originalWindow)
                {
                    newWindowHandle = handle;
                    break;
                }
            }
            if (newWindowHandle == null)
                throw new WebDriverTimeoutException("No new window found to switch to.");
            Driver.SwitchTo().Window(newWindowHandle);

            // Insert job to search for
            var box = Wait.UntilVisible(SearchInput);
            box.Clear();
            box.SendKeys(term);
            box.SendKeys(Keys.Enter);
            return this;
        }

        public JobDetailsPage ValidateResult(string jobTitle)
        {
            var job =  By.XPath($"//p[contains(normalize-space(),'{jobTitle}')]");
            Driver.ClickBy(job);
            return new JobDetailsPage(Driver, Config);
        }
    }
}
