namespace CultureRegistrar.Server.Application
{
    using System;

    public abstract class CultureRegistrationResultBase
    {
        protected CultureRegistrationResultBase(string cultureCode, Exception exception = null)
        {
            this.CultureCode = cultureCode;
            this.Exception = exception;
            this.Timestamp = DateTime.Now;
        }

        public string CultureCode { get; }

        public Exception Exception { get; }
        public DateTime Timestamp { get; }
    }
}