namespace Fixes.Generate_GUIDs;

public class Fixes
{
    [Test]
    public void Code()
        => new GuidLiterals()
        .ForCS()
        .AddSource(@"Cases/GenerateGuid.ToFix.cs")
        .ForCodeFix(new GenerateGuid(Next))
        .AddSource(@"Cases/GenerateGuid.Fixed.cs")
        .Verify();

    private static Guid Next() => Guid.Parse("1f570b45-8b18-496a-84a3-634db4f4c552");
}
