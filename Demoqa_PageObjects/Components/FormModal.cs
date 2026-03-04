using Demoqa_PageObjects.Extensions;

using OpenQA.Selenium;
using Serilog;

namespace Demoqa_PageObjects.Components
{
    public class FormModal
    {
        private IWebDriver _driver;
        private ILogger _logger;
        private IWebElement _modalContent => _driver.FindElement(By.Id("userForm"));

        public FormModal(IWebDriver driver, ILogger logger)
        {
            _driver = driver;
            _logger = logger;
        }

        public IWebElement FirstName => _modalContent.FindElement(By.Id("firstName"));
        public IWebElement LastName => _modalContent.FindElement(By.Id("lastName"));
        public IWebElement Email => _modalContent.FindElement(By.Id("userEmail"));
        public IWebElement Mobile => _modalContent.FindElement(By.Id("userNumber"));
        public IWebElement DateOfBirth => _modalContent.FindElement(By.Id("dateOfBirthInput"));
        public IWebElement Subject => _modalContent.FindElement(By.XPath("//div[@id = \"subjectsContainer\"]/descendant::input"));
        public IWebElement State => _modalContent.FindElement(By.Id("react-select-3-input"));
        public IWebElement City => _modalContent.FindElement(By.Id("react-select-4-input"));
        public IWebElement Submit => _modalContent.FindElement(By.Id("submit"));


        public void SelectGender(string gender)
        {
            var genderLabel = _modalContent.FindElement(By.XPath($"//label[text()='{gender}']"));
            genderLabel.ClickWithScroll(_driver);
        }

        public void SelectState(string state)
        {
            State.WriteWithScroll(_driver, state);
            State.SendKeys(Keys.Enter);
        }

        public void SelectCity(string city)
        {
            City.SendKeys(city);
            City.SendKeys(Keys.Enter);
        }

        public void SelectHobbies(string hobbiesString)
        {
            var hobbies = StringSplitter(hobbiesString);

            foreach (var hobby in hobbies)
            {
                string cleanHobby = hobby.Trim();

                IWebElement hobbyCheckbox = _modalContent.FindElement(By.XPath($"//label[text() = '{cleanHobby}']"));
                hobbyCheckbox.ClickWithScroll(_driver);
            }
        }

        public void EnterSubjects(string subjectString)
        {
            var subjects = StringSplitter(subjectString);

            foreach (var subject in subjects)
            {
                string cleanSubject = subject.Trim();

                Subject.WriteWithScroll(_driver, cleanSubject);
                Subject.SendKeys(Keys.Enter);
            }
        }

        public void FillDate(string date)
        {
            DateOfBirth.WriteWithScroll(_driver, Keys.Control + "a");
            DateOfBirth.SendKeys(date);
            DateOfBirth.SendKeys(Keys.Enter);
        }

        private string[] StringSplitter(string inputString)
        {
            return inputString.Split(',');
        }
    }
}
