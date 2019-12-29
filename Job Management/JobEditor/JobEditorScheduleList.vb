Imports System.Data
Imports System.Data.SqlClient
Imports SSA.CommonControl
Public Class JobEditorScheduleList
    Private Sub JobEditorScheduleList_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        MoveButtons()
        ReloadScheduleList()
    End Sub
    Private Sub ReloadScheduleList()
        ScheduleList.Items.Clear()

        Dim SQLResults As DataSet
        Dim SQLCmd As SqlCommand
        Dim SQLConn As SqlConnection
        Dim SQLda As SqlDataAdapter
        Dim RowCounter As Integer = 0

        SQLResults = New DataSet()

        Try

            SQLConn = New SqlConnection(ServerJobList.ServerInstance.ConnectionString)
            SQLCmd = New SqlCommand

            SQLCmd.CommandText = "sp_help_schedule"
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
            MsgBox("Error getting schedule list." & vbCrLf & ex.Message)
            Exit Sub
        End Try

        Dim SchedulerCounter As Integer = 0

        While SchedulerCounter <> SQLResults.Tables(0).Rows.Count
            ScheduleList.Items.Add(SQLResults.Tables(0).Rows(SchedulerCounter).Item("schedule_id"))
            ScheduleList.Items(SchedulerCounter).SubItems.Add(SQLResults.Tables(0).Rows(SchedulerCounter).Item("schedule_name"))
            ScheduleList.Items(SchedulerCounter).SubItems.Add(SQLResults.Tables(0).Rows(SchedulerCounter).Item("enabled"))
            ScheduleList.Items(SchedulerCounter).SubItems.Add(SQLResults.Tables(0).Rows(SchedulerCounter).Item("job_count"))
            ScheduleList.Items(SchedulerCounter).Checked = JobEditor.aJob.HasSchedule(SQLResults.Tables(0).Rows(SchedulerCounter).Item("schedule_id"))
            SchedulerCounter += 1
        End While
        SQLResults.Clear()
        SQLResults.Dispose()
    End Sub
    Private Sub JobEditorScheduleList_Resize(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Resize
        MoveButtons()
    End Sub
    Private Sub MoveButtons()
        Me.SplitContainer2.SplitterDistance = Me.SplitContainer1.Width / 2
        Button1.Left = (SplitContainer2.Panel1.Width / 2) - (Button1.Width / 2)
        Button2.Left = (SplitContainer2.Panel2.Width / 2) - (Button2.Width / 2)
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Public Sub ChangeSchedule(ByVal ScheduleId As Integer, ByVal JobId As String, ByVal AddSchedule As Boolean)
        Dim SQLResults As DataSet
        Dim SQLCmd As SqlCommand
        Dim SQLConn As SqlConnection
        Dim SQLda As SqlDataAdapter
        Dim RowCounter As Integer = 0

        SQLResults = New DataSet()


        SQLConn = New SqlConnection(ServerJobList.ServerInstance.ConnectionString)
        SQLCmd = New SqlCommand

        If AddSchedule = True Then
            SQLCmd.CommandText = "sp_attach_schedule"
        Else
            SQLCmd.CommandText = "sp_detach_schedule"
        End If

        SQLCmd.CommandType = CommandType.StoredProcedure
        SQLCmd.Parameters.Add("@job_id", SqlDbType.VarChar, 50)
        SQLCmd.Parameters.Item("@job_id").Value = JobId
        SQLCmd.Parameters.Add("@schedule_id", SqlDbType.Int)
        SQLCmd.Parameters.Item("@schedule_id").Value = ScheduleId

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
 
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim ScheduleLoop As Integer = 0
        Do Until ScheduleLoop = ScheduleList.Items.Count
            Debug.Print(ScheduleList.Items(ScheduleLoop).SubItems(0).Text)
            Try
                ChangeSchedule(ScheduleList.Items(ScheduleLoop).SubItems(0).Text, JobEditor.aJob.job_id, ScheduleList.Items(ScheduleLoop).Checked)
            Catch ex As Exception
                MsgBox("Error saving job schedule data." + vbCrLf + ex.Message, MsgBoxStyle.Critical)
                Exit Sub
            End Try
            ScheduleLoop += 1
        Loop
        JobEditor.DisplayData()
        MsgBox("Job schedule changes saved to server.")
        Me.Close()
    End Sub
End Class