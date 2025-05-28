using MagicLoaderGenerator.Localization;

namespace MagicLoaderGenerator.Filesystem;

/// <summary>
/// A record used to handle JSON (de)serialization of MagicLoader file
/// </summary>
public sealed record MagicLoaderFile
{
    /// <summary>
    /// Latest MagicLoader version
    /// </summary>
    public const string LatestMagicLoaderVersion = "2.4";
    // ReSharper disable once PropertyCanBeMadeInitOnly.Global
    /// <summary>
    /// The required version of MagicLoader
    /// </summary>
    public string? RequiresVersion { get; set; } = LatestMagicLoaderVersion;
    // ReSharper disable once PropertyCanBeMadeInitOnly.Global
    /// <summary>
    /// This field is required to use NewCells or CellMap entries, due to technical details in how the game treats cells
    /// </summary>
    public string? Plugin { get; set; }
    // ReSharper disable once PropertyCanBeMadeInitOnly.Global
    /// <summary>
    /// This field means that this specific .json will attempt to load before all the specified .json files
    /// </summary>
    public List<string>? LoadBefore { get; set; } = [];
    // ReSharper disable once PropertyCanBeMadeInitOnly.Global
    /// <summary>
    /// This field means that this specific .json will attempt to load after all the specified .json files
    /// </summary>
    public List<string>? LoadAfter { get; set; } = [];
    // ReSharper disable once PropertyCanBeMadeInitOnly.Global
    /// <summary>
    /// This field means that this specific .json file will be skipped if these json files are not present.
    /// </summary>
    public List<string>? RequiresJson { get; set; } = [];
    // ReSharper disable once PropertyCanBeMadeInitOnly.Global
    /// <summary>
    /// This field means that this specific .json file will be skipped if the specified mods are not active in plugins.txt
    /// </summary>
    public List<string>? RequiresActivePlugin { get; set; } = [];
    // ReSharper disable once PropertyCanBeMadeInitOnly.Global
    /// <summary>
    /// This field registers new interior cells
    /// </summary>
    public List<string>? NewCells { get; set; } = [];
    // ReSharper disable once PropertyCanBeMadeInitOnly.Global
    // ReSharper disable once CommentTypo
    /// <summary>
    /// If you have a custom <c>.umap</c>, or wish to use a different <c>.umap</c> than our selection of
    /// <c>/Game/Maps/Interiors/L_TestCastleInterior</c>,  you can map your cell's short
    /// FormID to a custom <c>.umap</c> path here
    /// </summary>
    public Dictionary<string, string>? CellMap { get; set; } = [];
    // ReSharper disable once PropertyCanBeMadeInitOnly.Global
    /// <summary>
    /// NewStrings is a convenience entry that lets you add/edit StringTable entries in one convenient place.
    /// It will automatically use the prefix to determine which StringTable it belongs in, and insert it accordingly.
    /// <see cref="LocStringPrefixesEnum"/>
    /// </summary>
    public Dictionary<string, string>? NewStrings { get; set; } = [];
    // ReSharper disable once PropertyCanBeMadeInitOnly.Global
    // ReSharper disable once CollectionNeverUpdated.Global
    // ReSharper disable once MemberCanBePrivate.Global
    /// <summary>
    /// A list of translation keys used to generate the AltarDynamicTexts entries of the mod files
    /// </summary>
    public Dictionary<string, string>? AltarDynamicTexts { get; set; } = [];
    // ReSharper disable once PropertyCanBeMadeInitOnly.Global
    // ReSharper disable once CollectionNeverUpdated.Global
    // ReSharper disable once MemberCanBePrivate.Global
    // ReSharper disable once InconsistentNaming
    /// <summary>
    /// A list of translation keys used to generate the AltarDynamicTexts_Edit entries of the mod files
    /// </summary>
    public Dictionary<string, string>? AltarDynamicTexts_Edit { get; set; } = [];
    // ReSharper disable once PropertyCanBeMadeInitOnly.Global
    // ReSharper disable once CollectionNeverUpdated.Global
    // ReSharper disable once MemberCanBePrivate.Global
    /// <summary>
    /// A list of translation keys used to generate the AltarStaticTexts entries of the mod files
    /// </summary>
    public Dictionary<string, string>? AltarStaticTexts { get; set; } = [];
    // ReSharper disable once PropertyCanBeMadeInitOnly.Global
    // ReSharper disable once CollectionNeverUpdated.Global
    // ReSharper disable once MemberCanBePrivate.Global
    // ReSharper disable once InconsistentNaming
    /// <summary>
    /// A list of translation keys used to generate the AltarStaticTexts_Edit entries of the mod files
    /// </summary>
    public Dictionary<string, string>? AltarStaticTexts_Edit { get; set; } = [];
    // ReSharper disable once PropertyCanBeMadeInitOnly.Global
    // ReSharper disable once CollectionNeverUpdated.Global
    // ReSharper disable once MemberCanBePrivate.Global
    /// <summary>
    /// A list of translation keys used to generate the BookContent entries of the mod files
    /// </summary>
    public Dictionary<string, string>? BookContent { get; set; } = [];
    // ReSharper disable once PropertyCanBeMadeInitOnly.Global
    // ReSharper disable once CollectionNeverUpdated.Global
    // ReSharper disable once MemberCanBePrivate.Global
    // ReSharper disable once InconsistentNaming
    /// <summary>
    /// A list of translation keys used to generate the BookContent_Edit entries of the mod files
    /// </summary>
    public Dictionary<string, string>? BookContent_Edit { get; set; } = [];
    // ReSharper disable once PropertyCanBeMadeInitOnly.Global
    // ReSharper disable once CollectionNeverUpdated.Global
    // ReSharper disable once MemberCanBePrivate.Global
    /// <summary>
    /// A list of translation keys used to generate the Descriptions entries of the mod files
    /// </summary>
    public Dictionary<string, string>? Descriptions { get; set; } = [];
    // ReSharper disable once PropertyCanBeMadeInitOnly.Global
    // ReSharper disable once CollectionNeverUpdated.Global
    // ReSharper disable once MemberCanBePrivate.Global
    // ReSharper disable once InconsistentNaming
    /// <summary>
    /// A list of translation keys used to generate the Descriptions_Edit entries of the mod files
    /// </summary>
    public Dictionary<string, string>? Descriptions_Edit { get; set; } = [];
    // ReSharper disable once PropertyCanBeMadeInitOnly.Global
    // ReSharper disable once CollectionNeverUpdated.Global
    // ReSharper disable once MemberCanBePrivate.Global
    /// <summary>
    /// A list of translation keys used to generate the FullNames entries of the mod files
    /// </summary>
    public Dictionary<string, string>? FullNames { get; set; } = [];
    // ReSharper disable once PropertyCanBeMadeInitOnly.Global
    // ReSharper disable once CollectionNeverUpdated.Global
    // ReSharper disable once MemberCanBePrivate.Global
    // ReSharper disable once InconsistentNaming
    /// <summary>
    /// A list of translation keys used to generate the FullNames_Edit entries of the mod files
    /// </summary>
    public Dictionary<string, string>? FullNames_Edit { get; set; } = [];
    // ReSharper disable once PropertyCanBeMadeInitOnly.Global
    // ReSharper disable once CollectionNeverUpdated.Global
    // ReSharper disable once MemberCanBePrivate.Global
    /// <summary>
    /// A list of translation keys used to generate the HardcodedContent entries of the mod files
    /// </summary>
    public Dictionary<string, string>? HardcodedContent { get; set; } = [];
    // ReSharper disable once PropertyCanBeMadeInitOnly.Global
    // ReSharper disable once CollectionNeverUpdated.Global
    // ReSharper disable once MemberCanBePrivate.Global
    // ReSharper disable once InconsistentNaming
    /// <summary>
    /// A list of translation keys used to generate the HardcodedContent_Edit entries of the mod files
    /// </summary>
    public Dictionary<string, string>? HardcodedContent_Edit { get; set; } = [];
    // ReSharper disable once PropertyCanBeMadeInitOnly.Global
    // ReSharper disable once CollectionNeverUpdated.Global
    // ReSharper disable once MemberCanBePrivate.Global
    /// <summary>
    /// A list of translation keys used to generate the LogEntries entries of the mod files
    /// </summary>
    public Dictionary<string, string>? LogEntries { get; set; } = [];
    // ReSharper disable once PropertyCanBeMadeInitOnly.Global
    // ReSharper disable once CollectionNeverUpdated.Global
    // ReSharper disable once MemberCanBePrivate.Global
    // ReSharper disable once InconsistentNaming
    /// <summary>
    /// A list of translation keys used to generate the LogEntries_Edit entries of the mod files
    /// </summary>
    public Dictionary<string, string>? LogEntries_Edit { get; set; } = [];
    // ReSharper disable once PropertyCanBeMadeInitOnly.Global
    // ReSharper disable once CollectionNeverUpdated.Global
    // ReSharper disable once MemberCanBePrivate.Global
    /// <summary>
    /// A list of translation keys used to generate the MissingEntries entries of the mod files
    /// </summary>
    public Dictionary<string, string>? MissingEntries { get; set; } = [];
    // ReSharper disable once PropertyCanBeMadeInitOnly.Global
    // ReSharper disable once CollectionNeverUpdated.Global
    // ReSharper disable once MemberCanBePrivate.Global
    // ReSharper disable once InconsistentNaming
    /// <summary>
    /// A list of translation keys used to generate the MissingEntries_Edit entries of the mod files
    /// </summary>
    public Dictionary<string, string>? MissingEntries_Edit { get; set; } = [];
    // ReSharper disable once PropertyCanBeMadeInitOnly.Global
    // ReSharper disable once CollectionNeverUpdated.Global
    // ReSharper disable once MemberCanBePrivate.Global
    /// <summary>
    /// A list of translation keys used to generate the ResponseTexts entries of the mod files
    /// </summary>
    public Dictionary<string, string>? ResponseTexts { get; set; } = [];
    // ReSharper disable once PropertyCanBeMadeInitOnly.Global
    // ReSharper disable once CollectionNeverUpdated.Global
    // ReSharper disable once MemberCanBePrivate.Global
    // ReSharper disable once InconsistentNaming
    /// <summary>
    /// A list of translation keys used to generate the ResponseTexts_Edit entries of the mod files
    /// </summary>
    public Dictionary<string, string>? ResponseTexts_Edit { get; set; } = [];
    // ReSharper disable once PropertyCanBeMadeInitOnly.Global
    // ReSharper disable once CollectionNeverUpdated.Global
    // ReSharper disable once MemberCanBePrivate.Global
    /// <summary>
    /// A list of translation keys used to generate the ScriptContent entries of the mod files
    /// </summary>
    public Dictionary<string, string>? ScriptContent { get; set; } = [];
    // ReSharper disable once PropertyCanBeMadeInitOnly.Global
    // ReSharper disable once CollectionNeverUpdated.Global
    // ReSharper disable once MemberCanBePrivate.Global
    // ReSharper disable once InconsistentNaming
    /// <summary>
    /// A list of translation keys used to generate the ScriptContent_Edit entries of the mod files
    /// </summary>
    public Dictionary<string, string>? ScriptContent_Edit { get; set; } = [];
    // ReSharper disable once PropertyCanBeMadeInitOnly.Global
    // ReSharper disable once CollectionNeverUpdated.Global
    // ReSharper disable once MemberCanBePrivate.Global
    /// <summary>
    /// Localization entries
    /// </summary>
    public List<LocalizationEntry>? Localization { get; set; } = [];

