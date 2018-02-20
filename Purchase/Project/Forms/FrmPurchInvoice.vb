Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Windows.Forms
Imports System.Data.SQLite
Public Class FrmPurchInvoice
    Inherits AgTemplate.TempTransaction
    Dim mQry$

    Public Event BaseFunction_MoveRecLine(ByVal SearchCode As String, ByVal Sr As Integer, ByVal mGridRow As Integer)
    Public Event BaseEvent_Save_InTransLine(ByVal SearchCode As String, ByVal Sr As Integer, ByVal mGridRow As Integer, ByVal Conn As SQLiteConnection, ByVal Cmd As SQLiteCommand)

    Public WithEvents AgCalcGrid1 As New AgStructure.AgCalcGrid
    Public WithEvents AgCustomGrid1 As New AgCustomFields.AgCustomGrid

    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Protected Const ColSNo As String = "S.No."
    Protected Const Col1PurchChallan As String = "Challan No"
    Protected Const Col1PurchChallanSr As String = "Purch Challan Sr"
    Protected Const Col1Item_UID As String = "Item UID"
    Protected Const Col1ItemCode As String = "Item Code"
    Protected Const Col1Item As String = "Item"
    Protected Const Col1Dimension1 As String = "Dimension1"
    Protected Const Col1Dimension2 As String = "Dimension2"
    Protected Const Col1Specification As String = "Specification"
    Protected Const Col1BaleNo As String = "Bale No"
    Protected Const Col1LotNo As String = "Lot No"
    Protected Const Col1SalesTaxGroup As String = "Sales Tax Group Item"
    Protected Const Col1DocQty As String = "Doc Qty"
    Protected Const Col1FreeQty As String = "Free Qty"
    Protected Const Col1RejQty As String = "Rej Qty"
    Protected Const Col1Qty As String = "Qty"
    Protected Const Col1Unit As String = "Unit"
    Protected Const Col1QtyDecimalPlaces As String = "Qty Decimal Places"
    Protected Const Col1DeliveryMeasure As String = "Delivery Measure"
    Protected Const Col1MeasurePerPcs As String = "Measure Per Pcs"
    Protected Const Col1PcsPerMeasure As String = "Pcs Per Measure"
    Protected Const Col1TotalDocMeasure As String = "Total Doc Measure"
    Protected Const Col1TotalFreeMeasure As String = "Total Free Measure"
    Protected Const Col1TotalRejMeasure As String = "Total Rej Measure"
    Protected Const Col1TotalMeasure As String = "Total Measure"
    Protected Const Col1MeasureUnit As String = "Measure Unit"
    Protected Const Col1MeasureDecimalPlaces As String = "Measure Decimal Places"
    Protected Const Col1DeliveryMeasureMultiplier As String = "Delivery Measure Multiplier"
    Protected Const Col1DeliveryMeasurePerPcs As String = "Delivery Measure Per Qty"
    Protected Const Col1TotalDocDeliveryMeasure As String = "Total Doc Delivery Measure"
    Protected Const Col1TotalFreeDeliveryMeasure As String = "Total Free Delivery Measure"
    Protected Const Col1TotalRejDeliveryMeasure As String = "Total Rej Delivery Measure"
    Protected Const Col1TotalDeliveryMeasure As String = "Total Delivery Measure"
    Protected Const Col1DeliveryMeasureDecimalPlaces As String = "Delivery Measure Decimal Places"
    Protected Const Col1Rate As String = "Rate"
    Protected Const Col1Amount As String = "Amount"
    Protected Const Col1ExpiryDate As String = "Expiry Date"
    Protected Const Col1Remark As String = "Remark"
    Protected Const Col1BillingType As String = "Billing Type"
    Protected Const Col1MRP As String = "MRP"
    Protected Const Col1Deal As String = "Deal"
    Protected Const Col1ProfitMarginPer As String = "Profit Margin %"
    Protected Const Col1PurchIndent As String = "PurchIndent"
    Protected Const Col1PurchIndentSr As String = "Purch Indent Sr"
    Protected Const Col1SaleRate As String = "Sale Rate"

    Dim IsSameUnit As Boolean = True
    Dim IsSameMeasureUnit As Boolean = True
    Dim IsSameDeliveryMeasureUnit As Boolean = True

    Dim intQtyDecimalPlaces As Integer = 0
    Dim intMeasureDecimalPlaces As Integer = 0
    Dim intDeliveryMeasureDecimalPlaces As Integer = 0

    Dim mIsEntryLocked As Boolean = False
    Public WithEvents TxtProcess As AgControls.AgTextBox
    Public WithEvents LblProcess As System.Windows.Forms.Label
    Friend WithEvents TP2 As TabPage
    Public WithEvents TxtTransporter As AgControls.AgTextBox
    Public WithEvents LblTransporter As Label
    Protected WithEvents TxtAgent As AgControls.AgTextBox
    Protected WithEvents LblAgent As Label
    Dim DGL As New AgControls.AgDataGrid

    Public Sub New(ByVal StrUPVar As String, ByVal DTUP As DataTable, ByVal strNCat As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Topctrl1.FSetParent(Me, StrUPVar, DTUP)
        Topctrl1.SetDisp(True)

        EntryNCat = strNCat

        mQry = "Select H.* from Voucher_Type_Settings H Left Join Voucher_Type Vt On H.V_Type = Vt.V_Type  Where Vt.NCat In ('" & EntryNCat & "') And H.Div_Code = '" & AgL.PubDivCode & "' And H.Site_Code ='" & AgL.PubSiteCode & "' "
        DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
    End Sub

#Region "Form Designer Code"
    Private Sub InitializeComponent()
        Me.Dgl1 = New AgControls.AgDataGrid()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TxtVendor = New AgControls.AgTextBox()
        Me.LblVendor = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.LblTotalDeliveryMeasure = New System.Windows.Forms.Label()
        Me.LblTotalDeliveryMeasureText = New System.Windows.Forms.Label()
        Me.LblTotalMeasure = New System.Windows.Forms.Label()
        Me.LblTotalMeasureText = New System.Windows.Forms.Label()
        Me.LblTotalQty = New System.Windows.Forms.Label()
        Me.LblTotalAmount = New System.Windows.Forms.Label()
        Me.LblTotalQtyText = New System.Windows.Forms.Label()
        Me.LblTotalAmountText = New System.Windows.Forms.Label()
        Me.Pnl1 = New System.Windows.Forms.Panel()
        Me.TxtStructure = New AgControls.AgTextBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.TxtSalesTaxGroupParty = New AgControls.AgTextBox()
        Me.Label27 = New System.Windows.Forms.Label()
        Me.TxtRemarks = New AgControls.AgTextBox()
        Me.Label30 = New System.Windows.Forms.Label()
        Me.TxtReferenceNo = New AgControls.AgTextBox()
        Me.LblReferenceNo = New System.Windows.Forms.Label()
        Me.LblVendorDocNo = New System.Windows.Forms.Label()
        Me.TxtVendorDocNo = New AgControls.AgTextBox()
        Me.LvlVendorDocDate = New System.Windows.Forms.Label()
        Me.TxtVendorDocDate = New AgControls.AgTextBox()
        Me.LblCurrency = New System.Windows.Forms.Label()
        Me.TxtCurrency = New AgControls.AgTextBox()
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
        Me.PnlCalcGrid = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.RbtInvoiceDirect = New System.Windows.Forms.RadioButton()
        Me.RbtInvoiceForChallan = New System.Windows.Forms.RadioButton()
        Me.GrpDirectInvoice = New System.Windows.Forms.GroupBox()
        Me.BtnFillPurchChallan = New System.Windows.Forms.Button()
        Me.PnlCustomGrid = New System.Windows.Forms.Panel()
        Me.TxtCustomFields = New AgControls.AgTextBox()
        Me.TxtGodown = New AgControls.AgTextBox()
        Me.LblGodown = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TxtBillToParty = New AgControls.AgTextBox()
        Me.LblPostToAc = New System.Windows.Forms.Label()
        Me.BtnFillPartyDetail = New System.Windows.Forms.Button()
        Me.TxtNature = New AgControls.AgTextBox()
        Me.TxtProcess = New AgControls.AgTextBox()
        Me.LblProcess = New System.Windows.Forms.Label()
        Me.TP2 = New System.Windows.Forms.TabPage()
        Me.TxtTransporter = New AgControls.AgTextBox()
        Me.LblTransporter = New System.Windows.Forms.Label()
        Me.TxtAgent = New AgControls.AgTextBox()
        Me.LblAgent = New System.Windows.Forms.Label()
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
        Me.GrpDirectInvoice.SuspendLayout()
        Me.TP2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.Location = New System.Drawing.Point(829, 581)
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
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(648, 581)
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
        Me.GBoxApprove.Location = New System.Drawing.Point(467, 581)
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
        Me.GBoxEntryType.Location = New System.Drawing.Point(168, 581)
        Me.GBoxEntryType.Size = New System.Drawing.Size(119, 40)
        '
        'TxtEntryType
        '
        Me.TxtEntryType.Location = New System.Drawing.Point(3, 19)
        Me.TxtEntryType.Tag = ""
        '
        'GrpUP
        '
        Me.GrpUP.Location = New System.Drawing.Point(16, 581)
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
        Me.GroupBox1.Location = New System.Drawing.Point(2, 577)
        Me.GroupBox1.Size = New System.Drawing.Size(1002, 4)
        '
        'GBoxDivision
        '
        Me.GBoxDivision.Location = New System.Drawing.Point(320, 581)
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
        Me.LblV_No.Location = New System.Drawing.Point(276, 267)
        Me.LblV_No.Size = New System.Drawing.Size(71, 16)
        Me.LblV_No.Tag = ""
        Me.LblV_No.Text = "Invoice No."
        Me.LblV_No.Visible = False
        '
        'TxtV_No
        '
        Me.TxtV_No.AgSelectedValue = ""
        Me.TxtV_No.BackColor = System.Drawing.Color.White
        Me.TxtV_No.Location = New System.Drawing.Point(384, 266)
        Me.TxtV_No.Size = New System.Drawing.Size(163, 18)
        Me.TxtV_No.TabIndex = 3
        Me.TxtV_No.Tag = ""
        Me.TxtV_No.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.TxtV_No.Visible = False
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(141, 39)
        Me.Label2.Tag = ""
        '
        'LblV_Date
        '
        Me.LblV_Date.BackColor = System.Drawing.Color.Transparent
        Me.LblV_Date.Location = New System.Drawing.Point(40, 34)
        Me.LblV_Date.Size = New System.Drawing.Size(78, 16)
        Me.LblV_Date.Tag = ""
        Me.LblV_Date.Text = "Invoice Date"
        '
        'LblV_TypeReq
        '
        Me.LblV_TypeReq.Location = New System.Drawing.Point(356, 19)
        Me.LblV_TypeReq.Tag = ""
        '
        'TxtV_Date
        '
        Me.TxtV_Date.AgSelectedValue = ""
        Me.TxtV_Date.BackColor = System.Drawing.Color.White
        Me.TxtV_Date.Location = New System.Drawing.Point(160, 33)
        Me.TxtV_Date.TabIndex = 2
        Me.TxtV_Date.Tag = ""
        '
        'LblV_Type
        '
        Me.LblV_Type.Location = New System.Drawing.Point(266, 15)
        Me.LblV_Type.Size = New System.Drawing.Size(78, 16)
        Me.LblV_Type.Tag = ""
        Me.LblV_Type.Text = "Invoice Type"
        '
        'TxtV_Type
        '
        Me.TxtV_Type.AgLastValueTag = ""
        Me.TxtV_Type.AgLastValueText = ""
        Me.TxtV_Type.AgSelectedValue = ""
        Me.TxtV_Type.BackColor = System.Drawing.Color.White
        Me.TxtV_Type.Location = New System.Drawing.Point(374, 13)
        Me.TxtV_Type.Size = New System.Drawing.Size(195, 18)
        Me.TxtV_Type.TabIndex = 1
        Me.TxtV_Type.Tag = ""
        '
        'LblSite_CodeReq
        '
        Me.LblSite_CodeReq.Location = New System.Drawing.Point(141, 19)
        Me.LblSite_CodeReq.Tag = ""
        '
        'LblSite_Code
        '
        Me.LblSite_Code.BackColor = System.Drawing.Color.Transparent
        Me.LblSite_Code.Location = New System.Drawing.Point(40, 14)
        Me.LblSite_Code.Size = New System.Drawing.Size(87, 16)
        Me.LblSite_Code.Tag = ""
        Me.LblSite_Code.Text = "Branch Name"
        '
        'TxtSite_Code
        '
        Me.TxtSite_Code.AgSelectedValue = ""
        Me.TxtSite_Code.BackColor = System.Drawing.Color.White
        Me.TxtSite_Code.Location = New System.Drawing.Point(160, 13)
        Me.TxtSite_Code.Size = New System.Drawing.Size(100, 18)
        Me.TxtSite_Code.TabIndex = 0
        Me.TxtSite_Code.Tag = ""
        '
        'LblDocId
        '
        Me.LblDocId.Tag = ""
        '
        'LblPrefix
        '
        Me.LblPrefix.Location = New System.Drawing.Point(336, 267)
        Me.LblPrefix.Tag = ""
        Me.LblPrefix.Visible = False
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TP2)
        Me.TabControl1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl1.Location = New System.Drawing.Point(-4, 40)
        Me.TabControl1.Size = New System.Drawing.Size(992, 135)
        Me.TabControl1.TabIndex = 0
        Me.TabControl1.Controls.SetChildIndex(Me.TP2, 0)
        Me.TabControl1.Controls.SetChildIndex(Me.TP1, 0)
        '
        'TP1
        '
        Me.TP1.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.TP1.Controls.Add(Me.TxtAgent)
        Me.TP1.Controls.Add(Me.LblAgent)
        Me.TP1.Controls.Add(Me.BtnFillPartyDetail)
        Me.TP1.Controls.Add(Me.TxtSalesTaxGroupParty)
        Me.TP1.Controls.Add(Me.Label27)
        Me.TP1.Controls.Add(Me.Label5)
        Me.TP1.Controls.Add(Me.TxtBillToParty)
        Me.TP1.Controls.Add(Me.LblPostToAc)
        Me.TP1.Controls.Add(Me.Label1)
        Me.TP1.Controls.Add(Me.Label4)
        Me.TP1.Controls.Add(Me.TxtVendor)
        Me.TP1.Controls.Add(Me.LblVendor)
        Me.TP1.Controls.Add(Me.TxtVendorDocNo)
        Me.TP1.Controls.Add(Me.LblVendorDocNo)
        Me.TP1.Controls.Add(Me.TxtVendorDocDate)
        Me.TP1.Controls.Add(Me.LvlVendorDocDate)
        Me.TP1.Controls.Add(Me.Label25)
        Me.TP1.Controls.Add(Me.TxtReferenceNo)
        Me.TP1.Controls.Add(Me.TxtStructure)
        Me.TP1.Controls.Add(Me.LblReferenceNo)
        Me.TP1.Location = New System.Drawing.Point(4, 22)
        Me.TP1.Size = New System.Drawing.Size(984, 109)
        Me.TP1.Text = "Document Detail"
        Me.TP1.Controls.SetChildIndex(Me.LblReferenceNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtStructure, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtReferenceNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label25, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblPrefix, 0)
        Me.TP1.Controls.SetChildIndex(Me.LvlVendorDocDate, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtVendorDocDate, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_No, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblVendorDocNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtV_No, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtVendorDocNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblVendor, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtVendor, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label4, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_TypeReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtV_Type, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_Type, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblSite_CodeReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtV_Date, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label2, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtSite_Code, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_Date, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblDocId, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtDocId, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblSite_Code, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label1, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblPostToAc, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtBillToParty, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label5, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label27, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtSalesTaxGroupParty, 0)
        Me.TP1.Controls.SetChildIndex(Me.BtnFillPartyDetail, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblAgent, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtAgent, 0)
        '
        'Topctrl1
        '
        Me.Topctrl1.Size = New System.Drawing.Size(984, 41)
        Me.Topctrl1.TabIndex = 7
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
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(141, 60)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(10, 7)
        Me.Label4.TabIndex = 694
        Me.Label4.Text = "Ä"
        '
        'TxtVendor
        '
        Me.TxtVendor.AgAllowUserToEnableMasterHelp = False
        Me.TxtVendor.AgLastValueTag = Nothing
        Me.TxtVendor.AgLastValueText = Nothing
        Me.TxtVendor.AgMandatory = True
        Me.TxtVendor.AgMasterHelp = False
        Me.TxtVendor.AgNumberLeftPlaces = 8
        Me.TxtVendor.AgNumberNegetiveAllow = False
        Me.TxtVendor.AgNumberRightPlaces = 2
        Me.TxtVendor.AgPickFromLastValue = False
        Me.TxtVendor.AgRowFilter = ""
        Me.TxtVendor.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtVendor.AgSelectedValue = Nothing
        Me.TxtVendor.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtVendor.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtVendor.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtVendor.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtVendor.Location = New System.Drawing.Point(160, 53)
        Me.TxtVendor.MaxLength = 0
        Me.TxtVendor.Name = "TxtVendor"
        Me.TxtVendor.Size = New System.Drawing.Size(380, 18)
        Me.TxtVendor.TabIndex = 4
        '
        'LblVendor
        '
        Me.LblVendor.AutoSize = True
        Me.LblVendor.BackColor = System.Drawing.Color.Transparent
        Me.LblVendor.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblVendor.Location = New System.Drawing.Point(40, 53)
        Me.LblVendor.Name = "LblVendor"
        Me.LblVendor.Size = New System.Drawing.Size(55, 16)
        Me.LblVendor.TabIndex = 693
        Me.LblVendor.Text = "Supplier"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Cornsilk
        Me.Panel1.Controls.Add(Me.LblTotalDeliveryMeasure)
        Me.Panel1.Controls.Add(Me.LblTotalDeliveryMeasureText)
        Me.Panel1.Controls.Add(Me.LblTotalMeasure)
        Me.Panel1.Controls.Add(Me.LblTotalMeasureText)
        Me.Panel1.Controls.Add(Me.LblTotalQty)
        Me.Panel1.Controls.Add(Me.LblTotalAmount)
        Me.Panel1.Controls.Add(Me.LblTotalQtyText)
        Me.Panel1.Controls.Add(Me.LblTotalAmountText)
        Me.Panel1.Location = New System.Drawing.Point(4, 386)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(975, 23)
        Me.Panel1.TabIndex = 694
        '
        'LblTotalDeliveryMeasure
        '
        Me.LblTotalDeliveryMeasure.AutoSize = True
        Me.LblTotalDeliveryMeasure.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalDeliveryMeasure.ForeColor = System.Drawing.Color.Black
        Me.LblTotalDeliveryMeasure.Location = New System.Drawing.Point(869, 3)
        Me.LblTotalDeliveryMeasure.Name = "LblTotalDeliveryMeasure"
        Me.LblTotalDeliveryMeasure.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalDeliveryMeasure.TabIndex = 716
        Me.LblTotalDeliveryMeasure.Text = "."
        '
        'LblTotalDeliveryMeasureText
        '
        Me.LblTotalDeliveryMeasureText.AutoSize = True
        Me.LblTotalDeliveryMeasureText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalDeliveryMeasureText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalDeliveryMeasureText.Location = New System.Drawing.Point(702, 3)
        Me.LblTotalDeliveryMeasureText.Name = "LblTotalDeliveryMeasureText"
        Me.LblTotalDeliveryMeasureText.Size = New System.Drawing.Size(161, 16)
        Me.LblTotalDeliveryMeasureText.TabIndex = 715
        Me.LblTotalDeliveryMeasureText.Text = "Total Deilvery Measure :"
        '
        'LblTotalMeasure
        '
        Me.LblTotalMeasure.AutoSize = True
        Me.LblTotalMeasure.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalMeasure.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalMeasure.Location = New System.Drawing.Point(576, 3)
        Me.LblTotalMeasure.Name = "LblTotalMeasure"
        Me.LblTotalMeasure.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalMeasure.TabIndex = 666
        Me.LblTotalMeasure.Text = "."
        Me.LblTotalMeasure.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTotalMeasureText
        '
        Me.LblTotalMeasureText.AutoSize = True
        Me.LblTotalMeasureText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalMeasureText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalMeasureText.Location = New System.Drawing.Point(465, 3)
        Me.LblTotalMeasureText.Name = "LblTotalMeasureText"
        Me.LblTotalMeasureText.Size = New System.Drawing.Size(105, 16)
        Me.LblTotalMeasureText.TabIndex = 665
        Me.LblTotalMeasureText.Text = "Total Measure :"
        '
        'LblTotalQty
        '
        Me.LblTotalQty.AutoSize = True
        Me.LblTotalQty.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalQty.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalQty.Location = New System.Drawing.Point(97, 3)
        Me.LblTotalQty.Name = "LblTotalQty"
        Me.LblTotalQty.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalQty.TabIndex = 660
        Me.LblTotalQty.Text = "."
        Me.LblTotalQty.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTotalAmount
        '
        Me.LblTotalAmount.AutoSize = True
        Me.LblTotalAmount.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalAmount.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalAmount.Location = New System.Drawing.Point(332, 4)
        Me.LblTotalAmount.Name = "LblTotalAmount"
        Me.LblTotalAmount.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalAmount.TabIndex = 662
        Me.LblTotalAmount.Text = "."
        Me.LblTotalAmount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTotalQtyText
        '
        Me.LblTotalQtyText.AutoSize = True
        Me.LblTotalQtyText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalQtyText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalQtyText.Location = New System.Drawing.Point(12, 3)
        Me.LblTotalQtyText.Name = "LblTotalQtyText"
        Me.LblTotalQtyText.Size = New System.Drawing.Size(72, 16)
        Me.LblTotalQtyText.TabIndex = 659
        Me.LblTotalQtyText.Text = "Total Qty :"
        '
        'LblTotalAmountText
        '
        Me.LblTotalAmountText.AutoSize = True
        Me.LblTotalAmountText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalAmountText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalAmountText.Location = New System.Drawing.Point(228, 3)
        Me.LblTotalAmountText.Name = "LblTotalAmountText"
        Me.LblTotalAmountText.Size = New System.Drawing.Size(100, 16)
        Me.LblTotalAmountText.TabIndex = 661
        Me.LblTotalAmountText.Text = "Total Amount :"
        '
        'Pnl1
        '
        Me.Pnl1.Location = New System.Drawing.Point(4, 202)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(975, 184)
        Me.Pnl1.TabIndex = 1
        '
        'TxtStructure
        '
        Me.TxtStructure.AgAllowUserToEnableMasterHelp = False
        Me.TxtStructure.AgLastValueTag = Nothing
        Me.TxtStructure.AgLastValueText = Nothing
        Me.TxtStructure.AgMandatory = False
        Me.TxtStructure.AgMasterHelp = False
        Me.TxtStructure.AgNumberLeftPlaces = 8
        Me.TxtStructure.AgNumberNegetiveAllow = False
        Me.TxtStructure.AgNumberRightPlaces = 2
        Me.TxtStructure.AgPickFromLastValue = False
        Me.TxtStructure.AgRowFilter = ""
        Me.TxtStructure.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtStructure.AgSelectedValue = Nothing
        Me.TxtStructure.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtStructure.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtStructure.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtStructure.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtStructure.Location = New System.Drawing.Point(641, 221)
        Me.TxtStructure.MaxLength = 20
        Me.TxtStructure.Name = "TxtStructure"
        Me.TxtStructure.Size = New System.Drawing.Size(60, 18)
        Me.TxtStructure.TabIndex = 15
        Me.TxtStructure.Visible = False
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.BackColor = System.Drawing.Color.Transparent
        Me.Label25.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(569, 222)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(61, 16)
        Me.Label25.TabIndex = 715
        Me.Label25.Text = "Structure"
        Me.Label25.Visible = False
        '
        'TxtSalesTaxGroupParty
        '
        Me.TxtSalesTaxGroupParty.AgAllowUserToEnableMasterHelp = False
        Me.TxtSalesTaxGroupParty.AgLastValueTag = Nothing
        Me.TxtSalesTaxGroupParty.AgLastValueText = Nothing
        Me.TxtSalesTaxGroupParty.AgMandatory = False
        Me.TxtSalesTaxGroupParty.AgMasterHelp = False
        Me.TxtSalesTaxGroupParty.AgNumberLeftPlaces = 8
        Me.TxtSalesTaxGroupParty.AgNumberNegetiveAllow = False
        Me.TxtSalesTaxGroupParty.AgNumberRightPlaces = 2
        Me.TxtSalesTaxGroupParty.AgPickFromLastValue = False
        Me.TxtSalesTaxGroupParty.AgRowFilter = ""
        Me.TxtSalesTaxGroupParty.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtSalesTaxGroupParty.AgSelectedValue = Nothing
        Me.TxtSalesTaxGroupParty.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtSalesTaxGroupParty.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtSalesTaxGroupParty.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtSalesTaxGroupParty.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtSalesTaxGroupParty.Location = New System.Drawing.Point(708, 54)
        Me.TxtSalesTaxGroupParty.MaxLength = 20
        Me.TxtSalesTaxGroupParty.Name = "TxtSalesTaxGroupParty"
        Me.TxtSalesTaxGroupParty.Size = New System.Drawing.Size(188, 18)
        Me.TxtSalesTaxGroupParty.TabIndex = 8
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.BackColor = System.Drawing.Color.Transparent
        Me.Label27.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.Location = New System.Drawing.Point(586, 54)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(104, 16)
        Me.Label27.TabIndex = 717
        Me.Label27.Text = "Sales Tax Group"
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
        Me.TxtRemarks.Location = New System.Drawing.Point(75, 434)
        Me.TxtRemarks.MaxLength = 255
        Me.TxtRemarks.Name = "TxtRemarks"
        Me.TxtRemarks.Size = New System.Drawing.Size(421, 18)
        Me.TxtRemarks.TabIndex = 4
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(2, 435)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(60, 16)
        Me.Label30.TabIndex = 723
        Me.Label30.Text = "Remarks"
        '
        'TxtReferenceNo
        '
        Me.TxtReferenceNo.AgAllowUserToEnableMasterHelp = False
        Me.TxtReferenceNo.AgLastValueTag = Nothing
        Me.TxtReferenceNo.AgLastValueText = Nothing
        Me.TxtReferenceNo.AgMandatory = False
        Me.TxtReferenceNo.AgMasterHelp = True
        Me.TxtReferenceNo.AgNumberLeftPlaces = 8
        Me.TxtReferenceNo.AgNumberNegetiveAllow = False
        Me.TxtReferenceNo.AgNumberRightPlaces = 2
        Me.TxtReferenceNo.AgPickFromLastValue = False
        Me.TxtReferenceNo.AgRowFilter = ""
        Me.TxtReferenceNo.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtReferenceNo.AgSelectedValue = Nothing
        Me.TxtReferenceNo.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtReferenceNo.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtReferenceNo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtReferenceNo.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtReferenceNo.Location = New System.Drawing.Point(374, 33)
        Me.TxtReferenceNo.MaxLength = 20
        Me.TxtReferenceNo.Name = "TxtReferenceNo"
        Me.TxtReferenceNo.Size = New System.Drawing.Size(195, 18)
        Me.TxtReferenceNo.TabIndex = 3
        '
        'LblReferenceNo
        '
        Me.LblReferenceNo.AutoSize = True
        Me.LblReferenceNo.BackColor = System.Drawing.Color.Transparent
        Me.LblReferenceNo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblReferenceNo.Location = New System.Drawing.Point(266, 33)
        Me.LblReferenceNo.Name = "LblReferenceNo"
        Me.LblReferenceNo.Size = New System.Drawing.Size(71, 16)
        Me.LblReferenceNo.TabIndex = 731
        Me.LblReferenceNo.Text = "Invoice No."
        '
        'LblVendorDocNo
        '
        Me.LblVendorDocNo.AutoSize = True
        Me.LblVendorDocNo.BackColor = System.Drawing.Color.Transparent
        Me.LblVendorDocNo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblVendorDocNo.Location = New System.Drawing.Point(586, 14)
        Me.LblVendorDocNo.Name = "LblVendorDocNo"
        Me.LblVendorDocNo.Size = New System.Drawing.Size(106, 16)
        Me.LblVendorDocNo.TabIndex = 706
        Me.LblVendorDocNo.Text = "Supplier Doc No."
        '
        'TxtVendorDocNo
        '
        Me.TxtVendorDocNo.AgAllowUserToEnableMasterHelp = False
        Me.TxtVendorDocNo.AgLastValueTag = Nothing
        Me.TxtVendorDocNo.AgLastValueText = Nothing
        Me.TxtVendorDocNo.AgMandatory = False
        Me.TxtVendorDocNo.AgMasterHelp = True
        Me.TxtVendorDocNo.AgNumberLeftPlaces = 8
        Me.TxtVendorDocNo.AgNumberNegetiveAllow = False
        Me.TxtVendorDocNo.AgNumberRightPlaces = 2
        Me.TxtVendorDocNo.AgPickFromLastValue = False
        Me.TxtVendorDocNo.AgRowFilter = ""
        Me.TxtVendorDocNo.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtVendorDocNo.AgSelectedValue = Nothing
        Me.TxtVendorDocNo.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtVendorDocNo.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtVendorDocNo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtVendorDocNo.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtVendorDocNo.Location = New System.Drawing.Point(708, 14)
        Me.TxtVendorDocNo.MaxLength = 20
        Me.TxtVendorDocNo.Name = "TxtVendorDocNo"
        Me.TxtVendorDocNo.Size = New System.Drawing.Size(188, 18)
        Me.TxtVendorDocNo.TabIndex = 6
        '
        'LvlVendorDocDate
        '
        Me.LvlVendorDocDate.AutoSize = True
        Me.LvlVendorDocDate.BackColor = System.Drawing.Color.Transparent
        Me.LvlVendorDocDate.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LvlVendorDocDate.Location = New System.Drawing.Point(586, 34)
        Me.LvlVendorDocDate.Name = "LvlVendorDocDate"
        Me.LvlVendorDocDate.Size = New System.Drawing.Size(103, 16)
        Me.LvlVendorDocDate.TabIndex = 708
        Me.LvlVendorDocDate.Text = "Supplier Doc Dt."
        '
        'TxtVendorDocDate
        '
        Me.TxtVendorDocDate.AgAllowUserToEnableMasterHelp = False
        Me.TxtVendorDocDate.AgLastValueTag = Nothing
        Me.TxtVendorDocDate.AgLastValueText = Nothing
        Me.TxtVendorDocDate.AgMandatory = False
        Me.TxtVendorDocDate.AgMasterHelp = True
        Me.TxtVendorDocDate.AgNumberLeftPlaces = 8
        Me.TxtVendorDocDate.AgNumberNegetiveAllow = False
        Me.TxtVendorDocDate.AgNumberRightPlaces = 2
        Me.TxtVendorDocDate.AgPickFromLastValue = False
        Me.TxtVendorDocDate.AgRowFilter = ""
        Me.TxtVendorDocDate.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtVendorDocDate.AgSelectedValue = Nothing
        Me.TxtVendorDocDate.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtVendorDocDate.AgValueType = AgControls.AgTextBox.TxtValueType.Date_Value
        Me.TxtVendorDocDate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtVendorDocDate.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtVendorDocDate.Location = New System.Drawing.Point(708, 34)
        Me.TxtVendorDocDate.MaxLength = 20
        Me.TxtVendorDocDate.Name = "TxtVendorDocDate"
        Me.TxtVendorDocDate.Size = New System.Drawing.Size(188, 18)
        Me.TxtVendorDocDate.TabIndex = 7
        '
        'LblCurrency
        '
        Me.LblCurrency.AutoSize = True
        Me.LblCurrency.BackColor = System.Drawing.Color.Transparent
        Me.LblCurrency.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCurrency.Location = New System.Drawing.Point(312, 414)
        Me.LblCurrency.Name = "LblCurrency"
        Me.LblCurrency.Size = New System.Drawing.Size(60, 16)
        Me.LblCurrency.TabIndex = 735
        Me.LblCurrency.Text = "Currency"
        '
        'TxtCurrency
        '
        Me.TxtCurrency.AgAllowUserToEnableMasterHelp = False
        Me.TxtCurrency.AgLastValueTag = Nothing
        Me.TxtCurrency.AgLastValueText = Nothing
        Me.TxtCurrency.AgMandatory = False
        Me.TxtCurrency.AgMasterHelp = False
        Me.TxtCurrency.AgNumberLeftPlaces = 8
        Me.TxtCurrency.AgNumberNegetiveAllow = False
        Me.TxtCurrency.AgNumberRightPlaces = 2
        Me.TxtCurrency.AgPickFromLastValue = False
        Me.TxtCurrency.AgRowFilter = ""
        Me.TxtCurrency.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtCurrency.AgSelectedValue = Nothing
        Me.TxtCurrency.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtCurrency.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtCurrency.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtCurrency.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCurrency.Location = New System.Drawing.Point(376, 414)
        Me.TxtCurrency.MaxLength = 20
        Me.TxtCurrency.Name = "TxtCurrency"
        Me.TxtCurrency.Size = New System.Drawing.Size(120, 18)
        Me.TxtCurrency.TabIndex = 3
        '
        'LinkLabel1
        '
        Me.LinkLabel1.BackColor = System.Drawing.Color.SteelBlue
        Me.LinkLabel1.DisabledLinkColor = System.Drawing.Color.White
        Me.LinkLabel1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel1.LinkColor = System.Drawing.Color.White
        Me.LinkLabel1.Location = New System.Drawing.Point(4, 181)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(230, 20)
        Me.LinkLabel1.TabIndex = 739
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "Purchase Invoice For Following Items"
        Me.LinkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'PnlCalcGrid
        '
        Me.PnlCalcGrid.Location = New System.Drawing.Point(605, 415)
        Me.PnlCalcGrid.Name = "PnlCalcGrid"
        Me.PnlCalcGrid.Size = New System.Drawing.Size(375, 160)
        Me.PnlCalcGrid.TabIndex = 6
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(356, 39)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(10, 7)
        Me.Label1.TabIndex = 737
        Me.Label1.Text = "Ä"
        '
        'RbtInvoiceDirect
        '
        Me.RbtInvoiceDirect.AutoSize = True
        Me.RbtInvoiceDirect.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RbtInvoiceDirect.Location = New System.Drawing.Point(8, 7)
        Me.RbtInvoiceDirect.Name = "RbtInvoiceDirect"
        Me.RbtInvoiceDirect.Size = New System.Drawing.Size(117, 17)
        Me.RbtInvoiceDirect.TabIndex = 0
        Me.RbtInvoiceDirect.TabStop = True
        Me.RbtInvoiceDirect.Text = "Invoice Direct"
        Me.RbtInvoiceDirect.UseVisualStyleBackColor = True
        '
        'RbtInvoiceForChallan
        '
        Me.RbtInvoiceForChallan.AutoSize = True
        Me.RbtInvoiceForChallan.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RbtInvoiceForChallan.Location = New System.Drawing.Point(127, 7)
        Me.RbtInvoiceForChallan.Name = "RbtInvoiceForChallan"
        Me.RbtInvoiceForChallan.Size = New System.Drawing.Size(152, 17)
        Me.RbtInvoiceForChallan.TabIndex = 1
        Me.RbtInvoiceForChallan.TabStop = True
        Me.RbtInvoiceForChallan.Text = "Invoice For Challan"
        Me.RbtInvoiceForChallan.UseVisualStyleBackColor = True
        '
        'GrpDirectInvoice
        '
        Me.GrpDirectInvoice.BackColor = System.Drawing.Color.Transparent
        Me.GrpDirectInvoice.Controls.Add(Me.RbtInvoiceDirect)
        Me.GrpDirectInvoice.Controls.Add(Me.RbtInvoiceForChallan)
        Me.GrpDirectInvoice.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GrpDirectInvoice.Location = New System.Drawing.Point(240, 174)
        Me.GrpDirectInvoice.Name = "GrpDirectInvoice"
        Me.GrpDirectInvoice.Size = New System.Drawing.Size(284, 26)
        Me.GrpDirectInvoice.TabIndex = 1
        Me.GrpDirectInvoice.TabStop = False
        '
        'BtnFillPurchChallan
        '
        Me.BtnFillPurchChallan.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnFillPurchChallan.Font = New System.Drawing.Font("Lucida Console", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnFillPurchChallan.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnFillPurchChallan.Location = New System.Drawing.Point(534, 179)
        Me.BtnFillPurchChallan.Margin = New System.Windows.Forms.Padding(0)
        Me.BtnFillPurchChallan.Name = "BtnFillPurchChallan"
        Me.BtnFillPurchChallan.Size = New System.Drawing.Size(35, 20)
        Me.BtnFillPurchChallan.TabIndex = 2
        Me.BtnFillPurchChallan.Text = "..."
        Me.BtnFillPurchChallan.UseVisualStyleBackColor = True
        '
        'PnlCustomGrid
        '
        Me.PnlCustomGrid.Location = New System.Drawing.Point(4, 455)
        Me.PnlCustomGrid.Name = "PnlCustomGrid"
        Me.PnlCustomGrid.Size = New System.Drawing.Size(492, 120)
        Me.PnlCustomGrid.TabIndex = 5
        '
        'TxtCustomFields
        '
        Me.TxtCustomFields.AgAllowUserToEnableMasterHelp = False
        Me.TxtCustomFields.AgLastValueTag = Nothing
        Me.TxtCustomFields.AgLastValueText = Nothing
        Me.TxtCustomFields.AgMandatory = False
        Me.TxtCustomFields.AgMasterHelp = False
        Me.TxtCustomFields.AgNumberLeftPlaces = 8
        Me.TxtCustomFields.AgNumberNegetiveAllow = False
        Me.TxtCustomFields.AgNumberRightPlaces = 2
        Me.TxtCustomFields.AgPickFromLastValue = False
        Me.TxtCustomFields.AgRowFilter = ""
        Me.TxtCustomFields.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtCustomFields.AgSelectedValue = Nothing
        Me.TxtCustomFields.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtCustomFields.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtCustomFields.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtCustomFields.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCustomFields.Location = New System.Drawing.Point(522, 587)
        Me.TxtCustomFields.MaxLength = 20
        Me.TxtCustomFields.Name = "TxtCustomFields"
        Me.TxtCustomFields.Size = New System.Drawing.Size(72, 18)
        Me.TxtCustomFields.TabIndex = 1012
        Me.TxtCustomFields.Text = "AgTextBox1"
        Me.TxtCustomFields.Visible = False
        '
        'TxtGodown
        '
        Me.TxtGodown.AgAllowUserToEnableMasterHelp = False
        Me.TxtGodown.AgLastValueTag = Nothing
        Me.TxtGodown.AgLastValueText = Nothing
        Me.TxtGodown.AgMandatory = False
        Me.TxtGodown.AgMasterHelp = False
        Me.TxtGodown.AgNumberLeftPlaces = 8
        Me.TxtGodown.AgNumberNegetiveAllow = False
        Me.TxtGodown.AgNumberRightPlaces = 2
        Me.TxtGodown.AgPickFromLastValue = False
        Me.TxtGodown.AgRowFilter = ""
        Me.TxtGodown.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtGodown.AgSelectedValue = Nothing
        Me.TxtGodown.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtGodown.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtGodown.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtGodown.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtGodown.Location = New System.Drawing.Point(75, 414)
        Me.TxtGodown.MaxLength = 0
        Me.TxtGodown.Name = "TxtGodown"
        Me.TxtGodown.Size = New System.Drawing.Size(229, 18)
        Me.TxtGodown.TabIndex = 2
        '
        'LblGodown
        '
        Me.LblGodown.AutoSize = True
        Me.LblGodown.BackColor = System.Drawing.Color.Transparent
        Me.LblGodown.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblGodown.Location = New System.Drawing.Point(2, 414)
        Me.LblGodown.Name = "LblGodown"
        Me.LblGodown.Size = New System.Drawing.Size(55, 16)
        Me.LblGodown.TabIndex = 742
        Me.LblGodown.Text = "Godown"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(141, 80)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(10, 7)
        Me.Label5.TabIndex = 3006
        Me.Label5.Text = "Ä"
        '
        'TxtBillToParty
        '
        Me.TxtBillToParty.AgAllowUserToEnableMasterHelp = False
        Me.TxtBillToParty.AgLastValueTag = Nothing
        Me.TxtBillToParty.AgLastValueText = Nothing
        Me.TxtBillToParty.AgMandatory = True
        Me.TxtBillToParty.AgMasterHelp = False
        Me.TxtBillToParty.AgNumberLeftPlaces = 8
        Me.TxtBillToParty.AgNumberNegetiveAllow = False
        Me.TxtBillToParty.AgNumberRightPlaces = 2
        Me.TxtBillToParty.AgPickFromLastValue = False
        Me.TxtBillToParty.AgRowFilter = ""
        Me.TxtBillToParty.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtBillToParty.AgSelectedValue = Nothing
        Me.TxtBillToParty.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtBillToParty.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtBillToParty.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtBillToParty.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtBillToParty.Location = New System.Drawing.Point(160, 73)
        Me.TxtBillToParty.MaxLength = 0
        Me.TxtBillToParty.Name = "TxtBillToParty"
        Me.TxtBillToParty.Size = New System.Drawing.Size(409, 18)
        Me.TxtBillToParty.TabIndex = 5
        '
        'LblPostToAc
        '
        Me.LblPostToAc.AutoSize = True
        Me.LblPostToAc.BackColor = System.Drawing.Color.Transparent
        Me.LblPostToAc.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblPostToAc.Location = New System.Drawing.Point(40, 74)
        Me.LblPostToAc.Name = "LblPostToAc"
        Me.LblPostToAc.Size = New System.Drawing.Size(73, 16)
        Me.LblPostToAc.TabIndex = 3005
        Me.LblPostToAc.Text = "Post to A/c"
        '
        'BtnFillPartyDetail
        '
        Me.BtnFillPartyDetail.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnFillPartyDetail.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnFillPartyDetail.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnFillPartyDetail.Location = New System.Drawing.Point(543, 53)
        Me.BtnFillPartyDetail.Margin = New System.Windows.Forms.Padding(0)
        Me.BtnFillPartyDetail.Name = "BtnFillPartyDetail"
        Me.BtnFillPartyDetail.Size = New System.Drawing.Size(26, 20)
        Me.BtnFillPartyDetail.TabIndex = 3007
        Me.BtnFillPartyDetail.TabStop = False
        Me.BtnFillPartyDetail.Text = "F"
        Me.BtnFillPartyDetail.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnFillPartyDetail.UseVisualStyleBackColor = True
        '
        'TxtNature
        '
        Me.TxtNature.AgAllowUserToEnableMasterHelp = False
        Me.TxtNature.AgLastValueTag = Nothing
        Me.TxtNature.AgLastValueText = Nothing
        Me.TxtNature.AgMandatory = True
        Me.TxtNature.AgMasterHelp = False
        Me.TxtNature.AgNumberLeftPlaces = 0
        Me.TxtNature.AgNumberNegetiveAllow = False
        Me.TxtNature.AgNumberRightPlaces = 0
        Me.TxtNature.AgPickFromLastValue = False
        Me.TxtNature.AgRowFilter = ""
        Me.TxtNature.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtNature.AgSelectedValue = Nothing
        Me.TxtNature.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtNature.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtNature.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtNature.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtNature.Location = New System.Drawing.Point(658, 180)
        Me.TxtNature.MaxLength = 20
        Me.TxtNature.Name = "TxtNature"
        Me.TxtNature.Size = New System.Drawing.Size(81, 18)
        Me.TxtNature.TabIndex = 1208
        Me.TxtNature.Text = "TxtNature"
        Me.TxtNature.Visible = False
        '
        'TxtProcess
        '
        Me.TxtProcess.AgAllowUserToEnableMasterHelp = False
        Me.TxtProcess.AgLastValueTag = Nothing
        Me.TxtProcess.AgLastValueText = ""
        Me.TxtProcess.AgMandatory = False
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
        Me.TxtProcess.Location = New System.Drawing.Point(720, 17)
        Me.TxtProcess.MaxLength = 0
        Me.TxtProcess.Name = "TxtProcess"
        Me.TxtProcess.Size = New System.Drawing.Size(188, 18)
        Me.TxtProcess.TabIndex = 9
        '
        'LblProcess
        '
        Me.LblProcess.AutoSize = True
        Me.LblProcess.BackColor = System.Drawing.Color.Transparent
        Me.LblProcess.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblProcess.Location = New System.Drawing.Point(598, 17)
        Me.LblProcess.Name = "LblProcess"
        Me.LblProcess.Size = New System.Drawing.Size(56, 16)
        Me.LblProcess.TabIndex = 3009
        Me.LblProcess.Text = "Process"
        '
        'TP2
        '
        Me.TP2.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.TP2.Controls.Add(Me.TxtTransporter)
        Me.TP2.Controls.Add(Me.LblTransporter)
        Me.TP2.Controls.Add(Me.TxtProcess)
        Me.TP2.Controls.Add(Me.LblProcess)
        Me.TP2.Location = New System.Drawing.Point(4, 22)
        Me.TP2.Name = "TP2"
        Me.TP2.Padding = New System.Windows.Forms.Padding(3)
        Me.TP2.Size = New System.Drawing.Size(984, 109)
        Me.TP2.TabIndex = 1
        Me.TP2.Text = "TabPage1"
        '
        'TxtTransporter
        '
        Me.TxtTransporter.AgAllowUserToEnableMasterHelp = False
        Me.TxtTransporter.AgLastValueTag = Nothing
        Me.TxtTransporter.AgLastValueText = ""
        Me.TxtTransporter.AgMandatory = False
        Me.TxtTransporter.AgMasterHelp = False
        Me.TxtTransporter.AgNumberLeftPlaces = 8
        Me.TxtTransporter.AgNumberNegetiveAllow = False
        Me.TxtTransporter.AgNumberRightPlaces = 2
        Me.TxtTransporter.AgPickFromLastValue = False
        Me.TxtTransporter.AgRowFilter = ""
        Me.TxtTransporter.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtTransporter.AgSelectedValue = Nothing
        Me.TxtTransporter.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtTransporter.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtTransporter.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtTransporter.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtTransporter.Location = New System.Drawing.Point(120, 17)
        Me.TxtTransporter.MaxLength = 100
        Me.TxtTransporter.Name = "TxtTransporter"
        Me.TxtTransporter.Size = New System.Drawing.Size(228, 18)
        Me.TxtTransporter.TabIndex = 3010
        '
        'LblTransporter
        '
        Me.LblTransporter.AutoSize = True
        Me.LblTransporter.BackColor = System.Drawing.Color.Transparent
        Me.LblTransporter.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTransporter.Location = New System.Drawing.Point(38, 17)
        Me.LblTransporter.Name = "LblTransporter"
        Me.LblTransporter.Size = New System.Drawing.Size(73, 16)
        Me.LblTransporter.TabIndex = 3011
        Me.LblTransporter.Text = "Transporter"
        '
        'TxtAgent
        '
        Me.TxtAgent.AgAllowUserToEnableMasterHelp = False
        Me.TxtAgent.AgLastValueTag = Nothing
        Me.TxtAgent.AgLastValueText = Nothing
        Me.TxtAgent.AgMandatory = False
        Me.TxtAgent.AgMasterHelp = False
        Me.TxtAgent.AgNumberLeftPlaces = 8
        Me.TxtAgent.AgNumberNegetiveAllow = False
        Me.TxtAgent.AgNumberRightPlaces = 2
        Me.TxtAgent.AgPickFromLastValue = False
        Me.TxtAgent.AgRowFilter = ""
        Me.TxtAgent.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtAgent.AgSelectedValue = Nothing
        Me.TxtAgent.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtAgent.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtAgent.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtAgent.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtAgent.Location = New System.Drawing.Point(708, 74)
        Me.TxtAgent.MaxLength = 20
        Me.TxtAgent.Name = "TxtAgent"
        Me.TxtAgent.Size = New System.Drawing.Size(188, 18)
        Me.TxtAgent.TabIndex = 3008
        '
        'LblAgent
        '
        Me.LblAgent.AutoSize = True
        Me.LblAgent.BackColor = System.Drawing.Color.Transparent
        Me.LblAgent.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblAgent.Location = New System.Drawing.Point(586, 74)
        Me.LblAgent.Name = "LblAgent"
        Me.LblAgent.Size = New System.Drawing.Size(42, 16)
        Me.LblAgent.TabIndex = 3009
        Me.LblAgent.Text = "Agent"
        '
        'FrmPurchInvoice
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.BackColor = System.Drawing.SystemColors.ButtonShadow
        Me.ClientSize = New System.Drawing.Size(984, 622)
        Me.Controls.Add(Me.TxtNature)
        Me.Controls.Add(Me.TxtCustomFields)
        Me.Controls.Add(Me.PnlCustomGrid)
        Me.Controls.Add(Me.BtnFillPurchChallan)
        Me.Controls.Add(Me.GrpDirectInvoice)
        Me.Controls.Add(Me.PnlCalcGrid)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Pnl1)
        Me.Controls.Add(Me.TxtGodown)
        Me.Controls.Add(Me.TxtRemarks)
        Me.Controls.Add(Me.Label30)
        Me.Controls.Add(Me.LblGodown)
        Me.Controls.Add(Me.TxtCurrency)
        Me.Controls.Add(Me.LblCurrency)
        Me.Name = "FrmPurchInvoice"
        Me.Text = "Purchase Invoice"
        Me.Controls.SetChildIndex(Me.LblCurrency, 0)
        Me.Controls.SetChildIndex(Me.TxtCurrency, 0)
        Me.Controls.SetChildIndex(Me.LblGodown, 0)
        Me.Controls.SetChildIndex(Me.Label30, 0)
        Me.Controls.SetChildIndex(Me.TxtRemarks, 0)
        Me.Controls.SetChildIndex(Me.TxtGodown, 0)
        Me.Controls.SetChildIndex(Me.Pnl1, 0)
        Me.Controls.SetChildIndex(Me.Panel1, 0)
        Me.Controls.SetChildIndex(Me.LinkLabel1, 0)
        Me.Controls.SetChildIndex(Me.PnlCalcGrid, 0)
        Me.Controls.SetChildIndex(Me.GrpDirectInvoice, 0)
        Me.Controls.SetChildIndex(Me.BtnFillPurchChallan, 0)
        Me.Controls.SetChildIndex(Me.PnlCustomGrid, 0)
        Me.Controls.SetChildIndex(Me.TxtCustomFields, 0)
        Me.Controls.SetChildIndex(Me.TabControl1, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.GrpUP, 0)
        Me.Controls.SetChildIndex(Me.GBoxEntryType, 0)
        Me.Controls.SetChildIndex(Me.GBoxApprove, 0)
        Me.Controls.SetChildIndex(Me.GBoxMoveToLog, 0)
        Me.Controls.SetChildIndex(Me.GroupBox2, 0)
        Me.Controls.SetChildIndex(Me.GBoxDivision, 0)
        Me.Controls.SetChildIndex(Me.Topctrl1, 0)
        Me.Controls.SetChildIndex(Me.TxtNature, 0)
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
        Me.GrpDirectInvoice.ResumeLayout(False)
        Me.GrpDirectInvoice.PerformLayout()
        Me.TP2.ResumeLayout(False)
        Me.TP2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Protected WithEvents LblVendor As System.Windows.Forms.Label
    Protected WithEvents TxtVendor As AgControls.AgTextBox
    Protected WithEvents Label4 As System.Windows.Forms.Label
    Protected WithEvents Panel1 As System.Windows.Forms.Panel
    Protected WithEvents LblTotalQty As System.Windows.Forms.Label
    Protected WithEvents LblTotalQtyText As System.Windows.Forms.Label
    Protected WithEvents Pnl1 As System.Windows.Forms.Panel
    Protected WithEvents TxtStructure As AgControls.AgTextBox
    Protected WithEvents Label25 As System.Windows.Forms.Label
    Protected WithEvents TxtSalesTaxGroupParty As AgControls.AgTextBox
    Protected WithEvents Label27 As System.Windows.Forms.Label
    Protected WithEvents LblTotalAmount As System.Windows.Forms.Label
    Protected WithEvents LblTotalAmountText As System.Windows.Forms.Label
    Protected WithEvents TxtRemarks As AgControls.AgTextBox
    Protected WithEvents Label30 As System.Windows.Forms.Label
    Protected WithEvents LblTotalMeasure As System.Windows.Forms.Label
    Protected WithEvents LblTotalMeasureText As System.Windows.Forms.Label
    Protected WithEvents TxtReferenceNo As AgControls.AgTextBox
    Protected WithEvents LblReferenceNo As System.Windows.Forms.Label
    Protected WithEvents TxtCurrency As AgControls.AgTextBox
    Protected WithEvents LblCurrency As System.Windows.Forms.Label
    Protected WithEvents TxtVendorDocDate As AgControls.AgTextBox
    Protected WithEvents LvlVendorDocDate As System.Windows.Forms.Label
    Protected WithEvents TxtVendorDocNo As AgControls.AgTextBox
    Protected WithEvents LblVendorDocNo As System.Windows.Forms.Label
    Protected WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    Protected WithEvents PnlCalcGrid As System.Windows.Forms.Panel
    Protected WithEvents Label1 As System.Windows.Forms.Label
    Protected WithEvents RbtInvoiceDirect As System.Windows.Forms.RadioButton
    Protected WithEvents RbtInvoiceForChallan As System.Windows.Forms.RadioButton
    Protected WithEvents GrpDirectInvoice As System.Windows.Forms.GroupBox
    Protected WithEvents BtnFillPurchChallan As System.Windows.Forms.Button
    Protected WithEvents PnlCustomGrid As System.Windows.Forms.Panel
    Protected WithEvents TxtCustomFields As AgControls.AgTextBox
    Protected WithEvents LblTotalDeliveryMeasure As System.Windows.Forms.Label
    Protected WithEvents LblTotalDeliveryMeasureText As System.Windows.Forms.Label
    Protected WithEvents TxtGodown As AgControls.AgTextBox
    Protected WithEvents LblGodown As System.Windows.Forms.Label
    Protected WithEvents Label5 As System.Windows.Forms.Label
    Protected WithEvents TxtBillToParty As AgControls.AgTextBox
    Protected WithEvents LblPostToAc As System.Windows.Forms.Label
    Protected WithEvents BtnFillPartyDetail As System.Windows.Forms.Button
    Protected WithEvents TxtNature As AgControls.AgTextBox
