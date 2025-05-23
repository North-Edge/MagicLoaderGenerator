namespace MagicLoaderGenerator.Filesystem.Abstractions;

/// <summary>
/// Interface used for file serializers
/// <para>
///     Implementations:<br/>
///     - <see cref="Filesystem.Transforms.JsonFileSerializer"/><br/>
/// </para>
/// </summary>
public interface IMagicLoaderFileSerializer
{
    /// <summary>
    /// Generates the final filename used when generating the mod files
    /// </summary>
    /// <param name="baseName">
    ///   The base filename of the mod file from the configuration file
    ///   (see <see cref="IModConfiguration.ModFiles"/>)
    /// </param>
    /// <returns>the filename used in the output</returns>
    string Filename(string baseName);

    /// <summary>
    /// Serializes a <see cref="MagicLoaderFile"/> as a string
    /// </summary>
    /// <param name="file">the file to serialize</param>
    /// <returns>the serialized file</returns>
    string Serialize(MagicLoaderFile file);
}
