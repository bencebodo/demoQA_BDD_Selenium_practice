using Demoqa_BDD.Context;
using Demoqa_PageObjects.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using Serilog;

namespace Demoqa_BDD.StepDefinitions
{
    [Binding]
    public class InteractionsStepDefinitions
    {
        private readonly ILogger _logger;
        private readonly IWebDriver _driver;
        private readonly WebDriverContext _context;
        private SelectablePage _selectablePage;

        public InteractionsStepDefinitions(WebDriverContext context, ILogger logger, SelectablePage selectablePage)
        {
            _logger = logger;
            _context = context;
            _driver = context.Driver;
            _selectablePage = selectablePage;
        }
        [When("I navigate to the {string} and select squares with the following values:")]
        public void WhenINavigateToTheAndSelectSquaresWithTheFollowingValues(string grid, DataTable dataTable)
        {
            _selectablePage.NavigateToTabByName(grid);

            HashSet<string> squares = dataTable.Rows
                                               .Select(row => row["Value"])
                                               .ToHashSet();
            _context.SelectedItems = squares;
            _selectablePage.ClickOnGridElementByName(squares);
        }

        [Then("the selected items should match the selected values")]
        public void ThenTheSelectedItemsShouldMatchTheSelectedValues()
        {
            HashSet<string> selectedElements = _selectablePage.GetSelectedElements();

            Assert.That(selectedElements, Is.EquivalentTo(_context.SelectedItems), "Collection of selected elements are not equal to expected collection");
        }
    }
}
