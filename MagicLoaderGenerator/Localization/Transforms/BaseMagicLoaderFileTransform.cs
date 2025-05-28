using MagicLoaderGenerator.Filesystem.Abstractions;
using MagicLoaderGenerator.Filesystem;

namespace MagicLoaderGenerator.Localization.Transforms;

/// <summary>
/// Base implementation of the <see cref="IMagicLoaderFileTransform"/> interface
/// </summary>
public class BaseMagicLoaderFileTransform: IMagicLoaderFileTransform
{
    // ReSharper disable once MemberCanBePrivate.Global
    /// <summary>
    /// The start marker for translation placeholders
    /// </summary>
    public const string MarkerStart = "$[[";
    // ReSharper disable once MemberCanBePrivate.Global
    /// <summary>
    /// The end marker for translation placeholders
    /// </summary>
    public const string MarkerEnd = "]]";

    /// <inheritdoc/>
    public virtual MagicLoaderFile Transform(ModFile modFile)
    {
        var result = MagicLoaderFileFactory.Create(modFile);


        foreach (var section in MagicLoaderSectionsEnum.Sections.Select(sectionName => result[sectionName])
                                                                .OfType<Dictionary<string, string>>())
        {
            List<string> identities = [];

            foreach (var (key, translation) in section)
            {
                var transformedLine = string.IsNullOrEmpty(translation) == false ? translation : FormatLine(key);

                if (transformedLine != $"{MarkerStart}{key}{MarkerEnd}")
                {
                    section[key] = transformedLine;
                }
                else
                {
                    identities.Add(key);
                }
            }
            // remove identity entries
            foreach (var key in identities)
            {
                section.Remove(key);
            }
        }

        if (result.Localization != null)
        {
            List<LocalizationEntry> identities = [];

            foreach (var entry in result.Localization)
            {
                if (entry.HasTranslation())
                    continue;

                var transformedLine = FormatLine(entry.Key);

                if (transformedLine != $"{MarkerStart}{entry.Key}{MarkerEnd}")
                {
                    entry.Global_Edit = transformedLine;
                }
                else
                {
                    identities.Add(entry);
                }
            }
            // remove identity entries
            foreach (var entry in identities)
            {
                result.Localization.Remove(entry);
            }
        }

        return result;
    }

    /// <summary>
    /// Formats the current line by replacing the entry key with its translation
    /// </summary>
    /// <param name="key">the translation key to format</param>
    /// <returns>the formatted line</returns>
    protected virtual string FormatLine(string key)
    {
        return $"{MarkerStart}{key}{MarkerEnd}";
    }
}
