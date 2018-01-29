Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data.SQLite
Public Class FrmJobLoss
    Inherits AgTemplate.TempTransaction
    Dim mQry$

    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Public Const ColSNo As String = "S.No."
    Public Const Col1CostCenter As String = "Cost Center"
    Public Const Col1JobOrder As String = "Job Order No"
    Protected Const Col1JobOrderSr As String = "Job Order Sr"
    Public Const Col1Item As String = "Item"
    Public Const Col1Dimension1 As String = "Dimension1"
    Public Const Col1Dimension2 As String = "Dimension2"
    Public Const Col1LotNo As String = "Lot No"
    Public Const Col1DocQty As String = "Doc Qty"
    Public Const Col1LossQty As String = "Qty"
    Public Const Col1QtyDecimalPlaces As String = "Qty Decimal Places"
    Public Const Col1Unit As String = "Unit"
    Public Const Col1MeasurePerPcs As String = "Measure Per Pcs"
    Public Const Col1DocMeasure As String = "Doc Measure"
    Public Const Col1LossMeasure As String = "Loss Measure"
    Public Const Col1MeasureUnit As String = "Measure Unit"
    Public Const Col1MeasureDecimalPlaces As String = "Measure Decimal Places"
    Public Const Col1Rate As String = "Rate"
    Public Const Col1Amount As String = "Amount"
    Protected WithEvents TxtJobWorker As AgControls.AgTextBox
    Protected WithEvents LblJobWorker As System.Windows.Forms.Label
    Protected WithEvents LblJobWorkerReq As System.Windows.Forms.Label
    Public Const Col1Remark As String = "Remark"

    Public Sub New(ByVal StrUPVar As String, ByVal DTUP As DataTable, ByVal strNCat As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Topctrl1.FSetParent(Me, StrUPVar, DTUP)
        Topctrl1.SetDisp(True)

        EntryNCat = strNCat

        mQry = "Select H.* from Voucher_Type_Settings H  Left Join Voucher_Type Vt  On H.V_Type = Vt.V_Type  Where Vt.NCat In ('" & EntryNCat & "') And H.Div_Code = '" & AgL.PubDivCode & "' And H.Site_Code ='" & AgL.PubSiteCode & "' "
        DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
    End Sub

#Region "Form Designer Code"
    Private Sub InitializeComponent()
        Me.Dgl1 = New AgControls.AgDataGrid
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.LblTotalAmount = New System.Windows.Forms.Label
        Me.LblTotalAmountText = New System.Windows.Forms.Label
        Me.LblTotalRecMeasure = New System.Windows.Forms.Label
        Me.LblTotalMeasureText = New System.Windows.Forms.Label
        Me.LblTotalRecQty = New System.Windows.Forms.Label
        Me.LblTotalQtyText = New System.Windows.Forms.Label
        Me.Pnl1 = New System.Windows.Forms.Panel
        Me.TxtRemarks = New AgControls.AgTextBox
        Me.TxtManualRefNo = New AgControls.AgTextBox
        Me.LblManualRefNo = New System.Windows.Forms.Label
        Me.LblStockHeadDetail = New System.Windows.Forms.LinkLabel
        Me.Label1 = New System.Windows.Forms.Label
        Me.BtnFillJobOrder = New System.Windows.Forms.Button
        Me.TxtProcess = New AgControls.AgTextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.TxtJobWorker = New AgControls.AgTextBox
        Me.LblJobWorker = New System.Windows.Forms.Label
        Me.LblJobWorkerReq = New System.Windows.Forms.Label
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
        Me.GroupBox2.Location = New System.Drawing.Point(746, 575)
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
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(582, 575)
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
        Me.GBoxApprove.Location = New System.Drawing.Point(415, 575)
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
        Me.GBoxEntryType.Location = New System.Drawing.Point(150, 575)
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
        Me.GBoxDivision.Location = New System.Drawing.Point(285, 575)
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
        Me.LblV_No.Location = New System.Drawing.Point(610, 183)
        Me.LblV_No.Size = New System.Drawing.Size(101, 16)
        Me.LblV_No.Tag = ""
        Me.LblV_No.Text = "Job Receive No."
        Me.LblV_No.Visible = False
        '
        'TxtV_No
        '
        Me.TxtV_No.AgSelectedValue = ""
        Me.TxtV_No.BackColor = System.Drawing.Color.White
        Me.TxtV_No.Location = New System.Drawing.Point(735, 182)
        Me.TxtV_No.Size = New System.Drawing.Size(125, 18)
        Me.TxtV_No.TabIndex = 3
        Me.TxtV_No.Tag = ""
        Me.TxtV_No.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.TxtV_No.Visible = False
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(302, 38)
        Me.Label2.Tag = ""
        '
        'LblV_Date
        '
        Me.LblV_Date.BackColor = System.Drawing.Color.Transparent
        Me.LblV_Date.Location = New System.Drawing.Point(178, 33)
        Me.LblV_Date.Size = New System.Drawing.Size(67, 16)
        Me.LblV_Date.Tag = ""
        Me.LblV_Date.Text = "Loss Date"
        '
        'LblV_TypeReq
        '
        Me.LblV_TypeReq.Location = New System.Drawing.Point(544, 19)
        Me.LblV_TypeReq.Tag = ""
        '
        'TxtV_Date
        '
        Me.TxtV_Date.AgSelectedValue = ""
        Me.TxtV_Date.BackColor = System.Drawing.Color.White
        Me.TxtV_Date.Location = New System.Drawing.Point(318, 32)
        Me.TxtV_Date.TabIndex = 2
        Me.TxtV_Date.Tag = ""
        '
        'LblV_Type
        '
        Me.LblV_Type.Location = New System.Drawing.Point(424, 13)
        Me.LblV_Type.Size = New System.Drawing.Size(67, 16)
        Me.LblV_Type.Tag = ""
        Me.LblV_Type.Text = "Loss Type"
        '
        'TxtV_Type
        '
        Me.TxtV_Type.AgSelectedValue = ""
        Me.TxtV_Type.BackColor = System.Drawing.Color.White
        Me.TxtV_Type.Location = New System.Drawing.Point(562, 12)
        Me.TxtV_Type.Size = New System.Drawing.Size(213, 18)
        Me.TxtV_Type.TabIndex = 1
        Me.TxtV_Type.Tag = ""
        '
        'LblSite_CodeReq
        '
        Me.LblSite_CodeReq.Location = New System.Drawing.Point(302, 18)
        Me.LblSite_CodeReq.Tag = ""
        '
        'LblSite_Code
        '
        Me.LblSite_Code.BackColor = System.Drawing.Color.Transparent
        Me.LblSite_Code.Location = New System.Drawing.Point(178, 13)
        Me.LblSite_Code.Size = New System.Drawing.Size(87, 16)
        Me.LblSite_Code.Tag = ""
        Me.LblSite_Code.Text = "Branch Name"
        '
        'TxtSite_Code
        '
        Me.TxtSite_Code.AgSelectedValue = ""
        Me.TxtSite_Code.BackColor = System.Drawing.Color.White
        Me.TxtSite_Code.Location = New System.Drawing.Point(318, 12)
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
        Me.LblPrefix.Location = New System.Drawing.Point(895, 32)
        Me.LblPrefix.Tag = ""
        Me.LblPrefix.Visible = False
        '
        'TabControl1
        '
        Me.TabControl1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl1.Location = New System.Drawing.Point(-5, 18)
        Me.TabControl1.Size = New System.Drawing.Size(975, 149)
        Me.TabControl1.TabIndex = 1
        '
        'TP1
        '
        Me.TP1.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.TP1.Controls.Add(Me.TxtJobWorker)
        Me.TP1.Controls.Add(Me.LblJobWorker)
        Me.TP1.Controls.Add(Me.LblJobWorkerReq)
        Me.TP1.Controls.Add(Me.Label3)
        Me.TP1.Controls.Add(Me.TxtProcess)
        Me.TP1.Controls.Add(Me.Label4)
        Me.TP1.Controls.Add(Me.Label5)
        Me.TP1.Controls.Add(Me.Label1)
        Me.TP1.Controls.Add(Me.TxtManualRefNo)
        Me.TP1.Controls.Add(Me.LblManualRefNo)
        Me.TP1.Controls.Add(Me.TxtRemarks)
        Me.TP1.Location = New System.Drawing.Point(4, 22)
        Me.TP1.Size = New System.Drawing.Size(967, 123)
        Me.TP1.Text = "Document Detail"
        Me.TP1.Controls.SetChildIndex(Me.TxtRemarks, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblPrefix, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblManualRefNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtManualRefNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtV_No, 0)
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
        Me.TP1.Controls.SetChildIndex(Me.Label1, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label5, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label4, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtProcess, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label3, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblJobWorkerReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblJobWorker, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtJobWorker, 0)
        '
        'Topctrl1
        '
        Me.Topctrl1.Size = New System.Drawing.Size(965, 41)
        Me.Topctrl1.TabIndex = 0
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
        Me.Panel1.Controls.Add(Me.LblTotalAmountText)
        Me.Panel1.Controls.Add(Me.LblTotalRecMeasure)
        Me.Panel1.Controls.Add(Me.LblTotalMeasureText)
        Me.Panel1.Controls.Add(Me.LblTotalRecQty)
        Me.Panel1.Controls.Add(Me.LblTotalQtyText)
        Me.Panel1.Location = New System.Drawing.Point(8, 542)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(948, 23)
        Me.Panel1.TabIndex = 694
        '
        'LblTotalAmount
        '
        Me.LblTotalAmount.AutoSize = True
        Me.LblTotalAmount.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalAmount.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalAmount.Location = New System.Drawing.Point(820, 3)
        Me.LblTotalAmount.Name = "LblTotalAmount"
        Me.LblTotalAmount.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalAmount.TabIndex = 668
        Me.LblTotalAmount.Text = "."
        Me.LblTotalAmount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTotalAmountText
        '
        Me.LblTotalAmountText.AutoSize = True
        Me.LblTotalAmountText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalAmountText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalAmountText.Location = New System.Drawing.Point(709, 3)
        Me.LblTotalAmountText.Name = "LblTotalAmountText"
        Me.LblTotalAmountText.Size = New System.Drawing.Size(100, 16)
        Me.LblTotalAmountText.TabIndex = 667
        Me.LblTotalAmountText.Text = "Total Amount :"
        '
        'LblTotalRecMeasure
        '
        Me.LblTotalRecMeasure.AutoSize = True
        Me.LblTotalRecMeasure.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalRecMeasure.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalRecMeasure.Location = New System.Drawing.Point(424, 3)
        Me.LblTotalRecMeasure.Name = "LblTotalRecMeasure"
        Me.LblTotalRecMeasure.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalRecMeasure.TabIndex = 666
        Me.LblTotalRecMeasure.Text = "."
        Me.LblTotalRecMeasure.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.LblTotalRecMeasure.Visible = False
        '
        'LblTotalMeasureText
        '
        Me.LblTotalMeasureText.AutoSize = True
        Me.LblTotalMeasureText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalMeasureText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalMeasureText.Location = New System.Drawing.Point(313, 3)
        Me.LblTotalMeasureText.Name = "LblTotalMeasureText"
        Me.LblTotalMeasureText.Size = New System.Drawing.Size(105, 16)
        Me.LblTotalMeasureText.TabIndex = 665
        Me.LblTotalMeasureText.Text = "Total Measure :"
        Me.LblTotalMeasureText.Visible = False
        '
        'LblTotalRecQty
        '
        Me.LblTotalRecQty.AutoSize = True
        Me.LblTotalRecQty.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalRecQty.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalRecQty.Location = New System.Drawing.Point(116, 3)
        Me.LblTotalRecQty.Name = "LblTotalRecQty"
        Me.LblTotalRecQty.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalRecQty.TabIndex = 660
        Me.LblTotalRecQty.Text = "."
        Me.LblTotalRecQty.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
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
        Me.Pnl1.Location = New System.Drawing.Point(8, 199)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(949, 342)
        Me.Pnl1.TabIndex = 3
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
        Me.TxtRemarks.Location = New System.Drawing.Point(318, 92)
        Me.TxtRemarks.MaxLength = 255
        Me.TxtRemarks.Name = "TxtRemarks"
        Me.TxtRemarks.Size = New System.Drawing.Size(457, 18)
        Me.TxtRemarks.TabIndex = 6
        '
        'TxtManualRefNo
        '
        Me.TxtManualRefNo.AgAllowUserToEnableMasterHelp = False
        Me.TxtManualRefNo.AgLastValueTag = Nothing
        Me.TxtManualRefNo.AgLastValueText = Nothing
        Me.TxtManualRefNo.AgMandatory = False
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
        Me.TxtManualRefNo.Location = New System.Drawing.Point(562, 32)
        Me.TxtManualRefNo.MaxLength = 50
        Me.TxtManualRefNo.Name = "TxtManualRefNo"
        Me.TxtManualRefNo.Size = New System.Drawing.Size(213, 18)
        Me.TxtManualRefNo.TabIndex = 3
        '
        'LblManualRefNo
        '
        Me.LblManualRefNo.AutoSize = True
        Me.LblManualRefNo.BackColor = System.Drawing.Color.Transparent
        Me.LblManualRefNo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblManualRefNo.Location = New System.Drawing.Point(424, 32)
        Me.LblManualRefNo.Name = "LblManualRefNo"
        Me.LblManualRefNo.Size = New System.Drawing.Size(60, 16)
        Me.LblManualRefNo.TabIndex = 726
        Me.LblManualRefNo.Text = "Loss No."
        '
        'LblStockHeadDetail
        '
        Me.LblStockHeadDetail.BackColor = System.Drawing.Color.SteelBlue
        Me.LblStockHeadDetail.DisabledLinkColor = System.Drawing.Color.White
        Me.LblStockHeadDetail.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblStockHeadDetail.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LblStockHeadDetail.LinkColor = System.Drawing.Color.White
        Me.LblStockHeadDetail.Location = New System.Drawing.Point(8, 178)
        Me.LblStockHeadDetail.Name = "LblStockHeadDetail"
        Me.LblStockHeadDetail.Size = New System.Drawing.Size(136, 20)
        Me.LblStockHeadDetail.TabIndex = 733
        Me.LblStockHeadDetail.TabStop = True
        Me.LblStockHeadDetail.Text = "Job Loss Detail"
        Me.LblStockHeadDetail.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(178, 93)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(53, 16)
        Me.Label1.TabIndex = 744
        Me.Label1.Text = "Remark"
        '
        'BtnFillJobOrder
        '
        Me.BtnFillJobOrder.BackColor = System.Drawing.Color.Transparent
        Me.BtnFillJobOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnFillJobOrder.Font = New System.Drawing.Font("Lucida Console", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnFillJobOrder.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnFillJobOrder.Location = New System.Drawing.Point(151, 177)
        Me.BtnFillJobOrder.Margin = New System.Windows.Forms.Padding(0)
        Me.BtnFillJobOrder.Name = "BtnFillJobOrder"
        Me.BtnFillJobOrder.Size = New System.Drawing.Size(38, 20)
        Me.BtnFillJobOrder.TabIndex = 2
        Me.BtnFillJobOrder.Text = "..."
        Me.BtnFillJobOrder.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnFillJobOrder.UseVisualStyleBackColor = False
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
        Me.TxtProcess.Location = New System.Drawing.Point(318, 52)
        Me.TxtProcess.MaxLength = 20
        Me.TxtProcess.Name = "TxtProcess"
        Me.TxtProcess.Size = New System.Drawing.Size(457, 18)
        Me.TxtProcess.TabIndex = 4
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(178, 53)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(56, 16)
        Me.Label4.TabIndex = 769
        Me.Label4.Text = "Process"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(302, 58)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(10, 7)
        Me.Label5.TabIndex = 770
        Me.Label5.Text = "Ä"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(544, 37)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(10, 7)
        Me.Label3.TabIndex = 773
        Me.Label3.Text = "Ä"
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
        Me.TxtJobWorker.Location = New System.Drawing.Point(318, 72)
        Me.TxtJobWorker.MaxLength = 20
        Me.TxtJobWorker.Name = "TxtJobWorker"
        Me.TxtJobWorker.Size = New System.Drawing.Size(457, 18)
        Me.TxtJobWorker.TabIndex = 5
        '
        'LblJobWorker
        '
        Me.LblJobWorker.AutoSize = True
        Me.LblJobWorker.BackColor = System.Drawing.Color.Transparent
        Me.LblJobWorker.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblJobWorker.Location = New System.Drawing.Point(178, 72)
        Me.LblJobWorker.Name = "LblJobWorker"
        Me.LblJobWorker.Size = New System.Drawing.Size(74, 16)
        Me.LblJobWorker.TabIndex = 775
        Me.LblJobWorker.Text = "Job Worker"
        '
        'LblJobWorkerReq
        '
        Me.LblJobWorkerReq.AutoSize = True
        Me.LblJobWorkerReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblJobWorkerReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblJobWorkerReq.Location = New System.Drawing.Point(302, 76)
        Me.LblJobWorkerReq.Name = "LblJobWorkerReq"
        Me.LblJobWorkerReq.Size = New System.Drawing.Size(10, 7)
        Me.LblJobWorkerReq.TabIndex = 776
        Me.LblJobWorkerReq.Text = "Ä"
        '
        'FrmJobLoss
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.BackColor = System.Drawing.SystemColors.ButtonShadow
        Me.ClientSize = New System.Drawing.Size(965, 616)
        Me.Controls.Add(Me.BtnFillJobOrder)
        Me.Controls.Add(Me.LblStockHeadDetail)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Pnl1)
        Me.Name = "FrmJobLoss"
        Me.Text = "Job Consumption"
        Me.Controls.SetChildIndex(Me.TabControl1, 0)
        Me.Controls.SetChildIndex(Me.Topctrl1, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.GrpUP, 0)
        Me.Controls.SetChildIndex(Me.GBoxEntryType, 0)
        Me.Controls.SetChildIndex(Me.GBoxApprove, 0)
        Me.Controls.SetChildIndex(Me.GBoxMoveToLog, 0)
        Me.Controls.SetChildIndex(Me.GroupBox2, 0)
        Me.Controls.SetChildIndex(Me.GBoxDivision, 0)
        Me.Controls.SetChildIndex(Me.Pnl1, 0)
        Me.Controls.SetChildIndex(Me.Panel1, 0)
        Me.Controls.SetChildIndex(Me.LblStockHeadDetail, 0)
        Me.Controls.SetChildIndex(Me.BtnFillJobOrder, 0)
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

    End Sub
    Public WithEvents Panel1 As System.Windows.Forms.Panel
    Public WithEvents LblTotalRecQty As System.Windows.Forms.Label
    Public WithEvents LblTotalQtyText As System.Windows.Forms.Label
    Public WithEvents Pnl1 As System.Windows.Forms.Panel
    Public WithEvents LblTotalRecMeasure As System.Windows.Forms.Label
    Public WithEvents TxtRemarks As AgControls.AgTextBox
    Public WithEvents LblTotalMeasureText As System.Windows.Forms.Label
    Public WithEvents TxtManualRefNo As AgControls.AgTextBox
    Public WithEvents LblManualRefNo As System.Windows.Forms.Label
    Public WithEvents LblStockHeadDetail As System.Windows.Forms.LinkLabel
    Public WithEvents LblTotalAmount As System.Windows.Forms.Label
    Public WithEvents LblTotalAmountText As System.Windows.Forms.Label
    Public WithEvents BtnFillJobOrder As System.Windows.Forms.Button
    Public WithEvents TxtProcess As AgControls.AgTextBox
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
#End Region

    Private Sub Frm_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "JobIssRec"
        LogTableName = "JobIssRec_Log"
        MainLineTableCsv = "JobReceiveDetail"
        LogLineTableCsv = "JobReceiveDetail_Log"
        AgL.GridDesign(Dgl1)
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mCondStr$

        mCondStr = " And IFNull(H.IsDeleted,0)=0 " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) &
                        " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "H.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        If IsApplyVTypePermission Then
            mCondStr = mCondStr & " And H.V_Type In (Select V_Type From User_VType_Permission VP Where VP.UserName = '" & AgL.PubUserName & "' And VP.Div_Code = '" & AgL.PubDivCode & "' And VP.Site_Code = '" & AgL.PubSiteCode & "') "
        End If


        AgL.PubFindQry = " SELECT H.DocID AS SearchCode, H.V_Date AS Date, H.ManualRefNo AS [Manual_No], H.Remarks  " &
                        " FROM JobIssRec H " &
                        " LEFT JOIN Voucher_Type Vt On h.V_Type = Vt.V_Type " &
                        " Where 1=1  " & mCondStr

        AgL.PubFindQry = " SELECT H.DocId AS SearchCode, H.V_Type AS [Consumption_Type], H.V_Date AS [Consumption_Date],  " &
            " H.ManualRefNo AS [Consumption_No], H.Remarks, H.EntryBy AS [Entry_By], H.EntryDate AS [Entry_Date] " &
            " FROM JobIssRec H   " &
            " LEFT JOIN Voucher_Type Vt   ON H.V_Type = vt.V_Type  " &
            " Where 1=1  " & mCondStr

        AgL.PubFindQryOrdBy = "[Date]"
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
                " From JobIssRec J " &
                " Left Join Voucher_Type Vt On J.V_Type = Vt.V_Type  " &
                " Where IFNull(IsDeleted,0) = 0  " & mCondStr & "  Order By J.V_Date, J.V_No  "

        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub FrmSaleOrder_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl1, Col1CostCenter, 100, 0, Col1CostCenter, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_CostCenter")), Boolean), False)
            .AddAgTextColumn(Dgl1, Col1Item, 200, 0, Col1Item, True, False)
            .AddAgTextColumn(Dgl1, Col1Dimension1, 100, 0, AgL.XNull(AgL.PubDtEnviro.Rows(0)("Caption_Dimension1")), CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Dimension1")), Boolean), False)
            .AddAgTextColumn(Dgl1, Col1Dimension2, 100, 0, AgL.XNull(AgL.PubDtEnviro.Rows(0)("Caption_Dimension2")), CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Dimension2")), Boolean), False)
            .AddAgTextColumn(Dgl1, Col1JobOrder, 100, 0, Col1JobOrder, True, False)
            .AddAgTextColumn(Dgl1, Col1JobOrderSr, 100, 0, Col1JobOrderSr, False, True)
            .AddAgTextColumn(Dgl1, Col1LotNo, 100, 0, Col1LotNo, True, False)
            .AddAgNumberColumn(Dgl1, Col1DocQty, 70, 8, 4, True, Col1DocQty, False, False, True)
            .AddAgNumberColumn(Dgl1, Col1LossQty, 70, 8, 4, True, Col1LossQty, True, False, True)
            .AddAgTextColumn(Dgl1, Col1Unit, 50, 0, Col1Unit, True, True)
            .AddAgTextColumn(Dgl1, Col1QtyDecimalPlaces, 50, 0, Col1QtyDecimalPlaces, False, True, False)
            .AddAgNumberColumn(Dgl1, Col1MeasurePerPcs, 70, 8, 4, False, Col1MeasurePerPcs, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_MeasurePerPcs")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_MeasurePerPcs")), Boolean), True)
            .AddAgNumberColumn(Dgl1, Col1DocMeasure, 80, 8, 4, False, Col1DocMeasure, False, True, True)
            .AddAgNumberColumn(Dgl1, Col1LossMeasure, 80, 8, 4, False, Col1LossMeasure, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Measure")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_Measure")), Boolean), True)
            .AddAgTextColumn(Dgl1, Col1MeasureUnit, 70, 0, Col1MeasureUnit, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_MeasureUnit")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_MeasureUnit")), Boolean), True)
            .AddAgTextColumn(Dgl1, Col1MeasureDecimalPlaces, 50, 0, Col1MeasureDecimalPlaces, False, True, False)
            .AddAgNumberColumn(Dgl1, Col1Rate, 70, 8, 2, False, Col1Rate, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Rate")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_Rate")), Boolean), True)
            .AddAgNumberColumn(Dgl1, Col1Amount, 70, 8, 2, False, Col1Amount, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Amount")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_Amount")), Boolean), True)
            .AddAgTextColumn(Dgl1, Col1Remark, 200, 255, Col1Remark, True, False)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 35
        Dgl1.AgSkipReadOnlyColumns = True
        Dgl1.AllowUserToOrderColumns = True

        AgCL.GridSetiingShowXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1, False)
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As SQLiteConnection, ByVal Cmd As SQLiteCommand) Handles Me.BaseEvent_Save_InTrans
        Dim I As Integer, mSr As Integer
        Dim bSelectionQry$ = ""

        mQry = "UPDATE JobIssRec " &
                " SET " &
                " ManualRefNo = " & AgL.Chk_Text(TxtManualRefNo.Text) & ", " &
                " Process = " & AgL.Chk_Text(TxtProcess.AgSelectedValue) & ", " &
                " JobWorker = " & AgL.Chk_Text(TxtJobWorker.AgSelectedValue) & ", " &
                " Remarks = " & AgL.Chk_Text(TxtRemarks.Text) & " " &
                " Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        If Topctrl1.Mode <> "Add" Then
            mQry = "Delete From JobReceiveDetail Where DocId = '" & SearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If

        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1Item, I).Value <> "" Then
                mSr += 1
                If bSelectionQry <> "" Then bSelectionQry += " UNION ALL "
                bSelectionQry += " Select " & AgL.Chk_Text(mSearchCode) & ", " &
                        " " & mSr & ", " &
                        " " & AgL.Chk_Text(Dgl1.AgSelectedValue(Col1Item, I)) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1Dimension1, I).Tag) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1Dimension2, I).Tag) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1LotNo, I).Value) & ", " &
                        " " & Val(Dgl1.Item(Col1DocQty, I).Value) & ", " &
                        " " & Val(Dgl1.Item(Col1LossQty, I).Value) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1Unit, I).Value) & ", " &
                        " " & Val(Dgl1.Item(Col1MeasurePerPcs, I).Value) & ", " &
                        " " & Val(Dgl1.Item(Col1DocMeasure, I).Value) & ", " &
                        " " & Val(Dgl1.Item(Col1LossMeasure, I).Value) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1MeasureUnit, I).Value) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1JobOrder, I).Tag) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1JobOrderSr, I).Value) & ", " &
                        " " & Val(Dgl1.Item(Col1Rate, I).Value) & ", " &
                        " " & Val(Dgl1.Item(Col1Amount, I).Value) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1Remark, I).Value) & ", " &
                        " " & AgL.Chk_Text(mSearchCode) & ", " &
                        " " & mSr & " "
            End If
        Next

        mQry = "INSERT INTO JobReceiveDetail(DocId, Sr, Item, Dimension1, Dimension2, LotNo, DocQty, LossQty, Unit, MeasurePerPcs, DocMeasure, TotalMeasure, " &
                " MeasureUnit, JobOrder, JobOrderSr, Rate, Amount, Remark, JobReceive, JobReceiveSr ) " & bSelectionQry
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Delete From StockProcess Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        Call FPostInStockProcess(Conn, Cmd)

        If AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Or AgL.StrCmp(AgL.PubUserName, "Sa") Then
            AgCL.GridSetiingWriteXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1)
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
                    " Max(H.JobWorker), L.Item, L.LotNo, Max(H.Godown), IFNull(Sum(L.Qty),0)+ IFNull(Sum(L.LossQty),0), Max(L.Unit), Max(L.MeasurePerPcs), " &
                    " Sum(L.TotalMeasure), Max(L.MeasureUnit),   " &
                    " Max(Remark), H.Process , L.Dimension1, L.Dimension2 " &
                    " From (Select * From JobIssRec Where DocId = '" & mSearchCode & "') H   " &
                    " LEFT JOIN JobReceiveDetail L On H.DocId = L.DocId   " &
                    " Group By L.DocId, L.Item, L.LotNo, H.Process, L.Dimension1, L.Dimension2 "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If
    End Sub

    Private Sub FrmProductionOrder_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim I As Integer
        Dim DsTemp As DataSet

        mQry = "Select J.*, P.Description As ProcessDesc, " &
                " Sg.Name + ',' + IFNull(C.CityName,'') As JobWorkerName " &
                " From JobIssRec J  " &
                " LEFT JOIN Process P  On J.Process = P.NCat " &
                " LEFT JOIN SubGroup SG  On J.JobWorker = Sg.SubCode " &
                " Left Join City C On Sg.CityCode = C.CityCode " &
                " Where J.DocID = '" & SearchCode & "'"
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                IniGrid()
                TxtManualRefNo.Text = AgL.XNull(.Rows(0)("ManualRefNo"))
                TxtProcess.Tag = AgL.XNull(.Rows(0)("Process"))
                TxtProcess.Text = AgL.XNull(.Rows(0)("ProcessDesc"))
                TxtJobWorker.Tag = AgL.XNull(.Rows(0)("JobWorker"))
                TxtJobWorker.Text = AgL.XNull(.Rows(0)("JobWorkerName"))
                TxtRemarks.Text = AgL.XNull(.Rows(0)("Remarks"))

                '-------------------------------------------------------------
                'Line Records are showing in Grid
                '-------------------------------------------------------------



                mQry = "Select L.*, I.Description As ItemDesc, J.V_Type + '-' + J.ManualRefNo As JobOrderNo, " &
                        " U.DecimalPlaces as QtyDecimalPlaces, MU.DecimalPlaces as MeasureDecimalPlaces, " &
                        " D1.Description As Dimension1Desc, D2.Description As Dimension2Desc " &
                        " From JobReceiveDetail L  " &
                        " LEFT JOIN Item I  On L.Item = I.Code " &
                        " LEFT JOIN JobOrder J  On L.JobOrder = J.DocId " &
                        " LEFT JOIN JobOrderDetail JOD  On L.JobOrder = JOD.DocId AND L.JobOrderSr = JOD.JobOrderSr " &
                        " Left Join Unit U  On L.Unit = U.Code " &
                        " Left Join Unit MU  On L.MeasureUnit = MU.Code " &
                        " Left Join Dimension1 D1   On L.Dimension1 = D1.Code " &
                        " Left Join Dimension2 D2   On L.Dimension2 = D2.Code " &
                        " Where L.DocId = '" & SearchCode & "' Order By L.Sr"
                DsTemp = AgL.FillData(mQry, AgL.GCn)
                With DsTemp.Tables(0)
                    Dgl1.RowCount = 1
                    Dgl1.Rows.Clear()
                    If .Rows.Count > 0 Then
                        For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                            Dgl1.Rows.Add()
                            Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count - 1

                            Dgl1.Item(Col1Item, I).Tag = AgL.XNull(.Rows(I)("Item"))
                            Dgl1.Item(Col1Item, I).Value = AgL.XNull(.Rows(I)("ItemDesc"))

                            Dgl1.Item(Col1Dimension1, I).Tag = AgL.XNull(.Rows(I)("Dimension1"))
                            Dgl1.Item(Col1Dimension1, I).Value = AgL.XNull(.Rows(I)("Dimension1Desc"))

                            Dgl1.Item(Col1Dimension2, I).Tag = AgL.XNull(.Rows(I)("Dimension2"))
                            Dgl1.Item(Col1Dimension2, I).Value = AgL.XNull(.Rows(I)("Dimension2Desc"))


                            Dgl1.Item(Col1LotNo, I).Value = AgL.XNull(.Rows(I)("LotNo"))
                            Dgl1.Item(Col1DocQty, I).Value = Format(AgL.VNull(.Rows(I)("DocQty")), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                            Dgl1.Item(Col1QtyDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("QtyDecimalPlaces"))
                            Dgl1.Item(Col1MeasurePerPcs, I).Value = Format(AgL.VNull(.Rows(I)("MeasurePerPcs")), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1DocMeasure, I).Value = Format(AgL.VNull(.Rows(I)("DocMeasure")), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1LossMeasure, I).Value = Format(AgL.VNull(.Rows(I)("TotalMeasure")), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1LossQty, I).Value = Format(AgL.VNull(.Rows(I)("LossQty")), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1MeasureUnit, I).Value = AgL.XNull(.Rows(I)("MeasureUnit"))
                            Dgl1.Item(Col1MeasureDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("MeasureDecimalPlaces"))
                            Dgl1.Item(Col1JobOrder, I).Tag = AgL.XNull(.Rows(I)("JobOrder"))
                            Dgl1.Item(Col1JobOrder, I).Value = AgL.XNull(.Rows(I)("JobOrderNo"))
                            Dgl1.Item(Col1JobOrderSr, I).Value = AgL.XNull(.Rows(I)("JobOrderSr"))

                            Dgl1.Item(Col1Rate, I).Value = Format(AgL.VNull(.Rows(I)("Rate")), "0.00")
                            Dgl1.Item(Col1Amount, I).Value = AgL.VNull(.Rows(I)("Amount"))
                            Dgl1.Item(Col1Remark, I).Value = AgL.XNull(.Rows(I)("Remark"))
                        Next I
                    End If
                End With

            End If
        End With

    End Sub

    Private Sub FrmProductionOrder_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Topctrl1.ChangeAgGridState(Dgl1, False)
        AgL.WinSetting(Me, 648, 971)
    End Sub

    Private Sub Txt_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtV_Type.Validating, TxtManualRefNo.Validating
        Select Case sender.NAME
            Case TxtV_Type.Name
                TxtManualRefNo.Text = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "StockHead", TxtV_Type.Tag, TxtV_Date.Text, TxtDivision.Tag, TxtSite_Code.Tag, AgTemplate.ClsMain.ManualRefType.Max)
                FAsignProcess()

            Case TxtManualRefNo.Name
                e.Cancel = Not AgTemplate.ClsMain.FCheckDuplicateRefNo("ManualRefNo", "StockHead", TxtV_Type.Tag, TxtV_Date.Text, TxtDivision.Tag, TxtSite_Code.Tag, Topctrl1.Mode, TxtManualRefNo.Text, mSearchCode)

            Case TxtProcess.Name
                If TxtJobWorker.AgHelpDataSet IsNot Nothing Then TxtJobWorker.AgHelpDataSet.Dispose() : TxtJobWorker.AgHelpDataSet = Nothing

        End Select
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_Topctrl_tbAdd() Handles Me.BaseEvent_Topctrl_tbAdd
        TxtManualRefNo.Text = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "StockHead", TxtV_Type.Tag, TxtV_Date.Text, TxtDivision.Tag, TxtSite_Code.Tag, AgTemplate.ClsMain.ManualRefType.Max)
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
                Case Col1LossQty
                    CType(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex), AgControls.AgTextColumn).AgNumberRightPlaces = Val(Dgl1.Item(Col1QtyDecimalPlaces, Dgl1.CurrentCell.RowIndex).Value)

                Case Col1MeasurePerPcs, Col1LossMeasure
                    CType(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex), AgControls.AgTextColumn).AgNumberRightPlaces = Val(Dgl1.Item(Col1MeasureDecimalPlaces, Dgl1.CurrentCell.RowIndex).Value)

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
                    Validating_Item(Dgl1.Item(Col1Item, mRowIndex).Tag, mRowIndex)

                Case Col1LotNo
                    Validating_LotNo(Dgl1.Item(Col1LotNo, mRowIndex).Tag, mRowIndex)

            End Select
            Call Calculation()
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
                Dgl1.Item(Col1LossQty, mRow).Value = 0
                Dgl1.Item(Col1MeasurePerPcs, mRow).Value = 0
                Dgl1.Item(Col1MeasureUnit, mRow).Value = ""
                Dgl1.AgSelectedValue(Col1JobOrder, mRow) = ""
                Dgl1.Item(Col1Rate, mRow).Value = 0
            Else
                If Dgl1.AgDataRow IsNot Nothing Then
                    Dgl1.Item(Col1LossQty, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("Bal.Qty").Value)
                    Dgl1.Item(Col1Rate, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("Rate").Value)
                    Dgl1.Item(Col1Unit, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("Unit").Value)
                    Dgl1.Item(Col1LotNo, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("LotNo").Value)
                    Dgl1.Item(Col1LotNo, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("LotNo").Value)
                    Dgl1.Item(Col1QtyDecimalPlaces, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("QtyDecimalPlaces").Value)

                    Dgl1.Item(Col1MeasurePerPcs, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("MeasurePerPcs").Value)
                    Dgl1.Item(Col1MeasureUnit, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("MeasureUnit").Value)
                    Dgl1.Item(Col1MeasureDecimalPlaces, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("MeasureDecimalPlaces").Value)

                    Dgl1.Item(Col1Dimension1, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("Dimension1").Value)
                    Dgl1.Item(Col1Dimension1, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("" & AgTemplate.ClsMain.FGetDimension1Caption() & "").Value)

                    Dgl1.Item(Col1Dimension2, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("Dimension2").Value)
                    Dgl1.Item(Col1Dimension2, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("" & AgTemplate.ClsMain.FGetDimension2Caption() & "").Value)

                    Dgl1.Item(Col1JobOrder, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("JobOrder").Value)
                    Dgl1.Item(Col1JobOrder, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("JobOrderNo").Value)
                    Dgl1.Item(Col1JobOrderSr, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("JobOrderSr").Value)




                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " On Validating_Item Function ")
        End Try
    End Sub

    Private Sub Validating_LotNo(ByVal Code As String, ByVal mRow As Integer)
        Dim DrTemp As DataRow() = Nothing
        Dim DtTemp As DataTable = Nothing
        Try
            If Dgl1.Item(Col1LotNo, mRow).Value.ToString.Trim = "" Or Dgl1.Item(Col1LotNo, mRow).Tag.ToString.Trim = "" Then
                Dgl1.Item(Col1LossQty, mRow).Value = 0
                Dgl1.Item(Col1Unit, mRow).Value = ""
                Dgl1.Item(Col1MeasurePerPcs, mRow).Value = 0
                Dgl1.Item(Col1MeasureUnit, mRow).Value = ""
                Dgl1.Item(Col1Item, mRow).Tag = ""
                Dgl1.Item(Col1Item, mRow).Value = ""
            Else
                If Dgl1.AgDataRow IsNot Nothing Then
                    Dgl1.Item(Col1Item, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("ItemCode").Value)
                    Dgl1.Item(Col1Item, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("Description").Value)
                    Dgl1.Item(Col1LossQty, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("Qty").Value)
                    Dgl1.Item(Col1Unit, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("Unit").Value)
                    Dgl1.Item(Col1QtyDecimalPlaces, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("QtyDecimalPlaces").Value)
                    Dgl1.Item(Col1MeasurePerPcs, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("MeasurePerPcs").Value)
                    Dgl1.Item(Col1MeasureUnit, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("MeasureUnit").Value)
                    Dgl1.Item(Col1LotNo, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("LotNo").Value)
                    Dgl1.Item(Col1MeasureDecimalPlaces, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("MeasureDecimalPlaces").Value)
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

        LblTotalRecQty.Text = 0
        LblTotalRecMeasure.Text = 0
        LblTotalAmount.Text = 0

        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1Item, I).Value <> "" Then
                Dgl1.Item(Col1LossMeasure, I).Value = Format(Val(Dgl1.Item(Col1LossQty, I).Value) * Val(Dgl1.Item(Col1MeasurePerPcs, I).Value), "0.000")
                Dgl1.Item(Col1DocMeasure, I).Value = Format(Val(Dgl1.Item(Col1DocQty, I).Value) * Val(Dgl1.Item(Col1MeasurePerPcs, I).Value), "0.000")
                Dgl1.Item(Col1Amount, I).Value = Format(Val(Dgl1.Item(Col1LossQty, I).Value) * Val(Dgl1.Item(Col1Rate, I).Value), "0.00")
                LblTotalRecQty.Text = Val(LblTotalRecQty.Text) + Val(Dgl1.Item(Col1LossQty, I).Value)
                LblTotalRecMeasure.Text = Val(LblTotalRecMeasure.Text) + Val(Dgl1.Item(Col1LossMeasure, I).Value)
                LblTotalAmount.Text = Val(LblTotalAmount.Text) + Val(Dgl1.Item(Col1Amount, I).Value)
            End If
        Next
        LblTotalRecQty.Text = Format(Val(LblTotalRecQty.Text), "0.000")
        LblTotalRecMeasure.Text = Format(Val(LblTotalRecMeasure.Text), "0.000")
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        Dim I As Integer = 0
        Dim BalQty As Double = 0
        If AgCL.AgIsBlankGrid(Dgl1, Dgl1.Columns(Col1Item).Index) = True Then passed = False : Exit Sub

        passed = AgTemplate.ClsMain.FCheckDuplicateRefNo("ManualRefNo", "JobIssRec", TxtV_Type.Tag, TxtV_Date.Text, TxtDivision.Tag, TxtSite_Code.Tag, Topctrl1.Mode, TxtManualRefNo.Text, mSearchCode)

        With Dgl1
            For I = 0 To .Rows.Count - 1
                If .Item(Col1Item, I).Value <> "" Then
                    If Val(.Item(Col1LossQty, I).Value) = 0 Then
                        MsgBox("Qty Is 0 At Row No " & Dgl1.Item(ColSNo, I).Value & "")
                        .CurrentCell = .Item(Col1LossQty, I) : Dgl1.Focus()
                        passed = False : Exit Sub
                    End If

                    ' For Validation of Stock Process 
                    If CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsPostedInStockProcess")), Boolean) Then
                        mQry = "SELECT IFNull(sum(H.Qty_Rec),0) - IFNull(sum(H.Qty_Iss),0) AS BalQty " &
                                " FROM StockProcess H  " &
                                " WHERE H.DocID <> " & AgL.Chk_Text(mSearchCode) & " AND H.SubCode = " & AgL.Chk_Text(TxtJobWorker.Tag) & " " &
                                " AND H.Item = " & AgL.Chk_Text(.Item(Col1Item, I).Tag) & " AND H.Process = " & AgL.Chk_Text(TxtProcess.Tag) & " " &
                                " AND IFNull(H.LotNo,'') = '" & .Item(Col1LotNo, I).Value & "' " &
                                " GROUP BY H.Item, IFNull(H.LotNo,'') "
                        BalQty = AgL.VNull(AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar)
                        If Math.Round(BalQty, 4) < Math.Round(Val(.Item(Col1LossQty, I).Value), 4) Then
                            MsgBox("Balance Qty of " & Dgl1.Item(Col1Item, I).Value & " is " & BalQty & " For Lot No = '" & Dgl1.Item(Col1LotNo, I).Value & "'")
                            .CurrentCell = .Item(Col1LossQty, I) : Dgl1.Focus()
                            passed = False : Exit Sub
                        End If
                    End If
                End If
            Next
        End With
    End Sub

    Private Function FCheckDuplicateRefNo() As Boolean
        FCheckDuplicateRefNo = True
        If Topctrl1.Mode = "Add" Then
            mQry = " SELECT COUNT(*) FROM StockHead WHERE ManualRefNo = '" & TxtManualRefNo.Text & "'   " &
                    " AND V_Type ='" & TxtV_Type.Tag & "'  And Div_Code = '" & TxtDivision.Tag & "' And Site_Code = '" & TxtSite_Code.Tag & "' And IFNull(IsDeleted,0) = 0  "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then FCheckDuplicateRefNo = False : MsgBox("Reference No. Already Exists") : TxtManualRefNo.Focus()
        Else
            mQry = " SELECT COUNT(*) FROM StockHead WHERE ManualRefNo = '" & TxtManualRefNo.Text & "'  " &
                    " AND V_Type ='" & TxtV_Type.Tag & "'  And Div_Code = '" & TxtDivision.Tag & "' And Site_Code = '" & TxtSite_Code.Tag & "' And IFNull(IsDeleted,0) = 0 AND DocID <>'" & mInternalCode & "'  "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then FCheckDuplicateRefNo = False : MsgBox("Reference No. Already Exists") : TxtManualRefNo.Focus()
        End If
    End Function

    Private Sub FrmProductionOrder_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
        Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
        LblTotalRecMeasure.Text = 0 : LblTotalRecQty.Text = 0
    End Sub

    Private Sub TempJobOrder_BaseEvent_ApproveDeletion_InTrans(ByVal SearchCode As String, ByVal Conn As SQLiteConnection, ByVal Cmd As SQLiteCommand) Handles Me.BaseEvent_ApproveDeletion_InTrans
        mQry = "Delete From StockProcess Where DocId = '" & mInternalCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    End Sub

    Private Sub FrmWeavingMaterialPenaltyNew_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub

    Private Sub BtnFillSaleChallan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnFillJobOrder.Click
        Try
            If Topctrl1.Mode = "Browse" Then Exit Sub
            Dim StrTicked As String = ""

            StrTicked = FHPGD_PendingJobOrderItems()
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
                " Max(VMain.JobOrderDate) AS JobOrderDate, Max(VMain.Description) As ItemDesc, " &
                " ROUND(IFNull(Sum(VMain.Qty), 0),4) As [Qty]    " &
                " FROM ( " & FRetFillItemWiseQry(strCond, "") & " ) As VMain " &
                " GROUP BY VMain.JobOrder, VMain.JobOrderSr " &
                " Order By JobOrderDate "

        FRH_Multiple = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(AgL.FillData(mQry, AgL.GCn).TABLES(0)), "", 500, 640, , , False)
        FRH_Multiple.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple.FFormatColumn(1, , 0, , False)
        FRH_Multiple.FFormatColumn(2, "Order No.", 120, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(3, "Order Date", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(4, "Item", 200, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(5, "Balance", 100, DataGridViewContentAlignment.MiddleRight)

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

            mQry = FRetFillItemWiseQry("", " And L.JobOrder + Convert(nVarChar, L.Sr) In (" & bOrderNoStr & ")")

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

                        Dgl1.Item(Col1Dimension1, I).Tag = AgL.XNull(.Rows(I)("Dimension1"))
                        Dgl1.Item(Col1Dimension1, I).Value = AgL.XNull(.Rows(I)("Dimension1Desc"))

                        Dgl1.Item(Col1Dimension2, I).Tag = AgL.XNull(.Rows(I)("Dimension2"))
                        Dgl1.Item(Col1Dimension2, I).Value = AgL.XNull(.Rows(I)("Dimension2Desc"))


                        Dgl1.Item(Col1LotNo, I).Value = AgL.XNull(.Rows(I)("LotNo"))

                        Dgl1.Item(Col1QtyDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("QtyDecimalPlaces"))
                        Dgl1.Item(Col1MeasureDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("MeasureDecimalPlaces"))
                        Dgl1.Item(Col1Item, I).Tag = AgL.XNull(.Rows(I)("Code"))
                        Dgl1.Item(Col1Item, I).Value = AgL.XNull(.Rows(I)("Description"))
                        Dgl1.Item(Col1DocQty, I).Value = Format(AgL.VNull(.Rows(I)("Qty")), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
                        Dgl1.Item(Col1LossQty, I).Value = Format(AgL.VNull(.Rows(I)("Qty")), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
                        Dgl1.Item(Col1Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                        Dgl1.Item(Col1MeasurePerPcs, I).Value = Format(AgL.VNull(.Rows(I)("MeasurePerPcs")), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                        Dgl1.Item(Col1MeasureUnit, I).Value = AgL.XNull(.Rows(I)("MeasureUnit"))


                    Next I
                End If
            End With
            Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function FRetFillItemWiseQry(ByVal HeaderConStr As String, ByVal LineConStr As String) As String
        FRetFillItemWiseQry = " SELECT Max(L.Item_Uid) As Item_Uid, Max(L.Item) As Code, Max(I.Description) as Description, " &
                    " Max(I.ManualCode) As ManualCode,  Max(L.LotNo) AS LotNo, " &
                    " Max(H.V_Type) + '-' +  Max(H.ManualRefNo) AS JobOrderNo,   " &
                    " Max(H.V_Date) as JobOrderDate,  " &
                    " Round(Sum(L.Qty),4) - round(IFNull(Max(Cd.Qty), 0),4) As Qty,   " &
                    " Max(I.Unit) as Unit,   " &
                    " Max(I.MeasureUnit) MeasureUnit, Max(L.Rate) as Rate,  Max(L.IncentiveRate) as IncentiveRate,  " &
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
                    "     SELECT DocID, V_Type, ManualRefNo, V_Date, Status, IsOrderOfUndefinedQty   " &
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
                    " Left Join Dimension1 D1 On L.Dimension1 = D1.Code " &
                    " Left Join Dimension2 D2 On L.Dimension2 = D2.Code " &
                    " WHERE 1 = 1 " & LineConStr &
                    " GROUP BY L.JobOrder, L.JobOrderSr  " &
                    " HAVING (round(IFNull(Sum(L.Qty),0),4) - round(IFNull(Max(Cd.Qty), 0),4) > 0  Or IFNull(Max(IsOrderOfUndefinedQty + 0),0) <> 0)   "

        '" 	) AS CD ON L.JobOrder = CD.JobOrder AND L.JobOrderSr = CD.JobOrderSr   " & _
    End Function


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

        mQry = " SELECT Max(L.Item) As Code, Max(I.Description) As Description, " &
                " Max(H.V_Type) + '-' +  Max(H.ManualRefNo) As JobOrderNo, Max(H.V_Date) as JobOrderDate," &
                " Max(D1.Description) As " & AgTemplate.ClsMain.FGetDimension1Caption() & ", " &
                " Max(D2.Description) As " & AgTemplate.ClsMain.FGetDimension2Caption() & ", Max(L.LotNo) As LotNo, " &
                " Round(Sum(L.Qty) - IFNull(Sum(Cd.Qty), 0),4) as [Bal.Qty],  Max(IG.Description) AS ItemGroupDesc,  " &
                " Max(I.Unit) as Unit,   " &
                " Sum(L.TotalMeasure) - IFNull(Sum(Cd.TotalMeasure), 0) as [Bal.Measure],    " &
                " Max(I.MeasureUnit) MeasureUnit, Max(L.Rate) as Rate,   " &
                " Max(I.SalesTaxPostingGroup) SalesTaxPostingGroup, L.JobOrder,   " &
                " Max(L.MeasurePerPcs) as MeasurePerPcs, " &
                " Max(L.ProdOrder) As ProdOrder, Max(Po.ManualRefNo) As ProdOrderNo, " &
                " L.JobOrderSr, Max(U.DecimalPlaces) as QtyDecimalPlaces,  " &
                " Max(U1.DecimalPlaces) as MeasureDecimalPlaces, Max(L.ProdOrderSr) As ProdOrderSr, " &
                " Max(L.Dimension1) As Dimension1, Max(L.Dimension2) As Dimension2, Max(L.IncentiveRate) as IncentiveRate  " &
                " FROM (  " &
                "     SELECT DocID, V_Type, ManualRefNo, V_Date, IsOrderOfUndefinedQty  " &
                "     FROM JobOrder    " &
                "     WHERE JobWorker ='" & TxtJobWorker.Tag & "'   " &
                "     And Process = '" & TxtProcess.Tag & "'   " &
                "     And Div_Code = '" & TxtDivision.Tag & "'   " &
                "     AND Site_Code = '" & TxtSite_Code.Tag & "'   " &
                "     AND V_Date <= '" & TxtV_Date.Text & "'   " &
                "     And IFNull(Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "'  " &
                "     ) H   " &
                " LEFT JOIN JobOrderDetail L  ON H.DocID = L.JobOrder  " &
                " LEFT JOIN ProdOrder Po  ON L.ProdOrder = Po.DocId " &
                " Left Join Item I  On L.Item  = I.Code   " &
                " LEFT JOIN ItemGroup IG On Ig.Code = I.ItemGroup " &
                " LEFT JOIN Voucher_Type Vt  ON H.V_Type = Vt.V_Type    " &
                " Left Join (   " &
                "     SELECT L.JobOrder, L.JobOrderSr, Sum(L.Qty) + IFNull(Sum(L.LossQty),0) AS Qty, Sum(L.TotalMeasure) As TotalMeasure " &
                " 	  FROM JobReceiveDetail L     " &
                "     Where L.DocId <> '" & mSearchCode & "'  " &
                " 	  GROUP BY L.JobOrder, L.JobOrderSr   " &
                " 	) AS CD ON L.DocId = CD.JobOrder AND L.Sr = CD.JobOrderSr   " &
                " LEFT JOIN Unit U On L.Unit = U.Code   " &
                " LEFT JOIN Unit U1 On L.MeasureUnit = U1.Code   " &
                " Left Join Dimension1 D1 On L.Dimension1 = D1.Code " &
                " Left Join Dimension2 D2 On L.Dimension2 = D2.Code " &
                " WHERE 1=1  " & strCond &
                " GROUP BY L.JobOrder, L.JobOrderSr  " &
                " Having (ROUND(Sum(L.Qty),4) - ROUND(IFNull(Max(Cd.Qty), 0),4) > 0 Or IFNull(Max(IsOrderOfUndefinedQty + 0),0) <> 0) " &
                " Order By JobOrderDate  "
        Dgl1.AgHelpDataSet(Col1Item, 15) = AgL.FillData(mQry, AgL.GCn)
    End Sub


    Private Sub Dgl1_EditingControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.EditingControl_KeyDown
        Try
            Dim MRowIndex As Integer = Dgl1.CurrentCell.RowIndex
            If AgL.StrCmp(Topctrl1.Mode, "Browse") Then Exit Sub
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Item
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1Item) Is Nothing Then
                            FCreateHelpItem()
                        End If
                    End If

            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub TxtOrderBy_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtProcess.KeyDown, TxtJobWorker.KeyDown
        Try
            Select Case sender.name
                Case TxtProcess.Name
                    If e.KeyCode <> Keys.Enter Then
                        If sender.AgHelpDataSet Is Nothing Then
                            mQry = " SELECT H.NCat AS Code, H.Description AS Process FROM Process H "
                            sender.AgHelpDataSet(0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If

                Case TxtJobWorker.Name
                    If e.KeyCode <> Keys.Enter Then
                        If TxtJobWorker.AgHelpDataSet Is Nothing Then
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

            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub TempJobOrder_BaseEvent_Topctrl_tbRef() Handles Me.BaseEvent_Topctrl_tbRef
        Try
            If Dgl1.AgHelpDataSet(Col1Item) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1Item).Dispose() : Dgl1.AgHelpDataSet(Col1Item) = Nothing
            If TxtJobWorker.AgHelpDataSet IsNot Nothing Then TxtJobWorker.AgHelpDataSet.Dispose() : TxtJobWorker.AgHelpDataSet = Nothing
            If Dgl1.AgHelpDataSet(Col1CostCenter) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1CostCenter).Dispose() : Dgl1.AgHelpDataSet(Col1CostCenter) = Nothing
        Catch ex As Exception
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
    End Sub

    Private Sub FrmJobConsumption_BaseEvent_Topctrl_tbEdit(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbEdit
        FAsignProcess()
    End Sub

    Private Sub FrmWeavingMaterialPenaltyNew_BaseEvent_Topctrl_tbPrn(ByVal SearchCode As String) Handles Me.BaseEvent_Topctrl_tbPrn
        mQry = " SELECT H.V_Date, H.V_Type + '-' + H.ManualRefNo As ManualRefNo, H.Remarks, P.Description As ProcessDesc, " &
                " H.EntryBy, H.EntryDate, H.ApproveBy, H.ApproveDate, JO.V_TYpe + '-' + JO.ManualrefNo AS OrderNo, " &
                " H.RoundOff, H.NetAmount, L.Qty, L.Unit, L.MeasurePerPcs, L.LotNo, " &
                " L.Sr, L.TotalMeasure, L.MeasureUnit, L.Rate, L.Amount, L.PerimeterPerPcs, L.TotalPerimeter, " &
                " L.Remark As LineRemark, U.DecimalPlaces AS UnitDecimalPlaces, " &
                " Sg.Name AS JobWorkerName, L.LossPer, L.LossQty, L.RetQty, " &
                " D1.Description AS D1Desc, D2.Description AS D2Desc, E.Caption_Dimension1, E.Caption_Dimension2, " &
                " Sg.Add1, Sg.Add2, Sg.Add3, Sg.Mobile, Sg.PAN, I.Description AS ItemDesc " &
                " FROM JobIssRec H   " &
                " LEFT JOIN JobReceiveDetail L  ON H.DocID = L.DocId " &
                " LEFT JOIN JobOrder JO  ON JO.DocID = L.JobOrder " &
                " LEFT JOIN SubGroup Sg  ON H.JobWorker = Sg.SubCode " &
                " LEFT JOIN Item I  ON L.Item = I.Code " &
                " LEFT JOIN Process P  On H.Process = P.NCat " &
                " LEFT JOIN Enviro E ON E.Site_Code = H.Site_Code AND E.Div_Code = H.Div_Code " &
                " LEFT JOIN Dimension1 D1 ON D1.Code = L.Dimension1 " &
                " LEFT JOIN Dimension2 D2 ON D2.Code = L.Dimension2 " &
                " LEFT JOIN Unit U ON L.Unit = U.Code  " &
                " WHERE H.DocID =  '" & mSearchCode & "' Order By L.Sr "
        ClsMain.FPrintThisDocument(Me, TxtV_Type.Tag, mQry, "Production_JobLoss_Print", "Job Loss From " & TxtProcess.Text)
    End Sub
End Class
