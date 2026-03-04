using Demoqa_PageObjects.PageObjects.Base;
using OpenQA.Selenium;
using Serilog;

namespace Demoqa_PageObjects.PageObjects
{
    public class SelectablePage : BasePage
    {
        private IWebElement dynamicTab(string name) => GetElement(By.XPath($"//button[contains(@id, 'demo-tab') and text() = '{name}']"));

        public SelectablePage(IWebDriver driver, ILogger logger) : base(driver, logger) { }

        public void NavigateToTabByName(string name)
        {
            Log.Information("Navigating to tab: {tabName}", name);
            dynamicTab(name).Click();
        }

        public void ClickOnGridElementByName(HashSet<string> names)
        {
            foreach (string name in names)
            {
                Log.Information("Selecting {name} grid element", name);
                var element = GetElement(name, ".list-group-item");
                try
                {
                    element.Click();
                }
                catch (NoSuchElementException) 
                {
                    Log.Error("\"{name}\" grid element is not present or not visible.");
                }
            }
        }

        public HashSet<string> GetSelectedElements()
        {
            HashSet<string> selectedElementsText = new HashSet<string>();
            IList<IWebElement> activeElements = GetElementsOrEmpty(By.CssSelector(".list-group-item.active"));
            Log.Information("Collecting active grid elements");
            foreach (IWebElement element in activeElements)
            {
                selectedElementsText.Add(element.Text);
            }
            return selectedElementsText;
        }
    }
}
