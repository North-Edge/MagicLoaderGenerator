using MagicLoaderGenerator.Localization.Abstractions;
using MagicLoaderGenerator.Filesystem.Abstractions;
using MagicLoaderGenerator.Localization.Transforms;
using MagicLoaderGenerator.Filesystem;

namespace MagicLoaderGenerator;

// ReSharper disable once UnusedType.Global
/// <summary>
/// Default implementation for a simple mod to generate translation files
/// </summary>
/// <param name="localization">an implementation of the <see cref="ILocalizationProvider"/> interface</param>
/// <param name="config">the application configuration</param>
public class MagicLoaderMod(ILocalizationProvider localization, AppConfig config)
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
    /// <returns></returns>
    public string Generate(IModOutputGenerator outputGenerator, IMagicLoaderFileTransform? fileTransform = null, bool cleanOutput = true)
    {
        var outputDir = Path.Combine(config.OutputDirectory, config.ModName);
        // create a translation transform, if necessary
        fileTransform ??= new TranslateFileTransform(localization);
        // clean the output directory, if necessary
        if (cleanOutput && Directory.Exists(outputDir))
        {
            Directory.Delete(outputDir, true);
        }
        // process all the supported languages
        foreach (var language in localization.SupportedLanguages)
        {
            // process all the file entries from the configuration that contain at least one entry
            foreach (var (filename, magicLoaderFile) in config.ModFiles.Where(f => f.Value.HasEntries()))
            {
                // transform the current file
                var transformedFile = fileTransform.Transform(language, magicLoaderFile);
                // add the file to the output generator
                outputGenerator.AddFile(filename, transformedFile);
            }
            // generate the output
            outputGenerator.Output(fileTransform.GetOutputName(config.ModName, language));
        }

        return outputDir;
    }
}
