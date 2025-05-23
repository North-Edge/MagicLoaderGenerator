using MagicLoaderGenerator.Localization.Abstractions;
using MagicLoaderGenerator.Filesystem.Abstractions;
using Microsoft.Extensions.Configuration;

namespace MagicLoaderGenerator.Filesystem;

// ReSharper disable once ClassWithVirtualMembersNeverInherited.Global
// ReSharper disable ReplaceAutoPropertyWithComputedProperty
// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global
// ReSharper disable once ClassNeverInstantiated.Global
/// <summary>
/// A record holding the parameters of the application
/// </summary>
public record AppConfig: ILocalizationConfiguration, IModConfiguration
{
    /// <summary>
    /// Constructor used to bind the configuration to the record
    /// </summary>
    /// <param name="config">the application configuration (see <see cref="ConfigurationBuilder"/>)</param>
    public AppConfig(IConfiguration config)
    {
        config.Bind(this);
    }

#region IModStructureConfiguration implementation
    /// <inheritdoc/>
    public virtual Dictionary<string, MagicLoaderFile> ModFiles { get; init; } = new();
    /// <inheritdoc/>
    public virtual string ModName { get; init; } = "MagicLoaderMod";
#endregion

#region IFilesystemConfiguration implementation
    /// <inheritdoc/>
    public virtual string ModDirectoryStructure { get; init; }
        = @"OblivionRemastered\Content\Dev\ObvData\Data\MagicLoader\";
    /// <inheritdoc/>
    public virtual string OutputDirectory { get; init;  } = "MagicLoader";
#endregion

#region ILocalizationConfiguration implementation
    /// <inheritdoc/>
    public virtual List<string>? Languages { get; init; } = null;
    /// <inheritdoc/>
    public virtual List<string>? IncludedSections { get; init; } = [ "ST_FullNames" ];
    /// <inheritdoc/>
    public virtual string? LocalizationSource { get; init; } = "Localization";
#endregion
}
