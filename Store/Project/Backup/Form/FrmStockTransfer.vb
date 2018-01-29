Imports System.IO
Public Class FrmStockTransfer
    Inherits AgTemplate.TempTransaction
    Dim mQry$

    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Public Const ColSNo As String = "S.No."
    Public Const Col1Item_UID As String = "Item UID"
    Public Const Col1ItemCode As String = "Item Code"
    Public Const Col1Item As String = "Item"
    Public Const Col1Dimension1 As String = "Dimension1"
    Public Const Col1Dimension2 As String = "Dimension2"
    Public Const Col1Specification As String = "Specification"
    Public Const Col1LotNo As String = "Lot No"
    Public Const Col1BaleNo As String = "Bale No"
    Public Const Col1Process As String = "Process"
    Public Const Col1CurrentStock As String = "Current Stock"
    Public Const Col1Qty As String = "Qty"
    Public Const Col1Unit As String = "Unit"
    Public Const Col1QtyDecimalPlaces As String = "Qty Decimal Places"
    Public Const Col1MeasurePerPcs As String = "Measure Per Pcs"
    Public Const Col1TotalMeasure As String = "Total Measure"
    Public Const Col1MeasureUnit As String = "Measure Unit"
    Public Const Col1MeasureDecimalPlaces As String = "Measure Decimal Places"
    Public Const Col1Rate As String = "Rate"
    Public Const Col1Amount As String = "Amount"
    Public Const Col1Remarks As String = "Remarks"

    Dim ImportMessegeStr$ = ""
    Protected WithEvents LblCurrentStock As System.Windows.Forms.Label
    Protected WithEvents LblCurrentStockText As System.Windows.Forms.Label
    Protected WithEvents ChkShowOnlyImportedRecords As System.Windows.Forms.CheckBox
    Protected WithEvents GrpDirectIssue As System.Windows.Forms.GroupBox
    Protected WithEvents RbtnForStock As System.Windows.Forms.RadioButton
    Protected WithEvents RbtTransferDirect As System.Windows.Forms.RadioButton
    Protected WithEvents BtnFillIssueDetail As System.Windows.Forms.Button
    Dim ImportMode As Boolean = False

    Public Sub New(ByVal StrUPVar As String, ByVal DTUP As DataTable, ByVal NCatStr As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Topctrl1.FSetParent(Me, StrUPVar, DTUP)
        Topctrl1.SetDisp(True)

        EntryNCat = NCatStr
        mQry = "Select H.* from Voucher_Type_Settings H Left Join Voucher_Type Vt On H.V_Type = Vt.V_Type  Where Vt.NCat In ('" & EntryNCat & "') And H.Div_Code = '" & AgL.PubDivCode & "' And H.Site_Code ='" & AgL.PubSiteCode & "' "
        DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)
    End Sub

