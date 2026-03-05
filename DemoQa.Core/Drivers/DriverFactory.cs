using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace DemoQa.Core.Drivers
{
    public class DriverFactory : IDriverFactory
    {
        public IWebDriver CreateDriver(string browser)
        {
            return browser.ToLower() switch
            {
                "chrome" => new ChromeDriver(GetChromeOptions()),
                "firefox" => new FirefoxDriver(GetFirefoxOptions()),
                "edge" => new EdgeDriver(GetEdgeOptions()),
                _ => throw new ArgumentException($"Unsupported browser: {browser}")
            };
        }

        private ChromeOptions GetChromeOptions()
        {
            var options = new ChromeOptions();
            options.AddArgument("--headless=new");
            options.AddArgument("--window-size=1920,1080");
            options.AddArgument("--disable-gpu");
            options.AddArgument("--no-sandbox");
            options.AddArgument("--disable-dev-shm-usage");
            options.BinaryLocation = "/usr/bin/google-chrome";
            options.PageLoadStrategy = PageLoadStrategy.None;
            return options;
        }

        private FirefoxOptions GetFirefoxOptions()
        {
            var options = new FirefoxOptions();
            options.AddArgument("--start-maximized");
            return options;
        }

        private EdgeOptions GetEdgeOptions()
        {
            var options = new EdgeOptions();
            options.AddArgument("--start-maximized");
            return options;

        }
    }
}
