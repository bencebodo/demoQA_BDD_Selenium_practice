using DemoQa.Core.Extensions;
using DemoQa.Pages.Pages.Base;
using OpenQA.Selenium;
using Serilog;

namespace DemoQa.Pages.Pages
{
    public class HomePage : BasePage
    {
        public HomePage(IWebDriver driver, ILogger logger) : base(driver, logger) { }

        public void NavigateToSectionByName(string sectionName)
        {
            WaitForPageToLoad();
            RemoveFooterAndAds();
            IWebElement targetCard = GetElement(By.XPath($"//*[normalize-space(text()) = '{sectionName}']/ancestor::div[contains(@class, 'top-card')]"));
            targetCard.ClickWithScroll(_driver);
        }

        public void NavigateToSubsectionByName(string subsectionName)
        {
            WaitForPageToLoad();
            RemoveFooterAndAds();
            IWebElement subsection_Btn = GetElement(By.XPath($"//li[contains(@id, 'item')]//span[normalize-space(text()) = '{subsectionName}']"));
            subsection_Btn.ClickWithJS(_driver);
        }

    }
}
