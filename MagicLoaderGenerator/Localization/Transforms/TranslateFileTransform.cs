using MagicLoaderGenerator.Localization.Abstractions;
using MagicLoaderGenerator.Filesystem.Abstractions;
using MagicLoaderGenerator.Localization.Providers;
using MagicLoaderGenerator.Filesystem;

namespace MagicLoaderGenerator.Localization.Transforms;

/// <summary>
/// Implementation of the <see cref="IMagicLoaderFileTransform"/> interface used
/// to replace localization keys with their translation found in the game files
/// </summary>
/// <param name="localization">
/// An implementation of the <see cref="ILocalizationProvider"/>
/// interface used to load the translations strings
/// </param>
public class TranslateFileTransform(ILocalizationProvider localization): BaseLocalizationTransform(localization)
{
    /// <inheritdoc/>
    public override MagicLoaderFile Transform(string language, MagicLoaderFile magicLoaderFile)
    {
        // check if the current MagicLoader file has FullNames_Edit entries to process
        if (magicLoaderFile.FullNamesEditEntries.Count > 0)
        {
            // create a dictionary to hold the result of the transformations
            magicLoaderFile.FullNames_Edit ??= new Dictionary<string, string>();

            foreach (var entry in magicLoaderFile.FullNamesEditEntries)
            {
                // format each entry found in the MagicLoader file
                magicLoaderFile.FullNames_Edit[entry] = FormatLine(language, entry);
            }
        }
        // check if the current MagicLoader file has FullNames entries to process
        if (magicLoaderFile.FullNameEntries.Count > 0 || Localization.ExtraLocalizationStrings.Count > 0)
        {
            // create a dictionary to hold the result of the transformations
            magicLoaderFile.FullNames ??= new Dictionary<string, string>();
            // add the localization strings from the configuration
            AddExtraLocalization(Localization, magicLoaderFile, language);

            foreach (var entry in magicLoaderFile.FullNameEntries)
            {
                // format each entry found in the MagicLoader file
                magicLoaderFile.FullNames[entry] = FormatLine(language, entry);
            }
        }

        return magicLoaderFile;
    }

    /// <inheritdoc/>
    public override string GetOutputName(string baseName, string language)
    {
        // suffix the name of the file with the language
        return $"{baseName}_{language}";
    }

    /// <inheritdoc/>
    protected override string FormatLine(string language, string key)
    {
        var defaultValue = base.FormatLine(language, key);

        // forward the translation to the default game localization for fullname localization strings
        if (BaseLocalizationProvider.IsBaseLanguage(language) && key.StartsWith(Localization.FullNamePrefix))
        {
            return defaultValue;
        }

        // retrieve the translation for the entry and default to the game localization if not found
        return Localization.GetValueOrDefault(language, key, defaultValue) ?? defaultValue;
    }
}
