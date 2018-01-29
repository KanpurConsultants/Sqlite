Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.IO
Public Class FrmWorkOrderX
    Inherits AgTemplate.TempTransaction
    Dim mQry$

    Public WithEvents AgCalcGrid1 As New AgStructure.AgCalcGrid
    Public WithEvents AgCustomGrid1 As New AgCustomFields.AgCustomGrid

    Public WithEvents Dgl1 As AgControls.AgDataGrid
    Protected Const ColSNo As String = "S.No."
    Protected Const Col1ItemCode As String = "Item Code"
    Protected Const Col1Item As String = "Item"
    Protected Const Col1Specification As String = "Specification"
    Protected Const Col1PartySKU As String = "Party SKU"
    Protected Const Col1PartyUPC As String = "Party UPC"
    Protected Const Col1PartySpecification As String = "Party Specification"
    Protected Const Col1XPartySKU As String = "XPartySKU"
    Protected Const Col1XPartyUPC As String = "XPartyUPC"
    Protected Const Col1XPartySpecification As String = "XParty Specification"
    Protected Const Col1SalesTaxGroup As String = "Sales Tax Group"
    Protected Const Col1ItemRateGroup As String = "Item Rate Group"

    Protected Const Col1Qty As String = "Qty"
    Protected Const Col1Unit As String = "Unit"
    Protected Const Col1QtyDecimalPlaces As String = "Qty Decimal Places"

    Protected Const Col1MeasurePerPcs As String = "Measure Per Pcs"
    Protected Const Col1MeasureUnit As String = "Measure Unit"

    Protected Const Col1DeliveryMeasureUnit As String = "Delivery Measure Unit"
    Protected Const Col1DeliveryMeasureMultiplier As String = "Delivery Measure Multiplier"
    Protected Const Col1TotalDeliveryMeasure As String = "Total Delivery Measure"
    Protected Const Col1DeliveryMeasureDecimalPlaces As String = "Delivery Measure Decimal Places"


    Protected Const Col1Rate As String = "Rate"
    Protected Const Col1RatePerQty As String = "Rate Per Qty"
    Protected Const Col1Amount As String = "Amount"
    Protected Const Col1BtnDeliveryDetail As String = "Delivery Detail"
    Protected Const Col1Remark As String = "Remark"



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
        Me.TxtParty = New AgControls.AgTextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.TxtPartyOrderDate = New AgControls.AgTextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.TxtPartyOrderNo = New AgControls.AgTextBox
        Me.Label9 = New System.Windows.Forms.Label
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
        Me.TxtTermsAndConditions = New AgControls.AgTextBox
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
        Me.ChkDeliveryDetailNotRequired = New System.Windows.Forms.CheckBox
        Me.ChkDontLockRows = New System.Windows.Forms.CheckBox
        Me.LblTermsAndConditions = New System.Windows.Forms.LinkLabel
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
        Me.TPShipping.SuspendLayout()
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
        Me.GBoxDivision.Controls.Add(Me.TxtCustomFields)
        Me.GBoxDivision.Location = New System.Drawing.Point(300, 574)
        Me.GBoxDivision.Size = New System.Drawing.Size(114, 40)
        Me.GBoxDivision.Controls.SetChildIndex(Me.TxtCustomFields, 0)
        Me.GBoxDivision.Controls.SetChildIndex(Me.TxtDivision, 0)
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
        Me.Label2.Location = New System.Drawing.Point(112, 30)
        Me.Label2.Tag = ""
        '
        'LblV_Date
        '
        Me.LblV_Date.BackColor = System.Drawing.Color.Transparent
        Me.LblV_Date.Location = New System.Drawing.Point(16, 25)
        Me.LblV_Date.Size = New System.Drawing.Size(71, 16)
        Me.LblV_Date.Tag = ""
        Me.LblV_Date.Text = "Order Date"
        '
        'LblV_TypeReq
        '
        Me.LblV_TypeReq.Location = New System.Drawing.Point(326, 10)
        Me.LblV_TypeReq.Tag = ""
        '
        'TxtV_Date
        '
        Me.TxtV_Date.AgSelectedValue = ""
        Me.TxtV_Date.BackColor = System.Drawing.Color.White
        Me.TxtV_Date.Location = New System.Drawing.Point(128, 24)
        Me.TxtV_Date.TabIndex = 2
        Me.TxtV_Date.Tag = ""
        '
        'LblV_Type
        '
        Me.LblV_Type.Location = New System.Drawing.Point(234, 6)
        Me.LblV_Type.Size = New System.Drawing.Size(71, 16)
        Me.LblV_Type.Tag = ""
        Me.LblV_Type.Text = "Order Type"
        '
        'TxtV_Type
        '
        Me.TxtV_Type.AgSelectedValue = ""
        Me.TxtV_Type.BackColor = System.Drawing.Color.White
        Me.TxtV_Type.Location = New System.Drawing.Point(342, 4)
        Me.TxtV_Type.Size = New System.Drawing.Size(163, 18)
        Me.TxtV_Type.TabIndex = 1
        Me.TxtV_Type.Tag = ""
        '
        'LblSite_CodeReq
        '
        Me.LblSite_CodeReq.Location = New System.Drawing.Point(112, 10)
        Me.LblSite_CodeReq.Tag = ""
        '
        'LblSite_Code
        '
        Me.LblSite_Code.BackColor = System.Drawing.Color.Transparent
        Me.LblSite_Code.Location = New System.Drawing.Point(16, 5)
        Me.LblSite_Code.Size = New System.Drawing.Size(87, 16)
        Me.LblSite_Code.Tag = ""
        Me.LblSite_Code.Text = "Branch Name"
        '
        'TxtSite_Code
        '
        Me.TxtSite_Code.AgSelectedValue = ""
        Me.TxtSite_Code.BackColor = System.Drawing.Color.White
        Me.TxtSite_Code.Location = New System.Drawing.Point(128, 4)
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
        Me.TP1.Controls.Add(Me.ChkDeliveryDetailNotRequired)
        Me.TP1.Controls.Add(Me.TxtNature)
        Me.TP1.Controls.Add(Me.Label1)
        Me.TP1.Controls.Add(Me.TxtReferenceNo)
        Me.TP1.Controls.Add(Me.LblReferenceNo)
        Me.TP1.Controls.Add(Me.BtnFillPartyDetail)
        Me.TP1.Controls.Add(Me.TxtSalesTaxGroupParty)
        Me.TP1.Controls.Add(Me.Label27)
        Me.TP1.Controls.Add(Me.TxtStructure)
        Me.TP1.Controls.Add(Me.Label25)
        Me.TP1.Controls.Add(Me.TxtDeliveryDate)
        Me.TP1.Controls.Add(Me.Label11)
        Me.TP1.Controls.Add(Me.TxtPartyOrderDate)
        Me.TP1.Controls.Add(Me.Label3)
        Me.TP1.Controls.Add(Me.TxtPartyOrderNo)
        Me.TP1.Controls.Add(Me.Label9)
        Me.TP1.Controls.Add(Me.Label4)
        Me.TP1.Controls.Add(Me.TxtParty)
        Me.TP1.Controls.Add(Me.Label5)
        Me.TP1.Controls.Add(Me.TxtRemarks)
        Me.TP1.Controls.Add(Me.Label30)
        Me.TP1.Controls.Add(Me.TxtAgent)
        Me.TP1.Controls.Add(Me.LblAgent)
        Me.TP1.Location = New System.Drawing.Point(4, 22)
        Me.TP1.Size = New System.Drawing.Size(984, 109)
        Me.TP1.Text = "Document Detail"
        Me.TP1.Controls.SetChildIndex(Me.LblAgent, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtAgent, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label30, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtRemarks, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label5, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtParty, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label4, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label9, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtPartyOrderNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label3, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtPartyOrderDate, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label11, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtDeliveryDate, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label25, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtStructure, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label27, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtSalesTaxGroupParty, 0)
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
        Me.TP1.Controls.SetChildIndex(Me.BtnFillPartyDetail, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblReferenceNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtReferenceNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label1, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtNature, 0)
        Me.TP1.Controls.SetChildIndex(Me.ChkDeliveryDetailNotRequired, 0)
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
        Me.Label4.Location = New System.Drawing.Point(112, 51)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(10, 7)
        Me.Label4.TabIndex = 694
        Me.Label4.Text = "Ä"
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
        Me.TxtParty.Location = New System.Drawing.Point(128, 44)
        Me.TxtParty.MaxLength = 0
        Me.TxtParty.Name = "TxtParty"
        Me.TxtParty.Size = New System.Drawing.Size(348, 18)
        Me.TxtParty.TabIndex = 4
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(16, 44)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(39, 16)
        Me.Label5.TabIndex = 693
        Me.Label5.Text = "Party"
        '
        'TxtPartyOrderDate
        '
        Me.TxtPartyOrderDate.AgAllowUserToEnableMasterHelp = False
        Me.TxtPartyOrderDate.AgLastValueTag = Nothing
        Me.TxtPartyOrderDate.AgLastValueText = Nothing
        Me.TxtPartyOrderDate.AgMandatory = False
        Me.TxtPartyOrderDate.AgMasterHelp = True
        Me.TxtPartyOrderDate.AgNumberLeftPlaces = 8
        Me.TxtPartyOrderDate.AgNumberNegetiveAllow = False
        Me.TxtPartyOrderDate.AgNumberRightPlaces = 2
        Me.TxtPartyOrderDate.AgPickFromLastValue = False
        Me.TxtPartyOrderDate.AgRowFilter = ""
        Me.TxtPartyOrderDate.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtPartyOrderDate.AgSelectedValue = Nothing
        Me.TxtPartyOrderDate.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtPartyOrderDate.AgValueType = AgControls.AgTextBox.TxtValueType.Date_Value
        Me.TxtPartyOrderDate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtPartyOrderDate.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPartyOrderDate.Location = New System.Drawing.Point(342, 84)
        Me.TxtPartyOrderDate.MaxLength = 20
        Me.TxtPartyOrderDate.Name = "TxtPartyOrderDate"
        Me.TxtPartyOrderDate.Size = New System.Drawing.Size(163, 18)
        Me.TxtPartyOrderDate.TabIndex = 8
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(234, 85)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(96, 16)
        Me.Label3.TabIndex = 708
        Me.Label3.Text = "Party Order Dt."
        '
        'TxtPartyOrderNo
        '
        Me.TxtPartyOrderNo.AgAllowUserToEnableMasterHelp = False
        Me.TxtPartyOrderNo.AgLastValueTag = Nothing
        Me.TxtPartyOrderNo.AgLastValueText = Nothing
        Me.TxtPartyOrderNo.AgMandatory = False
        Me.TxtPartyOrderNo.AgMasterHelp = True
        Me.TxtPartyOrderNo.AgNumberLeftPlaces = 8
        Me.TxtPartyOrderNo.AgNumberNegetiveAllow = False
        Me.TxtPartyOrderNo.AgNumberRightPlaces = 2
        Me.TxtPartyOrderNo.AgPickFromLastValue = False
        Me.TxtPartyOrderNo.AgRowFilter = ""
        Me.TxtPartyOrderNo.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtPartyOrderNo.AgSelectedValue = Nothing
        Me.TxtPartyOrderNo.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtPartyOrderNo.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtPartyOrderNo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtPartyOrderNo.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPartyOrderNo.Location = New System.Drawing.Point(128, 84)
        Me.TxtPartyOrderNo.MaxLength = 20
        Me.TxtPartyOrderNo.Name = "TxtPartyOrderNo"
        Me.TxtPartyOrderNo.Size = New System.Drawing.Size(100, 18)
        Me.TxtPartyOrderNo.TabIndex = 7
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(16, 85)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(99, 16)
        Me.Label9.TabIndex = 706
        Me.Label9.Text = "Party Order No."
        '
        'TxtDeliveryDate
        '
        Me.TxtDeliveryDate.AgAllowUserToEnableMasterHelp = False
        Me.TxtDeliveryDate.AgLastValueTag = Nothing
        Me.TxtDeliveryDate.AgLastValueText = Nothing
        Me.TxtDeliveryDate.AgMandatory = False
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
        Me.TxtDeliveryDate.Location = New System.Drawing.Point(128, 64)
        Me.TxtDeliveryDate.MaxLength = 20
        Me.TxtDeliveryDate.Name = "TxtDeliveryDate"
        Me.TxtDeliveryDate.Size = New System.Drawing.Size(100, 18)
        Me.TxtDeliveryDate.TabIndex = 10
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(15, 66)
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
        Me.LblTotalDeliveryMeasure.Location = New System.Drawing.Point(706, 3)
        Me.LblTotalDeliveryMeasure.Name = "LblTotalDeliveryMeasure"
        Me.LblTotalDeliveryMeasure.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalDeliveryMeasure.TabIndex = 712
        Me.LblTotalDeliveryMeasure.Text = "."
        '
        'LblTotalDeliveryMeasureText
        '
        Me.LblTotalDeliveryMeasureText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalDeliveryMeasureText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalDeliveryMeasureText.Location = New System.Drawing.Point(484, 3)
        Me.LblTotalDeliveryMeasureText.Name = "LblTotalDeliveryMeasureText"
        Me.LblTotalDeliveryMeasureText.Size = New System.Drawing.Size(213, 22)
        Me.LblTotalDeliveryMeasureText.TabIndex = 711
        Me.LblTotalDeliveryMeasureText.Text = "Deilvery Measure :"
        Me.LblTotalDeliveryMeasureText.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'LblTotalMeasure
        '
        Me.LblTotalMeasure.AutoSize = True
        Me.LblTotalMeasure.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalMeasure.ForeColor = System.Drawing.Color.Black
        Me.LblTotalMeasure.Location = New System.Drawing.Point(401, 3)
        Me.LblTotalMeasure.Name = "LblTotalMeasure"
        Me.LblTotalMeasure.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalMeasure.TabIndex = 666
        Me.LblTotalMeasure.Text = "."
        Me.LblTotalMeasure.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTotalMeasureText
        '
        Me.LblTotalMeasureText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalMeasureText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalMeasureText.Location = New System.Drawing.Point(198, 3)
        Me.LblTotalMeasureText.Name = "LblTotalMeasureText"
        Me.LblTotalMeasureText.Size = New System.Drawing.Size(196, 22)
        Me.LblTotalMeasureText.TabIndex = 665
        Me.LblTotalMeasureText.Text = "Measure :"
        Me.LblTotalMeasureText.TextAlign = System.Drawing.ContentAlignment.TopRight
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
        Me.LblTotalQty.Location = New System.Drawing.Point(146, 3)
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
        Me.LblTotalQtyText.Location = New System.Drawing.Point(10, 3)
        Me.LblTotalQtyText.Name = "LblTotalQtyText"
        Me.LblTotalQtyText.Size = New System.Drawing.Size(124, 22)
        Me.LblTotalQtyText.TabIndex = 659
        Me.LblTotalQtyText.Text = "Qty :"
        Me.LblTotalQtyText.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Pnl1
        '
        Me.Pnl1.Location = New System.Drawing.Point(2, 200)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(978, 178)
        Me.Pnl1.TabIndex = 1
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
        Me.TxtTermsAndConditions.Location = New System.Drawing.Point(2, 446)
        Me.TxtTermsAndConditions.MaxLength = 0
        Me.TxtTermsAndConditions.Multiline = True
        Me.TxtTermsAndConditions.Name = "TxtTermsAndConditions"
        Me.TxtTermsAndConditions.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.TxtTermsAndConditions.Size = New System.Drawing.Size(284, 115)
        Me.TxtTermsAndConditions.TabIndex = 2
        '
        'PnlCalcGrid
        '
        Me.PnlCalcGrid.Location = New System.Drawing.Point(668, 403)
        Me.PnlCalcGrid.Name = "PnlCalcGrid"
        Me.PnlCalcGrid.Size = New System.Drawing.Size(307, 158)
        Me.PnlCalcGrid.TabIndex = 4
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
        Me.TxtSalesTaxGroupParty.Location = New System.Drawing.Point(342, 64)
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
        Me.Label27.Location = New System.Drawing.Point(234, 65)
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
        Me.TxtShipToParty.AgLastValueText = Nothing
        Me.TxtShipToParty.AgMandatory = False
        Me.TxtShipToParty.AgMasterHelp = False
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
        Me.TxtRemarks.Location = New System.Drawing.Point(580, 25)
        Me.TxtRemarks.MaxLength = 255
        Me.TxtRemarks.Multiline = True
        Me.TxtRemarks.Name = "TxtRemarks"
        Me.TxtRemarks.Size = New System.Drawing.Size(392, 56)
        Me.TxtRemarks.TabIndex = 13
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(514, 24)
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
        Me.TxtAgent.Location = New System.Drawing.Point(580, 5)
        Me.TxtAgent.MaxLength = 0
        Me.TxtAgent.Name = "TxtAgent"
        Me.TxtAgent.Size = New System.Drawing.Size(392, 18)
        Me.TxtAgent.TabIndex = 11
        '
        'LblAgent
        '
        Me.LblAgent.AutoSize = True
        Me.LblAgent.BackColor = System.Drawing.Color.Transparent
        Me.LblAgent.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblAgent.Location = New System.Drawing.Point(514, 5)
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
        Me.TxtCurrency.Location = New System.Drawing.Point(68, 403)
        Me.TxtCurrency.MaxLength = 20
        Me.TxtCurrency.Name = "TxtCurrency"
        Me.TxtCurrency.Size = New System.Drawing.Size(216, 18)
        Me.TxtCurrency.TabIndex = 5
        '
        'Label28
        '
        Me.Label28.AutoSize = True
        Me.Label28.BackColor = System.Drawing.Color.Transparent
        Me.Label28.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label28.Location = New System.Drawing.Point(2, 404)
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
        Me.BtnFillPartyDetail.Location = New System.Drawing.Point(479, 44)
        Me.BtnFillPartyDetail.Margin = New System.Windows.Forms.Padding(0)
        Me.BtnFillPartyDetail.Name = "BtnFillPartyDetail"
        Me.BtnFillPartyDetail.Size = New System.Drawing.Size(26, 20)
        Me.BtnFillPartyDetail.TabIndex = 1201
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
        Me.LinkLabel1.Location = New System.Drawing.Point(2, 179)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(207, 20)
        Me.LinkLabel1.TabIndex = 1004
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "Work Order For Following Items"
        Me.LinkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(326, 30)
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
        Me.TxtReferenceNo.Location = New System.Drawing.Point(342, 24)
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
        Me.LblReferenceNo.Location = New System.Drawing.Point(234, 24)
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
        Me.TxtCustomFields.Location = New System.Drawing.Point(71, 0)
        Me.TxtCustomFields.MaxLength = 20
        Me.TxtCustomFields.Name = "TxtCustomFields"
        Me.TxtCustomFields.Size = New System.Drawing.Size(72, 18)
        Me.TxtCustomFields.TabIndex = 1012
        Me.TxtCustomFields.Text = "AgTextBox1"
        Me.TxtCustomFields.Visible = False
        '
        'PnlCustomGrid
        '
        Me.PnlCustomGrid.Location = New System.Drawing.Point(290, 403)
        Me.PnlCustomGrid.Name = "PnlCustomGrid"
        Me.PnlCustomGrid.Size = New System.Drawing.Size(370, 158)
        Me.PnlCustomGrid.TabIndex = 3
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
        'ChkDeliveryDetailNotRequired
        '
        Me.ChkDeliveryDetailNotRequired.AutoSize = True
        Me.ChkDeliveryDetailNotRequired.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkDeliveryDetailNotRequired.Location = New System.Drawing.Point(760, 88)
        Me.ChkDeliveryDetailNotRequired.Name = "ChkDeliveryDetailNotRequired"
        Me.ChkDeliveryDetailNotRequired.Size = New System.Drawing.Size(211, 17)
        Me.ChkDeliveryDetailNotRequired.TabIndex = 9
        Me.ChkDeliveryDetailNotRequired.Text = "Delivery Detail Not Required"
        Me.ChkDeliveryDetailNotRequired.UseVisualStyleBackColor = True
        '
        'ChkDontLockRows
        '
        Me.ChkDontLockRows.AutoSize = True
        Me.ChkDontLockRows.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkDontLockRows.Location = New System.Drawing.Point(212, 180)
        Me.ChkDontLockRows.Name = "ChkDontLockRows"
        Me.ChkDontLockRows.Size = New System.Drawing.Size(131, 17)
        Me.ChkDontLockRows.TabIndex = 1208
        Me.ChkDontLockRows.Text = "Don't Lock Rows"
        Me.ChkDontLockRows.UseVisualStyleBackColor = True
        '
        'LblTermsAndConditions
        '
        Me.LblTermsAndConditions.BackColor = System.Drawing.Color.SteelBlue
        Me.LblTermsAndConditions.DisabledLinkColor = System.Drawing.Color.White
        Me.LblTermsAndConditions.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTermsAndConditions.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LblTermsAndConditions.LinkColor = System.Drawing.Color.White
        Me.LblTermsAndConditions.Location = New System.Drawing.Point(2, 424)
        Me.LblTermsAndConditions.Name = "LblTermsAndConditions"
        Me.LblTermsAndConditions.Size = New System.Drawing.Size(124, 20)
        Me.LblTermsAndConditions.TabIndex = 1209
        Me.LblTermsAndConditions.TabStop = True
        Me.LblTermsAndConditions.Text = "Terms && Conditions"
        Me.LblTermsAndConditions.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'FrmWorkOrder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.BackColor = System.Drawing.SystemColors.ButtonShadow
        Me.ClientSize = New System.Drawing.Size(984, 618)
        Me.Controls.Add(Me.LblTermsAndConditions)
        Me.Controls.Add(Me.ChkDontLockRows)
        Me.Controls.Add(Me.PnlCustomGrid)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.TxtCurrency)
        Me.Controls.Add(Me.Label28)
        Me.Controls.Add(Me.PnlCalcGrid)
        Me.Controls.Add(Me.TxtTermsAndConditions)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Pnl1)
        Me.EntryNCat = "WO"
        Me.LogLineTableCsv = "WorkOrderDetail_LOG"
        Me.LogTableName = "WorkOrder_Log"
        Me.MainLineTableCsv = "WorkOrderDetail"
        Me.MainTableName = "WorkOrder"
        Me.Name = "FrmWorkOrder"
        Me.Text = "Work Order"
        Me.Controls.SetChildIndex(Me.Pnl1, 0)
        Me.Controls.SetChildIndex(Me.Panel1, 0)
        Me.Controls.SetChildIndex(Me.TxtTermsAndConditions, 0)
        Me.Controls.SetChildIndex(Me.PnlCalcGrid, 0)
        Me.Controls.SetChildIndex(Me.Topctrl1, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.GrpUP, 0)
        Me.Controls.SetChildIndex(Me.GBoxEntryType, 0)
        Me.Controls.SetChildIndex(Me.GBoxApprove, 0)
        Me.Controls.SetChildIndex(Me.GBoxMoveToLog, 0)
        Me.Controls.SetChildIndex(Me.Label28, 0)
        Me.Controls.SetChildIndex(Me.TxtCurrency, 0)
        Me.Controls.SetChildIndex(Me.GroupBox2, 0)
        Me.Controls.SetChildIndex(Me.GBoxDivision, 0)
        Me.Controls.SetChildIndex(Me.LinkLabel1, 0)
        Me.Controls.SetChildIndex(Me.TabControl1, 0)
        Me.Controls.SetChildIndex(Me.PnlCustomGrid, 0)
        Me.Controls.SetChildIndex(Me.ChkDontLockRows, 0)
        Me.Controls.SetChildIndex(Me.LblTermsAndConditions, 0)
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
        Me.TPShipping.ResumeLayout(False)
        Me.TPShipping.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Protected WithEvents Label5 As System.Windows.Forms.Label
    Protected WithEvents TxtDeliveryDate As AgControls.AgTextBox
    Protected WithEvents Label11 As System.Windows.Forms.Label
    Protected WithEvents TxtPartyOrderDate As AgControls.AgTextBox
    Protected WithEvents Label3 As System.Windows.Forms.Label
    Protected WithEvents TxtPartyOrderNo As AgControls.AgTextBox
    Protected WithEvents Label9 As System.Windows.Forms.Label
    Protected WithEvents TxtParty As AgControls.AgTextBox
    Protected WithEvents Label4 As System.Windows.Forms.Label
    Protected WithEvents Panel1 As System.Windows.Forms.Panel
    Protected WithEvents LblTotalQty As System.Windows.Forms.Label
    Protected WithEvents LblTotalQtyText As System.Windows.Forms.Label
    Protected WithEvents Pnl1 As System.Windows.Forms.Panel
    Protected WithEvents TxtTermsAndConditions As AgControls.AgTextBox
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
    Protected WithEvents ChkDeliveryDetailNotRequired As System.Windows.Forms.CheckBox
    Protected WithEvents ChkDontLockRows As System.Windows.Forms.CheckBox
    Protected WithEvents LblTermsAndConditions As System.Windows.Forms.LinkLabel
