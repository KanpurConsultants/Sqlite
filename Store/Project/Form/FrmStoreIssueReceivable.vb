Public Class FrmStoreIssueReceivable
    Inherits AgTemplate.TempTransaction
    Dim mQry$

    Protected WithEvents Dgl1 As New AgControls.AgDataGrid
    Protected Const ColSNo As String = "S.No."
    Protected Const Col1Item_UID As String = "Item UID"
    Protected Const Col1ItemCode As String = "Item Code"
    Protected Const Col1Item As String = "Item"
    Protected Const Col1LotNo As String = "Lot No"
    Protected Const Col1BaleNo As String = "Bale No"
    Protected Const Col1Process As String = "Process"
    Protected Const Col1ProdOrder As String = "Prod. Order"
    Protected Const Col1RequisitionNo As String = "Requisition No."
    Protected Const Col1RequisitionSr As String = "Requisition Sr"
    Protected Const Col1CurrentStock As String = "Current Stock"
    Protected Const Col1Qty As String = "Qty"
    Protected Const Col1Unit As String = "Unit"
    Protected Const Col1QtyDecimalPlaces As String = "Qty Decimal Places"
    Protected Const Col1MeasurePerPcs As String = "Measure Per Pcs"
    Protected Const Col1TotalMeasure As String = "Total Measure"
    Protected Const Col1MeasureUnit As String = "Measure Unit"
    Protected Const Col1MeasureDecimalPlaces As String = "Measure Decimal Places"
    Protected Const Col1Rate As String = "Rate"
    Protected Const Col1Amount As String = "Amount"
    Protected Const Col1Remarks As String = "Remarks"
    Protected Const Col1VNature As String = "VNature"

    Protected WithEvents LblReq_SubCode As System.Windows.Forms.Label
    Protected WithEvents TxtParty As AgControls.AgTextBox
    Protected WithEvents Label4 As System.Windows.Forms.Label
    Protected WithEvents Label3 As System.Windows.Forms.Label
    Protected WithEvents TxtProcess As AgControls.AgTextBox
    Protected WithEvents Label5 As System.Windows.Forms.Label
    Protected WithEvents LblReqNoofPerson As System.Windows.Forms.Label
    Protected WithEvents BtnFillIssueDetail As System.Windows.Forms.Button
    Protected WithEvents GrpDirectIssue As System.Windows.Forms.GroupBox
    Protected WithEvents RbtIssueDirect As System.Windows.Forms.RadioButton
    Protected WithEvents RbtIssueForReqisition As System.Windows.Forms.RadioButton
    Dim BlnIsLotNoVisible As Boolean = False

    Public Sub New(ByVal StrUPVar As String, ByVal DTUP As DataTable, ByVal NCatStr As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Topctrl1.FSetParent(Me, StrUPVar, DTUP)
        Topctrl1.SetDisp(True)

        EntryNCat = NCatStr
        mQry = "Select H.* from Voucher_Type_Settings H Left Join Voucher_Type Vt On H.V_Type = Vt.V_Type  Where Vt.NCat In ('" & EntryNCat & "') And H.Div_Code = '" & AgL.PubDivCode & "' And H.Site_Code ='" & AgL.PubSiteCode & "' "
        DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)

    End Sub

