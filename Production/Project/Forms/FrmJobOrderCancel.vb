Imports System.IO
Imports System.Data.SQLite
Public Class FrmJobOrderCancel
    Inherits AgTemplate.TempTransaction
    Public mQry$
    Protected Const ColSNo As String = "S.No."
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Protected Const Col1Item_Uid As String = "Item Uid"
    Protected Const Col1Item As String = "Item"
    Protected Const Col1LotNo As String = "Lot No"
    Protected Const Col1Dimension1 As String = "Dimension1"
    Protected Const Col1Dimension2 As String = "Dimension2"
    Protected Const Col1FromProcess As String = "From Process"
    Protected Const Col1JobOrder As String = "Job Order"
    Protected Const Col1JobOrderSr As String = "Job Order Sr"
    Protected Const Col1ProdOrder As String = "Prod Order"
    Protected Const Col1ProdOrderSr As String = "Prod Order Sr"
    Protected Const Col1Qty As String = "Qty"
    Protected Const Col1Unit As String = "Unit"
    Protected Const Col1QtyDecimalPlaces As String = "Qty Decimal Places"
    Protected Const Col1MeasurePerPcs As String = "Measure Per Pcs"
    Protected Const Col1TotalMeasure As String = "Total Measure"
    Protected Const Col1MeasureUnit As String = "Measure Unit"
    Protected Const Col1MeasureDecimalPlaces As String = "Measure Decimal Places"
    Protected Const Col1Rate As String = "Rate"
    Protected Const Col1Amount As String = "Amount"

    Public WithEvents Dgl5 As New AgControls.AgDataGrid
    Protected Const Col5Head As String = "Head"
    Protected Const Col5AtRate As String = "@"
    Protected Const Col5Amount As String = "Amount"

    Protected Const Row5GrossAmount As Byte = 0
    Protected Const Row5RoundOff As Byte = 1
    Protected Const Row5NetAmount As Byte = 2

    Protected mLastOrderBy$ = ""

    Dim mJobRateHelpDataSet As DataSet = Nothing
    Protected WithEvents RbtCancelForOrderItem As System.Windows.Forms.RadioButton
    Protected WithEvents GrpDirectInvoice As System.Windows.Forms.GroupBox
    Protected WithEvents RbtForOrder As System.Windows.Forms.RadioButton
    Protected WithEvents BtnConsumptionDetail As System.Windows.Forms.Button
    Dim DtJobEnviro As DataTable = Nothing

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
        Me.LblTotalMeasure = New System.Windows.Forms.Label
        Me.Label33 = New System.Windows.Forms.Label
        Me.LblTotalQty = New System.Windows.Forms.Label
        Me.LblTotalQtyText = New System.Windows.Forms.Label
        Me.Pnl1 = New System.Windows.Forms.Panel
        Me.Label30 = New System.Windows.Forms.Label
        Me.TxtRemarks = New AgControls.AgTextBox
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel
        Me.LblJobWorkerReq = New System.Windows.Forms.Label
        Me.TxtJobWorker = New AgControls.AgTextBox
        Me.LblJobWorker = New System.Windows.Forms.Label
        Me.TxtTermsAndConditions = New AgControls.AgTextBox
        Me.LinkLabel2 = New System.Windows.Forms.LinkLabel
        Me.TxtBillingType = New AgControls.AgTextBox
        Me.Label32 = New System.Windows.Forms.Label
        Me.TxtOrderBy = New AgControls.AgTextBox
        Me.LblOrderBy = New System.Windows.Forms.Label
        Me.LblOrderByReq = New System.Windows.Forms.Label
        Me.TxtGodown = New AgControls.AgTextBox
        Me.LblGodown = New System.Windows.Forms.Label
        Me.Pnl5 = New System.Windows.Forms.Panel
        Me.Label3 = New System.Windows.Forms.Label
        Me.TxtProcess = New AgControls.AgTextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.BtnFillJobOrder = New System.Windows.Forms.Button
        Me.RbtCancelForOrderItem = New System.Windows.Forms.RadioButton
        Me.GrpDirectInvoice = New System.Windows.Forms.GroupBox
        Me.RbtForOrder = New System.Windows.Forms.RadioButton
        Me.BtnConsumptionDetail = New System.Windows.Forms.Button
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
        Me.Label2.Location = New System.Drawing.Point(107, 40)
        Me.Label2.Tag = ""
        '
        'LblV_Date
        '
        Me.LblV_Date.BackColor = System.Drawing.Color.Transparent
        Me.LblV_Date.Location = New System.Drawing.Point(7, 35)
        Me.LblV_Date.Size = New System.Drawing.Size(79, 16)
        Me.LblV_Date.Tag = ""
        Me.LblV_Date.Text = "Cancel Date"
        '
        'LblV_TypeReq
        '
        Me.LblV_TypeReq.Location = New System.Drawing.Point(308, 20)
        Me.LblV_TypeReq.Tag = ""
        '
        'TxtV_Date
        '
        Me.TxtV_Date.AgSelectedValue = ""
        Me.TxtV_Date.BackColor = System.Drawing.Color.White
        Me.TxtV_Date.Location = New System.Drawing.Point(123, 34)
        Me.TxtV_Date.TabIndex = 2
        Me.TxtV_Date.Tag = ""
        '
        'LblV_Type
        '
        Me.LblV_Type.Location = New System.Drawing.Point(229, 16)
        Me.LblV_Type.Size = New System.Drawing.Size(79, 16)
        Me.LblV_Type.Tag = ""
        Me.LblV_Type.Text = "Cancel Type"
        '
        'TxtV_Type
        '
        Me.TxtV_Type.AgSelectedValue = ""
        Me.TxtV_Type.BackColor = System.Drawing.Color.White
        Me.TxtV_Type.Location = New System.Drawing.Point(324, 14)
        Me.TxtV_Type.Size = New System.Drawing.Size(153, 18)
        Me.TxtV_Type.TabIndex = 1
        Me.TxtV_Type.Tag = ""
        '
        'LblSite_CodeReq
        '
        Me.LblSite_CodeReq.Location = New System.Drawing.Point(107, 20)
        Me.LblSite_CodeReq.Tag = ""
        '
        'LblSite_Code
        '
        Me.LblSite_Code.BackColor = System.Drawing.Color.Transparent
        Me.LblSite_Code.Location = New System.Drawing.Point(7, 16)
        Me.LblSite_Code.Size = New System.Drawing.Size(87, 16)
        Me.LblSite_Code.TabIndex = 0
        Me.LblSite_Code.Tag = ""
        Me.LblSite_Code.Text = "Branch Name"
        '
        'TxtSite_Code
        '
        Me.TxtSite_Code.AgMandatory = True
        Me.TxtSite_Code.AgSelectedValue = ""
        Me.TxtSite_Code.BackColor = System.Drawing.Color.White
        Me.TxtSite_Code.Location = New System.Drawing.Point(123, 14)
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
        Me.TabControl1.Size = New System.Drawing.Size(991, 134)
        Me.TabControl1.TabIndex = 0
        '
        'TP1
        '
        Me.TP1.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.TP1.Controls.Add(Me.Label7)
        Me.TP1.Controls.Add(Me.TxtProcess)
        Me.TP1.Controls.Add(Me.Label4)
        Me.TP1.Controls.Add(Me.Label5)
        Me.TP1.Controls.Add(Me.Label3)
        Me.TP1.Controls.Add(Me.TxtGodown)
        Me.TP1.Controls.Add(Me.LblGodown)
        Me.TP1.Controls.Add(Me.TxtOrderBy)
        Me.TP1.Controls.Add(Me.LblOrderBy)
        Me.TP1.Controls.Add(Me.LblOrderByReq)
        Me.TP1.Controls.Add(Me.TxtManualRefNo)
        Me.TP1.Controls.Add(Me.Label32)
        Me.TP1.Controls.Add(Me.TxtBillingType)
        Me.TP1.Controls.Add(Me.LblManualRefNo)
        Me.TP1.Controls.Add(Me.TxtRemarks)
        Me.TP1.Controls.Add(Me.Label30)
        Me.TP1.Controls.Add(Me.TxtJobWorker)
        Me.TP1.Controls.Add(Me.LblJobWorker)
        Me.TP1.Controls.Add(Me.LblJobWorkerReq)
        Me.TP1.Location = New System.Drawing.Point(4, 22)
        Me.TP1.Size = New System.Drawing.Size(983, 108)
        Me.TP1.Text = "Document Detail"
        Me.TP1.Controls.SetChildIndex(Me.LblJobWorkerReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblJobWorker, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtJobWorker, 0)
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
        Me.TP1.Controls.SetChildIndex(Me.LblOrderByReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblOrderBy, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtOrderBy, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblGodown, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtGodown, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label3, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label5, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label4, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtProcess, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label7, 0)
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
        Me.TxtManualRefNo.Location = New System.Drawing.Point(324, 34)
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
        Me.LblManualRefNo.Location = New System.Drawing.Point(229, 34)
        Me.LblManualRefNo.Name = "LblManualRefNo"
        Me.LblManualRefNo.Size = New System.Drawing.Size(68, 16)
        Me.LblManualRefNo.TabIndex = 706
        Me.LblManualRefNo.Text = "Cancel No"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Cornsilk
        Me.Panel1.Controls.Add(Me.LblTotalAmount)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.LblTotalMeasure)
        Me.Panel1.Controls.Add(Me.Label33)
        Me.Panel1.Controls.Add(Me.LblTotalQty)
        Me.Panel1.Controls.Add(Me.LblTotalQtyText)
        Me.Panel1.Location = New System.Drawing.Point(4, 418)
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
        Me.LblTotalMeasure.Location = New System.Drawing.Point(525, 3)
        Me.LblTotalMeasure.Name = "LblTotalMeasure"
        Me.LblTotalMeasure.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalMeasure.TabIndex = 670
        Me.LblTotalMeasure.Text = "."
        Me.LblTotalMeasure.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.ForeColor = System.Drawing.Color.Maroon
        Me.Label33.Location = New System.Drawing.Point(411, 3)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(105, 16)
        Me.Label33.TabIndex = 669
        Me.Label33.Text = "Total Measure :"
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
        Me.Pnl1.Location = New System.Drawing.Point(4, 176)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(972, 241)
        Me.Pnl1.TabIndex = 2
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(486, 56)
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
        Me.TxtRemarks.Location = New System.Drawing.Point(587, 55)
        Me.TxtRemarks.MaxLength = 255
        Me.TxtRemarks.Multiline = True
        Me.TxtRemarks.Name = "TxtRemarks"
        Me.TxtRemarks.Size = New System.Drawing.Size(384, 37)
        Me.TxtRemarks.TabIndex = 8
        '
        'LinkLabel1
        '
        Me.LinkLabel1.BackColor = System.Drawing.Color.SteelBlue
        Me.LinkLabel1.DisabledLinkColor = System.Drawing.Color.White
        Me.LinkLabel1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel1.LinkColor = System.Drawing.Color.White
        Me.LinkLabel1.Location = New System.Drawing.Point(4, 155)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(219, 20)
        Me.LinkLabel1.TabIndex = 731
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "Job Order Cancel For Following Items"
        Me.LinkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblJobWorkerReq
        '
        Me.LblJobWorkerReq.AutoSize = True
        Me.LblJobWorkerReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblJobWorkerReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblJobWorkerReq.Location = New System.Drawing.Point(570, 20)
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
        Me.TxtJobWorker.Location = New System.Drawing.Point(587, 15)
        Me.TxtJobWorker.MaxLength = 20
        Me.TxtJobWorker.Name = "TxtJobWorker"
        Me.TxtJobWorker.Size = New System.Drawing.Size(384, 18)
        Me.TxtJobWorker.TabIndex = 6
        '
        'LblJobWorker
        '
        Me.LblJobWorker.AutoSize = True
        Me.LblJobWorker.BackColor = System.Drawing.Color.Transparent
        Me.LblJobWorker.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblJobWorker.Location = New System.Drawing.Point(486, 15)
        Me.LblJobWorker.Name = "LblJobWorker"
        Me.LblJobWorker.Size = New System.Drawing.Size(74, 16)
        Me.LblJobWorker.TabIndex = 731
        Me.LblJobWorker.Text = "Job Worker"
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
        Me.TxtTermsAndConditions.Location = New System.Drawing.Point(4, 462)
        Me.TxtTermsAndConditions.MaxLength = 255
        Me.TxtTermsAndConditions.Multiline = True
        Me.TxtTermsAndConditions.Name = "TxtTermsAndConditions"
        Me.TxtTermsAndConditions.Size = New System.Drawing.Size(615, 114)
        Me.TxtTermsAndConditions.TabIndex = 3
        '
        'LinkLabel2
        '
        Me.LinkLabel2.BackColor = System.Drawing.Color.SteelBlue
        Me.LinkLabel2.DisabledLinkColor = System.Drawing.Color.White
        Me.LinkLabel2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel2.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel2.LinkColor = System.Drawing.Color.White
        Me.LinkLabel2.Location = New System.Drawing.Point(4, 439)
        Me.LinkLabel2.Name = "LinkLabel2"
        Me.LinkLabel2.Size = New System.Drawing.Size(162, 20)
        Me.LinkLabel2.TabIndex = 748
        Me.LinkLabel2.TabStop = True
        Me.LinkLabel2.Text = "Job Terms && Conditions"
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
        Me.TxtOrderBy.Location = New System.Drawing.Point(123, 74)
        Me.TxtOrderBy.MaxLength = 20
        Me.TxtOrderBy.Name = "TxtOrderBy"
        Me.TxtOrderBy.Size = New System.Drawing.Size(354, 18)
        Me.TxtOrderBy.TabIndex = 5
        '
        'LblOrderBy
        '
        Me.LblOrderBy.AutoSize = True
        Me.LblOrderBy.BackColor = System.Drawing.Color.Transparent
        Me.LblOrderBy.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblOrderBy.Location = New System.Drawing.Point(7, 74)
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
        Me.LblOrderByReq.Location = New System.Drawing.Point(107, 80)
        Me.LblOrderByReq.Name = "LblOrderByReq"
        Me.LblOrderByReq.Size = New System.Drawing.Size(10, 7)
        Me.LblOrderByReq.TabIndex = 752
        Me.LblOrderByReq.Text = "Ä"
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
        Me.TxtGodown.Location = New System.Drawing.Point(587, 35)
        Me.TxtGodown.MaxLength = 255
        Me.TxtGodown.Name = "TxtGodown"
        Me.TxtGodown.Size = New System.Drawing.Size(384, 18)
        Me.TxtGodown.TabIndex = 7
        '
        'LblGodown
        '
        Me.LblGodown.AutoSize = True
        Me.LblGodown.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblGodown.Location = New System.Drawing.Point(486, 36)
        Me.LblGodown.Name = "LblGodown"
        Me.LblGodown.Size = New System.Drawing.Size(55, 16)
        Me.LblGodown.TabIndex = 757
        Me.LblGodown.Text = "Godown"
        '
        'Pnl5
        '
        Me.Pnl5.Location = New System.Drawing.Point(624, 445)
        Me.Pnl5.Name = "Pnl5"
        Me.Pnl5.Size = New System.Drawing.Size(353, 132)
        Me.Pnl5.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(308, 41)
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
        Me.TxtProcess.Location = New System.Drawing.Point(123, 54)
        Me.TxtProcess.MaxLength = 20
        Me.TxtProcess.Name = "TxtProcess"
        Me.TxtProcess.Size = New System.Drawing.Size(354, 18)
        Me.TxtProcess.TabIndex = 4
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(7, 54)
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
        Me.Label5.Location = New System.Drawing.Point(107, 60)
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
        Me.Label7.Location = New System.Drawing.Point(570, 40)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(10, 7)
        Me.Label7.TabIndex = 769
        Me.Label7.Text = "Ä"
        '
        'BtnFillJobOrder
        '
        Me.BtnFillJobOrder.BackColor = System.Drawing.Color.Transparent
        Me.BtnFillJobOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnFillJobOrder.Font = New System.Drawing.Font("Lucida Console", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnFillJobOrder.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnFillJobOrder.Location = New System.Drawing.Point(536, 155)
        Me.BtnFillJobOrder.Margin = New System.Windows.Forms.Padding(0)
        Me.BtnFillJobOrder.Name = "BtnFillJobOrder"
        Me.BtnFillJobOrder.Size = New System.Drawing.Size(38, 20)
        Me.BtnFillJobOrder.TabIndex = 1
        Me.BtnFillJobOrder.Text = "..."
        Me.BtnFillJobOrder.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnFillJobOrder.UseVisualStyleBackColor = False
        '
        'RbtCancelForOrderItem
        '
        Me.RbtCancelForOrderItem.AutoSize = True
        Me.RbtCancelForOrderItem.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RbtCancelForOrderItem.Location = New System.Drawing.Point(125, 7)
        Me.RbtCancelForOrderItem.Name = "RbtCancelForOrderItem"
        Me.RbtCancelForOrderItem.Size = New System.Drawing.Size(156, 17)
        Me.RbtCancelForOrderItem.TabIndex = 743
        Me.RbtCancelForOrderItem.TabStop = True
        Me.RbtCancelForOrderItem.Text = "For Job Order Items"
        Me.RbtCancelForOrderItem.UseVisualStyleBackColor = True
        '
        'GrpDirectInvoice
        '
        Me.GrpDirectInvoice.BackColor = System.Drawing.Color.Transparent
        Me.GrpDirectInvoice.Controls.Add(Me.RbtForOrder)
        Me.GrpDirectInvoice.Controls.Add(Me.RbtCancelForOrderItem)
        Me.GrpDirectInvoice.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GrpDirectInvoice.Location = New System.Drawing.Point(232, 148)
        Me.GrpDirectInvoice.Name = "GrpDirectInvoice"
        Me.GrpDirectInvoice.Size = New System.Drawing.Size(288, 26)
        Me.GrpDirectInvoice.TabIndex = 3005
        Me.GrpDirectInvoice.TabStop = False
        '
        'RbtForOrder
        '
        Me.RbtForOrder.AutoSize = True
        Me.RbtForOrder.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RbtForOrder.Location = New System.Drawing.Point(4, 8)
        Me.RbtForOrder.Name = "RbtForOrder"
        Me.RbtForOrder.Size = New System.Drawing.Size(114, 17)
        Me.RbtForOrder.TabIndex = 0
        Me.RbtForOrder.TabStop = True
        Me.RbtForOrder.Text = "For Job Order"
        Me.RbtForOrder.UseVisualStyleBackColor = True
        '
        'BtnConsumptionDetail
        '
        Me.BtnConsumptionDetail.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnConsumptionDetail.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnConsumptionDetail.Location = New System.Drawing.Point(606, 153)
        Me.BtnConsumptionDetail.Name = "BtnConsumptionDetail"
        Me.BtnConsumptionDetail.Size = New System.Drawing.Size(71, 23)
        Me.BtnConsumptionDetail.TabIndex = 3006
        Me.BtnConsumptionDetail.TabStop = False
        Me.BtnConsumptionDetail.Text = "Material"
        Me.BtnConsumptionDetail.UseVisualStyleBackColor = True
        '
        'FrmJobOrderCancel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.BackColor = System.Drawing.SystemColors.ButtonShadow
        Me.ClientSize = New System.Drawing.Size(984, 626)
        Me.Controls.Add(Me.BtnConsumptionDetail)
        Me.Controls.Add(Me.GrpDirectInvoice)
        Me.Controls.Add(Me.BtnFillJobOrder)
        Me.Controls.Add(Me.LinkLabel2)
        Me.Controls.Add(Me.Pnl5)
        Me.Controls.Add(Me.TxtTermsAndConditions)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Pnl1)
        Me.Name = "FrmJobOrderCancel"
        Me.Text = "Template Job Order"
        Me.Controls.SetChildIndex(Me.TabControl1, 0)
        Me.Controls.SetChildIndex(Me.Pnl1, 0)
        Me.Controls.SetChildIndex(Me.Panel1, 0)
        Me.Controls.SetChildIndex(Me.LinkLabel1, 0)
        Me.Controls.SetChildIndex(Me.TxtTermsAndConditions, 0)
        Me.Controls.SetChildIndex(Me.Pnl5, 0)
        Me.Controls.SetChildIndex(Me.LinkLabel2, 0)
        Me.Controls.SetChildIndex(Me.GBoxApprove, 0)
        Me.Controls.SetChildIndex(Me.Topctrl1, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.GrpUP, 0)
        Me.Controls.SetChildIndex(Me.GBoxEntryType, 0)
        Me.Controls.SetChildIndex(Me.GBoxMoveToLog, 0)
        Me.Controls.SetChildIndex(Me.GroupBox2, 0)
        Me.Controls.SetChildIndex(Me.GBoxDivision, 0)
        Me.Controls.SetChildIndex(Me.BtnFillJobOrder, 0)
        Me.Controls.SetChildIndex(Me.GrpDirectInvoice, 0)
        Me.Controls.SetChildIndex(Me.BtnConsumptionDetail, 0)
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
    Protected WithEvents TxtManualRefNo As AgControls.AgTextBox
    Protected WithEvents LblManualRefNo As System.Windows.Forms.Label
    Protected WithEvents Panel1 As System.Windows.Forms.Panel
    Protected WithEvents Pnl1 As System.Windows.Forms.Panel
    Protected WithEvents TxtRemarks As AgControls.AgTextBox
    Protected WithEvents Label30 As System.Windows.Forms.Label
    Protected WithEvents LblTotalMeasure As System.Windows.Forms.Label
    Protected WithEvents Label33 As System.Windows.Forms.Label
    Protected WithEvents LblTotalQty As System.Windows.Forms.Label
    Protected WithEvents LblTotalQtyText As System.Windows.Forms.Label
    Protected WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    Protected WithEvents LblJobWorkerReq As System.Windows.Forms.Label
    Protected WithEvents TxtJobWorker As AgControls.AgTextBox
    Protected WithEvents LblJobWorker As System.Windows.Forms.Label
    Protected WithEvents TxtTermsAndConditions As AgControls.AgTextBox
    Protected WithEvents LblTotalAmount As System.Windows.Forms.Label
    Protected WithEvents Label1 As System.Windows.Forms.Label
    Protected WithEvents LinkLabel2 As System.Windows.Forms.LinkLabel
    Protected WithEvents TxtBillingType As AgControls.AgTextBox
    Protected WithEvents Label32 As System.Windows.Forms.Label
    Protected WithEvents TxtOrderBy As AgControls.AgTextBox
    Protected WithEvents LblOrderBy As System.Windows.Forms.Label
    Protected WithEvents LblOrderByReq As System.Windows.Forms.Label
    Protected WithEvents TxtGodown As AgControls.AgTextBox
    Protected WithEvents LblGodown As System.Windows.Forms.Label
    Protected WithEvents Pnl5 As System.Windows.Forms.Panel
    Protected WithEvents Label3 As System.Windows.Forms.Label
    Protected WithEvents TxtProcess As AgControls.AgTextBox
    Protected WithEvents Label4 As System.Windows.Forms.Label
    Protected WithEvents Label5 As System.Windows.Forms.Label
    Protected WithEvents Label7 As System.Windows.Forms.Label
    Protected WithEvents BtnFillJobOrder As System.Windows.Forms.Button
