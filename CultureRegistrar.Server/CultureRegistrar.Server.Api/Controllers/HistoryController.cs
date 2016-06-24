namespace CultureRegistrar.Server.Api.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Web.Http;
    using System.Web.Http.Cors;
    using Application;

    [EnableCors("*", "*", "*")]
    [RoutePrefix("history")]
    public class HistoryController : ApiController
    {
        private readonly ILogEntryService _logEntryService;

        public HistoryController(ILogEntryService logEntryService)
        {
            if (logEntryService == null) throw new ArgumentNullException(nameof(logEntryService));

            this._logEntryService = logEntryService;
        }

        [HttpGet]
        [Route("")]
        public IEnumerable<LogEntryBatch> Get()
        {
            var logEntryBatches = this._logEntryService.Get();

            return logEntryBatches;
        }
    }
}