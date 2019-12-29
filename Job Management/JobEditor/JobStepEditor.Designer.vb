<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class JobStepEditor
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.Command = New System.Windows.Forms.TextBox
        Me.Database = New System.Windows.Forms.ComboBox
        Me.StepType = New System.Windows.Forms.ComboBox
        Me.StepName = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.FailStep = New System.Windows.Forms.ComboBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.SuccessStep = New System.Windows.Forms.ComboBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.AppendFile = New System.Windows.Forms.CheckBox
        Me.OutputFile = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.OnFailAction = New System.Windows.Forms.ComboBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.RetryInterval = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.RetryAttempt = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.OnSuccessAction = New System.Windows.Forms.ComboBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Button2 = New System.Windows.Forms.Button
        Me.Button1 = New System.Windows.Forms.Button
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.TabControl1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.Button2)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Button1)
        Me.SplitContainer1.Size = New System.Drawing.Size(480, 338)
        Me.SplitContainer1.SplitterDistance = 290
        Me.SplitContainer1.TabIndex = 0
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(480, 290)
        Me.TabControl1.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.Command)
        Me.TabPage1.Controls.Add(Me.Database)
        Me.TabPage1.Controls.Add(Me.StepType)
        Me.TabPage1.Controls.Add(Me.StepName)
        Me.TabPage1.Controls.Add(Me.Label5)
        Me.TabPage1.Controls.Add(Me.Label4)
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(472, 264)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "General"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Command
        '
        Me.Command.Location = New System.Drawing.Point(116, 109)
        Me.Command.Multiline = True
        Me.Command.Name = "Command"
        Me.Command.Size = New System.Drawing.Size(338, 135)
        Me.Command.TabIndex = 8
        '
        'Database
        '
        Me.Database.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Database.FormattingEnabled = True
        Me.Database.Location = New System.Drawing.Point(116, 76)
        Me.Database.Name = "Database"
        Me.Database.Size = New System.Drawing.Size(338, 21)
        Me.Database.TabIndex = 7
        '
        'StepType
        '
        Me.StepType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.StepType.FormattingEnabled = True
        Me.StepType.Items.AddRange(New Object() {"Operating System (CmdExec)", "T-SQL Script"})
        Me.StepType.Location = New System.Drawing.Point(116, 28)
        Me.StepType.Name = "StepType"
        Me.StepType.Size = New System.Drawing.Size(338, 21)
        Me.StepType.TabIndex = 6
        '
        'StepName
        '
        Me.StepName.Location = New System.Drawing.Point(116, 4)
        Me.StepName.Name = "StepName"
        Me.StepName.Size = New System.Drawing.Size(338, 20)
        Me.StepName.TabIndex = 5
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(28, 112)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(57, 13)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Command:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(28, 79)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(56, 13)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Database:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(9, 55)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(45, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Run As:"
        Me.Label3.Visible = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 31)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(59, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Step Type:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 7)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(63, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Step Name:"
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.FailStep)
        Me.TabPage2.Controls.Add(Me.Label12)
        Me.TabPage2.Controls.Add(Me.SuccessStep)
        Me.TabPage2.Controls.Add(Me.Label11)
        Me.TabPage2.Controls.Add(Me.AppendFile)
        Me.TabPage2.Controls.Add(Me.OutputFile)
        Me.TabPage2.Controls.Add(Me.Label10)
        Me.TabPage2.Controls.Add(Me.OnFailAction)
        Me.TabPage2.Controls.Add(Me.Label9)
        Me.TabPage2.Controls.Add(Me.RetryInterval)
        Me.TabPage2.Controls.Add(Me.Label8)
        Me.TabPage2.Controls.Add(Me.RetryAttempt)
        Me.TabPage2.Controls.Add(Me.Label7)
        Me.TabPage2.Controls.Add(Me.OnSuccessAction)
        Me.TabPage2.Controls.Add(Me.Label6)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(472, 264)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Advanced"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'FailStep
        '
        Me.FailStep.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.FailStep.FormattingEnabled = True
        Me.FailStep.Location = New System.Drawing.Point(150, 133)
        Me.FailStep.Name = "FailStep"
        Me.FailStep.Size = New System.Drawing.Size(227, 21)
        Me.FailStep.TabIndex = 14
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(69, 136)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(65, 13)
        Me.Label12.TabIndex = 13
        Me.Label12.Text = "Go To Step:"
        '
        'SuccessStep
        '
        Me.SuccessStep.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.SuccessStep.FormattingEnabled = True
        Me.SuccessStep.Location = New System.Drawing.Point(150, 34)
        Me.SuccessStep.Name = "SuccessStep"
        Me.SuccessStep.Size = New System.Drawing.Size(227, 21)
        Me.SuccessStep.TabIndex = 12
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(69, 37)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(65, 13)
        Me.Label11.TabIndex = 11
        Me.Label11.Text = "Go To Step:"
        '
        'AppendFile
        '
        Me.AppendFile.AutoSize = True
        Me.AppendFile.Location = New System.Drawing.Point(38, 196)
        Me.AppendFile.Name = "AppendFile"
        Me.AppendFile.Size = New System.Drawing.Size(98, 17)
        Me.AppendFile.TabIndex = 10
        Me.AppendFile.Text = "Append To File"
        Me.AppendFile.UseVisualStyleBackColor = True
        '
        'OutputFile
        '
        Me.OutputFile.Location = New System.Drawing.Point(116, 170)
        Me.OutputFile.Name = "OutputFile"
        Me.OutputFile.Size = New System.Drawing.Size(261, 20)
        Me.OutputFile.TabIndex = 9
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(8, 173)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(61, 13)
        Me.Label10.TabIndex = 8
        Me.Label10.Text = "Output File:"
        '
        'OnFailAction
        '
        Me.OnFailAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.OnFailAction.FormattingEnabled = True
        Me.OnFailAction.Items.AddRange(New Object() {"Go To Next Step", "Go To Step", "Quit Job Reporting Success", "Quit Job Reporting Failure"})
        Me.OnFailAction.Location = New System.Drawing.Point(131, 102)
        Me.OnFailAction.Name = "OnFailAction"
        Me.OnFailAction.Size = New System.Drawing.Size(244, 21)
        Me.OnFailAction.TabIndex = 7
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(5, 105)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(91, 13)
        Me.Label9.TabIndex = 6
        Me.Label9.Text = "On Failure Action:"
        '
        'RetryInterval
        '
        Me.RetryInterval.Location = New System.Drawing.Point(345, 65)
        Me.RetryInterval.Name = "RetryInterval"
        Me.RetryInterval.Size = New System.Drawing.Size(32, 20)
        Me.RetryInterval.TabIndex = 5
        Me.RetryInterval.Text = "0"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(201, 68)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(118, 13)
        Me.Label8.TabIndex = 4
        Me.Label8.Text = "Retry Interval (minutes):"
        '
        'RetryAttempt
        '
        Me.RetryAttempt.Location = New System.Drawing.Point(114, 65)
        Me.RetryAttempt.Name = "RetryAttempt"
        Me.RetryAttempt.Size = New System.Drawing.Size(33, 20)
        Me.RetryAttempt.TabIndex = 3
        Me.RetryAttempt.Text = "0"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(6, 68)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(79, 13)
        Me.Label7.TabIndex = 2
        Me.Label7.Text = "Retry Attempts:"
        '
        'OnSuccessAction
        '
        Me.OnSuccessAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.OnSuccessAction.FormattingEnabled = True
        Me.OnSuccessAction.Items.AddRange(New Object() {"Go To Next Step", "Go To Step", "Quit Job Reporting Success", "Quit Job Reporting Failure"})
        Me.OnSuccessAction.Location = New System.Drawing.Point(131, 4)
        Me.OnSuccessAction.Name = "OnSuccessAction"
        Me.OnSuccessAction.Size = New System.Drawing.Size(246, 21)
        Me.OnSuccessAction.TabIndex = 1
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(7, 7)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(101, 13)
        Me.Label6.TabIndex = 0
        Me.Label6.Text = "On Success Action:"
        '
        'Button2
        '
        Me.Button2.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Button2.Location = New System.Drawing.Point(288, 13)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 23)
        Me.Button2.TabIndex = 1
        Me.Button2.Text = "Cancel"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(120, 13)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Save"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'JobStepEditor
        '
        Me.AcceptButton = Me.Button1
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.Button2
        Me.ClientSize = New System.Drawing.Size(480, 338)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Name = "JobStepEditor"
        Me.Text = "JobStepEditor"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents StepType As System.Windows.Forms.ComboBox
    Friend WithEvents StepName As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Command As System.Windows.Forms.TextBox
    Friend WithEvents Database As System.Windows.Forms.ComboBox
    Friend WithEvents RetryInterval As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents RetryAttempt As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents OnSuccessAction As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents AppendFile As System.Windows.Forms.CheckBox
    Friend WithEvents OutputFile As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents OnFailAction As System.Windows.Forms.ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents FailStep As System.Windows.Forms.ComboBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents SuccessStep As System.Windows.Forms.ComboBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
End Class
