Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data.SQLite
Public Class FrmPurchChallanReturn
    Inherits AgTemplate.TempTransaction
    Dim mQry$

    Public WithEvents AgCalcGrid1 As New AgStructure.AgCalcGrid
    Public WithEvents AgCustomGrid1 As New AgCustomFields.AgCustomGrid

    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Protected Const ColSNo As String = "S.No."
    Protected Const Col1ItemCode As String = "Item Code"
    Protected Const Col1Item As String = "Item"
    Protected Const Col1PurchChallan As String = "Challan No"
    Protected Const Col1PurchChallanSr As String = "Purch Challan Sr"
    Protected Const Col1Item_UID As String = "Item UID"
    Protected Const Col1BaleNo As String = "Bale No"
    Protected Const Col1LotNo As String = "Lot No"
    Protected Const Col1SalesTaxGroup As String = "Sales Tax Group Item"
    Protected Const Col1DocQty As String = "Doc Qty"
    Protected Const Col1FreeQty As String = "Free Qty"
    Protected Const Col1Qty As String = "Qty"
    Protected Const Col1Unit As String = "Unit"
    Protected Const Col1QtyDecimalPlaces As String = "Qty Decimal Places"
    Protected Const Col1MeasurePerPcs As String = "Measure Per Pcs"
    Protected Const Col1PcsPerMeasure As String = "Pcs Per Measure"
    Protected Const Col1TotalDocMeasure As String = "Total Doc. Measure"
    Protected Const Col1TotalFreeMeasure As String = "Total Free Measure"
    Protected Const Col1TotalMeasure As String = "Total Measure"
    Protected Const Col1MeasureUnit As String = "Measure Unit"
    Protected Const Col1MeasureDecimalPlaces As String = "Measure Decimal Places"
    Protected Const Col1DeliveryMeasure As String = "Delivery Measure"
    Protected Const Col1DeliveryMeasureMultiplier As String = "Delivery Measure Multiplier"
    Protected Const Col1DeliveryMeasurePerPcs As String = "Delivery Measure Per Pcs"
    Protected Const Col1TotalDocDeliveryMeasure As String = "Total Doc. Delivery Measure"
    Protected Const Col1TotalFreeDeliveryMeasure As String = "Total Free Delivery Measure"
    Protected Const Col1TotalDeliveryMeasure As String = "Total Delivery Measure"
    Protected Const Col1DeliveryMeasureDecimalPlaces As String = "Delivery Measure Decimal Place"
    Protected Const Col1BillingType As String = "Billing Type"
    Protected Const Col1Rate As String = "Rate"
    Protected Const Col1Amount As String = "Amount"
    Protected Const Col1Remark As String = "Remark"
    Protected Const Col1Deal As String = "Deal"
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents TxtGodown As AgControls.AgTextBox
    Public WithEvents LblGodown As System.Windows.Forms.Label

    Public blnIsCarpetTrans As Boolean

    Public Sub New(ByVal StrUPVar As String, ByVal DTUP As DataTable, ByVal strNCat As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Topctrl1.FSetParent(Me, StrUPVar, DTUP)
        Topctrl1.SetDisp(True)

        If EntryNCat = "" Then EntryNCat = strNCat

        mQry = "Select H.* from Voucher_Type_Settings H  Left Join Voucher_Type Vt  On H.V_Type = Vt.V_Type  Where Vt.NCat In ('" & EntryNCat & "') And H.Div_Code = '" & AgL.PubDivCode & "' And H.Site_Code ='" & AgL.PubSiteCode & "' "
        DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)

    End Sub

