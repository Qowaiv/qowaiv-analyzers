Imports NUnit.Framework
Imports CodeAnalysis.TestTools
Imports Qowaiv.CodeAnalysis

Namespace DecorateFunctions_specs

    Public Class Verify

        <Test>
        Public Sub VisualBasic()
            Dim rule As New DecorateFunctions()
            rule.ForVB().
                AddSource("Cases/DecorateFunctions.vb").
                AddReference(Of Qowaiv.Percentage).
                Verify()
        End Sub

    End Class

End Namespace
