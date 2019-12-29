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
Imports Standalone_SQL_Agent_Service.JobRunner

Public Class SQLInstance
    Inherits UpdateScheduleRunTimes
    Public Shadows connectionString As String
    Delegate Sub StartInstanceDelegate(ByVal ConnectionString As String)
    Public Sub StartInstance()
        BaseInstanceThread(connectionString)
    End Sub
    Public Shadows StartDeligate As StartInstanceDelegate
    Public Sub BaseInstanceThread(ByVal ConnectionString As String)
        WriteErrorLog("Starting Thread for Connection String " + ConnectionString, 9, EventLogEntryType.Information)
        Dim job_guid As String
        Dim job_id As Integer
        Dim job_guid_list As JobGuidList
        Dim JobStepId As Integer
        'Dim WaitTime As Integer
        Dim aJob As Job
        Dim SQLResults As DataSet
        Dim JobSteps As DataSet
        Dim SQLCmd As SqlCommand
        Dim SQLStepsCmd As SqlCommand
        Dim LoopCounter As Integer = 0
        Dim StepCounter As Integer = 0
        Dim LoopStartTime As Date
        Dim LoopendTime As Date
        Dim ScheduleCounter As Integer = 0
        Dim hs As Schedule
        Dim StartUpPhase As Boolean = True

        Try
            SQLCmd = New SqlCommand
            SQLCmd.CommandText = "select getdate()"
            SQLCmd.CommandType = CommandType.Text
            RunSQLStatement(ConnectionString, SQLCmd)
        Catch ex As Exception
            WriteErrorLog("Error connecting to SQL Instance.  No connection attempt will be made until Sevice is restarted.", EventLogEntryType.Error)
            WriteErrorLog(ex.Message, EventLogEntryType.Information)
            Exit Sub
        End Try

        ChangeSQLConfig(ConnectionString, "show advanced", 1)
        ChangeSQLConfig(ConnectionString, "Agent XPs", 1)
        ChangeSQLConfig(ConnectionString, "show advanced", 0)

        CreateReplacementObjects(ConnectionString)
        
        MarkRunningJobsAsCancled(ConnectionString)

        If Now.Second <> 0 Then Thread.Sleep((60 - Now.Second) * 1000)

        UpdateJobSchedules(ConnectionString)

        'Dim Scheduler As New UpdateScheduleRunTimes()
        'Scheduler.ConnectionString = ConnectionString
        'Scheduler.StartDeligate = AddressOf UpdateJobSchedules

        'Dim SchedulerThread As New Thread(AddressOf Scheduler.StartJob)
        'SchedulerThread.IsBackground = True
        'SchedulerThread.Name = "Scheduler"
        'SchedulerThread.Start()


        Dim SQLConn As SqlConnection
        Dim SQLda As SqlDataAdapter

        While 1 = 1
            WriteErrorLog("Starting The Main Instance Loop", 11, EventLogEntryType.Information)
            'Get the Job data
            LoopStartTime = Now()

            SQLResults = New DataSet()
            job_guid_list = New JobGuidList

            Try

                SQLConn = New SqlConnection(ConnectionString)
                SQLCmd = New SqlCommand

                SQLCmd.CommandText = "sp_help_job_SSA"
                SQLCmd.CommandType = CommandType.StoredProcedure
                SQLCmd.Parameters.Add("@Enabled", SqlDbType.Bit)
                SQLCmd.Parameters.Item("@Enabled").Value = 1
                SQLCmd.Parameters.Item("@Enabled").Direction = ParameterDirection.Input

                SQLCmd.Parameters.Add("@execution_status", SqlDbType.Int)
                SQLCmd.Parameters.Item("@execution_status").Value = 4
                SQLCmd.Parameters.Item("@execution_status").Direction = ParameterDirection.Input

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
                WriteErrorLog("Problem Getting Job List.  Will pause and try again.", 2, EventLogEntryType.Error)
                WriteErrorLog(ex.Message, 2, EventLogEntryType.Error)
                GoTo FinishLoop
                'Exit Sub
            End Try
            WriteErrorLog(SQLResults.Tables(0).Rows.Count.ToString + " jobs found for " + ConnectionString, 10, EventLogEntryType.Information)

            LoopCounter = 0

            Dim Jobs(SQLResults.Tables(0).Rows.Count) As Job
            Dim JobsToRun(SQLResults.Tables(0).Rows.Count) As Job

            Do Until LoopCounter > SQLResults.Tables(0).Rows.Count
                Jobs(LoopCounter) = New Job
                JobsToRun(LoopCounter) = New Job
                LoopCounter += 1
            Loop

            LoopCounter = 0

            WriteErrorLog("Variables setup for " + SQLResults.Tables(0).Rows.Count.ToString + " jobs.", 11, EventLogEntryType.Information)

            While LoopCounter <> SQLResults.Tables(0).Rows.Count
                WriteErrorLog("Looping through job recordset", 11, EventLogEntryType.Information)
                StepCounter = 0

                job_guid = SQLResults.Tables(0).Rows(LoopCounter).Item("job_id").ToString

                WriteErrorLog("Putting Guid into array", 10, EventLogEntryType.Information)
                If job_guid_list.GetJobId(job_guid) = Nothing Then
                    job_id = job_guid_list.AddGuid(job_guid)
                Else
                    job_id = job_guid_list.GetJobId(job_guid)
                End If


                WriteErrorLog("Putting job values into ""Jobs"" variable.", 11, EventLogEntryType.Information)
                Jobs(job_id).job_id = job_guid
                Jobs(job_id).StartStepId = SQLResults.Tables(0).Rows(LoopCounter).Item("start_step_id").ToString
                Jobs(job_id).job_name = SQLResults.Tables(0).Rows(LoopCounter).Item("name").ToString

                SQLStepsCmd = New SqlCommand()
                SQLStepsCmd.CommandText = "sp_help_job_SSA"
                SQLStepsCmd.CommandType = CommandType.StoredProcedure

                SQLStepsCmd.Parameters.Add("@job_id", SqlDbType.VarChar, 50)
                SQLStepsCmd.Parameters.Item("@job_id").Value = job_guid
                SQLStepsCmd.Parameters.Item("@job_id").Direction = ParameterDirection.Input

                'SQLStepsCmd = New SqlCommand()
                JobSteps = New DataSet

                WriteErrorLog("Getting Job step data for job " + Jobs(job_id).job_name, 10, EventLogEntryType.Information)
                Try

                    SQLConn = New SqlConnection(ConnectionString)
                    SQLConn.Open()
                    SQLStepsCmd.Connection = SQLConn
                    SQLStepsCmd.ExecuteNonQuery()
                    SQLda = New SqlDataAdapter(SQLStepsCmd)
                    SQLda.Fill(JobSteps)
                    SQLConn.Close()
                    SQLStepsCmd.Dispose()
                    SQLConn = Nothing
                    SQLda = Nothing
                    SQLStepsCmd = Nothing
                Catch ex As Exception
                    WriteErrorLog("Problem Getting Job Step Data.  Will Pause and try again", 2, EventLogEntryType.Information)
                    WriteErrorLog(ex.Message, 2, EventLogEntryType.Information)
                    GoTo FinishLoop
                End Try


                ScheduleCounter = 0

                Do Until ScheduleCounter = JobSteps.Tables(2).Rows.Count
                    hs = New Schedule
                    hs.ScheduleId = JobSteps.Tables(2).Rows(ScheduleCounter).Item("schedule_id")
                    hs.ScheduleName = JobSteps.Tables(2).Rows(ScheduleCounter).Item("schedule_name")
                    hs.Enabled = JobSteps.Tables(2).Rows(ScheduleCounter).Item("enabled")
                    hs.freq_type = JobSteps.Tables(2).Rows(ScheduleCounter).Item("freq_type")
                    hs.freq_interval = JobSteps.Tables(2).Rows(ScheduleCounter).Item("freq_interval")
                    hs.freq_subday_type = JobSteps.Tables(2).Rows(ScheduleCounter).Item("freq_subday_type")
                    hs.freq_subday_interval = JobSteps.Tables(2).Rows(ScheduleCounter).Item("freq_subday_interval")
                    hs.freq_relative_interval = JobSteps.Tables(2).Rows(ScheduleCounter).Item("freq_relative_interval")
                    hs.freq_recurrence_factor = JobSteps.Tables(2).Rows(ScheduleCounter).Item("freq_recurrence_factor")
                    hs.active_start_date = JobSteps.Tables(2).Rows(ScheduleCounter).Item("active_start_date")
                    hs.active_start_time = JobSteps.Tables(2).Rows(ScheduleCounter).Item("active_start_time")
                    hs.active_end_date = JobSteps.Tables(2).Rows(ScheduleCounter).Item("active_end_date")
                    hs.active_end_time = JobSteps.Tables(2).Rows(ScheduleCounter).Item("active_end_time")
                    hs.NextRunTime = StringDatetoDate(JobSteps.Tables(2).Rows(ScheduleCounter).Item("next_run_date")) + " " + StringTimeToDate(JobSteps.Tables(2).Rows(ScheduleCounter).Item("next_run_time"))
                    hs.ScheduleUid = JobSteps.Tables(2).Rows(ScheduleCounter).Item("schedule_uid")

                    If hs.freq_type = e_freq_type.Run_At_Agent_Startup Then
                        If StartUpPhase = True Then Jobs(job_id).NextRunDate = Now
                    Else
                        Jobs(job_id).NextRunDate = hs.NextRunTime
                    End If

                    WriteErrorLog("Next run time for schedule " + hs.ScheduleName + " is " + hs.NextRunTime.ToString, 10, EventLogEntryType.Information)

                    Jobs(job_id).Schedule(hs.ScheduleId) = hs

                    ScheduleCounter += 1
                Loop

                Jobs(job_id).PrepareJobSteps(JobSteps.Tables(1).Rows.Count)

                WriteErrorLog("Job has " + JobSteps.Tables(1).Rows.Count.ToString + " steps")
                StepCounter = 0
                While StepCounter <> JobSteps.Tables(1).Rows.Count
                    WriteErrorLog("Processed Step " + JobSteps.Tables(1).Rows(StepCounter).Item("step_id").ToString, 10, EventLogEntryType.Information)
                    JobStepId = JobSteps.Tables(1).Rows(StepCounter).Item("step_id")

                    Jobs(job_id).JobStep(JobStepId) = New JobStep

                    Jobs(job_id).JobStep(JobStepId).JobStepId = CInt(JobSteps.Tables(1).Rows(StepCounter).Item("step_id"))
                    Jobs(job_id).JobStep(JobStepId).JobStepName = JobSteps.Tables(1).Rows(StepCounter).Item("step_name").ToString
                    Jobs(job_id).JobStep(JobStepId).JobStepCommand = JobSteps.Tables(1).Rows(StepCounter).Item("command").ToString
                    Jobs(job_id).JobStep(JobStepId).JobStepOnFailure = CInt(JobSteps.Tables(1).Rows(StepCounter).Item("on_fail_action").ToString.Substring(0, 1))
                    Jobs(job_id).JobStep(JobStepId).JobStepOnFailureStepId = CInt(JobSteps.Tables(1).Rows(StepCounter).Item("on_fail_step_id").ToString)
                    Jobs(job_id).JobStep(JobStepId).JobStepOnSuccess = CInt(JobSteps.Tables(1).Rows(StepCounter).Item("on_success_action").ToString.Substring(0, 1))
                    Jobs(job_id).JobStep(JobStepId).JobStepOnSuccessStepId = CInt(JobSteps.Tables(1).Rows(StepCounter).Item("on_success_step_id").ToString)
                    Jobs(job_id).JobStep(JobStepId).DatabaseName = JobSteps.Tables(1).Rows(StepCounter).Item("database_name").ToString
                    Jobs(job_id).JobStep(JobStepId).JobStepOutputPath = JobSteps.Tables(1).Rows(StepCounter).Item("output_file_name").ToString
                    Jobs(job_id).JobStep(JobStepId).JobStepAdvancedFlags = CInt(JobSteps.Tables(1).Rows(StepCounter).Item("flags").ToString.Substring(0, JobSteps.Tables(1).Rows(StepCounter).Item("flags").ToString.IndexOf(" ")))
                    If JobSteps.Tables(1).Rows(StepCounter).Item("subsystem").ToString = "TSQL" Then
                        Jobs(job_id).JobStep(JobStepId).JobStepType = JobStep.JobStepTypes.TSQL
                    ElseIf JobSteps.Tables(1).Rows(StepCounter).Item("subsystem").ToString = "CmdExec" Then
                        Jobs(job_id).JobStep(JobStepId).JobStepType = JobStep.JobStepTypes.OperationSystemCommand
                        Jobs(job_id).JobStep(JobStepId).OsCommandExpectedReturnValue = CInt(JobSteps.Tables(1).Rows(StepCounter).Item("cmdexec_success_code").ToString)
                    End If

                    StepCounter += 1
                End While

                LoopCounter += 1
            End While

            If Jobs.Length > 1 Then
                WriteErrorLog("Looking for jobs that need to be run", 10, EventLogEntryType.Information)
                'Find the Jobs to Run this time
                For Each aJob In Jobs
                    If aJob.job_id <> "" Then
                        WriteErrorLog(aJob.job_name + " - " + aJob.NextRunDate.ToString, 10, EventLogEntryType.Information)
                        If aJob.NextRunDate.Date = Now.Date _
                            And aJob.NextRunDate.Hour = Now.Hour _
                            And aJob.NextRunDate.Minute = Now.Minute Then
                            JobsToRun(job_guid_list.GetJobId(aJob.job_id)) = aJob
                            WriteErrorLog("Marking job " + aJob.job_name + " as needing to be run", 10, EventLogEntryType.Information)
                        End If
                    End If
                Next

                'Run Jobs Which are Waiting to be executed
                aJob = Nothing
                For Each aJob In JobsToRun
                    If aJob.job_id <> "" Then
                        Dim NewJob As New JobRunner()
                        NewJob.ConnectionString = ConnectionString
                        NewJob.aJob = aJob
                        'NewJob.StartJobDelegate = AddressOf NewJob.StartJob
                        WriteErrorLog("Starting job " + aJob.job_name, 10, EventLogEntryType.Information)
                        Dim NewThread As New Thread(AddressOf NewJob.StartJob)
                        NewThread.IsBackground = True
                        NewThread.Start()
                    End If
                Next
            Else
                WriteErrorLog("No Jobs to Run", 10, EventLogEntryType.Information)
            End If
