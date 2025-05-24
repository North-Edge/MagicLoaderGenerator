namespace MagicLoaderGenerator.Filesystem.Abstractions;

/// <summary>
/// Interface used to access the configuration parameters relating to the filesystem
/// </summary>
public interface IFileSystemConfiguration
{
    /// <summary>
    /// The directory structure of the mod used when generating the mod files
    /// </summary>
    string ModDirectoryStructure { get; }
    /// <summary>
    /// The output directory used when generating the mod files
    /// </summary>
    string OutputDirectory { get; }
    // ReSharper disable once UnusedMemberInSuper.Global
    /// <summary>
    /// The input directory used to import mod files
    /// </summary>
    string InputDirectory { get; }
}
