using MagicLoaderGenerator.Localization.Abstractions;
using System.Text.RegularExpressions;

namespace MagicLoaderGenerator.Localization.Providers;

// ReSharper disable MemberCanBePrivate.Global
/// <summary>
/// Base implementation of the <see cref="ILocalizationProvider"/> interface
/// </summary>
public abstract partial class BaseLocalizationProvider : ILocalizationProvider
{
    /// <summary>
    /// The prefix for all localization entries
    /// </summary>
    public const string LocalizationPrefix = "LOC_";
    /// <summary>
    /// The prefix for FullName entries
    /// </summary>
    public const string FullNamePrefix = LocalizationPrefix + "FN_";
    /// <summary>
    /// The start marker for translation placeholders
    /// </summary>
    public const string MarkerStart = "$[[";
    /// <summary>
    /// The end marker for translation placeholders
    /// </summary>
    public const string MarkerEnd = "]]";

    /// <summary>
    /// Regular expression used to parse an existing localization string in order to find placeholders
    /// </summary>
    /// <returns>the regular expression generated at compilation time</returns>
    [GeneratedRegex(@"([^$]*)?(\$\[\[)(" + LocalizationPrefix +@"[A-Za-z0-9_]+)(\]\])([^$]*)?")]
    private static partial Regex TranslationKeyRegex();

    /// <summary>
    /// The source of  the localization data (could be a directory or database connection string).
    /// If no value is specified, the localization data is assumed to be located in the default directory.
    /// </summary>
    protected readonly string LocalizationSource;
    /// <summary>
    /// The default directory relative to the working directory of the executable
    /// </summary>
    private const string DefaultDirectory = "Localization";
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
    public Dictionary<string, Dictionary<string, string>> ExtraLocalizationStrings { get; } = new();
    /// <summary>
    /// The localization configuration
    /// </summary>
    private readonly ILocalizationConfiguration _configuration;

    /// <summary>
    /// Base implementation of the <see cref="ILocalizationProvider"/> interface
    /// </summary>
    /// <param name="configuration">the localization configuration</param>
    protected BaseLocalizationProvider(ILocalizationConfiguration configuration)
    {
        _configuration = configuration;
        LoadLocalizationFromConfig(_configuration);
        LocalizationSource = _configuration.LocalizationSource ?? DefaultDirectory;
    }

    /// <inheritdoc/>
    public virtual List<string> SupportedLanguages => _configuration.Languages ?? DefaultLanguages;

    /// <inheritdoc/>
    public virtual string? GetValueOrDefault(string language, string key, string? defaultValue = null)
    {
        // retrieve the localization strings for the current language
        if (LocalizationStrings.TryGetValue(language, out var locStrings) == false)
        {
            // load the localization data for the current language
            locStrings = LoadLocalization(language, _configuration.IncludedSections);
        }
        // retrieve the translated data
        if (locStrings != null && locStrings.TryGetValue(key, out var result))
            return result;

        // if the localization wasn't found, check the extra localization data
        return ExtraLocalizationStrings.TryGetValue(language, out locStrings)
             ? locStrings.GetValueOrDefault(key) : defaultValue;
    }

    /// <summary>
    /// Checks if the specified language is the base language (English)
    /// </summary>
    /// <param name="language">the language to check</param>
    /// <returns><c>true</c> if the specified language is the base language, <c>false</c> otherwise</returns>
    public static bool IsBaseLanguage(string language) => language == LanguagesEnum.English;

    /// <summary>
    /// Loads the project specific localization strings from the configuration
    /// </summary>
    /// <param name="configuration">the configuration containing the localization data</param>
    private void LoadLocalizationFromConfig(ILocalizationConfiguration configuration)
    {
        foreach (var (key, localizations)  in configuration.Localizations)
        {
            foreach (var (language, localization) in localizations)
            {
                if (ExtraLocalizationStrings.TryGetValue(language, out var languageLocStrings) == false)
                {
                    languageLocStrings = new Dictionary<string, string>();
                    ExtraLocalizationStrings[language] = languageLocStrings;
                }

                languageLocStrings[key] = localization;
            }
        }
    }

    /// <summary>
    /// Loads the localization data for the specified languages
    /// </summary>
    /// <param name="language">the language to load</param>
    /// <param name="sections">list of sections to load</param>
    /// <returns>the translation data</returns>
    protected abstract Dictionary<string, string>? LoadLocalization(string language, List<string>? sections = null);

    /// <summary>
    /// Parses a value and return text or placeholders found in the value
    /// </summary>
    /// <param name="value">the value to parse</param>
    /// <returns>an enumeration of valid tokens</returns>
    public static IEnumerable<string> ParseValue(string value)
    {
        // filter empty strings or markers
        return TranslationKeyRegex().Split(value).Where(s => string.IsNullOrEmpty(s) == false
                                                          && s != MarkerStart
                                                          && s != MarkerEnd);
    }
}
