Imports System.IO
Imports System.Data.SQLite
Imports CrystalDecisions.CrystalReports.Engine
Public Class FrmJobInvoiceAmendment
    Inherits AgTemplate.TempTransaction
    Dim mQry$

    Public WithEvents AgCalcGrid1 As New AgStructure.AgCalcGrid

    Protected WithEvents Dgl1 As New AgControls.AgDataGrid
    Protected Const ColSNo As String = "S.No."
    Protected Const Col1JobWorker As String = "Job Worker"
    Protected Const Col1Item_Uid As String = "Item_Uid"
    Protected Const Col1Item As String = "Item"
    Protected Const Col1JobOrder As String = "Job Order"
    Protected Const Col1JobOrderSr As String = "Job Order Sr"
    Protected Const Col1ProdOrder As String = "Prod Order"
    Protected Const Col1ProdOrderSr As String = "Prod Order Sr"
    Protected Const Col1LotNo As String = "Lot No"
    Protected Const Col1JobReceive As String = "Job Receive"
    Protected Const Col1JobReceiveSr As String = "Job Receive Sr"
    Protected Const Col1JobInvoice As String = "Job Invoice"
    Protected Const Col1JobInvoiceSr As String = "Job Invoice Sr"
    Protected Const Col1BillQty As String = "Bill Qty"
    Protected Const Col1Unit As String = "Unit"
    Protected Const Col1QtyDecimalPlaces As String = "Qty Decimal Places"
    Protected Const Col1MeasurePerPcs As String = "Measure Per Pcs"
    Protected Const Col1TotalMeasure As String = "Total Measure"
    Protected Const Col1MeasureUnit As String = "Measure Unit"
    Protected Const Col1MeasureDecimalPlaces As String = "Measure Decimal Places"
    Protected Const Col1Rate_Inv As String = "Rate_Inv"
    Protected Const Col1Rate_Amd As String = "Rate_Amd"
    Protected Const Col1Rate As String = "Rate"
    Protected Const Col1Amount As String = "Amount"
    Protected Const Col1Remark As String = "Remark"
    Protected WithEvents TxtCostCenter As AgControls.AgTextBox
    Protected WithEvents TxtProcessAc As AgControls.AgTextBox

    Dim mRateAmendedColour As Color = Color.Cornsilk

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
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.LblTotalAmount = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Pnl1 = New System.Windows.Forms.Panel
        Me.TxtRemarks = New AgControls.AgTextBox
        Me.TxtManualRefNo = New AgControls.AgTextBox
        Me.LblManualRefNo = New System.Windows.Forms.Label
        Me.TxtProcess = New AgControls.AgTextBox
        Me.LblProcess = New System.Windows.Forms.Label
        Me.LblJobReceiveDetail = New System.Windows.Forms.LinkLabel
        Me.TxtBillingOn = New AgControls.AgTextBox
        Me.LblRemark1 = New System.Windows.Forms.Label
        Me.LblManualRefNoReq = New System.Windows.Forms.Label
        Me.PnlCalcGrid = New System.Windows.Forms.Panel
        Me.Label1 = New System.Windows.Forms.Label
        Me.TxtOrderBy = New AgControls.AgTextBox
        Me.TxtStructure = New AgControls.AgTextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.BtnFillJobInvoice = New System.Windows.Forms.Button
        Me.GrpDirectChallan = New System.Windows.Forms.GroupBox
        Me.RbtForJobInvoice = New System.Windows.Forms.RadioButton
        Me.RbtForJobInvoiceItems = New System.Windows.Forms.RadioButton
        Me.TxtFromDate = New AgControls.AgTextBox
        Me.TxtToDate = New AgControls.AgTextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.TxtCostCenter = New AgControls.AgTextBox
        Me.TxtProcessAc = New AgControls.AgTextBox
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
        Me.Label2.Location = New System.Drawing.Point(108, 36)
        Me.Label2.Tag = ""
        '
        'LblV_Date
        '
        Me.LblV_Date.BackColor = System.Drawing.Color.Transparent
        Me.LblV_Date.Location = New System.Drawing.Point(2, 31)
        Me.LblV_Date.Size = New System.Drawing.Size(109, 16)
        Me.LblV_Date.Tag = ""
        Me.LblV_Date.Text = "Amendment Date"
        '
        'LblV_TypeReq
        '
        Me.LblV_TypeReq.Location = New System.Drawing.Point(331, 16)
        Me.LblV_TypeReq.Tag = ""
        '
        'TxtV_Date
        '
        Me.TxtV_Date.AgSelectedValue = ""
        Me.TxtV_Date.BackColor = System.Drawing.Color.White
        Me.TxtV_Date.Location = New System.Drawing.Point(124, 30)
        Me.TxtV_Date.Size = New System.Drawing.Size(86, 18)
        Me.TxtV_Date.TabIndex = 2
        Me.TxtV_Date.Tag = ""
        '
        'LblV_Type
        '
        Me.LblV_Type.Location = New System.Drawing.Point(216, 12)
        Me.LblV_Type.Size = New System.Drawing.Size(109, 16)
        Me.LblV_Type.Tag = ""
        Me.LblV_Type.Text = "Amendment Type"
        '
        'TxtV_Type
        '
        Me.TxtV_Type.AgSelectedValue = ""
        Me.TxtV_Type.BackColor = System.Drawing.Color.White
        Me.TxtV_Type.Location = New System.Drawing.Point(345, 10)
        Me.TxtV_Type.Size = New System.Drawing.Size(154, 18)
        Me.TxtV_Type.TabIndex = 1
        Me.TxtV_Type.Tag = ""
        '
        'LblSite_CodeReq
        '
        Me.LblSite_CodeReq.Location = New System.Drawing.Point(108, 16)
        Me.LblSite_CodeReq.Tag = ""
        '
        'LblSite_Code
        '
        Me.LblSite_Code.BackColor = System.Drawing.Color.Transparent
        Me.LblSite_Code.Location = New System.Drawing.Point(2, 11)
        Me.LblSite_Code.Size = New System.Drawing.Size(87, 16)
        Me.LblSite_Code.Tag = ""
        Me.LblSite_Code.Text = "Branch Name"
        '
        'TxtSite_Code
        '
        Me.TxtSite_Code.AgSelectedValue = ""
        Me.TxtSite_Code.BackColor = System.Drawing.Color.White
        Me.TxtSite_Code.Location = New System.Drawing.Point(124, 10)
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
        Me.TabControl1.Size = New System.Drawing.Size(970, 105)
        Me.TabControl1.TabIndex = 0
        '
        'TP1
        '
        Me.TP1.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.TP1.Controls.Add(Me.Label7)
        Me.TP1.Controls.Add(Me.Label3)
        Me.TP1.Controls.Add(Me.TxtToDate)
        Me.TP1.Controls.Add(Me.TxtFromDate)
        Me.TP1.Controls.Add(Me.Label5)
        Me.TP1.Controls.Add(Me.Label4)
        Me.TP1.Controls.Add(Me.TxtStructure)
        Me.TP1.Controls.Add(Me.Label1)
        Me.TP1.Controls.Add(Me.TxtOrderBy)
        Me.TP1.Controls.Add(Me.LblManualRefNoReq)
        Me.TP1.Controls.Add(Me.LblRemark1)
        Me.TP1.Controls.Add(Me.TxtBillingOn)
        Me.TP1.Controls.Add(Me.TxtRemarks)
        Me.TP1.Controls.Add(Me.TxtManualRefNo)
        Me.TP1.Controls.Add(Me.LblManualRefNo)
        Me.TP1.Controls.Add(Me.TxtProcess)
        Me.TP1.Controls.Add(Me.LblProcess)
        Me.TP1.Location = New System.Drawing.Point(4, 22)
        Me.TP1.Size = New System.Drawing.Size(962, 79)
        Me.TP1.Text = "Document Detail"
        Me.TP1.Controls.SetChildIndex(Me.LblProcess, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtProcess, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblPrefix, 0)
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
        Me.TP1.Controls.SetChildIndex(Me.TxtOrderBy, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label1, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtStructure, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label4, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label5, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtFromDate, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtToDate, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label3, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label7, 0)
        '
        'Topctrl1
        '
        Me.Topctrl1.Size = New System.Drawing.Size(965, 41)
        Me.Topctrl1.TabIndex = 2
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
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Cornsilk
        Me.Panel1.Controls.Add(Me.LblTotalAmount)
        Me.Panel1.Controls.Add(Me.Label6)
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
        'Pnl1
        '
        Me.Pnl1.Location = New System.Drawing.Point(1, 150)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(962, 246)
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
        Me.TxtRemarks.Location = New System.Drawing.Point(583, 50)
        Me.TxtRemarks.MaxLength = 255
        Me.TxtRemarks.Name = "TxtRemarks"
        Me.TxtRemarks.Size = New System.Drawing.Size(356, 18)
        Me.TxtRemarks.TabIndex = 8
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
        Me.TxtManualRefNo.Location = New System.Drawing.Point(345, 30)
        Me.TxtManualRefNo.MaxLength = 50
        Me.TxtManualRefNo.Name = "TxtManualRefNo"
        Me.TxtManualRefNo.Size = New System.Drawing.Size(154, 18)
        Me.TxtManualRefNo.TabIndex = 3
        '
        'LblManualRefNo
        '
        Me.LblManualRefNo.AutoSize = True
        Me.LblManualRefNo.BackColor = System.Drawing.Color.Transparent
        Me.LblManualRefNo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblManualRefNo.Location = New System.Drawing.Point(216, 30)
        Me.LblManualRefNo.Name = "LblManualRefNo"
        Me.LblManualRefNo.Size = New System.Drawing.Size(102, 16)
        Me.LblManualRefNo.TabIndex = 726
        Me.LblManualRefNo.Text = "Amendment No."
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
        Me.TxtProcess.Location = New System.Drawing.Point(583, 10)
        Me.TxtProcess.MaxLength = 20
        Me.TxtProcess.Name = "TxtProcess"
        Me.TxtProcess.Size = New System.Drawing.Size(356, 18)
        Me.TxtProcess.TabIndex = 6
        '
        'LblProcess
        '
        Me.LblProcess.AutoSize = True
        Me.LblProcess.BackColor = System.Drawing.Color.Transparent
        Me.LblProcess.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblProcess.Location = New System.Drawing.Point(506, 11)
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
        Me.LblJobReceiveDetail.Location = New System.Drawing.Point(1, 129)
        Me.LblJobReceiveDetail.Name = "LblJobReceiveDetail"
        Me.LblJobReceiveDetail.Size = New System.Drawing.Size(136, 20)
        Me.LblJobReceiveDetail.TabIndex = 733
        Me.LblJobReceiveDetail.TabStop = True
        Me.LblJobReceiveDetail.Text = "Job Invoice Detail"
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
        Me.LblRemark1.Location = New System.Drawing.Point(505, 51)
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
        Me.LblManualRefNoReq.Location = New System.Drawing.Point(331, 37)
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
        Me.Label1.Location = New System.Drawing.Point(505, 31)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(60, 16)
        Me.Label1.TabIndex = 750
        Me.Label1.Text = "Order By"
        '
        'TxtOrderBy
        '
        Me.TxtOrderBy.AgAllowUserToEnableMasterHelp = False
        Me.TxtOrderBy.AgLastValueTag = Nothing
        Me.TxtOrderBy.AgLastValueText = Nothing
        Me.TxtOrderBy.AgMandatory = False
        Me.TxtOrderBy.AgMasterHelp = False
        Me.TxtOrderBy.AgNumberLeftPlaces = 0
        Me.TxtOrderBy.AgNumberNegetiveAllow = False
        Me.TxtOrderBy.AgNumberRightPlaces = 0
        Me.TxtOrderBy.AgPickFromLastValue = False
        Me.TxtOrderBy.AgRowFilter = ""
        Me.TxtOrderBy.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtOrderBy.AgSelectedValue = Nothing
        Me.TxtOrderBy.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtOrderBy.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtOrderBy.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtOrderBy.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtOrderBy.Location = New System.Drawing.Point(583, 30)
        Me.TxtOrderBy.MaxLength = 255
        Me.TxtOrderBy.Name = "TxtOrderBy"
        Me.TxtOrderBy.Size = New System.Drawing.Size(356, 18)
        Me.TxtOrderBy.TabIndex = 7
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
        Me.Label4.Location = New System.Drawing.Point(566, 14)
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
        Me.Label5.Location = New System.Drawing.Point(566, 37)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(10, 7)
        Me.Label5.TabIndex = 765
        Me.Label5.Text = "Ä"
        '
        'BtnFillJobInvoice
        '
        Me.BtnFillJobInvoice.BackColor = System.Drawing.Color.Transparent
        Me.BtnFillJobInvoice.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnFillJobInvoice.Font = New System.Drawing.Font("Lucida Console", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnFillJobInvoice.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnFillJobInvoice.Location = New System.Drawing.Point(466, 129)
        Me.BtnFillJobInvoice.Margin = New System.Windows.Forms.Padding(0)
        Me.BtnFillJobInvoice.Name = "BtnFillJobInvoice"
        Me.BtnFillJobInvoice.Size = New System.Drawing.Size(38, 20)
        Me.BtnFillJobInvoice.TabIndex = 1
        Me.BtnFillJobInvoice.Text = "..."
        Me.BtnFillJobInvoice.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnFillJobInvoice.UseVisualStyleBackColor = False
        '
        'GrpDirectChallan
        '
        Me.GrpDirectChallan.Controls.Add(Me.RbtForJobInvoice)
        Me.GrpDirectChallan.Controls.Add(Me.RbtForJobInvoiceItems)
        Me.GrpDirectChallan.Location = New System.Drawing.Point(143, 122)
        Me.GrpDirectChallan.Name = "GrpDirectChallan"
        Me.GrpDirectChallan.Size = New System.Drawing.Size(307, 26)
        Me.GrpDirectChallan.TabIndex = 766
        Me.GrpDirectChallan.TabStop = False
        '
        'RbtForJobInvoice
        '
        Me.RbtForJobInvoice.AutoSize = True
        Me.RbtForJobInvoice.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RbtForJobInvoice.Location = New System.Drawing.Point(5, 8)
        Me.RbtForJobInvoice.Name = "RbtForJobInvoice"
        Me.RbtForJobInvoice.Size = New System.Drawing.Size(126, 17)
        Me.RbtForJobInvoice.TabIndex = 0
        Me.RbtForJobInvoice.TabStop = True
        Me.RbtForJobInvoice.Text = "For Job Invoice"
        Me.RbtForJobInvoice.UseVisualStyleBackColor = True
        '
        'RbtForJobInvoiceItems
        '
        Me.RbtForJobInvoiceItems.AutoSize = True
        Me.RbtForJobInvoiceItems.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RbtForJobInvoiceItems.Location = New System.Drawing.Point(135, 8)
        Me.RbtForJobInvoiceItems.Name = "RbtForJobInvoiceItems"
        Me.RbtForJobInvoiceItems.Size = New System.Drawing.Size(168, 17)
        Me.RbtForJobInvoiceItems.TabIndex = 743
        Me.RbtForJobInvoiceItems.TabStop = True
        Me.RbtForJobInvoiceItems.Text = "For Job Invoice Items"
        Me.RbtForJobInvoiceItems.UseVisualStyleBackColor = True
        '
        'TxtFromDate
        '
        Me.TxtFromDate.AgAllowUserToEnableMasterHelp = False
        Me.TxtFromDate.AgLastValueTag = Nothing
        Me.TxtFromDate.AgLastValueText = Nothing
        Me.TxtFromDate.AgMandatory = True
        Me.TxtFromDate.AgMasterHelp = False
        Me.TxtFromDate.AgNumberLeftPlaces = 8
        Me.TxtFromDate.AgNumberNegetiveAllow = False
        Me.TxtFromDate.AgNumberRightPlaces = 2
        Me.TxtFromDate.AgPickFromLastValue = False
        Me.TxtFromDate.AgRowFilter = ""
        Me.TxtFromDate.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtFromDate.AgSelectedValue = Nothing
        Me.TxtFromDate.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtFromDate.AgValueType = AgControls.AgTextBox.TxtValueType.Date_Value
        Me.TxtFromDate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtFromDate.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFromDate.Location = New System.Drawing.Point(124, 50)
        Me.TxtFromDate.MaxLength = 20
        Me.TxtFromDate.Name = "TxtFromDate"
        Me.TxtFromDate.Size = New System.Drawing.Size(86, 18)
        Me.TxtFromDate.TabIndex = 4
        '
        'TxtToDate
        '
        Me.TxtToDate.AgAllowUserToEnableMasterHelp = False
        Me.TxtToDate.AgLastValueTag = Nothing
        Me.TxtToDate.AgLastValueText = Nothing
        Me.TxtToDate.AgMandatory = True
        Me.TxtToDate.AgMasterHelp = False
        Me.TxtToDate.AgNumberLeftPlaces = 8
        Me.TxtToDate.AgNumberNegetiveAllow = False
        Me.TxtToDate.AgNumberRightPlaces = 2
        Me.TxtToDate.AgPickFromLastValue = False
        Me.TxtToDate.AgRowFilter = ""
        Me.TxtToDate.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtToDate.AgSelectedValue = Nothing
        Me.TxtToDate.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtToDate.AgValueType = AgControls.AgTextBox.TxtValueType.Date_Value
        Me.TxtToDate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtToDate.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtToDate.Location = New System.Drawing.Point(345, 50)
        Me.TxtToDate.MaxLength = 20
        Me.TxtToDate.Name = "TxtToDate"
        Me.TxtToDate.Size = New System.Drawing.Size(154, 18)
        Me.TxtToDate.TabIndex = 5
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(2, 52)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(69, 16)
        Me.Label3.TabIndex = 768
        Me.Label3.Text = "From Date"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(216, 51)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(52, 16)
        Me.Label7.TabIndex = 769
        Me.Label7.Text = "To Date"
        '
        'TxtCostCenter
        '
        Me.TxtCostCenter.AgAllowUserToEnableMasterHelp = False
        Me.TxtCostCenter.AgLastValueTag = Nothing
        Me.TxtCostCenter.AgLastValueText = Nothing
        Me.TxtCostCenter.AgMandatory = False
        Me.TxtCostCenter.AgMasterHelp = False
        Me.TxtCostCenter.AgNumberLeftPlaces = 8
        Me.TxtCostCenter.AgNumberNegetiveAllow = False
        Me.TxtCostCenter.AgNumberRightPlaces = 2
        Me.TxtCostCenter.AgPickFromLastValue = False
        Me.TxtCostCenter.AgRowFilter = ""
        Me.TxtCostCenter.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtCostCenter.AgSelectedValue = Nothing
        Me.TxtCostCenter.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtCostCenter.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtCostCenter.BackColor = System.Drawing.Color.PowderBlue
        Me.TxtCostCenter.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtCostCenter.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCostCenter.Location = New System.Drawing.Point(583, 131)
        Me.TxtCostCenter.MaxLength = 20
        Me.TxtCostCenter.Name = "TxtCostCenter"
        Me.TxtCostCenter.Size = New System.Drawing.Size(98, 18)
        Me.TxtCostCenter.TabIndex = 767
        Me.TxtCostCenter.Text = "TxtCostCenter"
        Me.TxtCostCenter.Visible = False
        Me.TxtCostCenter.WordWrap = False
        '
        'TxtProcessAc
        '
        Me.TxtProcessAc.AgAllowUserToEnableMasterHelp = False
        Me.TxtProcessAc.AgLastValueTag = Nothing
        Me.TxtProcessAc.AgLastValueText = Nothing
        Me.TxtProcessAc.AgMandatory = False
        Me.TxtProcessAc.AgMasterHelp = False
        Me.TxtProcessAc.AgNumberLeftPlaces = 8
        Me.TxtProcessAc.AgNumberNegetiveAllow = False
        Me.TxtProcessAc.AgNumberRightPlaces = 2
        Me.TxtProcessAc.AgPickFromLastValue = False
        Me.TxtProcessAc.AgRowFilter = ""
        Me.TxtProcessAc.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtProcessAc.AgSelectedValue = Nothing
        Me.TxtProcessAc.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtProcessAc.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtProcessAc.BackColor = System.Drawing.Color.PowderBlue
        Me.TxtProcessAc.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtProcessAc.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtProcessAc.Location = New System.Drawing.Point(725, 129)
        Me.TxtProcessAc.MaxLength = 20
        Me.TxtProcessAc.Name = "TxtProcessAc"
        Me.TxtProcessAc.Size = New System.Drawing.Size(98, 18)
        Me.TxtProcessAc.TabIndex = 768
        Me.TxtProcessAc.Text = "TxtProcessAc"
        Me.TxtProcessAc.Visible = False
        Me.TxtProcessAc.WordWrap = False
        '
        'FrmJobInvoiceAmendment
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.BackColor = System.Drawing.SystemColors.ButtonShadow
        Me.ClientSize = New System.Drawing.Size(965, 616)
        Me.Controls.Add(Me.TxtProcessAc)
        Me.Controls.Add(Me.TxtCostCenter)
        Me.Controls.Add(Me.GrpDirectChallan)
        Me.Controls.Add(Me.BtnFillJobInvoice)
        Me.Controls.Add(Me.LblJobReceiveDetail)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Pnl1)
        Me.Controls.Add(Me.PnlCalcGrid)
        Me.Name = "FrmJobInvoiceAmendment"
        Me.Text = "Job Invoice Amendment"
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
        Me.Controls.SetChildIndex(Me.BtnFillJobInvoice, 0)
        Me.Controls.SetChildIndex(Me.GrpDirectChallan, 0)
        Me.Controls.SetChildIndex(Me.TxtCostCenter, 0)
        Me.Controls.SetChildIndex(Me.TxtProcessAc, 0)
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
    Protected WithEvents Panel1 As System.Windows.Forms.Panel
    Protected WithEvents Pnl1 As System.Windows.Forms.Panel
    Protected WithEvents TxtRemarks As AgControls.AgTextBox
    Protected WithEvents TxtManualRefNo As AgControls.AgTextBox
    Protected WithEvents LblManualRefNo As System.Windows.Forms.Label
    Protected WithEvents TxtProcess As AgControls.AgTextBox
    Protected WithEvents LblProcess As System.Windows.Forms.Label
    Protected WithEvents LblJobReceiveDetail As System.Windows.Forms.LinkLabel
    Protected WithEvents TxtBillingOn As AgControls.AgTextBox
    Protected WithEvents LblRemark1 As System.Windows.Forms.Label
    Protected WithEvents LblManualRefNoReq As System.Windows.Forms.Label
    Protected WithEvents PnlCalcGrid As System.Windows.Forms.Panel
    Protected WithEvents Label1 As System.Windows.Forms.Label
    Protected WithEvents TxtOrderBy As AgControls.AgTextBox
    Protected WithEvents TxtStructure As AgControls.AgTextBox
    Protected WithEvents Label4 As System.Windows.Forms.Label
    Protected WithEvents Label5 As System.Windows.Forms.Label
    Protected WithEvents BtnFillJobInvoice As System.Windows.Forms.Button
    Protected WithEvents LblTotalAmount As System.Windows.Forms.Label
    Protected WithEvents Label6 As System.Windows.Forms.Label
    Protected WithEvents GrpDirectChallan As System.Windows.Forms.GroupBox
    Protected WithEvents RbtForJobInvoice As System.Windows.Forms.RadioButton
    Protected WithEvents RbtForJobInvoiceItems As System.Windows.Forms.RadioButton
    Protected WithEvents Label7 As System.Windows.Forms.Label
    Protected WithEvents Label3 As System.Windows.Forms.Label
    Protected WithEvents TxtToDate As AgControls.AgTextBox
    Protected WithEvents TxtFromDate As AgControls.AgTextBox
