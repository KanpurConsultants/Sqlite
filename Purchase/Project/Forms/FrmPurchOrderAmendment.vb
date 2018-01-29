Imports System.IO
Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data.SQLite
Public Class FrmPurchOrderAmendment
    Inherits AgTemplate.TempTransaction
    Public mQry$

    Public WithEvents AgCalcGrid1 As New AgStructure.AgCalcGrid
    Public WithEvents AgCustomGrid1 As New AgCustomFields.AgCustomGrid

    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Protected Const ColSNo As String = "S.No."
    Protected Const Col1ItemCode As String = "Item Code"
    Protected Const Col1Item As String = "Item"
    Protected Const Col1PurchOrder As String = "Purch Order"
    Protected Const Col1PurchOrderSr As String = "Purch Order Sr"
    Protected Const Col1BillingType As String = "Billing Type"
    Protected Const Col1RateType As String = "Rate Type"
    Protected Const Col1Qty As String = "Qty"
    Protected Const Col1FreeQty As String = "Free Qty"
    Protected Const Col1DocQty As String = "Doc Qty"
    Protected Const Col1Unit As String = "Unit"
    Protected Const Col1SalesTaxGroup As String = "Sales Tax Group"
    Protected Const Col1QtyDecimalPlaces As String = "Qty Decimal Places"
    Protected Const Col1MeasurePerPcs As String = "Measure Per Qty"
    Protected Const Col1TotalDocMeasure As String = "Total Doc Measure"
    Protected Const Col1TotalMeasure As String = "Total Measure"
    Protected Const Col1TotalFreeMeasure As String = "Total Free Measure"
    Protected Const Col1MeasureUnit As String = "Measure Unit"
    Protected Const Col1MeasureDecimalPlaces As String = "Measure Decimal Places"
    Protected Const Col1DeliveryMeasure As String = "Delivery Measure"
    Protected Const Col1DeliveryMeasureMultiplier As String = "Delivery Measure Multiplier"
    Protected Const Col1DeliveryMeasurePerPcs As String = "Delivery Measure Per Qty"
    Protected Const Col1TotalDeliveryMeasure As String = "Total Delivery Measure"
    Protected Const Col1TotalFreeDeliveryMeasure As String = "Total Free Delivery Measure"
    Protected Const Col1DeliveryMeasureDecimalPlaces As String = "Delivery Measure Decimal Places"
    Protected Const Col1MRP As String = "MRP"
    Protected Const Col1Deal As String = "Deal"
    Protected Const Col1Rate As String = "Rate"
    Protected Const Col1Amount As String = "Amount"
    Protected Const Col1Specification As String = "Specification"

    Protected WithEvents TxtStructure As AgControls.AgTextBox
    Protected WithEvents TxtCustomFields As AgControls.AgTextBox
    Protected WithEvents PnlCustomGrid As System.Windows.Forms.Panel
    Protected WithEvents PnlCalcGrid As System.Windows.Forms.Panel
    Protected WithEvents TxtSalesTaxGroupParty As AgControls.AgTextBox
    Protected WithEvents RbtAddNewItem As System.Windows.Forms.RadioButton

    Dim FillForBalanceQty As Boolean = True

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
        Me.TxtManualRefNo = New AgControls.AgTextBox
        Me.LblManualRefNo = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.LblTotalAmount = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.LblTotalQty = New System.Windows.Forms.Label
        Me.LblTotalQtyText = New System.Windows.Forms.Label
        Me.Pnl1 = New System.Windows.Forms.Panel
        Me.Label30 = New System.Windows.Forms.Label
        Me.TxtRemarks = New AgControls.AgTextBox
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel
        Me.LblJobWorkerReq = New System.Windows.Forms.Label
        Me.TxtVendor = New AgControls.AgTextBox
        Me.LblJobWorker = New System.Windows.Forms.Label
        Me.TxtTermsAndConditions = New AgControls.AgTextBox
        Me.LinkLabel2 = New System.Windows.Forms.LinkLabel
        Me.TxtBillingType = New AgControls.AgTextBox
        Me.Label32 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.TxtV_Nature = New AgControls.AgTextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.GrpDirectChallan = New System.Windows.Forms.GroupBox
        Me.RbtAddNewItem = New System.Windows.Forms.RadioButton
        Me.RbtPlanForPurchOrder = New System.Windows.Forms.RadioButton
        Me.RbtForPurchOrderItems = New System.Windows.Forms.RadioButton
        Me.BtnFillPurchOrder = New System.Windows.Forms.Button
        Me.TxtStructure = New AgControls.AgTextBox
        Me.TxtCustomFields = New AgControls.AgTextBox
        Me.PnlCustomGrid = New System.Windows.Forms.Panel
        Me.PnlCalcGrid = New System.Windows.Forms.Panel
        Me.TxtSalesTaxGroupParty = New AgControls.AgTextBox
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
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.Location = New System.Drawing.Point(829, 585)
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
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(648, 585)
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
        Me.GBoxApprove.Location = New System.Drawing.Point(467, 585)
        Me.GBoxApprove.Text = "Approved By"
        '
        'TxtApproveBy
        '
        Me.TxtApproveBy.Size = New System.Drawing.Size(116, 18)
        Me.TxtApproveBy.Tag = ""
        '
        'GBoxEntryType
        '
        Me.GBoxEntryType.Location = New System.Drawing.Point(168, 585)
        Me.GBoxEntryType.Size = New System.Drawing.Size(119, 40)
        '
        'TxtEntryType
        '
        Me.TxtEntryType.Location = New System.Drawing.Point(3, 19)
        Me.TxtEntryType.Tag = ""
        '
        'GrpUP
        '
        Me.GrpUP.Location = New System.Drawing.Point(16, 585)
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
        Me.GroupBox1.Location = New System.Drawing.Point(2, 581)
        Me.GroupBox1.Size = New System.Drawing.Size(1002, 4)
        '
        'GBoxDivision
        '
        Me.GBoxDivision.Location = New System.Drawing.Point(320, 585)
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
        Me.LblV_No.Location = New System.Drawing.Point(229, 219)
        Me.LblV_No.Size = New System.Drawing.Size(88, 16)
        Me.LblV_No.Tag = ""
        Me.LblV_No.Text = "Job Order No."
        Me.LblV_No.Visible = False
        '
        'TxtV_No
        '
        Me.TxtV_No.AgSelectedValue = ""
        Me.TxtV_No.BackColor = System.Drawing.Color.White
        Me.TxtV_No.Location = New System.Drawing.Point(351, 218)
        Me.TxtV_No.Size = New System.Drawing.Size(149, 18)
        Me.TxtV_No.TabIndex = 3
        Me.TxtV_No.Tag = ""
        Me.TxtV_No.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.TxtV_No.Visible = False
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(106, 38)
        Me.Label2.Tag = ""
        '
        'LblV_Date
        '
        Me.LblV_Date.BackColor = System.Drawing.Color.Transparent
        Me.LblV_Date.Location = New System.Drawing.Point(14, 33)
        Me.LblV_Date.Size = New System.Drawing.Size(84, 16)
        Me.LblV_Date.Tag = ""
        Me.LblV_Date.Text = "Amend. Date"
        '
        'LblV_TypeReq
        '
        Me.LblV_TypeReq.Location = New System.Drawing.Point(310, 14)
        Me.LblV_TypeReq.Tag = ""
        '
        'TxtV_Date
        '
        Me.TxtV_Date.AgSelectedValue = ""
        Me.TxtV_Date.BackColor = System.Drawing.Color.White
        Me.TxtV_Date.Location = New System.Drawing.Point(125, 32)
        Me.TxtV_Date.TabIndex = 2
        Me.TxtV_Date.Tag = ""
        '
        'LblV_Type
        '
        Me.LblV_Type.Location = New System.Drawing.Point(231, 12)
        Me.LblV_Type.Size = New System.Drawing.Size(84, 16)
        Me.LblV_Type.Tag = ""
        Me.LblV_Type.Text = "Amend. Type"
        '
        'TxtV_Type
        '
        Me.TxtV_Type.AgSelectedValue = ""
        Me.TxtV_Type.BackColor = System.Drawing.Color.White
        Me.TxtV_Type.Location = New System.Drawing.Point(326, 12)
        Me.TxtV_Type.Size = New System.Drawing.Size(153, 18)
        Me.TxtV_Type.TabIndex = 1
        Me.TxtV_Type.Tag = ""
        '
        'LblSite_CodeReq
        '
        Me.LblSite_CodeReq.Location = New System.Drawing.Point(106, 14)
        Me.LblSite_CodeReq.Tag = ""
        '
        'LblSite_Code
        '
        Me.LblSite_Code.BackColor = System.Drawing.Color.Transparent
        Me.LblSite_Code.Location = New System.Drawing.Point(14, 12)
        Me.LblSite_Code.Size = New System.Drawing.Size(87, 16)
        Me.LblSite_Code.Tag = ""
        Me.LblSite_Code.Text = "Branch Name"
        '
        'TxtSite_Code
        '
        Me.TxtSite_Code.AgMandatory = True
        Me.TxtSite_Code.AgSelectedValue = ""
        Me.TxtSite_Code.BackColor = System.Drawing.Color.White
        Me.TxtSite_Code.Location = New System.Drawing.Point(125, 12)
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
        Me.LblPrefix.Location = New System.Drawing.Point(289, 219)
        Me.LblPrefix.Tag = ""
        Me.LblPrefix.Visible = False
        '
        'TabControl1
        '
        Me.TabControl1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl1.Location = New System.Drawing.Point(-4, 19)
        Me.TabControl1.Size = New System.Drawing.Size(991, 124)
        Me.TabControl1.TabIndex = 0
        '
        'TP1
        '
        Me.TP1.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.TP1.Controls.Add(Me.TxtSalesTaxGroupParty)
        Me.TP1.Controls.Add(Me.TxtCustomFields)
        Me.TP1.Controls.Add(Me.TxtStructure)
        Me.TP1.Controls.Add(Me.Label9)
        Me.TP1.Controls.Add(Me.TxtV_Nature)
        Me.TP1.Controls.Add(Me.Label6)
        Me.TP1.Controls.Add(Me.Label3)
        Me.TP1.Controls.Add(Me.TxtManualRefNo)
        Me.TP1.Controls.Add(Me.Label32)
        Me.TP1.Controls.Add(Me.TxtBillingType)
        Me.TP1.Controls.Add(Me.LblManualRefNo)
        Me.TP1.Controls.Add(Me.TxtRemarks)
        Me.TP1.Controls.Add(Me.Label30)
        Me.TP1.Controls.Add(Me.TxtVendor)
        Me.TP1.Controls.Add(Me.LblJobWorker)
        Me.TP1.Controls.Add(Me.LblJobWorkerReq)
        Me.TP1.Location = New System.Drawing.Point(4, 22)
        Me.TP1.Size = New System.Drawing.Size(983, 98)
        Me.TP1.Text = "Document Detail"
        Me.TP1.Controls.SetChildIndex(Me.LblJobWorkerReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblJobWorker, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtVendor, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label30, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtRemarks, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblManualRefNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtBillingType, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label32, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtManualRefNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtV_No, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtSite_Code, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_Date, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label2, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_No, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblSite_Code, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtV_Date, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblSite_CodeReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblPrefix, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_Type, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_TypeReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtV_Type, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblDocId, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtDocId, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label3, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label6, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtV_Nature, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label9, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtStructure, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtCustomFields, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtSalesTaxGroupParty, 0)
        '
        'Topctrl1
        '
        Me.Topctrl1.Size = New System.Drawing.Size(984, 41)
        Me.Topctrl1.TabIndex = 4
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
        Me.TxtManualRefNo.Location = New System.Drawing.Point(326, 32)
        Me.TxtManualRefNo.MaxLength = 50
        Me.TxtManualRefNo.Name = "TxtManualRefNo"
        Me.TxtManualRefNo.Size = New System.Drawing.Size(153, 18)
        Me.TxtManualRefNo.TabIndex = 3
        '
        'LblManualRefNo
        '
        Me.LblManualRefNo.AutoSize = True
        Me.LblManualRefNo.BackColor = System.Drawing.Color.Transparent
        Me.LblManualRefNo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblManualRefNo.Location = New System.Drawing.Point(231, 32)
        Me.LblManualRefNo.Name = "LblManualRefNo"
        Me.LblManualRefNo.Size = New System.Drawing.Size(73, 16)
        Me.LblManualRefNo.TabIndex = 706
        Me.LblManualRefNo.Text = "Amend. No"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Cornsilk
        Me.Panel1.Controls.Add(Me.LblTotalAmount)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.LblTotalQty)
        Me.Panel1.Controls.Add(Me.LblTotalQtyText)
        Me.Panel1.Location = New System.Drawing.Point(4, 438)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(972, 21)
        Me.Panel1.TabIndex = 694
        '
        'LblTotalAmount
        '
        Me.LblTotalAmount.AutoSize = True
        Me.LblTotalAmount.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalAmount.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalAmount.Location = New System.Drawing.Point(844, 2)
        Me.LblTotalAmount.Name = "LblTotalAmount"
        Me.LblTotalAmount.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalAmount.TabIndex = 672
        Me.LblTotalAmount.Text = "."
        Me.LblTotalAmount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Maroon
        Me.Label1.Location = New System.Drawing.Point(735, 2)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(100, 16)
        Me.Label1.TabIndex = 671
        Me.Label1.Text = "Total Amount :"
        '
        'LblTotalQty
        '
        Me.LblTotalQty.AutoSize = True
        Me.LblTotalQty.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalQty.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalQty.Location = New System.Drawing.Point(587, 2)
        Me.LblTotalQty.Name = "LblTotalQty"
        Me.LblTotalQty.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalQty.TabIndex = 668
        Me.LblTotalQty.Text = "."
        Me.LblTotalQty.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTotalQtyText
        '
        Me.LblTotalQtyText.AutoSize = True
        Me.LblTotalQtyText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalQtyText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalQtyText.Location = New System.Drawing.Point(502, 2)
        Me.LblTotalQtyText.Name = "LblTotalQtyText"
        Me.LblTotalQtyText.Size = New System.Drawing.Size(72, 16)
        Me.LblTotalQtyText.TabIndex = 667
        Me.LblTotalQtyText.Text = "Total Qty :"
        '
        'Pnl1
        '
        Me.Pnl1.Location = New System.Drawing.Point(4, 170)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(972, 268)
        Me.Pnl1.TabIndex = 1
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(502, 12)
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
        Me.TxtRemarks.Location = New System.Drawing.Point(590, 12)
        Me.TxtRemarks.MaxLength = 255
        Me.TxtRemarks.Multiline = True
        Me.TxtRemarks.Name = "TxtRemarks"
        Me.TxtRemarks.Size = New System.Drawing.Size(384, 75)
        Me.TxtRemarks.TabIndex = 6
        '
        'LinkLabel1
        '
        Me.LinkLabel1.BackColor = System.Drawing.Color.SteelBlue
        Me.LinkLabel1.DisabledLinkColor = System.Drawing.Color.White
        Me.LinkLabel1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel1.LinkColor = System.Drawing.Color.White
        Me.LinkLabel1.Location = New System.Drawing.Point(4, 147)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(261, 20)
        Me.LinkLabel1.TabIndex = 731
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "Purchase Order Amendment For Following Items"
        Me.LinkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblJobWorkerReq
        '
        Me.LblJobWorkerReq.AutoSize = True
        Me.LblJobWorkerReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblJobWorkerReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblJobWorkerReq.Location = New System.Drawing.Point(106, 57)
        Me.LblJobWorkerReq.Name = "LblJobWorkerReq"
        Me.LblJobWorkerReq.Size = New System.Drawing.Size(10, 7)
        Me.LblJobWorkerReq.TabIndex = 732
        Me.LblJobWorkerReq.Text = "Ä"
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
        Me.TxtVendor.Location = New System.Drawing.Point(125, 52)
        Me.TxtVendor.MaxLength = 20
        Me.TxtVendor.Name = "TxtVendor"
        Me.TxtVendor.Size = New System.Drawing.Size(354, 18)
        Me.TxtVendor.TabIndex = 4
        '
        'LblJobWorker
        '
        Me.LblJobWorker.AutoSize = True
        Me.LblJobWorker.BackColor = System.Drawing.Color.Transparent
        Me.LblJobWorker.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblJobWorker.Location = New System.Drawing.Point(14, 52)
        Me.LblJobWorker.Name = "LblJobWorker"
        Me.LblJobWorker.Size = New System.Drawing.Size(48, 16)
        Me.LblJobWorker.TabIndex = 731
        Me.LblJobWorker.Text = "Vendor"
        '
        'TxtTermsAndConditions
        '
        Me.TxtTermsAndConditions.AgAllowUserToEnableMasterHelp = False
        Me.TxtTermsAndConditions.AgLastValueTag = Nothing
        Me.TxtTermsAndConditions.AgLastValueText = Nothing
        Me.TxtTermsAndConditions.AgMandatory = False
        Me.TxtTermsAndConditions.AgMasterHelp = False
        Me.TxtTermsAndConditions.AgNumberLeftPlaces = 0
        Me.TxtTermsAndConditions.AgNumberNegetiveAllow = False
        Me.TxtTermsAndConditions.AgNumberRightPlaces = 0
        Me.TxtTermsAndConditions.AgPickFromLastValue = False
        Me.TxtTermsAndConditions.AgRowFilter = ""
        Me.TxtTermsAndConditions.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtTermsAndConditions.AgSelectedValue = Nothing
        Me.TxtTermsAndConditions.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtTermsAndConditions.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtTermsAndConditions.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtTermsAndConditions.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtTermsAndConditions.Location = New System.Drawing.Point(4, 486)
        Me.TxtTermsAndConditions.MaxLength = 255
        Me.TxtTermsAndConditions.Multiline = True
        Me.TxtTermsAndConditions.Name = "TxtTermsAndConditions"
        Me.TxtTermsAndConditions.Size = New System.Drawing.Size(343, 90)
        Me.TxtTermsAndConditions.TabIndex = 2
        '
        'LinkLabel2
        '
        Me.LinkLabel2.BackColor = System.Drawing.Color.SteelBlue
        Me.LinkLabel2.DisabledLinkColor = System.Drawing.Color.White
        Me.LinkLabel2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel2.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel2.LinkColor = System.Drawing.Color.White
        Me.LinkLabel2.Location = New System.Drawing.Point(4, 464)
        Me.LinkLabel2.Name = "LinkLabel2"
        Me.LinkLabel2.Size = New System.Drawing.Size(131, 20)
        Me.LinkLabel2.TabIndex = 748
        Me.LinkLabel2.TabStop = True
        Me.LinkLabel2.Text = "Terms && Conditions"
        Me.LinkLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtBillingType
        '
        Me.TxtBillingType.AgAllowUserToEnableMasterHelp = False
        Me.TxtBillingType.AgLastValueTag = Nothing
        Me.TxtBillingType.AgLastValueText = Nothing
        Me.TxtBillingType.AgMandatory = False
        Me.TxtBillingType.AgMasterHelp = False
        Me.TxtBillingType.AgNumberLeftPlaces = 0
        Me.TxtBillingType.AgNumberNegetiveAllow = False
        Me.TxtBillingType.AgNumberRightPlaces = 0
        Me.TxtBillingType.AgPickFromLastValue = False
        Me.TxtBillingType.AgRowFilter = ""
        Me.TxtBillingType.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtBillingType.AgSelectedValue = Nothing
        Me.TxtBillingType.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtBillingType.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtBillingType.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtBillingType.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtBillingType.Location = New System.Drawing.Point(93, 217)
        Me.TxtBillingType.MaxLength = 20
        Me.TxtBillingType.Name = "TxtBillingType"
        Me.TxtBillingType.Size = New System.Drawing.Size(101, 18)
        Me.TxtBillingType.TabIndex = 6
        Me.TxtBillingType.Text = "TxtBillingOn"
        Me.TxtBillingType.Visible = False
        '
        'Label32
        '
        Me.Label32.AutoSize = True
        Me.Label32.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label32.Location = New System.Drawing.Point(23, 217)
        Me.Label32.Name = "Label32"
        Me.Label32.Size = New System.Drawing.Size(64, 16)
        Me.Label32.TabIndex = 729
        Me.Label32.Text = "Billing On"
        Me.Label32.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(310, 39)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(10, 7)
        Me.Label3.TabIndex = 764
        Me.Label3.Text = "Ä"
        '
        'TxtV_Nature
        '
        Me.TxtV_Nature.AgAllowUserToEnableMasterHelp = False
        Me.TxtV_Nature.AgLastValueTag = Nothing
        Me.TxtV_Nature.AgLastValueText = Nothing
        Me.TxtV_Nature.AgMandatory = True
        Me.TxtV_Nature.AgMasterHelp = False
        Me.TxtV_Nature.AgNumberLeftPlaces = 8
        Me.TxtV_Nature.AgNumberNegetiveAllow = False
        Me.TxtV_Nature.AgNumberRightPlaces = 2
        Me.TxtV_Nature.AgPickFromLastValue = False
        Me.TxtV_Nature.AgRowFilter = ""
        Me.TxtV_Nature.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtV_Nature.AgSelectedValue = Nothing
        Me.TxtV_Nature.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtV_Nature.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtV_Nature.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtV_Nature.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtV_Nature.Location = New System.Drawing.Point(125, 72)
        Me.TxtV_Nature.MaxLength = 20
        Me.TxtV_Nature.Name = "TxtV_Nature"
        Me.TxtV_Nature.Size = New System.Drawing.Size(354, 18)
        Me.TxtV_Nature.TabIndex = 5
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(14, 74)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(46, 16)
        Me.Label6.TabIndex = 771
        Me.Label6.Text = "Nature"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label9.Location = New System.Drawing.Point(106, 78)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(10, 7)
        Me.Label9.TabIndex = 772
        Me.Label9.Text = "Ä"
        '
        'GrpDirectChallan
        '
        Me.GrpDirectChallan.BackColor = System.Drawing.Color.Transparent
        Me.GrpDirectChallan.Controls.Add(Me.RbtAddNewItem)
        Me.GrpDirectChallan.Controls.Add(Me.RbtPlanForPurchOrder)
        Me.GrpDirectChallan.Controls.Add(Me.RbtForPurchOrderItems)
        Me.GrpDirectChallan.Location = New System.Drawing.Point(271, 140)
        Me.GrpDirectChallan.Name = "GrpDirectChallan"
        Me.GrpDirectChallan.Size = New System.Drawing.Size(456, 25)
        Me.GrpDirectChallan.TabIndex = 750
        Me.GrpDirectChallan.TabStop = False
        '
        'RbtAddNewItem
        '
        Me.RbtAddNewItem.AutoSize = True
        Me.RbtAddNewItem.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RbtAddNewItem.Location = New System.Drawing.Point(319, 7)
        Me.RbtAddNewItem.Name = "RbtAddNewItem"
        Me.RbtAddNewItem.Size = New System.Drawing.Size(116, 17)
        Me.RbtAddNewItem.TabIndex = 753
        Me.RbtAddNewItem.TabStop = True
        Me.RbtAddNewItem.Text = "Add New Item"
        Me.RbtAddNewItem.UseVisualStyleBackColor = True
        '
        'RbtPlanForPurchOrder
        '
        Me.RbtPlanForPurchOrder.AutoSize = True
        Me.RbtPlanForPurchOrder.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RbtPlanForPurchOrder.Location = New System.Drawing.Point(5, 8)
        Me.RbtPlanForPurchOrder.Name = "RbtPlanForPurchOrder"
        Me.RbtPlanForPurchOrder.Size = New System.Drawing.Size(129, 17)
        Me.RbtPlanForPurchOrder.TabIndex = 0
        Me.RbtPlanForPurchOrder.TabStop = True
        Me.RbtPlanForPurchOrder.Text = "For Purch Order"
        Me.RbtPlanForPurchOrder.UseVisualStyleBackColor = True
        '
        'RbtForPurchOrderItems
        '
        Me.RbtForPurchOrderItems.AutoSize = True
        Me.RbtForPurchOrderItems.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RbtForPurchOrderItems.Location = New System.Drawing.Point(137, 7)
        Me.RbtForPurchOrderItems.Name = "RbtForPurchOrderItems"
        Me.RbtForPurchOrderItems.Size = New System.Drawing.Size(171, 17)
        Me.RbtForPurchOrderItems.TabIndex = 743
        Me.RbtForPurchOrderItems.TabStop = True
        Me.RbtForPurchOrderItems.Text = "For Purch Order Items"
        Me.RbtForPurchOrderItems.UseVisualStyleBackColor = True
        '
        'BtnFillPurchOrder
        '
        Me.BtnFillPurchOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnFillPurchOrder.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnFillPurchOrder.Location = New System.Drawing.Point(742, 144)
        Me.BtnFillPurchOrder.Name = "BtnFillPurchOrder"
        Me.BtnFillPurchOrder.Size = New System.Drawing.Size(29, 21)
        Me.BtnFillPurchOrder.TabIndex = 1
        Me.BtnFillPurchOrder.Text = "..."
        Me.BtnFillPurchOrder.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnFillPurchOrder.UseVisualStyleBackColor = True
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
        Me.TxtStructure.Location = New System.Drawing.Point(496, 46)
        Me.TxtStructure.MaxLength = 20
        Me.TxtStructure.Name = "TxtStructure"
        Me.TxtStructure.Size = New System.Drawing.Size(77, 18)
        Me.TxtStructure.TabIndex = 22
        Me.TxtStructure.Tag = ""
        Me.TxtStructure.Visible = False
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
        Me.TxtCustomFields.Location = New System.Drawing.Point(496, 69)
        Me.TxtCustomFields.MaxLength = 20
        Me.TxtCustomFields.Name = "TxtCustomFields"
        Me.TxtCustomFields.Size = New System.Drawing.Size(82, 18)
        Me.TxtCustomFields.TabIndex = 1013
        Me.TxtCustomFields.Text = "TxtCustomFields"
        Me.TxtCustomFields.Visible = False
        '
        'PnlCustomGrid
        '
        Me.PnlCustomGrid.Location = New System.Drawing.Point(382, 464)
        Me.PnlCustomGrid.Name = "PnlCustomGrid"
        Me.PnlCustomGrid.Size = New System.Drawing.Size(221, 112)
        Me.PnlCustomGrid.TabIndex = 751
        '
        'PnlCalcGrid
        '
        Me.PnlCalcGrid.Location = New System.Drawing.Point(659, 463)
        Me.PnlCalcGrid.Name = "PnlCalcGrid"
        Me.PnlCalcGrid.Size = New System.Drawing.Size(313, 112)
        Me.PnlCalcGrid.TabIndex = 752
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
        Me.TxtSalesTaxGroupParty.Location = New System.Drawing.Point(505, 45)
        Me.TxtSalesTaxGroupParty.MaxLength = 20
        Me.TxtSalesTaxGroupParty.Name = "TxtSalesTaxGroupParty"
        Me.TxtSalesTaxGroupParty.Size = New System.Drawing.Size(79, 18)
        Me.TxtSalesTaxGroupParty.TabIndex = 1014
        Me.TxtSalesTaxGroupParty.Visible = False
        '
        'FrmPurchaseOrderAmendment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.BackColor = System.Drawing.SystemColors.ButtonShadow
        Me.ClientSize = New System.Drawing.Size(984, 626)
        Me.Controls.Add(Me.PnlCalcGrid)
        Me.Controls.Add(Me.PnlCustomGrid)
        Me.Controls.Add(Me.GrpDirectChallan)
        Me.Controls.Add(Me.BtnFillPurchOrder)
        Me.Controls.Add(Me.LinkLabel2)
        Me.Controls.Add(Me.TxtTermsAndConditions)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Pnl1)
        Me.Name = "FrmPurchaseOrderAmendment"
        Me.Text = "Finishing Order Amendment Entry"
        Me.Controls.SetChildIndex(Me.TabControl1, 0)
        Me.Controls.SetChildIndex(Me.Pnl1, 0)
        Me.Controls.SetChildIndex(Me.Panel1, 0)
        Me.Controls.SetChildIndex(Me.LinkLabel1, 0)
        Me.Controls.SetChildIndex(Me.TxtTermsAndConditions, 0)
        Me.Controls.SetChildIndex(Me.LinkLabel2, 0)
        Me.Controls.SetChildIndex(Me.GBoxApprove, 0)
        Me.Controls.SetChildIndex(Me.Topctrl1, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.GrpUP, 0)
        Me.Controls.SetChildIndex(Me.GBoxEntryType, 0)
        Me.Controls.SetChildIndex(Me.GBoxMoveToLog, 0)
        Me.Controls.SetChildIndex(Me.GroupBox2, 0)
        Me.Controls.SetChildIndex(Me.GBoxDivision, 0)
        Me.Controls.SetChildIndex(Me.BtnFillPurchOrder, 0)
        Me.Controls.SetChildIndex(Me.GrpDirectChallan, 0)
        Me.Controls.SetChildIndex(Me.PnlCustomGrid, 0)
        Me.Controls.SetChildIndex(Me.PnlCalcGrid, 0)
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
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Protected WithEvents TxtManualRefNo As AgControls.AgTextBox
    Protected WithEvents LblManualRefNo As System.Windows.Forms.Label
    Protected WithEvents Panel1 As System.Windows.Forms.Panel
    Protected WithEvents Pnl1 As System.Windows.Forms.Panel
    Protected WithEvents TxtRemarks As AgControls.AgTextBox
    Protected WithEvents Label30 As System.Windows.Forms.Label
    Protected WithEvents LblTotalQty As System.Windows.Forms.Label
    Protected WithEvents LblTotalQtyText As System.Windows.Forms.Label
    Protected WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    Protected WithEvents LblJobWorkerReq As System.Windows.Forms.Label
    Protected WithEvents TxtVendor As AgControls.AgTextBox
    Protected WithEvents LblJobWorker As System.Windows.Forms.Label
    Protected WithEvents TxtTermsAndConditions As AgControls.AgTextBox
    Protected WithEvents LblTotalAmount As System.Windows.Forms.Label
    Protected WithEvents Label1 As System.Windows.Forms.Label
    Protected WithEvents LinkLabel2 As System.Windows.Forms.LinkLabel
    Protected WithEvents TxtBillingType As AgControls.AgTextBox
    Protected WithEvents Label32 As System.Windows.Forms.Label
    Protected WithEvents Label3 As System.Windows.Forms.Label
    Protected WithEvents TxtV_Nature As AgControls.AgTextBox
    Protected WithEvents Label9 As System.Windows.Forms.Label
    Protected WithEvents Label6 As System.Windows.Forms.Label
    Protected WithEvents GrpDirectChallan As System.Windows.Forms.GroupBox
    Protected WithEvents RbtPlanForPurchOrder As System.Windows.Forms.RadioButton
    Protected WithEvents RbtForPurchOrderItems As System.Windows.Forms.RadioButton
    Protected WithEvents BtnFillPurchOrder As System.Windows.Forms.Button
