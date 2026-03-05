using DemoQa.Core.Extensions;
using DemoQa.Pages.Components;
using DemoQa.Pages.Pages.Base;
using OpenQA.Selenium;
using Serilog;

namespace DemoQa.Pages.Pages
{
    public class WebTablesPage : BasePage
    {
        private WebTableComponent Table;

        public WebTablesPage(IWebDriver driver, ILogger logger) : base(driver, logger)
        {
            Table = new WebTableComponent(_driver);
        }

        public void ClickOnHeader(string headerName)
        {
            Log.Information("Clicking on {headerName} header", headerName);
            IWebElement header = GetElement(headerName, ".thead.th");
            header.ClickWithScroll(_driver);
        }

        public void DeleteRowByData(string headerName, string text)
        {
            Log.Information("Deleting {text} from {headerName} coloumn", text, headerName);
            Table.DeleteButtonByData(headerName, text).Click();
        }

        public bool IsDataPresent(string text, string headerName)
        {
            Log.Information("Verifying {text} is not present in {headerName} coloumn", text, headerName);
            List<string> headerTexts = Table.GetColoumnTexts(headerName);
            bool IsPresent = false;

            foreach (string headerText in headerTexts)
            {
                if (!string.IsNullOrWhiteSpace(text))
                {
                    if (headerText == text)
                    {
                        IsPresent = true;
                    }
                }
            }
            Log.Debug("Is {text} present in {headerName} coloumn? {bool}", text, headerName, IsPresent);
            return IsPresent;
        }

        public List<string> GetAllTexts()
        {
            Log.Information("Collecting all data from table");
            IList<IWebElement> headerNameCells = Table.GetAllElements_FromTable();
            List<string> cellText = new List<string>();

            foreach (var cell in headerNameCells)
            {
                if (!string.IsNullOrWhiteSpace(cell.Text))
                {
                    cellText.Add(cell.Text);
                }
            }
            return cellText;
        }

        public List<int> GetColumnValues(string headerName)
        {
            Log.Information("Collecting data from {headerName} coloumn", headerName);
            List<string> texts = Table.GetColoumnTexts(headerName);
            List<int> values = new List<int>();
            int value;

            foreach (string text in texts)
            {
                if (!string.IsNullOrWhiteSpace(text))
                {
                    value = int.Parse(text);
                    values.Add(value);
                }
            }
            Log.Debug("Collected data: {@value}", values);
            return values;
        }
    }
}
