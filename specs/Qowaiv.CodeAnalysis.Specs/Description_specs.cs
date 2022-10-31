namespace Description_specs;

public class Rules_have
{
    [Test]
    public void HelpLinkUri()
        => Rule.ParseShouldNotFail
        .HelpLinkUri.Should().Be("https://github.com/Qowaiv/qowaiv-analyzers/blob/main/rules/QW0002.md");

    [Test]
    public void Category()
       => Rule.ParseShouldNotFail
       .Category.Should().Be("Runtime Error");

}
