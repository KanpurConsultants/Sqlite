Imports System.IO
Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data.SQLite
Public Class FrmWorkOrderAmendment
    Inherits AgTemplate.TempTransaction
    Public mQry$

    Public WithEvents AgCalcGrid1 As New AgStructure.AgCalcGrid
    Public WithEvents AgCustomGrid1 As New AgCustomFields.AgCustomGrid

    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Protected Const ColSNo As String = "S.No."
    Protected Const Col1ItemCode As String = "Item Code"
    Protected Const Col1Item As String = "Item"
    Protected Const Col1WorkOrder As String = "Work Order"
    Protected Const Col1WorkOrderSr As String = "Work Order Sr"
    Protected Const Col1BillingType As String = "Billing Type"
    Protected Const Col1RateType As String = "Rate Type"
    Protected Const Col1RatePerQty As String = "Rate Per Qty"
    Protected Const Col1Qty As String = "Qty"
    Protected Const Col1DocQty As String = "Doc Qty"
    Protected Const Col1Unit As String = "Unit"
    Protected Const Col1SalesTaxGroup As String = "Sales Tax Group"
    Protected Const Col1QtyDecimalPlaces As String = "Qty Decimal Places"
    Protected Const Col1MeasurePerPcs As String = "Measure Per Qty"
    Protected Const Col1TotalDocMeasure As String = "Total Doc Measure"
    Protected Const Col1TotalMeasure As String = "Total Measure"
    Protected Const Col1MeasureUnit As String = "Measure Unit"
    Protected Const Col1MeasureDecimalPlaces As String = "Measure Decimal Places"
    Protected Const Col1DeliveryMeasure As String = "Delivery Measure"
    Protected Const Col1DeliveryMeasureMultiplier As String = "Delivery Measure Multiplier"
    Protected Const Col1DeliveryMeasurePerPcs As String = "Delivery Measure Per Qty"
    Protected Const Col1TotalDeliveryMeasure As String = "Total Delivery Measure"
    Protected Const Col1TotalDocDeliveryMeasure As String = "Total Doc Delivery Measure"
    Protected Const Col1DeliveryMeasureDecimalPlaces As String = "Delivery Measure Decimal Places"
    Protected Const Col1RateOrder As String = "Rate Order"
    Protected Const Col1RateAmendment As String = "Rate Amendment"
    Protected Const Col1Rate As String = "Rate"
    Protected Const Col1Amount As String = "Amount"
    Protected Const Col1Specification As String = "Specification"

    Protected WithEvents TxtStructure As AgControls.AgTextBox
    Protected WithEvents TxtCustomFields As AgControls.AgTextBox
    Protected WithEvents PnlCustomGrid As System.Windows.Forms.Panel
    Protected WithEvents PnlCalcGrid As System.Windows.Forms.Panel
    Protected WithEvents TxtSalesTaxGroupParty As AgControls.AgTextBox
    Protected WithEvents RbtAddNewItem As System.Windows.Forms.RadioButton
    Protected WithEvents LblTotalDeliveryMeasure As System.Windows.Forms.Label
    Protected WithEvents LblTotalDeliveryMeasureText As System.Windows.Forms.Label

    Dim BlnIsMeasurePerPcsVisible As Boolean = False
    Dim BlnIsMeasurePerPcsEditable As Boolean = False
    Dim BlnIsMeasureVisible As Boolean = False
    Dim BlnIsMeasureEditable As Boolean = False
    Dim BlnIsMeasureUnitVisible As Boolean = False
    Dim FillForBalanceQty As Boolean = True
    Dim BlnIsMeasureUnitEditable As Boolean = False

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
        Me.TxtParty = New AgControls.AgTextBox
        Me.LblJobWorker = New System.Windows.Forms.Label
        Me.TxtTermsAndConditions = New AgControls.AgTextBox
        Me.LinkLabel2 = New System.Windows.Forms.LinkLabel
        Me.Label32 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.TxtV_Nature = New AgControls.AgTextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.GrpDirectChallan = New System.Windows.Forms.GroupBox
        Me.RbtAddNewItem = New System.Windows.Forms.RadioButton
        Me.RbtForWorkOrder = New System.Windows.Forms.RadioButton
        Me.RbtForWorkOrderItems = New System.Windows.Forms.RadioButton
        Me.BtnFillWorkOrder = New System.Windows.Forms.Button
        Me.TxtStructure = New AgControls.AgTextBox
        Me.TxtCustomFields = New AgControls.AgTextBox
        Me.PnlCustomGrid = New System.Windows.Forms.Panel
        Me.PnlCalcGrid = New System.Windows.Forms.Panel
        Me.TxtSalesTaxGroupParty = New AgControls.AgTextBox
        Me.LblTotalDeliveryMeasure = New System.Windows.Forms.Label
        Me.LblTotalDeliveryMeasureText = New System.Windows.Forms.Label
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
        Me.TP1.Controls.Add(Me.LblManualRefNo)
        Me.TP1.Controls.Add(Me.TxtRemarks)
        Me.TP1.Controls.Add(Me.Label30)
        Me.TP1.Controls.Add(Me.TxtParty)
        Me.TP1.Controls.Add(Me.LblJobWorker)
        Me.TP1.Controls.Add(Me.LblJobWorkerReq)
        Me.TP1.Location = New System.Drawing.Point(4, 22)
        Me.TP1.Size = New System.Drawing.Size(983, 98)
        Me.TP1.Text = "Document Detail"
        Me.TP1.Controls.SetChildIndex(Me.LblJobWorkerReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblJobWorker, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtParty, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label30, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtRemarks, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblManualRefNo, 0)
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
        Me.Panel1.Controls.Add(Me.LblTotalDeliveryMeasure)
        Me.Panel1.Controls.Add(Me.LblTotalAmount)
        Me.Panel1.Controls.Add(Me.LblTotalDeliveryMeasureText)
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
        Me.LblTotalQty.Location = New System.Drawing.Point(219, 2)
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
        Me.LblTotalQtyText.Location = New System.Drawing.Point(134, 2)
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
        Me.LinkLabel1.Text = "Work Order Amendment For Following Items"
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
        Me.TxtParty.Location = New System.Drawing.Point(125, 52)
        Me.TxtParty.MaxLength = 20
        Me.TxtParty.Name = "TxtParty"
        Me.TxtParty.Size = New System.Drawing.Size(354, 18)
        Me.TxtParty.TabIndex = 4
        '
        'LblJobWorker
        '
        Me.LblJobWorker.AutoSize = True
        Me.LblJobWorker.BackColor = System.Drawing.Color.Transparent
        Me.LblJobWorker.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblJobWorker.Location = New System.Drawing.Point(14, 52)
        Me.LblJobWorker.Name = "LblJobWorker"
        Me.LblJobWorker.Size = New System.Drawing.Size(39, 16)
        Me.LblJobWorker.TabIndex = 731
        Me.LblJobWorker.Text = "Party"
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
        Me.GrpDirectChallan.Controls.Add(Me.RbtForWorkOrder)
        Me.GrpDirectChallan.Controls.Add(Me.RbtForWorkOrderItems)
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
        'RbtForWorkOrder
        '
        Me.RbtForWorkOrder.AutoSize = True
        Me.RbtForWorkOrder.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RbtForWorkOrder.Location = New System.Drawing.Point(5, 8)
        Me.RbtForWorkOrder.Name = "RbtForWorkOrder"
        Me.RbtForWorkOrder.Size = New System.Drawing.Size(126, 17)
        Me.RbtForWorkOrder.TabIndex = 0
        Me.RbtForWorkOrder.TabStop = True
        Me.RbtForWorkOrder.Text = "For Work Order"
        Me.RbtForWorkOrder.UseVisualStyleBackColor = True
        '
        'RbtForWorkOrderItems
        '
        Me.RbtForWorkOrderItems.AutoSize = True
        Me.RbtForWorkOrderItems.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RbtForWorkOrderItems.Location = New System.Drawing.Point(137, 7)
        Me.RbtForWorkOrderItems.Name = "RbtForWorkOrderItems"
        Me.RbtForWorkOrderItems.Size = New System.Drawing.Size(168, 17)
        Me.RbtForWorkOrderItems.TabIndex = 743
        Me.RbtForWorkOrderItems.TabStop = True
        Me.RbtForWorkOrderItems.Text = "For Work Order Items"
        Me.RbtForWorkOrderItems.UseVisualStyleBackColor = True
        '
        'BtnFillWorkOrder
        '
        Me.BtnFillWorkOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnFillWorkOrder.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnFillWorkOrder.Location = New System.Drawing.Point(742, 144)
        Me.BtnFillWorkOrder.Name = "BtnFillWorkOrder"
        Me.BtnFillWorkOrder.Size = New System.Drawing.Size(29, 21)
        Me.BtnFillWorkOrder.TabIndex = 1
        Me.BtnFillWorkOrder.Text = "..."
        Me.BtnFillWorkOrder.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnFillWorkOrder.UseVisualStyleBackColor = True
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
        'LblTotalDeliveryMeasure
        '
        Me.LblTotalDeliveryMeasure.AutoSize = True
        Me.LblTotalDeliveryMeasure.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalDeliveryMeasure.ForeColor = System.Drawing.Color.Black
        Me.LblTotalDeliveryMeasure.Location = New System.Drawing.Point(586, 1)
        Me.LblTotalDeliveryMeasure.Name = "LblTotalDeliveryMeasure"
        Me.LblTotalDeliveryMeasure.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalDeliveryMeasure.TabIndex = 3007
        Me.LblTotalDeliveryMeasure.Text = "."
        '
        'LblTotalDeliveryMeasureText
        '
        Me.LblTotalDeliveryMeasureText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalDeliveryMeasureText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalDeliveryMeasureText.Location = New System.Drawing.Point(357, 1)
        Me.LblTotalDeliveryMeasureText.Name = "LblTotalDeliveryMeasureText"
        Me.LblTotalDeliveryMeasureText.Size = New System.Drawing.Size(213, 22)
        Me.LblTotalDeliveryMeasureText.TabIndex = 3006
        Me.LblTotalDeliveryMeasureText.Text = "Deilvery Measure :"
        Me.LblTotalDeliveryMeasureText.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'FrmWorkOrderAmendment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.BackColor = System.Drawing.SystemColors.ButtonShadow
        Me.ClientSize = New System.Drawing.Size(984, 626)
        Me.Controls.Add(Me.PnlCalcGrid)
        Me.Controls.Add(Me.PnlCustomGrid)
        Me.Controls.Add(Me.GrpDirectChallan)
        Me.Controls.Add(Me.BtnFillWorkOrder)
        Me.Controls.Add(Me.LinkLabel2)
        Me.Controls.Add(Me.TxtTermsAndConditions)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Pnl1)
        Me.Name = "FrmWorkOrderAmendment"
        Me.Text = "Work Order Amendment Entry"
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
        Me.Controls.SetChildIndex(Me.BtnFillWorkOrder, 0)
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
    Protected WithEvents TxtParty As AgControls.AgTextBox
    Protected WithEvents LblJobWorker As System.Windows.Forms.Label
    Protected WithEvents TxtTermsAndConditions As AgControls.AgTextBox
    Protected WithEvents LblTotalAmount As System.Windows.Forms.Label
    Protected WithEvents Label1 As System.Windows.Forms.Label
    Protected WithEvents LinkLabel2 As System.Windows.Forms.LinkLabel
    Protected WithEvents Label32 As System.Windows.Forms.Label
    Protected WithEvents Label3 As System.Windows.Forms.Label
    Protected WithEvents TxtV_Nature As AgControls.AgTextBox
    Protected WithEvents Label9 As System.Windows.Forms.Label
    Protected WithEvents Label6 As System.Windows.Forms.Label
    Protected WithEvents GrpDirectChallan As System.Windows.Forms.GroupBox
    Protected WithEvents RbtForWorkOrder As System.Windows.Forms.RadioButton
    Protected WithEvents RbtForWorkOrderItems As System.Windows.Forms.RadioButton
    Protected WithEvents BtnFillWorkOrder As System.Windows.Forms.Button
