namespace CultureRegistrar.Server.Api.Infrastructure.Web
{
    using Application;
    using Microsoft.Web.Administration;

    public class CultureCacheBuster : ICultureCacheBuster
    {
        public void BustCache()
        {
            var serverManager = new ServerManager();
            var appPool = serverManager.ApplicationPools["culture-registrar"];
            if (appPool != null)
            {
                if (appPool.State == ObjectState.Stopped)
                {
                    appPool.Start();
                }
                else
                {
                    appPool.Recycle();
                }
            }
        }
    }
}