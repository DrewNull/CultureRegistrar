namespace CultureRegistrar.Server.Application
{
    using System.Collections.Generic;

    public interface ICultureService
    {
        IEnumerable<Culture> Get();

        IEnumerable<CultureRegisterResult> Register(IEnumerable<string> cultureNames, bool recycleAppPool = true);

        IEnumerable<CultureUnregisterResult> Unregister(IEnumerable<string> cultureNames);
    }
}