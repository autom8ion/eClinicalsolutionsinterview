
using FluentAssertions;
using OpenQA.Selenium;

namespace UiTests.Utilities
{

    public static class Validations
    {
        public static void ShouldBeVisible(this IWebElement element)
        {
            element.Displayed.Should().BeTrue("element should be visible");
        }

        public static void ShouldContainText(this IWebElement element, string expected)
        {
            element.Text.Should().ContainEquivalentOf(expected);
        }
    }
}
