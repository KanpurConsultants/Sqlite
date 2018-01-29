Imports System.IO
Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data.SQLite
Public Class FrmPurchChallan
    Inherits AgTemplate.TempTransaction
    Dim mQry$


    Public WithEvents AgCalcGrid1 As New AgStructure.AgCalcGrid
    Public WithEvents AgCustomGrid1 As New AgCustomFields.AgCustomGrid

    Public WithEvents Dgl1 As AgControls.AgDataGrid
    Public Const ColSNo As String = "S.No."
    Public Const Col1ItemCode As String = "Item Code"
    Public Const Col1Item As String = "Item"
    Public Const Col1Dimension1 As String = "Dimension1"
    Public Const Col1Dimension2 As String = "Dimension2"
    Public Const Col1Item_Uid As String = "Item UID"
    Public Const Col1PurchOrder As String = "Purch Order"
    Public Const Col1PurchOrderSr As String = "Purch Order Sr"
    Public Const Col1Specification As String = "Specification"
    Public Const Col1LotNo As String = "Lot No"
    Public Const Col1BaleNo As String = "Bale No"
    Public Const Col1BillingType As String = "Billing Type"
    Public Const Col1DeliveryMeasure As String = "Delivery Measure"
    Public Const Col1SalesTaxGroup As String = "Sales Tax Group Item"
    Public Const Col1DocQty As String = "Doc Qty"
    Public Const Col1FreeQty As String = "Free Qty"
    Public Const Col1RejQty As String = "Rejected Qty"
    Public Const Col1Qty As String = "Qty"
    Public Const Col1Unit As String = "Unit"
    Public Const Col1QtyDecimalPlaces As String = "Qty Decimal Places"
    Public Const Col1MeasurePerPcs As String = "Measure Per Pcs"
    Public Const Col1PcsPerMeasure As String = "Pcs Per Measure"
    Public Const Col1TotalDocMeasure As String = "Total Doc Measure"
    Public Const Col1TotalFreeMeasure As String = "Total Free Measure"
    Public Const Col1TotalRejMeasure As String = "Total Rej Measure"
    Public Const Col1TotalMeasure As String = "Total Measure"
    Public Const Col1MeasureUnit As String = "Measure Unit"
    Public Const Col1MeasureDecimalPlaces As String = "Measure Decimal Places"
    Public Const Col1DeliveryMeasureMultiplier As String = "Delivery Measure Multiplier"
    Public Const Col1TotalDocDeliveryMeasure As String = "Total Doc Delivery Measure"
    Public Const Col1TotalFreeDeliveryMeasure As String = "Total Free Delivery Measure"
    Public Const Col1TotalRejDeliveryMeasure As String = "Total Rej Delivery Measure"
    Public Const Col1DeliveryMeasurePerPcs As String = "Delivery Measure Per Pcs"
    Public Const Col1TotalDeliveryMeasure As String = "Total Delivery Measure"
    Public Const Col1DeliveryMeasureDecimalPlaces As String = "Delivery Measure Decimal Places"
    Public Const Col1Rate As String = "Rate"
    Public Const Col1Deal As String = "Deal"
    Public Const Col1Amount As String = "Amount"
    Public Const Col1Remark As String = "Remark"
    Public Const Col1MRP As String = "MRP"
    Public Const Col1ProfitMarginPer As String = "Profit Margin %"
    Public Const Col1SaleRate As String = "Sale Rate"
    Public Const Col1ExpiryDate As String = "Expiry Date"
    Public Const Col1VNature As String = "VNature"


    Dim Dgl As New AgControls.AgDataGrid
    Dim mPrevRowIndex As Integer = 0
    Dim DtPurchaseEnviro As DataTable
    Public blnIsCarpetTrans As Boolean
    Dim mHelpItemQry$ = ""
    Dim ImportMessegeStr$ = ""
    Dim ImportMode As Boolean = False

    Dim mIsEntryLocked As Boolean = False

    Public Sub New(ByVal StrUPVar As String, ByVal DTUP As DataTable, ByVal StrNCat As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Topctrl1.FSetParent(Me, StrUPVar, DTUP)
        Topctrl1.SetDisp(True)

        EntryNCat = StrNCat

        mQry = "Select H.* from Voucher_Type_Settings H Left Join Voucher_Type Vt On H.V_Type = Vt.V_Type  Where Vt.NCat In ('" & EntryNCat & "') And H.Div_Code = '" & AgL.PubDivCode & "' And H.Site_Code ='" & AgL.PubSiteCode & "' "
        DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
    End Sub

#Region "Form Designer Code"
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmPurchChallan))
        Me.Dgl1 = New AgControls.AgDataGrid
        Me.Label4 = New System.Windows.Forms.Label
        Me.TxtVendor = New AgControls.AgTextBox
        Me.LblVendor = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.LblTotalDeliveryMeasure = New System.Windows.Forms.Label
        Me.LblTotalDeliveryMeasureText = New System.Windows.Forms.Label
        Me.LblTotalMeasure = New System.Windows.Forms.Label
        Me.LblTotalMeasureText = New System.Windows.Forms.Label
        Me.LblTotalQty = New System.Windows.Forms.Label
        Me.LblTotalAmount = New System.Windows.Forms.Label
        Me.LblTotalQtyText = New System.Windows.Forms.Label
        Me.LblTotalAmountText = New System.Windows.Forms.Label
        Me.Pnl1 = New System.Windows.Forms.Panel
        Me.PnlCalcGrid = New System.Windows.Forms.Panel
        Me.TxtStructure = New AgControls.AgTextBox
        Me.Label25 = New System.Windows.Forms.Label
        Me.TxtSalesTaxGroupParty = New AgControls.AgTextBox
        Me.LblSalesTaxGroup = New System.Windows.Forms.Label
        Me.TxtRemarks = New AgControls.AgTextBox
        Me.Label30 = New System.Windows.Forms.Label
        Me.TxtReferenceNo = New AgControls.AgTextBox
        Me.LblReferenceNo = New System.Windows.Forms.Label
        Me.LblVendorDocNo = New System.Windows.Forms.Label
        Me.TxtVendorDocNo = New AgControls.AgTextBox
        Me.LvlVendorDocDate = New System.Windows.Forms.Label
        Me.TxtVendorDocDate = New AgControls.AgTextBox
        Me.LblCurrency = New System.Windows.Forms.Label
        Me.TxtCurrency = New AgControls.AgTextBox
        Me.TxtGodown = New AgControls.AgTextBox
        Me.LblGodown = New System.Windows.Forms.Label
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel
        Me.TxtGateEntryNo = New AgControls.AgTextBox
        Me.LblGateEntryNo = New System.Windows.Forms.Label
        Me.TxtForm = New AgControls.AgTextBox
        Me.LblForm = New System.Windows.Forms.Label
        Me.TxtFormNo = New AgControls.AgTextBox
        Me.LblFormNo = New System.Windows.Forms.Label
        Me.BtnRemoveFilter = New System.Windows.Forms.Button
        Me.LblReferenceNoReq = New System.Windows.Forms.Label
        Me.RbtChallanDirect = New System.Windows.Forms.RadioButton
        Me.RbtChallanForOrder = New System.Windows.Forms.RadioButton
        Me.GrpDirectChallan = New System.Windows.Forms.GroupBox
        Me.BtnFillGateDetail = New System.Windows.Forms.Button
        Me.BtnFillPurchOrder = New System.Windows.Forms.Button
        Me.TxtCustomFields = New AgControls.AgTextBox
        Me.PnlCustomGrid = New System.Windows.Forms.Panel
        Me.Label1 = New System.Windows.Forms.Label
        Me.GBoxImportFromExcel = New System.Windows.Forms.GroupBox
        Me.BtnImprtFromExcel = New System.Windows.Forms.Button
        Me.Label3 = New System.Windows.Forms.Label
        Me.GBoxExcessOrderQtyAllowed = New System.Windows.Forms.GroupBox
        Me.ChkExcessOrderQtyAllowed = New System.Windows.Forms.CheckBox
        Me.BtnFillPartyDetail = New System.Windows.Forms.Button
        Me.TxtNature = New AgControls.AgTextBox
        Me.BtnImportBarCode = New System.Windows.Forms.Button
        Me.TxtProcess = New AgControls.AgTextBox
        Me.LblProcess = New System.Windows.Forms.Label
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
        Me.GrpDirectChallan.SuspendLayout()
        Me.GBoxImportFromExcel.SuspendLayout()
        Me.GBoxExcessOrderQtyAllowed.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.Location = New System.Drawing.Point(830, 558)
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
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(653, 558)
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
        Me.GBoxApprove.Location = New System.Drawing.Point(466, 558)
        Me.GBoxApprove.Size = New System.Drawing.Size(148, 40)
        Me.GBoxApprove.Text = "Approved By"
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
        Me.GBoxEntryType.Location = New System.Drawing.Point(289, 558)
        Me.GBoxEntryType.Size = New System.Drawing.Size(119, 40)
        '
        'TxtEntryType
        '
        Me.TxtEntryType.Location = New System.Drawing.Point(3, 19)
        Me.TxtEntryType.Tag = ""
        '
        'GrpUP
        '
        Me.GrpUP.Location = New System.Drawing.Point(16, 558)
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
        Me.GroupBox1.Location = New System.Drawing.Point(2, 554)
        Me.GroupBox1.Size = New System.Drawing.Size(1002, 4)
        '
        'GBoxDivision
        '
        Me.GBoxDivision.Location = New System.Drawing.Point(672, 558)
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
        Me.LblV_No.Location = New System.Drawing.Point(235, 194)
        Me.LblV_No.Size = New System.Drawing.Size(76, 16)
        Me.LblV_No.Tag = ""
        Me.LblV_No.Text = "Receipt No."
        Me.LblV_No.Visible = False
        '
        'TxtV_No
        '
        Me.TxtV_No.AgSelectedValue = ""
        Me.TxtV_No.BackColor = System.Drawing.Color.White
        Me.TxtV_No.Location = New System.Drawing.Point(343, 193)
        Me.TxtV_No.Size = New System.Drawing.Size(163, 18)
        Me.TxtV_No.TabIndex = 3
        Me.TxtV_No.Tag = ""
        Me.TxtV_No.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.TxtV_No.Visible = False
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(111, 41)
        Me.Label2.Tag = ""
        '
        'LblV_Date
        '
        Me.LblV_Date.BackColor = System.Drawing.Color.Transparent
        Me.LblV_Date.Location = New System.Drawing.Point(15, 36)
        Me.LblV_Date.Size = New System.Drawing.Size(83, 16)
        Me.LblV_Date.Tag = ""
        Me.LblV_Date.Text = "Receipt Date"
        '
        'LblV_TypeReq
        '
        Me.LblV_TypeReq.Location = New System.Drawing.Point(325, 21)
        Me.LblV_TypeReq.Tag = ""
        '
        'TxtV_Date
        '
        Me.TxtV_Date.AgSelectedValue = ""
        Me.TxtV_Date.BackColor = System.Drawing.Color.White
        Me.TxtV_Date.Location = New System.Drawing.Point(127, 35)
        Me.TxtV_Date.TabIndex = 2
        Me.TxtV_Date.Tag = ""
        '
        'LblV_Type
        '
        Me.LblV_Type.Location = New System.Drawing.Point(233, 17)
        Me.LblV_Type.Size = New System.Drawing.Size(83, 16)
        Me.LblV_Type.Tag = ""
        Me.LblV_Type.Text = "Receipt Type"
        '
        'TxtV_Type
        '
        Me.TxtV_Type.AgSelectedValue = ""
        Me.TxtV_Type.BackColor = System.Drawing.Color.White
        Me.TxtV_Type.Location = New System.Drawing.Point(341, 15)
        Me.TxtV_Type.Size = New System.Drawing.Size(207, 18)
        Me.TxtV_Type.TabIndex = 1
        Me.TxtV_Type.Tag = ""
        '
        'LblSite_CodeReq
        '
        Me.LblSite_CodeReq.Location = New System.Drawing.Point(111, 21)
        Me.LblSite_CodeReq.Tag = ""
        '
        'LblSite_Code
        '
        Me.LblSite_Code.BackColor = System.Drawing.Color.Transparent
        Me.LblSite_Code.Location = New System.Drawing.Point(15, 16)
        Me.LblSite_Code.Size = New System.Drawing.Size(87, 16)
        Me.LblSite_Code.Tag = ""
        Me.LblSite_Code.Text = "Branch Name"
        '
        'TxtSite_Code
        '
        Me.TxtSite_Code.AgSelectedValue = ""
        Me.TxtSite_Code.BackColor = System.Drawing.Color.White
        Me.TxtSite_Code.Location = New System.Drawing.Point(127, 15)
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
        Me.LblPrefix.Location = New System.Drawing.Point(295, 194)
        Me.LblPrefix.Tag = ""
        Me.LblPrefix.Visible = False
        '
        'TabControl1
        '
        Me.TabControl1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl1.Location = New System.Drawing.Point(-4, 19)
        Me.TabControl1.Size = New System.Drawing.Size(992, 123)
        Me.TabControl1.TabIndex = 0
        '
        'TP1
        '
        Me.TP1.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.TP1.Controls.Add(Me.BtnFillPartyDetail)
        Me.TP1.Controls.Add(Me.GBoxExcessOrderQtyAllowed)
        Me.TP1.Controls.Add(Me.Label1)
        Me.TP1.Controls.Add(Me.LblReferenceNoReq)
        Me.TP1.Controls.Add(Me.TxtVendorDocNo)
        Me.TP1.Controls.Add(Me.LblVendorDocNo)
        Me.TP1.Controls.Add(Me.TxtReferenceNo)
        Me.TP1.Controls.Add(Me.LblReferenceNo)
        Me.TP1.Controls.Add(Me.TxtStructure)
        Me.TP1.Controls.Add(Me.Label25)
        Me.TP1.Controls.Add(Me.TxtVendorDocDate)
        Me.TP1.Controls.Add(Me.TxtSalesTaxGroupParty)
        Me.TP1.Controls.Add(Me.LblSalesTaxGroup)
        Me.TP1.Controls.Add(Me.LvlVendorDocDate)
        Me.TP1.Controls.Add(Me.Label4)
        Me.TP1.Controls.Add(Me.TxtVendor)
        Me.TP1.Controls.Add(Me.LblVendor)
        Me.TP1.Location = New System.Drawing.Point(4, 22)
        Me.TP1.Size = New System.Drawing.Size(984, 97)
        Me.TP1.Text = "Document Detail"
        Me.TP1.Controls.SetChildIndex(Me.LblVendor, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtVendor, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label4, 0)
        Me.TP1.Controls.SetChildIndex(Me.LvlVendorDocDate, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblSalesTaxGroup, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtSalesTaxGroupParty, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtVendorDocDate, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label25, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtStructure, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblReferenceNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtReferenceNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblVendorDocNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtVendorDocNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_TypeReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtV_Type, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_Type, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblPrefix, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblSite_CodeReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtV_Date, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_No, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label2, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtV_No, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtSite_Code, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_Date, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblDocId, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtDocId, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblSite_Code, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblReferenceNoReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label1, 0)
        Me.TP1.Controls.SetChildIndex(Me.GBoxExcessOrderQtyAllowed, 0)
        Me.TP1.Controls.SetChildIndex(Me.BtnFillPartyDetail, 0)
        '
        'Topctrl1
        '
        Me.Topctrl1.Size = New System.Drawing.Size(984, 41)
        Me.Topctrl1.TabIndex = 11
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
        Me.Label4.Location = New System.Drawing.Point(111, 62)
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
        Me.TxtVendor.Location = New System.Drawing.Point(127, 55)
        Me.TxtVendor.MaxLength = 0
        Me.TxtVendor.Name = "TxtVendor"
        Me.TxtVendor.Size = New System.Drawing.Size(396, 18)
        Me.TxtVendor.TabIndex = 4
        '
        'LblVendor
        '
        Me.LblVendor.AutoSize = True
        Me.LblVendor.BackColor = System.Drawing.Color.Transparent
        Me.LblVendor.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblVendor.Location = New System.Drawing.Point(15, 55)
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
        Me.Panel1.Location = New System.Drawing.Point(2, 372)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(980, 23)
        Me.Panel1.TabIndex = 694
        '
        'LblTotalDeliveryMeasure
        '
        Me.LblTotalDeliveryMeasure.AutoSize = True
        Me.LblTotalDeliveryMeasure.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalDeliveryMeasure.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalDeliveryMeasure.Location = New System.Drawing.Point(888, 3)
        Me.LblTotalDeliveryMeasure.Name = "LblTotalDeliveryMeasure"
        Me.LblTotalDeliveryMeasure.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalDeliveryMeasure.TabIndex = 668
        Me.LblTotalDeliveryMeasure.Text = "."
        Me.LblTotalDeliveryMeasure.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblTotalDeliveryMeasure.Visible = False
        '
        'LblTotalDeliveryMeasureText
        '
        Me.LblTotalDeliveryMeasureText.AutoSize = True
        Me.LblTotalDeliveryMeasureText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalDeliveryMeasureText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalDeliveryMeasureText.Location = New System.Drawing.Point(720, 3)
        Me.LblTotalDeliveryMeasureText.Name = "LblTotalDeliveryMeasureText"
        Me.LblTotalDeliveryMeasureText.Size = New System.Drawing.Size(161, 16)
        Me.LblTotalDeliveryMeasureText.TabIndex = 667
        Me.LblTotalDeliveryMeasureText.Text = "Total Delivery Measure :"
        Me.LblTotalDeliveryMeasureText.Visible = False
        '
        'LblTotalMeasure
        '
        Me.LblTotalMeasure.AutoSize = True
        Me.LblTotalMeasure.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalMeasure.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalMeasure.Location = New System.Drawing.Point(583, 3)
        Me.LblTotalMeasure.Name = "LblTotalMeasure"
        Me.LblTotalMeasure.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalMeasure.TabIndex = 666
        Me.LblTotalMeasure.Text = "."
        Me.LblTotalMeasure.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblTotalMeasure.Visible = False
        '
        'LblTotalMeasureText
        '
        Me.LblTotalMeasureText.AutoSize = True
        Me.LblTotalMeasureText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalMeasureText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalMeasureText.Location = New System.Drawing.Point(472, 3)
        Me.LblTotalMeasureText.Name = "LblTotalMeasureText"
        Me.LblTotalMeasureText.Size = New System.Drawing.Size(105, 16)
        Me.LblTotalMeasureText.TabIndex = 665
        Me.LblTotalMeasureText.Text = "Total Measure :"
        Me.LblTotalMeasureText.Visible = False
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
        Me.LblTotalAmount.Location = New System.Drawing.Point(354, 4)
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
        Me.LblTotalAmountText.Location = New System.Drawing.Point(250, 3)
        Me.LblTotalAmountText.Name = "LblTotalAmountText"
        Me.LblTotalAmountText.Size = New System.Drawing.Size(100, 16)
        Me.LblTotalAmountText.TabIndex = 661
        Me.LblTotalAmountText.Text = "Total Amount :"
        '
        'Pnl1
        '
        Me.Pnl1.Location = New System.Drawing.Point(2, 174)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(980, 198)
        Me.Pnl1.TabIndex = 1
        '
        'PnlCalcGrid
        '
        Me.PnlCalcGrid.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.PnlCalcGrid.Location = New System.Drawing.Point(673, 396)
        Me.PnlCalcGrid.Name = "PnlCalcGrid"
        Me.PnlCalcGrid.Size = New System.Drawing.Size(308, 157)
        Me.PnlCalcGrid.TabIndex = 9
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
        Me.TxtStructure.Location = New System.Drawing.Point(894, 123)
        Me.TxtStructure.MaxLength = 20
        Me.TxtStructure.Name = "TxtStructure"
        Me.TxtStructure.Size = New System.Drawing.Size(79, 18)
        Me.TxtStructure.TabIndex = 14
        Me.TxtStructure.Visible = False
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.BackColor = System.Drawing.Color.Transparent
        Me.Label25.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(827, 123)
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
        Me.TxtSalesTaxGroupParty.Location = New System.Drawing.Point(683, 55)
        Me.TxtSalesTaxGroupParty.MaxLength = 20
        Me.TxtSalesTaxGroupParty.Name = "TxtSalesTaxGroupParty"
        Me.TxtSalesTaxGroupParty.Size = New System.Drawing.Size(183, 18)
        Me.TxtSalesTaxGroupParty.TabIndex = 7
        '
        'LblSalesTaxGroup
        '
        Me.LblSalesTaxGroup.AutoSize = True
        Me.LblSalesTaxGroup.BackColor = System.Drawing.Color.Transparent
        Me.LblSalesTaxGroup.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblSalesTaxGroup.Location = New System.Drawing.Point(559, 55)
        Me.LblSalesTaxGroup.Name = "LblSalesTaxGroup"
        Me.LblSalesTaxGroup.Size = New System.Drawing.Size(104, 16)
        Me.LblSalesTaxGroup.TabIndex = 717
        Me.LblSalesTaxGroup.Text = "Sales Tax Group"
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
        Me.TxtRemarks.Location = New System.Drawing.Point(91, 521)
        Me.TxtRemarks.MaxLength = 255
        Me.TxtRemarks.Name = "TxtRemarks"
        Me.TxtRemarks.Size = New System.Drawing.Size(203, 18)
        Me.TxtRemarks.TabIndex = 8
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(2, 522)
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
        Me.TxtReferenceNo.AgMandatory = True
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
        Me.TxtReferenceNo.Location = New System.Drawing.Point(341, 35)
        Me.TxtReferenceNo.MaxLength = 20
        Me.TxtReferenceNo.Name = "TxtReferenceNo"
        Me.TxtReferenceNo.Size = New System.Drawing.Size(207, 18)
        Me.TxtReferenceNo.TabIndex = 3
        '
        'LblReferenceNo
        '
        Me.LblReferenceNo.AutoSize = True
        Me.LblReferenceNo.BackColor = System.Drawing.Color.Transparent
        Me.LblReferenceNo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblReferenceNo.Location = New System.Drawing.Point(233, 35)
        Me.LblReferenceNo.Name = "LblReferenceNo"
        Me.LblReferenceNo.Size = New System.Drawing.Size(76, 16)
        Me.LblReferenceNo.TabIndex = 731
        Me.LblReferenceNo.Text = "Receipt No."
        '
        'LblVendorDocNo
        '
        Me.LblVendorDocNo.AutoSize = True
        Me.LblVendorDocNo.BackColor = System.Drawing.Color.Transparent
        Me.LblVendorDocNo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblVendorDocNo.Location = New System.Drawing.Point(559, 15)
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
        Me.TxtVendorDocNo.Location = New System.Drawing.Point(683, 15)
        Me.TxtVendorDocNo.MaxLength = 20
        Me.TxtVendorDocNo.Name = "TxtVendorDocNo"
        Me.TxtVendorDocNo.Size = New System.Drawing.Size(183, 18)
        Me.TxtVendorDocNo.TabIndex = 5
        '
        'LvlVendorDocDate
        '
        Me.LvlVendorDocDate.AutoSize = True
        Me.LvlVendorDocDate.BackColor = System.Drawing.Color.Transparent
        Me.LvlVendorDocDate.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LvlVendorDocDate.Location = New System.Drawing.Point(559, 35)
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
        Me.TxtVendorDocDate.Location = New System.Drawing.Point(683, 35)
        Me.TxtVendorDocDate.MaxLength = 20
        Me.TxtVendorDocDate.Name = "TxtVendorDocDate"
        Me.TxtVendorDocDate.Size = New System.Drawing.Size(183, 18)
        Me.TxtVendorDocDate.TabIndex = 6
        '
        'LblCurrency
        '
        Me.LblCurrency.AutoSize = True
        Me.LblCurrency.BackColor = System.Drawing.Color.Transparent
        Me.LblCurrency.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCurrency.Location = New System.Drawing.Point(1, 402)
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
        Me.TxtCurrency.Location = New System.Drawing.Point(91, 401)
        Me.TxtCurrency.MaxLength = 20
        Me.TxtCurrency.Name = "TxtCurrency"
        Me.TxtCurrency.Size = New System.Drawing.Size(203, 18)
        Me.TxtCurrency.TabIndex = 2
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
        Me.TxtGodown.Location = New System.Drawing.Point(91, 421)
        Me.TxtGodown.MaxLength = 0
        Me.TxtGodown.Name = "TxtGodown"
        Me.TxtGodown.Size = New System.Drawing.Size(203, 18)
        Me.TxtGodown.TabIndex = 3
        '
        'LblGodown
        '
        Me.LblGodown.AutoSize = True
        Me.LblGodown.BackColor = System.Drawing.Color.Transparent
        Me.LblGodown.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblGodown.Location = New System.Drawing.Point(1, 421)
        Me.LblGodown.Name = "LblGodown"
        Me.LblGodown.Size = New System.Drawing.Size(55, 16)
        Me.LblGodown.TabIndex = 737
        Me.LblGodown.Text = "Godown"
        '
        'LinkLabel1
        '
        Me.LinkLabel1.BackColor = System.Drawing.Color.SteelBlue
        Me.LinkLabel1.DisabledLinkColor = System.Drawing.Color.White
        Me.LinkLabel1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel1.LinkColor = System.Drawing.Color.White
        Me.LinkLabel1.Location = New System.Drawing.Point(2, 153)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(230, 20)
        Me.LinkLabel1.TabIndex = 738
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "Purchase Challan For Following Items"
        Me.LinkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtGateEntryNo
        '
        Me.TxtGateEntryNo.AgAllowUserToEnableMasterHelp = False
        Me.TxtGateEntryNo.AgLastValueTag = Nothing
        Me.TxtGateEntryNo.AgLastValueText = Nothing
        Me.TxtGateEntryNo.AgMandatory = False
        Me.TxtGateEntryNo.AgMasterHelp = True
        Me.TxtGateEntryNo.AgNumberLeftPlaces = 8
        Me.TxtGateEntryNo.AgNumberNegetiveAllow = False
        Me.TxtGateEntryNo.AgNumberRightPlaces = 2
        Me.TxtGateEntryNo.AgPickFromLastValue = False
        Me.TxtGateEntryNo.AgRowFilter = ""
        Me.TxtGateEntryNo.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtGateEntryNo.AgSelectedValue = Nothing
        Me.TxtGateEntryNo.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtGateEntryNo.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtGateEntryNo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtGateEntryNo.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtGateEntryNo.Location = New System.Drawing.Point(91, 441)
        Me.TxtGateEntryNo.MaxLength = 0
        Me.TxtGateEntryNo.Name = "TxtGateEntryNo"
        Me.TxtGateEntryNo.Size = New System.Drawing.Size(172, 18)
        Me.TxtGateEntryNo.TabIndex = 4
        '
        'LblGateEntryNo
        '
        Me.LblGateEntryNo.AutoSize = True
        Me.LblGateEntryNo.BackColor = System.Drawing.Color.Transparent
        Me.LblGateEntryNo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblGateEntryNo.Location = New System.Drawing.Point(1, 442)
        Me.LblGateEntryNo.Name = "LblGateEntryNo"
        Me.LblGateEntryNo.Size = New System.Drawing.Size(95, 16)
        Me.LblGateEntryNo.TabIndex = 740
        Me.LblGateEntryNo.Text = "Gate Entry No."
        '
        'TxtForm
        '
        Me.TxtForm.AgAllowUserToEnableMasterHelp = False
        Me.TxtForm.AgLastValueTag = Nothing
        Me.TxtForm.AgLastValueText = Nothing
        Me.TxtForm.AgMandatory = False
        Me.TxtForm.AgMasterHelp = False
        Me.TxtForm.AgNumberLeftPlaces = 8
        Me.TxtForm.AgNumberNegetiveAllow = False
        Me.TxtForm.AgNumberRightPlaces = 2
        Me.TxtForm.AgPickFromLastValue = False
        Me.TxtForm.AgRowFilter = ""
        Me.TxtForm.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtForm.AgSelectedValue = Nothing
        Me.TxtForm.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtForm.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtForm.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtForm.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtForm.Location = New System.Drawing.Point(91, 461)
        Me.TxtForm.MaxLength = 0
        Me.TxtForm.Name = "TxtForm"
        Me.TxtForm.Size = New System.Drawing.Size(203, 18)
        Me.TxtForm.TabIndex = 5
        '
        'LblForm
        '
        Me.LblForm.AutoSize = True
        Me.LblForm.BackColor = System.Drawing.Color.Transparent
        Me.LblForm.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblForm.Location = New System.Drawing.Point(1, 462)
        Me.LblForm.Name = "LblForm"
        Me.LblForm.Size = New System.Drawing.Size(38, 16)
        Me.LblForm.TabIndex = 744
        Me.LblForm.Text = "Form"
        '
        'TxtFormNo
        '
        Me.TxtFormNo.AgAllowUserToEnableMasterHelp = False
        Me.TxtFormNo.AgLastValueTag = Nothing
        Me.TxtFormNo.AgLastValueText = Nothing
        Me.TxtFormNo.AgMandatory = False
        Me.TxtFormNo.AgMasterHelp = False
        Me.TxtFormNo.AgNumberLeftPlaces = 8
        Me.TxtFormNo.AgNumberNegetiveAllow = False
        Me.TxtFormNo.AgNumberRightPlaces = 2
        Me.TxtFormNo.AgPickFromLastValue = False
        Me.TxtFormNo.AgRowFilter = ""
        Me.TxtFormNo.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtFormNo.AgSelectedValue = Nothing
        Me.TxtFormNo.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtFormNo.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtFormNo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtFormNo.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFormNo.Location = New System.Drawing.Point(91, 481)
        Me.TxtFormNo.MaxLength = 0
        Me.TxtFormNo.Name = "TxtFormNo"
        Me.TxtFormNo.Size = New System.Drawing.Size(172, 18)
        Me.TxtFormNo.TabIndex = 6
        '
        'LblFormNo
        '
        Me.LblFormNo.AutoSize = True
        Me.LblFormNo.BackColor = System.Drawing.Color.Transparent
        Me.LblFormNo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblFormNo.Location = New System.Drawing.Point(1, 482)
        Me.LblFormNo.Name = "LblFormNo"
        Me.LblFormNo.Size = New System.Drawing.Size(62, 16)
        Me.LblFormNo.TabIndex = 746
        Me.LblFormNo.Text = "Form No."
        '
        'BtnRemoveFilter
        '
        Me.BtnRemoveFilter.BackColor = System.Drawing.Color.White
        Me.BtnRemoveFilter.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.BtnRemoveFilter.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnRemoveFilter.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.BtnRemoveFilter.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnRemoveFilter.Location = New System.Drawing.Point(264, 480)
        Me.BtnRemoveFilter.Name = "BtnRemoveFilter"
        Me.BtnRemoveFilter.Size = New System.Drawing.Size(30, 21)
        Me.BtnRemoveFilter.TabIndex = 749
        Me.BtnRemoveFilter.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnRemoveFilter.UseVisualStyleBackColor = False
        '
        'LblReferenceNoReq
        '
        Me.LblReferenceNoReq.AutoSize = True
        Me.LblReferenceNoReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblReferenceNoReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblReferenceNoReq.Location = New System.Drawing.Point(325, 42)
        Me.LblReferenceNoReq.Name = "LblReferenceNoReq"
        Me.LblReferenceNoReq.Size = New System.Drawing.Size(10, 7)
        Me.LblReferenceNoReq.TabIndex = 738
        Me.LblReferenceNoReq.Text = "Ä"
        '
        'RbtChallanDirect
        '
        Me.RbtChallanDirect.AutoSize = True
        Me.RbtChallanDirect.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RbtChallanDirect.Location = New System.Drawing.Point(153, 8)
        Me.RbtChallanDirect.Name = "RbtChallanDirect"
        Me.RbtChallanDirect.Size = New System.Drawing.Size(116, 17)
        Me.RbtChallanDirect.TabIndex = 743
        Me.RbtChallanDirect.TabStop = True
        Me.RbtChallanDirect.Text = "Challan Direct"
        Me.RbtChallanDirect.UseVisualStyleBackColor = True
        '
        'RbtChallanForOrder
        '
        Me.RbtChallanForOrder.AutoSize = True
        Me.RbtChallanForOrder.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RbtChallanForOrder.Location = New System.Drawing.Point(5, 8)
        Me.RbtChallanForOrder.Name = "RbtChallanForOrder"
        Me.RbtChallanForOrder.Size = New System.Drawing.Size(140, 17)
        Me.RbtChallanForOrder.TabIndex = 0
        Me.RbtChallanForOrder.TabStop = True
        Me.RbtChallanForOrder.Text = "Challan For Order"
        Me.RbtChallanForOrder.UseVisualStyleBackColor = True
        '
        'GrpDirectChallan
        '
        Me.GrpDirectChallan.Controls.Add(Me.RbtChallanForOrder)
        Me.GrpDirectChallan.Controls.Add(Me.RbtChallanDirect)
        Me.GrpDirectChallan.Location = New System.Drawing.Point(237, 146)
        Me.GrpDirectChallan.Name = "GrpDirectChallan"
        Me.GrpDirectChallan.Size = New System.Drawing.Size(274, 28)
        Me.GrpDirectChallan.TabIndex = 1
        Me.GrpDirectChallan.TabStop = False
        '
        'BtnFillGateDetail
        '
        Me.BtnFillGateDetail.BackColor = System.Drawing.Color.White
        Me.BtnFillGateDetail.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.BtnFillGateDetail.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnFillGateDetail.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(162, Byte))
        Me.BtnFillGateDetail.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnFillGateDetail.Location = New System.Drawing.Point(264, 438)
        Me.BtnFillGateDetail.Name = "BtnFillGateDetail"
        Me.BtnFillGateDetail.Size = New System.Drawing.Size(30, 21)
        Me.BtnFillGateDetail.TabIndex = 757
        Me.BtnFillGateDetail.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnFillGateDetail.UseVisualStyleBackColor = False
        '
        'BtnFillPurchOrder
        '
        Me.BtnFillPurchOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnFillPurchOrder.Font = New System.Drawing.Font("Verdana", 6.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnFillPurchOrder.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnFillPurchOrder.Location = New System.Drawing.Point(515, 153)
        Me.BtnFillPurchOrder.Margin = New System.Windows.Forms.Padding(0)
        Me.BtnFillPurchOrder.Name = "BtnFillPurchOrder"
        Me.BtnFillPurchOrder.Size = New System.Drawing.Size(24, 19)
        Me.BtnFillPurchOrder.TabIndex = 1
        Me.BtnFillPurchOrder.TabStop = False
        Me.BtnFillPurchOrder.Text = "..."
        Me.BtnFillPurchOrder.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnFillPurchOrder.UseVisualStyleBackColor = True
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
        Me.TxtCustomFields.Location = New System.Drawing.Point(457, 573)
        Me.TxtCustomFields.MaxLength = 20
        Me.TxtCustomFields.Name = "TxtCustomFields"
        Me.TxtCustomFields.Size = New System.Drawing.Size(72, 18)
        Me.TxtCustomFields.TabIndex = 1013
        Me.TxtCustomFields.Text = "AgTextBox1"
        Me.TxtCustomFields.Visible = False
        '
        'PnlCustomGrid
        '
        Me.PnlCustomGrid.Location = New System.Drawing.Point(300, 396)
        Me.PnlCustomGrid.Name = "PnlCustomGrid"
        Me.PnlCustomGrid.Size = New System.Drawing.Size(368, 158)
        Me.PnlCustomGrid.TabIndex = 10
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(667, 62)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(10, 7)
        Me.Label1.TabIndex = 739
        Me.Label1.Text = "Ä"
        '
        'GBoxImportFromExcel
        '
        Me.GBoxImportFromExcel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GBoxImportFromExcel.BackColor = System.Drawing.Color.Transparent
        Me.GBoxImportFromExcel.Controls.Add(Me.BtnImprtFromExcel)
        Me.GBoxImportFromExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GBoxImportFromExcel.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GBoxImportFromExcel.ForeColor = System.Drawing.Color.Maroon
        Me.GBoxImportFromExcel.Location = New System.Drawing.Point(193, 554)
        Me.GBoxImportFromExcel.Name = "GBoxImportFromExcel"
        Me.GBoxImportFromExcel.Size = New System.Drawing.Size(90, 55)
        Me.GBoxImportFromExcel.TabIndex = 1016
        Me.GBoxImportFromExcel.TabStop = False
        Me.GBoxImportFromExcel.Tag = "UP"
        Me.GBoxImportFromExcel.Text = "Import From Excel"
        Me.GBoxImportFromExcel.Visible = False
        '
        'BtnImprtFromExcel
        '
        Me.BtnImprtFromExcel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnImprtFromExcel.Image = CType(resources.GetObject("BtnImprtFromExcel.Image"), System.Drawing.Image)
        Me.BtnImprtFromExcel.Location = New System.Drawing.Point(53, 14)
        Me.BtnImprtFromExcel.Name = "BtnImprtFromExcel"
        Me.BtnImprtFromExcel.Size = New System.Drawing.Size(36, 34)
        Me.BtnImprtFromExcel.TabIndex = 670
        Me.BtnImprtFromExcel.TabStop = False
        Me.BtnImprtFromExcel.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(77, 426)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(10, 7)
        Me.Label3.TabIndex = 740
        Me.Label3.Text = "Ä"
        '
        'GBoxExcessOrderQtyAllowed
        '
        Me.GBoxExcessOrderQtyAllowed.BackColor = System.Drawing.Color.Transparent
        Me.GBoxExcessOrderQtyAllowed.Controls.Add(Me.ChkExcessOrderQtyAllowed)
        Me.GBoxExcessOrderQtyAllowed.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GBoxExcessOrderQtyAllowed.Font = New System.Drawing.Font("Courier New", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GBoxExcessOrderQtyAllowed.ForeColor = System.Drawing.Color.Maroon
        Me.GBoxExcessOrderQtyAllowed.Location = New System.Drawing.Point(876, 15)
        Me.GBoxExcessOrderQtyAllowed.Name = "GBoxExcessOrderQtyAllowed"
        Me.GBoxExcessOrderQtyAllowed.Size = New System.Drawing.Size(96, 53)
        Me.GBoxExcessOrderQtyAllowed.TabIndex = 749
        Me.GBoxExcessOrderQtyAllowed.TabStop = False
        Me.GBoxExcessOrderQtyAllowed.Tag = "UP"
        Me.GBoxExcessOrderQtyAllowed.Text = "Excess Order Qty Allowed"
        '
        'ChkExcessOrderQtyAllowed
        '
        Me.ChkExcessOrderQtyAllowed.AutoSize = True
        Me.ChkExcessOrderQtyAllowed.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkExcessOrderQtyAllowed.ForeColor = System.Drawing.Color.Black
        Me.ChkExcessOrderQtyAllowed.Location = New System.Drawing.Point(75, 17)
        Me.ChkExcessOrderQtyAllowed.Name = "ChkExcessOrderQtyAllowed"
        Me.ChkExcessOrderQtyAllowed.Size = New System.Drawing.Size(15, 14)
        Me.ChkExcessOrderQtyAllowed.TabIndex = 0
        Me.ChkExcessOrderQtyAllowed.UseVisualStyleBackColor = True
        '
        'BtnFillPartyDetail
        '
        Me.BtnFillPartyDetail.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnFillPartyDetail.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnFillPartyDetail.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnFillPartyDetail.Location = New System.Drawing.Point(522, 54)
        Me.BtnFillPartyDetail.Margin = New System.Windows.Forms.Padding(0)
        Me.BtnFillPartyDetail.Name = "BtnFillPartyDetail"
        Me.BtnFillPartyDetail.Size = New System.Drawing.Size(26, 20)
        Me.BtnFillPartyDetail.TabIndex = 3008
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
        Me.TxtNature.Location = New System.Drawing.Point(606, 154)
        Me.TxtNature.MaxLength = 20
        Me.TxtNature.Name = "TxtNature"
        Me.TxtNature.Size = New System.Drawing.Size(81, 18)
        Me.TxtNature.TabIndex = 1209
        Me.TxtNature.Text = "TxtNature"
        Me.TxtNature.Visible = False
        '
        'BtnImportBarCode
        '
        Me.BtnImportBarCode.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnImportBarCode.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnImportBarCode.ForeColor = System.Drawing.Color.Black
        Me.BtnImportBarCode.Location = New System.Drawing.Point(861, 148)
        Me.BtnImportBarCode.Margin = New System.Windows.Forms.Padding(0)
        Me.BtnImportBarCode.Name = "BtnImportBarCode"
        Me.BtnImportBarCode.Size = New System.Drawing.Size(121, 25)
        Me.BtnImportBarCode.TabIndex = 1210
        Me.BtnImportBarCode.TabStop = False
        Me.BtnImportBarCode.Text = "Import BarCode"
        Me.BtnImportBarCode.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnImportBarCode.UseVisualStyleBackColor = True
        '
        'TxtProcess
        '
        Me.TxtProcess.AgAllowUserToEnableMasterHelp = False
        Me.TxtProcess.AgLastValueTag = Nothing
        Me.TxtProcess.AgLastValueText = Nothing
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
        Me.TxtProcess.Location = New System.Drawing.Point(91, 501)
        Me.TxtProcess.MaxLength = 0
        Me.TxtProcess.Name = "TxtProcess"
        Me.TxtProcess.Size = New System.Drawing.Size(203, 18)
        Me.TxtProcess.TabIndex = 7
        '
        'LblProcess
        '
        Me.LblProcess.AutoSize = True
        Me.LblProcess.BackColor = System.Drawing.Color.Transparent
        Me.LblProcess.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblProcess.Location = New System.Drawing.Point(1, 502)
        Me.LblProcess.Name = "LblProcess"
        Me.LblProcess.Size = New System.Drawing.Size(56, 16)
        Me.LblProcess.TabIndex = 1212
        Me.LblProcess.Text = "Process"
        '
        'FrmPurchChallan
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.BackColor = System.Drawing.SystemColors.ButtonShadow
        Me.ClientSize = New System.Drawing.Size(984, 599)
        Me.Controls.Add(Me.TxtProcess)
        Me.Controls.Add(Me.LblProcess)
        Me.Controls.Add(Me.BtnImportBarCode)
        Me.Controls.Add(Me.TxtNature)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.PnlCustomGrid)
        Me.Controls.Add(Me.TxtCustomFields)
        Me.Controls.Add(Me.BtnFillPurchOrder)
        Me.Controls.Add(Me.BtnFillGateDetail)
        Me.Controls.Add(Me.BtnRemoveFilter)
        Me.Controls.Add(Me.TxtFormNo)
        Me.Controls.Add(Me.LblFormNo)
        Me.Controls.Add(Me.TxtForm)
        Me.Controls.Add(Me.LblForm)
        Me.Controls.Add(Me.TxtGateEntryNo)
        Me.Controls.Add(Me.LblGateEntryNo)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.PnlCalcGrid)
        Me.Controls.Add(Me.Pnl1)
        Me.Controls.Add(Me.TxtGodown)
        Me.Controls.Add(Me.LblGodown)
        Me.Controls.Add(Me.GrpDirectChallan)
        Me.Controls.Add(Me.GBoxImportFromExcel)
        Me.Controls.Add(Me.TxtRemarks)
        Me.Controls.Add(Me.Label30)
        Me.Controls.Add(Me.TxtCurrency)
        Me.Controls.Add(Me.LblCurrency)
        Me.Name = "FrmPurchChallan"
        Me.Text = "Purchase Challan"
        Me.Controls.SetChildIndex(Me.LblCurrency, 0)
        Me.Controls.SetChildIndex(Me.TxtCurrency, 0)
        Me.Controls.SetChildIndex(Me.Label30, 0)
        Me.Controls.SetChildIndex(Me.TxtRemarks, 0)
        Me.Controls.SetChildIndex(Me.GBoxImportFromExcel, 0)
        Me.Controls.SetChildIndex(Me.GrpDirectChallan, 0)
        Me.Controls.SetChildIndex(Me.LblGodown, 0)
        Me.Controls.SetChildIndex(Me.TxtGodown, 0)
        Me.Controls.SetChildIndex(Me.Pnl1, 0)
        Me.Controls.SetChildIndex(Me.PnlCalcGrid, 0)
        Me.Controls.SetChildIndex(Me.Panel1, 0)
        Me.Controls.SetChildIndex(Me.LinkLabel1, 0)
        Me.Controls.SetChildIndex(Me.LblGateEntryNo, 0)
        Me.Controls.SetChildIndex(Me.TxtGateEntryNo, 0)
        Me.Controls.SetChildIndex(Me.LblForm, 0)
        Me.Controls.SetChildIndex(Me.TxtForm, 0)
        Me.Controls.SetChildIndex(Me.LblFormNo, 0)
        Me.Controls.SetChildIndex(Me.TxtFormNo, 0)
        Me.Controls.SetChildIndex(Me.BtnRemoveFilter, 0)
        Me.Controls.SetChildIndex(Me.BtnFillGateDetail, 0)
        Me.Controls.SetChildIndex(Me.BtnFillPurchOrder, 0)
        Me.Controls.SetChildIndex(Me.TxtCustomFields, 0)
        Me.Controls.SetChildIndex(Me.PnlCustomGrid, 0)
        Me.Controls.SetChildIndex(Me.Label3, 0)
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
        Me.Controls.SetChildIndex(Me.BtnImportBarCode, 0)
        Me.Controls.SetChildIndex(Me.LblProcess, 0)
        Me.Controls.SetChildIndex(Me.TxtProcess, 0)
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
        Me.GrpDirectChallan.ResumeLayout(False)
        Me.GrpDirectChallan.PerformLayout()
        Me.GBoxImportFromExcel.ResumeLayout(False)
        Me.GBoxExcessOrderQtyAllowed.ResumeLayout(False)
        Me.GBoxExcessOrderQtyAllowed.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents LblVendor As System.Windows.Forms.Label
    Public WithEvents TxtVendor As AgControls.AgTextBox
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Panel1 As System.Windows.Forms.Panel
    Public WithEvents LblTotalQty As System.Windows.Forms.Label
    Public WithEvents LblTotalQtyText As System.Windows.Forms.Label
    Public WithEvents Pnl1 As System.Windows.Forms.Panel
    Public WithEvents PnlCalcGrid As System.Windows.Forms.Panel
    Public WithEvents TxtStructure As AgControls.AgTextBox
    Public WithEvents Label25 As System.Windows.Forms.Label
    Public WithEvents TxtSalesTaxGroupParty As AgControls.AgTextBox
    Public WithEvents LblSalesTaxGroup As System.Windows.Forms.Label
    Public WithEvents LblTotalAmount As System.Windows.Forms.Label
    Public WithEvents LblTotalAmountText As System.Windows.Forms.Label
    Public WithEvents TxtRemarks As AgControls.AgTextBox
    Public WithEvents Label30 As System.Windows.Forms.Label
    Public WithEvents LblTotalMeasure As System.Windows.Forms.Label
    Public WithEvents LblTotalMeasureText As System.Windows.Forms.Label
    Public WithEvents TxtReferenceNo As AgControls.AgTextBox
    Public WithEvents LblReferenceNo As System.Windows.Forms.Label
    Public WithEvents TxtCurrency As AgControls.AgTextBox
    Public WithEvents LblCurrency As System.Windows.Forms.Label
    Public WithEvents TxtVendorDocDate As AgControls.AgTextBox
    Public WithEvents LvlVendorDocDate As System.Windows.Forms.Label
    Public WithEvents TxtVendorDocNo As AgControls.AgTextBox
    Public WithEvents LblVendorDocNo As System.Windows.Forms.Label
    Public WithEvents TxtGodown As AgControls.AgTextBox
    Public WithEvents LblGodown As System.Windows.Forms.Label
    Public WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    Public WithEvents TxtGateEntryNo As AgControls.AgTextBox
    Public WithEvents LblGateEntryNo As System.Windows.Forms.Label
    Public WithEvents TxtForm As AgControls.AgTextBox
    Public WithEvents LblForm As System.Windows.Forms.Label
    Public WithEvents TxtFormNo As AgControls.AgTextBox
    Public WithEvents LblFormNo As System.Windows.Forms.Label
    Public WithEvents BtnRemoveFilter As System.Windows.Forms.Button
    Public WithEvents LblReferenceNoReq As System.Windows.Forms.Label
    Public WithEvents RbtChallanDirect As System.Windows.Forms.RadioButton
    Public WithEvents RbtChallanForOrder As System.Windows.Forms.RadioButton
    Public WithEvents GrpDirectChallan As System.Windows.Forms.GroupBox
    Public WithEvents BtnFillGateDetail As System.Windows.Forms.Button
    Public WithEvents BtnFillPurchOrder As System.Windows.Forms.Button
    Public WithEvents TxtCustomFields As AgControls.AgTextBox
    Public WithEvents PnlCustomGrid As System.Windows.Forms.Panel
    Public WithEvents LblTotalDeliveryMeasure As System.Windows.Forms.Label
    Public WithEvents LblTotalDeliveryMeasureText As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents GBoxImportFromExcel As System.Windows.Forms.GroupBox
    Public WithEvents BtnImprtFromExcel As System.Windows.Forms.Button
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents GBoxExcessOrderQtyAllowed As System.Windows.Forms.GroupBox
    Public WithEvents ChkExcessOrderQtyAllowed As System.Windows.Forms.CheckBox
    Public WithEvents BtnFillPartyDetail As System.Windows.Forms.Button
    Public WithEvents TxtNature As AgControls.AgTextBox
    Public WithEvents BtnImportBarCode As System.Windows.Forms.Button
    Public WithEvents TxtProcess As AgControls.AgTextBox
    Public WithEvents LblProcess As System.Windows.Forms.Label