#Region "Form Designer Code"
    Private Sub InitializeComponent()
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
        Me.BtnFillPurchChallan = New System.Windows.Forms.Button
        Me.PnlCustomGrid = New System.Windows.Forms.Panel
        Me.TxtCustomFields = New AgControls.AgTextBox
        Me.GrpDirectInvoice = New System.Windows.Forms.GroupBox
        Me.RbtnRetunForChallan = New System.Windows.Forms.RadioButton
        Me.RbtReturnDirect = New System.Windows.Forms.RadioButton
        Me.Label3 = New System.Windows.Forms.Label
        Me.TxtGodown = New AgControls.AgTextBox
        Me.LblGodown = New System.Windows.Forms.Label
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
        Me.Label2.Location = New System.Drawing.Point(348, 39)
        Me.Label2.Tag = ""
        '
        'LblV_Date
        '
        Me.LblV_Date.BackColor = System.Drawing.Color.Transparent
        Me.LblV_Date.Location = New System.Drawing.Point(244, 34)
        Me.LblV_Date.Size = New System.Drawing.Size(77, 16)
        Me.LblV_Date.Tag = ""
        Me.LblV_Date.Text = "Return Date"
        '
        'LblV_TypeReq
        '
        Me.LblV_TypeReq.Location = New System.Drawing.Point(560, 19)
        Me.LblV_TypeReq.Tag = ""
        '
        'TxtV_Date
        '
        Me.TxtV_Date.AgSelectedValue = ""
        Me.TxtV_Date.BackColor = System.Drawing.Color.White
        Me.TxtV_Date.Location = New System.Drawing.Point(364, 33)
        Me.TxtV_Date.TabIndex = 2
        Me.TxtV_Date.Tag = ""
        '
        'LblV_Type
        '
        Me.LblV_Type.Location = New System.Drawing.Point(470, 15)
        Me.LblV_Type.Size = New System.Drawing.Size(77, 16)
        Me.LblV_Type.Tag = ""
        Me.LblV_Type.Text = "Return Type"
        '
        'TxtV_Type
        '
        Me.TxtV_Type.AgSelectedValue = ""
        Me.TxtV_Type.BackColor = System.Drawing.Color.White
        Me.TxtV_Type.Location = New System.Drawing.Point(578, 13)
        Me.TxtV_Type.Size = New System.Drawing.Size(163, 18)
        Me.TxtV_Type.TabIndex = 1
        Me.TxtV_Type.Tag = ""
        '
        'LblSite_CodeReq
        '
        Me.LblSite_CodeReq.Location = New System.Drawing.Point(348, 19)
        Me.LblSite_CodeReq.Tag = ""
        '
        'LblSite_Code
        '
        Me.LblSite_Code.BackColor = System.Drawing.Color.Transparent
        Me.LblSite_Code.Location = New System.Drawing.Point(244, 14)
        Me.LblSite_Code.Size = New System.Drawing.Size(87, 16)
        Me.LblSite_Code.Tag = ""
        Me.LblSite_Code.Text = "Branch Name"
        '
        'TxtSite_Code
        '
        Me.TxtSite_Code.AgSelectedValue = ""
        Me.TxtSite_Code.BackColor = System.Drawing.Color.White
        Me.TxtSite_Code.Location = New System.Drawing.Point(364, 13)
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
        Me.TabControl1.Size = New System.Drawing.Size(992, 128)
        Me.TabControl1.TabIndex = 0
        '
        'TP1
        '
        Me.TP1.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.TP1.Controls.Add(Me.Label1)
        Me.TP1.Controls.Add(Me.Label4)
        Me.TP1.Controls.Add(Me.TxtVendor)
        Me.TP1.Controls.Add(Me.LblVendor)
        Me.TP1.Controls.Add(Me.Label30)
        Me.TP1.Controls.Add(Me.Label25)
        Me.TP1.Controls.Add(Me.TxtRemarks)
        Me.TP1.Controls.Add(Me.TxtReferenceNo)
        Me.TP1.Controls.Add(Me.TxtStructure)
        Me.TP1.Controls.Add(Me.LblReferenceNo)
        Me.TP1.Location = New System.Drawing.Point(4, 22)
        Me.TP1.Size = New System.Drawing.Size(984, 102)
        Me.TP1.Text = "Document Detail"
        Me.TP1.Controls.SetChildIndex(Me.LblReferenceNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtStructure, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtReferenceNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtRemarks, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label25, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblPrefix, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_No, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label30, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtV_No, 0)
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
        '
        'Topctrl1
        '
        Me.Topctrl1.Size = New System.Drawing.Size(984, 41)
        Me.Topctrl1.TabIndex = 3
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
        Me.Label4.Location = New System.Drawing.Point(348, 60)
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
        Me.TxtVendor.Location = New System.Drawing.Point(364, 53)
        Me.TxtVendor.MaxLength = 0
        Me.TxtVendor.Name = "TxtVendor"
        Me.TxtVendor.Size = New System.Drawing.Size(377, 18)
        Me.TxtVendor.TabIndex = 4
        '
        'LblVendor
        '
        Me.LblVendor.AutoSize = True
        Me.LblVendor.BackColor = System.Drawing.Color.Transparent
        Me.LblVendor.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblVendor.Location = New System.Drawing.Point(244, 53)
        Me.LblVendor.Name = "LblVendor"
        Me.LblVendor.Size = New System.Drawing.Size(48, 16)
        Me.LblVendor.TabIndex = 693
        Me.LblVendor.Text = "Vendor"
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
        Me.LblTotalDeliveryMeasure.Location = New System.Drawing.Point(670, 3)
        Me.LblTotalDeliveryMeasure.Name = "LblTotalDeliveryMeasure"
        Me.LblTotalDeliveryMeasure.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalDeliveryMeasure.TabIndex = 714
        Me.LblTotalDeliveryMeasure.Text = "."
        '
        'LblTotalDeliveryMeasureText
        '
        Me.LblTotalDeliveryMeasureText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalDeliveryMeasureText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalDeliveryMeasureText.Location = New System.Drawing.Point(466, 0)
        Me.LblTotalDeliveryMeasureText.Name = "LblTotalDeliveryMeasureText"
        Me.LblTotalDeliveryMeasureText.Size = New System.Drawing.Size(198, 19)
        Me.LblTotalDeliveryMeasureText.TabIndex = 713
        Me.LblTotalDeliveryMeasureText.Text = "Deilvery Measure :"
        Me.LblTotalDeliveryMeasureText.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LblTotalMeasure
        '
        Me.LblTotalMeasure.AutoSize = True
        Me.LblTotalMeasure.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalMeasure.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalMeasure.Location = New System.Drawing.Point(376, 3)
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
        Me.LblTotalMeasureText.Location = New System.Drawing.Point(218, 3)
        Me.LblTotalMeasureText.Name = "LblTotalMeasureText"
        Me.LblTotalMeasureText.Size = New System.Drawing.Size(152, 16)
        Me.LblTotalMeasureText.TabIndex = 665
        Me.LblTotalMeasureText.Text = "Total Measure :"
        Me.LblTotalMeasureText.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LblTotalQty
        '
        Me.LblTotalQty.AutoSize = True
        Me.LblTotalQty.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalQty.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalQty.Location = New System.Drawing.Point(124, 3)
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
        Me.LblTotalAmount.Location = New System.Drawing.Point(883, 3)
        Me.LblTotalAmount.Name = "LblTotalAmount"
        Me.LblTotalAmount.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalAmount.TabIndex = 662
        Me.LblTotalAmount.Text = "."
        Me.LblTotalAmount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTotalQtyText
        '
        Me.LblTotalQtyText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalQtyText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalQtyText.Location = New System.Drawing.Point(12, 3)
        Me.LblTotalQtyText.Name = "LblTotalQtyText"
        Me.LblTotalQtyText.Size = New System.Drawing.Size(106, 16)
        Me.LblTotalQtyText.TabIndex = 659
        Me.LblTotalQtyText.Text = "Total Qty :"
        Me.LblTotalQtyText.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LblTotalAmountText
        '
        Me.LblTotalAmountText.AutoSize = True
        Me.LblTotalAmountText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalAmountText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalAmountText.Location = New System.Drawing.Point(779, 3)
        Me.LblTotalAmountText.Name = "LblTotalAmountText"
        Me.LblTotalAmountText.Size = New System.Drawing.Size(100, 16)
        Me.LblTotalAmountText.TabIndex = 661
        Me.LblTotalAmountText.Text = "Total Amount :"
        '
        'Pnl1
        '
        Me.Pnl1.Location = New System.Drawing.Point(4, 175)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(975, 211)
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
        Me.TxtSalesTaxGroupParty.Location = New System.Drawing.Point(279, 415)
        Me.TxtSalesTaxGroupParty.MaxLength = 20
        Me.TxtSalesTaxGroupParty.Name = "TxtSalesTaxGroupParty"
        Me.TxtSalesTaxGroupParty.Size = New System.Drawing.Size(107, 18)
        Me.TxtSalesTaxGroupParty.TabIndex = 4
        '
        'Label27
        '
        Me.Label27.AutoSize = True
        Me.Label27.BackColor = System.Drawing.Color.Transparent
        Me.Label27.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label27.Location = New System.Drawing.Point(170, 416)
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
        Me.TxtRemarks.Location = New System.Drawing.Point(364, 73)
        Me.TxtRemarks.MaxLength = 255
        Me.TxtRemarks.Name = "TxtRemarks"
        Me.TxtRemarks.Size = New System.Drawing.Size(377, 18)
        Me.TxtRemarks.TabIndex = 5
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(244, 77)
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
        Me.TxtReferenceNo.Location = New System.Drawing.Point(578, 33)
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
        Me.LblReferenceNo.Location = New System.Drawing.Point(470, 33)
        Me.LblReferenceNo.Name = "LblReferenceNo"
        Me.LblReferenceNo.Size = New System.Drawing.Size(70, 16)
        Me.LblReferenceNo.TabIndex = 731
        Me.LblReferenceNo.Text = "Return No."
        '
        'LblCurrency
        '
        Me.LblCurrency.AutoSize = True
        Me.LblCurrency.BackColor = System.Drawing.Color.Transparent
        Me.LblCurrency.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCurrency.Location = New System.Drawing.Point(4, 415)
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
        Me.TxtCurrency.Location = New System.Drawing.Point(71, 415)
        Me.TxtCurrency.MaxLength = 20
        Me.TxtCurrency.Name = "TxtCurrency"
        Me.TxtCurrency.Size = New System.Drawing.Size(93, 18)
        Me.TxtCurrency.TabIndex = 3
        '
        'LinkLabel1
        '
        Me.LinkLabel1.BackColor = System.Drawing.Color.SteelBlue
        Me.LinkLabel1.DisabledLinkColor = System.Drawing.Color.White
        Me.LinkLabel1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel1.LinkColor = System.Drawing.Color.White
        Me.LinkLabel1.Location = New System.Drawing.Point(4, 152)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(230, 20)
        Me.LinkLabel1.TabIndex = 739
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "Purchase Return For Following Items"
        Me.LinkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'PnlCalcGrid
        '
        Me.PnlCalcGrid.Location = New System.Drawing.Point(670, 415)
        Me.PnlCalcGrid.Name = "PnlCalcGrid"
        Me.PnlCalcGrid.Size = New System.Drawing.Size(310, 160)
        Me.PnlCalcGrid.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(560, 39)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(10, 7)
        Me.Label1.TabIndex = 737
        Me.Label1.Text = "Ä"
        '
        'BtnFillPurchChallan
        '
        Me.BtnFillPurchChallan.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnFillPurchChallan.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnFillPurchChallan.ForeColor = System.Drawing.Color.Black
        Me.BtnFillPurchChallan.Location = New System.Drawing.Point(543, 152)
        Me.BtnFillPurchChallan.Margin = New System.Windows.Forms.Padding(0)
        Me.BtnFillPurchChallan.Name = "BtnFillPurchChallan"
        Me.BtnFillPurchChallan.Size = New System.Drawing.Size(30, 23)
        Me.BtnFillPurchChallan.TabIndex = 1
        Me.BtnFillPurchChallan.Text = "..."
        Me.BtnFillPurchChallan.UseVisualStyleBackColor = True
        '
        'PnlCustomGrid
        '
        Me.PnlCustomGrid.Location = New System.Drawing.Point(4, 458)
        Me.PnlCustomGrid.Name = "PnlCustomGrid"
        Me.PnlCustomGrid.Size = New System.Drawing.Size(382, 117)
        Me.PnlCustomGrid.TabIndex = 3
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
        Me.TxtCustomFields.Location = New System.Drawing.Point(698, 587)
        Me.TxtCustomFields.MaxLength = 20
        Me.TxtCustomFields.Name = "TxtCustomFields"
        Me.TxtCustomFields.Size = New System.Drawing.Size(72, 18)
        Me.TxtCustomFields.TabIndex = 1012
        Me.TxtCustomFields.Text = "AgTextBox1"
        Me.TxtCustomFields.Visible = False
        '
        'GrpDirectInvoice
        '
        Me.GrpDirectInvoice.BackColor = System.Drawing.Color.Transparent
        Me.GrpDirectInvoice.Controls.Add(Me.RbtnRetunForChallan)
        Me.GrpDirectInvoice.Controls.Add(Me.RbtReturnDirect)
        Me.GrpDirectInvoice.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GrpDirectInvoice.Location = New System.Drawing.Point(247, 146)
        Me.GrpDirectInvoice.Name = "GrpDirectInvoice"
        Me.GrpDirectInvoice.Size = New System.Drawing.Size(293, 26)
        Me.GrpDirectInvoice.TabIndex = 1013
        Me.GrpDirectInvoice.TabStop = False
        '
        'RbtnRetunForChallan
        '
        Me.RbtnRetunForChallan.AutoSize = True
        Me.RbtnRetunForChallan.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RbtnRetunForChallan.Location = New System.Drawing.Point(134, 7)
        Me.RbtnRetunForChallan.Name = "RbtnRetunForChallan"
        Me.RbtnRetunForChallan.Size = New System.Drawing.Size(146, 17)
        Me.RbtnRetunForChallan.TabIndex = 1014
        Me.RbtnRetunForChallan.TabStop = True
        Me.RbtnRetunForChallan.Text = "Return For Challan"
        Me.RbtnRetunForChallan.UseVisualStyleBackColor = True
        '
        'RbtReturnDirect
        '
        Me.RbtReturnDirect.AutoSize = True
        Me.RbtReturnDirect.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RbtReturnDirect.Location = New System.Drawing.Point(8, 7)
        Me.RbtReturnDirect.Name = "RbtReturnDirect"
        Me.RbtReturnDirect.Size = New System.Drawing.Size(111, 17)
        Me.RbtReturnDirect.TabIndex = 0
        Me.RbtReturnDirect.TabStop = True
        Me.RbtReturnDirect.Text = "Return Direct"
        Me.RbtReturnDirect.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(57, 444)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(10, 7)
        Me.Label3.TabIndex = 1016
        Me.Label3.Text = "Ä"
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
        Me.TxtGodown.Location = New System.Drawing.Point(71, 435)
        Me.TxtGodown.MaxLength = 0
        Me.TxtGodown.Name = "TxtGodown"
        Me.TxtGodown.Size = New System.Drawing.Size(315, 18)
        Me.TxtGodown.TabIndex = 1014
        '
        'LblGodown
        '
        Me.LblGodown.AutoSize = True
        Me.LblGodown.BackColor = System.Drawing.Color.Transparent
        Me.LblGodown.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblGodown.Location = New System.Drawing.Point(3, 439)
        Me.LblGodown.Name = "LblGodown"
        Me.LblGodown.Size = New System.Drawing.Size(55, 16)
        Me.LblGodown.TabIndex = 1015
        Me.LblGodown.Text = "Godown"
        '
        'FrmPurchChallanReturn
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.BackColor = System.Drawing.SystemColors.ButtonShadow
        Me.ClientSize = New System.Drawing.Size(984, 622)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TxtGodown)
        Me.Controls.Add(Me.LblGodown)
        Me.Controls.Add(Me.GrpDirectInvoice)
        Me.Controls.Add(Me.TxtCustomFields)
        Me.Controls.Add(Me.PnlCustomGrid)
        Me.Controls.Add(Me.BtnFillPurchChallan)
        Me.Controls.Add(Me.PnlCalcGrid)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Pnl1)
        Me.Controls.Add(Me.TxtSalesTaxGroupParty)
        Me.Controls.Add(Me.Label27)
        Me.Controls.Add(Me.LblCurrency)
        Me.Controls.Add(Me.TxtCurrency)
        Me.Name = "FrmPurchChallanReturn"
        Me.Text = "Purchase Return"
        Me.Controls.SetChildIndex(Me.TxtCurrency, 0)
        Me.Controls.SetChildIndex(Me.LblCurrency, 0)
        Me.Controls.SetChildIndex(Me.Label27, 0)
        Me.Controls.SetChildIndex(Me.TxtSalesTaxGroupParty, 0)
        Me.Controls.SetChildIndex(Me.TabControl1, 0)
        Me.Controls.SetChildIndex(Me.Pnl1, 0)
        Me.Controls.SetChildIndex(Me.Panel1, 0)
        Me.Controls.SetChildIndex(Me.LinkLabel1, 0)
        Me.Controls.SetChildIndex(Me.PnlCalcGrid, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.GrpUP, 0)
        Me.Controls.SetChildIndex(Me.GBoxEntryType, 0)
        Me.Controls.SetChildIndex(Me.GBoxApprove, 0)
        Me.Controls.SetChildIndex(Me.GBoxMoveToLog, 0)
        Me.Controls.SetChildIndex(Me.GroupBox2, 0)
        Me.Controls.SetChildIndex(Me.GBoxDivision, 0)
        Me.Controls.SetChildIndex(Me.Topctrl1, 0)
        Me.Controls.SetChildIndex(Me.BtnFillPurchChallan, 0)
        Me.Controls.SetChildIndex(Me.PnlCustomGrid, 0)
        Me.Controls.SetChildIndex(Me.TxtCustomFields, 0)
        Me.Controls.SetChildIndex(Me.GrpDirectInvoice, 0)
        Me.Controls.SetChildIndex(Me.LblGodown, 0)
        Me.Controls.SetChildIndex(Me.TxtGodown, 0)
        Me.Controls.SetChildIndex(Me.Label3, 0)
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
    Protected WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    Protected WithEvents PnlCalcGrid As System.Windows.Forms.Panel
    Protected WithEvents Label1 As System.Windows.Forms.Label
    Protected WithEvents BtnFillPurchChallan As System.Windows.Forms.Button
    Protected WithEvents PnlCustomGrid As System.Windows.Forms.Panel
    Protected WithEvents TxtCustomFields As AgControls.AgTextBox
    Protected WithEvents GrpDirectInvoice As System.Windows.Forms.GroupBox
    Protected WithEvents RbtReturnDirect As System.Windows.Forms.RadioButton
    Protected WithEvents LblTotalDeliveryMeasure As System.Windows.Forms.Label
    Protected WithEvents LblTotalDeliveryMeasureText As System.Windows.Forms.Label
    Protected WithEvents RbtnRetunForChallan As System.Windows.Forms.RadioButton
