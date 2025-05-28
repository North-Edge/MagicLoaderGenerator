namespace MagicLoaderGenerator.Localization;

// ReSharper disable once UnusedType.Global
// ReSharper disable UnusedMember.Global
// ReSharper disable InconsistentNaming
/// <summary>
/// Record used to map translation files extracted using FModel
/// </summary>
public record LocalizationFile
{
    /// <summary>
    /// FullNames entries (key prefix: LOC_FN_)
    /// </summary>
    public Dictionary<string, string>? ST_FullNames { get; set; } = null;
    /// <summary>
    /// Response texts entries (key prefix: LOC_RT_)
    /// </summary>
    public Dictionary<string, string>? ST_ResponseTexts { get; set; } = null;
    /// <summary>
    /// Script content entries (key prefix: LOC_SC_)
    /// </summary>
    public Dictionary<string, string>? ST_ScriptContent { get; set; } = null;
    /// <summary>
    /// Book content entries (key prefix: LOC_BK_)
    /// </summary>
    public Dictionary<string, string>? ST_BookContent { get; set; } = null;
    /// <summary>
    /// Quest log entries (key prefix: LOC_LE_)
    /// </summary>
    public Dictionary<string, string>? ST_LogEntries { get; set; } = null;
    /// <summary>
    /// Hardcoded content entries (key prefix: LOC_HC_)
    /// </summary>
    public Dictionary<string, string>? ST_HardcodedContent { get; set; } = null;
    /// <summary>
    /// Dynamic texts entries (key prefix: LOC_AD_)
    /// </summary>
    public Dictionary<string, string>? ST_AltarDynamicTexts { get; set; } = null;
    /// <summary>
    /// Descriptions entries (key prefix: LOC_DE_)
    /// </summary>
    public Dictionary<string, string>? ST_Descriptions { get; set; } = null;
    /// <summary>
    /// Static texts entries (key prefix: LOC_AS_)
    /// </summary>
    public Dictionary<string, string>? ST_AltarStaticTexts { get; set; } = null;
    /// <summary>
    /// Missing entries (key prefix: LOC_ME_)
    /// </summary>
    public Dictionary<string, string>? ST_MissingEntries { get; set; } = null;
    /// <summary>
    /// A list of all the section names generated using reflection
    /// </summary>
    private static List<string> Sections 
        => typeof(LocalizationFile).GetProperties().Where(p => p.PropertyType == typeof(Dictionary<string, string>))
                                                   .Select(p => p.Name).ToList();

    /// <summary>
    /// Groups all the loaded sections into one dictionary
    /// </summary>
    /// <param name="include">an optional list of sections to include</param>
    /// <returns>the loaded translations as a dictionary</returns>
    public Dictionary<string, string> ToDictionary(List<string>? include = null)
    {
        var dictionaries = Sections.Select(prop => Include(prop, include ?? Sections))
                                   .OfType<Dictionary<string, string>>()
                                   .ToList();

        return dictionaries.SelectMany(dict => dict).DistinctBy(kvp => kvp.Key).ToDictionary();
    }

    /// <summary>
    /// Retrieves the value of a section given its name using reflection
    /// </summary>
    /// <param name="name">the name of the section</param>
    /// <param name="inclusionList">the list of sections to include</param>
    /// <returns></returns>
    private Dictionary<string, string>? Include(string name, List<string> inclusionList)
    {
        var prop = typeof(LocalizationFile).GetProperty(name);

        // check if the section is in the inclusion list and retrieve the dictionary using reflection
        return prop?.PropertyType == typeof(Dictionary<string,string>) && inclusionList.Contains(name)
             ? prop.GetValue(this, null) as Dictionary<string,string> : null;
    }
}
