namespace Qowaiv.CodeAnalysis.Rules;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public abstract class GuidLiteralsBase(DescriptorContainer supportedDiagnostic, params IEnumerable<DiagnosticDescriptor> additional)
    : CodingRule(supportedDiagnostic, additional)
{
    protected abstract SystemType Type { get; }

    protected abstract bool IsValid(string literal);

    protected sealed override void Register(AnalysisContext context)
    {
        RegisterSyntaxNodeAction(
            context,
            Ctor,
            SyntaxKind.ObjectCreationExpression,
            SyntaxKind.ImplicitObjectCreationExpression);

        RegisterSyntaxNodeAction(
           context,
           Method,
           SyntaxKind.InvocationExpression);
    }

    private void Ctor(SyntaxNodeAnalysisContext context)
    {
        if (context.Node.ObjectCreation(context.SemanticModel) is { Symbol.ContainingType: INamedTypeSymbol type } node && type.Is(Type))
        {
            if (node.Arguments.Count is 1 && node.Arguments[0].Expression is LiteralExpressionSyntax literal)
            {
                Check(literal, context);
            }
            else if (node.Arguments.Count is 0)
            {
                context.ReportDiagnostic(SupportedDiagnostics[2], node);
            }
        }
    }

    private void Method(SyntaxNodeAnalysisContext context)
    {
        var invocation = context.Node.Cast<InvocationExpressionSyntax>();
        var access = invocation.Expression as MemberAccessExpressionSyntax;

        if (access?.Name() is "Parse")
        {
            var info = context.SemanticModel.GetSymbolInfo(access);

            if (info.Symbol?.ContainingType.Is(Type) is true
                && invocation.ArgumentList.Arguments[0].Expression is LiteralExpressionSyntax literal)
            {
                Check(literal, context);
            }

            if (info.CandidateSymbols.Any(t => t.ContainingType.Is(Type)))
            {
                var report = invocation.ArgumentList?.Arguments.FirstOrDefault()?.Expression ?? access;
                context.ReportDiagnostic(SupportedDiagnostics[1], report);
            }
        }
    }

    private void Check(LiteralExpressionSyntax literal, SyntaxNodeAnalysisContext context)
    {
        var text = literal.Token.Text.Trim('"');

        if (!IsValid(text))
        {
            context.ReportDiagnostic(SupportedDiagnostics[0], literal, text);
        }
    }
}