FinishLoop:
            LoopendTime = Now()

            UpdateJobSchedules(ConnectionString)
            StartUpPhase = False

            'If Now.Minute = 0 Or Now.Minute = 30 Then RemoveOldJobStepHistory(ConnectionString)

            'If DateDiff(DateInterval.Second, LoopStartTime, LoopendTime) = 0 Then Thread.Sleep(1000)
            'If (LoopendTime - LoopStartTime).Milliseconds < 1000 Then Thread.Sleep(LoopendTime - LoopStartTime)
            If (LoopendTime - LoopStartTime).Seconds < 60 Then

                WriteErrorLog("Thread sleeping for " + (60000 - (LoopendTime - LoopStartTime).Milliseconds).ToString + " milliseconds", 10, EventLogEntryType.Information)
                Thread.Sleep((60000 - (LoopendTime - LoopStartTime).Milliseconds))
                'End If
                'If Now.Second <> 0 Then 'If DateDiff(DateInterval.Second, LoopStartTime, LoopendTime) < 60 Then
                'WriteErrorLog("Thread sleeping for " + (60 - DateDiff(DateInterval.Second, LoopStartTime, LoopendTime)).ToString + " seconds.", 10)
                'Thread.Sleep((60 - DateDiff(DateInterval.Second, LoopStartTime, LoopendTime)) * 1000)
                'WriteErrorLog("Thread sleeping for " + ((60 - Now.Second) * 1000).ToString + " milliseconds", 10, EventLogEntryType.Information)
                'Thread.Sleep((60 - Now.Second) * 1000)

            Else
                WriteErrorLog("Thread ran for " + DateDiff(DateInterval.Second, LoopStartTime, LoopendTime).ToString + " thread not sleeping", 10, EventLogEntryType.Information)
            End If

        End While

        ChangeSQLConfig(ConnectionString, "show advanced", 1)
        ChangeSQLConfig(ConnectionString, "Agent XPs", 0)
        ChangeSQLConfig(ConnectionString, "show advanced", 0)
    End Sub

    Private Sub ChangeSQLConfig(ByVal ConnectionString As String, ByVal ConfigName As String, ByVal ConfigValue As Integer)
        Dim SQLConn As SqlConnection
        Dim SQLCmd As SqlCommand

        Try

            SQLConn = New SqlConnection(ConnectionString)
            SQLCmd = New SqlCommand

            SQLCmd.CommandText = "sp_configure"
            SQLCmd.CommandType = CommandType.StoredProcedure
            SQLCmd.Parameters.Add("@configname", SqlDbType.VarChar, 50)
            SQLCmd.Parameters.Item("@configname").Value = ConfigName
            SQLCmd.Parameters.Item("@configname").Direction = ParameterDirection.Input

            SQLCmd.Parameters.Add("@configvalue", SqlDbType.Int)
            SQLCmd.Parameters.Item("@configvalue").Value = ConfigValue
            SQLCmd.Parameters.Item("@configvalue").Direction = ParameterDirection.Input

            SQLConn.Open()
            SQLCmd.Connection = SQLConn
            SQLCmd.ExecuteNonQuery()
            SQLConn.Close()
            SQLCmd.Dispose()
            SQLConn = Nothing
            SQLCmd = Nothing
        Catch ex As Exception
            WriteErrorLog("Unable to change SQL Setting " + ConfigName + "(" + ConfigValue.ToString + ")", 2, EventLogEntryType.Error)
            WriteErrorLog(ex.Message, 1, EventLogEntryType.Error)
            'Exit Sub
        End Try


        Try

            SQLConn = New SqlConnection(ConnectionString)
            SQLCmd = New SqlCommand

            SQLCmd.CommandText = "reconfigure with override"
            SQLCmd.CommandType = CommandType.Text

            SQLConn.Open()
            SQLCmd.Connection = SQLConn
            SQLCmd.ExecuteNonQuery()
            SQLConn.Close()
            SQLCmd.Dispose()
            SQLConn = Nothing
            SQLCmd = Nothing
        Catch ex As Exception
            WriteErrorLog("Unable to reconfigure", 2, EventLogEntryType.Information)
            WriteErrorLog(ex.Message, 1, EventLogEntryType.Error)
            'Exit Sub
        End Try


    End Sub

    Private Sub CreateReplacementObjects(ByVal ConnectionString As String)
        WriteErrorLog("Verifying database objects", 1, EventLogEntryType.Information)
        CreateReplacementObject(ConnectionString, "xp_sqlagent_enum_jobs_SSA")
        CreateReplacementObject(ConnectionString, "sp_get_composite_job_info_SSA")
        CreateReplacementObject(ConnectionString, "sp_help_job_SSA")

    End Sub
    Private Sub CreateReplacementObject(ByVal ConnectionString As String, ByVal DbObject As String)
        Dim SQLcmd As SqlCommand
        Dim ExistingProcedureSource As String
        If DbObject = "xp_sqlagent_enum_jobs_SSA" Then
            If ObjectExists(ConnectionString, "xp_sqlagent_enum_jobs_SSA", "dbo") = True Then Exit Sub

            WriteErrorLog("Creating xp_sqlagent_enum_jobs_SSA", 2, EventLogEntryType.Information)
            SQLcmd = New SqlCommand
            SQLcmd.CommandText = "CREATE PROCEDURE xp_sqlagent_enum_jobs_SSA " + vbCrLf + _
                "   @can_see_all_running_jobs int, @job_owner sysname, @job_id uniqueidentifier AS " + vbCrLf + _
                "select sj.job_id, isnull(sjh.run_date, 0), isnull(sjh.run_time, 0), isnull(sjs.next_run_date, 0), isnull(sjs.next_run_time, 0), isnull(sjs.schedule_id, 0), 0 as requested_to_run," + vbCrLf + _
                "	0 as request_source, null as request_source_id, case when sjh.step_id = 0 then 1 else 0 end as running," + vbCrLf + _
                "	isnull(sjh.step_id, 0) as current_step, isnull(sjh.retries_attempted, 0) as current_retry_attempt," + vbCrLf + _
                "	case when sjh.job_id is null then 4 else case when sjh.step_id = 0 then 4" + vbCrLf + _
                "	else " + vbCrLf + _
                "		case when sjh.retries_attempted <> sjss.retry_attempts then" + vbCrLf + _
                "			3" + vbCrLf + _
                "		else" + vbCrLf + _
                "			1" + vbCrLf + _
                "		end" + vbCrLf + _
                "	end end as job_state" + vbCrLf + _
                "from sysjobs sj" + vbCrLf + _
                "left outer join sysjobhistory sjh on sj.job_id = sjh.job_id" + vbCrLf + _
                "	AND sjh.instance_id IN (SELECT instance_id FROM (SELECT job_id, max(instance_id) instance_id from sysjobhistory group by job_id) a)" + vbCrLf + _
                "left outer join (select job_id, min(dbo.agent_datetime(CASE WHEN next_run_date = 0 THEN NULL ELSE next_run_date END, CASE WHEN next_run_time = 0 THEN NULL ELSE next_run_time END)) rt from sysjobschedules group by job_id) sjs2 on sjs2.job_id = sj.job_id" + vbCrLf + _
                "left outer join sysjobschedules sjs on sjs2.job_id = sjs.job_id" + vbCrLf + _
                "   and sjs2.rt = dbo.agent_Datetime(case when sjs.next_run_date = 0 then null else sjs.next_run_date end, case when sjs.next_run_time = 0 then null else sjs.next_run_time end)" + vbCrLf + _
                "left outer join sysschedules ss on sjs.schedule_id = ss.schedule_id " + vbCrLf + _
                "	and ss.enabled = 1" + vbCrLf + _
                "left outer join sysjobsteps sjss on sj.job_id = sjss.job_id and sjh.step_id = sjss.step_id" + vbCrLf + _
                "WHERE sj.enabled = 1"
            SQLcmd.CommandType = CommandType.Text
            RunSQLStatement(ConnectionString, SQLcmd)
        End If
        If DbObject = "sp_get_composite_job_info_SSA" Then
            If ObjectExists(ConnectionString, "sp_get_composite_job_info_SSA", "dbo") = False Then
                ExistingProcedureSource = ""
                WriteErrorLog("Creating sp_get_composite_job_info", 2, EventLogEntryType.Information)
                GetProcedureSourceCode(ConnectionString, "sp_get_composite_job_info", ExistingProcedureSource)
                ExistingProcedureSource = ExistingProcedureSource.Replace("CREATE PROCEDURE sp_get_composite_job_info", "CREATE PROCEDURE sp_get_composite_job_info_SSA")
                ExistingProcedureSource = ExistingProcedureSource.Replace("master.dbo.xp_sqlagent_enum_jobs", "xp_sqlagent_enum_jobs_SSA")
                SQLcmd = New SqlCommand
                SQLcmd.CommandType = CommandType.Text
                SQLcmd.CommandText = ExistingProcedureSource
                RunSQLStatement(ConnectionString, SQLcmd)
            End If
        End If
        If DbObject = "sp_help_job_SSA" Then
            If ObjectExists(ConnectionString, "sp_help_job_SSA", "dbo") = True Then
                Exit Sub
            Else
                WriteErrorLog("Creating sp_help_job_SSA", 2, EventLogEntryType.Information)
                ExistingProcedureSource = ""
                GetProcedureSourceCode(ConnectionString, "sp_help_job", ExistingProcedureSource)
                ExistingProcedureSource = ExistingProcedureSource.Replace("CREATE PROCEDURE sp_help_job", "CREATE PROCEDURE sp_help_job_SSA")
                ExistingProcedureSource = ExistingProcedureSource.Replace(" sp_get_composite_job_info ", " sp_get_composite_job_info_SSA ")
                SQLcmd = New SqlCommand
                SQLcmd.CommandType = CommandType.Text
                SQLcmd.CommandText = ExistingProcedureSource
                RunSQLStatement(ConnectionString, SQLcmd)

            End If

        End If
    End Sub
    Private Sub GetProcedureSourceCode(ByVal ConnectionString As String, ByVal SourceDbObject As String, ByRef NewDbSource As String)
        Dim SQLConn As SqlConnection
        Dim SQLCmd As SqlCommand
        Dim SQLda As SqlDataAdapter
        Dim SQLResults As DataSet

        Try
            SQLConn = New SqlConnection(ConnectionString)
            SQLCmd = New SqlCommand

            SQLCmd.CommandText = "sp_helptext"
            SQLCmd.CommandType = CommandType.StoredProcedure

            SQLCmd.Parameters.Add("@objname", SqlDbType.VarChar, 255)
            SQLCmd.Parameters.Item("@objname").Value = SourceDbObject

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
        Catch ex As Exception
            WriteErrorLog("Unable to setup " + SourceDbObject + "_SSA", 2, EventLogEntryType.Information)
            WriteErrorLog(ex.Message, 1, EventLogEntryType.Error)
            Exit Sub
        End Try

        NewDbSource = ""

        Dim RowId As Integer = 0
        Debug.Print(SQLResults.Tables(0).Rows.Count)
        While SQLResults.Tables(0).Rows.Count > RowId
            NewDbSource += SQLResults.Tables(0).Rows(RowId).Item("Text").ToString + vbCrLf
            RowId += 1
        End While

    End Sub
    Private Sub CreateExtendedStoredProcedure(ByVal ConnectionString As String, ByVal ObjectName As String, ByVal DLL As String)
        Dim SQLConn As SqlConnection
        Dim SQLCmd As SqlCommand

        Try
            SQLConn = New SqlConnection(ConnectionString)
            SQLCmd = New SqlCommand

            SQLCmd.CommandText = "sp_addextendedproc"
            SQLCmd.CommandType = CommandType.StoredProcedure

            SQLCmd.Parameters.Add("@functname", SqlDbType.VarChar, 255)
            SQLCmd.Parameters.Item("@functname").Value = ObjectName

            SQLCmd.Parameters.Add("@dllname", SqlDbType.VarChar, 255)
            SQLCmd.Parameters.Item("@dllname").Value = DLL

            SQLConn.Open()
            SQLCmd.Connection = SQLConn
            SQLCmd.ExecuteNonQuery()

            SQLConn.Close()
            SQLCmd.Dispose()
            SQLConn = Nothing
            SQLCmd = Nothing
        Catch ex As Exception
            WriteErrorLog("Unable to create extended stored procedure " + ObjectName, 2, EventLogEntryType.Information)
            WriteErrorLog(ex.Message, 1, EventLogEntryType.Error)
            Exit Sub
        End Try
    End Sub
    Private Sub DropExtendedStoredProcedure(ByVal ConnectionString As String, ByVal DbObject As String)
        Dim SQLConn As SqlConnection
        Dim SQLCmd As SqlCommand

        Try
            SQLConn = New SqlConnection(ConnectionString)
            SQLCmd = New SqlCommand

            SQLCmd.CommandText = "sp_dropextendedproc"
            SQLCmd.CommandType = CommandType.StoredProcedure

            SQLCmd.Parameters.Add("@functname", SqlDbType.VarChar, 255)
            SQLCmd.Parameters.Item("@functname").Value = DbObject

            SQLConn.Open()
            SQLCmd.Connection = SQLConn
            SQLCmd.ExecuteNonQuery()

            SQLConn.Close()
            SQLCmd.Dispose()
            SQLConn = Nothing
            SQLCmd = Nothing
            WriteErrorLog("Dropped extended stored procedure " + DbObject, 1, EventLogEntryType.Information)
        Catch ex As Exception
            WriteErrorLog("Unable to drop extended stored procedure " + DbObject, 1, EventLogEntryType.Error)
            WriteErrorLog(ex.Message, 1, EventLogEntryType.Error)
            Exit Sub
        End Try

    End Sub
    Private Overloads Sub MarkRunningJobsAsCancled(ByVal ConnectionString As String)
        MarkRunningJobsAsCancled(ConnectionString, False)
    End Sub
    Private Overloads Sub MarkRunningJobsAsCancled(ByVal ConnectionString As String, ByVal SkipCheck As Boolean)
        Dim SQLCmd As SqlCommand
        Dim SQLConn As SqlConnection
        Dim SQLda As SqlDataAdapter
        Dim SQLResults As DataSet
        Dim LoopCounter As Integer = 0
        Dim aJob As Job


        WriteErrorLog("Starting The Cleanup Loop", 11, EventLogEntryType.Information)
        'Get the Job data

        SQLResults = New DataSet()

        Try

            SQLConn = New SqlConnection(ConnectionString)
            SQLCmd = New SqlCommand

            SQLCmd.CommandText = "sp_help_job_SSA"
            SQLCmd.CommandType = CommandType.StoredProcedure
            SQLCmd.Parameters.Add("@Enabled", SqlDbType.Bit)
            SQLCmd.Parameters.Item("@Enabled").Value = 1
            SQLCmd.Parameters.Item("@Enabled").Direction = ParameterDirection.Input

            SQLCmd.Parameters.Add("@execution_status", SqlDbType.Int)
            SQLCmd.Parameters.Item("@execution_status").Value = 1
            SQLCmd.Parameters.Item("@execution_status").Direction = ParameterDirection.Input

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
            WriteErrorLog("Problem cleaning up list of jobs.  Some jobs may not run correctly.", 1, EventLogEntryType.Error)
            WriteErrorLog(ex.Message, 2, EventLogEntryType.Information)
            WriteErrorLog(SQLConn.ServerVersion.ToString, EventLogEntryType.Information)
            If SQLConn.ServerVersion.ToString.Substring(0, 2) >= "10" And SkipCheck = False Then
                SQL2008Patch(ConnectionString)
                Exit Sub
            End If

        End Try
        ' Thread.Sleep(20000)

        WriteErrorLog(SQLResults.Tables(0).Rows.Count.ToString + " job(s) were still running when the agent was shut down.", EventLogEntryType.Information)
        Debug.Print(SQLResults.Tables(0).Rows.Count)
        While LoopCounter < SQLResults.Tables(0).Rows.Count
            aJob = New Job
            aJob.job_id = SQLResults.Tables(0).Rows(LoopCounter).Item("job_id").ToString
            WriteErrorLog("Shuting down job " + aJob.job_id, 10, EventLogEntryType.Information)

            ChangeJobstepStatus(ConnectionString, aJob, _
                JobStatus.Failed, Now.TimeOfDay, 0, _
                "The job was cancled as a result of a Standalong SQL Agent stutdown.", _
                SQLResults.Tables(0).Rows(LoopCounter).Item("next_run_date").ToString, _
                SQLResults.Tables(0).Rows(LoopCounter).Item("next_run_time").ToString)

            LoopCounter += 1
        End While

    End Sub
    Private Sub SQL2008Patch(ByVal ConnectionString As String)
        WriteErrorLog("Patching SQL 2008 MSDB data so the service doesn't break.", EventLogEntryType.Information)
        Dim SQLCmd As SqlCommand
        Dim SQLConn As SqlConnection
        Dim SQLda As SqlDataAdapter
        Dim SQLResults As DataSet

        SQLResults = New DataSet()

        Try

            SQLConn = New SqlConnection(ConnectionString)
            SQLCmd = New SqlCommand

            SQLCmd.CommandText = "update sysjobschedules " & _
                "set next_run_date = '20090101'," & _
                "next_run_time = '20000'" & _
                "where schedule_id = (select schedule_id from sysschedules WHERE name = 'syspolicy_purge_history_schedule')" & _
                "   and next_run_date = '0'"
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
            WriteErrorLog("Problem cleaning job schedule for syspolicy_purge_history_schedule", 1, EventLogEntryType.Error)
            WriteErrorLog(ex.Message, 2, EventLogEntryType.Information)
        End Try
        MarkRunningJobsAsCancled(ConnectionString, True)
    End Sub
    Private Sub RemoveOldJobStepHistory(ByVal ConnectionString As String)
        Dim InstanceHistorySettings As JobHistory

        Dim SQLConn As SqlConnection
        Dim SQLCmd As SqlCommand
        Dim SQLda As SqlDataAdapter
        Dim SQLResults As DataSet

        SQLResults = New DataSet
        Try
            SQLConn = New SqlConnection(ConnectionString)
            SQLCmd = New SqlCommand
            SQLCmd.CommandText = "sp_get_sqlagent_properties"
            SQLCmd.CommandType = CommandType.StoredProcedure

            SQLConn = New SqlConnection(ConnectionString)
            SQLConn.Open()
            SQLCmd.Connection = SQLConn
            SQLCmd.ExecuteNonQuery()
            SQLda = New SqlDataAdapter(SQLCmd)
            SQLda.Fill(SQLResults)

            SQLConn.Close()
            SQLCmd.Dispose()
            SQLConn = Nothing

            SQLCmd = Nothing
        Catch ex As Exception
            WriteErrorLog("Error getting SQL Server Agent Settings Data.", 1, EventLogEntryType.Error)
            WriteErrorLog(ex.Message, 1, EventLogEntryType.Information)
        End Try

        InstanceHistorySettings = New JobHistory
        InstanceHistorySettings.ConfiguredJobHistory = SQLResults.Tables(0).Rows(0).Item("jobhistory_max_rows")
        InstanceHistorySettings.ConfigureJobHistoryPerJob = SQLResults.Tables(0).Rows(0).Item("jobhistory_max_rows_per_job")

        SQLResults = New DataSet

        Try
            SQLConn = New SqlConnection(ConnectionString)
            SQLCmd = New SqlCommand
            SQLCmd.CommandText = "select count(*) ct from sysjobhistory with (nolock); select job_id, count(*) ct from sysjobhistory with (nolock) group by job_id"
            SQLCmd.CommandType = CommandType.Text

            SQLConn = New SqlConnection(ConnectionString)
            SQLConn.Open()
            SQLCmd.Connection = SQLConn
            SQLCmd.ExecuteNonQuery()
            SQLda = New SqlDataAdapter(SQLCmd)
            SQLda.Fill(SQLResults)

            SQLConn.Close()
            SQLCmd.Dispose()
            SQLConn = Nothing

            SQLCmd = Nothing
        Catch ex As Exception
            WriteErrorLog("Error getting SQL Server Agent Settings Data.", 1, EventLogEntryType.Error)
            WriteErrorLog(ex.Message, 1, EventLogEntryType.Information)
        End Try

        InstanceHistorySettings.InitalizeJobData(SQLResults.Tables(1).Rows.Count)
        InstanceHistorySettings.TotalHistorySize = SQLResults.Tables(0).Rows(0).Item("ct")

        Dim LoopCounter As Integer = 0
        While LoopCounter <> SQLResults.Tables(1).Rows.Count
            InstanceHistorySettings.JobData(SQLResults.Tables(1).Rows(LoopCounter).Item("job_id")).JobId = SQLResults.Tables(1).Rows(LoopCounter).Item("job_id")
            InstanceHistorySettings.JobData(SQLResults.Tables(1).Rows(LoopCounter).Item("job_id")).JobHistorySize = SQLResults.Tables(1).Rows(LoopCounter).Item("ct")

            If InstanceHistorySettings.JobData(SQLResults.Tables(1).Rows(LoopCounter).Item("job_id")).RowsToDelete <> 0 Then
                SQLCmd = New SqlCommand
                SQLCmd.CommandType = CommandType.Text
                SQLCmd.CommandTimeout = 15
                SQLCmd = "DELETE FROM dbo.jobstephistory" + vbCrLf + _
                    "where instance_id IN (SELECT TOP (" + InstanceHistorySettings.JobData(SQLResults.Tables(1).Rows(LoopCounter).Item("job_id")).RowsToDelete.ToString + ") instance_id FROM dbo.sysjobhistory WHERE job_id = '" + SQLResults.Tables(1).Rows(LoopCounter).Item("job_id") + "' ORDER BY instance_id)"
                InstanceHistorySettings.TotalHistorySize = InstanceHistorySettings.TotalHistorySize - InstanceHistorySettings.JobData(SQLResults.Tables(1).Rows(LoopCounter).Item("job_id")).RowsToDelete

                RunSQLStatement(ConnectionString, SQLCmd)
            End If

            LoopCounter += 1
        End While

        If InstanceHistorySettings.TotalHistorySize > InstanceHistorySettings.ConfiguredJobHistory Then
            SQLCmd = New SqlCommand
            SQLCmd.CommandType = CommandType.Text
            SQLCmd.CommandTimeout = 15
            SQLCmd.CommandText = "DELETE FROM dbo.jobstephistory" + vbCrLf + _
                "where isntance_id in (SELECT TOP (" + (InstanceHistorySettings.ConfiguredJobHistory - InstanceHistorySettings.TotalHistorySize) + ") instance_id from dbo.sysjobhistory ORDER By instance_id)"
            RunSQLStatement(ConnectionString, SQLCmd)
        End If

    End Sub
End Class
