using CodeAnalysis.TestTools;
using NUnit.Framework;
using CS = Qowaiv.CodeAnalysis.CSharp;
using VB = Qowaiv.CodeAnalysis.VisualBasic;

namespace Rules.UseTestableTimeProvider_specs
{
    public class Verify
    {
        [Test]
        public void CSharp()
            => new CS.UseTestableTimeProvider()
            .ForCS()
            .AddSource(@"Cases\UseTestableTimeProvider.cs")
            .Verify();

        [Test]
        public void VisualBasic()
            => new VB.UseTestableTimeProvider()
            .ForVB()
            .AddSource(@"Cases\UseTestableTimeProvider.vb")
            .Verify();
    }
}