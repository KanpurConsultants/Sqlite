Imports System.Data.SQLite
Public Class FrmPurchIndentNew
    Inherits AgTemplate.TempTransaction
    Public mQry$

    Public Event BaseFunction_MoveRecLine(ByVal SearchCode As String, ByVal Sr As Integer, ByVal mGridRow As Integer)
    Public Event BaseEvent_Save_InTransLine(ByVal SearchCode As String, ByVal Sr As Integer, ByVal mGridRow As Integer, ByVal Conn As SQLiteConnection, ByVal Cmd As SQLiteCommand)
    Protected Const ColSNo As String = "S.No."
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Protected Const Col1ItemCode As String = "Item Code"
    Protected Const Col1Item As String = "Item"
    Protected Const Col1ProdOrder As String = "Prod Order"
    Protected Const Col1PlanningNo As String = "Planning No"
    Protected Const Col1PlanningSr As String = "Planning Sr"
    Protected Const Col1CurrentStock As String = "Current Stock"
    Protected Const Col1QtyDecimalPlaces As String = "Qty Decimal Places"
    Protected Const Col1ReqQty As String = "Requisition Qty"
    Protected Const Col1IndentQty As String = "Indent Qty"
    Protected Const Col1Unit As String = "Unit"
    Protected Const Col1Rate As String = "Rate"
    Protected Const Col1MeasureDecimalPlaces As String = "Measure Decimal Places"
    Protected Const Col1MeasurePerPcs As String = "Measure Per Pcs"
    Protected Const Col1MeasureUnit As String = "Measure Unit"
    Protected Const Col1TotalReqMeasure As String = "Total Requisition Measure"
    Protected Const Col1TotalIndentMeasure As String = "Total Indent Measure"
    Protected Const Col1RequireDate As String = "Require Date"
    Protected Const Col1Remark As String = "Remark"

    Public WithEvents Dgl2 As New AgControls.AgDataGrid
    Protected Const Col2Item As String = "Item"
    Protected Const Col2RequisitionNo As String = "Requisition No"
    Protected Const Col2RequisitionSr As String = "Requisition Sr"
    Protected Const Col2Qty As String = "Qty"
    Protected Const Col2Unit As String = "Unit"
    Protected Const Col2MeasurePerPcs As String = "Measure Per Pcs"
    Protected Const Col2MeasureUnit As String = "Measure Unit"
    Protected Const Col2TotalMeasure As String = "Total Measure"
    Protected Const Col2RequireDate As String = "Require Date"

    Protected WithEvents BtnFillIndentDetail As System.Windows.Forms.Button
    Protected WithEvents GrpDirectIndent As System.Windows.Forms.GroupBox
    Protected WithEvents RbtIndentForPlanning As System.Windows.Forms.RadioButton
    Protected WithEvents RBtnIndForRequisition As System.Windows.Forms.RadioButton
    Protected WithEvents PnlReq As System.Windows.Forms.Panel
    Protected WithEvents RbtIndentDirect As System.Windows.Forms.RadioButton

    Public Sub New(ByVal StrUPVar As String, ByVal DTUP As DataTable, ByVal StrNCat As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Topctrl1.FSetParent(Me, StrUPVar, DTUP)
        Topctrl1.SetDisp(True)

        EntryNCat = StrNCat

        mQry = "Select H.* from Voucher_Type_Settings H Left Join Voucher_Type Vt On H.V_Type = Vt.V_Type  Where Vt.NCat In ('" & EntryNCat & "') And H.Div_Code = '" & AgL.PubDivCode & "' And H.Site_Code ='" & AgL.PubSiteCode & "' "
        DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)

    End Sub

