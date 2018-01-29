Imports System.Data.SQLite
Public Class FrmShiftMaster
    Inherits AgTemplate.TempMaster
    Dim mQry$

#Region "Designer Code"
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label
        Me.TxtDescription = New AgControls.AgTextBox
        Me.LblDescription = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.TxtFromTime = New AgControls.AgTextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.TxtToTime = New AgControls.AgTextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.TxtLunchToTime = New AgControls.AgTextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.TxtLunchFromTime = New AgControls.AgTextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.GrpUP.SuspendLayout()
        Me.GBoxEntryType.SuspendLayout()
        Me.GBoxMoveToLog.SuspendLayout()
        Me.GBoxApprove.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GBoxDivision.SuspendLayout()
        CType(Me.DTMaster, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Topctrl1
        '
        Me.Topctrl1.Size = New System.Drawing.Size(862, 41)
        Me.Topctrl1.tAdd = False
        Me.Topctrl1.tDel = False
        Me.Topctrl1.tEdit = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Location = New System.Drawing.Point(0, 224)
        Me.GroupBox1.Size = New System.Drawing.Size(904, 4)
        '
        'GrpUP
        '
        Me.GrpUP.Location = New System.Drawing.Point(14, 228)
        '
        'TxtEntryBy
        '
        Me.TxtEntryBy.Tag = ""
        Me.TxtEntryBy.Text = ""
        '
        'GBoxEntryType
        '
        Me.GBoxEntryType.Location = New System.Drawing.Point(148, 228)
        '
        'TxtEntryType
        '
        Me.TxtEntryType.Tag = ""
        '
        'GBoxMoveToLog
        '
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(554, 228)
        '
        'TxtMoveToLog
        '
        Me.TxtMoveToLog.Tag = ""
        '
        'GBoxApprove
        '
        Me.GBoxApprove.Location = New System.Drawing.Point(401, 228)
        Me.GBoxApprove.Text = "Approved By"
        '
        'TxtApproveBy
        '
        Me.TxtApproveBy.Location = New System.Drawing.Point(3, 23)
        Me.TxtApproveBy.Size = New System.Drawing.Size(136, 18)
        Me.TxtApproveBy.Tag = ""
        '
        'GroupBox2
        '
        Me.GroupBox2.Location = New System.Drawing.Point(704, 228)
        '
        'GBoxDivision
        '
        Me.GBoxDivision.Location = New System.Drawing.Point(278, 228)
        '
        'TxtDivision
        '
        Me.TxtDivision.AgSelectedValue = ""
        Me.TxtDivision.Tag = ""
        '
        'TxtStatus
        '
        Me.TxtStatus.AgSelectedValue = ""
        Me.TxtStatus.Tag = ""
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(318, 114)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(10, 7)
        Me.Label1.TabIndex = 666
        Me.Label1.Text = "Ä"
        '
        'TxtDescription
        '
        Me.TxtDescription.AgAllowUserToEnableMasterHelp = False
        Me.TxtDescription.AgLastValueTag = Nothing
        Me.TxtDescription.AgLastValueText = Nothing
        Me.TxtDescription.AgMandatory = True
        Me.TxtDescription.AgMasterHelp = True
        Me.TxtDescription.AgNumberLeftPlaces = 0
        Me.TxtDescription.AgNumberNegetiveAllow = False
        Me.TxtDescription.AgNumberRightPlaces = 0
        Me.TxtDescription.AgPickFromLastValue = False
        Me.TxtDescription.AgRowFilter = ""
        Me.TxtDescription.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtDescription.AgSelectedValue = Nothing
        Me.TxtDescription.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtDescription.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtDescription.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtDescription.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDescription.Location = New System.Drawing.Point(338, 106)
        Me.TxtDescription.MaxLength = 50
        Me.TxtDescription.Name = "TxtDescription"
        Me.TxtDescription.Size = New System.Drawing.Size(322, 18)
        Me.TxtDescription.TabIndex = 1
        '
        'LblDescription
        '
        Me.LblDescription.AutoSize = True
        Me.LblDescription.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDescription.Location = New System.Drawing.Point(203, 107)
        Me.LblDescription.Name = "LblDescription"
        Me.LblDescription.Size = New System.Drawing.Size(73, 16)
        Me.LblDescription.TabIndex = 661
        Me.LblDescription.Text = "Description"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(318, 135)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(10, 7)
        Me.Label2.TabIndex = 674
        Me.Label2.Text = "Ä"
        '
        'TxtFromTime
        '
        Me.TxtFromTime.AgAllowUserToEnableMasterHelp = False
        Me.TxtFromTime.AgLastValueTag = Nothing
        Me.TxtFromTime.AgLastValueText = Nothing
        Me.TxtFromTime.AgMandatory = True
        Me.TxtFromTime.AgMasterHelp = True
        Me.TxtFromTime.AgNumberLeftPlaces = 2
        Me.TxtFromTime.AgNumberNegetiveAllow = False
        Me.TxtFromTime.AgNumberRightPlaces = 2
        Me.TxtFromTime.AgPickFromLastValue = False
        Me.TxtFromTime.AgRowFilter = ""
        Me.TxtFromTime.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtFromTime.AgSelectedValue = Nothing
        Me.TxtFromTime.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtFromTime.AgValueType = AgControls.AgTextBox.TxtValueType.Number_Value
        Me.TxtFromTime.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtFromTime.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFromTime.Location = New System.Drawing.Point(338, 127)
        Me.TxtFromTime.MaxLength = 5
        Me.TxtFromTime.Name = "TxtFromTime"
        Me.TxtFromTime.Size = New System.Drawing.Size(100, 18)
        Me.TxtFromTime.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(203, 127)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(70, 16)
        Me.Label3.TabIndex = 673
        Me.Label3.Text = "From Time"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(541, 132)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(10, 7)
        Me.Label4.TabIndex = 677
        Me.Label4.Text = "Ä"
        '
        'TxtToTime
        '
        Me.TxtToTime.AgAllowUserToEnableMasterHelp = False
        Me.TxtToTime.AgLastValueTag = Nothing
        Me.TxtToTime.AgLastValueText = Nothing
        Me.TxtToTime.AgMandatory = True
        Me.TxtToTime.AgMasterHelp = True
        Me.TxtToTime.AgNumberLeftPlaces = 2
        Me.TxtToTime.AgNumberNegetiveAllow = False
        Me.TxtToTime.AgNumberRightPlaces = 2
        Me.TxtToTime.AgPickFromLastValue = False
        Me.TxtToTime.AgRowFilter = ""
        Me.TxtToTime.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtToTime.AgSelectedValue = Nothing
        Me.TxtToTime.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtToTime.AgValueType = AgControls.AgTextBox.TxtValueType.Number_Value
        Me.TxtToTime.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtToTime.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtToTime.Location = New System.Drawing.Point(560, 127)
        Me.TxtToTime.MaxLength = 5
        Me.TxtToTime.Name = "TxtToTime"
        Me.TxtToTime.Size = New System.Drawing.Size(100, 18)
        Me.TxtToTime.TabIndex = 3
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(448, 127)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(53, 16)
        Me.Label5.TabIndex = 676
        Me.Label5.Text = "To Time"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label6.Location = New System.Drawing.Point(541, 152)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(10, 7)
        Me.Label6.TabIndex = 683
        Me.Label6.Text = "Ä"
        '
        'TxtLunchToTime
        '
        Me.TxtLunchToTime.AgAllowUserToEnableMasterHelp = False
        Me.TxtLunchToTime.AgLastValueTag = Nothing
        Me.TxtLunchToTime.AgLastValueText = Nothing
        Me.TxtLunchToTime.AgMandatory = True
        Me.TxtLunchToTime.AgMasterHelp = True
        Me.TxtLunchToTime.AgNumberLeftPlaces = 2
        Me.TxtLunchToTime.AgNumberNegetiveAllow = False
        Me.TxtLunchToTime.AgNumberRightPlaces = 2
        Me.TxtLunchToTime.AgPickFromLastValue = False
        Me.TxtLunchToTime.AgRowFilter = ""
        Me.TxtLunchToTime.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtLunchToTime.AgSelectedValue = Nothing
        Me.TxtLunchToTime.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtLunchToTime.AgValueType = AgControls.AgTextBox.TxtValueType.Number_Value
        Me.TxtLunchToTime.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtLunchToTime.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtLunchToTime.Location = New System.Drawing.Point(560, 148)
        Me.TxtLunchToTime.MaxLength = 5
        Me.TxtLunchToTime.Name = "TxtLunchToTime"
        Me.TxtLunchToTime.Size = New System.Drawing.Size(100, 18)
        Me.TxtLunchToTime.TabIndex = 5
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(448, 149)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(92, 16)
        Me.Label7.TabIndex = 682
        Me.Label7.Text = "Lunch To Time"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label8.Location = New System.Drawing.Point(318, 156)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(10, 7)
        Me.Label8.TabIndex = 680
        Me.Label8.Text = "Ä"
        '
        'TxtLunchFromTime
        '
        Me.TxtLunchFromTime.AgAllowUserToEnableMasterHelp = False
        Me.TxtLunchFromTime.AgLastValueTag = Nothing
        Me.TxtLunchFromTime.AgLastValueText = Nothing
        Me.TxtLunchFromTime.AgMandatory = True
        Me.TxtLunchFromTime.AgMasterHelp = True
        Me.TxtLunchFromTime.AgNumberLeftPlaces = 2
        Me.TxtLunchFromTime.AgNumberNegetiveAllow = False
        Me.TxtLunchFromTime.AgNumberRightPlaces = 2
        Me.TxtLunchFromTime.AgPickFromLastValue = False
        Me.TxtLunchFromTime.AgRowFilter = ""
        Me.TxtLunchFromTime.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtLunchFromTime.AgSelectedValue = Nothing
        Me.TxtLunchFromTime.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtLunchFromTime.AgValueType = AgControls.AgTextBox.TxtValueType.Number_Value
        Me.TxtLunchFromTime.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtLunchFromTime.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtLunchFromTime.Location = New System.Drawing.Point(338, 148)
        Me.TxtLunchFromTime.MaxLength = 5
        Me.TxtLunchFromTime.Name = "TxtLunchFromTime"
        Me.TxtLunchFromTime.Size = New System.Drawing.Size(100, 18)
        Me.TxtLunchFromTime.TabIndex = 4
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(203, 149)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(109, 16)
        Me.Label9.TabIndex = 679
        Me.Label9.Text = "Lunch From Time"
        '
        'FrmShiftMaster
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(862, 272)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.TxtLunchToTime)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.TxtLunchFromTime)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TxtToTime)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TxtFromTime)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TxtDescription)
        Me.Controls.Add(Me.LblDescription)
        Me.Name = "FrmShiftMaster"
        Me.Text = "Shift Master"
        Me.Controls.SetChildIndex(Me.GBoxDivision, 0)
        Me.Controls.SetChildIndex(Me.GroupBox2, 0)
        Me.Controls.SetChildIndex(Me.Topctrl1, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.GrpUP, 0)
        Me.Controls.SetChildIndex(Me.GBoxEntryType, 0)
        Me.Controls.SetChildIndex(Me.GBoxApprove, 0)
        Me.Controls.SetChildIndex(Me.GBoxMoveToLog, 0)
        Me.Controls.SetChildIndex(Me.LblDescription, 0)
        Me.Controls.SetChildIndex(Me.TxtDescription, 0)
        Me.Controls.SetChildIndex(Me.Label1, 0)
        Me.Controls.SetChildIndex(Me.Label3, 0)
        Me.Controls.SetChildIndex(Me.TxtFromTime, 0)
        Me.Controls.SetChildIndex(Me.Label2, 0)
        Me.Controls.SetChildIndex(Me.Label5, 0)
        Me.Controls.SetChildIndex(Me.TxtToTime, 0)
        Me.Controls.SetChildIndex(Me.Label4, 0)
        Me.Controls.SetChildIndex(Me.Label9, 0)
        Me.Controls.SetChildIndex(Me.TxtLunchFromTime, 0)
        Me.Controls.SetChildIndex(Me.Label8, 0)
        Me.Controls.SetChildIndex(Me.Label7, 0)
        Me.Controls.SetChildIndex(Me.TxtLunchToTime, 0)
        Me.Controls.SetChildIndex(Me.Label6, 0)
        Me.GrpUP.ResumeLayout(False)
        Me.GrpUP.PerformLayout()
        Me.GBoxEntryType.ResumeLayout(False)
        Me.GBoxEntryType.PerformLayout()
        Me.GBoxMoveToLog.ResumeLayout(False)
        Me.GBoxMoveToLog.PerformLayout()
        Me.GBoxApprove.ResumeLayout(False)
        Me.GBoxApprove.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GBoxDivision.ResumeLayout(False)
        Me.GBoxDivision.PerformLayout()
        CType(Me.DTMaster, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Public WithEvents LblDescription As System.Windows.Forms.Label
    Public WithEvents TxtDescription As AgControls.AgTextBox
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents TxtFromTime As AgControls.AgTextBox
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents TxtToTime As AgControls.AgTextBox
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents TxtLunchToTime As AgControls.AgTextBox
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents TxtLunchFromTime As AgControls.AgTextBox
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
#End Region

    Private Sub FrmShift_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        If AgL.RequiredField(TxtDescription, LblDescription.Text) Then passed = False : Exit Sub

        If Val(TxtFromTime.Text) >= 24 Then Err.Raise(1, , "Invalid From Time !") : passed = False : Exit Sub
        If Val(TxtToTime.Text) >= 24 Then Err.Raise(1, , "Invalid To Time !") : passed = False : Exit Sub
        If Val(TxtLunchFromTime.Text) >= 24 Then Err.Raise(1, , "Invalid Lunch From Time !") : passed = False : Exit Sub
        If Val(TxtLunchToTime.Text) >= 24 Then Err.Raise(1, , "Invalid Lunch To Time !") : passed = False : Exit Sub

        'If Val(TxtFromTime.Text) > Val(TxtToTime.Text) Then Err.Raise(1, , "From Time is Greater than To Time !") : passed = False : Exit Sub
        If Val(TxtLunchFromTime.Text) > Val(TxtLunchToTime.Text) Then Err.Raise(1, , "Lunch From Time is Greater than Lunch To Time !") : passed = False : Exit Sub
        'If Val(TxtLunchFromTime.Text) < Val(TxtFromTime.Text) Or Val(TxtLunchFromTime.Text) > Val(TxtToTime.Text) Then Err.Raise(1, , "Lunch From Time should be Between From Time To & To Time !") : passed = False : Exit Sub
        'If Val(TxtLunchToTime.Text) < Val(TxtFromTime.Text) Or Val(TxtLunchToTime.Text) > Val(TxtToTime.Text) Then Err.Raise(1, , "Lunch To Time should be Between From Time To & To Time !") : passed = False : Exit Sub

        If Topctrl1.Mode = "Add" Then
            mQry = "Select count(*) From Shift Where Description='" & TxtDescription.Text & "' "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Description Already Exist!")
        Else
            mQry = "Select count(*) From Shift Where Description='" & TxtDescription.Text & "' And Code <> '" & mInternalCode & "' "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Description Already Exist!")
        End If
    End Sub

    Public Overridable Sub FrmShift_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mConStr$ = " Where 1=1  "
        AgL.PubFindQry = "SELECT I.Code As SearchCode, I.Description, I.FromTime, I.ToTime, I.LunchFromTime, I.LunchToTime From Shift I "
        AgL.PubFindQryOrdBy = "[Description]"
    End Sub

    Private Sub FrmShift_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "Shift"
        LogTableName = "Shift_Log"
    End Sub

    Private Sub FrmShift_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As SqliteConnection, ByVal Cmd As SqliteCommand) Handles Me.BaseEvent_Save_InTrans
        mQry = "UPDATE Shift " &
                " SET " &
                " Description = " & AgL.Chk_Text(TxtDescription.Text) & ", " &
                " FromTime = " & Val(TxtFromTime.Text) & ", " &
                " ToTime = " & Val(TxtToTime.Text) & ", " &
                " LunchFromTime = " & Val(TxtLunchFromTime.Text) & ", " &
                " LunchToTime = " & Val(TxtLunchToTime.Text) & " " &
                " Where Code = '" & SearchCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
    End Sub

    Private Sub FrmQuality1_BaseFunction_FIniList() Handles Me.BaseFunction_FIniList
        mQry = "Select Code, Description As Name " &
                " From Shift " &
                " Order By Description "
        TxtDescription.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)
    End Sub

    Private Sub FrmQuality1_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim DsTemp As DataSet

        mQry = "Select H.* " &
                " From Shift H " &
                " Where H.Code='" & SearchCode & "'"
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                mInternalCode = AgL.XNull(.Rows(0)("Code"))
                TxtDescription.Text = AgL.XNull(.Rows(0)("Description"))
                TxtFromTime.Text = Format(AgL.VNull(.Rows(0)("FromTime")), "0.00")
                TxtToTime.Text = Format(AgL.VNull(.Rows(0)("ToTime")), "0.00")
                TxtLunchFromTime.Text = Format(AgL.VNull(.Rows(0)("LunchFromTime")), "0.00")
                TxtLunchToTime.Text = Format(AgL.VNull(.Rows(0)("LunchToTime")), "0.00")
            End If
        End With
    End Sub

    Private Sub Topctrl1_tbAdd() Handles Topctrl1.tbAdd
        TxtDescription.Focus()
    End Sub

    Private Sub Topctrl1_tbEdit() Handles Topctrl1.tbEdit
        TxtDescription.Focus()
    End Sub

    Private Sub TxtDescription_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            If MsgBox("Do you want to save?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "Save") = MsgBoxResult.Yes Then
                Topctrl1.FButtonClick(13)
            End If
        End If
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Public Sub New(ByVal StrUPVar As String, ByVal DTUP As DataTable)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Topctrl1.FSetParent(Me, StrUPVar, DTUP)
        Topctrl1.SetDisp(True)

    End Sub

    Private Sub FrmShift_BaseFunction_FIniMast(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMast
        Dim mConStr$ = ""
        mQry = "Select I.Code As SearchCode " &
                " From Shift I " & mConStr &
                " Order By I.Description "
        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub

    Private Sub FrmShift_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AgL.WinSetting(Me, 300, 885)
    End Sub

    Private Function FGetRelationalData() As Boolean
        Try
            mQry = " Select Count(*) From SubGroup Where Shift = '" & mSearchCode & "'"
            If AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar) > 0 Then
                MsgBox(" Data Exists For Shift " & TxtDescription.Text & " In Employee Master . Can't Delete Entry", MsgBoxStyle.Information)
                FGetRelationalData = True
                Exit Function
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " in FGetRelationalData")
            FGetRelationalData = True
        End Try
    End Function

    Private Sub ME_BaseEvent_Topctrl_tbDel(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbDel
        Passed = Not FGetRelationalData()
    End Sub
End Class
