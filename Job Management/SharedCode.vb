Imports SSA.CommonControl

Module SharedCode

End Module
<AttributeUsage(AttributeTargets.Property, _
 Inherited:=True, AllowMultiple:=True)> _
 Public Class Instance
    Inherits System.Attribute

    Private fInstanceName As String
    Private fInstanceVersion As Version
    Private fInstanceWindowsAuth As Boolean
    Private fInstanceUserName As String
    Private fInstancePassword As String
    Private fJobs() As Job

    Public Property InstanceName() As String
        Get
            Return fInstanceName
        End Get
        Set(ByVal value As String)
            fInstanceName = value
        End Set
    End Property
    Public Property InstanceVersion() As Version
        Get
            Return fInstanceVersion
        End Get
        Set(ByVal value As Version)
            fInstanceVersion = value
        End Set
    End Property
    Public Property InstanceWindowsAuth() As Boolean
        Get
            Return fInstanceWindowsAuth
        End Get
        Set(ByVal value As Boolean)
            fInstanceWindowsAuth = value
        End Set
    End Property
    Public Property InstanceUserName() As String
        Get
            Return fInstanceUserName
        End Get
        Set(ByVal value As String)
            fInstanceUserName = value
        End Set
    End Property
    Public Property InstancePassword() As String
        Get
            Return fInstancePassword
        End Get
        Set(ByVal value As String)
            fInstancePassword = value
        End Set
    End Property
    Public ReadOnly Property ConnectionString() As String
        Get
            Dim ConnString As String
            ConnString = "Server=" + fInstanceName + ";Database=msdb;"
            If fInstanceWindowsAuth = True Then
                ConnString += "Trusted_Connection=True;"
            Else
                ConnString += "UserName=" + fInstanceUserName + ";Password=" + fInstancePassword + ";"
            End If
            Return ConnString
        End Get
    End Property
    Public Sub PrepareJobs(ByVal NumberOfJobs As Integer)
        Dim LoopCounter As Integer = 0
        Do Until LoopCounter > NumberOfJobs
            fJobs(LoopCounter) = New Job
            LoopCounter += 1
        Loop
    End Sub
    Public Property Job(ByVal job_guid As String) As Job
        Get
            Dim aJob As Job
            For Each aJob In fJobs
                If fJobs(aJob.job_id).job_id = job_guid Then Return aJob
            Next
        End Get
        Set(ByVal value As Job)

        End Set
    End Property
End Class