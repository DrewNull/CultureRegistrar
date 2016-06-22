namespace CultureRegistrar.Server.Application
{
    using System.Globalization;

    public static class CultureFactory
    {
        public static Culture Create(CultureInfo cultureInfo)
        {
            return Create(cultureInfo.Name);
        }

        public static Culture Create(string cultureName)
        {
            var culture = new Culture();
            culture.Code = cultureName;

            var codes = cultureName.Split('-');

            var languageCode = codes[0];
            var language = new CultureInfo(languageCode);
            culture.LanguageEnglishName = language.EnglishName;
            culture.LanguageNativeName = language.NativeName;

            var countryCode = codes[1];
            var country = new RegionInfo(countryCode);
            culture.CountryEnglishName = country.EnglishName;
            culture.CountryNativeName = country.NativeName;

            return culture;
        }
    }
}