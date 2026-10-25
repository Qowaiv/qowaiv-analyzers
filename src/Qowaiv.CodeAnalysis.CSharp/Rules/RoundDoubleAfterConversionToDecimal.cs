namespace Qowaiv.CodeAnalysis.Rules;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class RoundDoubleAfterConversionToDecimal() : CodingRule(Rule.RoundDoubleAfterConversionToDecimal)
{
    protected override void Register(AnalysisContext context)
        => RegisterSyntaxNodeAction(
            context,
            Report,
            SyntaxKind.CastExpression,
            SyntaxKind.ImplicitObjectCreationExpression,
            SyntaxKind.InvocationExpression,
            SyntaxKind.ObjectCreationExpression);

    private void Report(SyntaxNodeAnalysisContext context)
    {
        if (Conversion(context.Node, context.SemanticModel) is { } conversion && !IsMathRound(conversion, context.SemanticModel))
        {
            context.ReportDiagnostic(Diagnostic, conversion);
        }
    }

    [Pure]
    private static ExpressionSyntax? Conversion(SyntaxNode node, SemanticModel model) => node switch
    {
        CastExpressionSyntax cast when IsDoubleToDecimal(cast, model) => cast,
        _ when node.TryObjectCreation(model) is { } creation && IsDecimalCtorFromDouble(creation) => (ExpressionSyntax?)creation.Node,
        InvocationExpressionSyntax invocation when IsConvertToDecimal(invocation, model) => invocation,
        _ => null,
    };

    [Pure]
    private static bool IsDoubleToDecimal(CastExpressionSyntax cast, SemanticModel model)
        => IsDecimal(model.GetTypeInfo(cast.Type).Type)
        && IsDouble(model.GetTypeInfo(cast.Expression).Type);

    [Pure]
    private static bool IsDecimalCtorFromDouble(ObjectCreation creation)
        => creation.Symbol is { Parameters.Length: 1 } constructor
        && constructor.MemberOf(SystemType.System.Decimal)
        && constructor.Parameters[0].Type.Is(SystemType.System.Double);

    [Pure]
    private static bool IsConvertToDecimal(InvocationExpressionSyntax invocation, SemanticModel model)
        => model.GetSymbolInfo(invocation).Symbol is IMethodSymbol { Name: nameof(Convert.ToDecimal), Parameters.Length: 1 } method
        && method.MemberOf(SystemType.System.Convert)
        && IsDouble(method.Parameters[0].Type);

    [Pure]
    private static bool IsMathRound(ExpressionSyntax conversion, SemanticModel model)
        => EnclosingInvocation(conversion) is { } invocation
        && model.GetSymbolInfo(invocation).Symbol is IMethodSymbol { Name: nameof(Math.Round) } method
        && (method.MemberOf(SystemType.System.Math) || method.MemberOf(SystemType.System.Decimal));

    [Pure]
    private static InvocationExpressionSyntax? EnclosingInvocation(ExpressionSyntax node)
        => node.TrimParenthesis() is { Parent: ArgumentSyntax { Parent: BaseArgumentListSyntax { Parent: InvocationExpressionSyntax invocation } } }
        ? invocation
        : null;

    [Pure]
    private static bool IsDecimal(ITypeSymbol? type)
        => type.Is(SystemType.System.Decimal)
        || WithoutNullable(type).Is(SystemType.System.Decimal);

    [Pure]
    private static bool IsDouble(ITypeSymbol? type)
        => type.Is(SystemType.System.Double)
        || WithoutNullable(type).Is(SystemType.System.Double);

    [Pure]
    private static INamedTypeSymbol? WithoutNullable(ITypeSymbol? type)
        => (type as INamedTypeSymbol)?.NotNullable;

    
}
