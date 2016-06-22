namespace CultureRegistrar.Server.Api.Infrastructure.IO
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Web;
    using Application;

    public class Logger : ILogger
    {
        public void Log(CultureRegisterResult result)
        {
            this.Log(new[] { result });
        }

        public void Log(CultureUnregisterResult result)
        {
            this.Log(new[] { result });
        }

        public void Log(IEnumerable<CultureRegisterResult> results)
        {
            Log(results, GetRegisterResultMessage);
        }

        public void Log(IEnumerable<CultureUnregisterResult> results)
        {
            Log(results, GetUnregisterResultMessage);
        }

        public static string GetRegistrationResultMessage<T>(T result, string message)
            where T : CultureRegistrationResultBase
        {
            return $"{result.Timestamp.ToString("yyyy-MM-dd hh:mm:ss")} {message}";
        }

        private static string GetRegisterResultMessage(CultureRegisterResult result)
        {
            switch (result.Status)
            {
                case CultureRegisterStatus.Success:
                    return $"Culture \"{result.CultureCode}\" was successfully registered.";

                case CultureRegisterStatus.ErrorAlreadyExists:
                    return $"Culture \"{result.CultureCode}\" was already registered.";

                case CultureRegisterStatus.ErrorInvalidFormat:
                    return $"Invalid culture code: \"{result.CultureCode}\". Culture code must be in the following format: languageCode-regionCode (e.g., \"en-US\").";

                case CultureRegisterStatus.ErrorNoCountry:
                    return $"Invalid culture code: \"{result.CultureCode}\". No country was specified.";

                case CultureRegisterStatus.ErrorNoLanguage:
                    return $"Invalid culture code: \"{result.CultureCode}\". No language was specified.";

                default:
                case CultureRegisterStatus.ErrorOther:
                    return $"An error occurred and culture {result.CultureCode} was not registered: \"{result.Exception?.Message}\"";
            }
        }

        private static string GetUnregisterResultMessage(CultureUnregisterResult result)
        {
            switch (result.Status)
            {
                case CultureUnregisterStatus.Success:
                    return $"Culture \"{result.CultureCode}\" was successfully unregistered.";

                case CultureUnregisterStatus.ErrorDoesNotExist:
                    return $"Culture \"{result.CultureCode}\" was not found to unregister.";

                default:
                case CultureUnregisterStatus.ErrorOther:
                    return $"An error occurred and culture \"{result.CultureCode}\" was not unregistered: \"{result.Exception?.Message}\"";
            }
        }

        private static void Log<T>(IEnumerable<T> results, Func<T, string> getResultMessage) 
            where T : CultureRegistrationResultBase
        {
            var logEntries = results
                .Select(x => GetRegistrationResultMessage(x, getResultMessage(x)))
                .ToArray();

            WriteToFile(logEntries);
        }

        private static void WriteToFile(string[] logEntries)
        {
            string logDirectoryPath = HttpContext.Current.Server.MapPath(Constants.LogDirectoryPath);

            Directory.CreateDirectory(logDirectoryPath);

            File.WriteAllLines($"{logDirectoryPath}\\{DateTime.Now.ToString("yyyyMMddhhmmss")}.txt", logEntries);
        }
    }
}