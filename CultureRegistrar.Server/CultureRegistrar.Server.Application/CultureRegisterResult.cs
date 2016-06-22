namespace CultureRegistrar.Server.Application
{
    using System;

    public class CultureRegisterResult : CultureRegistrationResultBase
    {
        public CultureRegisterResult(string cultureCode, CultureRegisterStatus status, Exception exception = null)
            : base(cultureCode, exception)
        {
            this.Status = status;
        }

        public CultureRegisterStatus Status { get; }
    }
}