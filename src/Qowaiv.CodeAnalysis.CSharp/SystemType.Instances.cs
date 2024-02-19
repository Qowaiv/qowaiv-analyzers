namespace Qowaiv.CodeAnalysis;

public partial class SystemType
{
    public static readonly SystemType System_Object = New(typeof(object), SpecialType.System_Object);
    public static readonly SystemType System_DateTime = New(typeof(System.DateTime), SpecialType.System_DateTime);
    public static readonly SystemType System_Void = New(typeof(void), SpecialType.System_Void);

    public static readonly SystemType System_Collections_Generic_ICollection_T = New(typeof(System.Collections.Generic.ICollection<>), SpecialType.System_Collections_Generic_ICollection_T);
    public static readonly SystemType System_Collections_Generic_IDictionary_TKey_TValue = new("System.Collections.Generic.IDictionary<TKey, TValue>");
    public static readonly SystemType System_Collections_Generic_IList_T = New(typeof(System.Collections.Generic.IList<>), SpecialType.System_Collections_Generic_IList_T);
    public static readonly SystemType System_Collections_Generic_ISet_T = new("System.Collections.Generic.ISet<T>");

    public static readonly SystemType System_Collections_Generic_IReadOnlyCollection_T = new("System.Collections.Generic.IReadOnlyCollection<T>");
    public static readonly SystemType System_Collections_Generic_IReadOnlyDictionary_TKey_TValue = new("System.Collections.Generic.IReadOnlyDictionary<TKey, TValue>");
    public static readonly SystemType System_Collections_Generic_IReadOnlyList_T = new("System.Collections.Generic.IReadOnlyList<T>");
    public static readonly SystemType System_Collections_Generic_IReadOnlySet_T = new("System.Collections.Generic.IReadOnlySet<T>");

    public static readonly SystemType System_Attribute = typeof(System.Attribute);
    public static readonly SystemType System_DateOnly = new("System.DateOnly");
    public static readonly SystemType System_DateTimeOffset = typeof(System.DateTimeOffset);
    public static readonly SystemType System_Exception = typeof(System.Exception);
    public static readonly SystemType System_IDisposable = typeof(System.IDisposable);
    public static readonly SystemType System_ObsoleteAttribute = typeof(System.ObsoleteAttribute);

    public static readonly SystemType System_Text_Encoding = typeof(System.Text.Encoding);

    public static readonly SystemType System_Diagnostics_Contracts_PureAttribute = typeof(System.Diagnostics.Contracts.PureAttribute);
    public static readonly SystemType System_Threading_Task = typeof(System.Threading.Tasks.Task);
    public static readonly SystemType System_Threading_ValueTask = typeof(System.Threading.Tasks.ValueTask);

    public static readonly SystemType Qowaiv_Date = new("Qowaiv.Date");
}
