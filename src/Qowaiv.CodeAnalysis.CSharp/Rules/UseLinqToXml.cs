namespace Qowaiv.CodeAnalysis.Rules;

/// <summary>Implements <see cref="Rule.UseLinqToXml"/>.</summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class UseLinqToXml() : CodingRule(Rule.UseLinqToXml)
{
    protected override void Register(AnalysisContext context)
    {
        context.RegisterSyntaxNodeAction(ReportField, SyntaxKind.FieldDeclaration);
        context.RegisterSyntaxNodeAction(ReportMethod, SyntaxKind.MethodDeclaration);
        context.RegisterSyntaxNodeAction(ReportParameterList, SyntaxKind.ParameterList);
        context.RegisterSyntaxNodeAction(ReportObjectCreation, SyntaxKind.ObjectCreationExpression);
        context.RegisterSyntaxNodeAction(ReportProperty, SyntaxKind.PropertyDeclaration);
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

    private void Report(TypeSyntax? syntax, SyntaxNodeAnalysisContext context)
    {
        foreach (var sub in syntax.SubTypes())
        {
            if (context.SemanticModel.GetSymbolInfo(sub).Symbol is INamedTypeSymbol type
                && Usages.Keys.FirstOrDefault(type.Is) is { } obsolete)
            {
                context.ReportDiagnostic(Diagnostic, sub, Usages[obsolete].ShortName, type.Name);
            }
        }
    }

    private static readonly FrozenDictionary<SystemType, SystemType> Usages = new Dictionary<SystemType, SystemType>()
    {
        [SystemType.System.Xml.XmlAttribute] = SystemType.System.Xml.Linq.XAttribute,
        [SystemType.System.Xml.XmlComment] = SystemType.System.Xml.Linq.XComment,
        [SystemType.System.Xml.XmlDocument] = SystemType.System.Xml.Linq.XDocument,
        [SystemType.System.Xml.XmlDocumentType] = SystemType.System.Xml.Linq.XDocumentType,
        [SystemType.System.Xml.XmlDeclaration] = SystemType.System.Xml.Linq.XDeclaration,
        [SystemType.System.Xml.XmlElement] = SystemType.System.Xml.Linq.XElement,
        [SystemType.System.Xml.XmlLinkedNode] = SystemType.System.Xml.Linq.XNode,
        [SystemType.System.Xml.XmlNode] = SystemType.System.Xml.Linq.XNode,
        [SystemType.System.Xml.XmlText] = SystemType.System.Xml.Linq.XText,
    }
    .ToFrozenDictionary();
}
