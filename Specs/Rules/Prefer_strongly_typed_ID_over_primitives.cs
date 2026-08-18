namespace Rules.Prefer_strongly_typed_ID_over_primitives;

public class Verify
{
    [Test]
    public void Rule() => new PreventPrimitiveObssession()
        .ForCS()
        .AddSource(@"Cases/PreferStronglyTypedIdOverPrimitives.cs")
        .Verify();
}
