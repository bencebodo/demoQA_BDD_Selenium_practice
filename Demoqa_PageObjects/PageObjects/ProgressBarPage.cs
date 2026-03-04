using Demoqa_PageObjects.Extensions;
using Demoqa_PageObjects.PageObjects.Base;
using OpenQA.Selenium;
using Serilog;

namespace Demoqa_PageObjects.PageObjects
{
    public class ProgressBarPage : BasePage
    {
        private IWebElement startStopBtn => GetElement(By.Id("startStopButton"));
        private IWebElement resetButton => GetElement(By.Id("resetButton"));
        private IWebElement progressBar => GetElement(By.XPath("//div[@id = 'progressBar']/div"));

        public ProgressBarPage(IWebDriver driver, ILogger logger) : base(driver, logger) { }

        public void CLickOnStartButton()
        {
            progressBar.ScrollToElement(_driver);
            if (progressBar.GetAttribute("aria-valuenow") == "100")
            {
                Log.Information("Clicking on {Reset} button", resetButton.Text);
                resetButton.Click();
            }
            else
            {
                Log.Information("Clicking on {start} button", startStopBtn.Text);
                startStopBtn.Click();
            }
        }

        public void WaitForProgressBarToComplete()
        {
            Log.Information("Waiting until the progress bar is completed");
            wait.Until(d => progressBar.GetAttribute("aria-valuenow") == "100");
        }

        public string GetStartStopBtnText()
        {
            string buttonText = string.Empty;

            if (progressBar.GetAttribute("aria-valuenow") == "100")
            {
                buttonText = resetButton.Text;    
            }
            else
            {
                buttonText = startStopBtn.Text; 
            }

            Log.Information("Action button is set to {button}", buttonText);
            return buttonText;
        }

        public string GetProgressBarValueText()
        {
            string progressBarValue = $"{progressBar.GetAttribute("aria-valuenow")}%";
            Log.Information("Progress bar is on {value}", progressBarValue);
            return progressBarValue;
        }
    }
}
