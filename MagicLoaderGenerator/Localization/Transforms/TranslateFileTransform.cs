using MagicLoaderGenerator.Localization.Abstractions;
using MagicLoaderGenerator.Filesystem.Abstractions;
using MagicLoaderGenerator.Localization.Providers;
using MagicLoaderGenerator.Filesystem;

namespace MagicLoaderGenerator.Localization.Transforms;

// ReSharper disable VirtualMemberNeverOverridden.Global
/// <summary>
/// Implementation of the <see cref="IMagicLoaderFileTransform"/> interface used
/// to replace localization keys with their translation found in the game files
/// </summary>
/// <param name="localization">
/// An implementation of the <see cref="ILocalizationProvider"/>
/// interface used to load the translations strings
/// </param>
public class TranslateFileTransform(ILocalizationProvider localization)
    : BaseLocalizationTransform(localization)
{
    /// <inheritdoc/>
    public override MagicLoaderFile Transform(string language, ModFile modFile)
    {
        // perform the common transform from the base implementation
        var result = base.Transform(language, modFile);

        if (result.FullNames_Edit != null)
        {
            foreach (var (key, translation) in result.FullNames_Edit)
            {
                // format each edit entry found in the MagicLoader file
                result.FullNames_Edit[key] = string.IsNullOrEmpty(translation) == false
                                           ? TranslateValue(language, translation)
                                           : FormatLine(language, key);
            }
        }
        if (result.FullNames != null)
        {
            foreach (var (key, translation) in result.FullNames)
            {
                // format each entry found in the MagicLoader file
                result.FullNames[key] = string.IsNullOrEmpty(translation) == false
                                      ? TranslateValue(language, translation)
                                      : FormatLine(language, key);
            }
        }

        return result;
    }

    /// <summary>
    /// Translates the placeholders found in the specified value
    /// </summary>
    /// <param name="language">the current language</param>
    /// <param name="value">the value to parse</param>
    /// <returns>the translated value</returns>
    protected virtual string TranslateValue(string language, string value)
    {
        var parts = BaseLocalizationProvider.ParseValue(value);
        var result = string.Empty;

        foreach (var part in parts)
        {
            if (part.StartsWith(BaseLocalizationProvider.FullNamePrefix))
                result += FormatLine(language, part);
            else
                result += part;
        }

        return result;
    }

    /// <summary>
    /// Translates a localization string
    /// </summary>
    /// <param name="language">the current language</param>
    /// <param name="key">the localization key</param>
    /// <returns>the result of the translation</returns>
    protected virtual string Translate(string language, string key)
    {
        var defaultValue = base.FormatLine(language, key);

        // forward the translation to the default game localization for fullname localization strings
        if (BaseLocalizationProvider.IsBaseLanguage(language)
            && key.StartsWith(BaseLocalizationProvider.FullNamePrefix))
        {
            return defaultValue;
        }

        // retrieve the translation for the entry and default to the game localization if not found
        return Localization.GetValueOrDefault(language, key, defaultValue) ?? defaultValue;
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
        return Translate(language, key);
    }
}
