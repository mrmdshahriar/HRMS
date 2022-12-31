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

Partial Public Class Country
    Public Property Id As Integer
    Public Property Name As String
    Public Property RegionId As Nullable(Of Integer)
    Public Property Active As Nullable(Of Boolean)
    Public Property CreatedBy As String
    Public Property CreatedOn As Nullable(Of Date)
    Public Property LastModifiedBy As String
    Public Property LastModifiedOn As Nullable(Of Date)
    Public Property IsDeleted As Nullable(Of Boolean)
    Public Property Code As String
    Public Property CountryCode As String
    Public Property CountryPhoneCode As String

    Public Overridable Property Region As Region
    Public Overridable Property HrmEmployees As ICollection(Of HrmEmployee) = New HashSet(Of HrmEmployee)
    Public Overridable Property HrmEmployees1 As ICollection(Of HrmEmployee) = New HashSet(Of HrmEmployee)
    Public Overridable Property HrmEmployees2 As ICollection(Of HrmEmployee) = New HashSet(Of HrmEmployee)
    Public Overridable Property HrmEmployees3 As ICollection(Of HrmEmployee) = New HashSet(Of HrmEmployee)

End Class
