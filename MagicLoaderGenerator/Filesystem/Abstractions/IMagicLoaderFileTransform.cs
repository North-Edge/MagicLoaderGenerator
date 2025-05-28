using MagicLoaderGenerator.Localization.Transforms;

namespace MagicLoaderGenerator.Filesystem.Abstractions;

/// <summary>
/// Interface used for file transforms
/// <para>
///     Implementations:<br/>
///     - <see cref="BaseMagicLoaderFileTransform"/><br/>
///     - <see cref="ValueSuffixTransform"/><br/>
/// </para>
/// </summary>
public interface IMagicLoaderFileTransform
{
    /// <summary>
    /// Applies a transformation to the entries of the specified <see cref="ModFile"/>
    /// </summary>
    /// <param name="modFile">the file to be transformed</param>
    /// <returns>the transformed file</returns>
    MagicLoaderFile Transform(ModFile modFile);
}