#End Region

    Private Sub FrmQuality1_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "PurchChallan"
        LogTableName = "PurchChallan_Log"
        MainLineTableCsv = "PurchChallanDetail"
        LogLineTableCsv = "PurchChallanDetail_LOG"

        AgL.GridDesign(Dgl1)
        AgL.AddAgDataGrid(AgCalcGrid1, PnlCalcGrid)

        AgCalcGrid1.AgLibVar = AgL
        AgCalcGrid1.Visible = False

        AgL.AddAgDataGrid(AgCustomGrid1, PnlCustomGrid)

        AgCustomGrid1.AgLibVar = AgL
        AgCustomGrid1.SplitGrid = True
        AgCustomGrid1.MnuText = Me.Name
    End Sub

    Private Sub FrmPurchInvoice_BaseEvent_ApproveDeletion_InTrans(ByVal SearchCode As String, ByVal Conn As SqliteConnection, ByVal Cmd As SqliteCommand) Handles Me.BaseEvent_ApproveDeletion_InTrans
        mQry = " Delete From Stock Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        mQry = " Delete From StockVirtual Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Delete From JobIssRecUid Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " UPDATE JobIssRecUid Set JobRecDocID = Null Where JobRecDocID = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " UPDATE Item_UID SET " &
              " RecDocID = Null, " &
              " RecSr = Null, " &
              " Item_ManualUID = Null,  " &
              " IsInStock = 0 " &
              " WHERE RecDocId = '" & mInternalCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

    End Sub

    Private Sub FrmQuality1_BaseFunction_FIniMast(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMast
        Dim mCondStr$

        mCondStr = "  And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " " &
                        " And IfNull(H.Div_Code,'" & AgL.PubDivCode & "') = '" & AgL.PubDivCode & "' "
        mCondStr = mCondStr & " And Vt.NCat In ('" & EntryNCat & "')"

        If IsApplyVTypePermission Then
            mCondStr = mCondStr & " And H.V_Type In (Select V_Type From User_VType_Permission VP Where VP.UserName = '" & AgL.PubUserName & "' And VP.Div_Code = '" & AgL.PubDivCode & "' And VP.Site_Code = '" & AgL.PubSiteCode & "') "
        End If

        mQry = "Select DocID As SearchCode " &
                " From PurchChallan H " &
                " Left Join Voucher_Type Vt On H.V_Type = Vt.V_Type  " &
                " Where 1=1 " & mCondStr & "  Order By V_Date Desc "
        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub FrmSaleOrder_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mCondStr$

        mCondStr = " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "H.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat In ('" & EntryNCat & "')"

        If IsApplyVTypePermission Then
            mCondStr = mCondStr & " And H.V_Type In (Select V_Type From User_VType_Permission VP Where VP.UserName = '" & AgL.PubUserName & "' And VP.Div_Code = '" & AgL.PubDivCode & "' And VP.Site_Code = '" & AgL.PubSiteCode & "') "
        End If

        AgL.PubFindQry = " SELECT H.DocID AS SearchCode, H.V_Type AS [Challan_Type], H.V_Date AS [Challan_Date], " &
                        " H.ReferenceNo AS [Manual_No], Sg.Name As Supplier, H.Currency, H.SalesTaxGroupParty AS [Sales_Tax_Group_Party], " &
                        " H.VendorDocNo AS [Vendor_Doc_No], H.VendorDocDate AS [Vendor_Doc_Date], H.Remarks " &
                        " FROM PurchChallan H  " &
                        " Left Join SubGroup Sg On H.Vendor = Sg.SubCode " &
                        " Left Join Voucher_Type Vt On H.V_Type = Vt.V_Type  " &
                        " Where 1=1 " & mCondStr
        AgL.PubFindQryOrdBy = "[Entry_Date]"
    End Sub

    Private Sub FrmSaleOrder_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl1, Col1Item_Uid, 60, 0, Col1Item_Uid, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_ItemUID")), Boolean), False)
            .AddAgTextColumn(Dgl1, Col1ItemCode, 100, 0, Col1ItemCode, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_ItemCode")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_ItemCode")), Boolean))
            .AddAgTextColumn(Dgl1, Col1Item, 200, 0, Col1Item, True, Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_ItemName")), Boolean))
            .AddAgTextColumn(Dgl1, Col1Dimension1, 100, 0, ClsMain.FGetDimension1Caption(), CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Dimension1")), Boolean), False)
            .AddAgTextColumn(Dgl1, Col1Dimension2, 100, 0, ClsMain.FGetDimension2Caption(), CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Dimension2")), Boolean), False)
            .AddAgTextColumn(Dgl1, Col1Specification, 100, 255, Col1Specification, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Specification")), Boolean), False, False)
            .AddAgTextColumn(Dgl1, Col1PurchOrder, 80, 0, Col1PurchOrder, True, True)
            .AddAgTextColumn(Dgl1, Col1PurchOrderSr, 80, 0, Col1PurchOrderSr, False, True)
            .AddAgTextColumn(Dgl1, Col1LotNo, 80, 20, Col1LotNo, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_LotNo")), Boolean), False)
            .AddAgTextColumn(Dgl1, Col1BaleNo, 50, 0, Col1BaleNo, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_BaleNo")), Boolean), False)
            .AddAgTextColumn(Dgl1, Col1SalesTaxGroup, 120, 0, Col1SalesTaxGroup, False, False)
            .AddAgTextColumn(Dgl1, Col1BillingType, 50, 255, Col1BillingType, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_BillingType")), Boolean), False)
            .AddAgTextColumn(Dgl1, Col1DeliveryMeasure, 70, 50, Col1DeliveryMeasure, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_MeasureUnit")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_MeasureUnit")), Boolean), False)
            .AddAgNumberColumn(Dgl1, Col1DocQty, 60, 8, 3, False, Col1DocQty, True, False, True)
            .AddAgNumberColumn(Dgl1, Col1FreeQty, 60, 8, 3, False, Col1FreeQty, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_FreeQty")), Boolean), False, True)
            .AddAgNumberColumn(Dgl1, Col1RejQty, 60, 8, 3, False, Col1RejQty, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_RejQty")), Boolean), False, True)
            .AddAgNumberColumn(Dgl1, Col1Qty, 60, 8, 3, False, Col1Qty, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Qty")), Boolean), True, True)
            .AddAgTextColumn(Dgl1, Col1Unit, 50, 0, Col1Unit, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Unit")), Boolean), True)
            .AddAgTextColumn(Dgl1, Col1QtyDecimalPlaces, 50, 0, Col1QtyDecimalPlaces, False, True, False)
            .AddAgNumberColumn(Dgl1, Col1MeasurePerPcs, 70, 8, 3, False, Col1MeasurePerPcs, False, False, True)
            .AddAgNumberColumn(Dgl1, Col1PcsPerMeasure, 70, 8, 3, False, Col1PcsPerMeasure, False, False, True)
            .AddAgNumberColumn(Dgl1, Col1TotalDocMeasure, 70, 8, 3, False, Col1TotalDocMeasure, False, False, True)
            .AddAgNumberColumn(Dgl1, Col1TotalFreeMeasure, 70, 8, 3, False, Col1TotalFreeMeasure, False, False, True)
            .AddAgNumberColumn(Dgl1, Col1TotalRejMeasure, 70, 8, 3, False, Col1TotalRejMeasure, False, False, True)
            .AddAgNumberColumn(Dgl1, Col1TotalMeasure, 70, 8, 3, False, Col1TotalMeasure, False, True, True)
            .AddAgTextColumn(Dgl1, Col1MeasureUnit, 60, 0, Col1MeasureUnit, False, False)
            .AddAgTextColumn(Dgl1, Col1MeasureDecimalPlaces, 50, 0, Col1MeasureDecimalPlaces, False, True, False)
            .AddAgNumberColumn(Dgl1, Col1DeliveryMeasureMultiplier, 70, 8, 3, False, Col1DeliveryMeasureMultiplier, False, True, True)
            .AddAgNumberColumn(Dgl1, Col1DeliveryMeasurePerPcs, 110, 8, 4, False, Col1DeliveryMeasurePerPcs, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_MeasurePerPcs")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_MeasurePerPcs")), Boolean), True)
            .AddAgNumberColumn(Dgl1, Col1TotalDocDeliveryMeasure, 70, 8, 3, False, Col1TotalDocDeliveryMeasure, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Measure")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_Measure")), Boolean), True)
            .AddAgNumberColumn(Dgl1, Col1TotalFreeDeliveryMeasure, 70, 8, 3, False, Col1TotalFreeDeliveryMeasure, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_FreeMeasure")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_Measure")), Boolean), True)
            .AddAgNumberColumn(Dgl1, Col1TotalRejDeliveryMeasure, 70, 8, 3, False, Col1TotalRejDeliveryMeasure, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_RejMeasure")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_Measure")), Boolean), True)
            .AddAgNumberColumn(Dgl1, Col1TotalDeliveryMeasure, 70, 8, 3, False, Col1TotalDeliveryMeasure, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Measure")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_Measure")), Boolean), True)
            .AddAgTextColumn(Dgl1, Col1DeliveryMeasureDecimalPlaces, 50, 0, Col1DeliveryMeasureDecimalPlaces, False, True, False)
            .AddAgNumberColumn(Dgl1, Col1Rate, 60, 8, 3, False, Col1Rate, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Rate")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_Rate")), Boolean), True)
            .AddAgTextColumn(Dgl1, Col1Deal, 20, 0, Col1Deal, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Deal")), Boolean), False, False)
            .AddAgNumberColumn(Dgl1, Col1Amount, 70, 8, 2, False, Col1Amount, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Amount")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_Amount")), Boolean), True)
            .AddAgNumberColumn(Dgl1, Col1MRP, 60, 8, 2, False, Col1MRP, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_MRP")), Boolean), False, True)
            .AddAgNumberColumn(Dgl1, Col1ProfitMarginPer, 60, 8, 2, False, Col1ProfitMarginPer, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_ProfitMarginPer")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_ProfitMarginPer")), Boolean), True)
            .AddAgNumberColumn(Dgl1, Col1SaleRate, 60, 8, 2, False, Col1SaleRate, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_SaleRate")), Boolean), False, True)
            .AddAgDateColumn(Dgl1, Col1ExpiryDate, 90, Col1ExpiryDate, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_ExpiryDate")), Boolean), False)
            .AddAgTextColumn(Dgl1, Col1Remark, 150, 0, Col1Remark, True, False)
            .AddAgTextColumn(Dgl1, Col1VNature, 100, 0, Col1VNature, True, True, False)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.Anchor = Pnl1.Anchor
        Panel1.Anchor = Dgl1.Anchor
        Dgl1.ColumnHeadersHeight = 50

        If CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Measure")), Boolean) = False Then LblTotalDeliveryMeasure.Visible = False : LblTotalDeliveryMeasureText.Visible = False
        If CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Measure")), Boolean) = False Then LblTotalMeasure.Visible = False : LblTotalMeasureText.Visible = False

        AgCalcGrid1.Ini_Grid(LblV_Type.Tag, TxtV_Date.Text)

        AgCalcGrid1.AgLineGrid = Dgl1
        AgCalcGrid1.AgLineGridMandatoryColumn = Dgl1.Columns(Col1Item).Index
        AgCalcGrid1.AgLineGridGrossColumn = Dgl1.Columns(Col1Amount).Index
        AgCalcGrid1.AgLineGridPostingGroupSalesTaxProd = Dgl1.Columns(Col1SalesTaxGroup).Index

        Dgl1.AgSkipReadOnlyColumns = True

        Dgl1.AgLastColumn = Dgl1.Columns(Col1Remark).Index

        AgCustomGrid1.Ini_Grid(mSearchCode)
        AgCustomGrid1.SplitGrid = False

        Dgl1.AllowUserToOrderColumns = True

        If AgL.PubDtEnviro IsNot Nothing Then
            If AgL.PubDtEnviro.Rows.Count > 0 Then
                Dgl1.Columns(Col1PurchOrder).Visible = CType(AgL.VNull(AgL.PubDtEnviro.Rows(0)("IsVisible_PurchOrder")), Boolean)
            End If
        End If

        AgCL.GridSetiingShowXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1, False)
        AgCL.GridSetiingShowXml(Me.Text & AgCalcGrid1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, AgCalcGrid1, False)
        AgCL.GridSetiingShowXml(Me.Text & AgCustomGrid1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, AgCustomGrid1, False)
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

    Private Sub FrmSaleOrder_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As SQLiteConnection, ByVal Cmd As SQLiteCommand) Handles Me.BaseEvent_Save_InTrans
        Dim I As Integer, mSr As Integer
        Dim bSelectionQry$ = ""

        If BtnFillPartyDetail.Tag Is Nothing Then BtnFillPartyDetail.Tag = New FrmPurchPartyDetail

        mQry = " Update PurchChallan " &
                " SET  " &
                " ReferenceNo = " & AgL.Chk_Text(TxtReferenceNo.Text) & ", " &
                " Vendor = " & AgL.Chk_Text(TxtVendor.Tag) & ", " &
                " VendorName = " & AgL.Chk_Text(BtnFillPartyDetail.Tag.TxtVendorName.Text) & ", " &
                " VendorAdd1 = " & AgL.Chk_Text(BtnFillPartyDetail.Tag.TxtVendorAdd1.Text) & ", " &
                " VendorAdd2 = " & AgL.Chk_Text(BtnFillPartyDetail.Tag.TxtVendorAdd2.Text) & ", " &
                " VendorCity = " & AgL.Chk_Text(BtnFillPartyDetail.Tag.TxtVendorCity.Tag) & ", " &
                " VendorCityName = " & AgL.Chk_Text(BtnFillPartyDetail.Tag.TxtVendorCity.Text) & ", " &
                " VendorMobile = " & AgL.Chk_Text(BtnFillPartyDetail.Tag.TxtVendorMobile.Text) & ", " &
                " Currency = " & AgL.Chk_Text(TxtCurrency.Tag) & ", " &
                " SalesTaxGroupParty = " & AgL.Chk_Text(TxtSalesTaxGroupParty.Tag) & ", " &
                " Structure = " & AgL.Chk_Text(TxtStructure.Tag) & ", " &
                " CustomFields = " & AgL.Chk_Text(TxtCustomFields.Tag) & ", " &
                " VendorDocNo = " & AgL.Chk_Text(TxtVendorDocNo.Text) & ", " &
                " VendorDocDate = " & AgL.Chk_Text(TxtVendorDocDate.Text) & ", " &
                " Godown = " & AgL.Chk_Text(TxtGodown.Tag) & ", " &
                " Process = " & AgL.Chk_Text(TxtProcess.Tag) & ", " &
                " Remarks = " & AgL.Chk_Text(TxtRemarks.Text) & ", " &
                " TotalQty = " & Val(LblTotalQty.Text) & ", " &
                " TotalAmount = " & Val(LblTotalAmount.Text) & ", " &
                " TotalMeasure = " & Val(LblTotalMeasure.Text) & " ," &
                " GateEntryNo = " & AgL.Chk_Text(TxtGateEntryNo.Tag) & ", " &
                " Form = " & AgL.Chk_Text(TxtForm.Tag) & ", " &
                " FormNo = " & AgL.Chk_Text(TxtFormNo.Text) & ", " &
                " " & AgCalcGrid1.FFooterTableUpdateStr() & " " &
                " " & AgCustomGrid1.FFooterTableUpdateStr() & " " &
                " Where DocID = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Delete From PurchChallanDetail Where DocID = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Delete From Stock Where DocID = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Delete from JobIssRecUID Where DocID='" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " Update JobIssRecUID Set JobRecDocId = Null WHERE JobRecDocID =  '" & mInternalCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1Item, I).Value <> "" Then
                mSr += 1
                If bSelectionQry <> "" Then bSelectionQry += " UNION ALL "
                bSelectionQry += " Select " & AgL.Chk_Text(SearchCode) & ", " & mSr & ", " & AgL.Chk_Text(SearchCode) & ", " & mSr & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1PurchOrder, I).Tag) & ", " &
                        " " & Val(Dgl1.Item(Col1PurchOrderSr, I).Value) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1Item, I).Tag) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1Dimension1, I).Tag) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1Dimension2, I).Tag) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1Item_Uid, I).Tag) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1Specification, I).Value) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1LotNo, I).Value) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1BaleNo, I).Value) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1SalesTaxGroup, I).Tag) & ", " &
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
                        " " & AgL.Chk_Text(Dgl1.Item(Col1DeliveryMeasure, I).Value) & ", " &
                        " " & Val(Dgl1.Item(Col1DeliveryMeasurePerPcs, I).Value) & ", " &
                        " " & Val(Dgl1.Item(Col1DeliveryMeasureMultiplier, I).Value) & ", " &
                        " " & Val(Dgl1.Item(Col1TotalDocDeliveryMeasure, I).Value) & ", " &
                        " " & Val(Dgl1.Item(Col1TotalFreeDeliveryMeasure, I).Value) & ", " &
                        " " & Val(Dgl1.Item(Col1TotalRejDeliveryMeasure, I).Value) & ", " &
                        " " & Val(Dgl1.Item(Col1TotalDeliveryMeasure, I).Value) & ", " &
                        " " & Val(Dgl1.Item(Col1Rate, I).Value) & ", " &
                        " " & Val(Dgl1.Item(Col1Amount, I).Value) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1BillingType, I).Value) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1ExpiryDate, I).Value) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1Remark, I).Value) & " , " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1Deal, I).Value) & " , " &
                        " " & Val(Dgl1.Item(Col1MRP, I).Value) & " , " &
                        " " & Val(Dgl1.Item(Col1SaleRate, I).Value) & " , " &
                        " " & Val(Dgl1.Item(Col1ProfitMarginPer, I).Value) & " , " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1VNature, I).Value) & ", " &
                        " " & AgCalcGrid1.FLineTableFieldValuesStr(I) & " "
            End If
        Next

        mQry = "Insert Into PurchChallanDetail(DocId, Sr, PurchChallan, PurchChallanSr, PurchOrder, PurchOrderSr, Item, Dimension1, Dimension2, Item_Uid, Specification, LotNo, BaleNo, SalesTaxGroupItem, DocQty, " &
                " FreeQty, RejQty, Qty, Unit, MeasurePerPcs, PcsPerMeasure, MeasureUnit, " &
                " TotalDocMeasure, TotalFreeMeasure, TotalRejMeasure, TotalMeasure, " &
                " DeliveryMeasure, DeliveryMeasurePerPcs, DeliveryMeasureMultiplier, TotalDocDeliveryMeasure, TotalFreeDeliveryMeasure, TotalRejDeliveryMeasure, TotalDeliveryMeasure, " &
                " Rate, Amount, BillingType, ExpiryDate, Remark, Deal, Mrp, Sale_Rate, ProfitMarginPer, V_Nature, " & AgCalcGrid1.FLineTableFieldNameStr() & ") " & bSelectionQry
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mSr = 0

        mQry = " INSERT INTO  Stock(DocID, Sr, V_Type, V_Prefix, V_Date, V_No, Div_Code, Site_Code,   " &
                " SubCode, Currency, SalesTaxGroupParty, Structure, BillingType, Item,  " &
                " Godown,EType_IR, Qty_Iss, Qty_Rec, Unit, LotNo, MeasurePerPcs, Measure_Iss, Measure_Rec, MeasureUnit, " &
                " Rate, Amount, Landed_Value, Remarks, RecId, ReferenceDocId, ReferenceDocIdSr, ExpiryDate, MRP, Sale_Rate, Process) " &
                " SELECT L.DocId, L.Sr, H.V_Type, H.V_Prefix, H.V_Date, H.V_No, H.Div_Code, H.Site_Code, " &
                " H.Vendor, H.Currency, H.SalesTaxGroupParty, H.Structure, H.BillingType, L.Item, H.Godown,'R', 0, L.Qty, " &
                " L.Unit, L.LotNo, L.MeasurePerPcs,0, L.TotalMeasure, L.MeasureUnit, L.Landed_Value/L.Qty, L.Landed_Value, L.Landed_Value, " &
                " L.Remark, H.ReferenceNo, L.DocId, L.Sr, L.ExpiryDate, L.MRP, L.Sale_Rate, H.Process " &
                " FROM PurchChallanDetail L  " &
                " LEFT JOIN PurchChallan H ON L.DocId = H.DocID " &
                " Where L.DocId = '" & mSearchCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        FPostInStockVirtual(Conn, Cmd)

        Call FPostInJobIssRecUID(SearchCode, Conn, Cmd)

        If TxtGateEntryNo.Tag = "" Then
            Call FSaveGateEntryDetail(Conn, Cmd)
        End If

        If AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Or AgL.StrCmp(AgL.PubUserName, "Sa") Then
            AgCL.GridSetiingWriteXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1)
            AgCL.GridSetiingWriteXml(Me.Text & AgCalcGrid1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, AgCalcGrid1)
            AgCL.GridSetiingWriteXml(Me.Text & AgCustomGrid1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, AgCustomGrid1)
        End If
    End Sub

    Private Sub FPostInJobIssRecUID(ByVal SearchCode As String, ByVal Conn As SQLiteConnection, ByVal Cmd As SQLiteCommand)
        Dim I As Integer = 0, mSr As Integer = 0

        mQry = "Delete from JobIssRecUID Where DocId ='" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " INSERT INTO JobIssRecUID(DocID, TSr, Sr, IssRec, Process, JobRecDocID, Item, Item_UID, " &
                 " Godown, Site_Code, V_Date, V_Type, SubCode, Div_Code, RecId, EntryDate, Remark) " &
                 " Select L.DocId, L.Sr As TSr, L.Sr, 'R', H.Process, L.PurchOrder, L.Item, L.Item_Uid, " &
                 " H.Godown, H.Site_Code, H.V_Date, H.V_Type, H.Vendor, H.Div_Code, H.ReferenceNo, H.EntryDate, " &
                 " SubString(IfNull(H.Remarks,'') || '.' || IfNull(L.Remark,''),0,255) " &
                 " From (Select * From PurchChallanDetail  Where DocId = '" & mSearchCode & "' And Item_Uid Is Not Null) As L " &
                 " LEFT JOIN PurchChallan H  On L.DocId = H.DocId "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " Update JobIssRecUID " &
                " SET JobRecDocID = " & AgL.Chk_Text(mInternalCode) & " " &
                " WHERE JobRecDocID Is Null " &
                " And Item_UID In (Select Item_UID From PurchChallanDetail  Where DocId = '" & mSearchCode & "' And Item_Uid Is Not Null) " &
                " And Process = '" & TxtProcess.Tag & "' " &
                " AND ISSREC = 'I'" &
                " And Site_Code = '" & AgL.PubSiteCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Update Item_UID Set RecDocID = Null, RecSr = Null, Item_ManualUID=Null, IsInStock=0 Where RecDocID = '" & mInternalCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        For I = 0 To Dgl1.Rows.Count - 1
            If Dgl1.Item(Col1Item, I).Value <> "" And Dgl1.Item(Col1Item_Uid, I).Tag <> "" Then
                mSr += 1
                mQry = " UPDATE Item_UID SET " &
                        " RecDocID = '" & mInternalCode & "', " &
                        " RecSr = " & Val(mSr) & ", " &
                        " IsInStock = 1 " &
                        " WHERE Code = '" & Dgl1.AgSelectedValue(Col1Item_Uid, I) & "' "
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            End If
        Next


    End Sub

    Private Sub FPostInStockVirtual(ByVal Conn As SQLiteConnection, ByVal Cmd As SQLiteCommand)
        Dim I As Integer = 0
        Dim mSr As Integer = 0
        Dim StockVirtual As AgTemplate.ClsMain.StructStock = Nothing

        mQry = "Delete From StockVirtual Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " INSERT INTO StockVirtual(DocID, Sr, V_Type, V_Prefix, V_Date, V_No, RecId, Div_Code, Site_Code, SubCode, " &
                  " CostCenter, Item, Qty_Iss, Unit, MeasurePerPcs, Measure_Iss, MeasureUnit, Rate, Amount, " &
                  " ReferenceDocID, ReferenceDocIDSr) " &
                  " SELECT H.DocID, L.Sr, H.V_Type, H.V_Prefix, H.V_Date, H.V_No, H.ReferenceNo, H.Div_Code, " &
                  " H.Site_Code, H.Vendor, Null, L.Item, L.Qty, L.Unit, L.MeasurePerPcs, L.TotalMeasure, L.MeasureUnit, " &
                  " l.Rate, L.Amount, Pod.PurchIndent, Pod.PurchIndentSr " &
                  " FROM PurchChallan H  " &
                  " LEFT JOIN PurchChallanDetail L ON H.DocID = L.DocId " &
                  " LEFT JOIN PurchOrderDetail Pod ON L.PurchOrder = Pod.DocId And L.PurchOrderSr = Pod.Sr " &
                  " Where H.DocId = '" & mSearchCode & "' And L.PurchOrder Is Not Null "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    End Sub

    Private Sub FSaveGateEntryDetail(ByVal Conn As SQLiteConnection, ByVal Cmd As SQLiteCommand)
        If BtnFillGateDetail.Tag Is Nothing Then Exit Sub

        mQry = " INSERT INTO GateInOut(DocID, V_Type, V_Prefix, V_Date, V_No, Div_Code, " &
                " Site_Code, SubCode, VehicleType, VehicleNo, Transporter, LrNo, LrDate, " &
                " Remarks, Manual_RefNo, EntryBy, EntryDate,  EntryType, EntryStatus, Status) " &
                " VALUES ('" & mSearchCode & "', " & AgL.Chk_Text(TxtV_Type.AgSelectedValue) & ", " &
                " " & AgL.Chk_Text(LblPrefix.Text) & ",	" & AgL.Chk_Text(TxtV_Date.Text) & ", " &
                " " & Val(TxtV_No.Text) & ", " &
                " " & AgL.Chk_Text(TxtDivision.Tag) & ", " & AgL.Chk_Text(TxtSite_Code.Tag) & ", " &
                " " & AgL.Chk_Text(TxtVendor.Tag) & ",	" &
                " " & AgL.Chk_Text(BtnFillGateDetail.Tag.TxtVehicleType.Text) & ",	" &
                " " & AgL.Chk_Text(BtnFillGateDetail.Tag.TxtVehicleNo.Text) & ",	" &
                " " & AgL.Chk_Text(BtnFillGateDetail.Tag.TxtTransporter.Tag) & ",	" &
                " " & AgL.Chk_Text(BtnFillGateDetail.Tag.TxtLrNo.Text) & ",	" &
                " " & AgL.Chk_Text(BtnFillGateDetail.Tag.TxtLrDate.Text) & ",	" &
                " " & AgL.Chk_Text(TxtRemarks.Text) & ",	" &
                " " & AgL.Chk_Text(TxtReferenceNo.Text) & ",	" &
                " " & AgL.Chk_Text(AgL.PubUserName) & ", " & AgL.Chk_Text(AgL.GetDateTime(AgL.GcnRead)) & ", " &
                " " & AgL.Chk_Text(Topctrl1.Mode) & ", " & AgL.Chk_Text(LogStatus.LogOpen) & ", " &
                " " & AgL.Chk_Text(TxtStatus.Text) & " )"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " UPDATE PurchChallan Set GateEntryNo = " & AgL.Chk_Text(mSearchCode) & " Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    End Sub

    Private Sub FrmSaleOrder_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim I As Integer

        Dim DsTemp As DataSet

        mIsEntryLocked = False

        mQry = "Select H.*, V.Name || ',' || (Case When C.CityName Is Not Null Then ',' || C.CityName Else '' End) As VendorDesc, " &
                " C1.Description as CurrencyDesc, V.Nature,  " &
                " PO.V_Type || '-' || PO.ReferenceNo as PurchOrderDesc, G.Description as GodownDesc, " &
                " F.Description as FormDesc, T.DispName as TransporterDesc, GI.Manual_RefNo As GateEntryRefNo, " &
                " P.Description As ProcessDesc " &
                " From (Select * From PurchChallan  Where DocID='" & SearchCode & "') H " &
                " Left Join SubGroup V  On H.Vendor = V.SubCode " &
                " LEFT JOIN City C ON V.CityCode = C.CityCode  " &
                " Left Join Currency C1  On H.Currency = C1.Code " &
                " Left Join PurchOrder PO  On H.PurchOrder = PO.DocID " &
                " Left Join Godown G  On H.Godown = G.Code " &
                " Left Join Form_Master F  On H.Form = F.Code " &
                " Left Join SubGroup T  On H.Transporter = T.SubCode " &
                " LEFT JOIN GateInOut GI On H.GateEntryNo = GI.DocId " &
                " LEFT JOIN Process P ON H.Process = P.NCat "
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                TxtStructure.Tag = AgStructure.ClsMain.FGetStructureFromNCat(LblV_Type.Tag, AgL.GcnRead)
                TxtCustomFields.Tag = AgCustomFields.ClsMain.FGetCustomFieldFromV_Type(TxtV_Type.Tag, AgL.GcnRead)

                If AgL.XNull(.Rows(0)("Structure")) <> "" Then
                    TxtStructure.Tag = AgL.XNull(.Rows(0)("Structure"))
                End If

                AgCalcGrid1.FrmType = Me.FrmType
                AgCalcGrid1.AgStructure = TxtStructure.Tag
                AgCalcGrid1.AgVoucherCategory = "PURCH"

                If AgL.XNull(.Rows(0)("CustomFields")) <> "" Then
                    TxtCustomFields.Tag = AgL.XNull(.Rows(0)("CustomFields"))
                End If
                AgCustomGrid1.FrmType = Me.FrmType
                AgCustomGrid1.AgCustom = TxtCustomFields.Tag


                IniGrid()

                TxtReferenceNo.Text = AgL.XNull(.Rows(0)("ReferenceNo"))
                TxtVendor.Tag = AgL.XNull(.Rows(0)("Vendor"))
                TxtVendor.Text = AgL.XNull(.Rows(0)("VendorDesc"))

                TxtNature.Text = AgL.XNull(.Rows(0)("Nature"))

                Dim FrmObj As New FrmPurchPartyDetail
                FrmObj.TxtVendorMobile.Text = AgL.XNull(.Rows(0)("VendorMobile"))
                FrmObj.TxtVendorName.Text = AgL.XNull(.Rows(0)("VendorName"))
                FrmObj.TxtVendorAdd1.Text = AgL.XNull(.Rows(0)("VendorAdd1"))
                FrmObj.TxtVendorAdd2.Text = AgL.XNull(.Rows(0)("VendorAdd2"))
                FrmObj.TxtVendorCity.Tag = AgL.XNull(.Rows(0)("VendorCity"))
                FrmObj.TxtVendorCity.Text = AgL.XNull(.Rows(0)("VendorCityName"))
                BtnFillPartyDetail.Tag = FrmObj

                TxtCurrency.Tag = AgL.XNull(.Rows(0)("Currency"))
                TxtCurrency.Text = AgL.XNull(.Rows(0)("CurrencyDesc"))
                TxtVendorDocNo.Text = AgL.XNull(.Rows(0)("VendorDocNo"))
                TxtVendorDocDate.Text = AgL.XNull(.Rows(0)("VendorDocDate"))

                TxtProcess.Tag = AgL.XNull(.Rows(0)("Process"))
                TxtProcess.Text = AgL.XNull(.Rows(0)("ProcessDesc"))

                TxtCurrency.Tag = AgL.XNull(.Rows(0)("Currency"))
                TxtCurrency.Text = AgL.XNull(.Rows(0)("CurrencyDesc"))
                TxtGodown.Tag = AgL.XNull(.Rows(0)("Godown"))
                TxtGodown.Text = AgL.XNull(.Rows(0)("GodownDesc"))
                TxtRemarks.Text = AgL.XNull(.Rows(0)("Remarks"))

                TxtGateEntryNo.Tag = AgL.XNull(.Rows(0)("GateEntryNo"))
                TxtGateEntryNo.Text = AgL.XNull(.Rows(0)("GateEntryRefNo"))

                TxtForm.Tag = AgL.XNull(.Rows(0)("Form"))
                TxtForm.Text = AgL.XNull(.Rows(0)("FormDesc"))
                TxtFormNo.Text = AgL.XNull(.Rows(0)("FormNo"))
                TxtSalesTaxGroupParty.Tag = AgL.XNull(.Rows(0)("SalesTaxGroupParty"))
                TxtSalesTaxGroupParty.Text = AgL.XNull(.Rows(0)("SalesTaxGroupParty"))

                LblTotalQty.Text = AgL.VNull(.Rows(0)("TotalQty"))
                LblTotalAmount.Text = AgL.VNull(.Rows(0)("TotalAmount"))
                LblTotalMeasure.Text = AgL.VNull(.Rows(0)("TotalMeasure"))

                AgCalcGrid1.FMoveRecFooterTable(DsTemp.Tables(0), LblV_Type.Tag, TxtV_Date.Text)
                AgCustomGrid1.FMoveRecFooterTable(DsTemp.Tables(0))

                mQry = "SELECT H.DocID, H.Manual_RefNo, H.VehicleType, H.VehicleNo, H.Transporter, " &
                        " Sg.DispName AS TransporterName, H.LrNo, H.LrDate  " &
                        " FROM GateInOut H  " &
                        " LEFT JOIN SubGroup Sg ON H.Transporter = Sg.SubCode " &
                        " Where DocId = '" & TxtGateEntryNo.Tag & "'"
                DsTemp = AgL.FillData(mQry, AgL.GCn)

                With DsTemp.Tables(0)
                    If .Rows.Count > 0 Then
                        Dim FrmObjGate As New FrmPurchChallanGateDetail
                        FrmObjGate.TxtVehicleType.Text = AgL.XNull(.Rows(0)("VehicleType"))
                        FrmObjGate.TxtVehicleNo.Text = AgL.XNull(.Rows(0)("VehicleNo"))
                        FrmObjGate.TxtTransporter.Tag = AgL.XNull(.Rows(0)("Transporter"))
                        FrmObjGate.TxtTransporter.Text = AgL.XNull(.Rows(0)("TransporterName"))
                        FrmObjGate.TxtLRNo.Text = AgL.XNull(.Rows(0)("LRNo"))
                        FrmObjGate.TxtLRDate.Text = AgL.XNull(.Rows(0)("LRDate"))

                        BtnFillGateDetail.Tag = FrmObj
                    End If
                End With



                Dim strQryPurchaseInvoiced$ = "SELECT L.PurchChallan, L.PurchChallanSr, Sum(L.Qty) AS Qty " &
                                             "FROM PurchInvoiceDetail L  " &
                                             "Where L.PurchChallan = '" & SearchCode & "' " &
                                             "GROUP BY L.PurchChallan, L.PurchChallanSr  "


                mQry = "Select L.*, IU.Item_UID as Item_UIDDesc, PO.V_Type || '-' || PO.ReferenceNo as PurchOrderDesc, " &
                        " I.Description as ItemDesc, I.ManualCode As ItemManualCode, " &
                        " D1.Description As Dimension1Desc, D2.Description As Dimension2Desc, " &
                        " U.DecimalPlaces as QtyDecimalPlaces, MU.DecimalPlaces as MeasureDecimalPlaces, DMU.DecimalPlaces as DeliveryMeasureDecimalPlaces,  " &
                        " (Case When IfNull(PurInv.Qty,0) <> 0 Then 1 Else 0 End) as RowLocked " &
                        " From (Select * From PurchChallanDetail  where DocId = '" & SearchCode & "') L " &
                        " Left Join PurchOrder PO  On L.PurchOrder = PO.DocID " &
                        " Left Join Item I  On L.Item = I.Code " &
                        " Left Join Item_UID IU  On L.Item_UID = IU.Code " &
                        " LEFT JOIN Unit U On L.Unit = U.Code " &
                        " LEFT JOIN Unit MU ON L.MeasureUnit = MU.Code " &
                        " LEFT JOIN Unit Dmu On L.DeliveryMeasure = Dmu.Code " &
                        " Left Join Dimension1 D1   On L.Dimension1 = D1.Code " &
                        " Left Join Dimension2 D2   On L.Dimension2 = D2.Code " &
                        " Left Join (" & strQryPurchaseInvoiced & ") as PurInv On L.DocID = PurInv.PurchChallan and L.Sr = PurInv.PurchChallanSr " &
                        " Order By Sr"
                DsTemp = AgL.FillData(mQry, AgL.GCn)
                With DsTemp.Tables(0)
                    Dgl1.RowCount = 1
                    Dgl1.Rows.Clear()
                    If .Rows.Count > 0 Then
                        For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                            Dgl1.Rows.Add()
                            Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count - 1
                            Dgl1.Item(Col1PurchOrder, I).Tag = AgL.XNull(.Rows(I)("PurchOrder"))
                            Dgl1.Item(Col1PurchOrder, I).Value = AgL.XNull(.Rows(I)("PurchOrderDesc"))
                            Dgl1.Item(Col1PurchOrderSr, I).Value = AgL.XNull(.Rows(I)("PurchOrderSr"))
                            Dgl1.Item(Col1ItemCode, I).Tag = AgL.XNull(.Rows(I)("Item"))
                            Dgl1.Item(Col1ItemCode, I).Value = AgL.XNull(.Rows(I)("ItemManualCode"))
                            Dgl1.Item(Col1Item, I).Tag = AgL.XNull(.Rows(I)("Item"))
                            Dgl1.Item(Col1Item, I).Value = AgL.XNull(.Rows(I)("ItemDesc"))
                            Dgl1.Item(Col1Dimension1, I).Tag = AgL.XNull(.Rows(I)("Dimension1"))
                            Dgl1.Item(Col1Dimension1, I).Value = AgL.XNull(.Rows(I)("Dimension1Desc"))
                            Dgl1.Item(Col1Dimension2, I).Tag = AgL.XNull(.Rows(I)("Dimension2"))
                            Dgl1.Item(Col1Dimension2, I).Value = AgL.XNull(.Rows(I)("Dimension2Desc"))
                            Dgl1.Item(Col1Item_Uid, I).Tag = AgL.XNull(.Rows(I)("Item_UID"))
                            Dgl1.Item(Col1Item_Uid, I).Value = AgL.XNull(.Rows(I)("Item_UIDDesc"))
                            Dgl1.Item(Col1Specification, I).Value = AgL.XNull(.Rows(I)("Specification"))
                            Dgl1.Item(Col1LotNo, I).Value = AgL.XNull(.Rows(I)("LotNo"))
                            Dgl1.Item(Col1BaleNo, I).Value = AgL.XNull(.Rows(I)("BaleNo"))
                            Dgl1.Item(Col1SalesTaxGroup, I).Tag = AgL.XNull(.Rows(I)("SalesTaxGroupItem"))
                            Dgl1.Item(Col1SalesTaxGroup, I).Value = AgL.XNull(.Rows(I)("SalesTaxGroupItem"))
                            Dgl1.Item(Col1QtyDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("QtyDecimalPlaces"))
                            Dgl1.Item(Col1DocQty, I).Value = Format(AgL.VNull(.Rows(I)("DocQty")), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1FreeQty, I).Value = Format(AgL.VNull(.Rows(I)("FreeQty")), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1RejQty, I).Value = Format(AgL.VNull(.Rows(I)("RejQty")), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1Qty, I).Value = Format(AgL.VNull(.Rows(I)("Qty")), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1Deal, I).Value = AgL.XNull(.Rows(I)("Deal"))
                            Dgl1.Item(Col1ExpiryDate, I).Value = AgL.XNull(.Rows(I)("ExpiryDate"))
                            Dgl1.Item(Col1MRP, I).Value = AgL.VNull(.Rows(I)("Mrp"))
                            Dgl1.Item(Col1SaleRate, I).Value = AgL.VNull(.Rows(I)("Sale_Rate"))
                            Dgl1.Item(Col1ProfitMarginPer, I).Value = AgL.VNull(.Rows(I)("ProfitMarginPer"))
                            Dgl1.Item(Col1Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))

                            Dgl1.Item(Col1MeasureDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("MeasureDecimalPlaces"))
                            Dgl1.Item(Col1MeasurePerPcs, I).Value = Format(AgL.VNull(.Rows(I)("MeasurePerPcs")), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1PcsPerMeasure, I).Value = Format(AgL.VNull(.Rows(I)("PcsPerMeasure")), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1MeasureUnit, I).Value = AgL.XNull(.Rows(I)("MeasureUnit"))
                            Dgl1.Item(Col1TotalDocMeasure, I).Value = Format(AgL.VNull(.Rows(I)("TotalDocMeasure")), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1TotalFreeMeasure, I).Value = Format(AgL.VNull(.Rows(I)("TotalFreeMeasure")), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1TotalRejMeasure, I).Value = Format(AgL.VNull(.Rows(I)("TotalRejMeasure")), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1TotalMeasure, I).Value = Format(AgL.VNull(.Rows(I)("TotalMeasure")), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1BillingType, I).Value = AgL.XNull(.Rows(I)("BillingType"))
                            Dgl1.Item(Col1DeliveryMeasure, I).Value = AgL.XNull(.Rows(I)("DeliveryMeasure"))
                            Dgl1.Item(Col1DeliveryMeasurePerPcs, I).Value = AgL.VNull(.Rows(I)("DeliveryMeasurePerPcs"))
                            Dgl1.Item(Col1DeliveryMeasureMultiplier, I).Value = AgL.VNull(.Rows(I)("DeliveryMeasureMultiplier"))
                            Dgl1.Item(Col1DeliveryMeasureDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("DeliveryMeasureDecimalPlaces"))
                            Dgl1.Item(Col1TotalDocDeliveryMeasure, I).Value = Format(AgL.VNull(.Rows(I)("TotalDocDeliveryMeasure")), "0.".PadRight(AgL.VNull(.Rows(I)("DeliveryMeasureDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1TotalFreeDeliveryMeasure, I).Value = Format(AgL.VNull(.Rows(I)("TotalFreeDeliveryMeasure")), "0.".PadRight(AgL.VNull(.Rows(I)("DeliveryMeasureDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1TotalRejDeliveryMeasure, I).Value = Format(AgL.VNull(.Rows(I)("TotalRejDeliveryMeasure")), "0.".PadRight(AgL.VNull(.Rows(I)("DeliveryMeasureDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1TotalDeliveryMeasure, I).Value = Format(AgL.VNull(.Rows(I)("TotalDeliveryMeasure")), "0.".PadRight(AgL.VNull(.Rows(I)("DeliveryMeasureDecimalPlaces")) + 2, "0"))

                            Dgl1.Item(Col1Rate, I).Value = AgL.VNull(.Rows(I)("Rate"))
                            Dgl1.Item(Col1Amount, I).Value = Format(AgL.VNull(.Rows(I)("Amount")), "0.".PadRight(CType(Dgl1.Columns(Col1Amount), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                            Dgl1.Item(Col1ExpiryDate, I).Value = AgL.XNull(.Rows(I)("ExpiryDate"))
                            Dgl1.Item(Col1Remark, I).Value = AgL.XNull(.Rows(I)("Remark"))

                            Dgl1.Item(Col1VNature, I).Value = AgL.XNull(.Rows(I)("V_Nature"))

                            If .Rows(I)("RowLocked") > 0 Then Dgl1.Rows(I).DefaultCellStyle.BackColor = AgTemplate.ClsMain.Colours.GridRow_Locked : Dgl1.Rows(I).ReadOnly = True : mIsEntryLocked = True

                            Call AgCalcGrid1.FMoveRecLineTable(DsTemp.Tables(0), I)
                        Next I
                    End If
                End With
                If AgCustomGrid1.Rows.Count = 0 Then AgCustomGrid1.Visible = False
            End If
        End With
    End Sub

    Private Sub FrmSaleOrder_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Topctrl1.ChangeAgGridState(Dgl1, False)
        AgCalcGrid1.FrmType = Me.FrmType
        AgCustomGrid1.FrmType = Me.FrmType
        RbtChallanForOrder.Checked = True
        AgL.WinSetting(Me, 650, 990, 0, 0)
    End Sub

    Private Sub TxtVendor_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtVendor.KeyDown, TxtGateEntryNo.KeyDown, TxtCurrency.KeyDown, TxtSalesTaxGroupParty.KeyDown, TxtGodown.KeyDown, TxtForm.KeyDown, TxtFormNo.KeyDown, TxtProcess.KeyDown
        Dim strCond$ = ""
        Dim DsTemp As DataSet = Nothing
        Try
            If e.KeyCode = Keys.Enter Then Exit Sub

            Select Case sender.Name
                Case TxtVendor.Name
                    If TxtVendor.AgHelpDataSet Is Nothing Then
                        FCreateHelpSubgroup(sender)
                    End If

                Case TxtGateEntryNo.Name
                    If e.KeyCode = Keys.Insert Then
                        If TxtGateEntryNo.Tag <> "" Then
                            mQry = "SELECT H.DocID, H.Manual_RefNo, H.VehicleType, H.VehicleNo, H.Transporter, " &
                                    " Sg.DispName AS TransporterName, H.LrNo, H.LrDate  " &
                                    " FROM GateInOut H  " &
                                    " LEFT JOIN SubGroup Sg ON H.Transporter = Sg.SubCode " &
                                    " Where DocId = '" & TxtGateEntryNo.Tag & "'"
                            DsTemp = AgL.FillData(mQry, AgL.GCn)

                            With DsTemp.Tables(0)
                                If .Rows.Count > 0 Then
                                    Dim FrmObjGate As New FrmPurchChallanGateDetail
                                    FrmObjGate.TxtVehicleType.Text = AgL.XNull(.Rows(0)("VehicleType"))
                                    FrmObjGate.TxtVehicleNo.Text = AgL.XNull(.Rows(0)("VehicleNo"))
                                    FrmObjGate.TxtTransporter.Tag = AgL.XNull(.Rows(0)("Transporter"))
                                    FrmObjGate.TxtTransporter.Text = AgL.XNull(.Rows(0)("TransporterName"))
                                    FrmObjGate.TxtLRNo.Text = AgL.XNull(.Rows(0)("LRNo"))
                                    FrmObjGate.TxtLRDate.Text = AgL.XNull(.Rows(0)("LRDate"))
                                    BtnFillGateDetail.Tag = FrmObjGate
                                End If
                            End With
                        Else
                            Dim FrmObjGate As New FrmPurchChallanGateDetail
                            FrmObjGate.ShowDialog()
                        End If
                    Else
                        If TxtGateEntryNo.AgHelpDataSet Is Nothing Then
                            mQry = " SELECT GIO.DocID AS Code, GIO.V_Type +'-'+ Convert(NVARCHAR(5),GIO.V_No) AS [Entry No], " &
                                    " GIO.VehicleNo ,GIO.LrNo,GIO.LrDate, GIO.Transporter, T.DispName as TransporterDesc, GIO.Driver,  " &
                                    " GIO.Div_Code," &
                                    " IfNull(GIO.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') As Status " &
                                    " FROM GateInOut GIO Left Join SubGroup T On GIO.Transporter = T.SubCode " &
                                    " Where IfNull(GIO.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " &
                                    " And GIO.Div_Code = '" & TxtDivision.AgSelectedValue & "'"
                            TxtGateEntryNo.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If

                Case TxtCurrency.Name
                    If TxtCurrency.AgHelpDataSet Is Nothing Then
                        mQry = "SELECT Code, Description AS Currency " &
                                " FROM Currency " &
                                " Where IfNull(IsDeleted,0)=0 " &
                                " ORDER BY Code "
                        TxtCurrency.AgHelpDataSet(0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
                    End If

                Case TxtSalesTaxGroupParty.Name
                    If TxtSalesTaxGroupParty.AgHelpDataSet Is Nothing Then
                        mQry = "SELECT Description AS Code, Description " &
                               "FROM PostingGroupSalesTaxParty " &
                               "Where IfNull(Active,0)=1 "
                        TxtSalesTaxGroupParty.AgHelpDataSet(0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
                    End If

                Case TxtGodown.Name
                    If TxtGodown.AgHelpDataSet Is Nothing Then
                        mQry = "SELECT H.Code, H.Description " &
                                " FROM Godown H " &
                                " Where H.Site_Code = '" & TxtSite_Code.Tag & "' " &
                                " And IfNull(H.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " &
                                " Order By H.Description"
                        TxtGodown.AgHelpDataSet(0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
                    End If

                Case TxtForm.Name
                    If TxtForm.AgHelpDataSet Is Nothing Then
                        mQry = " Select F.Code, F.Description As Form " &
                                " From Form_Master F " &
                                " Where F.Category = 'Road Permit' " &
                                " And IfNull(F.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "'"
                        TxtForm.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)
                    End If

                Case TxtFormNo.Name
                    If TxtFormNo.AgHelpDataSet Is Nothing Then
                        If BtnRemoveFilter.Tag = 0 Then
                            strCond = " And IfNull(L.IsUtilised,0) = 0 "
                        End If
                        mQry = "SELECT L.FormNo AS Code, L.FormNo " &
                               "FROM Form_ReceiveDetail L  " &
                               "Where L.Form = '" & TxtForm.Tag & "' " &
                               "And L.IssueTo = '" & TxtVendor.Tag & "'"
                        TxtFormNo.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)
                    End If

                Case TxtProcess.Name
                    If e.KeyCode <> Keys.Enter Then
                        If TxtProcess.AgHelpDataSet Is Nothing Then
                            mQry = "Select P.NCat As Code, P.Description As Process, P.CostCenter, CCM.Name as CostCenterDesc, P.DefaultBillingType, P.Div_Code " &
                                  " From Process P  " &
                                  " Left Join CostCenterMast CCM On P.CostCenter = CCM.Code " &
                                  " Order By P.Description "
                            TxtProcess.AgHelpDataSet(4) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If

            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Txt_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtV_Type.Validating, TxtVendor.Validating, TxtSalesTaxGroupParty.Validating, TxtGateEntryNo.Validating, TxtReferenceNo.Validating
        Dim DtTemp As DataTable = Nothing
        Dim DsTemp As DataSet = Nothing
        Dim FrmObj As New FrmPurchPartyDetail
        Try
            Select Case sender.NAME
                Case TxtV_Type.Name
                    TxtStructure.Tag = AgStructure.ClsMain.FGetStructureFromNCat(LblV_Type.Tag, AgL.GcnRead)
                    If TxtStructure.Tag <> "" Then
                        TxtStructure.Text = AgL.Dman_Execute("Select IfNull(Description,'') From Structure  Where Code = '" & TxtStructure.Tag & "'", AgL.GcnRead).ExecuteScalar
                    End If
                    AgCalcGrid1.AgStructure = TxtStructure.Tag

                    TxtCustomFields.Tag = AgCustomFields.ClsMain.FGetCustomFieldFromV_Type(TxtV_Type.Tag, AgL.GcnRead)
                    AgCustomGrid1.AgCustom = TxtCustomFields.Tag

                    IniGrid()
                    TxtReferenceNo.Text = AgTemplate.ClsMain.FGetManualRefNo("ReferenceNo", "PurchChallan", TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, AgTemplate.ClsMain.ManualRefType.Max)
                    FAsignProcess()

                Case TxtVendor.Name
                    If sender.text.ToString.Trim <> "" Then
                        mQry = "Select H.Currency, H.SalesTaxPostingGroup, C.Description as CurrencyDesc, H.Nature " &
                               " From Subgroup H  " &
                               " Left Join Currency C  On H.Currency = C.Code " &
                               " Where H.SubCode = '" & TxtVendor.Tag & "'"
                        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
                        If DtTemp.Rows.Count > 0 Then
                            TxtCurrency.Tag = AgL.XNull(DtTemp.Rows(0)("Currency"))
                            TxtCurrency.Text = AgL.XNull(DtTemp.Rows(0)("CurrencyDesc"))
                            TxtSalesTaxGroupParty.Tag = AgL.XNull(DtTemp.Rows(0)("SalesTaxPostingGroup"))
                            TxtSalesTaxGroupParty.Text = AgL.XNull(DtTemp.Rows(0)("SalesTaxPostingGroup"))
                            AgCalcGrid1.AgPostingGroupSalesTaxParty = TxtSalesTaxGroupParty.Tag
                            TxtNature.Text = AgL.XNull(DtTemp.Rows(0)("Nature"))
                        Else
                            TxtCurrency.Tag = ""
                            TxtCurrency.Text = ""
                            TxtSalesTaxGroupParty.Tag = ""
                            TxtSalesTaxGroupParty.Text = ""
                            AgCalcGrid1.AgPostingGroupSalesTaxParty = ""
                            TxtNature.Text = ""
                        End If

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
                    AgCalcGrid1.AgPostingGroupSalesTaxParty = TxtSalesTaxGroupParty.Tag
                    Calculation()

                Case TxtReferenceNo.Name
                    e.Cancel = Not AgTemplate.ClsMain.FCheckDuplicateRefNo("ReferenceNo", "PurchChallan",
                                    TxtV_Type.Tag, TxtV_Date.Text, TxtDivision.Tag,
                                    TxtSite_Code.Tag, Topctrl1.Mode,
                                    TxtReferenceNo.Text, mInternalCode)

                Case TxtGateEntryNo.Name
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FrmSaleOrder_BaseEvent_Topctrl_tbAdd() Handles Me.BaseEvent_Topctrl_tbAdd
        TxtReferenceNo.Text = AgTemplate.ClsMain.FGetManualRefNo("ReferenceNo", "PurchChallan", TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, AgTemplate.ClsMain.ManualRefType.Max)
        TxtStructure.Tag = AgStructure.ClsMain.FGetStructureFromNCat(LblV_Type.Tag, AgL.GcnRead)
        If TxtStructure.Tag <> "" Then
            TxtStructure.Text = AgL.Dman_Execute("Select IfNull(Description,'') From Structure  Where Code = '" & TxtStructure.Tag & "'", AgL.GcnRead).ExecuteScalar
        End If
        AgCalcGrid1.AgStructure = TxtStructure.Tag

        mIsEntryLocked = False

        TxtCustomFields.Tag = AgCustomFields.ClsMain.FGetCustomFieldFromV_Type(TxtV_Type.Tag, AgL.GCn)
        AgCustomGrid1.AgCustom = TxtCustomFields.Tag
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


        IniGrid()
        TabControl1.SelectedTab = TP1

        TxtSalesTaxGroupParty.Tag = AgL.XNull(AgL.PubDtEnviro.Rows(0)("DefaultSalesTaxGroupParty"))
        TxtSalesTaxGroupParty.Text = AgL.XNull(AgL.PubDtEnviro.Rows(0)("DefaultSalesTaxGroupParty"))

        TxtCurrency.Tag = AgL.XNull(AgL.PubDtEnviro.Rows(0)("DefaultCurrency"))
        TxtCurrency.Text = AgL.XNull(AgL.Dman_Execute("Select Description From Currency Where Code = '" & TxtCurrency.Tag & "' ", AgL.GCn).ExecuteScalar)

        AgCalcGrid1.AgPostingGroupSalesTaxParty = TxtSalesTaxGroupParty.Tag

        RbtChallanDirect.Checked = True
        FAsignProcess()
    End Sub

    Private Sub Validating_ItemCode(ByVal mColumn As Integer, ByVal mRow As Integer, ByVal DrTemp As DataRow())
        Dim DtTemp As DataTable = Nothing
        Try
            If Dgl1.AgSelectedValue(mColumn, mRow) IsNot Nothing Then
                If Dgl1.Item(mColumn, mRow).Value.ToString.Trim = "" Or Dgl1.AgSelectedValue(mColumn, mRow).ToString.Trim = "" Then
                    Dgl1.Item(Col1Unit, mRow).Value = ""
                Else
                    If DrTemp IsNot Nothing Then
                        Dgl1.Item(Col1Item, mRow).Tag = AgL.XNull(DrTemp(0)("Code"))
                        Dgl1.Item(Col1Item, mRow).Value = AgL.XNull(DrTemp(0)("Description"))
                        Dgl1.Item(Col1ItemCode, mRow).Tag = AgL.XNull(DrTemp(0)("Code"))
                        Dgl1.Item(Col1ItemCode, mRow).Value = AgL.XNull(DrTemp(0)("ManualCode"))
                        Dgl1.Item(Col1Unit, mRow).Value = AgL.XNull(DrTemp(0)("Unit"))
                        Dgl1.Item(Col1QtyDecimalPlaces, mRow).Value = AgL.VNull(DrTemp(0)("QtyDecimalPlaces"))
                        Dgl1.Item(Col1MeasurePerPcs, mRow).Value = AgL.VNull(DrTemp(0)("MeasurePerPcs"))
                        Dgl1.Item(Col1MeasureUnit, mRow).Value = AgL.XNull(DrTemp(0)("MeasureUnit"))
                        Dgl1.Item(Col1MeasureDecimalPlaces, mRow).Value = AgL.VNull(DrTemp(0)("MeasureDecimalPlaces"))
                        Dgl1.Item(Col1Rate, mRow).Value = AgL.VNull(DrTemp(0)("Rate"))
                        Dgl1.Item(Col1SalesTaxGroup, mRow).Tag = AgL.XNull(DrTemp(0)("SalesTaxPostingGroup"))
                        If AgL.StrCmp(Dgl1.AgSelectedValue(Col1SalesTaxGroup, mRow), "") Then
                            Dgl1.Item(Col1SalesTaxGroup, mRow).Tag = AgL.XNull(AgL.PubDtEnviro.Rows(0)("DefaultSalesTaxGroupItem"))
                        End If
                        Dgl1.Item(Col1VNature, mRow).Value = AgL.XNull(DrTemp(0)("V_Nature"))
                        Dgl1.Item(Col1DeliveryMeasure, mRow).Value = AgL.XNull(DrTemp(0)("DeliveryMeasure"))
                        Dgl1.Item(Col1DeliveryMeasureDecimalPlaces, mRow).Value = AgL.VNull(DrTemp(0)("DeliveryMeasureDecimalPlaces"))
                        Dgl1.Item(Col1DeliveryMeasureMultiplier, mRow).Value = AgL.VNull(DrTemp(0)("DeliveryMeasureMultiplier"))
                        Dgl1.Item(Col1BillingType, mRow).Value = AgL.XNull(DrTemp(0)("BillingType"))
                        Dgl1.Item(Col1PurchOrder, mRow).Tag = AgL.XNull(DrTemp(0)("PurchOrder"))
                        Dgl1.Item(Col1PurchOrder, mRow).Value = AgL.XNull(DrTemp(0)("PurchOrderRefNo"))
                        Dgl1.Item(Col1PurchOrderSr, mRow).Value = AgL.XNull(DrTemp(0)("PurchOrderSr"))
                        Dgl1.Item(Col1DocQty, mRow).Value = AgL.VNull(DrTemp(0)("Bal.Qty"))
                        Dgl1.Item(Col1TotalDocMeasure, mRow).Value = AgL.VNull(DrTemp(0)("Bal.Measure"))
                    Else
                        If Dgl1.AgDataRow IsNot Nothing Then
                            Dgl1.Item(Col1Item, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("Code").Value)
                            Dgl1.Item(Col1Item, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("Description").Value)
                            Dgl1.Item(Col1ItemCode, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("Code").Value)
                            Dgl1.Item(Col1ItemCode, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("ManualCode").Value)
                            Dgl1.Item(Col1Unit, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("Unit").Value)
                            Dgl1.Item(Col1QtyDecimalPlaces, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("QtyDecimalPlaces").Value)
                            Dgl1.Item(Col1MeasurePerPcs, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("MeasurePerPcs").Value)
                            Dgl1.Item(Col1MeasureUnit, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("MeasureUnit").Value)
                            Dgl1.Item(Col1MeasureDecimalPlaces, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("MeasureDecimalPlaces").Value)
                            Dgl1.Item(Col1Rate, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("Rate").Value)
                            Dgl1.Item(Col1SalesTaxGroup, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("SalesTaxPostingGroup").Value)
                            If AgL.StrCmp(Dgl1.AgSelectedValue(Col1SalesTaxGroup, mRow), "") Then
                                Dgl1.Item(Col1SalesTaxGroup, mRow).Tag = AgL.XNull(AgL.PubDtEnviro.Rows(0)("DefaultSalesTaxGroupItem"))
                            End If

                            Dgl1.Item(Col1Dimension1, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("Dimension1").Value)
                            Dgl1.Item(Col1Dimension1, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("" & ClsMain.FGetDimension1Caption() & "").Value)
                            Dgl1.Item(Col1Dimension2, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("Dimension2").Value)
                            Dgl1.Item(Col1Dimension2, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("" & ClsMain.FGetDimension2Caption() & "").Value)

                            Dgl1.Item(Col1VNature, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("V_Nature").Value)
                            Dgl1.Item(Col1DeliveryMeasure, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("DeliveryMeasure").Value)
                            Dgl1.Item(Col1DeliveryMeasureDecimalPlaces, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("DeliveryMeasureDecimalPlaces").Value)
                            Dgl1.Item(Col1DeliveryMeasureMultiplier, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("DeliveryMeasureMultiplier").Value)
                            Dgl1.Item(Col1BillingType, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("BillingType").Value)
                            Dgl1.Item(Col1PurchOrder, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("PurchOrder").Value)
                            Dgl1.Item(Col1PurchOrder, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("PurchOrderRefNo").Value)
                            Dgl1.Item(Col1PurchOrderSr, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("PurchOrderSr").Value)
                            Dgl1.Item(Col1DocQty, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("Bal.Qty").Value)
                            Dgl1.Item(Col1TotalDocMeasure, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("Bal.Measure").Value)
                        End If
                    End If

                    Try
                        'Dgl1.Item(Col1DeliveryMeasure, mRow).Value = Dgl1.Item(Col1DeliveryMeasure, mRow - 1).Value
                        If mRow <> 0 Then
                            Dgl1.Item(Col1BillingType, mRow).Value = Dgl1.Item(Col1BillingType, mRow - 1).Value
                        End If
                    Catch ex As Exception
                    End Try
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " On Validating_Item Function ")
        End Try
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
                    If Dgl1.Item(Col1PurchOrder, Dgl1.CurrentCell.RowIndex).Value <> "" Then
                        Dgl1.Columns(Col1Dimension1).ReadOnly = True
                        Dgl1.Columns(Col1Dimension2).ReadOnly = True
                    Else
                        Dgl1.Columns(Col1Dimension1).ReadOnly = False
                        Dgl1.Columns(Col1Dimension2).ReadOnly = False
                    End If

            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Dgl1_EditingControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.EditingControl_KeyDown
        Dim strCond As String = ""
        Dim DrTemp As DataRow() = Nothing
        Dim bRowIndex As Integer = 0
        Dim bColumnIndex As Integer = 0
        Try
            bColumnIndex = Dgl1.CurrentCell.ColumnIndex
            bRowIndex = Dgl1.CurrentCell.RowIndex

            If Dgl1.CurrentCell Is Nothing Then Exit Sub
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Item
                    If e.KeyCode <> Keys.Enter And e.KeyCode <> Keys.Insert Then
                        If Dgl1.AgHelpDataSet(Col1Item) Is Nothing Then
                            FCreateHelpItem(Col1Item)
                        End If
                    ElseIf e.KeyCode = Keys.Insert Then
                        If RbtChallanDirect.Checked Then
                            'Dim bItemCode$ = ""
                            'bItemCode = FOpenMaster("Item Master", TxtV_Type.Tag)
                            'Dgl1.Item(Col1Item, bRowIndex).Value = ""
                            'Dgl1.Item(Col1Item, bRowIndex).Tag = ""
                            'Dgl1.CurrentCell = Dgl1.Item(Col1LotNo, bRowIndex)
                            'FCreateHelpItem(Col1Item)
                            'DrTemp = Dgl1.AgHelpDataSet(Col1Item).Tables(0).Select("Code = '" & bItemCode & "'")
                            'Dgl1.Item(Col1Item, bRowIndex).Tag = bItemCode
                            'Dgl1.Item(Col1Item, bRowIndex).Value = AgL.XNull(AgL.Dman_Execute("Select Description From Item Where Code = '" & Dgl1.Item(Col1Item, Dgl1.CurrentCell.RowIndex).Tag & "'", AgL.GCn).ExecuteScalar)
                            'Validating_ItemCode(Dgl1.Columns(Col1Item).Index, bRowIndex, DrTemp)

                            FOpenItemMaster(Dgl1.Columns(Col1Item).Index, Dgl1.CurrentCell.RowIndex)
                        End If
                    End If

                Case Col1ItemCode
                    If RbtChallanForOrder.Checked Then
                        If Dgl1.AgHelpDataSet(Col1ItemCode) Is Nothing Then
                            FCreateHelpItem(Col1ItemCode)
                        End If
                    End If

                Case Col1BillingType
                    If e.KeyCode <> Keys.Enter Then
                        mQry = " SELECT 'Qty' AS Code, 'Qty' AS Name " &
                                  " Union ALL " &
                                  " SELECT 'Measure' AS Code, 'Measure' AS Name"
                        Dgl1.AgHelpDataSet(Col1BillingType) = AgL.FillData(mQry, AgL.GCn)
                    End If

                Case Col1DeliveryMeasure
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1DeliveryMeasure) Is Nothing Then
                            mQry = " SELECT Code, Code AS Name FROM Unit Where IfNull(IsActive,1) <> 0  "
                            Dgl1.AgHelpDataSet(Col1DeliveryMeasure) = AgL.FillData(mQry, AgL.GCn)
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
                If RbtChallanForOrder.Checked Then
                    mQry = "SELECT Max(L.Item) As Code, Max(I.Description) as Description, Max(I.ManualCode) As ManualCode, " &
                            " Max(D1.Description) As " & ClsMain.FGetDimension1Caption() & ", " &
                            " Max(D2.Description) As " & ClsMain.FGetDimension2Caption() & ", " &
                            " Max(H.V_Type) || '-' ||  Max(H.ReferenceNo) AS PurchOrderRefNo, " &
                            " Max(H.V_Date) as PO_Date, Sum(L.Qty) - IfNull(Sum(Cd.Qty), 0) as [Bal.Qty], " &
                            " Max(I.Unit) as Unit, Max(L.BillingType) as BillingType, " &
                            " Sum(L.TotalMeasure) - IfNull(Sum(Cd.TotalMeasure), 0) as [Bal.Measure], " &
                            " Max(I.MeasureUnit) MeasureUnit, Max(L.Rate) as Rate, " &
                            " Max(I.SalesTaxPostingGroup) SalesTaxPostingGroup, " &
                            " Max(L.MeasurePerPcs) as MeasurePerPcs, Max(L.Dimension1) AS Dimension1, Max(L.Dimension2) AS Dimension2, " &
                            " Max(L.DeliveryMeasure) as DeliveryMeasure, Sum(L.TotalDeliveryMeasure) - IfNull(Sum(Cd.TotalDeliveryMeasure), 0) as [Bal.DeliveryMeasure], " &
                            " Max(L.DeliveryMeasurePerPcs) as DeliveryMeasurePerPcs, Max(L.DeliveryMeasureMultiplier) as DeliveryMeasureMultiplier, L.PurchOrder, L.PurchOrderSr, " &
                            " Max(U.DecimalPlaces) as QtyDecimalPlaces, Max(U1.DecimalPlaces) as MeasureDecimalPlaces, Max(U2.DecimalPlaces) as DeliveryMeasureDecimalPlaces, " &
                            " '" & RbtChallanForOrder.Text & "' AS V_Nature " &
                            " FROM (" &
                            "    SELECT DocID, V_Type, ReferenceNo, V_Date " &
                            "    FROM PurchOrder  " &
                            "    WHERE Vendor='" & TxtVendor.Tag & "' " &
                            "    And Div_Code = '" & TxtDivision.Tag & "' " &
                            "    AND Site_Code = '" & TxtSite_Code.Tag & "' " &
                            "    AND V_Date<='" & TxtV_Date.Text & "'" &
                            "    ) H " &
                            " LEFT JOIN PurchOrderDetail L  ON H.DocID = L.PurchOrder  " &
                            " Left Join Item I  On L.Item  = I.Code " &
                            " LEFT JOIN Voucher_Type Vt  ON H.V_Type = Vt.V_Type  " &
                            " Left Join ( " &
                            "    SELECT L.PurchOrder, L.PurchOrderSr, sum (L.Qty) AS Qty, Sum(L.TotalMeasure) as TotalMeasure, Sum(L.TotalDeliveryMeasure) as TotalDeliveryMeasure    " &
                            "	FROM PurchChallanDetail L   " &
                            "   Where DocId <> '" & mInternalCode & "' " &
                            "	GROUP BY L.PurchOrder, L.PurchOrderSr " &
                            "	) AS CD ON L.DocID = CD.PurchOrder AND L.Sr = CD.PurchOrderSr " &
                            " LEFT JOIN Unit U On L.Unit = U.Code " &
                            " LEFT JOIN Unit U1 On L.MeasureUnit = U1.Code " &
                            " LEFT JOIN Unit U2 On L.DeliveryMeasure = U2.Code " &
                            " Left Join Dimension1 D1 On L.Dimension1 = D1.Code " &
                            " Left Join Dimension2 D2 On L.Dimension2 = D2.Code " &
                            " WHERE 1=1  " & strCond &
                            " GROUP BY L.PurchOrder, L.PurchOrderSr " &
                            " Having Sum(L.Qty) - Sum(IfNull(Cd.Qty, 0)) > 0" &
                            " Order By Description, PO_Date "
                    Dgl1.AgHelpDataSet(Col1Item, 18) = AgL.FillData(mQry, AgL.GCn)
                Else
                    mQry = "SELECT I.Code, I.Description, I.ManualCode, " &
                            " I.Unit, I.SalesTaxPostingGroup, I.Measure As MeasurePerPcs, " &
                          " I.MeasureUnit, I.Rate, " &
                          " U.DecimalPlaces As QtyDecimalPlaces, U1.DecimalPlaces As MeasureDecimalPlaces, U1.DecimalPlaces As DeliveryMeasureDecimalPlaces, " &
                          " '' As Quot_No, '' As [Bal.Qty], '' As [Bal.Measure], '' As PurchQuotation, " &
                          " NULL As " & ClsMain.FGetDimension1Caption() & ", NULL As " & ClsMain.FGetDimension2Caption() & ", NULL AS Dimension1, NULL AS Dimension2, " &
                          " '' As PurchQuotationSr, '' As Rate, '' As PurchIndent, 0 As PurchIndentSr, '' As Indent_No, " &
                          " '' As ProdOrder, '' As ProdOrderNo, I.MeasureUnit as DeliveryMeasure, '' as [Bal.DeliveryMeasure], " &
                          " 1 As DeliveryMeasureMultiplier, Null As BillingType, " &
                          " Null As PurchOrder, Null As PurchOrderSr, Null As PurchOrderRefNo, " &
                          " '" & RbtChallanDirect.Text & "' AS V_Nature " &
                          " FROM Item I " &
                          " LEFT JOIN Unit U On I.Unit = U.Code " &
                          " LEFT JOIN Unit U1 On I.MeasureUnit = U1.Code " &
                          " Where IfNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
                    Dgl1.AgHelpDataSet(ColumnName, 32) = AgL.FillData(mQry, AgL.GCn)
                    mHelpItemQry = mQry
                End If

            Case Col1ItemCode
                If RbtChallanForOrder.Checked Then
                    mQry = "SELECT Max(L.Item) As Code, Max(I.ManualCode) As ManualCode, Max(I.Description) as Description, " &
                            " Max(D1.Description) As " & ClsMain.FGetDimension1Caption() & ", " &
                            " Max(D2.Description) As " & ClsMain.FGetDimension2Caption() & ", " &
                            " Max(H.V_Type) || '-' ||  Max(H.ReferenceNo) AS PurchOrderRefNo, " &
                            " Max(H.V_Date) as PO_Date, Sum(L.Qty) - IfNull(Sum(Cd.Qty), 0) as [Bal.Qty], " &
                            " Max(I.Unit) as Unit, " &
                            " Sum(L.TotalMeasure) - IfNull(Sum(Cd.TotalMeasure), 0) as [Bal.Measure], " &
                            " Max(I.MeasureUnit) MeasureUnit, Max(L.Rate) as Rate, " &
                            " Max(I.SalesTaxPostingGroup) SalesTaxPostingGroup, L.Dimension1, L.Dimension2, " &
                            " Max(L.MeasurePerPcs) as MeasurePerPcs, L.PurchOrder, L.PurchOrderSr, " &
                            " Max(U.DecimalPlaces) as QtyDecimalPlaces, Max(U1.DecimalPlaces) as MeasureDecimalPlaces, " &
                            " '" & RbtChallanDirect.Text & "' AS V_Nature " &
                            " FROM (" &
                            "    SELECT DocID, V_Type, ReferenceNo, V_Date " &
                            "    FROM PurchOrder  " &
                            "    WHERE Vendor='" & TxtVendor.Tag & "' " &
                            "    And Div_Code = '" & TxtDivision.Tag & "' " &
                            "    AND Site_Code = '" & TxtSite_Code.Tag & "' " &
                            "    AND V_Date<='" & TxtV_Date.Text & "'" &
                            "    ) H " &
                            " LEFT JOIN PurchOrderDetail L  ON H.DocID = L.PurchOrder  " &
                            " Left Join Item I  On L.Item  = I.Code " &
                            " LEFT JOIN Voucher_Type Vt  ON H.V_Type = Vt.V_Type  " &
                            " Left Join ( " &
                            "    SELECT L.PurchOrder, L.PurchOrderSr, sum (L.Qty) AS Qty, Sum(L.TotalMeasure) as TotalMeasure    " &
                            "	FROM PurchChallanDetail L   " &
                            "	GROUP BY L.PurchOrder, L.PurchOrderSr " &
                            "	) AS CD ON L.DocID = CD.PurchOrder AND L.Sr = CD.PurchOrderSr " &
                            " LEFT JOIN Unit U On L.Unit = U.Code " &
                            " LEFT JOIN Unit U1 On L.MeasureUnit = U1.Code " &
                            " WHERE 1=1  " & strCond &
                            " GROUP BY L.PurchOrder, L.PurchOrderSr " &
                            " Having Sum(L.Qty) - Sum(IfNull(Cd.Qty, 0)) > 0" &
                            "Order By Description, PO_Date "
                    Dgl1.AgHelpDataSet(Col1Item, 16) = AgL.FillData(mQry, AgL.GCn)
                Else
                    mQry = "SELECT I.Code, I.ManualCode, I.Description, I.Unit, I.SalesTaxPostingGroup, I.Measure As MeasurePerPcs, " &
                          " I.MeasureUnit, I.Rate, " &
                          " U.DecimalPlaces As QtyDecimalPlaces, U1.DecimalPlaces As MeasureDecimalPlaces, U1.DecimalPlaces As DeliveryMeasureDecimalPlaces, " &
                          " '' As Quot_No, '' As [Bal.Qty], '' As [Bal.Measure], '' As PurchQuotation, " &
                          " NULL As " & ClsMain.FGetDimension1Caption() & ", NULL As " & ClsMain.FGetDimension2Caption() & ", NULL AS Dimension1, NULL AS Dimension2, " &
                          " '' As PurchQuotationSr, '' As Rate, '' As PurchIndent, 0 As PurchIndentSr, '' As Indent_No, " &
                          " '' As ProdOrder, '' As ProdOrderNo, 1 As DeliveryMeasureMultiplier, Null As BillingType, " &
                          " Null As PurchOrder, Null As PurchOrderSr, Null As PurchOrderRefNo, " &
                          " '" & RbtChallanDirect.Text & "' AS V_Nature " &
                          " FROM Item I " &
                          " LEFT JOIN Unit U On I.Unit = U.Code " &
                          " LEFT JOIN Unit U1 On I.MeasureUnit = U1.Code " &
                          " Where IfNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
                    Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex, 32) = AgL.FillData(mQry, AgL.GCn)
                    mHelpItemQry = mQry
                End If
        End Select
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

        mQry = " SELECT H.SubCode, H.Name + (Case When C.CityName Is Not Null Then ',' || C.CityName Else '' End) AS [Party], " &
                " H.Currency, C1.Description As CurrencyDesc, H.Nature, H.SalesTaxPostingGroup " &
                " FROM SubGroup H  " &
                " LEFT JOIN City C ON H.CityCode = C.CityCode  " &
                " LEFT JOIN Currency C1 On H.Currency = C1.Code " &
                " Where IfNull(H.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
        sender.AgHelpDataSet(4, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
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
                Case Col1Item_Uid
                    Validating_Item_Uid(Dgl1.Item(Col1Item_Uid, mRowIndex).Value, mRowIndex)
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
                    Call FGetDeliveryMeasureMultiplier(mRowIndex)
                Case Col1TotalDocMeasure
                    Dgl1.Item(Col1TotalMeasure, Dgl1.CurrentCell.RowIndex).Value = Val(Dgl1.Item(Col1TotalDocMeasure, Dgl1.CurrentCell.RowIndex).Value)
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

        Dim DEALARR() As String = Nothing
        Dim DEALRATE As Double

        Dim MRATE As Double = 0


        LblTotalQty.Text = 0
        LblTotalMeasure.Text = 0
        LblTotalDeliveryMeasure.Text = 0
        LblTotalAmount.Text = 0


        AgCalcGrid1.AgLineGridPostingGroupSalesTaxProd = Dgl1.Columns(Col1SalesTaxGroup).Index
        AgCalcGrid1.AgPostingGroupSalesTaxParty = TxtSalesTaxGroupParty.AgSelectedValue
        AgCalcGrid1.AgPostingGroupSalesTaxItem = AgL.XNull(AgL.PubDtEnviro.Rows(0)("DefaultSalesTaxGroupItem"))
        AgCalcGrid1.AgVoucherCategory = "PURCH"

        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1Item, I).Value <> "" Then

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
                    Dgl1.Item(Col1Amount, I).Value = Format(Val(Dgl1.Item(Col1Qty, I).Value) * MRATE, "0.".PadRight(CType(Dgl1.Columns(Col1Amount), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                ElseIf AgL.StrCmp(Dgl1.Item(Col1BillingType, I).Value, "Doc Qty") Then
                    Dgl1.Item(Col1Amount, I).Value = Format(Val(Dgl1.Item(Col1DocQty, I).Value) * MRATE, "0.".PadRight(CType(Dgl1.Columns(Col1Amount), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                ElseIf AgL.StrCmp(Dgl1.Item(Col1BillingType, I).Value, "Doc Measure") Then
                    Dgl1.Item(Col1Amount, I).Value = Format(Val(Dgl1.Item(Col1TotalDocMeasure, I).Value) * MRATE, "0.".PadRight(CType(Dgl1.Columns(Col1Amount), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                Else
                    Dgl1.Item(Col1Amount, I).Value = Format(Val(Dgl1.Item(Col1Qty, I).Value) * MRATE, "0.".PadRight(CType(Dgl1.Columns(Col1Amount), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                End If

                If Dgl1.Item(Col1Item_Uid, I).Value <> "" Then
                    Dgl1.Item(Col1Qty, I).Value = 1
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
        Dim DtTemp As DataTable = Nothing
        If AgL.RequiredField(TxtVendor, LblVendor.Text) Then passed = False : Exit Sub
        If AgL.RequiredField(TxtReferenceNo, LblReferenceNo.Text) Then passed = False : Exit Sub
        'If AgL.RequiredField(TxtSalesTaxGroupParty, "Sales Tax Group") Then passed = False : Exit Sub
        If AgL.RequiredField(TxtGodown, "Godown") Then passed = False : Exit Sub


        If TxtVendorDocDate.Text <> "" Then
            If CDate(TxtVendorDocDate.Text) > CDate(TxtV_Date.Text) Then
                MsgBox("Party order date can't be greater than order date", MsgBoxStyle.Information)
                TxtVendorDocDate.Focus()
                passed = False : Exit Sub
            End If
        End If

        If AgCL.AgIsBlankGrid(Dgl1, Dgl1.Columns(Col1Item).Index) Then passed = False : Exit Sub
        If AgCL.AgIsDuplicate(Dgl1, "" + Dgl1.Columns(Col1Item).Index.ToString + "," + Dgl1.Columns(Col1LotNo).Index.ToString + "," + Dgl1.Columns(Col1BaleNo).Index.ToString + "," + Dgl1.Columns(Col1PurchOrder).Index.ToString + "," + Dgl1.Columns(Col1PurchOrderSr).Index.ToString + "," + Dgl1.Columns(Col1Item_Uid).Index.ToString + "," + Dgl1.Columns(Col1Specification).Index.ToString + "," & Dgl1.Columns(Col1Dimension1).Index & "," & Dgl1.Columns(Col1Dimension2).Index & "") Then passed = False : Exit Sub

        Dim mTampQry = " Declare @TmpTable as Table " &
              " ( " &
              " PurchOrder nVarchar(100), " &
              " PurchOrderSr INT, " &
              " ItemDesc nVarchar(100), " &
              " PurchOrderNo nVarchar(100), " &
              " Qty Float " &
              " )"

        With Dgl1
            For I = 0 To .Rows.Count - 1
                If .Item(Col1Item, I).Value <> "" Then
                    If Dgl1.Item(Col1BillingType, I).Value = "" Then
                        Dgl1.Item(Col1BillingType, I).Value = "Qty"
                    End If

                    If Val(.Item(Col1Qty, I).Value) = 0 And Val(.Item(Col1RejQty, I).Value) = 0 Then
                        MsgBox("Qty And Rejected Qty Are 0 At Row No " & Dgl1.Item(ColSNo, I).Value & "")
                        .CurrentCell = .Item(Col1Qty, I) : Dgl1.Focus()
                        passed = False : Exit Sub
                    End If

                    If Dgl1.Item(Col1Item_Uid, I).Value <> "" Then
                        Dgl1.Item(Col1Qty, I).Value = 1
                    End If

                    If Dgl1.Item(Col1PurchOrder, I).Value <> "" And Val(Dgl1.Item(Col1PurchOrderSr, I).Value) <> 0 Then
                        mTampQry += "Insert Into @TmpTable (PurchOrder, PurchOrderSr, ItemDesc, PurchOrderNo, Qty) " &
                                       " Values (" & AgL.Chk_Text(Dgl1.Item(Col1PurchOrder, I).Tag) & ", " &
                                       " " & AgL.Chk_Text(Dgl1.Item(Col1PurchOrderSr, I).Value) & ", " &
                                       " " & AgL.Chk_Text(Dgl1.Item(Col1Item, I).Value) & ", " &
                                       " " & AgL.Chk_Text(Dgl1.Item(Col1PurchOrder, I).Value) & ", " &
                                       " " & Val(Dgl1.Item(Col1Qty, I).Value) & ")"
                    End If
                End If
            Next
        End With

        mTampQry += " Select L.PurchOrder, L.PurchOrderSr, Sum(L.Qty) As Qty, Max(L.ItemDesc) As ItemDesc, Max(L.PurchOrderNo) As PurchOrderNo " &
                    " From @TmpTable L " &
                    " Group By L.PurchOrder, L.PurchOrderSr "
        DtTemp = AgL.FillData(mTampQry, AgL.GCn).Tables(0)

        If DtTemp.Rows.Count > 0 Then
            For I = 0 To DtTemp.Rows.Count - 1
                mQry = "SELECT Sum(L.Qty) - IfNull(Sum(Cd.Qty), 0) As Qty " &
                       " FROM (" &
                       "    SELECT * FROM PurchOrderDetail  " &
                       "    WHERE PurchOrder = '" & DtTemp.Rows(I)("PurchOrder") & "'" &
                       "    ) L " &
                       " LEFT JOIN ( " &
                       "    SELECT L.PurchOrder, L.PurchOrderSr, Sum (L.Qty) AS Qty " &
                       "	FROM PurchChallanDetail L   " &
                       "    Where L.DocID <> '" & mSearchCode & "' " &
                       "    And L.PurchOrder = '" & DtTemp.Rows(I)("PurchOrder") & "' " &
                       "    And L.PurchOrderSr = " & DtTemp.Rows(I)("PurchOrderSr") & "" &
                       "	GROUP BY L.PurchOrder, L.PurchOrderSr " &
                       "	) AS CD ON L.DocId = CD.PurchOrder AND L.Sr = CD.PurchOrderSr " &
                       " Where L.PurchOrder = '" & DtTemp.Rows(I)("PurchOrder") & "' " &
                       " And L.PurchOrderSr = " & DtTemp.Rows(I)("PurchOrderSr") & "" &
                       " GROUP BY L.PurchOrder, L.PurchOrderSr "
                Dim mPendingPurchOrderQty = AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar)

                If Math.Round(AgL.VNull(DtTemp.Rows(I)("Qty")), 4) > Math.Round(mPendingPurchOrderQty, 4) Then
                    If Not ChkExcessOrderQtyAllowed.Checked Then
                        MsgBox("Pending Purchase Order Qty <" & mPendingPurchOrderQty & "> For Item " & AgL.XNull(DtTemp.Rows(I)("ItemDesc")) & " And Purchase Order " & DtTemp.Rows(I)("PurchOrderNo") & " Is Less Then <" & AgL.VNull(DtTemp.Rows(I)("Qty")) & ">.", MsgBoxStyle.Information)
                        passed = False : Exit Sub
                    Else
                        If MsgBox("Pending Purchase Order Qty <" & mPendingPurchOrderQty & "> For Item " & AgL.XNull(DtTemp.Rows(I)("ItemDesc")) & " And Purchase Order " & DtTemp.Rows(I)("PurchOrderNo") & " Is Less Then <" & AgL.VNull(DtTemp.Rows(I)("Qty")) & ">.", MsgBoxStyle.YesNo, MsgBoxStyle.Question) = MsgBoxResult.No Then
                            passed = False : Exit Sub
                        End If
                    End If
                End If
            Next
        End If

        passed = AgTemplate.ClsMain.FCheckDuplicateRefNo("ReferenceNo", "PurchChallan",
                                    TxtV_Type.Tag, TxtV_Date.Text, TxtDivision.Tag,
                                    TxtSite_Code.Tag, Topctrl1.Mode,
                                    TxtReferenceNo.Text, mInternalCode)

        If TxtVendorDocNo.Text <> "" Then
            passed = ClsMain.FCheckDuplicatePartyDocNo("VendorDocNo", "PurchChallan",
                    TxtV_Type.AgSelectedValue, TxtVendorDocNo.Text, mSearchCode, "Vendor", TxtVendor.Tag)
        End If
    End Sub

    Private Sub FrmSaleOrder_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
        Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
        BtnRemoveFilter.Tag = 0
    End Sub

    Private Sub Dgl1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.KeyDown
        Try
            If Topctrl1.Mode <> "Browse" Then
                If e.Control = True And e.KeyCode = Keys.D Then
                    sender.CurrentRow.Selected = True
                End If
            End If

            If Dgl1.CurrentCell IsNot Nothing Then
                Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                    Case Col1Item
                        If e.KeyCode = Keys.Insert Then
                            'Dgl1.Item(Col1Item, Dgl1.CurrentCell.RowIndex).Tag = FOpenMaster("Item Master", TxtV_Type.Tag)
                            'Dgl1.Item(Col1Item, Dgl1.CurrentCell.RowIndex).Value = AgL.XNull(AgL.Dman_Execute("Select Description From Item Where Code = '" & Dgl1.Item(Col1Item, Dgl1.CurrentCell.RowIndex).Tag & "'", AgL.GCn).ExecuteScalar)
                            'SendKeys.Send("{Enter}")
                            FOpenItemMaster(Dgl1.Columns(Col1Item).Index, Dgl1.CurrentCell.RowIndex)
                        End If
                End Select
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ProcFillPurchOrderDetails(ByVal bPurchOrderStr As String)
        Dim DtTemp As DataTable = Nothing
        Dim bReferenceDocId$ = "", bCondStr$ = ""
        Dim I As Integer = 0
        Try
            'If Not AgL.StrCmp(Topctrl1.Mode, "Add") Then Exit Sub

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ContraV_Type")) <> "" Then
                bCondStr += " And CharIndex('|' || H.V_Type || '|','|" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ContraV_Type")) & "|') > 0 "
            End If

            mQry = "SELECT Max(L.Item) As Item, Max(I.Description) as Item_Name, Max(I.ManualCode) as ItemManualCode, Max(H.V_Type) || '-' ||  Max(H.ReferenceNo) AS PO_No, " &
                   " Max(H.V_Date) as PO_Date, Sum(L.Qty) - IfNull(Sum(Cd.Qty), 0) as [Bal.Qty], " &
                   " Sum(L.TotalMeasure) - IfNull(Sum(Cd.TotalMeasure), 0) as [Bal.Measure], " &
                   " Max(L.Unit) as Unit, Max(L.BillingType) as BillingType, Max(L.Rate) as Rate, Max(L.MeasureUnit) MeasureUnit, " &
                   " Max(L.SalesTaxGroupItem) SalesTaxGroupItem, L.PurchOrder, L.Sr As PurchOrderSr, " &
                   " Max(L.MeasurePerPcs) As MeasurePerPcs, Max(L.DeliveryMeasurePerPcs) As DeliveryMeasurePerPcs, " &
                   " Max(L.Specification) As Specification, " &
                   " Max(D1.Description) As D1Desc, Max(D2.Description) As D2Desc, " &
                   " Max(L.Dimension1) As Dimension1, Max(L.Dimension2) As Dimension2, " &
                   " Max(L.DeliveryMeasureMultiplier) as DeliveryMeasureMultiplier, Max(L.DeliveryMeasure) as DeliveryMeasure, " &
                   " Sum(L.TotalDeliveryMeasure) - IfNull(Sum(Cd.TotalDeliveryMeasure), 0) as [Bal.DeliveryMeasure], '" & RbtChallanForOrder.Text & "' AS V_Nature, " &
                   " Max(U.DecimalPlaces) As QtyDecimalPlaces, Max(U1.DecimalPlaces) As MeasureDecimalPlaces, Max(U2.DecimalPlaces) As DeliveryMeasureDecimalPlaces " &
                   " FROM (" &
                   "    SELECT DocID, V_Type, ReferenceNo, V_Date " &
                   "    FROM PurchOrder  " &
                   "    WHERE DocID In (" & bPurchOrderStr & ")" &
                   "    ) H " &
                   " LEFT JOIN PurchOrderDetail L  ON H.DocID = L.PurchOrder  " &
                   " Left Join Item I  On L.Item  = I.Code " &
                   " Left Join Dimension1 D1 On L.Dimension1 = D1.Code " &
                   " Left Join Dimension2 D2 On L.Dimension2 = D2.Code " &
                   " LEFT JOIN Voucher_Type Vt  ON H.V_Type = Vt.V_Type  " &
                   " Left Join ( " &
                   "    SELECT L.PurchOrder, L.PurchOrderSr, Sum (L.Qty) AS Qty, Sum(L.TotalMeasure) as TotalMeasure, Sum(L.TotalDeliveryMeasure) as TotalDeliveryMeasure  " &
                   "	FROM PurchChallanDetail L   " &
                   "    Where L.DocID <> '" & mInternalCode & "' " &
                   "	GROUP BY L.PurchOrder, L.PurchOrderSr " &
                   "	) AS CD ON L.DocId = CD.PurchOrder AND L.Sr = CD.PurchOrderSr " &
                   " LEFT JOIN Unit U On L.Unit = U.Code " &
                   " LEFT JOIN Unit U1 On L.MeasureUnit = U1.Code " &
                   " LEFT JOIN Unit U2 On L.DeliveryMeasure = U2.Code " &
                   " WHERE 1=1 " & bCondStr &
                   " GROUP BY L.PurchOrder, L.Sr " &
                   " Having Sum(L.Qty) - IfNull(MAx(Cd.Qty), 0) > 0  " &
                   " Order By PO_Date "

            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
            With DtTemp
                Dgl1.RowCount = 1
                Dgl1.Rows.Clear()
                If .Rows.Count > 0 Then
                    For I = 0 To .Rows.Count - 1
                        Dgl1.Rows.Add()
                        Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count - 1
                        Dgl1.Item(Col1PurchOrder, I).Tag = AgL.XNull(.Rows(I)("PurchOrder"))
                        Dgl1.Item(Col1PurchOrder, I).Value = AgL.XNull(.Rows(I)("PO_No"))
                        Dgl1.Item(Col1PurchOrderSr, I).Value = AgL.XNull(.Rows(I)("PurchOrderSr"))
                        Dgl1.Item(Col1ItemCode, I).Tag = AgL.XNull(.Rows(I)("Item"))
                        Dgl1.Item(Col1ItemCode, I).Tag = AgL.XNull(.Rows(I)("ItemManualCode"))
                        Dgl1.Item(Col1Item, I).Tag = AgL.XNull(.Rows(I)("Item"))
                        Dgl1.Item(Col1Item, I).Value = AgL.XNull(.Rows(I)("Item_Name"))
                        Dgl1.Item(Col1SalesTaxGroup, I).Tag = AgL.XNull(.Rows(I)("SalesTaxGroupItem"))
                        Dgl1.Item(Col1SalesTaxGroup, I).Value = AgL.XNull(.Rows(I)("SalesTaxGroupItem"))
                        Dgl1.Item(Col1Specification, I).Value = AgL.XNull(.Rows(I)("Specification"))

                        Dgl1.Item(Col1Dimension1, Dgl1.Rows.Count - 2).Tag = AgL.XNull(.Rows(I)("Dimension1"))
                        Dgl1.Item(Col1Dimension1, Dgl1.Rows.Count - 2).Value = AgL.XNull(.Rows(I)("D1Desc"))
                        Dgl1.Item(Col1Dimension2, Dgl1.Rows.Count - 2).Tag = AgL.XNull(.Rows(I)("Dimension2"))
                        Dgl1.Item(Col1Dimension2, Dgl1.Rows.Count - 2).Value = AgL.XNull(.Rows(I)("D2Desc"))

                        Dgl1.Item(Col1VNature, I).Value = AgL.XNull(.Rows(I)("V_Nature"))
                        Dgl1.Item(Col1BillingType, I).Value = AgL.XNull(.Rows(I)("BillingType"))
                        Dgl1.Item(Col1Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                        Dgl1.Item(Col1QtyDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("QtyDecimalPlaces"))
                        Dgl1.Item(Col1DocQty, I).Value = Format(AgL.VNull(.Rows(I)("Bal.Qty")), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
                        Dgl1.Item(Col1Qty, I).Value = Format(AgL.VNull(.Rows(I)("Bal.Qty")), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))


                        Dgl1.Item(Col1MeasureDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("MeasureDecimalPlaces"))
                        Dgl1.Item(Col1MeasureUnit, I).Value = AgL.XNull(.Rows(I)("MeasureUnit"))
                        Dgl1.Item(Col1MeasurePerPcs, I).Value = Format(AgL.VNull(.Rows(I)("MeasurePerPcs")), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                        Dgl1.Item(Col1TotalDocMeasure, I).Value = Format(AgL.VNull(.Rows(I)("Bal.Measure")), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                        Dgl1.Item(Col1TotalMeasure, I).Value = Format(AgL.VNull(.Rows(I)("Bal.Measure")), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))


                        Dgl1.Item(Col1DeliveryMeasureMultiplier, I).Value = AgL.VNull(.Rows(I)("DeliveryMeasureMultiplier"))
                        Dgl1.Item(Col1DeliveryMeasurePerPcs, I).Value = AgL.VNull(.Rows(I)("DeliveryMeasurePerPcs"))
                        Dgl1.Item(Col1DeliveryMeasureDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("DeliveryMeasureDecimalPlaces"))
                        Dgl1.Item(Col1DeliveryMeasure, I).Value = AgL.XNull(.Rows(I)("DeliveryMeasure"))
                        Dgl1.Item(Col1TotalDocDeliveryMeasure, I).Value = Format(AgL.VNull(.Rows(I)("Bal.DeliveryMeasure")), "0.".PadRight(AgL.VNull(.Rows(I)("DeliveryMeasureDecimalPlaces")) + 2, "0"))
                        Dgl1.Item(Col1TotalDeliveryMeasure, I).Value = Format(AgL.VNull(.Rows(I)("Bal.DeliveryMeasure")), "0.".PadRight(AgL.VNull(.Rows(I)("DeliveryMeasureDecimalPlaces")) + 2, "0"))


                        Dgl1.Item(Col1Rate, I).Value = Format(AgL.VNull(.Rows(I)("Rate")), "0.00")

                    Next I
                End If
            End With
            Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub BtnRemoveFilter_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnRemoveFilter.Click
        BtnRemoveFilter.Tag = 1
    End Sub

    Private Function FGetRelationalData() As Boolean
        Try

            'mQry = " DECLARE @Temp NVARCHAR(Max); "
            'mQry += " SET @Temp=''; "
            'mQry += " SELECT  @Temp=@Temp +  X.VNo || ', ' FROM (SELECT DISTINCT H.V_Type || '-' || Convert(VARCHAR,H.V_No) AS VNo FROM QCDetail   L LEFT JOIN QC  H ON L.DocId = H.DocID WHERE L.PurchChallan  = '" & TxtDocId.Text & "' And IfNull(H.IsDeleted,0)=0) AS X  "
            'mQry += " SELECT @Temp as RelationalData "
            'bRData = AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar
            'If bRData.Trim <> "" Then
            '    MsgBox(" Quality Checking " & bRData & " created against Challan No. " & TxtV_Type.Tag & "-" & TxtV_No.Text & ". Can't Modify Entry")
            '    FGetRelationalData = True
            '    Exit Function
            'End If


            'mQry = " DECLARE @Temp NVARCHAR(Max); "
            'mQry += " SET @Temp=''; "
            'mQry += " SELECT  @Temp=@Temp +  X.VNo || ', ' FROM (SELECT DISTINCT H.V_Type || '-' || Convert(VARCHAR,H.V_No) AS VNo FROM PurchInvoiceDetail   L LEFT JOIN PurchInvoice  H ON L.DocId = H.DocID WHERE L.PurchChallan  = '" & TxtDocId.Text & "'  And IfNull(H.IsDeleted,0)=0) AS X  "
            'mQry += " SELECT @Temp as RelationalData "
            'bRData = AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar
            'If bRData.Trim <> "" Then
            '    MsgBox(" Purchase Invoice " & bRData & " created against Challan No. " & TxtV_Type.Tag & "-" & TxtV_No.Text & ". Can't Modify Entry")
            '    FGetRelationalData = True
            '    Exit Function
            'End If

        Catch ex As Exception
            MsgBox(ex.Message & " in FGetRelationalData in TempRequisition")
            FGetRelationalData = True
        End Try
    End Function

    Private Sub ME_BaseEvent_Topctrl_tbEdit(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbEdit
        RbtChallanDirect.Checked = True

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

    Private Sub FrmGoodsReceipt_BaseEvent_Topctrl_tbRef() Handles Me.BaseEvent_Topctrl_tbRef
        If TxtVendor.AgHelpDataSet IsNot Nothing Then TxtVendor.AgHelpDataSet = Nothing
        If TxtGodown.AgHelpDataSet IsNot Nothing Then TxtGodown.AgHelpDataSet = Nothing
        If TxtSalesTaxGroupParty.AgHelpDataSet IsNot Nothing Then TxtSalesTaxGroupParty.AgHelpDataSet = Nothing
        If TxtCurrency.AgHelpDataSet IsNot Nothing Then TxtCurrency.AgHelpDataSet = Nothing
        If TxtForm.AgHelpDataSet IsNot Nothing Then TxtForm.AgHelpDataSet = Nothing
        If Dgl1.AgHelpDataSet(Col1Item) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1Item) = Nothing
    End Sub

    Private Sub FrmGoodsReceipt_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub

    Private Sub FrmPurchInvoice_StoreItem_BaseEvent_Topctrl_tbPrn(ByVal SearchCode As String) Handles Me.BaseEvent_Topctrl_tbPrn
        If TxtV_Type.Tag = "CPCHN" Then
            PrintCarpetChallan()
        Else
            Dim mStrOrderStatusQry As String = ""
            Dim mStrisOrderBalance As String = ""
            Dim DsTemp As DataSet

            mStrOrderStatusQry = " SELECT POD.DocID, POD.PurchOrdNo, POD.OrdDate, POD.DueDate, POD.ItemDesc, POD.OrderQty - IfNull(V1.RecQty,0) AS BalQty " &
                                    "  FROM " &
                                    "  (  " &
                                    "  SELECT H.DocID, Max(H.V_Type) || '-' || max(H.ReferenceNo) AS PurchOrdNo, Max(H.V_Date) AS OrdDate, Max(H.VendorDeliveryDate) AS DueDate," &
                                    "  max(I.Description) AS ItemDesc, L.PurchOrderSr, sum(L.Qty) AS OrderQty  " &
                                    "  FROM ( SELECT DISTINCT L.PurchOrder FROM PurchChallanDetail L WHERE L.DocId = '" & mSearchCode & "' ) M  " &
                                    "  LEFT JOIN PurchOrderDetail L ON L.PurchOrder = M.PurchOrder " &
                                    "  LEFT JOIN Item I ON I.Code = L.Item  " &
                                    "  LEFT JOIN PurchOrder H ON H.DocID = L.PurchOrder " &
                                    "  Where H.Vendor = " & AgL.Chk_Text(TxtVendor.Tag) & " " &
                                    "  AND H.Div_Code = " & AgL.Chk_Text(AgL.PubDivCode) & " AND H.Site_Code = " & AgL.Chk_Text(AgL.PubSiteCode) & " " &
                                    "  GROUP BY H.DocID, L.PurchOrderSr  " &
                                    "  ) POD  " &
                                    "  LEFT JOIN   " &
                                    "  (  " &
                                    "  SELECT PCD.PurchOrder, PCD.PurchOrderSr, sum(PCD.Qty) AS RecQty  " &
                                    "  FROM PurchChallan PC  " &
                                    "  LEFT JOIN PurchChallanDetail PCD ON PCD.DocId  = PC.DocID  " &
                                    "  WHERE IfNull(PCD.PurchOrder,'') <>''  " &
                                    "  AND PC.Vendor = " & AgL.Chk_Text(TxtVendor.Tag) & " " &
                                    "  GROUP By PCD.PurchOrder, PCD.PurchOrderSr  " &
                                    "   ) V1 ON V1.PurchOrder = POD.Docid AND V1.PurchOrderSr = POD.PurchOrderSr  " &
                                    " WHERE POD.OrderQty - IfNull(V1.RecQty,0) > 0 "
            DsTemp = AgL.FillData(mStrOrderStatusQry, AgL.GCn)
            With DsTemp.Tables(0)
                If .Rows.Count > 0 Then
                    mStrisOrderBalance = "Yes"
                Else
                    mStrisOrderBalance = "No"
                End If
            End With

            mQry = " SELECT H.DocID, H.V_Type, H.V_Date, H.ReferenceNo, " &
                " H.Currency, H.SalesTaxGroupParty, H.BillingType, H.VendorDocNo, H.VendorDocDate,  " &
                " H.Form, H.FormNo, H.Remarks, G.Description as Godown_Name, H.EntryBy, H.EntryDate, H.ApproveBy, H.ApproveDate, " &
                " L.DocId, L.Sr, L.Item, L.Specification, L.SalesTaxGroupItem, L.DocQty, L.RejQty, L.Qty, L.Unit,  U.Decimalplaces , UM.Decimalplaces AS MeasureDecimalPlaces, " &
                " L.MeasurePerPcs, L.MeasureUnit, L.TotalDocMeasure, L.TotalRejMeasure, L.TotalMeasure, L.Rate, L.Amount, L.Remark, L.LotNo, L.BaleNo, " &
                " SG.DispName AS VendorName, Sg.Add1, Sg.Add2, Sg.Add3, Sg.Mobile As VendorMobile, " &
                " L.TotalDocDeliveryMeasure, L.TotalRejDeliveryMeasure, L.TotalDeliveryMeasure, " &
                " D1.Description AS D1Desc, D2.Description AS D2Desc, E.Caption_Dimension1, E.Caption_Dimension2, " &
                " '" & mStrisOrderBalance & "' AS OrderBalance, " &
                " City.CityName As VendorCityName, I.Description AS ItemDesc, C.ReferenceNo as PurchChallanNo, PO.V_Type +'-'+ PO.ReferenceNo as PurchOrderNo,  " &
                " " & AgCalcGrid1.FLineTableFieldNameStr("L.", "L_") & " " &
                " " & AgCustomGrid1.FHeaderTableFieldNameStr("H.", "H_") & " " &
                " FROM (SELECT * FROM PurchChallan WHERE DocId = '" & mSearchCode & "') AS H  " &
                " LEFT JOIN (SELECT * FROM PurchChallanDetail WHERE DocId ='" & mSearchCode & "') AS  L ON H.DocID = L.DocId  " &
                " LEFT JOIN SubGroup Sg ON H.Vendor = Sg.SubCode " &
                " LEFT JOIN PurchChallan C ON L.PurchChallan = C.DocID " &
                " LEFT JOIN PurchOrder PO ON L.PurchOrder = PO.DocID " &
                " LEFT JOIN Item I ON L.Item = I.Code  " &
                " LEFT JOIN City ON Sg.CityCode = City.CityCode " &
                " Left Join Godown G On H.Godown = G.Code " &
                " LEFT JOIN Unit U ON U.Code = L.Unit " &
                " LEFT JOIN Unit UM ON UM.Code = L.MeasureUnit " &
                " LEFT JOIN Enviro E ON E.Site_Code = H.Site_Code AND E.Div_Code = H.Div_Code " &
                " LEFT JOIN Dimension1 D1 ON D1.Code = L.Dimension1 " &
                " LEFT JOIN Dimension2 D2 ON D2.Code = L.Dimension2 " &
                " Where H.DocId = '" & mSearchCode & "'"
            ClsMain.FPrintThisDocument(Me, TxtV_Type.Tag, mQry, "PurchChallan_Print|PurchChallanQtyMeasure_Print", "Purchase Challan", "For Qty|For Qty & Measure", mStrOrderStatusQry, "SUBREP1")
        End If

    End Sub

    Private Sub PrintCarpetChallan()
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
                " (DocId nVarChar(36), PurchOrder nVarChar(36), PurchOrderSr Integer, Item_UID NVARCHAR(2000) )"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

        mQry = " SELECT L.DocId, L.PurchOrder, L.PurchOrderSr " &
                  " FROM PurchChallanDetail L " &
                  " WHERE L.DOCID = '" & mSearchCode & "' " &
                  " GROUP BY L.DocId, L.PurchOrder, L.PurchOrderSr "
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                    mQry = " INSERT INTO [#" & bTempTable & "](DocId, PurchOrder, PurchOrderSr, Item_UID )" &
                            " SELECT '" & mSearchCode & "', '" & AgL.XNull(.Rows(I)("PurchOrder")) & "', " & AgL.VNull(.Rows(I)("PurchOrderSr")) & ", '" & mroll(mSearchCode, AgL.XNull(.Rows(I)("PurchOrder")), AgL.VNull(.Rows(I)("PurchOrderSr"))) & "' "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
                Next

            End If
        End With

        mQry = "SELECT T.DocId, T.PurchOrder, T.PurchOrderSr, T.Item_UID " &
                " From [#" & bTempTable & "] T "
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        bItemUIDJoin = "LEFT JOIN [#" & bTempTable & "] T  ON T.DocId=H.DocId AND T.PurchOrder =L.PurchOrder AND T.PurchOrderSr =L.PurchOrderSr "


        ' For Purch Order
        Dim mStrOrderStatusQry As String = ""
        Dim mStrisOrderBalance As String = ""

        mStrOrderStatusQry = " SELECT POD.DocID, POD.PurchOrdNo, POD.OrdDate, POD.DueDate, POD.ItemDesc, POD.OrderQty - IfNull(V1.RecQty,0) AS BalQty " &
                                "  FROM " &
                                "  (  " &
                                "  SELECT H.DocID, Max(H.V_Type) || '-' || max(H.ReferenceNo) AS PurchOrdNo, Max(H.V_Date) AS OrdDate, Max(H.VendorDeliveryDate) AS DueDate," &
                                "  max(I.Description) AS ItemDesc, L.PurchOrderSr, sum(L.Qty) AS OrderQty  " &
                                "  FROM ( SELECT DISTINCT L.PurchOrder FROM PurchChallanDetail L WHERE L.DocId = '" & mSearchCode & "' ) M  " &
                                "  LEFT JOIN PurchOrderDetail L ON L.PurchOrder = M.PurchOrder " &
                                "  LEFT JOIN Item I ON I.Code = L.Item  " &
                                "  LEFT JOIN PurchOrder H ON H.DocID = L.PurchOrder " &
                                "  Where H.Vendor = " & AgL.Chk_Text(TxtVendor.Tag) & " " &
                                "  AND H.Div_Code = " & AgL.Chk_Text(AgL.PubDivCode) & " AND H.Site_Code = " & AgL.Chk_Text(AgL.PubSiteCode) & " " &
                                "  GROUP BY H.DocID, L.PurchOrderSr  " &
                                "  ) POD  " &
                                "  LEFT JOIN   " &
                                "  (  " &
                                "  SELECT PCD.PurchOrder, PCD.PurchOrderSr, sum(PCD.Qty) AS RecQty  " &
                                "  FROM PurchChallan PC  " &
                                "  LEFT JOIN PurchChallanDetail PCD ON PCD.DocId  = PC.DocID  " &
                                "  WHERE IfNull(PCD.PurchOrder,'') <>''  " &
                                "  AND PC.Vendor = " & AgL.Chk_Text(TxtVendor.Tag) & " " &
                                "  GROUP By PCD.PurchOrder, PCD.PurchOrderSr  " &
                                "   ) V1 ON V1.PurchOrder = POD.Docid AND V1.PurchOrderSr = POD.PurchOrderSr  " &
                                " WHERE POD.OrderQty - IfNull(V1.RecQty,0) > 0 "
        DsTemp = AgL.FillData(mStrOrderStatusQry, AgL.GCn)
        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                mStrisOrderBalance = "Yes"
            Else
                mStrisOrderBalance = "No"
            End If
        End With


        mQry = "SELECT  H.DOCID, H.V_TYPE, H.V_DATE, H.REFERENCENO, H.VENDOR, H.CURRENCY, " &
                    " H.BILLINGTYPE, H.VENDORDOCNO, H.VENDORDOCDATE, H.REMARKS,  " &
                    " H.ENTRYBY, H.GATEENTRYNO, H.TRUCKNO,H.TRANSPORTER, H.FORM, H.FORMNO, " &
                    " L.PURCHORDER, 	L.PURCHORDERSR, L.ITEM, L.DOCQTY,	L.REJQTY, " &
                    " L.QTY, L.UNIT, L.MEASUREPERPCS,	L.MEASUREUNIT, L.TOTALDOCMEASURE, L.TOTALREJMEASURE, " &
                    " L.TOTALMEASURE,	L.RATE,	L.AMOUNT, L.LOTNO, L.REMARK AS LINEREMARK,	L.BALENO, L.BALECOUNT, L.FEETPERPCS, L.TOTALFEET, L.SPECIFICATION,  " &
                    " L.BILLINGTYPE,	L.PCSPERMEASURE,	L.PURCHCHALLAN,	L.PURCHCHALLANSR,	L.DELIVERYMEASURE,	L.DELIVERYMEASUREMULTIPLIER, " &
                    " L.TOTALDOCDELIVERYMEASURE,	L.TOTALREJDELIVERYMEASURE,	L.TOTALDELIVERYMEASURE,	L.FREEQTY,	L.TOTALFREEMEASURE, " &
                    " L.MRP,	L.NDP,	L.EXPIRYDATE,	L.TOTALFREEDELIVERYMEASURE,	T.Item_UID,	L.DELIVERYMEASUREPERPCS,	L.GROSS_AMOUNT, " &
                    " L.DISCOUNT_PRE_TAX_PER,	L.DISCOUNT_PRE_TAX,	L.OTHER_ADDITIONS_PRE_TAX_PER,	L.OTHER_ADDITIONS_PRE_TAX, " &
                    " L.SALES_TAX_TAXABLE_AMT, L.VAT_PER,	L.VAT,	L.SAT_PER,	L.SAT,	L.CST_PER,	L.CST,	L.SUB_TOTAL,	L.INSURANCE_PER, " &
                    " L.INSURANCE, L.FREIGHT,	L.HANDLING_CHARGES,	L.OTHER_CHARGES,  L.DISCOUNT_PER, L.DISCOUNT, L.ROUND_OFF_PER, " &
                    " L.ROUND_OFF, L.NET_AMOUNT, " &
                    " L.BASIC_EXCISE_DUTY_PER, L.BASIC_EXCISE_DUTY, L.EXCISE_ECESS_PER, L.EXCISE_ECESS, L.EXCISE_HECESS_PER, L.EXCISE_HECESS, L.TOTAL_EXCISE_DUTY, " &
                    " SG.DISPNAME AS VENDORNAME,SG.ADD1,SG.ADD2 ,SG.ADD3,SG.CITYCODE ,SG.MOBILE, SG.EMAIL, C.CITYNAME ,   " &
                    " I.DESCRIPTION AS ITEMNAME, IG.DESCRIPTION AS ITEMGROUP,  PO.V_TYPE AS ORDERTYPE,PO.V_NO AS ORDERNO, " &
                    " PO.REFERENCENO AS ORDERVOUCHERNO,SM.NAME AS SITENAME,  SG.TINNO AS VENDORTINNO, IG.DESCRIPTION AS ITEMGROUP, " &
                    " SGT.DISPNAME AS TRANSPORTERNAME,   G.DESCRIPTION AS GODOWNDESC,F.DESCRIPTION AS FORMDESC " &
                    " FROM ( SELECT * FROM PURCHCHALLAN  WHERE DOCID = '" & mSearchCode & "' ) AS H  " &
                    " LEFT JOIN PURCHCHALLANDETAIL L ON L.DOCID =H.DOCID  " &
                    " LEFT JOIN SUBGROUP SG ON SG.SUBCODE=H.VENDOR  " &
                    " LEFT JOIN GODOWN G ON G.CODE=H.GODOWN  " &
                    " LEFT JOIN CITY C ON C.CITYCODE =SG.CITYCODE  " &
                    " LEFT JOIN ITEM I ON I.CODE=L.ITEM  " &
                    " LEFT JOIN ITEMGROUP IG ON I.ITEMGROUP = IG.CODE  " &
                    " LEFT JOIN PURCHORDER PO ON PO.DOCID=L.PURCHORDER  " &
                    " LEFT JOIN VOUCHER_TYPE VT ON VT.V_TYPE= H.V_TYPE  " &
                    " LEFT JOIN SITEMAST SM ON SM.CODE=H.SITE_CODE  " &
                    " LEFT JOIN SUBGROUP SGT ON SGT.SUBCODE =H.TRANSPORTER   " &
                    " LEFT JOIN FORM_MASTER F ON F.CODE =H.FORM " &
                    " " & bItemUIDJoin & " "

        ClsMain.FPrintThisDocument(Me, TxtV_Type.Tag, mQry, "PurchChallan_Print|PurchChallanQtyMeasure_Print", "Purchase Challan", "For Qty|For Qty & Measure", mStrOrderStatusQry, "SUBREP1")
    End Sub

    Function mroll(ByVal bSearchCode As String, ByVal bPurchOrderDocId As String, ByVal bPurchOrderSr As Integer)
        Dim I As Integer
        Dim troll As Integer = 0
        Dim froll As Integer = 0
        Dim bCntSrl As Integer = 0
        mroll = ""

        Dim DsTemp As DataSet
        mQry = " SELECT IU.Item_UID AS BaleNo" &
                " FROM PurchChallanDetail JIRU  " &
                " LEFT JOIN Item_UID IU ON IU.Code=JIRU.Item_UID  " &
                " WHERE JIRU.DocID = '" & bSearchCode & "' AND JIRU.PurchOrder = '" & bPurchOrderDocId & "' AND JIRU.PurchOrderSr =" & bPurchOrderSr & " " &
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

    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnFillPurchOrder.Click

        If mIsEntryLocked Then Exit Sub

        If RbtChallanForOrder.Checked = True Then
            Dim strTicked As String
            strTicked = FHPGD_PendingPurchOrder()
            If strTicked <> "" Then
                ProcFillPurchOrderDetails(strTicked)
            End If
        End If


        'If AgL.StrCmp(Topctrl1.Mode, "Browse") Then Exit Sub
        'If RbtChallanForOrder.Checked Then
        '    strTicked = FHPGD_PendingPurchOrder()
        '    If strTicked <> "" Then
        '        ProcFillPurchOrderDetails(strTicked)
        '    End If
        'End If

    End Sub

    Private Function FHPGD_PendingPurchOrder() As String
        Dim FRH_Multiple As DMHelpGrid.FrmHelpGrid_Multi
        Dim StrSendText As String, bCondStr$ = ""
        Dim StrRtn As String = ""

        StrSendText = RbtChallanForOrder.Tag

        If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ContraV_Type")) <> "" Then
            bCondStr += " And CharIndex('|' || H.V_Type || '|','|" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ContraV_Type")) & "|') > 0 "
        End If

        mQry = "SELECT distinct 'o' As Tick, L.PurchOrder, Max(H.V_Type) || '-' ||  Max(H.ReferenceNo) AS PO_No, " &
                     " Max(H.V_Date) as PO_Date " &
                     " " &
                     " FROM (" &
                     "    SELECT DocID, V_Type, ReferenceNo, V_Date " &
                     "    FROM PurchOrder  " &
                     "    WHERE Vendor='" & TxtVendor.Tag & "' " &
                     "    And Div_Code = '" & TxtDivision.Tag & "' " &
                     "    AND Site_Code = '" & TxtSite_Code.Tag & "' " &
                     "    AND V_Date<='" & TxtV_Date.Text & "'" &
                     "    ) H " &
                     " LEFT JOIN PurchOrderDetail L  ON H.DocID = L.PurchOrder  " &
                     " Left Join Item I  On L.Item  = I.Code " &
                     " LEFT JOIN Voucher_Type Vt  ON H.V_Type = Vt.V_Type  " &
                     " Left Join ( " &
                     "    SELECT L.PurchOrder, L.Item, Sum(L.Qty) AS Qty  " &
                     "	  FROM PurchChallanDetail L   " &
                     "    Where L.DocID <> '" & mInternalCode & "'   " &
                     "	  GROUP BY L.PurchOrder, L.Item " &
                     "	) AS CD ON L.DocId = CD.PurchOrder AND L.Item = CD.Item " &
                     " WHERE 1=1 " & bCondStr &
                     " GROUP BY L.PurchOrder, L.PurchOrderSr " &
                     " Having Sum(L.Qty) - IfNull(Sum(Cd.Qty), 0) > 0 " &
                     " Order By PO_Date"

        FRH_Multiple = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(AgL.FillData(mQry, AgL.GCn).TABLES(0)), "", 300, 400, , , False)
        FRH_Multiple.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple.FFormatColumn(1, , 0, , False)
        FRH_Multiple.FFormatColumn(2, "Order No.", 150, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(3, "Order Date", 100, DataGridViewContentAlignment.MiddleLeft)
        'FRH_Multiple.FFormatColumn(4, "Balance Qty", 90, DataGridViewContentAlignment.MiddleRight)
        'FRH_Multiple.FFormatColumn(5, "Total Qty", 90, DataGridViewContentAlignment.MiddleRight)
        'FRH_Multiple.FFormatColumn(6, "Shipped Qty", 90, DataGridViewContentAlignment.MiddleRight)
        'FRH_Multiple.FFormatColumn(7, "Cancelled Qty", 90, DataGridViewContentAlignment.MiddleRight)

        FRH_Multiple.StartPosition = FormStartPosition.CenterScreen
        FRH_Multiple.ShowDialog()

        If FRH_Multiple.BytBtnValue = 0 Then
            StrRtn = FRH_Multiple.FFetchData(1, "'", "'", ",", True)
        End If
        FHPGD_PendingPurchOrder = StrRtn

        FRH_Multiple = Nothing
    End Function

    Private Sub BtnFillGateDetail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnFillGateDetail.Click
        FOpenGateDetail()
    End Sub

    Private Sub FOpenGateDetail()
        Dim FrmObj As FrmPurchChallanGateDetail
        Try
            If BtnFillGateDetail.Tag Is Nothing Then
                FrmObj = New FrmPurchChallanGateDetail
            Else
                FrmObj = BtnFillGateDetail.Tag
            End If
            FrmObj.DispText(IIf(Topctrl1.Mode = "Browse", False, True))
            FrmObj.ShowDialog()
            If FrmObj.mOkButtonPressed Then BtnFillGateDetail.Tag = FrmObj
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    'Private Sub Validating_Item_Uid(ByVal Item_Uid As String, ByVal mRow As Integer)
    '    Dim DrTemp As DataRow() = Nothing
    '    Dim DtTemp As DataTable = Nothing

    '    Try
    '        mQry = " SELECT I.Code, I.Description, I.Unit, I.ManualCode, I.MeasureUnit, I.Measure As MeasurePerPcs, " & _
    '               " U.DecimalPlaces as QtyDecimalPlaces, MU.DecimalPlaces as MeasureDecimalPlaces, UI.Code as ItemUIDCode " & _
    '               " FROM (Select Item, Code From Item_UID Where Item_Uid = '" & Dgl1.Item(Col1Item_Uid, mRow).Value & "') UI " & _
    '               " Left Join Item I  On UI.Item  = I.Code " & _
    '               " Left Join Unit U  On I.Unit = U.Code " & _
    '               " Left Join Unit MU  On I.MeasureUnit = MU.Code "
    '        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

    '        If DtTemp.Rows.Count > 0 Then
    '            Dgl1.Item(Col1Item_Uid, mRow).Tag = AgL.XNull(DtTemp.Rows(0)("ItemUIDCode"))
    '            Dgl1.Item(Col1ItemCode, mRow).Tag = AgL.XNull(DtTemp.Rows(0)("Code"))
    '            Dgl1.Item(Col1ItemCode, mRow).Value = AgL.XNull(DtTemp.Rows(0)("ManualCode"))
    '            Dgl1.Item(Col1Item, mRow).Tag = AgL.XNull(DtTemp.Rows(0)("Code"))
    '            Dgl1.Item(Col1Item, mRow).Value = AgL.XNull(DtTemp.Rows(0)("Description"))
    '            Dgl1.Item(Col1Qty, mRow).Value = 1
    '            Dgl1.Item(Col1Unit, mRow).Value = AgL.XNull(DtTemp.Rows(0)("Unit"))
    '            Dgl1.Item(Col1QtyDecimalPlaces, mRow).Value = AgL.VNull(DtTemp.Rows(0)("QtyDecimalPlaces"))
    '            Dgl1.Item(Col1MeasurePerPcs, mRow).Value = Format(AgL.VNull(DtTemp.Rows(0)("MeasurePerPcs")), "0.".PadRight(AgL.VNull(DtTemp.Rows(0)("MeasureDecimalPlaces")) + 2, "0"))
    '            Dgl1.Item(Col1TotalMeasure, mRow).Value = AgL.VNull(DtTemp.Rows(0)("MeasurePerPcs"))
    '            Dgl1.Item(Col1MeasureUnit, mRow).Value = AgL.XNull(DtTemp.Rows(0)("MeasureUnit"))
    '            Dgl1.Item(Col1MeasureDecimalPlaces, mRow).Value = AgL.VNull(DtTemp.Rows(0)("MeasureDecimalPlaces"))
    '        Else
    '            MsgBox("Invalid Item UID", MsgBoxStyle.Information)
    '            Dgl1.Item(Col1Item_Uid, mRow).Value = ""
    '        End If

    '    Catch ex As Exception
    '        MsgBox(ex.Message & " On Validating_Item_Uid Function ")
    '    End Try
    'End Sub

    Private Sub Validating_Item_Uid(ByVal Item_Uid As String, ByVal mRow As Integer)
        Dim DtTemp1 As DataTable = Nothing
        Dim ErrMsgStr$ = ""

        Try
            mQry = " Select Iu.Code As Item_UidCode, H.DocId As PurchOrder, H.V_Type || '-' || H.ReferenceNo As PurchOrderNo, " &
                        " L.Sr As PurchOrderSr, L.Rate, " &
                        " U.DecimalPlaces as QtyDecimalPlaces, MU.DecimalPlaces as MeasureDecimalPlaces, " &
                        " L.MeasurePerPcs, L.Unit, L.MeasureUnit, L.Item, I.Description As ItemDesc," &
                        " L.DeliveryMeasure, L.DeliveryMeasurePerPcs, L.DeliveryMeasureMultiplier, " &
                        " DMU.DecimalPlaces as DeliveryMeasureDecimalPlaces " &
                        " From PurchOrderDetail L  " &
                        " LEFT JOIN PurchOrder H  ON L.DocId = H.DocId " &
                        " LEFT JOIN Item_Uid Iu  ON L.DocId = Iu.GenDocId And L.Sr = IU.GenSr " &
                        " LEFT JOIN Item I  ON L.Item = I.Code " &
                        " Left Join Unit U  On L.Unit = U.Code " &
                        " Left Join Unit MU  On L.MeasureUnit = MU.Code " &
                        " Left Join Unit DMU  On L.DeliveryMeasure = DMU.Code " &
                        " Where Iu.Item_Uid = '" & Dgl1.Item(Col1Item_Uid, mRow).Value & "' "
            DtTemp1 = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtTemp1.Rows.Count > 0 Then
                Dgl1.Item(Col1Item_Uid, mRow).Tag = AgL.XNull(DtTemp1.Rows(0)("Item_UidCode"))
                Dgl1.Item(Col1Item, mRow).Tag = AgL.XNull(DtTemp1.Rows(0)("Item"))
                Dgl1.Item(Col1Item, mRow).Value = AgL.XNull(DtTemp1.Rows(0)("ItemDesc"))
                Dgl1.Item(Col1QtyDecimalPlaces, mRow).Value = AgL.VNull(DtTemp1.Rows(0)("QtyDecimalPlaces"))
                Dgl1.Item(Col1PurchOrder, mRow).Tag = AgL.XNull(DtTemp1.Rows(0)("PurchOrder"))
                Dgl1.Item(Col1PurchOrder, mRow).Value = AgL.XNull(DtTemp1.Rows(0)("PurchOrderNo"))
                Dgl1.Item(Col1PurchOrderSr, mRow).Value = AgL.XNull(DtTemp1.Rows(0)("PurchOrderSr"))
                Dgl1.Item(Col1DocQty, mRow).Value = 1
                Dgl1.Item(Col1Unit, mRow).Value = AgL.XNull(DtTemp1.Rows(0)("Unit"))
                Dgl1.Item(Col1MeasurePerPcs, mRow).Value = AgL.VNull(DtTemp1.Rows(0)("MeasurePerPcs"))
                Dgl1.Item(Col1MeasureUnit, mRow).Value = AgL.XNull(DtTemp1.Rows(0)("MeasureUnit"))
                Dgl1.Item(Col1MeasureDecimalPlaces, mRow).Value = AgL.VNull(DtTemp1.Rows(0)("MeasureDecimalPlaces"))
                Dgl1.Item(Col1DeliveryMeasure, mRow).Value = AgL.XNull(DtTemp1.Rows(0)("DeliveryMeasure"))
                Dgl1.Item(Col1DeliveryMeasurePerPcs, mRow).Value = AgL.VNull(DtTemp1.Rows(0)("DeliveryMeasurePerPcs"))
                Dgl1.Item(Col1DeliveryMeasureMultiplier, mRow).Value = AgL.VNull(DtTemp1.Rows(0)("DeliveryMeasureMultiplier"))
                Dgl1.Item(Col1DeliveryMeasureDecimalPlaces, mRow).Value = AgL.VNull(DtTemp1.Rows(0)("DeliveryMeasureDecimalPlaces"))
                Dgl1.Item(Col1Rate, mRow).Value = AgL.VNull(DtTemp1.Rows(0)("Rate"))
                Dgl1.Item(Col1VNature, mRow).Value = RbtChallanForOrder.Text
            Else
                MsgBox("Invalid Item UID", MsgBoxStyle.Information)
                Dgl1.Item(Col1Item_Uid, mRow).Value = ""
            End If
            Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub RbtChallanDirect_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RbtChallanDirect.Click, RbtChallanForOrder.Click
        Try
            Dgl1.AgHelpDataSet(Col1Item) = Nothing
            Dgl1.AgHelpDataSet(Col1ItemCode) = Nothing
        Catch ex As Exception
            MsgBox(ex.Message)
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
                    Dgl1.Item(Col1DeliveryMeasurePerPcs, mRow).Value = Dgl1.Item(Col1MeasurePerPcs, mRow).Value
                    Dgl1.Item(Col1DeliveryMeasure, mRow).Value = Dgl1.Item(Col1MeasureUnit, mRow).Value
                    Dgl1.Item(Col1DeliveryMeasure, mRow).Tag = Dgl1.Item(Col1MeasureUnit, mRow).Tag
                End If
            Else

                If Dgl1.Item(Col1MeasureUnit, mRow).Value <> "" And Dgl1.Item(Col1DeliveryMeasure, mRow).Value <> "" Then
                    If Dgl1.Item(Col1MeasureUnit, mRow).Value = Dgl1.Item(Col1DeliveryMeasure, mRow).Value Then
                        Dgl1.Item(Col1DeliveryMeasureMultiplier, mRow).Value = 1
                        Dgl1.Item(Col1DeliveryMeasureDecimalPlaces, mRow).Value = Dgl1.Item(Col1MeasureDecimalPlaces, mRow).Value
                    Else
                        mQry = " SELECT Multiplier, Rounding FROM UnitConversion WHERE FromUnit = '" & Dgl1.Item(Col1MeasureUnit, mRow).Value & "' AND ToUnit =  '" & Dgl1.Item(Col1DeliveryMeasure, mRow).Value & "' "
                        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
                        With DtTemp
                            If .Rows.Count > 0 Then
                                Dgl1.Item(Col1DeliveryMeasureMultiplier, mRow).Value = AgL.VNull(.Rows(0)("Multiplier"))
                                mQry = " Select DecimalPlaces From Unit Where Code = '" & Dgl1.Item(Col1DeliveryMeasure, mRow).Value & "'"
                                Dgl1.Item(Col1DeliveryMeasureDecimalPlaces, mRow).Value = AgL.VNull(AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar)

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

    Private Sub FrmPurchChallan_BaseFunction_DispText() Handles Me.BaseFunction_DispText
        'If Not AgL.StrCmp(Topctrl1.Mode, "Add") Then
        '    RbtChallanDirect.Enabled = False : RbtChallanForOrder.Enabled = False
        'Else
        '    RbtChallanDirect.Enabled = True : RbtChallanForOrder.Enabled = True
        'End If

        'If AgL.StrCmp(Topctrl1.Mode, "Browse") Then
        '    BtnFillPurchOrder.Enabled = False
        'ElseIf RbtChallanForOrder.Checked = True Then
        '    BtnFillPurchOrder.Enabled = True
        'Else
        '    BtnFillPurchOrder.Enabled = False
        'End If

        'If BlnIsDirectChallan Then
        'GrpDirectChallan.Visible = False
        'BtnFillPurchOrder.Visible = False
        'Dgl1.Columns(Col1PurchOrder).Visible = False

        'TxtGateEntryNo.Visible = False
        'LblGateEntryNo.Visible = False

        'TxtForm.Visible = False
        'LblForm.Visible = False

        'TxtFormNo.Visible = False
        'LblFormNo.Visible = False

        'BtnFillGateDetail.Visible = False
        'BtnRemoveFilter.Visible = False
        'End If

        GBoxImportFromExcel.Enabled = True

        If AgL.PubDtEnviro IsNot Nothing Then
            If AgL.PubDtEnviro.Rows.Count > 0 Then
                BtnFillPurchOrder.Visible = CType(AgL.VNull(AgL.PubDtEnviro.Rows(0)("IsVisible_PurchOrder")), Boolean)
                GrpDirectChallan.Visible = CType(AgL.VNull(AgL.PubDtEnviro.Rows(0)("IsVisible_PurchOrder")), Boolean)
            End If
        End If
    End Sub

    Private Sub BtnImprtFromExcel_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnImprtFromExcel.Click
        Dim ObjFrmImport As New FrmPurchImportFromExcel
        ObjFrmImport.LblTitle.Text = "Purchase Challan Import"
        ObjFrmImport.ImportFor = Me
        ObjFrmImport.ShowDialog()
    End Sub

    'Private Sub FShowLastRates(ByVal Item As String)
    '    Dim DtTemp As DataTable = Nothing
    '    Try
    '        mQry = " SELECT TOP 5 H.V_Date AS [Purch_Date], Sg.DispName As Vendor, L.Item, " & _
    '                    " L.Rate, L.Qty " & _
    '                    " FROM PurchInvoiceDetail L  " & _
    '                    " LEFT JOIN  PurchInvoice H ON L.DocId = H.DocId " & _
    '                    " LEFT JOIN SubGroup Sg ON H.Vendor = Sg.SubCode " & _
    '                    " Where L.Item = '" & Item & "'" & _
    '                    " And H.DocId <> '" & mSearchCode & "'" & _
    '                    " ORDER BY H.V_Date DESC	 "
    '        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

    '        If DtTemp.Rows.Count = 0 Then Dgl.DataSource = Nothing : Dgl.Visible = False : Exit Sub

    '        Dgl.DataSource = DtTemp
    '        Dgl.Visible = True

    '        Dgl.DataSource.DefaultView.RowFilter = " Item = '" & Item & "' "

    '        Me.Controls.Add(Dgl)
    '        Dgl.Left = Me.Left + 3
    '        Dgl.Top = Me.Bottom - Dgl.Height - 130
    '        Dgl.Height = 130
    '        Dgl.Width = 450
    '        Dgl.ColumnHeadersHeight = 40
    '        Dgl.AllowUserToAddRows = False
    '        If Dgl.Columns.Count > 0 Then
    '            Dgl.Columns("Purch_Date").Width = 82
    '            Dgl.Columns("Vendor").Width = 200
    '            Dgl.Columns("Rate").Width = 60
    '            Dgl.Columns("Qty").Width = 60
    '            Dgl.Columns("Purch_Date").SortMode = DataGridViewColumnSortMode.NotSortable
    '            Dgl.Columns("Rate").SortMode = DataGridViewColumnSortMode.NotSortable
    '            Dgl.Columns("Qty").SortMode = DataGridViewColumnSortMode.NotSortable
    '            'Dgl.Columns("Rate").CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleRight
    '            'Dgl.Columns("Qty").CellTemplate.Style.Alignment = DataGridViewContentAlignment.MiddleRight
    '            'Dgl.Columns("Rate").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
    '            'Dgl.Columns("Qty").HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleRight
    '            Dgl.RowHeadersVisible = False
    '            Dgl.EnableHeadersVisualStyles = False
    '            Dgl.AllowUserToResizeRows = False
    '            Dgl.ReadOnly = True
    '            Dgl.Columns("Item").Visible = False
    '            Dgl.AutoResizeRows()
    '            Dgl.AutoResizeColumnHeadersHeight()
    '            Dgl.BackgroundColor = Color.Cornsilk
    '            Dgl.ColumnHeadersDefaultCellStyle.BackColor = Color.Cornsilk
    '            Dgl.DefaultCellStyle.BackColor = Color.Cornsilk
    '            Dgl.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
    '            Dgl.CellBorderStyle = DataGridViewCellBorderStyle.None
    '            Dgl.Font = New Font(New FontFamily("Verdana"), 8)
    '            Dgl.ColumnHeadersDefaultCellStyle.Font = New Font(New FontFamily("Verdana"), 8, FontStyle.Bold)
    '            Dgl.BringToFront()
    '            Dgl.Show()
    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub

    Private Sub Dgl1_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.RowEnter
        If CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_TransactionHistory")), Boolean) = True Then
            FShowTransactionHistory(Dgl1.Item(Col1Item, e.RowIndex).Tag)
        End If

        Dim mRow = e.RowIndex
        Try
            If mPrevRowIndex <> e.RowIndex Then
                If Dgl1.Item(Col1Item, mRow).Value <> "" Then
                    If Dgl1.Item(Col1VNature, mRow).Value = RbtChallanForOrder.Text Then
                        RbtChallanForOrder.Checked = True
                    Else
                        RbtChallanDirect.Checked = True
                    End If
                End If
            End If

            mPrevRowIndex = mRow
        Catch ex As Exception
        End Try
    End Sub


    'Private Sub FShowTransactionHistory(ByVal Item As String)
    '    Dim DtTemp As DataTable = Nothing
    '    Dim CSV_Qry As String = ""
    '    Dim CSV_QryArr() As String = Nothing
    '    Dim I As Integer, J As Integer
    '    Dim IGridWidth As Integer = 0
    '    Try
    '        mQry = " SELECT TOP 5 L.Item, H.V_Date AS [Purch_Date], Sg.DispName As Vendor, " & _
    '                    " L.Rate, L.Qty " & _
    '                    " FROM PurchInvoiceDetail L  " & _
    '                    " LEFT JOIN  PurchInvoice H ON L.DocId = H.DocId " & _
    '                    " LEFT JOIN SubGroup Sg ON H.Vendor = Sg.SubCode " & _
    '                    " Where L.Item = '" & Item & "'" & _
    '                    " And H.DocId <> '" & mSearchCode & "'" & _
    '                    " ORDER BY H.V_Date DESC	 "

    '        If DtV_TypeSettings.Rows.Count <> 0 Then
    '            If AgL.XNull(DtV_TypeSettings.Rows(0)("TransactionHistory_SqlQuery")) <> "" Then
    '                mQry = AgL.XNull(DtV_TypeSettings.Rows(0)("TransactionHistory_SqlQuery"))
    '                mQry = Replace(mQry.ToString.ToUpper, "`<ITEMCODE>`", "'" & Item & "'")
    '                mQry = Replace(mQry.ToString.ToUpper, "`<SEARCHCODE>`", "'" & mSearchCode & "'")
    '            End If

    '            If AgL.XNull(DtV_TypeSettings.Rows(0)("TransactionHistory_ColumnWidthCsv")) <> "" Then
    '                CSV_Qry = AgL.XNull(DtV_TypeSettings.Rows(0)("TransactionHistory_ColumnWidthCsv"))
    '            End If
    '        End If

    '        If CSV_Qry <> "" Then CSV_QryArr = Split(CSV_Qry, ",")


    '        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

    '        If DtTemp.Rows.Count = 0 Then Dgl.DataSource = Nothing : Dgl.Visible = False : Exit Sub

    '        Dgl.DataSource = DtTemp
    '        Dgl.Visible = True

    '        Dgl.DataSource.DefaultView.RowFilter = " Item = '" & Item & "' "

    '        Me.Controls.Add(Dgl)
    '        Dgl.Left = Me.Left + 3
    '        Dgl.Top = Me.Bottom - Dgl.Height - 130
    '        Dgl.Height = 130
    '        Dgl.Width = 450
    '        Dgl.ColumnHeadersHeight = 40
    '        Dgl.AllowUserToAddRows = False
    '        If Dgl.Columns.Count > 0 Then

    '            If CSV_Qry <> "" Then J = CSV_QryArr.Length

    '            For I = 0 To Dgl.ColumnCount - 1
    '                If CSV_Qry <> "" Then
    '                    If I < J Then
    '                        If Val(CSV_QryArr(I)) > 0 Then
    '                            Dgl.Columns(I).Width = Val(CSV_QryArr(I))
    '                        Else
    '                            Dgl.Columns(I).Width = 100
    '                        End If
    '                    Else
    '                        Dgl.Columns(I).Width = 100
    '                    End If
    '                Else
    '                    Dgl.Columns(I).Width = 100
    '                End If
    '                Dgl.Columns(I).SortMode = DataGridViewColumnSortMode.NotSortable
    '                IGridWidth += Dgl.Columns(I).Width
    '            Next


    '            Dgl.Width = IGridWidth - 50

    '            'Dgl.Columns(0).Visible = False
    '            Dgl.RowHeadersVisible = False
    '            Dgl.EnableHeadersVisualStyles = False
    '            Dgl.AllowUserToResizeRows = False
    '            Dgl.ReadOnly = True
    '            Dgl.AutoResizeRows()
    '            Dgl.AutoResizeColumnHeadersHeight()
    '            Dgl.BackgroundColor = Color.Cornsilk
    '            Dgl.ColumnHeadersDefaultCellStyle.BackColor = Color.Cornsilk
    '            Dgl.DefaultCellStyle.BackColor = Color.Cornsilk
    '            Dgl.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
    '            Dgl.CellBorderStyle = DataGridViewCellBorderStyle.None
    '            Dgl.Font = New Font(New FontFamily("Verdana"), 8)
    '            Dgl.ColumnHeadersDefaultCellStyle.Font = New Font(New FontFamily("Verdana"), 8, FontStyle.Bold)
    '            Dgl.BringToFront()
    '            Dgl.Show()
    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub

    Private Sub Dgl1_Leave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Dgl1.Leave
        Dgl.Visible = False
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

    'Private Function FOpenMaster(ByVal MasterName As String, ByVal V_Type As String) As String
    '    Dim FrmObjMDI As Object
    '    Dim FrmObj As Object
    '    Dim DtTemp As DataTable = Nothing

    '    Dim StrModuleName As String = ""
    '    Dim StrMnuName As String = ""
    '    Dim StrMnuText As String = ""

    '    Try
    '        mQry = " Select * From Master_Settings Where MasterName = '" & MasterName & "' And V_Type = '" & V_Type & "' "
    '        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

    '        If DtTemp.Rows.Count = 0 Then
    '            mQry = " Select * From Master_Settings Where MasterName = '" & MasterName & "' "
    '            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
    '        End If

    '        If DtTemp.Rows.Count > 0 Then
    '            StrModuleName = AgL.XNull(DtTemp.Rows(0)("MnuAttachedInModule"))
    '            StrMnuName = AgL.XNull(DtTemp.Rows(0)("MnuName"))
    '            StrMnuText = AgL.XNull(DtTemp.Rows(0)("MnuText"))

    '            FrmObjMDI = Me.MdiParent

    '            FrmObj = FrmObjMDI.FOpenForm(StrModuleName, StrMnuName, StrMnuText)
    '            FrmObj.EntryPointIniMode = AgTemplate.ClsMain.EntryPointIniMode.Insertion
    '            FrmObj.StartPosition = FormStartPosition.CenterParent
    '            FrmObj.ShowDialog()
    '            FOpenMaster = FrmObj.mSearchCode
    '            FrmObj = Nothing
    '        Else
    '            FrmObj = Nothing
    '            FOpenMaster = Nothing
    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '        FOpenMaster = Nothing
    '    End Try
    'End Function

    Private Sub FImportFromTextFile()
        Dim Sr As StreamReader
        Dim Opn As New OpenFileDialog

        Dim Line$ = "", mDateTime$ = "", mMachine$ = "", mProcess$ = "", mJobRecBy$ = "", mBarcode$ = "", mSKU$ = ""
        Dim mDefaultGodown$ = "", mJobType$ = "", mJobWorker$ = "", mIssRec$ = "", StrQry$ = ""
        Dim ErrorLog$ = "", StrMessage$ = ""

        Dim I As Integer, J As Integer = 0, bBarCodeQty As Integer = 0
        Dim DtTemp As DataTable = Nothing
        Dim strArr() As String


        ImportMessegeStr = ""
        ImportMode = True

        Opn.ShowDialog()

        If Opn.FileName = "" Then Exit Sub

        Sr = New StreamReader(Opn.FileName)

        StrMessage = ""

        Dgl1.RowCount = 1 : Dgl1.Rows.Clear()

        Do
            I += 1
            Line = Sr.ReadLine()
            If Line IsNot Nothing Then
                strArr = Split(Line, ",")

                'If strArr.Length <> 8 Then
                '    MsgBox("Invalid records in file")
                '    Exit Sub
                'End If

                Dim Item_UidError$ = ""
                Item_UidError = FCheck_Item_UID(strArr(1))
                If Item_UidError = "" Then
                    Dgl1.Rows.Add()
                    Dgl1.Item(ColSNo, I - 1).Value = Dgl1.Rows.Count - 1
                    Dgl1.Item(Col1Item_Uid, I - 1).Value = strArr(1)
                    Validating_Item_Uid(Dgl1.Item(Col1Item_Uid, Dgl1.Rows.Count - 2).Value, Dgl1.Rows.Count - 2)
                Else
                    ImportMessegeStr += Item_UidError & vbCrLf
                End If
            End If
        Loop Until Line Is Nothing
        Sr.Close()
        Calculation()

        If ImportMessegeStr <> "" Then
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
        End If

        ImportMode = False
    End Sub

    Public Function FCheck_Item_UID(ByVal Item_UID As String) As String
        Dim Item_UidCode$ = "", ErrMsgStr$ = ""
        Dim DtTemp As DataTable = Nothing
        Dim bIssueCnt As Integer = 0

        mQry = " SELECT Code FROM Item_UID  WHERE Item_UID = '" & Item_UID & "'"
        Item_UidCode = AgL.XNull(AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar)
        If Item_UidCode = "" Then
            FCheck_Item_UID = "Carpet Id Is Not Valid."
            Exit Function
        Else
            FCheck_Item_UID = ""
        End If

        mQry = " Select I.Div_Code From Item_Uid Iu LEFT JOIN Item I ON Iu.Item = I.Code Where Iu.Code = '" & Item_UidCode & "' "
        If AgL.XNull(AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar) <> AgL.PubDivCode Then
            FCheck_Item_UID = "Carpet Id " & Item_UID & " Does Not Belong To This Division."
            Exit Function
        Else
            FCheck_Item_UID = ""
        End If

        mQry = " Select Subcode From Item_Uid WHere Code = '" & Item_UidCode & "'  "
        If AgL.XNull(AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar) <> TxtVendor.Tag Then
            FCheck_Item_UID = "Carpet Id " & Item_UID & " Is Not Issued To this Job Worker."
            Exit Function
        Else
            FCheck_Item_UID = ""
        End If
    End Function

    Private Sub BtnImportBarCode_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnImportBarCode.Click
        FImportFromTextFile()
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
        ClsMain.FGetTransactionHistory(Me, mSearchCode, mQry, Dgl, DtV_TypeSettings, ItemCode)
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
