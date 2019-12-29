Imports System.Data
Imports System.Data.SqlClient
Imports SSA.CommonControl
Imports SSA.CommonControl.Enums
Imports SSA.CommonControl.SharedCode

Public Class ServerJobList
    Public ServerInstance As JobServer
    Public InstanceJobList() As Job
    Public InstanceGuidLookup As JobGuidList



    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        'ServerInstance = New JobServer

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub ServerJobList_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Application.OpenForms.Count <> 1 Then
            Dim m As MsgBoxStyle
            m = MsgBoxStyle.YesNo
            m += MsgBoxStyle.Critical

            If MsgBox("There are " + (Application.OpenForms.Count - 1).ToString + " other window(s) open." + vbCrLf + "Are you sure you want to close this application?" + vbCrLf + "Unsaved changes will be lost.", m, "Error Closing Application") = MsgBoxResult.Yes Then
                End
            Else
                e.Cancel = True
            End If
        End If
    End Sub

    Private Sub ServerJobList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        DisableMenus()
    End Sub
    Public Sub PullJobList()

        JobList.Items.Clear()

        Dim SQLResults As DataSet
        Dim SQLCmd As SqlCommand
        Dim SQLConn As SqlConnection
        Dim SQLda As SqlDataAdapter
        Dim RowCounter As Integer = 0
        Dim job_id As String

        SQLResults = New DataSet()
        InstanceGuidLookup = New JobGuidList

        Try

            SQLConn = New SqlConnection(ServerInstance.ConnectionString)
            SQLCmd = New SqlCommand

            SQLCmd.CommandText = "sp_help_job_SSA"
            SQLCmd.CommandType = CommandType.StoredProcedure

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
            MsgBox("Error getting SQL Job List." & vbCrLf & ex.Message)
            Connect.Show()
            Me.Close()
            Exit Sub
        End Try

        ReDim InstanceJobList(SQLResults.Tables(0).Rows.Count)

        Do Until RowCounter = SQLResults.Tables(0).Rows.Count
            job_id = InstanceGuidLookup.AddGuid(SQLResults.Tables(0).Rows(RowCounter).Item("job_id").ToString)
            InstanceJobList(job_id) = New Job
            Debug.Print(job_id.ToString + " - " + SQLResults.Tables(0).Rows(RowCounter).Item("name"))
            InstanceJobList(job_id).job_id = SQLResults.Tables(0).Rows(RowCounter).Item("job_id").ToString
            InstanceJobList(job_id).job_name = SQLResults.Tables(0).Rows(RowCounter).Item("name")
            InstanceJobList(job_id).JobCategory = SQLResults.Tables(0).Rows(RowCounter).Item("category")

            If SQLResults.Tables(0).Rows(RowCounter).Item("enabled") = 0 Then
                InstanceJobList(job_id).Enabled = False
            Else
                InstanceJobList(job_id).Enabled = True
            End If

            InstanceJobList(job_id).StartStepId = SQLResults.Tables(0).Rows(RowCounter).Item("start_step_id")
            InstanceJobList(job_id).JobOwner = SQLResults.Tables(0).Rows(RowCounter).Item("owner")
            InstanceJobList(job_id).Nofify_Level_Page = SQLResults.Tables(0).Rows(RowCounter).Item("notify_level_page")
            InstanceJobList(job_id).Nofify_Netsend_Operator = SQLResults.Tables(0).Rows(RowCounter).Item("notify_netsend_operator")
            InstanceJobList(job_id).Notify_Email_Operator = SQLResults.Tables(0).Rows(RowCounter).Item("notify_email_operator")
            InstanceJobList(job_id).Notify_Level_Email = SQLResults.Tables(0).Rows(RowCounter).Item("notify_level_email")
            InstanceJobList(job_id).Notify_Level_EventLog = SQLResults.Tables(0).Rows(RowCounter).Item("notify_level_eventlog")
            InstanceJobList(job_id).Notify_Level_Netsend = SQLResults.Tables(0).Rows(RowCounter).Item("notify_level_netsend")
            InstanceJobList(job_id).Notify_Page_Operator = SQLResults.Tables(0).Rows(RowCounter).Item("notify_page_operator")
            InstanceJobList(job_id).Delete_Level = SQLResults.Tables(0).Rows(RowCounter).Item("delete_level")
            InstanceJobList(job_id).CreateDate = SQLResults.Tables(0).Rows(RowCounter).Item("date_created")
            InstanceJobList(job_id).ModifyDate = SQLResults.Tables(0).Rows(RowCounter).Item("date_modified")

            'Add item to grid
            JobList.Items.Add(InstanceJobList(job_id).job_id)
            JobList.Items(RowCounter).SubItems.Add(InstanceJobList(job_id).job_name)
            JobList.Items(RowCounter).SubItems.Add(InstanceJobList(job_id).Enabled)

            If SQLResults.Tables(0).Rows(RowCounter).Item("current_execution_status") = JobExecutionStatus.Executing Then
                JobList.Items(RowCounter).SubItems.Add("Executing " + SQLResults.Tables(0).Rows(RowCounter).Item("current_execution_step"))
            ElseIf SQLResults.Tables(0).Rows(RowCounter).Item("current_execution_status") = JobExecutionStatus.Between_Retries Then
                JobList.Items(RowCounter).SubItems.Add("Between Retries Step " + SQLResults.Tables(0).Rows(RowCounter).Item("current_execution_step"))
            ElseIf SQLResults.Tables(0).Rows(RowCounter).Item("current_execution_status") = JobExecutionStatus.Idle Then
                JobList.Items(RowCounter).SubItems.Add("Idle")
            ElseIf SQLResults.Tables(0).Rows(RowCounter).Item("current_execution_status") = JobExecutionStatus.Performing_Completion_Actions Then
                JobList.Items(RowCounter).SubItems.Add("Performing Completion Actions")
            ElseIf SQLResults.Tables(0).Rows(RowCounter).Item("current_execution_status") = JobExecutionStatus.Suspended Then
                JobList.Items(RowCounter).SubItems.Add("Suspended")
            ElseIf SQLResults.Tables(0).Rows(RowCounter).Item("current_execution_status") = JobExecutionStatus.Waiting_For_Thread Then
                JobList.Items(RowCounter).SubItems.Add("Waiting For Thread")
            Else
                JobList.Items(RowCounter).SubItems.Add("Unknown")
            End If
            If StringDatetoDate(SQLResults.Tables(0).Rows(RowCounter).Item("last_run_date")).year <> 9999 Then
                JobList.Items(RowCounter).SubItems.Add(StringDatetoDate(SQLResults.Tables(0).Rows(RowCounter).Item("last_run_date")) + " " + StringTimeToDate(SQLResults.Tables(0).Rows(RowCounter).Item("last_run_time")))
            Else
                JobList.Items(RowCounter).SubItems.Add("")
            End If

            If StringDatetoDate(SQLResults.Tables(0).Rows(RowCounter).Item("next_run_date")).year <> 9999 Then
                JobList.Items(RowCounter).SubItems.Add(StringDatetoDate(SQLResults.Tables(0).Rows(RowCounter).Item("next_run_date")) + " " + StringTimeToDate(SQLResults.Tables(0).Rows(RowCounter).Item("next_run_time")))
            Else
                JobList.Items(RowCounter).SubItems.Add("")
            End If

            RowCounter += 1
        Loop

        SQLResults = Nothing
        DisableMenus()

    End Sub

    Private Sub RefreshJobListToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RefreshJobListToolStripMenuItem.Click
        PullJobList()
    End Sub
    Private Sub RefreshWindowToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RefreshWindowToolStripMenuItem.Click
        PullJobList()
    End Sub
    Private Overloads Sub ShowJobEditor()
        JobEditor.Show()
        JobEditor.SetupForNewJob()

    End Sub
    Private Overloads Sub ShowJobEditor(ByVal job_guid As String)
        Dim aJob As Job
        JobEditor.Show()
        aJob = New Job
        aJob = InstanceJobList(InstanceGuidLookup.GetJobId(job_guid))
        JobEditor.aJob = aJob
        JobEditor.DisplayData()
    End Sub
    Private Sub ShowJobHistory(ByVal job_id As String)
        JobHistory.Show()
        JobHistory.job_id = job_id
    End Sub

    Private Sub AddJobToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddJobToolStripMenuItem.Click
        ShowJobEditor()
    End Sub

    Private Sub EditJobToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles EditJobToolStripMenuItem.Click
        ShowJobEditor(JobList.SelectedItems(0).SubItems(0).Text)
    End Sub

    Private Sub MnuEditJob_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MnuEditJob.Click
        ShowJobEditor(JobList.SelectedItems(0).SubItems(0).Text)
    End Sub

    Private Sub MnuAddJob_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MnuAddJob.Click
        ShowJobEditor()
    End Sub

    Private Sub MnuDeleteJob_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MnuDeleteJob.Click
        DeleteJob()
    End Sub
    Private Sub DeleteJobToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteJobToolStripMenuItem.Click
        DeleteJob()
    End Sub
    Private Sub DeleteJob()
        If MsgBox("Are you sure you wish to delete the job """ + JobList.SelectedItems(0).SubItems(1).Text + """?", MsgBoxStyle.YesNo) <> MsgBoxResult.Yes Then Exit Sub
        Dim SQLCmd As SqlCommand
        SQLCmd = New SqlCommand
        SQLCmd.CommandText = "sp_delete_job"
        SQLCmd.CommandType = CommandType.StoredProcedure
        SQLCmd.Parameters.Add("@job_id", SqlDbType.VarChar, 50)
        SQLCmd.Parameters.Item("@job_id").Value = JobList.SelectedItems(0).SubItems(0).Text
        Try
            RunSQLStatement(ServerInstance.ConnectionString, SQLCmd)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        PullJobList()
    End Sub
    Private Sub JobList_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles JobList.Click
        EnableMenus()
        Debug.Print(JobList.SelectedItems(0).SubItems(1).Text)
        If JobList.SelectedItems(0).SubItems(2).Text = "True" Then
            MnuEnableJob.Enabled = False
            MnuDisableJob.Enabled = True
        Else
            MnuEnableJob.Enabled = True
            MnuDisableJob.Enabled = False
        End If
    End Sub
    Private Sub DisableMenus()
        MnuDeleteJob.Enabled = False
        MnuEditJob.Enabled = False
        DeleteJobToolStripMenuItem.Enabled = False
        EditJobToolStripMenuItem.Enabled = False
        MnuDisableJob.Enabled = False
        MnuEnableJob.Enabled = False

    End Sub
    Private Sub EnableMenus()
        MnuDeleteJob.Enabled = True
        MnuEditJob.Enabled = True
        DeleteJobToolStripMenuItem.Enabled = True
        EditJobToolStripMenuItem.Enabled = True
    End Sub
    Private Sub ChangeJobStatus(ByVal Enabled As Boolean)
        Dim SQLCmd As SqlCommand
        SQLCmd = New SqlCommand
        SQLCmd.CommandText = "sp_update_job"
        SQLCmd.CommandType = CommandType.StoredProcedure
        SQLCmd.Parameters.Add("@job_id", SqlDbType.VarChar, 50)
        SQLCmd.Parameters.Item("@job_id").Value = JobList.SelectedItems(0).SubItems(0).Text
        SQLCmd.Parameters.Add("@enabled", SqlDbType.TinyInt)
        If Enabled = True Then
            SQLCmd.Parameters.Item("@enabled").Value = 1
        Else
            SQLCmd.Parameters.Item("@enabled").Value = 0
        End If
        Try
            RunSQLStatement(ServerInstance.ConnectionString, SQLCmd)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        PullJobList()
        If Enabled = True Then
            MsgBox(JobList.SelectedItems(0).SubItems(1).Text + " has been enabled.")
        Else
            MsgBox(JobList.SelectedItems(0).SubItems(1).Text + " has been disabled.")
        End If
    End Sub

    Private Sub MnuEnableJob_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MnuEnableJob.Click
        ChangeJobStatus(True)
    End Sub

    Private Sub MnuDisableJob_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MnuDisableJob.Click
        ChangeJobStatus(False)
    End Sub

    Private Sub AboutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutToolStripMenuItem.Click
        AboutBox1.Show()
    End Sub

    Private Sub JobList_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles JobList.SelectedIndexChanged

    End Sub
End Class