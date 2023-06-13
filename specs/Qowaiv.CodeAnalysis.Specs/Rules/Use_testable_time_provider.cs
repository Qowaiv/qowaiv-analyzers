using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Diagnostics;
using System.Collections.Immutable;

namespace Rules.Use_testable_time_provider;

public class Verify
{
    [Test]
    public void Rule()
        => new UseTestableTimeProvider()
        .ForCS()
        .AddSource(@"Cases/UseTestableTimeProvider.cs")
        .Verify();
}

public class Fixes
{
    [Test]
    public void Code()
        => new UseTestableTimeProvider()
        .ForCS()
        .AddSource(@"Cases/UseTestableTimeProvider.ToFix.cs")
        .ForCodeFix<UseQowaivClock>()
        .AddSource(@"Cases/UseTestableTimeProvider.Fixed.cs")
        .Verify();

    [Test]
    public void Both_S6354_and_QW0001()
        => new UseQowaivClock()
        .FixableDiagnosticIds
        .Should().BeEquivalentTo("S6354", "QW0001");
}
