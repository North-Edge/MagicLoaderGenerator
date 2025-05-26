using MagicLoaderGenerator.Filesystem.Abstractions;
using MagicLoaderGenerator.Filesystem.Transforms;
using Microsoft.Extensions.Logging;

namespace MagicLoaderGenerator.Filesystem.Generators;

/// <summary>
/// Implementation of the <see cref="IModOutputGenerator"/> interface
/// used to generate files in an uncompressed directory tree
/// </summary>
/// <param name="configuration">an implementation of the <see cref="IModConfiguration"/> interface</param>
/// <param name="serializer">an implementation of the <see cref="IMagicLoaderFileSerializer"/> interface</param>
public class FolderOutputGenerator(IModConfiguration configuration, IMagicLoaderFileSerializer? serializer = null)
    : IModOutputGenerator
{
    /// <summary>
    /// The files added to the generator
    /// </summary>
    protected readonly Dictionary<string, MagicLoaderFile> Files = new();
    /// <summary>
    /// The mod configuration used by the generator
    /// </summary>
    protected readonly IModConfiguration Configuration = configuration;
    /// <summary>
    /// The base output directory of the generator
    /// </summary>
    protected string OutputDirectory { get; } = Path.Combine(configuration.OutputDirectory, configuration.ModName);
    /// <summary>
    /// The <see cref="IMagicLoaderFileSerializer"/> implementation to use to serialize the file
    /// (defaults to <see cref="JsonFileSerializer"/> if none were passed to the constructor)
    /// </summary>
    protected readonly IMagicLoaderFileSerializer Serializer = serializer ?? new JsonFileSerializer();

    /// <inheritdoc/>
    public virtual void AddFile(string filename, MagicLoaderFile file)
    {
        Files.TryAdd(filename, file);
    }

    /// <inheritdoc/>
    public virtual void Output(string outputName, ILogger? logger = null)
    {
        // the generator will write the mod files in a directory structure dictated by the mod structure
        var outputDir = Path.Combine(OutputDirectory, outputName, Configuration.ModDirectoryStructure);

        // if the mod contains more than one file, group them in a directory matching the mod name
        if (Configuration.ModFiles.Count > 1)
        {
            outputDir += Configuration.ModName;
        }

        logger?.LogInformation("Generating output to `{outputDir}`", outputDir);
        // create the output directory, if necessary
        if (Directory.Exists(outputDir) == false)
        {
            logger?.LogDebug("Creating output directory `{outputDir}`", outputDir);
            Directory.CreateDirectory(outputDir);
        }
        // process all the files that were added to the generator
        foreach (var file in Files)
        {
            // serialize the file using the specified generator or the default JSON implementation
            var content = Serializer.Serialize(file.Value);
            var filename = Serializer.Filename(file.Key);

            logger?.LogDebug("Serialized the content of `{file}`", filename);

            // write the file to the filesystem
            File.WriteAllText(Path.Combine(outputDir, filename), content);
            logger?.LogInformation("Written the content of `{file}`", filename);
        }
        // clear the files added to the generator
        Files.Clear();
    }
}
