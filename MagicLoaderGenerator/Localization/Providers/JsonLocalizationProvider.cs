using MagicLoaderGenerator.Localization.Abstractions;
using Microsoft.Extensions.Configuration;

namespace MagicLoaderGenerator.Localization.Providers;

// ReSharper disable once UnusedType.Global
/// <summary>
/// Implementation of the <see cref="ILocalizationProvider"/> interface
/// used to load the translation data from exported game JSON files
/// </summary>
/// <param name="configuration">the localization configuration</param>
public class JsonLocalizationProvider(ILocalizationConfiguration configuration)
    : BaseLocalizationProvider(configuration)
{
    /// <summary>
    /// The name of the translation files as extracted by FModel
    /// </summary>
    private const string DefaultFilename = "Game.json";

    /// <inheritdoc/>
    protected override Dictionary<string, string>? LoadLocalization(string language, List<string>? sections = null)
    {
        // check if the translation data is already loaded
        if (LocalizationStrings.TryGetValue(language, out var result))
        {
            return result;
        }

        // check if the specified language is supported and the corresponding folder exists
        if (SupportedLanguages.Contains(language) && Directory.Exists($"{LocalizationSource}/{language}"))
        {
            // load the JSON file using the .NET ConfigurationBuilder
            var jsonFile = $"{LocalizationSource}/{language}/{DefaultFilename}";
            var builder = new ConfigurationBuilder().AddJsonFile(jsonFile);
            var localizationFile = new LocalizationFile();
            var localizationConfig = builder.Build();

            localizationConfig.Bind(localizationFile);
            // transform the loaded sections of the JSON File into a dictionary
            LocalizationStrings[language] = result = localizationFile.ToDictionary(sections);
        }

        return result;
    }
}
