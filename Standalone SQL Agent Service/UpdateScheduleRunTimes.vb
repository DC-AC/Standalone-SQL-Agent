Imports System.Diagnostics
Imports System.Data
Imports System.Data.SqlClient
Imports System.Threading
Imports System.IO
Imports System.Xml
Imports System
Imports SSA.CommonControl
Imports SSA.CommonControl.Enums
Imports SSA.CommonControl.SharedCode

Public Class UpdateScheduleRunTimes
    Delegate Sub StartJobDelegate(ByVal ConnectionString As String)

    Public ConnectionString As String

    Public StartDeligate As StartJobDelegate

    Public Sub StartJob()
        StartDeligate(ConnectionString)
    End Sub

    Public Sub UpdateJobSchedules(ByVal ConnectionString As String)
        Dim SQLConn As SqlConnection
        Dim SQLCmd As SqlCommand
        Dim SQLda As SqlDataAdapter
        Dim SQLResults As DataSet
        Dim StartTime As Date, EndTime As Date
        While 1 = 1
            WriteErrorLog("Downloading Schedules for processing", 10, EventLogEntryType.Error)
            StartTime = Now
            Try
                SQLConn = New SqlConnection(ConnectionString)
                SQLCmd = New SqlCommand
                SQLResults = New DataSet

                SQLCmd.CommandText = "select distinct sysschedules.* , case when next_run_date = 0 then 0 else isnull(sysjobhistory.run_date, 0) end as lastrundate, case when next_run_date = 0 then 0 else isnull(sysjobhistory.run_time, 0) end as lastruntime, isnull(sysjobschedules.next_run_date, 0) next_run_date, isnull(sysjobschedules.next_run_time, 0) next_run_time from sysschedules join sysjobschedules on sysschedules.schedule_id = sysjobschedules.schedule_id left outer join sysjobhistory on sysjobschedules.job_id = sysjobhistory.job_id 	and exists (select job_id, MAX(instance_id) instance_id  		from sysjobhistory a  		where a.job_id = sysjobhistory.job_id 		group by job_id 		having MAX(a.instance_id) = sysjobhistory.instance_id) where sysschedules.enabled = 1 and freq_type <> 64"
                SQLCmd.CommandType = CommandType.Text

                SQLConn.Open()
                SQLCmd.Connection = SQLConn
                SQLCmd.ExecuteNonQuery()
                SQLda = New SqlDataAdapter(SQLCmd)
                SQLda.Fill(SQLResults)
                SQLConn.Close()
                SQLCmd.Dispose()
                SQLConn = Nothing
                SQLda = Nothing
                SQLCmd = Nothing
            Catch ex As Exception
                WriteErrorLog("Problem Getting Schedule List.  Will pause and try again.", 2, EventLogEntryType.Error)
                WriteErrorLog(ex.Message, 2, EventLogEntryType.Error)
                GoTo ThreadSleeps
            End Try

            Dim ScheduleCounter As Integer = 0
            Dim NextStartDate As String = "", NextStartTime As String = "", NextStartDateTime As Date
            Dim SQLStatement As String

            WriteErrorLog("Found " + SQLResults.Tables(0).Rows.Count.ToString + " schedules to process", 10, EventLogEntryType.Information)
            While ScheduleCounter < SQLResults.Tables(0).Rows.Count
                If SQLResults.Tables(0).Rows(ScheduleCounter).Item("freq_type") <> e_freq_type.Run_At_Agent_Startup Then
                    WriteErrorLog("Processing Schedule " + SQLResults.Tables(0).Rows(ScheduleCounter).Item("name"), 10, EventLogEntryType.Information)

                    FindNextStartTime(SQLResults.Tables(0).Rows(ScheduleCounter).Item("freq_type"), _
                        SQLResults.Tables(0).Rows(ScheduleCounter).Item("freq_interval"), _
                        SQLResults.Tables(0).Rows(ScheduleCounter).Item("freq_subday_type"), _
                        SQLResults.Tables(0).Rows(ScheduleCounter).Item("freq_subday_interval"), _
                        SQLResults.Tables(0).Rows(ScheduleCounter).Item("freq_recurrence_factor"), _
                        SQLResults.Tables(0).Rows(ScheduleCounter).Item("active_start_date"), _
                        SQLResults.Tables(0).Rows(ScheduleCounter).Item("active_start_time"), _
                        SQLResults.Tables(0).Rows(ScheduleCounter).Item("active_end_date"), _
                        SQLResults.Tables(0).Rows(ScheduleCounter).Item("active_end_time"), _
                        SQLResults.Tables(0).Rows(ScheduleCounter).Item("freq_relative_interval"), _
                        NextStartDate, NextStartTime, NextStartDateTime, _
                        SQLResults.Tables(0).Rows(ScheduleCounter).Item("lastrundate"), _
                        SQLResults.Tables(0).Rows(ScheduleCounter).Item("lastruntime"))

                    WriteErrorLog(SQLResults.Tables(0).Rows(ScheduleCounter).Item("name") + " - " + NextStartDateTime.ToString, 10, EventLogEntryType.Information)
                    If DateDiff(DateInterval.Minute, Now(), NextStartDateTime) >= 0 _
                        And DateDiff(DateInterval.Minute, Now(), NextStartDateTime) < 60 _
                        And (NextStartDate <> SQLResults.Tables(0).Rows(ScheduleCounter).Item("next_run_date").ToString _
                        Or NextStartTime <> SQLResults.Tables(0).Rows(ScheduleCounter).Item("next_run_time").ToString) Then

                        SQLStatement = "update sysjobschedules set next_run_date = '" + NextStartDate + _
                            "', next_run_time = '" + NextStartTime + "' where schedule_id = " + SQLResults.Tables(0).Rows(ScheduleCounter).Item("schedule_id").ToString

                        Try

                            WriteErrorLog("Changing Next Run Date and Time in database for above schedule", 10, EventLogEntryType.Information)
                            'SQLConn = New SqlConnection(ConnectionString)
                            SQLCmd = New SqlCommand
                            'SQLResults = New DataSet

                            SQLCmd.CommandText = SQLStatement
                            SQLCmd.CommandType = CommandType.Text

                            RunSQLStatement(ConnectionString, SQLCmd)


                        Catch ex As Exception
                            WriteErrorLog("Problem updating schedule " + SQLResults.Tables(0).Rows(ScheduleCounter).Item("name").ToString, 2, EventLogEntryType.Error)
                            WriteErrorLog(SQLStatement, 10, EventLogEntryType.Error)
                            WriteErrorLog(ex.Message, 2, EventLogEntryType.Error)
                            GoTo ThreadSleeps
                        End Try
                    End If
                End If
                ScheduleCounter += 1
            End While

