Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data.SQLite
Public Class FrmJobQC_New
    Inherits AgTemplate.TempTransaction
    Public mQry$

    Public Const ColSNo As String = "S.No."
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Public Const Col1Item As String = "Item"
    Public Const Col1Dimension1 As String = "Dimension1"
    Public Const Col1Dimension2 As String = "Dimension2"
    Public Const Col1StockItem As String = "Stock Item"
    Public Const Col1StockDimension1 As String = "Stock Dimension1"
    Public Const Col1StockDimension2 As String = "Stock Dimension2"
    Public Const Col1JobReceive As String = "Job Receive"
    Public Const Col1JobReceiveSr As String = "Job Receive Sr"
    Public Const Col1Unit As String = "Unit"
    Public Const Col1LotNo As String = "LotNo"
    Public Const Col1QcQty As String = "QC Qty"
    Public Const Col1CheckedQty As String = "Checked Qty"
    Public Const Col1PassedQty As String = "Passed Qty"
    Protected WithEvents TxtPartyName As AgControls.AgTextBox
    Protected WithEvents Label5 As System.Windows.Forms.Label
    Protected WithEvents TxtGodown As AgControls.AgTextBox
    Protected WithEvents LblGodown As System.Windows.Forms.Label
    Protected WithEvents BtnFillDeatil As System.Windows.Forms.Button
    Protected WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Protected WithEvents RbtnQCCanRepaired As System.Windows.Forms.RadioButton
    Protected WithEvents RbtnQCCanNotRepaired As System.Windows.Forms.RadioButton
    Protected WithEvents RbtnQCCanRepairtoanother As System.Windows.Forms.RadioButton
    Public Const Col1Remark As String = "Remark"
    Dim IsQCFailed As Boolean = False

    Enum EnumQCFailedType
        CanBeRepaired = 1
        CanNotRepaired = 2
        CanRepairToAnother = 3
    End Enum

    Public Sub New(ByVal StrUPVar As String, ByVal DTUP As DataTable, ByVal strNCat As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Topctrl1.FSetParent(Me, StrUPVar, DTUP)
        Topctrl1.SetDisp(True)

        EntryNCat = strNCat

        'This is For Extracting Voucher Type Settings <Arpit>
        mQry = "Select H.* from Voucher_Type_Settings H Left Join Voucher_Type Vt On H.V_Type = Vt.V_Type  Where Vt.NCat In ('" & EntryNCat & "') And H.Div_Code = '" & AgL.PubDivCode & "' And H.Site_Code ='" & AgL.PubSiteCode & "' "
        DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
    End Sub

#Region "Form Designer Code"
    Private Sub InitializeComponent()
        Me.Dgl1 = New AgControls.AgDataGrid
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.LblTotalCheckedQty = New System.Windows.Forms.Label
        Me.LblTotalCheckedQtyText = New System.Windows.Forms.Label
        Me.LblTotalPassedQty = New System.Windows.Forms.Label
        Me.LblTotalPassedQtyText = New System.Windows.Forms.Label
        Me.LblTotalQCQty = New System.Windows.Forms.Label
        Me.LblTotalQCQtyText = New System.Windows.Forms.Label
        Me.Pnl1 = New System.Windows.Forms.Panel
        Me.Label30 = New System.Windows.Forms.Label
        Me.TxtRemarks = New AgControls.AgTextBox
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel
        Me.LblManualRefNo = New System.Windows.Forms.Label
        Me.LblManualRefNoReq = New System.Windows.Forms.Label
        Me.TxtManualRefNo = New AgControls.AgTextBox
        Me.TxtJobWorker = New AgControls.AgTextBox
        Me.LblJobWorker = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.TxtQCBy = New AgControls.AgTextBox
        Me.LblQCBy = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.TxtProcess = New AgControls.AgTextBox
        Me.LblProcess = New System.Windows.Forms.Label
        Me.GrpDirectChallan = New System.Windows.Forms.GroupBox
        Me.RbtForJobReceive = New System.Windows.Forms.RadioButton
        Me.RbtForJobReceiveItems = New System.Windows.Forms.RadioButton
        Me.BtnFillJobReceive = New System.Windows.Forms.Button
        Me.TxtPartyName = New AgControls.AgTextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.TxtGodown = New AgControls.AgTextBox
        Me.LblGodown = New System.Windows.Forms.Label
        Me.BtnFillDeatil = New System.Windows.Forms.Button
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.RbtnQCCanRepairtoanother = New System.Windows.Forms.RadioButton
        Me.RbtnQCCanRepaired = New System.Windows.Forms.RadioButton
        Me.RbtnQCCanNotRepaired = New System.Windows.Forms.RadioButton
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
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.Location = New System.Drawing.Point(756, 505)
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
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(596, 505)
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
        Me.GBoxApprove.Location = New System.Drawing.Point(421, 505)
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
        Me.GBoxEntryType.Location = New System.Drawing.Point(145, 505)
        Me.GBoxEntryType.Size = New System.Drawing.Size(119, 40)
        '
        'TxtEntryType
        '
        Me.TxtEntryType.Location = New System.Drawing.Point(3, 19)
        Me.TxtEntryType.Tag = ""
        '
        'GrpUP
        '
        Me.GrpUP.Location = New System.Drawing.Point(11, 505)
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
        Me.GroupBox1.Location = New System.Drawing.Point(2, 496)
        Me.GroupBox1.Size = New System.Drawing.Size(1002, 4)
        '
        'GBoxDivision
        '
        Me.GBoxDivision.Location = New System.Drawing.Point(287, 505)
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
        Me.LblV_No.Location = New System.Drawing.Point(44, 155)
        Me.LblV_No.Size = New System.Drawing.Size(71, 16)
        Me.LblV_No.Tag = ""
        Me.LblV_No.Text = "JobQC No."
        Me.LblV_No.Visible = False
        '
        'TxtV_No
        '
        Me.TxtV_No.AgSelectedValue = ""
        Me.TxtV_No.BackColor = System.Drawing.Color.White
        Me.TxtV_No.Location = New System.Drawing.Point(162, 153)
        Me.TxtV_No.Size = New System.Drawing.Size(135, 18)
        Me.TxtV_No.TabIndex = 3
        Me.TxtV_No.Tag = ""
        Me.TxtV_No.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.TxtV_No.Visible = False
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(320, 37)
        Me.Label2.Tag = ""
        '
        'LblV_Date
        '
        Me.LblV_Date.BackColor = System.Drawing.Color.Transparent
        Me.LblV_Date.Location = New System.Drawing.Point(214, 32)
        Me.LblV_Date.Size = New System.Drawing.Size(58, 16)
        Me.LblV_Date.Tag = ""
        Me.LblV_Date.Text = "QC Date"
        '
        'LblV_TypeReq
        '
        Me.LblV_TypeReq.Location = New System.Drawing.Point(544, 18)
        Me.LblV_TypeReq.Tag = ""
        '
        'TxtV_Date
        '
        Me.TxtV_Date.AgSelectedValue = ""
        Me.TxtV_Date.BackColor = System.Drawing.Color.White
        Me.TxtV_Date.Location = New System.Drawing.Point(337, 30)
        Me.TxtV_Date.Size = New System.Drawing.Size(135, 18)
        Me.TxtV_Date.TabIndex = 2
        Me.TxtV_Date.Tag = ""
        '
        'LblV_Type
        '
        Me.LblV_Type.Location = New System.Drawing.Point(483, 12)
        Me.LblV_Type.Size = New System.Drawing.Size(58, 16)
        Me.LblV_Type.Tag = ""
        Me.LblV_Type.Text = "QC Type"
        '
        'TxtV_Type
        '
        Me.TxtV_Type.AgSelectedValue = ""
        Me.TxtV_Type.BackColor = System.Drawing.Color.White
        Me.TxtV_Type.Location = New System.Drawing.Point(560, 11)
        Me.TxtV_Type.Size = New System.Drawing.Size(209, 18)
        Me.TxtV_Type.TabIndex = 1
        Me.TxtV_Type.Tag = ""
        '
        'LblSite_CodeReq
        '
        Me.LblSite_CodeReq.Location = New System.Drawing.Point(320, 17)
        Me.LblSite_CodeReq.Tag = ""
        '
        'LblSite_Code
        '
        Me.LblSite_Code.BackColor = System.Drawing.Color.Transparent
        Me.LblSite_Code.Location = New System.Drawing.Point(214, 13)
        Me.LblSite_Code.Size = New System.Drawing.Size(87, 16)
        Me.LblSite_Code.Tag = ""
        Me.LblSite_Code.Text = "Branch Name"
        '
        'TxtSite_Code
        '
        Me.TxtSite_Code.AgSelectedValue = ""
        Me.TxtSite_Code.BackColor = System.Drawing.Color.White
        Me.TxtSite_Code.Location = New System.Drawing.Point(337, 11)
        Me.TxtSite_Code.Size = New System.Drawing.Size(135, 18)
        Me.TxtSite_Code.TabIndex = 0
        Me.TxtSite_Code.Tag = ""
        '
        'LblDocId
        '
        Me.LblDocId.Tag = ""
        '
        'LblPrefix
        '
        Me.LblPrefix.Location = New System.Drawing.Point(20, 35)
        Me.LblPrefix.Tag = ""
        Me.LblPrefix.Visible = False
        '
        'TabControl1
        '
        Me.TabControl1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl1.Location = New System.Drawing.Point(-4, 20)
        Me.TabControl1.Size = New System.Drawing.Size(990, 198)
        Me.TabControl1.TabIndex = 0
        '
        'TP1
        '
        Me.TP1.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.TP1.Controls.Add(Me.TxtGodown)
        Me.TP1.Controls.Add(Me.LblGodown)
        Me.TP1.Controls.Add(Me.TxtPartyName)
        Me.TP1.Controls.Add(Me.Label5)
        Me.TP1.Controls.Add(Me.Label1)
        Me.TP1.Controls.Add(Me.TxtProcess)
        Me.TP1.Controls.Add(Me.LblProcess)
        Me.TP1.Controls.Add(Me.Label6)
        Me.TP1.Controls.Add(Me.TxtQCBy)
        Me.TP1.Controls.Add(Me.LblQCBy)
        Me.TP1.Controls.Add(Me.TxtJobWorker)
        Me.TP1.Controls.Add(Me.LblJobWorker)
        Me.TP1.Controls.Add(Me.LblManualRefNo)
        Me.TP1.Controls.Add(Me.LblManualRefNoReq)
        Me.TP1.Controls.Add(Me.TxtManualRefNo)
        Me.TP1.Controls.Add(Me.TxtRemarks)
        Me.TP1.Controls.Add(Me.Label30)
        Me.TP1.Location = New System.Drawing.Point(4, 22)
        Me.TP1.Size = New System.Drawing.Size(982, 172)
        Me.TP1.Text = "Document Detail"
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
        Me.TP1.Controls.SetChildIndex(Me.Label30, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtRemarks, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblDocId, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtDocId, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtManualRefNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblManualRefNoReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblManualRefNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblJobWorker, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtJobWorker, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblQCBy, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtQCBy, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label6, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblProcess, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtProcess, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label1, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label5, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtPartyName, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblGodown, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtGodown, 0)
        '
        'Topctrl1
        '
        Me.Topctrl1.Size = New System.Drawing.Size(984, 41)
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
        Me.Panel1.Controls.Add(Me.LblTotalCheckedQty)
        Me.Panel1.Controls.Add(Me.LblTotalCheckedQtyText)
        Me.Panel1.Controls.Add(Me.LblTotalPassedQty)
        Me.Panel1.Controls.Add(Me.LblTotalPassedQtyText)
        Me.Panel1.Controls.Add(Me.LblTotalQCQty)
        Me.Panel1.Controls.Add(Me.LblTotalQCQtyText)
        Me.Panel1.Location = New System.Drawing.Point(3, 470)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(977, 21)
        Me.Panel1.TabIndex = 694
        '
        'LblTotalCheckedQty
        '
        Me.LblTotalCheckedQty.AutoSize = True
        Me.LblTotalCheckedQty.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalCheckedQty.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalCheckedQty.Location = New System.Drawing.Point(454, 3)
        Me.LblTotalCheckedQty.Name = "LblTotalCheckedQty"
        Me.LblTotalCheckedQty.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalCheckedQty.TabIndex = 672
        Me.LblTotalCheckedQty.Text = "."
        Me.LblTotalCheckedQty.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTotalCheckedQtyText
        '
        Me.LblTotalCheckedQtyText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalCheckedQtyText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalCheckedQtyText.Location = New System.Drawing.Point(274, 2)
        Me.LblTotalCheckedQtyText.Name = "LblTotalCheckedQtyText"
        Me.LblTotalCheckedQtyText.Size = New System.Drawing.Size(169, 17)
        Me.LblTotalCheckedQtyText.TabIndex = 671
        Me.LblTotalCheckedQtyText.Text = "Checked Qty :"
        Me.LblTotalCheckedQtyText.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LblTotalPassedQty
        '
        Me.LblTotalPassedQty.AutoSize = True
        Me.LblTotalPassedQty.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalPassedQty.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalPassedQty.Location = New System.Drawing.Point(805, 4)
        Me.LblTotalPassedQty.Name = "LblTotalPassedQty"
        Me.LblTotalPassedQty.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalPassedQty.TabIndex = 670
        Me.LblTotalPassedQty.Text = "."
        Me.LblTotalPassedQty.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTotalPassedQtyText
        '
        Me.LblTotalPassedQtyText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalPassedQtyText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalPassedQtyText.Location = New System.Drawing.Point(606, 3)
        Me.LblTotalPassedQtyText.Name = "LblTotalPassedQtyText"
        Me.LblTotalPassedQtyText.Size = New System.Drawing.Size(193, 16)
        Me.LblTotalPassedQtyText.TabIndex = 669
        Me.LblTotalPassedQtyText.Text = "Passed Qty :"
        Me.LblTotalPassedQtyText.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LblTotalQCQty
        '
        Me.LblTotalQCQty.AutoSize = True
        Me.LblTotalQCQty.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalQCQty.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalQCQty.Location = New System.Drawing.Point(115, 4)
        Me.LblTotalQCQty.Name = "LblTotalQCQty"
        Me.LblTotalQCQty.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalQCQty.TabIndex = 668
        Me.LblTotalQCQty.Text = "."
        Me.LblTotalQCQty.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTotalQCQtyText
        '
        Me.LblTotalQCQtyText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalQCQtyText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalQCQtyText.Location = New System.Drawing.Point(8, 3)
        Me.LblTotalQCQtyText.Name = "LblTotalQCQtyText"
        Me.LblTotalQCQtyText.Size = New System.Drawing.Size(98, 17)
        Me.LblTotalQCQtyText.TabIndex = 667
        Me.LblTotalQCQtyText.Text = "QC Qty :"
        Me.LblTotalQCQtyText.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Pnl1
        '
        Me.Pnl1.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Pnl1.Location = New System.Drawing.Point(3, 247)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(977, 222)
        Me.Pnl1.TabIndex = 1
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(214, 129)
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
        Me.TxtRemarks.Location = New System.Drawing.Point(337, 130)
        Me.TxtRemarks.MaxLength = 255
        Me.TxtRemarks.Name = "TxtRemarks"
        Me.TxtRemarks.Size = New System.Drawing.Size(432, 18)
        Me.TxtRemarks.TabIndex = 8
        '
        'LinkLabel1
        '
        Me.LinkLabel1.BackColor = System.Drawing.Color.SteelBlue
        Me.LinkLabel1.DisabledLinkColor = System.Drawing.Color.White
        Me.LinkLabel1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel1.LinkColor = System.Drawing.Color.White
        Me.LinkLabel1.Location = New System.Drawing.Point(3, 224)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(75, 20)
        Me.LinkLabel1.TabIndex = 731
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "QC Detail"
        Me.LinkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblManualRefNo
        '
        Me.LblManualRefNo.AutoSize = True
        Me.LblManualRefNo.BackColor = System.Drawing.Color.Transparent
        Me.LblManualRefNo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblManualRefNo.Location = New System.Drawing.Point(483, 31)
        Me.LblManualRefNo.Name = "LblManualRefNo"
        Me.LblManualRefNo.Size = New System.Drawing.Size(47, 16)
        Me.LblManualRefNo.TabIndex = 740
        Me.LblManualRefNo.Text = "QC No"
        '
        'LblManualRefNoReq
        '
        Me.LblManualRefNoReq.AutoSize = True
        Me.LblManualRefNoReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblManualRefNoReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblManualRefNoReq.Location = New System.Drawing.Point(544, 36)
        Me.LblManualRefNoReq.Name = "LblManualRefNoReq"
        Me.LblManualRefNoReq.Size = New System.Drawing.Size(10, 7)
        Me.LblManualRefNoReq.TabIndex = 741
        Me.LblManualRefNoReq.Text = "Ä"
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
        Me.TxtManualRefNo.Location = New System.Drawing.Point(560, 30)
        Me.TxtManualRefNo.MaxLength = 20
        Me.TxtManualRefNo.Name = "TxtManualRefNo"
        Me.TxtManualRefNo.Size = New System.Drawing.Size(209, 18)
        Me.TxtManualRefNo.TabIndex = 3
        '
        'TxtJobWorker
        '
        Me.TxtJobWorker.AgAllowUserToEnableMasterHelp = False
        Me.TxtJobWorker.AgLastValueTag = Nothing
        Me.TxtJobWorker.AgLastValueText = Nothing
        Me.TxtJobWorker.AgMandatory = False
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
        Me.TxtJobWorker.Location = New System.Drawing.Point(337, 70)
        Me.TxtJobWorker.MaxLength = 50
        Me.TxtJobWorker.Name = "TxtJobWorker"
        Me.TxtJobWorker.Size = New System.Drawing.Size(432, 18)
        Me.TxtJobWorker.TabIndex = 6
        '
        'LblJobWorker
        '
        Me.LblJobWorker.AutoSize = True
        Me.LblJobWorker.BackColor = System.Drawing.Color.Transparent
        Me.LblJobWorker.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblJobWorker.Location = New System.Drawing.Point(214, 70)
        Me.LblJobWorker.Name = "LblJobWorker"
        Me.LblJobWorker.Size = New System.Drawing.Size(74, 16)
        Me.LblJobWorker.TabIndex = 743
        Me.LblJobWorker.Text = "Job Worker"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label6.Location = New System.Drawing.Point(544, 57)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(10, 7)
        Me.Label6.TabIndex = 748
        Me.Label6.Text = "Ä"
        '
        'TxtQCBy
        '
        Me.TxtQCBy.AgAllowUserToEnableMasterHelp = False
        Me.TxtQCBy.AgLastValueTag = Nothing
        Me.TxtQCBy.AgLastValueText = Nothing
        Me.TxtQCBy.AgMandatory = True
        Me.TxtQCBy.AgMasterHelp = False
        Me.TxtQCBy.AgNumberLeftPlaces = 8
        Me.TxtQCBy.AgNumberNegetiveAllow = False
        Me.TxtQCBy.AgNumberRightPlaces = 2
        Me.TxtQCBy.AgPickFromLastValue = False
        Me.TxtQCBy.AgRowFilter = ""
        Me.TxtQCBy.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtQCBy.AgSelectedValue = Nothing
        Me.TxtQCBy.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtQCBy.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtQCBy.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtQCBy.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtQCBy.Location = New System.Drawing.Point(560, 50)
        Me.TxtQCBy.MaxLength = 50
        Me.TxtQCBy.Name = "TxtQCBy"
        Me.TxtQCBy.Size = New System.Drawing.Size(209, 18)
        Me.TxtQCBy.TabIndex = 5
        '
        'LblQCBy
        '
        Me.LblQCBy.AutoSize = True
        Me.LblQCBy.BackColor = System.Drawing.Color.Transparent
        Me.LblQCBy.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblQCBy.Location = New System.Drawing.Point(483, 52)
        Me.LblQCBy.Name = "LblQCBy"
        Me.LblQCBy.Size = New System.Drawing.Size(47, 16)
        Me.LblQCBy.TabIndex = 747
        Me.LblQCBy.Text = "QC By"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(320, 57)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(10, 7)
        Me.Label1.TabIndex = 751
        Me.Label1.Text = "Ä"
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
        Me.TxtProcess.Location = New System.Drawing.Point(337, 50)
        Me.TxtProcess.MaxLength = 50
        Me.TxtProcess.Name = "TxtProcess"
        Me.TxtProcess.Size = New System.Drawing.Size(135, 18)
        Me.TxtProcess.TabIndex = 4
        '
        'LblProcess
        '
        Me.LblProcess.AutoSize = True
        Me.LblProcess.BackColor = System.Drawing.Color.Transparent
        Me.LblProcess.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblProcess.Location = New System.Drawing.Point(214, 51)
        Me.LblProcess.Name = "LblProcess"
        Me.LblProcess.Size = New System.Drawing.Size(56, 16)
        Me.LblProcess.TabIndex = 750
        Me.LblProcess.Text = "Process"
        '
        'GrpDirectChallan
        '
        Me.GrpDirectChallan.BackColor = System.Drawing.Color.Transparent
        Me.GrpDirectChallan.Controls.Add(Me.RbtForJobReceive)
        Me.GrpDirectChallan.Controls.Add(Me.RbtForJobReceiveItems)
        Me.GrpDirectChallan.Location = New System.Drawing.Point(82, 218)
        Me.GrpDirectChallan.Name = "GrpDirectChallan"
        Me.GrpDirectChallan.Size = New System.Drawing.Size(307, 25)
        Me.GrpDirectChallan.TabIndex = 3010
        Me.GrpDirectChallan.TabStop = False
        '
        'RbtForJobReceive
        '
        Me.RbtForJobReceive.AutoSize = True
        Me.RbtForJobReceive.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RbtForJobReceive.Location = New System.Drawing.Point(6, 7)
        Me.RbtForJobReceive.Name = "RbtForJobReceive"
        Me.RbtForJobReceive.Size = New System.Drawing.Size(128, 17)
        Me.RbtForJobReceive.TabIndex = 0
        Me.RbtForJobReceive.TabStop = True
        Me.RbtForJobReceive.Text = "For Job Receive"
        Me.RbtForJobReceive.UseVisualStyleBackColor = True
        '
        'RbtForJobReceiveItems
        '
        Me.RbtForJobReceiveItems.AutoSize = True
        Me.RbtForJobReceiveItems.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RbtForJobReceiveItems.Location = New System.Drawing.Point(137, 7)
        Me.RbtForJobReceiveItems.Name = "RbtForJobReceiveItems"
        Me.RbtForJobReceiveItems.Size = New System.Drawing.Size(170, 17)
        Me.RbtForJobReceiveItems.TabIndex = 743
        Me.RbtForJobReceiveItems.TabStop = True
        Me.RbtForJobReceiveItems.Text = "For Job Receive Items"
        Me.RbtForJobReceiveItems.UseVisualStyleBackColor = True
        '
        'BtnFillJobReceive
        '
        Me.BtnFillJobReceive.BackColor = System.Drawing.Color.Transparent
        Me.BtnFillJobReceive.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnFillJobReceive.Font = New System.Drawing.Font("Lucida Console", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnFillJobReceive.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnFillJobReceive.Location = New System.Drawing.Point(396, 221)
        Me.BtnFillJobReceive.Margin = New System.Windows.Forms.Padding(0)
        Me.BtnFillJobReceive.Name = "BtnFillJobReceive"
        Me.BtnFillJobReceive.Size = New System.Drawing.Size(38, 23)
        Me.BtnFillJobReceive.TabIndex = 3009
        Me.BtnFillJobReceive.Text = "..."
        Me.BtnFillJobReceive.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnFillJobReceive.UseVisualStyleBackColor = False
        '
        'TxtPartyName
        '
        Me.TxtPartyName.AgAllowUserToEnableMasterHelp = False
        Me.TxtPartyName.AgLastValueTag = Nothing
        Me.TxtPartyName.AgLastValueText = Nothing
        Me.TxtPartyName.AgMandatory = False
        Me.TxtPartyName.AgMasterHelp = False
        Me.TxtPartyName.AgNumberLeftPlaces = 8
        Me.TxtPartyName.AgNumberNegetiveAllow = False
        Me.TxtPartyName.AgNumberRightPlaces = 2
        Me.TxtPartyName.AgPickFromLastValue = False
        Me.TxtPartyName.AgRowFilter = ""
        Me.TxtPartyName.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtPartyName.AgSelectedValue = Nothing
        Me.TxtPartyName.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtPartyName.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtPartyName.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtPartyName.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPartyName.Location = New System.Drawing.Point(337, 90)
        Me.TxtPartyName.MaxLength = 50
        Me.TxtPartyName.Name = "TxtPartyName"
        Me.TxtPartyName.Size = New System.Drawing.Size(432, 18)
        Me.TxtPartyName.TabIndex = 7
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(214, 90)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(77, 16)
        Me.Label5.TabIndex = 753
        Me.Label5.Text = "Party Name"
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
        Me.TxtGodown.Location = New System.Drawing.Point(337, 110)
        Me.TxtGodown.MaxLength = 255
        Me.TxtGodown.Name = "TxtGodown"
        Me.TxtGodown.Size = New System.Drawing.Size(432, 18)
        Me.TxtGodown.TabIndex = 758
        '
        'LblGodown
        '
        Me.LblGodown.AutoSize = True
        Me.LblGodown.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblGodown.Location = New System.Drawing.Point(214, 112)
        Me.LblGodown.Name = "LblGodown"
        Me.LblGodown.Size = New System.Drawing.Size(55, 16)
        Me.LblGodown.TabIndex = 759
        Me.LblGodown.Text = "Godown"
        '
        'BtnFillDeatil
        '
        Me.BtnFillDeatil.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnFillDeatil.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnFillDeatil.Location = New System.Drawing.Point(868, 221)
        Me.BtnFillDeatil.Name = "BtnFillDeatil"
        Me.BtnFillDeatil.Size = New System.Drawing.Size(112, 23)
        Me.BtnFillDeatil.TabIndex = 3011
        Me.BtnFillDeatil.Text = "Copy Std. Qty"
        Me.BtnFillDeatil.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox3.Controls.Add(Me.RbtnQCCanRepairtoanother)
        Me.GroupBox3.Controls.Add(Me.RbtnQCCanRepaired)
        Me.GroupBox3.Controls.Add(Me.RbtnQCCanNotRepaired)
        Me.GroupBox3.Location = New System.Drawing.Point(437, 218)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(425, 25)
        Me.GroupBox3.TabIndex = 3011
        Me.GroupBox3.TabStop = False
        '
        'RbtnQCCanRepairtoanother
        '
        Me.RbtnQCCanRepairtoanother.AutoSize = True
        Me.RbtnQCCanRepairtoanother.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RbtnQCCanRepairtoanother.Location = New System.Drawing.Point(250, 8)
        Me.RbtnQCCanRepairtoanother.Name = "RbtnQCCanRepairtoanother"
        Me.RbtnQCCanRepairtoanother.Size = New System.Drawing.Size(168, 17)
        Me.RbtnQCCanRepairtoanother.TabIndex = 744
        Me.RbtnQCCanRepairtoanother.TabStop = True
        Me.RbtnQCCanRepairtoanother.Text = "Can Repair to Another"
        Me.RbtnQCCanRepairtoanother.UseVisualStyleBackColor = True
        '
        'RbtnQCCanRepaired
        '
        Me.RbtnQCCanRepaired.AutoSize = True
        Me.RbtnQCCanRepaired.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RbtnQCCanRepaired.Location = New System.Drawing.Point(5, 8)
        Me.RbtnQCCanRepaired.Name = "RbtnQCCanRepaired"
        Me.RbtnQCCanRepaired.Size = New System.Drawing.Size(111, 17)
        Me.RbtnQCCanRepaired.TabIndex = 0
        Me.RbtnQCCanRepaired.TabStop = True
        Me.RbtnQCCanRepaired.Text = "Can Repaired"
        Me.RbtnQCCanRepaired.UseVisualStyleBackColor = True
        '
        'RbtnQCCanNotRepaired
        '
        Me.RbtnQCCanNotRepaired.AutoSize = True
        Me.RbtnQCCanNotRepaired.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RbtnQCCanNotRepaired.Location = New System.Drawing.Point(115, 7)
        Me.RbtnQCCanNotRepaired.Name = "RbtnQCCanNotRepaired"
        Me.RbtnQCCanNotRepaired.Size = New System.Drawing.Size(137, 17)
        Me.RbtnQCCanNotRepaired.TabIndex = 743
        Me.RbtnQCCanNotRepaired.TabStop = True
        Me.RbtnQCCanNotRepaired.Text = "Can Not Repaired"
        Me.RbtnQCCanNotRepaired.UseVisualStyleBackColor = True
        '
        'FrmJobQC_New
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.BackColor = System.Drawing.SystemColors.ButtonShadow
        Me.ClientSize = New System.Drawing.Size(984, 546)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.BtnFillDeatil)
        Me.Controls.Add(Me.GrpDirectChallan)
        Me.Controls.Add(Me.BtnFillJobReceive)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Pnl1)
        Me.Name = "FrmJobQC_New"
        Me.Text = "Template JobQC "
        Me.Controls.SetChildIndex(Me.Pnl1, 0)
        Me.Controls.SetChildIndex(Me.Panel1, 0)
        Me.Controls.SetChildIndex(Me.LinkLabel1, 0)
        Me.Controls.SetChildIndex(Me.TabControl1, 0)
        Me.Controls.SetChildIndex(Me.Topctrl1, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.GrpUP, 0)
        Me.Controls.SetChildIndex(Me.GBoxEntryType, 0)
        Me.Controls.SetChildIndex(Me.GBoxApprove, 0)
        Me.Controls.SetChildIndex(Me.GBoxMoveToLog, 0)
        Me.Controls.SetChildIndex(Me.GroupBox2, 0)
        Me.Controls.SetChildIndex(Me.GBoxDivision, 0)
        Me.Controls.SetChildIndex(Me.BtnFillJobReceive, 0)
        Me.Controls.SetChildIndex(Me.GrpDirectChallan, 0)
        Me.Controls.SetChildIndex(Me.BtnFillDeatil, 0)
        Me.Controls.SetChildIndex(Me.GroupBox3, 0)
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
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Protected WithEvents Panel1 As System.Windows.Forms.Panel
    Protected WithEvents Pnl1 As System.Windows.Forms.Panel
    Protected WithEvents TxtRemarks As AgControls.AgTextBox
    Protected WithEvents Label30 As System.Windows.Forms.Label
    Protected WithEvents LblTotalQCQty As System.Windows.Forms.Label
    Protected WithEvents LblTotalQCQtyText As System.Windows.Forms.Label
    Protected WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    Protected WithEvents LblManualRefNo As System.Windows.Forms.Label
    Protected WithEvents LblManualRefNoReq As System.Windows.Forms.Label
    Protected WithEvents TxtManualRefNo As AgControls.AgTextBox
    Protected WithEvents TxtJobWorker As AgControls.AgTextBox
    Protected WithEvents LblJobWorker As System.Windows.Forms.Label
    Protected WithEvents LblTotalPassedQty As System.Windows.Forms.Label
    Protected WithEvents LblTotalPassedQtyText As System.Windows.Forms.Label
    Protected WithEvents Label6 As System.Windows.Forms.Label
    Protected WithEvents TxtQCBy As AgControls.AgTextBox
    Protected WithEvents LblQCBy As System.Windows.Forms.Label
    Protected WithEvents LblTotalCheckedQty As System.Windows.Forms.Label
    Protected WithEvents LblTotalCheckedQtyText As System.Windows.Forms.Label
    Protected WithEvents Label1 As System.Windows.Forms.Label
    Protected WithEvents TxtProcess As AgControls.AgTextBox
    Protected WithEvents LblProcess As System.Windows.Forms.Label
    Protected WithEvents GrpDirectChallan As System.Windows.Forms.GroupBox
    Protected WithEvents RbtForJobReceive As System.Windows.Forms.RadioButton
    Protected WithEvents RbtForJobReceiveItems As System.Windows.Forms.RadioButton
    Protected WithEvents BtnFillJobReceive As System.Windows.Forms.Button
