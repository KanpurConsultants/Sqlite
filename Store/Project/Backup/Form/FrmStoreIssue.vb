Imports System.IO
Public Class FrmStoreIssue
    Inherits AgTemplate.TempTransaction
    Dim mQry$

    Protected WithEvents Dgl1 As New AgControls.AgDataGrid
    Protected Const ColSNo As String = "S.No."
    Protected Const Col1Item_UID As String = "Item UID"
    Protected Const Col1ItemCode As String = "Item Code"
    Protected Const Col1Item As String = "Item"
    Protected Const Col1ItemGroup As String = "Item Group"
    Protected Const Col1FromProcess As String = "From Process"
    Protected Const Col1Dimension1 As String = "Dimension1"
    Protected Const Col1Dimension2 As String = "Dimension2"
    Protected Const Col1Specification As String = "Specification"
    Protected Const Col1LotNo As String = "Lot No"
    Protected Const Col1BaleNo As String = "Bale No"
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
    Protected WithEvents BtnImprtFromText As System.Windows.Forms.Button
    Protected Const Col1CostCenter As String = "Cost Center"
    
    Dim ImportMessegeStr$ = ""
    Dim ImportMode As Boolean = False
    Dim ImportAction_NewImport As String = "New Import"
    Protected WithEvents ChkShowOnlyImportedRecords As System.Windows.Forms.CheckBox
    Protected WithEvents RbtnForStock As System.Windows.Forms.RadioButton
    Protected WithEvents TxtReason As AgControls.AgTextBox
    Protected WithEvents Label6 As System.Windows.Forms.Label
    Protected WithEvents LblTotalAmountValue As System.Windows.Forms.Label
    Protected WithEvents LbLTotalAmount As System.Windows.Forms.Label
    Dim ImportAction_ClearImport As String = "Clear Import"

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
        Me.LblCurrentStock = New System.Windows.Forms.Label
        Me.LblTotalMeasureValue = New System.Windows.Forms.Label
        Me.LblCurrentStockText = New System.Windows.Forms.Label
        Me.LblTotalMeasure = New System.Windows.Forms.Label
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
        Me.RbtnForStock = New System.Windows.Forms.RadioButton
        Me.RbtIssueDirect = New System.Windows.Forms.RadioButton
        Me.RbtIssueForReqisition = New System.Windows.Forms.RadioButton
        Me.BtnImprtFromText = New System.Windows.Forms.Button
        Me.ChkShowOnlyImportedRecords = New System.Windows.Forms.CheckBox
        Me.TxtReason = New AgControls.AgTextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.LblTotalAmountValue = New System.Windows.Forms.Label
        Me.LbLTotalAmount = New System.Windows.Forms.Label
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
        Me.GroupBox2.Location = New System.Drawing.Point(733, 531)
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
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(582, 531)
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
        Me.GBoxApprove.Location = New System.Drawing.Point(415, 531)
        Me.GBoxApprove.Size = New System.Drawing.Size(148, 40)
        Me.GBoxApprove.Text = "Approved By"
        '
        'TxtApproveBy
        '
        Me.TxtApproveBy.Location = New System.Drawing.Point(29, 19)
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
        Me.GBoxEntryType.Location = New System.Drawing.Point(150, 531)
        Me.GBoxEntryType.Size = New System.Drawing.Size(119, 40)
        '
        'TxtEntryType
        '
        Me.TxtEntryType.Location = New System.Drawing.Point(3, 19)
        Me.TxtEntryType.Tag = ""
        '
        'GrpUP
        '
        Me.GrpUP.Location = New System.Drawing.Point(16, 531)
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
        Me.GroupBox1.Location = New System.Drawing.Point(2, 527)
        Me.GroupBox1.Size = New System.Drawing.Size(907, 4)
        '
        'GBoxDivision
        '
        Me.GBoxDivision.Location = New System.Drawing.Point(285, 531)
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
        Me.Label2.Location = New System.Drawing.Point(295, 47)
        Me.Label2.Tag = ""
        '
        'LblV_Date
        '
        Me.LblV_Date.BackColor = System.Drawing.Color.Transparent
        Me.LblV_Date.Location = New System.Drawing.Point(186, 42)
        Me.LblV_Date.Tag = ""
        Me.LblV_Date.Text = "Issue Date"
        '
        'LblV_TypeReq
        '
        Me.LblV_TypeReq.Location = New System.Drawing.Point(525, 27)
        Me.LblV_TypeReq.Tag = ""
        '
        'TxtV_Date
        '
        Me.TxtV_Date.AgSelectedValue = ""
        Me.TxtV_Date.BackColor = System.Drawing.Color.White
        Me.TxtV_Date.Location = New System.Drawing.Point(313, 41)
        Me.TxtV_Date.Size = New System.Drawing.Size(120, 18)
        Me.TxtV_Date.TabIndex = 2
        Me.TxtV_Date.Tag = ""
        '
        'LblV_Type
        '
        Me.LblV_Type.Location = New System.Drawing.Point(439, 23)
        Me.LblV_Type.Tag = ""
        Me.LblV_Type.Text = "Issue Type"
        '
        'TxtV_Type
        '
        Me.TxtV_Type.AgSelectedValue = ""
        Me.TxtV_Type.BackColor = System.Drawing.Color.White
        Me.TxtV_Type.Location = New System.Drawing.Point(541, 21)
        Me.TxtV_Type.Size = New System.Drawing.Size(187, 18)
        Me.TxtV_Type.TabIndex = 1
        Me.TxtV_Type.Tag = ""
        '
        'LblSite_CodeReq
        '
        Me.LblSite_CodeReq.Location = New System.Drawing.Point(295, 27)
        Me.LblSite_CodeReq.Tag = ""
        '
        'LblSite_Code
        '
        Me.LblSite_Code.BackColor = System.Drawing.Color.Transparent
        Me.LblSite_Code.Location = New System.Drawing.Point(186, 22)
        Me.LblSite_Code.Size = New System.Drawing.Size(87, 16)
        Me.LblSite_Code.Tag = ""
        Me.LblSite_Code.Text = "Branch Name"
        '
        'TxtSite_Code
        '
        Me.TxtSite_Code.AgSelectedValue = ""
        Me.TxtSite_Code.BackColor = System.Drawing.Color.White
        Me.TxtSite_Code.Location = New System.Drawing.Point(313, 21)
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
        Me.TabControl1.Size = New System.Drawing.Size(907, 173)
        Me.TabControl1.TabIndex = 0
        '
        'TP1
        '
        Me.TP1.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.TP1.Controls.Add(Me.TxtReason)
        Me.TP1.Controls.Add(Me.Label6)
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
        Me.TP1.Size = New System.Drawing.Size(899, 147)
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
        Me.TP1.Controls.SetChildIndex(Me.Label6, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtReason, 0)
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
        Me.TxtFromGodown.Location = New System.Drawing.Point(541, 61)
        Me.TxtFromGodown.MaxLength = 20
        Me.TxtFromGodown.Name = "TxtFromGodown"
        Me.TxtFromGodown.Size = New System.Drawing.Size(187, 18)
        Me.TxtFromGodown.TabIndex = 5
        '
        'LblGodown
        '
        Me.LblGodown.AutoSize = True
        Me.LblGodown.BackColor = System.Drawing.Color.Transparent
        Me.LblGodown.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblGodown.Location = New System.Drawing.Point(440, 61)
        Me.LblGodown.Name = "LblGodown"
        Me.LblGodown.Size = New System.Drawing.Size(55, 16)
        Me.LblGodown.TabIndex = 706
        Me.LblGodown.Text = "Godown"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Cornsilk
        Me.Panel1.Controls.Add(Me.LblTotalAmountValue)
        Me.Panel1.Controls.Add(Me.LbLTotalAmount)
        Me.Panel1.Controls.Add(Me.LblCurrentStock)
        Me.Panel1.Controls.Add(Me.LblTotalMeasureValue)
        Me.Panel1.Controls.Add(Me.LblCurrentStockText)
        Me.Panel1.Controls.Add(Me.LblTotalMeasure)
        Me.Panel1.Controls.Add(Me.LblTotalQty)
        Me.Panel1.Controls.Add(Me.LblTotalQtyText)
        Me.Panel1.Location = New System.Drawing.Point(5, 504)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(879, 23)
        Me.Panel1.TabIndex = 694
        '
        'LblCurrentStock
        '
        Me.LblCurrentStock.AutoSize = True
        Me.LblCurrentStock.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCurrentStock.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblCurrentStock.Location = New System.Drawing.Point(119, 3)
        Me.LblCurrentStock.Name = "LblCurrentStock"
        Me.LblCurrentStock.Size = New System.Drawing.Size(12, 16)
        Me.LblCurrentStock.TabIndex = 660
        Me.LblCurrentStock.Text = "."
        Me.LblCurrentStock.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTotalMeasureValue
        '
        Me.LblTotalMeasureValue.AutoSize = True
        Me.LblTotalMeasureValue.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalMeasureValue.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalMeasureValue.Location = New System.Drawing.Point(512, 3)
        Me.LblTotalMeasureValue.Name = "LblTotalMeasureValue"
        Me.LblTotalMeasureValue.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalMeasureValue.TabIndex = 666
        Me.LblTotalMeasureValue.Text = "."
        Me.LblTotalMeasureValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblCurrentStockText
        '
        Me.LblCurrentStockText.AutoSize = True
        Me.LblCurrentStockText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCurrentStockText.ForeColor = System.Drawing.Color.Maroon
        Me.LblCurrentStockText.Location = New System.Drawing.Point(11, 3)
        Me.LblCurrentStockText.Name = "LblCurrentStockText"
        Me.LblCurrentStockText.Size = New System.Drawing.Size(102, 16)
        Me.LblCurrentStockText.TabIndex = 659
        Me.LblCurrentStockText.Text = "Current Stock :"
        '
        'LblTotalMeasure
        '
        Me.LblTotalMeasure.AutoSize = True
        Me.LblTotalMeasure.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalMeasure.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalMeasure.Location = New System.Drawing.Point(401, 3)
        Me.LblTotalMeasure.Name = "LblTotalMeasure"
        Me.LblTotalMeasure.Size = New System.Drawing.Size(105, 16)
        Me.LblTotalMeasure.TabIndex = 665
        Me.LblTotalMeasure.Text = "Total Measure :"
        '
        'LblTotalQty
        '
        Me.LblTotalQty.AutoSize = True
        Me.LblTotalQty.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalQty.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalQty.Location = New System.Drawing.Point(315, 3)
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
        Me.LblTotalQtyText.Location = New System.Drawing.Point(230, 3)
        Me.LblTotalQtyText.Name = "LblTotalQtyText"
        Me.LblTotalQtyText.Size = New System.Drawing.Size(72, 16)
        Me.LblTotalQtyText.TabIndex = 659
        Me.LblTotalQtyText.Text = "Total Qty :"
        '
        'Pnl1
        '
        Me.Pnl1.Location = New System.Drawing.Point(4, 212)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(880, 292)
        Me.Pnl1.TabIndex = 1
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(186, 122)
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
        Me.TxtRemarks.Location = New System.Drawing.Point(313, 121)
        Me.TxtRemarks.MaxLength = 255
        Me.TxtRemarks.Name = "TxtRemarks"
        Me.TxtRemarks.Size = New System.Drawing.Size(415, 18)
        Me.TxtRemarks.TabIndex = 8
        '
        'LblFromGodownReq
        '
        Me.LblFromGodownReq.AutoSize = True
        Me.LblFromGodownReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblFromGodownReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblFromGodownReq.Location = New System.Drawing.Point(525, 68)
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
        Me.TxtManualRefNo.Location = New System.Drawing.Point(541, 41)
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
        Me.LblManualRefNo.Location = New System.Drawing.Point(439, 41)
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
        Me.LblMaterialPlanForFollowingItems.Location = New System.Drawing.Point(4, 191)
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
        Me.Label1.Location = New System.Drawing.Point(525, 47)
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
        Me.LblReq_SubCode.Location = New System.Drawing.Point(295, 86)
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
        Me.TxtParty.Location = New System.Drawing.Point(313, 81)
        Me.TxtParty.MaxLength = 20
        Me.TxtParty.Name = "TxtParty"
        Me.TxtParty.Size = New System.Drawing.Size(415, 18)
        Me.TxtParty.TabIndex = 6
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(186, 83)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(88, 16)
        Me.Label4.TabIndex = 734
        Me.Label4.Text = "Issue To (A/c)"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(295, 68)
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
        Me.TxtProcess.Location = New System.Drawing.Point(313, 61)
        Me.TxtProcess.MaxLength = 20
        Me.TxtProcess.Name = "TxtProcess"
        Me.TxtProcess.Size = New System.Drawing.Size(121, 18)
        Me.TxtProcess.TabIndex = 4
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(186, 62)
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
        Me.LblReqNoofPerson.Location = New System.Drawing.Point(738, 61)
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
        Me.BtnFillIssueDetail.Location = New System.Drawing.Point(522, 191)
        Me.BtnFillIssueDetail.Margin = New System.Windows.Forms.Padding(0)
        Me.BtnFillIssueDetail.Name = "BtnFillIssueDetail"
        Me.BtnFillIssueDetail.Size = New System.Drawing.Size(28, 19)
        Me.BtnFillIssueDetail.TabIndex = 806
        Me.BtnFillIssueDetail.TabStop = False
        Me.BtnFillIssueDetail.Text = "...."
        Me.BtnFillIssueDetail.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnFillIssueDetail.UseVisualStyleBackColor = False
        '
        'GrpDirectIssue
        '
        Me.GrpDirectIssue.BackColor = System.Drawing.Color.Transparent
        Me.GrpDirectIssue.Controls.Add(Me.RbtnForStock)
        Me.GrpDirectIssue.Controls.Add(Me.RbtIssueDirect)
        Me.GrpDirectIssue.Controls.Add(Me.RbtIssueForReqisition)
        Me.GrpDirectIssue.Location = New System.Drawing.Point(117, 182)
        Me.GrpDirectIssue.Name = "GrpDirectIssue"
        Me.GrpDirectIssue.Size = New System.Drawing.Size(402, 28)
        Me.GrpDirectIssue.TabIndex = 805
        Me.GrpDirectIssue.TabStop = False
        '
        'RbtnForStock
        '
        Me.RbtnForStock.AutoSize = True
        Me.RbtnForStock.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RbtnForStock.Location = New System.Drawing.Point(121, 8)
        Me.RbtnForStock.Name = "RbtnForStock"
        Me.RbtnForStock.Size = New System.Drawing.Size(87, 17)
        Me.RbtnForStock.TabIndex = 744
        Me.RbtnForStock.TabStop = True
        Me.RbtnForStock.Text = "For Stock"
        Me.RbtnForStock.UseVisualStyleBackColor = True
        '
        'RbtIssueDirect
        '
        Me.RbtIssueDirect.AutoSize = True
        Me.RbtIssueDirect.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RbtIssueDirect.Location = New System.Drawing.Point(9, 8)
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
        Me.RbtIssueForReqisition.Location = New System.Drawing.Point(231, 8)
        Me.RbtIssueForReqisition.Name = "RbtIssueForReqisition"
        Me.RbtIssueForReqisition.Size = New System.Drawing.Size(163, 17)
        Me.RbtIssueForReqisition.TabIndex = 0
        Me.RbtIssueForReqisition.TabStop = True
        Me.RbtIssueForReqisition.Text = "Issue For Requisition"
        Me.RbtIssueForReqisition.UseVisualStyleBackColor = True
        '
        'BtnImprtFromText
        '
        Me.BtnImprtFromText.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnImprtFromText.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnImprtFromText.Location = New System.Drawing.Point(811, 184)
        Me.BtnImprtFromText.Name = "BtnImprtFromText"
        Me.BtnImprtFromText.Size = New System.Drawing.Size(70, 25)
        Me.BtnImprtFromText.TabIndex = 807
        Me.BtnImprtFromText.TabStop = False
        Me.BtnImprtFromText.Text = "Import"
        Me.BtnImprtFromText.UseVisualStyleBackColor = True
        '
        'ChkShowOnlyImportedRecords
        '
        Me.ChkShowOnlyImportedRecords.AutoSize = True
        Me.ChkShowOnlyImportedRecords.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkShowOnlyImportedRecords.Location = New System.Drawing.Point(582, 192)
        Me.ChkShowOnlyImportedRecords.Name = "ChkShowOnlyImportedRecords"
        Me.ChkShowOnlyImportedRecords.Size = New System.Drawing.Size(214, 17)
        Me.ChkShowOnlyImportedRecords.TabIndex = 808
        Me.ChkShowOnlyImportedRecords.Text = "Show Only Imported Records"
        Me.ChkShowOnlyImportedRecords.UseVisualStyleBackColor = True
        '
        'TxtReason
        '
        Me.TxtReason.AgAllowUserToEnableMasterHelp = False
        Me.TxtReason.AgLastValueTag = Nothing
        Me.TxtReason.AgLastValueText = Nothing
        Me.TxtReason.AgMandatory = False
        Me.TxtReason.AgMasterHelp = False
        Me.TxtReason.AgNumberLeftPlaces = 8
        Me.TxtReason.AgNumberNegetiveAllow = False
        Me.TxtReason.AgNumberRightPlaces = 2
        Me.TxtReason.AgPickFromLastValue = False
        Me.TxtReason.AgRowFilter = ""
        Me.TxtReason.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtReason.AgSelectedValue = Nothing
        Me.TxtReason.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtReason.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtReason.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtReason.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtReason.Location = New System.Drawing.Point(313, 101)
        Me.TxtReason.MaxLength = 100
        Me.TxtReason.Name = "TxtReason"
        Me.TxtReason.Size = New System.Drawing.Size(415, 18)
        Me.TxtReason.TabIndex = 7
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(186, 103)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(52, 16)
        Me.Label6.TabIndex = 741
        Me.Label6.Text = "Reason"
        '
        'LblTotalAmountValue
        '
        Me.LblTotalAmountValue.AutoSize = True
        Me.LblTotalAmountValue.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalAmountValue.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalAmountValue.Location = New System.Drawing.Point(724, 3)
        Me.LblTotalAmountValue.Name = "LblTotalAmountValue"
        Me.LblTotalAmountValue.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalAmountValue.TabIndex = 668
        Me.LblTotalAmountValue.Text = "."
        Me.LblTotalAmountValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LbLTotalAmount
        '
        Me.LbLTotalAmount.AutoSize = True
        Me.LbLTotalAmount.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LbLTotalAmount.ForeColor = System.Drawing.Color.Maroon
        Me.LbLTotalAmount.Location = New System.Drawing.Point(613, 3)
        Me.LbLTotalAmount.Name = "LbLTotalAmount"
        Me.LbLTotalAmount.Size = New System.Drawing.Size(100, 16)
        Me.LbLTotalAmount.TabIndex = 667
        Me.LbLTotalAmount.Text = "Total Amount :"
        '
        'FrmStoreIssue
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.BackColor = System.Drawing.SystemColors.ButtonShadow
        Me.ClientSize = New System.Drawing.Size(889, 572)
        Me.Controls.Add(Me.ChkShowOnlyImportedRecords)
        Me.Controls.Add(Me.BtnImprtFromText)
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
        Me.Controls.SetChildIndex(Me.BtnImprtFromText, 0)
        Me.Controls.SetChildIndex(Me.ChkShowOnlyImportedRecords, 0)
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
        Me.PerformLayout()

    End Sub
    Protected WithEvents TxtFromGodown As AgControls.AgTextBox
    Protected WithEvents LblGodown As System.Windows.Forms.Label
    Protected WithEvents Panel1 As System.Windows.Forms.Panel
    Protected WithEvents LblTotalQty As System.Windows.Forms.Label
    Protected WithEvents LblTotalQtyText As System.Windows.Forms.Label
    Protected WithEvents Pnl1 As System.Windows.Forms.Panel
    Protected WithEvents LblTotalMeasureValue As System.Windows.Forms.Label
    Protected WithEvents TxtRemarks As AgControls.AgTextBox
    Protected WithEvents Label30 As System.Windows.Forms.Label
    Protected WithEvents LblFromGodownReq As System.Windows.Forms.Label
    Protected WithEvents LblTotalMeasure As System.Windows.Forms.Label
    Protected WithEvents TxtManualRefNo As AgControls.AgTextBox
    Protected WithEvents LblManualRefNo As System.Windows.Forms.Label
    Protected WithEvents LblMaterialPlanForFollowingItems As System.Windows.Forms.LinkLabel
    Protected WithEvents Label1 As System.Windows.Forms.Label
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
    Protected WithEvents LblCurrentStock As System.Windows.Forms.Label
    Protected WithEvents LblCurrentStockText As System.Windows.Forms.Label
