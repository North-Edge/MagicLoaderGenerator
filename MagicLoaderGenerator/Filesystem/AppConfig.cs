using MagicLoaderGenerator.Filesystem.Abstractions;
using Microsoft.Extensions.Configuration;

namespace MagicLoaderGenerator.Filesystem;

// ReSharper disable ReplaceAutoPropertyWithComputedProperty
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable once ClassNeverInstantiated.Global
// ReSharper disable once CollectionNeverUpdated.Global
// ReSharper disable MemberCanBePrivate.Global
/// <summary>
/// A record holding the parameters of the application
/// </summary>
public record AppConfig: IModConfiguration
{
    /// <summary>
    /// Default mod structure
    /// </summary>
    public const string DefaultModStructure = @"OblivionRemastered\Content\Dev\ObvData\Data\MagicLoader\";
    /// <summary>
    /// Default output directory
    /// </summary>
    public const string DefaultOutputDirectory = "MagicLoader";
    /// <summary>
    /// Default mod name
    /// </summary>
    public const string DefaultModName = "MagicLoaderMod";
    /// <summary>
    /// Default input directory
    /// </summary>
    public const string DefaultInputDirectory = "Input";

    // ReSharper disable once MemberCanBeProtected.Global
    /// <summary>
    /// Constructor used to bind the configuration to the record
    /// </summary>
    /// <param name="config">the application configuration (see <see cref="ConfigurationBuilder"/>)</param>
    public AppConfig(IConfiguration config)
    {
        config.Bind(this);

        // attempt to import files from the Input directory
        if (Directory.Exists(InputDirectory))
        {
            foreach (var file in Directory.EnumerateFiles(InputDirectory))
            {
                if (file.EndsWith(".json"))
                {
                    // add new mod entry for each JSON file found in the folder
                    ModFiles.TryAdd(Path.GetFileName(file), new ModFile { InputFile = file });
                }
            }
        }
    }

#region IModStructureConfiguration implementation
    /// <inheritdoc/>
    public Dictionary<string, ModFile> ModFiles { get; init; } = [];
    /// <inheritdoc/>
    public string ModName { get; init; } = DefaultModName;
#endregion

#region IFilesystemConfiguration implementation
    /// <inheritdoc/>
    public string ModDirectoryStructure { get; init; } = DefaultModStructure;
    /// <inheritdoc/>
    public string OutputDirectory { get; init; } = DefaultOutputDirectory;
    /// <inheritdoc/>
    public string InputDirectory { get; init; } = DefaultInputDirectory;
#endregion
}
