
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

namespace UiTests.Utilities
{
    public static class ElementExtensions
    {
        public static void DoubleClick(this IWebDriver driver, IWebElement element)
        {
            new Actions(driver).DoubleClick(element).Perform();
        }

        public static void ScrollIntoView(this IWebDriver driver, IWebElement element, bool alignToTop = true)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(arguments[1]);", element, alignToTop);
        }

        public static void ScrollBy(this IWebDriver driver, int x, int y)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript($"window.scrollBy({x}, {y});");
        }

        public static void SafeClick(this IWebDriver driver, IWebElement element)
        {
            try { element.Click(); }
            catch { ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", element); }
        }
        
        public static void ClickBy(this IWebDriver driver, By locator)
        {
            try
            {
                driver.FindElement(locator).Click();
            }
            catch
            {
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", driver.FindElement(locator));
            }
        }

        public static void TypeText(this IWebElement element, string text, bool clear = true)
        {
            if (clear) element.Clear();
            element.SendKeys(text);
        }
    }
}
