namespace CultureRegistrar.Server.Application
{
    using System.Collections.Generic;

    public interface ILogEntryService
    {
        IEnumerable<LogEntryBatch> Get();
    }
}