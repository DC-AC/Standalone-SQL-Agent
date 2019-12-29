<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class JobEditor
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(JobEditor))
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.LastExecuted = New System.Windows.Forms.Label
        Me.LastModified = New System.Windows.Forms.Label
        Me.Created = New System.Windows.Forms.Label
        Me.Description = New System.Windows.Forms.TextBox
        Me.Category = New System.Windows.Forms.ComboBox
        Me.OwnerUserName = New System.Windows.Forms.TextBox
        Me.JobName = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.JobEnabled = New System.Windows.Forms.CheckBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer
        Me.JobStepList = New System.Windows.Forms.ListView
        Me.ColumnHeader1 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader2 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader3 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader4 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader5 = New System.Windows.Forms.ColumnHeader
        Me.Label9 = New System.Windows.Forms.Label
        Me.StartStep = New System.Windows.Forms.ComboBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.Button5 = New System.Windows.Forms.Button
        Me.Button4 = New System.Windows.Forms.Button
        Me.DeleteStep = New System.Windows.Forms.Button
        Me.EditStep = New System.Windows.Forms.Button
        Me.NewStep = New System.Windows.Forms.Button
        Me.TabPage3 = New System.Windows.Forms.TabPage
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer
        Me.JobScheduleList = New System.Windows.Forms.ListView
        Me.ColumnHeader6 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader7 = New System.Windows.Forms.ColumnHeader
        Me.ColumnHeader8 = New System.Windows.Forms.ColumnHeader
        Me.PickSchedule = New System.Windows.Forms.Button
        Me.TabPage4 = New System.Windows.Forms.TabPage
        Me.Label10 = New System.Windows.Forms.Label
        Me.Action_Delete = New System.Windows.Forms.ComboBox
        Me.Action_EventLog = New System.Windows.Forms.ComboBox
        Me.Action_NetSend = New System.Windows.Forms.ComboBox
        Me.Action_Page = New System.Windows.Forms.ComboBox
        Me.Action_Email = New System.Windows.Forms.ComboBox
        Me.Operator_NetSend = New System.Windows.Forms.ComboBox
        Me.Operator_PAge = New System.Windows.Forms.ComboBox
        Me.Operator_Email = New System.Windows.Forms.ComboBox
        Me.Delete = New System.Windows.Forms.CheckBox
        Me.EventLog = New System.Windows.Forms.CheckBox
        Me.NetSend = New System.Windows.Forms.CheckBox
        Me.PAge = New System.Windows.Forms.CheckBox
        Me.Email = New System.Windows.Forms.CheckBox
        Me.Button6 = New System.Windows.Forms.Button
        Me.CancelButton = New System.Windows.Forms.Button
        Me.SaveButton = New System.Windows.Forms.Button
        Me.ImageList1 = New System.Windows.Forms.ImageList(Me.components)
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        Me.TabPage4.SuspendLayout()
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
        Me.SplitContainer1.Panel2.Controls.Add(Me.Button6)
        Me.SplitContainer1.Panel2.Controls.Add(Me.CancelButton)
        Me.SplitContainer1.Panel2.Controls.Add(Me.SaveButton)
        Me.SplitContainer1.Size = New System.Drawing.Size(530, 365)
        Me.SplitContainer1.SplitterDistance = 308
        Me.SplitContainer1.TabIndex = 0
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Controls.Add(Me.TabPage4)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(530, 308)
        Me.TabControl1.TabIndex = 1
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.LastExecuted)
        Me.TabPage1.Controls.Add(Me.LastModified)
        Me.TabPage1.Controls.Add(Me.Created)
        Me.TabPage1.Controls.Add(Me.Description)
        Me.TabPage1.Controls.Add(Me.Category)
        Me.TabPage1.Controls.Add(Me.OwnerUserName)
        Me.TabPage1.Controls.Add(Me.JobName)
        Me.TabPage1.Controls.Add(Me.Label7)
        Me.TabPage1.Controls.Add(Me.Label6)
        Me.TabPage1.Controls.Add(Me.Label5)
        Me.TabPage1.Controls.Add(Me.JobEnabled)
        Me.TabPage1.Controls.Add(Me.Label4)
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(522, 282)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Job Info"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'LastExecuted
        '
        Me.LastExecuted.BackColor = System.Drawing.Color.Gainsboro
        Me.LastExecuted.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LastExecuted.Location = New System.Drawing.Point(135, 250)
        Me.LastExecuted.Name = "LastExecuted"
        Me.LastExecuted.Size = New System.Drawing.Size(171, 23)
        Me.LastExecuted.TabIndex = 14
        '
        'LastModified
        '
        Me.LastModified.BackColor = System.Drawing.Color.Gainsboro
        Me.LastModified.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LastModified.Location = New System.Drawing.Point(135, 223)
        Me.LastModified.Name = "LastModified"
        Me.LastModified.Size = New System.Drawing.Size(171, 23)
        Me.LastModified.TabIndex = 13
        '
        'Created
        '
        Me.Created.BackColor = System.Drawing.Color.Gainsboro
        Me.Created.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Created.Location = New System.Drawing.Point(135, 196)
        Me.Created.Name = "Created"
        Me.Created.Size = New System.Drawing.Size(171, 23)
        Me.Created.TabIndex = 12
        '
        'Description
        '
        Me.Description.Location = New System.Drawing.Point(135, 83)
        Me.Description.Multiline = True
        Me.Description.Name = "Description"
        Me.Description.Size = New System.Drawing.Size(171, 100)
        Me.Description.TabIndex = 11
        '
        'Category
        '
        Me.Category.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Category.FormattingEnabled = True
        Me.Category.Location = New System.Drawing.Point(135, 56)
        Me.Category.Name = "Category"
        Me.Category.Size = New System.Drawing.Size(171, 21)
        Me.Category.TabIndex = 10
        '
        'OwnerUserName
        '
        Me.OwnerUserName.Location = New System.Drawing.Point(135, 30)
        Me.OwnerUserName.Name = "OwnerUserName"
        Me.OwnerUserName.Size = New System.Drawing.Size(171, 20)
        Me.OwnerUserName.TabIndex = 9
        '
        'JobName
        '
        Me.JobName.Location = New System.Drawing.Point(135, 4)
        Me.JobName.Name = "JobName"
        Me.JobName.Size = New System.Drawing.Size(171, 20)
        Me.JobName.TabIndex = 8
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(9, 251)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(78, 13)
        Me.Label7.TabIndex = 7
        Me.Label7.Text = "Last Executed:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(9, 224)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(73, 13)
        Me.Label6.TabIndex = 6
        Me.Label6.Text = "Last Modified:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(9, 197)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(47, 13)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "Created:"
        '
        'JobEnabled
        '
        Me.JobEnabled.AutoSize = True
        Me.JobEnabled.Location = New System.Drawing.Point(12, 166)
        Me.JobEnabled.Name = "JobEnabled"
        Me.JobEnabled.Size = New System.Drawing.Size(65, 17)
        Me.JobEnabled.TabIndex = 4
        Me.JobEnabled.Text = "Enabled"
        Me.JobEnabled.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(9, 79)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(63, 13)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Description:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(9, 54)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(52, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Category:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 31)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(92, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Owner Username:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 7)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(58, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Job Name:"
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.SplitContainer2)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(522, 282)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Job Steps"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(3, 3)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.JobStepList)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.Label9)
        Me.SplitContainer2.Panel2.Controls.Add(Me.StartStep)
        Me.SplitContainer2.Panel2.Controls.Add(Me.Label8)
        Me.SplitContainer2.Panel2.Controls.Add(Me.Button5)
        Me.SplitContainer2.Panel2.Controls.Add(Me.Button4)
        Me.SplitContainer2.Panel2.Controls.Add(Me.DeleteStep)
        Me.SplitContainer2.Panel2.Controls.Add(Me.EditStep)
        Me.SplitContainer2.Panel2.Controls.Add(Me.NewStep)
        Me.SplitContainer2.Size = New System.Drawing.Size(516, 276)
        Me.SplitContainer2.SplitterDistance = 201
        Me.SplitContainer2.TabIndex = 0
        '
        'JobStepList
        '
        Me.JobStepList.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader1, Me.ColumnHeader2, Me.ColumnHeader3, Me.ColumnHeader4, Me.ColumnHeader5})
        Me.JobStepList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.JobStepList.FullRowSelect = True
        Me.JobStepList.GridLines = True
        Me.JobStepList.Location = New System.Drawing.Point(0, 0)
        Me.JobStepList.MultiSelect = False
        Me.JobStepList.Name = "JobStepList"
        Me.JobStepList.Size = New System.Drawing.Size(516, 201)
        Me.JobStepList.TabIndex = 0
        Me.JobStepList.UseCompatibleStateImageBehavior = False
        Me.JobStepList.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "Step Id"
        '
        'ColumnHeader2
        '
        Me.ColumnHeader2.Text = "Step Name"
        Me.ColumnHeader2.Width = 120
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Job Step Type"
        Me.ColumnHeader3.Width = 123
        '
        'ColumnHeader4
        '
        Me.ColumnHeader4.Text = "On Success"
        Me.ColumnHeader4.Width = 84
        '
        'ColumnHeader5
        '
        Me.ColumnHeader5.Text = "On Failure"
        Me.ColumnHeader5.Width = 88
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(141, 20)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(57, 13)
        Me.Label9.TabIndex = 7
        Me.Label9.Text = "Start Step:"
        '
        'StartStep
        '
        Me.StartStep.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.StartStep.FormattingEnabled = True
        Me.StartStep.Location = New System.Drawing.Point(204, 17)
        Me.StartStep.Name = "StartStep"
        Me.StartStep.Size = New System.Drawing.Size(279, 21)
        Me.StartStep.TabIndex = 6
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(12, 25)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(59, 13)
        Me.Label8.TabIndex = 5
        Me.Label8.Text = "Move Step"
        '
        'Button5
        '
        Me.Button5.Image = CType(resources.GetObject("Button5.Image"), System.Drawing.Image)
        Me.Button5.Location = New System.Drawing.Point(42, 44)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(23, 23)
        Me.Button5.TabIndex = 4
        Me.Button5.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Image = CType(resources.GetObject("Button4.Image"), System.Drawing.Image)
        Me.Button4.Location = New System.Drawing.Point(15, 44)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(23, 23)
        Me.Button4.TabIndex = 3
        Me.Button4.UseVisualStyleBackColor = True
        '
        'DeleteStep
        '
        Me.DeleteStep.Enabled = False
        Me.DeleteStep.Location = New System.Drawing.Point(407, 44)
        Me.DeleteStep.Name = "DeleteStep"
        Me.DeleteStep.Size = New System.Drawing.Size(75, 23)
        Me.DeleteStep.TabIndex = 2
        Me.DeleteStep.Text = "Delete Step"
        Me.DeleteStep.UseVisualStyleBackColor = True
        '
        'EditStep
        '
        Me.EditStep.Location = New System.Drawing.Point(305, 44)
        Me.EditStep.Name = "EditStep"
        Me.EditStep.Size = New System.Drawing.Size(75, 23)
        Me.EditStep.TabIndex = 1
        Me.EditStep.Text = "Edit Step"
        Me.EditStep.UseVisualStyleBackColor = True
        '
        'NewStep
        '
        Me.NewStep.Location = New System.Drawing.Point(203, 44)
        Me.NewStep.Name = "NewStep"
        Me.NewStep.Size = New System.Drawing.Size(75, 23)
        Me.NewStep.TabIndex = 0
        Me.NewStep.Text = "New Step"
        Me.NewStep.UseVisualStyleBackColor = True
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.SplitContainer3)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage3.Size = New System.Drawing.Size(522, 282)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Schedules"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.Location = New System.Drawing.Point(3, 3)
        Me.SplitContainer3.Name = "SplitContainer3"
        Me.SplitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.Controls.Add(Me.JobScheduleList)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.PickSchedule)
        Me.SplitContainer3.Size = New System.Drawing.Size(516, 276)
        Me.SplitContainer3.SplitterDistance = 228
        Me.SplitContainer3.TabIndex = 0
        '
        'JobScheduleList
        '
        Me.JobScheduleList.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.ColumnHeader6, Me.ColumnHeader7, Me.ColumnHeader8})
        Me.JobScheduleList.Dock = System.Windows.Forms.DockStyle.Fill
        Me.JobScheduleList.FullRowSelect = True
        Me.JobScheduleList.GridLines = True
        Me.JobScheduleList.Location = New System.Drawing.Point(0, 0)
        Me.JobScheduleList.Name = "JobScheduleList"
        Me.JobScheduleList.Size = New System.Drawing.Size(516, 228)
        Me.JobScheduleList.TabIndex = 0
        Me.JobScheduleList.UseCompatibleStateImageBehavior = False
        Me.JobScheduleList.View = System.Windows.Forms.View.Details
        '
        'ColumnHeader6
        '
        Me.ColumnHeader6.Text = "Schedule Id"
        Me.ColumnHeader6.Width = 73
        '
        'ColumnHeader7
        '
        Me.ColumnHeader7.Text = "Schedule Name"
        Me.ColumnHeader7.Width = 240
        '
        'ColumnHeader8
        '
        Me.ColumnHeader8.Text = "Enabled"
        Me.ColumnHeader8.Width = 71
        '
        'PickSchedule
        '
        Me.PickSchedule.Location = New System.Drawing.Point(204, 10)
        Me.PickSchedule.Name = "PickSchedule"
        Me.PickSchedule.Size = New System.Drawing.Size(99, 23)
        Me.PickSchedule.TabIndex = 1
        Me.PickSchedule.Text = "Pick"
        Me.PickSchedule.UseVisualStyleBackColor = True
        '
        'TabPage4
        '
        Me.TabPage4.Controls.Add(Me.Label10)
        Me.TabPage4.Controls.Add(Me.Action_Delete)
        Me.TabPage4.Controls.Add(Me.Action_EventLog)
        Me.TabPage4.Controls.Add(Me.Action_NetSend)
        Me.TabPage4.Controls.Add(Me.Action_Page)
        Me.TabPage4.Controls.Add(Me.Action_Email)
        Me.TabPage4.Controls.Add(Me.Operator_NetSend)
        Me.TabPage4.Controls.Add(Me.Operator_PAge)
        Me.TabPage4.Controls.Add(Me.Operator_Email)
        Me.TabPage4.Controls.Add(Me.Delete)
        Me.TabPage4.Controls.Add(Me.EventLog)
        Me.TabPage4.Controls.Add(Me.NetSend)
        Me.TabPage4.Controls.Add(Me.PAge)
        Me.TabPage4.Controls.Add(Me.Email)
        Me.TabPage4.Location = New System.Drawing.Point(4, 22)
        Me.TabPage4.Name = "TabPage4"
        Me.TabPage4.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage4.Size = New System.Drawing.Size(522, 282)
        Me.TabPage4.TabIndex = 3
        Me.TabPage4.Text = "Notifications"
        Me.TabPage4.UseVisualStyleBackColor = True
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(18, 240)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(315, 26)
        Me.Label10.TabIndex = 13
        Me.Label10.Text = "WARNING: The settings displayed on this tab may not be " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "implimented on the serve" & _
            "r when using the standalone SQL Agent."
        '
        'Action_Delete
        '
        Me.Action_Delete.AutoCompleteCustomSource.AddRange(New String() {"When the job succeeds", "When the job fails", "When the job completes"})
        Me.Action_Delete.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Action_Delete.Enabled = False
        Me.Action_Delete.FormattingEnabled = True
        Me.Action_Delete.Location = New System.Drawing.Point(368, 123)
        Me.Action_Delete.Name = "Action_Delete"
        Me.Action_Delete.Size = New System.Drawing.Size(121, 21)
        Me.Action_Delete.TabIndex = 12
        '
        'Action_EventLog
        '
        Me.Action_EventLog.AutoCompleteCustomSource.AddRange(New String() {"When the job succeeds", "When the job fails", "When the job completes"})
        Me.Action_EventLog.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Action_EventLog.Enabled = False
        Me.Action_EventLog.FormattingEnabled = True
        Me.Action_EventLog.Location = New System.Drawing.Point(368, 96)
        Me.Action_EventLog.Name = "Action_EventLog"
        Me.Action_EventLog.Size = New System.Drawing.Size(121, 21)
        Me.Action_EventLog.TabIndex = 11
        '
        'Action_NetSend
        '
        Me.Action_NetSend.AutoCompleteCustomSource.AddRange(New String() {"When the job succeeds", "When the job fails", "When the job completes"})
        Me.Action_NetSend.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Action_NetSend.Enabled = False
        Me.Action_NetSend.FormattingEnabled = True
        Me.Action_NetSend.Location = New System.Drawing.Point(368, 69)
        Me.Action_NetSend.Name = "Action_NetSend"
        Me.Action_NetSend.Size = New System.Drawing.Size(121, 21)
        Me.Action_NetSend.TabIndex = 10
        '
        'Action_Page
        '
        Me.Action_Page.AutoCompleteCustomSource.AddRange(New String() {"When the job succeeds", "When the job fails", "When the job completes"})
        Me.Action_Page.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Action_Page.Enabled = False
        Me.Action_Page.FormattingEnabled = True
        Me.Action_Page.Location = New System.Drawing.Point(368, 42)
        Me.Action_Page.Name = "Action_Page"
        Me.Action_Page.Size = New System.Drawing.Size(121, 21)
        Me.Action_Page.TabIndex = 9
        '
        'Action_Email
        '
        Me.Action_Email.AutoCompleteCustomSource.AddRange(New String() {"When the job succeeds", "When the job fails", "When the job completes"})
        Me.Action_Email.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Action_Email.Enabled = False
        Me.Action_Email.FormattingEnabled = True
        Me.Action_Email.Items.AddRange(New Object() {"When the job succeeds", "When the job fails", "When the job completes"})
        Me.Action_Email.Location = New System.Drawing.Point(368, 15)
        Me.Action_Email.Name = "Action_Email"
        Me.Action_Email.Size = New System.Drawing.Size(121, 21)
        Me.Action_Email.TabIndex = 8
        '
        'Operator_NetSend
        '
        Me.Operator_NetSend.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Operator_NetSend.Enabled = False
        Me.Operator_NetSend.FormattingEnabled = True
        Me.Operator_NetSend.Location = New System.Drawing.Point(172, 69)
        Me.Operator_NetSend.Name = "Operator_NetSend"
        Me.Operator_NetSend.Size = New System.Drawing.Size(176, 21)
        Me.Operator_NetSend.TabIndex = 7
        '
        'Operator_PAge
        '
        Me.Operator_PAge.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Operator_PAge.Enabled = False
        Me.Operator_PAge.FormattingEnabled = True
        Me.Operator_PAge.Location = New System.Drawing.Point(172, 42)
        Me.Operator_PAge.Name = "Operator_PAge"
        Me.Operator_PAge.Size = New System.Drawing.Size(176, 21)
        Me.Operator_PAge.TabIndex = 6
        '
        'Operator_Email
        '
        Me.Operator_Email.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.Operator_Email.Enabled = False
        Me.Operator_Email.FormattingEnabled = True
        Me.Operator_Email.Location = New System.Drawing.Point(172, 15)
        Me.Operator_Email.Name = "Operator_Email"
        Me.Operator_Email.Size = New System.Drawing.Size(176, 21)
        Me.Operator_Email.TabIndex = 5
        '
        'Delete
        '
        Me.Delete.AutoSize = True
        Me.Delete.Location = New System.Drawing.Point(21, 125)
        Me.Delete.Name = "Delete"
        Me.Delete.Size = New System.Drawing.Size(140, 17)
        Me.Delete.TabIndex = 4
        Me.Delete.Text = "Automatically delete job:"
        Me.Delete.UseVisualStyleBackColor = True
        '
        'EventLog
        '
        Me.EventLog.AutoSize = True
        Me.EventLog.Location = New System.Drawing.Point(21, 98)
        Me.EventLog.Name = "EventLog"
        Me.EventLog.Size = New System.Drawing.Size(233, 17)
        Me.EventLog.TabIndex = 3
        Me.EventLog.Text = "Write to the Windows Application event log:"
        Me.EventLog.UseVisualStyleBackColor = True
        '
        'NetSend
        '
        Me.NetSend.AutoSize = True
        Me.NetSend.Location = New System.Drawing.Point(21, 71)
        Me.NetSend.Name = "NetSend"
        Me.NetSend.Size = New System.Drawing.Size(74, 17)
        Me.NetSend.TabIndex = 2
        Me.NetSend.Text = "Net Send:"
        Me.NetSend.UseVisualStyleBackColor = True
        '
        'PAge
        '
        Me.PAge.AutoSize = True
        Me.PAge.Location = New System.Drawing.Point(21, 44)
        Me.PAge.Name = "PAge"
        Me.PAge.Size = New System.Drawing.Size(54, 17)
        Me.PAge.TabIndex = 1
        Me.PAge.Text = "Page:"
        Me.PAge.UseVisualStyleBackColor = True
        '
        'Email
        '
        Me.Email.AutoSize = True
        Me.Email.Location = New System.Drawing.Point(21, 17)
        Me.Email.Name = "Email"
        Me.Email.Size = New System.Drawing.Size(57, 17)
        Me.Email.TabIndex = 0
        Me.Email.Text = "E-mail:"
        Me.Email.UseVisualStyleBackColor = True
        '
        'Button6
        '
        Me.Button6.Location = New System.Drawing.Point(327, 18)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(75, 23)
        Me.Button6.TabIndex = 2
        Me.Button6.Text = "Refresh"
        Me.Button6.UseVisualStyleBackColor = True
        '
        'CancelButton
        '
        Me.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.CancelButton.Location = New System.Drawing.Point(222, 18)
        Me.CancelButton.Name = "CancelButton"
        Me.CancelButton.Size = New System.Drawing.Size(75, 23)
        Me.CancelButton.TabIndex = 1
        Me.CancelButton.Text = "Cancel"
        Me.CancelButton.UseVisualStyleBackColor = True
        '
        'SaveButton
        '
        Me.SaveButton.Location = New System.Drawing.Point(119, 18)
        Me.SaveButton.Name = "SaveButton"
        Me.SaveButton.Size = New System.Drawing.Size(75, 23)
        Me.SaveButton.TabIndex = 0
        Me.SaveButton.Text = "Save Job"
        Me.SaveButton.UseVisualStyleBackColor = True
        '
        'ImageList1
        '
        Me.ImageList1.ImageStream = CType(resources.GetObject("ImageList1.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.ImageList1.TransparentColor = System.Drawing.Color.Transparent
        Me.ImageList1.Images.SetKeyName(0, "AudioCDPlus.png")
        '
        'JobEditor
        '
        Me.AcceptButton = Me.SaveButton
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.ClientSize = New System.Drawing.Size(530, 365)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "JobEditor"
        Me.Text = "Job Editor"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.Panel2.PerformLayout()
        Me.SplitContainer2.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        Me.SplitContainer3.ResumeLayout(False)
        Me.TabPage4.ResumeLayout(False)
        Me.TabPage4.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage4 As System.Windows.Forms.TabPage
    Friend WithEvents CancelButton As System.Windows.Forms.Button
    Friend WithEvents SaveButton As System.Windows.Forms.Button
    Friend WithEvents LastModified As System.Windows.Forms.Label
    Friend WithEvents Created As System.Windows.Forms.Label
    Friend WithEvents Description As System.Windows.Forms.TextBox
    Friend WithEvents Category As System.Windows.Forms.ComboBox
    Friend WithEvents OwnerUserName As System.Windows.Forms.TextBox
    Friend WithEvents JobName As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents JobEnabled As System.Windows.Forms.CheckBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents LastExecuted As System.Windows.Forms.Label
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents DeleteStep As System.Windows.Forms.Button
    Friend WithEvents EditStep As System.Windows.Forms.Button
    Friend WithEvents NewStep As System.Windows.Forms.Button
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents StartStep As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents JobStepList As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader2 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader4 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader5 As System.Windows.Forms.ColumnHeader
    Friend WithEvents Button6 As System.Windows.Forms.Button
    Friend WithEvents SplitContainer3 As System.Windows.Forms.SplitContainer
    Friend WithEvents JobScheduleList As System.Windows.Forms.ListView
    Friend WithEvents ColumnHeader6 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader7 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader8 As System.Windows.Forms.ColumnHeader
    Friend WithEvents PickSchedule As System.Windows.Forms.Button
    Friend WithEvents PAge As System.Windows.Forms.CheckBox
    Friend WithEvents Email As System.Windows.Forms.CheckBox
    Friend WithEvents NetSend As System.Windows.Forms.CheckBox
    Friend WithEvents Action_Delete As System.Windows.Forms.ComboBox
    Friend WithEvents Action_EventLog As System.Windows.Forms.ComboBox
    Friend WithEvents Action_NetSend As System.Windows.Forms.ComboBox
    Friend WithEvents Action_Page As System.Windows.Forms.ComboBox
    Friend WithEvents Action_Email As System.Windows.Forms.ComboBox
    Friend WithEvents Operator_NetSend As System.Windows.Forms.ComboBox
    Friend WithEvents Operator_PAge As System.Windows.Forms.ComboBox
    Friend WithEvents Operator_Email As System.Windows.Forms.ComboBox
    Friend WithEvents Delete As System.Windows.Forms.CheckBox
    Friend WithEvents EventLog As System.Windows.Forms.CheckBox
    Friend WithEvents ImageList1 As System.Windows.Forms.ImageList
    Friend WithEvents Label10 As System.Windows.Forms.Label
End Class
