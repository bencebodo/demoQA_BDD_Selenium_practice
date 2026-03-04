using Demoqa_BDD.Context;
using OpenQA.Selenium;
using Serilog;

namespace Demoqa_BDD.Support.Hooks
{
    [Binding]
    public class WebDriverHooks
    {
        private readonly WebDriverContext _context;
        public WebDriverHooks(WebDriverContext context)
        {
            _context = context;
        }

        [BeforeScenario]
        public void SetupBrowser()
        {
            //_context.Driver.Manage().Window.Maximize();
        }

        [AfterScenario]
        public void TearDownDriver()
        {
            if (_context.Driver != null)
            {
                _context.Driver.Quit();
                _context.Driver.Dispose();
            }
        }
    }
}
