Imports System.IO
Imports System.Data.SQLite
Imports CrystalDecisions.CrystalReports.Engine
Public Class FrmJobOrder
    Inherits AgTemplate.TempTransaction
    Public mQry$
    Public WithEvents AgCustomGrid1 As New AgCustomFields.AgCustomGrid

    Public Const ColSNo As String = "S.No."
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Public Const Col1Item_Uid As String = "Item_Uid"
    Public Const Col1Item As String = "Item"
    Public Const Col1ItemGroup As String = "Item Group"
    Public Const Col1ItemCategory As String = "Item Category"
    Public Const Col1Dimension1 As String = "Dimension1"
    Public Const Col1Dimension2 As String = "Dimension2"
    Public Const Col1LotNo As String = "Lot No"
    Public Const Col1FromProcess As String = "From Process"
    Public Const Col1ProdOrder As String = "Prod Order"
    Public Const Col1ProdOrderSr As String = "Prod Order Sr"
    Public Const Col1Qty As String = "Qty"
    Public Const Col1Unit As String = "Unit"
    Public Const Col1QtyDecimalPlaces As String = "Qty Decimal Places"
    Public Const Col1MeasurePerPcs As String = "Measure Per Pcs"
    Public Const Col1TotalMeasure As String = "Total Measure"
    Public Const Col1MeasureUnit As String = "Measure Unit"
    Public Const Col1MeasureDecimalPlaces As String = "Measure Decimal Places"
    Public Const Col1Rate As String = "Rate"
    Public Const Col1IncentiveRate As String = "Incentive Rate"
    Public Const Col1Amount As String = "Amount"
    Public Const Col1ProcessSequence As String = "Process Sequence"
    Public Const Col1ProcessIterationsAllowed As String = "Process Iterations Allowed"
    Public Const Col1V_Nature As String = "V_Nature"
    Public Const Col1Remark As String = "Remark"

    Public WithEvents Dgl3 As New AgControls.AgDataGrid
    Public Const Col3Parameter As String = "Parameter"
    Public Const Col3StdValue As String = "Standard Value"

    Public WithEvents Dgl5 As New AgControls.AgDataGrid
    Public Const Col5Head As String = "Head"
    Public Const Col5AtRate As String = "@"
    Public Const Col5Amount As String = "Amount"

    Protected Const Row5GrossAmount As Byte = 0
    Protected Const Row5RoundOff As Byte = 1
    Protected Const Row5NetAmount As Byte = 2

    Public WithEvents Dgl6 As New AgControls.AgDataGrid
    Public Const Col6Head As String = "Head"
    Public Const Col6AtRate As String = "@"
    Public Const Col6Amount As String = "Amount"

    Protected Const Row6Freight As Byte = 0

    Protected mLastOrderBy$ = ""

    Dim ImportMessegeStr$ = ""
    Dim ImportMode As Boolean = False
    Dim ImportAction_NewImport As String = "New Import"
    Dim ImportAction_ClearImport As String = "Clear Import"
    Protected WithEvents BtnMaterialIssueDetail As System.Windows.Forms.Button

    Dim DtJobEnviro As DataTable = Nothing

    Dim isRecordLocked As Boolean
    Dim mJobRateHelpDataSet As DataSet = Nothing
    Protected WithEvents TxtMachine As AgControls.AgTextBox
    Protected WithEvents LblMachine As System.Windows.Forms.Label
    Protected WithEvents PnlCustomGrid As System.Windows.Forms.Panel
    Protected WithEvents TxtCustomFields As AgControls.AgTextBox
    Protected WithEvents Pnl6 As System.Windows.Forms.Panel
    Protected WithEvents Label6 As System.Windows.Forms.Label
    Protected WithEvents TxtTimePenaltyDays As AgControls.AgTextBox
    Protected WithEvents Label9 As System.Windows.Forms.Label
    Protected WithEvents TxtTimePenalty As AgControls.AgTextBox
    Protected WithEvents Label8 As System.Windows.Forms.Label
    Protected WithEvents TxtTimeIncentive As AgControls.AgTextBox

    Dim mMeasureField$ = ""

    Public Sub New(ByVal StrUPVar As String, ByVal DTUP As DataTable, ByVal strNCat As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Topctrl1.FSetParent(Me, StrUPVar, DTUP)
        Topctrl1.SetDisp(True)

        EntryNCat = strNCat

        mQry = "Select H.* from Voucher_Type_Settings H  Left Join Voucher_Type Vt  On H.V_Type = Vt.V_Type  Where Vt.NCat In ('" & EntryNCat & "') And H.Div_Code = '" & AgL.PubDivCode & "' And H.Site_Code ='" & AgL.PubSiteCode & "' "
        DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
        DtJobEnviro = AgL.FillData("SELECT H.* FROM JobEnviro H Left Join Voucher_Type Vt On H.V_Type = Vt.V_Type Where Vt.NCat In ('" & EntryNCat & "') And H.Div_Code = '" & AgL.PubDivCode & "' And H.Site_Code ='" & AgL.PubSiteCode & "'", AgL.GCn).Tables(0)
    End Sub

#Region "Form Designer Code"
    Private Sub InitializeComponent()
        Me.Dgl1 = New AgControls.AgDataGrid
        Me.TxtManualRefNo = New AgControls.AgTextBox
        Me.LblManualRefNo = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.LblTotalAmount = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.LblTotalMeasure = New System.Windows.Forms.Label
        Me.LblTotalMeasureText = New System.Windows.Forms.Label
        Me.LblTotalQty = New System.Windows.Forms.Label
        Me.LblTotalQtyText = New System.Windows.Forms.Label
        Me.Pnl1 = New System.Windows.Forms.Panel
        Me.Label30 = New System.Windows.Forms.Label
        Me.TxtRemarks = New AgControls.AgTextBox
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel
        Me.LblJobWorkerReq = New System.Windows.Forms.Label
        Me.TxtJobWorker = New AgControls.AgTextBox
        Me.LblJobWorker = New System.Windows.Forms.Label
        Me.TxtDueDate = New AgControls.AgTextBox
        Me.LblDueDate = New System.Windows.Forms.Label
        Me.TxtTermsAndConditions = New AgControls.AgTextBox
        Me.LinkLabel2 = New System.Windows.Forms.LinkLabel
        Me.Pnl3 = New System.Windows.Forms.Panel
        Me.LblJobInstructions = New System.Windows.Forms.LinkLabel
        Me.TxtInsideOutside = New AgControls.AgTextBox
        Me.LblInsideOutside = New System.Windows.Forms.Label
        Me.TxtBillingType = New AgControls.AgTextBox
        Me.Label32 = New System.Windows.Forms.Label
        Me.TxtOrderBy = New AgControls.AgTextBox
        Me.LblOrderBy = New System.Windows.Forms.Label
        Me.LblOrderByReq = New System.Windows.Forms.Label
        Me.LblDueDateReq = New System.Windows.Forms.Label
        Me.TxtGodown = New AgControls.AgTextBox
        Me.LblGodown = New System.Windows.Forms.Label
        Me.LblWithMaterialYN = New System.Windows.Forms.Label
        Me.TxtWithMaterialYN = New AgControls.AgTextBox
        Me.TxtRate = New AgControls.AgTextBox
        Me.LblRate = New System.Windows.Forms.Label
        Me.Pnl5 = New System.Windows.Forms.Panel
        Me.RbtAllItems = New System.Windows.Forms.RadioButton
        Me.RbtForStock = New System.Windows.Forms.RadioButton
        Me.Label3 = New System.Windows.Forms.Label
        Me.TxtProcess = New AgControls.AgTextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.BtnImprtFromText = New System.Windows.Forms.Button
        Me.ChkShowOnlyImportedRecords = New System.Windows.Forms.CheckBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.TxtItemDivision = New AgControls.AgTextBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.RbtForProdOrder = New System.Windows.Forms.RadioButton
        Me.BtnMaterialIssueDetail = New System.Windows.Forms.Button
        Me.TxtMachine = New AgControls.AgTextBox
        Me.LblMachine = New System.Windows.Forms.Label
        Me.PnlCustomGrid = New System.Windows.Forms.Panel
        Me.TxtCustomFields = New AgControls.AgTextBox
        Me.Pnl6 = New System.Windows.Forms.Panel
        Me.Label6 = New System.Windows.Forms.Label
        Me.TxtTimePenaltyDays = New AgControls.AgTextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.TxtTimePenalty = New AgControls.AgTextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.TxtTimeIncentive = New AgControls.AgTextBox
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
        Me.Label2.Location = New System.Drawing.Point(106, 33)
        Me.Label2.Tag = ""
        '
        'LblV_Date
        '
        Me.LblV_Date.BackColor = System.Drawing.Color.Transparent
        Me.LblV_Date.Location = New System.Drawing.Point(10, 28)
        Me.LblV_Date.Size = New System.Drawing.Size(71, 16)
        Me.LblV_Date.Tag = ""
        Me.LblV_Date.Text = "Order Date"
        '
        'LblV_TypeReq
        '
        Me.LblV_TypeReq.Location = New System.Drawing.Point(308, 13)
        Me.LblV_TypeReq.Tag = ""
        '
        'TxtV_Date
        '
        Me.TxtV_Date.AgSelectedValue = ""
        Me.TxtV_Date.BackColor = System.Drawing.Color.White
        Me.TxtV_Date.Location = New System.Drawing.Point(123, 27)
        Me.TxtV_Date.TabIndex = 2
        Me.TxtV_Date.Tag = ""
        '
        'LblV_Type
        '
        Me.LblV_Type.Location = New System.Drawing.Point(229, 9)
        Me.LblV_Type.Size = New System.Drawing.Size(71, 16)
        Me.LblV_Type.Tag = ""
        Me.LblV_Type.Text = "Order Type"
        '
        'TxtV_Type
        '
        Me.TxtV_Type.AgSelectedValue = ""
        Me.TxtV_Type.BackColor = System.Drawing.Color.White
        Me.TxtV_Type.Location = New System.Drawing.Point(324, 7)
        Me.TxtV_Type.Size = New System.Drawing.Size(153, 18)
        Me.TxtV_Type.TabIndex = 1
        Me.TxtV_Type.Tag = ""
        '
        'LblSite_CodeReq
        '
        Me.LblSite_CodeReq.Location = New System.Drawing.Point(106, 13)
        Me.LblSite_CodeReq.Tag = ""
        '
        'LblSite_Code
        '
        Me.LblSite_Code.BackColor = System.Drawing.Color.Transparent
        Me.LblSite_Code.Location = New System.Drawing.Point(10, 9)
        Me.LblSite_Code.Size = New System.Drawing.Size(87, 16)
        Me.LblSite_Code.Tag = ""
        Me.LblSite_Code.Text = "Branch Name"
        '
        'TxtSite_Code
        '
        Me.TxtSite_Code.AgMandatory = True
        Me.TxtSite_Code.AgSelectedValue = ""
        Me.TxtSite_Code.BackColor = System.Drawing.Color.White
        Me.TxtSite_Code.Location = New System.Drawing.Point(123, 7)
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
        Me.TabControl1.Size = New System.Drawing.Size(991, 142)
        Me.TabControl1.TabIndex = 0
        '
        'TP1
        '
        Me.TP1.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.TP1.Controls.Add(Me.Label6)
        Me.TP1.Controls.Add(Me.TxtTimePenaltyDays)
        Me.TP1.Controls.Add(Me.Label9)
        Me.TP1.Controls.Add(Me.TxtTimePenalty)
        Me.TP1.Controls.Add(Me.Label8)
        Me.TP1.Controls.Add(Me.TxtTimeIncentive)
        Me.TP1.Controls.Add(Me.TxtCustomFields)
        Me.TP1.Controls.Add(Me.TxtMachine)
        Me.TP1.Controls.Add(Me.LblMachine)
        Me.TP1.Controls.Add(Me.Label10)
        Me.TP1.Controls.Add(Me.TxtItemDivision)
        Me.TP1.Controls.Add(Me.Label11)
        Me.TP1.Controls.Add(Me.Label7)
        Me.TP1.Controls.Add(Me.TxtProcess)
        Me.TP1.Controls.Add(Me.Label4)
        Me.TP1.Controls.Add(Me.Label5)
        Me.TP1.Controls.Add(Me.Label3)
        Me.TP1.Controls.Add(Me.TxtRate)
        Me.TP1.Controls.Add(Me.LblRate)
        Me.TP1.Controls.Add(Me.LblWithMaterialYN)
        Me.TP1.Controls.Add(Me.TxtWithMaterialYN)
        Me.TP1.Controls.Add(Me.TxtGodown)
        Me.TP1.Controls.Add(Me.LblGodown)
        Me.TP1.Controls.Add(Me.LblDueDateReq)
        Me.TP1.Controls.Add(Me.TxtOrderBy)
        Me.TP1.Controls.Add(Me.LblOrderBy)
        Me.TP1.Controls.Add(Me.LblOrderByReq)
        Me.TP1.Controls.Add(Me.TxtInsideOutside)
        Me.TP1.Controls.Add(Me.LblInsideOutside)
        Me.TP1.Controls.Add(Me.TxtManualRefNo)
        Me.TP1.Controls.Add(Me.Label32)
        Me.TP1.Controls.Add(Me.TxtBillingType)
        Me.TP1.Controls.Add(Me.LblManualRefNo)
        Me.TP1.Controls.Add(Me.TxtDueDate)
        Me.TP1.Controls.Add(Me.LblDueDate)
        Me.TP1.Controls.Add(Me.TxtRemarks)
        Me.TP1.Controls.Add(Me.Label30)
        Me.TP1.Controls.Add(Me.TxtJobWorker)
        Me.TP1.Controls.Add(Me.LblJobWorker)
        Me.TP1.Controls.Add(Me.LblJobWorkerReq)
        Me.TP1.Location = New System.Drawing.Point(4, 22)
        Me.TP1.Size = New System.Drawing.Size(983, 116)
        Me.TP1.Text = "Document Detail"
        Me.TP1.Controls.SetChildIndex(Me.LblJobWorkerReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblJobWorker, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtJobWorker, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label30, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtRemarks, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblDueDate, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtDueDate, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblManualRefNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtBillingType, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label32, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtManualRefNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblInsideOutside, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtInsideOutside, 0)
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
        Me.TP1.Controls.SetChildIndex(Me.LblOrderByReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblOrderBy, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtOrderBy, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblDueDateReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblGodown, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtGodown, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtWithMaterialYN, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblWithMaterialYN, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblRate, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtRate, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label3, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label5, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label4, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtProcess, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label7, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label11, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtItemDivision, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label10, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblMachine, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtMachine, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtCustomFields, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtTimeIncentive, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label8, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtTimePenalty, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label9, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtTimePenaltyDays, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label6, 0)
        '
        'Topctrl1
        '
        Me.Topctrl1.Size = New System.Drawing.Size(984, 41)
        Me.Topctrl1.TabIndex = 6
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
        Me.TxtManualRefNo.Location = New System.Drawing.Point(324, 27)
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
        Me.LblManualRefNo.Location = New System.Drawing.Point(229, 27)
        Me.LblManualRefNo.Name = "LblManualRefNo"
        Me.LblManualRefNo.Size = New System.Drawing.Size(60, 16)
        Me.LblManualRefNo.TabIndex = 706
        Me.LblManualRefNo.Text = "Order No"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Cornsilk
        Me.Panel1.Controls.Add(Me.LblTotalAmount)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.LblTotalMeasure)
        Me.Panel1.Controls.Add(Me.LblTotalMeasureText)
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
        'LblTotalMeasure
        '
        Me.LblTotalMeasure.AutoSize = True
        Me.LblTotalMeasure.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalMeasure.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalMeasure.Location = New System.Drawing.Point(460, 3)
        Me.LblTotalMeasure.Name = "LblTotalMeasure"
        Me.LblTotalMeasure.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalMeasure.TabIndex = 670
        Me.LblTotalMeasure.Text = "."
        Me.LblTotalMeasure.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTotalMeasureText
        '
        Me.LblTotalMeasureText.AutoSize = True
        Me.LblTotalMeasureText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalMeasureText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalMeasureText.Location = New System.Drawing.Point(354, 3)
        Me.LblTotalMeasureText.Name = "LblTotalMeasureText"
        Me.LblTotalMeasureText.Size = New System.Drawing.Size(105, 16)
        Me.LblTotalMeasureText.TabIndex = 669
        Me.LblTotalMeasureText.Text = "Total Measure :"
        '
        'LblTotalQty
        '
        Me.LblTotalQty.AutoSize = True
        Me.LblTotalQty.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalQty.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalQty.Location = New System.Drawing.Point(94, 3)
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
        Me.LblTotalQtyText.Location = New System.Drawing.Point(9, 3)
        Me.LblTotalQtyText.Name = "LblTotalQtyText"
        Me.LblTotalQtyText.Size = New System.Drawing.Size(72, 16)
        Me.LblTotalQtyText.TabIndex = 667
        Me.LblTotalQtyText.Text = "Total Qty :"
        '
        'Pnl1
        '
        Me.Pnl1.Location = New System.Drawing.Point(4, 186)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(972, 252)
        Me.Pnl1.TabIndex = 1
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(486, 88)
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
        Me.TxtRemarks.Location = New System.Drawing.Point(587, 87)
        Me.TxtRemarks.MaxLength = 255
        Me.TxtRemarks.Name = "TxtRemarks"
        Me.TxtRemarks.Size = New System.Drawing.Size(384, 18)
        Me.TxtRemarks.TabIndex = 17
        '
        'LinkLabel1
        '
        Me.LinkLabel1.BackColor = System.Drawing.Color.SteelBlue
        Me.LinkLabel1.DisabledLinkColor = System.Drawing.Color.White
        Me.LinkLabel1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel1.LinkColor = System.Drawing.Color.White
        Me.LinkLabel1.Location = New System.Drawing.Point(4, 165)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(190, 20)
        Me.LinkLabel1.TabIndex = 731
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "Job Order For Following Items"
        Me.LinkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblJobWorkerReq
        '
        Me.LblJobWorkerReq.AutoSize = True
        Me.LblJobWorkerReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblJobWorkerReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblJobWorkerReq.Location = New System.Drawing.Point(106, 92)
        Me.LblJobWorkerReq.Name = "LblJobWorkerReq"
        Me.LblJobWorkerReq.Size = New System.Drawing.Size(10, 7)
        Me.LblJobWorkerReq.TabIndex = 732
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
        Me.TxtJobWorker.Location = New System.Drawing.Point(123, 87)
        Me.TxtJobWorker.MaxLength = 20
        Me.TxtJobWorker.Name = "TxtJobWorker"
        Me.TxtJobWorker.Size = New System.Drawing.Size(354, 18)
        Me.TxtJobWorker.TabIndex = 7
        '
        'LblJobWorker
        '
        Me.LblJobWorker.AutoSize = True
        Me.LblJobWorker.BackColor = System.Drawing.Color.Transparent
        Me.LblJobWorker.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblJobWorker.Location = New System.Drawing.Point(10, 87)
        Me.LblJobWorker.Name = "LblJobWorker"
        Me.LblJobWorker.Size = New System.Drawing.Size(74, 16)
        Me.LblJobWorker.TabIndex = 731
        Me.LblJobWorker.Text = "Job Worker"
        '
        'TxtDueDate
        '
        Me.TxtDueDate.AgAllowUserToEnableMasterHelp = False
        Me.TxtDueDate.AgLastValueTag = Nothing
        Me.TxtDueDate.AgLastValueText = Nothing
        Me.TxtDueDate.AgMandatory = True
        Me.TxtDueDate.AgMasterHelp = False
        Me.TxtDueDate.AgNumberLeftPlaces = 0
        Me.TxtDueDate.AgNumberNegetiveAllow = False
        Me.TxtDueDate.AgNumberRightPlaces = 0
        Me.TxtDueDate.AgPickFromLastValue = False
        Me.TxtDueDate.AgRowFilter = ""
        Me.TxtDueDate.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtDueDate.AgSelectedValue = Nothing
        Me.TxtDueDate.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtDueDate.AgValueType = AgControls.AgTextBox.TxtValueType.Date_Value
        Me.TxtDueDate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtDueDate.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDueDate.Location = New System.Drawing.Point(123, 47)
        Me.TxtDueDate.MaxLength = 0
        Me.TxtDueDate.Name = "TxtDueDate"
        Me.TxtDueDate.Size = New System.Drawing.Size(100, 18)
        Me.TxtDueDate.TabIndex = 4
        '
        'LblDueDate
        '
        Me.LblDueDate.AutoSize = True
        Me.LblDueDate.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDueDate.Location = New System.Drawing.Point(10, 47)
        Me.LblDueDate.Name = "LblDueDate"
        Me.LblDueDate.Size = New System.Drawing.Size(62, 16)
        Me.LblDueDate.TabIndex = 736
        Me.LblDueDate.Text = "Due Date"
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
        Me.TxtTermsAndConditions.Location = New System.Drawing.Point(7, 486)
        Me.TxtTermsAndConditions.MaxLength = 255
        Me.TxtTermsAndConditions.Multiline = True
        Me.TxtTermsAndConditions.Name = "TxtTermsAndConditions"
        Me.TxtTermsAndConditions.Size = New System.Drawing.Size(187, 90)
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
        Me.LinkLabel2.Size = New System.Drawing.Size(162, 20)
        Me.LinkLabel2.TabIndex = 748
        Me.LinkLabel2.TabStop = True
        Me.LinkLabel2.Text = "Job Terms && Conditions"
        Me.LinkLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Pnl3
        '
        Me.Pnl3.Location = New System.Drawing.Point(374, 485)
        Me.Pnl3.Name = "Pnl3"
        Me.Pnl3.Size = New System.Drawing.Size(246, 92)
        Me.Pnl3.TabIndex = 4
        '
        'LblJobInstructions
        '
        Me.LblJobInstructions.BackColor = System.Drawing.Color.SteelBlue
        Me.LblJobInstructions.DisabledLinkColor = System.Drawing.Color.White
        Me.LblJobInstructions.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblJobInstructions.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LblJobInstructions.LinkColor = System.Drawing.Color.White
        Me.LblJobInstructions.Location = New System.Drawing.Point(377, 464)
        Me.LblJobInstructions.Name = "LblJobInstructions"
        Me.LblJobInstructions.Size = New System.Drawing.Size(114, 20)
        Me.LblJobInstructions.TabIndex = 750
        Me.LblJobInstructions.TabStop = True
        Me.LblJobInstructions.Text = "Job Instructions"
        Me.LblJobInstructions.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtInsideOutside
        '
        Me.TxtInsideOutside.AgAllowUserToEnableMasterHelp = False
        Me.TxtInsideOutside.AgLastValueTag = Nothing
        Me.TxtInsideOutside.AgLastValueText = Nothing
        Me.TxtInsideOutside.AgMandatory = False
        Me.TxtInsideOutside.AgMasterHelp = False
        Me.TxtInsideOutside.AgNumberLeftPlaces = 8
        Me.TxtInsideOutside.AgNumberNegetiveAllow = False
        Me.TxtInsideOutside.AgNumberRightPlaces = 2
        Me.TxtInsideOutside.AgPickFromLastValue = False
        Me.TxtInsideOutside.AgRowFilter = ""
        Me.TxtInsideOutside.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtInsideOutside.AgSelectedValue = Nothing
        Me.TxtInsideOutside.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtInsideOutside.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtInsideOutside.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtInsideOutside.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtInsideOutside.Location = New System.Drawing.Point(871, 27)
        Me.TxtInsideOutside.MaxLength = 50
        Me.TxtInsideOutside.Name = "TxtInsideOutside"
        Me.TxtInsideOutside.Size = New System.Drawing.Size(100, 18)
        Me.TxtInsideOutside.TabIndex = 11
        '
        'LblInsideOutside
        '
        Me.LblInsideOutside.AutoSize = True
        Me.LblInsideOutside.BackColor = System.Drawing.Color.Transparent
        Me.LblInsideOutside.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblInsideOutside.Location = New System.Drawing.Point(756, 28)
        Me.LblInsideOutside.Name = "LblInsideOutside"
        Me.LblInsideOutside.Size = New System.Drawing.Size(91, 16)
        Me.LblInsideOutside.TabIndex = 749
        Me.LblInsideOutside.Text = "Inside/Outside"
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
        'TxtOrderBy
        '
        Me.TxtOrderBy.AgAllowUserToEnableMasterHelp = False
        Me.TxtOrderBy.AgLastValueTag = Nothing
        Me.TxtOrderBy.AgLastValueText = Nothing
        Me.TxtOrderBy.AgMandatory = True
        Me.TxtOrderBy.AgMasterHelp = False
        Me.TxtOrderBy.AgNumberLeftPlaces = 8
        Me.TxtOrderBy.AgNumberNegetiveAllow = False
        Me.TxtOrderBy.AgNumberRightPlaces = 2
        Me.TxtOrderBy.AgPickFromLastValue = False
        Me.TxtOrderBy.AgRowFilter = ""
        Me.TxtOrderBy.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtOrderBy.AgSelectedValue = Nothing
        Me.TxtOrderBy.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtOrderBy.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtOrderBy.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtOrderBy.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtOrderBy.Location = New System.Drawing.Point(123, 67)
        Me.TxtOrderBy.MaxLength = 20
        Me.TxtOrderBy.Name = "TxtOrderBy"
        Me.TxtOrderBy.Size = New System.Drawing.Size(354, 18)
        Me.TxtOrderBy.TabIndex = 6
        '
        'LblOrderBy
        '
        Me.LblOrderBy.AutoSize = True
        Me.LblOrderBy.BackColor = System.Drawing.Color.Transparent
        Me.LblOrderBy.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblOrderBy.Location = New System.Drawing.Point(10, 67)
        Me.LblOrderBy.Name = "LblOrderBy"
        Me.LblOrderBy.Size = New System.Drawing.Size(60, 16)
        Me.LblOrderBy.TabIndex = 751
        Me.LblOrderBy.Text = "Order By"
        '
        'LblOrderByReq
        '
        Me.LblOrderByReq.AutoSize = True
        Me.LblOrderByReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblOrderByReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblOrderByReq.Location = New System.Drawing.Point(106, 73)
        Me.LblOrderByReq.Name = "LblOrderByReq"
        Me.LblOrderByReq.Size = New System.Drawing.Size(10, 7)
        Me.LblOrderByReq.TabIndex = 752
        Me.LblOrderByReq.Text = "Ä"
        '
        'LblDueDateReq
        '
        Me.LblDueDateReq.AutoSize = True
        Me.LblDueDateReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblDueDateReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblDueDateReq.Location = New System.Drawing.Point(106, 52)
        Me.LblDueDateReq.Name = "LblDueDateReq"
        Me.LblDueDateReq.Size = New System.Drawing.Size(10, 7)
        Me.LblDueDateReq.TabIndex = 753
        Me.LblDueDateReq.Text = "Ä"
        '
        'TxtGodown
        '
        Me.TxtGodown.AgAllowUserToEnableMasterHelp = False
        Me.TxtGodown.AgLastValueTag = Nothing
        Me.TxtGodown.AgLastValueText = Nothing
        Me.TxtGodown.AgMandatory = True
        Me.TxtGodown.AgMasterHelp = False
        Me.TxtGodown.AgNumberLeftPlaces = 0
        Me.TxtGodown.AgNumberNegetiveAllow = False
        Me.TxtGodown.AgNumberRightPlaces = 0
        Me.TxtGodown.AgPickFromLastValue = False
        Me.TxtGodown.AgRowFilter = ""
        Me.TxtGodown.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtGodown.AgSelectedValue = Nothing
        Me.TxtGodown.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtGodown.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtGodown.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtGodown.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtGodown.Location = New System.Drawing.Point(587, 27)
        Me.TxtGodown.MaxLength = 255
        Me.TxtGodown.Name = "TxtGodown"
        Me.TxtGodown.Size = New System.Drawing.Size(163, 18)
        Me.TxtGodown.TabIndex = 10
        '
        'LblGodown
        '
        Me.LblGodown.AutoSize = True
        Me.LblGodown.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblGodown.Location = New System.Drawing.Point(486, 28)
        Me.LblGodown.Name = "LblGodown"
        Me.LblGodown.Size = New System.Drawing.Size(55, 16)
        Me.LblGodown.TabIndex = 757
        Me.LblGodown.Text = "Godown"
        '
        'LblWithMaterialYN
        '
        Me.LblWithMaterialYN.AutoSize = True
        Me.LblWithMaterialYN.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblWithMaterialYN.Location = New System.Drawing.Point(756, 49)
        Me.LblWithMaterialYN.Name = "LblWithMaterialYN"
        Me.LblWithMaterialYN.Size = New System.Drawing.Size(111, 16)
        Me.LblWithMaterialYN.TabIndex = 761
        Me.LblWithMaterialYN.Text = "With Material Y/N"
        '
        'TxtWithMaterialYN
        '
        Me.TxtWithMaterialYN.AgAllowUserToEnableMasterHelp = False
        Me.TxtWithMaterialYN.AgLastValueTag = Nothing
        Me.TxtWithMaterialYN.AgLastValueText = Nothing
        Me.TxtWithMaterialYN.AgMandatory = False
        Me.TxtWithMaterialYN.AgMasterHelp = False
        Me.TxtWithMaterialYN.AgNumberLeftPlaces = 0
        Me.TxtWithMaterialYN.AgNumberNegetiveAllow = False
        Me.TxtWithMaterialYN.AgNumberRightPlaces = 0
        Me.TxtWithMaterialYN.AgPickFromLastValue = False
        Me.TxtWithMaterialYN.AgRowFilter = ""
        Me.TxtWithMaterialYN.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtWithMaterialYN.AgSelectedValue = Nothing
        Me.TxtWithMaterialYN.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtWithMaterialYN.AgValueType = AgControls.AgTextBox.TxtValueType.YesNo_Value
        Me.TxtWithMaterialYN.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtWithMaterialYN.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtWithMaterialYN.Location = New System.Drawing.Point(870, 47)
        Me.TxtWithMaterialYN.MaxLength = 20
        Me.TxtWithMaterialYN.Name = "TxtWithMaterialYN"
        Me.TxtWithMaterialYN.Size = New System.Drawing.Size(101, 18)
        Me.TxtWithMaterialYN.TabIndex = 13
        '
        'TxtRate
        '
        Me.TxtRate.AgAllowUserToEnableMasterHelp = False
        Me.TxtRate.AgLastValueTag = Nothing
        Me.TxtRate.AgLastValueText = Nothing
        Me.TxtRate.AgMandatory = False
        Me.TxtRate.AgMasterHelp = False
        Me.TxtRate.AgNumberLeftPlaces = 0
        Me.TxtRate.AgNumberNegetiveAllow = False
        Me.TxtRate.AgNumberRightPlaces = 0
        Me.TxtRate.AgPickFromLastValue = False
        Me.TxtRate.AgRowFilter = ""
        Me.TxtRate.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtRate.AgSelectedValue = Nothing
        Me.TxtRate.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtRate.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtRate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtRate.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtRate.Location = New System.Drawing.Point(871, 7)
        Me.TxtRate.MaxLength = 0
        Me.TxtRate.Name = "TxtRate"
        Me.TxtRate.Size = New System.Drawing.Size(100, 18)
        Me.TxtRate.TabIndex = 9
        '
        'LblRate
        '
        Me.LblRate.AutoSize = True
        Me.LblRate.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblRate.Location = New System.Drawing.Point(756, 8)
        Me.LblRate.Name = "LblRate"
        Me.LblRate.Size = New System.Drawing.Size(35, 16)
        Me.LblRate.TabIndex = 763
        Me.LblRate.Text = "Rate"
        '
        'Pnl5
        '
        Me.Pnl5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Pnl5.Location = New System.Drawing.Point(624, 462)
        Me.Pnl5.Name = "Pnl5"
        Me.Pnl5.Size = New System.Drawing.Size(353, 93)
        Me.Pnl5.TabIndex = 5
        '
        'RbtAllItems
        '
        Me.RbtAllItems.AutoSize = True
        Me.RbtAllItems.BackColor = System.Drawing.Color.Transparent
        Me.RbtAllItems.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RbtAllItems.Location = New System.Drawing.Point(198, 167)
        Me.RbtAllItems.Name = "RbtAllItems"
        Me.RbtAllItems.Size = New System.Drawing.Size(84, 17)
        Me.RbtAllItems.TabIndex = 759
        Me.RbtAllItems.TabStop = True
        Me.RbtAllItems.Text = "All Items"
        Me.RbtAllItems.UseVisualStyleBackColor = False
        '
        'RbtForStock
        '
        Me.RbtForStock.AutoSize = True
        Me.RbtForStock.BackColor = System.Drawing.Color.Transparent
        Me.RbtForStock.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RbtForStock.Location = New System.Drawing.Point(283, 167)
        Me.RbtForStock.Name = "RbtForStock"
        Me.RbtForStock.Size = New System.Drawing.Size(87, 17)
        Me.RbtForStock.TabIndex = 760
        Me.RbtForStock.TabStop = True
        Me.RbtForStock.Text = "For Stock"
        Me.RbtForStock.UseVisualStyleBackColor = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(308, 34)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(10, 7)
        Me.Label3.TabIndex = 764
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
        Me.TxtProcess.Location = New System.Drawing.Point(324, 47)
        Me.TxtProcess.MaxLength = 20
        Me.TxtProcess.Name = "TxtProcess"
        Me.TxtProcess.Size = New System.Drawing.Size(153, 18)
        Me.TxtProcess.TabIndex = 5
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(229, 47)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(56, 16)
        Me.Label4.TabIndex = 766
        Me.Label4.Text = "Process"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(308, 53)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(10, 7)
        Me.Label5.TabIndex = 767
        Me.Label5.Text = "Ä"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label7.Location = New System.Drawing.Point(570, 32)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(10, 7)
        Me.Label7.TabIndex = 769
        Me.Label7.Text = "Ä"
        '
        'BtnImprtFromText
        '
        Me.BtnImprtFromText.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnImprtFromText.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnImprtFromText.Location = New System.Drawing.Point(871, 160)
        Me.BtnImprtFromText.Name = "BtnImprtFromText"
        Me.BtnImprtFromText.Size = New System.Drawing.Size(105, 25)
        Me.BtnImprtFromText.TabIndex = 762
        Me.BtnImprtFromText.TabStop = False
        Me.BtnImprtFromText.Text = "New Import"
        Me.BtnImprtFromText.UseVisualStyleBackColor = True
        '
        'ChkShowOnlyImportedRecords
        '
        Me.ChkShowOnlyImportedRecords.AutoSize = True
        Me.ChkShowOnlyImportedRecords.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkShowOnlyImportedRecords.Location = New System.Drawing.Point(652, 167)
        Me.ChkShowOnlyImportedRecords.Name = "ChkShowOnlyImportedRecords"
        Me.ChkShowOnlyImportedRecords.Size = New System.Drawing.Size(214, 17)
        Me.ChkShowOnlyImportedRecords.TabIndex = 0
        Me.ChkShowOnlyImportedRecords.Text = "Show Only Imported Records"
        Me.ChkShowOnlyImportedRecords.UseVisualStyleBackColor = True
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label10.Location = New System.Drawing.Point(856, 34)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(10, 7)
        Me.Label10.TabIndex = 772
        Me.Label10.Text = "Ä"
        '
        'TxtItemDivision
        '
        Me.TxtItemDivision.AgAllowUserToEnableMasterHelp = False
        Me.TxtItemDivision.AgLastValueTag = Nothing
        Me.TxtItemDivision.AgLastValueText = Nothing
        Me.TxtItemDivision.AgMandatory = False
        Me.TxtItemDivision.AgMasterHelp = False
        Me.TxtItemDivision.AgNumberLeftPlaces = 0
        Me.TxtItemDivision.AgNumberNegetiveAllow = False
        Me.TxtItemDivision.AgNumberRightPlaces = 0
        Me.TxtItemDivision.AgPickFromLastValue = False
        Me.TxtItemDivision.AgRowFilter = ""
        Me.TxtItemDivision.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtItemDivision.AgSelectedValue = Nothing
        Me.TxtItemDivision.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtItemDivision.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtItemDivision.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtItemDivision.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtItemDivision.Location = New System.Drawing.Point(587, 47)
        Me.TxtItemDivision.MaxLength = 255
        Me.TxtItemDivision.Name = "TxtItemDivision"
        Me.TxtItemDivision.Size = New System.Drawing.Size(163, 18)
        Me.TxtItemDivision.TabIndex = 12
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(486, 48)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(81, 16)
        Me.Label11.TabIndex = 771
        Me.Label11.Text = "Item Division"
        '
        'RbtForProdOrder
        '
        Me.RbtForProdOrder.AutoSize = True
        Me.RbtForProdOrder.BackColor = System.Drawing.Color.Transparent
        Me.RbtForProdOrder.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RbtForProdOrder.Location = New System.Drawing.Point(374, 167)
        Me.RbtForProdOrder.Name = "RbtForProdOrder"
        Me.RbtForProdOrder.Size = New System.Drawing.Size(162, 17)
        Me.RbtForProdOrder.TabIndex = 766
        Me.RbtForProdOrder.TabStop = True
        Me.RbtForProdOrder.Text = "For Production Order"
        Me.RbtForProdOrder.UseVisualStyleBackColor = False
        '
        'BtnMaterialIssueDetail
        '
        Me.BtnMaterialIssueDetail.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnMaterialIssueDetail.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnMaterialIssueDetail.Location = New System.Drawing.Point(572, 163)
        Me.BtnMaterialIssueDetail.Name = "BtnMaterialIssueDetail"
        Me.BtnMaterialIssueDetail.Size = New System.Drawing.Size(71, 23)
        Me.BtnMaterialIssueDetail.TabIndex = 767
        Me.BtnMaterialIssueDetail.TabStop = False
        Me.BtnMaterialIssueDetail.Text = "Material"
        Me.BtnMaterialIssueDetail.UseVisualStyleBackColor = True
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
        Me.TxtMachine.Location = New System.Drawing.Point(587, 7)
        Me.TxtMachine.MaxLength = 20
        Me.TxtMachine.Name = "TxtMachine"
        Me.TxtMachine.Size = New System.Drawing.Size(163, 18)
        Me.TxtMachine.TabIndex = 8
        '
        'LblMachine
        '
        Me.LblMachine.AutoSize = True
        Me.LblMachine.BackColor = System.Drawing.Color.Transparent
        Me.LblMachine.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblMachine.Location = New System.Drawing.Point(486, 8)
        Me.LblMachine.Name = "LblMachine"
        Me.LblMachine.Size = New System.Drawing.Size(57, 16)
        Me.LblMachine.TabIndex = 774
        Me.LblMachine.Text = "Machine"
        '
        'PnlCustomGrid
        '
        Me.PnlCustomGrid.Location = New System.Drawing.Point(203, 484)
        Me.PnlCustomGrid.Name = "PnlCustomGrid"
        Me.PnlCustomGrid.Size = New System.Drawing.Size(144, 95)
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
        Me.TxtCustomFields.Location = New System.Drawing.Point(797, 8)
        Me.TxtCustomFields.MaxLength = 20
        Me.TxtCustomFields.Name = "TxtCustomFields"
        Me.TxtCustomFields.Size = New System.Drawing.Size(72, 18)
        Me.TxtCustomFields.TabIndex = 1014
        Me.TxtCustomFields.Text = "AgTextBox1"
        Me.TxtCustomFields.Visible = False
        '
        'Pnl6
        '
        Me.Pnl6.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Pnl6.Location = New System.Drawing.Point(624, 554)
        Me.Pnl6.Name = "Pnl6"
        Me.Pnl6.Size = New System.Drawing.Size(353, 25)
        Me.Pnl6.TabIndex = 768
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(813, 67)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(93, 16)
        Me.Label6.TabIndex = 1020
        Me.Label6.Text = "Leverage Days"
        '
        'TxtTimePenaltyDays
        '
        Me.TxtTimePenaltyDays.AgAllowUserToEnableMasterHelp = False
        Me.TxtTimePenaltyDays.AgLastValueTag = Nothing
        Me.TxtTimePenaltyDays.AgLastValueText = Nothing
        Me.TxtTimePenaltyDays.AgMandatory = False
        Me.TxtTimePenaltyDays.AgMasterHelp = False
        Me.TxtTimePenaltyDays.AgNumberLeftPlaces = 3
        Me.TxtTimePenaltyDays.AgNumberNegetiveAllow = False
        Me.TxtTimePenaltyDays.AgNumberRightPlaces = 0
        Me.TxtTimePenaltyDays.AgPickFromLastValue = False
        Me.TxtTimePenaltyDays.AgRowFilter = ""
        Me.TxtTimePenaltyDays.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtTimePenaltyDays.AgSelectedValue = Nothing
        Me.TxtTimePenaltyDays.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtTimePenaltyDays.AgValueType = AgControls.AgTextBox.TxtValueType.Number_Value
        Me.TxtTimePenaltyDays.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtTimePenaltyDays.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtTimePenaltyDays.Location = New System.Drawing.Point(915, 67)
        Me.TxtTimePenaltyDays.MaxLength = 50
        Me.TxtTimePenaltyDays.Name = "TxtTimePenaltyDays"
        Me.TxtTimePenaltyDays.Size = New System.Drawing.Size(56, 18)
        Me.TxtTimePenaltyDays.TabIndex = 16
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(650, 67)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(84, 16)
        Me.Label9.TabIndex = 1019
        Me.Label9.Text = "Time Penalty"
        '
        'TxtTimePenalty
        '
        Me.TxtTimePenalty.AgAllowUserToEnableMasterHelp = False
        Me.TxtTimePenalty.AgLastValueTag = Nothing
        Me.TxtTimePenalty.AgLastValueText = Nothing
        Me.TxtTimePenalty.AgMandatory = False
        Me.TxtTimePenalty.AgMasterHelp = False
        Me.TxtTimePenalty.AgNumberLeftPlaces = 8
        Me.TxtTimePenalty.AgNumberNegetiveAllow = False
        Me.TxtTimePenalty.AgNumberRightPlaces = 3
        Me.TxtTimePenalty.AgPickFromLastValue = False
        Me.TxtTimePenalty.AgRowFilter = ""
        Me.TxtTimePenalty.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtTimePenalty.AgSelectedValue = Nothing
        Me.TxtTimePenalty.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtTimePenalty.AgValueType = AgControls.AgTextBox.TxtValueType.Number_Value
        Me.TxtTimePenalty.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtTimePenalty.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtTimePenalty.Location = New System.Drawing.Point(747, 67)
        Me.TxtTimePenalty.MaxLength = 50
        Me.TxtTimePenalty.Name = "TxtTimePenalty"
        Me.TxtTimePenalty.Size = New System.Drawing.Size(56, 18)
        Me.TxtTimePenalty.TabIndex = 15
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(486, 67)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(90, 16)
        Me.Label8.TabIndex = 1018
        Me.Label8.Text = "Time Incentive"
        '
        'TxtTimeIncentive
        '
        Me.TxtTimeIncentive.AgAllowUserToEnableMasterHelp = False
        Me.TxtTimeIncentive.AgLastValueTag = Nothing
        Me.TxtTimeIncentive.AgLastValueText = Nothing
        Me.TxtTimeIncentive.AgMandatory = False
        Me.TxtTimeIncentive.AgMasterHelp = False
        Me.TxtTimeIncentive.AgNumberLeftPlaces = 8
        Me.TxtTimeIncentive.AgNumberNegetiveAllow = False
        Me.TxtTimeIncentive.AgNumberRightPlaces = 3
        Me.TxtTimeIncentive.AgPickFromLastValue = False
        Me.TxtTimeIncentive.AgRowFilter = ""
        Me.TxtTimeIncentive.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtTimeIncentive.AgSelectedValue = Nothing
        Me.TxtTimeIncentive.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtTimeIncentive.AgValueType = AgControls.AgTextBox.TxtValueType.Number_Value
        Me.TxtTimeIncentive.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtTimeIncentive.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtTimeIncentive.Location = New System.Drawing.Point(587, 67)
        Me.TxtTimeIncentive.MaxLength = 50
        Me.TxtTimeIncentive.Name = "TxtTimeIncentive"
        Me.TxtTimeIncentive.Size = New System.Drawing.Size(56, 18)
        Me.TxtTimeIncentive.TabIndex = 14
        '
        'FrmJobOrder
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.BackColor = System.Drawing.SystemColors.ButtonShadow
        Me.ClientSize = New System.Drawing.Size(984, 626)
        Me.Controls.Add(Me.Pnl6)
        Me.Controls.Add(Me.PnlCustomGrid)
        Me.Controls.Add(Me.BtnMaterialIssueDetail)
        Me.Controls.Add(Me.RbtForProdOrder)
        Me.Controls.Add(Me.ChkShowOnlyImportedRecords)
        Me.Controls.Add(Me.BtnImprtFromText)
        Me.Controls.Add(Me.LblJobInstructions)
        Me.Controls.Add(Me.Pnl3)
        Me.Controls.Add(Me.LinkLabel2)
        Me.Controls.Add(Me.Pnl5)
        Me.Controls.Add(Me.TxtTermsAndConditions)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Pnl1)
        Me.Controls.Add(Me.RbtForStock)
        Me.Controls.Add(Me.RbtAllItems)
        Me.Name = "FrmJobOrder"
        Me.Text = "Template Job Order"
        Me.Controls.SetChildIndex(Me.TabControl1, 0)
        Me.Controls.SetChildIndex(Me.RbtAllItems, 0)
        Me.Controls.SetChildIndex(Me.RbtForStock, 0)
        Me.Controls.SetChildIndex(Me.Pnl1, 0)
        Me.Controls.SetChildIndex(Me.Panel1, 0)
        Me.Controls.SetChildIndex(Me.LinkLabel1, 0)
        Me.Controls.SetChildIndex(Me.TxtTermsAndConditions, 0)
        Me.Controls.SetChildIndex(Me.Pnl5, 0)
        Me.Controls.SetChildIndex(Me.LinkLabel2, 0)
        Me.Controls.SetChildIndex(Me.Pnl3, 0)
        Me.Controls.SetChildIndex(Me.LblJobInstructions, 0)
        Me.Controls.SetChildIndex(Me.GBoxApprove, 0)
        Me.Controls.SetChildIndex(Me.Topctrl1, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.GrpUP, 0)
        Me.Controls.SetChildIndex(Me.GBoxEntryType, 0)
        Me.Controls.SetChildIndex(Me.GBoxMoveToLog, 0)
        Me.Controls.SetChildIndex(Me.GroupBox2, 0)
        Me.Controls.SetChildIndex(Me.GBoxDivision, 0)
        Me.Controls.SetChildIndex(Me.BtnImprtFromText, 0)
        Me.Controls.SetChildIndex(Me.ChkShowOnlyImportedRecords, 0)
        Me.Controls.SetChildIndex(Me.RbtForProdOrder, 0)
        Me.Controls.SetChildIndex(Me.BtnMaterialIssueDetail, 0)
        Me.Controls.SetChildIndex(Me.PnlCustomGrid, 0)
        Me.Controls.SetChildIndex(Me.Pnl6, 0)
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
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Protected WithEvents TxtManualRefNo As AgControls.AgTextBox
    Protected WithEvents LblManualRefNo As System.Windows.Forms.Label
    Protected WithEvents Panel1 As System.Windows.Forms.Panel
    Protected WithEvents Pnl1 As System.Windows.Forms.Panel
    Protected WithEvents TxtRemarks As AgControls.AgTextBox
    Protected WithEvents Label30 As System.Windows.Forms.Label
    Protected WithEvents LblTotalMeasure As System.Windows.Forms.Label
    Protected WithEvents LblTotalMeasureText As System.Windows.Forms.Label
    Protected WithEvents LblTotalQty As System.Windows.Forms.Label
    Protected WithEvents LblTotalQtyText As System.Windows.Forms.Label
    Protected WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    Protected WithEvents LblJobWorkerReq As System.Windows.Forms.Label
    Protected WithEvents TxtJobWorker As AgControls.AgTextBox
    Protected WithEvents LblJobWorker As System.Windows.Forms.Label
    Protected WithEvents TxtDueDate As AgControls.AgTextBox
    Protected WithEvents LblDueDate As System.Windows.Forms.Label
    Protected WithEvents TxtTermsAndConditions As AgControls.AgTextBox
    Protected WithEvents LblTotalAmount As System.Windows.Forms.Label
    Protected WithEvents Label1 As System.Windows.Forms.Label
    Protected WithEvents LinkLabel2 As System.Windows.Forms.LinkLabel
    Protected WithEvents Pnl3 As System.Windows.Forms.Panel
    Protected WithEvents LblJobInstructions As System.Windows.Forms.LinkLabel
    Protected WithEvents TxtInsideOutside As AgControls.AgTextBox
    Protected WithEvents LblInsideOutside As System.Windows.Forms.Label
    Protected WithEvents TxtBillingType As AgControls.AgTextBox
    Protected WithEvents Label32 As System.Windows.Forms.Label
    Protected WithEvents TxtOrderBy As AgControls.AgTextBox
    Protected WithEvents LblOrderBy As System.Windows.Forms.Label
    Protected WithEvents LblOrderByReq As System.Windows.Forms.Label
    Protected WithEvents LblDueDateReq As System.Windows.Forms.Label
    Protected WithEvents TxtGodown As AgControls.AgTextBox
    Protected WithEvents LblGodown As System.Windows.Forms.Label
    Protected WithEvents LblWithMaterialYN As System.Windows.Forms.Label
    Protected WithEvents TxtWithMaterialYN As AgControls.AgTextBox
    Protected WithEvents TxtRate As AgControls.AgTextBox
    Protected WithEvents LblRate As System.Windows.Forms.Label
    Protected WithEvents Pnl5 As System.Windows.Forms.Panel
    Protected WithEvents RbtAllItems As System.Windows.Forms.RadioButton
    Protected WithEvents RbtForStock As System.Windows.Forms.RadioButton
    Protected WithEvents Label3 As System.Windows.Forms.Label
    Protected WithEvents TxtProcess As AgControls.AgTextBox
    Protected WithEvents Label4 As System.Windows.Forms.Label
    Protected WithEvents Label5 As System.Windows.Forms.Label
    Protected WithEvents Label7 As System.Windows.Forms.Label
    Protected WithEvents BtnImprtFromText As System.Windows.Forms.Button
    Protected WithEvents ChkShowOnlyImportedRecords As System.Windows.Forms.CheckBox
    Protected WithEvents Label10 As System.Windows.Forms.Label
    Protected WithEvents TxtItemDivision As AgControls.AgTextBox
    Protected WithEvents Label11 As System.Windows.Forms.Label
    Protected WithEvents RbtForProdOrder As System.Windows.Forms.RadioButton
