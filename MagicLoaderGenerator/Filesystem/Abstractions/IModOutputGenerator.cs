namespace MagicLoaderGenerator.Filesystem.Abstractions;

/// <summary>
/// Interface used for output generators
/// <para>
///     Implementations:<br/>
///     - <see cref="Filesystem.Generators.FolderOutputGenerator"/><br/>
///     - <see cref="Filesystem.Generators.ZipOutputGenerator"/><br/>
/// </para>
/// </summary>
public interface IModOutputGenerator
{
    /// <summary>
    /// Adds a file to the output
    /// </summary>
    /// <param name="filename">the filename of the file</param>
    /// <param name="file">the <see cref="MagicLoaderFile"/> file to add</param>
    public void AddFile(string filename, MagicLoaderFile file);

    /// <summary>
    /// Outputs all the files added to the generator
    /// </summary>
    /// <param name="outputName">the name of the output (see <see cref="IMagicLoaderFileTransform.GetOutputName"/>)</param>
    public void Output(string outputName);
}
