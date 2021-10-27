Imports System
Imports Qowaiv

Public Class ParseShouldNotFail
    Public Sub Noncompliant()
        Dim invalidGuid = Guid.Parse("23432") ' Noncompliant {{Guid should contain 32 digits with 4 dashes (xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx).}}
        '                 ^^^^^^^^^^^^^^^^^^^
        Dim invalidNumber = Decimal.Parse("pindakaas") ' Noncompliant {{Input string was not in a correct format.}}
        Dim invalidPercentage = Percentage.Parse("45%432") ' Noncompliant {{Not a valid percentage}}
    End Sub

    Public Sub Compliant(str As String)
        Dim validParse = Guid.Parse("A2C6EEFC-02B9-4895-A97C-76AE27EC3C18") ' Compliant
        Dim notALitteral = Guid.Parse(str) ' Compliant
    End Sub
End Class

