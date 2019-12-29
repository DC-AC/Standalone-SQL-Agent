Imports System.IO
Imports System.Data
Imports System.Data.SqlClient

Public Class SharedCode
    Public Shared Version As System.Version = My.Application.Info.Version
    Public Shared DebugLevel As Integer
    Public Overloads Shared Sub WriteErrorLog(ByVal Message As String)
        WriteErrorLog(Message, 1, EventLogEntryType.Information)
    End Sub
    Public Overloads Shared Sub writeErrorLog(ByVal Message As String, ByVal AlertLevel As Diagnostics.EventLogEntryType)
        WriteErrorLog(Message, 1, AlertLevel)
    End Sub

    Public Overloads Shared Sub WriteErrorLog(ByVal Message As String, ByVal MessageLevel As Integer, ByVal AlertLevel As Diagnostics.EventLogEntryType)
        'Dim EventLog As New EventLog()
        'If Not Diagnostics.EventLog.SourceExists("Standalone SQL Agent") Then
        '    Diagnostics.EventLog.CreateEventSource("Standalone SQL Agent", "Application")
        'End If

        'EventLog.Source = "Application"


        'If Message.Length < 30000 Then EventLog.WriteEntry(Message)
        Message = Now.Date.ToString.Substring(0, Now.Date.ToString.IndexOf(" ")) + " " + Now.TimeOfDay.ToString + " - " + Message

        If MessageLevel <= DebugLevel Then
            Dim FullPath As String
            FullPath = System.AppDomain.CurrentDomain.BaseDirectory + "ERRORLOG"
            Dim bAns As Boolean = False
            Dim objReader As StreamWriter
            Try
                objReader = New StreamWriter(FullPath, True)
                objReader.WriteLine(Message)
                objReader.Close()
                bAns = True
            Catch Ex As Exception
                'ErrInfo = Ex.Message
                'MsgBox(Ex.Message)
            End Try
        End If
    End Sub

    Shared Function CastIntsToDate(ByVal iDate As String, ByVal iTime As String)
        Dim dDate As Date
        Try
            If iDate = "0" Then
                dDate = Nothing
            Else
                dDate = iDate.Substring(4, 2) + "/" + iDate.Substring(6, 2) + "/" + iDate.Substring(0, 4)
            End If
        Catch ex As Exception
            WriteErrorLog("Error parsing date information from SQL Server.  Using current date instead.", 10, EventLogEntryType.Error)
            WriteErrorLog(ex.Message, 10, EventLogEntryType.Error)
            dDate = Now.Date
        End Try
        If iTime.Length = 1 Or iTime.Length = 2 Then
            dDate = DateAdd(DateInterval.Second, CInt(iTime), dDate)
        ElseIf iTime.Length = 3 Then
            dDate = DateAdd(DateInterval.Minute, CInt(iTime.Substring(1, 1)), dDate)
            dDate = DateAdd(DateInterval.Second, CInt(iTime.Substring(1, 2)), dDate)
        ElseIf iTime.Length = 4 Then
            dDate = DateAdd(DateInterval.Minute, CInt(iTime.Substring(0, 2)), dDate)
            dDate = DateAdd(DateInterval.Second, CInt(iTime.Substring(2, 2)), dDate)
        ElseIf iTime.Length = 5 Then
            dDate = DateAdd(DateInterval.Hour, CInt(iTime.Substring(0, 1)), dDate)
            dDate = DateAdd(DateInterval.Minute, CInt(iTime.Substring(1, 2)), dDate)
            dDate = DateAdd(DateInterval.Second, CInt(iTime.Substring(3, 2)), dDate)
        ElseIf iTime.Length = 6 Then
            dDate = DateAdd(DateInterval.Hour, CInt(iTime.Substring(0, 2)), dDate)
            dDate = DateAdd(DateInterval.Minute, CInt(iTime.Substring(2, 2)), dDate)
            dDate = DateAdd(DateInterval.Second, CInt(iTime.Substring(4, 2)), dDate)
        End If
        Return dDate

    End Function
    Public Shared Function StringTimeToDate(ByVal StringTime As String)
        Dim DateTime As Date
        If StringTime.Length = 1 Then
            DateTime = "00:00:00"
        ElseIf StringTime.Length = 2 Then
            DateTime = "00:00:" + StringTime
        ElseIf StringTime.Length = 3 Then
            DateTime = "00:0" + StringTime.Substring(0, 1) + ":" + StringTime.Substring(1, 2)
        ElseIf StringTime.Length = 4 Then
            DateTime = "00:" + StringTime.Substring(0, 2) + ":" + StringTime.Substring(2, 2)
        ElseIf StringTime.Length = 5 Then
            DateTime = "0" + StringTime.Substring(0, 1) + ":" + StringTime.Substring(1, 2) + ":" + StringTime.Substring(3, 2)
        ElseIf StringTime.Length = 6 Then
            DateTime = StringTime.Substring(0, 2) + ":" + StringTime.Substring(2, 2) + ":" + StringTime.Substring(4, 2)
        Else
            WriteErrorLog("Unable to parse active_start_time", EventLogEntryType.Error)
        End If
        Return DateTime
    End Function
    Public Shared Function StringDatetoDate(ByVal StringDate As String)
        Dim DateTime As Date
        If StringDate = "0" Then
            DateTime = "12/31/9999"
        Else
            DateTime = StringDate.Substring(0, 4) + "/" + StringDate.Substring(4, 2) + "/" + StringDate.Substring(6, 2)
        End If

        Return DateTime
    End Function
    Public Shared Function DateDateToStringDate(ByVal DateDate As Date)
        Dim StringDate As String
        StringDate = DateDate.Year.ToString
        If DateDate.Month.ToString.Length = 1 Then StringDate = StringDate + "0"
        StringDate = StringDate + DateDate.Month.ToString
        If DateDate.Day.ToString.Length = 1 Then StringDate = StringDate + "0"
        StringDate = StringDate + DateDate.Day.ToString

        Return StringDate
    End Function
    Public Shared Function DateTimeToStringTime(ByVal Time As TimeSpan)
        Dim ReturnString As String
        ReturnString = ""
        If Time.Hours <> 0 Then ReturnString = Time.Hours.ToString
        If Time.Hours <> 0 Then
            If Time.Minutes <> 0 Then
                If Time.Minutes.ToString.Length = 1 Then ReturnString = ReturnString + "0"
                ReturnString = ReturnString + Time.Minutes.ToString
            Else
                ReturnString = ReturnString + "00"
            End If
        Else
            If Time.Minutes <> 0 Then
                ReturnString = ReturnString + Time.Minutes.ToString
            End If
        End If
        'If Time.Hours <> 0 And Time.Minutes <> 0 And Time.Seconds <> 0 Then
        If Time.Seconds.ToString.Length = 1 Then ReturnString = ReturnString + "0"
        ReturnString = ReturnString + Time.Seconds.ToString
        'End If

        If Time.Hours = 0 And Time.Minutes = 0 And Time.Seconds = 0 Then ReturnString = 0

        'If Time.Hours <> 0 And Time.Minutes <> 0 Then
        '    ReturnString = ReturnString + Time.Minutes.ToString
        'ElseIf Time.Hours <> 0 And Time.Minutes = 0 Then
        '    ReturnString = ReturnString + "00"
        'ElseIf Time.Hours = 0 And Time.Minutes <> 0 Then
        '    ReturnString = Time.Minutes
        'End If
        'If Time.Hours <> 0 And Time.Minutes <> 0 And Time.Seconds <> 0 Then
        '    ReturnString = ReturnString + Time.Seconds.ToString
        'ElseIf Time.Hours <> 0 And Time.Minutes <> 0 And Time.Seconds = 0 Then
        '    ReturnString = ReturnString + "00"
        'ElseIf Time.Hours <> 0 And Time.Minutes = 0 And Time.Seconds = 0 Then
        '    ReturnString = ReturnString + "00"
        'ElseIf Time.Hours = 0 And Time.Minutes = 0 And Time.Seconds <> 0 Then
        '    ReturnString = Time.Seconds.ToString
        'ElseIf Time.Hours = 0 And Time.Minutes = 0 And Time.Seconds = 0 Then
        '    ReturnString = 0
        'End If
        Return ReturnString
    End Function
    Public Shared Function GetInstallPath() As String
        Return System.AppDomain.CurrentDomain.BaseDirectory
    End Function
    Public Overloads Shared Function FormatconnectionString(ByVal InstanceName As String, ByVal DatabaseName As String)
        Return FormatconnectionString(InstanceName, DatabaseName, JobServer.InstanceAuthenticationMethod.WindowsAuthentication, Nothing, Nothing)
    End Function
    Public Overloads Shared Function FormatConnectionString(ByVal InstanceName As String, ByVal DatabaseName As String, ByVal AuthenticationMode As JobServer.InstanceAuthenticationMethod, ByVal UserName As String, ByVal Password As String)
        Dim Connstring As String
        InstanceName = "Server=" + InstanceName
        DatabaseName = "Database=" + DatabaseName
        If AuthenticationMode = JobServer.InstanceAuthenticationMethod.SQLAuthentication Then
            UserName = "UserName=" + UserName
            Password = "Password=" + Password
        End If

        Connstring = InstanceName + ";" + DatabaseName + ";"
        If AuthenticationMode = JobServer.InstanceAuthenticationMethod.WindowsAuthentication Then
            Connstring = Connstring + "Trusted_Connection=True;"
        Else
            Connstring = Connstring + UserName + ";" + Password + ";"
        End If
        Return Connstring
    End Function
    Public Shared Function ObjectExists(ByVal ConnectionString As String, ByVal DbObject As String, ByVal DbSchema As String) As Boolean

        Dim SQLConn As SqlConnection
        Dim SQLCmd As SqlCommand
        Dim SQLda As SqlDataAdapter
        Dim SQLResults As DataSet

        Try

            SQLConn = New SqlConnection(ConnectionString)
            SQLCmd = New SqlCommand

            SQLCmd.CommandText = "select name from sys.all_objects where name = '" + DbObject + "' and schema_id = schema_id('" + DbSchema + "')"
            SQLCmd.CommandType = CommandType.Text

            SQLConn.Open()
            SQLCmd.Connection = SQLConn
            SQLCmd.ExecuteNonQuery()
            SQLda = New SqlDataAdapter(SQLCmd)

            SQLResults = New DataSet

            SQLda.Fill(SQLResults)

            SQLConn.Close()
            SQLCmd.Dispose()
            SQLConn = Nothing
            SQLCmd = Nothing

            If SQLResults.Tables(0).Rows.Count = 0 Then
                Return False
            Else
                Return True
            End If

        Catch ex As Exception
            WriteErrorLog("Unable to Query for Database Object", 2, EventLogEntryType.Information)
            WriteErrorLog(ex.Message, 1, EventLogEntryType.Error)

            Return False
        End Try



    End Function
    Public Shared Function RunSQLStatement(ByVal ConnectionString As String, ByRef SQLCmd As SqlCommand)
        Dim Connection As SqlConnection
        Dim Results As DataSet
        Dim SQLda As SqlDataAdapter
        Connection = New SqlConnection(ConnectionString)
        Using Connection
            ' Try
            Connection.Open()
            SQLCmd.Connection = Connection
            'SQLCmd.ExecuteNonQuery()
            SQLda = New SqlDataAdapter(SQLCmd)
            Results = New DataSet()
            SQLda.Fill(Results)
            'Catch ex As Exception
            'SQLDataAdapter was empty
            'End Try

            'WriteErrorLog(SQLda.TableMappings.Count, 11, EventLogEntryType.Information)

            'WriteErrorLog(Results.Tables.Count, 11, EventLogEntryType.Information)
        End Using
        Return Results
    End Function

End Class