#End Region

    Private Sub FrmPurchaseOrderAmendment_BaseEvent_ApproveDeletion_InTrans(ByVal SearchCode As String, ByVal Conn As SqliteConnection, ByVal Cmd As SqliteCommand) Handles Me.BaseEvent_ApproveDeletion_InTrans
        mQry = "DELETE FROM PurchOrderDetail WHERE GenDocId = '" & mSearchCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    End Sub

    Private Sub FrmQuality1_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "PurchOrder"
        LogTableName = "PurchOrder_Log"
        MainLineTableCsv = "PurchOrderdetail"
        LogLineTableCsv = "PurchOrderdetail_Log"

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

        If IsApplyVTypePermission Then
            mCondStr = mCondStr & " And H.V_Type In (Select V_Type From User_VType_Permission VP Where VP.UserName = '" & AgL.PubUserName & "' And VP.Div_Code = '" & AgL.PubDivCode & "' And VP.Site_Code = '" & AgL.PubSiteCode & "') "
        End If

        mQry = " Select H.DocID As SearchCode " &
            " From PurchOrder H   " &
            " Left Join Voucher_Type Vt   On H.V_Type = Vt.V_Type  " &
            " Where IfNull(IsDeleted,0) = 0  " & mCondStr & "  Order By H.V_Date Desc "

        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mCondStr$ = ""

        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) &
                       " And IfNull(H.IsDeleted,0)=0 And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "H.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        If IsApplyVTypePermission Then
            mCondStr = mCondStr & " And H.V_Type In (Select V_Type From User_VType_Permission VP Where VP.UserName = '" & AgL.PubUserName & "' And VP.Div_Code = '" & AgL.PubDivCode & "' And VP.Site_Code = '" & AgL.PubSiteCode & "') "
        End If

        AgL.PubFindQry = " SELECT H.DocId AS SearchCode, H.V_Date AS [Purchase_Order_Date], H.ManualRefNo AS [Purch_Order_No], " &
                    " H.VendorName AS [Vendor_Name], H.Remarks, L.TotalQty AS [Total_Qty], L.TotalDeliveryMeasure AS [Total_Delivery_Measure], H.TotalAmount AS [Total_Amount],  " &
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

    Private Sub FrmProductionOrder_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl1, Col1ItemCode, 100, 0, Col1ItemCode, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_ItemCode")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_ItemCode")), Boolean))
            .AddAgTextColumn(Dgl1, Col1Item, 200, 0, Col1Item, True, Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_ItemName")), Boolean))
            .AddAgTextColumn(Dgl1, Col1PurchOrder, 100, 0, Col1PurchOrder, True, True)
            .AddAgTextColumn(Dgl1, Col1PurchOrderSr, 100, 0, Col1PurchOrderSr, False, True)
            .AddAgTextColumn(Dgl1, Col1BillingType, 45, 0, Col1BillingType, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_BillingType")), Boolean), True)
            .AddAgTextColumn(Dgl1, Col1RateType, 40, 0, Col1RateType, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_RateType")), Boolean), True)
            .AddAgNumberColumn(Dgl1, Col1Qty, 50, 8, 4, True, Col1Qty, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Qty")), Boolean), True, True)
            .AddAgNumberColumn(Dgl1, Col1FreeQty, 50, 8, 4, True, Col1FreeQty, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_FreeQty")), Boolean), False, True)
            .AddAgNumberColumn(Dgl1, Col1DocQty, 50, 8, 4, True, Col1DocQty, True, False, True)
            .AddAgTextColumn(Dgl1, Col1Unit, 40, 0, Col1Unit, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Unit")), Boolean), True)
            .AddAgTextColumn(Dgl1, Col1SalesTaxGroup, 40, 0, Col1SalesTaxGroup, False, True)
            .AddAgTextColumn(Dgl1, Col1QtyDecimalPlaces, 50, 0, Col1QtyDecimalPlaces, False, True, False)
            .AddAgNumberColumn(Dgl1, Col1MeasurePerPcs, 70, 8, 4, False, Col1MeasurePerPcs, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_MeasurePerPcs")), Boolean), True, True)
            .AddAgNumberColumn(Dgl1, Col1TotalDocMeasure, 80, 8, 4, True, Col1TotalDocMeasure, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Measure")), Boolean), True, True)
            .AddAgNumberColumn(Dgl1, Col1TotalMeasure, 80, 8, 4, True, Col1TotalMeasure, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Measure")), Boolean), True, True)
            .AddAgNumberColumn(Dgl1, Col1TotalFreeMeasure, 80, 8, 4, True, Col1TotalFreeMeasure, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Measure")), Boolean), True, True)
            .AddAgTextColumn(Dgl1, Col1MeasureUnit, 70, 0, Col1MeasureUnit, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_MeasureUnit")), Boolean), True)
            .AddAgTextColumn(Dgl1, Col1MeasureDecimalPlaces, 50, 0, Col1MeasureDecimalPlaces, False, True, False)
            .AddAgTextColumn(Dgl1, Col1DeliveryMeasure, 70, 0, Col1DeliveryMeasure, True, True)
            .AddAgNumberColumn(Dgl1, Col1DeliveryMeasureMultiplier, 70, 8, 4, False, Col1DeliveryMeasureMultiplier, True, True, True)
            .AddAgNumberColumn(Dgl1, Col1DeliveryMeasurePerPcs, 70, 8, 4, False, Col1DeliveryMeasurePerPcs, True, True, True)
            .AddAgNumberColumn(Dgl1, Col1TotalDeliveryMeasure, 70, 8, 4, True, Col1TotalDeliveryMeasure, True, True, True)
            .AddAgNumberColumn(Dgl1, Col1TotalFreeDeliveryMeasure, 70, 8, 4, True, Col1TotalFreeDeliveryMeasure, True, True, True)
            .AddAgNumberColumn(Dgl1, Col1DeliveryMeasureDecimalPlaces, 70, 8, 4, True, Col1DeliveryMeasureDecimalPlaces, True, True, True)
            .AddAgNumberColumn(Dgl1, Col1MRP, 60, 8, 2, True, Col1MRP, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_MRP")), Boolean), True, True)
            .AddAgTextColumn(Dgl1, Col1Deal, 50, 0, Col1Deal, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Deal")), Boolean), True, False)
            .AddAgTextColumn(Dgl1, Col1Specification, 50, 0, Col1Specification, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Specification")), Boolean), True, False)
            .AddAgNumberColumn(Dgl1, Col1Rate, 60, 8, 2, True, Col1Rate, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Rate")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_Rate")), Boolean), True)
            .AddAgNumberColumn(Dgl1, Col1Amount, 70, 8, 2, False, Col1Amount, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Amount")), Boolean), True, True)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 48
        Dgl1.AgSkipReadOnlyColumns = True
        Dgl1.AllowUserToOrderColumns = True

        AgTemplate.ClsMain.ProcCreateLink(Dgl1, Col1PurchOrder)


        AgCalcGrid1.AgLineGridPostingGroupSalesTaxProd = Dgl1.Columns(Col1SalesTaxGroup).Index
        AgCalcGrid1.Ini_Grid(LblV_Type.Tag, TxtV_Date.Text)

        AgCalcGrid1.AgLineGrid = Dgl1
        AgCalcGrid1.AgLineGridMandatoryColumn = Dgl1.Columns(Col1Item).Index
        AgCalcGrid1.AgLineGridGrossColumn = Dgl1.Columns(Col1Amount).Index


        AgCustomGrid1.Ini_Grid(mSearchCode)
        AgCustomGrid1.SplitGrid = False

        AgCalcGrid1.Name = "AgCalcGrid1"
        AgCustomGrid1.Name = "AgCustomGrid1"

        If AgL.PubUserName.ToUpper = AgLibrary.ClsConstant.PubSuperUserName.ToUpper Then
            AgCL.GridSetiingWriteXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1)
            AgCL.GridSetiingWriteXml(Me.Text & AgCalcGrid1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, AgCalcGrid1)
            AgCL.GridSetiingWriteXml(Me.Text & AgCustomGrid1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, AgCustomGrid1)
        End If

    End Sub

    Private Sub FrmProductionOrder_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As SQLiteConnection, ByVal Cmd As SQLiteCommand) Handles Me.BaseEvent_Save_InTrans
        Dim I As Integer, mSr As Integer
        Dim bSelectionQry$ = ""
        Dim mPurchOrderSr As Integer

        mQry = "UPDATE PurchOrder " &
                " SET " &
                " ManualRefNo = " & AgL.Chk_Text(TxtManualRefNo.Text) & ", " &
                " ReferenceNo = " & AgL.Chk_Text(TxtManualRefNo.Text) & ", " &
                " Vendor = " & AgL.Chk_Text(TxtVendor.Tag) & ", " &
                " VendorName = " & AgL.Chk_Text(TxtVendor.Text) & ", " &
                " BillingType = " & AgL.Chk_Text(TxtBillingType.Text) & ", " &
                " Structure = " & AgL.Chk_Text(TxtStructure.Tag) & ", " &
                " CustomFields = " & AgL.Chk_Text(TxtCustomFields.Tag) & ", " &
                " Remarks = " & AgL.Chk_Text(TxtRemarks.Text) & ", " &
                " TermsAndConditions = " & AgL.Chk_Text(TxtTermsAndConditions.Text) & ", " &
                "   " & AgCalcGrid1.FFooterTableUpdateStr() & " " &
                "   " & AgCustomGrid1.FFooterTableUpdateStr() & " " &
                " Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        'Never Try to Serialise Sr in Line Items 
        'As Some other Entry points may updating values to this Search code and Sr
        mSr = AgL.VNull(AgL.Dman_Execute("Select Max(Sr) From PurchOrderDetail  Where DocID = '" & mSearchCode & "'", AgL.GcnRead).ExecuteScalar)

        With Dgl1
            For I = 0 To .Rows.Count - 1
                If .Item(Col1Item, I).Value <> "" Then
                    If Dgl1.Item(ColSNo, I).Tag Is Nothing And Dgl1.Rows(I).Visible = True Then
                        mSr = mSr + 1
                        mPurchOrderSr = AgL.VNull(AgL.Dman_Execute("Select Max(Sr) From PurchOrderDetail  Where DocID = '" & Dgl1.Item(Col1PurchOrder, I).Tag & "'", AgL.GcnRead).ExecuteScalar) + 1

                        If Val(.Item(Col1PurchOrderSr, I).Value) = 0 Then
                            mQry = " INSERT INTO PurchOrderDetail (DocId, Sr,  GenDocId, GenDocIdSr, Item, SalesTaxGroupItem, Qty, Unit, " &
                                      " MeasurePerPcs, MeasureUnit,	TotalMeasure, Rate,	Amount,	PurchOrder,	PurchOrderSr, BillingType, " &
                                      " DeliveryMeasureMultiplier, TotalDeliveryMeasure, RateType,	DeliveryMeasure, DeliveryMeasurePerPcs, " &
                                      " FreeQty, TotalFreeMeasure, MRP, Deal, Specification, T_Nature, V_Nature, RateAffected, TotalFreeDeliveryMeasure,	DocQty,	DocMeasure ) " &
                                      " Values(" & AgL.Chk_Text(Dgl1.Item(Col1PurchOrder, I).Tag) & ", " & Val(mPurchOrderSr) & ", " &
                                      " " & AgL.Chk_Text(SearchCode) & ", " & mSr & ", " &
                                      " " & AgL.Chk_Text(Dgl1.Item(Col1Item, I).Tag) & ", " &
                                      " " & AgL.Chk_Text(Dgl1.Item(Col1SalesTaxGroup, I).Tag) & ", " &
                                      " 0, " &
                                      " " & AgL.Chk_Text(Dgl1.Item(Col1Unit, I).Value) & ", " &
                                      " " & Val(Dgl1.Item(Col1MeasurePerPcs, I).Value) & ", " &
                                      " " & AgL.Chk_Text(Dgl1.Item(Col1MeasureUnit, I).Value) & ", " &
                                      " 0, " &
                                      " " & Val(Dgl1.Item(Col1Rate, I).Value) & ", " &
                                      " 0, " &
                                      " " & AgL.Chk_Text(Dgl1.Item(Col1PurchOrder, I).Tag) & ", " &
                                      " " & Val(mPurchOrderSr) & ", " &
                                      " " & AgL.Chk_Text(Dgl1.Item(Col1BillingType, I).Value) & ", " &
                                      " " & Val(Dgl1.Item(Col1DeliveryMeasureMultiplier, I).Value) & ", " &
                                      " 0, " &
                                      " " & AgL.Chk_Text(Dgl1.Item(Col1RateType, I).Tag) & ", " &
                                      " " & AgL.Chk_Text(Dgl1.Item(Col1DeliveryMeasure, I).Value) & ", " &
                                      " " & Val(Dgl1.Item(Col1DeliveryMeasurePerPcs, I).Value) & ", " &
                                      " " & Val(Dgl1.Item(Col1FreeQty, I).Value) & ", " &
                                      " " & Val(Dgl1.Item(Col1TotalFreeMeasure, I).Value) & ", " &
                                      " " & Val(Dgl1.Item(Col1MRP, I).Value) & ", " &
                                      " " & AgL.Chk_Text(Dgl1.Item(Col1Deal, I).Value) & ", " &
                                      " " & AgL.Chk_Text(Dgl1.Item(Col1Specification, I).Value) & ", " &
                                      " " & AgTemplate.ClsMain.T_Nature.Amendment & ", " &
                                      " " & AgL.Chk_Text(TxtV_Nature.Text) & ", " &
                                      " " & IIf(TxtV_Nature.Text = "Rate Amendment", 1, 0) & ", " &
                                      " " & Val(Dgl1.Item(Col1TotalFreeDeliveryMeasure, I).Value) & ", " &
                                      " " & Val(Dgl1.Item(Col1DocQty, I).Value) & ", " &
                                      " " & Val(Dgl1.Item(Col1TotalDocMeasure, I).Value) & " ) "
                            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                            .Item(Col1PurchOrderSr, I).Value = mPurchOrderSr
                        End If

                        If bSelectionQry <> "" Then bSelectionQry += " UNION ALL "
                        bSelectionQry += " Select " & AgL.Chk_Text(mSearchCode) & ", " & mSr & ", " &
                                      " " & AgL.Chk_Text(.Item(Col1Item, I).Tag) & ", " & AgL.Chk_Text(.Item(Col1SalesTaxGroup, I).Value) & ", " &
                                      " " & Val(.Item(Col1Qty, I).Value) & ", " & AgL.Chk_Text(.Item(Col1Unit, I).Value) & ", " &
                                      " " & Val(.Item(Col1MeasurePerPcs, I).Value) & ", " & AgL.Chk_Text(.Item(Col1MeasureUnit, I).Value) & ", " &
                                      "	" & Val(.Item(Col1TotalMeasure, I).Value) & ", " & Val(.Item(Col1Rate, I).Value) & ", " & Val(.Item(Col1Amount, I).Value) & ", " &
                                      "	" & AgL.Chk_Text(.Item(Col1PurchOrder, I).Tag) & ",	" & AgL.Chk_Text(.Item(Col1PurchOrderSr, I).Value) & ", " & AgL.Chk_Text(.Item(Col1BillingType, I).Value) & ", " &
                                      " " & Val(.Item(Col1DeliveryMeasureMultiplier, I).Value) & ", " & Val(.Item(Col1TotalDeliveryMeasure, I).Value) & ", " & AgL.Chk_Text(.Item(Col1RateType, I).Value) & ", " &
                                      "	" & AgL.Chk_Text(.Item(Col1DeliveryMeasure, I).Value) & ", " & Val(.Item(Col1DeliveryMeasurePerPcs, I).Value) & ", " &
                                      " " & Val(.Item(Col1FreeQty, I).Value) & ", " & Val(.Item(Col1TotalFreeMeasure, I).Value) & ", " & Val(.Item(Col1MRP, I).Value) & ", " &
                                      " " & AgL.Chk_Text(.Item(Col1Deal, I).Value) & ", " & AgL.Chk_Text(Dgl1.Item(Col1Specification, I).Value) & ", " & AgL.Chk_Text(TxtV_Nature.Text) & ", " & AgTemplate.ClsMain.T_Nature.Amendment & ", " & IIf(TxtV_Nature.Text = "Rate Amendment", 1, 0) & ", " &
                                      " " & Val(.Item(Col1TotalFreeDeliveryMeasure, I).Value) & ",	" & Val(.Item(Col1DocQty, I).Value) & ", " & Val(.Item(Col1TotalDocMeasure, I).Value) & ", " &
                                      " " & AgCalcGrid1.FLineTableFieldValuesStr(I) & " "

                    Else
                        If Dgl1.Rows(I).Visible = True Then
                            If Dgl1.Rows(I).DefaultCellStyle.BackColor <> AgTemplate.ClsMain.Colours.GridRow_Locked Then
                                mQry = " UPDATE PurchOrderDetail SET " &
                                         " Item = " & AgL.Chk_Text(Dgl1.Item(Col1Item, I).Tag) & ", " &
                                         " SalesTaxGroupItem = " & AgL.Chk_Text(Dgl1.Item(Col1SalesTaxGroup, I).Value) & ", " &
                                         " Qty = " & Val(Dgl1.Item(Col1Qty, I).Value) & ", " &
                                         " Unit = " & AgL.Chk_Text(Dgl1.Item(Col1Unit, I).Value) & ", " &
                                         " MeasurePerPcs = " & Val(Dgl1.Item(Col1MeasurePerPcs, I).Value) & ", " &
                                         " MeasureUnit = " & AgL.Chk_Text(Dgl1.Item(Col1MeasureUnit, I).Value) & ", " &
                                         " TotalMeasure = " & Val(Dgl1.Item(Col1TotalMeasure, I).Value) & ", " &
                                         " Rate = " & Val(Dgl1.Item(Col1Rate, I).Value) & ", " &
                                         " Amount = " & Val(Dgl1.Item(Col1Amount, I).Value) & ", " &
                                         " PurchOrder = " & AgL.Chk_Text(Dgl1.Item(Col1PurchOrder, I).Tag) & ", " &
                                         " PurchOrderSr = " & Val(Dgl1.Item(Col1PurchOrderSr, I).Value) & ", " &
                                         " BillingType = " & AgL.Chk_Text(Dgl1.Item(Col1BillingType, I).Value) & ", " &
                                         " DeliveryMeasureMultiplier = " & Val(Dgl1.Item(Col1DeliveryMeasureMultiplier, I).Value) & ", " &
                                         " TotalDeliveryMeasure = " & Val(Dgl1.Item(Col1TotalDeliveryMeasure, I).Value) & ", " &
                                         " RateType = " & AgL.Chk_Text(Dgl1.Item(Col1RateType, I).Value) & ", " &
                                         " DeliveryMeasure = " & Val(Dgl1.Item(Col1TotalDeliveryMeasure, I).Value) & ", " &
                                         " DeliveryMeasurePerPcs = " & Val(Dgl1.Item(Col1DeliveryMeasurePerPcs, I).Value) & ", " &
                                         " FreeQty = " & Val(Dgl1.Item(Col1FreeQty, I).Value) & ", " &
                                         " TotalFreeMeasure = " & Val(Dgl1.Item(Col1TotalFreeMeasure, I).Value) & ", " &
                                         " MRP = " & Val(Dgl1.Item(Col1MRP, I).Value) & ", " &
                                         " Deal = " & AgL.Chk_Text(Dgl1.Item(Col1Deal, I).Value) & ", " &
                                         " Specification = " & AgL.Chk_Text(Dgl1.Item(Col1Specification, I).Value) & ", " &
                                         " V_Nature = " & AgL.Chk_Text(TxtV_Nature.Text) & ", " &
                                         " T_Nature = " & AgTemplate.ClsMain.T_Nature.Amendment & ", " &
                                         " RateAffected = " & IIf(TxtV_Nature.Text = "Rate Amendment", 1, 0) & ", " &
                                         " TotalFreeDeliveryMeasure = " & Val(Dgl1.Item(Col1TotalFreeDeliveryMeasure, I).Value) & ", " &
                                         " DocQty = " & Val(Dgl1.Item(Col1DocQty, I).Value) & ", " &
                                         " DocMeasure = " & Val(Dgl1.Item(Col1TotalDocMeasure, I).Value) & ", " &
                                         " " & AgCalcGrid1.FLineTableUpdateStr(I) & " " &
                                         " Where DocId = '" & mSearchCode & "' " &
                                         " And Sr = " & Dgl1.Item(ColSNo, I).Tag & " "
                                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                                mQry = " UPDATE PurchOrderDetail SET " &
                                         " Item = " & AgL.Chk_Text(Dgl1.Item(Col1Item, I).Tag) & ", " &
                                         " SalesTaxGroupItem = " & AgL.Chk_Text(Dgl1.Item(Col1SalesTaxGroup, I).Value) & ", " &
                                         " Qty = 0, " &
                                         " Unit = " & AgL.Chk_Text(Dgl1.Item(Col1Unit, I).Value) & ", " &
                                         " MeasurePerPcs = " & Val(Dgl1.Item(Col1MeasurePerPcs, I).Value) & ", " &
                                         " MeasureUnit = " & AgL.Chk_Text(Dgl1.Item(Col1MeasureUnit, I).Value) & ", " &
                                         " TotalMeasure = 0, " &
                                         " Rate = " & Val(Dgl1.Item(Col1Rate, I).Value) & ", " &
                                         " Amount = " & Val(Dgl1.Item(Col1Amount, I).Value) & ", " &
                                         " PurchOrder = " & AgL.Chk_Text(Dgl1.Item(Col1PurchOrder, I).Tag) & ", " &
                                         " PurchOrderSr = " & Val(Dgl1.Item(Col1PurchOrderSr, I).Value) & ", " &
                                         " BillingType = " & AgL.Chk_Text(Dgl1.Item(Col1BillingType, I).Value) & ", " &
                                         " DeliveryMeasureMultiplier = " & Val(Dgl1.Item(Col1DeliveryMeasureMultiplier, I).Value) & ", " &
                                         " TotalDeliveryMeasure = " & Val(Dgl1.Item(Col1TotalDeliveryMeasure, I).Value) & ", " &
                                         " RateType = " & AgL.Chk_Text(Dgl1.Item(Col1RateType, I).Value) & ", " &
                                         " DeliveryMeasure = " & Val(Dgl1.Item(Col1TotalDeliveryMeasure, I).Value) & ", " &
                                         " DeliveryMeasurePerPcs = " & Val(Dgl1.Item(Col1DeliveryMeasurePerPcs, I).Value) & ", " &
                                         " FreeQty = " & Val(Dgl1.Item(Col1FreeQty, I).Value) & ", " &
                                         " TotalFreeMeasure = " & Val(Dgl1.Item(Col1TotalFreeMeasure, I).Value) & ", " &
                                         " MRP = " & Val(Dgl1.Item(Col1MRP, I).Value) & ", " &
                                         " Deal = " & AgL.Chk_Text(Dgl1.Item(Col1Deal, I).Value) & ", " &
                                         " Specification = " & AgL.Chk_Text(Dgl1.Item(Col1Specification, I).Value) & ", " &
                                         " V_Nature = " & AgL.Chk_Text(TxtV_Nature.Text) & ", " &
                                         " T_Nature = " & AgTemplate.ClsMain.T_Nature.Amendment & ", " &
                                         " RateAffected = " & IIf(TxtV_Nature.Text = "Rate Amendment", 1, 0) & ", " &
                                         " TotalFreeDeliveryMeasure = " & Val(Dgl1.Item(Col1TotalFreeDeliveryMeasure, I).Value) & ", " &
                                         " DocQty = " & Val(Dgl1.Item(Col1DocQty, I).Value) & ", " &
                                         " DocMeasure = " & Val(Dgl1.Item(Col1TotalDocMeasure, I).Value) & " " &
                                         " Where GenDocId = '" & SearchCode & "' And GenDocIdSr = " & Val(Dgl1.Item(ColSNo, I).Tag) & " "
                                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                            End If
                        Else
                            mQry = " Delete From PurchOrderDetail Where DocId = '" & mSearchCode & "' And Sr = " & Dgl1.Item(ColSNo, I).Tag & "  "
                            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                            mQry = " Delete From PurchOrderDetail Where GenDocId = '" & mSearchCode & "' And GenDocIDSr = " & Dgl1.Item(ColSNo, I).Tag & "  "
                            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                        End If
                    End If
                End If
            Next
        End With

        If bSelectionQry <> "" Then
            mQry = " INSERT INTO PurchOrderDetail (DocId, Sr, Item, SalesTaxGroupItem, Qty, Unit, " &
                    " MeasurePerPcs, MeasureUnit,	TotalMeasure, Rate,	Amount,	PurchOrder,	PurchOrderSr, BillingType, " &
                    " DeliveryMeasureMultiplier, TotalDeliveryMeasure, RateType,	DeliveryMeasure, DeliveryMeasurePerPcs, " &
                    " FreeQty, TotalFreeMeasure, MRP, Deal, Specification, V_Nature, T_Nature, RateAffected, TotalFreeDeliveryMeasure,	DocQty,	DocMeasure , " & AgCalcGrid1.FLineTableFieldNameStr() & "  ) " & bSelectionQry
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If


        If CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsPostedInStockVirtual")), Boolean) = True Then
            FPostInStockVertual(mSearchCode, Conn, Cmd)
        End If

        If AgL.PubUserName.ToUpper = AgLibrary.ClsConstant.PubSuperUserName.ToUpper Then
            AgCL.GridSetiingWriteXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1)
            AgCL.GridSetiingWriteXml(Me.Text & AgCalcGrid1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, AgCalcGrid1)
            AgCL.GridSetiingWriteXml(Me.Text & AgCustomGrid1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, AgCustomGrid1)
        End If
    End Sub

    Private Sub FrmProductionOrder_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim I As Integer
        Dim DsTemp As DataSet

        Dim intQtyDecimalPlaces As Integer = 0
        Dim intMeasureDecimalPlaces As Integer = 0
        Dim intDeliveryMeasureDecimalPlaces As Integer = 0

        Dim IsSameUnit As Boolean = True
        Dim IsSameMeasureUnit As Boolean = True
        Dim IsSameDeliveryMeasureUnit As Boolean = True

        mQry = " SELECT H.*, SG.DispName AS VendorDispName " &
                " FROM PurchOrder H " &
                " LEFT JOIN SubGroup SG ON SG.SubCode = H.Vendor  " &
                " Where H.DocID = '" & SearchCode & "'"
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                TxtManualRefNo.Text = AgL.XNull(.Rows(0)("ManualRefNo"))
                TxtVendor.Tag = AgL.XNull(.Rows(0)("Vendor"))
                TxtVendor.Text = AgL.XNull(.Rows(0)("VendorDispName"))
                TxtRemarks.Text = AgL.XNull(.Rows(0)("Remarks"))
                TxtTermsAndConditions.Text = AgL.XNull(.Rows(0)("TermsAndConditions"))
                TxtBillingType.Text = AgL.XNull(.Rows(0)("BillingType"))

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

                AgCalcGrid1.FMoveRecFooterTable(DsTemp.Tables(0), EntryNCat, TxtV_Date.Text)

                AgCustomGrid1.FMoveRecFooterTable(DsTemp.Tables(0))

                '-------------------------------------------------------------
                'Line Records are showing in First Grid
                '-------------------------------------------------------------
                mQry = " SELECT L.* , I.ManualCode AS ItemCode, I.Description AS ItemDesc, PO.V_Type || '-' || PO.ManualRefNo  AS PurchOrderNo, PO.V_Date AS PurchOrderdate , " &
                        " U.DecimalPlaces as QtyDecimalPlaces, MU.DecimalPlaces as MeasureDecimalPlaces " &
                        " FROM PurchOrderDetail L " &
                        " LEFT JOIN Item I ON I.Code = L.Item  " &
                        " LEFT JOIN PurchOrder PO ON PO.DocID = L.PurchOrder  " &
                        " Left Join Unit U On L.Unit = U.Code " &
                        " Left Join Unit MU On L.MeasureUnit = MU.Code " &
                        " Where L.DocId = '" & SearchCode & "' Order By Sr "

                DsTemp = AgL.FillData(mQry, AgL.GCn)
                With DsTemp.Tables(0)
                    Dgl1.RowCount = 1
                    Dgl1.Rows.Clear()
                    If .Rows.Count > 0 Then
                        For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                            Dgl1.Rows.Add()
                            Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count - 1
                            Dgl1.Item(ColSNo, I).Tag = AgL.XNull(.Rows(I)("Sr"))
                            TxtV_Nature.Text = AgL.XNull(.Rows(I)("V_Nature"))
                            Dgl1.Item(Col1ItemCode, I).Tag = AgL.XNull(.Rows(I)("Item"))
                            Dgl1.Item(Col1ItemCode, I).Value = AgL.XNull(.Rows(I)("ItemCode"))
                            Dgl1.Item(Col1Item, I).Tag = AgL.XNull(.Rows(I)("Item"))
                            Dgl1.Item(Col1Item, I).Value = AgL.XNull(.Rows(I)("ItemDesc"))

                            Dgl1.Item(Col1PurchOrder, I).Tag = AgL.XNull(.Rows(I)("PurchOrder"))
                            Dgl1.Item(Col1PurchOrder, I).Value = AgL.XNull(.Rows(I)("PurchOrderNo"))
                            Dgl1.Item(Col1PurchOrderSr, I).Value = AgL.VNull(.Rows(I)("PurchOrderSr"))

                            Dgl1.Item(Col1BillingType, I).Value = AgL.XNull(.Rows(I)("BillingType"))
                            Dgl1.Item(Col1RateType, I).Value = AgL.XNull(.Rows(I)("RateType"))
                            Dgl1.Item(Col1QtyDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("QtyDecimalPlaces"))
                            Dgl1.Item(Col1DocQty, I).Value = Format(AgL.VNull(.Rows(I)("DocQty")), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1FreeQty, I).Value = Format(AgL.VNull(.Rows(I)("FreeQty")), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1Qty, I).Value = Format(AgL.VNull(.Rows(I)("Qty")), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                            Dgl1.Item(Col1SalesTaxGroup, I).Tag = AgL.XNull(.Rows(I)("SalesTaxGroupItem"))
                            Dgl1.Item(Col1SalesTaxGroup, I).Value = AgL.XNull(.Rows(I)("SalesTaxGroupItem"))
                            Dgl1.Item(Col1MeasurePerPcs, I).Value = Format(AgL.VNull(.Rows(I)("MeasurePerPcs")), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1TotalDocMeasure, I).Value = Format(AgL.VNull(.Rows(I)("TotalDocMeasure")), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1TotalMeasure, I).Value = Format(AgL.VNull(.Rows(I)("TotalMeasure")), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1TotalFreeMeasure, I).Value = Format(AgL.VNull(.Rows(I)("TotalFreeMeasure")), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1MeasureUnit, I).Value = AgL.XNull(.Rows(I)("MeasureUnit"))
                            Dgl1.Item(Col1MeasureDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("MeasureDecimalPlaces"))
                            Dgl1.Item(Col1DeliveryMeasurePerPcs, I).Value = AgL.VNull(.Rows(I)("DeliveryMeasurePerPcs"))
                            Dgl1.Item(Col1DeliveryMeasure, I).Value = AgL.XNull(.Rows(I)("TotalDeliveryMeasure"))
                            Dgl1.Item(Col1DeliveryMeasureMultiplier, I).Value = AgL.VNull(.Rows(I)("DeliveryMeasureMultiplier"))
                            Dgl1.Item(Col1TotalDeliveryMeasure, I).Value = AgL.VNull(.Rows(I)("TotalDeliveryMeasure"))
                            Dgl1.Item(Col1TotalFreeDeliveryMeasure, I).Value = AgL.VNull(.Rows(I)("TotalFreeDeliveryMeasure"))
                            Dgl1.Item(Col1MRP, I).Value = AgL.XNull(.Rows(I)("MRP"))
                            Dgl1.Item(Col1Deal, I).Value = AgL.XNull(.Rows(I)("Deal"))
                            Dgl1.Item(Col1Specification, I).Value = AgL.XNull(.Rows(I)("Specification"))
                            Dgl1.Item(Col1Rate, I).Value = AgL.VNull(.Rows(I)("Rate"))
                            Dgl1.Item(Col1Amount, I).Value = AgL.VNull(.Rows(I)("Amount"))


                            If Not AgL.StrCmp(Dgl1.Item(Col1Unit, I).Value, Dgl1.Item(Col1Unit, 0).Value) Then IsSameUnit = False
                            If Not AgL.StrCmp(Dgl1.Item(Col1MeasureUnit, I).Value, Dgl1.Item(Col1MeasureUnit, 0).Value) Then IsSameMeasureUnit = False
                            If Not AgL.StrCmp(Dgl1.Item(Col1DeliveryMeasure, I).Value, Dgl1.Item(Col1DeliveryMeasure, 0).Value) Then IsSameDeliveryMeasureUnit = False

                            If intQtyDecimalPlaces < Val(Dgl1.Item(Col1QtyDecimalPlaces, I).Value) Then intQtyDecimalPlaces = Val(Dgl1.Item(Col1QtyDecimalPlaces, I).Value)
                            If intMeasureDecimalPlaces < Val(Dgl1.Item(Col1MeasureDecimalPlaces, I).Value) Then intMeasureDecimalPlaces = Val(Dgl1.Item(Col1MeasureDecimalPlaces, I).Value)
                            If intDeliveryMeasureDecimalPlaces < Val(Dgl1.Item(Col1DeliveryMeasureDecimalPlaces, I).Value) Then intDeliveryMeasureDecimalPlaces = Val(Dgl1.Item(Col1DeliveryMeasureDecimalPlaces, I).Value)

                            LblTotalQty.Text = Val(LblTotalQty.Text) + Val(Dgl1.Item(Col1Qty, I).Value)
                            LblTotalAmount.Text = Val(LblTotalAmount.Text) + Val(Dgl1.Item(Col1Amount, I).Value)


                            Call AgCalcGrid1.FMoveRecLineTable(DsTemp.Tables(0), I)

                        Next I
                    End If
                End With
                'Calculation()
                If AgCustomGrid1.Rows.Count = 0 Then AgCustomGrid1.Visible = False
                '-------------------------------------------------------------
            End If
        End With

        AgCL.GridSetiingShowXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1, False)
    End Sub

    Private Sub FrmProductionOrder_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Topctrl1.ChangeAgGridState(Dgl1, False)
        AgL.WinSetting(Me, 654, 990, 0, 0)
        AgCalcGrid1.FrmType = Me.FrmType
        AgCustomGrid1.FrmType = Me.FrmType
    End Sub

    Private Sub DGL1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.KeyDown
        If Topctrl1.Mode = "Browse" Then Exit Sub
        If e.Control And e.KeyCode = Keys.D Then
            sender.CurrentRow.Selected = True
            sender.CurrentRow.Visible = False
        End If
        If e.Control Or e.Shift Or e.Alt Then Exit Sub

    End Sub

    Private Sub Dgl1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellEnter
        Try
            If AgL.StrCmp(Topctrl1.Mode, "Browse") Then Exit Sub
            If Dgl1.CurrentCell Is Nothing Then Exit Sub
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Qty, Col1DocQty
                    CType(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex), AgControls.AgTextColumn).AgNumberRightPlaces = Val(Dgl1.Item(Col1QtyDecimalPlaces, Dgl1.CurrentCell.RowIndex).Value)

                Case Col1MeasurePerPcs, Col1TotalMeasure, Col1TotalDocMeasure
                    CType(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex), AgControls.AgTextColumn).AgNumberRightPlaces = Val(Dgl1.Item(Col1MeasureDecimalPlaces, Dgl1.CurrentCell.RowIndex).Value)

            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub DGL1_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles Dgl1.RowsAdded
        'sender(ColSNo, sender.Rows.Count - 1).Value = Trim(sender.Rows.Count)
        sender(ColSNo, e.RowIndex).Value = e.RowIndex + 1
    End Sub

    Private Sub FrmProductionOrder_BaseFunction_Calculation() Handles Me.BaseFunction_Calculation
        Dim I As Integer

        LblTotalQty.Text = 0 : LblTotalAmount.Text = 0

        AgCalcGrid1.AgLineGridPostingGroupSalesTaxProd = Dgl1.Columns(Col1SalesTaxGroup).Index
        AgCalcGrid1.AgPostingGroupSalesTaxParty = TxtSalesTaxGroupParty.AgSelectedValue
        AgCalcGrid1.AgPostingGroupSalesTaxItem = AgL.XNull(AgL.PubDtEnviro.Rows(0)("DefaultSalesTaxGroupItem"))

        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Rows(I).Visible = True Then
                If Dgl1.Item(Col1Item, I).Value <> "" Then
                    If TxtV_Nature.Text = "Qty Amendment" Then
                        Dgl1.Item(Col1Qty, I).Value = Val(Dgl1.Item(Col1DocQty, I).Value)
                        Dgl1.Item(Col1TotalMeasure, I).Value = Val(Dgl1.Item(Col1TotalDocMeasure, I).Value)
                    Else
                        Dgl1.Item(Col1Qty, I).Value = 0
                        Dgl1.Item(Col1TotalMeasure, I).Value = 0
                    End If

                    Dgl1.Item(Col1TotalDocMeasure, I).Value = Format(Val(Dgl1.Item(Col1DocQty, I).Value) * Val(Dgl1.Item(Col1MeasurePerPcs, I).Value), "0.".PadRight(CType(Dgl1.Columns(Col1TotalDocMeasure), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                    Dgl1.Item(Col1TotalMeasure, I).Value = Format(Val(Dgl1.Item(Col1Qty, I).Value) * Val(Dgl1.Item(Col1MeasurePerPcs, I).Value), "0.".PadRight(CType(Dgl1.Columns(Col1TotalMeasure), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))

                    If AgL.StrCmp(Dgl1.Item(Col1BillingType, I).Value, "Qty") Or Dgl1.Item(Col1BillingType, I).Value = "" Then
                        Dgl1.Item(Col1Amount, I).Value = Format(Val(Dgl1.Item(Col1DocQty, I).Value) * Val(Dgl1.Item(Col1Rate, I).Value), "0.".PadRight(CType(Dgl1.Columns(Col1Amount), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                    ElseIf AgL.StrCmp(Dgl1.Item(Col1BillingType, I).Value, "Measure") Then
                        Dgl1.Item(Col1Amount, I).Value = Format(Val(Dgl1.Item(Col1TotalDocMeasure, I).Value) * Val(Dgl1.Item(Col1Rate, I).Value), "0.".PadRight(CType(Dgl1.Columns(Col1Amount), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                    End If

                    'Footer Calculation
                    LblTotalQty.Text = Val(LblTotalQty.Text) + Val(Dgl1.Item(Col1DocQty, I).Value)
                    LblTotalAmount.Text = Val(LblTotalAmount.Text) + Val(Dgl1.Item(Col1Amount, I).Value)
                End If
            End If
        Next

        AgCalcGrid1.AgPostingGroupSalesTaxParty = TxtSalesTaxGroupParty.Tag
        AgCalcGrid1.AgPostingGroupSalesTaxItem = AgL.XNull(AgL.PubDtEnviro.Rows(0)("DefaultSalesTaxGroupItem"))
        AgCalcGrid1.Calculation()
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        Dim I As Integer = 0
        Dim StrMessage As String = ""
        passed = FCheckDuplicateRefNo()

        If AgL.RequiredField(TxtVendor, LblJobWorker.Text) Then passed = False : Exit Sub
        If AgCL.AgIsBlankGrid(Dgl1, Dgl1.Columns(Col1Item).Index) Then passed = False : Exit Sub

        With Dgl1
            For I = 0 To .Rows.Count - 1
                If .Item(Col1Item, I).Value <> "" Then
                    If Dgl1.Rows(I).Visible Then
                        If AgL.StrCmp(TxtV_Nature.Text, "Qty Amendment") Then

                            If RbtAddNewItem.Checked = True Then
                                If .Item(Col1PurchOrder, I).Value = "" Then
                                    MsgBox("Purch. Order Is Blank At Row No " & Dgl1.Item(ColSNo, I).Value & "")
                                    .CurrentCell = .Item(Col1PurchOrder, I) : Dgl1.Focus()
                                    passed = False : Exit Sub
                                End If
                            End If

                            If Val(.Item(Col1Qty, I).Value) = 0 Then
                                MsgBox("Qty Is 0 At Row No " & Dgl1.Item(ColSNo, I).Value & "")
                                .CurrentCell = .Item(Col1Qty, I) : Dgl1.Focus()
                                passed = False : Exit Sub
                            End If

                            If Val(.Item(Col1Qty, I).Value) < 0 Then
                                mQry = " SELECT sum(L.Qty) AS BalQty " &
                                        " FROM PurchOrder H " &
                                        " LEFT JOIN PurchOrderDetail L ON L.DocId = H.DocID  " &
                                        " WHERE L.PurchOrder = '" & Dgl1.Item(Col1PurchOrder, I).Tag & "' " &
                                        " AND L.PurchOrderSr = " & Dgl1.Item(Col1PurchOrderSr, I).Value & " " &
                                        " AND H.DocId <> '" & mSearchCode & "' " &
                                        " GROUP BY L.PurchOrder, L.PurchOrderSr "
                                If Math.Abs(Val(.Item(Col1Qty, I).Value)) > AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar) Then
                                    MsgBox("Cancel Qty Is Less Then Total Pending Qty For Purchase Order At Row No " & Dgl1.Item(ColSNo, I).Value & ".", MsgBoxStyle.Information, "Validation")
                                    .CurrentCell = .Item(Col1DocQty, I) : Dgl1.Focus()
                                    passed = False : Exit Sub
                                End If
                            End If
                        Else
                            If Val(.Item(Col1DocQty, I).Value) <= 0 Then
                                MsgBox("DocQty Is 0 At Row No " & Dgl1.Item(ColSNo, I).Value & "")
                                .CurrentCell = .Item(Col1DocQty, I) : Dgl1.Focus()
                                passed = False : Exit Sub
                            End If


                            If Val(.Item(Col1DocQty, I).Value) > 0 Then
                                mQry = " SELECT sum(L.Qty) AS BalQty " &
                                        " FROM PurchOrder H " &
                                        " LEFT JOIN PurchOrderDetail L ON L.DocId = H.DocID  " &
                                        " WHERE L.PurchOrder = '" & Dgl1.Item(Col1PurchOrder, I).Tag & "' " &
                                        " AND L.PurchOrderSr = " & Dgl1.Item(Col1PurchOrderSr, I).Value & " " &
                                        " AND H.DocId <> '" & mSearchCode & "' " &
                                        " GROUP BY L.PurchOrder, L.PurchOrderSr "
                                If Val(.Item(Col1DocQty, I).Value) > AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar) Then
                                    MsgBox("Amendment Qty Is Greater Than Total Pending Qty For Purchase Order At Row No " & Dgl1.Item(ColSNo, I).Value & ".", MsgBoxStyle.Information, "Validation")
                                    .CurrentCell = .Item(Col1DocQty, I) : Dgl1.Focus()
                                    passed = False : Exit Sub
                                End If
                            End If

                            If Val(.Item(Col1Rate, I).Value) = 0 Then
                                MsgBox("Rate Is 0 At Row No " & Dgl1.Item(ColSNo, I).Value & "")
                                .CurrentCell = .Item(Col1Rate, I) : Dgl1.Focus()
                                passed = False : Exit Sub
                            End If

                        End If
                    End If
                End If
            Next
        End With

        If StrMessage <> "" Then
            MsgBox(StrMessage)
            passed = False : Exit Sub
        End If
    End Sub

    Private Function FCheckDuplicateRefNo() As Boolean
        FCheckDuplicateRefNo = True
        If Topctrl1.Mode = "Add" Then
            mQry = " SELECT COUNT(*) FROM PurchOrder   " &
                    " WHERE ManualRefNo = '" & TxtManualRefNo.Text & "'   " &
                    " AND V_Type ='" & TxtV_Type.AgSelectedValue & "'  " &
                    " And Div_Code = '" & TxtDivision.AgSelectedValue & "' " &
                    " And Site_Code = '" & TxtSite_Code.AgSelectedValue & "'  " &
                    " And EntryStatus <> 'Discard' "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then TxtManualRefNo.Text = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "JobOrder", TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, AgTemplate.ClsMain.ManualRefType.Max) : MsgBox("Reference No. Already Exists New Reference No. Alloted : " & TxtManualRefNo.Text)
        Else
            mQry = " SELECT COUNT(*) FROM PurchOrder  " &
                    " WHERE ManualRefNo = '" & TxtManualRefNo.Text & "'   " &
                    " AND V_Type ='" & TxtV_Type.AgSelectedValue & "'  " &
                    " And Div_Code = '" & TxtDivision.AgSelectedValue & "' " &
                    " And Site_Code = '" & TxtSite_Code.AgSelectedValue & "' " &
                    " AND DocID <>'" & mSearchCode & "' " &
                    " And EntryStatus <> 'Discard' "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then FCheckDuplicateRefNo = False : MsgBox("Reference No. Already Exists") : TxtManualRefNo.Focus()
        End If
    End Function

    Private Sub FrmProductionOrder_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
        Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
        LblTotalQty.Text = 0 : LblTotalAmount.Text = 0
    End Sub

    Private Sub Txt_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtV_Type.Validating, TxtManualRefNo.Validating, TxtV_Date.Validating, TxtVendor.Validating, TxtV_Nature.Validating
        Dim DrTemp As DataRow() = Nothing
        Dim DtTemp As DataTable = Nothing
        Dim I As Integer = 0
        Try
            Select Case sender.name
                Case TxtV_Date.Name
                    If Topctrl1.Mode = "Add" Then
                        TxtManualRefNo.Text = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "PurchOrder", TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, AgTemplate.ClsMain.ManualRefType.Max)
                    End If

                Case TxtV_Type.Name
                    FFillV_TypeValues()

                Case TxtManualRefNo.Name
                    e.Cancel = Not FCheckDuplicateRefNo()

                Case TxtV_Nature.Name
                    Call Calculation()
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FFillV_TypeValues()
        Dim DtTemp As DataTable = Nothing
        TxtTermsAndConditions.Text = AgTemplate.ClsMain.FRetTermsCondition(TxtV_Type.AgSelectedValue)
        If Topctrl1.Mode = "Add" Then
            TxtManualRefNo.Text = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "PurchOrder", TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, AgTemplate.ClsMain.ManualRefType.Max)
        End If

        TxtStructure.Tag = AgStructure.ClsMain.FGetStructureFromNCat(LblV_Type.Tag, AgL.GcnRead)
        AgCalcGrid1.AgStructure = TxtStructure.Tag

        TxtCustomFields.Tag = AgCustomFields.ClsMain.FGetCustomFieldFromV_Type(TxtV_Type.Tag, AgL.GcnRead)
        AgCustomGrid1.AgCustom = TxtCustomFields.Tag

        IniGrid()
    End Sub

    Private Sub Validating_Item(ByVal Code As String, ByVal mRow As Integer, ByVal ColoumnName As String)
        Dim DrTemp As DataRow() = Nothing
        Dim DtTemp As DataTable = Nothing
        Dim sqlConn As SQLiteConnection = Nothing
        Dim sqlDA As SQLiteDataAdapter = Nothing

        sqlConn = New SQLiteConnection
        sqlConn.ConnectionString = AgL.Gcn_ConnectionString
        sqlConn.Open()

        Try
            If Dgl1.Item(ColoumnName, mRow).Value.ToString.Trim = "" Or Dgl1.AgSelectedValue(ColoumnName, mRow).ToString.Trim = "" Then
                Dgl1.Item(Col1DocQty, mRow).Value = 0
                Dgl1.Item(Col1Qty, mRow).Value = 0
                Dgl1.Item(Col1Unit, mRow).Value = ""
                Dgl1.Item(Col1MeasurePerPcs, mRow).Value = 0
                Dgl1.Item(Col1MeasureUnit, mRow).Value = ""
                Dgl1.Item(Col1PurchOrder, mRow).Tag = ""
                Dgl1.Item(Col1PurchOrder, mRow).Value = ""
                Dgl1.Item(Col1PurchOrderSr, mRow).Value = 0
                Dgl1.Item(Col1Item, mRow).Value = ""
                Dgl1.Item(Col1Item, mRow).Tag = ""
                Dgl1.Item(Col1ItemCode, mRow).Value = ""
                Dgl1.Item(Col1ItemCode, mRow).Tag = ""
            Else
                If Dgl1.AgDataRow IsNot Nothing Then
                    Dgl1.Item(Col1QtyDecimalPlaces, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("QtyDecimalPlaces").Value)
                    Dgl1.Item(Col1Qty, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("Qty").Value)
                    Dgl1.Item(Col1DocQty, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("Qty").Value)
                    Dgl1.Item(Col1Unit, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("Unit").Value)
                    Dgl1.Item(Col1MeasurePerPcs, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("MeasurePerPcs").Value)
                    Dgl1.Item(Col1MeasureUnit, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("MeasureUnit").Value)
                    Dgl1.Item(Col1MeasureDecimalPlaces, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("MeasureDecimalPlaces").Value)
                    Dgl1.Item(Col1PurchOrder, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("PurchOrder").Value)
                    Dgl1.Item(Col1PurchOrder, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("PurchOrderNo").Value)
                    Dgl1.Item(Col1PurchOrderSr, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("PurchOrderSr").Value)
                    Dgl1.Item(Col1Item, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("ItemDesc").Value)
                    Dgl1.Item(Col1Item, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("Code").Value)
                    Dgl1.Item(Col1ItemCode, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("ItemCode").Value)
                    Dgl1.Item(Col1ItemCode, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("Code").Value)
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " On Validating_Item Function ")
        Finally
            If sqlConn IsNot Nothing Then sqlConn.Dispose()
            If sqlDA IsNot Nothing Then sqlDA.Dispose()
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
                Case Col1Item
                    Validating_Item(Dgl1.AgSelectedValue(Col1Item, mRowIndex), mRowIndex, Col1Item)

                Case Col1ItemCode
                    Validating_Item(Dgl1.AgSelectedValue(Col1ItemCode, mRowIndex), mRowIndex, Col1ItemCode)
            End Select
            Call Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FPostInStockVertual(ByVal SearchCode As String, ByVal Conn As SQLiteConnection, ByVal Cmd As SQLiteCommand)
        mQry = "Delete From StockVirtual Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "INSERT INTO StockVirtual( DocID, Sr, V_Type, V_Prefix, V_Date, V_No, RecID, Div_Code, Site_Code, " &
                 " SubCode, Item, Qty_Rec, Qty_Iss, Unit, MeasurePerPcs, Measure_Rec, Measure_Iss, MeasureUnit, " &
                 " Remarks ) " &
                 " Select L.DocID, L.Sr, H.V_Type, " &
                 " H.V_Prefix, H.V_Date, H.V_No, H.ManualRefNo, H.Div_Code, H.Site_Code,   " &
                 " H.Vendor, L.Item, " &
                 " L.Qty , 0, L.Unit, L.MeasurePerPcs, " &
                 " L.TotalMeasure, 0, " &
                 " L.MeasureUnit, L.Remark " &
                 " From (Select * From PurchOrder Where DocId = '" & mSearchCode & "') H   " &
                 " LEFT JOIN PurchOrderDetail L On H.DocId = L.DocId   "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    End Sub

    Private Sub TempJobOrder_BaseEvent_Topctrl_tbAdd() Handles Me.BaseEvent_Topctrl_tbAdd
        TxtManualRefNo.Text = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "PurchOrder", TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, AgTemplate.ClsMain.ManualRefType.Max)
        TxtTermsAndConditions.Text = AgTemplate.ClsMain.FRetTermsCondition(TxtV_Type.AgSelectedValue)
        RbtPlanForPurchOrder.Checked = True
        FFillV_TypeValues()
    End Sub

    Private Sub FCheckDuplicate(ByVal mRow As Integer)
        Dim I As Integer = 0
        Try
            With Dgl1
                For I = 0 To .Rows.Count - 1
                    If .Item(Col1Item, I).Value <> "" Then
                        If mRow <> I Then
                            If AgL.StrCmp(.Item(Col1Item, I).Value, .Item(Col1Item, mRow).Value) Then
                                MsgBox("Item " & .Item(Col1Item, I).Value & " Is Already Feeded At Row No " & .Item(ColSNo, I).Value & ".", MsgBoxStyle.Information)
                                .CurrentCell = .Item(Col1Item, I) : Dgl1.Focus()
                                .Rows.Remove(.Rows(mRow)) : Exit Sub
                            End If
                        End If
                    End If
                Next
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub TempJobOrder_BaseEvent_Topctrl_tbRef() Handles Me.BaseEvent_Topctrl_tbRef
        Try
            If Dgl1.AgHelpDataSet(Col1Item) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1Item).Dispose() : Dgl1.AgHelpDataSet(Col1Item) = Nothing
            If TxtVendor.AgHelpDataSet IsNot Nothing Then TxtVendor.AgHelpDataSet.Dispose() : TxtVendor.AgHelpDataSet = Nothing
        Catch ex As Exception
        End Try
    End Sub

    Private Sub TxtOrderBy_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtBillingType.KeyDown, TxtVendor.KeyDown, TxtV_Nature.KeyDown
        Try
            Select Case sender.name
                Case TxtBillingType.Name
                    If e.KeyCode <> Keys.Enter Then
                        If sender.AgHelpDataSet Is Nothing Then
                            sender.AgHelpDataSet(0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(AgTemplate.ClsMain.HelpQueries.BillingType, AgL.GCn)
                        End If
                    End If

                Case TxtV_Nature.Name
                    If e.KeyCode <> Keys.Enter Then
                        If sender.AgHelpDataSet Is Nothing Then
                            mQry = " SELECT 'Qty Amendment' AS Code, 'Qty Amendment' AS Nature " &
                                    " UNION ALL " &
                                    " SELECT 'Rate Amendment' AS Code, 'Rate Amendment' AS Nature "
                            sender.AgHelpDataSet(0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If

                Case TxtVendor.Name
                    If TxtVendor.AgHelpDataSet Is Nothing Then
                        FCreateHelpSubgroup(sender)
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

        mQry = " SELECT H.SubCode, H.DispName || ',' || IfNull(C.CityName,'') AS [Party], " &
                " H.Currency, C1.Description As CurrencyDesc, H.Nature, H.SalesTaxPostingGroup " &
                " FROM SubGroup H  " &
                " LEFT JOIN City C ON H.CityCode = C.CityCode  " &
                " LEFT JOIN Currency C1 On H.Currency = C1.Code " &
                " Where IfNull(H.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
        sender.AgHelpDataSet(4, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
    End Sub

    Private Sub Dgl1_EditingControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.EditingControl_KeyDown
        Try
            If AgL.StrCmp(Topctrl1.Mode, "Browse") Then Exit Sub
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Item
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1Item) Is Nothing Then
                            FCreateHelpItem()
                        End If
                    End If

                Case Col1ItemCode
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1ItemCode) Is Nothing Then
                            FCreateHelpItemCode()
                        End If
                    End If

                Case Col1PurchOrder
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1PurchOrder) Is Nothing Then
                            mQry = " SELECT H.DocID, H.V_Type || '-' || H.ReferenceNo AS PurchOrderNo , H.V_Date AS PurchOrderDate " &
                                    " FROM PurchOrder H " &
                                    " Where 1=1 AND H.Vendor = '" & TxtVendor.Tag & "' "
                            Dgl1.AgHelpDataSet(Col1PurchOrder) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FCreateHelpItem()
        Dim strCond As String = ""

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
                strCond += " And CharIndex('|' || I.Code || '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_Item")) & "') <= 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemDivision")) <> "" Then
                strCond += " And CharIndex('|' || I.Div_Code || '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemDivision")) & "') > 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemSite")) <> "" Then
                strCond += " And CharIndex('|' || I.Site_Code || '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemSite")) & "') > 0 "
            End If
        End If

        If RbtAddNewItem.Checked = True Then
            mQry = " SELECT I.Code, I.Description AS ItemDesc, I.ManualCode AS ItemCode, '' AS PurchOrderNo,   '' As PurchOrderDate, '' AS BillingType , " &
                    " '' AS RateType , '' AS PurchOrder, 0 AS PurchOrderSr,   1 As Qty,  0 AS FreeQty,    I.Unit , 0 As MeasurePerPcs,  I.MeasureUnit As MeasureUnit,    " &
                    " U.DecimalPlaces AS QtyDecimalPlaces, MU.DecimalPlaces AS MeasureDecimalPlaces , '' AS MRP,   '' AS Deal, 0 AS Rate ,  0 AS DeliveryMeasureMultiplier, 0 AS TotalDeliveryMeasure, 0 AS DeliveryMeasure  ,   0 AS DeliveryMeasurePerPcs   " &
                    " FROM Item I " &
                    " Left Join Unit U On I.Unit = U.Code     " &
                    " Left Join Unit MU On I.MeasureUnit = MU.Code  " &
                    " WHERE IfNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
            Dgl1.AgHelpDataSet(Col1Item, 18) = AgL.FillData(mQry, AgL.GCn)

            Dgl1.Columns(Col1PurchOrder).ReadOnly = False
        Else
            mQry = "  SELECT Max(L.Item) As Item, Max(I.Description) AS ItemDesc, max(I.ManualCode) AS ItemCode, Max(H.V_Type) || '-' ||  Max(H.ManualRefNo) AS PurchOrderNo,  " &
                    " Max(H.V_Date) As PurchOrderDate, max(L.BillingType) AS BillingType , max(L.RateType) AS RateType ,  L.PurchOrder, L.PurchOrderSr,  " &
                    " IfNull(Sum(L.Qty),0) As Qty,  IfNull(sum(L.freeQty),0) AS FreeQty,   " &
                    " Max(L.Unit) As Unit, Max(L.MeasurePerPcs) As MeasurePerPcs,  Max(L.MeasureUnit) As MeasureUnit,   Max(U.DecimalPlaces) AS QtyDecimalPlaces, Max(MU.DecimalPlaces) AS MeasureDecimalPlaces , max(L.MRP) AS MRP,  " &
                    " Max(L.Deal) AS Deal, max(L.Rate) AS Rate ,  max(L.DeliveryMeasureMultiplier) AS DeliveryMeasureMultiplier, max(TotalDeliveryMeasure) AS TotalDeliveryMeasure, max(DeliveryMeasure) AS DeliveryMeasure  ,  " &
                    " max(DeliveryMeasurePerPcs) AS DeliveryMeasurePerPcs  FROM (  " &
                    " SELECT DocID, V_Type, ManualRefNo , V_Date  " &
                    " FROM PurchOrder   " &
                    " WHERE Vendor ='" & TxtVendor.Tag & "'   " &
                    " And Div_Code = '" & TxtDivision.Tag & "'   " &
                    " AND Site_Code = '" & TxtSite_Code.Tag & "'   " &
                    " AND V_Date <= '" & TxtV_Date.Text & "'   " &
                    "  ) As H   LEFT JOIN PurchOrderDetail L  ON H.DocID = L.PurchOrder  " &
                    "  LEFT JOIN Item I On L.Item = I.Code    Left Join Unit U On L.Unit = U.Code  " &
                    "  Left Join Unit MU On L.MeasureUnit = MU.Code  " &
                    " WHERE L.DocId <> '" & mSearchCode & "'" & strCond &
                    "  GROUP BY L.PurchOrder, L.PurchOrderSr  " &
                    "  HAVING IfNull(Sum(L.Qty),0) > 0 "
            Dgl1.AgHelpDataSet(Col1Item, 18) = AgL.FillData(mQry, AgL.GCn)
        End If

    End Sub

    Private Sub FCreateHelpItemCode()
        Dim strCond As String = ""

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
                strCond += " And CharIndex('|' || I.Code || '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_Item")) & "') <= 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemDivision")) <> "" Then
                strCond += " And CharIndex('|' || I.Div_Code || '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemDivision")) & "') > 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemSite")) <> "" Then
                strCond += " And CharIndex('|' || I.Site_Code || '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemSite")) & "') > 0 "
            End If
        End If

        If RbtAddNewItem.Checked = True Then
            mQry = " SELECT I.Code, I.ManualCode AS ItemCode, I.Description AS ItemDesc,  '' AS PurchOrderNo,   '' As PurchOrderDate, '' AS BillingType , " &
                    " '' AS RateType , '' AS PurchOrder, 0 AS PurchOrderSr,   1 As Qty,  0 AS FreeQty,    I.Unit , 0 As MeasurePerPcs,  I.MeasureUnit As MeasureUnit,    " &
                    " U.DecimalPlaces AS QtyDecimalPlaces, MU.DecimalPlaces AS MeasureDecimalPlaces , '' AS MRP,   '' AS Deal, 0 AS Rate ,  0 AS DeliveryMeasureMultiplier, 0 AS TotalDeliveryMeasure, 0 AS DeliveryMeasure  ,   0 AS DeliveryMeasurePerPcs   " &
                    " FROM Item I " &
                    " Left Join Unit U On I.Unit = U.Code     " &
                    " Left Join Unit MU On I.MeasureUnit = MU.Code  " &
                    " WHERE IfNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
            Dgl1.AgHelpDataSet(Col1ItemCode, 18) = AgL.FillData(mQry, AgL.GCn)

            Dgl1.Columns(Col1PurchOrder).ReadOnly = False

        Else

            mQry = "  SELECT Max(L.Item) As Item,  max(I.ManualCode) AS ItemCode, Max(I.Description) AS ItemDesc, Max(H.V_Type) || '-' ||  Max(H.ManualRefNo) AS PurchOrderNo,  " &
                    " Max(H.V_Date) As PurchOrderDate, max(L.BillingType) AS BillingType , max(L.RateType) AS RateType ,  L.PurchOrder, L.PurchOrderSr,  " &
                    " IfNull(Sum(L.Qty),0) As Qty,  IfNull(sum(L.freeQty),0) AS FreeQty,   " &
                    " Max(L.Unit) As Unit, Max(L.MeasurePerPcs) As MeasurePerPcs,  Max(L.MeasureUnit) As MeasureUnit,   Max(U.DecimalPlaces) AS QtyDecimalPlaces, Max(MU.DecimalPlaces) AS MeasureDecimalPlaces , max(L.MRP) AS MRP,  " &
                    " Max(L.Deal) AS Deal, max(L.Rate) AS Rate ,  max(L.DeliveryMeasureMultiplier) AS DeliveryMeasureMultiplier, max(TotalDeliveryMeasure) AS TotalDeliveryMeasure, max(DeliveryMeasure) AS DeliveryMeasure  ,  " &
                    " max(DeliveryMeasurePerPcs) AS DeliveryMeasurePerPcs  FROM (  " &
                    " SELECT DocID, V_Type, ManualRefNo , V_Date  " &
                    " FROM PurchOrder   " &
                    " WHERE Vendor ='" & TxtVendor.Tag & "'   " &
                    " And Div_Code = '" & TxtDivision.Tag & "'   " &
                    " AND Site_Code = '" & TxtSite_Code.Tag & "'   " &
                    " AND V_Date <= '" & TxtV_Date.Text & "'   " &
                    "  ) As H   LEFT JOIN PurchOrderDetail L  ON H.DocID = L.PurchOrder  " &
                    "  LEFT JOIN Item I On L.Item = I.Code    Left Join Unit U On L.Unit = U.Code  " &
                    "  Left Join Unit MU On L.MeasureUnit = MU.Code  " &
                    " WHERE L.DocId <> '" & mSearchCode & "'" & strCond &
                    " GROUP BY L.PurchOrder, L.PurchOrderSr  " &
                    " HAVING IfNull(Sum(L.Qty),0) > 0 "
            Dgl1.AgHelpDataSet(Col1ItemCode, 18) = AgL.FillData(mQry, AgL.GCn)
        End If
    End Sub

    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub

    Private Sub RbtAllItems_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dgl1.AgHelpDataSet(Col1Item) = Nothing
    End Sub

    Private Sub TxtProcess_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Dgl1.AgHelpDataSet(Col1Item) = Nothing
    End Sub

    Private Sub Dgl1_CellContentClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellContentClick
        Dim Mdi As MDIMain = New MDIMain
        Try
            Select Case Dgl1.Columns(e.ColumnIndex).Name
                Case Col1PurchOrder
                    'Call ClsMain.ProcOpenLinkForm(Mdi.MnuFinishingOrderEntry, Dgl1.Item(Col1PurchOrder, e.RowIndex).Tag, Me.MdiParent)

            End Select
        Catch ex As Exception
        End Try
    End Sub

    Private Sub FrmFinishingOrder_BaseEvent_Topctrl_tbPrn(ByVal SearchCode As String) Handles Me.BaseEvent_Topctrl_tbPrn
        'Dim mCrd As New ReportDocument
        'Dim ReportView As New AgLibrary.RepView
        'Dim DsRep As New DataSet
        'Dim RepName As String = "", RepTitle As String = ""

        'Try
        '    Me.Cursor = Cursors.WaitCursor

        '    RepName = "POAmendment_Print" : RepTitle = "Purchase Order Amendment"
        '    AgL.PubReportTitle = "Purchase Order Amendment"

        '    mQry = " SELECT H.DocID, H.V_Type, H.V_Prefix, H.V_Date, H.V_No, H.ReferenceNo, H.Vendor, " & _
        '            " H.VendorName, H.TermsAndConditions, H.Remarks, H.ManualRefNo, L.Sr, L.Item,L.SalesTaxGroupItem,  " & _
        '            " Case WHEN IfNull(L.Qty,0) = 0 THEN L.DocQty ELSE L.Qty END AS Qty, L.Unit, L.MeasurePerPcs, L.MeasureUnit, L.TotalMeasure, L.Rate, L.Amount, L.Specification,  " & _
        '            " L.Remark AS LineRemark, L.PurchOrder, L.PurchOrderSr, L.BillingType, L.DocQty, L.DocMeasure, L.V_Nature, L.RateAffected, " & _
        '            " I.Description AS ItemDesc, I.ManualCode AS ItemCode, PO.ReferenceNo AS PurchOrderNo, " & _
        '            " " & AgCalcGrid1.FLineTableFieldNameStr("L.", "L_") & "  " & _
        '            " FROM PurchOrder H " & _
        '            " LEFT JOIN PurchOrderDetail L ON L.DocId = H.DocID  " & _
        '            " LEFT JOIN Item I ON I.Code = L.Item  " & _
        '            " LEFT JOIN PurchOrder PO ON PO.DocID = L.PurchOrder " & _
        '            " WHERE H.DocID =  '" & mSearchCode & "' "

        '    AgL.ADMain = New SqliteDataAdapter(mQry, AgL.GCn)
        '    AgL.ADMain.Fill(DsRep)

        '    AgPL.CreateFieldDefFile1(DsRep, AgL.PubReportPath & "\" & RepName & ".ttx", True)
        '    mCrd.Load(AgL.PubReportPath & "\" & RepName & ".rpt")
        '    mCrd.SetDataSource(DsRep.Tables(0))
        '    CType(ReportView.Controls("CrvReport"), CrystalDecisions.Windows.Forms.CrystalReportViewer).ReportSource = mCrd
        '    AgPL.Formula_Set(mCrd, RepTitle)

        '    AgPL.Show_Report(ReportView, "* " & RepTitle & " *", Me.MdiParent)

        '    Call AgL.LogTableEntry(mSearchCode, Me.Text, "P", AgL.PubMachineName, AgL.PubUserName, AgL.PubLoginDate, AgL.GCn, AgL.ECmd)
        '    DsRep.Dispose()

        'Catch Ex As Exception
        '    MsgBox(Ex.Message)
        'Finally
        '    Me.Cursor = Cursors.Default
        '    DsRep.Dispose()
        'End Try
        ClsMain.FPrintThisDocument(Me, TxtV_Type.Tag)
    End Sub

    Private Sub BtnFill_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnFillPurchOrder.Click
        Try
            If Topctrl1.Mode = "Browse" Then Exit Sub
            Dim StrTicked As String = ""

            If MsgBox("Do You Want To Fill Only Balance Qty ?", MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.Yes Then
                FillForBalanceQty = True
            Else
                FillForBalanceQty = False
            End If

            If RbtForPurchOrderItems.Checked Then
                StrTicked = FHPGD_PendingPurchOrderItems()
            Else
                StrTicked = FHPGD_PendingPurchOrder()
            End If

            If StrTicked <> "" Then
                FFillItemsForPendingPurchOrders(StrTicked)
            Else
                Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
            End If

            Dgl1.Focus()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function FHPGD_PendingPurchOrder() As String
        Dim FRH_Multiple As DMHelpGrid.FrmHelpGrid_Multi
        Dim StrRtn As String = ""

        mQry = " SELECT 'o' As Tick, VMain.PurchOrder, Max(VMain.PurchOrderNo) AS PurchOrderNo,  " &
                " Max(VMain.PurchOrderDate) AS PurchOrderDate   " &
                " FROM ( " & FRetFillItemWiseQry("And Vendor = '" & TxtVendor.Tag & "' And Div_Code = '" & TxtDivision.Tag & "' And Site_Code = '" & TxtSite_Code.Tag & "' And V_Date <= '" & TxtV_Date.Text & "'", "") & " ) As VMain " &
                " GROUP BY VMain.PurchOrder " &
                " Order By PurchOrderDate "

        FRH_Multiple = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(AgL.FillData(mQry, AgL.GCn).TABLES(0)), "", 500, 500, , , False)
        FRH_Multiple.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple.FFormatColumn(1, , 0, , False)
        FRH_Multiple.FFormatColumn(2, "Order No.", 150, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(3, "Order Date", 100, DataGridViewContentAlignment.MiddleLeft)

        FRH_Multiple.StartPosition = FormStartPosition.CenterScreen
        FRH_Multiple.ShowDialog()

        If FRH_Multiple.BytBtnValue = 0 Then
            StrRtn = FRH_Multiple.FFetchData(1, "'", "'", ",", True)
        End If
        FHPGD_PendingPurchOrder = StrRtn

        FRH_Multiple = Nothing
    End Function

    Private Function FHPGD_PendingPurchOrderItems() As String
        Dim FRH_Multiple As DMHelpGrid.FrmHelpGrid_Multi
        Dim StrRtn As String = ""

        mQry = " SELECT 'o' As Tick, VMain.PurchOrder + Convert(nVarChar, VMain.PurchOrderSr) As PurchOrderDocIdSr, " &
                " Max(VMain.PurchOrderNo) AS PurchOrderNo,  " &
                " Max(VMain.PurchOrderDate) AS PurchOrderDate, Max(VMain.ItemDesc) As ItemDesc " &
                " FROM ( " & FRetFillItemWiseQry("And Vendor = '" & TxtVendor.Tag & "' And Div_Code = '" & TxtDivision.Tag & "' And Site_Code = '" & TxtSite_Code.Tag & "' And V_Date <= '" & TxtV_Date.Text & "'", "") & " ) As VMain " &
                " GROUP BY VMain.PurchOrder, VMain.PurchOrderSr " &
                " Order By PurchOrderDate "
        FRH_Multiple = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(AgL.FillData(mQry, AgL.GCn).TABLES(0)), "", 500, 650, , , False)
        FRH_Multiple.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple.FFormatColumn(1, , 0, , False)
        FRH_Multiple.FFormatColumn(2, "Order No.", 180, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(3, "Order Date", 180, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(4, "Item", 150, DataGridViewContentAlignment.MiddleLeft)

        FRH_Multiple.StartPosition = FormStartPosition.CenterScreen
        FRH_Multiple.ShowDialog()

        If FRH_Multiple.BytBtnValue = 0 Then
            StrRtn = FRH_Multiple.FFetchData(1, "'", "'", ",", True)
        End If
        FHPGD_PendingPurchOrderItems = StrRtn

        FRH_Multiple = Nothing
    End Function

    Private Sub FFillItemsForPendingPurchOrders(ByVal bOrderNoStr As String)
        Dim I As Integer = 0
        Dim DtTemp As DataTable = Nothing
        Try
            If bOrderNoStr = "" Then Exit Sub

            If RbtForPurchOrderItems.Checked Then
                mQry = FRetFillItemWiseQry("", " And L.DocId + Convert(nVarChar, L.Sr) In (" & bOrderNoStr & ")")
            Else
                mQry = FRetFillItemWiseQry(" And DocId In (" & bOrderNoStr & ") ", "")
            End If
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
                        Dgl1.Item(ColSNo, J).Value = I + 1
                        Dgl1.Item(Col1Item, J).Tag = AgL.XNull(.Rows(I)("Item"))
                        Dgl1.Item(Col1Item, J).Value = AgL.XNull(.Rows(I)("ItemDesc"))
                        Dgl1.Item(Col1ItemCode, J).Tag = AgL.XNull(.Rows(I)("Item"))
                        Dgl1.Item(Col1ItemCode, J).Value = AgL.XNull(.Rows(I)("ItemCode"))
                        Dgl1.Item(Col1PurchOrder, J).Tag = AgL.XNull(.Rows(I)("PurchOrder"))
                        Dgl1.Item(Col1PurchOrder, J).Value = AgL.XNull(.Rows(I)("PurchOrderNo"))
                        Dgl1.Item(Col1SalesTaxGroup, J).Tag = AgL.XNull(.Rows(I)("SalesTaxGroupItem"))
                        Dgl1.Item(Col1SalesTaxGroup, J).Value = AgL.XNull(.Rows(I)("SalesTaxGroupItem"))
                        Dgl1.Item(Col1PurchOrderSr, J).Value = AgL.VNull(.Rows(I)("purchOrderSr"))
                        Dgl1.Item(Col1BillingType, J).Value = AgL.XNull(.Rows(I)("BillingType"))
                        Dgl1.Item(Col1RateType, J).Value = AgL.XNull(.Rows(I)("RateType"))

                        Dgl1.Item(Col1Qty, J).Value = Format(AgL.VNull(.Rows(I)("Qty")), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))

                        If FillForBalanceQty Then
                            Dgl1.Item(Col1DocQty, J).Value = -AgL.VNull(.Rows(I)("Qty"))
                        End If

                        Dgl1.Item(Col1FreeQty, J).Value = AgL.VNull(.Rows(I)("FreeQty"))
                        Dgl1.Item(Col1Unit, J).Value = AgL.XNull(.Rows(I)("Unit"))
                        Dgl1.Item(Col1QtyDecimalPlaces, J).Value = AgL.VNull(.Rows(I)("QtyDecimalPlaces"))
                        Dgl1.Item(Col1MeasurePerPcs, J).Value = Format(AgL.VNull(.Rows(I)("MeasurePerPcs")), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                        Dgl1.Item(Col1MeasureUnit, J).Value = AgL.XNull(.Rows(I)("MeasureUnit"))
                        Dgl1.Item(Col1MeasureDecimalPlaces, J).Value = AgL.VNull(.Rows(I)("MeasureDecimalPlaces"))

                        Dgl1.Item(Col1DeliveryMeasurePerPcs, J).Value = AgL.VNull(.Rows(I)("DeliveryMeasurePerPcs"))
                        Dgl1.Item(Col1DeliveryMeasure, J).Value = AgL.XNull(.Rows(I)("DeliveryMeasure"))
                        Dgl1.Item(Col1DeliveryMeasureMultiplier, J).Value = AgL.VNull(.Rows(I)("DeliveryMeasureMultiplier"))

                        Dgl1.Item(Col1Deal, J).Value = AgL.XNull(.Rows(I)("Deal"))
                        Dgl1.Item(Col1Specification, J).Value = AgL.XNull(.Rows(I)("Specification"))
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
        If DtV_TypeSettings.Rows.Count > 0 Then
            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")) <> "" Then
                LineConStr += " And CharIndex('|' || I.ItemType || '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")) & "') > 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemGroup")) <> "" Then
                LineConStr += " And CharIndex('|' || I.ItemGroup || '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemGroup")) & "') > 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_ItemGroup")) <> "" Then
                LineConStr += " And CharIndex('|' || I.ItemGroup || '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_ItemGroup")) & "') <= 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Item")) <> "" Then
                LineConStr += " And CharIndex('|' || I.Code || '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Item")) & "') > 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_Item")) <> "" Then
                LineConStr += " And CharIndex('|' || I.Code || '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_Item")) & "') <= 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemDivision")) <> "" Then
                LineConStr += " And CharIndex('|' || I.Div_Code || '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemDivision")) & "') > 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemSite")) <> "" Then
                LineConStr += " And CharIndex('|' || I.Site_Code || '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemSite")) & "') > 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ContraV_Type")) <> "" Then
                HeaderConStr += " And CharIndex('|' || V_Type || '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ContraV_Type")) & "') > 0 "
            End If
        End If

        FRetFillItemWiseQry = "  SELECT Max(H.V_Type) || '-' ||  Max(H.ReferenceNo) AS PurchOrderNo,  Max(H.V_Date) As PurchOrderDate, max(L.BillingType) AS BillingType , max(L.RateType) AS RateType , " &
                            " L.PurchOrder, L.PurchOrderSr,    Max(L.Item) As Item, Max(I.Description) AS ItemDesc, max(I.ManualCode) AS ItemCode,  IfNull(Sum(L.Qty),0) As Qty,  IfNull(sum(L.freeQty),0) AS FreeQty,   " &
                            " Max(L.Unit) As Unit, Max(L.MeasurePerPcs) As MeasurePerPcs,  Max(L.MeasureUnit) As MeasureUnit, Max(L.SalesTaxGroupItem) AS SalesTaxGroupItem, Max(L.Specification) AS Specification, " &
                            " Max(U.DecimalPlaces) AS QtyDecimalPlaces, Max(MU.DecimalPlaces) AS MeasureDecimalPlaces , max(L.MRP) AS MRP, Max(L.Deal) AS Deal, max(L.Rate) AS Rate , " &
                            " max(L.DeliveryMeasureMultiplier) AS DeliveryMeasureMultiplier, max(TotalDeliveryMeasure) AS TotalDeliveryMeasure, max(DeliveryMeasure) AS DeliveryMeasure  , max(DeliveryMeasurePerPcs) AS DeliveryMeasurePerPcs " &
                            " FROM (  " &
                            " SELECT DocID, V_Type, ReferenceNo , V_Date  " &
                            " FROM PurchOrder  Where 1=1 " & HeaderConStr & " " &
                            " ) As H  " &
                            " LEFT JOIN PurchOrderDetail L  ON H.DocID = L.PurchOrder  " &
                            " LEFT JOIN Item I On L.Item = I.Code   " &
                            " Left Join Unit U On L.Unit = U.Code   " &
                            " Left Join Unit MU On L.MeasureUnit = MU.Code   " &
                            " WHERE 1 = 1 " & LineConStr &
                            " GROUP BY L.PurchOrder, L.PurchOrderSr   "

        If FillForBalanceQty Then FRetFillItemWiseQry += " HAVING IfNull(Sum(L.Qty),0) > 0 "
    End Function
End Class
