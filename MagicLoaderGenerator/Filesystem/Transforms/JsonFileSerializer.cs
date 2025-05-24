using MagicLoaderGenerator.Filesystem.Abstractions;
using System.Text.Json.Serialization;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace MagicLoaderGenerator.Filesystem.Transforms;

/// <summary>
/// Implementation of the <see cref="IMagicLoaderFileSerializer"/>
/// used to serialize <see cref="ModFile"/> objects as JSON
/// </summary>
/// <param name="jsonSerializerOptions">Optional JSON serialization options</param>
public class JsonFileSerializer(JsonSerializerOptions? jsonSerializerOptions = null): IMagicLoaderFileSerializer
{
    /// <summary>
    /// The default JSON serialization options
    /// </summary>
    private readonly JsonSerializerOptions _defaultJsonOptions = new() {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull, // do not write empty properties
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,        // do not escape Unicode sequences
        WriteIndented = true                                          // format the output (prettify)
    };
    /// <summary>
    /// The file extension used for JSON files
    /// </summary>
    private const string FileExtension = ".json";

    /// <inheritdoc/>
    public string Filename(string baseName)
    {
        // suffix the filename with the JSON extension if necessary
        if (baseName.EndsWith(FileExtension) == false)
        {
            baseName += FileExtension;
        }

        return baseName;
    }

    /// <inheritdoc />
    public string Serialize(MagicLoaderFile file)
    {
        // serialize the file using the built-in JSON serializer
        return JsonSerializer.Serialize(file, jsonSerializerOptions ?? _defaultJsonOptions);
    }
}
