Imports System.Data.SQLite
Public Class FrmRequisition
    Inherits AgTemplate.TempTransaction
    Public mQry$

    Public Event BaseFunction_MoveRecLine(ByVal SearchCode As String, ByVal Sr As Integer, ByVal mGridRow As Integer)
    Public Event BaseEvent_Save_InTransLine(ByVal SearchCode As String, ByVal Sr As Integer, ByVal mGridRow As Integer, ByVal Conn As SqliteConnection, ByVal Cmd As SqliteCommand)
    Protected Const ColSNo As String = "S.No."
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Protected Const Col1ItemCode As String = "Item Code"
    Protected Const Col1Item As String = "Item"
    Protected Const Col1QtyDecimalPlaces As String = "Qty Decimal Places"
    Protected Const Col1Qty As String = "Qty"
    Protected Const Col1Unit As String = "Unit"
    Protected Const Col1MeasureDecimalPlaces As String = "Measure Decimal Places"
    Protected Const Col1MeasurePerPcs As String = "Measure Per Pcs"
    Protected Const Col1MeasureUnit As String = "Measure Unit"
    Protected Const Col1TotalMeasure As String = "Total Measure"
    Protected Const Col1RequireDate As String = "Require Date"
    Protected Const Col1Remark As String = "Remark"
    Protected Const Col1ApprovedQty As String = "Approved Qty"
    Protected Const Col1ApprovedBy As String = "Approved By"
    Protected Const Col1Specification As String = "Specification"
    Protected WithEvents Label1 As System.Windows.Forms.Label
    Protected WithEvents LblRequisitionNo As System.Windows.Forms.Label
    Protected WithEvents TxtManualRefNo As AgControls.AgTextBox

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
        Me.TxtRequisitionBy = New AgControls.AgTextBox
        Me.LblIndentor = New System.Windows.Forms.Label
        Me.LblDepartmentReq = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.LblRequisitionNo = New System.Windows.Forms.Label
        Me.TxtManualRefNo = New AgControls.AgTextBox
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
        Me.LblV_No.Location = New System.Drawing.Point(871, 40)
        Me.LblV_No.Size = New System.Drawing.Size(96, 16)
        Me.LblV_No.Tag = ""
        Me.LblV_No.Text = "Requisition No."
        Me.LblV_No.Visible = False
        '
        'TxtV_No
        '
        Me.TxtV_No.AgSelectedValue = ""
        Me.TxtV_No.BackColor = System.Drawing.Color.White
        Me.TxtV_No.Location = New System.Drawing.Point(874, 59)
        Me.TxtV_No.Size = New System.Drawing.Size(122, 18)
        Me.TxtV_No.TabIndex = 3
        Me.TxtV_No.Tag = ""
        Me.TxtV_No.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.TxtV_No.Visible = False
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
        Me.LblV_Date.Size = New System.Drawing.Size(103, 16)
        Me.LblV_Date.Tag = ""
        Me.LblV_Date.Text = "Requisition Date"
        '
        'LblV_TypeReq
        '
        Me.LblV_TypeReq.Location = New System.Drawing.Point(610, 19)
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
        Me.LblV_Type.Location = New System.Drawing.Point(496, 15)
        Me.LblV_Type.Size = New System.Drawing.Size(103, 16)
        Me.LblV_Type.Tag = ""
        Me.LblV_Type.Text = "Requisition Type"
        '
        'TxtV_Type
        '
        Me.TxtV_Type.AgSelectedValue = ""
        Me.TxtV_Type.BackColor = System.Drawing.Color.White
        Me.TxtV_Type.Location = New System.Drawing.Point(633, 13)
        Me.TxtV_Type.Size = New System.Drawing.Size(122, 18)
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
        Me.TP1.Controls.Add(Me.TxtManualRefNo)
        Me.TP1.Controls.Add(Me.Label1)
        Me.TP1.Controls.Add(Me.LblRequisitionNo)
        Me.TP1.Controls.Add(Me.LblDepartmentReq)
        Me.TP1.Controls.Add(Me.LblIndentorReq)
        Me.TP1.Controls.Add(Me.TxtRequisitionBy)
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
        Me.TP1.Controls.SetChildIndex(Me.TxtRequisitionBy, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblIndentorReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblDepartmentReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblRequisitionNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label1, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtManualRefNo, 0)
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
        Me.TxtDepartment.Size = New System.Drawing.Size(390, 18)
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
        Me.TxtRemarks.Size = New System.Drawing.Size(390, 18)
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
        Me.LinkLabel1.Size = New System.Drawing.Size(191, 20)
        Me.LinkLabel1.TabIndex = 731
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "Requisition For Following Items"
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
        'TxtRequisitionBy
        '
        Me.TxtRequisitionBy.AgAllowUserToEnableMasterHelp = False
        Me.TxtRequisitionBy.AgLastValueTag = Nothing
        Me.TxtRequisitionBy.AgLastValueText = Nothing
        Me.TxtRequisitionBy.AgMandatory = True
        Me.TxtRequisitionBy.AgMasterHelp = False
        Me.TxtRequisitionBy.AgNumberLeftPlaces = 8
        Me.TxtRequisitionBy.AgNumberNegetiveAllow = False
        Me.TxtRequisitionBy.AgNumberRightPlaces = 2
        Me.TxtRequisitionBy.AgPickFromLastValue = False
        Me.TxtRequisitionBy.AgRowFilter = ""
        Me.TxtRequisitionBy.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtRequisitionBy.AgSelectedValue = Nothing
        Me.TxtRequisitionBy.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtRequisitionBy.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtRequisitionBy.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtRequisitionBy.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtRequisitionBy.Location = New System.Drawing.Point(365, 73)
        Me.TxtRequisitionBy.MaxLength = 20
        Me.TxtRequisitionBy.Name = "TxtRequisitionBy"
        Me.TxtRequisitionBy.Size = New System.Drawing.Size(390, 18)
        Me.TxtRequisitionBy.TabIndex = 6
        '
        'LblIndentor
        '
        Me.LblIndentor.AutoSize = True
        Me.LblIndentor.BackColor = System.Drawing.Color.Transparent
        Me.LblIndentor.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblIndentor.Location = New System.Drawing.Point(241, 73)
        Me.LblIndentor.Name = "LblIndentor"
        Me.LblIndentor.Size = New System.Drawing.Size(92, 16)
        Me.LblIndentor.TabIndex = 731
        Me.LblIndentor.Text = "Requisition By"
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
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(610, 39)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(10, 7)
        Me.Label1.TabIndex = 736
        Me.Label1.Text = "Ä"
        '
        'LblRequisitionNo
        '
        Me.LblRequisitionNo.AutoSize = True
        Me.LblRequisitionNo.BackColor = System.Drawing.Color.Transparent
        Me.LblRequisitionNo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblRequisitionNo.Location = New System.Drawing.Point(496, 34)
        Me.LblRequisitionNo.Name = "LblRequisitionNo"
        Me.LblRequisitionNo.Size = New System.Drawing.Size(92, 16)
        Me.LblRequisitionNo.TabIndex = 735
        Me.LblRequisitionNo.Text = "Requisition No"
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
        Me.TxtManualRefNo.Location = New System.Drawing.Point(633, 33)
        Me.TxtManualRefNo.MaxLength = 50
        Me.TxtManualRefNo.Name = "TxtManualRefNo"
        Me.TxtManualRefNo.Size = New System.Drawing.Size(122, 18)
        Me.TxtManualRefNo.TabIndex = 3
        '
        'FrmRequisition
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.BackColor = System.Drawing.SystemColors.ButtonShadow
        Me.ClientSize = New System.Drawing.Size(994, 572)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Pnl1)
        Me.Name = "FrmRequisition"
        Me.Text = "Template Purchase Indent"
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
    Protected WithEvents TxtRequisitionBy As AgControls.AgTextBox
    Protected WithEvents LblIndentor As System.Windows.Forms.Label
    Protected WithEvents LblDepartmentReq As System.Windows.Forms.Label
