using Demoqa_PageObjects.Components;
using Demoqa_PageObjects.Extensions;
using Demoqa_PageObjects.PageObjects.Base;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;
using Serilog;

namespace Demoqa_PageObjects.PageObjects
{
    public class PracticeFormPage : BasePage
    {
        public PracticeFormPage(IWebDriver driver, ILogger logger) : base(driver, logger) { }

        public void FillForm(Dictionary<string, string> inputData)
        {
            Log.Information("Filling form based on input data");
            Form.FirstName.WriteWithScroll(_driver, inputData["First Name"]);
            Form.LastName.WriteWithScroll(_driver, inputData["Last Name"]);
            Form.Email.WriteWithScroll(_driver, inputData["Email"]);
            Form.SelectGender(inputData["Gender"]);
            Form.Mobile.WriteWithScroll(_driver, inputData["Mobile"]);
            Form.FillDate(inputData["Date of Birth"]);
            Form.EnterSubjects(inputData["Subjects"]);
            Form.SelectHobbies(inputData["Hobbies"]);
            Form.SelectState(inputData["State"]);
            wait.Until(ExpectedConditions.ElementToBeClickable(Form.City));
            Form.SelectCity(inputData["City"]);
        }

        public void SubmitForm()
        {
            Log.Information("Pressing {submit} button.", Form.Submit.Text);
            Form.Submit.ClickWithScroll(_driver);
        }

        public FormResultModal Results => new FormResultModal(_driver, _logger);

        public FormModal Form => new FormModal(_driver, _logger);
    }
}
