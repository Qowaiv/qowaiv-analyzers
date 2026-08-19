namespace Fixes.Generate_GUIDs;

public class Fixes
{
    [Test]
    public void GUIDs()
        => new GuidLiterals()
        .ForCS()
        .AddSource(@"Cases/GenerateGuid.ToFix.cs")
        .ForCodeFix(new GenerateGuid(NextGuid))
        .AddSource(@"Cases/GenerateGuid.Fixed.cs")
        .Verify();

    [Test]
    public void UUIDs()
        => new UuidLiterals()
        .ForCS()
        .AddSource(@"Cases/GenerateUuid.ToFix.cs")
        .AddReference<Qowaiv.Uuid>()
        .ForCodeFix(new GenerateUuid(NextUuid))
        .AddSource(@"Cases/GenerateUuid.Fixed.cs")
        .Verify();

    private static Guid NextGuid() => Guid.Parse("1f570b45-8b18-496a-84a3-634db4f4c552");

    private static string NextUuid() => "FJGaEYVukUieqEbPRn_GuA";
}