#End Region

    Private Sub FrmPurchRequisition_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "Requisition"
        LogTableName = "Requisition_Log"
        MainLineTableCsv = "RequisitionDetail"
        LogLineTableCsv = "RequisitionDetail_Log"
        AgL.GridDesign(Dgl1)
    End Sub

    Private Sub FrmPurchRequisition_BaseFunction_FIniMast(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMast
        Dim mCondStr$
        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) &
                       " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "H.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"


        If IsApplyVTypePermission Then
            mCondStr = mCondStr & " And H.V_Type In (Select V_Type From User_VType_Permission VP Where VP.UserName = '" & AgL.PubUserName & "' And VP.Div_Code = '" & AgL.PubDivCode & "' And VP.Site_Code = '" & AgL.PubSiteCode & "') "
        End If


        mQry = " Select H.DocID As SearchCode " &
            " From Requisition H " &
            " Left Join Voucher_Type Vt On H.V_Type = Vt.V_Type  " &
            " Where IfNull(IsDeleted,0) = 0  " & mCondStr & "  Order By H.V_Date, H.V_No  "

        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub FrmPurchRequisition_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mCondStr$

        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) &
                        " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "H.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"


        If IsApplyVTypePermission Then
            mCondStr = mCondStr & " And H.V_Type In (Select V_Type From User_VType_Permission VP Where VP.UserName = '" & AgL.PubUserName & "' And VP.Div_Code = '" & AgL.PubDivCode & "' And VP.Site_Code = '" & AgL.PubSiteCode & "') "
        End If


        AgL.PubFindQry = " SELECT H.DocID, H.V_Date AS [Requisition Date], H.ReferenceNo AS [Requisition No.] , SG.DispName AS [Requisition By], D.Description AS Department, H.Remarks, " &
                            " SM.Name AS Site_Name, DIV.Div_Name  " &
                            " FROM Requisition H " &
                            " LEFT JOIN SubGroup SG ON SG.SubCode = H.RequisitionBy  " &
                            " LEFT JOIN Department D ON D.Code = H.Department  " &
                            " LEFT JOIN SiteMast SM ON SM.Code = H.Site_Code " &
                            " LEFT JOIN Division DIV ON DIV.Div_Code = H.Div_Code  " &
                            " Left Join Voucher_Type Vt On H.V_Type = Vt.V_Type  " &
                            " Where IfNull(H.IsDeleted,0) = 0   " & mCondStr

        AgL.PubFindQryOrdBy = "[Entry Date]"
    End Sub

    Private Sub FrmPurchRequisition_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl1, Col1ItemCode, 100, 0, Col1ItemCode, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_ItemCode")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_ItemCode")), Boolean))
            .AddAgTextColumn(Dgl1, Col1Item, 200, 0, Col1Item, True, Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_ItemName")), Boolean))
            .AddAgTextColumn(Dgl1, Col1QtyDecimalPlaces, 50, 0, Col1QtyDecimalPlaces, False, True, False)
            .AddAgNumberColumn(Dgl1, Col1Qty, 60, 8, 4, False, Col1Qty, True, False, True)
            .AddAgTextColumn(Dgl1, Col1Unit, 50, 0, Col1Unit, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Unit")), Boolean), True)
            .AddAgTextColumn(Dgl1, Col1MeasureDecimalPlaces, 50, 0, Col1MeasureDecimalPlaces, False, True, False)
            .AddAgNumberColumn(Dgl1, Col1MeasurePerPcs, 80, 8, 4, False, Col1MeasurePerPcs, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_MeasurePerPcs")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_MeasurePerPcs")), Boolean), True)
            .AddAgTextColumn(Dgl1, Col1MeasureUnit, 55, 0, Col1MeasureUnit, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_MeasureUnit")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_MeasureUnit")), Boolean))
            .AddAgNumberColumn(Dgl1, Col1TotalMeasure, 80, 8, 4, False, Col1TotalMeasure, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Measure")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_Measure")), Boolean), True)
            .AddAgDateColumn(Dgl1, Col1RequireDate, 80, Col1RequireDate, True, False)
            .AddAgTextColumn(Dgl1, Col1Remark, 90, 255, Col1Remark, True, False)
            .AddAgNumberColumn(Dgl1, Col1ApprovedQty, 60, 8, 4, False, Col1ApprovedQty, True, True, True)
            .AddAgTextColumn(Dgl1, Col1ApprovedBy, 60, 0, Col1ApprovedBy, True, True)
            .AddAgTextColumn(Dgl1, Col1Specification, 90, 100, Col1Specification, True, False)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 35
        Dgl1.AgSkipReadOnlyColumns = True

        LblTotalMeasure.Visible = CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Measure")), Boolean)
        LblTotalMeasureText.Visible = CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Measure")), Boolean)
    End Sub

    Private Sub FrmPurchRequisition_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As SQLiteConnection, ByVal Cmd As SQLiteCommand) Handles Me.BaseEvent_Save_InTrans
        Dim I As Integer, mSr As Integer
        Dim bSelectionQry As String = ""
        mQry = " UPDATE Requisition " &
                " SET " &
                " ReferenceNo = " & AgL.Chk_Text(TxtManualRefNo.Text) & ", " &
                " Department = " & AgL.Chk_Text(TxtDepartment.Tag) & ", " &
                " RequisitionBy = " & AgL.Chk_Text(TxtRequisitionBy.Tag) & ", " &
                " Remarks = " & AgL.Chk_Text(TxtRemarks.Text) & " " &
                " Where DocID = '" & SearchCode & "'"

        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        'Never Try to Serialise Sr in Line Items 
        'As Some other Entry points may updating values to this Search code and Sr

        mSr = AgL.VNull(AgL.Dman_Execute("Select Max(Sr) From RequisitionDetail  Where DocID = '" & mSearchCode & "'", AgL.GcnRead).ExecuteScalar)
        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1Item, I).Value <> "" Then
                If Dgl1.Item(ColSNo, I).Tag Is Nothing And Dgl1.Rows(I).Visible = True Then
                    mSr += 1
                    If bSelectionQry <> "" Then bSelectionQry += " UNION ALL "
                    bSelectionQry += " Select " & AgL.Chk_Text(SearchCode) & ", " & mSr & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1Item, I).Tag) & ", " &
                            " " & Val(Dgl1.Item(Col1Qty, I).Value) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1Unit, I).Value) & ", " &
                            " " & Val(Dgl1.Item(Col1MeasurePerPcs, I).Value) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1MeasureUnit, I).Value) & ", " &
                            " " & Val(Dgl1.Item(Col1TotalMeasure, I).Value) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1RequireDate, I).Value) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1Specification, I).Value) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1Remark, I).Value) & "  "
                Else
                    If Dgl1.Rows(I).Visible = True Then
                        If Dgl1.Rows(I).DefaultCellStyle.BackColor <> RowLockedColour Then
                            mQry = " UPDATE RequisitionDetail " &
                                    " SET Item = " & AgL.Chk_Text(Dgl1.Item(Col1Item, I).Tag) & " , " &
                                    " Qty = " & Val(Dgl1.Item(Col1Qty, I).Value) & ", " &
                                    " Unit = " & AgL.Chk_Text(Dgl1.Item(Col1Unit, I).Value) & ", " &
                                    " MeasurePerPcs = " & Val(Dgl1.Item(Col1MeasurePerPcs, I).Value) & ", " &
                                    " MeasureUnit = " & AgL.Chk_Text(Dgl1.Item(Col1MeasureUnit, I).Value) & ", " &
                                    " TotalMeasure = " & Val(Dgl1.Item(Col1TotalMeasure, I).Value) & ", " &
                                    " RequireDate = " & AgL.Chk_Text(Dgl1.Item(Col1RequireDate, I).Value) & ", " &
                                    " Specification = " & AgL.Chk_Text(Dgl1.Item(Col1Specification, I).Value) & ", " &
                                    " Remark = " & AgL.Chk_Text(Dgl1.Item(Col1Remark, I).Value) & " " &
                                    " Where DocId = '" & mSearchCode & "' " &
                                    " And Sr = " & Dgl1.Item(ColSNo, I).Tag & " "
                            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                        End If
                    Else
                        mQry = " Delete From RequisitionDetail Where DocId = '" & mSearchCode & "' And Sr = " & Val(Dgl1.Item(ColSNo, I).Tag) & "  "
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                    End If
                End If
            End If
        Next

        mQry = " INSERT INTO RequisitionDetail (DocId, Sr,	Item, Qty, " &
                " Unit,	MeasurePerPcs,	MeasureUnit,	TotalMeasure, " &
                " RequireDate, Specification, Remark ) "
        mQry = mQry + bSelectionQry
        If bSelectionQry <> "" Then
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If

    End Sub

    Private Sub FrmPurchRequisition_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim I As Integer
        Dim DrTemp As DataRow() = Nothing
        Dim DsTemp As DataSet

        Dim IsSameUnit As Boolean = True
        Dim IsSameMeasureUnit As Boolean = True

        Dim intQtyDecimalPlaces As Integer = 0
        Dim intMeasureDecimalPlaces As Integer = 0

        Dim strQryPurchIndent As String = " SELECT L.Requisition, L.RequisitionSr,  sum(L.Qty) AS IndQty " &
                    " FROM PurchIndentReq L  " &
                    " WHERE IfNull(L.Requisition,'') <> ''  " &
                    " GROUP BY L.Requisition, L.RequisitionSr "

        mQry = " SELECT H.DocID, H.V_Type, H.ReferenceNo, H.V_Prefix, H.V_Date, H.V_No, H.RequisitionBy, H.Department, H.Remarks, " &
                " SG.DispName AS ReqByName, D.Description AS DepartmentName, L.Sr, L.Item, L.Qty, L.Unit, L.MeasurePerPcs, L.ApproveBy, L.ApproveQty , L.Specification, " &
                " L.MeasureUnit, L.TotalMeasure, L.RequireDate, L.Remark AS LineRemark, I.Description AS ItemName, I.ManualCode AS ItemCode,   " &
                " U.DecimalPlaces as QtyDecimalPlaces, MU.DecimalPlaces as MeasureDecimalPlaces, " &
                " ( Case When IfNull(PI.IndQty,0) > 0 Then 1 ELSE CASE WHEN IfNull(PI.IndQty,0) > 0 THEN 1 ELSE 0 END END ) as RowLocked " &
                " FROM " &
                " ( " &
                "  SELECT * FROM Requisition WHERE DocID = '" & SearchCode & "' " &
                " ) H  " &
                " LEFT JOIN SubGroup SG ON SG.SubCode = H.RequisitionBy  " &
                " LEFT JOIN Department D ON D.Code = H.Department  " &
                " LEFT JOIN RequisitionDetail L ON L.DocId = H.DocID  " &
                " LEFT JOIN Item I ON I.Code = L.Item " &
                " LEFT JOIN Unit U On L.Unit = U.Code " &
                " LEFT JOIN Unit MU ON L.MeasureUnit = MU.Code " &
                " Left Join ( " & strQryPurchIndent & " ) As PI On L.DocID = PI.Requisition and L.Sr = PI.Requisitionsr " &
                " Order By Sr "
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                TxtManualRefNo.Text = AgL.XNull(.Rows(0)("ReferenceNo"))
                TxtDepartment.Tag = AgL.XNull(.Rows(0)("Department"))
                TxtDepartment.Text = AgL.XNull(.Rows(0)("DepartmentName"))
                TxtRequisitionBy.Tag = AgL.XNull(.Rows(0)("RequisitionBy"))
                TxtRequisitionBy.Text = AgL.XNull(.Rows(0)("ReqByName"))
                TxtRemarks.Text = AgL.XNull(.Rows(0)("Remarks"))

                IniGrid()

                '-------------------------------------------------------------
                'Line Records are showing in First Grid
                '-------------------------------------------------------------

                For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                    Dgl1.Rows.Add()
                    Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count - 1
                    Dgl1.Item(ColSNo, I).Tag = AgL.XNull(.Rows(I)("Sr"))
                    Dgl1.Item(Col1Item, I).Value = AgL.XNull(.Rows(I)("ItemName"))
                    Dgl1.Item(Col1Item, I).Tag = AgL.XNull(.Rows(I)("Item"))
                    Dgl1.Item(Col1ItemCode, I).Value = AgL.XNull(.Rows(I)("ItemCode"))
                    Dgl1.Item(Col1ItemCode, I).Tag = AgL.XNull(.Rows(I)("Item"))
                    Dgl1.Item(Col1QtyDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("QtyDecimalPlaces"))
                    Dgl1.Item(Col1Qty, I).Value = Format(AgL.VNull(.Rows(I)("Qty")), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
                    Dgl1.Item(Col1Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                    Dgl1.Item(Col1MeasureDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("MeasureDecimalPlaces"))
                    Dgl1.Item(Col1MeasurePerPcs, I).Value = Format(AgL.VNull(.Rows(I)("MeasurePerPcs")), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                    Dgl1.Item(Col1TotalMeasure, I).Value = Format(AgL.VNull(.Rows(I)("TotalMeasure")), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                    Dgl1.Item(Col1MeasureUnit, I).Value = AgL.XNull(.Rows(I)("MeasureUnit"))
                    Dgl1.Item(Col1RequireDate, I).Value = AgL.XNull(.Rows(I)("RequireDate"))
                    Dgl1.Item(Col1Remark, I).Value = AgL.XNull(.Rows(I)("LineRemark"))
                    Dgl1.Item(Col1Specification, I).Value = AgL.XNull(.Rows(I)("Specification"))
                    Dgl1.Item(Col1ApprovedQty, I).Value = Format(AgL.VNull(.Rows(I)("ApproveQty")), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
                    Dgl1.Item(Col1ApprovedBy, I).Value = AgL.XNull(.Rows(I)("ApproveBy"))

                    If Dgl1.Item(Col1ApprovedQty, I).Value > 0 Then
                        Dgl1.Rows(I).DefaultCellStyle.BackColor = RowLockedColour
                        Dgl1.Rows(I).ReadOnly = True
                    End If


                    If .Rows(I)("RowLocked") > 0 Then Dgl1.Rows(I).DefaultCellStyle.BackColor = RowLockedColour

                    If Not AgL.StrCmp(Dgl1.Item(Col1Unit, I).Value, Dgl1.Item(Col1Unit, 0).Value) Then IsSameUnit = False
                    If Not AgL.StrCmp(Dgl1.Item(Col1MeasureUnit, I).Value, Dgl1.Item(Col1MeasureUnit, 0).Value) Then IsSameMeasureUnit = False

                    If intQtyDecimalPlaces < Val(Dgl1.Item(Col1QtyDecimalPlaces, I).Value) Then intQtyDecimalPlaces = Val(Dgl1.Item(Col1QtyDecimalPlaces, I).Value)
                    If intMeasureDecimalPlaces < Val(Dgl1.Item(Col1MeasureDecimalPlaces, I).Value) Then intMeasureDecimalPlaces = Val(Dgl1.Item(Col1MeasureDecimalPlaces, I).Value)
                    LblTotalQty.Text = Val(LblTotalQty.Text) + Val(Dgl1.Item(Col1Qty, I).Value)
                    LblTotalMeasure.Text = Val(LblTotalMeasure.Text) + Val(Dgl1.Item(Col1TotalMeasure, I).Value)

                    RaiseEvent BaseFunction_MoveRecLine(SearchCode, AgL.VNull(.Rows(I)("Sr")), I)
                Next I
            End If
            If Dgl1.Item(Col1Unit, 0).Value <> "" And IsSameUnit Then LblTotalQtyText.Text = "Total Qty (" & Dgl1.Item(Col1Unit, 0).Value & ") :" Else LblTotalQtyText.Text = "Total Qty :"
            If Dgl1.Item(Col1MeasureUnit, 0).Value <> "" And IsSameMeasureUnit Then LblTotalMeasureText.Text = "Total Measure (" & Dgl1.Item(Col1MeasureUnit, 0).Value & ") :" Else LblTotalMeasureText.Text = "Total Measure :"
        End With
        LblTotalQty.Text = Format(Val(LblTotalQty.Text), "0.".PadRight(intQtyDecimalPlaces + 2, "0"))
        LblTotalMeasure.Text = Format(Val(LblTotalMeasure.Text), "0.".PadRight(intMeasureDecimalPlaces + 2, "0"))
    End Sub

    Private Sub FrmPurchRequisition_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Topctrl1.ChangeAgGridState(Dgl1, False)
        AgL.WinSetting(Me, 600, 1000, 0, 0)
    End Sub

    Private Sub Dgl1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellEnter
        If Dgl1.CurrentCell Is Nothing Then Exit Sub
        Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
            Case Col1Qty
                CType(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex), AgControls.AgTextColumn).AgNumberRightPlaces = Val(Dgl1.Item(Col1QtyDecimalPlaces, Dgl1.CurrentCell.RowIndex).Value)

            Case Col1MeasurePerPcs, Col1TotalMeasure
                CType(Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex), AgControls.AgTextColumn).AgNumberRightPlaces = Val(Dgl1.Item(Col1MeasureDecimalPlaces, Dgl1.CurrentCell.RowIndex).Value)
        End Select
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
                mQry = " SELECT I.Code, I.Description AS ItemDesc, I.ManualCode AS ItemCode, I.Unit, " &
                        " I.Measure As MeasurePerPcs, I.MeasureUnit, U.DecimalPlaces As QtyDecimalPlaces, U1.DecimalPlaces As MeasureDecimalPlaces " &
                        " FROM Item I " &
                        " LEFT JOIN Unit U On I.Unit = U.Code " &
                        " LEFT JOIN Unit U1 On I.MeasureUnit = U1.Code " &
                        " Where IfNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
                Dgl1.AgHelpDataSet(Col1Item, 4) = AgL.FillData(mQry, AgL.GCn)


            Case Col1ItemCode
                mQry = " SELECT I.Code, I.ManualCode AS ItemCode, I.Description AS ItemDesc, I.Unit, " &
                        " I.Measure As MeasurePerPcs, I.MeasureUnit, U.DecimalPlaces As QtyDecimalPlaces, U1.DecimalPlaces As MeasureDecimalPlaces " &
                        " FROM Item I " &
                        " LEFT JOIN Unit U On I.Unit = U.Code " &
                        " LEFT JOIN Unit U1 On I.MeasureUnit = U1.Code " &
                        " Where IfNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " & strCond
                Dgl1.AgHelpDataSet(Col1ItemCode, 4) = AgL.FillData(mQry, AgL.GCn)
        End Select
    End Sub

    Private Sub DGL1_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles Dgl1.RowsAdded
        sender(ColSNo, e.RowIndex).Value = e.RowIndex + 1
    End Sub

    Private Sub FrmPurchRequisition_BaseFunction_Calculation() Handles Me.BaseFunction_Calculation
        Dim I As Integer
        Dim IsSameUnit As Boolean = True
        Dim IsSameMeasureUnit As Boolean = True

        Dim intQtyDecimalPlaces As Integer = 0
        Dim intMeasureDecimalPlaces As Integer = 0

        LblTotalQty.Text = 0
        LblTotalMeasure.Text = 0

        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1Item, I).Value <> "" And Dgl1.Rows(I).Visible = True Then

                'If In Item Master Measure Per Pcs Is Defined then this calculation will be executed.
                'For Example In Carpet Area Per Pcs Is Defined in Item Master and Total Area will be calculated
                'with that Area per pcs. 
                If Val(Dgl1.Item(Col1MeasurePerPcs, I).Value) <> 0 Then
                    Dgl1.Item(Col1TotalMeasure, I).Value = Format(Val(Dgl1.Item(Col1Qty, I).Value) * Val(Dgl1.Item(Col1MeasurePerPcs, I).Value), "0.".PadRight(CType(Dgl1.Columns(Col1TotalMeasure), AgControls.AgTextColumn).AgNumberRightPlaces + 2, "0"))
                End If

                If Not AgL.StrCmp(Dgl1.Item(Col1Unit, I).Value, Dgl1.Item(Col1Unit, 0).Value) Then IsSameUnit = False
                If Not AgL.StrCmp(Dgl1.Item(Col1MeasureUnit, I).Value, Dgl1.Item(Col1MeasureUnit, 0).Value) Then IsSameMeasureUnit = False

                If intQtyDecimalPlaces < Val(Dgl1.Item(Col1QtyDecimalPlaces, I).Value) Then intQtyDecimalPlaces = Val(Dgl1.Item(Col1QtyDecimalPlaces, I).Value)
                If intMeasureDecimalPlaces < Val(Dgl1.Item(Col1MeasureDecimalPlaces, I).Value) Then intMeasureDecimalPlaces = Val(Dgl1.Item(Col1MeasureDecimalPlaces, I).Value)

                'Footer Calculation
                LblTotalQty.Text = Val(LblTotalQty.Text) + Val(Dgl1.Item(Col1Qty, I).Value)
                LblTotalMeasure.Text = Val(LblTotalMeasure.Text) + Val(Dgl1.Item(Col1TotalMeasure, I).Value)
            End If
        Next

        LblTotalQty.Text = Format(Val(LblTotalQty.Text), "0.".PadRight(intQtyDecimalPlaces + 2, "0"))
        LblTotalMeasure.Text = Format(Val(LblTotalMeasure.Text), "0.".PadRight(intMeasureDecimalPlaces + 2, "0"))

        If Dgl1.Item(Col1Unit, 0).Value <> "" And IsSameUnit Then LblTotalQtyText.Text = "Qty (" & Dgl1.Item(Col1Unit, 0).Value & ") :" Else LblTotalQtyText.Text = "Qty :"
        If Dgl1.Item(Col1MeasureUnit, 0).Value <> "" And IsSameMeasureUnit Then LblTotalMeasureText.Text = "Measure (" & Dgl1.Item(Col1MeasureUnit, 0).Value & ") :" Else LblTotalMeasureText.Text = "Measure :"
    End Sub

    Private Sub FrmPurchRequisition_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        Dim I As Integer = 0
        If AgL.RequiredField(TxtManualRefNo, LblRequisitionNo.Text) Then passed = False : Exit Sub
        If AgL.RequiredField(TxtDepartment, LblDepartment.Text) Then passed = False : Exit Sub
        If AgL.RequiredField(TxtRequisitionBy, LblIndentor.Text) Then passed = False : Exit Sub

        If AgCL.AgIsBlankGrid(Dgl1, Dgl1.Columns(Col1Item).Index) Then passed = False : Exit Sub
        If AgCL.AgIsDuplicate(Dgl1, Dgl1.Columns(Col1Item).Index) Then passed = False : Exit Sub

        With Dgl1
            For I = 0 To .Rows.Count - 1
                If .Item(Col1Item, I).Value <> "" Then
                    If Val(.Item(Col1Qty, I).Value) = 0 Then
                        MsgBox("Qty Is 0 At Row No " & Dgl1.Item(ColSNo, I).Value & "")
                        .CurrentCell = .Item(Col1Qty, I) : Dgl1.Focus()
                        passed = False : Exit Sub
                    End If
                End If
            Next
        End With
    End Sub

    Private Sub FrmPurchRequisition_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
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
                Dgl1.Item(Col1QtyDecimalPlaces, mRow).Value = 0
                Dgl1.Item(Col1Qty, mRow).Value = 0
                Dgl1.Item(Col1MeasureDecimalPlaces, mRow).Value = 0
                Dgl1.Item(Col1TotalMeasure, mRow).Value = 0
                Dgl1.Item(Col1RequireDate, mRow).Value = ""
                Dgl1.Item(Col1Item, mRow).Value = ""
                Dgl1.Item(Col1Item, mRow).Tag = ""
                Dgl1.Item(Col1ItemCode, mRow).Value = ""
                Dgl1.Item(Col1ItemCode, mRow).Tag = ""
            Else
                If Dgl1.AgHelpDataSet(ColoumnName) IsNot Nothing Then
                    DrTemp = Dgl1.AgHelpDataSet(ColoumnName).Tables(0).Select("Code = '" & Code & "'")
                    Dgl1.Item(Col1QtyDecimalPlaces, mRow).Value = AgL.VNull(DrTemp(0)("QtyDecimalPlaces"))
                    Dgl1.Item(Col1Unit, mRow).Value = AgL.XNull(DrTemp(0)("Unit"))
                    Dgl1.Item(Col1MeasurePerPcs, mRow).Value = AgL.VNull(DrTemp(0)("MeasurePerPcs"))
                    Dgl1.Item(Col1MeasureDecimalPlaces, mRow).Value = AgL.VNull(DrTemp(0)("MeasureDecimalPlaces"))
                    Dgl1.Item(Col1MeasureUnit, mRow).Value = AgL.XNull(DrTemp(0)("MeasureUnit"))
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
                If sender.Rows(sender.currentcell.rowindex).DefaultCellStyle.BackColor = RowLockedColour Then
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
            '// Check for relational data in Purchase Indent
            mQry = " DECLARE @Temp NVARCHAR(Max); "
            mQry += " SET @Temp=''; "
            mQry += " SELECT  @Temp=@Temp +  X.VNo + ', ' FROM (SELECT DISTINCT H.V_Type + '-' + Convert(VARCHAR,H.V_No) AS VNo From PurchIndentReq  L LEFT JOIN PurchIndent H ON L.DocId = H.DocID WHERE L.Requisition  = '" & TxtDocId.Text & "') AS X  "
            mQry += " SELECT @Temp as RelationalData "
            bRData = AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar
            If bRData.Trim <> "" Then
                MsgBox("Requisition " & bRData & " created against Indent No. " & TxtV_Type.Tag & "-" & TxtV_No.Text & ". Can't Modify Entry")
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

        Dim bAppQty As String
        mQry = " SELECT IfNull(sum(L.ApproveQty),0) AS ApproveQty " &
                " FROM RequisitionDetail L " &
                " WHERE L.DocId = '" & TxtDocId.Text & "'  "
        bAppQty = AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar
        If Val(bAppQty) > 0 Then
            MsgBox("Requisition " & TxtV_Type.Tag & "-" & TxtV_No.Text & " is Approved . Can't Delete Entry")
            Passed = False
            Exit Sub
        End If

    End Sub

    Private Sub Txt_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtDepartment.KeyDown, TxtRequisitionBy.KeyDown
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

                Case TxtRequisitionBy.Name
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

        strCond += " And H.Department = '" & TxtDepartment.Tag & "' "

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

        mQry = " SELECT H.SubCode AS Code, H.DispName AS [Employee Name], H.ManualCode AS [Employee Code], " &
            " H.Currency, C1.Description As CurrencyDesc, H.Nature, H.SalesTaxPostingGroup " &
           " FROM Subgroup H " &
           " LEFT JOIN City C ON H.CityCode = C.CityCode  " &
           " LEFT JOIN Currency C1 On H.Currency = C1.Code " &
           " Where IfNull(H.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') =  '" & AgTemplate.ClsMain.EntryStatus.Active & "' AND H.MasterType = '" & ClsMain.MasterType.Employee & "'  " & strCond
        sender.AgHelpDataSet(4, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
    End Sub

    Private Sub FrmPurchIndent_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub

    Private Sub FrmPurchIndent_BaseEvent_Topctrl_tbPrn(ByVal SearchCode As String) Handles Me.BaseEvent_Topctrl_tbPrn
        mQry = " SELECT H.DocID, H.V_Date, H.ReferenceNo, H.Remarks, D.Description AS DepartmentName, " &
                " SG.DispName AS ReqByName, L.Sr, L.Qty, L.Unit, L.RequireDate, L.Remark AS LineRemark , I.Description AS  ItemDesc  " &
                " FROM Requisition H " &
                " LEFT JOIN RequisitionDetail L ON L.DocId = H.DocID  " &
                " LEFT JOIN Department D ON D.Code = H.Department  " &
                " LEFT JOIN SubGroup SG ON SG.SubCode = H.RequisitionBy  " &
                " LEFT JOIN Item I ON I.Code = L.Item " &
                " WHERE H.DocID =  '" & mSearchCode & "'  Order By L.Sr "
        ClsMain.FPrintThisDocument(Me, TxtV_Type.Tag, mQry, "Store_Requisition_Print", "Store Requisition")
    End Sub

    Private Sub FrmPurchIndent_BaseEvent_Topctrl_tbRef() Handles Me.BaseEvent_Topctrl_tbRef
        If TxtRequisitionBy.AgHelpDataSet IsNot Nothing Then TxtRequisitionBy.AgHelpDataSet.Dispose() : TxtRequisitionBy.AgHelpDataSet = Nothing
        If TxtDepartment.AgHelpDataSet IsNot Nothing Then TxtDepartment.AgHelpDataSet.Dispose() : TxtDepartment.AgHelpDataSet = Nothing
        If Dgl1.AgHelpDataSet(Col1Item) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1Item).Dispose() : Dgl1.AgHelpDataSet(Col1Item) = Nothing
        If Dgl1.AgHelpDataSet(Col1ItemCode) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1ItemCode).Dispose() : Dgl1.AgHelpDataSet(Col1ItemCode) = Nothing
    End Sub

    Private Sub FrmRequisition_BaseEvent_Topctrl_tbAdd() Handles Me.BaseEvent_Topctrl_tbAdd
        TxtManualRefNo.Text = AgTemplate.ClsMain.FGetManualRefNo("ReferenceNo", "Requisition", TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, AgTemplate.ClsMain.ManualRefType.Max)
    End Sub

    Private Sub TxtDepartment_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtDepartment.Validating
        TxtRequisitionBy.AgHelpDataSet = Nothing
        TxtRequisitionBy.Text = ""
        TxtRequisitionBy.Tag = ""
    End Sub
End Class
