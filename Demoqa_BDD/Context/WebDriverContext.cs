using OpenQA.Selenium;

namespace Demoqa_BDD.Context
{
    public class WebDriverContext
    {
        public IWebDriver Driver { get; set; }
        public string BaseUrl { get; set; }
        public Dictionary<string, string> FormData { get; set; } = new Dictionary<string, string>();
        public HashSet<string> SelectedItems { get; set; } = new HashSet<string>();
    }
}
