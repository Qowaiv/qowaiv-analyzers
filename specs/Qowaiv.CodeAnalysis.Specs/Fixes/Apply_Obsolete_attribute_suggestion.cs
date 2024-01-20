namespace Fixes.Apply_Obsolete_attribute_suggestion;

public class Finds
{
    [TestCase("Use Clock.UtcNow() instead.", "Clock.UtcNow()")]
    [TestCase("Date.Today will be dropped. Use Clock.Today() instead.", "Clock.Today()")]
    public void Suggestion(string obsoleteMessage, string suggestion)
        => ApplyObsoleteSuggestion.Suggestion(obsoleteMessage).Should().Be(suggestion);
}

public class Applies
{
    [Test]
    public void suggestions() => new NoRule()
        .ForCS()
        .WithCompilerWarnings(true)
        .AddSource(@"Cases/ApplyObsoleteSuggestion.ToFix.cs")
        .ForCodeFix<ApplyObsoleteSuggestion>()
        .AddSource(@"Cases/ApplyObsoleteSuggestion.Fixed.cs")
        .Verify();
}
