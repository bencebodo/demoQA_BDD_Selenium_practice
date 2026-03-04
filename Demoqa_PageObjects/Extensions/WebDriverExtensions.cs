using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace Demoqa_PageObjects.Extensions
{
    public static class WebDriverExtensions
    {
        public static void HideElementPermanently(this IWebDriver driver, string cssSelector)
        {
            string jsScript = $@"
            var style = document.createElement('style');
            style.innerHTML = '{cssSelector} {{ display: none !important; visibility: hidden !important; z-index: -1000 !important; }}';
            document.head.appendChild(style);";

            ((IJavaScriptExecutor)driver).ExecuteScript(jsScript);
        }
    }
}
