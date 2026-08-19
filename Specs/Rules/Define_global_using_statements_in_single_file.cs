namespace Rules.Define_global_using_statements_in_single_file;

public class Verify
{
    [TestCase("GlobalUsings.cs")]
    [TestCase("Cases/GlobalUsings.cs")]
    [TestCase("Cases\\GlobalUsings.cs")]
    public void Properties_GlobalUsings_cs(string globalConfigFile) => new DefineGlobalUsingStatementsInSingleFile(globalConfigFile)
        .ForCS()
        .AddSource(@"Cases/GlobalUsings.cs")
        .Verify();

    [Test]
    public void Other_location() => new DefineGlobalUsingStatementsInSingleFile()
        .ForCS()
        .AddSource(@"Cases/DefineGlobalUsingStatementsInSingleFile.cs")
        .Verify();
}
