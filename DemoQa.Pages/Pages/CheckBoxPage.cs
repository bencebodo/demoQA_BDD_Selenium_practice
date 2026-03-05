using DemoQa.Core.Extensions;
using DemoQa.Pages.Pages.Base;
using OpenQA.Selenium;
using Serilog;

namespace DemoQa.Pages.Pages
{
    public class CheckBoxPage : BasePage
    {
        public CheckBoxPage(IWebDriver driver, ILogger logger) : base(driver, logger) { }

        private IWebElement Expand_Btn(string name) => GetElement(By.XPath($"//span[@class='rc-tree-title' and text()='{name}']/parent::span/preceding-sibling::span[contains(@class, 'rc-tree-switcher')]"));
        private IWebElement Checkbox_BtnById(string name) => GetElement(By.XPath($"//span[@class='rc-tree-title' and text()='{name}']/parent::span/preceding-sibling::span[contains(@class, 'rc-tree-checkbox')]"));
        private IWebElement FolderName_SpanById(string name) => GetElement(By.XPath($"//span[@class='rc-tree-title' and text()='{name}']"));

        public void Expand_Folder(string folderName)
        {
            Log.Information("Expanding {folderName} folder", folderName);
            Expand_Btn(folderName).ClickWithScroll(_driver);
        }

        public void Check_Folder(string folderName)
        {
            Log.Information("Checking in {folderName} folder", folderName);
            Checkbox_BtnById(folderName).ClickWithScroll(_driver);
        }

        public void ClickOnFolderName(string folderName)
        {
            Log.Information("Clicking on {folderName} folder", folderName);
            FolderName_SpanById(folderName).ClickWithScroll(_driver);
        }

        public string GetResultText()
        {
            Log.Information("Collecting result data");
            IList<IWebElement> resultElements = GetElementsOrEmpty(By.Id("result"));
            HashSet<string> result = new HashSet<string>();

            foreach (IWebElement element in resultElements)
            {
                result.Add(element.Text);
                Log.Debug("Collected data:\r {data}", element.Text);
            }

            string rawText = string.Join(" ", result);
            string actualResult = rawText.Replace("\r\n", " ").Replace("\n", " ");

            return actualResult;
        }

        public void ExpandPathAndCheck(string[] path)
        {
            Log.Information("Navigating and checking path: {Path}", string.Join(" > ", path));

            for (int i = 0; i < path.Length - 1; i++)
            {
                string nodeName = path[i];

                var expandBtn = Expand_Btn(nodeName);

                if (expandBtn.GetAttribute("class").Contains("close"))
                {
                    Log.Debug("Expanding parent node: {NodeName}", nodeName);
                    expandBtn.ClickWithScroll(_driver);
                }
            }

            string finalNode = path.Last();

            var checkboxBtn = Checkbox_BtnById(finalNode);

            if (!checkboxBtn.GetAttribute("class").Contains("checked"))
            {
                Log.Debug("Checking target node: {FinalNode}", finalNode);
                checkboxBtn.ClickWithScroll(_driver); // Konzisztens kattintás
            }
        }
    }
}