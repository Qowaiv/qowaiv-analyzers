using Qowaiv;
using System;
using System.Collections.Generic;

class Compliant
{
    public List<Guid> Ids { get; } //                           Compliant
    public Dictionary<Guid, string> Lookup { get; } //          Compliant
    public Guid[] Array { get; } //                             Compliant
    public Dictionary<Guid, EmailAddress> Addresses { get; } // Compliant
}


class Noncompliant
{
    public List<Guid?> Ids { get; } //                  Noncompliant
    public Dictionary<Guid?, string> Lookup { get; } // Noncompliant
    public Guid?[] Array { get; } //                    Noncompliant
    public Guid?[][] JaggedArray { get; } //            Noncompliant
    public Guid?[,] MultidimensionalArray { get; } //   Noncompliant
    public Dictionary<Guid?, EmailAddress?> Addresses { get; }
    //                ^^^^^
    //                       ^^^^^^^^^^^^^ @-1  
}
