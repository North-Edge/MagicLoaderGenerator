namespace MagicLoaderGenerator.Filesystem;

/// <summary>
/// A record used to handle JSON (de)serialization of MagicLoader file
/// </summary>
public sealed record MagicLoaderFile
{
    // ReSharper disable once PropertyCanBeMadeInitOnly.Global
    // ReSharper disable once InconsistentNaming
    /// <summary>
    /// The fullname edit entries to write to the MagicLoader file
    /// </summary>
    public Dictionary<string, string>? FullNames_Edit { get; set; }

    /// <summary>
    /// The fullname entries to write to the MagicLoader file
    /// </summary>
    public Dictionary<string, string>? FullNames { get; set; }
}