#End Region

    Private Sub FrmPurchaseOrderAmendment_BaseEvent_ApproveDeletion_InTrans(ByVal SearchCode As String, ByVal Conn As SqliteConnection, ByVal Cmd As SqliteCommand) Handles Me.BaseEvent_ApproveDeletion_InTrans
        mQry = "DELETE FROM WorkOrderDetail WHERE GenDocId = '" & mSearchCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    End Sub

    Private Sub FrmQuality1_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "WorkOrder"
        LogTableName = "WorkOrder_Log"
        MainLineTableCsv = "WorkOrderdetail"
        LogLineTableCsv = "WorkOrderdetail_Log"

        AgL.GridDesign(Dgl1)

        AgL.AddAgDataGrid(AgCalcGrid1, PnlCalcGrid)
        AgCalcGrid1.AgLibVar = AgL
        AgCalcGrid1.Visible = False

        AgL.AddAgDataGrid(AgCustomGrid1, PnlCustomGrid)

        AgCustomGrid1.AgLibVar = AgL
        AgCustomGrid1.SplitGrid = True
        AgCustomGrid1.MnuText = Me.Name

    End Sub

    Public Sub FSetMeasureParameter(ByVal IsMeasurePerPcsVisible As Boolean, ByVal IsMeasurePerPcsEditable As Boolean, ByVal IsMeasureVisible As Boolean, ByVal IsMeasureEditable As Boolean, ByVal IsMeasureUnitVisible As Boolean, ByVal IsMeasureUnitEditable As Boolean)
        BlnIsMeasurePerPcsVisible = IsMeasurePerPcsVisible
        BlnIsMeasurePerPcsEditable = IsMeasurePerPcsEditable
        BlnIsMeasureVisible = IsMeasureVisible
        BlnIsMeasureEditable = IsMeasureEditable
        BlnIsMeasureUnitVisible = IsMeasureUnitVisible
        BlnIsMeasureUnitEditable = IsMeasureUnitEditable
    End Sub

    Private Sub FrmQuality1_BaseFunction_FIniMast(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMast
        Dim mCondStr$
        mCondStr = " " & AgL.CondStrFinancialYear("M.V_Date", AgL.PubStartDate, AgL.PubEndDate) & _
                       " And " & AgL.PubSiteCondition("M.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "M.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        mQry = " Select M.DocID As SearchCode " &
            " From WorkOrder M   " &
            " Left Join Voucher_Type Vt   On M.V_Type = Vt.V_Type  " &
            " Where IfNull(IsDeleted,0) = 0  " & mCondStr & "  Order By M.V_Date, M.V_No  "

        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mCondStr$

        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) &
                        " And IfNull(H.IsDeleted,0)=0 And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " And H.Div_Code = '" & AgL.PubDivCode & "'"
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        mCondStr = mCondStr & " And H.V_Type NOT IN " &
                    " ( Select L.V_Type " &
                    " FROM User_Exclude_VTypeDetail L  " &
                    " LEFT JOIN Voucher_Type Vt ON Vt.V_Type = L.V_Type  " &
                    " WHERE L.UserName = " & AgL.Chk_Text(AgL.PubUserName) & " And Vt.NCat in ('" & EntryNCat & "') ) "

        AgL.PubFindQry = " SELECT H.DocId AS SearchCode, H.V_Type,   H.V_Date AS [Date], H.V_No , H.ReferenceNo As  [Manual_No], " &
                    " H.PartyName , H.PartyAdd1 ,  H.PartyAdd2 , H.PartyCityName ,   H.PartyState , H.PartyCountry ,   " &
                    " H.Remarks, H.EntryBy AS [Entry_By], H.EntryDate AS [Entry_Date], H.EntryType AS [Entry_Type],  H.EntryStatus AS [Entry_Status], H.ApproveBy AS [Approve_By], H.ApproveDate AS [Approve_Date],  " &
                    " H.Status, D.Div_Name AS [Division], SM.Name AS [Site_Name] " &
                    " FROM WorkOrder  H " &
                    " LEFT JOIN Division D ON D.Div_Code =H.Div_Code   " &
                    " LEFT JOIN SiteMast SM ON SM.Code=H.Site_Code   " &
                    " LEFT JOIN voucher_type Vt ON H.V_Type = vt.V_Type  " &
                    " LEFT JOIN SubGroup SGA ON SGA.SubCode  = H.Agent  " &
                    " Where 1=1 " & mCondStr

        AgL.PubFindQryOrdBy = "[Date]"
    End Sub

    Private Sub FrmProductionOrder_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl1, Col1ItemCode, 100, 0, Col1ItemCode, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_ItemCode")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_ItemCode")), Boolean))
            .AddAgTextColumn(Dgl1, Col1Item, 200, 0, Col1Item, True, Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_ItemName")), Boolean))
            .AddAgTextColumn(Dgl1, Col1WorkOrder, 100, 0, Col1WorkOrder, True, True)
            .AddAgTextColumn(Dgl1, Col1WorkOrderSr, 100, 0, Col1WorkOrderSr, False, True)
            .AddAgTextColumn(Dgl1, Col1BillingType, 45, 0, Col1BillingType, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_BillingType")), Boolean), True)
            .AddAgTextColumn(Dgl1, Col1RateType, 40, 0, Col1RateType, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_RateType")), Boolean), True)
            .AddAgNumberColumn(Dgl1, Col1Qty, 50, 8, 4, True, Col1Qty, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Qty")), Boolean), True, True)
            .AddAgNumberColumn(Dgl1, Col1DocQty, 50, 8, 4, True, Col1DocQty, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Qty")), Boolean), False, True)
            .AddAgTextColumn(Dgl1, Col1Unit, 40, 0, Col1Unit, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Unit")), Boolean), True)
            .AddAgTextColumn(Dgl1, Col1SalesTaxGroup, 40, 0, Col1SalesTaxGroup, False, True)
            .AddAgTextColumn(Dgl1, Col1QtyDecimalPlaces, 50, 0, Col1QtyDecimalPlaces, False, True, False)
            .AddAgNumberColumn(Dgl1, Col1MeasurePerPcs, 100, 8, 4, False, Col1MeasurePerPcs, BlnIsMeasurePerPcsVisible, Not BlnIsMeasurePerPcsEditable, True)
            .AddAgNumberColumn(Dgl1, Col1TotalMeasure, 100, 8, 4, False, Col1TotalMeasure, BlnIsMeasureVisible, Not BlnIsMeasureEditable, True)
            .AddAgNumberColumn(Dgl1, Col1TotalDocMeasure, 80, 8, 4, True, Col1TotalDocMeasure, BlnIsMeasureVisible, Not BlnIsMeasureEditable, True)
            .AddAgTextColumn(Dgl1, Col1MeasureUnit, 100, 50, Col1MeasureUnit, BlnIsMeasureUnitVisible, Not BlnIsMeasureUnitEditable, False)
            .AddAgTextColumn(Dgl1, Col1MeasureDecimalPlaces, 50, 0, Col1MeasureDecimalPlaces, False, True, False)
            .AddAgTextColumn(Dgl1, Col1DeliveryMeasure, 70, 0, Col1DeliveryMeasure, True, True)
            .AddAgNumberColumn(Dgl1, Col1RatePerQty, 100, 8, 2, False, Col1RatePerQty, False, True, True)
            .AddAgNumberColumn(Dgl1, Col1DeliveryMeasureMultiplier, 70, 8, 4, False, Col1DeliveryMeasureMultiplier, False, True, True)
            .AddAgNumberColumn(Dgl1, Col1DeliveryMeasurePerPcs, 110, 8, 4, False, Col1DeliveryMeasurePerPcs, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_MeasurePerPcs")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_MeasurePerPcs")), Boolean), True)
            .AddAgNumberColumn(Dgl1, Col1TotalDeliveryMeasure, 85, 8, 4, False, Col1TotalDeliveryMeasure, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Measure")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_Measure")), Boolean), True)
            .AddAgNumberColumn(Dgl1, Col1TotalDocDeliveryMeasure, 70, 8, 4, True, Col1TotalDocDeliveryMeasure, True, True, True)
            .AddAgNumberColumn(Dgl1, Col1DeliveryMeasureDecimalPlaces, 70, 8, 4, True, Col1DeliveryMeasureDecimalPlaces, False, True, True)
            .AddAgTextColumn(Dgl1, Col1Specification, 50, 0, Col1Specification, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Specification")), Boolean), True, False)
            .AddAgNumberColumn(Dgl1, Col1RateOrder, 60, 8, 2, True, Col1RateOrder, True, True, True)
            .AddAgNumberColumn(Dgl1, Col1RateAmendment, 60, 8, 2, True, Col1RateAmendment, True, True, True)
            .AddAgNumberColumn(Dgl1, Col1Rate, 60, 8, 2, True, Col1Rate, True, True, True)
            .AddAgNumberColumn(Dgl1, Col1Amount, 70, 8, 2, False, Col1Amount, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Amount")), Boolean), True, True)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 48
        Dgl1.AgSkipReadOnlyColumns = True
        Dgl1.AllowUserToOrderColumns = True

        AgTemplate.ClsMain.ProcCreateLink(Dgl1, Col1WorkOrder)


        AgCalcGrid1.AgLineGridPostingGroupSalesTaxProd = Dgl1.Columns(Col1SalesTaxGroup).Index
        AgCalcGrid1.Ini_Grid(LblV_Type.Tag, TxtV_Date.Text)

        AgCalcGrid1.AgLineGrid = Dgl1
        AgCalcGrid1.AgLineGridMandatoryColumn = Dgl1.Columns(Col1Item).Index
        AgCalcGrid1.AgLineGridGrossColumn = Dgl1.Columns(Col1Amount).Index


        AgCustomGrid1.Ini_Grid(mSearchCode)
        AgCustomGrid1.SplitGrid = False

        AgCalcGrid1.Name = "AgCalcGrid1"
        AgCustomGrid1.Name = "AgCustomGrid1"

        If TxtV_Nature.Text = "Rate Amendment" Then
            Dgl1.Columns(Col1RateAmendment).ReadOnly = False
            Dgl1.Columns(Col1RateAmendment).Visible = True
            Dgl1.Columns(Col1RateOrder).Visible = True
            Dgl1.Columns(Col1TotalDeliveryMeasure).ReadOnly = True
            Dgl1.Columns(Col1TotalDeliveryMeasure).Visible = False
        Else
            Dgl1.Columns(Col1RateAmendment).ReadOnly = True
            Dgl1.Columns(Col1RateAmendment).Visible = False
            Dgl1.Columns(Col1RateOrder).Visible = False
            Dgl1.Columns(Col1TotalDeliveryMeasure).ReadOnly = False
            Dgl1.Columns(Col1TotalDeliveryMeasure).Visible = True
        End If

        If AgL.PubUserName.ToUpper = AgLibrary.ClsConstant.PubSuperUserName.ToUpper Then
            AgCL.GridSetiingWriteXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1)
            AgCL.GridSetiingWriteXml(Me.Text & AgCalcGrid1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, AgCalcGrid1)
            AgCL.GridSetiingWriteXml(Me.Text & AgCustomGrid1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, AgCustomGrid1)
        End If

    End Sub

    Private Sub FrmProductionOrder_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As SQLiteConnection, ByVal Cmd As SQLiteCommand) Handles Me.BaseEvent_Save_InTrans
        Dim I As Integer, mSr As Integer
        Dim bSelectionQry$ = ""
        Dim mWorkOrderSr As Integer

        mQry = " UPDATE WorkOrder " &
                "   SET " &
                "   ManualrefNo = " & AgL.Chk_Text(TxtManualRefNo.Text) & ", " &
                "   Party = " & AgL.Chk_Text(TxtParty.Tag) & ", " &
                "	PartyName = " & AgL.Chk_Text(TxtParty.Text) & ", " &
                "	SalesTaxGroupParty = " & AgL.Chk_Text(TxtSalesTaxGroupParty.Tag) & ", " &
                "	TermsAndConditions = " & AgL.Chk_Text(TxtTermsAndConditions.Text) & ", " &
                "	Remarks = " & AgL.Chk_Text(TxtRemarks.Text) & ", " &
                "	Structure = " & AgL.Chk_Text(TxtStructure.Tag) & ", " &
                "   CustomFields = " & AgL.Chk_Text(TxtCustomFields.Tag) & ", " &
                "   " & AgCalcGrid1.FFooterTableUpdateStr() & " " &
                "   " & AgCustomGrid1.FFooterTableUpdateStr() & " " &
                "   Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        'Never Try to Serialise Sr in Line Items 
        'As Some other Entry points may updating values to this Search code and Sr
        mSr = AgL.VNull(AgL.Dman_Execute("Select Max(Sr) From WorkOrderDetail  Where DocID = '" & mSearchCode & "'", AgL.GcnRead).ExecuteScalar)

        With Dgl1
            For I = 0 To .Rows.Count - 1
                If .Item(Col1Item, I).Value <> "" Then
                    If Dgl1.Item(ColSNo, I).Tag Is Nothing And Dgl1.Rows(I).Visible = True Then
                        mSr = mSr + 1
                        mWorkOrderSr = AgL.VNull(AgL.Dman_Execute("Select Max(Sr) From WorkOrderDetail  Where DocID = '" & Dgl1.Item(Col1WorkOrder, I).Tag & "'", AgL.GcnRead).ExecuteScalar) + 1

                        If Val(.Item(Col1WorkOrderSr, I).Value) = 0 Then
                            mQry = " INSERT INTO WorkOrderDetail	( DocId, Sr, Item, V_Nature,  T_Nature, Specification, SalesTaxGroupItem, " &
                                    " Qty, Unit, MeasurePerPcs,	MeasureUnit, TotalMeasure,	WorkOrder,	WorkOrderSr, " &
                                    " BillingType,	RateType, DeliveryMeasurePerPcs, DeliveryMeasure, DeliveryMeasureMultiplier, " &
                                    " DocQty, TotalDocMeasure, TotalDocDeliveryMeasure, " &
                                    " TotalDeliveryMeasure,	Rate, Amount ) " &
                                    " Values(" & AgL.Chk_Text(Dgl1.Item(Col1WorkOrder, I).Tag) & ", " & Val(mWorkOrderSr) & ", " &
                                    " " & AgL.Chk_Text(Dgl1.Item(Col1Item, I).Tag) & ", " &
                                    " " & AgL.Chk_Text(TxtV_Nature.Text) & ", " &
                                    " " & AgTemplate.ClsMain.T_Nature.Amendment & ", " &
                                    " " & AgL.Chk_Text(Dgl1.Item(Col1Specification, I).Value) & ", " &
                                    " " & AgL.Chk_Text(Dgl1.Item(Col1SalesTaxGroup, I).Value) & ", " &
                                    " 0, " &
                                    " " & AgL.Chk_Text(Dgl1.Item(Col1Unit, I).Value) & ", " &
                                    " " & Val(Dgl1.Item(Col1MeasurePerPcs, I).Value) & ", " &
                                    " " & AgL.Chk_Text(Dgl1.Item(Col1MeasureUnit, I).Value) & ", " &
                                    " 0, " &
                                    " " & AgL.Chk_Text(Dgl1.Item(Col1WorkOrder, I).Tag) & ", " &
                                    " " & Val(mWorkOrderSr) & ", " &
                                    " " & AgL.Chk_Text(Dgl1.Item(Col1BillingType, I).Value) & ", " &
                                    " " & AgL.Chk_Text(Dgl1.Item(Col1RateType, I).Value) & ", " &
                                    " " & Val(Dgl1.Item(Col1DeliveryMeasurePerPcs, I).Value) & ", " &
                                    " " & AgL.Chk_Text(Dgl1.Item(Col1DeliveryMeasure, I).Value) & ", " &
                                    " " & Val(Dgl1.Item(Col1DeliveryMeasureMultiplier, I).Value) & ", " &
                                    " " & Val(Dgl1.Item(Col1DocQty, I).Value) & ", " &
                                    " " & Val(Dgl1.Item(Col1TotalDocMeasure, I).Value) & ", " &
                                    " " & Val(Dgl1.Item(Col1TotalDocDeliveryMeasure, I).Value) & ", " &
                                    " 0, " &
                                    " " & Val(Dgl1.Item(Col1Rate, I).Value) & ", " &
                                    " " & Val(Dgl1.Item(Col1Amount, I).Value) & " ) "
                            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                            .Item(Col1WorkOrderSr, I).Value = mWorkOrderSr
                        End If

                        If bSelectionQry <> "" Then bSelectionQry += " UNION ALL "
                        bSelectionQry += " Select " & AgL.Chk_Text(mSearchCode) & ", " & mSr & ", " &
                                            " " & AgL.Chk_Text(.Item(Col1Item, I).Tag) & ", " & AgL.Chk_Text(TxtV_Nature.Text) & ",  " & AgTemplate.ClsMain.T_Nature.Amendment & ", " & AgL.Chk_Text(.Item(Col1Specification, I).Value) & ", " &
                                            " " & AgL.Chk_Text(.Item(Col1SalesTaxGroup, I).Value) & ", " &
                                            " " & Val(.Item(Col1Qty, I).Value) & ", " & AgL.Chk_Text(.Item(Col1Unit, I).Value) & ", " &
                                            " " & Val(.Item(Col1MeasurePerPcs, I).Value) & ", " & AgL.Chk_Text(.Item(Col1MeasureUnit, I).Value) & ", " &
                                            " " & Val(.Item(Col1TotalMeasure, I).Value) & ", " & AgL.Chk_Text(.Item(Col1WorkOrder, I).Tag) & ",	" &
                                            " " & Val(.Item(Col1WorkOrderSr, I).Value) & ", " &
                                            " " & AgL.Chk_Text(.Item(Col1BillingType, I).Value) & "," & AgL.Chk_Text(.Item(Col1RateType, I).Value) & ", " &
                                            " " & Val(.Item(Col1DeliveryMeasurePerPcs, I).Value) & ", " & AgL.Chk_Text(.Item(Col1DeliveryMeasure, I).Value) & ", " &
                                            " " & Val(.Item(Col1DeliveryMeasureMultiplier, I).Value) & ", " &
                                            " " & Val(.Item(Col1DocQty, I).Value) & ", " & Val(.Item(Col1TotalDocMeasure, I).Value) & ", " &
                                            " " & Val(.Item(Col1TotalDocDeliveryMeasure, I).Value) & ", " & Val(.Item(Col1TotalDeliveryMeasure, I).Value) & " , " &
                                            " " & Val(.Item(Col1RateOrder, I).Value) & ", " & Val(.Item(Col1RateAmendment, I).Value) & "," &
                                            " " & Val(.Item(Col1Rate, I).Value) & ", " & Val(.Item(Col1Amount, I).Value) & ", " & IIf(TxtV_Nature.Text = "Rate Amendment", 1, 0) & ", " &
                                            " " & AgCalcGrid1.FLineTableFieldValuesStr(I) & " "
                    Else
                        If Dgl1.Rows(I).Visible = True Then
                            If Dgl1.Rows(I).DefaultCellStyle.BackColor <> RowLockedColour Then
                                mQry = " UPDATE WorkOrderDetail " &
                                        " SET Item = " & AgL.Chk_Text(Dgl1.Item(Col1Item, I).Tag) & ", " &
                                        " V_Nature = " & AgL.Chk_Text(TxtV_Nature.Text) & ", " &
                                        " T_Nature = " & AgTemplate.ClsMain.T_Nature.Amendment & ", " &
                                        " Specification = " & AgL.Chk_Text(Dgl1.Item(Col1Specification, I).Value) & ", " &
                                        " SalesTaxGroupItem = " & AgL.Chk_Text(Dgl1.Item(Col1SalesTaxGroup, I).Value) & ", " &
                                        " Qty = " & Val(Dgl1.Item(Col1Qty, I).Value) & ", " &
                                        " Unit = " & AgL.Chk_Text(Dgl1.Item(Col1Unit, I).Value) & ", " &
                                        " MeasurePerPcs = " & Val(Dgl1.Item(Col1MeasurePerPcs, I).Value) & ", " &
                                        " MeasureUnit = " & AgL.Chk_Text(Dgl1.Item(Col1MeasureUnit, I).Value) & ", " &
                                        " TotalMeasure = " & Val(Dgl1.Item(Col1TotalMeasure, I).Value) & ", " &
                                        " WorkOrder = " & AgL.Chk_Text(Dgl1.Item(Col1WorkOrder, I).Tag) & ", " &
                                        " WorkOrderSr = " & Val(Dgl1.Item(Col1WorkOrderSr, I).Value) & ", " &
                                        " BillingType = " & AgL.Chk_Text(Dgl1.Item(Col1BillingType, I).Value) & ", " &
                                        " RateType = " & AgL.Chk_Text(Dgl1.Item(Col1RateType, I).Value) & ", " &
                                        " DeliveryMeasurePerPcs = " & Val(Dgl1.Item(Col1DeliveryMeasurePerPcs, I).Value) & ", " &
                                        " DeliveryMeasure = " & AgL.Chk_Text(Dgl1.Item(Col1DeliveryMeasure, I).Value) & ", " &
                                        " DeliveryMeasureMultiplier = " & Val(Dgl1.Item(Col1DeliveryMeasureMultiplier, I).Value) & ", " &
                                        " TotalDeliveryMeasure = " & Val(Dgl1.Item(Col1TotalDeliveryMeasure, I).Value) & ", " &
                                        " Rate_Ord = " & Val(Dgl1.Item(Col1RateOrder, I).Value) & ", " &
                                        " Rate_Amd = " & Val(Dgl1.Item(Col1RateAmendment, I).Value) & ", " &
                                        " Rate = " & Val(Dgl1.Item(Col1Rate, I).Value) & ", " &
                                        " AffectRate = " & IIf(TxtV_Nature.Text = "Rate Amendment", 1, 0) & ", " &
                                        " Amount = " & Val(Dgl1.Item(Col1Amount, I).Value) & ", " &
                                        " DocQty = " & Val(Dgl1.Item(Col1DocQty, I).Value) & ", " &
                                        " TotalDocMeasure = " & Val(Dgl1.Item(Col1TotalDocMeasure, I).Value) & ", " &
                                        " TotalDocDeliveryMeasure = " & Val(Dgl1.Item(Col1TotalDocDeliveryMeasure, I).Value) & " " &
                                        " Where DocId = '" & mSearchCode & "' And Sr = " & Dgl1.Item(ColSNo, I).Tag & " "
                                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                            End If
                        Else
                            mQry = " Delete From WorkOrderDetail Where DocId = '" & mSearchCode & "' And Sr = " & Dgl1.Item(ColSNo, I).Tag & "  "
                            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                            mQry = " Delete From WorkOrderDetail Where GenDocId = '" & mSearchCode & "' And GenDocIDSr = " & Dgl1.Item(ColSNo, I).Tag & "  "
                            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                        End If
                    End If
                End If
            Next
        End With

        If bSelectionQry <> "" Then
            mQry = " INSERT INTO WorkOrderDetail ( DocId, Sr, Item, V_Nature, T_Nature, Specification, SalesTaxGroupItem,	" &
                    " Qty, Unit, MeasurePerPcs,	MeasureUnit, TotalMeasure,	WorkOrder,	WorkOrderSr, " &
                    " BillingType, RateType, DeliveryMeasurePerPcs,	DeliveryMeasure, DeliveryMeasureMultiplier, " &
                    " DocQty, TotalDocMeasure, TotalDocDeliveryMeasure, " &
                    " TotalDeliveryMeasure,	Rate_Ord, Rate_Amd, Rate, Amount, AffectRate, " & AgCalcGrid1.FLineTableFieldNameStr() & " ) " & bSelectionQry
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

        mQry = " SELECT H.* " &
                " FROM WorkOrder H " &
                " Where H.DocID = '" & SearchCode & "'"
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                TxtManualRefNo.Text = AgL.XNull(.Rows(0)("ManualrefNo"))
                TxtParty.Tag = AgL.XNull(.Rows(0)("Party"))
                TxtParty.Text = AgL.XNull(.Rows(0)("PartyName"))
                TxtRemarks.Text = AgL.XNull(.Rows(0)("Remarks"))
                TxtTermsAndConditions.Text = AgL.XNull(.Rows(0)("TermsAndConditions"))

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
                mQry = " SELECT L.* , I.ManualCode AS ItemCode, I.Description AS ItemDesc, PO.V_Type + '-' + PO.ManualrefNo  AS WorkOrderNo, PO.V_Date AS WorkOrderdate , " &
                        " U.DecimalPlaces as QtyDecimalPlaces, MU.DecimalPlaces as MeasureDecimalPlaces " &
                        " FROM WorkOrderDetail L " &
                        " LEFT JOIN Item I ON I.Code = L.Item  " &
                        " LEFT JOIN WorkOrder PO ON PO.DocID = L.WorkOrder  " &
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

                            Dgl1.Item(Col1WorkOrder, I).Tag = AgL.XNull(.Rows(I)("WorkOrder"))
                            Dgl1.Item(Col1WorkOrder, I).Value = AgL.XNull(.Rows(I)("WorkOrderNo"))
                            Dgl1.Item(Col1WorkOrderSr, I).Value = AgL.VNull(.Rows(I)("WorkOrderSr"))

                            Dgl1.Item(Col1BillingType, I).Value = AgL.XNull(.Rows(I)("BillingType"))
                            Dgl1.Item(Col1RateType, I).Value = AgL.XNull(.Rows(I)("RateType"))
                            Dgl1.Item(Col1QtyDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("QtyDecimalPlaces"))
                            Dgl1.Item(Col1DocQty, I).Value = Format(AgL.VNull(.Rows(I)("DocQty")), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1Qty, I).Value = Format(AgL.VNull(.Rows(I)("Qty")), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                            Dgl1.Item(Col1SalesTaxGroup, I).Tag = AgL.XNull(.Rows(I)("SalesTaxGroupItem"))
                            Dgl1.Item(Col1SalesTaxGroup, I).Value = AgL.XNull(.Rows(I)("SalesTaxGroupItem"))
                            Dgl1.Item(Col1MeasurePerPcs, I).Value = Format(AgL.VNull(.Rows(I)("MeasurePerPcs")), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1TotalDocMeasure, I).Value = Format(AgL.VNull(.Rows(I)("TotalDocMeasure")), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1TotalMeasure, I).Value = Format(AgL.VNull(.Rows(I)("TotalMeasure")), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1MeasureUnit, I).Value = AgL.XNull(.Rows(I)("MeasureUnit"))
                            Dgl1.Item(Col1MeasureDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("MeasureDecimalPlaces"))
                            Dgl1.Item(Col1DeliveryMeasurePerPcs, I).Value = AgL.VNull(.Rows(I)("DeliveryMeasurePerPcs"))
                            Dgl1.Item(Col1DeliveryMeasure, I).Value = AgL.XNull(.Rows(I)("DeliveryMeasure"))
                            Dgl1.Item(Col1DeliveryMeasureMultiplier, I).Value = AgL.VNull(.Rows(I)("DeliveryMeasureMultiplier"))
                            Dgl1.Item(Col1TotalDeliveryMeasure, I).Value = AgL.VNull(.Rows(I)("TotalDeliveryMeasure"))
                            Dgl1.Item(Col1TotalDocDeliveryMeasure, I).Value = AgL.VNull(.Rows(I)("TotalDocDeliveryMeasure"))
                            Dgl1.Item(Col1Specification, I).Value = AgL.XNull(.Rows(I)("Specification"))
                            Dgl1.Item(Col1Rate, I).Value = AgL.VNull(.Rows(I)("Rate"))
                            Dgl1.Item(Col1RateOrder, I).Value = AgL.VNull(.Rows(I)("Rate_Ord"))
                            Dgl1.Item(Col1RateAmendment, I).Value = AgL.VNull(.Rows(I)("Rate_Amd"))
                            Dgl1.Item(Col1Amount, I).Value = AgL.VNull(.Rows(I)("Amount"))


                            If Not AgL.StrCmp(Dgl1.Item(Col1Unit, I).Value, Dgl1.Item(Col1Unit, 0).Value) Then IsSameUnit = False
                            If Not AgL.StrCmp(Dgl1.Item(Col1MeasureUnit, I).Value, Dgl1.Item(Col1MeasureUnit, 0).Value) Then IsSameMeasureUnit = False
                            If Not AgL.StrCmp(Dgl1.Item(Col1DeliveryMeasure, I).Value, Dgl1.Item(Col1DeliveryMeasure, 0).Value) Then IsSameDeliveryMeasureUnit = False

                            If intQtyDecimalPlaces < Val(Dgl1.Item(Col1QtyDecimalPlaces, I).Value) Then intQtyDecimalPlaces = Val(Dgl1.Item(Col1QtyDecimalPlaces, I).Value)
                            If intMeasureDecimalPlaces < Val(Dgl1.Item(Col1MeasureDecimalPlaces, I).Value) Then intMeasureDecimalPlaces = Val(Dgl1.Item(Col1MeasureDecimalPlaces, I).Value)
                            If intDeliveryMeasureDecimalPlaces < Val(Dgl1.Item(Col1DeliveryMeasureDecimalPlaces, I).Value) Then intDeliveryMeasureDecimalPlaces = Val(Dgl1.Item(Col1DeliveryMeasureDecimalPlaces, I).Value)

                            If TxtV_Nature.Text = "Rate Amendment" Then
                                LblTotalQty.Text = Val(LblTotalQty.Text) + Val(Dgl1.Item(Col1DocQty, I).Value)
                                LblTotalDeliveryMeasure.Text = Val(LblTotalDeliveryMeasure.Text) + Val(Dgl1.Item(Col1TotalDocDeliveryMeasure, I).Value)
                                LblTotalAmount.Text = Val(LblTotalAmount.Text) + Val(Dgl1.Item(Col1Amount, I).Value)
                            Else
                                LblTotalQty.Text = Val(LblTotalQty.Text) + Val(Dgl1.Item(Col1Qty, I).Value)
                                LblTotalDeliveryMeasure.Text = Val(LblTotalDeliveryMeasure.Text) + Val(Dgl1.Item(Col1TotalDeliveryMeasure, I).Value)
                                LblTotalAmount.Text = Val(LblTotalAmount.Text) + Val(Dgl1.Item(Col1Amount, I).Value)
                            End If

                            Call AgCalcGrid1.FMoveRecLineTable(DsTemp.Tables(0), I)
                        Next I
                    End If
                End With
                'Calculation()
                If AgCustomGrid1.Rows.Count = 0 Then AgCustomGrid1.Visible = False
                '-------------------------------------------------------------
            End If
        End With

        If TxtV_Nature.Text = "Rate Amendment" Then
            Dgl1.Columns(Col1RateAmendment).ReadOnly = False
            Dgl1.Columns(Col1RateAmendment).Visible = True
            Dgl1.Columns(Col1RateOrder).Visible = True
            Dgl1.Columns(Col1TotalDeliveryMeasure).ReadOnly = True
            Dgl1.Columns(Col1TotalDeliveryMeasure).Visible = False
        Else
            Dgl1.Columns(Col1RateAmendment).ReadOnly = True
            Dgl1.Columns(Col1RateAmendment).Visible = False
            Dgl1.Columns(Col1RateOrder).Visible = False
            Dgl1.Columns(Col1TotalDeliveryMeasure).ReadOnly = False
            Dgl1.Columns(Col1TotalDeliveryMeasure).Visible = True
        End If
        'AgCL.GridSetiingShowXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1)
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
        sender(ColSNo, sender.Rows.Count - 1).Value = Trim(sender.Rows.Count)
    End Sub

    Private Sub FrmProductionOrder_BaseFunction_Calculation() Handles Me.BaseFunction_Calculation
        Dim I As Integer

        Dim IsSameUnit As Boolean = True
        Dim IsSameMeasureUnit As Boolean = True
        Dim IsSameDeliveryMeasureUnit As Boolean = True

        Dim intQtyDecimalPlaces As Integer = 0
        Dim intMeasureDecimalPlaces As Integer = 0
        Dim intDeliveryMeasureDecimalPlaces As Integer = 0

        LblTotalQty.Text = 0
        LblTotalDeliveryMeasure.Text = 0
        LblTotalAmount.Text = 0

        AgCalcGrid1.AgLineGridPostingGroupSalesTaxProd = Dgl1.Columns(Col1SalesTaxGroup).Index
        AgCalcGrid1.AgPostingGroupSalesTaxParty = TxtSalesTaxGroupParty.AgSelectedValue
        AgCalcGrid1.AgPostingGroupSalesTaxItem = AgL.XNull(AgL.PubDtEnviro.Rows(0)("DefaultSalesTaxGroupItem"))

        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1Item, I).Value <> "" Then

                'New Calculation

                If TxtV_Nature.Text = "Rate Amendment" Then
                    Dgl1.Item(Col1Rate, I).Value = Dgl1.Item(Col1RateAmendment, I).Value - Dgl1.Item(Col1RateOrder, I).Value
                    Dgl1.Item(Col1Qty, I).Value = 0
                    Dgl1.Item(Col1TotalDeliveryMeasure, I).Value = 0

                    If AgL.VNull(Dgl1.Item(Col1DeliveryMeasureMultiplier, I).Value) <> 0 Then
                        If Val(Dgl1.Item(Col1MeasurePerPcs, I).Value) <> 0 Then
                            Dgl1.Item(Col1TotalDocDeliveryMeasure, I).Value = Format(Val(Dgl1.Item(Col1DocQty, I).Value) * Val(Dgl1.Item(Col1DeliveryMeasureMultiplier, I).Value), "0.".PadRight(Val(Dgl1.Item(Col1DeliveryMeasureDecimalPlaces, I).Value) + 2, "0"))
                        Else
                            Dgl1.Item(Col1DocQty, I).Value = Val(Dgl1.Item(Col1TotalDocDeliveryMeasure, I).Value) * Val(Dgl1.Item(Col1DeliveryMeasureMultiplier, I).Value)
                        End If
                    End If


                    If Val(Dgl1.Item(Col1DocQty, I).Value) <> 0 Then Dgl1.Item(Col1RatePerQty, I).Value = Val(Dgl1.Item(Col1Amount, I).Value) / Val(Dgl1.Item(Col1DocQty, I).Value)
                    Dgl1.Item(Col1Amount, I).Value = Format(Val(Dgl1.Item(Col1TotalDocDeliveryMeasure, I).Value) * Val(Dgl1.Item(Col1Rate, I).Value), "0.".PadRight(CType(Dgl1.Columns(Col1Amount), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))

                    LblTotalQty.Text = Val(LblTotalQty.Text) + Val(Dgl1.Item(Col1DocQty, I).Value)
                    LblTotalDeliveryMeasure.Text = Val(LblTotalDeliveryMeasure.Text) + Val(Dgl1.Item(Col1TotalDocDeliveryMeasure, I).Value)
                    LblTotalAmount.Text = Val(LblTotalAmount.Text) + Val(Dgl1.Item(Col1Amount, I).Value)
                Else
                    'In Case of Carpet Calculation
                    'User Will feed Qty first
                    'THen TotalMeasure is calculated on hte basis Of Measure Per Pcs
                    'If In Item Master Measure Per Pcs Is Defined then this calculation will be executed.
                    'For Example In Carpet Area Per Pcs Is Defined in Item Master and Total Area will be calculated
                    'with that Area per pcs. 
                    If AgL.VNull(Dgl1.Item(Col1DeliveryMeasureMultiplier, I).Value) <> 0 Then
                        If Val(Dgl1.Item(Col1MeasurePerPcs, I).Value) <> 0 Then
                            Dgl1.Item(Col1TotalDeliveryMeasure, I).Value = Format(Val(Dgl1.Item(Col1Qty, I).Value) * Val(Dgl1.Item(Col1DeliveryMeasureMultiplier, I).Value), "0.".PadRight(Val(Dgl1.Item(Col1DeliveryMeasureDecimalPlaces, I).Value) + 2, "0"))
                        Else
                            Dgl1.Item(Col1Qty, I).Value = Val(Dgl1.Item(Col1TotalDeliveryMeasure, I).Value) * Val(Dgl1.Item(Col1DeliveryMeasureMultiplier, I).Value)
                        End If
                    End If

                    'Delivery measure calculation
                    'By Default Deliver Measure Unit is Equal To Qty Unit
                    'Now if user changes the Deliver Measure
                    'Then the Delivery Measure Multiplier will auto come from Unit Conversio Table
                    'Delivery measure will be automatically calculated on the basis of delivery measure multiplier.

                    'For General Purpose Calculation
                    'User will feed Delivery Measure And Qty will calculate automatically on the basis of Delivery Measure Multiplier
                    'If the Deivery Measure Multiplier is 0 Or Unit Conversion factor does not exist in Unit Conversion
                    'Table the user will feed qty manually and Qty will not calculated automatically
                    If AgL.VNull(Dgl1.Item(Col1DeliveryMeasureMultiplier, I).Value) <> 0 Then

                    End If

                    If Val(Dgl1.Item(Col1Qty, I).Value) <> 0 Then Dgl1.Item(Col1RatePerQty, I).Value = Val(Dgl1.Item(Col1Amount, I).Value) / Val(Dgl1.Item(Col1Qty, I).Value)
                    Dgl1.Item(Col1Amount, I).Value = Format(Val(Dgl1.Item(Col1TotalDeliveryMeasure, I).Value) * Val(Dgl1.Item(Col1Rate, I).Value), "0.".PadRight(CType(Dgl1.Columns(Col1Amount), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))

                    LblTotalQty.Text = Val(LblTotalQty.Text) + Val(Dgl1.Item(Col1Qty, I).Value)
                    LblTotalDeliveryMeasure.Text = Val(LblTotalDeliveryMeasure.Text) + Val(Dgl1.Item(Col1TotalDeliveryMeasure, I).Value)
                    LblTotalAmount.Text = Val(LblTotalAmount.Text) + Val(Dgl1.Item(Col1Amount, I).Value)
                End If

                If Not AgL.StrCmp(Dgl1.Item(Col1Unit, I).Value, Dgl1.Item(Col1Unit, 0).Value) Then IsSameUnit = False
                If Not AgL.StrCmp(Dgl1.Item(Col1MeasureUnit, I).Value, Dgl1.Item(Col1MeasureUnit, 0).Value) Then IsSameMeasureUnit = False
                If Not AgL.StrCmp(Dgl1.Item(Col1DeliveryMeasure, I).Value, Dgl1.Item(Col1DeliveryMeasure, 0).Value) Then IsSameDeliveryMeasureUnit = False

                If intQtyDecimalPlaces < Val(Dgl1.Item(Col1QtyDecimalPlaces, I).Value) Then intQtyDecimalPlaces = Val(Dgl1.Item(Col1QtyDecimalPlaces, I).Value)
                If intMeasureDecimalPlaces < Val(Dgl1.Item(Col1MeasureDecimalPlaces, I).Value) Then intMeasureDecimalPlaces = Val(Dgl1.Item(Col1MeasureDecimalPlaces, I).Value)
                If intDeliveryMeasureDecimalPlaces < Val(Dgl1.Item(Col1DeliveryMeasureDecimalPlaces, I).Value) Then intDeliveryMeasureDecimalPlaces = Val(Dgl1.Item(Col1DeliveryMeasureDecimalPlaces, I).Value)
            End If
        Next

        AgCalcGrid1.AgPostingGroupSalesTaxParty = TxtSalesTaxGroupParty.Tag
        AgCalcGrid1.AgPostingGroupSalesTaxItem = AgL.XNull(AgL.PubDtEnviro.Rows(0)("DefaultSalesTaxGroupItem"))
        AgCalcGrid1.Calculation()

        LblTotalQty.Text = Format(Val(LblTotalQty.Text), "0.".PadRight(intQtyDecimalPlaces + 2, "0"))
        LblTotalDeliveryMeasure.Text = Format(Val(LblTotalDeliveryMeasure.Text), "0.".PadRight(intDeliveryMeasureDecimalPlaces + 2, "0"))
        LblTotalAmount.Text = Format(Val(LblTotalAmount.Text), "0.00")


        If Dgl1.Item(Col1Unit, 0).Value <> "" And IsSameUnit Then LblTotalQtyText.Text = "Qty (" & Dgl1.Item(Col1Unit, 0).Value & ") :" Else LblTotalQtyText.Text = "Qty :"
        If Dgl1.Item(Col1DeliveryMeasure, 0).Value <> "" And IsSameDeliveryMeasureUnit Then LblTotalDeliveryMeasureText.Text = "Delivery Measure (" & Dgl1.Item(Col1DeliveryMeasure, 0).Value & ") :" Else LblTotalDeliveryMeasureText.Text = "Delivery Measure :"

    End Sub

    Private Sub FrmProductionOrder_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        Dim I As Integer = 0
        Dim StrMessage As String = ""
        passed = FCheckDuplicateRefNo()

        If AgL.RequiredField(TxtParty, LblJobWorker.Text) Then passed = False : Exit Sub
        If AgCL.AgIsBlankGrid(Dgl1, Dgl1.Columns(Col1Item).Index) Then passed = False : Exit Sub

        With Dgl1
            For I = 0 To .Rows.Count - 1
                If .Item(Col1Item, I).Value <> "" Then
                    If Dgl1.Rows(I).Visible Then
                        If AgL.StrCmp(TxtV_Nature.Text, "Qty Amendment") Then

                            If RbtAddNewItem.Checked = True Then
                                If .Item(Col1WorkOrder, I).Value = "" Then
                                    MsgBox("Work. Order Is Blank At Row No " & Dgl1.Item(ColSNo, I).Value & "")
                                    .CurrentCell = .Item(Col1WorkOrder, I) : Dgl1.Focus()
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
                                        " FROM WorkOrder H " &
                                        " LEFT JOIN WorkOrderDetail L ON L.DocId = H.DocID  " &
                                        " WHERE L.WorkOrder = '" & Dgl1.Item(Col1WorkOrder, I).Tag & "' " &
                                        " AND L.WorkOrderSr = " & Dgl1.Item(Col1WorkOrderSr, I).Value & " " &
                                        " AND H.DocId <> '" & mSearchCode & "' " &
                                        " GROUP BY L.WorkOrder, L.WorkOrderSr "
                                If Math.Abs(Val(.Item(Col1Qty, I).Value)) > AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar) Then
                                    MsgBox("Cancel Qty Is Less Then Total Pending Qty For Work Order At Row No " & Dgl1.Item(ColSNo, I).Value & ".", MsgBoxStyle.Information, "Validation")
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
                                        " FROM WorkOrder H " &
                                        " LEFT JOIN WorkOrderDetail L ON L.DocId = H.DocID  " &
                                        " WHERE L.WorkOrder = '" & Dgl1.Item(Col1WorkOrder, I).Tag & "' " &
                                        " AND L.WorkOrderSr = " & Dgl1.Item(Col1WorkOrderSr, I).Value & " " &
                                        " AND H.DocId <> '" & mSearchCode & "' " &
                                        " GROUP BY L.WorkOrder, L.WorkOrderSr "
                                If Val(.Item(Col1DocQty, I).Value) > AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar) Then
                                    MsgBox("Amendment Qty Is Greater Than Total Pending Qty For Work Order At Row No " & Dgl1.Item(ColSNo, I).Value & ".", MsgBoxStyle.Information, "Validation")
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
            mQry = " SELECT COUNT(*) FROM WorkOrder   " &
                    " WHERE ManualRefNo = '" & TxtManualRefNo.Text & "'   " &
                    " AND V_Type ='" & TxtV_Type.AgSelectedValue & "'  " &
                    " And Div_Code = '" & TxtDivision.AgSelectedValue & "' " &
                    " And Site_Code = '" & TxtSite_Code.AgSelectedValue & "'  " &
                    " And EntryStatus <> 'Discard' "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then TxtManualRefNo.Text = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "JobOrder", TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, AgTemplate.ClsMain.ManualRefType.Max) : MsgBox("Reference No. Already Exists New Reference No. Alloted : " & TxtManualRefNo.Text)
        Else
            mQry = " SELECT COUNT(*) FROM WorkOrder  " &
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
        LblTotalQty.Text = 0 : LblTotalAmount.Text = 0 : LblTotalDeliveryMeasure.Text = 0
    End Sub

    Private Sub Txt_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtManualRefNo.Validating, TxtParty.Validating, TxtV_Nature.Validating
        Dim DrTemp As DataRow() = Nothing
        Dim DtTemp As DataTable = Nothing
        Dim I As Integer = 0
        Try
            Select Case sender.name
                Case TxtV_Date.Name
                    If Topctrl1.Mode = "Add" Then
                        TxtManualRefNo.Text = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "WorkOrder", TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, AgTemplate.ClsMain.ManualRefType.Max)
                    End If

                Case TxtV_Type.Name
                    FFillV_TypeValues()

                Case TxtManualRefNo.Name
                    e.Cancel = Not FCheckDuplicateRefNo()

                Case TxtV_Nature.Name
                    If TxtV_Nature.Text = "Rate Amendment" Then
                        Dgl1.Columns(Col1RateAmendment).ReadOnly = False
                        Dgl1.Columns(Col1RateAmendment).Visible = True
                        Dgl1.Columns(Col1RateOrder).Visible = True
                        Dgl1.Columns(Col1TotalDeliveryMeasure).ReadOnly = True
                        Dgl1.Columns(Col1TotalDeliveryMeasure).Visible = False
                    Else
                        Dgl1.Columns(Col1RateAmendment).ReadOnly = True
                        Dgl1.Columns(Col1RateAmendment).Visible = False
                        Dgl1.Columns(Col1RateOrder).Visible = False
                        Dgl1.Columns(Col1TotalDeliveryMeasure).ReadOnly = False
                        Dgl1.Columns(Col1TotalDeliveryMeasure).Visible = True
                    End If
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
            TxtManualRefNo.Text = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "WorkOrder", TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, AgTemplate.ClsMain.ManualRefType.Max)
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
                Dgl1.Item(Col1Specification, mRow).Value = ""
                Dgl1.Item(Col1Qty, mRow).Value = ""
                Dgl1.Item(Col1DocQty, mRow).Value = ""
                Dgl1.Item(Col1Unit, mRow).Value = ""
                Dgl1.Item(Col1Rate, mRow).Value = ""
                Dgl1.Item(Col1Amount, mRow).Value = ""
                Dgl1.Item(Col1MeasurePerPcs, mRow).Value = ""
                Dgl1.Item(Col1DeliveryMeasureMultiplier, mRow).Value = ""
                Dgl1.Item(Col1SalesTaxGroup, mRow).Value = ""
                Dgl1.Item(Col1BillingType, mRow).Value = ""
                Dgl1.Item(Col1WorkOrder, mRow).Tag = ""
                Dgl1.Item(Col1WorkOrder, mRow).Value = ""
                Dgl1.Item(Col1WorkOrderSr, mRow).Value = ""
                Dgl1.Item(Col1SalesTaxGroup, mRow).Tag = ""
            Else
                If Dgl1.AgDataRow IsNot Nothing Then
                    Dgl1.Item(Col1Specification, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("Specification").Value)
                    Dgl1.Item(Col1Qty, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("BalQty").Value)
                    Dgl1.Item(Col1DocQty, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("BalQty").Value)
                    Dgl1.Item(Col1Unit, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("Unit").Value)
                    Dgl1.Item(Col1DeliveryMeasure, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("DeliveryMeasure").Value)
                    Dgl1.Item(Col1QtyDecimalPlaces, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("QtyDecimalPlaces").Value)
                    Dgl1.Item(Col1TotalDeliveryMeasure, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("TotalDeliveryMeasure").Value)
                    Dgl1.Item(Col1TotalDocDeliveryMeasure, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("TotalDeliveryMeasure").Value)

                    Dgl1.Item(Col1MeasurePerPcs, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("MeasurePerPcs").Value)
                    Dgl1.Item(Col1MeasureDecimalPlaces, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("MeasureDecimalPlaces").Value)
                    Dgl1.Item(Col1DeliveryMeasureDecimalPlaces, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("MeasureDecimalPlaces").Value)
                    Dgl1.Item(Col1DeliveryMeasureMultiplier, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("DeliveryMeasureMultiplier").Value)
                    Dgl1.Item(Col1BillingType, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("BillingType").Value)
                    Dgl1.Item(Col1WorkOrder, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("WorkOrder").Value)
                    Dgl1.Item(Col1WorkOrder, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("WorkOrderNo").Value)
                    Dgl1.Item(Col1WorkOrderSr, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("WorkOrderSr").Value)
                    Dgl1.Item(Col1SalesTaxGroup, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("SalesTaxGroupItem").Value)

                    If AgL.XNull(Dgl1.AgDataRow.Cells("DeliveryMeasure").Value) = "" Then
                        Dgl1.Item(Col1DeliveryMeasure, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("Unit").Value)
                        Dgl1.Item(Col1DeliveryMeasure, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("Unit").Value)
                        Dgl1.Item(Col1DeliveryMeasureMultiplier, mRow).Value = 1
                        Dgl1.Item(Col1DeliveryMeasureDecimalPlaces, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("QtyDecimalPlaces").Value)
                    Else
                        Dgl1.Item(Col1DeliveryMeasure, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("DeliveryMeasure").Value)
                        Call FGetDeliveryMeasureMultiplier(mRow)
                    End If

                    If TxtV_Nature.Text = "Rate Amendment" Then
                        Dgl1.Columns(Col1RateAmendment).ReadOnly = False
                        Dgl1.Columns(Col1RateAmendment).Visible = True
                        Dgl1.Columns(Col1RateOrder).Visible = True
                        Dgl1.Columns(Col1TotalDeliveryMeasure).ReadOnly = True
                        Dgl1.Columns(Col1TotalDeliveryMeasure).Visible = False
                        Dgl1.Item(Col1RateOrder, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("Rate").Value)
                    Else
                        Dgl1.Columns(Col1RateAmendment).ReadOnly = True
                        Dgl1.Columns(Col1RateAmendment).Visible = False
                        Dgl1.Columns(Col1RateOrder).Visible = False
                        Dgl1.Columns(Col1TotalDeliveryMeasure).ReadOnly = False
                        Dgl1.Columns(Col1TotalDeliveryMeasure).Visible = True
                        Dgl1.Item(Col1Rate, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("Rate").Value)
                    End If

                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " On Validating_Item Function ")
        Finally
            If sqlConn IsNot Nothing Then sqlConn.Dispose()
            If sqlDA IsNot Nothing Then sqlDA.Dispose()
        End Try
    End Sub

    Private Sub FGetDeliveryMeasureMultiplier(ByVal mRow As Integer)
        Dim DtTemp As DataTable = Nothing
        Try
            If AgL.StrCmp(AgL.XNull(DtV_TypeSettings.Rows(0)("IndustryType")), "Carpet") Then
                If AgL.StrCmp(Dgl1.Item(Col1DeliveryMeasure, mRow).Value, Dgl1.Item(Col1MeasureUnit, mRow).Value) Then
                    Dgl1.Item(Col1DeliveryMeasureMultiplier, mRow).Value = Dgl1.Item(Col1MeasurePerPcs, mRow).Value
                    Dgl1.Item(Col1DeliveryMeasureDecimalPlaces, mRow).Value = Dgl1.Item(Col1MeasureDecimalPlaces, mRow).Value
                ElseIf AgL.StrCmp(Dgl1.Item(Col1DeliveryMeasure, mRow).Value, Dgl1.Item(Col1Unit, mRow).Value) Then
                    Dgl1.Item(Col1DeliveryMeasureMultiplier, mRow).Value = 1
                    Dgl1.Item(Col1DeliveryMeasureDecimalPlaces, mRow).Value = Dgl1.Item(Col1QtyDecimalPlaces, mRow).Value
                ElseIf AgL.StrCmp(Dgl1.Item(Col1DeliveryMeasure, mRow).Value, "SQ.FEET") Then
                    mQry = "Select FeetArea From Rug_Size Size Left Join Rug_CarpetSku Cs On Size.Code = Cs.Size Where Cs.Code = '" & Dgl1.Item(Col1Item, mRow).Tag & "' "
                    Dgl1.Item(Col1DeliveryMeasureMultiplier, mRow).Value = AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar)
                ElseIf AgL.StrCmp(Dgl1.Item(Col1DeliveryMeasure, mRow).Value, "SQ.METER") Then
                    mQry = "Select MeterArea From Rug_Size Size Left Join Rug_CarpetSku Cs On Size.Code = Cs.Size Where Cs.Code = '" & Dgl1.Item(Col1Item, mRow).Tag & "' "
                    Dgl1.Item(Col1DeliveryMeasureMultiplier, mRow).Value = AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar)
                Else
                    Dgl1.Item(Col1DeliveryMeasureMultiplier, mRow).Value = 0
                End If
            Else
                If Dgl1.Item(Col1DeliveryMeasure, mRow).Value <> "" Then
                    If AgL.StrCmp(Dgl1.Item(Col1DeliveryMeasure, mRow).Value, Dgl1.Item(Col1MeasureUnit, mRow).Value) Then
                        Dgl1.Item(Col1DeliveryMeasureMultiplier, mRow).Value = Dgl1.Item(Col1MeasurePerPcs, mRow).Value
                    ElseIf AgL.StrCmp(Dgl1.Item(Col1DeliveryMeasure, mRow).Value, Dgl1.Item(Col1Unit, mRow).Value) Then
                        Dgl1.Item(Col1DeliveryMeasureMultiplier, mRow).Value = 1
                    Else
                        mQry = " Select C.Multiplier, U.DecimalPlaces " &
                                " From UnitConversion C " &
                                " LEFT JOIN Unit U On C.FromUnit = U.Code " &
                                " Where Item = '" & Dgl1.Item(Col1Item, mRow).Tag & "' " &
                                " And FromUnit = '" & Dgl1.Item(Col1DeliveryMeasure, mRow).Value & "' " &
                                " And ToUnit = '" & Dgl1.Item(Col1Unit, mRow).Value & "' "
                        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
                        If DtTemp.Rows.Count > 0 Then
                            Dgl1.Item(Col1DeliveryMeasureMultiplier, mRow).Value = AgL.VNull(DtTemp.Rows(0)("Multiplier"))
                            Dgl1.Item(Col1DeliveryMeasureDecimalPlaces, mRow).Value = AgL.VNull(DtTemp.Rows(0)("DecimalPlaces"))
                        Else
                            Dgl1.Item(Col1DeliveryMeasureMultiplier, mRow).Value = 0
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
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
                 " From (Select * From WorkOrder Where DocId = '" & mSearchCode & "') H   " &
                 " LEFT JOIN WorkOrderDetail L On H.DocId = L.DocId   "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    End Sub

    Private Sub TempJobOrder_BaseEvent_Topctrl_tbAdd() Handles Me.BaseEvent_Topctrl_tbAdd
        TxtManualRefNo.Text = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "WorkOrder", TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, AgTemplate.ClsMain.ManualRefType.Max)
        TxtTermsAndConditions.Text = AgTemplate.ClsMain.FRetTermsCondition(TxtV_Type.AgSelectedValue)
        RbtForWorkOrder.Checked = True
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
            If TxtParty.AgHelpDataSet IsNot Nothing Then TxtParty.AgHelpDataSet.Dispose() : TxtParty.AgHelpDataSet = Nothing
        Catch ex As Exception
        End Try
    End Sub

    Private Sub TxtOrderBy_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtParty.KeyDown, TxtV_Nature.KeyDown
        Try
            Select Case sender.name
                Case TxtV_Nature.Name
                    If e.KeyCode <> Keys.Enter Then
                        If sender.AgHelpDataSet Is Nothing Then
                            mQry = " SELECT 'Qty Amendment' AS Code, 'Qty Amendment' AS Nature " &
                                    " UNION ALL " &
                                    " SELECT 'Rate Amendment' AS Code, 'Rate Amendment' AS Nature "
                            sender.AgHelpDataSet(0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If

                Case TxtParty.Name
                    If TxtParty.AgHelpDataSet Is Nothing Then
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

        mQry = " SELECT H.SubCode, H.DispName + ',' + IfNull(C.CityName,'') AS [Party], " &
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

                Case Col1WorkOrder
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1WorkOrder) Is Nothing Then
                            mQry = " SELECT H.DocID, H.V_Type + '-' + H.ManualRefNo AS WorkOrderNo , H.V_Date AS WorkOrderDate " &
                                    " FROM WorkOrder H " &
                                    " Where 1=1 AND H.Party = '" & TxtParty.Tag & "' "
                            Dgl1.AgHelpDataSet(Col1WorkOrder) = AgL.FillData(mQry, AgL.GCn)
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

        If RbtAddNewItem.Checked = True Then
            mQry = " SELECT I.Code, I.Description AS ItemDesc, I.ManualCode AS ItemCode, I.Unit, '' AS WorkOrderNo,   '' As WorkOrderDate, '' AS BillingType , " &
                    " '' AS RateType , '' AS WorkOrder, 0 AS WorkOrderSr, 1 As BalQty, 0 As MeasurePerPcs,  I.MeasureUnit As MeasureUnit, " &
                    " U.DecimalPlaces AS QtyDecimalPlaces, MU.DecimalPlaces AS MeasureDecimalPlaces , 0 AS Rate ,  0 AS DeliveryMeasureMultiplier, " &
                    " 0 AS TotalDeliveryMeasure, I.DeliveryMeasure, 0 AS DeliveryMeasurePerPcs , NULL AS Specification, Null AS SalesTaxGroupItem " &
                    " FROM Item I " &
                    " Left Join Unit U On I.Unit = U.Code     " &
                    " Left Join Unit MU On I.MeasureUnit = MU.Code  " &
                    " WHERE IfNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
            Dgl1.AgHelpDataSet(Col1Item, 18) = AgL.FillData(mQry, AgL.GCn)

            Dgl1.Columns(Col1WorkOrder).ReadOnly = False
        Else
            mQry = " SELECT Max(I.Code) AS Code,  Max(I.Description) AS Item,  Max(H.V_Type + '-' + H.ManualrefNo) AS WorkOrderNo, " &
                    " Sum(l.Qty) - IfNull(Max(VChallan.ShippedQty),0) AS BalQty, Max(L.Unit) AS Unit, Max(L.Specification) AS Specification," &
                    " max(U.Decimalplaces) AS QtyDecimalplaces, max(UM.Decimalplaces) AS MeasureDecimalplaces, max(L.Rate) AS Rate, max(L.MeasurePerPcs) AS MeasurePerPcs, Max(L.DeliveryMeasureMultiplier) AS DeliveryMeasureMultiplier, " &
                    " Sum(l.TotalDeliveryMeasure) - IfNull(Max(VChallan.TotalDeliveryMeasure),0) AS TotalDeliveryMeasure, max(L.DeliveryMeasure) AS DeliveryMeasure, max(L.DeliveryMeasure) AS BillingType, max(L.SalesTaxGroupItem) AS SalesTaxGroupItem, L.WorkOrder, L.WorkOrderSr " &
                    " FROM WorkOrderDetail L  " &
                    " LEFT JOIN WorkOrder H ON L.WorkOrder = H.DocID " &
                    " Left Join " &
                    " 	(SELECT L.WorkOrder, L.WorkOrderSr, Sum(L.Qty) AS ShippedQty, Sum(L.TotalDeliveryMeasure) AS TotalDeliveryMeasure " &
                    " 	 FROM WorkDispatchDetail L  " &
                    " 	 GROUP BY L.WorkOrder, L.WorkOrderSr) AS VChallan  " &
                    " ON L.WorkOrder = VChallan.WorkOrder AND L.WorkOrderSr = VChallan.WorkOrderSr " &
                    " LEFT JOIN Item I ON L.Item = I.Code " &
                    " LEFT JOIN Unit U On U.Code = L.Unit " &
                    " LEFT JOIN Unit UM On UM.Code = L.DeliveryMeasure " &
                    " Where IfNull(H.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "'  " &
                    " And H.Party = '" & TxtParty.Tag & "' And H.Div_Code = '" & TxtDivision.Tag & "'  And H.Site_Code = '" & TxtSite_Code.Tag & "' " &
                    " AND L.DocId <> '" & mInternalCode & "' " &
                    " GROUP BY L.WorkOrder, L.WorkOrderSr " &
                    " Having Sum(l.Qty) - IfNull(Max(VChallan.ShippedQty),0) > 0 "
            Dgl1.AgHelpDataSet(Col1Item, 9) = AgL.FillData(mQry, AgL.GCn)
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
                'Case Col1WorkOrder
                'Call ClsMain.ProcOpenLinkForm(Mdi.MnuFinishingOrderEntry, Dgl1.Item(Col1WorkOrder, e.RowIndex).Tag, Me.MdiParent)

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
        '            " L.Remark AS LineRemark, L.WorkOrder, L.WorkOrderSr, L.BillingType, L.DocQty, L.DocMeasure, L.V_Nature, L.RateAffected, " & _
        '            " I.Description AS ItemDesc, I.ManualCode AS ItemCode, PO.ReferenceNo AS WorkOrderNo, " & _
        '            " " & AgCalcGrid1.FLineTableFieldNameStr("L.", "L_") & "  " & _
        '            " FROM WorkOrder H " & _
        '            " LEFT JOIN WorkOrderDetail L ON L.DocId = H.DocID  " & _
        '            " LEFT JOIN Item I ON I.Code = L.Item  " & _
        '            " LEFT JOIN WorkOrder PO ON PO.DocID = L.WorkOrder " & _
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

    Private Sub BtnFill_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnFillWorkOrder.Click
        Try
            If Topctrl1.Mode = "Browse" Then Exit Sub
            If RbtAddNewItem.Checked Then Exit Sub

            Dim StrTicked As String = ""

            If MsgBox("Do You Want To Fill Only Balance Qty ?", MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.Yes Then
                FillForBalanceQty = True
            Else
                FillForBalanceQty = False
            End If

            If RbtForWorkOrderItems.Checked Then
                StrTicked = FHPGD_PendingWorkOrderItems()
            ElseIf RbtForWorkOrder.Checked Then
                StrTicked = FHPGD_PendingWorkOrder()
            End If

            If StrTicked <> "" Then
                FFillItemsForPendingWorkOrders(StrTicked)
            Else
                Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
            End If

            Dgl1.Focus()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function FHPGD_PendingWorkOrderItems() As String
        Dim FRH_Multiple As DMHelpGrid.FrmHelpGrid_Multi
        Dim StrRtn As String = ""

        mQry = " SELECT 'o' As Tick, VMain.WorkOrder + Convert(nVarChar, VMain.WorkOrderSr) As WorkOrderDocIdSr, " &
                " Max(VMain.WorkOrderNo) AS WorkOrderNo,  " &
                " Max(VMain.WorkOrderDate) AS WorkOrderDate, Max(VMain.ItemDesc) As ItemDesc, SUM(VMain.Qty) AS BalQty " &
                " FROM ( " & FRetFillItemWiseQry("And Party = '" & TxtParty.Tag & "' And Site_Code = '" & TxtSite_Code.Tag & "' And Div_Code = '" & TxtDivision.Tag & "' And V_Date <= '" & TxtV_Date.Text & "'", "") & " ) As VMain " &
                " GROUP BY VMain.WorkOrder, VMain.WorkOrderSr " &
                " Order By WorkOrderDate "

        FRH_Multiple = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(AgL.FillData(mQry, AgL.GCn).TABLES(0)), "", 500, 750, , , False)
        FRH_Multiple.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple.FFormatColumn(1, , 0, , False)
        FRH_Multiple.FFormatColumn(2, "Work Order No.", 180, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(3, "Work Order Date", 180, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(4, "Item", 150, DataGridViewContentAlignment.MiddleLeft)

        FRH_Multiple.StartPosition = FormStartPosition.CenterScreen
        FRH_Multiple.ShowDialog()

        If FRH_Multiple.BytBtnValue = 0 Then
            StrRtn = FRH_Multiple.FFetchData(1, "'", "'", ",", True)
        End If
        FHPGD_PendingWorkOrderItems = StrRtn

        FRH_Multiple = Nothing
    End Function

    Private Function FHPGD_PendingWorkOrder() As String
        Dim FRH_Multiple As DMHelpGrid.FrmHelpGrid_Multi
        Dim StrRtn As String = ""

        mQry = " SELECT 'o' As Tick, VMain.WorkOrder, Max(VMain.WorkOrderNo) AS WorkOrderNo,  " &
                " Max(VMain.WorkOrderDate) AS WorkOrderDate , SUM(VMain.Qty) AS BalQty   " &
                " FROM ( " & FRetFillItemWiseQry("And Party = '" & TxtParty.Tag & "' And Site_Code = '" & TxtSite_Code.Tag & "' And Div_Code = '" & TxtDivision.Tag & "' And V_Date <= '" & TxtV_Date.Text & "'", "") & " ) As VMain " &
                " GROUP BY VMain.WorkOrder " &
                " Order By WorkOrderDate "

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
        FHPGD_PendingWorkOrder = StrRtn

        FRH_Multiple = Nothing
    End Function

    Private Sub FFillItemsForPendingWorkOrders(ByVal bOrderNoStr As String)
        Dim I As Integer = 0
        Dim DtTemp As DataTable = Nothing
        Try
            If bOrderNoStr = "" Then Exit Sub

            If RbtForWorkOrderItems.Checked Then
                mQry = FRetFillItemWiseQry("", " And L.WorkOrder + Convert(nVarChar, L.WorkOrderSr) In (" & bOrderNoStr & ")")
            Else
                mQry = FRetFillItemWiseQry("", " And L.WorkOrder In (" & bOrderNoStr & ") ")
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
                        Dgl1.Item(Col1WorkOrder, J).Tag = AgL.XNull(.Rows(I)("WorkOrder"))
                        Dgl1.Item(Col1WorkOrder, J).Value = AgL.XNull(.Rows(I)("WorkOrderNo"))
                        Dgl1.Item(Col1WorkOrderSr, J).Value = AgL.VNull(.Rows(I)("WorkOrderSr"))
                        Dgl1.Item(Col1SalesTaxGroup, J).Tag = AgL.XNull(.Rows(I)("SalesTaxGroupItem"))
                        Dgl1.Item(Col1SalesTaxGroup, J).Value = AgL.XNull(.Rows(I)("SalesTaxGroupItem"))
                        Dgl1.Item(Col1BillingType, J).Value = AgL.XNull(.Rows(I)("BillingType"))
                        Dgl1.Item(Col1RateType, J).Value = AgL.XNull(.Rows(I)("RateType"))
                        Dgl1.Item(Col1Specification, J).Value = AgL.XNull(.Rows(I)("Specification"))


                        Dgl1.Item(Col1Unit, J).Value = AgL.XNull(.Rows(I)("Unit"))
                        Dgl1.Item(Col1QtyDecimalPlaces, J).Value = AgL.VNull(.Rows(I)("QtyDecimalPlaces"))
                        Dgl1.Item(Col1MeasurePerPcs, J).Value = Format(AgL.VNull(.Rows(I)("MeasurePerPcs")), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                        Dgl1.Item(Col1Qty, J).Value = Format(AgL.VNull(.Rows(I)("Qty")), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))

                        Dgl1.Item(Col1MeasureUnit, J).Value = AgL.XNull(.Rows(I)("MeasureUnit"))
                        Dgl1.Item(Col1DeliveryMeasure, I).Value = AgL.XNull(.Rows(I)("DeliveryMeasure"))
                        Dgl1.Item(Col1MeasureDecimalPlaces, J).Value = AgL.VNull(.Rows(I)("MeasureDecimalPlaces"))
                        Dgl1.Item(Col1DeliveryMeasurePerPcs, J).Value = AgL.VNull(.Rows(I)("DeliveryMeasurePerPcs"))
                        Dgl1.Item(Col1DeliveryMeasureMultiplier, J).Value = AgL.VNull(.Rows(I)("DeliveryMeasureMultiplier"))
                        Dgl1.Item(Col1TotalDeliveryMeasure, I).Value = Format(AgL.VNull(.Rows(I)("TotalDeliveryMeasure")), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))

                        If FillForBalanceQty Then
                            Dgl1.Item(Col1DocQty, J).Value = -AgL.VNull(.Rows(I)("Qty"))
                            Dgl1.Item(Col1TotalDocDeliveryMeasure, I).Value = -AgL.VNull(.Rows(I)("TotalDeliveryMeasure"))
                        Else
                            Dgl1.Item(Col1DocQty, J).Value = AgL.VNull(.Rows(I)("Qty"))
                            Dgl1.Item(Col1TotalDocDeliveryMeasure, I).Value = AgL.VNull(.Rows(I)("TotalDeliveryMeasure"))
                        End If

                        AgCalcGrid1.FCopyStructureLine(AgL.XNull(.Rows(I)("WorkOrder")), Dgl1, I, AgL.VNull(.Rows(I)("WorkOrderSr")))
                        CType(Dgl1.Columns(Col1Qty), AgControls.AgTextColumn).AgNumberRightPlaces = Val(Dgl1.Item(Col1QtyDecimalPlaces, Dgl1.CurrentCell.RowIndex).Value)
                        CType(Dgl1.Columns(Col1TotalMeasure), AgControls.AgTextColumn).AgNumberRightPlaces = Val(Dgl1.Item(Col1MeasureDecimalPlaces, Dgl1.CurrentCell.RowIndex).Value)

                        If TxtV_Nature.Text = "Rate Amendment" Then
                            Dgl1.Columns(Col1RateAmendment).ReadOnly = False
                            Dgl1.Columns(Col1RateAmendment).Visible = True
                            Dgl1.Columns(Col1RateOrder).Visible = True
                            Dgl1.Columns(Col1TotalDeliveryMeasure).ReadOnly = True
                            Dgl1.Columns(Col1TotalDeliveryMeasure).Visible = False
                            Dgl1.Item(Col1RateOrder, J).Value = AgL.VNull(Dgl1.AgDataRow.Cells("Rate").Value)
                        Else
                            Dgl1.Columns(Col1RateAmendment).ReadOnly = True
                            Dgl1.Columns(Col1RateAmendment).Visible = False
                            Dgl1.Columns(Col1RateOrder).Visible = False
                            Dgl1.Columns(Col1TotalDeliveryMeasure).ReadOnly = False
                            Dgl1.Columns(Col1TotalDeliveryMeasure).Visible = True
                            Dgl1.Item(Col1Rate, J).Value = AgL.VNull(.Rows(I)("Rate"))
                        End If

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
                LineConStr += " And CharIndex('|' + I.ItemType + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")) & "') > 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemGroup")) <> "" Then
                LineConStr += " And CharIndex('|' + I.ItemGroup + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemGroup")) & "') > 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_ItemGroup")) <> "" Then
                LineConStr += " And CharIndex('|' + I.ItemGroup + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_ItemGroup")) & "') <= 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Item")) <> "" Then
                LineConStr += " And CharIndex('|' + I.Code + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Item")) & "') > 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_Item")) <> "" Then
                LineConStr += " And CharIndex('|' + I.Code + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_Item")) & "') <= 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemDivision")) <> "" Then
                LineConStr += " And CharIndex('|' + I.Div_Code + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemDivision")) & "') > 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemSite")) <> "" Then
                LineConStr += " And CharIndex('|' + I.Site_Code + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemSite")) & "') > 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ContraV_Type")) <> "" Then
                HeaderConStr += " And CharIndex('|' + V_Type + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ContraV_Type")) & "') > 0 "
            End If
        End If

        FRetFillItemWiseQry = "  SELECT Max(H.V_Type) + '-' +  Max(H.ManualrefNo) AS WorkOrderNo,  Max(H.V_Date) As WorkOrderDate, max(L.BillingType) AS BillingType , max(L.RateType) AS RateType , " &
                            " L.WorkOrder, L.WorkOrderSr,    Max(L.Item) As Item, Max(I.Description) AS ItemDesc, max(I.ManualCode) AS ItemCode,  IfNull(Sum(L.Qty),0) As Qty,  " &
                            " Max(L.Unit) As Unit, Max(L.MeasurePerPcs) As MeasurePerPcs,  Max(L.MeasureUnit) As MeasureUnit, Max(L.SalesTaxGroupItem) AS SalesTaxGroupItem, Max(L.Specification) AS Specification, " &
                            " Max(U.DecimalPlaces) AS QtyDecimalPlaces, Max(MU.DecimalPlaces) AS MeasureDecimalPlaces , max(L.Rate) AS Rate , Max(L.DeliveryMeasure) As DeliveryMeasure," &
                            " max(L.DeliveryMeasureMultiplier) AS DeliveryMeasureMultiplier, Sum(TotalDeliveryMeasure) AS TotalDeliveryMeasure, max(DeliveryMeasurePerPcs) AS DeliveryMeasurePerPcs " &
                            " FROM (  " &
                            " SELECT DocID, V_Type, ManualrefNo , V_Date  " &
                            " FROM WorkOrder  Where 1=1 " & HeaderConStr & " " &
                            " ) As H  " &
                            " LEFT JOIN WorkOrderDetail L  ON H.DocID = L.WorkOrder  " &
                            " LEFT JOIN Item I On L.Item = I.Code   " &
                            " Left Join Unit U On L.Unit = U.Code   " &
                            " Left Join Unit MU On L.MeasureUnit = MU.Code   " &
                            " WHERE 1 = 1 " & LineConStr &
                            " GROUP BY L.WorkOrder, L.WorkOrderSr   "

        If FillForBalanceQty Then FRetFillItemWiseQry += " HAVING IfNull(Sum(L.Qty),0) > 0 "
    End Function
End Class