#Region "Form Designer Code"
    Private Sub InitializeComponent()
        Me.Dgl1 = New AgControls.AgDataGrid
        Me.TxtDepartment = New AgControls.AgTextBox
        Me.LblDepartment = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.LblTotalMeasure = New System.Windows.Forms.Label
        Me.LblTotalMeasureText = New System.Windows.Forms.Label
        Me.LblTotalQty = New System.Windows.Forms.Label
        Me.LblTotalQtyText = New System.Windows.Forms.Label
        Me.Pnl1 = New System.Windows.Forms.Panel
        Me.Label30 = New System.Windows.Forms.Label
        Me.TxtRemarks = New AgControls.AgTextBox
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel
        Me.LblIndentorReq = New System.Windows.Forms.Label
        Me.TxtIndentor = New AgControls.AgTextBox
        Me.LblIndentor = New System.Windows.Forms.Label
        Me.LblDepartmentReq = New System.Windows.Forms.Label
        Me.BtnFillIndentDetail = New System.Windows.Forms.Button
        Me.GrpDirectIndent = New System.Windows.Forms.GroupBox
        Me.RBtnIndForRequisition = New System.Windows.Forms.RadioButton
        Me.RbtIndentDirect = New System.Windows.Forms.RadioButton
        Me.RbtIndentForPlanning = New System.Windows.Forms.RadioButton
        Me.PnlReq = New System.Windows.Forms.Panel
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
        Me.GrpDirectIndent.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.Location = New System.Drawing.Point(756, 529)
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
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(596, 531)
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
        Me.GBoxApprove.Location = New System.Drawing.Point(421, 529)
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
        Me.GBoxEntryType.Location = New System.Drawing.Point(145, 529)
        Me.GBoxEntryType.Size = New System.Drawing.Size(119, 40)
        '
        'TxtEntryType
        '
        Me.TxtEntryType.Location = New System.Drawing.Point(3, 19)
        Me.TxtEntryType.Tag = ""
        '
        'GrpUP
        '
        Me.GrpUP.Location = New System.Drawing.Point(11, 529)
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
        Me.GroupBox1.Location = New System.Drawing.Point(2, 523)
        Me.GroupBox1.Size = New System.Drawing.Size(1012, 4)
        '
        'GBoxDivision
        '
        Me.GBoxDivision.Location = New System.Drawing.Point(287, 529)
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
        Me.LblV_No.Location = New System.Drawing.Point(493, 34)
        Me.LblV_No.Size = New System.Drawing.Size(67, 16)
        Me.LblV_No.Tag = ""
        Me.LblV_No.Text = "Indent No."
        '
        'TxtV_No
        '
        Me.TxtV_No.AgSelectedValue = ""
        Me.TxtV_No.BackColor = System.Drawing.Color.White
        Me.TxtV_No.Location = New System.Drawing.Point(595, 33)
        Me.TxtV_No.Size = New System.Drawing.Size(161, 18)
        Me.TxtV_No.TabIndex = 3
        Me.TxtV_No.Tag = ""
        Me.TxtV_No.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(349, 39)
        Me.Label2.Tag = ""
        '
        'LblV_Date
        '
        Me.LblV_Date.BackColor = System.Drawing.Color.Transparent
        Me.LblV_Date.Location = New System.Drawing.Point(241, 34)
        Me.LblV_Date.Size = New System.Drawing.Size(74, 16)
        Me.LblV_Date.Tag = ""
        Me.LblV_Date.Text = "Indent Date"
        '
        'LblV_TypeReq
        '
        Me.LblV_TypeReq.Location = New System.Drawing.Point(580, 19)
        Me.LblV_TypeReq.Tag = ""
        '
        'TxtV_Date
        '
        Me.TxtV_Date.AgSelectedValue = ""
        Me.TxtV_Date.BackColor = System.Drawing.Color.White
        Me.TxtV_Date.Location = New System.Drawing.Point(365, 33)
        Me.TxtV_Date.Size = New System.Drawing.Size(122, 18)
        Me.TxtV_Date.TabIndex = 2
        Me.TxtV_Date.Tag = ""
        '
        'LblV_Type
        '
        Me.LblV_Type.Location = New System.Drawing.Point(493, 15)
        Me.LblV_Type.Size = New System.Drawing.Size(74, 16)
        Me.LblV_Type.Tag = ""
        Me.LblV_Type.Text = "Indent Type"
        '
        'TxtV_Type
        '
        Me.TxtV_Type.AgSelectedValue = ""
        Me.TxtV_Type.BackColor = System.Drawing.Color.White
        Me.TxtV_Type.Location = New System.Drawing.Point(595, 13)
        Me.TxtV_Type.Size = New System.Drawing.Size(161, 18)
        Me.TxtV_Type.TabIndex = 1
        Me.TxtV_Type.Tag = ""
        '
        'LblSite_CodeReq
        '
        Me.LblSite_CodeReq.Location = New System.Drawing.Point(349, 19)
        Me.LblSite_CodeReq.Tag = ""
        '
        'LblSite_Code
        '
        Me.LblSite_Code.BackColor = System.Drawing.Color.Transparent
        Me.LblSite_Code.Location = New System.Drawing.Point(241, 15)
        Me.LblSite_Code.Size = New System.Drawing.Size(87, 16)
        Me.LblSite_Code.Tag = ""
        Me.LblSite_Code.Text = "Branch Name"
        '
        'TxtSite_Code
        '
        Me.TxtSite_Code.AgSelectedValue = ""
        Me.TxtSite_Code.BackColor = System.Drawing.Color.White
        Me.TxtSite_Code.Location = New System.Drawing.Point(365, 13)
        Me.TxtSite_Code.Size = New System.Drawing.Size(122, 18)
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
        Me.TabControl1.Location = New System.Drawing.Point(-6, 20)
        Me.TabControl1.Size = New System.Drawing.Size(1004, 151)
        Me.TabControl1.TabIndex = 0
        '
        'TP1
        '
        Me.TP1.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.TP1.Controls.Add(Me.PnlReq)
        Me.TP1.Controls.Add(Me.LblDepartmentReq)
        Me.TP1.Controls.Add(Me.LblIndentorReq)
        Me.TP1.Controls.Add(Me.TxtIndentor)
        Me.TP1.Controls.Add(Me.LblIndentor)
        Me.TP1.Controls.Add(Me.TxtRemarks)
        Me.TP1.Controls.Add(Me.Label30)
        Me.TP1.Controls.Add(Me.TxtDepartment)
        Me.TP1.Controls.Add(Me.LblDepartment)
        Me.TP1.Location = New System.Drawing.Point(4, 22)
        Me.TP1.Size = New System.Drawing.Size(996, 125)
        Me.TP1.Text = "Document Detail"
        Me.TP1.Controls.SetChildIndex(Me.TxtV_No, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtSite_Code, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_Date, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label2, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_No, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblSite_Code, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtV_Date, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblSite_CodeReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblDepartment, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblPrefix, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtDepartment, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_Type, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_TypeReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtV_Type, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label30, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtRemarks, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblDocId, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtDocId, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblIndentor, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtIndentor, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblIndentorReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblDepartmentReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.PnlReq, 0)
        '
        'Topctrl1
        '
        Me.Topctrl1.Size = New System.Drawing.Size(994, 41)
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
        'TxtDepartment
        '
        Me.TxtDepartment.AgAllowUserToEnableMasterHelp = False
        Me.TxtDepartment.AgLastValueTag = Nothing
        Me.TxtDepartment.AgLastValueText = Nothing
        Me.TxtDepartment.AgMandatory = True
        Me.TxtDepartment.AgMasterHelp = False
        Me.TxtDepartment.AgNumberLeftPlaces = 8
        Me.TxtDepartment.AgNumberNegetiveAllow = False
        Me.TxtDepartment.AgNumberRightPlaces = 2
        Me.TxtDepartment.AgPickFromLastValue = False
        Me.TxtDepartment.AgRowFilter = ""
        Me.TxtDepartment.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtDepartment.AgSelectedValue = Nothing
        Me.TxtDepartment.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtDepartment.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtDepartment.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtDepartment.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDepartment.Location = New System.Drawing.Point(365, 53)
        Me.TxtDepartment.MaxLength = 50
        Me.TxtDepartment.Name = "TxtDepartment"
        Me.TxtDepartment.Size = New System.Drawing.Size(391, 18)
        Me.TxtDepartment.TabIndex = 4
        '
        'LblDepartment
        '
        Me.LblDepartment.AutoSize = True
        Me.LblDepartment.BackColor = System.Drawing.Color.Transparent
        Me.LblDepartment.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDepartment.Location = New System.Drawing.Point(241, 53)
        Me.LblDepartment.Name = "LblDepartment"
        Me.LblDepartment.Size = New System.Drawing.Size(75, 16)
        Me.LblDepartment.TabIndex = 706
        Me.LblDepartment.Text = "Department"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Cornsilk
        Me.Panel1.Controls.Add(Me.LblTotalMeasure)
        Me.Panel1.Controls.Add(Me.LblTotalMeasureText)
        Me.Panel1.Controls.Add(Me.LblTotalQty)
        Me.Panel1.Controls.Add(Me.LblTotalQtyText)
        Me.Panel1.Location = New System.Drawing.Point(7, 502)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(976, 21)
        Me.Panel1.TabIndex = 694
        '
        'LblTotalMeasure
        '
        Me.LblTotalMeasure.AutoSize = True
        Me.LblTotalMeasure.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalMeasure.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalMeasure.Location = New System.Drawing.Point(877, 3)
        Me.LblTotalMeasure.Name = "LblTotalMeasure"
        Me.LblTotalMeasure.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalMeasure.TabIndex = 670
        Me.LblTotalMeasure.Text = "."
        Me.LblTotalMeasure.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTotalMeasureText
        '
        Me.LblTotalMeasureText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalMeasureText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalMeasureText.Location = New System.Drawing.Point(708, 3)
        Me.LblTotalMeasureText.Name = "LblTotalMeasureText"
        Me.LblTotalMeasureText.Size = New System.Drawing.Size(163, 16)
        Me.LblTotalMeasureText.TabIndex = 669
        Me.LblTotalMeasureText.Text = "Total Measure :"
        Me.LblTotalMeasureText.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'LblTotalQty
        '
        Me.LblTotalQty.AutoSize = True
        Me.LblTotalQty.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalQty.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalQty.Location = New System.Drawing.Point(548, 3)
        Me.LblTotalQty.Name = "LblTotalQty"
        Me.LblTotalQty.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalQty.TabIndex = 668
        Me.LblTotalQty.Text = "."
        Me.LblTotalQty.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTotalQtyText
        '
        Me.LblTotalQtyText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalQtyText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalQtyText.Location = New System.Drawing.Point(411, 3)
        Me.LblTotalQtyText.Name = "LblTotalQtyText"
        Me.LblTotalQtyText.Size = New System.Drawing.Size(131, 16)
        Me.LblTotalQtyText.TabIndex = 667
        Me.LblTotalQtyText.Text = "Total Qty :"
        Me.LblTotalQtyText.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Pnl1
        '
        Me.Pnl1.Location = New System.Drawing.Point(7, 196)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(976, 306)
        Me.Pnl1.TabIndex = 1
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(241, 95)
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
        Me.TxtRemarks.Location = New System.Drawing.Point(365, 93)
        Me.TxtRemarks.MaxLength = 255
        Me.TxtRemarks.Name = "TxtRemarks"
        Me.TxtRemarks.Size = New System.Drawing.Size(391, 18)
        Me.TxtRemarks.TabIndex = 7
        '
        'LinkLabel1
        '
        Me.LinkLabel1.BackColor = System.Drawing.Color.SteelBlue
        Me.LinkLabel1.DisabledLinkColor = System.Drawing.Color.White
        Me.LinkLabel1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel1.LinkColor = System.Drawing.Color.White
        Me.LinkLabel1.Location = New System.Drawing.Point(5, 175)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(260, 20)
        Me.LinkLabel1.TabIndex = 731
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "Purchase Indent For Following Items"
        Me.LinkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblIndentorReq
        '
        Me.LblIndentorReq.AutoSize = True
        Me.LblIndentorReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblIndentorReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblIndentorReq.Location = New System.Drawing.Point(349, 80)
        Me.LblIndentorReq.Name = "LblIndentorReq"
        Me.LblIndentorReq.Size = New System.Drawing.Size(10, 7)
        Me.LblIndentorReq.TabIndex = 732
        Me.LblIndentorReq.Text = "Ä"
        '
        'TxtIndentor
        '
        Me.TxtIndentor.AgAllowUserToEnableMasterHelp = False
        Me.TxtIndentor.AgLastValueTag = Nothing
        Me.TxtIndentor.AgLastValueText = Nothing
        Me.TxtIndentor.AgMandatory = True
        Me.TxtIndentor.AgMasterHelp = False
        Me.TxtIndentor.AgNumberLeftPlaces = 8
        Me.TxtIndentor.AgNumberNegetiveAllow = False
        Me.TxtIndentor.AgNumberRightPlaces = 2
        Me.TxtIndentor.AgPickFromLastValue = False
        Me.TxtIndentor.AgRowFilter = ""
        Me.TxtIndentor.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtIndentor.AgSelectedValue = Nothing
        Me.TxtIndentor.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtIndentor.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtIndentor.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtIndentor.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtIndentor.Location = New System.Drawing.Point(365, 73)
        Me.TxtIndentor.MaxLength = 20
        Me.TxtIndentor.Name = "TxtIndentor"
        Me.TxtIndentor.Size = New System.Drawing.Size(391, 18)
        Me.TxtIndentor.TabIndex = 6
        '
        'LblIndentor
        '
        Me.LblIndentor.AutoSize = True
        Me.LblIndentor.BackColor = System.Drawing.Color.Transparent
        Me.LblIndentor.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblIndentor.Location = New System.Drawing.Point(241, 73)
        Me.LblIndentor.Name = "LblIndentor"
        Me.LblIndentor.Size = New System.Drawing.Size(54, 16)
        Me.LblIndentor.TabIndex = 731
        Me.LblIndentor.Text = "Indentor"
        '
        'LblDepartmentReq
        '
        Me.LblDepartmentReq.AutoSize = True
        Me.LblDepartmentReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblDepartmentReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblDepartmentReq.Location = New System.Drawing.Point(349, 59)
        Me.LblDepartmentReq.Name = "LblDepartmentReq"
        Me.LblDepartmentReq.Size = New System.Drawing.Size(10, 7)
        Me.LblDepartmentReq.TabIndex = 733
        Me.LblDepartmentReq.Text = "Ä"
        '
        'BtnFillIndentDetail
        '
        Me.BtnFillIndentDetail.BackColor = System.Drawing.Color.Transparent
        Me.BtnFillIndentDetail.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnFillIndentDetail.Font = New System.Drawing.Font("Verdana", 6.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnFillIndentDetail.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnFillIndentDetail.Location = New System.Drawing.Point(752, 175)
        Me.BtnFillIndentDetail.Margin = New System.Windows.Forms.Padding(0)
        Me.BtnFillIndentDetail.Name = "BtnFillIndentDetail"
        Me.BtnFillIndentDetail.Size = New System.Drawing.Size(51, 19)
        Me.BtnFillIndentDetail.TabIndex = 760
        Me.BtnFillIndentDetail.TabStop = False
        Me.BtnFillIndentDetail.Text = "...."
        Me.BtnFillIndentDetail.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnFillIndentDetail.UseVisualStyleBackColor = False
        '
        'GrpDirectIndent
        '
        Me.GrpDirectIndent.BackColor = System.Drawing.Color.Transparent
        Me.GrpDirectIndent.Controls.Add(Me.RBtnIndForRequisition)
        Me.GrpDirectIndent.Controls.Add(Me.RbtIndentDirect)
        Me.GrpDirectIndent.Controls.Add(Me.RbtIndentForPlanning)
        Me.GrpDirectIndent.Location = New System.Drawing.Point(271, 166)
        Me.GrpDirectIndent.Name = "GrpDirectIndent"
        Me.GrpDirectIndent.Size = New System.Drawing.Size(478, 28)
        Me.GrpDirectIndent.TabIndex = 759
        Me.GrpDirectIndent.TabStop = False
        '
        'RBtnIndForRequisition
        '
        Me.RBtnIndForRequisition.AutoSize = True
        Me.RBtnIndForRequisition.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RBtnIndForRequisition.Location = New System.Drawing.Point(296, 6)
        Me.RBtnIndForRequisition.Name = "RBtnIndForRequisition"
        Me.RBtnIndForRequisition.Size = New System.Drawing.Size(170, 17)
        Me.RBtnIndForRequisition.TabIndex = 744
        Me.RBtnIndForRequisition.TabStop = True
        Me.RBtnIndForRequisition.Text = "Indent For Requisition"
        Me.RBtnIndForRequisition.UseVisualStyleBackColor = True
        '
        'RbtIndentDirect
        '
        Me.RbtIndentDirect.AutoSize = True
        Me.RbtIndentDirect.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RbtIndentDirect.Location = New System.Drawing.Point(15, 8)
        Me.RbtIndentDirect.Name = "RbtIndentDirect"
        Me.RbtIndentDirect.Size = New System.Drawing.Size(111, 17)
        Me.RbtIndentDirect.TabIndex = 743
        Me.RbtIndentDirect.TabStop = True
        Me.RbtIndentDirect.Text = "Indent Direct"
        Me.RbtIndentDirect.UseVisualStyleBackColor = True
        '
        'RbtIndentForPlanning
        '
        Me.RbtIndentForPlanning.AutoSize = True
        Me.RbtIndentForPlanning.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.RbtIndentForPlanning.Location = New System.Drawing.Point(136, 8)
        Me.RbtIndentForPlanning.Name = "RbtIndentForPlanning"
        Me.RbtIndentForPlanning.Size = New System.Drawing.Size(154, 17)
        Me.RbtIndentForPlanning.TabIndex = 0
        Me.RbtIndentForPlanning.TabStop = True
        Me.RbtIndentForPlanning.Text = "Indent For Planning"
        Me.RbtIndentForPlanning.UseVisualStyleBackColor = True
        '
        'PnlReq
        '
        Me.PnlReq.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.PnlReq.Location = New System.Drawing.Point(769, 13)
        Me.PnlReq.Name = "PnlReq"
        Me.PnlReq.Size = New System.Drawing.Size(231, 105)
        Me.PnlReq.TabIndex = 2
        Me.PnlReq.Visible = False
        '
        'FrmPurchIndentNew
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.BackColor = System.Drawing.SystemColors.ButtonShadow
        Me.ClientSize = New System.Drawing.Size(994, 572)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Pnl1)
        Me.Controls.Add(Me.BtnFillIndentDetail)
        Me.Controls.Add(Me.GrpDirectIndent)
        Me.Name = "FrmPurchIndentNew"
        Me.Text = "Template Purchase Indent"
        Me.Controls.SetChildIndex(Me.GrpDirectIndent, 0)
        Me.Controls.SetChildIndex(Me.BtnFillIndentDetail, 0)
        Me.Controls.SetChildIndex(Me.Pnl1, 0)
        Me.Controls.SetChildIndex(Me.Panel1, 0)
        Me.Controls.SetChildIndex(Me.LinkLabel1, 0)
        Me.Controls.SetChildIndex(Me.TabControl1, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.GrpUP, 0)
        Me.Controls.SetChildIndex(Me.GBoxEntryType, 0)
        Me.Controls.SetChildIndex(Me.GBoxApprove, 0)
        Me.Controls.SetChildIndex(Me.GBoxMoveToLog, 0)
        Me.Controls.SetChildIndex(Me.GroupBox2, 0)
        Me.Controls.SetChildIndex(Me.GBoxDivision, 0)
        Me.Controls.SetChildIndex(Me.Topctrl1, 0)
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
        Me.GrpDirectIndent.ResumeLayout(False)
        Me.GrpDirectIndent.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Protected WithEvents TxtDepartment As AgControls.AgTextBox
    Protected WithEvents LblDepartment As System.Windows.Forms.Label
    Protected WithEvents Panel1 As System.Windows.Forms.Panel
    Protected WithEvents Pnl1 As System.Windows.Forms.Panel
    Protected WithEvents TxtRemarks As AgControls.AgTextBox
    Protected WithEvents Label30 As System.Windows.Forms.Label
    Protected WithEvents LblTotalMeasure As System.Windows.Forms.Label
    Protected WithEvents LblTotalMeasureText As System.Windows.Forms.Label
    Protected WithEvents LblTotalQty As System.Windows.Forms.Label
    Protected WithEvents LblTotalQtyText As System.Windows.Forms.Label
    Protected WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    Protected WithEvents LblIndentorReq As System.Windows.Forms.Label
    Protected WithEvents TxtIndentor As AgControls.AgTextBox
    Protected WithEvents LblIndentor As System.Windows.Forms.Label
    Protected WithEvents LblDepartmentReq As System.Windows.Forms.Label
