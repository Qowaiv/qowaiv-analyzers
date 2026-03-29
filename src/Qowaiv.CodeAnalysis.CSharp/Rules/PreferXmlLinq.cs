namespace Qowaiv.CodeAnalysis.Rules;

/// <summary>Implements <see cref="Rule.PreferXmlLinq"/>.</summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class PreferXmlLinq() : ObsoleteTypes(
    [
        SyntaxKind.FieldDeclaration,
        SyntaxKind.MethodDeclaration,
        SyntaxKind.ObjectCreationExpression,
        SyntaxKind.ParameterList,
        SyntaxKind.PropertyDeclaration,
    ]
    , Rule.PreferXmlLinq)
{
    protected override void Report(SyntaxNodeAnalysisContext context, TypeSyntax node, INamedTypeSymbol type)
    {
        if (Usages.Keys.FirstOrDefault(type.Is) is { } obsolete)
        {
            context.ReportDiagnostic(Diagnostic, node, Usages[obsolete].ShortName, type!.Name);
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
