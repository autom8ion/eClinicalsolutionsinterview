using System;
using System.Linq;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using UiTests.Utilities;

namespace UiTests.Pages
{
    public class ApplicationPage : BasePage
    {
        public ApplicationPage(IWebDriver driver, IConfiguration config) : base(driver, config)
        {
        }

        private By SubmitButton => By.CssSelector("button[type='submit'], button[aria-label*='submit']");

        private By RequiredError => By.CssSelector(
            "p.helper-text.helper-text--error[data-testid$='-error'], " +
            "[data-testid$='-error'].helper-text--error, " +
            "p[id$='-error'].helper-text--error, " +
            "[aria-live='polite'].helper-text--error"
        );

        public ApplicationPage SubmitBlankApplication()
        {
            var submit = Wait.UntilClickable(SubmitButton);
            Driver.ScrollIntoView(submit);
            Driver.SafeClick(submit);
            return this;
        }

        public void ShouldSeeRequiredErrors()
        {
            var wait = new OpenQA.Selenium.Support.UI.WebDriverWait(Driver, TimeSpan.FromSeconds(12));
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException), typeof(StaleElementReferenceException));
            wait.Until(d =>
            {
                var elems = d.FindElements(RequiredError);
                foreach (var el in elems)
                {
                    try
                    {
                        if (el.Displayed)
                            return true;
                    }
                    catch (StaleElementReferenceException)
                    {
                        // retry on next poll
                    }
                }

                return false;
            });
        }
    }
}