#End Region

    Private Sub Frm_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "JobInvoice"
        LogTableName = "JobInvoice_Log"
        MainLineTableCsv = "JobInvoiceDetail"
        LogLineTableCsv = "JobInvoiceDetail_Log"
        AgL.GridDesign(Dgl1)

        AgL.AddAgDataGrid(AgCalcGrid1, PnlCalcGrid)

        AgCalcGrid1.AgLibVar = AgL
        AgCalcGrid1.Visible = False
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mCondStr$
        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) & _
                        " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "H.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        If IsApplyVTypePermission Then
            mCondStr = mCondStr & " And H.V_Type In (Select V_Type From User_VType_Permission VP Where VP.UserName = '" & AgL.PubUserName & "' And VP.Div_Code = '" & AgL.PubDivCode & "' And VP.Site_Code = '" & AgL.PubSiteCode & "') "
        End If


        AgL.PubFindQry = " SELECT H.DocID AS SearchCode, H.V_Type AS [Amendment_Type], H.V_Date AS Date, " &
                            " H.ManualRefNo AS [Amendment_No], P.Description As [Process], Sg1.Name As [Order_By], " &
                            " H.EntryBy AS [Entry_By], H.EntryDate AS [Entry_Date], " &
                            " H.ApproveBy As [Approve_By], H.ApproveDate As [Approve_Date] " &
                            " FROM JobInvoice H  " &
                            " Left Join Voucher_Type Vt  On H.V_Type = Vt.V_Type  " &
                            " LEFT JOIN SubGroup Sg1 On H.JobReceiveBy = Sg1.SubCode " &
                            " LEFT JOIN Process P On H.Process = P.NCat" &
                            " Where 1=1  " & mCondStr
        AgL.PubFindQryOrdBy = "[Entry_Date]"
    End Sub

    Private Sub FrmQuality1_BaseFunction_FIniMast(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMast
        Dim mCondStr$
        mCondStr = " " & AgL.CondStrFinancialYear("J.V_Date", AgL.PubStartDate, AgL.PubEndDate) &
                        " And " & AgL.PubSiteCondition("J.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "J.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        If IsApplyVTypePermission Then
            mCondStr = mCondStr & " And J.V_Type In (Select V_Type From User_VType_Permission VP Where VP.UserName = '" & AgL.PubUserName & "' And VP.Div_Code = '" & AgL.PubDivCode & "' And VP.Site_Code = '" & AgL.PubSiteCode & "') "
        End If


        mQry = " Select J.DocID As SearchCode " &
                " From JobInvoice J  " &
                " Left Join Voucher_Type Vt  On J.V_Type = Vt.V_Type  " &
                " Where 1=1 " & mCondStr & "  Order By J.V_Date, J.V_No   "

        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub FrmSaleOrder_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl1, Col1Item, 150, 0, Col1Item, True, False)
            .AddAgTextColumn(Dgl1, Col1Item_Uid, 80, 0, Col1Item_Uid, True, True, False)
            .AddAgTextColumn(Dgl1, Col1JobWorker, 250, 0, Col1JobWorker, True, True)
            .AddAgTextColumn(Dgl1, Col1JobOrder, 70, 0, Col1JobOrder, True, True)
            .AddAgTextColumn(Dgl1, Col1JobOrderSr, 100, 0, Col1JobOrderSr, False, True)
            .AddAgTextColumn(Dgl1, Col1JobReceive, 70, 0, Col1JobReceive, True, True)
            .AddAgTextColumn(Dgl1, Col1JobReceiveSr, 100, 0, Col1JobReceiveSr, False, True)
            .AddAgTextColumn(Dgl1, Col1JobInvoice, 70, 0, Col1JobInvoice, True, True)
            .AddAgTextColumn(Dgl1, Col1JobInvoiceSr, 100, 0, Col1JobInvoiceSr, False, True)
            .AddAgTextColumn(Dgl1, Col1ProdOrder, 60, 0, Col1ProdOrder, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_ProdOrder")), Boolean), True, False)
            .AddAgTextColumn(Dgl1, Col1ProdOrderSr, 90, 0, Col1ProdOrderSr, False, False, False)
            .AddAgTextColumn(Dgl1, Col1LotNo, 80, 20, Col1LotNo, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_LotNo")), Boolean), False)
            .AddAgNumberColumn(Dgl1, Col1BillQty, 60, 8, 4, False, Col1BillQty, True, True, True)
            .AddAgTextColumn(Dgl1, Col1Unit, 50, 0, Col1Unit, True, True)
            .AddAgTextColumn(Dgl1, Col1QtyDecimalPlaces, 50, 0, Col1QtyDecimalPlaces, False, True, False)
            .AddAgNumberColumn(Dgl1, Col1MeasurePerPcs, 70, 8, 4, False, Col1MeasurePerPcs, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_MeasurePerPcs")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_MeasurePerPcs")), Boolean), True)
            .AddAgNumberColumn(Dgl1, Col1TotalMeasure, 70, 8, 4, False, Col1TotalMeasure, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Measure")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_Measure")), Boolean), True)
            .AddAgTextColumn(Dgl1, Col1MeasureUnit, 70, 0, Col1MeasureUnit, True, True)
            .AddAgTextColumn(Dgl1, Col1MeasureDecimalPlaces, 50, 0, Col1MeasureDecimalPlaces, False, True, False)
            .AddAgNumberColumn(Dgl1, Col1Rate_Inv, 70, 8, 2, False, Col1Rate_Inv, True, True, True)
            .AddAgNumberColumn(Dgl1, Col1Rate_Amd, 70, 8, 2, False, Col1Rate_Amd, True, False, True)
            .AddAgNumberColumn(Dgl1, Col1Rate, 70, 8, 2, False, Col1Rate, True, True, True)
            .AddAgNumberColumn(Dgl1, Col1Amount, 70, 8, 2, False, Col1Amount, True, True, True)
            .AddAgTextColumn(Dgl1, Col1Remark, 200, 255, Col1Remark, True, False)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False

        Dgl1.ColumnHeadersHeight = 35
        Dgl1.AgSkipReadOnlyColumns = True

        Dgl1.AllowUserToOrderColumns = True

        AgTemplate.ClsMain.ProcCreateLink(Dgl1, Col1JobOrder)
        AgTemplate.ClsMain.ProcCreateLink(Dgl1, Col1ProdOrder)
        AgTemplate.ClsMain.ProcCreateLink(Dgl1, Col1JobReceive)
        AgTemplate.ClsMain.ProcCreateLink(Dgl1, Col1JobInvoice)

        AgCalcGrid1.Ini_Grid(LblV_Type.Tag, TxtV_Date.Text)

        AgCalcGrid1.AgLineGrid = Dgl1
        AgCalcGrid1.AgLineGridMandatoryColumn = Dgl1.Columns(Col1Item).Index
        AgCalcGrid1.AgLineGridGrossColumn = Dgl1.Columns(Col1Amount).Index

        AgCL.GridSetiingShowXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1, False)
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As SQLiteConnection, ByVal Cmd As SQLiteCommand) Handles Me.BaseEvent_Save_InTrans
        Dim I As Integer, mSr As Integer
        Dim Stock As AgTemplate.ClsMain.StructStock = Nothing, StockProcess As AgTemplate.ClsMain.StructStock = Nothing
        Dim bSelectionQry$ = ""

        mQry = "UPDATE JobInvoice " &
                " SET " &
                " ManualRefNo = " & AgL.Chk_Text(TxtManualRefNo.Text) & ", " &
                " BillingType = " & AgL.Chk_Text(TxtBillingOn.Text) & ", " &
                " Process = " & AgL.Chk_Text(TxtProcess.AgSelectedValue) & ", " &
                " CostCenter = " & AgL.Chk_Text(TxtCostCenter.Tag) & ", " &
                " JobReceiveBy = " & AgL.Chk_Text(TxtOrderBy.Tag) & ", " &
                " FromDate = " & AgL.Chk_Text(TxtFromDate.Text) & ", " &
                " ToDate = " & AgL.Chk_Text(TxtToDate.Text) & ", " &
                " Remarks = " & AgL.Chk_Text(TxtRemarks.Text) & ", " &
                " Structure = " & AgL.Chk_Text(TxtStructure.Tag) & ", " &
                " " & AgCalcGrid1.FFooterTableUpdateStr() & " " &
                " Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        If Topctrl1.Mode <> "Add" Then
            mQry = "Delete From JobInvoiceDetail Where DocId = '" & SearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If

        'Never Try to Serialise Sr in Line Items 
        'As Some other Entry points may updating values to this Search code and Sr
        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1Item, I).Value <> "" Then
                'If Dgl1.Rows(I).DefaultCellStyle.BackColor = mRateAmendedColour Then
                If Val(Dgl1.Item(Col1Rate, I).Value) <> 0 Or Val(AgCalcGrid1.AgChargesValue(AgTemplate.ClsMain.Charges.INCENTIVE, I, AgStructure.AgCalcGrid.LineColumnType.Percentage)) <> 0 Then
                    mSr += 1
                    If bSelectionQry <> "" Then bSelectionQry += " UNION ALL "
                    bSelectionQry += " Select " & AgL.Chk_Text(mSearchCode) & ", " &
                            " " & mSr & ", " & AgL.Chk_Text(Dgl1.Item(Col1JobWorker, I).Tag) & ", 1, " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1Item_Uid, I).Tag) & ", " &
                            " " & AgL.Chk_Text(Dgl1.AgSelectedValue(Col1Item, I)) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1LotNo, I).Value) & ", " &
                            " " & Val(Dgl1.Item(Col1BillQty, I).Value) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1Unit, I).Value) & ", " &
                            " " & Val(Dgl1.Item(Col1MeasurePerPcs, I).Value) & ", " &
                            " " & Val(Dgl1.Item(Col1TotalMeasure, I).Value) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1MeasureUnit, I).Value) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1ProdOrder, I).Tag) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1ProdOrderSr, I).Value) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1JobOrder, I).Tag) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1JobOrderSr, I).Value) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1JobReceive, I).Tag) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1JobReceiveSr, I).Value) & ", " &
                            " " & Val(Dgl1.Item(Col1Rate_Inv, I).Value) & ", " &
                            " " & Val(Dgl1.Item(Col1Rate_Amd, I).Value) & ", " &
                            " " & Val(Dgl1.Item(Col1Rate, I).Value) & ", " &
                            " " & AgTemplate.ClsMain.T_Nature.Amendment & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1Remark, I).Value) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1JobInvoice, I).Tag) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1JobInvoiceSr, I).Value) & ", " &
                            " " & AgCalcGrid1.FLineTableFieldValuesStr(I) & " "
                End If
            End If
        Next

        mQry = "INSERT INTO JobInvoiceDetail(DocId, Sr, JobWorker, AffectRate, Item_Uid, Item, LotNo, DocQty, Unit, MeasurePerPcs, DocMeasure, " &
                " MeasureUnit, ProdOrder, ProdOrderSr, JobOrder, JobOrderSr, JobReceive, JobReceiveSr, Rate_Inv, Rate_Amd, Rate, T_Nature, Remark, " &
                " JobInvoice, JobInvoiceSr, " & AgCalcGrid1.FLineTableFieldNameStr() & ") " & bSelectionQry
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)


        Dim DtTemp As DataTable = Nothing
        mQry = " Select L.JobWorker From JobInvoiceDetail L  Where L.DocId = '" & mSearchCode & "' Group By L.JobWorker "
        DtTemp = AgL.FillData(mQry, AgL.GcnRead).Tables(0)

        If DtTemp.Rows.Count > 0 Then
            For I = 0 To DtTemp.Rows.Count - 1
                If AgL.XNull(DtTemp.Rows(I)("JobWorker")) <> "" Then
                    Call AgTemplate.ClsMain.PostStructureLineToAccounts(AgCalcGrid1, TxtRemarks.Text, mSearchCode, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, TxtDivision.AgSelectedValue,
                             TxtV_Type.AgSelectedValue, LblPrefix.Text, TxtV_No.Text, TxtManualRefNo.Text,
                             AgL.XNull(DtTemp.Rows(I)("JobWorker")), TxtV_Date.Text, Conn, Cmd, TxtCostCenter.Tag)
                End If
            Next
        End If

        Call AccountPosting(Conn, Cmd)

        If AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Or AgL.StrCmp(AgL.PubUserName, "Sa") Then
            AgCL.GridSetiingWriteXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1)
        End If
    End Sub

    Private Function AccountPosting(ByVal Conn As SQLiteConnection, ByVal Cmd As SQLiteCommand) As Boolean
        Dim LedgAry() As AgLibrary.ClsMain.LedgRec
        Dim I As Integer, mSr As Integer = 0
        ' Dim DsTemp As DataSet = Nothing
        Dim mNarr As String = "", mCommonNarr$ = ""

        Dim AmtDr As Double = 0, AmtCr As Double = 0

        Dim GcnRead As SQLiteConnection
        GcnRead = New SQLiteConnection
        GcnRead.ConnectionString = AgL.Gcn_ConnectionString
        GcnRead.Open()


        mCommonNarr = ""
        mCommonNarr = TxtRemarks.Text
        If mCommonNarr.Length > 255 Then mCommonNarr = AgL.MidStr(mCommonNarr, 0, 255)
        mNarr = TxtRemarks.Text
        If mNarr.Length > 255 Then mNarr = AgL.MidStr(mNarr, 0, 255)

        AgL.LedgerUnPost(Conn, Cmd, mSearchCode)

        ReDim Preserve LedgAry(I)

        mQry = " INSERT INTO LedgerM(	DocId,	Site_Code,	V_No,	V_Type,	V_Prefix,	V_Date, " &
                " SubCode,	Narration,	U_Name,	U_EntDt,	U_AE) " &
                " VALUES ('" & mInternalCode & "', " & AgL.Chk_Text(TxtSite_Code.AgSelectedValue) & ",	" & Val(TxtV_No.Text) & "," &
                " " & AgL.Chk_Text(TxtV_Type.AgSelectedValue) & ",	" & AgL.Chk_Text(LblPrefix.Text) & ", " &
                " " & AgL.Chk_Text(TxtV_Date.Text) & ",	" & AgL.Chk_Text(TxtProcessAc.Tag) & ",	" &
                " " & AgL.Chk_Text(TxtRemarks.Text) & ",	" & AgL.Chk_Text(AgL.PubUserName) & ",	" &
                " " & AgL.Chk_Text(AgL.PubLoginDate) & ", " & AgL.Chk_Text(AgL.MidStr(Topctrl1.Mode, 0, 1)) & ") "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mSr = 1
        mQry = " INSERT INTO Ledger(DocId, V_SNo, V_No, V_Type, V_Prefix, V_Date, CostCenter, SubCode, ContraSub, AmtDr, AmtCr, Narration, " &
                " Site_Code, U_Name,	U_EntDt, U_AE, DivCode, RecId) " &
                " SELECT H.DocId, " & mSr & " AS V_SNo, max(H.V_No) AS V_No, Max(H.V_Type) AS V_Type, Max(H.V_Prefix) AS V_Prefix, Max(H.V_Date) AS V_Date, " &
                " " & AgL.Chk_Text(TxtCostCenter.Tag) & ", " & AgL.Chk_Text(TxtProcessAc.Tag) & " AS SubCode, Max(L.JobWorker) AS ContraSub,sum(L.NetAmount) AS  AmtDr, 0 AS AmtCr,  " &
                " max(H.Remarks) AS Narration, max(H.Site_Code) AS Site_Code, Max(H.EntryBy) EntryBy, Max(H.EntryDate) AS EntryDate, substring( Max(H.EntryType),1,1) AS EntryType, Max(H.Div_Code) AS Div_Code, Max(H.ManualRefNo) As RecId   " &
                " FROM JobInvoiceDetail L  " &
                " LEFT JOIN JobInvoice H ON H.DocID = L.DocId  " &
                " WHERE L.DocID ='" & mInternalCode & "' " &
                " GROUP BY H.DocID "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "INSERT INTO Ledger(DocId, V_SNo, V_No, V_Type, V_Prefix, V_Date, CostCenter, SubCode, ContraSub, AmtDr, AmtCr, Narration, " &
                " Site_Code, U_Name,	U_EntDt, U_AE, DivCode, RecId) " &
                " SELECT H.DocId,row_number() OVER (ORDER BY L.JobWorker) + " & mSr & " AS V_SNo, max(H.V_No) AS V_No, Max(H.V_Type) AS V_Type, " &
                " Max(H.V_Prefix) AS V_Prefix, Max(H.V_Date) AS V_Date, " & AgL.Chk_Text(TxtCostCenter.Tag) & ", " &
                " L.JobWorker AS SubCode, " & AgL.Chk_Text(TxtProcessAc.Tag) & " AS ContraSub, 0 AS AmtDr, Sum(L.NetAmount) AS  AmtCr, " &
                " max(H.Remarks) AS Narration, max(H.Site_Code) AS Site_Code, Max(H.EntryBy) EntryBy, Max(H.EntryDate) AS EntryDate, substring( Max(H.EntryType),1,1) AS EntryType, Max(H.Div_Code) AS Div_Code, Max(H.ManualRefNo) As RecId   " &
                " FROM JobInvoiceDetail L  " &
                " LEFT JOIN JobInvoice H ON H.DocID = L.DocId  " &
                " WHERE L.DocID ='" & mInternalCode & "' " &
                " GROUP BY H.DocID, L.JobWorker  "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    End Function

    Private Sub FrmProductionOrder_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim I As Integer
        Dim DsTemp As DataSet

        LblTotalAmount.Text = 0

        mQry = "Select J.*, G.Description As GodownDesc, P.Description As ProcessDesc, P.SubCode AS ProcessAc, CCM.Name as CostCenterDesc, " &
                " Sg.DispName As JobWorkerName, Sg1.DispName As JobReceiveByName " &
                " From JobInvoice J  " &
                " LEFT JOIN Godown G  On J.Godown = G.Code " &
                " LEFT JOIN Process P  On J.Process = P.NCat " &
                " LEFT JOIN SubGroup SG  On J.JobWorker = Sg.SubCode " &
                " LEFT JOIN SubGroup Sg1  On J.JobReceiveBy = Sg1.SubCode " &
                " LEFT JOIN CostCenterMast CCM  On J.CostCenter = CCM.Code " &
                " Where J.DocID = '" & SearchCode & "'"
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                TxtStructure.Tag = AgL.XNull(.Rows(0)("Structure"))
                If TxtStructure.Tag = "" Then
                    If AgL.XNull(DtV_TypeSettings.Rows(0)("Structure")) = "" Then
                        TxtStructure.Tag = AgStructure.ClsMain.FGetStructureFromNCat(LblV_Type.Tag, AgL.GcnRead)
                    Else
                        TxtStructure.Tag = AgL.XNull(DtV_TypeSettings.Rows(0)("Structure"))
                    End If
                End If

                AgCalcGrid1.FrmType = Me.FrmType
                AgCalcGrid1.AgStructure = TxtStructure.Tag
                IniGrid()

                TxtFromDate.Text = AgL.XNull(.Rows(0)("FromDate"))
                TxtToDate.Text = AgL.XNull(.Rows(0)("ToDate"))

                TxtManualRefNo.Text = AgL.XNull(.Rows(0)("ManualRefNo"))

                TxtProcess.Tag = AgL.XNull(.Rows(0)("Process"))
                TxtProcess.Text = AgL.XNull(.Rows(0)("ProcessDesc"))
                TxtProcessAc.Tag = AgL.XNull(.Rows(0)("ProcessAc"))
                TxtCostCenter.Tag = AgL.XNull(.Rows(0)("CostCenter"))
                TxtCostCenter.Text = AgL.XNull(.Rows(0)("CostCenterDesc"))

                TxtBillingOn.Text = AgL.XNull(.Rows(0)("BillingType"))

                TxtOrderBy.Tag = AgL.XNull(.Rows(0)("JobReceiveBy"))
                TxtOrderBy.Text = AgL.XNull(.Rows(0)("JobReceiveByName"))

                TxtRemarks.Text = AgL.XNull(.Rows(0)("Remarks"))

                AgCalcGrid1.FMoveRecFooterTable(DsTemp.Tables(0), LblV_Type.Tag, TxtV_Date.Text)

                '-------------------------------------------------------------
                'Line Records are showing in Grid
                '-------------------------------------------------------------

                mQry = "Select L.*, I.Description As ItemDesc, IU.Item_UID as Item_Uid_Desc, J.V_Type + '-' + J.ManualRefNo As JobOrderNo, " &
                        " IFNull(J.JobWithMaterialYN,0) As JobWithMaterialYN, " &
                        " U.DecimalPlaces as QtyDecimalPlaces, MU.DecimalPlaces as MeasureDecimalPlaces, " &
                        " P.ManualRefNo As ProdOrderNo, Sg.Name As JobWorkerName, " &
                        " J.V_Type + '-' + J.ManualRefNo As JobOrderNo, " &
                        " R.V_Type + '-' + R.ManualRefNo As JobReceiveNo, " &
                        " Ji.V_Type + '-' + Ji.ManualRefNo As JobInvoiceNo " &
                        " From JobInvoiceDetail L  " &
                        " LEFT JOIN Item I  On L.Item = I.Code " &
                        " Left Join Item_UID IU   On L.Item_UID = IU.Code " &
                        " LEFT JOIN ProdOrder P  On L.ProdOrder = P.DocId " &
                        " LEFT JOIN JobOrder J  On L.JobOrder = J.DocId " &
                        " LEFT JOIN JobIssRec R  On L.JobReceive = R.DocId " &
                        " LEFT JOIN JobInvoice Ji  On L.JobInvoice = Ji.DocId " &
                        " LEFT JOIN SubGroup Sg On L.JobWorker = Sg.SubCode " &
                        " Left Join Unit U  On L.Unit = U.Code " &
                        " Left Join Unit MU  On L.MeasureUnit = MU.Code " &
                        " Where L.DocId = '" & SearchCode & "' Order By L.Sr"
                DsTemp = AgL.FillData(mQry, AgL.GCn)
                With DsTemp.Tables(0)
                    Dgl1.RowCount = 1
                    Dgl1.Rows.Clear()
                    If .Rows.Count > 0 Then
                        For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                            Dgl1.Rows.Add()
                            Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count - 1

                            Dgl1.Item(Col1JobWorker, I).Tag = AgL.XNull(.Rows(I)("JobWorker"))
                            Dgl1.Item(Col1JobWorker, I).Value = AgL.XNull(.Rows(I)("JobWorkerName"))

                            Dgl1.Item(Col1Item_Uid, I).Tag = AgL.XNull(.Rows(I)("Item_Uid"))
                            Dgl1.Item(Col1Item_Uid, I).Value = AgL.XNull(.Rows(I)("Item_Uid_Desc"))

                            Dgl1.Item(Col1Item, I).Tag = AgL.XNull(.Rows(I)("Item"))
                            Dgl1.Item(Col1Item, I).Value = AgL.XNull(.Rows(I)("ItemDesc"))
                            Dgl1.Item(Col1LotNo, I).Value = AgL.XNull(.Rows(I)("LotNo"))
                            Dgl1.Item(Col1BillQty, I).Value = Format(AgL.VNull(.Rows(I)("DocQty")), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                            Dgl1.Item(Col1QtyDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("QtyDecimalPlaces"))
                            Dgl1.Item(Col1MeasurePerPcs, I).Value = Format(AgL.VNull(.Rows(I)("MeasurePerPcs")), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1TotalMeasure, I).Value = Format(AgL.VNull(.Rows(I)("DocMeasure")), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1MeasureUnit, I).Value = AgL.XNull(.Rows(I)("MeasureUnit"))
                            Dgl1.Item(Col1MeasureDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("MeasureDecimalPlaces"))

                            Dgl1.Item(Col1JobOrder, I).Tag = AgL.XNull(.Rows(I)("JobOrder"))
                            Dgl1.Item(Col1JobOrder, I).Value = AgL.XNull(.Rows(I)("JobOrderNo"))
                            Dgl1.Item(Col1JobOrderSr, I).Value = AgL.XNull(.Rows(I)("JobOrderSr"))

                            Dgl1.Item(Col1ProdOrder, I).Tag = AgL.XNull(.Rows(I)("ProdOrder"))
                            Dgl1.Item(Col1ProdOrder, I).Value = AgL.XNull(.Rows(I)("ProdOrderNo"))
                            Dgl1.Item(Col1ProdOrderSr, I).Value = AgL.XNull(.Rows(I)("ProdOrderSr"))

                            Dgl1.Item(Col1JobReceive, I).Tag = AgL.XNull(.Rows(I)("JobReceive"))
                            Dgl1.Item(Col1JobReceive, I).Value = AgL.XNull(.Rows(I)("JobReceiveNo"))
                            Dgl1.Item(Col1JobReceiveSr, I).Value = AgL.XNull(.Rows(I)("JobReceiveSr"))

                            Dgl1.Item(Col1JobInvoice, I).Tag = AgL.XNull(.Rows(I)("JobInvoice"))
                            Dgl1.Item(Col1JobInvoice, I).Value = AgL.XNull(.Rows(I)("JobInvoiceNo"))
                            Dgl1.Item(Col1JobInvoiceSr, I).Value = AgL.XNull(.Rows(I)("JobInvoiceSr"))

                            Dgl1.Item(Col1Rate_Inv, I).Value = Format(AgL.VNull(.Rows(I)("Rate_Inv")), "0.00")
                            Dgl1.Item(Col1Rate_Amd, I).Value = Format(AgL.VNull(.Rows(I)("Rate_Amd")), "0.00")

                            Dgl1.Item(Col1Rate, I).Value = Format(AgL.VNull(.Rows(I)("Rate")), "0.00")
                            Dgl1.Item(Col1Amount, I).Value = AgL.VNull(.Rows(I)("Amount"))
                            Dgl1.Item(Col1Remark, I).Value = AgL.XNull(.Rows(I)("Remark"))

                            LblTotalAmount.Text = Val(LblTotalAmount.Text) + Val(Dgl1.Item(Col1Amount, I).Value)

                            Call AgCalcGrid1.FMoveRecLineTable(DsTemp.Tables(0), I)
                        Next I
                    End If
                End With
            End If
        End With
    End Sub

    Private Sub FrmProductionOrder_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Topctrl1.ChangeAgGridState(Dgl1, False)
        AgCalcGrid1.FrmType = Me.FrmType
        AgL.WinSetting(Me, 648, 971, 0, 0)
    End Sub

    Private Sub Txt_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtV_Type.Validating, TxtV_Date.Validating, TxtManualRefNo.Validating, TxtProcess.Validating, TxtBillingOn.Validating
        Dim DtTemp As DataTable = Nothing
        Try
            Select Case sender.NAME
                Case TxtV_Type.Name
                    If AgL.XNull(DtV_TypeSettings.Rows(0)("Structure")) = "" Then
                        TxtStructure.AgSelectedValue = AgStructure.ClsMain.FGetStructureFromNCat(LblV_Type.Tag, AgL.GcnRead)
                        AgCalcGrid1.AgStructure = TxtStructure.AgSelectedValue
                    Else
                        TxtStructure.Tag = AgL.XNull(DtV_TypeSettings.Rows(0)("Structure"))
                        AgCalcGrid1.AgStructure = AgL.XNull(DtV_TypeSettings.Rows(0)("Structure"))
                    End If

                    IniGrid()
                    TxtManualRefNo.Text = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "JobInvoice", TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, AgTemplate.ClsMain.ManualRefType.Max)
                    If Dgl1.AgHelpDataSet(Col1Item) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1Item).Dispose() : Dgl1.AgHelpDataSet(Col1Item) = Nothing
                    If Dgl1.AgHelpDataSet(Col1JobWorker) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1JobWorker).Dispose() : Dgl1.AgHelpDataSet(Col1JobWorker) = Nothing
                    FAsignProcess()

                Case TxtProcess.Name
                    TxtBillingOn.Text = AgL.XNull(AgL.Dman_Execute(" SELECT H.DefaultBillingType FROM Process H  WHERE H.NCat = '" & TxtProcess.AgSelectedValue & "' ", AgL.GCn).ExecuteScalar)

                Case TxtV_Date.Name
                    If Topctrl1.Mode = "Add" Then
                        TxtManualRefNo.Text = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "JobInvoice", TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, AgTemplate.ClsMain.ManualRefType.Max)
                    End If

                Case TxtManualRefNo.Name
                    e.Cancel = Not AgTemplate.ClsMain.FCheckDuplicateRefNo("ManualRefNo", "JobInvoice", TxtV_Type.Tag, TxtV_Date.Text, TxtDivision.Tag, TxtSite_Code.Tag, Topctrl1.Mode, TxtManualRefNo.Text, mSearchCode)

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
                mQry = "Select P.NCat, P.Description, P.CostCenter, P.SubCode, Cm.Name As CostCenterName from Process P LEFT JOIN CostCenterMast Cm On P.CostCenter = Cm.Code Where P.NCat= '" & Replace(AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Process")), "|", "") & "'  "
                DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
                If DtTemp.Rows.Count > 0 Then
                    TxtProcess.Tag = AgL.XNull(DtTemp.Rows(0)("NCat"))
                    TxtProcess.Text = AgL.XNull(DtTemp.Rows(0)("Description"))
                    TxtCostCenter.Tag = AgL.XNull(DtTemp.Rows(0)("CostCenter"))
                    TxtCostCenter.Text = AgL.XNull(DtTemp.Rows(0)("CostCenterName"))
                    TxtProcessAc.Tag = AgL.XNull(DtTemp.Rows(0)("SubCode"))
                    TxtProcess.Enabled = False
                End If
            Else
                TxtProcess.Enabled = True
            End If
        End If
        TxtBillingOn.Text = AgL.XNull(AgL.Dman_Execute(" SELECT H.DefaultBillingType FROM Process H  WHERE H.NCat = '" & TxtProcess.AgSelectedValue & "' ", AgL.GCn).ExecuteScalar)
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_Topctrl_tbAdd() Handles Me.BaseEvent_Topctrl_tbAdd
        If AgL.XNull(DtV_TypeSettings.Rows(0)("Structure")) = "" Then
            TxtStructure.AgSelectedValue = AgStructure.ClsMain.FGetStructureFromNCat(LblV_Type.Tag, AgL.GcnRead)
            AgCalcGrid1.AgStructure = TxtStructure.AgSelectedValue
        Else
            TxtStructure.Tag = AgL.XNull(DtV_TypeSettings.Rows(0)("Structure"))
            AgCalcGrid1.AgStructure = AgL.XNull(DtV_TypeSettings.Rows(0)("Structure"))
        End If
        'AgCalcGrid1.AgNCat = LblV_Type.Tag


        IniGrid()

        'TxtProcess.Tag = AgL.Dman_Execute(" SELECT H.NCat FROM Process H  WHERE H.ProcessReceiveNCat = '" & EntryNCat & "' ", AgL.GCn).ExecuteScalar
        TxtBillingOn.Text = AgL.XNull(AgL.Dman_Execute(" SELECT H.DefaultBillingType FROM Process H  WHERE H.NCat = '" & TxtProcess.AgSelectedValue & "' ", AgL.GCn).ExecuteScalar)
        TxtManualRefNo.Text = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "JobInvoice", TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, AgTemplate.ClsMain.ManualRefType.Max)

        RbtForJobInvoice.Checked = True
        FAsignProcess()
    End Sub

    Private Sub DGL1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.KeyDown
        If e.Control And e.KeyCode = Keys.D Then
            sender.CurrentRow.Selected = True
        End If
        If e.Control Or e.Shift Or e.Alt Then Exit Sub
    End Sub

    Private Sub Dgl1_EditingControl_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Dgl1.EditingControl_Validating
        Dim I As Integer = 0
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

                Case Col1Item_Uid
                    ErrMsgStr = FCheck_Item_UID(Dgl1.Item(Col1Item_Uid, mRowIndex).Value, Dgl1.Item(Col1JobWorker, mRowIndex).Tag)
                    If ErrMsgStr <> "" Then
                        MsgBox(ErrMsgStr)
                        Dgl1.Item(Col1Item_Uid, Dgl1.CurrentCell.RowIndex).Value = ""
                        Dgl1.Item(Col1Item_Uid, Dgl1.CurrentCell.RowIndex).Tag = ""
                        Exit Sub
                    End If
                    Validating_Item_Uid(Dgl1.Item(Col1Item_Uid, Dgl1.CurrentCell.RowIndex).Value, Dgl1.CurrentCell.RowIndex)

                Case Col1Rate_Amd
                    If Dgl1.CurrentCell.RowIndex = 0 Then
                        If MsgBox("Apply To All ?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
                            For I = 0 To Dgl1.Rows.Count - 1
                                If Dgl1.Item(Col1Item, I).Value <> "" Then
                                    Dgl1.Item(Col1Rate_Amd, I).Value = Dgl1.Item(Col1Rate_Amd, 0).Value
                                End If
                            Next
                        End If
                    End If

                Case "Incentive @"
                    If Dgl1.CurrentCell.RowIndex = 0 Then
                        If MsgBox("Apply To All ?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
                            For I = 0 To Dgl1.Rows.Count - 1
                                If Dgl1.Item(Col1Item, I).Value <> "" Then
                                    'Dgl1.Item(Col1Rate_Amd, I).Value = Dgl1.Item(Col1Rate_Amd, 0).Value
                                    AgCalcGrid1.AgChargesValue(AgTemplate.ClsMain.Charges.INCENTIVE, I, AgStructure.AgCalcGrid.LineColumnType.Percentage) = AgCalcGrid1.AgChargesValue(AgTemplate.ClsMain.Charges.INCENTIVE, 0, AgStructure.AgCalcGrid.LineColumnType.Percentage)
                                End If
                            Next
                        End If
                    End If

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
                Dgl1.Item(Col1MeasurePerPcs, mRow).Value = 0
                Dgl1.Item(Col1MeasureUnit, mRow).Value = ""
                Dgl1.AgSelectedValue(Col1JobOrder, mRow) = ""
                Dgl1.Item(Col1Rate, mRow).Value = 0
            Else
                If Dgl1.AgDataRow IsNot Nothing Then
                    Dgl1.Item(Col1Item_Uid, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("Item_UidCode").Value)
                    Dgl1.Item(Col1Item_Uid, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("Item_UidDesc").Value)

                    Dgl1.Item(Col1BillQty, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("Qty").Value)
                    Dgl1.Item(Col1Rate_Inv, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("Rate").Value)
                    Dgl1.Item(Col1Unit, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("Unit").Value)
                    Dgl1.Item(Col1QtyDecimalPlaces, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("QtyDecimalPlaces").Value)

                    Dgl1.Item(Col1MeasurePerPcs, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("MeasurePerPcs").Value)
                    Dgl1.Item(Col1MeasureUnit, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("MeasureUnit").Value)
                    Dgl1.Item(Col1MeasureDecimalPlaces, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("MeasureDecimalPlaces").Value)

                    Dgl1.Item(Col1JobWorker, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("JobWorker").Value)
                    Dgl1.Item(Col1JobWorker, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("JobWorkerName").Value)

                    Dgl1.Item(Col1JobOrder, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("JobOrder").Value)
                    Dgl1.Item(Col1JobOrder, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("JobOrderNo").Value)
                    Dgl1.Item(Col1JobOrderSr, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("JobOrderSr").Value)

                    Dgl1.Item(Col1ProdOrder, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("ProdOrder").Value)
                    Dgl1.Item(Col1ProdOrder, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("ProdOrderNo").Value)
                    Dgl1.Item(Col1ProdOrderSr, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("ProdOrderSr").Value)

                    Dgl1.Item(Col1JobReceive, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("JobReceive").Value)
                    Dgl1.Item(Col1JobReceive, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("JobReceiveNo").Value)
                    Dgl1.Item(Col1JobReceiveSr, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("JobReceiveSr").Value)

                    Dgl1.Item(Col1JobInvoice, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("JobInvoice").Value)
                    Dgl1.Item(Col1JobInvoice, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("JobInvoiceNo").Value)
                    Dgl1.Item(Col1JobInvoiceSr, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("JobInvoiceSr").Value)
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

        LblTotalAmount.Text = 0

        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1Item, I).Value <> "" Then
                Dgl1.Item(Col1TotalMeasure, I).Value = Format(Val(Dgl1.Item(Col1BillQty, I).Value) * Val(Dgl1.Item(Col1MeasurePerPcs, I).Value), "0.0000")
                Dgl1.Item(Col1Rate, I).Value = Val(Dgl1.Item(Col1Rate_Amd, I).Value) - Val(Dgl1.Item(Col1Rate_Inv, I).Value)



                If AgL.StrCmp(TxtBillingOn.Text, "Qty") Or TxtBillingOn.Text = "" Then
                    Dgl1.Item(Col1Amount, I).Value = Format(Val(Dgl1.Item(Col1BillQty, I).Value) * Val(Dgl1.Item(Col1Rate, I).Value), "0.".PadRight(CType(Dgl1.Columns(Col1Amount), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                ElseIf AgL.StrCmp(TxtBillingOn.Text, "Measure") Or AgL.StrCmp(TxtBillingOn.Text, "Area") Then
                    Dgl1.Item(Col1Amount, I).Value = Format(Val(Dgl1.Item(Col1TotalMeasure, I).Value) * Val(Dgl1.Item(Col1Rate, I).Value), "0.".PadRight(CType(Dgl1.Columns(Col1Amount), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                End If

                If Val(Dgl1.Item(Col1Rate_Amd, I).Value) <> Val(Dgl1.Item(Col1Rate_Inv, I).Value) And Val(Dgl1.Item(Col1Rate_Amd, I).Value) <> 0 Then
                    Dgl1.Rows(I).DefaultCellStyle.BackColor = mRateAmendedColour
                Else
                    Dgl1.Rows(I).DefaultCellStyle.BackColor = Color.White
                End If

                LblTotalAmount.Text = Val(LblTotalAmount.Text) + Val(Dgl1.Item(Col1Amount, I).Value)
            End If
        Next
        AgCalcGrid1.Calculation()
        LblTotalAmount.Text = Val(LblTotalAmount.Text)
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        Dim I As Integer = 0
        Dim DrTemp() As DataRow = Nothing
        Dim DtTemp As DataTable = Nothing
        'Dim mmsgStr$ = ""

        If AgL.RequiredField(TxtManualRefNo, LblManualRefNo.Text) Then passed = False : Exit Sub
        If AgL.RequiredField(TxtFromDate, "From Date") Then passed = False : Exit Sub
        If AgL.RequiredField(TxtToDate, "To Date") Then passed = False : Exit Sub
        If AgCL.AgIsBlankGrid(Dgl1, Dgl1.Columns(Col1Item).Index) = True Then passed = False : Exit Sub
        If AgCL.AgIsDuplicate(Dgl1, "" + Dgl1.Columns(Col1Item_Uid).Index.ToString + "," + Dgl1.Columns(Col1Item).Index.ToString + "," + Dgl1.Columns(Col1ProdOrder).Index.ToString + "," + Dgl1.Columns(Col1JobOrder).Index.ToString + "," + Dgl1.Columns(Col1JobInvoice).Index.ToString + "") Then passed = False : Exit Sub

        If CDate(TxtV_Date.Text) < CDate(TxtFromDate.Text) Or CDate(TxtV_Date.Text) > CDate(TxtToDate.Text) Then
            MsgBox("Entry Date should between From Date & To Date.") : passed = False : Exit Sub
        End If

        For I = 0 To Dgl1.Rows.Count - 1
            If Dgl1.Item(Col1Item, I).Value <> "" Then
                If Val(Dgl1.Item(Col1Rate_Amd, I).Value) = 0 Then
                    MsgBox("Rate_Amd value is 0 at row no " & Dgl1.Item(ColSNo, I).Value & "", MsgBoxStyle.Exclamation)
                    Dgl1.CurrentCell = Dgl1.Item(Col1Rate_Amd, I) : Dgl1.Focus()
                    passed = False : Exit Sub
                End If

                'mQry = "SELECT IFNull(count(*),0) AS Cnt FROM JobInvoiceDetail L  LEFT JOIN Jobinvoice H ON H.DocId = L.DocID WHERE L.JobInvoice = " & AgL.Chk_Text(Dgl1.Item(Col1JobInvoice, I).Tag) & " AND L.JobInvoiceSr = " & Dgl1.Item(Col1JobInvoiceSr, I).Value & " AND H.V_TYpe = '" & TxtV_Type.Tag & "' AND H.DocID <> '" & mInternalCode & "' "
                'If AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar) > 0 Then
                '    mmsgStr = mmsgStr & "Amendment is already done for Item " & Dgl1.Item(Col1Item, I).Value & " and Invoice No " & Dgl1.Item(Col1JobInvoice, I).Value & " at row no " & Dgl1.Item(ColSNo, I).Value & "" & vbCrLf
                'End If

            End If
        Next

        'If mmsgStr <> "" Then
        '    If MsgBox(mmsgStr & "Do You Want to Countinue?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
        '        Dgl1.Focus()
        '        passed = False : Exit Sub
        '    End If
        'End If

        passed = AgTemplate.ClsMain.FCheckDuplicateRefNo("ManualRefNo", "JobInvoice", TxtV_Type.Tag, TxtV_Date.Text, TxtDivision.Tag, TxtSite_Code.Tag, Topctrl1.Mode, TxtManualRefNo.Text, mSearchCode)
    End Sub

    Private Sub FrmProductionOrder_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
        Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
    End Sub

    Private Sub TempJobOrder_BaseEvent_ApproveDeletion_InTrans(ByVal SearchCode As String, ByVal Conn As SQLiteConnection, ByVal Cmd As SQLiteCommand) Handles Me.BaseEvent_ApproveDeletion_InTrans
        AgL.LedgerUnPost(Conn, Cmd, SearchCode)
    End Sub

    Private Sub TempJobReceive_BaseEvent_Topctrl_tbRef() Handles Me.BaseEvent_Topctrl_tbRef
        Try
            If TxtProcess.AgHelpDataSet IsNot Nothing Then TxtProcess.AgHelpDataSet.Dispose() : TxtProcess.AgHelpDataSet = Nothing
            If Dgl1.AgHelpDataSet(Col1Item) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1Item).Dispose() : Dgl1.AgHelpDataSet(Col1Item) = Nothing
            If Dgl1.AgHelpDataSet(Col1JobOrder) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1JobOrder).Dispose() : Dgl1.AgHelpDataSet(Col1JobOrder) = Nothing
            If TxtOrderBy.AgHelpDataSet IsNot Nothing Then TxtOrderBy.AgHelpDataSet.Dispose() : TxtOrderBy.AgHelpDataSet = Nothing
        Catch ex As Exception
        End Try
    End Sub

    Private Sub TxtJobWorker_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtProcess.KeyDown, TxtOrderBy.KeyDown
        Try
            Select Case sender.name
                Case TxtProcess.Name
                    If e.KeyCode <> Keys.Enter Then
                        If TxtProcess.AgHelpDataSet Is Nothing Then
                            mQry = "Select P.NCat As Code, Vc.NCatDescription As Process, P.Div_Code " &
                                  " From Process P  " &
                                  " LEFT JOIN VoucherCat Vc  On P.NCat  = Vc.NCat " &
                                  " Order By Vc.NCatDescription "
                            TxtProcess.AgHelpDataSet(1, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If

                Case TxtOrderBy.Name
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
                            FCreateHelpItem()
                        End If
                    End If

                Case Col1JobWorker
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1JobWorker) Is Nothing Then
                            mQry = " SELECT Sg.SubCode AS Code, Sg.Name AS JobWorker, H.Process, " &
                                     " IFNull(Sg.IsDeleted,0) AS IsDeleted,  SG.Div_Code, " &
                                     " IFNull(Sg.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') As Status " &
                                     " FROM SubGroup Sg  " &
                                     " LEFT JOIN JobWorkerProcess H   On Sg.SubCode = H.SubCode  " &
                                     " Where IFNull(Sg.IsDeleted,0) = 0 " &
                                     " And Sg.Status = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " &
                                     " And CharIndex('|' + '" & TxtDivision.Tag & "' + '|', IFNull(Sg.DivisionList,'|' + '" & TxtDivision.Tag & "' + '|')) > 0 " &
                                     " And H.Process = '" & TxtProcess.Tag & "' "
                            Dgl1.AgHelpDataSet(Col1JobWorker, 4) = AgL.FillData(mQry, AgL.GCn)
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

        mQry = " SELECT Max(L.Item) As Code, " &
                    " Max(I.Description) as Description, " &
                    " Max(Sg.Name) As JobWorkerName,  " &
                    " Sum(L.BillQty) As Qty, Max(I.Unit) as Unit,   " &
                    " Max(Ig.Description) as ItemGroup, " &
                    " Max(I.ManualCode) As ManualCode,   " &
                    " Max(L.Item_Uid) as Item_UidCode, " &
                    " Max(Iu.Item_Uid) as Item_UidDesc, " &
                    " Max(I.MeasureUnit) MeasureUnit, Max(L.Rate) as Rate,   " &
                    " Max(L.MeasurePerPcs) as MeasurePerPcs, " &
                    " L.JobInvoice, L.JobInvoiceSr, Max(H.V_Type) + '-' +  Max(H.ManualRefNo) AS JobInvoiceNo,   " &
                    " Max(L.JobReceive) As JobReceive, Max(L.JobReceiveSr) As JobReceiveSr, Max(H.V_Type) + '-' +  Max(Ir.ManualRefNo) As JobReceiveNo, " &
                    " Max(L.JobOrder) As JobOrder, Max(L.JobOrderSr) As JobOrderSr, Max(H.V_Type) + '-' +  Max(Jo.ManualRefNo) As JobOrderNo, " &
                    " Max(L.ProdOrder) As ProdOrder, Max(L.ProdOrderSr) As ProdOrderSr, Max(H.V_Type) + '-' +  Max(Po.ManualRefNo) As ProdOrderNo, " &
                    " Max(H.V_Date) as JobInvoiceDate,  " &
                    " Max(U.DecimalPlaces) as QtyDecimalPlaces,  " &
                    " Max(U1.DecimalPlaces) as MeasureDecimalPlaces, " &
                    " Max(Iu.Item_Uid) As Item_UidDesc, " &
                    " Max(H.JobWorker) As JobWorker " &
                    " FROM (  " &
                    "     SELECT DocID, V_Type, ManualRefNo, V_Date, JobWorker   " &
                    "     FROM JobInvoice  " &
                    "     Where 1=1 And Site_Code = '" & TxtSite_Code.Tag & "' " &
                    "     And Div_Code = '" & TxtDivision.Tag & "'  " &
                    "     And Process = '" & TxtProcess.Tag & "'  " &
                    "     And V_Date Between '" & TxtFromDate.Text & "' And '" & TxtToDate.Text & "' " &
                    "     ) H   " &
                    " LEFT JOIN JobInvoiceDetail L  ON H.DocID = L.JobInvoice " &
                    " LEFT JOIN JobIssRec Ir On L.JobReceive = Ir.DocId " &
                    " LEFT JOIN JobOrder Jo On L.JobOrder = Jo.DocId " &
                    " LEFT JOIN ProdOrder Po  ON L.ProdOrder = Po.DocId " &
                    " LEFT JOIN SubGroup Sg On H.JobWorker = Sg.SubCode " &
                    " Left Join Item I  On L.Item  = I.Code   " &
                    " LEFT JOIN ItemGroup Ig ON I.ItemGroup = Ig.Code " &
                    " LEFT JOIN Item_Uid Iu On L.Item_Uid = Iu.Code " &
                    " LEFT JOIN Voucher_Type Vt  ON H.V_Type = Vt.V_Type    " &
                    " LEFT JOIN Unit U  On L.Unit = U.Code   " &
                    " LEFT JOIN Unit U1  On L.MeasureUnit = U1.Code   " &
                    " WHERE 1 = 1 " &
                    " GROUP BY L.JobInvoice, L.JobInvoiceSr "
        Dgl1.AgHelpDataSet(Col1Item, 22) = AgL.FillData(mQry, AgL.GCn)
    End Sub

    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub

    Private Sub BtnFillSaleChallan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnFillJobInvoice.Click
        Try
            If Topctrl1.Mode = "Browse" Then Exit Sub
            Dim StrTicked As String

            If RbtForJobInvoice.Checked Then
                StrTicked = FFillJobInvoiceSelection()
            Else
                StrTicked = FFillJobInvoiceItemSelection()
            End If

            If StrTicked <> "" Then
                FFillItemsForJobInvoice(StrTicked)
            Else
                Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
            End If

            Dgl1.Focus()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function FFillJobInvoiceSelection() As String
        Dim FRH_Multiple As DMHelpGrid.FrmHelpGrid_Multi
        Dim StrRtn As String = ""

        Dim strCond As String = ""

        strCond = " And Process = '" & TxtProcess.Tag & "' " &
                    " And Div_Code = '" & TxtDivision.Tag & "'   " &
                    " AND Site_Code = '" & TxtSite_Code.Tag & "'   " &
                    " AND V_Date Between '" & TxtFromDate.Text & "' And '" & TxtToDate.Text & "' "

        mQry = " SELECT 'o' As Tick, VMain.JobInvoice, Max(VMain.JobInvoiceNo) AS JobInvoiceNo, " &
                " Max(VMain.JobInvoiceDate) AS JobInvoiceDate, Max(VMain.JobWorkerName) AS JobWorkerName   " &
                " FROM ( " & FRetFillJobInvoiceItemWiseQry(strCond, "") & " ) As VMain " &
                " GROUP BY VMain.JobInvoice " &
                " Order By JobInvoiceDate "
        FRH_Multiple = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(AgL.FillData(mQry, AgL.GCn).TABLES(0)), "", 400, 600, , , False)
        FRH_Multiple.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple.FFormatColumn(1, , 0, , False)
        FRH_Multiple.FFormatColumn(2, "Invoice No.", 150, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(3, "Invoice Date", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(4, "Job Worker", 200, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.StartPosition = FormStartPosition.CenterScreen
        FRH_Multiple.ShowDialog()

        If FRH_Multiple.BytBtnValue = 0 Then
            StrRtn = FRH_Multiple.FFetchData(1, "'", "'", ",", True)
        End If
        FFillJobInvoiceSelection = StrRtn

        FRH_Multiple = Nothing
    End Function


    Private Function FFillJobInvoiceItemSelection() As String
        Dim FRH_Multiple As DMHelpGrid.FrmHelpGrid_Multi
        Dim StrRtn As String = ""

        Dim strCond As String = ""

        strCond = " And Process = '" & TxtProcess.Tag & "' " &
                    " And Div_Code = '" & TxtDivision.Tag & "'   " &
                    " AND Site_Code = '" & TxtSite_Code.Tag & "'   " &
                    " AND V_Date Between '" & TxtFromDate.Text & "' And '" & TxtToDate.Text & "' "

        mQry = " SELECT 'o' As Tick, VMain.JobInvoice + VMain.Code As JobInvoicePlusItem, " &
                " Max(VMain.JobInvoiceNo) AS JobInvoiceNo, " &
                " Max(VMain.JobInvoiceDate) AS JobInvoiceDate, Max(VMain.Description) As ItemDesc, " &
                " Max(VMain.ItemGroup) As ItemGroup, Max(VMain.JobWorkerName) As JobWorkerName " &
                " FROM ( " & FRetFillJobInvoiceItemWiseQry(strCond, "") & " ) As VMain " &
                " GROUP BY VMain.JobInvoice, VMain.Code " &
                " Order By ItemDesc "
        FRH_Multiple = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(AgL.FillData(mQry, AgL.GCn).TABLES(0)), "", 500, 850, , , False)
        FRH_Multiple.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple.FFormatColumn(1, , 0, , False)
        FRH_Multiple.FFormatColumn(2, "Invoice No.", 150, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(3, "Invoice Date", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(4, "Item", 150, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(5, "Item Group", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(6, "Job Worker", 200, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.StartPosition = FormStartPosition.CenterScreen
        FRH_Multiple.ShowDialog()

        If FRH_Multiple.BytBtnValue = 0 Then
            StrRtn = FRH_Multiple.FFetchData(1, "'", "'", ",", True)
        End If
        FFillJobInvoiceItemSelection = StrRtn

        FRH_Multiple = Nothing
    End Function

    Private Sub FFillItemsForJobInvoice(ByVal SelectionValue As String)
        Dim I As Integer = 0
        Dim DtTemp As DataTable = Nothing
        Try
            If SelectionValue = "" Then Exit Sub

            If RbtForJobInvoice.Checked Then
                mQry = FRetFillJobInvoiceItemWiseQry(" And DocId In (" & SelectionValue & ") ", "")
            Else
                mQry = FRetFillJobInvoiceItemWiseQry("", " And L.JobInvoice + L.Item In (" & SelectionValue & ") ") & " Order By Description "
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

                        Dgl1.Item(Col1JobReceive, I).Tag = AgL.XNull(.Rows(I)("JobReceive"))
                        Dgl1.Item(Col1JobReceive, I).Value = AgL.XNull(.Rows(I)("JobReceiveNo"))
                        Dgl1.Item(Col1JobReceiveSr, I).Value = AgL.XNull(.Rows(I)("JobReceiveSr"))

                        Dgl1.Item(Col1JobInvoice, I).Tag = AgL.XNull(.Rows(I)("JobInvoice"))
                        Dgl1.Item(Col1JobInvoice, I).Value = AgL.XNull(.Rows(I)("JobInvoiceNo"))
                        Dgl1.Item(Col1JobInvoiceSr, I).Value = AgL.XNull(.Rows(I)("JobInvoiceSr"))

                        Dgl1.Item(Col1JobWorker, I).Tag = AgL.XNull(.Rows(I)("JobWorker"))
                        Dgl1.Item(Col1JobWorker, I).Value = AgL.XNull(.Rows(I)("JobWorkerName"))

                        Dgl1.Item(Col1Item_Uid, I).Tag = AgL.XNull(.Rows(I)("Item_Uid"))
                        Dgl1.Item(Col1Item_Uid, I).Value = AgL.XNull(.Rows(I)("Item_UidDesc"))

                        Dgl1.Item(Col1Item, I).Tag = AgL.XNull(.Rows(I)("Code"))
                        Dgl1.Item(Col1Item, I).Value = AgL.XNull(.Rows(I)("Description"))

                        Dgl1.Item(Col1BillQty, I).Value = Format(AgL.VNull(.Rows(I)("Qty")), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
                        Dgl1.Item(Col1Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                        Dgl1.Item(Col1MeasurePerPcs, I).Value = Format(AgL.VNull(.Rows(I)("MeasurePerPcs")), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                        Dgl1.Item(Col1MeasureUnit, I).Value = AgL.XNull(.Rows(I)("MeasureUnit"))

                        Dgl1.Item(Col1Rate_Inv, I).Value = Format(AgL.VNull(.Rows(I)("Rate")), "0.00")
                        Dgl1.Item(Col1Rate_Amd, I).Value = Format(AgL.VNull(.Rows(I)("Rate")), "0.00")
                    Next I
                End If
            End With
            AgCalcGrid1.Calculation(True)
            Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function FRetFillJobInvoiceItemWiseQry(ByVal HeaderConStr As String, ByVal LineConStr As String) As String
        FRetFillJobInvoiceItemWiseQry = " SELECT Max(L.Item_Uid) As Item_Uid, Max(L.Item) As Code, " &
                    " Max(I.Description) as Description, " &
                    " Max(Ig.Description) as ItemGroup, " &
                    " Max(I.ManualCode) As ManualCode,   " &
                    " Sum(L.BillQty) As Qty, Max(I.Unit) as Unit,   " &
                    " Max(I.MeasureUnit) MeasureUnit, Sum(L.Rate) as Rate,   " &
                    " Max(L.MeasurePerPcs) as MeasurePerPcs, " &
                    " L.JobInvoice, L.JobInvoiceSr, Max(H.V_Type) + '-' +  Max(H.ManualRefNo) AS JobInvoiceNo,   " &
                    " Max(L.JobReceive) As JobReceive, Max(L.JobReceiveSr) As JobReceiveSr, Max(H.V_Type) + '-' +  Max(Ir.ManualRefNo) As JobReceiveNo, " &
                    " Max(L.JobOrder) As JobOrder, Max(L.JobOrderSr) As JobOrderSr, Max(H.V_Type) + '-' +  Max(Jo.ManualRefNo) As JobOrderNo, " &
                    " Max(L.ProdOrder) As ProdOrder, Max(L.ProdOrderSr) As ProdOrderSr, Max(H.V_Type) + '-' +  Max(Po.ManualRefNo) As ProdOrderNo, " &
                    " Max(H.V_Date) as JobInvoiceDate,  " &
                    " Max(U.DecimalPlaces) as QtyDecimalPlaces,  " &
                    " Max(U1.DecimalPlaces) as MeasureDecimalPlaces, " &
                    " Max(Iu.Item_Uid) As Item_UidDesc, " &
                    " Max(H.JobWorker) As JobWorker, Max(Sg.Name) As JobWorkerName   " &
                    " FROM (  " &
                    "     SELECT DocID, V_Type, ManualRefNo, V_Date, JobWorker   " &
                    "     FROM JobInvoice  Where 1=1 " & HeaderConStr & " " &
                    "     ) H   " &
                    " LEFT JOIN JobInvoiceDetail L  ON H.DocID = L.JobInvoice " &
                    " LEFT JOIN JobIssRec Ir On L.JobReceive = Ir.DocId " &
                    " LEFT JOIN JobOrder Jo On L.JobOrder = Jo.DocId " &
                    " LEFT JOIN ProdOrder Po  ON L.ProdOrder = Po.DocId " &
                    " LEFT JOIN SubGroup Sg On H.JobWorker = Sg.SubCode " &
                    " Left Join Item I  On L.Item  = I.Code   " &
                    " LEFT JOIN ItemGroup Ig ON I.ItemGroup = Ig.Code " &
                    " LEFT JOIN Item_Uid Iu On L.Item_Uid = Iu.Code " &
                    " LEFT JOIN Voucher_Type Vt  ON H.V_Type = Vt.V_Type    " &
                    " LEFT JOIN Unit U  On L.Unit = U.Code   " &
                    " LEFT JOIN Unit U1  On L.MeasureUnit = U1.Code   " &
                    " WHERE 1 = 1 " & LineConStr &
                    " GROUP BY L.JobInvoice, L.JobInvoiceSr "
    End Function

    Private Sub Dgl1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellEnter
        Try
            If AgL.StrCmp(Topctrl1.Mode, "Browse") Then Exit Sub
            If Dgl1.CurrentCell Is Nothing Then Exit Sub
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1BillQty
                    CType(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex), AgControls.AgTextColumn).AgNumberRightPlaces = Val(Dgl1.Item(Col1QtyDecimalPlaces, Dgl1.CurrentCell.RowIndex).Value)

                Case Col1MeasurePerPcs, Col1TotalMeasure
                    CType(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex), AgControls.AgTextColumn).AgNumberRightPlaces = Val(Dgl1.Item(Col1MeasureDecimalPlaces, Dgl1.CurrentCell.RowIndex).Value)
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Validating_Item_Uid(ByVal Item_Uid As String, ByVal mRow As Integer)
        Dim DtTemp1 As DataTable = Nothing
        Dim ErrMsgStr$ = ""

        Try
            mQry = " Select Code From Item_Uid Where Item_Uid = '" & Item_Uid & "' "
            Dgl1.Item(Col1Item_Uid, mRow).Tag = AgL.XNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar)

            mQry = " Select H.DocId As JobOrder, H.V_Type + '-' + H.ManualRefNo As JobOrderNo, " &
                        " L.Sr As JobOrderSr, L.Rate, L.ProdOrder, Po.ManualRefNo As ProdOrderNo, " &
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

                Dgl1.Item(Col1BillQty, mRow).Value = 1
                Dgl1.Item(Col1Unit, mRow).Value = AgL.XNull(DtTemp1.Rows(0)("Unit"))

                Dgl1.Item(Col1MeasurePerPcs, mRow).Value = AgL.VNull(DtTemp1.Rows(0)("MeasurePerPcs"))
                Dgl1.Item(Col1MeasureUnit, mRow).Value = AgL.XNull(DtTemp1.Rows(0)("MeasureUnit"))
                Dgl1.Item(Col1MeasureDecimalPlaces, mRow).Value = AgL.VNull(DtTemp1.Rows(0)("MeasureDecimalPlaces"))

                Dgl1.Item(Col1ProdOrder, mRow).Tag = AgL.XNull(DtTemp1.Rows(0)("ProdOrder"))
                Dgl1.Item(Col1ProdOrder, mRow).Value = AgL.XNull(DtTemp1.Rows(0)("ProdOrderNo"))
                Dgl1.Item(Col1Rate, mRow).Value = AgL.VNull(DtTemp1.Rows(0)("Rate"))
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

    Private Sub ChkShowOnlyImportedRecords_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        FIniMaster(1)
        Topctrl1.SetDisp(True)
        MoveRec()
    End Sub

    Private Sub FrmFinishingOrder_BaseEvent_Topctrl_tbPrn(ByVal SearchCode As String) Handles Me.BaseEvent_Topctrl_tbPrn
        mQry = " SELECT H.V_Date, H.V_Type + '-' + H.ManualRefNo As ManualRefNo, H.Remarks, " &
                " H.EntryBy, H.EntryDate, H.ApproveBy, H.ApproveDate,  " &
                " L.DocQty As Qty, L.Unit, L.MeasurePerPcs,  " &
                " L.DocMeasure As TotalMeasure, L.MeasureUnit, L.Rate, L.Net_Amount,  " &
                " L.Rate_Inv, L.Rate_Amd, " &
                " L.Remark As LineRemark,  " &
                " L.Item_Uid, Sg.Name AS JobWorkerName, " &
                " Sg1.Name AS OrderByName, " &
                " I.Description AS ItemDesc, Iu.Item_Uid As Item_UidDesc, " &
                " Ig.Description As ItemGroupDesc " &
                " FROM JobInvoice H   " &
                " LEFT JOIN JobInvoiceDetail L  ON H.DocID = L.DocId " &
                " LEFT JOIN SubGroup Sg  ON IFNull(H.JobWorker,L.JobWorker) = Sg.SubCode " &
                " LEFT JOIN SubGroup Sg1  ON H.JobReceiveBy = Sg1.SubCode " &
                " LEFT JOIN Item I  ON L.Item = I.Code " &
                " LEFT JOIN Item_Uid Iu  ON L.Item_Uid = Iu.Code " &
                " LEFT JOIN ItemGroup Ig ON I.ItemGroup = Ig.Code " &
                " WHERE H.DocID =  '" & mSearchCode & "' Order By L.Sr "
        ClsMain.FPrintThisDocument(Me, TxtV_Type.Tag, mQry, "Prod_JobInvoiceAmendment_Print", "Job Invoice Amendment")
    End Sub

    Private Sub FrmJobInvoice_BaseEvent_Topctrl_tbEdit(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbEdit
        Passed = Not ClsMain.FLockOldEntryInNewEntryPoint(TxtProcess.Tag, TxtV_Date.Text)
        FAsignProcess()
        RbtForJobInvoice.Checked = True
    End Sub
End Class
