namespace Rules.Decorate_pure_functions;

public class Verify
{
    [Test]
    public void CSharp()
        => new DecoratePureFunctions()
        .ForCS()
        .AddSource(@"Cases/DecoratePureFunctions.cs")
        .AddReference<FluentAssertions.CustomAssertionAttribute>()
        .Verify();
}
