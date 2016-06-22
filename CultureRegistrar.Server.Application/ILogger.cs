namespace CultureRegistrar.Server.Application
{
    using System.Collections.Generic;

    public interface ILogger
    {
        void Log(CultureRegisterResult result);

        void Log(CultureUnregisterResult result);

        void Log(IEnumerable<CultureRegisterResult> results);

        void Log(IEnumerable<CultureUnregisterResult> results);
    }
}