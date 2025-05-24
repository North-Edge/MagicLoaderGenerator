using MagicLoaderGenerator.Localization.Transforms;

namespace MagicLoaderGenerator.Filesystem.Abstractions;

/// <summary>
/// Interface used for file transforms
/// <para>
///     Implementations:<br/>
///     - <see cref="TranslateFileTransform"/><br/>
///     - <see cref="ValueSuffixTransformer"/><br/>
/// </para>
/// </summary>
public interface IMagicLoaderFileTransform
{
    /// <summary>
    /// Applies a transformation to the entries of the specified <see cref="ModFile"/>
    /// </summary>
    /// <param name="language">the language used in the transformation</param>
    /// <param name="modFile">the file to be transformed</param>
    /// <returns>the transformed file</returns>
    MagicLoaderFile Transform(string language, ModFile modFile);

    /// <summary>
    /// Generates the name of the file output
    /// </summary>
    /// <param name="baseName">
    ///   The base filename of the mod file from the configuration file
    ///   (see <see cref="IModConfiguration.ModFiles"/>)
    /// </param>
    /// <param name="language">the language used in the transformation</param>
    /// <returns>the name of the file output</returns>
    string GetOutputName(string baseName, string language);
}
