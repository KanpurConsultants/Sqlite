Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Data.SQLite
Public Class FrmPurchOrder
    Inherits AgTemplate.TempTransaction
    Dim mQry$

    Public WithEvents AgCalcGrid1 As New AgStructure.AgCalcGrid
    Public WithEvents AgCustomGrid1 As New AgCustomFields.AgCustomGrid

    Public WithEvents Dgl1 As AgControls.AgDataGrid
    Protected Const ColSNo As String = "S.No."
    Protected Const Col1ItemCode As String = "Item Code"
    Protected Const Col1Item As String = "Item"
    Protected Const Col1Dimension1 As String = "Dimension1"
    Protected Const Col1Dimension2 As String = "Dimension2"
    Protected Const Col1PurchQuotation As String = "Quotation No"
    Protected Const Col1PurchQuotationSr As String = "Purch Quotation Sr"
    Protected Const Col1PurchIndent As String = "Indent No"
    Protected Const Col1PurchIndentSr As String = "Purch Indent Sr"
    Protected Const Col1MaterialPlan As String = "Material Plan No"
    Protected Const Col1MaterialPlanSr As String = "Material Plan Sr"
    Protected Const Col1Specification As String = "Specification"
    Protected Const Col1PartySKU As String = "Party SKU"
    Protected Const Col1XPartySKU As String = "X Party SKU"
    Protected Const Col1BillingType As String = "Billing Type"
    Protected Const Col1RateType As String = "Rate Type"
    Protected Const Col1DeliveryMeasure As String = "Delivery Measure"
    Protected Const Col1Qty As String = "Qty"
    Protected Const Col1FreeQty As String = "Free Qty"
    Protected Const Col1Unit As String = "Unit"
    Protected Const Col1QtyDecimalPlaces As String = "Qty Decimal Places"
    Protected Const Col1SalesTaxGroup As String = "Sales Tax Group"
    Protected Const Col1MeasurePerPcs As String = "Measure Per Qty"
    Protected Const Col1PcsPerMeasure As String = "Qty Per Measure"
    Protected Const Col1TotalMeasure As String = "Total Measure"
    Protected Const Col1TotalFreeMeasure As String = "Total Free Measure"
    Protected Const Col1MeasureUnit As String = "Measure Unit"
    Protected Const Col1MeasureDecimalPlaces As String = "Measure Decimal Places"
    Protected Const Col1DeliveryMeasureMultiplier As String = "Delivery Measure Multiplier"
    Protected Const Col1DeliveryMeasurePerPcs As String = "Delivery Measure Per Qty"
    Protected Const Col1TotalDeliveryMeasure As String = "Total Delivery Measure"
    Protected Const Col1TotalFreeDeliveryMeasure As String = "Total Free Delivery Measure"
    Protected Const Col1DeliveryMeasureDecimalPlaces As String = "Delivery Measure Decimal Places"
    Protected Const Col1MRP As String = "MRP"
    Protected Const Col1Deal As String = "Deal"
    Protected Const Col1Rate As String = "Rate"
    Protected Const Col1Amount As String = "Amount"
    Protected Const Col1BtnDeliveryDetail As String = "Delivery Detail"

    Dim mIsEntryLocked As Boolean = False

    Dim Dgl As New AgControls.AgDataGrid

    Dim DtPurchaseEnviro As DataTable
    Protected WithEvents BtnPrintBarcode As System.Windows.Forms.Button
    Protected WithEvents Label3 As System.Windows.Forms.Label
    Protected WithEvents TxtTermsAndConditions As AgControls.AgTextBox
    Public blnIsCarpetTrans As Boolean


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
        Me.Dgl1 = New AgControls.AgDataGrid
        Me.Label4 = New System.Windows.Forms.Label
        Me.TxtVendor = New AgControls.AgTextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.TxtDeliveryDate = New AgControls.AgTextBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.LblTotalDeliveryMeasure = New System.Windows.Forms.Label
        Me.LblTotalDeliveryMeasureText = New System.Windows.Forms.Label
        Me.LblTotalMeasure = New System.Windows.Forms.Label
        Me.LblTotalMeasureText = New System.Windows.Forms.Label
        Me.LblTotalAmount = New System.Windows.Forms.Label
        Me.LblTotalAmountText = New System.Windows.Forms.Label
        Me.LblTotalQty = New System.Windows.Forms.Label
        Me.LblTotalQtyText = New System.Windows.Forms.Label
        Me.Pnl1 = New System.Windows.Forms.Panel
        Me.ChkDeliveryDetailNotRequired = New System.Windows.Forms.CheckBox
        Me.PnlCalcGrid = New System.Windows.Forms.Panel
        Me.TxtStructure = New AgControls.AgTextBox
        Me.Label25 = New System.Windows.Forms.Label
        Me.TxtSalesTaxGroupParty = New AgControls.AgTextBox
        Me.Label27 = New System.Windows.Forms.Label
        Me.Label24 = New System.Windows.Forms.Label
        Me.TxtShipToParty = New AgControls.AgTextBox
        Me.Label22 = New System.Windows.Forms.Label
        Me.TxtShipToPartyAdd1 = New AgControls.AgTextBox
        Me.TxtShipToPartyAdd2 = New AgControls.AgTextBox
        Me.Label21 = New System.Windows.Forms.Label
        Me.TxtShipToPartyCity = New AgControls.AgTextBox
        Me.Label20 = New System.Windows.Forms.Label
        Me.TxtShipToPartyState = New AgControls.AgTextBox
        Me.Label19 = New System.Windows.Forms.Label
        Me.TxtShipToPartyCountry = New AgControls.AgTextBox
        Me.TPShipping = New System.Windows.Forms.TabPage
        Me.TxtReferencePartyDocumentDate = New AgControls.AgTextBox
        Me.Label14 = New System.Windows.Forms.Label
        Me.TxtReferencePartyDocumentNo = New AgControls.AgTextBox
        Me.Label34 = New System.Windows.Forms.Label
        Me.TxtReferenceParty = New AgControls.AgTextBox
        Me.Label35 = New System.Windows.Forms.Label
        Me.TxtRemarks = New AgControls.AgTextBox
        Me.Label30 = New System.Windows.Forms.Label
        Me.TxtAgent = New AgControls.AgTextBox
        Me.LblAgent = New System.Windows.Forms.Label
        Me.TxtCurrency = New AgControls.AgTextBox
        Me.Label28 = New System.Windows.Forms.Label
        Me.BtnFillPartyDetail = New System.Windows.Forms.Button
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel
        Me.Label1 = New System.Windows.Forms.Label
        Me.TxtReferenceNo = New AgControls.AgTextBox
        Me.LblReferenceNo = New System.Windows.Forms.Label
        Me.TxtCustomFields = New AgControls.AgTextBox
        Me.PnlCustomGrid = New System.Windows.Forms.Panel
        Me.TxtNature = New AgControls.AgTextBox
        Me.BtnMailBox = New System.Windows.Forms.Button
        Me.GrpMailBox = New System.Windows.Forms.GroupBox
        Me.BtnFillPendingQuotation = New System.Windows.Forms.Button
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.RbtOrderForIndent = New System.Windows.Forms.RadioButton
        Me.RbtOrderForQuotation = New System.Windows.Forms.RadioButton
        Me.RbtOrderDirect = New System.Windows.Forms.RadioButton
        Me.BtnPrintBarcode = New System.Windows.Forms.Button
        Me.Label3 = New System.Windows.Forms.Label
        Me.TxtTermsAndConditions = New AgControls.AgTextBox
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
        Me.Pnl1.SuspendLayout()
        Me.TPShipping.SuspendLayout()
        Me.GrpMailBox.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.Location = New System.Drawing.Point(832, 574)
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
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(653, 574)
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
        Me.GBoxApprove.Location = New System.Drawing.Point(466, 574)
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
        Me.GBoxEntryType.Location = New System.Drawing.Point(150, 574)
        Me.GBoxEntryType.Size = New System.Drawing.Size(119, 40)
        '
        'TxtEntryType
        '
        Me.TxtEntryType.Location = New System.Drawing.Point(3, 19)
        Me.TxtEntryType.Tag = ""
        '
        'GrpUP
        '
        Me.GrpUP.Location = New System.Drawing.Point(16, 574)
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
        Me.GroupBox1.Location = New System.Drawing.Point(2, 562)
        Me.GroupBox1.Size = New System.Drawing.Size(1002, 4)
        '
        'GBoxDivision
        '
        Me.GBoxDivision.Location = New System.Drawing.Point(300, 574)
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
        Me.LblV_No.Location = New System.Drawing.Point(234, 238)
        Me.LblV_No.Size = New System.Drawing.Size(64, 16)
        Me.LblV_No.Tag = ""
        Me.LblV_No.Text = "Order No."
        Me.LblV_No.Visible = False
        '
        'TxtV_No
        '
        Me.TxtV_No.AgSelectedValue = ""
        Me.TxtV_No.BackColor = System.Drawing.Color.White
        Me.TxtV_No.Location = New System.Drawing.Point(342, 237)
        Me.TxtV_No.Size = New System.Drawing.Size(163, 18)
        Me.TxtV_No.TabIndex = 3
        Me.TxtV_No.Tag = ""
        Me.TxtV_No.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.TxtV_No.Visible = False
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(112, 36)
        Me.Label2.Tag = ""
        '
        'LblV_Date
        '
        Me.LblV_Date.BackColor = System.Drawing.Color.Transparent
        Me.LblV_Date.Location = New System.Drawing.Point(19, 31)
        Me.LblV_Date.Size = New System.Drawing.Size(71, 16)
        Me.LblV_Date.Tag = ""
        Me.LblV_Date.Text = "Order Date"
        '
        'LblV_TypeReq
        '
        Me.LblV_TypeReq.Location = New System.Drawing.Point(326, 16)
        Me.LblV_TypeReq.Tag = ""
        '
        'TxtV_Date
        '
        Me.TxtV_Date.AgSelectedValue = ""
        Me.TxtV_Date.BackColor = System.Drawing.Color.White
        Me.TxtV_Date.Location = New System.Drawing.Point(128, 30)
        Me.TxtV_Date.TabIndex = 2
        Me.TxtV_Date.Tag = ""
        '
        'LblV_Type
        '
        Me.LblV_Type.Location = New System.Drawing.Point(234, 12)
        Me.LblV_Type.Size = New System.Drawing.Size(71, 16)
        Me.LblV_Type.Tag = ""
        Me.LblV_Type.Text = "Order Type"
        '
        'TxtV_Type
        '
        Me.TxtV_Type.AgLastValueTag = ""
        Me.TxtV_Type.AgLastValueText = ""
        Me.TxtV_Type.AgSelectedValue = ""
        Me.TxtV_Type.BackColor = System.Drawing.Color.White
        Me.TxtV_Type.Location = New System.Drawing.Point(342, 10)
        Me.TxtV_Type.Size = New System.Drawing.Size(163, 18)
        Me.TxtV_Type.TabIndex = 1
        Me.TxtV_Type.Tag = ""
        '
        'LblSite_CodeReq
        '
        Me.LblSite_CodeReq.Location = New System.Drawing.Point(112, 16)
        Me.LblSite_CodeReq.Tag = ""
        '
        'LblSite_Code
        '
        Me.LblSite_Code.BackColor = System.Drawing.Color.Transparent
        Me.LblSite_Code.Location = New System.Drawing.Point(19, 11)
        Me.LblSite_Code.Size = New System.Drawing.Size(87, 16)
        Me.LblSite_Code.Tag = ""
        Me.LblSite_Code.Text = "Branch Name"
        '
        'TxtSite_Code
        '
        Me.TxtSite_Code.AgSelectedValue = ""
        Me.TxtSite_Code.BackColor = System.Drawing.Color.White
        Me.TxtSite_Code.Location = New System.Drawing.Point(128, 10)
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
        Me.LblPrefix.Location = New System.Drawing.Point(294, 238)
        Me.LblPrefix.Tag = ""
        Me.LblPrefix.Visible = False
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TPShipping)
        Me.TabControl1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl1.Location = New System.Drawing.Point(-4, 41)
        Me.TabControl1.Size = New System.Drawing.Size(992, 135)
        Me.TabControl1.TabIndex = 0
        Me.TabControl1.Controls.SetChildIndex(Me.TPShipping, 0)
        Me.TabControl1.Controls.SetChildIndex(Me.TP1, 0)
        '
        'TP1
        '
        Me.TP1.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.TP1.Controls.Add(Me.TxtNature)
        Me.TP1.Controls.Add(Me.Label1)
        Me.TP1.Controls.Add(Me.TxtReferenceNo)
        Me.TP1.Controls.Add(Me.LblReferenceNo)
        Me.TP1.Controls.Add(Me.BtnFillPartyDetail)
        Me.TP1.Controls.Add(Me.TxtSalesTaxGroupParty)
        Me.TP1.Controls.Add(Me.Label27)
        Me.TP1.Controls.Add(Me.TxtStructure)
        Me.TP1.Controls.Add(Me.Label25)
        Me.TP1.Controls.Add(Me.Label4)
        Me.TP1.Controls.Add(Me.TxtVendor)
        Me.TP1.Controls.Add(Me.Label5)
        Me.TP1.Controls.Add(Me.TxtRemarks)
        Me.TP1.Controls.Add(Me.Label30)
        Me.TP1.Controls.Add(Me.TxtDeliveryDate)
        Me.TP1.Controls.Add(Me.Label11)
        Me.TP1.Controls.Add(Me.TxtAgent)
        Me.TP1.Controls.Add(Me.LblAgent)
        Me.TP1.Location = New System.Drawing.Point(4, 22)
        Me.TP1.Size = New System.Drawing.Size(984, 109)
        Me.TP1.Text = "Document Detail"
        Me.TP1.Controls.SetChildIndex(Me.LblAgent, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtAgent, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label11, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtDeliveryDate, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label30, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtRemarks, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label5, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtVendor, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label4, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label25, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtStructure, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label27, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtSalesTaxGroupParty, 0)
        Me.TP1.Controls.SetChildIndex(Me.BtnFillPartyDetail, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblReferenceNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtReferenceNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label1, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtNature, 0)
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
        Me.Label4.Location = New System.Drawing.Point(112, 57)
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
        Me.TxtVendor.Location = New System.Drawing.Point(128, 50)
        Me.TxtVendor.MaxLength = 0
        Me.TxtVendor.Name = "TxtVendor"
        Me.TxtVendor.Size = New System.Drawing.Size(348, 18)
        Me.TxtVendor.TabIndex = 4
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(19, 51)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(48, 16)
        Me.Label5.TabIndex = 693
        Me.Label5.Text = "Vendor"
        '
        'TxtDeliveryDate
        '
        Me.TxtDeliveryDate.AgAllowUserToEnableMasterHelp = False
        Me.TxtDeliveryDate.AgLastValueTag = Nothing
        Me.TxtDeliveryDate.AgLastValueText = Nothing
        Me.TxtDeliveryDate.AgMandatory = True
        Me.TxtDeliveryDate.AgMasterHelp = True
        Me.TxtDeliveryDate.AgNumberLeftPlaces = 8
        Me.TxtDeliveryDate.AgNumberNegetiveAllow = False
        Me.TxtDeliveryDate.AgNumberRightPlaces = 2
        Me.TxtDeliveryDate.AgPickFromLastValue = False
        Me.TxtDeliveryDate.AgRowFilter = ""
        Me.TxtDeliveryDate.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtDeliveryDate.AgSelectedValue = Nothing
        Me.TxtDeliveryDate.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtDeliveryDate.AgValueType = AgControls.AgTextBox.TxtValueType.Date_Value
        Me.TxtDeliveryDate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtDeliveryDate.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDeliveryDate.Location = New System.Drawing.Point(128, 70)
        Me.TxtDeliveryDate.MaxLength = 20
        Me.TxtDeliveryDate.Name = "TxtDeliveryDate"
        Me.TxtDeliveryDate.Size = New System.Drawing.Size(100, 18)
        Me.TxtDeliveryDate.TabIndex = 5
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(19, 71)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(84, 16)
        Me.Label11.TabIndex = 710
        Me.Label11.Text = "Delivery Date"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Cornsilk
        Me.Panel1.Controls.Add(Me.LblTotalDeliveryMeasure)
        Me.Panel1.Controls.Add(Me.LblTotalDeliveryMeasureText)
        Me.Panel1.Controls.Add(Me.LblTotalMeasure)
        Me.Panel1.Controls.Add(Me.LblTotalMeasureText)
        Me.Panel1.Controls.Add(Me.LblTotalAmount)
        Me.Panel1.Controls.Add(Me.LblTotalAmountText)
        Me.Panel1.Controls.Add(Me.LblTotalQty)
        Me.Panel1.Controls.Add(Me.LblTotalQtyText)
        Me.Panel1.Location = New System.Drawing.Point(2, 378)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(979, 23)
        Me.Panel1.TabIndex = 694
        '
        'LblTotalDeliveryMeasure
        '
        Me.LblTotalDeliveryMeasure.AutoSize = True
        Me.LblTotalDeliveryMeasure.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalDeliveryMeasure.ForeColor = System.Drawing.Color.Black
        Me.LblTotalDeliveryMeasure.Location = New System.Drawing.Point(636, 3)
        Me.LblTotalDeliveryMeasure.Name = "LblTotalDeliveryMeasure"
        Me.LblTotalDeliveryMeasure.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalDeliveryMeasure.TabIndex = 712
        Me.LblTotalDeliveryMeasure.Text = "."
        '
        'LblTotalDeliveryMeasureText
        '
        Me.LblTotalDeliveryMeasureText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalDeliveryMeasureText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalDeliveryMeasureText.Location = New System.Drawing.Point(340, 3)
        Me.LblTotalDeliveryMeasureText.Name = "LblTotalDeliveryMeasureText"
        Me.LblTotalDeliveryMeasureText.Size = New System.Drawing.Size(282, 19)
        Me.LblTotalDeliveryMeasureText.TabIndex = 711
        Me.LblTotalDeliveryMeasureText.Text = "Deilvery Measure :"
        Me.LblTotalDeliveryMeasureText.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'LblTotalMeasure
        '
        Me.LblTotalMeasure.AutoSize = True
        Me.LblTotalMeasure.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalMeasure.ForeColor = System.Drawing.Color.Black
        Me.LblTotalMeasure.Location = New System.Drawing.Point(303, 3)
        Me.LblTotalMeasure.Name = "LblTotalMeasure"
        Me.LblTotalMeasure.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalMeasure.TabIndex = 666
        Me.LblTotalMeasure.Text = "."
        Me.LblTotalMeasure.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblTotalMeasure.Visible = False
        '
        'LblTotalMeasureText
        '
        Me.LblTotalMeasureText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalMeasureText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalMeasureText.Location = New System.Drawing.Point(196, 3)
        Me.LblTotalMeasureText.Name = "LblTotalMeasureText"
        Me.LblTotalMeasureText.Size = New System.Drawing.Size(101, 16)
        Me.LblTotalMeasureText.TabIndex = 665
        Me.LblTotalMeasureText.Text = "Measure :"
        Me.LblTotalMeasureText.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.LblTotalMeasureText.Visible = False
        '
        'LblTotalAmount
        '
        Me.LblTotalAmount.AutoSize = True
        Me.LblTotalAmount.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalAmount.ForeColor = System.Drawing.Color.Black
        Me.LblTotalAmount.Location = New System.Drawing.Point(880, 3)
        Me.LblTotalAmount.Name = "LblTotalAmount"
        Me.LblTotalAmount.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalAmount.TabIndex = 662
        Me.LblTotalAmount.Text = "."
        Me.LblTotalAmount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTotalAmountText
        '
        Me.LblTotalAmountText.AutoSize = True
        Me.LblTotalAmountText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalAmountText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalAmountText.Location = New System.Drawing.Point(812, 3)
        Me.LblTotalAmountText.Name = "LblTotalAmountText"
        Me.LblTotalAmountText.Size = New System.Drawing.Size(65, 16)
        Me.LblTotalAmountText.TabIndex = 661
        Me.LblTotalAmountText.Text = "Amount :"
        '
        'LblTotalQty
        '
        Me.LblTotalQty.AutoSize = True
        Me.LblTotalQty.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalQty.ForeColor = System.Drawing.Color.Black
        Me.LblTotalQty.Location = New System.Drawing.Point(144, 3)
        Me.LblTotalQty.Name = "LblTotalQty"
        Me.LblTotalQty.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalQty.TabIndex = 660
        Me.LblTotalQty.Text = "."
        Me.LblTotalQty.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTotalQtyText
        '
        Me.LblTotalQtyText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalQtyText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalQtyText.Location = New System.Drawing.Point(3, 3)
        Me.LblTotalQtyText.Name = "LblTotalQtyText"
        Me.LblTotalQtyText.Size = New System.Drawing.Size(134, 16)
        Me.LblTotalQtyText.TabIndex = 659
        Me.LblTotalQtyText.Text = "Qty :"
        Me.LblTotalQtyText.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Pnl1
        '
        Me.Pnl1.Controls.Add(Me.ChkDeliveryDetailNotRequired)
        Me.Pnl1.Location = New System.Drawing.Point(2, 206)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(978, 172)
        Me.Pnl1.TabIndex = 1
        '
        'ChkDeliveryDetailNotRequired
        '
        Me.ChkDeliveryDetailNotRequired.AutoSize = True
        Me.ChkDeliveryDetailNotRequired.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkDeliveryDetailNotRequired.Location = New System.Drawing.Point(170, 112)
        Me.ChkDeliveryDetailNotRequired.Name = "ChkDeliveryDetailNotRequired"
        Me.ChkDeliveryDetailNotRequired.Size = New System.Drawing.Size(211, 17)
        Me.ChkDeliveryDetailNotRequired.TabIndex = 9
        Me.ChkDeliveryDetailNotRequired.Text = "Delivery Detail Not Required"
        Me.ChkDeliveryDetailNotRequired.UseVisualStyleBackColor = True
        Me.ChkDeliveryDetailNotRequired.Visible = False
        '
        'PnlCalcGrid
        '
        Me.PnlCalcGrid.Location = New System.Drawing.Point(668, 403)
        Me.PnlCalcGrid.Name = "PnlCalcGrid"
        Me.PnlCalcGrid.Size = New System.Drawing.Size(307, 158)
        Me.PnlCalcGrid.TabIndex = 5
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
        Me.TxtStructure.AgSelectedValue = ""
        Me.TxtStructure.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtStructure.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtStructure.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtStructure.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtStructure.Location = New System.Drawing.Point(609, 255)
        Me.TxtStructure.MaxLength = 20
        Me.TxtStructure.Name = "TxtStructure"
        Me.TxtStructure.Size = New System.Drawing.Size(104, 18)
        Me.TxtStructure.TabIndex = 21
        Me.TxtStructure.Tag = ""
        Me.TxtStructure.Visible = False
        '
        'Label25
        '
        Me.Label25.AutoSize = True
        Me.Label25.BackColor = System.Drawing.Color.Transparent
        Me.Label25.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(510, 256)
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
        Me.TxtSalesTaxGroupParty.Location = New System.Drawing.Point(342, 70)
        Me.TxtSalesTaxGroupParty.MaxLength = 20
        Me.TxtSalesTaxGroupParty.Name = "TxtSalesTaxGroupParty"
        Me.TxtSalesTaxGroupParty.Size = New System.Drawing.Size(163, 18)
        Me.TxtSalesTaxGroupParty.TabIndex = 6
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.BackColor = System.Drawing.Color.Transparent
        Me.Label27.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.Location = New System.Drawing.Point(234, 71)
        Me.Label27.Name = "Label27"
        Me.Label27.Size = New System.Drawing.Size(104, 16)
        Me.Label27.TabIndex = 717
        Me.Label27.Text = "Sales Tax Group"
        '
        'Label24
        '
        Me.Label24.AutoSize = True
        Me.Label24.BackColor = System.Drawing.Color.Transparent
        Me.Label24.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.Location = New System.Drawing.Point(13, 9)
        Me.Label24.Name = "Label24"
        Me.Label24.Size = New System.Drawing.Size(84, 16)
        Me.Label24.TabIndex = 715
        Me.Label24.Text = "Ship to Party"
        '
        'TxtShipToParty
        '
        Me.TxtShipToParty.AgAllowUserToEnableMasterHelp = False
        Me.TxtShipToParty.AgLastValueTag = Nothing
        Me.TxtShipToParty.AgLastValueText = ""
        Me.TxtShipToParty.AgMandatory = False
        Me.TxtShipToParty.AgMasterHelp = True
        Me.TxtShipToParty.AgNumberLeftPlaces = 8
        Me.TxtShipToParty.AgNumberNegetiveAllow = False
        Me.TxtShipToParty.AgNumberRightPlaces = 2
        Me.TxtShipToParty.AgPickFromLastValue = False
        Me.TxtShipToParty.AgRowFilter = ""
        Me.TxtShipToParty.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtShipToParty.AgSelectedValue = Nothing
        Me.TxtShipToParty.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtShipToParty.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtShipToParty.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtShipToParty.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtShipToParty.Location = New System.Drawing.Point(125, 9)
        Me.TxtShipToParty.MaxLength = 20
        Me.TxtShipToParty.Name = "TxtShipToParty"
        Me.TxtShipToParty.Size = New System.Drawing.Size(355, 18)
        Me.TxtShipToParty.TabIndex = 0
        '
        'Label22
        '
        Me.Label22.AutoSize = True
        Me.Label22.BackColor = System.Drawing.Color.Transparent
        Me.Label22.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.Location = New System.Drawing.Point(13, 29)
        Me.Label22.Name = "Label22"
        Me.Label22.Size = New System.Drawing.Size(56, 16)
        Me.Label22.TabIndex = 718
        Me.Label22.Text = "Address"
        '
        'TxtShipToPartyAdd1
        '
        Me.TxtShipToPartyAdd1.AgAllowUserToEnableMasterHelp = False
        Me.TxtShipToPartyAdd1.AgLastValueTag = Nothing
        Me.TxtShipToPartyAdd1.AgLastValueText = Nothing
        Me.TxtShipToPartyAdd1.AgMandatory = False
        Me.TxtShipToPartyAdd1.AgMasterHelp = True
        Me.TxtShipToPartyAdd1.AgNumberLeftPlaces = 8
        Me.TxtShipToPartyAdd1.AgNumberNegetiveAllow = False
        Me.TxtShipToPartyAdd1.AgNumberRightPlaces = 2
        Me.TxtShipToPartyAdd1.AgPickFromLastValue = False
        Me.TxtShipToPartyAdd1.AgRowFilter = ""
        Me.TxtShipToPartyAdd1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtShipToPartyAdd1.AgSelectedValue = Nothing
        Me.TxtShipToPartyAdd1.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtShipToPartyAdd1.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtShipToPartyAdd1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtShipToPartyAdd1.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtShipToPartyAdd1.Location = New System.Drawing.Point(125, 29)
        Me.TxtShipToPartyAdd1.MaxLength = 20
        Me.TxtShipToPartyAdd1.Name = "TxtShipToPartyAdd1"
        Me.TxtShipToPartyAdd1.Size = New System.Drawing.Size(355, 18)
        Me.TxtShipToPartyAdd1.TabIndex = 1
        '
        'TxtShipToPartyAdd2
        '
        Me.TxtShipToPartyAdd2.AgAllowUserToEnableMasterHelp = False
        Me.TxtShipToPartyAdd2.AgLastValueTag = Nothing
        Me.TxtShipToPartyAdd2.AgLastValueText = Nothing
        Me.TxtShipToPartyAdd2.AgMandatory = False
        Me.TxtShipToPartyAdd2.AgMasterHelp = True
        Me.TxtShipToPartyAdd2.AgNumberLeftPlaces = 8
        Me.TxtShipToPartyAdd2.AgNumberNegetiveAllow = False
        Me.TxtShipToPartyAdd2.AgNumberRightPlaces = 2
        Me.TxtShipToPartyAdd2.AgPickFromLastValue = False
        Me.TxtShipToPartyAdd2.AgRowFilter = ""
        Me.TxtShipToPartyAdd2.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtShipToPartyAdd2.AgSelectedValue = Nothing
        Me.TxtShipToPartyAdd2.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtShipToPartyAdd2.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtShipToPartyAdd2.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtShipToPartyAdd2.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtShipToPartyAdd2.Location = New System.Drawing.Point(125, 49)
        Me.TxtShipToPartyAdd2.MaxLength = 20
        Me.TxtShipToPartyAdd2.Name = "TxtShipToPartyAdd2"
        Me.TxtShipToPartyAdd2.Size = New System.Drawing.Size(355, 18)
        Me.TxtShipToPartyAdd2.TabIndex = 2
        '
        'Label21
        '
        Me.Label21.AutoSize = True
        Me.Label21.BackColor = System.Drawing.Color.Transparent
        Me.Label21.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.Location = New System.Drawing.Point(13, 69)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(31, 16)
        Me.Label21.TabIndex = 721
        Me.Label21.Text = "City"
        '
        'TxtShipToPartyCity
        '
        Me.TxtShipToPartyCity.AgAllowUserToEnableMasterHelp = False
        Me.TxtShipToPartyCity.AgLastValueTag = Nothing
        Me.TxtShipToPartyCity.AgLastValueText = Nothing
        Me.TxtShipToPartyCity.AgMandatory = False
        Me.TxtShipToPartyCity.AgMasterHelp = True
        Me.TxtShipToPartyCity.AgNumberLeftPlaces = 8
        Me.TxtShipToPartyCity.AgNumberNegetiveAllow = False
        Me.TxtShipToPartyCity.AgNumberRightPlaces = 2
        Me.TxtShipToPartyCity.AgPickFromLastValue = False
        Me.TxtShipToPartyCity.AgRowFilter = ""
        Me.TxtShipToPartyCity.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtShipToPartyCity.AgSelectedValue = Nothing
        Me.TxtShipToPartyCity.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtShipToPartyCity.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtShipToPartyCity.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtShipToPartyCity.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtShipToPartyCity.Location = New System.Drawing.Point(125, 69)
        Me.TxtShipToPartyCity.MaxLength = 20
        Me.TxtShipToPartyCity.Name = "TxtShipToPartyCity"
        Me.TxtShipToPartyCity.Size = New System.Drawing.Size(355, 18)
        Me.TxtShipToPartyCity.TabIndex = 3
        '
        'Label20
        '
        Me.Label20.AutoSize = True
        Me.Label20.BackColor = System.Drawing.Color.Transparent
        Me.Label20.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.Location = New System.Drawing.Point(13, 89)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(39, 16)
        Me.Label20.TabIndex = 723
        Me.Label20.Text = "State"
        '
        'TxtShipToPartyState
        '
        Me.TxtShipToPartyState.AgAllowUserToEnableMasterHelp = False
        Me.TxtShipToPartyState.AgLastValueTag = Nothing
        Me.TxtShipToPartyState.AgLastValueText = Nothing
        Me.TxtShipToPartyState.AgMandatory = False
        Me.TxtShipToPartyState.AgMasterHelp = True
        Me.TxtShipToPartyState.AgNumberLeftPlaces = 8
        Me.TxtShipToPartyState.AgNumberNegetiveAllow = False
        Me.TxtShipToPartyState.AgNumberRightPlaces = 2
        Me.TxtShipToPartyState.AgPickFromLastValue = False
        Me.TxtShipToPartyState.AgRowFilter = ""
        Me.TxtShipToPartyState.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtShipToPartyState.AgSelectedValue = Nothing
        Me.TxtShipToPartyState.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtShipToPartyState.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtShipToPartyState.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtShipToPartyState.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtShipToPartyState.Location = New System.Drawing.Point(125, 89)
        Me.TxtShipToPartyState.MaxLength = 20
        Me.TxtShipToPartyState.Name = "TxtShipToPartyState"
        Me.TxtShipToPartyState.Size = New System.Drawing.Size(134, 18)
        Me.TxtShipToPartyState.TabIndex = 4
        '
        'Label19
        '
        Me.Label19.AutoSize = True
        Me.Label19.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.Location = New System.Drawing.Point(262, 90)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(53, 16)
        Me.Label19.TabIndex = 725
        Me.Label19.Text = "Country"
        '
        'TxtShipToPartyCountry
        '
        Me.TxtShipToPartyCountry.AgAllowUserToEnableMasterHelp = False
        Me.TxtShipToPartyCountry.AgLastValueTag = Nothing
        Me.TxtShipToPartyCountry.AgLastValueText = Nothing
        Me.TxtShipToPartyCountry.AgMandatory = False
        Me.TxtShipToPartyCountry.AgMasterHelp = True
        Me.TxtShipToPartyCountry.AgNumberLeftPlaces = 8
        Me.TxtShipToPartyCountry.AgNumberNegetiveAllow = False
        Me.TxtShipToPartyCountry.AgNumberRightPlaces = 2
        Me.TxtShipToPartyCountry.AgPickFromLastValue = False
        Me.TxtShipToPartyCountry.AgRowFilter = ""
        Me.TxtShipToPartyCountry.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtShipToPartyCountry.AgSelectedValue = Nothing
        Me.TxtShipToPartyCountry.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtShipToPartyCountry.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtShipToPartyCountry.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtShipToPartyCountry.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtShipToPartyCountry.Location = New System.Drawing.Point(321, 89)
        Me.TxtShipToPartyCountry.MaxLength = 20
        Me.TxtShipToPartyCountry.Name = "TxtShipToPartyCountry"
        Me.TxtShipToPartyCountry.Size = New System.Drawing.Size(159, 18)
        Me.TxtShipToPartyCountry.TabIndex = 5
        '
        'TPShipping
        '
        Me.TPShipping.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.TPShipping.Controls.Add(Me.TxtReferencePartyDocumentDate)
        Me.TPShipping.Controls.Add(Me.Label14)
        Me.TPShipping.Controls.Add(Me.TxtReferencePartyDocumentNo)
        Me.TPShipping.Controls.Add(Me.Label34)
        Me.TPShipping.Controls.Add(Me.TxtReferenceParty)
        Me.TPShipping.Controls.Add(Me.Label35)
        Me.TPShipping.Controls.Add(Me.TxtShipToPartyCountry)
        Me.TPShipping.Controls.Add(Me.Label19)
        Me.TPShipping.Controls.Add(Me.TxtShipToPartyState)
        Me.TPShipping.Controls.Add(Me.Label20)
        Me.TPShipping.Controls.Add(Me.TxtShipToPartyCity)
        Me.TPShipping.Controls.Add(Me.Label21)
        Me.TPShipping.Controls.Add(Me.TxtShipToPartyAdd2)
        Me.TPShipping.Controls.Add(Me.TxtShipToPartyAdd1)
        Me.TPShipping.Controls.Add(Me.Label22)
        Me.TPShipping.Controls.Add(Me.TxtShipToParty)
        Me.TPShipping.Controls.Add(Me.Label24)
        Me.TPShipping.Location = New System.Drawing.Point(4, 22)
        Me.TPShipping.Name = "TPShipping"
        Me.TPShipping.Size = New System.Drawing.Size(984, 109)
        Me.TPShipping.TabIndex = 2
        Me.TPShipping.Text = "Shipping Detail"
        '
        'TxtReferencePartyDocumentDate
        '
        Me.TxtReferencePartyDocumentDate.AgAllowUserToEnableMasterHelp = False
        Me.TxtReferencePartyDocumentDate.AgLastValueTag = Nothing
        Me.TxtReferencePartyDocumentDate.AgLastValueText = Nothing
        Me.TxtReferencePartyDocumentDate.AgMandatory = False
        Me.TxtReferencePartyDocumentDate.AgMasterHelp = False
        Me.TxtReferencePartyDocumentDate.AgNumberLeftPlaces = 0
        Me.TxtReferencePartyDocumentDate.AgNumberNegetiveAllow = False
        Me.TxtReferencePartyDocumentDate.AgNumberRightPlaces = 0
        Me.TxtReferencePartyDocumentDate.AgPickFromLastValue = False
        Me.TxtReferencePartyDocumentDate.AgRowFilter = ""
        Me.TxtReferencePartyDocumentDate.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtReferencePartyDocumentDate.AgSelectedValue = Nothing
        Me.TxtReferencePartyDocumentDate.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtReferencePartyDocumentDate.AgValueType = AgControls.AgTextBox.TxtValueType.Date_Value
        Me.TxtReferencePartyDocumentDate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtReferencePartyDocumentDate.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtReferencePartyDocumentDate.Location = New System.Drawing.Point(876, 29)
        Me.TxtReferencePartyDocumentDate.MaxLength = 0
        Me.TxtReferencePartyDocumentDate.Name = "TxtReferencePartyDocumentDate"
        Me.TxtReferencePartyDocumentDate.Size = New System.Drawing.Size(95, 18)
        Me.TxtReferencePartyDocumentDate.TabIndex = 728
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.Location = New System.Drawing.Point(772, 29)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(98, 16)
        Me.Label14.TabIndex = 731
        Me.Label14.Text = "Document Date"
        '
        'TxtReferencePartyDocumentNo
        '
        Me.TxtReferencePartyDocumentNo.AgAllowUserToEnableMasterHelp = False
        Me.TxtReferencePartyDocumentNo.AgLastValueTag = Nothing
        Me.TxtReferencePartyDocumentNo.AgLastValueText = Nothing
        Me.TxtReferencePartyDocumentNo.AgMandatory = False
        Me.TxtReferencePartyDocumentNo.AgMasterHelp = True
        Me.TxtReferencePartyDocumentNo.AgNumberLeftPlaces = 0
        Me.TxtReferencePartyDocumentNo.AgNumberNegetiveAllow = False
        Me.TxtReferencePartyDocumentNo.AgNumberRightPlaces = 0
        Me.TxtReferencePartyDocumentNo.AgPickFromLastValue = False
        Me.TxtReferencePartyDocumentNo.AgRowFilter = ""
        Me.TxtReferencePartyDocumentNo.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtReferencePartyDocumentNo.AgSelectedValue = Nothing
        Me.TxtReferencePartyDocumentNo.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtReferencePartyDocumentNo.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtReferencePartyDocumentNo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtReferencePartyDocumentNo.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtReferencePartyDocumentNo.Location = New System.Drawing.Point(637, 29)
        Me.TxtReferencePartyDocumentNo.MaxLength = 20
        Me.TxtReferencePartyDocumentNo.Name = "TxtReferencePartyDocumentNo"
        Me.TxtReferencePartyDocumentNo.Size = New System.Drawing.Size(127, 18)
        Me.TxtReferencePartyDocumentNo.TabIndex = 727
        '
        'Label34
        '
        Me.Label34.AutoSize = True
        Me.Label34.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label34.Location = New System.Drawing.Point(486, 31)
        Me.Label34.Name = "Label34"
        Me.Label34.Size = New System.Drawing.Size(145, 16)
        Me.Label34.TabIndex = 730
        Me.Label34.Text = "Ref Party Document No"
        '
        'TxtReferenceParty
        '
        Me.TxtReferenceParty.AgAllowUserToEnableMasterHelp = False
        Me.TxtReferenceParty.AgLastValueTag = Nothing
        Me.TxtReferenceParty.AgLastValueText = Nothing
        Me.TxtReferenceParty.AgMandatory = False
        Me.TxtReferenceParty.AgMasterHelp = False
        Me.TxtReferenceParty.AgNumberLeftPlaces = 0
        Me.TxtReferenceParty.AgNumberNegetiveAllow = False
        Me.TxtReferenceParty.AgNumberRightPlaces = 0
        Me.TxtReferenceParty.AgPickFromLastValue = False
        Me.TxtReferenceParty.AgRowFilter = ""
        Me.TxtReferenceParty.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtReferenceParty.AgSelectedValue = Nothing
        Me.TxtReferenceParty.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtReferenceParty.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtReferenceParty.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtReferenceParty.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtReferenceParty.Location = New System.Drawing.Point(637, 9)
        Me.TxtReferenceParty.MaxLength = 0
        Me.TxtReferenceParty.Name = "TxtReferenceParty"
        Me.TxtReferenceParty.Size = New System.Drawing.Size(334, 18)
        Me.TxtReferenceParty.TabIndex = 726
        '
        'Label35
        '
        Me.Label35.AutoSize = True
        Me.Label35.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label35.Location = New System.Drawing.Point(486, 11)
        Me.Label35.Name = "Label35"
        Me.Label35.Size = New System.Drawing.Size(101, 16)
        Me.Label35.TabIndex = 729
        Me.Label35.Text = "Reference Party"
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
        Me.TxtRemarks.Location = New System.Drawing.Point(580, 32)
        Me.TxtRemarks.MaxLength = 255
        Me.TxtRemarks.Multiline = True
        Me.TxtRemarks.Name = "TxtRemarks"
        Me.TxtRemarks.Size = New System.Drawing.Size(392, 56)
        Me.TxtRemarks.TabIndex = 8
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(514, 34)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(60, 16)
        Me.Label30.TabIndex = 723
        Me.Label30.Text = "Remarks"
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
        Me.TxtAgent.Location = New System.Drawing.Point(580, 12)
        Me.TxtAgent.MaxLength = 0
        Me.TxtAgent.Name = "TxtAgent"
        Me.TxtAgent.Size = New System.Drawing.Size(392, 18)
        Me.TxtAgent.TabIndex = 7
        '
        'LblAgent
        '
        Me.LblAgent.AutoSize = True
        Me.LblAgent.BackColor = System.Drawing.Color.Transparent
        Me.LblAgent.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblAgent.Location = New System.Drawing.Point(514, 11)
        Me.LblAgent.Name = "LblAgent"
        Me.LblAgent.Size = New System.Drawing.Size(42, 16)
        Me.LblAgent.TabIndex = 729
        Me.LblAgent.Text = "Agent"
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
        Me.TxtCurrency.Location = New System.Drawing.Point(67, 403)
        Me.TxtCurrency.MaxLength = 20
        Me.TxtCurrency.Name = "TxtCurrency"
        Me.TxtCurrency.Size = New System.Drawing.Size(219, 18)
        Me.TxtCurrency.TabIndex = 2
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.BackColor = System.Drawing.Color.Transparent
        Me.Label28.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.Location = New System.Drawing.Point(5, 404)
        Me.Label28.Name = "Label28"
        Me.Label28.Size = New System.Drawing.Size(60, 16)
        Me.Label28.TabIndex = 736
        Me.Label28.Text = "Currency"
        '
        'BtnFillPartyDetail
        '
        Me.BtnFillPartyDetail.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnFillPartyDetail.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnFillPartyDetail.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnFillPartyDetail.Location = New System.Drawing.Point(479, 50)
        Me.BtnFillPartyDetail.Margin = New System.Windows.Forms.Padding(0)
        Me.BtnFillPartyDetail.Name = "BtnFillPartyDetail"
        Me.BtnFillPartyDetail.Size = New System.Drawing.Size(26, 20)
        Me.BtnFillPartyDetail.TabIndex = 5
        Me.BtnFillPartyDetail.TabStop = False
        Me.BtnFillPartyDetail.Text = "F"
        Me.BtnFillPartyDetail.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnFillPartyDetail.UseVisualStyleBackColor = True
        '
        'LinkLabel1
        '
        Me.LinkLabel1.BackColor = System.Drawing.Color.SteelBlue
        Me.LinkLabel1.DisabledLinkColor = System.Drawing.Color.White
        Me.LinkLabel1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel1.LinkColor = System.Drawing.Color.White
        Me.LinkLabel1.Location = New System.Drawing.Point(2, 185)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(229, 20)
        Me.LinkLabel1.TabIndex = 1004
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "Purchase Order For Following Items"
        Me.LinkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(326, 36)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(10, 7)
        Me.Label1.TabIndex = 1204
        Me.Label1.Text = "Ä"
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
        Me.TxtReferenceNo.Location = New System.Drawing.Point(342, 30)
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
        Me.LblReferenceNo.Location = New System.Drawing.Point(234, 30)
        Me.LblReferenceNo.Name = "LblReferenceNo"
        Me.LblReferenceNo.Size = New System.Drawing.Size(64, 16)
        Me.LblReferenceNo.TabIndex = 1203
        Me.LblReferenceNo.Text = "Order No."
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
        Me.TxtCustomFields.Location = New System.Drawing.Point(417, 589)
        Me.TxtCustomFields.MaxLength = 20
        Me.TxtCustomFields.Name = "TxtCustomFields"
        Me.TxtCustomFields.Size = New System.Drawing.Size(111, 18)
        Me.TxtCustomFields.TabIndex = 1012
        Me.TxtCustomFields.Text = "TxtCustomFields"
        Me.TxtCustomFields.Visible = False
        '
        'PnlCustomGrid
        '
        Me.PnlCustomGrid.Location = New System.Drawing.Point(290, 403)
        Me.PnlCustomGrid.Name = "PnlCustomGrid"
        Me.PnlCustomGrid.Size = New System.Drawing.Size(370, 158)
        Me.PnlCustomGrid.TabIndex = 4
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
        Me.TxtNature.Location = New System.Drawing.Point(517, 189)
        Me.TxtNature.MaxLength = 20
        Me.TxtNature.Name = "TxtNature"
        Me.TxtNature.Size = New System.Drawing.Size(81, 18)
        Me.TxtNature.TabIndex = 1207
        Me.TxtNature.Text = "TxtNature"
        Me.TxtNature.Visible = False
        '
        'BtnMailBox
        '
        Me.BtnMailBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnMailBox.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnMailBox.Location = New System.Drawing.Point(15, 18)
        Me.BtnMailBox.Name = "BtnMailBox"
        Me.BtnMailBox.Size = New System.Drawing.Size(71, 24)
        Me.BtnMailBox.TabIndex = 1015
        Me.BtnMailBox.TabStop = False
        Me.BtnMailBox.UseVisualStyleBackColor = True
        '
        'GrpMailBox
        '
        Me.GrpMailBox.BackColor = System.Drawing.Color.Transparent
        Me.GrpMailBox.Controls.Add(Me.BtnMailBox)
        Me.GrpMailBox.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GrpMailBox.Font = New System.Drawing.Font("Courier New", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GrpMailBox.ForeColor = System.Drawing.Color.Maroon
        Me.GrpMailBox.Location = New System.Drawing.Point(668, 567)
        Me.GrpMailBox.Name = "GrpMailBox"
        Me.GrpMailBox.Size = New System.Drawing.Size(99, 49)
        Me.GrpMailBox.TabIndex = 1004
        Me.GrpMailBox.TabStop = False
        Me.GrpMailBox.Tag = "UP"
        Me.GrpMailBox.Text = "Mail Box"
        Me.GrpMailBox.Visible = False
        '
        'BtnFillPendingQuotation
        '
        Me.BtnFillPendingQuotation.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnFillPendingQuotation.Font = New System.Drawing.Font("Verdana", 6.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnFillPendingQuotation.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnFillPendingQuotation.Location = New System.Drawing.Point(548, 185)
        Me.BtnFillPendingQuotation.Margin = New System.Windows.Forms.Padding(0)
        Me.BtnFillPendingQuotation.Name = "BtnFillPendingQuotation"
        Me.BtnFillPendingQuotation.Size = New System.Drawing.Size(24, 19)
        Me.BtnFillPendingQuotation.TabIndex = 2
        Me.BtnFillPendingQuotation.TabStop = False
        Me.BtnFillPendingQuotation.Text = "..."
        Me.BtnFillPendingQuotation.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnFillPendingQuotation.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.RbtOrderForIndent)
        Me.GroupBox3.Controls.Add(Me.RbtOrderForQuotation)
        Me.GroupBox3.Controls.Add(Me.RbtOrderDirect)
        Me.GroupBox3.Location = New System.Drawing.Point(239, 177)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(299, 28)
        Me.GroupBox3.TabIndex = 1
        Me.GroupBox3.TabStop = False
        '
        'RbtOrderForIndent
        '
        Me.RbtOrderForIndent.AutoSize = True
        Me.RbtOrderForIndent.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RbtOrderForIndent.Location = New System.Drawing.Point(9, 8)
        Me.RbtOrderForIndent.Name = "RbtOrderForIndent"
        Me.RbtOrderForIndent.Size = New System.Drawing.Size(94, 17)
        Me.RbtOrderForIndent.TabIndex = 2
        Me.RbtOrderForIndent.TabStop = True
        Me.RbtOrderForIndent.Text = "For Indent"
        Me.RbtOrderForIndent.UseVisualStyleBackColor = True
        '
        'RbtOrderForQuotation
        '
        Me.RbtOrderForQuotation.AutoSize = True
        Me.RbtOrderForQuotation.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RbtOrderForQuotation.Location = New System.Drawing.Point(109, 7)
        Me.RbtOrderForQuotation.Name = "RbtOrderForQuotation"
        Me.RbtOrderForQuotation.Size = New System.Drawing.Size(114, 17)
        Me.RbtOrderForQuotation.TabIndex = 0
        Me.RbtOrderForQuotation.TabStop = True
        Me.RbtOrderForQuotation.Text = "For Quotation"
        Me.RbtOrderForQuotation.UseVisualStyleBackColor = True
        '
        'RbtOrderDirect
        '
        Me.RbtOrderDirect.AutoSize = True
        Me.RbtOrderDirect.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RbtOrderDirect.Location = New System.Drawing.Point(229, 7)
        Me.RbtOrderDirect.Name = "RbtOrderDirect"
        Me.RbtOrderDirect.Size = New System.Drawing.Size(64, 17)
        Me.RbtOrderDirect.TabIndex = 1
        Me.RbtOrderDirect.TabStop = True
        Me.RbtOrderDirect.Text = "Direct"
        Me.RbtOrderDirect.UseVisualStyleBackColor = True
        '
        'BtnPrintBarcode
        '
        Me.BtnPrintBarcode.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnPrintBarcode.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnPrintBarcode.Location = New System.Drawing.Point(868, 183)
        Me.BtnPrintBarcode.Name = "BtnPrintBarcode"
        Me.BtnPrintBarcode.Size = New System.Drawing.Size(112, 22)
        Me.BtnPrintBarcode.TabIndex = 1015
        Me.BtnPrintBarcode.Text = "Print Barcode"
        Me.BtnPrintBarcode.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(5, 425)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(134, 16)
        Me.Label3.TabIndex = 1016
        Me.Label3.Text = "Terms And Conditions"
        '
        'TxtTermsAndConditions
        '
        Me.TxtTermsAndConditions.AgAllowUserToEnableMasterHelp = False
        Me.TxtTermsAndConditions.AgLastValueTag = Nothing
        Me.TxtTermsAndConditions.AgLastValueText = Nothing
        Me.TxtTermsAndConditions.AgMandatory = False
        Me.TxtTermsAndConditions.AgMasterHelp = True
        Me.TxtTermsAndConditions.AgNumberLeftPlaces = 8
        Me.TxtTermsAndConditions.AgNumberNegetiveAllow = False
        Me.TxtTermsAndConditions.AgNumberRightPlaces = 2
        Me.TxtTermsAndConditions.AgPickFromLastValue = False
        Me.TxtTermsAndConditions.AgRowFilter = ""
        Me.TxtTermsAndConditions.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtTermsAndConditions.AgSelectedValue = Nothing
        Me.TxtTermsAndConditions.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtTermsAndConditions.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtTermsAndConditions.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtTermsAndConditions.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtTermsAndConditions.Location = New System.Drawing.Point(8, 444)
        Me.TxtTermsAndConditions.MaxLength = 0
        Me.TxtTermsAndConditions.Multiline = True
        Me.TxtTermsAndConditions.Name = "TxtTermsAndConditions"
        Me.TxtTermsAndConditions.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TxtTermsAndConditions.Size = New System.Drawing.Size(278, 117)
        Me.TxtTermsAndConditions.TabIndex = 3
        '
        'FrmPurchOrder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.BackColor = System.Drawing.SystemColors.ButtonShadow
        Me.ClientSize = New System.Drawing.Size(984, 618)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.BtnPrintBarcode)
        Me.Controls.Add(Me.BtnFillPendingQuotation)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GrpMailBox)
        Me.Controls.Add(Me.PnlCustomGrid)
        Me.Controls.Add(Me.TxtCustomFields)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.PnlCalcGrid)
        Me.Controls.Add(Me.TxtTermsAndConditions)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.TxtCurrency)
        Me.Controls.Add(Me.Label28)
        Me.Controls.Add(Me.Pnl1)
        Me.EntryNCat = "SO"
        Me.LogLineTableCsv = "PurchOrderDetail_LOG"
        Me.LogTableName = "PurchOrder_Log"
        Me.MainLineTableCsv = "PurchOrderDetail"
        Me.MainTableName = "PurchOrder"
        Me.Name = "FrmPurchOrder"
        Me.Text = "Purchase Order"
        Me.Controls.SetChildIndex(Me.Pnl1, 0)
        Me.Controls.SetChildIndex(Me.Label28, 0)
        Me.Controls.SetChildIndex(Me.TxtCurrency, 0)
        Me.Controls.SetChildIndex(Me.Panel1, 0)
        Me.Controls.SetChildIndex(Me.TxtTermsAndConditions, 0)
        Me.Controls.SetChildIndex(Me.PnlCalcGrid, 0)
        Me.Controls.SetChildIndex(Me.LinkLabel1, 0)
        Me.Controls.SetChildIndex(Me.TxtCustomFields, 0)
        Me.Controls.SetChildIndex(Me.PnlCustomGrid, 0)
        Me.Controls.SetChildIndex(Me.GrpMailBox, 0)
        Me.Controls.SetChildIndex(Me.GroupBox3, 0)
        Me.Controls.SetChildIndex(Me.BtnFillPendingQuotation, 0)
        Me.Controls.SetChildIndex(Me.BtnPrintBarcode, 0)
        Me.Controls.SetChildIndex(Me.Label3, 0)
        Me.Controls.SetChildIndex(Me.Topctrl1, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.GrpUP, 0)
        Me.Controls.SetChildIndex(Me.GBoxEntryType, 0)
        Me.Controls.SetChildIndex(Me.GBoxApprove, 0)
        Me.Controls.SetChildIndex(Me.GBoxMoveToLog, 0)
        Me.Controls.SetChildIndex(Me.GroupBox2, 0)
        Me.Controls.SetChildIndex(Me.GBoxDivision, 0)
        Me.Controls.SetChildIndex(Me.TabControl1, 0)
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
        Me.Pnl1.ResumeLayout(False)
        Me.Pnl1.PerformLayout()
        Me.TPShipping.ResumeLayout(False)
        Me.TPShipping.PerformLayout()
        Me.GrpMailBox.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Protected WithEvents Label5 As System.Windows.Forms.Label
    Protected WithEvents TxtDeliveryDate As AgControls.AgTextBox
    Protected WithEvents Label11 As System.Windows.Forms.Label
    Protected WithEvents TxtVendor As AgControls.AgTextBox
    Protected WithEvents Label4 As System.Windows.Forms.Label
    Protected WithEvents Panel1 As System.Windows.Forms.Panel
    Protected WithEvents LblTotalQty As System.Windows.Forms.Label
    Protected WithEvents LblTotalQtyText As System.Windows.Forms.Label
    Protected WithEvents Pnl1 As System.Windows.Forms.Panel
    Protected WithEvents PnlCalcGrid As System.Windows.Forms.Panel
    Protected WithEvents TxtStructure As AgControls.AgTextBox
    Protected WithEvents Label25 As System.Windows.Forms.Label
    Protected WithEvents TxtSalesTaxGroupParty As AgControls.AgTextBox
    Protected WithEvents Label27 As System.Windows.Forms.Label
    Protected WithEvents TPShipping As System.Windows.Forms.TabPage
    Protected WithEvents TxtShipToPartyCountry As AgControls.AgTextBox
    Protected WithEvents Label19 As System.Windows.Forms.Label
    Protected WithEvents TxtShipToPartyState As AgControls.AgTextBox
    Protected WithEvents Label20 As System.Windows.Forms.Label
    Protected WithEvents TxtShipToPartyCity As AgControls.AgTextBox
    Protected WithEvents Label21 As System.Windows.Forms.Label
    Protected WithEvents TxtShipToPartyAdd2 As AgControls.AgTextBox
    Protected WithEvents TxtShipToPartyAdd1 As AgControls.AgTextBox
    Protected WithEvents Label22 As System.Windows.Forms.Label
    Protected WithEvents TxtShipToParty As AgControls.AgTextBox
    Protected WithEvents LblTotalAmount As System.Windows.Forms.Label
    Protected WithEvents LblTotalAmountText As System.Windows.Forms.Label
    Protected WithEvents TxtRemarks As AgControls.AgTextBox
    Protected WithEvents Label30 As System.Windows.Forms.Label
    Protected WithEvents LblTotalMeasure As System.Windows.Forms.Label
    Protected WithEvents LblTotalMeasureText As System.Windows.Forms.Label
    Protected WithEvents TxtAgent As AgControls.AgTextBox
    Protected WithEvents LblAgent As System.Windows.Forms.Label
    Protected WithEvents Label24 As System.Windows.Forms.Label
    Protected WithEvents LblTotalDeliveryMeasure As System.Windows.Forms.Label
    Protected WithEvents LblTotalDeliveryMeasureText As System.Windows.Forms.Label
    Protected WithEvents TxtReferencePartyDocumentDate As AgControls.AgTextBox
    Protected WithEvents Label14 As System.Windows.Forms.Label
    Protected WithEvents TxtReferencePartyDocumentNo As AgControls.AgTextBox
    Protected WithEvents Label34 As System.Windows.Forms.Label
    Protected WithEvents TxtReferenceParty As AgControls.AgTextBox
    Protected WithEvents Label35 As System.Windows.Forms.Label
    Protected WithEvents TxtCurrency As AgControls.AgTextBox
    Protected WithEvents Label28 As System.Windows.Forms.Label
    Protected WithEvents BtnFillPartyDetail As System.Windows.Forms.Button
    Protected WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    Protected WithEvents Label1 As System.Windows.Forms.Label
    Protected WithEvents TxtReferenceNo As AgControls.AgTextBox
    Protected WithEvents LblReferenceNo As System.Windows.Forms.Label
    Protected WithEvents TxtCustomFields As AgControls.AgTextBox
    Protected WithEvents PnlCustomGrid As System.Windows.Forms.Panel
    Protected WithEvents TxtNature As AgControls.AgTextBox
    Protected WithEvents BtnMailBox As System.Windows.Forms.Button
    Protected WithEvents GrpMailBox As System.Windows.Forms.GroupBox
    Protected WithEvents ChkDeliveryDetailNotRequired As System.Windows.Forms.CheckBox
    Protected WithEvents BtnFillPendingQuotation As System.Windows.Forms.Button
    Protected WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Protected WithEvents RbtOrderForQuotation As System.Windows.Forms.RadioButton
    Protected WithEvents RbtOrderDirect As System.Windows.Forms.RadioButton
    Protected WithEvents RbtOrderForIndent As System.Windows.Forms.RadioButton
