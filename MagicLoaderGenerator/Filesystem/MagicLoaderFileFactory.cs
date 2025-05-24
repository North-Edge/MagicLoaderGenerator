using MagicLoaderGenerator.Localization.Providers;
using System.Text.Json;

namespace MagicLoaderGenerator.Filesystem;

public static class MagicLoaderFileFactory
{
    /// <summary>
    /// Generates a dictionary of empty translations based on the specified localization keys
    /// </summary>
    /// <param name="entries">the localization keys</param>
    /// <returns>the entries if any localization</returns>
    private static Dictionary<string, string>? CreateEntries(List<string> entries)
    {
        if (entries.Count == 0)
            return null;
        // only strings starting with LOC_FN_ are supported
        return entries.Where(e => e.StartsWith(BaseLocalizationProvider.FullNamePrefix))
                      .Select(e => new KeyValuePair<string, string>(e, string.Empty))
                      .ToDictionary();
    }

    /// <summary>
    /// Create a <see cref="MagicLoaderFile"/> from a <see cref="ModFile"/> entry
    /// </summary>
    /// <param name="modFile">the <see cref="ModFile"/> used to create the file</param>
    /// <returns>a <see cref="MagicLoaderFile"/></returns>
    public static MagicLoaderFile Create(ModFile modFile)
    {
        return Load(modFile.InputFile) ?? new MagicLoaderFile {
            FullNames_Edit = CreateEntries(modFile.FullNamesEditEntries),
            FullNames = CreateEntries(modFile.FullNameEntries)
        };
    }

    // ReSharper disable once MemberCanBePrivate.Global
    /// <summary>
    /// Creates a <see cref="MagicLoaderFile"/> given its file path
    /// </summary>
    /// <param name="filePath">the path of the file</param>
    /// <param name="rethrow">flag specifying if JSON exceptions should be rethrown</param>
    /// <returns>the deserialized file if successful; null otherwise</returns>
    public static MagicLoaderFile? Load(string? filePath, bool rethrow = false)
    {
        if (string.IsNullOrEmpty(filePath) == false && File.Exists(filePath))
        {
            try
            {
                return JsonSerializer.Deserialize<MagicLoaderFile>(File.ReadAllText(filePath));
            }
            catch (JsonException)
            {
                if (rethrow)
                    throw;
            }
        }

        return null;
    }
}
