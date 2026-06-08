namespace Rules.Prefer_strongly_typedI_ID_over_GUID;

public class Verify
{
    [Test]
    public void Rule() => new PreferStronglyTypedIdOverGuid()
        .ForCS()
        .AddSource(@"Cases/PreferStronglyTypedIdOverGuid.cs")
        .AddReference<Qowaiv.Uuid>()
        .Verify();
}
