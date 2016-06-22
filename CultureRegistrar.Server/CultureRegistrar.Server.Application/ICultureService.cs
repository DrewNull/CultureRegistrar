namespace CultureRegistrar.Server.Application
{
    using System.Collections.Generic;

    public interface ICultureService
    {
        IEnumerable<Culture> Get();

        CultureRegisterResult Register(string cultureName);

        IEnumerable<CultureRegisterResult> Register(IEnumerable<string> cultureNames);

        CultureUnregisterResult Unregister(string cultureName);

        IEnumerable<CultureUnregisterResult> Unregister(IEnumerable<string> cultureNames);
    }
}