namespace Microsoft.CodeAnalysis.Diagnostics;

/// <summary>Extensions on <see cref="Compilation"/>.</summary>
internal static class CompilationExtensions
{
    extension(Compilation compilation)
    {
        public LanguageVersion CSharpVersion
            => compilation is CSharpCompilation cs ? cs.LanguageVersion : default;
    }
}
