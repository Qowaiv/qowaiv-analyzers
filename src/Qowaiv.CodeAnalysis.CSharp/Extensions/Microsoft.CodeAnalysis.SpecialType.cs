namespace Microsoft.CodeAnalysis;

internal static class SpecialTypeExtensions
{
    public static Type? GetRuntimeType(this SpecialType specialType) 
        => specialType switch
        {
            SpecialType.System_Object => typeof(object),
            SpecialType.System_Enum => typeof(Enum),
            SpecialType.System_MulticastDelegate => typeof(MulticastDelegate),
            SpecialType.System_Delegate => typeof(Delegate),
            SpecialType.System_ValueType => typeof(ValueType),
            SpecialType.System_Boolean => typeof(bool),
            SpecialType.System_Char => typeof(char),
            SpecialType.System_SByte => typeof(sbyte),
            SpecialType.System_Byte => typeof(byte),
            SpecialType.System_Int16 => typeof(short),
            SpecialType.System_UInt16 => typeof(ushort),
            SpecialType.System_Int32 => typeof(int),
            SpecialType.System_UInt32 => typeof(uint),
            SpecialType.System_Int64 => typeof(long),
            SpecialType.System_UInt64 => typeof(ulong),
            SpecialType.System_Decimal => typeof(decimal),
            SpecialType.System_Single => typeof(float),
            SpecialType.System_Double => typeof(double),
            SpecialType.System_String => typeof(string),
            SpecialType.System_IntPtr => typeof(IntPtr),
            SpecialType.System_UIntPtr => typeof(UIntPtr),
            SpecialType.System_Array => typeof(Array),
            SpecialType.System_Collections_IEnumerable => typeof(System.Collections.IEnumerable),
            SpecialType.System_Collections_IEnumerator => typeof(System.Collections.IEnumerator),
            SpecialType.System_DateTime => typeof(DateTime),
            SpecialType.System_Runtime_CompilerServices_IsVolatile => typeof(System.Runtime.CompilerServices.IsVolatile),
            SpecialType.System_IDisposable => typeof(IDisposable),
            SpecialType.System_TypedReference => typeof(TypedReference),
            SpecialType.System_RuntimeArgumentHandle => typeof(RuntimeArgumentHandle),
            SpecialType.System_RuntimeFieldHandle => typeof(RuntimeFieldHandle),
            SpecialType.System_RuntimeMethodHandle => typeof(RuntimeMethodHandle),
            SpecialType.System_RuntimeTypeHandle => typeof(RuntimeTypeHandle),
            SpecialType.System_IAsyncResult => typeof(IAsyncResult),
            SpecialType.System_AsyncCallback => typeof(AsyncCallback),
            _ => null,
        };
}
