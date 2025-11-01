using System;
using Microsoft.Extensions.Configuration;
using Reqnroll;
using Reqnroll.BoDi;

namespace UiTests.Support
{
    [Binding]
    public class ConfigBootstrap
    {
        private readonly IObjectContainer _container;

        public ConfigBootstrap(IObjectContainer container)
        {
            _container = container;
        }

        // Register IConfiguration so Reqnroll/BoDi can resolve it in Hooks and other bindings
        [BeforeScenario(Order = 0)]
        public void RegisterConfiguration()
        {
            var basePath = AppContext.BaseDirectory; // points to bin/{Config}/net8.0
            var config = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: false)
                .Build();

            // Register or replace existing registration
            try
            {
                _container.RegisterInstanceAs<IConfiguration>(config);
            }
            catch
            {
                // If already registered by a prior hook, replace it
                _container.Resolve<IConfiguration>();
                _container.RegisterInstanceAs<IConfiguration>(config);
            }
        }
    }
}
