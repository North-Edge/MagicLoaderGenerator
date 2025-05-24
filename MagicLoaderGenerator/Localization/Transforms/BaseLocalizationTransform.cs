using MagicLoaderGenerator.Localization.Abstractions;
using MagicLoaderGenerator.Filesystem.Abstractions;
using MagicLoaderGenerator.Filesystem;

namespace MagicLoaderGenerator.Localization.Transforms;

/// <summary>
/// Base implementation of the <see cref="IMagicLoaderFileTransform"/> interface
/// </summary>
/// <param name="localization">
/// An implementation of the <see cref="ILocalizationProvider"/>
/// interface used to load the translations strings
/// </param>
public abstract class BaseLocalizationTransform(ILocalizationProvider localization): IMagicLoaderFileTransform
{
    /// <summary>
    /// The localization provider implementation
    /// </summary>
    protected readonly ILocalizationProvider Localization = localization;

    /// <inheritdoc/>
    public abstract MagicLoaderFile Transform(string language, MagicLoaderFile magicLoaderFile);

    /// <inheritdoc/>
    public abstract string GetOutputName(string baseName, string language);

    /// <summary>
    /// Adds the extra localization loaded from the configuration to the specified file
    /// </summary>
    /// <param name="localizationProvider">an implementation of the <see cref="ILocalizationProvider"/> interface</param>
    /// <param name="magicLoaderFile">the file to which the translations are added</param>
    /// <param name="language">the current language</param>
    protected static void AddExtraLocalization(ILocalizationProvider localizationProvider,
                                               MagicLoaderFile magicLoaderFile,
                                               string language)
    {
        // create a dictionary to hold the result of the transformations
        magicLoaderFile.FullNames ??= new Dictionary<string, string>();

        foreach (var (key, localizations) in localizationProvider.ExtraLocalizationStrings)
        {
            if (localizations.TryGetValue(language, out var extraLocString))
            {
                magicLoaderFile.FullNames[key] = extraLocString;
            }
        }
    }

    /// <summary>
    /// Formats the current line by replacing the entry key with its translation
    /// </summary>
    /// <param name="language">the language used in the transformation</param>
    /// <param name="key">the translation key to format</param>
    /// <returns>the formatted line</returns>
    protected virtual string FormatLine(string language, string key)
    {
        return $"$[[{key}]]";
    }
}
