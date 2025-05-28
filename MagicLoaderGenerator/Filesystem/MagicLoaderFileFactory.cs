using MagicLoaderGenerator.Localization;
using System.Text.Json;

namespace MagicLoaderGenerator.Filesystem;

public static class MagicLoaderFileFactory
{
    /// <summary>
    /// Generates a dictionary of empty translations based on the specified localization keys
    /// </summary>
    /// <param name="entries">the localization keys</param>
    /// <returns>the entries if any localization</returns>
    private static Dictionary<string, Dictionary<string, string>> GroupEntries(IList<string> entries)
    {
        var result = new Dictionary<string, Dictionary<string, string>>();

        foreach (var entry in entries)
        {
            foreach (var prefix in LocStringPrefixesEnum.Values.Where(entry.StartsWith))
            {
                if (result.TryGetValue(prefix, out var dictionary) == false)
                {
                    result[prefix] = dictionary = [];
                }

                dictionary.Add(entry, string.Empty);
            }
        }

        return result;
    }

    /// <summary>
    /// Create a <see cref="MagicLoaderFile"/> from a <see cref="ModFile"/> entry
    /// </summary>
    /// <param name="modFile">the <see cref="ModFile"/> used to create the file</param>
    /// <returns>a <see cref="MagicLoaderFile"/></returns>
    public static MagicLoaderFile Create(ModFile modFile)
    {
        var groupedEntries = GroupEntries(modFile.Entries);
        var groupedEditEntries = GroupEntries(modFile.EditEntries);
        var localization = modFile.GlobalEdit.Count == 0 ? null
            : modFile.GlobalEdit.Select(key => new LocalizationEntry { Key = key }).ToList();

        if (localization != null && modFile.Localization is { Count: > 0 })
        {
            localization.AddRange(modFile.Localization);
        }

        return Load(modFile.InputFile) ?? new MagicLoaderFile {
            AltarDynamicTexts_Edit = groupedEditEntries.GetValueOrDefault(LocStringPrefixesEnum.AltarDynamicTexts),
            AltarStaticTexts_Edit = groupedEditEntries.GetValueOrDefault(LocStringPrefixesEnum.AltarStaticTexts),
            HardcodedContent_Edit = groupedEditEntries.GetValueOrDefault(LocStringPrefixesEnum.HardcodedContent),
            MissingEntries_Edit = groupedEditEntries.GetValueOrDefault(LocStringPrefixesEnum.MissingEntries),
            ResponseTexts_Edit = groupedEditEntries.GetValueOrDefault(LocStringPrefixesEnum.ResponseTexts),
            ScriptContent_Edit = groupedEditEntries.GetValueOrDefault(LocStringPrefixesEnum.ScriptContent),
            AltarDynamicTexts = groupedEntries.GetValueOrDefault(LocStringPrefixesEnum.AltarDynamicTexts),
            Descriptions_Edit = groupedEditEntries.GetValueOrDefault(LocStringPrefixesEnum.Descriptions),
            AltarStaticTexts = groupedEntries.GetValueOrDefault(LocStringPrefixesEnum.AltarStaticTexts),
            HardcodedContent = groupedEntries.GetValueOrDefault(LocStringPrefixesEnum.HardcodedContent),
            BookContent_Edit = groupedEditEntries.GetValueOrDefault(LocStringPrefixesEnum.BookContent),
            LogEntries_Edit = groupedEditEntries.GetValueOrDefault(LocStringPrefixesEnum.LogEntries),
            MissingEntries = groupedEntries.GetValueOrDefault(LocStringPrefixesEnum.MissingEntries),
            FullNames_Edit = groupedEditEntries.GetValueOrDefault(LocStringPrefixesEnum.FullNames),
            ResponseTexts = groupedEntries.GetValueOrDefault(LocStringPrefixesEnum.ResponseTexts),
            ScriptContent = groupedEntries.GetValueOrDefault(LocStringPrefixesEnum.ScriptContent),
            Descriptions = groupedEntries.GetValueOrDefault(LocStringPrefixesEnum.Descriptions),
            BookContent = groupedEntries.GetValueOrDefault(LocStringPrefixesEnum.BookContent),
            LogEntries = groupedEntries.GetValueOrDefault(LocStringPrefixesEnum.LogEntries),
            FullNames = groupedEntries.GetValueOrDefault(LocStringPrefixesEnum.FullNames),
            RequiresActivePlugin = modFile.RequiresActivePlugin,
            RequiresVersion = modFile.RequiresVersion,
            RequiresJson = modFile.RequiresJson,
            NewStrings = modFile.NewStrings,
            LoadBefore = modFile.LoadBefore,
            LoadAfter = modFile.LoadAfter,
            NewCells = modFile.NewCells,
            Localization = localization,
            CellMap = modFile.CellMap,
            Plugin = modFile.Plugin
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
