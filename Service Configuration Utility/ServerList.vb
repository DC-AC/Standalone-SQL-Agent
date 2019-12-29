Imports System.Diagnostics
Imports System.IO
Imports SSA.CommonControl
Imports SSA.CommonControl.ConfigFile
Imports SSA.CommonControl.SharedCode
Imports ConfigSettings

Public Class ServerList
    Shared Servers(50) As JobServer
    Public ConfigFile As String

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'Dim FileName As String
        'Dim FileDiag As OpenFileDialog
        'FileDiag = New OpenFileDialog
        'FileDiag.Filter = "Config File|app.config"
        'FileDiag.ShowDialog()
        'FileName = FileDiag.FileName
        'If FileName <> "" Then
        '    ConfigFile = FileName
        '    ReadConfigFile(FileName)
        'Else
        '    If File.Exists(FileName) Then
        '        MsgBox("Config File Load Cancled!")
        '        Exit Sub
        '    Else
        '        If MsgBox("Config file not found.  Create New File", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
        '            WriteConfigFile()
        '        Else
        '            Exit Sub
        '        End If
        '    End If
        'End If

        ReadConfigFile()
        Button2.Enabled = True
        Button3.Enabled = True
        Button4.Enabled = True
        Button5.Enabled = True
    End Sub

    Private Sub ReadConfigFile()
        ReDim Servers(50)
        Dim aServer As JobServer
        Dim ServerCount As Integer = 0
        'Dim objReader As StreamReader
        Dim ConnString As String
        Dim ConnStringSplit As Array
        Dim ShortString As String
        Dim InstanceCount As Integer = 1

        UIServerList.Items.Clear()

        'Try
        'objReader = New StreamReader(FileName)
        ' MsgBox(ReturnInstanceCount())
        While InstanceCount <= ReturnInstanceCount()
            ConnString = ReturnConnectionString(InstanceCount)

            ConnStringSplit = ConnString.Split(";")

            aServer = New JobServer

            For Each ShortString In ConnStringSplit
                If ShortString.Contains("Server") Then aServer.InstanceName = ShortString.Replace("Server=", "")
                If ShortString.Contains("Database") Then aServer.DatabaseName = ShortString.Replace("Database=", "")
                If ShortString.Contains("Trusted_Connection") = True Then
                    aServer.AuthenticationMethod = JobServer.InstanceAuthenticationMethod.WindowsAuthentication
                Else
                    If aServer.AuthenticationMethod <> JobServer.InstanceAuthenticationMethod.WindowsAuthentication Then
                        aServer.AuthenticationMethod = JobServer.InstanceAuthenticationMethod.SQLAuthentication
                        aServer.UserName = ShortString.Replace("UserName=", "")
                        aServer.Password = ShortString.Replace("Password=", "")
                    End If
                End If
                Debug.Print(ShortString)
            Next

            WriteServerData(aServer.InstanceName, aServer.DatabaseName, aServer.AuthenticationMethod, aServer.UserName, aServer.Password)

            ServerCount += 1
            InstanceCount += 1
        End While

        'objReader.Close()
        'Catch Ex As Exception
        'MsgBox(Ex.Message)
        'End Try
        RefreshScreenList()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        ServerEditor.Show()
        ServerEditor.Text = "New Server"
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If UIServerList.SelectedItems.Count = 0 Then
            MsgBox("Please select a server from the list before clicking edit.")
            Exit Sub
        End If
        ServerEditor.Show()

        ServerEditor.InstanceName.Text = UIServerList.SelectedItems.Item(0).SubItems(0).Text
        ServerEditor.DatabaseName.Text = UIServerList.SelectedItems.Item(0).SubItems(1).Text
        If UIServerList.SelectedItems.Item(0).SubItems(2).Text = "Windows" Then
            ServerEditor.RadioButton1.Checked = True
            ServerEditor.UserName.Enabled = False
            ServerEditor.Password.Enabled = False
        Else
            ServerEditor.RadioButton2.Checked = True
            ServerEditor.UserName.Text = UIServerList.SelectedItems.Item(0).SubItems(3).Text
            ServerEditor.UserName.Enabled = True
            ServerEditor.Password.Text = UIServerList.SelectedItems.Item(0).SubItems(4).Text
            ServerEditor.Password.Enabled = True
        End If
    End Sub
    Private Sub WriteConfigFile()
        If UIServerList.Items.Count = 0 Then
            MsgBox("You can not write the configuration file until there is at least one database instance on the list.")
            Exit Sub
        End If

        If File.Exists(ConfigFile + ".backup") Then
            Try
                File.Delete(ConfigFile + ".backup")
            Catch ex As Exception
                MsgBox("Error saving config file." + vbCrLf + ex.Message, MsgBoxStyle.Critical)
                Exit Sub
            End Try
        End If

        If File.Exists(ConfigFile) = True Then
            Try
                File.Move(ConfigFile, ConfigFile + ".backup")
            Catch ex As Exception
                MsgBox("Error saving config file." + vbCrLf + ex.Message, MsgBoxStyle.Critical)
                Exit Sub
            End Try
        End If

        Dim InstanceCount As Integer = 1

        While InstanceCount <= ReturnInstanceCount()
            RemoveSetting(ReturnRealInstanceName(ReturnInstanceName(InstanceCount)))
            RemoveSetting(ReturnInstanceName(InstanceCount))
            InstanceCount += 1
        End While

        WriteSetting("InstanceCount", 0)


        Dim bAns As Boolean = False
        'Dim objReader As StreamWriter
        Dim aServer As JobServer
        Dim ConnString As String
        For Each aServer In Servers
            If aServer IsNot Nothing Then
                If aServer.AuthenticationMethod = JobServer.InstanceAuthenticationMethod.WindowsAuthentication Then
                    ConnString = FormatconnectionString(aServer.InstanceName, aServer.DatabaseName)
                Else
                    ConnString = FormatconnectionString(aServer.InstanceName, aServer.DatabaseName, JobServer.InstanceAuthenticationMethod.SQLAuthentication, aServer.UserName, aServer.Password)
                End If
                AddConnectionString(ConnString, aServer.InstanceName)
                'Try
                '    objReader = New StreamWriter(ConfigFile, True)
                '    objReader.WriteLine(ConnString)
                '    objReader.Close()
                '    bAns = True
                'Catch Ex As Exception
                '    MsgBox(Ex.Message, MsgBoxStyle.Critical)
                '    Exit Sub
                'End Try
            End If
        Next
        MsgBox("Config File has been saved." + vbCrLf + "You must restart the Standalone SQL Agent Service for any changes to take effect.", MsgBoxStyle.Information)

    End Sub
    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        WriteConfigFile()
    End Sub


    Public Overloads Shared Sub WriteServerData(ByVal InstanceName As String, ByVal DatabaseName As String)
        WriteServerData(InstanceName, DatabaseName, JobServer.InstanceAuthenticationMethod.WindowsAuthentication, Nothing, Nothing, False)
    End Sub
    Public Overloads Shared Sub WriteServerData(ByVal InstanceName As String, ByVal DatabaseName As String, ByVal RefreshScreen As Boolean)
        WriteServerData(InstanceName, DatabaseName, JobServer.InstanceAuthenticationMethod.WindowsAuthentication, Nothing, Nothing, RefreshScreen)
    End Sub
    Public Overloads Shared Sub WriteServerdata(ByVal InstanceName As String, ByVal DatabaseName As String, ByVal AuthenticationMethod As JobServer.InstanceAuthenticationMethod, ByVal UserName As String, ByVal Password As String)
        WriteServerData(InstanceName, DatabaseName, AuthenticationMethod, UserName, Password, False)
    End Sub
    Public Overloads Shared Sub WriteServerData(ByVal InstanceName As String, ByVal DatabaseName As String, ByVal AuthenticationMethod As JobServer.InstanceAuthenticationMethod, ByVal UserName As String, ByVal Password As String, ByVal RefreshScreen As Boolean)

        Dim LoopCount As Integer = 0
        Do Until Servers(LoopCount) Is Nothing
            If Servers(LoopCount).InstanceName = InstanceName Then Exit Do
            LoopCount += 1
        Loop
        If Servers(LoopCount) Is Nothing Then
            Servers(LoopCount) = New JobServer
        End If
        Servers(LoopCount).InstanceName = InstanceName
        Servers(LoopCount).DatabaseName = DatabaseName
        Servers(LoopCount).AuthenticationMethod = AuthenticationMethod
        Servers(LoopCount).UserName = UserName
        Servers(LoopCount).Password = Password

        If RefreshScreen = True Then ServerList.RefreshScreenList()

    End Sub
    Private Sub RefreshScreenList()
        Dim Liststring(5) As String
        Dim LoopCount As Integer
        Dim ListItem As ListViewItem

        UIServerList.Items.Clear()

        Do Until LoopCount = 50
            If Servers(LoopCount) IsNot Nothing Then

                ReDim Liststring(5)
                ListItem = New ListViewItem

                Liststring(0) = Servers(LoopCount).InstanceName
                Liststring(1) = Servers(LoopCount).DatabaseName
                If Servers(LoopCount).AuthenticationMethod = JobServer.InstanceAuthenticationMethod.WindowsAuthentication Then
                    Liststring(2) = "Windows"
                Else
                    Liststring(2) = "SQL"
                    Liststring(3) = Servers(LoopCount).UserName
                    Liststring(4) = Servers(LoopCount).Password
                End If

                ListItem = New ListViewItem(Liststring)
                UIServerList.Items.Add(ListItem)
            End If
            LoopCount += 1
        Loop

    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        If UIServerList.SelectedItems.Count = 0 Then
            MsgBox("Please select a server from the list before clicking delete.")
            Exit Sub
        End If

        Dim LoopCount As Integer = 0
        Do
            If Servers(LoopCount) IsNot Nothing Then
                If Servers(LoopCount).InstanceName = UIServerList.SelectedItems.Item(0).SubItems(0).Text Then Exit Do
            End If

            LoopCount += 1
        Loop
        Servers(LoopCount) = Nothing
        RefreshScreenList()
        If UIServerList.Items.Count = 0 Then
            Button3.Enabled = False
        End If
    End Sub
End Class
