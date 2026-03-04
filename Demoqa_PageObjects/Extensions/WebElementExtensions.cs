using OpenQA.Selenium;

namespace Demoqa_PageObjects.Extensions
{
    public static class WebElementExtensions
    {
        public static void ClickWithScroll(this IWebElement element, IWebDriver driver)
        {
            ScrollToElement(element, driver);

            element.Click();
        }

        public static void WriteWithScroll(this IWebElement element, IWebDriver driver, string text)
        {
            if (text == null) return;

            element.ScrollToElement(driver);

            element.Clear();
            element.SendKeys(text);
        }
        public static string GetIdOfElement(this IWebElement element) => element.GetAttribute("id");

        public static void ScrollToElement(this IWebElement element, IWebDriver driver)
        {
            var jsExecutor = (IJavaScriptExecutor)driver;
            jsExecutor.ExecuteScript("arguments[0].scrollIntoView({block: 'center'});", element);
        }

        public static void ClickWithJS(this IWebElement element, IWebDriver driver)
        {
            var jsExecutor = (IJavaScriptExecutor)driver;
            jsExecutor.ExecuteScript("arguments[0].click();", element);
        }
    }
}
