using MagicLoaderGenerator.Filesystem.Abstractions;
using System.IO.Compression;
using System.Text;

namespace MagicLoaderGenerator.Filesystem.Generators;

// ReSharper disable once UnusedType.Global
/// <summary>
/// Implementation of the <see cref="IModOutputGenerator"/> interface
/// used to generate ZIP archives containing the generated mod files
/// </summary>
/// <param name="configuration">an implementation of the <see cref="IModConfiguration"/> interface</param>
/// <param name="serializer">an implementation of the <see cref="IMagicLoaderFileSerializer"/> interface</param>
public class ZipOutputGenerator(IModConfiguration configuration, IMagicLoaderFileSerializer? serializer = null)
    : FolderOutputGenerator(configuration, serializer)
{
    public override void Output(string outputName)
    {
        var zipContent = new MemoryStream();
        var modStructure = Configuration.ModDirectoryStructure;
        // create the ZIP archive in memory
        using var archive = new ZipArchive(zipContent, ZipArchiveMode.Create);

        // create the output directory, if necessary
        if (Directory.Exists(OutputDirectory) == false)
        {
            Directory.CreateDirectory(OutputDirectory);
        }
        // if the mod contains more than one file, group them in a directory matching the mod name
        if (Configuration.ModFiles.Count > 1)
        {
            modStructure += Configuration.ModName;
        }
        // process all the files that were added to the generator
        foreach (var file in Files)
        {
            // serialize the file and transform the result into a byte array
            var content = Encoding.UTF8.GetBytes(Serializer.Serialize(file.Value));
            var filename = Path.Combine(modStructure, Serializer.Filename(file.Key));
            // add the file to the ZIP archive
            AddEntry(filename, content, archive);
        }
        // write the ZIP archive to the filesystem
        File.WriteAllBytes(Path.Combine(OutputDirectory, $"{outputName}.zip"), zipContent.ToArray());
        // clear the files added to the generator
        Files.Clear();
    }

    /// <summary>
    /// Adds a buffer to the ZIP archive
    /// </summary>
    /// <param name="filename">the name of the file within the archive</param>
    /// <param name="fileContent">the content of the file</param>
    /// <param name="archive">the ZIP archive</param>
    private static void AddEntry(string filename, byte[] fileContent, ZipArchive archive)
    {
        // add an entry to the ZIP archive for the file
        var entry = archive.CreateEntry(filename);
        using var stream = entry.Open();
        // write the file content to the ZIP archive
        stream.Write(fileContent, 0, fileContent.Length);
    }
}