#End Region


    Private Sub FPostInBuyerSku(ByVal SearchCode As String, ByVal Conn As SqliteConnection, ByVal Cmd As SqliteCommand)
        Dim I As Integer
        Dim mSr As Integer

        '------------------------------------------------------------------------
        'Updating Buyer Wise Item SKU and UPC (Universal Product Code)
        '-------------------------------------------------------------------------
        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1Item, I).Value <> "" And Dgl1.Item(Col1PartySKU, I).Value <> "" Then
                If Not AgL.StrCmp(Dgl1.Item(Col1PartySKU, I).Value, Dgl1.Item(Col1XPartySKU, I).Value) Then
                    If Dgl1.Item(Col1XPartySKU, I).Value = "" Then
                        mQry = "Select IfNull(Max(Sr),0)+1 From ItemBuyer  Where Code = '" & Dgl1.Item(Col1Item, I).Tag & "'"
                        mSr = AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar
                        mQry = "INSERT INTO ItemBuyer (Code, Sr, Buyer, BuyerSku) " &
                               " VALUES (" & AgL.Chk_Text(Dgl1.Item(Col1Item, I).Tag) & ", " & mSr & ", " &
                               " " & AgL.Chk_Text(TxtVendor.Tag) & ", " &
                               " " & AgL.Chk_Text(Dgl1.Item(Col1PartySKU, I).Value) & ") "
                    Else
                        mQry = "UPDATE ItemBuyer " &
                               " SET BuyerSku = " & AgL.Chk_Text(Dgl1.Item(Col1PartySKU, I).Value) & " " &
                               " Where Code = '" & SearchCode & "' "
                    End If
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                End If
            End If
        Next
        '-------------------------------------------------------------------------
    End Sub

    Private Sub FrmPurchOrder_BaseEvent_ApproveDeletion_InTrans(ByVal SearchCode As String, ByVal Conn As SQLiteConnection, ByVal Cmd As SQLiteCommand) Handles Me.BaseEvent_ApproveDeletion_InTrans
        mQry = " Delete From StockVirtual Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    End Sub

    Private Sub FrmQuality1_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "PurchOrder"
        LogTableName = "PurchOrder_Log"
        MainLineTableCsv = "PurchOrderDetail,PurchOrderDeliveryDetail"
        LogLineTableCsv = "PurchOrderDetail_LOG,PurchOrderDeliveryDetail_Log"

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

        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) &
                        " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "H.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        If IsApplyVTypePermission Then
            mCondStr = mCondStr & " And H.V_Type In (Select V_Type From User_VType_Permission VP Where VP.UserName = '" & AgL.PubUserName & "' And VP.Div_Code = '" & AgL.PubDivCode & "' And VP.Site_Code = '" & AgL.PubSiteCode & "') "
        End If

        mQry = "Select DocID As SearchCode " &
                " From PurchOrder H " &
                " Left Join Voucher_Type Vt On H.V_Type = Vt.V_Type  " &
                " Where IfNull(IsDeleted,0)=0  " & mCondStr & "  Order By V_Date Desc "
        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub FrmPurchOrder_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mCondStr$ = ""

        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) &
                       " And IfNull(H.IsDeleted,0)=0 And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "H.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        If IsApplyVTypePermission Then
            mCondStr = mCondStr & " And H.V_Type In (Select V_Type From User_VType_Permission VP Where VP.UserName = '" & AgL.PubUserName & "' And VP.Div_Code = '" & AgL.PubDivCode & "' And VP.Site_Code = '" & AgL.PubSiteCode & "') "
        End If

        AgL.PubFindQry = " SELECT H.DocId AS SearchCode, H.V_Date AS [Purchase_Order_Date], H.ReferenceNo AS [Purch_Order_No], " &
                    " H.VendorName AS [Vendor_Name], H.VendorAdd1 AS [Vendor_Add1], " &
                    " H.VendorAdd2 AS [Vendor_Add2], H.VendorCityName AS [Vendor_City_Name],  " &
                    " H.VendorState AS [Vendor_State], H.VendorCountry AS [Vendor_Country], " &
                    " H.ShipToPartyName AS [Ship_TO_Party_Name], H.ShipToPartyAdd1 AS [Ship_TO_Party_Add1], " &
                    " H.ShipToPartyAdd2 AS [Ship_To_Party_Add2],  " &
                    " H.ShipToPartyCityName AS [Ship_To_Party_City_Name], H.ShipToPartyState AS [Ship_TO_Party_State], " &
                    " H.ShipToPartyCountry AS [Ship_TO_Party_Country], H.Currency, " &
                    " H.VendorDeliveryDate AS [Vendor_Delivery_Date], " &
                    " H.Remarks, L.TotalQty AS [Total_Qty], L.TotalDeliveryMeasure AS [Total_Delivery_Measure], H.TotalAmount AS [Total_Amount],  " &
                    " H.EntryBy AS [Entry_By], H.EntryDate AS [Entry_Date] " &
                    " FROM PurchOrder  H " &
                    " LEFT JOIN (Select DocId, Sum(Qty) As TotalQty, Sum(TotalDeliveryMeasure) As TotalDeliveryMeasure, " &
                    "               Sum(Amount) As TotalAmount " &
                    "               From PurchOrderDetail " &
                    "               Group By DocId ) As L On H.DocId = L.DocId " &
                    " LEFT JOIN Division D ON D.Div_Code =H.Div_Code   " &
                    " LEFT JOIN SiteMast SM ON SM.Code=H.Site_Code  " &
                    " LEFT JOIN voucher_type Vt ON H.V_Type = vt.V_Type " &
                    " LEFT JOIN SubGroup SGA ON SGA.SubCode  = H.Agent  " &
                    " LEFT JOIN SeaPort DP ON H.DestinationPort = DP.Code  " &
                    " Where 1=1 " & mCondStr

        AgL.PubFindQryOrdBy = "[Order Date]"
    End Sub

    Private Sub FrmPurchOrder_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl1, Col1ItemCode, 100, 0, Col1ItemCode, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_ItemCode")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_ItemCode")), Boolean))
            .AddAgTextColumn(Dgl1, Col1Item, 200, 0, Col1Item, True, Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_ItemName")), Boolean))
            .AddAgTextColumn(Dgl1, Col1Dimension1, 100, 0, ClsMain.FGetDimension1Caption(), CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Dimension1")), Boolean), False)
            .AddAgTextColumn(Dgl1, Col1Dimension2, 100, 0, ClsMain.FGetDimension2Caption(), CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Dimension2")), Boolean), False)
            .AddAgTextColumn(Dgl1, Col1PurchQuotation, 80, 0, Col1PurchQuotation, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_PurchQuotation")), Boolean), True, False)
            .AddAgTextColumn(Dgl1, Col1PurchQuotationSr, 60, 0, Col1PurchQuotationSr, False, True, False)
            .AddAgTextColumn(Dgl1, Col1PurchIndent, 80, 0, Col1PurchIndent, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_PurchIndent")), Boolean), True, False)
            .AddAgTextColumn(Dgl1, Col1PurchIndentSr, 60, 0, Col1PurchIndentSr, False, True, False)
            .AddAgTextColumn(Dgl1, Col1MaterialPlan, 80, 0, Col1MaterialPlan, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_ProdOrder")), Boolean), True, False)
            .AddAgTextColumn(Dgl1, Col1MaterialPlanSr, 60, 0, Col1MaterialPlanSr, False, True, False)
            .AddAgTextColumn(Dgl1, Col1Specification, 100, 255, Col1Specification, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Specification")), Boolean), False, False)
            .AddAgTextColumn(Dgl1, Col1PartySKU, 110, 50, Col1PartySKU, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_PartySKU")), Boolean), False, False)
            .AddAgTextColumn(Dgl1, Col1XPartySKU, 270, 50, Col1XPartySKU, False, False, False)
            .AddAgTextColumn(Dgl1, Col1BillingType, 70, 50, Col1BillingType, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_BillingType")), Boolean), False, False)
            .AddAgTextColumn(Dgl1, Col1RateType, 100, 50, Col1RateType, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_RateType")), Boolean), False, False)
            .AddAgNumberColumn(Dgl1, Col1Qty, 80, 8, 3, False, Col1Qty, True, False, True)
            .AddAgNumberColumn(Dgl1, Col1FreeQty, 70, 8, 0, False, Col1FreeQty, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_FreeQty")), Boolean), False, True)
            .AddAgTextColumn(Dgl1, Col1Unit, 50, 0, Col1Unit, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Unit")), Boolean), True)
            .AddAgTextColumn(Dgl1, Col1QtyDecimalPlaces, 50, 0, Col1QtyDecimalPlaces, False, True, False)
            .AddAgNumberColumn(Dgl1, Col1PcsPerMeasure, 70, 8, 4, False, Col1PcsPerMeasure, False, True, True)
            .AddAgNumberColumn(Dgl1, Col1MeasurePerPcs, 80, 8, 4, False, Col1MeasurePerPcs, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_MeasurePerPcs")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_MeasurePerPcs")), Boolean), True)
            .AddAgNumberColumn(Dgl1, Col1TotalMeasure, 70, 8, 4, False, Col1TotalMeasure, False, True, True)
            .AddAgNumberColumn(Dgl1, Col1TotalFreeMeasure, 70, 8, 4, False, Col1TotalFreeMeasure, False, True, True)
            .AddAgTextColumn(Dgl1, Col1MeasureDecimalPlaces, 50, 0, Col1MeasureDecimalPlaces, False, True, False)
            .AddAgTextColumn(Dgl1, Col1MeasureUnit, 50, 0, Col1MeasureUnit, False, True)
            .AddAgTextColumn(Dgl1, Col1DeliveryMeasure, 70, 50, Col1DeliveryMeasure, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Measure")), Boolean), False, False)
            .AddAgNumberColumn(Dgl1, Col1DeliveryMeasureMultiplier, 100, 8, 4, False, Col1DeliveryMeasureMultiplier, False, True, True)
            .AddAgNumberColumn(Dgl1, Col1DeliveryMeasurePerPcs, 110, 8, 4, False, Col1DeliveryMeasurePerPcs, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_MeasurePerPcs")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_MeasurePerPcs")), Boolean), True)
            .AddAgNumberColumn(Dgl1, Col1TotalDeliveryMeasure, 85, 8, 4, False, Col1TotalDeliveryMeasure, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Measure")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_Measure")), Boolean), True)
            .AddAgNumberColumn(Dgl1, Col1TotalFreeDeliveryMeasure, 110, 8, 4, False, Col1TotalFreeDeliveryMeasure, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_FreeQty")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_Measure")), Boolean), True)
            .AddAgTextColumn(Dgl1, Col1DeliveryMeasureDecimalPlaces, 50, 0, Col1DeliveryMeasureDecimalPlaces, False, True, False)
            .AddAgNumberColumn(Dgl1, Col1MRP, 70, 8, 2, False, Col1MRP, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_MRP")), Boolean), False, True)
            .AddAgTextColumn(Dgl1, Col1Deal, 70, 255, Col1Deal, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Deal")), Boolean), False)
            .AddAgNumberColumn(Dgl1, Col1Rate, 60, 8, 3, False, Col1Rate, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Rate")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_Rate")), Boolean), True)
            .AddAgNumberColumn(Dgl1, Col1Amount, 70, 8, 2, False, Col1Amount, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Amount")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_Amount")), Boolean), True)
            .AddAgButtonColumn(Dgl1, Col1BtnDeliveryDetail, 60, Col1BtnDeliveryDetail, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_DeliveryDetail")), Boolean), False)
            .AddAgTextColumn(Dgl1, Col1SalesTaxGroup, 70, 0, Col1SalesTaxGroup, True, False, False)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.AgSkipReadOnlyColumns = True
        Dgl1.ColumnHeadersHeight = 35
        Dgl1.AgAllowFind = False

        Dgl1.AllowUserToOrderColumns = True

        AgCalcGrid1.AgLineGridPostingGroupSalesTaxProd = Dgl1.Columns(Col1SalesTaxGroup).Index
        AgCalcGrid1.Ini_Grid(LblV_Type.Tag, TxtV_Date.Text)

        AgCalcGrid1.AgLineGrid = Dgl1
        AgCalcGrid1.AgLineGridMandatoryColumn = Dgl1.Columns(Col1Item).Index
        AgCalcGrid1.AgLineGridGrossColumn = Dgl1.Columns(Col1Amount).Index


        AgCustomGrid1.Ini_Grid(mSearchCode)
        AgCustomGrid1.SplitGrid = False

        AgCalcGrid1.Name = "AgCalcGrid1"
        AgCustomGrid1.Name = "AgCustomGrid1"

        AgCL.GridSetiingWriteXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1)
        AgCL.GridSetiingWriteXml(Me.Text & AgCalcGrid1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, AgCalcGrid1)
        AgCL.GridSetiingWriteXml(Me.Text & AgCustomGrid1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, AgCustomGrid1)
    End Sub

    Private Sub FrmPurchOrder_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As SQLiteConnection, ByVal Cmd As SQLiteCommand) Handles Me.BaseEvent_Save_InTrans
        Dim I As Integer, mSr As Integer
        Dim bSelectionQry$ = "", bSelecttionLineQry$ = ""

        If BtnFillPartyDetail.Tag Is Nothing Then BtnFillPartyDetail.Tag = New FrmPurchPartyDetail

        mQry = "UPDATE PurchOrder " &
                "   SET " &
                "   ReferenceNo = " & AgL.Chk_Text(TxtReferenceNo.Text) & ", " &
                "   Vendor = " & AgL.Chk_Text(TxtVendor.Tag) & ", " &
                "   VendorName = " & AgL.Chk_Text(BtnFillPartyDetail.Tag.TxtVendorName.Text) & ", " &
                "   VendorAdd1 = " & AgL.Chk_Text(BtnFillPartyDetail.Tag.TxtVendorAdd1.Text) & ", " &
                "   VendorAdd2 = " & AgL.Chk_Text(BtnFillPartyDetail.Tag.TxtVendorAdd2.Text) & ", " &
                "   VendorCity = " & AgL.Chk_Text(BtnFillPartyDetail.Tag.TxtVendorCity.Tag) & ", " &
                "   VendorCityName = " & AgL.Chk_Text(BtnFillPartyDetail.Tag.TxtVendorCity.Text) & ", " &
                "   VendorMobile = " & AgL.Chk_Text(BtnFillPartyDetail.Tag.TxtVendorMobile.Text) & ", " &
                "	Currency = " & AgL.Chk_Text(TxtCurrency.Tag) & ", " &
                "	ShipToPartyName = " & AgL.Chk_Text(TxtShipToParty.Text) & ", " &
                "	ShipToPartyAdd1 = " & AgL.Chk_Text(TxtShipToPartyAdd1.Text) & ", " &
                "	ShipToPartyAdd2 = " & AgL.Chk_Text(TxtShipToPartyAdd2.Text) & ", " &
                "	ShipToPartyCity = " & AgL.Chk_Text(TxtShipToPartyCity.Tag) & ", " &
                "	ShipToPartyCityName = " & AgL.Chk_Text(TxtShipToPartyCity.Text) & ", " &
                "	ShipToPartyState = " & AgL.Chk_Text(TxtShipToPartyState.Text) & ", " &
                "	ShipToPartyCountry = " & AgL.Chk_Text(TxtShipToPartyCountry.Text) & ", " &
                "	SalesTaxGroupParty = " & AgL.Chk_Text(TxtSalesTaxGroupParty.Tag) & ", " &
                "	VendorDeliveryDate =" & AgL.Chk_Text(TxtDeliveryDate.Text) & ", " &
                "	TermsAndConditions = " & AgL.Chk_Text(TxtTermsAndConditions.Text) & ", " &
                "	Remarks = " & AgL.Chk_Text(TxtRemarks.Text) & ", " &
                "	Structure = " & AgL.Chk_Text(TxtStructure.Tag) & ", " &
                "   CustomFields = " & AgL.Chk_Text(TxtCustomFields.Tag) & ", " &
                "   ReferenceParty = " & AgL.Chk_Text(TxtReferenceParty.Tag) & ", " &
                "   Agent = " & AgL.Chk_Text(TxtAgent.Tag) & ", " &
                "   " & AgCalcGrid1.FFooterTableUpdateStr() & " " &
                "   " & AgCustomGrid1.FFooterTableUpdateStr() & " " &
                "   Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Select Max(Sr) From PurchOrderDetail  Where DocID = '" & mSearchCode & "'"
        mSr = AgL.VNull(AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar)

        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1Item, I).Value <> "" Then
                If Dgl1.Item(ColSNo, I).Tag Is Nothing And Dgl1.Rows(I).Visible = True Then
                    mSr += 1
                    If bSelectionQry <> "" Then bSelectionQry += " UNION ALL "
                    bSelectionQry += " Select " & AgL.Chk_Text(mSearchCode) & ", " & mSr & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1Item, I).Tag) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1Dimension1, I).Tag) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1Dimension2, I).Tag) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1PurchQuotation, I).Tag) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1PurchQuotationSr, I).Value) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1PurchIndent, I).Tag) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1PurchIndentSr, I).Value) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1MaterialPlan, I).Tag) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1MaterialPlanSr, I).Value) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1Specification, I).Value) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1PartySKU, I).Value) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1BillingType, I).Value) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1RateType, I).Value) & ", " &
                            " " & Val(Dgl1.Item(Col1Qty, I).Value) & ", " &
                            " " & Val(Dgl1.Item(Col1FreeQty, I).Value) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1Unit, I).Value) & ", " &
                            " " & Val(Dgl1.Item(Col1MRP, I).Value) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1Deal, I).Value) & ", " &
                            " " & Val(Dgl1.Item(Col1Rate, I).Value) & ", " &
                            " " & Val(Dgl1.Item(Col1Amount, I).Value) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1SalesTaxGroup, I).Tag) & ", " &
                            " " & Val(Dgl1.Item(Col1PcsPerMeasure, I).Value) & ", " &
                            " " & Val(Dgl1.Item(Col1MeasurePerPcs, I).Value) & ", " &
                            " " & Val(Dgl1.Item(Col1TotalMeasure, I).Value) & ", " &
                            " " & Val(Dgl1.Item(Col1TotalFreeMeasure, I).Value) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1MeasureUnit, I).Value) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1DeliveryMeasure, I).Value) & ", " &
                            " " & Val(Dgl1.Item(Col1DeliveryMeasureMultiplier, I).Value) & ", " &
                            " " & Val(Dgl1.Item(Col1DeliveryMeasurePerPcs, I).Value) & ", " &
                            " " & Val(Dgl1.Item(Col1TotalDeliveryMeasure, I).Value) & ", " &
                            " " & Val(Dgl1.Item(Col1TotalFreeDeliveryMeasure, I).Value) & ", " &
                            " " & AgL.Chk_Text(mSearchCode) & ", " &
                            " " & mSr & ", " &
                            " " & AgCalcGrid1.FLineTableFieldValuesStr(I) & " "
                    Call FGetLineQry(bSelecttionLineQry, Conn, Cmd, I, mSearchCode, mSr)
                Else
                    If Dgl1.Rows(I).Visible = True Then
                        If Dgl1.Rows(I).DefaultCellStyle.BackColor <> AgTemplate.ClsMain.Colours.GridRow_Locked Then
                            mQry = " UPDATE PurchOrderDetail " &
                                    " SET " &
                                    " Item = " & AgL.Chk_Text(Dgl1.Item(Col1Item, I).Tag) & ", " &
                                    " Dimension1 = " & AgL.Chk_Text(Dgl1.Item(Col1Dimension1, I).Tag) & ", " &
                                    " Dimension2 = " & AgL.Chk_Text(Dgl1.Item(Col1Dimension2, I).Tag) & ", " &
                                    " PurchQuotation = " & AgL.Chk_Text(Dgl1.Item(Col1PurchQuotation, I).Tag) & ", " &
                                    " PurchQuotationSr = " & AgL.Chk_Text(Dgl1.Item(Col1PurchQuotationSr, I).Value) & ", " &
                                    " PurchIndent = " & AgL.Chk_Text(Dgl1.Item(Col1PurchIndent, I).Tag) & ", " &
                                    " PurchIndentSr = " & AgL.Chk_Text(Dgl1.Item(Col1PurchIndentSr, I).Value) & ", " &
                                    " MaterialPlan = " & AgL.Chk_Text(Dgl1.Item(Col1MaterialPlan, I).Tag) & ", " &
                                    " MaterialPlanSr = " & AgL.Chk_Text(Dgl1.Item(Col1MaterialPlanSr, I).Value) & ", " &
                                    " Specification = " & AgL.Chk_Text(Dgl1.Item(Col1Specification, I).Value) & ", " &
                                    " PartySKU = " & AgL.Chk_Text(Dgl1.Item(Col1PartySKU, I).Value) & ", " &
                                    " BillingType = " & AgL.Chk_Text(Dgl1.Item(Col1BillingType, I).Value) & ", " &
                                    " RateType = " & AgL.Chk_Text(Dgl1.Item(Col1RateType, I).Value) & ", " &
                                    " Qty = " & Val(Dgl1.Item(Col1Qty, I).Value) & ", " &
                                    " FreeQty = " & Val(Dgl1.Item(Col1FreeQty, I).Value) & ", " &
                                    " Unit = " & AgL.Chk_Text(Dgl1.Item(Col1Unit, I).Value) & ", " &
                                    " MRP = " & Val(Dgl1.Item(Col1MRP, I).Value) & ", " &
                                    " Deal = " & AgL.Chk_Text(Dgl1.Item(Col1Deal, I).Value) & ", " &
                                    " Rate = " & Val(Dgl1.Item(Col1Rate, I).Value) & ", " &
                                    " Amount = " & Val(Dgl1.Item(Col1Amount, I).Value) & ", " &
                                    " SalesTaxGroupItem = " & AgL.Chk_Text(Dgl1.Item(Col1SalesTaxGroup, I).Tag) & ", " &
                                    " PcsPerMeasure = " & Val(Dgl1.Item(Col1PcsPerMeasure, I).Value) & ", " &
                                    " MeasurePerPcs = " & Val(Dgl1.Item(Col1MeasurePerPcs, I).Value) & ", " &
                                    " TotalMeasure = " & Val(Dgl1.Item(Col1TotalMeasure, I).Value) & ", " &
                                    " TotalFreeMeasure = " & Val(Dgl1.Item(Col1TotalFreeMeasure, I).Value) & ", " &
                                    " MeasureUnit = " & AgL.Chk_Text(Dgl1.Item(Col1MeasureUnit, I).Value) & ", " &
                                    " DeliveryMeasure = " & AgL.Chk_Text(Dgl1.Item(Col1DeliveryMeasure, I).Value) & ", " &
                                    " DeliveryMeasureMultiplier = " & Val(Dgl1.Item(Col1DeliveryMeasureMultiplier, I).Value) & ", " &
                                    " DeliveryMeasurePerPcs = " & Val(Dgl1.Item(Col1DeliveryMeasurePerPcs, I).Value) & ", " &
                                    " TotalDeliveryMeasure = " & Val(Dgl1.Item(Col1TotalDeliveryMeasure, I).Value) & ", " &
                                    " TotalFreeDeliveryMeasure = " & Val(Dgl1.Item(Col1TotalFreeDeliveryMeasure, I).Value) & ", " &
                                    " PurchOrder = '" & mSearchCode & "', " &
                                    " PurchOrderSr = " & Dgl1.Item(ColSNo, I).Tag & ", " &
                                    " " & AgCalcGrid1.FLineTableUpdateStr(I) & " " &
                                    " Where DocId = '" & mSearchCode & "' " &
                                    " And Sr = " & Dgl1.Item(ColSNo, I).Tag & " "
                            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                            Call FGetLineQry(bSelecttionLineQry, Conn, Cmd, I, mSearchCode, Dgl1.Item(ColSNo, I).Tag)
                        End If
                    Else
                        mQry = " Delete From PurchOrderDeliveryDetail Where DocId = '" & mSearchCode & "' And TSr = " & Val(Dgl1.Item(ColSNo, I).Tag) & "  "
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                        mQry = " Delete From PurchOrderDetail Where DocId = '" & mSearchCode & "' And Sr = " & Val(Dgl1.Item(ColSNo, I).Tag) & "  "
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                    End If
                End If
            End If
        Next

        If bSelectionQry <> "" Then
            mQry = "INSERT INTO PurchOrderDetail (DocId, Sr, Item, Dimension1, Dimension2, PurchQuotation, PurchQuotationSr, " &
                    " PurchIndent, PurchIndentSr, MaterialPlan, MaterialPlanSr, Specification, PartySKU, BillingType, RateType, " &
                    " Qty, FreeQty, Unit, MRP, Deal, Rate, Amount, SalesTaxGroupItem, PcsPerMeasure, MeasurePerPcs, TotalMeasure, TotalFreeMeasure, " &
                    " MeasureUnit, DeliveryMeasure, DeliveryMeasureMultiplier, " &
                    " DeliveryMeasurePerPcs, TotalDeliveryMeasure, TotalFreeDeliveryMeasure, " &
                    " PurchOrder, PurchOrderSr, " & AgCalcGrid1.FLineTableFieldNameStr() & ") " & bSelectionQry
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If

        If bSelecttionLineQry <> "" Then
            mQry = " INSERT INTO PurchOrderDeliveryDetail(DocId, TSr, Sr, Item,  " &
                    " Qty, Unit, MeasurePerPcs, MeasureUnit, TotalMeasure, DeliveryDate, DeliveryInstructions, PurchOrder, PurchOrderSr, PurchOrderDelSchSr) " & bSelecttionLineQry
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If

        Call FPostInBuyerSku(mSearchCode, Conn, Cmd)

        Call FPostInStockVirtual(Conn, Cmd)

        If AgL.VNull(DtPurhcaseEnviro.Rows(0)("GenerateItem_UidFromPO")) <> 0 Then
            Call FGenerateItemUid(Conn, Cmd)
        End If

        If AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Or AgL.StrCmp(AgL.PubUserName, "Sa") Then
            AgCL.GridSetiingWriteXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1)
            AgCL.GridSetiingWriteXml(Me.Text & AgCalcGrid1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, AgCalcGrid1)
            AgCL.GridSetiingWriteXml(Me.Text & AgCustomGrid1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, AgCustomGrid1)
        End If
    End Sub

    Private Sub FPostInStockVirtual(ByVal Conn As SQLiteConnection, ByVal Cmd As SQLiteCommand)
        Dim I As Integer = 0
        Dim mSr As Integer = 0
        Dim StockVirtual As AgTemplate.ClsMain.StructStock = Nothing

        mQry = "Delete From StockVirtual Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " INSERT INTO StockVirtual(DocID, Sr, V_Type, V_Prefix, V_Date, V_No, RecId, Div_Code, Site_Code, SubCode, " &
                  " CostCenter, Item, Qty_Rec, Unit, MeasurePerPcs, Measure_Rec, MeasureUnit, Rate, Amount, " &
                  " ReferenceDocID, ReferenceDocIDSr) " &
                  " SELECT H.DocID, L.Sr, H.V_Type, H.V_Prefix, H.V_Date, H.V_No, H.ReferenceNo, H.Div_Code, " &
                  " H.Site_Code, H.Vendor, Null, L.Item, L.Qty, L.Unit, L.MeasurePerPcs, L.TotalMeasure, L.MeasureUnit, " &
                  " l.Rate, L.Amount, L.PurchIndent, L.PurchIndentSr " &
                  " FROM PurchOrder H  " &
                  " LEFT JOIN PurchOrderDetail L ON H.DocID = L.DocId " &
                  " Where H.DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    End Sub

    Private Sub FrmPurchOrder_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim I As Integer

        Dim IsSameUnit As Boolean = True
        Dim IsSameMeasureUnit As Boolean = True
        Dim IsSameDeliveryMeasureUnit As Boolean = True

        Dim intQtyDecimalPlaces As Integer = 0
        Dim intMeasureDecimalPlaces As Integer = 0
        Dim intDeliveryMeasureDecimalPlaces As Integer = 0

        Dim DsTemp As DataSet


        LblTotalQty.Text = 0
        LblTotalMeasure.Text = 0
        LblTotalDeliveryMeasure.Text = 0
        LblTotalAmount.Text = 0

        mIsEntryLocked = False

        mQry = "Select H.*, Sg.DispName As AgentName, " &
                " C1.Description As CurrencyDesc, Sg.Nature, Sg2.DispName As ReferencePartyName " &
                " From PurchOrder H " &
                " Left Join City C On H.VendorCity = C.CityCode " &
                " LEFT JOIN SubGroup Sg On H.Agent = Sg.SubCode " &
                " LEFT JOIN SubGroup Sg2 On H.ReferenceParty = Sg2.SubCode " &
                " LEFT JOIN Currency C1 On H.Currency = C1.Code " &
                " Where H.DocID='" & SearchCode & "'"
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

                If AgL.XNull(.Rows(0)("CustomFields")) <> "" Then
                    TxtCustomFields.Tag = AgL.XNull(.Rows(0)("CustomFields"))
                End If
                AgCustomGrid1.FrmType = Me.FrmType
                AgCustomGrid1.AgCustom = TxtCustomFields.Tag

                IniGrid()

                TxtVendor.Tag = AgL.XNull(.Rows(0)("Vendor"))
                TxtVendor.Text = AgL.XNull(.Rows(0)("VendorName"))

                TxtReferenceNo.Text = AgL.XNull(.Rows(0)("ReferenceNo"))

                TxtNature.Text = AgL.XNull(.Rows(0)("Nature"))

                TxtShipToParty.Text = AgL.XNull(.Rows(0)("ShipToPartyName"))
                TxtShipToPartyAdd1.Text = AgL.XNull(.Rows(0)("ShipToPartyAdd1"))
                TxtShipToPartyAdd2.Text = AgL.XNull(.Rows(0)("ShipToPartyAdd2"))
                TxtShipToPartyCity.Tag = AgL.XNull(.Rows(0)("ShipToPartyCity"))
                TxtShipToPartyState.Text = AgL.XNull(.Rows(0)("ShipToPartyState"))
                TxtShipToPartyCountry.Text = AgL.XNull(.Rows(0)("ShipToPartyCountry"))

                TxtCurrency.Tag = AgL.XNull(.Rows(0)("Currency"))
                TxtCurrency.Text = AgL.XNull(.Rows(0)("CurrencyDesc"))

                TxtSalesTaxGroupParty.Tag = AgL.XNull(.Rows(0)("SalesTaxGroupParty"))
                TxtSalesTaxGroupParty.Text = AgL.XNull(.Rows(0)("SalesTaxGroupParty"))

                TxtDeliveryDate.Text = AgL.XNull(.Rows(0)("VendorDeliveryDate"))
                TxtTermsAndConditions.Text = AgL.XNull(.Rows(0)("TermsAndConditions"))
                TxtRemarks.Text = AgL.XNull(.Rows(0)("Remarks"))

                TxtAgent.Tag = AgL.XNull(.Rows(0)("Agent"))
                TxtAgent.Text = AgL.XNull(.Rows(0)("AgentName"))

                TxtReferenceParty.Tag = AgL.XNull(.Rows(0)("ReferenceParty"))
                TxtReferenceParty.Text = AgL.XNull(.Rows(0)("ReferencePartyName"))

                If TxtDeliveryDate.Text = "" Then
                    ChkDeliveryDetailNotRequired.Checked = True
                Else
                    ChkDeliveryDetailNotRequired.Checked = False
                End If

                Dim FrmObj As New FrmPurchPartyDetail
                FrmObj.TxtVendorMobile.Text = AgL.XNull(.Rows(0)("VendorMobile"))
                FrmObj.TxtVendorName.Text = AgL.XNull(.Rows(0)("VendorName"))
                FrmObj.TxtVendorAdd1.Text = AgL.XNull(.Rows(0)("VendorAdd1"))
                FrmObj.TxtVendorAdd2.Text = AgL.XNull(.Rows(0)("VendorAdd2"))
                FrmObj.TxtVendorCity.Tag = AgL.XNull(.Rows(0)("VendorCity"))
                FrmObj.TxtVendorCity.Text = AgL.XNull(.Rows(0)("VendorCityName"))

                BtnFillPartyDetail.Tag = FrmObj

                AgCalcGrid1.FMoveRecFooterTable(DsTemp.Tables(0), EntryNCat, TxtV_Date.Text)

                AgCustomGrid1.FMoveRecFooterTable(DsTemp.Tables(0))

                ClsMain.FFillPurchaseEnviro(TxtV_Type.Tag)
                If AgL.VNull(DtPurhcaseEnviro.Rows(0)("GenerateItem_UidFromPO")) <> 0 Then BtnPrintBarcode.Visible = True Else BtnPrintBarcode.Visible = False

                Dim strQryPurchChallan$ = "SELECT L.PurchOrder, L.PurchOrderSr, Sum(L.Qty) AS Qty " &
                         "FROM PurchChallanDetail L  " &
                         "Where L.PurchOrder = '" & SearchCode & "' " &
                         "GROUP BY L.PurchOrder, L.PurchOrderSr "

                Dim strQryPurchOrderAmend$ = "SELECT L.PurchOrder, L.PurchOrderSr, Sum(L.Qty) AS Qty " &
                                        "FROM PurchOrderDetail L  " &
                                        "Where L.PurchOrder = '" & SearchCode & "' And L.PurchOrder <> L.DocID  " &
                                        "GROUP BY L.PurchOrder, L.PurchOrderSr "

                mQry = "Select L.*, I.ManualCode , I.Description As ItemDesc, I.ManualCode As ItemManualCode, " &
                        " U.DecimalPlaces as QtyDecimalPlaces, MU.DecimalPlaces as MeasureDecimalPlaces, DMU.DecimalPlaces as DeliveryMeasureDecimalPlaces,  " &
                        " Q.V_Type || '-' || Q.ReferenceNo As PurchQuotationNo, " &
                        " Pi.V_Type || '-' || Pi.ManualRefNo As PurchIndentNo, " &
                        " D1.Description As Dimension1Desc, D2.Description As Dimension2Desc, " &
                        " MP.ManualRefNo As MaterialPlanNo, " &
                        " (Case When IfNull(PurChallan.Qty,0) <> 0 Or IfNull(PurAmend.Qty,0) <> 0 Then 1 Else 0 End) as RowLocked " &
                        " From PurchOrderDetail L " &
                        " LEFT JOIN PurchQuotation Q ON L.PurchQuotation = Q.DocId " &
                        " LEFT JOIN PurchIndent Pi ON L.PurchIndent = Pi.DocId " &
                        " LEFT JOIN MaterialPlan MP On L.MaterialPlan = MP.DocId " &
                        " LEFT JOIN Item I On L.Item = I.Code  " &
                        " Left Join Unit U On I.Unit = U.Code " &
                        " Left Join Unit MU On I.MeasureUnit = MU.Code " &
                        " LEFT JOIN Unit Dmu On L.DeliveryMeasure = Dmu.Code " &
                        " Left Join Dimension1 D1   On L.Dimension1 = D1.Code " &
                        " Left Join Dimension2 D2   On L.Dimension2 = D2.Code " &
                        " Left Join (" & strQryPurchChallan & ") as PurChallan On L.DocID = PurChallan.PurchOrder and L.Sr = PurChallan.PurchOrderSr " &
                        " Left Join (" & strQryPurchOrderAmend & ") as PurAmend On L.DocID = PurAmend.PurchOrder And L.Sr = PurAmend.PurchOrderSr " &
                        " Where L.DocId = '" & SearchCode & "' Order By L.Sr "
                DsTemp = AgL.FillData(mQry, AgL.GCn)
                With DsTemp.Tables(0)
                    Dgl1.RowCount = 1
                    Dgl1.Rows.Clear()
                    If .Rows.Count > 0 Then
                        For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                            Dgl1.Rows.Add()
                            Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count - 1
                            Dgl1.Item(ColSNo, I).Tag = AgL.XNull(.Rows(I)("Sr"))
                            Dgl1.Item(Col1ItemCode, I).Tag = AgL.XNull(.Rows(I)("Item"))
                            Dgl1.Item(Col1ItemCode, I).Value = AgL.XNull(.Rows(I)("ManualCode"))
                            Dgl1.Item(Col1Item, I).Tag = AgL.XNull(.Rows(I)("Item"))
                            Dgl1.Item(Col1Item, I).Value = AgL.XNull(.Rows(I)("ItemDesc"))
                            Dgl1.Item(Col1ItemCode, I).Value = AgL.XNull(.Rows(I)("ItemManualCode"))

                            Dgl1.Item(Col1Dimension1, I).Tag = AgL.XNull(.Rows(I)("Dimension1"))
                            Dgl1.Item(Col1Dimension1, I).Value = AgL.XNull(.Rows(I)("Dimension1Desc"))
                            Dgl1.Item(Col1Dimension2, I).Tag = AgL.XNull(.Rows(I)("Dimension2"))
                            Dgl1.Item(Col1Dimension2, I).Value = AgL.XNull(.Rows(I)("Dimension2Desc"))

                            Dgl1.Item(Col1PurchQuotation, I).Tag = AgL.XNull(.Rows(I)("PurchQuotation"))
                            Dgl1.Item(Col1PurchQuotation, I).Value = AgL.XNull(.Rows(I)("PurchQuotationNo"))
                            Dgl1.Item(Col1PurchQuotationSr, I).Value = AgL.XNull(.Rows(I)("PurchQuotationSr"))

                            Dgl1.Item(Col1PurchIndent, I).Tag = AgL.XNull(.Rows(I)("PurchIndent"))
                            Dgl1.Item(Col1PurchIndent, I).Value = AgL.XNull(.Rows(I)("PurchIndentNo"))
                            Dgl1.Item(Col1PurchIndentSr, I).Value = AgL.XNull(.Rows(I)("PurchIndentSr"))

                            Dgl1.Item(Col1MaterialPlan, I).Tag = AgL.XNull(.Rows(I)("MaterialPlan"))
                            Dgl1.Item(Col1MaterialPlan, I).Value = AgL.XNull(.Rows(I)("MaterialPlanNo"))
                            Dgl1.Item(Col1MaterialPlanSr, I).Value = AgL.XNull(.Rows(I)("MaterialPlanSr"))

                            Dgl1.Item(Col1Specification, I).Value = AgL.XNull(.Rows(I)("Specification"))
                            Dgl1.Item(Col1BillingType, I).Value = AgL.XNull(.Rows(I)("BillingType"))
                            Dgl1.Item(Col1RateType, I).Value = AgL.XNull(.Rows(I)("RateType"))
                            Dgl1.Item(Col1PartySKU, I).Value = AgL.XNull(.Rows(I)("VendorSKU"))
                            Dgl1.Item(Col1XPartySKU, I).Value = AgL.XNull(.Rows(I)("VendorSKU"))
                            Dgl1.Item(Col1Qty, I).Value = Format(AgL.VNull(.Rows(I)("Qty")), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1FreeQty, I).Value = Format(AgL.VNull(.Rows(I)("FreeQty")), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1MRP, I).Value = AgL.VNull(.Rows(I)("MRP"))
                            Dgl1.Item(Col1Deal, I).Value = AgL.XNull(.Rows(I)("Deal"))
                            Dgl1.Item(Col1Rate, I).Value = AgL.VNull(.Rows(I)("Rate"))
                            Dgl1.Item(Col1QtyDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("QtyDecimalPlaces"))
                            Dgl1.Item(Col1Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                            Dgl1.Item(Col1Amount, I).Value = AgL.VNull(.Rows(I)("Amount"))
                            Dgl1.Item(Col1DeliveryMeasure, I).Value = AgL.XNull(.Rows(I)("DeliveryMeasure"))
                            Dgl1.Item(Col1SalesTaxGroup, I).Tag = AgL.XNull(.Rows(I)("SalesTaxGroupItem"))
                            Dgl1.Item(Col1SalesTaxGroup, I).Value = AgL.XNull(.Rows(I)("SalesTaxGroupItem"))
                            Dgl1.Item(Col1PcsPerMeasure, I).Value = AgL.VNull(.Rows(I)("PcsPerMeasure"))
                            Dgl1.Item(Col1MeasurePerPcs, I).Value = Format(AgL.VNull(.Rows(I)("MeasurePerPcs")), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1TotalMeasure, I).Value = Format(AgL.VNull(.Rows(I)("TotalMeasure")), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1TotalFreeMeasure, I).Value = Format(AgL.VNull(.Rows(I)("TotalFreeMeasure")), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1MeasureDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("MeasureDecimalPlaces"))
                            Dgl1.Item(Col1MeasureUnit, I).Value = AgL.XNull(.Rows(I)("MeasureUnit"))
                            Dgl1.Item(Col1DeliveryMeasureMultiplier, I).Value = AgL.VNull(.Rows(I)("DeliveryMeasureMultiplier"))
                            Dgl1.Item(Col1DeliveryMeasurePerPcs, I).Value = Format(AgL.VNull(.Rows(I)("DeliveryMeasurePerPcs")), "0.".PadRight(AgL.VNull(.Rows(I)("DeliveryMeasureDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1TotalDeliveryMeasure, I).Value = Format(AgL.VNull(.Rows(I)("TotalDeliveryMeasure")), "0.".PadRight(AgL.VNull(.Rows(I)("DeliveryMeasureDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1TotalFreeDeliveryMeasure, I).Value = Format(AgL.VNull(.Rows(I)("TotalFreeDeliveryMeasure")), "0.".PadRight(AgL.VNull(.Rows(I)("DeliveryMeasureDecimalPlaces")) + 2, "0"))

                            Dgl1.Item(Col1DeliveryMeasureDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("DeliveryMeasureDecimalPlaces"))

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

                            Call FMoveRecLine(mSearchCode, AgL.VNull(.Rows(I)("Sr")), I)



                        Next I
                    End If

                    If Dgl1.Item(Col1Unit, 0).Value <> "" And IsSameUnit Then LblTotalQtyText.Text = "Total Qty (" & Dgl1.Item(Col1Unit, 0).Value & ") :" Else LblTotalQtyText.Text = "Total Qty :"
                    If Dgl1.Item(Col1MeasureUnit, 0).Value <> "" And IsSameMeasureUnit Then LblTotalMeasureText.Text = "Total Measure (" & Dgl1.Item(Col1MeasureUnit, 0).Value & ") :" Else LblTotalMeasureText.Text = "Total Measure :"
                    If Dgl1.Item(Col1DeliveryMeasure, 0).Value <> "" And IsSameDeliveryMeasureUnit Then LblTotalDeliveryMeasureText.Text = "Total Delivery Measure (" & Dgl1.Item(Col1DeliveryMeasure, 0).Value & ") :" Else LblTotalDeliveryMeasureText.Text = "Total Delivery Measure :"
                End With

                LblTotalQty.Text = Format(Val(LblTotalQty.Text), "0.".PadRight(intQtyDecimalPlaces + 2, "0"))
                LblTotalMeasure.Text = Format(Val(LblTotalMeasure.Text), "0.".PadRight(intMeasureDecimalPlaces + 2, "0"))
                LblTotalDeliveryMeasure.Text = Format(Val(LblTotalDeliveryMeasure.Text), "0.".PadRight(intDeliveryMeasureDecimalPlaces + 2, "0"))
                LblTotalAmount.Text = Format(Val(LblTotalAmount.Text), "0.00")

                If AgCustomGrid1.Rows.Count = 0 Then AgCustomGrid1.Visible = False
                '-------------------------------------------------------------
            End If
        End With
    End Sub

    Private Sub FrmPurchOrder_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Topctrl1.ChangeAgGridState(Dgl1, False)
        AgCalcGrid1.FrmType = Me.FrmType
        AgCustomGrid1.FrmType = Me.FrmType
    End Sub

    Private Sub Txt_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtV_Type.Validating, TxtShipToPartyCity.Validating
        Dim I As Integer = 0
        Try
            Select Case sender.NAME
                Case TxtV_Type.Name
                    TxtStructure.Tag = AgStructure.ClsMain.FGetStructureFromNCat(LblV_Type.Tag, AgL.GcnRead)
                    AgCalcGrid1.AgStructure = TxtStructure.Tag

                    TxtCustomFields.Tag = AgCustomFields.ClsMain.FGetCustomFieldFromV_Type(TxtV_Type.Tag, AgL.GcnRead)
                    AgCustomGrid1.AgCustom = TxtCustomFields.Tag

                    IniGrid()
                    TxtReferenceNo.Text = AgTemplate.ClsMain.FGetManualRefNo("ReferenceNo", "PurchOrder", TxtV_Type.Tag, TxtV_Date.Text, TxtDivision.Tag, TxtSite_Code.Tag, AgTemplate.ClsMain.ManualRefType.Max)
                    TxtTermsAndConditions.Text = AgTemplate.ClsMain.FRetTermsCondition(TxtV_Type.AgSelectedValue)
                    ClsMain.FFillPurchaseEnviro(TxtV_Type.Tag)
                    If AgL.VNull(DtPurhcaseEnviro.Rows(0)("GenerateItem_UidFromPO")) <> 0 Then BtnPrintBarcode.Visible = True Else BtnPrintBarcode.Visible = False

                Case TxtShipToPartyCity.Name
                    Validating_ShipToPartyCity(sender.Tag)
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Validating_ShipToPartyCity(ByVal Code As String)
        Dim DrTemp As DataRow() = Nothing
        If TxtShipToPartyCity.Text <> "" Then
            If TxtShipToPartyCity.AgHelpDataSet IsNot Nothing Then
                DrTemp = TxtShipToPartyCity.AgHelpDataSet.Tables(0).Select(" Code = '" & Code & "' ")
                If DrTemp.Length > 0 Then
                    TxtShipToPartyState.Text = AgL.XNull(DrTemp(0)("State"))
                    TxtShipToPartyCountry.Text = AgL.XNull(DrTemp(0)("Country"))
                Else
                    TxtShipToPartyState.Text = ""
                    TxtShipToPartyCountry.Text = ""
                End If
            End If
        End If
    End Sub

    Private Sub FrmPurchOrder_BaseEvent_Topctrl_tbAdd() Handles Me.BaseEvent_Topctrl_tbAdd
        BtnFillPartyDetail.Tag = Nothing

        TxtStructure.Tag = AgStructure.ClsMain.FGetStructureFromNCat(LblV_Type.Tag, AgL.GcnRead)
        AgCalcGrid1.AgStructure = TxtStructure.Tag

        TxtCustomFields.Tag = AgCustomFields.ClsMain.FGetCustomFieldFromV_Type(TxtV_Type.Tag, AgL.GCn)
        AgCustomGrid1.AgCustom = TxtCustomFields.Tag


        IniGrid()
        TabControl1.SelectedTab = TP1

        mIsEntryLocked = False
        TxtVendor.Enabled = True

        RbtOrderForIndent.Checked = True

        ClsMain.FFillPurchaseEnviro(TxtV_Type.Tag)
        If AgL.VNull(DtPurhcaseEnviro.Rows(0)("GenerateItem_UidFromPO")) <> 0 Then BtnPrintBarcode.Visible = True Else BtnPrintBarcode.Visible = False

        TxtReferenceNo.Text = AgTemplate.ClsMain.FGetManualRefNo("ReferenceNo", "PurchOrder", TxtV_Type.Tag, TxtV_Date.Text, TxtDivision.Tag, TxtSite_Code.Tag, AgTemplate.ClsMain.ManualRefType.Max)
        TxtVendor.Focus()
    End Sub

    Private Sub TxtSaleToParty_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtVendor.KeyDown, TxtReferenceParty.KeyDown, TxtCurrency.KeyDown, TxtSalesTaxGroupParty.KeyDown, TxtAgent.KeyDown, TxtShipToPartyCity.KeyDown, TxtShipToParty.KeyDown
        Dim FrmObj As Object = Nothing

        Try
            If e.KeyCode = Keys.Enter Then Exit Sub

            Select Case sender.Name
                Case TxtVendor.Name, TxtReferenceParty.Name, TxtShipToParty.Name
                    If e.KeyCode <> Keys.Enter Then
                        If sender.AgHelpDataSet Is Nothing Then
                            FCreateHelpSubgroup(sender)
                        End If
                    End If

                Case TxtCurrency.Name
                    If TxtCurrency.AgHelpDataSet Is Nothing Then
                        mQry = "SELECT Code, Description AS Currency FROM Currency ORDER BY Code "
                        TxtCurrency.AgHelpDataSet(0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
                    End If

                Case TxtSalesTaxGroupParty.Name
                    If TxtSalesTaxGroupParty.AgHelpDataSet Is Nothing Then
                        mQry = "SELECT Description AS Code, Description, IfNull(Active,0)  " &
                                " FROM PostingGroupSalesTaxParty " &
                                " Where IfNull(Active,1) <> 0 "
                        TxtSalesTaxGroupParty.AgHelpDataSet(1, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
                    End If

                Case TxtAgent.Name
                    If TxtAgent.AgHelpDataSet Is Nothing Then
                        mQry = "SELECT SubCode AS Code, DispName || ',' || IfNull(C.CityName,'') As Agent  " &
                                " FROM SubGroup Sg " &
                                " LEFT JOIN City C On Sg.CityCode = C.CityCode " &
                                " Where IfNull(Sg.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' "
                        TxtAgent.AgHelpDataSet(0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
                    End If

                Case TxtShipToPartyCity.Name
                    If TxtShipToPartyCity.AgHelpDataSet Is Nothing Then
                        mQry = " SELECT C.CityCode AS Code, C.CityName, C.State, C.Country " &
                                " FROM City C  " &
                                " Where IfNull(C.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' "
                        TxtShipToPartyCity.AgHelpDataSet(0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub TxtSaleToParty_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtVendor.Validating, TxtV_Date.Validating, TxtDeliveryDate.Validating, TxtShipToParty.Validating, TxtSalesTaxGroupParty.Validating
        Dim DrTemp As DataRow()
        Dim DtTemp As DataTable = Nothing
        Dim I As Integer = 0, J As Integer = 0
        Dim FrmObj As New FrmPurchPartyDetail
        Try
            Select Case sender.name
                Case TxtVendor.Name
                    If sender.text.ToString.Trim <> "" Then
                        If sender.AgHelpDataSet IsNot Nothing Then
                            DrTemp = sender.AgHelpDataSet.Tables(0).Select("SubCode = " & AgL.Chk_Text(sender.Tag) & "")

                            If DrTemp.Length > 0 Then
                                If TxtCurrency.Text = "" Then
                                    TxtCurrency.Tag = AgL.XNull(DrTemp(0)("Currency"))
                                    TxtCurrency.Text = AgL.XNull(DrTemp(0)("CurrencyDesc"))
                                End If

                                If TxtSalesTaxGroupParty.Text = "" Then
                                    TxtSalesTaxGroupParty.Tag = AgL.XNull(DrTemp(0)("SalesTaxPostingGroup"))
                                    TxtSalesTaxGroupParty.Text = AgL.XNull(DrTemp(0)("SalesTaxPostingGroup"))
                                End If

                                TxtNature.Text = AgL.XNull(DrTemp(0)("Nature"))
                            End If

                            Call ProcFillExportDetail(TxtVendor.Tag, TxtV_Date.Text)
                        End If

                        If TxtReferenceParty.Text = "" Then
                            TxtReferenceParty.Tag = TxtVendor.Tag
                            TxtReferenceParty.Text = TxtVendor.Text
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
                    Else
                        TxtCurrency.Tag = ""
                        TxtCurrency.Text = ""
                        TxtShipToParty.Text = ""
                        TxtShipToPartyAdd1.Text = ""
                        TxtShipToPartyAdd2.Text = ""
                        TxtShipToPartyCity.Tag = ""
                        TxtShipToPartyState.Text = ""
                        TxtShipToPartyCountry.Text = ""
                        BtnFillPartyDetail.Tag = Nothing
                    End If


                Case TxtDeliveryDate.Name
                    For I = 0 To Dgl1.Rows.Count - 1
                        If Dgl1.Item(Col1BtnDeliveryDetail, I).Tag IsNot Nothing Then
                            For J = 0 To Dgl1.Item(Col1BtnDeliveryDetail, I).Tag.Dgl1.Rows.Count - 1
                                If Val(Dgl1.Item(Col1BtnDeliveryDetail, I).Tag.Dgl1.Item(FrmPurchOrderDelivery.Col1Qty, J).Value) <> 0 Then
                                    If TxtDeliveryDate.Text <> "" Then
                                        Dgl1.Item(Col1BtnDeliveryDetail, I).Tag.Dgl1.Item(FrmPurchOrderDelivery.Col1DeliveryDate, J).Value = TxtDeliveryDate.Text
                                    Else
                                        'Dgl1.Item(Col1BtnDeliveryDetail, I).Tag.Dgl1.Item(FrmPurchOrderDelivery.Col1DeliveryDate, J).Value = ""
                                    End If
                                End If
                            Next
                        End If
                    Next

                Case TxtShipToParty.Name
                    If TxtShipToParty.Tag <> "" Then
                        mQry = " Select IfNull(Add1,'') As Add1, IfNull(Add2,'') As Add2, " &
                                   " Sg.CityCode As City, C.CityName As CityName, " &
                                   " C.State, C.Country  " &
                                   " From SubGroup Sg " &
                                   " LEFT JOIN City C ON Sg.CityCode = C.CityCode " &
                                   " Where Sg.SubCode = '" & TxtShipToParty.Tag & "'  "
                        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
                        With DtTemp
                            TxtShipToPartyAdd1.Text = AgL.XNull(.Rows(0)("Add1"))
                            TxtShipToPartyAdd2.Text = AgL.XNull(.Rows(0)("Add2"))
                            TxtShipToPartyCity.Tag = AgL.XNull(.Rows(0)("City"))
                            TxtShipToPartyCity.Text = AgL.XNull(.Rows(0)("CityName"))
                            TxtShipToPartyState.Text = AgL.XNull(.Rows(0)("State"))
                            TxtShipToPartyCountry.Text = AgL.XNull(.Rows(0)("Country"))
                        End With
                    End If

                Case TxtSalesTaxGroupParty.Name
                    Call Calculation()

            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Validating_ItemCode(ByVal mColumn As Integer, ByVal mRow As Integer)
        Dim DrTemp As DataRow() = Nothing
        Dim DtTemp As DataTable = Nothing
        Try
            If Dgl1.Item(mColumn, mRow).Value.ToString.Trim = "" Or Dgl1.AgSelectedValue(mColumn, mRow).ToString.Trim = "" Then
                Dgl1.Item(Col1Unit, mRow).Value = ""
                Dgl1.Item(Col1SalesTaxGroup, mRow).Value = ""
                Dgl1.Item(Col1MeasureUnit, mRow).Value = ""
                Dgl1.Item(Col1MeasurePerPcs, mRow).Value = ""
                Dgl1.Item(Col1Rate, mRow).Value = ""
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

                    Dgl1.Item(Col1DeliveryMeasureDecimalPlaces, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("MeasureDecimalPlaces").Value)

                    Dgl1.Item(Col1DeliveryMeasure, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("MeasureUnit").Value)
                    Dgl1.Item(Col1DeliveryMeasureMultiplier, mRow).Value = 1

                    Dgl1.Item(Col1Rate, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("Rate").Value)
                    Dgl1.Item(Col1Specification, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("Specification").Value)

                    Dgl1.Item(Col1Dimension1, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("Dimension1").Value)
                    Dgl1.Item(Col1Dimension1, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("" & ClsMain.FGetDimension1Caption() & "").Value)
                    Dgl1.Item(Col1Dimension2, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("Dimension2").Value)
                    Dgl1.Item(Col1Dimension2, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("" & ClsMain.FGetDimension2Caption() & "").Value)



                    Dgl1.Item(Col1PurchQuotation, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("PurchQuotation").Value)
                    Dgl1.Item(Col1PurchQuotation, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("Quot_No").Value)
                    Dgl1.Item(Col1PurchQuotationSr, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("PurchQuotationSr").Value)

                    Dgl1.Item(Col1PurchIndent, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("PurchIndent").Value)
                    Dgl1.Item(Col1PurchIndent, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("Indent_No").Value)
                    Dgl1.Item(Col1PurchIndentSr, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("PurchIndentSr").Value)

                    Dgl1.Item(Col1MaterialPlan, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("ProdOrder").Value)
                    Dgl1.Item(Col1MaterialPlan, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("ProdOrderNo").Value)

                    Dgl1.Item(Col1Qty, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("Bal.Qty").Value)
                    Dgl1.Item(Col1TotalMeasure, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("Bal.Measure").Value)

                    Dgl1.Item(Col1Rate, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("Bal.Measure").Value)

                    Dgl1.Item(Col1SalesTaxGroup, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("SalesTaxPostingGroup").Value)
                    Dgl1.Item(Col1SalesTaxGroup, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("SalesTaxPostingGroup").Value)

                    If AgL.StrCmp(Dgl1.Item(Col1SalesTaxGroup, mRow).Tag, "") Then
                        Dgl1.Item(Col1SalesTaxGroup, mRow).Tag = AgL.XNull(AgL.PubDtEnviro.Rows(0)("DefaultSalesTaxGroupItem"))
                        Dgl1.Item(Col1SalesTaxGroup, mRow).Value = AgL.XNull(AgL.PubDtEnviro.Rows(0)("DefaultSalesTaxGroupItem"))
                    End If
                    If Dgl1.Item(Col1MeasureUnit, mRow).Value = "" Then Dgl1.Item(Col1TotalMeasure, mRow).ReadOnly = True
                End If
                Try
                    If mRow <> 0 Then
                        Dgl1.Item(Col1DeliveryMeasure, mRow).Value = Dgl1.Item(Col1DeliveryMeasure, mRow - 1).Value
                        Dgl1.Item(Col1BillingType, mRow).Value = Dgl1.Item(Col1BillingType, mRow - 1).Value
                        Dgl1.Item(Col1RateType, mRow).Value = Dgl1.Item(Col1RateType, mRow - 1).Value
                    End If
                Catch ex As Exception
                End Try
            End If

            mQry = "Select BuyerSKU From ItemBuyer Where Code = '" & Dgl1.Item(mColumn, mRow).Tag & "' And Buyer = '" & TxtReferenceParty.Tag & "' "
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtTemp.Rows.Count > 0 Then
                Dgl1.Item(Col1PartySKU, mRow).Value = AgL.XNull(DtTemp.Rows(0)("BuyerSKU"))
            Else
                Dgl1.Item(Col1PartySKU, mRow).Value = ""
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " On Validating_Item Function ")
        End Try
    End Sub

    Private Sub Dgl1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellEnter
        If Dgl1.CurrentCell Is Nothing Then Exit Sub
        Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
            Case Col1Qty
                CType(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex), AgControls.AgTextColumn).AgNumberRightPlaces = Val(Dgl1.Item(Col1QtyDecimalPlaces, Dgl1.CurrentCell.RowIndex).Value)

            Case Col1MeasurePerPcs, Col1TotalMeasure
                CType(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex), AgControls.AgTextColumn).AgNumberRightPlaces = Val(Dgl1.Item(Col1MeasureDecimalPlaces, Dgl1.CurrentCell.RowIndex).Value)

            Case Col1Dimension1, Col1Dimension2
                If Dgl1.Item(Col1PurchIndent, Dgl1.CurrentCell.RowIndex).Value <> "" Then
                    Dgl1.Columns(Col1Dimension1).ReadOnly = True
                    Dgl1.Columns(Col1Dimension2).ReadOnly = True
                Else
                    Dgl1.Columns(Col1Dimension1).ReadOnly = False
                    Dgl1.Columns(Col1Dimension2).ReadOnly = False
                End If

        End Select
    End Sub

    Private Sub Dgl1_EditingControl_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Dgl1.EditingControl_Validating
        Dim I As Integer
        If Topctrl1.Mode = "Browse" Then Exit Sub
        Dim mRowIndex As Integer, mColumnIndex As Integer

        Dim DrTemp As DataRow() = Nothing
        Try
            mRowIndex = Dgl1.CurrentCell.RowIndex
            mColumnIndex = Dgl1.CurrentCell.ColumnIndex
            If Dgl1.Item(mColumnIndex, mRowIndex).Value Is Nothing Then Dgl1.Item(mColumnIndex, mRowIndex).Value = ""
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Item
                    Validating_ItemCode(mColumnIndex, mRowIndex)
                    Call FillDeliveryDetail(mRowIndex, False)
                    Call FGetDeliveryMeasureMultiplier(mRowIndex)

                    If CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_TransactionHistory")), Boolean) = True Then
                        FShowTransactionHistory(Dgl1.Item(Col1Item, mRowIndex).Tag)
                    End If

                Case Col1ItemCode
                    Validating_ItemCode(mColumnIndex, mRowIndex)
                    Call FillDeliveryDetail(mRowIndex, False)
                    Call FGetDeliveryMeasureMultiplier(mRowIndex)

                    If CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_TransactionHistory")), Boolean) = True Then
                        FShowTransactionHistory(Dgl1.Item(Col1Item, mRowIndex).Tag)
                    End If

                Case Col1Qty
                    Call FillDeliveryDetail(mRowIndex, False)

                Case Col1DeliveryMeasure
                    Call FGetDeliveryMeasureMultiplier(mRowIndex)

                    If mRowIndex < Dgl1.RowCount - 1 Then
                        If Dgl1.Item(Col1DeliveryMeasure, mRowIndex).Value <> "" Then
                            If Dgl1.Item(Col1DeliveryMeasure, mRowIndex + 1).Value <> Dgl1.Item(Col1DeliveryMeasure, mRowIndex).Value And Dgl1.Item(Col1Item, mRowIndex + 1).Value <> "" Then
                                If MsgBox("Apply to all?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
                                    For I = 0 To Dgl1.RowCount - 1
                                        If Dgl1.Item(Col1Item, I).Value <> "" Then
                                            Dgl1.Item(Col1DeliveryMeasure, I).Value = Dgl1.Item(Col1DeliveryMeasure, mRowIndex).Value
                                            Call FGetDeliveryMeasureMultiplier(I)
                                        End If
                                    Next
                                    Calculation()
                                End If
                            End If
                        End If
                    End If

                Case Col1BillingType
                    If mRowIndex < Dgl1.RowCount - 1 Then
                        If Dgl1.Item(Col1BillingType, mRowIndex).Value <> "" Then
                            If Dgl1.Item(Col1BillingType, mRowIndex + 1).Value <> Dgl1.Item(Col1BillingType, mRowIndex).Value And Dgl1.Item(Col1Item, mRowIndex + 1).Value <> "" Then
                                If MsgBox("Apply to all?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
                                    For I = 0 To Dgl1.RowCount - 1
                                        If Dgl1.Item(Col1Item, I).Value <> "" Then
                                            Dgl1.Item(Col1BillingType, I).Value = Dgl1.Item(Col1BillingType, mRowIndex).Value
                                        End If
                                    Next
                                    Calculation()
                                End If
                            End If
                        End If
                    End If

                Case Col1RateType
                    Dgl1.Item(Col1Rate, mRowIndex).Value = ClsMain.FGetItemRate(Dgl1.Item(Col1Item, mRowIndex).Tag, Dgl1.Item(Col1RateType, mRowIndex).Tag, TxtV_Date.Text)
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

        Dim DealArr() As String = Nothing
        Dim DealRate As Double = 0
        Dim mRate As Double = 0

        Dim IsSameUnit As Boolean = True
        Dim IsSameMeasureUnit As Boolean = True
        Dim IsSameDeliveryMeasureUnit As Boolean = True

        Dim intQtyDecimalPlaces As Integer = 0
        Dim intMeasureDecimalPlaces As Integer = 0
        Dim intDeliveryMeasureDecimalPlaces As Integer = 0

        LblTotalQty.Text = 0
        LblTotalMeasure.Text = 0
        LblTotalDeliveryMeasure.Text = 0
        LblTotalAmount.Text = 0

        AgCalcGrid1.AgLineGridPostingGroupSalesTaxProd = Dgl1.Columns(Col1SalesTaxGroup).Index
        AgCalcGrid1.AgPostingGroupSalesTaxParty = TxtSalesTaxGroupParty.AgSelectedValue
        AgCalcGrid1.AgPostingGroupSalesTaxItem = AgL.XNull(AgL.PubDtEnviro.Rows(0)("DefaultSalesTaxGroupItem"))

        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1Item, I).Value <> "" Then

                'For Deal Calculation
                DealRate = 0
                If Dgl1.Item(Col1Deal, I).Value <> "" Then
                    DealArr = Split(Dgl1.Item(Col1Deal, I).Value.ToString, "+", 2)
                    If DealArr.Length = 2 Then
                        DealRate = Format((Val(Dgl1.Item(Col1Rate, I).Value) * Val(DealArr(0))) / (Val(DealArr(0)) + Val(DealArr(1))), "0.00")
                    End If
                End If

                If DealRate <> 0 Then
                    mRate = DealRate
                Else
                    mRate = Val(Dgl1.Item(Col1Rate, I).Value)
                End If

                'If In Item Master Measure Per Pcs Is Defined then this calculation will be executed.
                'For Example In Carpet Area Per Pcs Is Defined in Item Master and Total Area will be calculated
                'with that Area per pcs. 
                If Val(Dgl1.Item(Col1MeasurePerPcs, I).Value) <> 0 Then
                    Dgl1.Item(Col1TotalMeasure, I).Value = Format(Val(Dgl1.Item(Col1Qty, I).Value) * Val(Dgl1.Item(Col1MeasurePerPcs, I).Value), "0.".PadRight(CType(Dgl1.Columns(Col1TotalMeasure), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                End If

                'If in item master Pcs Per Measure is defined this calculation will be executed.
                'for example in case of soap user will feed how many cartons he purchased in the measure field and
                'qty will be calculated on the basis of the pcs per measure.
                If Val(Dgl1.Item(Col1PcsPerMeasure, I).Value) <> 0 Then
                    Dgl1.Item(Col1Qty, I).Value = Format(Val(Dgl1.Item(Col1TotalMeasure, I).Value) * Val(Dgl1.Item(Col1PcsPerMeasure, I).Value), "0.".PadRight(CType(Dgl1.Columns(Col1Qty), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                End If

                'if the qty unit and mesure units are equal then qty will auto come in mesure fields
                'for example yarn's unit and measure unit is Kg
                'In this case same figure will be copied in the measure.
                If AgL.StrCmp(Dgl1.Item(Col1MeasureUnit, I).Value, Dgl1.Item(Col1Unit, I).Value) Then
                    Dgl1.Item(Col1TotalMeasure, I).Value = Format(Val(Dgl1.Item(Col1Qty, I).Value), "0.".PadRight(CType(Dgl1.Columns(Col1TotalMeasure), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
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
                If Val(Dgl1.Item(Col1DeliveryMeasureMultiplier, I).Value) <> 0 Then
                    Dgl1.Item(Col1DeliveryMeasurePerPcs, I).Value = Format(Val(Dgl1.Item(Col1MeasurePerPcs, I).Value) * Val(Dgl1.Item(Col1DeliveryMeasureMultiplier, I).Value), "0.".PadRight(CType(Dgl1.Columns(Col1TotalDeliveryMeasure), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                    Dgl1.Item(Col1TotalDeliveryMeasure, I).Value = Format(Val(Dgl1.Item(Col1TotalMeasure, I).Value) * Val(Dgl1.Item(Col1DeliveryMeasureMultiplier, I).Value), "0.".PadRight(CType(Dgl1.Columns(Col1TotalDeliveryMeasure), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                ElseIf Val(Dgl1.Item(Col1DeliveryMeasurePerPcs, I).Value) <> 0 Then
                    Dgl1.Item(Col1TotalDeliveryMeasure, I).Value = Format(Val(Dgl1.Item(Col1Qty, I).Value) * Val(Dgl1.Item(Col1DeliveryMeasurePerPcs, I).Value), "0.".PadRight(CType(Dgl1.Columns(Col1TotalDeliveryMeasure), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                End If

                If AgL.StrCmp(Dgl1.Item(Col1BillingType, I).Value, "Measure") Then
                    Dgl1.Item(Col1Amount, I).Value = Format(Val(Dgl1.Item(Col1TotalDeliveryMeasure, I).Value) * mRate, "0.".PadRight(CType(Dgl1.Columns(Col1Amount), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                ElseIf AgL.StrCmp(Dgl1.Item(Col1BillingType, I).Value, "Qty") Then
                    Dgl1.Item(Col1Amount, I).Value = Format(Val(Dgl1.Item(Col1Qty, I).Value) * mRate, "0.".PadRight(CType(Dgl1.Columns(Col1Amount), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                Else
                    Dgl1.Item(Col1Amount, I).Value = Format(Val(Dgl1.Item(Col1Qty, I).Value) * mRate, "0.".PadRight(CType(Dgl1.Columns(Col1Amount), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                End If

                If Not AgL.StrCmp(Dgl1.Item(Col1Unit, I).Value, Dgl1.Item(Col1Unit, 0).Value) Then IsSameUnit = False
                If Not AgL.StrCmp(Dgl1.Item(Col1MeasureUnit, I).Value, Dgl1.Item(Col1MeasureUnit, 0).Value) Then IsSameMeasureUnit = False
                If Not AgL.StrCmp(Dgl1.Item(Col1DeliveryMeasure, I).Value, Dgl1.Item(Col1DeliveryMeasure, 0).Value) Then IsSameDeliveryMeasureUnit = False

                If intQtyDecimalPlaces < Val(Dgl1.Item(Col1QtyDecimalPlaces, I).Value) Then intQtyDecimalPlaces = Val(Dgl1.Item(Col1QtyDecimalPlaces, I).Value)
                If intMeasureDecimalPlaces < Val(Dgl1.Item(Col1MeasureDecimalPlaces, I).Value) Then intMeasureDecimalPlaces = Val(Dgl1.Item(Col1MeasureDecimalPlaces, I).Value)
                If intDeliveryMeasureDecimalPlaces < Val(Dgl1.Item(Col1DeliveryMeasureDecimalPlaces, I).Value) Then intDeliveryMeasureDecimalPlaces = Val(Dgl1.Item(Col1DeliveryMeasureDecimalPlaces, I).Value)


                'Footer Calculation
                LblTotalQty.Text = Val(LblTotalQty.Text) + Val(Dgl1.Item(Col1Qty, I).Value)
                LblTotalMeasure.Text = Val(LblTotalMeasure.Text) + Val(Dgl1.Item(Col1TotalMeasure, I).Value)
                LblTotalDeliveryMeasure.Text = Val(LblTotalDeliveryMeasure.Text) + Val(Dgl1.Item(Col1TotalDeliveryMeasure, I).Value)
                LblTotalAmount.Text = Val(LblTotalAmount.Text) + Val(Dgl1.Item(Col1Amount, I).Value)
            End If
        Next

        AgCalcGrid1.AgPostingGroupSalesTaxParty = TxtSalesTaxGroupParty.Tag
        AgCalcGrid1.AgPostingGroupSalesTaxItem = AgL.XNull(AgL.PubDtEnviro.Rows(0)("DefaultSalesTaxGroupItem"))
        AgCalcGrid1.AgLineGridPostingGroupSalesTaxProd = Dgl1.Columns(Col1SalesTaxGroup).Index
        AgCalcGrid1.AgVoucherCategory = "PURCH"
        AgCalcGrid1.Calculation()

        LblTotalQty.Text = Format(Val(LblTotalQty.Text), "0.".PadRight(intQtyDecimalPlaces + 2, "0"))
        LblTotalMeasure.Text = Format(Val(LblTotalMeasure.Text), "0.".PadRight(intMeasureDecimalPlaces + 2, "0"))
        LblTotalDeliveryMeasure.Text = Format(Val(LblTotalDeliveryMeasure.Text), "0.".PadRight(intDeliveryMeasureDecimalPlaces + 2, "0"))
        LblTotalAmount.Text = Format(Val(LblTotalAmount.Text), "0.00")


        If Dgl1.Item(Col1Unit, 0).Value <> "" And IsSameUnit Then LblTotalQtyText.Text = "Qty (" & Dgl1.Item(Col1Unit, 0).Value & ") :" Else LblTotalQtyText.Text = "Qty :"
        If Dgl1.Item(Col1MeasureUnit, 0).Value <> "" And IsSameMeasureUnit Then LblTotalMeasureText.Text = "Measure (" & Dgl1.Item(Col1MeasureUnit, 0).Value & ") :" Else LblTotalMeasureText.Text = "Measure :"
        If Dgl1.Item(Col1DeliveryMeasure, 0).Value <> "" And IsSameDeliveryMeasureUnit Then LblTotalDeliveryMeasureText.Text = "Delivery Measure (" & Dgl1.Item(Col1DeliveryMeasure, 0).Value & ") :" Else LblTotalDeliveryMeasureText.Text = "Delivery Measure :"
    End Sub

    Private Sub FrmPurchOrder_BaseFunction_DispText() Handles Me.BaseFunction_DispText
        TxtShipToPartyState.Enabled = False
        TxtShipToPartyCountry.Enabled = False

        'TxtPartyOrderNo.Enabled = False
        'TxtPartyOrderDate.Enabled = False

        'If CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_ItemCode")), Boolean) = False Then
        '    TabControl1.TabPages.Remove(TPShipping)
        'End If

        If CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Measure")), Boolean) = False Then
            LblTotalDeliveryMeasureText.Visible = False
            LblTotalDeliveryMeasure.Visible = False
        End If



        'BtnFillPendingQuotation.Enabled = False
    End Sub

    Private Sub TxtOrderCancelDate_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtRemarks.LostFocus
        Select Case sender.NAME
            Case TxtRemarks.Name
                TabControl1.SelectedTab = TPShipping
        End Select
    End Sub

    Private Sub FrmPurchOrder_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        Dim I As Integer = 0
        Dim bIndentQty As Double : Dim bOrderQty As Double

        If AgL.RequiredField(TxtReferenceNo, LblReferenceNo.Text) Then passed = False : Exit Sub

        If TxtDeliveryDate.Text <> "" Then
            If CDate(TxtV_Date.Text) > CDate(TxtDeliveryDate.Text) Then
                MsgBox("Delivery date can't be less than order date")
                TabControl1.SelectedTab = TP1 : TxtDeliveryDate.Focus()
                passed = False : Exit Sub
            End If
        End If

        If AgCL.AgIsBlankGrid(Dgl1, Dgl1.Columns(Col1Item).Index) Then passed = False : Exit Sub
        If AgCL.AgIsDuplicate(Dgl1, "" & Dgl1.Columns(Col1Item).Index & "," & Dgl1.Columns(Col1PurchIndent).Index & "," & Dgl1.Columns(Col1PurchQuotation).Index & "," & Dgl1.Columns(Col1Specification).Index & "," & Dgl1.Columns(Col1Dimension1).Index & "," & Dgl1.Columns(Col1Dimension2).Index & "") Then passed = False : Exit Sub

        If Not ChkDeliveryDetailNotRequired.Checked Then
            If TxtDeliveryDate.Text = "" Then
                MsgBox("Delivery Date Is Blank", MsgBoxStyle.Information)
                TxtDeliveryDate.Focus()
                passed = False : Exit Sub
            End If
        End If

        passed = AgTemplate.ClsMain.FCheckDuplicateRefNo("ReferenceNo", "PurchOrder", TxtV_Type.Tag, TxtV_Date.Text, TxtDivision.Tag, TxtSite_Code.Tag, Topctrl1.Mode, TxtReferenceNo.Text, mSearchCode)

        With Dgl1
            For I = 0 To .Rows.Count - 1
                If .Item(Col1Item, I).Value <> "" Then
                    If Val(.Item(Col1Qty, I).Value) = 0 Then
                        MsgBox("Qty Is 0 At Row No " & Dgl1.Item(ColSNo, I).Value & "")
                        .CurrentCell = .Item(Col1Qty, I) : Dgl1.Focus()
                        passed = False : Exit Sub
                    End If
                End If

                '--------------- Data Validation for Order Qty should not greater than Indent Qty from Purchase Indent
                If .Item(Col1Item, I).Value <> "" And .AgSelectedValue(Col1PurchIndent, I) <> "" Then
                    bIndentQty = 0
                    bOrderQty = 0
                    mQry = " SELECT Sum(L.IndentQty) AS IndentQty " &
                            " FROM PurchIndentDetail L " &
                            " WHERE L.PurchIndent = '" & .AgSelectedValue(Col1PurchIndent, I) & "' AND L.PurchIndentSr = " & Val(.Item(Col1PurchIndentSr, I).Value) & " " &
                            " GROUP BY L.PurchIndent, L.PurchIndentSr "
                    AgL.ECmd = AgL.Dman_Execute(mQry, AgL.GCn)
                    bIndentQty = AgL.ECmd.ExecuteScalar()

                    mQry = " SELECT IfNull(sum(POD.Qty),0)  AS OrderQty " &
                            " FROM PurchOrderDetail POD " &
                            " WHERE POD.PurchIndent IS NOT NULL " &
                            " AND POD.DocId <> '" & mInternalCode & "' " &
                            " AND POD.PurchIndentSr = " & Val(.Item(Col1PurchIndentSr, I).Value) & " " &
                            " AND POD.PurchIndent = '" & .AgSelectedValue(Col1PurchIndent, I) & "' "
                    AgL.ECmd = AgL.Dman_Execute(mQry, AgL.GCn)
                    bOrderQty = AgL.ECmd.ExecuteScalar()

                    'If Math.Round(bIndentQty, 4) < Math.Round((Val(Dgl1.Item(Col1Qty, I).Value) + bOrderQty), 4) Then
                    '    MsgBox("Order Qty is Greater than Balance Indent Qty At Row No " & Dgl1.Item(ColSNo, I).Value & "")
                    '    .CurrentCell = .Item(Col1Qty, I) : Dgl1.Focus()
                    '    passed = False : Exit Sub
                    'End If
                End If
            Next
        End With
    End Sub

    Private Sub TxtShipToPartyCity_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtShipToPartyCity.Enter
        Select Case sender.name
            Case TxtShipToPartyCity.Name
        End Select
    End Sub

    Private Sub FrmPurchOrder_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
        Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
    End Sub

    Private Sub FrmGoodsReceipt_BaseEvent_Topctrl_tbPrn(ByVal SearchCode As String) Handles Me.BaseEvent_Topctrl_tbPrn
        Dim mSubQry As String = ""
        mQry = " SELECT H.DocID, H.V_Type, H.V_Date, H.ReferenceNo as PurchOrderNo , H.VendorName, H.VendorAdd1, H.VendorAdd2, " &
                " H.VendorCity, H.VendorCityName, H.VendorState, H.VendorCountry, H.Currency, H.SalesTaxGroupParty, " &
                " H.VendorOrderNo, H.VendorOrderDate, H.VendorDeliveryDate, H.VendorOrderCancelDate,  " &
                " H.TermsAndConditions, H.Remarks,  H.EntryBy, H.EntryDate, H.ApproveBy, H.ApproveDate, H.MoveToLog, H.MoveToLogDate, " &
                " H.IsDeleted, H.Status, H.UID, H.VendorMobile, H.PriceMode, IfNull(DL.CntDelivery,0) AS CntDelivery, " &
                " PI.V_Type as PurchIndentType, PI.ManualRefNo as PurchIndentNo, L.PurchIndentSr, " &
                " PQ.V_Type as PurchQuotationType, PQ.ReferenceNo as PurchQuotationNo, L.PurchQuotationSr, " &
                " MP.V_Type as MaterialPlanType, MP.ManualRefNo as MaterialPlanNo, L.MaterialPlanSr, " &
                " L.DocId, L.Sr, I.Description AS ItemDesc, L.VendorSKU, L.VendorUPC, L.SalesTaxGroupItem,  " &
                " L.Qty, L.Unit, L.MeasurePerPcs, L.MeasureUnit, L.TotalMeasure, L.Rate, L.Amount,  " &
                " D1.Description AS D1Desc, D2.Description AS D2Desc, E.Caption_Dimension1, E.Caption_Dimension2, " &
                " L.ShippedQty, L.ShippedMeasure, L.Specification, L.Remark, U.DecimalPlaces , UM.DecimalPlaces AS MeasureDecimalPlaces,  " &
                " L.BillingType, L.VendorSpecification,  " &
                " L.Supplier, L.DeliveryMeasureMultiplier,  " &
                " L.TotalDeliveryMeasure, L.RateType, L.DeliveryMeasure, L.FreeQty, L.PcsPerMeasure,  " &
                " L.TotalFreeMeasure, L.MRP, L.Deal, L.PartySKU, L.DeliveryMeasurePerPcs,  " &
                " L.TotalFreeDeliveryMeasure, " &
                " " & AgCalcGrid1.FLineTableFieldNameStr("L.", "L_") & " " &
                " " & AgCustomGrid1.FHeaderTableFieldNameStr("H.", "H_") & " " &
                " FROM (SELECT * FROM PurchOrder  WHERE DocId = '" & mSearchCode & "') AS H  " &
                " LEFT JOIN PurchOrderDetail L  ON H.DocID = L.DocId  " &
                " LEFT JOIN PurchIndent PI  ON L.PurchIndent = PI.DocId  " &
                " LEFT JOIN PurchQuotation PQ  ON L.PurchQuotation = PQ.DocId  " &
                " LEFT JOIN MaterialPlan MP  ON L.MaterialPlan = MP.DocId  " &
                " LEFT JOIN Item I  ON L.Item = I.Code " &
                " LEFT JOIN Unit U ON U.Code = L.Unit " &
                " LEFT JOIN Unit UM ON UM.Code = L.MeasureUnit " &
                " LEFT JOIN Enviro E ON E.Site_Code = H.Site_Code AND E.Div_Code = H.Div_Code " &
                " LEFT JOIN Dimension1 D1 ON D1.Code = L.Dimension1 " &
                " LEFT JOIN Dimension2 D2 ON D2.Code = L.Dimension2 " &
                " LEFT JOIN ( " &
                " SELECT L.DocId, L.TSr, count(*) AS CntDelivery " &
                " FROM PurchOrderDeliveryDetail L " &
                " GROUP BY L.DocId, L.TSr " &
                " ) DL ON DL.DocId = L.DocId AND DL.TSr = L.Sr "

        mSubQry = " SELECT L.DocId, L.TSr, L.Sr, I.Description AS ItemDesc, L.DeliveryDate, L.Qty, L.Unit, L.TotalMeasure, L.MeasureUnit, " &
                    " L.DeliveryInstructions , U.DecimalPlaces , UM.DecimalPlaces AS MeasureDecimalPlaces " &
                    " FROM PurchOrderDeliveryDetail L " &
                    " LEFT JOIN Item I ON I.Code = L.Item " &
                    " LEFT JOIN Unit U ON U.Code = L.Unit " &
                    " LEFT JOIN Unit UM ON UM.Code = L.MeasureUnit " &
                    " WHERE L.DocId = '" & mSearchCode & "'"

        ClsMain.FPrintThisDocument(Me, TxtV_Type.Tag, mQry, "PurchOrder_Print", "Purchase Order", , mSubQry, "SUBREP2")
    End Sub

    Private Sub ProcFillExportDetail(ByVal Party As String, ByVal V_Date As String)
        Dim DsTemp As DataSet = Nothing
        Try
            If Not AgL.StrCmp(Topctrl1.Mode, "Add") Then Exit Sub

            mQry = "SELECT H.* " &
                    " FROM PurchOrder H " &
                    " WHERE H.Vendor = '" & Party & "' " &
                    " AND H.V_Date <= '" & V_Date & "' " &
                    " ORDER BY H.V_Date DESC Limit 1	 "
            DsTemp = AgL.FillData(mQry, AgL.GCn)

            With DsTemp.Tables(0)
                If .Rows.Count > 0 Then
                    TxtCurrency.Tag = AgL.XNull(.Rows(0)("Currency"))
                    TxtShipToParty.Text = AgL.XNull(.Rows(0)("ShipToPartyName"))
                    TxtShipToPartyAdd1.Text = AgL.XNull(.Rows(0)("ShipToPartyAdd1"))
                    TxtShipToPartyAdd2.Text = AgL.XNull(.Rows(0)("ShipToPartyAdd2"))
                    TxtShipToPartyCity.Tag = AgL.XNull(.Rows(0)("ShipToPartyCity"))
                    TxtShipToPartyState.Text = AgL.XNull(.Rows(0)("ShipToPartyState"))
                    TxtShipToPartyCountry.Text = AgL.XNull(.Rows(0)("ShipToPartyCountry"))
                Else
                    TxtCurrency.Tag = ""
                    TxtShipToParty.Text = ""
                    TxtShipToPartyAdd1.Text = ""
                    TxtShipToPartyAdd2.Text = ""
                    TxtShipToPartyCity.Tag = ""
                    TxtShipToPartyState.Text = ""
                    TxtShipToPartyCountry.Text = ""
                End If
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub DGL1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.KeyDown
        Dim FrmObj As Object = Nothing
        'Dim CFOpen As New ClsFunction
        'Dim MDI As New MDIMain
        Dim DrTemp As DataRow() = Nothing
        Dim bRowIndex As Integer = 0, bColumnIndex As Integer = 0
        Dim bItemCode$ = ""

        If Topctrl1.Mode = "Browse" Then Exit Sub
        If Dgl1.CurrentCell Is Nothing Then Exit Sub

        If e.Control And e.KeyCode = Keys.D And Dgl1.Rows(Dgl1.CurrentCell.RowIndex).DefaultCellStyle.BackColor <> AgTemplate.ClsMain.Colours.GridRow_Locked Then
            sender.CurrentRow.Selected = True
            sender.CurrentRow.Visible = False
        End If
        If e.Control Or e.Shift Or e.Alt Then Exit Sub
    End Sub

    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub

    Private Sub FrmCarpetMaterialPlan_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AgL.WinSetting(Me, 650, 992, 0, 0)
    End Sub

    Private Sub TxtReferenceParty_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtReferenceParty.Enter
        Select Case sender.name
            Case TxtReferenceParty.Name
        End Select
    End Sub

    Private Sub Dgl1_CellContentClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellContentClick
        Dim FrmObj As FrmPurchOrderDelivery = Nothing
        Dim bColumnIndex As Integer = 0
        Dim bRowIndex As Integer = 0
        Dim I As Integer = 0
        Try
            bColumnIndex = Dgl1.CurrentCell.ColumnIndex
            bRowIndex = Dgl1.CurrentCell.RowIndex
            If Dgl1.Item(Col1Item, bRowIndex).Value = "" Then Exit Sub
            Select Case Dgl1.Columns(e.ColumnIndex).Name
                Case Col1BtnDeliveryDetail
                    If AgL.StrCmp(Topctrl1.Mode, "Browse") Then
                        Dgl1.Item(Col1BtnDeliveryDetail, bRowIndex).Tag.ShowDialog()
                    Else
                        FillDeliveryDetail(bRowIndex, True)
                    End If
            End Select
            If Not AgL.StrCmp(Topctrl1.Mode, "Browse") Then Call Calculation()
        Catch ex As Exception
            MsgBox(ex.Message & " in Dgl1_CellContentClick function")
        End Try
    End Sub

    Private Sub FillDeliveryDetail(ByVal bRowIndex As Integer, ByVal ShowWindow As Boolean)
        If Dgl1.Item(Col1BtnDeliveryDetail, bRowIndex).Tag Is Nothing Then
            Dgl1.Item(Col1BtnDeliveryDetail, bRowIndex).Tag = FunRetNewObject()
        End If
        Dgl1.Item(Col1BtnDeliveryDetail, bRowIndex).Tag.Dgl1.Readonly = IIf(AgL.StrCmp(Topctrl1.Mode, "Browse"), True, False)
        Dgl1.Item(Col1BtnDeliveryDetail, bRowIndex).Tag.LblItemName.Text = Dgl1.Item(Col1Item, bRowIndex).Value
        Dgl1.Item(Col1BtnDeliveryDetail, bRowIndex).Tag.LblQty.Text = Dgl1.Item(Col1Qty, bRowIndex).Value
        Dgl1.Item(Col1BtnDeliveryDetail, bRowIndex).Tag.LblOrderDate.Text = TxtV_Date.Text
        Dgl1.Item(Col1BtnDeliveryDetail, bRowIndex).Tag.LblDeliveryDate.Text = TxtDeliveryDate.Text
        Dgl1.Item(Col1BtnDeliveryDetail, bRowIndex).Tag.Unit = Dgl1.Item(Col1Unit, bRowIndex).Value
        Dgl1.Item(Col1BtnDeliveryDetail, bRowIndex).Tag.MeasurePerPcs = Val(Dgl1.Item(Col1MeasurePerPcs, bRowIndex).Value)
        Dgl1.Item(Col1BtnDeliveryDetail, bRowIndex).Tag.MeasureUnit = Val(Dgl1.Item(Col1MeasureUnit, bRowIndex).Value)
        Dgl1.Item(Col1BtnDeliveryDetail, bRowIndex).Tag.EntryMode = Topctrl1.Mode

        If Val(Dgl1.Item(Col1BtnDeliveryDetail, bRowIndex).Tag.Dgl1.Item(FrmPurchOrderDelivery.Col1Qty, 0).Value) = 0 Then
            Dgl1.Item(Col1BtnDeliveryDetail, bRowIndex).Tag.Dgl1.Item(FrmPurchOrderDelivery.Col1Qty, 0).Value = Dgl1.Item(Col1Qty, bRowIndex).Value
            Dgl1.Item(Col1BtnDeliveryDetail, bRowIndex).Tag.Validate_Qty(0)
            Dgl1.Item(Col1BtnDeliveryDetail, bRowIndex).Tag.Calculation()
        End If

        If ShowWindow = True Then Dgl1.Item(Col1BtnDeliveryDetail, bRowIndex).Tag.ShowDialog()
    End Sub

    Private Function FunRetNewObject() As Object
        Dim FrmObj As FrmPurchOrderDelivery
        Try
            FrmObj = New FrmPurchOrderDelivery
            FrmObj.IniGrid()
            FunRetNewObject = FrmObj
        Catch ex As Exception
            FunRetNewObject = Nothing
            MsgBox(ex.Message)
        End Try
    End Function

    Public Sub FMoveRecLine(ByVal SearchCode As String, ByVal TSr As Integer, ByVal mGridRow As Integer)
        Dim DtTemp As DataTable = Nothing
        Dim I As Integer = 0
        Try
            Dgl1.Item(Col1BtnDeliveryDetail, mGridRow).Tag = FunRetNewObject()

            mQry = "Select L.PurchOrderDelSchSr, Sum(L.Qty) As Qty, Max(L.Unit) As Unit, Max(L.MeasurePerPcs) As MeasurePerPcs, " &
                  " Max(L.MeasureUnit) As MeasureUnit, Max(L.TotalMeasure) As TotalMeasure, " &
                  " Max(L.DeliveryDate) As DeliveryDate, Max(L.DeliveryInstructions) As DeliveryInstructions, " &
                  " Max(I.Description) As ItemDesc " &
                  " From PurchOrderDeliveryDetail L " &
                  " LEFT JOIN Item I ON L.Item = I.Code " &
                  " Where L.DocId = '" & SearchCode & "' " &
                  " And L.TSr = " & Val(TSr) & " " &
                  " GROUP BY L.PurchOrder, L.PurchOrderSr, L.PurchOrderDelSchSr " &
                  " Order By L.PurchOrderDelSchSr "
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

            With DtTemp
                Dgl1.Item(Col1BtnDeliveryDetail, mGridRow).Tag.Dgl1.RowCount = 1 : Dgl1.Item(Col1BtnDeliveryDetail, mGridRow).Tag.Dgl1.Rows.Clear()
                If DtTemp.Rows.Count > 0 Then
                    For I = 0 To DtTemp.Rows.Count - 1
                        Dgl1.Item(Col1BtnDeliveryDetail, mGridRow).Tag.Dgl1.Rows.Add()
                        Dgl1.Item(Col1BtnDeliveryDetail, mGridRow).Tag.LblItemName.Text = AgL.XNull(.Rows(I)("ItemDesc"))

                        Dgl1.Item(Col1BtnDeliveryDetail, mGridRow).Tag.LblQty.Text = Dgl1.Item(Col1Qty, mGridRow).Value
                        Dgl1.Item(Col1BtnDeliveryDetail, mGridRow).Tag.LblOrderDate.Text = TxtV_Date.Text
                        Dgl1.Item(Col1BtnDeliveryDetail, mGridRow).Tag.LblDeliveryDate.Text = TxtDeliveryDate.Text

                        Dgl1.Item(Col1BtnDeliveryDetail, mGridRow).Tag.Dgl1.Item(FrmPurchOrderDelivery.ColSNo, I).Value = Dgl1.Item(Col1BtnDeliveryDetail, mGridRow).Tag.Dgl1.Rows.Count - 1
                        Dgl1.Item(Col1BtnDeliveryDetail, mGridRow).Tag.Dgl1.Item(FrmPurchOrderDelivery.ColSNo, I).Tag = AgL.VNull(.Rows(I)("PurchOrderDelSchSr"))
                        Dgl1.Item(Col1BtnDeliveryDetail, mGridRow).Tag.Dgl1.Item(FrmPurchOrderDelivery.Col1Qty, I).Value = AgL.VNull(.Rows(I)("Qty"))
                        Dgl1.Item(Col1BtnDeliveryDetail, mGridRow).Tag.Dgl1.Item(FrmPurchOrderDelivery.Col1Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                        Dgl1.Item(Col1BtnDeliveryDetail, mGridRow).Tag.Dgl1.Item(FrmPurchOrderDelivery.Col1MeasurePerPcs, I).Value = AgL.VNull(.Rows(I)("MeasurePerPcs"))
                        Dgl1.Item(Col1BtnDeliveryDetail, mGridRow).Tag.Dgl1.Item(FrmPurchOrderDelivery.Col1MeasureUnit, I).Value = AgL.XNull(.Rows(I)("MeasureUnit"))
                        Dgl1.Item(Col1BtnDeliveryDetail, mGridRow).Tag.Dgl1.Item(FrmPurchOrderDelivery.Col1TotalMeasure, I).Value = AgL.VNull(.Rows(I)("TotalMeasure"))
                        Dgl1.Item(Col1BtnDeliveryDetail, mGridRow).Tag.Dgl1.Item(FrmPurchOrderDelivery.Col1DeliveryDate, I).Value = AgL.XNull(.Rows(I)("DeliveryDate"))
                        Dgl1.Item(Col1BtnDeliveryDetail, mGridRow).Tag.Dgl1.Item(FrmPurchOrderDelivery.Col1DeliveryInstruction, I).Value = AgL.XNull(.Rows(I)("DeliveryInstructions"))
                        Dgl1.Item(Col1BtnDeliveryDetail, mGridRow).Tag.EntryMode = Topctrl1.Mode
                    Next I
                End If
            End With

            mQry = " SELECT Count(*) As Cnt " &
                    " FROM PurchOrderDeliveryDetail L  " &
                    " WHERE L.PurchOrder = '" & SearchCode & "' AND L.PurchOrderSr = '" & TSr & "' " &
                    " GROUP BY L.PurchOrder, L.PurchOrderSr, L.PurchOrderDelSchSr  " &
                    " HAVING Count(*)  > 1 "
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

            If DtTemp.Rows.Count > 0 Then
                Dgl1.Rows(mGridRow).DefaultCellStyle.BackColor = AgTemplate.ClsMain.Colours.GridRow_Locked
                Dgl1.Item(Col1BtnDeliveryDetail, mGridRow).Style.ForeColor = Color.Red
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub FGetLineQry(ByRef SelectionLineQry As String, ByVal Conn As SQLiteConnection,
                           ByVal Cmd As SQLiteCommand, ByVal mGridRow As Integer, ByVal DocId As String, ByVal TSr As Integer)
        Dim I As Integer = 0, mLineSr As Integer = 0
        For I = 0 To Dgl1.Item(Col1BtnDeliveryDetail, mGridRow).Tag.Dgl1.Rows.Count - 1
            If Dgl1.Item(Col1BtnDeliveryDetail, mGridRow).Tag.Dgl1.Item(FrmPurchOrderDelivery.Col1DeliveryDate, I).Value <> "" Then
                If Dgl1.Item(Col1BtnDeliveryDetail, mGridRow).Tag.Dgl1.Item(FrmPurchOrderDelivery.ColSNo, I).Tag Is Nothing Then
                    mLineSr += 1
                    If SelectionLineQry <> "" Then SelectionLineQry += " UNION ALL "
                    SelectionLineQry += " Select " & AgL.Chk_Text(mSearchCode) & ", " &
                            " " & Val(TSr) & ", " &
                            " " & Val(mLineSr) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1Item, mGridRow).Tag) & ", " &
                            " " & Val(Dgl1.Item(Col1BtnDeliveryDetail, mGridRow).Tag.Dgl1.Item(FrmPurchOrderDelivery.Col1Qty, I).Value) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1BtnDeliveryDetail, mGridRow).Tag.Dgl1.Item(FrmPurchOrderDelivery.Col1Unit, I).Value) & ", " &
                            " " & Val(Dgl1.Item(Col1BtnDeliveryDetail, mGridRow).Tag.Dgl1.Item(FrmPurchOrderDelivery.Col1MeasurePerPcs, I).Value) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1BtnDeliveryDetail, mGridRow).Tag.Dgl1.Item(FrmPurchOrderDelivery.Col1MeasureUnit, I).Value) & ", " &
                            " " & Val(Dgl1.Item(Col1BtnDeliveryDetail, mGridRow).Tag.Dgl1.Item(FrmPurchOrderDelivery.Col1TotalMeasure, I).Value) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1BtnDeliveryDetail, mGridRow).Tag.Dgl1.Item(FrmPurchOrderDelivery.Col1DeliveryDate, I).Value) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1BtnDeliveryDetail, mGridRow).Tag.Dgl1.Item(FrmPurchOrderDelivery.Col1DeliveryInstruction, I).Value) & ", " &
                            " " & AgL.Chk_Text(mSearchCode) & ", " &
                            " " & Val(TSr) & ", " &
                            " " & Val(mLineSr) & " "
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                Else
                    mQry = " UPDATE PurchOrderDeliveryDetail " &
                             " SET DeliveryDate = " & AgL.Chk_Text(Dgl1.Item(Col1BtnDeliveryDetail, mGridRow).Tag.Dgl1.Item(FrmPurchOrderDelivery.Col1DeliveryDate, I).Value) & ", " &
                             " Item = " & AgL.Chk_Text(Dgl1.Item(Col1Item, mGridRow).Tag) & ", " &
                             " Qty = " & Val(Dgl1.Item(Col1BtnDeliveryDetail, mGridRow).Tag.Dgl1.Item(FrmPurchOrderDelivery.Col1Qty, I).Value) & ", " &
                             " Unit = " & AgL.Chk_Text(Dgl1.Item(Col1BtnDeliveryDetail, mGridRow).Tag.Dgl1.Item(FrmPurchOrderDelivery.Col1Unit, I).Value) & ", " &
                             " MeasurePerPcs = " & Val(Dgl1.Item(Col1BtnDeliveryDetail, mGridRow).Tag.Dgl1.Item(FrmPurchOrderDelivery.Col1MeasurePerPcs, I).Value) & ", " &
                             " MeasureUnit = " & AgL.Chk_Text(Dgl1.Item(Col1BtnDeliveryDetail, mGridRow).Tag.Dgl1.Item(FrmPurchOrderDelivery.Col1MeasureUnit, I).Value) & ", " &
                             " TotalMeasure = " & Val(Dgl1.Item(Col1BtnDeliveryDetail, mGridRow).Tag.Dgl1.Item(FrmPurchOrderDelivery.Col1TotalMeasure, I).Value) & ", " &
                             " DeliveryInstructions = " & AgL.Chk_Text(Dgl1.Item(Col1BtnDeliveryDetail, mGridRow).Tag.Dgl1.Item(FrmPurchOrderDelivery.Col1DeliveryInstruction, I).Value) & " " &
                             " Where DocId = '" & mSearchCode & "' " &
                             " And TSr = " & TSr & " " &
                             " And Sr = " & Dgl1.Item(Col1BtnDeliveryDetail, mGridRow).Tag.Dgl1.Item(FrmPurchOrderDelivery.ColSNo, I).Tag & " "
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                End If
            End If
        Next
    End Sub

    Private Sub Dgl1_EditingControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.EditingControl_KeyDown
        Dim FrmObj As Object = Nothing
        'Dim CFOpen As New ClsFunction
        'Dim MDI As New MDIMain
        Dim bRowIndex As Integer = 0, bColumnIndex As Integer = 0
        Dim bItemCode As String = ""
        Dim DrTemp As DataRow() = Nothing
        Try
            bRowIndex = Dgl1.CurrentCell.RowIndex
            bColumnIndex = Dgl1.CurrentCell.ColumnIndex

            If e.KeyCode = Keys.Enter Then Exit Sub
            If Topctrl1.Mode = "Browse" Then Exit Sub

            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1ItemCode
                    If Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) Is Nothing Then
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

                Case Col1BillingType
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1BillingType) Is Nothing Then
                            Dgl1.AgHelpDataSet(Col1BillingType) = AgL.FillData(ClsMain.HelpQueries.BillingType, AgL.GCn)
                        End If
                    End If

                Case Col1DeliveryMeasure
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1DeliveryMeasure) Is Nothing Then
                            mQry = " SELECT Code, Code AS Name FROM Unit Where IfNull(IsActive,1) <> 0  "
                            Dgl1.AgHelpDataSet(Col1DeliveryMeasure) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If

                Case Col1RateType
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1RateType) Is Nothing Then
                            mQry = " SELECT H.Code, H.Description  FROM RateType H " &
                                    " Where IfNull(H.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' "
                            Dgl1.AgHelpDataSet(Col1RateType) = AgL.FillData(mQry, AgL.GCn)
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

    Private Sub FrmSaleInvoice_BaseEvent_Topctrl_tbRef() Handles Me.BaseEvent_Topctrl_tbRef
        If Dgl1.AgHelpDataSet(Col1ItemCode) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1ItemCode).Dispose() : Dgl1.AgHelpDataSet(Col1ItemCode) = Nothing
        If Dgl1.AgHelpDataSet(Col1Item) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1Item).Dispose() : Dgl1.AgHelpDataSet(Col1Item) = Nothing
        If Dgl1.AgHelpDataSet(Col1BillingType) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1BillingType).Dispose() : Dgl1.AgHelpDataSet(Col1BillingType) = Nothing
        If TxtCurrency.AgHelpDataSet IsNot Nothing Then TxtCurrency.AgHelpDataSet.Dispose() : TxtCurrency.AgHelpDataSet = Nothing
        If TxtVendor.AgHelpDataSet IsNot Nothing Then TxtVendor.AgHelpDataSet.Dispose() : TxtVendor.AgHelpDataSet = Nothing
        If TxtSalesTaxGroupParty.AgHelpDataSet IsNot Nothing Then TxtSalesTaxGroupParty.AgHelpDataSet.Dispose() : TxtSalesTaxGroupParty.AgHelpDataSet = Nothing
        If TxtShipToParty.AgHelpDataSet IsNot Nothing Then TxtShipToParty.AgHelpDataSet.Dispose() : TxtShipToParty.AgHelpDataSet = Nothing
        If TxtReferenceParty.AgHelpDataSet IsNot Nothing Then TxtReferenceParty.AgHelpDataSet.Dispose() : TxtReferenceParty.AgHelpDataSet = Nothing
        If TxtAgent.AgHelpDataSet IsNot Nothing Then TxtAgent.AgHelpDataSet.Dispose() : TxtAgent.AgHelpDataSet = Nothing
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

    Private Sub FGetDeliveryMeasureMultiplier(ByVal mRow As Integer)
        Dim DtTemp As DataTable = Nothing
        Dim I As Integer = 0
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
                    If Dgl1.Item(Col1MeasureUnit, mRow).Value = Dgl1.Item(Col1DeliveryMeasure, I).Value Then
                        Dgl1.Item(Col1DeliveryMeasureMultiplier, mRow).Value = 1
                        Dgl1.Item(Col1DeliveryMeasureDecimalPlaces, mRow).Value = Dgl1.Item(Col1MeasureDecimalPlaces, mRow).Value
                    Else
                        mQry = " SELECT Multiplier, Rounding FROM UnitConversion WHERE FromUnit = '" & Dgl1.Item(Col1MeasureUnit, mRow).Value & "' AND ToUnit =  '" & Dgl1.Item(Col1DeliveryMeasure, mRow).Value & "' "
                        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
                        With DtTemp
                            If .Rows.Count > 0 Then
                                Dgl1.Item(Col1DeliveryMeasureMultiplier, mRow).Value = AgL.VNull(.Rows(0)("Multiplier"))
                                If Dgl1.Item(Col1DeliveryMeasureMultiplier, mRow).Value = 0 Then
                                    MsgBox("Define Multiplier In Unit Conversion To Convert " & Dgl1.Item(Col1DeliveryMeasure, mRow).Value & " From " & Dgl1.Item(Col1MeasureUnit, mRow).Value & " ", MsgBoxStyle.Information)
                                    Dgl1.Item(Col1DeliveryMeasure, mRow).Value = ""
                                Else
                                    mQry = " Select DecimalPlaces From Unit Where Code = '" & Dgl1.Item(Col1DeliveryMeasure, mRow).Value & "'"
                                    Dgl1.Item(Col1DeliveryMeasureDecimalPlaces, mRow).Value = AgL.VNull(AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar)
                                End If
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

    Private Sub ChkDeliveryDetailNotRequired_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkDeliveryDetailNotRequired.Click
        Dim I As Integer = 0, J As Integer = 0

        If ChkDeliveryDetailNotRequired.Checked Then
            TxtDeliveryDate.Text = ""
            TxtDeliveryDate.Enabled = False
            For I = 0 To Dgl1.Rows.Count - 1
                If Dgl1.Item(Col1BtnDeliveryDetail, I).Tag IsNot Nothing Then
                    For J = 0 To Dgl1.Item(Col1BtnDeliveryDetail, I).Tag.Dgl1.Rows.Count - 1
                        If Val(Dgl1.Item(Col1BtnDeliveryDetail, I).Tag.Dgl1.Item(FrmPurchOrderDelivery.Col1Qty, J).Value) <> 0 Then
                            Dgl1.Item(Col1BtnDeliveryDetail, I).Tag.Dgl1.Item(FrmPurchOrderDelivery.Col1DeliveryDate, J).Value = ""
                        End If
                    Next
                End If
            Next
        Else
            TxtDeliveryDate.Enabled = True
        End If
    End Sub

    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnFillPendingQuotation.Click
        Dim strTicked As String

        If AgL.StrCmp(Topctrl1.Mode, "Browse") Then Exit Sub
        If mIsEntryLocked Then Exit Sub

        If RbtOrderForQuotation.Checked Then
            strTicked = FHPGD_PendingPurchQuotation()
            If strTicked <> "" Then
                ProcFillPurchQuotationDetails(strTicked)
            End If
        ElseIf RbtOrderForIndent.Checked Then
            strTicked = FHPGD_PendingPurchIndent()
            If strTicked <> "" Then
                ProcFillPurchIndentDetails(strTicked)
            End If
        End If
    End Sub

    Private Function FHPGD_PendingPurchQuotation() As String
        Dim FRH_Multiple As DMHelpGrid.FrmHelpGrid_Multi
        Dim StrSendText As String
        Dim StrRtn As String = ""

        StrSendText = RbtOrderForQuotation.Tag

        mQry = " SELECT 'o' As Tick, L.PurchQuotation, Max(H.V_Type) || '-' ||  Max(H.ReferenceNo) AS QuotationNo, " &
                " Max(H.V_Date) as QuotationDate " &
                " FROM ( " &
                " 	SELECT DocID, V_Type, ReferenceNo, V_Date  " &
                " 	FROM PurchQuotation   " &
                " 	WHERE Vendor = '" & TxtVendor.Tag & "' " &
                " 	AND Div_Code = '" & TxtDivision.Tag & "' " &
                " 	AND Site_Code = '" & TxtSite_Code.Tag & "' " &
                " 	AND V_Date <= '" & TxtV_Date.Text & "') AS H " &
                " LEFT JOIN PurchQuotationDetail L  ON H.DocId = L.DocId " &
                " LEFT JOIN Item I  ON L.Item = I.Code " &
                " LEFT JOIN Voucher_Type Vt  ON H.V_Type = Vt.V_Type " &
                " LEFT JOIN ( " &
                " 	SELECT L.PurchQuotation, L.PurchQuotationSr, Sum(L.Qty) AS Qty " &
                " 	FROM PurchOrderDetail L   " &
                "   Where L.DocId <> '" & mSearchCode & "'" &
                " 	GROUP BY L.PurchQuotation, L.PurchQuotationSr " &
                " ) AS Cd ON L.DocId = Cd.PurchQuotation AND L.Sr = Cd.PurchQuotationSr " &
                " WHERE L.Qty -  IfNull(Cd.Qty, 0) > 0  " &
                " GROUP BY L.PurchQuotation " &
                " ORDER BY QuotationDate "

        FRH_Multiple = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(AgL.FillData(mQry, AgL.GCn).TABLES(0)), "", 300, 400, , , False)
        FRH_Multiple.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple.FFormatColumn(1, , 0, , False)
        FRH_Multiple.FFormatColumn(2, "Quotation No.", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(3, "Quotation Date", 100, DataGridViewContentAlignment.MiddleLeft)

        FRH_Multiple.StartPosition = FormStartPosition.CenterScreen
        FRH_Multiple.ShowDialog()

        If FRH_Multiple.BytBtnValue = 0 Then
            StrRtn = FRH_Multiple.FFetchData(1, "'", "'", ",", True)
        End If
        FHPGD_PendingPurchQuotation = StrRtn

        FRH_Multiple = Nothing
    End Function

    Private Sub ProcFillPurchQuotationDetails(ByVal bPurchQuotStr As String)
        Dim DtTemp As DataTable = Nothing
        Dim bReferenceDocId$ = "", bConStr$ = ""
        Dim I As Integer = 0
        Try
            mQry = " SELECT Max(L.Item) As Item, Max(I.Description) as Item_Name, " &
                        " Max(I.ManualCode) as ItemManualCode, Max(H.V_Type) || '-' ||  Max(H.ReferenceNo) AS Quot_No,   " &
                        " Max(H.V_Date) as Quot_Date, Sum(L.Qty) - IfNull(Sum(Cd.Qty), 0) as [Bal.Qty],   " &
                        " Sum(L.TotalMeasure) - IfNull(Sum(Cd.TotalMeasure), 0) as [Bal.Measure],   " &
                        " Max(L.Unit) as Unit, Max(L.Rate) as Rate, Max(L.MeasureUnit) MeasureUnit,   " &
                        " Max(I.SalesTaxPostingGroup) SalesTaxGroupItem, L.PurchQuotation,  " &
                        " L.PurchQuotationSr As PurchQuotationSr,   " &
                        " Max(L.MeasurePerPcs) As MeasurePerPcs,   " &
                        " Max(U.DecimalPlaces) As QtyDecimalPlaces, Max(U1.DecimalPlaces) As MeasureDecimalPlaces   " &
                        " FROM (  " &
                        "    SELECT DocID, V_Type, ReferenceNo, V_Date   " &
                        "    FROM PurchQuotation     " &
                        "    WHERE DocID In (" & bPurchQuotStr & ")" &
                        "    ) H   " &
                        " LEFT JOIN PurchQuotationDetail L  ON H.DocID = L.DocId    " &
                        " Left Join Item I  On L.Item  = I.Code   " &
                        " LEFT JOIN Voucher_Type Vt  ON H.V_Type = Vt.V_Type    " &
                        " Left Join (   " &
                        "    SELECT L.PurchQuotation, L.PurchQuotationSr, Sum (L.Qty) AS Qty,  " &
                        "    Sum(L.TotalMeasure) as TotalMeasure    " &
                        "    FROM PurchOrderDetail  L     " &
                        "    Where L.DocId <> '" & mSearchCode & "'" &
                        "    GROUP BY L.PurchQuotation, L.PurchQuotationSr  " &
                        " ) AS CD ON L.DocId = CD.PurchQuotation AND L.Sr = CD.PurchQuotationSr   " &
                        " LEFT JOIN Unit U On L.Unit = U.Code   " &
                        " LEFT JOIN Unit U1 On L.MeasureUnit = U1.Code   " &
                        " WHERE L.Qty - IfNull(Cd.Qty, 0) > 0  " &
                        " GROUP BY L.PurchQuotation, L.PurchQuotationSr  " &
                        " ORDER BY Quot_Date "
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

            With DtTemp
                Dgl1.RowCount = 1
                Dgl1.Rows.Clear()
                If .Rows.Count > 0 Then
                    For I = 0 To .Rows.Count - 1
                        Dgl1.Rows.Add()
                        Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count - 1
                        Dgl1.Item(Col1PurchQuotation, I).Tag = AgL.XNull(.Rows(I)("PurchQuotation"))
                        Dgl1.Item(Col1PurchQuotation, I).Value = AgL.XNull(.Rows(I)("Quot_No"))
                        Dgl1.Item(Col1PurchQuotationSr, I).Value = AgL.XNull(.Rows(I)("PurchQuotationSr"))
                        Dgl1.Item(Col1ItemCode, I).Tag = AgL.XNull(.Rows(I)("Item"))
                        Dgl1.Item(Col1ItemCode, I).Tag = AgL.XNull(.Rows(I)("ItemManualCode"))
                        Dgl1.Item(Col1Item, I).Tag = AgL.XNull(.Rows(I)("Item"))
                        Dgl1.Item(Col1Item, I).Value = AgL.XNull(.Rows(I)("Item_Name"))
                        Dgl1.Item(Col1SalesTaxGroup, I).Tag = AgL.XNull(.Rows(I)("SalesTaxGroupItem"))
                        Dgl1.Item(Col1SalesTaxGroup, I).Value = AgL.XNull(.Rows(I)("SalesTaxGroupItem"))

                        Dgl1.Item(Col1Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                        Dgl1.Item(Col1QtyDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("QtyDecimalPlaces"))
                        Dgl1.Item(Col1Qty, I).Value = Format(AgL.VNull(.Rows(I)("Bal.Qty")), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))

                        Dgl1.Item(Col1MeasureDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("MeasureDecimalPlaces"))
                        Dgl1.Item(Col1MeasureUnit, I).Value = AgL.XNull(.Rows(I)("MeasureUnit"))
                        Dgl1.Item(Col1MeasurePerPcs, I).Value = Format(AgL.VNull(.Rows(I)("MeasurePerPcs")), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                        Dgl1.Item(Col1TotalMeasure, I).Value = Format(AgL.VNull(.Rows(I)("Bal.Measure")), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))

                        Dgl1.Item(Col1DeliveryMeasureMultiplier, I).Value = 1
                        Dgl1.Item(Col1DeliveryMeasure, I).Value = AgL.XNull(.Rows(I)("MeasureUnit"))

                        Dgl1.Item(Col1Rate, I).Value = Format(AgL.VNull(.Rows(I)("Rate")), "0.00")

                        FillDeliveryDetail(I, False)
                    Next I
                End If
            End With
            Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function FHPGD_PendingPurchIndent() As String
        Dim FRH_Multiple As DMHelpGrid.FrmHelpGrid_Multi
        Dim StrSendText As String
        Dim StrRtn As String = ""
        Dim ContraV_TypeCondStr As String = ""

        StrSendText = RbtOrderForIndent.Tag

        If DtV_TypeSettings.Rows.Count > 0 Then
            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ContraV_Type")) <> "" Then
                ContraV_TypeCondStr += " And CharIndex('|' || V_Type || '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ContraV_Type")) & "') > 0 "
            End If
        End If

        mQry = " SELECT 'o' As Tick, L.PurchIndent + Convert(Varchar,L.PurchIndentSr) as PurchIndent, " &
                " Max(H.V_Type) || '-' ||  Max(H.ManualRefNo) AS IndentNo, " &
                " Max(H.V_Date) as IndentDate, Max(I.Description) as Item, " &
                " Max(D1.Description) As " & ClsMain.FGetDimension1Caption() & ", " &
                " Max(D2.Description) As " & ClsMain.FGetDimension2Caption() & ", " &
                " Sum(L.IndentQty) as Qty " &
                " FROM ( " &
                " 	SELECT DocID, V_Type, ManualRefNo, V_Date  " &
                " 	FROM PurchIndent   " &
                " 	WHERE Div_Code = '" & TxtDivision.Tag & "' " &
                " 	AND Site_Code = '" & TxtSite_Code.Tag & "' " &
                " 	AND V_Date <= '" & TxtV_Date.Text & "'" & ContraV_TypeCondStr &
                " ) As H LEFT JOIN PurchIndentDetail L  ON H.DocId = L.PurchIndent " &
                " LEFT JOIN Item I  ON L.Item = I.Code " &
                " Left Join Dimension1 D1 On L.Dimension1 = D1.Code " &
                " Left Join Dimension2 D2 On L.Dimension2 = D2.Code " &
                " LEFT JOIN Voucher_Type Vt  ON H.V_Type = Vt.V_Type " &
                " LEFT JOIN ( " &
                "   SELECT L.PurchIndent, L.PurchIndentSr, Sum(L.Qty) AS Qty  	" &
                "   FROM PurchOrderDetail L       	" &
                "   Where L.DocId <> '" & mSearchCode & "'" &
                "   GROUP BY L.PurchIndent, L.PurchIndentSr    	" &
                " ) AS Cd ON L.DocId = Cd.PurchIndent  AND L.Sr = Cd.PurchIndentSr " &
                " WHERE L.IndentQty > IfNull(Cd.Qty,0) " &
                " GROUP BY L.PurchIndent, L.PurchIndentSr " &
                " ORDER BY IndentDate, L.PurchIndentSr  "

        FRH_Multiple = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(AgL.FillData(mQry, AgL.GCn).TABLES(0)), "", 500, 900, , , False)
        FRH_Multiple.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple.FFormatColumn(1, , 0, , False)
        FRH_Multiple.FFormatColumn(2, "Indent No.", 150, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(3, "Indent Date", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(4, "Item Name", 200, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(5, ClsMain.FGetDimension1Caption(), 150, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(6, ClsMain.FGetDimension2Caption(), 150, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(7, "Indent Qty", 100, DataGridViewContentAlignment.MiddleRight)

        FRH_Multiple.StartPosition = FormStartPosition.CenterScreen
        FRH_Multiple.ShowDialog()

        If FRH_Multiple.BytBtnValue = 0 Then
            StrRtn = FRH_Multiple.FFetchData(1, "'", "'", ",", True)
        End If
        FHPGD_PendingPurchIndent = StrRtn

        FRH_Multiple = Nothing
    End Function

    Private Sub ProcFillPurchIndentDetails(ByVal bPurchIndentStr As String)
        Dim DtTemp As DataTable = Nothing
        Dim bReferenceDocId$ = "", bConStr$ = ""
        Dim I As Integer = 0
        Try
            mQry = " SELECT Max(L.Item) As Item, Max(I.Description) as Item_Name, " &
                        " Max(I.ManualCode) as ItemManualCode, Max(H.V_Type) || '-' ||  Max(H.ManualRefNo) AS Indent_No,   " &
                        " Max(H.V_Date) as Quot_Date, Sum(L.IndentQty) - IfNull(Sum(Cd.Qty), 0) as [Bal.Qty],   " &
                        " Sum(L.TotalIndentMeasure) - IfNull(Sum(Cd.TotalMeasure), 0) as [Bal.Measure],   " &
                        " Max(L.Unit) as Unit, Max(L.Rate) as Rate, Max(L.MeasureUnit) MeasureUnit,   " &
                        " Max(I.SalesTaxPostingGroup) SalesTaxGroupItem, L.PurchIndent,  " &
                        " Max(MP.ManualRefNo) As MaterialPlanNo, Max(L.MaterialPlan) As MaterialPlan, Max(L.MaterialPlanSr) as MaterialPlanSr, " &
                        " L.PurchIndentSr As PurchIndentSr, Max(I.Measure) As MeasurePerPcs, " &
                        " Max(D1.Description) As D1Desc, Max(D2.Description) As D2Desc, " &
                        " Max(L.Dimension1) As Dimension1, Max(L.Dimension2) As Dimension2, " &
                        " Max(U.DecimalPlaces) As QtyDecimalPlaces, Max(U1.DecimalPlaces) As MeasureDecimalPlaces   " &
                        " FROM (  " &
                        "    SELECT PurchIndent.DocID, PurchIndentDetail.Sr, V_Type, ManualRefNo, V_Date   " &
                        "    FROM PurchIndent     " &
                        "    Left Join PurchIndentDetail   On PurchIndent.DocID = PurchIndentDetail.DocID  " &
                        "    WHERE PurchIndent.DocID + Convert(Varchar,PurchIndentDetail.Sr) In (" & bPurchIndentStr & ")" &
                        "    ) H   " &
                        " LEFT JOIN PurchIndentDetail L  ON H.DocID = L.PurchIndent And H.Sr = L.PurchIndentSr " &
                        " LEFT JOIN MaterialPlan MP On L.MaterialPlan = MP.DocId " &
                        " Left Join Item I  On L.Item  = I.Code   " &
                        " Left Join Dimension1 D1 On L.Dimension1 = D1.Code " &
                        " Left Join Dimension2 D2 On L.Dimension2 = D2.Code " &
                        " LEFT JOIN Voucher_Type Vt  ON H.V_Type = Vt.V_Type    " &
                        " Left Join (   " &
                        "    SELECT L.PurchIndent, L.PurchIndentSr, Sum (L.Qty) AS Qty,  " &
                        "    Sum(L.TotalMeasure) as TotalMeasure    " &
                        "    FROM PurchOrderDetail  L     " &
                        "    Where L.DocId <> '" & mSearchCode & "'" &
                        "    GROUP BY L.PurchIndent, L.PurchIndentSr  " &
                        " ) AS CD ON L.DocId = CD.PurchIndent AND L.Sr = CD.PurchIndentSr   " &
                        " LEFT JOIN Unit U On L.Unit = U.Code   " &
                        " LEFT JOIN Unit U1 On L.MeasureUnit = U1.Code   " &
                        " WHERE L.IndentQty - IfNull(Cd.Qty, 0) > 0  " &
                        " GROUP BY L.PurchIndent, L.PurchIndentSr  " &
                        " ORDER BY Quot_Date "
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

            For I = 0 To Dgl1.Rows.Count - 2
                Dgl1.Rows(I).Visible = False
            Next

            With DtTemp
                'Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
                If .Rows.Count > 0 Then
                    For I = 0 To .Rows.Count - 1
                        Dgl1.Rows.Add()
                        Dgl1.Item(ColSNo, Dgl1.Rows.Count - 2).Value = Dgl1.Rows.Count - 1
                        Dgl1.Item(Col1PurchIndent, Dgl1.Rows.Count - 2).Tag = AgL.XNull(.Rows(I)("PurchIndent"))
                        Dgl1.Item(Col1PurchIndent, Dgl1.Rows.Count - 2).Value = AgL.XNull(.Rows(I)("Indent_No"))
                        Dgl1.Item(Col1PurchIndentSr, Dgl1.Rows.Count - 2).Value = AgL.XNull(.Rows(I)("PurchIndentSr"))
                        Dgl1.Item(Col1ItemCode, Dgl1.Rows.Count - 2).Tag = AgL.XNull(.Rows(I)("Item"))
                        Dgl1.Item(Col1ItemCode, Dgl1.Rows.Count - 2).Tag = AgL.XNull(.Rows(I)("ItemManualCode"))

                        Dgl1.Item(Col1Item, Dgl1.Rows.Count - 2).Tag = AgL.XNull(.Rows(I)("Item"))
                        Dgl1.Item(Col1Item, Dgl1.Rows.Count - 2).Value = AgL.XNull(.Rows(I)("Item_Name"))

                        Dgl1.Item(Col1Dimension1, Dgl1.Rows.Count - 2).Tag = AgL.XNull(.Rows(I)("Dimension1"))
                        Dgl1.Item(Col1Dimension1, Dgl1.Rows.Count - 2).Value = AgL.XNull(.Rows(I)("D1Desc"))
                        Dgl1.Item(Col1Dimension2, Dgl1.Rows.Count - 2).Tag = AgL.XNull(.Rows(I)("Dimension2"))
                        Dgl1.Item(Col1Dimension2, Dgl1.Rows.Count - 2).Value = AgL.XNull(.Rows(I)("D2Desc"))

                        'Dgl1.Item(Col1MaterialPlan, Dgl1.Rows.Count - 2).Tag = AgL.XNull(.Rows(I)("MaterialPlan"))
                        'Dgl1.Item(Col1MaterialPlan, Dgl1.Rows.Count - 2).Value = AgL.XNull(.Rows(I)("MaterialPlanNo"))
                        'Dgl1.Item(Col1MaterialPlanSr, Dgl1.Rows.Count - 2).Value = AgL.XNull(.Rows(I)("MaterialPlanSr"))

                        Dgl1.Item(Col1SalesTaxGroup, Dgl1.Rows.Count - 2).Tag = AgL.XNull(.Rows(I)("SalesTaxGroupItem"))
                        Dgl1.Item(Col1SalesTaxGroup, Dgl1.Rows.Count - 2).Value = AgL.XNull(.Rows(I)("SalesTaxGroupItem"))

                        Dgl1.Item(Col1Unit, Dgl1.Rows.Count - 2).Value = AgL.XNull(.Rows(I)("Unit"))
                        Dgl1.Item(Col1QtyDecimalPlaces, Dgl1.Rows.Count - 2).Value = AgL.VNull(.Rows(I)("QtyDecimalPlaces"))
                        Dgl1.Item(Col1Qty, Dgl1.Rows.Count - 2).Value = Format(AgL.VNull(.Rows(I)("Bal.Qty")), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))

                        Dgl1.Item(Col1MeasureDecimalPlaces, Dgl1.Rows.Count - 2).Value = AgL.VNull(.Rows(I)("MeasureDecimalPlaces"))
                        Dgl1.Item(Col1MeasureUnit, Dgl1.Rows.Count - 2).Value = AgL.XNull(.Rows(I)("MeasureUnit"))
                        Dgl1.Item(Col1MeasurePerPcs, Dgl1.Rows.Count - 2).Value = Format(AgL.VNull(.Rows(I)("MeasurePerPcs")), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                        Dgl1.Item(Col1TotalMeasure, Dgl1.Rows.Count - 2).Value = Format(AgL.VNull(.Rows(I)("Bal.Measure")), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))

                        Dgl1.Item(Col1DeliveryMeasureMultiplier, Dgl1.Rows.Count - 2).Value = 1
                        Dgl1.Item(Col1DeliveryMeasure, Dgl1.Rows.Count - 2).Value = AgL.XNull(.Rows(I)("MeasureUnit"))

                        Dgl1.Item(Col1Rate, Dgl1.Rows.Count - 2).Value = Format(AgL.VNull(.Rows(I)("Rate")), "0.00")

                        FillDeliveryDetail(Dgl1.Rows.Count - 2, False)
                    Next I
                End If
            End With
            Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub RbtOrderDirect_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RbtOrderDirect.Click, RbtOrderForQuotation.Click, RbtOrderForIndent.Click
        If Dgl1.AgHelpDataSet(Col1Item) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1Item) = Nothing
        If Dgl1.AgHelpDataSet(Col1ItemCode) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1ItemCode) = Nothing

        'Select Case sender.Name
        '    Case RbtOrderDirect.Name
        '        BtnFillPendingQuotation.Enabled = False

        '    Case Else
        '        If mIsEntryLocked = False Then BtnFillPendingQuotation.Enabled = True
        'End Select
    End Sub

    Private Sub FrmPurchOrder_BaseEvent_Topctrl_tbEdit(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbEdit
        RbtOrderForIndent.Checked = True


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
                ContraV_TypeCondStr += " And CharIndex('|' || V_Type || '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ContraV_Type")) & "') > 0 "
            End If
        End If

        Select Case ColumnName
            Case Col1Item
                If RbtOrderForIndent.Checked Then
                    mQry = " SELECT Max(L.Item) As Code, Max(I.Description) as Description, " &
                            " Max(D1.Description) As " & ClsMain.FGetDimension1Caption() & ", " &
                            " Max(D2.Description) As " & ClsMain.FGetDimension2Caption() & ", " &
                            " Max(I.ManualCode) as ManualCode, Max(H.V_Type) || '-' ||  Max(H.ManualRefNo) AS Indent_No, " &
                            " Max(H.V_Date) as Indent_Date, Sum(L.IndentQty) - IfNull(Sum(Cd.Qty), 0) as [Bal.Qty],    " &
                            " Sum(L.TotalIndentMeasure) - IfNull(Sum(Cd.TotalMeasure), 0) as [Bal.Measure],    " &
                            " Max(L.Unit) as Unit, Max(L.Rate) as Rate, Max(L.MeasureUnit) MeasureUnit,   " &
                            " Max(I.SalesTaxPostingGroup) As SalesTaxPostingGroup, L.PurchIndent,  " &
                            " L.PurchIndentSr As PurchIndentSr, Max(I.Measure) As MeasurePerPcs,  " &
                            " Max(U.DecimalPlaces) As QtyDecimalPlaces, " &
                            " Max(U1.DecimalPlaces) As MeasureDecimalPlaces, " &
                            " Max(L.ProdOrder) As ProdOrder, Max(Po.ManualRefNo) As ProdOrderNo, " &
                            " Max(L.Dimension1) As Dimension1, Max(L.Dimension2) As Dimension2, " &
                            " '' As PurchQuotation, 0 As PurchQuotationSr, '' As Quot_No, Max(I.Specification) As Specification " &
                            " FROM ( " &
                            " 	SELECT DocID, V_Type, ManualRefNo, V_Date " &
                            "   FROM PurchIndent   " &
                            "   Where Div_Code = '" & AgL.PubDivCode & "' " &
                            "   And Site_Code = '" & AgL.PubSiteCode & "' " &
                            "   AND V_Date <= '" & TxtV_Date.Text & "' " & ContraV_TypeCondStr &
                            " ) H     " &
                            " LEFT JOIN PurchIndentDetail L  ON H.DocID = L.PurchIndent      " &
                            " Left Join Item I  On L.Item  = I.Code     " &
                            " Left Join Dimension1 D1 On L.Dimension1 = D1.Code " &
                            " Left Join Dimension2 D2 On L.Dimension2 = D2.Code " &
                            " LEFT JOIN ProdOrder Po On L.ProdOrder = Po.DocId " &
                            " LEFT JOIN Voucher_Type Vt  ON H.V_Type = Vt.V_Type      " &
                            " LEFT JOIN (  " &
                            " 	SELECT L1.PurchIndent, L1.PurchIndentSr, Sum (L.Qty) AS Qty,       " &
                            " 	Sum(L.TotalMeasure) as TotalMeasure " &
                            " 	FROM PurchOrderDetail  L    " &
                            "   LEFT Join PurchOrderDetail L1  ON L.PurchOrder = L1.DocID  AND L.PurchOrderSr = L1.Sr " &
                            "   Where L.DocID <> '" & mInternalCode & "'  " &
                            " 	GROUP BY L1.PurchIndent, L1.PurchIndentSr   " &
                            " ) AS CD ON L.DocId = CD.PurchIndent AND L.Sr = CD.PurchIndentSr   " &
                            " LEFT JOIN Unit U On L.Unit = U.Code     " &
                            " LEFT JOIN Unit U1 On L.MeasureUnit = U1.Code " &
                            " WHERE 1=1 " & strCond &
                            " GROUP BY L.PurchIndent, L.PurchIndentSr " &
                            " HAVING Sum(L.IndentQty) - IfNull(Sum(Cd.Qty), 0) > 0 " &
                            " ORDER BY Indent_Date  "
                    Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex, 17) = AgL.FillData(mQry, AgL.GCn)
                ElseIf RbtOrderForQuotation.Checked Then
                    mQry = " SELECT Max(L.Item) As Code, Max(I.Description) as Description, " &
                            " Max(I.ManualCode) as ManualCode, Max(H.V_Type) || '-' ||  Max(H.ReferenceNo) AS Quot_No,   " &
                            " Max(H.V_Date) as Quot_Date, Sum(L.Qty) - IfNull(Sum(Cd.Qty), 0) as [Bal.Qty],   " &
                            " Sum(L.TotalMeasure) - IfNull(Sum(Cd.TotalMeasure), 0) as [Bal.Measure],   " &
                            " Max(L.Unit) as Unit, Max(L.Rate) as Rate, Max(L.MeasureUnit) MeasureUnit,   " &
                            " Max(I.SalesTaxPostingGroup) As SalesTaxPostingGroup, L.PurchQuotation,  " &
                            " L.PurchQuotationSr As PurchQuotationSr,   " &
                            " Max(L.MeasurePerPcs) As MeasurePerPcs,   " &
                            " Max(U.DecimalPlaces) As QtyDecimalPlaces, Max(U1.DecimalPlaces) As MeasureDecimalPlaces, " &
                            " Max(L.PurchIndent) As PurchIndent, Max(L.PurchIndentSr) As PurchIndentSr, " &
                            " Max(Pid.ProdOrder) As ProdOrder, Max(Po.ManualRefNo) As ProdOrderNo, " &
                            " Max(Pi.V_Type) || '-' ||  Max(Pi.ManualRefNo) AS Indent_No, Max(I.Specification) As Specification, " &
                            " Null As Dimension1, Null As " & ClsMain.FGetDimension1Caption() & ", Null As Dimension2, Null As " & ClsMain.FGetDimension2Caption() & " " &
                            " FROM (  " &
                            "    SELECT DocID, V_Type, ReferenceNo, V_Date   " &
                            "    FROM PurchQuotation     " &
                            "    Where Div_Code = '" & AgL.PubDivCode & "' And Site_Code = '" & AgL.PubSiteCode & "'" &
                            "    ) H   " &
                            " LEFT JOIN PurchQuotationDetail L  ON H.DocID = L.DocId    " &
                            " Left Join Item I  On L.Item  = I.Code   " &
                            " LEFT JOIN PurchIndent Pi On L.PurchIndent = Pi.DocId " &
                            " LEFT JOIN PurchIndentDetail Pid On L.PurchIndent = Pid.DocId And L.PurchIndentSr = Pid.Sr " &
                            " LEFT JOIN ProdOrder Po On Pid.ProdOrder = Po.DocId " &
                            " LEFT JOIN Voucher_Type Vt  ON H.V_Type = Vt.V_Type    " &
                            " Left Join (   " &
                            "    SELECT L.PurchQuotation, L.PurchQuotationSr, Sum (L.Qty) AS Qty,  " &
                            "    Sum(L.TotalMeasure) as TotalMeasure    " &
                            "    FROM PurchOrderDetail  L     " &
                            "    GROUP BY L.PurchQuotation, L.PurchQuotationSr  " &
                            " ) AS CD ON L.DocId = CD.PurchQuotation AND L.Sr = CD.PurchQuotationSr   " &
                            " LEFT JOIN Unit U On L.Unit = U.Code   " &
                            " LEFT JOIN Unit U1 On L.MeasureUnit = U1.Code   " &
                            " WHERE L.Qty - IfNull(Cd.Qty, 0) > 0  " & strCond &
                            " GROUP BY L.PurchQuotation, L.PurchQuotationSr  " &
                            " ORDER BY Quot_Date "
                    Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex, 19) = AgL.FillData(mQry, AgL.GCn)
                Else
                    mQry = "SELECT I.Code, I.Description, I.ManualCode, I.Unit, I.SalesTaxPostingGroup, I.Measure As MeasurePerPcs, " &
                          " I.MeasureUnit, I.Rate, " &
                          " U.DecimalPlaces As QtyDecimalPlaces, U1.DecimalPlaces As MeasureDecimalPlaces, " &
                          " '' As Quot_No, '' As [Bal.Qty], '' As [Bal.Measure], '' As PurchQuotation, " &
                          " '' As PurchQuotationSr, '' As Rate, '' As PurchIndent, 0 As PurchIndentSr, '' As Indent_No, " &
                          " '' As ProdOrder, '' As ProdOrderNo, I.Specification, " &
                          " Null As Dimension1, Null As " & ClsMain.FGetDimension1Caption() & ", Null As Dimension2, Null As " & ClsMain.FGetDimension2Caption() & " " &
                          " FROM Item I " &
                          " LEFT JOIN Unit U On I.Unit = U.Code " &
                          " LEFT JOIN Unit U1 On I.MeasureUnit = U1.Code " &
                          " Where IfNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
                    Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex, 22) = AgL.FillData(mQry, AgL.GCn)
                End If

            Case Col1ItemCode
                If RbtOrderForIndent.Checked Then
                    mQry = " SELECT Max(L.Item) As Code, Max(I.ManualCode) as ManualCode, Max(I.Description) as Description, " &
                            " Max(D1.Description) As " & ClsMain.FGetDimension1Caption() & ", " &
                            " Max(D2.Description) As " & ClsMain.FGetDimension2Caption() & ", " &
                            " Max(H.V_Type) || '-' ||  Max(H.ManualRefNo) AS Indent_No, " &
                            " Max(H.V_Date) as Indent_Date, Sum(L.IndentQty) - IfNull(Sum(Cd.Qty), 0) as [Bal.Qty],    " &
                            " Sum(L.TotalIndentMeasure) - IfNull(Sum(Cd.TotalMeasure), 0) as [Bal.Measure],    " &
                            " Max(L.Unit) as Unit, Max(L.Rate) as Rate, Max(L.MeasureUnit) MeasureUnit,   " &
                            " Max(I.SalesTaxPostingGroup) As SalesTaxPostingGroup, L.PurchIndent,  " &
                            " L.PurchIndentSr As PurchIndentSr, Max(L.MeasurePerPcs) As MeasurePerPcs,  " &
                            " Max(U.DecimalPlaces) As QtyDecimalPlaces, " &
                            " Max(U1.DecimalPlaces) As MeasureDecimalPlaces, " &
                            " Max(L.Dimension1) As Dimension1, Max(L.Dimension2) As Dimension2, " &
                            " Max(L.ProdOrder) As ProdOrder, Max(Po.ManualRefNo) As ProdOrderNo, " &
                            " '' As PurchQuotation, 0 As PurchQuotationSr, '' As Quot_No, Max(I.Specification) As Specification " &
                            " FROM ( " &
                            " 	SELECT DocID, V_Type, ManualRefNo, V_Date " &
                            "   FROM PurchIndent         " &
                            "   Where Div_Code = '" & AgL.PubDivCode & "' " &
                            "   And Site_Code = '" & AgL.PubSiteCode & "' " &
                            "   AND V_Date <= '" & TxtV_Date.Text & "' " & ContraV_TypeCondStr &
                            " ) H     " &
                            " LEFT JOIN PurchIndentDetail L  ON H.DocID = L.DocId      " &
                            " Left Join Item I  On L.Item  = I.Code     " &
                            " Left Join Dimension1 D1 On L.Dimension1 = D1.Code " &
                            " Left Join Dimension2 D2 On L.Dimension2 = D2.Code " &
                            " LEFT JOIN ProdOrder Po On L.ProdOrder = Po.DocId " &
                            " LEFT JOIN Voucher_Type Vt  ON H.V_Type = Vt.V_Type      " &
                            " LEFT JOIN (        " &
                            " 	SELECT L.PurchIndent, L.PurchIndentSr, Sum (L.Qty) AS Qty,       " &
                            " 	Sum(L.TotalMeasure) as TotalMeasure " &
                            " 	FROM PurchOrderDetail  L          " &
                            " 	GROUP BY L.PurchIndent, L.PurchIndentSr   " &
                            " ) AS CD ON L.DocId = CD.PurchIndent AND L.Sr = CD.PurchIndentSr   " &
                            " LEFT JOIN Unit U On L.Unit = U.Code     " &
                            " LEFT JOIN Unit U1 On L.MeasureUnit = U1.Code " &
                            " WHERE 1=1 " & strCond &
                            " GROUP BY L.PurchIndent, L.PurchIndentSr    " &
                            " HAVING Sum(L.IndentQty) - IfNull(Sum(Cd.Qty), 0) > 0 " &
                            " ORDER BY Indent_Date  "
                    Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex, 17) = AgL.FillData(mQry, AgL.GCn)
                ElseIf RbtOrderForQuotation.Checked Then
                    mQry = " SELECT Max(L.Item) As Code, Max(I.ManualCode) as ManualCode, Max(I.Description) as Description, " &
                            " Max(H.V_Type) || '-' ||  Max(H.ReferenceNo) AS Quot_No,   " &
                            " Max(H.V_Date) as Quot_Date, Sum(L.Qty) - IfNull(Sum(Cd.Qty), 0) as [Bal.Qty],   " &
                            " Sum(L.TotalMeasure) - IfNull(Sum(Cd.TotalMeasure), 0) as [Bal.Measure],   " &
                            " Max(L.Unit) as Unit, Max(L.Rate) as Rate, Max(L.MeasureUnit) MeasureUnit,   " &
                            " Max(I.SalesTaxPostingGroup) As SalesTaxPostingGroup, L.PurchQuotation,  " &
                            " L.PurchQuotationSr As PurchQuotationSr,   " &
                            " Max(L.MeasurePerPcs) As MeasurePerPcs,   " &
                            " Max(U.DecimalPlaces) As QtyDecimalPlaces, Max(U1.DecimalPlaces) As MeasureDecimalPlaces, " &
                            " Max(L.PurchIndent) As PurchIndent, Max(L.PurchIndentSr) As PurchIndentSr, " &
                            " Max(Pid.ProdOrder) As ProdOrder, Max(Po.ManualRefNo) As ProdOrderNo, " &
                            " Max(Pi.V_Type) || '-' ||  Max(Pi.ManualRefNo) AS Indent_No, Max(I.Specification) As Specification, " &
                            " Null As Dimension1, Null As " & ClsMain.FGetDimension1Caption() & ", Null As Dimension2, Null As " & ClsMain.FGetDimension2Caption() & " " &
                            " FROM (  " &
                            "    SELECT DocID, V_Type, ReferenceNo, V_Date   " &
                            "    FROM PurchQuotation     " &
                            "    Where Div_Code = '" & AgL.PubDivCode & "' And Site_Code = '" & AgL.PubSiteCode & "'" &
                            "    ) H   " &
                            " LEFT JOIN PurchQuotationDetail L  ON H.DocID = L.DocId    " &
                            " Left Join Item I  On L.Item  = I.Code   " &
                            " LEFT JOIN PurchIndent Pi On L.PurchIndent = Pi.DocId " &
                            " LEFT JOIN PurchIndentDetail Pid On L.PurchIndent = Pid.DocId And L.PurchIndentSr = Pid.Sr " &
                            " LEFT JOIN ProdOrder Po On Pid.ProdOrder = Po.DocId " &
                            " LEFT JOIN Voucher_Type Vt  ON H.V_Type = Vt.V_Type    " &
                            " Left Join (   " &
                            "    SELECT L.PurchQuotation, L.PurchQuotationSr, Sum (L.Qty) AS Qty,  " &
                            "    Sum(L.TotalMeasure) as TotalMeasure    " &
                            "    FROM PurchOrderDetail  L     " &
                            "    GROUP BY L.PurchQuotation, L.PurchQuotationSr  " &
                            " ) AS CD ON L.DocId = CD.PurchQuotation AND L.Sr = CD.PurchQuotationSr   " &
                            " LEFT JOIN Unit U On L.Unit = U.Code   " &
                            " LEFT JOIN Unit U1 On L.MeasureUnit = U1.Code   " &
                            " WHERE L.Qty - IfNull(Cd.Qty, 0) > 0  " & strCond &
                            " GROUP BY L.PurchQuotation, L.PurchQuotationSr  " &
                            " ORDER BY Quot_Date "
                    Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex, 19) = AgL.FillData(mQry, AgL.GCn)
                Else
                    mQry = "SELECT I.Code, I.ManualCode, I.Description, I.Unit, I.SalesTaxPostingGroup, I.Measure As MeasurePerPcs, " &
                          " I.MeasureUnit, I.Rate, " &
                          " U.DecimalPlaces As QtyDecimalPlaces, U1.DecimalPlaces As MeasureDecimalPlaces, " &
                          " '' As Quot_No, '' As [Bal.Qty], '' As [Bal.Measure], '' As PurchQuotation, " &
                          " '' As PurchQuotationSr, '' As Rate, '' As PurchIndent, 0 As PurchIndentSr, '' As Indent_No, " &
                          " '' As ProdOrder, '' As ProdOrderNo, I.Specification, " &
                          " Null As Dimension1, Null As " & ClsMain.FGetDimension1Caption() & ", Null As Dimension2, Null As " & ClsMain.FGetDimension2Caption() & " " &
                          " FROM Item I " &
                          " LEFT JOIN Unit U On I.Unit = U.Code " &
                          " LEFT JOIN Unit U1 On I.MeasureUnit = U1.Code " &
                          " Where IfNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
                    Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex, 22) = AgL.FillData(mQry, AgL.GCn)
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

        mQry = " SELECT H.SubCode, H.DispName || ',' || IfNull(C.CityName,'') AS [Party], " &
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

    'Private Sub FShowTransactionHistory(ByVal Item As String)
    '    Dim DtTemp As DataTable = Nothing
    '    Dim CSV_Qry As String = ""
    '    Dim CSV_QryArr() As String = Nothing
    '    Dim I As Integer, J As Integer
    '    Dim IGridWidth As Integer = 0
    '    Try
    '        mQry = " SELECT TOP 5 L.Item,H.V_Date AS [Purch_Date], Sg.DispName As Vendor, " & _
    '                    " L.Rate, L.Qty " & _
    '                    " FROM PurchInvoiceDetail L  " & _
    '                    " LEFT JOIN  PurchInvoice H ON L.DocId = H.DocId " & _
    '                    " LEFT JOIN SubGroup Sg ON H.Vendor = Sg.SubCode " & _
    '                    " Where L.Item = '" & Item & "'" & _
    '                    " And H.DocId <> '" & mSearchCode & "'" & _
    '                    " ORDER BY H.V_Date DESC "

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

    '            ' Dgl.Columns(0).Visible = False
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

    Private Sub FGenerateItemUid(ByVal Conn As SQLiteConnection, ByVal Cmd As SQLiteCommand)
        Dim bItemUidCode$ = ""
        Dim I As Integer = 0, J As Integer = 0, bGeneratedUid As Integer = 0, bMaxItemUid As Long = 0
        Dim mSr As Integer = 0
        Dim mItemCode$ = ""
        Dim mGenSr As Integer = 0
        Dim bItem_UidGenStartNo As Long = 50000000
        Dim bItem_UidGenEndNo As Long = 70000000

        Dim DtTemp As DataTable = Nothing
        mQry = " Select Sr, Item, Qty From PurchOrderDetail  Where DocId = '" & mSearchCode & "'"
        DtTemp = AgL.FillData(mQry, AgL.GcnRead).Tables(0)

        If DtTemp.Rows.Count > 0 Then
            For J = 0 To DtTemp.Rows.Count - 1
                mQry = " SELECT Count(I.Item_UID) FROM Item_UID I  " &
                        " WHERE I.GenDocID = '" & mSearchCode & "' And GenSr = " & AgL.XNull(DtTemp.Rows(J)("Sr")) & " "
                bGeneratedUid = AgL.VNull(AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar)

                If bGeneratedUid < AgL.VNull(DtTemp.Rows(J)("Qty")) Then
                    For I = 1 To AgL.VNull(DtTemp.Rows(J)("Qty")) - bGeneratedUid
                        bItemUidCode = AgL.GetMaxId("Item_UID", "Code", AgL.GcnRead, AgL.PubDivCode, AgL.PubSiteCode, 8, True, True, , AgL.Gcn_ConnectionString)
                        mSr = mSr + 1

                        mQry = " SELECT IfNull(Max(Convert(BIGINT,I.Item_UID)),0) FROM Item_UID I    Where Convert(BIGINT,I.Item_UID) > " & bItem_UidGenStartNo & " And Convert(BIGINT,I.Item_UID) < " & bItem_UidGenEndNo & "  "
                        bMaxItemUid = AgL.VNull(AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar)

                        If bMaxItemUid = 0 Then bMaxItemUid = bItem_UidGenStartNo

                        mQry = " INSERT INTO Item_UID (GenDocID, GenSr, Sr, Item, Code, Item_UID, SubCode, IsInStock) " &
                                " VALUES ('" & mSearchCode & "', " & AgL.XNull(DtTemp.Rows(J)("Sr")) & ", " & mSr & ", " &
                                " " & AgL.Chk_Text(AgL.XNull(DtTemp.Rows(J)("Item"))) & ", " &
                                " " & AgL.Chk_Text(bItemUidCode) & ", " & AgL.Chk_Text(bMaxItemUid + I) & ", " &
                                " " & AgL.Chk_Text(TxtVendor.AgSelectedValue) & ", 0)"
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                    Next
                ElseIf bGeneratedUid > AgL.VNull(DtTemp.Rows(J)("Qty")) Then
                    mQry = " DELETE FROM Item_UID " &
                            " WHERE Code IN (" &
                            "       SELECT TOP " & bGeneratedUid - AgL.VNull(DtTemp.Rows(I)("Qty")) & " Code FROM Item_UID  " &
                            "       WHERE GenDocID = '" & mSearchCode & "' " &
                            "       And Item = '" & AgL.XNull(DtTemp.Rows(J)("Item")) & "' And RecDocID is Null " &
                            "       Order BY Code Desc)"
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                End If
            Next
        End If
    End Sub

    Private Sub BtnBarCodeFill_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPrintBarcode.Click
        If Not AgL.StrCmp(Topctrl1.Mode, "Browse") Then Exit Sub
        Dim FrmObj As New FrmBarCodePrint
        FrmObj.mSearchCode = mSearchCode
        FrmObj.ShowDialog()
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

    Private Sub FrmPurchOrder_BaseEvent_Topctrl_tbDel(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbDel
        If mIsEntryLocked Then
            MsgBox("Referential data exist. Can't delete record.")
            Passed = False
        End If
    End Sub
End Class

