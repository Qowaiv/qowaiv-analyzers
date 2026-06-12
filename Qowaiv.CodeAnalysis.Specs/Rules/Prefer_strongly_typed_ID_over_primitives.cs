namespace Rules.Prefer_strongly_typed_ID_over_primitives;

public class Verify
{
    [Test]
    public void Rule() => new PreferStronglyTypedIdOverPrimitives()
        .ForCS()
        .AddSource(@"Cases/PreferStronglyTypedIdOverPrimitives.cs")
        .Verify();
}
