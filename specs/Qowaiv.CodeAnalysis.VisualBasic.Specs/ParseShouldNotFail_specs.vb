Imports NUnit.Framework
Imports CodeAnalysis.TestTools
Imports Qowaiv.CodeAnalysis

Namespace ParseShouldNotFail_specs

    Public Class Verify

        <Test>
        Public Sub VisualBasic()
            Dim rule As New ParseShouldNotFail()
            rule.ForVB().
                AddSource("Cases/ParseShouldNotFail.vb").
                AddReference(Of Qowaiv.Percentage).
                Verify()
        End Sub

    End Class

End Namespace
