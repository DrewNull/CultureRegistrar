namespace CultureRegistrar.Server.Api.Infrastructure.Web
{
    using System;
    using System.Collections.Generic;
    using System.Web;
    using Application;
    using Microsoft.Web.Administration;

    public class AppPoolService : IAppPoolService
    {
        public void RecycleAppPools(IEnumerable<string> appPoolNames)
        {
            var serverManager = new ServerManager();

            foreach (string appPoolName in appPoolNames)
            {
                RecycleAppPool(appPoolName, serverManager);
            }
        }

        public void RecycleCurrentAppPool()
        {
            var serverManager = new ServerManager();

            string appPoolName = GetCurrentAppPoolName(serverManager);

            RecycleAppPool(appPoolName, serverManager);
        }

        private static void RecycleAppPool(string appPoolName, ServerManager serverManager)
        {
            if (string.IsNullOrWhiteSpace(appPoolName))
                return;

            var appPool = serverManager.ApplicationPools[appPoolName];

            if (appPool == null)
                return;

            if (appPool.State == ObjectState.Stopped)
            {
                appPool.Start();
            }
            else
            {
                appPool.Recycle();
            }
        }

        private static string GetCurrentAppPoolName(ServerManager serverManager)
        {
            var site = serverManager.Sites[System.Web.Hosting.HostingEnvironment.ApplicationHost.GetSiteName()];

            string appVirtaulPath = HttpRuntime.AppDomainAppVirtualPath;

            foreach (var application in site.Applications)
            {
                if (string.Equals(application.Path, appVirtaulPath, StringComparison.InvariantCultureIgnoreCase))
                {
                    return application.ApplicationPoolName;
                }
            }

            return null;
        }
    }
}