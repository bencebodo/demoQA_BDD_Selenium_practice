using Demoqa_BDD.Context;
using Demoqa_PageObjects.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using Serilog;

namespace Demoqa_BDD.StepDefinitions
{
    [Binding]
    public class WidgetsStepDefinitions
    {
        private readonly ILogger _logger;
        private readonly IWebDriver _driver;
        private readonly WebDriverContext _context;
        private ProgressBarPage _progressBarPage;
        private AutoCompletePage _autoCompletePage;

        public WidgetsStepDefinitions(WebDriverContext context, ILogger logger, ProgressBarPage progressBarPage, AutoCompletePage autoCompletePage)
        {
            _logger = logger;
            _context = context;
            _driver = context.Driver;
            _progressBarPage = progressBarPage;
            _autoCompletePage = autoCompletePage;
        }

        [When("I enter {string} in the Type multiple color names field")]
        public void WhenIEnterInTheTypeMultipleColorNamesField(string g)
        {
            _autoCompletePage.WriteInMultipleColorField(g);
        }

        [Then("the auto complete should suggest {int} variants")]
        public void ThenTheAutoCompleteShouldSuggestVariants(int p0)
        {
            int suggestionsCount = _autoCompletePage.GetSuggestionCount();

            Assert.That(suggestionsCount, Is.EqualTo(p0));
        }

        [Then("all displayed suggestions should contain the letter {string}")]
        public void ThenAllDisplayedSuggestionsShouldContainTheLetter(string g)
        {
            HashSet<string> actualSuggestions = _autoCompletePage.GetSuggestionTexts();

            Assert.That(actualSuggestions, Has.All.Contain(g));
        }

        [When("I enter the following colors and accept suggestions in the Type multiple color names field:")]
        public void WhenIEnterTheFollowingColorsAndAcceptSuggestionsInTheTypeMultipleColorNamesField(DataTable dataTable)
        {
            HashSet<string> colors = dataTable.Rows
                .Select(row => row["Value"])
                .ToHashSet();

            _autoCompletePage.WriteInMultipleColorField(colors);
        }


        [When("I delete the following colors")]
        public void WhenIDeleteTheFollowingColors(DataTable dataTable)
        {
            HashSet<string> colors = dataTable.Rows
                .Select(row => row["Value"])
                .ToHashSet();

            _autoCompletePage.DeleteFromField(colors);
        }


        [Then("the Type multiple color names field should display the following colors")]
        public void ThenTheTypeMultipleColorNamesFieldShouldDisplayTheFollowingColors(DataTable dataTable)
        {
            HashSet<string> exptectedColors = dataTable.Rows
                .Select(row => row["Value"])
                .ToHashSet();

            HashSet<string> actualColorTexts = _autoCompletePage.GetColorTexts();

            Assert.That(actualColorTexts, Is.EqualTo(exptectedColors), "Collection of entered colors does not equals to collection of expected colors");
        }



        [When("I press {string} button and wait for the progress bar to reach {string}")]
        public void WhenIPressButtonAndWaitForTheProgressBarToReach(string start, string p1)
        {
            _progressBarPage.CLickOnStartButton();
            _progressBarPage.WaitForProgressBarToComplete();
        }

        [Then("the button should change to {string}")]
        public void ThenTheButtonShouldChangeTo(string reset)
        {
            string buttonText = _progressBarPage.GetStartStopBtnText();

            Assert.That(buttonText, Is.EqualTo(reset), $"Action button did not change to {reset}");
        }


        [When("I press {string} button")]
        public void WhenIPressButton(string reset)
        {
            _progressBarPage.CLickOnStartButton();
        }

        [Then("the progress bar should show {string} completion")]
        public void ThenTheProgressBarShouldShowCompletion(string p0)
        {
            string progressValue = _progressBarPage.GetProgressBarValueText();

            Assert.That(progressValue, Is.EqualTo(p0), $"Progress bar did not reached {p0}");
        }
    }
}
