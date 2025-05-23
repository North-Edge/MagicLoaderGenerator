using MagicLoaderGenerator.Localization.Abstractions;

namespace MagicLoaderGenerator.Localization.Providers;

/// <summary>
/// Base implementation of the <see cref="ILocalizationProvider"/> interface
/// </summary>
/// <param name="configuration">the localization configuration</param>
public abstract class BaseLocalizationProvider(ILocalizationConfiguration configuration) : ILocalizationProvider
{
    /// <summary>
    /// The source of  the localization data (could be a directory or database connection string).
    /// If no value is specified, the localization data is assumed to be located in the default directory.
    /// </summary>
    protected readonly string LocalizationSource = configuration.LocalizationSource ?? DefaultDirectory;
    /// <summary>
    /// The default directory relative to the working directory of the executable
    /// </summary>
    private const string DefaultDirectory = "Localization";
    // ReSharper disable once MemberCanBePrivate.Global
    /// <summary>
    /// The prefix for fullname translation keys
    /// </summary>
    public const string FullnamePrefix = "LOC_FN_";
    /// <summary>
    /// A list of the default languages
    /// </summary>
    private static List<string> DefaultLanguages => [
        LanguagesEnum.German,
        LanguagesEnum.English,
        LanguagesEnum.Spanish,
        LanguagesEnum.French,
        LanguagesEnum.Italian,
        LanguagesEnum.Japanese,
        LanguagesEnum.Polish,
        LanguagesEnum.Portuguese,
        LanguagesEnum.Russian,
        LanguagesEnum.SimplifiedChinese
    ];
    /// <summary>
    /// Dictionary containing the loaded translation dictionaries as value and the language as key
    /// </summary>
    protected readonly Dictionary<string, Dictionary<string, string>> LocalizationStrings = new();
    /// <inheritdoc/>
    public virtual List<string> SupportedLanguages => configuration.Languages ?? DefaultLanguages;

    /// <inheritdoc/>
    public virtual string GetValueOrDefault(string language, string key, string defaultValue)
    {
        // forward the translation to the default game localization for fullname localization strings
        if (IsBaseLanguage(language) && key.StartsWith(FullnamePrefix))
        {
            return defaultValue;
        }
        // retrieve the localization strings for the current language
        if (LocalizationStrings.TryGetValue(language, out var locStrings) == false)
        {
            // load the localization data for the current language
            locStrings = LoadLocalization(language, configuration.IncludedSections);
        }
        // retrieve the translated data
        return locStrings?.GetValueOrDefault(key, defaultValue) ?? defaultValue;
    }

    /// <summary>
    /// Checks if the specified language is the base language (English)
    /// </summary>
    /// <param name="language">the language to check</param>
    /// <returns><c>true</c> if the specified language is the base language, <c>false</c> otherwise</returns>
    private static bool IsBaseLanguage(string language) => language == LanguagesEnum.English;

    /// <summary>
    /// Loads the localization data for the specified languages
    /// </summary>
    /// <param name="language">the language to load</param>
    /// <param name="sections">list of sections to load</param>
    /// <returns>the translation data</returns>
    protected abstract Dictionary<string, string>? LoadLocalization(string language, List<string>? sections = null);
}
