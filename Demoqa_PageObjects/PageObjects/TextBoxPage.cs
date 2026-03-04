using Demoqa_PageObjects.Components;
using Demoqa_PageObjects.Extensions;
using Demoqa_PageObjects.PageObjects.Base;
using OpenQA.Selenium;
using Serilog;

namespace Demoqa_PageObjects.PageObjects
{
    public class TextBoxPage : BasePage
    {
        private IWebElement fullName_Field => GetElement(By.Id("userName"));
        private IWebElement email_Field => GetElement(By.Id("userEmail"));
        private IWebElement currentAddress_Field => GetElement(By.XPath("//textarea[@id = \"currentAddress\"]"));
        private IWebElement permanentAddres_Field => GetElement(By.XPath("//textarea[@id = \"permanentAddress\"]"));
        private IWebElement submit_Btn => GetElement(By.Id("submit"));

        public TextBoxPage(IWebDriver driver, ILogger logger) : base(driver, logger) { }

        public TextBoxResultComponent Results => new TextBoxResultComponent(_driver, _logger);

        public void FillForm(Dictionary<string, string> inputData)
        {
            Log.Information("Filling form based on input data");

            fullName_Field.SendKeys(inputData["Full Name"]);
            email_Field.SendKeys(inputData["Email"]);
            currentAddress_Field.SendKeys(inputData["Current Address"]);
            permanentAddres_Field.SendKeys(inputData["Permanent Address"]);
        }

        public void SubmitForm()
        {
            Log.Information("Pressing {submit} button.", submit_Btn.Text);
            submit_Btn.ClickWithScroll(_driver);
        }
    }
}
