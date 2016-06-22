namespace CultureRegistrar.Server.Api.Infrastructure.IO
{
    using System.Collections.Generic;
    using System.IO;
    using Application;

    public class LogEntryService : ILogEntryService
    {
        public IEnumerable<LogEntryBatch> Get()
        {
            var directory = new DirectoryInfo(Constants.LogDirectoryPath);

            var files = directory.GetFiles("*.txt");

            foreach (var file in files)
            {
                var batch = new LogEntryBatch();
                batch.Name = file.Name;
                batch.LogEntries = File.ReadLines(file.FullName);

                yield return batch;
            }
        }
    }
}