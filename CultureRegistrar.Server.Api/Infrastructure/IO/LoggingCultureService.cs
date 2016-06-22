﻿namespace CultureRegistrar.Server.Api.Infrastructure.IO
{
    using System;
    using System.Collections.Generic;
    using Application;

    public class LoggingCultureService : ICultureService
    {
        private readonly ICultureService _service;

        private readonly ILogger _logger;

        public LoggingCultureService(ICultureService service, ILogger logger)
        {
            if (service == null) throw new ArgumentNullException(nameof(service));
            if (logger == null) throw new ArgumentNullException(nameof(logger));

            this._service = service;
            this._logger = logger;
        }

        public IEnumerable<Culture> Get()
        {
            return this._service.Get();
        }

        public CultureRegisterResult Register(string cultureName)
        {
            var result = this._service.Register(cultureName);

            this._logger.Log(result);

            return result;
        }

        public IEnumerable<CultureRegisterResult> Register(IEnumerable<string> cultureNames)
        {
            var results = this._service.Register(cultureNames);

            this._logger.Log(results);

            return results;
        }

        public CultureUnregisterResult Unregister(string cultureName)
        {
            var result = this._service.Unregister(cultureName);

            this._logger.Log(result);

            return result;
        }

        public IEnumerable<CultureUnregisterResult> Unregister(IEnumerable<string> cultureNames)
        {
            var results = this._service.Unregister(cultureNames);

            this._logger.Log(results);

            return results;
        }
    }
}