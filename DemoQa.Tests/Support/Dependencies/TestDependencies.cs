using DemoQa.Core.Drivers;
using DemoQa.Pages.Pages;
using DemoQa.Tests.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenQA.Selenium;
using Reqnroll.Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace DemoQa.Tests.Support.Dependencies
{
    public static class TestDependencies
    {
        [ScenarioDependencies]
        public static IServiceCollection CreateService()
        {
            var services = new ServiceCollection();
            IConfiguration config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .Build();
            services.AddSingleton<IConfiguration>(config);
            services.AddSingleton<IDriverFactory, DriverFactory>();
            services.AddScoped<WebDriverContext>();
            services.AddScoped<IWebDriver>(provider =>
            provider.GetRequiredService<WebDriverContext>().Driver);
            var pagesAssembly = typeof(HomePage).Assembly;
            var pageTypes = pagesAssembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.Name.EndsWith("Page"));

            foreach (var type in pageTypes)
            {
                services.AddScoped(type);
            }
            services.AddSingleton<ILogger>(provider => Log.Logger);

            return services;
        }
    }
}
