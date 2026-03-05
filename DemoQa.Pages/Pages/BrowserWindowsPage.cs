using DemoQa.Core.Extensions;
using DemoQa.Pages.Pages.Base;
using OpenQA.Selenium;
using Serilog;

namespace DemoQa.Pages.Pages
{
    public class BrowserWindowsPage : BasePage
    {
        private IWebElement sampleHeading => GetElement(By.Id("sampleHeading"));
        private IWebElement bodyElement => GetElement(By.TagName("body"));

        public BrowserWindowsPage(IWebDriver driver, ILogger logger) : base(driver, logger) { }

        public void ClickOnElementByName(string name)
        {
            int oldwindowCount = _driver.WindowHandles.Count;
            Log.Debug("Window handle count: {count}", oldwindowCount);

            Log.Information("Interacting with \"{buttonName}\"", name);
            IWebElement element = GetElement(name, ".btn.btn-primary");
            element.ClickWithScroll(_driver);

            wait.Until(d => d.WindowHandles.Count > oldwindowCount);
            Log.Debug("Window handle count: {count}", _driver.WindowHandles.Count);
        }

        public string GetTextOnNewWindow(string windowType)
        {
            SwitchToNewWindow();

            string result = string.Empty;

            if (windowType == "New Tab" || windowType == "New Window")
            {
                Log.Debug("New window type: [{type}]", windowType);
                Log.Information("Reading text from \"{type}\"", windowType);
                result = sampleHeading.Text;
                Log.Information("Text from new window: \"{text}\"", result);
            }
            else
            {
                Log.Debug("New window type: [{type}]", windowType);
                try
                {
                    Log.Information("Reading text from \"{type}\"", windowType);
                    result = bodyElement.Text;
                    Log.Information("Text from new window: \"{text}\"", result);
                }
                catch (NoSuchElementException)
                {
                    Log.Error("Pop up window is not present.");
                }
            }
            _driver.Close();
            _driver.SwitchTo().Window(_driver.WindowHandles[0]);

            return result;
        }

        private void SwitchToNewWindow()
        {
            Log.Debug("Window handle ID: {id}", _driver.CurrentWindowHandle);
            Log.Information("Switching on new window");
            wait.Until(d => d.WindowHandles.Count > 1);

            string newWindowHandle = _driver.WindowHandles[_driver.WindowHandles.Count - 1];
            _driver.SwitchTo().Window(newWindowHandle);
            Log.Debug("Window handle ID: {id}", _driver.CurrentWindowHandle);
        }
    }
}
