<AttributeUsage(AttributeTargets.Property, _
 Inherited:=True, AllowMultiple:=True)> _
 Public Class JobHistory
    Inherits System.Attribute

    Private fConfiguredJobHistory As Integer
    Private fConfiguredJobHistoryPerJob As Integer
    Private fTotalHistorySize As Integer
    Private fJobData() As JobStepHistory
    Private fNumberOfJobs As Integer

    Public Property ConfiguredJobHistory() As Integer
        Get
            Return fConfiguredJobHistory
        End Get
        Set(ByVal value As Integer)
            fConfiguredJobHistory = value
        End Set
    End Property
    Public Property ConfigureJobHistoryPerJob() As Integer
        Get
            Return fConfiguredJobHistoryPerJob
        End Get
        Set(ByVal value As Integer)
            fConfiguredJobHistoryPerJob = value
        End Set
    End Property
    Public Property TotalHistorySize() As Integer
        Get
            Return fTotalHistorySize
        End Get
        Set(ByVal value As Integer)
            fTotalHistorySize = value
        End Set
    End Property
    Public Property JobData(ByVal job_guid As String) As JobStepHistory
        Get
            Dim LoopId As Integer = 0
            While LoopId <> fJobData.Length
                If job_guid = fJobData(LoopId).JobId Then Return fJobData(LoopId)
                LoopId += 1
            End While
            Return Nothing
        End Get
        Set(ByVal value As JobStepHistory)
            Dim LoopId As Integer = 0
            Dim Imported As Boolean = True
            While LoopId <> fJobData.Length
                If job_guid = fJobData(LoopId).JobId Then
                    fJobData(LoopId) = value
                    Imported = True
                End If
                LoopId += 1
            End While
            If Imported = False Then
                LoopId += 1
                fJobData(LoopId) = New JobStepHistory
                fJobData(LoopId) = value
            End If

        End Set
    End Property
    Public Sub InitalizeJobData(ByVal NumberOfJobs As Integer)
        Dim LoopCounter As Integer = 0
        While LoopCounter <> NumberOfJobs
            JobData(LoopCounter) = New JobStepHistory
            JobData(LoopCounter).MaxJobHistorySize = fConfiguredJobHistoryPerJob
            LoopCounter += 1
        End While
        fNumberOfJobs = NumberOfJobs
    End Sub
    Public ReadOnly Property NumberOfJobs() As Integer
        Get
            Return fNumberOfJobs
        End Get
    End Property
End Class

<AttributeUsage(AttributeTargets.Property, _
 Inherited:=True, AllowMultiple:=True)> _
 Public Class JobStepHistory
    Inherits System.Attribute

    Private fJobId As String
    Private fJobHistorySize As Integer
    Private fMaxJobHistorySize As Integer

    Public Property JobId() As String
        Get
            Return fJobId
        End Get
        Set(ByVal value As String)
            fJobId = value
        End Set
    End Property
    Public Property JobHistorySize() As Integer
        Get
            Return fJobHistorySize
        End Get
        Set(ByVal value As Integer)
            fJobHistorySize = value
        End Set
    End Property
    Public Property MaxJobHistorySize() As Integer
        Get
            Return fMaxJobHistorySize
        End Get
        Set(ByVal value As Integer)
            fMaxJobHistorySize = value
        End Set
    End Property
    Public ReadOnly Property RowsToDelete() As Integer
        Get
            If fJobHistorySize - fMaxJobHistorySize > 0 Then
                Return fJobHistorySize - fMaxJobHistorySize
            Else
                Return 0
            End If
        End Get
    End Property
End Class