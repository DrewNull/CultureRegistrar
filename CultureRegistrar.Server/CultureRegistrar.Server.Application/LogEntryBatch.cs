namespace CultureRegistrar.Server.Application
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract(Name = "logEntryBatch")]
    public class LogEntryBatch
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "logEntries")]
        public IEnumerable<string> LogEntries { get; set; }
    }
}