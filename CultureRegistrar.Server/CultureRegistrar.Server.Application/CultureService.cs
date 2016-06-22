namespace CultureRegistrar.Server.Application
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    public class CultureService : ICultureService
    {
        public IEnumerable<Culture> Get()
        {
            var cultureInfos = CultureInfo.GetCultures(CultureTypes.UserCustomCulture);

            foreach (var cultureInfo in cultureInfos)
            {
                var culture = CultureFactory.Create(cultureInfo);

                yield return culture;
            }
        }

        public CultureRegisterResult Register(string cultureName)
        {
            var resultFactory = new CultureRegisterResultFactory(cultureName);

            try
            {
                var codes = cultureName.Split('-');

                if (codes.Length != 2)
                {
                    return resultFactory.Create(CultureRegisterStatus.ErrorInvalidFormat);
                }

                string languageCode = codes[0];

                if (string.IsNullOrWhiteSpace(languageCode))
                {
                    return resultFactory.Create(CultureRegisterStatus.ErrorNoLanguage);
                }

                var language = new CultureInfo(languageCode);

                string countryCode = codes[1];

                if (string.IsNullOrWhiteSpace(countryCode))
                {
                    return resultFactory.Create(CultureRegisterStatus.ErrorNoCountry);
                }

                var country = new RegionInfo(countryCode);

                var existingCultures = CultureInfo.GetCultures(CultureTypes.AllCultures);

                bool cultureAlreadyExists = existingCultures
                    .Any(x => string.Equals(x.Name, cultureName, StringComparison.InvariantCultureIgnoreCase));

                if (cultureAlreadyExists)
                {
                    return resultFactory.Create(CultureRegisterStatus.ErrorAlreadyExists);
                }

                var builder = new CultureAndRegionInfoBuilder(cultureName, CultureAndRegionModifiers.None);

                // try to load existing info for the language
                var languageInfo = new CultureInfo(languageCode);
                builder.LoadDataFromCultureInfo(languageInfo);

                // try to load existing info for the country
                var regionInfo = new RegionInfo(countryCode);
                builder.LoadDataFromRegionInfo(regionInfo);

                // provide friendly names for Sitecore
                builder.CultureEnglishName = $"{language.EnglishName} ({country.EnglishName})";
                builder.CultureNativeName = $"{language.NativeName} ({country.NativeName})";

                builder.Register();

                return resultFactory.Create(CultureRegisterStatus.Success);
            }
            catch (Exception exception)
            {
                return resultFactory.Create(CultureRegisterStatus.ErrorOther, exception);
            }
        }

        public IEnumerable<CultureRegisterResult> Register(IEnumerable<string> cultureNames)
        {
            return cultureNames.Select(this.Register);
        }

        public CultureUnregisterResult Unregister(string cultureName)
        {
            var resultFactory = new CultureUnregisterResultFactory(cultureName);

            try
            {
                var existingCultures = CultureInfo.GetCultures(CultureTypes.AllCultures);

                bool cultureDoesNotExist = !existingCultures
                    .Any(x => string.Equals(x.Name, cultureName, StringComparison.InvariantCultureIgnoreCase));

                if (cultureDoesNotExist)
                {
                    return resultFactory.Create(CultureUnregisterStatus.ErrorDoesNotExist);
                }

                CultureAndRegionInfoBuilder.Unregister(cultureName);

                return resultFactory.Create(CultureUnregisterStatus.Success);
            }
            catch (Exception exception)
            {
                return resultFactory.Create(CultureUnregisterStatus.ErrorOther, exception);
            }
        }

        public IEnumerable<CultureUnregisterResult> Unregister(IEnumerable<string> cultureNames)
        {
            return cultureNames.Select(this.Unregister);
        }
    }
}