using DemoQa.Core.Drivers;
using OpenQA.Selenium;
using Microsoft.Extensions.Configuration;

namespace DemoQa.Tests.Context
{
    public class WebDriverContext : IDisposable
    {
        public IWebDriver Driver { get; }
        public string BaseUrl { get; }
        public Dictionary<string, string> FormData { get; set; } = new Dictionary<string, string>();
        public HashSet<string> SelectedItems { get; set; } = new HashSet<string>();

        public WebDriverContext(IDriverFactory factory, IConfiguration config)
        {
            var browser = config["Browser"] ?? "chrome";
            Driver = factory.CreateDriver(browser);
            BaseUrl = config["BaseUrl"];
        }

        public void Dispose()
        {
            Driver?.Quit();
            Driver?.Dispose();
        }
    }
}
