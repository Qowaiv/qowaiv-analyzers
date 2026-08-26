using strings = System.Collections.Immutable.ImmutableArray<string>;

namespace Qowaiv.CodeAnalysis;

internal static class Tag
{
    public const string Bug = "Bug";
    public const string DataAnnotations = "Data Annotations";
    public const string Design = "Design";
    public const string Error = "Error";
    public const string PrimitiveObsession = "primitive obsession";
    public static readonly strings SVO = ["SVO", "Value Object", "Single Value Object"];
    public const string Test = "Test";
    public const string Validation = "Validation";
}
