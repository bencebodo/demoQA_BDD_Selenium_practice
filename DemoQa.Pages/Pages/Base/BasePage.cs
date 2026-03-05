using DemoQa.Core.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using Serilog;

namespace DemoQa.Pages.Pages.Base
{
    public abstract class BasePage
    {
        protected IWebDriver _driver;
        protected IJavaScriptExecutor jsExecutor;
        protected WebDriverWait wait;
        protected readonly ILogger _logger;

        public BasePage(IWebDriver driver, ILogger logger)
        {
            _logger = logger;
            _driver = driver;
            jsExecutor = (IJavaScriptExecutor)_driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            _driver.HideElementPermanently(".amp-animate");
        }

        protected IWebElement GetElement(By locator)
        {
            var element = wait.Until(ExpectedConditions.ElementExists(locator));
            element.ScrollToElement(_driver);
            return element;
        }

        protected IList<IWebElement> GetElementsOrEmpty(By locator)
        {
            TimeSpan defaultTimeout = _driver.Manage().Timeouts().ImplicitWait;
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.Zero;
            wait.Until(ExpectedConditions.ElementExists(locator));
            try
            {
                return _driver.FindElements(locator);
            }
            finally
            {
                _driver.Manage().Timeouts().ImplicitWait = defaultTimeout;
            }
        }

        protected IWebElement GetElement(string text, string cssSelector)
        {
            IList<IWebElement> allElementsByClass = GetElementsOrEmpty(By.CssSelector(cssSelector));
            IWebElement targetCard = null;

            foreach (var element in allElementsByClass)
            {
                if (element.Text == text)
                {
                    targetCard = element;
                    break;
                }
            }

            return targetCard;
        }

        protected void WaitForPageToLoad()
        {
            wait.Until(driver =>
            {
                try
                {
                    string state = jsExecutor.ExecuteScript("return document.readyState").ToString();
                    return state == "complete";
                }
                catch (Exception)
                {
                    return false;
                }
            });
        }

        public void RemoveFooterAndAds()
        {
            Log.Information("Removing annoying footer and ads from the DOM via JavaScript.");
            IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;

            try
            {
                js.ExecuteScript("var footer = document.querySelector('footer'); if(footer) footer.remove();");

                js.ExecuteScript("var fixedban = document.querySelector('#fixedban'); if(fixedban) fixedban.remove();");

                js.ExecuteScript("document.querySelectorAll('iframe').forEach(function(el) { el.remove(); });");

                js.ExecuteScript("document.querySelectorAll('[id^=google_ads]').forEach(function(el) { el.remove(); });");

                js.ExecuteScript("document.querySelectorAll('[id^=RightSide_Advertisement]').forEach(function(el) { el.remove(); });");
            }
            catch (Exception ex)
            {
                Log.Debug("No footer or ads found to remove, or already removed. {Message}", ex.Message);
            }
        }
    }
}
