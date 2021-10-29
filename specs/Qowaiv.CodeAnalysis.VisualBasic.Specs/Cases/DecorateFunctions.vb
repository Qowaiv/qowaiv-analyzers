Imports System
Imports System.Diagnostics.Contracts

Public Class Noncompliant

    Public Function PureFunction() As Integer ' Noncompliant {{Decorate this method with a [Pure] or [Impure] attribute.}}
        Return 42
    End Function

    Public Function PureFunction(scale) As Integer ' Noncompliant
        Return scale * 42
    End Function

    <OtherAttribute>
    Public Function DecoratedOtherwise() As Integer ' Noncompliant@-1
        Return 42
    End Function
End Class

Public Class Compliant
    <Pure>
    Public Function PureFunction() As Integer
        Return 42
    End Function

    <Obsolete>
    Public Function Obsolete() As Integer ' Compliant {{Obsolete methods are ignored.}}
        Return 0
    End Function

    Public Sub Void() 'Compliant {{Void methods are impure per definition.}}
    End Sub

    Public Function Scope() As IDisposable ' Compliant {{Disposable methods are expected To be impure.}}
        Return Nothing
    End Function

    Public Function TryParse(str As String, ByRef result As Object) As Boolean ' Compliant {{Methods With out parameters are expected To be impure.}}
        If str Is Nothing Then
            result = Nothing
            Return False
        Else
            result = str
            Return True
        End If
    End Function

    <Impure>
    Public Function ImpureMethod(input As Integer) As Integer ' Compliant {{Decorated With something derived from an ImpureAttribute.}}
        Return 69
    End Function

    <FluenSyntax>
    Public Function FluentSyntax() As Compliant ' Compliant {{Decorated With something derived from an ImpureAttribute.}}
        Return Me
    End Function
End Class

Public Class OtherAttribute
    Inherits Attribute
End Class
Public Class ImpureAttribute
    Inherits Attribute
End Class
Public Class FluenSyntaxAttribute
    Inherits ImpureAttribute
End Class
