Imports System.Data.SQLite
Public Class FrmTermCondition
    Inherits AgTemplate.TempMaster
    Friend WithEvents Label26 As System.Windows.Forms.Label
    Friend WithEvents TxtSiteCode As AgControls.AgTextBox
    Friend WithEvents Label27 As System.Windows.Forms.Label
    Friend WithEvents LblEntryTypeReq As System.Windows.Forms.Label
    Friend WithEvents TxtVoucherType As AgControls.AgTextBox
    Friend WithEvents LblEntryType As System.Windows.Forms.Label
    Protected WithEvents BtnCopyToAllDiv As System.Windows.Forms.Button
    Protected WithEvents BtnCopyToAllSite As System.Windows.Forms.Button
    Friend WithEvents TxtTermCondition As AgControls.AgTextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Dim mQry$

    Public Sub New(ByVal StrUPVar As String, ByVal DTUP As DataTable)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Topctrl1.FSetParent(Me, StrUPVar, DTUP)
        Topctrl1.SetDisp(True)
    End Sub

    Private Sub FrmVoucher_Type_Print_SettingsMaster_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        If AgL.RequiredField(TxtVoucherType, LblEntryType.Text) Then passed = False : Exit Sub

        If Topctrl1.Mode = "Add" Then
            mQry = "Select count(*) From Voucher_Type_Settings Where V_Type='" & TxtVoucherType.AgSelectedValue & "' And Div_Code = '" & AgL.PubDivCode & "' And Site_Code ='" & AgL.PubSiteCode & "' "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then passed = False : MsgBox("Entry Type Already Exists")
        Else
            mQry = "Select count(*) From Voucher_Type_Settings Where V_Type='" & TxtVoucherType.AgSelectedValue & "' And Div_Code = '" & AgL.PubDivCode & "' And Site_Code ='" & AgL.PubSiteCode & "' And Code <> '" & mInternalCode & "' "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then passed = False : MsgBox("Entry Type Already Exists")
        End If
    End Sub

    Private Sub FrmVoucher_Type_Print_SettingsMaster_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mConStr$ = ""
        mConStr = " WHERE 1=1 AND H.Div_Code = '" & AgL.PubDivCode & "' And H.Site_Code = '" & AgL.PubSiteCode & "' AND IfNull(H.IsDeleted,0) = 0"
        AgL.PubFindQry = " SELECT H.Code, H.V_Type, Vt.Description AS [V Type], SM.Name AS SiteName, D.Div_Name, H.TermsCondition AS [Terms & Condition] " &
                        " FROM Voucher_Type_Settings H  " &
                        " LEFT JOIN Voucher_Type Vt ON Vt.V_Type = H.V_Type  " &
                        " LEFT JOIN SiteMast SM ON SM.Code = H.Site_Code  " &
                        " LEFT JOIN Division D ON D.Div_Code = H.Div_Code " &
                        " " & mConStr & " "
        AgL.PubFindQryOrdBy = "[V Type]"
    End Sub

    Private Sub FrmVoucher_Type_Print_SettingsMaster_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "Voucher_Type_Settings"
        LogTableName = "Voucher_Type_Settings_Log"
    End Sub

    Private Sub FrmVoucher_Type_Print_SettingsMaster_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As SQLiteConnection, ByVal Cmd As SQLiteCommand) Handles Me.BaseEvent_Save_InTrans
        mQry = " UPDATE Voucher_Type_Settings " &
                " SET V_Type = " & AgL.Chk_Text(TxtVoucherType.Tag) & " , " &
                " EntryBy = '" & AgL.PubUserName & "', " &
                " EntryDate = '" & AgL.PubLoginDate & "', " &
                " ApproveBy =  '" & AgL.PubUserName & "', " &
                " ApproveDate = '" & AgL.PubLoginDate & "', " &
                " Site_Code = '" & TxtSiteCode.Tag & "', " &
                " Div_Code = '" & TxtDivision.Tag & "', " &
                " TermsCondition = '" & TxtTermCondition.Text & "' " &
                " Where Code = '" & SearchCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
    End Sub

    Private Sub FrmVoucher_Type_Print_SettingsMaster_BaseFunction_DispText() Handles Me.BaseFunction_DispText
        If AgL.StrCmp(Topctrl1.Mode, "Edit") Then
            TxtVoucherType.Enabled = False
        End If
        TxtSiteCode.Enabled = False
    End Sub

    Private Sub FrmVoucher_Type_Print_SettingsMaster_BaseFunction_FIniMast(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMast
        Dim mConStr$ = ""
        mConStr = "WHERE 1=1 And Div_Code = '" & AgL.PubDivCode & "' And Site_Code = '" & AgL.PubSiteCode & "'  AND IfNull(IsDeleted,0) = 0 "
        mQry = "Select Code As SearchCode " &
            " From Voucher_Type_Settings " & mConStr &
            " Order By V_Type "
        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub FrmVoucher_Type_Print_SettingsMaster_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim DsTemp As DataSet
        mQry = " SELECT H.* , Vt.Description AS VtDesc, SM.Name AS SiteName  " &
            " FROM Voucher_Type_Settings H " &
            " LEFT JOIN Voucher_Type Vt ON Vt.V_Type = H.V_Type  " &
            " LEFT JOIN SiteMast SM ON SM.Code = H.Site_Code " &
            " Where H.Code='" & SearchCode & "'"
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                mInternalCode = AgL.XNull(.Rows(0)("Code"))
                TxtVoucherType.Tag = AgL.XNull(.Rows(0)("V_Type"))
                TxtVoucherType.Text = AgL.XNull(.Rows(0)("VtDesc"))
                TxtSiteCode.Tag = AgL.XNull(.Rows(0)("Site_Code"))
                TxtSiteCode.Text = AgL.XNull(.Rows(0)("SiteName"))
                TxtTermCondition.Text = AgL.XNull(.Rows(0)("TermsCondition"))
            End If
        End With
        Topctrl1.tPrn = False
    End Sub

    Private Sub FrmVoucher_Type_Print_SettingsMaster_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        AgL.WinSetting(Me, 550, 875, 0, 0)
    End Sub

    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub

    Private Sub Topctrl1_tbAdd() Handles Topctrl1.tbAdd
        TxtVoucherType.Focus()
        TxtSiteCode.Tag = AgL.PubSiteCode
        TxtSiteCode.Text = AgL.Dman_Execute("SELECT Name FROM SiteMast Where Code = '" & TxtSiteCode.Tag & "'", AgL.GcnRead).ExecuteScalar
    End Sub

    Private Sub Topctrl1_tbEdit() Handles Topctrl1.tbEdit
        TxtTermCondition.Focus()
    End Sub

    Private Sub InitializeComponent()
        Me.Label26 = New System.Windows.Forms.Label
        Me.TxtSiteCode = New AgControls.AgTextBox
        Me.Label27 = New System.Windows.Forms.Label
        Me.LblEntryTypeReq = New System.Windows.Forms.Label
        Me.TxtVoucherType = New AgControls.AgTextBox
        Me.LblEntryType = New System.Windows.Forms.Label
        Me.BtnCopyToAllDiv = New System.Windows.Forms.Button
        Me.BtnCopyToAllSite = New System.Windows.Forms.Button
        Me.TxtTermCondition = New AgControls.AgTextBox
        Me.Label1 = New System.Windows.Forms.Label
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
        Me.Topctrl1.Size = New System.Drawing.Size(869, 41)
        '
        'GroupBox1
        '
        Me.GroupBox1.Location = New System.Drawing.Point(0, 463)
        Me.GroupBox1.Size = New System.Drawing.Size(871, 10)
        '
        'GrpUP
        '
        Me.GrpUP.Location = New System.Drawing.Point(29, 473)
        '
        'TxtEntryBy
        '
        Me.TxtEntryBy.Tag = ""
        Me.TxtEntryBy.Text = ""
        '
        'GBoxEntryType
        '
        Me.GBoxEntryType.Location = New System.Drawing.Point(259, 473)
        Me.GBoxEntryType.Size = New System.Drawing.Size(121, 44)
        '
        'TxtEntryType
        '
        Me.TxtEntryType.Size = New System.Drawing.Size(115, 18)
        Me.TxtEntryType.Tag = ""
        '
        'GBoxMoveToLog
        '
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(580, 478)
        Me.GBoxMoveToLog.Size = New System.Drawing.Size(121, 44)
        '
        'TxtMoveToLog
        '
        Me.TxtMoveToLog.Size = New System.Drawing.Size(89, 18)
        Me.TxtMoveToLog.Tag = ""
        '
        'GBoxApprove
        '
        Me.GBoxApprove.Location = New System.Drawing.Point(436, 478)
        Me.GBoxApprove.Size = New System.Drawing.Size(121, 44)
        Me.GBoxApprove.Text = "Approved By"
        '
        'TxtApproveBy
        '
        Me.TxtApproveBy.Location = New System.Drawing.Point(3, 23)
        Me.TxtApproveBy.Size = New System.Drawing.Size(115, 18)
        Me.TxtApproveBy.Tag = ""
        '
        'CmdDiscard
        '
        Me.CmdDiscard.Location = New System.Drawing.Point(92, 18)
        '
        'GroupBox2
        '
        Me.GroupBox2.Location = New System.Drawing.Point(719, 473)
        Me.GroupBox2.Size = New System.Drawing.Size(121, 44)
        '
        'GBoxDivision
        '
        Me.GBoxDivision.Location = New System.Drawing.Point(489, 473)
        Me.GBoxDivision.Size = New System.Drawing.Size(121, 44)
        '
        'TxtDivision
        '
        Me.TxtDivision.AgSelectedValue = ""
        Me.TxtDivision.Size = New System.Drawing.Size(115, 18)
        Me.TxtDivision.Tag = ""
        '
        'TxtStatus
        '
        Me.TxtStatus.AgSelectedValue = ""
        Me.TxtStatus.Size = New System.Drawing.Size(89, 18)
        Me.TxtStatus.Tag = ""
        '
        'Label26
        '
        Me.Label26.AutoSize = True
        Me.Label26.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label26.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label26.Location = New System.Drawing.Point(186, 93)
        Me.Label26.Name = "Label26"
        Me.Label26.Size = New System.Drawing.Size(10, 7)
        Me.Label26.TabIndex = 730
        Me.Label26.Text = "Ä"
        '
        'TxtSiteCode
        '
        Me.TxtSiteCode.AgAllowUserToEnableMasterHelp = False
        Me.TxtSiteCode.AgLastValueTag = Nothing
        Me.TxtSiteCode.AgLastValueText = Nothing
        Me.TxtSiteCode.AgMandatory = True
        Me.TxtSiteCode.AgMasterHelp = False
        Me.TxtSiteCode.AgNumberLeftPlaces = 0
        Me.TxtSiteCode.AgNumberNegetiveAllow = False
        Me.TxtSiteCode.AgNumberRightPlaces = 0
        Me.TxtSiteCode.AgPickFromLastValue = False
        Me.TxtSiteCode.AgRowFilter = ""
        Me.TxtSiteCode.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtSiteCode.AgSelectedValue = Nothing
        Me.TxtSiteCode.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtSiteCode.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtSiteCode.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtSiteCode.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtSiteCode.Location = New System.Drawing.Point(199, 88)
        Me.TxtSiteCode.MaxLength = 0
        Me.TxtSiteCode.Name = "TxtSiteCode"
        Me.TxtSiteCode.Size = New System.Drawing.Size(288, 18)
        Me.TxtSiteCode.TabIndex = 2
        Me.TxtSiteCode.Text = "TxtSiteCode"
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.Location = New System.Drawing.Point(32, 88)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(76, 16)
        Me.Label27.TabIndex = 729
        Me.Label27.Text = "Site/Branch"
        '
        'LblEntryTypeReq
        '
        Me.LblEntryTypeReq.AutoSize = True
        Me.LblEntryTypeReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblEntryTypeReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblEntryTypeReq.Location = New System.Drawing.Point(186, 74)
        Me.LblEntryTypeReq.Name = "LblEntryTypeReq"
        Me.LblEntryTypeReq.Size = New System.Drawing.Size(10, 7)
        Me.LblEntryTypeReq.TabIndex = 788
        Me.LblEntryTypeReq.Text = "Ä"
        '
        'TxtVoucherType
        '
        Me.TxtVoucherType.AgAllowUserToEnableMasterHelp = False
        Me.TxtVoucherType.AgLastValueTag = Nothing
        Me.TxtVoucherType.AgLastValueText = Nothing
        Me.TxtVoucherType.AgMandatory = True
        Me.TxtVoucherType.AgMasterHelp = False
        Me.TxtVoucherType.AgNumberLeftPlaces = 0
        Me.TxtVoucherType.AgNumberNegetiveAllow = False
        Me.TxtVoucherType.AgNumberRightPlaces = 0
        Me.TxtVoucherType.AgPickFromLastValue = False
        Me.TxtVoucherType.AgRowFilter = ""
        Me.TxtVoucherType.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtVoucherType.AgSelectedValue = Nothing
        Me.TxtVoucherType.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtVoucherType.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtVoucherType.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtVoucherType.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtVoucherType.Location = New System.Drawing.Point(199, 69)
        Me.TxtVoucherType.MaxLength = 0
        Me.TxtVoucherType.Name = "TxtVoucherType"
        Me.TxtVoucherType.Size = New System.Drawing.Size(288, 18)
        Me.TxtVoucherType.TabIndex = 1
        Me.TxtVoucherType.Text = "TxtVoucherType"
        '
        'LblEntryType
        '
        Me.LblEntryType.AutoSize = True
        Me.LblEntryType.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblEntryType.Location = New System.Drawing.Point(32, 69)
        Me.LblEntryType.Name = "LblEntryType"
        Me.LblEntryType.Size = New System.Drawing.Size(70, 16)
        Me.LblEntryType.TabIndex = 787
        Me.LblEntryType.Text = "Entry Type"
        '
        'BtnCopyToAllDiv
        '
        Me.BtnCopyToAllDiv.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnCopyToAllDiv.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnCopyToAllDiv.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnCopyToAllDiv.Location = New System.Drawing.Point(633, 77)
        Me.BtnCopyToAllDiv.Margin = New System.Windows.Forms.Padding(0)
        Me.BtnCopyToAllDiv.Name = "BtnCopyToAllDiv"
        Me.BtnCopyToAllDiv.Size = New System.Drawing.Size(148, 25)
        Me.BtnCopyToAllDiv.TabIndex = 789
        Me.BtnCopyToAllDiv.TabStop = False
        Me.BtnCopyToAllDiv.Text = "Copy To All Division"
        Me.BtnCopyToAllDiv.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnCopyToAllDiv.UseVisualStyleBackColor = True
        '
        'BtnCopyToAllSite
        '
        Me.BtnCopyToAllSite.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnCopyToAllSite.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnCopyToAllSite.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnCopyToAllSite.Location = New System.Drawing.Point(633, 51)
        Me.BtnCopyToAllSite.Margin = New System.Windows.Forms.Padding(0)
        Me.BtnCopyToAllSite.Name = "BtnCopyToAllSite"
        Me.BtnCopyToAllSite.Size = New System.Drawing.Size(148, 25)
        Me.BtnCopyToAllSite.TabIndex = 790
        Me.BtnCopyToAllSite.TabStop = False
        Me.BtnCopyToAllSite.Text = "Copy To All Site"
        Me.BtnCopyToAllSite.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnCopyToAllSite.UseVisualStyleBackColor = True
        '
        'TxtTermCondition
        '
        Me.TxtTermCondition.AgAllowUserToEnableMasterHelp = False
        Me.TxtTermCondition.AgLastValueTag = Nothing
        Me.TxtTermCondition.AgLastValueText = Nothing
        Me.TxtTermCondition.AgMandatory = False
        Me.TxtTermCondition.AgMasterHelp = False
        Me.TxtTermCondition.AgNumberLeftPlaces = 0
        Me.TxtTermCondition.AgNumberNegetiveAllow = False
        Me.TxtTermCondition.AgNumberRightPlaces = 0
        Me.TxtTermCondition.AgPickFromLastValue = False
        Me.TxtTermCondition.AgRowFilter = ""
        Me.TxtTermCondition.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtTermCondition.AgSelectedValue = Nothing
        Me.TxtTermCondition.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtTermCondition.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtTermCondition.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtTermCondition.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtTermCondition.Location = New System.Drawing.Point(199, 107)
        Me.TxtTermCondition.MaxLength = 0
        Me.TxtTermCondition.Multiline = True
        Me.TxtTermCondition.Name = "TxtTermCondition"
        Me.TxtTermCondition.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.TxtTermCondition.Size = New System.Drawing.Size(603, 347)
        Me.TxtTermCondition.TabIndex = 3
        Me.TxtTermCondition.Text = "TxtTerm&Condition"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(32, 107)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(107, 16)
        Me.Label1.TabIndex = 791
        Me.Label1.Text = "Term && Condition"
        '
        'FrmTermCondition
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(869, 522)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.BtnCopyToAllSite)
        Me.Controls.Add(Me.BtnCopyToAllDiv)
        Me.Controls.Add(Me.TxtTermCondition)
        Me.Controls.Add(Me.TxtSiteCode)
        Me.Controls.Add(Me.Label27)
        Me.Controls.Add(Me.Label26)
        Me.Controls.Add(Me.LblEntryType)
        Me.Controls.Add(Me.TxtVoucherType)
        Me.Controls.Add(Me.LblEntryTypeReq)
        Me.Name = "FrmTermCondition"
        Me.Text = "Term & Condition"
        Me.Controls.SetChildIndex(Me.LblEntryTypeReq, 0)
        Me.Controls.SetChildIndex(Me.TxtVoucherType, 0)
        Me.Controls.SetChildIndex(Me.LblEntryType, 0)
        Me.Controls.SetChildIndex(Me.Label26, 0)
        Me.Controls.SetChildIndex(Me.Label27, 0)
        Me.Controls.SetChildIndex(Me.TxtSiteCode, 0)
        Me.Controls.SetChildIndex(Me.Topctrl1, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.GrpUP, 0)
        Me.Controls.SetChildIndex(Me.GBoxEntryType, 0)
        Me.Controls.SetChildIndex(Me.GBoxApprove, 0)
        Me.Controls.SetChildIndex(Me.GBoxMoveToLog, 0)
        Me.Controls.SetChildIndex(Me.GroupBox2, 0)
        Me.Controls.SetChildIndex(Me.GBoxDivision, 0)
        Me.Controls.SetChildIndex(Me.TxtTermCondition, 0)
        Me.Controls.SetChildIndex(Me.BtnCopyToAllDiv, 0)
        Me.Controls.SetChildIndex(Me.BtnCopyToAllSite, 0)
        Me.Controls.SetChildIndex(Me.Label1, 0)
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

    Private Sub Txt_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtVoucherType.KeyDown, TxtSiteCode.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then Exit Sub
            If e.KeyCode = Keys.Delete Then sender.Tag = "" : sender.Text = "" : Exit Sub

            Select Case sender.Name
                Case TxtVoucherType.Name
                    If TxtVoucherType.AgHelpDataSet Is Nothing Then
                        mQry = " SELECT V_Type AS Code,Description AS [Entry Type], V_Type AS [Voucher Type] " &
                                " FROM Voucher_Type   " &
                                " Where IfNull(Description,'') <> '' " &
                                " Order By Description "
                        TxtVoucherType.AgHelpDataSet() = AgL.FillData(mQry, AgL.GCn)
                    End If

                Case TxtSiteCode.Name
                    If TxtSiteCode.AgHelpDataSet Is Nothing Then
                        mQry = " SELECT H.Code, H.Name FROM SiteMast H " &
                                " Order By H.Name "
                        TxtSiteCode.AgHelpDataSet() = AgL.FillData(mQry, AgL.GCn)
                    End If

            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub BtnCopyToAllSite_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCopyToAllSite.Click
        If Topctrl1.Mode <> "Browse" Then Exit Sub
        If MsgBox("Are You Sure To Copy this for All Sites?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton1, AgLibrary.ClsMain.PubMsgTitleInfo) = vbYes Then
            ProcCopyToAllSite()
            MsgBox("Process is completed !")
        End If
    End Sub

    Private Sub ProcCopyToAllSite()
        Dim DsTemp As DataSet
        Dim mTrans As String = ""
        Dim I As Integer
        mQry = "SELECT Code FROM SiteMast WHERE Code <> '" & AgL.PubSiteCode & "'"
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        AgL.ECmd = AgL.GCn.CreateCommand
        AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
        AgL.ECmd.Transaction = AgL.ETrans
        mTrans = "Begin"

        Try
            With DsTemp.Tables(0)
                If .Rows.Count > 0 Then
                    For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                        mQry = " INSERT INTO Voucher_Type_Settings_Log " &
                                " SELECT * FROM Voucher_Type_Settings WHERE V_Type = '" & TxtVoucherType.Tag & "' AND Site_Code = '" & AgL.XNull(.Rows(I)("Code")) & "' AND Div_Code = '" & AgL.PubDivCode & "' "
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                        mQry = "Select count(*) From Voucher_Type_Settings  Where V_Type='" & TxtVoucherType.AgSelectedValue & "' And Div_Code = '" & AgL.PubDivCode & "' And Site_Code = '" & AgL.XNull(.Rows(I)("Code")) & "' "
                        If AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar <= 0 Then
                            mQry = " INSERT INTO Voucher_Type_Settings (Code, V_Type , EntryBy , EntryDate, ApproveBy ,ApproveDate , Site_Code, Div_Code ) " &
                                    " Values (" & AgL.Chk_Text(GetCode(AgL.XNull(.Rows(I)("Code")), AgL.PubDivCode)) & ", " & AgL.Chk_Text(TxtVoucherType.Tag) & ", " & AgL.Chk_Text(AgL.PubUserName) & ", " & AgL.Chk_Text(AgL.GetDateTime(AgL.GcnRead)) & ", " & AgL.Chk_Text(AgL.PubUserName) & ", " & AgL.Chk_Text(AgL.GetDateTime(AgL.GcnRead)) & ",'" & AgL.XNull(.Rows(I)("Code")) & "', " & AgL.Chk_Text(AgL.PubDivCode) & " ) "
                            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                        End If

                        mQry = "  Update Voucher_Type_Settings  " &
                                " SET TermsCondition = V1.TermsCondition " &
                                " FROM " &
                                " ( " &
                                " SELECT *    " &
                                " From Voucher_Type_Settings   " &
                                " Where V_TYpe = " & AgL.Chk_Text(TxtVoucherType.Tag) & " AND Div_Code = " & AgL.Chk_Text(AgL.PubDivCode) & " AND Site_Code =" & AgL.Chk_Text(AgL.PubSiteCode) & "  " &
                                " ) V1 WHERE V1.V_TYpe = Voucher_Type_Settings.V_Type  " &
                                " AND V1.Div_Code = Voucher_Type_Settings.Div_Code  " &
                                " AND voucher_type_settings.Site_Code =  '" & AgL.XNull(.Rows(I)("Code")) & "' "
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    Next
                End If
            End With

            AgL.ETrans.Commit()
            mTrans = "Commit"
        Catch ex As Exception
            If mTrans = "Begin" Then
                AgL.ETrans.Rollback()
            ElseIf mTrans = "Commit" Then
                Topctrl1.FButtonClick(14, True)
            End If
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ProcCopyToAllDivision()
        Dim DsTemp As DataSet
        Dim mTrans As String = ""
        Dim I As Integer
        mQry = "SELECT Div_Code AS Code FROM Division WHERE Div_Code <> '" & AgL.PubDivCode & "'"
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        AgL.ECmd = AgL.GCn.CreateCommand
        AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
        AgL.ECmd.Transaction = AgL.ETrans
        mTrans = "Begin"

        Try
            With DsTemp.Tables(0)
                If .Rows.Count > 0 Then
                    For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                        mQry = " INSERT INTO Voucher_Type_Settings_Log " &
                                " SELECT * FROM Voucher_Type_Settings WHERE V_Type = '" & TxtVoucherType.Tag & "' AND Div_Code = '" & AgL.XNull(.Rows(I)("Code")) & "' AND Site_Code = '" & AgL.PubSiteCode & "' "
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                        mQry = "Select count(*) From Voucher_Type_Settings  Where V_Type='" & TxtVoucherType.AgSelectedValue & "' And Site_Code = '" & AgL.PubSiteCode & "' And Div_Code = '" & AgL.XNull(.Rows(I)("Code")) & "' "
                        If AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar <= 0 Then
                            mQry = " INSERT INTO Voucher_Type_Settings (Code, V_Type , EntryBy , EntryDate, ApproveBy ,ApproveDate, Div_Code, Site_Code ) " &
                                    " Values (" & AgL.Chk_Text(GetCode(AgL.PubSiteCode, AgL.XNull(.Rows(I)("Code")))) & ", " & AgL.Chk_Text(TxtVoucherType.Tag) & ", " & AgL.Chk_Text(AgL.PubUserName) & ", " & AgL.Chk_Text(AgL.GetDateTime(AgL.GcnRead)) & ", " & AgL.Chk_Text(AgL.PubUserName) & ", " & AgL.Chk_Text(AgL.GetDateTime(AgL.GcnRead)) & ",'" & AgL.XNull(.Rows(I)("Code")) & "', " & AgL.Chk_Text(AgL.PubSiteCode) & " ) "
                            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                        End If

                        mQry = "  Update Voucher_Type_Settings  " &
                                " SET TermsCondition = V1.TermsCondition " &
                                " FROM " &
                                " ( " &
                                " SELECT *    " &
                                " From Voucher_Type_Settings   " &
                                " Where V_TYpe = " & AgL.Chk_Text(TxtVoucherType.Tag) & " AND Site_Code = " & AgL.Chk_Text(AgL.PubSiteCode) & " AND Div_Code = " & AgL.Chk_Text(AgL.PubDivCode) & "  " &
                                " ) V1 WHERE V1.V_TYpe = Voucher_Type_Settings.V_Type  " &
                                " AND V1.Site_Code = Voucher_Type_Settings.Site_Code  " &
                                " AND voucher_type_settings.Div_Code =  '" & AgL.XNull(.Rows(I)("Code")) & "' "
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                    Next
                End If
            End With

            AgL.ETrans.Commit()
            mTrans = "Commit"
        Catch ex As Exception
            If mTrans = "Begin" Then
                AgL.ETrans.Rollback()
            ElseIf mTrans = "Commit" Then
                Topctrl1.FButtonClick(14, True)
            End If
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function GetCode(ByVal bSiteCode As String, ByVal bDivCode As String) As String
        GetCode = AgL.GetMaxId("Voucher_Type_Settings", "Code", AgL.GCn, bDivCode, bSiteCode, 4, True, True, , AgL.Gcn_ConnectionString)
    End Function

    Private Sub BtnCopyToAllDiv_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCopyToAllDiv.Click
        If Topctrl1.Mode <> "Browse" Then Exit Sub
        If MsgBox("Are You Sure To Copy this for All Division?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton1, AgLibrary.ClsMain.PubMsgTitleInfo) = vbYes Then
            ProcCopyToAllDivision()
            MsgBox("Process is completed !")
        End If
    End Sub
End Class
