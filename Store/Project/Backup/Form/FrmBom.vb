Imports CrystalDecisions.CrystalReports.Engine
Public Class FrmBom
    Inherits AgTemplate.TempMaster
    Dim mQry$
    Public Const ColSNo As String = "SNo"
    Public WithEvents DGL1 As New AgControls.AgDataGrid
    Public Const Col1BaseItem As String = "Item"
    Public Const Col1BaseItemGroup As String = "Item Group"
    Public Const Col1Process As String = "Process"
    Public Const Col1Qty As String = "Qty"
    Public Const Col1Unit As String = "Unit"
    Public Const Col1Wastage As String = "Wastage %"
    Public Const Col1IsMarkedForMainItem As String = "Is Marked For Main Item"
    Public Const Col1ShowBom As String = "Show Bom"

    Public Sub New(ByVal StrUPVar As String, ByVal DTUP As DataTable)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Topctrl1.FSetParent(Me, StrUPVar, DTUP)
        Topctrl1.SetDisp(True)
    End Sub

#Region "Designer Code"
    Private Sub InitializeComponent()
        Me.TxtProdBatchQty = New AgControls.AgTextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.TxtItem = New AgControls.AgTextBox
        Me.LblItem = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.LblForQuantity = New System.Windows.Forms.Label
        Me.TxtProdBatchUnit = New AgControls.AgTextBox
        Me.LblUnit = New System.Windows.Forms.Label
        Me.Pnl1 = New System.Windows.Forms.Panel
        Me.TxtCopyFrom = New AgControls.AgTextBox
        Me.GrpCopyFrom = New System.Windows.Forms.GroupBox
        Me.LblPaymentDetail = New System.Windows.Forms.LinkLabel
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.GrpUP.SuspendLayout()
        Me.GBoxEntryType.SuspendLayout()
        Me.GBoxMoveToLog.SuspendLayout()
        Me.GBoxApprove.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GBoxDivision.SuspendLayout()
        CType(Me.DTMaster, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GrpCopyFrom.SuspendLayout()
        Me.SuspendLayout()
        '
        'Topctrl1
        '
        Me.Topctrl1.Size = New System.Drawing.Size(894, 41)
        Me.Topctrl1.tAdd = False
        Me.Topctrl1.tDel = False
        Me.Topctrl1.tEdit = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Location = New System.Drawing.Point(0, 474)
        Me.GroupBox1.Size = New System.Drawing.Size(936, 4)
        '
        'GrpUP
        '
        Me.GrpUP.Location = New System.Drawing.Point(14, 478)
        '
        'TxtEntryBy
        '
        Me.TxtEntryBy.Tag = ""
        Me.TxtEntryBy.Text = ""
        '
        'GBoxEntryType
        '
        Me.GBoxEntryType.Location = New System.Drawing.Point(240, 478)
        '
        'TxtEntryType
        '
        Me.TxtEntryType.Tag = ""
        '
        'GBoxMoveToLog
        '
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(553, 478)
        '
        'TxtMoveToLog
        '
        Me.TxtMoveToLog.Tag = ""
        '
        'GBoxApprove
        '
        Me.GBoxApprove.Location = New System.Drawing.Point(399, 478)
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
        Me.GroupBox2.Location = New System.Drawing.Point(703, 478)
        '
        'GBoxDivision
        '
        Me.GBoxDivision.Location = New System.Drawing.Point(275, 478)
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
        'TxtProdBatchQty
        '
        Me.TxtProdBatchQty.AgAllowUserToEnableMasterHelp = False
        Me.TxtProdBatchQty.AgLastValueTag = Nothing
        Me.TxtProdBatchQty.AgLastValueText = Nothing
        Me.TxtProdBatchQty.AgMandatory = True
        Me.TxtProdBatchQty.AgMasterHelp = True
        Me.TxtProdBatchQty.AgNumberLeftPlaces = 8
        Me.TxtProdBatchQty.AgNumberNegetiveAllow = False
        Me.TxtProdBatchQty.AgNumberRightPlaces = 3
        Me.TxtProdBatchQty.AgPickFromLastValue = False
        Me.TxtProdBatchQty.AgRowFilter = ""
        Me.TxtProdBatchQty.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtProdBatchQty.AgSelectedValue = Nothing
        Me.TxtProdBatchQty.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtProdBatchQty.AgValueType = AgControls.AgTextBox.TxtValueType.Number_Value
        Me.TxtProdBatchQty.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtProdBatchQty.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtProdBatchQty.Location = New System.Drawing.Point(341, 78)
        Me.TxtProdBatchQty.MaxLength = 0
        Me.TxtProdBatchQty.Name = "TxtProdBatchQty"
        Me.TxtProdBatchQty.Size = New System.Drawing.Size(129, 15)
        Me.TxtProdBatchQty.TabIndex = 2
        Me.TxtProdBatchQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(326, 62)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(10, 7)
        Me.Label1.TabIndex = 679
        Me.Label1.Text = "Ä"
        '
        'TxtItem
        '
        Me.TxtItem.AgAllowUserToEnableMasterHelp = False
        Me.TxtItem.AgLastValueTag = Nothing
        Me.TxtItem.AgLastValueText = Nothing
        Me.TxtItem.AgMandatory = True
        Me.TxtItem.AgMasterHelp = True
        Me.TxtItem.AgNumberLeftPlaces = 0
        Me.TxtItem.AgNumberNegetiveAllow = False
        Me.TxtItem.AgNumberRightPlaces = 0
        Me.TxtItem.AgPickFromLastValue = False
        Me.TxtItem.AgRowFilter = ""
        Me.TxtItem.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtItem.AgSelectedValue = Nothing
        Me.TxtItem.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtItem.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtItem.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtItem.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtItem.Location = New System.Drawing.Point(341, 60)
        Me.TxtItem.MaxLength = 50
        Me.TxtItem.Name = "TxtItem"
        Me.TxtItem.Size = New System.Drawing.Size(325, 15)
        Me.TxtItem.TabIndex = 1
        '
        'LblItem
        '
        Me.LblItem.AutoSize = True
        Me.LblItem.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblItem.Location = New System.Drawing.Point(240, 60)
        Me.LblItem.Name = "LblItem"
        Me.LblItem.Size = New System.Drawing.Size(33, 16)
        Me.LblItem.TabIndex = 674
        Me.LblItem.Text = "Item"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.LemonChiffon
        Me.Panel1.Location = New System.Drawing.Point(14, 449)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(866, 23)
        Me.Panel1.TabIndex = 697
        '
        'LblForQuantity
        '
        Me.LblForQuantity.AutoSize = True
        Me.LblForQuantity.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblForQuantity.Location = New System.Drawing.Point(240, 78)
        Me.LblForQuantity.Name = "LblForQuantity"
        Me.LblForQuantity.Size = New System.Drawing.Size(80, 16)
        Me.LblForQuantity.TabIndex = 698
        Me.LblForQuantity.Text = "For Quantity"
        '
        'TxtProdBatchUnit
        '
        Me.TxtProdBatchUnit.AgAllowUserToEnableMasterHelp = False
        Me.TxtProdBatchUnit.AgLastValueTag = Nothing
        Me.TxtProdBatchUnit.AgLastValueText = Nothing
        Me.TxtProdBatchUnit.AgMandatory = True
        Me.TxtProdBatchUnit.AgMasterHelp = False
        Me.TxtProdBatchUnit.AgNumberLeftPlaces = 0
        Me.TxtProdBatchUnit.AgNumberNegetiveAllow = False
        Me.TxtProdBatchUnit.AgNumberRightPlaces = 0
        Me.TxtProdBatchUnit.AgPickFromLastValue = False
        Me.TxtProdBatchUnit.AgRowFilter = ""
        Me.TxtProdBatchUnit.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtProdBatchUnit.AgSelectedValue = Nothing
        Me.TxtProdBatchUnit.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtProdBatchUnit.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtProdBatchUnit.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtProdBatchUnit.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtProdBatchUnit.Location = New System.Drawing.Point(559, 78)
        Me.TxtProdBatchUnit.MaxLength = 50
        Me.TxtProdBatchUnit.Name = "TxtProdBatchUnit"
        Me.TxtProdBatchUnit.Size = New System.Drawing.Size(107, 15)
        Me.TxtProdBatchUnit.TabIndex = 3
        '
        'LblUnit
        '
        Me.LblUnit.AutoSize = True
        Me.LblUnit.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblUnit.Location = New System.Drawing.Point(492, 77)
        Me.LblUnit.Name = "LblUnit"
        Me.LblUnit.Size = New System.Drawing.Size(31, 16)
        Me.LblUnit.TabIndex = 701
        Me.LblUnit.Text = "Unit"
        '
        'Pnl1
        '
        Me.Pnl1.Location = New System.Drawing.Point(15, 128)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(865, 320)
        Me.Pnl1.TabIndex = 4
        '
        'TxtCopyFrom
        '
        Me.TxtCopyFrom.AgAllowUserToEnableMasterHelp = False
        Me.TxtCopyFrom.AgLastValueTag = Nothing
        Me.TxtCopyFrom.AgLastValueText = Nothing
        Me.TxtCopyFrom.AgMandatory = True
        Me.TxtCopyFrom.AgMasterHelp = False
        Me.TxtCopyFrom.AgNumberLeftPlaces = 0
        Me.TxtCopyFrom.AgNumberNegetiveAllow = False
        Me.TxtCopyFrom.AgNumberRightPlaces = 0
        Me.TxtCopyFrom.AgPickFromLastValue = False
        Me.TxtCopyFrom.AgRowFilter = ""
        Me.TxtCopyFrom.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtCopyFrom.AgSelectedValue = Nothing
        Me.TxtCopyFrom.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtCopyFrom.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtCopyFrom.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtCopyFrom.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCopyFrom.Location = New System.Drawing.Point(6, 19)
        Me.TxtCopyFrom.MaxLength = 50
        Me.TxtCopyFrom.Name = "TxtCopyFrom"
        Me.TxtCopyFrom.Size = New System.Drawing.Size(164, 15)
        Me.TxtCopyFrom.TabIndex = 703
        '
        'GrpCopyFrom
        '
        Me.GrpCopyFrom.Controls.Add(Me.TxtCopyFrom)
        Me.GrpCopyFrom.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GrpCopyFrom.ForeColor = System.Drawing.Color.DarkRed
        Me.GrpCopyFrom.Location = New System.Drawing.Point(697, 80)
        Me.GrpCopyFrom.Name = "GrpCopyFrom"
        Me.GrpCopyFrom.Size = New System.Drawing.Size(176, 42)
        Me.GrpCopyFrom.TabIndex = 705
        Me.GrpCopyFrom.TabStop = False
        Me.GrpCopyFrom.Text = "Copy From"
        '
        'LblPaymentDetail
        '
        Me.LblPaymentDetail.BackColor = System.Drawing.Color.SteelBlue
        Me.LblPaymentDetail.DisabledLinkColor = System.Drawing.Color.White
        Me.LblPaymentDetail.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblPaymentDetail.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LblPaymentDetail.LinkColor = System.Drawing.Color.White
        Me.LblPaymentDetail.Location = New System.Drawing.Point(15, 107)
        Me.LblPaymentDetail.Name = "LblPaymentDetail"
        Me.LblPaymentDetail.Size = New System.Drawing.Size(135, 20)
        Me.LblPaymentDetail.TabIndex = 735
        Me.LblPaymentDetail.TabStop = True
        Me.LblPaymentDetail.Text = "Consumption List"
        Me.LblPaymentDetail.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(326, 80)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(10, 7)
        Me.Label2.TabIndex = 736
        Me.Label2.Text = "Ä"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(543, 80)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(10, 7)
        Me.Label3.TabIndex = 737
        Me.Label3.Text = "Ä"
        '
        'FrmBom
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(894, 522)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.LblPaymentDetail)
        Me.Controls.Add(Me.GrpCopyFrom)
        Me.Controls.Add(Me.TxtProdBatchUnit)
        Me.Controls.Add(Me.LblUnit)
        Me.Controls.Add(Me.LblForQuantity)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.TxtProdBatchQty)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Pnl1)
        Me.Controls.Add(Me.TxtItem)
        Me.Controls.Add(Me.LblItem)
        Me.Name = "FrmBom"
        Me.Text = "BOM Master"
        Me.Controls.SetChildIndex(Me.LblItem, 0)
        Me.Controls.SetChildIndex(Me.TxtItem, 0)
        Me.Controls.SetChildIndex(Me.Pnl1, 0)
        Me.Controls.SetChildIndex(Me.Label1, 0)
        Me.Controls.SetChildIndex(Me.TxtProdBatchQty, 0)
        Me.Controls.SetChildIndex(Me.Panel1, 0)
        Me.Controls.SetChildIndex(Me.LblForQuantity, 0)
        Me.Controls.SetChildIndex(Me.LblUnit, 0)
        Me.Controls.SetChildIndex(Me.TxtProdBatchUnit, 0)
        Me.Controls.SetChildIndex(Me.GrpCopyFrom, 0)
        Me.Controls.SetChildIndex(Me.GBoxDivision, 0)
        Me.Controls.SetChildIndex(Me.GroupBox2, 0)
        Me.Controls.SetChildIndex(Me.Topctrl1, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.GrpUP, 0)
        Me.Controls.SetChildIndex(Me.GBoxEntryType, 0)
        Me.Controls.SetChildIndex(Me.GBoxApprove, 0)
        Me.Controls.SetChildIndex(Me.GBoxMoveToLog, 0)
        Me.Controls.SetChildIndex(Me.LblPaymentDetail, 0)
        Me.Controls.SetChildIndex(Me.Label2, 0)
        Me.Controls.SetChildIndex(Me.Label3, 0)
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
        Me.GrpCopyFrom.ResumeLayout(False)
        Me.GrpCopyFrom.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents LblItem As System.Windows.Forms.Label
    Public WithEvents Panel1 As System.Windows.Forms.Panel
    Public WithEvents TxtItem As AgControls.AgTextBox
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents TxtProdBatchQty As AgControls.AgTextBox
    Public WithEvents LblForQuantity As System.Windows.Forms.Label
    Public WithEvents TxtProdBatchUnit As AgControls.AgTextBox
    Public WithEvents LblUnit As System.Windows.Forms.Label
    Public WithEvents Pnl1 As System.Windows.Forms.Panel
    Protected WithEvents TxtCopyFrom As AgControls.AgTextBox
    Protected WithEvents GrpCopyFrom As System.Windows.Forms.GroupBox
    Protected WithEvents LblPaymentDetail As System.Windows.Forms.LinkLabel
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
#End Region

    Private Sub FrmYarn_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        Dim I As Integer = 0
        Dim J As Integer = 0

        Call Calculation()

        If AgL.RequiredField(TxtItem, LblItem.Text) Then passed = False : Exit Sub
        If AgL.RequiredField(TxtProdBatchQty, LblForQuantity.Text) Then passed = False : Exit Sub
        If AgL.RequiredField(TxtProdBatchUnit, LblUnit.Text) Then passed = False : Exit Sub

        If AgCL.AgIsBlankGrid(DGL1, DGL1.Columns(Col1BaseItem).Index) Then passed = False : Exit Sub
        If AgCL.AgIsDuplicate(DGL1, "" & DGL1.Columns(Col1Process).Index & "," & DGL1.Columns(Col1BaseItem).Index & "") Then passed = False : Exit Sub

        With DGL1
            For I = 0 To .Rows.Count - 1
                If .Item(Col1BaseItem, I).Value <> "" Then
                    If .Item(Col1Process, I).Value = "" Then
                        DGL1.CurrentCell = DGL1.Item(Col1Process, I) : DGL1.Focus()
                        'Err.Raise(1, , "Process Is Blank At Row No. " & DGL1.Item(ColSNo, I).Value & " ")
                    End If

                    If Val(.Item(Col1Qty, I).Value) = 0 Then
                        DGL1.CurrentCell = DGL1.Item(Col1Qty, I) : DGL1.Focus()
                        Err.Raise(1, , "Qty Is Blank At Row No. " & DGL1.Item(ColSNo, I).Value & " ")
                    End If
                End If
            Next
        End With
    End Sub

    Private Sub FrmYarn_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        AgL.PubFindQry = "SELECT I.Code, I.Description As Item " & _
                            " FROM Item I Where I.ItemType = '" & ClsMain.ItemType.BOM & "' "
        AgL.PubFindQryOrdBy = "[Item]"
    End Sub

    Private Sub FrmYarn_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "Item"
        LogTableName = "Item_Log"
        MainLineTableCsv = "BomDetail"
        LogLineTableCsv = "BomDetail_Log"
    End Sub

    Private Sub FrmYarn_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As System.Data.SqlClient.SqlConnection, ByVal Cmd As System.Data.SqlClient.SqlCommand) Handles Me.BaseEvent_Save_InTrans
        Dim I As Integer = 0
        Dim mSr As Integer = 0

        mQry = " UPDATE Item SET " & _
                " Description = " & AgL.Chk_Text(TxtItem.Text) & ", " & _
                " ItemType = " & AgL.Chk_Text(ClsMain.ItemType.BOM) & ", " & _
                " Unit = " & AgL.Chk_Text(TxtProdBatchUnit.Text) & ", " & _
                " ProdBatchQty = " & Val(TxtProdBatchQty.Text) & ", " & _
                " ProdBatchUnit = " & AgL.Chk_Text(TxtProdBatchUnit.Text) & " " & _
                " Where Code = '" & SearchCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        mQry = "Delete From BomDetail Where Code = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        For I = 0 To DGL1.RowCount - 1
            If DGL1.Item(Col1BaseItem, I).Value <> "" Then
                mSr += 1
                mQry = " INSERT INTO BomDetail(Code, Sr, BaseItem, Process, IsMarkedForMainItem, " & _
                        " Item, Qty, Unit, WastagePer ) " & _
                        " VALUES (" & AgL.Chk_Text(mSearchCode) & ", " & _
                        " " & mSr & ", " & _
                        " " & AgL.Chk_Text(DGL1.Item(Col1BaseItem, I).Tag) & ", " & _
                        " " & AgL.Chk_Text(DGL1.Item(Col1Process, I).Tag) & ", " & _
                        " " & IIf(AgL.StrCmp(DGL1.Item(Col1IsMarkedForMainItem, I).Value, AgLibrary.ClsConstant.StrCheckedValue), 1, 0) & ", " & _
                        " " & AgL.Chk_Text(DGL1.Item(Col1BaseItem, I).Tag) & ", " & Val(DGL1.Item(Col1Qty, I).Value) & " , " & _
                        " " & AgL.Chk_Text(DGL1.Item(Col1Unit, I).Value) & ", " & Val(DGL1.Item(Col1Wastage, I).Value) & " )"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If
        Next

        If AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Or AgL.StrCmp(AgL.PubUserName, "Sa") Then
            AgCL.GridSetiingWriteXml(Me.Text & DGL1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, DGL1)
        End If
    End Sub

    Private Sub FrmQuality1_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
        DGL1.RowCount = 1 : DGL1.Rows.Clear()
    End Sub

    Private Sub FrmQuality1_BaseFunction_DispText() Handles Me.BaseFunction_DispText
        If AgL.StrCmp(Topctrl1.Mode, "Add") Then
            GrpCopyFrom.Visible = True
        Else
            GrpCopyFrom.Visible = False
        End If
    End Sub

    Private Sub FrmYarn_BaseFunction_FIniMast(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMast
        mQry = "Select Code As SearchCode " & _
                " From Item " & _
                " Where ItemType = '" & ClsMain.ItemType.BOM & "'" & _
                " Order By Description "
        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub FrmQuality1_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        With AgCL
            .AddAgTextColumn(DGL1, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgTextColumn(DGL1, Col1BaseItem, 200, 0, Col1BaseItem, True, False, False)
            .AddAgTextColumn(DGL1, Col1BaseItemGroup, 100, 0, Col1BaseItemGroup, True, True, False)
            .AddAgTextColumn(DGL1, Col1Process, 120, 0, Col1Process, True, False, False)
            .AddAgNumberColumn(DGL1, Col1Qty, 80, 8, 3, False, Col1Qty, True, False, True)
            .AddAgTextColumn(DGL1, Col1Unit, 70, 10, Col1Unit, True, False, False)
            .AddAgNumberColumn(DGL1, Col1Wastage, 60, 8, 3, False, Col1Wastage, True, False, True)
            .AddAgCheckColumn(DGL1, Col1IsMarkedForMainItem, 100, Col1IsMarkedForMainItem, True)
            .AddAgButtonColumn(DGL1, Col1ShowBom, 50, Col1ShowBom, True)
        End With
        AgL.AddAgDataGrid(DGL1, Pnl1)
        DGL1.ColumnHeadersHeight = 35
        DGL1.EnableHeadersVisualStyles = False
        DGL1.ColumnHeadersHeight = 70

        Try
            DGL1.Item(Col1IsMarkedForMainItem, 0).Value = AgLibrary.ClsConstant.StrUnCheckedValue
        Catch ex As Exception
        End Try
    End Sub

    Private Sub FrmQuality1_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim DsTemp As DataSet
        Dim DrTemp As DataRow() = Nothing

        mQry = "Select I.* " & _
                " From Item I " & _
                " Where I.Code ='" & SearchCode & "'"
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                mInternalCode = AgL.XNull(.Rows(0)("Code"))
                TxtItem.Text = AgL.XNull(.Rows(0)("Description"))
                TxtProdBatchUnit.Text = AgL.XNull(.Rows(0)("ProdBatchUnit"))
                TxtProdBatchQty.Text = Format(AgL.VNull(.Rows(0)("ProdBatchQty")), "0.000")
                '-------------------------------------------------------------
                'Line Records are showing in Grid
                '-------------------------------------------------------------
                Dim I As Integer
                mQry = " Select L.*, Ig.Description as ItemGroupName, " & _
                       " P.Description As ProcessDesc , SFI.Description As BaseItemDesc " & _
                       " From BomDetail L " & _
                       " Left Join Item SFI On SFI.Code = L.BaseItem    " & _
                       " Left Join ItemGroup IG On SFI.ItemGroup = IG.Code " & _
                       " LEFT JOIN Process P On L.Process = P.NCat " & _
                       " Where L.Code = '" & mSearchCode & "' " & _
                       " Order BY L.Sr "
                DsTemp = AgL.FillData(mQry, AgL.GCn)
                With DsTemp.Tables(0)
                    DGL1.RowCount = 1
                    DGL1.Rows.Clear()
                    If .Rows.Count > 0 Then
                        For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                            DGL1.Rows.Add()
                            DGL1.Item(ColSNo, I).Value = DGL1.Rows.Count - 1
                            DGL1.Item(Col1Process, I).Tag = AgL.XNull(.Rows(I)("Process"))
                            DGL1.Item(Col1Process, I).Value = AgL.XNull(.Rows(I)("ProcessDesc"))
                            DGL1.Item(Col1IsMarkedForMainItem, I).Value = IIf(AgL.VNull(.Rows(I)("IsMarkedForMainItem")) = 0, AgLibrary.ClsConstant.StrUnCheckedValue, AgLibrary.ClsConstant.StrCheckedValue)
                            DGL1.Item(Col1BaseItem, I).Tag = AgL.XNull(.Rows(I)("BaseItem"))
                            DGL1.Item(Col1BaseItem, I).Value = AgL.XNull(.Rows(I)("BaseItemDesc"))
                            DGL1.Item(Col1BaseItemGroup, I).Value = AgL.XNull(.Rows(I)("ItemGroupName"))
                            DGL1.Item(Col1Qty, I).Value = Format(AgL.VNull(.Rows(I)("Qty")), "0.000")
                            DGL1.Item(Col1Unit, I).Tag = AgL.XNull(.Rows(I)("Unit"))
                            DGL1.Item(Col1Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                            DGL1.Item(Col1Wastage, I).Value = Format(AgL.VNull(.Rows(I)("WastagePer")), "0.000")
                        Next I
                    End If
                End With
            End If
        End With
        GrpCopyFrom.Visible = False

        AgCL.GridSetiingShowXml(Me.Text & DGL1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, DGL1, False)
    End Sub

    Private Sub Topctrl1_tbAdd() Handles Topctrl1.tbAdd
        TxtItem.Focus()
    End Sub

    Private Sub Topctrl1_tbEdit() Handles Topctrl1.tbEdit
        TxtItem.Focus()
    End Sub

    Private Sub DGL1_EditingControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DGL1.EditingControl_KeyDown
        If Topctrl1.Mode = "Browse" Then Exit Sub

        Try
            Select Case DGL1.Columns(DGL1.CurrentCell.ColumnIndex).Name
                Case Col1BaseItem
                    If e.KeyCode <> Keys.Enter And e.KeyCode <> Keys.Insert Then
                        If DGL1.AgHelpDataSet(Col1BaseItem) Is Nothing Then
                            FCreateHelpItem()
                        End If
                    ElseIf e.KeyCode = Keys.Insert Then
                        FOpenItemMaster(DGL1.CurrentCell.RowIndex)
                    End If

                Case Col1Process
                    If e.KeyCode <> Keys.Enter And e.KeyCode <> Keys.Insert Then
                        If DGL1.AgHelpDataSet(Col1Process) Is Nothing Then
                            FCreateHelpProcess(Col1Process)
                        End If
                    ElseIf e.KeyCode = Keys.Insert Then
                        FOpenProcessMaster(DGL1.CurrentCell.ColumnIndex, DGL1.CurrentCell.RowIndex)
                    End If

                Case Col1Unit
                    If e.KeyCode <> Keys.Enter Then
                        If DGL1.AgHelpDataSet(Col1Unit) Is Nothing Then
                            mQry = " SELECT Code as Code, Code as  Unit " & _
                                    " FROM Unit ORDER BY Code "
                            DGL1.AgHelpDataSet(Col1Unit) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FCreateHelpItem()
        mQry = " SELECT I.Code AS Code, I.Description AS Item , IG.Description AS ItemGroupDesc, I.Unit, I.ItemType, I.Div_Code, I.ItemGroup " & _
               " FROM Item I  " & _
               " LEFT JOIN ItemGroup IG ON IG. Code = I.ItemGroup " & _
               " Order By I.Description"
        DGL1.AgHelpDataSet(Col1BaseItem, 4) = AgL.FillData(mQry, AgL.GCn)
    End Sub

    Private Sub FCreateHelpProcess(ByVal strColName As String)
        mQry = "SELECT H.NCat, H.Description As Process  " & _
               "FROM Process H "
        DGL1.AgHelpDataSet(strColName) = AgL.FillData(mQry, AgL.GCn)
    End Sub

    Private Sub FOpenItemMaster(ByVal RowIndex As Integer)
        Dim DrTemp As DataRow() = Nothing
        Dim bItemCode$ = ""
        bItemCode = AgTemplate.ClsMain.FOpenMaster(Me, "BOM Master", "BOM")
        DGL1.Item(Col1BaseItem, RowIndex).Value = ""
        DGL1.Item(Col1BaseItem, RowIndex).Tag = ""
        DGL1.CurrentCell = DGL1.Item(Col1Qty, RowIndex)
        FCreateHelpItem()
        DrTemp = DGL1.AgHelpDataSet(Col1BaseItem).Tables(0).Select("Code = '" & bItemCode & "'")
        DGL1.Item(Col1BaseItem, RowIndex).Tag = bItemCode
        DGL1.Item(Col1BaseItem, RowIndex).Value = AgL.XNull(AgL.Dman_Execute("Select Description From Item Where Code = '" & DGL1.Item(Col1BaseItem, DGL1.CurrentCell.RowIndex).Tag & "'", AgL.GCn).ExecuteScalar)
        Validating_Item(RowIndex)
        DGL1.CurrentCell = DGL1.Item(Col1BaseItem, RowIndex)
        SendKeys.Send("{Enter}")
    End Sub

    Private Sub FOpenProcessMaster(ByVal ColumnIndex As Integer, ByVal RowIndex As Integer)
        'Dim DrTemp As DataRow() = Nothing
        'Dim bItemCode$ = ""
        'bItemCode = AgTemplate.ClsMain.FOpenMaster(Me, "Process Master", "BOM")
        'DGL1.Item(ColumnIndex, RowIndex).Value = ""
        'DGL1.Item(ColumnIndex, RowIndex).Tag = ""
        'DGL1.CurrentCell = DGL1.Item(Col1Qty, RowIndex)
        'FCreateHelpProcess(DGL1.Columns(ColumnIndex).Name)
        'DrTemp = DGL1.AgHelpDataSet(ColumnIndex).Tables(0).Select("Code = '" & bItemCode & "'")
        'DGL1.Item(ColumnIndex, RowIndex).Tag = bItemCode
        'DGL1.Item(ColumnIndex, RowIndex).Value = AgL.XNull(AgL.Dman_Execute("Select Description From Process Where NCat = '" & DGL1.Item(ColumnIndex, DGL1.CurrentCell.RowIndex).Tag & "'", AgL.GCn).ExecuteScalar)
        'DGL1.CurrentCell = DGL1.Item(ColumnIndex, RowIndex)
        'SendKeys.Send("{Enter}")
    End Sub

    Public Sub Dgl1_EditingControl_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles DGL1.EditingControl_Validating
        If Topctrl1.Mode = "Browse" Then Exit Sub
        Dim mRowIndex As Integer, mColumnIndex As Integer
        Dim DrTemp As DataRow() = Nothing
        Try
            mRowIndex = DGL1.CurrentCell.RowIndex
            mColumnIndex = DGL1.CurrentCell.ColumnIndex
            If DGL1.Item(mColumnIndex, mRowIndex).Value Is Nothing Then DGL1.Item(mColumnIndex, mRowIndex).Value = ""
            Select Case DGL1.Columns(DGL1.CurrentCell.ColumnIndex).Name
                Case Col1BaseItem
                    Validating_Item(mRowIndex)
            End Select
            Call Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Validating_Item(ByVal mRow As Integer)
        Dim DtTemp As DataTable = Nothing
        Try
            If DGL1.Item(Col1BaseItem, mRow).Value.ToString.Trim = "" Or DGL1.AgSelectedValue(Col1BaseItem, mRow).ToString.Trim = "" Then
                DGL1.Item(Col1Unit, mRow).Value = ""
                DGL1.Item(Col1BaseItemGroup, mRow).Value = ""
            Else
                If DGL1.AgDataRow IsNot Nothing Then
                    DGL1.Item(Col1BaseItemGroup, mRow).Value = AgL.XNull(DGL1.AgDataRow.Cells("ItemGroupDesc").Value)
                    DGL1.Item(Col1Unit, mRow).Value = AgL.XNull(DGL1.AgDataRow.Cells("Unit").Value)
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " On Validating_Item Function ")
        End Try
    End Sub

    Private Sub DGL1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DGL1.KeyDown
        If Topctrl1.Mode = "Browse" Then Exit Sub
        If e.Control And e.KeyCode = Keys.D Then
            sender.CurrentRow.Selected = True
        End If

        If e.KeyCode = Keys.Delete Then
            If sender.currentrow.selected Then
                If sender.Rows(sender.currentcell.rowindex).DefaultCellStyle.BackColor = RowLockedColour Then
                    MsgBox("Locked Row is not allowed to select.")
                    e.Handled = True
                Else
                    sender.Rows(sender.currentcell.rowindex).Visible = False
                    Calculation()
                    e.Handled = True
                End If
            End If
        End If

        If e.Control Or e.Shift Or e.Alt Then Exit Sub

        If DGL1.CurrentCell IsNot Nothing Then
            Select Case DGL1.Columns(DGL1.CurrentCell.ColumnIndex).Name
                Case Col1BaseItem
                    If e.KeyCode = Keys.Insert Then
                        FOpenItemMaster(DGL1.CurrentCell.RowIndex)
                    End If

                Case Col1Process
                    If e.KeyCode = Keys.Insert Then
                        FOpenProcessMaster(DGL1.CurrentCell.ColumnIndex, DGL1.CurrentCell.RowIndex)
                    End If
            End Select
        End If

        If Not AgL.StrCmp(Topctrl1.Mode, "Browse") Then
            If DGL1.CurrentCell IsNot Nothing Then
                Select Case sender.Columns(sender.CurrentCell.ColumnIndex).Name
                    Case Col1IsMarkedForMainItem
                        If e.KeyCode = Keys.Space Then
                            Try
                                If DGL1.Rows(DGL1.CurrentCell.RowIndex).DefaultCellStyle.BackColor <> RowLockedColour Then
                                    AgL.ProcSetCheckColumnCellValue(sender, sender.Columns(Col1IsMarkedForMainItem).Index)
                                End If
                            Catch ex As Exception
                            End Try
                        End If
                End Select
            End If
        End If
    End Sub

    Private Sub DGL1_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles DGL1.RowsAdded
        sender(ColSNo, e.RowIndex).Value = e.RowIndex + 1
        Try
            DGL1.Item(Col1IsMarkedForMainItem, e.RowIndex).Value = AgLibrary.ClsConstant.StrUnCheckedValue
        Catch ex As Exception
        End Try
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Private Sub TxtCopyFrom_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtCopyFrom.Validating
        Dim DsTemp As DataSet = Nothing
        Dim I As Integer = 0
        Try
            mQry = " Select L.*, I.Description As ItemDesc, IG.Description as ItemGroupName, " & _
                   " P.Description As ProcessDesc , SFI.Description As SemiFinishedItemDesc " & _
                   " From BomDetail L " & _
                   " Left Join Item I On I.Code = L.Item " & _
                   " Left Join Item SFI On SFI.Code = L.SemiFinishedItem    " & _
                   " Left Join ItemGroup IG On I.ItemGroup = IG.Code " & _
                   " LEFT JOIN Process P On L.Process = P.NCat " & _
                   " Where L.Code = '" & TxtCopyFrom.Tag & "' "
            DsTemp = AgL.FillData(mQry, AgL.GCn)
            With DsTemp.Tables(0)
                DGL1.RowCount = 1
                DGL1.Rows.Clear()
                If .Rows.Count > 0 Then
                    For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                        DGL1.Rows.Add()
                        DGL1.Item(ColSNo, I).Value = DGL1.Rows.Count - 1
                        DGL1.Item(Col1BaseItem, I).Tag = AgL.XNull(.Rows(I)("Item"))
                        DGL1.Item(Col1BaseItem, I).Value = AgL.XNull(.Rows(I)("ItemDesc"))
                        DGL1.Item(Col1BaseItemGroup, I).Value = AgL.XNull(.Rows(I)("ItemGroupName"))
                        DGL1.Item(Col1Process, I).Tag = AgL.XNull(.Rows(I)("Process"))
                        DGL1.Item(Col1Process, I).Value = AgL.XNull(.Rows(I)("ProcessDesc"))
                        DGL1.Item(Col1Unit, I).Tag = AgL.XNull(.Rows(I)("Unit"))
                        DGL1.Item(Col1Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                        DGL1.Item(Col1Qty, I).Value = Format(AgL.VNull(.Rows(I)("Qty")), "0.000")
                    Next I
                End If
            End With
            Call Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FrmQuality1_BaseFunction_Calculation() Handles Me.BaseFunction_Calculation
    End Sub

    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub

    Private Sub FrmDepartment_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AgL.WinSetting(Me, 550, 900)
        AgL.GridDesign(DGL1)
    End Sub

    Private Sub FrmBom_BaseEvent_Topctrl_tbRef() Handles Me.BaseEvent_Topctrl_tbRef
        If DGL1.AgHelpDataSet(Col1BaseItem) IsNot Nothing Then DGL1.AgHelpDataSet(Col1BaseItem) = Nothing
        If DGL1.AgHelpDataSet(Col1Process) IsNot Nothing Then DGL1.AgHelpDataSet(Col1Process) = Nothing
        If TxtItem.AgHelpDataSet IsNot Nothing Then TxtItem.AgHelpDataSet = Nothing
        If TxtProdBatchUnit.AgHelpDataSet IsNot Nothing Then TxtProdBatchUnit.AgHelpDataSet = Nothing
        If TxtCopyFrom.AgHelpDataSet IsNot Nothing Then TxtCopyFrom.AgHelpDataSet = Nothing
    End Sub

    Private Sub TxtDescription_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtItem.KeyDown, TxtProdBatchUnit.KeyDown, TxtCopyFrom.KeyDown
        Try
            Select Case sender.Name
                Case TxtItem.Name
                    If e.KeyCode <> Keys.Enter Then
                        If TxtItem.AgHelpDataSet Is Nothing Then
                            mQry = "SELECT I.Code, I.Description From Item I Where I.ItemType = '" & ClsMain.ItemType.BOM & "' Order By I.Description "
                            TxtItem.AgHelpDataSet(0) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If

                Case TxtProdBatchUnit.Name
                    If e.KeyCode <> Keys.Enter Then
                        If TxtProdBatchUnit.AgHelpDataSet Is Nothing Then
                            mQry = " SELECT Code as Code, Code as  Unit " & _
                                    " FROM Unit " & _
                                    " ORDER BY Code "
                            TxtProdBatchUnit.AgHelpDataSet() = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If

                Case TxtCopyFrom.Name
                    If e.KeyCode <> Keys.Enter Then
                        If TxtCopyFrom.AgHelpDataSet Is Nothing Then
                            mQry = "SELECT Code, Description FROM Item "
                            TxtCopyFrom.AgHelpDataSet(0, GrpCopyFrom.Top, GrpCopyFrom.Left) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FrmBom_BaseEvent_Topctrl_tbPrn(ByVal SearchCode As String) Handles Me.BaseEvent_Topctrl_tbPrn
        mQry = " SELECT H.Code, H.Description, H.ForQty, H.ForUnit, H.Item, H.EntryBy, H.ApproveBy, U.DecimalPlaces, " & _
                " L.Sr, I.Description AS ItemDesc, IM.Photo, L.Qty, L.ConsumptionPer, P.Description AS ProcessDesc, " & _
                " L.Specification, L.Unit, L.WastagePer, IG.Description AS ItemGroup  " & _
                " FROM BOM H " & _
                " LEFT JOIN BomDetail L ON L.Code = H.Code  " & _
                " LEFT JOIN Item I ON I.Code = L.BaseItem  " & _
                " LEFT JOIN Item_Image  IM ON IM.Code = H.Item  " & _
                " LEFT JOIN Unit U ON U.Code = H.ForUnit " & _
                " LEFT JOIN Process P ON P.NCat = L.Process  " & _
                " LEFT JOIN ItemGroup IG ON IG.Code = I.ItemGroup " & _
                " Where H.Code = '" & mInternalCode & "' "
        ClsMain.FPrintThisDocument(Me, "", mQry, "Noida_BOM_Print|Noida_BOM_Print_WithoutBOMQty", "BOM Master|BOM Msater", "BOM Master|BOM Master without BOM Qty")
    End Sub

    Private Sub DGL1_CellMouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DGL1.CellMouseUp
        If AgL.StrCmp(Topctrl1.Mode, "Browse") Then Exit Sub
        Dim mRowIndex As Integer, mColumnIndex As Integer

        Try
            mRowIndex = sender.CurrentCell.RowIndex
            mColumnIndex = sender.CurrentCell.ColumnIndex

            If sender.Item(mColumnIndex, mRowIndex).Value Is Nothing Then sender.Item(mColumnIndex, mRowIndex).Value = ""
            Select Case sender.Columns(sender.CurrentCell.ColumnIndex).Name
                Case Col1IsMarkedForMainItem
                    Try
                        If DGL1.Rows(DGL1.CurrentCell.RowIndex).DefaultCellStyle.BackColor <> RowLockedColour Then
                            AgL.ProcSetCheckColumnCellValue(sender, sender.Columns(Col1IsMarkedForMainItem).Index)
                        End If
                    Catch ex As Exception
                    End Try
            End Select
            Calculation()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub DGL1_CellContentClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DGL1.CellContentClick
        Try
            Select Case DGL1.Columns(DGL1.CurrentCell.ColumnIndex).Name
                Case Col1ShowBom
                    If DGL1.Item(Col1BaseItem, DGL1.CurrentCell.RowIndex).Value <> "" Then
                        Dim FrmObj As Object = Nothing
                        Dim CFOpen As New ClsFunction
                        Dim Mdi As New MDIMain
                        'FrmObj = CFOpen.FOpen(Mdi.MnuBOMMaster.Name, Mdi.MnuBOMMaster.Text, True)
                        FrmObj.MdiParent = Me.MdiParent
                        FrmObj.Show()
                        FrmObj.FindMove(DGL1.Item(Col1BaseItem, DGL1.CurrentCell.RowIndex).Tag)
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class
