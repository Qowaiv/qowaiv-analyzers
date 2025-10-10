namespace Rules.Decorate_pure_functions;

public class Verify
{
    [Test]
    public void CSharp() => new DecoratePureFunctions()
        .ForCS()
        .AddSource(@"Cases/DecoratePureFunctions.cs")
        .AddReference<AwesomeAssertions.CustomAssertionAttribute>()
        .AddReference<System.Diagnostics.CodeAnalysis.DoesNotReturnAttribute>()
        .Verify();
}
