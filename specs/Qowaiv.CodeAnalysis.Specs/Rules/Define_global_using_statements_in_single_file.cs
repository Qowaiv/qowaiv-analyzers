namespace Rules.Define_global_using_statements_in_single_file;

public class Verify
{
    [Test]
    public void Properties_GlobalUsings_cs() => new DefineGlobalUsingStatementsInSingleFile("Cases/GlobalUsings.cs")
        .ForCS()
        .AddSource(@"Cases/GlobalUsings.cs")
        .Verify();

    [Test]
    public void Other_location() => new DefineGlobalUsingStatementsInSingleFile()
        .ForCS()
        .AddSource(@"Cases/DefineGlobalUsingStatementsInSingleFile.cs")
        .Verify();
}
