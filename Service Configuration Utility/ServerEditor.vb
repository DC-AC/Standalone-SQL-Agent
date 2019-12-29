Imports Service_Configuration_Utility.ServerList
Public Class ServerEditor

    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton1.CheckedChanged
        If RadioButton1.Checked = True Then
            UserName.Enabled = False
            Password.Enabled = False
        Else
            UserName.Enabled = True
            Password.Enabled = True
        End If
    End Sub

    Private Sub RadioButton2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton2.CheckedChanged
        If RadioButton1.Checked = True Then
            UserName.Enabled = False
            Password.Enabled = False
        Else
            UserName.Enabled = True
            Password.Enabled = True
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If DatabaseName.Text = "" Then
            If MsgBox("Please enter the database name which will hold the SQL Server Agent database objects." + vbCrLf + "This is normally the msdb database.  Would you like to use the msdb database?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                DatabaseName.Text = "msdb"
            Else
                Exit Sub
            End If
        End If
        If LCase(DatabaseName.Text) <> "msdb" Then
            MsgBox("A custom database name has been entered.  This is an advanced configuration which may not work correctly.", MsgBoxStyle.Exclamation)
        End If
        If InstanceName.Text = "" Then
            MsgBox("Please enter the name of the instance to connect to.", MsgBoxStyle.Critical)
            Exit Sub
        End If
        If Me.RadioButton1.Checked = True Then
            WriteServerData(InstanceName.Text, DatabaseName.Text, True)
        Else
            If UserName.Text = "" Then
                MsgBox("Please enter a valid username", MsgBoxStyle.Critical)
                Exit Sub
            End If
            If Password.Text = "" Then
                MsgBox("A blank password was supplied.  This is an insecure configuration and should be changed.", MsgBoxStyle.Information)
            End If
            WriteServerData(InstanceName.Text, DatabaseName.Text, SSA.CommonControl.JobServer.InstanceAuthenticationMethod.SQLAuthentication, UserName.Text, Password.Text, True)
        End If
        ServerList.Button3.Enabled = True
        Me.Close()
    End Sub

End Class