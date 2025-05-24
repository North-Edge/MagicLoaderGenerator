namespace MagicLoaderGenerator.Filesystem.Abstractions;

/// <summary>
/// Interface used to access the configuration parameters relating to the mod file
/// </summary>
public interface IModConfiguration: IFileSystemConfiguration
{
    /// <summary>
    /// A mapping containing the filename of the mod files as key and
    /// the corresponding <see cref="MagicLoaderFile"/> object as value
    /// </summary>
    public Dictionary<string, ModFile> ModFiles { get; }
    /// <summary>
    /// The name of the mod
    /// </summary>
    public string ModName { get; }
}