    /// <summary>
    /// Indexer used to access sections by their name
    /// </summary>
    /// <param name="section">the name of the section</param>
    /// <exception cref="InvalidOperationException">section doesn't exist</exception>
    /// <returns>the section if found; throw an exception otherwise</returns>
    public Dictionary<string, string>? this[string section]
    {
        get
        {
            return section switch
            {
                MagicLoaderSectionsEnum.AltarDynamicTexts_Edit => AltarDynamicTexts_Edit,
                MagicLoaderSectionsEnum.HardcodedContent_Edit => HardcodedContent_Edit,
                MagicLoaderSectionsEnum.AltarStaticTexts_Edit => AltarStaticTexts_Edit,
                MagicLoaderSectionsEnum.MissingEntries_Edit => MissingEntries_Edit,
                MagicLoaderSectionsEnum.ResponseTexts_Edit => ResponseTexts_Edit,
                MagicLoaderSectionsEnum.ScriptContent_Edit => ScriptContent_Edit,
                MagicLoaderSectionsEnum.AltarDynamicTexts => AltarDynamicTexts,
                MagicLoaderSectionsEnum.Descriptions_Edit => Descriptions_Edit,
                MagicLoaderSectionsEnum.BookContent_Edit => BookContent_Edit,
                MagicLoaderSectionsEnum.HardcodedContent => HardcodedContent,
                MagicLoaderSectionsEnum.AltarStaticTexts => AltarStaticTexts,
                MagicLoaderSectionsEnum.LogEntries_Edit => LogEntries_Edit,
                MagicLoaderSectionsEnum.FullNames_Edit => FullNames_Edit,
                MagicLoaderSectionsEnum.MissingEntries => MissingEntries,
                MagicLoaderSectionsEnum.ResponseTexts => ResponseTexts,
                MagicLoaderSectionsEnum.ScriptContent => ScriptContent,
                MagicLoaderSectionsEnum.Descriptions => Descriptions,
                MagicLoaderSectionsEnum.BookContent => BookContent,
                MagicLoaderSectionsEnum.NewStrings => NewStrings,
                MagicLoaderSectionsEnum.LogEntries => LogEntries,
                MagicLoaderSectionsEnum.FullNames => FullNames,
                _ => throw new InvalidOperationException("Invalid section")
            };
        }
    }
}
