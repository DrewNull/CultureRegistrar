namespace CultureRegistrar.Server.Api.Infrastructure.Web
{
    using System;
    using System.Web;
    using Application;
    using Microsoft.Web.Administration;

    public class CultureCacheBuster : ICultureCacheBuster
    {
        public void BustCache()
        {
            var serverManager = new ServerManager();

            string appPoolName = GetCurrentAppPoolName(serverManager);

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