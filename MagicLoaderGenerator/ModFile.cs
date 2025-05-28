using MagicLoaderGenerator.Filesystem;
using MagicLoaderGenerator.Localization;

namespace MagicLoaderGenerator;

/// <summary>
/// A record used to load the translation keys to be used to generate MagicLoader files or the path of an existing file.
/// </summary>
public sealed record ModFile
{
    // ReSharper disable once ReplaceAutoPropertyWithComputedProperty
    /// <summary>
    /// The required version of MagicLoader
    /// </summary>
    public string RequiresVersion { get; } = MagicLoaderFile.LatestMagicLoaderVersion;
    // ReSharper disable once ReplaceAutoPropertyWithComputedProperty
    // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Global
    // ReSharper disable once PropertyCanBeMadeInitOnly.Global
    /// <summary>
    /// This field is required to use NewCells or CellMap entries, due to technical details in how the game treats cells
    /// </summary>
    public string? Plugin { get; init; } = null;
    // ReSharper disable once ReplaceAutoPropertyWithComputedProperty
    // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Global
    // ReSharper disable once PropertyCanBeMadeInitOnly.Global
    /// <summary>
    /// This field means that this specific .json will attempt to load before all the specified .json files
    /// </summary>
    public List<string>? LoadBefore { get; init; } = null;
    // ReSharper disable once ReplaceAutoPropertyWithComputedProperty
    // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Global
    // ReSharper disable once PropertyCanBeMadeInitOnly.Global
    /// <summary>
    /// This field means that this specific .json will attempt to load after all the specified .json files
    /// </summary>
    public List<string>? LoadAfter { get; init; } = null;
    // ReSharper disable once ReplaceAutoPropertyWithComputedProperty
    // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Global
    // ReSharper disable once PropertyCanBeMadeInitOnly.Global
    /// <summary>
    /// This field means that this specific .json file will be skipped if these json files are not present.
    /// </summary>
    public List<string>? RequiresJson { get; init; } = null;
    // ReSharper disable once ReplaceAutoPropertyWithComputedProperty
    // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Global
    // ReSharper disable once PropertyCanBeMadeInitOnly.Global
    /// <summary>
    /// This field means that this specific .json file will be skipped if the specified mods are not active in plugins.txt
    /// </summary>
    public List<string>? RequiresActivePlugin { get; init; } = null;
    // ReSharper disable once ReplaceAutoPropertyWithComputedProperty
    // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Global
    // ReSharper disable once MemberCanBePrivate.Global
    /// <summary>
    /// This field registers new interior cells
    /// </summary>
    public List<string>? NewCells { get; init; } = null;
    // ReSharper disable once ReplaceAutoPropertyWithComputedProperty
    // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Global
    // ReSharper disable once PropertyCanBeMadeInitOnly.Global
    // ReSharper disable once CommentTypo
    /// <summary>
    /// If you have a custom <c>.umap</c>, or wish to use a different <c>.umap</c> than our selection of
    /// <c>/Game/Maps/Interiors/L_TestCastleInterior</c>,  you can map your cell's short
    /// FormID to a custom <c>.umap</c> path here
    /// </summary>
    public Dictionary<string, string>? CellMap { get; init; } = null;
    // ReSharper disable once ReplaceAutoPropertyWithComputedProperty
    // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Global
    // ReSharper disable once PropertyCanBeMadeInitOnly.Global
    // ReSharper disable once CollectionNeverUpdated.Global
    // ReSharper disable once MemberCanBePrivate.Global
    /// <summary>
    /// NewStrings is a convenience entry that lets you add/edit StringTable entries in one convenient place.
    /// It will automatically use the prefix to determine which StringTable it belongs in, and insert it accordingly.
    /// <see cref="Localization.LocStringPrefixesEnum"/>
    /// </summary>
    public Dictionary<string, string>? NewStrings { get; init; } = null;
    // ReSharper disable once ReplaceAutoPropertyWithComputedProperty
    // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Global
    // ReSharper disable once PropertyCanBeMadeInitOnly.Global
    // ReSharper disable once CollectionNeverUpdated.Global
    // ReSharper disable once MemberCanBePrivate.Global
    /// <summary>
    /// New localization entries
    /// </summary>
    public List<LocalizationEntry>? Localization { get; init; } = null;
    // ReSharper disable once CollectionNeverUpdated.Global
    // ReSharper disable once MemberCanBePrivate.Global
    /// <summary>
    /// A list of translation keys used to generate the FullNames entries of the mod files
    /// </summary>
    public List<string> Entries { get; } = [];
    // ReSharper disable once CollectionNeverUpdated.Global
    // ReSharper disable once MemberCanBePrivate.Global
    /// <summary>
    /// A list of translation keys used to generate the FullNamesEdit entries of the mod files
    /// </summary>
    public List<string> EditEntries { get; } = [];
    // ReSharper disable once CollectionNeverUpdated.Global
    // ReSharper disable once MemberCanBePrivate.Global
    /// <summary>
    /// A list of translation keys used to generate GlobalEdit entries in the Localization of the mod
    /// </summary>
    public List<string> GlobalEdit { get; } = [];
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    /// <summary>
    /// The path of an existing input file
    /// </summary>
    public string? InputFile { get; init; }

    /// <summary>
    /// Checks if the MagicLoader file contains any potential data
    /// </summary>
    /// <returns><c>true</c> if the file contains data; <c>false</c> otherwise</returns>
    public bool HasEntries()
    {
        return string.IsNullOrEmpty(InputFile) == false
            || NewStrings?.Count != 0
            || EditEntries.Count != 0
            || GlobalEdit.Count != 0
            || Entries.Count != 0;
    }
}
