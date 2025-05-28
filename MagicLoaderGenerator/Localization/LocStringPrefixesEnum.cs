namespace MagicLoaderGenerator.Localization;

/// <summary>
/// Prefix of localization strings found in the game localization files
/// </summary>
public static class LocStringPrefixesEnum
{
    public static readonly List<string> Values = [
        FullNames,
        ResponseTexts,
        ScriptContent,
        BookContent,
        LogEntries,
        HardcodedContent,
        AltarDynamicTexts,
        Descriptions,
        AltarStaticTexts,
        MissingEntries
    ];

    /// <summary>
    /// FullNames entries (key prefix: LOC_FN_)
    /// </summary>
    public const string FullNames = "LOC_FN_";
    /// <summary>
    /// Response texts entries (key prefix: LOC_RT_)
    /// </summary>
    public const string ResponseTexts = "LOC_RT_";
    /// <summary>
    /// Script content entries (key prefix: LOC_SC_)
    /// </summary>
    public const string ScriptContent = "LOC_SC_";
    /// <summary>
    /// Book content entries (key prefix: LOC_BK_)
    /// </summary>
    public const string BookContent = "LOC_BK_";
    /// <summary>
    /// Quest log entries (key prefix: LOC_LE_)
    /// </summary>
    public const string LogEntries = "LOC_LE_";
    /// <summary>
    /// Hardcoded content entries (key prefix: LOC_HC_)
    /// </summary>
    public const string HardcodedContent = "LOC_HC_";
    /// <summary>
    /// Dynamic texts entries (key prefix: LOC_AD_)
    /// </summary>
    public const string AltarDynamicTexts = "LOC_AD_";
    /// <summary>
    /// Descriptions entries (key prefix: LOC_DE_)
    /// </summary>
    public const string Descriptions = "LOC_DE_";
    /// <summary>
    /// Static texts entries (key prefix: LOC_AS_)
    /// </summary>
    public const string AltarStaticTexts = "LOC_AS_";
    /// <summary>
    /// Missing entries (key prefix: LOC_ME_)
    /// </summary>
    public const string MissingEntries = "LOC_ME_";
}
