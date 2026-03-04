using Demoqa_PageObjects.Extensions;
using OpenQA.Selenium;

namespace Demoqa_PageObjects.Components
{
    public class WebTableComponent
    {
        private IWebDriver _driver;

        public WebTableComponent(IWebDriver driver)
        {
            _driver = driver;
        }

        public IList<IWebElement> GetAllElements_FromTable()
        {
            string salaryColumnSelector = "tbody tr";
            return tableRoot.FindElements(By.CssSelector(salaryColumnSelector));
        }

        private IWebElement tableRoot => _driver.FindElement(By.CssSelector(".table.table-striped"));

        public List<string> GetColoumnTexts(string headerName)
        {
            var headers = tableRoot.FindElements(By.CssSelector("thead tr th"));
            List<string> cellText = new List<string>();

            foreach (var cell in headers)
            {
                cellText.Add(cell.Text);
            }

            return cellText;
        }

        public IWebElement DeleteButtonByData(string headerName, string text)
        {
            int rowIndex = GetRowIndex(headerName, text);
            string removeId = GetDeleteIds("Action")[rowIndex];

            return tableRoot.FindElement(By.Id(removeId));
        }

        public List<string> GetDeleteIds(string headerName)
        {
            string deleteSpanSelector = $"tbody tr td:nth-child({GetColumnIndex(headerName)}) span[title='Delete']";
            IList<IWebElement> headerNameCells = tableRoot.FindElements(By.CssSelector(deleteSpanSelector));
            List<string> cellText = new List<string>();

            foreach (var cell in headerNameCells)
            {
                cellText.Add(cell.GetIdOfElement());
            }

            return cellText;
        }

        public int GetRowIndex(string headerName, string text)
        {
            var headers = tableRoot.FindElements(By.CssSelector("thead tr th"));

            for (int i = 0; i < headers.Count; i++)
            {
                if (headers[i].Text.Trim() == headerName)
                {
                    return i + 1; //
                }
            }
            throw new NotFoundException($"Column with header '{headerName}' was not found.");
        }

        public int GetColumnIndex(string text)
        {
            IList<IWebElement> allElementsByClass = tableRoot.FindElements(By.CssSelector("thead th"));
            int elementIndex = 0;

            for (int i = 0; i < allElementsByClass.Count; i++)
            {
                if (allElementsByClass[i].Text == text)
                {
                    elementIndex = i;
                    break;
                }
            }

            return elementIndex + 1;
        }
    }
}
