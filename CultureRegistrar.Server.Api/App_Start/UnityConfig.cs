namespace CultureRegistrar.Server.Api
{
    using System.Web.Http;
    using Application;
    using Controllers;
    using Infrastructure.IO;
    using Microsoft.Practices.Unity;
    using Unity.WebApi;

    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();
            
            // Application
            container.RegisterType<ICultureService, CultureService>();

            // Infrastructure
            container.RegisterType<ICultureService, LoggingCultureService>("Logging");
            container.RegisterType<ILogEntryService, LogEntryService>();
            container.RegisterType<ILogger, Logger>();

            // API
            container.RegisterType<RegistrationController>(
                new InjectionConstructor(
                    new ResolvedParameter<ICultureService>("Logging")));

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}