// tests/UiTests/Properties/AssemblyInfo.cs
using System;
using Allure.Commons;
using Allure.NUnit;
using Allure.NUnit.Attributes;
using NUnit.Framework;

[assembly: Parallelizable(ParallelScope.All)]
[assembly: LevelOfParallelism(4)]

// // Optional labeling in Allure UI
// [assembly: AllureSuite("UI Automation")]
// [assembly: AllureSubSuite("Regression")]

// Allure adapter must be applied on a type (not assembly)
[SetUpFixture]
[AllureNUnit]
public class AllureSetup
{
    [OneTimeSetUp]
    public void ConfigureAllure()
    {
        // Point Allure to config next to test binaries
        var work = TestContext.CurrentContext.WorkDirectory;
        var cfg = System.IO.Path.Combine(work, "allureConfig.json");
        if (System.IO.File.Exists(cfg))
            Environment.SetEnvironmentVariable("ALLURE_CONFIG", cfg);

        // Optional: start with a clean result directory
        AllureLifecycle.Instance.CleanupResultDirectory();
    }
}