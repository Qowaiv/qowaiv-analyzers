using FluentAssertions;
using NUnit.Framework;
using Qowaiv.CodeAnalysis.Diagnostics;

namespace Description_specs
{
    public class Rules_have
    {
        [Test]
        public void HelpLinkUri()
            => Description.ParseShouldNotFail[0]
            .HelpLinkUri.Should().Be("https://github.com/Qowaiv/qowaiv-analyzers/blob/main/rules/QW0002.md");

    }
}
