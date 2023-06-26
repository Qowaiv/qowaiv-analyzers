using Microsoft.CodeAnalysis.CSharp;

namespace Rules.Use_file_scoped_namespace_declarations_specs;

public class Not_before
{
    [TestCase(LanguageVersion.CSharp7)]
    [TestCase(LanguageVersion.CSharp8)]
    [TestCase(LanguageVersion.CSharp9)]
    public void CSharp_10(LanguageVersion version)
       => new UseFileScopedNamespaceDeclarations()
        .ForCS()
        .AddSnippet("namespace NamespaceScope { }")
        .WithLanguageVersion(version)
        .Verify();
}

public class Verify
{
    [Test]
    public void File_scoped_is_compliant()
         => new UseFileScopedNamespaceDeclarations()
        .ForCS()
        .AddSnippet("namespace NamespaceScope;")
        .WithLanguageVersion(LanguageVersion.CSharp10)
        .Verify();

    [Test]
    public void Classic_scope_is_noncompliant()
         => new UseFileScopedNamespaceDeclarations()
        .ForCS()
        .AddSnippet(@"
namespace NamespaceScope { } // Noncompliant {{Use a file-scoped namespace declaration instead.}}
//        ^^^^^^^^^^^^^^")
        .WithLanguageVersion(LanguageVersion.CSharp10)
        .Verify();

    [Test]
    public void files_with_multiple_declarations_are_ignored()
        => new UseFileScopedNamespaceDeclarations()
       .ForCS()
       .AddSnippet(@"
namespace NamespaceScope { }
namespace Other { }")
       .WithLanguageVersion(LanguageVersion.CSharp10)
       .Verify();

    [Test]
    public void files_with_nested_declarations_are_ignored()
       => new UseFileScopedNamespaceDeclarations()
      .ForCS()
      .AddSnippet(@"
namespace NamespaceScope
{
    namespace ChildScope { }
}
")
      .WithLanguageVersion(LanguageVersion.CSharp10)
      .Verify();
}


