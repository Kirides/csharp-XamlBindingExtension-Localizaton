using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SimpleLocalization.Localization.Abstractions;

namespace SimpleLocalization.Localization
{
    public class JsonLocalizer : ILocalizer
    {
        public string LanguageTag { get; set; }
        private JObject LanguageStore { get; set; }

        public string this[string key] => GetText(key);
        public string GetText(string key) => key == null ? null : LanguageStore.SelectToken(key)?.Value<string>();

        public JsonLocalizer(string json, string languageTag)
        {
            LanguageStore = JsonConvert.DeserializeObject<JObject>(json);
            LanguageTag = languageTag;
        }
    }
}
