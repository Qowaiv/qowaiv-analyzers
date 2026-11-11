namespace Qowaiv.CodeAnalysis.Shared;

/// <summary>
/// The using directives in scope for a syntax tree, resolved as plain strings.
/// </summary>
/// <remarks>
/// Type references can only hide their name behind a using alias, so for
/// name based filtering only the alias identifiers matter. For rules that match
/// whole namespaces (or assemblies) the imported namespaces matter as well.
/// </remarks>
internal sealed class UsingDirectives(FrozenSet<string> aliases, ImmutableArray<string> targets, bool hasExternAliases)
{
    /// <summary>The identifiers of the using aliases (both <c>using</c> and <c>global using</c>).</summary>
    public FrozenSet<string> Aliases { get; } = aliases;

    /// <summary>Indicates that the file contains <c>extern alias</c> directives.</summary>
    public bool HasExternAliases { get; } = hasExternAliases;

    /// <summary>
    /// Returns true if an imported namespace (or an extern alias, that can hide
    /// it) mentions the token, or if the token can not be reasoned about.
    /// </summary>
    [Pure]
    public bool Imports(string token)
        => HasExternAliases
        || Targets.Any(target => target.Contains(token));

    /// <summary>Returns true if the name is a using alias.</summary>
    [Pure]
    public bool IsAlias(string name) => Aliases.Contains(name);

    /// <summary>Combines the using directives of a file with the global ones of the compilation.</summary>
    [Pure]
    public UsingDirectives Merge(UsingDirectives global) => new(
        aliases: Aliases.Union(global.Aliases).ToFrozenSet(),
        targets: [.. Targets, .. global.Targets],
        hasExternAliases: HasExternAliases || global.HasExternAliases);

    private ImmutableArray<string> Targets { get; } = targets;

    /// <summary>Resolves (and caches) the using directives in scope for the tree.</summary>
    [Pure]
    public static UsingDirectives For(SyntaxTree tree, Compilation compilation)
        => Merged.GetValue(compilation, _ => new()).GetOrAdd(tree, t => Scan(t).Merge(Globals(compilation)));

    private static UsingDirectives Globals(Compilation compilation)
        => Global.GetValue(compilation, ScanGlobals);

    private static UsingDirectives Scan(SyntaxTree tree)
    {
        var aliases = new HashSet<string>();
        var targets = new List<string>();
        var hasExterns = false;

        if (tree.TryGetRoot(out var node) && node is CompilationUnitSyntax unit)
        {
            hasExterns = unit.Externs.Count > 0;

            foreach (var directive in DescendantUsings(unit))
            {
                Collect(directive, aliases, targets);
            }
        }
        return new([.. aliases], [.. targets], hasExterns);
    }

    private static UsingDirectives ScanGlobals(Compilation compilation)
    {
        var aliases = new HashSet<string>();
        var targets = new List<string>();

        foreach (var tree in compilation.SyntaxTrees)
        {
            if (tree.TryGetRoot(out var node) && node is CompilationUnitSyntax unit)
            {
                foreach (var directive in DescendantUsings(unit).Where(IsGlobal))
                {
                    Collect(directive, aliases, targets);
                }
            }
        }
        return new([.. aliases], [.. targets], false);
    }

    private static void Collect(UsingDirectiveSyntax directive, HashSet<string> aliases, List<string> targets)
    {
        targets.Add(directive.NamespaceOrType.ToString());

        if (directive.Alias is { } alias)
        {
            aliases.Add(alias.Name.Identifier.ValueText);
        }
    }

    private static bool IsGlobal(UsingDirectiveSyntax directive)
        => directive.ChildTokens().Any(token => token.IsKind(SyntaxKind.GlobalKeyword));

    /// <summary>
    /// Enumerates the using directives of the compilation unit and its (nested)
    /// namespace declarations, without walking all members.
    /// </summary>
    private static IEnumerable<UsingDirectiveSyntax> DescendantUsings(CompilationUnitSyntax unit)
    {
        foreach (var directive in unit.Usings)
        {
            yield return directive;
        }

        var pending = new Queue<BaseNamespaceDeclarationSyntax>();
        EnqueueNamespaces(pending, unit.Members);

        while (pending.Count > 0)
        {
            var declaration = pending.Dequeue();

            foreach (var directive in declaration.Usings)
            {
                yield return directive;
            }
            EnqueueNamespaces(pending, declaration.Members);
        }
    }

    private static void EnqueueNamespaces(Queue<BaseNamespaceDeclarationSyntax> pending, SyntaxList<MemberDeclarationSyntax> members)
    {
        foreach (var declaration in members.OfType<BaseNamespaceDeclarationSyntax>())
        {
            pending.Enqueue(declaration);
        }
    }

    private static readonly ConditionalWeakTable<Compilation, UsingDirectives> Global = new();
    private static readonly ConditionalWeakTable<Compilation, ConcurrentDictionary<SyntaxTree, UsingDirectives>> Merged = new();
}
