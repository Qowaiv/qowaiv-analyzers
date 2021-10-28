using CodeAnalysis.TestTools;
using NUnit.Framework;
using Qowaiv.CodeAnalysis;

namespace Rules.UseTestableTimeProvider_specs
{
    public class Verify
    {
        [Test]
        public void CSharp()
            => new UseTestableTimeProvider()
            .ForCS()
            .AddSource(@"Cases\UseTestableTimeProvider.cs")
            .Verify();
    }
}
