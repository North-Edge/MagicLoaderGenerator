using MagicLoaderGenerator.Localization.Abstractions;
using MagicLoaderGenerator.Filesystem.Abstractions;
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
public class TranslateFileTransform(ILocalizationProvider localization): IMagicLoaderFileTransform
{
    /// <inheritdoc/>
    public virtual MagicLoaderFile Transform(string language, MagicLoaderFile magicLoaderFile)
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
        if (magicLoaderFile.FullNameEntries.Count > 0)
        {
            // create a dictionary to hold the result of the transformations
            magicLoaderFile.FullNames ??= new Dictionary<string, string>();

            foreach (var entry in magicLoaderFile.FullNameEntries)
            {
                // format each entry found in the MagicLoader file
                magicLoaderFile.FullNames[entry] = FormatLine(language, entry);
            }
        }

        return magicLoaderFile;
    }

    /// <inheritdoc/>
    public virtual string GetOutputName(string baseName, string language)
    {
        // suffix the name of the file with the language
        return $"{baseName}_{language}";
    }

    /// <summary>
    /// Formats the current line by replacing the entry key with its translation
    /// </summary>
    /// <param name="language">the language used in the transformation</param>
    /// <param name="key">the translation key to format</param>
    /// <returns></returns>
    protected virtual string FormatLine(string language, string key)
    {
        // retrieve the translation for the entry and default to the game localization if not found
        return localization.GetValueOrDefault(language, key, $"$[[{key}]]");
    }
}
