﻿using MagicLoaderGenerator.Filesystem.Abstractions;

namespace MagicLoaderGenerator.Localization.Abstractions;

/// <summary>
/// Interface used to access the configuration parameters relating to the localization
/// </summary>
public interface ILocalizationConfiguration: IFileSystemConfiguration
{
    /// <summary>
    /// The languages used during the output generation
    /// </summary>
    public List<string>? Languages { get; }
    /// <summary>
    /// The sections of the game translation files to load
    /// </summary>
    public List<string>? IncludedSections { get; }
    /// <summary>
    /// A string defining the source of the localization data.
    /// Could be directory or connection string to a database, for instance.
    /// </summary>
    public string? LocalizationSource { get; }
}
