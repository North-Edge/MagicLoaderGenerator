namespace MagicLoaderGenerator.Localization.Abstractions;

/// <summary>
/// Interface used for localization providers
/// <para>
///     Implementations:<br/>
///     - <see cref="Localization.Providers.BaseLocalizationProvider"/><br/>
///     - <see cref="Localization.Providers.JsonLocalizationProvider"/><br/>
/// </para>
/// </summary>
public interface ILocalizationProvider
{
    /// <summary>
    /// The languages supported by the localization provider
    /// </summary>
    public List<string> SupportedLanguages { get; }

    /// <summary>
    /// Retrieves a localized string given a key for the specified language
    /// </summary>
    /// <param name="language">the language</param>
    /// <param name="key">the translation key</param>
    /// <param name="defaultValue">a default value to use if the translation wasn't found</param>
    /// <returns>the localized string, if found; the default value otherwise</returns>
    public string GetValueOrDefault(string language, string key, string defaultValue);
}
