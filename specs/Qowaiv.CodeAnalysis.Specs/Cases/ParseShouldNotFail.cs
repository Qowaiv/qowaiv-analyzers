using System;
using Qowaiv;

public class ParseShouldNotFail
{
    public void Noncompliant()
    {
        var invalidGuid = Guid.Parse("23432"); // Noncompliant {{Guid should contain 32 digits with 4 dashes (xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx).}}
        //                ^^^^^^^^^^^^^^^^^^^
        var invalidNumber = decimal.Parse("pindakaas"); // Noncompliant {{Input string was not in a correct format.}}
        var invalidPercentage = Percentage.Parse("45%432"); // Noncompliant {{Not a valid percentage}}
    }

    public void Compliant(string str)
    {
        var validParse = Guid.Parse("A2C6EEFC-02B9-4895-A97C-76AE27EC3C18"); // Compliant
        var notALiteral = Guid.Parse(str); // Compliant
        var unknownClass = UnknownClass.Parse("literal"); // Error [CS0103]
    }
}