#Region "Form Designer Code"
    Private Sub InitializeComponent()
        Me.Dgl1 = New AgControls.AgDataGrid
        Me.TxtFromGodown = New AgControls.AgTextBox
        Me.LblFromGodown = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.LblCurrentStock = New System.Windows.Forms.Label
        Me.LblCurrentStockText = New System.Windows.Forms.Label
        Me.LblTotalMeasure = New System.Windows.Forms.Label
        Me.Label33 = New System.Windows.Forms.Label
        Me.LblTotalQty = New System.Windows.Forms.Label
        Me.LblTotalQtyText = New System.Windows.Forms.Label
        Me.Pnl1 = New System.Windows.Forms.Panel
        Me.Label30 = New System.Windows.Forms.Label
        Me.TxtRemarks = New AgControls.AgTextBox
        Me.LblFromGodownReq = New System.Windows.Forms.Label
        Me.TxtManualRefNo = New AgControls.AgTextBox
        Me.LblManualRefNo = New System.Windows.Forms.Label
        Me.LblMaterialPlanForFollowingItems = New System.Windows.Forms.LinkLabel
        Me.Label1 = New System.Windows.Forms.Label
        Me.TxtToGodown = New AgControls.AgTextBox
        Me.LblToGodown = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.BtnImprtFromText = New System.Windows.Forms.Button
        Me.ChkShowOnlyImportedRecords = New System.Windows.Forms.CheckBox
        Me.GrpDirectIssue = New System.Windows.Forms.GroupBox
        Me.RbtnForStock = New System.Windows.Forms.RadioButton
        Me.RbtTransferDirect = New System.Windows.Forms.RadioButton
        Me.BtnFillIssueDetail = New System.Windows.Forms.Button
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
        Me.GrpDirectIssue.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.Location = New System.Drawing.Point(733, 493)
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
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(582, 493)
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
        Me.GBoxApprove.Location = New System.Drawing.Point(415, 493)
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
        Me.GBoxEntryType.Location = New System.Drawing.Point(150, 493)
        Me.GBoxEntryType.Size = New System.Drawing.Size(119, 40)
        '
        'TxtEntryType
        '
        Me.TxtEntryType.Location = New System.Drawing.Point(3, 19)
        Me.TxtEntryType.Tag = ""
        '
        'GrpUP
        '
        Me.GrpUP.Location = New System.Drawing.Point(16, 493)
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
        Me.GroupBox1.Location = New System.Drawing.Point(2, 489)
        Me.GroupBox1.Size = New System.Drawing.Size(907, 4)
        '
        'GBoxDivision
        '
        Me.GBoxDivision.Location = New System.Drawing.Point(285, 493)
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
        Me.LblV_No.Location = New System.Drawing.Point(348, 232)
        Me.LblV_No.Size = New System.Drawing.Size(78, 16)
        Me.LblV_No.Tag = ""
        Me.LblV_No.Text = "Transfer No."
        Me.LblV_No.Visible = False
        '
        'TxtV_No
        '
        Me.TxtV_No.AgSelectedValue = ""
        Me.TxtV_No.BackColor = System.Drawing.Color.White
        Me.TxtV_No.Location = New System.Drawing.Point(470, 231)
        Me.TxtV_No.Size = New System.Drawing.Size(217, 18)
        Me.TxtV_No.TabIndex = 3
        Me.TxtV_No.Tag = ""
        Me.TxtV_No.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.TxtV_No.Visible = False
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(292, 53)
        Me.Label2.Tag = ""
        '
        'LblV_Date
        '
        Me.LblV_Date.BackColor = System.Drawing.Color.Transparent
        Me.LblV_Date.Location = New System.Drawing.Point(186, 48)
        Me.LblV_Date.Size = New System.Drawing.Size(89, 16)
        Me.LblV_Date.Tag = ""
        Me.LblV_Date.Text = "Transfer  Date"
        '
        'LblV_TypeReq
        '
        Me.LblV_TypeReq.Location = New System.Drawing.Point(525, 33)
        Me.LblV_TypeReq.Tag = ""
        '
        'TxtV_Date
        '
        Me.TxtV_Date.AgSelectedValue = ""
        Me.TxtV_Date.BackColor = System.Drawing.Color.White
        Me.TxtV_Date.Location = New System.Drawing.Point(313, 47)
        Me.TxtV_Date.Size = New System.Drawing.Size(120, 18)
        Me.TxtV_Date.TabIndex = 2
        Me.TxtV_Date.Tag = ""
        '
        'LblV_Type
        '
        Me.LblV_Type.Location = New System.Drawing.Point(439, 29)
        Me.LblV_Type.Size = New System.Drawing.Size(85, 16)
        Me.LblV_Type.Tag = ""
        Me.LblV_Type.Text = "Transfer Type"
        '
        'TxtV_Type
        '
        Me.TxtV_Type.AgSelectedValue = ""
        Me.TxtV_Type.BackColor = System.Drawing.Color.White
        Me.TxtV_Type.Location = New System.Drawing.Point(541, 27)
        Me.TxtV_Type.Size = New System.Drawing.Size(187, 18)
        Me.TxtV_Type.TabIndex = 1
        Me.TxtV_Type.Tag = ""
        '
        'LblSite_CodeReq
        '
        Me.LblSite_CodeReq.Location = New System.Drawing.Point(292, 33)
        Me.LblSite_CodeReq.Tag = ""
        '
        'LblSite_Code
        '
        Me.LblSite_Code.BackColor = System.Drawing.Color.Transparent
        Me.LblSite_Code.Location = New System.Drawing.Point(186, 28)
        Me.LblSite_Code.Size = New System.Drawing.Size(87, 16)
        Me.LblSite_Code.Tag = ""
        Me.LblSite_Code.Text = "Branch Name"
        '
        'TxtSite_Code
        '
        Me.TxtSite_Code.AgSelectedValue = ""
        Me.TxtSite_Code.BackColor = System.Drawing.Color.White
        Me.TxtSite_Code.Location = New System.Drawing.Point(313, 27)
        Me.TxtSite_Code.Size = New System.Drawing.Size(120, 18)
        Me.TxtSite_Code.TabIndex = 0
        Me.TxtSite_Code.Tag = ""
        '
        'LblDocId
        '
        Me.LblDocId.Tag = ""
        '
        'LblPrefix
        '
        Me.LblPrefix.Location = New System.Drawing.Point(711, 192)
        Me.LblPrefix.Tag = ""
        Me.LblPrefix.Visible = False
        '
        'TabControl1
        '
        Me.TabControl1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl1.Location = New System.Drawing.Point(-3, 5)
        Me.TabControl1.Size = New System.Drawing.Size(896, 159)
        Me.TabControl1.TabIndex = 0
        '
        'TP1
        '
        Me.TP1.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.TP1.Controls.Add(Me.Label3)
        Me.TP1.Controls.Add(Me.TxtToGodown)
        Me.TP1.Controls.Add(Me.LblToGodown)
        Me.TP1.Controls.Add(Me.Label1)
        Me.TP1.Controls.Add(Me.TxtManualRefNo)
        Me.TP1.Controls.Add(Me.LblManualRefNo)
        Me.TP1.Controls.Add(Me.LblFromGodownReq)
        Me.TP1.Controls.Add(Me.Label30)
        Me.TP1.Controls.Add(Me.TxtRemarks)
        Me.TP1.Controls.Add(Me.TxtFromGodown)
        Me.TP1.Controls.Add(Me.LblFromGodown)
        Me.TP1.Location = New System.Drawing.Point(4, 22)
        Me.TP1.Size = New System.Drawing.Size(888, 133)
        Me.TP1.Text = "Document Detail"
        Me.TP1.Controls.SetChildIndex(Me.TxtV_No, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtSite_Code, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_Date, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label2, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_No, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblSite_Code, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtV_Date, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblSite_CodeReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblFromGodown, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblPrefix, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtFromGodown, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_Type, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtRemarks, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label30, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_TypeReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtV_Type, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblDocId, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtDocId, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblFromGodownReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblManualRefNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtManualRefNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label1, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblToGodown, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtToGodown, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label3, 0)
        '
        'Topctrl1
        '
        Me.Topctrl1.Size = New System.Drawing.Size(889, 41)
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
        'TxtFromGodown
        '
        Me.TxtFromGodown.AgAllowUserToEnableMasterHelp = False
        Me.TxtFromGodown.AgLastValueTag = Nothing
        Me.TxtFromGodown.AgLastValueText = Nothing
        Me.TxtFromGodown.AgMandatory = True
        Me.TxtFromGodown.AgMasterHelp = False
        Me.TxtFromGodown.AgNumberLeftPlaces = 8
        Me.TxtFromGodown.AgNumberNegetiveAllow = False
        Me.TxtFromGodown.AgNumberRightPlaces = 2
        Me.TxtFromGodown.AgPickFromLastValue = False
        Me.TxtFromGodown.AgRowFilter = ""
        Me.TxtFromGodown.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtFromGodown.AgSelectedValue = Nothing
        Me.TxtFromGodown.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtFromGodown.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtFromGodown.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtFromGodown.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFromGodown.Location = New System.Drawing.Point(313, 67)
        Me.TxtFromGodown.MaxLength = 20
        Me.TxtFromGodown.Name = "TxtFromGodown"
        Me.TxtFromGodown.Size = New System.Drawing.Size(415, 18)
        Me.TxtFromGodown.TabIndex = 4
        '
        'LblFromGodown
        '
        Me.LblFromGodown.AutoSize = True
        Me.LblFromGodown.BackColor = System.Drawing.Color.Transparent
        Me.LblFromGodown.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblFromGodown.Location = New System.Drawing.Point(186, 67)
        Me.LblFromGodown.Name = "LblFromGodown"
        Me.LblFromGodown.Size = New System.Drawing.Size(89, 16)
        Me.LblFromGodown.TabIndex = 706
        Me.LblFromGodown.Text = "From Godown"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Cornsilk
        Me.Panel1.Controls.Add(Me.LblCurrentStock)
        Me.Panel1.Controls.Add(Me.LblCurrentStockText)
        Me.Panel1.Controls.Add(Me.LblTotalMeasure)
        Me.Panel1.Controls.Add(Me.Label33)
        Me.Panel1.Controls.Add(Me.LblTotalQty)
        Me.Panel1.Controls.Add(Me.LblTotalQtyText)
        Me.Panel1.Location = New System.Drawing.Point(5, 464)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(879, 23)
        Me.Panel1.TabIndex = 694
        '
        'LblCurrentStock
        '
        Me.LblCurrentStock.AutoSize = True
        Me.LblCurrentStock.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCurrentStock.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblCurrentStock.Location = New System.Drawing.Point(118, 3)
        Me.LblCurrentStock.Name = "LblCurrentStock"
        Me.LblCurrentStock.Size = New System.Drawing.Size(12, 16)
        Me.LblCurrentStock.TabIndex = 668
        Me.LblCurrentStock.Text = "."
        Me.LblCurrentStock.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblCurrentStockText
        '
        Me.LblCurrentStockText.AutoSize = True
        Me.LblCurrentStockText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCurrentStockText.ForeColor = System.Drawing.Color.Maroon
        Me.LblCurrentStockText.Location = New System.Drawing.Point(10, 3)
        Me.LblCurrentStockText.Name = "LblCurrentStockText"
        Me.LblCurrentStockText.Size = New System.Drawing.Size(102, 16)
        Me.LblCurrentStockText.TabIndex = 667
        Me.LblCurrentStockText.Text = "Current Stock :"
        '
        'LblTotalMeasure
        '
        Me.LblTotalMeasure.AutoSize = True
        Me.LblTotalMeasure.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalMeasure.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalMeasure.Location = New System.Drawing.Point(754, 3)
        Me.LblTotalMeasure.Name = "LblTotalMeasure"
        Me.LblTotalMeasure.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalMeasure.TabIndex = 666
        Me.LblTotalMeasure.Text = "."
        Me.LblTotalMeasure.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label33
        '
        Me.Label33.AutoSize = True
        Me.Label33.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label33.ForeColor = System.Drawing.Color.Maroon
        Me.Label33.Location = New System.Drawing.Point(643, 3)
        Me.Label33.Name = "Label33"
        Me.Label33.Size = New System.Drawing.Size(105, 16)
        Me.Label33.TabIndex = 665
        Me.Label33.Text = "Total Measure :"
        '
        'LblTotalQty
        '
        Me.LblTotalQty.AutoSize = True
        Me.LblTotalQty.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalQty.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalQty.Location = New System.Drawing.Point(446, 3)
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
        Me.LblTotalQtyText.Location = New System.Drawing.Point(361, 3)
        Me.LblTotalQtyText.Name = "LblTotalQtyText"
        Me.LblTotalQtyText.Size = New System.Drawing.Size(72, 16)
        Me.LblTotalQtyText.TabIndex = 659
        Me.LblTotalQtyText.Text = "Total Qty :"
        '
        'Pnl1
        '
        Me.Pnl1.Location = New System.Drawing.Point(4, 190)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(880, 274)
        Me.Pnl1.TabIndex = 1
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(186, 108)
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
        Me.TxtRemarks.Location = New System.Drawing.Point(313, 107)
        Me.TxtRemarks.MaxLength = 255
        Me.TxtRemarks.Name = "TxtRemarks"
        Me.TxtRemarks.Size = New System.Drawing.Size(415, 18)
        Me.TxtRemarks.TabIndex = 6
        '
        'LblFromGodownReq
        '
        Me.LblFromGodownReq.AutoSize = True
        Me.LblFromGodownReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblFromGodownReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblFromGodownReq.Location = New System.Drawing.Point(292, 74)
        Me.LblFromGodownReq.Name = "LblFromGodownReq"
        Me.LblFromGodownReq.Size = New System.Drawing.Size(10, 7)
        Me.LblFromGodownReq.TabIndex = 724
        Me.LblFromGodownReq.Text = "Ä"
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
        Me.TxtManualRefNo.Location = New System.Drawing.Point(541, 47)
        Me.TxtManualRefNo.MaxLength = 50
        Me.TxtManualRefNo.Name = "TxtManualRefNo"
        Me.TxtManualRefNo.Size = New System.Drawing.Size(187, 18)
        Me.TxtManualRefNo.TabIndex = 3
        '
        'LblManualRefNo
        '
        Me.LblManualRefNo.AutoSize = True
        Me.LblManualRefNo.BackColor = System.Drawing.Color.Transparent
        Me.LblManualRefNo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblManualRefNo.Location = New System.Drawing.Point(439, 48)
        Me.LblManualRefNo.Name = "LblManualRefNo"
        Me.LblManualRefNo.Size = New System.Drawing.Size(78, 16)
        Me.LblManualRefNo.TabIndex = 731
        Me.LblManualRefNo.Text = "Transfer No."
        '
        'LblMaterialPlanForFollowingItems
        '
        Me.LblMaterialPlanForFollowingItems.BackColor = System.Drawing.Color.SteelBlue
        Me.LblMaterialPlanForFollowingItems.DisabledLinkColor = System.Drawing.Color.White
        Me.LblMaterialPlanForFollowingItems.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblMaterialPlanForFollowingItems.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LblMaterialPlanForFollowingItems.LinkColor = System.Drawing.Color.White
        Me.LblMaterialPlanForFollowingItems.Location = New System.Drawing.Point(4, 170)
        Me.LblMaterialPlanForFollowingItems.Name = "LblMaterialPlanForFollowingItems"
        Me.LblMaterialPlanForFollowingItems.Size = New System.Drawing.Size(107, 19)
        Me.LblMaterialPlanForFollowingItems.TabIndex = 804
        Me.LblMaterialPlanForFollowingItems.TabStop = True
        Me.LblMaterialPlanForFollowingItems.Text = "Item Detail"
        Me.LblMaterialPlanForFollowingItems.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(525, 53)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(10, 7)
        Me.Label1.TabIndex = 732
        Me.Label1.Text = "Ä"
        '
        'TxtToGodown
        '
        Me.TxtToGodown.AgAllowUserToEnableMasterHelp = False
        Me.TxtToGodown.AgLastValueTag = Nothing
        Me.TxtToGodown.AgLastValueText = Nothing
        Me.TxtToGodown.AgMandatory = True
        Me.TxtToGodown.AgMasterHelp = False
        Me.TxtToGodown.AgNumberLeftPlaces = 8
        Me.TxtToGodown.AgNumberNegetiveAllow = False
        Me.TxtToGodown.AgNumberRightPlaces = 2
        Me.TxtToGodown.AgPickFromLastValue = False
        Me.TxtToGodown.AgRowFilter = ""
        Me.TxtToGodown.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtToGodown.AgSelectedValue = Nothing
        Me.TxtToGodown.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtToGodown.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtToGodown.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtToGodown.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtToGodown.Location = New System.Drawing.Point(313, 87)
        Me.TxtToGodown.MaxLength = 20
        Me.TxtToGodown.Name = "TxtToGodown"
        Me.TxtToGodown.Size = New System.Drawing.Size(415, 18)
        Me.TxtToGodown.TabIndex = 5
        '
        'LblToGodown
        '
        Me.LblToGodown.AutoSize = True
        Me.LblToGodown.BackColor = System.Drawing.Color.Transparent
        Me.LblToGodown.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblToGodown.Location = New System.Drawing.Point(186, 88)
        Me.LblToGodown.Name = "LblToGodown"
        Me.LblToGodown.Size = New System.Drawing.Size(72, 16)
        Me.LblToGodown.TabIndex = 734
        Me.LblToGodown.Text = "To Godown"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(292, 94)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(10, 7)
        Me.Label3.TabIndex = 735
        Me.Label3.Text = "Ä"
        '
        'BtnImprtFromText
        '
        Me.BtnImprtFromText.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnImprtFromText.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnImprtFromText.Location = New System.Drawing.Point(813, 164)
        Me.BtnImprtFromText.Name = "BtnImprtFromText"
        Me.BtnImprtFromText.Size = New System.Drawing.Size(70, 25)
        Me.BtnImprtFromText.TabIndex = 764
        Me.BtnImprtFromText.TabStop = False
        Me.BtnImprtFromText.Text = "Import"
        Me.BtnImprtFromText.UseVisualStyleBackColor = True
        '
        'ChkShowOnlyImportedRecords
        '
        Me.ChkShowOnlyImportedRecords.AutoSize = True
        Me.ChkShowOnlyImportedRecords.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkShowOnlyImportedRecords.Location = New System.Drawing.Point(595, 167)
        Me.ChkShowOnlyImportedRecords.Name = "ChkShowOnlyImportedRecords"
        Me.ChkShowOnlyImportedRecords.Size = New System.Drawing.Size(214, 17)
        Me.ChkShowOnlyImportedRecords.TabIndex = 805
        Me.ChkShowOnlyImportedRecords.Text = "Show Only Imported Records"
        Me.ChkShowOnlyImportedRecords.UseVisualStyleBackColor = True
        '
        'GrpDirectIssue
        '
        Me.GrpDirectIssue.BackColor = System.Drawing.Color.Transparent
        Me.GrpDirectIssue.Controls.Add(Me.RbtnForStock)
        Me.GrpDirectIssue.Controls.Add(Me.RbtTransferDirect)
        Me.GrpDirectIssue.Location = New System.Drawing.Point(123, 160)
        Me.GrpDirectIssue.Name = "GrpDirectIssue"
        Me.GrpDirectIssue.Size = New System.Drawing.Size(245, 28)
        Me.GrpDirectIssue.TabIndex = 806
        Me.GrpDirectIssue.TabStop = False
        '
        'RbtnForStock
        '
        Me.RbtnForStock.AutoSize = True
        Me.RbtnForStock.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RbtnForStock.Location = New System.Drawing.Point(138, 8)
        Me.RbtnForStock.Name = "RbtnForStock"
        Me.RbtnForStock.Size = New System.Drawing.Size(87, 17)
        Me.RbtnForStock.TabIndex = 744
        Me.RbtnForStock.TabStop = True
        Me.RbtnForStock.Text = "For Stock"
        Me.RbtnForStock.UseVisualStyleBackColor = True
        '
        'RbtTransferDirect
        '
        Me.RbtTransferDirect.AutoSize = True
        Me.RbtTransferDirect.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RbtTransferDirect.Location = New System.Drawing.Point(9, 8)
        Me.RbtTransferDirect.Name = "RbtTransferDirect"
        Me.RbtTransferDirect.Size = New System.Drawing.Size(124, 17)
        Me.RbtTransferDirect.TabIndex = 743
        Me.RbtTransferDirect.TabStop = True
        Me.RbtTransferDirect.Text = "Transfer Direct"
        Me.RbtTransferDirect.UseVisualStyleBackColor = True
        '
        'BtnFillIssueDetail
        '
        Me.BtnFillIssueDetail.BackColor = System.Drawing.Color.Transparent
        Me.BtnFillIssueDetail.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnFillIssueDetail.Font = New System.Drawing.Font("Verdana", 6.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnFillIssueDetail.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnFillIssueDetail.Location = New System.Drawing.Point(371, 168)
        Me.BtnFillIssueDetail.Margin = New System.Windows.Forms.Padding(0)
        Me.BtnFillIssueDetail.Name = "BtnFillIssueDetail"
        Me.BtnFillIssueDetail.Size = New System.Drawing.Size(28, 19)
        Me.BtnFillIssueDetail.TabIndex = 807
        Me.BtnFillIssueDetail.TabStop = False
        Me.BtnFillIssueDetail.Text = "...."
        Me.BtnFillIssueDetail.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnFillIssueDetail.UseVisualStyleBackColor = False
        '
        'FrmStockTransfer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.BackColor = System.Drawing.SystemColors.ButtonShadow
        Me.ClientSize = New System.Drawing.Size(889, 534)
        Me.Controls.Add(Me.BtnFillIssueDetail)
        Me.Controls.Add(Me.GrpDirectIssue)
        Me.Controls.Add(Me.ChkShowOnlyImportedRecords)
        Me.Controls.Add(Me.BtnImprtFromText)
        Me.Controls.Add(Me.LblMaterialPlanForFollowingItems)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Pnl1)
        Me.Name = "FrmStockTransfer"
        Me.Text = "Stock Transfer"
        Me.Controls.SetChildIndex(Me.TabControl1, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.GrpUP, 0)
        Me.Controls.SetChildIndex(Me.GBoxEntryType, 0)
        Me.Controls.SetChildIndex(Me.GBoxApprove, 0)
        Me.Controls.SetChildIndex(Me.GBoxMoveToLog, 0)
        Me.Controls.SetChildIndex(Me.GroupBox2, 0)
        Me.Controls.SetChildIndex(Me.Pnl1, 0)
        Me.Controls.SetChildIndex(Me.Panel1, 0)
        Me.Controls.SetChildIndex(Me.GBoxDivision, 0)
        Me.Controls.SetChildIndex(Me.LblMaterialPlanForFollowingItems, 0)
        Me.Controls.SetChildIndex(Me.BtnImprtFromText, 0)
        Me.Controls.SetChildIndex(Me.Topctrl1, 0)
        Me.Controls.SetChildIndex(Me.ChkShowOnlyImportedRecords, 0)
        Me.Controls.SetChildIndex(Me.GrpDirectIssue, 0)
        Me.Controls.SetChildIndex(Me.BtnFillIssueDetail, 0)
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
        Me.GrpDirectIssue.ResumeLayout(False)
        Me.GrpDirectIssue.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Protected WithEvents TxtFromGodown As AgControls.AgTextBox
    Protected WithEvents LblFromGodown As System.Windows.Forms.Label
    Protected WithEvents Panel1 As System.Windows.Forms.Panel
    Protected WithEvents LblTotalQty As System.Windows.Forms.Label
    Protected WithEvents LblTotalQtyText As System.Windows.Forms.Label
    Protected WithEvents Pnl1 As System.Windows.Forms.Panel
    Protected WithEvents LblTotalMeasure As System.Windows.Forms.Label
    Protected WithEvents TxtRemarks As AgControls.AgTextBox
    Protected WithEvents Label30 As System.Windows.Forms.Label
    Protected WithEvents LblFromGodownReq As System.Windows.Forms.Label
    Protected WithEvents Label33 As System.Windows.Forms.Label
    Protected WithEvents TxtManualRefNo As AgControls.AgTextBox
    Protected WithEvents LblManualRefNo As System.Windows.Forms.Label
    Protected WithEvents LblMaterialPlanForFollowingItems As System.Windows.Forms.LinkLabel
    Protected WithEvents Label1 As System.Windows.Forms.Label
    Protected WithEvents TxtToGodown As AgControls.AgTextBox
    Protected WithEvents LblToGodown As System.Windows.Forms.Label
    Protected WithEvents Label3 As System.Windows.Forms.Label
    Protected WithEvents BtnImprtFromText As System.Windows.Forms.Button
