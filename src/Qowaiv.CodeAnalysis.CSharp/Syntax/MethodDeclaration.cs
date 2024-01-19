﻿namespace Qowaiv.CodeAnalysis.Syntax;

public abstract class MethodDeclaration : SyntaxAbstraction
{
    protected MethodDeclaration(SyntaxNode node, SemanticModel model)
        : base(node) => LazySymbol = new(() => model.GetDeclaredSymbol(node) as INamedTypeSymbol);

    public INamedTypeSymbol? Symbol => LazySymbol.Value;

    private readonly Lazy<INamedTypeSymbol?> LazySymbol;

    public abstract SyntaxList<AttributeListSyntax> AttributeLists { get; }

    public IEnumerable<AttributeSyntax> Attributes => AttributeLists.SelectMany(a => a.Attributes);

    public bool IsConcrete => !IsAbstract && !IsStatic;

    public bool IsStatic
        => Modifiers.Contains(SyntaxKind.StaticKeyword)
        || (IsPartial && Symbol?.IsStatic == true);

    public bool IsSealed
        => Modifiers.Contains(SyntaxKind.SealedKeyword)
        || (IsPartial && Symbol?.IsSealed == true);

    public bool IsAbstract
        => Modifiers.Contains(SyntaxKind.AbstractKeyword)
        || (IsPartial && Symbol?.IsAbstract == true);

    public bool IsPartial => Modifiers.Contains(SyntaxKind.PartialKeyword);

    public abstract bool IsRecord { get; }

    public abstract IEnumerable<SyntaxKind> Modifiers { get; }

    internal sealed class Class(ClassDeclarationSyntax node, SemanticModel model) : MethodDeclaration(node, model)
    {
        private readonly ClassDeclarationSyntax TypedNode = node;

        public override bool IsRecord => false;

        public override SyntaxList<AttributeListSyntax> AttributeLists => TypedNode.AttributeLists;

        public override IEnumerable<SyntaxKind> Modifiers => TypedNode.Modifiers.Select(m => m.Kind());
    }

    internal sealed class Record(RecordDeclarationSyntax node, SemanticModel model) : MethodDeclaration(node, model)
    {
        private readonly RecordDeclarationSyntax TypedNode = node;

        public override bool IsRecord => true;

        public override SyntaxList<AttributeListSyntax> AttributeLists => TypedNode.AttributeLists;

        public override IEnumerable<SyntaxKind> Modifiers => TypedNode.Modifiers.Select(m => m.Kind());
    }
}
