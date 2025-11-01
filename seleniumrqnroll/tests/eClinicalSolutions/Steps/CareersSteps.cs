using Allure.NUnit.Attributes;
using Reqnroll;
using OpenQA.Selenium;
using Microsoft.Extensions.Configuration;
using UITests.Automation.Support;
using UiTests.Pages;

namespace UiTests.Steps
{
    [Binding]
    [AllureSuite("UI")]
    [AllureFeature("Application")]
    public class eClicnicalSteps
    {
        private readonly AutomationContext _ctx;
        public eClicnicalSteps(AutomationContext ctx) { _ctx = ctx; }
        
        [AllureStep]
        [Given("I am on the home page")]
        public void GivenIAmOnTheHomePage()
        {
            new HomePage(_ctx.Driver!, _ctx.Config).GoTo();
        }

        [AllureStep]
        [When(@"I search careers for (.*)")]
        public void WhenISearchCareersFor(string term)
        {
            var homePage = new HomePage(_ctx.Driver!, _ctx.Config);
            homePage.OpenCareers();

            var careers = new CareersPage(_ctx.Driver!, _ctx.Config);
            careers.ViewOpenPositions();

            var greenhouse = new GreenHousePage(_ctx.Driver!, _ctx.Config);
            greenhouse.SearchJobs(term);
        }
    }
}