#End Region

    Private Sub FrmPurchChallanReturn_BaseEvent_ApproveDeletion_InTrans(ByVal SearchCode As String, ByVal Conn As SQLiteConnection, ByVal Cmd As SQLiteCommand) Handles Me.BaseEvent_ApproveDeletion_InTrans
        mQry = " Delete From Stock Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        mQry = " Delete From Ledger Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
    End Sub

    Private Sub FrmQuality1_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "PurchChallan"
        MainLineTableCsv = "PurchChallanDetail"
        LogTableName = "PurchChallan_Log"
        LogLineTableCsv = "PurchChallanDetail_Log"

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
                " From PurchChallan H " &
                " Left Join Voucher_Type Vt On H.V_Type = Vt.V_Type  " &
                " Where IfNull(IsDeleted,0)=0  " & mCondStr & "  Order By V_Date Desc "
        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub FrmPurchReturn_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mCondStr$

        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) &
                        " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " And H.Div_Code = '" & AgL.PubDivCode & "'"
        mCondStr = mCondStr & " And Vt.NCat In ('" & EntryNCat & "')"

        If IsApplyVTypePermission Then
            mCondStr = mCondStr & " And H.V_Type In (Select V_Type From User_VType_Permission VP Where VP.UserName = '" & AgL.PubUserName & "' And VP.Div_Code = '" & AgL.PubDivCode & "' And VP.Site_Code = '" & AgL.PubSiteCode & "') "
        End If

        AgL.PubFindQry = " SELECT H.DocID AS SearchCode, Vt.Description AS [Invoice_Type], H.V_Date AS Date, " &
                            " H.ReferenceNo AS [Manual_No], SGV.DispName As Vendor, H.SalesTaxGroupParty AS [Sales_Tax_Group_Party], H.VendorDocNo AS [Vendor_Doc_No],  " &
                            " H.VendorDocDate AS [Vendor_Doc_Date], H.Remarks, L.TotalQty AS [Total_Qty], " &
                            " L.TotalMeasure AS [Total_Measure], L.TotalAmount AS [Total_Amount],  " &
                            " H.EntryBy AS [Entry_By], H.EntryDate AS [Entry_Date], H.EntryType AS [Entry_Type] " &
                            " FROM PurchChallan H " &
                            " LEFT JOIN (" &
                            " SELECT L.DocId, sum(L.Qty) AS TotalQty, sum(L.TotalMeasure) AS TotalMeasure, sum(L.Amount) AS TotalAmount " &
                            " FROM PurchChallanDetail L " &
                            " GROUP BY L.DocId " &
                            " ) L On L.DocId = H.DocId " &
                            " LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type " &
                            " LEFT JOIN SubGroup SGV ON SGV.SubCode  = H.Vendor  " &
                            " Where IfNull(H.IsDeleted,0) = 0  " & mCondStr

        AgL.PubFindQryOrdBy = "[Entry Date]"
    End Sub

    Private Sub FrmPurchReturn_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl1, Col1ItemCode, 60, 0, Col1ItemCode, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_ItemCode")), Boolean), False)
            .AddAgTextColumn(Dgl1, Col1Item, 140, 0, Col1Item, True, Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_ItemName")), Boolean))
            .AddAgTextColumn(Dgl1, Col1PurchChallan, 70, 0, Col1PurchChallan, True, True)
            .AddAgTextColumn(Dgl1, Col1PurchChallanSr, 40, 5, Col1PurchChallanSr, False, True, False)
            .AddAgTextColumn(Dgl1, Col1Item_UID, 60, 0, Col1Item_UID, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_ItemUID")), Boolean), False)
            .AddAgTextColumn(Dgl1, Col1BaleNo, 50, 0, Col1BaleNo, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_BaleNo")), Boolean), True)
            .AddAgTextColumn(Dgl1, Col1LotNo, 50, 0, Col1LotNo, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_LotNo")), Boolean), True)
            .AddAgTextColumn(Dgl1, Col1SalesTaxGroup, 130, 0, Col1SalesTaxGroup, False, False)
            .AddAgNumberColumn(Dgl1, Col1DocQty, 70, 8, 4, False, Col1DocQty, True, True, True)
            .AddAgNumberColumn(Dgl1, Col1FreeQty, 60, 8, 3, False, Col1FreeQty, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_FreeQty")), Boolean), False, True)
            .AddAgNumberColumn(Dgl1, Col1Qty, 70, 8, 4, False, Col1Qty, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Qty")), Boolean), False, True)
            .AddAgTextColumn(Dgl1, Col1Unit, 50, 0, Col1Unit, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Unit")), Boolean), True)
            .AddAgTextColumn(Dgl1, Col1QtyDecimalPlaces, 50, 0, Col1QtyDecimalPlaces, False, True, False)
            .AddAgNumberColumn(Dgl1, Col1MeasurePerPcs, 70, 8, 4, False, Col1MeasurePerPcs, False, True, True)
            .AddAgNumberColumn(Dgl1, Col1PcsPerMeasure, 70, 8, 4, False, Col1PcsPerMeasure, False, True, True)
            .AddAgNumberColumn(Dgl1, Col1TotalDocMeasure, 70, 8, 4, False, Col1TotalDocMeasure, False, True, True)
            .AddAgNumberColumn(Dgl1, Col1TotalFreeMeasure, 70, 8, 4, False, Col1TotalFreeMeasure, False, True, True)
            .AddAgNumberColumn(Dgl1, Col1TotalMeasure, 70, 8, 4, False, Col1TotalMeasure, False, True, True)
            .AddAgTextColumn(Dgl1, Col1MeasureUnit, 60, 50, Col1MeasureUnit, False, True)
            .AddAgTextColumn(Dgl1, Col1MeasureDecimalPlaces, 50, 0, Col1MeasureDecimalPlaces, False, True, False)
            .AddAgTextColumn(Dgl1, Col1DeliveryMeasure, 70, 50, Col1DeliveryMeasure, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Measure")), Boolean), False, False)
            .AddAgNumberColumn(Dgl1, Col1DeliveryMeasureMultiplier, 100, 8, 4, False, Col1DeliveryMeasureMultiplier, False, True, True)
            .AddAgNumberColumn(Dgl1, Col1DeliveryMeasurePerPcs, 110, 8, 4, False, Col1DeliveryMeasurePerPcs, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_MeasurePerPcs")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_MeasurePerPcs")), Boolean), True)
            .AddAgNumberColumn(Dgl1, Col1TotalDocDeliveryMeasure, 70, 8, 3, False, Col1TotalDocDeliveryMeasure, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Measure")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_Measure")), Boolean), True)
            .AddAgNumberColumn(Dgl1, Col1TotalFreeDeliveryMeasure, 70, 8, 3, False, Col1TotalFreeDeliveryMeasure, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_FreeMeasure")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_Measure")), Boolean), True)
            .AddAgNumberColumn(Dgl1, Col1TotalDeliveryMeasure, 70, 8, 4, False, Col1TotalDeliveryMeasure, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Measure")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_Measure")), Boolean), True)
            .AddAgTextColumn(Dgl1, Col1DeliveryMeasureDecimalPlaces, 50, 0, Col1DeliveryMeasureDecimalPlaces, False, True, False)
            .AddAgTextColumn(Dgl1, Col1BillingType, 50, 255, Col1BillingType, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_BillingType")), Boolean), False)
            .AddAgNumberColumn(Dgl1, Col1Rate, 80, 8, 2, False, Col1Rate, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Rate")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_Rate")), Boolean), True)
            .AddAgNumberColumn(Dgl1, Col1Amount, 100, 8, 2, False, Col1Amount, True, True, True)
            .AddAgTextColumn(Dgl1, Col1Remark, 200, 255, Col1Remark, True, False)
            .AddAgTextColumn(Dgl1, Col1Deal, 70, 255, Col1Deal, True, False)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 50

        AgCalcGrid1.Ini_Grid(LblV_Type.Tag, TxtV_Date.Text)

        AgCalcGrid1.AgFixedRows = 6

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

    Private Sub FrmPurchReturn_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As SQLiteConnection, ByVal Cmd As SQLiteCommand) Handles Me.BaseEvent_Save_InTrans
        Dim I As Integer, mSr As Integer
        Dim bSelectionQry$ = ""

        mQry = " Update PurchChallan " &
                " SET " &
                " ReferenceNo = " & AgL.Chk_Text(TxtReferenceNo.Text) & ", " &
                " Vendor = " & AgL.Chk_Text(TxtVendor.AgSelectedValue) & ", " &
                " Currency = " & AgL.Chk_Text(TxtCurrency.AgSelectedValue) & ", " &
                " SalesTaxGroupParty = " & AgL.Chk_Text(TxtSalesTaxGroupParty.Text) & ", " &
                " Structure = " & AgL.Chk_Text(TxtStructure.AgSelectedValue) & ", " &
                " Godown = " & AgL.Chk_Text(TxtGodown.Tag) & ", " &
                " CustomFields = " & AgL.Chk_Text(TxtCustomFields.Tag) & ", " &
                " Remarks = " & AgL.Chk_Text(TxtRemarks.Text) & ", " &
                " " & AgCalcGrid1.FFooterTableUpdateStr() & " " &
                " " & AgCustomGrid1.FFooterTableUpdateStr() & " " &
                " Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mSr = AgL.VNull(AgL.Dman_Execute("Select Max(Sr) From PurchChallanDetail  Where DocID = '" & mSearchCode & "'", AgL.GcnRead).ExecuteScalar)

        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1Item, I).Value <> "" Then
                If Dgl1.Item(ColSNo, I).Tag Is Nothing And Dgl1.Rows(I).Visible = True Then
                    mSr += 1
                    If bSelectionQry <> "" Then bSelectionQry += " UNION ALL "
                    bSelectionQry += " Select " & AgL.Chk_Text(mSearchCode) & ", " & mSr & ", " &
                                        " " & AgL.Chk_Text(Dgl1.Item(Col1PurchChallan, I).Tag) & ", " &
                                        " " & Val(Dgl1.Item(Col1PurchChallanSr, I).Value) & ", " &
                                        " " & AgL.Chk_Text(Dgl1.Item(Col1Item_UID, I).Tag) & ", " &
                                        " " & AgL.Chk_Text(Dgl1.Item(Col1Item, I).Tag) & ", " &
                                        " " & AgL.Chk_Text(Dgl1.Item(Col1BaleNo, I).Value) & ", " &
                                        " " & AgL.Chk_Text(Dgl1.Item(Col1LotNo, I).Value) & ", " &
                                        " " & AgL.Chk_Text(Dgl1.Item(Col1SalesTaxGroup, I).Tag) & ", " &
                                        " " & Val(Dgl1.Item(Col1DocQty, I).Value) & ", " &
                                        " " & Val(Dgl1.Item(Col1FreeQty, I).Value) & ", " &
                                        " " & -Val(Dgl1.Item(Col1Qty, I).Value) & ", " &
                                        " " & AgL.Chk_Text(Dgl1.Item(Col1Unit, I).Value) & ", " &
                                        " " & Val(Dgl1.Item(Col1MeasurePerPcs, I).Value) & ", " &
                                        " " & Val(Dgl1.Item(Col1PcsPerMeasure, I).Value) & ", " &
                                        " " & Val(Dgl1.Item(Col1TotalDocMeasure, I).Value) & ", " &
                                        " " & Val(Dgl1.Item(Col1TotalFreeMeasure, I).Value) & ", " &
                                        " " & -Val(Dgl1.Item(Col1TotalMeasure, I).Value) & ", " &
                                        " " & AgL.Chk_Text(Dgl1.Item(Col1MeasureUnit, I).Value) & ", " &
                                        " " & AgL.Chk_Text(Dgl1.Item(Col1DeliveryMeasure, I).Value) & ", " &
                                        " " & Val(Dgl1.Item(Col1DeliveryMeasurePerPcs, I).Value) & ", " &
                                        " " & Val(Dgl1.Item(Col1TotalDocDeliveryMeasure, I).Value) & ", " &
                                        " " & Val(Dgl1.Item(Col1TotalFreeDeliveryMeasure, I).Value) & ", " &
                                        " " & -Val(Dgl1.Item(Col1TotalDeliveryMeasure, I).Value) & ", " &
                                        " " & Val(Dgl1.Item(Col1Rate, I).Value) & ", " &
                                        " " & -Val(Dgl1.Item(Col1Amount, I).Value) & ", " &
                                        " " & AgL.Chk_Text(Dgl1.Item(Col1Remark, I).Value) & ", " &
                                        " " & AgL.Chk_Text(Dgl1.Item(Col1BillingType, I).Value) & " , " &
                                        " " & AgL.Chk_Text(Dgl1.Item(Col1Deal, I).Value) & ", " &
                                        " " & AgTemplate.ClsMain.T_Nature.Returned & ", " &
                                        " '" & IIf(RbtnRetunForChallan.Checked, RbtnRetunForChallan.Text, RbtReturnDirect.Text) & "', " &
                                        " " & AgCalcGrid1.FLineTableFieldValuesStr(I) & " "
                Else
                    If Dgl1.Rows(I).Visible = True Then
                        If Dgl1.Rows(I).DefaultCellStyle.BackColor <> AgTemplate.ClsMain.Colours.GridRow_Locked Then
                            mQry = "UPDATE PurchChallanDetail SET " &
                                        " PurchChallan = " & AgL.Chk_Text(Dgl1.Item(Col1PurchChallan, I).Tag) & ", " &
                                        " PurchChallanSr = " & Val(Dgl1.Item(Col1PurchChallanSr, I).Value) & ", " &
                                        " Item_Uid = " & AgL.Chk_Text(Dgl1.Item(Col1Item_UID, I).Tag) & ", " &
                                        " Item = " & AgL.Chk_Text(Dgl1.Item(Col1Item, I).Tag) & ", " &
                                        " BaleNo = " & AgL.Chk_Text(Dgl1.Item(Col1BaleNo, I).Value) & ", " &
                                        " LotNo = " & AgL.Chk_Text(Dgl1.Item(Col1LotNo, I).Value) & ", " &
                                        " DocQty = " & Val(Dgl1.Item(Col1DocQty, I).Value) & ", " &
                                        " FreeQty = " & Val(Dgl1.Item(Col1FreeQty, I).Value) & ", " &
                                        " Qty = " & -Val(Dgl1.Item(Col1Qty, I).Value) & ", " &
                                        " Unit = " & AgL.Chk_Text(Dgl1.Item(Col1Unit, I).Value) & ", " &
                                        " MeasurePerPcs = " & Val(Dgl1.Item(Col1MeasurePerPcs, I).Value) & ", " &
                                        " PcsPerMeasure = " & Val(Dgl1.Item(Col1PcsPerMeasure, I).Value) & ", " &
                                        " TotalDocMeasure = " & Val(Dgl1.Item(Col1TotalDocMeasure, I).Value) & ", " &
                                        " TotalFreeMeasure = " & Val(Dgl1.Item(Col1TotalFreeMeasure, I).Value) & ", " &
                                        " TotalMeasure = " & -Val(Dgl1.Item(Col1TotalMeasure, I).Value) & ", " &
                                        " MeasureUnit = " & AgL.Chk_Text(Dgl1.Item(Col1MeasureUnit, I).Value) & ", " &
                                        " DeliveryMeasure = " & AgL.Chk_Text(Dgl1.Item(Col1DeliveryMeasure, I).Value) & ", " &
                                        " DeliveryMeasurePerPcs = " & Val(Dgl1.Item(Col1DeliveryMeasurePerPcs, I).Value) & ", " &
                                        " TotalDocDeliveryMeasure = " & Val(Dgl1.Item(Col1TotalDocDeliveryMeasure, I).Value) & ", " &
                                        " TotalFreeDeliveryMeasure = " & Val(Dgl1.Item(Col1TotalFreeDeliveryMeasure, I).Value) & ", " &
                                        " TotalDeliveryMeasure = " & -Val(Dgl1.Item(Col1TotalDeliveryMeasure, I).Value) & ", " &
                                        " Rate = " & Val(Dgl1.Item(Col1Rate, I).Value) & ", " &
                                        " Amount = " & -Val(Dgl1.Item(Col1Amount, I).Value) & ", " &
                                        " Remark = " & AgL.Chk_Text(Dgl1.Item(Col1Remark, I).Value) & ", " &
                                        " BillingType = " & AgL.Chk_Text(Dgl1.Item(Col1BillingType, I).Value) & " , " &
                                        " T_Nature = " & AgTemplate.ClsMain.T_Nature.Returned & ", " &
                                        " Deal = " & AgL.Chk_Text(Dgl1.Item(Col1Deal, I).Value) & ", " &
                                        " V_Nature = '" & IIf(RbtnRetunForChallan.Checked, RbtnRetunForChallan.Text, RbtReturnDirect.Text) & "', " &
                                        " " & AgCalcGrid1.FLineTableUpdateStr(I) & " " &
                                        " Where DocId = '" & mSearchCode & "' " &
                                        " And Sr = " & Dgl1.Item(ColSNo, I).Tag & " "
                            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                        End If
                    Else
                        mQry = " Delete From PurchChallanDetail Where DocId = '" & mSearchCode & "' And Sr = " & Val(Dgl1.Item(ColSNo, I).Tag) & "  "
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                    End If
                End If
            End If
        Next

        If bSelectionQry <> "" Then
            mQry = "INSERT INTO PurchChallanDetail (DocId, Sr, PurchChallan, PurchChallanSr, " &
                    " Item_Uid, Item, BaleNo, LotNo, SalesTaxGroupItem, DocQty, FreeQty, Qty, " &
                    " Unit, MeasurePerPcs, PcsPerMeasure,  TotalDocMeasure, TotalFreeMeasure, TotalMeasure, MeasureUnit, " &
                    " DeliveryMeasure, DeliveryMeasurePerPcs, TotalDocDeliveryMeasure, TotalFreeDeliveryMeasure, TotalDeliveryMeasure, " &
                    " Rate, Amount, Remark, BillingType, Deal, T_Nature, V_Nature, " & AgCalcGrid1.FLineTableFieldNameStr() & ") " & bSelectionQry
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If

        Call ClsMain.PostStructureLineToAccounts(AgCalcGrid1, TxtRemarks.Text, mSearchCode, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, TxtDivision.AgSelectedValue,
                                             TxtV_Type.AgSelectedValue, LblPrefix.Text, TxtV_No.Text, TxtReferenceNo.Text, TxtVendor.AgSelectedValue, TxtV_Date.Text, Conn, Cmd)

        mQry = "Delete From Stock Where DocId = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " INSERT INTO  Stock(DocID, Sr, V_Type, V_Prefix, V_Date, V_No, Div_Code, Site_Code,   " &
                " SubCode, Currency, SalesTaxGroupParty, Structure, BillingType, Item,  " &
                " Godown, Qty_Iss, Qty_Rec, Unit, LotNo, MeasurePerPcs, Measure_Iss, Measure_Rec, MeasureUnit, " &
                " Rate, Amount, NetAmount, Remarks, RecId, ReferenceDocId, ReferenceDocIdSr, ExpiryDate) " &
                " SELECT L.DocId, L.Sr, H.V_Type, H.V_Prefix, H.V_Date, H.V_No, H.Div_Code, H.Site_Code, " &
                " H.Vendor, H.Currency, H.SalesTaxGroupParty, H.Structure, H.BillingType, L.Item, H.Godown, Abs(L.Qty), 0, " &
                " L.Unit, L.LotNo, L.MeasurePerPcs, Abs(L.TotalMeasure), 0, L.MeasureUnit, L.Rate, L.Amount, L.Amount, " &
                " L.Remark, H.ReferenceNo, L.PurchChallan, L.PurchChallanSr, L.ExpiryDate " &
                " FROM PurchChallanDetail L  " &
                " LEFT JOIN PurchChallan H ON L.DocId = H.DocID " &
                " Where L.DocId = '" & mSearchCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        'ProcPurchChallanPosting(Conn, Cmd)

        If AgL.PubUserName.ToUpper = AgLibrary.ClsConstant.PubSuperUserName.ToUpper Then
            AgCL.GridSetiingWriteXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1)
        End If
    End Sub

    Private Sub ProcPurchChallanPosting(ByVal Conn As SQLiteConnection, ByVal Cmd As SQLiteCommand)
        Dim I As Integer
        Dim bSelectionQry As String = ""

        mQry = "Delete From PurchChallanDetail Where DocId =" & AgL.Chk_Text(mInternalCode) & " "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        mQry = "Delete From PurchChallan Where DocId =" & AgL.Chk_Text(mInternalCode) & " "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        mQry = " INSERT INTO PurchChallan ( DocID,	V_Type,	V_Prefix,	V_Date,	V_No, " &
                " Div_Code,	Site_Code, ReferenceNo, Vendor,	SalesTaxGroupParty,	Structure, " &
                " Remarks ) " &
                " VALUES ( " & AgL.Chk_Text(mInternalCode) & ",	" & AgL.Chk_Text(TxtV_Type.Tag) & ", " & AgL.Chk_Text(LblV_No.Tag) & ", " &
                " " & AgL.Chk_Text(TxtV_Date.Text) & ",	" & Val(TxtV_No.Text) & ", " &
                " " & AgL.Chk_Text(TxtDivision.Tag) & ", " & AgL.Chk_Text(TxtSite_Code.Tag) & "," & AgL.Chk_Text(TxtReferenceNo.Text) & ", " &
                " " & AgL.Chk_Text(TxtVendor.Tag) & ", " & AgL.Chk_Text(TxtSalesTaxGroupParty.Text) & ", " & AgL.Chk_Text(TxtStructure.AgSelectedValue) & ", " &
                " " & AgL.Chk_Text(TxtRemarks.Text) & "	) "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1Item, I).Value <> "" Then
                If bSelectionQry <> "" Then bSelectionQry += " UNION ALL "
                bSelectionQry += " Select " & AgL.Chk_Text(mSearchCode) & "," & Val(Dgl1.Item(ColSNo, I).Value) & " , " & AgL.Chk_Text(TxtV_Date.Text) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1Item, I).Tag) & ",	" & AgL.Chk_Text(Dgl1.Item(Col1SalesTaxGroup, I).Tag) & " , " &
                        " " & Val(Dgl1.Item(Col1DocQty, I).Value) & " , " & Val(Dgl1.Item(Col1FreeQty, I).Value) & ", " & -Val(Dgl1.Item(Col1Qty, I).Value) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1Unit, I).Value) & ",	" & Val(Dgl1.Item(Col1MeasurePerPcs, I).Value) & ",	" & AgL.Chk_Text(Dgl1.Item(Col1MeasureUnit, I).Value) & ", " &
                        " " & Val(Dgl1.Item(Col1TotalDocMeasure, I).Value) & ",	" & Val(Dgl1.Item(Col1TotalFreeMeasure, I).Value) & ",	" & -Val(Dgl1.Item(Col1TotalMeasure, I).Value) & ",	" & Val(Dgl1.Item(Col1Rate, I).Value) & ", " &
                        " " & -Val(Dgl1.Item(Col1Amount, I).Value) & ",	 " & AgL.Chk_Text(Dgl1.Item(Col1LotNo, I).Value) & ",	" & AgL.Chk_Text(Dgl1.Item(Col1Remark, I).Value) & ", " & AgL.Chk_Text(Dgl1.Item(Col1BaleNo, I).Value) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1BillingType, I).Value) & ", " & AgL.Chk_Text(Dgl1.Item(Col1PurchChallan, I).Tag) & ", " &
                        " " & Val(Dgl1.Item(Col1PurchChallanSr, I).Value) & ",	" & AgL.Chk_Text(Dgl1.Item(Col1DeliveryMeasure, I).Value) & ",	" & AgL.Chk_Text(Dgl1.Item(Col1DeliveryMeasureMultiplier, I).Value) & ", " &
                        " " & Val(Dgl1.Item(Col1TotalDocDeliveryMeasure, I).Value) & ",	" & Val(Dgl1.Item(Col1TotalFreeDeliveryMeasure, I).Value) & ",	" & -Val(Dgl1.Item(Col1TotalDeliveryMeasure, I).Value) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1Item_UID, I).Tag) & ",	" & Val(Dgl1.Item(Col1DeliveryMeasurePerPcs, I).Value) & ", " &
                        " " & AgCalcGrid1.FLineTableFieldValuesStr(I) & " "
            End If
        Next

        If bSelectionQry <> "" Then
            mQry = " INSERT INTO PurchChallanDetail (DocId, Sr,	V_Date,	 Item,	SalesTaxGroupItem, Docqty, FreeQty, Qty, " &
                    " Unit, MeasurePerPcs,	MeasureUnit, TotalDocMeasure, TotalFreeMeasure, TotalMeasure,Rate,	Amount, " &
                    " LotNo, Remark, BALENO, BillingType, PurchChallan,	PurchChallanSr, " &
                    " DeliveryMeasure,	DeliveryMeasureMultiplier, TotalDocDeliveryMeasure,	TotalFreeDeliveryMeasure, TotalDeliveryMeasure, " &
                    " Item_UID,	DeliveryMeasurePerPcs,	" & AgCalcGrid1.FLineTableFieldNameStr() & " ) " & bSelectionQry
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If

    End Sub

    Private Sub FrmPurchReturn_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim I As Integer

        Dim intQtyDecimalPlaces As Integer = 0
        Dim intMeasureDecimalPlaces As Integer = 0
        Dim intDeliveryMeasureDecimalPlaces As Integer = 0

        Dim IsSameUnit As Boolean = True
        Dim IsSameMeasureUnit As Boolean = True
        Dim IsSameDeliveryMeasureUnit As Boolean = True

        Dim DsTemp As DataSet

        mQry = "Select H.*, Sg.DispName As VendorDispName, C.Description As CurrencyDesc, G.Description as GodownDesc " &
                " From (Select * From PurchChallan Where DocID='" & SearchCode & "') H " &
                " LEFT JOIN SubGroup Sg ON H.Vendor = Sg.SubCode " &
                " LEFT JOIN Currency C ON H.Currency = C.Code " &
                " Left Join Godown G On H.Godown = G.Code "
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

                TxtReferenceNo.Text = AgL.XNull(.Rows(0)("ReferenceNo"))
                TxtVendor.Tag = AgL.XNull(.Rows(0)("Vendor"))
                TxtVendor.Text = AgL.XNull(.Rows(0)("VendorDispName"))
                TxtCurrency.Tag = AgL.XNull(.Rows(0)("Currency"))
                TxtCurrency.Text = AgL.XNull(.Rows(0)("Currency"))
                TxtGodown.Tag = AgL.XNull(.Rows(0)("Godown"))
                TxtGodown.Text = AgL.XNull(.Rows(0)("GodownDesc"))



                TxtSalesTaxGroupParty.Tag = AgL.XNull(.Rows(0)("SalesTaxGroupParty"))
                TxtSalesTaxGroupParty.Text = AgL.XNull(.Rows(0)("SalesTaxGroupParty"))
                TxtRemarks.Text = AgL.XNull(.Rows(0)("Remarks"))
                LblTotalQty.Text = AgL.VNull(.Rows(0)("TotalQty"))
                LblTotalAmount.Text = AgL.VNull(.Rows(0)("TotalAmount"))
                LblTotalMeasure.Text = AgL.VNull(.Rows(0)("TotalMeasure"))

                AgCalcGrid1.FMoveRecFooterTable(DsTemp.Tables(0), LblV_Type.Tag, TxtV_Date.Text)

                AgCustomGrid1.FMoveRecFooterTable(DsTemp.Tables(0))


                '-------------------------------------------------------------
                'Line Records are showing in Grid
                '-------------------------------------------------------------
                mQry = " Select L.*, I.Description As ItemDesc, I.ManualCode, " &
                        " C.V_Type || '-' || C.ReferenceNo As ChallanRefNo, " &
                        " U.DecimalPlaces as QtyDecimalPlaces, MU.DecimalPlaces as MeasureDecimalPlaces " &
                        " From (Select * From PurchChallanDetail Where DocId = '" & SearchCode & "') As L " &
                        " LEFT JOIN Item I ON L.Item = I.Code " &
                        " LEFT JOIN PurchChallan C On L.DocID = C.DocId " &
                        " LEFT JOIN Unit U On L.Unit = U.Code " &
                        " LEFT JOIN Unit MU ON L.MeasureUnit = MU.Code " &
                        " Order By L.Sr"
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
                            Dgl1.Item(Col1PurchChallanSr, I).Value = AgL.VNull(.Rows(I)("PurchChallanSr"))
                            Dgl1.Item(Col1Item_UID, I).Value = AgL.XNull(.Rows(I)("Item_UID"))
                            Dgl1.Item(Col1ItemCode, I).Tag = AgL.XNull(.Rows(I)("Item"))
                            Dgl1.Item(Col1ItemCode, I).Value = AgL.XNull(.Rows(I)("ManualCode"))
                            Dgl1.Item(Col1Item, I).Tag = AgL.XNull(.Rows(I)("Item"))
                            Dgl1.Item(Col1Item, I).Value = AgL.XNull(.Rows(I)("ItemDesc"))
                            Dgl1.Item(Col1BaleNo, I).Value = AgL.XNull(.Rows(I)("BaleNo"))
                            Dgl1.Item(Col1LotNo, I).Value = AgL.XNull(.Rows(I)("LotNo"))
                            Dgl1.Item(Col1SalesTaxGroup, I).Tag = AgL.XNull(.Rows(I)("SalesTaxGroupItem"))
                            Dgl1.Item(Col1QtyDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("QtyDecimalPlaces"))
                            Dgl1.Item(Col1DocQty, I).Value = Format(Math.Abs(AgL.VNull(.Rows(I)("DocQty"))), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1FreeQty, I).Value = Format(Math.Abs(AgL.VNull(.Rows(I)("FreeQty"))), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1Qty, I).Value = Format(Math.Abs(AgL.VNull(.Rows(I)("Qty"))), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                            Dgl1.Item(Col1MeasureDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("MeasureDecimalPlaces"))
                            Dgl1.Item(Col1MeasurePerPcs, I).Value = Format(AgL.VNull(.Rows(I)("MeasurePerPcs")), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1PcsPerMeasure, I).Value = AgL.VNull(.Rows(I)("PcsPerMeasure"))
                            Dgl1.Item(Col1MeasureUnit, I).Value = AgL.XNull(.Rows(I)("MeasureUnit"))
                            Dgl1.Item(Col1TotalMeasure, I).Value = Format(Math.Abs(AgL.VNull(.Rows(I)("TotalMeasure"))), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1TotalDocMeasure, I).Value = Format(Math.Abs(AgL.VNull(.Rows(I)("TotalDocMeasure"))), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1TotalFreeMeasure, I).Value = Format(Math.Abs(AgL.VNull(.Rows(I)("TotalFreeMeasure"))), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1DeliveryMeasureDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("MeasureDecimalPlaces"))
                            Dgl1.Item(Col1DeliveryMeasurePerPcs, I).Value = Format(AgL.VNull(.Rows(I)("DeliveryMeasurePerPcs")), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1DeliveryMeasure, I).Value = AgL.XNull(.Rows(I)("DeliveryMeasure"))
                            Dgl1.Item(Col1DeliveryMeasurePerPcs, I).Value = Format(AgL.VNull(.Rows(I)("DeliveryMeasurePerPcs")), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1TotalDocDeliveryMeasure, I).Value = Format(AgL.VNull(.Rows(I)("TotalDocDeliveryMeasure")), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1TotalFreeDeliveryMeasure, I).Value = Format(AgL.VNull(.Rows(I)("TotalFreeDeliveryMeasure")), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1TotalDeliveryMeasure, I).Value = Format(Math.Abs(AgL.VNull(.Rows(I)("TotalDeliveryMeasure"))), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1Rate, I).Value = AgL.VNull(.Rows(I)("Rate"))
                            Dgl1.Item(Col1Amount, I).Value = Format(Math.Abs(AgL.VNull(.Rows(I)("Amount"))), "0.00")
                            Dgl1.Item(Col1Remark, I).Value = AgL.XNull(.Rows(I)("Remark"))
                            Dgl1.Item(Col1BillingType, I).Value = AgL.XNull(.Rows(I)("BillingType"))
                            Dgl1.Item(Col1Deal, I).Value = AgL.XNull(.Rows(I)("Deal"))

                            If AgL.XNull(.Rows(I)("V_Nature")) = RbtnRetunForChallan.Text Then
                                RbtnRetunForChallan.Checked = True
                            Else
                                RbtReturnDirect.Checked = True
                            End If

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

                            Call AgCalcGrid1.FMoveRecLineTable(DsTemp.Tables(0), I)
                        Next I
                    End If
                    If Dgl1.Item(Col1Unit, 0).Value <> "" And IsSameUnit Then LblTotalQtyText.Text = "Qty (" & Dgl1.Item(Col1Unit, 0).Value & ") :" Else LblTotalQtyText.Text = "Total Qty :"
                    If Dgl1.Item(Col1MeasureUnit, 0).Value <> "" And IsSameMeasureUnit Then LblTotalMeasureText.Text = "Measure (" & Dgl1.Item(Col1MeasureUnit, 0).Value & ") :" Else LblTotalMeasureText.Text = "Total Measure :"
                    If Dgl1.Item(Col1DeliveryMeasure, 0).Value <> "" And IsSameDeliveryMeasureUnit Then LblTotalDeliveryMeasureText.Text = "Delivery Measure (" & Dgl1.Item(Col1DeliveryMeasure, 0).Value & ") :" Else LblTotalDeliveryMeasureText.Text = "Total Delivery Measure :"
                End With
                If AgCustomGrid1.Rows.Count = 0 Then AgCustomGrid1.Visible = False
                'Calculation()
                '-------------------------------------------------------------
            End If
        End With
    End Sub

    Private Sub FrmPurchReturn_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Topctrl1.ChangeAgGridState(Dgl1, False)
        AgCalcGrid1.FrmType = Me.FrmType
    End Sub

    Private Sub Txt_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtV_Type.Validating, TxtVendor.Validating, TxtSalesTaxGroupParty.Validating, TxtReferenceNo.Validating
        Dim DrTemp As DataRow() = Nothing
        Try
            Select Case sender.NAME
                Case TxtV_Type.Name
                    TxtStructure.AgSelectedValue = AgStructure.ClsMain.FGetStructureFromNCat(LblV_Type.Tag, AgL.GcnRead)
                    AgCalcGrid1.AgStructure = TxtStructure.AgSelectedValue
                    AgCalcGrid1.AgNCat = LblV_Type.Tag

                    TxtCustomFields.Tag = AgCustomFields.ClsMain.FGetCustomFieldFromV_Type(TxtV_Type.AgSelectedValue, AgL.GcnRead)
                    AgCustomGrid1.AgCustom = TxtCustomFields.Tag
                    IniGrid()

                    TxtReferenceNo.Text = AgTemplate.ClsMain.FGetManualRefNo("ReferenceNo", "PurchChallan", TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, AgTemplate.ClsMain.ManualRefType.Max)

                Case TxtVendor.Name
                    If TxtV_Date.Text <> "" And TxtVendor.Text <> "" Then
                        DrTemp = sender.AgHelpDataSet.Tables(0).Select("Code = " & AgL.Chk_Text(sender.AgSelectedValue) & "")
                        TxtCurrency.Tag = AgL.XNull(DrTemp(0)("Currency"))
                        TxtCurrency.Text = AgL.XNull(DrTemp(0)("Currency"))
                    End If
                    BtnFillPurchChallan.Tag = Nothing

                Case TxtSalesTaxGroupParty.Name
                    AgCalcGrid1.AgPostingGroupSalesTaxParty = TxtSalesTaxGroupParty.AgSelectedValue
                    Calculation()

                Case TxtReferenceNo.Name
                    e.Cancel = Not AgTemplate.ClsMain.FCheckDuplicateRefNo("ReferenceNo", "PurchChallan",
                                    TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue,
                                    TxtSite_Code.AgSelectedValue, Topctrl1.Mode,
                                    TxtReferenceNo.Text, mSearchCode)
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FrmPurchReturn_BaseEvent_Topctrl_tbAdd() Handles Me.BaseEvent_Topctrl_tbAdd
        TxtStructure.AgSelectedValue = AgStructure.ClsMain.FGetStructureFromNCat(LblV_Type.Tag, AgL.GcnRead)
        AgCalcGrid1.AgStructure = TxtStructure.AgSelectedValue
        AgCalcGrid1.AgNCat = LblV_Type.Tag

        TxtCustomFields.Tag = AgCustomFields.ClsMain.FGetCustomFieldFromV_Type(TxtV_Type.Tag, AgL.GCn)
        AgCustomGrid1.AgCustom = TxtCustomFields.Tag

        Try
            TxtGodown.Tag = AgL.XNull(DtV_TypeSettings.Rows(0)("DEFAULT_Godown"))
            TxtGodown.Text = AgL.XNull(AgL.Dman_Execute(" Select Description From Godown Where Code = '" & TxtGodown.Tag & "'", AgL.GCn).ExecuteScalar)
        Catch ex As Exception
            MsgBox("Default Godown Is Not Set In Enviro", MsgBoxStyle.Information)
        End Try


        IniGrid()
        TabControl1.SelectedTab = TP1

        TxtSalesTaxGroupParty.AgSelectedValue = AgL.XNull(AgL.PubDtEnviro.Rows(0)("DefaultSalesTaxGroupParty"))
        AgCalcGrid1.AgPostingGroupSalesTaxParty = TxtSalesTaxGroupParty.AgSelectedValue
        TxtReferenceNo.Text = AgTemplate.ClsMain.FGetManualRefNo("ReferenceNo", "PurchChallan", TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, AgTemplate.ClsMain.ManualRefType.Max)
        TxtVendor.Focus()
    End Sub

    Private Sub Dgl1_EditingControl_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Dgl1.EditingControl_Validating
        If Topctrl1.Mode = "Browse" Then Exit Sub
        Dim mRowIndex As Integer, mColumnIndex As Integer
        Dim DrTemp As DataRow() = Nothing
        Dim I As Integer
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

    Private Sub FrmPurchReturn_BaseFunction_Calculation() Handles Me.BaseFunction_Calculation
        Dim I As Integer
        Dim DealArr() As String = Nothing
        Dim DealRate As Double = 0
        Dim mRate As Double = 0

        LblTotalQty.Text = 0
        LblTotalMeasure.Text = 0
        LblTotalDeliveryMeasure.Text = 0
        LblTotalAmount.Text = 0

        Dim IsSameUnit As Boolean = True
        Dim IsSameMeasureUnit As Boolean = True
        Dim IsSameDeliveryMeasureUnit As Boolean = True

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
                    Dgl1.Item(Col1TotalDocMeasure, I).Value = Format(Val(Dgl1.Item(Col1DocQty, I).Value) * Val(Dgl1.Item(Col1MeasurePerPcs, I).Value), "0.".PadRight(CType(Dgl1.Columns(Col1TotalDocMeasure), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                End If

                'If in item master Pcs Per Measure is defined this calculation will be executed.
                'for example in case of soap user will feed how many cartons he purchased in the measure field and
                'qty will be calculated on the basis of the pcs per measure.
                If Val(Dgl1.Item(Col1PcsPerMeasure, I).Value) <> 0 Then
                    Dgl1.Item(Col1Qty, I).Value = Format(Val(Dgl1.Item(Col1TotalMeasure, I).Value) * Val(Dgl1.Item(Col1PcsPerMeasure, I).Value), "0.".PadRight(CType(Dgl1.Columns(Col1Qty), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                End If

                'By default measure unit will automatically come in delivery measure unit and delivery measure
                'multiplier will be set to 1.

                If Dgl1.Item(Col1MeasureUnit, I).Value <> "" And Dgl1.Item(Col1DeliveryMeasure, I).Value <> "" Then
                    If AgL.StrCmp(Dgl1.Item(Col1MeasureUnit, I).Value, Dgl1.Item(Col1DeliveryMeasure, I).Value) Then
                        Dgl1.Item(Col1DeliveryMeasureMultiplier, I).Value = 1
                    End If
                End If

                If Val(Dgl1.Item(Col1DeliveryMeasurePerPcs, I).Value) <> 0 Then
                    Dgl1.Item(Col1TotalDeliveryMeasure, I).Value = Format(Val(Dgl1.Item(Col1Qty, I).Value) * Val(Dgl1.Item(Col1DeliveryMeasurePerPcs, I).Value), "0.".PadRight(CType(Dgl1.Columns(Col1TotalDeliveryMeasure), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                    Dgl1.Item(Col1TotalDocDeliveryMeasure, I).Value = Format(Val(Dgl1.Item(Col1DocQty, I).Value) * Val(Dgl1.Item(Col1DeliveryMeasurePerPcs, I).Value), "0.".PadRight(CType(Dgl1.Columns(Col1TotalDeliveryMeasure), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                End If

                'if the qty unit and mesure units are equal then qty will auto come in mesure fields
                'for example yarn's unit and measure unit is Kg
                'In this case same figure will be copied in the measure.
                If AgL.StrCmp(Dgl1.Item(Col1MeasureUnit, I).Value, Dgl1.Item(Col1Unit, I).Value) Then
                    Dgl1.Item(Col1TotalMeasure, I).Value = Format(Val(Dgl1.Item(Col1Qty, I).Value), "0.".PadRight(CType(Dgl1.Columns(Col1TotalMeasure), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                End If

                If AgL.StrCmp(Dgl1.Item(Col1BillingType, I).Value, "Measure") Then
                    Dgl1.Item(Col1Amount, I).Value = Format(Val(Dgl1.Item(Col1TotalDeliveryMeasure, I).Value) * mRate, "0.".PadRight(CType(Dgl1.Columns(Col1Amount), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                ElseIf AgL.StrCmp(Dgl1.Item(Col1BillingType, I).Value, "Qty") Then
                    Dgl1.Item(Col1Amount, I).Value = Format(Val(Dgl1.Item(Col1Qty, I).Value) * mRate, "0.".PadRight(CType(Dgl1.Columns(Col1Amount), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                Else
                    Dgl1.Item(Col1Amount, I).Value = Format(Val(Dgl1.Item(Col1Qty, I).Value) * mRate, "0.".PadRight(CType(Dgl1.Columns(Col1Amount), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
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
        LblTotalQty.Text = Val(LblTotalQty.Text)
        LblTotalMeasure.Text = Val(LblTotalMeasure.Text)
        LblTotalAmount.Text = Val(LblTotalAmount.Text)

        If Dgl1.Item(Col1Unit, 0).Value <> "" And IsSameUnit Then LblTotalQtyText.Text = "Qty (" & Dgl1.Item(Col1Unit, 0).Value & ") :" Else LblTotalQtyText.Text = "Qty :"
        If Dgl1.Item(Col1MeasureUnit, 0).Value <> "" And IsSameMeasureUnit Then LblTotalMeasureText.Text = "Measure (" & Dgl1.Item(Col1MeasureUnit, 0).Value & ") :" Else LblTotalMeasureText.Text = "Measure :"
        If Dgl1.Item(Col1DeliveryMeasure, 0).Value <> "" And IsSameDeliveryMeasureUnit Then LblTotalDeliveryMeasureText.Text = "Delivery Measure (" & Dgl1.Item(Col1DeliveryMeasure, 0).Value & ") :" Else LblTotalDeliveryMeasureText.Text = "Delivery Measure :"

    End Sub

    Private Sub FrmPurchReturn_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        Dim I As Integer = 0
        If AgL.RequiredField(TxtVendor, LblVendor.Text) Then passed = False : Exit Sub
        If AgL.RequiredField(TxtGodown, "Godown") Then passed = False : Exit Sub
        If AgCL.AgIsBlankGrid(Dgl1, Dgl1.Columns(Col1Item).Index) Then passed = False : Exit Sub

        With Dgl1
            For I = 0 To .Rows.Count - 1
                If .Item(Col1Item, I).Value <> "" Then
                    If Val(.Item(Col1Qty, I).Value) = 0 Then
                        MsgBox("Qty Is 0 At Row No " & Dgl1.Item(ColSNo, I).Value & "")
                        .CurrentCell = .Item(Col1Qty, I) : Dgl1.Focus()
                        passed = False : Exit Sub
                    End If

                    If AgL.VNull(DtV_TypeSettings.Rows(0)("IsMandatory_Rate")) <> 0 Then
                        If Val(.Item(Col1Rate, I).Value) = 0 Then
                            MsgBox("Rate Is 0 At Row No " & Dgl1.Item(ColSNo, I).Value & "")
                            .CurrentCell = .Item(Col1Rate, I) : Dgl1.Focus()
                            passed = False : Exit Sub
                        End If
                    End If
                End If
            Next
        End With

        passed = AgTemplate.ClsMain.FCheckDuplicateRefNo("ReferenceNo", "PurchChallan",
                                    TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue,
                                    TxtSite_Code.AgSelectedValue, Topctrl1.Mode,
                                    TxtReferenceNo.Text, mSearchCode)

    End Sub

    Private Sub FrmPurchReturn_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
        Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
    End Sub

    Private Sub Dgl1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellEnter
        Try
            If AgL.StrCmp(Topctrl1.Mode, "Browse") Then Exit Sub
            If Dgl1.CurrentCell Is Nothing Then Exit Sub
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Qty
                    CType(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex), AgControls.AgTextColumn).AgNumberRightPlaces = Val(Dgl1.Item(Col1QtyDecimalPlaces, Dgl1.CurrentCell.RowIndex).Value)

                Case Col1MeasurePerPcs, Col1TotalMeasure
                    CType(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex), AgControls.AgTextColumn).AgNumberRightPlaces = Val(Dgl1.Item(Col1MeasureDecimalPlaces, Dgl1.CurrentCell.RowIndex).Value)

                Case Col1LotNo, Col1BaleNo
                    'If Dgl1.AgSelectedValue(Col1PurchInvoice, Dgl1.CurrentCell.RowIndex) = "" Then
                    Dgl1.CurrentCell.ReadOnly = False
                    Dgl1.CurrentCell.Style.BackColor = Color.White
                    'End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ProcFillItemsForChallan(ByVal bChallanNoStr As String)
        Dim I As Integer = 0
        Dim DtTemp As DataTable = Nothing
        Dim bCondStr$ = ""

        Try
            If bChallanNoStr = "" Then Exit Sub

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ContraV_Type")) <> "" Then
                bCondStr += " And CharIndex('|' || H.V_Type || '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ContraV_Type")) & "') > 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")) <> "" Then
                bCondStr += " And CharIndex('|' || I.ItemType || '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")) & "') > 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemGroup")) <> "" Then
                bCondStr += " And CharIndex('|' || I.ItemGroup || '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemGroup")) & "') > 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_ItemGroup")) <> "" Then
                bCondStr += " And CharIndex('|' || I.ItemGroup || '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_ItemGroup")) & "') <= 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Item")) <> "" Then
                bCondStr += " And CharIndex('|' || I.Code || '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Item")) & "') > 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_Item")) <> "" Then
                bCondStr += " And CharIndex('|' || I.Code || '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_Item")) & "') <= 0 "
            End If

            mQry = " SELECT L.PurchChallan, L.PurchChallanSr, Max(H.V_Type) || '-' ||  Max(H.ReferenceNo) AS PurchChallanNo, max(H.V_Date) AS Challan_Date, " &
                    " max(L.Item) AS Item, Max(I.Description) as ItemDesc,  Max(I.ManualCode) as ItemManualCode, IfNull(Sum(L.Qty),0) As [Bal.Qty], " &
                    " Max(L.Unit) AS Unit, Max(L.Rate) as Rate, Max(L.MeasureUnit) MeasureUnit, Max(L.DeliveryMeasure) DeliveryMeasure, " &
                    " Max(L.SalesTaxGroupItem) SalesTaxGroupItem, Max(L.MeasurePerPcs) As MeasurePerPcs, Max(L.Item_UID) As Item_UID, " &
                    " Max(U.DecimalPlaces) As QtyDecimalPlaces, Max(U1.DecimalPlaces) As MeasureDecimalPlaces, " &
                    " Max(L.BaleNo) As BaleNo, Max(L.LotNo) as LotNo, Max(L.PcsPerMeasure) AS PcsPerMeasure, IfNull(Max(L.BillingType),'Qty') AS BillingType " &
                    " FROM (     " &
                    " SELECT DocID, V_Type, ReferenceNo, V_Date  " &
                    " FROM PurchChallan   " &
                    " WHERE Vendor = '" & TxtVendor.Tag & "'  " &
                    " And Div_Code = '" & TxtDivision.Tag & "'  " &
                    " AND Site_Code = '" & TxtSite_Code.Tag & "' " &
                    " AND V_Date <= '" & TxtV_Date.Text & "'    " &
                    " ) H  " &
                    " LEFT JOIN PurchChallanDetail L On H.DocId = L.DocId   " &
                    " LEFT JOIN Item I ON I.Code = L.Item " &
                    " LEFT JOIN Unit U On L.Unit = U.Code " &
                    " LEFT JOIN Unit U1 On L.MeasureUnit = U1.Code " &
                    " WHERE L.PurchChallan In (" & bChallanNoStr & ") " & bCondStr &
                    " GROUP BY L.PurchChallan, L.PurchChallanSr " &
                    " Having IfNull(Sum(L.Qty),0) > 0  "
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

            With DtTemp
                Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
                If .Rows.Count > 0 Then
                    For I = 0 To .Rows.Count - 1
                        Dgl1.Rows.Add()
                        Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count - 1
                        Dgl1.Item(Col1ItemCode, I).Tag = AgL.XNull(.Rows(I)("Item"))
                        Dgl1.Item(Col1ItemCode, I).Value = AgL.XNull(.Rows(I)("ItemManualCode"))
                        Dgl1.Item(Col1Item, I).Tag = AgL.XNull(.Rows(I)("Item"))
                        Dgl1.Item(Col1Item, I).Value = AgL.XNull(.Rows(I)("ItemDesc"))
                        Dgl1.Item(Col1PurchChallan, I).Tag = AgL.XNull(.Rows(I)("PurchChallan"))
                        Dgl1.Item(Col1PurchChallan, I).Value = AgL.XNull(.Rows(I)("PurchChallanNo"))
                        Dgl1.Item(Col1PurchChallanSr, I).Value = AgL.XNull(.Rows(I)("PurchChallanSr"))
                        Dgl1.Item(Col1Item_UID, I).Tag = AgL.XNull(.Rows(I)("Item_UID"))
                        mQry = " Select Item_UID From Item_Uid Where Code = '" & AgL.XNull(.Rows(I)("Item_UID")) & "'"
                        Dgl1.Item(Col1Item_UID, I).Value = AgL.XNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar)
                        Dgl1.Item(Col1BaleNo, I).Value = AgL.XNull(.Rows(I)("BaleNo"))
                        Dgl1.Item(Col1LotNo, I).Value = AgL.XNull(.Rows(I)("LotNo"))
                        Dgl1.Item(Col1SalesTaxGroup, I).Tag = AgL.XNull(.Rows(I)("SalesTaxGroupItem"))
                        Dgl1.Item(Col1QtyDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("QtyDecimalPlaces"))
                        Dgl1.Item(Col1DocQty, I).Value = Format(AgL.VNull(.Rows(I)("Bal.Qty")), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
                        Dgl1.Item(Col1Qty, I).Value = Format(AgL.VNull(.Rows(I)("Bal.Qty")), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
                        Dgl1.Item(Col1Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                        Dgl1.Item(Col1MeasureDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("MeasureDecimalPlaces"))
                        Dgl1.Item(Col1MeasurePerPcs, I).Value = Format(AgL.VNull(.Rows(I)("MeasurePerPcs")), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                        Dgl1.Item(Col1PcsPerMeasure, I).Value = AgL.VNull(.Rows(I)("PcsPerMeasure"))
                        Dgl1.Item(Col1MeasureUnit, I).Value = AgL.XNull(.Rows(I)("MeasureUnit"))
                        Dgl1.Item(Col1DeliveryMeasureDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("MeasureDecimalPlaces"))
                        Dgl1.Item(Col1DeliveryMeasurePerPcs, I).Value = Format(AgL.VNull(.Rows(I)("MeasurePerPcs")), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                        Dgl1.Item(Col1DeliveryMeasure, I).Value = AgL.XNull(.Rows(I)("DeliveryMeasure"))
                        Dgl1.Item(Col1Rate, I).Value = Format(AgL.VNull(.Rows(I)("Rate")), "0.00")
                        Dgl1.Item(Col1BillingType, I).Value = AgL.XNull(.Rows(I)("BillingType"))
                    Next I
                End If
            End With
            AgCalcGrid1.AgVoucherCategory = "PURCH"
            AgCalcGrid1.Calculation(True)
            Calculation()
            If Dgl1.Item(Col1PurchChallan, 0).Value <> "" Then Dgl1.Columns(Col1Item).ReadOnly = True
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub DGL1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.KeyDown
        If e.Control And e.KeyCode = Keys.D Then
            sender.CurrentRow.Selected = True
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

        mQry = " SELECT H.SubCode AS Code, H.DispName || ',' || IfNull(C.CityName,'') AS [Party], " &
                " H.Currency, C1.Description As CurrencyDesc, H.Nature, H.SalesTaxPostingGroup " &
                " FROM SubGroup H  " &
                " LEFT JOIN City C ON H.CityCode = C.CityCode  " &
                " LEFT JOIN Currency C1 On H.Currency = C1.Code " &
                " Where IfNull(H.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
        sender.AgHelpDataSet(4, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
    End Sub

    'Private Function FGetRelationalData() As Boolean
    '    Try
    '        Dim bRData As String
    '        '// Check for relational data in Purchase Return
    '        mQry = " DECLARE @Temp NVARCHAR(Max); "
    '        mQry += " SET @Temp=''; "
    '        mQry += " SELECT  @Temp=@Temp +  X.VNo || ', ' FROM (SELECT DISTINCT H.V_Type || '-' || Convert(VARCHAR,H.V_No) AS VNo From PurchInvoiceDetail  L LEFT JOIN PurchInvoice H ON L.DocId = H.DocID WHERE L.PurchInvoice  = '" & TxtDocId.Text & "' And IfNull(H.IsDeleted,0) = 0) AS X  "
    '        mQry += " SELECT @Temp as RelationalData "
    '        bRData = AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar
    '        If bRData.Trim <> "" Then
    '            MsgBox(" Purchase Return " & bRData & " created against Invoice No. " & TxtV_Type.Tag & "-" & TxtV_No.Text & ". Can't Modify Entry")
    '            FGetRelationalData = True
    '            Exit Function
    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.Message & " in FGetRelationalData in TempRequisition")
    '        FGetRelationalData = True
    '    End Try
    'End Function

    Private Sub ME_BaseEvent_Topctrl_tbEdit(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbEdit
        'Passed = Not FGetRelationalData()
    End Sub

    Private Sub ME_BaseEvent_Topctrl_tbDel(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbDel
        ' Passed = Not FGetRelationalData()
    End Sub

    Private Sub FrmCarpetMaterialPlan_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AgL.WinSetting(Me, 654, 990, 0, 0)
        AgCustomGrid1.FrmType = Me.FrmType
    End Sub

    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub

    Private Sub BtnFillPurchChallan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnFillPurchChallan.Click
        Try
            If Topctrl1.Mode = "Browse" Then Exit Sub
            Dim StrTicked As String

            If RbtnRetunForChallan.Checked = True Then
                StrTicked = FHPGD_PendingPurchChallanNo()
            Else
                StrTicked = ""
            End If

            If StrTicked <> "" Then
                If RbtnRetunForChallan.Checked = True Then
                    ProcFillItemsForChallan(StrTicked)
                End If
            Else
                Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
            End If

            Dgl1.Focus()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Function FHPGD_PendingPurchChallanNo() As String
        Dim FRH_Multiple As DMHelpGrid.FrmHelpGrid_Multi
        Dim StrRtn As String = ""

        Dim bCondStr$ = ""

        If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ContraV_Type")) <> "" Then
            bCondStr += " And CharIndex('|' || H.V_Type || '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ContraV_Type")) & "') > 0 "
        End If

        If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")) <> "" Then
            bCondStr += " And CharIndex('|' || I.ItemType || '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")) & "') > 0 "
        End If

        If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemGroup")) <> "" Then
            bCondStr += " And CharIndex('|' || I.ItemGroup || '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemGroup")) & "') > 0 "
        End If

        If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_ItemGroup")) <> "" Then
            bCondStr += " And CharIndex('|' || I.ItemGroup || '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_ItemGroup")) & "') <= 0 "
        End If

        If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Item")) <> "" Then
            bCondStr += " And CharIndex('|' || I.Code || '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Item")) & "') > 0 "
        End If

        If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_Item")) <> "" Then
            bCondStr += " And CharIndex('|' || I.Code || '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_Item")) & "') <= 0 "
        End If

        mQry = "SELECT 'o' As Tick, L.PurchChallan, Max(H.V_Type) || '-' ||  Max(H.ReferenceNo) AS Challan_No, " &
                  " Max(H.V_Date) as Invoice_Date " &
                  " FROM (" &
                  "    SELECT DocID, V_Type, ReferenceNo, V_Date " &
                  "    FROM PurchChallan  " &
                  "    WHERE Vendor ='" & TxtVendor.Tag & "' " &
                  "    And Div_Code = '" & TxtDivision.Tag & "' " &
                  "    AND Site_Code = '" & TxtSite_Code.Tag & "' " &
                  "    AND V_Date <= '" & TxtV_Date.Text & "' " &
                  "    AND DocId <> '" & mSearchCode & "' " &
                  "    ) H " &
                  " LEFT JOIN PurchChallanDetail L  ON H.DocID = L.DocId  " &
                  " LEFT JOIN Item I ON I.Code = L.Item  " & bCondStr &
                  " GROUP BY L.PurchChallan " &
                  " Having IfNull(Sum(L.Qty),0) > 0 " &
                  " Order By Invoice_Date "


        FRH_Multiple = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(AgL.FillData(mQry, AgL.GCn).TABLES(0)), "", 300, 315, , , False)
        FRH_Multiple.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple.FFormatColumn(1, , 0, , False)
        FRH_Multiple.FFormatColumn(2, "Challan No.", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(3, "Challan Date", 100, DataGridViewContentAlignment.MiddleLeft)

        FRH_Multiple.StartPosition = FormStartPosition.CenterScreen
        FRH_Multiple.ShowDialog()

        If FRH_Multiple.BytBtnValue = 0 Then
            StrRtn = FRH_Multiple.FFetchData(1, "'", "'", ",", True)
        End If
        FHPGD_PendingPurchChallanNo = StrRtn

        FRH_Multiple = Nothing
    End Function

    Private Sub Dgl1_EditingControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.EditingControl_KeyDown
        Try
            If AgL.StrCmp(Topctrl1.Mode, "Browse") Then Exit Sub
            If Dgl1.CurrentCell Is Nothing Then Exit Sub

            Dim bCondStr$ = ""

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")) <> "" Then
                bCondStr += " And CharIndex('|' || I.ItemType || '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")) & "') > 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemGroup")) <> "" Then
                bCondStr += " And CharIndex('|' || I.ItemGroup || '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemGroup")) & "') > 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_ItemGroup")) <> "" Then
                bCondStr += " And CharIndex('|' || I.ItemGroup || '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_ItemGroup")) & "') <= 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Item")) <> "" Then
                bCondStr += " And CharIndex('|' || I.Code || '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Item")) & "') > 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_Item")) <> "" Then
                bCondStr += " And CharIndex('|' || I.Code || '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_Item")) & "') <= 0 "
            End If


            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1ItemCode
                    If Dgl1.AgHelpDataSet(Col1ItemCode) Is Nothing Then
                        If RbtnRetunForChallan.Checked Then
                            mQry = " SELECT  max(L.Item) AS Item, Max(I.ManualCode) AS ItemManualCode, Max(I.Description) as ItemDesc,  L.PurchChallan, L.PurchChallanSr, Max(H.V_Type) || '-' ||  Max(H.ReferenceNo) AS PurchChallanNo, max(H.V_Date) AS Challan_Date, " &
                                    " IfNull(Sum(L.Qty),0) As [Bal.Qty], " &
                                    " Max(L.Unit) AS Unit, Max(L.Rate) as Rate, Max(L.MeasureUnit) MeasureUnit, Max(L.DeliveryMeasure) DeliveryMeasure, " &
                                    " Max(L.SalesTaxGroupItem) SalesTaxGroupItem, Max(L.MeasurePerPcs) As MeasurePerPcs, Max(L.Item_UID) As Item_UID, " &
                                    " Max(U.DecimalPlaces) As QtyDecimalPlaces, Max(U1.DecimalPlaces) As MeasureDecimalPlaces, " &
                                    " Max(L.BaleNo) As BaleNo, Max(L.PcsPerMeasure) AS PcsPerMeasure, IfNull(Max(L.BillingType),'Qty') AS BillingType " &
                                    " FROM (     " &
                                    " SELECT DocID, V_Type, ReferenceNo, V_Date  " &
                                    " FROM PurchChallan   " &
                                    " WHERE Vendor = '" & TxtVendor.Tag & "'  " &
                                    " And Div_Code = '" & TxtDivision.Tag & "'  " &
                                    " AND Site_Code = '" & TxtSite_Code.Tag & "' " &
                                    " AND V_Date <= '" & TxtV_Date.Text & "'    " &
                                    " ) H  " &
                                    " LEFT JOIN PurchChallanDetail L On H.DocId = L.DocId   " &
                                    " LEFT JOIN Item I ON I.Code = L.Item " &
                                    " LEFT JOIN Unit U On L.Unit = U.Code " &
                                    " LEFT JOIN Unit U1 On L.MeasureUnit = U1.Code " &
                                    " Where 1 =1 " & bCondStr &
                                    " GROUP BY L.PurchChallan, L.PurchChallanSr " &
                                    " Having IfNull(Sum(L.Qty),0) > 0  "
                        Else
                            mQry = "SELECT I.Code As Item, I.ManualCode As ItemManualCode, I.Description As ItemDesc, " &
                                      " '' As Invoice_No, '' As Invoice_Date, '' As PurchChallanNo, '' AS Item_UID, '' AS BaleNo, " &
                                      " 0 As [Bal.Qty], I.Unit,0 As Rate, I.SalesTaxPostingGroup , " &
                                      " '' As PurchChallan, 0 As PurchChallanSr, " &
                                      " I.Measure As MeasurePerPcs, I.MeasureUnit, " &
                                      " U.DecimalPlaces as QtyDecimalPlaces, U1.DecimalPlaces as MeasureDecimalPlaces, " &
                                      " 0 As Qty, I.SalesTaxPostingGroup As SalesTaxGroupItem " &
                                      " FROM Item I " &
                                      " LEFT JOIN Unit U On I.Unit = U.Code " &
                                      " LEFT JOIN Unit U1 On I.MeasureUnit = U1.Code " &
                                      " Where IfNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & bCondStr
                            Dgl1.AgHelpDataSet(Col1ItemCode, 18) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If


                Case Col1Item
                    If Dgl1.AgHelpDataSet(Col1Item) Is Nothing Then
                        If RbtnRetunForChallan.Checked Then
                            mQry = " SELECT  max(L.Item) AS Item, Max(I.Description) as ItemDesc, Max(I.ManualCode) AS ItemManualCode, L.PurchChallan, L.PurchChallanSr, Max(H.V_Type) || '-' ||  Max(H.ReferenceNo) AS PurchChallanNo, max(H.V_Date) AS Challan_Date, " &
                                    " IfNull(Sum(L.Qty),0) As [Bal.Qty], " &
                                    " Max(L.Unit) AS Unit, Max(L.Rate) as Rate, Max(L.MeasureUnit) MeasureUnit, Max(L.DeliveryMeasure) DeliveryMeasure, " &
                                    " Max(L.SalesTaxGroupItem) SalesTaxGroupItem, Max(L.MeasurePerPcs) As MeasurePerPcs, Max(L.Item_UID) As Item_UID, " &
                                    " Max(U.DecimalPlaces) As QtyDecimalPlaces, Max(U1.DecimalPlaces) As MeasureDecimalPlaces, " &
                                    " Max(L.BaleNo) As BaleNo, Max(L.PcsPerMeasure) AS PcsPerMeasure, IfNull(Max(L.BillingType),'Qty') AS BillingType " &
                                    " FROM (     " &
                                    "   SELECT DocID, V_Type, ReferenceNo, V_Date  " &
                                    "   FROM PurchChallan   " &
                                    "   WHERE Vendor = '" & TxtVendor.Tag & "'  " &
                                    "   And Div_Code = '" & TxtDivision.Tag & "'  " &
                                    "   AND Site_Code = '" & TxtSite_Code.Tag & "' " &
                                    "   AND V_Date <= '" & TxtV_Date.Text & "'    " &
                                    " ) H  " &
                                    " LEFT JOIN PurchChallanDetail L On H.DocId = L.DocId   " &
                                    " LEFT JOIN Item I ON I.Code = L.Item " &
                                    " LEFT JOIN Unit U On L.Unit = U.Code " &
                                    " LEFT JOIN Unit U1 On L.MeasureUnit = U1.Code " &
                                    " Where 1 =1 " & bCondStr &
                                    " GROUP BY L.PurchChallan, L.PurchChallanSr " &
                                    " Having IfNull(Sum(L.Qty),0) > 0  "
                            Dgl1.AgHelpDataSet(Col1Item, 15) = AgL.FillData(mQry, AgL.GCn)
                        Else
                            mQry = "SELECT I.Code As Item, I.Description As ItemDesc, I.ManualCode As ItemManualCode, " &
                                       " '' As Invoice_No, '' As Invoice_Date, '' As PurchChallanNo, '' AS Item_UID, '' AS BaleNo, " &
                                       " 0 As [Bal.Qty], I.Unit,0 As Rate, I.SalesTaxPostingGroup , " &
                                       " '' As PurchChallan, 0 As PurchChallanSr, " &
                                       " I.Measure As MeasurePerPcs, I.MeasureUnit, " &
                                       " U.DecimalPlaces as QtyDecimalPlaces, U1.DecimalPlaces as MeasureDecimalPlaces, " &
                                       " 0 As Qty, I.SalesTaxPostingGroup As SalesTaxGroupItem " &
                                       " FROM Item I " &
                                       " LEFT JOIN Unit U On I.Unit = U.Code " &
                                       " LEFT JOIN Unit U1 On I.MeasureUnit = U1.Code " &
                                       " Where IfNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & bCondStr
                            Dgl1.AgHelpDataSet(Col1Item, 18) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If

                Case Col1BillingType
                    If Dgl1.AgHelpDataSet(Col1BillingType) Is Nothing Then
                        mQry = " SELECT 'Qty' AS Code, 'Qty' AS Name " &
                            " Union ALL " &
                            " SELECT 'Measure' AS Code, 'Measure' AS Name "
                        Dgl1.AgHelpDataSet(Col1BillingType) = AgL.FillData(mQry, AgL.GCn)
                    End If

                Case Col1DeliveryMeasure
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1DeliveryMeasure) Is Nothing Then
                            mQry = " SELECT Code, Code AS Name FROM Unit Where IfNull(IsActive,1) <> 0  "
                            Dgl1.AgHelpDataSet(Col1DeliveryMeasure) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If

            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FGetDeliveryMeasureMultiplier(ByVal mRow As Integer)
        Dim DtTemp As DataTable = Nothing
        Dim I As Integer = 0
        blnIsCarpetTrans = True
        Try
            If blnIsCarpetTrans Then
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

    Private Sub Validating_Item_Uid(ByVal Item_Uid As String, ByVal mRow As Integer)
        Dim DrTemp As DataRow() = Nothing
        Dim DtTemp As DataTable = Nothing

        Try
            mQry = " SELECT I.Code, I.Description, I.Unit, I.ManualCode, I.MeasureUnit, I.Measure As MeasurePerPcs, " &
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
                    Dgl1.Item(Col1Item, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("Item").Value)
                    Dgl1.Item(Col1Item, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("ItemDesc").Value)
                    Dgl1.Item(Col1ItemCode, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("Item").Value)
                    Dgl1.Item(Col1ItemCode, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("ItemManualCode").Value)
                    Dgl1.Item(Col1PurchChallan, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("PurchChallan").Value)
                    Dgl1.Item(Col1PurchChallan, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("PurchChallanNo").Value)
                    Dgl1.Item(Col1PurchChallanSr, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("PurchChallanSr").Value)
                    Dgl1.Item(Col1Item_UID, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("Item_UID").Value)
                    Dgl1.Item(Col1BaleNo, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("BaleNo").Value)
                    Dgl1.Item(Col1SalesTaxGroup, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("SalesTaxGroupItem").Value)
                    Dgl1.Item(Col1Qty, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("Bal.Qty").Value)
                    Dgl1.Item(Col1DocQty, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("Bal.Qty").Value)
                    Dgl1.Item(Col1Unit, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("Unit").Value)
                    Dgl1.Item(Col1QtyDecimalPlaces, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("QtyDecimalPlaces").Value)
                    Dgl1.Item(Col1MeasurePerPcs, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("MeasurePerPcs").Value)
                    Dgl1.Item(Col1MeasureUnit, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("MeasureUnit").Value)
                    Dgl1.Item(Col1MeasureDecimalPlaces, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("MeasureDecimalPlaces").Value)
                    Dgl1.Item(Col1DeliveryMeasurePerPcs, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("MeasurePerPcs").Value)
                    Dgl1.Item(Col1DeliveryMeasure, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("MeasureUnit").Value)
                    Dgl1.Item(Col1DeliveryMeasureDecimalPlaces, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("MeasureDecimalPlaces").Value)
                    Dgl1.Item(Col1Rate, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("Rate").Value)
                    Dgl1.Item(Col1BillingType, mRow).Value = "Qty"

                    If AgL.StrCmp(Dgl1.AgSelectedValue(Col1SalesTaxGroup, mRow), "") Then
                        Dgl1.Item(Col1SalesTaxGroup, mRow).Tag = AgL.XNull(AgL.PubDtEnviro.Rows(0)("DefaultSalesTaxGroupItem"))
                    End If
                    If Dgl1.Item(Col1MeasureUnit, mRow).Value = "" Then Dgl1.Item(Col1TotalMeasure, mRow).ReadOnly = True
                End If
                Try
                    Dgl1.Item(Col1BillingType, mRow).Value = Dgl1.Item(Col1BillingType, mRow - 1).Value
                Catch ex As Exception
                End Try
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " On Validating_Item Function ")
        End Try
    End Sub

    Private Sub TxtCurrency_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtCurrency.KeyDown, TxtVendor.KeyDown, TxtSalesTaxGroupParty.KeyDown, TxtGodown.KeyDown
        Try
            If AgL.StrCmp(Topctrl1.Mode, "Browse") Then Exit Sub
            Select Case sender.name
                Case TxtGodown.Name
                    If TxtGodown.AgHelpDataSet Is Nothing Then
                        mQry = "SELECT H.Code, H.Description " &
                                " FROM Godown H " &
                                " Where H.Site_Code = '" & TxtSite_Code.Tag & "' " &
                                " And IfNull(H.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " &
                                " Order By H.Description"
                        TxtGodown.AgHelpDataSet(0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
                    End If


                Case TxtCurrency.Name
                    If TxtCurrency.AgHelpDataSet Is Nothing Then
                        mQry = "SELECT Code, Code AS Currency, IfNull(IsDeleted,0) AS IsDeleted " &
                                " FROM Currency " &
                                " ORDER BY Code "
                        TxtCurrency.AgHelpDataSet(1, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
                    End If

                Case TxtVendor.Name
                    If e.KeyCode <> Keys.Enter Then
                        If sender.AgHelpDataSet Is Nothing Then
                            FCreateHelpSubgroup(sender)
                        End If
                    End If

                Case TxtSalesTaxGroupParty.Name
                    If TxtSalesTaxGroupParty.AgHelpDataSet Is Nothing Then
                        mQry = "SELECT Description AS Code, Description, IfNull(Active,0)  FROM PostingGroupSalesTaxParty "
                        TxtSalesTaxGroupParty.AgHelpDataSet(1, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
                    End If

            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub RbtReturnDirect_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RbtReturnDirect.Click
        If Dgl1.AgHelpDataSet(Col1Item) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1Item).Dispose() : Dgl1.AgHelpDataSet(Col1Item) = Nothing
    End Sub

    Private Sub FrmPurchChallanReturn_BaseEvent_Topctrl_tbRef() Handles Me.BaseEvent_Topctrl_tbRef
        If Dgl1.AgHelpDataSet(Col1Item) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1Item).Dispose() : Dgl1.AgHelpDataSet(Col1Item) = Nothing
        If Dgl1.AgHelpDataSet(Col1BillingType) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1BillingType).Dispose() : Dgl1.AgHelpDataSet(Col1BillingType) = Nothing
        If Dgl1.AgHelpDataSet(Col1ItemCode) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1ItemCode).Dispose() : Dgl1.AgHelpDataSet(Col1ItemCode) = Nothing
        If TxtCurrency.AgHelpDataSet IsNot Nothing Then TxtCurrency.AgHelpDataSet.Dispose() : TxtCurrency.AgHelpDataSet = Nothing
        If TxtVendor.AgHelpDataSet IsNot Nothing Then TxtVendor.AgHelpDataSet.Dispose() : TxtVendor.AgHelpDataSet = Nothing
        If TxtSalesTaxGroupParty.AgHelpDataSet IsNot Nothing Then TxtSalesTaxGroupParty.AgHelpDataSet.Dispose() : TxtSalesTaxGroupParty.AgHelpDataSet = Nothing
        If TxtGodown.AgHelpDataSet IsNot Nothing Then TxtGodown.AgHelpDataSet = Nothing
    End Sub

    Private Sub RbtnRetunForChallan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RbtnRetunForChallan.Click, RbtReturnDirect.Click
        If Dgl1.AgHelpDataSet(Col1Item) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1Item) = Nothing
    End Sub

    Private Sub FrmPurchChallanReturn_BaseEvent_Topctrl_tbPrn(ByVal SearchCode As String) Handles Me.BaseEvent_Topctrl_tbPrn
        mQry = " SELECT H.DocID, H.V_Type, H.V_Date, H.ReferenceNo, " &
    " H.Currency, H.SalesTaxGroupParty, H.BillingType, H.VendorDocNo, H.VendorDocDate,  " &
    " H.Form, H.FormNo, H.Remarks, G.Description as Godown_Name, H.EntryBy, H.EntryDate, H.ApproveBy, H.ApproveDate, " &
    " L.DocId, L.Sr, L.Item, L.Specification, L.SalesTaxGroupItem, L.DocQty, L.RejQty, L.Qty, L.Unit,  U.Decimalplaces , UM.Decimalplaces AS MeasureDecimalPlaces, " &
    " L.MeasurePerPcs, L.MeasureUnit, L.TotalDocMeasure, L.TotalRejMeasure, L.TotalMeasure, L.Rate, L.Amount, L.Remark, L.LotNo, L.BaleNo, " &
    " SG.DispName AS VendorName, Sg.Add1, Sg.Add2, Sg.Add3, Sg.Mobile As VendorMobile, " &
    " L.TotalDocDeliveryMeasure, L.TotalRejDeliveryMeasure, L.TotalDeliveryMeasure, " &
    " D1.Description AS D1Desc, D2.Description AS D2Desc, E.Caption_Dimension1, E.Caption_Dimension2, " &
    " City.CityName As VendorCityName, I.Description AS ItemDesc, C.V_Type || '-' || C.ReferenceNo as PurchChallanNo, PO.V_Type +'-'+ PO.ReferenceNo as PurchOrderNo,  " &
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
        ClsMain.FPrintThisDocument(Me, TxtV_Type.Tag, mQry, "PurchChallanReturn_Print|PurchChallanReturnQtyMeasure_Print", "Purchase Challan Return", "For Qty|For Qty & Measure")
    End Sub
End Class
