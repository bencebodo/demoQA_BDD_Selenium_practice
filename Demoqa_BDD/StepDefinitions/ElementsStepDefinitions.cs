using Demoqa_BDD.Context;
using Demoqa_PageObjects.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using Serilog;

namespace Demoqa_BDD.StepDefinitions
{
    [Binding]
    public class ElementsStepDefinitions
    {
        private readonly ILogger _logger;
        private readonly IWebDriver _driver;
        private readonly WebDriverContext _context;
        private ButtonsPage _buttonsPage;
        private TextBoxPage _textBoxPage;
        private CheckBoxPage _checkBoxPage;
        private WebTablesPage _webTablesPage;

        public ElementsStepDefinitions(WebDriverContext context, ILogger logger, ButtonsPage buttonsPage, TextBoxPage textBoxPage, CheckBoxPage checkBoxPage, WebTablesPage webTablesPage)
        {
            _logger = logger;
            _context = context;
            _driver = context.Driver;
            _buttonsPage = buttonsPage;
            _textBoxPage = textBoxPage;
            _checkBoxPage = checkBoxPage;
            _webTablesPage = webTablesPage;
        }

        // EZ HELYETTESÍTI AZ ÖSSZES KORÁBBI [When] METÓDUSODAT!
        [When(@"I select the following folders in the tree:")]
        public void WhenISelectTheFollowingFoldersInTheTree(Table table)
        {
            foreach (var row in table.Rows)
            {
                string rawPath = row["Folder Path"];

                string[] pathArray = rawPath.Split('>').Select(p => p.Trim()).ToArray();

                _checkBoxPage.ExpandPathAndCheck(pathArray);
            }
        }

        [Then(@"the output area should display the following selected items:")]
        public void ThenTheOutputAreaShouldDisplayTheFollowingSelectedItems(Table table)
        {
            string actualResult = _checkBoxPage.GetResultText();

            Assert.Multiple(() =>
            {
                foreach (var row in table.Rows)
                {
                    string expectedItem = row["Item"];
                    Assert.That(actualResult, Does.Contain(expectedItem),
                        $"{expectedItem} cannot be found on page. Actual result: {actualResult}");
                }
            });
        }

        [When("I delete the row where the {string} column shows {string}")]
        public void WhenIDeleteTheRowWhereTheColumnShows(string p0, string alden)
        {
            _webTablesPage.DeleteRowByData(p0, alden);
        }

        [Then("the table should display {int} rows")]
        public void ThenTheTableShouldDisplayRows(int p0)
        {
            int actualRows = _webTablesPage.GetAllTexts().Count();

            Assert.That(actualRows, Is.EqualTo(p0), $"Table rows number is not equal to {p0}. Actual rows = {actualRows}");
        }

        [Then("{string} should not be displayed in {string} column")]
        public void ThenShouldNotBeDisplayedInColumn(string compliance, string department)
        {
            bool isElementPresent = _webTablesPage.IsDataPresent(compliance, department);

            Assert.That(isElementPresent, Is.False, $"{compliance} is still present in {department} coloumn.");
        }

        [When("I perform the required action to {string} button")]
        public void WhenIPerformTheRequiredActionToButton(string p0)
        {
            _buttonsPage.PerformAction(p0);
        }

        [Then("the message {string} should be displayed")]
        public void ThenTheMessageShouldBeDisplayed(string p0)
        {
            bool messageIsDisplayed = _buttonsPage.VerifyResult(p0);

            Assert.That(messageIsDisplayed, Is.True, $"\"{p0}\" is not displayed.");

        }
    }
}
