namespace CultureRegistrar.Server.Application
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    public class CultureService : ICultureService
    {
        private readonly IAppPoolService _appPoolService;

        public CultureService(IAppPoolService appPoolService)
        {
            if (appPoolService == null) throw new ArgumentNullException(nameof(appPoolService));

            this._appPoolService = appPoolService;
        }

        public IEnumerable<Culture> Get()
        {
            var cultures = CultureInfo
                .GetCultures(CultureTypes.UserCustomCulture)
                .Select(CultureFactory.Create)
                .Where(x => x != null)
                .OrderBy(x => x.Code);

            return cultures;
        }

        public IEnumerable<CultureRegisterResult> Register(IEnumerable<string> cultureNames, bool recycleAppPool = true)
        {
            var results = cultureNames.Select(this.Register);

            if (recycleAppPool)
            {
                this._appPoolService.RecycleCurrentAppPool();
            }

            return results;
        }

        public IEnumerable<CultureUnregisterResult> Unregister(IEnumerable<string> cultureNames)
        {
            return cultureNames.Select(this.Unregister);
        }

        private CultureRegisterResult Register(string cultureName)
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
                
                var languageInfo = new CultureInfo(languageCode);
                builder.LoadDataFromCultureInfo(languageInfo);

                var regionInfo = new RegionInfo(countryCode);
                builder.LoadDataFromRegionInfo(regionInfo);

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

        private CultureUnregisterResult Unregister(string cultureName)
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
    }
}