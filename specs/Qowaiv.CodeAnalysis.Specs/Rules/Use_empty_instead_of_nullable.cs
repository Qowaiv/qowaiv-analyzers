using Microsoft.CodeAnalysis.CSharp;

namespace Rules.Use_empty_instead_of_nullable;

public class Verify
{
    [Test]
    public void File_scoped_is_compliant()
        => new UseEmptyInsteadOfNullable()
        .ForCS()
        .AddSource(@"Cases/UseEmptyInsteadOfNullable.cs")
        .AddReference<Qowaiv.Percentage>()
        .WithLanguageVersion(LanguageVersion.CSharp10)
        .Verify();
}
