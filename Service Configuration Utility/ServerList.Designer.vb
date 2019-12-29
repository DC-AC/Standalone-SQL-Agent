<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ServerList
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ServerList))
        Me.Button1 = New System.Windows.Forms.Button
        Me.Button2 = New System.Windows.Forms.Button
        Me.Button3 = New System.Windows.Forms.Button
        Me.UIServerList = New System.Windows.Forms.ListView
        Me.ServerName = New System.Windows.Forms.ColumnHeader
        Me.Database = New System.Windows.Forms.ColumnHeader
        Me.AuthenticationMethod = New System.Windows.Forms.ColumnHeader
        Me.UserName = New System.Windows.Forms.ColumnHeader
        Me.Password = New System.Windows.Forms.ColumnHeader
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.Button4 = New System.Windows.Forms.Button
        Me.Button5 = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(69, 13)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(93, 23)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Load Config File"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Enabled = False
        Me.Button2.Location = New System.Drawing.Point(216, 13)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(95, 23)
        Me.Button2.TabIndex = 1
        Me.Button2.Text = "Save Config File"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Enabled = False
        Me.Button3.Location = New System.Drawing.Point(159, 42)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(73, 23)
        Me.Button3.TabIndex = 2
        Me.Button3.Text = "Edit Server"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'UIServerList
        '
        Me.UIServerList.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ServerName, Me.Database, Me.AuthenticationMethod, Me.UserName, Me.Password})
        Me.UIServerList.FullRowSelect = True
        Me.UIServerList.Location = New System.Drawing.Point(13, 71)
        Me.UIServerList.MultiSelect = False
        Me.UIServerList.Name = "UIServerList"
        Me.UIServerList.Size = New System.Drawing.Size(356, 187)
        Me.UIServerList.TabIndex = 3
        Me.UIServerList.UseCompatibleStateImageBehavior = False
        Me.UIServerList.View = System.Windows.Forms.View.Details
        '
        'ServerName
        '
        Me.ServerName.Text = "Server Name"
        Me.ServerName.Width = 81
        '
        'Database
        '
        Me.Database.Text = "Database"
        '
        'AuthenticationMethod
        '
        Me.AuthenticationMethod.Text = "Authentication Method"
        Me.AuthenticationMethod.Width = 81
        '
        'UserName
        '
        Me.UserName.Text = "User Name"
        Me.UserName.Width = 70
        '
        'Password
        '
        Me.Password.Text = "Password"
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'Button4
        '
        Me.Button4.Enabled = False
        Me.Button4.Location = New System.Drawing.Point(58, 42)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(77, 23)
        Me.Button4.TabIndex = 4
        Me.Button4.Text = "Add Server"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Button5
        '
        Me.Button5.Enabled = False
        Me.Button5.Location = New System.Drawing.Point(252, 42)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(81, 23)
        Me.Button5.TabIndex = 5
        Me.Button5.Text = "Delete Server"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'ServerList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(384, 267)
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.UIServerList)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.Name = "ServerList"
        Me.Text = "Configuration File Editor"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents UIServerList As System.Windows.Forms.ListView
    Friend WithEvents ServerName As System.Windows.Forms.ColumnHeader
    Friend WithEvents Database As System.Windows.Forms.ColumnHeader
    Friend WithEvents AuthenticationMethod As System.Windows.Forms.ColumnHeader
    Friend WithEvents UserName As System.Windows.Forms.ColumnHeader
    Friend WithEvents Password As System.Windows.Forms.ColumnHeader
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents Button5 As System.Windows.Forms.Button

End Class