#End Region

    Private Sub FrmStockTransfer_BaseEvent_ApproveDeletion_InTrans(ByVal SearchCode As String, ByVal Conn As System.Data.SqlClient.SqlConnection, ByVal Cmd As System.Data.SqlClient.SqlCommand) Handles Me.BaseEvent_ApproveDeletion_InTrans
        Dim I As Integer = 0
        For I = 0 To Dgl1.Rows.Count - 1
            If Dgl1.Item(Col1Item_UID, I).Tag <> "" Then
                AgTemplate.ClsMain.FUpdateItem_UidOnDelete(Dgl1.Item(Col1Item_UID, I).Tag, mSearchCode, Conn, Cmd)
            End If
        Next


        mQry = " Delete From Stock Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " Delete From JobIssRecUid Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    End Sub

    Private Sub Frm_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "StockHead"
        LogTableName = "StockHead_Log"
        MainLineTableCsv = "Stock,StockHeadDetail"
        LogLineTableCsv = "Stock_LOG,StockHeadDetail_Log"
        AgL.GridDesign(Dgl1)
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mCondStr$
        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) & _
                        " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "H.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"


        If IsApplyVTypePermission Then
            mCondStr = mCondStr & " And H.V_Type In (Select V_Type From User_VType_Permission VP Where VP.UserName = '" & AgL.PubUserName & "' And VP.Div_Code = '" & AgL.PubDivCode & "' And VP.Site_Code = '" & AgL.PubSiteCode & "') "
        End If


        AgL.PubFindQry = " SELECT H.DocID AS SearchCode, H.V_Type AS Transfer_Type, H.V_Date AS Transfer_Date, " & _
                " H.ManualRefNo As Transfer_No, Gf.Description As From_Godown, Gt.Description As To_Godown,  " & _
                " H.Remarks,  H.EntryBy AS [Entry_By], H.EntryDate AS [Entry_Date], H.EntryType AS [Entry_Type]  " & _
                " FROM  StockHead H  " & _
                " LEFT JOIN Division D ON D.Div_Code=H.Div_Code  " & _
                " LEFT JOIN Process P ON H.Process=P.NCat  " & _
                " LEFT JOIN Subgroup Sg ON H.SubCode=Sg.SubCode  " & _
                " LEFT JOIN SiteMast SM ON SM.Code=H.Site_Code  " & _
                " LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type " & _
                " LEFT JOIN Godown GF ON GF.Code = H.FromGodown  " & _
                " LEFT JOIN Godown GT ON GT.Code = H.ToGodown  " & _
                " Where 1 = 1 " & mCondStr

        AgL.PubFindQryOrdBy = "[Entry Date]"
    End Sub

    Private Sub FrmQuality1_BaseFunction_FIniMast(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMast
        Dim mCondStr$
        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) & _
                        " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "H.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        If IsApplyVTypePermission Then
            mCondStr = mCondStr & " And H.V_Type In (Select V_Type From User_VType_Permission VP Where VP.UserName = '" & AgL.PubUserName & "' And VP.Div_Code = '" & AgL.PubDivCode & "' And VP.Site_Code = '" & AgL.PubSiteCode & "') "
        End If

        mQry = " Select H.DocID As SearchCode " & _
            " From StockHead H " & _
            " Left Join Voucher_Type Vt On H.V_Type = Vt.V_Type  " & _
            " Where 1 = 1 " & mCondStr & "  Order By H.V_Date, H.V_No  "

        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub FrmSaleOrder_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl1, Col1Item_UID, 100, 0, Col1Item_UID, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_ItemUID")), Boolean), False, False)
            .AddAgTextColumn(Dgl1, Col1ItemCode, 100, 0, Col1ItemCode, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_ItemCode")), Boolean), False, False)
            .AddAgTextColumn(Dgl1, Col1Item, 250, 0, Col1Item, True, False, False)
            .AddAgTextColumn(Dgl1, Col1Dimension1, 100, 0, AgTemplate.ClsMain.FGetDimension1Caption(), CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Dimension1")), Boolean), False)
            .AddAgTextColumn(Dgl1, Col1Dimension2, 100, 0, AgTemplate.ClsMain.FGetDimension2Caption(), CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Dimension2")), Boolean), False)
            .AddAgTextColumn(Dgl1, Col1Specification, 100, 0, Col1Specification, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Specification")), Boolean), False, False)
            .AddAgTextColumn(Dgl1, Col1Process, 100, 0, Col1Process, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_ProcessLine")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_ProcessLine")), Boolean), False)
            .AddAgTextColumn(Dgl1, Col1LotNo, 100, 0, Col1LotNo, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_LotNo")), Boolean), False, False)
            .AddAgTextColumn(Dgl1, Col1BaleNo, 100, 0, Col1BaleNo, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_BaleNo")), Boolean), False, False)
            .AddAgNumberColumn(Dgl1, Col1CurrentStock, 80, 8, 4, False, Col1CurrentStock, False, True, True)
            .AddAgNumberColumn(Dgl1, Col1Qty, 100, 8, 4, False, Col1Qty, True, False, True)
            .AddAgTextColumn(Dgl1, Col1Unit, 50, 0, Col1Unit, True, True, False)
            .AddAgTextColumn(Dgl1, Col1QtyDecimalPlaces, 50, 0, Col1QtyDecimalPlaces, False, True, False)
            .AddAgNumberColumn(Dgl1, Col1MeasurePerPcs, 70, 8, 3, False, Col1MeasurePerPcs, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_MeasurePerPcs")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_MeasurePerPcs")), Boolean), True)
            .AddAgNumberColumn(Dgl1, Col1TotalMeasure, 70, 8, 3, False, Col1TotalMeasure, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Measure")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_Measure")), Boolean), True)
            .AddAgTextColumn(Dgl1, Col1MeasureUnit, 50, 0, Col1MeasureUnit, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_MeasureUnit")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_MeasureUnit")), Boolean))
            .AddAgTextColumn(Dgl1, Col1MeasureDecimalPlaces, 50, 0, Col1MeasureDecimalPlaces, False, True, False)
            .AddAgNumberColumn(Dgl1, Col1Rate, 90, 8, 2, False, Col1Rate, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Rate")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_Rate")), Boolean), True)
            .AddAgNumberColumn(Dgl1, Col1Amount, 90, 8, 2, False, Col1Amount, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Amount")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_Amount")), Boolean), True)
            .AddAgTextColumn(Dgl1, Col1Remarks, 250, 0, Col1Remarks, True, False, False)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False

        Dgl1.ColumnHeadersHeight = 35

        Dgl1.AgSkipReadOnlyColumns = True
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As System.Data.SqlClient.SqlConnection, ByVal Cmd As System.Data.SqlClient.SqlCommand) Handles Me.BaseEvent_Save_InTrans
        Dim I As Integer, mSr As Integer
        Dim bSelectionQry$ = ""
        mQry = "UPDATE StockHead " & _
                " SET " & _
                " FromGodown = " & AgL.Chk_Text(TxtFromGodown.Tag) & ", " & _
                " ToGodown = " & AgL.Chk_Text(TxtToGodown.Tag) & ", " & _
                " ManualRefNo = " & AgL.Chk_Text(TxtManualRefNo.Text) & ", " & _
                " Remarks = " & AgL.Chk_Text(TxtRemarks.Text) & " " & _
                " Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        If Topctrl1.Mode <> "Add" Then
            mQry = " SELECT Item_UID FROM StockHeadDetail With (NoLock) WHERE DocId = '" & mSearchCode & "' And Item_Uid Is Not Null "
            Dim DtItem_Uid As DataTable = AgL.FillData(mQry, AgL.GcnRead).Tables(0)
            If DtItem_Uid.Rows.Count > 0 Then
                For I = 0 To DtItem_Uid.Rows.Count - 1
                    AgTemplate.ClsMain.FUpdateItem_UidOnDelete(DtItem_Uid.Rows(I)("Item_Uid"), mSearchCode, Conn, Cmd)
                Next
            End If
        End If

        mSr = AgL.VNull(AgL.Dman_Execute("Select Max(Sr) From StockHeadDetail With (NoLock) Where DocID = '" & mSearchCode & "'", AgL.GcnRead).ExecuteScalar)
        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1Item, I).Value <> "" Then
                If Dgl1.Item(ColSNo, I).Tag Is Nothing And Dgl1.Rows(I).Visible = True Then
                    mSr += 1
                    If bSelectionQry <> "" Then bSelectionQry += " UNION ALL "
                    bSelectionQry += " Select " & AgL.Chk_Text(mSearchCode) & ", " & mSr & ", " & _
                                " " & AgL.Chk_Text(Dgl1.Item(Col1Item_UID, I).Tag) & ", " & _
                                " " & AgL.Chk_Text(Dgl1.AgSelectedValue(Col1Item, I)) & ", " & _
                                " " & AgL.Chk_Text(Dgl1.Item(Col1Dimension1, I).Tag) & ", " & _
                                " " & AgL.Chk_Text(Dgl1.Item(Col1Dimension2, I).Tag) & ", " & _
                                " " & AgL.Chk_Text(Dgl1.Item(Col1Specification, I).Value) & ",  " & _
                                " " & AgL.Chk_Text(Dgl1.Item(Col1LotNo, I).Value) & ",  " & _
                                " " & AgL.Chk_Text(Dgl1.Item(Col1BaleNo, I).Value) & ",  " & _
                                " " & AgL.Chk_Text(TxtFromGodown.AgSelectedValue) & ", " & _
                                " " & Val(Dgl1.Item(Col1Qty, I).Value) & ", " & _
                                " " & AgL.Chk_Text(Dgl1.Item(Col1Unit, I).Value) & ",  " & _
                                " " & Val(Dgl1.Item(Col1MeasurePerPcs, I).Value) & ", " & _
                                " " & Val(Dgl1.Item(Col1TotalMeasure, I).Value) & ", " & _
                                " " & AgL.Chk_Text(Dgl1.Item(Col1MeasureUnit, I).Value) & ", " & _
                                " " & Val(Dgl1.Item(Col1Rate, I).Value) & ", " & _
                                " " & Val(Dgl1.Item(Col1Amount, I).Value) & ", " & _
                                " " & AgL.Chk_Text(Dgl1.Item(Col1Remarks, I).Value) & ", " & _
                                " " & AgL.Chk_Text(Dgl1.Item(Col1Process, I).Tag) & ", " & _
                                " " & Val(Dgl1.Item(Col1CurrentStock, I).Value) & ""
                Else
                    If Dgl1.Rows(I).Visible = True Then
                        If Dgl1.Rows(I).DefaultCellStyle.BackColor <> RowLockedColour Then
                            mQry = " UPDATE StockHeadDetail " & _
                                    " SET " & _
                                    " Item_UID = " & AgL.Chk_Text(Dgl1.Item(Col1Item_UID, I).Tag) & ", " & _
                                    " Item = " & AgL.Chk_Text(Dgl1.Item(Col1Item, I).Tag) & ", " & _
                                    " Dimension1 = " & AgL.Chk_Text(Dgl1.Item(Col1Dimension1, I).Tag) & ", " & _
                                    " Dimension2 = " & AgL.Chk_Text(Dgl1.Item(Col1Dimension2, I).Tag) & ", " & _
                                    " Specification = " & AgL.Chk_Text(Dgl1.Item(Col1Specification, I).Value) & ", " & _
                                    " LotNo = " & AgL.Chk_Text(Dgl1.Item(Col1LotNo, I).Value) & ", " & _
                                    " BaleNo = " & AgL.Chk_Text(Dgl1.Item(Col1BaleNo, I).Value) & ", " & _
                                    " Qty = " & Val(Dgl1.Item(Col1Qty, I).Value) & ", " & _
                                    " Unit = " & AgL.Chk_Text(Dgl1.Item(Col1Unit, I).Value) & ", " & _
                                    " MeasurePerPcs = " & Val(Dgl1.Item(Col1MeasurePerPcs, I).Value) & ", " & _
                                    " MeasureUnit = " & AgL.Chk_Text(Dgl1.Item(Col1MeasureUnit, I).Value) & ", " & _
                                    " TotalMeasure = " & Val(Dgl1.Item(Col1TotalMeasure, I).Value) & ", " & _
                                    " Rate = " & Val(Dgl1.Item(Col1Rate, I).Value) & ", " & _
                                    " Amount = " & Val(Dgl1.Item(Col1Amount, I).Value) & ", " & _
                                    " Remarks = " & AgL.Chk_Text(Dgl1.Item(Col1Remarks, I).Value) & ", " & _
                                    " Process = " & AgL.Chk_Text(Dgl1.Item(Col1Process, I).Tag) & ", " & _
                                    " CurrentStock = " & Val(Dgl1.Item(Col1CurrentStock, I).Value) & " " & _
                                    " Where DocId = '" & mSearchCode & "' " & _
                                    " And Sr = " & Dgl1.Item(ColSNo, I).Tag & " "
                            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                        End If
                    Else
                        mQry = " Delete From StockHeadDetail Where DocId = '" & mSearchCode & "' And Sr = " & Val(Dgl1.Item(ColSNo, I).Tag) & "  "
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                    End If
                End If
            End If
        Next

        If bSelectionQry <> "" Then
            mQry = " INSERT INTO StockHeadDetail ( DocID, Sr, Item_UID, Item, Dimension1, Dimension2, Specification, LotNo, BaleNo, Godown, Qty, Unit, " & _
                    " MeasurePerPcs, TotalMeasure, MeasureUnit, Rate, Amount, Remarks, Process, " & _
                    " CurrentStock) " & bSelectionQry
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If

        'If AgL.StrCmp(TxtV_Type.Tag, "CTRF") Then
        '    'FPostStockProcessWise(mSearchCode, Conn, Cmd)

        '    'Code For Stock Posting Process Wise
        '    Dim StockView$ = ""
        '    StockView = " Select L.DocID, L.Sr, H.V_Type, " & _
        '                " H.V_Prefix, H.V_Date, H.V_No, H.ManualRefNo As RecId, H.Div_Code, " & _
        '                " H.Site_Code,   " & _
        '                " H.SubCode As SubCode, L.Item, H.FromGodown As Godown, L.Qty, L.Unit, L.MeasurePerPcs, " & _
        '                " L.TotalMeasure, L.MeasureUnit, L.Process As Process " & _
        '                " From StockHead As H With (NoLock)  " & _
        '                " LEFT JOIN StockHeadDetail As L With (NoLock) On H.DocId = L.DocId " & _
        '                " Where H.DocId = '" & mInternalCode & "' "
        '    AgTemplate.ClsMain.FPostInStockWithProcess(StockView, mInternalCode, TxtFromGodown.Tag, TxtV_Date.Text, Conn, Cmd)


        '    Dim mToGodownDiv_Code$ = ""
        '    mQry = " Select Div_Code From Godown G With (NoLock) Where G.Code = '" & TxtToGodown.AgSelectedValue & "'"
        '    mToGodownDiv_Code = AgL.XNull(AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar)

        '    Dim mMaxSr As Integer = 0
        '    mMaxSr = AgL.VNull(AgL.Dman_Execute(" Select Max(Sr) From Stock With (NoLock) Where DocId = '" & mSearchCode & "'", AgL.GcnRead).ExecuteScalar)
        '    mQry = " INSERT INTO Stock (DocId, Sr, V_Type, V_Prefix, " & _
        '            " V_Date, V_No, RecID, Div_Code, Site_Code, " & _
        '            " Item_UID, Item, Dimension1, Dimension2, Godown, Qty_Iss, Qty_Rec, Unit, " & _
        '            " MeasurePerPcs, Measure_Iss, Measure_Rec, MeasureUnit, " & _
        '            " Rate, Amount, LotNo, BaleNo, Process, Remarks) " & _
        '            " Select DocId, Row_Number() Over (Order By Sr) + " & mMaxSr & ", V_Type, V_Prefix, " & _
        '            " V_Date, V_No, RecID, '" & mToGodownDiv_Code & "', Site_Code, " & _
        '            " Item_UID, Item, Dimension1, Dimension2, '" & TxtToGodown.Tag & "', Qty_Rec, Qty_Iss, Unit, " & _
        '            " MeasurePerPcs, Measure_Rec, Measure_Iss , MeasureUnit, " & _
        '            " Rate, Amount, LotNo, BaleNo, Process, Remarks " & _
        '            " From Stock " & _
        '            " Where DocId = '" & mSearchCode & "'"
        '    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        '    'Code End For Stock Posting Process Wise
        'Else
        '    FPostInStock(mSearchCode, Conn, Cmd)
        'End If

        '-------------Qry Was Written For Managing Process Wise Stock For Surya Carpet
        'But After physical stock it is turned into normal Stock Posting

        FPostInStock(mSearchCode, Conn, Cmd)
        FPostInJobIssRecUID(mSearchCode, Conn, Cmd)

        For I = 0 To Dgl1.Rows.Count - 1
            If Dgl1.Item(Col1Item_UID, I).Tag <> "" Then
                AgTemplate.ClsMain.FUpdateItem_Uid(Dgl1.Item(Col1Item_UID, I).Tag, Topctrl1.Mode, mSearchCode, TxtV_Type.Tag, TxtV_Date.Text, "", TxtToGodown.Tag, "", AgTemplate.ClsMain.Item_UidStatus.Receive, TxtManualRefNo.Text, Conn, Cmd)
            End If
        Next
    End Sub

    Private Sub FPostInJobIssRecUID(ByVal SearchCode As String, ByVal Conn As System.Data.SqlClient.SqlConnection, ByVal Cmd As System.Data.SqlClient.SqlCommand)
        Dim I As Integer = 0, bSr As Integer = 0

        mQry = "Delete from JobIssRecUID Where DocId ='" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " INSERT INTO JobIssRecUID(DocID, TSr, Sr, IssRec, Process, Item, Item_UID, " & _
                 " Godown, Site_Code, V_Date, V_Type, SubCode, Div_Code, RecId, EntryDate, Remark, JobRecDocID) " & _
                 " Select L.DocId, L.Sr As TSr, L.Sr, 'I', " & _
                 " L.Process, L.Item, L.Item_Uid, " & _
                 " H.FromGodown, H.Site_Code, H.V_Date, H.V_Type, H.SubCode, H.Div_Code, H.ManualRefNo, H.EntryDate, " & _
                 " SubString(IsNull(H.Remarks,'') + '.' + IsNull(L.Remarks,''),0,255), L.DocId " & _
                 " From (Select * From StockHeadDetail With (NoLock) Where DocId = '" & mSearchCode & "' And Item_Uid Is Not Null) As L " & _
                 " LEFT JOIN StockHead H With (NoLock) On L.DocId = H.DocId "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        Dim mMaxSr As Integer = AgL.VNull(AgL.Dman_Execute("Select Max(L.Sr) From StockHeadDetail L With (NoLock) Where L.DocId = '" & mSearchCode & "'", AgL.GcnRead).ExecuteScalar)

        mQry = " INSERT INTO JobIssRecUID(DocID, TSr, Sr, IssRec, Process, Item, Item_UID, " & _
                 " Godown, Site_Code, V_Date, V_Type, SubCode, Div_Code, RecId, EntryDate, Remark, JobRecDocID) " & _
                 " Select L.DocId, Row_Number() Over (Order By L.Item) + " & mMaxSr & " As TSr, " & _
                 " Row_Number() Over (Order By L.Item) + " & mMaxSr & " As Sr, 'R', " & _
                 " L.Process, L.Item, L.Item_Uid, " & _
                 " H.ToGodown, H.Site_Code, H.V_Date, H.V_Type, H.SubCode, H.Div_Code, H.ManualRefNo, H.EntryDate, " & _
                 " SubString(IsNull(H.Remarks,'') + '.' + IsNull(L.Remarks,''),0,255), L.DocId  " & _
                 " From (Select * From StockHeadDetail With (NoLock) Where DocId = '" & mSearchCode & "' And Item_Uid Is Not Null) As L " & _
                 " LEFT JOIN StockHead H With (NoLock) On L.DocId = H.DocId "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    End Sub

    Private Sub FrmProductionOrder_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim I As Integer
        Dim DsTemp As DataSet

        mQry = "Select H.*, Fg.Description as FromGodownDesc, Tg.Description as ToGodownDesc, " & _
                " Sg.Name + (Case When IsNull(Sg.CityCode,'')<>'' Then ', ' + C.CityName Else '' End) as PartyName " & _
                " From StockHead H " & _
                " Left Join Godown Fg on H.FromGodown = Fg.Code " & _
                " Left Join Godown Tg on H.ToGodown = Tg.Code " & _
                " Left Join Subgroup Sg on H.SubCode = Sg.SubCode " & _
                " Left Join City C on Sg.CityCode = C.CityCode " & _
                " Where H.DocID ='" & SearchCode & "'"
        DsTemp = AgL.FillData(mQry, AgL.GCn)


        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                TxtFromGodown.Tag = AgL.XNull(.Rows(0)("FromGodown"))
                TxtFromGodown.Text = AgL.XNull(.Rows(0)("FromGodownDesc"))
                TxtToGodown.Tag = AgL.XNull(.Rows(0)("ToGodown"))
                TxtToGodown.Text = AgL.XNull(.Rows(0)("ToGodownDesc"))
                TxtManualRefNo.Text = AgL.XNull(.Rows(0)("ManualRefNo"))
                TxtRemarks.Text = AgL.XNull(.Rows(0)("Remarks"))
                LblTotalQty.Text = AgL.VNull(.Rows(0)("TotalQty"))
                LblTotalMeasure.Text = AgL.VNull(.Rows(0)("TotalMeasure"))
                IniGrid()
                '-------------------------------------------------------------
                'Line Records are showing in Grid
                '-------------------------------------------------------------

                mQry = "Select S.*, I.ManualCode as Item_No, I.Description as Item_Desc, " & _
                       " U.DecimalPlaces as QtyDecimalPlaces, MU.DecimalPlaces as MeasureDecimalPlaces, " & _
                       " P.Description as Process_Desc, IU.Item_UID as Item_UID_Desc, " & _
                       " D1.Description As Dimension1Desc, D2.Description As Dimension2Desc " & _
                       " From (Select * From StockHeadDetail where DocId = '" & SearchCode & "') S " & _
                       " Left Join Item I On S.Item = I.Code " & _
                       " Left Join Unit U On I.Unit = U.Code " & _
                       " Left Join Unit MU On I.MeasureUnit = MU.Code " & _
                       " Left Join Dimension1 D1 With (Nolock)  On S.Dimension1 = D1.Code " & _
                       " Left Join Dimension2 D2 With (Nolock)  On S.Dimension2 = D2.Code " & _
                       " Left Join Item_UID IU On S.Item_UID = IU.Code " & _
                       " Left Join Process P On S.Process = P.NCat " & _
                       " Order By Sr"
                DsTemp = AgL.FillData(mQry, AgL.GCn)
                With DsTemp.Tables(0)
                    Dgl1.RowCount = 1
                    Dgl1.Rows.Clear()
                    If .Rows.Count > 0 Then
                        For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                            Dgl1.Rows.Add()
                            Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count - 1
                            Dgl1.Item(ColSNo, I).Tag = AgL.VNull(.Rows(I)("Sr"))
                            Dgl1.Item(Col1Item_UID, I).Tag = AgL.XNull(.Rows(I)("Item_UID"))
                            Dgl1.Item(Col1Item_UID, I).Value = AgL.XNull(.Rows(I)("Item_UID_Desc"))
                            Dgl1.Item(Col1ItemCode, I).Tag = AgL.XNull(.Rows(I)("Item"))
                            Dgl1.Item(Col1ItemCode, I).Value = AgL.XNull(.Rows(I)("Item_No"))
                            Dgl1.Item(Col1Item, I).Tag = AgL.XNull(.Rows(I)("Item"))
                            Dgl1.Item(Col1Item, I).Value = AgL.XNull(.Rows(I)("Item_Desc"))

                            Dgl1.Item(Col1Dimension1, I).Tag = AgL.XNull(.Rows(I)("Dimension1"))
                            Dgl1.Item(Col1Dimension1, I).Value = AgL.XNull(.Rows(I)("Dimension1Desc"))

                            Dgl1.Item(Col1Dimension2, I).Tag = AgL.XNull(.Rows(I)("Dimension2"))
                            Dgl1.Item(Col1Dimension2, I).Value = AgL.XNull(.Rows(I)("Dimension2Desc"))

                            Dgl1.Item(Col1Specification, I).Value = AgL.XNull(.Rows(I)("Specification"))
                            Dgl1.Item(Col1Qty, I).Value = Format(AgL.VNull(.Rows(I)("Qty")), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                            Dgl1.Item(Col1QtyDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("QtyDecimalPlaces"))
                            Dgl1.Item(Col1MeasurePerPcs, I).Value = Format(AgL.VNull(.Rows(I)("MeasurePerPcs")), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1TotalMeasure, I).Value = Format(AgL.VNull(.Rows(I)("TotalMeasure")), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                            Dgl1.Item(Col1MeasureUnit, I).Value = AgL.XNull(.Rows(I)("MeasureUnit"))
                            Dgl1.Item(Col1MeasureDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("MeasureDecimalPlaces"))
                            Dgl1.Item(Col1Rate, I).Value = AgL.VNull(.Rows(I)("Rate"))
                            Dgl1.Item(Col1Amount, I).Value = AgL.VNull(.Rows(I)("Amount"))
                            Dgl1.Item(Col1LotNo, I).Value = AgL.XNull(.Rows(I)("LotNo"))
                            Dgl1.Item(Col1BaleNo, I).Value = AgL.XNull(.Rows(I)("BaleNo"))
                            Dgl1.Item(Col1Process, I).Tag = AgL.XNull(.Rows(I)("Process"))
                            Dgl1.Item(Col1Process, I).Value = AgL.XNull(.Rows(I)("Process_Desc"))
                            Dgl1.Item(Col1Remarks, I).Value = AgL.XNull(.Rows(I)("Remarks"))
                            Dgl1.Item(Col1CurrentStock, I).Value = AgL.VNull(.Rows(I)("CurrentStock"))
                        Next I
                    End If
                End With
                Calculation()
                '-------------------------------------------------------------
            End If
        End With
        BtnImprtFromText.Tag = Nothing
    End Sub

    Private Sub FrmProductionOrder_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Topctrl1.ChangeAgGridState(Dgl1, False)
    End Sub

    Private Sub TxtFromGodown_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtFromGodown.KeyDown, TxtToGodown.KeyDown
        Select Case sender.Name
            Case TxtFromGodown.Name
                If e.KeyCode <> Keys.Enter Then
                    If sender.AgHelpDataset Is Nothing Then
                        mQry = "SELECT G.Code, G.Description " & _
                                " FROM Godown G " & _
                                " LEFT JOIN SiteMast Sm On G.Site_Code = Sm.Code  " & _
                                " Where G.Site_Code = '" & TxtSite_Code.AgSelectedValue & "'  " & _
                                " And IsNull(G.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & _
                                " Order By G.Description "
                        sender.AgHelpDataset(0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
                    End If
                End If

            Case TxtToGodown.Name
                If e.KeyCode <> Keys.Enter Then
                    If sender.AgHelpDataset Is Nothing Then
                        mQry = "SELECT G.Code, G.Description " & _
                                " FROM Godown G " & _
                                " Where IsNull(G.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & _
                                " Order By G.Description "
                        sender.AgHelpDataset(0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
                    End If
                End If
        End Select
    End Sub

    Private Sub Txt_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtV_Type.Validating, TxtManualRefNo.Validating, TxtFromGodown.Validating
        Try
            Select Case sender.NAME
                Case TxtV_Type.Name
                    TxtManualRefNo.Text = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "StockHead", TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, AgTemplate.ClsMain.ManualRefType.Max)
                    IniGrid()
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_Topctrl_tbAdd() Handles Me.BaseEvent_Topctrl_tbAdd
        TxtManualRefNo.Text = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "StockHead", TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, AgTemplate.ClsMain.ManualRefType.Max)
        If AgL.XNull(DtV_TypeSettings.Rows(0)("Default_Godown")) <> "" Then
            TxtFromGodown.Tag = AgL.XNull(DtV_TypeSettings.Rows(0)("Default_Godown"))
            TxtFromGodown.Text = AgL.Dman_Execute("Select Description from Godown Where Code = '" & AgL.XNull(DtV_TypeSettings.Rows(0)("Default_Godown")) & "' ", AgL.GCn).ExecuteScalar
        End If
    End Sub

    Private Sub Dgl1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellEnter
        If Dgl1.CurrentCell Is Nothing Then Exit Sub
        Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
            Case Col1Qty
                CType(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex), AgControls.AgTextColumn).AgNumberRightPlaces = Val(Dgl1.Item(Col1QtyDecimalPlaces, Dgl1.CurrentCell.RowIndex).Value)
            Case Col1MeasurePerPcs, Col1TotalMeasure
                CType(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex), AgControls.AgTextColumn).AgNumberRightPlaces = Val(Dgl1.Item(Col1MeasureDecimalPlaces, Dgl1.CurrentCell.RowIndex).Value)
            Case Col1LotNo
                Dgl1.AgHelpDataSet(Col1LotNo) = Nothing
            Case Col1Process
                If Dgl1.Item(Col1Item_UID, Dgl1.CurrentCell.RowIndex).Value <> "" Then
                    Dgl1.Item(Col1Process, Dgl1.CurrentCell.RowIndex).ReadOnly = True
                Else
                    Dgl1.Item(Col1Process, Dgl1.CurrentCell.RowIndex).ReadOnly = False
                End If
        End Select
    End Sub

    Private Sub Dgl1_EditingControl_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Dgl1.EditingControl_Validating
        If Topctrl1.Mode = "Browse" Then Exit Sub
        Dim mRowIndex As Integer, mColumnIndex As Integer
        Dim DrTemp As DataRow() = Nothing
        Dim I As Integer = 0
        Dim ErrMsgStr$ = ""

        Try
            mRowIndex = Dgl1.CurrentCell.RowIndex
            mColumnIndex = Dgl1.CurrentCell.ColumnIndex
            If Dgl1.Item(mColumnIndex, mRowIndex).Value Is Nothing Then Dgl1.Item(mColumnIndex, mRowIndex).Value = ""
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Item_UID
                    ErrMsgStr = FCheck_Item_UID(Dgl1.Item(Col1Item_UID, mRowIndex).Value)
                    If ErrMsgStr <> "" Then
                        MsgBox(ErrMsgStr)
                        Dgl1.Item(Col1Item_UID, Dgl1.CurrentCell.RowIndex).Value = ""
                        Dgl1.Item(Col1Item_UID, Dgl1.CurrentCell.RowIndex).Tag = ""
                        Exit Sub
                    End If
                    Validating_Item_Uid(Dgl1.Item(Col1Item_UID, mRowIndex).Value, mRowIndex)

                Case Col1Item
                    Validating_ItemCode(mColumnIndex, mRowIndex)
                    'FCheckDuplicate(mRowIndex)
                Case Col1ItemCode
                    Validating_ItemCode(mColumnIndex, mRowIndex)
                Case Col1LotNo
                    LblCurrentStock.Text = Format(AgTemplate.ClsMain.FunRetStock(Dgl1.Item(Col1Item, Dgl1.CurrentCell.RowIndex).Tag, mSearchCode, , TxtFromGodown.Tag, , , TxtV_Date.Text, Dgl1.Item(Col1LotNo, Dgl1.CurrentCell.RowIndex).Value), "0.".PadRight(Dgl1.Item(Col1QtyDecimalPlaces, Dgl1.CurrentCell.RowIndex).Value + 2, "0"))

                Case Col1Process
                    If Dgl1.Item(Col1Process, mRowIndex).Value <> "" Then
                        If MsgBox("Apply To All ?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2) = MsgBoxResult.Yes Then
                            For I = mRowIndex To Dgl1.Rows.Count - 1
                                If Dgl1.Item(Col1Item_UID, I).Value = "" And Dgl1.Item(Col1Item, I).Value <> "" Then
                                    Dgl1.Item(Col1Process, I).Tag = Dgl1.Item(Col1Process, mRowIndex).Tag
                                    Dgl1.Item(Col1Process, I).Value = Dgl1.Item(Col1Process, mRowIndex).Value
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

    Private Sub Validating_ItemCode(ByVal mColumn As Integer, ByVal mRow As Integer)
        Dim DrTemp As DataRow() = Nothing
        Dim DtTemp As DataTable = Nothing
        Try
            If Dgl1.Item(mColumn, mRow).Value.ToString.Trim = "" Or Dgl1.AgSelectedValue(mColumn, mRow).ToString.Trim = "" Then
                Dgl1.Item(Col1Unit, mRow).Value = ""
                Dgl1.Item(Col1CurrentStock, mRow).Value = ""
            Else
                If Dgl1.AgDataRow IsNot Nothing Then
                    Dgl1.Item(Col1Item, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("Item").Value)
                    Dgl1.Item(Col1Item, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("ItemDesc").Value)
                    Dgl1.Item(Col1Qty, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("Qty").Value)
                    Dgl1.Item(Col1ItemCode, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("Item").Value)
                    Dgl1.Item(Col1ItemCode, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("ItemCode").Value)
                    Dgl1.Item(Col1Unit, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("Unit").Value)

                    Dgl1.Item(Col1LotNo, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("LotNo").Value)
                    Dgl1.Item(Col1Process, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("Process").Value)
                    Dgl1.Item(Col1Process, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("ProcessCode").Value)
                    Dgl1.Item(Col1Dimension1, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("Dimension1").Value)
                    Dgl1.Item(Col1Dimension1, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells(AgTemplate.ClsMain.FGetDimension1Caption()).Value)
                    Dgl1.Item(Col1Dimension2, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("Dimension2").Value)
                    Dgl1.Item(Col1Dimension2, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells(AgTemplate.ClsMain.FGetDimension2Caption()).Value)

                    If CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Rate")), Boolean) Or CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsMandatory_Rate")), Boolean) Then
                        Dgl1.Item(Col1Rate, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("Rate").Value)
                    End If
                    Dgl1.Item(Col1QtyDecimalPlaces, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("QtyDecimalPlaces").Value)
                    Dgl1.Item(Col1MeasurePerPcs, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("MeasurePerPcs").Value)
                    Dgl1.Item(Col1MeasureUnit, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("MeasureUnit").Value)
                    Dgl1.Item(Col1MeasureDecimalPlaces, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("MeasureDecimalPlaces").Value)
                    Dgl1.Item(Col1CurrentStock, mRow).Value = AgTemplate.ClsMain.FunRetStock(Dgl1.AgSelectedValue(Col1ItemCode, mRow), mSearchCode, , TxtFromGodown.AgSelectedValue, , , TxtV_Date.Text, Dgl1.Item(Col1LotNo, Dgl1.CurrentCell.RowIndex).Value)
                    If Not AgL.StrCmp(Topctrl1.Mode, "Browse") Then
                        LblCurrentStock.Text = Format(AgTemplate.ClsMain.FunRetStock(Dgl1.Item(Col1Item, Dgl1.CurrentCell.RowIndex).Tag, mSearchCode, , TxtFromGodown.Tag, , , TxtV_Date.Text, Dgl1.Item(Col1LotNo, Dgl1.CurrentCell.RowIndex).Value), "0.".PadRight(Dgl1.Item(Col1QtyDecimalPlaces, Dgl1.CurrentCell.RowIndex).Value + 2, "0"))
                    End If
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

        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1Item, I).Value <> "" Then
                Dgl1.Item(Col1TotalMeasure, I).Value = Format(Val(Dgl1.Item(Col1Qty, I).Value) * Val(Dgl1.Item(Col1MeasurePerPcs, I).Value), "0.".PadRight(Val(Dgl1.Item(Col1MeasureDecimalPlaces, I).Value) + 2, "0"))
                If Val(Dgl1.Item(Col1Qty, I).Value) > 0 Then
                    Dgl1.Item(Col1Amount, I).Value = Format(Val(Dgl1.Item(Col1Qty, I).Value) * Val(Dgl1.Item(Col1Rate, I).Value), "0.00")
                End If

                LblTotalQty.Text = Val(LblTotalQty.Text) + Val(Dgl1.Item(Col1Qty, I).Value)
                LblTotalMeasure.Text = Val(LblTotalMeasure.Text) + Val(Dgl1.Item(Col1TotalMeasure, I).Value)
            End If
        Next
        LblTotalQty.Text = Format(Val(LblTotalQty.Text), "0.000")
        LblTotalMeasure.Text = Format(Val(LblTotalMeasure.Text), "0.000")
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        Dim I As Integer = 0
        Dim DtTemp As DataTable = Nothing
        Dim DtGodownSettings As DataTable = Nothing
        Dim mSelectionQry$ = ""

        If AgL.RequiredField(TxtFromGodown, "From Godown") Then passed = False : Exit Sub
        If AgL.RequiredField(TxtToGodown, "To Godown") Then passed = False : Exit Sub
        If AgCL.AgIsBlankGrid(Dgl1, Dgl1.Columns(Col1Item).Index) = True Then passed = False : Exit Sub
        If AgCL.AgIsDuplicate(Dgl1, "" & Dgl1.Columns(Col1Item).Index & "," & Dgl1.Columns(Col1Specification).Index & "," & Dgl1.Columns(Col1Item_UID).Index & "," & Dgl1.Columns(Col1LotNo).Index & "," & Dgl1.Columns(Col1Process).Index & "," & Dgl1.Columns(Col1Dimension1).Index & "," & Dgl1.Columns(Col1Dimension2).Index & "") = True Then passed = False : Exit Sub

        With Dgl1
            For I = 0 To .Rows.Count - 1
                If Dgl1.Rows(I).Visible Then
                    If .Item(Col1Item, I).Value <> "" Then
                        If Val(.Item(Col1Qty, I).Value) = 0 Then
                            MsgBox("Qty Is 0 At Row No " & Dgl1.Item(ColSNo, I).Value & "")
                            .CurrentCell = .Item(Col1Qty, I) : Dgl1.Focus()
                            passed = False : Exit Sub
                        End If

                        'If mSelectionQry <> "" Then mSelectionQry += " UNION ALL "
                        'mSelectionQry += "Select " & AgL.Chk_Text(Dgl1.Item(Col1Item, I).Tag) & ", " & _
                        '            " " & AgL.Chk_Text(Dgl1.Item(Col1LotNo, I).Value) & ", " & _
                        '            " " & Val(Dgl1.Item(Col1Qty, I).Value) & " "

                        If mSelectionQry <> "" Then mSelectionQry += " UNION ALL "
                        mSelectionQry += "Select " & AgL.Chk_Text(Dgl1.Item(Col1Item, I).Tag) & ", " & _
                                " " & AgL.Chk_Text(Dgl1.Item(Col1LotNo, I).Value) & ", " & _
                                " " & AgL.Chk_Text(Dgl1.Item(Col1Dimension1, I).Tag) & ", " & _
                                " " & AgL.Chk_Text(Dgl1.Item(Col1Dimension2, I).Tag) & ", " & _
                                " " & AgL.Chk_Text(Dgl1.Item(Col1Process, I).Tag) & ", " & _
                                " " & Val(Dgl1.Item(Col1Qty, I).Value) & " "

                        If CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsMandatory_ProcessLine")), Boolean) Then
                            If Dgl1.Item(Col1Process, I).Value = "" Then
                                MsgBox(" Process Is Required At Line No " & Dgl1.Item(ColSNo, I).Value & "")
                                Dgl1.CurrentCell = Dgl1.Item(Col1Process, I) : Dgl1.Focus()
                                passed = False : Exit Sub
                            End If
                        End If
                    End If
                End If
            Next
        End With

        If mSelectionQry <> "" Then
            'Selection Qry Contains Loop Genearted Selecion Qry String For Item And Its Quantity
            'For Example Select " & AgL.Chk_Text(Dgl1.Item(Col1Item, I).Tag) & ", " & Val(Dgl1.Item(Col1Qty, I).Value) & " 
            '
            passed = AgTemplate.ClsMain.FIsNegativeStock(mSelectionQry, mSearchCode, TxtFromGodown.Tag, TxtV_Date.Text)
        End If
    End Sub

    Private Sub FrmProductionOrder_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
        Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
        LblTotalMeasure.Text = 0 : LblTotalQty.Text = 0
    End Sub

    Private Sub DGL1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.KeyDown
        If e.Control And e.KeyCode = Keys.D Then
            sender.CurrentRow.Selected = True
            sender.CurrentRow.VISIBLE = False
        End If
        If e.Control Or e.Shift Or e.Alt Then Exit Sub
    End Sub

    Private Sub FrmYarnSKUOpeningStock_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AgL.WinSetting(Me, 566, 895)
    End Sub

    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub

    Private Sub Validating_Item_Uid(ByVal Item_Uid As String, ByVal mRow As Integer)
        Dim DrTemp As DataRow() = Nothing
        Dim DtTemp As DataTable = Nothing

        Try
            mQry = " SELECT I.Code, I.Description, I.Unit, I.ManualCode, I.Prod_Measure, I.MeasureUnit, I.Rate, " & _
                   " U.DecimalPlaces as QtyDecimalPlaces, MU.DecimalPlaces as MeasureDecimalPlaces, UI.Code as ItemUIDCode, UI.ProdOrder as ProdOrderDocID, PO.V_Type + '-' + PO.ManualRefNo as ProdOrderNo  " & _
                   " FROM (Select Item, Code, ProdOrder From Item_UID Where Item_Uid = '" & Dgl1.Item(Col1Item_UID, mRow).Value & "') UI " & _
                   " Left Join ProdOrder PO With (NoLock) On UI.ProdOrder = PO.DocID " & _
                   " Left Join Item I With (NoLock) On UI.Item  = I.Code " & _
                   " Left Join Unit U With (NoLock) On I.Unit = U.Code " & _
                   " Left Join Unit MU With (NoLock) On I.MeasureUnit = MU.Code "
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtTemp.Rows.Count > 0 Then
                Dgl1.Item(Col1Item_UID, mRow).Tag = AgL.XNull(DtTemp.Rows(0)("ItemUIDCode"))
                Dgl1.Item(Col1ItemCode, mRow).Tag = AgL.XNull(DtTemp.Rows(0)("Code"))
                Dgl1.Item(Col1ItemCode, mRow).Value = AgL.XNull(DtTemp.Rows(0)("ManualCode"))
                Dgl1.Item(Col1Item, mRow).Tag = AgL.XNull(DtTemp.Rows(0)("Code"))
                Dgl1.Item(Col1Item, mRow).Value = AgL.XNull(DtTemp.Rows(0)("Description"))
                Dgl1.Item(Col1Qty, mRow).Value = 1
                Dgl1.Item(Col1Unit, mRow).Value = AgL.XNull(DtTemp.Rows(0)("Unit"))
                If CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Rate")), Boolean) Or CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsMandatory_Rate")), Boolean) Then
                    Dgl1.Item(Col1Rate, mRow).Value = AgL.VNull(DtTemp.Rows(0)("Rate"))
                End If
                Dgl1.Item(Col1QtyDecimalPlaces, mRow).Value = AgL.VNull(DtTemp.Rows(0)("QtyDecimalPlaces"))
                Dgl1.Item(Col1MeasurePerPcs, mRow).Value = Format(AgL.VNull(DtTemp.Rows(0)("Prod_Measure")), "0.".PadRight(AgL.VNull(DtTemp.Rows(0)("MeasureDecimalPlaces")) + 2, "0"))
                Dgl1.Item(Col1TotalMeasure, mRow).Value = AgL.VNull(DtTemp.Rows(0)("Prod_Measure"))
                Dgl1.Item(Col1MeasureUnit, mRow).Value = AgL.XNull(DtTemp.Rows(0)("MeasureUnit"))
                Dgl1.Item(Col1MeasureDecimalPlaces, mRow).Value = AgL.VNull(DtTemp.Rows(0)("MeasureDecimalPlaces"))
                'Dgl1.Item(Col1CurrentStock, mRow).Value = AgTemplate.ClsMain.FunRetStock(Dgl1.AgSelectedValue(Col1ItemCode, mRow), mSearchCode, , TxtFromGodown.AgSelectedValue, , , TxtV_Date.Text)
                'mQry = "Select NCat, Description From Process Where NCat = (SELECT TOP 1 Process FROM Stock WHERE Item_UID  ='" & Dgl1.Item(Col1Item_UID, mRow).Tag & "' ORDER BY V_Date Desc)"
                'DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
                'If DtTemp.Rows.Count > 0 Then
                '    Dgl1.Item(Col1Process, mRow).Tag = AgL.XNull(DtTemp.Rows(0)("NCat"))
                '    Dgl1.Item(Col1Process, mRow).Value = AgL.XNull(DtTemp.Rows(0)("Description"))
                'End If

                mQry = " SELECT TOP 1 P.NCat As ProcessCode, P.Description As ProcessDesc " & _
                        " FROM JobIssRecUID L  " & _
                        " LEFT JOIN Process P ON L.Process = P.NCat " & _
                        " WHERE Item_UID = '" & Dgl1.Item(Col1Item_UID, mRow).Tag & "' " & _
                        " ORDER BY L.V_Date DESC, P.Sr DESC "
                Dim DtProcess As DataTable = AgL.FillData(mQry, AgL.GCn).Tables(0)
                If DtProcess.Rows.Count > 0 Then
                    Dgl1.Item(Col1Process, mRow).Tag = AgL.XNull(DtProcess.Rows(0)("ProcessCode"))
                    Dgl1.Item(Col1Process, mRow).Value = AgL.XNull(DtProcess.Rows(0)("ProcessDesc"))
                End If
            Else
                Dgl1.Item(Col1Item_UID, mRow).Tag = ""
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " On Validating_Item_Uid Function ")
        End Try
    End Sub

    Public Function FCheck_Item_UID(ByVal Item_UID As String) As String
        Dim Item_UidCode$ = "", ErrMsgStr$ = ""
        Dim DtTemp As DataTable = Nothing

        mQry = " SELECT Code FROM Item_UID With (NoLock) WHERE Item_UID = '" & Item_UID & "'"
        Item_UidCode = AgL.XNull(AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar)
        If Item_UidCode = "" Then
            FCheck_Item_UID = "Carpet Id Is Not Valid."
            Exit Function
        Else
            FCheck_Item_UID = ""
        End If

        mQry = " Select RecDocID From Item_Uid With (NoLock) Where Code = '" & Item_UidCode & "' "
        If AgL.XNull(AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar) = "" Then
            FCheck_Item_UID = "Carpet Id " & Item_UID & " Is Not Received From Weaving Process."
            Exit Function
        Else
            FCheck_Item_UID = ""
        End If


        mQry = " Select Item_Uid, ClosedRemark From Item_Uid With (NoLock) " & _
              " Where Code = '" & Item_UidCode & "' " & _
              " And ISNULL(IsClosed,0) = 1 "
        DtTemp = AgL.FillData(mQry, AgL.GcnRead).Tables(0)

        If DtTemp.Rows.Count > 0 Then
            FCheck_Item_UID = "Carpet Id " & Item_UID & " Is " & AgL.XNull(DtTemp.Rows(0)("ClosedRemark"))
            Exit Function
        Else
            FCheck_Item_UID = ""
        End If


    End Function


    Private Sub FCreateHelpItem(ByVal ColumnName As String)
        Dim strCond As String = ""
        Dim ContraV_TypeCondStr As String = ""

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
                strCond += " And CharIndex('|' + I.Item + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_Item")) & "') <= 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemDivision")) <> "" Then
                strCond += " And CharIndex('|' + I.Div_Code + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemDivision")) & "') > 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemSite")) <> "" Then
                strCond += " And CharIndex('|' + I.Site_Code + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemSite")) & "') > 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ContraV_Type")) <> "" Then
                ContraV_TypeCondStr += " And CharIndex('|' + V_Type + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ContraV_Type")) & "') > 0 "
            End If
        End If

        Select Case ColumnName
            Case Col1Item
                If RbtnForStock.Checked Then
                    mQry = " SELECT H.Item, Max(I.Description) AS ItemDesc, H.LotNo, " & _
                            " Max(D1.Description) AS " & AgTemplate.ClsMain.FGetDimension1Caption() & ", Max(D2.Description) AS " & AgTemplate.ClsMain.FGetDimension2Caption() & ", Max(P.Description) AS Process, Round(isnull(sum(H.Qty_Rec),0) - isnull(sum(H.Qty_Iss),0),4) AS Qty, " & _
                            " H.process AS ProcessCode, Max(I.Unit) AS Unit, H.Dimension1, H.Dimension2, Max(I.ManualCode) AS ItemCode, " & _
                            " Max(I.Measure) AS MeasurePerPcs, Max(I.MeasureUnit) AS MeasureUnit, Max(U.DecimalPlaces) AS QtyDecimalPlaces, Max(IG.Description) AS ItemGroupDesc, Max(MU.DecimalPlaces) as MeasureDecimalPlaces, Max(I.Rate) AS Rate, " & _
                            " NULL AS RequisitionNo, NULL AS RequisitionDocId, NULL AS RequisitionSr " & _
                            " FROM Stock H WITH (Nolock) " & _
                            " LEFT JOIN Item I WITH (Nolock) ON I.Code = H.Item  " & _
                            " LEFT JOIN Process P ON P.NCat = H.Process " & _
                            " LEFT JOIN Dimension1 D1 WITH (Nolock) ON D1.Code = H.Dimension1 " & _
                            " LEFT JOIN Dimension2 D2 WITH (Nolock) ON D2.Code = H.Dimension2  " & _
                            " LEFT JOIN ItemGroup IG On Ig.Code = I.ItemGroup" & _
                            " Left Join Unit U On I.Unit = U.Code " & _
                            " Left Join Unit MU On I.MeasureUnit = MU.Code " & _
                            " WHERE isnull(H.Item,'') <> ''  AND H.Godown = " & AgL.Chk_Text(TxtFromGodown.Tag) & " " & _
                            " AND H.V_Date <= " & AgL.Chk_Text(TxtV_Date.Text) & " AND H.DocID <> " & AgL.Chk_Text(mInternalCode) & " " & _
                            " And IsNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "'  " & strCond & _
                            " GROUP BY H.Item, H.LotNo, H.Process, H.Dimension1, H.Dimension2    " & _
                            " HAVING Round(isnull(sum(H.Qty_Rec),0) - isnull(sum(H.Qty_Iss),0),4) > 0 " & _
                            " Order By Max(I.Description) "
                    Dgl1.AgHelpDataSet(Col1Item, 13) = AgL.FillData(mQry, AgL.GCn)
                Else
                    mQry = " SELECT I.Code AS Item, I.Description AS ItemDesc, I.ManualCode AS ItemCode, I.Unit, " & _
                        " I.Measure AS MeasurePerPcs, I.MeasureUnit, U.DecimalPlaces as QtyDecimalPlaces, Null AS LotNo, IG.Description AS ItemGroupDesc, MU.DecimalPlaces as MeasureDecimalPlaces, I.Rate, " & _
                        " NULL AS ProcessCode, NULL AS Dimension1, NULL AS Dimension2, NULL AS " & AgTemplate.ClsMain.FGetDimension1Caption() & ", NULL AS " & AgTemplate.ClsMain.FGetDimension2Caption() & ", NULL AS Process, " & _
                        " NULL AS RequisitionNo, NULL AS RequisitionDocId, NULL AS RequisitionSr, 0 AS Qty " & _
                        " FROM Item I " & _
                        " LEFT JOIN ItemGroup IG On Ig.Code = I.ItemGroup" & _
                        " Left Join Unit U On I.Unit = U.Code " & _
                        " Left Join Unit MU On I.MeasureUnit = MU.Code " & _
                        " Where IsNull(I.IsDeleted ,0)  = 0 And " & _
                        " IsNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "')='" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
                    Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex, 17) = AgL.FillData(mQry, AgL.GCn)
                End If

            Case Col1ItemCode
                If RbtnForStock.Checked Then
                    mQry = " SELECT H.Item, Max(I.ManualCode) AS ItemCode, " & _
                            " Max(D1.Description) AS " & AgTemplate.ClsMain.FGetDimension1Caption() & ", Max(D2.Description) AS " & AgTemplate.ClsMain.FGetDimension2Caption() & ", Max(P.Description) AS Process, Round(isnull(sum(H.Qty_Rec),0) - isnull(sum(H.Qty_Iss),0),4) AS Qty, " & _
                            " H.process AS ProcessCode, Max(I.Unit) AS Unit, H.Dimension1, H.Dimension2, Max(I.Description) AS ItemDesc, " & _
                            " Max(I.Measure) AS MeasurePerPcs, Max(I.MeasureUnit) AS MeasureUnit, Max(U.DecimalPlaces) AS QtyDecimalPlaces, Max(IG.Description) AS ItemGroupDesc, Max(MU.DecimalPlaces) as MeasureDecimalPlaces, Max(I.Rate) AS Rate, " & _
                            " NULL AS RequisitionNo, NULL AS RequisitionDocId, NULL AS RequisitionSr " & _
                            " FROM Stock H WITH (Nolock) " & _
                            " LEFT JOIN Item I WITH (Nolock) ON I.Code = H.Item  " & _
                            " LEFT JOIN Process P ON P.NCat = H.Process " & _
                            " LEFT JOIN Dimension1 D1 WITH (Nolock) ON D1.Code = H.Dimension1 " & _
                            " LEFT JOIN Dimension2 D2 WITH (Nolock) ON D2.Code = H.Dimension2  " & _
                            " LEFT JOIN ItemGroup IG On Ig.Code = I.ItemGroup" & _
                            " Left Join Unit U On I.Unit = U.Code " & _
                            " Left Join Unit MU On I.MeasureUnit = MU.Code " & _
                            " WHERE isnull(H.Item,'') <> ''  AND H.Godown = " & AgL.Chk_Text(TxtFromGodown.Tag) & " " & _
                            " AND H.V_Date <= " & AgL.Chk_Text(TxtV_Date.Text) & " AND H.DocID <> " & AgL.Chk_Text(mInternalCode) & " " & _
                            " And IsNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "'  " & strCond & _
                            " GROUP BY H.Item, H.Process, H.Dimension1, H.Dimension2    " & _
                            " HAVING Round(isnull(sum(H.Qty_Rec),0) - isnull(sum(H.Qty_Iss),0),4) > 0 " & _
                            " Order By Max(I.Description) "
                    Dgl1.AgHelpDataSet(Col1Item, 13) = AgL.FillData(mQry, AgL.GCn)
                Else
                    mQry = " SELECT H.Code, H.ManualCode as Item_No, H.Description as Item_Name, H.Unit, " & _
                        " H.Measure, H.MeasureUnit, U.DecimalPlaces as QtyDecimalPlaces, IG.Description AS ItemDesc, MU.DecimalPlaces as MeasureDecimalPlaces, H.Rate " & _
                        " FROM Item H " & _
                        " LEFT JOIN ItemGroup IG On Ig.Code = H.ItemGroup" & _
                        " Left Join Unit U On H.Unit = U.Code " & _
                        " Left Join Unit MU On H.MeasureUnit = MU.Code " & _
                        " Where IsNull(H.IsDeleted ,0)  = 0 And " & _
                        " IsNull(H.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "')='" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
                    Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex, 4) = AgL.FillData(mQry, AgL.GCn)
                End If
        End Select
    End Sub

    Private Sub Dgl1_EditingControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.EditingControl_KeyDown
        Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
            Case Col1ItemCode
                If e.KeyCode <> Keys.Enter Then
                    If Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) Is Nothing Then
                        FCreateHelpItem(Col1ItemCode)
                    End If
                End If

            Case Col1Item
                If e.KeyCode <> Keys.Enter Then
                    If Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) Is Nothing Then
                        FCreateHelpItem(Col1Item)
                    End If
                End If

            Case Col1Process
                If e.KeyCode <> Keys.Enter Then
                    If Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) Is Nothing Then
                        mQry = " SELECT P.NCat AS Code, P.Description  " & _
                                " FROM Process P  "
                        Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) = AgL.FillData(mQry, AgL.GCn)
                    End If
                End If

            Case Col1LotNo
                If e.KeyCode <> Keys.Enter Then
                    If Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) Is Nothing Then
                        FCreateHelpLotNo()
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
    End Sub

    Private Sub FCreateHelpLotNo()
        If AgL.VNull(AgL.Dman_Execute("Select IsRequired_LotNo From ItemSiteDetail L Where Code = '" & Dgl1.Item(Col1Item, Dgl1.CurrentCell.RowIndex).Tag & "' And Div_Code = '" & AgL.PubDivCode & "' And Site_Code = '" & AgL.PubSiteCode & "' ", AgL.GcnRead).ExecuteScalar) <> 0 Then
            mQry = " SELECT L.LotNo AS Code, L.LotNo, IsNull(Sum(L.Qty_Rec), 0) - IsNull(Sum(L.Qty_Iss), 0) AS Qty " & _
                    " FROM Stock L  " & _
                    " WHERE L.Item = '" & Dgl1.Item(Col1Item, Dgl1.CurrentCell.RowIndex).Tag & "' AND isnull(l.LotNo,'') <> '' " & _
                    " And L.V_Date <= '" & TxtV_Date.Text & "' " & _
                    " And L.Godown = '" & TxtFromGodown.Tag & "' " & _
                    " And L.DocId <> '" & mSearchCode & "'" & _
                    " GROUP BY L.LotNo " & _
                    " HAVING IsNull(Sum(L.Qty_Rec), 0) - IsNull(Sum(L.Qty_Iss), 0) <> 0 "
            Dgl1.AgHelpDataSet(Col1LotNo) = AgL.FillData(mQry, AgL.GCn)
        End If
    End Sub

    Private Sub FrmStoreIssue_BaseEvent_Topctrl_tbPrn(ByVal SearchCode As String) Handles Me.BaseEvent_Topctrl_tbPrn
        mQry = " SELECT H.DocID, H.V_Type, H.V_Date, H.ManualRefNo, H.Remarks, H.EntryBy, H.EntryDate, " & _
                " H.ApproveBy, H.ApproveDate,  H.Status,  " & _
                " L.Item, L.Sr, L.QTY, L.UNIT, L.REMARKS AS LINEREMARKS, L.LotNo,  " & _
                " FG.DESCRIPTION AS FromGodownDesc, TG.Description as ToGodownDesc,  I.Description AS ItemDesc, Unit.DecimalPlaces,   " & _
                " D1.Description AS D1Desc,  D2.Description AS D2Desc, " & _
                " '" & AgTemplate.ClsMain.FGetDimension1Caption() & "' AS Caption_Dimension1,  '" & AgTemplate.ClsMain.FGetDimension2Caption() & "' AS Caption_Dimension2, " & _
                " I.ITEMGROUP, I.ITEMTYPE, IG.DESCRIPTION AS ItemGroupDesc " & _
                " FROM StockHead H   " & _
                " LEFT JOIN StockHeadDetail L ON H.DOCID = L.DOCID   " & _
                " LEFT JOIN Voucher_Type VT ON H.V_Type = VT.V_Type  " & _
                " LEFT JOIN Godown FG ON H.FromGodown = FG.Code     " & _
                " LEFT JOIN Godown TG ON H.ToGodown = TG.Code       " & _
                " LEFT JOIN Item I ON L.Item = I.Code   " & _
                " Left join Unit On I.Unit = Unit.Code " & _
                " LEFT JOIN ItemGroup  IG ON I.ItemGroup = IG.Code  " & _
                " LEFT JOIN Dimension1 D1 ON D1.Code = L.Dimension1 " & _
                " LEFT JOIN Dimension2 D2 ON D2.Code = L.Dimension2 " & _
                " WHERE H.DocID =  '" & mSearchCode & "'  Order By L.Sr "
        ClsMain.FPrintThisDocument(Me, TxtV_Type.Tag, mQry, "Store_StockTransfer_Print", "Stock Transfer")
    End Sub

    Private Sub FrmStoreIssue_BaseEvent_Topctrl_tbRef() Handles Me.BaseEvent_Topctrl_tbRef
        If Dgl1.AgHelpDataSet(Col1Item) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1Item) = Nothing
        If TxtFromGodown.AgHelpDataSet IsNot Nothing Then TxtFromGodown.AgHelpDataSet = Nothing
        If TxtToGodown.AgHelpDataSet IsNot Nothing Then TxtToGodown.AgHelpDataSet = Nothing
    End Sub

    Private Sub FCheckDuplicate(ByVal mRow As Integer)
        Dim I As Integer = 0
        Try
            With Dgl1
                For I = 0 To .Rows.Count - 1
                    If .Item(Col1Item, I).Value <> "" Then
                        If mRow <> I Then
                            If AgL.StrCmp(.Item(Col1Item, I).Value, .Item(Col1Item, mRow).Value) Then
                                If MsgBox("Item " & .Item(Col1Item, I).Value & " Is Already Feeded At Row No " & .Item(ColSNo, I).Value & ". Do You Want It In New Line ?", MsgBoxStyle.YesNo + MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2) = MsgBoxResult.No Then
                                    .CurrentCell = .Item(Col1Item, I) : Dgl1.Focus()
                                    .Rows.Remove(.Rows(mRow)) : Exit Sub
                                End If
                            End If
                        End If
                    End If
                Next
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FImportFromTextFile()
        Dim Sr As StreamReader
        Dim Opn As New OpenFileDialog

        Dim Line$ = "", mDateTime$ = "", mMachine$ = "", mProcess$ = "", mJobRecBy$ = "", mBarcode$ = "", mSKU$ = ""
        Dim mDefaultGodown$ = "", mJobType$ = "", mJobWorker$ = "", mIssRec$ = "", StrQry$ = ""
        Dim ErrorLog$ = "", StrMessage$ = ""

        Dim I As Integer, J As Integer = 0, bBarCodeQty As Integer = 0
        Dim DtTemp As DataTable = Nothing
        Dim strArr() As String


        ImportMessegeStr = ""
        ImportMode = True

        Opn.ShowDialog()

        If Opn.FileName = "" Then Exit Sub

        Sr = New StreamReader(Opn.FileName)

        StrMessage = ""

        Dgl1.RowCount = 1 : Dgl1.Rows.Clear()

        Do
            I += 1
            Line = Sr.ReadLine()
            If Line IsNot Nothing Then
                strArr = Split(Line, ",")
                If strArr.Length <> 8 Then
                    MsgBox("Invalid records in file")
                    Exit Sub
                End If

                Dgl1.Rows.Add()
                Dgl1.Item(ColSNo, I - 1).Value = Dgl1.Rows.Count - 1
                Dgl1.Item(Col1Item_UID, I - 1).Value = strArr(7)

                Dim Item_UidError$ = ""
                Item_UidError = FCheck_Item_UID(Dgl1.Item(Col1Item_UID, Dgl1.Rows.Count - 2).Value)

                If Item_UidError = "" Then
                    Validating_Item_Uid(Dgl1.Item(Col1Item_UID, Dgl1.Rows.Count - 2).Value, Dgl1.Rows.Count - 2)
                Else
                    Dgl1.Item(Col1Item_UID, Dgl1.Rows.Count - 2).Value = ""
                    Dgl1.Item(Col1Item_UID, Dgl1.Rows.Count - 2).Tag = ""
                End If

                StrMessage = StrMessage + Item_UidError
                If StrMessage <> "" Then
                    MsgBox(StrMessage)
                    Exit Sub
                End If
            End If
        Loop Until Line Is Nothing
        Sr.Close()
        Calculation()
        ImportMode = False
    End Sub

    Private Sub BtnImprtFromText_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnImprtFromText.Click
        If TxtV_Type.Tag = "CTRF" Then
            FImportFromTextFile()
        Else
            If BtnImprtFromText.Tag Is Nothing Then
                Dim FrmObj As Form
                FrmObj = AgTemplate.ClsMain.FRetImportForm(Me, TxtV_Type.Tag)
                If FrmObj IsNot Nothing Then
                    FrmObj.Owner = Me
                    BtnImprtFromText.Tag = FrmObj
                    FrmObj.ShowDialog()
                    FrmObj = Nothing
                End If
            Else
                BtnImprtFromText.Tag.ShowDialog()
            End If
        End If
        Dgl1.Focus()
    End Sub

    Private Sub Dgl1_RowEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.RowEnter
        If Not AgL.StrCmp(Topctrl1.Mode, "Browse") Then
            LblCurrentStock.Visible = True : LblCurrentStockText.Visible = True
            LblCurrentStock.Text = Format(AgTemplate.ClsMain.FunRetStock(Dgl1.Item(Col1Item, e.RowIndex).Tag, mSearchCode, , TxtFromGodown.Tag, , , TxtV_Date.Text, Dgl1.Item(Col1LotNo, e.RowIndex).Value), "0.".PadRight(Dgl1.Item(Col1QtyDecimalPlaces, e.RowIndex).Value + 2, "0"))
        Else
            LblCurrentStock.Visible = False : LblCurrentStockText.Visible = False
        End If
    End Sub

    Private Sub FPostInStock(ByVal SearchCode As String, ByVal Conn As SqlClient.SqlConnection, ByVal Cmd As SqlClient.SqlCommand)
        mQry = "Delete From Stock Where DocId = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " INSERT INTO Stock (DocId, Sr, V_Type, V_Prefix,  V_Date, V_No, RecID, Div_Code, Site_Code, SubCode, " & _
                  " Item, Dimension1, Dimension2, Manufacturer, Godown, Qty_Iss, Unit,  MeasurePerPcs, Measure_Iss, MeasureUnit,  Rate, Amount, " & _
                  " Cost, LotNo, BaleNo, Process, Remarks, ReferenceDocId, ReferenceDocIdSr)  " & _
                  " SELECT H.DocID, row_number() OVER (ORDER BY L.Item), max(H.V_Type) AS V_Type, max(H.V_Prefix) AS V_Prefix, max(H.V_Date) AS V_Date, max(H.V_No) AS V_No, Max(H.ManualRefNo) AS RecId, " & _
                  " max(H.Div_Code) AS Div_Code, max(H.Site_Code) AS Site_Code, max(H.SubCode) AS SubCode, L.Item, L.Dimension1, L.Dimension2, L.Manufacturer, max(H.FromGodown) AS Godown, " & _
                  " sum(L.Qty) AS Qty_Iss, Max(L.Unit) AS Unit, max(L.MeasurePerPcs) AS  MeasurePerPcs, sum(L.TotalMeasure) AS Measure_Iss, max(L.MeasureUnit) AS MeasureUnit, max(L.Rate) AS Rate, " & _
                  " sum(L.Amount) AS Amount, max(L.CostCenter) AS Cost, L.LotNo, L.BaleNo, L.Process, max(H.Remarks) AS Remarks, H.DocID, row_number() OVER (ORDER BY L.Item) " & _
                  " FROM StockHeadDetail L " & _
                  " LEFT JOIN StockHead H ON H.DocID = L.DocID " & _
                  " WHERE H.DocID = " & AgL.Chk_Text(mSearchCode) & " " & _
                  " GROUP BY H.DocID, L.Item, L.Dimension1, L.Dimension2, L.Manufacturer, L.LotNo,L.BaleNo, L.Process "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        Dim mToGodownDiv_Code$ = ""
        mQry = " Select Div_Code From Godown G With (NoLock) Where G.Code = '" & TxtToGodown.AgSelectedValue & "'"
        mToGodownDiv_Code = AgL.XNull(AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar)

        Dim mMaxSr As Integer = 0
        mQry = " Select Max(Sr) From Stock With (NoLock) Where DocId = '" & mSearchCode & "'"
        mMaxSr = AgL.VNull(AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar)

        mQry = " INSERT INTO Stock (DocId, Sr, V_Type, V_Prefix,  V_Date, V_No, RecID, Div_Code, Site_Code, SubCode, " & _
              " Item, Dimension1, Dimension2, Manufacturer, Godown, Qty_Rec, Unit,  MeasurePerPcs, Measure_Rec, MeasureUnit,  Rate, Amount, " & _
              " Cost, LotNo, BaleNo, Process, Remarks, ReferenceDocId, ReferenceDocIdSr)  " & _
              " SELECT H.DocID, Row_Number() Over (Order By L.Item) + " & mMaxSr & ", max(H.V_Type) AS V_Type, max(H.V_Prefix) AS V_Prefix, max(H.V_Date) AS V_Date, max(H.V_No) AS V_No, Max(H.ManualRefNo) AS RecId, " & _
              " '" & mToGodownDiv_Code & "' AS Div_Code, max(H.Site_Code) AS Site_Code, max(H.SubCode) AS SubCode, L.Item, L.Dimension1, L.Dimension2, L.Manufacturer, max(H.ToGodown) AS Godown, " & _
              " sum(L.Qty) AS Qty_Rec, Max(L.Unit) AS Unit, max(L.MeasurePerPcs) AS  MeasurePerPcs, sum(L.TotalMeasure) AS Measure_Rec, max(L.MeasureUnit) AS MeasureUnit, max(L.Rate) AS Rate, " & _
              " sum(L.Amount) AS Amount, max(L.CostCenter) AS Cost, L.LotNo, L.BaleNo, L.Process, max(H.Remarks) AS Remarks, H.DocID, row_number() OVER (ORDER BY L.Item) " & _
              " FROM StockHeadDetail L " & _
              " LEFT JOIN StockHead H ON H.DocID = L.DocID " & _
              " WHERE H.DocID = " & AgL.Chk_Text(mSearchCode) & " " & _
              " GROUP BY H.DocID, L.Item, L.Dimension1, L.Dimension2, L.Manufacturer, L.LotNo,L.BaleNo, L.Process "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    End Sub

    
    Private Sub BtnFillIssueDetail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnFillIssueDetail.Click
        If Topctrl1.Mode = "Browse" Then Exit Sub

        Dim strTicked As String
        If RbtnForStock.Checked = True Then
            strTicked = FHPGD_Items()
            If strTicked <> "" Then
                FFillItems(strTicked)
            End If

        Else
            Exit Sub
        End If
    End Sub

    Private Function FHPGD_Items() As String
        Dim FRH_Multiple As DMHelpGrid.FrmHelpGrid_Multi
        Dim StrRtn As String = ""

        mQry = "  SELECT 'o' As Tick,  VMain.CodeStr, VMain.ItemDesc, VMain.LotNo, VMain.ItemGroupDesc, VMain.Dimension1Desc, VMain.Dimension2Desc, VMain.Process, VMain.Qty " & _
                " FROM ( " & FRetFillItemWiseQry(" ", "") & " ) As VMain " & _
                " Order By VMain.ItemDesc, VMain.LotNo, VMain.Dimension1Desc, VMain.Dimension2Desc, VMain.Process "

        FRH_Multiple = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(AgL.FillData(mQry, AgL.GCn).TABLES(0)), "", 500, 970, , , False)
        FRH_Multiple.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple.FFormatColumn(1, , 0, , False)
        FRH_Multiple.FFormatColumn(2, "Item", 200, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(3, "Lot No", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(4, "Item Group", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(5, AgTemplate.ClsMain.FGetDimension1Caption(), 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(6, AgTemplate.ClsMain.FGetDimension2Caption(), 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(7, "Process", 110, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(8, "Qty", 100, DataGridViewContentAlignment.MiddleLeft)

        FRH_Multiple.StartPosition = FormStartPosition.CenterScreen
        FRH_Multiple.ShowDialog()

        If FRH_Multiple.BytBtnValue = 0 Then
            StrRtn = FRH_Multiple.FFetchData(1, "'", "'", ",", True)
        End If
        FHPGD_Items = StrRtn

        FRH_Multiple = Nothing
    End Function

    Private Sub FFillItems(ByVal bItemStr As String)
        Dim I As Integer = 0
        Dim DtTemp As DataTable = Nothing

        Try
            If bItemStr = "" Then Exit Sub
            mQry = FRetFillItemWiseQry("", " And ISNULL(H.Item,'') + ISNULL(H.LotNo,'') + ISNULL(H.Process,'') + ISNULL(H.Dimension1,'') + ISNULL(H.Dimension2,'') In (" & bItemStr & ")")
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
                        Dgl1.Item(ColSNo, J).Value = Dgl1.Rows.Count - 1
                        Dgl1.Item(Col1Item, J).Tag = AgL.XNull(.Rows(I)("Item"))
                        Dgl1.Item(Col1Item, J).Value = AgL.XNull(.Rows(I)("ItemDesc"))

                        Dgl1.Item(Col1Dimension1, J).Tag = AgL.XNull(.Rows(I)("Dimension1"))
                        Dgl1.Item(Col1Dimension1, J).Value = AgL.XNull(.Rows(I)("Dimension1Desc"))
                        Dgl1.Item(Col1Dimension2, J).Tag = AgL.XNull(.Rows(I)("Dimension2"))
                        Dgl1.Item(Col1Dimension2, J).Value = AgL.XNull(.Rows(I)("Dimension2Desc"))
                        Dgl1.Item(Col1LotNo, J).Value = AgL.XNull(.Rows(I)("LotNo"))
                        Dgl1.Item(Col1Process, J).Tag = AgL.XNull(.Rows(I)("ProcessCode"))
                        Dgl1.Item(Col1Process, J).Value = AgL.XNull(.Rows(I)("Process"))

                        Dgl1.Item(Col1Qty, J).Value = Format(AgL.VNull(.Rows(I)("Qty")), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
                        Dgl1.Item(Col1Unit, J).Value = AgL.XNull(.Rows(I)("Unit"))
                        Dgl1.Item(Col1QtyDecimalPlaces, J).Value = AgL.VNull(.Rows(I)("QtyDecimalPlaces"))
                        Dgl1.Item(Col1MeasurePerPcs, J).Value = Format(AgL.VNull(.Rows(I)("MeasurePerPcs")), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                        Dgl1.Item(Col1MeasureUnit, J).Value = AgL.XNull(.Rows(I)("MeasureUnit"))
                        Dgl1.Item(Col1MeasureDecimalPlaces, J).Value = AgL.VNull(.Rows(I)("MeasureDecimalPlaces"))
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
        FRetFillItemWiseQry = " SELECT ISNULL(H.Item,'') + ISNULL(H.LotNo,'') + ISNULL(H.Process,'') + ISNULL(H.Dimension1,'') + ISNULL(H.Dimension2,'') AS CodeStr, ISNULL(H.Item,'') AS Item , Max(I.Description) AS ItemDesc, H.LotNo, " & _
                            " Max(D1.Description) AS Dimension1Desc, Max(D2.Description) AS Dimension2Desc, Max(P.Description) AS Process, Round(isnull(sum(H.Qty_Rec),0) - isnull(sum(H.Qty_Iss),0),4) AS Qty, " & _
                            " H.process AS ProcessCode, Max(I.Unit) AS Unit, H.Dimension1, H.Dimension2, Max(I.ManualCode) AS ItemCode, " & _
                            " Max(I.Measure) AS MeasurePerPcs, Max(I.MeasureUnit) AS MeasureUnit, Max(U.DecimalPlaces) AS QtyDecimalPlaces, Max(IG.Description) AS ItemGroupDesc, Max(MU.DecimalPlaces) as MeasureDecimalPlaces, Max(I.Rate) AS Rate, " & _
                            " NULL AS RequisitionNo, NULL AS RequisitionDocId, NULL AS RequisitionSr, '" & RbtnForStock.Text & "' AS V_Nature " & _
                            " FROM Stock H WITH (Nolock) " & _
                            " LEFT JOIN Item I WITH (Nolock) ON I.Code = H.Item  " & _
                            " LEFT JOIN Process P ON P.NCat = H.Process " & _
                            " LEFT JOIN Dimension1 D1 WITH (Nolock) ON D1.Code = H.Dimension1 " & _
                            " LEFT JOIN Dimension2 D2 WITH (Nolock) ON D2.Code = H.Dimension2  " & _
                            " LEFT JOIN ItemGroup IG On Ig.Code = I.ItemGroup" & _
                            " Left Join Unit U On I.Unit = U.Code " & _
                            " Left Join Unit MU On I.MeasureUnit = MU.Code " & _
                            " WHERE isnull(H.Item,'') <> ''  AND H.Godown = " & AgL.Chk_Text(TxtFromGodown.Tag) & " " & _
                            " AND H.V_Date <= " & AgL.Chk_Text(TxtV_Date.Text) & " AND H.DocID <> " & AgL.Chk_Text(mInternalCode) & " " & _
                            " And IsNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "'  " & HeaderConStr & LineConStr & _
                            " GROUP BY H.Item, H.LotNo, H.Process, H.Dimension1, H.Dimension2 " & _
                            " HAVING Round(isnull(sum(H.Qty_Rec),0) - isnull(sum(H.Qty_Iss),0),4) > 0 "
    End Function
End Class
