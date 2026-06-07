namespace Rules.Define_global_using_statements_separately;

public class Verify
{
    [Test]
    public void Rule_for_only_globals() => new DefineGlobalUsingStatementsSeparately()
        .ForCS()
        .AddSource(@"Cases/DefineGlobalUsingStatementsSeparately.GlobalUsings.cs")
        .Verify();

    [Test]
    public void Rule_with_mix() => new DefineGlobalUsingStatementsSeparately()
        .ForCS()
        .AddSource(@"Cases/DefineGlobalUsingStatementsSeparately.cs")
        .Verify();
}
