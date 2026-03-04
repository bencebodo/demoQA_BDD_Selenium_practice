using Demoqa_BDD.Context;
using Demoqa_PageObjects.PageObjects;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Reqnroll.Microsoft.Extensions.DependencyInjection;
using Reqnroll.Tracing;
using Serilog;

namespace Demoqa_BDD.Support.Dependencies
{
    public static class TestDependencies
    {
        [ScenarioDependencies]
        public static IServiceCollection CreateService()
        {
            var services = new ServiceCollection();
            services.AddScoped<IWebDriver>(provider =>
            {
                ChromeOptions options = new ChromeOptions();
                options.AddArgument("--headless=new");
                options.AddArgument("--window-size=1920,1080");
                options.AddArgument("--disable-gpu");
                options.AddArgument("--no-sandbox"); 
                options.AddArgument("--disable-dev-shm-usage"); 
                options.BinaryLocation = "/usr/bin/google-chrome";
                options.PageLoadStrategy = PageLoadStrategy.None;
                return new ChromeDriver(options);
            });
            services.AddScoped<WebDriverContext>(provider =>
            {
                var driver = provider.GetRequiredService<IWebDriver>();
                return new WebDriverContext()
                {
                    Driver = driver,
                    BaseUrl = "https://demoqa.com/"
                };
            });
            services.AddScoped<HomePage>();
            services.AddScoped<AutoCompletePage>();
            services.AddScoped<BrowserWindowsPage>();
            services.AddScoped<ButtonsPage>();
            services.AddScoped<CheckBoxPage>();
            services.AddScoped<PracticeFormPage>();
            services.AddScoped<ProgressBarPage>();
            services.AddScoped<SelectablePage>();
            services.AddScoped<TextBoxPage>();
            services.AddScoped<WebTablesPage>();
            services.AddSingleton<ILogger>(provider => Log.Logger);

            return services;
        }
    }
}
