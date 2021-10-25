using CodeAnalysis.TestTools;
using NUnit.Framework;
using CS = Qowaiv.CodeAnalysis.CSharp;
using VB = Qowaiv.CodeAnalysis.VisualBasic;

namespace Rules.ParseShouldNotFail_specs
{
    public class Verify
    {
        [Test]
        public void CSharp()
            => new CS.ParseShouldNotFail()
            .ForCS()
            .AddSource(@"Cases\ParseShouldNotFail.cs")
            .AddReference<Qowaiv.Percentage>()
            .Verify();

        [Test]
        public void VisualBasic()
            => new VB.ParseShouldNotFail()
            .ForVB()
            .AddSource(@"Cases\ParseShouldNotFail.vb")
            .AddReference<Qowaiv.Percentage>()
            .Verify();
    }
}
