namespace Qowaiv.CodeAnalysis;

public sealed partial class SystemType
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

    /// <summary>Casts a <see cref="System.Type"/> to a <see cref="SystemType"/>.</summary>
    public static implicit operator SystemType(Type type) => new(type.FullName, default);

    private static SystemType New(Type type, SpecialType specialType) => new(type.FullName, specialType);

    private static string Last(string[] array) => array[array.Length - 1];
}
