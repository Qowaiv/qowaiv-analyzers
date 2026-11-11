using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Qowaiv.CodeAnalysis.Rules;

[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class UseFormattedAmountLiterals() : CodingRule(Rule.UseFormattedAmountLiterals)
{
    protected override void Register(AnalysisContext context)
        => RegisterSyntaxNodeAction(context, Report, SyntaxKind.NumericLiteralExpression);
    
    private void Report(SyntaxNodeAnalysisContext context)
    {
        var node = context.Node.Cast<LiteralExpressionSyntax>();

        if ((Cast() ?? UnformattedExpression()) is { } parent
            && !IsObsoleteCode())
        {
            context.ReportDiagnostic(Diagnostic, parent);
        }

        SyntaxNode? Cast()
            => Trim(node.Parent) is CastExpressionSyntax cast
            && cast.Type.TypeNode(context.SemanticModel).Symbol.Is(SystemType.Qowaiv.Financial.Amount)
            ? cast
            : null;

        SyntaxNode? UnformattedExpression()
            => Trim(node.Parent) is MemberAccessExpressionSyntax expression
            && expression.Method(context.SemanticModel) is { Name: "Amount", Parameters.Length: 0 } method
            && method.ReturnType.Is(SystemType.Qowaiv.Financial.Amount)
            && !Amount.IsFormatted(node.Token.Text)
            ? expression.Parent
            : null;

        bool IsObsoleteCode()
            => node.Ancestors().Any(IsObsoleteDeclaration);

        bool IsObsoleteDeclaration(SyntaxNode ancestor)
            => ancestor switch
            {
                // The type declaration resolves its symbol lazily, and only
                // when it has attributes or is partial.
                TypeDeclarationSyntax type
                    => type.TypeDeclaration(context.SemanticModel).IsObsolete,

                // The symbol of a property is only resolved when it has
                // attributes, or when it is partial, as the attributes of a
                // partial member can be declared on another part.
                PropertyDeclarationSyntax property
                    when property.AttributeLists.Count > 0 || property.Modifiers.Any(SyntaxKind.PartialKeyword)
                    => property.PropertyDeclaration(context.SemanticModel).IsObsolete,

                // The symbol of a method is only resolved when it has
                // attributes, or when it is partial.
                BaseMethodDeclarationSyntax method
                    when method.AttributeLists.Count > 0 || method.Modifiers.Any(SyntaxKind.PartialKeyword)
                    => context.SemanticModel.GetDeclaredSymbol(method, context.CancellationToken) is { IsObsolete: true },

                // For a field, the attributes are on the field declaration,
                // while the symbol is declared by the variable declarator.
                VariableDeclaratorSyntax declarator
                    when declarator.Parent?.Parent is FieldDeclarationSyntax { AttributeLists.Count: > 0 }
                    => context.SemanticModel.GetDeclaredSymbol(declarator, context.CancellationToken) is { IsObsolete: true },

                _ => false,
            };
    }

    private static SyntaxNode? Trim(SyntaxNode? node) => node switch
    {
        null => null,
        ParenthesizedExpressionSyntax or 
        PrefixUnaryExpressionSyntax => Trim(node.Parent),
        _ => node,
    };
}
