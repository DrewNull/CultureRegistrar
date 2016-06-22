namespace CultureRegistrar.Server.Application
{
    using System.Runtime.Serialization;

    [DataContract(Name = "culture")]
    public class Culture
    {
        [DataMember(Name = "code")]
        public string Code { get; set; }

        [DataMember(Name = "countryEnglishName")]
        public string CountryEnglishName { get; set; }

        [DataMember(Name = "countryNativeName")]
        public string CountryNativeName { get; set; }

        [DataMember(Name = "languageEnglishName")]
        public string LanguageEnglishName { get; set; }

        [DataMember(Name = "languageNativeName")]
        public string LanguageNativeName { get; set; }
    }
}