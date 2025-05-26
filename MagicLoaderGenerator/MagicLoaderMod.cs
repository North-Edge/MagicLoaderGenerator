using MagicLoaderGenerator.Localization.Abstractions;
using MagicLoaderGenerator.Filesystem.Abstractions;
using MagicLoaderGenerator.Localization.Transforms;
using MagicLoaderGenerator.Filesystem;
using Microsoft.Extensions.Logging;

namespace MagicLoaderGenerator;

using FileTransforms = Dictionary<string, IMagicLoaderFileTransform?>;

// ReSharper disable once UnusedType.Global
/// <summary>
/// Default implementation for a simple mod to generate translation files
/// </summary>
/// <param name="localization">an implementation of the <see cref="ILocalizationProvider"/> interface</param>
/// <param name="config">the application configuration</param>
/// <param name="logger">Optional logger</param>
public class MagicLoaderMod(ILocalizationProvider localization, AppConfig config, ILogger? logger = null)
{
    // ReSharper disable once UnusedMember.Global
    /// <summary>
    /// Generate the translation files for the mod using the specified generator
    /// </summary>
    /// <param name="outputGenerator">an implementation of the <see cref="IModOutputGenerator"/> interface</param>
    /// <param name="fileTransform">
    /// an optional implementation of the <see cref="IMagicLoaderFileTransform"/> interface
    /// (the <see cref="TranslateFileTransform"/> implementation is used by default)
    /// </param>
    /// <param name="cleanOutput">flag specifying if the output directory should be clean before generating files</param>
    /// <returns>the output directory</returns>
    public string Generate(IModOutputGenerator outputGenerator, IMagicLoaderFileTransform? fileTransform = null, bool cleanOutput = true)
    {
        return Generate(outputGenerator, new FileTransforms {{ "", fileTransform }}, cleanOutput);
    }

    // ReSharper disable once MemberCanBePrivate.Global
    /// <summary>
    /// Generate the translation files for the mod using the specified generator
    /// </summary>
    /// <param name="outputGenerator">an implementation of the <see cref="IModOutputGenerator"/> interface</param>
    /// <param name="fileTransforms">
    /// a set of implementations of the <see cref="IMagicLoaderFileTransform"/> interface each representing a variant
    /// (the <see cref="TranslateFileTransform"/> implementation is used by default)
    /// </param>
    /// <param name="cleanOutput">flag specifying if the output directory should be clean before generating files</param>
    /// <returns>the output directory</returns>
    public string Generate(IModOutputGenerator outputGenerator, FileTransforms fileTransforms, bool cleanOutput = true)
    {
        try
        {
            var outputDir = Path.Combine(config.OutputDirectory, config.ModName);
            var transformCount = fileTransforms.Count;

            logger?.LogInformation("Generating mod `{modName}`", config.ModName);
            logger?.LogInformation("Output directory: `{outputDir}`", outputDir);

            // clean the output directory, if necessary
            if (cleanOutput && Directory.Exists(outputDir))
            {
                logger?.LogDebug("Cleaning output directory `{outputDir}`", outputDir);
                Directory.Delete(outputDir, true);
            }

            // process all the supported languages
            foreach (var language in localization.SupportedLanguages)
            {
                var singleVariant = transformCount == 1;

                logger?.LogInformation("Mod variant(s): {variants}", transformCount > 0
                    ? "None"
                    : string.Join(", ", fileTransforms.Keys));

                foreach (var (variant, fileTransform) in fileTransforms)
                {
                    // create the default translation transform, if necessary
                    var transform = fileTransform ?? new TranslateFileTransform(localization);
                    var outputName = $"{transform.GetOutputName(config.ModName, language)}";

                    logger?.LogInformation("Generating{variant} {outputName} using {type}",
                        string.IsNullOrEmpty(variant) ? "" : $" variant `{variant}` of",
                        outputName, transform.GetType().Name);

                    if (singleVariant == false && string.IsNullOrEmpty(variant) == false)
                    {
                        outputName += $"_{variant}";
                    }

                    // process all the file entries from the configuration that contain at least one entry
                    foreach (var (filename, modFile) in config.ModFiles.Where(f => f.Value.HasEntries()))
                    {
                        logger?.LogInformation("Transforming `{filename}` for language `{language}`", filename,
                            language);
                        // transform the current file
                        var transformedFile = transform.Transform(language, modFile);
                        logger?.LogDebug("Adding {filename} for language `{language}` to the output", filename,
                            language);
                        // add the file to the output generator
                        outputGenerator.AddFile(filename, transformedFile);
                    }

                    logger?.LogInformation("Generating output for`{filename}`", outputName);
                    // generate the output
                    outputGenerator.Output(outputName, logger);
                }
            }

            return outputDir;
        }
        catch (Exception ex)
        {
            logger?.LogError(ex, "Generating mod `{modName}` failed:\n", config.ModName);
            throw;
        }
    }
}
