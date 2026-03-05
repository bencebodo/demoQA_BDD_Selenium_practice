using DemoQa.Core.Extensions;
using DemoQa.Pages.Pages.Base;
using OpenQA.Selenium;
using Serilog;

namespace DemoQa.Pages.Pages
{
    public class ButtonsPage : BasePage
    {

        public ButtonsPage(IWebDriver driver, ILogger logger) : base(driver, logger) { }

        private void PerformDoubleClick(IWebElement clickable_Btn)
        {
            string script = @"
                var evt = new MouseEvent('dblclick', {
                    bubbles: true,
                    cancelable: true,
                    view: window
                });
                arguments[0].dispatchEvent(evt);";

            jsExecutor.ExecuteScript(script, clickable_Btn);
        }

        private void PerformRightClick(IWebElement clickable_Btn)
        {
            string script = @"
                var evt = new MouseEvent('contextmenu', {
                    bubbles: true,
                    cancelable: true,
                    view: window
                });
                arguments[0].dispatchEvent(evt);";

            jsExecutor.ExecuteScript(script, clickable_Btn);
        }

        private void PerformClick(IWebElement clickable_Btn)
        {
            clickable_Btn.Click();
        }

        public bool VerifyResult(string text)
        {
            Log.Information("Verifying that the following message is displayed:\r{text}", text);
            By dynamicPath = By.XPath($"//*[text()='{text}']");
            IWebElement textElement = GetElement(dynamicPath);
            Log.Debug("\"{text}\" is displayed? [{bool}]", text, textElement.Displayed);
            return textElement.Displayed;
        }

        public void PerformAction(string name)
        {
            IWebElement clickable_Btn = GetElement(name, ".btn-primary");
            clickable_Btn.ScrollToElement(_driver);
            Log.Information("Clicking on {button} button", clickable_Btn.Text);
            switch (clickable_Btn.Text)
            {
                case "Double Click Me":
                    PerformDoubleClick(clickable_Btn);
                    break;
                case "Right Click Me":
                    PerformRightClick(clickable_Btn);
                    break;
                case "Click Me":
                    PerformClick(clickable_Btn);
                    break;
                default:
                    break;
            }
        }
    }
}
