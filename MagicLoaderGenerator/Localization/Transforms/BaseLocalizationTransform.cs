using MagicLoaderGenerator.Localization.Abstractions;
using MagicLoaderGenerator.Filesystem.Abstractions;
using MagicLoaderGenerator.Filesystem;
using MagicLoaderGenerator.Localization.Providers;

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
    public virtual MagicLoaderFile Transform(string language, ModFile modFile)
    {
        var result = MagicLoaderFileFactory.Create(modFile);

        if (Localization.ExtraLocalizationStrings.Count > 0)
        {
            // add the localization strings from the configuration
            AddExtraLocalization(Localization, result, language);
        }

        return result;
    }

    /// <inheritdoc/>
    public abstract string GetOutputName(string baseName, string language);

    /// <summary>
    /// Adds the extra localization loaded from the configuration to the specified file
    /// </summary>
    /// <param name="localizationProvider">an implementation of the <see cref="ILocalizationProvider"/> interface</param>
    /// <param name="modFile">the file to which the translations are added</param>
    /// <param name="language">the current language</param>
    private static void AddExtraLocalization(ILocalizationProvider localizationProvider,
                                             MagicLoaderFile modFile, string language)
    {
        modFile.FullNames ??= [];

        foreach (var (key, localizations) in localizationProvider.ExtraLocalizationStrings)
        {
            if (localizations.TryGetValue(language, out var extraLocString))
            {
                modFile.FullNames[key] = extraLocString;
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
        return $"{BaseLocalizationProvider.MarkerStart}{key}{BaseLocalizationProvider.MarkerEnd}";
    }
}
