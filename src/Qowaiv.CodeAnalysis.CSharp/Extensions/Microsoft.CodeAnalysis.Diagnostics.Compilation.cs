namespace Microsoft.CodeAnalysis.Diagnostics;

/// <summary>Extensions on <see cref="Compilation"/>.</summary>
internal static class CompilationExtensions
{
    public static LanguageVersion LanguageVersion(this Compilation compilation)
        => compilation is CSharpCompilation cs ? cs.LanguageVersion : default;
}