#End Region

    Private Sub FrmStoreIssue_BaseEvent_ApproveDeletion_InTrans(ByVal SearchCode As String, ByVal Conn As System.Data.SqlClient.SqlConnection, ByVal Cmd As System.Data.SqlClient.SqlCommand) Handles Me.BaseEvent_ApproveDeletion_InTrans
        Dim I As Integer = 0
        For I = 0 To Dgl1.Rows.Count - 1
            If Dgl1.Item(Col1Item_UID, I).Tag <> "" Then
                AgTemplate.ClsMain.FUpdateItem_UidOnDelete(Dgl1.Item(Col1Item_UID, I).Tag, mSearchCode, Conn, Cmd)
            End If
        Next

        If CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsPostedInStockProcess")), Boolean) Then
            mQry = "Delete From StockProcess Where DocId = '" & SearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If

        mQry = " Delete from JobIssRecUid Where DocId = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    End Sub

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

        If IsApplyVTypePermission Then
            mCondStr = mCondStr & " And H.V_Type In (Select V_Type From User_VType_Permission VP Where VP.UserName = '" & AgL.PubUserName & "' And VP.Div_Code = '" & AgL.PubDivCode & "' And VP.Site_Code = '" & AgL.PubSiteCode & "') "
        End If

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

        If IsApplyVTypePermission Then
            mCondStr = mCondStr & " And H.V_Type In (Select V_Type From User_VType_Permission VP Where VP.UserName = '" & AgL.PubUserName & "' And VP.Div_Code = '" & AgL.PubDivCode & "' And VP.Site_Code = '" & AgL.PubSiteCode & "') "
        End If

        mQry = " Select H.DocID As SearchCode " & _
            " From StockHead H " & _
            " Left Join Voucher_Type Vt On H.V_Type = Vt.V_Type  " & _
            " Where IsNull(IsDeleted,0) = 0  " & mCondStr & "  Order By H.V_Date, H.V_No  "

        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub FrmSaleOrder_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl1, Col1Item_UID, 100, 0, Col1Item_UID, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_ItemUID")), Boolean), False, False)
            .AddAgTextColumn(Dgl1, Col1ItemCode, 100, 0, Col1ItemCode, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_ItemCode")), Boolean), False, False)
            .AddAgTextColumn(Dgl1, Col1Item, 250, 0, Col1Item, True, False, False)
            .AddAgTextColumn(Dgl1, Col1ItemGroup, 100, 0, Col1ItemGroup, True, True)
            .AddAgTextColumn(Dgl1, Col1FromProcess, 100, 0, Col1FromProcess, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_ProcessLine")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_ProcessLine")), Boolean), False)
            .AddAgTextColumn(Dgl1, Col1Dimension1, 100, 0, AgTemplate.ClsMain.FGetDimension1Caption(), CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Dimension1")), Boolean), False)
            .AddAgTextColumn(Dgl1, Col1Dimension2, 100, 0, AgTemplate.ClsMain.FGetDimension2Caption(), CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Dimension2")), Boolean), False)
            .AddAgTextColumn(Dgl1, Col1Specification, 100, 0, Col1Specification, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Specification")), Boolean), False, False)
            .AddAgTextColumn(Dgl1, Col1LotNo, 100, 0, Col1LotNo, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_LotNo")), Boolean), False, False)
            .AddAgTextColumn(Dgl1, Col1BaleNo, 100, 0, Col1BaleNo, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_BaleNo")), Boolean), False, False)
            .AddAgTextColumn(Dgl1, Col1RequisitionNo, 80, 0, Col1RequisitionNo, False, True, False)
            .AddAgNumberColumn(Dgl1, Col1RequisitionSr, 50, 8, 4, False, Col1RequisitionSr, False, True, True)
            .AddAgNumberColumn(Dgl1, Col1CurrentStock, 80, 8, 4, False, Col1CurrentStock, False, True, True)
            .AddAgNumberColumn(Dgl1, Col1Qty, 100, 8, 4, False, Col1Qty, True, False, True)
            .AddAgTextColumn(Dgl1, Col1Unit, 50, 0, Col1Unit, True, True, False)
            .AddAgTextColumn(Dgl1, Col1CostCenter, 120, 0, Col1CostCenter, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_CostCenter")), Boolean), False, False)
            .AddAgTextColumn(Dgl1, Col1QtyDecimalPlaces, 50, 0, Col1QtyDecimalPlaces, False, True, False)
            .AddAgNumberColumn(Dgl1, Col1MeasurePerPcs, 70, 8, 3, False, Col1MeasurePerPcs, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_MeasurePerPcs")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_MeasurePerPcs")), Boolean), True)
            .AddAgNumberColumn(Dgl1, Col1TotalMeasure, 70, 8, 3, False, Col1TotalMeasure, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Measure")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_Measure")), Boolean), True)
            .AddAgTextColumn(Dgl1, Col1MeasureUnit, 50, 0, Col1MeasureUnit, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_MeasureUnit")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_MeasureUnit")), Boolean))
            .AddAgTextColumn(Dgl1, Col1MeasureDecimalPlaces, 50, 0, Col1MeasureDecimalPlaces, False, True, False)
            .AddAgNumberColumn(Dgl1, Col1Rate, 90, 8, 2, False, Col1Rate, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Rate")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_Rate")), Boolean), True)
            .AddAgNumberColumn(Dgl1, Col1Amount, 90, 8, 2, False, Col1Amount, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Amount")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_Amount")), Boolean), True)
            .AddAgTextColumn(Dgl1, Col1Remarks, 250, 0, Col1Remarks, True, False, False)
            .AddAgTextColumn(Dgl1, Col1VNature, 100, 0, Col1VNature, False, True, False)
        End With

        If CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Measure")), Boolean) Then
            LblTotalMeasure.Visible = True
            LblTotalMeasureValue.Visible = True
        Else
            LblTotalMeasure.Visible = False
            LblTotalMeasureValue.Visible = False
        End If

        If CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Amount")), Boolean) Then
            LbLTotalAmount.Visible = True
            LblTotalAmountValue.Visible = True
        Else
            LbLTotalAmount.Visible = False
            LblTotalAmountValue.Visible = False
        End If

        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False

        Dgl1.ColumnHeadersHeight = 35

        Dgl1.AgSkipReadOnlyColumns = True
        Dgl1.AllowUserToOrderColumns = True

        AgCL.GridSetiingShowXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1, False)
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As System.Data.SqlClient.SqlConnection, ByVal Cmd As System.Data.SqlClient.SqlCommand) Handles Me.BaseEvent_Save_InTrans
        Dim I As Integer, mSr As Integer

        mQry = "UPDATE StockHead " & _
                " SET " & _
                " TotalQty = " & Val(LblTotalQty.Text) & ", " & _
                " TotalMeasure = " & Val(LblTotalMeasureValue.Text) & ", " & _
                " Amount = " & Val(LblTotalAmountValue.Text) & ", " & _
                " SubCode = " & AgL.Chk_Text(TxtParty.Tag) & ", " & _
                " FromGodown = " & AgL.Chk_Text(TxtFromGodown.Tag) & ", " & _
                " Process = " & AgL.Chk_Text(TxtProcess.Tag) & ", " & _
                " Reason = " & AgL.Chk_Text(TxtReason.Tag) & ", " & _
                " ManualRefNo = " & AgL.Chk_Text(TxtManualRefNo.Text) & ", " & _
                " Remarks = " & AgL.Chk_Text(TxtRemarks.Text) & " " & _
                " Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        If Topctrl1.Mode <> "Add" Then
            mQry = " SELECT Item_UID FROM StockHeadDetail With (NoLock) WHERE DocId = '" & mSearchCode & "' And Item_Uid Is Not Null "
            Dim DtItem_Uid As DataTable = AgL.FillData(mQry, AgL.GcnRead).Tables(0)
            If DtItem_Uid.Rows.Count > 0 Then
                For I = 0 To DtItem_Uid.Rows.Count - 1
                    AgTemplate.ClsMain.FUpdateItem_UidOnDelete(DtItem_Uid.Rows(I)("Item_Uid"), mSearchCode, Conn, Cmd)
                Next
            End If
        End If

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

                mQry = " INSERT INTO dbo.StockHeadDetail ( DocID, Sr, Item_UID, Item, Process, Dimension1, Dimension2, Specification, LotNo, BaleNo, Godown, Qty, Unit, " & _
                        " MeasurePerPcs, TotalMeasure, MeasureUnit, Rate, Amount, Remarks,  " & _
                        " Requisition, RequisitionSr, CurrentStock, V_Nature, CostCenter) " & _
                        " VALUES (" & AgL.Chk_Text(mSearchCode) & ", " & _
                        " " & mSr & ", " & _
                        " " & AgL.Chk_Text(Dgl1.Item(Col1Item_UID, I).Tag) & ", " & _
                        " " & AgL.Chk_Text(Dgl1.AgSelectedValue(Col1Item, I)) & ", " & _
                        " " & AgL.Chk_Text(Dgl1.Item(Col1FromProcess, I).Tag) & ", " & _
                        " " & AgL.Chk_Text(Dgl1.Item(Col1Dimension1, I).Tag) & ", " & _
                        " " & AgL.Chk_Text(Dgl1.Item(Col1Dimension2, I).Tag) & ", " & _
                        " " & AgL.Chk_Text(Dgl1.Item(Col1Specification, I).Value) & ",  " & _
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
                        " " & AgL.Chk_Text(Dgl1.Item(Col1RequisitionNo, I).Tag) & ", " & Val(Dgl1.Item(Col1RequisitionSr, I).Value) & ", " & _
                        " " & Val(Dgl1.Item(Col1CurrentStock, I).Value) & ", " & _
                        " " & AgL.Chk_Text(Dgl1.Item(Col1VNature, I).Value) & ", " & _
                        " " & AgL.Chk_Text(Dgl1.Item(Col1CostCenter, I).Tag) & " " & _
                        "  ) "
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            End If
        Next

        FPostInJobIssRecUID(mSearchCode, Conn, Cmd)

        For I = 0 To Dgl1.Rows.Count - 1
            If Dgl1.Item(Col1Item_UID, I).Tag <> "" Then
                AgTemplate.ClsMain.FUpdateItem_Uid(Dgl1.Item(Col1Item_UID, I).Tag, Topctrl1.Mode, mSearchCode, TxtV_Type.Tag, TxtV_Date.Text, TxtParty.Tag, "", TxtProcess.Tag, AgTemplate.ClsMain.Item_UidStatus.Issue, TxtManualRefNo.Text, Conn, Cmd)
            End If
        Next


        'Code For Stock Posting Process Wise
        'If AgL.StrCmp(TxtV_Type.Tag, "CAISS") Then
        '    Dim StockView$ = ""
        '    StockView = " Select L.DocID, L.Sr, H.V_Type, " & _
        '                " H.V_Prefix, H.V_Date, H.V_No, H.ManualRefNo As RecId, H.Div_Code, " & _
        '                " H.Site_Code,   " & _
        '                " H.SubCode As SubCode, L.Item, H.FromGodown As Godown, L.Qty, L.Unit, L.MeasurePerPcs, " & _
        '                " L.TotalMeasure, L.MeasureUnit, L.Process As Process " & _
        '                " From StockHead As H With (NoLock)  " & _
        '                " LEFT JOIN StockHeadDetail As L With (NoLock) On H.DocId = L.DocId " & _
        '                " Where H.DocId = '" & mInternalCode & "' "
        '    AgTemplate.ClsMain.FPostInStockWithProcess(StockView, mInternalCode, TxtFromGodown.Tag, TxtV_Date.Text, Conn, Cmd)
        'Else

        '-------------Qry Was Written For Managing Process Wise Stock For Surya Carpet
        'But After physical stock it is turned into 

        mQry = " INSERT INTO Stock (DocId, Sr, V_Type, V_Prefix,  V_Date, V_No, RecID, Div_Code, Site_Code, SubCode, " & _
                " Item, Dimension1, Dimension2, Manufacturer, Godown, Qty_Iss, Unit,  MeasurePerPcs, Measure_Iss, MeasureUnit,  Rate, Amount, Landed_Value, EType_IR, " & _
                " Cost, LotNo, BaleNo, Process, Remarks, ReferenceDocId, ReferenceDocIdSr)  " & _
                " SELECT H.DocID, row_number() OVER (ORDER BY L.Item), max(H.V_Type) AS V_Type, max(H.V_Prefix) AS V_Prefix, max(H.V_Date) AS V_Date, max(H.V_No) AS V_No, Max(H.ManualRefNo) AS RecId, " & _
                " max(H.Div_Code) AS Div_Code, max(H.Site_Code) AS Site_Code, max(H.SubCode) AS SubCode, L.Item, L.Dimension1, L.Dimension2, L.Manufacturer, max(H.FromGodown) AS Godown, " & _
                " sum(L.Qty) AS Qty_Iss, Max(L.Unit) AS Unit, max(L.MeasurePerPcs) AS  MeasurePerPcs, sum(L.TotalMeasure) AS Measure_Iss, max(L.MeasureUnit) AS MeasureUnit, max(L.Rate) AS Rate, " & _
                " sum(L.Amount) AS Amount, sum(L.Amount) AS Amount, 'I', max(L.CostCenter) AS Cost, L.LotNo, L.BaleNo, L.Process, max(H.Remarks) AS Remarks, H.DocID, row_number() OVER (ORDER BY L.Item) " & _
                " FROM StockHeadDetail L " & _
                " LEFT JOIN StockHead H ON H.DocID = L.DocID " & _
                " WHERE H.DocID = " & AgL.Chk_Text(mSearchCode) & " " & _
                " GROUP BY H.DocID, L.Item, L.Dimension1, L.Dimension2, L.Manufacturer, L.LotNo,L.BaleNo, L.Process "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        If CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsPostedInStockProcess")), Boolean) Then
            mQry = " INSERT INTO StockProcess (DocId, Sr, V_Type, V_Prefix,  V_Date, V_No, RecID, Div_Code, Site_Code, SubCode, " & _
                    " Item, Dimension1, Dimension2, Manufacturer, Godown, Qty_Rec, Unit,  MeasurePerPcs, Measure_Rec, MeasureUnit,  Rate, Amount, " & _
                    " Cost, LotNo, BaleNo, Process, Remarks, ReferenceDocId, ReferenceDocIdSr)  " & _
                    " SELECT H.DocID, row_number() OVER (ORDER BY L.Item), max(H.V_Type) AS V_Type, max(H.V_Prefix) AS V_Prefix, max(H.V_Date) AS V_Date, max(H.V_No) AS V_No, Max(H.ManualRefNo) AS RecId, " & _
                    " max(H.Div_Code) AS Div_Code, max(H.Site_Code) AS Site_Code, max(H.SubCode) AS SubCode, L.Item, L.Dimension1, L.Dimension2, L.Manufacturer, max(H.FromGodown) AS Godown, " & _
                    " sum(L.Qty) AS Qty_Rec, Max(L.Unit) AS Unit, max(L.MeasurePerPcs) AS  MeasurePerPcs, sum(L.TotalMeasure) AS Measure_Rec, max(L.MeasureUnit) AS MeasureUnit, max(L.Rate) AS Rate, " & _
                    " sum(L.Amount) AS Amount, max(L.CostCenter) AS Cost, L.LotNo, L.BaleNo, H.Process, max(H.Remarks) AS Remarks, H.DocID, row_number() OVER (ORDER BY L.Item) " & _
                    " FROM StockHeadDetail L " & _
                    " LEFT JOIN StockHead H ON H.DocID = L.DocID " & _
                    " WHERE H.DocID = " & AgL.Chk_Text(mSearchCode) & " " & _
                    " GROUP BY H.DocID, L.Item, L.Dimension1, L.Dimension2, L.Manufacturer, L.LotNo,L.BaleNo, H.Process "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If

        'End If
        'Code End For Stock Posting Process Wise

        If AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Or AgL.StrCmp(AgL.PubUserName, "Sa") Then
            AgCL.GridSetiingWriteXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1)
        End If
    End Sub

    Private Sub FPostInJobIssRecUID(ByVal SearchCode As String, ByVal Conn As System.Data.SqlClient.SqlConnection, ByVal Cmd As System.Data.SqlClient.SqlCommand)
        Dim I As Integer = 0, bSr As Integer = 0

        mQry = "Delete from JobIssRecUID Where DocId ='" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " INSERT INTO JobIssRecUID(DocID, TSr, Sr, IssRec, Process, Item, Item_UID, " & _
                 " Godown, Site_Code, V_Date, V_Type, SubCode, Div_Code, RecId, EntryDate, Remark) " & _
                 " Select L.DocId, L.Sr As TSr, L.Sr, 'I', " & _
                 " H.Process, L.Item, L.Item_Uid, " & _
                 " H.FromGodown, H.Site_Code, H.V_Date, H.V_Type, H.SubCode, H.Div_Code, H.ManualRefNo, H.EntryDate, " & _
                 " SubString(IsNull(H.Remarks,'') + '.' + IsNull(L.Remarks,''),0,255) " & _
                 " From (Select * From StockHeadDetail With (NoLock) Where DocId = '" & mSearchCode & "' And Item_Uid Is Not Null) As L " & _
                 " LEFT JOIN StockHead H With (NoLock) On L.DocId = H.DocId "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    End Sub

    Private Sub FrmProductionOrder_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim I As Integer

        Dim DsTemp As DataSet

        mQry = "Select H.*, G.Description as FromGodownDesc, P.Description as ProcessDesc, R.Description As ReasonDesc, " & _
               " Sg.Name + (Case When IsNull(Sg.CityCode,'')<>'' Then ', ' + C.CityName Else '' End) as PartyName " & _
                " From StockHead H " & _
                " Left Join Godown G on H.FromGodown = G.Code " & _
                " Left Join Reason R on H.Reason = R.Code " & _
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
                TxtReason.Tag = AgL.XNull(.Rows(0)("Reason"))
                TxtReason.Text = AgL.XNull(.Rows(0)("ReasonDesc"))
                TxtParty.Tag = AgL.XNull(.Rows(0)("SubCode"))
                TxtParty.Text = AgL.XNull(.Rows(0)("PartyName"))
                TxtManualRefNo.Text = AgL.XNull(.Rows(0)("ManualRefNo"))
                TxtRemarks.Text = AgL.XNull(.Rows(0)("Remarks"))
                LblTotalQty.Text = AgL.VNull(.Rows(0)("TotalQty"))
                LblTotalMeasureValue.Text = AgL.VNull(.Rows(0)("TotalMeasure"))
                LblTotalAmountValue.Text = AgL.VNull(.Rows(0)("Amount"))
                IniGrid()
                '-------------------------------------------------------------
                'Line Records are showing in Grid
                '-------------------------------------------------------------

                mQry = "Select S.*, I.ManualCode as Item_No, Iu.Item_Uid As Item_UidDesc, I.Description as Item_Desc, R.ReferenceNo AS ReqNo, " & _
                       "U.DecimalPlaces as QtyDecimalPlaces, MU.DecimalPlaces as MeasureDecimalPlaces, IG.Description AS ItemGroupDesc," & _
                       "P.Description as ProcessDesc, Cm.Name As CostCenterName,  " & _
                       "D1.Description As Dimension1Desc, D2.Description As Dimension2Desc " & _
                       "from (Select * From StockHeadDetail where DocId = '" & SearchCode & "') S " & _
                       "Left Join Item I On S.Item = I.Code " & _
                       "Left Join ItemGroup IG On I.ItemGroup = IG.Code " & _
                       "LEFT JOIN Item_Uid Iu ON S.Item_Uid = Iu.Code " & _
                       "Left Join Unit U On I.Unit = U.Code " & _
                       "Left Join Unit MU On I.MeasureUnit = MU.Code " & _
                       "Left Join Dimension1 D1 With (Nolock)  On S.Dimension1 = D1.Code " & _
                       "Left Join Dimension2 D2 With (Nolock)  On S.Dimension2 = D2.Code " & _
                       "Left Join Requisition R On S.Requisition = R.DocID " & _
                       "Left Join Process P On S.Process = P.NCat " & _
                       "Left Join CostCenterMast Cm On S.CostCenter = Cm.Code " & _
                       "Order By Sr"
                DsTemp = AgL.FillData(mQry, AgL.GCn)
                With DsTemp.Tables(0)
                    Dgl1.RowCount = 1
                    Dgl1.Rows.Clear()
                    If .Rows.Count > 0 Then
                        For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                            Dgl1.Rows.Add()
                            Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count - 1
                            Dgl1.Item(Col1Item_UID, I).Tag = AgL.XNull(.Rows(I)("Item_UID"))
                            Dgl1.Item(Col1Item_UID, I).Value = AgL.XNull(.Rows(I)("Item_UidDesc"))
                            Dgl1.Item(Col1ItemCode, I).Tag = AgL.XNull(.Rows(I)("Item"))
                            Dgl1.Item(Col1ItemCode, I).Value = AgL.XNull(.Rows(I)("Item_No"))
                            Dgl1.Item(Col1Item, I).Tag = AgL.XNull(.Rows(I)("Item"))
                            Dgl1.Item(Col1Item, I).Value = AgL.XNull(.Rows(I)("Item_Desc"))

                            Dgl1.Item(Col1FromProcess, I).Tag = AgL.XNull(.Rows(I)("Process"))
                            Dgl1.Item(Col1FromProcess, I).Value = AgL.XNull(.Rows(I)("ProcessDesc"))

                            Dgl1.Item(Col1Dimension1, I).Tag = AgL.XNull(.Rows(I)("Dimension1"))
                            Dgl1.Item(Col1Dimension1, I).Value = AgL.XNull(.Rows(I)("Dimension1Desc"))

                            Dgl1.Item(Col1Dimension2, I).Tag = AgL.XNull(.Rows(I)("Dimension2"))
                            Dgl1.Item(Col1Dimension2, I).Value = AgL.XNull(.Rows(I)("Dimension2Desc"))

                            Dgl1.Item(Col1Specification, I).Value = AgL.XNull(.Rows(I)("Specification"))
                            Dgl1.Item(Col1RequisitionNo, I).Tag = AgL.XNull(.Rows(I)("Requisition"))
                            Dgl1.Item(Col1RequisitionNo, I).Value = AgL.XNull(.Rows(I)("ReqNo"))
                            Dgl1.Item(Col1RequisitionSr, I).Value = AgL.VNull(.Rows(I)("RequisitionSr"))
                            Dgl1.Item(Col1VNature, I).Value = AgL.XNull(.Rows(I)("V_Nature"))
                            Dgl1.Item(Col1ItemGroup, I).Value = AgL.XNull(.Rows(I)("ItemGroupDesc"))
                            Dgl1.Item(Col1Qty, I).Value = Format(AgL.VNull(.Rows(I)("Qty")), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                            Dgl1.Item(Col1QtyDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("QtyDecimalPlaces"))
                            Dgl1.Item(Col1MeasurePerPcs, I).Value = Format(AgL.VNull(.Rows(I)("MeasurePerPcs")), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1TotalMeasure, I).Value = Format(AgL.VNull(.Rows(I)("TotalMeasure")), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1MeasureUnit, I).Value = AgL.XNull(.Rows(I)("MeasureUnit"))
                            Dgl1.Item(Col1MeasureDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("MeasureDecimalPlaces"))
                            Dgl1.Item(Col1Rate, I).Value = AgL.VNull(.Rows(I)("Rate"))
                            Dgl1.Item(Col1Amount, I).Value = AgL.VNull(.Rows(I)("Amount"))
                            Dgl1.Item(Col1LotNo, I).Value = AgL.XNull(.Rows(I)("LotNo"))
                            Dgl1.Item(Col1BaleNo, I).Value = AgL.XNull(.Rows(I)("BaleNo"))
                            Dgl1.Item(Col1Remarks, I).Value = AgL.XNull(.Rows(I)("Remarks"))
                            Dgl1.Item(Col1CurrentStock, I).Value = AgL.VNull(.Rows(I)("CurrentStock"))

                            Dgl1.Item(Col1CostCenter, I).Tag = AgL.XNull(.Rows(I)("CostCenter"))
                            Dgl1.Item(Col1CostCenter, I).Value = AgL.XNull(.Rows(I)("CostCenterName"))
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

    Private Sub TxtFromGodown_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtFromGodown.KeyDown, TxtParty.KeyDown, TxtProcess.KeyDown, TxtReason.KeyDown
        Select Case sender.Name
            Case TxtFromGodown.Name
                If e.KeyCode <> Keys.Enter Then
                    If sender.AgHelpDataset Is Nothing Then
                        mQry = "SELECT G.Code, G.Description " & _
                                " FROM Godown G " & _
                                " LEFT JOIN SiteMast Sm On G.Site_Code = Sm.Code  " & _
                                " Where G.Site_Code = '" & TxtSite_Code.AgSelectedValue & "'  " & _
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

            Case TxtReason.Name
                If e.KeyCode <> Keys.Enter Then
                    If sender.AgHelpDataSet Is Nothing Then
                        mQry = " SELECT H.Code, H.Description AS Reason FROM Reason H "
                        sender.AgHelpDataSet(0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
                    End If
                End If

            Case TxtProcess.Name
                If e.KeyCode <> Keys.Enter Then
                    If sender.AgHelpDataSet Is Nothing Then
                        If InStr(",", AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Process"))) <= 0 Then
                            mQry = "Select NCat, Description from Process Where NCat IN (" & Replace(AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Process")), "|", "'") & ")  "
                        Else
                            mQry = " SELECT H.NCat AS Code, H.Description AS Process FROM Process H "
                        End If
                        sender.AgHelpDataSet(0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
                    End If
                End If
        End Select
    End Sub

    Private Sub Txt_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtV_Type.Validating, TxtManualRefNo.Validating, TxtFromGodown.Validating, TxtParty.Validating
        Select Case sender.NAME
            Case TxtV_Type.Name
                TxtManualRefNo.Text = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "StockHead", TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, AgTemplate.ClsMain.ManualRefType.Max)
                IniGrid()
                FAsignProcess()
                TxtParty.AgHelpDataSet = Nothing
                Dgl1.AgHelpDataSet(Col1Item) = Nothing

                If AgL.StrCmp(Topctrl1.Mode, "Add") Then
                    TxtManualRefNo.Text = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "StockHead", TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, AgTemplate.ClsMain.ManualRefType.Max)
                End If

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
                Dgl1.AgHelpDataSet(Col1CostCenter) = Nothing
        End Select
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_Topctrl_tbAdd() Handles Me.BaseEvent_Topctrl_tbAdd
        TxtManualRefNo.Text = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "StockHead", TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, AgTemplate.ClsMain.ManualRefType.Max)
        If AgL.XNull(DtV_TypeSettings.Rows(0)("Default_Godown")) <> "" Then
            TxtFromGodown.Tag = AgL.XNull(DtV_TypeSettings.Rows(0)("Default_Godown"))
            TxtFromGodown.Text = AgL.Dman_Execute("Select Description from Godown Where Code = '" & AgL.XNull(DtV_TypeSettings.Rows(0)("Default_Godown")) & "' ", AgL.GCn).ExecuteScalar
        End If
        FAsignProcess()
        BtnImprtFromText.Text = ImportAction_NewImport

        TxtFromGodown.Tag = PubDefaultGodownCode
        TxtFromGodown.Text = PubDefaultGodownName
    End Sub

    Private Sub FAsignProcess()
        Dim DtTemp As DataTable = Nothing
        TxtProcess.Enabled = False
        If CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Process")), Boolean) Then
            If InStr(",", AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Process"))) <= 0 Then
                mQry = "Select NCat, Description from Process Where NCat IN (" & Replace(AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Process")), "|", "'") & ")  "
                DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
                If DtTemp.Rows.Count > 0 Then
                    If DtTemp.Rows.Count = 1 Then
                        TxtProcess.Tag = AgL.XNull(DtTemp.Rows(0)("NCat"))
                        TxtProcess.Text = AgL.XNull(DtTemp.Rows(0)("Description"))
                        TxtProcess.Enabled = False
                    Else
                        TxtProcess.Enabled = True
                        TxtProcess.Tag = ""
                        TxtProcess.Text = ""
                    End If
                End If
            Else
                TxtProcess.Enabled = True
                TxtProcess.Tag = ""
                TxtProcess.Text = ""
            End If
        Else
            TxtProcess.Enabled = False
            TxtProcess.Tag = ""
            TxtProcess.Text = ""
            TxtProcess.AgHelpDataSet = Nothing
        End If

        If TxtFromGodown.Tag = "" Then
            TxtFromGodown.Tag = AgL.XNull(DtV_TypeSettings.Rows(0)("DEFAULT_Godown"))
            TxtFromGodown.Text = AgL.XNull(AgL.Dman_Execute("SELECT Description  FROM Godown WHERE Code = " & AgL.Chk_Text(TxtFromGodown.Tag) & " ", AgL.GCn).ExecuteScalar)
        End If
    End Sub

    Private Sub Dgl1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellEnter
        If Dgl1.CurrentCell Is Nothing Then Exit Sub
        Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
            Case Col1Qty
                CType(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex), AgControls.AgTextColumn).AgNumberRightPlaces = Val(Dgl1.Item(Col1QtyDecimalPlaces, Dgl1.CurrentCell.RowIndex).Value)
            Case Col1MeasurePerPcs, Col1TotalMeasure
                CType(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex), AgControls.AgTextColumn).AgNumberRightPlaces = Val(Dgl1.Item(Col1MeasureDecimalPlaces, Dgl1.CurrentCell.RowIndex).Value)
            Case Col1LotNo
                Dgl1.AgHelpDataSet(Col1LotNo) = Nothing

            Case Col1CostCenter
                Try
                    If Dgl1.CurrentCell.RowIndex <> 0 Then
                        If Dgl1.Item(Col1CostCenter, Dgl1.CurrentCell.RowIndex).Value = "" Then
                            Dgl1.Item(Col1CostCenter, Dgl1.CurrentCell.RowIndex).Tag = Dgl1.Item(Col1CostCenter, Dgl1.CurrentCell.RowIndex - 1).Tag
                            Dgl1.Item(Col1CostCenter, Dgl1.CurrentCell.RowIndex).Value = Dgl1.Item(Col1CostCenter, Dgl1.CurrentCell.RowIndex - 1).Value
                        End If
                    End If
                Catch ex As Exception
                End Try

            Case Col1FromProcess
                If Dgl1.Item(Col1Item_UID, Dgl1.CurrentCell.RowIndex).Value <> "" Then
                    Dgl1.Item(Col1FromProcess, Dgl1.CurrentCell.RowIndex).ReadOnly = True
                Else
                    Dgl1.Item(Col1FromProcess, Dgl1.CurrentCell.RowIndex).ReadOnly = False
                End If
        End Select
    End Sub

    Private Sub Dgl1_EditingControl_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Dgl1.EditingControl_Validating
        If Topctrl1.Mode = "Browse" Then Exit Sub
        Dim mRowIndex As Integer, mColumnIndex As Integer
        Dim DrTemp As DataRow() = Nothing
        Dim I As Integer = 0
        Try
            mRowIndex = Dgl1.CurrentCell.RowIndex
            mColumnIndex = Dgl1.CurrentCell.ColumnIndex
            If Dgl1.Item(mColumnIndex, mRowIndex).Value Is Nothing Then Dgl1.Item(mColumnIndex, mRowIndex).Value = ""
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Item_UID
                    Validating_Item_Uid(Dgl1.Item(Col1Item_UID, mRowIndex).Value, mRowIndex)

                Case Col1Item
                    Validating_ItemCode(mColumnIndex, mRowIndex)
                    If mRowIndex > 0 Then
                        Dgl1.Item(Col1CostCenter, mRowIndex).Value = Dgl1.Item(Col1CostCenter, mRowIndex - 1).Value
                        Dgl1.Item(Col1CostCenter, mRowIndex).Tag = Dgl1.Item(Col1CostCenter, mRowIndex - 1).Tag
                    End If

                Case Col1ItemCode
                    Validating_ItemCode(mColumnIndex, mRowIndex)

                Case Col1LotNo
                    If Dgl1.AgHelpDataSet(Col1LotNo) IsNot Nothing Then
                        LblCurrentStock.Text = Format(AgTemplate.ClsMain.FunRetStock(Dgl1.Item(Col1Item, Dgl1.CurrentCell.RowIndex).Tag, mSearchCode, , TxtFromGodown.Tag, , , TxtV_Date.Text, Dgl1.Item(Col1LotNo, Dgl1.CurrentCell.RowIndex).Value), "0.".PadRight(Dgl1.Item(Col1QtyDecimalPlaces, Dgl1.CurrentCell.RowIndex).Value + 2, "0"))
                        Dgl1.Item(Col1Qty, mRowIndex).Value = Format(AgTemplate.ClsMain.FunRetStock(Dgl1.Item(Col1Item, Dgl1.CurrentCell.RowIndex).Tag, mSearchCode, , TxtFromGodown.Tag, , , TxtV_Date.Text, Dgl1.Item(Col1LotNo, Dgl1.CurrentCell.RowIndex).Value), "0.".PadRight(Dgl1.Item(Col1QtyDecimalPlaces, Dgl1.CurrentCell.RowIndex).Value + 2, "0"))
                        Validating_LotNo(Dgl1.Item(Col1Item, mRowIndex).Tag, mRowIndex)
                    End If

                Case Col1FromProcess
                    If Dgl1.Item(Col1FromProcess, mRowIndex).Value <> "" Then
                        If MsgBox("Apply To All ?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
                            For I = mRowIndex To Dgl1.Rows.Count - 1
                                If Dgl1.Item(Col1Item_UID, I).Value = "" And Dgl1.Item(Col1Item, I).Value <> "" Then
                                    Dgl1.Item(Col1FromProcess, I).Tag = Dgl1.Item(Col1FromProcess, mRowIndex).Tag
                                    Dgl1.Item(Col1FromProcess, I).Value = Dgl1.Item(Col1FromProcess, mRowIndex).Value
                                End If
                            Next
                        End If
                    End If
            End Select
            Call Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Validating_ItemCode(ByVal mColumn As Integer, ByVal mRow As Integer)
        Dim DrTemp As DataRow() = Nothing
        Dim DtTemp As DataTable = Nothing
        'Try
        If Dgl1.Item(mColumn, mRow).Value.ToString.Trim = "" Or Dgl1.AgSelectedValue(mColumn, mRow).ToString.Trim = "" Then
            Dgl1.Item(Col1Unit, mRow).Value = ""
            Dgl1.Item(Col1CurrentStock, mRow).Value = ""
        Else
            If Dgl1.AgDataRow IsNot Nothing Then
                Dgl1.Item(Col1Item, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("Item").Value)
                Dgl1.Item(Col1Item, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("ItemDesc").Value)
                Dgl1.Item(Col1ItemGroup, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("ItemGroupDesc").Value)
                Dgl1.Item(Col1RequisitionNo, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("RequisitionDocId").Value)
                Dgl1.Item(Col1RequisitionNo, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("RequisitionNo").Value)
                Dgl1.Item(Col1RequisitionSr, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("RequisitionSr").Value)
                Dgl1.Item(Col1Qty, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("Qty").Value)
                Dgl1.Item(Col1ItemCode, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("Item").Value)
                Dgl1.Item(Col1ItemCode, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("ItemCode").Value)
                Dgl1.Item(Col1Unit, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("Unit").Value)
                Dgl1.Item(Col1VNature, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("V_Nature").Value)
                Dgl1.Item(Col1LotNo, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("LotNo").Value)
                Dgl1.Item(Col1FromProcess, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("Process").Value)
                Dgl1.Item(Col1FromProcess, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("ProcessCode").Value)
                Dgl1.Item(Col1Dimension1, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("Dimension1").Value)
                Dgl1.Item(Col1Dimension1, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells(AgTemplate.ClsMain.FGetDimension1Caption()).Value)
                Dgl1.Item(Col1Dimension2, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("Dimension2").Value)
                Dgl1.Item(Col1Dimension2, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells(AgTemplate.ClsMain.FGetDimension2Caption()).Value)

                If CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Rate")), Boolean) Or CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsMandatory_Rate")), Boolean) Then
                    Dgl1.Item(Col1Rate, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("Rate").Value)
                End If
                Dgl1.Item(Col1QtyDecimalPlaces, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("QtyDecimalPlaces").Value)
                Dgl1.Item(Col1MeasurePerPcs, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("MeasurePerPcs").Value)
                Dgl1.Item(Col1MeasureUnit, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("MeasureUnit").Value)
                Dgl1.Item(Col1MeasureDecimalPlaces, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("MeasureDecimalPlaces").Value)
                Dgl1.Item(Col1CurrentStock, mRow).Value = AgTemplate.ClsMain.FunRetStock(Dgl1.AgSelectedValue(Col1ItemCode, mRow), mSearchCode, , TxtFromGodown.AgSelectedValue, , , TxtV_Date.Text, Dgl1.Item(Col1LotNo, Dgl1.CurrentCell.RowIndex).Value)
                If Not AgL.StrCmp(Topctrl1.Mode, "Browse") Then
                    LblCurrentStock.Text = Format(AgTemplate.ClsMain.FunRetStock(Dgl1.Item(Col1Item, Dgl1.CurrentCell.RowIndex).Tag, mSearchCode, , TxtFromGodown.Tag, , , TxtV_Date.Text, Dgl1.Item(Col1LotNo, Dgl1.CurrentCell.RowIndex).Value), "0.".PadRight(Dgl1.Item(Col1QtyDecimalPlaces, Dgl1.CurrentCell.RowIndex).Value + 2, "0"))
                End If
            End If
        End If
        'Catch ex As Exception
        '    MsgBox(ex.Message & " On Validating_Item Function ")
        'End Try
    End Sub

    Private Sub Validating_LotNo(ByVal Code As String, ByVal mRow As Integer)
        Dim DrTemp As DataRow() = Nothing
        Dim DtTemp As DataTable = Nothing

        Try
            If Dgl1.Item(Col1LotNo, mRow).Value.ToString.Trim = "" Or Dgl1.AgSelectedValue(Col1LotNo, mRow).ToString.Trim = "" Then
                Dgl1.Item(Col1Item, mRow).Tag = ""
                Dgl1.Item(Col1Item, mRow).Value = ""
                Dgl1.Item(Col1Qty, mRow).Value = 0
                Dgl1.Item(Col1Unit, mRow).Value = ""
                Dgl1.Item(Col1MeasurePerPcs, mRow).Value = 0
                Dgl1.Item(Col1MeasureUnit, mRow).Value = ""
            Else
                If Dgl1.AgDataRow IsNot Nothing Then
                    Dgl1.Item(Col1Item, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("ItemCode").Value)
                    Dgl1.Item(Col1Item, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("ItemDesc").Value)
                    Dgl1.Item(Col1Unit, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("Unit").Value)
                    Dgl1.Item(Col1QtyDecimalPlaces, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("QtyDecimalPlaces").Value)
                    Dgl1.Item(Col1MeasurePerPcs, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("MeasurePerPcs").Value)
                    Dgl1.Item(Col1MeasureUnit, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("MeasureUnit").Value)
                    Dgl1.Item(Col1ItemGroup, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("ItemGroupDesc").Value)
                    Dgl1.Item(Col1MeasureDecimalPlaces, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("MeasureDecimalPlaces").Value)
                    Dgl1.Item(Col1Qty, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("Qty").Value)
                    Dgl1.Item(Col1FromProcess, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("Process").Value)
                    Dgl1.Item(Col1FromProcess, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("ProcessCode").Value)
                    Dgl1.Item(Col1Dimension1, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("Dimension1").Value)
                    Dgl1.Item(Col1Dimension1, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("" & AgTemplate.ClsMain.FGetDimension1Caption() & "").Value)
                    Dgl1.Item(Col1Dimension2, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("Dimension2").Value)
                    Dgl1.Item(Col1Dimension2, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("" & AgTemplate.ClsMain.FGetDimension2Caption() & "").Value)
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " On Validating_LotNo Function ")
        End Try
    End Sub

    Private Sub DGL1_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles Dgl1.RowsAdded, Dgl1.RowsAdded
        sender(ColSNo, e.RowIndex).Value = e.RowIndex + 1
    End Sub

    Private Sub FrmProductionOrder_BaseFunction_Calculation() Handles Me.BaseFunction_Calculation
        Dim I As Integer
        LblTotalQty.Text = 0
        LblTotalMeasureValue.Text = 0
        LblTotalAmountValue.Text = 0

        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1Item, I).Value <> "" Then
                Dgl1.Item(Col1TotalMeasure, I).Value = Format(Val(Dgl1.Item(Col1Qty, I).Value) * Val(Dgl1.Item(Col1MeasurePerPcs, I).Value), "0.".PadRight(Val(Dgl1.Item(Col1MeasureDecimalPlaces, I).Value) + 2, "0"))
                If Val(Dgl1.Item(Col1Qty, I).Value) > 0 Then
                    Dgl1.Item(Col1Amount, I).Value = Format(Val(Dgl1.Item(Col1Qty, I).Value) * Val(Dgl1.Item(Col1Rate, I).Value), "0.00")
                End If

                LblTotalQty.Text = Val(LblTotalQty.Text) + Val(Dgl1.Item(Col1Qty, I).Value)
                LblTotalMeasureValue.Text = Val(LblTotalMeasureValue.Text) + Val(Dgl1.Item(Col1TotalMeasure, I).Value)
                LblTotalAmountValue.Text = Val(LblTotalAmountValue.Text) + Val(Dgl1.Item(Col1Amount, I).Value)
            End If
        Next
        LblTotalQty.Text = Format(Val(LblTotalQty.Text), "0.000")
        LblTotalMeasureValue.Text = Format(Val(LblTotalMeasureValue.Text), "0.000")
        LblTotalAmountValue.Text = Format(Val(LblTotalAmountValue.Text), "0.00")
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        Dim I As Integer = 0
        Dim DtTemp As DataTable = Nothing
        Dim mSelectionQry$ = ""

        If CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Process")), Boolean) Then
            If AgL.RequiredField(TxtProcess, "Process") Then passed = False : Exit Sub
        End If
        If AgL.RequiredField(TxtFromGodown, "From Godown") Then passed = False : Exit Sub
        If AgCL.AgIsBlankGrid(Dgl1, Dgl1.Columns(Col1Item).Index) = True Then passed = False : Exit Sub
        If AgCL.AgIsDuplicate(Dgl1, "" + Dgl1.Columns(Col1Item).Index.ToString + "," + Dgl1.Columns(Col1Item_UID).Index.ToString + "," + Dgl1.Columns(Col1LotNo).Index.ToString + "," + Dgl1.Columns(Col1Dimension1).Index.ToString + "," + Dgl1.Columns(Col1Dimension2).Index.ToString + "") Then passed = False : Exit Sub

        With Dgl1
            For I = 0 To .Rows.Count - 1
                If Dgl1.Rows(I).Visible Then
                    If .Item(Col1Item, I).Value <> "" Then
                        If Val(.Item(Col1Qty, I).Value) = 0 Then
                            MsgBox("Qty Is 0 At Row No " & Dgl1.Item(ColSNo, I).Value & "")
                            .CurrentCell = .Item(Col1Qty, I) : Dgl1.Focus()
                            passed = False : Exit Sub
                        End If

                        'If mSelectionQry <> "" Then mSelectionQry += " UNION ALL "
                        'mSelectionQry += "Select " & AgL.Chk_Text(Dgl1.Item(Col1Item, I).Tag) & ", " & _
                        '        " " & AgL.Chk_Text(Dgl1.Item(Col1LotNo, I).Value) & ", " & _
                        '        " " & Val(Dgl1.Item(Col1Qty, I).Value) & " "

                        'For Data Validation With Process & Dimensions
                        If mSelectionQry <> "" Then mSelectionQry += " UNION ALL "
                        mSelectionQry += "Select " & AgL.Chk_Text(Dgl1.Item(Col1Item, I).Tag) & ", " & _
                                " " & AgL.Chk_Text(Dgl1.Item(Col1LotNo, I).Value) & ", " & _
                                " " & AgL.Chk_Text(Dgl1.Item(Col1Dimension1, I).Tag) & ", " & _
                                " " & AgL.Chk_Text(Dgl1.Item(Col1Dimension2, I).Tag) & ", " & _
                                " " & AgL.Chk_Text(Dgl1.Item(Col1FromProcess, I).Tag) & ", " & _
                                " " & Val(Dgl1.Item(Col1Qty, I).Value) & " "

                        If CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsMandatory_ProcessLine")), Boolean) Then
                            If Dgl1.Item(Col1FromProcess, I).Value = "" Then
                                MsgBox(" Process Is Required At Line No " & Dgl1.Item(ColSNo, I).Value & "")
                                Dgl1.CurrentCell = Dgl1.Item(Col1FromProcess, I) : Dgl1.Focus()
                                passed = False : Exit Sub
                            End If
                        End If
                    End If
                End If
            Next
        End With

        Dim StrMsg1$ = ""
        StrMsg1 = FDataValidation_Item_UID()

        If StrMsg1 <> "" Then
            If ImportMode = True Then
                ImportMessegeStr += StrMsg1
            Else
                MsgBox(StrMsg1)
            End If
            passed = False : Exit Sub
        End If

        If mSelectionQry <> "" Then
            'Selection Qry Contains Loop Genearted Selecion Qry String For Item And Its Quantity
            'For Example Select " & AgL.Chk_Text(Dgl1.Item(Col1Item, I).Tag) & ", " & Val(Dgl1.Item(Col1Qty, I).Value) & " 
            'passed = ClsMain.FIsNegativeStock(mSelectionQry, mSearchCode, TxtFromGodown.Tag, TxtV_Date.Text)
            passed = AgTemplate.ClsMain.FIsNegativeStock(mSelectionQry, mSearchCode, TxtFromGodown.Tag, TxtV_Date.Text)
        End If
    End Sub

    Public Function FDataValidation_Item_UID() As String
        Dim DtTemp As DataTable = Nothing
        Dim DtTemp1 As DataTable = Nothing
        Dim I As Integer = 0
        Dim mItem_UidStr$ = ""
        Dim MsgStr$ = ""

        For I = 0 To Dgl1.Rows.Count - 1
            If Dgl1.Item(Col1Item_Uid, I).Tag <> "" Then
                If mItem_UidStr = "" Then
                    mItem_UidStr = AgL.Chk_Text(Dgl1.Item(Col1Item_Uid, I).Tag)
                Else
                    mItem_UidStr += "," & AgL.Chk_Text(Dgl1.Item(Col1Item_Uid, I).Tag)
                End If
            End If
        Next

        If mItem_UidStr = "" Then FDataValidation_Item_UID = "" : Exit Function

        'mQry = " Select Iu.Item_Uid From Item_Uid Iu LEFT JOIN Item I ON Iu.Item = I.Code Where Iu.Code In (" & mItem_UidStr & ") And I.Div_Code <> '" & IIf(TxtItemDivision.Text <> "", TxtItemDivision.Tag, AgL.PubDivCode) & "'"
        'DtTemp = AgL.FillData(mQry, AgL.GcnRead).Tables(0)

        'If DtTemp.Rows.Count > 0 Then
        '    For I = 0 To DtTemp.Rows.Count - 1
        '        MsgStr += "Carpet Id " & AgL.XNull(DtTemp.Rows(I)("Item_Uid")) & " Does Not Belong To " & IIf(TxtItemDivision.Text <> "", TxtItemDivision.Text, AgL.PubDivName) & "."
        '    Next
        'End If

        'mQry = " Select Iu.Item_Uid " & _
        '            " From StockProcess L " & _
        '            " LEFT JOIN Item_Uid Iu On L.Item_Uid = Iu.Code " & _
        '            " Where IsNull(L.Qty_Iss,0) > 0 And L.Process = '" & TxtProcess.Tag & "' " & _
        '            " And L.Item_UID In (" & mItem_UidStr & ") " & _
        '            " And L.DocID <> '" & mSearchCode & "'  " & _
        '            " Group By Iu.Item_Uid " & _
        '            " Having IsNull(Count(*),0) > 0 "
        'DtTemp = AgL.FillData(mQry, AgL.GcnRead).Tables(0)

        'If DtTemp.Rows.Count > 0 Then
        '    For I = 0 To DtTemp.Rows.Count - 1
        '        MsgStr += "Carpet Id " & AgL.XNull(DtTemp.Rows(I)("Item_Uid")) & " has already completed this process"
        '    Next
        'End If

        mQry = " Select Item_Uid From Item_Uid With (NoLock) " & _
                " Where Code In (" & mItem_UidStr & ") " & _
                " And RecDocId Is Null "
        DtTemp = AgL.FillData(mQry, AgL.GcnRead).Tables(0)

        If DtTemp.Rows.Count > 0 Then
            For I = 0 To DtTemp.Rows.Count - 1
                MsgStr += "Carpet Id " & AgL.XNull(DtTemp.Rows(I)("Item_Uid")) & " Is Not Received From Weaving Process."
            Next
        End If

        mQry = "SELECT I.Item_UID " & _
               " FROM (SELECT DocID, Item_UID " & _
               "       FROM JobIssRecUID WITH (NoLock) " & _
               "       WHERE Item_UID In (" & mItem_UidStr & ") And IssRec= 'I') I " & _
               " LEFT JOIN JobIssRecUID R WITH (NoLock) ON I.DocID = R.JobRecDocID AND I.Item_UID = R.Item_UID  " & _
               " WHERE R.DocID IS NULL AND I.DocID <> '" & mSearchCode & "' " & _
               " Group By I.Item_UID " & _
               " Having Count(I.DocId) > 0 "
        DtTemp = AgL.FillData(mQry, AgL.GcnRead).Tables(0)
        If DtTemp.Rows.Count > 0 Then
            For I = 0 To DtTemp.Rows.Count - 1
                mQry = "SELECT TOP 1 Sg.Name, H.ManualRefNo, H.V_Date, Vc.NCatDescription AS ProcessDesc, " & _
                            " Iu.Item_Uid As Item_UidDesc " & _
                            " FROM (SELECT DocID, Item_UID FROM JobIssRecUID WITH (NoLock) " & _
                            "       WHERE Item_UID = '" & DtTemp.Rows(0)("Item_Uid") & "' And IssRec='I') I " & _
                            " LEFT JOIN JobIssRecUID R WITH (NoLock) ON I.DocID = R.JobRecDocID AND I.Item_UID = R.Item_UID  " & _
                            " LEFT JOIN JobOrder H WITH (NoLock) ON I.DocID = H.DocID " & _
                            " LEFT JOIN Item_Uid Iu On I.Item_Uid = Iu.Code " & _
                            " LEFT JOIN SubGroup Sg WITH (NoLock) ON H.JobWorker = Sg.SubCode " & _
                            " LEFT JOIN VoucherCat Vc WITH (NoLock) ON H.Process = Vc.NCat " & _
                            " WHERE R.DocID IS NULL AND I.DocID <> '" & mSearchCode & "' " & _
                            " ORDER BY H.V_Date Desc "
                DtTemp1 = AgL.FillData(mQry, AgL.GcnRead).Tables(0)

                If DtTemp1.Rows.Count > 0 Then
                    MsgStr += "Carpet Id " & DtTemp1.Rows(0)("Item_UidDesc") & " Is Already Issued To " & AgL.XNull(DtTemp1.Rows(0)("Name")) & " For " & AgL.XNull(DtTemp1.Rows(0)("ProcessDesc")) & " On Date " & AgL.XNull(DtTemp1.Rows(0)("V_Date")) & " Against Ref No " & AgL.XNull(DtTemp1.Rows(0)("ManualRefNo")) & "."
                End If
            Next
        End If
        FDataValidation_Item_UID = MsgStr
        Dgl1.Focus()
    End Function

    Private Sub FrmProductionOrder_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
        Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
        LblTotalMeasureValue.Text = 0 : LblTotalQty.Text = 0 : LblTotalAmountValue.Text = 0
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
        AgL.WinSetting(Me, 600, 895)
    End Sub

    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub

    Private Sub Validating_Item_Uid(ByVal Item_Uid As String, ByVal mRow As Integer)
        Dim DrTemp As DataRow() = Nothing
        Dim DtTemp As DataTable = Nothing

        Try
            mQry = " SELECT I.Code, I.Description, I.Unit, I.ManualCode, I.Prod_Measure, I.MeasureUnit, I.Rate, IG.Description AS ItemGroupDesc," & _
                   " U.DecimalPlaces as QtyDecimalPlaces, MU.DecimalPlaces as MeasureDecimalPlaces, UI.Code as ItemUIDCode, UI.ProdOrder as ProdOrderDocID, PO.V_Type + '-' + PO.ManualRefNo as ProdOrderNo  " & _
                   " FROM (Select Item, Code, ProdOrder From Item_UID Where Item_Uid = '" & Dgl1.Item(Col1Item_UID, mRow).Value & "') UI " & _
                   " Left Join ProdOrder PO With (NoLock) On UI.ProdOrder = PO.DocID " & _
                   " Left Join Item I With (NoLock) On UI.Item  = I.Code " & _
                   " LEFT JOIN ItemGroup IG on IG.Code = I.ItemGroup " & _
                   " Left Join Unit U With (NoLock) On I.Unit = U.Code " & _
                   " Left Join Unit MU With (NoLock) On I.MeasureUnit = MU.Code "
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
            Dgl1.Item(Col1Item_UID, mRow).Tag = AgL.XNull(DtTemp.Rows(0)("ItemUIDCode"))
            Dgl1.Item(Col1ItemCode, mRow).Tag = AgL.XNull(DtTemp.Rows(0)("Code"))
            Dgl1.Item(Col1ItemCode, mRow).Value = AgL.XNull(DtTemp.Rows(0)("ManualCode"))
            Dgl1.Item(Col1Item, mRow).Tag = AgL.XNull(DtTemp.Rows(0)("Code"))
            Dgl1.Item(Col1Item, mRow).Value = AgL.XNull(DtTemp.Rows(0)("Description"))
            Dgl1.Item(Col1ItemGroup, mRow).Value = AgL.XNull(DtTemp.Rows(0)("ItemGroupDesc"))
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
            Dgl1.Item(Col1CurrentStock, mRow).Value = AgTemplate.ClsMain.FunRetStock(Dgl1.AgSelectedValue(Col1ItemCode, mRow), mSearchCode, , TxtFromGodown.AgSelectedValue, , , TxtV_Date.Text, Dgl1.Item(Col1LotNo, Dgl1.CurrentCell.RowIndex).Value)

            mQry = " SELECT TOP 1 P.NCat As ProcessCode, P.Description As ProcessDesc " & _
                        " FROM JobIssRecUID L  " & _
                        " LEFT JOIN Process P ON L.Process = P.NCat " & _
                        " WHERE Item_UID = '" & Dgl1.Item(Col1Item_UID, mRow).Tag & "' " & _
                        " ORDER BY L.V_Date DESC, P.Sr DESC "
            Dim DtProcess As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtProcess.Rows.Count > 0 Then
                Dgl1.Item(Col1FromProcess, mRow).Tag = AgL.XNull(DtProcess.Rows(0)("ProcessCode"))
                Dgl1.Item(Col1FromProcess, mRow).Value = AgL.XNull(DtProcess.Rows(0)("ProcessDesc"))
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
            mQry = " SELECT H.SubCode AS Code, H.Name + (Case When IsNull(H.CityCode,'')<>'' Then ', ' + C.CityName Else '' End) as Name  " & _
                    " FROM Subgroup H  With (NoLock) " & _
                    " Left Join City C On H.CityCode = C.CityCode" & _
                    " Where IsNull(H.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') ='" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
            TxtParty.AgHelpDataSet(0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
        Else
            mQry = " SELECT H.SubCode AS Code,  H.Name + (Case When IsNull(H.CityCode,'')<>'' Then ', ' + C.CityName Else '' End) as Name  " & _
                    " FROM Subgroup H  With (NoLock) " & _
                    " Left Join City C On H.CityCode = C.CityCode" & _
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
                            " max(L.Unit) AS Unit, Max(IG.Description) AS ItemGroupDesc, H.DocID AS RequisitionDocId, L.Sr AS RequisitionSr,  max(L.MeasurePerPcs) AS MeasurePerPcs, max(I.Rate) AS Rate, " & _
                            " ISNULL(Max(U.DecimalPlaces),0) As QtyDecimalPlaces, ISNULL(Max(UM.DecimalPlaces),0) As MeasureDecimalPlaces, max(L.MeasureUnit) AS MeasureUnit, " & _
                            " NULL AS ProcessCode, NULL AS Dimension1, NULL AS Dimension2, Null AS LotNo, NULL AS " & AgTemplate.ClsMain.FGetDimension1Caption() & ", NULL AS " & AgTemplate.ClsMain.FGetDimension2Caption() & ", NULL AS Process, " & _
                            " '" & RbtIssueForReqisition.Text & "' AS V_Nature " & _
                            " FROM Requisition H " & _
                            " LEFT JOIN RequisitionDetail L ON L.DocId = H.DocID " & _
                            " LEFT JOIN Item I ON I.Code = L.Item  " & _
                            " LEFT JOIN ItemGroup IG On Ig.Code = I.ItemGroup" & _
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
                    Dgl1.AgHelpDataSet(Col1Item, 15) = AgL.FillData(mQry, AgL.GCn)
                ElseIf RbtnForStock.Checked Then
                    mQry = " SELECT H.Item, Max(I.Description) AS ItemDesc, H.LotNo, " & _
                            " Max(D1.Description) AS " & AgTemplate.ClsMain.FGetDimension1Caption() & ", Max(D2.Description) AS " & AgTemplate.ClsMain.FGetDimension2Caption() & ", Max(P.Description) AS Process, Round(isnull(sum(H.Qty_Rec),0) - isnull(sum(H.Qty_Iss),0),4) AS Qty, " & _
                            " H.process AS ProcessCode, Max(I.Unit) AS Unit, H.Dimension1, H.Dimension2, Max(I.ManualCode) AS ItemCode, " & _
                            " Max(I.Measure) AS MeasurePerPcs, Max(I.MeasureUnit) AS MeasureUnit, Max(U.DecimalPlaces) AS QtyDecimalPlaces, Max(IG.Description) AS ItemGroupDesc, Max(MU.DecimalPlaces) as MeasureDecimalPlaces, Max(I.Rate) AS Rate, " & _
                            " NULL AS RequisitionNo, NULL AS RequisitionDocId, NULL AS RequisitionSr, '" & RbtnForStock.Text & "' AS V_Nature " & _
                            " FROM Stock H WITH (Nolock) " & _
                            " LEFT JOIN Item I WITH (Nolock) ON I.Code = H.Item  " & _
                            " LEFT JOIN Process P ON P.NCat = H.Process " & _
                            " LEFT JOIN Dimension1 D1 WITH (Nolock) ON D1.Code = H.Dimension1 " & _
                            " LEFT JOIN Dimension2 D2 WITH (Nolock) ON D2.Code = H.Dimension2  " & _
                            " LEFT JOIN ItemGroup IG On Ig.Code = I.ItemGroup" & _
                            " Left Join Unit U On I.Unit = U.Code " & _
                            " Left Join Unit MU On I.MeasureUnit = MU.Code " & _
                            " WHERE isnull(H.Item,'') <> ''  AND H.Godown = " & AgL.Chk_Text(TxtFromGodown.Tag) & " " & _
                            " AND H.V_Date <= " & AgL.Chk_Text(TxtV_Date.Text) & " AND H.DocID <> " & AgL.Chk_Text(mInternalCode) & " " & _
                            " And IsNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "'  " & strCond & _
                            " GROUP BY H.Item, H.LotNo, H.Process, H.Dimension1, H.Dimension2    " & _
                            " HAVING Round(isnull(sum(H.Qty_Rec),0) - isnull(sum(H.Qty_Iss),0),4) > 0 " & _
                            " Order By Max(I.Description) "
                    Dgl1.AgHelpDataSet(Col1Item, 13) = AgL.FillData(mQry, AgL.GCn)
                Else
                    mQry = " SELECT I.Code AS Item, I.Description AS ItemDesc, I.ManualCode AS ItemCode, I.Unit, " & _
                        " I.Measure AS MeasurePerPcs, I.MeasureUnit, U.DecimalPlaces as QtyDecimalPlaces, Null AS LotNo, IG.Description AS ItemGroupDesc, MU.DecimalPlaces as MeasureDecimalPlaces, I.Rate, " & _
                        " NULL AS ProcessCode, NULL AS Dimension1, NULL AS Dimension2, NULL AS " & AgTemplate.ClsMain.FGetDimension1Caption() & ", NULL AS " & AgTemplate.ClsMain.FGetDimension2Caption() & ", NULL AS Process, " & _
                        " NULL AS RequisitionNo, NULL AS RequisitionDocId, NULL AS RequisitionSr, 0 AS Qty, " & _
                        " '" & RbtIssueDirect.Text & "' AS V_Nature " & _
                        " FROM Item I " & _
                        " LEFT JOIN ItemGroup IG On Ig.Code = I.ItemGroup" & _
                        " Left Join Unit U On I.Unit = U.Code " & _
                        " Left Join Unit MU On I.MeasureUnit = MU.Code " & _
                        " Where IsNull(I.IsDeleted ,0)  = 0 And " & _
                        " IsNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "')='" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
                    Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex, 17) = AgL.FillData(mQry, AgL.GCn)
                End If

            Case Col1ItemCode
                If RbtIssueForReqisition.Checked Then
                    mQry = " SELECT max(L.Item) AS Code, max(I.ManualCode) AS ItemCode, max(I.Description) AS ItemDesc,  max(H.V_Type) + '-' + max ( Convert(NVarchar,H.V_No)) AS PlanningNo, L.MaterialPlanSr , L.MaterialPlan ,  max(L.Unit) AS Unit, " & _
                            " max(L.MeasurePerPcs) AS MeasurePerPcs, Max(IG.Description) AS ItemGroupDesc, max(L.MeasureUnit) AS MeasureUnit, isnull(sum(L.UserPurchPlanQty ),0) - isnull(sum(D.IndQty ),0) AS PlanQty, sum(L.UserPurchPlanMeasure ) - isnull(sum(D.IndMeasure ),0) AS PlanMeasure, " & _
                            " Max(U.DecimalPlaces) As QtyDecimalPlaces, Max(UM.DecimalPlaces) As MeasureDecimalPlaces " & _
                            " FROM MaterialPlan H " & _
                            " LEFT JOIN MaterialPlanDetail L ON L.DocId = H.DocID  " & _
                            " LEFT JOIN Item I ON I.Code = L.Item  " & _
                            " LEFT JOIN ItemGroup IG On Ig.Code = I.ItemGroup" & _
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
                ElseIf RbtnForStock.Checked Then
                    mQry = " SELECT H.Item, Max(I.ManualCode) AS ItemCode, " & _
                            " Max(D1.Description) AS " & AgTemplate.ClsMain.FGetDimension1Caption() & ", Max(D2.Description) AS " & AgTemplate.ClsMain.FGetDimension2Caption() & ", Max(P.Description) AS Process, Round(isnull(sum(H.Qty_Rec),0) - isnull(sum(H.Qty_Iss),0),4) AS Qty, " & _
                            " H.process AS ProcessCode, Max(I.Unit) AS Unit, H.Dimension1, H.Dimension2, Max(I.Description) AS ItemDesc, " & _
                            " Max(I.Measure) AS MeasurePerPcs, Max(I.MeasureUnit) AS MeasureUnit, Max(U.DecimalPlaces) AS QtyDecimalPlaces, Max(IG.Description) AS ItemGroupDesc, Max(MU.DecimalPlaces) as MeasureDecimalPlaces, Max(I.Rate) AS Rate, " & _
                            " NULL AS RequisitionNo, NULL AS RequisitionDocId, NULL AS RequisitionSr, '" & RbtnForStock.Text & "' AS V_Nature " & _
                            " FROM Stock H WITH (Nolock) " & _
                            " LEFT JOIN Item I WITH (Nolock) ON I.Code = H.Item  " & _
                            " LEFT JOIN Process P ON P.NCat = H.Process " & _
                            " LEFT JOIN Dimension1 D1 WITH (Nolock) ON D1.Code = H.Dimension1 " & _
                            " LEFT JOIN Dimension2 D2 WITH (Nolock) ON D2.Code = H.Dimension2  " & _
                            " LEFT JOIN ItemGroup IG On Ig.Code = I.ItemGroup" & _
                            " Left Join Unit U On I.Unit = U.Code " & _
                            " Left Join Unit MU On I.MeasureUnit = MU.Code " & _
                            " WHERE isnull(H.Item,'') <> ''  AND H.Godown = " & AgL.Chk_Text(TxtFromGodown.Tag) & " " & _
                            " AND H.V_Date <= " & AgL.Chk_Text(TxtV_Date.Text) & " AND H.DocID <> " & AgL.Chk_Text(mInternalCode) & " " & _
                            " And IsNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "'  " & strCond & _
                            " GROUP BY H.Item, H.Process, H.Dimension1, H.Dimension2    " & _
                            " HAVING Round(isnull(sum(H.Qty_Rec),0) - isnull(sum(H.Qty_Iss),0),4) > 0 " & _
                            " Order By Max(I.Description) "
                    Dgl1.AgHelpDataSet(Col1Item, 13) = AgL.FillData(mQry, AgL.GCn)
                Else
                    mQry = " SELECT H.Code, H.ManualCode as Item_No, H.Description as Item_Name, H.Unit, " & _
                        " H.Measure, H.MeasureUnit, U.DecimalPlaces as QtyDecimalPlaces, IG.Description AS ItemDesc, MU.DecimalPlaces as MeasureDecimalPlaces, H.Rate " & _
                        " FROM Item H " & _
                        " LEFT JOIN ItemGroup IG On Ig.Code = H.ItemGroup" & _
                        " Left Join Unit U On H.Unit = U.Code " & _
                        " Left Join Unit MU On H.MeasureUnit = MU.Code " & _
                        " Where IsNull(H.IsDeleted ,0)  = 0 And " & _
                        " IsNull(H.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "')='" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
                    Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex, 4) = AgL.FillData(mQry, AgL.GCn)
                End If
        End Select
    End Sub

    Private Sub Dgl1_EditingControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.EditingControl_KeyDown
        Try
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

                Case Col1Unit
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) Is Nothing Then
                            mQry = " SELECT H.Code, H.Code as Description  " & _
                                    " FROM Unit H Order by H.Code  "
                            Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If

                Case Col1CostCenter
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) Is Nothing Then
                            If TxtParty.Tag <> "" Then
                                mQry = "SELECT C.Code, C.Name FROM CostCenterMast C WHERE C.Subcode = '" & TxtParty.Tag & "'"
                            Else
                                mQry = "SELECT C.Code, C.Name FROM CostCenterMast C "
                            End If
                            Dgl1.AgHelpDataSet(Col1CostCenter) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If

                Case Col1LotNo
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) Is Nothing Then
                            FCreateHelpLotNo()
                        End If
                    End If

                Case Col1Dimension1
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1Dimension1) Is Nothing Then
                            mQry = " SELECT Code, Description  FROM Dimension1  "
                            Dgl1.AgHelpDataSet(Col1Dimension1) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If

                Case Col1Dimension2
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1Dimension2) Is Nothing Then
                            mQry = " SELECT Code, Description  FROM Dimension2  "
                            Dgl1.AgHelpDataSet(Col1Dimension2) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If

                Case Col1FromProcess
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1FromProcess) Is Nothing Then
                            mQry = " SELECT P.NCat, P.Description FROM Process P  "
                            Dgl1.AgHelpDataSet(Col1FromProcess) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FCreateHelpLotNo()
        Dim strCond As String = ""

        If Dgl1.Item(Col1Item, Dgl1.CurrentCell.RowIndex).Value <> "" Then
            If AgL.VNull(AgL.Dman_Execute(" Select IsNull(IsRequired_LotNo,0) As IsRequired_LotNo " & _
                                          " From ItemSiteDetail Where Code = '" & Dgl1.Item(Col1Item, Dgl1.CurrentCell.RowIndex).Tag & "' " & _
                                          " And Site_Code = '" & AgL.PubSiteCode & "'", AgL.GCn).ExecuteScalar) = 0 Then
                Dgl1.AgHelpDataSet(Col1LotNo) = Nothing
                Exit Sub
            End If
        End If


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
                strCond += " And CharIndex('|' + I.Code + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_Item")) & "') <= 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemDivision")) <> "" Then
                strCond += " And CharIndex('|' + I.Div_Code + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemDivision")) & "') > 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemSite")) <> "" Then
                strCond += " And CharIndex('|' + I.Site_Code + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemSite")) & "') > 0 "
            End If
        End If

        If Dgl1.Item(Col1Item, Dgl1.CurrentCell.RowIndex).Value <> "" Then
            strCond += " And L.Item = '" & Dgl1.Item(Col1Item, Dgl1.CurrentCell.RowIndex).Tag & "'"
        End If

        mQry = " SELECT L.LotNo As Code, Max(L.LotNo) As LotNo, Max(I.Description) As ItemDesc, Max(P.Description) As Process, " & _
                " IsNull(Sum(L.Qty_Rec),0) - IsNull(Sum(L.Qty_Iss),0) AS Qty, Max(I.Unit) As Unit, " & _
                " Max(IG.Description) AS ItemGroupDesc, Max(I.SalesTaxPostingGroup) As SalesTaxPostingGroup,  " & _
                " Max(I.Finishing_Measure) As MeasurePerPcs,  Max(I.MeasureUnit) As MeasureUnit,  " & _
                " Max(U.DecimalPlaces) as QtyDecimalPlaces, Max(U1.DecimalPlaces) as MeasureDecimalPlaces, L.Item As ItemCode, " & _
                " L.Process As ProcessCode, '' As ProdOrder, '' As ProdOrderNo, '' As ProdOrderSr, " & _
                " Null As Dimension1, Null As " & AgTemplate.ClsMain.FGetDimension1Caption() & ", " & _
                " Null As Dimension2, Null As " & AgTemplate.ClsMain.FGetDimension2Caption() & " " & _
                " FROM Stock L " & _
                " LEFT JOIN Item I ON L.Item = I.Code " & _
                " LEFT JOIN ItemGroup IG On Ig.Code = I.ItemGroup " & _
                " LEFT JOIN Process P On L.Process = P.NCat " & _
                " LEFT JOIN ProcessSequenceDetail Psd ON I.ProcessSequence = Psd.Code AND L.Process = Psd.Process " & _
                " LEFT JOIN Unit U On I.Unit = U.Code " & _
                " LEFT JOIN Unit U1 On I.MeasureUnit = U1.Code " & _
                " Where L.LotNo Is Not Null " & _
                " And IsNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') <= '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond & _
                " Group By L.Item, L.LotNo, L.Process " & _
                " Having IsNull(Sum(L.Qty_Rec),0) - IsNull(Sum(L.Qty_Iss),0) > 0 " & _
                " Order By LotNo, Item "
        Dgl1.AgHelpDataSet(Col1LotNo, 14) = AgL.FillData(mQry, AgL.GCn)
    End Sub

    Private Sub FrmStoreIssue_BaseEvent_Topctrl_tbPrn(ByVal SearchCode As String) Handles Me.BaseEvent_Topctrl_tbPrn
        mQry = " SELECT H.DOCID, H.V_TYPE, H.V_DATE, H.V_NO, H.MANUALREFNO, H.REMARKS, H.ENTRYBY, H.ENTRYDATE, " & _
                " H.ENTRYTYPE, H.ENTRYSTATUS,  H.APPROVEBY, H.APPROVEDATE,  H.STATUS, U.DecimalPlaces, S.InsideOutside, " & _
                " L.SR, L.ITEM, L.LOTNO, ISNULL(L.QTY,0) AS QTY, L.UNIT, L.REMARKS AS LINEREMARKS,  S.NAME AS JOBWORKERNAME, S.DISPNAME AS JOBWORKERDISPNAME,   S.ADD1, " & _
                " S.ADD2,S.ADD3,C.CITYNAME,S.MOBILE,S.PHONE, S.PAN,  G.DESCRIPTION AS GODOWNDESC,  I.DESCRIPTION AS ITEMDESC,   " & _
                " '" & AgTemplate.ClsMain.FGetDimension1Caption() & "' AS Caption_Dimension1,  '" & AgTemplate.ClsMain.FGetDimension2Caption() & "' AS Caption_Dimension2, " & _
                " D1.Description AS D1Desc,  D2.Description AS D2Desc, " & _
                " I.ITEMGROUP , I.ITEMTYPE, IG.DESCRIPTION AS ITEMGROUPDESC, P.Description AS ProcessDesc, CM.NAME AS COSTCENTERNAME " & _
                " FROM STOCKHEAD H   " & _
                " LEFT JOIN STOCKHEADDETAIL L ON H.DOCID = L.DOCID   " & _
                " LEFT JOIN VOUCHER_TYPE VT ON H.V_TYPE = VT.V_TYPE  " & _
                " LEFT JOIN SUBGROUP S ON H.SUBCODE = S.SUBCODE   " & _
                " LEFT JOIN CITY C ON S.CITYCODE = C.CITYCODE   " & _
                " LEFT JOIN GODOWN G ON H.FROMGODOWN = G.CODE   " & _
                " LEFT JOIN ITEM I ON L.ITEM = I.CODE   " & _
                " LEFT JOIN ITEMGROUP  IG ON I.ITEMGROUP = IG.CODE  " & _
                " LEFT JOIN COSTCENTERMAST CM ON L.COSTCENTER = CM.CODE " & _
                " LEFT JOIN Process P ON P.NCat = H.Process " & _
                " LEFT JOIN Unit U ON U.Code = L.Unit " & _
                " LEFT JOIN Dimension1 D1 ON D1.Code = L.Dimension1 " & _
                " LEFT JOIN Dimension2 D2 ON D2.Code = L.Dimension2 " & _
                " WHERE H.DocID =  '" & mSearchCode & "'  Order By L.Sr "
        ClsMain.FPrintThisDocument(Me, TxtV_Type.Tag, mQry, "Store_Issue_Print", "Store Issue")
    End Sub

    Private Sub FrmStoreIssue_BaseEvent_Topctrl_tbRef() Handles Me.BaseEvent_Topctrl_tbRef
        If TxtParty.AgHelpDataSet IsNot Nothing Then TxtParty.AgHelpDataSet = Nothing
        If Dgl1.AgHelpDataSet(Col1Item) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1Item) = Nothing
        If Dgl1.AgHelpDataSet(Col1LotNo) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1LotNo) = Nothing
    End Sub

    Private Sub BtnFillIssueDetail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnFillIssueDetail.Click
        If Topctrl1.Mode = "Browse" Then Exit Sub

        Dim strTicked As String
        If RbtIssueForReqisition.Checked = True Then
            strTicked = FHPGD_RequisionNo()
            If strTicked <> "" Then
                ProcFillRequisitionDetails(strTicked)
            End If
        ElseIf RbtnForStock.Checked = True Then
            strTicked = FHPGD_Items()
            If strTicked <> "" Then
                FFillItems(strTicked)
            End If

        Else
            Exit Sub
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
                        Dgl1.Item(Col1CurrentStock, I).Value = AgTemplate.ClsMain.FunRetStock(Dgl1.AgSelectedValue(Col1Item, I), mInternalCode, , , , , TxtV_Date.Text, Dgl1.Item(Col1LotNo, Dgl1.CurrentCell.RowIndex).Value)
                    Next I
                End If
            End With
            Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function FHPGD_Items() As String
        Dim FRH_Multiple As DMHelpGrid.FrmHelpGrid_Multi
        Dim StrRtn As String = ""

        mQry = "  SELECT 'o' As Tick,  VMain.CodeStr, VMain.ItemDesc, VMain.LotNo, VMain.ItemGroupDesc, VMain.Dimension1Desc, VMain.Dimension2Desc, VMain.Process, VMain.Qty " & _
                " FROM ( " & FRetFillItemWiseQry(" ", "") & " ) As VMain " & _
                " Order By VMain.ItemDesc, VMain.LotNo, VMain.Dimension1Desc, VMain.Dimension2Desc, VMain.Process "

        FRH_Multiple = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(AgL.FillData(mQry, AgL.GCn).TABLES(0)), "", 500, 970, , , False)
        FRH_Multiple.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple.FFormatColumn(1, , 0, , False)
        FRH_Multiple.FFormatColumn(2, "Item", 200, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(3, "Lot No", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(4, "Item Group", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(5, AgTemplate.ClsMain.FGetDimension1Caption(), 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(6, AgTemplate.ClsMain.FGetDimension2Caption(), 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(7, "Process", 110, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(8, "Qty", 100, DataGridViewContentAlignment.MiddleLeft)

        FRH_Multiple.StartPosition = FormStartPosition.CenterScreen
        FRH_Multiple.ShowDialog()

        If FRH_Multiple.BytBtnValue = 0 Then
            StrRtn = FRH_Multiple.FFetchData(1, "'", "'", ",", True)
        End If
        FHPGD_Items = StrRtn

        FRH_Multiple = Nothing
    End Function

    Private Sub FFillItems(ByVal bItemStr As String)
        Dim I As Integer = 0
        Dim DtTemp As DataTable = Nothing

        Try
            If bItemStr = "" Then Exit Sub


            mQry = FRetFillItemWiseQry("", " And ISNULL(H.Item,'') + ISNULL(H.LotNo,'') + ISNULL(H.Process,'') + ISNULL(H.Dimension1,'') + ISNULL(H.Dimension2,'') In (" & bItemStr & ")")

            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

            For I = 0 To Dgl1.Rows.Count - 1
                If Dgl1.Item(Col1Item, I).Value <> "" Then
                    Dgl1.Rows(I).Visible = False
                End If
            Next

            Dim J As Integer = Dgl1.Rows.Count - 1

            With DtTemp
                If .Rows.Count > 0 Then
                    For I = 0 To .Rows.Count - 1
                        Dgl1.Rows.Add()
                        Dgl1.Item(ColSNo, J).Value = Dgl1.Rows.Count - 1
                        Dgl1.Item(Col1Item, J).Tag = AgL.XNull(.Rows(I)("Item"))
                        Dgl1.Item(Col1Item, J).Value = AgL.XNull(.Rows(I)("ItemDesc"))

                        Dgl1.Item(Col1Dimension1, J).Tag = AgL.XNull(.Rows(I)("Dimension1"))
                        Dgl1.Item(Col1Dimension1, J).Value = AgL.XNull(.Rows(I)("Dimension1Desc"))
                        Dgl1.Item(Col1Dimension2, J).Tag = AgL.XNull(.Rows(I)("Dimension2"))
                        Dgl1.Item(Col1Dimension2, J).Value = AgL.XNull(.Rows(I)("Dimension2Desc"))
                        Dgl1.Item(Col1LotNo, J).Value = AgL.XNull(.Rows(I)("LotNo"))
                        Dgl1.Item(Col1ItemGroup, J).Value = AgL.XNull(.Rows(I)("ItemGroupDesc"))


                        Dgl1.Item(Col1FromProcess, J).Tag = AgL.XNull(.Rows(I)("ProcessCode"))
                        Dgl1.Item(Col1FromProcess, J).Value = AgL.XNull(.Rows(I)("Process"))

                        Dgl1.Item(Col1Qty, J).Value = Format(AgL.VNull(.Rows(I)("Qty")), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
                        Dgl1.Item(Col1Unit, J).Value = AgL.XNull(.Rows(I)("Unit"))
                        Dgl1.Item(Col1QtyDecimalPlaces, J).Value = AgL.VNull(.Rows(I)("QtyDecimalPlaces"))
                        Dgl1.Item(Col1MeasurePerPcs, J).Value = Format(AgL.VNull(.Rows(I)("MeasurePerPcs")), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                        Dgl1.Item(Col1MeasureUnit, J).Value = AgL.XNull(.Rows(I)("MeasureUnit"))
                        Dgl1.Item(Col1MeasureDecimalPlaces, J).Value = AgL.VNull(.Rows(I)("MeasureDecimalPlaces"))
                        Dgl1.Item(Col1Rate, J).Value = AgL.VNull(.Rows(I)("Rate"))



                        CType(Dgl1.Columns(Col1Qty), AgControls.AgTextColumn).AgNumberRightPlaces = Val(Dgl1.Item(Col1QtyDecimalPlaces, Dgl1.CurrentCell.RowIndex).Value)
                        CType(Dgl1.Columns(Col1TotalMeasure), AgControls.AgTextColumn).AgNumberRightPlaces = Val(Dgl1.Item(Col1MeasureDecimalPlaces, Dgl1.CurrentCell.RowIndex).Value)

                        J += 1
                    Next I
                End If
            End With
            Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function FRetFillItemWiseQry(ByVal HeaderConStr As String, ByVal LineConStr As String) As String
        FRetFillItemWiseQry = " SELECT ISNULL(H.Item,'') + ISNULL(H.LotNo,'') + ISNULL(H.Process,'') + ISNULL(H.Dimension1,'') + ISNULL(H.Dimension2,'') AS CodeStr, ISNULL(H.Item,'') AS Item , Max(I.Description) AS ItemDesc, H.LotNo, " & _
                            " Max(D1.Description) AS Dimension1Desc, Max(D2.Description) AS Dimension2Desc, Max(P.Description) AS Process, Round(isnull(sum(H.Qty_Rec),0) - isnull(sum(H.Qty_Iss),0),4) AS Qty, " & _
                            " H.process AS ProcessCode, Max(I.Unit) AS Unit, H.Dimension1, H.Dimension2, Max(I.ManualCode) AS ItemCode, " & _
                            " Max(I.Measure) AS MeasurePerPcs, Max(I.MeasureUnit) AS MeasureUnit, Max(U.DecimalPlaces) AS QtyDecimalPlaces, Max(IG.Description) AS ItemGroupDesc, Max(MU.DecimalPlaces) as MeasureDecimalPlaces, Max(I.Rate) AS Rate, " & _
                            " NULL AS RequisitionNo, NULL AS RequisitionDocId, NULL AS RequisitionSr, '" & RbtnForStock.Text & "' AS V_Nature " & _
                            " FROM Stock H WITH (Nolock) " & _
                            " LEFT JOIN Item I WITH (Nolock) ON I.Code = H.Item  " & _
                            " LEFT JOIN Process P ON P.NCat = H.Process " & _
                            " LEFT JOIN Dimension1 D1 WITH (Nolock) ON D1.Code = H.Dimension1 " & _
                            " LEFT JOIN Dimension2 D2 WITH (Nolock) ON D2.Code = H.Dimension2  " & _
                            " LEFT JOIN ItemGroup IG On Ig.Code = I.ItemGroup" & _
                            " Left Join Unit U On I.Unit = U.Code " & _
                            " Left Join Unit MU On I.MeasureUnit = MU.Code " & _
                            " WHERE isnull(H.Item,'') <> ''  AND H.Godown = " & AgL.Chk_Text(TxtFromGodown.Tag) & " " & _
                            " AND H.V_Date <= " & AgL.Chk_Text(TxtV_Date.Text) & " AND H.DocID <> " & AgL.Chk_Text(mInternalCode) & " " & _
                            " And IsNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "'  " & HeaderConStr & LineConStr & _
                            " GROUP BY H.Item, H.LotNo, H.Process, H.Dimension1, H.Dimension2 " & _
                            " HAVING Round(isnull(sum(H.Qty_Rec),0) - isnull(sum(H.Qty_Iss),0),4) > 0 "
    End Function

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

        If Not AgL.StrCmp(Topctrl1.Mode, "Browse") Then
            LblCurrentStock.Visible = True : LblCurrentStockText.Visible = True
            LblCurrentStock.Text = Format(AgTemplate.ClsMain.FunRetStock(Dgl1.Item(Col1Item, e.RowIndex).Tag, mSearchCode, , TxtFromGodown.Tag, , , TxtV_Date.Text, Dgl1.Item(Col1LotNo, e.RowIndex).Value), "0.".PadRight(Dgl1.Item(Col1QtyDecimalPlaces, e.RowIndex).Value + 2, "0"))
        Else
            LblCurrentStock.Visible = False : LblCurrentStockText.Visible = False
        End If
    End Sub

    Private Sub RbtIssueForReqisition_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RbtIssueForReqisition.CheckedChanged, RbtIssueDirect.CheckedChanged, RbtnForStock.CheckedChanged
        Dgl1.AgHelpDataSet(Col1Item) = Nothing
    End Sub

    Private Sub BtnImprtFromText_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnImprtFromText.Click
        If AgL.StrCmp(TxtV_Type.Tag, "CAISS") Then
            ProcImportFromExcel()
        Else
            If AgL.StrCmp(BtnImprtFromText.Text, ImportAction_NewImport) Then
                FImportFromTextFile()
                ChkShowOnlyImportedRecords.Checked = True
                ChkShowOnlyImportedRecords.Visible = True
            Else
                mQry = " UPDATE JobOrder Set EntryStatus = '" & AgTemplate.ClsMain.LogStatus.LogImportClear & "' Where DocId = '" & mSearchCode & "'"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                FIniMaster(1)
                MoveRec()
            End If
        End If
    End Sub

    Private Sub ProcImportFromExcel()
        Dim DtMain As DataTable
        Dim DrTemp As DataRow() = Nothing
        Dim strCond$ = ""
        Dim mQry$ = "", ErrorLog$ = "", bFileName$ = ""
        Dim I, J As Integer
        Dim DtItem As DataTable = Nothing
        Dim StrErrLog As String = ""
        'Try

        If Topctrl1.Mode <> "Add" Then
            MsgBox("Import can be done only on Add mode")
            Exit Sub
        End If

        mQry = "Select '' as Srl, 'Item' as [Field Name], 'Text' as [Data Type], 100 as [Length] "
        mQry = mQry + "Union All Select  '' as Srl,'Qty' as [Field Name], 'Number' as [Data Type], '' as [Length] "

        DtMain = AgL.FillData(mQry, AgL.GCn).Tables(0)
        Dim ObjFrmImport As New FrmImportFromExcel
        ObjFrmImport.LblTitle.Text = "Import from excel"
        ObjFrmImport.Dgl1.DataSource = DtMain


        ObjFrmImport.ShowDialog()
        bFileName = ObjFrmImport.TxtExcelPath.Text

        If Not AgL.StrCmp(ObjFrmImport.UserAction, "OK") Then Exit Sub

        DtMain = ObjFrmImport.P_DsExcelData.Tables(0)



        For I = 0 To DtMain.Rows.Count - 1
            If AgL.XNull(DtMain.Rows(I)("Item")) <> "" Then
                If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")) <> "" Then
                    strCond += " And CharIndex('|' + H.ItemType + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")) & "') > 0 "
                End If

                mQry = " Select Count(*) From Item H Where H.Description = " & AgL.Chk_Text(AgL.XNull(DtMain.Rows(I)("Item"))) & " " & strCond
                If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar = 0 Then
                    If ErrorLog = "" Then
                        ErrorLog = vbCrLf & "These Items Are Not Present In Master" & vbCrLf
                        ErrorLog += AgL.XNull(DtMain.Rows(I)("Item")) & ", "
                    Else
                        ErrorLog += AgL.XNull(DtMain.Rows(I)("Item")) & ", "
                    End If
                End If
            End If
        Next


        With DtMain
            For I = 0 To .Rows.Count - 1
                If AgL.VNull(.Rows(I)("Qty")) = 0 Then
                    ErrorLog += "Qty is 0 at row no " & (I + 1).ToString & "" & vbCrLf
                End If
            Next
        End With

        If ErrorLog <> "" Then
            If File.Exists(My.Application.Info.DirectoryPath + " \ " + "ErrorLog.txt") Then
                My.Computer.FileSystem.WriteAllText(My.Application.Info.DirectoryPath + "\" + "ErrorLog.txt", ErrorLog, False)
            Else
                File.Create(My.Application.Info.DirectoryPath + " \ " + "ErrorLog.txt")
                My.Computer.FileSystem.WriteAllText(My.Application.Info.DirectoryPath + " \ " + "ErrorLog.txt", ErrorLog, False)
            End If
            System.Diagnostics.Process.Start("notepad.exe", My.Application.Info.DirectoryPath + "\" + "ErrorLog.txt")
            Exit Sub
        End If

        For I = 0 To DtMain.Rows.Count - 1


            'For J = 0 To DtTemp.Rows.Count - 1
            Dgl1.Rows.Add()
            Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count - 1


            Dgl1.Item(Col1Item, I).Value = AgL.XNull(DtMain.Rows(I)("Item"))
            mQry = " Select I.Code As ItemCode From Item I Where I.Description = '" & AgL.XNull(DtMain.Rows(I)("Item")) & "'"
            Dgl1.Item(Col1Item, I).Tag = AgL.XNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar)
            Dgl1.Item(Col1ItemCode, I).Tag = AgL.XNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar)

            mQry = " Select ManualCode From Item Where Code = '" & Dgl1.Item(Col1ItemCode, J).Tag & "'"
            Dgl1.Item(Col1ItemCode, I).Value = AgL.XNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar)

            Dgl1.Item(Col1Qty, I).Value = AgL.VNull(DtMain.Rows(I)("Qty"))


            mQry = "SELECT I.Unit, I.Measure As MeasurePerPcs, " & _
                    " I.MeasureUnit, I.Rate, " & _
                    " U.DecimalPlaces As QtyDecimalPlaces, U1.DecimalPlaces As MeasureDecimalPlaces, I.Specification " & _
                    " FROM Item I " & _
                    " LEFT JOIN Unit U On I.Unit = U.Code " & _
                    " LEFT JOIN Unit U1 On I.MeasureUnit = U1.Code " & _
                    " Where I.Code = '" & Dgl1.Item(Col1Item, I).Tag & "' "
            DtItem = AgL.FillData(mQry, AgL.GCn).Tables(0)

            With DtItem
                If .Rows.Count > 0 Then
                    Dgl1.Item(Col1Unit, I).Value = AgL.XNull(DtItem.Rows(0)("Unit"))

                    Dgl1.Item(Col1MeasurePerPcs, I).Value = AgL.VNull(DtItem.Rows(0)("MeasurePerPcs"))
                    Dgl1.Item(Col1MeasureUnit, I).Value = AgL.XNull(DtItem.Rows(0)("MeasureUnit"))
                    Dgl1.Item(Col1QtyDecimalPlaces, I).Value = AgL.VNull(DtItem.Rows(0)("QtyDecimalPlaces"))
                    Dgl1.Item(Col1MeasureDecimalPlaces, I).Value = AgL.VNull(DtItem.Rows(0)("MeasureDecimalPlaces"))
                    Dgl1.Item(Col1Specification, I).Value = AgL.XNull(DtItem.Rows(0)("Specification"))
                End If
            End With
        Next
        Calculation()
        'Next
        'Catch ex As Exception
        '    MsgBox(ex.Message)
        'Finally
        '    'FW.Dispose()
        'End Try
    End Sub


    Private Sub FImportFromTextFile()
        Dim Sr As StreamReader
        Dim Opn As New OpenFileDialog
        Dim mItemDivisionCode$ = ""
        Dim mItemDivisionText$ = ""

        Dim Line$ = "", mDateTime$ = "", mMachine$ = "", mProcess$ = "", mJobRecBy$ = "", mBarcode$ = "", mSKU$ = ""
        Dim mDefaultGodown$ = "", mJobType$ = "", mJobWorker$ = "", mIssRec$ = "", StrQry$ = ""
        Dim mMeasurePerPcs As Double = 0
        Dim ErrorLog$ = "", StrMessage$ = ""
        Dim mItem_UidDesc$ = ""

        Dim I As Integer, J As Integer = 0, bBarCodeQty As Integer = 0
        Dim DtTemp As DataTable, DtLineRec As DataTable
        Dim strArr() As String

        DtTemp = AgL.FillData("Select Godown from EnviroDefaultGodown Where Div_Code = '" & AgL.PubDivCode & "' and Site_Code = '" & AgL.PubSiteCode & "' ", AgL.GCn).Tables(0)
        If DtTemp.Rows.Count > 0 Then
            mDefaultGodown = DtTemp.Rows(0)("Godown")
        End If


        If Topctrl1.Mode <> "Add" Then
            MsgBox("Import can be done only on Add mode")
            Exit Sub
            If TxtProcess.Text = "" Then
                MsgBox("Process is mandatory to import records")
                Exit Sub
            End If
        End If

        ImportMessegeStr = ""
        ImportMode = True

        Opn.ShowDialog()

        If Opn.FileName = "" Then Exit Sub

        'mItemDivisionCode = TxtItemDivision.Tag
        'mItemDivisionText = TxtItemDivision.Text

        Sr = New StreamReader(Opn.FileName)

        StrMessage = ""

        StrQry = "  Declare @TmpTable as Table " & _
                    " ( " & _
                    " Process nVarchar(10), " & _
                    " IssRec nVarchar(10), " & _
                    " JobWorker nVarchar(10), " & _
                    " OrderBy nVarchar(10), " & _
                    " BarCode nVarchar(10), " & _
                    " Sku nVarchar(10), " & _
                    " MeasurePerPcs Float " & _
                    " ) "

        Do
            I += 1
            Line = Sr.ReadLine()
            If Line IsNot Nothing Then
                strArr = Split(Line, ",")

                If strArr.Length <> 14 Then
                    MsgBox("Invalid records in file")
                    Exit Sub
                End If

                mDateTime = strArr(1)
                mMachine = strArr(3)
                mIssRec = strArr(5)
                mProcess = strArr(7)
                mJobWorker = strArr(9)
                mJobRecBy = strArr(11)
                mBarcode = strArr(13)

                mSKU = ""
                mItem_UidDesc = strArr(13)



                If mIssRec <> "I" Then MsgBox("IssRec Is Not Equal To ""I"".Can't Proceed.") : Exit Sub

                DtTemp = AgL.FillData("Select Process From ProcessCode Where Code = '" & mProcess & "' and Div_Code = '" & AgL.PubDivCode & "' ", AgL.GcnRead).Tables(0)
                If DtTemp.Rows.Count > 0 Then
                    mProcess = DtTemp.Rows(0)("Process")
                Else
                    If StrMessage <> "" Then StrMessage += vbCrLf
                    StrMessage += "Invalid Value Found in Process Field at Row No. " & I
                End If

                If mProcess <> TxtProcess.Tag Then
                    MsgBox("Process In Text File Is Not Equal To " & TxtProcess.Text & "", MsgBoxStyle.Information)
                    Exit Sub
                End If

                DtTemp = AgL.FillData("Select SubCode From SubGroup WITH (NoLock) Where ManualCode = '" & mJobWorker & "'  And CharIndex('|' + '" & AgL.PubDivCode & "' + '|', DivisionList) > 0  and Site_Code = '" & AgL.PubSiteCode & "'", AgL.GcnRead).Tables(0)
                If DtTemp.Rows.Count > 0 Then
                    mJobWorker = DtTemp.Rows(0)("SubCode")
                Else
                    If StrMessage <> "" Then StrMessage += vbCrLf
                    StrMessage += "Invalid Value Found in JobWorker Field at Row No. " & I
                End If

                'and Div_Code = '" & AgL.PubDivCode & "'
                DtTemp = AgL.FillData("Select SubCode From SubGroup WITH (NoLock) Where ManualCode = '" & mJobRecBy & "'  and Site_Code = '" & AgL.PubSiteCode & "'", AgL.GcnRead).Tables(0)
                If DtTemp.Rows.Count > 0 Then
                    mJobRecBy = DtTemp.Rows(0)("SubCode")
                Else
                    If StrMessage <> "" Then StrMessage += vbCrLf
                    StrMessage += "Invalid Value Found in JobRecBy Field at Row No. " & I
                End If

                If mBarcode.Trim = "" Then
                    If StrMessage <> "" Then StrMessage += vbCrLf
                    StrMessage += "No value defined in Barcode Field at Row No. " & I
                End If

                If mBarcode.Trim <> "" Then
                    DtTemp = AgL.FillData("Select Item_Uid.Code, Item_Uid.Item, Item.Measure From Item_UID LEFT JOIN Item On Item_Uid.Item = Item.Code Where Item_Uid.Item_UID = '" & mBarcode & "' ", AgL.GCn).Tables(0)
                    If DtTemp.Rows.Count > 0 Then
                        mBarcode = DtTemp.Rows(0)("Code")
                        mSKU = DtTemp.Rows(0)("Item")
                        mMeasurePerPcs = AgL.VNull(DtTemp.Rows(0)("Measure"))
                    Else
                        If StrMessage <> "" Then StrMessage += vbCrLf
                        MsgBox("Invalid Value Found in Barcode Field at Row No. " & I)
                    End If
                End If

                If StrMessage <> "" Then
                    MsgBox(StrMessage)
                    Exit Sub
                End If

                Dim Item_UidError$ = ""
                Item_UidError = FCheck_Item_UID(mItem_UidDesc)
                If Item_UidError = "" Then
                    StrQry += " Insert Into @TmpTable (Process, IssRec, JobWorker, OrderBy, Barcode, Sku, MeasurePerPcs) "
                    StrQry += " Values (" & AgL.Chk_Text(mProcess) & ", " & AgL.Chk_Text(mIssRec) & ", " & _
                                " " & AgL.Chk_Text(mJobWorker) & ", " & AgL.Chk_Text(mJobRecBy) & ", " & _
                                " " & AgL.Chk_Text(mBarcode) & ", " & AgL.Chk_Text(mSKU) & ", " & AgL.Chk_Text(mMeasurePerPcs) & ") "
                Else
                    ImportMessegeStr += Item_UidError & vbCrLf
                End If

            End If
        Loop Until Line Is Nothing
        Sr.Close()


        mQry = StrQry & " Select Process, IssRec, JobWorker, OrderBy " & _
                " From @TmpTable " & _
                " Where Process = '" & mProcess & "' And IssRec = 'I' " & _
                " Group by Process, IssRec, JobWorker, OrderBy "
        DtTemp = AgL.FillData(mQry, AgL.GcnRead).tables(0)

        For I = 0 To DtTemp.Rows.Count - 1
            If I > 0 Then Topctrl1.FButtonClick(0)

            Dgl1.Focus()

            TxtProcess.Tag = mProcess
            TxtProcess.Text = AgL.XNull(AgL.Dman_Execute("Select Description From Process Where NCat = '" & TxtProcess.Tag & "' ", AgL.GCn).ExecuteScalar)

            'TxtOrderBy.Tag = DtTemp.Rows(I)("OrderBy")
            'TxtOrderBy.Text = AgL.XNull(AgL.Dman_Execute("Select Name From SubGroup Sg Where SubCode = '" & TxtOrderBy.Tag & "'", AgL.GCn).ExecuteScalar)

            TxtParty.Tag = DtTemp.Rows(I)("JobWorker")
            TxtParty.Text = AgL.XNull(AgL.Dman_Execute("Select Name From SubGroup Sg Where SubCode = '" & TxtParty.Tag & "'", AgL.GCn).ExecuteScalar)

            TxtFromGodown.Tag = mDefaultGodown
            TxtFromGodown.Text = AgL.XNull(AgL.Dman_Execute("Select Description From Godown Where Code = '" & TxtFromGodown.Tag & "'", AgL.GCn).ExecuteScalar)

            'If TxtV_Date.Text <> "" And TxtDueDate.Text = "" And AgL.PubDtEnviro.Rows.Count > 0 Then
            '    TxtDueDate.Text = DateAdd(DateInterval.Day, AgL.VNull(AgL.PubDtEnviro.Rows(0)("DefaultDueDays")), CDate(TxtV_Date.Text))
            'End If

            'If mItemDivisionCode <> "" Then TxtItemDivision.Tag = mItemDivisionCode
            'If mItemDivisionText <> "" Then TxtItemDivision.Text = mItemDivisionText

            'ProcFillJobValues()

            'TxtInsideOutside.Text = AgL.XNull(AgL.Dman_Execute("Select InsideOutside From JobWorker Where SubCode = '" & TxtJobWorker.Tag & "'", AgL.GCn).ExecuteScalar)

            mQry = StrQry & " Select Process, Sku, BarCode, Max(MeasurePerPcs) As MeasurePerPcs From @TmpTable " & _
                    " Where Process = '" & TxtProcess.Tag & "' And Jobworker = '" & TxtParty.Tag & "' " & _
                    " Group By Process, Sku, BarCode " & _
                    " Order By MeasurePerPcs, Sku "
            DtLineRec = AgL.FillData(mQry, AgL.GcnRead).Tables(0)

            For J = 0 To DtLineRec.Rows.Count - 1
                Dgl1.Rows.Add()
                Dgl1.Item(ColSNo, Dgl1.Rows.Count - 2).Value = Dgl1.Rows.Count - 1
                Dgl1.Item(Col1Item_UID, Dgl1.Rows.Count - 2).Tag = DtLineRec.Rows(J)("BarCode")
                Dgl1.Item(Col1Item_UID, Dgl1.Rows.Count - 2).Value = AgL.XNull(AgL.Dman_Execute("Select Item_Uid From Item_Uid Where Code = '" & DtLineRec.Rows(J)("BarCode") & "'", AgL.GCn).ExecuteScalar)

                'ImportMessegeStr = FCheck_Item_UID(Dgl1.Item(Col1Item_Uid, Dgl1.Rows.Count - 2).Tag, Dgl1.Rows.Count - 2)
                Validating_Item_Uid(Dgl1.Item(Col1Item_UID, Dgl1.Rows.Count - 2).Value, Dgl1.Rows.Count - 2)
            Next

            Calculation()


            Topctrl1.FButtonClick(13)

        Next

        If ImportMessegeStr <> "" Then
            If File.Exists(My.Application.Info.DirectoryPath + "\Error Log\" + AgL.PubUserName + "ErrorLog.txt") Then
                My.Computer.FileSystem.WriteAllText(My.Application.Info.DirectoryPath + "\Error Log\" + AgL.PubUserName + "ErrorLog.txt", ImportMessegeStr, False)
            Else
                File.Create(My.Application.Info.DirectoryPath + "\Error Log\" + AgL.PubUserName + "ErrorLog.txt").Dispose()
                My.Computer.FileSystem.WriteAllText(My.Application.Info.DirectoryPath + "\Error Log\" + AgL.PubUserName + "ErrorLog.txt", ImportMessegeStr, False)
            End If
            System.Diagnostics.Process.Start("notepad.exe", My.Application.Info.DirectoryPath + "\Error Log\" + AgL.PubUserName + "ErrorLog.txt")
            Exit Sub
        End If

        ImportMode = False
    End Sub

    Public Function FCheck_Item_UID(ByVal Item_UID As String) As String
        Dim Item_UidCode$ = "", ErrMsgStr$ = ""
        Dim DtTemp As DataTable = Nothing
        Dim mProcessSequence$ = ""
        Dim mProcessIterationsAllowed As Integer = 0

        mQry = " SELECT Code FROM Item_UID With (NoLock) WHERE Item_UID = '" & Item_UID & "'"
        Item_UidCode = AgL.XNull(AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar)
        If Item_UidCode = "" Then
            FCheck_Item_UID = "Carpet Id Is Not Valid."
            Exit Function
        Else
            FCheck_Item_UID = ""
        End If

        mQry = "Select ProcessSequence, " & _
                "       (Select Count(*) from ProcessSequenceDetail " & _
                "        Where Code = H.ProcessSequence And Process = '" & TxtProcess.Tag & "') As IterationsAllowed " & _
                " From Item H Where Code = (Select Item From Item_Uid Where Code = '" & Item_UidCode & "') "
        DtTemp = AgL.FillData(mQry, AgL.GcnRead).tables(0)
        If DtTemp.Rows.Count > 0 Then
            mProcessSequence = AgL.XNull(DtTemp.Rows(0)("ProcessSequence"))
            mProcessIterationsAllowed = AgL.VNull(DtTemp.Rows(0)("IterationsAllowed"))
        End If


        If mProcessSequence <> "" Then
            If Val(mProcessIterationsAllowed) > 0 Then
                mQry = "Select IsNull(Count(*),0) from JobIssRecUID " & _
                        " Where IssRec='I' And Process = '" & TxtProcess.Tag & "' " & _
                        " And Item_UID = '" & Item_UidCode & "' " & _
                        " And DocID <> '" & mSearchCode & "'  "
                If AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar + 1 > Val(mProcessIterationsAllowed) Then
                    If MsgBox("Carpet Id " & Item_UID & " has already completed this process.Do you want to issue it again", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2) = MsgBoxResult.No Then
                        FCheck_Item_UID = "Carpet Id " & Item_UID & " has already completed this process"
                        Exit Function
                    Else
                        FCheck_Item_UID = ""
                    End If
                End If
            End If
        End If

        'mQry = " Select Iu.Item_Uid From Item_Uid Iu LEFT JOIN Item I ON Iu.Item = I.Code Where Iu.Code = '" & Item_UidCode & "' And I.Div_Code <> '" & IIf(TxtItemDivision.Text <> "", TxtItemDivision.Tag, AgL.PubDivCode) & "'"
        DtTemp = AgL.FillData(mQry, AgL.GcnRead).Tables(0)
        If DtTemp.Rows.Count > 0 Then
            'FCheck_Item_UID = "Carpet Id " & AgL.XNull(DtTemp.Rows(0)("Item_Uid")) & " Does Not Belong To " & IIf(TxtItemDivision.Text <> "", TxtItemDivision.Text, AgL.PubDivName) & "."
            Exit Function
        Else
            FCheck_Item_UID = ""
        End If


        mQry = " Select RecDocID From Item_Uid With (NoLock) Where Code = '" & Item_UidCode & "' "
        If AgL.XNull(AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar) = "" Then
            FCheck_Item_UID = "Carpet Id " & Item_UID & " Is Not Received From Weaving Process."
            Exit Function
        Else
            FCheck_Item_UID = ""
        End If

        mQry = "SELECT Count(I.DocID) " & _
               " FROM (SELECT DocID, Item_UID FROM JobIssRecUID WITH (NoLock) " & _
               " WHERE Item_UID ='" & Item_UidCode & "' And IssRec= 'I') I " & _
               " LEFT JOIN JobIssRecUID R WITH (NoLock) ON I.DocID = R.JobRecDocID AND I.Item_UID = R.Item_UID  " & _
               " WHERE R.DocID IS NULL AND I.DocID <> '" & mSearchCode & "'"
        If AgL.VNull(AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar) > 0 Then
            mQry = "SELECT TOP 1 Sg.Name, H.ManualRefNo, H.V_Date, Vc.NCatDescription AS ProcessDesc " & _
                    " FROM (SELECT DocID, Item_UID FROM JobIssRecUID WITH (NoLock) " & _
                    " WHERE Item_UID ='" & Item_UidCode & "' And IssRec='I') I " & _
                    " LEFT JOIN JobIssRecUID R WITH (NoLock) ON I.DocID = R.JobRecDocID AND I.Item_UID = R.Item_UID  " & _
                    " LEFT JOIN JobOrder H WITH (NoLock) ON I.DocID = H.DocID " & _
                    " LEFT JOIN SubGroup Sg WITH (NoLock) ON H.JobWorker = Sg.SubCode " & _
                    " LEFT JOIN VoucherCat Vc WITH (NoLock) ON H.Process = Vc.NCat " & _
                    " WHERE R.DocID IS NULL AND I.DocID <> '" & mSearchCode & "' " & _
                    " ORDER BY H.V_Date Desc "
            DtTemp = AgL.FillData(mQry, AgL.GcnRead).Tables(0)
            FCheck_Item_UID = "Carpet Id " & Item_UID & " Is Already Issued To " & AgL.XNull(DtTemp.Rows(0)("Name")) & " For " & AgL.XNull(DtTemp.Rows(0)("ProcessDesc")) & " On Date " & AgL.XNull(DtTemp.Rows(0)("V_Date")) & " Against Ref No " & AgL.XNull(DtTemp.Rows(0)("ManualRefNo")) & "."
            Exit Function
        Else
            FCheck_Item_UID = ""
        End If
    End Function
End Class
