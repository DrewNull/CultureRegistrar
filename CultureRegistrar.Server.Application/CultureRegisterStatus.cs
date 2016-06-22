namespace CultureRegistrar.Server.Application
{
    public enum CultureRegisterStatus
    {
        Success,
        ErrorInvalidFormat,
        ErrorNoLanguage,
        ErrorNoCountry,
        ErrorAlreadyExists,
        ErrorOther
    }
}