<AttributeUsage(AttributeTargets.Property, _
 Inherited:=True, AllowMultiple:=True)> _
 Public Class JobGuidList
    Inherits System.Attribute

    Dim JobList(10000) As String
    Dim JobListId As Integer = 0

    Public Function AddGuid(ByVal JobGuid As String) As String

        Dim JobId As Integer
        JobId = GetNextJobId()
        JobList.SetValue(JobGuid, JobId)
        Return JobId

    End Function
    Public Function GetJobId(ByVal JobGuid As String) As Integer
        Return FindJobId(JobGuid)
    End Function
    Public Function GetJobGuid(ByVal JobId As Integer) As String
        Return (JobList(JobId))
    End Function
    Public Sub DeleteJobGuid(ByVal JobGuid As String)

        Dim JobId As Integer
        JobId = FindJobId(JobGuid)
        JobList(JobId) = Nothing

    End Sub

    Private Function FindJobId(ByVal JobGuid As String)
        Dim i As Integer = 0
        Do Until i = JobList.Length
            If JobList(i) = JobGuid Then Return i
            i += 1
        Loop
        Return Nothing
    End Function
    Private Function GetNextJobId()
        JobListId += 1
        Return JobListid


    End Function
End Class

