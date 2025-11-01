using System;
using System.Linq;
using Reqnroll;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Microsoft.Extensions.Configuration;
using UiTests.Pages;
using UiTests.Support;
using Allure.NUnit.Attributes;
using UITests.Automation.Support;

namespace UiTests.Steps
{
    [Binding]
    [AllureSuite("UI")]
    [AllureFeature("Application")]
    public class ApplicationSteps
    {
        private readonly AutomationContext _ctx;
        public ApplicationSteps(AutomationContext ctx) { _ctx = ctx; }

        [AllureStep]
        [When("I open the job result by title (.*)")]
        public void WhenIOpenTheJobResult(string jobTitle)
        {
            var greenhouse= new GreenHousePage(_ctx.Driver!, _ctx.Config);
            greenhouse.ValidateResult(jobTitle);
        }

        [AllureStep]
        [When("I start the application process with blank details")]
        public void WhenIStartTheApplication()
        {
            var details = new JobDetailsPage(_ctx.Driver!, _ctx.Config);
            var app = details.StartApplication();
        }

        [AllureStep]
        [Then("I should see required field errors for a blank application")]
        public void ThenIShouldSeeRequiredFieldErrorsForABlankApplication()
        {
            var app = new ApplicationPage(_ctx.Driver!, _ctx.Config);
            app.SubmitBlankApplication().ShouldSeeRequiredErrors();
        }
    }
}
