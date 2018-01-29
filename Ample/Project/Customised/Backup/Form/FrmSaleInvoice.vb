Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Public Class FrmSaleInvoice
    Inherits AgTemplate.TempTransaction
    Dim mQry$

    Public WithEvents AgCalcGrid1 As New AgStructure.AgCalcGrid
    Public WithEvents AgCustomGrid1 As New AgCustomFields.AgCustomGrid





    '========================================================================
    '========================DATA GRID AND COLUMNS DEFINITION================
    '========================================================================
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Protected Const ColSNo As String = "S.No."
    Protected Const Col1SaleChallan As String = "Dispatch Slip"
    Protected Const Col1SaleChallanSr As String = "Sale Challan Sr"
    Protected Const Col1SaleOrder As String = "Sale Order"
    Protected Const Col1SaleOrderSr As String = "Sale Order Sr"
    Protected Const Col1Item_UID As String = "Item_UID"
    Protected Const Col1ItemCode As String = "Item Code"
    Protected Const Col1Item As String = "Item"
    Protected Const Col1SalesTaxGroup As String = "Sales Tax Group Item"
    Protected Const Col1BillingType As String = "Billing Type"
    Protected Const Col1RateType As String = "Rate Type"
    Protected Const Col1DeliveryMeasure As String = "Delivery Measure"
    Protected Const Col1BaleNo As String = "Bale No"
    Protected Const Col1Qty As String = "Qty"
    Protected Const Col1Unit As String = "Unit"
    Protected Const Col1QtyDecimalPlaces As String = "Qty Decimal Places"
    Protected Const Col1MeasurePerPcs As String = "Measure Per Pcs"
    Protected Const Col1TotalMeasure As String = "Total Measure"
    Protected Const Col1MeasureUnit As String = "Measure Unit"
    Protected Const Col1MeasureDecimalPlaces As String = "Measure Decimal Places"
    Protected Const Col1DeliveryMeasureMultiplier As String = "Delivery Measure Multiplier"
    Protected Const Col1TotalDeliveryMeasure As String = "Total Delivery Measure"
    Protected Const Col1Rate As String = "Rate"
    Protected Const Col1Amount As String = "Amount"
    Protected Const Col1Remark As String = "Remark"
    '========================================================================





    Private Const InvoiceType_Direct = "Direct"
    Private Const InvoiceType_ForOrder = "For Order"
    Private Const InvoiceType_ForChallan = "For Challan"

    Dim BlnIsMeasurePerPcsEditable As Boolean = False
    Dim BlnIsMeasureUnitEditable As Boolean = False
    Dim BlnIsMeasureEditable As Boolean = False
    Dim BlnIsMeasurePerPcsVisible As Boolean = False
    Dim BlnIsMeasureVisible As Boolean = False
    Dim BlnIsMeasureUnitVisible As Boolean = False
    Dim BlnIsDeliveryMeasureVisible As Boolean = False
    Dim BlnIsTotalDeliveryMeasureVisible As Boolean = False
    Dim BlnIsBaleNoVisible As Boolean = False
    Dim BlnIsBillingTypeVisible As Boolean = False
    Dim BlnIsItemCodeVisible As Boolean = False
    Dim BlnIsItemUIDVisible As Boolean = False
    Dim BlnIsCheckQcQty As Boolean = False
    Dim BlnIsDirectInvoice As Boolean = False
    Dim BlnIsRateTypeVisible As Boolean = False
    Protected WithEvents Label5 As System.Windows.Forms.Label
    Protected WithEvents TxtBillToParty As AgControls.AgTextBox
    Protected WithEvents Label6 As System.Windows.Forms.Label

    Dim mItemType$ = ""

    Public Sub New(ByVal StrUPVar As String, ByVal DTUP As DataTable, ByVal ItemTypeStr As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Topctrl1.FSetParent(Me, StrUPVar, DTUP)
        Topctrl1.SetDisp(True)

        If EntryNCat = "" Then EntryNCat = AgTemplate.ClsMain.Temp_NCat.SaleInvoice
        mItemType = Replace(ItemTypeStr, ",", "','")
    End Sub

#Region "Form Designer Code"
    Private Sub InitializeComponent()
        Me.Dgl1 = New AgControls.AgDataGrid
        Me.Label4 = New System.Windows.Forms.Label
        Me.TxtSaleToParty = New AgControls.AgTextBox
        Me.LblBuyer = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.LblTotalBale = New System.Windows.Forms.Label
        Me.LblTotalBaleText = New System.Windows.Forms.Label
        Me.LblTotalDeliveryMeasure = New System.Windows.Forms.Label
        Me.LblTotalDeliveryMeasureText = New System.Windows.Forms.Label
        Me.LblTotalMeasure = New System.Windows.Forms.Label
        Me.LblTotalMeasureText = New System.Windows.Forms.Label
        Me.LblTotalQty = New System.Windows.Forms.Label
        Me.LblTotalAmount = New System.Windows.Forms.Label
        Me.LblTotalQtyText = New System.Windows.Forms.Label
        Me.LblTotalAmountText = New System.Windows.Forms.Label
        Me.Pnl1 = New System.Windows.Forms.Panel
        Me.TxtStructure = New AgControls.AgTextBox
        Me.Label25 = New System.Windows.Forms.Label
        Me.TxtSalesTaxGroupParty = New AgControls.AgTextBox
        Me.Label27 = New System.Windows.Forms.Label
        Me.TxtRemarks = New AgControls.AgTextBox
        Me.Label30 = New System.Windows.Forms.Label
        Me.TxtReferenceNo = New AgControls.AgTextBox
        Me.LblReferenceNo = New System.Windows.Forms.Label
        Me.LblCurrency = New System.Windows.Forms.Label
        Me.TxtCurrency = New AgControls.AgTextBox
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel
        Me.PnlCalcGrid = New System.Windows.Forms.Panel
        Me.Label1 = New System.Windows.Forms.Label
        Me.RbtInvoiceDirect = New System.Windows.Forms.RadioButton
        Me.RbtInvoiceForChallan = New System.Windows.Forms.RadioButton
        Me.GrpDirectInvoice = New System.Windows.Forms.GroupBox
        Me.RbtInvoiceForOrder = New System.Windows.Forms.RadioButton
        Me.BtnFillSaleChallan = New System.Windows.Forms.Button
        Me.TxtCreditDays = New AgControls.AgTextBox
        Me.LblCreditDays = New System.Windows.Forms.Label
        Me.TxtCreditLimit = New AgControls.AgTextBox
        Me.LblCreditLimit = New System.Windows.Forms.Label
        Me.TxtCurrBal = New AgControls.AgTextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.LblNature = New System.Windows.Forms.Label
        Me.TxtNature = New AgControls.AgTextBox
        Me.BtnFillPartyDetail = New System.Windows.Forms.Button
        Me.PnlCustomGrid = New System.Windows.Forms.Panel
        Me.TxtCustomFields = New AgControls.AgTextBox
        Me.TxtBaleNo = New AgControls.AgTextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.TxtBillToParty = New AgControls.AgTextBox
        Me.Label6 = New System.Windows.Forms.Label
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
        Me.Label2.Location = New System.Drawing.Point(111, 32)
        Me.Label2.Tag = ""
        '
        'LblV_Date
        '
        Me.LblV_Date.BackColor = System.Drawing.Color.Transparent
        Me.LblV_Date.Location = New System.Drawing.Point(7, 27)
        Me.LblV_Date.Size = New System.Drawing.Size(78, 16)
        Me.LblV_Date.Tag = ""
        Me.LblV_Date.Text = "Invoice Date"
        '
        'LblV_TypeReq
        '
        Me.LblV_TypeReq.Location = New System.Drawing.Point(323, 12)
        Me.LblV_TypeReq.Tag = ""
        '
        'TxtV_Date
        '
        Me.TxtV_Date.AgSelectedValue = ""
        Me.TxtV_Date.BackColor = System.Drawing.Color.White
        Me.TxtV_Date.Location = New System.Drawing.Point(127, 26)
        Me.TxtV_Date.TabIndex = 2
        Me.TxtV_Date.Tag = ""
        '
        'LblV_Type
        '
        Me.LblV_Type.Location = New System.Drawing.Point(233, 8)
        Me.LblV_Type.Size = New System.Drawing.Size(78, 16)
        Me.LblV_Type.Tag = ""
        Me.LblV_Type.Text = "Invoice Type"
        '
        'TxtV_Type
        '
        Me.TxtV_Type.AgSelectedValue = ""
        Me.TxtV_Type.BackColor = System.Drawing.Color.White
        Me.TxtV_Type.Location = New System.Drawing.Point(341, 6)
        Me.TxtV_Type.Size = New System.Drawing.Size(163, 18)
        Me.TxtV_Type.TabIndex = 1
        Me.TxtV_Type.Tag = ""
        '
        'LblSite_CodeReq
        '
        Me.LblSite_CodeReq.Location = New System.Drawing.Point(111, 12)
        Me.LblSite_CodeReq.Tag = ""
        '
        'LblSite_Code
        '
        Me.LblSite_Code.BackColor = System.Drawing.Color.Transparent
        Me.LblSite_Code.Location = New System.Drawing.Point(7, 7)
        Me.LblSite_Code.Size = New System.Drawing.Size(87, 16)
        Me.LblSite_Code.Tag = ""
        Me.LblSite_Code.Text = "Branch Name"
        '
        'TxtSite_Code
        '
        Me.TxtSite_Code.AgSelectedValue = ""
        Me.TxtSite_Code.BackColor = System.Drawing.Color.White
        Me.TxtSite_Code.Location = New System.Drawing.Point(127, 6)
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
        Me.TabControl1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl1.Location = New System.Drawing.Point(-4, 17)
        Me.TabControl1.Size = New System.Drawing.Size(992, 117)
        Me.TabControl1.TabIndex = 0
        '
        'TP1
        '
        Me.TP1.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.TP1.Controls.Add(Me.Label5)
        Me.TP1.Controls.Add(Me.TxtBillToParty)
        Me.TP1.Controls.Add(Me.Label6)
        Me.TP1.Controls.Add(Me.BtnFillPartyDetail)
        Me.TP1.Controls.Add(Me.TxtCurrBal)
        Me.TP1.Controls.Add(Me.LblNature)
        Me.TP1.Controls.Add(Me.TxtNature)
        Me.TP1.Controls.Add(Me.Label3)
        Me.TP1.Controls.Add(Me.TxtCreditLimit)
        Me.TP1.Controls.Add(Me.LblCreditLimit)
        Me.TP1.Controls.Add(Me.TxtCreditDays)
        Me.TP1.Controls.Add(Me.LblCreditDays)
        Me.TP1.Controls.Add(Me.Label1)
        Me.TP1.Controls.Add(Me.Label30)
        Me.TP1.Controls.Add(Me.TxtRemarks)
        Me.TP1.Controls.Add(Me.Label4)
        Me.TP1.Controls.Add(Me.TxtSaleToParty)
        Me.TP1.Controls.Add(Me.LblBuyer)
        Me.TP1.Controls.Add(Me.TxtCurrency)
        Me.TP1.Controls.Add(Me.LblCurrency)
        Me.TP1.Controls.Add(Me.Label25)
        Me.TP1.Controls.Add(Me.TxtReferenceNo)
        Me.TP1.Controls.Add(Me.TxtStructure)
        Me.TP1.Controls.Add(Me.LblReferenceNo)
        Me.TP1.Controls.Add(Me.Label27)
        Me.TP1.Controls.Add(Me.TxtSalesTaxGroupParty)
        Me.TP1.Location = New System.Drawing.Point(4, 22)
        Me.TP1.Size = New System.Drawing.Size(984, 91)
        Me.TP1.Text = "Document Detail"
        Me.TP1.Controls.SetChildIndex(Me.TxtSalesTaxGroupParty, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label27, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblReferenceNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtStructure, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtReferenceNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label25, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblCurrency, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtCurrency, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblPrefix, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_No, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtV_No, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblBuyer, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtSaleToParty, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label4, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_TypeReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtV_Type, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_Type, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtRemarks, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblSite_CodeReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label30, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtV_Date, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label2, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtSite_Code, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_Date, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblDocId, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtDocId, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblSite_Code, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label1, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblCreditDays, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtCreditDays, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblCreditLimit, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtCreditLimit, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label3, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtNature, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblNature, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtCurrBal, 0)
        Me.TP1.Controls.SetChildIndex(Me.BtnFillPartyDetail, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label6, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtBillToParty, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label5, 0)
        '
        'Topctrl1
        '
        Me.Topctrl1.Size = New System.Drawing.Size(984, 41)
        Me.Topctrl1.TabIndex = 5
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
        Me.Label4.Location = New System.Drawing.Point(111, 53)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(10, 7)
        Me.Label4.TabIndex = 694
        Me.Label4.Text = "Ä"
        '
        'TxtSaleToParty
        '
        Me.TxtSaleToParty.AgAllowUserToEnableMasterHelp = False
        Me.TxtSaleToParty.AgMandatory = True
        Me.TxtSaleToParty.AgMasterHelp = False
        Me.TxtSaleToParty.AgNumberLeftPlaces = 8
        Me.TxtSaleToParty.AgNumberNegetiveAllow = False
        Me.TxtSaleToParty.AgNumberRightPlaces = 2
        Me.TxtSaleToParty.AgPickFromLastValue = False
        Me.TxtSaleToParty.AgRowFilter = ""
        Me.TxtSaleToParty.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtSaleToParty.AgSelectedValue = Nothing
        Me.TxtSaleToParty.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtSaleToParty.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtSaleToParty.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtSaleToParty.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtSaleToParty.Location = New System.Drawing.Point(127, 46)
        Me.TxtSaleToParty.MaxLength = 0
        Me.TxtSaleToParty.Name = "TxtSaleToParty"
        Me.TxtSaleToParty.Size = New System.Drawing.Size(349, 18)
        Me.TxtSaleToParty.TabIndex = 4
        '
        'LblBuyer
        '
        Me.LblBuyer.AutoSize = True
        Me.LblBuyer.BackColor = System.Drawing.Color.Transparent
        Me.LblBuyer.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblBuyer.Location = New System.Drawing.Point(7, 46)
        Me.LblBuyer.Name = "LblBuyer"
        Me.LblBuyer.Size = New System.Drawing.Size(39, 16)
        Me.LblBuyer.TabIndex = 693
        Me.LblBuyer.Text = "Party"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Cornsilk
        Me.Panel1.Controls.Add(Me.LblTotalBale)
        Me.Panel1.Controls.Add(Me.LblTotalBaleText)
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
        Me.Panel1.Size = New System.Drawing.Size(974, 23)
        Me.Panel1.TabIndex = 694
        '
        'LblTotalBale
        '
        Me.LblTotalBale.AutoSize = True
        Me.LblTotalBale.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalBale.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalBale.Location = New System.Drawing.Point(725, 4)
        Me.LblTotalBale.Name = "LblTotalBale"
        Me.LblTotalBale.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalBale.TabIndex = 716
        Me.LblTotalBale.Text = "."
        Me.LblTotalBale.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTotalBaleText
        '
        Me.LblTotalBaleText.AutoSize = True
        Me.LblTotalBaleText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalBaleText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalBaleText.Location = New System.Drawing.Point(633, 3)
        Me.LblTotalBaleText.Name = "LblTotalBaleText"
        Me.LblTotalBaleText.Size = New System.Drawing.Size(86, 16)
        Me.LblTotalBaleText.TabIndex = 715
        Me.LblTotalBaleText.Text = "Total Bales :"
        '
        'LblTotalDeliveryMeasure
        '
        Me.LblTotalDeliveryMeasure.AutoSize = True
        Me.LblTotalDeliveryMeasure.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalDeliveryMeasure.ForeColor = System.Drawing.Color.Black
        Me.LblTotalDeliveryMeasure.Location = New System.Drawing.Point(537, 3)
        Me.LblTotalDeliveryMeasure.Name = "LblTotalDeliveryMeasure"
        Me.LblTotalDeliveryMeasure.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalDeliveryMeasure.TabIndex = 714
        Me.LblTotalDeliveryMeasure.Text = "."
        '
        'LblTotalDeliveryMeasureText
        '
        Me.LblTotalDeliveryMeasureText.AutoSize = True
        Me.LblTotalDeliveryMeasureText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalDeliveryMeasureText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalDeliveryMeasureText.Location = New System.Drawing.Point(370, 3)
        Me.LblTotalDeliveryMeasureText.Name = "LblTotalDeliveryMeasureText"
        Me.LblTotalDeliveryMeasureText.Size = New System.Drawing.Size(161, 16)
        Me.LblTotalDeliveryMeasureText.TabIndex = 713
        Me.LblTotalDeliveryMeasureText.Text = "Total Deilvery Measure :"
        '
        'LblTotalMeasure
        '
        Me.LblTotalMeasure.AutoSize = True
        Me.LblTotalMeasure.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalMeasure.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalMeasure.Location = New System.Drawing.Point(282, 3)
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
        Me.LblTotalMeasureText.Location = New System.Drawing.Point(171, 3)
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
        Me.LblTotalAmount.Location = New System.Drawing.Point(900, 4)
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
        Me.LblTotalAmountText.Location = New System.Drawing.Point(796, 3)
        Me.LblTotalAmountText.Name = "LblTotalAmountText"
        Me.LblTotalAmountText.Size = New System.Drawing.Size(100, 16)
        Me.LblTotalAmountText.TabIndex = 661
        Me.LblTotalAmountText.Text = "Total Amount :"
        '
        'Pnl1
        '
        Me.Pnl1.Location = New System.Drawing.Point(4, 158)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(973, 227)
        Me.Pnl1.TabIndex = 3
        '
        'TxtStructure
        '
        Me.TxtStructure.AgAllowUserToEnableMasterHelp = False
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
        Me.TxtSalesTaxGroupParty.Location = New System.Drawing.Point(792, 4)
        Me.TxtSalesTaxGroupParty.MaxLength = 20
        Me.TxtSalesTaxGroupParty.Name = "TxtSalesTaxGroupParty"
        Me.TxtSalesTaxGroupParty.Size = New System.Drawing.Size(185, 18)
        Me.TxtSalesTaxGroupParty.TabIndex = 7
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.BackColor = System.Drawing.Color.Transparent
        Me.Label27.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.Location = New System.Drawing.Point(686, 6)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(104, 16)
        Me.Label27.TabIndex = 717
        Me.Label27.Text = "Sales Tax Group"
        '
        'TxtRemarks
        '
        Me.TxtRemarks.AgAllowUserToEnableMasterHelp = False
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
        Me.TxtRemarks.Location = New System.Drawing.Point(598, 44)
        Me.TxtRemarks.MaxLength = 255
        Me.TxtRemarks.Name = "TxtRemarks"
        Me.TxtRemarks.Size = New System.Drawing.Size(379, 18)
        Me.TxtRemarks.TabIndex = 11
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(510, 46)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(60, 16)
        Me.Label30.TabIndex = 723
        Me.Label30.Text = "Remarks"
        '
        'TxtReferenceNo
        '
        Me.TxtReferenceNo.AgAllowUserToEnableMasterHelp = False
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
        Me.TxtReferenceNo.Location = New System.Drawing.Point(341, 26)
        Me.TxtReferenceNo.MaxLength = 20
        Me.TxtReferenceNo.Name = "TxtReferenceNo"
        Me.TxtReferenceNo.Size = New System.Drawing.Size(163, 18)
        Me.TxtReferenceNo.TabIndex = 3
        '
        'LblReferenceNo
        '
        Me.LblReferenceNo.AutoSize = True
        Me.LblReferenceNo.BackColor = System.Drawing.Color.Transparent
        Me.LblReferenceNo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblReferenceNo.Location = New System.Drawing.Point(233, 26)
        Me.LblReferenceNo.Name = "LblReferenceNo"
        Me.LblReferenceNo.Size = New System.Drawing.Size(71, 16)
        Me.LblReferenceNo.TabIndex = 731
        Me.LblReferenceNo.Text = "Invoice No."
        '
        'LblCurrency
        '
        Me.LblCurrency.AutoSize = True
        Me.LblCurrency.BackColor = System.Drawing.Color.Transparent
        Me.LblCurrency.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCurrency.Location = New System.Drawing.Point(510, 6)
        Me.LblCurrency.Name = "LblCurrency"
        Me.LblCurrency.Size = New System.Drawing.Size(60, 16)
        Me.LblCurrency.TabIndex = 735
        Me.LblCurrency.Text = "Currency"
        '
        'TxtCurrency
        '
        Me.TxtCurrency.AgAllowUserToEnableMasterHelp = False
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
        Me.TxtCurrency.Location = New System.Drawing.Point(598, 5)
        Me.TxtCurrency.MaxLength = 20
        Me.TxtCurrency.Name = "TxtCurrency"
        Me.TxtCurrency.Size = New System.Drawing.Size(84, 18)
        Me.TxtCurrency.TabIndex = 6
        '
        'LinkLabel1
        '
        Me.LinkLabel1.BackColor = System.Drawing.Color.SteelBlue
        Me.LinkLabel1.DisabledLinkColor = System.Drawing.Color.White
        Me.LinkLabel1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel1.LinkColor = System.Drawing.Color.White
        Me.LinkLabel1.Location = New System.Drawing.Point(4, 137)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(230, 20)
        Me.LinkLabel1.TabIndex = 739
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "Sale Invoice For Following Items"
        Me.LinkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'PnlCalcGrid
        '
        Me.PnlCalcGrid.Location = New System.Drawing.Point(670, 413)
        Me.PnlCalcGrid.Name = "PnlCalcGrid"
        Me.PnlCalcGrid.Size = New System.Drawing.Size(308, 157)
        Me.PnlCalcGrid.TabIndex = 1007
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(323, 32)
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
        Me.RbtInvoiceDirect.Size = New System.Drawing.Size(64, 17)
        Me.RbtInvoiceDirect.TabIndex = 0
        Me.RbtInvoiceDirect.TabStop = True
        Me.RbtInvoiceDirect.Text = "Direct"
        Me.RbtInvoiceDirect.UseVisualStyleBackColor = True
        '
        'RbtInvoiceForChallan
        '
        Me.RbtInvoiceForChallan.AutoSize = True
        Me.RbtInvoiceForChallan.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RbtInvoiceForChallan.Location = New System.Drawing.Point(174, 8)
        Me.RbtInvoiceForChallan.Name = "RbtInvoiceForChallan"
        Me.RbtInvoiceForChallan.Size = New System.Drawing.Size(107, 17)
        Me.RbtInvoiceForChallan.TabIndex = 1
        Me.RbtInvoiceForChallan.TabStop = True
        Me.RbtInvoiceForChallan.Text = "For Dispatch"
        Me.RbtInvoiceForChallan.UseVisualStyleBackColor = True
        '
        'GrpDirectInvoice
        '
        Me.GrpDirectInvoice.BackColor = System.Drawing.Color.Transparent
        Me.GrpDirectInvoice.Controls.Add(Me.RbtInvoiceForOrder)
        Me.GrpDirectInvoice.Controls.Add(Me.RbtInvoiceDirect)
        Me.GrpDirectInvoice.Controls.Add(Me.RbtInvoiceForChallan)
        Me.GrpDirectInvoice.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GrpDirectInvoice.Location = New System.Drawing.Point(240, 131)
        Me.GrpDirectInvoice.Name = "GrpDirectInvoice"
        Me.GrpDirectInvoice.Size = New System.Drawing.Size(280, 26)
        Me.GrpDirectInvoice.TabIndex = 1
        Me.GrpDirectInvoice.TabStop = False
        '
        'RbtInvoiceForOrder
        '
        Me.RbtInvoiceForOrder.AutoSize = True
        Me.RbtInvoiceForOrder.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RbtInvoiceForOrder.Location = New System.Drawing.Point(78, 8)
        Me.RbtInvoiceForOrder.Name = "RbtInvoiceForOrder"
        Me.RbtInvoiceForOrder.Size = New System.Drawing.Size(88, 17)
        Me.RbtInvoiceForOrder.TabIndex = 3
        Me.RbtInvoiceForOrder.TabStop = True
        Me.RbtInvoiceForOrder.Text = "For Order"
        Me.RbtInvoiceForOrder.UseVisualStyleBackColor = True
        '
        'BtnFillSaleChallan
        '
        Me.BtnFillSaleChallan.Font = New System.Drawing.Font("Lucida Console", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnFillSaleChallan.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnFillSaleChallan.Location = New System.Drawing.Point(521, 136)
        Me.BtnFillSaleChallan.Margin = New System.Windows.Forms.Padding(0)
        Me.BtnFillSaleChallan.Name = "BtnFillSaleChallan"
        Me.BtnFillSaleChallan.Size = New System.Drawing.Size(34, 20)
        Me.BtnFillSaleChallan.TabIndex = 2
        Me.BtnFillSaleChallan.Text = "..."
        Me.BtnFillSaleChallan.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnFillSaleChallan.UseVisualStyleBackColor = True
        '
        'TxtCreditDays
        '
        Me.TxtCreditDays.AgAllowUserToEnableMasterHelp = False
        Me.TxtCreditDays.AgMandatory = False
        Me.TxtCreditDays.AgMasterHelp = False
        Me.TxtCreditDays.AgNumberLeftPlaces = 8
        Me.TxtCreditDays.AgNumberNegetiveAllow = False
        Me.TxtCreditDays.AgNumberRightPlaces = 0
        Me.TxtCreditDays.AgPickFromLastValue = False
        Me.TxtCreditDays.AgRowFilter = ""
        Me.TxtCreditDays.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtCreditDays.AgSelectedValue = Nothing
        Me.TxtCreditDays.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtCreditDays.AgValueType = AgControls.AgTextBox.TxtValueType.Number_Value
        Me.TxtCreditDays.BackColor = System.Drawing.Color.White
        Me.TxtCreditDays.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtCreditDays.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCreditDays.Location = New System.Drawing.Point(942, 24)
        Me.TxtCreditDays.MaxLength = 20
        Me.TxtCreditDays.Name = "TxtCreditDays"
        Me.TxtCreditDays.ReadOnly = True
        Me.TxtCreditDays.Size = New System.Drawing.Size(36, 18)
        Me.TxtCreditDays.TabIndex = 10
        Me.TxtCreditDays.TabStop = False
        Me.TxtCreditDays.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtCreditDays.UseWaitCursor = True
        '
        'LblCreditDays
        '
        Me.LblCreditDays.AutoSize = True
        Me.LblCreditDays.BackColor = System.Drawing.Color.Transparent
        Me.LblCreditDays.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCreditDays.Location = New System.Drawing.Point(863, 25)
        Me.LblCreditDays.Name = "LblCreditDays"
        Me.LblCreditDays.Size = New System.Drawing.Size(76, 16)
        Me.LblCreditDays.TabIndex = 739
        Me.LblCreditDays.Text = "Credit Days"
        '
        'TxtCreditLimit
        '
        Me.TxtCreditLimit.AgAllowUserToEnableMasterHelp = False
        Me.TxtCreditLimit.AgMandatory = False
        Me.TxtCreditLimit.AgMasterHelp = False
        Me.TxtCreditLimit.AgNumberLeftPlaces = 8
        Me.TxtCreditLimit.AgNumberNegetiveAllow = False
        Me.TxtCreditLimit.AgNumberRightPlaces = 0
        Me.TxtCreditLimit.AgPickFromLastValue = False
        Me.TxtCreditLimit.AgRowFilter = ""
        Me.TxtCreditLimit.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtCreditLimit.AgSelectedValue = Nothing
        Me.TxtCreditLimit.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtCreditLimit.AgValueType = AgControls.AgTextBox.TxtValueType.Number_Value
        Me.TxtCreditLimit.BackColor = System.Drawing.Color.White
        Me.TxtCreditLimit.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtCreditLimit.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCreditLimit.Location = New System.Drawing.Point(792, 24)
        Me.TxtCreditLimit.MaxLength = 20
        Me.TxtCreditLimit.Name = "TxtCreditLimit"
        Me.TxtCreditLimit.ReadOnly = True
        Me.TxtCreditLimit.Size = New System.Drawing.Size(69, 18)
        Me.TxtCreditLimit.TabIndex = 9
        Me.TxtCreditLimit.TabStop = False
        Me.TxtCreditLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtCreditLimit.UseWaitCursor = True
        '
        'LblCreditLimit
        '
        Me.LblCreditLimit.AutoSize = True
        Me.LblCreditLimit.BackColor = System.Drawing.Color.Transparent
        Me.LblCreditLimit.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCreditLimit.Location = New System.Drawing.Point(686, 25)
        Me.LblCreditLimit.Name = "LblCreditLimit"
        Me.LblCreditLimit.Size = New System.Drawing.Size(74, 16)
        Me.LblCreditLimit.TabIndex = 741
        Me.LblCreditLimit.Text = "Credit Limit"
        '
        'TxtCurrBal
        '
        Me.TxtCurrBal.AgAllowUserToEnableMasterHelp = False
        Me.TxtCurrBal.AgMandatory = False
        Me.TxtCurrBal.AgMasterHelp = False
        Me.TxtCurrBal.AgNumberLeftPlaces = 8
        Me.TxtCurrBal.AgNumberNegetiveAllow = False
        Me.TxtCurrBal.AgNumberRightPlaces = 2
        Me.TxtCurrBal.AgPickFromLastValue = False
        Me.TxtCurrBal.AgRowFilter = ""
        Me.TxtCurrBal.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtCurrBal.AgSelectedValue = Nothing
        Me.TxtCurrBal.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtCurrBal.AgValueType = AgControls.AgTextBox.TxtValueType.Number_Value
        Me.TxtCurrBal.BackColor = System.Drawing.Color.White
        Me.TxtCurrBal.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtCurrBal.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCurrBal.Location = New System.Drawing.Point(598, 25)
        Me.TxtCurrBal.MaxLength = 20
        Me.TxtCurrBal.Name = "TxtCurrBal"
        Me.TxtCurrBal.ReadOnly = True
        Me.TxtCurrBal.Size = New System.Drawing.Size(84, 18)
        Me.TxtCurrBal.TabIndex = 8
        Me.TxtCurrBal.TabStop = False
        Me.TxtCurrBal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.TxtCurrBal.UseWaitCursor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(510, 26)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(86, 16)
        Me.Label3.TabIndex = 743
        Me.Label3.Text = "Curr. Balance"
        '
        'LblNature
        '
        Me.LblNature.AutoSize = True
        Me.LblNature.BackColor = System.Drawing.Color.Transparent
        Me.LblNature.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblNature.Location = New System.Drawing.Point(622, 163)
        Me.LblNature.Name = "LblNature"
        Me.LblNature.Size = New System.Drawing.Size(46, 16)
        Me.LblNature.TabIndex = 745
        Me.LblNature.Text = "Nature"
        Me.LblNature.Visible = False
        '
        'TxtNature
        '
        Me.TxtNature.AgAllowUserToEnableMasterHelp = False
        Me.TxtNature.AgMandatory = False
        Me.TxtNature.AgMasterHelp = False
        Me.TxtNature.AgNumberLeftPlaces = 8
        Me.TxtNature.AgNumberNegetiveAllow = False
        Me.TxtNature.AgNumberRightPlaces = 2
        Me.TxtNature.AgPickFromLastValue = False
        Me.TxtNature.AgRowFilter = ""
        Me.TxtNature.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtNature.AgSelectedValue = Nothing
        Me.TxtNature.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtNature.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtNature.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtNature.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtNature.Location = New System.Drawing.Point(736, 162)
        Me.TxtNature.MaxLength = 20
        Me.TxtNature.Name = "TxtNature"
        Me.TxtNature.Size = New System.Drawing.Size(95, 18)
        Me.TxtNature.TabIndex = 10
        Me.TxtNature.Visible = False
        '
        'BtnFillPartyDetail
        '
        Me.BtnFillPartyDetail.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnFillPartyDetail.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnFillPartyDetail.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnFillPartyDetail.Location = New System.Drawing.Point(478, 44)
        Me.BtnFillPartyDetail.Margin = New System.Windows.Forms.Padding(0)
        Me.BtnFillPartyDetail.Name = "BtnFillPartyDetail"
        Me.BtnFillPartyDetail.Size = New System.Drawing.Size(26, 20)
        Me.BtnFillPartyDetail.TabIndex = 3000
        Me.BtnFillPartyDetail.TabStop = False
        Me.BtnFillPartyDetail.Text = "F"
        Me.BtnFillPartyDetail.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnFillPartyDetail.UseVisualStyleBackColor = True
        '
        'PnlCustomGrid
        '
        Me.PnlCustomGrid.Location = New System.Drawing.Point(4, 413)
        Me.PnlCustomGrid.Name = "PnlCustomGrid"
        Me.PnlCustomGrid.Size = New System.Drawing.Size(382, 157)
        Me.PnlCustomGrid.TabIndex = 4
        '
        'TxtCustomFields
        '
        Me.TxtCustomFields.AgAllowUserToEnableMasterHelp = False
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
        Me.TxtCustomFields.Location = New System.Drawing.Point(486, 594)
        Me.TxtCustomFields.MaxLength = 20
        Me.TxtCustomFields.Name = "TxtCustomFields"
        Me.TxtCustomFields.Size = New System.Drawing.Size(72, 18)
        Me.TxtCustomFields.TabIndex = 1011
        Me.TxtCustomFields.Text = "AgTextBox1"
        Me.TxtCustomFields.Visible = False
        '
        'TxtBaleNo
        '
        Me.TxtBaleNo.AgAllowUserToEnableMasterHelp = False
        Me.TxtBaleNo.AgMandatory = False
        Me.TxtBaleNo.AgMasterHelp = True
        Me.TxtBaleNo.AgNumberLeftPlaces = 0
        Me.TxtBaleNo.AgNumberNegetiveAllow = False
        Me.TxtBaleNo.AgNumberRightPlaces = 0
        Me.TxtBaleNo.AgPickFromLastValue = False
        Me.TxtBaleNo.AgRowFilter = ""
        Me.TxtBaleNo.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtBaleNo.AgSelectedValue = Nothing
        Me.TxtBaleNo.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtBaleNo.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtBaleNo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtBaleNo.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtBaleNo.Location = New System.Drawing.Point(388, 475)
        Me.TxtBaleNo.MaxLength = 255
        Me.TxtBaleNo.Name = "TxtBaleNo"
        Me.TxtBaleNo.Size = New System.Drawing.Size(280, 18)
        Me.TxtBaleNo.TabIndex = 1012
        Me.TxtBaleNo.Text = "TxtBaleNo"
        Me.TxtBaleNo.Visible = False
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(111, 73)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(10, 7)
        Me.Label5.TabIndex = 3003
        Me.Label5.Text = "Ä"
        '
        'TxtBillToParty
        '
        Me.TxtBillToParty.AgAllowUserToEnableMasterHelp = False
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
        Me.TxtBillToParty.Location = New System.Drawing.Point(127, 66)
        Me.TxtBillToParty.MaxLength = 0
        Me.TxtBillToParty.Name = "TxtBillToParty"
        Me.TxtBillToParty.Size = New System.Drawing.Size(377, 18)
        Me.TxtBillToParty.TabIndex = 5
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(7, 66)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(73, 16)
        Me.Label6.TabIndex = 3002
        Me.Label6.Text = "Post to A/c"
        '
        'FrmSaleInvoice
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.BackColor = System.Drawing.SystemColors.ButtonShadow
        Me.ClientSize = New System.Drawing.Size(984, 622)
        Me.Controls.Add(Me.TxtBaleNo)
        Me.Controls.Add(Me.TxtCustomFields)
        Me.Controls.Add(Me.PnlCustomGrid)
        Me.Controls.Add(Me.BtnFillSaleChallan)
        Me.Controls.Add(Me.PnlCalcGrid)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Pnl1)
        Me.Controls.Add(Me.GrpDirectInvoice)
        Me.Name = "FrmSaleInvoice"
        Me.Text = "Sale Invoice"
        Me.Controls.SetChildIndex(Me.GrpDirectInvoice, 0)
        Me.Controls.SetChildIndex(Me.TabControl1, 0)
        Me.Controls.SetChildIndex(Me.Pnl1, 0)
        Me.Controls.SetChildIndex(Me.Panel1, 0)
        Me.Controls.SetChildIndex(Me.LinkLabel1, 0)
        Me.Controls.SetChildIndex(Me.PnlCalcGrid, 0)
        Me.Controls.SetChildIndex(Me.BtnFillSaleChallan, 0)
        Me.Controls.SetChildIndex(Me.PnlCustomGrid, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.GrpUP, 0)
        Me.Controls.SetChildIndex(Me.GBoxEntryType, 0)
        Me.Controls.SetChildIndex(Me.GBoxApprove, 0)
        Me.Controls.SetChildIndex(Me.GBoxMoveToLog, 0)
        Me.Controls.SetChildIndex(Me.GroupBox2, 0)
        Me.Controls.SetChildIndex(Me.GBoxDivision, 0)
        Me.Controls.SetChildIndex(Me.Topctrl1, 0)
        Me.Controls.SetChildIndex(Me.TxtCustomFields, 0)
        Me.Controls.SetChildIndex(Me.TxtBaleNo, 0)
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
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Protected WithEvents LblBuyer As System.Windows.Forms.Label
    Protected WithEvents TxtSaleToParty As AgControls.AgTextBox
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
    Protected WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    Protected WithEvents PnlCalcGrid As System.Windows.Forms.Panel
    Protected WithEvents Label1 As System.Windows.Forms.Label
    Protected WithEvents RbtInvoiceDirect As System.Windows.Forms.RadioButton
    Protected WithEvents RbtInvoiceForChallan As System.Windows.Forms.RadioButton
    Protected WithEvents GrpDirectInvoice As System.Windows.Forms.GroupBox
    Protected WithEvents BtnFillSaleChallan As System.Windows.Forms.Button
    Protected WithEvents TxtCreditDays As AgControls.AgTextBox
    Protected WithEvents LblCreditDays As System.Windows.Forms.Label
    Protected WithEvents TxtCreditLimit As AgControls.AgTextBox
    Protected WithEvents LblCreditLimit As System.Windows.Forms.Label
    Protected WithEvents LblNature As System.Windows.Forms.Label
    Protected WithEvents TxtNature As AgControls.AgTextBox
    Protected WithEvents TxtCurrBal As AgControls.AgTextBox
    Protected WithEvents Label3 As System.Windows.Forms.Label
    Protected WithEvents BtnFillPartyDetail As System.Windows.Forms.Button
    Protected WithEvents PnlCustomGrid As System.Windows.Forms.Panel
    Protected WithEvents TxtCustomFields As AgControls.AgTextBox
    Protected WithEvents RbtInvoiceForOrder As System.Windows.Forms.RadioButton
    Protected WithEvents LblTotalDeliveryMeasure As System.Windows.Forms.Label
    Protected WithEvents LblTotalDeliveryMeasureText As System.Windows.Forms.Label
    Protected WithEvents LblTotalBale As System.Windows.Forms.Label
    Protected WithEvents LblTotalBaleText As System.Windows.Forms.Label
    Protected WithEvents TxtBaleNo As AgControls.AgTextBox
#End Region

    Private Sub FrmQuality1_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "SaleInvoice"
        LogTableName = "SaleInvoice_Log"
        MainLineTableCsv = "SaleInvoiceDetail"
        LogLineTableCsv = "SaleInvoiceDetail_Log"

        AgL.GridDesign(Dgl1)
        AgL.AddAgDataGrid(AgCalcGrid1, PnlCalcGrid)

        AgCalcGrid1.AgLibVar = AgL
        AgCalcGrid1.Visible = False

        AgL.AddAgDataGrid(AgCustomGrid1, PnlCustomGrid)

        AgCustomGrid1.AgLibVar = AgL
        AgCustomGrid1.SplitGrid = True
        AgCustomGrid1.MnuText = Me.Name
    End Sub

    Private Sub FrmQuality1_BaseFunction_FIniMast(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMast
        Dim mCondStr$

        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) & _
                        " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " And H.Div_Code = '" & AgL.PubDivCode & "' "
        mCondStr = mCondStr & " And Vt.NCat In ('" & EntryNCat & "')"

        mQry = "Select DocID As SearchCode " & _
                " From SaleInvoice H " & _
                " Left Join Voucher_Type Vt On H.V_Type = Vt.V_Type  " & _
                " Where IsNull(IsDeleted,0)=0  " & mCondStr & "  Order By V_Date Desc "
        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub FrmSaleOrder_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mCondStr$

        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) & _
                        " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " And H.Div_Code = '" & AgL.PubDivCode & "'"
        mCondStr = mCondStr & " And Vt.NCat In ('" & EntryNCat & "')"

        AgL.PubFindQry = " SELECT H.DocID AS SearchCode, Vt.Description AS [Invoice_Type], H.V_Date AS Date, SGV.DispName AS [Party], " & _
                            " H.ReferenceNo AS [Manual_No], H.Currency, H.SalesTaxGroupParty AS [Sales_Tax_Group_Party], " & _
                            " H.Remarks, H.TotalQty AS [Total_Qty], H.TotalMeasure AS [Total_Measure], H.TotalAmount AS [Total_Amount],  " & _
                            " H.EntryBy AS [Entry_By], H.EntryDate AS [Entry_Date], H.EntryType AS [Entry_Type] " & _
                            " FROM SaleInvoice H " & _
                            " LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type " & _
                            " LEFT JOIN SubGroup SGV ON SGV.SubCode  = H.SaleToParty " & _
                            " Where IsNull(H.IsDeleted,0) = 0  " & mCondStr

        AgL.PubFindQryOrdBy = "[Entry Date]"
    End Sub

    Private Sub FrmSaleOrder_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl1, Col1Item_UID, 80, 0, Col1Item_UID, BlnIsItemUIDVisible, False)
            .AddAgTextColumn(Dgl1, Col1ItemCode, 80, 0, Col1ItemCode, BlnIsItemCodeVisible, False)
            .AddAgTextColumn(Dgl1, Col1Item, 130, 0, Col1Item, True, False)
            .AddAgTextColumn(Dgl1, Col1SaleOrder, 60, 0, Col1SaleOrder, True, True)
            .AddAgTextColumn(Dgl1, Col1SaleOrderSr, 40, 5, Col1SaleOrderSr, False, True, False)
            .AddAgTextColumn(Dgl1, Col1SaleChallan, 60, 0, Col1SaleChallan, True, True)
            .AddAgTextColumn(Dgl1, Col1SaleChallanSr, 40, 5, Col1SaleChallanSr, False, True, False)
            .AddAgTextColumn(Dgl1, Col1SalesTaxGroup, 100, 0, Col1SalesTaxGroup, False, False)
            .AddAgTextColumn(Dgl1, Col1BillingType, 80, 255, Col1BillingType, BlnIsBillingTypeVisible, False)
            .AddAgTextColumn(Dgl1, Col1RateType, 100, 50, Col1RateType, BlnIsRateTypeVisible, False, False)
            .AddAgTextColumn(Dgl1, Col1DeliveryMeasure, 100, 50, Col1DeliveryMeasure, BlnIsDeliveryMeasureVisible, False, False)
            .AddAgTextColumn(Dgl1, Col1BaleNo, 60, 255, Col1BaleNo, BlnIsBaleNoVisible, False)
            .AddAgTextColumn(Dgl1, Col1QtyDecimalPlaces, 50, 0, Col1QtyDecimalPlaces, False, True, False)
            .AddAgNumberColumn(Dgl1, Col1Qty, 80, 8, 4, False, Col1Qty, True, False, True)
            .AddAgTextColumn(Dgl1, Col1Unit, 50, 0, Col1Unit, True, True)
            .AddAgNumberColumn(Dgl1, Col1MeasurePerPcs, 70, 8, 4, False, Col1MeasurePerPcs, BlnIsMeasurePerPcsVisible, Not BlnIsMeasurePerPcsEditable, True)
            .AddAgNumberColumn(Dgl1, Col1TotalMeasure, 70, 8, 4, False, Col1TotalMeasure, BlnIsMeasureVisible, Not BlnIsMeasureEditable, True)
            .AddAgTextColumn(Dgl1, Col1MeasureDecimalPlaces, 50, 0, Col1MeasureDecimalPlaces, False, True, False)
            .AddAgTextColumn(Dgl1, Col1MeasureUnit, 60, 0, Col1MeasureUnit, BlnIsMeasureUnitVisible, Not BlnIsMeasureUnitEditable)
            .AddAgNumberColumn(Dgl1, Col1DeliveryMeasureMultiplier, 100, 8, 4, False, Col1DeliveryMeasureMultiplier, False, True, True)
            .AddAgNumberColumn(Dgl1, Col1TotalDeliveryMeasure, 100, 8, 4, False, Col1TotalDeliveryMeasure, BlnIsTotalDeliveryMeasureVisible, True, True)
            .AddAgNumberColumn(Dgl1, Col1Rate, 80, 8, 4, False, Col1Rate, True, False, True)
            .AddAgNumberColumn(Dgl1, Col1Amount, 100, 8, 2, False, Col1Amount, True, True, True)
            .AddAgTextColumn(Dgl1, Col1Remark, 150, 255, Col1Remark, True, False)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 35

        AgCalcGrid1.Ini_Grid(EntryNCat, TxtV_Date.Text)


        AgCalcGrid1.AgLineGrid = Dgl1
        AgCalcGrid1.AgLineGridMandatoryColumn = Dgl1.Columns(Col1Item).Index
        AgCalcGrid1.AgLineGridGrossColumn = Dgl1.Columns(Col1Amount).Index
        AgCalcGrid1.AgLineGridPostingGroupSalesTaxProd = Dgl1.Columns(Col1SalesTaxGroup).Index
        AgCalcGrid1.AgPostingPartyAc = TxtSaleToParty.AgSelectedValue

        AgCustomGrid1.Ini_Grid(mSearchCode)
        AgCustomGrid1.SplitGrid = False

        ClsMain.ProcCreateLink(Dgl1, Col1SaleOrder)
        ClsMain.ProcCreateLink(Dgl1, Col1SaleChallan)

        Dgl1.AgSkipReadOnlyColumns = True

        If BlnIsDirectInvoice Then
            Dgl1.Columns(Col1SaleOrder).Visible = False
            Dgl1.Columns(Col1SaleChallan).Visible = False
        End If
    End Sub

    Private Sub FrmSaleOrder_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As System.Data.SqlClient.SqlConnection, ByVal Cmd As System.Data.SqlClient.SqlCommand) Handles Me.BaseEvent_Save_InTrans
        Dim I As Integer, mSr As Integer
        Dim bSelectionQry$ = "", bInvoiceType$ = ""

        If RbtInvoiceForChallan.Checked Then
            bInvoiceType = InvoiceType_ForChallan
        ElseIf RbtInvoiceForOrder.Checked Then
            bInvoiceType = InvoiceType_ForOrder
        Else
            bInvoiceType = InvoiceType_Direct
        End If

        mQry = " Update SaleInvoice " & _
                " SET  " & _
                " ReferenceNo = " & AgL.Chk_Text(TxtReferenceNo.Text) & ", " & _
                " SaleToParty = " & AgL.Chk_Text(TxtSaleToParty.Tag) & ", " & _
                " BillToParty = " & AgL.Chk_Text(TxtBillToParty.Tag) & ", " & _
                " Currency = " & AgL.Chk_Text(TxtCurrency.Tag) & ", " & _
                " SalesTaxGroupParty = " & AgL.Chk_Text(TxtSalesTaxGroupParty.Text) & ", " & _
                " Structure = " & AgL.Chk_Text(TxtStructure.Tag) & ", " & _
                " Remarks = " & AgL.Chk_Text(TxtRemarks.Text) & ", " & _
                " CreditDays = " & Val(TxtCreditDays.Text) & ", " & _
                " CreditLimit = " & Val(TxtCreditLimit.Text) & ", " & _
                " CustomFields = " & AgL.Chk_Text(TxtCustomFields.Tag) & ", " & _
                " InvoiceGenType = " & AgL.Chk_Text(bInvoiceType) & ", " & _
                " BaleNoStr = " & AgL.Chk_Text(TxtBaleNo.Text) & ", " & _
                " TotalQty = " & Val(LblTotalQty.Text) & ", " & _
                " TotalAmount = " & Val(LblTotalAmount.Text) & ", " & _
                " TotalBale = " & Val(LblTotalBale.Text) & ", " & _
                " TotalMeasure = " & Val(LblTotalMeasure.Text) & ", " & _
                " TotalDeliveryMeasure = " & Val(LblTotalDeliveryMeasure.Text) & ", " & _
                " " & AgCalcGrid1.FFooterTableUpdateStr() & " " & _
                " " & AgCustomGrid1.FFooterTableUpdateStr() & " " & _
                " Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        Call FSavePartyDetail(Conn, Cmd)

        mQry = "Delete From SaleInvoiceDetail Where DocId = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Insert Into SaleInvoiceDetail(DocId, Sr, SaleChallan, SaleChallanSr, SaleOrder, SaleOrderSr, Item_Uid, Item, SalesTaxGroupItem, " & _
                " Qty, Unit, MeasurePerPcs, MeasureUnit, " & _
                " TotalMeasure, Rate, Amount, Remark, SaleInvoice, SaleInvoiceSr, BillingType, RateType, BaleNo, DeliveryMeasure, DeliveryMeasureMultiplier, TotalDeliveryMeasure, " & AgCalcGrid1.FLineTableFieldNameStr() & ") "

        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1Item, I).Value <> "" Then
                mSr += 1
                If bSelectionQry <> "" Then bSelectionQry += " UNION ALL "
                bSelectionQry += " Select " & AgL.Chk_Text(mSearchCode) & ", " & mSr & ", " & _
                                    " " & AgL.Chk_Text(Dgl1.Item(Col1SaleChallan, I).Tag) & ", " & _
                                    " " & Val(Dgl1.Item(Col1SaleChallanSr, I).Value) & ", " & _
                                    " " & AgL.Chk_Text(Dgl1.Item(Col1SaleOrder, I).Tag) & ", " & _
                                    " " & Val(Dgl1.Item(Col1SaleOrderSr, I).Value) & ", " & _
                                    " " & AgL.Chk_Text(Dgl1.Item(Col1Item_UID, I).Tag) & ", " & _
                                    " " & AgL.Chk_Text(Dgl1.Item(Col1Item, I).Tag) & ", " & _
                                    " " & AgL.Chk_Text(Dgl1.Item(Col1SalesTaxGroup, I).Tag) & ", " & _
                                    " " & Val(Dgl1.Item(Col1Qty, I).Value) & ", " & _
                                    " " & AgL.Chk_Text(Dgl1.Item(Col1Unit, I).Value) & ", " & _
                                    " " & Val(Dgl1.Item(Col1MeasurePerPcs, I).Value) & ", " & _
                                    " " & AgL.Chk_Text(Dgl1.Item(Col1MeasureUnit, I).Value) & ", " & _
                                    " " & Val(Dgl1.Item(Col1TotalMeasure, I).Value) & ", " & _
                                    " " & Val(Dgl1.Item(Col1Rate, I).Value) & ", " & _
                                    " " & Val(Dgl1.Item(Col1Amount, I).Value) & ", " & _
                                    " " & AgL.Chk_Text(Dgl1.Item(Col1Remark, I).Value) & ", " & _
                                    " " & AgL.Chk_Text(mSearchCode) & ", " & _
                                    " " & mSr & ", " & _
                                    " " & AgL.Chk_Text(Dgl1.Item(Col1BillingType, I).Value) & " , " & _
                                    " " & AgL.Chk_Text(Dgl1.Item(Col1RateType, I).Value) & ", " & _
                                    " " & AgL.Chk_Text(Dgl1.Item(Col1BaleNo, I).Value) & " , " & _
                                    " " & AgL.Chk_Text(Dgl1.Item(Col1DeliveryMeasure, I).Value) & ", " & _
                                    " " & Val(Dgl1.Item(Col1DeliveryMeasureMultiplier, I).Value) & ", " & _
                                    " " & Val(Dgl1.Item(Col1TotalDeliveryMeasure, I).Value) & ", " & _
                                    " " & AgCalcGrid1.FLineTableFieldValuesStr(I) & " "
            End If
        Next
        mQry = mQry + bSelectionQry
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        Call FPostInSaleChallan(Conn, Cmd)

        Call ClsMain.PostStructureToAccounts(AgCalcGrid1, TxtRemarks.Text, mSearchCode, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, TxtDivision.AgSelectedValue, _
                                             TxtV_Type.AgSelectedValue, LblPrefix.Text, TxtV_No.Text, TxtReferenceNo.Text, TxtBillToParty.AgSelectedValue, TxtV_Date.Text, Conn, Cmd)

        'Call AccountPosting()


    End Sub

    Private Sub FrmSaleOrder_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim I As Integer

        Dim DsTemp As DataSet

        mQry = "Select H.*, Sg.Name + ',' + IsNull(C1.CityName,'') As SaleToPartyDesc, " & _
               "BillToParty.Name + ',' + IsNull(BillToPartyCity.CityName,'') As BillToPartyDesc, " & _
               "C.Description As CurrencyDesc, C1.CityName As SaleToPartyCityName " & _
               " From (Select * From SaleInvoice With (NoLock) Where DocID='" & SearchCode & "') H " & _
               " LEFT JOIN SubGroup Sg With (NoLock) ON H.SaleToParty = Sg.SubCode " & _
               " LEFT JOIN SubGroup BillToParty With (NoLock) ON H.BillToParty = BillToParty.SubCode " & _
               " LEFT JOIN Currency C With (NoLock) ON H.Currency = C.Code " & _
               " LEFT JOIN City C1 With (NoLock) On H.SaleToPartyCity = C1.CityCode " & _
               " LEFT JOIN City BillToPartyCity With (NoLock) On BillToParty.CityCode = BillToPartyCity.CityCode "
        DsTemp = AgL.FillData(mQry, AgL.GCn)


        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                TxtStructure.AgSelectedValue = AgStructure.ClsMain.FGetStructureFromNCat(LblV_Type.Tag, AgL.GcnRead)
                TxtCustomFields.AgSelectedValue = AgCustomFields.ClsMain.FGetCustomFieldFromV_Type(TxtV_Type.AgSelectedValue, AgL.GcnRead)

                If AgL.XNull(.Rows(0)("Structure")) <> "" Then
                    TxtStructure.Tag = AgL.XNull(.Rows(0)("Structure"))
                End If
                AgCalcGrid1.FrmType = Me.FrmType
                AgCalcGrid1.AgStructure = TxtStructure.Tag

                If AgL.XNull(.Rows(0)("CustomFields")) <> "" Then
                    TxtCustomFields.AgSelectedValue = AgL.XNull(.Rows(0)("CustomFields"))
                End If
                AgCustomGrid1.FrmType = Me.FrmType
                AgCustomGrid1.AgCustom = TxtCustomFields.AgSelectedValue

                IniGrid()

                TxtReferenceNo.Text = AgL.XNull(.Rows(0)("ReferenceNo"))
                TxtSaleToParty.Tag = AgL.XNull(.Rows(0)("SaleToParty"))
                TxtSaleToParty.Text = AgL.XNull(.Rows(0)("SaleToPartyDesc"))
                TxtBillToParty.Tag = AgL.XNull(.Rows(0)("BillToParty"))
                TxtBillToParty.Text = AgL.XNull(.Rows(0)("BillToPartyDesc"))
                TxtCurrency.Tag = AgL.XNull(.Rows(0)("Currency"))
                TxtCurrency.Text = AgL.XNull(.Rows(0)("CurrencyDesc"))

                TxtBaleNo.Text = AgL.XNull(.Rows(0)("BaleNoStr"))

                Call FGetCurrBal(TxtSaleToParty.AgSelectedValue)

                If AgL.XNull(.Rows(0)("InvoiceGenType")) = InvoiceType_ForChallan Then
                    RbtInvoiceForChallan.Checked = True
                ElseIf AgL.XNull(.Rows(0)("InvoiceGenType")) = InvoiceType_ForOrder Then
                    RbtInvoiceForOrder.Checked = True
                Else
                    RbtInvoiceDirect.Checked = True
                End If

                TxtSalesTaxGroupParty.Tag = AgL.XNull(.Rows(0)("SalesTaxGroupParty"))
                TxtSalesTaxGroupParty.Text = AgL.XNull(.Rows(0)("SalesTaxGroupParty"))
                TxtRemarks.Text = AgL.XNull(.Rows(0)("Remarks"))
                TxtCreditDays.Text = AgL.VNull(.Rows(0)("CreditDays"))
                TxtCreditLimit.Text = AgL.VNull(.Rows(0)("CreditLimit"))
                LblTotalQty.Text = AgL.VNull(.Rows(0)("TotalQty"))
                LblTotalBale.Text = AgL.VNull(.Rows(0)("TotalBale"))
                LblTotalAmount.Text = AgL.VNull(.Rows(0)("TotalAmount"))
                LblTotalMeasure.Text = AgL.VNull(.Rows(0)("TotalMeasure"))
                LblTotalDeliveryMeasure.Text = AgL.VNull(.Rows(0)("TotalDeliveryMeasure"))

                Dim FrmObj As New FrmSaleInvoicePartyDetail
                FrmObj.TxtSaleToPartyMobile.Text = AgL.XNull(.Rows(0)("SaleToPartyMobile"))
                FrmObj.TxtSaleToPartyName.Text = AgL.XNull(.Rows(0)("SaleToPartyName"))
                FrmObj.TxtSaleToPartyAdd1.Text = AgL.XNull(.Rows(0)("SaleToPartyAdd1"))
                FrmObj.TxtSaleToPartyAdd2.Text = AgL.XNull(.Rows(0)("SaleToPartyAdd2"))
                FrmObj.TxtSaleToPartyCity.Tag = AgL.XNull(.Rows(0)("SaleToPartyCity"))
                FrmObj.TxtSaleToPartyCity.Text = AgL.XNull(.Rows(0)("SaleToPartyCityName"))

                BtnFillPartyDetail.Tag = FrmObj

                'AgCustomGrid1.MoveRec_TransFooter(SearchCode)

                AgCalcGrid1.FMoveRecFooterTable(DsTemp.Tables(0), EntryNCat, TxtV_Date.Text)

                AgCustomGrid1.FMoveRecFooterTable(DsTemp.Tables(0))


                '-------------------------------------------------------------
                'Line Records are showing in Grid
                '-------------------------------------------------------------
                mQry = "Select L.*, I.Description As ItemDesc, I.ManualCode, C.V_Type + '-' + C.ReferenceNo As ChallanRefNo, " & _
                        " O.V_Type + '-' + O.ReferenceNo As OrderRefNo, U.DecimalPlaces, " & _
                        " U.DecimalPlaces as QtyDecimalPlaces, MU.DecimalPlaces as MeasureDecimalPlaces " & _
                        " From (Select * From SaleInvoiceDetail With (NoLock) Where DocId = '" & SearchCode & "') As L " & _
                        " LEFT JOIN Item I With (NoLock) ON L.Item = I.Code " & _
                        " LEFT JOIN SaleChallan C With (NoLock) On L.SaleChallan = C.DocId " & _
                        " LEFT JOIN SaleOrder O With (NoLock) On L.SaleOrder = O.DocId " & _
                        " Left Join Unit U On L.Unit = U.Code " & _
                        " Left Join Unit MU On L.MeasureUnit = MU.Code " & _
                        " Order By L.Sr "
                DsTemp = AgL.FillData(mQry, AgL.GCn)
                With DsTemp.Tables(0)
                    Dgl1.RowCount = 1
                    Dgl1.Rows.Clear()
                    If .Rows.Count > 0 Then
                        For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                            Dgl1.Rows.Add()
                            Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count - 1

                            Dgl1.Item(Col1SaleChallan, I).Tag = AgL.XNull(.Rows(I)("SaleChallan"))
                            Dgl1.Item(Col1SaleChallan, I).Value = AgL.XNull(.Rows(I)("ChallanRefNo"))
                            Dgl1.Item(Col1SaleChallanSr, I).Value = AgL.VNull(.Rows(I)("SaleChallanSr"))

                            Dgl1.Item(Col1SaleOrder, I).Tag = AgL.XNull(.Rows(I)("SaleOrder"))
                            Dgl1.Item(Col1SaleOrder, I).Value = AgL.XNull(.Rows(I)("OrderRefNo"))
                            Dgl1.Item(Col1SaleOrderSr, I).Value = AgL.VNull(.Rows(I)("SaleOrderSr"))

                            Dgl1.Item(Col1Item_UID, I).Value = AgL.XNull(.Rows(I)("Item_UID"))

                            Dgl1.Item(Col1ItemCode, I).Tag = AgL.XNull(.Rows(I)("Item"))
                            Dgl1.Item(Col1ItemCode, I).Value = AgL.XNull(.Rows(I)("ManualCode"))
                            Dgl1.Item(Col1Item, I).Tag = AgL.XNull(.Rows(I)("Item"))
                            Dgl1.Item(Col1Item, I).Value = AgL.XNull(.Rows(I)("ItemDesc"))
                            Dgl1.Item(Col1SalesTaxGroup, I).Tag = AgL.XNull(.Rows(I)("SalesTaxGroupItem"))

                            Dgl1.Item(Col1QtyDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("QtyDecimalPlaces"))

                            Dgl1.Item(Col1Qty, I).Value = Format(AgL.VNull(.Rows(I)("Qty")), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                            Dgl1.Item(Col1MeasureDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("MeasureDecimalPlaces"))
                            Dgl1.Item(Col1MeasurePerPcs, I).Value = Format(AgL.VNull(.Rows(I)("MeasurePerPcs")), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1MeasureUnit, I).Value = AgL.XNull(.Rows(I)("MeasureUnit"))
                            Dgl1.Item(Col1TotalMeasure, I).Value = Format(AgL.VNull(.Rows(I)("TotalMeasure")), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1Rate, I).Value = AgL.VNull(.Rows(I)("Rate"))
                            Dgl1.Item(Col1Amount, I).Value = Format(AgL.VNull(.Rows(I)("Amount")), "0.00")
                            Dgl1.Item(Col1DeliveryMeasure, I).Value = AgL.XNull(.Rows(I)("DeliveryMeasure"))
                            Dgl1.Item(Col1Remark, I).Value = AgL.XNull(.Rows(I)("Remark"))
                            Dgl1.Item(Col1BillingType, I).Value = AgL.XNull(.Rows(I)("BillingType"))
                            Dgl1.Item(Col1RateType, I).Value = AgL.XNull(.Rows(I)("RateType"))
                            Dgl1.Item(Col1BaleNo, I).Value = AgL.XNull(.Rows(I)("BaleNo"))
                            Dgl1.Item(Col1DeliveryMeasureMultiplier, I).Value = AgL.VNull(.Rows(I)("DeliveryMeasureMultiplier"))
                            Dgl1.Item(Col1TotalDeliveryMeasure, I).Value = AgL.VNull(.Rows(I)("TotalDeliveryMeasure"))

                            Call AgCalcGrid1.FMoveRecLineTable(DsTemp.Tables(0), I)
                        Next I
                    End If
                End With
                If AgCustomGrid1.Rows.Count = 0 Then AgCustomGrid1.Visible = False

                RbtInvoiceDirect.Enabled = False : RbtInvoiceForChallan.Enabled = False : RbtInvoiceForOrder.Enabled = False
                BtnFillSaleChallan.Enabled = False

                'If Dgl1.Item(Col1SaleChallan, 0).Tag = mSearchCode Then
                '    RbtInvoiceDirect.Checked = True
                'Else
                '    RbtInvoiceForChallan.Checked = True
                'End If

                '-------------------------------------------------------------

            End If
        End With
    End Sub

    Private Sub FrmSaleOrder_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Topctrl1.ChangeAgGridState(Dgl1, False)
        AgCalcGrid1.FrmType = Me.FrmType
        AgCustomGrid1.FrmType = Me.FrmType
    End Sub



    Private Sub Txt_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtV_Type.Validating, TxtSaleToParty.Validating, TxtSalesTaxGroupParty.Validating, TxtReferenceNo.Validating
        Dim DrTemp As DataRow() = Nothing
        Dim DtTemp As DataTable = Nothing
        Dim FrmObj As New FrmSaleInvoicePartyDetail
        Try
            Select Case sender.NAME
                Case TxtV_Type.Name
                    TxtStructure.AgSelectedValue = AgStructure.ClsMain.FGetStructureFromNCat(LblV_Type.Tag, AgL.GcnRead)
                    AgCalcGrid1.AgStructure = TxtStructure.AgSelectedValue

                    TxtCustomFields.AgSelectedValue = AgCustomFields.ClsMain.FGetCustomFieldFromV_Type(TxtV_Type.AgSelectedValue, AgL.GcnRead)
                    AgCustomGrid1.AgCustom = TxtCustomFields.AgSelectedValue

                    IniGrid()
                    TxtReferenceNo.Text = AgTemplate.ClsMain.FGetManualRefNo("ReferenceNo", "SaleInvoice", TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, AgTemplate.ClsMain.ManualRefType.Max)

                Case TxtSaleToParty.Name
                    If TxtV_Date.Text <> "" And TxtSaleToParty.Text <> "" Then
                        DrTemp = sender.AgHelpDataSet.Tables(0).Select("Code = " & AgL.Chk_Text(sender.AgSelectedValue) & "")
                        TxtCurrency.AgSelectedValue = AgL.XNull(DrTemp(0)("Currency"))
                        TxtCreditDays.Text = AgL.VNull(DrTemp(0)("CreditDays"))
                        TxtCreditLimit.Text = AgL.VNull(DrTemp(0)("CreditLimit"))
                        TxtNature.Text = AgL.XNull(DrTemp(0)("Nature"))

                        FGetCurrBal(TxtSaleToParty.AgSelectedValue)
                        If AgL.StrCmp(TxtNature.Text, "Cash") Then
                            FOpenPartyDetail()
                        Else
                            mQry = " Select Mobile As SaleToPartyMobile, DispName As SaleToPartyName, " & _
                                    " IsNull(Add1,'') As SaleToPartyAdd1, IsNull(Add2,'') As SaleToPartyAdd2, " & _
                                    " Sg.CityCode As SaleToPartyCity, C.CityName As SaleToPartyCityName  " & _
                                    " From SubGroup Sg " & _
                                    " LEFT JOIN City C ON Sg.CityCode = C.CityCode " & _
                                    " Where Sg.SubCode = '" & TxtSaleToParty.AgSelectedValue & "'  "
                            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

                            With DtTemp
                                FrmObj.TxtSaleToPartyMobile.Text = AgL.XNull(.Rows(0)("SaleToPartyMobile"))
                                FrmObj.TxtSaleToPartyName.Text = AgL.XNull(.Rows(0)("SaleToPartyName"))
                                FrmObj.TxtSaleToPartyAdd1.Text = AgL.XNull(.Rows(0)("SaleToPartyAdd1"))
                                FrmObj.TxtSaleToPartyAdd2.Text = AgL.XNull(.Rows(0)("SaleToPartyAdd2"))
                                FrmObj.TxtSaleToPartyCity.Tag = AgL.XNull(.Rows(0)("SaleToPartyCity"))
                                FrmObj.TxtSaleToPartyCity.Text = AgL.XNull(.Rows(0)("SaleToPartyCityName"))
                            End With
                            BtnFillPartyDetail.Tag = FrmObj
                        End If
                        TxtBillToParty.Tag = TxtSaleToParty.Tag
                        TxtBillToParty.Text = TxtSaleToParty.Text
                    End If
                    BtnFillSaleChallan.Tag = Nothing

                Case TxtSalesTaxGroupParty.Name
                    AgCalcGrid1.AgPostingGroupSalesTaxParty = TxtSalesTaxGroupParty.AgSelectedValue
                    Calculation()

                Case TxtReferenceNo.Name
                    e.Cancel = Not AgTemplate.ClsMain.FCheckDuplicateRefNo("ReferenceNo", "SaleInvoice", _
                                    TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue, _
                                    TxtSite_Code.AgSelectedValue, Topctrl1.Mode, _
                                    TxtReferenceNo.Text, mSearchCode)


                Case TxtReferenceNo.Name
                    e.Cancel = Not FCheckDuplicateRefNo()

            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FGetCurrBal(ByVal Party As String)
        mQry = " Select IsNull(Sum(AmtDr),0) - IsNull(Sum(AmtCr),0) As CurrBal From Ledger Where SubCode = '" & Party & "'"
        TxtCurrBal.Text = AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar)
    End Sub

    Private Sub FrmSaleOrder_BaseEvent_Topctrl_tbAdd() Handles Me.BaseEvent_Topctrl_tbAdd
        TxtStructure.AgSelectedValue = AgStructure.ClsMain.FGetStructureFromNCat(LblV_Type.Tag, AgL.GcnRead)
        AgCalcGrid1.AgStructure = TxtStructure.AgSelectedValue
        AgCalcGrid1.AgNCat = EntryNCat

        TxtCustomFields.AgSelectedValue = AgCustomFields.ClsMain.FGetCustomFieldFromV_Type(TxtV_Type.AgSelectedValue, AgL.GCn)
        AgCustomGrid1.AgCustom = TxtCustomFields.AgSelectedValue

        IniGrid()
        TabControl1.SelectedTab = TP1
        TxtSalesTaxGroupParty.AgSelectedValue = AgL.XNull(AgL.PubDtEnviro.Rows(0)("DefaultSalesTaxGroupParty"))
        AgCalcGrid1.AgPostingGroupSalesTaxParty = TxtSalesTaxGroupParty.AgSelectedValue
        RbtInvoiceDirect.Checked = True
        TxtReferenceNo.Text = AgTemplate.ClsMain.FGetManualRefNo("ReferenceNo", "SaleInvoice", TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, AgTemplate.ClsMain.ManualRefType.Max)
        TxtSaleToParty.Focus()
    End Sub

    'Private Sub FValidating_Item(ByVal Code As String, ByVal mRow As Integer)
    '    Dim DrTemp As DataRow() = Nothing
    '    Dim DtTemp As DataTable = Nothing
    '    Try
    '        If Dgl1.Item(Col1Item, mRow).Value.ToString.Trim = "" Or Dgl1.AgSelectedValue(Col1Item, mRow).ToString.Trim = "" Then
    '            Dgl1.Item(Col1Unit, mRow).Value = ""
    '            Dgl1.Item(Col1SalesTaxGroup, mRow).Value = ""
    '            Dgl1.Item(Col1MeasureUnit, mRow).Value = ""
    '            Dgl1.Item(Col1MeasurePerPcs, mRow).Value = ""
    '            Dgl1.Item(Col1Rate, mRow).Value = ""
    '        Else
    '            If Dgl1.AgHelpDataSet(Col1Item) IsNot Nothing Then
    '                DrTemp = Dgl1.AgHelpDataSet(Col1Item).Tables(0).Select("Code = '" & Code & "'")
    '                If DrTemp.Length > 0 Then
    '                    Call FSetColumnDecimalPlace(Dgl1.AgSelectedValue(Col1Item, mRow), mRow)
    '                    Dgl1.Item(Col1Unit, mRow).Value = AgL.XNull(DrTemp(0)("Unit"))
    '                    Dgl1.Item(Col1MeasureUnit, mRow).Value = AgL.XNull(DrTemp(0)("MeasureUnit"))
    '                    Dgl1.Item(Col1MeasurePerPcs, mRow).Value = AgL.VNull(DrTemp(0)("MeasurePerPcs"))
    '                    Dgl1.Item(Col1Rate, mRow).Value = AgL.VNull(DrTemp(0)("Rate"))
    '                    Dgl1.AgSelectedValue(Col1SalesTaxGroup, mRow) = AgL.XNull(DrTemp(0)("SalesTaxPostingGroup"))
    '                    If AgL.StrCmp(Dgl1.AgSelectedValue(Col1SalesTaxGroup, mRow), "") Then
    '                        Dgl1.AgSelectedValue(Col1SalesTaxGroup, mRow) = AgL.XNull(AgL.PubDtEnviro.Rows(0)("DefaultSalesTaxGroupItem"))
    '                    End If
    '                End If
    '                If Dgl1.Item(Col1MeasureUnit, mRow).Value = "" Then Dgl1.Item(Col1TotalMeasure, mRow).ReadOnly = True
    '            End If
    '            Try
    '                Dgl1.Item(Col1DeliveryMeasure, mRow).Value = Dgl1.Item(Col1DeliveryMeasure, mRow - 1).Value
    '            Catch ex As Exception
    '            End Try
    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.Message & " On Validating_Item Function ")
    '    End Try
    'End Sub

    Private Sub Validating_Item_Uid(ByVal Item_Uid As String, ByVal mRow As Integer)
        Dim DrTemp As DataRow() = Nothing
        Dim DtTemp As DataTable = Nothing

        Try
            mQry = " SELECT I.Code, I.Description, I.Unit, I.ManualCode, I.MeasureUnit, I.Measure As MeasurePerPcs, " & _
                   " U.DecimalPlaces as QtyDecimalPlaces, MU.DecimalPlaces as MeasureDecimalPlaces, UI.Code as ItemUIDCode " & _
                   " FROM (Select Item, Code From Item_UID Where Item_Uid = '" & Dgl1.Item(Col1Item_UID, mRow).Value & "') UI " & _
                   " Left Join Item I With (NoLock) On UI.Item  = I.Code " & _
                   " Left Join Unit U With (NoLock) On I.Unit = U.Code " & _
                   " Left Join Unit MU With (NoLock) On I.MeasureUnit = MU.Code "
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

            If DtTemp.Rows.Count > 0 Then
                Dgl1.Item(Col1Item_UID, mRow).Tag = AgL.XNull(DtTemp.Rows(0)("ItemUIDCode"))
                Dgl1.Item(Col1ItemCode, mRow).Tag = AgL.XNull(DtTemp.Rows(0)("Code"))
                Dgl1.Item(Col1ItemCode, mRow).Value = AgL.XNull(DtTemp.Rows(0)("ManualCode"))
                Dgl1.Item(Col1Item, mRow).Tag = AgL.XNull(DtTemp.Rows(0)("Code"))
                Dgl1.Item(Col1Item, mRow).Value = AgL.XNull(DtTemp.Rows(0)("Description"))
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

    Private Sub Validating_ItemCode(ByVal mColumn As Integer, ByVal mRow As Integer)
        Dim DrTemp As DataRow() = Nothing
        Dim DtTemp As DataTable = Nothing
        Try
            If Dgl1.Item(mColumn, mRow).Value.ToString.Trim = "" Or Dgl1.AgSelectedValue(mColumn, mRow).ToString.Trim = "" Then
                Dgl1.Item(Col1Unit, mRow).Value = ""
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

                    Dgl1.Item(Col1DeliveryMeasure, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("MeasureUnit").Value)
                    Dgl1.Item(Col1DeliveryMeasureMultiplier, mRow).Value = 1

                    Dgl1.Item(Col1BillingType, mRow).Value = "Qty"


                    Dgl1.Item(Col1SalesTaxGroup, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("SalesTaxPostingGroup").Value)
                    If AgL.StrCmp(Dgl1.AgSelectedValue(Col1SalesTaxGroup, mRow), "") Then
                        Dgl1.Item(Col1SalesTaxGroup, mRow).Tag = AgL.XNull(AgL.PubDtEnviro.Rows(0)("DefaultSalesTaxGroupItem"))
                    End If
                    If Dgl1.Item(Col1MeasureUnit, mRow).Value = "" Then Dgl1.Item(Col1TotalMeasure, mRow).ReadOnly = True

                    Dgl1.Item(Col1Rate, mRow).Value = ClsMain.FGetItemRate(Dgl1.Item(Col1Item, mRow).Tag, Dgl1.Item(Col1RateType, mRow).Tag, TxtV_Date.Text)
                    If Val(Dgl1.Item(Col1Rate, mRow).Value) = 0 Then
                        Dgl1.Item(Col1Rate, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("Rate").Value)
                    End If
                End If
                Try
                    Dgl1.Item(Col1DeliveryMeasure, mRow).Value = Dgl1.Item(Col1DeliveryMeasure, mRow - 1).Value
                    Dgl1.Item(Col1BillingType, mRow).Value = Dgl1.Item(Col1BillingType, mRow - 1).Value
                    Dgl1.Item(Col1RateType, mRow).Value = Dgl1.Item(Col1RateType, mRow - 1).Value
                Catch ex As Exception
                End Try
                End If
        Catch ex As Exception
            MsgBox(ex.Message & " On Validating_Item Function ")
        End Try
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
                    Call FGetDeliveryMeasureMultiplier(mRowIndex)

                Case Col1Item
                    Validating_ItemCode(mColumnIndex, mRowIndex)
                    Call FGetDeliveryMeasureMultiplier(mRowIndex)

                Case Col1ItemCode
                    Validating_ItemCode(mColumnIndex, mRowIndex)
                    Call FGetDeliveryMeasureMultiplier(mRowIndex)

                Case Col1DeliveryMeasure
                    Call FGetDeliveryMeasureMultiplier(mRowIndex)

                Case Col1RateType
                    Dgl1.Item(Col1Rate, mRowIndex).Value = ClsMain.FGetItemRate(Dgl1.Item(Col1Item, mRowIndex).Tag, Dgl1.Item(Col1RateType, mRowIndex).Tag, TxtV_Date.Text)
            End Select
            Call Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub DGL1_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles Dgl1.RowsAdded, Dgl1.RowsAdded
        sender(ColSNo, sender.Rows.Count - 1).Value = Trim(sender.Rows.Count)
    End Sub

    Private Sub FrmSaleOrder_BaseFunction_Calculation() Handles Me.BaseFunction_Calculation
        Dim I As Integer

        If Topctrl1.Mode = "Browse" Then Exit Sub

        LblTotalQty.Text = 0
        LblTotalMeasure.Text = 0
        LblTotalDeliveryMeasure.Text = 0
        LblTotalBale.Text = 0
        LblTotalAmount.Text = 0

        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1Item, I).Value <> "" Then
                If Val(Dgl1.Item(Col1MeasurePerPcs, I).Value) <> 0 Then
                    Dgl1.Item(Col1TotalMeasure, I).Value = Format(Val(Dgl1.Item(Col1Qty, I).Value) * Val(Dgl1.Item(Col1MeasurePerPcs, I).Value), "0.".PadRight(CType(Dgl1.Columns(Col1TotalMeasure), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                End If
                Dgl1.Item(Col1TotalDeliveryMeasure, I).Value = Format(Val(Dgl1.Item(Col1TotalMeasure, I).Value) * Val(Dgl1.Item(Col1DeliveryMeasureMultiplier, I).Value), "0.0000")

                If AgL.StrCmp(Dgl1.Item(Col1BillingType, I).Value, "Qty") Or Dgl1.Item(Col1BillingType, I).Value = "" Then
                    Dgl1.Item(Col1Amount, I).Value = Format(Val(Dgl1.Item(Col1Qty, I).Value) * Val(Dgl1.Item(Col1Rate, I).Value), "0.".PadRight(CType(Dgl1.Columns(Col1Amount), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                ElseIf AgL.StrCmp(Dgl1.Item(Col1BillingType, I).Value, "Measure") Then
                    Dgl1.Item(Col1Amount, I).Value = Format(Val(Dgl1.Item(Col1TotalMeasure, I).Value) * Val(Dgl1.Item(Col1Rate, I).Value), "0.".PadRight(CType(Dgl1.Columns(Col1Amount), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                Else
                    Dgl1.Item(Col1Amount, I).Value = Format(Val(Dgl1.Item(Col1Qty, I).Value) * Val(Dgl1.Item(Col1Rate, I).Value), "0.".PadRight(CType(Dgl1.Columns(Col1Amount), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                End If

                'Footer Calculation
                LblTotalQty.Text = Val(LblTotalQty.Text) + Val(Dgl1.Item(Col1Qty, I).Value)
                LblTotalMeasure.Text = Val(LblTotalMeasure.Text) + Val(Dgl1.Item(Col1TotalMeasure, I).Value)
                LblTotalDeliveryMeasure.Text = Val(LblTotalDeliveryMeasure.Text) + Val(Dgl1.Item(Col1TotalDeliveryMeasure, I).Value)
                LblTotalAmount.Text = Val(LblTotalAmount.Text) + Val(Dgl1.Item(Col1Amount, I).Value)

                LblTotalBale.Text += 1
            End If
        Next
        AgCalcGrid1.AgPostingGroupSalesTaxParty = TxtSalesTaxGroupParty.AgSelectedValue
        AgCalcGrid1.Calculation()
        LblTotalQty.Text = Val(LblTotalQty.Text)
        LblTotalMeasure.Text = Val(LblTotalMeasure.Text)
        LblTotalAmount.Text = Val(LblTotalAmount.Text)
    End Sub

    Private Sub FrmSaleOrder_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        Dim I As Integer = 0
        Dim bQcPassedQty As Double = 0, bInvoicedQty As Double = 0
        If AgL.RequiredField(TxtSaleToParty, LblBuyer.Text) Then passed = False : Exit Sub

        If AgCL.AgIsBlankGrid(Dgl1, Dgl1.Columns(Col1Item).Index) Then passed = False : Exit Sub

        If Val(TxtCreditLimit.Text) > 0 Then
            If Val(AgCalcGrid1.AgChargesValue(AgTemplate.ClsMain.Charges.NETAMOUNT, AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Amount)) + Val(TxtCurrBal.Text) > Val(TxtCreditLimit.Text) Then
                MsgBox("Total Balance Of " & TxtSaleToParty.Name & " Is Exceeding Its Credit Limit " & TxtCreditLimit.Text & ".")
                passed = False : Exit Sub
            End If
        End If

        With Dgl1
            For I = 0 To .Rows.Count - 1
                If .Item(Col1Item, I).Value <> "" Then
                    If Val(.Item(Col1Qty, I).Value) = 0 Then
                        MsgBox("Qty Is 0 At Row No " & Dgl1.Item(ColSNo, I).Value & "")
                        .CurrentCell = .Item(Col1Qty, I) : Dgl1.Focus()
                        passed = False : Exit Sub
                    End If

                    If Val(.Item(Col1Rate, I).Value) = 0 Then
                        MsgBox("Rate Is 0 At Row No " & Dgl1.Item(ColSNo, I).Value & "")
                        .CurrentCell = .Item(Col1Rate, I) : Dgl1.Focus()
                        passed = False : Exit Sub
                    End If

                    If BlnIsCheckQcQty Then
                        If .Item(Col1SaleOrder, I).Value <> "" Then
                            mQry = " SELECT Sum(L.PassedQty) FROM SaleQCDetail L WHERE L.SaleOrder = '" & Dgl1.Item(Col1SaleOrder, I).Tag & "' AND L.Item = '" & Dgl1.Item(Col1Item, I).Tag & "'  "
                            bQcPassedQty = AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar)

                            mQry = " SELECT Sum(L.Qty) FROM SaleInvoiceDetail L WHERE L.SaleOrder = '" & Dgl1.Item(Col1SaleOrder, I).Tag & "' AND L.Item = '" & Dgl1.Item(Col1Item, I).Tag & "' And L.DocId <> '" & mSearchCode & "'  "
                            bInvoicedQty = AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar)

                            If bQcPassedQty < bInvoicedQty + Val(Dgl1.Item(Col1Qty, I).Value) Then
                                MsgBox("Total Invoice Qty Of " & .Item(Col1Item, I).Value & " For Sale Order " & .Item(Col1SaleOrder, I).Value & " Is " & bInvoicedQty + Val(Dgl1.Item(Col1Qty, I).Value) & "." & vbCrLf & "QC Passed Qty Is " & Val(bQcPassedQty) & ".Can't Continue.")
                                passed = False : Exit Sub
                            End If
                        End If
                    End If
                End If
            Next
        End With

        passed = AgTemplate.ClsMain.FCheckDuplicateRefNo("ReferenceNo", "SaleChallan", _
                                    TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue, _
                                    TxtSite_Code.AgSelectedValue, Topctrl1.Mode, _
                                    TxtReferenceNo.Text, mSearchCode)

        Call FGetBaleStr(mSearchCode)
    End Sub

    Private Sub TxtBuyer_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtSaleToParty.KeyDown, TxtCurrency.KeyDown, TxtSalesTaxGroupParty.KeyDown, TxtBillToParty.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then Exit Sub
            Select Case sender.name
                Case TxtCurrency.Name
                    If CType(sender, AgControls.AgTextBox).AgHelpDataSet Is Nothing Then
                        If e.KeyCode <> Keys.Enter Then
                            mQry = "SELECT Code, Code AS Currency, IsNull(IsDeleted,0) AS IsDeleted " & _
                                    " FROM Currency " & _
                                    " ORDER BY Code "
                            CType(sender, AgControls.AgTextBox).AgHelpDataSet(1, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If

                Case TxtSaleToParty.Name
                    If CType(sender, AgControls.AgTextBox).AgHelpDataSet Is Nothing Then
                        If e.KeyCode <> Keys.Enter Then
                            mQry = "SELECT Sg.SubCode As Code, Sg.Name + ',' + IsNull(C.CityName,'') As Party, Sg.SalesTaxPostingGroup, " & _
                                    " Sg.SalesTaxPostingGroup, Sg.Currency, " & _
                                    " Sg.Div_Code, Sg.CreditDays, Sg.CreditLimit, Sg.Nature " & _
                                    " FROM SubGroup Sg " & _
                                    " LEFT JOIN City C ON Sg.CityCode = C.CityCode  " & _
                                    " Where Sg.Nature in ('Customer','Supplier','Cash') " & _
                                    " And IsNull(Sg.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' "
                            CType(sender, AgControls.AgTextBox).AgHelpDataSet(7, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If

                Case TxtBillToParty.Name
                    If CType(sender, AgControls.AgTextBox).AgHelpDataSet Is Nothing Then
                        If e.KeyCode <> Keys.Enter Then
                            mQry = "SELECT Sg.SubCode As Code, Sg.Name + ',' + IsNull(C.CityName,'') As Account_Name " & _
                                    " FROM SubGroup Sg " & _
                                    " LEFT JOIN City C ON Sg.CityCode = C.CityCode  " & _
                                    " Where IsNull(Sg.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' "
                            CType(sender, AgControls.AgTextBox).AgHelpDataSet(0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If

                Case TxtSalesTaxGroupParty.Name
                    If CType(sender, AgControls.AgTextBox).AgHelpDataSet Is Nothing Then
                        If e.KeyCode <> Keys.Enter Then
                            mQry = "SELECT Description AS Code, Description FROM PostingGroupSalesTaxParty Where IsNull(Active,0)=1 "
                            CType(sender, AgControls.AgTextBox).AgHelpDataSet(0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FrmSaleOrder_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
        Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
    End Sub

    Private Sub Dgl1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellEnter
        Try
            If AgL.StrCmp(Topctrl1.Mode, "Browse") Then Exit Sub
            If Dgl1.CurrentCell Is Nothing Then Exit Sub
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Item
                    If RbtInvoiceForChallan.Checked Or RbtInvoiceForOrder.Checked Then
                        Dgl1.Columns(Col1Item).ReadOnly = True
                    Else
                        Dgl1.Columns(Col1Item).ReadOnly = False
                    End If

                Case Col1Qty
                    CType(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex), AgControls.AgTextColumn).AgNumberRightPlaces = Val(Dgl1.Item(Col1QtyDecimalPlaces, Dgl1.CurrentCell.RowIndex).Value)

                Case Col1MeasurePerPcs, Col1TotalMeasure
                    CType(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex), AgControls.AgTextColumn).AgNumberRightPlaces = Val(Dgl1.Item(Col1MeasureDecimalPlaces, Dgl1.CurrentCell.RowIndex).Value)
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    'Private Sub FFillPendingChallans(ByVal bBuyer As String, ByVal bV_Date As String, ByVal FrmObj As FrmSaleInvoiceChallanList)
    '    Dim DtTemp As DataTable = Nothing
    '    Dim bConStr$ = ""
    '    Dim I As Integer = 0
    '    Try
    '        mQry = "Select Distinct Vt.Description AS ChallanType, H.DocID AS ChallanNo, H.V_Date AS SaleChallanDate " & _
    '                " From SaleInvoiceDetail L " & _
    '                " LEFT JOIN SaleChallan H On L.SaleChallan = H.DocId " & _
    '                " LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type " & _
    '                " Where L.DocId = '" & mSearchCode & "' "
    '        mQry += " UNION ALL "
    '        mQry += " SELECT Vt.Description AS ChallanType, H.DocID AS ChallanNo, H.V_Date AS SaleChallanDate " & _
    '                    " FROM SaleChallan H  " & _
    '                    " LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type " & _
    '                    " Left Join  " & _
    '                    " 	(SELECT L.SaleChallan, Sum(L.Qty) AS InvoiceQty FROM SaleInvoiceDetail L GROUP BY L.SaleChallan) AS V1 " & _
    '                    " ON H.DocId = V1.SaleChallan " & _
    '                    " WHERE H.SaleToParty = '" & bBuyer & "'  " & _
    '                    " And H.V_Date <= '" & bV_Date & "' " & _
    '                    " And H.TotalQty - IsNull(V1.InvoiceQty,0) > 0 "

    '        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

    '        With DtTemp
    '            FrmObj.DGL1.RowCount = 1
    '            FrmObj.DGL1.Rows.Clear()
    '            If .Rows.Count > 0 Then
    '                For I = 0 To .Rows.Count - 1
    '                    FrmObj.DGL1.Rows.Add()
    '                    FrmObj.DGL1.Item(FrmSaleInvoiceChallanList.COl1Select, I).Value = AgLibrary.ClsConstant.StrCheckedValue
    '                    FrmObj.DGL1.Item(FrmSaleInvoiceChallanList.COl1SaleChallanType, I).Value = AgL.XNull(.Rows(I)("ChallanType"))
    '                    FrmObj.DGL1.AgSelectedValue(FrmSaleInvoiceChallanList.COl1SaleChallan, I) = AgL.XNull(.Rows(I)("ChallanNo"))
    '                    FrmObj.DGL1.Item(FrmSaleInvoiceChallanList.COl1SaleChallanDate, I).Value = AgL.XNull(.Rows(I)("SaleChallanDate"))
    '                Next I
    '            End If
    '        End With
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub

    'Private Sub FFillPendingOrders(ByVal bBuyer As String, ByVal bV_Date As String, ByVal FrmObj As FrmSaleInvoiceOrderList)
    '    Dim DtTemp As DataTable = Nothing
    '    Dim bConStr$ = ""
    '    Dim I As Integer = 0
    '    Try
    '        mQry = "Select Distinct Vt.Description AS OrderType, H.DocID AS OrderNo, H.V_Date AS SaleOrderDate " & _
    '                " From SaleInvoiceDetail L " & _
    '                " LEFT JOIN SaleOrder H On L.SaleOrder = H.DocId " & _
    '                " LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type " & _
    '                " Where L.DocId = '" & mSearchCode & "' "
    '        mQry += " UNION ALL "
    '        mQry += " SELECT Vt.Description AS OrderType, H.DocID AS OrderNo, H.V_Date AS SaleOrderDate " & _
    '                    " FROM SaleOrder H  " & _
    '                    " LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type " & _
    '                    " Left Join " & _
    '                    " 	(SELECT L.SaleOrder, Sum(L.Qty) AS DispatchQty  " & _
    '                    " 	 FROM SaleChallanDetail L GROUP BY L.SaleOrder) AS V1 " & _
    '                    " ON H.DocId = V1.SaleOrder " & _
    '                    " WHERE H.SaleToParty = '" & bBuyer & "' " & _
    '                    " AND H.V_Date <= '" & bV_Date & "' " & _
    '                    " And H.TotalQty - IsNull(V1.DispatchQty,0) > 0  "

    '        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

    '        With DtTemp
    '            FrmObj.DGL1.RowCount = 1
    '            FrmObj.DGL1.Rows.Clear()
    '            If .Rows.Count > 0 Then
    '                For I = 0 To .Rows.Count - 1
    '                    FrmObj.DGL1.Rows.Add()
    '                    FrmObj.DGL1.Item(FrmSaleInvoiceOrderList.COl1Select, I).Value = AgLibrary.ClsConstant.StrCheckedValue
    '                    FrmObj.DGL1.Item(FrmSaleInvoiceOrderList.COl1SaleOrderType, I).Value = AgL.XNull(.Rows(I)("OrderType"))
    '                    FrmObj.DGL1.AgSelectedValue(FrmSaleInvoiceOrderList.COl1SaleOrder, I) = AgL.XNull(.Rows(I)("OrderNo"))
    '                    FrmObj.DGL1.Item(FrmSaleInvoiceOrderList.COl1SaleOrderDate, I).Value = AgL.XNull(.Rows(I)("SaleOrderDate"))
    '                Next I
    '            End If
    '        End With
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub

    Private Sub FFillItemsForChallan(ByVal bChallanNoStr As String)
        Dim I As Integer = 0
        Dim DtTemp As DataTable = Nothing
        Try
            If bChallanNoStr = "" Then Exit Sub
            mQry = "SELECT Cd.DocId As SaleChallan, Cd.Sr, Cd.Item, Cd.SalesTaxGroupItem, Cd.Qty, Cd.Unit, " & _
                        " Cd.MeasurePerPcs, Cd.MeasureUnit, Cd.TotalMeasure, C.BillingType, " & _
                        " CASE WHEN  IsNull(Od.Rate,0) = 0 THEN IsNull(Cd.Rate,0) ELSE IsNull(Od.Rate,0) END AS Rate, " & _
                        " I.Description As ItemDesc, C.V_Type + '-' + C.ReferenceNo As SaleChallanRefNo, Cd.Sr As SaleChallanSr, Cd.BaleNo " & _
                        " FROM SaleChallanDetail Cd " & _
                        " LEFT JOIN SaleChallan C On Cd.DocId = C.DocId " & _
                        " LEFT JOIN SaleOrderDetail Od On C.SaleOrder = Od.DocId " & _
                        "                               And Cd.Item = Od.Item " & _
                        " LEFT JOIN Item I ON Cd.Item = I.Code " & _
                        " WHERE Cd.DocId In (" & bChallanNoStr & ") "
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

            With DtTemp
                Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
                If .Rows.Count > 0 Then
                    For I = 0 To .Rows.Count - 1
                        Dgl1.Rows.Add()
                        Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count - 1
                        Dgl1.Item(Col1SaleChallan, I).Tag = AgL.XNull(.Rows(I)("SaleChallan"))
                        Dgl1.Item(Col1SaleChallan, I).Value = AgL.XNull(.Rows(I)("SaleChallanRefNo"))
                        Dgl1.Item(Col1SaleChallanSr, I).Value = AgL.XNull(.Rows(I)("SaleChallanSr"))
                        Dgl1.Item(Col1Item, I).Tag = AgL.XNull(.Rows(I)("Item"))
                        Dgl1.Item(Col1Item, I).Value = AgL.XNull(.Rows(I)("ItemDesc"))
                        Dgl1.Item(Col1BaleNo, I).Value = AgL.XNull(.Rows(I)("BaleNo"))
                        Dgl1.Item(Col1SalesTaxGroup, I).Tag = AgL.XNull(.Rows(I)("SalesTaxGroupItem"))
                        Dgl1.Item(Col1Qty, I).Value = AgL.VNull(.Rows(I)("Qty"))
                        Dgl1.Item(Col1Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                        Dgl1.Item(Col1MeasurePerPcs, I).Value = Format(AgL.VNull(.Rows(I)("MeasurePerPcs")), "0.000")
                        Dgl1.Item(Col1MeasureUnit, I).Value = AgL.XNull(.Rows(I)("MeasureUnit"))
                        Dgl1.Item(Col1TotalMeasure, I).Value = Format(AgL.VNull(.Rows(I)("TotalMeasure")), "0.000")
                        Dgl1.Item(Col1Rate, I).Value = Format(AgL.VNull(.Rows(I)("Rate")), "0.00")
                        Dgl1.Item(Col1BillingType, I).Value = AgL.XNull(.Rows(I)("BillingType"))

                        AgCalcGrid1.FCopyStructureLine(AgL.XNull(.Rows(I)("SaleChallan")), Dgl1, I, AgL.VNull(.Rows(I)("Sr")))
                    Next I
                End If
            End With
            AgCalcGrid1.Calculation(True)
            Calculation()
            If Dgl1.Item(Col1SaleChallan, 0).Value <> "" Then Dgl1.Columns(Col1Item).ReadOnly = True
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FFillItemsForOrder(ByVal bOrderNoStr As String)
        Dim I As Integer = 0
        Dim DtTemp As DataTable = Nothing
        Try
            If bOrderNoStr = "" Then Exit Sub
            mQry = " SELECT L.DocId AS SaleOrder, L.Sr, L.Item, L.SalesTaxGroupItem, L.Qty, L.Unit, " & _
                        " L.MeasurePerPcs, L.MeasureUnit, L.TotalMeasure, L.BillingType, " & _
                        " L.Rate, I.Description AS ItemDesc, H.V_Type + '-' + H.ReferenceNo AS SaleOrderRefNo, L.Sr AS SaleOrderSr, " & _
                        " L.DeliveryMeasure, L.DeliveryMeasureMultiplier, L.TotalDeliveryMeasure " & _
                        " FROM SaleOrderDetail L   " & _
                        " LEFT JOIN SaleOrder H ON L.DocId = H.DocID " & _
                        " LEFT JOIN Item I ON L.Item = I.Code " & _
                        " WHERE L.DocId In (" & bOrderNoStr & ") "
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

            With DtTemp
                Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
                If .Rows.Count > 0 Then
                    For I = 0 To .Rows.Count - 1
                        Dgl1.Rows.Add()
                        Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count - 1
                        Dgl1.Item(Col1SaleOrder, I).Tag = AgL.XNull(.Rows(I)("SaleOrder"))
                        Dgl1.Item(Col1SaleOrder, I).Value = AgL.XNull(.Rows(I)("SaleOrderRefNo"))
                        Dgl1.Item(Col1SaleOrderSr, I).Value = AgL.XNull(.Rows(I)("SaleOrderSr"))
                        Dgl1.Item(Col1Item, I).Tag = AgL.XNull(.Rows(I)("Item"))
                        Dgl1.Item(Col1Item, I).Value = AgL.XNull(.Rows(I)("ItemDesc"))
                        Dgl1.Item(Col1SalesTaxGroup, I).Tag = AgL.XNull(.Rows(I)("SalesTaxGroupItem"))
                        Dgl1.Item(Col1Qty, I).Value = AgL.VNull(.Rows(I)("Qty"))
                        Dgl1.Item(Col1Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                        Dgl1.Item(Col1MeasurePerPcs, I).Value = Format(AgL.VNull(.Rows(I)("MeasurePerPcs")), "0.000")
                        Dgl1.Item(Col1MeasureUnit, I).Value = AgL.XNull(.Rows(I)("MeasureUnit"))
                        Dgl1.Item(Col1TotalMeasure, I).Value = Format(AgL.VNull(.Rows(I)("TotalMeasure")), "0.000")
                        Dgl1.Item(Col1Rate, I).Value = Format(AgL.VNull(.Rows(I)("Rate")), "0.00")
                        Dgl1.Item(Col1BillingType, I).Value = AgL.XNull(.Rows(I)("BillingType"))
                        Dgl1.Item(Col1DeliveryMeasure, I).Value = AgL.XNull(.Rows(I)("DeliveryMeasure"))
                        Dgl1.Item(Col1DeliveryMeasureMultiplier, I).Value = AgL.VNull(.Rows(I)("DeliveryMeasureMultiplier"))
                        Dgl1.Item(Col1TotalDeliveryMeasure, I).Value = AgL.VNull(.Rows(I)("TotalDeliveryMeasure"))

                        'AgCalcGrid1.FCopyStructureLine(AgL.XNull(.Rows(I)("SaleOrder")), Dgl1, I, AgL.VNull(.Rows(I)("Sr")))
                    Next I
                End If
            End With
            AgCalcGrid1.Calculation(True)
            Calculation()
            If Dgl1.Item(Col1SaleOrder, 0).Value <> "" Then Dgl1.Columns(Col1Item).ReadOnly = True
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub TempSaleInvoice_BaseFunction_DispText() Handles Me.BaseFunction_DispText
        If Dgl1.Item(Col1SaleChallan, 0).Value <> "" Then Dgl1.Columns(Col1Item).ReadOnly = True

        If Not AgL.StrCmp(Topctrl1.Mode, "Add") Then
            RbtInvoiceDirect.Enabled = False : RbtInvoiceForChallan.Enabled = False : RbtInvoiceForOrder.Enabled = False
        Else
            RbtInvoiceDirect.Enabled = True : RbtInvoiceForChallan.Enabled = True : RbtInvoiceForOrder.Enabled = True
        End If

        If AgL.StrCmp(Topctrl1.Mode, "Browse") Then
            BtnFillSaleChallan.Enabled = False
        ElseIf RbtInvoiceForChallan.Checked = True Or RbtInvoiceForOrder.Checked = True Then
            BtnFillSaleChallan.Enabled = True
        Else
            BtnFillSaleChallan.Enabled = False
        End If

        If BlnIsDirectInvoice Then
            GrpDirectInvoice.Visible = False
            BtnFillSaleChallan.Visible = False
        End If

        If BlnIsTotalDeliveryMeasureVisible = False Then LblTotalDeliveryMeasure.Visible = False : LblTotalDeliveryMeasureText.Visible = False
        If BlnIsMeasureVisible = False Then LblTotalMeasure.Visible = False : LblTotalMeasureText.Visible = False
        If BlnIsBaleNoVisible = False Then LblTotalBale.Visible = False : LblTotalBaleText.Visible = False

        BtnFillPartyDetail.Tag = Nothing
    End Sub

    Private Sub DGL1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.KeyDown
        If e.Control And e.KeyCode = Keys.D Then
            sender.CurrentRow.Selected = True
        End If
        If e.Control Or e.Shift Or e.Alt Then Exit Sub
        Call FOpenMaster(e)
    End Sub

    Private Function FGetRelationalData() As Boolean
        Try
            Dim bRData As String
            '// Check for relational data in Sale Return
            mQry = " DECLARE @Temp NVARCHAR(Max); "
            mQry += " SET @Temp=''; "
            mQry += " SELECT  @Temp=@Temp +  X.VNo + ', ' FROM (SELECT DISTINCT H.V_Type + '-' + Convert(VARCHAR,H.V_No) AS VNo From SaleInvoiceDetail  L LEFT JOIN SaleInvoice H ON L.DocId = H.DocID WHERE L.ReferenceDocID  = '" & TxtDocId.Text & "' And IsNull(H.IsDeleted,0) = 0) AS X  "
            mQry += " SELECT @Temp as RelationalData "
            bRData = AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar
            If bRData.Trim <> "" Then
                MsgBox(" Sale Return " & bRData & " created against Invoice No. " & TxtV_Type.Tag & "-" & TxtV_No.Text & ". Can't Modify Entry")
                FGetRelationalData = True
                Exit Function
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " in FGetRelationalData in TempRequisition")
            FGetRelationalData = True
        End Try
    End Function

    Private Sub ME_BaseEvent_Topctrl_tbEdit(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbEdit
        Passed = Not FGetRelationalData()
    End Sub

    Private Sub ME_BaseEvent_Topctrl_tbDel(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbDel
        Passed = Not FGetRelationalData()

        mQry = " Delete From SaleChallanDetail Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        mQry = " Delete From SaleChallan Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        mQry = " Delete From Stock Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        mQry = " Delete From Ledger Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
    End Sub

    Private Function FCheckDuplicateRefNo() As Boolean
        FCheckDuplicateRefNo = True

        If Topctrl1.Mode = "Add" Then
            mQry = " SELECT COUNT(*) FROM SaleInvoice WHERE ReferenceNo = '" & TxtReferenceNo.Text & "'   " & _
                   " AND V_Type ='" & TxtV_Type.AgSelectedValue & "'  And Div_Code = '" & TxtDivision.AgSelectedValue & "' And Site_Code = '" & TxtSite_Code.AgSelectedValue & "' And IsNull(IsDeleted,0) = 0  "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then FCheckDuplicateRefNo = False : MsgBox("Reference No. Already Exists") : TxtReferenceNo.Focus()
        Else
            mQry = " SELECT COUNT(*) FROM SaleInvoice WHERE ReferenceNo = '" & TxtReferenceNo.Text & "'  " & _
                   " AND V_Type ='" & TxtV_Type.AgSelectedValue & "'  And Div_Code = '" & TxtDivision.AgSelectedValue & "' And Site_Code = '" & TxtSite_Code.AgSelectedValue & "' And IsNull(IsDeleted,0) = 0 AND DocID <>'" & mSearchCode & "'  "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then FCheckDuplicateRefNo = False : MsgBox("Reference No. Already Exists") : TxtReferenceNo.Focus()
        End If
    End Function

    Private Sub FrmCarpetMaterialPlan_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AgL.WinSetting(Me, 654, 990, 0, 0)
    End Sub

    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub

    'Private Sub BtnFillSaleChallan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnFillSaleChallan.Click
    '    Try
    '        If Topctrl1.Mode = "Browse" Then Exit Sub

    '        If RbtInvoiceForChallan.Checked Then
    '            Call FOpenChallanList()
    '        ElseIf RbtInvoiceForOrder.Checked Then
    '            Call FOpenOrderList()
    '        End If

    '        Dgl1.Focus()
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub

    'New Code For Multiple Selection

    Private Sub BtnFillSaleChallan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnFillSaleChallan.Click
        Try
            If Topctrl1.Mode = "Browse" Then Exit Sub
            Dim StrTicked As String

            If RbtInvoiceForChallan.Checked Then
                StrTicked = FHPGD_PendingSaleChallan()
                If StrTicked <> "" Then
                    FFillItemsForChallan(StrTicked)
                Else
                    Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
                End If
            Else
                StrTicked = FHPGD_PendingSaleOrder()
                If StrTicked <> "" Then
                    FFillItemsForOrder(StrTicked)
                Else
                    Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
                End If
            End If
            Dgl1.Focus()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function FHPGD_PendingSaleChallan() As String
        Dim FRH_Multiple As DMHelpGrid.FrmHelpGrid_Multi
        Dim StrSendText As String
        Dim StrRtn As String = ""

        StrSendText = RbtInvoiceForChallan.Tag

        mQry = "Select Distinct 'o' As Tick, H.DocId As SaleChallan, H.V_Type + '-' + H.ReferenceNo As SaleChallanNo, H.V_Date AS SaleChallanDate " & _
                         " From SaleInvoiceDetail L " & _
                         " LEFT JOIN SaleChallan H On L.SaleChallan = H.DocId " & _
                         " LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type " & _
                         " Where L.DocId = '" & mSearchCode & "' "
        mQry += " UNION ALL "
        mQry += " SELECT 'o' As Tick, H.DocId As SaleChallan, H.V_Type + '-' + H.ReferenceNo As SaleChallanNo, H.V_Date AS SaleChallanDate " & _
                    " FROM SaleChallan H  " & _
                    " LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type " & _
                    " Left Join  " & _
                    " 	(SELECT L.SaleChallan, Sum(L.Qty) AS InvoiceQty FROM SaleInvoiceDetail L GROUP BY L.SaleChallan) AS V1 " & _
                    " ON H.DocId = V1.SaleChallan " & _
                    " WHERE H.SaleToParty = '" & TxtSaleToParty.Tag & "'  " & _
                    " And H.V_Date <= '" & TxtV_Date.Text & "' " & _
                    " And H.TotalQty - IsNull(V1.InvoiceQty,0) > 0 "
        FRH_Multiple = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(AgL.FillData(mQry, AgL.GCn).TABLES(0)), "", 300, 600, , , False)
        FRH_Multiple.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple.FFormatColumn(1, , 0, , False)
        FRH_Multiple.FFormatColumn(2, "Challan No.", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(3, "Challan Date", 100, DataGridViewContentAlignment.MiddleLeft)

        FRH_Multiple.StartPosition = FormStartPosition.CenterScreen
        FRH_Multiple.ShowDialog()

        If FRH_Multiple.BytBtnValue = 0 Then
            StrRtn = FRH_Multiple.FFetchData(1, "'", "'", ",", True)
        End If
        FHPGD_PendingSaleChallan = StrRtn

        FRH_Multiple = Nothing
    End Function

    Private Function FHPGD_PendingSaleOrder() As String
        Dim FRH_Multiple As DMHelpGrid.FrmHelpGrid_Multi
        Dim StrSendText As String
        Dim StrRtn As String = ""

        StrSendText = RbtInvoiceForChallan.Tag

        mQry = "Select Distinct 'o' As Tick, H.DocId As SaleOrder, H.V_Type + '-' + H.ReferenceNo As SaleOrderNo, H.V_Date AS OrderDate " & _
         " From SaleInvoiceDetail L " & _
         " LEFT JOIN SaleOrder H On L.SaleOrder = H.DocId " & _
         " LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type " & _
         " Where L.DocId = '" & mSearchCode & "' "
        mQry += " UNION ALL "
        mQry += " SELECT 'o' As Tick, H.DocId As SaleOrder, H.V_Type + '-' + H.ReferenceNo As SaleOrderNo, H.V_Date AS OrderDate " & _
                    " FROM SaleOrder H  " & _
                    " LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type " & _
                    " Left Join " & _
                    " 	(SELECT L.SaleOrder, Sum(L.Qty) AS DispatchQty  " & _
                    " 	 FROM SaleChallanDetail L GROUP BY L.SaleOrder) AS V1 " & _
                    " ON H.DocId = V1.SaleOrder " & _
                    " WHERE H.SaleToParty = '" & TxtSaleToParty.Tag & "' " & _
                    " AND H.V_Date <= '" & TxtV_Date.Text & "' " & _
                    " And H.TotalQty - IsNull(V1.DispatchQty,0) > 0  "

        FRH_Multiple = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(AgL.FillData(mQry, AgL.GCn).TABLES(0)), "", 300, 600, , , False)
        FRH_Multiple.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple.FFormatColumn(1, , 0, , False)
        FRH_Multiple.FFormatColumn(2, "Order No.", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(3, "Order Date", 100, DataGridViewContentAlignment.MiddleLeft)

        FRH_Multiple.StartPosition = FormStartPosition.CenterScreen
        FRH_Multiple.ShowDialog()

        If FRH_Multiple.BytBtnValue = 0 Then
            StrRtn = FRH_Multiple.FFetchData(1, "'", "'", ",", True)
        End If
        FHPGD_PendingSaleOrder = StrRtn

        FRH_Multiple = Nothing
    End Function

    'Private Sub FOpenChallanList()
    '    Dim FrmObj As FrmSaleInvoiceChallanList = Nothing
    '    Dim I As Integer = 0
    '    Dim bChallanStr$ = ""
    '    If BtnFillSaleChallan.Tag Is Nothing Then
    '        FrmObj = New FrmSaleInvoiceChallanList
    '        FrmObj.IniGrid()
    '        FrmObj.Ini_List()
    '        FrmObj.LblBuyerName.Text = TxtSaleToParty.Text
    '        FFillPendingChallans(TxtSaleToParty.AgSelectedValue, TxtV_Date.Text, FrmObj)
    '    Else
    '        FrmObj = BtnFillSaleChallan.Tag
    '    End If
    '    FrmObj.ShowDialog()

    '    If FrmObj.mOkButtonPressed Then
    '        With FrmObj.DGL1
    '            If .Rows.Count > 0 Then
    '                For I = 0 To .Rows.Count - 1
    '                    If .Item(FrmSaleInvoiceChallanList.COl1SaleChallan, I).Value <> "" And AgL.StrCmp(.Item(FrmSaleInvoiceChallanList.COl1Select, I).Value, AgLibrary.ClsConstant.StrCheckedValue) Then
    '                        If bChallanStr = "" Then
    '                            bChallanStr = "'" & .AgSelectedValue(FrmSaleInvoiceChallanList.COl1SaleChallan, I) & "'"
    '                        Else
    '                            bChallanStr &= "," & "'" & .AgSelectedValue(FrmSaleInvoiceChallanList.COl1SaleChallan, I) & "'"
    '                        End If
    '                    End If
    '                Next
    '                Call FFillItemsForChallan(bChallanStr)
    '                BtnFillSaleChallan.Tag = FrmObj
    '            Else
    '                Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
    '            End If
    '        End With
    '    End If
    'End Sub

    'Private Sub FOpenOrderList()
    '    Dim FrmObj As FrmSaleInvoiceOrderList = Nothing
    '    Dim I As Integer = 0
    '    Dim bOrderStr$ = ""
    '    If BtnFillSaleChallan.Tag Is Nothing Then
    '        FrmObj = New FrmSaleInvoiceOrderList
    '        FrmObj.IniGrid()
    '        FrmObj.Ini_List()
    '        FrmObj.LblBuyerName.Text = TxtSaleToParty.Text
    '        FFillPendingOrders(TxtSaleToParty.AgSelectedValue, TxtV_Date.Text, FrmObj)
    '    Else
    '        FrmObj = BtnFillSaleChallan.Tag
    '    End If
    '    FrmObj.ShowDialog()

    '    If FrmObj.mOkButtonPressed Then
    '        With FrmObj.DGL1
    '            If .Rows.Count > 0 Then
    '                For I = 0 To .Rows.Count - 1
    '                    If .Item(FrmSaleInvoiceOrderList.COl1SaleOrder, I).Value <> "" And AgL.StrCmp(.Item(FrmSaleInvoiceOrderList.COl1Select, I).Value, AgLibrary.ClsConstant.StrCheckedValue) Then
    '                        If borderStr = "" Then
    '                            borderStr = "'" & .AgSelectedValue(FrmSaleInvoiceOrderList.COl1SaleOrder, I) & "'"
    '                        Else
    '                            borderStr &= "," & "'" & .AgSelectedValue(FrmSaleInvoiceOrderList.COl1SaleOrder, I) & "'"
    '                        End If
    '                    End If
    '                Next
    '                Call FFillItemsForOrder(bOrderStr)
    '                BtnFillSaleChallan.Tag = FrmObj
    '            Else
    '                Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
    '            End If
    '        End With
    '    End If
    'End Sub

    Private Sub FPostInSaleChallan(ByVal Conn As SqlClient.SqlConnection, ByVal Cmd As SqlClient.SqlCommand)
        Dim I As Integer = 0, Cnt As Integer = 0
        Dim bSelectionQry$ = ""

        If Dgl1.Item(Col1SaleChallan, 0).Value = "" Then
            mQry = " INSERT INTO SaleChallan(DocID, V_Type, V_Prefix, V_Date, V_No, Div_Code, Site_Code, ReferenceNo, SaleToParty, " & _
                    " Currency, SalesTaxGroupParty, Structure, BillingType, Form, FormNo,  " & _
                    " Remarks, TotalQty, TotalMeasure, TotalAmount, EntryBy, EntryDate, EntryType,  " & _
                    " EntryStatus, ApproveBy, ApproveDate, MoveToLog, MoveToLogDate, IsDeleted, Status, Godown) " & _
                    " SELECT DocID, V_Type, V_Prefix, V_Date, V_No, Div_Code, Site_Code, ReferenceNo, SaleToParty,  " & _
                    " Currency, SalesTaxGroupParty, Structure, BillingType, Form, FormNo,  " & _
                    " Remarks, TotalQty, TotalMeasure, TotalAmount, EntryBy, EntryDate, EntryType,  " & _
                    " EntryStatus, ApproveBy, ApproveDate, MoveToLog, MoveToLogDate, IsDeleted, Status, Godown " & _
                    " FROM SaleInvoice  " & _
                    " Where DocId = '" & mSearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

            Call FPostInChallanDetail(Conn, Cmd)
        ElseIf Dgl1.Item(Col1SaleChallan, 0).Tag = mSearchCode Then
            mQry = " UPDATE SaleChallan " & _
                        " SET  " & _
                        " SaleChallan.V_Date = SaleInvoice.V_Date,  " & _
                        " SaleChallan.ReferenceNo = SaleInvoice.ReferenceNo,  " & _
                        " SaleChallan.SaleToParty = SaleInvoice.SaleToParty,  " & _
                        " SaleChallan.Currency = SaleInvoice.Currency,  " & _
                        " SaleChallan.SalesTaxGroupParty = SaleInvoice.SalesTaxGroupParty,  " & _
                        " SaleChallan.Structure = SaleInvoice.Structure,  " & _
                        " SaleChallan.BillingType = SaleInvoice.BillingType,  " & _
                        " SaleChallan.Form = SaleInvoice.Form,  " & _
                        " SaleChallan.FormNo = SaleInvoice.FormNo,  " & _
                        " SaleChallan.Godown = SaleInvoice.Godown,  " & _
                        " SaleChallan.Remarks = SaleInvoice.Remarks,  " & _
                        " SaleChallan.TotalQty = SaleInvoice.TotalQty,  " & _
                        " SaleChallan.TotalMeasure = SaleInvoice.TotalMeasure,  " & _
                        " SaleChallan.TotalAmount = SaleInvoice.TotalAmount,  " & _
                        " SaleChallan.EntryBy = SaleInvoice.EntryBy,  " & _
                        " SaleChallan.EntryDate = SaleInvoice.EntryDate,  " & _
                        " SaleChallan.EntryType = SaleInvoice.EntryType,  " & _
                        " SaleChallan.EntryStatus = SaleInvoice.EntryStatus,  " & _
                        " SaleChallan.ApproveBy = SaleInvoice.ApproveBy,  " & _
                        " SaleChallan.ApproveDate = SaleInvoice.ApproveDate,  " & _
                        " SaleChallan.MoveToLog = SaleInvoice.MoveToLog,  " & _
                        " SaleChallan.MoveToLogDate = SaleInvoice.MoveToLogDate,  " & _
                        " SaleChallan.IsDeleted = SaleInvoice.IsDeleted,  " & _
                        " SaleChallan.Status = SaleInvoice.Status  " & _
                        " From SaleInvoice " & _
                        " Where SaleChallan.DocId = SaleInvoice.DocId " & _
                        " And SaleChallan.DocId = '" & mSearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

            mQry = " Delete From SaleChallanDetail Where DocId = '" & mSearchCode & "' "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

            mQry = " Delete From Stock Where DocId = '" & mSearchCode & "' "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

            Call FPostInChallanDetail(Conn, Cmd)
        End If


    End Sub

    Private Sub FPostInChallanDetail(ByVal Conn As SqlClient.SqlConnection, ByVal Cmd As SqlClient.SqlCommand)
        mQry = " INSERT INTO SaleChallanDetail(L.DocId, L.Sr, L.Item, L.SaleOrder, L.SalesTaxGroupItem, L.Qty, L.Unit, L.MeasurePerPcs, L.MeasureUnit, " & _
                " L.TotalMeasure, L.Rate, L.Amount)  " & _
                " SELECT L.DocId, L.Sr, L.Item, L.SaleOrder, L.SalesTaxGroupItem, L.Qty, L.Unit, L.MeasurePerPcs, L.MeasureUnit,  " & _
                " L.TotalMeasure, L.Rate, L.Amount " & _
                " FROM SaleInvoiceDetail L  " & _
                " Where L.DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " INSERT INTO Stock(DocID, Sr, V_Type, V_Prefix, V_Date, V_No, Div_Code, Site_Code,   " & _
                " SubCode, Currency, SalesTaxGroupParty, Structure, BillingType, Item,  " & _
                " Godown, Qty_Iss, Qty_Rec, Unit, LotNo, MeasurePerPcs, Measure_Iss, Measure_Rec, MeasureUnit, " & _
                " Rate, Amount, NetAmount, Remarks, RecId) " & _
                " SELECT L.DocId, L.Sr, H.V_Type, H.V_Prefix, H.V_Date, H.V_No, H.Div_Code, H.Site_Code, " & _
                " H.SaleToParty, H.Currency, H.SalesTaxGroupParty, H.Structure, H.BillingType, L.Item, H.Godown, L.Qty, 0, " & _
                " L.Unit, L.LotNo, L.MeasurePerPcs, 0, L.TotalMeasure, L.MeasureUnit, L.Rate, L.Amount, L.Amount, L.Remark, H.ReferenceNo  " & _
                " FROM SaleChallanDetail L  " & _
                " LEFT JOIN SaleChallan H ON L.DocId = H.DocID " & _
                " Where L.DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " UPDATE SaleInvoiceDetail " & _
                " Set " & _
                " SaleChallan = DocId, " & _
                " SaleChallanSr = Sr " & _
                " Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    End Sub

    Private Sub RbtInvoiceDirect_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RbtInvoiceDirect.Click, RbtInvoiceForChallan.Click, RbtInvoiceForOrder.Click
        Try
            Select Case sender.Name
                Case RbtInvoiceDirect.Name
                    BtnFillSaleChallan.Enabled = False

                Case RbtInvoiceForChallan.Name, RbtInvoiceForOrder.Name
                    BtnFillSaleChallan.Enabled = True
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FrmSaleInvoice_BaseEvent_Topctrl_tbRef() Handles Me.BaseEvent_Topctrl_tbRef
        Try
            If Dgl1.AgHelpDataSet(Col1Item) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1Item).Dispose() : Dgl1.AgHelpDataSet(Col1Item) = Nothing
        Catch ex As Exception
        End Try
        Try
            If Dgl1.AgHelpDataSet(Col1BillingType) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1BillingType).Dispose() : Dgl1.AgHelpDataSet(Col1BillingType) = Nothing
        Catch ex As Exception
        End Try
        If TxtCurrency.AgHelpDataSet IsNot Nothing Then TxtCurrency.AgHelpDataSet.Dispose() : TxtCurrency.AgHelpDataSet = Nothing
        If TxtSaleToParty.AgHelpDataSet IsNot Nothing Then TxtSaleToParty.AgHelpDataSet.Dispose() : TxtSaleToParty.AgHelpDataSet = Nothing
        If TxtSalesTaxGroupParty.AgHelpDataSet IsNot Nothing Then TxtSalesTaxGroupParty.AgHelpDataSet.Dispose() : TxtSalesTaxGroupParty.AgHelpDataSet = Nothing
    End Sub

    Private Sub PrintDocument(ByVal SearchCode As String)
        Dim mCrd As New ReportDocument
        Dim ReportView As New AgLibrary.RepView
        Dim DsRep As New DataSet
        Dim strQry As String = "", RepName As String = "", RepTitle As String = ""
        Dim bCondstr As String = ""

        Try
            Me.Cursor = Cursors.Default

            RepName = "SaleInvoice_Print" : RepTitle = "Material Issue Slip"

            mQry = " SELECT H.DocID, H.V_Type, H.V_Prefix, H.V_Date, H.V_No, H.Div_Code, H.Site_Code, H.ReferenceNo, H.SaleToParty, " & _
                        " H.SaleToPartyMobile, H.Currency,  " & _
                        " H.SalesTaxGroupParty, H.Structure, H.BillingType, H.Form, H.FormNo, H.ReferenceDocId, H.Remarks, H.TotalQty,  " & _
                        " H.TotalMeasure, H.TotalAmount, H.EntryBy, H.EntryDate, H.EntryType, H.EntryStatus, H.ApproveBy, H.ApproveDate,  " & _
                        " H.Godown, H.Vendor, H.SaleToPartyTinNo, H.SaleToPartyCstNo,  " & _
                        " H.Transporter, H.Vehicle, H.VehicleDescription, H.Driver, H.DriverName, H.DriverContactNo, H.LrNo, H.LrDate,  " & _
                        " H.CreditDays, L.DocId, L.Sr, L.SaleOrder, " & _
                        " L.SaleOrderSr, L.SaleChallan, L.SaleChallanSr, L.Item, L.Specification,  " & _
                        " L.SalesTaxGroupItem, L.Qty, L.Unit, L.MeasurePerPcs, L.MeasureUnit, L.TotalMeasure,  " & _
                        " L.Rate, L.Amount, L.ReferenceDocId, L.LotNo, L.UID, L.BaleNo, " & _
                        " Sg.DispName AS SaleToPartyName, Sg.Add1, Sg.Add2, Sg.Add3, C.CityName AS SaleToPartyCity , " & _
                        " G.Description AS GodownDesc, I.Description As ItemDesc, " & AgCalcGrid1.FLineTableFieldNameStr("H.", "H_") & ", " & _
                        " " & AgCustomGrid1.FHeaderTableFieldNameStr("H.", "H_") & " " & _
                        " FROM (SELECT * FROM SaleInvoice WHERE DocId = '" & mSearchCode & "') As H  " & _
                        " LEFT JOIN (SELECT * FROM SaleInvoiceDetail WHERE DocId = '" & mSearchCode & "') AS  L  ON H.DocID = L.DocId " & _
                        " LEFT JOIN SubGroup Sg ON H.SaleToParty = Sg.SubCode  " & _
                        " LEFT JOIN City C ON Sg.CityCode = C.CityCode " & _
                        " LEFT JOIN Godown G ON H.Godown = G.Code " & _
                        " LEFT JOIN Item I On L.Item = I.Code " & _
                        " Where H.DocId = '" & mSearchCode & "'"

            '" & AgCalcGrid1.FLineTableFieldNameStr("H.", "H_") & "
            AgL.ADMain = New SqlClient.SqlDataAdapter(mQry, AgL.GCn)
            AgL.ADMain.Fill(DsRep)
            AgPL.CreateFieldDefFile1(DsRep, AgL.PubReportPath & "\" & RepName & ".ttx", True)
            mCrd.Load(AgL.PubReportPath & "\" & RepName & ".rpt")
            mCrd.SetDataSource(DsRep.Tables(0))
            CType(ReportView.Controls("CrvReport"), CrystalDecisions.Windows.Forms.CrystalReportViewer).ReportSource = mCrd
            AgPL.Formula_Set(mCrd, RepTitle)
            AgPL.Show_Report(ReportView, "* " & RepTitle & " *", Me.MdiParent)


            Call AgL.LogTableEntry(mSearchCode, Me.Text, "P", AgL.PubMachineName, AgL.PubUserName, AgL.PubLoginDate, AgL.GCn, AgL.ECmd)
        Catch Ex As Exception
            MsgBox(Ex.Message)
        End Try
    End Sub

    Private Sub FrmPurchInvoice_StoreItem_BaseEvent_Topctrl_tbPrn(ByVal SearchCode As String) Handles Me.BaseEvent_Topctrl_tbPrn
        mQry = " SELECT H.DocID, H.V_Type, H.V_Prefix, H.V_Date, H.V_No, H.Div_Code, H.Site_Code,  " & _
                    " H.SaleToParty, H.SaleToPartyName, H.SaleToPartyAdd1, H.SaleToPartyAdd2,  " & _
                    " H.SaleToPartyCity, H.SaleToPartyCityName, H.SaleToPartyState, H.SaleToPartyCountry, " & _
                    " SG2.Mobile As SaleToPartyMobile ,SG2.EMail As SaleToPartyEMail, H.ShipToParty,  " & _
                    " H.ShipToPartyName, H.ShipToPartyAdd1, H.ShipToPartyAdd2, H.ShipToPartyCity,  " & _
                    " H.ShipToPartyCityName, H.ShipToPartyState, H.ShipToPartyCountry, H.Currency,  " & _
                    " H.SalesTaxGroupParty, H.Structure, H.BillingType, H.ReferenceNo, H.PartyOrderNo,  " & _
                    " H.PartyOrderDate, H.PartyDeliveryDate, H.PartyDeliveryTime, H.PartyOrderCancelDate,  " & _
                    " H.TermsAndConditions, H.Remarks, H.TotalQty, H.TotalMeasure, H.StockTotalMeasure,  " & _
                    " H.TotalAmount, H.SaleOrder, H.EntryBy, H.EntryDate, H.EntryType, H.EntryStatus,  " & _
                    " H.ApproveBy, H.ApproveDate, H.MoveToLog, H.MoveToLogDate, H.IsDeleted, H.Status,  " & _
                    " H.UID, H.DestinationPort, H.FinalPlaceOfDelivery, H.PreCarriageBy, H.PlaceOfReceipt,  " & _
                    " H.ShipmentThrough, H.BankAcNoBuyer, H.BankNameBuyer, H.BankAddressBuyer, H.PriceMode,  " & _
                    " H.Agent, H.CustomFields, H.Supplier, H.SaleToPartyMobile, H.ReferencePartyDocumentNo,  " & _
                    " H.ReferencePartyDocumentDate, H.OrderType, H.ReferenceParty, " & _
                    " L.DocId, L.Sr, L.Vendor, L.Item, L.Specification, L.PartySKU, L.PartyUPC,  " & _
                    " L.SalesTaxGroupItem, L.Qty, L.Unit, L.MeasurePerPcs, L.MeasureUnit, L.TotalMeasure,  " & _
                    " L.SaleOrder, L.BillingType, L.StockMeasurePerPcs, L.StockTotalMeasure, L.Rate, L.Amount,  " & _
                    " L.ShippedQty, L.ShippedMeasure, L.ProdOrdQty, L.ProdOrdMeasure, L.ProdPlanQty,  " & _
                    " L.ProdPlanMeasure, L.PurchQty, L.PurchMeasure, L.ProdIssQty, L.ProdIssMeasure,  " & _
                    " L.ProdRecQty, L.ProdRecMeasure, L.Priority, L.DeliveryOrderQty, L.DeliveryOrderMeasure,  " & _
                    " L.UID, L.DeliveryMeasure,  " & _
                    " L.DeliveryMeasurePerPcs, L.TotalDeliveryMeasure,  " & _
                    " L.Supplier, L.DeliveryMeasureMultiplier, " & _
                    " Sg.DispName AS AgentName, Sg1.DispName AS SupplierName, I.Description AS ItemDesc, " & _
                    " " & AgCalcGrid1.FLineTableFieldNameStr("H.", "H_") & " " & _
                    " " & AgCustomGrid1.FHeaderTableFieldNameStr("H.", "H_") & " " & _
                    " FROM (SELECT * FROM SaleOrder With (NoLock) WHERE DocId = '" & mSearchCode & "') AS H  " & _
                    " LEFT JOIN (SELECT * FROM SaleOrderDetail With (NoLock) WHERE DocId = '" & mSearchCode & "') AS L ON H.DocID = L.DocId " & _
                    " LEFT JOIN SubGroup Sg With (NoLock) ON H.Agent = Sg.SubCode " & _
                    " LEFT JOIN SubGroup Sg1 With (NoLock) ON L.Supplier = Sg1.SubCode " & _
                    " LEFT JOIN SubGroup Sg2 With (NoLock) On H.SaleTOParty = Sg2.SubCode " & _
                    " LEFT JOIN Item I With (NoLock) ON L.Item = I.Code " & _
                    " WHERE H.DocID='" & SearchCode & "'"


        mQry = "SELECT H.DocID, H.V_Type, H.V_Prefix, H.V_Date, H.V_No, H.Div_Code, H.Site_Code, H.ReferenceNo, " & _
            "H.Godown, H.Vendor, H.SaleToParty, H.SaleToPartyName, H.SaleToPartyAddress, H.SaleToPartyCity, " & _
            "H.SaleToPartyMobile, H.SaleToPartyTinNo, H.SaleToPartyCstNo, H.ShipToParty, H.ShipToPartyName, " & _
            "H.ShipToPartyAddress, H.ShipToPartyCity, H.ShipToPartyMobile, H.SaleOrder, H.SaleChallan, " & _
            "H.Currency, H.SalesTaxGroupParty, H.Structure, H.BillingType, H.Form, H.FormNo, " & _
            "H.Transporter, H.Vehicle, H.VehicleDescription, H.Driver, H.DriverName, " & _
            "H.DriverContactNo, H.LrNo, H.LrDate, H.PrivateMark, H.PortOfLoading, " & _
            "H.DestinationPort, H.FinalPlaceOfDelivery, H.PreCarriageBy, H.PlaceOfPreCarriage, " & _
            "H.ShipmentThrough, H.CreditDays, H.ReferenceDocId, H.Remarks, " & _
            "H.TotalQty, H.TotalMeasure, H.TotalAmount, H.EntryBy, H.EntryDate, " & _
            "H.EntryType, H.EntryStatus, H.ApproveBy, H.ApproveDate, H.MoveToLog, " & _
            "H.MoveToLogDate, H.IsDeleted, H.Status, H.UID, H.PaymentMode, H.TableCode, " & _
            "H.PostingAc, H.SaleToPartyAdd1, H.SaleToPartyAdd2, H.CustomFields, " & _
            "H.CreditLimit, H.BaleNoStr, H.InvoiceGenType, H.TotalDeliveryMeasure, H.TotalBale," & _
            "L.SaleOrder, L.SaleOrderSr, L.SaleChallan, L.SaleChallanSr, L.Item, L.Specification, " & _
            "L.SalesTaxGroupItem, L.DocQty, L.Qty, L.Unit, L.MeasurePerPcs, L.MeasureUnit, " & _
            "L.TotalDocMeasure, L.TotalMeasure, L.Rate, L.Amount, L.ReferenceDocId, " & _
            "L.LotNo, L.UID, L.BusinessVolume, L.PointValue, L.BaleNo, L.Remark, " & _
            "L.BillingType, L.Item_UID, L.ItemInvoiceGroup, L.SaleInvoice, " & _
            "L.SaleInvoiceSr, L.DeliveryMeasure, L.DeliveryMeasureMultiplier, " & _
            "L.TotalDeliveryMeasure, L.RateType, " & _
            "I.Description AS ItemDesc, I.ManualCode As ItemManualCode, " & _
            "G.Description AS GodownDesc, Sg.DispName As PartyName, " & _
            "Sg.ManualCode As PartyManualCode, Sg.Add1, Sg.Add2, Sg.Add3, C.CityName, BillToParty.Name as BillToPartyName, " & _
            " " & AgCalcGrid1.FFooterTableFieldNameStr("H.", "H_") & ", " & _
            " " & AgCalcGrid1.FLineTableFieldNameStr("L.", "L_") & " " & _
            " " & AgCustomGrid1.FHeaderTableFieldNameStr("H.", "H_") & " " & _
            "FROM SaleInvoice H " & _
            "LEFT JOIN SaleInvoiceDetail L ON H.DocID = L.DocId " & _
            "LEFT JOIN Godown G ON H.Godown = G.Code " & _
            "LEFT JOIN Item I ON L.Item = I.Code " & _
            "LEFT JOIN SubGroup Sg ON H.SaleToParty = Sg.SubCode " & _
            "LEFT JOIN SubGroup BillToParty ON H.BillToParty = BillToParty.SubCode " & _
            "LEFT JOIN City C ON Sg.CityCode = C.CityCode " & _
            "WHERE H.DocID='" & SearchCode & "'"



        ClsMain.FPrintThisDocument(Me, TxtV_Type.Tag, mQry, "SaleInvoice_Print", "Sale Invoice")

    End Sub

    Private Sub TxtDescription_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtRemarks.KeyDown
        'If e.KeyCode = Keys.Enter Then
        '    If MsgBox("Do you want to save?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "Save") = MsgBoxResult.Yes Then
        '        Topctrl1.FButtonClick(13)
        '    End If
        'End If
    End Sub

    Private Sub FSavePartyDetail(ByVal Conn As SqlClient.SqlConnection, ByVal Cmd As SqlClient.SqlCommand)
        If BtnFillPartyDetail.Tag Is Nothing Then Exit Sub

        mQry = " UPDATE SaleInvoice " & _
                " SET SaleToPartyName = '" & BtnFillPartyDetail.Tag.TxtSaleToPartyName.Text & "', " & _
                " SaleToPartyAdd1 = '" & BtnFillPartyDetail.Tag.TxtSaleToPartyAdd1.Text & "', " & _
                " SaleToPartyAdd2 = '" & BtnFillPartyDetail.Tag.TxtSaleToPartyAdd2.Text & "', " & _
                " SaleToPartyCity = '" & BtnFillPartyDetail.Tag.TxtSaleToPartyCity.AgSelectedValue & "', " & _
                " SaleToPartyMobile = '" & BtnFillPartyDetail.Tag.TxtSaleToPartyMobile.Text & "' " & _
                " Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    End Sub

    Private Sub BtnFillPartyDetail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnFillPartyDetail.Click
        FOpenPartyDetail()
    End Sub

    Private Sub FOpenPartyDetail()
        Dim FrmObj As FrmSaleInvoicePartyDetail
        Try
            If BtnFillPartyDetail.Tag Is Nothing Then
                FrmObj = New FrmSaleInvoicePartyDetail
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

    Private Sub FGetDeliveryMeasureMultiplier(ByVal mRow As Integer)
        Dim DtTemp As DataTable = Nothing
        Dim I As Integer = 0
        Try
            If Dgl1.Item(Col1Unit, mRow).Value <> "" And Dgl1.Item(Col1DeliveryMeasure, mRow).Value <> "" And Val(Dgl1.Item(Col1MeasurePerPcs, mRow).Value) <> 0 Then
                mQry = " SELECT Multiplier, Rounding FROM UnitConversion WHERE FromUnit = '" & Dgl1.Item(Col1MeasureUnit, mRow).Value & "' AND ToUnit =  '" & Dgl1.Item(Col1DeliveryMeasure, mRow).Value & "' "
                DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
                With DtTemp
                    If .Rows.Count > 0 Then
                        Dgl1.Item(Col1DeliveryMeasureMultiplier, mRow).Value = AgL.VNull(.Rows(0)("Multiplier"))
                    End If
                End With
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FGetBaleStr(ByVal SearchCode As String)
        Dim I As Integer
        Dim mBale As String = ""
        Dim tBale As Integer = 0
        Dim fBale As Integer = 0

        Dim DsTemp As DataSet

        mQry = "Select Distinct Convert(Float,BaleNo) as BaleNo from SaleInvoiceDetail With (NoLock) Where DocId = '" & SearchCode & "' Order By  Convert(Float,BaleNo) "
        DsTemp = AgL.FillData(mQry, AgL.GcnRead)
        With DsTemp.Tables(0)

            If .Rows.Count > 0 Then
                For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                    If fBale = 0 Then
                        fBale = AgL.VNull(.Rows(I)("BaleNo"))
                        mBale = AgL.XNull(.Rows(I)("BaleNo"))
                    ElseIf fBale + 1 <> AgL.VNull(.Rows(I)("BaleNo")) Then
                        mBale = mBale & "-" & AgL.XNull(.Rows(I - 1)("BaleNo")) & ", " & AgL.XNull(.Rows(I)("BaleNo"))
                        fBale = AgL.VNull(.Rows(I)("BaleNo"))
                    Else
                        fBale = AgL.VNull(.Rows(I)("BaleNo"))
                    End If

                    If I = DsTemp.Tables(0).Rows.Count - 1 Then
                        If fBale <> AgL.VNull(.Rows(I)("BaleNo")) Then
                            mBale = mBale & ", " & AgL.XNull(.Rows(I)("BaleNo")) & ""
                        Else
                            mBale = mBale & "-" & AgL.XNull(.Rows(I)("BaleNo")) & ""
                        End If
                    End If
                Next I
            End If
        End With
        TxtBaleNo.Text = mBale
    End Sub

    Private Sub Dgl1_CellContentClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellContentClick
        Dim Mdi As MDIMain = New MDIMain
        Try
            Select Case Dgl1.Columns(e.ColumnIndex).Name
                Case Col1SaleOrder
                    'Call ClsMain.ProcOpenLinkForm(Mdi.MnuSaleOrderEntry, Dgl1.Item(Col1SaleOrder, e.RowIndex).Tag, Me.MdiParent)

                    'Case Col1SaleChallan
                    '    Call ClsMain.ProcOpenLinkForm(Mdi.MnuQCRequestEntry, Dgl1.Item(Col1SaleQCReq, e.RowIndex).Tag, Me.MdiParent)
            End Select
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Dgl1_EditingControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.EditingControl_KeyDown
        Try
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1ItemCode
                    If e.KeyCode = Keys.Insert Then Call FOpenMaster(e)
                    If Dgl1.AgHelpDataSet(Col1ItemCode) Is Nothing Then
                        mQry = "SELECT I.Code, I.ManualCode, I.Description, I.Unit, I.ItemType, I.SalesTaxPostingGroup , " & _
                               " IsNull(I.IsDeleted ,0) AS IsDeleted, I.Div_Code, " & _
                               " I.MeasureUnit, I.Measure As MeasurePerPcs, I.Rate As Rate, 1 As PendingQty, I.Status, " & _
                               " U.DecimalPlaces as QtyDecimalPlaces, U1.DecimalPlaces as MeasureDecimalPlaces " & _
                               " FROM Item I " & _
                               " LEFT JOIN Unit U On I.Unit = U.Code " & _
                               " LEFT JOIN Unit U1 On I.MeasureUnit = U1.Code " & _
                               " Where I.ItemType IN ('" & mItemType & "') " & _
                               " And IsNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' "
                        Dgl1.AgHelpDataSet(Col1ItemCode, 12) = AgL.FillData(mQry, AgL.GCn)
                    End If



                Case Col1Item
                    If e.KeyCode = Keys.Insert Then Call FOpenMaster(e)
                    If Dgl1.AgHelpDataSet(Col1Item) Is Nothing Then
                        mQry = "SELECT I.Code, I.Description, I.ManualCode, I.Unit, I.ItemType, I.SalesTaxPostingGroup , " & _
                               " IsNull(I.IsDeleted ,0) AS IsDeleted, I.Div_Code, " & _
                               " I.MeasureUnit, I.Measure As MeasurePerPcs, I.Rate As Rate, 1 As PendingQty, I.Status, " & _
                               " U.DecimalPlaces As QtyDecimalPlaces, U1.DecimalPlaces As MeasureDecimalPlaces " & _
                               " FROM Item I " & _
                               " LEFT JOIN Unit U On I.Unit = U.Code " & _
                               " LEFT JOIN Unit U1 On I.MeasureUnit = U1.Code " & _
                               " Where I.ItemType IN ('" & mItemType & "') " & _
                               " And IsNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' "
                        Dgl1.AgHelpDataSet(Col1Item, 12) = AgL.FillData(mQry, AgL.GCn)
                    End If

                Case Col1BillingType
                    If Dgl1.AgHelpDataSet(Col1BillingType) Is Nothing Then
                        mQry = " SELECT 'Qty' AS Code, 'Qty' AS Name " & _
                                " Union ALL " & _
                                " SELECT 'Measure' AS Code, 'Measure' AS Name "
                        Dgl1.AgHelpDataSet(Col1BillingType) = AgL.FillData(mQry, AgL.GCn)
                    End If

                Case Col1DeliveryMeasure
                    If Dgl1.AgHelpDataSet(Col1DeliveryMeasure) Is Nothing Then
                        mQry = " SELECT Code, Code AS Description FROM Unit "
                        Dgl1.AgHelpDataSet(Col1DeliveryMeasure) = AgL.FillData(mQry, AgL.GCn)
                    End If

                Case Col1RateType
                    If Dgl1.AgHelpDataSet(Col1RateType) Is Nothing Then
                        mQry = " SELECT H.Code, H.Description  FROM RateType H " & _
                                " Where IsNull(H.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' "
                        Dgl1.AgHelpDataSet(Col1RateType) = AgL.FillData(mQry, AgL.GCn)
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FOpenMaster(ByVal e As System.Windows.Forms.KeyEventArgs)
        Dim FrmObj As Object = Nothing
        Dim CFOpen As New ClsFunction
        Dim DtTemp As DataTable = Nothing
        Try
            If AgL.StrCmp(Topctrl1.Mode, "Browse") Then Exit Sub
            If e.KeyCode = Keys.Insert Then
                If Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name = Col1Item Then
                    If Not mItemType.Contains(",") Then
                        mQry = " Select MnuName, MnuText From ItemType Where Code = '" & mItemType & "' "
                        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
                        If DtTemp.Rows.Count > 0 Then
                            FrmObj = CFOpen.FOpen(DtTemp.Rows(0)("MnuName"), DtTemp.Rows(0)("MnuText"), True)
                            If FrmObj IsNot Nothing Then
                                FrmObj.MdiParent = Me.MdiParent
                                FrmObj.Show()
                                FrmObj.Topctrl1.FButtonClick(0)
                                FrmObj = Nothing
                            End If
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub FSetParameter(ByVal IsMeasurePerPcsEditable As Boolean, ByVal IsMeasureUnitEditable As Boolean, _
                             ByVal IsMeasureEditable As Boolean, ByVal IsMeasurePerPcsVisible As Boolean, _
                             ByVal IsMeasureVisible As Boolean, ByVal IsMeasureUnitVisible As Boolean, _
                             ByVal IsDeliveryMeasureVisible As Boolean, ByVal IsTotalDeliveryMeasureVisible As Boolean, _
                             ByVal IsBaleNoVisible As Boolean, ByVal IsBillingTypeVisible As Boolean, _
                             ByVal IsItemCodeVisible As Boolean, ByVal IsItemUIDVisible As Boolean, _
                             ByVal IsCheckQcQty As Boolean, ByVal IsDirectInvoice As Boolean, _
                             ByVal IsRateTypeVisible As Boolean)
        BlnIsMeasurePerPcsEditable = IsMeasurePerPcsEditable
        BlnIsMeasureEditable = IsMeasureEditable
        BlnIsMeasureUnitEditable = IsMeasureUnitEditable
        BlnIsMeasurePerPcsVisible = IsMeasurePerPcsVisible
        BlnIsMeasureVisible = IsMeasureVisible
        BlnIsMeasureUnitVisible = IsMeasureUnitVisible
        BlnIsDeliveryMeasureVisible = IsDeliveryMeasureVisible
        BlnIsTotalDeliveryMeasureVisible = IsTotalDeliveryMeasureVisible
        BlnIsBaleNoVisible = IsBaleNoVisible
        BlnIsBillingTypeVisible = IsBillingTypeVisible
        BlnIsItemCodeVisible = IsItemCodeVisible
        BlnIsItemUIDVisible = IsItemUIDVisible
        BlnIsCheckQcQty = IsCheckQcQty
        BlnIsDirectInvoice = IsDirectInvoice
        BlnIsRateTypeVisible = IsRateTypeVisible
    End Sub
End Class
