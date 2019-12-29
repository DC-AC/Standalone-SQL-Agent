Imports System.Diagnostics
Imports System.Data
Imports System.Data.SqlClient
Imports System.Threading
Imports System.IO
Imports System.Xml
Imports System
Imports System.Security.Cryptography
Imports Standalone_SQL_Agent_Service.UpdateScheduleRunTimes
Imports Standalone_SQL_Agent_Service.SQLInstance
Imports SSA.CommonControl
Imports SSA.CommonControl.SharedCode
Imports SSA.CommonControl.ConfigFile
Public Class BaseCode
    Public Server(3) As String
    Public ServerCount As Integer = 0
    Shared ShutdownThreads As Boolean = False
    Public Shared DebugLevel As Integer = 11
    Private Sub PrepLogFiles(ByVal FilesToKeep As Integer)

        DeleteFile(GetInstallPath() + "ERRORLOG." + FilesToKeep.ToString)
        While FilesToKeep <> 0
            RenameFile(GetInstallPath() + "ERRORLOG." + (FilesToKeep - 1).ToString, GetInstallPath() + "ERRORLOG." + FilesToKeep.ToString)
            FilesToKeep -= 1
        End While
        RenameFile(GetInstallPath() + "ERRORLOG", GetInstallPath() + "ERRORLOG.1")

    End Sub
    Shared Sub DeleteFile(ByVal FileName As String)
        Try
            If File.Exists(FileName) Then File.Delete(FileName)
        Catch ex As Exception
            Debug.Print(ex.Message)
        End Try

    End Sub
    Private Sub RenameFile(ByVal OldFileNAme As String, ByVal NewFileName As String)
        Try
            If File.Exists(NewFileName) Then DeleteFile(NewFileName)
            If File.Exists(OldFileNAme) Then File.Move(OldFileNAme, NewFileName)
        Catch ex As Exception
            Debug.Print(ex.Message)
        End Try

    End Sub
    Protected Overrides Sub OnStart(ByVal args() As String)

        'Eventually I'll make this a setting somewhere.
        SharedCode.DebugLevel = DebugLevel
        PrepLogFiles(6)

        WriteErrorLog("Starting Standalone SQL Agent.", 1, EventLogEntryType.Information)
        ReadConfigFile()

        StartInstances()

    End Sub
    Private Sub StartInstances()
        Dim i As Integer = 0
        Dim connectionString As String
        Dim NewInstance As New SQLInstance()

        While i <= My.Settings("InstanceCount")

            connectionString = ReturnConnectionString(i)

            If connectionString.Contains("AppName") Then
                connectionString = connectionString.Replace(connectionString.Substring(connectionString.IndexOf("Application Name"), connectionString.IndexOf(";", connectionString.IndexOf("Application Name")) - connectionString.IndexOf("Application Name")), "Application Name='Standalone SQL Agent';")
            Else
                connectionString = connectionString + "Application Name='Standalone SQL Agent';"
            End If

            NewInstance.connectionString = connectionString
            NewInstance.StartDeligate = AddressOf NewInstance.BaseInstanceThread
            'NewJob.StartJobDelegate = AddressOf NewJob.StartJob

            Dim InstanceThread As New Thread(AddressOf NewInstance.StartInstance)
            InstanceThread.IsBackground = True
            InstanceThread.Start()

            i += 1
        End While
    End Sub
    Protected Overrides Sub OnStop()
        ' Add code here to perform any tear-down necessary to stop your service
        WriteErrorLog("Shutting down Standalone SQL Agent", 1, EventLogEntryType.Information)
        ShutdownThreads = True
    End Sub
    Private Sub ReadConfigFile()
        If File.Exists(System.AppDomain.CurrentDomain.BaseDirectory + "agent.config") Then
            Dim objReader As StreamReader
            Dim ConnString As String = Nothing
            Try
                objReader = New StreamReader(System.AppDomain.CurrentDomain.BaseDirectory + "agent.config")
                While objReader.EndOfStream = False

                    ConnString = objReader.ReadLine
                    Server.SetValue(ConnString, ServerCount)
                    WriteErrorLog(ConnString, 11, EventLogEntryType.Information)
                    'Server(ServerCount) = ConnString
                    ServerCount += 1
                End While

                objReader.Close()
            Catch Ex As Exception
                WriteErrorLog(Ex.Message, 2, EventLogEntryType.Warning)
                WriteErrorLog("Failed on " + ServerCount.ToString, 2, EventLogEntryType.Warning)
            End Try
        Else
            WriteErrorLog("Config File Not Found.", 1, EventLogEntryType.Error)
            End
        End If
        WriteErrorLog("Config File Read.  " + ServerCount.ToString + " servers found.", 2, EventLogEntryType.Information)
    End Sub
End Class




