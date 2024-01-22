﻿namespace Qowaiv.CodeAnalysis.Rules;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class UseSystemDateOnly : CodingRule
{
    public UseSystemDateOnly() : base(Rule.UseSystemDateOnly) { }

    protected override void Register(AnalysisContext context)
    {
        context.RegisterSyntaxNodeAction(ReportField, SyntaxKind.FieldDeclaration);
        context.RegisterSyntaxNodeAction(ReportMethod, SyntaxKind.MethodDeclaration);
        context.RegisterSyntaxNodeAction(ReportParameterList, SyntaxKind.ParameterList);
        context.RegisterSyntaxNodeAction(ReportProperty, SyntaxKind.PropertyDeclaration);
    }

    private void ReportField(SyntaxNodeAnalysisContext context)
        => Report(context.Node.Cast<FieldDeclarationSyntax>().Declaration?.Type, context);

    private void ReportMethod(SyntaxNodeAnalysisContext context)
        => Report(context.Node.Cast<MethodDeclarationSyntax>().ReturnType, context);

    private void ReportParameterList(SyntaxNodeAnalysisContext context)
    {
        foreach (var type in context.Node.Cast<ParameterListSyntax>().Parameters.Select(p => p.Type))
        {
            Report(type, context);
        }
    }

    private void ReportProperty(SyntaxNodeAnalysisContext context)
        => Report(context.Node.Cast<PropertyDeclarationSyntax>().Type, context);

    private static void Report(TypeSyntax? syntax, SyntaxNodeAnalysisContext context)
    {
        foreach (var sub in syntax.SubTypes())
        {
            if (context.SemanticModel.GetTypeInfo(sub).Type is INamedTypeSymbol type
                && (type.NotNullable() ?? type).Is(SystemType.Qowaiv_Date))
            {
                context.ReportDiagnostic(Rule.UseSystemDateOnly, sub);
            }
        }
    }
}