#Region "Form Designer Code"
    Private Sub InitializeComponent()
        Me.Dgl1 = New AgControls.AgDataGrid
        Me.TxtFromGodown = New AgControls.AgTextBox
        Me.LblGodown = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.LblTotalMeasure = New System.Windows.Forms.Label
        Me.Label33 = New System.Windows.Forms.Label
        Me.LblTotalQty = New System.Windows.Forms.Label
        Me.LblTotalQtyText = New System.Windows.Forms.Label
        Me.Pnl1 = New System.Windows.Forms.Panel
        Me.Label30 = New System.Windows.Forms.Label
        Me.TxtRemarks = New AgControls.AgTextBox
        Me.LblFromGodownReq = New System.Windows.Forms.Label
        Me.TxtManualRefNo = New AgControls.AgTextBox
        Me.LblManualRefNo = New System.Windows.Forms.Label
        Me.LblMaterialPlanForFollowingItems = New System.Windows.Forms.LinkLabel
        Me.Label1 = New System.Windows.Forms.Label
        Me.LblReq_SubCode = New System.Windows.Forms.Label
        Me.TxtParty = New AgControls.AgTextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.TxtProcess = New AgControls.AgTextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.LblReqNoofPerson = New System.Windows.Forms.Label
        Me.BtnFillIssueDetail = New System.Windows.Forms.Button
        Me.GrpDirectIssue = New System.Windows.Forms.GroupBox
        Me.RbtIssueDirect = New System.Windows.Forms.RadioButton
        Me.RbtIssueForReqisition = New System.Windows.Forms.RadioButton
        Me.GroupBox2.SuspendLayout()
        Me.GBoxMoveToLog.SuspendLayout()
        Me.GBoxApprove.SuspendLayout()
        Me.GBoxEntryType.SuspendLayout()
        Me.GrpUP.SuspendLayout()
        Me.GBoxDivision.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TP1.SuspendLayout()
        CType(Me.DTMaster, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.Dgl1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.GrpDirectIssue.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.Location = New System.Drawing.Point(733, 493)
        Me.GroupBox2.Size = New System.Drawing.Size(148, 40)
        '
        'TxtStatus
        '
        Me.TxtStatus.AgSelectedValue = ""
        Me.TxtStatus.Location = New System.Drawing.Point(29, 19)
        Me.TxtStatus.Tag = ""
        '
        'CmdStatus
        '
        Me.CmdStatus.Size = New System.Drawing.Size(26, 19)
        '
        'GBoxMoveToLog
        '
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(582, 493)
        Me.GBoxMoveToLog.Size = New System.Drawing.Size(148, 40)
        '
        'TxtMoveToLog
        '
        Me.TxtMoveToLog.Location = New System.Drawing.Point(29, 19)
        Me.TxtMoveToLog.Tag = ""
        '
        'CmdMoveToLog
        '
        Me.CmdMoveToLog.Size = New System.Drawing.Size(26, 19)
        '
        'GBoxApprove
        '
        Me.GBoxApprove.Location = New System.Drawing.Point(415, 493)
        Me.GBoxApprove.Size = New System.Drawing.Size(148, 40)
        '
        'TxtApproveBy
        '
        Me.TxtApproveBy.Location = New System.Drawing.Point(29, 19)
        Me.TxtApproveBy.Size = New System.Drawing.Size(116, 18)
        Me.TxtApproveBy.Tag = ""
        '
        'CmdDiscard
        '
        Me.CmdDiscard.Size = New System.Drawing.Size(26, 19)
        '
        'CmdApprove
        '
        Me.CmdApprove.Size = New System.Drawing.Size(26, 19)
        '
        'GBoxEntryType
        '
        Me.GBoxEntryType.Location = New System.Drawing.Point(150, 493)
        Me.GBoxEntryType.Size = New System.Drawing.Size(119, 40)
        '
        'TxtEntryType
        '
        Me.TxtEntryType.Location = New System.Drawing.Point(3, 19)
        Me.TxtEntryType.Tag = ""
        '
        'GrpUP
        '
        Me.GrpUP.Location = New System.Drawing.Point(16, 493)
        Me.GrpUP.Size = New System.Drawing.Size(119, 40)
        '
        'TxtEntryBy
        '
        Me.TxtEntryBy.Location = New System.Drawing.Point(3, 19)
        Me.TxtEntryBy.Tag = ""
        Me.TxtEntryBy.Text = ""
        '
        'GroupBox1
        '
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.GroupBox1.Location = New System.Drawing.Point(2, 489)
        Me.GroupBox1.Size = New System.Drawing.Size(907, 4)
        '
        'GBoxDivision
        '
        Me.GBoxDivision.Location = New System.Drawing.Point(285, 493)
        Me.GBoxDivision.Size = New System.Drawing.Size(114, 40)
        '
        'TxtDivision
        '
        Me.TxtDivision.AgSelectedValue = ""
        Me.TxtDivision.Location = New System.Drawing.Point(3, 19)
        Me.TxtDivision.Tag = ""
        '
        'TxtDocId
        '
        Me.TxtDocId.AgSelectedValue = ""
        Me.TxtDocId.BackColor = System.Drawing.Color.White
        Me.TxtDocId.Tag = ""
        Me.TxtDocId.Text = ""
        '
        'LblV_No
        '
        Me.LblV_No.Location = New System.Drawing.Point(348, 232)
        Me.LblV_No.Size = New System.Drawing.Size(78, 16)
        Me.LblV_No.Tag = ""
        Me.LblV_No.Text = "Transfer No."
        Me.LblV_No.Visible = False
        '
        'TxtV_No
        '
        Me.TxtV_No.AgSelectedValue = ""
        Me.TxtV_No.BackColor = System.Drawing.Color.White
        Me.TxtV_No.Location = New System.Drawing.Point(470, 231)
        Me.TxtV_No.Size = New System.Drawing.Size(217, 18)
        Me.TxtV_No.TabIndex = 3
        Me.TxtV_No.Tag = ""
        Me.TxtV_No.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.TxtV_No.Visible = False
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(295, 53)
        Me.Label2.Tag = ""
        '
        'LblV_Date
        '
        Me.LblV_Date.BackColor = System.Drawing.Color.Transparent
        Me.LblV_Date.Location = New System.Drawing.Point(186, 48)
        Me.LblV_Date.Tag = ""
        Me.LblV_Date.Text = "Issue Date"
        '
        'LblV_TypeReq
        '
        Me.LblV_TypeReq.Location = New System.Drawing.Point(525, 33)
        Me.LblV_TypeReq.Tag = ""
        '
        'TxtV_Date
        '
        Me.TxtV_Date.AgSelectedValue = ""
        Me.TxtV_Date.BackColor = System.Drawing.Color.White
        Me.TxtV_Date.Location = New System.Drawing.Point(313, 47)
        Me.TxtV_Date.Size = New System.Drawing.Size(120, 18)
        Me.TxtV_Date.TabIndex = 1
        Me.TxtV_Date.Tag = ""
        '
        'LblV_Type
        '
        Me.LblV_Type.Location = New System.Drawing.Point(439, 29)
        Me.LblV_Type.Tag = ""
        Me.LblV_Type.Text = "Issue Type"
        '
        'TxtV_Type
        '
        Me.TxtV_Type.AgSelectedValue = ""
        Me.TxtV_Type.BackColor = System.Drawing.Color.White
        Me.TxtV_Type.Location = New System.Drawing.Point(541, 27)
        Me.TxtV_Type.Size = New System.Drawing.Size(187, 18)
        Me.TxtV_Type.Tag = ""
        '
        'LblSite_CodeReq
        '
        Me.LblSite_CodeReq.Location = New System.Drawing.Point(295, 33)
        Me.LblSite_CodeReq.Tag = ""
        '
        'LblSite_Code
        '
        Me.LblSite_Code.BackColor = System.Drawing.Color.Transparent
        Me.LblSite_Code.Location = New System.Drawing.Point(186, 28)
        Me.LblSite_Code.Size = New System.Drawing.Size(87, 16)
        Me.LblSite_Code.Tag = ""
        Me.LblSite_Code.Text = "Branch Name"
        '
        'TxtSite_Code
        '
        Me.TxtSite_Code.AgSelectedValue = ""
        Me.TxtSite_Code.BackColor = System.Drawing.Color.White
        Me.TxtSite_Code.Location = New System.Drawing.Point(313, 27)
        Me.TxtSite_Code.Size = New System.Drawing.Size(120, 18)
        Me.TxtSite_Code.TabIndex = 0
        Me.TxtSite_Code.Tag = ""
        '
        'LblDocId
        '
        Me.LblDocId.Tag = ""
        '
        'LblPrefix
        '
        Me.LblPrefix.Location = New System.Drawing.Point(711, 192)
        Me.LblPrefix.Tag = ""
        Me.LblPrefix.Visible = False
        '
        'TabControl1
        '
        Me.TabControl1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl1.Location = New System.Drawing.Point(-9, 5)
        Me.TabControl1.Size = New System.Drawing.Size(907, 161)
        Me.TabControl1.TabIndex = 0
        '
        'TP1
        '
        Me.TP1.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.TP1.Controls.Add(Me.LblReqNoofPerson)
        Me.TP1.Controls.Add(Me.Label3)
        Me.TP1.Controls.Add(Me.TxtProcess)
        Me.TP1.Controls.Add(Me.Label5)
        Me.TP1.Controls.Add(Me.LblReq_SubCode)
        Me.TP1.Controls.Add(Me.TxtParty)
        Me.TP1.Controls.Add(Me.Label4)
        Me.TP1.Controls.Add(Me.Label1)
        Me.TP1.Controls.Add(Me.TxtManualRefNo)
        Me.TP1.Controls.Add(Me.LblManualRefNo)
        Me.TP1.Controls.Add(Me.LblFromGodownReq)
        Me.TP1.Controls.Add(Me.Label30)
        Me.TP1.Controls.Add(Me.TxtRemarks)
        Me.TP1.Controls.Add(Me.TxtFromGodown)
        Me.TP1.Controls.Add(Me.LblGodown)
        Me.TP1.Location = New System.Drawing.Point(4, 22)
        Me.TP1.Size = New System.Drawing.Size(899, 135)
        Me.TP1.Text = "Document Detail"
        Me.TP1.Controls.SetChildIndex(Me.TxtV_No, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtSite_Code, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_Date, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label2, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_No, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblSite_Code, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtV_Date, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblSite_CodeReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblGodown, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblPrefix, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtFromGodown, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_Type, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtRemarks, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label30, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_TypeReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtV_Type, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblDocId, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtDocId, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblFromGodownReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblManualRefNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtManualRefNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label1, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label4, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtParty, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblReq_SubCode, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label5, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtProcess, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label3, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblReqNoofPerson, 0)
        '
        'Topctrl1
        '
        Me.Topctrl1.Size = New System.Drawing.Size(889, 41)
        Me.Topctrl1.TabIndex = 0
        '
        'Dgl1
        '
        Me.Dgl1.AgAllowFind = True
        Me.Dgl1.AgLastColumn = -1
        Me.Dgl1.AgMandatoryColumn = 0
        Me.Dgl1.AgReadOnlyColumnColor = System.Drawing.Color.Ivory
        Me.Dgl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.Dgl1.AgSkipReadOnlyColumns = False
        Me.Dgl1.CancelEditingControlValidating = False
        Me.Dgl1.GridSearchMethod = AgControls.AgLib.TxtSearchMethod.Comprehensive
        Me.Dgl1.Location = New System.Drawing.Point(0, 0)
        Me.Dgl1.Name = "Dgl1"
        Me.Dgl1.Size = New System.Drawing.Size(240, 150)
        Me.Dgl1.TabIndex = 0
        '
        'TxtFromGodown
        '
        Me.TxtFromGodown.AgAllowUserToEnableMasterHelp = False
        Me.TxtFromGodown.AgLastValueTag = Nothing
        Me.TxtFromGodown.AgLastValueText = Nothing
        Me.TxtFromGodown.AgMandatory = True
        Me.TxtFromGodown.AgMasterHelp = False
        Me.TxtFromGodown.AgNumberLeftPlaces = 8
        Me.TxtFromGodown.AgNumberNegetiveAllow = False
        Me.TxtFromGodown.AgNumberRightPlaces = 2
        Me.TxtFromGodown.AgPickFromLastValue = False
        Me.TxtFromGodown.AgRowFilter = ""
        Me.TxtFromGodown.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtFromGodown.AgSelectedValue = Nothing
        Me.TxtFromGodown.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtFromGodown.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtFromGodown.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtFromGodown.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFromGodown.Location = New System.Drawing.Point(541, 87)
        Me.TxtFromGodown.MaxLength = 20
        Me.TxtFromGodown.Name = "TxtFromGodown"
        Me.TxtFromGodown.Size = New System.Drawing.Size(187, 18)
        Me.TxtFromGodown.TabIndex = 6
        '
        'LblGodown
        '
        Me.LblGodown.AutoSize = True
        Me.LblGodown.BackColor = System.Drawing.Color.Transparent
        Me.LblGodown.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblGodown.Location = New System.Drawing.Point(440, 87)
        Me.LblGodown.Name = "LblGodown"
        Me.LblGodown.Size = New System.Drawing.Size(55, 16)
        Me.LblGodown.TabIndex = 706
        Me.LblGodown.Text = "Godown"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Cornsilk
        Me.Panel1.Controls.Add(Me.LblTotalMeasure)
        Me.Panel1.Controls.Add(Me.Label33)
        Me.Panel1.Controls.Add(Me.LblTotalQty)
        Me.Panel1.Controls.Add(Me.LblTotalQtyText)
        Me.Panel1.Location = New System.Drawing.Point(5, 464)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(879, 23)
        Me.Panel1.TabIndex = 694
        '
        'LblTotalMeasure
        '
        Me.LblTotalMeasure.AutoSize = True
        Me.LblTotalMeasure.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalMeasure.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalMeasure.Location = New System.Drawing.Point(424, 3)
        Me.LblTotalMeasure.Name = "LblTotalMeasure"
        Me.LblTotalMeasure.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalMeasure.TabIndex = 666
        Me.LblTotalMeasure.Text = "."
        Me.LblTotalMeasure.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.ForeColor = System.Drawing.Color.Maroon
        Me.Label33.Location = New System.Drawing.Point(313, 3)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(105, 16)
        Me.Label33.TabIndex = 665
        Me.Label33.Text = "Total Measure :"
        '
        'LblTotalQty
        '
        Me.LblTotalQty.AutoSize = True
        Me.LblTotalQty.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalQty.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalQty.Location = New System.Drawing.Point(116, 3)
        Me.LblTotalQty.Name = "LblTotalQty"
        Me.LblTotalQty.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalQty.TabIndex = 660
        Me.LblTotalQty.Text = "."
        Me.LblTotalQty.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTotalQtyText
        '
        Me.LblTotalQtyText.AutoSize = True
        Me.LblTotalQtyText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalQtyText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalQtyText.Location = New System.Drawing.Point(31, 3)
        Me.LblTotalQtyText.Name = "LblTotalQtyText"
        Me.LblTotalQtyText.Size = New System.Drawing.Size(72, 16)
        Me.LblTotalQtyText.TabIndex = 659
        Me.LblTotalQtyText.Text = "Total Qty :"
        '
        'Pnl1
        '
        Me.Pnl1.Location = New System.Drawing.Point(4, 190)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(880, 274)
        Me.Pnl1.TabIndex = 1
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(186, 108)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(60, 16)
        Me.Label30.TabIndex = 723
        Me.Label30.Text = "Remarks"
        '
        'TxtRemarks
        '
        Me.TxtRemarks.AgAllowUserToEnableMasterHelp = False
        Me.TxtRemarks.AgLastValueTag = Nothing
        Me.TxtRemarks.AgLastValueText = Nothing
        Me.TxtRemarks.AgMandatory = False
        Me.TxtRemarks.AgMasterHelp = False
        Me.TxtRemarks.AgNumberLeftPlaces = 0
        Me.TxtRemarks.AgNumberNegetiveAllow = False
        Me.TxtRemarks.AgNumberRightPlaces = 0
        Me.TxtRemarks.AgPickFromLastValue = False
        Me.TxtRemarks.AgRowFilter = ""
        Me.TxtRemarks.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtRemarks.AgSelectedValue = Nothing
        Me.TxtRemarks.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtRemarks.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtRemarks.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtRemarks.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtRemarks.Location = New System.Drawing.Point(313, 107)
        Me.TxtRemarks.MaxLength = 255
        Me.TxtRemarks.Name = "TxtRemarks"
        Me.TxtRemarks.Size = New System.Drawing.Size(415, 18)
        Me.TxtRemarks.TabIndex = 7
        '
        'LblFromGodownReq
        '
        Me.LblFromGodownReq.AutoSize = True
        Me.LblFromGodownReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblFromGodownReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblFromGodownReq.Location = New System.Drawing.Point(525, 94)
        Me.LblFromGodownReq.Name = "LblFromGodownReq"
        Me.LblFromGodownReq.Size = New System.Drawing.Size(10, 7)
        Me.LblFromGodownReq.TabIndex = 724
        Me.LblFromGodownReq.Text = "Ä"
        '
        'TxtManualRefNo
        '
        Me.TxtManualRefNo.AgAllowUserToEnableMasterHelp = False
        Me.TxtManualRefNo.AgLastValueTag = Nothing
        Me.TxtManualRefNo.AgLastValueText = Nothing
        Me.TxtManualRefNo.AgMandatory = True
        Me.TxtManualRefNo.AgMasterHelp = False
        Me.TxtManualRefNo.AgNumberLeftPlaces = 8
        Me.TxtManualRefNo.AgNumberNegetiveAllow = False
        Me.TxtManualRefNo.AgNumberRightPlaces = 2
        Me.TxtManualRefNo.AgPickFromLastValue = False
        Me.TxtManualRefNo.AgRowFilter = ""
        Me.TxtManualRefNo.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtManualRefNo.AgSelectedValue = Nothing
        Me.TxtManualRefNo.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtManualRefNo.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtManualRefNo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtManualRefNo.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtManualRefNo.Location = New System.Drawing.Point(541, 47)
        Me.TxtManualRefNo.MaxLength = 50
        Me.TxtManualRefNo.Name = "TxtManualRefNo"
        Me.TxtManualRefNo.Size = New System.Drawing.Size(187, 18)
        Me.TxtManualRefNo.TabIndex = 3
        '
        'LblManualRefNo
        '
        Me.LblManualRefNo.AutoSize = True
        Me.LblManualRefNo.BackColor = System.Drawing.Color.Transparent
        Me.LblManualRefNo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblManualRefNo.Location = New System.Drawing.Point(439, 47)
        Me.LblManualRefNo.Name = "LblManualRefNo"
        Me.LblManualRefNo.Size = New System.Drawing.Size(67, 16)
        Me.LblManualRefNo.TabIndex = 731
        Me.LblManualRefNo.Text = "Issue. No."
        '
        'LblMaterialPlanForFollowingItems
        '
        Me.LblMaterialPlanForFollowingItems.BackColor = System.Drawing.Color.SteelBlue
        Me.LblMaterialPlanForFollowingItems.DisabledLinkColor = System.Drawing.Color.White
        Me.LblMaterialPlanForFollowingItems.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblMaterialPlanForFollowingItems.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LblMaterialPlanForFollowingItems.LinkColor = System.Drawing.Color.White
        Me.LblMaterialPlanForFollowingItems.Location = New System.Drawing.Point(4, 168)
        Me.LblMaterialPlanForFollowingItems.Name = "LblMaterialPlanForFollowingItems"
        Me.LblMaterialPlanForFollowingItems.Size = New System.Drawing.Size(107, 19)
        Me.LblMaterialPlanForFollowingItems.TabIndex = 804
        Me.LblMaterialPlanForFollowingItems.TabStop = True
        Me.LblMaterialPlanForFollowingItems.Text = "Issued Items"
        Me.LblMaterialPlanForFollowingItems.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(525, 53)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(10, 7)
        Me.Label1.TabIndex = 732
        Me.Label1.Text = "Ä"
        '
        'LblReq_SubCode
        '
        Me.LblReq_SubCode.AutoSize = True
        Me.LblReq_SubCode.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblReq_SubCode.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblReq_SubCode.Location = New System.Drawing.Point(295, 74)
        Me.LblReq_SubCode.Name = "LblReq_SubCode"
        Me.LblReq_SubCode.Size = New System.Drawing.Size(10, 7)
        Me.LblReq_SubCode.TabIndex = 735
        Me.LblReq_SubCode.Text = "Ä"
        '
        'TxtParty
        '
        Me.TxtParty.AgAllowUserToEnableMasterHelp = False
        Me.TxtParty.AgLastValueTag = Nothing
        Me.TxtParty.AgLastValueText = Nothing
        Me.TxtParty.AgMandatory = True
        Me.TxtParty.AgMasterHelp = False
        Me.TxtParty.AgNumberLeftPlaces = 8
        Me.TxtParty.AgNumberNegetiveAllow = False
        Me.TxtParty.AgNumberRightPlaces = 2
        Me.TxtParty.AgPickFromLastValue = False
        Me.TxtParty.AgRowFilter = ""
        Me.TxtParty.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtParty.AgSelectedValue = Nothing
        Me.TxtParty.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtParty.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtParty.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtParty.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtParty.Location = New System.Drawing.Point(313, 67)
        Me.TxtParty.MaxLength = 20
        Me.TxtParty.Name = "TxtParty"
        Me.TxtParty.Size = New System.Drawing.Size(415, 18)
        Me.TxtParty.TabIndex = 4
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(186, 69)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(109, 16)
        Me.Label4.TabIndex = 734
        Me.Label4.Text = "Issue To (Person)"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(295, 94)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(10, 7)
        Me.Label3.TabIndex = 738
        Me.Label3.Text = "Ä"
        '
        'TxtProcess
        '
        Me.TxtProcess.AgAllowUserToEnableMasterHelp = False
        Me.TxtProcess.AgLastValueTag = Nothing
        Me.TxtProcess.AgLastValueText = Nothing
        Me.TxtProcess.AgMandatory = True
        Me.TxtProcess.AgMasterHelp = False
        Me.TxtProcess.AgNumberLeftPlaces = 8
        Me.TxtProcess.AgNumberNegetiveAllow = False
        Me.TxtProcess.AgNumberRightPlaces = 2
        Me.TxtProcess.AgPickFromLastValue = False
        Me.TxtProcess.AgRowFilter = ""
        Me.TxtProcess.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtProcess.AgSelectedValue = Nothing
        Me.TxtProcess.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtProcess.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtProcess.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtProcess.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtProcess.Location = New System.Drawing.Point(313, 87)
        Me.TxtProcess.MaxLength = 20
        Me.TxtProcess.Name = "TxtProcess"
        Me.TxtProcess.Size = New System.Drawing.Size(121, 18)
        Me.TxtProcess.TabIndex = 5
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(186, 88)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(56, 16)
        Me.Label5.TabIndex = 737
        Me.Label5.Text = "Process"
        '
        'LblReqNoofPerson
        '
        Me.LblReqNoofPerson.AutoSize = True
        Me.LblReqNoofPerson.BackColor = System.Drawing.Color.Transparent
        Me.LblReqNoofPerson.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblReqNoofPerson.ForeColor = System.Drawing.Color.Red
        Me.LblReqNoofPerson.Location = New System.Drawing.Point(738, 67)
        Me.LblReqNoofPerson.Name = "LblReqNoofPerson"
        Me.LblReqNoofPerson.Size = New System.Drawing.Size(85, 15)
        Me.LblReqNoofPerson.TabIndex = 739
        Me.LblReqNoofPerson.Text = "From Godown"
        '
        'BtnFillIssueDetail
        '
        Me.BtnFillIssueDetail.BackColor = System.Drawing.Color.Transparent
        Me.BtnFillIssueDetail.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnFillIssueDetail.Font = New System.Drawing.Font("Verdana", 6.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnFillIssueDetail.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnFillIssueDetail.Location = New System.Drawing.Point(451, 170)
        Me.BtnFillIssueDetail.Margin = New System.Windows.Forms.Padding(0)
        Me.BtnFillIssueDetail.Name = "BtnFillIssueDetail"
        Me.BtnFillIssueDetail.Size = New System.Drawing.Size(51, 19)
        Me.BtnFillIssueDetail.TabIndex = 806
        Me.BtnFillIssueDetail.TabStop = False
        Me.BtnFillIssueDetail.Text = "...."
        Me.BtnFillIssueDetail.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnFillIssueDetail.UseVisualStyleBackColor = False
        '
        'GrpDirectIssue
        '
        Me.GrpDirectIssue.BackColor = System.Drawing.Color.Transparent
        Me.GrpDirectIssue.Controls.Add(Me.RbtIssueDirect)
        Me.GrpDirectIssue.Controls.Add(Me.RbtIssueForReqisition)
        Me.GrpDirectIssue.Location = New System.Drawing.Point(116, 161)
        Me.GrpDirectIssue.Name = "GrpDirectIssue"
        Me.GrpDirectIssue.Size = New System.Drawing.Size(326, 28)
        Me.GrpDirectIssue.TabIndex = 805
        Me.GrpDirectIssue.TabStop = False
        '
        'RbtIssueDirect
        '
        Me.RbtIssueDirect.AutoSize = True
        Me.RbtIssueDirect.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RbtIssueDirect.Location = New System.Drawing.Point(15, 8)
        Me.RbtIssueDirect.Name = "RbtIssueDirect"
        Me.RbtIssueDirect.Size = New System.Drawing.Size(104, 17)
        Me.RbtIssueDirect.TabIndex = 743
        Me.RbtIssueDirect.TabStop = True
        Me.RbtIssueDirect.Text = "Issue Direct"
        Me.RbtIssueDirect.UseVisualStyleBackColor = True
        '
        'RbtIssueForReqisition
        '
        Me.RbtIssueForReqisition.AutoSize = True
        Me.RbtIssueForReqisition.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RbtIssueForReqisition.Location = New System.Drawing.Point(144, 8)
        Me.RbtIssueForReqisition.Name = "RbtIssueForReqisition"
        Me.RbtIssueForReqisition.Size = New System.Drawing.Size(159, 17)
        Me.RbtIssueForReqisition.TabIndex = 0
        Me.RbtIssueForReqisition.TabStop = True
        Me.RbtIssueForReqisition.Text = "IssueFor Requisition"
        Me.RbtIssueForReqisition.UseVisualStyleBackColor = True
        '
        'FrmStoreIssue
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.BackColor = System.Drawing.SystemColors.ButtonShadow
        Me.ClientSize = New System.Drawing.Size(889, 534)
        Me.Controls.Add(Me.BtnFillIssueDetail)
        Me.Controls.Add(Me.GrpDirectIssue)
        Me.Controls.Add(Me.LblMaterialPlanForFollowingItems)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Pnl1)
        Me.Name = "FrmStoreIssue"
        Me.Text = "Material Issue from Store Entry"
        Me.Controls.SetChildIndex(Me.TabControl1, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.GrpUP, 0)
        Me.Controls.SetChildIndex(Me.GBoxEntryType, 0)
        Me.Controls.SetChildIndex(Me.GBoxApprove, 0)
        Me.Controls.SetChildIndex(Me.GBoxMoveToLog, 0)
        Me.Controls.SetChildIndex(Me.GroupBox2, 0)
        Me.Controls.SetChildIndex(Me.Pnl1, 0)
        Me.Controls.SetChildIndex(Me.Panel1, 0)
        Me.Controls.SetChildIndex(Me.GBoxDivision, 0)
        Me.Controls.SetChildIndex(Me.LblMaterialPlanForFollowingItems, 0)
        Me.Controls.SetChildIndex(Me.Topctrl1, 0)
        Me.Controls.SetChildIndex(Me.GrpDirectIssue, 0)
        Me.Controls.SetChildIndex(Me.BtnFillIssueDetail, 0)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GBoxMoveToLog.ResumeLayout(False)
        Me.GBoxMoveToLog.PerformLayout()
        Me.GBoxApprove.ResumeLayout(False)
        Me.GBoxApprove.PerformLayout()
        Me.GBoxEntryType.ResumeLayout(False)
        Me.GBoxEntryType.PerformLayout()
        Me.GrpUP.ResumeLayout(False)
        Me.GrpUP.PerformLayout()
        Me.GBoxDivision.ResumeLayout(False)
        Me.GBoxDivision.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TP1.ResumeLayout(False)
        Me.TP1.PerformLayout()
        CType(Me.DTMaster, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.Dgl1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GrpDirectIssue.ResumeLayout(False)
        Me.GrpDirectIssue.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Protected WithEvents TxtFromGodown As AgControls.AgTextBox
    Protected WithEvents LblGodown As System.Windows.Forms.Label
    Protected WithEvents Panel1 As System.Windows.Forms.Panel
    Protected WithEvents LblTotalQty As System.Windows.Forms.Label
    Protected WithEvents LblTotalQtyText As System.Windows.Forms.Label
    Protected WithEvents Pnl1 As System.Windows.Forms.Panel
    Protected WithEvents LblTotalMeasure As System.Windows.Forms.Label
    Protected WithEvents TxtRemarks As AgControls.AgTextBox
    Protected WithEvents Label30 As System.Windows.Forms.Label
    Protected WithEvents LblFromGodownReq As System.Windows.Forms.Label
    Protected WithEvents Label33 As System.Windows.Forms.Label
    Protected WithEvents TxtManualRefNo As AgControls.AgTextBox
    Protected WithEvents LblManualRefNo As System.Windows.Forms.Label
    Protected WithEvents LblMaterialPlanForFollowingItems As System.Windows.Forms.LinkLabel
    Protected WithEvents Label1 As System.Windows.Forms.Label
#End Region

    Private Sub Frm_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "StockHead"
        LogTableName = "StockHead_Log"
        MainLineTableCsv = "Stock,StockHeadDetail"
        LogLineTableCsv = "Stock_LOG,StockHeadDetail_Log"
        AgL.GridDesign(Dgl1)
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mCondStr$
        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) & _
                        " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "H.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        AgL.PubFindQry = " SELECT H.DocID AS SearchCode, H.V_Type AS [Issue_Type], H.V_Date AS Date, " & _
                " H.ManualRefNo, P.Description as Process, Sg.Name as Party_Name,  " & _
                " H.Remarks,  H.EntryBy AS [Entry_By], H.EntryDate AS [Entry_Date], H.EntryType AS [Entry_Type]  " & _
                " FROM  StockHead H  " & _
                " LEFT JOIN Division D ON D.Div_Code=H.Div_Code  " & _
                " LEFT JOIN Process P ON H.Process=P.NCat  " & _
                " LEFT JOIN Subgroup Sg ON H.SubCode=Sg.SubCode  " & _
                " LEFT JOIN SiteMast SM ON SM.Code=H.Site_Code  " & _
                " LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type " & _
                " LEFT JOIN Godown GF ON GF.Code = H.FromGodown  " & _
                " LEFT JOIN Godown GT ON GT.Code = H.ToGodown  " & _
                " Where IsNull(H.IsDeleted,0) = 0  " & mCondStr

        AgL.PubFindQryOrdBy = "[Entry Date]"
    End Sub

    Private Sub FrmQuality1_BaseFunction_FIniMast(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMast
        Dim mCondStr$
        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) & _
                        " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "H.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        mQry = " Select H.DocID As SearchCode " & _
            " From StockHead H " & _
            " Left Join Voucher_Type Vt On H.V_Type = Vt.V_Type  " & _
            " Where IsNull(IsDeleted,0) = 0  " & mCondStr & "  Order By H.V_Date Desc "

        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub FrmSaleOrder_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl1, Col1Item_UID, 100, 0, Col1Item_UID, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_ItemUID")), Boolean), False, False)
            .AddAgTextColumn(Dgl1, Col1ItemCode, 100, 0, Col1ItemCode, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_ItemCode")), Boolean), False, False)
            .AddAgTextColumn(Dgl1, Col1Item, 250, 0, Col1Item, True, False, False)
            .AddAgTextColumn(Dgl1, Col1ProdOrder, 100, 0, Col1ProdOrder, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_ProdOrder")), Boolean), False, False)
            .AddAgTextColumn(Dgl1, Col1Process, 100, 0, Col1Process, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_ProcessLine")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_ProcessLine")), Boolean), False)
            .AddAgTextColumn(Dgl1, Col1LotNo, 100, 0, Col1LotNo, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_LotNo")), Boolean), False, False)
            .AddAgTextColumn(Dgl1, Col1BaleNo, 100, 0, Col1BaleNo, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_BaleNo")), Boolean), False, False)
            .AddAgTextColumn(Dgl1, Col1RequisitionNo, 80, 0, Col1RequisitionNo, True, True, False)
            .AddAgNumberColumn(Dgl1, Col1RequisitionSr, 50, 8, 4, False, Col1RequisitionSr, False, True, True)
            .AddAgNumberColumn(Dgl1, Col1CurrentStock, 80, 8, 4, False, Col1CurrentStock, True, True, True)
            .AddAgNumberColumn(Dgl1, Col1Qty, 100, 8, 4, False, Col1Qty, True, False, True)
            .AddAgTextColumn(Dgl1, Col1Unit, 50, 0, Col1Unit, True, True, False)
            .AddAgTextColumn(Dgl1, Col1QtyDecimalPlaces, 50, 0, Col1QtyDecimalPlaces, False, True, False)
            .AddAgNumberColumn(Dgl1, Col1MeasurePerPcs, 70, 8, 3, False, Col1MeasurePerPcs, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_MeasurePerPcs")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_MeasurePerPcs")), Boolean), True)
            .AddAgNumberColumn(Dgl1, Col1TotalMeasure, 70, 8, 3, False, Col1TotalMeasure, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Measure")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_Measure")), Boolean), True)
            .AddAgTextColumn(Dgl1, Col1MeasureUnit, 50, 0, Col1MeasureUnit, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_MeasureUnit")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_MeasureUnit")), Boolean))
            .AddAgTextColumn(Dgl1, Col1MeasureDecimalPlaces, 50, 0, Col1MeasureDecimalPlaces, False, True, False)
            .AddAgNumberColumn(Dgl1, Col1Rate, 90, 8, 2, False, Col1Rate, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Rate")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_Rate")), Boolean), True)
            .AddAgNumberColumn(Dgl1, Col1Amount, 90, 8, 2, False, Col1Amount, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Amount")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_Amount")), Boolean), True)
            .AddAgTextColumn(Dgl1, Col1Remarks, 250, 0, Col1Remarks, True, False, False)
            .AddAgTextColumn(Dgl1, Col1VNature, 100, 0, Col1VNature, True, True, False)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False

        Dgl1.ColumnHeadersHeight = 35

        Dgl1.AgSkipReadOnlyColumns = True
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As System.Data.SqlClient.SqlConnection, ByVal Cmd As System.Data.SqlClient.SqlCommand) Handles Me.BaseEvent_Save_InTrans
        Dim I As Integer, mSr As Integer

        mQry = "UPDATE StockHead " & _
                " SET " & _
                " TotalQty = " & Val(LblTotalQty.Text) & ", " & _
                " TotalMeasure = " & Val(LblTotalMeasure.Text) & ", " & _
                " SubCode = " & AgL.Chk_Text(TxtParty.Tag) & ", " & _
                " FromGodown = " & AgL.Chk_Text(TxtFromGodown.Tag) & ", " & _
                " Process = " & AgL.Chk_Text(TxtProcess.Tag) & ", " & _
                " ManualRefNo = " & AgL.Chk_Text(TxtManualRefNo.Text) & ", " & _
                " Remarks = " & AgL.Chk_Text(TxtRemarks.Text) & " " & _
                " Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Delete From StockHeadDetail Where DocId = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Delete From Stock Where DocId = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        If CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsPostedInStockProcess")), Boolean) Then
            mQry = "Delete From StockProcess Where DocId = '" & SearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If


        'Never Try to Serialise Sr in Line Items 
        'As Some other Entry points may updating values to this Search code and Sr
        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1Item, I).Value <> "" Then
                mSr += 1

                mQry = " INSERT INTO dbo.StockHeadDetail ( DocID, Sr, Item_UID, Item, LotNo, BaleNo, Godown, Qty, Unit, " & _
                        " MeasurePerPcs, TotalMeasure, MeasureUnit, Rate, Amount, Remarks, Process, " & _
                        " Requisition, RequisitionSr, CurrentStock, V_Nature ) " & _
                        " VALUES (" & AgL.Chk_Text(mSearchCode) & ", " & _
                        " " & mSr & ", " & _
                        " " & AgL.Chk_Text(Dgl1.Item(Col1Item_UID, I).Tag) & ", " & _
                        " " & AgL.Chk_Text(Dgl1.AgSelectedValue(Col1Item, I)) & ", " & _
                        " " & AgL.Chk_Text(Dgl1.Item(Col1LotNo, I).Value) & ",  " & _
                        " " & AgL.Chk_Text(Dgl1.Item(Col1BaleNo, I).Value) & ",  " & _
                        " " & AgL.Chk_Text(TxtFromGodown.AgSelectedValue) & ", " & _
                        " " & Val(Dgl1.Item(Col1Qty, I).Value) & ", " & _
                        " " & AgL.Chk_Text(Dgl1.Item(Col1Unit, I).Value) & ",  " & _
                        " " & Val(Dgl1.Item(Col1MeasurePerPcs, I).Value) & ", " & _
                        " " & Val(Dgl1.Item(Col1TotalMeasure, I).Value) & ", " & AgL.Chk_Text(Dgl1.Item(Col1MeasureUnit, I).Value) & ", " & _
                        " " & Val(Dgl1.Item(Col1Rate, I).Value) & ", " & _
                        " " & Val(Dgl1.Item(Col1Amount, I).Value) & ", " & _
                        " " & AgL.Chk_Text(Dgl1.Item(Col1Remarks, I).Value) & ", " & _
                        " " & AgL.Chk_Text(IIf(TxtProcess.Text = "", Dgl1.Item(Col1Process, I).Tag, TxtProcess.Tag)) & ", " & _
                        " " & AgL.Chk_Text(Dgl1.Item(Col1RequisitionNo, I).Tag) & ", " & Val(Dgl1.Item(Col1RequisitionSr, I).Value) & ", " & _
                        " " & Val(Dgl1.Item(Col1CurrentStock, I).Value) & ", " & _
                        " " & AgL.Chk_Text(Dgl1.Item(Col1VNature, I).Value) & " ) "
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                mQry = " INSERT INTO Stock (DocId, Sr, V_Type, V_Prefix, " & _
                        " V_Date, V_No, RecID, Div_Code, Site_Code, SubCode, " & _
                        " Item_UID, Item, Godown, Qty_Iss, Unit, " & _
                        " MeasurePerPcs, Measure_Iss, MeasureUnit, " & _
                        " Rate, Amount,NetAmount, Cost, LotNo, BaleNo, Process, Remarks,CurrentStock) " & _
                        " VALUES (" & AgL.Chk_Text(mSearchCode) & ", " & _
                        " " & mSr & ", " & AgL.Chk_Text(TxtV_Type.AgSelectedValue) & ", " & AgL.Chk_Text(LblPrefix.Text) & ", " & _
                        " " & AgL.Chk_Text(TxtV_Date.Text) & ", " & Val(TxtV_No.Text) & ", " & AgL.Chk_Text(TxtManualRefNo.Text) & ", " & _
                        " " & AgL.Chk_Text(TxtDivision.AgSelectedValue) & ", " & AgL.Chk_Text(TxtSite_Code.AgSelectedValue) & ",  " & _
                        " " & AgL.Chk_Text(TxtParty.Tag) & ", " & _
                        " " & AgL.Chk_Text(Dgl1.Item(Col1Item_UID, I).Tag) & ", " & _
                        " " & AgL.Chk_Text(Dgl1.AgSelectedValue(Col1Item, I)) & ", " & _
                        " " & AgL.Chk_Text(TxtFromGodown.AgSelectedValue) & ", " & _
                        " " & Val(Dgl1.Item(Col1Qty, I).Value) & ", " & _
                        " " & AgL.Chk_Text(Dgl1.Item(Col1Unit, I).Value) & ",  " & _
                        " " & Val(Dgl1.Item(Col1MeasurePerPcs, I).Value) & ", " & _
                        " " & Val(Dgl1.Item(Col1TotalMeasure, I).Value) & ", " & AgL.Chk_Text(Dgl1.Item(Col1MeasureUnit, I).Value) & ", " & _
                        " " & Val(Dgl1.Item(Col1Rate, I).Value) & ", " & _
                        " " & Val(Dgl1.Item(Col1Amount, I).Value) & ", " & Val(Dgl1.Item(Col1Amount, I).Value) & ", " & Val(Dgl1.Item(Col1Amount, I).Value) & ", " & _
                        " " & AgL.Chk_Text(Dgl1.Item(Col1LotNo, I).Value) & ",  " & _
                        " " & AgL.Chk_Text(Dgl1.Item(Col1BaleNo, I).Value) & ",  " & _
                        " " & AgL.Chk_Text(IIf(TxtProcess.Text = "", Dgl1.Item(Col1Process, I).Tag, TxtProcess.Tag)) & ", " & _
                        " " & AgL.Chk_Text(Dgl1.Item(Col1Remarks, I).Value) & ", " & Val(Dgl1.Item(Col1CurrentStock, I).Value) & " ) "
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                If CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsPostedInStockProcess")), Boolean) Then

                    mQry = "INSERT INTO StockProcess (DocId, Sr, V_Type, V_Prefix, " & _
                            " V_Date, V_No, RecID, Div_Code, Site_Code, SubCode, " & _
                            " Item_UID, Item, Godown, Qty_Rec, Unit, " & _
                            " MeasurePerPcs, Measure_Rec, MeasureUnit, " & _
                            "Rate, Amount,NetAmount, Cost, LotNo, BaleNo, Process, Remarks,CurrentStock) " & _
                            " VALUES (" & AgL.Chk_Text(mSearchCode) & ", " & _
                            " " & mSr & ", " & AgL.Chk_Text(TxtV_Type.AgSelectedValue) & ", " & AgL.Chk_Text(LblPrefix.Text) & ", " & _
                            " " & AgL.Chk_Text(TxtV_Date.Text) & ", " & Val(TxtV_No.Text) & ", " & AgL.Chk_Text(TxtManualRefNo.Text) & ", " & _
                            " " & AgL.Chk_Text(TxtDivision.AgSelectedValue) & ", " & AgL.Chk_Text(TxtSite_Code.AgSelectedValue) & ",  " & _
                            " " & AgL.Chk_Text(TxtParty.Tag) & ", " & _
                            " " & AgL.Chk_Text(Dgl1.Item(Col1Item_UID, I).Tag) & ", " & _
                            " " & AgL.Chk_Text(Dgl1.AgSelectedValue(Col1Item, I)) & ", " & _
                            " " & AgL.Chk_Text(TxtFromGodown.AgSelectedValue) & ", " & _
                            " " & Val(Dgl1.Item(Col1Qty, I).Value) & ", " & _
                            " " & AgL.Chk_Text(Dgl1.Item(Col1Unit, I).Value) & ",  " & _
                            " " & Val(Dgl1.Item(Col1MeasurePerPcs, I).Value) & ", " & _
                            " " & Val(Dgl1.Item(Col1TotalMeasure, I).Value) & ", " & AgL.Chk_Text(Dgl1.Item(Col1MeasureUnit, I).Value) & ", " & _
                            " " & Val(Dgl1.Item(Col1Rate, I).Value) & ", " & _
                            " " & Val(Dgl1.Item(Col1Amount, I).Value) & ", " & Val(Dgl1.Item(Col1Amount, I).Value) & ", " & Val(Dgl1.Item(Col1Amount, I).Value) & ", " & _
                            " " & AgL.Chk_Text(Dgl1.Item(Col1LotNo, I).Value) & ",  " & _
                            " " & AgL.Chk_Text(Dgl1.Item(Col1BaleNo, I).Value) & ",  " & _
                            " " & AgL.Chk_Text(Dgl1.Item(Col1Process, I).Tag) & ", " & _
                            " " & AgL.Chk_Text(Dgl1.Item(Col1Remarks, I).Value) & ", " & Val(Dgl1.Item(Col1CurrentStock, I).Value) & " ) "
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                End If
            End If
        Next
    End Sub

    Private Sub FrmProductionOrder_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim I As Integer

        Dim DsTemp As DataSet

        mQry = "Select H.*, G.Description as FromGodownDesc, P.Description as ProcessDesc, " & _
               " Sg.Name + (Case When IsNull(Sg.CityCode,'')<>'' Then ', ' + C.CityName Else '' End) as PartyName " & _
                " From StockHead H " & _
                " Left Join Godown G on H.FromGodown = G.Code " & _
                " Left Join Subgroup Sg on H.SubCode = Sg.SubCode " & _
                " Left Join City C on Sg.CityCode = C.CityCode " & _
                " Left Join Process P on H.Process = P.NCat " & _
                " Where H.DocID='" & SearchCode & "'"
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then

                TxtFromGodown.Tag = AgL.XNull(.Rows(0)("FromGodown"))
                TxtFromGodown.Text = AgL.XNull(.Rows(0)("FromGodownDesc"))
                TxtProcess.Tag = AgL.XNull(.Rows(0)("Process"))
                TxtProcess.Text = AgL.XNull(.Rows(0)("ProcessDesc"))
                TxtParty.Tag = AgL.XNull(.Rows(0)("SubCode"))
                TxtParty.Text = AgL.XNull(.Rows(0)("PartyName"))
                TxtManualRefNo.Text = AgL.XNull(.Rows(0)("ManualRefNo"))
                TxtRemarks.Text = AgL.XNull(.Rows(0)("Remarks"))
                LblTotalQty.Text = AgL.VNull(.Rows(0)("TotalQty"))
                LblTotalMeasure.Text = AgL.VNull(.Rows(0)("TotalMeasure"))
                IniGrid()
                '-------------------------------------------------------------
                'Line Records are showing in Grid
                '-------------------------------------------------------------

                mQry = "Select S.*, I.ManualCode as Item_No, I.Description as Item_Desc, R.ReferenceNo AS ReqNo, " & _
                       "U.DecimalPlaces as QtyDecimalPlaces, MU.DecimalPlaces as MeasureDecimalPlaces, " & _
                       "P.Description as Process_Desc " & _
                       "from (Select * From StockHeadDetail where DocId = '" & SearchCode & "') S " & _
                       "Left Join Item I On S.Item = I.Code " & _
                       "Left Join Unit U On I.Unit = U.Code " & _
                       "Left Join Unit MU On I.MeasureUnit = MU.Code " & _
                       "Left Join Requisition R On S.Requisition = R.DocID " & _
                       "Left Join Process P On S.Process = P.NCat " & _
                       "Order By Sr"
                DsTemp = AgL.FillData(mQry, AgL.GCn)
                With DsTemp.Tables(0)
                    Dgl1.RowCount = 1
                    Dgl1.Rows.Clear()
                    If .Rows.Count > 0 Then
                        For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                            Dgl1.Rows.Add()
                            Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count - 1
                            Dgl1.Item(Col1Item_UID, I).Value = AgL.XNull(.Rows(I)("Item_UID"))
                            Dgl1.Item(Col1ItemCode, I).Tag = AgL.XNull(.Rows(I)("Item"))
                            Dgl1.Item(Col1ItemCode, I).Value = AgL.XNull(.Rows(I)("Item_No"))
                            Dgl1.Item(Col1Item, I).Tag = AgL.XNull(.Rows(I)("Item"))
                            Dgl1.Item(Col1Item, I).Value = AgL.XNull(.Rows(I)("Item_Desc"))
                            ' Dgl1.Item(Col1ProdOrder, I).Tag = AgL.XNull(.Rows(I)("ProdOrder"))
                            'Dgl1.Item(Col1ProdOrder, I).Value = AgL.XNull(.Rows(I)("ProdOrderNo"))
                            Dgl1.Item(Col1RequisitionNo, I).Tag = AgL.XNull(.Rows(I)("Requisition"))
                            Dgl1.Item(Col1RequisitionNo, I).Value = AgL.XNull(.Rows(I)("ReqNo"))
                            Dgl1.Item(Col1RequisitionSr, I).Value = AgL.VNull(.Rows(I)("RequisitionSr"))
                            Dgl1.Item(Col1VNature, I).Value = AgL.XNull(.Rows(I)("V_Nature"))

                            Dgl1.Item(Col1Qty, I).Value = Format(AgL.VNull(.Rows(I)("Qty")), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                            Dgl1.Item(Col1QtyDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("QtyDecimalPlaces"))
                            Dgl1.Item(Col1MeasurePerPcs, I).Value = Format(AgL.VNull(.Rows(I)("MeasurePerPcs")), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1TotalMeasure, I).Value = Format(AgL.VNull(.Rows(I)("TotalMeasure")), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1MeasureUnit, I).Value = AgL.XNull(.Rows(I)("MeasureUnit"))
                            Dgl1.Item(Col1MeasureDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("MeasureDecimalPlaces"))
                            Dgl1.Item(Col1Rate, I).Value = AgL.VNull(.Rows(I)("Rate"))
                            Dgl1.Item(Col1Amount, I).Value = AgL.VNull(.Rows(I)("Amount"))
                            'Dgl1.Item(Col1CurrentStock, I).Value = Format(AgTemplate.ClsMain.FunRetStock(Dgl1.AgSelectedValue(Col1Item, I), mSearchCode, , , , , TxtV_Date.Text), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1LotNo, I).Value = AgL.XNull(.Rows(I)("LotNo"))
                            Dgl1.Item(Col1BaleNo, I).Value = AgL.XNull(.Rows(I)("BaleNo"))
                            Dgl1.Item(Col1Process, I).Tag = AgL.XNull(.Rows(I)("Process"))
                            Dgl1.Item(Col1Process, I).Value = AgL.XNull(.Rows(I)("Process_Desc"))
                            Dgl1.Item(Col1Remarks, I).Value = AgL.XNull(.Rows(I)("Remarks"))
                            Dgl1.Item(Col1CurrentStock, I).Value = AgL.VNull(.Rows(I)("CurrentStock"))
                        Next I
                    End If
                End With
                Calculation()
                '-------------------------------------------------------------
            End If
        End With
    End Sub

    Private Sub FrmProductionOrder_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Topctrl1.ChangeAgGridState(Dgl1, False)
    End Sub

    Private Sub TxtFromGodown_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtFromGodown.KeyDown, TxtParty.KeyDown
        Select Case sender.Name
            Case TxtFromGodown.Name
                If e.KeyCode <> Keys.Enter Then
                    If sender.AgHelpDataset Is Nothing Then
                        mQry = "SELECT G.Code, G.Description " & _
                                " FROM Godown G " & _
                                " LEFT JOIN SiteMast Sm On G.Site_Code = Sm.Code  " & _
                                " Where G.Div_Code = '" & AgL.PubDivCode & "' " & _
                                " And G.Site_Code = '" & TxtSite_Code.AgSelectedValue & "'  " & _
                                " And IsNull(G.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & _
                                " Order By G.Description "
                        sender.AgHelpDataset(0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
                    End If
                End If

            Case TxtParty.Name
                If e.KeyCode <> Keys.Enter Then
                    If sender.AgHelpDataset Is Nothing Then
                        FCreateHelpSubgroup()
                    End If
                End If
        End Select
    End Sub

    Private Sub Txt_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtV_Type.Validating, TxtManualRefNo.Validating, TxtFromGodown.Validating, TxtParty.Validating
        Dim DtTemp As DataTable
        Select Case sender.NAME
            Case TxtV_Type.Name
                TxtManualRefNo.Text = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "StockHead", TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, AgTemplate.ClsMain.ManualRefType.Max)
                TxtProcess.Enabled = False
                If CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Process")), Boolean) Then
                    If InStr(",", AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Process"))) <= 0 Then
                        mQry = "Select NCat, Description from Process Where NCat= '" & Replace(AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Process")), "|", "") & "'  "
                        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
                        If DtTemp.Rows.Count > 0 Then
                            TxtProcess.Tag = AgL.XNull(DtTemp.Rows(0)("NCat"))
                            TxtProcess.Text = AgL.XNull(DtTemp.Rows(0)("Description"))
                            TxtProcess.Enabled = False
                        End If
                    Else
                        TxtProcess.Enabled = True
                    End If
                End If
                'If TxtV_Type.AgLastValueTag <> TxtV_Type.Tag Then
                TxtParty.AgHelpDataSet = Nothing
                Dgl1.AgHelpDataSet(Col1Item) = Nothing
                'End If
            Case TxtParty.Name
                mQry = " SELECT count(DISTINCT H.DocID) AS NoOfReq " & _
                        " FROM Requisition H " & _
                        " LEFT JOIN RequisitionDetail L ON L.DocId = H.DocID  " & _
                        " Left Join " & _
                        " ( " & _
                        " SELECT S.Requisition, S.RequisitionSr, sum(S.Qty) AS IssQty  " & _
                        " FROM StockHeadDetail S  " & _
                        " WHERE isnull(S.Requisition,'') <> ''  " & _
                        " GROUP BY S.Requisition, S.RequisitionSr " & _
                        " ) V1 ON V1.Requisition = H.DocId AND V1.RequisitionSr = L.Sr " & _
                        " WHERE isnull(L.ApproveQty,0) - isnull(V1.IssQty,0) > 0 AND H.RequisitionBy = '" & TxtParty.Tag & "' "

                If Val(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar) > 0 Then
                    LblReqNoofPerson.Text = "No. of Requisition : " & AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar.ToString
                Else
                    LblReqNoofPerson.Text = ""
                End If

                TxtManualRefNo.Text = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "StockHead", TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, AgTemplate.ClsMain.ManualRefType.Max)


        End Select
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_Topctrl_tbAdd() Handles Me.BaseEvent_Topctrl_tbAdd
        Dim DtTemp As DataTable
        TxtManualRefNo.Text = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "StockHead", TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, AgTemplate.ClsMain.ManualRefType.Max)
        If AgL.XNull(DtV_TypeSettings.Rows(0)("Default_Godown")) <> "" Then
            TxtFromGodown.Tag = AgL.XNull(DtV_TypeSettings.Rows(0)("Default_Godown"))
            TxtFromGodown.Text = AgL.Dman_Execute("Select Description from Godown Where Code = '" & AgL.XNull(DtV_TypeSettings.Rows(0)("Default_Godown")) & "' ", AgL.GCn).ExecuteScalar
        End If


        If TxtV_Type.Text <> "" Then
            TxtProcess.Enabled = False
            If CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Process")), Boolean) Then
                If InStr(",", AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Process"))) <= 0 Then
                    mQry = "Select NCat, Description from Process Where NCat= '" & Replace(AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Process")), "|", "") & "'  "
                    DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
                    If DtTemp.Rows.Count > 0 Then
                        TxtProcess.Tag = AgL.XNull(DtTemp.Rows(0)("NCat"))
                        TxtProcess.Text = AgL.XNull(DtTemp.Rows(0)("Description"))
                        TxtProcess.Enabled = False
                    End If
                Else
                    TxtProcess.Enabled = True
                End If
            End If
        End If
    End Sub


    Private Sub Dgl1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellEnter
        If Dgl1.CurrentCell Is Nothing Then Exit Sub
        Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
            Case Col1Qty
                CType(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex), AgControls.AgTextColumn).AgNumberRightPlaces = Val(Dgl1.Item(Col1QtyDecimalPlaces, Dgl1.CurrentCell.RowIndex).Value)
            Case Col1MeasurePerPcs, Col1TotalMeasure
                CType(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex), AgControls.AgTextColumn).AgNumberRightPlaces = Val(Dgl1.Item(Col1MeasureDecimalPlaces, Dgl1.CurrentCell.RowIndex).Value)
            Case Col1ProdOrder
                Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) = Nothing
        End Select
    End Sub

    Private Sub Dgl1_EditingControl_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Dgl1.EditingControl_Validating
        If Topctrl1.Mode = "Browse" Then Exit Sub
        Dim mRowIndex As Integer, mColumnIndex As Integer
        Dim DrTemp As DataRow() = Nothing
        Try
            mRowIndex = Dgl1.CurrentCell.RowIndex
            mColumnIndex = Dgl1.CurrentCell.ColumnIndex
            If Dgl1.Item(mColumnIndex, mRowIndex).Value Is Nothing Then Dgl1.Item(mColumnIndex, mRowIndex).Value = ""
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Item_UID
                    Validating_Item_Uid(Dgl1.Item(Col1Item_UID, mRowIndex).Value, mRowIndex)
                Case Col1Item
                    Validating_ItemCode(mColumnIndex, mRowIndex)
                Case Col1ItemCode
                    Validating_ItemCode(mColumnIndex, mRowIndex)
            End Select
            Call Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    'Private Sub Validating_Item(ByVal Code As String, ByVal mRow As Integer)
    '    Dim DrTemp As DataRow() = Nothing
    '    Dim DtTemp As DataTable = Nothing
    '    Try

    '        If Dgl1.Item(Col1Item, mRow).Value.ToString.Trim = "" Or Dgl1.AgSelectedValue(Col1Item, mRow).ToString.Trim = "" Then
    '            Dgl1.Item(Col1Unit, mRow).Value = ""
    '            Dgl1.Item(Col1Unit, mRow).Value = ""
    '            Dgl1.Item(Col1CurrentStock, mRow).Value = ""
    '        Else
    '            If Dgl1.AgHelpDataSet(Col1Item) IsNot Nothing Then
    '                DrTemp = Dgl1.AgHelpDataSet(Col1Item).Tables(0).Select("Code = '" & Code & "'")
    '                Dgl1.AgSelectedValue(Col1ItemCode, mRow) = Dgl1.AgSelectedValue(Col1Item, mRow)
    '                Dgl1.Item(Col1Unit, mRow).Value = AgL.XNull(DrTemp(0)("Unit"))
    '                Dgl1.Item(Col1QtyDecimalPlaces, mRow).Value = AgL.VNull(DtTemp.Rows(0)("QtyDecimalPlaces"))
    '                Dgl1.Item(Col1MeasurePerPcs, mRow).Value = AgL.XNull(DrTemp(0)("Measure"))
    '                Dgl1.Item(Col1MeasureUnit, mRow).Value = AgL.XNull(DrTemp(0)("MeasureUnit"))
    '                Dgl1.Item(Col1MeasureDecimalPlaces, mRow).Value = AgL.VNull(DtTemp.Rows(0)("MeasureDecimalPlaces"))
    '                Dgl1.Item(Col1CurrentStock, mRow).Value = AgTemplate.ClsMain.FunRetStock(Dgl1.AgSelectedValue(Col1Item, mRow), mSearchCode, , TxtFromGodown.AgSelectedValue, , , TxtV_Date.Text)
    '            End If
    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.Message & " On Validating_Item Function ")
    '    End Try
    'End Sub

    Private Sub Validating_ItemCode(ByVal mColumn As Integer, ByVal mRow As Integer)
        Dim DrTemp As DataRow() = Nothing
        Dim DtTemp As DataTable = Nothing
        Try
            If Dgl1.Item(mColumn, mRow).Value.ToString.Trim = "" Or Dgl1.AgSelectedValue(mColumn, mRow).ToString.Trim = "" Then
                Dgl1.Item(Col1Unit, mRow).Value = ""
                Dgl1.Item(Col1CurrentStock, mRow).Value = ""
            Else
                If Dgl1.AgDataRow IsNot Nothing Then
                    Dgl1.Item(Col1Item, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("Item").Value)
                    Dgl1.Item(Col1Item, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("ItemDesc").Value)
                    Dgl1.Item(Col1RequisitionNo, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("RequisitionDocId").Value)
                    Dgl1.Item(Col1RequisitionNo, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("RequisitionNo").Value)
                    Dgl1.Item(Col1RequisitionSr, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("RequisitionSr").Value)
                    Dgl1.Item(Col1Qty, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("Qty").Value)
                    Dgl1.Item(Col1ItemCode, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("Item").Value)
                    Dgl1.Item(Col1ItemCode, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("ItemCode").Value)
                    Dgl1.Item(Col1Unit, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("Unit").Value)
                    Dgl1.Item(Col1VNature, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("V_Nature").Value)
                    If CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Rate")), Boolean) Or CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsMandatory_Rate")), Boolean) Then
                        Dgl1.Item(Col1Rate, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("Rate").Value)
                    End If
                    Dgl1.Item(Col1QtyDecimalPlaces, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("QtyDecimalPlaces").Value)
                    Dgl1.Item(Col1MeasurePerPcs, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("MeasurePerPcs").Value)
                    Dgl1.Item(Col1MeasureUnit, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("MeasureUnit").Value)
                    Dgl1.Item(Col1MeasureDecimalPlaces, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("MeasureDecimalPlaces").Value)
                    Dgl1.Item(Col1CurrentStock, mRow).Value = AgTemplate.ClsMain.FunRetStock(Dgl1.AgSelectedValue(Col1ItemCode, mRow), mSearchCode, , TxtFromGodown.AgSelectedValue, , , TxtV_Date.Text)
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " On Validating_Item Function ")
        End Try
    End Sub

    Private Sub DGL1_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles Dgl1.RowsAdded, Dgl1.RowsAdded
        sender(ColSNo, sender.Rows.Count - 1).Value = Trim(sender.Rows.Count)
    End Sub

    Private Sub FrmProductionOrder_BaseFunction_Calculation() Handles Me.BaseFunction_Calculation
        Dim I As Integer
        LblTotalQty.Text = 0
        LblTotalMeasure.Text = 0

        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1Item, I).Value <> "" Then
                Dgl1.Item(Col1TotalMeasure, I).Value = Format(Val(Dgl1.Item(Col1Qty, I).Value) * Val(Dgl1.Item(Col1MeasurePerPcs, I).Value), "0.".PadRight(Val(Dgl1.Item(Col1MeasureDecimalPlaces, I).Value) + 2, "0"))
                If Val(Dgl1.Item(Col1Qty, I).Value) > 0 Then
                    Dgl1.Item(Col1Amount, I).Value = Format(Val(Dgl1.Item(Col1Qty, I).Value) * Val(Dgl1.Item(Col1Rate, I).Value), "0.00")
                End If

                LblTotalQty.Text = Val(LblTotalQty.Text) + Val(Dgl1.Item(Col1Qty, I).Value)
                LblTotalMeasure.Text = Val(LblTotalMeasure.Text) + Val(Dgl1.Item(Col1TotalMeasure, I).Value)
            End If
        Next
        LblTotalQty.Text = Format(Val(LblTotalQty.Text), "0.000")
        LblTotalMeasure.Text = Format(Val(LblTotalMeasure.Text), "0.000")
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        Dim I As Integer = 0

        If AgL.RequiredField(TxtFromGodown, "From Godown") Then passed = False : Exit Sub
        If AgCL.AgIsBlankGrid(Dgl1, Dgl1.Columns(Col1Item).Index) = True Then passed = False : Exit Sub
        If AgCL.AgIsDuplicate(Dgl1, CStr(Dgl1.Columns(Col1Item).Index & "," & CStr(Dgl1.Columns(Col1RequisitionNo).Index))) = True Then passed = False : Exit Sub

        With Dgl1
            For I = 0 To .Rows.Count - 1
                If .Item(Col1Item, I).Value <> "" Then
                    If Val(.Item(Col1Qty, I).Value) = 0 Then
                        MsgBox("Qty Is 0 At Row No " & Dgl1.Item(ColSNo, I).Value & "")
                        .CurrentCell = .Item(Col1Qty, I) : Dgl1.Focus()
                        passed = False : Exit Sub
                    End If

                    If Val(.Item(Col1CurrentStock, I).Value) < Val(.Item(Col1Qty, I).Value) Then
                        If MsgBox("Qty of " & .Item(Col1Item, I).Value & " is less than " & Dgl1.Item(Col1Qty, I).Value & " in Stock. " & vbCrLf & " Do you want to Countinoue ?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                            .CurrentCell = .Item(Col1Qty, I) : Dgl1.Focus()
                            passed = False : Exit Sub
                        End If
                    End If
                End If
            Next
        End With
    End Sub

    Private Sub FrmProductionOrder_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
        Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
        LblTotalMeasure.Text = 0 : LblTotalQty.Text = 0
        LblReqNoofPerson.Text = ""
    End Sub

    Private Sub DGL1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.KeyDown
        If e.Control And e.KeyCode = Keys.D Then
            sender.CurrentRow.Selected = True
        End If
        If e.Control Or e.Shift Or e.Alt Then Exit Sub
    End Sub

    Private Sub TempStockTransferIssue_BaseFunction_Create() Handles Me.BaseFunction_CreateHelpDataSet

    End Sub

    Private Sub FrmYarnSKUOpeningStock_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AgL.WinSetting(Me, 566, 895)
    End Sub

    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub

    Private Sub Validating_Item_Uid(ByVal Item_Uid As String, ByVal mRow As Integer)
        Dim DrTemp As DataRow() = Nothing
        Dim DtTemp As DataTable = Nothing

        Try
            mQry = " SELECT I.Code, I.Description, I.Unit, I.ManualCode, I.Prod_Measure, I.MeasureUnit, I.Rate, " & _
                   " U.DecimalPlaces as QtyDecimalPlaces, MU.DecimalPlaces as MeasureDecimalPlaces, UI.Code as ItemUIDCode, UI.ProdOrder as ProdOrderDocID, PO.V_Type + '-' + PO.ManualRefNo as ProdOrderNo  " & _
                   " FROM (Select Item, Code, ProdOrder From Item_UID Where Item_Uid = '" & Dgl1.Item(Col1Item_UID, mRow).Value & "') UI " & _
                   " Left Join ProdOrder PO With (NoLock) On UI.ProdOrder = PO.DocID " & _
                   " Left Join Item I With (NoLock) On UI.Item  = I.Code " & _
                   " Left Join Unit U With (NoLock) On I.Unit = U.Code " & _
                   " Left Join Unit MU With (NoLock) On I.MeasureUnit = MU.Code "
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
            Dgl1.Item(Col1Item_UID, mRow).Tag = AgL.XNull(DtTemp.Rows(0)("ItemUIDCode"))
            Dgl1.Item(Col1ItemCode, mRow).Tag = AgL.XNull(DtTemp.Rows(0)("Code"))
            Dgl1.Item(Col1ItemCode, mRow).Value = AgL.XNull(DtTemp.Rows(0)("ManualCode"))
            Dgl1.Item(Col1Item, mRow).Tag = AgL.XNull(DtTemp.Rows(0)("Code"))
            Dgl1.Item(Col1Item, mRow).Value = AgL.XNull(DtTemp.Rows(0)("Description"))
            Dgl1.Item(Col1ProdOrder, mRow).Tag = AgL.XNull(DtTemp.Rows(0)("ProdOrderDocID"))
            Dgl1.Item(Col1ProdOrder, mRow).Value = AgL.XNull(DtTemp.Rows(0)("ProdOrderNo"))
            Dgl1.Item(Col1Qty, mRow).Value = 1
            Dgl1.Item(Col1Unit, mRow).Value = AgL.XNull(DtTemp.Rows(0)("Unit"))
            If CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Rate")), Boolean) Or CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsMandatory_Rate")), Boolean) Then
                Dgl1.Item(Col1Rate, mRow).Value = AgL.VNull(DtTemp.Rows(0)("Rate"))
            End If
            Dgl1.Item(Col1QtyDecimalPlaces, mRow).Value = AgL.VNull(DtTemp.Rows(0)("QtyDecimalPlaces"))
            Dgl1.Item(Col1MeasurePerPcs, mRow).Value = Format(AgL.VNull(DtTemp.Rows(0)("Prod_Measure")), "0.".PadRight(AgL.VNull(DtTemp.Rows(0)("MeasureDecimalPlaces")) + 2, "0"))
            Dgl1.Item(Col1TotalMeasure, mRow).Value = AgL.VNull(DtTemp.Rows(0)("Prod_Measure"))
            Dgl1.Item(Col1MeasureUnit, mRow).Value = AgL.XNull(DtTemp.Rows(0)("MeasureUnit"))
            Dgl1.Item(Col1MeasureDecimalPlaces, mRow).Value = AgL.VNull(DtTemp.Rows(0)("MeasureDecimalPlaces"))
            Dgl1.Item(Col1CurrentStock, mRow).Value = AgTemplate.ClsMain.FunRetStock(Dgl1.AgSelectedValue(Col1ItemCode, mRow), mSearchCode, , TxtFromGodown.AgSelectedValue, , , TxtV_Date.Text)
            mQry = "Select NCat, Description From Process Where NCat = (SELECT TOP 1 Process FROM Stock WHERE Item_UID  ='" & Dgl1.Item(Col1Item_UID, mRow).Tag & "' ORDER BY V_Date Desc)"
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtTemp.Rows.Count > 0 Then
                Dgl1.Item(Col1Process, mRow).Tag = AgL.XNull(DtTemp.Rows(0)("NCat"))
                Dgl1.Item(Col1Process, mRow).Value = AgL.XNull(DtTemp.Rows(0)("Description"))
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " On Validating_Item_Uid Function ")
        End Try
    End Sub

    Private Sub FCreateHelpSubgroup()
        Dim strCond As String = ""
        If DtV_TypeSettings.Rows.Count > 0 Then
            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_AcGroup")) <> "" Then
                strCond += " And CharIndex('|' + H.GroupCode + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_AcGroup")) & "') > 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_AcGroup")) <> "" Then
                strCond += " And CharIndex('|' + H.GroupCode + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_AcGroup")) & "') <= 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_SubgroupDivision")) <> "" Then
                strCond += " And CharIndex('|' + H.Div_Code + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_subGroupDivision")) & "') > 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_SubgroupSite")) <> "" Then
                strCond += " And CharIndex('|' + H.Site_Code + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_subGroupSite")) & "') > 0 "
            End If
        End If

        If TxtProcess.Text = "" Then
            mQry = " SELECT H.SubCode AS Code, H.Name AS JobWorker, City.CityName as City_Name " & _
                    " FROM Subgroup H  With (NoLock) " & _
                    " Left Join City On H.CityCode = City.CityCode" & _
                    " Where IsNull(H.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') ='" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
            TxtParty.AgHelpDataSet(0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
        Else
            mQry = " SELECT H.SubCode AS Code, H.Name AS JobWorker, City.CityName as City_Name " & _
                    " FROM Subgroup H  With (NoLock) " & _
                    " Left Join City On H.CityCode = City.CityCode" & _
                    " Left Join JobworkerProcess JP On H.SubCode = JP.SubCode" & _
                    " Where IsNull(H.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') ='" & AgTemplate.ClsMain.EntryStatus.Active & "' And JP.Process = '" & TxtProcess.Tag & "' " & strCond
            TxtParty.AgHelpDataSet(0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
        End If
    End Sub

    Private Sub FCreateHelpItem(ByVal ColumnName As String)
        Dim strCond As String = ""
        Dim ContraV_TypeCondStr As String = ""

        If DtV_TypeSettings.Rows.Count > 0 Then
            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")) <> "" Then
                strCond += " And CharIndex('|' + I.ItemType + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")) & "') > 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemGroup")) <> "" Then
                strCond += " And CharIndex('|' + I.ItemGroup + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemGroup")) & "') > 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_ItemGroup")) <> "" Then
                strCond += " And CharIndex('|' + I.ItemGroup + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_ItemGroup")) & "') <= 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Item")) <> "" Then
                strCond += " And CharIndex('|' + I.Code + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Item")) & "') > 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_Item")) <> "" Then
                strCond += " And CharIndex('|' + I.Item + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_Item")) & "') <= 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemDivision")) <> "" Then
                strCond += " And CharIndex('|' + I.Div_Code + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemDivision")) & "') > 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemSite")) <> "" Then
                strCond += " And CharIndex('|' + I.Site_Code + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemSite")) & "') > 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ContraV_Type")) <> "" Then
                ContraV_TypeCondStr += " And CharIndex('|' + V_Type + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ContraV_Type")) & "') > 0 "
            End If
        End If

        Select Case ColumnName
            Case Col1Item
                If RbtIssueForReqisition.Checked Then
                    mQry = " SELECT max(L.Item) AS Item, max(I.Description) AS ItemDesc, max(I.ManualCode) AS ItemCode, max(H.ReferenceNo) AS RequisitionNo, isnull(sum(L.ApproveQty),0) - isnull(sum(V1.IssueQty),0) AS Qty, " & _
                            " max(L.Unit) AS Unit, H.DocID AS RequisitionDocId, L.Sr AS RequisitionSr,  max(L.MeasurePerPcs) AS MeasurePerPcs, max(I.Rate) AS Rate, " & _
                            " ISNULL(Max(U.DecimalPlaces),0) As QtyDecimalPlaces, ISNULL(Max(UM.DecimalPlaces),0) As MeasureDecimalPlaces, max(L.MeasureUnit) AS MeasureUnit, " & _
                            " '" & RbtIssueForReqisition.Text & "' AS V_Nature " & _
                            " FROM Requisition H " & _
                            " LEFT JOIN RequisitionDetail L ON L.DocId = H.DocID " & _
                            " LEFT JOIN Item I ON I.Code = L.Item  " & _
                            " LEFT JOIN Unit U ON U.Code = L.Unit  " & _
                            " LEFT JOIN Unit UM ON UM.Code = L.MeasureUnit  " & _
                            " Left Join " & _
                            " ( " & _
                            " SELECT S.Requisition, S.RequisitionSr , sum(S.Qty) AS IssueQty  " & _
                            " FROM StockHeadDetail  S " & _
                            " WHERE isnull(S.Requisition,'') <> '' AND S.DocId <> '" & mSearchCode & "'  " & _
                            " GROUP BY S.Requisition, S.RequisitionSr  " & _
                            " ) V1 ON V1.Requisition = H.DocId AND V1.RequisitionSr = L.Sr " & _
                            " WHERE 1=1 " & _
                            " AND H.Div_Code = '" & TxtDivision.Tag & "'  AND H.Site_Code ='" & TxtSite_Code.Tag & "'   " & _
                            " AND H.V_Date <= '" & TxtV_Date.Text & "' AND H.RequisitionBy = '" & TxtParty.Tag & "'  " & _
                            " " & strCond & _
                            " GROUP BY H.DocID, L.Sr  " & _
                            " HAVING isnull(sum(L.ApproveQty),0) - isnull(sum(V1.IssueQty),0) > 0 "
                    Dgl1.AgHelpDataSet(Col1Item, 8) = AgL.FillData(mQry, AgL.GCn)
                Else
                    mQry = " SELECT I.Code AS Item, I.Description AS ItemDesc, I.ManualCode AS ItemCode, I.Unit, " & _
                        " I.Measure AS MeasurePerPcs, I.MeasureUnit, U.DecimalPlaces as QtyDecimalPlaces, MU.DecimalPlaces as MeasureDecimalPlaces, I.Rate, " & _
                        " NULL AS RequisitionNo, NULL AS RequisitionDocId, NULL AS RequisitionSr, 0 AS Qty, " & _
                        " '" & RbtIssueDirect.Text & "' AS V_Nature " & _
                        " FROM Item I " & _
                        " Left Join Unit U On I.Unit = U.Code " & _
                        " Left Join Unit MU On I.MeasureUnit = MU.Code " & _
                        " Where IsNull(I.IsDeleted ,0)  = 0 And " & _
                        " IsNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "')='" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
                    Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex, 10) = AgL.FillData(mQry, AgL.GCn)
                End If

            Case Col1ItemCode
                If RbtIssueForReqisition.Checked Then
                    mQry = " SELECT max(L.Item) AS Code, max(I.ManualCode) AS ItemCode, max(I.Description) AS ItemDesc,  max(H.V_Type) + '-' + max ( Convert(NVarchar,H.V_No)) AS PlanningNo, L.MaterialPlanSr , L.MaterialPlan ,  max(L.Unit) AS Unit, " & _
                            " max(L.MeasurePerPcs) AS MeasurePerPcs, max(L.MeasureUnit) AS MeasureUnit, isnull(sum(L.UserPurchPlanQty ),0) - isnull(sum(D.IndQty ),0) AS PlanQty, sum(L.UserPurchPlanMeasure ) - isnull(sum(D.IndMeasure ),0) AS PlanMeasure, " & _
                            " Max(U.DecimalPlaces) As QtyDecimalPlaces, Max(UM.DecimalPlaces) As MeasureDecimalPlaces " & _
                            " FROM MaterialPlan H " & _
                            " LEFT JOIN MaterialPlanDetail L ON L.DocId = H.DocID  " & _
                            " LEFT JOIN Item I ON I.Code = L.Item  " & _
                            " LEFT JOIN Unit U ON U.Code = L.Unit  " & _
                            " LEFT JOIN Unit UM ON UM.Code = L.MeasureUnit  " & _
                            " LEFT JOIN " & _
                            " ( " & _
                            " SELECT IND.MaterialPlan, IND.MaterialPlanSr, sum(IND.IndentQty) AS IndQty , SUM(IND.TotalIndentMeasure) AS IndMeasure  " & _
                            " FROM PurchIndentDetail IND " & _
                            " WHERE isnull(IND.MaterialPlan,'') <> ''  " & _
                            " GROUP BY IND.MaterialPlan, IND.MaterialPlanSr " & _
                            " ) AS D ON D.MaterialPlan = L.DocId AND D.MaterialPlanSr = L.Sr " & _
                            " WHERE isnull(L.MaterialPlan,'') <> '' AND IND.DocId <> '" & mSearchCode & "' " & strCond & _
                            " GROUP BY L.MaterialPlan ,L.MaterialPlanSr " & _
                            " HAVING isnull(sum(L.UserPurchPlanQty ),0) - isnull(sum(D.IndQty ),0) > 0 "
                    Dgl1.AgHelpDataSet(Col1ItemCode, 6) = AgL.FillData(mQry, AgL.GCn)
                Else
                    mQry = " SELECT H.Code, H.ManualCode as Item_No, H.Description as Item_Name, H.Unit, " & _
                        " H.Measure, H.MeasureUnit, U.DecimalPlaces as QtyDecimalPlaces, MU.DecimalPlaces as MeasureDecimalPlaces, H.Rate " & _
                        " FROM Item H " & _
                        " Left Join Unit U On H.Unit = U.Code " & _
                        " Left Join Unit MU On H.MeasureUnit = MU.Code " & _
                        " Where IsNull(H.IsDeleted ,0)  = 0 And " & _
                        " IsNull(H.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "')='" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
                    Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex, 4) = AgL.FillData(mQry, AgL.GCn)
                End If
        End Select
    End Sub

    Private Sub Dgl1_EditingControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.EditingControl_KeyDown
        Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
            Case Col1ItemCode
                If e.KeyCode <> Keys.Enter Then
                    If Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) Is Nothing Then
                        FCreateHelpItem(Col1ItemCode)
                    End If
                End If

            Case Col1Item
                If e.KeyCode <> Keys.Enter Then
                    If Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) Is Nothing Then
                        FCreateHelpItem(Col1Item)
                    End If
                End If

            Case Col1ProdOrder
                If e.KeyCode <> Keys.Enter Then
                    If Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) Is Nothing Then
                        mQry = "SELECT H.DocId AS Code, " & _
                                " Max(H.ManualRefNo) As Prod_Order_No, Max(H.V_Type) as Prod_Order_Type " & _
                                " FROM (Select DocID From ProdOrderDetail With (NoLock) Where Item ='" & Dgl1.Item(Col1Item, Dgl1.CurrentCell.RowIndex).Tag & "') L " & _
                                " Left Join ProdOrder H With (NoLock) On L.DocID = H.DocId  " & _
                                " Group By H.DocId"
                        Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) = AgL.FillData(mQry, AgL.GCn)
                    End If
                End If
            Case Col1Process
                If e.KeyCode <> Keys.Enter Then
                    If Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) Is Nothing Then
                        mQry = " SELECT P.NCat AS Code, P.Description  " & _
                                " FROM Process P  "
                        Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) = AgL.FillData(mQry, AgL.GCn)
                    End If
                End If

            Case Col1Unit
                If e.KeyCode <> Keys.Enter Then
                    If Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) Is Nothing Then
                        mQry = " SELECT H.Code, H.Code as Description  " & _
                                " FROM Unit H Order by H.Code  "
                        Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) = AgL.FillData(mQry, AgL.GCn)
                    End If
                End If
        End Select
    End Sub

    Private Sub FrmStoreIssue_BaseEvent_Topctrl_tbPrn(ByVal SearchCode As String) Handles Me.BaseEvent_Topctrl_tbPrn
        mQry = " SELECT H.DOCID, H.V_TYPE, H.V_DATE, H.V_NO, H.MANUALREFNO, H.REMARKS, H.ENTRYBY, H.ENTRYDATE, " & _
                " H.ENTRYTYPE, H.ENTRYSTATUS,  H.APPROVEBY, H.APPROVEDATE,  H.STATUS,  " & _
                " L.SR, L.ITEM, ISNULL(L.QTY_ISS,0)+ISNULL(L.QTY_REC,0) AS QTY, L.UNIT, L.REMARKS AS LINEREMARKS,  S.NAME AS JOBWORKERNAME, S.DISPNAME AS JOBWORKERDISPNAME,   S.ADD1, " & _
                " S.ADD2,S.ADD3,C.CITYNAME,S.MOBILE,S.PHONE, S.PAN,  G.DESCRIPTION AS GODOWNDESC,  I.DESCRIPTION AS ITEMDESC,   " & _
                " I.ITEMGROUP ,   I.ITEMTYPE, IG.DESCRIPTION AS ITEMGROUPDESC " & _
                " FROM STOCKHEAD H   " & _
                " LEFT JOIN STOCK L ON H.DOCID = L.DOCID   " & _
                " LEFT JOIN VOUCHER_TYPE VT ON H.V_TYPE = VT.V_TYPE  " & _
                " LEFT JOIN SUBGROUP S ON H.SUBCODE = S.SUBCODE   " & _
                " LEFT JOIN CITY C ON S.CITYCODE = C.CITYCODE   " & _
                " LEFT JOIN GODOWN G ON H.FROMGODOWN = G.CODE   " & _
                " LEFT JOIN ITEM I ON L.ITEM = I.CODE   " & _
                " LEFT JOIN ITEMGROUP  IG ON I.ITEMGROUP = IG.CODE  " & _
                " WHERE H.DocID =  '" & mSearchCode & "'  Order By L.Sr "
        ClsMain.FPrintThisDocument(Me, TxtV_Type.Tag, mQry, "Store_Issue_Print", "Store Issue")
    End Sub

    Private Sub FrmStoreIssue_BaseEvent_Topctrl_tbRef() Handles Me.BaseEvent_Topctrl_tbRef
        TxtParty.AgHelpDataSet = Nothing
        Dgl1.AgHelpDataSet(Col1Item) = Nothing
    End Sub

    Private Sub BtnFillIssueDetail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnFillIssueDetail.Click
        If Topctrl1.Mode = "Browse" Then Exit Sub
        If RbtIssueForReqisition.Checked = False Then Exit Sub
        Dim strTicked As String
        strTicked = FHPGD_RequisionNo()
        If strTicked <> "" Then
            ProcFillRequisitionDetails(strTicked)
        End If
    End Sub

    Private Function FHPGD_RequisionNo() As String
        Dim FRH_Multiple As DMHelpGrid.FrmHelpGrid_Multi
        Dim StrSendText As String, bCondStr$ = ""
        Dim StrRtn As String = ""

        StrSendText = RbtIssueForReqisition.Tag

        If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ContraV_Type")) <> "" Then
            bCondStr += " And CharIndex('|' & H.V_Type & '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ContraV_Type")) & "') > 0 "
        End If

        If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")) <> "" Then
            bCondStr += " And CharIndex('|' & I.ItemType & '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")) & "') > 0 "
        End If

        If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemGroup")) <> "" Then
            bCondStr += " And CharIndex('|' & I.ItemGroup & '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemGroup")) & "') > 0 "
        End If

        If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_ItemGroup")) <> "" Then
            bCondStr += " And CharIndex('|' & I.ItemGroup & '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_ItemGroup")) & "') <= 0 "
        End If

        If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Item")) <> "" Then
            bCondStr += " And CharIndex('|' & I.Code & '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Item")) & "') > 0 "
        End If

        If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_Item")) <> "" Then
            bCondStr += " And CharIndex('|' & I.Code & '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_Item")) & "') <= 0 "
        End If

        mQry = " SELECT DISTINCT 'o' AS Tick, H.DocID, max(H.ReferenceNo) AS ReqNo, max(H.V_Date) AS ReqDate " & _
                " FROM Requisition H " & _
                " LEFT JOIN RequisitionDetail L ON L.DocId = H.DocID " & _
                " LEFT JOIN Item I ON I.Code = L.Item " & _
                " Left Join " & _
                " ( " & _
                " SELECT S.Requisition, S.RequisitionSr , sum(S.Qty) AS IssueQty  " & _
                " FROM StockHeadDetail  S " & _
                " WHERE isnull(S.Requisition,'') <> '' AND S.DocId  <> '" & mSearchCode & "' " & _
                " GROUP BY S.Requisition, S.RequisitionSr  " & _
                " ) V1 ON V1.Requisition = H.DocId AND V1.RequisitionSr = L.Sr " & _
                " WHERE isnull(L.ApproveQty,0) - isnull(V1.IssueQty,0) > 0 " & _
                " AND H.Div_Code = '" & TxtDivision.Tag & "'  AND H.Site_Code ='" & TxtSite_Code.Tag & "'   " & _
                " AND H.V_Date <= '" & TxtV_Date.Text & "' AND H.RequisitionBy = '" & TxtParty.Tag & "'  " & _
                " " & bCondStr & _
                " GROUP BY H.DocID "

        FRH_Multiple = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(AgL.FillData(mQry, AgL.GCn).TABLES(0)), "", 400, 320, , , False)
        FRH_Multiple.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple.FFormatColumn(1, , 0, , False)
        FRH_Multiple.FFormatColumn(2, "Req. No.", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(3, "Req. Date", 100, DataGridViewContentAlignment.MiddleLeft)

        FRH_Multiple.StartPosition = FormStartPosition.CenterScreen
        FRH_Multiple.ShowDialog()

        If FRH_Multiple.BytBtnValue = 0 Then
            StrRtn = FRH_Multiple.FFetchData(1, "'", "'", ",", True)
        End If
        FHPGD_RequisionNo = StrRtn

        FRH_Multiple = Nothing
    End Function

    Private Sub ProcFillRequisitionDetails(ByVal bRequisitionStr As String)
        Dim DtTemp As DataTable = Nothing
        Dim bReferenceDocId$ = "", bCondStr$ = ""
        Dim I As Integer = 0
        Try
            If Not AgL.StrCmp(Topctrl1.Mode, "Add") Then Exit Sub

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ContraV_Type")) <> "" Then
                bCondStr += " And CharIndex('|' & H.V_Type & '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ContraV_Type")) & "') > 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")) <> "" Then
                bCondStr += " And CharIndex('|' & I.ItemType & '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")) & "') > 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemGroup")) <> "" Then
                bCondStr += " And CharIndex('|' & I.ItemGroup & '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemGroup")) & "') > 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_ItemGroup")) <> "" Then
                bCondStr += " And CharIndex('|' & I.ItemGroup & '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_ItemGroup")) & "') <= 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Item")) <> "" Then
                bCondStr += " And CharIndex('|' & I.Code & '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Item")) & "') > 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_Item")) <> "" Then
                bCondStr += " And CharIndex('|' & I.Code & '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_Item")) & "') <= 0 "
            End If

            mQry = " SELECT H.DocID, L.Sr, max(H.ReferenceNo) AS ReqNo, isnull(sum(L.ApproveQty),0) - isnull(sum(V1.IssueQty),0) AS BalQty, " & _
                    " max(L.Item) AS Item, max(I.Description) AS ItemDesc, max(I.ManualCode) AS ItemCode  ,max(L.Unit) AS Unit, max(L.MeasurePerPcs) AS MeasurePerPcs, " & _
                    " ISNULL(Max(U.DecimalPlaces),0) As QtyDecimalPlaces, ISNULL(Max(UM.DecimalPlaces),0) As MeasureDecimalPlaces, max(L.MeasureUnit) AS MeasureUnit, '" & RbtIssueForReqisition.Text & "' AS V_Nature " & _
                    " FROM Requisition H " & _
                    " LEFT JOIN RequisitionDetail L ON L.DocId = H.DocID " & _
                    " LEFT JOIN Item I ON I.Code = L.Item  " & _
                    " LEFT JOIN Unit U ON U.Code = L.Unit  " & _
                    " LEFT JOIN Unit UM ON UM.Code = L.MeasureUnit  " & _
                    " Left Join " & _
                    " ( " & _
                    " SELECT S.Requisition, S.RequisitionSr , sum(S.Qty) AS IssueQty  " & _
                    " FROM StockHeadDetail  S " & _
                    " WHERE isnull(S.Requisition,'') <> '' AND S.DocId <> '" & mSearchCode & "'  " & _
                    " GROUP BY S.Requisition, S.RequisitionSr  " & _
                    " ) V1 ON V1.Requisition = H.DocId AND V1.RequisitionSr = L.Sr " & _
                    " WHERE 1=1 " & _
                    " AND H.Div_Code = '" & TxtDivision.Tag & "'  AND H.Site_Code ='" & TxtSite_Code.Tag & "'   " & _
                    " AND H.V_Date <= '" & TxtV_Date.Text & "' AND H.RequisitionBy = '" & TxtParty.Tag & "'  " & _
                    " AND L.DocId IN ( " & bRequisitionStr & " ) " & bCondStr & _
                    " GROUP BY H.DocID, L.Sr  " & _
                    " HAVING isnull(sum(L.ApproveQty),0) - isnull(sum(V1.IssueQty),0) > 0 "

            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
            With DtTemp
                Dgl1.RowCount = 1
                Dgl1.Rows.Clear()
                If .Rows.Count > 0 Then
                    For I = 0 To .Rows.Count - 1
                        Dgl1.Rows.Add()
                        Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count - 1
                        Dgl1.Item(Col1ItemCode, I).Tag = AgL.XNull(.Rows(I)("Item"))
                        Dgl1.Item(Col1ItemCode, I).Value = AgL.XNull(.Rows(I)("ItemCode"))
                        Dgl1.Item(Col1Item, I).Tag = AgL.XNull(.Rows(I)("Item"))
                        Dgl1.Item(Col1Item, I).Value = AgL.XNull(.Rows(I)("ItemDesc"))
                        Dgl1.Item(Col1RequisitionNo, I).Value = AgL.XNull(.Rows(I)("ReqNo"))
                        Dgl1.Item(Col1RequisitionNo, I).Tag = AgL.XNull(.Rows(I)("DocId"))
                        Dgl1.Item(Col1RequisitionSr, I).Value = AgL.VNull(.Rows(I)("Sr"))
                        Dgl1.Item(Col1Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                        Dgl1.Item(Col1QtyDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("QtyDecimalPlaces"))
                        Dgl1.Item(Col1Qty, I).Value = Format(AgL.VNull(.Rows(I)("BalQty")), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
                        Dgl1.Item(Col1MeasureDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("MeasureDecimalPlaces"))
                        Dgl1.Item(Col1MeasureUnit, I).Value = AgL.XNull(.Rows(I)("MeasureUnit"))
                        Dgl1.Item(Col1VNature, I).Value = AgL.XNull(.Rows(I)("V_Nature"))
                        Dgl1.Item(Col1MeasurePerPcs, I).Value = Format(AgL.VNull(.Rows(I)("MeasurePerPcs")), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                        Dgl1.Item(Col1CurrentStock, I).Value = AgTemplate.ClsMain.FunRetStock(Dgl1.AgSelectedValue(Col1Item, I), mInternalCode, , , , , TxtV_Date.Text)
                    Next I
                End If
            End With
            Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Dgl1_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.RowEnter
        If Dgl1.CurrentCell Is Nothing Then Exit Sub
        Dim mRow = e.RowIndex ' Dgl1.CurrentCell.RowIndex

        If Dgl1.Item(Col1Item, mRow).Value <> "" Then
            If Dgl1.Item(Col1VNature, mRow).Value = RbtIssueForReqisition.Text Then
                RbtIssueForReqisition.Checked = True
            Else
                RbtIssueDirect.Checked = True
            End If
        End If

    End Sub

    Private Sub RbtIssueForReqisition_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RbtIssueForReqisition.CheckedChanged
        Dgl1.AgHelpDataSet(Col1Item) = Nothing
    End Sub
End Class
