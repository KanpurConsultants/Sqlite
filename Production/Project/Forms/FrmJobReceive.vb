Imports System.IO
Imports System.Data.SQLite
Imports CrystalDecisions.CrystalReports.Engine
Public Class FrmJobReceive
    Inherits AgTemplate.TempTransaction
    Dim mQry$

    Public WithEvents AgCalcGrid1 As New AgStructure.AgCalcGrid
    Public WithEvents AgCustomGrid1 As New AgCustomFields.AgCustomGrid

    Protected WithEvents Dgl1 As New AgControls.AgDataGrid
    Protected Const ColSNo As String = "S.No."
    Protected Const Col1Item_Uid As String = "Item_Uid"
    Protected Const Col1Item As String = "Item"
    Protected Const Col1StockItem As String = "Stock Item"
    Protected Const Col1ItemGroup As String = "Item Group"

    Public Const Col1Dimension1 As String = "Dimension1"
    Public Const Col1Dimension2 As String = "Dimension2"

    Protected Const Col1JobOrder As String = "Job Order"
    Protected Const Col1JobOrderSr As String = "Job Order Sr"
    Protected Const Col1ProdOrder As String = "Prod Order"
    Protected Const Col1ProdOrderSr As String = "Prod Order Sr"
    Protected Const Col1LotNo As String = "Lot No"
    Protected Const Col1JobOrderLotNo As String = "JobOrder LotNo"
    Protected Const Col1DocQty As String = "Doc. Qty"
    Protected Const Col1Qty As String = "Qty"
    Protected Const Col1BillQty As String = "Bill Qty"
    Protected Const Col1RejQty As String = "Rej. Qty"
    Protected Const Col1LossPer As String = "Loss %"
    Protected Const Col1LossQty As String = "Loss Qty"
    Protected Const Col1Unit As String = "Unit"
    Protected Const Col1QtyDecimalPlaces As String = "Qty Decimal Places"
    Protected Const Col1MeasurePerPcs As String = "Measure Per Pcs"
    Protected Const Col1DocMeasure As String = "Doc. Measure"
    Protected Const Col1TotalMeasure As String = "Total Measure"
    Protected Const Col1BillMeasure As String = "Bill Measure"
    Protected Const Col1RetMeasure As String = "Ret.Measure"
    Protected Const Col1MeasureUnit As String = "Measure Unit"
    Protected Const Col1MeasureDecimalPlaces As String = "Measure Decimal Places"
    Protected Const Col1Machine As String = "Machine"
    Protected Const Col1Rate As String = "Rate"
    Protected Const Col1Amount As String = "Amount"
    Protected Const Col1Remark As String = "Remark"

    Dim ImportMessegeStr$ = ""

    Dim IsSameUnit As Boolean = True
    Dim IsSameMeasureUnit As Boolean = True
    Dim IsSameDeliveryMeasureUnit As Boolean = True

    Dim intQtyDecimalPlaces As Integer = 0
    Dim intMeasureDecimalPlaces As Integer = 0
    Dim intDeliveryMeasureDecimalPlaces As Integer = 0

    Dim ImportMode As Boolean = False
    Dim ImportAction_NewImport As String = "New Import"
    Dim ImportAction_ClearImport As String = "Clear Import"
    Dim isRecordLocked As Boolean
    Dim DtJobEnviro As DataTable = Nothing
    Protected WithEvents TxtPartyDocNo As AgControls.AgTextBox
    Protected WithEvents Label3 As System.Windows.Forms.Label
    Protected WithEvents BtnByProduct As System.Windows.Forms.Button
    Protected WithEvents TxtMachine As AgControls.AgTextBox
    Protected WithEvents LblMachine As System.Windows.Forms.Label

    Dim mMeasureField$ = ""

    Public Sub New(ByVal StrUPVar As String, ByVal DTUP As DataTable, ByVal strNCat As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Topctrl1.FSetParent(Me, StrUPVar, DTUP)
        Topctrl1.SetDisp(True)

        EntryNCat = strNCat

        mQry = "Select H.* from Voucher_Type_Settings H Left Join Voucher_Type Vt On H.V_Type = Vt.V_Type  Where Vt.NCat In ('" & EntryNCat & "') And H.Div_Code = '" & AgL.PubDivCode & "' And H.Site_Code ='" & AgL.PubSiteCode & "' "
        DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
        DtJobEnviro = AgL.FillData("SELECT H.* FROM JobEnviro H Left Join Voucher_Type Vt On H.V_Type = Vt.V_Type Where Vt.NCat In ('" & EntryNCat & "') And H.Div_Code = '" & AgL.PubDivCode & "' And H.Site_Code ='" & AgL.PubSiteCode & "'", AgL.GCn).Tables(0)

    End Sub

#Region "Form Designer Code"
    Private Sub InitializeComponent()
        Me.Dgl1 = New AgControls.AgDataGrid
        Me.TxtGodown = New AgControls.AgTextBox
        Me.LblGodown = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.LblTotalAmount = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.LblTotalMeasure = New System.Windows.Forms.Label
        Me.LblTotalMeasureText = New System.Windows.Forms.Label
        Me.LblTotalQty = New System.Windows.Forms.Label
        Me.LblTotalQtyText = New System.Windows.Forms.Label
        Me.Pnl1 = New System.Windows.Forms.Panel
        Me.TxtRemarks = New AgControls.AgTextBox
        Me.LblGodownReq = New System.Windows.Forms.Label
        Me.TxtManualRefNo = New AgControls.AgTextBox
        Me.LblManualRefNo = New System.Windows.Forms.Label
        Me.LblJobWorkerReq = New System.Windows.Forms.Label
        Me.TxtJobWorker = New AgControls.AgTextBox
        Me.LblJobWorker = New System.Windows.Forms.Label
        Me.TxtProcess = New AgControls.AgTextBox
        Me.LblProcess = New System.Windows.Forms.Label
        Me.LblJobReceiveDetail = New System.Windows.Forms.LinkLabel
        Me.TxtBillingOn = New AgControls.AgTextBox
        Me.LblRemark1 = New System.Windows.Forms.Label
        Me.LblManualRefNoReq = New System.Windows.Forms.Label
        Me.PnlCalcGrid = New System.Windows.Forms.Panel
        Me.Label1 = New System.Windows.Forms.Label
        Me.TxtJobReceiveBy = New AgControls.AgTextBox
        Me.TxtStructure = New AgControls.AgTextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.BtnFillJobOrder = New System.Windows.Forms.Button
        Me.ChkShowOnlyImportedRecords = New System.Windows.Forms.CheckBox
        Me.BtnImprtFromText = New System.Windows.Forms.Button
        Me.BtnConsumptionDetail = New System.Windows.Forms.Button
        Me.TxtCustomFields = New AgControls.AgTextBox
        Me.PnlCustomGrid = New System.Windows.Forms.Panel
        Me.GrpDirectChallan = New System.Windows.Forms.GroupBox
        Me.RbtForJobOrder = New System.Windows.Forms.RadioButton
        Me.RbtForJobOrderItems = New System.Windows.Forms.RadioButton
        Me.TxtPartyDocNo = New AgControls.AgTextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.BtnByProduct = New System.Windows.Forms.Button
        Me.TxtMachine = New AgControls.AgTextBox
        Me.LblMachine = New System.Windows.Forms.Label
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
        Me.GroupBox2.Location = New System.Drawing.Point(794, 575)
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
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(620, 575)
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
        Me.GBoxApprove.Location = New System.Drawing.Point(446, 575)
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
        Me.GBoxEntryType.Location = New System.Drawing.Point(161, 575)
        Me.GBoxEntryType.Size = New System.Drawing.Size(119, 40)
        '
        'TxtEntryType
        '
        Me.TxtEntryType.Location = New System.Drawing.Point(3, 19)
        Me.TxtEntryType.Tag = ""
        '
        'GrpUP
        '
        Me.GrpUP.Location = New System.Drawing.Point(16, 575)
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
        Me.GroupBox1.Location = New System.Drawing.Point(2, 571)
        Me.GroupBox1.Size = New System.Drawing.Size(983, 4)
        '
        'GBoxDivision
        '
        Me.GBoxDivision.Location = New System.Drawing.Point(306, 575)
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
        Me.LblV_No.Location = New System.Drawing.Point(16, 108)
        Me.LblV_No.Size = New System.Drawing.Size(101, 16)
        Me.LblV_No.Tag = ""
        Me.LblV_No.Text = "Job Receive No."
        Me.LblV_No.Visible = False
        '
        'TxtV_No
        '
        Me.TxtV_No.AgSelectedValue = ""
        Me.TxtV_No.BackColor = System.Drawing.Color.White
        Me.TxtV_No.Location = New System.Drawing.Point(141, 107)
        Me.TxtV_No.Size = New System.Drawing.Size(125, 18)
        Me.TxtV_No.TabIndex = 3
        Me.TxtV_No.Tag = ""
        Me.TxtV_No.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.TxtV_No.Visible = False
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(108, 39)
        Me.Label2.Tag = ""
        '
        'LblV_Date
        '
        Me.LblV_Date.BackColor = System.Drawing.Color.Transparent
        Me.LblV_Date.Location = New System.Drawing.Point(19, 34)
        Me.LblV_Date.Size = New System.Drawing.Size(84, 16)
        Me.LblV_Date.Tag = ""
        Me.LblV_Date.Text = "Receive Date"
        '
        'LblV_TypeReq
        '
        Me.LblV_TypeReq.Location = New System.Drawing.Point(302, 19)
        Me.LblV_TypeReq.Tag = ""
        '
        'TxtV_Date
        '
        Me.TxtV_Date.AgSelectedValue = ""
        Me.TxtV_Date.BackColor = System.Drawing.Color.White
        Me.TxtV_Date.Location = New System.Drawing.Point(124, 33)
        Me.TxtV_Date.Size = New System.Drawing.Size(86, 18)
        Me.TxtV_Date.TabIndex = 2
        Me.TxtV_Date.Tag = ""
        '
        'LblV_Type
        '
        Me.LblV_Type.Location = New System.Drawing.Point(216, 15)
        Me.LblV_Type.Size = New System.Drawing.Size(84, 16)
        Me.LblV_Type.Tag = ""
        Me.LblV_Type.Text = "Receive Type"
        '
        'TxtV_Type
        '
        Me.TxtV_Type.AgSelectedValue = ""
        Me.TxtV_Type.BackColor = System.Drawing.Color.White
        Me.TxtV_Type.Location = New System.Drawing.Point(322, 13)
        Me.TxtV_Type.Size = New System.Drawing.Size(143, 18)
        Me.TxtV_Type.TabIndex = 1
        Me.TxtV_Type.Tag = ""
        '
        'LblSite_CodeReq
        '
        Me.LblSite_CodeReq.Location = New System.Drawing.Point(108, 19)
        Me.LblSite_CodeReq.Tag = ""
        '
        'LblSite_Code
        '
        Me.LblSite_Code.BackColor = System.Drawing.Color.Transparent
        Me.LblSite_Code.Location = New System.Drawing.Point(19, 14)
        Me.LblSite_Code.Size = New System.Drawing.Size(87, 16)
        Me.LblSite_Code.Tag = ""
        Me.LblSite_Code.Text = "Branch Name"
        '
        'TxtSite_Code
        '
        Me.TxtSite_Code.AgSelectedValue = ""
        Me.TxtSite_Code.BackColor = System.Drawing.Color.White
        Me.TxtSite_Code.Location = New System.Drawing.Point(124, 13)
        Me.TxtSite_Code.Size = New System.Drawing.Size(86, 18)
        Me.TxtSite_Code.TabIndex = 0
        Me.TxtSite_Code.Tag = ""
        '
        'LblDocId
        '
        Me.LblDocId.Tag = ""
        '
        'LblPrefix
        '
        Me.LblPrefix.Location = New System.Drawing.Point(788, 138)
        Me.LblPrefix.Tag = ""
        Me.LblPrefix.Visible = False
        '
        'TabControl1
        '
        Me.TabControl1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl1.Location = New System.Drawing.Point(-4, 18)
        Me.TabControl1.Size = New System.Drawing.Size(970, 123)
        Me.TabControl1.TabIndex = 0
        '
        'TP1
        '
        Me.TP1.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.TP1.Controls.Add(Me.TxtMachine)
        Me.TP1.Controls.Add(Me.LblMachine)
        Me.TP1.Controls.Add(Me.TxtPartyDocNo)
        Me.TP1.Controls.Add(Me.Label3)
        Me.TP1.Controls.Add(Me.Label5)
        Me.TP1.Controls.Add(Me.Label4)
        Me.TP1.Controls.Add(Me.TxtStructure)
        Me.TP1.Controls.Add(Me.Label1)
        Me.TP1.Controls.Add(Me.TxtJobReceiveBy)
        Me.TP1.Controls.Add(Me.LblManualRefNoReq)
        Me.TP1.Controls.Add(Me.LblRemark1)
        Me.TP1.Controls.Add(Me.TxtBillingOn)
        Me.TP1.Controls.Add(Me.TxtRemarks)
        Me.TP1.Controls.Add(Me.TxtManualRefNo)
        Me.TP1.Controls.Add(Me.LblManualRefNo)
        Me.TP1.Controls.Add(Me.TxtGodown)
        Me.TP1.Controls.Add(Me.LblGodownReq)
        Me.TP1.Controls.Add(Me.LblGodown)
        Me.TP1.Controls.Add(Me.TxtJobWorker)
        Me.TP1.Controls.Add(Me.LblJobWorker)
        Me.TP1.Controls.Add(Me.LblJobWorkerReq)
        Me.TP1.Controls.Add(Me.TxtProcess)
        Me.TP1.Controls.Add(Me.LblProcess)
        Me.TP1.Location = New System.Drawing.Point(4, 22)
        Me.TP1.Size = New System.Drawing.Size(962, 97)
        Me.TP1.Text = "Document Detail"
        Me.TP1.Controls.SetChildIndex(Me.LblProcess, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtProcess, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblPrefix, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblJobWorkerReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblJobWorker, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtJobWorker, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblGodown, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblGodownReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtGodown, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblManualRefNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtManualRefNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtV_No, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtRemarks, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtSite_Code, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_Date, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label2, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_No, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblSite_Code, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtV_Date, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblSite_CodeReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_Type, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_TypeReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtV_Type, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblDocId, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtDocId, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtBillingOn, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblRemark1, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblManualRefNoReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtJobReceiveBy, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label1, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtStructure, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label4, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label5, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label3, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtPartyDocNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblMachine, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtMachine, 0)
        '
        'Topctrl1
        '
        Me.Topctrl1.Size = New System.Drawing.Size(965, 41)
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
        'TxtGodown
        '
        Me.TxtGodown.AgAllowUserToEnableMasterHelp = False
        Me.TxtGodown.AgLastValueTag = Nothing
        Me.TxtGodown.AgLastValueText = Nothing
        Me.TxtGodown.AgMandatory = True
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
        Me.TxtGodown.Location = New System.Drawing.Point(583, 53)
        Me.TxtGodown.MaxLength = 20
        Me.TxtGodown.Name = "TxtGodown"
        Me.TxtGodown.Size = New System.Drawing.Size(169, 18)
        Me.TxtGodown.TabIndex = 8
        '
        'LblGodown
        '
        Me.LblGodown.AutoSize = True
        Me.LblGodown.BackColor = System.Drawing.Color.Transparent
        Me.LblGodown.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblGodown.Location = New System.Drawing.Point(474, 54)
        Me.LblGodown.Name = "LblGodown"
        Me.LblGodown.Size = New System.Drawing.Size(55, 16)
        Me.LblGodown.TabIndex = 706
        Me.LblGodown.Text = "Godown"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Cornsilk
        Me.Panel1.Controls.Add(Me.LblTotalAmount)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.LblTotalMeasure)
        Me.Panel1.Controls.Add(Me.LblTotalMeasureText)
        Me.Panel1.Controls.Add(Me.LblTotalQty)
        Me.Panel1.Controls.Add(Me.LblTotalQtyText)
        Me.Panel1.Location = New System.Drawing.Point(1, 397)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(961, 23)
        Me.Panel1.TabIndex = 694
        '
        'LblTotalAmount
        '
        Me.LblTotalAmount.AutoSize = True
        Me.LblTotalAmount.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalAmount.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalAmount.Location = New System.Drawing.Point(832, 3)
        Me.LblTotalAmount.Name = "LblTotalAmount"
        Me.LblTotalAmount.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalAmount.TabIndex = 668
        Me.LblTotalAmount.Text = "."
        Me.LblTotalAmount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Maroon
        Me.Label6.Location = New System.Drawing.Point(719, 3)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(100, 16)
        Me.Label6.TabIndex = 667
        Me.Label6.Text = "Total Amount :"
        '
        'LblTotalMeasure
        '
        Me.LblTotalMeasure.AutoSize = True
        Me.LblTotalMeasure.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalMeasure.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalMeasure.Location = New System.Drawing.Point(452, 3)
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
        Me.LblTotalMeasureText.Location = New System.Drawing.Point(340, 3)
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
        Me.Pnl1.Location = New System.Drawing.Point(1, 168)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(962, 228)
        Me.Pnl1.TabIndex = 1
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
        Me.TxtRemarks.Location = New System.Drawing.Point(583, 73)
        Me.TxtRemarks.MaxLength = 255
        Me.TxtRemarks.Multiline = True
        Me.TxtRemarks.Name = "TxtRemarks"
        Me.TxtRemarks.Size = New System.Drawing.Size(356, 18)
        Me.TxtRemarks.TabIndex = 10
        '
        'LblGodownReq
        '
        Me.LblGodownReq.AutoSize = True
        Me.LblGodownReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblGodownReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblGodownReq.Location = New System.Drawing.Point(570, 59)
        Me.LblGodownReq.Name = "LblGodownReq"
        Me.LblGodownReq.Size = New System.Drawing.Size(10, 7)
        Me.LblGodownReq.TabIndex = 724
        Me.LblGodownReq.Text = "Ä"
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
        Me.TxtManualRefNo.Location = New System.Drawing.Point(322, 33)
        Me.TxtManualRefNo.MaxLength = 50
        Me.TxtManualRefNo.Name = "TxtManualRefNo"
        Me.TxtManualRefNo.Size = New System.Drawing.Size(143, 18)
        Me.TxtManualRefNo.TabIndex = 3
        '
        'LblManualRefNo
        '
        Me.LblManualRefNo.AutoSize = True
        Me.LblManualRefNo.BackColor = System.Drawing.Color.Transparent
        Me.LblManualRefNo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblManualRefNo.Location = New System.Drawing.Point(216, 33)
        Me.LblManualRefNo.Name = "LblManualRefNo"
        Me.LblManualRefNo.Size = New System.Drawing.Size(77, 16)
        Me.LblManualRefNo.TabIndex = 726
        Me.LblManualRefNo.Text = "Receive No."
        '
        'LblJobWorkerReq
        '
        Me.LblJobWorkerReq.AutoSize = True
        Me.LblJobWorkerReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblJobWorkerReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblJobWorkerReq.Location = New System.Drawing.Point(108, 77)
        Me.LblJobWorkerReq.Name = "LblJobWorkerReq"
        Me.LblJobWorkerReq.Size = New System.Drawing.Size(10, 7)
        Me.LblJobWorkerReq.TabIndex = 735
        Me.LblJobWorkerReq.Text = "Ä"
        '
        'TxtJobWorker
        '
        Me.TxtJobWorker.AgAllowUserToEnableMasterHelp = False
        Me.TxtJobWorker.AgLastValueTag = Nothing
        Me.TxtJobWorker.AgLastValueText = Nothing
        Me.TxtJobWorker.AgMandatory = True
        Me.TxtJobWorker.AgMasterHelp = False
        Me.TxtJobWorker.AgNumberLeftPlaces = 8
        Me.TxtJobWorker.AgNumberNegetiveAllow = False
        Me.TxtJobWorker.AgNumberRightPlaces = 2
        Me.TxtJobWorker.AgPickFromLastValue = False
        Me.TxtJobWorker.AgRowFilter = ""
        Me.TxtJobWorker.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtJobWorker.AgSelectedValue = Nothing
        Me.TxtJobWorker.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtJobWorker.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtJobWorker.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtJobWorker.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtJobWorker.Location = New System.Drawing.Point(124, 73)
        Me.TxtJobWorker.MaxLength = 20
        Me.TxtJobWorker.Name = "TxtJobWorker"
        Me.TxtJobWorker.Size = New System.Drawing.Size(341, 18)
        Me.TxtJobWorker.TabIndex = 5
        '
        'LblJobWorker
        '
        Me.LblJobWorker.AutoSize = True
        Me.LblJobWorker.BackColor = System.Drawing.Color.Transparent
        Me.LblJobWorker.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblJobWorker.Location = New System.Drawing.Point(19, 73)
        Me.LblJobWorker.Name = "LblJobWorker"
        Me.LblJobWorker.Size = New System.Drawing.Size(74, 16)
        Me.LblJobWorker.TabIndex = 734
        Me.LblJobWorker.Text = "Job Worker"
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
        Me.TxtProcess.Location = New System.Drawing.Point(124, 53)
        Me.TxtProcess.MaxLength = 20
        Me.TxtProcess.Name = "TxtProcess"
        Me.TxtProcess.Size = New System.Drawing.Size(341, 18)
        Me.TxtProcess.TabIndex = 4
        '
        'LblProcess
        '
        Me.LblProcess.AutoSize = True
        Me.LblProcess.BackColor = System.Drawing.Color.Transparent
        Me.LblProcess.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblProcess.Location = New System.Drawing.Point(19, 54)
        Me.LblProcess.Name = "LblProcess"
        Me.LblProcess.Size = New System.Drawing.Size(56, 16)
        Me.LblProcess.TabIndex = 737
        Me.LblProcess.Text = "Process"
        '
        'LblJobReceiveDetail
        '
        Me.LblJobReceiveDetail.BackColor = System.Drawing.Color.SteelBlue
        Me.LblJobReceiveDetail.DisabledLinkColor = System.Drawing.Color.White
        Me.LblJobReceiveDetail.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblJobReceiveDetail.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LblJobReceiveDetail.LinkColor = System.Drawing.Color.White
        Me.LblJobReceiveDetail.Location = New System.Drawing.Point(-1, 147)
        Me.LblJobReceiveDetail.Name = "LblJobReceiveDetail"
        Me.LblJobReceiveDetail.Size = New System.Drawing.Size(123, 20)
        Me.LblJobReceiveDetail.TabIndex = 733
        Me.LblJobReceiveDetail.TabStop = True
        Me.LblJobReceiveDetail.Text = "Job Receive Detail"
        Me.LblJobReceiveDetail.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtBillingOn
        '
        Me.TxtBillingOn.AgAllowUserToEnableMasterHelp = False
        Me.TxtBillingOn.AgLastValueTag = Nothing
        Me.TxtBillingOn.AgLastValueText = Nothing
        Me.TxtBillingOn.AgMandatory = False
        Me.TxtBillingOn.AgMasterHelp = False
        Me.TxtBillingOn.AgNumberLeftPlaces = 8
        Me.TxtBillingOn.AgNumberNegetiveAllow = False
        Me.TxtBillingOn.AgNumberRightPlaces = 2
        Me.TxtBillingOn.AgPickFromLastValue = False
        Me.TxtBillingOn.AgRowFilter = ""
        Me.TxtBillingOn.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtBillingOn.AgSelectedValue = Nothing
        Me.TxtBillingOn.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtBillingOn.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtBillingOn.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtBillingOn.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtBillingOn.Location = New System.Drawing.Point(855, 139)
        Me.TxtBillingOn.MaxLength = 20
        Me.TxtBillingOn.Name = "TxtBillingOn"
        Me.TxtBillingOn.Size = New System.Drawing.Size(84, 18)
        Me.TxtBillingOn.TabIndex = 744
        Me.TxtBillingOn.Visible = False
        '
        'LblRemark1
        '
        Me.LblRemark1.AutoSize = True
        Me.LblRemark1.BackColor = System.Drawing.Color.Transparent
        Me.LblRemark1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblRemark1.Location = New System.Drawing.Point(474, 73)
        Me.LblRemark1.Name = "LblRemark1"
        Me.LblRemark1.Size = New System.Drawing.Size(60, 16)
        Me.LblRemark1.TabIndex = 745
        Me.LblRemark1.Text = "Remarks"
        '
        'LblManualRefNoReq
        '
        Me.LblManualRefNoReq.AutoSize = True
        Me.LblManualRefNoReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblManualRefNoReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblManualRefNoReq.Location = New System.Drawing.Point(302, 40)
        Me.LblManualRefNoReq.Name = "LblManualRefNoReq"
        Me.LblManualRefNoReq.Size = New System.Drawing.Size(10, 7)
        Me.LblManualRefNoReq.TabIndex = 746
        Me.LblManualRefNoReq.Text = "Ä"
        '
        'PnlCalcGrid
        '
        Me.PnlCalcGrid.Location = New System.Drawing.Point(652, 426)
        Me.PnlCalcGrid.Name = "PnlCalcGrid"
        Me.PnlCalcGrid.Size = New System.Drawing.Size(310, 135)
        Me.PnlCalcGrid.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(474, 34)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(97, 16)
        Me.Label1.TabIndex = 750
        Me.Label1.Text = "Job Receive By"
        '
        'TxtJobReceiveBy
        '
        Me.TxtJobReceiveBy.AgAllowUserToEnableMasterHelp = False
        Me.TxtJobReceiveBy.AgLastValueTag = Nothing
        Me.TxtJobReceiveBy.AgLastValueText = Nothing
        Me.TxtJobReceiveBy.AgMandatory = False
        Me.TxtJobReceiveBy.AgMasterHelp = False
        Me.TxtJobReceiveBy.AgNumberLeftPlaces = 0
        Me.TxtJobReceiveBy.AgNumberNegetiveAllow = False
        Me.TxtJobReceiveBy.AgNumberRightPlaces = 0
        Me.TxtJobReceiveBy.AgPickFromLastValue = False
        Me.TxtJobReceiveBy.AgRowFilter = ""
        Me.TxtJobReceiveBy.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtJobReceiveBy.AgSelectedValue = Nothing
        Me.TxtJobReceiveBy.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtJobReceiveBy.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtJobReceiveBy.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtJobReceiveBy.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtJobReceiveBy.Location = New System.Drawing.Point(583, 33)
        Me.TxtJobReceiveBy.MaxLength = 255
        Me.TxtJobReceiveBy.Name = "TxtJobReceiveBy"
        Me.TxtJobReceiveBy.Size = New System.Drawing.Size(356, 18)
        Me.TxtJobReceiveBy.TabIndex = 7
        '
        'TxtStructure
        '
        Me.TxtStructure.AgAllowUserToEnableMasterHelp = False
        Me.TxtStructure.AgLastValueTag = Nothing
        Me.TxtStructure.AgLastValueText = Nothing
        Me.TxtStructure.AgMandatory = True
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
        Me.TxtStructure.Location = New System.Drawing.Point(855, 163)
        Me.TxtStructure.MaxLength = 20
        Me.TxtStructure.Name = "TxtStructure"
        Me.TxtStructure.Size = New System.Drawing.Size(142, 18)
        Me.TxtStructure.TabIndex = 763
        Me.TxtStructure.Text = "TxtStructure"
        Me.TxtStructure.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(107, 57)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(10, 7)
        Me.Label4.TabIndex = 764
        Me.Label4.Text = "Ä"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(570, 40)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(10, 7)
        Me.Label5.TabIndex = 765
        Me.Label5.Text = "Ä"
        '
        'BtnFillJobOrder
        '
        Me.BtnFillJobOrder.BackColor = System.Drawing.Color.Transparent
        Me.BtnFillJobOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnFillJobOrder.Font = New System.Drawing.Font("Lucida Console", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnFillJobOrder.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnFillJobOrder.Location = New System.Drawing.Point(411, 143)
        Me.BtnFillJobOrder.Margin = New System.Windows.Forms.Padding(0)
        Me.BtnFillJobOrder.Name = "BtnFillJobOrder"
        Me.BtnFillJobOrder.Size = New System.Drawing.Size(36, 24)
        Me.BtnFillJobOrder.TabIndex = 1
        Me.BtnFillJobOrder.Text = "..."
        Me.BtnFillJobOrder.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnFillJobOrder.UseVisualStyleBackColor = False
        '
        'ChkShowOnlyImportedRecords
        '
        Me.ChkShowOnlyImportedRecords.AutoSize = True
        Me.ChkShowOnlyImportedRecords.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkShowOnlyImportedRecords.Location = New System.Drawing.Point(649, 148)
        Me.ChkShowOnlyImportedRecords.Name = "ChkShowOnlyImportedRecords"
        Me.ChkShowOnlyImportedRecords.Size = New System.Drawing.Size(214, 17)
        Me.ChkShowOnlyImportedRecords.TabIndex = 761
        Me.ChkShowOnlyImportedRecords.Text = "Show Only Imported Records"
        Me.ChkShowOnlyImportedRecords.UseVisualStyleBackColor = True
        '
        'BtnImprtFromText
        '
        Me.BtnImprtFromText.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnImprtFromText.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnImprtFromText.Location = New System.Drawing.Point(867, 142)
        Me.BtnImprtFromText.Name = "BtnImprtFromText"
        Me.BtnImprtFromText.Size = New System.Drawing.Size(93, 25)
        Me.BtnImprtFromText.TabIndex = 763
        Me.BtnImprtFromText.TabStop = False
        Me.BtnImprtFromText.Text = "New Import"
        Me.BtnImprtFromText.UseVisualStyleBackColor = True
        '
        'BtnConsumptionDetail
        '
        Me.BtnConsumptionDetail.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnConsumptionDetail.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnConsumptionDetail.Location = New System.Drawing.Point(450, 143)
        Me.BtnConsumptionDetail.Name = "BtnConsumptionDetail"
        Me.BtnConsumptionDetail.Size = New System.Drawing.Size(101, 25)
        Me.BtnConsumptionDetail.TabIndex = 764
        Me.BtnConsumptionDetail.TabStop = False
        Me.BtnConsumptionDetail.Text = "Consumption"
        Me.BtnConsumptionDetail.UseVisualStyleBackColor = True
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
        Me.TxtCustomFields.Location = New System.Drawing.Point(22, 547)
        Me.TxtCustomFields.MaxLength = 20
        Me.TxtCustomFields.Name = "TxtCustomFields"
        Me.TxtCustomFields.Size = New System.Drawing.Size(72, 18)
        Me.TxtCustomFields.TabIndex = 1013
        Me.TxtCustomFields.Text = "AgTextBox1"
        Me.TxtCustomFields.Visible = False
        '
        'PnlCustomGrid
        '
        Me.PnlCustomGrid.Location = New System.Drawing.Point(270, 426)
        Me.PnlCustomGrid.Name = "PnlCustomGrid"
        Me.PnlCustomGrid.Size = New System.Drawing.Size(370, 135)
        Me.PnlCustomGrid.TabIndex = 2
        '
        'GrpDirectChallan
        '
        Me.GrpDirectChallan.BackColor = System.Drawing.Color.Transparent
        Me.GrpDirectChallan.Controls.Add(Me.RbtForJobOrder)
        Me.GrpDirectChallan.Controls.Add(Me.RbtForJobOrderItems)
        Me.GrpDirectChallan.Location = New System.Drawing.Point(124, 140)
        Me.GrpDirectChallan.Name = "GrpDirectChallan"
        Me.GrpDirectChallan.Size = New System.Drawing.Size(283, 25)
        Me.GrpDirectChallan.TabIndex = 3008
        Me.GrpDirectChallan.TabStop = False
        '
        'RbtForJobOrder
        '
        Me.RbtForJobOrder.AutoSize = True
        Me.RbtForJobOrder.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RbtForJobOrder.Location = New System.Drawing.Point(5, 8)
        Me.RbtForJobOrder.Name = "RbtForJobOrder"
        Me.RbtForJobOrder.Size = New System.Drawing.Size(114, 17)
        Me.RbtForJobOrder.TabIndex = 0
        Me.RbtForJobOrder.TabStop = True
        Me.RbtForJobOrder.Text = "For Job Order"
        Me.RbtForJobOrder.UseVisualStyleBackColor = True
        '
        'RbtForJobOrderItems
        '
        Me.RbtForJobOrderItems.AutoSize = True
        Me.RbtForJobOrderItems.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RbtForJobOrderItems.Location = New System.Drawing.Point(125, 7)
        Me.RbtForJobOrderItems.Name = "RbtForJobOrderItems"
        Me.RbtForJobOrderItems.Size = New System.Drawing.Size(156, 17)
        Me.RbtForJobOrderItems.TabIndex = 743
        Me.RbtForJobOrderItems.TabStop = True
        Me.RbtForJobOrderItems.Text = "For Job Order Items"
        Me.RbtForJobOrderItems.UseVisualStyleBackColor = True
        '
        'TxtPartyDocNo
        '
        Me.TxtPartyDocNo.AgAllowUserToEnableMasterHelp = False
        Me.TxtPartyDocNo.AgLastValueTag = Nothing
        Me.TxtPartyDocNo.AgLastValueText = Nothing
        Me.TxtPartyDocNo.AgMandatory = False
        Me.TxtPartyDocNo.AgMasterHelp = False
        Me.TxtPartyDocNo.AgNumberLeftPlaces = 8
        Me.TxtPartyDocNo.AgNumberNegetiveAllow = False
        Me.TxtPartyDocNo.AgNumberRightPlaces = 2
        Me.TxtPartyDocNo.AgPickFromLastValue = False
        Me.TxtPartyDocNo.AgRowFilter = ""
        Me.TxtPartyDocNo.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtPartyDocNo.AgSelectedValue = Nothing
        Me.TxtPartyDocNo.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtPartyDocNo.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtPartyDocNo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtPartyDocNo.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPartyDocNo.Location = New System.Drawing.Point(855, 53)
        Me.TxtPartyDocNo.MaxLength = 20
        Me.TxtPartyDocNo.Name = "TxtPartyDocNo"
        Me.TxtPartyDocNo.Size = New System.Drawing.Size(84, 18)
        Me.TxtPartyDocNo.TabIndex = 9
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(758, 53)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(94, 16)
        Me.Label3.TabIndex = 767
        Me.Label3.Text = "Party Doc. No."
        '
        'BtnByProduct
        '
        Me.BtnByProduct.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnByProduct.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnByProduct.Location = New System.Drawing.Point(555, 143)
        Me.BtnByProduct.Name = "BtnByProduct"
        Me.BtnByProduct.Size = New System.Drawing.Size(87, 25)
        Me.BtnByProduct.TabIndex = 3009
        Me.BtnByProduct.TabStop = False
        Me.BtnByProduct.Text = "By Product"
        Me.BtnByProduct.UseVisualStyleBackColor = True
        '
        'TxtMachine
        '
        Me.TxtMachine.AgAllowUserToEnableMasterHelp = False
        Me.TxtMachine.AgLastValueTag = Nothing
        Me.TxtMachine.AgLastValueText = Nothing
        Me.TxtMachine.AgMandatory = False
        Me.TxtMachine.AgMasterHelp = False
        Me.TxtMachine.AgNumberLeftPlaces = 8
        Me.TxtMachine.AgNumberNegetiveAllow = False
        Me.TxtMachine.AgNumberRightPlaces = 2
        Me.TxtMachine.AgPickFromLastValue = False
        Me.TxtMachine.AgRowFilter = ""
        Me.TxtMachine.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtMachine.AgSelectedValue = Nothing
        Me.TxtMachine.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtMachine.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtMachine.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtMachine.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtMachine.Location = New System.Drawing.Point(583, 13)
        Me.TxtMachine.MaxLength = 20
        Me.TxtMachine.Name = "TxtMachine"
        Me.TxtMachine.Size = New System.Drawing.Size(356, 18)
        Me.TxtMachine.TabIndex = 6
        '
        'LblMachine
        '
        Me.LblMachine.AutoSize = True
        Me.LblMachine.BackColor = System.Drawing.Color.Transparent
        Me.LblMachine.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblMachine.Location = New System.Drawing.Point(474, 13)
        Me.LblMachine.Name = "LblMachine"
        Me.LblMachine.Size = New System.Drawing.Size(57, 16)
        Me.LblMachine.TabIndex = 769
        Me.LblMachine.Text = "Machine"
        '
        'FrmJobReceive
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.BackColor = System.Drawing.SystemColors.ButtonShadow
        Me.ClientSize = New System.Drawing.Size(965, 616)
        Me.Controls.Add(Me.BtnByProduct)
        Me.Controls.Add(Me.GrpDirectChallan)
        Me.Controls.Add(Me.PnlCustomGrid)
        Me.Controls.Add(Me.TxtCustomFields)
        Me.Controls.Add(Me.BtnConsumptionDetail)
        Me.Controls.Add(Me.BtnImprtFromText)
        Me.Controls.Add(Me.ChkShowOnlyImportedRecords)
        Me.Controls.Add(Me.BtnFillJobOrder)
        Me.Controls.Add(Me.LblJobReceiveDetail)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Pnl1)
        Me.Controls.Add(Me.PnlCalcGrid)
        Me.Name = "FrmJobReceive"
        Me.Text = "Template Job Receive"
        Me.Controls.SetChildIndex(Me.TabControl1, 0)
        Me.Controls.SetChildIndex(Me.PnlCalcGrid, 0)
        Me.Controls.SetChildIndex(Me.GBoxMoveToLog, 0)
        Me.Controls.SetChildIndex(Me.Pnl1, 0)
        Me.Controls.SetChildIndex(Me.Panel1, 0)
        Me.Controls.SetChildIndex(Me.LblJobReceiveDetail, 0)
        Me.Controls.SetChildIndex(Me.GBoxApprove, 0)
        Me.Controls.SetChildIndex(Me.Topctrl1, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.GrpUP, 0)
        Me.Controls.SetChildIndex(Me.GBoxEntryType, 0)
        Me.Controls.SetChildIndex(Me.GroupBox2, 0)
        Me.Controls.SetChildIndex(Me.GBoxDivision, 0)
        Me.Controls.SetChildIndex(Me.BtnFillJobOrder, 0)
        Me.Controls.SetChildIndex(Me.ChkShowOnlyImportedRecords, 0)
        Me.Controls.SetChildIndex(Me.BtnImprtFromText, 0)
        Me.Controls.SetChildIndex(Me.BtnConsumptionDetail, 0)
        Me.Controls.SetChildIndex(Me.TxtCustomFields, 0)
        Me.Controls.SetChildIndex(Me.PnlCustomGrid, 0)
        Me.Controls.SetChildIndex(Me.GrpDirectChallan, 0)
        Me.Controls.SetChildIndex(Me.BtnByProduct, 0)
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
    Protected WithEvents TxtGodown As AgControls.AgTextBox
    Protected WithEvents LblGodown As System.Windows.Forms.Label
    Protected WithEvents Panel1 As System.Windows.Forms.Panel
    Protected WithEvents LblTotalQty As System.Windows.Forms.Label
    Protected WithEvents LblTotalQtyText As System.Windows.Forms.Label
    Protected WithEvents Pnl1 As System.Windows.Forms.Panel
    Protected WithEvents LblTotalMeasure As System.Windows.Forms.Label
    Protected WithEvents TxtRemarks As AgControls.AgTextBox
    Protected WithEvents LblGodownReq As System.Windows.Forms.Label
    Protected WithEvents LblTotalMeasureText As System.Windows.Forms.Label
    Protected WithEvents TxtManualRefNo As AgControls.AgTextBox
    Protected WithEvents LblManualRefNo As System.Windows.Forms.Label
    Protected WithEvents TxtProcess As AgControls.AgTextBox
    Protected WithEvents LblProcess As System.Windows.Forms.Label
    Protected WithEvents LblJobWorkerReq As System.Windows.Forms.Label
    Protected WithEvents TxtJobWorker As AgControls.AgTextBox
    Protected WithEvents LblJobWorker As System.Windows.Forms.Label
    Protected WithEvents LblJobReceiveDetail As System.Windows.Forms.LinkLabel
    Protected WithEvents TxtBillingOn As AgControls.AgTextBox
    Protected WithEvents LblRemark1 As System.Windows.Forms.Label
    Protected WithEvents LblManualRefNoReq As System.Windows.Forms.Label
    Protected WithEvents PnlCalcGrid As System.Windows.Forms.Panel
    Protected WithEvents Label1 As System.Windows.Forms.Label
    Protected WithEvents TxtJobReceiveBy As AgControls.AgTextBox
    Protected WithEvents TxtStructure As AgControls.AgTextBox
    Protected WithEvents Label4 As System.Windows.Forms.Label
    Protected WithEvents Label5 As System.Windows.Forms.Label
    Protected WithEvents BtnFillJobOrder As System.Windows.Forms.Button
    Protected WithEvents LblTotalAmount As System.Windows.Forms.Label
    Protected WithEvents Label6 As System.Windows.Forms.Label
    Protected WithEvents ChkShowOnlyImportedRecords As System.Windows.Forms.CheckBox
    Protected WithEvents BtnImprtFromText As System.Windows.Forms.Button
    Protected WithEvents BtnConsumptionDetail As System.Windows.Forms.Button
    Protected WithEvents TxtCustomFields As AgControls.AgTextBox
    Protected WithEvents PnlCustomGrid As System.Windows.Forms.Panel
    Protected WithEvents GrpDirectChallan As System.Windows.Forms.GroupBox
    Protected WithEvents RbtForJobOrder As System.Windows.Forms.RadioButton
    Protected WithEvents RbtForJobOrderItems As System.Windows.Forms.RadioButton