#End Region

    Private Sub FrmPurchInvoice_BaseEvent_ApproveDeletion_InTrans(ByVal SearchCode As String, ByVal Conn As SQLiteConnection, ByVal Cmd As SQLiteCommand) Handles Me.BaseEvent_ApproveDeletion_InTrans
        mQry = " UPDATE PurchInvoiceDetail Set PurchChallan = NULL Where DocId = '" & mSearchCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        mQry = " Delete From PurchChallanDetail Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        mQry = " Delete From PurchChallan Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        mQry = " Delete From Stock Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        mQry = " Delete From Ledger Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
    End Sub

    Private Sub FrmQuality1_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "PurchInvoice"
        MainLineTableCsv = "PurchInvoiceDetail"
        LogTableName = "PurchInvoice_Log"
        LogLineTableCsv = "PurchInvoiceDetail_Log"

        AgL.GridDesign(Dgl1)
        AgL.AddAgDataGrid(AgCalcGrid1, PnlCalcGrid)

        AgCalcGrid1.AgLibVar = AgL

        AgL.AddAgDataGrid(AgCustomGrid1, PnlCustomGrid)

        AgCustomGrid1.AgLibVar = AgL
        AgCustomGrid1.SplitGrid = True
        AgCustomGrid1.MnuText = Me.Name
    End Sub

    Private Sub FrmQuality1_BaseFunction_FIniMast(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMast
        Dim mCondStr$

        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) &
                        " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " And H.Div_Code = '" & AgL.PubDivCode & "' "
        mCondStr = mCondStr & " And Vt.NCat In ('" & EntryNCat & "')"

        If IsApplyVTypePermission Then
            mCondStr = mCondStr & " And H.V_Type In (Select V_Type From User_VType_Permission VP Where VP.UserName = '" & AgL.PubUserName & "' And VP.Div_Code = '" & AgL.PubDivCode & "' And VP.Site_Code = '" & AgL.PubSiteCode & "') "
        End If

        mQry = "Select DocID As SearchCode " &
                " From PurchInvoice H " &
                " Left Join Voucher_Type Vt On H.V_Type = Vt.V_Type  " &
                " Where IfNull(IsDeleted,0)=0  " & mCondStr & "  Order By V_Date Desc "
        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub FrmSaleOrder_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mCondStr$

        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) &
                        " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " And H.Div_Code = '" & AgL.PubDivCode & "'"
        mCondStr = mCondStr & " And Vt.NCat In ('" & EntryNCat & "')"

        If IsApplyVTypePermission Then
            mCondStr = mCondStr & " And H.V_Type In (Select V_Type From User_VType_Permission VP Where VP.UserName = '" & AgL.PubUserName & "' And VP.Div_Code = '" & AgL.PubDivCode & "' And VP.Site_Code = '" & AgL.PubSiteCode & "') "
        End If

        AgL.PubFindQry = " SELECT H.DocID AS SearchCode, Vt.Description AS [Invoice_Type], H.V_Date AS Date, " &
                            " H.ReferenceNo AS [Manual_No], SGV.DispName As Vendor, H.SalesTaxGroupParty AS [Sales_Tax_Group_Party], H.VendorDocNo AS [Vendor_Doc_No],  " &
                            " H.VendorDocDate AS [Vendor_Doc_Date], H.Remarks, H.TotalQty AS [Total_Qty], " &
                            " H.TotalMeasure AS [Total_Measure], H.TotalAmount AS [Total_Amount],  " &
                            " H.EntryBy AS [Entry_By], H.EntryDate AS [Entry_Date], H.EntryType AS [Entry_Type] " &
                            " FROM PurchInvoice H " &
                            " LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type " &
                            " LEFT JOIN SubGroup SGV ON SGV.SubCode  = H.Vendor  " &
                            " Where IfNull(H.IsDeleted,0) = 0  " & mCondStr

        AgL.PubFindQryOrdBy = "[Entry Date]"
    End Sub

    Private Sub FrmSaleOrder_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl1, Col1Item_UID, 60, 0, Col1Item_UID, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_ItemUID")), Boolean), False)
            .AddAgTextColumn(Dgl1, Col1ItemCode, 100, 0, Col1ItemCode, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_ItemCode")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_ItemCode")), Boolean))
            .AddAgTextColumn(Dgl1, Col1Item, 200, 0, Col1Item, True, Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_ItemName")), Boolean))
            .AddAgTextColumn(Dgl1, Col1Dimension1, 100, 0, ClsMain.FGetDimension1Caption(), CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Dimension1")), Boolean), False)
            .AddAgTextColumn(Dgl1, Col1Dimension2, 100, 0, ClsMain.FGetDimension2Caption(), CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Dimension2")), Boolean), False)
            .AddAgTextColumn(Dgl1, Col1Specification, 100, 255, Col1Specification, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Specification")), Boolean), False, False)
            .AddAgTextColumn(Dgl1, Col1BaleNo, 50, 0, Col1BaleNo, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_BaleNo")), Boolean), False)
            .AddAgTextColumn(Dgl1, Col1LotNo, 50, 0, Col1LotNo, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_LotNo")), Boolean), False)
            .AddAgTextColumn(Dgl1, Col1PurchChallan, 70, 0, Col1PurchChallan, True, True)
            .AddAgTextColumn(Dgl1, Col1PurchChallanSr, 40, 5, Col1PurchChallanSr, False, True, False)
            .AddAgTextColumn(Dgl1, Col1BillingType, 50, 255, Col1BillingType, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_BillingType")), Boolean), False)
            .AddAgTextColumn(Dgl1, Col1DeliveryMeasure, 70, 50, Col1DeliveryMeasure, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_MeasureUnit")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_MeasureUnit")), Boolean), False)
            .AddAgNumberColumn(Dgl1, Col1DeliveryMeasureMultiplier, 100, 8, 4, False, Col1DeliveryMeasureMultiplier, False, True, True)
            .AddAgNumberColumn(Dgl1, Col1DeliveryMeasurePerPcs, 110, 8, 4, False, Col1DeliveryMeasurePerPcs, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_MeasurePerPcs")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_MeasurePerPcs")), Boolean), True)
            .AddAgNumberColumn(Dgl1, Col1TotalDocDeliveryMeasure, 70, 8, 3, False, Col1TotalDocDeliveryMeasure, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Measure")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_Measure")), Boolean), True)
            .AddAgNumberColumn(Dgl1, Col1TotalFreeDeliveryMeasure, 70, 8, 3, False, Col1TotalFreeDeliveryMeasure, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_FreeMeasure")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_Measure")), Boolean), True)
            .AddAgNumberColumn(Dgl1, Col1TotalRejDeliveryMeasure, 70, 8, 3, False, Col1TotalRejDeliveryMeasure, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_RejMeasure")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_Measure")), Boolean), True)
            .AddAgNumberColumn(Dgl1, Col1TotalDeliveryMeasure, 70, 8, 4, False, Col1TotalDeliveryMeasure, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Qty")), Boolean), True, True)
            .AddAgTextColumn(Dgl1, Col1DeliveryMeasureDecimalPlaces, 50, 0, Col1DeliveryMeasureDecimalPlaces, False, True, False)

            .AddAgNumberColumn(Dgl1, Col1DocQty, 70, 8, 4, False, Col1DocQty, True, False, True)
            .AddAgNumberColumn(Dgl1, Col1FreeQty, 60, 8, 3, False, Col1FreeQty, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_FreeQty")), Boolean), False, True)
            .AddAgNumberColumn(Dgl1, Col1RejQty, 70, 8, 4, False, Col1RejQty, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_RejQty")), Boolean), False, True)
            .AddAgNumberColumn(Dgl1, Col1Qty, 70, 8, 4, False, Col1Qty, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Qty")), Boolean), True, True)
            .AddAgTextColumn(Dgl1, Col1Unit, 50, 0, Col1Unit, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Unit")), Boolean), True)
            .AddAgTextColumn(Dgl1, Col1QtyDecimalPlaces, 50, 0, Col1QtyDecimalPlaces, False, True, False)

            .AddAgNumberColumn(Dgl1, Col1MeasurePerPcs, 70, 8, 3, False, Col1MeasurePerPcs, False, True, True)
            .AddAgNumberColumn(Dgl1, Col1PcsPerMeasure, 70, 8, 3, False, Col1PcsPerMeasure, False, True, True)
            .AddAgNumberColumn(Dgl1, Col1TotalDocMeasure, 70, 8, 3, False, Col1TotalDocMeasure, False, True, True)
            .AddAgNumberColumn(Dgl1, Col1TotalFreeMeasure, 70, 8, 3, False, Col1TotalFreeMeasure, False, True, True)
            .AddAgNumberColumn(Dgl1, Col1TotalRejMeasure, 70, 8, 3, False, Col1TotalRejMeasure, False, True, True)
            .AddAgNumberColumn(Dgl1, Col1TotalMeasure, 70, 8, 3, False, Col1TotalMeasure, False, True, True)
            .AddAgTextColumn(Dgl1, Col1MeasureUnit, 60, 0, Col1MeasureUnit, False, True)
            .AddAgTextColumn(Dgl1, Col1MeasureDecimalPlaces, 50, 0, Col1MeasureDecimalPlaces, False, True, False)

            .AddAgNumberColumn(Dgl1, Col1Rate, 80, 8, 3, False, Col1Rate, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Rate")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_Rate")), Boolean), True)
            .AddAgNumberColumn(Dgl1, Col1Amount, 100, 8, 2, False, Col1Amount, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Amount")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_Amount")), Boolean), True)
            .AddAgNumberColumn(Dgl1, Col1MRP, 80, 8, 2, False, Col1MRP, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_MRP")), Boolean), False, True)
            .AddAgNumberColumn(Dgl1, Col1SaleRate, 80, 8, 2, False, Col1SaleRate, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_SaleRate")), Boolean), False, True)
            .AddAgDateColumn(Dgl1, Col1ExpiryDate, 90, Col1ExpiryDate, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_ExpiryDate")), Boolean), False)
            .AddAgTextColumn(Dgl1, Col1Remark, 200, 255, Col1Remark, True, False)
            .AddAgTextColumn(Dgl1, Col1SalesTaxGroup, 60, 0, Col1SalesTaxGroup, True, False)
            .AddAgTextColumn(Dgl1, Col1Deal, 70, 255, Col1Deal, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Deal")), Boolean), False)
            .AddAgNumberColumn(Dgl1, Col1ProfitMarginPer, 100, 8, 2, False, Col1ProfitMarginPer, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_ProfitMarginPer")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_ProfitMarginPer")), Boolean), True)
            .AddAgTextColumn(Dgl1, Col1PurchIndent, 70, 255, Col1PurchIndent, False, False)
            .AddAgTextColumn(Dgl1, Col1PurchIndentSr, 40, 5, Col1PurchIndentSr, False, True, False)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 50

        AgCalcGrid1.Ini_Grid(LblV_Type.Tag, TxtV_Date.Text)

        AgCalcGrid1.AgFixedRows = 6

        If CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Measure")), Boolean) = False Then LblTotalDeliveryMeasure.Visible = False : LblTotalDeliveryMeasureText.Visible = False
        If CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Measure")), Boolean) = False Then LblTotalMeasure.Visible = False : LblTotalMeasureText.Visible = False


        Dgl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple

        AgCalcGrid1.AgLineGrid = Dgl1
        AgCalcGrid1.AgLineGridMandatoryColumn = Dgl1.Columns(Col1Item).Index
        AgCalcGrid1.AgLineGridGrossColumn = Dgl1.Columns(Col1Amount).Index
        AgCalcGrid1.AgLineGridPostingGroupSalesTaxProd = Dgl1.Columns(Col1SalesTaxGroup).Index
        AgCalcGrid1.AgPostingPartyAc = TxtVendor.AgSelectedValue

        AgCustomGrid1.Ini_Grid(mSearchCode)
        AgCustomGrid1.SplitGrid = False


        Dgl1.AgLastColumn = Dgl1.Columns(Col1Remark).Index
        AgCL.GridSetiingShowXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1, False)
        Dgl1.AgSkipReadOnlyColumns = True
        Dgl1.AllowUserToOrderColumns = True
    End Sub

    Private Sub FrmSaleOrder_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As SQLiteConnection, ByVal Cmd As SQLiteCommand) Handles Me.BaseEvent_Save_InTrans
        Dim I As Integer, mSr As Integer
        Dim mVendorDocDate As String
        Dim mExpiryDate As String
        Dim bSelectionQry$ = ""

        If BtnFillPartyDetail.Tag Is Nothing Then BtnFillPartyDetail.Tag = New FrmPurchPartyDetail

        If TxtVendorDocDate.Text <> "" Then
            mVendorDocDate = CDate(TxtVendorDocDate.Text).ToString("u")
        Else
            mVendorDocDate = ""
        End If

        mQry = " Update PurchInvoice " &
                " SET  " &
                " ReferenceNo = " & AgL.Chk_Text(TxtReferenceNo.Text) & ", " &
                " Vendor = " & AgL.Chk_Text(TxtVendor.AgSelectedValue) & ", " &
                " VendorName = " & AgL.Chk_Text(BtnFillPartyDetail.Tag.TxtVendorName.Text) & ", " &
                " VendorAdd1 = " & AgL.Chk_Text(BtnFillPartyDetail.Tag.TxtVendorAdd1.Text) & ", " &
                " VendorAdd2 = " & AgL.Chk_Text(BtnFillPartyDetail.Tag.TxtVendorAdd2.Text) & ", " &
                " VendorCity = " & AgL.Chk_Text(BtnFillPartyDetail.Tag.TxtVendorCity.Tag) & ", " &
                " VendorCityName = " & AgL.Chk_Text(BtnFillPartyDetail.Tag.TxtVendorCity.Text) & ", " &
                " VendorMobile = " & AgL.Chk_Text(BtnFillPartyDetail.Tag.TxtVendorMobile.Text) & ", " &
                " BillToParty = " & AgL.Chk_Text(TxtBillToParty.Tag) & ", " &
                " Currency = " & AgL.Chk_Text(TxtCurrency.AgSelectedValue) & ", " &
                " SalesTaxGroupParty = " & AgL.Chk_Text(TxtSalesTaxGroupParty.Text) & ", " &
                " Agent = " & AgL.Chk_Text(AgL.XNull(TxtAgent.Tag)) & ", " &
                " Transporter = " & AgL.Chk_Text(AgL.XNull(TxtTransporter.Tag)) & ", " &
                " Godown = " & AgL.Chk_Text(AgL.XNull(TxtGodown.Tag)) & ", " &
                " Structure = " & AgL.Chk_Text(TxtStructure.AgSelectedValue) & ", " &
                " CustomFields = " & AgL.Chk_Text(TxtCustomFields.AgSelectedValue) & ", " &
                " VendorDocNo = " & AgL.Chk_Text(TxtVendorDocNo.Text) & ", " &
                " VendorDocDate = " & AgL.Chk_Text(mVendorDocDate) & ", " &
                " Process = " & AgL.Chk_Text(TxtProcess.Tag) & ", " &
                " Remarks = " & AgL.Chk_Text(TxtRemarks.Text) & ", " &
                " TotalQty = " & Val(LblTotalQty.Text) & ", " &
                " TotalAmount = " & Val(LblTotalAmount.Text) & ", " &
                " TotalMeasure = " & Val(LblTotalMeasure.Text) & ", " &
                " TotalDeliveryMeasure = " & Val(LblTotalDeliveryMeasure.Text) & ", " &
                " " & AgCalcGrid1.FFooterTableUpdateStr() & " " &
                " " & AgCustomGrid1.FFooterTableUpdateStr() & " " &
                " Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        'mQry = "Delete From PurchInvoiceDetail Where DocId = '" & SearchCode & "'"
        'AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mSr = AgL.VNull(AgL.Dman_Execute("Select Max(Sr) From PurchInvoiceDetail  Where DocID = '" & mSearchCode & "'", AgL.GcnRead).ExecuteScalar)
        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1Item, I).Value <> "" Then

                If Dgl1.Item(Col1ExpiryDate, I).Value <> "" Then
                    mExpiryDate = CDate(Dgl1.Item(Col1ExpiryDate, I).Value).ToString("u")
                Else
                    mExpiryDate = ""
                End If

                If Dgl1.Item(ColSNo, I).Tag Is Nothing And Dgl1.Rows(I).Visible = True Then
                    mSr += 1


                    If bSelectionQry <> "" Then bSelectionQry += " UNION ALL "
                    bSelectionQry += " Select " & AgL.Chk_Text(mSearchCode) & ", " & mSr & ", " &
                                        " " & AgL.Chk_Text(Dgl1.Item(Col1PurchChallan, I).Tag) & ", " &
                                        " " & AgL.Chk_Text(IIf(Val(Dgl1.Item(Col1PurchChallanSr, I).Value) = 0, "", Dgl1.Item(Col1PurchChallanSr, I).Value)) & ", " &
                                        " " & AgL.Chk_Text(mSearchCode) & ", " & mSr & ", " &
                                        " " & AgL.Chk_Text(Dgl1.Item(Col1Item_UID, I).Tag) & ", " &
                                        " " & AgL.Chk_Text(Dgl1.Item(Col1Item, I).Tag) & ", " &
                                        " " & AgL.Chk_Text(Dgl1.Item(Col1Dimension1, I).Tag) & ", " &
                                        " " & AgL.Chk_Text(Dgl1.Item(Col1Dimension2, I).Tag) & ", " &
                                        " " & AgL.Chk_Text(Dgl1.Item(Col1Specification, I).Value) & ", " &
                                        " " & AgL.Chk_Text(Dgl1.Item(Col1BaleNo, I).Value) & ", " &
                                        " " & AgL.Chk_Text(Dgl1.Item(Col1SalesTaxGroup, I).Tag) & ", " &
                                        " " & Val(Dgl1.Item(Col1ProfitMarginPer, I).Value) & ", " &
                                        " " & Val(Dgl1.Item(Col1DocQty, I).Value) & ", " &
                                        " " & Val(Dgl1.Item(Col1FreeQty, I).Value) & ", " &
                                        " " & Val(Dgl1.Item(Col1RejQty, I).Value) & ", " &
                                        " " & Val(Dgl1.Item(Col1Qty, I).Value) & ", " &
                                        " " & AgL.Chk_Text(Dgl1.Item(Col1Unit, I).Value) & ", " &
                                        " " & Val(Dgl1.Item(Col1MeasurePerPcs, I).Value) & ", " &
                                        " " & Val(Dgl1.Item(Col1PcsPerMeasure, I).Value) & ", " &
                                        " " & AgL.Chk_Text(Dgl1.Item(Col1MeasureUnit, I).Value) & ", " &
                                        " " & Val(Dgl1.Item(Col1TotalDocMeasure, I).Value) & ", " &
                                        " " & Val(Dgl1.Item(Col1TotalFreeMeasure, I).Value) & ", " &
                                        " " & Val(Dgl1.Item(Col1TotalRejMeasure, I).Value) & ", " &
                                        " " & Val(Dgl1.Item(Col1TotalMeasure, I).Value) & ", " &
                                        " " & Val(Dgl1.Item(Col1Rate, I).Value) & ", " &
                                        " " & Val(Dgl1.Item(Col1Amount, I).Value) & ", " &
                                        " " & Val(Dgl1.Item(Col1SaleRate, I).Value) & ", " &
                                        " " & Val(Dgl1.Item(Col1MRP, I).Value) & ", " &
                                        " " & AgL.Chk_Text(Dgl1.Item(Col1Remark, I).Value) & ", " &
                                        " " & AgL.Chk_Text(Dgl1.Item(Col1LotNo, I).Value) & ", " &
                                        " " & AgL.Chk_Text(Dgl1.Item(Col1Deal, I).Value) & ", " &
                                        " " & AgL.Chk_Text(mExpiryDate) & ", " &
                                        " " & AgL.Chk_Text(Dgl1.Item(Col1BillingType, I).Value) & " , " &
                                        " " & AgL.Chk_Text(Dgl1.Item(Col1DeliveryMeasure, I).Value) & ", " &
                                        " " & Val(Dgl1.Item(Col1DeliveryMeasureMultiplier, I).Value) & ", " &
                                        " " & Val(Dgl1.Item(Col1DeliveryMeasurePerPcs, I).Value) & ", " &
                                        " " & Val(Dgl1.Item(Col1TotalDocDeliveryMeasure, I).Value) & ", " &
                                        " " & Val(Dgl1.Item(Col1TotalFreeDeliveryMeasure, I).Value) & ", " &
                                        " " & Val(Dgl1.Item(Col1TotalRejDeliveryMeasure, I).Value) & ", " &
                                        " " & Val(Dgl1.Item(Col1TotalDeliveryMeasure, I).Value) & ", " &
                                        " " & AgL.Chk_Text(Dgl1.Item(Col1PurchIndent, I).Value) & ", " &
                                        " " & AgCalcGrid1.FLineTableFieldValuesStr(I) & " "
                    Call FUpdateDeal(I, Conn, Cmd)
                Else
                    If Dgl1.Rows(I).Visible = True Then
                        'If Dgl1.Rows(I).DefaultCellStyle.BackColor <> AgTemplate.ClsMain.Colours.GridRow_Locked Then
                        mQry = "Update dbo.PurchInvoiceDetail " &
                                " SET Item = " & AgL.Chk_Text(Dgl1.Item(Col1Item, I).Tag) & ", " &
                                " Dimension1 = " & AgL.Chk_Text(Dgl1.Item(Col1Dimension1, I).Tag) & ", " &
                                " Dimension2 = " & AgL.Chk_Text(Dgl1.Item(Col1Dimension2, I).Tag) & ", " &
                                " Specification = " & AgL.Chk_Text(Dgl1.Item(Col1Specification, I).Value) & ", " &
                                " SalesTaxGroupItem = " & AgL.Chk_Text(Dgl1.Item(Col1SalesTaxGroup, I).Tag) & ", " &
                                " ProfitMarginPer = " & Val(Dgl1.Item(Col1ProfitMarginPer, I).Value) & ", " &
                                " DocQty = " & Val(Dgl1.Item(Col1DocQty, I).Value) & ", " &
                                " RejQty = " & Val(Dgl1.Item(Col1RejQty, I).Value) & ", " &
                                " 	FreeQty = " & Val(Dgl1.Item(Col1FreeQty, I).Value) & ", " &
                                " 	Qty = " & Val(Dgl1.Item(Col1Qty, I).Value) & ", " &
                                " 	Unit = " & AgL.Chk_Text(Dgl1.Item(Col1Unit, I).Value) & ", " &
                                " 	MeasurePerPcs = " & Val(Dgl1.Item(Col1MeasurePerPcs, I).Value) & ", " &
                                " 	MeasureUnit = " & AgL.Chk_Text(Dgl1.Item(Col1MeasureUnit, I).Value) & ", " &
                                " 	TotalDocMeasure = " & Val(Dgl1.Item(Col1TotalDocMeasure, I).Value) & ", " &
                                " 	TotalMeasure = " & Val(Dgl1.Item(Col1TotalMeasure, I).Value) & ", " &
                                " 	Rate = " & Val(Dgl1.Item(Col1Rate, I).Value) & ", " &
                                " 	Amount = " & Val(Dgl1.Item(Col1Amount, I).Value) & ", " &
                                " 	Sale_Rate = " & Val(Dgl1.Item(Col1SaleRate, I).Value) & ", " &
                                " 	MRP = " & Val(Dgl1.Item(Col1MRP, I).Value) & ", " &
                                " 	Remark = " & AgL.Chk_Text(Dgl1.Item(Col1Remark, I).Value) & ", " &
                                " 	LotNo = " & AgL.Chk_Text(Dgl1.Item(Col1LotNo, I).Value) & ", " &
                                " 	PurchIndent = " & AgL.Chk_Text(Dgl1.Item(Col1PurchIndent, I).Tag) & ", " &
                                " 	PurchIndentSr = " & AgL.Chk_Text(Dgl1.Item(Col1PurchIndentSr, I).Value) & ", " &
                                " 	PurchChallan = " & AgL.Chk_Text(Dgl1.Item(Col1PurchChallan, I).Tag) & ", " &
                                " 	PurchChallanSr = " & AgL.Chk_Text(IIf(Val(Dgl1.Item(Col1PurchChallanSr, I).Value) > 0, Dgl1.Item(Col1PurchChallanSr, I).Value, "")) & ", " &
                                " 	BillingType = " & AgL.Chk_Text(Dgl1.Item(Col1BillingType, I).Value) & ", " &
                                " 	BaleNo = " & AgL.Chk_Text(Dgl1.Item(Col1BaleNo, I).Value) & ", " &
                                " 	TotalRejMeasure = " & Val(Dgl1.Item(Col1TotalRejMeasure, I).Value) & ", " &
                                " 	Item_Uid = " & AgL.Chk_Text(Dgl1.Item(Col1Item_UID, I).Tag) & ", " &
                                " 	DeliveryMeasure = " & AgL.Chk_Text(Dgl1.Item(Col1DeliveryMeasure, I).Value) & ", " &
                                " 	DeliveryMeasureMultiplier = " & Val(Dgl1.Item(Col1DeliveryMeasureMultiplier, I).Value) & ", " &
                                " 	DeliveryMeasurePerPcs = " & Val(Dgl1.Item(Col1DeliveryMeasurePerPcs, I).Value) & ", " &
                                " 	PcsPerMeasure = " & Val(Dgl1.Item(Col1PcsPerMeasure, I).Value) & ", " &
                                " 	TotalDocDeliveryMeasure = " & Val(Dgl1.Item(Col1TotalDocDeliveryMeasure, I).Value) & ", " &
                                " 	TotalRejDeliveryMeasure = " & Val(Dgl1.Item(Col1TotalRejDeliveryMeasure, I).Value) & ", " &
                                " 	TotalDeliveryMeasure = " & Val(Dgl1.Item(Col1TotalDeliveryMeasure, I).Value) & ", " &
                                " 	ExpiryDate = " & AgL.Chk_Text(Dgl1.Item(Col1ExpiryDate, I).Value) & ", " &
                                " 	TotalFreeMeasure = " & Val(Dgl1.Item(Col1TotalFreeMeasure, I).Value) & ", " &
                                " 	TotalFreeDeliveryMeasure = " & Val(Dgl1.Item(Col1TotalFreeDeliveryMeasure, I).Value) & ", " &
                                " 	Deal = " & AgL.Chk_Text(Dgl1.Item(Col1Deal, I).Value) & ", " &
                                " " & AgCalcGrid1.FLineTableUpdateStr(I) & " " &
                                "   Where DocId = '" & mSearchCode & "' " &
                                "   And Sr = " & Dgl1.Item(ColSNo, I).Tag & " "
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                        'End If
                    Else
                        mQry = " Delete From PurchInvoiceDetail Where DocId = '" & mSearchCode & "' And Sr = " & Val(Dgl1.Item(ColSNo, I).Tag) & "  "
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                    End If
                End If
            End If
        Next

        mQry = "Insert Into PurchInvoiceDetail(DocId, Sr, PurchChallan, PurchChallanSr, PurchInvoice, PurchInvoiceSr, " &
                " Item_Uid, Item, Dimension1, Dimension2, Specification, BaleNo, SalesTaxGroupItem, " &
                " ProfitMarginPer, DocQty, FreeQty, RejQty, Qty, Unit, MeasurePerPcs, PcsPerMeasure, MeasureUnit, TotalDocMeasure, TotalFreeMeasure, TotalRejMeasure, " &
                " TotalMeasure, Rate, Amount, Sale_Rate, MRP, Remark, LotNo, Deal, ExpiryDate, BillingType, " &
                " DeliveryMeasure, DeliveryMeasureMultiplier, DeliveryMeasurePerPcs, TotalDocDeliveryMeasure, TotalFreeDeliveryMeasure, TotalRejDeliveryMeasure, " &
                " TotalDeliveryMeasure, PurchIndent, " & AgCalcGrid1.FLineTableFieldNameStr() & ") "
        mQry = mQry + bSelectionQry
        If bSelectionQry <> "" Then
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If
        Call FPostInPurchChallan(Conn, Cmd)


        Dim mNarration As String = "Being goods purchased from " & TxtVendor.Text & " Bill No. " & TxtVendorDocNo.Text & " Dated " & TxtVendorDocDate.Text
        Call AgTemplate.ClsMain.PostStructureLineToAccounts(AgCalcGrid1, mNarration, mSearchCode, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, TxtDivision.AgSelectedValue,
                                             TxtV_Type.AgSelectedValue, LblPrefix.Text, TxtV_No.Text, TxtReferenceNo.Text, TxtBillToParty.Tag, TxtV_Date.Text, Conn, Cmd)


        If AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Or AgL.StrCmp(AgL.PubUserName, "Sa") Then
            AgCL.GridSetiingWriteXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1)
        End If
    End Sub

    Private Sub FrmSaleOrder_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim I As Integer

        Dim DsTemp As DataSet

        mIsEntryLocked = False

        mQry = " Select H.*, Sg.Name || (Case When C.CityName Is Not Null Then ',' || C.CityName Else '' End) AS  VendorDispName, 
                 C1.Description As CurrencyDesc, Sg.Nature, 
                 G.Description As GodownDesc, 
                 Sg1.Name || (Case When C2.CityName Is Not Null Then ',' || C2.CityName Else '' End) AS  BillToPartyName,
                 Vt.Category As Voucher_Category, 
                 P.Description As ProcessDesc, Agent.Name As AgentName, Transporter.Name As TransporterName 
                 From (Select * From PurchInvoice Where DocID='" & SearchCode & "') H 
                 LEFT JOIN SubGroup Sg ON H.Vendor = Sg.SubCode 
                 LEFT JOIN City C On Sg.CityCode = C.CityCode  
                 Left Join Currency C1  On H.Currency = C1.Code 
                 LEFT JOIN SubGroup Sg1 On H.BillToParty = Sg1.SubCode 
                 LEFT JOIN City C2 On Sg1.CityCode = C2.CityCode  
                 Left Join Godown G  On H.Godown = G.Code  
                 Left Join SubGroup Agent On H.Agent = Agent.Subcode 
                 Left Join SubGroup Transporter On H.Transporter = Transporter.Subcode 
                 Left Join Voucher_Type Vt On H.V_Type = Vt.V_Type 
                 LEFT JOIN Process P On H.Process = P.NCat "
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                TxtStructure.AgSelectedValue = AgStructure.ClsMain.FGetStructureFromNCat(LblV_Type.Tag, AgL.GcnRead)

                If AgL.XNull(.Rows(0)("Structure")) <> "" Then
                    TxtStructure.Tag = AgL.XNull(.Rows(0)("Structure"))
                End If
                AgCalcGrid1.FrmType = Me.FrmType
                AgCalcGrid1.AgStructure = TxtStructure.Tag
                AgCalcGrid1.AgVoucherCategory = "PURCH"

                If AgL.XNull(.Rows(0)("CustomFields")) <> "" Then
                    TxtCustomFields.AgSelectedValue = AgL.XNull(.Rows(0)("CustomFields"))
                End If
                AgCustomGrid1.FrmType = Me.FrmType
                AgCustomGrid1.AgCustom = TxtCustomFields.AgSelectedValue


                IniGrid()

                TxtReferenceNo.Text = AgL.XNull(.Rows(0)("ReferenceNo"))
                TxtVendor.Tag = AgL.XNull(.Rows(0)("Vendor"))
                TxtVendor.Text = AgL.XNull(.Rows(0)("VendorDispName"))

                TxtProcess.Tag = AgL.XNull(.Rows(0)("Process"))
                TxtProcess.Text = AgL.XNull(.Rows(0)("ProcessDesc"))

                TxtNature.Text = AgL.XNull(.Rows(0)("Sg.Nature"))

                TxtBillToParty.Tag = AgL.XNull(.Rows(0)("BillToParty"))
                TxtBillToParty.Text = AgL.XNull(.Rows(0)("BillToPartyName"))

                TxtCurrency.Tag = AgL.XNull(.Rows(0)("Currency"))
                TxtCurrency.Text = AgL.XNull(.Rows(0)("CurrencyDesc"))
                TxtVendorDocNo.Text = AgL.XNull(.Rows(0)("VendorDocNo"))
                TxtVendorDocDate.Text = AgL.RetDate(AgL.XNull(.Rows(0)("VendorDocDate")))

                TxtGodown.Tag = AgL.XNull(.Rows(0)("Godown"))
                TxtGodown.Text = AgL.XNull(.Rows(0)("GodownDesc"))
                TxtAgent.Tag = AgL.XNull(.Rows(0)("Agent"))
                TxtAgent.Text = AgL.XNull(.Rows(0)("AgentName"))
                TxtTransporter.Tag = AgL.XNull(.Rows(0)("Transporter"))
                TxtTransporter.Text = AgL.XNull(.Rows(0)("TransporterName"))


                Dim FrmObj As New FrmPurchPartyDetail
                FrmObj.TxtVendorMobile.Text = AgL.XNull(.Rows(0)("VendorMobile"))
                FrmObj.TxtVendorName.Text = AgL.XNull(.Rows(0)("VendorName"))
                FrmObj.TxtVendorAdd1.Text = AgL.XNull(.Rows(0)("VendorAdd1"))
                FrmObj.TxtVendorAdd2.Text = AgL.XNull(.Rows(0)("VendorAdd2"))
                FrmObj.TxtVendorCity.Tag = AgL.XNull(.Rows(0)("VendorCity"))
                FrmObj.TxtVendorCity.Text = AgL.XNull(.Rows(0)("VendorCityName"))

                BtnFillPartyDetail.Tag = FrmObj

                TxtSalesTaxGroupParty.Tag = AgL.XNull(.Rows(0)("SalesTaxGroupParty"))
                TxtSalesTaxGroupParty.Text = AgL.XNull(.Rows(0)("SalesTaxGroupParty"))
                AgCalcGrid1.AgPostingGroupSalesTaxParty = TxtSalesTaxGroupParty.AgSelectedValue
                AgCalcGrid1.AgPostingGroupSalesTaxItem = AgL.XNull(AgL.PubDtEnviro.Rows(0)("DefaultSalesTaxGroupItem"))

                TxtRemarks.Text = AgL.XNull(.Rows(0)("Remarks"))

                AgCalcGrid1.FMoveRecFooterTable(DsTemp.Tables(0), LblV_Type.Tag, TxtV_Date.Text)

                AgCustomGrid1.FMoveRecFooterTable(DsTemp.Tables(0))


                LblTotalQty.Text = "0"
                LblTotalAmount.Text = "0"
                LblTotalMeasure.Text = "0"
                LblTotalDeliveryMeasure.Text = "0"


                '-------------------------------------------------------------
                'Line Records are showing in Grid
                '-------------------------------------------------------------
                Dim strQryPurchaseShipped$ = "Select L.ReferenceDocId, L.ReferenceDocIdSr, Sum(L.Qty) As Qty " &
                                             "FROM SaleChallanDetail L " &
                                             "Where L.ReferenceDocId = '" & mSearchCode & "' " &
                                             "GROUP BY L.ReferenceDocId, L.ReferenceDocIdSr "

                Dim strQryPurchaseReturn$ = "SELECT L.PurchInvoice, L.PurchInvoiceSr, Sum(L.Qty) AS Qty " &
                         "FROM PurchInvoiceDetail L  " &
                         "Where L.PurchInvoice = '" & SearchCode & "' And L.PurchInvoice <> L.DocId " &
                         "GROUP BY L.PurchInvoice, L.PurchInvoiceSr  "


                mQry = "Select L.*, I.Description As ItemDesc, I.ManualCode, C.V_Type || '-' || C.ReferenceNo As ChallanRefNo, " &
                        " U.DecimalPlaces as QtyDecimalPlaces, MU.DecimalPlaces as MeasureDecimalPlaces, DMU.DecimalPlaces as DeliveryMeasureDecimalPlaces, " &
                        " D1.Description As Dimension1Desc, D2.Description As Dimension2Desc, IU.Item_UID AS Item_UIDDesc, " &
                        " (Case When IfNull(PurShipped.Qty,0) <> 0 Or IfNull(PurReturn.Qty,0) <> 0 Then 1 Else 0 End) as RowLocked " &
                        " From (Select * From PurchInvoiceDetail Where DocId = '" & SearchCode & "') As L " &
                        " LEFT JOIN Item I ON L.Item = I.Code " &
                        " Left Join Dimension1 D1   On L.Dimension1 = D1.Code " &
                        " Left Join Dimension2 D2   On L.Dimension2 = D2.Code " &
                        " LEFT JOIN PurchChallan C On L.PurchChallan = C.DocId " &
                        " LEFT JOIN Unit U On L.Unit = U.Code " &
                        " LEFT JOIN Unit MU ON L.MeasureUnit = MU.Code " &
                        " LEFT JOIN Unit Dmu On L.DeliveryMeasure = Dmu.Code " &
                        " Left Join Item_UID IU  On L.Item_UID = IU.Code " &
                        " Left Join (" & strQryPurchaseShipped & ") as PurShipped On L.DocID = PurShipped.ReferenceDocID and L.Sr = PurShipped.ReferenceDocIDSr " &
                        " Left Join (" & strQryPurchaseReturn & ") as PurReturn On L.DocID = PurReturn.PurchInvoice And L.Sr = PurReturn.PurchInvoiceSr " &
                        " Order By L.Sr "
                DsTemp = AgL.FillData(mQry, AgL.GCn)
                With DsTemp.Tables(0)
                    Dgl1.RowCount = 1
                    Dgl1.Rows.Clear()
                    If .Rows.Count > 0 Then
                        For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                            Dgl1.Rows.Add()
                            Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count - 1
                            Dgl1.Item(ColSNo, I).Tag = AgL.XNull(.Rows(I)("Sr"))
                            Dgl1.Item(Col1PurchChallan, I).Tag = AgL.XNull(.Rows(I)("PurchChallan"))
                            Dgl1.Item(Col1PurchChallan, I).Value = AgL.XNull(.Rows(I)("ChallanRefNo"))
                            Dgl1.Item(Col1PurchChallanSr, I).Value = AgL.XNull(.Rows(I)("PurchChallanSr"))
                            Dgl1.Item(Col1Item_UID, I).Tag = AgL.XNull(.Rows(I)("Item_UID"))
                            Dgl1.Item(Col1Item_UID, I).Value = AgL.XNull(.Rows(I)("Item_UIDDesc"))
                            Dgl1.Item(Col1ItemCode, I).Tag = AgL.XNull(.Rows(I)("Item"))
                            Dgl1.Item(Col1Dimension1, I).Tag = AgL.XNull(.Rows(I)("Dimension1"))
                            Dgl1.Item(Col1Dimension1, I).Value = AgL.XNull(.Rows(I)("Dimension1Desc"))
                            Dgl1.Item(Col1Dimension2, I).Tag = AgL.XNull(.Rows(I)("Dimension2"))
                            Dgl1.Item(Col1Dimension2, I).Value = AgL.XNull(.Rows(I)("Dimension2Desc"))
                            Dgl1.Item(Col1ItemCode, I).Value = AgL.XNull(.Rows(I)("I.ManualCode"))
                            Dgl1.Item(Col1Item, I).Tag = AgL.XNull(.Rows(I)("Item"))
                            Dgl1.Item(Col1Item, I).Value = AgL.XNull(.Rows(I)("ItemDesc"))
                            Dgl1.Item(Col1Specification, I).Value = AgL.XNull(.Rows(I)("Specification"))
                            Dgl1.Item(Col1LotNo, I).Value = AgL.XNull(.Rows(I)("LotNo"))
                            Dgl1.Item(Col1BaleNo, I).Value = AgL.XNull(.Rows(I)("BaleNo"))
                            Dgl1.Item(Col1SalesTaxGroup, I).Tag = AgL.XNull(.Rows(I)("SalesTaxGroupItem"))
                            Dgl1.Item(Col1SalesTaxGroup, I).Value = AgL.XNull(.Rows(I)("SalesTaxGroupItem"))
                            Dgl1.Item(Col1QtyDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("QtyDecimalPlaces"))
                            Dgl1.Item(Col1ProfitMarginPer, I).Value = AgL.VNull(.Rows(I)("ProfitMarginPer"))
                            Dgl1.Item(Col1DocQty, I).Value = Format(AgL.VNull(.Rows(I)("DocQty")), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1FreeQty, I).Value = Format(AgL.VNull(.Rows(I)("FreeQty")), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1RejQty, I).Value = Format(AgL.VNull(.Rows(I)("RejQty")), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1Qty, I).Value = Format(AgL.VNull(.Rows(I)("Qty")), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                            Dgl1.Item(Col1MeasureDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("MeasureDecimalPlaces"))
                            Dgl1.Item(Col1MeasurePerPcs, I).Value = Format(AgL.VNull(.Rows(I)("MeasurePerPcs")), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1PcsPerMeasure, I).Value = Format(AgL.VNull(.Rows(I)("PcsPerMeasure")), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1MeasureUnit, I).Value = AgL.XNull(.Rows(I)("MeasureUnit"))
                            Dgl1.Item(Col1TotalDocMeasure, I).Value = Format(AgL.VNull(.Rows(I)("TotalDocMeasure")), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1TotalFreeMeasure, I).Value = Format(AgL.VNull(.Rows(I)("TotalFreeMeasure")), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1TotalRejMeasure, I).Value = Format(AgL.VNull(.Rows(I)("TotalRejMeasure")), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1TotalMeasure, I).Value = Format(AgL.VNull(.Rows(I)("TotalMeasure")), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1Rate, I).Value = AgL.VNull(.Rows(I)("Rate"))
                            Dgl1.Item(Col1Amount, I).Value = Format(AgL.VNull(.Rows(I)("Amount")), "0.00")
                            Dgl1.Item(Col1SaleRate, I).Value = AgL.VNull(.Rows(I)("Sale_Rate"))
                            Dgl1.Item(Col1MRP, I).Value = AgL.VNull(.Rows(I)("MRP"))
                            Dgl1.Item(Col1ExpiryDate, I).Value = AgL.XNull(.Rows(I)("ExpiryDate"))

                            Dgl1.Item(Col1Remark, I).Value = AgL.XNull(.Rows(I)("Remark"))
                            Dgl1.Item(Col1Deal, I).Value = AgL.XNull(.Rows(I)("Deal"))

                            Dgl1.Item(Col1BillingType, I).Value = AgL.XNull(.Rows(I)("BillingType"))

                            Dgl1.Item(Col1PurchIndent, I).Value = AgL.XNull(.Rows(I)("PurchIndent"))

                            Dgl1.Item(Col1DeliveryMeasure, I).Value = AgL.XNull(.Rows(I)("DeliveryMeasure"))
                            Dgl1.Item(Col1DeliveryMeasurePerPcs, I).Value = Format(AgL.VNull(.Rows(I)("DeliveryMeasurePerPcs")), "0.".PadRight(AgL.VNull(.Rows(I)("DeliveryMeasureDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1DeliveryMeasureDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("DeliveryMeasureDecimalPlaces"))
                            Dgl1.Item(Col1TotalDocDeliveryMeasure, I).Value = Format(AgL.VNull(.Rows(I)("TotalDocDeliveryMeasure")), "0.".PadRight(AgL.VNull(.Rows(I)("DeliveryMeasureDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1TotalFreeDeliveryMeasure, I).Value = Format(AgL.VNull(.Rows(I)("TotalFreeDeliveryMeasure")), "0.".PadRight(AgL.VNull(.Rows(I)("DeliveryMeasureDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1TotalRejDeliveryMeasure, I).Value = Format(AgL.VNull(.Rows(I)("TotalRejDeliveryMeasure")), "0.".PadRight(AgL.VNull(.Rows(I)("DeliveryMeasureDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1TotalDeliveryMeasure, I).Value = Format(AgL.VNull(.Rows(I)("TotalDeliveryMeasure")), "0.".PadRight(AgL.VNull(.Rows(I)("DeliveryMeasureDecimalPlaces")) + 2, "0"))


                            'If .Rows(I)("RowLocked") > 0 Then Dgl1.Rows(I).DefaultCellStyle.BackColor = AgTemplate.ClsMain.Colours.GridRow_Locked


                            If Not AgL.StrCmp(Dgl1.Item(Col1Unit, I).Value, Dgl1.Item(Col1Unit, 0).Value) Then IsSameUnit = False
                            If Not AgL.StrCmp(Dgl1.Item(Col1MeasureUnit, I).Value, Dgl1.Item(Col1MeasureUnit, 0).Value) Then IsSameMeasureUnit = False
                            If Not AgL.StrCmp(Dgl1.Item(Col1DeliveryMeasure, I).Value, Dgl1.Item(Col1DeliveryMeasure, 0).Value) Then IsSameDeliveryMeasureUnit = False

                            If intQtyDecimalPlaces < Val(Dgl1.Item(Col1QtyDecimalPlaces, I).Value) Then intQtyDecimalPlaces = Val(Dgl1.Item(Col1QtyDecimalPlaces, I).Value)
                            If intMeasureDecimalPlaces < Val(Dgl1.Item(Col1MeasureDecimalPlaces, I).Value) Then intMeasureDecimalPlaces = Val(Dgl1.Item(Col1MeasureDecimalPlaces, I).Value)
                            If intDeliveryMeasureDecimalPlaces < Val(Dgl1.Item(Col1DeliveryMeasureDecimalPlaces, I).Value) Then intDeliveryMeasureDecimalPlaces = Val(Dgl1.Item(Col1DeliveryMeasureDecimalPlaces, I).Value)

                            LblTotalQty.Text = Val(LblTotalQty.Text) + Val(Dgl1.Item(Col1Qty, I).Value)
                            LblTotalMeasure.Text = Val(LblTotalMeasure.Text) + Val(Dgl1.Item(Col1TotalMeasure, I).Value)
                            LblTotalDeliveryMeasure.Text = Val(LblTotalDeliveryMeasure.Text) + Val(Dgl1.Item(Col1TotalDeliveryMeasure, I).Value)
                            LblTotalAmount.Text = Val(LblTotalAmount.Text) + Val(Dgl1.Item(Col1Amount, I).Value)

                            If .Rows(I)("RowLocked") > 0 Then Dgl1.Rows(I).DefaultCellStyle.BackColor = AgTemplate.ClsMain.Colours.GridRow_Locked : Dgl1.Rows(I).ReadOnly = True : mIsEntryLocked = True



                            Call AgCalcGrid1.FMoveRecLineTable(DsTemp.Tables(0), I)

                        Next I
                    End If
                End With
                AgCalcGrid1.FMoveRecLineLedgerAc()
                If AgCustomGrid1.Rows.Count = 0 Then AgCustomGrid1.Visible = False

                'If Dgl1.Item(Col1PurchChallan, 0).Tag = mSearchCode Then
                '    RbtInvoiceDirect.Checked = True
                'Else
                '    RbtInvoiceForChallan.Checked = True
                'End If

                'Calculation()
                '-------------------------------------------------------------
            End If
        End With
    End Sub



    Private Sub FrmSaleOrder_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Topctrl1.ChangeAgGridState(Dgl1, False)
        AgCalcGrid1.FrmType = Me.FrmType
    End Sub

    Private Sub Txt_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtV_Type.Validating, TxtVendor.Validating, TxtSalesTaxGroupParty.Validating, TxtReferenceNo.Validating, TxtV_Date.Validating
        Dim DrTemp As DataRow() = Nothing
        Dim DtTemp As DataTable = Nothing
        Dim FrmObj As New FrmPurchPartyDetail
        Try
            Select Case sender.NAME
                Case TxtV_Type.Name
                    TxtStructure.AgSelectedValue = AgStructure.ClsMain.FGetStructureFromNCat(LblV_Type.Tag, AgL.GcnRead)
                    AgCalcGrid1.AgStructure = TxtStructure.AgSelectedValue
                    AgCalcGrid1.AgNCat = LblV_Type.Tag

                    TxtCustomFields.AgSelectedValue = AgCustomFields.ClsMain.FGetCustomFieldFromV_Type(TxtV_Type.AgSelectedValue, AgL.GcnRead)
                    AgCustomGrid1.AgCustom = TxtCustomFields.AgSelectedValue

                    IniGrid()
                    FAsignProcess()
                    TxtReferenceNo.Text = AgTemplate.ClsMain.FGetManualRefNo("ReferenceNo", "PurchInvoice", TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, AgTemplate.ClsMain.ManualRefType.Max)

                Case TxtVendor.Name
                    If TxtVendor.Text <> "" Then
                        If sender.AgDataRow IsNot Nothing Then
                            TxtCurrency.AgSelectedValue = AgL.XNull(sender.AgDataRow.Cells("Currency").Value)
                            TxtSalesTaxGroupParty.Tag = AgL.XNull(sender.AgDataRow.Cells("SalesTaxPostingGroup").Value)
                            TxtSalesTaxGroupParty.Text = AgL.XNull(sender.AgDataRow.Cells("SalesTaxPostingGroup").Value)
                            TxtNature.Text = AgL.XNull(sender.AgDataRow.Cells("Nature").Value)
                        End If

                        TxtBillToParty.Tag = TxtVendor.Tag
                        TxtBillToParty.Text = TxtVendor.Text
                        BtnFillPurchChallan.Tag = Nothing

                        If AgL.StrCmp(TxtNature.Text, "Cash") Then
                            TxtCurrency.Tag = AgL.XNull(AgL.PubDtEnviro.Rows(0)("DefaultCurrency"))
                            If TxtCurrency.Tag <> "" Then
                                TxtCurrency.Text = AgL.XNull(AgL.Dman_Execute("Select Description From Currency Where Code = '" & TxtCurrency.Tag & "'  ", AgL.GCn).ExecuteScalar)
                            End If
                            TxtSalesTaxGroupParty.Tag = AgL.XNull(AgL.PubDtEnviro.Rows(0)("DefaultSalesTaxGroupParty"))
                            TxtSalesTaxGroupParty.Text = AgL.XNull(AgL.PubDtEnviro.Rows(0)("DefaultSalesTaxGroupParty"))

                            FOpenPartyDetail()
                        Else
                            mQry = " Select Mobile , DispName , " &
                                    " IfNull(Add1,'') Add1, IfNull(Add2,'') Add2, " &
                                    " Sg.CityCode , C.CityName " &
                                    " From SubGroup Sg " &
                                    " LEFT JOIN City C ON Sg.CityCode = C.CityCode " &
                                    " Where Sg.SubCode = '" & TxtVendor.AgSelectedValue & "'  "
                            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
                            With DtTemp
                                FrmObj.TxtVendorMobile.Text = AgL.XNull(.Rows(0)("Mobile"))
                                FrmObj.TxtVendorName.Text = AgL.XNull(.Rows(0)("DispName"))
                                FrmObj.TxtVendorAdd1.Text = AgL.XNull(.Rows(0)("Add1"))
                                FrmObj.TxtVendorAdd2.Text = AgL.XNull(.Rows(0)("Add2"))
                                FrmObj.TxtVendorCity.Tag = AgL.XNull(.Rows(0)("CityCode"))
                                FrmObj.TxtVendorCity.Text = AgL.XNull(.Rows(0)("CityName"))
                            End With
                            BtnFillPartyDetail.Tag = FrmObj
                        End If
                    End If

                Case TxtSalesTaxGroupParty.Name
                    AgCalcGrid1.AgPostingGroupSalesTaxParty = TxtSalesTaxGroupParty.AgSelectedValue
                    Calculation()

                Case TxtReferenceNo.Name
                    e.Cancel = Not AgTemplate.ClsMain.FCheckDuplicateRefNo("ReferenceNo", "PurchInvoice",
                                    TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue,
                                    TxtSite_Code.AgSelectedValue, Topctrl1.Mode,
                                    TxtReferenceNo.Text, mSearchCode)

                Case TxtReferenceNo.Name
                    e.Cancel = Not FCheckDuplicateRefNo()

            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FrmSaleOrder_BaseEvent_Topctrl_tbAdd() Handles Me.BaseEvent_Topctrl_tbAdd
        TxtStructure.AgSelectedValue = AgStructure.ClsMain.FGetStructureFromNCat(LblV_Type.Tag, AgL.GcnRead)
        AgCalcGrid1.AgStructure = TxtStructure.AgSelectedValue
        AgCalcGrid1.AgNCat = LblV_Type.Tag

        mIsEntryLocked = False

        TxtCustomFields.AgSelectedValue = AgCustomFields.ClsMain.FGetCustomFieldFromV_Type(TxtV_Type.AgSelectedValue, AgL.GCn)
        AgCustomGrid1.AgCustom = TxtCustomFields.AgSelectedValue

        Try
            TxtGodown.Tag = AgL.XNull(DtV_TypeSettings.Rows(0)("DEFAULT_Godown"))
            TxtGodown.Text = AgL.XNull(AgL.Dman_Execute(" Select Description From Godown Where Code = '" & TxtGodown.Tag & "'", AgL.GCn).ExecuteScalar)
        Catch ex As Exception
            MsgBox("Default Godown Is Not Set In Enviro", MsgBoxStyle.Information)
        End Try

        Try
            TxtCurrency.Tag = AgL.XNull(AgL.PubDtEnviro.Rows(0)("DefaultCurrency"))
            TxtCurrency.Text = AgL.XNull(AgL.Dman_Execute(" Select Description From Currency Where Code = '" & TxtCurrency.Tag & "'", AgL.GCn).ExecuteScalar)
        Catch ex As Exception
            MsgBox("Default Currency Is Not Set In Enviro", MsgBoxStyle.Information)
        End Try

        FAsignProcess()
        IniGrid()
        TabControl1.SelectedTab = TP1
        TxtSalesTaxGroupParty.AgSelectedValue = AgL.XNull(AgL.PubDtEnviro.Rows(0)("DefaultSalesTaxGroupParty"))
        AgCalcGrid1.AgPostingGroupSalesTaxParty = TxtSalesTaxGroupParty.AgSelectedValue
        AgCalcGrid1.AgPostingGroupSalesTaxItem = AgL.XNull(AgL.PubDtEnviro.Rows(0)("DefaultSalesTaxGroupItem"))
        RbtInvoiceDirect.Checked = True
        TxtReferenceNo.Text = AgTemplate.ClsMain.FGetManualRefNo("ReferenceNo", "PurchInvoice", TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, AgTemplate.ClsMain.ManualRefType.Max)
        'TxtVendor.Focus()
    End Sub

    Private Sub Dgl1_EditingControl_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Dgl1.EditingControl_LostFocus
        If Topctrl1.Mode = "Browse" Then Exit Sub
        Dim mRowIndex As Integer, mColumnIndex As Integer
        Dim DrTemp As DataRow() = Nothing
        Try
            mRowIndex = Dgl1.CurrentCell.RowIndex
            mColumnIndex = Dgl1.CurrentCell.ColumnIndex
            If Dgl1.Item(mColumnIndex, mRowIndex).Value Is Nothing Then Dgl1.Item(mColumnIndex, mRowIndex).Value = ""
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Rate
                    Calculation()
            End Select
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
    '            Dgl1.Item(Col1SalesTaxGroup, mRow).Value = ""
    '            Dgl1.Item(Col1MeasureUnit, mRow).Value = ""
    '            Dgl1.Item(Col1MeasurePerPcs, mRow).Value = ""
    '            Dgl1.Item(Col1Rate, mRow).Value = ""
    '            Dgl1.Item(Col1DocQty, mRow).Value = ""
    '        Else
    '            If Dgl1.AgHelpDataSet(Col1Item) IsNot Nothing Then
    '                DrTemp = Dgl1.AgHelpDataSet(Col1Item).Tables(0).Select("Code = '" & Code & "'")
    '                Call FSetColumnDecimalPlace(Dgl1.AgSelectedValue(Col1Item, mRow), mRow)
    '                Dgl1.Item(Col1Unit, mRow).Value = AgL.XNull(DrTemp(0)("Unit"))
    '                Dgl1.Item(Col1MeasureUnit, mRow).Value = AgL.XNull(DrTemp(0)("MeasureUnit"))
    '                Dgl1.Item(Col1MeasurePerPcs, mRow).Value = AgL.VNull(DrTemp(0)("MeasurePerPcs"))
    '                Dgl1.Item(Col1Rate, mRow).Value = AgL.VNull(DrTemp(0)("Rate"))
    '                Dgl1.AgSelectedValue(Col1SalesTaxGroup, mRow) = AgL.XNull(DrTemp(0)("SalesTaxPostingGroup"))
    '                If AgL.StrCmp(Dgl1.AgSelectedValue(Col1SalesTaxGroup, mRow), "") Then
    '                    Dgl1.AgSelectedValue(Col1SalesTaxGroup, mRow) = AgL.XNull(AgL.PubDtEnviro.Rows(0)("DefaultSalesTaxGroupItem"))
    '                End If

    '            End If
    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.Message & " On Validating_Item Function ")
    '    End Try
    'End Sub

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
                    Call FGetDeliveryMeasureMultiplier(mRowIndex)

                Case Col1Item
                    Validating_ItemCode(mColumnIndex, mRowIndex, DrTemp)
                    Call FGetDeliveryMeasureMultiplier(mRowIndex)

                    If CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_TransactionHistory")), Boolean) = True Then
                        FShowTransactionHistory(Dgl1.Item(Col1Item, mRowIndex).Tag)
                    End If

                Case Col1ItemCode
                    Validating_ItemCode(mColumnIndex, mRowIndex, DrTemp)
                    Call FGetDeliveryMeasureMultiplier(mRowIndex)

                    If CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_TransactionHistory")), Boolean) = True Then
                        FShowTransactionHistory(Dgl1.Item(Col1Item, mRowIndex).Tag)
                    End If

                Case Col1DeliveryMeasure
                    If Dgl1.AgDataRow IsNot Nothing Then
                        Dgl1.Item(Col1DeliveryMeasureDecimalPlaces, mRowIndex).Value = AgL.XNull(Dgl1.AgDataRow.Cells("DecimalPlaces").Value)
                    End If

                    Call FGetDeliveryMeasureMultiplier(mRowIndex)
            End Select
            Call Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub DGL1_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles Dgl1.RowsAdded, Dgl1.RowsAdded
        'sender(ColSNo, sender.Rows.Count - 1).Value = Trim(sender.Rows.Count)
        sender(ColSNo, e.RowIndex).Value = e.RowIndex + 1
    End Sub

    Private Sub FrmSaleOrder_BaseFunction_Calculation() Handles Me.BaseFunction_Calculation
        Dim I As Integer

        LblTotalQty.Text = 0
        LblTotalMeasure.Text = 0
        LblTotalDeliveryMeasure.Text = 0
        LblTotalAmount.Text = 0

        Dim DEALARR() As String = Nothing
        Dim DEALRATE As Double

        Dim MRATE As Double = 0
        AgCalcGrid1.AgPostingGroupSalesTaxParty = TxtSalesTaxGroupParty.AgSelectedValue
        AgCalcGrid1.AgVoucherCategory = "PURCH"

        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1Item, I).Value <> "" And Dgl1.Rows(I).Visible Then

                Dgl1.Item(Col1Qty, I).Value = Val(Dgl1.Item(Col1DocQty, I).Value) - Val(Dgl1.Item(Col1RejQty, I).Value) + Val(Dgl1.Item(Col1FreeQty, I).Value)



                DEALRATE = 0
                If Dgl1.Item(Col1Deal, I).Value <> "" Then
                    DEALARR = Split(Dgl1.Item(Col1Deal, I).Value.ToString, "+", 2)
                    If DEALARR.Length = 2 Then
                        DEALRATE = Format((Val(Dgl1.Item(Col1Rate, I).Value) * Val(DEALARR(0))) / (Val(DEALARR(0)) + Val(DEALARR(1))), "0.00")
                    End If
                End If


                If DEALRATE <> 0 Then
                    MRATE = DEALRATE
                Else
                    MRATE = Val(Dgl1.Item(Col1Rate, I).Value)
                End If


                'If In Item Master Measure Per Pcs Is Defined then this calculation will be executed.
                'For Example In Carpet Area Per Pcs Is Defined in Item Master and Total Area will be calculated
                'with that Area per pcs. 
                If Val(Dgl1.Item(Col1MeasurePerPcs, I).Value) <> 0 Then
                    Dgl1.Item(Col1TotalMeasure, I).Value = Format(Val(Dgl1.Item(Col1Qty, I).Value) * Val(Dgl1.Item(Col1MeasurePerPcs, I).Value), "0.".PadRight(Val(Dgl1.Item(Col1MeasureDecimalPlaces, I).Value) + 2, "0"))
                    Dgl1.Item(Col1TotalDocMeasure, I).Value = Format(Val(Dgl1.Item(Col1DocQty, I).Value) * Val(Dgl1.Item(Col1MeasurePerPcs, I).Value), "0.".PadRight(Val(Dgl1.Item(Col1MeasureDecimalPlaces, I).Value) + 2, "0"))
                    Dgl1.Item(Col1TotalFreeMeasure, I).Value = Format(Val(Dgl1.Item(Col1FreeQty, I).Value) * Val(Dgl1.Item(Col1MeasurePerPcs, I).Value), "0.".PadRight(Val(Dgl1.Item(Col1MeasureDecimalPlaces, I).Value) + 2, "0"))
                    Dgl1.Item(Col1TotalRejMeasure, I).Value = Format(Val(Dgl1.Item(Col1RejQty, I).Value) * Val(Dgl1.Item(Col1MeasurePerPcs, I).Value), "0.".PadRight(Val(Dgl1.Item(Col1MeasureDecimalPlaces, I).Value) + 2, "0"))
                End If

                'If in item master Pcs Per Measure is defined this calculation will be executed.
                'for example in case of soap user will feed how many cartons he purchased in the measure field and
                'qty will be calculated on the basis of the pcs per measure.
                If Val(Dgl1.Item(Col1PcsPerMeasure, I).Value) <> 0 Then
                    Dgl1.Item(Col1Qty, I).Value = Format(Val(Dgl1.Item(Col1TotalMeasure, I).Value) * Val(Dgl1.Item(Col1PcsPerMeasure, I).Value), "0.".PadRight(Val(Dgl1.Item(Col1QtyDecimalPlaces, I).Value) + 2, "0"))
                    Dgl1.Item(Col1DocQty, I).Value = Format(Val(Dgl1.Item(Col1TotalDocMeasure, I).Value) * Val(Dgl1.Item(Col1PcsPerMeasure, I).Value), "0.".PadRight(Val(Dgl1.Item(Col1QtyDecimalPlaces, I).Value) + 2, "0"))
                    Dgl1.Item(Col1FreeQty, I).Value = Format(Val(Dgl1.Item(Col1TotalFreeMeasure, I).Value) * Val(Dgl1.Item(Col1PcsPerMeasure, I).Value), "0.".PadRight(Val(Dgl1.Item(Col1QtyDecimalPlaces, I).Value) + 2, "0"))
                    Dgl1.Item(Col1RejQty, I).Value = Format(Val(Dgl1.Item(Col1TotalRejMeasure, I).Value) * Val(Dgl1.Item(Col1PcsPerMeasure, I).Value), "0.".PadRight(Val(Dgl1.Item(Col1QtyDecimalPlaces, I).Value) + 2, "0"))
                End If

                'if the qty unit and mesure units are equal then qty will auto come in mesure fields
                'for example yarn's unit and measure unit is Kg
                'In this case same figure will be copied in the measure.
                If AgL.StrCmp(Dgl1.Item(Col1MeasureUnit, I).Value, Dgl1.Item(Col1Unit, I).Value) Then
                    Dgl1.Item(Col1TotalMeasure, I).Value = Format(Val(Dgl1.Item(Col1Qty, I).Value), "0.".PadRight(Val(Dgl1.Item(Col1MeasureDecimalPlaces, I).Value) + 2, "0"))
                    Dgl1.Item(Col1TotalDocMeasure, I).Value = Format(Val(Dgl1.Item(Col1DocQty, I).Value), "0.".PadRight(Val(Dgl1.Item(Col1MeasureDecimalPlaces, I).Value) + 2, "0"))
                    Dgl1.Item(Col1TotalFreeMeasure, I).Value = Format(Val(Dgl1.Item(Col1FreeQty, I).Value), "0.".PadRight(Val(Dgl1.Item(Col1MeasureDecimalPlaces, I).Value) + 2, "0"))
                    Dgl1.Item(Col1TotalRejMeasure, I).Value = Format(Val(Dgl1.Item(Col1RejQty, I).Value), "0.".PadRight(Val(Dgl1.Item(Col1MeasureDecimalPlaces, I).Value) + 2, "0"))
                End If

                'By default measure unit will automatically come in delivery meaure unit and delivery measure
                'multiplier will be set to 1.
                If Val(Dgl1.Item(Col1TotalMeasure, I).Value) = 0 Then
                    Dgl1.Item(Col1DeliveryMeasureMultiplier, I).Value = 0
                ElseIf AgL.StrCmp(Dgl1.Item(Col1MeasureUnit, I).Value, Dgl1.Item(Col1DeliveryMeasure, I).Value) Then
                    Dgl1.Item(Col1DeliveryMeasureMultiplier, I).Value = 1
                End If

                'Delivery measure calculation
                'Delivery measure will be automatically calculated on the basis of delivery measure multiplier.
                'Dgl1.Item(Col1TotalDeliveryMeasure, I).Value = Format(Val(Dgl1.Item(Col1TotalMeasure, I).Value) * Val(Dgl1.Item(Col1DeliveryMeasureMultiplier, I).Value), "0.".PadRight(CType(Dgl1.Columns(Col1TotalDeliveryMeasure), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                If Val(Dgl1.Item(Col1DeliveryMeasurePerPcs, I).Value) <> 0 Then
                    Dgl1.Item(Col1TotalDeliveryMeasure, I).Value = Format(Val(Dgl1.Item(Col1Qty, I).Value) * Val(Dgl1.Item(Col1DeliveryMeasurePerPcs, I).Value), "0.".PadRight(Val(Dgl1.Item(Col1DeliveryMeasureDecimalPlaces, I).Value) + 2, "0"))
                    Dgl1.Item(Col1TotalDocDeliveryMeasure, I).Value = Format(Val(Dgl1.Item(Col1DocQty, I).Value) * Val(Dgl1.Item(Col1DeliveryMeasurePerPcs, I).Value), "0.".PadRight(Val(Dgl1.Item(Col1DeliveryMeasureDecimalPlaces, I).Value) + 2, "0"))
                    Dgl1.Item(Col1TotalFreeDeliveryMeasure, I).Value = Format(Val(Dgl1.Item(Col1FreeQty, I).Value) * Val(Dgl1.Item(Col1DeliveryMeasurePerPcs, I).Value), "0.".PadRight(Val(Dgl1.Item(Col1DeliveryMeasureDecimalPlaces, I).Value) + 2, "0"))
                    Dgl1.Item(Col1TotalRejDeliveryMeasure, I).Value = Format(Val(Dgl1.Item(Col1RejQty, I).Value) * Val(Dgl1.Item(Col1DeliveryMeasurePerPcs, I).Value), "0.".PadRight(Val(Dgl1.Item(Col1DeliveryMeasureDecimalPlaces, I).Value) + 2, "0"))
                ElseIf Val(Dgl1.Item(Col1DeliveryMeasureMultiplier, I).Value) <> 0 Then
                    'Dgl1.Item(Col1DeliveryMeasurePerPcs, I).Value = Format(Val(Dgl1.Item(Col1MeasurePerPcs, I).Value) * Val(Dgl1.Item(Col1DeliveryMeasureMultiplier, I).Value), "0.".PadRight(Val(Dgl1.Item(Col1DeliveryMeasureDecimalPlaces, I).Value) + 2, "0"))
                    Dgl1.Item(Col1TotalDeliveryMeasure, I).Value = Format(Val(Dgl1.Item(Col1TotalMeasure, I).Value) * Val(Dgl1.Item(Col1DeliveryMeasureMultiplier, I).Value), "0.".PadRight(Val(Dgl1.Item(Col1DeliveryMeasureDecimalPlaces, I).Value) + 2, "0"))
                    Dgl1.Item(Col1TotalDocDeliveryMeasure, I).Value = Format(Val(Dgl1.Item(Col1TotalDocMeasure, I).Value) * Val(Dgl1.Item(Col1DeliveryMeasureMultiplier, I).Value), "0.".PadRight(Val(Dgl1.Item(Col1DeliveryMeasureDecimalPlaces, I).Value) + 2, "0"))
                    Dgl1.Item(Col1TotalFreeDeliveryMeasure, I).Value = Format(Val(Dgl1.Item(Col1TotalFreeMeasure, I).Value) * Val(Dgl1.Item(Col1DeliveryMeasureMultiplier, I).Value), "0.".PadRight(Val(Dgl1.Item(Col1DeliveryMeasureDecimalPlaces, I).Value) + 2, "0"))
                    Dgl1.Item(Col1TotalRejDeliveryMeasure, I).Value = Format(Val(Dgl1.Item(Col1TotalRejMeasure, I).Value) * Val(Dgl1.Item(Col1DeliveryMeasureMultiplier, I).Value), "0.".PadRight(Val(Dgl1.Item(Col1DeliveryMeasureDecimalPlaces, I).Value) + 2, "0"))
                End If
                Dgl1.Item(Col1TotalDeliveryMeasure, I).Value = Val(Dgl1.Item(Col1TotalDocDeliveryMeasure, I).Value) - Val(Dgl1.Item(Col1TotalRejDeliveryMeasure, I).Value) '+ Val(Dgl1.Item(Col1TotalFreeDeliveryMeasure, I).Value)


                If AgL.StrCmp(Dgl1.Item(Col1BillingType, I).Value, "Measure") Then
                    Dgl1.Item(Col1Amount, I).Value = Format(Val(Dgl1.Item(Col1TotalDeliveryMeasure, I).Value) * MRATE, "0.".PadRight(CType(Dgl1.Columns(Col1Amount), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                ElseIf AgL.StrCmp(Dgl1.Item(Col1BillingType, I).Value, "Qty") Then
                    Dgl1.Item(Col1Amount, I).Value = Format(Val(Dgl1.Item(Col1DocQty, I).Value) * MRATE, "0.".PadRight(CType(Dgl1.Columns(Col1Amount), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                ElseIf AgL.StrCmp(Dgl1.Item(Col1BillingType, I).Value, "Doc Qty") Then
                    Dgl1.Item(Col1Amount, I).Value = Format(Val(Dgl1.Item(Col1DocQty, I).Value) * MRATE, "0.".PadRight(CType(Dgl1.Columns(Col1Amount), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                ElseIf AgL.StrCmp(Dgl1.Item(Col1BillingType, I).Value, "Doc Measure") Then
                    Dgl1.Item(Col1Amount, I).Value = Format(Val(Dgl1.Item(Col1TotalDocMeasure, I).Value) * MRATE, "0.".PadRight(CType(Dgl1.Columns(Col1Amount), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                Else
                    Dgl1.Item(Col1Amount, I).Value = Format(Val(Dgl1.Item(Col1DocQty, I).Value) * MRATE, "0.".PadRight(CType(Dgl1.Columns(Col1Amount), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                End If

                'Footer Calculation
                LblTotalQty.Text = Val(LblTotalQty.Text) + Val(Dgl1.Item(Col1Qty, I).Value)
                LblTotalMeasure.Text = Val(LblTotalMeasure.Text) + Val(Dgl1.Item(Col1TotalMeasure, I).Value)
                LblTotalDeliveryMeasure.Text = Val(LblTotalDeliveryMeasure.Text) + Val(Dgl1.Item(Col1TotalDeliveryMeasure, I).Value)
                LblTotalAmount.Text = Val(LblTotalAmount.Text) + Val(Dgl1.Item(Col1Amount, I).Value)
            End If
        Next
        AgCalcGrid1.AgPostingGroupSalesTaxParty = TxtSalesTaxGroupParty.AgSelectedValue
        AgCalcGrid1.AgVoucherCategory = "PURCH"
        AgCalcGrid1.Calculation()

        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1Item, I).Value <> "" Then
                If Val(Dgl1.Item(Col1ProfitMarginPer, I).Value) > 0 Then
                    Dgl1.Item(Col1SaleRate, I).Value = Format((Val(AgCalcGrid1.AgChargesValue("LV", I, AgStructure.AgCalcGrid.LineColumnType.Amount)) + (Val(AgCalcGrid1.AgChargesValue("LV", I, AgStructure.AgCalcGrid.LineColumnType.Amount)) * Val(Dgl1.Item(Col1ProfitMarginPer, I).Value) / 100)) / Val(Dgl1.Item(Col1Qty, I).Value), "0.00")
                End If
            End If
        Next I


        LblTotalQty.Text = Val(LblTotalQty.Text)
        LblTotalMeasure.Text = Val(LblTotalMeasure.Text)
        LblTotalAmount.Text = Val(LblTotalAmount.Text)
    End Sub

    Private Sub FrmSaleOrder_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        Dim I As Integer = 0
        If AgL.RequiredField(TxtVendor, LblVendor.Text) Then passed = False : Exit Sub
        If AgL.RequiredField(TxtBillToParty, LblPostToAc.Text) Then passed = False : Exit Sub
        If AgCL.AgIsBlankGrid(Dgl1, Dgl1.Columns(Col1Item).Index) Then passed = False : Exit Sub
        If AgCL.AgIsDuplicate(Dgl1, "" + Dgl1.Columns(Col1Item).Index.ToString + "," + Dgl1.Columns(Col1Specification).Index.ToString + "," + Dgl1.Columns(Col1LotNo).Index.ToString + "," + Dgl1.Columns(Col1BaleNo).Index.ToString + "," + Dgl1.Columns(Col1PurchChallan).Index.ToString + "," + Dgl1.Columns(Col1PurchChallanSr).Index.ToString + "," & Dgl1.Columns(Col1Dimension1).Index & "," & Dgl1.Columns(Col1Dimension2).Index & "") = True Then passed = False : Exit Sub

        With Dgl1
            For I = 0 To .Rows.Count - 1
                If .Item(Col1Item, I).Value <> "" And Dgl1.Rows(I).Visible Then
                    If Val(.Item(Col1Qty, I).Value) = 0 Then
                        MsgBox("Qty Is 0 At Row No " & Dgl1.Item(ColSNo, I).Value & "")
                        .CurrentCell = .Item(Col1DocQty, I) : Dgl1.Focus()
                        passed = False : Exit Sub
                    End If

                    'If Val(.Item(Col1Rate, I).Value) = 0 Then
                    '    MsgBox("Rate Is 0 At Row No " & Dgl1.Item(ColSNo, I).Value & "")
                    '    .CurrentCell = .Item(Col1Rate, I) : Dgl1.Focus()
                    '    passed = False : Exit Sub
                    'End If
                End If
            Next
        End With

        passed = AgTemplate.ClsMain.FCheckDuplicateRefNo("ReferenceNo", "PurchInvoice",
                                    TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue,
                                    TxtSite_Code.AgSelectedValue, Topctrl1.Mode,
                                    TxtReferenceNo.Text, mSearchCode)

        If TxtVendorDocNo.Text <> "" Then
            passed = ClsMain.FCheckDuplicatePartyDocNo("VendorDocNo", "PurchInvoice",
                    TxtV_Type.AgSelectedValue, TxtVendorDocNo.Text, mSearchCode, "Vendor", TxtVendor.Tag)
        End If
    End Sub

    Private Sub FrmSaleOrder_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
        Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
    End Sub

    Private Sub Dgl1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellEnter
        Try
            If AgL.StrCmp(Topctrl1.Mode, "Browse") Then Exit Sub
            If Dgl1.CurrentCell Is Nothing Then Exit Sub
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Qty, Col1DocQty, Col1RejQty
                    CType(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex), AgControls.AgTextColumn).AgNumberRightPlaces = Val(Dgl1.Item(Col1QtyDecimalPlaces, Dgl1.CurrentCell.RowIndex).Value)

                Case Col1MeasurePerPcs, Col1TotalMeasure, Col1TotalDocMeasure, Col1TotalRejMeasure
                    CType(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex), AgControls.AgTextColumn).AgNumberRightPlaces = Val(Dgl1.Item(Col1MeasureDecimalPlaces, Dgl1.CurrentCell.RowIndex).Value)

                Case Col1TotalDeliveryMeasure, Col1TotalDocDeliveryMeasure, Col1TotalRejDeliveryMeasure
                    CType(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex), AgControls.AgTextColumn).AgNumberRightPlaces = Val(Dgl1.Item(Col1DeliveryMeasureDecimalPlaces, Dgl1.CurrentCell.RowIndex).Value)

                Case Col1Dimension1, Col1Dimension2
                    If Dgl1.Item(Col1PurchIndent, Dgl1.CurrentCell.RowIndex).Value <> "" Then
                        Dgl1.Columns(Col1Dimension1).ReadOnly = True
                        Dgl1.Columns(Col1Dimension2).ReadOnly = True
                    Else
                        Dgl1.Columns(Col1Dimension1).ReadOnly = False
                        Dgl1.Columns(Col1Dimension2).ReadOnly = False
                    End If
                Case Col1Rate
                    If Topctrl1.Mode = "Edit" Then Dgl1.CurrentCell.ReadOnly = False
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ProcFillItems(ByVal bChallanNoStr As String)
        Dim I As Integer = 0
        Dim DtTemp As DataTable = Nothing
        Try
            If bChallanNoStr = "" Then Exit Sub

            mQry = "SELECT Max(L.Item) As Item, Max(I.Description) as Item_Name, " &
                        " Max(I.ManualCode) as ItemManualCode, Max(L.Specification) as Specification, " &
                        " Max(H.V_Type) || '-' ||  Max(H.ReferenceNo) AS ChallanNo,   " &
                        " Max(H.V_Date) as ChallanDate, Max(L.BillingType) as BillingType, " &
                        " Sum(L.DocQty) - IfNull(Sum(Cd.DocQty), 0) as [Bal.DocQty],   " &
                        " Sum(L.FreeQty) - IfNull(Sum(Cd.FreeQty), 0) as [Bal.FreeQty],   " &
                        " Sum(L.Qty) - IfNull(Sum(Cd.Qty), 0) as [Bal.Qty],   " &
                        " Sum(L.TotalMeasure) - IfNull(Sum(Cd.TotalMeasure), 0) as [Bal.Measure],   " &
                        " Sum(L.TotalDeliveryMeasure) - IfNull(Sum(Cd.TotalDeliveryMeasure), 0) as [Bal.DeliveryMeasure],   " &
                        " Max(L.Unit) as Unit, Max(L.MeasureUnit) as MeasureUnit, Max(L.DeliveryMeasure) as DeliveryMeasure, Max(L.Rate) as Rate,  " &
                        " Max(L.SalesTaxGroupItem) SalesTaxGroupItem, L.PurchChallan, L.PurchChallanSr, " &
                        " Max(D1.Description) As D1Desc, Max(D2.Description) As D2Desc, " &
                        " Max(L.Dimension1) As Dimension1, Max(L.Dimension2) As Dimension2, " &
                        " Max(L.Item_UId) As Item_UId, Max(IU.Item_UId) As Item_UIdDesc, " &
                        " Max(L.MeasurePerPcs) As MeasurePerPcs, Max(L.DeliveryMeasurePerPcs) as DeliveryMeasurePerPcs, Max(L.DeliveryMeasureMultiplier) as DeliveryMeasureMultiplier, " &
                        " Max(L.MeasureUnit) As MeasureUnit,  Max(L.Deal) as Deal, Max(L.ProfitMarginPer) as ProfitMarginPer, Max(L.Mrp) as Mrp, Max(L.Sale_Rate) as Sale_Rate, max(L.ExpiryDate) as ExpiryDate, " &
                        " Max(U.DecimalPlaces) As QtyDecimalPlaces, Max(U1.DecimalPlaces) As MeasureDecimalPlaces, Max(U2.DecimalPlaces) As DeliveryMeasureDecimalPlaces   " &
                        " FROM (  " &
                        "    SELECT DocID, V_Type, ReferenceNo, V_Date   " &
                        "    FROM PurchChallan    " &
                        " ) AS  H   " &
                        " LEFT JOIN PurchChallanDetail L  ON H.DocID = L.PurchChallan    " &
                        " Left Join Item I  On L.Item  = I.Code   " &
                        " LEFT JOIN Voucher_Type Vt  ON H.V_Type = Vt.V_Type    " &
                        " Left Join (   " &
                        "    SELECT L.PurchChallan, L.PurchChallanSr, Sum (L.Qty) AS Qty, " &
                        "    Sum(L.TotalMeasure) as TotalMeasure, Sum(L.TotalDeliveryMeasure) as TotalDeliveryMeasure, " &
                        "    Sum (L.DocQty) AS DocQty, Sum (L.FreeQty) AS FreeQty  " &
                        "    FROM PurchInvoiceDetail L     " &
                        "    Where L.DocId <> '" & mSearchCode & "'   " &
                        "    GROUP BY L.PurchChallan, L.PurchChallanSr " &
                        " ) AS CD ON L.DocId = CD.PurchChallan AND L.Sr = CD.PurchChallanSr " &
                        " LEFT JOIN Unit U On L.Unit = U.Code   " &
                        " LEFT JOIN Unit U1 On L.MeasureUnit = U1.Code   " &
                        " LEFT JOIN Unit U2 On L.DeliveryMeasure = U2.Code   " &
                        " Left Join Dimension1 D1 On L.Dimension1 = D1.Code " &
                        " Left Join Dimension2 D2 On L.Dimension2 = D2.Code " &
                        " LEFT JOIN Item_UID IU ON IU.code = L.Item_UID " &
                        " Where L.PurchChallan + Convert(nVarChar,L.PurchChallanSr) In (" & bChallanNoStr & ")" &
                        " GROUP BY L.PurchChallan, L.PurchChallanSr " &
                        " Having Sum(L.Qty) - IfNull(Sum(Cd.Qty), 0) > 0  " &
                        " Order By ChallanDate "
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
                        Dgl1.Item(Col1PurchChallan, J).Tag = AgL.XNull(.Rows(I)("PurchChallan"))
                        Dgl1.Item(Col1PurchChallan, J).Value = AgL.XNull(.Rows(I)("ChallanNo"))
                        Dgl1.Item(Col1PurchChallanSr, J).Value = AgL.XNull(.Rows(I)("PurchChallanSr"))
                        Dgl1.Item(Col1Item, J).Tag = AgL.XNull(.Rows(I)("Item"))
                        Dgl1.Item(Col1Item_UID, J).Tag = AgL.XNull(.Rows(I)("Item_UId"))
                        Dgl1.Item(Col1Item_UID, J).Value = AgL.XNull(.Rows(I)("Item_UIdDesc"))
                        Dgl1.Item(Col1BillingType, J).Value = AgL.XNull(.Rows(I)("Billingtype"))
                        Dgl1.Item(Col1DeliveryMeasure, J).Value = AgL.XNull(.Rows(I)("DeliveryMeasure"))
                        Dgl1.Item(Col1Item, J).Value = AgL.XNull(.Rows(I)("Item_Name"))
                        Dgl1.Item(Col1Dimension1, Dgl1.Rows.Count - 2).Tag = AgL.XNull(.Rows(I)("Dimension1"))
                        Dgl1.Item(Col1Dimension1, Dgl1.Rows.Count - 2).Value = AgL.XNull(.Rows(I)("D1Desc"))
                        Dgl1.Item(Col1Dimension2, Dgl1.Rows.Count - 2).Tag = AgL.XNull(.Rows(I)("Dimension2"))
                        Dgl1.Item(Col1Dimension2, Dgl1.Rows.Count - 2).Value = AgL.XNull(.Rows(I)("D2Desc"))
                        Dgl1.Item(Col1Specification, J).Value = AgL.XNull(.Rows(I)("Specification"))
                        Dgl1.Item(Col1SalesTaxGroup, J).Tag = AgL.XNull(.Rows(I)("SalesTaxGroupItem"))
                        Dgl1.Item(Col1SalesTaxGroup, J).Value = AgL.XNull(.Rows(I)("SalesTaxGroupItem"))
                        Dgl1.Item(Col1DocQty, J).Value = AgL.VNull(.Rows(I)("Bal.Qty"))
                        Dgl1.Item(Col1FreeQty, J).Value = AgL.VNull(.Rows(I)("Bal.FreeQty"))
                        Dgl1.Item(Col1Qty, J).Value = AgL.VNull(.Rows(I)("Bal.Qty"))
                        Dgl1.Item(Col1QtyDecimalPlaces, J).Value = AgL.VNull(.Rows(I)("QtyDecimalPlaces"))
                        Dgl1.Item(Col1MeasureDecimalPlaces, J).Value = AgL.VNull(.Rows(I)("MeasureDecimalPlaces"))
                        Dgl1.Item(Col1DeliveryMeasureDecimalPlaces, J).Value = AgL.VNull(.Rows(I)("DeliveryMeasureDecimalPlaces"))
                        Dgl1.Item(Col1TotalMeasure, J).Value = AgL.VNull(.Rows(I)("Bal.Measure"))
                        Dgl1.Item(Col1TotalDocDeliveryMeasure, J).Value = AgL.VNull(.Rows(I)("Bal.DeliveryMeasure"))
                        Dgl1.Item(Col1TotalDeliveryMeasure, J).Value = AgL.VNull(.Rows(I)("Bal.DeliveryMeasure"))
                        Dgl1.Item(Col1Unit, J).Value = AgL.XNull(.Rows(I)("Unit"))
                        Dgl1.Item(Col1MeasurePerPcs, J).Value = Format(AgL.VNull(.Rows(I)("MeasurePerPcs")), "0.0000")
                        Dgl1.Item(Col1DeliveryMeasurePerPcs, J).Value = Format(AgL.VNull(.Rows(I)("DeliveryMeasurePerPcs")), "0.0000")
                        Dgl1.Item(Col1DeliveryMeasureMultiplier, J).Value = Format(AgL.VNull(.Rows(I)("DeliveryMeasureMultiplier")), "0.0000")
                        Dgl1.Item(Col1MeasureUnit, J).Value = AgL.XNull(.Rows(I)("Unit"))
                        Dgl1.Item(Col1Rate, J).Value = Format(AgL.VNull(.Rows(I)("Rate")), "0.00")
                        Dgl1.Item(Col1MRP, J).Value = Format(AgL.VNull(.Rows(I)("Mrp")), "0.00")
                        Dgl1.Item(Col1SaleRate, J).Value = Format(AgL.VNull(.Rows(I)("Sale_Rate")), "0.00")
                        Dgl1.Item(Col1ProfitMarginPer, J).Value = Format(AgL.VNull(.Rows(I)("ProfitMarginPer")), "0.00")
                        Dgl1.Item(Col1Deal, J).Value = AgL.XNull(.Rows(I)("Deal"))
                        Dgl1.Item(Col1ExpiryDate, J).Value = AgL.XNull(.Rows(I)("ExpiryDate"))



                        J += 1
                        'FGetPurchIndent(Dgl1.Item(Col1Item, I).Tag, Dgl1.Item(Col1PurchIndent, I).Value)

                        'AgCalcGrid1.FCopyStructureLine(AgL.XNull(.Rows(I)("PurchChallan")), Dgl1, I, AgL.VNull(.Rows(I)("PurchChallan")))
                    Next I
                End If
            End With
            AgCalcGrid1.Calculation(True)
            Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub TempPurchInvoice_BaseFunction_DispText() Handles Me.BaseFunction_DispText
        'If AgL.StrCmp(Topctrl1.Mode, "Browse") Then
        '    BtnFillPurchChallan.Enabled = False
        'ElseIf RbtInvoiceForChallan.Checked = True Then
        '    BtnFillPurchChallan.Enabled = True
        'Else
        '    BtnFillPurchChallan.Enabled = False
        'End If

        'If BlnIsDirectInvoice Then
        '    GrpDirectInvoice.Visible = False
        '    BtnFillPurchChallan.Visible = False
        '    Dgl1.Columns(Col1PurchChallan).Visible = False
        'End If

        'If BlnIsTotalDeliveryMeasureVisible = False Then LblTotalDeliveryMeasure.Visible = False : LblTotalDeliveryMeasureText.Visible = False
        'If BlnIsMeasureVisible = False Then LblTotalMeasure.Visible = False : LblTotalMeasureText.Visible = False


    End Sub

    Private Sub DGL1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.KeyDown
        If Dgl1.CurrentCell IsNot Nothing Then
            If e.Control And e.KeyCode = Keys.D And Dgl1.Rows(Dgl1.CurrentCell.RowIndex).DefaultCellStyle.BackColor <> AgTemplate.ClsMain.Colours.GridRow_Locked Then
                sender.CurrentRow.Visible = False
                Calculation()
            End If
        End If

        If e.KeyCode = Keys.Delete Then
            If sender.currentrow.selected Then
                If sender.Rows(sender.currentcell.rowindex).DefaultCellStyle.BackColor = AgTemplate.ClsMain.Colours.GridRow_Locked Then
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

        If Dgl1.CurrentCell IsNot Nothing Then
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Item
                    If e.KeyCode = Keys.Insert Then
                        FOpenItemMaster(Dgl1.Columns(Col1Item).Index, Dgl1.CurrentCell.RowIndex)
                    End If
            End Select
        End If
        'If e.KeyCode = Keys.Enter Then
        '    If Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name = Col1Item Then
        '        If Dgl1.Item(Dgl1.CurrentCell.ColumnIndex, Dgl1.CurrentCell.RowIndex).Value Is Nothing Then Dgl1.Item(Dgl1.CurrentCell.ColumnIndex, Dgl1.CurrentCell.RowIndex).Value = ""
        '        If Dgl1.Item(Dgl1.CurrentCell.ColumnIndex, Dgl1.CurrentCell.RowIndex).Value = "" Then
        '            AgCalcGrid1.Focus()
        '        End If
        '    End If
        'End If


        'Call FOpenMaster(e)
    End Sub

    Private Function FGetRelationalData() As Boolean
        Try
            Dim bRData As String
            '// Check for relational data in Purchase Return
            mQry = " DECLARE @Temp NVARCHAR(Max); "
            mQry += " SET @Temp=''; "
            mQry += " SELECT  @Temp=@Temp +  X.VNo || ', ' FROM (SELECT DISTINCT H.V_Type || '-' || Convert(VARCHAR,H.V_No) AS VNo From PurchInvoiceDetail  L LEFT JOIN PurchInvoice H ON L.DocId = H.DocID WHERE L.ReferenceDocID  = '" & TxtDocId.Text & "' And IfNull(H.IsDeleted,0) = 0) AS X  "
            mQry += " SELECT @Temp as RelationalData "
            bRData = AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar
            If bRData.Trim <> "" Then
                MsgBox(" Purchase Return " & bRData & " created against Invoice No. " & TxtV_Type.Tag & "-" & TxtV_No.Text & ". Can't Modify Entry")
                FGetRelationalData = True
                Exit Function
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " in FGetRelationalData in TempRequisition")
            FGetRelationalData = True
        End Try
    End Function

    Private Sub ME_BaseEvent_Topctrl_tbEdit(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbEdit
        RbtInvoiceDirect.Checked = True

        If mIsEntryLocked Then
            If AgL.PubUserName.ToUpper = "SA" Or AgL.PubUserName.ToUpper = AgLibrary.ClsConstant.PubSuperUserName Then
                If MsgBox("Referential data exist. Do you want to modify record?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                    Passed = False
                    Exit Sub
                Else
                    TxtVendor.Enabled = False
                End If
            Else
                MsgBox("Referential data exist. Can't modify record.")
                Passed = False
                Exit Sub
            End If
        End If
        FAsignProcess()
    End Sub

    Private Sub ME_BaseEvent_Topctrl_tbDel(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbDel
        If mIsEntryLocked Then
            MsgBox("Referential data exist. Can't delete record.")
            Passed = False
        End If
    End Sub

    Private Function FCheckDuplicateRefNo() As Boolean
        FCheckDuplicateRefNo = True

        If Topctrl1.Mode = "Add" Then
            mQry = " SELECT COUNT(*) FROM PurchInvoice WHERE ReferenceNo = '" & TxtReferenceNo.Text & "'   " &
                   " AND V_Type ='" & TxtV_Type.AgSelectedValue & "'  And Div_Code = '" & TxtDivision.AgSelectedValue & "' And Site_Code = '" & TxtSite_Code.AgSelectedValue & "' And IfNull(IsDeleted,0) = 0  "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then FCheckDuplicateRefNo = False : MsgBox("Reference No. Already Exists") : TxtReferenceNo.Focus()
        Else
            mQry = " SELECT COUNT(*) FROM PurchInvoice WHERE ReferenceNo = '" & TxtReferenceNo.Text & "'  " &
                   " AND V_Type ='" & TxtV_Type.AgSelectedValue & "'  And Div_Code = '" & TxtDivision.AgSelectedValue & "' And Site_Code = '" & TxtSite_Code.AgSelectedValue & "' And IfNull(IsDeleted,0) = 0 AND DocID <>'" & mSearchCode & "'  "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then FCheckDuplicateRefNo = False : MsgBox("Reference No. Already Exists") : TxtReferenceNo.Focus()
        End If

        If Topctrl1.Mode = "Add" Then
            mQry = " SELECT COUNT(*) FROM PurchInvoice WHERE VendorDocNo = '" & TxtVendorDocNo.Text & "' And Vendor = '" & TxtVendor.AgSelectedValue & "'  " &
                   " AND V_Type ='" & TxtV_Type.AgSelectedValue & "'  And Div_Code = '" & TxtDivision.AgSelectedValue & "' And Site_Code = '" & TxtSite_Code.AgSelectedValue & "' And IfNull(IsDeleted,0) = 0  "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then FCheckDuplicateRefNo = False : MsgBox("Vendor Doc. No. Already Exists") : TxtReferenceNo.Focus()
        Else
            mQry = " SELECT COUNT(*) FROM PurchInvoice WHERE VendorDocNo = '" & TxtVendorDocNo.Text & "'  And Vendor = '" & TxtVendor.AgSelectedValue & "'  " &
                   " AND V_Type ='" & TxtV_Type.AgSelectedValue & "'  And Div_Code = '" & TxtDivision.AgSelectedValue & "' And Site_Code = '" & TxtSite_Code.AgSelectedValue & "' And IfNull(IsDeleted,0) = 0 AND DocID <>'" & mSearchCode & "'  "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then FCheckDuplicateRefNo = False : MsgBox("Vendor Doc No. Already Exists") : TxtReferenceNo.Focus()
        End If
    End Function

    Private Sub FrmCarpetMaterialPlan_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AgL.WinSetting(Me, 654, 990, 0, 0)
        AgCustomGrid1.FrmType = Me.FrmType
    End Sub

    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub

    Private Sub BtnFillSaleChallan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnFillPurchChallan.Click
        Try
            If Topctrl1.Mode = "Browse" Then Exit Sub
            If mIsEntryLocked Then Exit Sub
            If RbtInvoiceForChallan.Checked = True Then
                Dim StrTicked As String
                StrTicked = FHPGD_PendingSaleChallan()
                If StrTicked <> "" Then
                    ProcFillItems(StrTicked)
                Else
                    Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
                End If
                Dgl1.Focus()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function FHPGD_PendingSaleChallan() As String
        Dim FRH_Multiple As DMHelpGrid.FrmHelpGrid_Multi
        Dim StrSendText As String
        Dim StrRtn As String = ""

        StrSendText = RbtInvoiceForChallan.Tag

        mQry = " SELECT 'o' As Tick, L.PurchChallan + Convert(nVarChar,L.PurchChallanSr) As PurchChallanDocIdSr, " &
                " Max(H.V_Type) || '-' ||  Max(H.ReferenceNo) AS ChallanNo, " &
                " Max(H.V_Date) as ChallanDate, Max(I.Description) as Item_Name,  " &
                " Max(D1.Description) As " & ClsMain.FGetDimension1Caption() & ", " &
                " Max(D2.Description) As " & ClsMain.FGetDimension2Caption() & ", " &
                " Sum(L.Qty) - IfNull(Sum(Cd.Qty), 0) as [Bal.Qty],   " &
                " Max(L.Unit) as Unit " &
                " FROM (  " &
                "       SELECT DocID, V_Type, ReferenceNo, V_Date   " &
                "       FROM PurchChallan    " &
                "       WHERE Vendor='" & TxtVendor.Tag & "' " &
                "       And Div_Code = '" & TxtDivision.Tag & "' " &
                "       AND Site_Code = '" & TxtSite_Code.Tag & "' " &
                "       AND V_Date<='" & TxtV_Date.Text & "'" &
                " ) AS  H   " &
                " LEFT JOIN PurchChallanDetail L  ON H.DocID = L.PurchChallan    " &
                " Left Join Item I  On L.Item  = I.Code   " &
                " LEFT JOIN Voucher_Type Vt  ON H.V_Type = Vt.V_Type    " &
                " Left Join (   " &
                "       SELECT L.PurchChallan, L.PurchChallanSr, Sum (L.Qty) AS Qty  " &
                "       FROM PurchInvoiceDetail L     " &
                "       Where L.DocId <> '" & mSearchCode & "'" &
                "       GROUP BY L.PurchChallan, L.PurchChallanSr " &
                " ) AS CD ON L.DocId = CD.PurchChallan AND L.Sr = CD.PurchChallanSr " &
                " Left Join Dimension1 D1 On L.Dimension1 = D1.Code " &
                " Left Join Dimension2 D2 On L.Dimension2 = D2.Code " &
                " LEFT JOIN Unit U On L.Unit = U.Code   " &
                " LEFT JOIN Unit U1 On L.MeasureUnit = U1.Code   " &
                " GROUP BY L.PurchChallan, L.PurchChallanSr " &
                " Having  Sum(L.Qty) - IfNull(Sum(Cd.Qty), 0) > 0 " &
                " Order By ChallanDate "

        FRH_Multiple = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(AgL.FillData(mQry, AgL.GCn).TABLES(0)), "", 300, 730, , , False)
        FRH_Multiple.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple.FFormatColumn(1, , 0, , False)
        FRH_Multiple.FFormatColumn(2, "Challan No.", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(3, "Challan Date", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(4, "Item Name", 200, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(5, ClsMain.FGetDimension1Caption(), 200, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(6, ClsMain.FGetDimension2Caption(), 200, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(7, "Bal Qty", 100, DataGridViewContentAlignment.MiddleRight)
        FRH_Multiple.FFormatColumn(8, "Unit", 100, DataGridViewContentAlignment.MiddleLeft)

        FRH_Multiple.StartPosition = FormStartPosition.CenterScreen
        FRH_Multiple.ShowDialog()

        If FRH_Multiple.BytBtnValue = 0 Then
            StrRtn = FRH_Multiple.FFetchData(1, "'", "'", ",", True)
        End If
        FHPGD_PendingSaleChallan = StrRtn

        FRH_Multiple = Nothing
    End Function

    Private Sub FPostInPurchChallan(ByVal Conn As SQLiteConnection, ByVal Cmd As SQLiteCommand)
        Dim I As Integer = 0, Cnt As Integer = 0
        Dim bSelectionQry$ = ""

        mQry = " UPDATE PurchInvoiceDetail " &
                " Set " &
                " PurchChallan = NULL, " &
                " PurchChallanSr = NULL " &
                " Where DocId = '" & mSearchCode & "' " &
                " And PurchChallan = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " Select Count(*) From PurchInvoiceDetail L  Where L.DocId = '" & mSearchCode & "' And L.PurchChallan Is Null "
        If AgL.VNull(AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar) > 0 Then
            mQry = " Select Count(*) From PurchChallan   Where DocId = '" & mSearchCode & "' "
            If AgL.VNull(AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar) > 0 Then
                mQry = " Update dbo.PurchChallan " &
                       " SET DocID = PurchInvoice.docid, " &
                       " 	V_Type = PurchInvoice.v_type," &
                       " 	V_Prefix = PurchInvoice.v_prefix, " &
                       " 	V_Date = PurchInvoice.v_date, " &
                       " 	V_No = PurchInvoice.v_no, " &
                       " 	Div_Code = PurchInvoice.div_code, " &
                       " 	Site_Code = PurchInvoice.site_code, " &
                       " 	ReferenceNo = PurchInvoice.referenceno, " &
                       " 	Vendor = PurchInvoice.vendor, " &
                       " 	PurchOrder = PurchInvoice.purchorder, " &
                       " 	Currency = PurchInvoice.currency, " &
                       " 	SalesTaxGroupParty = PurchInvoice.salestaxgroupparty, " &
                       " 	Structure = PurchInvoice.structure, " &
                       " 	BillingType = PurchInvoice.billingtype, " &
                       " 	VendorDocNo = PurchInvoice.vendordocno, " &
                       " 	VendorDocDate = PurchInvoice.vendordocdate, " &
                       " 	Form = PurchInvoice.form, " &
                       " 	FormNo = PurchInvoice.formno, " &
                       " 	Godown = PurchInvoice.godown, " &
                       " 	Process = PurchInvoice.Process, " &
                       " 	Remarks = PurchInvoice.remarks, " &
                       " 	TotalQty = PurchInvoice.totalqty, " &
                       " 	TotalMeasure = PurchInvoice.totalmeasure, " &
                       " 	TotalAmount = PurchInvoice.totalamount, " &
                       " 	EntryBy = PurchInvoice.entryby, " &
                       " 	EntryDate = PurchInvoice.entrydate, " &
                       " 	EntryType = PurchInvoice.entrytype, " &
                       " 	EntryStatus = PurchInvoice.entrystatus, " &
                       " 	ApproveBy = PurchInvoice.approveby, " &
                       " 	ApproveDate = PurchInvoice.approvedate, " &
                       " 	MoveToLog = PurchInvoice.movetolog, " &
                       " 	MoveToLogDate = PurchInvoice.movetologdate, " &
                       " 	IsDeleted = PurchInvoice.isdeleted, " &
                       " 	Status = PurchInvoice.status, " &
                       " 	UID = PurchInvoice.uid, " &
                       " 	CustomFields = PurchInvoice.customfields " &
                       "    FROM PurchInvoice  " &
                       "    WHERE PurchChallan.DocID = PurchInvoice.DocID " &
                       "    And PurchInvoice.DocID ='" & mSearchCode & "'    "
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            Else
                mQry = " INSERT INTO PurchChallan(DocID, V_Type, V_Prefix, V_Date, V_No, Div_Code, Site_Code, ReferenceNo, Vendor, " &
                        " Currency, SalesTaxGroupParty, Structure, BillingType, VendorDocNo, VendorDocDate, Form, FormNo,  " &
                        " Remarks, TotalQty, TotalMeasure, TotalAmount, EntryBy, EntryDate, EntryType,  " &
                        " EntryStatus, ApproveBy, ApproveDate, MoveToLog, MoveToLogDate, IsDeleted, Status, Godown, Process) " &
                        " SELECT DocID, V_Type, V_Prefix, V_Date, V_No, Div_Code, Site_Code, ReferenceNo, Vendor,  " &
                        " Currency, SalesTaxGroupParty, Structure, BillingType, VendorDocNo, VendorDocDate, Form, FormNo,  " &
                        " Remarks, TotalQty, TotalMeasure, TotalAmount, EntryBy, EntryDate, EntryType,  " &
                        " EntryStatus, ApproveBy, ApproveDate, MoveToLog, MoveToLogDate, IsDeleted, Status, Godown, Process " &
                        " FROM PurchInvoice  " &
                        " Where DocId = '" & mSearchCode & "'"
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            End If
        End If

        mQry = "Delete FROM PurchChallanDetail Where DocID ='" & mSearchCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Insert Into PurchChallanDetail(DocId, Sr, PurchChallan, PurchChallanSr, " &
                " Item_Uid, Item, Specification, BaleNo, SalesTaxGroupItem, LotNo, " &
                " DocQty, FreeQty, RejQty, Qty, Unit, MeasurePerPcs, PcsPerMeasure, MeasureUnit, TotalDocMeasure, TotalFreeMeasure, TotalRejMeasure, " &
                " TotalMeasure, Rate, Amount, Landed_Value, Remark, Deal, ExpiryDate, BillingType, " &
                " DeliveryMeasure, DeliveryMeasureMultiplier, TotalDocDeliveryMeasure, TotalFreeDeliveryMeasure, TotalRejDeliveryMeasure, " &
                " TotalDeliveryMeasure, MRP, Sale_Rate) " &
                " Select L.DocId, L.Sr, L.DocId, L.Sr, " &
                " L.Item_Uid, L.Item, L.Specification, L.BaleNo, L.SalesTaxGroupItem, L.LotNo, " &
                " L.DocQty, L.FreeQty, L.RejQty, L.Qty, L.Unit, L.MeasurePerPcs, L.PcsPerMeasure, L.MeasureUnit, L.TotalDocMeasure, L.TotalFreeMeasure, L.TotalRejMeasure, " &
                " L.TotalMeasure, L.Landed_Value/L.Qty As Rate, L.Landed_Value, L.Landed_Value, L.Remark, L.Deal, " &
                " L.ExpiryDate, L.BillingType, " &
                " L.DeliveryMeasure, L.DeliveryMeasureMultiplier, L.TotalDocDeliveryMeasure, L.TotalFreeDeliveryMeasure, L.TotalRejDeliveryMeasure, " &
                " L.TotalDeliveryMeasure, L.Mrp, L.Sale_Rate " &
                " FROM PurchInvoiceDetail L " &
                " Where L.DocId = '" & mSearchCode & "' And L.PurchChallan Is Null "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " Delete From Stock Where DocId = '" & mSearchCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " INSERT INTO  Stock(DocID, Sr, V_Type, V_Prefix, V_Date, V_No, Div_Code, Site_Code,   " &
                " SubCode, Currency, SalesTaxGroupParty, Structure, BillingType, Item,  " &
                " Godown,EType_IR, Qty_Iss, Qty_Rec, Unit, LotNo, MeasurePerPcs, Measure_Iss, Measure_Rec, MeasureUnit, " &
                " Rate, Amount, Landed_Value, Remarks, RecId, ReferenceDocId, ReferenceDocIdSr, ExpiryDate, Sale_Rate, MRP, Process) " &
                " SELECT L.DocId, L.Sr, H.V_Type, H.V_Prefix, H.V_Date, H.V_No, H.Div_Code, H.Site_Code, " &
                " H.Vendor, H.Currency, H.SalesTaxGroupParty, H.Structure, H.BillingType, L.Item, H.Godown,'R', 0, L.Qty, " &
                " L.Unit, L.LotNo, L.MeasurePerPcs,0, L.TotalMeasure, L.MeasureUnit, L.Rate, L.Amount, L.Landed_Value, " &
                " L.Remark, H.ReferenceNo, L.DocId, L.Sr, L.ExpiryDate, L.Sale_Rate, L.MRP, Process " &
                " FROM PurchChallanDetail L  " &
                " LEFT JOIN PurchChallan H On L.DocId = H.DocId " &
                " Where L.DocId = '" & mSearchCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        'mQry = " UPDATE PurchChallanDetail " & _
        '        " Set PurchChallanDetail.Rate = PurchInvoiceDetail.Landed_Value/PurchInvoiceDetail.Qty, " & _
        '        " PurchChallanDetail.Amount = PurchInvoiceDetail.Landed_Value, " & _
        '        " PurchChallanDetail.Landed_Value = PurchInvoiceDetail.Landed_Value " & _
        '        " From PurchInvoiceDetail " & _
        '        " Where PurchChallanDetail.DocId = PurchInvoiceDetail.PurchChallan " & _
        '        " And PurchChallanDetail.Sr = PurchInvoiceDetail.PurchChallanSr " & _
        '        " And PurchInvoiceDetail.DocId = '" & mSearchCode & "'"
        'AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " UPDATE PurchInvoiceDetail " &
                " Set " &
                " PurchChallan = DocId, " &
                " PurchChallanSr = Sr " &
                " Where DocId = '" & mSearchCode & "' " &
                " And PurchChallan Is Null "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    End Sub

    Private Sub RbtInvoiceDirect_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RbtInvoiceDirect.Click, RbtInvoiceForChallan.Click
        Try
            Select Case sender.Name
                Case RbtInvoiceDirect.Name
                    BtnFillPurchChallan.Enabled = False

                Case RbtInvoiceForChallan.Name
                    BtnFillPurchChallan.Enabled = True
            End Select
            Dgl1.AgHelpDataSet(Col1Item) = Nothing
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FrmPurchInvoice_BaseEvent_Topctrl_tbRef() Handles Me.BaseEvent_Topctrl_tbRef
        Try
            If Dgl1.AgHelpDataSet(Col1Item) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1Item).Dispose() : Dgl1.AgHelpDataSet(Col1Item) = Nothing
        Catch ex As Exception
        End Try
        Try
            If Dgl1.AgHelpDataSet(Col1BillingType) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1BillingType).Dispose() : Dgl1.AgHelpDataSet(Col1BillingType) = Nothing
        Catch ex As Exception
        End Try
        Try
            If Dgl1.AgHelpDataSet(Col1ItemCode) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1ItemCode).Dispose() : Dgl1.AgHelpDataSet(Col1ItemCode) = Nothing
        Catch ex As Exception
        End Try
        If TxtCurrency.AgHelpDataSet IsNot Nothing Then TxtCurrency.AgHelpDataSet.Dispose() : TxtCurrency.AgHelpDataSet = Nothing
        If TxtVendor.AgHelpDataSet IsNot Nothing Then TxtVendor.AgHelpDataSet.Dispose() : TxtVendor.AgHelpDataSet = Nothing
        If TxtSalesTaxGroupParty.AgHelpDataSet IsNot Nothing Then TxtSalesTaxGroupParty.AgHelpDataSet.Dispose() : TxtSalesTaxGroupParty.AgHelpDataSet = Nothing
    End Sub

    Private Sub BtnFillPartyDetail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnFillPartyDetail.Click
        FOpenPartyDetail()
    End Sub

    Private Sub FOpenPartyDetail()
        Dim FrmObj As FrmPurchPartyDetail
        Try
            If BtnFillPartyDetail.Tag Is Nothing Then
                FrmObj = New FrmPurchPartyDetail
            Else
                FrmObj = BtnFillPartyDetail.Tag
            End If
            FrmObj.DispText(IIf(Topctrl1.Mode = "Browse", False, True))
            FrmObj.ShowDialog()
            If FrmObj.mOkButtonPressed Then BtnFillPartyDetail.Tag = FrmObj
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FrmPurchInvoice_StoreItem_BaseEvent_Topctrl_tbPrn(ByVal SearchCode As String) Handles Me.BaseEvent_Topctrl_tbPrn
        If TxtV_Type.Tag = "CPINV" Then
            PrintCarpetInvoice()
        Else
            mQry = " SELECT H.DocID, H.V_Type, H.V_Date, H.ReferenceNo, " &
                        " H.Currency, H.SalesTaxGroupParty, H.BillingType, H.VendorDocNo, H.VendorDocDate,  " &
                        " H.Form, H.FormNo, H.Remarks, H.EntryBy, H.EntryDate, H.ApproveBy, H.ApproveDate, " &
                        " L.DocId, L.Sr, L.Item, L.Specification, L.SalesTaxGroupItem, L.DocQty, L.RejQty, L.Qty, L.Unit, U.DecimalPlaces as UnitDecimalPlaces,  " &
                        " L.MeasurePerPcs, L.MeasureUnit, L.TotalDocMeasure, L.TotalRejMeasure, L.TotalMeasure, L.Rate, L.Amount, L.Remark, L.LotNo, " &
                        " SG.DispName AS VendorName, Sg.Add1, Sg.Add2, Sg.Add3, Sg.Mobile As VendorMobile, " &
                        " D1.Description AS D1Desc, D2.Description AS D2Desc, E.Caption_Dimension1, E.Caption_Dimension2, " &
                        " City.CityName As VendorCityName, I.Description AS ItemDesc, C.ReferenceNo as PurchChallanNo, PO.ReferenceNo as PurchOrderNo,  " &
                        " L.TotalDeliveryMeasure, L.DeliveryMeasure, " &
                        " H.VendorName as Trans_VendorName, H.VendorAdd1 as Trans_VendorAdd1, H.VendorAdd2 as Trans_VendorAdd2, H.VendorMobile as Trans_VendorMobile, H.VendorCityName as Trans_VendorCityName, " &
                        " " & AgCalcGrid1.FLineTableFieldNameStr("L.", "L_") & " " &
                        " " & AgCustomGrid1.FHeaderTableFieldNameStr("H.", "H_") & " " &
                        " FROM (SELECT * FROM PurchInvoice WHERE DocId = '" & mSearchCode & "') AS H  " &
                        " LEFT JOIN (SELECT * FROM PurchInvoiceDetail WHERE DocId ='" & mSearchCode & "') AS  L ON H.DocID = L.DocId  " &
                        " LEFT JOIN SubGroup Sg ON H.Vendor = Sg.SubCode " &
                        " LEFT JOIN PurchChallan C ON L.PurchChallan = C.DocID " &
                        " LEFT JOIN PurchOrder PO ON L.PurchOrder = PO.DocID " &
                        " LEFT JOIN Item I ON L.Item = I.Code  " &
                        " LEFT JOIN Unit U ON I.Unit = U.Code  " &
                        " LEFT JOIN Enviro E ON E.Site_Code = H.Site_Code AND E.Div_Code = H.Div_Code " &
                        " LEFT JOIN Dimension1 D1 ON D1.Code = L.Dimension1 " &
                        " LEFT JOIN Dimension2 D2 ON D2.Code = L.Dimension2 " &
                        " LEFT JOIN City ON Sg.CityCode = City.CityCode " &
                        " Where H.DocId = '" & mSearchCode & "'"
            ClsMain.FPrintThisDocument(Me, TxtV_Type.Tag, mQry, "PurchInvoice_Print|PurchInvoiceQtyMeasure_Print", "Purchase Invoice", "For Qty|For Qty & Measure")
        End If

    End Sub


    Private Sub PrintCarpetInvoice()
        Dim I As Integer = 0
        Dim DsTemp As DataSet = Nothing
        Dim bTempTable$ = ""
        Dim bStructJoin As String = "", bItemUIDJoin As String = ""
        Dim mCondStr As String = ""
        Dim mReportQry$ = ""
        Dim mOrderByStr$ = ""

        bTempTable = AgL.GetGUID(AgL.GCn).ToString
        bTempTable = "Temp" & bTempTable
        mQry = "CREATE TABLE [#" & bTempTable & "] " &
                " (DocId nVarChar(36), PurchChallan nVarChar(36), PurchOrder nVarChar(36), PurchOrderSr Integer, Item_UID NVARCHAR(2000) )"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

        mQry = " SELECT PID.DocId, L.DocId AS PurchChallan, L.PurchOrder, L.PurchOrderSr " &
                  " FROM PurchInvoiceDetail PID " &
                  " LEFT JOIN PurchChallanDetail L on L.DocId = PID.PurchChallan AND L.Sr = PID.PurchChallanSr " &
                  " WHERE PID.DOCID = '" & mSearchCode & "' " &
                  " GROUP BY PID.DocId, L.DocId, L.PurchOrder, L.PurchOrderSr "
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                    mQry = " INSERT INTO [#" & bTempTable & "](DocId, PurchChallan, PurchOrder, PurchOrderSr, Item_UID )" &
                            " SELECT '" & mSearchCode & "',  '" & AgL.XNull(.Rows(I)("PurchChallan")) & "', '" & AgL.XNull(.Rows(I)("PurchOrder")) & "', " & AgL.VNull(.Rows(I)("PurchOrderSr")) & ", '" & mroll(mSearchCode, AgL.XNull(.Rows(I)("PurchChallan")), AgL.XNull(.Rows(I)("PurchOrder")), AgL.VNull(.Rows(I)("PurchOrderSr"))) & "' "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
                Next

            End If
        End With

        mQry = "SELECT T.DocId, T.PurchChallan, T.PurchOrder, T.PurchOrderSr, T.Item_UID " &
                " From [#" & bTempTable & "] T "
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        bItemUIDJoin = "LEFT JOIN [#" & bTempTable & "] T  ON T.DocId=H.DocId AND T.PurchChallan =L.PurchChallan AND T.PurchOrder =PCD.PurchOrder AND T.PurchOrderSr =PCD.PurchOrderSr "

        mQry = " SELECT H.DocID, H.V_Type, H.V_Date, H.ReferenceNo,  H.Currency, H.SalesTaxGroupParty, L.BillingType, " &
                " H.VendorDocNo, H.VendorDocDate,   H.Form, H.FormNo, H.Remarks, H.EntryBy, H.EntryDate, H.ApproveBy, H.ApproveDate, " &
                " L.DocId, L.Sr, L.Item, L.Specification, L.SalesTaxGroupItem, L.DocQty, L.RejQty, L.Qty, L.Unit,  T.Item_UID," &
                " L.DeliveryMeasurePerPcs, L.DeliveryMeasure, L.TotalDocDeliveryMeasure, L.TotalRejDeliveryMeasure, L.TotalDeliveryMeasure, L.Rate, L.Amount, " &
                " L.Remark, L.LotNo,  SG.DispName AS VendorName, Sg.Add1, Sg.Add2, Sg.Add3, Sg.Mobile As VendorMobile,  " &
                " City.CityName As VendorCityName, I.Description AS ItemDesc, C.ReferenceNo as PurchChallanNo, IfNull(PO.ManualRefNo,PO.ReferenceNo) as PurchOrderNo,  PCD.PurchOrderSr, " &
                " (Case When L.DeliveryMeasure='SQ.Meter' Then Size.PrintingDescriptionMeter When  L.DeliveryMeasure='SQ.Cms' Then Size.PrintingDescriptionCms Else Size.PrintingDescription End) as SizeDesc, " &
                " L.Gross_Amount  L_Gross_Amount,L.Basic_Excise_Duty_Per  L_Basic_Excise_Duty_Per,L.Basic_Excise_Duty  L_Basic_Excise_Duty, " &
                " L.Excise_ECess_Per  L_Excise_ECess_Per,L.Excise_ECess  L_Excise_ECess,L.Excise_HECess_Per  L_Excise_HECess_Per, " &
                " L.Excise_HECess  L_Excise_HECess,L.Total_Excise_Duty  L_Total_Excise_Duty,L.Discount_Pre_Tax_Per  L_Discount_Pre_Tax_Per, " &
                " L.Discount_Pre_Tax  L_Discount_Pre_Tax,L.Other_Additions_Pre_Tax  L_Other_Additions_Pre_Tax, " &
                " L.Sales_Tax_Taxable_Amt  L_Sales_Tax_Taxable_Amt,L.Vat_Per  L_Vat_Per,L.Vat  L_Vat,L.Sat_Per  L_Sat_Per,L.Sat  L_Sat, " &
                " L.Cst_Per  L_Cst_Per,L.Cst  L_Cst,L.Custom_Duty_Taxable_Amt  L_Custom_Duty_Taxable_Amt, " &
                " L.Custom_Duty_Per  L_Custom_Duty_Per,L.Custom_Duty  L_Custom_Duty,L.Custom_Duty_ECess_Per  L_Custom_Duty_ECess_Per, " &
                " L.Custom_Duty_ECess  L_Custom_Duty_ECess,L.Custom_Duty_HECess_Per  L_Custom_Duty_HECess_Per, " &
                " L.Custom_Duty_HECess  L_Custom_Duty_HECess,L.Additional_Duty_Per  L_Additional_Duty_Per,L.Additional_Duty  L_Additional_Duty, " &
                " L.Total_Custom_Duty  L_Total_Custom_Duty,L.Sub_Total  L_Sub_Total,L.Insurance_Per  L_Insurance_Per,L.Insurance  L_Insurance, " &
                " L.Freight_Per  L_Freight_Per,L.Freight  L_Freight,L.Handling_Charges_Per  L_Handling_Charges_Per, " &
                " L.Handling_Charges  L_Handling_Charges,L.Other_Charges_Per  L_Other_Charges_Per,L.Other_Charges  L_Other_Charges, " &
                " L.Discount_Per  L_Discount_Per,L.Discount  L_Discount,L.Round_Off_Per  L_Round_Off_Per,L.Round_Off  L_Round_Off, " &
                " L.Net_Amount  L_Net_Amount,L.Freight_Outward  L_Freight_Outward     " &
                " FROM (SELECT * FROM PurchInvoice WHERE DocId = '" & mSearchCode & "') AS H    " &
                " LEFT JOIN (SELECT * FROM PurchInvoiceDetail WHERE DocId ='" & mSearchCode & "') AS  L ON H.DocID = L.DocId  " &
                " LEFT JOIN SubGroup Sg ON H.Vendor = Sg.SubCode   " &
                " LEFT JOIN PurchChallan C ON L.PurchChallan = C.DocID   " &
                " LEFT JOIN PurchChallanDetail PCD ON L.PurchChallan = PCD.DocID AND L.PurchChallanSr = PCD.Sr " &
                " LEFT JOIN PurchOrder PO ON PCD.PurchOrder = PO.DocID   " &
                " LEFT JOIN Item I ON L.Item = I.Code    " &
                " Left Join Rug_CarpetSku Cs On I.Code = Cs.Code " &
                " Left Join Rug_Size Size On Cs.Size = Size.Code " &
                " LEFT JOIN City ON Sg.CityCode = City.CityCode " &
                " " & bItemUIDJoin & " "
        ClsMain.FPrintThisDocument(Me, TxtV_Type.Tag, mQry, "PurchInvoice_Print|PurchInvoiceQtyMeasure_Print", "Purchase Invoice", "For Qty|For Qty & Measure")
    End Sub

    Function mroll(ByVal bSearchCode As String, ByVal bPurchChallanDocId As String, ByVal bPurchOrderDocId As String, ByVal bPurchOrderSr As Integer)
        Dim I As Integer
        Dim troll As Integer = 0
        Dim froll As Integer = 0
        Dim bCntSrl As Integer = 0
        mroll = ""

        Dim DsTemp As DataSet
        mQry = " SELECT IU.Item_UID AS BaleNo" &
                " FROM PurchInvoiceDetail JIRU  " &
                " LEFT JOIN Item_UID IU ON IU.Code=JIRU.Item_UID  " &
                " LEFT JOIN PurchChallanDetail PCD ON PCD.DocId=JIRU.PurchChallan And PCD.Sr=JIRU.PurchChallanSr " &
                " WHERE JIRU.DocID = '" & bSearchCode & "' AND JIRU.PurchChallan = '" & bPurchChallanDocId & "' " &
                " AND PCD.PurchOrder = '" & bPurchOrderDocId & "' AND PCD.PurchOrderSr =" & bPurchOrderSr & " " &
                " Order By (Case When IsNumeric(IU.Item_UID)>0 Then Convert(Numeric,IU.Item_UID) Else 0 End)  "


        DsTemp = AgL.FillData(mQry, AgL.GcnRead)
        With DsTemp.Tables(0)

            If .Rows.Count > 0 Then
                For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                    If froll = 0 Then
                        froll = AgL.VNull(.Rows(I)("BaleNo"))
                        mroll = AgL.XNull(.Rows(I)("BaleNo"))
                        bCntSrl = 1
                    ElseIf froll + 1 <> AgL.VNull(.Rows(I)("BaleNo")) Then
                        If bCntSrl > 1 Then
                            mroll = mroll & "-" & AgL.XNull(.Rows(I - 1)("BaleNo")) & ", " & AgL.XNull(.Rows(I)("BaleNo"))
                            bCntSrl = 1
                        Else
                            mroll = mroll & ", " & AgL.XNull(.Rows(I)("BaleNo"))
                            bCntSrl = 1
                        End If
                        froll = AgL.VNull(.Rows(I)("BaleNo"))
                    Else
                        froll = AgL.VNull(.Rows(I)("BaleNo"))
                        bCntSrl += 1
                    End If

                    If I = DsTemp.Tables(0).Rows.Count - 1 Then
                        If froll <> AgL.VNull(.Rows(I)("BaleNo")) Then
                            mroll = mroll & ", " & AgL.XNull(.Rows(I)("BaleNo")) & ""
                        Else
                            If bCntSrl > 1 Then
                                mroll = mroll & "-" & AgL.XNull(.Rows(I)("BaleNo")) & ""
                            Else
                                bCntSrl = 0
                            End If
                        End If
                    End If
                Next I
            End If
        End With

        mroll = mroll
    End Function

    Private Sub TxtDescription_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtRemarks.KeyDown
        'If e.KeyCode = Keys.Enter Then
        '    If MsgBox("Do you want to save?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "Save") = MsgBoxResult.Yes Then
        '        Topctrl1.FButtonClick(13)
        '    End If
        'End If
    End Sub

    Private Function AccountPosting() As Boolean
        Dim LedgAry() As AgLibrary.ClsMain.LedgRec
        Dim I As Integer, J As Integer = 0
        Dim DsTemp As DataSet = Nothing
        Dim mNarr As String = "", mCommonNarr$ = ""
        Dim mNetAmount As Double, mRoundOff As Double = 0
        Dim GcnRead As SQLiteConnection
        GcnRead = New SQLiteConnection
        GcnRead.ConnectionString = AgL.Gcn_ConnectionString
        GcnRead.Open()

        mNetAmount = 0
        mCommonNarr = ""
        mCommonNarr = ""
        If mCommonNarr.Length > 255 Then mCommonNarr = AgL.MidStr(mCommonNarr, 0, 255)

        ReDim Preserve LedgAry(I)
        I = UBound(LedgAry) + 1
        ReDim Preserve LedgAry(I)
        LedgAry(I).SubCode = AgL.XNull(AgL.PubDtEnviro.Rows(0)("PurchaseAc"))
        LedgAry(I).ContraSub = TxtVendor.AgSelectedValue
        LedgAry(I).AmtCr = 0
        LedgAry(I).AmtDr = Val(AgCalcGrid1.AgChargesValue(AgTemplate.ClsMain.Charges.NETAMOUNT, AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Amount))
        If mNarr.Length > 255 Then mNarr = AgL.MidStr(mNarr, 0, 255)
        LedgAry(I).Narration = mNarr

        I = UBound(LedgAry) + 1
        ReDim Preserve LedgAry(I)
        LedgAry(I).SubCode = TxtVendor.AgSelectedValue
        LedgAry(I).ContraSub = AgL.XNull(AgL.PubDtEnviro.Rows(0)("PurchaseAc"))
        LedgAry(I).AmtCr = Val(AgCalcGrid1.AgChargesValue(AgTemplate.ClsMain.Charges.NETAMOUNT, AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Amount))
        LedgAry(I).AmtDr = 0
        LedgAry(I).Narration = mNarr

        If AgL.PubManageOfflineData Then
            If AgL.LedgerPost(AgL.MidStr(Topctrl1.Mode, 0, 1), LedgAry, AgL.GcnSite, AgL.ECmdSite, mSearchCode, CDate(TxtV_Date.Text), AgL.PubUserName, AgL.PubLoginDate, mCommonNarr, , AgL.GcnSite_ConnectionString) = False Then
                AccountPosting = False : Err.Raise(1, , "Error in Ledger Posting")
            Else
            End If
        End If

        If AgL.LedgerPost(AgL.MidStr(Topctrl1.Mode, 0, 1), LedgAry, AgL.GCn, AgL.ECmd, mSearchCode, CDate(TxtV_Date.Text), AgL.PubUserName, AgL.PubLoginDate, mCommonNarr, , AgL.Gcn_ConnectionString) = False Then
            AccountPosting = False : Err.Raise(1, , "Error in Ledger Posting")
        End If
        GcnRead.Close()
        GcnRead.Dispose()
    End Function

    Private Sub Dgl1_EditingControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.EditingControl_KeyDown
        Try
            If AgL.StrCmp(Topctrl1.Mode, "Browse") Then Exit Sub
            If Dgl1.CurrentCell Is Nothing Then Exit Sub
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Item
                    If e.KeyCode <> Keys.Enter And e.KeyCode <> Keys.Insert Then
                        If Dgl1.AgHelpDataSet(Col1Item) Is Nothing Then
                            FCreateHelpItem(Col1Item)
                        End If
                    ElseIf e.KeyCode = Keys.Insert Then
                        If RbtInvoiceDirect.Checked Then
                            FOpenItemMaster(Dgl1.Columns(Col1Item).Index, Dgl1.CurrentCell.RowIndex)
                        End If
                    End If



                Case Col1BillingType
                    If Dgl1.AgHelpDataSet(Col1BillingType) Is Nothing Then
                        mQry = " SELECT 'Qty' AS Code, 'Qty' AS Name " &
                            " Union ALL " &
                            " SELECT 'Doc Qty' AS Code, 'Doc Qty' AS Name " &
                            " Union ALL " &
                            " SELECT 'Measure' AS Code, 'Measure' AS Name " &
                            " Union ALL " &
                            " SELECT 'Doc Measure' AS Code, 'Doc Measure' AS Name "
                        Dgl1.AgHelpDataSet(Col1BillingType) = AgL.FillData(mQry, AgL.GCn)
                    End If

                Case Col1DeliveryMeasure
                    If Dgl1.AgHelpDataSet(Col1DeliveryMeasure) Is Nothing Then
                        mQry = " SELECT Code, Code AS Description, DecimalPlaces FROM Unit "
                        Dgl1.AgHelpDataSet(Col1DeliveryMeasure, 1) = AgL.FillData(mQry, AgL.GCn)
                    End If
                Case Col1SalesTaxGroup
                    If Dgl1.AgHelpDataSet(Col1SalesTaxGroup) Is Nothing Then
                        mQry = " SELECT Description as Code, Description FROM PostingGroupSalesTaxItem "
                        Dgl1.AgHelpDataSet(Col1SalesTaxGroup) = AgL.FillData(mQry, AgL.GCn)
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

            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    'Private Sub FOpenMaster(ByVal e As System.Windows.Forms.KeyEventArgs)
    '    Dim FrmObj As Object = Nothing
    '    Dim CFOpen As New ClsFunction
    '    Dim DtTemp As DataTable = Nothing
    '    Try
    '        If AgL.StrCmp(Topctrl1.Mode, "Browse") Then Exit Sub

    '        If e.KeyCode = Keys.Insert Then
    '            If Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name = Col1Item Then
    '                If Not mItemType.Contains(",") Then
    '                    mQry = " Select MnuName, MnuText From ItemType Where Code = '" & mItemType & "' "
    '                    DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
    '                    If DtTemp.Rows.Count > 0 Then
    '                        FrmObj = CFOpen.FOpen(DtTemp.Rows(0)("MnuName"), DtTemp.Rows(0)("MnuText"), True)
    '                        If FrmObj IsNot Nothing Then
    '                            FrmObj.MdiParent = Me.MdiParent
    '                            FrmObj.Show()
    '                            FrmObj.Topctrl1.FButtonClick(0)
    '                            FrmObj = Nothing
    '                        End If
    '                    End If
    '                End If
    '            End If
    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub

    Private Sub Validating_Item_Uid(ByVal Item_Uid As String, ByVal mRow As Integer)
        Dim DrTemp As DataRow() = Nothing
        Dim DtTemp As DataTable = Nothing

        Try
            mQry = " SELECT I.Code, I.Description, I.Specification, I.Unit, I.ManualCode, I.MeasureUnit, I.Measure As MeasurePerPcs, " &
                   " U.DecimalPlaces as QtyDecimalPlaces, MU.DecimalPlaces as MeasureDecimalPlaces, UI.Code as ItemUIDCode " &
                   " FROM (Select Item, Code From Item_UID Where Item_Uid = '" & Dgl1.Item(Col1Item_UID, mRow).Value & "') UI " &
                   " Left Join Item I  On UI.Item  = I.Code " &
                   " Left Join Unit U  On I.Unit = U.Code " &
                   " Left Join Unit MU  On I.MeasureUnit = MU.Code "
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

            If DtTemp.Rows.Count > 0 Then
                Dgl1.Item(Col1Item_UID, mRow).Tag = AgL.XNull(DtTemp.Rows(0)("ItemUIDCode"))
                Dgl1.Item(Col1ItemCode, mRow).Tag = AgL.XNull(DtTemp.Rows(0)("Code"))
                Dgl1.Item(Col1ItemCode, mRow).Value = AgL.XNull(DtTemp.Rows(0)("ManualCode"))
                Dgl1.Item(Col1Item, mRow).Tag = AgL.XNull(DtTemp.Rows(0)("Code"))
                Dgl1.Item(Col1Item, mRow).Value = AgL.XNull(DtTemp.Rows(0)("Description"))
                Dgl1.Item(Col1Specification, mRow).Value = AgL.XNull(DtTemp.Rows(0)("Specification"))
                Dgl1.Item(Col1Qty, mRow).Value = 1
                Dgl1.Item(Col1Unit, mRow).Value = AgL.XNull(DtTemp.Rows(0)("Unit"))
                Dgl1.Item(Col1QtyDecimalPlaces, mRow).Value = AgL.VNull(DtTemp.Rows(0)("QtyDecimalPlaces"))
                Dgl1.Item(Col1MeasurePerPcs, mRow).Value = Format(AgL.VNull(DtTemp.Rows(0)("MeasurePerPcs")), "0.".PadRight(AgL.VNull(DtTemp.Rows(0)("MeasureDecimalPlaces")) + 2, "0"))
                Dgl1.Item(Col1TotalMeasure, mRow).Value = AgL.VNull(DtTemp.Rows(0)("MeasurePerPcs"))
                Dgl1.Item(Col1MeasureUnit, mRow).Value = AgL.XNull(DtTemp.Rows(0)("MeasureUnit"))
                Dgl1.Item(Col1MeasureDecimalPlaces, mRow).Value = AgL.VNull(DtTemp.Rows(0)("MeasureDecimalPlaces"))
            Else
                MsgBox("Invalid Item UID", MsgBoxStyle.Information)
                Dgl1.Item(Col1Item_UID, mRow).Value = ""
            End If

        Catch ex As Exception
            MsgBox(ex.Message & " On Validating_Item_Uid Function ")
        End Try
    End Sub

    Private Sub Validating_ItemCode(ByVal mColumn As Integer, ByVal mRow As Integer, ByVal DrTemp As DataRow())
        Dim DtTemp As DataTable = Nothing
        Try
            If Dgl1.Item(mColumn, mRow).Value.ToString.Trim = "" Or Dgl1.AgSelectedValue(mColumn, mRow).ToString.Trim = "" Then
                Dgl1.Item(Col1Unit, mRow).Value = ""
                Dgl1.Item(Col1Dimension1, mRow).Value = ""
                Dgl1.Item(Col1Dimension1, mRow).Tag = ""
                Dgl1.Item(Col1Dimension2, mRow).Value = ""
                Dgl1.Item(Col1Dimension2, mRow).Tag = ""
                Dgl1.Item(Col1Item_UID, mRow).Value = ""
                Dgl1.Item(Col1Item_UID, mRow).Tag = ""
            Else
                If DrTemp IsNot Nothing Then
                    Dgl1.Item(Col1Item, mRow).Tag = AgL.XNull(DrTemp(0)("Code"))
                    Dgl1.Item(Col1Item, mRow).Value = AgL.XNull(DrTemp(0)("Description"))
                    Dgl1.Item(Col1ItemCode, mRow).Tag = AgL.XNull(DrTemp(0)("Code"))
                    Dgl1.Item(Col1ItemCode, mRow).Value = AgL.XNull(DrTemp(0)("ManualCode"))

                    Dgl1.Item(Col1Item_UID, mRow).Tag = AgL.XNull(DrTemp(0)("Item_UId"))
                    Dgl1.Item(Col1Item_UID, mRow).Value = AgL.XNull(DrTemp(0)("Item_UIdDesc"))

                    Dgl1.Item(Col1Specification, mRow).Value = AgL.XNull(DrTemp(0)("Specification"))
                    Dgl1.Item(Col1Unit, mRow).Value = AgL.XNull(DrTemp(0)("Unit"))
                    Dgl1.Item(Col1Rate, mRow).Value = AgL.VNull(DrTemp(0)("Rate"))
                    Dgl1.Item(Col1SalesTaxGroup, mRow).Tag = AgL.XNull(DrTemp(0)("SalesTaxPostingGroup"))
                    Dgl1.Item(Col1SalesTaxGroup, mRow).Tag = AgL.XNull(DrTemp(0)("SalesTaxPostingGroup"))
                    If AgL.StrCmp(Dgl1.AgSelectedValue(Col1SalesTaxGroup, mRow), "") Then
                        Dgl1.Item(Col1SalesTaxGroup, mRow).Tag = AgL.XNull(AgL.PubDtEnviro.Rows(0)("DefaultSalesTaxGroupItem"))
                        Dgl1.Item(Col1SalesTaxGroup, mRow).Tag = AgL.XNull(AgL.PubDtEnviro.Rows(0)("DefaultSalesTaxGroupItem"))
                    End If
                    Dgl1.Item(Col1Dimension1, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("Dimension1").Value)
                    Dgl1.Item(Col1Dimension1, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("" & ClsMain.FGetDimension1Caption() & "").Value)
                    Dgl1.Item(Col1Dimension2, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("Dimension2").Value)
                    Dgl1.Item(Col1Dimension2, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("" & ClsMain.FGetDimension2Caption() & "").Value)

                    Dgl1.Item(Col1MeasurePerPcs, mRow).Value = AgL.VNull(DrTemp(0)("MeasurePerPcs"))
                    Dgl1.Item(Col1MeasureUnit, mRow).Value = AgL.XNull(DrTemp(0)("MeasureUnit"))
                    Dgl1.Item(Col1QtyDecimalPlaces, mRow).Value = AgL.VNull(DrTemp(0)("QtyDecimalPlaces"))
                    Dgl1.Item(Col1MeasureDecimalPlaces, mRow).Value = AgL.VNull(DrTemp(0)("MeasureDecimalPlaces"))

                    Dgl1.Item(Col1DeliveryMeasure, mRow).Value = AgL.XNull(DrTemp(0)("MeasureUnit"))
                    Dgl1.Item(Col1DeliveryMeasureDecimalPlaces, mRow).Value = AgL.VNull(DrTemp(0)("MeasureDecimalPlaces"))
                    Dgl1.Item(Col1BillingType, mRow).Value = "Qty"
                    Dgl1.Item(Col1DocQty, mRow).Value = AgL.VNull(DrTemp(0)("Bal.DocQty"))
                    Dgl1.Item(Col1FreeQty, mRow).Value = AgL.VNull(DrTemp(0)("Bal.FreeQty"))
                    Dgl1.Item(Col1Qty, mRow).Value = AgL.VNull(DrTemp(0)("Bal.Qty"))
                    Dgl1.Item(Col1PurchChallan, mRow).Tag = AgL.XNull(DrTemp(0)("PurchChallan"))
                    Dgl1.Item(Col1PurchChallan, mRow).Value = AgL.XNull(DrTemp(0)("ChallanNo"))
                    Dgl1.Item(Col1PurchChallanSr, mRow).Value = AgL.XNull(DrTemp(0)("PurchChallanSr"))
                Else
                    If Dgl1.AgDataRow IsNot Nothing Then
                        Dgl1.Item(Col1Item, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("Code").Value)
                        Dgl1.Item(Col1Item, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("Description").Value)
                        Dgl1.Item(Col1ItemCode, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("Code").Value)
                        Dgl1.Item(Col1ItemCode, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("ManualCode").Value)
                        Dgl1.Item(Col1Dimension1, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("Dimension1").Value)
                        Dgl1.Item(Col1Dimension1, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("" & ClsMain.FGetDimension1Caption() & "").Value)
                        Dgl1.Item(Col1Dimension2, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("Dimension2").Value)
                        Dgl1.Item(Col1Dimension2, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("" & ClsMain.FGetDimension2Caption() & "").Value)
                        Dgl1.Item(Col1Specification, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("Specification").Value)
                        Dgl1.Item(Col1Unit, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("Unit").Value)
                        Dgl1.Item(Col1Rate, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("Rate").Value)

                        Dgl1.Item(Col1Item_UID, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("Item_UId").Value)
                        Dgl1.Item(Col1Item_UID, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("Item_UIdDesc").Value)

                        Dgl1.Item(Col1SalesTaxGroup, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("SalesTaxPostingGroup").Value)
                        Dgl1.Item(Col1SalesTaxGroup, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("SalesTaxPostingGroup").Value)
                        If AgL.StrCmp(Dgl1.Item(Col1SalesTaxGroup, mRow).Tag, "") Then
                            Dgl1.Item(Col1SalesTaxGroup, mRow).Tag = AgL.XNull(AgL.PubDtEnviro.Rows(0)("DefaultSalesTaxGroupItem"))
                            Dgl1.Item(Col1SalesTaxGroup, mRow).Value = AgL.XNull(AgL.PubDtEnviro.Rows(0)("DefaultSalesTaxGroupItem"))
                        End If
                        Dgl1.Item(Col1MeasurePerPcs, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("MeasurePerPcs").Value)
                        Dgl1.Item(Col1MeasureUnit, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("MeasureUnit").Value)
                        Dgl1.Item(Col1QtyDecimalPlaces, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("QtyDecimalPlaces").Value)
                        Dgl1.Item(Col1MeasureDecimalPlaces, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("MeasureDecimalPlaces").Value)

                        Dgl1.Item(Col1DeliveryMeasure, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("MeasureUnit").Value)
                        Dgl1.Item(Col1DeliveryMeasureDecimalPlaces, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("MeasureDecimalPlaces").Value)
                        Dgl1.Item(Col1DeliveryMeasureMultiplier, mRow).Value = 1
                        Dgl1.Item(Col1BillingType, mRow).Value = "Qty"

                        Dgl1.Item(Col1DocQty, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("Bal.DocQty").Value)
                        Dgl1.Item(Col1FreeQty, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("Bal.FreeQty").Value)
                        Dgl1.Item(Col1Qty, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("Bal.Qty").Value)
                        Dgl1.Item(Col1PurchChallan, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("PurchChallan").Value)
                        Dgl1.Item(Col1PurchChallan, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("ChallanNo").Value)
                        Dgl1.Item(Col1PurchChallanSr, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("PurchChallanSr").Value)

                        mQry = " Select L.Rate, L.MRP From PurchChallanDetail L LEFT JOIN PurchChallan H ON L.DocId = H.DocId Where L.Item = '" & Dgl1.Item(Col1Item, mRow).Tag & "' Order By H.V_Date Desc Limit 1 "
                        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

                        If DtTemp.Rows.Count > 0 Then
                            Dgl1.Item(Col1MRP, mRow).Value = AgL.VNull(DtTemp.Rows(0)("MRP"))
                            Dgl1.Item(Col1Rate, mRow).Value = AgL.VNull(DtTemp.Rows(0)("Rate"))
                        End If

                        mQry = "Select ProfitMarginPer " &
                               "From Item " &
                               "Where Code = '" & AgL.XNull(Dgl1.AgDataRow.Cells("Code").Value) & "' "
                        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

                        If DtTemp.Rows.Count > 0 Then
                            Dgl1.Item(Col1ProfitMarginPer, mRow).Value = AgL.VNull(DtTemp.Rows(0)("ProfitMarginPer"))
                        End If

                        'If RbtInvoiceDirect.Checked Then FGetPurchChallan(mRow)

                        If RbtInvoiceForChallan.Checked Then
                            Dgl1.Item(Col1ProfitMarginPer, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("ProfitMarginPer").Value)
                            Dgl1.Item(Col1SaleRate, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("Sale_Rate").Value)
                            Dgl1.Item(Col1MRP, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("Mrp").Value)
                            Dgl1.Item(Col1ExpiryDate, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("ExpiryDate").Value)
                            Dgl1.Item(Col1Deal, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("Deal").Value)
                        End If

                        'FGetPurchIndent(Dgl1.Item(Col1Item, mRow).Tag, Dgl1.Item(Col1PurchIndent, mRow).Value)
                    End If
                End If

                Try
                    If mRow <> 0 Then Dgl1.Item(Col1BillingType, mRow).Value = Dgl1.Item(Col1BillingType, mRow - 1).Value
                Catch ex As Exception
                End Try
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " On Validating_Item Function ")
        End Try
    End Sub

    Private Sub FGetDeliveryMeasureMultiplier(ByVal mRow As Integer)
        Dim DtTemp As DataTable = Nothing
        Try
            If AgL.StrCmp(AgL.XNull(DtV_TypeSettings.Rows(0)("IndustryType")), "Carpet") Then
                Dgl1.Item(Col1DeliveryMeasureMultiplier, mRow).Value = 0
                If AgL.StrCmp(Dgl1.Item(Col1DeliveryMeasure, mRow).Value, "SQ.FEET") Then
                    mQry = "Select FeetArea From Rug_Size Size Left Join Rug_CarpetSku Cs On Size.Code = Cs.Size Where Cs.Code = '" & Dgl1.Item(Col1Item, mRow).Tag & "' "
                    DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
                    If DtTemp.Rows.Count > 0 Then
                        Dgl1.Item(Col1DeliveryMeasurePerPcs, mRow).Value = AgL.VNull(DtTemp.Rows(0)(0))
                    End If
                ElseIf AgL.StrCmp(Dgl1.Item(Col1DeliveryMeasure, mRow).Value, "SQ.METER") Then
                    mQry = "Select MeterArea From Rug_Size Size Left Join Rug_CarpetSku Cs On Size.Code = Cs.Size Where Cs.Code = '" & Dgl1.Item(Col1Item, mRow).Tag & "' "
                    DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
                    If DtTemp.Rows.Count > 0 Then
                        Dgl1.Item(Col1DeliveryMeasurePerPcs, mRow).Value = AgL.VNull(DtTemp.Rows(0)(0))
                    End If
                Else
                    mQry = "Select YardArea From Rug_Size Size Left Join Rug_CarpetSku Cs On Size.Code = Cs.Size Where Cs.Code = '" & Dgl1.Item(Col1Item, mRow).Tag & "' "
                    DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
                    If DtTemp.Rows.Count > 0 Then
                        Dgl1.Item(Col1DeliveryMeasurePerPcs, mRow).Value = AgL.VNull(DtTemp.Rows(0)(0))
                    End If

                    'Dgl1.Item(Col1DeliveryMeasurePerPcs, mRow).Value = Dgl1.Item(Col1MeasurePerPcs, mRow).Value
                    'Dgl1.Item(Col1DeliveryMeasure, mRow).Value = Dgl1.Item(Col1MeasureUnit, mRow).Value
                    'Dgl1.Item(Col1DeliveryMeasure, mRow).Tag = Dgl1.Item(Col1MeasureUnit, mRow).Tag
                End If
            Else

                If Dgl1.Item(Col1MeasureUnit, mRow).Value <> "" And Dgl1.Item(Col1DeliveryMeasure, mRow).Value <> "" Then
                    If Dgl1.Item(Col1MeasureUnit, mRow).Value = Dgl1.Item(Col1DeliveryMeasure, mRow).Value Then
                        Dgl1.Item(Col1DeliveryMeasureMultiplier, mRow).Value = 1
                    Else
                        mQry = " SELECT Multiplier, Rounding FROM UnitConversion WHERE FromUnit = '" & Dgl1.Item(Col1MeasureUnit, mRow).Value & "' AND ToUnit =  '" & Dgl1.Item(Col1DeliveryMeasure, mRow).Value & "' "
                        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
                        With DtTemp
                            If .Rows.Count > 0 Then
                                Dgl1.Item(Col1DeliveryMeasureMultiplier, mRow).Value = AgL.VNull(.Rows(0)("Multiplier"))
                            Else
                                MsgBox("Define Multiplier In Unit Conversion To Convert " & Dgl1.Item(Col1DeliveryMeasure, mRow).Value & " From " & Dgl1.Item(Col1MeasureUnit, mRow).Value & " ", MsgBoxStyle.Information)
                                Dgl1.Item(Col1DeliveryMeasure, mRow).Value = ""
                            End If
                        End With
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub TxtCurrency_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtCurrency.KeyDown, TxtVendor.KeyDown, TxtSalesTaxGroupParty.KeyDown, TxtGodown.KeyDown, TxtBillToParty.KeyDown, TxtProcess.KeyDown
        Try
            If AgL.StrCmp(Topctrl1.Mode, "Browse") Then Exit Sub
            Select Case sender.name
                Case TxtCurrency.Name
                    If TxtCurrency.AgHelpDataSet Is Nothing Then
                        mQry = "SELECT Code, Description AS Currency, IfNull(IsDeleted,0) AS IsDeleted " &
                                " FROM Currency " &
                                " ORDER BY Code "
                        TxtCurrency.AgHelpDataSet(1, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
                    End If

                Case TxtVendor.Name
                    If TxtVendor.AgHelpDataSet Is Nothing Then
                        FCreateHelpSubgroup(sender)
                    End If


                Case TxtBillToParty.Name
                    If CType(sender, AgControls.AgTextBox).AgHelpDataSet Is Nothing Then
                        If e.KeyCode <> Keys.Enter Then
                            mQry = "SELECT Sg.SubCode As Code, Sg.Name || ',' || IfNull(C.CityName,'') As Account_Name " &
                                    " FROM SubGroup Sg " &
                                    " LEFT JOIN City C ON Sg.CityCode = C.CityCode  " &
                                    " Where IfNull(Sg.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' "
                            CType(sender, AgControls.AgTextBox).AgHelpDataSet(0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If


                Case TxtSalesTaxGroupParty.Name
                    If TxtSalesTaxGroupParty.AgHelpDataSet Is Nothing Then
                        mQry = "SELECT Description AS Code, Description, IfNull(Active,0) FROM PostingGroupSalesTaxParty "
                        TxtSalesTaxGroupParty.AgHelpDataSet(1, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
                    End If

                Case TxtGodown.Name
                    If TxtGodown.AgHelpDataSet Is Nothing Then
                        mQry = "SELECT H.Code, H.Description " &
                                " FROM Godown H " &
                                " Where H.Div_Code = '" & TxtDivision.Tag & "' " &
                                " And H.Site_Code = '" & TxtSite_Code.Tag & "' " &
                                " And IfNull(H.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " &
                                " Order By H.Description"
                        TxtGodown.AgHelpDataSet(0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
                    End If

                Case TxtProcess.Name
                    If e.KeyCode <> Keys.Enter Then
                        If TxtProcess.AgHelpDataSet Is Nothing Then
                            mQry = "Select P.NCat As Code, P.Description As Process, P.CostCenter, CCM.Name as CostCenterDesc, P.DefaultBillingType, P.Div_Code " &
                                  " From Process P  " &
                                  " Left Join CostCenterMast CCM On P.CostCenter = CCM.Code " &
                                  " Order By P.Description "
                            TxtProcess.AgHelpDataSet(4, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If

            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FCreateHelpSubgroup(ByVal sender As AgControls.AgTextBox)
        Dim strCond As String = ""
        If DtV_TypeSettings.Rows.Count > 0 Then
            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_AcGroup")) <> "" Then
                strCond += " And CharIndex('|' || H.GroupCode || '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_AcGroup")) & "') > 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_AcGroup")) <> "" Then
                strCond += " And CharIndex('|' || H.GroupCode || '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_AcGroup")) & "') <= 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_SubgroupDivision")) <> "" Then
                strCond += " And CharIndex('|' || H.Div_Code || '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_subGroupDivision")) & "') > 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_SubgroupSite")) <> "" Then
                strCond += " And CharIndex('|' || H.Site_Code || '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_subGroupSite")) & "') > 0 "
            End If
        End If

        strCond += " And H.Nature In ('" & ClsMain.SubGroupNature.Customer & "','" & ClsMain.SubGroupNature.Supplier & "','" & ClsMain.SubGroupNature.Cash & "','" & ClsMain.SubGroupNature.Bank & "')"

        mQry = " SELECT H.SubCode, H.Name || (Case When C.CityName Is Not Null Then ',' || C.CityName Else '' End) AS [Party], " &
                " H.Currency, C1.Description As CurrencyDesc, H.Nature, H.SalesTaxPostingGroup " &
                " FROM SubGroup H  " &
                " LEFT JOIN City C ON H.CityCode = C.CityCode  " &
                " LEFT JOIN Currency C1 On H.Currency = C1.Code " &
                " Where IfNull(H.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
        sender.AgHelpDataSet(4, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
    End Sub

    Private Sub Dgl1_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.RowEnter
        If CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_TransactionHistory")), Boolean) = True Then
            FShowTransactionHistory(Dgl1.Item(Col1Item, e.RowIndex).Tag)
        End If
    End Sub

    Private Sub Dgl1_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Dgl1.Leave
        DGL.Visible = False
    End Sub

    Private Sub FCheckDuplicate(ByVal mRow As Integer)
        Dim I As Integer = 0
        Try
            With Dgl1
                For I = 0 To .Rows.Count - 1
                    If .Item(Col1Item, I).Value <> "" Then
                        If mRow <> I Then
                            If AgL.StrCmp(.Item(Col1Item, I).Value, .Item(Col1Item, mRow).Value) Then
                                If MsgBox("Item " & .Item(Col1Item, I).Value & " Is Already Feeded At Row No " & .Item(ColSNo, I).Value & ".Do You Want To Continue ?", MsgBoxStyle.Information + MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                                    Dgl1.Item(Col1Item, mRow).Tag = "" : Dgl1.Item(Col1Item, mRow).Value = ""
                                End If
                                '.CurrentCell = .Item(Col1Item, I) : Dgl1.Focus()
                                '.Rows.Remove(.Rows(mRow)) : Exit Sub
                            End If
                        End If
                    End If
                Next
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FUpdateDeal(ByVal mRow As Integer, ByVal Conn As SQLiteConnection, ByVal Cmd As SQLiteCommand)
        Dim UPDATEQRY$ = ""

        UPDATEQRY = " UPDATE Item Set " &
                " Deal = (Select L.DEAL From PURCHINVOICEDETAIL L LEFT JOIN PURCHINVOICE H ON L.DOCID = H.DOCID ORDER BY V_DATE DESC Limit 1) " &
                " Where Code = '" & Dgl1.Item(Col1Item, mRow).Tag & "'"
        AgL.Dman_ExecuteNonQry(UPDATEQRY, Conn, Cmd)
    End Sub

    'Private Sub FOpenItemMaster()
    '    Dim FrmObj As Object = Nothing
    '    Dim CFOpen As New ClsFunction
    '    Dim MDI As New MDIMain
    '    Dim DrTemp As DataRow() = Nothing
    '    Dim bRowIndex As Integer = 0, bColumnIndex As Integer = 0
    '    Dim bItemCode$ = ""
    '    Try
    '        bRowIndex = Dgl1.CurrentCell.RowIndex
    '        bColumnIndex = Dgl1.CurrentCell.ColumnIndex

    '        Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
    '            Case Col1Item
    '                FrmObj = CFOpen.FOpen("MnuItemMaster", "Item Master", True)
    '                If FrmObj IsNot Nothing Then
    '                    FrmObj.StartPosition = FormStartPosition.Manual
    '                    FrmObj.IsReturnValue = True
    '                    FrmObj.Top = 50
    '                    FrmObj.ShowDialog()
    '                    bItemCode = FrmObj.mItemCode
    '                    FrmObj = Nothing

    '                    Dgl1.Item(Col1Item, bRowIndex).Value = ""
    '                    Dgl1.Item(Col1Item, bRowIndex).Tag = ""

    '                    Dgl1.CurrentCell = Dgl1.Item(Col1DocQty, bRowIndex)

    '                    mQry = "SELECT I.Code, I.Description, I.ManualCode, I.Specification, I.Unit, I.SalesTaxPostingGroup, I.Measure As MeasurePerPcs, " & _
    '                              " I.MeasureUnit, I.Rate, " & _
    '                              " U.DecimalPlaces As QtyDecimalPlaces, U1.DecimalPlaces As MeasureDecimalPlaces " & _
    '                              " FROM Item I " & _
    '                              " LEFT JOIN Unit U On I.Unit = U.Code " & _
    '                              " LEFT JOIN Unit U1 On I.MeasureUnit = U1.Code " & _
    '                              " Where IfNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' "
    '                    Dgl1.AgHelpDataSet(Col1Item, 7) = AgL.FillData(mQry, AgL.GCn)

    '                    If Dgl1.AgHelpDataSet(Col1Item) IsNot Nothing Then
    '                        DrTemp = Dgl1.AgHelpDataSet(Col1Item).Tables(0).Select("Code = '" & bItemCode & "'")
    '                        If DrTemp.Length > 0 Then
    '                            Dgl1.Item(Col1Item, bRowIndex).Tag = AgL.XNull(DrTemp(0)("Code"))
    '                            Dgl1.Item(Col1Item, bRowIndex).Value = AgL.XNull(DrTemp(0)("Description"))
    '                            Dgl1.Item(Col1ItemCode, bRowIndex).Tag = AgL.XNull(DrTemp(0)("Code"))
    '                            Dgl1.Item(Col1ItemCode, bRowIndex).Value = AgL.XNull(DrTemp(0)("ManualCode"))
    '                            Dgl1.Item(Col1Specification, bRowIndex).Value = AgL.XNull(DrTemp(0)("Specification"))
    '                            Dgl1.Item(Col1Unit, bRowIndex).Value = AgL.XNull(DrTemp(0)("Unit"))
    '                            Dgl1.Item(Col1QtyDecimalPlaces, bRowIndex).Value = AgL.VNull(DrTemp(0)("QtyDecimalPlaces"))
    '                            Dgl1.Item(Col1MeasurePerPcs, bRowIndex).Value = AgL.XNull(DrTemp(0)("MeasurePerPcs"))
    '                            Dgl1.Item(Col1MeasureUnit, bRowIndex).Value = AgL.XNull(DrTemp(0)("MeasureUnit"))
    '                            Dgl1.Item(Col1MeasureDecimalPlaces, bRowIndex).Value = AgL.VNull(DrTemp(0)("MeasureDecimalPlaces"))
    '                            Dgl1.Item(Col1DeliveryMeasure, bRowIndex).Value = AgL.XNull(DrTemp(0)("MeasureUnit"))
    '                            Dgl1.Item(Col1DeliveryMeasureMultiplier, bRowIndex).Value = 1
    '                            Dgl1.Item(Col1Rate, bRowIndex).Value = AgL.XNull(DrTemp(0)("Rate"))
    '                            Dgl1.Item(Col1SalesTaxGroup, bRowIndex).Tag = AgL.XNull(DrTemp(0)("SalesTaxPostingGroup"))
    '                            Dgl1.Item(Col1SalesTaxGroup, bRowIndex).Value = AgL.XNull(DrTemp(0)("SalesTaxPostingGroup"))
    '                            If AgL.StrCmp(Dgl1.AgSelectedValue(Col1SalesTaxGroup, bRowIndex), "") Then
    '                                Dgl1.Item(Col1SalesTaxGroup, bRowIndex).Tag = AgL.XNull(AgL.PubDtEnviro.Rows(0)("DefaultSalesTaxGroupItem"))
    '                                Dgl1.Item(Col1SalesTaxGroup, bRowIndex).Value = AgL.XNull(AgL.PubDtEnviro.Rows(0)("DefaultSalesTaxGroupItem"))
    '                            End If
    '                        End If
    '                    End If
    '                End If
    '        End Select
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub

    Private Sub FGetPurchIndent(ByVal ItemCode As String, ByRef PurchIndent As String)
        mQry = " Select H.DocId From PurchIndent H LEFT JOIN PurchIndentDetail L On H.DocId = L.DocId " &
                " Where L.Item = '" & ItemCode & "' " &
                " And H.V_Date <= '" & TxtV_Date.Text & "' " &
                " Order By H.V_Date  "
        PurchIndent = AgL.XNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar)
    End Sub

    Private Sub FGetPurchChallan(ByVal mRow As Integer)
        Dim FRH_Single As DMHelpGrid.FrmHelpGrid
        Dim StrRtn As String = ""
        Dim DtTemp As DataTable = Nothing

        mQry = " SELECT  L.PurchChallan + Convert(nVarChar,L.PurchChallanSr) As PurchChallanDocIdSr, " &
                " Max(H.V_Type) || '-' ||  Max(H.ReferenceNo) AS ChallanNo, " &
                " Max(H.V_Date) as ChallanDate, Sum(L.Qty) - IfNull(Sum(Cd.Qty), 0) as [Bal.Qty],     " &
                " Max(L.Unit) as Unit  " &
                " FROM ( " &
                "    SELECT PurchChallan.DocID, PurchChallan.V_Type, PurchChallan.ReferenceNo, PurchChallan.V_Date " &
                "    FROM PurchChallan      " &
                "    LEFT JOIN Voucher_Type On PurchChallan.V_Type = Voucher_Type.V_Type    " &
                "    WHERE PurchChallan.Vendor = '" & TxtVendor.Tag & "'   " &
                "    And PurchChallan.Div_Code = '" & TxtDivision.Tag & "'   " &
                "    AND PurchChallan.Site_Code = '" & TxtSite_Code.Tag & "'   " &
                "    AND PurchChallan.V_Date< = '" & TxtV_Date.Text & "' " &
                " ) AS  H     " &
                " LEFT JOIN PurchChallanDetail L  ON H.DocID = L.DocId      " &
                " Left Join (     " &
                "    SELECT L.PurchChallan, L.PurchChallanSr, Sum (L.Qty) AS Qty    " &
                "    FROM PurchInvoiceDetail L       " &
                "    Where L.DocId <> '" & mSearchCode & "'   " &
                "    GROUP BY L.PurchChallan, L.PurchChallanSr   " &
                " ) AS CD ON L.DocId = CD.PurchChallan AND L.Sr = CD.PurchChallanSr   " &
                " LEFT JOIN Unit U On L.Unit = U.Code     " &
                " LEFT JOIN Unit U1 On L.MeasureUnit = U1.Code " &
                " WHERE L.Qty - IfNull(Cd.Qty, 0) > 0    " &
                " And L.Item = '" & Dgl1.Item(Col1Item, mRow).Tag & "' " &
                " GROUP BY L.PurchChallan + Convert(nVarChar,L.PurchChallanSr) "

        If AgL.FillData(mQry, AgL.GCn).Tables(0).Rows.Count > 0 Then
            FRH_Single = New DMHelpGrid.FrmHelpGrid(New DataView(AgL.FillData(mQry, AgL.GCn).TABLES(0)), "", 300, 400, , , False)
            FRH_Single.FFormatColumn(0, , 0, , False)
            FRH_Single.FFormatColumn(1, "Challan No.", 100, DataGridViewContentAlignment.MiddleLeft)
            FRH_Single.FFormatColumn(2, "Challan Date", 100, DataGridViewContentAlignment.MiddleLeft)
            FRH_Single.FFormatColumn(3, "Bal Qty", 70, DataGridViewContentAlignment.MiddleRight)
            FRH_Single.FFormatColumn(4, "Unit", 60, DataGridViewContentAlignment.MiddleLeft)

            FRH_Single.StartPosition = FormStartPosition.CenterScreen
            FRH_Single.ShowDialog()

            If FRH_Single.DRReturn IsNot Nothing Then
                StrRtn = FRH_Single.DRReturn.Item(0)
            Else
                FGetPurchChallan(mRow)
            End If

            mQry = " Select L.DocId, H.V_Type || '-' || H.ReferenceNo as ChallanNo,L.Sr, L.Qty, L.Rate, L.MRP, L.ExpiryDate " &
                    " From PurchChallanDetail L " &
                    " LEFT JOIN PurchChallan H On L.DocId = H.DocId " &
                    " Where L.DocId + Convert(nVarChar,L.Sr) = '" & StrRtn & "'"
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

            If DtTemp.Rows.Count > 0 Then
                Dgl1.Item(Col1PurchChallan, mRow).Tag = AgL.XNull(DtTemp.Rows(0)("DocId"))
                Dgl1.Item(Col1PurchChallan, mRow).Value = AgL.XNull(DtTemp.Rows(0)("ChallanNo"))
                Dgl1.Item(Col1PurchChallanSr, mRow).Value = AgL.XNull(DtTemp.Rows(0)("Sr"))
                Dgl1.Item(Col1DocQty, mRow).Value = AgL.VNull(DtTemp.Rows(0)("Qty"))
                Dgl1.Item(Col1Qty, mRow).Value = AgL.VNull(DtTemp.Rows(0)("Qty"))
                Dgl1.Item(Col1Rate, mRow).Value = AgL.VNull(DtTemp.Rows(0)("Rate"))
                Dgl1.Item(Col1MRP, mRow).Value = AgL.VNull(DtTemp.Rows(0)("MRP"))
            End If
        End If

        FRH_Single = Nothing
    End Sub

    Private Sub TxtVendorDocDate_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtVendorDocDate.Enter
        Try
            Select Case sender.Name
                Case TxtVendorDocDate.Name
                    If TxtVendorDocDate.Text = "" Then
                        TxtVendorDocDate.Text = TxtV_Date.Text
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FCreateHelpItem(ByVal ColumnName As String)
        Dim strCond As String = ""

        Dim ContraV_TypeCondStr As String = ""

        If DtV_TypeSettings.Rows.Count > 0 Then
            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")) <> "" Then
                strCond += " And CharIndex('|' || I.ItemType || '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")) & "') > 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemGroup")) <> "" Then
                strCond += " And CharIndex('|' || I.ItemGroup || '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemGroup")) & "') > 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_ItemGroup")) <> "" Then
                strCond += " And CharIndex('|' || I.ItemGroup || '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_ItemGroup")) & "') <= 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Item")) <> "" Then
                strCond += " And CharIndex('|' || I.Code || '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Item")) & "') > 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_Item")) <> "" Then
                strCond += " And CharIndex('|' || I.Item || '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_Item")) & "') <= 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemDivision")) <> "" Then
                strCond += " And CharIndex('|' || I.Div_Code || '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemDivision")) & "') > 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemSite")) <> "" Then
                strCond += " And CharIndex('|' || I.Site_Code || '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemSite")) & "') > 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ContraV_Type")) <> "" Then
                ContraV_TypeCondStr += " And CharIndex('|' || H.V_Type || '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ContraV_Type")) & "') > 0 "
            End If
        End If

        Select Case ColumnName
            Case Col1Item
                If RbtInvoiceForChallan.Checked = True Then
                    mQry = "SELECT Max(L.Item) As Code, Max(I.Description) as Description, " &
                            " Max(D1.Description) As " & ClsMain.FGetDimension1Caption() & ", " &
                            " Max(D2.Description) As " & ClsMain.FGetDimension2Caption() & ", " &
                            " Max(I.Specification) as Specification, " &
                            " Max(H.V_Type) || '-' ||  Max(H.ReferenceNo) AS ChallanNo,   " &
                            " Max(H.V_Date) as ChallanDate, Sum(L.Qty) - IfNull(Sum(Cd.Qty), 0) as [Bal.Qty],   " &
                            " Sum(L.DocQty) - IfNull(Sum(Cd.DocQty), 0) as [Bal.DocQty],  " &
                            " Sum(L.FreeQty) - IfNull(Sum(Cd.FreeQty), 0) as [Bal.FreeQty],  " &
                            " Max(L.Unit) as Unit, Max(L.Rate) as Rate,  " &
                            " Max(I.ManualCode) as ManualCode,  " &
                            " Max(L.Dimension1) As Dimension1, Max(L.Dimension2) As Dimension2, " &
                            " Max(L.Item_UId) As Item_UId, Max(IU.Item_UId) As Item_UIdDesc, " &
                            " Max(L.SalesTaxGroupItem) SalesTaxPostingGroup, L.PurchChallan, L.PurchChallanSr, " &
                            " Max(L.MeasurePerPcs) As MeasurePerPcs,  Max(L.MeasureUnit) As MeasureUnit, Max(L.Deal) as Deal, Max(L.ProfitMarginPer) as ProfitMarginPer, Max(L.Mrp) as Mrp, Max(L.Sale_Rate) as Sale_Rate, max(L.ExpiryDate) as ExpiryDate, " &
                            " Max(U.DecimalPlaces) As QtyDecimalPlaces, Max(U1.DecimalPlaces) As MeasureDecimalPlaces   " &
                            " FROM (  " &
                            "    SELECT DocID, V_Type, ReferenceNo, V_Date   " &
                            "    FROM PurchChallan    " &
                            "    WHERE Vendor='" & TxtVendor.Tag & "' " &
                            "    And Div_Code = '" & TxtDivision.Tag & "' " &
                            "    AND Site_Code = '" & TxtSite_Code.Tag & "' " &
                            "    AND V_Date<='" & TxtV_Date.Text & "'" &
                            " ) AS  H   " &
                            " LEFT JOIN PurchChallanDetail L  ON H.DocID = L.PurchChallan   " &
                            " Left Join Item I  On L.Item  = I.Code   " &
                            " LEFT JOIN Voucher_Type Vt  ON H.V_Type = Vt.V_Type    " &
                            " Left Join (   " &
                            "    SELECT L.PurchChallan, L.PurchChallanSr, Sum (L.Qty) AS Qty, " &
                            "    Sum (L.DocQty) AS DocQty, Sum (L.FreeQty) AS FreeQty " &
                            "    FROM PurchInvoiceDetail L     " &
                            "    GROUP BY L.PurchChallan, L.PurchChallanSr " &
                            " ) AS CD ON L.DocId = CD.PurchChallan AND L.Sr = CD.PurchChallanSr " &
                            " LEFT JOIN Unit U On L.Unit = U.Code   " &
                            " LEFT JOIN Unit U1 On L.MeasureUnit = U1.Code   " &
                            " Left Join Dimension1 D1 On L.Dimension1 = D1.Code " &
                            " Left Join Dimension2 D2 On L.Dimension2 = D2.Code " &
                            " LEFT JOIN Item_UID IU ON IU.code = L.Item_UID " &
                            " WHERE 1=1  " & strCond &
                            " GROUP BY L.PurchChallan, L.PurchChallanSr " &
                            " Having Max(L.Qty) - Sum(IfNull(Cd.Qty, 0)) > 0 " &
                            " Order By Description, ChallanDate "
                    Dgl1.AgHelpDataSet(Col1Item, 18) = AgL.FillData(mQry, AgL.GCn)
                Else
                    mQry = "SELECT I.Code, I.Description, I.Specification, I.ManualCode, '' As ChallanNo, '' As ChallanDate, " &
                            " 0 As [Bal.DocQty], 0 As [Bal.FreeQty], 0 As [Bal.Qty], I.Unit,0 As Rate, I.SalesTaxPostingGroup , " &
                            " '' As PurchChallan, 0 As PurchChallanSr, " &
                            " I.Measure As MeasurePerPcs, I.MeasureUnit, " &
                            " U.DecimalPlaces as QtyDecimalPlaces, U1.DecimalPlaces as MeasureDecimalPlaces, " &
                            " Null As Item_UId, Null As Item_UIdDesc, " &
                            " Null As PurchChallan, Null As ChallanNo, Null As PurchChallanSr, " &
                            " Null As Dimension1, Null As " & ClsMain.FGetDimension1Caption() & ", Null As Dimension2, Null As " & ClsMain.FGetDimension2Caption() & ", " &
                            " Null As ProfitMarginPer, Null As Sale_Rate, Null As MRP, Null As ExpiryDate, Null As Deal  " &
                            " FROM Item I " &
                            " LEFT JOIN Unit U On I.Unit = U.Code " &
                            " LEFT JOIN Unit U1 On I.MeasureUnit = U1.Code " &
                            " Where IfNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
                    Dgl1.AgHelpDataSet(Col1Item, 30) = AgL.FillData(mQry, AgL.GCn)
                End If
        End Select
    End Sub

    Private Sub FOpenItemMaster(ByVal ColumnIndex As Integer, ByVal RowIndex As Integer)
        Dim DrTemp As DataRow() = Nothing
        Dim bItemCode$ = ""
        bItemCode = AgTemplate.ClsMain.FOpenMaster(Me, "Item Master", TxtV_Type.Tag)
        Dgl1.Item(ColumnIndex, RowIndex).Value = ""
        Dgl1.Item(ColumnIndex, RowIndex).Tag = ""
        Dgl1.CurrentCell = Dgl1.Item(Col1DocQty, RowIndex)
        FCreateHelpItem(Dgl1.Columns(ColumnIndex).Name)
        DrTemp = Dgl1.AgHelpDataSet(ColumnIndex).Tables(0).Select("Code = '" & bItemCode & "'")
        Dgl1.Item(ColumnIndex, RowIndex).Tag = bItemCode
        Dgl1.Item(ColumnIndex, RowIndex).Value = AgL.XNull(AgL.Dman_Execute("Select Description From Item Where Code = '" & Dgl1.Item(ColumnIndex, Dgl1.CurrentCell.RowIndex).Tag & "'", AgL.GCn).ExecuteScalar)
        Validating_ItemCode(ColumnIndex, RowIndex, DrTemp)
        Dgl1.CurrentCell = Dgl1.Item(Col1Item, RowIndex)
        SendKeys.Send("{Enter}")
    End Sub

    Private Sub FShowTransactionHistory(ByVal ItemCode As String)
        mQry = " SELECT L.Item, H.V_Date AS [Purch_Date], Sg.DispName As Vendor, " &
                " L.Rate, L.Qty " &
                " FROM PurchInvoiceDetail L  " &
                " LEFT JOIN  PurchInvoice H ON L.DocId = H.DocId " &
                " LEFT JOIN SubGroup Sg ON H.Vendor = Sg.SubCode " &
                " Where L.Item = '" & ItemCode & "'" &
                " And H.DocId <> '" & mSearchCode & "'" &
                " ORDER BY H.V_Date DESC Limit 5"
        ClsMain.FGetTransactionHistory(Me, mSearchCode, mQry, DGL, DtV_TypeSettings, ItemCode)
    End Sub

    Private Sub FAsignProcess()
        Dim DtTemp As DataTable = Nothing
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
    End Sub
End Class
