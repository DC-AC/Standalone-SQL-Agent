<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class JobEditorScheduleList
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(JobEditorScheduleList))
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.ScheduleList = New System.Windows.Forms.ListView
        Me.ScheduleId = New System.Windows.Forms.ColumnHeader
        Me.ScheduleName = New System.Windows.Forms.ColumnHeader
        Me.Enabled = New System.Windows.Forms.ColumnHeader
        Me.JobCount = New System.Windows.Forms.ColumnHeader
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer
        Me.Button1 = New System.Windows.Forms.Button
        Me.Button2 = New System.Windows.Forms.Button
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
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
        Me.SplitContainer1.Panel1.Controls.Add(Me.ScheduleList)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Size = New System.Drawing.Size(415, 273)
        Me.SplitContainer1.SplitterDistance = 230
        Me.SplitContainer1.TabIndex = 0
        '
        'ScheduleList
        '
        Me.ScheduleList.AllowColumnReorder = True
        Me.ScheduleList.CheckBoxes = True
        Me.ScheduleList.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ScheduleId, Me.ScheduleName, Me.Enabled, Me.JobCount})
        Me.ScheduleList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ScheduleList.FullRowSelect = True
        Me.ScheduleList.GridLines = True
        Me.ScheduleList.HideSelection = False
        Me.ScheduleList.Location = New System.Drawing.Point(0, 0)
        Me.ScheduleList.MultiSelect = False
        Me.ScheduleList.Name = "ScheduleList"
        Me.ScheduleList.ShowItemToolTips = True
        Me.ScheduleList.Size = New System.Drawing.Size(415, 230)
        Me.ScheduleList.TabIndex = 0
        Me.ScheduleList.UseCompatibleStateImageBehavior = False
        Me.ScheduleList.View = System.Windows.Forms.View.Details
        '
        'ScheduleId
        '
        Me.ScheduleId.Text = "Schedule Id"
        '
        'ScheduleName
        '
        Me.ScheduleName.Text = "Schedule Name"
        '
        'Enabled
        '
        Me.Enabled.Text = "Enabled"
        '
        'JobCount
        '
        Me.JobCount.Text = "Job Count"
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.Button1)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.Button2)
        Me.SplitContainer2.Size = New System.Drawing.Size(415, 39)
        Me.SplitContainer2.SplitterDistance = 181
        Me.SplitContainer2.TabIndex = 0
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(55, 8)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(101, 23)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Save Settings"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(78, 8)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(99, 23)
        Me.Button2.TabIndex = 0
        Me.Button2.Text = "Cancel Settings"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'JobEditorScheduleList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(415, 273)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "JobEditorScheduleList"
        Me.Text = "Available Schedules"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents ScheduleList As System.Windows.Forms.ListView
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents ScheduleId As System.Windows.Forms.ColumnHeader
    Friend WithEvents ScheduleName As System.Windows.Forms.ColumnHeader
    Friend WithEvents Enabled As System.Windows.Forms.ColumnHeader
    Friend WithEvents JobCount As System.Windows.Forms.ColumnHeader
End Class
