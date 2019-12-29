<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ServerJobList
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ServerJobList))
        Me.RightClickMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.MnuAddJob = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuEditJob = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuDeleteJob = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem3 = New System.Windows.Forms.ToolStripSeparator
        Me.MnuEnableJob = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuDisableJob = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator
        Me.MnuViewJobHistory = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripSeparator
        Me.RefreshJobListToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.JobList = New System.Windows.Forms.ListView
        Me.JobId = New System.Windows.Forms.ColumnHeader
        Me.JobName = New System.Windows.Forms.ColumnHeader
        Me.Enabled = New System.Windows.Forms.ColumnHeader
        Me.Status = New System.Windows.Forms.ColumnHeader
        Me.LastRunTime = New System.Windows.Forms.ColumnHeader
        Me.NextRunTime = New System.Windows.Forms.ColumnHeader
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.CommandToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.JobEditorToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.AddJobToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.EditJobToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.DeleteJobToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ScheduleEditorToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.RefreshWindowToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.EditSQLAgentPropertiesToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.HelpToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.AboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.RightClickMenu.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'RightClickMenu
        '
        Me.RightClickMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuAddJob, Me.MnuEditJob, Me.MnuDeleteJob, Me.ToolStripMenuItem3, Me.MnuEnableJob, Me.MnuDisableJob, Me.ToolStripMenuItem1, Me.MnuViewJobHistory, Me.ToolStripMenuItem2, Me.RefreshJobListToolStripMenuItem})
        Me.RightClickMenu.Name = "RightClickMenu"
        Me.RightClickMenu.Size = New System.Drawing.Size(162, 176)
        '
        'MnuAddJob
        '
        Me.MnuAddJob.Name = "MnuAddJob"
        Me.MnuAddJob.Size = New System.Drawing.Size(161, 22)
        Me.MnuAddJob.Text = "&Add Job"
        '
        'MnuEditJob
        '
        Me.MnuEditJob.Name = "MnuEditJob"
        Me.MnuEditJob.Size = New System.Drawing.Size(161, 22)
        Me.MnuEditJob.Text = "&Edit Job"
        '
        'MnuDeleteJob
        '
        Me.MnuDeleteJob.Name = "MnuDeleteJob"
        Me.MnuDeleteJob.Size = New System.Drawing.Size(161, 22)
        Me.MnuDeleteJob.Text = "&Delete Job"
        '
        'ToolStripMenuItem3
        '
        Me.ToolStripMenuItem3.Name = "ToolStripMenuItem3"
        Me.ToolStripMenuItem3.Size = New System.Drawing.Size(158, 6)
        '
        'MnuEnableJob
        '
        Me.MnuEnableJob.Name = "MnuEnableJob"
        Me.MnuEnableJob.Size = New System.Drawing.Size(161, 22)
        Me.MnuEnableJob.Text = "E&nable Job"
        '
        'MnuDisableJob
        '
        Me.MnuDisableJob.Name = "MnuDisableJob"
        Me.MnuDisableJob.Size = New System.Drawing.Size(161, 22)
        Me.MnuDisableJob.Text = "&Disable Job"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(158, 6)
        '
        'MnuViewJobHistory
        '
        Me.MnuViewJobHistory.Name = "MnuViewJobHistory"
        Me.MnuViewJobHistory.Size = New System.Drawing.Size(161, 22)
        Me.MnuViewJobHistory.Text = "View Job &History"
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(158, 6)
        '
        'RefreshJobListToolStripMenuItem
        '
        Me.RefreshJobListToolStripMenuItem.Name = "RefreshJobListToolStripMenuItem"
        Me.RefreshJobListToolStripMenuItem.Size = New System.Drawing.Size(161, 22)
        Me.RefreshJobListToolStripMenuItem.Text = "&Refresh Job List"
        '
        'JobList
        '
        Me.JobList.AllowColumnReorder = True
        Me.JobList.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.JobId, Me.JobName, Me.Enabled, Me.Status, Me.LastRunTime, Me.NextRunTime})
        Me.JobList.ContextMenuStrip = Me.RightClickMenu
        Me.JobList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.JobList.FullRowSelect = True
        Me.JobList.GridLines = True
        Me.JobList.HideSelection = False
        Me.JobList.Location = New System.Drawing.Point(0, 24)
        Me.JobList.MultiSelect = False
        Me.JobList.Name = "JobList"
        Me.JobList.ShowItemToolTips = True
        Me.JobList.Size = New System.Drawing.Size(332, 238)
        Me.JobList.TabIndex = 1
        Me.JobList.UseCompatibleStateImageBehavior = False
        Me.JobList.View = System.Windows.Forms.View.Details
        '
        'JobId
        '
        Me.JobId.Text = "Job Id"
        '
        'JobName
        '
        Me.JobName.Text = "Job Name"
        '
        'Enabled
        '
        Me.Enabled.Text = "Enabled"
        '
        'Status
        '
        Me.Status.Text = "Status"
        '
        'LastRunTime
        '
        Me.LastRunTime.Text = "Last Run Time"
        '
        'NextRunTime
        '
        Me.NextRunTime.Text = "Next Run Time"
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CommandToolStripMenuItem, Me.HelpToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(332, 24)
        Me.MenuStrip1.TabIndex = 2
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'CommandToolStripMenuItem
        '
        Me.CommandToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.JobEditorToolStripMenuItem, Me.ScheduleEditorToolStripMenuItem, Me.RefreshWindowToolStripMenuItem, Me.EditSQLAgentPropertiesToolStripMenuItem})
        Me.CommandToolStripMenuItem.Name = "CommandToolStripMenuItem"
        Me.CommandToolStripMenuItem.Size = New System.Drawing.Size(76, 20)
        Me.CommandToolStripMenuItem.Text = "&Command"
        '
        'JobEditorToolStripMenuItem
        '
        Me.JobEditorToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AddJobToolStripMenuItem, Me.EditJobToolStripMenuItem, Me.DeleteJobToolStripMenuItem})
        Me.JobEditorToolStripMenuItem.Name = "JobEditorToolStripMenuItem"
        Me.JobEditorToolStripMenuItem.Size = New System.Drawing.Size(209, 22)
        Me.JobEditorToolStripMenuItem.Text = "&Job Editor"
        '
        'AddJobToolStripMenuItem
        '
        Me.AddJobToolStripMenuItem.Name = "AddJobToolStripMenuItem"
        Me.AddJobToolStripMenuItem.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.N), System.Windows.Forms.Keys)
        Me.AddJobToolStripMenuItem.Size = New System.Drawing.Size(160, 22)
        Me.AddJobToolStripMenuItem.Text = "Add Job"
        '
        'EditJobToolStripMenuItem
        '
        Me.EditJobToolStripMenuItem.Name = "EditJobToolStripMenuItem"
        Me.EditJobToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2
        Me.EditJobToolStripMenuItem.Size = New System.Drawing.Size(160, 22)
        Me.EditJobToolStripMenuItem.Text = "Edit Job"
        '
        'DeleteJobToolStripMenuItem
        '
        Me.DeleteJobToolStripMenuItem.Name = "DeleteJobToolStripMenuItem"
        Me.DeleteJobToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete
        Me.DeleteJobToolStripMenuItem.Size = New System.Drawing.Size(160, 22)
        Me.DeleteJobToolStripMenuItem.Text = "Delete Job"
        '
        'ScheduleEditorToolStripMenuItem
        '
        Me.ScheduleEditorToolStripMenuItem.Name = "ScheduleEditorToolStripMenuItem"
        Me.ScheduleEditorToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F3
        Me.ScheduleEditorToolStripMenuItem.Size = New System.Drawing.Size(209, 22)
        Me.ScheduleEditorToolStripMenuItem.Text = "&Schedule Editor"
        '
        'RefreshWindowToolStripMenuItem
        '
        Me.RefreshWindowToolStripMenuItem.Name = "RefreshWindowToolStripMenuItem"
        Me.RefreshWindowToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5
        Me.RefreshWindowToolStripMenuItem.Size = New System.Drawing.Size(209, 22)
        Me.RefreshWindowToolStripMenuItem.Text = "&Refresh Window"
        '
        'EditSQLAgentPropertiesToolStripMenuItem
        '
        Me.EditSQLAgentPropertiesToolStripMenuItem.Name = "EditSQLAgentPropertiesToolStripMenuItem"
        Me.EditSQLAgentPropertiesToolStripMenuItem.Size = New System.Drawing.Size(209, 22)
        Me.EditSQLAgentPropertiesToolStripMenuItem.Text = "&Edit SQL Agent Properties"
        '
        'HelpToolStripMenuItem
        '
        Me.HelpToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AboutToolStripMenuItem})
        Me.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem"
        Me.HelpToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
        Me.HelpToolStripMenuItem.Text = "&Help"
        '
        'AboutToolStripMenuItem
        '
        Me.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
        Me.AboutToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1
        Me.AboutToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.AboutToolStripMenuItem.Text = "&About"
        '
        'ServerJobList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(332, 262)
        Me.Controls.Add(Me.JobList)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "ServerJobList"
        Me.Text = "Job List"
        Me.RightClickMenu.ResumeLayout(False)
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RightClickMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents MnuAddJob As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuEditJob As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuDeleteJob As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents MnuViewJobHistory As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents JobId As System.Windows.Forms.ColumnHeader
    Friend WithEvents JobName As System.Windows.Forms.ColumnHeader
    Friend WithEvents Enabled As System.Windows.Forms.ColumnHeader
    Friend WithEvents Status As System.Windows.Forms.ColumnHeader
    Friend WithEvents LastRunTime As System.Windows.Forms.ColumnHeader
    Friend WithEvents NextRunTime As System.Windows.Forms.ColumnHeader
    Friend WithEvents JobList As System.Windows.Forms.ListView
    Friend WithEvents ToolStripMenuItem2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents RefreshJobListToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuEnableJob As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuDisableJob As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents CommandToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents JobEditorToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AddJobToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditJobToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RefreshWindowToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditSQLAgentPropertiesToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteJobToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents HelpToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AboutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ScheduleEditorToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
End Class