ThreadSleeps:
            If Thread.CurrentThread.Name <> "Scheduler" Then Exit While
            EndTime = Now
            WriteErrorLog("Sleeping Schedule Updater for " + (60000 - (EndTime - StartTime).Milliseconds).ToString + " milliseconds", 10, EventLogEntryType.Information)
            If Now.Second <> 0 Then Thread.Sleep((60000 - (EndTime - StartTime).Milliseconds) * 1000)
            GoTo StartsBackup

StartsBackup:
        End While
        WriteErrorLog("Shutting down schedule updater", 2, EventLogEntryType.Information)
    End Sub

    Private Sub FindNextStartTime(ByVal freq_type As Integer, ByVal freq_interval As Integer, _
    ByVal freq_subday_type As Integer, ByVal freq_subday_interval As Integer, ByVal freq_recurrence_factor As Integer, _
    ByVal active_start_date As String, ByVal active_start_time As String, ByVal active_end_date As String, _
    ByVal active_end_time As String, ByVal freq_relative_interval As Integer, ByRef NextStartDate As String, ByRef NextStartTime As String, _
    ByRef NextStartDateTime As Date, ByVal last_run_Date As String, ByVal last_run_time As String)

        Dim d_active_start_date As Date
        Dim d_active_start_time As TimeSpan

        d_active_start_date = StringDatetoDate(active_start_date)
        d_active_start_time = CDate(StringTimeToDate(active_start_time)).TimeOfDay

        If freq_type = e_freq_type.One_Time_Only Then
            NextStartDate = active_start_date
            NextStartTime = active_start_time
        ElseIf freq_type = e_freq_type.Daily Then
            If freq_subday_type = e_freq_subday_type.Time_Specified Then
                If d_active_start_time < Now.TimeOfDay Then d_active_start_date = DateAdd(DateInterval.Day, 1, Now())
                NextStartTime = active_start_time
                NextStartDate = DateDateToStringDate(d_active_start_date)
            Else
                NextStartIntervalTime(active_start_date, active_start_time, active_end_date, active_end_time, _
                    freq_subday_type, freq_subday_interval, d_active_start_date, d_active_start_time, last_run_Date, last_run_time)
            End If
        ElseIf freq_type = e_freq_type.Weekly Then
            'Figure out if the job is scheduled to run today using freq_interval
            If System.Enum.GetName(GetType(e_freq_interval_weekly), freq_interval) = Now.DayOfWeek.ToString Then
                NextStartIntervalTime(active_start_date, active_start_time, active_end_date, active_end_time, _
                    freq_subday_type, freq_subday_interval, d_active_start_date, d_active_start_time, last_run_Date, last_run_time)
            End If
        ElseIf freq_type = e_freq_type.Monthly Then
            'Figure out if the job is scheduled to run today using now.day
            If freq_interval = Now.Day Then
                NextStartIntervalTime(active_start_date, active_start_time, active_end_date, active_end_time, _
                    freq_subday_type, freq_subday_interval, d_active_start_date, d_active_start_time, last_run_Date, last_run_time)
            End If
        ElseIf freq_type = e_freq_type.Monthly_with_freq_interval Then
            'Single Day of the Month
            If freq_interval = e_freq_interval_monthly_relitave.Day And Now.Day = 1 Then
                NextStartIntervalTime(active_start_date, active_start_time, active_end_date, active_end_time, _
                     freq_subday_type, freq_subday_interval, d_active_start_date, d_active_start_time, last_run_Date, last_run_time)
            End If
            'Relitave Days of the month
            'Weekdays
            If freq_interval = e_freq_interval_monthly_relitave.Weekday _
                And ((freq_relative_interval = e_freq_relative_interval.First _
                And Now.DayOfWeek = DayOfWeek.Monday) _
                Or (freq_relative_interval = e_freq_relative_interval.Second _
                And Now.DayOfWeek = DayOfWeek.Tuesday) _
                 Or (freq_relative_interval = e_freq_relative_interval.Third _
                And Now.DayOfWeek = DayOfWeek.Wednesday) _
                 Or (freq_relative_interval = e_freq_relative_interval.Fourth _
                And Now.DayOfWeek = DayOfWeek.Thursday) _
                 Or (freq_relative_interval = e_freq_relative_interval.Last _
                And Now.DayOfWeek = DayOfWeek.Friday)) Then
                NextStartIntervalTime(active_start_date, active_start_time, active_end_date, active_end_time, _
                    freq_subday_type, freq_subday_interval, d_active_start_date, d_active_start_time, last_run_Date, last_run_time)
            End If
            'Weekends
            If freq_interval = e_freq_interval_monthly_relitave.WeekendDay _
               And (freq_relative_interval = e_freq_relative_interval.First _
               And Now.DayOfWeek = DayOfWeek.Saturday) _
               Or ((freq_relative_interval = e_freq_relative_interval.Second _
                   Or (freq_relative_interval = e_freq_relative_interval.Last) _
               And Now.DayOfWeek = DayOfWeek.Sunday)) Then
                NextStartIntervalTime(active_start_date, active_start_time, active_end_date, active_end_time, _
                    freq_subday_type, freq_subday_interval, d_active_start_date, d_active_start_time, last_run_Date, last_run_time)
            End If
            'Relitave Day of the month
            'First n of the month
            If freq_relative_interval = e_freq_relative_interval.First _
                And Now.Day <= 7 _
                And ((freq_interval = e_freq_interval_monthly_relitave.Sunday _
                And Now.DayOfWeek = DayOfWeek.Sunday) _
                Or (freq_interval = e_freq_interval_monthly_relitave.Monday _
                And Now.DayOfWeek = DayOfWeek.Monday) _
                 Or (freq_interval = e_freq_interval_monthly_relitave.Tuesday _
                And Now.DayOfWeek = DayOfWeek.Tuesday) _
                 Or (freq_interval = e_freq_interval_monthly_relitave.Wednesday _
                And Now.DayOfWeek = DayOfWeek.Wednesday) _
                 Or (freq_interval = e_freq_interval_monthly_relitave.Thursday _
                And Now.DayOfWeek = DayOfWeek.Thursday) _
                 Or (freq_interval = e_freq_interval_monthly_relitave.Friday _
                And Now.DayOfWeek = DayOfWeek.Friday) _
                 Or (freq_interval = e_freq_interval_monthly_relitave.Saturday _
                And Now.DayOfWeek = DayOfWeek.Saturday) _
                ) Then
                NextStartIntervalTime(active_start_date, active_start_time, active_end_date, active_end_time, _
                    freq_subday_type, freq_subday_interval, d_active_start_date, d_active_start_time, last_run_Date, last_run_time)

            End If
            'Second n of the month
            If freq_relative_interval = e_freq_relative_interval.First _
                And Now.Day <= 14 And Now.Day > 7 _
                And ((freq_interval = e_freq_interval_monthly_relitave.Sunday _
                And Now.DayOfWeek = DayOfWeek.Sunday) _
                Or (freq_interval = e_freq_interval_monthly_relitave.Monday _
                And Now.DayOfWeek = DayOfWeek.Monday) _
                 Or (freq_interval = e_freq_interval_monthly_relitave.Tuesday _
                And Now.DayOfWeek = DayOfWeek.Tuesday) _
                 Or (freq_interval = e_freq_interval_monthly_relitave.Wednesday _
                And Now.DayOfWeek = DayOfWeek.Wednesday) _
                 Or (freq_interval = e_freq_interval_monthly_relitave.Thursday _
                And Now.DayOfWeek = DayOfWeek.Thursday) _
                 Or (freq_interval = e_freq_interval_monthly_relitave.Friday _
                And Now.DayOfWeek = DayOfWeek.Friday) _
                 Or (freq_interval = e_freq_interval_monthly_relitave.Saturday _
                And Now.DayOfWeek = DayOfWeek.Saturday) _
                ) Then
                NextStartIntervalTime(active_start_date, active_start_time, active_end_date, active_end_time, _
                    freq_subday_type, freq_subday_interval, d_active_start_date, d_active_start_time, last_run_Date, last_run_time)
            End If
            'Third n of the month
            If freq_relative_interval = e_freq_relative_interval.First _
                And Now.Day <= 21 And Now.Day > 14 _
                And ((freq_interval = e_freq_interval_monthly_relitave.Sunday _
                And Now.DayOfWeek = DayOfWeek.Sunday) _
                Or (freq_interval = e_freq_interval_monthly_relitave.Monday _
                And Now.DayOfWeek = DayOfWeek.Monday) _
                 Or (freq_interval = e_freq_interval_monthly_relitave.Tuesday _
                And Now.DayOfWeek = DayOfWeek.Tuesday) _
                 Or (freq_interval = e_freq_interval_monthly_relitave.Wednesday _
                And Now.DayOfWeek = DayOfWeek.Wednesday) _
                 Or (freq_interval = e_freq_interval_monthly_relitave.Thursday _
                And Now.DayOfWeek = DayOfWeek.Thursday) _
                 Or (freq_interval = e_freq_interval_monthly_relitave.Friday _
                And Now.DayOfWeek = DayOfWeek.Friday) _
                 Or (freq_interval = e_freq_interval_monthly_relitave.Saturday _
                And Now.DayOfWeek = DayOfWeek.Saturday) _
                ) Then
                NextStartIntervalTime(active_start_date, active_start_time, active_end_date, active_end_time, _
                    freq_subday_type, freq_subday_interval, d_active_start_date, d_active_start_time, last_run_Date, last_run_time)

            End If
            'fourth n of the month
            If freq_relative_interval = e_freq_relative_interval.First _
                And Now.Day <= 28 And Now.Day > 21 _
                And ((freq_interval = e_freq_interval_monthly_relitave.Sunday _
                And Now.DayOfWeek = DayOfWeek.Sunday) _
                Or (freq_interval = e_freq_interval_monthly_relitave.Monday _
                And Now.DayOfWeek = DayOfWeek.Monday) _
                 Or (freq_interval = e_freq_interval_monthly_relitave.Tuesday _
                And Now.DayOfWeek = DayOfWeek.Tuesday) _
                 Or (freq_interval = e_freq_interval_monthly_relitave.Wednesday _
                And Now.DayOfWeek = DayOfWeek.Wednesday) _
                 Or (freq_interval = e_freq_interval_monthly_relitave.Thursday _
                And Now.DayOfWeek = DayOfWeek.Thursday) _
                 Or (freq_interval = e_freq_interval_monthly_relitave.Friday _
                And Now.DayOfWeek = DayOfWeek.Friday) _
                 Or (freq_interval = e_freq_interval_monthly_relitave.Saturday _
                And Now.DayOfWeek = DayOfWeek.Saturday) _
                ) Then
                NextStartIntervalTime(active_start_date, active_start_time, active_end_date, active_end_time, _
                    freq_subday_type, freq_subday_interval, d_active_start_date, d_active_start_time, last_run_Date, last_run_time)

            End If
            'last n of the month
            If freq_relative_interval = e_freq_relative_interval.First _
                And Now.Day <= GetLastDayOfMonth() And Now.Day > GetLastDayOfMonth() - 7 _
                And ((freq_interval = e_freq_interval_monthly_relitave.Sunday _
                And Now.DayOfWeek = DayOfWeek.Sunday) _
                Or (freq_interval = e_freq_interval_monthly_relitave.Monday _
                And Now.DayOfWeek = DayOfWeek.Monday) _
                 Or (freq_interval = e_freq_interval_monthly_relitave.Tuesday _
                And Now.DayOfWeek = DayOfWeek.Tuesday) _
                 Or (freq_interval = e_freq_interval_monthly_relitave.Wednesday _
                And Now.DayOfWeek = DayOfWeek.Wednesday) _
                 Or (freq_interval = e_freq_interval_monthly_relitave.Thursday _
                And Now.DayOfWeek = DayOfWeek.Thursday) _
                 Or (freq_interval = e_freq_interval_monthly_relitave.Friday _
                And Now.DayOfWeek = DayOfWeek.Friday) _
                 Or (freq_interval = e_freq_interval_monthly_relitave.Saturday _
                And Now.DayOfWeek = DayOfWeek.Saturday) _
                ) Then
                NextStartIntervalTime(active_start_date, active_start_time, active_end_date, active_end_time, _
                    freq_subday_type, freq_subday_interval, d_active_start_date, d_active_start_time, last_run_Date, last_run_time)
            End If
        ElseIf freq_type = e_freq_type.Run_At_Agent_Startup Then
            NextStartIntervalTime(active_start_date, active_start_time, active_end_date, active_end_time, _
                    freq_subday_type, freq_subday_interval, d_active_start_date, d_active_start_time, last_run_Date, last_run_time)

        ElseIf freq_type = e_freq_type.Run_when_Idle Then
            'This will be dealt with if I ever add in the alert and performance monitoring pieces.
            'Until then, fuck it.
        End If
        If d_active_start_time <> Nothing And d_active_start_date <> Nothing Then
            NextStartTime = DateTimeToStringTime(d_active_start_time)
            NextStartDate = DateDateToStringDate(d_active_start_date)
            NextStartDateTime = StringDatetoDate(NextStartDate) + " " + StringTimeToDate(NextStartTime)
        End If

    End Sub
    Public Function GetLastDayOfMonth()
        Dim WorkingDate As Date
        WorkingDate = Now()
        WorkingDate = DateAdd(DateInterval.Month, 1, WorkingDate)
        WorkingDate = DateAdd(DateInterval.Day, WorkingDate.Day * -1, WorkingDate)
        WorkingDate = DateAdd(DateInterval.Day, -1, WorkingDate)
        Return WorkingDate.Day
    End Function
    Public Sub NextStartIntervalTime(ByVal active_start_date As String, ByVal active_start_time As String, _
        ByVal active_end_date As String, ByVal active_end_time As String, ByVal freq_subday_type As Integer, _
        ByVal freq_subday_interval As Integer, ByRef d_active_start_date As Date, ByRef d_active_start_time As TimeSpan, _
        ByVal last_run_Date As String, ByRef last_run_time As String)

        If StringDatetoDate(active_start_date) > Now.Date() Or _
            StringDatetoDate(active_end_date) < Now.Date() Then
            d_active_start_date = Nothing
            d_active_start_time = Nothing
            Exit Sub
        End If
        If CDate(StringTimeToDate(active_start_time)).TimeOfDay > Now.TimeOfDay Or _
            CDate(StringTimeToDate(active_end_time)).TimeOfDay < Now.TimeOfDay Then
            d_active_start_date = Nothing
            d_active_start_time = Nothing
            Exit Sub
        End If

        Dim NextStartTime As Date
        'If last_run_Date = "0" And last_run_time = "0" Then
        'NextStartTime = StringDatetoDate(active_start_date) + " " + StringTimeToDate(active_start_time)
        'Else
        'NextStartTime = StringDatetoDate(last_run_Date) + " " + StringTimeToDate(last_run_time)
        'End If
        NextStartTime = Now.Date + " " + StringTimeToDate(active_start_time)

        Do Until NextStartTime > Now()
            If freq_subday_type = e_freq_subday_type.Seconds Then
                NextStartTime = DateAdd(DateInterval.Second, freq_subday_interval, NextStartTime)
            ElseIf freq_subday_type = e_freq_subday_type.Minutes Then
                NextStartTime = DateAdd(DateInterval.Minute, freq_subday_interval, NextStartTime)
            ElseIf freq_subday_type = e_freq_subday_type.Hours Then
                NextStartTime = DateAdd(DateInterval.Hour, freq_subday_interval, NextStartTime)
            End If
        Loop

        d_active_start_date = NextStartTime.Date
        d_active_start_time = NextStartTime.TimeOfDay

    End Sub

End Class
