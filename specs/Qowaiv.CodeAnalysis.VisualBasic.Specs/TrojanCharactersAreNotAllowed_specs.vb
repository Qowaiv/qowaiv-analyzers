Imports NUnit.Framework
Imports CodeAnalysis.TestTools
Imports Qowaiv.CodeAnalysis

Namespace TrojanCharactersAreNotAllowed_specs

    Public Class Verify

        <Test>
        Public Sub VisualBasic()
            Dim rule As New TrojanCharactersAreNotAllowed()
            rule.ForVB().
                AddSource("Cases/TrojanCharactersAreNotAllowed.vb").
                Verify()
        End Sub

    End Class

End Namespace