#End Region

    Private Sub Frm_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "JobIssRec"
        LogTableName = "JobIssRec_Log"
        MainLineTableCsv = "JobReceiveDetail"
        LogLineTableCsv = "JobReceiveDetail_Log"
        AgL.GridDesign(Dgl1)

        AgL.AddAgDataGrid(AgCalcGrid1, PnlCalcGrid)
        AgL.AddAgDataGrid(AgCustomGrid1, PnlCustomGrid)

        AgCalcGrid1.AgLibVar = AgL
        AgCalcGrid1.Visible = False

        AgCustomGrid1.AgLibVar = AgL
        AgCustomGrid1.SplitGrid = True
        AgCustomGrid1.MnuText = Me.Name

    End Sub

    Private Sub FrmProductionOrder_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mCondStr$
        mCondStr = " And IFNull(H.IsDeleted,0)=0  " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) &
                        " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "H.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        If ChkShowOnlyImportedRecords.Checked Then
            mCondStr = mCondStr & " And H.EntryStatus = '" & AgTemplate.ClsMain.LogStatus.LogImport & "' " &
                                    " And H.EntryBy = '" & AgL.PubUserName & "'"
        End If

        If IsApplyVTypePermission Then
            mCondStr = mCondStr & " And H.V_Type In (Select V_Type From User_VType_Permission VP Where VP.UserName = '" & AgL.PubUserName & "' And VP.Div_Code = '" & AgL.PubDivCode & "' And VP.Site_Code = '" & AgL.PubSiteCode & "') "
        End If


        AgL.PubFindQry = " SELECT H.DocID AS SearchCode, H.V_Type AS [Receive_Type], H.V_Date AS Date, " &
                            " H.ManualRefNo AS [Receive_No], P.Description As [Process], Sg.Name As [Job_Worker], Sg1.Name As [Job_Receive_By], " &
                            " G.Description As Godown, H.Remarks, H.EntryBy AS [Entry_By], H.EntryDate AS [Entry_Date], " &
                            " H.ApproveBy As [Approve_By], H.ApproveDate As [Approve_Date] " &
                            " FROM JobIssRec H  " &
                            " Left Join Voucher_Type Vt  On H.V_Type = Vt.V_Type  " &
                            " LEFT JOIN SubGroup Sg On H.JobWorker = Sg.SubCode " &
                            " LEFT JOIN SubGroup Sg1 On H.JobReceiveBy = Sg1.SubCode " &
                            " LEFT JOIN Godown G ON H.Godown = G.Code " &
                            " LEFT JOIN Process P On H.Process = P.NCat" &
                            " Where 1=1  " & mCondStr
        AgL.PubFindQryOrdBy = "[Entry_Date]"
    End Sub

    Private Sub FrmQuality1_BaseFunction_FIniMast(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMast
        Dim mCondStr$
        mCondStr = " " & AgL.CondStrFinancialYear("J.V_Date", AgL.PubStartDate, AgL.PubEndDate) &
                        " And " & AgL.PubSiteCondition("J.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "J.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        If ChkShowOnlyImportedRecords.Checked Then
            mCondStr = mCondStr & " And J.EntryStatus = '" & AgTemplate.ClsMain.LogStatus.LogImport & "'"
        End If

        If IsApplyVTypePermission Then
            mCondStr = mCondStr & " And J.V_Type In (Select V_Type From User_VType_Permission VP  Where VP.UserName = '" & AgL.PubUserName & "' And VP.Div_Code = '" & AgL.PubDivCode & "' And VP.Site_Code = '" & AgL.PubSiteCode & "') "
        End If


        mQry = " Select J.DocID As SearchCode " &
                " From JobIssRec J  " &
                " Left Join Voucher_Type Vt  On J.V_Type = Vt.V_Type  " &
                " Where IFNull(IsDeleted,0) = 0  " & mCondStr & "  Order By J.V_Date, J.V_Type, J.V_No "

        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub FrmSaleOrder_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl1, Col1Item_Uid, 80, 0, Col1Item_Uid, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_ItemUID")), Boolean), False, False)
            .AddAgTextColumn(Dgl1, Col1Item, 150, 0, Col1Item, True, False)

            .AddAgTextColumn(Dgl1, Col1StockItem, 80, 20, Col1StockItem, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_StockItem")), Boolean), False)
            .AddAgTextColumn(Dgl1, Col1ItemGroup, 100, 0, Col1ItemGroup, True, True)

            .AddAgTextColumn(Dgl1, Col1Dimension1, 100, 0, AgL.XNull(AgL.PubDtEnviro.Rows(0)("Caption_Dimension1")), CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Dimension1")), Boolean), False)
            .AddAgTextColumn(Dgl1, Col1Dimension2, 100, 0, AgL.XNull(AgL.PubDtEnviro.Rows(0)("Caption_Dimension2")), CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Dimension2")), Boolean), False)

            .AddAgTextColumn(Dgl1, Col1JobOrder, 100, 0, Col1JobOrder, True, True)
            .AddAgTextColumn(Dgl1, Col1JobOrderSr, 100, 0, Col1JobOrderSr, False, True)
            .AddAgTextColumn(Dgl1, Col1ProdOrder, 100, 0, Col1ProdOrder, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_ProdOrder")), Boolean), True)
            .AddAgTextColumn(Dgl1, Col1ProdOrderSr, 90, 0, Col1ProdOrderSr, False, False, False)
            .AddAgTextColumn(Dgl1, Col1LotNo, 80, 20, Col1LotNo, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_LotNo")), Boolean), False)
            .AddAgTextColumn(Dgl1, Col1JobOrderLotNo, 80, 20, Col1JobOrderLotNo, False, False)
            .AddAgTextColumn(Dgl1, Col1Machine, 100, 0, Col1Machine, True, False)
            .AddAgNumberColumn(Dgl1, Col1DocQty, 60, 8, 4, False, Col1DocQty, True, False, True)
            .AddAgNumberColumn(Dgl1, Col1RejQty, 60, 8, 4, False, Col1RejQty, True, False, True)
            .AddAgNumberColumn(Dgl1, Col1Qty, 60, 8, 4, False, Col1Qty, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Qty")), Boolean), True, True)
            .AddAgNumberColumn(Dgl1, Col1BillQty, 60, 8, 4, False, Col1BillQty, True, False, True)
            .AddAgTextColumn(Dgl1, Col1Unit, 60, 0, Col1Unit, True, True)
            .AddAgTextColumn(Dgl1, Col1QtyDecimalPlaces, 50, 0, Col1QtyDecimalPlaces, False, True, False)
            .AddAgNumberColumn(Dgl1, Col1LossPer, 60, 8, 4, False, Col1LossPer, CType(AgL.VNull(DtJobEnviro.Rows(0)("IsVisible_LossPer")), Boolean), False, True)
            .AddAgNumberColumn(Dgl1, Col1LossQty, 60, 8, 4, False, Col1LossQty, CType(AgL.VNull(DtJobEnviro.Rows(0)("IsVisible_Loss")), Boolean), False, True)
            .AddAgNumberColumn(Dgl1, Col1MeasurePerPcs, 70, 8, 4, False, Col1MeasurePerPcs, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_MeasurePerPcs")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_MeasurePerPcs")), Boolean), True)
            .AddAgNumberColumn(Dgl1, Col1DocMeasure, 70, 8, 4, False, Col1DocMeasure, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Measure")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_Measure")), Boolean), True)
            .AddAgNumberColumn(Dgl1, Col1RetMeasure, 70, 8, 4, False, Col1RetMeasure, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_RejMeasure")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_Measure")), Boolean), True)
            .AddAgNumberColumn(Dgl1, Col1TotalMeasure, 70, 8, 4, False, Col1TotalMeasure, False, True, True)
            .AddAgNumberColumn(Dgl1, Col1BillMeasure, 70, 8, 4, False, Col1BillMeasure, False, True, True)
            .AddAgTextColumn(Dgl1, Col1MeasureUnit, 70, 0, Col1MeasureUnit, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_MeasureUnit")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_MeasureUnit")), Boolean))
            .AddAgTextColumn(Dgl1, Col1MeasureDecimalPlaces, 50, 0, Col1MeasureDecimalPlaces, False, True, False)
            .AddAgNumberColumn(Dgl1, Col1Rate, 70, 8, 2, False, Col1Rate, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Rate")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_Rate")), Boolean), True)
            .AddAgNumberColumn(Dgl1, Col1Amount, 70, 8, 2, False, Col1Amount, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Amount")), Boolean), True, True)
            .AddAgTextColumn(Dgl1, Col1Remark, 200, 255, Col1Remark, True, False)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 35
        Dgl1.AgSkipReadOnlyColumns = True
        Dgl1.AllowUserToOrderColumns = True

        LblTotalMeasure.Visible = CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Measure")), Boolean)
        LblTotalMeasureText.Visible = CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Measure")), Boolean)

        AgTemplate.ClsMain.ProcCreateLink(Dgl1, Col1JobOrder)
        AgTemplate.ClsMain.ProcCreateLink(Dgl1, Col1ProdOrder)

        AgCalcGrid1.Ini_Grid(LblV_Type.Tag, TxtV_Date.Text)

        AgCalcGrid1.AgLineGrid = Dgl1
        AgCalcGrid1.AgLineGridMandatoryColumn = Dgl1.Columns(Col1Item).Index
        AgCalcGrid1.AgLineGridGrossColumn = Dgl1.Columns(Col1Amount).Index

        AgCustomGrid1.Name = "AgCustomGrid1"
        AgCustomGrid1.Ini_Grid(mSearchCode)
        AgCustomGrid1.SplitGrid = False

        If CType(AgL.VNull(DtJobEnviro.Rows(0)("IsPostedInJobOrder")), Boolean) Then
            BtnFillJobOrder.Enabled = False
            Dgl1.Columns(Col1JobOrder).Visible = False
            Dgl1.Columns(Col1ProdOrder).Visible = False
        Else
            BtnFillJobOrder.Enabled = True
            Dgl1.Columns(Col1JobOrder).Visible = True
            Dgl1.Columns(Col1ProdOrder).Visible = True
        End If

        AgCL.GridSetiingShowXml(Me.Text & TxtV_Type.Tag & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1, False)
        AgCL.GridSetiingShowXml(Me.Text & AgCustomGrid1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, AgCustomGrid1)
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As SQLiteConnection, ByVal Cmd As SQLiteCommand) Handles Me.BaseEvent_Save_InTrans
        Dim I As Integer, mSr As Integer
        Dim Stock As AgTemplate.ClsMain.StructStock = Nothing, StockProcess As AgTemplate.ClsMain.StructStock = Nothing
        Dim bSelectionQry$ = ""

        mQry = "UPDATE JobIssRec " &
                " SET " &
                " ManualRefNo = " & AgL.Chk_Text(TxtManualRefNo.Text) & ", " &
                " BillingType = " & AgL.Chk_Text(TxtBillingOn.Text) & ", " &
                " Process = " & AgL.Chk_Text(TxtProcess.AgSelectedValue) & ", " &
                " JobWorker = " & AgL.Chk_Text(TxtJobWorker.AgSelectedValue) & ", " &
                " JobReceiveBy = " & AgL.Chk_Text(TxtJobReceiveBy.Tag) & ", " &
                " Godown = " & AgL.Chk_Text(TxtGodown.AgSelectedValue) & ", " &
                " JobWorkerDocNo = " & AgL.Chk_Text(TxtPartyDocNo.Text) & ", " &
                " Remarks = " & AgL.Chk_Text(TxtRemarks.Text) & ", " &
                " Structure = " & AgL.Chk_Text(TxtStructure.Tag) & ", " &
                " CustomFields = " & AgL.Chk_Text(TxtCustomFields.Tag) & ", " &
                " RecQty = " & Val(LblTotalQty.Text) & ", " &
                " RecMeasure = " & Val(LblTotalMeasure.Text) & ", " &
                " " & AgCalcGrid1.FFooterTableUpdateStr() & " " &
                " " & AgCustomGrid1.FFooterTableUpdateStr() & " " &
                " Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        If Topctrl1.Mode <> "Add" Then
            'mQry = " SELECT Item_UID FROM JobReceiveDetail  WHERE DocId = '" & mSearchCode & "' And Item_Uid Is Not Null "
            'Dim DtItem_Uid As DataTable = AgL.FillData(mQry, AgL.GcnRead).Tables(0)
            'If DtItem_Uid.Rows.Count > 0 Then
            '    For I = 0 To DtItem_Uid.Rows.Count - 1
            '        AgTemplate.ClsMain.FUpdateItem_UidOnDelete(DtItem_Uid.Rows(I)("Item_Uid"), mSearchCode, Conn, Cmd)
            '    Next
            'End If

            mQry = "Delete From JobReceiveDetail Where DocId = '" & SearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If

        'Never Try to Serialise Sr in Line Items 
        'As Some other Entry points may updating values to this Search code and Sr
        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1Item, I).Value <> "" Then
                mSr += 1
                If bSelectionQry <> "" Then bSelectionQry += " UNION ALL "
                bSelectionQry += " Select " & AgL.Chk_Text(mSearchCode) & ", " &
                        " " & mSr & ", " & AgL.Chk_Text(Dgl1.Item(Col1Item_Uid, I).Tag) & ", " &
                        " " & AgL.Chk_Text(Dgl1.AgSelectedValue(Col1Item, I)) & ", " &
                         " " & AgL.Chk_Text(Dgl1.Item(Col1StockItem, I).Tag) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1Dimension1, I).Tag) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1Dimension2, I).Tag) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1LotNo, I).Value) & ", " &
                        " " & Val(Dgl1.Item(Col1DocQty, I).Value) & ", " &
                        " " & Val(Dgl1.Item(Col1RejQty, I).Value) & ", " &
                        " " & Val(Dgl1.Item(Col1Qty, I).Value) & ", " &
                        " " & Val(Dgl1.Item(Col1BillQty, I).Value) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1Unit, I).Value) & ", " &
                        " " & Val(Dgl1.Item(Col1MeasurePerPcs, I).Value) & ", " &
                        " " & Val(Dgl1.Item(Col1DocMeasure, I).Value) & ", " &
                        " " & Val(Dgl1.Item(Col1RetMeasure, I).Value) & ", " &
                        " " & Val(Dgl1.Item(Col1TotalMeasure, I).Value) & ", " &
                        " " & Val(Dgl1.Item(Col1BillMeasure, I).Value) & ", " &
                        " " & Val(Dgl1.Item(Col1LossPer, I).Value) & ", " &
                        " " & Val(Dgl1.Item(Col1LossQty, I).Value) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1MeasureUnit, I).Value) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1ProdOrder, I).Tag) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1ProdOrderSr, I).Value) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1JobOrder, I).Tag) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1JobOrderSr, I).Value) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1Machine, I).Tag) & ", " &
                        " " & Val(Dgl1.Item(Col1Rate, I).Value) & ", " &
                        " " & Val(Dgl1.Item(Col1Amount, I).Value) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1Remark, I).Value) & ", " &
                        " " & AgL.Chk_Text(mSearchCode) & ", " &
                        " " & mSr & ", " &
                        " " & AgCalcGrid1.FLineTableFieldValuesStr(I) & " "

            End If
        Next

        If bSelectionQry <> "" Then
            mQry = "INSERT INTO JobReceiveDetail(DocId, Sr, Item_Uid, Item, StockItem, Dimension1, Dimension2, LotNo, DocQty, RetQty, Qty, BillQty, Unit, MeasurePerPcs, DocMeasure,RetMeasure, TotalMeasure, BillMeasure,  LossPer, LossQty," &
                    " MeasureUnit, ProdOrder, ProdOrderSr, JobOrder, JobOrderSr, Machine, Rate, Amount, Remark, JobReceive, JobReceiveSr, " & AgCalcGrid1.FLineTableFieldNameStr() & ") " & bSelectionQry
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If

        If AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Or AgL.StrCmp(AgL.PubUserName, "Sa") Then
            AgCL.GridSetiingWriteXml(Me.Text & TxtV_Type.Tag & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1)
            AgCL.GridSetiingWriteXml(Me.Text & AgCustomGrid1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, AgCustomGrid1)
        End If

        mQry = "Delete From Stock Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Delete From StockProcess Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        If CType(AgL.VNull(DtJobEnviro.Rows(0)("IsPostedInJobOrder")), Boolean) Then
            Call FPostInJobOrder(Conn, Cmd)
        Else
            If bSelectionQry <> "" Then Call FPostInStockProcess(Conn, Cmd)
            If bSelectionQry <> "" Then Call FPostInStockVirtual(Conn, Cmd)
        End If

        If CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsPostedInStock")), Boolean) Then
            If bSelectionQry <> "" Then Call FPostInStock(Conn, Cmd)
        End If

        Call FPostConsumption(Conn, Cmd)
        If bSelectionQry <> "" Then Call FPostByProduct(Conn, Cmd)
        If bSelectionQry <> "" Then Call FPostInJobIssRecUID(SearchCode, Conn, Cmd)

        'For I = 0 To Dgl1.Rows.Count - 1
        '    If Dgl1.Item(Col1Item_Uid, I).Tag <> "" And Dgl1.Item(Col1StockItem, I).Tag = "" Then
        '        AgTemplate.ClsMain.FUpdateItem_Uid(Dgl1.Item(Col1Item_Uid, I).Tag, Topctrl1.Mode, mSearchCode, TxtV_Type.Tag, TxtV_Date.Text, TxtJobWorker.Tag, TxtGodown.Tag, TxtProcess.Tag, AgTemplate.ClsMain.Item_UidStatus.Receive, TxtManualRefNo.Text, Conn, Cmd)
        '    End If
        'Next

        If ImportMode = True Then
            mQry = " UPDATE JobIssRec Set EntryStatus = '" & AgTemplate.ClsMain.LogStatus.LogImport & "' Where DocId = '" & mSearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If
    End Sub

    Private Sub FPostInJobOrder(ByVal Conn As SQLiteConnection, ByVal Cmd As SQLiteCommand)
        Dim I As Integer = 0, Cnt As Integer = 0
        Dim bSelectionQry$ = ""

        mQry = " UPDATE JobReceiveDetail " &
                " Set " &
                " JobOrder = NULL, " &
                " JobOrderSr = NULL " &
                " Where DocId = '" & mSearchCode & "' " &
                " And JobOrder  = '" & mSearchCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " UPDATE JobReceiveBOM " &
                " Set " &
                " JobOrder = NULL, " &
                " JobOrderSr = NULL " &
                " Where DocId = '" & mSearchCode & "' " &
                " And JobOrder  = '" & mSearchCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " Delete From JobOrderDetail Where DocId = '" & mSearchCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " Delete From JobOrder Where DocId = '" & mSearchCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " Select Count(*) From JobReceiveDetail L  " &
                " Where L.DocId = '" & mSearchCode & "' " &
                " And (L.JobOrder = '" & mSearchCode & "' Or L.JobOrder Is Null) "
        If AgL.VNull(AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar) > 0 Then
            mQry = " INSERT INTO JobOrder(DocID, V_Type, V_Prefix, V_Date, V_No, Div_Code, Site_Code, ManualRefNo, " &
                    " Process,	JobWorker,	Godown,	Remarks,	Structure,	EntryBy, " &
                    " EntryDate, EntryType, EntryStatus, ApproveBy, ApproveDate, MoveToLog, MoveToLogDate, " &
                    " IsDeleted,	Status,	UID,	BillingType,	OrderBy ) " &
                    " Select DocID, V_Type, V_Prefix, V_Date, V_No, Div_Code, Site_Code, ManualRefNo, " &
                    " Process,	JobWorker,	Godown,	Remarks,	Structure,	EntryBy, " &
                    " EntryDate, EntryType, EntryStatus, ApproveBy, ApproveDate, MoveToLog, MoveToLogDate, " &
                    " IsDeleted, Status, UID, BillingType, JobReceiveBy " &
                    " FROM JobIssRec " &
                    " Where DocId = '" & mSearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

            mQry = " INSERT INTO JobOrderDetail(DocId, Sr, Item_UID,	Item,	Qty,	Unit,	MeasurePerPcs, " &
                    " TotalMeasure,	MeasureUnit, JobOrder, JobOrderSr,	Rate,	Remark, ProdOrder, ProdOrderSr, LotNo ) " &
                    " Select DocId, Sr, Item_UID,	Item,	Qty,	Unit,	MeasurePerPcs, " &
                    " TotalMeasure,	MeasureUnit, DocId, Sr, Rate,	Remark, " &
                    " ProdOrder, ProdOrderSr, LotNo " &
                    " FROM JobReceiveDetail Where DocId = '" & mSearchCode & "' "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

            mQry = " UPDATE JobReceiveDetail " &
                    " Set " &
                    " JobOrder = DocId, " &
                    " JobOrderSr = Sr " &
                    " Where DocId = '" & mSearchCode & "' " &
                    " And JobOrder Is Null "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If
    End Sub

    Private Sub FrmProductionOrder_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim I As Integer
        Dim DsTemp As DataSet

        mQry = "Select J.*, G.Description As GodownDesc, P.Description As ProcessDesc, " &
                " Sg.Name + ',' + IFNull(C.CityName,'') As JobWorkerName , Sg1.DispName As JobReceiveByName " &
                " From JobIssRec J  " &
                " LEFT JOIN Godown G  On J.Godown = G.Code " &
                " LEFT JOIN Process P  On J.Process = P.NCat " &
                " LEFT JOIN SubGroup SG  On J.JobWorker = Sg.SubCode " &
                " LEFT JOIN SubGroup Sg1  On J.JobReceiveBy = Sg1.SubCode " &
                " Left Join City C  On Sg.CityCode = C.CityCode " &
                " Where J.DocID = '" & SearchCode & "'"
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                TxtStructure.AgSelectedValue = AgStructure.ClsMain.FGetStructureFromNCat(LblV_Type.Tag, AgL.GcnRead)
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

                DtJobEnviro = AgL.FillData("SELECT * FROM JobEnviro WHERE V_Type ='" & TxtV_Type.Tag & "' AND Site_Code='" & AgL.PubSiteCode & "' AND Div_Code='" & AgL.PubDivCode & "'", AgL.GCn).Tables(0)
                IniGrid()

                TxtGodown.Tag = AgL.XNull(.Rows(0)("Godown"))
                TxtGodown.Text = AgL.XNull(.Rows(0)("GodownDesc"))
                TxtPartyDocNo.Text = AgL.XNull(.Rows(0)("JobWorkerDocNo"))

                TxtManualRefNo.Text = AgL.XNull(.Rows(0)("ManualRefNo"))

                TxtProcess.Tag = AgL.XNull(.Rows(0)("Process"))
                TxtProcess.Text = AgL.XNull(.Rows(0)("ProcessDesc"))

                TxtBillingOn.Text = AgL.XNull(.Rows(0)("BillingType"))

                TxtJobWorker.Tag = AgL.XNull(.Rows(0)("JobWorker"))
                TxtJobWorker.Text = AgL.XNull(.Rows(0)("JobWorkerName"))
                TxtJobReceiveBy.Tag = AgL.XNull(.Rows(0)("JobReceiveBy"))
                TxtJobReceiveBy.Text = AgL.XNull(.Rows(0)("JobReceiveByName"))

                TxtRemarks.Text = AgL.XNull(.Rows(0)("Remarks"))

                LblTotalQty.Text = "0"
                LblTotalAmount.Text = "0"
                LblTotalMeasure.Text = "0"

                ChkShowOnlyImportedRecords.Visible = True
                If AgL.StrCmp(AgL.XNull(.Rows(0)("EntryStatus")), AgTemplate.ClsMain.LogStatus.LogImport) Then
                    BtnImprtFromText.Text = ImportAction_ClearImport
                Else
                    BtnImprtFromText.Text = ImportAction_NewImport
                End If


                AgCalcGrid1.FMoveRecFooterTable(DsTemp.Tables(0), LblV_Type.Tag, TxtV_Date.Text)

                AgCustomGrid1.FMoveRecFooterTable(DsTemp.Tables(0))

                isRecordLocked = False
                '-------------------------------------------------------------
                'Line Records are showing in Grid
                '-------------------------------------------------------------
                Dim strQryJobReceived$ = "SELECT L.JobReceive, L.JobReceiveSr, Sum(L.Qty) AS Qty " &
                                         "FROM JobInvoiceDetail L  " &
                                         "Where L.JobReceive = '" & SearchCode & "' " &
                                         "GROUP BY L.JobReceive, L.JobReceiveSr  "



                mQry = "Select L.*, I.Description As ItemDesc, SI.Description As StockItemDesc, IG.Description As ItemGroupDesc, IU.Item_UID as Item_Uid_Desc, J.V_Type + '-' + J.ManualRefNo As JobOrderNo, " &
                        " IFNull(J.JobWithMaterialYN,0) As JobWithMaterialYN, JOD.LotNo As JobOrderLotNo, " &
                        " U.DecimalPlaces as QtyDecimalPlaces, MU.DecimalPlaces as MeasureDecimalPlaces, " &
                        " P.ManualRefNo As ProdOrderNo, SGM.DispName AS MachineName, " &
                        " D1.Description As Dimension1Desc, D2.Description As Dimension2Desc, " &
                        " (Case When IFNull(JobInv.Qty,0) > 0 Then 1 Else 0 End) as RowLocked " &
                        " From JobReceiveDetail L  " &
                        " LEFT JOIN Item I  On L.Item = I.Code " &
                        " LEFT JOIN Item SI  On L.StockItem = SI.Code " &
                        " LEFT JOIN ItemGroup IG  On I.ItemGroup = IG.Code " &
                        " Left Join Item_UID IU   On L.Item_UID = IU.Code " &
                        " LEFT JOIN JobOrder J  On L.JobOrder = J.DocId " &
                        " LEFT JOIN JobOrderDetail JOD  On L.JobOrder = JOD.DocId AND L.JobOrderSr = JOD.JobOrderSr " &
                        " LEFT JOIN ProdOrder P  On L.ProdOrder = P.DocId " &
                        " Left Join Unit U  On L.Unit = U.Code " &
                        " Left Join Unit MU  On L.MeasureUnit = MU.Code " &
                        " Left Join SubGroup SGM   On L.Machine = SGM.SubCode " &
                        " Left Join Dimension1 D1   On L.Dimension1 = D1.Code " &
                        " Left Join Dimension2 D2   On L.Dimension2 = D2.Code " &
                        " Left Join (" & strQryJobReceived & ") as JobInv On L.DocID + Convert(VarChar,L.Sr) = JobInv.JobReceive + Convert(VarChar,JobInv.JobReceiveSr) " &
                        " Where L.DocId = '" & SearchCode & "' Order By L.Sr"
                DsTemp = AgL.FillData(mQry, AgL.GCn)
                With DsTemp.Tables(0)
                    Dgl1.RowCount = 1
                    Dgl1.Rows.Clear()
                    If .Rows.Count > 0 Then
                        For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                            Dgl1.Rows.Add()
                            Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count - 1

                            Dgl1.Item(Col1Item_Uid, I).Tag = AgL.XNull(.Rows(I)("Item_Uid"))
                            Dgl1.Item(Col1Item_Uid, I).Value = AgL.XNull(.Rows(I)("Item_Uid_Desc"))

                            Dgl1.Item(Col1Item, I).Tag = AgL.XNull(.Rows(I)("Item"))
                            Dgl1.Item(Col1Item, I).Value = AgL.XNull(.Rows(I)("ItemDesc"))
                            Dgl1.Item(Col1StockItem, I).Tag = AgL.XNull(.Rows(I)("StockItem"))
                            Dgl1.Item(Col1StockItem, I).Value = AgL.XNull(.Rows(I)("StockItemDesc"))
                            Dgl1.Item(Col1Dimension1, I).Tag = AgL.XNull(.Rows(I)("Dimension1"))
                            Dgl1.Item(Col1Dimension1, I).Value = AgL.XNull(.Rows(I)("Dimension1Desc"))

                            Dgl1.Item(Col1Dimension2, I).Tag = AgL.XNull(.Rows(I)("Dimension2"))
                            Dgl1.Item(Col1Dimension2, I).Value = AgL.XNull(.Rows(I)("Dimension2Desc"))

                            Dgl1.Item(Col1Machine, I).Tag = AgL.XNull(.Rows(I)("Machine"))
                            Dgl1.Item(Col1Machine, I).Value = AgL.XNull(.Rows(I)("MachineName"))

                            Dgl1.Item(Col1ItemGroup, I).Value = AgL.XNull(.Rows(I)("ItemGroupDesc"))
                            Dgl1.Item(Col1LotNo, I).Value = AgL.XNull(.Rows(I)("LotNo"))
                            Dgl1.Item(Col1JobOrderLotNo, I).Value = AgL.XNull(.Rows(I)("JobOrderLotNo"))
                            Dgl1.Item(Col1DocQty, I).Value = Format(AgL.VNull(.Rows(I)("DocQty")), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1RejQty, I).Value = Format(AgL.VNull(.Rows(I)("RetQty")), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1Qty, I).Value = Format(AgL.VNull(.Rows(I)("Qty")), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1BillQty, I).Value = Format(AgL.VNull(.Rows(I)("BillQty")), "0.00")
                            Dgl1.Item(Col1Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                            Dgl1.Item(Col1QtyDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("QtyDecimalPlaces"))
                            Dgl1.Item(Col1MeasurePerPcs, I).Value = Format(AgL.VNull(.Rows(I)("MeasurePerPcs")), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1DocMeasure, I).Value = Format(AgL.VNull(.Rows(I)("DocMeasure")), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1RetMeasure, I).Value = Format(AgL.VNull(.Rows(I)("RetMeasure")), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1TotalMeasure, I).Value = Format(AgL.VNull(.Rows(I)("TotalMeasure")), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1BillMeasure, I).Value = Format(AgL.VNull(.Rows(I)("BillMeasure")), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1LossPer, I).Value = AgL.VNull(.Rows(I)("LossPer"))
                            Dgl1.Item(Col1LossQty, I).Value = Format(AgL.VNull(.Rows(I)("LossQty")), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1MeasureUnit, I).Value = AgL.XNull(.Rows(I)("MeasureUnit"))
                            Dgl1.Item(Col1MeasureDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("MeasureDecimalPlaces"))

                            Dgl1.Item(Col1JobOrder, I).Tag = AgL.XNull(.Rows(I)("JobOrder"))
                            Dgl1.Item(Col1JobOrder, I).Value = AgL.XNull(.Rows(I)("JobOrderNo"))
                            Dgl1.Item(Col1JobOrderSr, I).Value = AgL.XNull(.Rows(I)("JobOrderSr"))

                            Dgl1.Item(Col1ProdOrder, I).Tag = AgL.XNull(.Rows(I)("ProdOrder"))
                            Dgl1.Item(Col1ProdOrder, I).Value = AgL.XNull(.Rows(I)("ProdOrderNo"))
                            Dgl1.Item(Col1ProdOrderSr, I).Value = AgL.XNull(.Rows(I)("ProdOrderSr"))


                            Dgl1.Item(Col1Rate, I).Value = Format(AgL.VNull(.Rows(I)("Rate")), "0.00")
                            Dgl1.Item(Col1Amount, I).Value = AgL.VNull(.Rows(I)("Amount"))
                            Dgl1.Item(Col1Remark, I).Value = AgL.XNull(.Rows(I)("Remark"))




                            If Not AgL.StrCmp(Dgl1.Item(Col1Unit, I).Value, Dgl1.Item(Col1Unit, 0).Value) Then IsSameUnit = False
                            If Not AgL.StrCmp(Dgl1.Item(Col1MeasureUnit, I).Value, Dgl1.Item(Col1MeasureUnit, 0).Value) Then IsSameMeasureUnit = False

                            If intQtyDecimalPlaces < Val(Dgl1.Item(Col1QtyDecimalPlaces, I).Value) Then intQtyDecimalPlaces = Val(Dgl1.Item(Col1QtyDecimalPlaces, I).Value)
                            If intMeasureDecimalPlaces < Val(Dgl1.Item(Col1MeasureDecimalPlaces, I).Value) Then intMeasureDecimalPlaces = Val(Dgl1.Item(Col1MeasureDecimalPlaces, I).Value)

                            LblTotalQty.Text = Val(LblTotalQty.Text) + Val(Dgl1.Item(Col1Qty, I).Value)
                            LblTotalMeasure.Text = Val(LblTotalMeasure.Text) + Val(Dgl1.Item(Col1TotalMeasure, I).Value)
                            LblTotalAmount.Text = Val(LblTotalAmount.Text) + Val(Dgl1.Item(Col1Amount, I).Value)


                            If .Rows(I)("RowLocked") > 0 Then
                                Dgl1.Rows(I).DefaultCellStyle.BackColor = AgTemplate.ClsMain.Colours.GridRow_Locked : Dgl1.Rows(I).ReadOnly = True
                                If isRecordLocked = False Then isRecordLocked = True
                            End If



                            Call AgCalcGrid1.FMoveRecLineTable(DsTemp.Tables(0), I)
                        Next I
                    End If
                End With

                If AgCustomGrid1.Rows.Count = 0 Then AgCustomGrid1.Visible = False
            End If
        End With
        BtnConsumptionDetail.Tag = Nothing
        BtnByProduct.Tag = Nothing
    End Sub

    Private Sub FrmProductionOrder_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Topctrl1.ChangeAgGridState(Dgl1, False)
        AgCalcGrid1.FrmType = Me.FrmType
        AgCustomGrid1.FrmType = Me.FrmType
        AgL.WinSetting(Me, 644, 971, 0, 0)
    End Sub

    Private Sub Txt_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtV_Type.Validating, TxtV_Date.Validating, TxtManualRefNo.Validating, TxtProcess.Validating, TxtBillingOn.Validating
        Dim DtTemp As DataTable = Nothing

        Try
            Select Case sender.NAME
                Case TxtV_Type.Name
                    'TxtStructure.AgSelectedValue = AgStructure.ClsMain.FGetStructureFromNCat(LblV_Type.Tag, AgL.GcnRead)
                    'AgCalcGrid1.AgStructure = TxtStructure.AgSelectedValue


                    If TxtV_Type.Tag <> "" Then
                        DtJobEnviro = AgL.FillData("SELECT * FROM JobEnviro WHERE V_Type ='" & TxtV_Type.Tag & "' AND Site_Code='" & AgL.PubSiteCode & "' AND Div_Code='" & AgL.PubDivCode & "'", AgL.GCn).Tables(0)
                        If DtJobEnviro.Rows.Count = 0 Then
                            MsgBox("Job Enivro Settings are not defined. Can't Continue!")
                            Topctrl1.FButtonClick(14, True)
                            Exit Sub
                        End If
                        Dgl1.AgHelpDataSet(Col1Item) = Nothing
                    End If

                    If AgL.XNull(DtV_TypeSettings.Rows(0)("Structure")) = "" Then
                        TxtStructure.AgSelectedValue = AgStructure.ClsMain.FGetStructureFromNCat(LblV_Type.Tag, AgL.GcnRead)
                        AgCalcGrid1.AgStructure = TxtStructure.AgSelectedValue
                    Else
                        TxtStructure.Tag = AgL.XNull(DtV_TypeSettings.Rows(0)("Structure"))
                        AgCalcGrid1.AgStructure = AgL.XNull(DtV_TypeSettings.Rows(0)("Structure"))
                    End If

                    TxtCustomFields.Tag = AgCustomFields.ClsMain.FGetCustomFieldFromV_Type(TxtV_Type.Tag, AgL.GcnRead)
                    AgCustomGrid1.AgCustom = TxtCustomFields.Tag

                    IniGrid()
                    TxtManualRefNo.Text = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "JobIssRec", TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, AgTemplate.ClsMain.ManualRefType.Max)
                    If Dgl1.AgHelpDataSet(Col1Item) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1Item).Dispose() : Dgl1.AgHelpDataSet(Col1Item) = Nothing
                    If TxtJobWorker.AgHelpDataSet IsNot Nothing Then TxtJobWorker.AgHelpDataSet.Dispose() : TxtJobWorker.AgHelpDataSet = Nothing


                    FAsignProcess()
                    FAsignMeasureField()


                Case TxtProcess.Name
                    TxtBillingOn.Text = AgL.XNull(AgL.Dman_Execute(" SELECT H.DefaultBillingType FROM Process H  WHERE H.NCat = '" & TxtProcess.AgSelectedValue & "' ", AgL.GCn).ExecuteScalar)

                Case TxtV_Date.Name
                    If Topctrl1.Mode = "Add" Then
                        TxtManualRefNo.Text = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "JobIssRec", TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, AgTemplate.ClsMain.ManualRefType.Max)
                    End If

                Case TxtManualRefNo.Name
                    e.Cancel = Not FCheckDuplicateRefNo()

                Case TxtBillingOn.Name
                    TxtBillingOn.Text = AgL.XNull(AgL.Dman_Execute(" SELECT H.DefaultBillingType FROM Process H  WHERE H.NCat = '" & TxtProcess.AgSelectedValue & "' ", AgL.GCn).ExecuteScalar)
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
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
        TxtBillingOn.Text = AgL.XNull(AgL.Dman_Execute(" SELECT H.DefaultBillingType FROM Process H  WHERE H.NCat = '" & TxtProcess.AgSelectedValue & "' ", AgL.GCn).ExecuteScalar)

        If TxtGodown.Tag = "" Then
            TxtGodown.Tag = AgL.XNull(DtV_TypeSettings.Rows(0)("DEFAULT_Godown"))
            TxtGodown.Text = AgL.XNull(AgL.Dman_Execute("SELECT Description  FROM Godown WHERE Code = " & AgL.Chk_Text(TxtGodown.Tag) & " ", AgL.GCn).ExecuteScalar)
        End If
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_Topctrl_tbAdd() Handles Me.BaseEvent_Topctrl_tbAdd
        TxtStructure.AgSelectedValue = AgStructure.ClsMain.FGetStructureFromNCat(LblV_Type.Tag, AgL.GcnRead)
        AgCalcGrid1.AgStructure = TxtStructure.AgSelectedValue
        AgCalcGrid1.AgNCat = LblV_Type.Tag


        TxtCustomFields.Tag = AgCustomFields.ClsMain.FGetCustomFieldFromV_Type(TxtV_Type.Tag, AgL.GCn)
        AgCustomGrid1.AgCustom = TxtCustomFields.Tag

        IniGrid()

        'TxtProcess.Tag = AgL.Dman_Execute(" SELECT H.NCat FROM Process H  WHERE H.ProcessReceiveNCat = '" & EntryNCat & "' ", AgL.GCn).ExecuteScalar
        TxtBillingOn.Text = AgL.XNull(AgL.Dman_Execute(" SELECT H.DefaultBillingType FROM Process H  WHERE H.NCat = '" & TxtProcess.AgSelectedValue & "' ", AgL.GCn).ExecuteScalar)
        TxtManualRefNo.Text = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "JobIssRec", TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, AgTemplate.ClsMain.ManualRefType.Max)

        FAsignProcess()
        FAsignMeasureField()

        TxtGodown.Tag = PubDefaultGodownCode
        TxtGodown.Text = PubDefaultGodownName

        BtnImprtFromText.Text = ImportAction_NewImport
    End Sub

    Private Sub DGL1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.KeyDown
        If e.Control And e.KeyCode = Keys.D Then
            sender.CurrentRow.Selected = True
        End If
        If e.Control Or e.Shift Or e.Alt Then Exit Sub
    End Sub

    Private Sub Dgl1_EditingControl_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Dgl1.EditingControl_Validating
        If Topctrl1.Mode = "Browse" Then Exit Sub
        Dim mRowIndex As Integer, mColumnIndex As Integer
        Dim DrTemp As DataRow() = Nothing
        Dim ErrMsgStr$ = ""
        Try
            mRowIndex = Dgl1.CurrentCell.RowIndex
            mColumnIndex = Dgl1.CurrentCell.ColumnIndex
            If Dgl1.Item(mColumnIndex, mRowIndex).Value Is Nothing Then Dgl1.Item(mColumnIndex, mRowIndex).Value = ""
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Item
                    Validating_Item(Dgl1.AgSelectedValue(Col1Item, mRowIndex), mRowIndex)
                    'FCheckDuplicate(mRowIndex)
                    Dgl1.Item(Col1Qty, mRowIndex).Value = Format(Val(Dgl1.Item(Col1Qty, mRowIndex).Value), "0.".PadRight(CType(Dgl1.Columns(Col1Qty), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                    Dgl1.Item(Col1BillQty, mRowIndex).Value = Format(Val(Dgl1.Item(Col1BillQty, mRowIndex).Value), "0.".PadRight(CType(Dgl1.Columns(Col1BillQty), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))

                Case Col1Item_Uid
                    ErrMsgStr = FCheck_Item_UID(Dgl1.Item(Col1Item_Uid, mRowIndex).Value, TxtJobWorker.Tag)
                    If ErrMsgStr <> "" Then
                        MsgBox(ErrMsgStr)
                        Dgl1.Item(Col1Item_Uid, Dgl1.CurrentCell.RowIndex).Value = ""
                        Dgl1.Item(Col1Item_Uid, Dgl1.CurrentCell.RowIndex).Tag = ""
                        Exit Sub
                    End If
                    Validating_Item_Uid(Dgl1.Item(Col1Item_Uid, Dgl1.CurrentCell.RowIndex).Value, Dgl1.CurrentCell.RowIndex)

                Case Col1DocQty
                    Dgl1.Item(Col1BillQty, mRowIndex).Value = Format(Val(Dgl1.Item(Col1DocQty, mRowIndex).Value), "0.".PadRight(CType(Dgl1.Columns(Col1BillQty), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))


            End Select
            If Not AgL.StrCmp(Topctrl1.Mode, "Browse") Then Call Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Validating_Item(ByVal Code As String, ByVal mRow As Integer)
        Dim DrTemp As DataRow() = Nothing
        Dim DtTemp As DataTable = Nothing
        Try
            If Dgl1.Item(Col1Item, mRow).Value.ToString.Trim = "" Or Dgl1.AgSelectedValue(Col1Item, mRow).ToString.Trim = "" Then
                Dgl1.Item(Col1Unit, mRow).Value = ""
                Dgl1.Item(Col1Qty, mRow).Value = 0
                Dgl1.Item(Col1MeasurePerPcs, mRow).Value = 0
                Dgl1.Item(Col1MeasureUnit, mRow).Value = ""
                Dgl1.AgSelectedValue(Col1JobOrder, mRow) = ""
                Dgl1.Item(Col1Rate, mRow).Value = 0
            Else
                If Dgl1.AgDataRow IsNot Nothing Then
                    Dgl1.Item(Col1DocQty, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("Bal.Qty").Value)
                    Dgl1.Item(Col1Qty, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("Bal.Qty").Value)
                    Dgl1.Item(Col1BillQty, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("Bal.Qty").Value)
                    Dgl1.Item(Col1Rate, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("Rate").Value)
                    Dgl1.Item(Col1Unit, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("Unit").Value)
                    Dgl1.Item(Col1ItemGroup, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("ItemGroupDesc").Value)
                    Dgl1.Item(Col1LotNo, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("LotNo").Value)
                    Dgl1.Item(Col1JobOrderLotNo, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("LotNo").Value)
                    Dgl1.Item(Col1QtyDecimalPlaces, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("QtyDecimalPlaces").Value)

                    Dgl1.Item(Col1MeasurePerPcs, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("MeasurePerPcs").Value)
                    Dgl1.Item(Col1MeasureUnit, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("MeasureUnit").Value)
                    Dgl1.Item(Col1MeasureDecimalPlaces, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("MeasureDecimalPlaces").Value)

                    Dgl1.Item(Col1Machine, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("Machine").Value)
                    Dgl1.Item(Col1Machine, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("MachineName").Value)

                    Dgl1.Item(Col1Dimension1, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("Dimension1").Value)
                    Dgl1.Item(Col1Dimension1, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("" & AgTemplate.ClsMain.FGetDimension1Caption() & "").Value)

                    Dgl1.Item(Col1Dimension2, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("Dimension2").Value)
                    Dgl1.Item(Col1Dimension2, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("" & AgTemplate.ClsMain.FGetDimension2Caption() & "").Value)

                    Dgl1.Item(Col1JobOrder, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("JobOrder").Value)
                    Dgl1.Item(Col1JobOrder, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("JobOrderNo").Value)
                    Dgl1.Item(Col1JobOrderSr, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("JobOrderSr").Value)

                    Dgl1.Item(Col1ProdOrder, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("ProdOrder").Value)
                    Dgl1.Item(Col1ProdOrder, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("ProdOrderNo").Value)
                    Dgl1.Item(Col1ProdOrderSr, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("ProdOrderSr").Value)

                    AgCalcGrid1.AgChargesValue(AgTemplate.ClsMain.Charges.INCENTIVE, mRow, AgStructure.AgCalcGrid.LineColumnType.Percentage) = AgL.VNull(Dgl1.AgDataRow.Cells("IncentiveRate").Value)
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " On Validating_Item Function ")
        End Try
    End Sub

    Private Sub DGL1_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles Dgl1.RowsAdded, Dgl1.RowsAdded
        sender(ColSNo, e.RowIndex).Value = e.RowIndex + 1
    End Sub

    Private Sub FrmProductionOrder_BaseFunction_Calculation() Handles Me.BaseFunction_Calculation
        Dim I As Integer

        LblTotalQty.Text = 0
        LblTotalMeasure.Text = 0
        LblTotalAmount.Text = 0

        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1Item, I).Value <> "" Then

                Dgl1.Item(Col1Qty, I).Value = Val(Dgl1.Item(Col1DocQty, I).Value) - Val(Dgl1.Item(Col1RejQty, I).Value) - Val(Dgl1.Item(Col1LossQty, I).Value)
                If Val(Dgl1.Item(Col1BillQty, I).Value) = 0 Then Dgl1.Item(Col1BillQty, I).Value = Val(Dgl1.Item(Col1DocQty, I).Value) - Val(Dgl1.Item(Col1RejQty, I).Value) - Val(Dgl1.Item(Col1LossQty, I).Value)

                Dgl1.Item(Col1RetMeasure, I).Value = Format(Val(Dgl1.Item(Col1RejQty, I).Value) * Val(Dgl1.Item(Col1MeasurePerPcs, I).Value), "0.".PadRight(CType(Dgl1.Columns(Col1RetMeasure), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                Dgl1.Item(Col1TotalMeasure, I).Value = Format(Val(Dgl1.Item(Col1Qty, I).Value) * Val(Dgl1.Item(Col1MeasurePerPcs, I).Value), "0.".PadRight(CType(Dgl1.Columns(Col1TotalMeasure), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                Dgl1.Item(Col1BillMeasure, I).Value = Format(Val(Dgl1.Item(Col1BillQty, I).Value) * Val(Dgl1.Item(Col1MeasurePerPcs, I).Value), "0.".PadRight(CType(Dgl1.Columns(Col1BillMeasure), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))

                If Val(Dgl1.Item(Col1MeasurePerPcs, I).Value) <> 0 Then
                    Dgl1.Item(Col1DocMeasure, I).Value = Format(Val(Dgl1.Item(Col1DocQty, I).Value) * Val(Dgl1.Item(Col1MeasurePerPcs, I).Value), "0.".PadRight(CType(Dgl1.Columns(Col1BillMeasure), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                End If

                If AgL.StrCmp(TxtBillingOn.Text, "Qty") Or TxtBillingOn.Text = "" Then
                    Dgl1.Item(Col1Amount, I).Value = Format(Val(Dgl1.Item(Col1BillQty, I).Value) * Val(Dgl1.Item(Col1Rate, I).Value), "0.".PadRight(CType(Dgl1.Columns(Col1Amount), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                ElseIf AgL.StrCmp(TxtBillingOn.Text, "Measure") Or AgL.StrCmp(TxtBillingOn.Text, "Area") Then
                    Dgl1.Item(Col1Amount, I).Value = Format(Val(Dgl1.Item(Col1BillMeasure, I).Value) * Val(Dgl1.Item(Col1Rate, I).Value), "0.".PadRight(CType(Dgl1.Columns(Col1Amount), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                End If

                If Val(Dgl1.Item(Col1LossPer, I).Value) <> 0 Then
                    Dgl1.Item(Col1LossQty, I).Value = Val(Dgl1.Item(Col1LossPer, I).Value) * Val(Dgl1.Item(Col1Qty, I).Value)
                End If
                LblTotalQty.Text = Val(LblTotalQty.Text) + Val(Dgl1.Item(Col1Qty, I).Value)
                LblTotalMeasure.Text = Val(LblTotalMeasure.Text) + Val(Dgl1.Item(Col1TotalMeasure, I).Value)
                LblTotalAmount.Text = Val(LblTotalAmount.Text) + Val(Dgl1.Item(Col1Amount, I).Value)

                If Val(Dgl1.Item(Col1TotalMeasure, I).Value) = 0 And Val(Dgl1.Item(Col1MeasurePerPcs, I).Value) = 0 And Val(Dgl1.Item(Col1DocMeasure, I).Value) <> 0 Then
                    Dgl1.Item(Col1TotalMeasure, I).Value = Dgl1.Item(Col1DocMeasure, I).Value
                End If
            End If
        Next
        AgCalcGrid1.Calculation()
        LblTotalQty.Text = Val(LblTotalQty.Text)
        LblTotalMeasure.Text = Val(LblTotalMeasure.Text)
        LblTotalAmount.Text = Val(LblTotalAmount.Text)
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        Dim I As Integer = 0
        Dim bBalQty As Double = 0
        ' Dim DrTemp() As DataRow = Nothing
        'Dim DtTemp As DataTable = Nothing
        'Dim mJobOrderStr$ = ""

        If AgL.RequiredField(TxtGodown, LblGodown.Text) Then passed = False : Exit Sub
        If AgL.RequiredField(TxtManualRefNo, LblManualRefNo.Text) Then passed = False : Exit Sub
        ' If AgCL.AgIsBlankGrid(Dgl1, Dgl1.Columns(Col1Item).Index) = True Then passed = False : Exit Sub
        If AgCL.AgIsDuplicate(Dgl1, "" + Dgl1.Columns(Col1Item).Index.ToString + "," + Dgl1.Columns(Col1Item_Uid).Index.ToString + "," + Dgl1.Columns(Col1JobOrder).Index.ToString + "," + Dgl1.Columns(Col1JobOrderSr).Index.ToString + "," + Dgl1.Columns(Col1LotNo).Index.ToString + "," + Dgl1.Columns(Col1Machine).Index.ToString + "," + Dgl1.Columns(Col1Dimension1).Index.ToString + "," + Dgl1.Columns(Col1Dimension2).Index.ToString + "") Then passed = False : Exit Sub

        passed = FCheckDuplicateRefNo()

        With Dgl1
            For I = 0 To .Rows.Count - 1
                If .Item(Col1Item, I).Value <> "" Then
                    mQry = " SELECT IFNull(IsRequired_LotNo,0) AS IsRequired_LotNo FROM ItemSiteDetail " &
                            " WHERE Code = '" & .Item(Col1Item, I).Tag & "' " &
                            " AND Div_Code = '" & AgL.PubDivCode & "' AND Site_Code = '" & AgL.PubSiteCode & "' "
                    If AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar) <> 0 And .Item(Col1LotNo, I).Value = "" Then
                        MsgBox("Lot No is Required For Item : " & .Item(Col1Item, I).Value & " At Row No. " & I + 1 & "", MsgBoxStyle.Information)
                        .CurrentCell = .Item(Col1LotNo, I) : Dgl1.Focus()
                        passed = False : Exit Sub
                    End If

                    Dim DtBalance As DataTable = Nothing

                    If CType(AgL.VNull(DtJobEnviro.Rows(0)("IsPostedInJobOrder")), Boolean) = False Then
                        'mQry = " SELECT IFNull(Max(H.IsOrderOfUndefinedQty+0),0) As IsOrderOfUndefinedQty, " & _
                        '        " round(IFNull(sum(L.Qty),0)-(IFNull(sum(VRec.Qty),0)+IFNull(sum(VRec.LossQty),0)),4)  AS BalQty " & _
                        '        " FROM JobOrderDetail L " & _
                        '        " Left Join JobOrder H ON L.JobOrder = H.DocId " & _
                        '        " LEFT JOIN " & _
                        '        " ( " & _
                        '        "   SELECT JRD.JobOrder, JRD.JobOrderSr, IFNull(sum(JRD.Qty),0) AS Qty, IFNull(sum(JRD.LossQty),0) AS LossQty " & _
                        '        "   FROM JobReceiveDetail JRD " & _
                        '        "   WHERE JRD.JobOrder = '" & .Item(Col1JobOrder, I).Tag & "' AND JRD.JobOrderSr = " & .Item(Col1JobOrderSr, I).Value & " " & _
                        '        "   AND JRD.DocId <> '" & mInternalCode & "' " & _
                        '        "   GROUP BY JRD.JobOrder, JRD.JobOrderSr " & _
                        '        " ) VRec ON VRec.JobOrder = L.JobOrder AND VRec.JobOrderSr = L.JobOrderSr " & _
                        '        " WHERE L.JobOrder = '" & .Item(Col1JobOrder, I).Tag & "' " & _
                        '        " AND L.JobOrderSr =  " & .Item(Col1JobOrderSr, I).Value & " "

                        mQry = " SELECT VO.IsOrderOfUndefinedQty, round(IFNull(VO.OrdQty,0)-(IFNull(VR.Qty,0)+IFNull(VR.LossQty,0)),4)  AS BalQty " &
                                " FROM  " &
                                " ( " &
                                " SELECT  L.JobOrder, L.JobOrderSr, IFNull(Max(H.IsOrderOfUndefinedQty+0),0) As IsOrderOfUndefinedQty, sum(L.Qty)  AS OrdQty " &
                                " FROM JobOrderDetail L " &
                                " LEFT JOIN JobOrder H ON H.DocID = L.JobOrder  " &
                                " WHERE L.JobOrder = '" & .Item(Col1JobOrder, I).Tag & "'  AND L.JobOrderSr = " & .Item(Col1JobOrderSr, I).Value & " " &
                                " GROUP BY L.JobOrder, L.JobOrderSr " &
                                " ) VO " &
                                " LEFT JOIN  " &
                                " ( " &
                                "  SELECT JRD.JobOrder, JRD.JobOrderSr, IFNull(sum(JRD.Qty),0) AS Qty, IFNull(sum(JRD.LossQty),0) AS LossQty     " &
                                "  FROM JobReceiveDetail JRD  " &
                                "  WHERE JRD.JobOrder = '" & .Item(Col1JobOrder, I).Tag & "' AND JRD.JobOrderSr = " & .Item(Col1JobOrderSr, I).Value & "  " &
                                " AND JRD.DocId <> '" & mInternalCode & "'  " &
                                "  GROUP BY JRD.JobOrder, JRD.JobOrderSr   " &
                                " ) VR ON VR.JobOrder = VO.JobOrder AND VR.JobOrderSr = VO.JobOrderSr "
                        DtBalance = AgL.FillData(mQry, AgL.GCn).Tables(0)

                        bBalQty = Val(.Item(Col1Qty, I).Value) + Val(.Item(Col1LossQty, I).Value)

                        If Math.Round(AgL.VNull(DtBalance.Rows(0)("BalQty")), 4) < Math.Round(bBalQty, 4) Then
                            If AgL.VNull(DtBalance.Rows(0)("IsOrderOfUndefinedQty")) = 0 Then
                                If CType(AgL.VNull(DtJobEnviro.Rows(0)("AllowExcessOrderQty")), Boolean) = False Then
                                    MsgBox("Balance Order Qty " & AgL.VNull(DtBalance.Rows(0)("BalQty")) & " is Less than " & bBalQty & " At Row No. " & I + 1 & "", MsgBoxStyle.Information)
                                    .CurrentCell = .Item(Col1Qty, I) : Dgl1.Focus()
                                    passed = False : Exit Sub
                                Else
                                    If MsgBox("Balance Order Qty " & AgL.VNull(DtBalance.Rows(0)("BalQty")) & " is Less than " & bBalQty & " At Row No. " & I + 1 & ". Do You Want To Continue ?", MsgBoxStyle.Information + MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                                        .CurrentCell = .Item(Col1Qty, I) : Dgl1.Focus()
                                        passed = False : Exit Sub
                                    End If
                                End If
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

        'For Validation For BOM Detail  
        StrMsg1 = ""
        If CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsPostConsumption")), Boolean) Then
            If CType(AgL.VNull(DtJobEnviro.Rows(0)("IsMandatory_ItemBOM")), Boolean) Then
                With Dgl1
                    For I = 0 To .Rows.Count - 1
                        If .Item(Col1Item, I).Value <> "" Then
                            mQry = " SELECT IFNull(count(*),0) FROM BomDetail WHERE BaseItem = " & AgL.Chk_Text(.Item(Col1Item, I).Tag) & " "
                            If AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar) <= 0 Then
                                If StrMsg1 <> "" Then StrMsg1 += vbCrLf
                                StrMsg1 += "BOM Detail is Not Feeded For Item : " & .Item(Col1Item, I).Value & " At Row No. " & I + 1 & ""
                            End If
                        End If
                    Next
                End With

                If StrMsg1 <> "" Then
                    If MsgBox(StrMsg1 & vbCrLf & " Do you want to Post Same Item in Consumption ?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2) = MsgBoxResult.No Then
                        passed = False : Exit Sub
                    End If
                End If
            End If
        End If

        StrMsg1 = ""
        If CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsMandatory_Machine")), Boolean) Then
            With Dgl1
                For I = 0 To .Rows.Count - 1
                    If .Item(Col1Item, I).Tag <> "" And .Item(Col1Machine, I).Tag = "" Then
                        StrMsg1 += "Machine is Not Feeded For Item : " & .Item(Col1Item, I).Value & " At Row No. " & I + 1 & "" & vbCrLf
                    End If
                Next
            End With

            If StrMsg1 <> "" Then
                If MsgBox(StrMsg1) Then
                    passed = False : Exit Sub
                End If
            End If
        End If

    End Sub

    Private Function FCheckDuplicateRefNo() As Boolean
        FCheckDuplicateRefNo = True
        If Topctrl1.Mode = "Add" Then
            mQry = " SELECT COUNT(*) FROM JobIssRec WHERE ManualRefNo = '" & TxtManualRefNo.Text & "'   " &
                        " AND V_Type ='" & TxtV_Type.AgSelectedValue & "'  And Div_Code = '" & TxtDivision.AgSelectedValue & "' And Site_Code = '" & TxtSite_Code.AgSelectedValue & "' And EntryStatus <> 'Discard' "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then TxtManualRefNo.Text = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "JobIssRec", TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, AgTemplate.ClsMain.ManualRefType.Max) : MsgBox("Reference No. Already Exists New Reference No. Alloted : " & TxtManualRefNo.Text)
        Else
            mQry = " SELECT COUNT(*) FROM JobIssRec WHERE ManualRefNo = '" & TxtManualRefNo.Text & "'  " &
                    " AND V_Type ='" & TxtV_Type.AgSelectedValue & "'  And Div_Code = '" & TxtDivision.AgSelectedValue & "' And Site_Code = '" & TxtSite_Code.AgSelectedValue & "' And IFNull(IsDeleted,0) = 0 AND DocID <>'" & mInternalCode & "'  "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then FCheckDuplicateRefNo = False : MsgBox("Reference No. Already Exists") : TxtManualRefNo.Focus()
        End If
    End Function

    Private Sub FrmProductionOrder_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
        Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
        LblTotalMeasure.Text = 0 : LblTotalQty.Text = 0
        BtnConsumptionDetail.Tag = Nothing
        BtnByProduct.Tag = Nothing
    End Sub

    Private Sub FPostInStock(ByVal Conn As SQLiteConnection, ByVal Cmd As SQLiteCommand)
        Dim StockProcess As AgTemplate.ClsMain.StructStock = Nothing
        Dim Stock As AgTemplate.ClsMain.StructStock = Nothing
        Dim MaxSr As Integer = 0

        If CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsPostedInStock")), Boolean) Then
            mQry = "Delete From Stock Where DocId = '" & mSearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

            'mQry = "INSERT INTO Stock(DocID, Sr, V_Type, V_Prefix, V_Date, V_No, RecID, Div_Code, Site_Code, " & _
            '         " SubCode, Item, LotNo, Godown, Qty_Rec, Unit, MeasurePerPcs, Measure_Rec, MeasureUnit, " & _
            '         " Remarks, Process, Dimension1, Dimension2 ) " & _
            '         " Select L.DocID, row_number() OVER (ORDER BY L.Item),Max(H.V_Type), " & _
            '         " Max(H.V_Prefix), Max(H.V_Date), Max(H.V_No), Max(H.ManualRefNo), Max(H.Div_Code), Max(H.Site_Code),   " & _
            '         " Max(H.JobWorker), L.Item, L.LotNo, Max(H.Godown), Sum(L.Qty), Max(L.Unit), Max(L.MeasurePerPcs), " & _
            '         " Sum(L.TotalMeasure), Max(L.MeasureUnit),   " & _
            '         " Max(L.Remark), CASE WHEN IFNull(P.DontAffectedStock,0) = 0 THEN H.Process ELSE NULL END , L.Dimension1, L.Dimension2 " & _
            '         " From (Select * From JobIssRec Where DocId = '" & mSearchCode & "') H   " & _
            '         " LEFT JOIN JobReceiveDetail L On H.DocId = L.DocId   " & _
            '         " LEFT JOIN Process P ON P.NCat= H.Process " & _
            '         " Group By IFNull(P.DontAffectedStock,0), L.DocId, L.Item, L.LotNo, H.Process, L.Dimension1, L.Dimension2 "

            mQry = "INSERT INTO Stock(DocID, Sr, V_Type, V_Prefix, V_Date, V_No, RecID, Div_Code, Site_Code, " &
                     " SubCode, Item, LotNo, Godown, Qty_Rec, Unit, MeasurePerPcs, Measure_Rec, MeasureUnit, " &
                     " Remarks, Process, Dimension1, Dimension2 ) " &
                     " Select L.DocID, row_number() OVER (ORDER BY L.Item),Max(H.V_Type), " &
                     " Max(H.V_Prefix), Max(H.V_Date), Max(H.V_No), Max(H.ManualRefNo), Max(H.Div_Code), Max(H.Site_Code),   " &
                     " Max(H.JobWorker), IFNull(L.StockItem,L.Item) AS Item, L.LotNo, Max(H.Godown)," &
                     " Sum( CASE WHEN L.Unit <> I.Unit THEN L.DocMeasure ELSE L.Qty END) AS Rec_Qty, " &
                     " Max( CASE WHEN L.Unit <> I.Unit THEN L.MeasureUnit ELSE L.Unit END ) AS Unit, " &
                     " Max(L.MeasurePerPcs), " &
                     " Sum( CASE WHEN L.Unit <> I.Unit THEN L.Qty ELSE L.TotalMeasure END) AS TotalMeasure, " &
                     " Max( CASE WHEN L.Unit <> I.Unit THEN L.Unit ELSE L.MeasureUnit END ) AS Unit, " &
                     " Max(L.Remark), CASE WHEN IFNull(P.DontAffectedStock,0) = 0 THEN H.Process ELSE NULL END , L.Dimension1, L.Dimension2 " &
                     " From (Select * From JobIssRec  Where DocId = '" & mSearchCode & "') H   " &
                     " LEFT JOIN JobReceiveDetail L  On H.DocId = L.DocId   " &
                     " LEFT JOIN Process P  ON P.NCat= H.Process " &
                     " LEFT JOIN Item I  ON I.Code= L.Item " &
                     " Group By IFNull(P.DontAffectedStock,0), L.DocId, L.StockItem, L.Item, L.LotNo, H.Process, L.Dimension1, L.Dimension2 "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If
    End Sub

    Private Sub FPostInStockProcess(ByVal Conn As SQLiteConnection, ByVal Cmd As SQLiteCommand)
        Dim StockProcess As AgTemplate.ClsMain.StructStock = Nothing

        If CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsPostedInStockProcess")), Boolean) Then
            mQry = "INSERT INTO StockProcess(DocID, Sr, V_Type, V_Prefix, V_Date, V_No, RecID, Div_Code, Site_Code, " &
                    " SubCode, Item, LotNo, Godown, Qty_Iss, Unit, MeasurePerPcs, Measure_Iss, MeasureUnit, " &
                    " Remarks, Process, Dimension1, Dimension2 ) " &
                    " Select L.DocID, row_number() OVER (ORDER BY L.Item),Max(H.V_Type), " &
                    " Max(H.V_Prefix), Max(H.V_Date), Max(H.V_No), Max(H.ManualRefNo), Max(H.Div_Code), Max(H.Site_Code),   " &
                    " Max(H.JobWorker), L.Item, L.LotNo, Max(H.Godown), Sum(L.Qty)+ IFNull(Sum(L.LossQty),0), Max(L.Unit), Max(L.MeasurePerPcs), " &
                    " Sum(L.TotalMeasure), Max(L.MeasureUnit),   " &
                    " Max(Remark), H.Process , L.Dimension1, L.Dimension2 " &
                    " From (Select * From JobIssRec Where DocId = '" & mSearchCode & "') H   " &
                    " LEFT JOIN JobReceiveDetail L On H.DocId = L.DocId   " &
                    " Group By L.DocId, L.Item, L.LotNo, H.Process, L.Dimension1, L.Dimension2 "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If
    End Sub

    Private Sub FPostInStockVirtual(ByVal Conn As SQLiteConnection, ByVal Cmd As SQLiteCommand)
        If CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsPostedInStockVirtual")), Boolean) Then
            mQry = "Delete From StockVirtual Where DocId = '" & mInternalCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

            mQry = "INSERT INTO StockVirtual(DocID, Sr, V_Type, V_Prefix, V_Date, V_No, RecID, Div_Code, Site_Code, " &
                    " SubCode, Item, LotNo, Godown, Qty_Iss, Unit, MeasurePerPcs, Measure_Iss, MeasureUnit, " &
                    " Remarks, Process, CostCenter, Dimension1, Dimension2) " &
                    " Select L.DocID, row_number() OVER (ORDER BY L.Item),Max(H.V_Type), " &
                    " Max(H.V_Prefix), Max(H.V_Date), Max(H.V_No), Max(H.ManualRefNo), Max(H.Div_Code), Max(H.Site_Code),   " &
                    " Max(H.JobWorker), L.Item, L.LotNo, Max(H.Godown), Sum(L.Qty), Max(L.Unit), Max(L.MeasurePerPcs), " &
                    " Sum(L.TotalMeasure), Max(L.MeasureUnit),   " &
                    " Max(Remark), H.Process, Max(J.CostCenter) As CostCenter , L.Dimension1, L.Dimension2 " &
                    " From (Select * From JobIssRec Where DocId = '" & mSearchCode & "') H   " &
                    " LEFT JOIN JobReceiveDetail L On H.DocId = L.DocId   " &
                    " LEFT JOIN JobOrder J On L.JobOrder = J.DocId " &
                    " Group By L.DocId, L.LotNo, L.Item, H.Process, L.Dimension1, L.Dimension2 "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If
    End Sub

    Private Sub TempJobOrder_BaseEvent_ApproveDeletion_InTrans(ByVal SearchCode As String, ByVal Conn As SQLiteConnection, ByVal Cmd As SQLiteCommand) Handles Me.BaseEvent_ApproveDeletion_InTrans
        Dim I As Integer = 0
        'For I = 0 To Dgl1.Rows.Count - 1
        '    If Dgl1.Item(Col1Item_Uid, I).Tag <> "" Then
        '        AgTemplate.ClsMain.FUpdateItem_UidOnDelete(Dgl1.Item(Col1Item_Uid, I).Tag, mSearchCode, Conn, Cmd)
        '    End If
        'Next

        mQry = " Delete from Stock Where DocId = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Delete From StockProcess Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " UPDATE JobReceiveDetail " &
                " Set " &
                " JobOrder = NULL, " &
                " JobOrderSr = NULL " &
                " Where DocId = '" & mSearchCode & "' " &
                " And JobOrder  = '" & mSearchCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " UPDATE JobReceiveBOM " &
                " Set " &
                " JobOrder = NULL " &
                " Where DocId = '" & mSearchCode & "' " &
                " And JobOrder  = '" & mSearchCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Delete From JobOrderDetail Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Delete From JobOrder  Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Delete From JobReceiveByProduct  Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Delete From JobReceiveDetail Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Delete From JobIssRec  Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)


        mQry = "Delete From JobReceiveBom Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)


        mQry = "Delete From Dues Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Delete From JobIssRecUid Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " UPDATE JobIssRecUid Set JobRecDocID = Null Where JobRecDocID = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    End Sub

    Private Sub TempJobReceive_BaseEvent_Topctrl_tbRef() Handles Me.BaseEvent_Topctrl_tbRef
        Try
            If TxtGodown.AgHelpDataSet IsNot Nothing Then TxtGodown.AgHelpDataSet.Dispose() : TxtGodown.AgHelpDataSet = Nothing
            If TxtJobWorker.AgHelpDataSet IsNot Nothing Then TxtJobWorker.AgHelpDataSet.Dispose() : TxtJobWorker.AgHelpDataSet = Nothing
            If TxtProcess.AgHelpDataSet IsNot Nothing Then TxtProcess.AgHelpDataSet.Dispose() : TxtProcess.AgHelpDataSet = Nothing
            If Dgl1.AgHelpDataSet(Col1Item) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1Item).Dispose() : Dgl1.AgHelpDataSet(Col1Item) = Nothing
            If Dgl1.AgHelpDataSet(Col1JobOrder) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1JobOrder).Dispose() : Dgl1.AgHelpDataSet(Col1JobOrder) = Nothing
            If TxtJobReceiveBy.AgHelpDataSet IsNot Nothing Then TxtJobReceiveBy.AgHelpDataSet.Dispose() : TxtJobReceiveBy.AgHelpDataSet = Nothing
        Catch ex As Exception
        End Try
    End Sub

    Private Sub TxtJobWorker_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtGodown.KeyDown, TxtJobWorker.KeyDown, TxtProcess.KeyDown, TxtJobReceiveBy.KeyDown, TxtMachine.KeyDown
        Try
            Select Case sender.name
                Case TxtGodown.Name
                    If e.KeyCode <> Keys.Enter Then
                        If TxtGodown.AgHelpDataSet Is Nothing Then
                            mQry = "SELECT G.Code, G.Description, Sm.ManualCode As Site, G.Site_Code, G.Div_Code, IFNull(G.IsDeleted,0) as IsDeleted, " &
                                    " IFNull(G.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') AS Status " &
                                    " FROM Godown G  " &
                                    " LEFT JOIN SiteMast Sm  On G.Site_Code = Sm.Code " &
                                    " Where IFNull(G.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " &
                                    " And Site_Code = '" & TxtSite_Code.AgSelectedValue & "' " &
                                    " And IFNull(G.IsDeleted,0) = 0 "
                            TxtGodown.AgHelpDataSet(4, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If

                Case TxtJobWorker.Name
                    If e.KeyCode <> Keys.Enter Then
                        If sender.AgHelpDataSet Is Nothing Then
                            mQry = " SELECT Sg.SubCode AS Code, Sg.Name + ',' + IFNull(C.CityName,'') AS JobWorker, H.Process, " &
                                     " IFNull(Sg.IsDeleted,0) AS IsDeleted,  SG.Div_Code, " &
                                     " IFNull(Sg.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') As Status " &
                                     " FROM SubGroup Sg  " &
                                     " LEFT JOIN JobWorkerProcess H   On Sg.SubCode = H.SubCode  " &
                                     " LEFT JOIN City C ON Sg.CityCode = C.CityCode  " &
                                     " Where IFNull(Sg.IsDeleted,0) = 0 " &
                                     " And Sg.Status = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " &
                                     " And CharIndex('|' + '" & TxtDivision.Tag & "' + '|', IFNull(Sg.DivisionList,'|' + '" & TxtDivision.Tag & "' + '|')) > 0 " &
                                     " And H.Process = '" & TxtProcess.Tag & "' "
                            sender.AgHelpDataSet(4, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If

                Case TxtMachine.Name
                    'If e.KeyCode <> Keys.Enter Then
                    '    If sender.AgHelpDataset Is Nothing Then
                    '        mQry = " SELECT Sg.SubCode AS Code, Sg.Name As JobWorker " & _
                    '                   " FROM SubGroup Sg  " & _
                    '                   " LEFT JOIN JobWorkerProcess H   On Sg.SubCode = H.SubCode  " & _
                    '                   " Where IFNull(Sg.IsDeleted,0) = 0 " & _
                    '                   " And Sg.Status = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & _
                    '                   " And CharIndex('|' + '" & TxtDivision.Tag & "' + '|', IFNull(Sg.DivisionList,'|' + '" & TxtDivision.Tag & "' + '|')) > 0 " & _
                    '                   " And H.Process = '" & TxtProcess.Tag & "' AND SG.MasterType = '" & AgTemplate.ClsMain.SubgroupType.Machine & "' "
                    '        sender.AgHelpDataSet(0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
                    '    End If
                    'End If

                Case TxtProcess.Name
                    If e.KeyCode <> Keys.Enter Then
                        If TxtProcess.AgHelpDataSet Is Nothing Then
                            mQry = " SELECT H.NCat AS Code, H.Description AS Process FROM Process H "
                            TxtProcess.AgHelpDataSet(0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If

                Case TxtJobReceiveBy.Name
                    If sender.AgHelpDataSet Is Nothing Then
                        mQry = " SELECT L.SubCode AS Code, L.DispName AS OrderBy " &
                                    " FROM SubGroup L   " &
                                    " Where IFNull(L.IsDeleted,0) = 0 AND MasterType = '" & AgTemplate.ClsMain.SubgroupType.Employee & "'" &
                                    " And IFNull(L.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' "
                        sender.AgHelpDataSet(0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Dgl1_EditingControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.EditingControl_KeyDown
        Try
            If AgL.StrCmp(Topctrl1.Mode, "Browse") Then Exit Sub
            If Dgl1.CurrentCell Is Nothing Then Exit Sub
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Item
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1Item) Is Nothing Then
                            If CType(AgL.VNull(DtJobEnviro.Rows(0)("IsPostedInJobOrder")), Boolean) Then
                                FCreateHelpItemFromMaster()
                            Else
                                FCreateHelpItem()
                            End If
                        End If
                    End If

                Case Col1StockItem
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1StockItem) Is Nothing Then
                            FCreateHelpStockItem()
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

                Case Col1Machine
                    'If e.KeyCode <> Keys.Enter Then
                    '    If Dgl1.AgHelpDataSet(Col1Machine) Is Nothing Then
                    '        mQry = " SELECT Sg.SubCode AS Code, Sg.Name As JobWorker " & _
                    '                   " FROM SubGroup Sg  " & _
                    '                   " LEFT JOIN JobWorkerProcess H   On Sg.SubCode = H.SubCode  " & _
                    '                   " Where IFNull(Sg.IsDeleted,0) = 0 " & _
                    '                   " And Sg.Status = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & _
                    '                   " And CharIndex('|' + '" & TxtDivision.Tag & "' + '|', IFNull(Sg.DivisionList,'|' + '" & TxtDivision.Tag & "' + '|')) > 0 " & _
                    '                   " And H.Process = '" & TxtProcess.Tag & "' AND SG.MasterType = '" & AgTemplate.ClsMain.SubgroupType.Machine & "' "
                    '        Dgl1.AgHelpDataSet(Col1Machine) = AgL.FillData(mQry, AgL.GCn)
                    '    End If
                    'End If
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

        Dim MainCondStr As String = ""
        If TxtMachine.Tag <> "" Then
            MainCondStr = " AND Machine = '" & TxtMachine.Tag & "' "
        End If
        mQry = " SELECT Max(L.Item) As Code, Max(I.Description) As Description, Max(L.LotNo) As LotNo,  " &
                " Max(H.V_Type) + '-' +  Max(H.ManualRefNo) As JobOrderNo,   " &
                " Max(H.V_Date) as JobOrderDate,  " &
                " Max(D1.Description) As " & AgTemplate.ClsMain.FGetDimension1Caption() & ", " &
                " Max(D2.Description) As " & AgTemplate.ClsMain.FGetDimension2Caption() & ", " &
                " Sum(L.Qty) - IFNull(Sum(Cd.Qty), 0) as [Bal.Qty],  Max(IG.Description) AS ItemGroupDesc,  " &
                " Case When Max(L.Unit) <> Max(I.Unit) THEN Max(L.Unit) Else Max(I.Unit) END AS Unit, " &
                " Sum(L.TotalMeasure) - IFNull(Sum(Cd.TotalMeasure), 0) as [Bal.Measure],  Max(H.Machine) AS Machine,  Max(SGM.DispName) AS MachineName, " &
                " Case When Max(L.Unit) <> Max(I.Unit) THEN Max(I.Unit) Else Max(I.MeasureUnit) END AS MeasureUnit, " &
                " Max(L.Rate) as Rate,   " &
                " Max(I.SalesTaxPostingGroup) SalesTaxPostingGroup, L.JobOrder,   " &
                " Max(L.MeasurePerPcs) as MeasurePerPcs, " &
                " Max(L.ProdOrder) As ProdOrder, Max(Po.ManualRefNo) As ProdOrderNo, " &
                " L.JobOrderSr, Max(U.DecimalPlaces) as QtyDecimalPlaces,  " &
                " Max(U1.DecimalPlaces) as MeasureDecimalPlaces, Max(L.ProdOrderSr) As ProdOrderSr, " &
                " Max(L.Dimension1) As Dimension1, Max(L.Dimension2) As Dimension2, Max(L.IncentiveRate) as IncentiveRate  " &
                " FROM (  " &
                "     SELECT DocID, V_Type, ManualRefNo, Machine, V_Date, IsOrderOfUndefinedQty  " &
                "     FROM JobOrder    " &
                "     WHERE JobWorker ='" & TxtJobWorker.Tag & "'   " &
                "     And Process = '" & TxtProcess.Tag & "'   " &
                "     And Div_Code = '" & TxtDivision.Tag & "'   " &
                "     AND Site_Code = '" & TxtSite_Code.Tag & "'   " &
                "     " & MainCondStr & "    " &
                "     AND V_Date <= '" & TxtV_Date.Text & "'   " &
                "     And IFNull(Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "'  " &
                "     ) H   " &
                " LEFT JOIN JobOrderDetail L  ON H.DocID = L.JobOrder  " &
                " LEFT JOIN ProdOrder Po  ON L.ProdOrder = Po.DocId " &
                " Left Join Item I  On L.Item  = I.Code   " &
                " LEFT JOIN ItemGroup IG  On Ig.Code = I.ItemGroup " &
                " LEFT JOIN Voucher_Type Vt  ON H.V_Type = Vt.V_Type    " &
                " Left Join (   " &
                "     SELECT L.JobOrder, L.JobOrderSr, Sum(L.Qty) + IFNull(Sum(L.LossQty),0) AS Qty, Sum(L.TotalMeasure) As TotalMeasure " &
                " 	  FROM JobReceiveDetail L     " &
                "     Where L.DocId <> '" & mSearchCode & "'  " &
                " 	  GROUP BY L.JobOrder, L.JobOrderSr   " &
                " 	) AS CD ON L.DocId = CD.JobOrder AND L.Sr = CD.JobOrderSr   " &
                " LEFT JOIN Unit U  On L.Unit = U.Code   " &
                " LEFT JOIN Unit U1  On L.MeasureUnit = U1.Code   " &
                " LEFT JOIN SubGroup SGM  On H.Machine = SGM.SubCode   " &
                " Left Join Dimension1 D1  On L.Dimension1 = D1.Code " &
                " Left Join Dimension2 D2  On L.Dimension2 = D2.Code " &
                " WHERE 1=1  " & strCond &
                " GROUP BY L.JobOrder, L.JobOrderSr  " &
                " Having (ROUND(Sum(L.Qty),4) - ROUND(IFNull(Max(Cd.Qty), 0),4) > 0 Or IFNull(Max(IsOrderOfUndefinedQty + 0),0) <> 0) " &
                " Order By JobOrderDate  "
        'L.Qty - IFNull(Cd.Qty, 0) > 0
        Dgl1.AgHelpDataSet(Col1Item, 16) = AgL.FillData(mQry, AgL.GCn)
    End Sub

    Private Sub FCreateHelpItemFromMaster()
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

        mQry = " SELECT I.Code, I.Description, IG.Description AS ItemGroupDesc, NULL AS LotNo, NULL AS JobOrderNo, NULL AS JobOrderDate, 0 AS [Bal.Qty], " &
                " I.Unit, 0 AS [Bal.Measure], I.MeasureUnit, 0 AS Rate, 0 As IncentiveRate, NULL AS SaleTaxPostingGroup, NULL AS JobOrder, NULL AS LotNo, " &
                " I." & mMeasureField & " AS MeasurePerPcs,   NULL AS ProdOrder, NULL AS ProdOrderNo, NULL AS JObOrderSr,  " &
                " U.DecimalPlaces AS QtyDecimalPlaces,  NULL AS ProdOrderSr, UM.DecimalPlaces AS MeasureDecimalPlaces,   NULL AS Machine, NULL AS MachineName, " &
                " Null As Dimension1, Null As " & AgTemplate.ClsMain.FGetDimension1Caption() & ", Null As Dimension2, Null As " & AgTemplate.ClsMain.FGetDimension2Caption() & " " &
                " FROM Item I  " &
                " LEFT JOIN ItemGroup IG On Ig.Code = I.ItemGroup" &
                " LEFT JOIN Unit U ON U.Code = I.Unit  " &
                " LEFT JOIN Unit UM ON UM.Code = I.MeasureUnit  " &
                " Where 1= 1 And IFNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') <= '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond & " " &
                " ORDER BY I.Description "
        Dgl1.AgHelpDataSet(Col1Item, 24) = AgL.FillData(mQry, AgL.GcnRead)
    End Sub

    Private Sub FCreateHelpStockItem()
        Dim strCond As String = ""
        'If DtV_TypeSettings.Rows.Count > 0 Then
        '    If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")) <> "" Then
        '        strCond += " And CharIndex('|' + I.ItemType + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")) & "') > 0 "
        '    End If

        '    If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemGroup")) <> "" Then
        '        strCond += " And CharIndex('|' + I.ItemGroup + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemGroup")) & "') > 0 "
        '    End If

        '    If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_ItemGroup")) <> "" Then
        '        strCond += " And CharIndex('|' + I.ItemGroup + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_ItemGroup")) & "') <= 0 "
        '    End If

        '    If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Item")) <> "" Then
        '        strCond += " And CharIndex('|' + I.Code + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Item")) & "') > 0 "
        '    End If

        '    If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_Item")) <> "" Then
        '        strCond += " And CharIndex('|' + I.Code + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_Item")) & "') <= 0 "
        '    End If

        '    If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemDivision")) <> "" Then
        '        strCond += " And CharIndex('|' + I.Div_Code + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemDivision")) & "') > 0 "
        '    End If

        '    If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemSite")) <> "" Then
        '        strCond += " And CharIndex('|' + I.Site_Code + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemSite")) & "') > 0 "
        '    End If
        'End If

        mQry = " SELECT I.Code, I.Description, IG.Description AS ItemGroupDesc, NULL AS LotNo, NULL AS JobOrderNo, NULL AS JobOrderDate, 0 AS [Bal.Qty], " &
                " I.Unit, 0 AS [Bal.Measure], I.MeasureUnit, 0 AS Rate, 0 As IncentiveRate, NULL AS SaleTaxPostingGroup, NULL AS JobOrder, NULL AS LotNo, " &
                " I." & mMeasureField & " AS MeasurePerPcs,   NULL AS ProdOrder, NULL AS ProdOrderNo, NULL AS JObOrderSr,  " &
                " U.DecimalPlaces AS QtyDecimalPlaces,  NULL AS ProdOrderSr, UM.DecimalPlaces AS MeasureDecimalPlaces,   NULL AS Machine, NULL AS MachineName, " &
                " Null As Dimension1, Null As " & AgTemplate.ClsMain.FGetDimension1Caption() & ", Null As Dimension2, Null As " & AgTemplate.ClsMain.FGetDimension2Caption() & " " &
                " FROM Item I  " &
                " LEFT JOIN ItemGroup IG On Ig.Code = I.ItemGroup" &
                " LEFT JOIN Unit U ON U.Code = I.Unit  " &
                " LEFT JOIN Unit UM ON UM.Code = I.MeasureUnit  " &
                " Where 1= 1 And IFNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') <= '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond & " " &
                " ORDER BY I.Description "
        Dgl1.AgHelpDataSet(Col1StockItem, 24) = AgL.FillData(mQry, AgL.GcnRead)
    End Sub

    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub

    Private Sub BtnFillSaleChallan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnFillJobOrder.Click
        Try
            If Topctrl1.Mode = "Browse" Then Exit Sub
            Dim StrTicked As String = ""

            If RbtForJobOrderItems.Checked Then
                StrTicked = FHPGD_PendingJobOrderItems()
            Else
                StrTicked = FHPGD_PendingJobOrder()
            End If

            If StrTicked <> "" Then
                FFillItemsForOrder(StrTicked)
            Else
                Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
            End If

            Dgl1.Focus()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function FHPGD_PendingJobOrder() As String
        Dim FRH_Multiple As DMHelpGrid.FrmHelpGrid_Multi
        Dim StrRtn As String = ""
        Dim strCond As String = ""

        strCond = " And JobWorker = '" & TxtJobWorker.Tag & "'   " &
                    " And Process = '" & TxtProcess.Tag & "' " &
                    " And Div_Code = '" & TxtDivision.Tag & "'   " &
                    " AND Site_Code = '" & TxtSite_Code.Tag & "'   " &
                    " AND V_Date <= '" & TxtV_Date.Text & "'  " &
                    " And IFNull(Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "'"

        mQry = " SELECT 'o' As Tick, VMain.JobOrder, Max(VMain.JobOrderNo) AS JobOrderNo, " &
                " Max(VMain.JobOrderDate) AS JobOrderDate, Round(IFNull(Sum(VMain.Qty), 0),4) As [Qty]    " &
                " FROM ( " & FRetFillItemWiseQry(strCond, "") & " ) As VMain " &
                " GROUP BY VMain.JobOrder " &
                " Order By JobOrderDate "

        FRH_Multiple = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(AgL.FillData(mQry, AgL.GCn).TABLES(0)), "", 400, 500, , , False)
        FRH_Multiple.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple.FFormatColumn(1, , 0, , False)
        FRH_Multiple.FFormatColumn(2, "Order No.", 150, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(3, "Order Date", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(4, "Balance", 100, DataGridViewContentAlignment.MiddleRight)

        FRH_Multiple.StartPosition = FormStartPosition.CenterScreen
        FRH_Multiple.ShowDialog()

        If FRH_Multiple.BytBtnValue = 0 Then
            StrRtn = FRH_Multiple.FFetchData(1, "'", "'", ",", True)
        End If
        FHPGD_PendingJobOrder = StrRtn

        FRH_Multiple = Nothing
    End Function

    Private Function FHPGD_PendingJobOrderItems() As String
        Dim FRH_Multiple As DMHelpGrid.FrmHelpGrid_Multi
        Dim StrRtn As String = ""
        Dim strCond$ = ""

        strCond = " And JobWorker = '" & TxtJobWorker.Tag & "'   " &
                    " And Process = '" & TxtProcess.Tag & "' " &
                    " And Div_Code = '" & TxtDivision.Tag & "'   " &
                    " AND Site_Code = '" & TxtSite_Code.Tag & "'   " &
                    " AND V_Date <= '" & TxtV_Date.Text & "'  " &
                    " And IFNull(Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "'"

        mQry = " SELECT 'o' As Tick, VMain.JobOrder + Convert(nVarChar, VMain.JobOrderSr) As JobOrderDocIdSr, " &
                " Max(VMain.JobOrderNo) AS JobOrderNo,  " &
                " Max(VMain.JobOrderDate) AS JobOrderDate, Max(VMain.Description) As ItemDesc, Max(VMain.LotNo) As LotNo, " &
                " Max(VMain.Dimension1Desc) AS " & AgTemplate.ClsMain.FGetDimension1Caption() & ", Max(VMain.Dimension2Desc) AS " & AgTemplate.ClsMain.FGetDimension2Caption() & ", " &
                " ROUND(IFNull(Sum(VMain.Qty), 0),4) As [Qty]    " &
                " FROM ( " & FRetFillItemWiseQry(strCond, "") & " ) As VMain " &
                " GROUP BY VMain.JobOrder, VMain.JobOrderSr " &
                " Order By JobOrderDate "

        FRH_Multiple = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(AgL.FillData(mQry, AgL.GCn).TABLES(0)), "", 500, 940, , , False)
        FRH_Multiple.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple.FFormatColumn(1, , 0, , False)
        FRH_Multiple.FFormatColumn(2, "Order No.", 120, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(3, "Order Date", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(4, "Item", 200, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(5, "Lot No", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(6, AgTemplate.ClsMain.FGetDimension1Caption(), 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(7, AgTemplate.ClsMain.FGetDimension2Caption(), 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(8, "Balance", 100, DataGridViewContentAlignment.MiddleRight)

        FRH_Multiple.StartPosition = FormStartPosition.CenterScreen
        FRH_Multiple.ShowDialog()

        If FRH_Multiple.BytBtnValue = 0 Then
            StrRtn = FRH_Multiple.FFetchData(1, "'", "'", ",", True)
        End If
        FHPGD_PendingJobOrderItems = StrRtn

        FRH_Multiple = Nothing
    End Function

    Private Sub FFillItemsForOrder(ByVal bOrderNoStr As String)
        Dim I As Integer = 0
        Dim DtTemp As DataTable = Nothing
        Try
            If bOrderNoStr = "" Then Exit Sub

            If RbtForJobOrderItems.Checked Then
                mQry = FRetFillItemWiseQry("", " And L.JobOrder + Convert(nVarChar, L.Sr) In (" & bOrderNoStr & ")")
            Else
                mQry = FRetFillItemWiseQry(" And DocId In (" & bOrderNoStr & ") ", "")
            End If

            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

            With DtTemp
                Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
                If .Rows.Count > 0 Then
                    For I = 0 To .Rows.Count - 1
                        Dgl1.Rows.Add()
                        Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count - 1
                        Dgl1.Item(Col1JobOrder, I).Tag = AgL.XNull(.Rows(I)("JobOrder"))
                        Dgl1.Item(Col1JobOrder, I).Value = AgL.XNull(.Rows(I)("JobOrderNo"))
                        Dgl1.Item(Col1JobOrderSr, I).Value = AgL.XNull(.Rows(I)("JobOrderSr"))

                        Dgl1.Item(Col1ProdOrder, I).Tag = AgL.XNull(.Rows(I)("ProdOrder"))
                        Dgl1.Item(Col1ProdOrder, I).Value = AgL.XNull(.Rows(I)("ProdOrderNo"))
                        Dgl1.Item(Col1ProdOrderSr, I).Value = AgL.XNull(.Rows(I)("ProdOrderSr"))


                        Dgl1.Item(Col1Dimension1, I).Tag = AgL.XNull(.Rows(I)("Dimension1"))
                        Dgl1.Item(Col1Dimension1, I).Value = AgL.XNull(.Rows(I)("Dimension1Desc"))

                        Dgl1.Item(Col1Dimension2, I).Tag = AgL.XNull(.Rows(I)("Dimension2"))
                        Dgl1.Item(Col1Dimension2, I).Value = AgL.XNull(.Rows(I)("Dimension2Desc"))

                        Dgl1.Item(Col1Machine, I).Tag = AgL.XNull(.Rows(I)("Machine"))
                        Dgl1.Item(Col1Machine, I).Value = AgL.XNull(.Rows(I)("MachineName"))

                        Dgl1.Item(Col1LotNo, I).Value = AgL.XNull(.Rows(I)("LotNo"))
                        Dgl1.Item(Col1JobOrderLotNo, I).Value = AgL.XNull(.Rows(I)("LotNo"))
                        Dgl1.Item(Col1Item_Uid, I).Tag = AgL.XNull(.Rows(I)("Item_Uid"))
                        Dgl1.Item(Col1Item_Uid, I).Value = AgL.XNull(.Rows(I)("Item_UidDesc"))
                        Dgl1.Item(Col1ItemGroup, I).Value = AgL.XNull(.Rows(I)("ItemGroupDesc"))
                        'Dgl1.Item(Col1Item_Uid, I).Value = AgL.XNull(AgL.Dman_Execute("Select Item_Uid From Item_Uid Where Code = '" & AgL.XNull(.Rows(I)("Item_Uid")) & "' ", AgL.GCn).ExecuteScalar)
                        Dgl1.Item(Col1QtyDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("QtyDecimalPlaces"))
                        Dgl1.Item(Col1MeasureDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("MeasureDecimalPlaces"))
                        Dgl1.Item(Col1Item, I).Tag = AgL.XNull(.Rows(I)("Code"))
                        Dgl1.Item(Col1Item, I).Value = AgL.XNull(.Rows(I)("Description"))
                        Dgl1.Item(Col1Rate, I).Value = AgL.VNull(.Rows(I)("Rate"))
                        Dgl1.Item(Col1DocQty, I).Value = Format(AgL.VNull(.Rows(I)("Qty")), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
                        Dgl1.Item(Col1BillQty, I).Value = Format(AgL.VNull(.Rows(I)("Qty")), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
                        Dgl1.Item(Col1Qty, I).Value = Format(AgL.VNull(.Rows(I)("Qty")), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
                        Dgl1.Item(Col1Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                        Dgl1.Item(Col1MeasurePerPcs, I).Value = Format(AgL.VNull(.Rows(I)("MeasurePerPcs")), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                        Dgl1.Item(Col1MeasureUnit, I).Value = AgL.XNull(.Rows(I)("MeasureUnit"))

                        AgCalcGrid1.AgChargesValue(AgTemplate.ClsMain.Charges.INCENTIVE, I, AgStructure.AgCalcGrid.LineColumnType.Percentage) = Format(AgL.VNull(.Rows(I)("IncentiveRate")), "0.00")

                    Next I
                End If
            End With
            AgCalcGrid1.Calculation(True)
            Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function FRetFillItemWiseQry(ByVal HeaderConStr As String, ByVal LineConStr As String) As String
        If TxtMachine.Tag <> "" Then
            HeaderConStr = HeaderConStr & " AND Machine = '" & TxtMachine.Tag & "' "
        End If

        FRetFillItemWiseQry = " SELECT Max(L.Item_Uid) As Item_Uid, Max(L.Item) As Code, Max(I.Description) as Description, " &
                    " Max(I.ManualCode) As ManualCode,  Max(L.LotNo) AS LotNo, " &
                    " Max(H.V_Type) + '-' +  Max(H.ManualRefNo) AS JobOrderNo,   " &
                    " Max(H.V_Date) as JobOrderDate,  " &
                    " Round(Sum(L.Qty),4) - round(IFNull(Max(Cd.Qty), 0),4) As Qty,   " &
                    " Case When Max(L.Unit) <> Max(I.Unit) THEN Max(L.Unit) Else Max(I.Unit) END AS Unit, " &
                    " Case When Max(L.Unit) <> Max(I.Unit) THEN Max(I.Unit) Else Max(I.MeasureUnit) END AS MeasureUnit, " &
                    " Max(L.Rate) as Rate,  Max(L.IncentiveRate) as IncentiveRate, Max(H.Machine) AS Machine,  Max(SGM.DispName) AS MachineName, " &
                    " L.JobOrder, Max(IG.Description) AS ItemGroupDesc,  " &
                    " Max(L.MeasurePerPcs) as MeasurePerPcs, " &
                    " L.JobOrderSr,   " &
                    " Max(L.ProdOrder) As ProdOrder, Max(Po.ManualRefNo) As ProdOrderNo, " &
                    " Max(U.DecimalPlaces) as QtyDecimalPlaces,  " &
                    " Max(U1.DecimalPlaces) as MeasureDecimalPlaces, " &
                    " Max(Iu.Item_Uid) As Item_UidDesc, " &
                    " Max(L.ProdOrderSr) As ProdOrderSr, " &
                    " Max(L.Dimension1) As Dimension1, Max(D1.Description) As Dimension1Desc, " &
                    " Max(L.Dimension2) As Dimension2, Max(D2.Description) As Dimension2Desc " &
                    " FROM (  " &
                    "     SELECT DocID, V_Type, ManualRefNo, V_Date, Status, Machine, IsOrderOfUndefinedQty   " &
                    "     FROM JobOrder  Where 1=1 " & HeaderConStr & " " &
                    "     ) H   " &
                    " LEFT JOIN JobOrderDetail L  ON H.DocID = L.JobOrder    " &
                    " LEFT JOIN ProdOrder Po  ON L.ProdOrder = Po.DocId " &
                    " Left Join Item I  On L.Item  = I.Code   " &
                    " LEFT JOIN ItemGroup IG On Ig.Code = I.ItemGroup" &
                    " LEFT JOIN Item_Uid Iu On L.Item_Uid = Iu.Code " &
                    " LEFT JOIN Voucher_Type Vt  ON H.V_Type = Vt.V_Type    " &
                    " Left Join (   " &
                    "     SELECT L.JobOrder, L.JobOrderSr, sum(L.Qty) + IFNull(sum(L.LossQty),0) AS Qty " &
                    " 	  FROM JobReceiveDetail L     " &
                    "     LEFT JOIN JobIssRec H  ON L.DocId = H.DocID  " &
                    "     WHERE L.DocId <> '" & mSearchCode & "' " &
                    "     And H.JobWorker = '" & TxtJobWorker.Tag & "'  " &
                    " 	  GROUP BY L.JobOrder, L.JobOrderSr   " &
                    " 	) AS CD ON L.JobOrder + Convert(nVarChar,L.JobOrderSr) = CD.JobOrder + Convert(nVarChar,CD.JobOrderSr) " &
                    " LEFT JOIN Unit U  On L.Unit = U.Code   " &
                    " LEFT JOIN Unit U1  On L.MeasureUnit = U1.Code   " &
                    " LEFT JOIN SubGroup SGM  On H.Machine = SGM.SubCode  " &
                    " Left Join Dimension1 D1 On L.Dimension1 = D1.Code " &
                    " Left Join Dimension2 D2 On L.Dimension2 = D2.Code " &
                    " WHERE 1 = 1 " & LineConStr &
                    " GROUP BY L.JobOrder, L.JobOrderSr  " &
                    " HAVING (round(IFNull(Sum(L.Qty),0),4) - round(IFNull(Max(Cd.Qty), 0),4) > 0  Or IFNull(Max(IsOrderOfUndefinedQty + 0),0) <> 0)   "

        '" 	) AS CD ON L.JobOrder = CD.JobOrder AND L.JobOrderSr = CD.JobOrderSr   " & _
    End Function

    Private Sub Dgl1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellEnter
        Try
            If AgL.StrCmp(Topctrl1.Mode, "Browse") Then Exit Sub
            If Dgl1.CurrentCell Is Nothing Then Exit Sub
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Qty, Col1BillQty
                    CType(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex), AgControls.AgTextColumn).AgNumberRightPlaces = Val(Dgl1.Item(Col1QtyDecimalPlaces, Dgl1.CurrentCell.RowIndex).Value)

                Case Col1MeasurePerPcs, Col1TotalMeasure, Col1BillMeasure
                    CType(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex), AgControls.AgTextColumn).AgNumberRightPlaces = Val(Dgl1.Item(Col1MeasureDecimalPlaces, Dgl1.CurrentCell.RowIndex).Value)

                Case Col1LotNo
                    If Dgl1.Item(Col1JobOrderLotNo, Dgl1.CurrentCell.RowIndex).Value <> "" Then
                        Dgl1.Item(Col1LotNo, Dgl1.CurrentCell.RowIndex).ReadOnly = True
                    Else
                        Dgl1.Item(Col1LotNo, Dgl1.CurrentCell.RowIndex).ReadOnly = False
                    End If

                Case Col1Dimension1, Col1Dimension2
                    If Dgl1.Item(Col1ProdOrder, Dgl1.CurrentCell.RowIndex).Value <> "" Then
                        Dgl1.Columns(Col1Dimension1).ReadOnly = True
                        Dgl1.Columns(Col1Dimension2).ReadOnly = True
                    Else
                        Dgl1.Columns(Col1Dimension1).ReadOnly = False
                        Dgl1.Columns(Col1Dimension2).ReadOnly = False
                    End If

                Case Col1Item
                    If Dgl1.AgHelpDataSet(Col1Item) IsNot Nothing Then
                        Dgl1.AgRowFilter(Dgl1.Columns(Col1Item).Index) = FFilterUsedItems()
                    End If

            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function FFilterUsedItems() As String
        Dim I As Integer = 0
        FFilterUsedItems = " 1=1 "

        Try
            With Dgl1
                For I = 0 To .Rows.Count - 1
                    If .Item(Col1Item, I).Value <> "" Then
                        FFilterUsedItems += " And Code +  IFNull(Dimension1,'') +  IFNull(Dimension2,'') +  IFNull(LotNo,'') + IFNull(Machine,'') +  IFNull(JobOrder,'') +  IFNull(JobOrderSr,'') <> '" & Dgl1.AgSelectedValue(Col1Item, I) & "' + '" & Dgl1.Item(Col1Dimension1, I).Tag & "' + '" & Dgl1.Item(Col1Dimension2, I).Tag & "' + '" & Dgl1.Item(Col1LotNo, I).Value & "' +'" & Dgl1.Item(Col1Machine, I).Value & "' + '" & Dgl1.Item(Col1JobOrder, I).Tag & "' + '" & Dgl1.Item(Col1JobOrderSr, I).Value & "'"
                    End If
                Next
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function

    Private Sub Validating_Item_Uid(ByVal Item_Uid As String, ByVal mRow As Integer)
        Dim DtTemp1 As DataTable = Nothing
        Dim ErrMsgStr$ = ""

        Try
            mQry = " Select Code From Item_Uid Where Item_Uid = '" & Item_Uid & "' "
            Dgl1.Item(Col1Item_Uid, mRow).Tag = AgL.XNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar)

            mQry = " Select  H.DocId As JobOrder, H.V_Type + '-' + H.ManualRefNo As JobOrderNo, " &
                        " L.Sr As JobOrderSr, L.Rate, L.IncentiveRate, L.ProdOrder, Po.ManualRefNo As ProdOrderNo, " &
                        " U.DecimalPlaces as QtyDecimalPlaces, MU.DecimalPlaces as MeasureDecimalPlaces, " &
                        " L.MeasurePerPcs, L.Unit, L.MeasureUnit, L.Item, I.Description As ItemDesc " &
                        " From JobOrderDetail L  " &
                        " LEFT JOIN JobOrder H  ON L.DocId = H.DocId " &
                        " LEFT JOIN ProdOrder Po  ON L.ProdOrder = Po.DocId " &
                        " LEFT JOIN JobIssRecUID JU ON L.DocId = JU.DocID AND L.Sr = JU.TSr  " &
                        " LEFT JOIN Item I  ON L.Item = I.Code " &
                        " Left Join Unit U  On L.Unit = U.Code " &
                        " Left Join Unit MU  On L.MeasureUnit = MU.Code " &
                        " Where JU.Item_Uid = '" & Dgl1.Item(Col1Item_Uid, mRow).Tag & "' " &
                        " And H.Process = '" & TxtProcess.Tag & "' " &
                        " Order By H.V_Date Desc Limit 1"
            DtTemp1 = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtTemp1.Rows.Count > 0 Then
                Dgl1.Item(Col1Item, mRow).Tag = AgL.XNull(DtTemp1.Rows(0)("Item"))
                Dgl1.Item(Col1Item, mRow).Value = AgL.XNull(DtTemp1.Rows(0)("ItemDesc"))

                Dgl1.Item(Col1QtyDecimalPlaces, mRow).Value = AgL.VNull(DtTemp1.Rows(0)("QtyDecimalPlaces"))

                Dgl1.Item(Col1JobOrder, mRow).Tag = AgL.XNull(DtTemp1.Rows(0)("JobOrder"))
                Dgl1.Item(Col1JobOrder, mRow).Value = AgL.XNull(DtTemp1.Rows(0)("JobOrderNo"))
                Dgl1.Item(Col1JobOrderSr, mRow).Value = AgL.XNull(DtTemp1.Rows(0)("JobOrderSr"))

                Dgl1.Item(Col1Qty, mRow).Value = 1
                Dgl1.Item(Col1BillQty, mRow).Value = 1
                Dgl1.Item(Col1Unit, mRow).Value = AgL.XNull(DtTemp1.Rows(0)("Unit"))

                Dgl1.Item(Col1MeasurePerPcs, mRow).Value = AgL.VNull(DtTemp1.Rows(0)("MeasurePerPcs"))
                Dgl1.Item(Col1MeasureUnit, mRow).Value = AgL.XNull(DtTemp1.Rows(0)("MeasureUnit"))
                Dgl1.Item(Col1MeasureDecimalPlaces, mRow).Value = AgL.VNull(DtTemp1.Rows(0)("MeasureDecimalPlaces"))

                Dgl1.Item(Col1ProdOrder, mRow).Tag = AgL.XNull(DtTemp1.Rows(0)("ProdOrder"))
                Dgl1.Item(Col1ProdOrder, mRow).Value = AgL.XNull(DtTemp1.Rows(0)("ProdOrderNo"))
                Dgl1.Item(Col1Rate, mRow).Value = AgL.VNull(DtTemp1.Rows(0)("Rate"))

                AgCalcGrid1.AgChargesValue(AgTemplate.ClsMain.Charges.INCENTIVE, mRow, AgStructure.AgCalcGrid.LineColumnType.Percentage) = AgL.VNull(DtTemp1.Rows(0)("IncentiveRate"))
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Function FCheck_Item_UID(ByVal Item_UID As String, ByVal JobWorker As String) As String
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

        mQry = " Select RecDocID From Item_Uid  Where Code = '" & Item_UidCode & "' "
        If AgL.XNull(AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar) = "" Then
            FCheck_Item_UID = "Carpet Id " & Item_UID & " Is Not Received From Weaving Process."
            Exit Function
        Else
            FCheck_Item_UID = ""
        End If

        'mQry = " Select I.Div_Code From Item_Uid Iu LEFT JOIN Item I ON Iu.Item = I.Code Where Iu.Code = '" & Item_UidCode & "' "
        'If AgL.XNull(AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar) <> AgL.PubDivCode Then
        '    FCheck_Item_UID = "Carpet Id " & Item_UID & " Does Not Belong To This Division."
        '    Exit Function
        'Else
        '    FCheck_Item_UID = ""
        'End If

        mQry = " SELECT L.Process " &
                " FROM (Select * From JobIssRecUID  Where Item_UID = '" & Item_UidCode & "' And ISSREC = 'I' And Process='" & TxtProcess.Tag & "') L " &
                " Left Join JobIssRecUID L1  On L.DocID = L1.JobRecDocId And L.Item_UID = L1.Item_UID " &
                " WHERE (L1.DocID Is Null Or L1.DocID = '" & mSearchCode & "')  "
        If AgL.FillData(mQry, AgL.GCn).Tables(0).rows.Count <= 0 Then
            FCheck_Item_UID = "Carpet Id " & Item_UID & " Is Not In " & TxtProcess.Text & "."
            Exit Function
        Else
            FCheck_Item_UID = ""
        End If

        mQry = " SELECT H.JobWorker " &
                " FROM (Select * From JobIssRecUID  Where Item_UID = '" & Item_UidCode & "' And ISSREC = 'I' And Process='" & TxtProcess.Tag & "') L  " &
                " LEFT JOIN JobOrder H ON L.DocID = H.DocID " &
                " Left Join JobIssRecUID L1  On L.DocID = L1.JobRecDocId And L.Item_UID = L1.Item_UID " &
                " WHERE (L1.DocID Is Null Or L1.DocID = '" & mSearchCode & "') "
        If AgL.XNull(AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar) <> JobWorker Then
            FCheck_Item_UID = "Carpet Id " & Item_UID & " Is Not Issued To this Job Worker."
            Exit Function
        Else
            FCheck_Item_UID = ""
        End If

        'mQry = " Select L.DocId " & _
        '       " From JobIssRecUID L " & _
        '       " Where L.Item_Uid = '" & Item_UidCode & "' " & _
        '       " And L.Process = '" & TxtProcess.Tag & "' " & _
        '       " AND L.ISSREC = 'I' " & _
        '       " And (L.JobRecDocID Is Null Or L.JobRecDocId = '" & mSearchCode & "') "
        'If AgL.FillData(mQry, AgL.GCn).Tables(0).rows.Count <= 0 Then
        '    FCheck_Item_UID = "No Order Pending For Carpet Id " & Item_UID & "."
        '    Exit Function
        'Else
        '    FCheck_Item_UID = ""
        'End If


        'mQry = " SELECT TOP 1 Sg.DispName, H.ManualRefNo, H.V_Date, Vc.NCatDescription AS ProcessDesc " & _
        '        " FROM JobIssRecUID L  " & _
        '        " LEFT JOIN JobIssRec H  ON L.DocID = H.DocID  " & _
        '        " LEFT JOIN SubGroup Sg   ON H.JobWorker = Sg.SubCode " & _
        '        " LEFT JOIN VoucherCat  Vc   ON H.Process =  Vc.NCat " & _
        '        " WHERE L.Item_UID = '" & Item_UidCode & "'  " & _
        '        " AND L.ISSREC = 'R' " & _
        '        " AND L.Process = '" & TxtProcess.Tag & "' " & _
        '        " AND L.JobRecDocID = '" & Dgl1.Item(Col1JobOrder, mRowIndex).Tag & "' " & _
        '        " And L.DocId <> '" & mSearchCode & "'" & _
        '        " ORDER BY H.EntryDate DESC	 "
        'DtTemp = AgL.FillData(mQry, AgL.GcnRead).Tables(0)
        'If DtTemp.Rows.Count > 0 Then
        '    FCheck_Item_UID = "Carpet Id " & Item_UID & " Is Already Received From " & AgL.XNull(DtTemp.Rows(0)("DispName")) & " From Process  " & AgL.XNull(DtTemp.Rows(0)("ProcessDesc")) & " On Date " & AgL.XNull(DtTemp.Rows(0)("V_Date")) & " Against Ref No.  " & AgL.XNull(DtTemp.Rows(0)("ManualRefNo")) & " "
        '    Exit Function
        'Else
        '    FCheck_Item_UID = ""
        'End If
    End Function

    Private Sub FPostInJobIssRecUID(ByVal SearchCode As String, ByVal Conn As SQLiteConnection, ByVal Cmd As SQLiteCommand)
        Dim I As Integer = 0, bSr As Integer = 0

        mQry = "Delete from JobIssRecUID Where DocId ='" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " INSERT INTO JobIssRecUID(DocID, TSr, Sr, IssRec, Process, JobRecDocID, Item, Item_UID, " &
                 " Godown, Site_Code, V_Date, V_Type, SubCode, Div_Code, RecId, EntryDate) " &
                 " Select L.DocId, L.Sr As TSr, L.Sr, 'R', H.Process, L.JobOrder, L.Item, L.Item_Uid, " &
                 " H.Godown, H.Site_Code, H.V_Date, H.V_Type, H.JobWorker, H.Div_Code, H.ManualRefNo, H.EntryDate " &
                 " From (Select * From JobReceiveDetail  Where DocId = '" & mSearchCode & "' And Item_Uid Is Not Null) As L " &
                 " LEFT JOIN JobIssRec H  On L.DocId = H.DocId "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " Update JobIssRecUID " &
                " SET JobRecDocID = " & AgL.Chk_Text(mInternalCode) & " " &
                " WHERE JobRecDocID Is Null " &
                " And Item_UID In (Select Item_UID From JobReceiveDetail  Where DocId = '" & mSearchCode & "' And Item_Uid Is Not Null) " &
                " And Process = '" & TxtProcess.Tag & "' " &
                " AND ISSREC = 'I' " &
                " And Site_Code = '" & AgL.PubSiteCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    End Sub

    Private Sub BtnImprtFromText_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnImprtFromText.Click
        If AgL.StrCmp(BtnImprtFromText.Text, ImportAction_NewImport) Then
            FImportFromTextFile()
            ChkShowOnlyImportedRecords.Checked = True
        Else
            mQry = " UPDATE JobIssRec Set EntryStatus = '" & AgTemplate.ClsMain.LogStatus.LogImportClear & "' Where DocId = '" & mSearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            FIniMaster(1)
            MoveRec()
        End If
    End Sub

    Private Sub FImportFromTextFile()
        ' Create an instance of StreamReader to read from a file.
        Dim Sr As StreamReader
        Dim Opn As New OpenFileDialog
        Dim mItem_UidDesc$ = ""


        Dim Line$ = "", mDateTime$ = "", mMachine$ = "", mProcess$ = "", mJobRecBy$ = "", mBarcode$ = "", mSKU$ = ""
        Dim mDefaultGodown$ = "", mJobType$ = "", mJobWorker$ = "", mIssRec$ = "", StrQry$ = ""
        Dim mMeasurePerPcs As Double = 0
        Dim StrMessage$ = ""

        Dim I As Integer, J As Integer = 0, bBarCodeQty As Integer = 0
        Dim DtTemp As DataTable, DtLineRec As DataTable
        Dim strArr() As String

        DtTemp = AgL.FillData("Select Godown from EnviroDefaultGodown Where Div_Code = '" & AgL.PubDivCode & "' and Site_Code = '" & AgL.PubSiteCode & "' and ItemType ='" & ClsMain.ItemType.CarpetSKU & "' ", AgL.GCn).Tables(0)
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

        If Opn.FileName <> "" Then
            Sr = New StreamReader(Opn.FileName)
        Else
            Exit Sub
        End If


        StrMessage = ""

        StrQry = "  Declare @TmpTable as Table " &
                    " ( " &
                    " Process nVarchar(10), " &
                    " IssRec nVarchar(10), " &
                    " JobWorker nVarchar(10), " &
                    " JobRecBy nVarchar(10), " &
                    " BarCode nVarchar(10), " &
                    " Sku nVarchar(10), " &
                    " MeasurePerPcs Float " &
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

                If mIssRec <> "R" Then MsgBox("IssRec Is Not Equal To ""R"".Can't Proceed.") : Exit Sub

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

                DtTemp = AgL.FillData("Select SubCode From SubGroup  Where ManualCode = '" & mJobWorker & "' And CharIndex('|' + '" & AgL.PubDivCode & "' + '|', DivisionList) > 0  And Site_Code = '" & AgL.PubSiteCode & "'", AgL.GcnRead).Tables(0)
                If DtTemp.Rows.Count > 0 Then
                    mJobWorker = DtTemp.Rows(0)("SubCode")
                Else
                    If StrMessage <> "" Then StrMessage += vbCrLf
                    StrMessage += "Invalid Value Found in JobWorker Field at Row No. " & I
                End If

                DtTemp = AgL.FillData("Select SubCode From SubGroup  Where ManualCode = '" & mJobRecBy & "' and Div_Code = '" & AgL.PubDivCode & "' and Site_Code = '" & AgL.PubSiteCode & "'", AgL.GcnRead).Tables(0)
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
                    DtTemp = AgL.FillData("Select Item_Uid.Code, Item_Uid.Item, Item." & mMeasurePerPcs & " As Measure From Item_UID LEFT JOIN Item On Item_Uid.Item = Item.Code Where Item_Uid.Item_UID = '" & mBarcode & "' ", AgL.GCn).Tables(0)
                    If DtTemp.Rows.Count > 0 Then
                        mBarcode = DtTemp.Rows(0)("Code")
                        mSKU = DtTemp.Rows(0)("Item")
                        mMeasurePerPcs = AgL.VNull(DtTemp.Rows(0)("Measure"))
                    Else
                        If StrMessage <> "" Then StrMessage += vbCrLf
                        MsgBox("Invalid Value Found in Barcode Field at Row No. " & I)
                    End If
                End If

                Dim Item_UidError$ = ""
                Item_UidError = FCheck_Item_UID(mItem_UidDesc, mJobWorker)
                If Item_UidError = "" Then
                    StrQry += " Insert Into @TmpTable (Process, IssRec, JobWorker, JobRecBy, Barcode, Sku, MeasurePerPcs) "
                    StrQry += " Values (" & AgL.Chk_Text(mProcess) & ", " & AgL.Chk_Text(mIssRec) & ", " &
                                " " & AgL.Chk_Text(mJobWorker) & ", " & AgL.Chk_Text(mJobRecBy) & ", " &
                                " " & AgL.Chk_Text(mBarcode) & ", " & AgL.Chk_Text(mSKU) & ", " & AgL.Chk_Text(mMeasurePerPcs) & ") "
                Else
                    ImportMessegeStr += Item_UidError & vbCrLf
                End If


                If StrMessage <> "" Then
                    MsgBox(StrMessage)
                    Exit Sub
                End If
            End If
        Loop Until Line Is Nothing
        Sr.Close()

        mQry = StrQry & " Select Process, IssRec, JobWorker, JobRecBy " &
                " From @TmpTable " &
                " Where Process = '" & mProcess & "' And IssRec = 'R' " &
                " Group by Process, IssRec, JobWorker, JobRecBy "
        DtTemp = AgL.FillData(mQry, AgL.GcnRead).tables(0)


        For I = 0 To DtTemp.Rows.Count - 1
            If I > 0 Then Topctrl1.FButtonClick(0)

            TxtProcess.Tag = mProcess
            TxtProcess.Text = AgL.XNull(AgL.Dman_Execute("Select Description From Process Where NCat = '" & TxtProcess.Tag & "' ", AgL.GCn).ExecuteScalar)

            TxtJobReceiveBy.Tag = DtTemp.Rows(I)("JobRecBy")
            TxtJobReceiveBy.Text = AgL.XNull(AgL.Dman_Execute("Select Name From SubGroup Sg Where SubCode = '" & TxtJobReceiveBy.Tag & "'", AgL.GCn).ExecuteScalar)

            TxtJobWorker.Tag = DtTemp.Rows(I)("JobWorker")
            TxtJobWorker.Text = AgL.XNull(AgL.Dman_Execute("Select Name From SubGroup Sg Where SubCode = '" & TxtJobWorker.Tag & "'", AgL.GCn).ExecuteScalar)

            TxtGodown.Tag = PubDefaultGodownCode
            TxtGodown.Text = PubDefaultGodownName

            TxtBillingOn.Text = AgL.XNull(AgL.Dman_Execute(" SELECT H.DefaultBillingType FROM Process H  WHERE H.NCat = '" & TxtProcess.AgSelectedValue & "' ", AgL.GCn).ExecuteScalar)

            mQry = StrQry & " Select Process, Sku, BarCode, Max(MeasurePerPcs) As MeasurePerPcs From @TmpTable " &
                    " Where Process = '" & TxtProcess.Tag & "' " &
                    " And JobRecBy = '" & TxtJobReceiveBy.Tag & "'" &
                    " And JobWorker = '" & TxtJobWorker.Tag & "'" &
                    " Group By Process, Sku, BarCode " &
                    " Order By MeasurePerPcs, Sku "
            DtLineRec = AgL.FillData(mQry, AgL.GcnRead).Tables(0)

            For J = 0 To DtLineRec.Rows.Count - 1
                Dgl1.Rows.Add()
                Dgl1.Item(ColSNo, Dgl1.Rows.Count - 2).Value = Dgl1.Rows.Count - 1
                Dgl1.Item(Col1Item_Uid, Dgl1.Rows.Count - 2).Tag = DtLineRec.Rows(J)("BarCode")
                Dgl1.Item(Col1Item_Uid, Dgl1.Rows.Count - 2).Value = AgL.XNull(AgL.Dman_Execute("Select Item_Uid From Item_Uid Where Code = '" & DtLineRec.Rows(J)("BarCode") & "'", AgL.GCn).ExecuteScalar)

                Validating_Item_Uid(Dgl1.Item(Col1Item_Uid, Dgl1.Rows.Count - 2).Value, Dgl1.Rows.Count - 2)
            Next

            Calculation()

            Topctrl1.FButtonClick(13)
        Next

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

    Private Sub Dgl1_CellContentClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellContentClick
        Dim Mdi As MDIMain = New MDIMain
        Try
            Select Case Dgl1.Columns(e.ColumnIndex).Name
                Case Col1JobOrder
                    Call ClsMain.ProcOpenLinkForm(Mdi.MnuJobOrder, Dgl1.Item(Col1JobOrder, e.RowIndex).Tag, Me.MdiParent)

                    'Case Col1ProdOrder
                    '    Call ClsMain.ProcOpenLinkForm(Mdi.MnuSaleOrderEntry, Dgl1.Item(Col1ProdOrder, e.RowIndex).Tag, Me.MdiParent)
            End Select
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ChkShowOnlyImportedRecords_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkShowOnlyImportedRecords.Click
        FIniMaster(1)
        Topctrl1.SetDisp(True)
        MoveRec()
    End Sub

    Private Sub FrmFinishingOrder_BaseFunction_DispText() Handles Me.BaseFunction_DispText
        If AgL.StrCmp(Topctrl1.Mode, "Browse") Then
            ChkShowOnlyImportedRecords.Visible = True
        Else
            ChkShowOnlyImportedRecords.Visible = False
        End If
    End Sub

    Private Sub FrmFinishingOrder_BaseEvent_Topctrl_tbPrn(ByVal SearchCode As String) Handles Me.BaseEvent_Topctrl_tbPrn
        Dim RepName As String = ""
        Dim mJobOn$ = ""

        mQry = " Select JobOn From Process Where NCat = '" & TxtProcess.Tag & "'"
        mJobOn = AgL.XNull(AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar)

        If AgL.StrCmp(mJobOn, "Qty") Then
            RepName = "Trade_JobReceiveQtyPrint"
        Else
            RepName = "Rug_FinishingReceivePrint"
        End If

        mQry = " SELECT H.V_Date, H.V_Type + '-' + H.ManualRefNo As ManualRefNo, H.Remarks, H.JobWorkerDocNo, " &
                " H.EntryBy, H.EntryDate, H.ApproveBy, H.ApproveDate, SGM.DispName AS MachineName, " &
                " H.RoundOff, H.NetAmount, L.Qty, L.Unit, L.MeasurePerPcs, L.LotNo, " &
                " L.TotalMeasure, L.MeasureUnit, L.Rate, L.Amount, L.PerimeterPerPcs, L.TotalPerimeter, " &
                " L.Remark As LineRemark, U.DecimalPlaces, I.Specification AS ItemSpecification, " &
                " L.Item_Uid, Sg.Name AS JobWorkerName, L.LossPer, L.LossQty, L.RetQty, H.Freight, " &
                " D1.Description AS D1Desc, D2.Description AS D2Desc, E.Caption_Dimension1, E.Caption_Dimension2, " &
                " Sg.Add1, Sg.Add2, Sg.Add3, Sg.Mobile, Sg.PAN, Sg1.DispName AS JobRecByName, G.Description AS GodownDesc, " &
                " I.Description AS ItemDesc, SI.Description AS StockItemDesc, Iu.Item_Uid As Item_UidDesc, " &
                " Ig.Description As ItemGroupDesc, '" & "Item Receive From " & TxtProcess.Text & "' As GatePassTitle " &
                " FROM JobIssRec H   " &
                " LEFT JOIN JobReceiveDetail L  ON H.DocID = L.DocId " &
                " LEFT JOIN SubGroup Sg  ON H.JobWorker = Sg.SubCode " &
                " LEFT JOIN SubGroup Sg1  ON H.JobReceiveBy = Sg1.SubCode " &
                " LEFT JOIN Godown G  ON H.Godown = G.Code " &
                " LEFT JOIN Item I  ON L.Item = I.Code " &
                " LEFT JOIN Item SI  ON L.StockItem = SI.Code " &
                " LEFT JOIN Item_Uid Iu  ON L.Item_Uid = Iu.Code " &
                " LEFT JOIN ItemGroup Ig ON I.ItemGroup = Ig.Code " &
                " LEFT JOIN Enviro E ON E.Site_Code = H.Site_Code AND E.Div_Code = H.Div_Code " &
                " LEFT JOIN Dimension1 D1 ON D1.Code = L.Dimension1 " &
                " LEFT JOIN Dimension2 D2 ON D2.Code = L.Dimension2 " &
                " LEFT JOIN Unit U ON L.Unit = U.Code  " &
                " LEFT JOIN SubGroup SGM ON SGM.SubCode = L.Machine " &
                " WHERE H.DocID =  '" & mSearchCode & "' Order By L.Sr "
        ClsMain.FPrintThisDocument(Me, TxtV_Type.Tag, mQry, RepName, "Item Receive From " & TxtProcess.Text)
    End Sub

    'Private Sub FPost_JobOrderWiseDue(ByRef Conn As SqliteConnection, ByRef Cmd As SqliteCommand)
    '    Dim StructDues As AgTemplate.ClsMain.Dues = Nothing
    '    Dim DtTemp As DataTable
    '    Dim mSr As Integer, I As Integer

    '    mQry = "SELECT L.JobOrder, L.DocId, Sum(NetAmount) AS NetAmount  FROM JobReceiveDetail  L  " & _
    '           " WHERE DocID ='" & mInternalCode & "' " & _
    '           " GROUP BY L.JobOrder, L.DocId "
    '    DtTemp = AgL.FillData(mQry, AgL.GcnRead).Tables(0)


    '    For I = 0 To DtTemp.Rows.Count - 1
    '        mSr += 1
    '        With StructDues
    '            .DocID = mSearchCode
    '            .Sr = mSr
    '            .V_Type = TxtV_Type.AgSelectedValue
    '            .V_Prefix = LblPrefix.Text
    '            .V_Date = TxtV_Date.Text
    '            .V_No = Val(TxtV_No.Text)
    '            .Div_Code = TxtDivision.AgSelectedValue
    '            .Site_Code = TxtSite_Code.AgSelectedValue
    '            .CashCredit = ""
    '            .SubCode = TxtJobWorker.AgSelectedValue
    '            .Narration = Dgl1.Item(Col1Remark, I).Value
    '            .ReferenceDocID = AgL.XNull(DtTemp.Rows(I)("JobOrder"))
    '            .RefV_Type = ""
    '            .RefV_No = 0
    '            .RefPartyName = TxtJobWorker.Text
    '            .RefPartyAddress = ""
    '            .RefPartyCity = ""
    '            .PaybleAmount = AgL.VNull(DtTemp.Rows(I)("NetAmount"))
    '            .ReceivableAmount = 0
    '            .AdjustedAmount = 0
    '            .EntryBy = TxtEntryBy.Text
    '            .EntryDate = AgL.GetDateTime(AgL.GcnRead)
    '            .EntryType = TxtEntryType.Text
    '            .EntryStatus = LogStatus.LogOpen
    '            .ApproveBy = TxtApproveBy.Text
    '            .ApproveDate = ""
    '            .MoveToLog = ""
    '            .MoveToLogDate = ""
    '            .IsDeleted = 0
    '            .Status = TxtStatus.Text
    '            Call AgTemplate.ClsMain.ProcGetPartyAddress(TxtJobWorker.AgSelectedValue, .RefPartyAddress, .RefPartyCity, AgL.GcnRead)
    '            Call ProcGetVType(.ReferenceDocID, .RefV_Type, .RefV_No, AgL.GcnRead)
    '        End With
    '        Call AgTemplate.ClsMain.ProcPostInDues(Conn, Cmd, StructDues)
    '    Next
    'End Sub

    Private Sub ProcGetVType(ByVal DocId As String, ByRef V_Type As String, ByRef V_No As Long, ByVal Conn As SQLiteConnection)
        Dim DtTemp As DataTable = Nothing
        Dim bTable As String = ""
        Try
            mQry = " SELECT H.V_Type, H.V_No FROM JobOrder H  WHERE H.DocID = '" & DocId & "' "
            DtTemp = AgL.FillData(mQry, Conn).Tables(0)
            With DtTemp
                If .Rows.Count > 0 Then
                    V_Type = AgL.XNull(DtTemp.Rows(0)("V_Type"))
                    V_No = AgL.VNull(DtTemp.Rows(0)("V_No"))
                End If
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Function FDataValidation_Item_UID() As String
        Dim DtTemp As DataTable = Nothing
        Dim DtTemp1 As DataTable = Nothing
        Dim I As Integer = 0
        Dim mItem_UidStr$ = ""
        Dim mItem_UidPlusJobOrderStr$ = ""
        Dim MsgStr$ = ""

        For I = 0 To Dgl1.Rows.Count - 1
            If Dgl1.Item(Col1Item_Uid, I).Tag <> "" Then
                If mItem_UidStr = "" Then
                    mItem_UidStr = AgL.Chk_Text(Dgl1.Item(Col1Item_Uid, I).Tag)
                    mItem_UidPlusJobOrderStr = AgL.Chk_Text(Dgl1.Item(Col1Item_Uid, I).Tag + Dgl1.Item(Col1JobOrder, I).Tag)
                Else
                    mItem_UidStr += "," & AgL.Chk_Text(Dgl1.Item(Col1Item_Uid, I).Tag)
                    mItem_UidPlusJobOrderStr += "," & AgL.Chk_Text(Dgl1.Item(Col1Item_Uid, I).Tag + Dgl1.Item(Col1JobOrder, I).Tag)
                End If
            End If
        Next

        If mItem_UidStr = "" Then FDataValidation_Item_UID = "" : Exit Function

        mQry = " Select Iu.Item_Uid From Item_Uid Iu LEFT JOIN Item I ON Iu.Item = I.Code Where Iu.Code In (" & mItem_UidStr & ") And I.Div_Code <> '" & AgL.PubDivCode & "'"
        DtTemp = AgL.FillData(mQry, AgL.GcnRead).Tables(0)

        'If DtTemp.Rows.Count > 0 Then
        '    For I = 0 To DtTemp.Rows.Count - 1
        '        MsgStr += "Carpet Id " & AgL.XNull(DtTemp.Rows(I)("Item_Uid")) & " Does Not Belong To " & AgL.PubDivName & "."
        '    Next
        'End If

        'mQry = " Select Iu.Item_Uid " & _
        '            " From StockProcess L  " & _
        '            " LEFT JOIN Item_Uid Iu  On L.Item_Uid = Iu.Code " & _
        '            " Where IFNull(L.Qty_Iss,0) > 0 And L.Process = '" & TxtProcess.Tag & "' " & _
        '            " And L.Item_UID In (" & mItem_UidStr & ") " & _
        '            " And L.DocID <> '" & mSearchCode & "'  " & _
        '            " Group By Iu.Item_Uid " & _
        '            " Having IFNull(Count(*),0) > 0 "
        'DtTemp = AgL.FillData(mQry, AgL.GcnRead).Tables(0)

        'If DtTemp.Rows.Count > 0 Then
        '    For I = 0 To DtTemp.Rows.Count - 1
        '        MsgStr += "Carpet Id " & AgL.XNull(DtTemp.Rows(I)("Item_Uid")) & " has already completed this process"
        '    Next
        'End If

        mQry = " Select Item_Uid From Item_Uid  " &
                " Where Code In (" & mItem_UidStr & ") " &
                " And RecDocId Is Null "
        DtTemp = AgL.FillData(mQry, AgL.GcnRead).Tables(0)

        If DtTemp.Rows.Count > 0 Then
            For I = 0 To DtTemp.Rows.Count - 1
                MsgStr += "Carpet Id " & AgL.XNull(DtTemp.Rows(I)("Item_Uid")) & " Is Not Received From Weaving Process." & vbCrLf
            Next
        End If

        mQry = " SELECT Sg.DispName, H.ManualRefNo, H.V_Date, P.Description AS ProcessDesc, Iu.Item_Uid As Item_UidDesc " &
                " FROM JobIssRecUID L  " &
                " LEFT JOIN JobOrder H  ON L.JobRecDocID = H.DocID  " &
                " LEFT JOIN SubGroup Sg   ON H.JobWorker = Sg.SubCode " &
                " LEFT JOIN Process P ON H.Process =  P.NCat " &
                " LEFT JOIN Item_Uid Iu On L.Item_Uid = Iu.Code " &
                " WHERE L.Item_UID In (" & mItem_UidStr & ")  " &
                " AND L.ISSREC = 'R' " &
                " AND L.Process = '" & TxtProcess.Tag & "' " &
                " AND L.Item_Uid + L.JobRecDocID In (" & mItem_UidPlusJobOrderStr & ") " &
                " AND L.DocId <> '" & mSearchCode & "'" &
                " ORDER BY H.EntryDate DESC	 "
        DtTemp = AgL.FillData(mQry, AgL.GcnRead).Tables(0)
        If DtTemp.Rows.Count > 0 Then
            For I = 0 To DtTemp.Rows.Count - 1
                MsgStr += "Carpet Id " & DtTemp.Rows(I)("Item_UidDesc") & " Is Already Received From " & AgL.XNull(DtTemp.Rows(I)("DispName")) & " From Process  " & AgL.XNull(DtTemp.Rows(I)("ProcessDesc")) & " On Date " & AgL.XNull(DtTemp.Rows(I)("V_Date")) & " Against Ref No. " & AgL.XNull(DtTemp.Rows(I)("ManualRefNo")) & " " & vbCrLf
            Next
        End If

        mQry = " SELECT Iu.Item_Uid " &
                " FROM (Select * From JobIssRecUID  " &
                "       Where Item_UID In (" & mItem_UidStr & ") And ISSREC = 'I' " &
                "       And Process ='" & TxtProcess.Tag & "') L " &
                " LEFT JOIN Item_Uid Iu On L.Item_Uid = Iu.Code " &
                " LEFT JOIN JobIssRecUID L1  On L.DocID = L1.JobRecDocId And L.Item_UID = L1.Item_UID " &
                " WHERE (L1.DocID Is Null Or L1.DocID = '" & mSearchCode & "')  " &
                " And L.Process <> '" & TxtProcess.Tag & "'"
        DtTemp = AgL.FillData(mQry, AgL.GcnRead).Tables(0)

        If DtTemp.Rows.Count > 0 Then
            For I = 0 To DtTemp.Rows.Count - 1
                MsgStr += "Carpet Id " & DtTemp.Rows(I)("Item_Uid") & " Is Not In " & TxtProcess.Text & "." & vbCrLf
            Next
        End If

        mQry = " SELECT Iu.Item_Uid " &
                " FROM (Select * From JobIssRecUID  " &
                "       Where Item_UID In (" & mItem_UidStr & ") And ISSREC = 'I' " &
                "       And Process ='" & TxtProcess.Tag & "') L  " &
                " LEFT JOIN Item_Uid Iu On L.Item_Uid = Iu.Code " &
                " LEFT JOIN JobOrder H ON L.DocID = H.DocID " &
                " LEFT JOIN JobIssRecUID L1  On L.DocID = L1.JobRecDocId And L.Item_UID = L1.Item_UID " &
                " WHERE (L1.DocID Is Null Or L1.DocID = '" & mSearchCode & "') " &
                " And H.JobWorker <> '" & TxtJobWorker.Tag & "'"
        DtTemp = AgL.FillData(mQry, AgL.GcnRead).Tables(0)

        If DtTemp.Rows.Count > 0 Then
            For I = 0 To DtTemp.Rows.Count - 1
                MsgStr += "Carpet Id " & DtTemp.Rows(I)("Item_Uid") & " Is Not Issued To this Job Worker." & vbCrLf
            Next
        End If
        FDataValidation_Item_UID = MsgStr
    End Function

    Private Sub FrmRugFinishingOrder_BaseEvent_Topctrl_tbEdit(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbEdit
        If isRecordLocked Then
            MsgBox("Referential data exist. Can't modify record.")
            Passed = False
            Exit Sub
        End If

        Passed = Not ClsMain.FLockOldEntryInNewEntryPoint(TxtProcess.Tag, TxtV_Date.Text)
        FAsignProcess()
        FAsignMeasureField()
    End Sub

    Private Sub FrmJobReceive_BaseEvent_Topctrl_tbDel(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbDel
        If isRecordLocked Then
            MsgBox("Referential data exist. Can't delete record.")
            Passed = False
        End If
    End Sub

    Private Sub BtnConsumptionDetail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnConsumptionDetail.Click
        Dim FrmObj As FrmJobReceiveBom
        If BtnConsumptionDetail.Tag Is Nothing Then
            FrmObj = New FrmJobReceiveBom(TxtJobWorker.Tag, TxtProcess.Tag, TxtV_Date.Text, mInternalCode)
            FrmObj.IniGrid()
            If AgL.StrCmp(Topctrl1.Mode, "Browse") Then
                FMovRecConsumption(FrmObj) : BtnConsumptionDetail.Tag = FrmObj
            End If
        Else
            FrmObj = BtnConsumptionDetail.Tag
        End If
        FrmObj.Owner = Me
        FrmObj.StartPosition = FormStartPosition.CenterScreen
        FrmObj.ShowDialog()

        If FrmObj.mOkButtonPressed Then
            BtnConsumptionDetail.Tag = FrmObj
        End If
    End Sub

    Private Sub FPostConsumption(ByVal Conn As SQLiteConnection, ByVal Cmd As SQLiteCommand)
        Dim MaxSr As Integer = 0
        Dim I As Integer = 0
        Dim mSr As Integer = 0
        Dim bSelectionQry As String = ""
        Dim FrmObj As FrmJobReceiveBom = Nothing
        Dim strmsg As String = ""

        If CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsPostConsumption")), Boolean) Then
            mQry = "Delete From JobReceiveBOM Where DocId = '" & mSearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

            'mQry = "INSERT INTO JobReceiveBOM(DocId, TSr, Sr, JobOrder, JobOrderSr, JobOrderBomSr, " & _
            '        " Item, Qty, Unit, MeasurePerPcs, TotalMeasure, MeasureUnit, Transaction_Type ) " & _
            '        " SELECT L.DocId, L.Sr AS TSr, row_NUMBER() OVER (ORDER BY L.Sr) AS Sr, " & _
            '        " L.JobOrder, L.JobOrderSr, row_NUMBER() OVER (ORDER BY L.Sr) As JobOrderBomSr, " & _
            '        " IFNull(Bd.Item,L.Item) AS Item, IFNull(Bd.Qty,1) * (L.Qty + IFNull(L.LossQty,0)) AS BomQty, IFNull(BomItem.Unit,I.Unit) AS Unit, " & _
            '        " IFNull(BomItem.Measure,I." & mMeasureField & ") AS Measure, IFNull(Bd.Qty,1) * (L.Qty + IFNull(L.LossQty,0)) As TotalMeasure, " & _
            '        " IFNull(BomItem.MeasureUnit,I.measureUnit) AS MeasureUnit, 'From BOM'  " & _
            '        " FROM (Select * From JobReceiveDetail Where DocId = '" & mSearchCode & "') As L  " & _
            '        " LEFT JOIN Item I On L.Item = I.Code " & _
            '        " LEFT JOIN BomDetail Bd ON I.Code = Bd.BaseItem " & _
            '        " LEFT JOIN Item BomItem ON Bd.Item = BomItem.Code "

            mQry = "INSERT INTO JobReceiveBOM(DocId, TSr, Sr, JobOrder, JobOrderSr, JobOrderBomSr, " &
                    " Item, Qty, Unit, MeasurePerPcs, TotalMeasure, MeasureUnit, Transaction_Type ) " &
                    " SELECT L.DocId, L.Sr AS TSr, row_NUMBER() OVER (ORDER BY L.Sr) AS Sr, " &
                    " L.JobOrder, L.JobOrderSr, row_NUMBER() OVER (ORDER BY L.Sr) As JobOrderBomSr, " &
                    " IFNull(Bd.Item,L.Item) AS Item, IFNull(Bd.Qty,0) * (L.Qty + IFNull(L.LossQty,0)) AS BomQty, IFNull(BomItem.Unit,I.Unit) AS Unit, " &
                    " IFNull(BomItem.Measure,I." & mMeasureField & ") AS Measure, IFNull(Bd.Qty,1) * (L.Qty + IFNull(L.LossQty,0)) As TotalMeasure, " &
                    " IFNull(BomItem.MeasureUnit,I.measureUnit) AS MeasureUnit, 'From BOM'  " &
                    " FROM (Select * From JobReceiveDetail Where DocId = '" & mSearchCode & "') As L  " &
                    " LEFT JOIN Item I On L.Item = I.Code " &
                    " LEFT JOIN BomDetail Bd ON I.Code = Bd.BaseItem " &
                    " LEFT JOIN Item BomItem ON Bd.Item = BomItem.Code " &
                    " WHERE IFNull(Bd.Qty,0) > 0 "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If

        If BtnConsumptionDetail.Tag IsNot Nothing Then
            mQry = "Delete From JobReceiveBOM Where DocId = '" & mSearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

            FrmObj = BtnConsumptionDetail.Tag

            With FrmObj
                For I = 0 To .Dgl1.Rows.Count - 1
                    If .Dgl1.Item(FrmJobReceiveBom.Col1Item, I).Value <> "" Then
                        mSr += 1
                        If bSelectionQry <> "" Then bSelectionQry += " UNION ALL "
                        bSelectionQry += "SELECT '" & mSearchCode & "', 1 AS TSr, " & mSr & " As Sr, " &
                                " " & AgL.Chk_Text(.Dgl1.Item(FrmJobReceiveBom.Col1Item, I).Tag) & ", " &
                                " " & AgL.Chk_Text(.Dgl1.Item(FrmJobReceiveBom.Col1LotNo, I).Value) & ", " &
                                " " & AgL.Chk_Text(.Dgl1.Item(FrmJobReceiveBom.Col1Dimension1, I).Tag) & ", " &
                                " " & AgL.Chk_Text(.Dgl1.Item(FrmJobReceiveBom.Col1Dimension2, I).Tag) & ", " &
                                " " & Val(.Dgl1.Item(FrmJobReceiveBom.Col1DocQty, I).Value) & ", " &
                                " " & Val(.Dgl1.Item(FrmJobReceiveBom.Col1LossQty, I).Value) & ", " &
                                " " & Val(.Dgl1.Item(FrmJobReceiveBom.Col1Qty, I).Value) & ", " &
                                " " & AgL.Chk_Text(.Dgl1.Item(FrmJobReceiveBom.Col1Unit, I).Value) & ", 'Manual' "
                    End If
                Next
            End With

            mQry = "INSERT INTO JobReceiveBOM(DocId, TSr, Sr, Item, LotNo, Dimension1, Dimension2, DocQty, LossQty, Qty, Unit, Transaction_Type ) " & bSelectionQry
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If

        MaxSr = AgL.VNull(AgL.Dman_Execute("Select Max(Sr) From StockProcess  Where DocId = '" & mSearchCode & "'", AgL.GcnRead).ExecuteScalar)

        mQry = "INSERT INTO StockProcess (DocID, Sr, V_Type, V_Prefix, V_Date, V_No, RecId, Div_Code, " &
                " Site_Code, SubCode, Item, Godown, Qty_Iss, Unit, MeasurePerPcs, Measure_Iss, MeasureUnit, " &
                " Process, CostCenter, LotNo, Dimension1, Dimension2 ) " &
                " SELECT L.DocId, " & MaxSr & " + row_number() OVER (ORDER BY L.Item) AS Sr, " &
                " Max(H.V_Type), Max(H.V_Prefix), Max(H.V_Date), Max(H.V_No), Max(H.ManualRefNo), Max(H.Div_Code), Max(H.Site_Code), " &
                " Max(H.JobWorker), L.Item, Max(H.Godown), Sum(L.Qty) As Qty_Iss, Max(L.Unit), " &
                " Max(L.MeasurePerPcs), Sum(L.TotalMeasure) Measure_Iss, " &
                " Max(L.MeasureUnit), H.Process, J.CostCenter, L.LotNo, L.Dimension1, L.Dimension2 " &
                " FROM (Select * From JobReceiveBom Where DocId = '" & mSearchCode & "') As L  " &
                " LEFT JOIN JobIssRec H On L.DocId = H.DocId " &
                " LEFT JOIN JobOrder J On L.JobOrder = J.DocId " &
                " Group By L.DocId, L.Item, L.LotNo, J.CostCenter, L.Dimension1, L.Dimension2, H.Process "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    End Sub

    Private Sub FMovRecConsumption(ByVal FrmObj As FrmJobReceiveBom)
        Dim DtTemp As DataTable = Nothing
        Dim I As Integer = 0

        mQry = " Select L.*, I.Description As ItemDesc, D1.Description As Dimension1Desc , D2.Description As Dimension2Desc " &
                " From JobReceiveBom L " &
                " LEFT JOIN Item I On L.Item = I.Code " &
                "  LEFT JOIN Dimension1 D1 ON D1.Code = L.Dimension1 " &
                " LEFT JOIN Dimension2 D2 ON D2.Code = L.Dimension2 " &
                " Where L.DocId = '" & mSearchCode & "'"
        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

        With FrmObj
            .Dgl1.RowCount = 1 : .Dgl1.Rows.Clear()
            If DtTemp.Rows.Count > 0 Then
                For I = 0 To DtTemp.Rows.Count - 1
                    .Dgl1.Rows.Add()
                    .Dgl1.Item(FrmJobReceiveBom.ColSNo, I).Value = .Dgl1.Rows.Count - 1
                    .Dgl1.Item(FrmJobReceiveBom.Col1Item, I).Tag = AgL.XNull(DtTemp.Rows(I)("Item"))
                    .Dgl1.Item(FrmJobReceiveBom.Col1Item, I).Value = AgL.XNull(DtTemp.Rows(I)("ItemDesc"))
                    .Dgl1.Item(FrmJobReceiveBom.Col1LotNo, I).Value = AgL.XNull(DtTemp.Rows(I)("LotNo"))
                    .Dgl1.Item(FrmJobReceiveBom.Col1Dimension1, I).Tag = AgL.XNull(DtTemp.Rows(I)("Dimension1"))
                    .Dgl1.Item(FrmJobReceiveBom.Col1Dimension1, I).Value = AgL.XNull(DtTemp.Rows(I)("Dimension1Desc"))
                    .Dgl1.Item(FrmJobReceiveBom.Col1Dimension2, I).Tag = AgL.XNull(DtTemp.Rows(I)("Dimension2"))
                    .Dgl1.Item(FrmJobReceiveBom.Col1Dimension2, I).Value = AgL.XNull(DtTemp.Rows(I)("Dimension2Desc"))
                    .Dgl1.Item(FrmJobReceiveBom.Col1DocQty, I).Value = AgL.VNull(DtTemp.Rows(I)("DocQty"))
                    .Dgl1.Item(FrmJobReceiveBom.Col1LossQty, I).Value = AgL.VNull(DtTemp.Rows(I)("LossQty"))
                    .Dgl1.Item(FrmJobReceiveBom.Col1Qty, I).Value = AgL.VNull(DtTemp.Rows(I)("Qty"))
                    .Dgl1.Item(FrmJobReceiveBom.Col1Unit, I).Value = AgL.XNull(DtTemp.Rows(I)("Unit"))
                Next I
            End If
        End With
    End Sub

    Private Sub FAsignMeasureField()
        Try
            If DtJobEnviro.Rows.Count > 0 Then
                If AgL.XNull(DtJobEnviro.Rows(0)("Field_Measure")) <> "" Then
                    mMeasureField = AgL.XNull(DtJobEnviro.Rows(0)("Field_Measure"))
                Else
                    mMeasureField = "Finishing_Measure"
                End If
            Else
                mMeasureField = "Finishing_Measure"
            End If
        Catch ex As Exception
            MsgBox("Field_Measure Is Not Defined In Job Enviro...!", MsgBoxStyle.Information)
        End Try
    End Sub

    Private Sub BtnByProduct_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnByProduct.Click
        Dim I As Integer = 0
        Dim mJobOrderList$ = ""
        For I = 0 To Dgl1.Rows.Count - 1
            If Dgl1.Item(Col1Item, I).Value <> "" Then
                If mJobOrderList = "" Then
                    mJobOrderList = AgL.Chk_Text(Dgl1.Item(Col1JobOrder, I).Tag)
                Else
                    mJobOrderList += "," + AgL.Chk_Text(Dgl1.Item(Col1JobOrder, I).Tag)
                End If
            End If
        Next



        Dim FrmObj As FrmJobReceiveByProduct
        If BtnByProduct.Tag Is Nothing Then
            FrmObj = New FrmJobReceiveByProduct
            FrmObj.JobOrderList = mJobOrderList
            FrmObj.IniGrid()
            FMovRecByProduct(FrmObj) : BtnByProduct.Tag = FrmObj
        Else
            FrmObj = BtnByProduct.Tag
            FrmObj.JobOrderList = mJobOrderList
        End If
        FrmObj.Owner = Me
        FrmObj.StartPosition = FormStartPosition.CenterScreen
        FrmObj.ShowDialog()

        If FrmObj.mOkButtonPressed Then
            BtnByProduct.Tag = FrmObj
        End If
    End Sub

    Private Sub FPostByProduct(ByVal Conn As SQLiteConnection, ByVal Cmd As SQLiteCommand)
        Dim MaxSr As Integer = 0
        Dim I As Integer = 0
        Dim mSr As Integer = 0
        Dim bSelectionQry As String = ""
        Dim FrmObj As FrmJobReceiveByProduct = Nothing
        Dim strmsg As String = ""

        If BtnByProduct.Tag IsNot Nothing Then
            mQry = "Delete From JobReceiveByProduct Where DocId = '" & mSearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

            FrmObj = BtnByProduct.Tag

            With FrmObj
                For I = 0 To .Dgl1.Rows.Count - 1
                    If .Dgl1.Item(FrmJobReceiveByProduct.Col1Item, I).Value <> "" Then
                        mSr += 1
                        If bSelectionQry <> "" Then bSelectionQry += " UNION ALL "
                        bSelectionQry += "SELECT '" & mSearchCode & "', " & mSr & " As Sr, " &
                                " " & AgL.Chk_Text(.Dgl1.Item(FrmJobReceiveByProduct.Col1Item, I).Tag) & ", " &
                                " " & AgL.Chk_Text(.Dgl1.Item(FrmJobReceiveByProduct.Col1LotNo, I).Value) & ", " &
                                " " & Val(.Dgl1.Item(FrmJobReceiveByProduct.Col1Qty, I).Value) & ", " &
                                " " & AgL.Chk_Text(.Dgl1.Item(FrmJobReceiveByProduct.Col1Unit, I).Value) & ", " &
                                " " & AgL.Chk_Text(.Dgl1.Item(FrmJobReceiveByProduct.Col1JobOrder, I).Tag) & " "
                    End If
                Next
            End With

            If bSelectionQry <> "" Then
                mQry = "INSERT INTO JobReceiveByProduct(DocId, Sr, Item, LotNo, Qty, Unit, JobOrder) " & bSelectionQry
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                MaxSr = AgL.VNull(AgL.Dman_Execute("Select Max(Sr) From StockProcess  Where DocId = '" & mSearchCode & "'", AgL.GcnRead).ExecuteScalar)

                mQry = "INSERT INTO Stock(DocID, Sr, V_Type, V_Prefix, V_Date, V_No, RecID, Div_Code, Site_Code,  SubCode, Item, LotNo, Godown, Qty_Rec, Unit, MeasurePerPcs, Measure_Rec, MeasureUnit ) " &
                        " Select L.DocID, " & MaxSr & " + row_number() OVER (ORDER BY L.Item) AS Sr ,Max(H.V_Type),  Max(H.V_Prefix), Max(H.V_Date), Max(H.V_No), Max(H.ManualRefNo), Max(H.Div_Code), Max(H.Site_Code), " &
                        " Max(H.JobWorker), L.Item, L.LotNo, Max(H.Godown), Sum(L.Qty), Max(L.Unit), Max(L.MeasurePerPcs),  Sum(L.TotalMeasure), Max(L.MeasureUnit) " &
                        " From (Select * From JobIssRec Where DocId = '" & mSearchCode & "' ) H  " &
                        " LEFT JOIN JobReceiveByProduct L On H.DocId = L.DocId " &
                        " LEFT JOIN Process P ON P.NCat= H.Process   " &
                        " Group By IFNull(P.DontAffectedStock,0), L.DocId, L.Item, L.LotNo "
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            End If
        End If
    End Sub

    Private Sub FMovRecByProduct(ByVal FrmObj As FrmJobReceiveByProduct)
        Dim DtTemp As DataTable = Nothing
        Dim I As Integer = 0

        mQry = " Select L.*, I.Description As ItemDesc, Jo.ManualRefNo As JobOrderNo " & _
                " From JobReceiveByProduct L " & _
                " LEFT JOIN Item I On L.Item = I.Code " & _
                " LEFT JOIN JobOrder Jo On L.JobOrder = Jo.DocId " & _
                " Where L.DocId = '" & mSearchCode & "'"
        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

        With FrmObj
            .Dgl1.RowCount = 1 : .Dgl1.Rows.Clear()
            If DtTemp.Rows.Count > 0 Then
                For I = 0 To DtTemp.Rows.Count - 1
                    .Dgl1.Rows.Add()
                    .Dgl1.Item(FrmJobReceiveByProduct.ColSNo, I).Value = .Dgl1.Rows.Count - 1
                    .Dgl1.Item(FrmJobReceiveByProduct.Col1Item, I).Tag = AgL.XNull(DtTemp.Rows(I)("Item"))
                    .Dgl1.Item(FrmJobReceiveByProduct.Col1Item, I).Value = AgL.XNull(DtTemp.Rows(I)("ItemDesc"))
                    .Dgl1.Item(FrmJobReceiveByProduct.Col1LotNo, I).Value = AgL.XNull(DtTemp.Rows(I)("LotNo"))
                    .Dgl1.Item(FrmJobReceiveByProduct.Col1Qty, I).Value = AgL.VNull(DtTemp.Rows(I)("Qty"))
                    .Dgl1.Item(FrmJobReceiveByProduct.Col1Unit, I).Value = AgL.XNull(DtTemp.Rows(I)("Unit"))
                    .Dgl1.Item(FrmJobReceiveByProduct.Col1JobOrder, I).Tag = AgL.XNull(DtTemp.Rows(I)("JobOrder"))
                    .Dgl1.Item(FrmJobReceiveByProduct.Col1JobOrder, I).Value = AgL.XNull(DtTemp.Rows(I)("JobOrderNo"))
                Next I
            End If
        End With
    End Sub
End Class
