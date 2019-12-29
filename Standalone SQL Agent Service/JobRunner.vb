Imports System.Diagnostics
Imports System.Data
Imports System.Data.Sql
Imports System.Data.SqlClient
Imports System.IO
'Imports Standalone_SQL_Agent_Service.SharedSubs
Imports Standalone_SQL_Agent_Service.BaseCode
Imports System.Threading
Imports SSA.CommonControl
Imports SSA.CommonControl.SharedCode
Public Class JobRunner
    Delegate Sub StartJobDelegate(ByVal ConnectionString As String, ByVal aJob As Job)

    Public ConnectionString As String
    Public aJob As Job
    Public StartDeligate As StartJobDelegate

    Public Sub StartJob()
        RunJob(ConnectionString, aJob)
    End Sub

    Public Sub RunJob(ByVal ConnectionString As String, ByVal JobInfo As Job)

        WriteErrorLog(JobInfo.job_name + " has started", 10, EventLogEntryType.Information)
        Dim JobSteps(JobInfo.JobStepCount) As JobStep
        Dim JobStep As JobStep
        Dim ResultString As String
        Dim ResultCode As Integer
        Dim Duration As TimeSpan
        Dim Retries As Integer
        Dim StartTime As Date, EndTime As Date
        Dim NextjobStep As Integer

        JobStep = New JobStep
        JobStep = JobInfo.JobStep(JobInfo.StartStepId)

        Dim JobStartDateTime As Date

        JobStartDateTime = Now
        Do
            ResultString = ""

            StartTime = Now
            WriteErrorLog(JobInfo.job_name + " Step " + JobStep.JobStepId.ToString + " starting.", 11, EventLogEntryType.Information)

            If JobStep.JobStepType = JobStep.JobStepTypes.TSQL Then
                RunJobStepTSQL(ConnectionString, JobStep.DatabaseName, JobStep.JobStepCommand, ResultString, ResultCode)
            ElseIf JobStep.JobStepType = JobStep.JobStepTypes.OperationSystemCommand Then
                RunJobStepCmdExec(JobStep.JobStepCommand, ResultCode, ResultString, JobStep.OsCommandExpectedReturnValue.ToString, JobInfo.job_id)
            End If
            WriteErrorLog(JobInfo.job_name + " Step " + JobStep.JobStepId.ToString + " finished.", 11, EventLogEntryType.Information)

            If JobStep.JobStepOutputPath <> "" Then WriteOutputToFile(JobStep.JobStepOutputPath, JobStep.JobStepOutputPathAppendFlag, ResultString)
            EndTime = Now
            Duration = EndTime - StartTime

            ChangeJobstepStatus(ConnectionString, JobInfo, JobStep.JobStepId, ResultCode, Duration, Retries, ResultString, CInt(DateDateToStringDate(StartTime.Date)), CInt(DateTimeToStringTime(StartTime.TimeOfDay)))

            If ResultCode = JobStatus.Succeeded Then
                If JobStep.JobStepOnSuccess = JobStepOnNext.QuitWithSuccess Then
                    ChangeJobstepStatus(ConnectionString, JobInfo, JobStatus.Succeeded, (Now - StartTime), Retries, "Job ran successfully by the Standalone SQL Agent", CInt(DateDateToStringDate(StartTime.Date)), CInt(DateTimeToStringTime(StartTime.TimeOfDay)))
                    Exit Do
                ElseIf JobStep.JobStepOnSuccess = JobStepOnNext.QuitWithFailure Then
                    ChangeJobstepStatus(ConnectionString, JobInfo, JobStatus.Failed, (Now - StartTime), Retries, "Job ran unsuccessfully by the Standalone SQL Agent", CInt(DateDateToStringDate(StartTime.Date)), CInt(DateTimeToStringTime(StartTime.TimeOfDay)))
                    Exit Do
                ElseIf JobStep.JobStepOnSuccess = JobStepOnNext.GoToNextStep Then
                    WriteErrorLog("Checking if " + JobInfo.job_name + " Step " + (JobStep.JobStepId + 1).ToString + "(" + JobInfo.JobStepCount.ToString + ") exists")
                    If JobInfo.JobStepExists(JobStep.JobStepId + 1) = False Then
                        WriteErrorLog(JobInfo.job_name + " Step " + (JobStep.JobStepId + 1).ToString + "(" + JobInfo.JobStepCount.ToString + ") doesn't exist")
                        ChangeJobstepStatus(ConnectionString, JobInfo, JobStatus.Failed, (Now - StartTime), Retries, "Job ran unsuccessfully by the Standalone SQL Agent.  Job Config is invalid.", CInt(DateDateToStringDate(StartTime.Date)), CInt(DateTimeToStringTime(StartTime.TimeOfDay)))
                        Exit Do
                    End If
                    NextjobStep = JobInfo.JobStep(JobStep.JobStepId + 1).JobStepId
                    JobStep = New JobStep
                    JobStep = JobInfo.JobStep(NextjobStep)
                ElseIf JobStep.JobStepOnSuccess = JobStepOnNext.GoToStepId Then
                    If JobInfo.JobStepExists(JobStep.JobStepOnSuccessStepId) = False Then
                        ChangeJobstepStatus(ConnectionString, JobInfo, JobStatus.Failed, (Now - StartTime), Retries, "Job ran unsuccessfully by the Standalone SQL Agent.  Job Config is invalid.", CInt(DateDateToStringDate(StartTime.Date)), CInt(DateTimeToStringTime(StartTime.TimeOfDay)))
                        Exit Do
                    End If
                    NextjobStep = JobInfo.JobStep(JobStep.JobStepOnSuccessStepId).JobStepId
                    JobStep = New JobStep
                    JobStep = JobInfo.JobStep(NextjobStep)
                End If
            ElseIf ResultCode = JobStatus.Failed Then
                If JobStep.JobStepOnFailure = JobStepOnNext.QuitWithSuccess Then
                    ChangeJobstepStatus(ConnectionString, JobInfo, JobStatus.Succeeded, (Now - StartTime), Retries, "Job ran successfully by the Standalone SQL Agent", CInt(DateDateToStringDate(StartTime.Date)), CInt(DateTimeToStringTime(StartTime.TimeOfDay)))
                    Exit Do
                ElseIf JobStep.JobStepOnFailure = JobStepOnNext.QuitWithFailure Then
                    ChangeJobstepStatus(ConnectionString, JobInfo, JobStatus.Failed, (Now - StartTime), Retries, "Job ran unsuccessfully by the Standalone SQL Agent", CInt(DateDateToStringDate(StartTime.Date)), CInt(DateTimeToStringTime(StartTime.TimeOfDay)))
                    Exit Do
                ElseIf JobStep.JobStepOnFailure = JobStepOnNext.GoToNextStep Then
                    If JobInfo.JobStepExists(JobStep.JobStepId + 1) = False Then
                        ChangeJobstepStatus(ConnectionString, JobInfo, 0, JobStatus.Failed, (Now - StartTime), Retries, "Job ran unsuccessfully by the Standalone SQL Agent.  Job Config is invalid.", CInt(DateDateToStringDate(StartTime.Date)), CInt(DateTimeToStringTime(StartTime.TimeOfDay)))
                        Exit Do
                    End If
                    NextjobStep = JobInfo.JobStep(JobStep.JobStepId + 1).JobStepId
                    JobStep = New JobStep
                    JobStep = JobInfo.JobStep(NextjobStep)
                ElseIf JobStep.JobStepOnFailure = JobStepOnNext.GoToStepId Then
                    If JobInfo.JobStepExists(JobStep.JobStepOnSuccessStepId) = False Then
                        ChangeJobstepStatus(ConnectionString, JobInfo, 0, JobStatus.Failed, (Now - StartTime), Retries, "Job ran unsuccessfully by the Standalone SQL Agent.  Job Config is invalid.", CInt(DateDateToStringDate(StartTime.Date)), CInt(DateTimeToStringTime(StartTime.TimeOfDay)))
                        Exit Do
                    End If
                    NextjobStep = JobInfo.JobStep(JobStep.JobStepOnFailureStepId).JobStepId
                    JobStep = New JobStep
                    JobStep = JobInfo.JobStep(NextjobStep)
                End If
            End If
        Loop
    End Sub
    Private Sub RunJobStepTSQL(ByVal ConnetionString As String, ByVal DatabaseName As String, ByVal TSQL As String, ByRef ResultString As String, ByRef Resultcode As Integer)
        Dim SQLConn As SqlConnection
        Dim SQLCmd As SqlCommand
        Dim SQLda As SqlDataAdapter
        Dim SQLResults As DataSet
        SQLCmd = New SqlCommand
        SQLda = New SqlDataAdapter
        SQLConn = New SqlConnection
        SQLResults = New DataSet

        Try

            SQLCmd.CommandText = TSQL
            SQLCmd.CommandType = CommandType.Text

            SQLConn = New SqlConnection(ConnectionString)
            SQLConn.Open()
            SQLCmd.Connection = SQLConn
            SQLCmd.CommandTimeout = 0
            SQLCmd.ExecuteNonQuery()
            SQLda = New SqlDataAdapter(SQLCmd)
            SQLda.Fill(SQLResults)
            SQLConn.Close()
            SQLCmd.Dispose()
            SQLConn = Nothing
            SQLda = Nothing
            SQLCmd = Nothing
            Resultcode = JobStatus.Succeeded
        Catch ex As Exception
            WriteErrorLog("Job Step Execution Failed.", 1, EventLogEntryType.Error)
            WriteErrorLog(ex.Message, 2, EventLogEntryType.Information)
            ResultString = ex.Message
            Resultcode = JobStatus.Failed
        End Try

        ResultString = ""
        Dim Table As DataTable
        Dim LoopCounter As Integer, ColumnCounter As Integer
        For Each Table In SQLResults.Tables
            ColumnCounter = 0
            While ColumnCounter <> Table.Columns.Count
                ResultString += Table.Columns.Item(ColumnCounter).ToString
                If ColumnCounter + 1 <> Table.Columns.Count Then ResultString += vbTab
                ColumnCounter += 1
            End While
            ResultString += vbCrLf
            LoopCounter = 0
            ColumnCounter = 0
            While LoopCounter <> Table.Rows.Count
                While ColumnCounter <> Table.Columns.Count
                    ResultString += Table.Rows(LoopCounter).Item(ColumnCounter).ToString
                    If ColumnCounter + 1 <> Table.Columns.Count Then ResultString += vbTab
                    ColumnCounter += 1
                End While
                ResultString += vbCrLf
                LoopCounter += 1
            End While
        Next
    End Sub
    Private Sub RunJobStepCmdExec(ByVal CommandLine As String, ByRef ResultCode As Integer, ByRef ResultString As String, ByVal ExpectedStatusCode As Integer, ByVal job_id As String)
        Dim ShellProcess As Process
        Dim sOutput As String = ""
        Dim sError As String = ""
        Dim TempFile As String
        ResultString = ""

        TempFile = GetInstallPath() + job_id + ".bat"
        DeleteFile(TempFile)



        Try

            Dim objReader As StreamWriter
            objReader = New StreamWriter(TempFile, True)
            objReader.WriteLine(CommandLine)
            objReader.Close()

            ShellProcess = New Process
            ShellProcess.StartInfo.FileName = TempFile
            ShellProcess.StartInfo.RedirectStandardOutput = True
            ShellProcess.StartInfo.RedirectStandardError = True
            ShellProcess.StartInfo.CreateNoWindow = True
            ShellProcess.StartInfo.UseShellExecute = False

            ShellProcess.Start()

            sOutput = ShellProcess.StandardOutput.ReadToEnd()
            sError = ShellProcess.StandardError.ReadToEnd()

            If ShellProcess.ExitCode <> ExpectedStatusCode Then
                ResultCode = JobStatus.Failed
            Else
                ResultCode = JobStatus.Succeeded
            End If

            If ResultString.Length > 3000 Then ResultString = ResultString.Substring(0, 3000)
            ResultString = "Return Code: " + ShellProcess.ExitCode.ToString + vbCrLf + ResultString

        Catch ex As Exception
            ResultString = ex.Message
            ResultCode = JobStatus.Failed
        End Try

        DeleteFile(TempFile)

        If sError <> "" Then ResultString = ResultString + sError
        If sOutput <> "" Then ResultString = ResultString + sOutput

    End Sub
    Public Sub WriteOutputToFile(ByVal FileName As String, ByVal AppendToFile As Boolean, ByVal DataToWrite As String)
        Dim objReader As StreamWriter
        Try
            objReader = New StreamWriter(FileName, True)
            objReader.WriteLine(DataToWrite)
            objReader.Close()
        Catch Ex As Exception
            WriteErrorLog("Unable to write job step output to " + FileName, EventLogEntryType.Error)
            'ErrInfo = Ex.Message
            'MsgBox(Ex.Message)
        End Try
    End Sub
    Public Overloads Shared Sub ChangeJobstepStatus(ByVal ConnectionString As String, ByVal aJob As Job, ByVal Status As Integer, ByVal Duration As TimeSpan, ByVal Retries As Integer, ByVal message As String, ByVal run_date As Integer, ByVal run_time As Integer)
        ChangeJobstepStatus(ConnectionString, aJob.job_id, 0, Status, Duration, Retries, message, run_date, run_time)
    End Sub

    Public Overloads Shared Sub ChangeJobStepStatus(ByVal ConnectionString As String, ByVal aJob As Job, ByVal JobStepId As Integer, ByVal Status As Integer, ByVal Duration As TimeSpan, ByVal Retries As Integer, ByVal message As String, ByVal run_date As Integer, ByVal run_time As Integer)
        ChangeJobstepStatus(ConnectionString, aJob.job_id, JobStepId, Status, Duration, Retries, message, run_date, run_time)
    End Sub
    Public Overloads Shared Sub ChangeJobStepStatus(ByVal ConnectionString As String, ByVal JobId As String, ByVal JobStepId As Integer, ByVal Status As Integer, ByVal Duration As TimeSpan, ByVal Retries As Integer, ByVal message As String, ByVal run_date As Integer, ByVal run_time As Integer)
        Dim SQLCmd As SqlCommand

        SQLCmd = New SqlCommand

        SQLCmd.CommandText = "sp_sqlagent_log_jobhistory"
        SQLCmd.CommandType = CommandType.StoredProcedure
        SQLCmd.Parameters.Add("@job_id", SqlDbType.VarChar, 40)
        SQLCmd.Parameters.Add("@step_id", SqlDbType.Int)
        SQLCmd.Parameters.Add("@message", SqlDbType.VarChar, 4000)
        SQLCmd.Parameters.Add("@run_status", SqlDbType.Int)
        SQLCmd.Parameters.Add("@run_date", SqlDbType.Int)
        SQLCmd.Parameters.Add("@run_time", SqlDbType.Int)
        SQLCmd.Parameters.Add("@run_duration", SqlDbType.Int)
        SQLCmd.Parameters.Add("@retries_attempted", SqlDbType.Int)

        SQLCmd.Parameters.Item("@job_id").Value = JobId
        SQLCmd.Parameters.Item("@step_id").Value = JobStepId
        SQLCmd.Parameters.Item("@message").Value = message
        SQLCmd.Parameters.Item("@run_status").Value = Status
        SQLCmd.Parameters.Item("@run_date").Value = run_date
        SQLCmd.Parameters.Item("@run_time").Value = run_time
        SQLCmd.Parameters.Item("@run_duration").Value = Duration.TotalSeconds
        SQLCmd.Parameters.Item("@retries_attempted").Value = 0

        RunSQLStatement(ConnectionString, SQLCmd)
    End Sub
    Public Enum JobStatus
        Failed = 0
        Succeeded = 1
        Retry = 2
        Cancled = 3
        InProgress = 4
    End Enum
    Private Enum LastRunOutcome
        Failed = 0
        Succeeded = 1
        Retry = 2
        Cancled = 3
        Unknown = 5
    End Enum
    Private Enum JobStepOnNext
        QuitWithSuccess = 1
        QuitWithFailure = 2
        GoToNextStep = 3
        GoToStepId = 4
    End Enum
End Class