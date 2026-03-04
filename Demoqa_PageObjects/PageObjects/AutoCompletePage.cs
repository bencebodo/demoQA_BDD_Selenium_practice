using Demoqa_PageObjects.PageObjects.Base;
using OpenQA.Selenium;
using Serilog;

namespace Demoqa_PageObjects.PageObjects
{
    public class AutoCompletePage : BasePage
    {
        private IWebElement multipleColor_field => GetElement(By.XPath("//div[@id = \"autoCompleteMultipleContainer\"]/descendant::input"));
        private IWebElement delete_btn(string text) => GetElement(By.XPath($"//div[text() = '{text}']/following-sibling::div[contains(@class, 'remove')]"));

        public AutoCompletePage(IWebDriver driver, ILogger logger) : base(driver, logger) { }

        public void WriteInMultipleColorField(string input)
        {
            Log.Information("Entering {input} in Type multiple color names", input);
            multipleColor_field.SendKeys(input);
        }

        public void WriteInMultipleColorField(HashSet<string> colors)
        {
            foreach (string color in colors)
            {
                Log.Information("Entering {input} in Type multiple color field", color);
                multipleColor_field.SendKeys(color);
                multipleColor_field.SendKeys(Keys.Enter);
            }
        }

        public int GetSuggestionCount() => GetSuggestions().Count();

        public HashSet<string> GetSuggestionTexts()
        {
            Log.Information("Collecting presented suggestions data.");
            IList<IWebElement> suggestions = GetSuggestions();
            HashSet<string> suggestionResults = suggestions.Select(e => e.Text.ToLower()).ToHashSet();
            Log.Debug("Suggested colors: {@colors}", suggestionResults);
            return suggestionResults;
        }

        public HashSet<string> GetColorTexts()
        {
            IList<IWebElement> enteredColors = GetEnteredColors();
            Log.Information("Collecting entered data");
            HashSet<string> colors = enteredColors.Select(e => e.Text).ToHashSet();
            Log.Debug("Entered colors: {@colors}", colors);
            return colors;
        }

        public void DeleteFromField(HashSet<string> colors)
        {
            foreach (string color in colors)
            {
                Log.Information("Deleting {color} from entered in Type multiple color field", color);
                delete_btn(color).Click();
            }
        }

        private IList<IWebElement> GetSuggestions() => GetElementsOrEmpty(By.CssSelector(".auto-complete__option"));
        private IList<IWebElement> GetEnteredColors() => GetElementsOrEmpty(By.CssSelector(".auto-complete__multi-value__label"));
    }
}
