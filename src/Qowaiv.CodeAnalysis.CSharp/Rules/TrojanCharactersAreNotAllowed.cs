using Microsoft.CodeAnalysis.Text;

namespace Qowaiv.CodeAnalysis.Rules;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class TrojanCharactersAreNotAllowed : CodingRule
{
    public TrojanCharactersAreNotAllowed() : base(Rule.TrojanCharactersAreNotAllowed) { }

    protected override void Register(AnalysisContext context)
        => context.RegisterSyntaxTreeAction(Report);

    private void Report(SyntaxTreeAnalysisContext context)
    {
        foreach (var point in context.Tree.GetText().Lines
            .SelectMany(line => CodePoint.Parse(line.ToString(), line.Start))
            .Where(point => IsVulnerability(point.Utf32)))
        {
            context.ReportDiagnostic(
                 Microsoft.CodeAnalysis.Diagnostic.Create(
                    Diagnostic,
                    context.Tree.GetLocation(point.TextSpan),
                    point.Utf32));
        }
    }

    public static bool IsVulnerability(int utf32)
        => IsSuspiciousCategory(utf32)
        && !InAllowedRange(utf32);

    private static bool IsSuspiciousCategory(int utf32)
        => char.ConvertFromUtf32(utf32)
        .Any(ch => IsSuspiciousCategory(char.GetUnicodeCategory(ch)));

    private static bool IsSuspiciousCategory(UnicodeCategory cat)
        => cat == UnicodeCategory.Control
        || cat == UnicodeCategory.Format
        || cat == UnicodeCategory.Surrogate
        || cat == UnicodeCategory.OtherNotAssigned;

    private static bool InAllowedRange(int utf32)
        => utf32 == '\t'
        || Arabic.Contains(utf32)
        || ArabicExtendedA.Contains(utf32)
        || ArabicExtendedB.Contains(utf32)
        || ArabicPresentationFormsA.Contains(utf32)
        || ArabicPresentationFormsB.Contains(utf32)
        || RumiNumeralSymbols.Contains(utf32)
        || IndicSiyaqNumbers.Contains(utf32)
        || OttomanSiyaqNumbers.Contains(utf32)
        || ArabicMathematicalAlphabeticSymbols.Contains(utf32)
        || CJKUnifiedIdeographs.Contains(utf32)
        || CJKUnifiedIdeographsExtensionA.Contains(utf32)
        || CJKUnifiedIdeographsExtensionB.Contains(utf32)
        || CJKUnifiedIdeographsExtensionC.Contains(utf32)
        || CJKUnifiedIdeographsExtensionD.Contains(utf32)
        || CJKUnifiedIdeographsExtensionE.Contains(utf32)
        || CJKUnifiedIdeographsExtensionF.Contains(utf32)
        || CyrillicExtendedC.Contains(utf32)
        || MathematicalAlphanumeric.Contains(utf32);

    private static readonly Range Arabic = new(0x00600, 0x006FF);
    private static readonly Range ArabicExtendedA = new(0x008A0, 0x008FF);
    private static readonly Range ArabicExtendedB = new(0x00870, 0x0089F);
    private static readonly Range ArabicPresentationFormsA = new(0x0FB50, 0x0FDFF);
    private static readonly Range ArabicPresentationFormsB = new(0x0FE70, 0x0FEFF);
    private static readonly Range RumiNumeralSymbols = new(0x10E60, 0x10E7F);
    private static readonly Range IndicSiyaqNumbers = new(0x1EC70, 0x1ECBF);
    private static readonly Range OttomanSiyaqNumbers = new(0x1ED00, 0x1ED4F);
    private static readonly Range ArabicMathematicalAlphabeticSymbols = new(0x1EE00, 0x1EEFF);
    private static readonly Range CJKUnifiedIdeographs = new(0x04E00, 0X09FEF);
    private static readonly Range CJKUnifiedIdeographsExtensionA = new(0x03400, 0X04DBF);
    private static readonly Range CJKUnifiedIdeographsExtensionB = new(0x20000, 0X2A6DF);
    private static readonly Range CJKUnifiedIdeographsExtensionC = new(0x2A700, 0X2B73F);
    private static readonly Range CJKUnifiedIdeographsExtensionD = new(0x2B740, 0X2B81F);
    private static readonly Range CJKUnifiedIdeographsExtensionE = new(0x2B820, 0X2CEAF);
    private static readonly Range CJKUnifiedIdeographsExtensionF = new(0x2CEB0, 0X2EBEF);
    private static readonly Range CyrillicExtendedC = new(0x01C80, 0x01C8F);
    private static readonly Range MathematicalAlphanumeric = new(0x1D400, 0x01D7DD);

    private readonly struct Range(int start, int end)
    {
        public int Start { get; } = start;

        public int End { get; } = end;

        public bool Contains(int utf32) => utf32 >= Start && utf32 <= End;
    }

    private readonly struct CodePoint(int utf32, int start)
    {
        public int Utf32 { get; } = utf32;

        public int Index { get; } = start;

        public TextSpan TextSpan => TextSpan.FromBounds(Index, Index + 1);

        public static List<CodePoint> Parse(string str, int offset)
        {
            var codePoints = new List<CodePoint>(str.Length + 8);
            var index = 0;
            while (index < str.Length)
            {
                var point = char.ConvertToUtf32(str, index);
                var size = char.IsHighSurrogate(str[index]) ? 2 : 1;
                codePoints.Add(new CodePoint(point, index + offset));
                offset -= size - 1;
                index += size;
            }
            return codePoints;
        }
    }
}
