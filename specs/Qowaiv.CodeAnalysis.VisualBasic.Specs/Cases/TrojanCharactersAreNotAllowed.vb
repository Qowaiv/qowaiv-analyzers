Class Controller
    Function Access() As Boolean

        Dim access_level As String = "user"
        If (access_level <> "⁣user") Then ' Noncompliant {{Trojan Horse character U+2063 detected.}}
            '                ^
            Return True
        End If
        Return False
    End Function
End Class
