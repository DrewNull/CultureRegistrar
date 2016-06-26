namespace CultureRegistrar.Server.Application
{
    using System.Collections.Generic;

    public interface IAppPoolService
    {
        void RecycleCurrentAppPool();

        void RecycleAppPools(IEnumerable<string> appPoolNames);
    }
}