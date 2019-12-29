<AttributeUsage(AttributeTargets.Property, _
 Inherited:=True, AllowMultiple:=True)> _
 Public Class Schedule
    Inherits System.Attribute

    Private fScheduleId As Integer
    Private fScheduleUid As Guid
    Private fScheduleName As String
    Private fenabled As Boolean
    Private ffreq_type As Integer
    Private ffreq_interval As Integer
    Private ffreq_subday_type As Integer
    Private ffreq_subday_interval As Integer
    Private ffreq_relative_interval As Integer
    Private ffreq_recurrence_factor As Integer
    Private factive_start_date As Integer
    Private factive_end_date As Integer
    Private factive_start_time As Integer
    Private factive_end_time As Integer
    Private fNextRunTime As Date

    Public Property NextRunTime() As Date
        Get
            Return fNextRunTime
        End Get
        Set(ByVal value As Date)
            fNextRunTime = value
        End Set
    End Property
    Public Property ScheduleId() As Integer
        Get
            Return fScheduleId
        End Get
        Set(ByVal value As Integer)
            fScheduleId = value
        End Set
    End Property
    Public Property ScheduleUid() As Guid
        Get
            Return fScheduleUid
        End Get
        Set(ByVal value As Guid)
            fScheduleUid = value
        End Set
    End Property
    Public Property ScheduleName() As String
        Get
            Return fScheduleName
        End Get
        Set(ByVal value As String)
            fScheduleName = value
        End Set
    End Property
    Public Property Enabled() As Boolean
        Get
            Return fenabled
        End Get
        Set(ByVal value As Boolean)
            fenabled = value
        End Set
    End Property
    Public Property freq_type() As Integer
        Get
            Return ffreq_type
        End Get
        Set(ByVal value As Integer)
            ffreq_type = value
        End Set
    End Property
    Public Property freq_interval() As Integer
        Get
            Return ffreq_interval
        End Get
        Set(ByVal value As Integer)
            ffreq_interval = value
        End Set
    End Property
    Public Property freq_subday_type() As Integer
        Get
            Return ffreq_subday_type
        End Get
        Set(ByVal value As Integer)
            ffreq_subday_type = value
        End Set
    End Property
    Public Property freq_subday_interval() As Integer
        Get
            Return ffreq_subday_interval
        End Get
        Set(ByVal value As Integer)
            ffreq_subday_interval = value
        End Set
    End Property
    Public Property freq_relative_interval() As Integer
        Get
            Return ffreq_relative_interval
        End Get
        Set(ByVal value As Integer)
            ffreq_relative_interval = value
        End Set
    End Property
    Public Property freq_recurrence_factor() As Integer
        Get
            Return ffreq_recurrence_factor
        End Get
        Set(ByVal value As Integer)
            ffreq_recurrence_factor = value
        End Set
    End Property
    Public Property active_start_date() As Integer
        Get
            Return factive_start_date
        End Get
        Set(ByVal value As Integer)
            factive_start_date = value
        End Set
    End Property
    Public Property active_end_date() As Integer
        Get
            Return factive_end_date
        End Get
        Set(ByVal value As Integer)
            factive_end_date = value
        End Set
    End Property
    Public Property active_start_time() As Integer
        Get
            Return factive_start_time
        End Get
        Set(ByVal value As Integer)
            factive_start_time = value
        End Set
    End Property
    Public Property active_end_time() As Integer
        Get
            Return factive_end_time
        End Get
        Set(ByVal value As Integer)
            factive_end_time = value
        End Set
    End Property

End Class
