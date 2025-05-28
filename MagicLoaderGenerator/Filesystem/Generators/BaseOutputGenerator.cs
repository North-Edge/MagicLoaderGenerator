using MagicLoaderGenerator.Filesystem.Abstractions;
using MagicLoaderGenerator.Filesystem.Transforms;
using Microsoft.Extensions.Logging;

namespace MagicLoaderGenerator.Filesystem.Generators;

/// <summary>
/// Base implementation of the <see cref="IModOutputGenerator"/> interface
/// </summary>
/// <param name="configuration">an implementation of the <see cref="IModConfiguration"/> interface</param>
/// <param name="serializer">an implementation of the <see cref="IMagicLoaderFileSerializer"/> interface</param>
public abstract class BaseOutputGenerator(IModConfiguration configuration,
                                          IMagicLoaderFileSerializer? serializer = null)
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
    public abstract void AddFile(string filename, MagicLoaderFile file);

    /// <inheritdoc/>
    public abstract void Output(string outputName, ILogger? logger = null);
}
