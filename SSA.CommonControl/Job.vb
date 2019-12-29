Imports System.Diagnostics
<AttributeUsage(AttributeTargets.Property, _
 Inherited:=True, AllowMultiple:=True)> _
 Public Class Job
    Inherits System.Attribute

    Private fjob_id As String
    Private fjob_name As String
    Private fEnabled As Boolean
    Private fDelete_level As Integer
    Private fJobCreated As Date
    Private fJobModified As Date
    Private fDescription As String
    Private fStartStepId As Integer
    Private fJobStepCount As Integer
    Private fJobOwner As String
    Private fJobCategory As String
    Private fJobSteps(1000) As JobStep
    Private fSchedule(1000) As Schedule
    Private fNextRunDate As Date
    Private fnotify_level_eventlog As NotifyLevel
    Private fnotify_level_email As NotifyLevel
    Private fnotify_level_netsend As NotifyLevel
    Private fnotify_level_page As NotifyLevel
    Private fnotify_level_delete As NotifyLevel
    Private fnotify_email_operator As String
    Private fnotify_netsend_operator As String
    Private fnotify_page_operator As String
    Public ReadOnly Property JobStepNameExists(ByVal JobStepName As String)
        Get
            Dim JobStepId As Integer = 1
            While JobStepId <= JobStepCount
                If JobStep(JobStepId).JobStepName = JobStepName Then Return True
                JobStepId += 1
            End While
            Return False
        End Get
    End Property
    Public Property Delete_Level() As Integer
        Get
            Return fDelete_level
        End Get
        Set(ByVal value As Integer)
            fDelete_level = value
        End Set
    End Property
    Public Property Notify_Page_Operator() As String
        Get
            Return fnotify_page_operator
        End Get
        Set(ByVal value As String)
            fnotify_page_operator = value
        End Set
    End Property
    Public Property Nofify_Netsend_Operator() As String
        Get
            Return fnotify_netsend_operator
        End Get
        Set(ByVal value As String)
            fnotify_netsend_operator = value
        End Set
    End Property
    Public Property Notify_Email_Operator() As String
        Get
            Return fnotify_email_operator
        End Get
        Set(ByVal value As String)
            fnotify_email_operator = value
        End Set
    End Property
    Public Property Nofify_Level_Page() As NotifyLevel
        Get
            Return fnotify_level_page
        End Get
        Set(ByVal value As NotifyLevel)
            fnotify_level_page = value
        End Set
    End Property
    Public Property Notify_Level_Netsend() As NotifyLevel
        Get
            Return fnotify_level_netsend
        End Get
        Set(ByVal value As NotifyLevel)
            fnotify_level_netsend = value
        End Set
    End Property
    Public Property Notify_Level_Email() As NotifyLevel
        Get
            Return fnotify_level_email
        End Get
        Set(ByVal value As NotifyLevel)
            fnotify_level_email = value
        End Set
    End Property
    Public Property Notify_Level_EventLog() As NotifyLevel
        Get
            Return fnotify_level_eventlog
        End Get
        Set(ByVal value As NotifyLevel)
            fnotify_level_eventlog = value
        End Set
    End Property
    Public Property Notify_Level_Delete() As NotifyLevel
        Get
            Return fnotify_level_delete
        End Get
        Set(ByVal value As NotifyLevel)
            fnotify_level_delete = value
        End Set
    End Property
    Public Property Schedule(ByVal ScheduleId As Integer) As Schedule
        Get
            Return fSchedule(ScheduleId)
        End Get
        Set(ByVal value As Schedule)
            If fSchedule(value.ScheduleId) Is Nothing Then fSchedule(value.ScheduleId) = New Schedule
            fSchedule(value.ScheduleId) = value
        End Set
    End Property
    Public Property NextRunDate() As Date
        Get
            Return fNextRunDate
        End Get
        Set(ByVal value As Date)
            If fNextRunDate <> Nothing Then
                If fNextRunDate > value Then
                    fNextRunDate = value
                End If
            Else
                fNextRunDate = value
            End If
        End Set
    End Property
    Public Function AddJobStep()
        Dim JobStepId As Integer
        JobStepId = JobStepCount + 1

        fJobSteps(JobStepId) = New JobStep
        fJobStepCount = JobStepId

        Return JobStepId
    End Function
    Public Sub PrepareJobSteps(ByVal NumberOfSteps As Integer)
        Dim LoopCount As Integer = 0
        While LoopCount < fJobSteps.Length
            fJobSteps(LoopCount) = Nothing
            LoopCount += 1
        End While
        LoopCount = 0
        While LoopCount < NumberOfSteps 'fJobSteps.GetLength(0)
            fJobSteps(LoopCount) = New JobStep
            LoopCount += 1
        End While
        fJobStepCount = NumberOfSteps
    End Sub
    Public ReadOnly Property JobStepCount() As Integer
        Get
            Return fJobStepCount
        End Get
    End Property
    Public ReadOnly Property JobStepExists(ByVal JobStepId As Integer) As Boolean
        Get
            If JobStepId >= 1 And JobStepId <= fJobStepCount Then
                Return True
            Else
                Return False
            End If
        End Get
    End Property
    Public Property job_name() As String
        Get
            Return fjob_name
        End Get
        Set(ByVal value As String)
            fjob_name = value
        End Set
    End Property
    Public Property job_id() As String
        Get
            Return fjob_id
        End Get
        Set(ByVal value As String)
            fjob_id = value
        End Set
    End Property
    Public Property JobOwner() As String
        Get
            Return fJobOwner
        End Get
        Set(ByVal value As String)
            fJobOwner = value
        End Set
    End Property
    Public Property JobCategory() As String
        Get
            Return fJobCategory
        End Get
        Set(ByVal value As String)
            fJobCategory = value
        End Set
    End Property
    Public Property StartStepId() As Integer
        Get
            Return fStartStepId
        End Get
        Set(ByVal value As Integer)
            fStartStepId = value
        End Set
    End Property
    Public Property JobStep(ByVal JobStepId As Integer) As JobStep
        Get
            Return fJobSteps(JobStepId)
        End Get
        Set(ByVal value As JobStep)
            fJobSteps(JobStepId) = New JobStep
            fJobSteps(JobStepId) = value
        End Set
    End Property
    Public Property Enabled() As Boolean
        Get
            Return fEnabled
        End Get
        Set(ByVal value As Boolean)
            fEnabled = value
        End Set
    End Property
    Public Property Description() As String
        Get
            Return fDescription
        End Get
        Set(ByVal value As String)
            fDescription = value
        End Set
    End Property
    Public Sub Clear()

    End Sub
    Public Property CreateDate() As Date
        Get
            Return fJobCreated
        End Get
        Set(ByVal value As Date)
            fJobCreated = value
        End Set
    End Property
    Public Property ModifyDate() As Date
        Get
            Return fJobModified
        End Get
        Set(ByVal value As Date)
            fJobModified = value
        End Set
    End Property
    Public Function HasSchedule(ByVal ScheduleId As Integer)
        If Schedule(ScheduleId) Is Nothing Then
            Debug.Print("Not included")
            Return False
        Else
            Debug.Print("Included")
            Return True
        End If
    End Function
    Public Enum NotifyLevel
        Never = 0
        Success = 1
        Failure = 2
        Always = 3
    End Enum
End Class