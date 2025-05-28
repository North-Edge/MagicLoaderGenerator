namespace MagicLoaderGenerator.Localization.Transforms;

// ReSharper disable once UnusedType.Global
/// <summary>
/// Specialization of the <see cref="BaseMagicLoaderFileTransform"/> implementation used to add a suffix to the translated entry
/// </summary>
/// <param name="suffix">the suffix to add to the translated entries</param>
public class ValueSuffixTransform(string suffix): BaseMagicLoaderFileTransform
{
    /// <inheritdoc/>
    protected override string FormatLine(string key)
    {
        // translate the entry then add the suffix to it
        return $"{base.FormatLine(key)} {suffix}";
    }
}
