namespace Qowaiv.CodeAnalysis.Shared;

public abstract class ObsoleteTypes(ImmutableArray<SyntaxKind> syntaxKinds, DescriptorContainer supportedDiagnostic, params DiagnosticDescriptor[] additional)
    : CodingRule(supportedDiagnostic, additional)
{
    protected abstract void Report(SyntaxNodeAnalysisContext context, SyntaxNode node, INamedTypeSymbol type);

    /// <summary>
    /// The short names of the types the rule deals with. Type references that do
    /// not end in one of these (and are no using alias) are not bound.
    /// </summary>
    protected virtual FrozenSet<string> CandidateNames => FrozenSet<string>.Empty;

    /// <summary>
    /// A token shared by the namespaces of the involved types. Files that do not
    /// import such a namespace are skipped as a whole, without binding.
    /// </summary>
    protected virtual string? NamespaceToken => null;

    protected override void Register(AnalysisContext context)
    {
        foreach (var kind in SyntaxKinds)
        {
            RegisterSyntaxNodeAction(context, Action(kind), kind);
        }

        Action<SyntaxNodeAnalysisContext> Action(SyntaxKind kind) => kind switch
        {
            SyntaxKind.Attribute => ReportAttribute,
            SyntaxKind.FieldDeclaration => ReportField,
            SyntaxKind.MethodDeclaration => ReportMethod,
            SyntaxKind.ParameterList => ReportParameterList,
            SyntaxKind.ObjectCreationExpression => ReportObjectCreation,
            SyntaxKind.PropertyDeclaration => ReportProperty,
            SyntaxKind.SimpleBaseType => ReportSimpleBaseType,
            _ => throw new NotSupportedException($"Syntax Kind {kind} is not supported."),
        };
    }

    private void ReportAttribute(SyntaxNodeAnalysisContext context)
    {
        var attribute = context.Node.Cast<AttributeSyntax>();

        if (IsInvolvedAttribute(attribute.Name, UsingDirectives.For(context.SemanticModel.SyntaxTree, context.Compilation))
            && new AttributeDecoration(attribute, context.SemanticModel).Symbol is { } symbol)
        {
            Report(context, context.Node, symbol);
        }
    }

    private void ReportField(SyntaxNodeAnalysisContext context)
        => Report(context.Node.Cast<FieldDeclarationSyntax>().Declaration?.Type, context);

    private void ReportMethod(SyntaxNodeAnalysisContext context)
        => Report(context.Node.Cast<MethodDeclarationSyntax>().ReturnType, context);

    private void ReportObjectCreation(SyntaxNodeAnalysisContext context)
        => Report(context.Node.Cast<ObjectCreationExpressionSyntax>().Type, context);

    private void ReportParameterList(SyntaxNodeAnalysisContext context)
    {
        foreach (var type in context.Node.Cast<ParameterListSyntax>().Parameters.Select(p => p.Type))
        {
            Report(type, context);
        }
    }

    private void ReportProperty(SyntaxNodeAnalysisContext context)
        => Report(context.Node.Cast<PropertyDeclarationSyntax>().Type, context);

    private void ReportSimpleBaseType(SyntaxNodeAnalysisContext context)
        => Report(context.Node.Cast<SimpleBaseTypeSyntax>().Type, context);

    private void Report(TypeSyntax? syntax, SyntaxNodeAnalysisContext context)
    {
        var usings = UsingDirectives.For(context.SemanticModel.SyntaxTree, context.Compilation);
        var permissive = NamespaceToken is { } token && usings.Imports(token);

        foreach (var node in syntax.SubTypes())
        {
            if (IsInvolved(node, usings, permissive)
                && context.SemanticModel.GetSymbolInfo(node, context.CancellationToken).Symbol is INamedTypeSymbol type)
            {
                Report(context, node, type);
            }
        }
    }

    /// <summary>
    /// Cheap (string only) check to avoid binding type references that can not
    /// denote an involved type. Anything that can not be determined is passed on.
    /// </summary>
    [Pure]
    private bool IsInvolved(TypeSyntax node, UsingDirectives usings, bool permissive)
    {
        if (NamespaceToken is { } token)
        {
            // The types live in namespaces sharing the token: the reference is
            // involved if the file imports one, or spells the namespace out.
            return permissive || node.Mentions(token);
        }
        else if (CandidateNames.Count == 0)
        {
            return true; // No filter configured.
        }
        else
        {
            // A type is referred to by its own name, or through a using alias.
            return node.SimpleName() is not { } name
                || CandidateNames.Contains(name)
                || usings.IsAlias(name);
        }
    }

    /// <inheritdoc cref="IsInvolved(TypeSyntax, UsingDirectives, bool)"/>
    [Pure]
    private bool IsInvolvedAttribute(NameSyntax name, UsingDirectives usings)
    {
        if (NamespaceToken is { } token)
        {
            return usings.Imports(token) || name.Mentions(token);
        }
        else if (CandidateNames.Count == 0)
        {
            return true;
        }
        else
        {
            // The Attribute suffix may be left out, so both spellings can denote the type.
            return name.SimpleName() is not { } simple
                || CandidateNames.Contains(simple)
                || CandidateNames.Contains(simple + "Attribute")
                || usings.IsAlias(simple);
        }
    }

    private readonly ImmutableArray<SyntaxKind> SyntaxKinds = syntaxKinds;
}
