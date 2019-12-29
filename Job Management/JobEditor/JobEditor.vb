Imports System.Threading
Imports SSA.CommonControl
Imports SSA.CommonControl.SharedCode
Imports System.Data
Imports System.Data.SqlClient
Public Class JobEditor
    Public aJob As Job

    Private Sub CancelButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CancelButton.Click
        Me.Close()
    End Sub
    Public Sub DisplayData()
        DisplayDataTab1()
        DisplayDataTab2()
        'Tab 3 data comes from Tab2 procedure since we already have the data
        DisplayDataTab4()
    End Sub
    Public Sub SetupForNewJob()
        LoadCategoryList(True)
        EnableTab4DropDowns()
        aJob = New Job
        aJob.PrepareJobSteps(0)
        Button6.Enabled = False
        TabControl1.TabPages.Item(2).Dispose()
    End Sub
    Private Sub LoadCategoryList(ByVal IgnoreJob As Boolean)
        Dim LoopCounter As Integer = 0
        Dim SQLCmd As SqlCommand
        Dim Results As DataSet

        SQLCmd = New SqlCommand
        SQLCmd.CommandType = CommandType.StoredProcedure
        SQLCmd.CommandText = "sp_help_category"
        SQLCmd.Parameters.Add("@type", SqlDbType.VarChar, 5)
        SQLCmd.Parameters.Item("@type").Value = "LOCAL"

        Try
            Results = RunSQLStatement(ServerJobList.ServerInstance.ConnectionString, SQLCmd)

        Catch ex As Exception
            MsgBox("Error retreiving Category List." & vbCrLf & ex.Message)
            Exit Sub
        End Try

        Category.Items.Clear()
        If IgnoreJob = False Then Debug.Print(aJob.JobCategory)

        LoopCounter = 0
        Do Until LoopCounter = Results.Tables(0).Rows.Count
            Category.Items.Add(Results.Tables(0).Rows(LoopCounter).Item("name"))
            If IgnoreJob = False Then
                If Results.Tables(0).Rows(LoopCounter).Item("name") = aJob.JobCategory Then Category.SelectedIndex = LoopCounter
            End If
            LoopCounter += 1
        Loop

        If IgnoreJob = True Then
            Category.SelectedIndex = 0
        End If

        SQLCmd.Dispose()
        Results = Nothing
    End Sub
    Private Sub DisplayDataTab1()
        Me.JobName.Text = aJob.job_name
        Me.OwnerUserName.Text = aJob.JobOwner

        Me.Description.Text = aJob.Description
        Me.JobEnabled.Checked = aJob.Enabled
        Me.Created.Text = aJob.CreateDate.ToString
        Me.LastModified.Text = aJob.ModifyDate.ToString

        'Get the last run date and time
        Dim LoopCounter As Integer = 0
        Dim SQLCmd As SqlCommand
        Dim Connection As SqlConnection
        Dim Results As DataSet
        Dim SQLda As SqlDataAdapter
        SQLCmd = New SqlCommand
        SQLCmd.CommandType = CommandType.StoredProcedure
        SQLCmd.CommandText = "sp_help_jobhistory"
        SQLCmd.Parameters.Add("@job_id", SqlDbType.VarChar, 50)
        SQLCmd.Parameters.Item("@job_id").Value = aJob.job_id
        Try
            Results = RunSQLStatement(ServerJobList.ServerInstance.ConnectionString, SQLCmd)

        Catch ex As Exception
            MsgBox("Error retreiving job last run time." & vbCrLf & ex.Message)
            Exit Sub
        End Try
        LoadCategoryList(False)
        If Results.Tables(0).Rows.Count <> 0 Then Me.LastExecuted.Text = StringDatetoDate(Results.Tables(0).Rows(0).Item("run_date")) + " " + StringTimeToDate(Results.Tables(0).Rows(0).Item("run_time"))
        SQLCmd.Dispose()
        Results.Dispose()
        Results = Nothing


    End Sub
    Private Sub DisplayDataTab2()
        'Also handles tab 3
        Dim SQLCmd As SqlCommand
        Dim Results As DataSet
        SQLCmd = New SqlCommand
        SQLCmd.CommandType = CommandType.StoredProcedure
        SQLCmd.CommandText = "sp_help_job"
        SQLCmd.Parameters.Add("@job_id", SqlDbType.VarChar, 50)
        SQLCmd.Parameters.Item("@job_id").Value = aJob.job_id
        
        Try
            Results = RunSQLStatement(ServerJobList.ServerInstance.ConnectionString, SQLCmd)
        Catch ex As Exception
            MsgBox("Error retreiving job last run time." & vbCrLf & ex.Message)
            Exit Sub
        End Try

        JobStepList.Items.Clear()
        StartStep.Items.Clear()
        Debug.Print(Results.Tables(1).Rows.Count.ToString + " steps to process.")
        aJob.PrepareJobSteps(Results.Tables(1).Rows.Count)
        Dim LoopCounter As Integer = 0
        While LoopCounter <> Results.Tables(1).Rows.Count
            JobStepList.Items.Add(Results.Tables(1).Rows(LoopCounter).Item("step_id"))
            JobStepList.Items(LoopCounter).SubItems.Add(Results.Tables(1).Rows(LoopCounter).Item("step_name"))
            JobStepList.Items(LoopCounter).SubItems.Add(Results.Tables(1).Rows(LoopCounter).Item("subsystem"))
            JobStepList.Items(LoopCounter).SubItems.Add(Results.Tables(1).Rows(LoopCounter).Item("on_success_action").ToString.Substring(3, Len(Results.Tables(1).Rows(LoopCounter).Item("on_success_action")) - 4))
            JobStepList.Items(LoopCounter).SubItems.Add(Results.Tables(1).Rows(LoopCounter).Item("on_fail_action").ToString.Substring(3, Len(Results.Tables(1).Rows(LoopCounter).Item("on_fail_action")) - 4))

            StartStep.Items.Add(Results.Tables(1).Rows(LoopCounter).Item("step_name"))

            aJob.JobStep(LoopCounter).JobStepId = Results.Tables(1).Rows(LoopCounter).Item("step_id")
            aJob.JobStep(LoopCounter).JobStepName = Results.Tables(1).Rows(LoopCounter).Item("step_name")
            If Results.Tables(1).Rows(LoopCounter).Item("subsystem") = "TSQL" Then
                aJob.JobStep(LoopCounter).JobStepType = JobStep.JobStepTypes.TSQL
            ElseIf Results.Tables(1).Rows(LoopCounter).Item("subsystem") = "CMDEXEC" Then
                aJob.JobStep(LoopCounter).JobStepType = JobStep.JobStepTypes.OperationSystemCommand
            End If

            aJob.JobStep(LoopCounter).IsNewStep = False
            aJob.JobStep(LoopCounter).JobStepOnSuccess = Results.Tables(1).Rows(LoopCounter).Item("on_success_action").ToString.Substring(0, 1)
            aJob.JobStep(LoopCounter).JobStepOnFailure = Results.Tables(1).Rows(LoopCounter).Item("on_fail_action").ToString.Substring(0, 1)
            aJob.JobStep(LoopCounter).JobStepOnSuccessStepId = Results.Tables(1).Rows(LoopCounter).Item("on_success_step_id")
            aJob.JobStep(LoopCounter).JobStepOnFailureStepId = Results.Tables(1).Rows(LoopCounter).Item("on_fail_step_id")
            aJob.JobStep(LoopCounter).DatabaseName = Results.Tables(1).Rows(LoopCounter).Item("database_name")
            aJob.JobStep(LoopCounter).JobStepAdvancedFlags = Results.Tables(1).Rows(LoopCounter).Item("flags").ToString.Substring(0, Results.Tables(1).Rows(LoopCounter).Item("flags").ToString.IndexOf(" "))
            aJob.JobStep(LoopCounter).JobStepCommand = Results.Tables(1).Rows(LoopCounter).Item("command")
            aJob.JobStep(LoopCounter).JobStepOutputPath = Results.Tables(1).Rows(LoopCounter).Item("output_file_name").ToString
            aJob.JobStep(LoopCounter).OsCommandExpectedReturnValue = Results.Tables(1).Rows(LoopCounter).Item("cmdexec_success_code")
            aJob.JobStep(LoopCounter).JobStepRetryAttempts = Results.Tables(1).Rows(LoopCounter).Item("retry_attempts")
            aJob.JobStep(LoopCounter).JobStepRetryInterval = Results.Tables(1).Rows(LoopCounter).Item("retry_interval")

            LoopCounter += 1
        End While
        StartStep.SelectedIndex = aJob.StartStepId - 1

        LoopCounter = 0
        JobScheduleList.Items.Clear()
        Dim aSchedule As Schedule
        While LoopCounter <> Results.Tables(2).Rows.Count
            JobScheduleList.Items.Add(Results.Tables(2).Rows(LoopCounter).Item("schedule_id"))
            JobScheduleList.Items(LoopCounter).SubItems.Add(Results.Tables(2).Rows(LoopCounter).Item("schedule_name"))
            If Results.Tables(2).Rows(LoopCounter).Item("enabled") = 0 Then
                JobScheduleList.Items(LoopCounter).SubItems.Add("False")
            Else
                JobScheduleList.Items(LoopCounter).SubItems.Add("true")
            End If

            aSchedule = New Schedule

            aSchedule.ScheduleId = Results.Tables(2).Rows(LoopCounter).Item("schedule_id")
            aSchedule.ScheduleName = Results.Tables(2).Rows(LoopCounter).Item("schedule_name")
            aSchedule.Enabled = Results.Tables(2).Rows(LoopCounter).Item("enabled")


            aJob.Schedule(Results.Tables(2).Rows(LoopCounter).Item("schedule_id")) = New Schedule
            aJob.Schedule(Results.Tables(2).Rows(LoopCounter).Item("schedule_id")) = aSchedule
            LoopCounter += 1
        End While
    End Sub
    Private Sub DisplayDataTab4()
        If aJob.Notify_Level_Email = Job.NotifyLevel.Never Then
            Email.Checked = False
        Else
            Email.Checked = True
            Action_Email.SelectedIndex = aJob.Notify_Level_Email - 1
            Operator_Email.SelectedText = aJob.Notify_Email_Operator
        End If
        If aJob.Notify_Level_EventLog = Job.NotifyLevel.Never Then
            EventLog.Checked = False
        Else
            EventLog.Checked = True
            Action_EventLog.SelectedIndex = aJob.Notify_Level_EventLog - 1
        End If
        If aJob.Notify_Level_Netsend = Job.NotifyLevel.Never Then
            NetSend.Checked = False
        Else
            NetSend.Checked = True
            Action_NetSend.SelectedIndex = aJob.Notify_Level_Netsend - 1
            Operator_NetSend.SelectedText = aJob.Nofify_Netsend_Operator
        End If
        If aJob.Nofify_Level_Page = Job.NotifyLevel.Never Then
            PAge.Checked = False
        Else
            PAge.Checked = True
            Action_Page.SelectedIndex = aJob.Nofify_Level_Page - 1
            Operator_PAge.SelectedText = aJob.Notify_Page_Operator
        End If
        If aJob.Notify_Level_Delete = Job.NotifyLevel.Never Then
            Delete.Checked = False
        Else
            Delete.Checked = True
            Action_Delete.SelectedIndex = aJob.Notify_Level_Delete - 1
        End If

        EnableTab4DropDowns()
    End Sub
    Private Sub EnableTab4DropDowns()
        Operator_Email.Enabled = Email.Checked
        Action_Email.Enabled = Email.Checked
        Operator_PAge.Enabled = PAge.Checked
        Action_Page.Enabled = PAge.Checked
        Operator_NetSend.Enabled = NetSend.Checked
        Action_NetSend.Enabled = NetSend.Checked
        Action_EventLog.Enabled = EventLog.Checked
        Action_Delete.Enabled = Delete.Checked
    End Sub
    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        DisplayData()
    End Sub
    Private Sub JobStepList_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles JobStepList.Click
        EditStep.Enabled = True
        DeleteStep.Enabled = True

    End Sub
    Private Sub JobScheduleList_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles JobScheduleList.Click
    End Sub
    Private Sub Email_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Email.CheckedChanged
        EnableTab4DropDowns()
    End Sub
    Private Sub PAge_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PAge.CheckedChanged
        EnableTab4DropDowns()
    End Sub
    Public BeenShown As Boolean = False
    Private Sub NetSend_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NetSend.CheckedChanged
        EnableTab4DropDowns()
    End Sub
    Private Sub EventLog_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EventLog.CheckedChanged
        EnableTab4DropDowns()
    End Sub
    Private Sub Delete_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Delete.CheckedChanged
        EnableTab4DropDowns()
    End Sub
    Private Sub SaveButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveButton.Click
        If JobName.Text = "" Then
            MsgBox("Please enter the name of the job")
            Exit Sub
        End If
        If OwnerUserName.Text = "" Then
            MsgBox("Please enter the name of the user who should be the owner of this job.")
            Exit Sub
        End If
        If Category.SelectedIndex = -1 Then
            MsgBox("Please select a job category from the list.")
            Exit Sub
        End If
        If JobStepList.Items.Count = 0 Then
            MsgBox("Please add at least one job step to this job.")
            Exit Sub
        End If
        If StartStep.SelectedIndex = -1 Then
            MsgBox("Please select the startup step for this job.")
            Exit Sub
        End If
        If Email.Checked = True Then
            If Operator_Email.SelectedIndex = -1 Then
                MsgBox("Please select an operator to email.")
                Exit Sub
            End If
            If Action_Email.SelectedIndex = -1 Then
                MsgBox("Please select when the email operator should be emailed.")
                Exit Sub
            End If
        End If
        If PAge.Checked = True Then
            If Operator_PAge.SelectedIndex = -1 Then
                MsgBox("Please select an operator to page")
                Exit Sub
            End If
            If Action_Page.SelectedIndex = -1 Then
                MsgBox("Please select when the page operator should be paged.")
                Exit Sub
            End If
        End If
        If NetSend.Checked = True Then
            If Operator_NetSend.SelectedIndex = -1 Then
                MsgBox("Please select an operator to [NET SEND to")
                Exit Sub
            End If
            If Action_NetSend.SelectedIndex = -1 Then
                MsgBox("Please select when the net send operator should be notified.")
                Exit Sub
            End If
        End If
        If Delete.Checked = True Then
            If Action_Delete.SelectedIndex = -1 Then
                MsgBox("Please select on what action the job should be deleted.")
                Exit Sub
            End If
        End If
        If EventLog.Checked = True Then
            If Action_EventLog.SelectedIndex = -1 Then
                MsgBox("Please select on what action the job should log to the event log.")
                Exit Sub
            End If
        End If
        SaveJob()
        SaveJobSteps()
        If aJob.job_id Is Nothing Then
            If MsgBox("Would you like to select job schedule(s) now?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                JobEditorScheduleList.Show()
                Me.Close()
            End If
        End If
    End Sub
    Private Sub SaveJobSteps()
        Dim JobStepCounter As Integer = 0
        Dim SQLCmd As SqlCommand
        Dim SQLResults As DataSet
        SQLCmd = New SqlCommand
        SQLCmd.CommandType = CommandType.StoredProcedure

        SQLCmd.CommandText = "sp_help_job"
        SQLCmd.Parameters.Add("@job_id", SqlDbType.VarChar, 50)
        SQLCmd.Parameters.Item("@job_id").Value = aJob.job_id

        SQLResults = New DataSet

        SQLResults = RunSQLStatement(ServerJobList.ServerInstance.ConnectionString, SQLCmd)
        SQLCmd = New SqlCommand
        If Not aJob.job_id Is Nothing Then
            SQLCmd.CommandText = "sp_delete_jobstep"
            SQLCmd.Parameters.Add("@job_id", SqlDbType.VarChar, 50)
            SQLCmd.Parameters.Item("@job_id").Value = aJob.job_id

            SQLCmd.Parameters.Add("@step_id", SqlDbType.Int)
            While JobStepCounter <> SQLResults.Tables(1).Rows.Count
                SQLCmd.Parameters.Item("@step_id").Value = SQLResults.Tables(1).Rows(JobStepCounter).Item("step_id")

                RunSQLStatement(ServerJobList.ServerInstance.ConnectionString, SQLCmd)
                JobStepCounter += 1
            End While

            SQLResults.Dispose()
            SQLResults = Nothing
        End If
        SQLCmd = New SqlCommand
        JobStepCounter = 1

        Do Until JobStepCounter > aJob.JobStepCount
            SQLCmd.CommandText = "sp_add_jobstep"

            SQLCmd.Parameters.Add("@job_id", SqlDbType.VarChar, 50)
            SQLCmd.Parameters.Item("@job_id").Value = aJob.job_id

            SQLCmd.Parameters.Add("@step_id", SqlDbType.Int)
            SQLCmd.Parameters.Item("@step_id").Value = aJob.JobStep(JobStepCounter).JobStepId

            SQLCmd.Parameters.Add("@step_name", SqlDbType.VarChar, 255)
            SQLCmd.Parameters.Item("@step_name").Value = aJob.JobStep(JobStepCounter).JobStepName

            SQLCmd.Parameters.Add("@subsystem", SqlDbType.NVarChar, 40)
            If aJob.JobStep(JobStepCounter).JobStepType = JobStep.JobStepTypes.TSQL Then SQLCmd.Parameters.Item("@subsystem").Value = "TSQL"
            If aJob.JobStep(JobStepCounter).JobStepType = JobStep.JobStepTypes.TSQL Then SQLCmd.Parameters.Item("@subsystem").Value = "SqlCmd"

            SQLCmd.Parameters.Add("@command", SqlDbType.NVarChar, 4000)
            SQLCmd.Parameters.Item("@command").Value = aJob.JobStep(JobStepCounter).JobStepCommand

            If aJob.JobStep(JobStepCounter).JobStepType = JobStep.JobStepTypes.OperationSystemCommand Then
                SQLCmd.Parameters.Add("@cmdexec_success_code", SqlDbType.Int)
                SQLCmd.Parameters.Item("@cmdexec_success_code").Value = aJob.JobStep(JobStepCounter).OsCommandExpectedReturnValue
            End If

            SQLCmd.Parameters.Add("@on_success_action", SqlDbType.Int)
            SQLCmd.Parameters.Item("@on_success_action").Value = aJob.JobStep(JobStepCounter).JobStepOnSuccess

            If aJob.JobStep(JobStepCounter).JobStepOnSuccess = Enums.JobStepAction.Go_To_Step Then
                SQLCmd.Parameters.Add("@on_success_step_id", SqlDbType.Int)
                SQLCmd.Parameters.Item("@on_success_step_id").Value = aJob.JobStep(JobStepCounter).JobStepOnSuccessStepId
            End If

            SQLCmd.Parameters.Add("@on_fail_action", SqlDbType.Int)
            SQLCmd.Parameters.Item("@on_fail_action").Value = aJob.JobStep(JobStepCounter).JobStepOnFailure

            If aJob.JobStep(JobStepCounter).JobStepOnFailure = Enums.JobStepAction.Go_To_Step Then
                SQLCmd.Parameters.Add("@on_fail_step_id", SqlDbType.Int)
                SQLCmd.Parameters.Item("@on_fail_step_id").Value = aJob.JobStep(JobStepCounter).JobStepOnFailureStepId
            End If

            SQLCmd.Parameters.Add("@database_name", SqlDbType.NVarChar, 255)
            SQLCmd.Parameters.Item("@database_name").Value = aJob.JobStep(JobStepCounter).DatabaseName

            'Not implimented, stored procedure defaults to NULL
            'SQLCmd.Parameters.Add("@database_user_name", SqlDbType.VarChar, 255)
            'SQLCmd.Parameters.Item("@database_user_name").Value = aJob.JobStep(JobStepCounter).databaseusername

            SQLCmd.Parameters.Add("@retry_attempts", SqlDbType.Int)
            SQLCmd.Parameters.Item("@retry_attempts").Value = aJob.JobStep(JobStepCounter).JobStepRetryAttempts

            SQLCmd.Parameters.Add("@retry_interval", SqlDbType.Int)
            SQLCmd.Parameters.Item("@retry_interval").Value = aJob.JobStep(JobStepCounter).JobStepRetryInterval

            SQLCmd.Parameters.Add("@output_file_name", SqlDbType.VarChar, 200)
            SQLCmd.Parameters.Item("@output_file_name").Value = aJob.JobStep(JobStepCounter).JobStepOutputPath

            SQLCmd.Parameters.Add("@flags", SqlDbType.Int)
            SQLCmd.Parameters.Item("@flags").Value = aJob.JobStep(JobStepCounter).JobStepOutputPathAppendFlag

            'Not implimented, stored procedure defaults to NULL
            'SQLCmd.Parameters.Add("@proxy_id", SqlDbType.Int)
            'SQLCmd.Parameters.Item("@proxy_id").Value = aJob.JobStep(JobStepCounter).jobstepProxyId

            'SQLCmd.Parameters.Add("@proxy_name", SqlDbType.Int)
            'SQLCmd.Parameters.Item("@proxy_name").Value = aJob.JobStep(JobStepCounter).jobstepProxyName

            Try
                RunSQLStatement(ServerJobList.ServerInstance.ConnectionString, SQLCmd)
            Catch ex As Exception
                MsgBox("Error saving job step settings." + vbCrLf + ex.Message)
                Exit Sub
            End Try

            JobStepCounter += 1
        Loop
    End Sub
    Private Sub SaveJob()

        Dim SQLCmd As SqlCommand
        SQLCmd = New SqlCommand
        SQLCmd.CommandType = CommandType.StoredProcedure
        If aJob.job_id Is Nothing Then
            SQLCmd.CommandText = "sp_add_job"
        Else
            SQLCmd.CommandText = "sp_update_job"
        End If

        SQLCmd.Parameters.Add("@job_id", SqlDbType.VarChar, 50)
        SQLCmd.Parameters.Item("@job_id").Value = aJob.job_id

        If aJob.job_id Is Nothing Then
            SQLCmd.Parameters.Item("@job_id").Direction = ParameterDirection.Output
            SQLCmd.Parameters.Add("@job_name", SqlDbType.NVarChar, 128)
            SQLCmd.Parameters.Item("@job_name").Value = JobName.Text
        End If

        If JobName.Text <> aJob.job_name And Not aJob.job_id Is Nothing Then
            SQLCmd.Parameters.Add("@new_name", SqlDbType.NVarChar, 128)
            SQLCmd.Parameters.Item("@new_name").Value = JobName.Text
        End If

        SQLCmd.Parameters.Add("@enabled", SqlDbType.Bit)
        SQLCmd.Parameters.Item("@enabled").Value = JobEnabled.Checked

        SQLCmd.Parameters.Add("@description", SqlDbType.NVarChar, 512)
        SQLCmd.Parameters.Item("@description").Value = Description.Text

        SQLCmd.Parameters.Add("@start_step_id", SqlDbType.Int)
        SQLCmd.Parameters.Item("@start_step_id").Value = StartStep.SelectedIndex + 1

        SQLCmd.Parameters.Add("@category_name", SqlDbType.NVarChar, 128)
        SQLCmd.Parameters.Item("@category_name").Value = Category.Text

        SQLCmd.Parameters.Add("@owner_login_name", SqlDbType.NVarChar, 128)
        SQLCmd.Parameters.Item("@owner_login_name").Value = OwnerUserName.Text

        SQLCmd.Parameters.Add("@notify_level_eventlog", SqlDbType.Int)
        If EventLog.Checked = False Then
            SQLCmd.Parameters.Item("@notify_level_eventlog").Value = 0
        Else
            SQLCmd.Parameters.Item("@notify_level_eventlog").Value = Action_EventLog.SelectedIndex + 1
        End If

        SQLCmd.Parameters.Add("@notify_level_email", SqlDbType.Int)
        If Email.Checked = False Then
            SQLCmd.Parameters.Item("@notify_level_email").Value = 0
        Else
            SQLCmd.Parameters.Item("@notify_level_email").Value = Action_Email.SelectedIndex + 1
        End If

        SQLCmd.Parameters.Add("@notify_level_netsend", SqlDbType.Int)
        If NetSend.Checked = False Then
            SQLCmd.Parameters.Item("@notify_level_netsend").Value = 0
        Else
            SQLCmd.Parameters.Item("@notify_level_netsend").Value = Action_NetSend.SelectedIndex + 1
        End If

        SQLCmd.Parameters.Add("@notify_level_page", SqlDbType.Int)
        If PAge.Checked = False Then
            SQLCmd.Parameters.Item("@notify_level_page").Value = 0
        Else
            SQLCmd.Parameters.Item("@notify_level_page").Value = Action_Page.SelectedIndex + 1
        End If

        SQLCmd.Parameters.Add("@notify_email_operator_name", SqlDbType.NVarChar, 128)
        SQLCmd.Parameters.Item("@notify_email_operator_name").Value = Operator_Email.Text

        SQLCmd.Parameters.Add("@notify_netsend_operator_name", SqlDbType.NVarChar, 128)
        SQLCmd.Parameters.Item("@notify_netsend_operator_name").Value = Operator_NetSend.Text

        SQLCmd.Parameters.Add("@notify_page_operator_name", SqlDbType.NVarChar, 128)
        SQLCmd.Parameters.Item("@notify_page_operator_name").Value = Operator_PAge.Text

        SQLCmd.Parameters.Add("@delete_level", SqlDbType.Int)
        If Delete.Checked = False Then
            SQLCmd.Parameters.Item("@delete_level").Value = 0
        Else
            SQLCmd.Parameters.Item("@delete_level").Value = Action_Delete.SelectedIndex
        End If

        Try
            RunSQLStatement(ServerJobList.ServerInstance.ConnectionString, SQLCmd)

            If aJob.job_id Is Nothing Then
                aJob.job_id = SQLCmd.Parameters.Item("@job_id").Value
                'Print(aJob.job_id)
            End If
        Catch ex As Exception
            MsgBox("Error updating job." + vbCrLf + ex.Message)
        End Try

    End Sub

    Private Sub NewStep_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NewStep.Click
        JobStepEditor.Show()
        JobStepEditor.SetupJobStep()
        JobStepEditor.NewStep = True
    End Sub

    Private Sub JobName_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles JobName.TextChanged
        If JobName.Text.Contains("asshat") And BeenShown = False Then
            BeenShown = True
            Dim t As Thread
            t = New Thread(AddressOf Warn)
            t.Start()
        End If
    End Sub
    Private Sub Warn()
        Dim f As Form

        f = New Form
        f.Text = "SQLBatman says ""Don't be an asshat!"""
        f.BackgroundImage = Me.BackgroundImage
        f.Height = f.BackgroundImage.Height + 100
        f.Width = f.BackgroundImage.Width
        f.BackgroundImageLayout = ImageLayout.Center
        f.StartPosition = FormStartPosition.CenterScreen
        f.MinimizeBox = False
        f.MaximizeBox = False
        f.Icon = Me.Icon
        Beep()
        f.Show()

        Thread.Sleep(5000)
        f.Close()
    End Sub

    Private Sub EditStep_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditStep.Click
        EditJobStep()
    End Sub
    Private Sub EditJobStep()
        If JobStepList.SelectedItems.Count = 0 Then
            MsgBox("Please select a job step to edit.")
            Exit Sub
        End If
        Dim jse As JobStepEditor
        Dim aJobStep As JobStep
        aJobStep = New JobStep
        jse = New JobStepEditor
        aJobStep = aJob.JobStep(JobStepList.SelectedItems.Item(0).Text)
        jse.aJobStep = aJobStep
        jse.Show()
        jse.SetupJobStep()
        jse.NewStep = False
    End Sub

    Private Sub StartStep_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles StartStep.SelectedIndexChanged
        aJob.StartStepId = StartStep.SelectedIndex + 1
    End Sub

    Private Sub PickSchedule_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PickSchedule.Click
        JobEditorScheduleList.Show()
    End Sub
End Class