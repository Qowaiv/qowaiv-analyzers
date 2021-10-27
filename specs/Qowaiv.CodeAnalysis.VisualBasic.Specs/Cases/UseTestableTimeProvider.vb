﻿Imports System

Public Class DateTimeAsProvider

    Public Sub Noncompliant()
        Dim now As Date = DateTime.Now ' Noncompliant {{Use a testable (date) time provider instead.}}
        '                 ^^^^^^^^^^^^
        Dim utc As Date = DateTime.UtcNow ' Noncompliant
        Dim today As Date = DateTime.Today ' Noncompliant
        Dim _date As Date = Date.Now ' Noncompliant
    End Sub

    Public Sub CompliantAre()
        Dim other As Integer = DateTime.DaysInMonth(2000, 2) ' Compliant
        Dim noSystemDateTime As NotSystem.DateTime = NotSystem.DateTime.Now ' Compliant
        Dim noSystemtDate As NotSystem.Date = NotSystem.Date.Now ' Compliant
    End Sub
End Class

Namespace NotSystem
    Public Class DateTime
        Public Shared ReadOnly Property Now As DateTime
            Get
                Return New DateTime()
            End Get
        End Property
    End Class
    Public Class [Date]
        Public Shared ReadOnly Property Now As NotSystem.Date
            Get
                Return New NotSystem.Date()
            End Get
        End Property
    End Class
End Namespace
