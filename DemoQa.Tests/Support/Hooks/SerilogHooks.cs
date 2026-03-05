using Serilog;
using System.Diagnostics;

namespace DemoQa.Tests.Support.Hooks
{
    [Binding]
    public class SerilogHooks
    {
        private readonly ScenarioContext _scenarioContext;

        public SerilogHooks(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeTestRun]
        public static void InitializeLogger()
        {
            Trace.Listeners.Clear();
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console(
            outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}"
            )
            .CreateLogger();
            Log.Information("Test run started");
        }

        [BeforeScenario(Order = 0)]
        public void LogScenarioStart()
        {
            Log.Information("Starting scenario");
        }

        [AfterStep]
        public void LogResult()
        {
            var stepText = _scenarioContext.StepContext.StepInfo.Text;

            if (_scenarioContext.TestError != null)
            {
                Log.Error(_scenarioContext.TestError, "Error in step: {StepText}", stepText);
            }
        }

        [AfterScenario(Order = 100)]
        public void LogScenarioFinish()
        {
            Log.Information("Finishing scenario");

        }

        [AfterTestRun]
        public static void TearDownLogger()
        {
            Log.Information("Test run finished");
            Log.CloseAndFlush();
        }
    }
}
