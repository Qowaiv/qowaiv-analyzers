using System;
using System.Collections.Generic;

namespace Compliant
{
    internal class InternalClass
    {
        public int Value { get; set; } // Compliant {{Internal classes are ignored.}}
    }

    internal record InternalRecord
    {
        public int Value { get; set; } // Compliant {{Internal records are ignored.}}
    }

    internal struct InternalStruct
    {
        public int Value { get; set; } // Compliant {{Internal structs are ignored.}}
    }

    public interface Interface
    {
        int Value { get; } // Compliant {{Interface with get only property.}}
    }

    public class Class
    {
        public Class(int val) => GetOnly = val;

        public static int StaticProperties { get; set; } // Compliant {{Static properties are ignored.}}

        public int PublicField; // Compliant {{Fields are ignored.}}

        public int InitOnly { get; init; } // Compliant {{Property can only be changed during the init phase.}}

        public int Caclulated => 42 * PublicField; // Compliant {{Calculated properties are considered immutable.}}

        public int GetOnly { get; } // Compliant {{Property is set during construction.}}

        internal int Internal { get; set; } // Compliant {{Internal properties are ignored.}}

        internal protected int InternalProtected { get; set; } // Compliant {{Internal protected properties are ignored.}}

        int WithoutAccessors { get; set; } // Compliant {{Properties without accessors are private and hence ignored.}}

        private int Private { get; set; } // Compliant {{Private properties are ignored.}}
    }

    public record Record
    {
        public Record(int val) => GetOnly = val;

        public int GetOnly { get; } // Compliant {{Property is set during construction.}}

        public int InitOnly { get; init; } // Compliant {{Property can only be changed during the init phase.}}
    }

    public struct Struct
    {
        public Struct(int val) => GetOnly = val;

        public int GetOnly { get; } // Compliant {{Property is set during construction.}}
    }

    public ref struct RefStruct
    {
        public int Value { get; set; } // Compliant {{Ref structs are intended mutable.}}
    }

    [Mutable]
    public class DecoratedClass
    {
        public int Value { get; set; } // Compliant {{Class is decorated.}}
    }

    [Configuration]
    public class DecoratedWithDerivedClass
    {
        public int Value { get; set; } // Compliant {{Class is decorated.}}
    }

    [Obsolete]
    public class ObsoleteClass
    {
        public int Value { get; set; } // Compliant {{Class is obsolete.}}
    }

    public class ObsoleteProperty
    {
        [Obsolete]
        public int Value { get; set; } // Compliant {{Class is obsolete.}}
    }

    public class InheritsFromMutableClass : Noncompliant.Class
    {
        public int Value { get; set; } // Compliant {{Base class is mutable.}}
    }

    public class QowaivAggregate : Qowaiv.DomainModel.Aggregate<QowaivAggregate, int>
    {
        public QowaivAggregate() : base(42, null) { }

        public string Property { get; private set; } // Compliant {{Base class is Qowaiv Aggregate.}}
    }
}

namespace Noncompliant
{
    public class Class
    {
        public int PublicSet { get; set; } // Noncompliant {{Remove this setter, or make this property init-only.}}
        //                          ^^^^
        
        public int ProtectedSet { get; protected set; } // Noncompliant 
        //                             ^^^^^^^^^^^^^^

        public int PrivateSet { get; private set; } // Noncompliant

        protected int Protected { get; set; } // Noncompliant
        
        protected int ProtectedPrivateSet { get; private set; } // Noncompliant

        public int WithUnderlyingField
        {
            get => underlying;
            set => underlying = value; // Noncompliant
//          ^^^^^^^^^^^^^^^^^^^^^^^^^^
        }

        public int WithUnderlyingFieldClassic
        {
            get
            {
                return underlying;
            }
            set { underlying = value; }  // Noncompliant
//          ^^^^^^^^^^^^^^^^^^^^^^^^^^^
        }
        private int underlying;
    }

    public class Record
    {
        public int PublicSet { get; set; } // Noncompliant 
    }

    public class Struct
    {
        public int PublicSet { get; set; } // Noncompliant 
    }

    public interface Interface
    {
        int Value { get; set; } // Noncompliant
    }
}

public sealed class ConfigurationAttribute : MutableAttribute { }
public class MutableAttribute : Attribute { }