#End Region

    Private Sub FrmQuality1_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "PurchIndent"
        LogTableName = "PurchIndent_Log"
        MainLineTableCsv = "PurchIndentDetail,PurchIndentReq"
        LogLineTableCsv = "PurchIndentDetail_Log,PurchIndentReq_Log"
        AgL.GridDesign(Dgl1)
    End Sub

    Private Sub FrmQuality1_BaseFunction_FIniMast(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMast
        Dim mCondStr$
        mCondStr = " " & AgL.CondStrFinancialYear("P.V_Date", AgL.PubStartDate, AgL.PubEndDate) &
                       " And " & AgL.PubSiteCondition("P.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "P.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        If IsApplyVTypePermission Then
            mCondStr = mCondStr & " And P.V_Type In (Select V_Type From User_VType_Permission VP Where VP.UserName = '" & AgL.PubUserName & "' And VP.Div_Code = '" & AgL.PubDivCode & "' And VP.Site_Code = '" & AgL.PubSiteCode & "') "
        End If


        mQry = " Select P.DocID As SearchCode " &
            " From PurchIndent P " &
            " Left Join Voucher_Type Vt On P.V_Type = Vt.V_Type  " &
            " Where IfNull(IsDeleted,0) = 0  " & mCondStr & "  Order By P.V_Date Desc "

        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mCondStr$

        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) &
                        " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "H.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        If IsApplyVTypePermission Then
            mCondStr = mCondStr & " And H.V_Type In (Select V_Type From User_VType_Permission VP Where VP.UserName = '" & AgL.PubUserName & "' And VP.Div_Code = '" & AgL.PubDivCode & "' And VP.Site_Code = '" & AgL.PubSiteCode & "') "
        End If


        AgL.PubFindQry = " SELECT H.DocID AS SearchCode, H.DeliveryMeasure AS [Delivery Measure], H.V_Type AS [Indent Type], H.V_Prefix AS [Prefix], H.V_Date AS [Indent Date], H.V_No AS [Indent No], " &
                            " H.Remarks, H.TotalQty AS [Total Qty], H.TotalMeasure AS [Total Measure], H.EntryBy AS [Entry By], H.EntryDate AS [Entry Date], H.EntryType AS [Entry Type],  " &
                            " H.EntryStatus AS [Entry Status], H.ApproveBy AS [Approve By], H.ApproveDate AS [Approve Date], H.MoveToLog AS [Move To Log], H.MoveToLogDate AS [Move To Log Date], H.Status,  " &
                            " D.Div_Name AS Division, SM.Name AS [Site Name],DE.Description AS Department, SGI.DispName AS [Indentor Name], PO.ManualRefNo AS [Prod. ORDER No ] " &
                            " FROM  PurchIndent H " &
                            " LEFT JOIN Division D ON D.Div_Code =H.Div_Code   " &
                            " LEFT JOIN SiteMast SM ON SM.Code=H.Site_Code   " &
                            " LEFT JOIN voucher_type Vt ON H.V_Type = vt.V_Type  " &
                            " LEFT JOIN Department DE ON DE.Code=H.Department  " &
                            " LEFT JOIN SubGroup  SGI ON SGI.SubCode  = H.Indentor  " &
                            " LEFT JOIN ProdOrder PO ON PO.DocID  = H.ProdOrder  " &
                            " Where IfNull(H.IsDeleted,0) = 0   " & mCondStr

        AgL.PubFindQryOrdBy = "[Entry Date]"
    End Sub

    Private Sub FrmProductionOrder_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl1, Col1ItemCode, 100, 0, Col1ItemCode, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_ItemCode")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_ItemCode")), Boolean))
            .AddAgTextColumn(Dgl1, Col1Item, 200, 0, Col1Item, True, Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_ItemName")), Boolean))
            .AddAgTextColumn(Dgl1, Col1ProdOrder, 80, 0, Col1ProdOrder, True, True)
            .AddAgTextColumn(Dgl1, Col1PlanningNo, 80, 0, Col1PlanningNo, True, True)
            .AddAgTextColumn(Dgl1, Col1PlanningSr, 80, 0, Col1PlanningSr, False, True)
            .AddAgNumberColumn(Dgl1, Col1CurrentStock, 80, 8, 4, False, Col1CurrentStock, True, True, True)
            .AddAgTextColumn(Dgl1, Col1QtyDecimalPlaces, 50, 0, Col1QtyDecimalPlaces, False, True, False)
            .AddAgNumberColumn(Dgl1, Col1ReqQty, 80, 8, 4, False, Col1ReqQty, True, True, True)
            .AddAgNumberColumn(Dgl1, Col1IndentQty, 80, 8, 3, False, Col1IndentQty, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Qty")), Boolean), False, True)
            .AddAgTextColumn(Dgl1, Col1Unit, 50, 0, Col1Unit, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Unit")), Boolean), True)
            .AddAgNumberColumn(Dgl1, Col1Rate, 60, 8, 2, False, Col1Rate, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Rate")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_Rate")), Boolean), True)
            .AddAgTextColumn(Dgl1, Col1MeasureDecimalPlaces, 50, 0, Col1MeasureDecimalPlaces, False, True, False)
            .AddAgNumberColumn(Dgl1, Col1MeasurePerPcs, 80, 8, 4, False, Col1MeasurePerPcs, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_MeasurePerPcs")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_MeasurePerPcs")), Boolean), True)
            .AddAgTextColumn(Dgl1, Col1MeasureUnit, 50, 0, Col1MeasureUnit, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_MeasureUnit")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_MeasureUnit")), Boolean))
            .AddAgNumberColumn(Dgl1, Col1TotalReqMeasure, 80, 8, 4, False, Col1TotalReqMeasure, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Measure")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_Measure")), Boolean), True)
            .AddAgNumberColumn(Dgl1, Col1TotalIndentMeasure, 80, 8, 4, False, Col1TotalIndentMeasure, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Measure")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_Measure")), Boolean), True)
            .AddAgDateColumn(Dgl1, Col1RequireDate, 80, Col1RequireDate, True, False)
            .AddAgTextColumn(Dgl1, Col1Remark, 80, 255, Col1Remark, True, False)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 35


        If AgL.PubDtEnviro.Rows.Count > 0 Then
            If AgL.VNull(AgL.PubDtEnviro.Rows(0)("IsLotNoApplicable")) <> 0 Then
                Dgl1.Columns(Col1CurrentStock).CellTemplate.Style.Font = New Font(Dgl1.DefaultCellStyle.Font.FontFamily, Dgl1.DefaultCellStyle.Font.Size, FontStyle.Underline)
                Dgl1.Columns(Col1CurrentStock).CellTemplate.Style.ForeColor = Color.Blue
            End If
        End If

        Dgl1.AgSkipReadOnlyColumns = True

        Dgl2.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl2, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl2, Col2Item, 200, 0, Col2Item, True, False)
            .AddAgTextColumn(Dgl2, Col2RequisitionNo, 80, 0, Col2RequisitionNo, True, True)
            .AddAgNumberColumn(Dgl2, Col2RequisitionSr, 80, 8, 4, False, Col2RequisitionSr, True, True, True)
            .AddAgNumberColumn(Dgl2, Col2Qty, 80, 8, 4, False, Col2Qty, True, True, True)
            .AddAgTextColumn(Dgl2, Col2Unit, 50, 0, Col2Unit, True, True)
            .AddAgNumberColumn(Dgl2, Col2MeasurePerPcs, 80, 8, 4, False, Col2MeasurePerPcs, True, True, True)
            .AddAgTextColumn(Dgl2, Col2MeasureUnit, 50, 0, Col2MeasureUnit, True, True)
            .AddAgNumberColumn(Dgl2, Col2TotalMeasure, 80, 8, 4, False, Col2TotalMeasure, True, True, True)
            .AddAgDateColumn(Dgl2, Col2RequireDate, 80, Col2RequireDate, True, False)
        End With
        AgL.AddAgDataGrid(Dgl2, PnlReq)
        Dgl2.EnableHeadersVisualStyles = False
        Dgl2.ColumnHeadersHeight = 35
        Dgl2.Visible = False

    End Sub

    Private Sub FrmPurchIndent_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As SQLiteConnection, ByVal Cmd As SQLiteCommand) Handles Me.BaseEvent_Save_InTrans
        Dim I As Integer, J As Integer, mSr As Integer, mSr1 As Integer
        Dim bSelectionQry As String = ""
        Dim bSelectionQry1 As String = ""
        mQry = "UPDATE PurchIndent " &
                " SET " &
                " ManualRefNo = V_No, " &
                " Department = " & AgL.Chk_Text(TxtDepartment.Tag) & ", " &
                " Indentor = " & AgL.Chk_Text(TxtIndentor.Tag) & ", " &
                " Remarks = " & AgL.Chk_Text(TxtRemarks.Text) & " " &
                " Where DocID = '" & SearchCode & "'"

        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        'Never Try to Serialise Sr in Line Items 
        'As Some other Entry points may updating values to this Search code and Sr
        mSr1 = 0
        mSr = AgL.VNull(AgL.Dman_Execute("Select Max(Sr) From PurchIndentDetail  Where DocID = '" & mSearchCode & "'", AgL.GcnRead).ExecuteScalar)
        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1Item, I).Value <> "" Then
                If Dgl1.Item(ColSNo, I).Tag Is Nothing And Dgl1.Rows(I).Visible = True Then
                    mSr += 1
                    If bSelectionQry <> "" Then bSelectionQry += " UNION ALL "
                    bSelectionQry += " Select " & AgL.Chk_Text(SearchCode) & ", " & mSr & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1Item, I).Tag) & ", " &
                            " " & Val(Dgl1.Item(Col1CurrentStock, I).Value) & ", " &
                            " " & Val(Dgl1.Item(Col1ReqQty, I).Value) & ", " &
                            " " & Val(Dgl1.Item(Col1IndentQty, I).Value) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1Unit, I).Value) & ", " &
                            " " & Val(Dgl1.Item(Col1Rate, I).Value) & ", " &
                            " " & Val(Dgl1.Item(Col1MeasurePerPcs, I).Value) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1MeasureUnit, I).Value) & ", " &
                            " " & Val(Dgl1.Item(Col1TotalReqMeasure, I).Value) & ", " &
                            " " & Val(Dgl1.Item(Col1TotalIndentMeasure, I).Value) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1RequireDate, I).Value) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1ProdOrder, I).Tag) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1PlanningNo, I).Tag) & ", " &
                            " " & Val(Dgl1.Item(Col1PlanningSr, I).Value) & ", " &
                            " " & AgL.Chk_Text(mInternalCode) & ", " & mSr & ", " &
                            " '" & IIf(RbtIndentForPlanning.Checked, RbtIndentForPlanning.Text, RbtIndentDirect.Text) & "', " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1Remark, I).Value) & "  "
                Else
                    If Dgl1.Rows(I).Visible = True Then
                        If Dgl1.Rows(I).DefaultCellStyle.BackColor <> AgTemplate.ClsMain.Colours.GridRow_Locked Then
                            mQry = " UPDATE PurchIndentDetail " &
                                    " SET Item = " & AgL.Chk_Text(Dgl1.Item(Col1Item, I).Tag) & " , " &
                                    " CurrentStock = " & Val(Dgl1.Item(Col1CurrentStock, I).Value) & ", " &
                                    " ReqQty = " & Val(Dgl1.Item(Col1ReqQty, I).Value) & ", " &
                                    " IndentQty = " & Val(Dgl1.Item(Col1IndentQty, I).Value) & ", " &
                                    " Unit = " & AgL.Chk_Text(Dgl1.Item(Col1Unit, I).Value) & ", " &
                                    " Rate = " & Val(Dgl1.Item(Col1Rate, I).Value) & ", " &
                                    " MeasurePerPcs = " & Val(Dgl1.Item(Col1MeasurePerPcs, I).Value) & ", " &
                                    " MeasureUnit = " & AgL.Chk_Text(Dgl1.Item(Col1MeasureUnit, I).Value) & ", " &
                                    " TotalReqMeasure = " & Val(Dgl1.Item(Col1TotalReqMeasure, I).Value) & ", " &
                                    " TotalIndentMeasure = " & Val(Dgl1.Item(Col1TotalIndentMeasure, I).Value) & ", " &
                                    " RequireDate = " & AgL.Chk_Text(Dgl1.Item(Col1RequireDate, I).Value) & ", " &
                                    " ProdOrder = " & AgL.Chk_Text(Dgl1.Item(Col1ProdOrder, I).Tag) & ", " &
                                    " MaterialPlan = " & AgL.Chk_Text(Dgl1.Item(Col1PlanningNo, I).Tag) & ", " &
                                    " MaterialPlanSr = " & Val(Dgl1.Item(Col1PlanningSr, I).Value) & ", " &
                                    " V_Nature = '" & IIf(RbtIndentDirect.Checked, RbtIndentDirect.Text, RbtIndentForPlanning.Text) & "' , " &
                                    " Remark = " & AgL.Chk_Text(Dgl1.Item(Col1Remark, I).Value) & " " &
                                    " Where DocId = '" & mSearchCode & "' " &
                                    " And Sr = " & Dgl1.Item(ColSNo, I).Tag & " "
                            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                        End If
                    Else
                        mQry = " Delete From PurchIndentDetail Where DocId = '" & mSearchCode & "' And Sr = " & Val(Dgl1.Item(ColSNo, I).Tag) & "  "
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                    End If
                End If

                If Dgl1.Rows(I).Visible = True Then
                    For J = 0 To Dgl2.RowCount - 1
                        If Dgl2.Item(Col2Item, J).Value <> "" And Dgl2.Item(Col2Item, J).Value = Dgl1.Item(Col1Item, I).Value Then
                            mSr1 += 1
                            If bSelectionQry1 <> "" Then bSelectionQry1 += " UNION ALL "
                            bSelectionQry1 += "Select " & AgL.Chk_Text(SearchCode) & " , " & mSr1 & ", " & AgL.Chk_Text(Dgl2.Item(Col2RequisitionNo, J).Tag) & ", " &
                                    " " & AgL.Chk_Text(Dgl2.Item(Col2Item, J).Tag) & ",	" & Val(Dgl2.Item(Col2Qty, J).Value) & ", " & AgL.Chk_Text(Dgl2.Item(Col2Unit, J).Value) & ", " &
                                    " " & Val(Dgl2.Item(Col2MeasurePerPcs, J).Value) & ", " & AgL.Chk_Text(Dgl2.Item(Col2Unit, J).Value) & " ," & Val(Dgl2.Item(Col2TotalMeasure, J).Value) & ", " &
                                    " " & AgL.Chk_Text(Dgl2.Item(Col2RequireDate, J).Value) & ",	" & Val(Dgl2.Item(Col2RequisitionSr, J).Value) & "  "
                        End If
                    Next
                End If
            End If
        Next

        mQry = " INSERT INTO PurchIndentDetail (DocId, Sr,	Item,	CurrentStock,	ReqQty,	IndentQty, " &
                " Unit,	Rate,	MeasurePerPcs,	MeasureUnit,	TotalReqMeasure,	TotalIndentMeasure, " &
                " RequireDate, ProdOrder, MaterialPlan,	MaterialPlanSr,	PurchIndent,	PurchIndentSr,	V_Nature, Remark	) "
        mQry = mQry + bSelectionQry
        If bSelectionQry <> "" Then
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If

        mQry = " Delete From PurchIndentReq Where DocId = '" & mSearchCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " INSERT INTO PurchIndentReq	( DocId, Sr, Requisition, Item,	Qty, Unit, " &
                " MeasurePerPcs, MeasureUnit, TotalMeasure,	RequireDate, RequisitionSr	) "
        mQry = mQry + bSelectionQry1
        If bSelectionQry1 <> "" Then
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If

    End Sub

    Private Sub FrmProductionOrder_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim I As Integer
        Dim DrTemp As DataRow() = Nothing
        Dim DsTemp As DataSet

        Dim IsSameUnit As Boolean = True
        Dim IsSameMeasureUnit As Boolean = True

        Dim intQtyDecimalPlaces As Integer = 0
        Dim intMeasureDecimalPlaces As Integer = 0

        Dim strQryPurchQuotation$ = " SELECT H.PurchIndent, H.PurchIndentsr, sum(H.Qty)  AS Qty " &
                                        " FROM PurchQuotationDetail H " &
                                        " GROUP BY H.PurchIndent, H.PurchIndentsr "

        Dim strQryPurchOrder$ = " SELECT H.PurchIndent, H.PurchIndentsr, sum(H.Qty)  AS Qty " &
                                " FROM PurchOrderDetail H " &
                                " GROUP BY H.PurchIndent, H.PurchIndentsr "

        mQry = " SELECT H.* , SG.DispName AS IndentorName, D.Description AS DepartmentDesc, " &
                " L.Sr, L.Item, I.Description AS ItemDesc, I.ManualCode AS ItemCode, L.CurrentStock, " &
                " U.DecimalPlaces as QtyDecimalPlaces, MU.DecimalPlaces as MeasureDecimalPlaces, " &
                " L.ReqQty, L.IndentQty ,L.Unit, L.Rate, L.MeasurePerPcs, L.MeasureUnit, L.TotalReqMeasure, L.V_Nature, " &
                " L.TotalIndentMeasure, L.RequireDate, L.MaterialPlan, L.MaterialPlanSr ,L.PurchIndent , L.PurchIndentSr ,L.Remark AS LineRemark, " &
                " M.V_Type || '-' || Convert(NVarchar,M.V_No) AS PlanningNo, M.ProdOrder, ProdOrder.ManualRefNo as ProdOrderNo,    " &
                " ( Case When IfNull(PQ.Qty,0) > 0 Then 1 ELSE CASE WHEN IfNull(PO.Qty,0) > 0 THEN 1 ELSE 0 END END ) as RowLocked " &
                " FROM " &
                " ( " &
                " SELECT * FROM PurchIndent WHERE DocID = '" & SearchCode & "' " &
                " ) H  " &
                " LEFT JOIN SubGroup SG ON SG.SubCode = H.Indentor " &
                " LEFT JOIN Department D ON D.Code = H.Department " &
                " LEFT JOIN PurchIndentDetail L ON L.DocId = H.DocId " &
                " LEFT JOIN Item I ON I.Code = L.Item  " &
                " LEFT JOIN MaterialPlan M ON M.DocId = L.MaterialPlan  " &
                " LEFT JOIN ProdOrder ON M.ProdOrder = ProdOrder.DocID  " &
                " LEFT JOIN Unit U On L.Unit = U.Code " &
                " LEFT JOIN Unit MU ON L.MeasureUnit = MU.Code " &
                " Left Join ( " & strQryPurchQuotation & " ) As PQ On L.DocID = PQ.PurchIndent and L.Sr = PQ.PurchIndentsr " &
                " Left Join ( " & strQryPurchOrder & " ) As PO On L.DocID = PO.PurchIndent and L.Sr = PO.PurchIndentsr " &
                " Order By Sr "

        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                TxtDepartment.Tag = AgL.XNull(.Rows(0)("Department"))
                TxtDepartment.Text = AgL.XNull(.Rows(0)("DepartmentDesc"))
                TxtIndentor.Tag = AgL.XNull(.Rows(0)("Indentor"))
                TxtIndentor.Text = AgL.XNull(.Rows(0)("IndentorName"))
                TxtRemarks.Text = AgL.XNull(.Rows(0)("Remarks"))

                If AgL.XNull(.Rows(0)("V_Nature")) = RbtIndentForPlanning.Text Then
                    RbtIndentForPlanning.Checked = True
                Else
                    RbtIndentDirect.Checked = True
                End If

                IniGrid()

                '-------------------------------------------------------------
                'Line Records are showing in First Grid
                '-------------------------------------------------------------

                For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                    Dgl1.Rows.Add()
                    Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count - 1
                    Dgl1.Item(ColSNo, I).Tag = AgL.XNull(.Rows(I)("Sr"))
                    Dgl1.Item(Col1Item, I).Value = AgL.XNull(.Rows(I)("ItemDesc"))
                    Dgl1.Item(Col1Item, I).Tag = AgL.XNull(.Rows(I)("Item"))
                    Dgl1.Item(Col1ItemCode, I).Value = AgL.XNull(.Rows(I)("ItemCode"))
                    Dgl1.Item(Col1ItemCode, I).Tag = AgL.XNull(.Rows(I)("Item"))
                    Dgl1.Item(Col1QtyDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("QtyDecimalPlaces"))
                    Dgl1.Item(Col1CurrentStock, I).Value = Format(AgL.VNull(.Rows(I)("CurrentStock")), "0.000")
                    Dgl1.Item(Col1ReqQty, I).Value = AgL.VNull(.Rows(I)("ReqQty"))
                    Dgl1.Item(Col1IndentQty, I).Value = AgL.VNull(.Rows(I)("IndentQty"))
                    Dgl1.Item(Col1Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                    Dgl1.Item(Col1Rate, I).Value = Format(AgL.VNull(.Rows(I)("Rate")), "0.000")
                    Dgl1.Item(Col1MeasureDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("MeasureDecimalPlaces"))
                    Dgl1.Item(Col1MeasurePerPcs, I).Value = Format(AgL.VNull(.Rows(I)("MeasurePerPcs")), "0.000")
                    Dgl1.Item(Col1MeasureUnit, I).Value = AgL.XNull(.Rows(I)("MeasureUnit"))
                    Dgl1.Item(Col1TotalReqMeasure, I).Value = Format(AgL.VNull(.Rows(I)("TotalReqMeasure")), "0.000")
                    Dgl1.Item(Col1TotalIndentMeasure, I).Value = Format(AgL.VNull(.Rows(I)("TotalIndentMeasure")), "0.000")
                    Dgl1.Item(Col1RequireDate, I).Value = AgL.XNull(.Rows(I)("RequireDate"))
                    Dgl1.Item(Col1Remark, I).Value = AgL.XNull(.Rows(I)("LineRemark"))
                    Dgl1.Item(Col1PlanningNo, I).Value = AgL.XNull(.Rows(I)("PlanningNo"))
                    Dgl1.Item(Col1PlanningNo, I).Tag = AgL.XNull(.Rows(I)("MaterialPlan"))
                    Dgl1.Item(Col1ProdOrder, I).Tag = AgL.XNull(.Rows(I)("ProdOrder"))
                    Dgl1.Item(Col1ProdOrder, I).Value = AgL.XNull(.Rows(I)("ProdOrderNo"))
                    Dgl1.Item(Col1PlanningSr, I).Value = AgL.VNull(.Rows(I)("MaterialPlanSr"))

                    If .Rows(I)("RowLocked") > 0 Then Dgl1.Rows(I).DefaultCellStyle.BackColor = AgTemplate.ClsMain.Colours.GridRow_Locked

                    If Not AgL.StrCmp(Dgl1.Item(Col1Unit, I).Value, Dgl1.Item(Col1Unit, 0).Value) Then IsSameUnit = False
                    If Not AgL.StrCmp(Dgl1.Item(Col1MeasureUnit, I).Value, Dgl1.Item(Col1MeasureUnit, 0).Value) Then IsSameMeasureUnit = False

                    If intQtyDecimalPlaces < Val(Dgl1.Item(Col1QtyDecimalPlaces, I).Value) Then intQtyDecimalPlaces = Val(Dgl1.Item(Col1QtyDecimalPlaces, I).Value)
                    If intMeasureDecimalPlaces < Val(Dgl1.Item(Col1MeasureDecimalPlaces, I).Value) Then intMeasureDecimalPlaces = Val(Dgl1.Item(Col1MeasureDecimalPlaces, I).Value)

                    LblTotalQty.Text = Val(LblTotalQty.Text) + Val(Dgl1.Item(Col1IndentQty, I).Value)
                    LblTotalMeasure.Text = Val(LblTotalMeasure.Text) + Val(Dgl1.Item(Col1TotalIndentMeasure, I).Value)

                    RaiseEvent BaseFunction_MoveRecLine(SearchCode, AgL.VNull(.Rows(I)("Sr")), I)
                Next I
            End If
            If Dgl1.Item(Col1Unit, 0).Value <> "" And IsSameUnit Then LblTotalQtyText.Text = "Total Qty (" & Dgl1.Item(Col1Unit, 0).Value & ") :" Else LblTotalQtyText.Text = "Total Qty :"
            If Dgl1.Item(Col1MeasureUnit, 0).Value <> "" And IsSameMeasureUnit Then LblTotalMeasureText.Text = "Total Measure (" & Dgl1.Item(Col1MeasureUnit, 0).Value & ") :" Else LblTotalMeasureText.Text = "Total Measure :"
        End With
        LblTotalQty.Text = Format(Val(LblTotalQty.Text), "0.".PadRight(intQtyDecimalPlaces + 2, "0"))
        LblTotalMeasure.Text = Format(Val(LblTotalMeasure.Text), "0.".PadRight(intMeasureDecimalPlaces + 2, "0"))

        mQry = " SELECT L.*, R.ReferenceNo AS ReqNo, I.Description AS ItemDesc " &
                " FROM PurchIndentReq L " &
                " LEFT JOIN Requisition R ON R.DocID = L.Requisition  " &
                " LEFT JOIN Item I ON I.Code = L.Item  " &
                " WHERE L.DocID = '" & SearchCode & "' Order By L.Sr "
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                    Dgl2.Rows.Add()
                    Dgl2.Item(ColSNo, I).Value = Dgl2.Rows.Count - 1
                    Dgl2.Item(Col2Item, I).Value = AgL.XNull(.Rows(I)("ItemDesc"))
                    Dgl2.Item(Col2Item, I).Tag = AgL.XNull(.Rows(I)("Item"))
                    Dgl2.Item(Col2Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                    Dgl2.Item(Col2Qty, I).Value = AgL.VNull(.Rows(I)("Qty"))
                    Dgl2.Item(Col2RequisitionNo, I).Tag = AgL.XNull(.Rows(I)("Requisition"))
                    Dgl2.Item(Col2RequisitionNo, I).Value = AgL.XNull(.Rows(I)("ReqNo"))
                    Dgl2.Item(Col2RequisitionSr, I).Value = AgL.VNull(.Rows(I)("RequisitionSr"))
                    Dgl2.Item(Col2MeasurePerPcs, I).Value = Format(AgL.VNull(.Rows(I)("MeasurePerPcs")), "0.000")
                    Dgl2.Item(Col2MeasureUnit, I).Value = AgL.XNull(.Rows(I)("MeasureUnit"))
                    Dgl2.Item(Col2TotalMeasure, I).Value = Format(AgL.VNull(.Rows(I)("TotalMeasure")), "0.000")
                    Dgl2.Item(Col2RequireDate, I).Value = AgL.XNull(.Rows(I)("RequireDate"))
                Next
            End If
        End With

    End Sub

    Private Sub FrmProductionOrder_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Topctrl1.ChangeAgGridState(Dgl1, False)
        RbtIndentForPlanning.Checked = True
        AgL.WinSetting(Me, 600, 1000, 0, 0)
    End Sub

    Private Sub Dgl1_EditingControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.EditingControl_KeyDown
        Dim strCond As String = ""
        Try
            If Dgl1.CurrentCell Is Nothing Then Exit Sub
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Item
                    If Dgl1.AgHelpDataSet(Col1Item) Is Nothing Then
                        FCreateHelpItem(Col1Item)
                    End If

                Case Col1ItemCode
                    If Dgl1.AgHelpDataSet(Col1ItemCode) Is Nothing Then
                        FCreateHelpItem(Col1ItemCode)
                    End If


            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FCreateHelpItem(ByVal ColumnName As String)
        Dim strCond As String = ""
        Dim ContraV_TypeCondStr As String = ""

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
                strCond += " And CharIndex('|' || I.Item || '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_Item")) & "') <= 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemDivision")) <> "" Then
                strCond += " And CharIndex('|' || I.Div_Code || '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemDivision")) & "') > 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemSite")) <> "" Then
                strCond += " And CharIndex('|' || I.Site_Code || '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemSite")) & "') > 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ContraV_Type")) <> "" Then
                ContraV_TypeCondStr += " And CharIndex('|' || V_Type || '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ContraV_Type")) & "') > 0 "
            End If
        End If

        Select Case ColumnName
            Case Col1Item
                If RbtIndentForPlanning.Checked Then
                    mQry = " SELECT max(L.Item) AS Code, max(I.Description) AS ItemDesc, max(I.ManualCode) AS ItemCode, max(H.V_Type) || '-' || max ( Convert(NVarchar,H.ManualRefNo)) AS PlanningNo, L.MaterialPlanSr , L.MaterialPlan, Max(H.ProdOrder) as ProdOrder, Max(ProdOrder.ManualRefNo) as ProdOrderNo,  max(L.Unit) AS Unit, " &
                            " max(L.MeasurePerPcs) AS MeasurePerPcs, max(L.MeasureUnit) AS MeasureUnit, IfNull(sum(L.UserPurchPlanQty ),0) - IfNull(sum(D.IndQty ),0) AS PlanQty, sum(L.UserPurchPlanMeasure ) - IfNull(sum(D.IndMeasure ),0) AS PlanMeasure,  " &
                            " Max(U.DecimalPlaces) As QtyDecimalPlaces, Max(UM.DecimalPlaces) As MeasureDecimalPlaces " &
                            " FROM MaterialPlan H " &
                            " LEFT JOIN MaterialPlanDetail L ON L.DocId = H.DocID  " &
                            " LEFT JOIN ProdOrder ON H.ProdOrder = ProdOrder.DocID  " &
                            " LEFT JOIN Item I ON I.Code = L.Item  " &
                            " LEFT JOIN Unit U ON U.Code = L.Unit  " &
                            " LEFT JOIN Unit UM ON UM.Code = L.MeasureUnit  " &
                            " LEFT JOIN " &
                            " ( " &
                            " SELECT IND.MaterialPlan, IND.MaterialPlanSr, sum(IND.IndentQty) AS IndQty , SUM(IND.TotalIndentMeasure) AS IndMeasure  " &
                            " FROM PurchIndentDetail IND " &
                            " WHERE IfNull(IND.MaterialPlan,'') <> '' AND IND.DocId <> '" & mSearchCode & "'  " &
                            " GROUP BY IND.MaterialPlan, IND.MaterialPlanSr " &
                            " ) AS D ON D.MaterialPlan = L.DocId AND D.MaterialPlanSr = L.Sr " &
                            " WHERE IfNull(L.MaterialPlan,'') <> ''  " & strCond &
                            " GROUP BY L.MaterialPlan ,L.MaterialPlanSr " &
                            " HAVING IfNull(sum(L.UserPurchPlanQty ),0) - IfNull(sum(D.IndQty ),0) > 0 "
                    Dgl1.AgHelpDataSet(Col1Item, 6) = AgL.FillData(mQry, AgL.GCn)
                Else
                    mQry = " SELECT I.Code, I.Description AS ItemDesc, I.ManualCode AS ItemCode, '' ProdOrder, '' ProdOrderNo, '' AS PlanningNo, '' AS MaterialPlanSr, '' AS MaterialPlan, I.Unit, " &
                            " I.Measure As MeasurePerPcs, I.MeasureUnit, 0 AS PlanQty, 0 AS PlanMeasure, U.DecimalPlaces As QtyDecimalPlaces, U1.DecimalPlaces As MeasureDecimalPlaces " &
                            " FROM Item I " &
                            " LEFT JOIN Unit U On I.Unit = U.Code " &
                            " LEFT JOIN Unit U1 On I.MeasureUnit = U1.Code " &
                            " Where IfNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
                    Dgl1.AgHelpDataSet(Col1Item, 10) = AgL.FillData(mQry, AgL.GCn)
                End If

            Case Col1ItemCode
                If RbtIndentForPlanning.Checked Then
                    mQry = " SELECT max(L.Item) AS Code, max(I.ManualCode) AS ItemCode, max(I.Description) AS ItemDesc,  max(H.V_Type) || '-' || max ( Convert(NVarchar,H.V_No)) AS PlanningNo, L.MaterialPlanSr , L.MaterialPlan ,  max(L.Unit) AS Unit, " &
                            " max(L.MeasurePerPcs) AS MeasurePerPcs, max(L.MeasureUnit) AS MeasureUnit, IfNull(sum(L.UserPurchPlanQty ),0) - IfNull(sum(D.IndQty ),0) AS PlanQty, sum(L.UserPurchPlanMeasure ) - IfNull(sum(D.IndMeasure ),0) AS PlanMeasure, " &
                            " Max(U.DecimalPlaces) As QtyDecimalPlaces, Max(UM.DecimalPlaces) As MeasureDecimalPlaces " &
                            " FROM MaterialPlan H " &
                            " LEFT JOIN MaterialPlanDetail L ON L.DocId = H.DocID  " &
                            " LEFT JOIN Item I ON I.Code = L.Item  " &
                            " LEFT JOIN Unit U ON U.Code = L.Unit  " &
                            " LEFT JOIN Unit UM ON UM.Code = L.MeasureUnit  " &
                            " LEFT JOIN " &
                            " ( " &
                            " SELECT IND.MaterialPlan, IND.MaterialPlanSr, sum(IND.IndentQty) AS IndQty , SUM(IND.TotalIndentMeasure) AS IndMeasure  " &
                            " FROM PurchIndentDetail IND " &
                            " WHERE IfNull(IND.MaterialPlan,'') <> ''  " &
                            " GROUP BY IND.MaterialPlan, IND.MaterialPlanSr " &
                            " ) AS D ON D.MaterialPlan = L.DocId AND D.MaterialPlanSr = L.Sr " &
                            " WHERE IfNull(L.MaterialPlan,'') <> '' AND IND.DocId <> '" & mSearchCode & "' " & strCond &
                            " GROUP BY L.MaterialPlan ,L.MaterialPlanSr " &
                            " HAVING IfNull(sum(L.UserPurchPlanQty ),0) - IfNull(sum(D.IndQty ),0) > 0 "
                    Dgl1.AgHelpDataSet(Col1ItemCode, 6) = AgL.FillData(mQry, AgL.GCn)
                Else
                    mQry = " SELECT I.Code, I.ManualCode AS ItemCode, I.Description AS ItemDesc,  '' AS PlanningNo, '' AS MaterialPlanSr, '' AS MaterialPlan, I.Unit, " &
                            " I.Measure As MeasurePerPcs, I.MeasureUnit, 0 AS PlanQty, 0 AS PlanMeasure, U.DecimalPlaces As QtyDecimalPlaces, U1.DecimalPlaces As MeasureDecimalPlaces " &
                            " FROM Item I " &
                            " LEFT JOIN Unit U On I.Unit = U.Code " &
                            " LEFT JOIN Unit U1 On I.MeasureUnit = U1.Code " &
                            " Where IfNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
                    Dgl1.AgHelpDataSet(Col1ItemCode, 10) = AgL.FillData(mQry, AgL.GCn)
                End If
        End Select
    End Sub

    Private Sub DGL1_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles Dgl1.RowsAdded
        'sender(ColSNo, sender.Rows.Count - 1).Value = Trim(sender.Rows.Count)
        sender(ColSNo, e.RowIndex).Value = e.RowIndex + 1
    End Sub

    Private Sub FrmProductionOrder_BaseFunction_Calculation() Handles Me.BaseFunction_Calculation
        Dim I As Integer

        LblTotalQty.Text = 0 : LblTotalMeasure.Text = 0

        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1Item, I).Value <> "" Then
                Dgl1.Item(Col1TotalReqMeasure, I).Value = Format(Val(Dgl1.Item(Col1ReqQty, I).Value) * Val(Dgl1.Item(Col1MeasurePerPcs, I).Value), "0.00")
                Dgl1.Item(Col1TotalIndentMeasure, I).Value = Format(Val(Dgl1.Item(Col1IndentQty, I).Value) * Val(Dgl1.Item(Col1MeasurePerPcs, I).Value), "0.00")
                'Footer Calculation
                LblTotalQty.Text = Val(LblTotalQty.Text) + Val(Dgl1.Item(Col1IndentQty, I).Value)
                LblTotalMeasure.Text = Val(LblTotalMeasure.Text) + Val(Dgl1.Item(Col1TotalIndentMeasure, I).Value)
            End If
        Next
        LblTotalMeasure.Text = Format(Val(LblTotalMeasure.Text), "0.000")
        LblTotalQty.Text = Format(Val(LblTotalQty.Text), "0.000")
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        Dim I As Integer = 0

        If AgL.RequiredField(TxtDepartment, LblDepartment.Text) Then passed = False : Exit Sub
        If AgL.RequiredField(TxtIndentor, LblIndentor.Text) Then passed = False : Exit Sub

        If AgCL.AgIsBlankGrid(Dgl1, Dgl1.Columns(Col1Item).Index) Then passed = False : Exit Sub

        If AgCL.AgIsDuplicate(Dgl1, Dgl1.Columns(Col1Item).Index & "," & Dgl1.Columns(Col1PlanningNo).Index) Then passed = False : Exit Sub

        With Dgl1
            For I = 0 To .Rows.Count - 1
                If .Item(Col1Item, I).Value <> "" Then
                    If Val(.Item(Col1IndentQty, I).Value) = 0 Then
                        MsgBox("Qty Is 0 At Row No " & Dgl1.Item(ColSNo, I).Value & "")
                        .CurrentCell = .Item(Col1IndentQty, I) : Dgl1.Focus()
                        passed = False : Exit Sub
                    End If

                    If RbtIndentForPlanning.Checked = True Then
                        If .Item(Col1PlanningNo, I).Value = "" Then
                            MsgBox("Planning No. Is Black At Row No " & Dgl1.Item(ColSNo, I).Value & "")
                            .CurrentCell = .Item(Col1Item, I) : Dgl1.Focus()
                            passed = False : Exit Sub
                        End If
                    Else
                        If .Item(Col1PlanningNo, I).Value <> "" Then
                            MsgBox("Planning No. Is Invalid At Row No " & Dgl1.Item(ColSNo, I).Value & "")
                            .CurrentCell = .Item(Col1Item, I) : Dgl1.Focus()
                            passed = False : Exit Sub
                        End If
                    End If

                    If AgL.VNull(DtV_TypeSettings.Rows(0)("IsMandatory_Rate")) = 1 Then
                        If Val(.Item(Col1Rate, I).Value) = 0 Then
                            MsgBox("Rate Is 0 At Row No " & Dgl1.Item(ColSNo, I).Value & "")
                            .CurrentCell = .Item(Col1Rate, I) : Dgl1.Focus()
                            passed = False : Exit Sub
                        End If
                    End If
                End If
            Next
        End With
    End Sub

    Private Sub FrmProductionOrder_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
        Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
        LblTotalMeasure.Text = 0 : LblTotalQty.Text = 0
    End Sub

    Private Sub Validating_Item(ByVal Code As String, ByVal mRow As Integer, ByVal ColoumnName As String)
        Dim DrTemp As DataRow() = Nothing
        Dim DtTemp As DataTable = Nothing
        Try
            If Dgl1.Item(ColoumnName, mRow).Value.ToString.Trim = "" Or Dgl1.AgSelectedValue(ColoumnName, mRow).ToString.Trim = "" Then
                Dgl1.Item(Col1Unit, mRow).Value = ""
                Dgl1.Item(Col1MeasurePerPcs, mRow).Value = 0
                Dgl1.Item(Col1MeasureUnit, mRow).Value = ""
                Dgl1.Item(Col1CurrentStock, mRow).Value = ""
                Dgl1.Item(Col1ReqQty, mRow).Value = 0
                Dgl1.Item(Col1IndentQty, mRow).Value = 0
                Dgl1.Item(Col1TotalIndentMeasure, mRow).Value = 0
                Dgl1.Item(Col1TotalReqMeasure, mRow).Value = 0
                Dgl1.Item(Col1PlanningNo, mRow).Value = ""
                Dgl1.Item(Col1PlanningNo, mRow).Tag = ""
                Dgl1.Item(Col1PlanningSr, mRow).Value = ""
                Dgl1.Item(Col1RequireDate, mRow).Value = ""
                Dgl1.Item(Col1Item, mRow).Value = ""
                Dgl1.Item(Col1Item, mRow).Tag = ""
                Dgl1.Item(Col1ItemCode, mRow).Value = ""
                Dgl1.Item(Col1ItemCode, mRow).Tag = ""
            Else
                If Dgl1.AgHelpDataSet(ColoumnName) IsNot Nothing Then
                    DrTemp = Dgl1.AgHelpDataSet(ColoumnName).Tables(0).Select("Code = '" & Code & "'")
                    Dgl1.Item(Col1Unit, mRow).Value = AgL.XNull(DrTemp(0)("Unit"))
                    Dgl1.Item(Col1MeasurePerPcs, mRow).Value = AgL.VNull(DrTemp(0)("MeasurePerPcs"))
                    Dgl1.Item(Col1MeasureUnit, mRow).Value = AgL.XNull(DrTemp(0)("MeasureUnit"))
                    Dgl1.Item(Col1ReqQty, mRow).Value = Format(AgL.VNull(DrTemp(0)("PlanQty")), "0.".PadRight(AgL.VNull(DrTemp(0)("QtyDecimalPlaces")) + 2, "0"))
                    Dgl1.Item(Col1IndentQty, mRow).Value = Format(AgL.VNull(DrTemp(0)("PlanQty")), "0.".PadRight(AgL.VNull(DrTemp(0)("QtyDecimalPlaces")) + 2, "0"))
                    Dgl1.Item(Col1TotalIndentMeasure, mRow).Value = Format(AgL.VNull(DrTemp(0)("PlanMeasure")), "0.".PadRight(AgL.VNull(DrTemp(0)("MeasureDecimalPlaces")) + 2, "0"))
                    Dgl1.Item(Col1TotalReqMeasure, mRow).Value = Format(AgL.VNull(DrTemp(0)("PlanMeasure")), "0.".PadRight(AgL.VNull(DrTemp(0)("MeasureDecimalPlaces")) + 2, "0"))
                    Dgl1.Item(Col1ProdOrder, mRow).Tag = AgL.XNull(DrTemp(0)("ProdOrder"))
                    Dgl1.Item(Col1ProdOrder, mRow).Value = AgL.XNull(DrTemp(0)("ProdOrderNo"))
                    Dgl1.Item(Col1PlanningNo, mRow).Value = AgL.XNull(DrTemp(0)("PlanningNo"))
                    Dgl1.Item(Col1PlanningNo, mRow).Tag = AgL.XNull(DrTemp(0)("MaterialPlan"))
                    Dgl1.Item(Col1PlanningSr, mRow).Value = AgL.XNull(DrTemp(0)("MaterialPlanSr"))
                    Dgl1.Item(Col1CurrentStock, mRow).Value = AgTemplate.ClsMain.FunRetStock(Dgl1.AgSelectedValue(Col1Item, mRow), mInternalCode, , , , , TxtV_Date.Text)
                    Dgl1.Item(Col1Item, mRow).Value = AgL.XNull(DrTemp(0)("ItemDesc"))
                    Dgl1.Item(Col1Item, mRow).Tag = AgL.XNull(DrTemp(0)("Code"))
                    Dgl1.Item(Col1ItemCode, mRow).Value = AgL.XNull(DrTemp(0)("ItemCode"))
                    Dgl1.Item(Col1ItemCode, mRow).Tag = AgL.XNull(DrTemp(0)("Code"))
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " On Validating_Item Function ")
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
                    Dgl1.Item(Col1RequireDate, mRowIndex).Value = TxtV_Date.Text
                Case Col1ItemCode
                    Validating_Item(Dgl1.AgSelectedValue(Col1ItemCode, mRowIndex), mRowIndex, Col1ItemCode)
                    Dgl1.Item(Col1RequireDate, mRowIndex).Value = TxtV_Date.Text
            End Select
            Call Calculation()
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

    Private Function FGetRelationalData() As Boolean
        Try

            Dim bRData As String
            '// Check for relational data in Purchase Quotation
            mQry = " DECLARE @Temp NVARCHAR(Max); "
            mQry += " SET @Temp=''; "
            mQry += " SELECT  @Temp=@Temp +  X.VNo || ', ' FROM (SELECT DISTINCT H.V_Type || '-' || Convert(VARCHAR,H.V_No) AS VNo From PurchQuotationDetail  L LEFT JOIN PurchQuotation H ON L.DocId = H.DocID WHERE L.PurchIndent  = '" & TxtDocId.Text & "') AS X  "
            mQry += " SELECT @Temp as RelationalData "
            bRData = AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar
            If bRData.Trim <> "" Then
                MsgBox(" Purchase Quotation " & bRData & " created against Indent No. " & TxtV_Type.Tag & "-" & TxtV_No.Text & ". Can't Modify Entry")
                FGetRelationalData = True
                Exit Function
            End If

            '// Check for relational data in Purchase Order
            mQry = " DECLARE @Temp NVARCHAR(Max); "
            mQry += " SET @Temp=''; "
            mQry += " SELECT  @Temp=@Temp +  X.VNo || ', ' FROM (SELECT DISTINCT H.V_Type || '-' || Convert(VARCHAR,H.V_No) AS VNo From PurchOrderDetail  L LEFT JOIN PurchOrder H ON L.DocId = H.DocID WHERE L.PurchIndent  = '" & TxtDocId.Text & "') AS X  "
            mQry += " SELECT @Temp as RelationalData "
            bRData = AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar
            If bRData.Trim <> "" Then
                MsgBox(" Purchase Order " & bRData & " created against Indent No. " & TxtV_Type.Tag & "-" & TxtV_No.Text & ". Can't Modify Entry")
                FGetRelationalData = True
                Exit Function
            End If


        Catch ex As Exception
            MsgBox(ex.Message & " in FGetRelationalData in TempRequisition")
            FGetRelationalData = True
        End Try
    End Function


    Private Sub TempPurchIndent_BaseEvent_Topctrl_tbEdit(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbEdit
        Passed = Not FGetRelationalData()
    End Sub

    Private Sub TempPurchIndent_BaseEvent_Topctrl_tbDel(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbDel
        Passed = Not FGetRelationalData()
    End Sub

    Private Sub Dgl1_CellMouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles Dgl1.CellMouseMove
        Try
            If AgL.PubDtEnviro.Rows.Count > 0 Then
                If AgL.VNull(AgL.PubDtEnviro.Rows(0)("IsLotNoApplicable")) = 0 Then Exit Sub
            End If

            Select Case Dgl1.Columns(e.ColumnIndex).Name
                Case Col1CurrentStock
                    Dgl1.Cursor = Cursors.Hand

                Case Else
                    Dgl1.Cursor = Cursors.Default
            End Select
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Dgl1_CellContentClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellContentClick
        Dim FrmObj As Form = Nothing
        Try
            If AgL.PubDtEnviro.Rows.Count > 0 Then
                If AgL.VNull(AgL.PubDtEnviro.Rows(0)("IsLotNoApplicable")) = 0 Then Exit Sub
            End If

            Select Case Dgl1.Columns(e.ColumnIndex).Name
                'Case Col1CurrentStock
                '    FrmObj = New AgTemplate.FrmLotWiseStock()
                '    CType(FrmObj, AgTemplate.FrmLotWiseStock).Item = Dgl1.AgSelectedValue(Col1Item, e.RowIndex)
                '    CType(FrmObj, AgTemplate.FrmLotWiseStock).ItemName = Dgl1.Item(Col1Item, e.RowIndex).Value
                '    CType(FrmObj, AgTemplate.FrmLotWiseStock).Qty = Val(Dgl1.Item(Col1CurrentStock, e.RowIndex).Value)
                '    CType(FrmObj, AgTemplate.FrmLotWiseStock).V_Date = TxtV_Date.Text
                '    FrmObj.ShowDialog()
            End Select
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Txt_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtDepartment.KeyDown, TxtIndentor.KeyDown
        Dim strCond$ = ""
        Try
            If e.KeyCode = Keys.Enter Then Exit Sub
            Select Case sender.Name
                Case TxtDepartment.Name
                    If TxtDepartment.AgHelpDataSet Is Nothing Then
                        mQry = " Select H.Code As Code, H.Description As Department  " &
                               " From Department H " &
                               " Where IfNull(H.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') =  '" & AgTemplate.ClsMain.EntryStatus.Active & "'  "
                        TxtDepartment.AgHelpDataSet(, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
                    End If

                Case TxtIndentor.Name
                    If e.KeyCode <> Keys.Enter Then
                        If sender.AgHelpDataSet Is Nothing Then
                            FCreateHelpSubgroup(sender)
                        End If
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

        mQry = " SELECT H.SubCode AS Code, H.DispName AS [Employee Name], H.ManualCode AS [Employee Code], " &
            " H.Currency, C1.Description As CurrencyDesc, H.Nature, H.SalesTaxPostingGroup " &
           " FROM Subgroup H " &
           " LEFT JOIN City C ON H.CityCode = C.CityCode  " &
           " LEFT JOIN Currency C1 On H.Currency = C1.Code " &
           " Where IfNull(H.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') =  '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
        sender.AgHelpDataSet(, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
    End Sub

    Private Sub BtnFillIndentDetail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnFillIndentDetail.Click
        If Topctrl1.Mode = "Browse" Then Exit Sub
        If RbtIndentDirect.Checked = True Then Exit Sub
        If RbtIndentForPlanning.Checked = True Then
            Dim strTicked As String
            strTicked = FHPGD_PlanningNo()
            If strTicked <> "" Then
                ProcFillPlanningDetails(strTicked)
            End If
        ElseIf RBtnIndForRequisition.Checked = True Then
            Dim strTicked As String
            strTicked = FHPGD_RequisitionNo()
            If strTicked <> "" Then
                ProcFillRequisitionDetails(strTicked)
            End If
        End If
    End Sub

    Private Function FHPGD_PlanningNo() As String
        Dim FRH_Multiple As DMHelpGrid.FrmHelpGrid_Multi
        Dim StrSendText As String, bCondStr$ = ""
        Dim StrRtn As String = ""

        StrSendText = RbtIndentForPlanning.Tag

        AgL.Dman_ExecuteNonQry("UPDATE MaterialPlanDetail SET MaterialPlan = docId, MaterialPlanSr = sr WHERE MaterialPlan IS NULL  ", AgL.GCn)

        If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ContraV_Type")) <> "" Then
            bCondStr += " And CharIndex('|' & H.V_Type & '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ContraV_Type")) & "') > 0 "
        End If

        If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")) <> "" Then
            bCondStr += " And CharIndex('|' & I.ItemType & '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")) & "') > 0 "
        End If

        If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemGroup")) <> "" Then
            bCondStr += " And CharIndex('|' & I.ItemGroup & '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemGroup")) & "') > 0 "
        End If

        If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_ItemGroup")) <> "" Then
            bCondStr += " And CharIndex('|' & I.ItemGroup & '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_ItemGroup")) & "') <= 0 "
        End If

        If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Item")) <> "" Then
            bCondStr += " And CharIndex('|' & I.Code & '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Item")) & "') > 0 "
        End If

        If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_Item")) <> "" Then
            bCondStr += " And CharIndex('|' & I.Code & '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_Item")) & "') <= 0 "
        End If

        mQry = " SELECT DISTINCT 'o' AS Tick, L.MaterialPlan, max(H.Planning_No) AS Planning_No, max(H.V_Date) AS Planning_Date " &
                " FROM  " &
                " ( " &
                " SELECT H.DocID, H.V_Type || '-' || convert(NVARCHAR,H.ManualRefNo) AS Planning_No, H.V_Type, H.V_Date  " &
                " FROM MaterialPlan H   " &
                " WHERE H.Div_Code = '" & TxtDivision.Tag & "'  AND H.Site_Code ='" & TxtSite_Code.Tag & "'   " &
                " AND H.V_Date <= '" & TxtV_Date.Text & "'  " &
                " ) H " &
                " LEFT JOIN MaterialPlanDetail L ON L.MaterialPlan = H.DocID  " &
                " LEFT JOIN Item I ON I.Code = L.Item " &
                " LEFT JOIN " &
                " ( " &
                " SELECT IND.MaterialPlan, IND.MaterialPlanSr, sum(IND.IndentQty) AS IndQty  " &
                " FROM PurchIndentDetail IND " &
                " WHERE IfNull(IND.MaterialPlan,'') <> '' AND IND.DocId <> '" & mSearchCode & "' " &
                " GROUP BY IND.MaterialPlan, IND.MaterialPlanSr " &
                " ) AS D ON D.MaterialPlan = L.DocId AND D.MaterialPlanSr = L.Sr " &
                " WHERE 1=1 " & bCondStr &
                " GROUP BY L.MaterialPlan " &
                " HAVING IfNull(sum(L.UserPurchPlanQty ),0) - IfNull(sum(D.IndQty ),0) > 0 "


        FRH_Multiple = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(AgL.FillData(mQry, AgL.GCn).TABLES(0)), "", 400, 320, , , False)
        FRH_Multiple.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple.FFormatColumn(1, , 0, , False)
        FRH_Multiple.FFormatColumn(2, "Planning No.", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(3, "Planning Date", 100, DataGridViewContentAlignment.MiddleLeft)

        FRH_Multiple.StartPosition = FormStartPosition.CenterScreen
        FRH_Multiple.ShowDialog()

        If FRH_Multiple.BytBtnValue = 0 Then
            StrRtn = FRH_Multiple.FFetchData(1, "'", "'", ",", True)
        End If
        FHPGD_PlanningNo = StrRtn

        FRH_Multiple = Nothing
    End Function

    Private Function FHPGD_RequisitionNo() As String
        Dim FRH_Multiple As DMHelpGrid.FrmHelpGrid_Multi
        Dim StrSendText As String, bCondStr$ = ""
        Dim StrRtn As String = ""

        StrSendText = RBtnIndForRequisition.Tag

        If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ContraV_Type")) <> "" Then
            bCondStr += " And CharIndex('|' & H.V_Type & '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ContraV_Type")) & "') > 0 "
        End If

        If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")) <> "" Then
            bCondStr += " And CharIndex('|' & I.ItemType & '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")) & "') > 0 "
        End If

        If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemGroup")) <> "" Then
            bCondStr += " And CharIndex('|' & I.ItemGroup & '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemGroup")) & "') > 0 "
        End If

        If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_ItemGroup")) <> "" Then
            bCondStr += " And CharIndex('|' & I.ItemGroup & '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_ItemGroup")) & "') <= 0 "
        End If

        If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Item")) <> "" Then
            bCondStr += " And CharIndex('|' & I.Code & '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Item")) & "') > 0 "
        End If

        If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_Item")) <> "" Then
            bCondStr += " And CharIndex('|' & I.Code & '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_Item")) & "') <= 0 "
        End If

        mQry = " SELECT 'o' AS Tick, H.DocID, max(H.ReferenceNo) AS ReqNo, max(H.V_Date) AS ReqDate, max(SG.DispName) AS ReqBy " &
                " FROM Requisition H " &
                " LEFT JOIN RequisitionDetail L ON L.DocId = H.DocID  " &
                " LEFT JOIN Item I on I.Code = L.Item " &
                " LEFT JOIN (  " &
                " SELECT S.Requisition , S.RequisitionSr , sum(S.Qty) AS RecQty FROM StockHeadDetail S   " &
                " LEFT JOIN StockHead SH ON SH.DocID = S.DocID  WHERE IfNull(S.Requisition,'') <> ''  " &
                " GROUP BY S.Requisition , S.RequisitionSr  ) VIS ON VIS.Requisition = L.DocId AND VIS.RequisitionSr = L.Sr  " &
                " LEFT JOIN (  SELECT L.Requisition, L.RequisitionSr, sum(L.Qty) AS IndQty  " &
                " FROM PurchIndentReq L   " &
                " Where L.DocId <> '" & mSearchCode & "' " &
                " GROUP BY L.Requisition, L.RequisitionSr  ) VPI ON VPI.Requisition = L.DocId AND VPI.RequisitionSr = L.Sr " &
                " LEFT JOIN SubGroup SG ON SG.SubCode = H.RequisitionBy  " &
                " WHERE  IfNull(L.ApproveQty,0) - IfNull(VIS.RecQty,0)- IfNull(VPI.IndQty,0) > 0 " & bCondStr &
                " GROUP BY H.DocID "

        FRH_Multiple = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(AgL.FillData(mQry, AgL.GCn).TABLES(0)), "", 400, 420, , , False)
        FRH_Multiple.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple.FFormatColumn(1, , 0, , False)
        FRH_Multiple.FFormatColumn(2, "Requisition No.", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(3, "Requisition Date", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(4, "Requisition By", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.StartPosition = FormStartPosition.CenterScreen
        FRH_Multiple.ShowDialog()

        If FRH_Multiple.BytBtnValue = 0 Then
            StrRtn = FRH_Multiple.FFetchData(1, "'", "'", ",", True)
        End If
        FHPGD_RequisitionNo = StrRtn

        FRH_Multiple = Nothing
    End Function


    Private Sub ProcFillPlanningDetails(ByVal bPurchOrderStr As String)
        Dim DtTemp As DataTable = Nothing
        Dim bReferenceDocId$ = "", bCondStr$ = ""
        Dim I As Integer = 0
        Try
            If Not AgL.StrCmp(Topctrl1.Mode, "Add") Then Exit Sub

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ContraV_Type")) <> "" Then
                bCondStr += " And CharIndex('|' & H.V_Type & '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ContraV_Type")) & "') > 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")) <> "" Then
                bCondStr += " And CharIndex('|' & I.ItemType & '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")) & "') > 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemGroup")) <> "" Then
                bCondStr += " And CharIndex('|' & I.ItemGroup & '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemGroup")) & "') > 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_ItemGroup")) <> "" Then
                bCondStr += " And CharIndex('|' & I.ItemGroup & '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_ItemGroup")) & "') <= 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Item")) <> "" Then
                bCondStr += " And CharIndex('|' & I.Code & '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Item")) & "') > 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_Item")) <> "" Then
                bCondStr += " And CharIndex('|' & I.Code & '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_Item")) & "') <= 0 "
            End If

            mQry = " SELECT L.MaterialPlan ,L.MaterialPlanSr, Max(H.ProdOrder) as ProdOrder, Max(ProdOrder.ManualRefNo) as ProdOrderNo, max(H.V_Type) || '-' || max ( Convert(NVarchar,H.V_No)) AS PlanningNo,  max(L.Item) AS Item, max(I.Description) AS ItemDesc, max(I.ManualCode) AS ItemCode  ,max(L.Unit) AS Unit, max(L.MeasurePerPcs) AS MeasurePerPcs, " &
                    " max(L.MeasureUnit) AS MeasureUnit, IfNull(sum(L.UserPurchPlanQty ),0) - IfNull(sum(D.IndQty ),0) AS PlanQty, sum(L.UserPurchPlanMeasure ) - IfNull(sum(D.IndMeasure ),0) AS PlanMeasure,  " &
                    " IfNull(Max(U.DecimalPlaces),0) As QtyDecimalPlaces, IfNull(Max(UM.DecimalPlaces),0) As MeasureDecimalPlaces " &
                    " FROM MaterialPlan H " &
                    " LEFT JOIN MaterialPlanDetail L ON L.DocId = H.DocID  " &
                    " LEFT JOIN ProdOrder ON H.ProdOrder = ProdOrder.DocID  " &
                    " LEFT JOIN Item I ON I.Code = L.Item  " &
                    " LEFT JOIN Unit U ON U.Code = L.Unit  " &
                    " LEFT JOIN Unit UM ON UM.Code = L.MeasureUnit  " &
                    " LEFT JOIN " &
                    " ( " &
                    " SELECT IND.MaterialPlan, IND.MaterialPlanSr, sum(IND.IndentQty) AS IndQty , SUM(IND.TotalIndentMeasure) AS IndMeasure  " &
                    " FROM PurchIndentDetail IND " &
                    " WHERE IfNull(IND.MaterialPlan,'') <> '' AND IND.DocId <> '" & mSearchCode & "' " &
                    " GROUP BY IND.MaterialPlan, IND.MaterialPlanSr " &
                    " ) AS D ON D.MaterialPlan = L.DocId AND D.MaterialPlanSr = L.Sr " &
                    " WHERE IfNull(L.MaterialPlan,'') <> '' AND L.MaterialPlan IN ( " & bPurchOrderStr & " ) " & bCondStr &
                    " GROUP BY L.MaterialPlan ,L.MaterialPlanSr " &
                    " HAVING IfNull(sum(L.UserPurchPlanQty ),0) - IfNull(sum(D.IndQty ),0) > 0 "
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
            With DtTemp
                Dgl1.RowCount = 1
                Dgl1.Rows.Clear()
                If .Rows.Count > 0 Then
                    For I = 0 To .Rows.Count - 1
                        Dgl1.Rows.Add()
                        Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count - 1
                        Dgl1.Item(Col1ItemCode, I).Tag = AgL.XNull(.Rows(I)("Item"))
                        Dgl1.Item(Col1ItemCode, I).Value = AgL.XNull(.Rows(I)("ItemCode"))
                        Dgl1.Item(Col1Item, I).Tag = AgL.XNull(.Rows(I)("Item"))
                        Dgl1.Item(Col1Item, I).Value = AgL.XNull(.Rows(I)("ItemDesc"))
                        Dgl1.Item(Col1ProdOrder, I).Value = AgL.XNull(.Rows(I)("ProdOrderNo"))
                        Dgl1.Item(Col1ProdOrder, I).Tag = AgL.XNull(.Rows(I)("ProdOrder"))
                        Dgl1.Item(Col1PlanningNo, I).Value = AgL.XNull(.Rows(I)("PlanningNo"))
                        Dgl1.Item(Col1PlanningNo, I).Tag = AgL.XNull(.Rows(I)("MaterialPlan"))
                        Dgl1.Item(Col1PlanningSr, I).Value = AgL.VNull(.Rows(I)("MaterialPlanSr"))
                        Dgl1.Item(Col1Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                        Dgl1.Item(Col1QtyDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("QtyDecimalPlaces"))
                        Dgl1.Item(Col1ReqQty, I).Value = Format(AgL.VNull(.Rows(I)("PlanQty")), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
                        Dgl1.Item(Col1IndentQty, I).Value = Format(AgL.VNull(.Rows(I)("PlanQty")), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
                        Dgl1.Item(Col1MeasureDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("MeasureDecimalPlaces"))
                        Dgl1.Item(Col1MeasureUnit, I).Value = AgL.XNull(.Rows(I)("MeasureUnit"))
                        Dgl1.Item(Col1MeasurePerPcs, I).Value = Format(AgL.VNull(.Rows(I)("MeasurePerPcs")), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                        Dgl1.Item(Col1TotalReqMeasure, I).Value = Format(AgL.VNull(.Rows(I)("PlanMeasure")), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                        Dgl1.Item(Col1TotalIndentMeasure, I).Value = Format(AgL.VNull(.Rows(I)("PlanMeasure")), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                        Dgl1.Item(Col1CurrentStock, I).Value = AgTemplate.ClsMain.FunRetStock(Dgl1.AgSelectedValue(Col1Item, I), mInternalCode, , , , , TxtV_Date.Text)
                        Dgl1.Item(Col1RequireDate, I).Value = TxtV_Date.Text
                    Next I
                End If
            End With
            Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub ProcFillRequisitionDetails(ByVal bPurchOrderStr As String)
        Dim DtTemp As DataTable = Nothing
        Dim bReferenceDocId$ = "", bCondStr$ = ""
        Dim I As Integer = 0
        Try
            If Not AgL.StrCmp(Topctrl1.Mode, "Add") Then Exit Sub

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ContraV_Type")) <> "" Then
                bCondStr += " And CharIndex('|' & H.V_Type & '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ContraV_Type")) & "') > 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")) <> "" Then
                bCondStr += " And CharIndex('|' & I.ItemType & '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")) & "') > 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemGroup")) <> "" Then
                bCondStr += " And CharIndex('|' & I.ItemGroup & '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemGroup")) & "') > 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_ItemGroup")) <> "" Then
                bCondStr += " And CharIndex('|' & I.ItemGroup & '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_ItemGroup")) & "') <= 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Item")) <> "" Then
                bCondStr += " And CharIndex('|' & I.Code & '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Item")) & "') > 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_Item")) <> "" Then
                bCondStr += " And CharIndex('|' & I.Code & '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_Item")) & "') <= 0 "
            End If

            mQry = " SELECT H.DocID, L.Sr, max(H.ReferenceNo) AS ReqNo, Max(L.Item) AS Item, max(I.Description) AS ItemDesc,  max(I.ManualCode) AS ItemCode, " &
                    " max(L.Unit) AS Unit, max(L.MeasurePerPcs) AS MeasurePerPcs, max(L.MeasureUnit) AS MeasureUnit, Max(L.RequireDate) AS RequireDate, " &
                    " IfNull(Max(U.DecimalPlaces),0) As QtyDecimalPlaces, IfNull(Max(UM.DecimalPlaces),0) As MeasureDecimalPlaces, " &
                    " IfNull(sum(L.ApproveQty),0) - IfNull(sum(VIS.RecQty),0)- IfNull(sum(VPI.IndQty),0) AS BalQty " &
                    " FROM Requisition H " &
                    " LEFT JOIN RequisitionDetail L ON L.DocId = H.DocID  " &
                    " LEFT JOIN Item I ON I.Code = L.Item  " &
                    " LEFT JOIN Unit U ON U.Code = L.Unit   " &
                    " LEFT JOIN Unit UM ON UM.Code = L.MeasureUnit  " &
                    " LEFT JOIN (   " &
                    " SELECT S.Requisition , S.RequisitionSr , sum(S.Qty) AS RecQty FROM StockHeadDetail S   " &
                    " LEFT JOIN StockHead SH ON SH.DocID = S.DocID  WHERE IfNull(S.Requisition,'') <> ''    " &
                    " GROUP BY S.Requisition , S.RequisitionSr  ) VIS ON VIS.Requisition = L.DocId AND VIS.RequisitionSr = L.Sr  " &
                    " LEFT JOIN (  SELECT L.Requisition, L.RequisitionSr, sum(L.Qty) AS IndQty  " &
                    " FROM PurchIndentReq L   " &
                    " Where 1=1 AND L.DocId <> '" & mSearchCode & "' " &
                    " GROUP BY L.Requisition, L.RequisitionSr  ) VPI ON VPI.Requisition = L.DocId AND VPI.RequisitionSr = L.Sr " &
                    " LEFT JOIN SubGroup SG ON SG.SubCode = H.RequisitionBy  " &
                    " WHERE  IfNull(L.ApproveQty,0) - IfNull(VIS.RecQty,0)- IfNull(VPI.IndQty,0) > 0 " &
                    " AND H.DocID IN ( " & bPurchOrderStr & " ) " & bCondStr &
                    " GROUP BY H.DocID, L.Sr  "

            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
            With DtTemp
                Dgl2.RowCount = 1
                Dgl2.Rows.Clear()
                If .Rows.Count > 0 Then
                    For I = 0 To .Rows.Count - 1
                        Dgl2.Rows.Add()
                        Dgl2.Item(ColSNo, I).Value = Dgl2.Rows.Count - 1
                        Dgl2.Item(Col2Item, I).Tag = AgL.XNull(.Rows(I)("Item"))
                        Dgl2.Item(Col2Item, I).Value = AgL.XNull(.Rows(I)("ItemDesc"))
                        Dgl2.Item(Col2Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                        Dgl2.Item(Col2Qty, I).Value = AgL.VNull(.Rows(I)("BalQty"))
                        Dgl2.Item(Col2MeasureUnit, I).Value = AgL.XNull(.Rows(I)("MeasureUnit"))
                        Dgl2.Item(Col2RequisitionNo, I).Value = AgL.XNull(.Rows(I)("ReqNo"))
                        Dgl2.Item(Col2RequisitionNo, I).Tag = AgL.XNull(.Rows(I)("DocId"))
                        Dgl2.Item(Col2RequisitionSr, I).Value = AgL.VNull(.Rows(I)("Sr"))
                        Dgl2.Item(Col2MeasurePerPcs, I).Value = Format(AgL.VNull(.Rows(I)("MeasurePerPcs")), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                        Dgl2.Item(Col2RequireDate, I).Value = AgL.XNull(.Rows(I)("RequireDate"))
                    Next I
                End If
            End With

            mQry = " SELECT max(H.ReferenceNo) AS ReqNo, L.Item, max(I.Description) AS ItemDesc,  max(I.ManualCode) AS ItemCode, " &
                    " max(L.Unit) AS Unit, max(L.MeasurePerPcs) AS MeasurePerPcs, max(L.MeasureUnit) AS MeasureUnit, Max(L.RequireDate) AS RequireDate, " &
                    " IfNull(Max(U.DecimalPlaces),0) As QtyDecimalPlaces, IfNull(Max(UM.DecimalPlaces),0) As MeasureDecimalPlaces, " &
                    " IfNull(sum(L.ApproveQty),0) - IfNull(sum(VIS.RecQty),0)- IfNull(sum(VPI.IndQty),0) AS BalQty " &
                    " FROM Requisition H " &
                    " LEFT JOIN RequisitionDetail L ON L.DocId = H.DocID  " &
                    " LEFT JOIN Item I ON I.Code = L.Item  " &
                    " LEFT JOIN Unit U ON U.Code = L.Unit   " &
                    " LEFT JOIN Unit UM ON UM.Code = L.MeasureUnit  " &
                    " LEFT JOIN (   " &
                    " SELECT S.Requisition , S.RequisitionSr , sum(S.Qty) AS RecQty FROM StockHeadDetail S   " &
                    " LEFT JOIN StockHead SH ON SH.DocID = S.DocID  WHERE IfNull(S.Requisition,'') <> ''    " &
                    " GROUP BY S.Requisition , S.RequisitionSr  ) VIS ON VIS.Requisition = L.DocId AND VIS.RequisitionSr = L.Sr  " &
                    " LEFT JOIN (  SELECT L.Requisition, L.RequisitionSr, sum(L.Qty) AS IndQty  " &
                    " FROM PurchIndentReq L   " &
                    " Where 1=1 AND L.DocId <> '" & mSearchCode & "' " &
                    " GROUP BY L.Requisition, L.RequisitionSr  ) VPI ON VPI.Requisition = L.DocId AND VPI.RequisitionSr = L.Sr " &
                    " LEFT JOIN SubGroup SG ON SG.SubCode = H.RequisitionBy  " &
                    " WHERE  IfNull(L.ApproveQty,0) - IfNull(VIS.RecQty,0)- IfNull(VPI.IndQty,0) > 0 " &
                    " AND H.DocID IN ( " & bPurchOrderStr & " ) " & bCondStr &
                    " GROUP BY L.Item  "

            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
            With DtTemp
                Dgl1.RowCount = 1
                Dgl1.Rows.Clear()
                If .Rows.Count > 0 Then
                    For I = 0 To .Rows.Count - 1
                        Dgl1.Rows.Add()
                        Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count - 1
                        Dgl1.Item(Col1ItemCode, I).Tag = AgL.XNull(.Rows(I)("Item"))
                        Dgl1.Item(Col1ItemCode, I).Value = AgL.XNull(.Rows(I)("ItemCode"))
                        Dgl1.Item(Col1Item, I).Tag = AgL.XNull(.Rows(I)("Item"))
                        Dgl1.Item(Col1Item, I).Value = AgL.XNull(.Rows(I)("ItemDesc"))
                        Dgl1.Item(Col1Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                        Dgl1.Item(Col1QtyDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("QtyDecimalPlaces"))
                        Dgl1.Item(Col1ReqQty, I).Value = AgL.VNull(.Rows(I)("BalQty"))
                        Dgl1.Item(Col1IndentQty, I).Value = AgL.VNull(.Rows(I)("BalQty"))
                        Dgl1.Item(Col1MeasureDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("MeasureDecimalPlaces"))
                        Dgl1.Item(Col1MeasureUnit, I).Value = AgL.XNull(.Rows(I)("MeasureUnit"))
                        Dgl1.Item(Col1MeasurePerPcs, I).Value = Format(AgL.VNull(.Rows(I)("MeasurePerPcs")), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                        Dgl1.Item(Col1CurrentStock, I).Value = AgTemplate.ClsMain.FunRetStock(Dgl1.AgSelectedValue(Col1Item, I), mInternalCode, , , , , TxtV_Date.Text)
                        Dgl1.Item(Col1RequireDate, I).Value = AgL.XNull(.Rows(I)("RequireDate"))
                    Next I
                End If
            End With

            Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FrmPurchIndent_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub

    Private Sub FrmPurchIndent_BaseEvent_Topctrl_tbPrn(ByVal SearchCode As String) Handles Me.BaseEvent_Topctrl_tbPrn
        ClsMain.FPrintThisDocument(Me, TxtV_Type.Tag)
    End Sub

    Private Sub FrmPurchIndent_BaseEvent_Topctrl_tbRef() Handles Me.BaseEvent_Topctrl_tbRef
        If TxtIndentor.AgHelpDataSet IsNot Nothing Then TxtIndentor.AgHelpDataSet.Dispose() : TxtIndentor.AgHelpDataSet = Nothing
        If TxtDepartment.AgHelpDataSet IsNot Nothing Then TxtDepartment.AgHelpDataSet.Dispose() : TxtDepartment.AgHelpDataSet = Nothing
        If Dgl1.AgHelpDataSet(Col1Item) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1Item).Dispose() : Dgl1.AgHelpDataSet(Col1Item) = Nothing
        If Dgl1.AgHelpDataSet(Col1ItemCode) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1ItemCode).Dispose() : Dgl1.AgHelpDataSet(Col1ItemCode) = Nothing
    End Sub
End Class
