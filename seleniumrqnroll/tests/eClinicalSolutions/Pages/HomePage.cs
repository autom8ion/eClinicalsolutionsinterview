
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using UiTests.Utilities;

namespace UiTests.Pages
{
    public class HomePage : BasePage
    {
        public HomePage(IWebDriver driver, IConfiguration config) : base(driver, config) { }

        // TODO: Update selectors to match the live site
        private By eClinicalLogo => By.CssSelector("div.logo");
        private By CareersLink => By.CssSelector(".footer-ctn a[href*=\"careers\"]");

        public HomePage GoTo()
        {
            var HomePage = Wait.UntilVisible(eClinicalLogo);
            return this;
        }

        public CareersPage OpenCareers()
        { 
            var link = Wait.UntilClickable(CareersLink);
            Driver.SafeClick(link);
            return new CareersPage(Driver, Config);
        }
    }
}
