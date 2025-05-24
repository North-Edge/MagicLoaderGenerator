namespace MagicLoaderGenerator.Filesystem;

/// <summary>
/// A record used to load the translation keys to be used to generate MagicLoader files or the path of an existing file.
/// </summary>
public sealed record ModFile
{
    // ReSharper disable once CollectionNeverUpdated.Global
    /// <summary>
    /// A list of translation keys used to generate the fullname edit entries of the mod files
    /// </summary>
    public List<string> FullNamesEditEntries { get; } = [];
    // ReSharper disable once CollectionNeverUpdated.Global
    /// <summary>
    /// A list of translation keys used to generate the fullname entries of the mod files
    /// </summary>
    public List<string> FullNameEntries { get; } = [];
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
               || FullNamesEditEntries.Count != 0
               || FullNameEntries.Count != 0;
    }
}
