namespace MagicLoaderGenerator.Localization;

/// <summary>
/// MagicLoader localization entry
/// </summary>
public record LocalizationEntry
{
    /// <summary>
    /// The localization string key
    /// </summary>
    public required string Key { get; init; }
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    // ReSharper disable once InconsistentNaming
    /// <summary>
    /// Global edit for all languages
    /// </summary>
    public string? Global_Edit { get; set; }
    // ReSharper disable once UnusedMember.Global
    // ReSharper disable once InconsistentNaming
    /// <summary>
    /// German translation
    /// </summary>
    public string? DE { get; set; }
    /// <summary>
    /// German edit
    /// </summary>
    // ReSharper disable once UnusedMember.Global
    // ReSharper disable once InconsistentNaming
    public string? DE_Edit { get; set; }
    // ReSharper disable once UnusedMember.Global
    // ReSharper disable once InconsistentNaming
    /// <summary>
    /// Spanish translation
    /// </summary>
    public string? ES { get; set; }
    // ReSharper disable once UnusedMember.Global
    // ReSharper disable once InconsistentNaming
    /// <summary>
    /// Spanish edit
    /// </summary>
    public string? ES_Edit { get; set; }
    // ReSharper disable once UnusedMember.Global
    // ReSharper disable once InconsistentNaming
    /// <summary>
    /// English translation
    /// </summary>
    public string? EN { get; set; }
    // ReSharper disable once UnusedMember.Global
    // ReSharper disable once InconsistentNaming
    /// <summary>
    /// English edit
    /// </summary>
    public string? EN_Edit { get; set; }
    // ReSharper disable once UnusedMember.Global
    // ReSharper disable once InconsistentNaming
    /// <summary>
    /// French translation
    /// </summary>
    public string? FR { get; set; }
    // ReSharper disable once UnusedMember.Global
    // ReSharper disable once InconsistentNaming
    /// <summary>
    /// French edit
    /// </summary>
    public string? FR_Edit { get; set; }
    // ReSharper disable once UnusedMember.Global
    // ReSharper disable once InconsistentNaming
    /// <summary>
    /// Italian translation
    /// </summary>
    public string? IT { get; set; }
    // ReSharper disable once UnusedMember.Global
    // ReSharper disable once InconsistentNaming
    /// <summary>
    /// Italian edit
    /// </summary>
    public string? IT_Edit { get; set; }
    // ReSharper disable once UnusedMember.Global
    // ReSharper disable once InconsistentNaming
    /// <summary>
    /// Japanese translation
    /// </summary>
    public string? JA { get; set; }
    // ReSharper disable once UnusedMember.Global
    // ReSharper disable once InconsistentNaming
    /// <summary>
    /// Japanese edit
    /// </summary>
    public string? JA_Edit { get; set; }
    // ReSharper disable once UnusedMember.Global
    // ReSharper disable once InconsistentNaming
    /// <summary>
    /// Polish translation
    /// </summary>
    public string? PL { get; set; }
    // ReSharper disable once UnusedMember.Global
    // ReSharper disable once InconsistentNaming
    /// <summary>
    /// Polish edit
    /// </summary>
    public string? PL_Edit { get; set; }
    // ReSharper disable once UnusedMember.Global
    // ReSharper disable once InconsistentNaming
    /// <summary>
    /// Portuguese translation
    /// </summary>
    public string? PT { get; set; }
    // ReSharper disable once UnusedMember.Global
    // ReSharper disable once InconsistentNaming
    /// <summary>
    /// Portuguese edit
    /// </summary>
    public string? PT_Edit { get; set; }
    // ReSharper disable once UnusedMember.Global
    // ReSharper disable once InconsistentNaming
    /// <summary>
    /// Russian translation
    /// </summary>
    public string? RU { get; set; }
    // ReSharper disable once UnusedMember.Global
    // ReSharper disable once InconsistentNaming
    /// <summary>
    /// Russian edit
    /// </summary>
    public string? RU_Edit { get; set; }
    // ReSharper disable once UnusedMember.Global
    // ReSharper disable once InconsistentNaming
    /// <summary>
    /// Simplified chinese translation
    /// </summary>
    public string? ZH { get; set; }
    // ReSharper disable once UnusedMember.Global
    // ReSharper disable once InconsistentNaming
    /// <summary>
    /// Simplified chinese edit
    /// </summary>
    public string? ZH_Edit { get; set; }

    /// <summary>
    /// Checks if the localization entry has any localization data
    /// </summary>
    /// <returns><c>true</c> if the entry has any translation data; <c>false</c> otherwise</returns>
    public bool HasTranslation()
    {
        var properties = GetType().GetProperties();

        foreach (var property in properties)
        {
            if (property.Name != "Key" && property.GetValue(this) != null)
                return true;
        }

        return false;
    }
}