#End Region

    Private Sub FrmFinishingOrder_BaseEvent_ApproveDeletion_InTrans(ByVal SearchCode As String, ByVal Conn As SQLiteConnection, ByVal Cmd As SQLiteCommand) Handles Me.BaseEvent_ApproveDeletion_InTrans
        Dim I As Integer = 0
        'For I = 0 To Dgl1.Rows.Count - 1
        '    If Dgl1.Item(Col1Item_Uid, I).Tag <> "" Then
        '        AgTemplate.ClsMain.FUpdateItem_UidOnDelete(Dgl1.Item(Col1Item_Uid, I).Tag, mSearchCode, Conn, Cmd)
        '    End If
        'Next


        mQry = " Delete from Stock Where DocId = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " Delete from StockProcess Where DocId = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " Delete from JobIssueDetail Where DocId = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " Delete from JobIssRec Where DocId = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " Delete from StockVirtual Where DocId = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " Delete from JobIssRecUid Where DocId = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " Delete from JobOrderBom Where DocId = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " UPDATE JobOrder Set CostCenter = Null Where CostCenter = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " Delete From CostCenterMast Where Code = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    End Sub

    Private Sub FrmQuality1_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "JobOrder"
        LogTableName = "JobOrder_Log"
        MainLineTableCsv = "JobOrderdetail,JobOrderQCInstruction"
        LogLineTableCsv = "JobOrderdetail_Log,JobOrderQCInstruction_Log"

        AgL.AddAgDataGrid(AgCustomGrid1, PnlCustomGrid)
        AgCustomGrid1.AgLibVar = AgL
        AgCustomGrid1.SplitGrid = True
        AgCustomGrid1.MnuText = Me.Name

        AgL.GridDesign(Dgl1)
        AgL.GridDesign(Dgl3)
        AgL.GridDesign(Dgl5)
        AgL.GridDesign(Dgl6)
    End Sub

    Private Sub FrmQuality1_BaseFunction_FIniMast(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMast
        Dim mCondStr$
        mCondStr = " " & AgL.CondStrFinancialYear("M.V_Date", AgL.PubStartDate, AgL.PubEndDate) &
                       " And " & AgL.PubSiteCondition("M.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "M.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        If ChkShowOnlyImportedRecords.Checked Then
            mCondStr = mCondStr & " And M.EntryStatus = '" & AgTemplate.ClsMain.LogStatus.LogImport & "' " &
                                    " And M.EntryBy = '" & AgL.PubUserName & "'"
        End If

        If IsApplyVTypePermission Then
            mCondStr = mCondStr & " And M.V_Type In (Select V_Type From User_VType_Permission VP Where VP.UserName = '" & AgL.PubUserName & "' And VP.Div_Code = '" & AgL.PubDivCode & "' And VP.Site_Code = '" & AgL.PubSiteCode & "') "
        End If


        mQry = " Select M.DocID As SearchCode " &
            " From JobOrder M   " &
            " Left Join Voucher_Type Vt   On M.V_Type = Vt.V_Type  " &
            " Where IFNull(IsDeleted,0) = 0  " & mCondStr & "  Order By M.V_Date, M.V_Type, M.V_No  "

        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mCondStr$

        mCondStr = " And IFNull(H.IsDeleted,0)=0 " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) &
                        " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "H.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        If ChkShowOnlyImportedRecords.Checked Then
            mCondStr = mCondStr & " And H.EntryStatus = '" & AgTemplate.ClsMain.LogStatus.LogImport & "' " &
                                    " And H.EntryBy = '" & AgL.PubUserName & "'"
        End If

        If IsApplyVTypePermission Then
            mCondStr = mCondStr & " And H.V_Type In (Select V_Type From User_VType_Permission VP Where VP.UserName = '" & AgL.PubUserName & "' And VP.Div_Code = '" & AgL.PubDivCode & "' And VP.Site_Code = '" & AgL.PubSiteCode & "') "
        End If


        AgL.PubFindQry = " SELECT H.DocId AS SearchCode, H.V_Type AS [ORDER_Type], H.V_Date AS [ORDER_Date],  " &
                    " H.ManualRefNo AS [Order_No], H.DueDate AS [Due_Date], " &
                    " SGJ.Name AS [Job_Worker], SGO.Name AS [ORDER_BY], G.Description AS Godown,  " &
                    " H.TotalQty AS [Total_Qty], H.TotalMeasure AS [Total_Measure], H.TotalAmount AS [Total_Amount],  " &
                    " H.Remarks, H.EntryBy AS [Entry_By], H.EntryDate AS [Entry_Date], " &
                    " H.ApproveBy AS [Approve By], H.ApproveDate AS [Approve Date]  " &
                    " FROM JobOrder H   " &
                    " LEFT JOIN Voucher_Type Vt   ON H.V_Type = vt.V_Type  " &
                    " LEFT JOIN SubGroup SGJ   ON SGJ.SubCode=H.JobWorker  " &
                    " LEFT JOIN SubGroup SGO   ON SGO.SubCode = H.OrderBy  " &
                    " LEFT JOIN Godown G   ON G.Code = H.Godown   " &
                    " Where 1=1  " & mCondStr
        AgL.PubFindQryOrdBy = "[Order Date]"
    End Sub

    Private Sub FrmProductionOrder_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl1, Col1Item_Uid, 80, 0, Col1Item_Uid, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_ItemUID")), Boolean), False, False)
            .AddAgTextColumn(Dgl1, Col1Item, 150, 0, Col1Item, True, False, False)
            .AddAgTextColumn(Dgl1, Col1ItemGroup, 100, 0, Col1ItemGroup, True, True)

            If AgL.PubDivCode = "K" Then
                .AddAgTextColumn(Dgl1, Col1ItemCategory, 100, 0, Col1ItemCategory, True, True)
            Else
                .AddAgTextColumn(Dgl1, Col1ItemCategory, 100, 0, Col1ItemCategory, False, True)
            End If

            .AddAgTextColumn(Dgl1, Col1Dimension1, 100, 0, AgTemplate.ClsMain.FGetDimension1Caption(), CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Dimension1")), Boolean), False)
            .AddAgTextColumn(Dgl1, Col1Dimension2, 100, 0, AgTemplate.ClsMain.FGetDimension2Caption(), CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Dimension2")), Boolean), False)

            .AddAgTextColumn(Dgl1, Col1LotNo, 80, 20, Col1LotNo, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_LotNo")), Boolean), False)
            .AddAgTextColumn(Dgl1, Col1FromProcess, 90, 0, Col1FromProcess, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_ProcessLine")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_ProcessLine")), Boolean), False)
            .AddAgTextColumn(Dgl1, Col1ProdOrder, 100, 0, Col1ProdOrder, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_ProdOrder")), Boolean), False, False)
            .AddAgTextColumn(Dgl1, Col1ProdOrderSr, 90, 0, Col1ProdOrderSr, False, False, False)
            .AddAgNumberColumn(Dgl1, Col1Qty, 70, 8, 4, False, Col1Qty, True, False, True)
            .AddAgTextColumn(Dgl1, Col1Unit, 70, 0, Col1Unit, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Unit")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_Unit")), Boolean))
            .AddAgTextColumn(Dgl1, Col1QtyDecimalPlaces, 50, 0, Col1QtyDecimalPlaces, False, True, False)
            .AddAgNumberColumn(Dgl1, Col1MeasurePerPcs, 70, 8, 4, False, Col1MeasurePerPcs, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_MeasurePerPcs")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_MeasurePerPcs")), Boolean), True)
            .AddAgNumberColumn(Dgl1, Col1TotalMeasure, 80, 8, 4, False, Col1TotalMeasure, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Measure")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_Measure")), Boolean), True)
            .AddAgTextColumn(Dgl1, Col1MeasureUnit, 70, 0, Col1MeasureUnit, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_MeasureUnit")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_MeasureUnit")), Boolean), True)
            .AddAgTextColumn(Dgl1, Col1MeasureDecimalPlaces, 50, 0, Col1MeasureDecimalPlaces, False, True, False)
            .AddAgNumberColumn(Dgl1, Col1Rate, 60, 8, 2, False, Col1Rate, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Rate")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_Rate")), Boolean), True)
            .AddAgNumberColumn(Dgl1, Col1IncentiveRate, 60, 8, 2, False, Col1IncentiveRate, True, False, True)
            .AddAgNumberColumn(Dgl1, Col1Amount, 80, 8, 2, False, Col1Amount, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Amount")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_Amount")), Boolean), True)
            .AddAgTextColumn(Dgl1, Col1ProcessSequence, 100, 0, Col1ProcessSequence, False, True)
            .AddAgTextColumn(Dgl1, Col1ProcessIterationsAllowed, 100, 0, Col1ProcessIterationsAllowed, False, True)
            .AddAgTextColumn(Dgl1, Col1V_Nature, 150, 0, Col1V_Nature, False, False, False)
            .AddAgTextColumn(Dgl1, Col1Remark, 150, 0, Col1Remark, True, False, False)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 40
        Dgl1.AgSkipReadOnlyColumns = True

        LblTotalMeasure.Visible = CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Measure")), Boolean)
        LblTotalMeasureText.Visible = CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Measure")), Boolean)

        AgTemplate.ClsMain.ProcCreateLink(Dgl1, Col1ProdOrder)
        Dgl1.AllowUserToOrderColumns = True

        Dgl3.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl3, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl3, Col3Parameter, 145, 0, Col3Parameter, True, True)
            .AddAgTextColumn(Dgl3, Col3StdValue, 100, 0, Col3StdValue, True, False)
        End With
        AgL.AddAgDataGrid(Dgl3, Pnl3)
        Dgl3.EnableHeadersVisualStyles = False
        Dgl3.ColumnHeadersHeight = 20
        Dgl3.AllowUserToAddRows = False
        Dgl3.AgSkipReadOnlyColumns = True

        Dgl5.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl5, Col5Head, 120, 5, Col5Head, True, True)
            .AddAgNumberColumn(Dgl5, Col5AtRate, 50, 5, 5, False, "@", True, True)
            .AddAgNumberColumn(Dgl5, Col5Amount, 150, 5, 5, False, Col5Amount, True, False)
        End With
        AgL.AddAgDataGrid(Dgl5, Pnl5)
        Dgl5.EnableHeadersVisualStyles = False
        Dgl5.ColumnHeadersHeight = 18
        Dgl5.AgSkipReadOnlyColumns = True

        Dgl5.RowCount = 3
        Dgl5.Item(Col5Head, Row5GrossAmount).Value = "Gross Amount"
        Dgl5.Item(Col5Head, Row5RoundOff).Value = "Round Off"
        Dgl5.Item(Col5Head, Row5NetAmount).Value = "Net Amount"

        Dgl5.ReadOnly = True
        Dgl5.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue
        Dgl5.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        Dgl5.ColumnHeadersDefaultCellStyle.Font = New Font(Dgl5.ColumnHeadersDefaultCellStyle.Font, FontStyle.Bold)
        Dgl5.ColumnHeadersHeight = 25

        Dgl6.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl6, Col6Head, 120, 5, Col6Head, True, True)
            .AddAgNumberColumn(Dgl6, Col6AtRate, 50, 5, 5, False, "@", True, True)
            .AddAgNumberColumn(Dgl6, Col6Amount, 150, 5, 5, False, Col6Amount, True, False)
        End With
        AgL.AddAgDataGrid(Dgl6, Pnl6)
        Dgl6.EnableHeadersVisualStyles = False
        Dgl6.ColumnHeadersHeight = 18
        Dgl6.AgSkipReadOnlyColumns = True

        Dgl6.RowCount = 1
        Dgl6.Item(Col6Head, Row6Freight).Value = "Freight"

        Dgl6.ColumnHeadersVisible = False

        'Dgl6.ColumnHeadersDefaultCellStyle.BackColor = Color.SteelBlue
        'Dgl6.ColumnHeadersDefaultCellStyle.ForeColor = Color.White
        'Dgl6.ColumnHeadersDefaultCellStyle.Font = New Font(Dgl6.ColumnHeadersDefaultCellStyle.Font, FontStyle.Bold)
        'Dgl6.ColumnHeadersHeight = 25

        AgCustomGrid1.Name = "AgCustomGrid1"
        AgCustomGrid1.Ini_Grid(mSearchCode)
        AgCustomGrid1.SplitGrid = False

        AgCL.GridSetiingShowXml(Me.Text & TxtV_Type.Tag & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1, False)
        AgCL.GridSetiingShowXml(Me.Text & AgCustomGrid1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, AgCustomGrid1)

    End Sub

    Private Sub FrmProductionOrder_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As SQLiteConnection, ByVal Cmd As SQLiteCommand) Handles Me.BaseEvent_Save_InTrans
        Dim I As Integer, mSr As Integer
        Dim mSaveParameterQry = ""
        Dim mSaveIsOrderOfUndefinedQty = ""
        Dim bSelectionQry$ = ""

        If AgL.Dman_Execute("Select count(*) from CostCenterMast  Where Code ='" & mSearchCode & "'", AgL.GcnRead).ExecuteScalar = 0 Then
            mQry = "INSERT INTO CostCenterMast(Code,Name,Subcode, Status, Div_Code, Site_Code, U_Name, U_EntDt, U_AE) " &
                    " Values (" & AgL.Chk_Text(mSearchCode) & ", " & AgL.Chk_Text(TxtManualRefNo.Text) & ", " &
                    " " & AgL.Chk_Text(TxtJobWorker.Tag) & ", " &
                    " " & AgL.Chk_Text(AgTemplate.ClsMain.EntryStatus.Active) & ", " &
                    " " & AgL.Chk_Text(AgL.PubDivCode) & ", " &
                    " " & AgL.Chk_Text(AgL.PubSiteCode) & ", " &
                    " " & AgL.Chk_Text(AgL.PubUserName) & ", " &
                    " " & AgL.Chk_Text(AgL.PubLoginDate) & ", 'A') "
        Else
            mQry = "Update CostCenterMast Set " &
                    " Name = " & AgL.Chk_Text(TxtManualRefNo.Text) & ", " &
                    " SubCode = " & AgL.Chk_Text(TxtJobWorker.Tag) & " " &
                    " Where Code = " & AgL.Chk_Text(mSearchCode) & ""
        End If
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)


        If AgL.StrCmp(Topctrl1.Mode, "Add") Then
            If DtJobEnviro.Rows.Count > 0 Then
                mSaveParameterQry = " IsAllowed_MaterialIssue = " & AgL.VNull(DtJobEnviro.Rows(0)("IsAllowed_MaterialIssue")) & ", "
                mSaveIsOrderOfUndefinedQty = " IsOrderOfUndefinedQty = " & AgL.VNull(DtJobEnviro.Rows(0)("IsOrderOfUndefinedQty")) & ", "
            End If
        End If



        mQry = "UPDATE JobOrder " &
                " SET " & mSaveParameterQry & mSaveIsOrderOfUndefinedQty &
                " ManualRefNo = " & AgL.Chk_Text(TxtManualRefNo.Text) & ", " &
                " Process = " & AgL.Chk_Text(TxtProcess.AgSelectedValue) & ", " &
                " JobWorker = " & AgL.Chk_Text(TxtJobWorker.AgSelectedValue) & ", " &
                " Machine = " & AgL.Chk_Text(TxtMachine.AgSelectedValue) & ", " &
                " OrderBy = " & AgL.Chk_Text(TxtOrderBy.AgSelectedValue) & ", " &
                " BillingType = " & AgL.Chk_Text(TxtBillingType.Text) & ", " &
                " DueDate = " & AgL.ConvertDate(TxtDueDate.Text) & ", " &
                " TotalQty = " & Val(LblTotalQty.Text) & ", " &
                " Rate = " & Val(TxtRate.Text) & ", " &
                " TotalAmount = " & Val(LblTotalAmount.Text) & ", " &
                " Freight = " & Val(Dgl6.Item(Col6Amount, Row6Freight).Value) & ", " &
                " RoundOff = " & Val(Dgl5.Item(Col5Amount, Row5RoundOff).Value) & ", " &
                " NetAmount = " & Val(Dgl5.Item(Col5Amount, Row5NetAmount).Value) & ", " &
                " TotalMeasure = " & Val(LblTotalMeasure.Text) & ", " &
                " Remarks = " & AgL.Chk_Text(TxtRemarks.Text) & ", " &
                " CostCenter = " & AgL.Chk_Text(mSearchCode) & ", " &
                " TimeIncentive = " & Val(TxtTimeIncentive.Text) & ", " &
                " TimePenalty = " & Val(TxtTimePenalty.Text) & ", " &
                " TimePenaltyDays = " & Val(TxtTimePenaltyDays.Text) & ", " &
                " TermsAndConditions = " & AgL.Chk_Text(TxtTermsAndConditions.Text) & ", " &
                " InsideOutside = " & AgL.Chk_Text(TxtInsideOutside.Text) & ",  " &
                " JobWithMaterialYN = " & IIf(AgL.StrCmp(TxtWithMaterialYN.Text, "Yes"), 1, 0) & ", " &
                " Godown = " & AgL.Chk_Text(TxtGodown.AgSelectedValue) & ", " &
                " CustomFields = " & AgL.Chk_Text(TxtCustomFields.Tag) & " " &
                " " & AgCustomGrid1.FFooterTableUpdateStr() & " " &
                " Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        'If Topctrl1.Mode <> "Add" Then
        '    mQry = " SELECT Item_UID FROM JobOrderDetail  WHERE DocId = '" & mSearchCode & "' And Item_Uid Is Not Null "
        '    Dim DtItem_Uid As DataTable = AgL.FillData(mQry, AgL.GcnRead).Tables(0)
        '    If DtItem_Uid.Rows.Count > 0 Then
        '        For I = 0 To DtItem_Uid.Rows.Count - 1
        '            AgTemplate.ClsMain.FUpdateItem_UidOnDelete(DtItem_Uid.Rows(I)("Item_Uid"), mSearchCode, Conn, Cmd)
        '        Next
        '    End If
        'End If


        mQry = "Delete From JobOrderDetail Where DocId = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Delete From JobOrderQCInstruction Where DocId = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        'Never Try to Serialise Sr in Line Items 
        'As Some other Entry points may updating values to this Search code and Sr
        With Dgl1
            For I = 0 To .RowCount - 1
                If .Item(Col1Item, I).Value <> "" Then
                    mSr += 1
                    If bSelectionQry <> "" Then bSelectionQry += " UNION ALL "
                    bSelectionQry += " Select " & AgL.Chk_Text(mSearchCode) & ", 	" &
                            " " & mSr & ", " & AgL.Chk_Text(.Item(Col1Item_Uid, I).Tag) & ", " &
                            " " & AgL.Chk_Text(.Item(Col1Item, I).Tag) & ", " &
                            " " & AgL.Chk_Text(.Item(Col1Dimension1, I).Tag) & ", " &
                            " " & AgL.Chk_Text(.Item(Col1Dimension2, I).Tag) & ", " &
                            " " & AgL.Chk_Text(.Item(Col1LotNo, I).Value) & "," &
                            " " & AgL.Chk_Text(.Item(Col1FromProcess, I).Tag) & ", " &
                            " " & AgL.Chk_Text(.Item(Col1ProdOrder, I).Tag) & ", " &
                            " " & AgL.Chk_Text(.Item(Col1ProdOrderSr, I).Value) & ", " &
                            " " & Val(.Item(Col1Qty, I).Value) & ", " & AgL.Chk_Text(.Item(Col1Unit, I).Value) & ",	" &
                            " " & Val(.Item(Col1MeasurePerPcs, I).Value) & ", " & Val(.Item(Col1TotalMeasure, I).Value) & ", " &
                            " " & AgL.Chk_Text(.Item(Col1MeasureUnit, I).Value) & ", " &
                            " " & Val(.Item(Col1Rate, I).Value) & ",	" &
                            " " & Val(.Item(Col1IncentiveRate, I).Value) & ",	" &
                            " " & Val(.Item(Col1Amount, I).Value) & ", " &
                            " " & AgL.Chk_Text(.Item(Col1V_Nature, I).Value) & ", " &
                            " " & AgL.Chk_Text(.Item(Col1Remark, I).Value) & ", " &
                            " " & AgL.Chk_Text(mSearchCode) & ", " &
                            " " & mSr & " "

                End If
            Next
        End With





        If bSelectionQry <> "" Then
            mQry = "  INSERT INTO JobOrderDetail(DocId, Sr, " &
                    " Item_Uid, Item, Dimension1, Dimension2, LotNo, FromProcess, ProdOrder, ProdOrderSr, Qty, Unit, MeasurePerPcs, TotalMeasure, " &
                    " MeasureUnit, Rate, IncentiveRate, Amount, V_Nature, Remark, JobOrder, JobOrderSr) " & bSelectionQry
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If

        mSr = 0
        bSelectionQry = ""
        With Dgl3
            For I = 0 To .RowCount - 1
                If .Item(Col3Parameter, I).Value <> "" Then
                    mSr += 1
                    If bSelectionQry <> "" Then bSelectionQry += " UNION ALL "
                    bSelectionQry += " Select '" & mSearchCode & "', " & Val(mSr) & ", " &
                            " " & AgL.Chk_Text(.Item(Col3Parameter, I).Value) & ", " &
                            " " & AgL.Chk_Text(.Item(Col3StdValue, I).Value) & " "
                End If
            Next
        End With

        If bSelectionQry <> "" Then
            mQry = " INSERT INTO JobOrderQCInstruction(DocId, " &
                    " Sr, Parameter, StdValue) " & bSelectionQry
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If





        FPostInStockProcess(mSearchCode, Conn, Cmd)
        FPostInJobIssRecUID(mSearchCode, Conn, Cmd)
        FPostFreight(mSearchCode, Conn, Cmd)

        'For I = 0 To Dgl1.Rows.Count - 1
        '    If Dgl1.Item(Col1Item_Uid, I).Tag <> "" Then
        '        AgTemplate.ClsMain.FUpdateItem_Uid(Dgl1.Item(Col1Item_Uid, I).Tag, Topctrl1.Mode, mSearchCode, TxtV_Type.Tag, TxtV_Date.Text, TxtJobWorker.Tag, "", TxtProcess.Tag, AgTemplate.ClsMain.Item_UidStatus.Issue, TxtManualRefNo.Text, Conn, Cmd)
        '    End If
        'Next


        If ImportMode = True Then
            mQry = " UPDATE JobOrder Set EntryStatus = '" & AgTemplate.ClsMain.LogStatus.LogImport & "' Where DocId = '" & mSearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If

        If AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Or AgL.StrCmp(AgL.PubUserName, "Sa") Then
            AgCL.GridSetiingWriteXml(Me.Text & TxtV_Type.Tag & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1)
            AgCL.GridSetiingWriteXml(Me.Text & AgCustomGrid1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, AgCustomGrid1)
        End If

        mLastOrderBy = TxtOrderBy.AgSelectedValue

        If AgL.VNull(AgL.Dman_Execute(" Select IFNull(IsAllowed_MaterialIssue,0) From JobOrder  Where DocId = '" & mSearchCode & "'", AgL.GcnRead).ExecuteScalar) <> 0 Then
            FPostMaterialIssue(Conn, Cmd)
        End If
    End Sub

    Private Sub FrmProductionOrder_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim I As Integer
        Dim DrTemp As DataRow() = Nothing
        Dim DsTemp As DataSet
        Dim DtItem As DataTable = Nothing

        mQry = "Select P.*, Sg.Name + ',' + IFNull(C.CityName,'') As JobWorkerName, SGM.Name As MachineName, Sg1.DispName As OrderByName, " &
                " G.Description As GodownDesc, Pr.Description As ProcessDesc " &
                " From JobOrder P  " &
                " LEFT JOIN SubGroup Sg  On P.JobWorker = Sg.SubCode " &
                " LEFT JOIN SubGroup SGM  On P.Machine = SGM.SubCode " &
                " LEFT JOIN SubGroup Sg1  On P.OrderBy = Sg1.SubCode " &
                " LEFT JOIN Godown G  On P.Godown = G.Code " &
                " Left Join City C On Sg.CityCode = C.CityCode " &
                " LEFT JOIN Process Pr  On P.Process = Pr.NCat " &
                " Where P.DocID = '" & SearchCode & "'"
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then

                TxtCustomFields.Tag = AgCustomFields.ClsMain.FGetCustomFieldFromV_Type(TxtV_Type.Tag, AgL.GcnRead)
                If AgL.XNull(.Rows(0)("CustomFields")) <> "" Then
                    TxtCustomFields.Tag = AgL.XNull(.Rows(0)("CustomFields"))
                End If
                AgCustomGrid1.FrmType = Me.FrmType
                AgCustomGrid1.AgCustom = TxtCustomFields.Tag

                IniGrid()
                TxtManualRefNo.Text = AgL.XNull(.Rows(0)("ManualRefNo"))

                TxtJobWorker.Tag = AgL.XNull(.Rows(0)("JobWorker"))
                TxtJobWorker.Text = AgL.XNull(.Rows(0)("JobWorkerName"))
                TxtMachine.Tag = AgL.XNull(.Rows(0)("Machine"))
                TxtMachine.Text = AgL.XNull(.Rows(0)("MachineName"))
                TxtOrderBy.Tag = AgL.XNull(.Rows(0)("OrderBy"))
                TxtOrderBy.Text = AgL.XNull(.Rows(0)("OrderByName"))

                TxtProcess.Tag = AgL.XNull(.Rows(0)("Process"))
                TxtProcess.Text = AgL.XNull(.Rows(0)("ProcessDesc"))

                TxtDueDate.Text = AgL.XNull(.Rows(0)("DueDate"))
                TxtRemarks.Text = AgL.XNull(.Rows(0)("Remarks"))
                TxtTermsAndConditions.Text = AgL.XNull(.Rows(0)("TermsAndConditions"))
                TxtBillingType.Text = AgL.XNull(.Rows(0)("BillingType"))
                TxtInsideOutside.Text = AgL.XNull(.Rows(0)("InsideOutside"))
                LblTotalQty.Text = AgL.VNull(.Rows(0)("TotalQty"))
                TxtRate.Text = AgL.VNull(.Rows(0)("Rate"))
                LblTotalAmount.Text = AgL.VNull(.Rows(0)("TotalAmount"))
                LblTotalMeasure.Text = AgL.VNull(.Rows(0)("TotalMeasure"))

                TxtTimeIncentive.Text = AgL.VNull(.Rows(0)("TimeIncentive"))
                TxtTimePenalty.Text = AgL.VNull(.Rows(0)("TimePenalty"))
                TxtTimePenaltyDays.Text = AgL.VNull(.Rows(0)("TimePenaltyDays"))

                TxtGodown.Tag = AgL.XNull(.Rows(0)("Godown"))
                TxtGodown.Text = AgL.XNull(AgL.Dman_Execute(" SELECT Description FROM Godown WHERE Code =  '" & AgL.XNull(.Rows(0)("Godown")) & "' ", AgL.GCn).ExecuteScalar)

                TxtWithMaterialYN.Text = IIf(AgL.VNull(.Rows(0)("JobWithMaterialYN")) = 0, "No", "Yes")

                If AgL.VNull(.Rows(I)("IsAllowed_MaterialIssue")) = 0 Then
                    BtnMaterialIssueDetail.Visible = False
                Else
                    BtnMaterialIssueDetail.Visible = True
                End If


                AgCustomGrid1.FMoveRecFooterTable(DsTemp.Tables(0))

                Dgl5.Item(Col5Amount, Row5GrossAmount).Value = AgL.VNull(.Rows(0)("TotalAmount"))
                Dgl5.Item(Col5Amount, Row5RoundOff).Value = AgL.VNull(.Rows(0)("RoundOff"))
                Dgl5.Item(Col5Amount, Row5NetAmount).Value = AgL.VNull(.Rows(0)("NetAmount"))

                Dgl6.Item(Col6Amount, Row6Freight).Value = AgL.VNull(.Rows(0)("Freight"))

                ChkShowOnlyImportedRecords.Visible = True
                If AgL.StrCmp(AgL.XNull(.Rows(0)("EntryStatus")), AgTemplate.ClsMain.LogStatus.LogImport) Then
                    BtnImprtFromText.Text = ImportAction_ClearImport
                Else
                    BtnImprtFromText.Text = ImportAction_NewImport
                End If

                BtnMaterialIssueDetail.Tag = Nothing

                isRecordLocked = False
                '-------------------------------------------------------------
                'Line Records are showing in First Grid
                '-------------------------------------------------------------
                Dim strQryJobReceived$ = "SELECT L.JobOrder, L.JobOrderSr, Sum(L.Qty) AS Qty " &
                                         "FROM JobReceiveDetail L  " &
                                         "Where L.JobOrder = '" & SearchCode & "' " &
                                         "GROUP BY L.JobOrder, L.JobOrderSr  "

                Dim strQryJobAmended$ = "SELECT L.JobOrder, L.JobOrderSr, Sum(L.Qty) AS Qty " &
                                        "FROM JobOrderDetail L  " &
                                        "Where L.JobOrder = '" & SearchCode & "' And L.JobOrder <> L.DocID  " &
                                        "GROUP BY L.JobOrder, L.JobOrderSr  "


                mQry = "Select L.*, IU.Item_UID as Item_UID_Desc, I.Description As ItemDesc, " &
                        " U.DecimalPlaces as QtyDecimalPlaces, MU.DecimalPlaces as MeasureDecimalPlaces, " &
                        " P.Description As FromProcessDesc, Po.ManualRefNo As ProdOrderNo, " &
                        " I.ItemGroup, IG.Description AS ItemGroupDesc, " &
                        " I.ItemCategory, Ic.Description AS ItemCategoryDesc, " &
                        " D1.Description As Dimension1Desc, D2.Description As Dimension2Desc, " &
                        " (Case When IFNull(JobRec.Qty,0) > 0 Or IFNull(JobAmd.Qty,0) > 0 Then 1 Else 0 End) as RowLocked " &
                        " From JobOrderDetail L   " &
                        " LEFT JOIN Item I   On L.Item = I.Code " &
                        " LEFT JOIN ItemGroup IG On Ig.Code = I.ItemGroup " &
                        " LEFT JOIN ItemCategory Ic On Ic.Code = I.ItemCategory " &
                        " LEFT JOIN Item_UID IU   On L.Item_UID = IU.Code " &
                        " LEFT JOIN Process P   On L.FromProcess = P.NCat " &
                        " LEFT JOIN ProdOrder Po   On L.ProdOrder = Po.DocId " &
                        " Left Join Unit U   On L.Unit = U.Code " &
                        " Left Join Unit MU   On L.MeasureUnit = MU.Code " &
                        " Left Join Dimension1 D1   On L.Dimension1 = D1.Code " &
                        " Left Join Dimension2 D2   On L.Dimension2 = D2.Code " &
                        " Left Join (" & strQryJobReceived & ") as JobRec On L.DocID + Convert(VarChar,L.Sr) = JobRec.JobOrder + Convert(VarChar,JobRec.JobOrderSr) " &
                        " Left Join (" & strQryJobAmended & ") as JobAmd On L.DocID + Convert(VarChar,L.Sr) = JobAmd.JobOrder + Convert(VarChar,JobAmd.JobOrderSr) " &
                        " Where L.DocId = '" & SearchCode & "' Order By Sr"

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
                            'Dgl1.Item(Col1Item_Uid, I).Value = AgL.XNull(AgL.Dman_Execute("Select Item_Uid From Item_Uid Where Code = '" & AgL.XNull(.Rows(I)("Item_Uid")) & "' ", AgL.GCn).ExecuteScalar)

                            Dgl1.Item(Col1Item, I).Tag = AgL.XNull(.Rows(I)("Item"))
                            Dgl1.Item(Col1Item, I).Value = AgL.XNull(.Rows(I)("ItemDesc"))

                            Dgl1.Item(Col1Dimension1, I).Tag = AgL.XNull(.Rows(I)("Dimension1"))
                            Dgl1.Item(Col1Dimension1, I).Value = AgL.XNull(.Rows(I)("Dimension1Desc"))
                            Dgl1.Item(Col1Dimension2, I).Tag = AgL.XNull(.Rows(I)("Dimension2"))
                            Dgl1.Item(Col1Dimension2, I).Value = AgL.XNull(.Rows(I)("Dimension2Desc"))


                            Dgl1.Item(Col1ItemGroup, I).Tag = AgL.XNull(.Rows(I)("ItemGroup"))
                            Dgl1.Item(Col1ItemGroup, I).Value = AgL.XNull(.Rows(I)("ItemGroupDesc"))

                            Dgl1.Item(Col1ItemCategory, I).Tag = AgL.XNull(.Rows(I)("ItemCategory"))
                            Dgl1.Item(Col1ItemCategory, I).Value = AgL.XNull(.Rows(I)("ItemCategoryDesc"))


                            Dgl1.Item(Col1LotNo, I).Value = AgL.XNull(.Rows(I)("LotNo"))

                            Dgl1.Item(Col1FromProcess, I).Tag = AgL.XNull(.Rows(I)("FromProcess"))
                            Dgl1.Item(Col1FromProcess, I).Value = AgL.XNull(.Rows(I)("FromProcessDesc"))

                            Dgl1.Item(Col1ProdOrder, I).Tag = AgL.XNull(.Rows(I)("ProdOrder"))
                            Dgl1.Item(Col1ProdOrder, I).Value = AgL.XNull(.Rows(I)("ProdOrderNo"))
                            Dgl1.Item(Col1ProdOrderSr, I).Value = AgL.XNull(.Rows(I)("ProdOrderSr"))

                            mQry = "Select ProcessSequence, (Select Count(*) from ProcessSequenceDetail  Where Code = H.ProcessSequence And Process = '" & TxtProcess.Tag & "') as IterationsAllowed from Item H  Where Code = '" & Dgl1.Item(Col1Item, I).Tag & "' "
                            DtItem = AgL.FillData(mQry, AgL.GcnRead).Tables(0)

                            If DtItem.Rows.Count > 0 Then
                                Dgl1.Item(Col1ProcessSequence, I).Value = AgL.XNull(DtItem.Rows(0)("ProcessSequence"))
                                Dgl1.Item(Col1ProcessIterationsAllowed, I).Value = AgL.VNull(DtItem.Rows(0)("IterationsAllowed"))
                            End If

                            Dgl1.Item(Col1Qty, I).Value = Format(AgL.VNull(.Rows(I)("Qty")), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))

                            Dgl1.Item(Col1QtyDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("QtyDecimalPlaces"))

                            Dgl1.Item(Col1MeasurePerPcs, I).Value = Format(AgL.VNull(.Rows(I)("MeasurePerPcs")), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1TotalMeasure, I).Value = Format(AgL.VNull(.Rows(I)("TotalMeasure")), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1MeasureUnit, I).Value = AgL.XNull(.Rows(I)("MeasureUnit"))

                            Dgl1.Item(Col1MeasureDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("MeasureDecimalPlaces"))

                            Dgl1.Item(Col1Rate, I).Value = AgL.VNull(.Rows(I)("Rate"))
                            Dgl1.Item(Col1IncentiveRate, I).Value = AgL.VNull(.Rows(I)("IncentiveRate"))
                            Dgl1.Item(Col1Amount, I).Value = AgL.VNull(.Rows(I)("Amount"))

                            Dgl1.Item(Col1V_Nature, I).Value = AgL.XNull(.Rows(I)("V_Nature"))
                            Dgl1.Item(Col1Remark, I).Value = AgL.XNull(.Rows(I)("Remark"))

                            If .Rows(I)("RowLocked") > 0 Then
                                Dgl1.Rows(I).DefaultCellStyle.BackColor = AgTemplate.ClsMain.Colours.GridRow_Locked : Dgl1.Rows(I).ReadOnly = True
                                If isRecordLocked = False Then isRecordLocked = True
                            End If


                        Next I
                    End If
                End With

                '-------------------------------------------------------------
                'Line Records are showing in First Grid
                '-------------------------------------------------------------

                mQry = "Select * from JobOrderQCInstruction   Where DocId = '" & SearchCode & "' Order By Sr"
                DsTemp = AgL.FillData(mQry, AgL.GCn)
                With DsTemp.Tables(0)
                    Dgl3.RowCount = 1
                    Dgl3.Rows.Clear()
                    If .Rows.Count > 0 Then
                        For I = 0 To .Rows.Count - 1
                            Dgl3.Rows.Add()
                            Dgl3.Item(ColSNo, I).Value = Dgl3.Rows.Count
                            Dgl3.Item(Col3Parameter, I).Value = AgL.XNull(.Rows(I)("Parameter"))
                            Dgl3.Item(Col3StdValue, I).Value = AgL.XNull(.Rows(I)("StdValue"))
                        Next I
                    End If
                End With

                'Calculation()
                '-------------------------------------------------------------
            End If
        End With

        If AgCustomGrid1.Rows.Count = 0 Then AgCustomGrid1.Visible = False

    End Sub

    Private Sub FrmProductionOrder_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Topctrl1.ChangeAgGridState(Dgl1, False)
        Topctrl1.ChangeAgGridState(Dgl3, False)
        'If AgL.VNull(AgL.PubDtEnviro.Rows(0)("PrintToPrinter")) <> 0 Then RbtAllItems.Checked = True Else RbtForStock.Checked = True
        AgCustomGrid1.FrmType = Me.FrmType
        RbtAllItems.Checked = True
        AgL.WinSetting(Me, 660, 992, 0, 0)
    End Sub

    Private Sub DGL1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.KeyDown
        If e.Control And e.KeyCode = Keys.D Then
            sender.CurrentRow.Selected = True
        End If
        If e.Control Or e.Shift Or e.Alt Then Exit Sub
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

                Case Col1Dimension1, Col1Dimension2
                    If Dgl1.Item(Col1ProdOrder, Dgl1.CurrentCell.RowIndex).Value <> "" Then
                        Dgl1.Columns(Col1Dimension1).ReadOnly = True
                        Dgl1.Columns(Col1Dimension2).ReadOnly = True
                    Else
                        Dgl1.Columns(Col1Dimension1).ReadOnly = False
                        Dgl1.Columns(Col1Dimension2).ReadOnly = False
                    End If

                Case Col1LotNo
                    Dgl1.AgHelpDataSet(Col1LotNo) = Nothing

                Case Col1FromProcess
                    Dgl1.AgHelpDataSet(Col1FromProcess) = Nothing
                    If Dgl1.Item(Col1Item_Uid, Dgl1.CurrentCell.RowIndex).Value <> "" And Dgl1.Item(Col1FromProcess, Dgl1.CurrentCell.RowIndex).Value <> "" Then
                        Dgl1.Columns(Col1FromProcess).ReadOnly = True
                    Else
                        Dgl1.Columns(Col1FromProcess).ReadOnly = False
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

    Private Sub DGL1_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles Dgl1.RowsAdded
        sender(ColSNo, e.RowIndex).Value = e.RowIndex + 1
    End Sub

    Private Sub FrmProductionOrder_BaseFunction_Calculation() Handles Me.BaseFunction_Calculation
        Dim I As Integer

        LblTotalQty.Text = 0 : LblTotalMeasure.Text = 0 : LblTotalAmount.Text = 0

        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1Item, I).Value <> "" Then
                Dgl1.Item(Col1TotalMeasure, I).Value = Format(Val(Dgl1.Item(Col1Qty, I).Value) * Val(Dgl1.Item(Col1MeasurePerPcs, I).Value), "0.".PadRight(CType(Dgl1.Columns(Col1TotalMeasure), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))

                If AgL.StrCmp(TxtBillingType.Text, "Qty") Or TxtBillingType.Text = "" Then
                    Dgl1.Item(Col1Amount, I).Value = Format(Val(Dgl1.Item(Col1Qty, I).Value) * Val(Dgl1.Item(Col1Rate, I).Value), "0.".PadRight(CType(Dgl1.Columns(Col1Amount), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                ElseIf AgL.StrCmp(TxtBillingType.Text, "Measure") Then
                    Dgl1.Item(Col1Amount, I).Value = Format(Val(Dgl1.Item(Col1TotalMeasure, I).Value) * Val(Dgl1.Item(Col1Rate, I).Value), "0.".PadRight(CType(Dgl1.Columns(Col1Amount), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                End If

                'Footer Calculation
                LblTotalQty.Text = Val(LblTotalQty.Text) + Val(Dgl1.Item(Col1Qty, I).Value)
                LblTotalAmount.Text = Val(LblTotalAmount.Text) + Val(Dgl1.Item(Col1Amount, I).Value)
                LblTotalMeasure.Text = Val(LblTotalMeasure.Text) + Val(Dgl1.Item(Col1TotalMeasure, I).Value)
            End If
        Next
        Dgl5.Item(Col5Amount, Row5GrossAmount).Value = LblTotalAmount.Text
        Dgl5.Item(Col5Amount, Row5RoundOff).Value = Math.Round(Val(Dgl5.Item(Col5Amount, Row5GrossAmount).Value) - Math.Round(Val(Dgl5.Item(Col5Amount, Row5GrossAmount).Value)), 2)
        Dgl5.Item(Col5Amount, Row5NetAmount).Value = Math.Round(Val(Dgl5.Item(Col5Amount, Row5GrossAmount).Value))
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        Dim I As Integer = 0
        Dim DtTemp As DataTable = Nothing
        Dim DtTemp1 As DataTable = Nothing
        Dim mCurrStock As Double
        Dim StrMessage As String = ""
        Dim mSelectionQry$ = ""
        Dim bPlanQty As Double : Dim bOrderQty As Double

        passed = FCheckDuplicateRefNo()

        If AgL.RequiredField(TxtJobWorker, LblJobWorker.Text) Then passed = False : Exit Sub
        If AgL.RequiredField(TxtDueDate, LblDueDate.Text) Then passed = False : Exit Sub
        If AgCL.AgIsBlankGrid(Dgl1, Dgl1.Columns(Col1Item).Index) Then passed = False : Exit Sub
        If AgCL.AgIsDuplicate(Dgl1, "" + Dgl1.Columns(Col1Item).Index.ToString + "," + Dgl1.Columns(Col1Item_Uid).Index.ToString + "," + Dgl1.Columns(Col1ProdOrder).Index.ToString + "," + Dgl1.Columns(Col1LotNo).Index.ToString + "," + Dgl1.Columns(Col1Dimension1).Index.ToString + "," + Dgl1.Columns(Col1Dimension2).Index.ToString + "") Then passed = False : Exit Sub

        If CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsPostedInStock")), Boolean) = True Then
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
                    End If
                Next
            End With
        End If

        Dim mTampQry = "  Declare @TmpTable as Table " &
                  " ( " &
                  " Item nVarchar(100), " &
                  " Process nVarchar(100), " &
                  " Qty Float " &
                  " )"

        With Dgl1
            For I = 0 To .Rows.Count - 1
                If .Item(Col1Item, I).Value <> "" Then
                    StrMessage = ""

                    If CType(AgL.VNull(DtJobEnviro.Rows(0)("IsOrderOfUndefinedQty")), Boolean) = False Then
                        If Val(.Item(Col1Qty, I).Value) = 0 Then
                            If StrMessage <> "" Then StrMessage += vbCrLf
                            StrMessage += "Qty Is 0 At Row No " & Dgl1.Item(ColSNo, I).Value & ""
                        End If
                    End If

                    If StrMessage <> "" Then
                        MsgBox(StrMessage)
                        passed = False : Exit Sub
                    End If

                    If CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_ProcessLine")), Boolean) Then
                        If Dgl1.Item(Col1FromProcess, I).Value = "" And Dgl1.Item(Col1Item_Uid, I).Value = "" Then
                            MsgBox(" Process Is Required At Line No " & Dgl1.Item(ColSNo, I).Value & "")
                            Dgl1.CurrentCell = Dgl1.Item(Col1FromProcess, I) : Dgl1.Focus()
                            passed = False : Exit Sub
                        End If
                    End If



                    'If CType(IFNull(DtV_TypeSettings.Rows(0)("IsMandatory_Rate"), "1"), Boolean) Then
                    '    If Val(.Item(Col1Rate, I).Value) = 0 Then
                    '        If StrMessage <> "" Then StrMessage += vbCrLf
                    '        StrMessage += "Rate Is 0 At Row No " & Dgl1.Item(ColSNo, I).Value & ""
                    '    End If
                    'End If

                    If StrMessage <> "" Then
                        MsgBox(StrMessage)
                        passed = False : Exit Sub
                    End If

                    StrMessage = ""
                    If AgL.PubDtEnviro.Rows(0)("IsNegetiveStockAllowed") Then
                        mCurrStock = AgTemplate.ClsMain.FunRetStock(.AgSelectedValue(Col1Item, I), mSearchCode, , TxtGodown.AgSelectedValue, , AgTemplate.ClsMain.StockStatus.Standard, TxtV_Date.Text)
                        If mCurrStock < Val(.Item(Col1Qty, I).Value) Then
                            If StrMessage <> "" Then StrMessage += vbCrLf
                            StrMessage += "Qty of " & .Item(Col1Item, I).Value & " In " & TxtGodown.Text & " is less than " & Dgl1.Item(Col1Qty, I).Value & vbCrLf & " Current Stock Is : " & mCurrStock & "."
                        End If
                    End If

                    If StrMessage <> "" Then
                        If MsgBox(StrMessage & vbCrLf & "Do you want to continue?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                            passed = False : Exit Sub
                        End If
                    End If

                    If Dgl1.Item(Col1FromProcess, I).Value <> "" Then
                        mTampQry += "Insert Into @TmpTable (Item, Process, Qty) " &
                                   " Values (" & AgL.Chk_Text(Dgl1.Item(Col1Item, I).Tag) & ", " &
                                   " " & AgL.Chk_Text(Dgl1.Item(Col1FromProcess, I).Tag) & ", " &
                                   " " & Val(Dgl1.Item(Col1Qty, I).Value) & ")"
                    End If

                    If CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsPostedInStock")), Boolean) Then

                        'If mSelectionQry <> "" Then mSelectionQry += " UNION ALL "
                        'mSelectionQry += "Select " & AgL.Chk_Text(Dgl1.Item(Col1Item, I).Tag) & ", " & AgL.Chk_Text(Dgl1.Item(Col1LotNo, I).Value) & ", " & Val(Dgl1.Item(Col1Qty, I).Value) & " "
                        If mSelectionQry <> "" Then mSelectionQry += " UNION ALL "
                        mSelectionQry += "Select " & AgL.Chk_Text(Dgl1.Item(Col1Item, I).Tag) & ", " &
                                " " & AgL.Chk_Text(Dgl1.Item(Col1LotNo, I).Value) & ", " &
                                " " & AgL.Chk_Text(Dgl1.Item(Col1Dimension1, I).Tag) & ", " &
                                " " & AgL.Chk_Text(Dgl1.Item(Col1Dimension2, I).Tag) & ", " &
                                " " & AgL.Chk_Text(Dgl1.Item(Col1FromProcess, I).Tag) & ", " &
                                " " & Val(Dgl1.Item(Col1Qty, I).Value) & " "

                    End If
                End If
            Next
        End With


        If AgL.VNull(AgL.Dman_Execute("Select IFNull(RestrictNegetiveStock,0) From Godown Where Code = '" & TxtGodown.Tag & "'", AgL.GcnRead).ExecuteScalar) <> 0 Then
            mTampQry += " Select L.Item, L.Process, Sum(L.Qty) As Qty, Max(I.Description) As ItemDesc " &
                        " From @TmpTable L " &
                        " LEFT JOIN Item I On L.Item = I.Code " &
                        " Group By Item, Process "
            DtTemp = AgL.FillData(mTampQry, AgL.GCn).tables(0)

            If DtTemp.Rows.Count > 0 Then
                For I = 0 To DtTemp.Rows.Count - 1
                    mQry = " Select IFNull(Sum(Qty_Rec),0) - IFNull(Sum(Qty_Iss),0) As Qty From Stock Where Item = '" & DtTemp.Rows(I)("Item") & "' And Process = '" & DtTemp.Rows(I)("Process") & "' And DocId <> '" & mSearchCode & "'"
                    If Math.Round(AgL.VNull(DtTemp.Rows(I)("Qty")), 4) > Math.Round(AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar), 4) Then
                        MsgBox("Current Stock Of Item " & DtTemp.Rows(I)("ItemDesc") & " In Process " & DtTemp.Rows(I)("Process") & " Is Less Then " & AgL.VNull(DtTemp.Rows(I)("Qty")) & "", MsgBoxStyle.Information)
                        passed = False : Exit Sub
                    End If
                Next
            End If
        End If


        'Start Stock Validation Start For Material Issued With Job Order
        Dim mTamp1Qry = "  Declare @Tmp1Table as Table " &
          " ( " &
          " Item nVarchar(100), " &
          " LotNo nVarchar(100), " &
          " Dimension1 nVarchar(100), " &
          " Dimension2 nVarchar(100), " &
          " Qty Float " &
          " )"

        If BtnMaterialIssueDetail.Tag IsNot Nothing Then
            Dim FrmObj As FrmJobOrderMaterialIssue = BtnMaterialIssueDetail.Tag
            With FrmObj
                For I = 0 To .Dgl1.Rows.Count - 1
                    If .Dgl1.Item(FrmJobOrderMaterialIssue.Col1Item, I).Value <> "" Then

                        'If mSelectionQry <> "" Then mSelectionQry += " UNION ALL "
                        'mSelectionQry += "SELECT " & AgL.Chk_Text(.Dgl1.Item(FrmJobOrderMaterialIssue.Col1Item, I).Tag) & " As Item, " & _
                        '        " " & AgL.Chk_Text(.Dgl1.Item(FrmJobOrderMaterialIssue.Col1LotNo, I).Value) & ", " & _
                        '        " " & Val(.Dgl1.Item(FrmJobOrderMaterialIssue.Col1Qty, I).Value) & " "

                        If mSelectionQry <> "" Then mSelectionQry += " UNION ALL "
                        mSelectionQry += "Select " & AgL.Chk_Text(.Dgl1.Item(FrmJobOrderMaterialIssue.Col1Item, I).Tag) & " As Item, " &
                                " " & AgL.Chk_Text(.Dgl1.Item(FrmJobOrderMaterialIssue.Col1LotNo, I).Value) & ", " &
                                " " & AgL.Chk_Text(.Dgl1.Item(FrmJobOrderMaterialIssue.Col1Dimension1, I).Tag) & ", " &
                                " " & AgL.Chk_Text(.Dgl1.Item(FrmJobOrderMaterialIssue.Col1Dimension2, I).Tag) & ", " &
                                " " & AgL.Chk_Text(.Dgl1.Item(FrmJobOrderMaterialIssue.Col1FromProcess, I).Tag) & ", " &
                                " " & Val(.Dgl1.Item(FrmJobOrderMaterialIssue.Col1Qty, I).Value) & " "

                        mTamp1Qry += "Insert Into @Tmp1Table (Item, LotNo, Dimension1, Dimension2, Qty) " &
                                   " Values ( " & AgL.Chk_Text(.Dgl1.Item(FrmJobOrderMaterialIssue.Col1Item, I).Tag) & ", " &
                                   " " & AgL.Chk_Text(.Dgl1.Item(FrmJobOrderMaterialIssue.Col1LotNo, I).Value) & ", " &
                                   " " & AgL.Chk_Text(.Dgl1.Item(FrmJobOrderMaterialIssue.Col1Dimension1, I).Tag) & ", " &
                                   " " & AgL.Chk_Text(.Dgl1.Item(FrmJobOrderMaterialIssue.Col1Dimension2, I).Tag) & ", " &
                                   " " & Val(.Dgl1.Item(FrmJobOrderMaterialIssue.Col1Qty, I).Value) & " )"

                    End If
                Next
            End With
        End If

        If mSelectionQry <> "" Then
            'Selection Qry Contains Loop Genearted Selecion Qry String For Item And Its Quantity
            'For Example Select " & AgL.Chk_Text(Dgl1.Item(Col1Item, I).Tag) & ", " & Val(Dgl1.Item(Col1Qty, I).Value) & " 
            passed = AgTemplate.ClsMain.FIsNegativeStock(mSelectionQry, mSearchCode, TxtGodown.Tag, TxtV_Date.Text)
        End If


        If AgL.VNull(AgL.Dman_Execute("Select IFNull(RestrictNegetiveStock,0) From Godown Where Code = '" & TxtGodown.Tag & "'", AgL.GcnRead).ExecuteScalar) <> 0 Then
            mTamp1Qry += " Select L.Item, L.LotNo, L.Dimension1, L.Dimension2, Sum(L.Qty) As Qty, Max(I.Description) As ItemDesc, Max(D1.Description) As D1Desc, Max(D2.Description) As D2Desc  " &
                        " From @Tmp1Table L " &
                        " LEFT JOIN Item I On L.Item = I.Code " &
                        " LEFT JOIN Dimension1 D1 On L.Dimension1 = D1.Code " &
                        " LEFT JOIN Dimension2 D2 On L.Dimension2 = D2.Code " &
                        " Group By L.Item, L.LotNo, L.Dimension1, L.Dimension2 "
            DtTemp1 = AgL.FillData(mTamp1Qry, AgL.GCn).tables(0)
            Dim StrMsg As String = ""

            If DtTemp1.Rows.Count > 0 Then
                For I = 0 To DtTemp1.Rows.Count - 1
                    mQry = " Select IFNull(Sum(Qty_Rec),0) - IFNull(Sum(Qty_Iss),0) As Qty From Stock Where Item = '" & DtTemp1.Rows(I)("Item") & "' And IFNull(LotNo,'') = '" & DtTemp1.Rows(I)("LotNo") & "' And IFNull(Dimension1,'') = '" & DtTemp1.Rows(I)("Dimension1") & "' And IFNull(Dimension2,'') = '" & DtTemp1.Rows(I)("Dimension2") & "' And DocId <> '" & mSearchCode & "'"
                    If Math.Round(AgL.VNull(DtTemp1.Rows(I)("Qty")), 4) > Math.Round(AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar), 4) Then
                        StrMsg = "Current Stock Of Item " & DtTemp1.Rows(I)("ItemDesc") & ""
                        'If AgL.XNull(DtTemp1.Rows(I)("Process")) <> "" Then StrMsg = StrMsg & " In Process " & DtTemp1.Rows(I)("Process") & ""
                        If AgL.XNull(DtTemp1.Rows(I)("LotNo")) <> "" Then StrMsg = StrMsg & "  For Lot No " & DtTemp1.Rows(I)("LotNo") & ""
                        If AgL.XNull(DtTemp1.Rows(I)("D1Desc")) <> "" Then StrMsg = StrMsg & "  For " & AgTemplate.ClsMain.FGetDimension1Caption() & "  " & DtTemp1.Rows(I)("D1Desc") & ""
                        If AgL.XNull(DtTemp1.Rows(I)("D2Desc")) <> "" Then StrMsg = StrMsg & "  For " & AgTemplate.ClsMain.FGetDimension2Caption() & "  " & DtTemp1.Rows(I)("D2Desc") & ""
                        StrMsg = StrMsg & " Is Less Then " & AgL.VNull(DtTemp1.Rows(I)("Qty")) & ""
                        MsgBox(StrMsg, MsgBoxStyle.Information)
                        passed = False : Exit Sub
                    End If
                Next
            End If
        End If

        'End Stock Validation End For Material Issued With Job Order



        If StrMessage <> "" Then
            MsgBox(StrMessage)
            passed = False : Exit Sub
        End If

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
            passed = AgTemplate.ClsMain.FIsNegativeStock(mSelectionQry, mSearchCode, TxtGodown.Tag, TxtV_Date.Text)
        End If


        '--------------- Data Validation for Order Qty should not greater than Plan Qty from Production Order
        With Dgl1
            For I = 0 To .Rows.Count - 1
                If .Item(Col1Item, I).Value <> "" And .AgSelectedValue(Col1ProdOrder, I) <> "" Then
                    bPlanQty = 0
                    bOrderQty = 0
                    mQry = " SELECT Sum(L.Qty) AS PlanQty " &
                            " FROM ProdOrderDetail L " &
                            " WHERE L.ProdOrder = '" & .AgSelectedValue(Col1ProdOrder, I) & "' AND L.ProdOrderSr = " & Val(.Item(Col1ProdOrderSr, I).Value) & " " &
                            " GROUP BY L.ProdOrder, L.ProdOrderSr "
                    AgL.ECmd = AgL.Dman_Execute(mQry, AgL.GCn)
                    bPlanQty = AgL.ECmd.ExecuteScalar()

                    mQry = " SELECT IFNull(sum(JOD.Qty),0)  AS OrderQty " &
                            " FROM JobOrderDetail JOD " &
                            " WHERE JOD.ProdOrder IS NOT NULL " &
                            " AND JOD.DocId <> '" & mInternalCode & "' " &
                            " AND JOD.ProdOrderSr = " & Val(.Item(Col1ProdOrderSr, I).Value) & " " &
                            " AND JOD.ProdOrder = '" & .AgSelectedValue(Col1ProdOrder, I) & "' "
                    AgL.ECmd = AgL.Dman_Execute(mQry, AgL.GCn)
                    bOrderQty = AgL.ECmd.ExecuteScalar()

                    'If Math.Round(bPlanQty, 4) < Math.Round((Val(Dgl1.Item(Col1Qty, I).Value) + bOrderQty), 4) Then
                    '    MsgBox("Order Qty is Greater than Balance Plan Qty At Row No " & Dgl1.Item(ColSNo, I).Value & "")
                    '    .CurrentCell = .Item(Col1Qty, I) : Dgl1.Focus()
                    '    passed = False : Exit Sub
                    'End If
                End If
            Next
        End With
    End Sub

    Private Function FCheckDuplicateRefNo() As Boolean
        FCheckDuplicateRefNo = True
        If Topctrl1.Mode = "Add" Then
            mQry = " SELECT COUNT(*) FROM JobOrder   " &
                    " WHERE ManualRefNo = '" & TxtManualRefNo.Text & "'   " &
                    " AND V_Type ='" & TxtV_Type.AgSelectedValue & "'  " &
                    " And Div_Code = '" & TxtDivision.AgSelectedValue & "' " &
                    " And Site_Code = '" & TxtSite_Code.AgSelectedValue & "'  " &
                    " And EntryStatus <> '" & AgTemplate.ClsMain.LogStatus.LogDiscard & "' "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then TxtManualRefNo.Text = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "JobOrder", TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, AgTemplate.ClsMain.ManualRefType.Max) : MsgBox("Reference No. Already Exists New Reference No. Alloted : " & TxtManualRefNo.Text)
        Else
            mQry = " SELECT COUNT(*) FROM JobOrder  " &
                    " WHERE ManualRefNo = '" & TxtManualRefNo.Text & "'   " &
                    " AND V_Type ='" & TxtV_Type.AgSelectedValue & "'  " &
                    " And Div_Code = '" & TxtDivision.AgSelectedValue & "' " &
                    " And Site_Code = '" & TxtSite_Code.AgSelectedValue & "' " &
                    " AND DocID <>'" & mSearchCode & "' " &
                    " And EntryStatus <> '" & AgTemplate.ClsMain.LogStatus.LogDiscard & "' "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then FCheckDuplicateRefNo = False : MsgBox("Reference No. Already Exists") : TxtManualRefNo.Focus()
        End If
    End Function

    Private Sub FrmProductionOrder_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
        Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
        Dgl3.RowCount = 1 : Dgl3.Rows.Clear()

        LblTotalMeasure.Text = 0 : LblTotalQty.Text = 0 : LblTotalAmount.Text = 0

        Dgl5.Item(Col5Amount, Row5GrossAmount).Value = 0
        Dgl6.Item(Col6Amount, Row6Freight).Value = 0
        Dgl5.Item(Col5Amount, Row5RoundOff).Value = 0
        Dgl5.Item(Col5Amount, Row5NetAmount).Value = 0
    End Sub

    Private Sub Txt_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtV_Type.Validating, TxtManualRefNo.Validating, TxtV_Date.Validating, TxtJobWorker.Validating, TxtRate.Validating, TxtItemDivision.Validating
        Dim DrTemp As DataRow() = Nothing
        Dim DtTemp As DataTable = Nothing
        Dim I As Integer = 0
        Dim DueDays As Double = 0

        Try
            Select Case sender.name
                Case TxtItemDivision.Name
                    mJobRateHelpDataSet = FGetJobRateHelpDataSet()
                Case TxtV_Date.Name
                    If TxtV_Date.Text <> "" And TxtDueDate.Text = "" And AgL.PubDtEnviro.Rows.Count > 0 Then
                        TxtDueDate.Text = DateAdd(DateInterval.Day, AgL.VNull(AgL.PubDtEnviro.Rows(0)("DefaultDueDays")), CDate(TxtV_Date.Text))
                        mQry = "Select DefaultDueDays from Process Where NCat= '" & Replace(AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Process")), "|", "") & "'  "
                        DueDays = AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar)
                        If DueDays > 0 Then
                            TxtDueDate.Text = DateAdd(DateInterval.Day, DueDays, CDate(TxtV_Date.Text))
                        End If
                    End If
                    If Topctrl1.Mode = "Add" Then
                        TxtManualRefNo.Text = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "JobOrder", TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, AgTemplate.ClsMain.ManualRefType.Max)
                    End If
                    If Dgl1.AgHelpDataSet(Col1Item) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1Item).Dispose() : Dgl1.AgHelpDataSet(Col1Item) = Nothing
                    If TxtJobWorker.AgHelpDataSet IsNot Nothing Then TxtJobWorker.AgHelpDataSet.Dispose() : TxtJobWorker.AgHelpDataSet = Nothing
                    If AgL.StrCmp(Topctrl1.Mode, "Add") Then Call ProcFillJobValues()

                Case TxtV_Type.Name
                    TxtTermsAndConditions.Text = AgTemplate.ClsMain.FRetTermsCondition(TxtV_Type.AgSelectedValue)
                    If Topctrl1.Mode = "Add" Then
                        TxtManualRefNo.Text = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "JobOrder", TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, AgTemplate.ClsMain.ManualRefType.Max)
                    End If

                    TxtCustomFields.Tag = AgCustomFields.ClsMain.FGetCustomFieldFromV_Type(TxtV_Type.Tag, AgL.GcnRead)
                    AgCustomGrid1.AgCustom = TxtCustomFields.Tag

                    IniGrid()

                    FFillJobEnviro()


                    FAsignProcess()
                    FAsignMeasureField()
                    mJobRateHelpDataSet = FGetJobRateHelpDataSet()

                    mQry = "Select * from JobEnviro  Where V_Type = '" & TxtV_Type.Tag & "' And Div_Code = '" & TxtDivision.Tag & "' And Site_Code ='" & TxtSite_Code.Tag & "' "
                    DtJobEnviro = AgL.FillData(mQry, AgL.GCn).Tables(0)
                    If DtV_TypeSettings.Rows.Count = 0 Then
                        MsgBox("Job Envirnment Settings are not defined. Can't Continue!")
                        Topctrl1.FButtonClick(14, True)
                        Exit Sub
                    End If

                    If DtJobEnviro.Rows.Count > 0 Then
                        If AgL.VNull(DtJobEnviro.Rows(0)("IsAllowed_MaterialIssue")) = 0 Then
                            BtnMaterialIssueDetail.Visible = False
                        Else
                            BtnMaterialIssueDetail.Visible = True
                        End If
                    End If

                Case TxtManualRefNo.Name
                    e.Cancel = Not FCheckDuplicateRefNo()

                Case TxtJobWorker.Name
                    If TxtJobWorker.AgSelectedValue <> "" Then
                        mQry = "Select IFNull(H.JobWithMaterialYN,0) As JobWithMaterialYN, H.InsideOutside " &
                                " From JobWorker H   " &
                                " Where H.SubCode = '" & TxtJobWorker.AgSelectedValue & "' "
                        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
                        With DtTemp
                            If .Rows.Count > 0 Then
                                If AgL.XNull(.Rows(0)("InsideOutside")) <> "" And TxtInsideOutside.Text = "" Then TxtInsideOutside.Text = AgL.XNull(.Rows(0)("InsideOutside"))
                                TxtWithMaterialYN.Text = IIf(AgL.VNull(.Rows(0)("JobWithMaterialYN")) = 0, "No", "Yes")
                            End If
                        End With
                    End If

                Case TxtRate.Name
                    For I = 0 To Dgl1.Rows.Count - 1
                        'If Val(Dgl1.Item(Col1Rate, I).Value) = "0" Then
                        If Dgl1.Item(Col1Item, I).Value <> "" Then
                            Dgl1.Item(Col1Rate, I).Value = Val(TxtRate.Text)
                        End If
                    Next
                    Calculation()

            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Validating_Item(ByVal mColumn As Integer, ByVal mRow As Integer)
        Dim DrTemp As DataRow() = Nothing
        Dim DtTemp As DataTable = Nothing
        Dim sqlConn As SQLiteConnection = Nothing
        Dim sqlDA As SQLiteDataAdapter = Nothing

        sqlConn = New SQLiteConnection
        sqlConn.ConnectionString = AgL.Gcn_ConnectionString
        sqlConn.Open()

        Try
            If Dgl1.Item(mColumn, mRow).Value.ToString.Trim = "" Or Dgl1.AgSelectedValue(mColumn, mRow).ToString.Trim = "" Then
                Dgl1.Item(Col1Qty, mRow).Value = 0
                Dgl1.Item(Col1Unit, mRow).Value = ""
                Dgl1.Item(Col1MeasurePerPcs, mRow).Value = 0
                Dgl1.Item(Col1MeasureUnit, mRow).Value = ""
            Else
                If Dgl1.AgDataRow IsNot Nothing Then
                    Dgl1.Item(Col1Item, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("ItemCode").Value)
                    Dgl1.Item(Col1Item, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("Item").Value)
                    Dgl1.Item(Col1Unit, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("Unit").Value)
                    Dgl1.Item(Col1QtyDecimalPlaces, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("QtyDecimalPlaces").Value)
                    Dgl1.Item(Col1MeasurePerPcs, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("MeasurePerPcs").Value)
                    Dgl1.Item(Col1MeasureUnit, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("MeasureUnit").Value)
                    Dgl1.Item(Col1MeasureDecimalPlaces, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("MeasureDecimalPlaces").Value)

                    Dgl1.Item(Col1Qty, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("Qty").Value)
                    Dgl1.Item(Col1FromProcess, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("Process").Value)
                    Dgl1.Item(Col1FromProcess, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("ProcessCode").Value)


                    Dgl1.Item(Col1ItemGroup, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("ItemGroup").Value)
                    Dgl1.Item(Col1ItemGroup, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("ItemGroupDesc").Value)
                    Dgl1.Item(Col1LotNo, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("LotNo").Value)
                    Dgl1.Item(Col1ItemCategory, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("ItemCategory").Value)
                    Dgl1.Item(Col1ItemCategory, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("ItemCategoryDesc").Value)



                    Dgl1.Item(Col1Dimension1, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("Dimension1").Value)
                    Dgl1.Item(Col1Dimension1, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("" & AgTemplate.ClsMain.FGetDimension1Caption() & "").Value)

                    Dgl1.Item(Col1Dimension2, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("Dimension2").Value)
                    Dgl1.Item(Col1Dimension2, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("" & AgTemplate.ClsMain.FGetDimension2Caption() & "").Value)



                    If RbtForStock.Checked Then
                        Dgl1.Item(Col1V_Nature, mRow).Value = RbtForStock.Text
                    ElseIf RbtForProdOrder.Checked Then
                        Dgl1.Item(Col1V_Nature, mRow).Value = RbtForProdOrder.Text
                    Else
                        Dgl1.Item(Col1V_Nature, mRow).Value = RbtAllItems.Text
                    End If

                    'Dgl1.Item(Col1Rate, mRow).Value = FGetJobRate(TxtProcess.Tag, TxtJobWorker.Tag, Dgl1.Item(Col1Item, mRow).Tag)

                    'If AgL.StrCmp(AgL.PubCompShortName, "Surya") Or AgL.StrCmp(AgL.PubCompShortName, "CWPL") Then
                    If AgL.StrCmp(AgL.PubCompShortName, "Surya") Then
                        Dgl1.Item(Col1Rate, mRow).Value = ClsMain.FGetJobRate(mJobRateHelpDataSet, Val(TxtRate.Text),
                                TxtProcess.Tag, TxtInsideOutside.Text, TxtV_Date.Text,
                                Dgl1.Item(Col1Item, mRow).Tag, Dgl1.Item(Col1ItemGroup, mRow).Tag,
                                Dgl1.Item(Col1ItemCategory, mRow).Tag, Val(Dgl1.Item(Col1MeasurePerPcs, mRow).Value))

                        Dgl1.Item(Col1IncentiveRate, mRow).Value = ClsMain.FGetJobIncentiveRate(mJobRateHelpDataSet,
                                TxtProcess.Tag, TxtInsideOutside.Text, TxtV_Date.Text,
                                Dgl1.Item(Col1Item, mRow).Tag, Dgl1.Item(Col1ItemGroup, mRow).Tag,
                                Dgl1.Item(Col1ItemCategory, mRow).Tag, Val(Dgl1.Item(Col1MeasurePerPcs, mRow).Value))

                    End If



                    mQry = "Select ProcessSequence, (Select Count(*) from ProcessSequenceDetail Where Code = H.ProcessSequence And Process = '" & LblV_Type.Tag & "') as IterationsAllowed from Item H Where Code = '" & Dgl1.Item(Col1Item, mRow).Tag & "' "
                    DtTemp = AgL.FillData(mQry, AgL.GcnRead).tables(0)
                    If DtTemp.Rows.Count > 0 Then
                        Dgl1.Item(Col1ProcessSequence, mRow).Value = AgL.XNull(DtTemp.Rows(0)("ProcessSequence"))
                        Dgl1.Item(Col1ProcessIterationsAllowed, mRow).Value = AgL.VNull(DtTemp.Rows(0)("IterationsAllowed"))
                    End If

                    'Code Writtern For Retreiving Pervious Process
                    If Dgl1.Item(Col1FromProcess, mRow).Value = "" Then
                        'mQry = " SELECT Top 1 Psd.Process As ProcessCode, P.Description As ProcessDesc " & _
                        '        " FROM Item I  " & _
                        '        " LEFT JOIN ProcessSequenceDetail Psd ON I.ProcessSequence = Psd.Code " & _
                        '        " LEFT JOIN Process P ON Psd.Process = P.NCat " & _
                        '        " WHERE I.Code = '" & Dgl1.Item(Col1Item, mRow).Tag & "' " & _
                        '        " AND Psd.Sequence < (Select Sequence From ProcessSequenceDetail " & _
                        '        "                     Where Code = '" & Dgl1.Item(Col1ProcessSequence, mRow).Value & "'  " & _
                        '        "                     And Process = '" & TxtProcess.Tag & "')  " & _
                        '        " Order By Psd.Sequence Desc "
                        'Dim DtProcess As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
                        'If DtProcess.Rows.Count > 0 Then
                        '    Dgl1.Item(Col1FromProcess, mRow).Tag = AgL.XNull(DtProcess.Rows(0)("ProcessCode"))
                        '    Dgl1.Item(Col1FromProcess, mRow).Value = AgL.XNull(DtProcess.Rows(0)("ProcessDesc"))
                        'End If
                    End If

                    Dgl1.Item(Col1ProdOrder, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("ProdOrder").Value)
                    Dgl1.Item(Col1ProdOrder, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("ProdOrderNo").Value)
                    Dgl1.Item(Col1ProdOrderSr, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("ProdOrderSr").Value)
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
        Dim ErrMsgStr$ = ""
        Dim I As Integer = 0
        Try
            mRowIndex = Dgl1.CurrentCell.RowIndex
            mColumnIndex = Dgl1.CurrentCell.ColumnIndex
            If Dgl1.Item(mColumnIndex, mRowIndex).Value Is Nothing Then Dgl1.Item(mColumnIndex, mRowIndex).Value = ""
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Item
                    If mJobRateHelpDataSet Is Nothing Then mJobRateHelpDataSet = FGetJobRateHelpDataSet()
                    Validating_Item(mColumnIndex, mRowIndex)

                Case Col1Dimension1
                    If RbtForStock.Checked Then
                        If mJobRateHelpDataSet Is Nothing Then mJobRateHelpDataSet = FGetJobRateHelpDataSet()
                        Validating_Item(mColumnIndex, mRowIndex)
                    End If

                Case Col1Dimension2
                    If RbtForStock.Checked Then
                        If mJobRateHelpDataSet Is Nothing Then mJobRateHelpDataSet = FGetJobRateHelpDataSet()
                        Validating_Item(mColumnIndex, mRowIndex)
                    End If

                Case Col1Item_Uid
                    ErrMsgStr = FCheck_Item_UID(Dgl1.Item(Col1Item_Uid, mRowIndex).Value)
                    If ErrMsgStr <> "" Then
                        MsgBox(ErrMsgStr)
                        Dgl1.Item(Col1Item_Uid, Dgl1.CurrentCell.RowIndex).Value = ""
                        Dgl1.Item(Col1Item_Uid, Dgl1.CurrentCell.RowIndex).Tag = ""
                        Exit Sub
                    End If
                    If mJobRateHelpDataSet Is Nothing Then mJobRateHelpDataSet = FGetJobRateHelpDataSet()
                    Validating_Item_Uid(Dgl1.Item(Col1Item_Uid, mRowIndex).Value, mRowIndex)

                Case Col1LotNo
                    If Dgl1.Item(Col1LotNo, mRowIndex).Tag IsNot Nothing Then
                        Validating_LotNo(Dgl1.Item(Col1LotNo, mRowIndex).Tag, mRowIndex)
                    End If

                Case Col1FromProcess
                    'If Dgl1.Item(Col1FromProcess, mRowIndex).Tag IsNot Nothing Then
                    '    Validating_FromProcess(Dgl1.Item(Col1FromProcess, mRowIndex).Tag, mRowIndex)
                    'End If

                    If Dgl1.Item(Col1FromProcess, mRowIndex).Value <> "" Then
                        If MsgBox("Apply To All ?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
                            For I = mRowIndex To Dgl1.Rows.Count - 1
                                If Dgl1.Item(Col1Item_Uid, I).Value = "" And Dgl1.Item(Col1Item, I).Value <> "" Then
                                    Dgl1.Item(Col1FromProcess, I).Tag = Dgl1.Item(Col1FromProcess, mRowIndex).Tag
                                    Dgl1.Item(Col1FromProcess, I).Value = Dgl1.Item(Col1FromProcess, mRowIndex).Value
                                End If
                            Next
                        End If
                    End If

                Case Col1Unit
                    If Dgl1.CurrentCell.RowIndex = 0 Then
                        If MsgBox("Apply To All ?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
                            For I = 0 To Dgl1.Rows.Count - 1
                                If Dgl1.Item(Col1Item, I).Value <> "" Then
                                    Dgl1.Item(Col1Unit, I).Value = Dgl1.Item(Col1Unit, 0).Value
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

                    If RbtForStock.Checked Then
                        Dgl1.Item(Col1V_Nature, mRow).Value = RbtForStock.Text
                    ElseIf RbtForProdOrder.Checked Then
                        Dgl1.Item(Col1V_Nature, mRow).Value = RbtForProdOrder.Text
                    Else
                        Dgl1.Item(Col1V_Nature, mRow).Value = RbtAllItems.Text
                    End If

                    'Dgl1.Item(Col1Rate, mRow).Value = FGetJobRate(TxtProcess.Tag, TxtJobWorker.Tag, Dgl1.Item(Col1Item, mRow).Tag)

                    If AgL.StrCmp(AgL.PubCompShortName, "Surya") Then
                        Dgl1.Item(Col1Rate, mRow).Value = ClsMain.FGetJobRate(mJobRateHelpDataSet, Val(TxtRate.Text),
                                TxtProcess.Tag, TxtInsideOutside.Text, TxtV_Date.Text,
                                Dgl1.Item(Col1Item, mRow).Tag, Dgl1.Item(Col1ItemGroup, mRow).Tag,
                                Dgl1.Item(Col1ItemCategory, mRow).Tag, Val(Dgl1.Item(Col1MeasurePerPcs, mRow).Value))

                    End If



                    mQry = "Select ProcessSequence, (Select Count(*) from ProcessSequenceDetail Where Code = H.ProcessSequence And Process = '" & LblV_Type.Tag & "') as IterationsAllowed from Item H Where Code = '" & Dgl1.Item(Col1Item, mRow).Tag & "' "
                    DtTemp = AgL.FillData(mQry, AgL.GcnRead).tables(0)
                    If DtTemp.Rows.Count > 0 Then
                        Dgl1.Item(Col1ProcessSequence, mRow).Value = AgL.XNull(DtTemp.Rows(0)("ProcessSequence"))
                        Dgl1.Item(Col1ProcessIterationsAllowed, mRow).Value = AgL.VNull(DtTemp.Rows(0)("IterationsAllowed"))
                    End If

                    'Code Writtern For Retreiving Pervious Process
                    If Dgl1.Item(Col1FromProcess, mRow).Value = "" Then
                        mQry = " SELECT Psd.Process As ProcessCode, P.Description As ProcessDesc " &
                                " FROM Item I  " &
                                " LEFT JOIN ProcessSequenceDetail Psd ON I.ProcessSequence = Psd.Code " &
                                " LEFT JOIN Process P ON Psd.Process = P.NCat " &
                                " WHERE I.Code = '" & Dgl1.Item(Col1Item, mRow).Tag & "' " &
                                " AND Psd.Sequence < (Select Sequence From ProcessSequenceDetail " &
                                "                     Where Code = '" & Dgl1.Item(Col1ProcessSequence, mRow).Value & "'  " &
                                "                     And Process = '" & TxtProcess.Tag & "')  " &
                                " Order By Psd.Sequence Desc Limit 1"
                        Dim DtProcess As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
                        If DtProcess.Rows.Count > 0 Then
                            Dgl1.Item(Col1FromProcess, mRow).Tag = AgL.XNull(DtProcess.Rows(0)("ProcessCode"))
                            Dgl1.Item(Col1FromProcess, mRow).Value = AgL.XNull(DtProcess.Rows(0)("ProcessDesc"))
                        End If
                    End If

                    Dgl1.Item(Col1ProdOrder, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("ProdOrder").Value)
                    Dgl1.Item(Col1ProdOrder, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("ProdOrderNo").Value)
                    Dgl1.Item(Col1ProdOrderSr, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("ProdOrderSr").Value)
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " On Validating_LotNo Function ")
        End Try
    End Sub

    Private Sub Validating_FromProcess(ByVal Code As String, ByVal mRow As Integer)
        Dim DrTemp As DataRow() = Nothing
        Dim DtTemp As DataTable = Nothing

        Try
            If Dgl1.Item(Col1FromProcess, mRow).Value.ToString.Trim = "" Or Dgl1.AgSelectedValue(Col1FromProcess, mRow).ToString.Trim = "" Then
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
                    Dgl1.Item(Col1MeasureDecimalPlaces, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("MeasureDecimalPlaces").Value)
                    Dgl1.Item(Col1Qty, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("Qty").Value)
                    Dgl1.Item(Col1LotNo, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("LotNo").Value)
                    Dgl1.Item(Col1Dimension1, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("Dimension1").Value)
                    Dgl1.Item(Col1Dimension1, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("" & AgTemplate.ClsMain.FGetDimension1Caption() & "").Value)
                    Dgl1.Item(Col1Dimension2, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("Dimension2").Value)
                    Dgl1.Item(Col1Dimension2, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("" & AgTemplate.ClsMain.FGetDimension2Caption() & "").Value)

                    Dgl1.Item(Col1ItemGroup, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("ItemGroup").Value)
                    Dgl1.Item(Col1ItemGroup, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("ItemGroupDesc").Value)

                    Dgl1.Item(Col1ItemCategory, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("ItemCategory").Value)
                    Dgl1.Item(Col1ItemCategory, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("ItemCategoryDesc").Value)


                    If RbtForStock.Checked Then
                        Dgl1.Item(Col1V_Nature, mRow).Value = RbtForStock.Text
                    ElseIf RbtForProdOrder.Checked Then
                        Dgl1.Item(Col1V_Nature, mRow).Value = RbtForProdOrder.Text
                    Else
                        Dgl1.Item(Col1V_Nature, mRow).Value = RbtAllItems.Text
                    End If

                    'Dgl1.Item(Col1Rate, mRow).Value = FGetJobRate(TxtProcess.Tag, TxtJobWorker.Tag, Dgl1.Item(Col1Item, mRow).Tag)

                    If AgL.StrCmp(AgL.PubCompShortName, "Surya") Then
                        Dgl1.Item(Col1Rate, mRow).Value = ClsMain.FGetJobRate(mJobRateHelpDataSet, Val(TxtRate.Text),
                                TxtProcess.Tag, TxtInsideOutside.Text, TxtV_Date.Text,
                                Dgl1.Item(Col1Item, mRow).Tag, Dgl1.Item(Col1ItemGroup, mRow).Tag,
                                Dgl1.Item(Col1ItemCategory, mRow).Tag, Val(Dgl1.Item(Col1MeasurePerPcs, mRow).Value))

                    End If


                    Dgl1.Item(Col1ProdOrder, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("ProdOrder").Value)
                    Dgl1.Item(Col1ProdOrder, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("ProdOrderNo").Value)
                    Dgl1.Item(Col1ProdOrderSr, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("ProdOrderSr").Value)
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " On Validating_LotNo Function ")
        End Try
    End Sub

    Private Sub FPostInStockProcess(ByVal SearchCode As String, ByVal Conn As SQLiteConnection, ByVal Cmd As SQLiteCommand)
        Dim Stock As AgTemplate.ClsMain.StructStock = Nothing, StockProcess As AgTemplate.ClsMain.StructStock = Nothing


        If CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsPostedInStock")), Boolean) Then
            'Qry Was Written For Managing Process Wise Stock For Surya Carpet
            'But After physical stock it is turned into normal Stock Posting

            'If AgL.StrCmp(AgL.PubCompShortName, "Surya") Then
            '    'Code For Stock Posting Process Wise
            '    Dim StockView$ = ""
            '    StockView = " Select L.DocID, L.Sr, H.V_Type, " & _
            '                " H.V_Prefix, H.V_Date, H.V_No, H.ManualRefNo As RecId, H.Div_Code, " & _
            '                " H.Site_Code,   " & _
            '                " H.JobWorker As SubCode, L.Item, H.Godown, L.Qty, L.Unit, L.MeasurePerPcs, " & _
            '                " L.TotalMeasure, L.MeasureUnit, L.FromProcess As Process " & _
            '                " From JobOrder As H   " & _
            '                " LEFT JOIN JobOrderDetail As L  On H.DocId = L.DocId " & _
            '                " Where H.DocId = '" & mInternalCode & "' "
            '    AgTemplate.ClsMain.FPostInStockWithProcess(StockView, mInternalCode, TxtGodown.Tag, TxtV_Date.Text, Conn, Cmd)
            '    'Code End For Stock Posting Process Wise
            'Else
            mQry = "Delete From Stock Where DocId = '" & mSearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

            mQry = "INSERT INTO Stock(DocID, Sr, V_Type, V_Prefix, V_Date, V_No, RecID, Div_Code, Site_Code, " &
                    " SubCode, Item, Godown, Qty_Iss, Unit, MeasurePerPcs, Measure_Iss, MeasureUnit, " &
                    " Remarks, Process, CostCenter, LotNo, Dimension1, Dimension2 ) " &
                    " Select L.DocID, row_number() OVER (ORDER BY L.Item),Max(H.V_Type), " &
                    " Max(H.V_Prefix), Max(H.V_Date), Max(H.V_No), Max(H.ManualRefNo), Max(H.Div_Code), Max(H.Site_Code),   " &
                    " Max(H.JobWorker), L.Item, Max(H.Godown), Sum(L.Qty), Max(L.Unit), Max(L.MeasurePerPcs), " &
                    " Sum(L.TotalMeasure), Max(L.MeasureUnit),   " &
                    " Max(Remark), L.FromProcess, Max(H.CostCenter), L.LotNo, " &
                    " L.Dimension1, L.Dimension2 " &
                    " From (Select * From JobOrder Where DocId = '" & mSearchCode & "') H   " &
                    " LEFT JOIN JobOrderDetail L On H.DocId = L.DocId   " &
                    " Group By L.DocId, L.Item, L.LotNo, L.FromProcess, L.Dimension1, L.Dimension2 "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            'End If
        End If


        If CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsPostedInStockProcess")), Boolean) Then
            mQry = "Delete From StockProcess Where DocId = '" & mSearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

            mQry = "INSERT INTO StockProcess(DocID, Sr, V_Type, V_Prefix, V_Date, V_No, RecID, Div_Code, Site_Code, " &
                     " SubCode, Item, Godown, Qty_Rec, Unit, MeasurePerPcs, Measure_Rec, MeasureUnit, " &
                     " Remarks, Process, CostCenter, LotNo , Dimension1, Dimension2 ) " &
                     " Select L.DocID, row_number() OVER (ORDER BY L.Item),Max(H.V_Type), " &
                     " Max(H.V_Prefix), Max(H.V_Date), Max(H.V_No), Max(H.ManualRefNo), Max(H.Div_Code), Max(H.Site_Code),   " &
                     " Max(H.JobWorker), L.Item, Max(H.Godown), Sum(L.Qty), Max(L.Unit), Max(L.MeasurePerPcs), " &
                     " Sum(L.TotalMeasure), Max(L.MeasureUnit),   " &
                     " Max(L.Remark), H.Process, Max(H.CostCenter) As CostCenter, L.LotNo, " &
                     " L.Dimension1, L.Dimension2 " &
                     " From (Select * From JobOrder Where DocId = '" & mSearchCode & "') H   " &
                     " LEFT JOIN JobOrderDetail L On H.DocId = L.DocId   " &
                     " Group By L.DocId, L.Item, L.LotNo, H.Process, L.Dimension1, L.Dimension2 "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If

        If CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsPostedInStockVirtual")), Boolean) = True Then
            mQry = "Delete From StockVirtual Where DocId = '" & mSearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

            mQry = "INSERT INTO StockVirtual(DocID, Sr, V_Type, V_Prefix, V_Date, V_No, RecID, Div_Code, Site_Code, " &
                     " SubCode, Item, Godown, Qty_Rec, Unit, MeasurePerPcs, Measure_Rec, MeasureUnit, " &
                     " Remarks, Process, CostCenter, LotNo ) " &
                     " Select L.DocID, row_number() OVER (ORDER BY L.Item),Max(H.V_Type), " &
                     " Max(H.V_Prefix), Max(H.V_Date), Max(H.V_No), Max(H.ManualRefNo), Max(H.Div_Code), Max(H.Site_Code),   " &
                     " Max(H.JobWorker), L.Item, Max(H.Godown), Sum(L.Qty), Max(L.Unit), Max(L.MeasurePerPcs), " &
                     " Sum(L.TotalMeasure), Max(L.MeasureUnit),   " &
                     " Max(L.Remark), L.FromProcess, Max(H.CostCenter) As CostCenter, L.LotNo " &
                     " From (Select * From JobOrder Where DocId = '" & mSearchCode & "') H   " &
                     " LEFT JOIN JobOrderDetail L On H.DocId = L.DocId   " &
                     " Group By L.DocId, L.Item, L.LotNo, L.FromProcess  "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If


        If CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsPostConsumption")), Boolean) Then
            mQry = "Delete From JobOrderBOM Where DocId = '" & mSearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

            mQry = "INSERT INTO JobOrderBOM(DocId, TSr, Sr, JobOrder, JobOrderSr, JobOrderBomSr, " &
                    " Item, Qty, Unit, ConsumptionPerMeasure, MeasurePerPcs, TotalMeasure, MeasureUnit) " &
                    " SELECT L.DocId, L.Sr AS TSr, row_NUMBER() OVER (ORDER BY L.Sr) AS Sr, " &
                    " L.JobOrder, L.JobOrderSr, row_NUMBER() OVER (ORDER BY L.Sr) As JobOrderBomSr, " &
                    " Bd.Item, Bd.Qty * L.Qty AS BomQty, BomItem.Unit, " &
                    " Bd.Qty AS ConsumptionPerMeasure, BomItem.Measure, " &
                    " Bd.Qty * L.Qty As TotalMeasure, BomItem.MeasureUnit  " &
                    " FROM (Select * From JobOrderDetail Where DocId = '" & mSearchCode & "') As L  " &
                    " LEFT JOIN Item I On L.Item = I.Code " &
                    " LEFT JOIN BomDetail Bd ON I.Code = Bd.BaseItem " &
                    " LEFT JOIN Item BomItem ON Bd.Item = BomItem.Code " &
                    " Where Bd.Process = '" & TxtProcess.Tag & "'"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If
    End Sub

    Private Sub FPostFreight(ByVal SearchCode As String, ByVal Conn As SQLiteConnection, ByVal Cmd As SQLiteCommand)
        Dim FreightAc As String = ""
        If AgL.PubDtEnviro.Rows.Count > 0 Then
            FreightAc = AgL.XNull(AgL.PubDtEnviro.Rows(0)("FreightAc"))
        End If

        mQry = "Delete From Ledger Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        'mQry = "Delete From DuesPaymentDetail Where DocId = '" & mSearchCode & "'"
        'AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        'mQry = "Delete From DuesPayment Where DocId = '" & mSearchCode & "'"
        'AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        'mQry = "INSERT INTO DuesPayment (DocID, V_Type, V_Prefix, V_Date, V_No, Div_Code, Site_Code, TransactionType, SubCode,   NetAmount, Remark, EntryBy, EntryDate, EntryType, EntryStatus, ApproveBy, ApproveDate, MoveToLog, MoveToLogDate, IsDeleted, Status, ManualRefNo, CostCenter, Process) " & _
        '        " SELECT DocID, V_Type, V_Prefix, V_Date, V_No, Div_Code, Site_Code, '1', " & AgL.Chk_Text(FreightAc) & ",   NetAmount, Remarks, EntryBy, EntryDate, EntryType, EntryStatus, ApproveBy, ApproveDate, MoveToLog, MoveToLogDate, IsDeleted, Status, ManualRefNo, CostCenter, Process " & _
        '        " FROM JobOrder  WHERE DocID = '" & mSearchCode & "' "
        'AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        'mQry = "INSERT INTO dbo.DuesPaymentDetail (DocID, Sr, Amount, SubCode, NetAmount, Remark, CostCenter) " & _
        '        " SELECT DocID, 1 AS Sr, Freight, JobWorker,  Freight,  Remarks, Process  FROM JobOrder WHERE DocID = '" & mSearchCode & "' "
        'AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)


        mQry = " INSERT INTO Ledger(DocId, V_SNo, V_No, V_Type, RecId, V_Prefix, V_Date, SubCode, ContraSub,  AmtDr, AmtCr, Narration,	Site_Code,U_Name,	U_EntDt,	DivCode, CostCenter, JobOrder) " &
                " SELECT DocId, 1 AS V_SNo, V_No, V_Type, ManualrefNo AS RecId, V_Prefix, V_Date, H.JobWorker  AS SubCode, " & AgL.Chk_Text(FreightAc) & " AS ContraSub,  H.Freight AS AmtDr, 0 AS AmtCr, 'Freight' AS Narration,	Site_Code, H.EntryBy AS U_Name,  H.EntryDate 	U_EntDt,	Div_Code, CostCenter, JobOrder " &
                " FROM JobOrder H WHERE DocID = '" & mSearchCode & "' AND IFNull(H.Freight,0) <> 0 "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " INSERT INTO Ledger(DocId, V_SNo, V_No, V_Type, RecId, V_Prefix, V_Date, SubCode, ContraSub,  AmtDr, AmtCr, Narration,	Site_Code,U_Name,	U_EntDt,	DivCode, CostCenter, JobOrder) " &
                " SELECT DocId, 2 AS V_SNo, V_No, V_Type, ManualrefNo AS RecId, V_Prefix, V_Date, " & AgL.Chk_Text(FreightAc) & "  AS SubCode, H.JobWorker AS ContraSub,  0 AS AmtDr, H.Freight AS AmtCr, 'Freight' AS Narration,	Site_Code, H.EntryBy AS U_Name,  H.EntryDate 	U_EntDt,	Div_Code, CostCenter, JobOrder " &
                " FROM JobOrder H WHERE DocID = '" & mSearchCode & "' AND IFNull(H.Freight,0) <> 0 "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

    End Sub

    Private Sub TempJobOrder_BaseEvent_Topctrl_tbAdd() Handles Me.BaseEvent_Topctrl_tbAdd
        TxtManualRefNo.Text = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "JobOrder", TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, AgTemplate.ClsMain.ManualRefType.Max)
        TxtTermsAndConditions.Text = AgTemplate.ClsMain.FRetTermsCondition(TxtV_Type.AgSelectedValue)
        TxtOrderBy.Tag = mLastOrderBy
        TxtOrderBy.Text = AgL.Dman_Execute(" SELECT DispName FROM SubGroup WHERE SubCode = '" & mLastOrderBy & "'", AgL.GCn).ExecuteScalar

        TxtCustomFields.Tag = AgCustomFields.ClsMain.FGetCustomFieldFromV_Type(TxtV_Type.Tag, AgL.GCn)
        AgCustomGrid1.AgCustom = TxtCustomFields.Tag
        IniGrid()
        FAsignProcess()
        FAsignMeasureField()
        BtnImprtFromText.Text = ImportAction_NewImport

        If DtJobEnviro.Rows.Count > 0 Then
            If AgL.VNull(DtJobEnviro.Rows(0)("IsAllowed_MaterialIssue")) = 0 Then
                BtnMaterialIssueDetail.Visible = False
            Else
                BtnMaterialIssueDetail.Visible = True
            End If
        End If


        TxtGodown.Tag = PubDefaultGodownCode
        TxtGodown.Text = PubDefaultGodownName
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

        If TxtGodown.Tag = "" Then
            TxtGodown.Tag = AgL.XNull(DtV_TypeSettings.Rows(0)("DEFAULT_Godown"))
            TxtGodown.Text = AgL.XNull(AgL.Dman_Execute("SELECT Description  FROM Godown WHERE Code = " & AgL.Chk_Text(TxtGodown.Tag) & " ", AgL.GCn).ExecuteScalar)
        End If
    End Sub

    Private Sub ProcFillJobValues()
        Dim I As Integer
        Dim DtTemp As DataTable = Nothing
        Try
            mQry = " SELECT L.Parameter, L.StdValue  " &
                    " FROM QcGroupDetail L    " &
                    " LEFT JOIN QcGroup H   ON L.Code = H.Code " &
                    " Where L.Code = (SELECT P.QcGroup FROM Process P   WHERE P.NCat = '" & TxtProcess.Tag & "') " &
                    " And H.Div_Code = '" & AgL.PubDivCode & "' "
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
            With DtTemp
                Dgl3.RowCount = 1
                Dgl3.Rows.Clear()
                If .Rows.Count > 0 Then
                    For I = 0 To .Rows.Count - 1
                        Dgl3.Rows.Add()
                        Dgl3.Item(ColSNo, I).Value = Dgl3.Rows.Count
                        Dgl3.Item(Col3Parameter, I).Value = AgL.XNull(.Rows(I)("Parameter"))
                        Dgl3.Item(Col3StdValue, I).Value = AgL.XNull(.Rows(I)("StdValue"))
                    Next
                End If
            End With

            mQry = " SELECT H.InsideOutside,  H.DefaultJobOrderFor, H.DefaultBillingType " &
                    " FROM Process H   " &
                    " WHERE H.NCat = '" & TxtProcess.Tag & "' "
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
            With DtTemp
                If .Rows.Count > 0 Then
                    TxtInsideOutside.Text = AgL.XNull(.Rows(0)("InsideOutside"))
                    TxtBillingType.Text = AgL.XNull(.Rows(0)("DefaultBillingType"))
                End If
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ProcCheckForDefaultProperties()
        Dim bMsgStr$ = ""
        Try
            If TxtInsideOutside.Text = "" Then
                bMsgStr &= "Set the Default value for ""Inside/Outside"" In Process Master." & vbCrLf
            End If
            If TxtBillingType.Text = "" Then
                bMsgStr &= "Set the Default value for ""Billing Type"" In Process Master."
            End If
            If bMsgStr <> "" Then
                MsgBox(bMsgStr, MsgBoxStyle.Exclamation)
                Topctrl1.FButtonClick(14, True)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
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
            If Dgl1.AgHelpDataSet(Col1LotNo) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1LotNo).Dispose() : Dgl1.AgHelpDataSet(Col1LotNo) = Nothing
            If Dgl1.AgHelpDataSet(Col1Item) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1Item).Dispose() : Dgl1.AgHelpDataSet(Col1Item) = Nothing
            If TxtJobWorker.AgHelpDataSet IsNot Nothing Then TxtJobWorker.AgHelpDataSet.Dispose() : TxtJobWorker.AgHelpDataSet = Nothing
            If TxtGodown.AgHelpDataSet IsNot Nothing Then TxtGodown.AgHelpDataSet.Dispose() : TxtGodown.AgHelpDataSet = Nothing
            If TxtOrderBy.AgHelpDataSet IsNot Nothing Then TxtOrderBy.AgHelpDataSet.Dispose() : TxtOrderBy.AgHelpDataSet = Nothing
        Catch ex As Exception
        End Try
    End Sub

    Private Sub TxtOrderBy_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtOrderBy.KeyDown, TxtGodown.KeyDown, TxtBillingType.KeyDown, TxtInsideOutside.KeyDown, TxtJobWorker.KeyDown, TxtProcess.KeyDown, TxtItemDivision.KeyDown, TxtMachine.KeyDown
        Try
            Select Case sender.name
                Case TxtItemDivision.Name
                    If e.KeyCode <> Keys.Enter Then
                        If sender.AgHelpDataSet Is Nothing Then
                            mQry = "SELECT Div_Code, Div_Name  FROM Division "
                            sender.AgHelpDataSet(0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If


                Case TxtGodown.Name
                    If e.KeyCode <> Keys.Enter Then
                        If sender.AgHelpDataSet Is Nothing Then
                            mQry = " SELECT H.Code, H.Description AS Godown, " &
                                    " H.Div_Code, IFNull(H.IsDeleted,0) As IsDeleted, " &
                                    " IFNull(H.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') As Status, " &
                                    " H.Site_Code " &
                                    " FROM Godown H     " &
                                    " Where IFNull(H.IsDeleted,0) = 0 " &
                                    " And IFNull(H.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " &
                                    " And Site_Code = '" & TxtSite_Code.AgSelectedValue & "'"
                            sender.AgHelpDataSet(4, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If


                Case TxtOrderBy.Name
                    If e.KeyCode <> Keys.Enter Then
                        If sender.AgHelpDataSet Is Nothing Then
                            mQry = " SELECT L.SubCode AS Code, L.DispName AS OrderBy " &
                                    " FROM SubGroup L   " &
                                    " Where IFNull(L.IsDeleted,0) = 0 AND MasterType = '" & AgTemplate.ClsMain.SubgroupType.Employee & "'" &
                                    " And IFNull(L.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' "
                            sender.AgHelpDataSet(0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If


                Case TxtBillingType.Name
                    If e.KeyCode <> Keys.Enter Then
                        If sender.AgHelpDataSet Is Nothing Then
                            sender.AgHelpDataSet(0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(AgTemplate.ClsMain.HelpQueries.BillingType, AgL.GCn)
                        End If
                    End If


                Case TxtInsideOutside.Name
                    If e.KeyCode <> Keys.Enter Then
                        If sender.AgHelpDataSet Is Nothing Then
                            mQry = " Select '" & AgTemplate.ClsMain.JobType.Inside & "' As Code, '" & AgTemplate.ClsMain.JobType.Inside & "' As JobType   " &
                                    " UNION ALL " &
                                    " Select '" & AgTemplate.ClsMain.JobType.Outside & "' As Code, '" & AgTemplate.ClsMain.JobType.Outside & "' As JobType   "
                            sender.AgHelpDataSet(0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
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
                        If sender.AgHelpDataSet Is Nothing Then
                            mQry = " SELECT H.NCat AS Code, H.Description AS Process FROM Process H "
                            sender.AgHelpDataSet(0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
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

                Case Col1Dimension1
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1Dimension1) Is Nothing Then
                            FCreateHelpDimension1()
                        End If
                    End If

                Case Col1Dimension2
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1Dimension2) Is Nothing Then
                            FCreateHelpDimension2()
                        End If
                    End If

                    'If e.KeyCode <> Keys.Enter Then
                    '    If Dgl1.AgHelpDataSet(Col1Dimension1) Is Nothing Then
                    '        mQry = " SELECT Code, Description  FROM Dimension1  "
                    '        Dgl1.AgHelpDataSet(Col1Dimension1) = AgL.FillData(mQry, AgL.GCn)
                    '    End If
                    'End If

                    'Case Col1Dimension2
                    '    If e.KeyCode <> Keys.Enter Then
                    '        If Dgl1.AgHelpDataSet(Col1Dimension2) Is Nothing Then
                    '            mQry = " SELECT Code, Description  FROM Dimension2  "
                    '            Dgl1.AgHelpDataSet(Col1Dimension2) = AgL.FillData(mQry, AgL.GCn)
                    '        End If
                    '    End If

                Case Col1LotNo
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1LotNo) Is Nothing Then
                            FCreateHelpLotNo()
                        End If
                    End If

                    'Case Col1FromProcess
                    '    If e.KeyCode <> Keys.Enter Then
                    '        If Dgl1.AgHelpDataSet(Col1FromProcess) Is Nothing Then
                    '            FCreateHelpFromProcess()
                    '        End If
                    '    End If

                Case Col1FromProcess
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1FromProcess) Is Nothing Then
                            mQry = " SELECT P.NCat AS Code, P.Description FROM Process P  "
                            Dgl1.AgHelpDataSet(Col1FromProcess) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If

                Case Col1Unit
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1Unit) Is Nothing Then
                            mQry = " SELECT Code, Code  FROM Unit "
                            Dgl1.AgHelpDataSet(Col1Unit) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If

                Case Col1ProdOrder
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1ProdOrder) Is Nothing Then
                            mQry = " SELECT H.DocId, H.V_Type + '-' + H.ManualRefNo AS OrderNo, H.V_Date AS [Order Date] " &
                                    " FROM ProdOrder H ORDER BY H.V_Date "
                            Dgl1.AgHelpDataSet(Col1ProdOrder) = AgL.FillData(mQry, AgL.GCn)
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
            If AgL.VNull(AgL.Dman_Execute(" Select IFNull(IsRequired_LotNo,0) As IsRequired_LotNo " &
                                          " From ItemSiteDetail Where Code = '" & Dgl1.Item(Col1Item, Dgl1.CurrentCell.RowIndex).Tag & "' " &
                                          " And Site_Code = '" & AgL.PubSiteCode & "'", AgL.GCn).ExecuteScalar) = 0 Then
                Dgl1.AgHelpDataSet(Col1LotNo) = Nothing
                Exit Sub
            End If
        End If

        If Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsPostedInStock")), Boolean) Then Exit Sub

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

            If TxtItemDivision.Text <> "" Then
                strCond += " And  I.Div_Code = '" & TxtItemDivision.Tag & "' "
            Else
                If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemDivision")) <> "" Then
                    strCond += " And CharIndex('|' + I.Div_Code + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemDivision")) & "') > 0 "
                End If
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemSite")) <> "" Then
                strCond += " And CharIndex('|' + I.Site_Code + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemSite")) & "') > 0 "
            End If
        End If

        If Dgl1.Item(Col1Item, Dgl1.CurrentCell.RowIndex).Value <> "" Then
            strCond += " And L.Item = '" & Dgl1.Item(Col1Item, Dgl1.CurrentCell.RowIndex).Tag & "'"
        End If

        mQry = " SELECT L.LotNo As Code, Max(L.LotNo) As LotNo, Max(I.Description) As ItemDesc, Max(P.Description) As Process, " &
                " IFNull(Sum(L.Qty_Rec),0) - IFNull(Sum(L.Qty_Iss),0) AS Qty, Max(I.Unit) As Unit, " &
                " max(D1.Description)  As " & AgTemplate.ClsMain.FGetDimension1Caption() & ", " &
                " max(D2.Description) As " & AgTemplate.ClsMain.FGetDimension2Caption() & " , " &
                "  Max(IG.Description) AS ItemGroupDesc, Max(IG.Description) AS ItemCategoryDesc," &
                " Max(I.ItemCategory) As ItemCategory,Max(I.ItemGroup) As ItemGroup,  " &
                " Max(I.SalesTaxPostingGroup) As SalesTaxPostingGroup,  " &
                " Max(I." & mMeasureField & ") As MeasurePerPcs,  Max(I.MeasureUnit) As MeasureUnit,  " &
                " Max(U.DecimalPlaces) as QtyDecimalPlaces, Max(U1.DecimalPlaces) as MeasureDecimalPlaces, L.Item As ItemCode, " &
                " L.Process As ProcessCode, '' As ProdOrder, '' As ProdOrderNo, '' As ProdOrderSr, " &
                " L.Dimension1 As Dimension1,  " &
                " L.Dimension2 As Dimension2 " &
                " FROM Stock L " &
                " LEFT JOIN Item I ON L.Item = I.Code " &
                " LEFT JOIN ItemGroup IG On Ig.Code = I.ItemGroup " &
                " LEFT JOIN ItemCategory Ic On Ic.Code = I.ItemCategory " &
                " LEFT JOIN Process P On L.Process = P.NCat " &
                " LEFT JOIN ProcessSequenceDetail Psd ON I.ProcessSequence = Psd.Code AND L.Process = Psd.Process " &
                " LEFT JOIN Unit U On I.Unit = U.Code " &
                " LEFT JOIN Unit U1 On I.MeasureUnit = U1.Code " &
                " LEFT JOIN Dimension1 D1 ON D1.Code = L.Dimension1  " &
                " LEFT JOIN Dimension2 D2 ON D2.Code = L.Dimension2  " &
                " Where L.LotNo Is Not Null " &
                " And IFNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') <= '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond &
                " Group By L.Item, L.LotNo, L.Process, L.Dimension1, L.Dimension2 " &
                " Having IFNull(Sum(L.Qty_Rec),0) - IFNull(Sum(L.Qty_Iss),0) > 0 " &
                " Order By LotNo, Item "
        Dgl1.AgHelpDataSet(Col1LotNo, 16) = AgL.FillData(mQry, AgL.GCn)


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

            If TxtItemDivision.Text <> "" Then
                strCond += " And  I.Div_Code = '" & TxtItemDivision.Tag & "' "
            Else
                If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemDivision")) <> "" Then
                    strCond += " And CharIndex('|' + I.Div_Code + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemDivision")) & "') > 0 "
                End If
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemSite")) <> "" Then
                strCond += " And CharIndex('|' + I.Site_Code + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemSite")) & "') > 0 "
            End If
        End If

        If RbtAllItems.Checked Then
            mQry = "SELECT I.Code AS ItemCode, I.Description AS Item, I.Unit, IG.Description AS ItemGroupDesc, Ic.Description As ItemCategoryDesc, I.ItemType, I.SalesTaxPostingGroup , " &
                    " I." & mMeasureField & " As MeasurePerPcs,  I.MeasureUnit, NULL AS LotNo, " &
                    " I.ItemGroup, I.ItemCategory, Ig.Description As ItemGroupDesc, " &
                    " U.DecimalPlaces as QtyDecimalPlaces, U1.DecimalPlaces as MeasureDecimalPlaces, " &
                    " '' As Qty, '' As Process, '' As ProcessCode, '' As ProdOrder, '' As ProdOrderNo, '' As ProdOrderSr, " &
                    " Null As Dimension1, Null As " & AgTemplate.ClsMain.FGetDimension1Caption() & ", Null As Dimension2, Null As " & AgTemplate.ClsMain.FGetDimension2Caption() & " " &
                    " FROM Item I  " &
                    " LEFT JOIN ItemGroup Ig  On I.ItemGroup = Ig.Code " &
                    " LEFT JOIN ItemCategory Ic  On I.ItemCategory = Ic.Code " &
                    " LEFT JOIN Unit U On I.Unit = U.Code " &
                    " LEFT JOIN Unit U1 On I.MeasureUnit = U1.Code " &
                    " Where IFNull(I.IsDeleted,0) = 0 " &
                    " And IFNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') <= '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
            Dgl1.AgHelpDataSet(Col1Item, 20) = AgL.FillData(mQry, AgL.GCn)
        ElseIf RbtForStock.Checked Then

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_ItemProcess")) <> "" Then
                strCond += " And CharIndex('|' + L.Process + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_ItemProcess")) & "') <= 0 "
            End If

            mQry = " SELECT L.Item AS ItemCode, Max(I.Description) As Item, L.LotNo, max(D1.Description) As " & AgTemplate.ClsMain.FGetDimension1Caption() & ", max(D2.Description) As " & AgTemplate.ClsMain.FGetDimension2Caption() & ", " &
                    " Max(P.Description) As Process,  Round(IFNull(Sum(L.Qty_Rec),0) - IFNull(Sum(L.Qty_Iss),0),4) AS Qty, Max(I.Unit) As Unit, " &
                    " Max(I.ItemGroup) As ItemGroup, Max(Ig.Description) As ItemGroupDesc, Max(I.SalesTaxPostingGroup) As SalesTaxPostingGroup,   Max(I." & mMeasureField & ") As MeasurePerPcs, " &
                    " Max(I.MeasureUnit) As MeasureUnit,   Max(U.DecimalPlaces) as QtyDecimalPlaces, Max(U1.DecimalPlaces) as MeasureDecimalPlaces,  " &
                    " Max(I.ItemCategory) As ItemCategory, Max(Ic.Description) As ItemCategoryDesc, " &
                    " L.Process As ProcessCode, '' As ProdOrder, '' As ProdOrderNo, '' As ProdOrderSr,  L.Dimension1 As Dimension1, L.Dimension2 As Dimension2 " &
                    " FROM Stock L " &
                    " LEFT JOIN Item I ON L.Item = I.Code " &
                    " LEFT JOIN ItemGroup Ig  On I.ItemGroup = Ig.Code " &
                    " LEFT JOIN ItemCategory Ic  On I.ItemCategory = Ic.Code " &
                    " LEFT JOIN Process P On L.Process = P.NCat " &
                    " LEFT JOIN ProcessSequenceDetail Psd ON I.ProcessSequence = Psd.Code AND L.process = Psd.Process  " &
                    " LEFT JOIN Unit U On I.Unit = U.Code " &
                    " LEFT JOIN Unit U1 On I.MeasureUnit = U1.Code " &
                    " LEFT JOIN Dimension1 D1 ON D1.Code = L.Dimension1  " &
                    " LEFT JOIN Dimension2 D2 ON D2.Code = L.Dimension2  " &
                    " Where IFNull(I.IsDeleted,0) = 0 AND L.Docid <> '" & mSearchCode & "' " &
                    " And IFNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') <= '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond &
                    " Group By L.Item, L.LotNo, L.Process, L.Dimension1, L.Dimension2 " &
                    " Having Round(IFNull(Sum(L.Qty_Rec),0) - IFNull(Sum(L.Qty_Iss),0),4) > 0 " &
                    " Order By Max(I.Description) "
            Dgl1.AgHelpDataSet(Col1Item, 15) = AgL.FillData(mQry, AgL.GCn)
        Else
            mQry = " SELECT Max(L.Item) As ItemCode, Max(I.Description) AS Item, " &
                    " Max(H.V_Type + '-' + H.ManualRefNo) As ProdOrderNo,   " &
                    " Max(D1.Description) As " & AgTemplate.ClsMain.FGetDimension1Caption() & ", " &
                    " Max(D2.Description) As " & AgTemplate.ClsMain.FGetDimension2Caption() & ", " &
                    " Round(IFNull(Sum(L.Qty),0) - IFNull(Max(Cd.JobOrderQty), 0),4) As Qty, Max(L.Unit) As Unit, " &
                    " Max(L.MeasurePerPcs) As MeasurePerPcs, NULL AS LotNo, " &
                    " Max(I.ItemGroup) As ItemGroup, Max(Ig.Description) As ItemGroupDesc, " &
                    " Max(I.ItemCategory) As ItemCategory, Max(Ic.Description) As ItemCategoryDesc, " &
                    " Max(L.MeasureUnit) As MeasureUnit, " &
                    " Max(U.DecimalPlaces) as QtyDecimalPlaces, Max(MU.DecimalPlaces) as MeasureDecimalPlaces, " &
                    " L.ProdOrder, L.ProdOrderSr, Null As Process, Null As ProcessCode, " &
                    " Max(L.Dimension1) As Dimension1, " &
                    " Max(L.Dimension2) As Dimension2 " &
                    " FROM (        " &
                    "    SELECT DocID, V_Type, ManualRefNo , V_Date         " &
                    "    FROM ProdOrder    " &
                    "    Where V_Date <= '" & TxtV_Date.Text & "'  " &
                    "    And Div_Code = '" & TxtDivision.Tag & "' And Site_Code = '" & TxtSite_Code.Tag & "'   " &
                    " ) H         " &
                    " LEFT JOIN ProdOrderDetail L  ON H.DocID = L.ProdOrder     " &
                    " Left Join (         " &
                    "    SELECT L.ProdOrder, L.ProdOrderSr, Sum(L.Qty) AS JobOrderQty,     " &
                    "    Sum(L.TotalMeasure) As JobOrderMeasure       " &
                    "    FROM JobOrderDetail  L      " &
                    "    LEFT JOIN JobOrder H ON L.DocId = H.DocID " &
                    "    WHERE L.DocId <> '" & mSearchCode & "'   " &
                    "    AND H.Process = '" & TxtProcess.Tag & "' " &
                    "    GROUP BY L.ProdOrder, L.ProdOrderSr       " &
                    " ) AS CD ON L.DocId = Cd.ProdOrder AND L.Sr = Cd.ProdOrderSr   " &
                    " LEFT JOIN Item I On L.Item = I.Code     " &
                    " LEFT JOIN ItemGroup Ig  On I.ItemGroup = Ig.Code " &
                    " LEFT JOIN ItemCategory Ic  On I.ItemCategory = Ic.Code " &
                    " Left Join Unit U On L.Unit = U.Code     " &
                    " Left Join Unit MU On L.MeasureUnit = MU.Code " &
                    " Left Join Dimension1 D1 On L.Dimension1 = D1.Code " &
                    " Left Join Dimension2 D2 On L.Dimension2 = D2.Code " &
                    " Where L.Process = '" & TxtProcess.Tag & "' " &
                    " GROUP BY L.ProdOrder, L.ProdOrderSr     " &
                    " HAVING Round(IFNull(Sum(L.Qty),0) - IFNull(Max(Cd.JobOrderQty), 0),4) > 0 "
            Dgl1.AgHelpDataSet(Col1Item, 15) = AgL.FillData(mQry, AgL.GCn)
        End If
    End Sub

    Private Sub FCreateHelpDimension1()
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

            If TxtItemDivision.Text <> "" Then
                strCond += " And  I.Div_Code = '" & TxtItemDivision.Tag & "' "
            Else
                If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemDivision")) <> "" Then
                    strCond += " And CharIndex('|' + I.Div_Code + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemDivision")) & "') > 0 "
                End If
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemSite")) <> "" Then
                strCond += " And CharIndex('|' + I.Site_Code + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemSite")) & "') > 0 "
            End If
        End If

        If RbtForStock.Checked Then
            mQry = " SELECT L.Dimension1 As Dimension1, max(D1.Description) As " & AgTemplate.ClsMain.FGetDimension1Caption() & ", Max(I.Description) As Item, L.LotNo, max(D2.Description) As " & AgTemplate.ClsMain.FGetDimension2Caption() & ", " &
                    " Max(P.Description) As Process,  Round(IFNull(Sum(L.Qty_Rec),0) - IFNull(Sum(L.Qty_Iss),0),4) AS Qty, Max(I.Unit) As Unit, " &
                    " Max(I.ItemGroup) As ItemGroup, Max(Ig.Description) As ItemGroupDesc, Max(I.SalesTaxPostingGroup) As SalesTaxPostingGroup,   Max(I." & mMeasureField & ") As MeasurePerPcs, " &
                    " Max(I.MeasureUnit) As MeasureUnit,   Max(U.DecimalPlaces) as QtyDecimalPlaces, Max(U1.DecimalPlaces) as MeasureDecimalPlaces,  " &
                    " Max(I.ItemCategory) As ItemCategory, Max(Ic.Description) As ItemCategoryDesc, " &
                    " L.Process As ProcessCode, '' As ProdOrder, '' As ProdOrderNo, '' As ProdOrderSr,   L.Item AS ItemCode, L.Dimension2 As Dimension2 " &
                    " FROM Stock L " &
                    " LEFT JOIN Item I ON L.Item = I.Code " &
                    " LEFT JOIN ItemGroup Ig  On I.ItemGroup = Ig.Code " &
                    " LEFT JOIN ItemCategory Ic  On I.ItemCategory = Ic.Code " &
                    " LEFT JOIN Process P On L.Process = P.NCat " &
                    " LEFT JOIN ProcessSequenceDetail Psd ON I.ProcessSequence = Psd.Code AND L.process = Psd.Process  " &
                    " LEFT JOIN Unit U On I.Unit = U.Code " &
                    " LEFT JOIN Unit U1 On I.MeasureUnit = U1.Code " &
                    " LEFT JOIN Dimension1 D1 ON D1.Code = L.Dimension1  " &
                    " LEFT JOIN Dimension2 D2 ON D2.Code = L.Dimension2  " &
                    " Where IFNull(I.IsDeleted,0) = 0 AND L.Docid <> '" & mSearchCode & "' " &
                    " And IFNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') <= '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond &
                    " Group By L.Item, L.LotNo, L.Process, L.Dimension1, L.Dimension2 " &
                    " Having Round(IFNull(Sum(L.Qty_Rec),0) - IFNull(Sum(L.Qty_Iss),0),4) > 0 " &
                    " Order By Max(I.Description) "
            Dgl1.AgHelpDataSet(Col1Dimension1, 15) = AgL.FillData(mQry, AgL.GCn)
        Else
            mQry = " SELECT Code, Description  FROM Dimension1  "
            Dgl1.AgHelpDataSet(Col1Dimension1) = AgL.FillData(mQry, AgL.GCn)
        End If
    End Sub

    Private Sub FCreateHelpDimension2()
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

            If TxtItemDivision.Text <> "" Then
                strCond += " And  I.Div_Code = '" & TxtItemDivision.Tag & "' "
            Else
                If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemDivision")) <> "" Then
                    strCond += " And CharIndex('|' + I.Div_Code + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemDivision")) & "') > 0 "
                End If
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemSite")) <> "" Then
                strCond += " And CharIndex('|' + I.Site_Code + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemSite")) & "') > 0 "
            End If
        End If

        If RbtForStock.Checked Then
            mQry = " SELECT L.Dimension2 As Dimension2, max(D2.Description) As " & AgTemplate.ClsMain.FGetDimension2Caption() & ", Max(I.Description) As Item, L.LotNo, max(D1.Description) As " & AgTemplate.ClsMain.FGetDimension1Caption() & ", " &
                    " Max(P.Description) As Process,  Round(IFNull(Sum(L.Qty_Rec),0) - IFNull(Sum(L.Qty_Iss),0),4) AS Qty, Max(I.Unit) As Unit, " &
                    " Max(I.ItemGroup) As ItemGroup, Max(Ig.Description) As ItemGroupDesc, Max(I.SalesTaxPostingGroup) As SalesTaxPostingGroup,   Max(I." & mMeasureField & ") As MeasurePerPcs, " &
                    " Max(I.MeasureUnit) As MeasureUnit,   Max(U.DecimalPlaces) as QtyDecimalPlaces, Max(U1.DecimalPlaces) as MeasureDecimalPlaces,  " &
                    " Max(I.ItemCategory) As ItemCategory, Max(Ic.Description) As ItemCategoryDesc, " &
                    " L.Process As ProcessCode, '' As ProdOrder, '' As ProdOrderNo, '' As ProdOrderSr,   L.Item AS ItemCode, L.Dimension1 As Dimension1 " &
                    " FROM Stock L " &
                    " LEFT JOIN Item I ON L.Item = I.Code " &
                    " LEFT JOIN ItemGroup Ig  On I.ItemGroup = Ig.Code " &
                    " LEFT JOIN ItemCategory Ic  On I.ItemCategory = Ic.Code " &
                    " LEFT JOIN Process P On L.Process = P.NCat " &
                    " LEFT JOIN ProcessSequenceDetail Psd ON I.ProcessSequence = Psd.Code AND L.process = Psd.Process  " &
                    " LEFT JOIN Unit U On I.Unit = U.Code " &
                    " LEFT JOIN Unit U1 On I.MeasureUnit = U1.Code " &
                    " LEFT JOIN Dimension1 D1 ON D1.Code = L.Dimension1  " &
                    " LEFT JOIN Dimension2 D2 ON D2.Code = L.Dimension2  " &
                    " Where IFNull(I.IsDeleted,0) = 0 AND L.Docid <> '" & mSearchCode & "' " &
                    " And IFNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') <= '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond &
                    " Group By L.Item, L.LotNo, L.Process, L.Dimension1, L.Dimension2 " &
                    " Having Round(IFNull(Sum(L.Qty_Rec),0) - IFNull(Sum(L.Qty_Iss),0),4) > 0 " &
                    " Order By Max(I.Description) "
            Dgl1.AgHelpDataSet(Col1Dimension2, 15) = AgL.FillData(mQry, AgL.GCn)
        Else
            mQry = " SELECT Code, Description  FROM Dimension2  "
            Dgl1.AgHelpDataSet(Col1Dimension2) = AgL.FillData(mQry, AgL.GCn)
        End If
    End Sub

    Private Sub FCreateHelpFromProcess()
        Dim strCond As String = ""

        'If Dgl1.Item(Col1Item, Dgl1.CurrentCell.RowIndex).Value <> "" Then
        '    If AgL.VNull(AgL.Dman_Execute(" Select IFNull(IsRequired_LotNo,0) As IsRequired_LotNo " & _
        '                                  " From ItemSiteDetail Where Code = '" & Dgl1.Item(Col1Item, Dgl1.CurrentCell.RowIndex).Tag & "' " & _
        '                                  " And Site_Code = '" & AgL.PubSiteCode & "'", AgL.GCn).ExecuteScalar) = 0 Then
        '        Dgl1.AgHelpDataSet(Col1LotNo) = Nothing
        '        Exit Sub
        '    End If
        'End If


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

            If TxtItemDivision.Text <> "" Then
                strCond += " And  I.Div_Code = '" & TxtItemDivision.Tag & "' "
            Else
                If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemDivision")) <> "" Then
                    strCond += " And CharIndex('|' + I.Div_Code + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemDivision")) & "') > 0 "
                End If
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemSite")) <> "" Then
                strCond += " And CharIndex('|' + I.Site_Code + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemSite")) & "') > 0 "
            End If
        End If

        If Dgl1.Item(Col1Item, Dgl1.CurrentCell.RowIndex).Value <> "" Then
            strCond += " And L.Item = '" & Dgl1.Item(Col1Item, Dgl1.CurrentCell.RowIndex).Tag & "'"
        End If

        mQry = " SELECT L.Process As Code, Max(P.Description) As Process, Max(L.LotNo) As LotNo, Max(I.Description) As ItemDesc,   " &
                " IFNull(Sum(L.Qty_Rec),0) - IFNull(Sum(L.Qty_Iss),0) AS Qty, Max(I.Unit) As Unit, " &
                " Max(I.ItemGroup) As ItemGroup,  Max(IG.Description) AS ItemGroupDesc, " &
                " Max(I.ItemCategory) As ItemCategory,  Max(IG.Description) AS ItemCategoryDesc, " &
                " Max(I.SalesTaxPostingGroup) As SalesTaxPostingGroup,  " &
                " Max(I." & mMeasureField & ") As MeasurePerPcs,  Max(I.MeasureUnit) As MeasureUnit,  " &
                " Max(U.DecimalPlaces) as QtyDecimalPlaces, Max(U1.DecimalPlaces) as MeasureDecimalPlaces, L.Item As ItemCode, " &
                " L.Process As ProcessCode, '' As ProdOrder, '' As ProdOrderNo, '' As ProdOrderSr, " &
                " Null As Dimension1, Null As " & AgTemplate.ClsMain.FGetDimension1Caption() & ", " &
                " Null As Dimension2, Null As " & AgTemplate.ClsMain.FGetDimension2Caption() & " " &
                " FROM Stock L " &
                " LEFT JOIN Item I ON L.Item = I.Code " &
                " LEFT JOIN ItemGroup IG On Ig.Code = I.ItemGroup " &
                " LEFT JOIN ItemCategory Ic On Ic.Code = I.ItemCategory " &
                " LEFT JOIN Process P On L.Process = P.NCat " &
                " LEFT JOIN ProcessSequenceDetail Psd ON I.ProcessSequence = Psd.Code AND L.Process = Psd.Process " &
                " LEFT JOIN Unit U On I.Unit = U.Code " &
                " LEFT JOIN Unit U1 On I.MeasureUnit = U1.Code " &
                " Where L.Process Is Not Null " &
                " And IFNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') <= '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond &
                " Group By L.Item, L.LotNo, L.Process " &
                " Having IFNull(Sum(L.Qty_Rec),0) - IFNull(Sum(L.Qty_Iss),0) > 0 " &
                " Order By L.Process, L.Item, LotNo "
        Dgl1.AgHelpDataSet(Col1FromProcess, 14) = AgL.FillData(mQry, AgL.GCn)
    End Sub

    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub

    Private Sub Validating_Item_Uid(ByVal Item_Uid As String, ByVal mRow As Integer)
        Dim DsTemp As DataSet = Nothing
        Dim DtTemp As DataTable = Nothing
        Dim ErrMsgStr$ = ""

        Try
            mQry = "  SELECT Iu.Code As Item_UidCode, Iu.Item_UID , Iu.Item AS ItemCode, I.Description AS Item, " &
                    " I.Unit, Iu.ProdOrder, Po.ManualRefNo As ProdOrderNo, " &
                    " I.ItemGroup, IG.Description AS ItemGroupDesc, " &
                    " I.ItemCategory, Ic.Description AS ItemCategoryDesc, " &
                    " I.MeasureUnit, U.DecimalPlaces as QtyDecimalPlaces, MU.DecimalPlaces as MeasureDecimalPlaces, " &
                    " Iu.ProdOrder, Po.ManualRefNo As ProdOrderNo, " &
                    " I." & mMeasureField & " As MeasurePerPcs " &
                    " FROM Item_UID Iu  " &
                    " LEFT JOIN Item I ON I.Code = Iu.Item   " &
                    " LEFT JOIN ItemGroup IG ON IG.Code = I.ItemGroup " &
                    " LEFT JOIN ItemCategory Ic ON Ic.Code = I.ItemCategory " &
                    " LEFT JOIN ProdOrder PO ON PO.DocID = Iu.ProdOrder " &
                    " Left Join Unit U  On I.Unit = U.Code " &
                    " Left Join Unit MU  On I.MeasureUnit = MU.Code " &
                    " WHERE Iu.Item_UID = '" & Item_Uid & "' "
            DsTemp = AgL.FillData(mQry, AgL.GCn)
            With DsTemp.Tables(0)
                If .Rows.Count > 0 Then
                    Dgl1.Item(Col1Item_Uid, mRow).Tag = AgL.XNull(.Rows(0)("Item_UidCode"))

                    Dgl1.Item(Col1Item, mRow).Tag = AgL.XNull(.Rows(0)("ItemCode"))
                    Dgl1.Item(Col1Item, mRow).Value = AgL.XNull(.Rows(0)("Item"))
                    'Dgl1.Item(Col1ProdOrder, mRow).Tag = AgL.XNull(.Rows(0)("ProdOrder"))
                    'Dgl1.Item(Col1ProdOrder, mRow).Value = AgL.XNull(.Rows(0)("ProdOrderNo"))


                    Dgl1.Item(Col1ItemGroup, mRow).Tag = AgL.XNull(.Rows(0)("ItemGroup"))
                    Dgl1.Item(Col1ItemGroup, mRow).Value = AgL.XNull(.Rows(0)("ItemGroupDesc"))

                    Dgl1.Item(Col1ItemCategory, mRow).Tag = AgL.XNull(.Rows(0)("ItemCategory"))
                    Dgl1.Item(Col1ItemCategory, mRow).Value = AgL.XNull(.Rows(0)("ItemCategoryDesc"))


                    Dgl1.Item(Col1Qty, mRow).Value = 1
                    Dgl1.Item(Col1Unit, mRow).Value = AgL.XNull(.Rows(0)("Unit"))

                    Dgl1.Item(Col1QtyDecimalPlaces, mRow).Value = AgL.VNull(.Rows(0)("QtyDecimalPlaces"))

                    Dgl1.Item(Col1MeasurePerPcs, mRow).Value = AgL.VNull(.Rows(0)("MeasurePerPcs"))

                    Dgl1.Item(Col1MeasureUnit, mRow).Value = AgL.XNull(.Rows(0)("MeasureUnit"))
                    Dgl1.Item(Col1MeasureDecimalPlaces, mRow).Value = AgL.VNull(.Rows(0)("MeasureDecimalPlaces"))

                    'Dgl1.Item(Col1Rate, mRow).Value = FGetJobRate(TxtProcess.Tag, TxtJobWorker.Tag, Dgl1.Item(Col1Item, mRow).Tag)
                    If AgL.StrCmp(AgL.PubCompShortName, "Surya") Then
                        Dgl1.Item(Col1Rate, mRow).Value = ClsMain.FGetJobRate(mJobRateHelpDataSet, Val(TxtRate.Text),
                                TxtProcess.Tag, TxtInsideOutside.Text, TxtV_Date.Text,
                                Dgl1.Item(Col1Item, mRow).Tag, Dgl1.Item(Col1ItemGroup, mRow).Tag,
                                Dgl1.Item(Col1ItemCategory, mRow).Tag, Val(Dgl1.Item(Col1MeasurePerPcs, mRow).Value))

                        Dgl1.Item(Col1IncentiveRate, mRow).Value = ClsMain.FGetJobIncentiveRate(mJobRateHelpDataSet,
                               TxtProcess.Tag, TxtInsideOutside.Text, TxtV_Date.Text,
                               Dgl1.Item(Col1Item, mRow).Tag, Dgl1.Item(Col1ItemGroup, mRow).Tag,
                               Dgl1.Item(Col1ItemCategory, mRow).Tag, Val(Dgl1.Item(Col1MeasurePerPcs, mRow).Value))
                    End If


                    mQry = " SELECT  P.NCat As ProcessCode, P.Description As ProcessDesc " &
                            " FROM JobIssRecUID L  " &
                            " LEFT JOIN Process P ON L.Process = P.NCat " &
                            " WHERE Item_UID = '" & Dgl1.Item(Col1Item_Uid, mRow).Tag & "' " &
                            " ORDER BY L.V_Date DESC, P.Sr DESC Limit 1"
                    Dim DtProcess As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
                    If DtProcess.Rows.Count > 0 Then
                        Dgl1.Item(Col1FromProcess, mRow).Tag = AgL.XNull(DtProcess.Rows(0)("ProcessCode"))
                        Dgl1.Item(Col1FromProcess, mRow).Value = AgL.XNull(DtProcess.Rows(0)("ProcessDesc"))
                    End If

                    'mQry = " SELECT TOP 1 L.Process FROM StockProcess L WHERE L.Item_UID = '" & Dgl1.Item(Col1Item_Uid, mRow).Tag & "' And DocId <> '" & mSearchCode & "' And IFNull(L.Qty_Iss,0) > 0 ORDER BY L.V_Date DESC "
                    'Dgl1.Item(Col1FromProcess, mRow).Value = AgL.XNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar)

                    Dgl1.Item(Col1V_Nature, mRow).Value = RbtForStock.Text
                Else
                    MsgBox("Invalid Item UID !")
                    Dgl1.Item(Col1Item_Uid, mRow).Value = ""
                End If
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Function FCheck_Item_UID(ByVal Item_UID As String) As String
        Dim Item_UidCode$ = "", ErrMsgStr$ = ""
        Dim DtTemp As DataTable = Nothing
        'Dim mProcessSequence$ = ""
        'Dim mProcessIterationsAllowed As Integer = 0

        mQry = " SELECT Code FROM Item_UID  WHERE Item_UID = '" & Item_UID & "'"
        Item_UidCode = AgL.XNull(AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar)
        If Item_UidCode = "" Then
            FCheck_Item_UID = "Carpet Id Is Not Valid."
            Exit Function
        Else
            FCheck_Item_UID = ""
        End If

        'mQry = "Select ProcessSequence, " & _
        '        "       (Select Count(*) from ProcessSequenceDetail " & _
        '        "        Where Code = H.ProcessSequence And Process = '" & TxtProcess.Tag & "') As IterationsAllowed " & _
        '        " From Item H Where Code = (Select Item From Item_Uid Where Code = '" & Item_UidCode & "') "
        'DtTemp = AgL.FillData(mQry, AgL.GcnRead).tables(0)
        'If DtTemp.Rows.Count > 0 Then
        '    mProcessSequence = AgL.XNull(DtTemp.Rows(0)("ProcessSequence"))
        '    mProcessIterationsAllowed = AgL.VNull(DtTemp.Rows(0)("IterationsAllowed"))
        'End If


        'If mProcessSequence <> "" Then
        '    If Val(mProcessIterationsAllowed) > 0 Then
        '        mQry = "Select IFNull(Count(*),0) from JobIssRecUID " & _
        '                " Where IssRec='I' And Process = '" & TxtProcess.Tag & "' " & _
        '                " And Item_UID = '" & Item_UidCode & "' " & _
        '                " And DocID <> '" & mSearchCode & "'  "
        '        If AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar + 1 > Val(mProcessIterationsAllowed) Then
        '            If MsgBox("Carpet Id " & Item_UID & " has already completed this process.Do you want to issue it again", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2) = MsgBoxResult.No Then
        '                FCheck_Item_UID = "Carpet Id " & Item_UID & " has already completed this process"
        '                Exit Function
        '            Else
        '                FCheck_Item_UID = ""
        '            End If
        '        End If
        '    End If
        'End If



        mQry = " Select Iu.Item_Uid From Item_Uid Iu LEFT JOIN Item I ON Iu.Item = I.Code Where Iu.Code = '" & Item_UidCode & "' And I.Div_Code <> '" & IIf(TxtItemDivision.Text <> "", TxtItemDivision.Tag, AgL.PubDivCode) & "'"
        DtTemp = AgL.FillData(mQry, AgL.GcnRead).Tables(0)
        If DtTemp.Rows.Count > 0 Then
            FCheck_Item_UID = "Carpet Id " & AgL.XNull(DtTemp.Rows(0)("Item_Uid")) & " Does Not Belong To " & IIf(TxtItemDivision.Text <> "", TxtItemDivision.Text, AgL.PubDivName) & "."
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

        mQry = "Select IFNull(Count(*),0) from JobIssRecUID " &
                " Where IssRec='I' And Process = '" & TxtProcess.Tag & "' " &
                " And Item_UID = '" & Item_UidCode & "' " &
                " And DocID <> '" & mSearchCode & "'  "
        If AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar > 0 Then
            If MsgBox("Carpet Id " & Item_UID & " has already completed this process.Do you want to issue it again", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2) = MsgBoxResult.No Then
                FCheck_Item_UID = "Carpet Id " & Item_UID & " has already completed this process"
                Exit Function
            Else
                FCheck_Item_UID = ""
            End If
        End If

        'mQry = "SELECT IFNull(charindex ('" & TxtProcess.Tag & "',Item_Uid.ProcessesDone ),0) AS ProcessDone FROM Item_UID " & _
        '       " Where Code  = '" & Item_UidCode & "' "
        'If AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar > 0 Then
        '    If MsgBox("Carpet Id " & Item_UID & " has already completed this process.Do you want to issue it again", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2) = MsgBoxResult.No Then
        '        FCheck_Item_UID = "Carpet Id " & Item_UID & " has already completed this process"
        '        Exit Function
        '    Else
        '        FCheck_Item_UID = ""
        '    End If
        'End If

        mQry = " Select Item_Uid, ClosedRemark From Item_Uid  " &
              " Where Code = '" & Item_UidCode & "' " &
              " And IFNull(IsClosed,0) = 1 "
        DtTemp = AgL.FillData(mQry, AgL.GcnRead).Tables(0)

        If DtTemp.Rows.Count > 0 Then
            'FCheck_Item_UID = "Carpet Id " & Item_UID & " Is Not Packed."
            FCheck_Item_UID = "Carpet Id " & Item_UID & " Is " & AgL.XNull(DtTemp.Rows(0)("ClosedRemark"))
            Exit Function
        Else
            FCheck_Item_UID = ""
        End If

        mQry = "SELECT Count(I.DocID) " &
               " FROM (SELECT DocID, Item_UID, Site_Code FROM JobIssRecUID  " &
               " WHERE Item_UID ='" & Item_UidCode & "' And IssRec= 'I') I " &
               " LEFT JOIN JobIssRecUID R  ON I.DocID = R.JobRecDocID AND I.Item_UID = R.Item_UID  " &
               " WHERE R.DocID IS NULL AND I.DocID <> '" & mSearchCode & "' And I.Site_Code = '" & AgL.PubSiteCode & "'"
        If AgL.VNull(AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar) > 0 Then
            mQry = "SELECT Sg.Name, H.ManualRefNo, H.V_Date, Vc.NCatDescription AS ProcessDesc " &
                    " FROM (SELECT DocID, Item_UID, Site_Code  FROM JobIssRecUID  " &
                    " WHERE Item_UID ='" & Item_UidCode & "' And IssRec='I') I " &
                    " LEFT JOIN JobIssRecUID R  ON I.DocID = R.JobRecDocID AND I.Item_UID = R.Item_UID  " &
                    " LEFT JOIN JobOrder H  ON I.DocID = H.DocID " &
                    " LEFT JOIN SubGroup Sg  ON H.JobWorker = Sg.SubCode " &
                    " LEFT JOIN VoucherCat Vc  ON H.Process = Vc.NCat " &
                    " WHERE R.DocID IS NULL AND I.DocID <> '" & mSearchCode & "' And I.Site_Code = '" & AgL.PubSiteCode & "' " &
                    " ORDER BY H.V_Date Desc Limit 1"
            DtTemp = AgL.FillData(mQry, AgL.GcnRead).Tables(0)
            FCheck_Item_UID = "Carpet Id " & Item_UID & " Is Already Issued To " & AgL.XNull(DtTemp.Rows(0)("Name")) & " For " & AgL.XNull(DtTemp.Rows(0)("ProcessDesc")) & " On Date " & AgL.XNull(DtTemp.Rows(0)("V_Date")) & " Against Ref No " & AgL.XNull(DtTemp.Rows(0)("ManualRefNo")) & "."
            Exit Function
        Else
            FCheck_Item_UID = ""
        End If
    End Function

    Private Sub RbtAllItems_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RbtAllItems.Click, RbtForStock.Click, RbtForProdOrder.Click
        Dgl1.AgHelpDataSet(Col1Item) = Nothing
        Dgl1.AgHelpDataSet(Col1Dimension1) = Nothing
        Dgl1.AgHelpDataSet(Col1Dimension2) = Nothing
    End Sub

    Private Sub TxtProcess_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtProcess.Validating
        If Dgl1.AgHelpDataSet(Col1Item) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1Item).Dispose() : Dgl1.AgHelpDataSet(Col1Item) = Nothing
        If TxtJobWorker.AgHelpDataSet IsNot Nothing Then TxtJobWorker.AgHelpDataSet.Dispose() : TxtJobWorker.AgHelpDataSet = Nothing
        If AgL.StrCmp(Topctrl1.Mode, "Add") Then Call ProcFillJobValues()
    End Sub

    Private Sub BtnImprtFromText_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnImprtFromText.Click
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

        If mJobRateHelpDataSet Is Nothing Then mJobRateHelpDataSet = FGetJobRateHelpDataSet()

        ImportMessegeStr = ""
        ImportMode = True

        Opn.ShowDialog()

        If Opn.FileName = "" Then Exit Sub

        mItemDivisionCode = TxtItemDivision.Tag
        mItemDivisionText = TxtItemDivision.Text

        Sr = New StreamReader(Opn.FileName)

        StrMessage = ""

        StrQry = "  Declare @TmpTable as Table " &
                    " ( " &
                    " Process nVarchar(10), " &
                    " IssRec nVarchar(10), " &
                    " JobWorker nVarchar(10), " &
                    " OrderBy nVarchar(10), " &
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

                DtTemp = AgL.FillData("Select SubCode From SubGroup  Where ManualCode = '" & mJobWorker & "'  And CharIndex('|' + '" & AgL.PubDivCode & "' + '|', DivisionList) > 0  and Site_Code = '" & AgL.PubSiteCode & "'", AgL.GcnRead).Tables(0)
                If DtTemp.Rows.Count > 0 Then
                    mJobWorker = DtTemp.Rows(0)("SubCode")
                Else
                    If StrMessage <> "" Then StrMessage += vbCrLf
                    StrMessage += "Invalid Value Found in JobWorker Field at Row No. " & I
                End If


                'For Checking that this job worker has permission for this process...
                DtTemp = AgL.FillData("Select J.SubCode From JobWorkerProcess J  Where J.SubCode = '" & mJobWorker & "'  And J.Process = '" & TxtProcess.Tag & "'", AgL.GcnRead).Tables(0)
                If DtTemp.Rows.Count = 0 Then
                    If StrMessage <> "" Then StrMessage += vbCrLf
                    StrMessage += "JobWorker Code " & strArr(9) & " at Row No. " & I & " is not permitted for " & TxtProcess.Text & ""
                End If



                'and Div_Code = '" & AgL.PubDivCode & "'
                DtTemp = AgL.FillData("Select SubCode From SubGroup  Where ManualCode = '" & mJobRecBy & "'  and Site_Code = '" & AgL.PubSiteCode & "'", AgL.GcnRead).Tables(0)
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
                    DtTemp = AgL.FillData("Select Item_Uid.Code, Item_Uid.Item, Item." & mMeasureField & " As Measure From Item_UID LEFT JOIN Item On Item_Uid.Item = Item.Code Where Item_Uid.Item_UID = '" & mBarcode & "' ", AgL.GCn).Tables(0)
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
                    StrQry += " Values (" & AgL.Chk_Text(mProcess) & ", " & AgL.Chk_Text(mIssRec) & ", " &
                                " " & AgL.Chk_Text(mJobWorker) & ", " & AgL.Chk_Text(mJobRecBy) & ", " &
                                " " & AgL.Chk_Text(mBarcode) & ", " & AgL.Chk_Text(mSKU) & ", " & AgL.Chk_Text(mMeasurePerPcs) & ") "
                Else
                    ImportMessegeStr += Item_UidError & vbCrLf
                End If

            End If
        Loop Until Line Is Nothing
        Sr.Close()


        mQry = StrQry & " Select Process, IssRec, JobWorker, OrderBy " &
                " From @TmpTable " &
                " Where Process = '" & mProcess & "' And IssRec = 'I' " &
                " Group by Process, IssRec, JobWorker, OrderBy "
        DtTemp = AgL.FillData(mQry, AgL.GcnRead).tables(0)
        Dim DueDays As Double = 0
        For I = 0 To DtTemp.Rows.Count - 1
            If I > 0 Then Topctrl1.FButtonClick(0)

            Dgl1.Focus()

            TxtProcess.Tag = mProcess
            TxtProcess.Text = AgL.XNull(AgL.Dman_Execute("Select Description From Process Where NCat = '" & TxtProcess.Tag & "' ", AgL.GCn).ExecuteScalar)

            TxtOrderBy.Tag = DtTemp.Rows(I)("OrderBy")
            TxtOrderBy.Text = AgL.XNull(AgL.Dman_Execute("Select Name From SubGroup Sg Where SubCode = '" & TxtOrderBy.Tag & "'", AgL.GCn).ExecuteScalar)

            TxtJobWorker.Tag = DtTemp.Rows(I)("JobWorker")
            TxtJobWorker.Text = AgL.XNull(AgL.Dman_Execute("Select Name From SubGroup Sg Where SubCode = '" & TxtJobWorker.Tag & "'", AgL.GCn).ExecuteScalar)

            TxtGodown.Tag = PubDefaultGodownCode
            TxtGodown.Text = PubDefaultGodownName

            If TxtV_Date.Text <> "" And TxtDueDate.Text = "" And AgL.PubDtEnviro.Rows.Count > 0 Then
                TxtDueDate.Text = DateAdd(DateInterval.Day, AgL.VNull(AgL.PubDtEnviro.Rows(0)("DefaultDueDays")), CDate(TxtV_Date.Text))
                mQry = "Select DefaultDueDays from Process Where NCat= '" & mProcess & "'  "
                DueDays = AgL.VNull(AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar)
                If DueDays > 0 Then
                    TxtDueDate.Text = DateAdd(DateInterval.Day, DueDays, CDate(TxtV_Date.Text))
                End If
            End If

            If mItemDivisionCode <> "" Then TxtItemDivision.Tag = mItemDivisionCode
            If mItemDivisionText <> "" Then TxtItemDivision.Text = mItemDivisionText

            mJobRateHelpDataSet = FGetJobRateHelpDataSet()

            ProcFillJobValues()

            TxtInsideOutside.Text = AgL.XNull(AgL.Dman_Execute("Select InsideOutside From JobWorker Where SubCode = '" & TxtJobWorker.Tag & "'", AgL.GCn).ExecuteScalar)

            mQry = StrQry & " Select Process, Sku, BarCode, Max(MeasurePerPcs) As MeasurePerPcs From @TmpTable " &
                    " Where Process = '" & TxtProcess.Tag & "' And Jobworker = '" & TxtJobWorker.Tag & "' " &
                    " Group By Process, Sku, BarCode " &
                    " Order By MeasurePerPcs, Sku "
            DtLineRec = AgL.FillData(mQry, AgL.GcnRead).Tables(0)

            For J = 0 To DtLineRec.Rows.Count - 1
                Dgl1.Rows.Add()
                Dgl1.Item(ColSNo, Dgl1.Rows.Count - 2).Value = Dgl1.Rows.Count - 1
                Dgl1.Item(Col1Item_Uid, Dgl1.Rows.Count - 2).Tag = DtLineRec.Rows(J)("BarCode")
                Dgl1.Item(Col1Item_Uid, Dgl1.Rows.Count - 2).Value = AgL.XNull(AgL.Dman_Execute("Select Item_Uid From Item_Uid Where Code = '" & DtLineRec.Rows(J)("BarCode") & "'", AgL.GCn).ExecuteScalar)

                'ImportMessegeStr = FCheck_Item_UID(Dgl1.Item(Col1Item_Uid, Dgl1.Rows.Count - 2).Tag, Dgl1.Rows.Count - 2)
                Validating_Item_Uid(Dgl1.Item(Col1Item_Uid, Dgl1.Rows.Count - 2).Value, Dgl1.Rows.Count - 2)
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

    Private Sub FPostInJobIssRecUID(ByVal SearchCode As String, ByVal Conn As SQLiteConnection, ByVal Cmd As SQLiteCommand)
        Dim I As Integer = 0, bSr As Integer = 0

        mQry = "Delete from JobIssRecUID Where DocId ='" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " INSERT INTO JobIssRecUID(DocID, TSr, Sr, IssRec, Process, Item, Item_UID, " &
                 " Godown, Site_Code, V_Date, V_Type, SubCode, Div_Code, RecId, EntryDate, Remark) " &
                 " Select L.DocId, L.Sr As TSr, L.Sr, 'I', " &
                 " H.Process, L.Item, L.Item_Uid, " &
                 " H.Godown, H.Site_Code, H.V_Date, H.V_Type, H.JobWorker, H.Div_Code, H.ManualRefNo, H.EntryDate, " &
                 " SubString(IFNull(H.Remarks,'') + '.' + IFNull(L.Remark,''),0,255) " &
                 " From (Select * From JobOrderDetail  Where DocId = '" & mSearchCode & "' And Item_Uid Is Not Null) As L " &
                 " LEFT JOIN JobOrder H  On L.DocId = H.DocId "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    End Sub

    Private Sub Dgl1_CellContentClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellContentClick
        Dim Mdi As MDIMain = New MDIMain
        Try
            Select Case Dgl1.Columns(e.ColumnIndex).Name
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
        mQry = " SELECT H.V_Date, H.V_Type + '-' + H.ManualRefNo As ManualRefNo, H.DueDate, H.Remarks, P.Description AS FromProcessDesc, " &
                " H.JobInstructions, H.TermsAndConditions,   H.EntryBy, H.EntryDate, H.ApproveBy, H.ApproveDate, H.InsideOutside, " &
                " H.TimeIncentive, H.TimePenalty, H.TimePenaltyDays, " &
                " L.Qty, L.Unit, L.MeasurePerPcs, L.TotalMeasure, L.MeasureUnit, L.Rate, L.LotNo, L.Amount, L.PerimeterPerPcs, L.TotalPerimeter, L.Perimeter, " &
                " L.Remark As LineRemark,   L.Item_Uid, Sg.Name AS JobWorkerName,  Sg.Add1, Sg.Add2, Sg.Add3, Sg.Mobile, Sg.PAN, H.Freight, " &
                " Sg1.DispName AS OrderByName, G.Description AS GodownDesc,  I.Description AS ItemDesc, I.Specification AS ItemSpecification, U.DecimalPlaces, " &
                " D1.Description AS D1Desc, D2.Description AS D2Desc, E.Caption_Dimension1, E.Caption_Dimension2, " &
                " Iu.Item_Uid As Item_UidDesc, Div.Div_Name, Ig.Description As ItemGroupDesc   " &
                " FROM JobOrder H  " &
                " LEFT JOIN JobOrderDetail L ON H.DocID = L.DocId   " &
                " LEFT JOIN SubGroup Sg ON H.JobWorker = Sg.SubCode  " &
                " LEFT JOIN SubGroup Sg1 ON H.OrderBy = Sg1.SubCode  " &
                " LEFT JOIN Godown G ON H.Godown = G.Code  " &
                " LEFT JOIN Item I ON L.Item = I.Code  " &
                " LEFT JOIN Item_Uid Iu ON L.Item_Uid = Iu.Code   " &
                " LEFT JOIN ItemGroup Ig ON I.ItemGroup = Ig.Code  " &
                " LEFT JOIN Division Div On H.Div_Code = Div.Div_Code   " &
                " LEFT JOIN Enviro E ON E.Site_Code = H.Site_Code AND E.Div_Code = H.Div_Code " &
                " LEFT JOIN Dimension1 D1 ON D1.Code = L.Dimension1 " &
                " LEFT JOIN Dimension2 D2 ON D2.Code = L.Dimension2 " &
                " LEFT JOIN Unit U ON L.Unit = U.Code  " &
                " LEFT JOIN Process P ON L.FromProcess = P.NCat  " &
                " WHERE H.DocID =  '" & mSearchCode & "'  Order By L.Sr "
        ClsMain.FPrintThisDocument(Me, TxtV_Type.Tag, mQry, "Trade_JobOrderPrint", "Job Order For " & TxtProcess.Text)
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

        mQry = " Select Iu.Item_Uid From Item_Uid Iu LEFT JOIN Item I ON Iu.Item = I.Code Where Iu.Code In (" & mItem_UidStr & ") And I.Div_Code <> '" & IIf(TxtItemDivision.Text <> "", TxtItemDivision.Tag, AgL.PubDivCode) & "'"
        DtTemp = AgL.FillData(mQry, AgL.GcnRead).Tables(0)

        If DtTemp.Rows.Count > 0 Then
            For I = 0 To DtTemp.Rows.Count - 1
                MsgStr += "Carpet Id " & AgL.XNull(DtTemp.Rows(I)("Item_Uid")) & " Does Not Belong To " & IIf(TxtItemDivision.Text <> "", TxtItemDivision.Text, AgL.PubDivName) & "."
            Next
        End If

        'mQry = " Select Iu.Item_Uid " & _
        '            " From StockProcess L " & _
        '            " LEFT JOIN Item_Uid Iu On L.Item_Uid = Iu.Code " & _
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
                MsgStr += "Carpet Id " & AgL.XNull(DtTemp.Rows(I)("Item_Uid")) & " Is Not Received From Weaving Process."
            Next
        End If

        mQry = " Select Item_Uid, ClosedRemark From Item_Uid  " &
                " Where Code In (" & mItem_UidStr & ") " &
                " And IFNull(IsClosed,0) = 1 "
        DtTemp = AgL.FillData(mQry, AgL.GcnRead).Tables(0)

        If DtTemp.Rows.Count > 0 Then
            For I = 0 To DtTemp.Rows.Count - 1
                'MsgStr += "Carpet Id " & AgL.XNull(DtTemp.Rows(I)("Item_Uid")) & " Is Packed."
                MsgStr += "Carpet Id " & AgL.XNull(DtTemp.Rows(I)("Item_Uid")) & " Is " & AgL.XNull(DtTemp.Rows(I)("ClosedRemark"))
            Next
        End If

        mQry = "SELECT I.Item_UID " &
               " FROM (SELECT DocID, Item_UID, Site_Code " &
               "       FROM JobIssRecUID  " &
               "       WHERE Item_UID In (" & mItem_UidStr & ") And IssRec= 'I') I " &
               " LEFT JOIN JobIssRecUID R  ON I.DocID = R.JobRecDocID AND I.Item_UID = R.Item_UID  " &
               " WHERE R.DocID IS NULL AND I.DocID <> '" & mSearchCode & "' " &
               " And I.Site_Code = '" & AgL.PubSiteCode & "'" &
               " Group By I.Item_UID " &
               " Having Count(I.DocId) > 0 "
        DtTemp = AgL.FillData(mQry, AgL.GcnRead).Tables(0)
        If DtTemp.Rows.Count > 0 Then
            For I = 0 To DtTemp.Rows.Count - 1
                mQry = "SELECT Sg.Name, H.ManualRefNo, H.V_Date, Vc.NCatDescription AS ProcessDesc, " &
                            " Iu.Item_Uid As Item_UidDesc " &
                            " FROM (SELECT DocID, Item_UID, Site_Code FROM JobIssRecUID  " &
                            "       WHERE Item_UID = '" & DtTemp.Rows(0)("Item_Uid") & "' And IssRec='I') I " &
                            " LEFT JOIN JobIssRecUID R  ON I.DocID = R.JobRecDocID AND I.Item_UID = R.Item_UID  " &
                            " LEFT JOIN JobOrder H  ON I.DocID = H.DocID " &
                            " LEFT JOIN Item_Uid Iu On I.Item_Uid = Iu.Code " &
                            " LEFT JOIN SubGroup Sg  ON H.JobWorker = Sg.SubCode " &
                            " LEFT JOIN VoucherCat Vc  ON H.Process = Vc.NCat " &
                            " WHERE R.DocID IS NULL AND I.DocID <> '" & mSearchCode & "' " &
                            " And I.Site_Code = '" & AgL.PubSiteCode & "' " &
                            " ORDER BY H.V_Date Desc Limit 1"
                DtTemp1 = AgL.FillData(mQry, AgL.GcnRead).Tables(0)

                If DtTemp1.Rows.Count > 0 Then
                    MsgStr += "Carpet Id " & DtTemp1.Rows(0)("Item_UidDesc") & " Is Already Issued To " & AgL.XNull(DtTemp1.Rows(0)("Name")) & " For " & AgL.XNull(DtTemp1.Rows(0)("ProcessDesc")) & " On Date " & AgL.XNull(DtTemp1.Rows(0)("V_Date")) & " Against Ref No " & AgL.XNull(DtTemp1.Rows(0)("ManualRefNo")) & "."
                End If
            Next
        End If

        FDataValidation_Item_UID = MsgStr
        Dgl1.Focus()
    End Function

    Private Sub FrmJobOrder_BaseEvent_Topctrl_tbEdit(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbEdit
        Passed = Not FGetRelationalData()

        If isRecordLocked Then
            If AgL.PubUserName.ToUpper = "SA" Or AgL.PubUserName.ToUpper = AgLibrary.ClsConstant.PubSuperUserName Then
                If MsgBox("Referential data exist. Do you want to modify record?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                    Passed = False
                    Exit Sub
                Else
                    TxtJobWorker.Enabled = False
                End If
            Else
                MsgBox("Referential data exist. Can't modify record.")
                Passed = False
                Exit Sub
            End If
        End If

        Passed = Not ClsMain.FLockOldEntryInNewEntryPoint(TxtProcess.Tag, TxtV_Date.Text)
        FFillJobEnviro()
        FAsignProcess()
        FAsignMeasureField()
    End Sub

    'Private Function FGetJobRate(ByVal mProcess As String, ByVal mParty As String, ByVal mItem As String) As Double
    '    If Val(TxtRate.Text) > 0 Then
    '        FGetJobRate = Val(TxtRate.Text)
    '    Else
    '        mQry = " Select Rate From RateListDetail L  " & _
    '                " Where IFNull(SubCode,'') = (SELECT CASE WHEN  Count(*) > 0 THEN Max(PartyRateGroup) ELSE '' END From SubGroup Where SubCode = '" & mParty & "') " & _
    '                " And IFNull(Item,'') = (SELECT CASE WHEN Count(*) > 0 THEN Max(ItemRateGroup)  ELSE '' END  From ItemProcessDetail Where Code = '" & mItem & "' And Process = '" & mProcess & "') " & _
    '                " And Process ='" & mProcess & "'"
    '        FGetJobRate = AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar)
    '    End If
    'End Function

    Private Sub FrmJobOrder_BaseEvent_Topctrl_tbDel(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbDel
        Passed = Not FGetRelationalData()
        If isRecordLocked Then
            MsgBox("Referential data exist. Can't delete record.")
            Passed = False
        End If
    End Sub

    Private Function FGetRelationalData() As Boolean
        Try
            Dim bRData As String
            mQry = " DECLARE @Temp NVARCHAR(Max); "
            mQry += " SET @Temp=''; "
            mQry += " SELECT  @Temp=@Temp +  X.VNo + ', ' " &
                    " FROM ( " &
                    "   SELECT DISTINCT H.V_Type + '-' + Convert(VARCHAR,H.ManualRefNo) AS VNo " &
                    "   FROM Stock L " &
                    "   LEFT JOIN StockHead H on H.Docid = L.DocId " &
                    "   WHERE L.CostCenter = '" & mInternalCode & "' " &
                    "   And IFNull(H.IsDeleted,0)=0) AS X  "
            mQry += " SELECT @Temp as RelationalData "
            bRData = AgL.XNull(AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar)
            If bRData.Trim <> "" Then
                MsgBox("Material Issue " & bRData & " created against Job Order No. " & TxtV_Type.Tag & "-" & TxtV_No.Text & ". Can't Modify Entry")
                FGetRelationalData = True
                Exit Function
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " in FGetRelationalData in TempMaterialPlan")
            FGetRelationalData = True
        End Try
    End Function

    Private Sub BtnConsumptionDetail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnMaterialIssueDetail.Click
        Dim FrmObj As FrmJobOrderMaterialIssue
        If BtnMaterialIssueDetail.Tag Is Nothing Then
            FrmObj = New FrmJobOrderMaterialIssue(TxtV_Date.Text, TxtGodown.Tag, mInternalCode)
            FrmObj.IniGrid()
            'If AgL.StrCmp(Topctrl1.Mode, "Browse") Then
            FMovRecMaterialIssue(FrmObj) : BtnMaterialIssueDetail.Tag = FrmObj
            'End If
        Else
            FrmObj = BtnMaterialIssueDetail.Tag
        End If
        FrmObj.Owner = Me
        FrmObj.StartPosition = FormStartPosition.CenterScreen
        FrmObj.ShowDialog()

        If FrmObj.mOkButtonPressed Then
            BtnMaterialIssueDetail.Tag = FrmObj
        End If
    End Sub

    Private Sub FPostMaterialIssue(ByVal Conn As SQLiteConnection, ByVal Cmd As SQLiteCommand)
        Dim MaxSr As Integer = 0
        Dim I As Integer = 0
        Dim mSr As Integer = 0
        Dim bSelectionQry As String = ""
        Dim FrmObj As FrmJobOrderMaterialIssue = Nothing

        mQry = ""
        If BtnMaterialIssueDetail.Tag IsNot Nothing Then
            mQry = "Delete From Stock Where DocId = '" & mSearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            mQry = "Delete From StockProcess Where DocId = '" & mSearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            mQry = "Delete From JobIssueDetail Where DocId = '" & mSearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            mQry = "Delete From JobIssRec Where DocId = '" & mSearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

            FrmObj = BtnMaterialIssueDetail.Tag

            With FrmObj
                For I = 0 To .Dgl1.Rows.Count - 1
                    If .Dgl1.Item(FrmJobOrderMaterialIssue.Col1Item, I).Value <> "" Then
                        mSr += 1
                        If bSelectionQry <> "" Then bSelectionQry += " UNION ALL "
                        bSelectionQry += "SELECT '" & mSearchCode & "', " & mSr & " As Sr, " &
                                " " & AgL.Chk_Text(.Dgl1.Item(FrmJobOrderMaterialIssue.Col1Item, I).Tag) & ", " &
                                " " & AgL.Chk_Text(.Dgl1.Item(FrmJobOrderMaterialIssue.Col1LotNo, I).Value) & ", " &
                                " " & AgL.Chk_Text(.Dgl1.Item(FrmJobOrderMaterialIssue.Col1FromProcess, I).Tag) & ", " &
                                " " & AgL.Chk_Text(.Dgl1.Item(FrmJobOrderMaterialIssue.Col1Dimension1, I).Tag) & ", " &
                                " " & AgL.Chk_Text(.Dgl1.Item(FrmJobOrderMaterialIssue.Col1Dimension2, I).Tag) & ", " &
                                " " & Val(.Dgl1.Item(FrmJobOrderMaterialIssue.Col1Qty, I).Value) & ", " &
                                " " & Val(.Dgl1.Item(FrmJobOrderMaterialIssue.Col1Rate, I).Value) & ", " &
                                " " & Val(.Dgl1.Item(FrmJobOrderMaterialIssue.Col1Amount, I).Value) & ", " &
                                " " & AgL.Chk_Text(.Dgl1.Item(FrmJobOrderMaterialIssue.Col1Unit, I).Value) & " "
                    End If
                Next
            End With

            mQry = " INSERT INTO JobIssRec (DocID, V_Type, V_Prefix, V_Date, V_No, Div_Code, Site_Code, ManualRefNo, Process, JobWorker, Godown, IssQty, EntryBy, EntryDate, Status, CostCenter) " &
                    " SELECT H.DocID, H.V_Type, H.V_Prefix, H.V_Date, H.V_No, H.Div_Code, H.Site_Code, " &
                    " H.ManualRefNo, H.Process, H.JobWorker, H.Godown, H.TotalQty, H.EntryBy, H.EntryDate, " &
                    " H.Status, H.CostCenter  " &
                    " FROM JobOrder H Where H.DocId = '" & mSearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

            mQry = "INSERT INTO JobIssueDetail(DocId, Sr, Item, LotNO, PrevProcess, Dimension1, Dimension2, Qty, Rate, Amount, Unit) " & bSelectionQry
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

            MaxSr = AgL.VNull(AgL.Dman_Execute("Select Max(Sr) From Stock  Where DocId = '" & mSearchCode & "'", AgL.GcnRead).ExecuteScalar)

            mQry = "INSERT INTO Stock(DocID, Sr, V_Type, V_Prefix, V_Date, V_No, RecId, Div_Code, " &
                    " Site_Code, SubCode, Item, LotNo, Godown, Qty_Iss, Unit, MeasurePerPcs, Measure_Iss, MeasureUnit, " &
                    " Process, Dimension1, Dimension2, Rate, Amount, CostCenter) " &
                    " SELECT L.DocId, " & MaxSr & " + row_NUMBER() OVER (ORDER BY L.Sr) AS Sr, " &
                    " H.V_Type, H.V_Prefix, H.V_Date, H.V_No, H.ManualRefNo, H.Div_Code, H.Site_Code, " &
                    " H.JobWorker, L.Item, LotNo, H.Godown, L.Qty As Qty_Iss, L.Unit, " &
                    " L.MeasurePerPcs, L.TotalMeasure Measure_Iss, " &
                    " L.MeasureUnit, L.PrevProcess, L.Dimension1, L.Dimension2, L.Rate, L.Amount, J.CostCenter " &
                    " FROM (Select * From JobIssueDetail Where DocId = '" & mSearchCode & "') As L  " &
                    " LEFT JOIN JobIssRec H On L.DocId = H.DocId " &
                    " LEFT JOIN JobOrder J On L.JobOrder = J.DocId "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

            mQry = "INSERT INTO StockProcess(DocID, Sr, V_Type, V_Prefix, V_Date, V_No, RecId, Div_Code, " &
                    " Site_Code, SubCode, Item, LotNo, Godown, Qty_Rec, Unit, MeasurePerPcs, Measure_Rec, MeasureUnit, " &
                    " Process, Dimension1, Dimension2, Rate, Amount, CostCenter) " &
                    " SELECT L.DocId, " & MaxSr & " + row_NUMBER() OVER (ORDER BY L.Sr) AS Sr, " &
                    " H.V_Type, H.V_Prefix, H.V_Date, H.V_No, H.ManualRefNo, H.Div_Code, H.Site_Code, " &
                    " H.JobWorker, L.Item, LotNo, H.Godown, L.Qty As Qty_Iss, L.Unit, " &
                    " L.MeasurePerPcs, L.TotalMeasure Measure_Iss, " &
                    " L.MeasureUnit, '" & TxtProcess.Tag & "', L.Dimension1, L.Dimension2, L.Rate, L.Amount, J.CostCenter " &
                    " FROM (Select * From JobIssueDetail Where DocId = '" & mSearchCode & "') As L  " &
                    " LEFT JOIN JobIssRec H On L.DocId = H.DocId " &
                    " LEFT JOIN JobOrder J On L.JobOrder = J.DocId "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If
    End Sub

    Private Sub FMovRecMaterialIssue(ByVal FrmObj As FrmJobOrderMaterialIssue)
        Dim DtTemp As DataTable = Nothing
        Dim I As Integer = 0

        mQry = " Select L.*, I.Description As ItemDesc,P.Description AS FromProcessDesc, D1.Description As D1Desc, D2.Description AS D2Desc " &
                " From JobIssueDetail L " &
                " LEFT JOIN Item I On L.Item = I.Code " &
                " LEFT JOIN Process P On L.PrevProcess = P.NCat " &
                " LEFT JOIN Dimension1 D1 On L.Dimension1 = D1.Code " &
                " LEFT JOIN Dimension2 D2 On L.Dimension2 = D2.Code " &
                " Where L.DocId = '" & mSearchCode & "'"
        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

        With FrmObj
            .Dgl1.RowCount = 1 : .Dgl1.Rows.Clear()
            If DtTemp.Rows.Count > 0 Then
                For I = 0 To DtTemp.Rows.Count - 1
                    .Dgl1.Rows.Add()
                    .Dgl1.Item(FrmJobOrderMaterialIssue.ColSNo, I).Value = .Dgl1.Rows.Count - 1
                    .Dgl1.Item(FrmJobOrderMaterialIssue.Col1Item, I).Tag = AgL.XNull(DtTemp.Rows(I)("Item"))
                    .Dgl1.Item(FrmJobOrderMaterialIssue.Col1Item, I).Value = AgL.XNull(DtTemp.Rows(I)("ItemDesc"))
                    .Dgl1.Item(FrmJobOrderMaterialIssue.Col1LotNo, I).Value = AgL.XNull(DtTemp.Rows(I)("LotNo"))
                    .Dgl1.Item(FrmJobOrderMaterialIssue.Col1Qty, I).Value = AgL.VNull(DtTemp.Rows(I)("Qty"))
                    .Dgl1.Item(FrmJobOrderMaterialIssue.Col1Rate, I).Value = AgL.VNull(DtTemp.Rows(I)("Rate"))
                    .Dgl1.Item(FrmJobOrderMaterialIssue.Col1Amount, I).Value = AgL.VNull(DtTemp.Rows(I)("Amount"))
                    .Dgl1.Item(FrmJobOrderMaterialIssue.Col1Unit, I).Value = AgL.XNull(DtTemp.Rows(I)("Unit"))

                    .Dgl1.Item(FrmJobOrderMaterialIssue.Col1FromProcess, I).Tag = AgL.XNull(DtTemp.Rows(I)("PrevProcess"))
                    .Dgl1.Item(FrmJobOrderMaterialIssue.Col1FromProcess, I).Value = AgL.XNull(DtTemp.Rows(I)("FromProcessDesc"))

                    .Dgl1.Item(FrmJobOrderMaterialIssue.Col1Dimension1, I).Tag = AgL.XNull(DtTemp.Rows(I)("Dimension1"))
                    .Dgl1.Item(FrmJobOrderMaterialIssue.Col1Dimension1, I).Value = AgL.XNull(DtTemp.Rows(I)("D1Desc"))
                    .Dgl1.Item(FrmJobOrderMaterialIssue.Col1Dimension2, I).Tag = AgL.XNull(DtTemp.Rows(I)("Dimension2"))
                    .Dgl1.Item(FrmJobOrderMaterialIssue.Col1Dimension2, I).Value = AgL.XNull(DtTemp.Rows(I)("D2Desc"))
                Next I
            End If
        End With
    End Sub

    Private Function FFilterUsedItems() As String
        Dim I As Integer = 0
        FFilterUsedItems = " 1=1 "

        Try
            With Dgl1
                For I = 0 To .Rows.Count - 1
                    If .Item(Col1Item, I).Value <> "" Then
                        If RbtForProdOrder.Checked Then
                            'FFilterUsedItems += " And ProdOrder +  ProdOrderSr  <> '" & Dgl1.Item(Col1ProdOrder, I).Tag & "' + '" & Dgl1.Item(Col1ProdOrderSr, I).Value.ToString & "'"
                            FFilterUsedItems += " And Item + IFNull(ProdOrder,'') +  IFNull(Dimension1,'') + IFNull(Dimension2,'') <> '" & Dgl1.Item(Col1Item, I).Tag & "' + '" & Dgl1.Item(Col1ProdOrder, I).Tag & "' + '" & Dgl1.Item(Col1Dimension1, I).Tag & "' + '" & Dgl1.Item(Col1Dimension2, I).Tag & "'"
                            ''Else
                            ''    FFilterUsedItems += " And JobOrder +  JobOrderSr <> '" & Dgl1.Item(Col1JobOrder, I).Tag & "' + '" & Dgl1.Item(Col1JobOrderSr, I).Value.ToString & "'"
                        End If
                    End If
                Next
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function

    Private Function FGetJobRateHelpDataSet() As DataSet
        Try
            mQry = "Select * from JobWorkerRateDetail L  LEFT JOIN JobWorkerRate H  ON L.Code = H.Code Where L.Process = '" & TxtProcess.Tag & "' And H.Div_Code = '" & IIf(TxtItemDivision.Tag <> "", TxtItemDivision.Tag, AgL.PubDivCode) & "' Order By L.WEF Desc "
            FGetJobRateHelpDataSet = AgL.FillData(mQry, AgL.GcnRead)
        Catch ex As Exception
            FGetJobRateHelpDataSet = Nothing
            MsgBox(ex.Message)
        End Try
    End Function

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

    Private Sub FFillJobEnviro()
        mQry = "Select * from JobEnviro  Where V_Type = '" & TxtV_Type.Tag & "' And Div_Code = '" & TxtDivision.Tag & "' And Site_Code ='" & TxtSite_Code.Tag & "' "
        DtJobEnviro = AgL.FillData(mQry, AgL.GCn).Tables(0)
    End Sub
End Class
