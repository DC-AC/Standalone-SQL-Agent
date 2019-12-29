Imports SSA.CommonControl.SharedCode
<AttributeUsage(AttributeTargets.Property, _
 Inherited:=True, AllowMultiple:=True)> _
 Public Class JobServer
    Inherits System.Attribute

    Private fInstanceName As String
    Private fDatabaseName As String
    Private fAuthenticationMethod As InstanceAuthenticationMethod
    Private fUserName As String
    Private fPassword As String

    Public Property InstanceName() As String
        Get
            Return fInstanceName
        End Get
        Set(ByVal value As String)
            fInstanceName = value
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
    Public Property AuthenticationMethod() As InstanceAuthenticationMethod
        Get
            Return fAuthenticationMethod
        End Get
        Set(ByVal value As InstanceAuthenticationMethod)
            fAuthenticationMethod = value
        End Set
    End Property
    Public Property UserName() As String
        Get
            Return fUserName
        End Get
        Set(ByVal value As String)
            fUserName = value
        End Set
    End Property
    Public Property Password() As String
        Get
            Return fPassword
        End Get
        Set(ByVal value As String)
            fPassword = value
        End Set
    End Property
    Public ReadOnly Property ConnectionString() As String
        Get
            Return FormatconnectionString(fInstanceName, fDatabaseName, fAuthenticationMethod, fUserName, fPassword)
        End Get
    End Property
    Public Enum InstanceAuthenticationMethod
        WindowsAuthentication = 1
        SQLAuthentication = 2
    End Enum
End Class
