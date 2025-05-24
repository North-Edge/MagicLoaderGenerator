using MagicLoaderGenerator.Localization.Abstractions;

namespace MagicLoaderGenerator.Localization.Transforms;

// ReSharper disable once UnusedType.Global
/// <summary>
/// Specialization of the <see cref="TranslateFileTransform"/> implementation used to add a suffix to the translated entry
/// </summary>
/// <param name="localization">
/// An implementation of the <see cref="ILocalizationProvider"/>
/// interface used to load the translations strings
/// </param>
/// <param name="suffix">the suffix to add to the translated entries</param>
public class ValueSuffixTransformer(ILocalizationProvider localization, string suffix)
    : TranslateFileTransform(localization)
{
    /// <inheritdoc/>
    protected override string FormatLine(string language, string key)
    {
        // translate the entry then add the suffix to it
        return $"{Translate(language, key)} {suffix}";
    }
}
