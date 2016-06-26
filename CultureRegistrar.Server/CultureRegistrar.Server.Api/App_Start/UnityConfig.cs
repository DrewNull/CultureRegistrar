namespace CultureRegistrar.Server.Api
{
    using System.Web.Http;
    using Application;
    using Controllers;
    using Infrastructure.IO;
    using Infrastructure.Web;
    using Microsoft.Practices.Unity;
    using Unity.WebApi;

    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();
            
            // Application
            container.RegisterType<ICultureService, CultureService>();

            // Infrastructure - IO
            container.RegisterType<ICultureService, LoggingCultureService>("Logging");
            container.RegisterType<ILogEntryService, LogEntryService>();
            container.RegisterType<ILogger, Logger>();

            // Infrastructure - Web
            container.RegisterType<IAppPoolService, AppPoolService>();

            // API
            container.RegisterType<RegistrationController>(
                new InjectionConstructor(
                    new ResolvedParameter<ICultureService>("Logging")));

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}