#End Region

    Private Sub FrmFinishingOrder_BaseEvent_ApproveDeletion_InTrans(ByVal SearchCode As String, ByVal Conn As SqliteConnection, ByVal Cmd As SqliteCommand) Handles Me.BaseEvent_ApproveDeletion_InTrans
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

        mQry = "Delete From JobOrderBom Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " Delete from JobIssRecUid Where DocId = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " UPDATE JobIssRecUid Set JobRecDocID = Null Where JobRecDocID = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Delete From JobReceiveDetail Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Delete From JobIssRec Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

    End Sub

    Private Sub FrmQuality1_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "JobOrder"
        LogTableName = "JobOrder_Log"
        MainLineTableCsv = "JobOrderdetail"
        LogLineTableCsv = "JobOrderdetail_Log"

        AgL.GridDesign(Dgl1)
        AgL.GridDesign(Dgl5)
    End Sub

    Private Sub FrmQuality1_BaseFunction_FIniMast(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMast
        Dim mCondStr$
        mCondStr = " " & AgL.CondStrFinancialYear("M.V_Date", AgL.PubStartDate, AgL.PubEndDate) & _
                       " And " & AgL.PubSiteCondition("M.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "M.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        If IsApplyVTypePermission Then
            mCondStr = mCondStr & " And M.V_Type In (Select V_Type From User_VType_Permission VP Where VP.UserName = '" & AgL.PubUserName & "' And VP.Div_Code = '" & AgL.PubDivCode & "' And VP.Site_Code = '" & AgL.PubSiteCode & "') "
        End If


        mQry = " Select M.DocID As SearchCode " &
            " From JobOrder M   " &
            " Left Join Voucher_Type Vt   On M.V_Type = Vt.V_Type  " &
            " Where IFNull(IsDeleted,0) = 0  " & mCondStr & "  Order By M.V_Date, M.V_No  "

        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mCondStr$

        mCondStr = " And IFNull(H.IsDeleted,0)=0 " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) &
                        " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "H.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        If IsApplyVTypePermission Then
            mCondStr = mCondStr & " And H.V_Type In (Select V_Type From User_VType_Permission VP Where VP.UserName = '" & AgL.PubUserName & "' And VP.Div_Code = '" & AgL.PubDivCode & "' And VP.Site_Code = '" & AgL.PubSiteCode & "') "
        End If


        AgL.PubFindQry = " SELECT H.DocId AS SearchCode, H.V_Type AS [ORDER_Type], H.V_Date AS [ORDER_Date], P.Description AS Process,  " &
                    " H.ManualRefNo AS [Order_No], SGJ.Name AS [Job_Jober], SGO.Name AS [ORDER_BY], G.Description AS Godown,  " &
                    " Abs(H.TotalQty) AS [Total_Qty], Abs(H.TotalMeasure) AS [Total_Measure], Abs(H.TotalAmount) AS [Total_Amount],  " &
                    " H.Remarks, H.EntryBy AS [Entry_By], H.EntryDate AS [Entry_Date], " &
                    " H.ApproveBy AS [Approve By], H.ApproveDate AS [Approve Date]  " &
                    " FROM JobOrder H   " &
                    " LEFT JOIN Voucher_Type Vt   ON H.V_Type = vt.V_Type  " &
                    " LEFT JOIN SubGroup SGJ   ON SGJ.SubCode=H.Jobworker  " &
                    " LEFT JOIN SubGroup SGO   ON SGO.SubCode = H.OrderBy  " &
                    " LEFT JOIN Process P ON P.NCat = H.Process " &
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
            .AddAgTextColumn(Dgl1, Col1Dimension1, 100, 0, AgL.XNull(AgL.PubDtEnviro.Rows(0)("Caption_Dimension1")), CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Dimension1")), Boolean), False)
            .AddAgTextColumn(Dgl1, Col1Dimension2, 100, 0, AgL.XNull(AgL.PubDtEnviro.Rows(0)("Caption_Dimension2")), CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Dimension2")), Boolean), False)
            .AddAgTextColumn(Dgl1, Col1LotNo, 80, 20, Col1LotNo, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_LotNo")), Boolean), True)
            .AddAgTextColumn(Dgl1, Col1FromProcess, 90, 0, Col1FromProcess, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_ProcessLine")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_ProcessLine")), Boolean), False)
            .AddAgTextColumn(Dgl1, Col1JobOrder, 100, 0, Col1JobOrder, True, True)
            .AddAgTextColumn(Dgl1, Col1JobOrderSr, 100, 0, Col1JobOrderSr, False, True)
            .AddAgTextColumn(Dgl1, Col1ProdOrder, 100, 0, Col1ProdOrder, True, True, False)
            .AddAgTextColumn(Dgl1, Col1ProdOrderSr, 100, 0, Col1ProdOrderSr, False, True)
            .AddAgNumberColumn(Dgl1, Col1Qty, 50, 8, 4, False, Col1Qty, True, False, True)
            .AddAgTextColumn(Dgl1, Col1Unit, 60, 0, Col1Unit, True, True)
            .AddAgTextColumn(Dgl1, Col1QtyDecimalPlaces, 50, 0, Col1QtyDecimalPlaces, False, True, False)
            .AddAgNumberColumn(Dgl1, Col1MeasurePerPcs, 70, 8, 4, False, Col1MeasurePerPcs, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_MeasurePerPcs")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_MeasurePerPcs")), Boolean), True)
            .AddAgNumberColumn(Dgl1, Col1TotalMeasure, 70, 8, 4, False, Col1TotalMeasure, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Measure")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_Measure")), Boolean), True)
            .AddAgTextColumn(Dgl1, Col1MeasureUnit, 70, 0, Col1MeasureUnit, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_MeasureUnit")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_MeasureUnit")), Boolean))
            .AddAgTextColumn(Dgl1, Col1MeasureDecimalPlaces, 50, 0, Col1MeasureDecimalPlaces, False, True, False)
            .AddAgNumberColumn(Dgl1, Col1Rate, 60, 8, 2, False, Col1Rate, True, False, True)
            .AddAgNumberColumn(Dgl1, Col1Amount, 70, 8, 2, False, Col1Amount, True, True, True)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 40
        Dgl1.AgSkipReadOnlyColumns = True

        AgTemplate.ClsMain.ProcCreateLink(Dgl1, Col1ProdOrder)

        Dgl1.AllowUserToOrderColumns = True

        AgTemplate.ClsMain.ProcCreateLink(Dgl1, Col1JobOrder)
        AgTemplate.ClsMain.ProcCreateLink(Dgl1, Col1ProdOrder)

        With AgCL
            .AddAgTextColumn(Dgl5, Col5Head, 150, 5, Col5Head, True, True)
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
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As SQLiteConnection, ByVal Cmd As SQLiteCommand) Handles Me.BaseEvent_Save_InTrans
        Dim I As Integer, mSr As Integer
        Dim bSelectionQry$ = ""

        mQry = "UPDATE JobOrder " &
                " SET " &
                " ManualRefNo = " & AgL.Chk_Text(TxtManualRefNo.Text) & ", " &
                " Process = " & AgL.Chk_Text(TxtProcess.AgSelectedValue) & ", " &
                " Jobworker = " & AgL.Chk_Text(TxtJobWorker.AgSelectedValue) & ", " &
                " OrderBy = " & AgL.Chk_Text(TxtOrderBy.AgSelectedValue) & ", " &
                " BillingType = " & AgL.Chk_Text(TxtBillingType.Text) & ", " &
                " TotalQty = " & -Val(LblTotalQty.Text) & ", " &
                " TotalAmount = " & -Val(LblTotalAmount.Text) & ", " &
                " RoundOff = " & -Val(Dgl5.Item(Col5Amount, Row5RoundOff).Value) & ", " &
                " NetAmount = " & -Val(Dgl5.Item(Col5Amount, Row5NetAmount).Value) & ", " &
                " TotalMeasure = " & Val(LblTotalMeasure.Text) & ", " &
                " Remarks = " & AgL.Chk_Text(TxtRemarks.Text) & ", " &
                " TermsAndConditions = " & AgL.Chk_Text(TxtTermsAndConditions.Text) & ", " &
                " Godown = " & AgL.Chk_Text(TxtGodown.AgSelectedValue) & " " &
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
                            " " & AgL.Chk_Text(.Item(Col1JobOrder, I).Tag) & ", " &
                            " " & AgL.Chk_Text(.Item(Col1JobOrderSr, I).Value) & ", " &
                            " " & AgL.Chk_Text(.Item(Col1ProdOrder, I).Tag) & ", " &
                            " " & AgL.Chk_Text(.Item(Col1ProdOrderSr, I).Value) & ", " &
                            " " & AgL.Chk_Text(.Item(Col1LotNo, I).Value) & ", " &
                            " " & AgL.Chk_Text(.Item(Col1Dimension1, I).Tag) & ", " &
                            " " & AgL.Chk_Text(.Item(Col1Dimension2, I).Tag) & ", " &
                            " " & AgL.Chk_Text(.Item(Col1FromProcess, I).Tag) & ", " &
                            " " & -Val(.Item(Col1Qty, I).Value) & ", " & AgL.Chk_Text(.Item(Col1Unit, I).Value) & ",	" &
                            " " & Val(.Item(Col1MeasurePerPcs, I).Value) & ", " & -Val(.Item(Col1TotalMeasure, I).Value) & ", " &
                            " " & AgL.Chk_Text(.Item(Col1MeasureUnit, I).Value) & ", " &
                            " " & AgTemplate.ClsMain.T_Nature.Cancellation & ", " &
                            " " & Val(.Item(Col1Rate, I).Value) & ",	" &
                            " " & -Val(.Item(Col1Amount, I).Value) & ""
                End If
            Next
        End With

        If bSelectionQry <> "" Then
            mQry = "  INSERT INTO JobOrderDetail(DocId, Sr, " &
                    " Item_Uid, Item, JobOrder, JobOrderSr, ProdOrder, ProdOrderSr, LotNo, Dimension1, Dimension2, FromProcess, Qty, Unit, MeasurePerPcs, TotalMeasure, " &
                    " MeasureUnit, T_Nature, Rate, Amount) " & bSelectionQry
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If

        FPostInStockProcess(mSearchCode, Conn, Cmd)
        FPostInJobIssRecUID(mSearchCode, Conn, Cmd)

        'For I = 0 To Dgl1.Rows.Count - 1
        '    If Dgl1.Item(Col1Item_Uid, I).Tag <> "" Then
        '        AgTemplate.ClsMain.FUpdateItem_Uid(Dgl1.Item(Col1Item_Uid, I).Tag, Topctrl1.Mode, mSearchCode, TxtV_Type.Tag, TxtV_Date.Text, TxtJobWorker.Tag, TxtGodown.Tag, TxtProcess.Tag, AgTemplate.ClsMain.Item_UidStatus.Receive, TxtManualRefNo.Text, Conn, Cmd)
        '    End If
        'Next

        If AgL.PubUserName.ToUpper = AgLibrary.ClsConstant.PubSuperUserName.ToUpper Then
            AgCL.GridSetiingWriteXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1)
        End If

        mLastOrderBy = TxtOrderBy.AgSelectedValue

        If AgL.VNull(AgL.Dman_Execute(" Select IFNull(IsAllowed_MaterialIssue,0) From JobOrder  Where DocId = '" & mSearchCode & "'", AgL.GcnRead).ExecuteScalar) <> 0 Then
            FPostMaterialReceive(Conn, Cmd)
        End If
    End Sub

    Private Sub FrmProductionOrder_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim I As Integer
        Dim DrTemp As DataRow() = Nothing
        Dim DsTemp As DataSet
        Dim DtItem As DataTable = Nothing

        mQry = "Select P.*, Sg.DispName As JobworkerName, Sg1.DispName As OrderByName, " &
                " G.Description As GodownDesc, Pr.Description As ProcessDesc " &
                " From JobOrder P  " &
                " LEFT JOIN SubGroup Sg On P.Jobworker = Sg.SubCode " &
                " LEFT JOIN SubGroup Sg1 On P.OrderBy = Sg1.SubCode " &
                " LEFT JOIN Godown G On P.Godown = G.Code " &
                " LEFT JOIN Process Pr On P.Process = Pr.NCat " &
                " Where P.DocID = '" & SearchCode & "'"
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                IniGrid()
                TxtManualRefNo.Text = AgL.XNull(.Rows(0)("ManualRefNo"))

                TxtJobWorker.Tag = AgL.XNull(.Rows(0)("Jobworker"))
                TxtJobWorker.Text = AgL.XNull(.Rows(0)("JobworkerName"))

                TxtOrderBy.Tag = AgL.XNull(.Rows(0)("OrderBy"))
                TxtOrderBy.Text = AgL.XNull(.Rows(0)("OrderByName"))

                TxtProcess.Tag = AgL.XNull(.Rows(0)("Process"))
                TxtProcess.Text = AgL.XNull(.Rows(0)("ProcessDesc"))

                TxtRemarks.Text = AgL.XNull(.Rows(0)("Remarks"))
                TxtTermsAndConditions.Text = AgL.XNull(.Rows(0)("TermsAndConditions"))
                TxtBillingType.Text = AgL.XNull(.Rows(0)("BillingType"))

                LblTotalQty.Text = Math.Abs(AgL.VNull(.Rows(0)("TotalQty")))
                LblTotalAmount.Text = Math.Abs(AgL.VNull(.Rows(0)("TotalAmount")))
                LblTotalMeasure.Text = Math.Abs(AgL.VNull(.Rows(0)("TotalMeasure")))

                TxtGodown.Tag = AgL.XNull(.Rows(0)("Godown"))
                TxtGodown.Text = AgL.XNull(AgL.Dman_Execute(" SELECT Description FROM Godown WHERE Code =  '" & AgL.XNull(.Rows(0)("Godown")) & "' ", AgL.GCn).ExecuteScalar)

                Dgl5.Item(Col5Amount, Row5GrossAmount).Value = AgL.VNull(.Rows(0)("TotalAmount"))
                Dgl5.Item(Col5Amount, Row5RoundOff).Value = AgL.VNull(.Rows(0)("RoundOff"))
                Dgl5.Item(Col5Amount, Row5NetAmount).Value = AgL.VNull(.Rows(0)("NetAmount"))

                '-------------------------------------------------------------
                'Line Records are showing in First Grid
                '-------------------------------------------------------------
                mQry = "Select L.*, I.Description As ItemDesc, P.Description AS FromProcessDesc, " &
                        " U.DecimalPlaces as QtyDecimalPlaces, MU.DecimalPlaces as MeasureDecimalPlaces, " &
                        " D1.Description As Dimension1Desc, D2.Description As Dimension2Desc, " &
                        " PO.V_Type + '-' + Po.ManualRefNo As ProdOrderNo, " &
                        " J.V_Type + '-' + J.ManualRefNo As JobOrderNo " &
                        " From JobOrderDetail L  " &
                        " LEFT JOIN Item I On L.Item = I.Code " &
                        " LEFT JOIN ProdOrder Po On L.ProdOrder = Po.DocId " &
                        " LEFT JOIN JobOrder J On L.JobOrder = J.DocId " &
                        " LEFT JOIN Process P On L.FromProcess = P.NCat " &
                        " Left Join Unit U On L.Unit = U.Code " &
                        " Left Join Unit MU On L.MeasureUnit = MU.Code " &
                        " Left Join Dimension1 D1   On L.Dimension1 = D1.Code " &
                        " Left Join Dimension2 D2   On L.Dimension2 = D2.Code " &
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
                            Dgl1.Item(Col1Item_Uid, I).Value = AgL.XNull(AgL.Dman_Execute("Select Item_Uid From Item_Uid Where Code = '" & AgL.XNull(.Rows(I)("Item_Uid")) & "' ", AgL.GCn).ExecuteScalar)

                            Dgl1.Item(Col1Item, I).Tag = AgL.XNull(.Rows(I)("Item"))
                            Dgl1.Item(Col1Item, I).Value = AgL.XNull(.Rows(I)("ItemDesc"))

                            Dgl1.Item(Col1FromProcess, I).Tag = AgL.XNull(.Rows(I)("FromProcess"))
                            Dgl1.Item(Col1FromProcess, I).Value = AgL.XNull(.Rows(I)("FromProcessDesc"))
                            Dgl1.Item(Col1JobOrder, I).Tag = AgL.XNull(.Rows(I)("JobOrder"))
                            Dgl1.Item(Col1JobOrder, I).Value = AgL.XNull(.Rows(I)("JobOrderNo"))

                            Dgl1.Item(Col1JobOrderSr, I).Value = AgL.VNull(.Rows(I)("JobOrderSr"))

                            Dgl1.Item(Col1ProdOrder, I).Tag = AgL.XNull(.Rows(I)("ProdOrder"))
                            Dgl1.Item(Col1ProdOrder, I).Value = AgL.XNull(.Rows(I)("ProdOrderNo"))

                            Dgl1.Item(Col1ProdOrderSr, I).Value = AgL.VNull(.Rows(I)("ProdOrderSr"))

                            Dgl1.Item(Col1Qty, I).Value = Format(Math.Abs(AgL.VNull(.Rows(I)("Qty"))), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))

                            Dgl1.Item(Col1QtyDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("QtyDecimalPlaces"))

                            Dgl1.Item(Col1Dimension1, I).Tag = AgL.XNull(.Rows(I)("Dimension1"))
                            Dgl1.Item(Col1Dimension1, I).Value = AgL.XNull(.Rows(I)("Dimension1Desc"))

                            Dgl1.Item(Col1Dimension2, I).Tag = AgL.XNull(.Rows(I)("Dimension2"))
                            Dgl1.Item(Col1Dimension2, I).Value = AgL.XNull(.Rows(I)("Dimension2Desc"))

                            Dgl1.Item(Col1MeasurePerPcs, I).Value = Format(AgL.VNull(.Rows(I)("MeasurePerPcs")), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1TotalMeasure, I).Value = Format(Math.Abs(AgL.VNull(.Rows(I)("TotalMeasure"))), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1MeasureUnit, I).Value = AgL.XNull(.Rows(I)("MeasureUnit"))

                            Dgl1.Item(Col1MeasureDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("MeasureDecimalPlaces"))

                            Dgl1.Item(Col1Rate, I).Value = AgL.VNull(.Rows(I)("Rate"))
                            Dgl1.Item(Col1Amount, I).Value = Math.Abs(AgL.VNull(.Rows(I)("Amount")))
                        Next I
                    End If
                End With
                Calculation()
                '-------------------------------------------------------------
            End If
        End With

        AgCL.GridSetiingShowXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1, False)
        BtnConsumptionDetail.Tag = Nothing
    End Sub

    Private Sub FrmProductionOrder_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Topctrl1.ChangeAgGridState(Dgl1, False)
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

                Case Col1FromProcess
                    Dgl1.AgHelpDataSet(Col1FromProcess) = Nothing
                    If Dgl1.Item(Col1Item_Uid, Dgl1.CurrentCell.RowIndex).Value <> "" Then
                        Dgl1.Columns(Col1FromProcess).ReadOnly = True
                    Else
                        Dgl1.Columns(Col1FromProcess).ReadOnly = False
                    End If

                Case Col1LotNo
                    Dgl1.AgHelpDataSet(Col1LotNo) = Nothing

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

    'Private Sub FrmProductionOrder_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
    '    Dim I As Integer = 0
    '    Dim DtTemp As DataTable = Nothing
    '    Dim StrMessage As String = ""

    '    passed = FCheckDuplicateRefNo()

    '    If AgL.RequiredField(TxtJobWorker, LblJobWorker.Text) Then passed = False : Exit Sub
    '    If AgCL.AgIsBlankGrid(Dgl1, Dgl1.Columns(Col1Item).Index) Then passed = False : Exit Sub


    '    Dim mTampQry = " Declare @TmpTable as Table " & _
    '                  " ( " & _
    '                  " Item nVarchar(100), " & _
    '                  " JobOrder nVarchar(100), " & _
    '                  " Qty Float " & _
    '                  " )"

    '    With Dgl1
    '        For I = 0 To .Rows.Count - 1
    '            If .Item(Col1Item, I).Value <> "" Then
    '                StrMessage = ""
    '                If Val(.Item(Col1Qty, I).Value) = 0 Then
    '                    If StrMessage <> "" Then StrMessage += vbCrLf
    '                    StrMessage += "Qty Is 0 At Row No " & Dgl1.Item(ColSNo, I).Value & ""
    '                End If
    '                If StrMessage <> "" Then
    '                    MsgBox(StrMessage)
    '                    passed = False : Exit Sub
    '                End If

    '                'If Val(.Item(Col1Rate, I).Value) = 0 Then
    '                '    If StrMessage <> "" Then StrMessage += vbCrLf
    '                '    StrMessage += "Rate Is 0 At Row No " & Dgl1.Item(ColSNo, I).Value & ""
    '                'End If
    '                'If StrMessage <> "" Then
    '                '    MsgBox(StrMessage)
    '                '    passed = False : Exit Sub
    '                'End If

    '                If StrMessage <> "" Then
    '                    If MsgBox(StrMessage & vbCrLf & "Do you want to continue?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
    '                        passed = False : Exit Sub
    '                    End If
    '                End If

    '                mTampQry += "Insert Into @TmpTable (Item, JobOrder, Qty) " & _
    '                               " Values (" & AgL.Chk_Text(Dgl1.Item(Col1Item, I).Tag) & ", " & _
    '                               " " & AgL.Chk_Text(Dgl1.Item(Col1JobOrder, I).Tag) & ", " & _
    '                               " " & Val(Dgl1.Item(Col1Qty, I).Tag) & ")"
    '                If Dgl1.Item(Col1Item_Uid, I).Value <> "" Then
    '                    Dim ErrMsgStr$ = ""
    '                    ErrMsgStr = FCheck_Item_UID(Dgl1.Item(Col1Item_Uid, I).Value, I, True)
    '                    If ErrMsgStr <> "" Then
    '                        MsgBox(ErrMsgStr) : passed = False : Exit Sub
    '                    End If
    '                End If
    '            End If
    '        Next
    '    End With

    '    mTampQry += " Select L.Item, L.JobOrder, Sum(L.Qty) As Qty, Max(I.Description) As ItemDesc " & _
    '                " From @TmpTable L " & _
    '                " LEFT JOIN Item I On L.Item = I.Code " & _
    '                " Group By Item, JobOrder "
    '    DtTemp = AgL.FillData(mTampQry, AgL.GCn).tables(0)

    '    If DtTemp.Rows.Count > 0 Then
    '        For I = 0 To DtTemp.Rows.Count - 1
    '            mQry = " SELECT Sum(L.Qty) - IFNull(Max(V1.ReceiveQty),0) " & _
    '                    " FROM JobOrderDetail L " & _
    '                    " LEFT JOIN ( " & _
    '                    " 	SELECT D.Item, D.JobOrder, Sum(D.Qty) AS ReceiveQty " & _
    '                    " 	FROM JobReceiveDetail D " & _
    '                    " 	WHERE D.Item = '" & DtTemp.Rows(I)("Item") & "' " & _
    '                    " 	AND D.JobOrder =  '" & DtTemp.Rows(I)("JobOrder") & "' " & _
    '                    " 	GROUP BY D.Item, D.JobOrder " & _
    '                    " ) AS V1 ON L.Item = V1.Item AND L.JobOrder = V1.JobOrder "
    '            If AgL.VNull(DtTemp.Rows(I)("Qty")) > AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar) Then
    '                MsgBox("Current Stock Of Item " & DtTemp.Rows(I)("ItemDesc") & " In Process " & DtTemp.Rows(I)("Process") & " Is Less Then " & AgL.VNull(DtTemp.Rows(I)("Qty")) & "", MsgBoxStyle.Information)
    '                passed = False : Exit Sub
    '            End If
    '        Next
    '    End If

    '    If StrMessage <> "" Then
    '        MsgBox(StrMessage)
    '        passed = False : Exit Sub
    '    End If
    'End Sub

    Private Sub FrmProductionOrder_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        Dim I As Integer = 0
        Dim DtTemp As DataTable = Nothing
        Dim StrMessage As String = ""

        passed = FCheckDuplicateRefNo()
        If AgL.RequiredField(TxtJobWorker, LblJobWorker.Text) Then passed = False : Exit Sub
        If AgCL.AgIsBlankGrid(Dgl1, Dgl1.Columns(Col1Item).Index) Then passed = False : Exit Sub

        Dim bOrdQty As Double = 0
        Dim bRecQty As Double = 0

        With Dgl1
            StrMessage = ""
            For I = 0 To .Rows.Count - 1
                If .Item(Col1Item, I).Value <> "" Then
                    If Val(.Item(Col1Qty, I).Value) = 0 Then
                        If StrMessage <> "" Then StrMessage += vbCrLf
                        StrMessage += "Qty Is 0 At Row No " & Dgl1.Item(ColSNo, I).Value & ""
                    End If

                    mQry = "SELECT IFNull(count(Item_UID),0) AS Cnt FROM JobIssRecUID " &
                            " WHERE DocId = " & AgL.Chk_Text(Dgl1.Item(Col1JobOrder, I).Tag) & " " &
                            " AND TSr = " & Val(Dgl1.Item(Col1JobOrderSr, I).Value) & " " &
                            " AND ISSREC ='I' " &
                            " GROUP BY DocId, TSr "
                    If AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar) > 0 And Dgl1.Item(Col1Item_Uid, I).Value = "" Then
                        If StrMessage <> "" Then StrMessage += vbCrLf
                        StrMessage += "Fill Item UID At Row No " & Dgl1.Item(ColSNo, I).Value & ""
                    End If

                    mQry = "SELECT Round(sum(L.Qty),4) AS OrdQty  FROM JobOrderDetail L  " &
                            " WHERE L.JobOrder = " & AgL.Chk_Text(Dgl1.Item(Col1JobOrder, I).Tag) & " " &
                            " AND L.JobOrderSr = " & Val(Dgl1.Item(Col1JobOrderSr, I).Value) & " " &
                            " AND L.DocId <> " & AgL.Chk_Text(mSearchCode) & " " &
                            " GROUP BY L.JobOrder, L.JobOrderSr "
                    bOrdQty = AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar)

                    mQry = "SELECT Round(sum(L.Qty) + IFNull(sum(L.LossQty),0),4) AS RecQty  FROM JobReceiveDetail L  " &
                            " WHERE L.JobOrder = " & AgL.Chk_Text(Dgl1.Item(Col1JobOrder, I).Tag) & " " &
                            " AND L.JobOrderSr = " & Val(Dgl1.Item(Col1JobOrderSr, I).Value) & " " &
                            " GROUP BY L.JobOrder, L.JobOrderSr "
                    bRecQty = AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar)

                    If bOrdQty - Val(.Item(Col1Qty, I).Value) < bRecQty Then
                        If StrMessage <> "" Then StrMessage += vbCrLf
                        StrMessage += "Cancel Qty Is Greater Than Balance Qty At Row No " & Dgl1.Item(ColSNo, I).Value & ""
                    End If

                    If Dgl1.Item(Col1Item_Uid, I).Value <> "" Then
                        Dim ErrMsgStr$ = ""
                        ErrMsgStr = FCheck_Item_UID(Dgl1.Item(Col1Item_Uid, I).Value, I, True)
                        If ErrMsgStr <> "" Then
                            MsgBox(ErrMsgStr) : passed = False : Exit Sub
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
            mQry = " SELECT COUNT(*) FROM JobOrder   " &
                    " WHERE ManualRefNo = '" & TxtManualRefNo.Text & "'   " &
                    " AND V_Type ='" & TxtV_Type.AgSelectedValue & "'  " &
                    " And Div_Code = '" & TxtDivision.AgSelectedValue & "' " &
                    " And Site_Code = '" & TxtSite_Code.AgSelectedValue & "'  " &
                    " And EntryStatus <> 'Discard' "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then TxtManualRefNo.Text = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "JobOrder", TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, AgTemplate.ClsMain.ManualRefType.Max) : MsgBox("Reference No. Already Exists New Reference No. Alloted : " & TxtManualRefNo.Text)
        Else
            mQry = " SELECT COUNT(*) FROM JobOrder  " &
                    " WHERE ManualRefNo = '" & TxtManualRefNo.Text & "'   " &
                    " AND V_Type ='" & TxtV_Type.AgSelectedValue & "'  " &
                    " And Div_Code = '" & TxtDivision.AgSelectedValue & "' " &
                    " And Site_Code = '" & TxtSite_Code.AgSelectedValue & "' " &
                    " And EntryStatus ='" & AgTemplate.ClsMain.LogStatus.LogOpen & "' " &
                    " AND DocID <>'" & mSearchCode & "' " &
                    " And EntryStatus <> 'Discard' "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then FCheckDuplicateRefNo = False : MsgBox("Reference No. Already Exists") : TxtManualRefNo.Focus()
        End If
    End Function

    Private Sub FrmProductionOrder_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
        Dgl1.RowCount = 1 : Dgl1.Rows.Clear()

        LblTotalMeasure.Text = 0 : LblTotalQty.Text = 0 : LblTotalAmount.Text = 0

        Dgl5.Item(Col5Amount, Row5GrossAmount).Value = 0
        Dgl5.Item(Col5Amount, Row5RoundOff).Value = 0
        Dgl5.Item(Col5Amount, Row5NetAmount).Value = 0
    End Sub

    Private Sub Txt_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtV_Type.Validating, TxtManualRefNo.Validating, TxtV_Date.Validating, TxtJobWorker.Validating
        Dim DrTemp As DataRow() = Nothing
        Dim DtTemp As DataTable = Nothing
        Dim I As Integer = 0
        Try
            Select Case sender.name
                Case TxtV_Date.Name
                    If Topctrl1.Mode = "Add" Then
                        TxtManualRefNo.Text = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "JobOrder", TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, AgTemplate.ClsMain.ManualRefType.Max)
                    End If

                Case TxtV_Type.Name
                    TxtTermsAndConditions.Text = AgTemplate.ClsMain.FRetTermsCondition(TxtV_Type.AgSelectedValue)
                    'If Topctrl1.Mode = "Add" Then
                    '    TxtManualRefNo.Text = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "JobOrder", TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, AgTemplate.ClsMain.ManualRefType.Max)
                    'End If

                    'TxtProcess.Enabled = False
                    'If CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Process")), Boolean) Then
                    '    If InStr(",", AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Process"))) <= 0 Then
                    '        mQry = "Select NCat, Description from Process Where NCat= '" & Replace(AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Process")), "|", "") & "'  "
                    '        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
                    '        If DtTemp.Rows.Count > 0 Then
                    '            TxtProcess.Tag = AgL.XNull(DtTemp.Rows(0)("NCat"))
                    '            TxtProcess.Text = AgL.XNull(DtTemp.Rows(0)("Description"))
                    '            TxtProcess.Enabled = False
                    '        End If
                    '    Else
                    '        TxtProcess.Enabled = True
                    '    End If
                    'End If


                    If TxtV_Type.Tag <> "" Then
                        DtJobEnviro = AgL.FillData("SELECT * FROM JobEnviro WHERE V_Type ='" & TxtV_Type.Tag & "' AND Site_Code='" & AgL.PubSiteCode & "' AND Div_Code='" & AgL.PubDivCode & "'", AgL.GCn).Tables(0)
                        If DtJobEnviro.Rows.Count = 0 Then
                            MsgBox("Job Enivro Settings are not defined. Can't Continue!")
                            Topctrl1.FButtonClick(14, True)
                            Exit Sub
                        End If
                    End If

                    'If AgL.XNull(DtV_TypeSettings.Rows(0)("Structure")) = "" Then
                    '    TxtStructure.AgSelectedValue = AgStructure.ClsMain.FGetStructureFromNCat(LblV_Type.Tag, AgL.GcnRead)
                    '    AgCalcGrid1.AgStructure = TxtStructure.AgSelectedValue
                    'Else
                    '    TxtStructure.Tag = AgL.XNull(DtV_TypeSettings.Rows(0)("Structure"))
                    '    AgCalcGrid1.AgStructure = AgL.XNull(DtV_TypeSettings.Rows(0)("Structure"))
                    'End If

                    IniGrid()
                    TxtManualRefNo.Text = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "JobOrder", TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, AgTemplate.ClsMain.ManualRefType.Max)
                    If Dgl1.AgHelpDataSet(Col1Item) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1Item).Dispose() : Dgl1.AgHelpDataSet(Col1Item) = Nothing
                    If TxtJobWorker.AgHelpDataSet IsNot Nothing Then TxtJobWorker.AgHelpDataSet.Dispose() : TxtJobWorker.AgHelpDataSet = Nothing
                    FAsignProcess()

                Case TxtManualRefNo.Name
                    e.Cancel = Not FCheckDuplicateRefNo()
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
    End Sub

    Private Sub Validating_Item(ByVal Code As String, ByVal mRow As Integer)
        Dim DrTemp As DataRow() = Nothing
        Dim DtTemp As DataTable = Nothing
        Dim sqlConn As SQLiteConnection = Nothing
        Dim sqlDA As SQLiteDataAdapter = Nothing

        sqlConn = New SQLiteConnection
        sqlConn.ConnectionString = AgL.Gcn_ConnectionString
        sqlConn.Open()

        Try
            If Dgl1.Item(Col1Item, mRow).Value.ToString.Trim = "" Or Dgl1.AgSelectedValue(Col1Item, mRow).ToString.Trim = "" Then
                Dgl1.Item(Col1Qty, mRow).Value = 0
                Dgl1.Item(Col1Unit, mRow).Value = ""
                Dgl1.Item(Col1MeasurePerPcs, mRow).Value = 0
                Dgl1.Item(Col1MeasureUnit, mRow).Value = ""
                Dgl1.Item(Col1JobOrder, mRow).Value = ""
                Dgl1.Item(Col1LotNo, mRow).Value = ""
                Dgl1.Item(Col1JobOrder, mRow).Tag = ""
                Dgl1.Item(Col1JobOrderSr, mRow).Value = 0
                Dgl1.Item(Col1FromProcess, mRow).Tag = ""
                Dgl1.Item(Col1FromProcess, mRow).Value = ""
            Else
                If Dgl1.AgDataRow IsNot Nothing Then
                    Dgl1.Item(Col1Unit, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("Unit").Value)
                    Dgl1.Item(Col1QtyDecimalPlaces, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("QtyDecimalPlaces").Value)
                    Dgl1.Item(Col1MeasurePerPcs, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("MeasurePerPcs").Value)
                    Dgl1.Item(Col1MeasureUnit, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("MeasureUnit").Value)
                    Dgl1.Item(Col1MeasureDecimalPlaces, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("MeasureDecimalPlaces").Value)
                    Dgl1.Item(Col1Qty, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("Bal.Qty").Value)
                    Dgl1.Item(Col1Rate, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("Rate").Value)
                    Dgl1.Item(Col1JobOrder, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("JobOrderNo").Value)
                    Dgl1.Item(Col1JobOrder, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("JobOrder").Value)
                    Dgl1.Item(Col1JobOrderSr, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("JobOrderSr").Value)
                    Dgl1.Item(Col1ProdOrder, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("ProdOrderNo").Value)
                    Dgl1.Item(Col1ProdOrder, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("ProdOrder").Value)
                    Dgl1.Item(Col1ProdOrderSr, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("ProdOrderSr").Value)
                    Dgl1.Item(Col1LotNo, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("LotNo").Value)
                    Dgl1.Item(Col1FromProcess, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("FromProcess").Value)
                    Dgl1.Item(Col1FromProcess, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("FromProcessDesc").Value)

                    Dgl1.Item(Col1Dimension1, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("Dimension1").Value)
                    Dgl1.Item(Col1Dimension1, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("" & AgTemplate.ClsMain.FGetDimension1Caption() & "").Value)
                    Dgl1.Item(Col1Dimension2, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("Dimension2").Value)
                    Dgl1.Item(Col1Dimension2, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("" & AgTemplate.ClsMain.FGetDimension2Caption() & "").Value)

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
                    Validating_Item(Dgl1.AgSelectedValue(Col1Item, mRowIndex), mRowIndex)

                Case Col1Item_Uid
                    Validating_Item_Uid(Dgl1.Item(Col1Item_Uid, mRowIndex).Value, mRowIndex)
            End Select
            Call Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FPostInStockProcess(ByVal SearchCode As String, ByVal Conn As SQLiteConnection, ByVal Cmd As SQLiteCommand)
        Dim MaxSr As Integer = 0

        If CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsPostedInStock")), Boolean) Then
            mQry = "Delete From Stock Where DocId = '" & mSearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

            'mQry = "INSERT INTO Stock(DocID, Sr, V_Type, V_Prefix, V_Date, V_No, RecID, Div_Code, Site_Code, " & _
            '         " SubCode, Item, Godown, Qty_Rec, Unit, MeasurePerPcs, Measure_Rec, MeasureUnit, " & _
            '         " Remarks, Process, CostCenter) " & _
            '         " Select L.DocID, row_number() OVER (ORDER BY L.Item),Max(H.V_Type), " & _
            '         " Max(H.V_Prefix), Max(H.V_Date), Max(H.V_No), Max(H.ManualRefNo), Max(H.Div_Code), Max(H.Site_Code),   " & _
            '         " Max(H.Jobworker), L.Item, Max(H.Godown), Sum(ABs(L.Qty)), Max(L.Unit), Max(L.MeasurePerPcs), " & _
            '         " Sum(Abs(L.TotalMeasure)), Max(L.MeasureUnit),   " & _
            '         " Max(L.Remark), H.Process, Max(J.CostCenter) " & _
            '         " From (Select * From JobOrder Where DocId = '" & mSearchCode & "') H   " & _
            '         " LEFT JOIN JobOrderDetail L On H.DocId = L.DocId   " & _
            '         " LEFT JOIN JobOrder J On L.JobOrder = J.DocId " & _
            '         " Group By L.DocId, L.Item, L.ProdOrder, H.Process "
            mQry = "INSERT INTO Stock(DocID, Sr, V_Type, V_Prefix, V_Date, V_No, RecID, Div_Code, Site_Code, " &
                     " SubCode, Item, Godown, Qty_Rec, Unit, MeasurePerPcs, Measure_Rec, MeasureUnit, " &
                     " Remarks, Process, CostCenter) " &
                     " Select L.DocID, row_number() OVER (ORDER BY L.Item),Max(H.V_Type), " &
                     " Max(H.V_Prefix), Max(H.V_Date), Max(H.V_No), Max(H.ManualRefNo), Max(H.Div_Code), Max(H.Site_Code),   " &
                     " Max(H.Jobworker), L.Item, Max(H.Godown), Sum(ABs(L.Qty)), Max(L.Unit), Max(L.MeasurePerPcs), " &
                     " Sum(Abs(L.TotalMeasure)), Max(L.MeasureUnit),   " &
                     " Max(L.Remark), L.FromProcess, Max(J.CostCenter) " &
                     " From (Select * From JobOrder Where DocId = '" & mSearchCode & "') H   " &
                     " LEFT JOIN JobOrderDetail L On H.DocId = L.DocId   " &
                     " LEFT JOIN JobOrder J On L.JobOrder = J.DocId " &
                     " Group By L.DocId, L.Item, L.ProdOrder, L.FromProcess "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If

        If CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsPostedInStockProcess")), Boolean) Then
            mQry = "Delete From StockProcess Where DocId = '" & mInternalCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

            mQry = "INSERT INTO StockProcess(DocID, Sr, V_Type, V_Prefix, V_Date, V_No, RecID, Div_Code, Site_Code, " &
                    " SubCode, Item, Godown, Qty_Iss, Unit, MeasurePerPcs, Measure_Iss, MeasureUnit, " &
                    " Remarks, Process, CostCenter) " &
                    " Select L.DocID, row_number() OVER (ORDER BY L.Item),Max(H.V_Type), " &
                    " Max(H.V_Prefix), Max(H.V_Date), Max(H.V_No), Max(H.ManualRefNo), Max(H.Div_Code), Max(H.Site_Code),   " &
                    " Max(H.Jobworker), L.Item, Max(H.Godown), Sum(Abs(L.Qty)), Max(L.Unit), Max(L.MeasurePerPcs), " &
                    " Sum(Abs(L.TotalMeasure)), Max(L.MeasureUnit),   " &
                    " Max(Remark), H.Process, Max(J.CostCenter) " &
                    " From (Select * From JobOrder Where DocId = '" & mSearchCode & "') H   " &
                    " LEFT JOIN JobOrderDetail L On H.DocId = L.DocId   " &
                    " LEFT JOIN JobOrder J On L.JobOrder = J.DocId " &
                    " Group By L.DocId, L.Item, L.ProdOrder, H.Process "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If

        If CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsPostConsumption")), Boolean) Then
            mQry = "Delete From JobOrderBOM Where DocId = '" & mSearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

            mQry = "INSERT INTO JobOrderBOM(DocId, TSr, Sr, JobOrder, JobOrderSr, JobOrderBomSr, " &
                    " Item, Qty, Unit, ConsumptionPerMeasure, MeasurePerPcs, TotalMeasure, MeasureUnit) " &
                    " SELECT L.DocId, L.Sr AS TSr, row_NUMBER() OVER (ORDER BY L.Sr) AS Sr, " &
                    " J.DocId As JobOrder, J.TSr As JobOrderSr, J.Sr As JobOrderBomSr, " &
                    " J.Item, J.ConsumptionPerMeasure * L.Qty AS BomQty, J.Unit, " &
                    " J.ConsumptionPerMeasure, J.MeasurePerPcs, " &
                    " J.MeasurePerPcs * J.ConsumptionPerMeasure * L.Qty As TotalMeasure, J.MeasureUnit  " &
                    " FROM (Select * From JobOrderDetail Where DocId = '" & mSearchCode & "') As L  " &
                    " LEFT JOIN JobOrderBom J On L.JobOrder = J.DocId And L.JobOrderSr = J.TSr " &
                    " Where J.Item Is Not Null "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If

    End Sub

    Private Sub TempJobOrder_BaseEvent_Topctrl_tbAdd() Handles Me.BaseEvent_Topctrl_tbAdd
        TxtManualRefNo.Text = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "JobOrder", TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, AgTemplate.ClsMain.ManualRefType.Max)
        TxtTermsAndConditions.Text = AgTemplate.ClsMain.FRetTermsCondition(TxtV_Type.AgSelectedValue)
        TxtOrderBy.Tag = mLastOrderBy
        TxtOrderBy.Text = AgL.Dman_Execute(" SELECT DispName FROM SubGroup WHERE SubCode = '" & mLastOrderBy & "'", AgL.GCn).ExecuteScalar
        IniGrid()
        FAsignProcess()
    End Sub

    Private Sub ProcFillJobValues()
        Dim DtTemp As DataTable = Nothing
        Try
            mQry = " SELECT H.DefaultBillingType " &
                    " FROM Process H   " &
                    " WHERE H.NCat = '" & TxtProcess.Tag & "' "
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
            With DtTemp
                If .Rows.Count > 0 Then
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
            If Dgl1.AgHelpDataSet(Col1Item) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1Item).Dispose() : Dgl1.AgHelpDataSet(Col1Item) = Nothing
            If TxtGodown.AgHelpDataSet IsNot Nothing Then TxtGodown.AgHelpDataSet.Dispose() : TxtGodown.AgHelpDataSet = Nothing
            If TxtJobWorker.AgHelpDataSet IsNot Nothing Then TxtJobWorker.AgHelpDataSet.Dispose() : TxtJobWorker.AgHelpDataSet = Nothing
            If TxtOrderBy.AgHelpDataSet IsNot Nothing Then TxtOrderBy.AgHelpDataSet.Dispose() : TxtOrderBy.AgHelpDataSet = Nothing
        Catch ex As Exception
        End Try
    End Sub

    Private Sub TxtOrderBy_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtOrderBy.KeyDown, TxtGodown.KeyDown, TxtBillingType.KeyDown, TxtJobWorker.KeyDown, TxtProcess.KeyDown
        Try
            Select Case sender.name
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
                                    " And H.Div_Code = '" & TxtDivision.AgSelectedValue & "' " &
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

                Case TxtJobWorker.Name
                    If e.KeyCode <> Keys.Enter Then
                        If sender.AgHelpDataSet Is Nothing Then
                            mQry = " SELECT Sg.SubCode AS Code, Sg.Name AS Jobworker, H.Process, " &
                               " IFNull(Sg.IsDeleted,0) AS IsDeleted,  SG.Div_Code, " &
                               " IFNull(Sg.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') As Status " &
                               " FROM SubGroup Sg  " &
                               " LEFT JOIN JobworkerProcess H   On Sg.SubCode = H.SubCode  " &
                               " Where IFNull(Sg.IsDeleted,0) = 0 " &
                               " And Sg.Status = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " &
                               " And CharIndex('|' + '" & TxtDivision.Tag & "' + '|', IFNull(Sg.DivisionList,'|' + '" & TxtDivision.Tag & "' + '|')) > 0 " &
                               " And H.Process = '" & TxtProcess.Tag & "' "
                            sender.AgHelpDataSet(4, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
                        End If
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
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Item
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1Item) Is Nothing Then
                            FCreateHelpItem()
                        End If
                    End If

                Case Col1FromProcess
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1FromProcess) Is Nothing Then
                            mQry = " SELECT NCat AS Code, Description  FROM Process "
                            Dgl1.AgHelpDataSet(Col1FromProcess) = AgL.FillData(mQry, AgL.GCn)
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

        mQry = " SELECT Max(L.Item) As Code, Max(I.Description) As Description,  Max(L.LotNo) As LotNo, " &
                " Max(D1.Description) As " & AgTemplate.ClsMain.FGetDimension1Caption() & ", " &
                " Max(D2.Description) As " & AgTemplate.ClsMain.FGetDimension2Caption() & ", " &
                " Max(H.V_Type) + '-' +  Max(H.ManualRefNo) As JobOrderNo,   " &
                " Max(H.V_Date) as JobOrderDate,  " &
                " ROUND(Sum(L.Qty) - IFNull(Max(Cd.Qty), 0),4) as [Bal.Qty],   " &
                " Max(I.Unit) as Unit,  Max(L.FromProcess) As FromProcess, Max(P.Description) As FromProcessDesc,  " &
                " Sum(L.TotalMeasure) - IFNull(Sum(Cd.TotalMeasure), 0) as [Bal.Measure],   " &
                " Max(I.MeasureUnit) MeasureUnit, Max(L.Rate) as Rate,   " &
                " Max(I.SalesTaxPostingGroup) SalesTaxPostingGroup, L.JobOrder,   " &
                " Max(L.MeasurePerPcs) as MeasurePerPcs, " &
                " Max(L.ProdOrder) As ProdOrder, Max(Po.V_Type)+'-'+Max(Po.ManualRefNo) As ProdOrderNo, Max(L.ProdOrderSr) As ProdOrderSr, " &
                " L.JobOrderSr, Max(U.DecimalPlaces) as QtyDecimalPlaces,  Max(L.Dimension1) As Dimension1, Max(L.Dimension2) As Dimension2, " &
                " Max(U1.DecimalPlaces) as MeasureDecimalPlaces   " &
                " FROM (  " &
                "     SELECT DocID, V_Type, ManualRefNo, V_Date " &
                "     FROM JobOrder    " &
                "     WHERE Jobworker ='" & TxtJobWorker.Tag & "'   " &
                "     And Process = '" & TxtProcess.Tag & "'   " &
                "     And Div_Code = '" & TxtDivision.Tag & "'   " &
                "     AND Site_Code = '" & TxtSite_Code.Tag & "'   " &
                "     AND V_Date <= '" & TxtV_Date.Text & "'   " &
                "     ) H   " &
                " LEFT JOIN JobOrderDetail L  ON H.DocID = L.JobOrder  " &
                " LEFT JOIN ProdOrder Po  ON L.ProdOrder = Po.DocId " &
                " Left Join Item I  On L.Item  = I.Code   " &
                " Left Join Process P  On L.FromProcess  = P.NCat   " &
                " LEFT JOIN Voucher_Type Vt  ON H.V_Type = Vt.V_Type    " &
                " Left Join Dimension1 D1 On L.Dimension1 = D1.Code " &
                " Left Join Dimension2 D2 On L.Dimension2 = D2.Code " &
                " Left Join (   " &
                "     SELECT L.JobOrder, L.JobOrderSr, Sum(L.Qty) + IFNull(Sum(L.LossQty),0) AS Qty, Sum(L.TotalMeasure) as TotalMeasure " &
                " 	  FROM JobReceiveDetail L     " &
                " 	  GROUP BY L.JobOrder, L.JobOrderSr   " &
                " 	) AS CD ON L.JobOrder = CD.JobOrder AND L.JobOrderSr = CD.JobOrderSr   " &
                " LEFT JOIN Unit U On L.Unit = U.Code   " &
                " LEFT JOIN Unit U1 On L.MeasureUnit = U1.Code   " &
                " WHERE L.DocId <> '" & mSearchCode & "'" & strCond &
                " GROUP BY L.JobOrder, L.JobOrderSr  " &
                " Having ROUND(Sum(L.Qty) - IFNull(Max(Cd.Qty), 0),4) > 0 " &
                " Order By JobOrderDate  "
        Dgl1.AgHelpDataSet(Col1Item, 15) = AgL.FillData(mQry, AgL.GCn)
    End Sub

    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub

    Private Sub Validating_Item_Uid(ByVal Item_Uid As String, ByVal mRow As Integer)
        Dim DsTemp As DataSet = Nothing
        Dim DtTemp As DataTable = Nothing
        Dim ErrMsgStr$ = ""

        Try
            ErrMsgStr = FCheck_Item_UID(Dgl1.Item(Col1Item_Uid, mRow).Value, mRow, False)
            If ErrMsgStr <> "" Then MsgBox(ErrMsgStr) : Exit Sub

            'mQry = "  SELECT Iu.Code As Item_UidCode, Iu.Item_UID , Iu.Item AS ItemCode, I.Description AS Item, " & _
            '        " I.Unit, Iu.ProdOrder, Po.ManualRefNo As ProdOrderNo, " & _
            '        " I.MeasureUnit, U.DecimalPlaces as QtyDecimalPlaces, MU.DecimalPlaces as MeasureDecimalPlaces, " & _
            '        " Iu.ProdOrder, Po.ManualRefNo As ProdOrderNo, " & _
            '        " I.Finishing_Measure As MeasurePerPcs " & _
            '        " FROM Item_UID Iu  " & _
            '        " LEFT JOIN Item I ON I.Code = Iu.Item   " & _
            '        " LEFT JOIN ProdOrder PO ON PO.DocID = Iu.ProdOrder " & _
            '        " Left Join Unit U  On I.Unit = U.Code " & _
            '        " Left Join Unit MU  On I.MeasureUnit = MU.Code " & _
            '        " WHERE Iu.Item_UID = '" & Item_Uid & "' "

            mQry = "  SELECT Iu.Code As Item_UidCode, Iu.Item_UID , Iu.Item AS ItemCode, I.Description AS Item, " &
                    " I.Unit, I.MeasureUnit, U.DecimalPlaces as QtyDecimalPlaces, MU.DecimalPlaces as MeasureDecimalPlaces, " &
                    " I.Finishing_Measure As MeasurePerPcs , JO.DocId As JobOrder, JOD.Sr AS JobOrderSr, Jo.V_Type +'-'+JO.ManualRefNo AS OrderNo, JOD.Rate, JOD.FromProcess, P.Description AS FromProcessDesc " &
                    " FROM Item_UID Iu " &
                    " LEFT JOIN JobIssRecUID JIR ON JIR.Item_UID = Iu.Code AND IFNull(JIR.JobRecDocID,'') ='' " &
                    " LEFT JOIN JobOrderDetail JOD ON JOD.DocId = JIR.DocID  AND JOD.Sr=JIR.TSr  " &
                    " LEFT JOIN JobOrder JO ON JO.DocID = JOD.DocID  " &
                    " LEFT JOIN Item I ON I.Code = Iu.Item  " &
                    " Left Join Unit U  On I.Unit = U.Code " &
                    " Left Join Unit MU  On I.MeasureUnit = MU.Code  " &
                    " LEFT JOIN Process P ON P.NCat = JOD.FromProcess  " &
                    " WHERE Iu.Item_UID = '" & Item_Uid & "' "

            DsTemp = AgL.FillData(mQry, AgL.GCn)
            With DsTemp.Tables(0)
                If .Rows.Count > 0 Then
                    Dgl1.Item(Col1Item_Uid, mRow).Tag = AgL.XNull(.Rows(0)("Item_UidCode"))

                    Dgl1.Item(Col1Item, mRow).Tag = AgL.XNull(.Rows(0)("ItemCode"))
                    Dgl1.Item(Col1Item, mRow).Value = AgL.XNull(.Rows(0)("Item"))

                    Dgl1.Item(Col1JobOrder, mRow).Tag = AgL.XNull(.Rows(0)("JobOrder"))
                    Dgl1.Item(Col1JobOrder, mRow).Value = AgL.XNull(.Rows(0)("OrderNo"))
                    Dgl1.Item(Col1JobOrderSr, mRow).Value = AgL.VNull(.Rows(0)("JobOrderSR"))
                    Dgl1.Item(Col1FromProcess, mRow).Tag = AgL.XNull(.Rows(0)("FromProcess"))
                    Dgl1.Item(Col1FromProcess, mRow).Value = AgL.XNull(.Rows(0)("FromProcessDesc"))

                    Dgl1.Item(Col1Qty, mRow).Value = 1
                    Dgl1.Item(Col1Rate, mRow).Value = AgL.VNull(.Rows(0)("Rate"))
                    Dgl1.Item(Col1Unit, mRow).Value = AgL.XNull(.Rows(0)("Unit"))
                    Dgl1.Item(Col1QtyDecimalPlaces, mRow).Value = AgL.VNull(.Rows(0)("QtyDecimalPlaces"))
                    Dgl1.Item(Col1MeasurePerPcs, mRow).Value = AgL.VNull(.Rows(0)("MeasurePerPcs"))
                    Dgl1.Item(Col1MeasureUnit, mRow).Value = AgL.XNull(.Rows(0)("MeasureUnit"))
                    Dgl1.Item(Col1MeasureDecimalPlaces, mRow).Value = AgL.VNull(.Rows(0)("MeasureDecimalPlaces"))
                Else
                    MsgBox("Invalid Item UID !")
                    Dgl1.Item(Col1Item_Uid, mRow).Value = ""
                End If
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Function FCheck_Item_UID(ByVal Item_UID As String, ByVal mRowIndex As Integer, ByVal SaveTimeValidation As Boolean) As String
        Dim Item_UidCode$ = "", ErrMsgStr$ = ""
        Dim DtTemp As DataTable = Nothing
        Dim bIssueCnt As Integer = 0

        mQry = " SELECT Code FROM Item_UID  WHERE Item_UID = '" & Item_UID & "'"
        Dgl1.Item(Col1Item_Uid, mRowIndex).Tag = AgL.XNull(AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar)
        If Dgl1.Item(Col1Item_Uid, mRowIndex).Tag = "" Then
            If SaveTimeValidation = False Then
                Dgl1.Item(Col1Item_Uid, mRowIndex).Value = ""
                Dgl1.Item(Col1Item_Uid, mRowIndex).Tag = ""
            End If

            FCheck_Item_UID = "Carpet Id Is Not Valid."
            Exit Function
        Else
            FCheck_Item_UID = ""
        End If

        'mQry = " Select I.Div_Code From Item_Uid Iu LEFT JOIN Item I ON Iu.Item = I.Code Where Iu.Code = '" & Dgl1.Item(Col1Item_Uid, mRowIndex).Tag & "'"
        'mQry = " SELECT JO.Div_Code FROM JobIssRecUID  L " & _
        '        " LEFT JOIN JobOrder JO ON JO.DocID = L.DocID  " & _
        '        " WHERE L.Item_UID='" & Dgl1.Item(Col1Item_Uid, mRowIndex).Tag & "' AND L.DocID = 'K1 SWASH 2013     168'"

        'If AgL.XNull(AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar) <> AgL.PubDivCode Then
        '    If SaveTimeValidation = False Then
        '        Dgl1.Item(Col1Item_Uid, mRowIndex).Value = ""
        '        Dgl1.Item(Col1Item_Uid, mRowIndex).Tag = ""
        '    End If

        '    FCheck_Item_UID = "Carpet Id " & Item_UID & " Does Not Belong To " & AgL.PubDivName & "."
        '    Exit Function
        'Else
        '    FCheck_Item_UID = ""
        'End If

        mQry = " Select RecDocID From Item_Uid  Where Code = '" & Dgl1.Item(Col1Item_Uid, mRowIndex).Tag & "' "
        If AgL.XNull(AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar) = "" Then
            If SaveTimeValidation = False Then
                Dgl1.Item(Col1Item_Uid, mRowIndex).Value = ""
                Dgl1.Item(Col1Item_Uid, mRowIndex).Tag = ""
            End If

            FCheck_Item_UID = "Carpet Id " & Item_UID & " Is Not Received From Weaving Process."
            Exit Function
        Else
            FCheck_Item_UID = ""
        End If

        mQry = " SELECT Sg.DispName, H.ManualRefNo, H.V_Date, Vc.NCatDescription AS ProcessDesc " &
                " FROM JobIssRecUID L  " &
                " LEFT JOIN JobOrder H  ON L.DocID = H.DocID  " &
                " LEFT JOIN SubGroup Sg   ON H.Jobworker = Sg.SubCode " &
                " LEFT JOIN VoucherCat  Vc  ON H.Process =  Vc.NCat " &
                " WHERE L.Item_UID = '" & Dgl1.Item(Col1Item_Uid, mRowIndex).Tag & "'  " &
                " AND L.ISSREC = 'R' " &
                " AND L.Process = '" & TxtProcess.Tag & "' " &
                " AND L.JobRecDocID = '" & Dgl1.Item(Col1JobOrder, mRowIndex).Tag & "' " &
                " And L.DocId <> '" & mSearchCode & "'" &
                " ORDER BY H.EntryDate DESC	 Limit 1"
        DtTemp = AgL.FillData(mQry, AgL.GcnRead).Tables(0)
        If DtTemp.Rows.Count > 0 Then
            If SaveTimeValidation = False Then
                Dgl1.Item(Col1Item_Uid, mRowIndex).Value = ""
                Dgl1.Item(Col1Item_Uid, mRowIndex).Tag = ""
            End If
            FCheck_Item_UID = "Carpet Id " & Item_UID & " Is Already Received From " & AgL.XNull(DtTemp.Rows(0)("DispName")) & " From Process  " & AgL.XNull(DtTemp.Rows(0)("ProcessDesc")) & " On Date " & AgL.XNull(DtTemp.Rows(0)("V_Date")) & " Against Ref No.  " & AgL.XNull(DtTemp.Rows(0)("ManualRefNo")) & " "
            Exit Function
        Else
            FCheck_Item_UID = ""
        End If

        mQry = " SELECT L.Process " &
                " FROM (Select * From JobIssRecUID  Where Item_UID = '" & Dgl1.Item(Col1Item_Uid, mRowIndex).Tag & "' And ISSREC = 'I' And Process='" & TxtProcess.Tag & "') L " &
                " Left Join JobIssRecUID L1  On L.DocID = L1.JobRecDocId And L.Item_UID = L1.Item_UID " &
                " WHERE (L1.DocID Is Null Or L1.DocID = '" & mSearchCode & "')  "
        If AgL.FillData(mQry, AgL.GCn).Tables(0).rows.Count <= 0 Then
            If SaveTimeValidation = False Then
                Dgl1.Item(Col1Item_Uid, mRowIndex).Value = ""
                Dgl1.Item(Col1Item_Uid, mRowIndex).Tag = ""
            End If
            FCheck_Item_UID = "Carpet Id " & Item_UID & " Is Not In This Process."
            Exit Function
        Else
            FCheck_Item_UID = ""
        End If

        mQry = " SELECT H.Jobworker " &
                " FROM (Select * From JobIssRecUID  Where Item_UID = '" & Dgl1.Item(Col1Item_Uid, mRowIndex).Tag & "' And ISSREC = 'I' And Process='" & TxtProcess.Tag & "') L  " &
                " LEFT JOIN JobOrder H ON L.DocID = H.DocID " &
                " Left Join JobIssRecUID L1  On L.DocID = L1.JobRecDocId And L.Item_UID = L1.Item_UID " &
                " WHERE (L1.DocID Is Null Or L1.DocID = '" & mSearchCode & "') "
        If AgL.XNull(AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar) <> TxtJobWorker.Tag Then
            If SaveTimeValidation = False Then
                Dgl1.Item(Col1Item_Uid, mRowIndex).Value = ""
                Dgl1.Item(Col1Item_Uid, mRowIndex).Tag = ""
            End If
            FCheck_Item_UID = "Carpet Id " & Item_UID & " Is Not Issued To this Job Jober."
            Exit Function
        Else
            FCheck_Item_UID = ""
        End If
    End Function

    Private Sub RbtAllItems_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Dgl1.AgHelpDataSet(Col1Item) = Nothing
    End Sub

    Private Sub TxtProcess_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtProcess.Validating
        Dgl1.AgHelpDataSet(Col1Item) = Nothing
        Call ProcFillJobValues()
    End Sub

    Private Sub FPostInJobIssRecUID(ByVal SearchCode As String, ByVal Conn As SQLiteConnection, ByVal Cmd As SQLiteCommand)
        Dim I As Integer = 0, bSr As Integer = 0

        mQry = "Delete from JobIssRecUID Where DocId ='" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " INSERT INTO JobIssRecUID(DocID, TSr, Sr, IssRec, Process, JobRecDocID, Item, Item_UID, " &
                 " Godown, Site_Code, V_Date, V_Type, SubCode, Div_Code, RecId, EntryDate) " &
                 " Select L.DocId, L.Sr As TSr, L.Sr, 'R', " &
                 " H.Process, L.JobOrder, L.Item, L.Item_Uid, " &
                 " H.Godown, H.Site_Code, H.V_Date, H.V_Type, H.Jobworker, H.Div_Code, H.ManualRefNo, H.EntryDate " &
                 " From (Select * From JobOrderDetail  Where DocId = '" & mSearchCode & "' And Item_Uid Is Not Null) As L " &
                 " LEFT JOIN JobOrder H  On L.DocId = H.DocId "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " Update JobIssRecUID " &
                " SET JobRecDocID = " & AgL.Chk_Text(mInternalCode) & " " &
                " WHERE JobRecDocID Is Null " &
                " And Item_UID In (Select Item_UID From JobOrderDetail  Where DocId = '" & mSearchCode & "' And Item_Uid Is Not Null) " &
                " And Process = '" & TxtProcess.Tag & "' " &
                " AND ISSREC = 'I'" &
                " And Site_Code = '" & AgL.PubSiteCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    End Sub

    Private Sub BtnFillSaleChallan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnFillJobOrder.Click
        Try
            If Topctrl1.Mode = "Browse" Then Exit Sub
            Dim StrTicked As String

            If RbtCancelForOrderItem.Checked Then
                StrTicked = FHPGD_PendingJobOrderItems()
            Else
                StrTicked = FHPGD_PendingJobOrder()
            End If

            If StrTicked <> "" Then
                FFillItemsForPendingJobOrders(StrTicked)
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

        mQry = " SELECT 'o' As Tick, VMain.JobOrder + Convert(nVarChar, VMain.JobOrderSr) As JobOrderDocIdSr, " &
                " Max(VMain.JobOrderNo) AS JobOrderNo,  " &
                " Max(VMain.JobOrderDate) AS JobOrderDate, Max(VMain.ItemDesc) As ItemDesc, SUM(VMain.Qty) AS BalQty " &
                " FROM ( " & FRetFillItemWiseQry("And JobWorker = '" & TxtJobWorker.Tag & "' And Process = '" & TxtProcess.Tag & "' And Site_Code = '" & TxtSite_Code.Tag & "' And Div_Code = '" & TxtDivision.Tag & "' And V_Date <= '" & TxtV_Date.Text & "'", "") & " ) As VMain " &
                " GROUP BY VMain.JobOrder, VMain.JobOrderSr " &
                " Order By JobOrderDate "

        FRH_Multiple = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(AgL.FillData(mQry, AgL.GCn).TABLES(0)), "", 500, 750, , , False)
        FRH_Multiple.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple.FFormatColumn(1, , 0, , False)
        FRH_Multiple.FFormatColumn(2, "Job Order No.", 180, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(3, "Job Order Date", 180, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(4, "Item", 150, DataGridViewContentAlignment.MiddleLeft)

        FRH_Multiple.StartPosition = FormStartPosition.CenterScreen
        FRH_Multiple.ShowDialog()

        If FRH_Multiple.BytBtnValue = 0 Then
            StrRtn = FRH_Multiple.FFetchData(1, "'", "'", ",", True)
        End If
        FHPGD_PendingJobOrderItems = StrRtn

        FRH_Multiple = Nothing
    End Function

    Private Function FHPGD_PendingJobOrder() As String
        Dim FRH_Multiple As DMHelpGrid.FrmHelpGrid_Multi
        Dim StrRtn As String = ""

        mQry = " SELECT 'o' As Tick, VMain.JobOrder, Max(VMain.JobOrderNo) AS JobOrderNo,  " &
                " Max(VMain.JobOrderDate) AS JobOrderDate , SUM(VMain.Qty) AS BalQty   " &
                " FROM ( " & FRetFillItemWiseQry("And JobWorker = '" & TxtJobWorker.Tag & "' And Process = '" & TxtProcess.Tag & "' And Site_Code = '" & TxtSite_Code.Tag & "' And Div_Code = '" & TxtDivision.Tag & "' And V_Date <= '" & TxtV_Date.Text & "'", "") & " ) As VMain " &
                " GROUP BY VMain.JobOrder " &
                " Order By JobOrderDate "

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
        FHPGD_PendingJobOrder = StrRtn

        FRH_Multiple = Nothing
    End Function

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


        FRetFillItemWiseQry = " SELECT Max(H.V_Type) + '-' +  Max(H.ManualRefNo) AS JobOrderNo, " &
                  " Max(H.V_Date) As JobOrderDate, L.JobOrder, L.JobOrderSr, " &
                  " Max(L.Item) As Item, Max(I.Description) AS ItemDesc, " &
                  " Max(L.FromProcess) As FromProcess, Max(P.Description) AS FromProcessDesc, " &
                  " Round(IFNull(Sum(L.Qty),0) - IFNull(Max(VJobRec.ChallanQty), 0) - IFNull(Max(VJobRec.LossQty), 0),4) As Qty, " &
                  " IFNull(Sum(L.TotalMeasure),0) - IFNull(Max(VJobRec.TotalMeasure), 0) As TotalMeasure, " &
                  " Max(L.Unit) As Unit, Max(L.MeasurePerPcs) As MeasurePerPcs, Max(L.Rate) AS Rate, " &
                  " Max(L.Dimension1) As Dimension1, Max(D1.Description) As Dimension1Desc, " &
                  " Max(L.Dimension2) As Dimension2, Max(D2.Description) As Dimension2Desc, " &
                  " Max(L.MeasureUnit) As MeasureUnit, Max(L.Specification) AS Specification, " &
                  " Max(L.Item_Uid) As Item_Uid, Max(Iu.Item_Uid) As Item_UidDesc, Max(L.ProdOrder) AS ProdOrder, Max(PO.V_Type) + '-' + Max(PO.ManualRefNo) AS ProdOrderNo, Max(L.ProdOrderSr) AS ProdOrderSr, " &
                  " Max(U.DecimalPlaces) as QtyDecimalPlaces, Max(MU.DecimalPlaces) as MeasureDecimalPlaces " &
                  " FROM (    " &
                  "     SELECT DocID, V_Type, ManualRefNo , V_Date     " &
                  "     FROM JobOrder  Where 1=1 " & HeaderConStr & " " &
                  " ) As H     " &
                  " LEFT JOIN JobOrderDetail L  ON H.DocID = L.JobOrder " &
                  " LEFT JOIN ProdOrder PO on PO.DocId = L.ProdOrder " &
                  " LEFT JOIN ( " &
                  "     SELECT L.JobOrder, L.JobOrderSr, Sum(L.Qty) AS ChallanQty, Sum(L.LossQty) AS LossQty, Sum(L.TotalMeasure) AS TotalMeasure " &
                  "     FROM JobReceiveDetail  L  " &
                  "     GROUP BY L.JobOrder, L.JobOrderSr  " &
                  " ) AS VJobRec ON L.DocId = VJobRec.JobOrder AND L.Sr = VJobRec.JobOrderSr   " &
                  " LEFT JOIN Item I On L.Item = I.Code " &
                  " LEFT JOIN Process P On P.NCat = L.FromProcess " &
                  " LEFT JOIN Item_Uid Iu On L.Item_Uid = Iu.Code " &
                  " Left Join Unit U On L.Unit = U.Code " &
                  " Left Join Unit MU On L.MeasureUnit = MU.Code " &
                  " Left Join Dimension1 D1 On L.Dimension1 = D1.Code " &
                  " Left Join Dimension2 D2 On L.Dimension2 = D2.Code " &
                  " WHERE 1 = 1 " & LineConStr &
                  " GROUP BY L.JobOrder, L.JobOrderSr "
        FRetFillItemWiseQry += " HAVING Round(IFNull(Sum(L.Qty),0) - IFNull(Max(VJobRec.ChallanQty), 0) - IFNull(Max(VJobRec.LossQty), 0),4) > 0 "
    End Function

    Private Sub FFillItemsForPendingJobOrders(ByVal bOrderNoStr As String)
        Dim I As Integer = 0
        Dim DtTemp As DataTable = Nothing
        Try
            If bOrderNoStr = "" Then Exit Sub

            If RbtCancelForOrderItem.Checked Then
                mQry = FRetFillItemWiseQry("", " And L.JobOrder + Convert(nVarChar, L.JobOrderSr) In (" & bOrderNoStr & ")")
            Else
                mQry = FRetFillItemWiseQry("", " And L.JobOrder In (" & bOrderNoStr & ") ")
            End If
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
            Dgl1.RowCount = 1
            Dgl1.Rows.Clear()

            With DtTemp
                If .Rows.Count > 0 Then
                    For I = 0 To .Rows.Count - 1
                        Dgl1.Rows.Add()
                        Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count - 1
                        Dgl1.Item(Col1Item_Uid, I).Tag = AgL.XNull(.Rows(I)("Item_Uid"))
                        Dgl1.Item(Col1Item_Uid, I).Value = AgL.XNull(.Rows(I)("Item_UidDesc"))
                        Dgl1.Item(Col1Item, I).Tag = AgL.XNull(.Rows(I)("Item"))
                        Dgl1.Item(Col1Item, I).Value = AgL.XNull(.Rows(I)("ItemDesc"))
                        Dgl1.Item(Col1JobOrder, I).Tag = AgL.XNull(.Rows(I)("JobOrder"))
                        Dgl1.Item(Col1JobOrder, I).Value = AgL.XNull(.Rows(I)("JobOrderNo"))
                        Dgl1.Item(Col1JobOrderSr, I).Value = AgL.XNull(.Rows(I)("JobOrderSr"))
                        Dgl1.Item(Col1ProdOrder, I).Tag = AgL.XNull(.Rows(I)("ProdOrder"))
                        Dgl1.Item(Col1ProdOrder, I).Value = AgL.XNull(.Rows(I)("ProdOrderNo"))
                        Dgl1.Item(Col1ProdOrderSr, I).Value = AgL.XNull(.Rows(I)("ProdOrderSr"))
                        Dgl1.Item(Col1FromProcess, I).Tag = AgL.XNull(.Rows(I)("FromProcess"))
                        Dgl1.Item(Col1FromProcess, I).Value = AgL.XNull(.Rows(I)("FromProcessDesc"))
                        Dgl1.Item(Col1QtyDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("QtyDecimalPlaces"))
                        Dgl1.Item(Col1Qty, I).Value = Format(AgL.VNull(.Rows(I)("Qty")), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
                        Dgl1.Item(Col1Rate, I).Value = AgL.VNull(.Rows(I)("Rate"))
                        Dgl1.Item(Col1Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                        Dgl1.Item(Col1MeasureUnit, I).Value = AgL.XNull(.Rows(I)("MeasureUnit"))
                        Dgl1.Item(Col1MeasureDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("MeasureDecimalPlaces"))
                        Dgl1.Item(Col1TotalMeasure, I).Value = Format(AgL.VNull(.Rows(I)("TotalMeasure")), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                        Dgl1.Item(Col1MeasurePerPcs, I).Value = Format(AgL.VNull(.Rows(I)("MeasurePerPcs")), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                        Dgl1.Item(Col1MeasureUnit, I).Value = AgL.XNull(.Rows(I)("MeasureUnit"))

                        Dgl1.Item(Col1Dimension1, I).Tag = AgL.XNull(.Rows(I)("Dimension1"))
                        Dgl1.Item(Col1Dimension1, I).Value = AgL.XNull(.Rows(I)("Dimension1Desc"))

                        Dgl1.Item(Col1Dimension2, I).Tag = AgL.XNull(.Rows(I)("Dimension2"))
                        Dgl1.Item(Col1Dimension2, I).Value = AgL.XNull(.Rows(I)("Dimension2Desc"))

                        'AgCalcGrid1.FCopyStructureLine(AgL.XNull(.Rows(I)("WorkOrder")), Dgl1, I, AgL.VNull(.Rows(I)("WorkOrderSr")))
                        CType(Dgl1.Columns(Col1Qty), AgControls.AgTextColumn).AgNumberRightPlaces = Val(Dgl1.Item(Col1QtyDecimalPlaces, Dgl1.CurrentCell.RowIndex).Value)
                        CType(Dgl1.Columns(Col1TotalMeasure), AgControls.AgTextColumn).AgNumberRightPlaces = Val(Dgl1.Item(Col1MeasureDecimalPlaces, Dgl1.CurrentCell.RowIndex).Value)

                    Next I
                End If
            End With
            Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    'Private Sub FFillItemsForOrder(ByVal bOrderNoStr As String)
    '    Dim I As Integer = 0
    '    Dim DtTemp As DataTable = Nothing
    '    Try
    '        If bOrderNoStr = "" Then Exit Sub
    '        mQry = " SELECT Max(L.Item_Uid) As Item_Uid, Max(L.Item) As Code, Max(I.Description) as Description, " & _
    '                " Max(I.ManualCode) As ManualCode,   Max(L.LotNo) As LotNo," & _
    '                " Max(H.V_Type) + '-' +  Max(H.ManualRefNo) AS JobOrderRefNo,   " & _
    '                " Max(H.V_Date) as JobOrderDate,  " & _
    '                " Sum(L.Qty) - IFNull(Max(Cd.Qty), 0) as [Bal.Qty],   " & _
    '                " Max(I.Unit) as Unit,   " & _
    '                " Sum(L.TotalMeasure) - IFNull(Sum(Cd.TotalMeasure), 0) as [Bal.Measure],   " & _
    '                " Max(I.MeasureUnit) MeasureUnit, Max(L.Rate) as Rate, Max(L.FromProcess) As FromProcess, Max(P.Description) As FromProcessDesc,  " & _
    '                " L.JobOrder, Max(L.ProdOrder) As ProdOrder, Max(Po.ManualRefNo) As ProdOrderNo,   " & _
    '                " Max(L.MeasurePerPcs) as MeasurePerPcs, " & _
    '                " L.JobOrderSr,   " & _
    '                " Max(U.DecimalPlaces) as QtyDecimalPlaces,  " & _
    '                " Max(U1.DecimalPlaces) as MeasureDecimalPlaces   " & _
    '                " FROM (  " & _
    '                "     SELECT DocID, V_Type, ManualRefNo, V_Date   " & _
    '                "     FROM JobOrder    " & _
    '                "     WHERE Jobworker ='" & TxtJobWorker.Tag & "'   " & _
    '                "     And Process = '" & TxtProcess.Tag & "'   " & _
    '                "     And Div_Code = '" & TxtDivision.Tag & "'   " & _
    '                "     AND Site_Code = '" & TxtSite_Code.Tag & "'   " & _
    '                "     AND V_Date <= '" & TxtV_Date.Text & "'   " & _
    '                "     ) H   " & _
    '                " LEFT JOIN JobOrderDetail L  ON H.DocID = L.JobOrder    " & _
    '                " Left Join Item I  On L.Item  = I.Code   " & _
    '                " LEFT JOIN ProdOrder Po On L.ProdOrder = Po.DocId " & _
    '                " LEFT JOIN Process P On L.FromProcess = P.NCat " & _
    '                " LEFT JOIN Voucher_Type Vt  ON H.V_Type = Vt.V_Type    " & _
    '                " Left Join (   " & _
    '                "     SELECT L.JobOrder, L.JobOrderSr, sum (L.Qty) AS Qty, Sum(L.TotalMeasure) as TotalMeasure      " & _
    '                " 	  FROM JobReceiveDetail L     " & _
    '                " 	  GROUP BY L.JobOrder, L.JobOrderSr   " & _
    '                " 	) AS CD ON L.JobOrder = CD.JobOrder AND L.JobOrderSr = CD.JobOrderSr   " & _
    '                " LEFT JOIN Unit U On L.Unit = U.Code   " & _
    '                " LEFT JOIN Unit U1 On L.MeasureUnit = U1.Code   " & _
    '                " WHERE L.JobOrder In (" & bOrderNoStr & ") " & _
    '                " And L.DocId <> '" & mSearchCode & "'" & _
    '                " GROUP BY L.JobOrder, L.JobOrderSr  " & _
    '                " HAVING IFNull(Sum(L.Qty),0) - IFNull(Max(Cd.Qty), 0) > 0 " & _
    '                " Order By JobOrderDate  "

    '        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

    '        With DtTemp
    '            Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
    '            If .Rows.Count > 0 Then
    '                For I = 0 To .Rows.Count - 1
    '                    Dgl1.Rows.Add()
    '                    Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count - 1

    '                    Dgl1.Item(Col1JobOrder, I).Tag = AgL.XNull(.Rows(I)("JobOrder"))
    '                    Dgl1.Item(Col1JobOrder, I).Value = AgL.XNull(.Rows(I)("JobOrderRefNo"))
    '                    Dgl1.Item(Col1JobOrderSr, I).Value = AgL.XNull(.Rows(I)("JobOrderSr"))
    '                    Dgl1.Item(Col1ProdOrder, I).Tag = AgL.XNull(.Rows(I)("ProdOrder"))
    '                    Dgl1.Item(Col1ProdOrder, I).Value = AgL.XNull(.Rows(I)("ProdOrderNo"))

    '                    Dgl1.Item(Col1FromProcess, I).Tag = AgL.XNull(.Rows(I)("FromProcess"))
    '                    Dgl1.Item(Col1FromProcess, I).Value = AgL.XNull(.Rows(I)("FromProcessDesc"))

    '                    Dgl1.Item(Col1Item_Uid, I).Tag = AgL.XNull(.Rows(I)("Item_Uid"))
    '                    Dgl1.Item(Col1Item_Uid, I).Value = AgL.XNull(AgL.Dman_Execute("Select Item_Uid From Item_Uid Where Code = '" & AgL.XNull(.Rows(I)("Item_Uid")) & "' ", AgL.GCn).ExecuteScalar)

    '                    Dgl1.Item(Col1Item, I).Tag = AgL.XNull(.Rows(I)("Code"))
    '                    Dgl1.Item(Col1Item, I).Value = AgL.XNull(.Rows(I)("Description"))
    '                    Dgl1.Item(Col1LotNo, I).Value = AgL.XNull(.Rows(I)("LotNo"))
    '                    Dgl1.Item(Col1Qty, I).Value = Format(AgL.VNull(.Rows(I)("Bal.Qty")), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
    '                    Dgl1.Item(Col1Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
    '                    Dgl1.Item(Col1MeasurePerPcs, I).Value = Format(AgL.VNull(.Rows(I)("MeasurePerPcs")), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
    '                    Dgl1.Item(Col1MeasureUnit, I).Value = AgL.XNull(.Rows(I)("MeasureUnit"))
    '                    Dgl1.Item(Col1TotalMeasure, I).Value = Format(AgL.VNull(.Rows(I)("Bal.Measure")), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
    '                    Dgl1.Item(Col1Rate, I).Value = Format(AgL.VNull(.Rows(I)("Rate")), "0.00")
    '                Next I
    '            End If
    '        End With
    '        Calculation()
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub

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

    Private Sub FrmFinishingOrder_BaseEvent_Topctrl_tbPrn(ByVal SearchCode As String) Handles Me.BaseEvent_Topctrl_tbPrn
        mQry = " SELECT H.V_Date, H.V_Type + '-' + H.ManualRefNo As ManualRefNo, H.DueDate, H.Remarks, P.Description AS FromProcessDesc, " &
                " H.JobInstructions, H.TermsAndConditions,   H.EntryBy, H.EntryDate, H.ApproveBy, H.ApproveDate, H.InsideOutside, JO.ManualrefNo AS OrderNo, " &
                " abs(L.Qty) AS Qty, L.Unit, L.MeasurePerPcs, abs(L.TotalMeasure) AS TotalMeasure, L.MeasureUnit, L.Rate, L.LotNo, L.Amount, L.PerimeterPerPcs, L.TotalPerimeter, L.Perimeter, " &
                " L.Remark As LineRemark,   L.Item_Uid, Sg.Name AS JobWorkerName,  Sg.Add1, Sg.Add2, Sg.Add3, Sg.Mobile, Sg.PAN, " &
                " Sg1.DispName AS OrderByName, G.Description AS GodownDesc,  I.Description AS ItemDesc, U.DecimalPlaces, " &
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
                " LEFT JOIN JobOrder JO ON JO.DocId = L.JobOrder " &
                " WHERE H.DocID =  '" & mSearchCode & "'  Order By L.Sr "
        ClsMain.FPrintThisDocument(Me, TxtV_Type.Tag, mQry, "Trade_JobOrderCancelPrint", "Job Order Cancel For " & TxtProcess.Text)
    End Sub

    Private Sub BtnConsumptionDetail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnConsumptionDetail.Click
        Dim FrmObj As FrmJobOrderCancelMaterialReceive
        If BtnConsumptionDetail.Tag Is Nothing Then
            FrmObj = New FrmJobOrderCancelMaterialReceive(TxtJobWorker.Tag, TxtProcess.Tag, TxtV_Date.Text, mInternalCode)
            FrmObj.IniGrid()
            'If AgL.StrCmp(Topctrl1.Mode, "Browse") Then
            FMovRecMaterialIssue(FrmObj) : BtnConsumptionDetail.Tag = FrmObj
            'End If
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
    Private Sub FMovRecMaterialIssue(ByVal FrmObj As FrmJobOrderCancelMaterialReceive)
        Dim DtTemp As DataTable = Nothing
        Dim I As Integer = 0

        mQry = " Select L.*, I.Description As ItemDesc,P.Description AS FromProcessDesc, D1.Description As D1Desc, D2.Description AS D2Desc " &
                " From JobReceiveDetail L " &
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
                    .Dgl1.Item(FrmJobOrderCancelMaterialReceive.ColSNo, I).Value = .Dgl1.Rows.Count - 1
                    .Dgl1.Item(FrmJobOrderCancelMaterialReceive.Col1Item, I).Tag = AgL.XNull(DtTemp.Rows(I)("Item"))
                    .Dgl1.Item(FrmJobOrderCancelMaterialReceive.Col1Item, I).Value = AgL.XNull(DtTemp.Rows(I)("ItemDesc"))
                    .Dgl1.Item(FrmJobOrderCancelMaterialReceive.Col1LotNo, I).Value = AgL.XNull(DtTemp.Rows(I)("LotNo"))
                    .Dgl1.Item(FrmJobOrderCancelMaterialReceive.Col1Qty, I).Value = AgL.VNull(DtTemp.Rows(I)("Qty"))
                    .Dgl1.Item(FrmJobOrderCancelMaterialReceive.Col1Unit, I).Value = AgL.XNull(DtTemp.Rows(I)("Unit"))

                    .Dgl1.Item(FrmJobOrderCancelMaterialReceive.Col1FromProcess, I).Tag = AgL.XNull(DtTemp.Rows(I)("PrevProcess"))
                    .Dgl1.Item(FrmJobOrderCancelMaterialReceive.Col1FromProcess, I).Value = AgL.XNull(DtTemp.Rows(I)("FromProcessDesc"))

                    .Dgl1.Item(FrmJobOrderCancelMaterialReceive.Col1Dimension1, I).Tag = AgL.XNull(DtTemp.Rows(I)("Dimension1"))
                    .Dgl1.Item(FrmJobOrderCancelMaterialReceive.Col1Dimension1, I).Value = AgL.XNull(DtTemp.Rows(I)("D1Desc"))
                    .Dgl1.Item(FrmJobOrderCancelMaterialReceive.Col1Dimension2, I).Tag = AgL.XNull(DtTemp.Rows(I)("Dimension2"))
                    .Dgl1.Item(FrmJobOrderCancelMaterialReceive.Col1Dimension2, I).Value = AgL.XNull(DtTemp.Rows(I)("D2Desc"))
                Next I
            End If
        End With
    End Sub

    Private Sub FPostMaterialReceive(ByVal Conn As SQLiteConnection, ByVal Cmd As SQLiteCommand)
        Dim MaxSr As Integer = 0
        Dim I As Integer = 0
        Dim mSr As Integer = 0
        Dim bSelectionQry As String = ""
        Dim FrmObj As FrmJobOrderCancelMaterialReceive = Nothing

        mQry = ""
        If BtnConsumptionDetail.Tag IsNot Nothing Then
            mQry = "Delete From Stock Where DocId = '" & mSearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            mQry = "Delete From StockProcess Where DocId = '" & mSearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            mQry = "Delete From JobReceiveDetail Where DocId = '" & mSearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            mQry = "Delete From JobIssRec Where DocId = '" & mSearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

            FrmObj = BtnConsumptionDetail.Tag

            With FrmObj
                For I = 0 To .Dgl1.Rows.Count - 1
                    If .Dgl1.Item(FrmJobOrderCancelMaterialReceive.Col1Item, I).Value <> "" Then
                        mSr += 1
                        If bSelectionQry <> "" Then bSelectionQry += " UNION ALL "
                        bSelectionQry += "SELECT '" & mSearchCode & "', " & mSr & " As Sr, " &
                                " " & AgL.Chk_Text(.Dgl1.Item(FrmJobOrderCancelMaterialReceive.Col1Item, I).Tag) & ", " &
                                " " & AgL.Chk_Text(.Dgl1.Item(FrmJobOrderCancelMaterialReceive.Col1LotNo, I).Value) & ", " &
                                " " & AgL.Chk_Text(.Dgl1.Item(FrmJobOrderCancelMaterialReceive.Col1FromProcess, I).Tag) & ", " &
                                " " & AgL.Chk_Text(.Dgl1.Item(FrmJobOrderCancelMaterialReceive.Col1Dimension1, I).Tag) & ", " &
                                " " & AgL.Chk_Text(.Dgl1.Item(FrmJobOrderCancelMaterialReceive.Col1Dimension2, I).Tag) & ", " &
                                " " & Val(.Dgl1.Item(FrmJobOrderCancelMaterialReceive.Col1Qty, I).Value) & ", " &
                                " " & AgL.Chk_Text(.Dgl1.Item(FrmJobOrderCancelMaterialReceive.Col1Unit, I).Value) & " "
                    End If
                Next
            End With

            mQry = " INSERT INTO JobIssRec (DocID, V_Type, V_Prefix, V_Date, V_No, Div_Code, Site_Code, ManualRefNo, Process, JobWorker, Godown, IssQty, EntryBy, EntryDate, Status, CostCenter) " &
                    " SELECT H.DocID, H.V_Type, H.V_Prefix, H.V_Date, H.V_No, H.Div_Code, H.Site_Code, " &
                    " H.ManualRefNo, H.Process, H.JobWorker, H.Godown, H.TotalQty, H.EntryBy, H.EntryDate, " &
                    " H.Status, H.CostCenter  " &
                    " FROM JobOrder H Where H.DocId = '" & mSearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

            mQry = "INSERT INTO JobReceiveDetail(DocId, Sr, Item, LotNO, PrevProcess, Dimension1, Dimension2, Qty, Unit) " & bSelectionQry
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

            MaxSr = AgL.VNull(AgL.Dman_Execute("Select Max(Sr) From Stock  Where DocId = '" & mSearchCode & "'", AgL.GcnRead).ExecuteScalar)

            mQry = "INSERT INTO Stock(DocID, Sr, V_Type, V_Prefix, V_Date, V_No, RecId, Div_Code, " & _
                    " Site_Code, SubCode, Item, LotNo, Godown, Qty_Rec, Unit, MeasurePerPcs, Measure_Rec, MeasureUnit, " & _
                    " Process, Dimension1, Dimension2, CostCenter) " & _
                    " SELECT L.DocId, " & MaxSr & " + row_NUMBER() OVER (ORDER BY L.Sr) AS Sr, " & _
                    " H.V_Type, H.V_Prefix, H.V_Date, H.V_No, H.ManualRefNo, H.Div_Code, H.Site_Code, " & _
                    " H.JobWorker, L.Item, LotNo, H.Godown, L.Qty As Qty_Iss, L.Unit, " & _
                    " L.MeasurePerPcs, L.TotalMeasure Measure_Iss, " & _
                    " L.MeasureUnit, L.PrevProcess, L.Dimension1, L.Dimension2, J.CostCenter " & _
                    " FROM (Select * From JobReceiveDetail Where DocId = '" & mSearchCode & "') As L  " & _
                    " LEFT JOIN JobIssRec H On L.DocId = H.DocId " & _
                    " LEFT JOIN JobOrder J On L.JobOrder = J.DocId "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

            mQry = "INSERT INTO StockProcess(DocID, Sr, V_Type, V_Prefix, V_Date, V_No, RecId, Div_Code, " & _
                    " Site_Code, SubCode, Item, LotNo, Godown, Qty_Iss, Unit, MeasurePerPcs, Measure_Iss, MeasureUnit, " & _
                    " Process, Dimension1, Dimension2, CostCenter) " & _
                    " SELECT L.DocId, " & MaxSr & " + row_NUMBER() OVER (ORDER BY L.Sr) AS Sr, " & _
                    " H.V_Type, H.V_Prefix, H.V_Date, H.V_No, H.ManualRefNo, H.Div_Code, H.Site_Code, " & _
                    " H.JobWorker, L.Item, LotNo, H.Godown, L.Qty As Qty_Iss, L.Unit, " & _
                    " L.MeasurePerPcs, L.TotalMeasure Measure_Iss, " & _
                    " L.MeasureUnit, '" & TxtProcess.Tag & "', L.Dimension1, L.Dimension2, J.CostCenter " & _
                    " FROM (Select * From JobReceiveDetail Where DocId = '" & mSearchCode & "') As L  " & _
                    " LEFT JOIN JobIssRec H On L.DocId = H.DocId " & _
                    " LEFT JOIN JobOrder J On L.JobOrder = J.DocId "
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If
    End Sub
End Class
