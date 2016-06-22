namespace CultureRegistrar.Server.Application
{
    using System;

    public class CultureUnregisterResultFactory
    {
        private readonly string _cultureCode;

        public CultureUnregisterResultFactory(string cultureCode)
        {
            this._cultureCode = cultureCode;
        }

        public CultureUnregisterResult Create(CultureUnregisterStatus status, Exception exception = null)
        {
            return new CultureUnregisterResult(this._cultureCode, status, exception);
        }
    }
}