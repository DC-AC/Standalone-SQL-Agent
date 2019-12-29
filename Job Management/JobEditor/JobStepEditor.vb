Imports SSA.CommonControl
Imports SSA.CommonControl.SharedCode
Imports System.Data
Imports System.Data.SqlClient

Public Class JobStepEditor
    Public aJobStep As JobStep = Nothing
    Public NewStep As Boolean = False

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Public Sub SetupJobStep()
        'Get Database List
        Dim SQLCommand As SqlCommand
        SQLCommand = New SqlCommand
        Dim ds As DataSet
        SQLCommand.CommandText = "sp_helpdb"
        SQLCommand.CommandType = CommandType.StoredProcedure
        ds = RunSQLStatement(ServerJobList.ServerInstance.ConnectionString, SQLCommand)
        For Each i As DataRow In ds.Tables(0).Rows
            Database.Items.Add(i.Item("name"))
        Next
        If aJobStep Is Nothing Then
            Me.Database.SelectedItem = "master"
        Else
            Me.Database.SelectedItem = aJobStep.DatabaseName
        End If

        If Not aJobStep Is Nothing Then
            StepName.Text = aJobStep.JobStepName
            Command.Text = aJobStep.JobStepCommand
        End If

        'Populate Job Step List
        Dim LoopCount As Integer = 1
        While LoopCount <= JobEditor.aJob.JobStepCount
            SuccessStep.Items.Add(JobEditor.aJob.JobStep(LoopCount).JobStepId.ToString + " " + JobEditor.aJob.JobStep(LoopCount).JobStepName)
            FailStep.Items.Add(JobEditor.aJob.JobStep(LoopCount).JobStepId.ToString + " " + JobEditor.aJob.JobStep(LoopCount).JobStepName)
            LoopCount += 1
        End While


        If aJobStep Is Nothing Then
            Me.StepType.SelectedItem = "T-SQL Script"
            SuccessStep.Enabled = False
            FailStep.Enabled = False

        Else
            If aJobStep.JobStepType = JobStep.JobStepTypes.OperationSystemCommand Then
                StepType.SelectedItem = "Operating System (CmdExec)"
            ElseIf aJobStep.JobStepType = JobStep.JobStepTypes.TSQL Then
                StepType.SelectedItem = "T-SQL Script"
            Else
                StepType.SelectedItem = ""
            End If

            SuccessStep.Enabled = False
            FailStep.Enabled = False
            If aJobStep.JobStepOnSuccess = Enums.JobStepAction.Go_To_Next_Step Then
                OnSuccessAction.SelectedIndex = 0
            ElseIf aJobStep.JobStepOnSuccess = Enums.JobStepAction.Go_To_Step Then
                OnSuccessAction.SelectedIndex = 1
                SuccessStep.Enabled = True
                SuccessStep.SelectedItem = aJobStep.JobStepOnSuccessStepId - 1
            ElseIf aJobStep.JobStepOnSuccess = Enums.JobStepAction.Quit_With_Success Then
                OnSuccessAction.SelectedIndex = 2
            Else
                OnSuccessAction.SelectedIndex = 3
            End If

            If aJobStep.JobStepOnFailure = Enums.JobStepAction.Go_To_Next_Step Then
                OnFailAction.SelectedIndex = 0
            ElseIf aJobStep.JobStepOnFailure = Enums.JobStepAction.Go_To_Step Then
                OnFailAction.SelectedIndex = 1
                FailStep.Enabled = True
                FailStep.SelectedItem = aJobStep.JobStepOnFailureStepId - 1
            ElseIf aJobStep.JobStepOnFailure = Enums.JobStepAction.Quit_With_Success Then
                OnFailAction.SelectedIndex = 2
            Else
                OnFailAction.SelectedIndex = 3
            End If

        End If
    End Sub
    Private Sub SaveStep()
        If NewStep = True Then
            aJobStep = New JobStep
            aJobStep.JobStepId = JobEditor.aJob.AddJobStep
            
            Debug.Print("Job Step Id " + aJobStep.JobStepId.ToString)
            aJobStep.IsNewStep = True
            JobEditor.StartStep.Items.Add(StepName.Text)
            If JobEditor.StartStep.Items.Count = 1 Then JobEditor.StartStep.SelectedIndex = 0
        End If
        aJobStep.JobStepName = StepName.Text
        If StepType.SelectedText = "T-SQL Script" Then
            aJobStep.JobStepType = JobStep.JobStepTypes.TSQL
        Else
            aJobStep.JobStepType = JobStep.JobStepTypes.OperationSystemCommand
        End If
        aJobStep.DatabaseName = Database.SelectedText
        aJobStep.JobStepCommand = Command.Text
        aJobStep.JobStepOutputPath = OutputFile.Text

        If OnSuccessAction.SelectedIndex = 0 Then
            aJobStep.JobStepOnSuccess = Enums.JobStepAction.Go_To_Next_Step
        ElseIf OnSuccessAction.SelectedIndex = 1 Then
            aJobStep.JobStepOnSuccess = Enums.JobStepAction.Go_To_Step
            aJobStep.JobStepOnSuccessStepId = SuccessStep.SelectedIndex + 1
        ElseIf OnSuccessAction.SelectedIndex = 2 Then
            aJobStep.JobStepOnSuccess = Enums.JobStepAction.Quit_With_Success
        ElseIf OnSuccessAction.SelectedIndex = 3 Then
            aJobStep.JobStepOnSuccess = Enums.JobStepAction.Quit_With_Failure
        End If

        If OnFailAction.SelectedIndex = 0 Then
            aJobStep.JobStepOnFailure = Enums.JobStepAction.Go_To_Next_Step
        ElseIf OnFailAction.SelectedIndex = 1 Then
            aJobStep.JobStepOnFailure = Enums.JobStepAction.Go_To_Step
            aJobStep.JobStepOnFailureStepId = FailStep.SelectedIndex + 1
        ElseIf OnFailAction.SelectedIndex = 2 Then
            aJobStep.JobStepOnFailure = Enums.JobStepAction.Quit_With_Success
        ElseIf OnFailAction.SelectedIndex = 3 Then
            aJobStep.JobStepOnFailure = Enums.JobStepAction.Quit_With_Failure
        End If

        aJobStep.JobStepRetryAttempts = RetryAttempt.Text
        aJobStep.JobStepRetryInterval = RetryInterval.Text
        aJobStep.JobStepOutputPath = OutputFile.Text

        If NewStep = True Then
            JobEditor.aJob.JobStep(aJobStep.JobStepId) = New JobStep
            JobEditor.aJob.JobStep(aJobStep.JobStepId) = aJobStep

            JobEditor.JobStepList.Items.Add(aJobStep.JobStepId)
            JobEditor.JobStepList.Items(aJobStep.JobStepId - 1).SubItems.Add(aJobStep.JobStepName)
            JobEditor.JobStepList.Items(aJobStep.JobStepId - 1).SubItems.Add(aJobStep.JobStepType)
            JobEditor.JobStepList.Items(aJobStep.JobStepId - 1).SubItems.Add(aJobStep.JobStepOnSuccess)
            JobEditor.JobStepList.Items(aJobStep.JobStepId - 1).SubItems.Add(aJobStep.JobStepOnFailure)
        End If

        JobEditor.aJob.JobStep(aJobStep.JobStepId) = aJobStep
        JobEditor.JobStepList.Items(aJobStep.JobStepId - 1).SubItems.Item(1).Text = aJobStep.JobStepName
        If aJobStep.JobStepType = JobStep.JobStepTypes.TSQL Then
            JobEditor.JobStepList.Items(aJobStep.JobStepId - 1).SubItems.Item(2).Text = "T-SQL Script"
        ElseIf aJobStep.JobStepType = JobStep.JobStepTypes.OperationSystemCommand Then
            JobEditor.JobStepList.Items(aJobStep.JobStepId - 1).SubItems.Item(2).Text = "Operating System (CmdExec)"
        End If

        If aJobStep.JobStepOnSuccess = Enums.JobStepAction.Go_To_Next_Step Then
            JobEditor.JobStepList.Items(aJobStep.JobStepId - 1).SubItems.Item(3).Text = "Go To Next Step"
        ElseIf aJobStep.JobStepOnSuccess = Enums.JobStepAction.Go_To_Step Then
            JobEditor.JobStepList.Items(aJobStep.JobStepId - 1).SubItems.Item(3).Text = "Go To Step"
        ElseIf aJobStep.JobStepOnSuccess = Enums.JobStepAction.Quit_With_Failure Then
            JobEditor.JobStepList.Items(aJobStep.JobStepId - 1).SubItems.Item(3).Text = "Quit Job Reporting Success"
        ElseIf aJobStep.JobStepOnSuccess = Enums.JobStepAction.Quit_With_Success Then
            JobEditor.JobStepList.Items(aJobStep.JobStepId - 1).SubItems.Item(3).Text = "Quit Job Reporting Failure"
        End If

        If aJobStep.JobStepOnFailure = Enums.JobStepAction.Go_To_Next_Step Then
            JobEditor.JobStepList.Items(aJobStep.JobStepId - 1).SubItems.Item(4).Text = "Go To Next Step"
        ElseIf aJobStep.JobStepOnFailure = Enums.JobStepAction.Go_To_Step Then
            JobEditor.JobStepList.Items(aJobStep.JobStepId - 1).SubItems.Item(4).Text = "Go To Step"
        ElseIf aJobStep.JobStepOnFailure = Enums.JobStepAction.Quit_With_Failure Then
            JobEditor.JobStepList.Items(aJobStep.JobStepId - 1).SubItems.Item(4).Text = "Quit Job Reporting Success"
        ElseIf aJobStep.JobStepOnFailure = Enums.JobStepAction.Quit_With_Success Then
            JobEditor.JobStepList.Items(aJobStep.JobStepId - 1).SubItems.Item(4).Text = "Quit Job Reporting Failure"
        End If

        JobEditor.aJob.JobStep(aJobStep.JobStepId) = aJobStep


        Me.Close()
    End Sub


    Private Sub OutputFile_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OutputFile.TextChanged
        If OutputFile.Text = "" Then
            AppendFile.Enabled = False
        Else
            AppendFile.Enabled = True
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If OnSuccessAction.SelectedIndex = -1 Then
            If MsgBox("The successful action option was not set.  Would you like to set it to mark the job as successful on step completion?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                OnSuccessAction.SelectedIndex = 2
            Else
                Exit Sub
            End If
        End If
        If OnFailAction.SelectedIndex = -1 Then
            If MsgBox("The failure action option was not set.  Would you like to set it to makr the job as failed on step failure?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                OnFailAction.SelectedIndex = 3
            Else
                Exit Sub
            End If
        End If
        If NewStep = True Then
            If JobEditor.aJob.JobStepNameExists(StepName.Text) = True Then
                MsgBox("There is already a job step with this name for this job.  Please select a different name.", MsgBoxStyle.Exclamation)
                StepName.SelectAll()
                Exit Sub
            End If
        End If
        SaveStep()
    End Sub

    Private Sub OnSuccessAction_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OnSuccessAction.SelectedIndexChanged
        If OnSuccessAction.SelectedIndex = 1 Then
            SuccessStep.Enabled = True
        Else
            SuccessStep.Enabled = False
        End If
    End Sub


    Private Sub OnFailAction_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OnFailAction.SelectedIndexChanged
        If OnFailAction.SelectedIndex = 1 Then
            FailStep.Enabled = True
        Else
            FailStep.Enabled = False
        End If
    End Sub
End Class