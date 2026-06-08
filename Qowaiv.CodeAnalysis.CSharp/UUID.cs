namespace Qowaiv.CodeAnalysis;

public static class UUID
{
    public static bool IsValid(string str)
        => str.Length is 22
        && str.All("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-_".Contains);

    public static string Next() => Convert
        .ToBase64String(Guid.NewGuid().ToByteArray())
        .Substring(0, 22)
        .Replace('+', '-')
        .Replace('/', '_');
}
