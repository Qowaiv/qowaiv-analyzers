using CodeAnalysis.TestTools;
using NUnit.Framework;
using Qowaiv.CodeAnalysis;

namespace Rules.DecorateFunctions_specs
{
    public class Verify
    {
        [Test]
        public void CSharp()
            => new DecorateFunctions()
            .ForCS()
            .AddSource(@"Cases\DecorateFunctions.cs")
            .AddReference<FluentAssertions.CustomAssertionAttribute>()
            .Verify();
    }
}
