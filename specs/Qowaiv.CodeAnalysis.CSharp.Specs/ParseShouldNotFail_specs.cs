using CodeAnalysis.TestTools;
using NUnit.Framework;
using Qowaiv.CodeAnalysis;

namespace Rules.ParseShouldNotFail_specs
{
    public class Verify
    {
        [Test]
        public void CSharp()
            => new ParseShouldNotFail()
            .ForCS()
            .AddSource(@"Cases\ParseShouldNotFail.cs")
            .AddReference<Qowaiv.Percentage>()
            .Verify();
    }
}
