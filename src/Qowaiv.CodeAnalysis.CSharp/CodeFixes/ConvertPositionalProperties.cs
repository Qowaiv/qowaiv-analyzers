using Qowaiv.CodeAnalysis.Rules;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;
using static Microsoft.CodeAnalysis.CSharp.SyntaxKind;

namespace Qowaiv.CodeAnalysis.CodeFixes;

[ExportCodeFixProvider(LanguageNames.CSharp)]
public sealed class ConvertPositionalProperties() : CodeFix(Rule.PreferRegularOverPositionalProperties.Id)
{
    public async override Task RegisterCodeFixesAsync(CodeFixContext context)
    {
        if (await context.ChangeDocumentContext() is { Node: ParameterSyntax parameter } change)
        {
            change.RegisterFix("Convert to regular property.", context, d => Change(parameter, d));
        }
    }

    private static Task<Document> Change(ParameterSyntax parameter, ChangeDocumentContext context)
    {
        var parameters = (ParameterListSyntax)parameter.Parent!;
        var declaration = (RecordDeclarationSyntax)parameters.Parent!;

        var parameterTrivia = parameter.GetLeadingTrivia();

        var property = PropertyDeclaration(parameter.Type!, parameter.Identifier)
            .AddModifiers(parameter)
            .AddAccessors()
            .AddAttributes(parameter.AttributeLists)
            .WithDefault(parameter.Default)
            .WithLeadingTrivia(parameterTrivia.Insert(0, LineFeed));

        var updated = declaration
            .WithBraces()
            .WithParameterList(ParameterList(parameters.Parameters.Remove(parameter)))
            .AddMembers(property);

        return context.ReplaceNode(declaration, updated);
    }
}

file static class Extensions
{
    public static PropertyDeclarationSyntax AddModifiers(this PropertyDeclarationSyntax property, ParameterSyntax param)
        => PreferRegularOverPositionalProperties.NotNull(param)
        ? property.AddModifiers(Token(PublicKeyword), Token(SyntaxKindExt.RequiredKeyword))
        : property.AddModifiers(Token(PublicKeyword));

    public static PropertyDeclarationSyntax AddAccessors(this PropertyDeclarationSyntax property) => property
        .AddAccessorListAccessors(
            AccessorDeclaration(GetAccessorDeclaration).WithSemicolonToken(Token(SemicolonToken)),
            AccessorDeclaration(InitAccessorDeclaration).WithSemicolonToken(Token(SemicolonToken)));

    public static PropertyDeclarationSyntax AddAttributes(this PropertyDeclarationSyntax property, SyntaxList<AttributeListSyntax> list)
    {
        var attrs = list.SelectMany(l => l.Attributes)
            .Select(attr => AttributeList(SeparatedList([attr])));

        return attrs.Any()
            ? property.WithAttributeLists([.. attrs])
            : property;
    }

    public static PropertyDeclarationSyntax WithDefault(this PropertyDeclarationSyntax property, EqualsValueClauseSyntax? @default)
        => @default is { }
        ? property.WithInitializer(@default)
            .WithSemicolonToken(Token(SemicolonToken))
        : property;

    public static RecordDeclarationSyntax WithBraces(this RecordDeclarationSyntax record) => record
        .WithOpenBraceToken(Token(OpenBraceToken))
        .WithCloseBraceToken(Token(CloseBraceToken))
        .WithSemicolonToken(default);
}
