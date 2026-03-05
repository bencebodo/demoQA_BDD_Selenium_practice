using OpenQA.Selenium;
using Serilog;

namespace DemoQa.Pages.Components
{
    public class FormResultModal
    {
        private IWebDriver _driver;
        private ILogger _logger;

        private IWebElement _modalContent => _driver.FindElement(By.ClassName("table-responsive"));

        public FormResultModal(IWebDriver driver, ILogger logger)
        {
            _driver = driver;
            _logger = logger;
        }

        public string GetValueByLabel(string label)
        {
            string value = string.Empty;
            Log.Information("Collecting presented data from {label} row", label);
            value = _modalContent.FindElement(By.XPath($".//tr[td[text()='{label}']]//td[2]")).Text;
            Log.Debug("Collected data: {value}", value);

            return value;
        }

        public HashSet<string> GetLabels()
        {
            HashSet<string> result = new HashSet<string>();

            var rows = _modalContent.FindElements(By.XPath(".//tbody/tr"));

            foreach (var row in rows)
            {
                var cells = row.FindElements(By.TagName("td"));

                if (cells.Count > 0)
                {
                    string label = cells[0].Text;
                    string value = cells[1].Text;

                    if (label != "Label" && !string.IsNullOrWhiteSpace(value))
                    {
                        result.Add(label);
                    }
                }
            }
            return result;
        }
    }
}