#End Region

    Private Sub FrmJobQC_BaseEvent_ApproveDeletion_InTrans(ByVal SearchCode As String, ByVal Conn As SqliteConnection, ByVal Cmd As SqliteCommand) Handles Me.BaseEvent_ApproveDeletion_InTrans
        mQry = " Delete From ProdOrderDetail Where DOcId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " Delete From ProdOrder WHere DOcID = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " Delete From StockAdj WHere StockOutDocid = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " Delete From Stock WHere Docid = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " Delete From Stock WHere ReferenceDocID = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

    End Sub

    Private Sub TempRequisition_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "JobQC"
        LogTableName = "JobQC_Log"
        MainLineTableCsv = "JobQCDetail"
        LogLineTableCsv = "JobQCDetail_Log"
        AgL.GridDesign(Dgl1)
    End Sub

    Private Sub FrmTempRequisition_FIniMast(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMast
        Dim mCondStr$
        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) & _
               " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "H.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        mQry = " Select H.DocID As SearchCode " &
            " From JobQC H " &
            " Left Join Voucher_Type Vt On H.V_Type = Vt.V_Type  " &
            " Where IFNull(H.IsDeleted,0) = 0  " & mCondStr & "  Order By H.V_Date Desc "

        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub FrmTempRequisition_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mCondStr$

        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) &
                        " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "H.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        AgL.PubFindQry = " SELECT H.DocID AS SearchCode, Vt.Description AS [QC_Type], H.V_Date AS QC_Date, " &
                            " H.ManualRefNo As [QC_No], " &
                            " Sg1.Name As QC_By, Sg2.Name As Job_Worker, H.Remarks, " &
                            " H.EntryBy AS [Entry_By], H.EntryDate AS [Entry_Date] " &
                            " FROM JobQC H  " &
                            " LEFT JOIN SubGroup Sg1 On H.QCBy = Sg1.SubCode " &
                            " LEFT JOIN SubGroup SG2 ON H.jobWorker = Sg2.SubCOde   " &
                            " LEFT JOIN Voucher_type Vt ON H.V_Type = Vt.V_Type " &
                            " Where IFNull(H.IsDeleted,0) = 0  " & mCondStr
        AgL.PubFindQryOrdBy = "[Date]"
    End Sub

    Private Sub FrmTempRequisition_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 35, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl1, Col1Item, 140, 0, Col1Item, True, False)
            .AddAgTextColumn(Dgl1, Col1Dimension1, 100, 0, AgTemplate.ClsMain.FGetDimension1Caption(), CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Dimension1")), Boolean), False)
            .AddAgTextColumn(Dgl1, Col1Dimension2, 100, 0, AgTemplate.ClsMain.FGetDimension2Caption(), CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Dimension2")), Boolean), False)
            .AddAgTextColumn(Dgl1, Col1StockItem, 140, 0, Col1StockItem, True, False)
            .AddAgTextColumn(Dgl1, Col1StockDimension1, 100, 0, "Stock " & AgTemplate.ClsMain.FGetDimension1Caption(), CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Dimension1")), Boolean), False)
            .AddAgTextColumn(Dgl1, Col1StockDimension2, 100, 0, "Stock " & AgTemplate.ClsMain.FGetDimension2Caption(), CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Dimension2")), Boolean), False)
            .AddAgTextColumn(Dgl1, Col1LotNo, 140, 0, Col1LotNo, False, False)
            .AddAgTextColumn(Dgl1, Col1JobReceive, 60, 0, Col1JobReceive, True, True)
            .AddAgTextColumn(Dgl1, Col1JobReceiveSr, 60, 0, Col1JobReceiveSr, False, True)
            .AddAgNumberColumn(Dgl1, Col1QcQty, 55, 5, 4, False, Col1QcQty, True, False)
            .AddAgNumberColumn(Dgl1, Col1CheckedQty, 60, 5, 4, False, Col1CheckedQty, True, False, True)
            .AddAgNumberColumn(Dgl1, Col1PassedQty, 55, 5, 4, False, Col1PassedQty, True, False, True)
            .AddAgTextColumn(Dgl1, Col1Unit, 50, 20, Col1Unit, True, True)
            .AddAgTextColumn(Dgl1, Col1Remark, 142, 255, Col1Remark, True, False)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 35
        Dgl1.AgSkipReadOnlyColumns = True

        AgCL.GridSetiingShowXml(Me.Text & TxtV_Type.Tag & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1, False)
    End Sub

    Private Sub FrmTempRequisition_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As SQLiteConnection, ByVal Cmd As SQLiteCommand) Handles Me.BaseEvent_Save_InTrans
        Dim I As Integer, mSr As Integer
        Dim dsTemp As DataSet = Nothing
        Dim bSelectionQry$ = ""

        'Dim IsStockItem As Boolean = False

        mQry = " Delete From StockAdj Where StockOutDocId  = '" & mSearchCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Delete From Stock Where ReferenceDocID = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " UPDATE JobQC SET " &
                " ManualRefNo = " & AgL.Chk_Text(TxtManualRefNo.Text) & ", " &
                " Process = " & AgL.Chk_Text(TxtProcess.Tag) & ", " &
                " QCBy = " & AgL.Chk_Text(TxtQCBy.Tag) & ", " &
                " JobWorker = " & AgL.Chk_Text(TxtJobWorker.Tag) & ", " &
                " Godown = " & AgL.Chk_Text(TxtGodown.Tag) & ", " &
                " Party = " & AgL.Chk_Text(TxtPartyName.Tag) & ", " &
                " JobQcFailedType = " & IIf(RbtnQCCanRepaired.Checked, EnumQCFailedType.CanBeRepaired, IIf(RbtnQCCanNotRepaired.Checked, EnumQCFailedType.CanNotRepaired, IIf(RbtnQCCanRepairtoanother.Checked, EnumQCFailedType.CanRepairToAnother, 0))) & ", " &
                " Remarks = " & AgL.Chk_Text(TxtRemarks.Text) & " " &
                " WHERE DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        'Never Try to Serialise Sr in Line Items 
        'As Some other Entry points may updating values to this Search code and Sr
        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1Item, I).Value <> "" Then
                If Dgl1.Item(ColSNo, I).Tag Is Nothing And Dgl1.Rows(I).Visible = True Then
                    mSr += 1
                    If bSelectionQry <> "" Then bSelectionQry += " UNION ALL "
                    bSelectionQry += " Select " & AgL.Chk_Text(mInternalCode) & "," & mSr & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1Dimension1, I).Tag) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1Dimension2, I).Tag) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1StockItem, I).Tag) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1StockDimension1, I).Tag) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1StockDimension2, I).Tag) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1JobReceive, I).Tag) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1JobReceiveSr, I).Value) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1Unit, I).Value) & ", " &
                            " " & Val(Dgl1.Item(Col1QcQty, I).Value) & ", " &
                            " " & Val(Dgl1.Item(Col1CheckedQty, I).Value) & ", " &
                            " " & Val(Dgl1.Item(Col1PassedQty, I).Value) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1Remark, I).Value) & " "
                Else
                    If Dgl1.Rows(I).Visible = True Then
                        If Dgl1.Rows(I).DefaultCellStyle.BackColor <> AgTemplate.ClsMain.Colours.GridRow_Locked Then
                            mQry = " UPDATE JobQCDetail " &
                                    " SET " &
                                    " Dimension1 = " & AgL.Chk_Text(Dgl1.Item(Col1Dimension1, I).Tag) & ", " &
                                    " Dimension2 = " & AgL.Chk_Text(Dgl1.Item(Col1Dimension2, I).Tag) & ", " &
                                    " Unit = " & AgL.Chk_Text(Dgl1.Item(Col1Unit, I).Value) & ", " &
                                    " StockItem = " & AgL.Chk_Text(Dgl1.Item(Col1StockItem, I).Tag) & ", " &
                                    " StockDimension1 = " & AgL.Chk_Text(Dgl1.Item(Col1StockDimension1, I).Tag) & ", " &
                                    " StockDimension2 = " & AgL.Chk_Text(Dgl1.Item(Col1StockDimension2, I).Tag) & ", " &
                                    " JobReceive = " & AgL.Chk_Text(Dgl1.Item(Col1JobReceive, I).Tag) & ", " &
                                    " JobReceiveSr = " & AgL.Chk_Text(Dgl1.Item(Col1JobReceiveSr, I).Value) & ", " &
                                    " QCQty = " & Val(Dgl1.Item(Col1QcQty, I).Value) & ", " &
                                    " CheckedQty = " & Val(Dgl1.Item(Col1CheckedQty, I).Value) & ", " &
                                    " PassedQty = " & Val(Dgl1.Item(Col1PassedQty, I).Value) & ", " &
                                    " Remarks = " & AgL.Chk_Text(Dgl1.Item(Col1Remark, I).Value) & " " &
                                    " Where DocId = '" & mSearchCode & "' " &
                                    " And Sr = " & Dgl1.Item(ColSNo, I).Tag & " "
                            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                        End If
                    Else
                        mQry = " Delete From JobQCDetail Where DocId = '" & mSearchCode & "' And Sr = " & Val(Dgl1.Item(ColSNo, I).Tag) & "  "
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                    End If



                End If

                'If Dgl1.Item(Col1StockItem, I).Value <> "" Then
                '    IsStockItem = True
                'End If
            End If
        Next


        If bSelectionQry <> "" Then
            mQry = " INSERT INTO JobQCDetail(DocId, Sr, Dimension1, Dimension2, StockItem,  StockDimension1, StockDimension2, JobReceive, JobReceiveSr, Unit, QcQty, " &
                    " CheckedQty, PassedQty, Remarks) " & bSelectionQry
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If

        If IsQCFailed = True Then
            Call FPostInProdOrder(Conn, Cmd)
        End If

        If AgL.VNull(AgL.PubDtEnviro.Rows(0)("IsMandatory_JobQCToStockPosting")) <> 0 Then
            Call FPostStock(Conn, Cmd)
        End If



        If AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Or AgL.StrCmp(AgL.PubUserName, "sa") Then
            AgCL.GridSetiingWriteXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1)
        End If
    End Sub

    Private Sub FrmTempRequisition_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim I As Integer
        Dim DrTemp As DataRow() = Nothing
        Dim DsTemp As DataSet

        LblTotalQCQty.Text = 0
        LblTotalCheckedQty.Text = 0
        LblTotalPassedQty.Text = 0

        Dim IsSameUnit As Boolean = True
        Dim IsSameMeasureUnit As Boolean = True
        Dim IsSameDeliveryMeasureUnit As Boolean = True

        mQry = "Select H.*, Sg1.DispName As JobWorkerName, Sg2.DispName As QcByName, Sg3.DispName As PartyName, P.Description As ProcessDesc " &
                " From JobQC H " &
                " LEFT JOIN SubGroup Sg1 ON H.JobWorker = Sg1.SubCode " &
                " LEFT JOIN SubGroup Sg2 On H.QCBy = Sg2.SubCode " &
                " LEFT JOIN SubGroup Sg3 On H.Party = Sg3.SubCode " &
                " LEFT JOIN Process P ON H.Process = P.NCat " &
                " Where H.DocID = '" & SearchCode & "'"
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then

                TxtManualRefNo.Text = AgL.XNull(.Rows(0)("ManualRefNo"))
                TxtProcess.Tag = AgL.XNull(.Rows(0)("Process"))
                TxtProcess.Text = AgL.XNull(.Rows(0)("ProcessDesc"))
                TxtQCBy.Tag = AgL.XNull(.Rows(0)("QCBy"))
                TxtQCBy.Text = AgL.XNull(.Rows(0)("QCByName"))
                TxtJobWorker.Tag = AgL.XNull(.Rows(0)("JobWorker"))
                TxtJobWorker.Text = AgL.XNull(.Rows(0)("JobWorkerName"))
                TxtPartyName.Tag = AgL.XNull(.Rows(0)("Party"))
                TxtPartyName.Text = AgL.XNull(.Rows(0)("PartyName"))
                TxtRemarks.Text = AgL.XNull(.Rows(0)("Remarks"))

                TxtGodown.Tag = AgL.XNull(.Rows(0)("Godown"))
                TxtGodown.Text = AgL.XNull(AgL.Dman_Execute(" SELECT Description FROM Godown WHERE Code =  '" & AgL.XNull(.Rows(0)("Godown")) & "' ", AgL.GCn).ExecuteScalar)

                If AgL.VNull(.Rows(I)("JobQcFailedType")) = EnumQCFailedType.CanBeRepaired Then
                    RbtnQCCanRepaired.Checked = True
                ElseIf AgL.VNull(.Rows(I)("JobQcFailedType")) = EnumQCFailedType.CanNotRepaired Then
                    RbtnQCCanNotRepaired.Checked = True
                ElseIf AgL.VNull(.Rows(I)("JobQcFailedType")) = EnumQCFailedType.CanRepairToAnother Then
                    RbtnQCCanRepairtoanother.Checked = True
                End If

                '-------------------------------------------------------------
                'Line Records are showing in First Grid
                '-------------------------------------------------------------
                mQry = "Select L.*, Jrd.Item, I.Description As ItemDesc, I.Unit, S.V_Type + '-' +  S.ManualRefNo As JobReceiveRefNo, " &
                        " U.DecimalPlaces As UnitDecimalPlaces,  " &
                        " SI.Description As StockItemDesc, SD1.Description As StockDimension1Desc, SD2.Description As StockDimension2Desc, " &
                        " D1.Description As " & AgTemplate.ClsMain.FGetDimension1Caption & ", " &
                        " D2.Description As " & AgTemplate.ClsMain.FGetDimension2Caption & " " &
                        " FROM JobQCDetail L  " &
                        " LEFT JOIN JobIssRec S  On L.JobReceive = S.DocId " &
                        " LEFT JOIN JobReceiveDetail Jrd  ON L.JobReceive = Jrd.DocId ANd L.JobReceiveSr = Jrd.Sr  " &
                        " LEFT JOIN Item I  ON Jrd.Item = I.Code " &
                        " LEFT JOIN Unit U  On I.Unit = U.Code " &
                        " Left Join Dimension1 D1   On L.Dimension1 = D1.Code " &
                        " Left Join Dimension2 D2   On L.Dimension2 = D2.Code " &
                        " LEFT JOIN Item SI  ON L.StockItem = SI.Code " &
                        " Left Join Dimension1 SD1   On L.StockDimension1 = SD1.Code " &
                        " Left Join Dimension2 SD2   On L.StockDimension2 = SD2.Code " &
                        " Where L.DocId = '" & SearchCode & "' " &
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
                            Dgl1.Item(Col1Item, I).Tag = AgL.XNull(.Rows(I)("Item"))
                            Dgl1.Item(Col1Item, I).Value = AgL.XNull(.Rows(I)("ItemDesc"))
                            Dgl1.Item(Col1Dimension1, I).Tag = AgL.XNull(.Rows(I)("Dimension1"))
                            Dgl1.Item(Col1Dimension1, I).Value = AgL.XNull(.Rows(I)(AgTemplate.ClsMain.FGetDimension1Caption))
                            Dgl1.Item(Col1Dimension2, I).Tag = AgL.XNull(.Rows(I)("Dimension2"))
                            Dgl1.Item(Col1Dimension2, I).Value = AgL.XNull(.Rows(I)(AgTemplate.ClsMain.FGetDimension2Caption))

                            Dgl1.Item(Col1StockItem, I).Tag = AgL.XNull(.Rows(I)("StockItem"))
                            Dgl1.Item(Col1StockItem, I).Value = AgL.XNull(.Rows(I)("StockItemDesc"))
                            Dgl1.Item(Col1StockDimension1, I).Tag = AgL.XNull(.Rows(I)("StockDimension1"))
                            Dgl1.Item(Col1StockDimension1, I).Value = AgL.XNull(.Rows(I)("StockDimension1Desc"))
                            Dgl1.Item(Col1StockDimension2, I).Tag = AgL.XNull(.Rows(I)("StockDimension2"))
                            Dgl1.Item(Col1StockDimension2, I).Value = AgL.XNull(.Rows(I)("StockDimension2Desc"))

                            Dgl1.Item(Col1JobReceive, I).Tag = AgL.XNull(.Rows(I)("JobReceive"))
                            Dgl1.Item(Col1JobReceive, I).Value = AgL.XNull(.Rows(I)("JobReceiveRefNo"))
                            Dgl1.Item(Col1JobReceiveSr, I).Value = AgL.VNull(.Rows(I)("JobReceiveSr"))
                            Dgl1.Item(Col1QcQty, I).Value = Format(AgL.VNull(.Rows(I)("QCQty")), "0.".PadRight(AgL.VNull(.Rows(I)("UnitDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1CheckedQty, I).Value = Format(AgL.VNull(.Rows(I)("CheckedQty")), "0.".PadRight(AgL.VNull(.Rows(I)("UnitDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1PassedQty, I).Value = Format(AgL.VNull(.Rows(I)("PassedQty")), "0.".PadRight(AgL.VNull(.Rows(I)("UnitDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                            Dgl1.Item(Col1Remark, I).Value = AgL.XNull(.Rows(I)("Remarks"))

                            LblTotalQCQty.Text = Val(LblTotalQCQty.Text) + Val(Dgl1.Item(Col1QcQty, I).Value)
                            LblTotalCheckedQty.Text = Val(LblTotalCheckedQty.Text) + Val(Dgl1.Item(Col1CheckedQty, I).Value)
                            LblTotalPassedQty.Text = Val(LblTotalPassedQty.Text) + Val(Dgl1.Item(Col1PassedQty, I).Value)

                            If Not AgL.StrCmp(Dgl1.Item(Col1Unit, I).Value, Dgl1.Item(Col1Unit, 0).Value) Then IsSameUnit = False
                        Next I
                    End If


                    If IsSameUnit Then
                        LblTotalQCQtyText.Text = "QC Qty (" & Dgl1.Item(Col1Unit, 0).Value & ") :"
                        LblTotalCheckedQtyText.Text = "Checked Qty (" & Dgl1.Item(Col1Unit, 0).Value & ") :"
                        LblTotalPassedQtyText.Text = "Passed Qty (" & Dgl1.Item(Col1Unit, 0).Value & ") :"
                    Else
                        LblTotalQCQtyText.Text = "QC Qty :"
                        LblTotalCheckedQtyText.Text = "Checked Qty :"
                        LblTotalPassedQtyText.Text = "Passed Qty :"
                    End If
                End With
                '-------------------------------------------------------------
            End If
        End With
    End Sub

    Private Sub FrmTempRequisition_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Topctrl1.ChangeAgGridState(Dgl1, False)
    End Sub

    Private Sub Dgl1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellEnter
        Try
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1PassedQty
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub DGL1_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles Dgl1.RowsAdded
        'sender(ColSNo, e.RowIndex).Value = e.RowIndex + 1
        sender(ColSNo, e.RowIndex).Value = e.RowIndex + 1
    End Sub

    Private Sub FrmTempRequisition_BaseFunction_Calculation() Handles Me.BaseFunction_Calculation
        Dim I As Integer
        LblTotalQCQty.Text = 0 : LblTotalCheckedQty.Text = 0 : LblTotalPassedQty.Text = 0

        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1Item, I).Value <> "" Then
                LblTotalQCQty.Text = Val(LblTotalQCQty.Text) + Val(Dgl1.Item(Col1QcQty, I).Value)
                LblTotalCheckedQty.Text = Val(LblTotalCheckedQty.Text) + Val(Dgl1.Item(Col1CheckedQty, I).Value)
                LblTotalPassedQty.Text = Val(LblTotalPassedQty.Text) + Val(Dgl1.Item(Col1PassedQty, I).Value)
            End If
        Next
    End Sub

    Private Sub FrmTempRequisition_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        Dim I As Integer = 0

        If AgL.RequiredField(TxtManualRefNo, LblManualRefNo.Text) Then passed = False : Exit Sub
        'If AgL.RequiredField(TxtJobWorker, LblJobWorker.Text) Then passed = False : Exit Sub
        If AgL.RequiredField(TxtQCBy, LblQCBy.Text) Then passed = False : Exit Sub

        If AgCL.AgIsBlankGrid(Dgl1, Dgl1.Columns(Col1Item).Index) = True Then passed = False : Exit Sub
        If AgCL.AgIsDuplicate(Dgl1, "" + Dgl1.Columns(Col1Item).Index.ToString + "," + Dgl1.Columns(Col1Dimension1).Index.ToString + "," + Dgl1.Columns(Col1JobReceive).Index.ToString + "," + Dgl1.Columns(Col1Dimension2).Index.ToString + "") Then passed = False : Exit Sub

        With Dgl1
            For I = 0 To .Rows.Count - 1
                If .Item(Col1JobReceive, I).Value <> "" Then
                    If Val(.Item(Col1QcQty, I).Value) = 0 Then
                        MsgBox("Qc Qty is 0 At Row No " & Dgl1.Item(ColSNo, I).Value & "", MsgBoxStyle.Information)
                        Dgl1.CurrentCell = Dgl1.Item(Col1QcQty, I)
                        passed = False : Exit Sub
                    End If

                    If Val(.Item(Col1PassedQty, I).Value) > Val(.Item(Col1QcQty, I).Value) Then
                        MsgBox("Passed Qty Is Greater Than Qc Qty At Row No " & Dgl1.Item(ColSNo, I).Value & "", MsgBoxStyle.Information)
                        Dgl1.CurrentCell = Dgl1.Item(Col1PassedQty, I)
                        passed = False : Exit Sub
                    End If

                    If Val(.Item(Col1PassedQty, I).Value) <> Val(.Item(Col1QcQty, I).Value) Then
                        IsQCFailed = True
                    End If
                End If
            Next
        End With

        If IsQCFailed = True And RbtnQCCanRepaired.Checked = False And RbtnQCCanNotRepaired.Checked = False And RbtnQCCanRepairtoanother.Checked = False Then
            MsgBox("Please Select QC Failed Type !", MsgBoxStyle.Information)
            RbtnQCCanRepaired.Focus()
            passed = False : Exit Sub
        End If
    End Sub

    Private Sub FrmTempRequisition_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
        Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
        LblTotalQCQty.Text = 0 : LblTotalPassedQty.Text = 0
        IsQCFailed = False
    End Sub

    Private Sub Txt_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtV_Type.Validating
        Dim DrTemp As DataRow() = Nothing
        Try
            Select Case sender.name
                Case TxtV_Type.Name
                    If Topctrl1.Mode = "Add" Then
                        TxtManualRefNo.Text = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "JobQC", TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, AgTemplate.ClsMain.ManualRefType.Max)
                    End If
                    IniGrid()
                    FAsignProcess()

                Case TxtPartyName.Name
                    If Dgl1.AgHelpDataSet(Col1Item) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1Item).Dispose() : Dgl1.AgHelpDataSet(Col1Item) = Nothing
                    If Dgl1.AgHelpDataSet(Col1Dimension1) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1Dimension1).Dispose() : Dgl1.AgHelpDataSet(Col1Dimension1) = Nothing
                    If Dgl1.AgHelpDataSet(Col1Dimension2) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1Dimension2).Dispose() : Dgl1.AgHelpDataSet(Col1Dimension2) = Nothing

            End Select
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
                    Validating_ItemCode(mColumnIndex, mRowIndex)

                Case Col1Dimension1
                    Validating_ItemCode(mColumnIndex, mRowIndex)

                Case Col1Dimension2
                    Validating_ItemCode(mColumnIndex, mRowIndex)
            End Select
            Call Calculation()
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
                Dgl1.Item(Col1Item, mRow).Tag = ""
                Dgl1.Item(Col1Item, mRow).Value = ""
                Dgl1.Item(Col1Dimension1, mRow).Tag = ""
                Dgl1.Item(Col1Dimension1, mRow).Value = ""
                Dgl1.Item(Col1Dimension2, mRow).Tag = ""
                Dgl1.Item(Col1Dimension2, mRow).Value = ""
                Dgl1.Item(Col1JobReceive, mRow).Tag = ""
                Dgl1.Item(Col1JobReceive, mRow).Value = ""
                Dgl1.Item(Col1JobReceiveSr, mRow).Value = ""
                Dgl1.Item(Col1Unit, mRow).Value = ""
                Dgl1.Item(Col1QcQty, mRow).Value = ""
                Dgl1.Item(Col1PassedQty, mRow).Value = ""
            Else
                If Dgl1.AgDataRow IsNot Nothing Then
                    Dgl1.Item(Col1Item, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("Code").Value)
                    Dgl1.Item(Col1Item, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("ItemDesc").Value)
                    Dgl1.Item(Col1Dimension1, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("Dimension1").Value)
                    Dgl1.Item(Col1Dimension1, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("" & AgTemplate.ClsMain.FGetDimension1Caption() & "").Value)
                    Dgl1.Item(Col1Dimension2, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("Dimension2").Value)
                    Dgl1.Item(Col1Dimension2, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("" & AgTemplate.ClsMain.FGetDimension2Caption() & "").Value)
                    Dgl1.Item(Col1JobReceive, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("JobReceive").Value)
                    Dgl1.Item(Col1JobReceive, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("JobReceiveNo").Value)
                    Dgl1.Item(Col1JobReceiveSr, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("JobReceiveSr").Value)
                    Dgl1.Item(Col1Unit, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("Unit").Value)
                    Dgl1.Item(Col1QcQty, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("Bal.Qty").Value)
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " On Validating_Item Function ")
        End Try
    End Sub

    Private Sub TempBookIssue_BaseEvent_Topctrl_tbAdd() Handles Me.BaseEvent_Topctrl_tbAdd
        TxtManualRefNo.Text = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "JobQC", TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, AgTemplate.ClsMain.ManualRefType.Max)
        FAsignProcess()
        TxtQCBy.Focus()
    End Sub

    Private Sub TxtBuyer_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtJobWorker.KeyDown, TxtQCBy.KeyDown, TxtPartyName.KeyDown, TxtGodown.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then Exit Sub

            Select Case sender.Name
                Case TxtJobWorker.Name
                    If TxtJobWorker.AgHelpDataSet Is Nothing Then
                        mQry = " SELECT Sg.SubCode AS Code, Sg.Name AS JobWorker, H.Process, " &
                                 " IFNull(Sg.IsDeleted,0) AS IsDeleted,  SG.Div_Code, " &
                                 " IFNull(Sg.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') As Status " &
                                 " FROM SubGroup Sg  " &
                                 " LEFT JOIN JobWorkerProcess H   On Sg.SubCode = H.SubCode  " &
                                 " Where IFNull(Sg.IsDeleted,0) = 0 " &
                                 " And Sg.Status = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " &
                                 " And CharIndex('|' + '" & TxtDivision.Tag & "' + '|', IFNull(Sg.DivisionList,'|' + '" & TxtDivision.Tag & "' + '|')) > 0 " &
                                 " And H.Process = '" & TxtProcess.Tag & "' "
                        sender.AgHelpDataSet(4, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
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

                Case TxtPartyName.Name
                    If TxtPartyName.AgHelpDataSet Is Nothing Then
                        mQry = "SELECT Sg.SubCode As Code, Sg.Name + ',' + IFNull(C.CityName,'') As Party, Sg.SalesTaxPostingGroup, " &
                                " Sg.SalesTaxPostingGroup, Sg.Currency, " &
                                " Sg.Div_Code " &
                                " FROM SubGroup Sg " &
                                " LEFT JOIN City C ON Sg.CityCode = C.CityCode  " &
                                " Where IFNull(Sg.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' "
                        sender.AgHelpDataSet(4, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
                    End If

                Case TxtQCBy.Name
                    If TxtQCBy.AgHelpDataSet Is Nothing Then
                        mQry = " SELECT Sg.SubCode AS Code, Sg.DispName FROM SubGroup Sg " &
                                " Where IFNull(Sg.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "'"
                        TxtQCBy.AgHelpDataSet(0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
                    End If

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
            If Dgl1.CurrentCell Is Nothing Then Exit Sub
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

                Case Col1StockDimension1
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1StockDimension1) Is Nothing Then
                            mQry = " SELECT Code, Description  FROM Dimension1  "
                            Dgl1.AgHelpDataSet(Col1StockDimension1) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If

                Case Col1StockDimension2
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1StockDimension2) Is Nothing Then
                            mQry = " SELECT Code, Description  FROM Dimension2  "
                            Dgl1.AgHelpDataSet(Col1StockDimension2) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If

                Case Col1StockItem
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1StockItem) Is Nothing Then
                            mQry = " SELECT I.Code, I.Description, I.Unit, IG.Description AS ItemGroupDesc, I.ItemType, I.SalesTaxPostingGroup , " &
                                      " I.Measure,  I.MeasureUnit, " &
                                      " I.ItemGroup, I.ItemCategory, Ig.Description As ItemGroupDesc, Ic.Description As ItemCategoryDesc, " &
                                      " U.DecimalPlaces as QtyDecimalPlaces, U1.DecimalPlaces as MeasureDecimalPlaces " &
                                      " FROM Item I  " &
                                      " LEFT JOIN ItemGroup Ig  On I.ItemGroup = Ig.Code " &
                                      " LEFT JOIN ItemCategory Ic  On I.ItemCategory = Ic.Code " &
                                      " LEFT JOIN Unit U On I.Unit = U.Code " &
                                      " LEFT JOIN Unit U1 On I.MeasureUnit = U1.Code " &
                                      " Where IFNull(I.IsDeleted,0) = 0 " &
                                      " And IFNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') <= '" & AgTemplate.ClsMain.EntryStatus.Active & "' "
                            Dgl1.AgHelpDataSet(Col1StockItem, 5) = AgL.FillData(mQry, AgL.GCn)
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

        Dim StrCond1 As String = ""
        If TxtJobWorker.Tag <> "" Then
            StrCond1 = " AND H.JobWorker ='" & TxtJobWorker.Tag & "'"
        End If

        If TxtPartyName.Tag <> "" Then
            StrCond1 = " AND PD.Party ='" & TxtPartyName.Tag & "'"
        End If


        mQry = " SELECT Max(L.Item) As Code, Max(I.Description) As ItemDesc,   " &
                " Max(H.V_Type) + '-' +  Max(H.ManualRefNo) As JobReceiveNo,   " &
                " Max(H.V_Date) as JobReceiveDate,  " &
                " IFNull(Sum(L.Qty),0) - IFNull(Max(Cd.Qty), 0) as [Bal.Qty],   " &
                " Max(I.Unit) as Unit, " &
                " Max(D1.Description) As " & AgTemplate.ClsMain.FGetDimension1Caption & ", " &
                " Max(D2.Description) As " & AgTemplate.ClsMain.FGetDimension2Caption & ", " &
                " Max(U.DecimalPlaces) as QtyDecimalPlaces,  " &
                " Max(L.Dimension1) As Dimension1, " &
                " Max(L.Dimension2) As Dimension2, " &
                " L.JobReceive, L.JobReceiveSr   " &
                " FROM (  " &
                "     SELECT H.DocID, H.V_Type, H.ManualRefNo, H.V_Date " &
                "     FROM JobIssRec H    " &
                "     LEFT JOIN ( SELECT L.DocId, max(MP.Party) AS Party " &
                "               FROM JobReceiveDetail  L  " &
                "               LEFT JOIN JobOrderDetail J  ON J.DocId = L.JobOrder AND J.Sr = L.JobOrderSr " &
                "               LEFT JOIN ProdOrderDetail P  ON P.DocId = J.ProdOrder AND  P.Sr  = J.ProdOrderSr " &
                "               LEFT JOIN MaterialPlan MP  ON MP.DocID = P.MaterialPlan " &
                "               GROUP BY L.DocId  ) PD ON PD.DocId = H.DocId  " &
                "     WHERE H.Div_Code = '" & TxtDivision.Tag & "'   " &
                "     AND H.Site_Code = '" & TxtSite_Code.Tag & "'   " &
                "     AND H.V_Date <= '" & TxtV_Date.Text & "' " &
                "     AND IFNull(H.IsMandatory_JobQC,0) <> 0   " &
                "    " & StrCond1 & " " &
                "     ) H   " &
                " LEFT JOIN JobReceiveDetail L  ON H.DocID = L.JobReceive  " &
                " Left Join Item I  On L.Item  = I.Code   " &
                " LEFT JOIN Voucher_Type Vt  ON H.V_Type = Vt.V_Type    " &
                " Left Join (   " &
                "     SELECT L.JobReceive, L.JobReceiveSr, Sum(L.PassedQty) AS Qty " &
                " 	  FROM JobQCDetail L     " &
                "     Where L.DocId <> '" & mSearchCode & "'  " &
                " 	  GROUP BY L.JobReceive, L.JobReceiveSr " &
                " 	) AS CD ON L.JobReceive = CD.JobReceive AND L.JobReceiveSr = CD.JobReceiveSr   " &
                " LEFT JOIN Unit U On L.Unit = U.Code   " &
                " Left Join Dimension1 D1   On L.Dimension1 = D1.Code " &
                " Left Join Dimension2 D2   On L.Dimension2 = D2.Code " &
                " WHERE 1=1 " & strCond &
                " GROUP BY L.JobReceive, L.JobReceiveSr  " &
                " Having IFNull(Sum(L.Qty),0) - IFNull(Max(Cd.Qty), 0) > 0 " &
                " Order By JobReceiveDate  "
        Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex, 5) = AgL.FillData(mQry, AgL.GcnRead)



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

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemDivision")) <> "" Then
                strCond += " And CharIndex('|' + I.Div_Code + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemDivision")) & "') > 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemSite")) <> "" Then
                strCond += " And CharIndex('|' + I.Site_Code + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemSite")) & "') > 0 "
            End If
        End If

        Dim StrCond1 As String = ""
        If TxtJobWorker.Tag <> "" Then
            StrCond1 = " AND JobWorker ='" & TxtJobWorker.Tag & "'"
        End If

        If TxtPartyName.Tag <> "" Then
            StrCond1 = " AND PD.Party ='" & TxtPartyName.Tag & "'"
        End If

        mQry = " SELECT Max(L.Dimension1) As Dimension1, " &
                " Max(D1.Description) As " & AgTemplate.ClsMain.FGetDimension1Caption & ", " &
                " Max(I.Description) As ItemDesc,   " &
                " Max(H.V_Type) + '-' +  Max(H.ManualRefNo) As JobReceiveNo,   " &
                " Max(H.V_Date) as JobReceiveDate,  " &
                " IFNull(Sum(L.Qty),0) - IFNull(Max(Cd.Qty), 0) as [Bal.Qty],   " &
                " Max(I.Unit) as Unit, " &
                " Max(D2.Description) As " & AgTemplate.ClsMain.FGetDimension2Caption & ", " &
                " Max(U.DecimalPlaces) as QtyDecimalPlaces,  " &
                " Max(L.Dimension2) As Dimension2, Max(L.Item) As Code, " &
                " L.JobReceive, L.JobReceiveSr   " &
                " FROM (  " &
                "     SELECT H.DocID, H.V_Type, H.ManualRefNo, H.V_Date " &
                "     FROM JobIssRec H    " &
                "     LEFT JOIN ( SELECT L.DocId, max(MP.Party) AS Party " &
                "               FROM JobReceiveDetail  L  " &
                "               LEFT JOIN JobOrderDetail J  ON J.DocId = L.JobOrder AND J.Sr = L.JobOrderSr " &
                "               LEFT JOIN ProdOrderDetail P  ON P.DocId = J.ProdOrder AND  P.Sr  = J.ProdOrderSr " &
                "               LEFT JOIN MaterialPlan MP  ON MP.DocID = P.MaterialPlan " &
                "               GROUP BY L.DocId  ) PD ON PD.DocId = H.DocId  " &
                "     WHERE H.Div_Code = '" & TxtDivision.Tag & "'   " &
                "     AND H.Site_Code = '" & TxtSite_Code.Tag & "'   " &
                "     AND H.V_Date <= '" & TxtV_Date.Text & "' " &
                "     AND IFNull(H.IsMandatory_JobQC,0) <> 0 " &
                "    " & StrCond1 & " " &
                "     ) H   " &
                " LEFT JOIN JobReceiveDetail L  ON H.DocID = L.JobReceive  " &
                " Left Join Item I  On L.Item  = I.Code   " &
                " LEFT JOIN Voucher_Type Vt  ON H.V_Type = Vt.V_Type    " &
                " Left Join (   " &
                "     SELECT L.JobReceive, L.JobReceiveSr, Sum(L.PassedQty) AS Qty " &
                " 	  FROM JobQCDetail L     " &
                "     Where L.DocId <> '" & mSearchCode & "'  " &
                " 	  GROUP BY L.JobReceive, L.JobReceiveSr " &
                " 	) AS CD ON L.JobReceive = CD.JobReceive AND L.JobReceiveSr = CD.JobReceiveSr   " &
                " LEFT JOIN Unit U On L.Unit = U.Code   " &
                " Left Join Dimension1 D1   On L.Dimension1 = D1.Code " &
                " Left Join Dimension2 D2   On L.Dimension2 = D2.Code " &
                " WHERE 1=1 " & strCond &
                " GROUP BY L.JobReceive, L.JobReceiveSr  " &
                " Having IFNull(Sum(L.Qty),0) - IFNull(Max(Cd.Qty), 0) > 0 " &
                " Order By JobReceiveDate  "
        Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex, 5) = AgL.FillData(mQry, AgL.GcnRead)
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

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemDivision")) <> "" Then
                strCond += " And CharIndex('|' + I.Div_Code + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemDivision")) & "') > 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemSite")) <> "" Then
                strCond += " And CharIndex('|' + I.Site_Code + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemSite")) & "') > 0 "
            End If
        End If

        Dim StrCond1 As String = ""
        If TxtJobWorker.Tag <> "" Then
            StrCond1 = " AND JobWorker ='" & TxtJobWorker.Tag & "'"
        End If

        If TxtPartyName.Tag <> "" Then
            StrCond1 = " AND PD.Party ='" & TxtPartyName.Tag & "'"
        End If

        mQry = " SELECT Max(L.Dimension2) As Dimension2, " &
                " Max(D2.Description) As " & AgTemplate.ClsMain.FGetDimension2Caption & ", " &
                " Max(I.Description) As ItemDesc,   " &
                " Max(H.V_Type) + '-' +  Max(H.ManualRefNo) As JobReceiveNo,   " &
                " Max(H.V_Date) as JobReceiveDate,  " &
                " IFNull(Sum(L.Qty),0) - IFNull(Max(Cd.Qty), 0) as [Bal.Qty],   " &
                " Max(I.Unit) as Unit, " &
                " Max(D1.Description) As " & AgTemplate.ClsMain.FGetDimension1Caption & ", " &
                " Max(U.DecimalPlaces) as QtyDecimalPlaces,  " &
                " Max(L.Dimension1) As Dimension1, Max(L.Item) As Code, " &
                " L.JobReceive, L.JobReceiveSr   " &
                " FROM (  " &
                "     SELECT H.DocID, H.V_Type, H.ManualRefNo, H.V_Date " &
                "     FROM JobIssRec H    " &
                "     LEFT JOIN ( SELECT L.DocId, max(MP.Party) AS Party " &
                "               FROM JobReceiveDetail  L  " &
                "               LEFT JOIN JobOrderDetail J  ON J.DocId = L.JobOrder AND J.Sr = L.JobOrderSr " &
                "               LEFT JOIN ProdOrderDetail P  ON P.DocId = J.ProdOrder AND  P.Sr  = J.ProdOrderSr " &
                "               LEFT JOIN MaterialPlan MP  ON MP.DocID = P.MaterialPlan " &
                "               GROUP BY L.DocId  ) PD ON PD.DocId = H.DocId  " &
                "     WHERE H.Div_Code = '" & TxtDivision.Tag & "'   " &
                "     AND H.Site_Code = '" & TxtSite_Code.Tag & "'   " &
                "     AND H.V_Date <= '" & TxtV_Date.Text & "' " &
                "    " & StrCond1 & " " &
                "     ) H   " &
                " LEFT JOIN JobReceiveDetail L  ON H.DocID = L.JobReceive  " &
                " Left Join Item I  On L.Item  = I.Code   " &
                " LEFT JOIN Voucher_Type Vt  ON H.V_Type = Vt.V_Type    " &
                " Left Join (   " &
                "     SELECT L.JobReceive, L.JobReceiveSr, Sum(L.PassedQty) AS Qty " &
                " 	  FROM JobQCDetail L     " &
                "     Where L.DocId <> '" & mSearchCode & "'  " &
                " 	  GROUP BY L.JobReceive, L.JobReceiveSr " &
                " 	) AS CD ON L.JobReceive = CD.JobReceive AND L.JobReceiveSr = CD.JobReceiveSr   " &
                " LEFT JOIN Unit U On L.Unit = U.Code   " &
                " Left Join Dimension1 D1   On L.Dimension1 = D1.Code " &
                " Left Join Dimension2 D2   On L.Dimension2 = D2.Code " &
                " WHERE 1=1 " & strCond &
                " GROUP BY L.JobReceive, L.JobReceiveSr  " &
                " Having IFNull(Sum(L.Qty),0) - IFNull(Max(Cd.Qty), 0) > 0 " &
                " Order By JobReceiveDate  "
        Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex, 5) = AgL.FillData(mQry, AgL.GcnRead)
    End Sub

    Private Sub FrmCarpetMaterialPlan_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AgL.WinSetting(Me, 578, 990, 0, 0)
    End Sub

    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub

    Private Sub FrmSaleInvoice_BaseEvent_Topctrl_tbRef() Handles Me.BaseEvent_Topctrl_tbRef
        If Dgl1.AgHelpDataSet(Col1Item) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1Item).Dispose() : Dgl1.AgHelpDataSet(Col1Item) = Nothing
        If TxtQCBy.AgHelpDataSet IsNot Nothing Then TxtQCBy.AgHelpDataSet.Dispose() : TxtQCBy.AgHelpDataSet = Nothing
        If TxtJobWorker.AgHelpDataSet IsNot Nothing Then TxtJobWorker.AgHelpDataSet.Dispose() : TxtJobWorker.AgHelpDataSet = Nothing
    End Sub

    Private Sub DGL1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.KeyDown
        If AgL.StrCmp(Topctrl1.Mode, "Browse") Then Exit Sub
        If Dgl1.CurrentCell Is Nothing Then Dgl1.CurrentCell = Dgl1.Item(Col1Item, 0)

        If e.Control And e.KeyCode = Keys.D And Dgl1.Rows(Dgl1.CurrentCell.RowIndex).DefaultCellStyle.BackColor <> AgTemplate.ClsMain.Colours.GridRow_Locked Then
            sender.CurrentRow.Visible = False
        End If
        If e.Control Or e.Shift Or e.Alt Then Exit Sub


        If e.KeyCode = Keys.Enter Then
            If Dgl1.CurrentCell.ColumnIndex = 1 Then
                If Dgl1.Item(Dgl1.CurrentCell.ColumnIndex, Dgl1.CurrentCell.RowIndex).Value Is Nothing Then Dgl1.Item(Dgl1.CurrentCell.ColumnIndex, Dgl1.CurrentCell.RowIndex).Value = ""
                If Dgl1.Item(Dgl1.CurrentCell.ColumnIndex, Dgl1.CurrentCell.RowIndex).Value = "" Then
                    If MsgBox("Do you want to save?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton1, "Save") = MsgBoxResult.Yes Then
                        Topctrl1.FButtonClick(13)
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub FrmGoodsReceipt_BaseEvent_Topctrl_tbPrn(ByVal SearchCode As String) Handles Me.BaseEvent_Topctrl_tbPrn
        mQry = " Select H.DocID, H.V_Type, H.V_Prefix, H.V_Date, H.ManualRefNo,H.Remarks,  H.EntryBy, H.EntryDate, " &
                " L.Sr, L.JobReceive, L.QcQty, L.CheckedQty, L.PassedQty, L.Remarks AS LineRemark,  " &
                " I.Description As ItemDesc, I.Unit, S.V_Type + '-' +  S.ManualRefNo As JobReceiveRefNo,  " &
                " U.DecimalPlaces As UnitDecimalPlaces, " &
                " D1.Description AS D1Desc, D2.Description AS D2Desc, E.Caption_Dimension1, E.Caption_Dimension2, " &
                " Sg1.DispName As JobWorkerName, Sg2.DispName As QcByName, P.Description As ProcessDesc  " &
                " From JobQC H  " &
                " LEFT JOIN SubGroup Sg1 ON H.JobWorker = Sg1.SubCode  " &
                " LEFT JOIN SubGroup Sg2 On H.QCBy = Sg2.SubCode  " &
                " LEFT JOIN Process P ON H.Process = P.NCat  " &
                " LEFT JOIN JobQCDetail L ON H.DocID = L.DocId  " &
                " LEFT JOIN JobIssRec S  On L.JobReceive = S.DocId  " &
                " LEFT JOIN JobReceiveDetail Jrd  ON L.JobReceive = Jrd.DocId ANd L.JobReceiveSr = Jrd.Sr  " &
                " LEFT JOIN Item I  ON Jrd.Item = I.Code  " &
                " LEFT JOIN Unit U  On I.Unit = U.Code  " &
                " LEFT JOIN Enviro E ON E.Site_Code = H.Site_Code AND E.Div_Code = H.Div_Code " &
                " Left Join Dimension1 D1   On L.Dimension1 = D1.Code " &
                " Left Join Dimension2 D2   On L.Dimension2 = D2.Code " &
                " Where H.DocID = '" & mSearchCode & "' "
        ClsMain.FPrintThisDocument(Me, TxtV_Type.Tag, mQry, "Production_JobQC_Print", "Job QC")
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

    Private Sub FrmJobQC_BaseEvent_Topctrl_tbEdit(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbEdit
        FAsignProcess()
    End Sub

    Private Sub BtnFillSaleChallan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnFillJobReceive.Click
        Try
            If Topctrl1.Mode = "Browse" Then Exit Sub
            Dim StrTicked As String = ""

            If RbtForJobReceiveItems.Checked Then
                StrTicked = FHPGD_PendingJobReceiveItems()
            Else
                StrTicked = FHPGD_PendingJobReceive()
            End If

            If StrTicked <> "" Then
                FFillItemsForReceive(StrTicked)
            Else
                Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
            End If

            Dgl1.Focus()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function FHPGD_PendingJobReceive() As String
        Dim FRH_Multiple As DMHelpGrid.FrmHelpGrid_Multi
        Dim StrRtn As String = ""

        Dim strCondHeader$ = ""
        Dim strCondLine$ = ""

        strCondHeader = " And Process = '" & TxtProcess.Tag & "' " &
                    " And Div_Code = '" & TxtDivision.Tag & "'   " &
                    " AND Site_Code = '" & TxtSite_Code.Tag & "'   " &
                    " AND V_Date <= '" & TxtV_Date.Text & "'  "

        If TxtJobWorker.Tag <> "" Then
            strCondHeader = strCondHeader & " AND H.JobWorker ='" & TxtJobWorker.Tag & "'"
        End If

        If TxtPartyName.Tag <> "" Then
            strCondLine = strCondLine & " AND JO.Party ='" & TxtPartyName.Tag & "'"
        End If

        mQry = " SELECT 'o' As Tick, VMain.JobReceive, Max(VMain.JobReceiveNo) AS JobReceiveNo, " &
                " Max(VMain.JobReceiveDate) AS JobReceiveDate, Round(IFNull(Sum(VMain.Qty), 0),4) As [Qty]    " &
                " FROM ( " & FRetFillItemWiseQry(strCondHeader, strCondLine) & " ) As VMain " &
                " GROUP BY VMain.JobReceive " &
                " Order By JobReceiveDate "

        FRH_Multiple = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(AgL.FillData(mQry, AgL.GCn).TABLES(0)), "", 400, 500, , , False)
        FRH_Multiple.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple.FFormatColumn(1, , 0, , False)
        FRH_Multiple.FFormatColumn(2, "Receive No.", 150, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(3, "Receive Date", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(4, "Balance", 100, DataGridViewContentAlignment.MiddleRight)

        FRH_Multiple.StartPosition = FormStartPosition.CenterScreen
        FRH_Multiple.ShowDialog()

        If FRH_Multiple.BytBtnValue = 0 Then
            StrRtn = FRH_Multiple.FFetchData(1, "'", "'", ",", True)
        End If
        FHPGD_PendingJobReceive = StrRtn

        FRH_Multiple = Nothing
    End Function

    Private Function FHPGD_PendingJobReceiveItems() As String
        Dim FRH_Multiple As DMHelpGrid.FrmHelpGrid_Multi
        Dim StrRtn As String = ""
        Dim strCondHeader$ = ""
        Dim strCondLine$ = ""

        strCondHeader = " And Process = '" & TxtProcess.Tag & "' " &
                    " And Div_Code = '" & TxtDivision.Tag & "'   " &
                    " AND Site_Code = '" & TxtSite_Code.Tag & "'   " &
                    " AND V_Date <= '" & TxtV_Date.Text & "'  "

        If TxtJobWorker.Tag <> "" Then
            strCondHeader = strCondHeader & " AND H.JobWorker ='" & TxtJobWorker.Tag & "'"
        End If

        If TxtPartyName.Tag <> "" Then
            strCondLine = strCondLine & " AND JO.Party ='" & TxtPartyName.Tag & "'"
        End If

        mQry = " SELECT 'o' As Tick, VMain.JobReceive + Convert(nVarChar, VMain.JobReceiveSr) As JobReceiveDocIdSr, " &
                " Max(VMain.JobReceiveNo) AS JobReceiveNo,  " &
                " Max(VMain.JobReceiveDate) AS JobReceiveDate, Max(VMain.Description) As ItemDesc, " &
                " Max(VMain." & AgTemplate.ClsMain.FGetDimension1Caption & ") As " & AgTemplate.ClsMain.FGetDimension1Caption & ", " &
                " Max(VMain." & AgTemplate.ClsMain.FGetDimension2Caption & ") As " & AgTemplate.ClsMain.FGetDimension2Caption & ", " &
                " ROUND(IFNull(Sum(VMain.Qty), 0),4) As [Qty]    " &
                " FROM ( " & FRetFillItemWiseQry(strCondHeader, strCondLine) & " ) As VMain " &
                " GROUP BY VMain.JobReceive, VMain.JobReceiveSr " &
                " Order By JobReceiveDate "

        FRH_Multiple = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(AgL.FillData(mQry, AgL.GCn).TABLES(0)), "", 500, 720, , , False)
        FRH_Multiple.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple.FFormatColumn(1, , 0, , False)
        FRH_Multiple.FFormatColumn(2, "Receive No.", 120, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(3, "Receive Date", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(4, "Item", 150, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(5, AgTemplate.ClsMain.FGetDimension1Caption, 80, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(6, AgTemplate.ClsMain.FGetDimension2Caption, 80, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(7, "Balance", 70, DataGridViewContentAlignment.MiddleRight)

        FRH_Multiple.StartPosition = FormStartPosition.CenterScreen
        FRH_Multiple.ShowDialog()

        If FRH_Multiple.BytBtnValue = 0 Then
            StrRtn = FRH_Multiple.FFetchData(1, "'", "'", ",", True)
        End If
        FHPGD_PendingJobReceiveItems = StrRtn

        FRH_Multiple = Nothing
    End Function

    Private Sub FFillItemsForReceive(ByVal bReceiveNoStr As String)
        Dim I As Integer = 0
        Dim DtTemp As DataTable = Nothing
        Try
            If bReceiveNoStr = "" Then Exit Sub

            If RbtForJobReceiveItems.Checked Then
                mQry = FRetFillItemWiseQry("", " And L.JobReceive + Convert(nVarChar, L.JobReceiveSr) In (" & bReceiveNoStr & ")")
            Else
                mQry = FRetFillItemWiseQry(" And DocId In (" & bReceiveNoStr & ") ", "")
            End If

            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)


            For I = 0 To Dgl1.Rows.Count - 1
                If Dgl1.Item(Col1Item, I).Value <> "" Then
                    Dgl1.Rows(I).Visible = False
                End If
            Next
            Dim J As Integer = Dgl1.Rows.Count - 1

            With DtTemp
                Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
                If .Rows.Count > 0 Then
                    For I = 0 To .Rows.Count - 1
                        Dgl1.Rows.Add()
                        Dgl1.Item(ColSNo, J).Value = Dgl1.Rows.Count - 1
                        Dgl1.Item(Col1JobReceive, J).Tag = AgL.XNull(.Rows(I)("JobReceive"))
                        Dgl1.Item(Col1JobReceive, J).Value = AgL.XNull(.Rows(I)("JobReceiveNo"))
                        Dgl1.Item(Col1JobReceiveSr, J).Value = AgL.XNull(.Rows(I)("JobReceiveSr"))
                        Dgl1.Item(Col1Item, J).Tag = AgL.XNull(.Rows(I)("Code"))
                        Dgl1.Item(Col1Item, J).Value = AgL.XNull(.Rows(I)("Description"))


                        Dgl1.Item(Col1Dimension1, J).Tag = AgL.XNull(.Rows(I)("Dimension1"))
                        Dgl1.Item(Col1Dimension1, J).Value = AgL.XNull(.Rows(I)(AgTemplate.ClsMain.FGetDimension1Caption()))
                        Dgl1.Item(Col1Dimension2, J).Tag = AgL.XNull(.Rows(I)("Dimension2"))
                        Dgl1.Item(Col1Dimension2, J).Value = AgL.XNull(.Rows(I)(AgTemplate.ClsMain.FGetDimension2Caption()))


                        Dgl1.Item(Col1QcQty, J).Value = Format(AgL.VNull(.Rows(I)("Qty")), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
                        Dgl1.Item(Col1Unit, J).Value = AgL.XNull(.Rows(I)("Unit"))

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
        FRetFillItemWiseQry = " SELECT Max(L.Item) As Code, Max(I.Description) As Description,   " &
                " Max(H.V_Type) + '-' +  Max(H.ManualRefNo) As JobReceiveNo,   " &
                " Max(H.V_Date) as JobReceiveDate,  " &
                " Max(D1.Description) As " & AgTemplate.ClsMain.FGetDimension1Caption & ", " &
                " Max(D2.Description) As " & AgTemplate.ClsMain.FGetDimension2Caption & ", " &
                " IFNull(Sum(L.Qty),0) - IFNull(Max(Cd.Qty), 0) as Qty,   " &
                " Max(I.Unit) as Unit, Max(U.DecimalPlaces) as QtyDecimalPlaces,  " &
                " Max(L.Dimension1) As Dimension1, " &
                " Max(L.Dimension2) As Dimension2, " &
                " L.JobReceive, L.JobReceiveSr   " &
                " FROM (  " &
                "     SELECT DocID, V_Type, ManualRefNo, V_Date " &
                "     FROM JobIssRec H    " &
                "     Where 1=1 AND IFNull(H.IsMandatory_JobQC,0) <> 0 " & HeaderConStr & " " &
                "     ) H   " &
                " LEFT JOIN JobReceiveDetail L  ON H.DocID = L.JobReceive  " &
                " LEFT JOIN JobOrder JO  ON JO.DocID = L.JobOrder  " &
                " Left Join Item I  On L.Item  = I.Code   " &
                " LEFT JOIN Voucher_Type Vt  ON H.V_Type = Vt.V_Type    " &
                " Left Join (   " &
                "     SELECT L.JobReceive, L.JobReceiveSr, Sum(L.QCQty) AS Qty, Sum(L.PassedQty) AS PassedQty " &
                " 	  FROM JobQCDetail L     " &
                "     LEFT JOIN JobQC H  ON L.DocId = H.DocID " &
                "     Where L.DocId <> '" & mSearchCode & "'  " &
                " 	  GROUP BY L.JobReceive, L.JobReceiveSr " &
                " 	) AS CD ON L.JobReceive = CD.JobReceive AND L.JobReceiveSr = CD.JobReceiveSr   " &
                " LEFT JOIN Unit U On L.Unit = U.Code   " &
                " Left Join Dimension1 D1   On L.Dimension1 = D1.Code " &
                " Left Join Dimension2 D2   On L.Dimension2 = D2.Code " &
                " WHERE 1 = 1 " & LineConStr &
                " GROUP BY L.JobReceive, L.JobReceiveSr  " &
                " Having IFNull(Sum(L.Qty),0) - IFNull(Max(Cd.Qty), 0) > 0 "
    End Function

    Private Sub FPostInProdOrder(ByVal Conn As SQLiteConnection, ByVal Cmd As SQLiteCommand)
        mQry = " Delete From ProdOrderDetail Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " Delete From ProdOrder Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "INSERT INTO ProdOrder(DocID, V_Type, V_Prefix, V_Date, V_No,Div_Code,Site_Code, " &
                " Remarks, EntryBy, EntryDate,	EntryType, Party, " &
                " EntryStatus, ApproveBy,	ApproveDate, MoveToLog, MoveToLogDate, IsDeleted, Status, ManualRefNo ) " &
                " SELECT DocID, V_Type, V_Prefix, V_Date, V_No,Div_Code,Site_Code,  " &
                " Remarks, EntryBy, EntryDate,	EntryType, Party, " &
                " EntryStatus, ApproveBy,	ApproveDate, MoveToLog, MoveToLogDate, IsDeleted, Status, ManualRefNo " &
                " FROM JobQc WHERE DocID = '" & mInternalCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        'mQry = "INSERT INTO ProdOrderDetail	(DocId,	Sr,	Item,Qty,Unit, Process, " & _
        '        " ProdOrder, ProdOrderSr, Dimension1, Dimension2 ) " & _
        '        " SELECT L.DocId, L.Sr, Jrd.Item, IFNull(L.QCQty,0) - IFNull(L.PassedQty,0) As Qty, Jrd.Unit, " & _
        '        " H.Process, L.DocId, L.Sr, L.Dimension1, L.Dimension2 " & _
        '        " FROM JobQcDetail L " & _
        '        " LEFT JOIN JobQc H ON H.DocId = L.DocId  " & _
        '        " LEFT JOIN JobReceiveDetail Jrd ON L.JobReceive = Jrd.DocId And L.JobReceiveSr = Jrd.Sr " & _
        '        " WHERE L.DocID = '" & mInternalCode & "' "
        'For QC in Dyeing House
        mQry = "INSERT INTO ProdOrderDetail	(DocId,	Sr,	Item,Qty,Unit, Process, " &
                " ProdOrder, ProdOrderSr, Dimension1, Dimension2 ) " &
                " SELECT L.DocId, L.Sr, POD.Item, IFNull(L.QCQty,0) - IFNull(L.PassedQty,0) As Qty, Jrd.Unit, " &
                " H.Process, L.DocId, L.Sr, L.Dimension1, L.Dimension2 " &
                " FROM JobQcDetail L " &
                " LEFT JOIN JobQc H ON H.DocId = L.DocId  " &
                " LEFT JOIN JobReceiveDetail Jrd ON L.JobReceive = Jrd.DocId And L.JobReceiveSr = Jrd.Sr " &
                " LEFT JOIN JobOrderDetail JOD on JRD.JobOrder = JOD.DocId and JRD.JobOrderSr = JOD.Sr " &
                " LEFT JOIN ProdOrderDetail POD on JOD.ProdOrder = POD.DocId and JOD.ProdOrderSr = POD.Sr " &
                " WHERE L.DocID = '" & mInternalCode & "'  AND IFNull(L.QCQty,0)-IFNull(L.PassedQty,0) > 0 "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    End Sub

    'Private Sub FPostStock(ByVal Conn As SqliteConnection, ByVal Cmd As SqliteCommand)
    '    Dim mMaxSr As Integer = 0
    '    mQry = " INSERT INTO Stock (DocId, Sr, V_Type, V_Prefix,  V_Date, V_No, RecID, Div_Code, Site_Code, Item, Dimension1, Dimension2, LotNo, Process, Godown, Qty_Rec, Unit, Remarks, ReferenceDocId, ReferenceDocIdSr)   " & _
    '            " SELECT H.DocID, row_number() OVER (ORDER BY Jrd.Item) + " & mMaxSr & ", max(H.V_Type) AS V_Type, max(H.V_Prefix) AS V_Prefix, max(H.V_Date) AS V_Date, max(H.V_No) AS V_No, Max(H.ManualRefNo) AS RecId,  max(H.Div_Code) AS Div_Code, max(H.Site_Code) AS Site_Code,  Jrd.Item,  " & _
    '            " L.Dimension1, L.Dimension2,  Jrd.LotNo, Max(H.Process) AS Process, max(H.Godown) AS Godown,  " & _
    '            " sum(L.PassedQty) AS Qty_Rec, Max(L.Unit) AS Unit, max(H.Remarks) AS Remarks, H.DocID, row_number() OVER (ORDER BY Jrd.Item) " & _
    '            " FROM JobQcDetail L " & _
    '            " LEFT JOIN JobQc  H ON H.DocID = L.DocID " & _
    '            " LEFT JOIN JobReceiveDetail Jrd  ON L.JobReceive = Jrd.DocId ANd L.JobReceiveSr = Jrd.Sr  " & _
    '            " WHERE H.DocID = " & AgL.Chk_Text(mSearchCode) & "   AND IFNull(L.PassedQty,0) > 0  " & _
    '            " GROUP BY H.DocID, Jrd.Item, L.Dimension1, L.Dimension2, Jrd.LotNo "
    '    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

    '    If IsQCFailed = True Then
    '        mQry = " Select Max(Sr) From Stock  Where DocId = '" & mSearchCode & "'"
    '        mMaxSr = AgL.VNull(AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar)

    '        If RbtnQCCanRepairtoanother.Checked = True Then
    '            mQry = " INSERT INTO Stock (DocId, Sr, V_Type, V_Prefix,  V_Date, V_No, RecID, Div_Code, Site_Code, Item, Dimension1, LotNo, Godown, Qty_Rec, Unit, Remarks, ReferenceDocId, ReferenceDocIdSr)   " & _
    '                    " SELECT H.DocID, row_number() OVER (ORDER BY L.StockItem) + " & mMaxSr & ", max(H.V_Type) AS V_Type, max(H.V_Prefix) AS V_Prefix, max(H.V_Date) AS V_Date, max(H.V_No) AS V_No, Max(H.ManualRefNo) AS RecId,  max(H.Div_Code) AS Div_Code, max(H.Site_Code) AS Site_Code,  L.StockItem,  " & _
    '                    " L.StockDimension1, 'QC Failed' AS LotNo, max(H.Godown) AS Godown,  " & _
    '                    "  sum(L.QCQty)-sum(L.PassedQty) AS Qty_Rec, Max(L.Unit) AS Unit, max(H.Remarks) AS Remarks, H.DocID, row_number() OVER (ORDER BY L.StockItem) " & _
    '                    "  FROM JobQcDetail L " & _
    '                    "  LEFT JOIN JobQc  H ON H.DocID = L.DocID " & _
    '                    "  LEFT JOIN JobReceiveDetail Jrd  ON L.JobReceive = Jrd.DocId ANd L.JobReceiveSr = Jrd.Sr  " & _
    '                    "  WHERE H.DocID = " & AgL.Chk_Text(mSearchCode) & " AND IFNull(L.QCQty,0)-IFNull(L.PassedQty,0) > 0   " & _
    '                    "  GROUP BY H.DocID, L.StockItem, L.StockDimension1 "
    '            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    '        ElseIf RbtnQCCanNotRepaired.Checked = True Then
    '            mQry = " INSERT INTO Stock (DocId, Sr, V_Type, V_Prefix,  V_Date, V_No, RecID, Div_Code, Site_Code, Item, Dimension1, LotNo, Godown, Qty_Rec, Unit, Remarks, ReferenceDocId, ReferenceDocIdSr)   " & _
    '                    " SELECT H.DocID, row_number() OVER (ORDER BY Jrd.Item) + " & mMaxSr & ", max(H.V_Type) AS V_Type, max(H.V_Prefix) AS V_Prefix, max(H.V_Date) AS V_Date, max(H.V_No) AS V_No, Max(H.ManualRefNo) AS RecId,  max(H.Div_Code) AS Div_Code, max(H.Site_Code) AS Site_Code,  Jrd.Item,  " & _
    '                    " '" & AgL.XNull(AgL.PubDtEnviro.Rows(0)("QCFailedDimension1")) & "' ,'QC Failed' AS LotNo,  max(H.Godown) AS Godown,  " & _
    '                    "  sum(L.QCQty)-sum(L.PassedQty) AS Qty_Rec, Max(L.Unit) AS Unit, max(H.Remarks) AS Remarks, H.DocID, row_number() OVER (ORDER BY Jrd.Item) " & _
    '                    "  FROM JobQcDetail L " & _
    '                    "  LEFT JOIN JobQc  H ON H.DocID = L.DocID " & _
    '                    "  LEFT JOIN JobReceiveDetail Jrd  ON L.JobReceive = Jrd.DocId ANd L.JobReceiveSr = Jrd.Sr  " & _
    '                    "  WHERE H.DocID = " & AgL.Chk_Text(mSearchCode) & " AND IFNull(L.QCQty,0)-IFNull(L.PassedQty,0) > 0   " & _
    '                    "  GROUP BY H.DocID, Jrd.Item "
    '            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    '        ElseIf RbtnQCCanRepaired.Checked = True Then
    '            mQry = " INSERT INTO Stock (DocId, Sr, V_Type, V_Prefix,  V_Date, V_No, RecID, Div_Code, Site_Code, Item, Dimension1, Dimension2, LotNo, Godown, Qty_Rec, Unit, Remarks, ReferenceDocId, ReferenceDocIdSr)   " & _
    '                    " SELECT H.DocID, row_number() OVER (ORDER BY Jrd.Item) + " & mMaxSr & ", max(H.V_Type) AS V_Type, max(H.V_Prefix) AS V_Prefix, max(H.V_Date) AS V_Date, max(H.V_No) AS V_No, Max(H.ManualRefNo) AS RecId,  max(H.Div_Code) AS Div_Code, max(H.Site_Code) AS Site_Code,  Jrd.Item,  " & _
    '                    " Jrd.Dimension1 , Jrd.Dimension2 , 'QC Failed' AS LotNo, max(H.Godown) AS Godown,  " & _
    '                    " sum(L.QCQty)-sum(L.PassedQty) AS Qty_Rec, Max(L.Unit) AS Unit, max(H.Remarks) AS Remarks, H.DocID, row_number() OVER (ORDER BY Jrd.Item) " & _
    '                    "  FROM JobQcDetail L " & _
    '                    "  LEFT JOIN JobQc  H ON H.DocID = L.DocID " & _
    '                    "  LEFT JOIN JobReceiveDetail Jrd  ON L.JobReceive = Jrd.DocId ANd L.JobReceiveSr = Jrd.Sr  " & _
    '                    "  WHERE H.DocID = " & AgL.Chk_Text(mSearchCode) & " AND IFNull(L.QCQty,0)-IFNull(L.PassedQty,0) > 0   " & _
    '                    "  GROUP BY H.DocID, Jrd.Item, Jrd.Dimension1 , Jrd.Dimension2 "
    '            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    '        End If
    '    End If



    'End Sub

    Private Sub FrmJobQC_BaseEvent_Save_PostTrans(ByVal SearchCode As String) Handles Me.BaseEvent_Save_PostTrans
        'Dim I As Integer
        'For I = 0 To Dgl1.RowCount - 1
        '    If Dgl1.Item(Col1StockItem, I).Value <> "" Then
        '        mQry = "INSERT INTO StockAdj(StockInDocID,StockInSr,StockOutDocID,StockOutSr,Site_Code,Div_Code,AdjQty) " & _
        '               " SELECT " & AgL.Chk_Text(Dgl1.Item(Col1JobReceive, I).Tag) & " , " & AgL.Chk_Text(Dgl1.Item(Col1JobReceiveSr, I).Value) & ", " & AgL.Chk_Text(mSearchCode) & " , 1, '" & AgL.PubSiteCode & "', '" & AgL.PubDivCode & "', " & Val(Dgl1.Item(Col1QcQty, I).Value) - Val(Dgl1.Item(Col1PassedQty, I).Value) & " "
        '        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        '    End If
        'Next
    End Sub


    Private Sub FPostStock(ByVal Conn As SQLiteConnection, ByVal Cmd As SQLiteCommand)
        Dim mMaxSr As Integer = 0
        mQry = " INSERT INTO Stock (DocId, Sr, V_Type, V_Prefix,  V_Date, V_No, RecID, Div_Code, Site_Code, Item, Dimension1, Dimension2, LotNo, Process, Godown, Qty_Rec, Unit, Remarks, ReferenceDocId, ReferenceDocIdSr)   " &
                " SELECT L.JobReceive AS DocID, row_number() OVER (ORDER BY Jrd.Item) + " & mMaxSr & ", max(JIR.V_Type) AS V_Type, max(JIR.V_Prefix) AS V_Prefix, max(JIR.V_Date) AS V_Date, max(JIR.V_No) AS V_No, Max(JIR.ManualRefNo) AS RecId,  max(JIR.Div_Code) AS Div_Code, max(JIR.Site_Code) AS Site_Code,  Jrd.Item,  " &
                " L.Dimension1, L.Dimension2,  Jrd.LotNo, Max(H.Process) AS Process, max(H.Godown) AS Godown,  " &
                " sum(L.PassedQty) AS Qty_Rec, Max(L.Unit) AS Unit, max(H.Remarks) AS Remarks, L.DocId, row_number() OVER (ORDER BY Jrd.Item) + " & mMaxSr & " " &
                " FROM JobQcDetail L " &
                " LEFT JOIN JobQc  H ON H.DocID = L.DocID " &
                " LEFT JOIN JobReceiveDetail Jrd  ON L.JobReceive = Jrd.DocId ANd L.JobReceiveSr = Jrd.Sr  " &
                " LEFT JOIN JobIssRec JIR ON JIR.DocId = Jrd.DocId " &
                " WHERE H.DocID = " & AgL.Chk_Text(mSearchCode) & "   AND IFNull(L.PassedQty,0) > 0  " &
                " GROUP BY L.JobReceive, L.DocId, Jrd.Item, L.Dimension1, L.Dimension2, Jrd.LotNo "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        If IsQCFailed = True Then
            mQry = " Select Max(Sr) From Stock  Where DocId = " & AgL.Chk_Text(Dgl1.Item(Col1JobReceive, 0).Tag) & ""
            mMaxSr = AgL.VNull(AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar)

            If RbtnQCCanRepairtoanother.Checked = True Then
                mQry = " INSERT INTO Stock (DocId, Sr, V_Type, V_Prefix,  V_Date, V_No, RecID, Div_Code, Site_Code, Item, Dimension1, LotNo, Godown, Qty_Rec, Unit, Remarks, ReferenceDocId, ReferenceDocIdSr)   " &
                        " SELECT L.JobReceive AS DocID, row_number() OVER (ORDER BY L.StockItem) + " & mMaxSr & ", max(JIR.V_Type) AS V_Type, max(JIR.V_Prefix) AS V_Prefix, max(JIR.V_Date) AS V_Date, max(JIR.V_No) AS V_No, Max(JIR.ManualRefNo) AS RecId,  max(JIR.Div_Code) AS Div_Code, max(JIR.Site_Code) AS Site_Code,  L.StockItem,  " &
                        " L.StockDimension1, 'QC Failed' AS LotNo, max(H.Godown) AS Godown,  " &
                        "  sum(L.QCQty)-sum(L.PassedQty) AS Qty_Rec, Max(L.Unit) AS Unit, max(H.Remarks) AS Remarks, L.DocId, row_number() OVER (ORDER BY L.StockItem) + " & mMaxSr & " " &
                        "  FROM JobQcDetail L " &
                        "  LEFT JOIN JobQc  H ON H.DocID = L.DocID " &
                        "  LEFT JOIN JobReceiveDetail Jrd  ON L.JobReceive = Jrd.DocId ANd L.JobReceiveSr = Jrd.Sr  " &
                        " LEFT JOIN JobIssRec JIR ON JIR.DocId = Jrd.DocId " &
                        "  WHERE H.DocID = " & AgL.Chk_Text(mSearchCode) & " AND IFNull(L.QCQty,0)-IFNull(L.PassedQty,0) > 0   " &
                        "  GROUP BY L.JobReceive, L.DocId, L.StockItem, L.StockDimension1 "
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            ElseIf RbtnQCCanNotRepaired.Checked = True Then
                mQry = " INSERT INTO Stock (DocId, Sr, V_Type, V_Prefix,  V_Date, V_No, RecID, Div_Code, Site_Code, Item, Dimension1, LotNo, Godown, Qty_Rec, Unit, Remarks, ReferenceDocId, ReferenceDocIdSr)   " &
                        " SELECT L.JobReceive AS DocID, row_number() OVER (ORDER BY Jrd.Item) + " & mMaxSr & ", max(JIR.V_Type) AS V_Type, max(JIR.V_Prefix) AS V_Prefix, max(JIR.V_Date) AS V_Date, max(JIR.V_No) AS V_No, Max(JIR.ManualRefNo) AS RecId,  max(JIR.Div_Code) AS Div_Code, max(JIR.Site_Code) AS Site_Code,  Jrd.Item,  " &
                        " '" & AgL.XNull(AgL.PubDtEnviro.Rows(0)("QCFailedDimension1")) & "' ,'QC Failed' AS LotNo,  max(H.Godown) AS Godown,  " &
                        "  sum(L.QCQty)-sum(L.PassedQty) AS Qty_Rec, Max(L.Unit) AS Unit, max(H.Remarks) AS Remarks, L.DocId, row_number() OVER (ORDER BY Jrd.Item) + " & mMaxSr & " " &
                        "  FROM JobQcDetail L " &
                        "  LEFT JOIN JobQc  H ON H.DocID = L.DocID " &
                        "  LEFT JOIN JobReceiveDetail Jrd  ON L.JobReceive = Jrd.DocId ANd L.JobReceiveSr = Jrd.Sr  " &
                        " LEFT JOIN JobIssRec JIR ON JIR.DocId = Jrd.DocId " &
                        "  WHERE H.DocID = " & AgL.Chk_Text(mSearchCode) & " AND IFNull(L.QCQty,0)-IFNull(L.PassedQty,0) > 0   " &
                        "  GROUP BY L.JobReceive, L.DocId, Jrd.Item "
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            ElseIf RbtnQCCanRepaired.Checked = True Then
                mQry = " INSERT INTO Stock (DocId, Sr, V_Type, V_Prefix,  V_Date, V_No, RecID, Div_Code, Site_Code, Item, Dimension1, Dimension2, LotNo, Godown, Qty_Rec, Unit, Remarks, ReferenceDocId, ReferenceDocIdSr)   " &
                        " SELECT L.JobReceive AS DocID, row_number() OVER (ORDER BY Jrd.Item) + " & mMaxSr & ", max(JIR.V_Type) AS V_Type, max(JIR.V_Prefix) AS V_Prefix, max(JIR.V_Date) AS V_Date, max(JIR.V_No) AS V_No, Max(JIR.ManualRefNo) AS RecId,  max(JIR.Div_Code) AS Div_Code, max(JIR.Site_Code) AS Site_Code,  Jrd.Item,  " &
                        " Jrd.Dimension1 , Jrd.Dimension2 , 'QC Failed' AS LotNo, max(H.Godown) AS Godown,  " &
                        " sum(L.QCQty)-sum(L.PassedQty) AS Qty_Rec, Max(L.Unit) AS Unit, max(H.Remarks) AS Remarks, L.DocId, row_number() OVER (ORDER BY Jrd.Item) + " & mMaxSr & " " &
                        "  FROM JobQcDetail L " &
                        "  LEFT JOIN JobQc  H ON H.DocID = L.DocID " &
                        "  LEFT JOIN JobReceiveDetail Jrd  ON L.JobReceive = Jrd.DocId ANd L.JobReceiveSr = Jrd.Sr  " &
                        " LEFT JOIN JobIssRec JIR ON JIR.DocId = Jrd.DocId " &
                        "  WHERE H.DocID = " & AgL.Chk_Text(mSearchCode) & " AND IFNull(L.QCQty,0)-IFNull(L.PassedQty,0) > 0   " &
                        "  GROUP BY L.JobReceive, L.DocId, Jrd.Item, Jrd.Dimension1 , Jrd.Dimension2 "
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            End If
        End If



    End Sub

    Private Sub BtnFillDeatil_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnFillDeatil.Click
        If Topctrl1.Mode <> "Browse" Then
            Dim I As Integer
            With Dgl1
                If .RowCount <> 0 Then
                    For I = 0 To .RowCount - 1
                        If .Item(Col1Item, I).Value <> "" Then
                            Dgl1.Item(Col1CheckedQty, I).Value = Dgl1.Item(Col1QcQty, I).Value
                            Dgl1.Item(Col1PassedQty, I).Value = Dgl1.Item(Col1QcQty, I).Value
                        End If
                    Next
                End If
                Calculation()
            End With
        End If
    End Sub
End Class
