
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using UiTests.Utilities;

namespace UiTests.Pages
{
    public class CareersPage : BasePage
    {
        public CareersPage(IWebDriver driver, IConfiguration config) : base(driver, config) { }

        private By OpenPositions => By.CssSelector("a[class='button external-link'] span");

        public GreenHousePage ViewOpenPositions()
        {
            var positionsButton = Wait.UntilClickable(OpenPositions);
            Driver.SafeClick(positionsButton);
            return new GreenHousePage(Driver, Config);
        }

    }
}