#End Region

    Private Sub FPostInBuyerSku(ByVal SearchCode As String, ByVal Conn As System.Data.SqlClient.SqlConnection, ByVal Cmd As System.Data.SqlClient.SqlCommand)
        Dim I As Integer
        Dim mSr As Integer

        '------------------------------------------------------------------------
        'Updating Buyer Wise Item SKU and UPC (Universal Product Code)
        '-------------------------------------------------------------------------
        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1Item, I).Value <> "" And (Dgl1.Item(Col1PartySKU, I).Value <> "" Or Dgl1.Item(Col1PartyUPC, I).Value <> "" Or Dgl1.Item(Col1PartySpecification, I).Value <> "") Then
                If (Not AgL.StrCmp(Dgl1.Item(Col1PartySKU, I).Value, Dgl1.Item(Col1XPartySKU, I).Value)) Or (Not AgL.StrCmp(Dgl1.Item(Col1PartyUPC, I).Value, Dgl1.Item(Col1XPartyUPC, I).Value)) Or (Not AgL.StrCmp(Dgl1.Item(Col1PartySpecification, I).Value, Dgl1.Item(Col1XPartySpecification, I).Value)) Then
                    If AgL.VNull(AgL.Dman_Execute("Select Count(*) From ItemBuyer With (NoLock) Where Code = '" & Dgl1.Item(Col1Item, I).Tag & "' And Buyer = '" & TxtParty.Tag & "'", AgL.GcnRead).ExecuteScalar) = 0 Then
                        mQry = "Select IsNull(Max(Sr),0)+1 From ItemBuyer With (NoLock) Where Code = '" & Dgl1.Item(Col1Item, I).Tag & "'"
                        mSr = AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar

                        mQry = "INSERT INTO ItemBuyer (Code, Sr, Buyer, BuyerSku, BuyerUpcCode, BuyerSpecification) " & _
                               " VALUES (" & AgL.Chk_Text(Dgl1.Item(Col1Item, I).Tag) & ", " & mSr & ", " & _
                               " " & AgL.Chk_Text(TxtParty.Tag) & ", " & AgL.Chk_Text(Dgl1.Item(Col1PartySKU, I).Value) & ", " & _
                               " " & AgL.Chk_Text(Dgl1.Item(Col1PartyUPC, I).Value) & ", " & _
                               " " & AgL.Chk_Text(Dgl1.Item(Col1PartySpecification, I).Value) & ") "
                    Else
                        mQry = "UPDATE ItemBuyer " & _
                               " SET BuyerSku = " & AgL.Chk_Text(Dgl1.Item(Col1PartySKU, I).Value) & ", " & _
                               " BuyerUpcCode =" & AgL.Chk_Text(Dgl1.Item(Col1PartyUPC, I).Value) & ", " & _
                               " BuyerSpecification =" & AgL.Chk_Text(Dgl1.Item(Col1PartySpecification, I).Value) & " " & _
                               " Where Code = '" & Dgl1.Item(Col1Item, I).Tag & "' " & _
                               " And Buyer = '" & TxtParty.Tag & "'"
                    End If
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                End If
            End If
        Next
        '-------------------------------------------------------------------------
    End Sub

    Private Sub FrmQuality1_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "WorkOrder"
        LogTableName = "WorkOrder_Log"
        MainLineTableCsv = "WorkOrderDetail,WorkOrderDeliveryDetail"
        LogLineTableCsv = "WorkOrderDetail_LOG,WorkOrderDeliveryDetail_Log"

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
            " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "H.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        mQry = "Select DocID As SearchCode " & _
                " From WorkOrder H " & _
                " Left Join Voucher_Type Vt On H.V_Type = Vt.V_Type  " & _
                " Where 1=1  " & mCondStr & "  Order By V_Date Desc "
        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub FrmWorkOrder_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mCondStr$

        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) & _
                        " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "H.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        AgL.PubFindQry = " SELECT H.DocID, H.V_Type, H.V_Date As [Order Date], H.ReferenceNo AS [ORDER No], H.PartyName AS [Party Name], isnull(H.PartyAdd1,'') + isnull(H.PartyAdd2,'') + isnull(H.PartyCityName,'') AS [Party Address], " & _
                " H.ShipToPartyName AS [Ship To Party], H.Currency, H.SalesTaxGroupParty, H.PartyOrderNo AS [Party Order No], H.PartyOrderDate AS [Party Order Date] , " & _
                " H.OrderType, H.Remarks, SG.DispName AS [Agent Name]   " & _
                " FROM WorkOrder H " & _
                " LEFT JOIN SubGroup SG ON SG.SubCode = H.Agent  " & _
                " LEFT JOIN voucher_type Vt ON H.V_Type = vt.V_Type " & _
                " Where 1=1 " & mCondStr

        AgL.PubFindQryOrdBy = "[Order Date]"
    End Sub

    Private Sub FrmWorkOrder_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl1, Col1ItemCode, 100, 0, Col1ItemCode, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_ItemCode")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_ItemCode")), Boolean), False)
            .AddAgTextColumn(Dgl1, Col1Item, 150, 0, Col1Item, True, Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_ItemName")), Boolean), False)
            .AddAgTextColumn(Dgl1, Col1Specification, 100, 255, Col1Specification, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Specification")), Boolean), False, False)
            .AddAgTextColumn(Dgl1, Col1PartySKU, 110, 50, Col1PartySKU, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_PartySKU")), Boolean), False, False)
            .AddAgTextColumn(Dgl1, Col1PartyUPC, 110, 20, Col1PartyUPC, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_PartyUPC")), Boolean), False, False)
            .AddAgTextColumn(Dgl1, Col1PartySpecification, 110, 20, Col1PartySpecification, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_PartySpecification")), Boolean), False, False)
            .AddAgTextColumn(Dgl1, Col1XPartySKU, 270, 50, Col1XPartySKU, False, False, False)
            .AddAgTextColumn(Dgl1, Col1XPartyUPC, 270, 50, Col1XPartyUPC, False, False, False)
            .AddAgTextColumn(Dgl1, Col1XPartySpecification, 270, 50, Col1XPartySpecification, False, False, False)
            .AddAgTextColumn(Dgl1, Col1ItemRateGroup, 100, 50, Col1ItemRateGroup, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_RateType")), Boolean), False, False)

            .AddAgTextColumn(Dgl1, Col1MeasureUnit, 50, 0, Col1MeasureUnit, True, True, False)
            .AddAgNumberColumn(Dgl1, Col1MeasurePerPcs, 50, 8, 4, False, Col1MeasurePerPcs, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_MeasurePerPcs")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_MeasurePerPcs")), Boolean), True)


            .AddAgTextColumn(Dgl1, Col1DeliveryMeasureUnit, 70, 50, Col1DeliveryMeasureUnit, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_MeasureUnit")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_MeasureUnit")), Boolean), False, False)
            .AddAgNumberColumn(Dgl1, Col1DeliveryMeasureMultiplier, 100, 8, 4, False, Col1DeliveryMeasureMultiplier, True, True, True)
            .AddAgNumberColumn(Dgl1, Col1TotalDeliveryMeasure, 85, 8, 4, False, Col1TotalDeliveryMeasure, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Measure")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_Measure")), Boolean), True)
            .AddAgTextColumn(Dgl1, Col1DeliveryMeasureDecimalPlaces, 50, 0, Col1DeliveryMeasureDecimalPlaces, False, True, False)

            .AddAgNumberColumn(Dgl1, Col1Qty, 70, 8, 0, False, Col1Qty, True, False, True)
            .AddAgTextColumn(Dgl1, Col1Unit, 50, 0, Col1Unit, True, True, False)
            .AddAgTextColumn(Dgl1, Col1QtyDecimalPlaces, 50, 0, Col1QtyDecimalPlaces, False, True, False)


            .AddAgNumberColumn(Dgl1, Col1Rate, 70, 8, 2, False, Col1Rate, True, False, True)
            .AddAgNumberColumn(Dgl1, Col1Amount, 70, 8, 2, False, Col1Amount, True, True, True)
            .AddAgButtonColumn(Dgl1, Col1BtnDeliveryDetail, 60, Col1BtnDeliveryDetail, True, False)
            .AddAgTextColumn(Dgl1, Col1Remark, 70, 255, Col1Remark, True, False, False)

            .AddAgTextColumn(Dgl1, Col1SalesTaxGroup, 70, 0, Col1SalesTaxGroup, False, False, False)
            .AddAgNumberColumn(Dgl1, Col1RatePerQty, 100, 8, 2, False, Col1RatePerQty, False, True, True)

        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.AgSkipReadOnlyColumns = True
        Dgl1.ColumnHeadersHeight = 35
        Dgl1.AllowUserToOrderColumns = True


        AgCalcGrid1.Name = "AgCalcGrid1"
        AgCalcGrid1.Ini_Grid(LblV_Type.Tag, TxtV_Date.Text)

        AgCalcGrid1.AgLineGrid = Dgl1
        AgCalcGrid1.AgLineGridMandatoryColumn = Dgl1.Columns(Col1Item).Index
        AgCalcGrid1.AgLineGridGrossColumn = Dgl1.Columns(Col1Amount).Index
        AgCalcGrid1.AgLineGridPostingGroupSalesTaxProd = Dgl1.Columns(Col1SalesTaxGroup).Index

        LblTotalMeasure.Visible = CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Measure")), Boolean)
        LblTotalDeliveryMeasureText.Visible = CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Measure")), Boolean)

        LblTotalMeasure.Visible = CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Measure")), Boolean)
        LblTotalDeliveryMeasureText.Visible = CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Measure")), Boolean)

        AgCustomGrid1.Name = "AgCustomGrid1"
        AgCustomGrid1.Ini_Grid(mSearchCode)
        AgCustomGrid1.SplitGrid = False

        AgCL.GridSetiingShowXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1)
        AgCL.GridSetiingShowXml(Me.Text & AgCalcGrid1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, AgCalcGrid1)
        AgCL.GridSetiingShowXml(Me.Text & AgCustomGrid1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, AgCustomGrid1)
    End Sub

    Private Sub FrmWorkOrder_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As System.Data.SqlClient.SqlConnection, ByVal Cmd As System.Data.SqlClient.SqlCommand) Handles Me.BaseEvent_Save_InTrans
        Dim I As Integer, mSr As Integer
        Dim bSelectionQry$ = "", bSelecttionLineQry$ = ""

        mQry = "UPDATE WorkOrder " & _
                "   SET " & _
                "   ReferenceNo = " & AgL.Chk_Text(TxtReferenceNo.Text) & ", " & _
                "   Party = " & AgL.Chk_Text(TxtParty.Tag) & ", " & _
                "	Currency = " & AgL.Chk_Text(TxtCurrency.Tag) & ", " & _
                "	ShipToPartyName = " & AgL.Chk_Text(TxtShipToParty.Text) & ", " & _
                "	ShipToPartyAdd1 = " & AgL.Chk_Text(TxtShipToPartyAdd1.Text) & ", " & _
                "	ShipToPartyAdd2 = " & AgL.Chk_Text(TxtShipToPartyAdd2.Text) & ", " & _
                "	ShipToPartyCity = " & AgL.Chk_Text(TxtShipToPartyCity.Tag) & ", " & _
                "	ShipToPartyCityName = " & AgL.Chk_Text(TxtShipToPartyCity.Text) & ", " & _
                "	ShipToPartyState = " & AgL.Chk_Text(TxtShipToPartyState.Text) & ", " & _
                "	ShipToPartyCountry = " & AgL.Chk_Text(TxtShipToPartyCountry.Text) & ", " & _
                "	SalesTaxGroupParty = " & AgL.Chk_Text(TxtSalesTaxGroupParty.Tag) & ", " & _
                "	PartyOrderNo = " & AgL.Chk_Text(TxtPartyOrderNo.Text) & ", " & _
                "	PartyOrderDate = " & AgL.Chk_Text(TxtPartyOrderDate.Text) & ", " & _
                "	PartyDeliveryDate =" & AgL.Chk_Text(TxtDeliveryDate.Text) & ", " & _
                "	TermsAndConditions = " & AgL.Chk_Text(TxtTermsAndConditions.Text) & ", " & _
                "	Remarks = " & AgL.Chk_Text(TxtRemarks.Text) & ", " & _
                "	Structure = " & AgL.Chk_Text(TxtStructure.Tag) & ", " & _
                "   CustomFields = " & AgL.Chk_Text(TxtCustomFields.Tag) & ", " & _
                "   ReferenceParty = " & AgL.Chk_Text(TxtReferenceParty.Tag) & ", " & _
                "   ReferencePartyDocumentNo = " & AgL.Chk_Text(TxtReferencePartyDocumentNo.Text) & ", " & _
                "   ReferencePartyDocumentDate = " & AgL.Chk_Text(TxtReferencePartyDocumentDate.Text) & ", " & _
                "   Agent = " & AgL.Chk_Text(TxtAgent.Tag) & ", " & _
                "   PartyName = '" & BtnFillPartyDetail.Tag.TxtName.Text & "', " & _
                "   PartyAdd1 = '" & BtnFillPartyDetail.Tag.TxtAdd1.Text & "', " & _
                "   PartyAdd2 = '" & BtnFillPartyDetail.Tag.TxtAdd2.Text & "', " & _
                "   PartyCity = '" & BtnFillPartyDetail.Tag.TxtCity.Tag & "', " & _
                "   PartyCityName = '" & BtnFillPartyDetail.Tag.TxtCity.Text & "', " & _
                "   PartyMobile = '" & BtnFillPartyDetail.Tag.TxtMobile.Text & "', " & _
                "   " & AgCalcGrid1.FFooterTableUpdateStr() & " " & _
                "   " & AgCustomGrid1.FFooterTableUpdateStr() & " " & _
                "   Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mSr = AgL.VNull(AgL.Dman_Execute("Select Max(Sr) From WorkOrderDetail With (NoLock) Where DocID = '" & mSearchCode & "'", AgL.GcnRead).ExecuteScalar)
        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1Item, I).Value <> "" Then
                If Dgl1.Item(ColSNo, I).Tag Is Nothing And Dgl1.Rows(I).Visible = True Then
                    mSr += 1
                    If bSelectionQry <> "" Then bSelectionQry += " UNION ALL "
                    bSelectionQry += " Select " & AgL.Chk_Text(mSearchCode) & ", " & mSr & ", " & _
                            " " & AgL.Chk_Text(Dgl1.Item(Col1Item, I).Tag) & ", " & _
                            " " & AgL.Chk_Text(Dgl1.Item(Col1Specification, I).Value) & ", " & _
                            " " & AgL.Chk_Text(Dgl1.Item(Col1PartySKU, I).Value) & ", " & _
                            " " & AgL.Chk_Text(Dgl1.Item(Col1PartyUPC, I).Value) & ", " & _
                            " " & AgL.Chk_Text(Dgl1.Item(Col1PartySpecification, I).Value) & ", " & _
                            " " & AgL.Chk_Text(Dgl1.Item(Col1SalesTaxGroup, I).Value) & ", " & _
                            " " & AgL.Chk_Text(Dgl1.Item(Col1ItemRateGroup, I).Value) & ", " & _
                            " " & Val(Dgl1.Item(Col1Qty, I).Value) & ", " & _
                            " " & AgL.Chk_Text(Dgl1.Item(Col1Unit, I).Value) & ", " & _
                            " " & Val(Dgl1.Item(Col1Qty, I).Value) & ", " & _
                            " " & AgL.Chk_Text(Dgl1.Item(Col1Unit, I).Value) & ", " & _
                            " " & Val(Dgl1.Item(Col1MeasurePerPcs, I).Value) & ", " & _
                            " " & AgL.Chk_Text(Dgl1.Item(Col1MeasureUnit, I).Value) & ", " & _
                            " " & AgL.Chk_Text(Dgl1.Item(Col1DeliveryMeasureUnit, I).Value) & ", " & _
                            " " & Val(Dgl1.Item(Col1DeliveryMeasureMultiplier, I).Value) & ", " & _
                            " " & Val(Dgl1.Item(Col1TotalDeliveryMeasure, I).Value) & ", " & _
                            " " & Val(Dgl1.Item(Col1Rate, I).Value) & ", " & _
                            " " & Val(Dgl1.Item(Col1RatePerQty, I).Value) & ", " & _
                            " " & Val(Dgl1.Item(Col1Amount, I).Value) & ", " & _
                            " " & AgL.Chk_Text(Dgl1.Item(Col1Remark, I).Value) & ", " & _
                            " " & AgL.Chk_Text(mSearchCode) & ", " & mSr & ", " & _
                            " " & AgCalcGrid1.FLineTableFieldValuesStr(I) & " "
                    Call FGetLineQry(bSelecttionLineQry, Conn, Cmd, I, mSr)
                Else
                    If Dgl1.Rows(I).Visible = True Then
                        If Dgl1.Rows(I).DefaultCellStyle.BackColor <> RowLockedColour Then
                            mQry = " UPDATE WorkOrderDetail " & _
                                    " SET " & _
                                    " Item = " & AgL.Chk_Text(Dgl1.Item(Col1Item, I).Tag) & ", " & _
                                    " Specification = " & AgL.Chk_Text(Dgl1.Item(Col1Specification, I).Value) & ", " & _
                                    " PartySKU = " & AgL.Chk_Text(Dgl1.Item(Col1PartySKU, I).Value) & ", " & _
                                    " PartyUPC = " & AgL.Chk_Text(Dgl1.Item(Col1PartyUPC, I).Value) & ", " & _
                                    " PartySpecification = " & AgL.Chk_Text(Dgl1.Item(Col1PartySpecification, I).Value) & ", " & _
                                    " SalesTaxGroupItem = " & AgL.Chk_Text(Dgl1.Item(Col1SalesTaxGroup, I).Value) & ", " & _
                                    " RateType = " & AgL.Chk_Text(Dgl1.Item(Col1ItemRateGroup, I).Value) & ", " & _
                                    " Qty = " & Val(Dgl1.Item(Col1Qty, I).Value) & ", " & _
                                    " Unit = " & AgL.Chk_Text(Dgl1.Item(Col1Unit, I).Value) & ", " & _
                                    " Rate = " & Val(Dgl1.Item(Col1Rate, I).Value) & ", " & _
                                    " RatePerQty = " & Val(Dgl1.Item(Col1RatePerQty, I).Value) & ", " & _
                                    " Amount = " & Val(Dgl1.Item(Col1Amount, I).Value) & ", " & _
                                    " DeliveryMeasureMultiplier = " & Val(Dgl1.Item(Col1DeliveryMeasureMultiplier, I).Value) & ", " & _
                                    " MeasurePerPcs = " & Val(Dgl1.Item(Col1MeasurePerPcs, I).Value) & ", " & _
                                    " TotalDeliveryMeasure = " & Val(Dgl1.Item(Col1TotalDeliveryMeasure, I).Value) & ", " & _
                                    " DeliveryMeasure = " & AgL.Chk_Text(Dgl1.Item(Col1DeliveryMeasureUnit, I).Value) & ", " & _
                                    " " & AgCalcGrid1.FLineTableUpdateStr(I) & " " & _
                                    " Where DocId = '" & mSearchCode & "' " & _
                                    " And Sr = " & Dgl1.Item(ColSNo, I).Tag & " "
                            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                            FUpdateDeliveryDetail(Conn, Cmd, I, mSearchCode, Dgl1.Item(ColSNo, I).Tag)
                        End If
                    Else
                        mQry = " Delete From WorkOrderDeliveryDetail Where DocId = '" & mSearchCode & "' And TSr = " & Val(Dgl1.Item(ColSNo, I).Tag) & "  "
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                        mQry = " Delete From WorkOrderDetail Where DocId = '" & mSearchCode & "' And Sr = " & Val(Dgl1.Item(ColSNo, I).Tag) & "  "
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                    End If
                End If
            End If
        Next

        If bSelectionQry <> "" Then
            mQry = "INSERT INTO WorkOrderDetail (DocId, Sr, Item,Specification, SalesTaxGroupItem, Qty, " & _
                    " Unit, Rate, RatePerQty, RatePerMeasure, Amount, PartySKU, PartyUPC, PartySpecification, " & _
                    " WorkOrder, WorkOrderSr, " & _
                    " TotalDeliveryMeasure, DeliveryMeasureMultiplier, DeliveryMeasure, MeasurePerPcs, " & _
                    " RateType, " & AgCalcGrid1.FLineTableFieldNameStr() & ") " & bSelectionQry
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If

        If bSelecttionLineQry <> "" Then
            mQry = " INSERT INTO WorkOrderDeliveryDetail(DocId, TSr, Sr, Item,  " & _
                    " Qty, Unit, MeasurePerPcs, MeasureUnit, TotalMeasure, DeliveryDate, DeliveryInstructions, " & _
                    " WorkOrder, WorkOrderSr, WorkOrderDelSchSr) " & bSelecttionLineQry
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If

        FPostInBuyerSku(mSearchCode, Conn, Cmd)

        If AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Or AgL.StrCmp(AgL.PubUserName, "sa") Then
            AgCL.GridSetiingWriteXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1)
            AgCL.GridSetiingWriteXml(Me.Text & AgCalcGrid1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, AgCalcGrid1)
            AgCL.GridSetiingWriteXml(Me.Text & AgCustomGrid1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, AgCustomGrid1)
        End If
    End Sub

    Private Sub FrmWorkOrder_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim I As Integer

        Dim IsSameUnit As Boolean = True
        Dim IsSameMeasureUnit As Boolean = True
        Dim IsSameDeliveryMeasureUnit As Boolean = True

        Dim DsTemp As DataSet

        mQry = "Select H.*, Sg.DispName As AgentName, " & _
                " C1.Description As CurrencyDesc, Sg.Nature, Sg2.DispName As ReferencePartyName " & _
                " From WorkOrder H " & _
                " LEFT JOIN SubGroup Sg On H.Agent = Sg.SubCode " & _
                " LEFT JOIN SubGroup Sg2 On H.ReferenceParty = Sg2.SubCode " & _
                " LEFT JOIN Currency C1 On H.Currency = C1.Code " & _
                " Where H.DocID='" & SearchCode & "' "
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

                TxtParty.Tag = AgL.XNull(.Rows(0)("Party"))
                TxtParty.Text = AgL.XNull(.Rows(0)("PartyName"))

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
                TxtPartyOrderNo.Text = AgL.XNull(.Rows(0)("PartyOrderNo"))
                TxtPartyOrderDate.Text = AgL.XNull(.Rows(0)("PartyOrderDate"))
                TxtDeliveryDate.Text = AgL.XNull(.Rows(0)("PartyDeliveryDate"))
                TxtTermsAndConditions.Text = AgL.XNull(.Rows(0)("TermsAndConditions"))
                TxtRemarks.Text = AgL.XNull(.Rows(0)("Remarks"))
                TxtAgent.Tag = AgL.XNull(.Rows(0)("Agent"))
                TxtAgent.Text = AgL.XNull(.Rows(0)("AgentName"))
                TxtReferenceParty.Tag = AgL.XNull(.Rows(0)("ReferenceParty"))
                TxtReferenceParty.Text = AgL.XNull(.Rows(0)("ReferencePartyName"))
                TxtReferencePartyDocumentNo.Text = AgL.XNull(.Rows(0)("ReferencePartyDocumentNo"))
                TxtReferencePartyDocumentDate.Text = AgL.XNull(.Rows(0)("ReferencePartyDocumentDate"))

                If TxtDeliveryDate.Text = "" Then
                    ChkDeliveryDetailNotRequired.Checked = True
                Else
                    ChkDeliveryDetailNotRequired.Checked = False
                End If

                Dim FrmObj As New FrmPartyDetail
                FrmObj.TxtMobile.Text = AgL.XNull(.Rows(0)("PartyMobile"))
                FrmObj.TxtName.Text = AgL.XNull(.Rows(0)("PartyName"))
                FrmObj.TxtAdd1.Text = AgL.XNull(.Rows(0)("PartyAdd1"))
                FrmObj.TxtAdd2.Text = AgL.XNull(.Rows(0)("PartyAdd2"))
                FrmObj.TxtCity.Tag = AgL.XNull(.Rows(0)("PartyCity"))
                FrmObj.TxtCity.Text = AgL.XNull(.Rows(0)("PartyCityName"))

                BtnFillPartyDetail.Tag = FrmObj

                AgCalcGrid1.FMoveRecFooterTable(DsTemp.Tables(0), EntryNCat, TxtV_Date.Text)

                AgCustomGrid1.FMoveRecFooterTable(DsTemp.Tables(0))


                mQry = "Select L.*, I.ManualCode , I.Description As ItemDesc, " & _
                        " U.DecimalPlaces as QtyDecimalPlaces, MU.DecimalPlaces as MeasureDecimalPlaces, DMU.DecimalPlaces As DeliveryMeasureDecimalPlaces, " & _
                        " Sq.V_Type + '-' + Sq.ReferenceNo As SaleQuotationNo,  " & _
                        " QD.RatePerQty As SaleQuotationRatePerQty, QD.RatePerMeasure As SaleQuotationRatePerMeasure " & _
                        " From WorkOrderDetail L " & _
                        " LEFT JOIN Item I On L.Item = I.Code  " & _
                        " Left Join Unit U On L.Unit = U.Code " & _
                        " Left Join Unit MU On L.MeasureUnit = MU.Code " & _
                        " Left Join Unit DMU On L.DeliveryMeasure = DMU.Code " & _
                        " LEFT JOIN SaleQuotation Sq On L.SaleQuotation  = Sq.DocId " & _
                        " LEFT JOIN SaleQuotationDetail QD With (NoLock) On L.SaleQuotation = QD.DocId And L.SaleQuotationSr = QD.Sr " & _
                        " Where L.DocId = '" & SearchCode & "' " & _
                        " And L.GenDocId Is Null " & _
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
                            Dgl1.Item(Col1ItemCode, I).Tag = AgL.XNull(.Rows(I)("Item"))
                            Dgl1.Item(Col1ItemCode, I).Value = AgL.XNull(.Rows(I)("ManualCode"))
                            Dgl1.Item(Col1Item, I).Tag = AgL.XNull(.Rows(I)("Item"))
                            Dgl1.Item(Col1Item, I).Value = AgL.XNull(.Rows(I)("ItemDesc"))
                            Dgl1.Item(Col1Specification, I).Value = AgL.XNull(.Rows(I)("Specification"))
                            Dgl1.Item(Col1ItemRateGroup, I).Value = AgL.XNull(.Rows(I)("RateType"))
                            Dgl1.Item(Col1PartySKU, I).Value = AgL.XNull(.Rows(I)("PartySKU"))
                            Dgl1.Item(Col1XPartySKU, I).Value = AgL.XNull(.Rows(I)("PartySKU"))
                            Dgl1.Item(Col1PartyUPC, I).Value = AgL.XNull(.Rows(I)("PartyUPC"))
                            Dgl1.Item(Col1XPartyUPC, I).Value = AgL.XNull(.Rows(I)("PartyUPC"))
                            Dgl1.Item(Col1PartySpecification, I).Value = AgL.XNull(.Rows(I)("PartySpecification"))
                            Dgl1.Item(Col1XPartySpecification, I).Value = AgL.XNull(.Rows(I)("PartySpecification"))
                            Dgl1.Item(Col1Qty, I).Value = Format(AgL.VNull(.Rows(I)("Qty")), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1Rate, I).Value = AgL.VNull(.Rows(I)("Rate"))
                            Dgl1.Item(Col1RatePerQty, I).Value = AgL.VNull(.Rows(I)("RatePerQty"))
                            ' Dgl1.Item(Col1RatePerMeasure, I).Value = AgL.VNull(.Rows(I)("RatePerMeasure"))
                            Dgl1.Item(Col1QtyDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("QtyDecimalPlaces"))
                            Dgl1.Item(Col1Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                            Dgl1.Item(Col1Amount, I).Value = AgL.VNull(.Rows(I)("Amount"))
                            'Dgl1.Item(Col1SaleQuotation, I).Tag = AgL.XNull(.Rows(I)("SaleQuotation"))
                            'Dgl1.Item(Col1SaleQuotation, I).Value = AgL.XNull(.Rows(I)("SaleQuotationNo"))
                            'Dgl1.Item(Col1SaleQuotationRatePerQty, I).Value = AgL.VNull(.Rows(I)("SaleQuotationRatePerQty"))
                            'Dgl1.Item(Col1SaleQuotationRatePerMeasure, I).Value = AgL.VNull(.Rows(I)("SaleQuotationRatePerMeasure"))
                            'Dgl1.Item(Col1SaleQuotationSr, I).Value = AgL.XNull(.Rows(I)("SaleQuotationSr"))
                            Dgl1.Item(Col1SalesTaxGroup, I).Tag = AgL.XNull(.Rows(I)("SalesTaxGroupItem"))
                            Dgl1.Item(Col1DeliveryMeasureUnit, I).Value = AgL.XNull(.Rows(I)("DeliveryMeasure"))
                            Dgl1.Item(Col1DeliveryMeasureMultiplier, I).Value = AgL.VNull(.Rows(I)("DeliveryMeasureMultiplier"))
                            Dgl1.Item(Col1MeasurePerPcs, I).Value = Format(AgL.VNull(.Rows(I)("MeasurePerPcs")), "0.".PadRight(AgL.VNull(.Rows(I)("DeliveryMeasureDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1TotalDeliveryMeasure, I).Value = Format(AgL.VNull(.Rows(I)("TotalDeliveryMeasure")), "0.".PadRight(AgL.VNull(.Rows(I)("DeliveryMeasureDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1DeliveryMeasureDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("DeliveryMeasureDecimalPlaces"))

                            If Not AgL.StrCmp(Dgl1.Item(Col1Unit, I).Value, Dgl1.Item(Col1Unit, 0).Value) Then IsSameUnit = False
                            If Not AgL.StrCmp(Dgl1.Item(Col1DeliveryMeasureUnit, I).Value, Dgl1.Item(Col1DeliveryMeasureUnit, 0).Value) Then IsSameDeliveryMeasureUnit = False

                            FFormatRateCells(I)

                            Call AgCalcGrid1.FMoveRecLineTable(DsTemp.Tables(0), I)

                            Call FMoveRecLine(mSearchCode, AgL.VNull(.Rows(I)("Sr")), I)
                        Next I
                    End If

                    If IsSameUnit Then LblTotalQtyText.Text = "Qty (" & Dgl1.Item(Col1Unit, 0).Value & ") :" Else LblTotalQtyText.Text = "Qty :"
                    If IsSameDeliveryMeasureUnit Then LblTotalDeliveryMeasureText.Text = "Delivery Measure (" & Dgl1.Item(Col1DeliveryMeasureUnit, 0).Value & ") :" Else LblTotalDeliveryMeasureText.Text = "Delivery Measure :"

                End With
                If AgCustomGrid1.Rows.Count = 0 Then AgCustomGrid1.Visible = False
                '-------------------------------------------------------------

                AgCL.GridSetiingShowXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1)
            End If
        End With
    End Sub

    Private Sub FrmWorkOrder_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Topctrl1.ChangeAgGridState(Dgl1, False)
        AgCalcGrid1.FrmType = Me.FrmType
        AgCustomGrid1.FrmType = Me.FrmType
        AgL.WinSetting(Me, 650, 992, 0, 0)
    End Sub

    Private Sub Txt_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtV_Type.Validating, TxtShipToPartyCity.Validating, TxtV_Date.Validating
        Dim I As Integer = 0
        Try
            Select Case sender.NAME
                Case TxtV_Type.Name
                    TxtStructure.Tag = AgStructure.ClsMain.FGetStructureFromNCat(LblV_Type.Tag, AgL.GcnRead)
                    AgCalcGrid1.AgStructure = TxtStructure.Tag
                    TxtCustomFields.Tag = AgCustomFields.ClsMain.FGetCustomFieldFromV_Type(TxtV_Type.Tag, AgL.GcnRead)
                    AgCustomGrid1.AgCustom = TxtCustomFields.Tag
                    IniGrid()
                    TxtReferenceNo.Text = AgTemplate.ClsMain.FGetManualRefNo("ReferenceNo", "WorkOrder", TxtV_Type.Tag, TxtV_Date.Text, TxtDivision.Tag, TxtSite_Code.Tag, AgTemplate.ClsMain.ManualRefType.Max)

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
            If TxtShipToParty.AgHelpDataSet IsNot Nothing Then
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

    Private Sub FrmWorkOrder_BaseEvent_Topctrl_tbAdd() Handles Me.BaseEvent_Topctrl_tbAdd
        BtnFillPartyDetail.Tag = Nothing

        TxtStructure.Tag = AgStructure.ClsMain.FGetStructureFromNCat(LblV_Type.Tag, AgL.GcnRead)
        AgCalcGrid1.AgStructure = TxtStructure.Tag

        TxtCustomFields.Tag = AgCustomFields.ClsMain.FGetCustomFieldFromV_Type(TxtV_Type.Tag, AgL.GCn)
        AgCustomGrid1.AgCustom = TxtCustomFields.Tag

        IniGrid()
        TabControl1.SelectedTab = TP1
        TxtReferenceNo.Text = AgTemplate.ClsMain.FGetManualRefNo("ReferenceNo", "WorkOrder", TxtV_Type.Tag, TxtV_Date.Text, TxtDivision.Tag, TxtSite_Code.Tag, AgTemplate.ClsMain.ManualRefType.Max)
        TxtPartyOrderDate.Text = TxtV_Date.Text
        TxtParty.Focus()
    End Sub

    Private Sub TxtSaleToParty_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtParty.KeyDown, TxtReferenceParty.KeyDown, TxtCurrency.KeyDown, TxtSalesTaxGroupParty.KeyDown, TxtAgent.KeyDown, TxtShipToPartyCity.KeyDown, TxtShipToParty.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then Exit Sub

            Select Case sender.Name
                Case TxtParty.Name
                    If e.KeyCode = Keys.Insert Then
                        Dim FrmObj As Object = Nothing
                        Dim CFOpen As New ClsFunction
                        Dim MDI As New MDIMain
                        FrmObj = CFOpen.FOpen("MnuCustomerMaster", "Customer Master", True)
                        If FrmObj IsNot Nothing Then
                            FrmObj.StartPosition = FormStartPosition.Manual
                            FrmObj.IsReturnValue = True
                            FrmObj.Top = 50
                            FrmObj.ShowDialog()
                            TxtParty.Tag = FrmObj.mSearchCode
                            TxtParty.Text = FrmObj.TxtDispName.Text
                            FrmObj = Nothing
                            TxtParty.AgHelpDataSet = Nothing
                            TxtPartyOrderNo.Focus()
                        End If
                    Else
                        If sender.AgHelpDataSet Is Nothing Then
                            FCreateHelpSubgroup()
                        End If
                    End If

                Case TxtReferenceParty.Name, TxtShipToParty.Name
                    If sender.AgHelpDataSet Is Nothing Then
                        sender.AgHelpDataSet(4, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = TxtParty.AgHelpDataSet
                    End If

                Case TxtCurrency.Name
                    If TxtCurrency.AgHelpDataSet Is Nothing Then
                        mQry = "SELECT Code, Description AS Currency FROM Currency ORDER BY Code "
                        TxtCurrency.AgHelpDataSet(0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
                    End If

                Case TxtSalesTaxGroupParty.Name
                    If TxtSalesTaxGroupParty.AgHelpDataSet Is Nothing Then
                        mQry = "SELECT Description AS Code, Description, IsNull(Active,0)  " & _
                                " FROM PostingGroupSalesTaxParty " & _
                                " Where IsNull(Active,1) <> 0 "
                        TxtSalesTaxGroupParty.AgHelpDataSet(1, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
                    End If

                Case TxtAgent.Name
                    If TxtAgent.AgHelpDataSet Is Nothing Then
                        mQry = "SELECT SubCode AS Code, DispName + ',' + IsNull(C.CityName,'') As Agent  " & _
                                " FROM SubGroup Sg " & _
                                " LEFT JOIN City C On Sg.CityCode = C.CityCode " & _
                                " Where IsNull(Sg.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' AND SG.MasterType = '" & ClsMain.MasterType.Agent & "'  "
                        TxtAgent.AgHelpDataSet(0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
                    End If

                Case TxtShipToPartyCity.Name
                    If TxtShipToPartyCity.AgHelpDataSet Is Nothing Then
                        mQry = " SELECT C.CityCode AS Code, C.CityName, C.State, C.Country " & _
                                " FROM City C  " & _
                                " Where IsNull(C.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' "
                        TxtShipToPartyCity.AgHelpDataSet(0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub TxtSaleToParty_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtParty.Validating, TxtPartyOrderNo.Validating, TxtV_Date.Validating, TxtPartyOrderNo.Validating, TxtPartyOrderDate.Validating, TxtDeliveryDate.Validating, TxtShipToParty.Validating
        Dim DrTemp As DataRow()
        Dim DtTemp As DataTable = Nothing
        Dim I As Integer = 0, J As Integer = 0
        Dim FrmObj As New FrmPartyDetail
        Try
            Select Case sender.name
                Case TxtParty.Name
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

                            Call ProcFillExportDetail(TxtParty.Tag, TxtV_Date.Text)
                        End If

                        If TxtReferenceParty.Text = "" Then
                            TxtReferenceParty.Tag = TxtParty.Tag
                            TxtReferenceParty.Text = TxtParty.Text
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
                            If TxtCurrency.Tag = "" Then
                                TxtCurrency.Tag = AgL.XNull(AgL.PubDtEnviro.Rows(0)("DefaultCurrency"))
                                If TxtCurrency.Tag <> "" Then
                                    TxtCurrency.Text = AgL.XNull(AgL.Dman_Execute("Select Description From Currency Where Code = '" & TxtCurrency.Tag & "'  ", AgL.GCn).ExecuteScalar)
                                End If
                            End If

                            If TxtSalesTaxGroupParty.Tag = "" Then
                                TxtSalesTaxGroupParty.Tag = AgL.XNull(AgL.PubDtEnviro.Rows(0)("DefaultSalesTaxGroupParty"))
                                TxtSalesTaxGroupParty.Text = AgL.XNull(AgL.PubDtEnviro.Rows(0)("DefaultSalesTaxGroupParty"))
                            End If

                            mQry = " Select Mobile As SaleToPartyMobile, DispName As SaleToPartyName, " & _
                                    " IsNull(Add1,'') + ' ' + IsNull(Add2,'')  + ' ' + IsNull(Add3,'')  As SaleToPartyAddress, " & _
                                    " Sg.CityCode As SaleToPartyCity, C.CityName As SaleToPartyCityName  " & _
                                    " From SubGroup Sg " & _
                                    " LEFT JOIN City C ON Sg.CityCode = C.CityCode " & _
                                    " Where Sg.SubCode = '" & TxtParty.AgSelectedValue & "'  "
                            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
                            With DtTemp
                                FrmObj.TxtMobile.Text = AgL.XNull(.Rows(0)("SaleToPartyMobile"))
                                FrmObj.TxtName.Text = AgL.XNull(.Rows(0)("SaleToPartyName"))
                                FrmObj.TxtAdd1.Text = AgL.XNull(.Rows(0)("SaleToPartyAddress"))
                                FrmObj.TxtCity.Tag = AgL.XNull(.Rows(0)("SaleToPartyCity"))
                                FrmObj.TxtCity.Text = AgL.XNull(.Rows(0)("SaleToPartyCityName"))
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

                Case TxtPartyOrderNo.Name
                    If TxtReferencePartyDocumentNo.Text = "" Then
                        TxtReferencePartyDocumentNo.Text = TxtPartyOrderNo.Text
                    End If
                    If Topctrl1.Mode = "Add" Then
                        mQry = " SELECT COUNT(*) FROM WorkOrder  WHERE PartyOrderNo  = '" & TxtPartyOrderNo.Text & "' AND Party ='" & TxtParty.Tag & "' And Site_Code = '" & AgL.PubSiteCode & "' And Div_Code = '" & AgL.PubDivCode & "' "
                        If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then MsgBox("Party Document No. Already Exists")
                    Else
                        mQry = "  SELECT COUNT(*) FROM WorkOrder WHERE PartyOrderNo  = '" & TxtPartyOrderNo.Text & "' AND Party ='" & TxtParty.Tag & "' And Site_Code = '" & AgL.PubSiteCode & "' And Div_Code = '" & AgL.PubDivCode & "' AND DocID <>'" & mInternalCode & "' "
                        If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then MsgBox("Reference No. Already Exists")
                    End If

                Case TxtV_Date.Name
                    If TxtPartyOrderDate.Text <> "" Then
                        TxtPartyOrderDate.Text = TxtV_Date.Text
                    End If

                Case TxtPartyOrderDate.Name
                    If TxtReferencePartyDocumentDate.Text = "" Then
                        TxtReferencePartyDocumentDate.Text = TxtPartyOrderDate.Text
                    End If

                Case TxtDeliveryDate.Name
                    For I = 0 To Dgl1.Rows.Count - 1
                        If Dgl1.Item(Col1BtnDeliveryDetail, I).Tag IsNot Nothing Then
                            For J = 0 To Dgl1.Item(Col1BtnDeliveryDetail, I).Tag.Dgl1.Rows.Count - 1
                                If Val(Dgl1.Item(Col1BtnDeliveryDetail, I).Tag.Dgl1.Item(FrmWorkOrderDelivery.Col1Qty, J).Value) <> 0 Then
                                    If TxtDeliveryDate.Text <> "" Then
                                        Dgl1.Item(Col1BtnDeliveryDetail, I).Tag.Dgl1.Item(FrmWorkOrderDelivery.Col1DeliveryDate, J).Value = TxtDeliveryDate.Text
                                    Else
                                        'Dgl1.Item(Col1BtnDeliveryDetail, I).Tag.Dgl1.Item(FrmWorkOrderDelivery.Col1DeliveryDate, J).Value = ""
                                    End If
                                End If
                            Next
                        End If
                    Next

                Case TxtShipToParty.Name
                    If TxtShipToParty.Tag <> "" Then
                        mQry = " Select IsNull(Add1,'') As Add1, IsNull(Add2,'') As Add2, " & _
                                   " Sg.CityCode As City, C.CityName As CityName, " & _
                                   " C.State, C.Country  " & _
                                   " From SubGroup Sg " & _
                                   " LEFT JOIN City C ON Sg.CityCode = C.CityCode " & _
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
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Validating_ItemCode(ByVal mColumn As Integer, ByVal mRow As Integer)
        Dim DrTemp As DataRow() = Nothing
        Dim DtTemp As DataTable = Nothing
        Try
            If Dgl1.AgSelectedValue(mColumn, mRow) Is Nothing Then Dgl1.AgSelectedValue(mColumn, mRow) = ""

            If Dgl1.Item(mColumn, mRow).Value.ToString.Trim = "" Or Dgl1.AgSelectedValue(mColumn, mRow).ToString.Trim = "" Then
                Dgl1.Item(Col1Unit, mRow).Value = ""
                Dgl1.Item(Col1SalesTaxGroup, mRow).Value = ""
                Dgl1.Item(Col1Rate, mRow).Value = ""
            Else
                If Dgl1.AgDataRow IsNot Nothing Then
                    Dgl1.Item(Col1Item, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("Code").Value)
                    Dgl1.Item(Col1Item, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("Description").Value)
                    Dgl1.Item(Col1ItemCode, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("Code").Value)
                    Dgl1.Item(Col1ItemCode, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("ManualCode").Value)

                    Dgl1.Item(Col1Unit, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("Unit").Value)
                    Dgl1.Item(Col1QtyDecimalPlaces, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("QtyDecimalPlaces").Value)
                    Dgl1.Item(Col1DeliveryMeasureDecimalPlaces, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("QtyDecimalPlaces").Value)

                    'If AgL.XNull(Dgl1.AgDataRow.Cells("MeasureUnit").Value) = "" Then
                    '    Dgl1.Item(Col1DeliveryMeasureUnit, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("Unit").Value)
                    '    Dgl1.Item(Col1MeasureUnit, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("Unit").Value)
                    '    Dgl1.Item(Col1MeasurePerPcs, mRow).Value = 1
                    'Else
                    Dgl1.Item(Col1DeliveryMeasureUnit, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("MeasureUnit").Value)
                    Dgl1.Item(Col1MeasureUnit, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("MeasureUnit").Value)
                    Dgl1.Item(Col1MeasurePerPcs, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("MeasurePerPcs").Value)
                    Dgl1.Item(Col1DeliveryMeasureMultiplier, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("MeasurePerPcs").Value)

                    'End If



                    Dgl1.Item(Col1Specification, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("Specification").Value)
                    Dgl1.Item(Col1SalesTaxGroup, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("SalesTaxPostingGroup").Value)
                    If AgL.StrCmp(Dgl1.AgSelectedValue(Col1SalesTaxGroup, mRow), "") Then
                        Dgl1.Item(Col1SalesTaxGroup, mRow).Tag = AgL.XNull(AgL.PubDtEnviro.Rows(0)("DefaultSalesTaxGroupItem"))
                    End If
                    Dgl1.Item(Col1Rate, mRow).Value = 0

                    'ClsMain.FGetItemRate(Dgl1.Item(Col1Item, mRow).Tag, Dgl1.Item(Col1ItemRateGroup, mRow).Tag, _
                    '     TxtV_Date.Text, TxtParty.Tag, "", _
                    '     Dgl1.Item(Col1Rate, mRow).Value, Dgl1.Item(Col1SaleQuotationRatePerQty, mRow).Value, _
                    '     Dgl1.Item(Col1SaleQuotationRatePerMeasure, mRow).Value, Dgl1.Item(Col1SaleQuotation, mRow).Tag, _
                    '     Dgl1.Item(Col1SaleQuotation, mRow).Value, Dgl1.Item(Col1SaleQuotationSr, mRow).Value, _
                    '     Dgl1.Item(Col1Qty, mRow).Value)
                End If
                Try
                    Dgl1.Item(Col1DeliveryMeasureUnit, mRow).Value = Dgl1.Item(Col1DeliveryMeasureUnit, mRow - 1).Value
                    Dgl1.Item(Col1ItemRateGroup, mRow).Value = Dgl1.Item(Col1ItemRateGroup, mRow - 1).Value
                Catch ex As Exception
                End Try
            End If

            mQry = "Select BuyerSKU, BuyerUpcCode, BuyerSpecification from ItemBuyer Where Code = '" & Dgl1.Item(mColumn, mRow).Tag & "' And Buyer = '" & TxtParty.Tag & "' "
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtTemp.Rows.Count > 0 Then
                Dgl1.Item(Col1PartySKU, mRow).Value = AgL.XNull(DtTemp.Rows(0)("BuyerSKU"))
                Dgl1.Item(Col1PartyUPC, mRow).Value = AgL.XNull(DtTemp.Rows(0)("BuyerUPCCode"))
                Dgl1.Item(Col1PartySpecification, mRow).Value = AgL.XNull(DtTemp.Rows(0)("BuyerSpecification"))
            Else
                Dgl1.Item(Col1PartySKU, mRow).Value = ""
                Dgl1.Item(Col1PartyUPC, mRow).Value = ""
                Dgl1.Item(Col1PartySpecification, mRow).Value = ""
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

            Case Col1TotalDeliveryMeasure
                CType(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex), AgControls.AgTextColumn).AgNumberRightPlaces = Val(Dgl1.Item(Col1DeliveryMeasureDecimalPlaces, Dgl1.CurrentCell.RowIndex).Value)

        End Select
    End Sub

    Private Sub Dgl1_EditingControl_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Dgl1.EditingControl_Validating
        Dim I As Integer = 0
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

                Case Col1ItemCode
                    Validating_ItemCode(mColumnIndex, mRowIndex)
                    Call FillDeliveryDetail(mRowIndex, False)
                    Call FGetDeliveryMeasureMultiplier(mRowIndex)

                Case Col1Qty
                    Call FillDeliveryDetail(mRowIndex, False)

                Case Col1DeliveryMeasureUnit
                    Call FGetDeliveryMeasureMultiplier(mRowIndex)

                    If mRowIndex < Dgl1.RowCount - 1 Then
                        If Dgl1.Item(Col1DeliveryMeasureUnit, mRowIndex).Value <> "" Then
                            If Dgl1.Item(Col1DeliveryMeasureUnit, mRowIndex + 1).Value <> Dgl1.Item(Col1DeliveryMeasureUnit, mRowIndex).Value And Dgl1.Item(Col1Item, mRowIndex + 1).Value <> "" Then
                                If MsgBox("Apply to all?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
                                    For I = 0 To Dgl1.RowCount - 1
                                        If Dgl1.Item(Col1Item, I).Value <> "" Then
                                            Dgl1.Item(Col1DeliveryMeasureUnit, I).Value = Dgl1.Item(Col1DeliveryMeasureUnit, mRowIndex).Value
                                            Call FGetDeliveryMeasureMultiplier(I)
                                        End If
                                    Next
                                    Calculation()
                                End If
                            End If
                        End If
                    End If


                Case Col1ItemRateGroup
                    'ClsMain.FGetItemRate(Dgl1.Item(Col1Item, mRowIndex).Tag, Dgl1.Item(Col1ItemRateGroup, mRowIndex).Tag, TxtV_Date.Text, TxtParty.Tag, "", Dgl1.Item(Col1Rate, mRowIndex).Value, Dgl1.Item(Col1SaleQuotationRatePerQty, mRowIndex).Value, Dgl1.Item(Col1SaleQuotationRatePerMeasure, mRowIndex).Value, Dgl1.Item(Col1SaleQuotation, mRowIndex).Tag, Dgl1.Item(Col1SaleQuotation, mRowIndex).Value, Dgl1.Item(Col1SaleQuotationSr, mRowIndex).Value, Dgl1.Item(Col1Qty, mRowIndex).Value)
            End Select
            Call Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub DGL1_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles Dgl1.RowsAdded, Dgl1.RowsAdded
        sender(ColSNo, sender.Rows.Count - 1).Value = Trim(sender.Rows.Count)
    End Sub

    Private Sub FrmWorkOrder_BaseFunction_Calculation() Handles Me.BaseFunction_Calculation
        Dim I As Integer

        Dim IsSameUnit As Boolean = True
        Dim IsSameDeliveryMeasureUnit As Boolean = True

        Dim intQtyDecimalPlaces As Integer = 0
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

                'If In Item Master Measure Per Pcs Is Defined then this calculation will be executed.
                'For Example In Carpet Area Per Pcs Is Defined in Item Master and Total Area will be calculated
                'with that Area per pcs. 
                'If Val(Dgl1.Item(Col1DeliveryMeasurePerPcs, I).Value) <> 0 Then
                'Dgl1.Item(Col1TotaldMeasure, I).Value = Format(Val(Dgl1.Item(Col1Qty, I).Value) * Val(Dgl1.Item(Col1MeasurePerPcs, I).Value), "0.".PadRight(CType(Dgl1.Columns(Col1TotalMeasure), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                'End If

                'If in item master Pcs Per Measure is defined this calculation will be executed.
                'for example in case of soap user will feed how many cartons he purchased in the measure field and
                'qty will be calculated on the basis of the pcs per measure.
                'If Val(Dgl1.Item(Col1PcsPerMeasure, I).Value) <> 0 Then
                '    Dgl1.Item(Col1Qty, I).Value = Format(Val(Dgl1.Item(Col1TotalMeasure, I).Value) * Val(Dgl1.Item(Col1PcsPerMeasure, I).Value), "0.".PadRight(CType(Dgl1.Columns(Col1Qty), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                'End If

                'if the qty unit and mesure units are equal then qty will auto come in mesure fields
                'for example yarn's unit and measure unit is Kg
                'In this case same figure will be copied in the measure.
                'If AgL.StrCmp(Dgl1.Item(Col1MeasureUnit, I).Value, Dgl1.Item(Col1Unit, I).Value) Then
                '    Dgl1.Item(Col1MeasurePerPcs, I).Value = 1
                '    Dgl1.Item(Col1TotalMeasure, I).Value = Format(Val(Dgl1.Item(Col1Qty, I).Value), "0.".PadRight(CType(Dgl1.Columns(Col1TotalMeasure), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                'End If

                'By default measure unit will automatically come in delivery meaure unit and delivery measure
                'multiplier will be set to 1.

                'If Dgl1.Item(Col1DeliveryMeasure, I).Value = "" Then
                '    Dgl1.Item(Col1DeliveryMeasure, I).Value = Dgl1.Item(Col1Unit, I).Value
                'End If

                'If Dgl1.Item(Col1Unit, I).Value <> "" And Dgl1.Item(Col1DeliveryMeasure, I).Value <> "" Then
                '    If AgL.StrCmp(Dgl1.Item(Col1Unit, I).Value, Dgl1.Item(Col1DeliveryMeasure, I).Value) Then
                '        Dgl1.Item(Col1DeliveryMeasureMultiplier, I).Value = 1
                '    End If
                'End If

                'Delivery measure calculation
                'Delivery measure will be automatically calculated on the basis of delivery measure multiplier.
                'If Val(Dgl1.Item(Col1DeliveryMeasureMultiplier, I).Value) <> 0 Then
                '    'Dgl1.Item(Col1DeliveryMeasurePerPcs, I).Value = Format(Val(Dgl1.Item(Col1DeliveryMeasurePerPcs, I).Value) * Val(Dgl1.Item(Col1DeliveryMeasureMultiplier, I).Value), "0.".PadRight(CType(Dgl1.Columns(Col1TotalDeliveryMeasure), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                '    'Dgl1.Item(Col1TotalDeliveryMeasure, I).Value = Format(Val(Dgl1.Item(Col1TotalMeasure, I).Value) * Val(Dgl1.Item(Col1DeliveryMeasureMultiplier, I).Value), "0.".PadRight(CType(Dgl1.Columns(Col1TotalDeliveryMeasure), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                'ElseIf Val(Dgl1.Item(Col1DeliveryMeasurePerPcs, I).Value) <> 0 Then
                '    Dgl1.Item(Col1TotalDeliveryMeasure, I).Value = Format(Val(Dgl1.Item(Col1Qty, I).Value) * Val(Dgl1.Item(Col1DeliveryMeasurePerPcs, I).Value), "0.".PadRight(CType(Dgl1.Columns(Col1TotalDeliveryMeasure), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                'End If




                'If AgL.StrCmp(Dgl1.Item(Col1BillingType, I).Value, "Qty") Or Dgl1.Item(Col1BillingType, I).Value = "" Then
                '    Dgl1.Item(Col1RatePerQty, I).Value = Val(Dgl1.Item(Col1Rate, I).Value)
                '    'If Val(Dgl1.Item(Col1MeasurePerPcs, I).Value) <> 0 Then
                '    '    Dgl1.Item(Col1RatePerMeasure, I).Value = Math.Round(Val(Dgl1.Item(Col1RatePerQty, I).Value) / Val(Dgl1.Item(Col1MeasurePerPcs, I).Value), 2)
                '    'End If
                'Else : AgL.StrCmp(Dgl1.Item(Col1BillingType, I).Value, "Measure")
                'Dgl1.Item(Col1RatePerMeasure, I).Value = Val(Dgl1.Item(Col1Rate, I).Value)
                'If Val(Dgl1.Item(Col1DeliveryMeasurePerPcs, I).Value) <> 0 Then
                '    Dgl1.Item(Col1RatePerQty, I).Value = Math.Round(Val(Dgl1.Item(Col1RatePerMeasure, I).Value) * Val(Dgl1.Item(Col1DeliveryMeasurePerPcs, I).Value), 2)
                'End If
                ' End If

                'If AgL.StrCmp(Dgl1.Item(Col1BillingType, I).Value, "Measure") Or Dgl1.Item(Col1BillingType, I).Value = "" Then

                'ElseIf AgL.StrCmp(Dgl1.Item(Col1BillingType, I).Value, "Qty") Then
                '    Dgl1.Item(Col1Amount, I).Value = Format(Val(Dgl1.Item(Col1Qty, I).Value) * Val(Dgl1.Item(Col1Rate, I).Value), "0.".PadRight(CType(Dgl1.Columns(Col1Amount), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                'Else
                '    Dgl1.Item(Col1Amount, I).Value = Format(Val(Dgl1.Item(Col1Qty, I).Value) * Val(Dgl1.Item(Col1Rate, I).Value), "0.".PadRight(CType(Dgl1.Columns(Col1Amount), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                'End If


                'Dgl1.Item(Col1TotalDeliveryMeasure, I).Value = Format(Val(Dgl1.Item(Col1Qty, I).Value) * Val(Dgl1.Item(Col1DeliveryMeasureMultiplier, I).Value), "0.".PadRight(CType(Dgl1.Columns(Col1TotalDeliveryMeasure), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                'Dgl1.Item(Col1Amount, I).Value = Format(Val(Dgl1.Item(Col1TotalDeliveryMeasure, I).Value) * Val(Dgl1.Item(Col1Rate, I).Value), "0.".PadRight(CType(Dgl1.Columns(Col1Amount), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))


                If Val(Dgl1.Item(Col1MeasurePerPcs, I).Value) > 0 Then
                    Dgl1.Item(Col1TotalDeliveryMeasure, I).Value = Val(Dgl1.Item(Col1Qty, I).Value) * Val(Dgl1.Item(Col1MeasurePerPcs, I).Value) * Val(Dgl1.Item(Col1DeliveryMeasureMultiplier, I).Value)
                ElseIf Val(Dgl1.Item(Col1DeliveryMeasureMultiplier, I).Value) > 0 Then
                    Dgl1.Item(Col1Qty, I).Value = Val(Dgl1.Item(Col1TotalDeliveryMeasure, I).Value) * Val(Dgl1.Item(Col1DeliveryMeasureMultiplier, I).Value)
                End If

                Dgl1.Item(Col1Amount, I).Value = Format(Val(Dgl1.Item(Col1TotalDeliveryMeasure, I).Value) * Val(Dgl1.Item(Col1Rate, I).Value), "0.".PadRight(CType(Dgl1.Columns(Col1Amount), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))

                If Not AgL.StrCmp(Dgl1.Item(Col1Unit, I).Value, Dgl1.Item(Col1Unit, 0).Value) Then IsSameUnit = False
                If Not AgL.StrCmp(Dgl1.Item(Col1DeliveryMeasureUnit, I).Value, Dgl1.Item(Col1DeliveryMeasureUnit, 0).Value) Then IsSameDeliveryMeasureUnit = False

                If intQtyDecimalPlaces < Val(Dgl1.Item(Col1QtyDecimalPlaces, I).Value) Then intQtyDecimalPlaces = Val(Dgl1.Item(Col1QtyDecimalPlaces, I).Value)
                If intDeliveryMeasureDecimalPlaces < Val(Dgl1.Item(Col1DeliveryMeasureDecimalPlaces, I).Value) Then intDeliveryMeasureDecimalPlaces = Val(Dgl1.Item(Col1DeliveryMeasureDecimalPlaces, I).Value)


                'Footer Calculation
                LblTotalQty.Text = Val(LblTotalQty.Text) + Val(Dgl1.Item(Col1Qty, I).Value)
                LblTotalDeliveryMeasure.Text = Val(LblTotalDeliveryMeasure.Text) + Val(Dgl1.Item(Col1TotalDeliveryMeasure, I).Value)
                LblTotalAmount.Text = Val(LblTotalAmount.Text) + Val(Dgl1.Item(Col1Amount, I).Value)

                FFormatRateCells(I)
            End If
        Next

        AgCalcGrid1.AgPostingGroupSalesTaxParty = TxtSalesTaxGroupParty.Tag
        AgCalcGrid1.AgPostingGroupSalesTaxItem = AgL.XNull(AgL.PubDtEnviro.Rows(0)("DefaultSalesTaxGroupItem"))
        AgCalcGrid1.Calculation()

        LblTotalQty.Text = Format(Val(LblTotalQty.Text), "0.".PadRight(intQtyDecimalPlaces + 2, "0"))
        LblTotalMeasure.Text = Format(Val(LblTotalMeasure.Text), "0.".PadRight(intDeliveryMeasureDecimalPlaces + 2, "0"))
        LblTotalDeliveryMeasure.Text = Format(Val(LblTotalDeliveryMeasure.Text), "0.".PadRight(intDeliveryMeasureDecimalPlaces + 2, "0"))
        LblTotalAmount.Text = Format(Val(LblTotalAmount.Text), "0.00")


        If Dgl1.Item(Col1Unit, 0).Value <> "" And IsSameUnit Then LblTotalQtyText.Text = "Qty (" & Dgl1.Item(Col1Unit, 0).Value & ") :" Else LblTotalQtyText.Text = "Qty :"
        'If Dgl1.Item(Col1MeasureUnit, 0).Value <> "" And IsSameMeasureUnit Then LblTotalMeasureText.Text = "Measure (" & Dgl1.Item(Col1MeasureUnit, 0).Value & ") :" Else LblTotalMeasureText.Text = "Measure :"
        If Dgl1.Item(Col1DeliveryMeasureUnit, 0).Value <> "" And IsSameDeliveryMeasureUnit Then LblTotalDeliveryMeasureText.Text = "Delivery Measure (" & Dgl1.Item(Col1DeliveryMeasureUnit, 0).Value & ") :" Else LblTotalDeliveryMeasureText.Text = "Delivery Measure :"
    End Sub

    Private Sub FrmWorkOrder_BaseFunction_DispText() Handles Me.BaseFunction_DispText
        TxtShipToPartyState.Enabled = False
        TxtShipToPartyCountry.Enabled = False
        If AgL.PubUserName.ToUpper.Trim <> "SUPER" Then
            ChkDontLockRows.Visible = False
        End If
    End Sub

    Private Sub TxtOrderCancelDate_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtRemarks.LostFocus
        Select Case sender.NAME
            Case TxtRemarks.Name
                TabControl1.SelectedTab = TPShipping
        End Select
    End Sub

    Private Sub FrmWorkOrder_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        Dim I As Integer = 0
        If AgL.RequiredField(TxtReferenceNo, LblReferenceNo.Text) Then passed = False : Exit Sub

        If TxtPartyOrderDate.Text <> "" Then
            If CDate(TxtPartyOrderDate.Text) > CDate(TxtV_Date.Text) Then
                MsgBox("Party order date can't be greater than order date")
                TxtPartyOrderDate.Focus()
                passed = False : Exit Sub
            End If
        End If

        If TxtDeliveryDate.Text <> "" Then
            If CDate(TxtV_Date.Text) > CDate(TxtDeliveryDate.Text) Then
                MsgBox("Delivery date can't be less than order date")
                TabControl1.SelectedTab = TP1 : TxtDeliveryDate.Focus()
                passed = False : Exit Sub
            End If
        End If

        If AgCL.AgIsBlankGrid(Dgl1, Dgl1.Columns(Col1Item).Index) Then passed = False : Exit Sub
        If AgCL.AgIsDuplicate(Dgl1, Dgl1.Columns(Col1Item).Index) Then passed = False : Exit Sub

        If Not ChkDeliveryDetailNotRequired.Checked Then
            If TxtDeliveryDate.Text = "" Then
                MsgBox("Delivery Date Is Blank", MsgBoxStyle.Information)
                TxtDeliveryDate.Focus()
                passed = False : Exit Sub
            End If
        End If

        passed = AgTemplate.ClsMain.FCheckDuplicateRefNo("ReferenceNo", "WorkOrder", TxtV_Type.Tag, TxtV_Date.Text, TxtDivision.Tag, TxtSite_Code.Tag, Topctrl1.Mode, TxtReferenceNo.Text, mSearchCode)
        If passed = False Then Exit Sub

        If Topctrl1.Mode = "Add" Then
            mQry = " SELECT COUNT(*) FROM WorkOrder  WHERE PartyOrderNo  = '" & TxtPartyOrderNo.Text & "' AND Party ='" & TxtParty.Tag & "' And Site_Code = '" & AgL.PubSiteCode & "' And Div_Code = '" & AgL.PubDivCode & "' "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then passed = False : MsgBox("Party Document No. Already Exists")
        Else
            mQry = "  SELECT COUNT(*) FROM WorkOrder WHERE PartyOrderNo  = '" & TxtPartyOrderNo.Text & "' AND Party ='" & TxtParty.Tag & "' And Site_Code = '" & AgL.PubSiteCode & "' And Div_Code = '" & AgL.PubDivCode & "' AND DocID <>'" & mInternalCode & "' "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then passed = False : MsgBox("Reference No. Already Exists")
        End If

        With Dgl1
            For I = 0 To .Rows.Count - 1
                If .Item(Col1Item, I).Value <> "" Then
                    If Dgl1.Rows(I).Visible Then
                        If Val(.Item(Col1Qty, I).Value) = 0 Then
                            MsgBox("Qty Is 0 At Row No " & Dgl1.Item(ColSNo, I).Value & "")
                            .CurrentCell = .Item(Col1Qty, I) : Dgl1.Focus()
                            passed = False : Exit Sub
                        End If
                    End If
                End If
            Next
        End With
    End Sub

    Private Sub TxtShipToPartyCity_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtShipToPartyCity.Enter
        Select Case sender.name
            Case TxtShipToPartyCity.Name
        End Select
    End Sub

    Private Sub FrmWorkOrder_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
        Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
    End Sub

    Private Sub FrmWorkOrder_BaseEvent_Topctrl_tbPrn(ByVal SearchCode As String) Handles Me.BaseEvent_Topctrl_tbPrn
        mQry = "  SELECT WO.DocID, WO.V_Type, WO.V_Date, WO.V_No, WO.PartyName, WO.PartyAdd1, WO.PartyAdd2, WO.PartyCity,  WO.PartyCityName, WO.PartyState, " & _
                " WO.PartyCountry, WO.ShipToPartyName,  WO.ShipToPartyAdd1, WO.ShipToPartyAdd2, WO.ShipToPartyCity, WO.ShipToPartyCityName,  WO.ShipToPartyState, WO.ShipToPartyCountry,  " & _
                " WO.Currency, WO.SalesTaxGroupParty,  WO.PartyOrderNo, WO.PartyOrderDate, WO.PartyDeliveryDate,  WO.PartyOrderCancelDate, " & _
                " WO.TermsAndConditions,  WO.Remarks, WO.EntryBy, WO.EntryType, WO.ApproveBy,   " & _
                " SG.EMail AS PartyEMail, SG.Mobile AS SaleToPartyMobile,  WO.ShipToParty, WO.Agent,   " & _
                " WO.ReferenceParty , WO.ReferencePartyDocumentNo ,	WO.ReferencePartyDocumentDate, SGR.DispName AS ReferencePartyName,   " & _
                " WOD.Sr, WOD.Item, WOD.SalesTaxGroupItem, WOD.Qty, WOD.Unit, WOD.Rate, WOD.Amount, WOD.SPECIFICATION,  WOD.UID, IB.BuyerSKU as PartySKU,  " & _
                " IB.BuyerUpcCode as PartyUPC,  WOD.MeasurePerPcs, WOD.TotalMeasure AS LineTotalMeasure,  D.Div_Name,SM.Name AS SiteName,  " & _
                " I.ManualCode AS ItemCode,  I.Description AS ItemDesc,Vt.Description AS OrderTypeDesc,  WOD.MeasurePerPcs As AreaPerPcs, WOD.TotalMeasure As LineTotalArea   " & _
                " FROM WorkOrder WO   " & _
                " LEFT JOIN WorkOrderDetail WOD ON WOD.DocID =WO.DocID   " & _
                " LEFT JOIN Division D ON D.Div_Code=WO.Div_Code    " & _
                " LEFT JOIN SiteMast SM ON SM.Code=WO.Site_Code    " & _
                " LEFT JOIN Item I ON I.Code=WOD.Item    " & _
                " LEFT JOIN ItemBuyer IB ON I.Code = IB.Code AND  WO.Party =  IB.Buyer   " & _
                " LEFT JOIN SubGroup SG ON SG.SubCode = WO.Party   " & _
                " LEFT JOIN Voucher_Type Vt ON Vt.V_Type = WO.V_Type   " & _
                " LEFT JOIN SUBGROUP SGR ON SGR.SubCode = WO.ReferenceParty   " & _
                " WHERE WO.DocID ='" & SearchCode & "'"
        ClsMain.FPrintThisDocument(Me, TxtV_Type.Tag, mQry, "WorkOrder_Print", "Work Order")
    End Sub

    Private Sub ProcFillExportDetail(ByVal Party As String, ByVal V_Date As String)
        Dim DsTemp As DataSet = Nothing
        Try
            If Not AgL.StrCmp(Topctrl1.Mode, "Add") Then Exit Sub

            mQry = "SELECT TOP 1 H.* " & _
                    " FROM WorkOrder H " & _
                    " WHERE H.Party = '" & Party & "' " & _
                    " AND H.V_Date <= '" & V_Date & "' " & _
                    " ORDER BY H.V_Date DESC	 "
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
        Dim DrTemp As DataRow() = Nothing
        Dim bRowIndex As Integer = 0, bColumnIndex As Integer = 0
        Dim bItemCode$ = ""

        If Topctrl1.Mode = "Browse" Then Exit Sub
        If Dgl1.CurrentCell Is Nothing Then Dgl1.CurrentCell = Dgl1.Item(Col1Item, 0)

        If e.Control And e.KeyCode = Keys.D And Dgl1.Rows(Dgl1.CurrentCell.RowIndex).DefaultCellStyle.BackColor <> RowLockedColour Then
            sender.CurrentRow.Selected = True
            sender.CurrentRow.Visible = False
        End If
        If e.Control Or e.Shift Or e.Alt Then Exit Sub

        Try
            bRowIndex = Dgl1.CurrentCell.RowIndex
            bColumnIndex = Dgl1.CurrentCell.ColumnIndex

            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Item
                    If e.KeyCode = Keys.Insert Then
                        Dim FrmObj As Object = Nothing
                        Dim CFOpen As New ClsFunction
                        Dim MDI As New MDIMain
                        FrmObj = CFOpen.FOpen("MnuItemMaster", "Item Master", True)
                        If FrmObj IsNot Nothing Then
                            FrmObj.StartPosition = FormStartPosition.Manual
                            FrmObj.IsReturnValue = True
                            FrmObj.Top = 50
                            FrmObj.ShowDialog()
                            bItemCode = FrmObj.mItemCode
                            FrmObj = Nothing
                            Dgl1.Item(Col1Item, bRowIndex).Value = ""
                            Dgl1.Item(Col1Item, bRowIndex).Tag = ""
                            Dgl1.CurrentCell = Dgl1.Item(Col1Specification, bRowIndex)
                            FCreateHelpItem()
                            If Dgl1.AgHelpDataSet(Col1Item) IsNot Nothing Then
                                DrTemp = Dgl1.AgHelpDataSet(Col1Item).Tables(0).Select("Code = '" & bItemCode & "'")
                                If DrTemp.Length > 0 Then
                                    Dgl1.Item(Col1Item, bRowIndex).Tag = AgL.XNull(DrTemp(0)("Code"))
                                    Dgl1.Item(Col1Item, bRowIndex).Value = AgL.XNull(DrTemp(0)("Description"))
                                    Dgl1.Item(Col1ItemCode, bRowIndex).Tag = AgL.XNull(DrTemp(0)("Code"))
                                    Dgl1.Item(Col1ItemCode, bRowIndex).Value = AgL.XNull(DrTemp(0)("ManualCode"))
                                    Dgl1.Item(Col1Unit, bRowIndex).Value = AgL.XNull(DrTemp(0)("Unit"))
                                    Dgl1.Item(Col1QtyDecimalPlaces, bRowIndex).Value = AgL.VNull(DrTemp(0)("QtyDecimalPlaces"))
                                    'Dgl1.Item(Col1MeasurePerPcs, bRowIndex).Value = AgL.XNull(DrTemp(0)("MeasurePerPcs"))
                                    'Dgl1.Item(Col1MeasureUnit, bRowIndex).Value = AgL.XNull(DrTemp(0)("MeasureUnit"))
                                    'Dgl1.Item(Col1MeasureDecimalPlaces, bRowIndex).Value = AgL.VNull(DrTemp(0)("MeasureDecimalPlaces"))
                                    Dgl1.Item(Col1DeliveryMeasureDecimalPlaces, bRowIndex).Value = AgL.VNull(DrTemp(0)("DeliveryMeasureDecimalPlaces"))
                                    Dgl1.Item(Col1DeliveryMeasureUnit, bRowIndex).Value = AgL.XNull(DrTemp(0)("MeasureUnit"))
                                    Dgl1.Item(Col1DeliveryMeasureMultiplier, bRowIndex).Value = 1
                                    Dgl1.Item(Col1Rate, bRowIndex).Value = AgL.XNull(DrTemp(0)("Rate"))
                                    Dgl1.Item(Col1SalesTaxGroup, bRowIndex).Tag = AgL.XNull(DrTemp(0)("SalesTaxPostingGroup"))
                                    If AgL.StrCmp(Dgl1.AgSelectedValue(Col1SalesTaxGroup, bRowIndex), "") Then
                                        Dgl1.Item(Col1SalesTaxGroup, bRowIndex).Tag = AgL.XNull(AgL.PubDtEnviro.Rows(0)("DefaultSalesTaxGroupItem"))
                                    End If
                                    'If Dgl1.Item(Col1MeasureUnit, bRowIndex).Value = "" Then Dgl1.Item(Col1TotalMeasure, bRowIndex).ReadOnly = True
                                    'ClsMain.FGetItemRate(Dgl1.Item(Col1Item, bRowIndex).Tag, Dgl1.Item(Col1ItemRateGroup, bRowIndex).Tag, TxtV_Date.Text, TxtParty.Tag, "", Dgl1.Item(Col1Rate, bRowIndex).Value, Dgl1.Item(Col1SaleQuotationRatePerQty, bRowIndex).Value, Dgl1.Item(Col1SaleQuotationRatePerMeasure, bRowIndex).Value, Dgl1.Item(Col1SaleQuotation, bRowIndex).Tag, Dgl1.Item(Col1SaleQuotation, bRowIndex).Value, Dgl1.Item(Col1SaleQuotationSr, bRowIndex).Value, Dgl1.Item(Col1Qty, bRowIndex).Value)
                                End If
                            End If
                        End If
                    End If

            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub

    Private Sub TxtReferenceParty_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtReferenceParty.Enter
        Select Case sender.name
            Case TxtReferenceParty.Name
        End Select
    End Sub

    Private Sub Dgl1_CellContentClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellContentClick
        Dim bColumnIndex As Integer = 0
        Dim bRowIndex As Integer = 0
        Dim I As Integer = 0
        Try
            bColumnIndex = Dgl1.CurrentCell.ColumnIndex
            bRowIndex = Dgl1.CurrentCell.RowIndex
            If Dgl1.Item(Col1Item, bRowIndex).Value = "" Then Exit Sub
            Select Case Dgl1.Columns(e.ColumnIndex).Name
                Case Col1BtnDeliveryDetail
                    Dim FrmObj As FrmWorkOrderDelivery = Nothing
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
        'Dgl1.Item(Col1BtnDeliveryDetail, bRowIndex).Tag.MeasurePerPcs = Val(Dgl1.Item(Col1MeasurePerPcs, bRowIndex).Value)
        'Dgl1.Item(Col1BtnDeliveryDetail, bRowIndex).Tag.MeasureUnit = Val(Dgl1.Item(Col1MeasureUnit, bRowIndex).Value)
        Dgl1.Item(Col1BtnDeliveryDetail, bRowIndex).Tag.EntryMode = Topctrl1.Mode

        If Val(Dgl1.Item(Col1BtnDeliveryDetail, bRowIndex).Tag.Dgl1.Item(FrmWorkOrderDelivery.Col1Qty, 0).Value) = 0 Then
            Dgl1.Item(Col1BtnDeliveryDetail, bRowIndex).Tag.Dgl1.Item(FrmWorkOrderDelivery.Col1Qty, 0).Value = Dgl1.Item(Col1Qty, bRowIndex).Value
            Dgl1.Item(Col1BtnDeliveryDetail, bRowIndex).Tag.Validate_Qty(0)
            Dgl1.Item(Col1BtnDeliveryDetail, bRowIndex).Tag.Calculation()
        End If

        If ShowWindow = True Then Dgl1.Item(Col1BtnDeliveryDetail, bRowIndex).Tag.ShowDialog()
    End Sub

    Private Function FunRetNewObject() As Object
        Dim FrmObj As FrmWorkOrderDelivery
        Try
            FrmObj = New FrmWorkOrderDelivery
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

            mQry = "Select Sum(L.Qty) As Qty, Max(L.Unit) As Unit, Max(L.MeasurePerPcs) As MeasurePerPcs, " & _
                    " Max(L.MeasureUnit) As MeasureUnit, Max(L.TotalMeasure) As TotalMeasure, " & _
                    " Max(L.DeliveryDate) As DeliveryDate, Max(L.DeliveryInstructions) As DeliveryInstructions, " & _
                    " Max(I.Description) As ItemDesc, Max(L.Sr) As Sr " & _
                    " From WorkOrderDeliveryDetail L " & _
                    " LEFT JOIN Item I ON L.Item = I.Code " & _
                    " Where L.WorkOrder = '" & SearchCode & "' " & _
                    " And L.WorkOrderSr = " & Val(TSr) & " " & _
                    " GROUP BY L.WorkOrder, L.WorkOrderSr, L.WorkOrderDelSchSr " & _
                    " Having Sum(L.Qty) > 0 " & _
                    " Order By L.WorkOrderDelSchSr "
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

                        Dgl1.Item(Col1BtnDeliveryDetail, mGridRow).Tag.Dgl1.Item(FrmWorkOrderDelivery.ColSNo, I).Value = Dgl1.Item(Col1BtnDeliveryDetail, mGridRow).Tag.Dgl1.Rows.Count - 1
                        Dgl1.Item(Col1BtnDeliveryDetail, mGridRow).Tag.Dgl1.Item(FrmWorkOrderDelivery.ColSNo, I).Tag = AgL.VNull(.Rows(I)("Sr"))
                        Dgl1.Item(Col1BtnDeliveryDetail, mGridRow).Tag.Dgl1.Item(FrmWorkOrderDelivery.Col1Qty, I).Value = AgL.VNull(.Rows(I)("Qty"))
                        Dgl1.Item(Col1BtnDeliveryDetail, mGridRow).Tag.Dgl1.Item(FrmWorkOrderDelivery.Col1Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                        Dgl1.Item(Col1BtnDeliveryDetail, mGridRow).Tag.Dgl1.Item(FrmWorkOrderDelivery.Col1MeasurePerPcs, I).Value = AgL.VNull(.Rows(I)("MeasurePerPcs"))
                        Dgl1.Item(Col1BtnDeliveryDetail, mGridRow).Tag.Dgl1.Item(FrmWorkOrderDelivery.Col1MeasureUnit, I).Value = AgL.XNull(.Rows(I)("MeasureUnit"))
                        Dgl1.Item(Col1BtnDeliveryDetail, mGridRow).Tag.Dgl1.Item(FrmWorkOrderDelivery.Col1TotalMeasure, I).Value = AgL.VNull(.Rows(I)("TotalMeasure"))
                        Dgl1.Item(Col1BtnDeliveryDetail, mGridRow).Tag.Dgl1.Item(FrmWorkOrderDelivery.Col1DeliveryDate, I).Value = AgL.XNull(.Rows(I)("DeliveryDate"))
                        Dgl1.Item(Col1BtnDeliveryDetail, mGridRow).Tag.Dgl1.Item(FrmWorkOrderDelivery.Col1DeliveryInstruction, I).Value = AgL.XNull(.Rows(I)("DeliveryInstructions"))
                        Dgl1.Item(Col1BtnDeliveryDetail, mGridRow).Tag.EntryMode = Topctrl1.Mode
                    Next I
                End If
            End With

            mQry = " SELECT Count(*) As Cnt " & _
                    " FROM WorkOrderDeliveryDetail L  " & _
                    " WHERE L.WorkOrder = '" & SearchCode & "' AND L.WorkOrderSr = '" & TSr & "' " & _
                    " GROUP BY L.WorkOrder, L.WorkOrderSr, L.WorkOrderDelSchSr  " & _
                    " HAVING Count(*)  > 1 "
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

            If DtTemp.Rows.Count > 0 And Not ChkDontLockRows.Checked Then
                Dgl1.Rows(mGridRow).DefaultCellStyle.BackColor = RowLockedColour
                Dgl1.Rows(mGridRow).ReadOnly = True
            End If


            mQry = " SELECT Count(*) As Cnt " & _
                    " FROM WorkDispatchDetail L  " & _
                    " WHERE L.WorkOrder = '" & SearchCode & "' AND L.WorkOrderSr = '" & TSr & "' " & _
                    " GROUP BY L.WorkOrder, L.WorkOrderSr  " & _
                    " HAVING Count(*)  > 0 "
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

            If DtTemp.Rows.Count > 0 And Not ChkDontLockRows.Checked Then
                Dgl1.Rows(mGridRow).DefaultCellStyle.BackColor = RowLockedColour
                Dgl1.Rows(mGridRow).ReadOnly = True
            End If



            mQry = " SELECT Count(*) As Cnt " & _
                    " FROM WorkOrderDeliveryDetail L  " & _
                    " WHERE L.WorkOrder = '" & SearchCode & "' AND L.WorkOrderSr = '" & TSr & "' " & _
                    " GROUP BY L.WorkOrder, L.WorkOrderSr " & _
                    " HAVING Count(*) > 1 "
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

            If DtTemp.Rows.Count > 0 Then
                Dgl1.Item(Col1BtnDeliveryDetail, mGridRow).Style.ForeColor = Color.Red
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub FGetLineQry(ByRef SelectionLineQry As String, ByVal Conn As SqlClient.SqlConnection, ByVal Cmd As SqlClient.SqlCommand, ByVal mGridRow As Integer, ByVal Sr As Integer)
        Dim I As Integer = 0, mLineSr As Integer = 0

        If Dgl1.Item(Col1BtnDeliveryDetail, mGridRow).Tag IsNot Nothing Then
            For I = 0 To Dgl1.Item(Col1BtnDeliveryDetail, mGridRow).Tag.Dgl1.Rows.Count - 1
                If Dgl1.Item(Col1BtnDeliveryDetail, mGridRow).Tag.Dgl1.Item(FrmWorkOrderDelivery.Col1DeliveryDate, I).Value <> "" Then
                    mLineSr += 1
                    If SelectionLineQry <> "" Then SelectionLineQry += " UNION ALL "
                    SelectionLineQry += " Select " & AgL.Chk_Text(mSearchCode) & ", " & _
                            " " & Val(Sr) & ", " & _
                            " " & Val(mLineSr) & ", " & _
                            " " & AgL.Chk_Text(Dgl1.Item(Col1Item, mGridRow).Tag) & ", " & _
                            " " & Val(Dgl1.Item(Col1BtnDeliveryDetail, mGridRow).Tag.Dgl1.Item(FrmWorkOrderDelivery.Col1Qty, I).Value) & ", " & _
                            " " & AgL.Chk_Text(Dgl1.Item(Col1BtnDeliveryDetail, mGridRow).Tag.Dgl1.Item(FrmWorkOrderDelivery.Col1Unit, I).Value) & ", " & _
                            " " & Val(Dgl1.Item(Col1BtnDeliveryDetail, mGridRow).Tag.Dgl1.Item(FrmWorkOrderDelivery.Col1MeasurePerPcs, I).Value) & ", " & _
                            " " & AgL.Chk_Text(Dgl1.Item(Col1BtnDeliveryDetail, mGridRow).Tag.Dgl1.Item(FrmWorkOrderDelivery.Col1MeasureUnit, I).Value) & ", " & _
                            " " & Val(Dgl1.Item(Col1BtnDeliveryDetail, mGridRow).Tag.Dgl1.Item(FrmWorkOrderDelivery.Col1TotalMeasure, I).Value) & ", " & _
                            " " & AgL.Chk_Text(Dgl1.Item(Col1BtnDeliveryDetail, mGridRow).Tag.Dgl1.Item(FrmWorkOrderDelivery.Col1DeliveryDate, I).Value) & ", " & _
                            " " & AgL.Chk_Text(Dgl1.Item(Col1BtnDeliveryDetail, mGridRow).Tag.Dgl1.Item(FrmWorkOrderDelivery.Col1DeliveryInstruction, I).Value) & ", " & _
                            " " & AgL.Chk_Text(mSearchCode) & ", " & Val(Sr) & ", " & Val(mLineSr) & " "
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                End If
            Next
        End If
    End Sub

    Public Sub FUpdateDeliveryDetail(ByVal Conn As SqlClient.SqlConnection, ByVal Cmd As SqlClient.SqlCommand, ByVal mGridRow As Integer, ByVal DocId As String, ByVal TSr As Integer)
        Dim I As Integer = 0, mLineSr As Integer = 0
        For I = 0 To Dgl1.Item(Col1BtnDeliveryDetail, mGridRow).Tag.Dgl1.Rows.Count - 1
            If Dgl1.Item(Col1BtnDeliveryDetail, mGridRow).Tag.Dgl1.Item(FrmWorkOrderDelivery.Col1DeliveryDate, I).Value <> "" Then
                mQry = " UPDATE WorkOrderDeliveryDetail " & _
                         " SET DeliveryDate = " & AgL.Chk_Text(Dgl1.Item(Col1BtnDeliveryDetail, mGridRow).Tag.Dgl1.Item(FrmWorkOrderDelivery.Col1DeliveryDate, I).Value) & ", " & _
                         " Item = " & AgL.Chk_Text(Dgl1.Item(Col1Item, mGridRow).Tag) & ", " & _
                         " Qty = " & Val(Dgl1.Item(Col1BtnDeliveryDetail, mGridRow).Tag.Dgl1.Item(FrmWorkOrderDelivery.Col1Qty, I).Value) & ", " & _
                         " Unit = " & AgL.Chk_Text(Dgl1.Item(Col1BtnDeliveryDetail, mGridRow).Tag.Dgl1.Item(FrmWorkOrderDelivery.Col1Unit, I).Value) & ", " & _
                         " MeasurePerPcs = " & Val(Dgl1.Item(Col1BtnDeliveryDetail, mGridRow).Tag.Dgl1.Item(FrmWorkOrderDelivery.Col1MeasurePerPcs, I).Value) & ", " & _
                         " MeasureUnit = " & AgL.Chk_Text(Dgl1.Item(Col1BtnDeliveryDetail, mGridRow).Tag.Dgl1.Item(FrmWorkOrderDelivery.Col1MeasureUnit, I).Value) & ", " & _
                         " TotalMeasure = " & Val(Dgl1.Item(Col1BtnDeliveryDetail, mGridRow).Tag.Dgl1.Item(FrmWorkOrderDelivery.Col1TotalMeasure, I).Value) & ", " & _
                         " DeliveryInstructions = " & AgL.Chk_Text(Dgl1.Item(Col1BtnDeliveryDetail, mGridRow).Tag.Dgl1.Item(FrmWorkOrderDelivery.Col1DeliveryInstruction, I).Value) & " " & _
                         " Where DocId = '" & mSearchCode & "' " & _
                         " And TSr = " & TSr & " " & _
                         " And Sr = " & Dgl1.Item(Col1BtnDeliveryDetail, mGridRow).Tag.Dgl1.Item(FrmWorkOrderDelivery.ColSNo, I).Tag & " "
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            End If
        Next
    End Sub

    Private Sub Dgl1_EditingControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.EditingControl_KeyDown
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
                        FCreateHelpItem()
                    End If

                Case Col1Item
                    If e.KeyCode = Keys.Insert Then
                        Dim FrmObj As Object = Nothing
                        Dim CFOpen As New ClsFunction
                        Dim MDI As New MDIMain
                        FrmObj = CFOpen.FOpen("MnuItemMaster", "Item Master", True)
                        If FrmObj IsNot Nothing Then
                            FrmObj.StartPosition = FormStartPosition.Manual
                            FrmObj.IsReturnValue = True
                            FrmObj.Top = 50
                            FrmObj.ShowDialog()
                            bItemCode = FrmObj.mItemCode
                            FrmObj = Nothing

                            Dgl1.Item(Col1Item, bRowIndex).Value = ""
                            Dgl1.Item(Col1Item, bRowIndex).Tag = ""

                            Dgl1.CurrentCell = Dgl1.Item(Col1Specification, bRowIndex)

                            mQry = "SELECT I.Code, I.Description, I.ManualCode, I.Unit, I.SalesTaxPostingGroup, I.Measure As MeasurePerPcs, " & _
                                      " I.MeasureUnit, I.Rate, " & _
                                      " U.DecimalPlaces As QtyDecimalPlaces, U1.DecimalPlaces As MeasureDecimalPlaces, I.BillingOn  " & _
                                      " FROM Item I " & _
                                      " LEFT JOIN Unit U On I.Unit = U.Code " & _
                                      " LEFT JOIN Unit U1 On I.MeasureUnit = U1.Code " & _
                                      " Where IsNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' "
                            Dgl1.AgHelpDataSet(Col1Item, 7) = AgL.FillData(mQry, AgL.GCn)

                            If Dgl1.AgHelpDataSet(Col1Item) IsNot Nothing Then
                                DrTemp = Dgl1.AgHelpDataSet(Col1Item).Tables(0).Select("Code = '" & bItemCode & "'")
                                If DrTemp.Length > 0 Then
                                    Dgl1.Item(Col1Item, bRowIndex).Tag = AgL.XNull(DrTemp(0)("Code"))
                                    Dgl1.Item(Col1Item, bRowIndex).Value = AgL.XNull(DrTemp(0)("Description"))
                                    Dgl1.Item(Col1ItemCode, bRowIndex).Tag = AgL.XNull(DrTemp(0)("Code"))
                                    Dgl1.Item(Col1ItemCode, bRowIndex).Value = AgL.XNull(DrTemp(0)("ManualCode"))
                                    Dgl1.Item(Col1Unit, bRowIndex).Value = AgL.XNull(DrTemp(0)("Unit"))
                                    Dgl1.Item(Col1QtyDecimalPlaces, bRowIndex).Value = AgL.VNull(DrTemp(0)("QtyDecimalPlaces"))
                                    'Dgl1.Item(Col1MeasurePerPcs, bRowIndex).Value = AgL.XNull(DrTemp(0)("MeasurePerPcs"))
                                    'Dgl1.Item(Col1MeasureUnit, bRowIndex).Value = AgL.XNull(DrTemp(0)("MeasureUnit"))
                                    'Dgl1.Item(Col1MeasureDecimalPlaces, bRowIndex).Value = AgL.VNull(DrTemp(0)("MeasureDecimalPlaces"))
                                    Dgl1.Item(Col1DeliveryMeasureUnit, bRowIndex).Value = AgL.XNull(DrTemp(0)("MeasureUnit"))
                                    Dgl1.Item(Col1DeliveryMeasureMultiplier, bRowIndex).Value = 1
                                    Dgl1.Item(Col1DeliveryMeasureDecimalPlaces, bRowIndex).Value = AgL.VNull(DrTemp(0)("MeasureDecimalPlaces"))
                                    Dgl1.Item(Col1Rate, bRowIndex).Value = AgL.XNull(DrTemp(0)("Rate"))
                                    Dgl1.Item(Col1SalesTaxGroup, bRowIndex).Tag = AgL.XNull(DrTemp(0)("SalesTaxPostingGroup"))
                                    If AgL.StrCmp(Dgl1.AgSelectedValue(Col1SalesTaxGroup, bRowIndex), "") Then
                                        Dgl1.Item(Col1SalesTaxGroup, bRowIndex).Tag = AgL.XNull(AgL.PubDtEnviro.Rows(0)("DefaultSalesTaxGroupItem"))
                                    End If
                                    'If Dgl1.Item(Col1MeasureUnit, bRowIndex).Value = "" Then Dgl1.Item(Col1TotalMeasure, bRowIndex).ReadOnly = True
                                    'ClsMain.FGetItemRate(Dgl1.Item(Col1Item, bRowIndex).Tag, Dgl1.Item(Col1ItemRateGroup, bRowIndex).Tag, TxtV_Date.Text, TxtParty.Tag, "", Dgl1.Item(Col1Rate, bRowIndex).Value, Dgl1.Item(Col1SaleQuotationRatePerQty, bRowIndex).Value, Dgl1.Item(Col1SaleQuotationRatePerMeasure, bRowIndex).Value, Dgl1.Item(Col1SaleQuotation, bRowIndex).Tag, Dgl1.Item(Col1SaleQuotation, bRowIndex).Value, Dgl1.Item(Col1SaleQuotationSr, bRowIndex).Value, Dgl1.Item(Col1Qty, bRowIndex).Value)
                                End If
                            End If
                        End If
                    Else
                        If Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) Is Nothing Then
                            FCreateHelpItem()
                        End If
                    End If


                Case Col1DeliveryMeasureUnit
                    If Dgl1.AgHelpDataSet(Col1DeliveryMeasureUnit) Is Nothing Then
                        mQry = " SELECT Code, Code AS Name FROM Unit Where IsNull(IsActive,1) <> 0  "
                        Dgl1.AgHelpDataSet(Col1DeliveryMeasureUnit) = AgL.FillData(mQry, AgL.GCn)
                    End If


                Case Col1ItemRateGroup
                    If Dgl1.AgHelpDataSet(Col1ItemRateGroup) Is Nothing Then
                        mQry = " SELECT H.Code, H.Description  FROM RateType H " & _
                                " Where IsNull(H.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' "
                        Dgl1.AgHelpDataSet(Col1ItemRateGroup) = AgL.FillData(mQry, AgL.GCn)
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FrmSaleInvoice_BaseEvent_Topctrl_tbRef() Handles Me.BaseEvent_Topctrl_tbRef
        If Dgl1.AgHelpDataSet(Col1ItemCode) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1ItemCode).Dispose() : Dgl1.AgHelpDataSet(Col1ItemCode) = Nothing
        If Dgl1.AgHelpDataSet(Col1Item) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1Item).Dispose() : Dgl1.AgHelpDataSet(Col1Item) = Nothing
        If TxtCurrency.AgHelpDataSet IsNot Nothing Then TxtCurrency.AgHelpDataSet.Dispose() : TxtCurrency.AgHelpDataSet = Nothing
        If TxtParty.AgHelpDataSet IsNot Nothing Then TxtParty.AgHelpDataSet.Dispose() : TxtParty.AgHelpDataSet = Nothing
        If TxtSalesTaxGroupParty.AgHelpDataSet IsNot Nothing Then TxtSalesTaxGroupParty.AgHelpDataSet.Dispose() : TxtSalesTaxGroupParty.AgHelpDataSet = Nothing
        If TxtShipToParty.AgHelpDataSet IsNot Nothing Then TxtShipToParty.AgHelpDataSet.Dispose() : TxtShipToParty.AgHelpDataSet = Nothing
        If TxtReferenceParty.AgHelpDataSet IsNot Nothing Then TxtReferenceParty.AgHelpDataSet.Dispose() : TxtReferenceParty.AgHelpDataSet = Nothing
        If TxtAgent.AgHelpDataSet IsNot Nothing Then TxtAgent.AgHelpDataSet.Dispose() : TxtAgent.AgHelpDataSet = Nothing
    End Sub

    Private Sub BtnFillPartyDetail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnFillPartyDetail.Click
        FOpenPartyDetail()
    End Sub

    Private Sub FOpenPartyDetail()
        Dim FrmObj As FrmPartyDetail
        Try
            If BtnFillPartyDetail.Tag Is Nothing Then
                FrmObj = New FrmPartyDetail
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
        'Dim DtTemp As DataTable = Nothing
        Try
            'If blnIsCarpetTrans Then
            '    Dgl1.Item(Col1DeliveryMeasureMultiplier, mRow).Value = 0
            '    If AgL.StrCmp(Dgl1.Item(Col1DeliveryMeasureUnit, mRow).Value, "SQ.FEET") Then
            '        mQry = "Select FeetArea From Rug_Size Size Left Join Rug_CarpetSku Cs On Size.Code = Cs.Size Where Cs.Code = '" & Dgl1.Item(Col1Item, mRow).Tag & "' "
            '        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
            '        If DtTemp.Rows.Count > 0 Then
            '            Dgl1.Item(Col1MeasurePerPcs, mRow).Value = AgL.VNull(DtTemp.Rows(0)(0))
            '        End If
            '    ElseIf AgL.StrCmp(Dgl1.Item(Col1DeliveryMeasureUnit, mRow).Value, "SQ.METER") Then
            '        mQry = "Select MeterArea From Rug_Size Size Left Join Rug_CarpetSku Cs On Size.Code = Cs.Size Where Cs.Code = '" & Dgl1.Item(Col1Item, mRow).Tag & "' "
            '        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
            '        If DtTemp.Rows.Count > 0 Then
            '            Dgl1.Item(Col1MeasurePerPcs, mRow).Value = AgL.VNull(DtTemp.Rows(0)(0))
            '        End If
            '    Else
            '        'Dgl1.Item(Col1DeliveryMeasurePerPcs, mRow).Value = Dgl1.Item(Col1MeasurePerPcs, mRow).Value
            '        'Dgl1.Item(Col1DeliveryMeasure, mRow).Value = Dgl1.Item(Col1MeasureUnit, mRow).Value
            '        'Dgl1.Item(Col1DeliveryMeasure, mRow).Tag = Dgl1.Item(Col1MeasureUnit, mRow).Tag
            '    End If
            'Else
            'If Dgl1.Item(Col1MeasureUnit, mRow).Value <> "" And Dgl1.Item(Col1DeliveryMeasure, mRow).Value <> "" Then
            '    If Dgl1.Item(Col1MeasureUnit, mRow).Value = Dgl1.Item(Col1DeliveryMeasure, mRow).Value Then
            '        Dgl1.Item(Col1DeliveryMeasureMultiplier, mRow).Value = 1
            '        Dgl1.Item(Col1DeliveryMeasureDecimalPlaces, mRow).Value = Dgl1.Item(Col1MeasureDecimalPlaces, mRow).Value
            '    Else
            '        'mQry = " SELECT Multiplier, Rounding FROM UnitConversion WHERE FromUnit = '" & Dgl1.Item(Col1MeasureUnit, mRow).Value & "' AND ToUnit =  '" & Dgl1.Item(Col1DeliveryMeasure, mRow).Value & "' "
            '        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
            '        With DtTemp
            '            If .Rows.Count > 0 Then
            '                Dgl1.Item(Col1DeliveryMeasureMultiplier, mRow).Value = AgL.VNull(.Rows(0)("Multiplier"))
            '                If Dgl1.Item(Col1DeliveryMeasureMultiplier, mRow).Value = 0 Then
            '                    MsgBox("Define Multiplier In Unit Conversion To Convert " & Dgl1.Item(Col1DeliveryMeasure, mRow).Value & " From " & Dgl1.Item(Col1MeasureUnit, mRow).Value & " ", MsgBoxStyle.Information)
            '                    Dgl1.Item(Col1DeliveryMeasure, mRow).Value = ""
            '                Else
            '                    mQry = " Select DecimalPlaces From Unit Where Code = '" & Dgl1.Item(Col1DeliveryMeasure, mRow).Value & "'"
            '                    Dgl1.Item(Col1DeliveryMeasureDecimalPlaces, mRow).Value = AgL.VNull(AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar)
            '                End If
            '            Else
            '                MsgBox("Define Multiplier In Unit Conversion To Convert " & Dgl1.Item(Col1DeliveryMeasure, mRow).Value & " From " & Dgl1.Item(Col1MeasureUnit, mRow).Value & " ", MsgBoxStyle.Information)
            '                Dgl1.Item(Col1DeliveryMeasure, mRow).Value = ""
            '            End If
            '        End With
            '    End If
            'End If
            'End If



            If Dgl1.Item(Col1DeliveryMeasureUnit, mRow).Value = Dgl1.Item(Col1MeasureUnit, mRow).Value Then
                Dgl1.Item(Col1DeliveryMeasureMultiplier, mRow).Value = 1
            Else
                If Val(Dgl1.Item(Col1MeasurePerPcs, mRow).Value) > 0 Then
                    mQry = "SELECT Multiplier FROM UnitConversion WHERE FromUnit = '" & Dgl1.Item(Col1DeliveryMeasureUnit, mRow).Value & "' AND ToUnit = '" & Dgl1.Item(Col1MeasureUnit, mRow).Value & "' "
                    Dgl1.Item(Col1DeliveryMeasureMultiplier, mRow).Value = Val(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar)
                Else
                    mQry = "SELECT Multiplier FROM UnitConversion WHERE FromUnit = '" & Dgl1.Item(Col1DeliveryMeasureUnit, mRow).Value & "' AND ToUnit = '" & Dgl1.Item(Col1Unit, mRow).Value & "' "
                    Dgl1.Item(Col1DeliveryMeasureMultiplier, mRow).Value = Val(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar)
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
                        If Val(Dgl1.Item(Col1BtnDeliveryDetail, I).Tag.Dgl1.Item(FrmWorkOrderDelivery.Col1Qty, J).Value) <> 0 Then
                            Dgl1.Item(Col1BtnDeliveryDetail, I).Tag.Dgl1.Item(FrmWorkOrderDelivery.Col1DeliveryDate, J).Value = ""
                        End If
                    Next
                End If
            Next
        Else
            TxtDeliveryDate.Enabled = True
        End If
    End Sub

    Private Sub FFormatRateCells(ByVal mRow As Integer)
        'If AgL.StrCmp(Dgl1.Item(Col1BillingType, mRow).Value, "Qty") Or Dgl1.Item(Col1BillingType, mRow).Value = "" Then
        '    If Val(Dgl1.Item(Col1SaleQuotationRatePerQty, mRow).Value) = 0 Then
        '        Dgl1.Item(Col1Rate, mRow).Style.Font = New Font(Dgl1.DefaultCellStyle.Font.FontFamily, Dgl1.DefaultCellStyle.Font.Size, FontStyle.Regular)
        '        Dgl1.Item(Col1Rate, mRow).Style.ForeColor = Color.Black
        '    ElseIf Val(Dgl1.Item(Col1SaleQuotationRatePerQty, mRow).Value) < Val(Dgl1.Item(Col1RatePerQty, mRow).Value) Then
        '        Dgl1.Item(Col1Rate, mRow).Style.Font = New Font(Dgl1.DefaultCellStyle.Font.FontFamily, Dgl1.DefaultCellStyle.Font.Size, FontStyle.Bold)
        '        Dgl1.Item(Col1Rate, mRow).Style.ForeColor = Color.Red
        '    ElseIf Val(Dgl1.Item(Col1SaleQuotationRatePerQty, mRow).Value) > Val(Dgl1.Item(Col1RatePerQty, mRow).Value) Then
        '        Dgl1.Item(Col1Rate, mRow).Style.Font = New Font(Dgl1.DefaultCellStyle.Font.FontFamily, Dgl1.DefaultCellStyle.Font.Size, FontStyle.Bold)
        '        Dgl1.Item(Col1Rate, mRow).Style.ForeColor = Color.Green
        '    Else
        '        Dgl1.Item(Col1Rate, mRow).Style.Font = New Font(Dgl1.DefaultCellStyle.Font.FontFamily, Dgl1.DefaultCellStyle.Font.Size, FontStyle.Regular)
        '        Dgl1.Item(Col1Rate, mRow).Style.ForeColor = Color.Black
        '    End If
        'Else
        'If Val(Dgl1.Item(Col1SaleQuotationRatePerMeasure, mRow).Value) = 0 Then
        '    Dgl1.Item(Col1Rate, mRow).Style.Font = New Font(Dgl1.DefaultCellStyle.Font.FontFamily, Dgl1.DefaultCellStyle.Font.Size, FontStyle.Regular)
        '    Dgl1.Item(Col1Rate, mRow).Style.ForeColor = Color.Black
        'ElseIf Val(Dgl1.Item(Col1SaleQuotationRatePerMeasure, mRow).Value) < Val(Dgl1.Item(Col1RatePerMeasure, mRow).Value) Then
        '    Dgl1.Item(Col1Rate, mRow).Style.Font = New Font(Dgl1.DefaultCellStyle.Font.FontFamily, Dgl1.DefaultCellStyle.Font.Size, FontStyle.Bold)
        '    Dgl1.Item(Col1Rate, mRow).Style.ForeColor = Color.Red
        'ElseIf Val(Dgl1.Item(Col1SaleQuotationRatePerMeasure, mRow).Value) > Val(Dgl1.Item(Col1RatePerMeasure, mRow).Value) Then
        '    Dgl1.Item(Col1Rate, mRow).Style.Font = New Font(Dgl1.DefaultCellStyle.Font.FontFamily, Dgl1.DefaultCellStyle.Font.Size, FontStyle.Bold)
        '    Dgl1.Item(Col1Rate, mRow).Style.ForeColor = Color.Green
        'Else
        '    Dgl1.Item(Col1Rate, mRow).Style.Font = New Font(Dgl1.DefaultCellStyle.Font.FontFamily, Dgl1.DefaultCellStyle.Font.Size, FontStyle.Regular)
        '    Dgl1.Item(Col1Rate, mRow).Style.ForeColor = Color.Black
        'End If
        'End If
    End Sub

    Private Sub FCreateHelpItem()
        Dim strCond As String = ""
        If DtV_TypeSettings.Rows.Count > 0 Then
            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")) <> "" Then
                strCond += " And CharIndex('|' + H.ItemType + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")) & "') > 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemGroup")) <> "" Then
                strCond += " And CharIndex('|' + H.ItemGroup + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemGroup")) & "') > 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_ItemGroup")) <> "" Then
                strCond += " And CharIndex('|' + H.ItemGroup + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_ItemGroup")) & "') <= 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Item")) <> "" Then
                strCond += " And CharIndex('|' + H.Code + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Item")) & "') > 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_Item")) <> "" Then
                strCond += " And CharIndex('|' + H.Item + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_Item")) & "') <= 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemDivision")) <> "" Then
                strCond += " And CharIndex('|' + H.Div_Code + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemDivision")) & "') > 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemSite")) <> "" Then
                strCond += " And CharIndex('|' + H.Site_Code + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemSite")) & "') > 0 "
            End If
        End If

        If AgL.StrCmp(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name, Col1Item) Then
            mQry = "SELECT H.Code, H.Description,  H.ManualCode, IG.Description AS [Item Group], IC.Description AS [Item Category],IT.Name AS [Item Type], H.Unit, H.SalesTaxPostingGroup, ISNULL(H.Measure,1) As MeasurePerPcs, " & _
                      " ISNULL(H.MeasureUnit,H.Unit) AS MeasureUnit, H.Rate, H.Specification, " & _
                      " U.DecimalPlaces As QtyDecimalPlaces, ISNULL(U1.DecimalPlaces,U.DecimalPlaces) As MeasureDecimalPlaces, H.BillingOn  " & _
                      " FROM Item H " & _
                      " LEFT JOIN ItemGroup IG ON IG.Code = H.ItemGroup  " & _
                      " LEFT JOIN ItemCategory IC ON IC.Code = H.ItemCategory  " & _
                      " LEFT JOIN ItemType IT ON IT.Code = H.ItemType " & _
                      " LEFT JOIN Unit U On H.Unit = U.Code " & _
                      " LEFT JOIN Unit U1 On H.MeasureUnit = U1.Code " & _
                      " Where IsNull(H.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "')='" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
            Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex, 9) = AgL.FillData(mQry, AgL.GCn)
        End If

        If AgL.StrCmp(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name, Col1ItemCode) Then
            mQry = "SELECT H.Code, H.ManualCode, H.Description, IG.Description AS [Item Group], IC.Description AS [Item Category],IT.Name AS [Item Type], H.Unit, H.SalesTaxPostingGroup, H.Measure As MeasurePerPcs, " & _
                      " H.MeasureUnit, H.Rate, H.Specification, " & _
                      " U.DecimalPlaces As QtyDecimalPlaces, U1.DecimalPlaces As MeasureDecimalPlaces, H.BillingOn  " & _
                      " FROM Item H " & _
                      " LEFT JOIN ItemGroup IG ON IG.Code = H.ItemGroup  " & _
                      " LEFT JOIN ItemCategory IC ON IC.Code = H.ItemCategory  " & _
                      " LEFT JOIN ItemType IT ON IT.Code = H.ItemType " & _
                      " LEFT JOIN Unit U On H.Unit = U.Code " & _
                      " LEFT JOIN Unit U1 On H.MeasureUnit = U1.Code " & _
                      " Where IsNull(H.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "')='" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
            Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex, 9) = AgL.FillData(mQry, AgL.GCn)
        End If
    End Sub

    Private Sub FCreateHelpSubgroup()
        Dim strCond As String = ""
        If DtV_TypeSettings.Rows.Count > 0 Then
            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_AcGroup")) <> "" Then
                strCond += " And CharIndex('|' + Sg.GroupCode + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_AcGroup")) & "') > 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_AcGroup")) <> "" Then
                strCond += " And CharIndex('|' + Sg.GroupCode + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_AcGroup")) & "') <= 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_SubgroupDivision")) <> "" Then
                strCond += " And CharIndex('|' + Sg.Div_Code + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_subGroupDivision")) & "') > 0 "
            End If


            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_SubgroupSite")) <> "" Then
                strCond += " And CharIndex('|' + Sg.Site_Code + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_subGroupSite")) & "') > 0 "
            End If
        End If
        mQry = "SELECT Sg.SubCode, Sg.Name + ',' + IsNull(C.CityName,'') AS [Party], " & _
                " Sg.Currency, C1.Description As CurrencyDesc, Sg.Nature, Sg.SalesTaxPostingGroup " & _
                " FROM SubGroup Sg  " & _
                " LEFT JOIN City C ON Sg.CityCode = C.CityCode  " & _
                " LEFT JOIN Currency C1 On Sg.Currency = C1.Code " & _
                " Where IsNull(Sg.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
        TxtParty.AgHelpDataSet(4, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
    End Sub
End Class

