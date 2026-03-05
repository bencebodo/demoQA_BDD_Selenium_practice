using OpenQA.Selenium;
using Serilog;

namespace DemoQa.Pages.Components
{
    public class TextBoxResultComponent
    {
        private IWebDriver _driver;
        private ILogger _logger;

        private IWebElement _outputDiv => _driver.FindElement(By.Id("output"));

        public TextBoxResultComponent(IWebDriver driver, ILogger logger)
        {
            _driver = driver;
            _logger = logger;
        }

        private string GetTextSafe(By locator)
        {
            string safeText = string.Empty;
            string fullText = _outputDiv.FindElement(locator).Text;
            safeText = fullText.Contains(":") ? fullText.Split(":")[1].Trim() : fullText;

            Log.Debug("Collected data: {text}\r", safeText);
            return safeText;
        }

        public string Name
        {
            get
            {
                Log.Information("Collecting name from results");
                return GetTextSafe(By.Id("name"));
            }
        }

        public string Email
        {
            get
            {
                Log.Information("Collecting e-mail from results");
                return GetTextSafe(By.Id("email"));
            }
        }

        public string CurrentAddress
        {
            get
            {
                Log.Information("Collecting current address from results");
                return GetTextSafe(By.Id("currentAddress"));
            }
        }

        public string PermanentAddress
        {
            get
            {
                Log.Information("Collecting permanent address from results");
                return GetTextSafe(By.Id("permanentAddress"));
            }
        }
    }
}
