'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated from a template.
'
'     Manual changes to this file may cause unexpected behavior in your application.
'     Manual changes to this file will be overwritten if the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Imports System
Imports System.Collections.Generic

Partial Public Class HrmAttendance
    Public Property Id As Integer
    Public Property [Date] As Nullable(Of Date)
    Public Property TimeIn As Nullable(Of Date)
    Public Property TimeOut As Nullable(Of Date)
    Public Property IsPresent As Nullable(Of Boolean)
    Public Property IsAbsent As Nullable(Of Boolean)
    Public Property IsLeave As Nullable(Of Boolean)
    Public Property LeaveType As String
    Public Property IsHoliday As Nullable(Of Boolean)
    Public Property Holiday As String
    Public Property IsHalfDay As Nullable(Of Boolean)
    Public Property IsLate As Nullable(Of Boolean)
    Public Property IsEarly As Nullable(Of Boolean)
    Public Property Department As Nullable(Of Integer)
    Public Property Employee As Nullable(Of Integer)
    Public Property Month As String

End Class
