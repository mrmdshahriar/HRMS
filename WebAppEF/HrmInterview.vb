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

Partial Public Class HrmInterview
    Public Property Id As Integer
    Public Property HrmApplyFormId As Nullable(Of Integer)
    Public Property HrmJobPostId As Nullable(Of Integer)
    Public Property Result As String
    Public Property TotalScore As Nullable(Of Integer)
    Public Property TotalEarned As Nullable(Of Integer)
    Public Property CreatedBy As String
    Public Property CreatedOn As Nullable(Of Date)
    Public Property LastModifiedBy As String
    Public Property LastModifiedOn As Nullable(Of Date)
    Public Property IsDeleted As Nullable(Of Boolean)
    Public Property HrmEmployee_Id As Nullable(Of Integer)

    Public Overridable Property HrmEmployee As HrmEmployee
    Public Overridable Property HrmInterviewDetails As ICollection(Of HrmInterviewDetail) = New HashSet(Of HrmInterviewDetail)
    Public Overridable Property HrmJobPost As HrmJobPost

End Class
