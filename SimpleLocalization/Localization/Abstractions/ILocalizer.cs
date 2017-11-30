namespace SimpleLocalization.Localization.Abstractions
{
    public interface ILocalizer
    {
        string this[string key] { get; }

        string LanguageTag { get; set; }

        string GetText(string key);
    }
}