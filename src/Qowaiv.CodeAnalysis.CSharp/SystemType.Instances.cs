namespace Qowaiv.CodeAnalysis;

public partial class SystemType
{
    public static class System
    {
        public static readonly SystemType Object = New(typeof(object), SpecialType.System_Object);
        public static readonly SystemType String = New(typeof(string), SpecialType.System_String);
        public static readonly SystemType Decimal = New(typeof(decimal), SpecialType.System_Decimal);
        public static readonly SystemType DateTime = New(typeof(global::System.DateTime), SpecialType.System_DateTime);
        public static readonly SystemType Enum = New(typeof(global::System.Enum), SpecialType.System_Enum);
        public static readonly SystemType Void = New(typeof(void), SpecialType.System_Void);

        public static readonly SystemType Attribute = typeof(global::System.Attribute);
        public static readonly SystemType DateOnly = new("System.DateOnly");
        public static readonly SystemType DateTimeOffset = typeof(global::System.DateTimeOffset);
        public static readonly SystemType Exception = typeof(global::System.Exception);
        public static readonly SystemType IConvertible = typeof(global::System.IConvertible);
        public static readonly SystemType IComparable = typeof(global::System.IComparable);
        public static readonly SystemType IDisposable = typeof(global::System.IDisposable);
        public static readonly SystemType Math = typeof(global::System.Math);
        public static readonly SystemType ObsoleteAttribute = typeof(global::System.ObsoleteAttribute);
        public static readonly SystemType TimeProvider = new("System.TimeProvider");
        public static readonly SystemType Type = typeof(global::System.Type);

        public static class Collections
        {
            public static readonly SystemType IEnumerator = typeof(global::System.Collections.IEnumerator);

            public static class Generic
            {
                public static readonly SystemType ICollection_T = New(typeof(global::System.Collections.Generic.ICollection<>), SpecialType.System_Collections_Generic_ICollection_T);
                public static readonly SystemType IDictionary_TKey_TValue = new("System.Collections.Generic.IDictionary<TKey, TValue>");
                public static readonly SystemType IList_T = New(typeof(global::System.Collections.Generic.IList<>), SpecialType.System_Collections_Generic_IList_T);
                public static readonly SystemType ISet_T = new("System.Collections.Generic.ISet<T>");

                public static readonly SystemType IReadOnlyCollection_T = new("System.Collections.Generic.IReadOnlyCollection<T>");
                public static readonly SystemType IReadOnlyDictionary_TKey_TValue = new("System.Collections.Generic.IReadOnlyDictionary<TKey, TValue>");
                public static readonly SystemType IReadOnlyList_T = new("System.Collections.Generic.IReadOnlyList<T>");
                public static readonly SystemType IReadOnlySet_T = new("System.Collections.Generic.IReadOnlySet<T>");
            }
        }

        public static class ComponentModel
        {
            public static class DataAnnotations
            {
                public static readonly SystemType AllowedValuesAttribute /*..*/ = new("System.ComponentModel.DataAnnotations.AllowedValuesAttribute");
                public static readonly SystemType Base64StringAttribute /*...*/ = new("System.ComponentModel.DataAnnotations.Base64StringAttribute");
                public static readonly SystemType CreditCardAttribute /*.....*/ = new("System.ComponentModel.DataAnnotations.CreditCardAttribute");
                public static readonly SystemType DeniedValuesAttribute /*...*/ = new("System.ComponentModel.DataAnnotations.DeniedValuesAttribute");
                public static readonly SystemType EmailAddressAttribute /*...*/ = new("System.ComponentModel.DataAnnotations.EmailAddressAttribute");
                public static readonly SystemType EnumDataTypeAttribute /*...*/ = new("System.ComponentModel.DataAnnotations.EnumDataTypeAttribute");
                public static readonly SystemType FileExtensionsAttribute /*.*/ = new("System.ComponentModel.DataAnnotations.FileExtensionsAttribute");
                public static readonly SystemType PhoneAttribute /*..........*/ = new("System.ComponentModel.DataAnnotations.PhoneAttribute");
                public static readonly SystemType RangeAttribute /*..........*/ = new("System.ComponentModel.DataAnnotations.RangeAttribute");
                public static readonly SystemType RequiredAttribute /*.......*/ = new("System.ComponentModel.DataAnnotations.RequiredAttribute");
                public static readonly SystemType StringLengthAttribute /*...*/ = new("System.ComponentModel.DataAnnotations.StringLengthAttribute");
                public static readonly SystemType UrlAttribute /*............*/ = new("System.ComponentModel.DataAnnotations.UrlAttribute");
                public static readonly SystemType ValidationAttribute /*.....*/ = new("System.ComponentModel.DataAnnotations.ValidationAttribute");
            }
        }

        public static class Diagnostics
        {
            public static class CodeAnalysis
            {
                public static readonly SystemType DoesNotReturnAttribute = new("System.Diagnostics.CodeAnalysis.DoesNotReturnAttribute");
            }

            public static class Contracts
            {
                public static readonly SystemType PureAttribute = typeof(global::System.Diagnostics.Contracts.PureAttribute);
            }
        }

        public static class Threading
        {
            public static readonly SystemType Task = typeof(global::System.Threading.Tasks.Task);
            public static readonly SystemType ValueTask = typeof(global::System.Threading.Tasks.ValueTask);
        }

        public static class Xml
        {
            public static class Serialization
            {
                public static readonly SystemType IXmlSerializable = typeof(global::System.Xml.Serialization.IXmlSerializable);
            }
        }
    }

    public static class Qowaiv
    {
        public static readonly SystemType Clock = new("Qowaiv.Clock");
        public static readonly SystemType Date = new("Qowaiv.Date");

        public static class Validation
        {
            public static class DataAnnotations
            {
                public static readonly SystemType ValidatesAttribute = new("Qowaiv.Validation.DataAnnotations.ValidatesAttribute");
            }
        }
    }
}
