namespace Qowaiv.CodeAnalysis.Rules;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class UseLeadingZerosToDefineDateConstants() : CodingRule(Rule.UseLeadingZerosToDefineDateConstants)
{
    protected override void Register(AnalysisContext context)
        => RegisterSyntaxNodeAction(
            context,
            Report,
            SyntaxKind.ObjectCreationExpression,
            SyntaxKind.ImplicitObjectCreationExpression);

    private void Report(SyntaxNodeAnalysisContext context)
    {
        var node = context.Node.ObjectCreation(context.SemanticModel);

        if (node is { Symbol.Parameters.Length: >= 3 } && node.Arguments.All(IsNumericLiteral))
        {
            foreach (var argument in node.Arguments)
            {
                var tooShort = TooShort(argument);

                if (tooShort > 0)
                {
                    context.ReportDiagnostic(Diagnostic, argument, tooShort is 1 ? string.Empty : "s", argument.Symbol!.Name);
                }
            }
        }
    }

    private static bool IsNumericLiteral(Argument argument)
        => argument.IsLiteral
        && argument.Symbol is { } symbol
        && Arguments.TryGetValue(symbol.Name, out var arg)
        && symbol.Type.Is(arg.Type);

    private static int TooShort(Argument argument)
    {
        var length = Arguments[argument.Symbol!.Name].Length;
        return length - argument.Expression.GetText().Length;
    }

    private static readonly FrozenDictionary<string, Arg> Arguments = new Dictionary<string, Arg>()
    {
        ["year"] = new(4, SystemType.System.Int32),
        ["month"] = new(2, SystemType.System.Int32),
        ["day"] = new(2, SystemType.System.Int32),
        ["hour"] = new(2, SystemType.System.Int32),
        ["minute"] = new(2, SystemType.System.Int32),
        ["second"] = new(2, SystemType.System.Int32),
        ["millisecond"] = new(3, SystemType.System.Int32),
        ["kind"] = new(0, SystemType.System.DateTimeKind),
    }
    .ToFrozenDictionary();

    private readonly struct Arg(int length, SystemType type)
    {
        public readonly int Length = length;
        public readonly SystemType Type = type;
    }
}
