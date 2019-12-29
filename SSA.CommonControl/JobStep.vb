Imports SSA.CommonControl.Enums
<AttributeUsage(AttributeTargets.Property, _
 Inherited:=True, AllowMultiple:=True)> _
 Public Class JobStep
    Inherits System.Attribute

    Private fJobStepId As Integer
    Private fJobStepName As String
    Private fJobStepOnSuccess As JobStepAction
    Private fJobStepOnFailure As JobStepAction
    Private fJobStepOnSuccessStepId As Integer
    Private fJobStepOnFailureStepId As Integer
    Private fJobStepType As String
    Private fJobStepRetryAttempts As Integer
    Private fJobStepRetryInterval As Integer
    Private fDatabaseName As String
    Private fJobStepCommand As String
    Private fJobStepOutputPath As String
    Private fJobStepAdvancedFlags As Integer
    Private fJobStepOutputPathAppendFlag As Boolean
    Private finstance_id As Integer
    Private fOsCommandExpectedReturnValue As Integer
    Private fIsNewStep As Boolean
    Private fDeleteStep As Boolean

    Public Property DeleteStep() As Boolean
        Get
            Return fDeleteStep
        End Get
        Set(ByVal value As Boolean)
            fDeleteStep = value
        End Set
    End Property
    Public Property IsNewStep() As Boolean
        Get
            Return fIsNewStep
        End Get
        Set(ByVal value As Boolean)
            fIsNewStep = value
        End Set
    End Property

    Public Property JobStepRetryAttempts() As Integer
        Get
            Return fJobStepRetryAttempts
        End Get
        Set(ByVal value As Integer)
            fJobStepRetryAttempts = value
        End Set
    End Property
    Public Property JobStepRetryInterval() As Integer
        Get
            Return fJobStepRetryInterval
        End Get
        Set(ByVal value As Integer)
            fJobStepRetryInterval = value
        End Set
    End Property
    Public ReadOnly Property JobStepOutputPathAppendFlag() As Boolean
        Get
            If (fJobStepAdvancedFlags And 2) = 2 Then
                Return True
            Else
                Return False
            End If
        End Get
    End Property
    Public Property JobStepAdvancedFlags() As Integer
        Get
            Return fJobStepAdvancedFlags
        End Get
        Set(ByVal value As Integer)
            fJobStepAdvancedFlags = value
        End Set
    End Property
    Public Property JobStepName() As String
        Get
            Return fJobStepName
        End Get
        Set(ByVal value As String)
            fJobStepName = value
        End Set
    End Property
    Public Property instance_id() As Integer
        Get
            Return finstance_id
        End Get
        Set(ByVal value As Integer)
            finstance_id = value
        End Set
    End Property
    Public Property DatabaseName() As String
        Get
            Return fDatabaseName
        End Get
        Set(ByVal value As String)
            fDatabaseName = value
        End Set
    End Property
    Public Property JobStepCommand() As String
        Get
            Return fJobStepCommand
        End Get
        Set(ByVal value As String)
            fJobStepCommand = value
        End Set
    End Property
    Public Property JobStepId() As Integer
        Get
            Return fJobStepId
        End Get
        Set(ByVal value As Integer)
            fJobStepId = value
        End Set
    End Property
    Public Property JobStepOnSuccess() As JobStepAction
        Get
            Return fJobStepOnSuccess
        End Get
        Set(ByVal value As JobStepAction)
            fJobStepOnSuccess = value
        End Set
    End Property
    Public Property JobStepOnFailure() As JobStepAction
        Get
            Return fJobStepOnFailure
        End Get
        Set(ByVal value As JobStepAction)
            fJobStepOnFailure = value
        End Set
    End Property
    Public Property JobStepOnSuccessStepId() As Integer
        Get
            Return fJobStepOnSuccessStepId
        End Get
        Set(ByVal value As Integer)
            fJobStepOnSuccessStepId = value
        End Set
    End Property
    Public Property JobStepOnFailureStepId() As Integer
        Get
            Return fJobStepOnFailureStepId
        End Get
        Set(ByVal value As Integer)
            fJobStepOnFailureStepId = value
        End Set
    End Property
    Public Property JobStepType() As JobStepTypes
        Get
            Return fJobStepType
        End Get
        Set(ByVal value As JobStepTypes)
            fJobStepType = value
        End Set
    End Property
    Public Property JobStepOutputPath() As String
        Get
            Return fJobStepOutputPath
        End Get
        Set(ByVal value As String)
            fJobStepOutputPath = value
        End Set
    End Property

    Public Property OsCommandExpectedReturnValue() As Integer
        Get
            Return fOsCommandExpectedReturnValue
        End Get
        Set(ByVal value As Integer)
            fOsCommandExpectedReturnValue = OsCommandExpectedReturnValue
        End Set
    End Property

    Public Enum JobStepTypes
        TSQL = 1
        OperationSystemCommand = 2

    End Enum


End Class