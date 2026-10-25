namespace Microsoft.CodeAnalysis.CSharp;

internal static class SyntaxKindExtensions
{
    [Pure]
    public static Accessibility GetAccessibility(
        this IEnumerable<SyntaxKind>? accessors,
        Accessibility @default = Accessibility.Private)
    {
        const int Internal = 1;
        const int Protected = 2;

        var flags = 0;

        foreach (var accessor in accessors ?? [])
        {
            switch (accessor)
            {
                case SyntaxKind.PublicKeyword: return Accessibility.Public;
                case SyntaxKind.PrivateKeyword: return Accessibility.Private;
                case SyntaxKind.ProtectedKeyword: flags |= Protected; break;
                case SyntaxKind.InternalKeyword: flags |= Internal; break;
            }
        }
        return flags switch
        {
            Internal => Accessibility.Internal,
            Protected => Accessibility.Protected,
            Protected | Internal => Accessibility.ProtectedAndInternal,
            _ => @default,
        };
    }
}
