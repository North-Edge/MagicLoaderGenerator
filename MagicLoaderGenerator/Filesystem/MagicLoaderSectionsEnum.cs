namespace MagicLoaderGenerator.Filesystem;

/// <summary>
/// Name of the sections in the JSON format of MagicLoader
/// </summary>
public static class MagicLoaderSectionsEnum
{
    /// <summary>
    /// The sections of a MagicLoader
    /// </summary>
    public static readonly List<string> Sections = [
        FullNames,
        FullNames_Edit,
        ResponseTexts,
        ResponseTexts_Edit,
        ScriptContent,
        ScriptContent_Edit,
        BookContent,
        BookContent_Edit,
        LogEntries,
        LogEntries_Edit,
        HardcodedContent,
        HardcodedContent_Edit,
        AltarDynamicTexts,
        AltarDynamicTexts_Edit,
        Descriptions,
        Descriptions_Edit,
        AltarStaticTexts,
        AltarStaticTexts_Edit,
        MissingEntries,
        MissingEntries_Edit,
        NewStrings
    ];

    /// <summary>
    /// NewString entries
    /// </summary>
    public const string NewStrings = nameof(NewStrings);
    /// <summary>
    /// FullNames entries (key prefix: LOC_FN_)
    /// </summary>
    public const string FullNames = nameof(FullNames);
    // ReSharper disable once InconsistentNaming
    /// <summary>
    /// FullNames edit entries (key prefix: LOC_FN_)
    /// </summary>
    public const string FullNames_Edit = nameof(FullNames_Edit);
    /// <summary>
    /// Response texts entries (key prefix: LOC_RT_)
    /// </summary>
    public const string ResponseTexts = nameof(ResponseTexts);
    // ReSharper disable once InconsistentNaming
    /// <summary>
    /// Response texts edit entries (key prefix: LOC_RT_)
    /// </summary>
    public const string ResponseTexts_Edit = nameof(ResponseTexts_Edit);
    /// <summary>
    /// Script content entries (key prefix: LOC_SC_)
    /// </summary>
    public const string ScriptContent = nameof(ScriptContent);
    // ReSharper disable once InconsistentNaming
    /// <summary>
    /// Script content edit entries (key prefix: LOC_SC_)
    /// </summary>
    public const string ScriptContent_Edit = nameof(ScriptContent_Edit);
    /// <summary>
    /// Book content entries (key prefix: LOC_BK_)
    /// </summary>
    public const string BookContent = nameof(BookContent);
    // ReSharper disable once InconsistentNaming
    /// <summary>
    /// Book content edit entries (key prefix: LOC_BK_)
    /// </summary>
    public const string BookContent_Edit = nameof(BookContent_Edit);
    /// <summary>
    /// Quest log entries (key prefix: LOC_LE_)
    /// </summary>
    public const string LogEntries = nameof(LogEntries);
    // ReSharper disable once InconsistentNaming
    /// <summary>
    /// Quest log edit entries (key prefix: LOC_LE_)
    /// </summary>
    public const string LogEntries_Edit = nameof(LogEntries_Edit);
    /// <summary>
    /// Hardcoded content entries (key prefix: LOC_HC_)
    /// </summary>
    public const string HardcodedContent = nameof(HardcodedContent);
    // ReSharper disable once InconsistentNaming
    /// <summary>
    /// Hardcoded content edit entries (key prefix: LOC_HC_)
    /// </summary>
    public const string HardcodedContent_Edit = nameof(HardcodedContent_Edit);
    /// <summary>
    /// Dynamic texts entries (key prefix: LOC_AD_)
    /// </summary>
    public const string AltarDynamicTexts = nameof(AltarDynamicTexts);
    // ReSharper disable once InconsistentNaming
    /// <summary>
    /// Dynamic texts edit entries (key prefix: LOC_AD_)
    /// </summary>
    public const string AltarDynamicTexts_Edit = nameof(AltarDynamicTexts_Edit);
    /// <summary>
    /// Descriptions entries (key prefix: LOC_DE_)
    /// </summary>
    public const string Descriptions = nameof(Descriptions);
    // ReSharper disable once InconsistentNaming
    /// <summary>
    /// Descriptions edit entries (key prefix: LOC_DE_)
    /// </summary>
    public const string Descriptions_Edit = nameof(Descriptions_Edit);
    /// <summary>
    /// Static texts entries (key prefix: LOC_AS_)
    /// </summary>
    public const string AltarStaticTexts = nameof(AltarStaticTexts);
    // ReSharper disable once InconsistentNaming
    /// <summary>
    /// Static texts edit entries (key prefix: LOC_AS_)
    /// </summary>
    public const string AltarStaticTexts_Edit = nameof(AltarStaticTexts_Edit);
    /// <summary>
    /// Missing entries (key prefix: LOC_ME_)
    /// </summary>
    public const string MissingEntries = nameof(MissingEntries);
    // ReSharper disable once InconsistentNaming
    /// <summary>
    /// Missing edit entries (key prefix: LOC_ME_)
    /// </summary>
    public const string MissingEntries_Edit = nameof(MissingEntries_Edit);
}
