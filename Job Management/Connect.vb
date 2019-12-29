Imports SSA.CommonControl
Imports SSA.CommonControl.SharedCode
Public Class Connect
    Private Sub Connect_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        AdjustTextBoxes()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub AdjustTextBoxes()
        If RadioButton1.Checked = True Then
            UserName.Enabled = False
            Password.Enabled = False
        Else
            UserName.Enabled = True
            Password.Enabled = True
        End If
    End Sub
    Private Sub ChangeForms()
        Dim ServerInstance As JobServer

        If ServerName.Text = "" Then
            MsgBox("Please enter a server name.")
            Exit Sub
        End If
        Dim ConnectionString As String
        If RadioButton1.Checked = True Then
            ConnectionString = FormatconnectionString(ServerName.Text, "msdb")
        Else
            If UserName.Text = "" Then
                MsgBox("Please enter a username.")
                Exit Sub
            End If
            ConnectionString = FormatconnectionString(ServerName.Text, "msdb", SSA.CommonControl.JobServer.InstanceAuthenticationMethod.SQLAuthentication, UserName.Text, Password.Text)
        End If
        ServerInstance = New JobServer

        ServerInstance.InstanceName = ServerName.Text
        ServerInstance.DatabaseName = "msdb"
        If RadioButton1.Checked = True Then
            ServerInstance.AuthenticationMethod = JobServer.InstanceAuthenticationMethod.WindowsAuthentication
        Else
            ServerInstance.AuthenticationMethod = JobServer.InstanceAuthenticationMethod.SQLAuthentication
            ServerInstance.UserName = UserName.Text
            ServerInstance.Password = Password.Text
        End If

        ServerJobList.Show()
        ServerJobList.ServerInstance = New JobServer
        ServerJobList.ServerInstance = ServerInstance
        ServerJobList.Activate()
        ServerJobList.PullJobList()
        If ServerJobList.CanFocus = True Then
            Me.Close()
        End If
    End Sub
    Private Sub Button2_Click_2(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        End
    End Sub

    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        ChangeForms()
    End Sub

    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton1.CheckedChanged
        AdjustTextBoxes()
    End Sub

    Private Sub RadioButton2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton2.CheckedChanged
        AdjustTextBoxes()
    End Sub
End Class
