namespace CultureRegistrar.Server.Api.Infrastructure.IO
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web;
    using Application;

    public class LogEntryService : ILogEntryService
    {
        public IEnumerable<LogEntryBatch> Get()
        {
            var directoryPath = HttpContext.Current.Server.MapPath(Constants.LogDirectoryPath);

            var directory = new DirectoryInfo(directoryPath);

            var files = directory.GetFiles("*.txt").Reverse();

            foreach (var file in files)
            {
                var batch = new LogEntryBatch();
                batch.Name = file.Name;
                batch.LogEntries = File.ReadLines(file.FullName).Reverse();

                yield return batch;
            }
        }
    }
}