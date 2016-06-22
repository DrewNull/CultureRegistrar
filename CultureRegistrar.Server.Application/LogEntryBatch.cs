namespace CultureRegistrar.Server.Application
{
    using System.Collections.Generic;

    public class LogEntryBatch
    {
        public string Name { get; set; }

        public IEnumerable<string> LogEntries { get; set; }
    }
}