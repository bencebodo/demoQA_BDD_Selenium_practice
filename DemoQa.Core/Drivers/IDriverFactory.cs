using OpenQA.Selenium;

namespace DemoQa.Core.Drivers
{
    public interface IDriverFactory
    {
        IWebDriver CreateDriver(string browserType);
    }
}
