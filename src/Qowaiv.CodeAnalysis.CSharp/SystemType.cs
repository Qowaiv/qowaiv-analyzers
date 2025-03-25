namespace Qowaiv.CodeAnalysis;

public sealed partial class SystemType : IEquatable<SystemType>
{
    private SystemType(string fullName, SpecialType specialType = SpecialType.None)
    {
        FullName = fullName;
        ShortName = Last(fullName.Split('.'));
        Type = specialType;
    }

    public string FullName { get; }

    public string ShortName { get; }

    public SpecialType Type { get; }

    internal bool Matches(string fullName)
        => Type == SpecialType.None
        && FullName == fullName;

    internal bool Matches(SpecialType specialType)
        => specialType != SpecialType.None
        && Type == specialType;

    /// <inheritdoc />
    public override string ToString() => FullName;

    /// <inheritdoc />
    public override bool Equals(object obj) => obj is SystemType other && Equals(other);

    /// <inheritdoc />
    public bool Equals(SystemType other)
        => FullName == other.FullName
        && Type == other.Type;

    /// <inheritdoc />
    public override int GetHashCode() => FullName.GetHashCode();

    /// <summary>Casts a <see cref="System.Type"/> to a <see cref="SystemType"/>.</summary>
    public static implicit operator SystemType(Type type) => new(type.FullName, default);

    private static SystemType New(Type type, SpecialType specialType) => new(type.FullName, specialType);

    public static SystemType New(ITypeSymbol type)
        => new(type.GetFullMetaDataName(), type.SpecialType);

    public static SystemType Parse(string str) => new(str);

    private static string Last(string[] array) => array[array.Length - 1];
}
