using System;
using System.Collections;
using System.Collections.Generic;

namespace Compliant
{
    internal class InternalClass
    {
        public List<int> Value { get; set; } // Compliant {{Internal classes are ignored.}}
    }

    internal record InternalRecord
    {
        public List<int> Value { get; set; } // Compliant {{Internal records are ignored.}}
    }

    internal struct InternalStruct
    {
        public List<int> Value { get; set; } // Compliant {{Internal structs are ignored.}}
    }

    public class Class
    {
        public string String { get; init; } // Compliant {{Strings are immutable.}}

        public ImmutableClass ImmutableClass { get; } // Compliant

        public IReadOnlyCollection<int> Collection { get; } // Compliant

        public IReadOnlyDictionary<string, int> Dictionary { get; } // Compliant

        public IReadOnlyList<int> List { get; } // Compliant
        
        public IReadOnlySet<int> Set { get; } // Compliant

        public ImmutableList ImmutableList { get; } // Compliant
        
        public RecuriveImmutableClass Recurive { get; } // Compliant

        public Guid ReadOnly { get; } // Compliant {{Guids are read-only.}}
    }

    public record Record
    {
        public ImmutableClass ImmutableClass { get; } // Compliant
    }

    public struct Struct
    {
        public ImmutableClass ImmutableClass { get; } // Compliant
    }

    public interface Interface
    {
        ImmutableClass ImmutableClass { get; } // Compliant
    }

    public ref struct RefStruct
    {
        public List<int> Value { get; set; } // Compliant {{Ref structs are intended mutable.}}
    }

    [Mutable]
    public class DecoratedClass
    {
        public List<int> Value { get; set; } // Compliant {{Class is decorated.}}
    }

    [Configuration]
    public class DecoratedWithDerivedClass
    {
        public List<int> Value { get; set; } // Compliant {{Class is decorated.}}
    }

    [Obsolete]
    public class ObsoleteClass
    {
        public List<int> Value { get; set; } // Compliant {{Class is obsolete.}}
    }

    public class ObsoleteProperty
    {
        [Obsolete]
        public List<int> Value { get; set; } // Compliant {{Class is obsolete.}}
    }
}

namespace Noncompliant
{
    public class Class
    {
        public int[] Array { get; } // Noncompliant {{Use an immutable type.}}
        //     ^^^^^
        public ICollection<int> ICollection { get; } // Noncompliant
        //     ^^^^^^^^^^^^^^^^

        public IList<int> IList { get; } // Noncompliant
        
        public ISet<int> Set { get; } // Noncompliant
        
        public IDictionary<string, int> IDictionary { get; } // Noncompliant

        public List<int> List { get; } // Noncompliant

        public HashSet<int> HashSet { get; } // Noncompliant
        
        public Dictionary<string, int> Dictionary { get; } // Noncompliant

        public MutableClass MutableClass { get; } // Noncompliant

        public IReadOnlyList<MutableClass> MutableClasses { get; } // Noncompliant

        public MutableList MutableList { get; } // Noncompliant, MutableList inherits from IList<T>.

        public WithMutableProps WithMutableProps { get; } // Noncompliant
    }

    public record Record
    {
        public MutableClass MutableClass { get; } // Noncompliant
    }

    public struct Struct
    {
        public MutableClass MutableClass { get; } // Noncompliant
    }

    public interface Interface
    {
        MutableClass MutableClass { get; } // Noncompliant
    }
}

public class ImmutableClass 
{
    public int Value { get; init; }
}

public class RecuriveImmutableClass
{
    public RecuriveImmutableClass2 Parent { get; init; }
}

public class RecuriveImmutableClass2
{
    public RecuriveImmutableClass Parent { get; init; }
}

[Mutable]
public class MutableClass { }

public class WithMutableProps
{
    public int Value { get; set; }
}

public sealed class MutableList : List<int> { }

public sealed class ImmutableList : IReadOnlyList<int>
{
    public int this[int index] => 42;

    public int Count => 42;

    public IEnumerator<int> GetEnumerator()
    {
        yield return 42;
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

public sealed class ConfigurationAttribute : MutableAttribute { }
public class MutableAttribute : Attribute { }

