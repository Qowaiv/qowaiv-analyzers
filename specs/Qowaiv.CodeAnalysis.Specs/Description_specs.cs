namespace Description_specs;

public class Rules_have
{
    [Test]
    public void HelpLinkUri()
        => Description.ParseShouldNotFail
        .HelpLinkUri.Should().Be("https://github.com/Qowaiv/qowaiv-analyzers/blob/main/rules/QW0002.md");

    [Test]
    public void Category()
       => Description.ParseShouldNotFail
       .Category.Should().Be("Runtime Error");

}
