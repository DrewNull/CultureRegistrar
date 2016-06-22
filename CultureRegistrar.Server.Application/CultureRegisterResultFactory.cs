namespace CultureRegistrar.Server.Application
{
    using System;

    public class CultureRegisterResultFactory
    {
        private readonly string _cultureCode;

        public CultureRegisterResultFactory(string cultureCode)
        {
            this._cultureCode = cultureCode;
        }

        public CultureRegisterResult Create(CultureRegisterStatus status, Exception exception = null)
        {
            return new CultureRegisterResult(this._cultureCode, status, exception);
        }
    }
}