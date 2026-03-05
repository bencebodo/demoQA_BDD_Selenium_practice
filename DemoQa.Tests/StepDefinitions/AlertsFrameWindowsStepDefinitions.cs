using DemoQa.Pages.Pages;
using DemoQa.Tests.Context;
using NUnit.Framework;
using OpenQA.Selenium;
using Serilog;

namespace DemoQa.Tests.StepDefinitions
{
    [Binding]
    public class AlertsFrameWindowsStepDefinitions
    {
        private readonly ILogger _logger;
        private readonly IWebDriver _driver;
        private readonly WebDriverContext _context;
        private BrowserWindowsPage _browserWindowPage;

        public AlertsFrameWindowsStepDefinitions(WebDriverContext context, ILogger logger, BrowserWindowsPage browserWindowPage)
        {
            _logger = logger;
            _context = context;
            _driver = context.Driver;
            _browserWindowPage = browserWindowPage;
        }

        [When("I press the {string} button")]
        public void WhenIPressTheButton(string p0)
        {
            _browserWindowPage.ClickOnElementByName(p0);
        }

        [Then("the {string} should be displayed in a {string}")]
        public void ThenTheShouldBeDisplayedInA(string p0, string windowType)
        {
            string actualText = _browserWindowPage.GetTextOnNewWindow(windowType);

            Assert.That(actualText, Is.EqualTo(p0), $"{p0} is not present in {windowType}");
        }
    }
}
