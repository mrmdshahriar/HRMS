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

Partial Public Class AspNetRole
    Public Property Id As String
    Public Property RoleCategoryId As Nullable(Of Integer)
    Public Property CreatedBy As String
    Public Property CreatedOn As Date
    Public Property IsDeleted As Boolean
    Public Property LastModifiedBy As String
    Public Property LastModifiedOn As Date
    Public Property Name As String

    Public Overridable Property AspNetUserRoles As ICollection(Of AspNetUserRole) = New HashSet(Of AspNetUserRole)
    Public Overridable Property HrmEmployees As ICollection(Of HrmEmployee) = New HashSet(Of HrmEmployee)

End Class
