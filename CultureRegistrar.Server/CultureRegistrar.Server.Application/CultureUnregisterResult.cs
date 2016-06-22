namespace CultureRegistrar.Server.Application
{
    using System;

    public class CultureUnregisterResult : CultureRegistrationResultBase
    {
        public CultureUnregisterResult(string cultureCode, CultureUnregisterStatus status, Exception exception = null)
            : base(cultureCode, exception)
        {
            this.Status = status;
        }

        public CultureUnregisterStatus Status { get; }
    }
}