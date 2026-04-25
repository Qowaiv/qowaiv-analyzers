namespace Qowaiv.CodeAnalysis.Diagnostics;

/// <summary>A container/wrapper around a <see cref="DiagnosticDescriptor"/>.</summary>
/// <remarks>
/// As a <see cref="DiagnosticDescriptor"/> is sealed, we need some composition
/// to extend it.
/// </remarks>
public sealed class DescriptorContainer
{
    /// <summary>Indicates of the rule should analyze test code.</summary>
    public bool AnalyzeTestCode { get; set; }

    /// <summary>The descriptor of a <see cref="CodingRule"/>.</summary>
    public DiagnosticDescriptor Descriptor { get; set; } = null!;

    /// <inheritdoc cref="DiagnosticDescriptor.Id" />
    public string Id => Descriptor.Id;

    /// <inheritdoc cref="DiagnosticDescriptor.Category" />
    public string Category => Descriptor.Category;

    /// <inheritdoc cref="DiagnosticDescriptor.HelpLinkUri" />
    public string HelpLinkUri => Descriptor.HelpLinkUri;

    public static implicit operator DiagnosticDescriptor(DescriptorContainer container) => container.Descriptor;
}
