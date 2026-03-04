using Demoqa_BDD.Context;
using Demoqa_PageObjects.Models;
using Demoqa_PageObjects.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using Serilog;
using System.Xml.Linq;

namespace Demoqa_BDD.StepDefinitions
{
    [Binding]
    public class CommonStepDefinitions
    {
        private readonly ILogger _logger;
        private readonly IWebDriver _driver;
        private readonly WebDriverContext _context;
        private HomePage _homePage;
        private TextBoxPage _textBoxPage;
        private PracticeFormPage _formPage;

        public CommonStepDefinitions(WebDriverContext context, ILogger logger, HomePage homePage, TextBoxPage textBoxPage, PracticeFormPage formPage)
        {
            _logger = logger;
            _context = context;
            _driver = context.Driver;
            _formPage = formPage;
            _textBoxPage = textBoxPage;
            _homePage = homePage;
        }

        [Given("I'm on the Home page of Demoqa")]
        public void GivenImOnTheHomePageOfDemoqa()
        {
            Log.Information("Navigating to Home Page ({baseUrl})", _context.BaseUrl);
            _driver.Navigate().GoToUrl(_context.BaseUrl);
            Log.Debug("Current URL: {Url}", _driver.Url);
        }

        [Given("I navigate to the {string} section")]
        public void GivenINavigateToTheSection(string elements)
        {
            Log.Information("Navigating to {section} section", elements);
            _homePage.NavigateToSectionByName(elements);
            Log.Debug("Current URL: {Url}", _driver.Url);
            
        }

        [When("I navigate to the {string} subsection")]
        public void WhenINavigateToTheSubsection(string p0)
        {
            Log.Information("Navigating to {subsection} subsection", p0);
            _homePage.NavigateToSubsectionByName(p0);
            Log.Debug("Current URL: {Url}", _driver.Url);
            
        }

        [When("I fill the form with the following data:")]
        public void WhenIFillTheFormWithTheFollowingData(DataTable datatable)
        {
            Dictionary<string, string> inputData = datatable.Rows.ToDictionary(row => row["Field"], row => row["Value"]);
            _context.FormData = inputData;
            if (_driver.Url.Contains("automation-practice-form"))
                _formPage.FillForm(inputData);
            else
                _textBoxPage.FillForm(inputData);
        }

        [When("I submit the form")]
        public void WhenISubmitTheForm()
        {
            if (_driver.Url.Contains("automation-practice-form"))
                _formPage.SubmitForm();
            else
                _textBoxPage.SubmitForm();
        }

        [Then("the output area should display the submitted information")]
        public void ThenTheOutputAreaShouldDisplayTheSubmittedInformation()
        {
            Dictionary<string, string> inputData = _context.FormData;



            if (_driver.Url.Contains("automation-practice-form"))
            {
                HashSet<string> labels = _formPage.Results.GetLabels();

                var expectedResultsDictionary = new StudentFormData(inputData).GetExpectedResultMap(labels);

                foreach (var label in labels)
                {
                    string expectedValue = expectedResultsDictionary[label];
                    string actualValue = _formPage.Results.GetValueByLabel(label);

                    Assert.That(actualValue, Is.EqualTo(expectedValue), $"{expectedValue} is not present. Actual result = {actualValue}");
                }
            }
            else
            {
                string outputFullName = _textBoxPage.Results.Name;
                string outputEmail = _textBoxPage.Results.Email;
                string outputCurrentAddress = _textBoxPage.Results.CurrentAddress;
                string outputPermanentAddress = _textBoxPage.Results.PermanentAddress;

                Assert.That(outputFullName, Does.Contain(inputData["Full Name"]), "Full Name does not match.");
                Assert.That(outputEmail, Does.Contain(inputData["Email"]), "Email does not match.");
                Assert.That(outputCurrentAddress, Does.Contain(inputData["Current Address"]), "Current address does not match.");
                Assert.That(outputPermanentAddress, Does.Contain(inputData["Permanent Address"]), "Permanent address does not match.");
            }
        }
    }
}
