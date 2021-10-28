Imports NUnit.Framework
Imports CodeAnalysis.TestTools
Imports Qowaiv.CodeAnalysis

Namespace UseTestableTimeProvider_specs

    Public Class Verify

        <Test>
        Public Sub VisualBasic()
            Dim rule As New UseTestableTimeProvider()
            rule.ForVB().
                AddSource("Cases\UseTestableTimeProvider.vb").
                AddReference(Of Qowaiv.Percentage).
                Verify()
        End Sub

    End Class

End Namespace
