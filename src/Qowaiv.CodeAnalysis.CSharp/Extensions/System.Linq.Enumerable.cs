namespace System.Linq;

internal static class EnumerableExtensions
{
    public static IReadOnlyList<T> Singleton<T>(this T? item)
        => item is { } ? [item] : Array.Empty<T>();
}
