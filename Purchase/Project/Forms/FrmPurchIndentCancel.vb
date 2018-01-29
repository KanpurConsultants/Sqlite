Imports System.Data.SQLite
Public Class FrmPurchIndentCancel
    Inherits AgTemplate.TempTransaction
    Public mQry$

    Public Const ColSNo As String = "S.No."
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Public Const Col1ItemCode As String = "Item Code"
    Public Const Col1Item As String = "Item"
    Public Const Col1PurchIndent As String = "Purch Indent"
    Public Const Col1PurchIndentSr As String = "Purch Indent Sr"
    Public Const Col1MaterialPlan As String = "Planning No"
    Public Const Col1MaterialPlanSr As String = "Planning Sr"
    Public Const Col1QtyDecimalPlaces As String = "Qty Decimal Places"
    Public Const Col1Qty As String = "Qty"
    Public Const Col1Unit As String = "Unit"
    Public Const Col1Rate As String = "Rate"
    Public Const Col1MeasureDecimalPlaces As String = "Measure Decimal Places"
    Public Const Col1MeasurePerPcs As String = "Measure Per Pcs"
    Public Const Col1MeasureUnit As String = "Measure Unit"
    Public Const Col1TotalMeasure As String = "Total Indent Measure"
    Public Const Col1Remark As String = "Remark"

    Public Const mV_Nature = "Indent Cancel"

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
        Me.BtnFillIndentDetail = New System.Windows.Forms.Button
        Me.LblReferenceNoReq = New System.Windows.Forms.Label
        Me.TxtManualRefNo = New AgControls.AgTextBox
        Me.LblReferenceNo = New System.Windows.Forms.Label
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
        Me.LblV_No.Location = New System.Drawing.Point(784, 94)
        Me.LblV_No.Size = New System.Drawing.Size(67, 16)
        Me.LblV_No.Tag = ""
        Me.LblV_No.Text = "Indent No."
        Me.LblV_No.Visible = False
        '
        'TxtV_No
        '
        Me.TxtV_No.AgSelectedValue = ""
        Me.TxtV_No.BackColor = System.Drawing.Color.White
        Me.TxtV_No.Location = New System.Drawing.Point(857, 92)
        Me.TxtV_No.Size = New System.Drawing.Size(106, 18)
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
        Me.LblV_Date.Location = New System.Drawing.Point(241, 35)
        Me.LblV_Date.Size = New System.Drawing.Size(79, 16)
        Me.LblV_Date.Tag = ""
        Me.LblV_Date.Text = "Cancel Date"
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
        Me.LblV_Type.Size = New System.Drawing.Size(79, 16)
        Me.LblV_Type.Tag = ""
        Me.LblV_Type.Text = "Cancel Type"
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
        Me.TabControl1.Size = New System.Drawing.Size(1004, 129)
        Me.TabControl1.TabIndex = 0
        '
        'TP1
        '
        Me.TP1.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.TP1.Controls.Add(Me.LblReferenceNoReq)
        Me.TP1.Controls.Add(Me.TxtManualRefNo)
        Me.TP1.Controls.Add(Me.LblReferenceNo)
        Me.TP1.Controls.Add(Me.LblIndentorReq)
        Me.TP1.Controls.Add(Me.TxtIndentor)
        Me.TP1.Controls.Add(Me.LblIndentor)
        Me.TP1.Controls.Add(Me.TxtRemarks)
        Me.TP1.Controls.Add(Me.Label30)
        Me.TP1.Location = New System.Drawing.Point(4, 22)
        Me.TP1.Size = New System.Drawing.Size(996, 103)
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
        Me.TP1.Controls.SetChildIndex(Me.LblIndentor, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtIndentor, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblIndentorReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblReferenceNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtManualRefNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblReferenceNoReq, 0)
        '
        'Topctrl1
        '
        Me.Topctrl1.Size = New System.Drawing.Size(994, 41)
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
        Me.Pnl1.Location = New System.Drawing.Point(7, 174)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(976, 328)
        Me.Pnl1.TabIndex = 1
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(241, 75)
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
        Me.TxtRemarks.Location = New System.Drawing.Point(365, 73)
        Me.TxtRemarks.MaxLength = 255
        Me.TxtRemarks.Name = "TxtRemarks"
        Me.TxtRemarks.Size = New System.Drawing.Size(391, 18)
        Me.TxtRemarks.TabIndex = 6
        '
        'LinkLabel1
        '
        Me.LinkLabel1.BackColor = System.Drawing.Color.SteelBlue
        Me.LinkLabel1.DisabledLinkColor = System.Drawing.Color.White
        Me.LinkLabel1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LinkLabel1.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LinkLabel1.LinkColor = System.Drawing.Color.White
        Me.LinkLabel1.Location = New System.Drawing.Point(6, 153)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(274, 20)
        Me.LinkLabel1.TabIndex = 731
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "Purchase Indent Cancel For Following Items"
        Me.LinkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'LblIndentorReq
        '
        Me.LblIndentorReq.AutoSize = True
        Me.LblIndentorReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblIndentorReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblIndentorReq.Location = New System.Drawing.Point(349, 60)
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
        Me.TxtIndentor.Location = New System.Drawing.Point(365, 53)
        Me.TxtIndentor.MaxLength = 20
        Me.TxtIndentor.Name = "TxtIndentor"
        Me.TxtIndentor.Size = New System.Drawing.Size(391, 18)
        Me.TxtIndentor.TabIndex = 5
        '
        'LblIndentor
        '
        Me.LblIndentor.AutoSize = True
        Me.LblIndentor.BackColor = System.Drawing.Color.Transparent
        Me.LblIndentor.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblIndentor.Location = New System.Drawing.Point(241, 55)
        Me.LblIndentor.Name = "LblIndentor"
        Me.LblIndentor.Size = New System.Drawing.Size(60, 16)
        Me.LblIndentor.TabIndex = 731
        Me.LblIndentor.Text = "Order By"
        '
        'BtnFillIndentDetail
        '
        Me.BtnFillIndentDetail.BackColor = System.Drawing.Color.Transparent
        Me.BtnFillIndentDetail.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnFillIndentDetail.Font = New System.Drawing.Font("Verdana", 6.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnFillIndentDetail.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnFillIndentDetail.Location = New System.Drawing.Point(290, 153)
        Me.BtnFillIndentDetail.Margin = New System.Windows.Forms.Padding(0)
        Me.BtnFillIndentDetail.Name = "BtnFillIndentDetail"
        Me.BtnFillIndentDetail.Size = New System.Drawing.Size(27, 19)
        Me.BtnFillIndentDetail.TabIndex = 760
        Me.BtnFillIndentDetail.TabStop = False
        Me.BtnFillIndentDetail.Text = "...."
        Me.BtnFillIndentDetail.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnFillIndentDetail.UseVisualStyleBackColor = False
        '
        'LblReferenceNoReq
        '
        Me.LblReferenceNoReq.AutoSize = True
        Me.LblReferenceNoReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblReferenceNoReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblReferenceNoReq.Location = New System.Drawing.Point(581, 40)
        Me.LblReferenceNoReq.Name = "LblReferenceNoReq"
        Me.LblReferenceNoReq.Size = New System.Drawing.Size(10, 7)
        Me.LblReferenceNoReq.TabIndex = 741
        Me.LblReferenceNoReq.Text = "Ä"
        '
        'TxtManualRefNo
        '
        Me.TxtManualRefNo.AgAllowUserToEnableMasterHelp = False
        Me.TxtManualRefNo.AgLastValueTag = Nothing
        Me.TxtManualRefNo.AgLastValueText = Nothing
        Me.TxtManualRefNo.AgMandatory = True
        Me.TxtManualRefNo.AgMasterHelp = True
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
        Me.TxtManualRefNo.Location = New System.Drawing.Point(595, 33)
        Me.TxtManualRefNo.MaxLength = 20
        Me.TxtManualRefNo.Name = "TxtManualRefNo"
        Me.TxtManualRefNo.Size = New System.Drawing.Size(161, 18)
        Me.TxtManualRefNo.TabIndex = 3
        '
        'LblReferenceNo
        '
        Me.LblReferenceNo.AutoSize = True
        Me.LblReferenceNo.BackColor = System.Drawing.Color.Transparent
        Me.LblReferenceNo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblReferenceNo.Location = New System.Drawing.Point(493, 33)
        Me.LblReferenceNo.Name = "LblReferenceNo"
        Me.LblReferenceNo.Size = New System.Drawing.Size(72, 16)
        Me.LblReferenceNo.TabIndex = 740
        Me.LblReferenceNo.Text = "Cancel No."
        '
        'FrmPurchIndentCancel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.BackColor = System.Drawing.SystemColors.ButtonShadow
        Me.ClientSize = New System.Drawing.Size(994, 572)
        Me.Controls.Add(Me.LinkLabel1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Pnl1)
        Me.Controls.Add(Me.BtnFillIndentDetail)
        Me.Name = "FrmPurchIndentCancel"
        Me.Text = "Template Purchase Indent"
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
        Me.ResumeLayout(False)

    End Sub
    Public WithEvents Panel1 As System.Windows.Forms.Panel
    Public WithEvents Pnl1 As System.Windows.Forms.Panel
    Public WithEvents TxtRemarks As AgControls.AgTextBox
    Public WithEvents Label30 As System.Windows.Forms.Label
    Public WithEvents LblTotalMeasure As System.Windows.Forms.Label
    Public WithEvents LblTotalMeasureText As System.Windows.Forms.Label
    Public WithEvents LblTotalQty As System.Windows.Forms.Label
    Public WithEvents LblTotalQtyText As System.Windows.Forms.Label
    Public WithEvents LinkLabel1 As System.Windows.Forms.LinkLabel
    Public WithEvents LblIndentorReq As System.Windows.Forms.Label
    Public WithEvents TxtIndentor As AgControls.AgTextBox
    Public WithEvents LblIndentor As System.Windows.Forms.Label
    Public WithEvents BtnFillIndentDetail As System.Windows.Forms.Button
    Public WithEvents LblReferenceNoReq As System.Windows.Forms.Label
    Public WithEvents TxtManualRefNo As AgControls.AgTextBox
    Public WithEvents LblReferenceNo As System.Windows.Forms.Label
#End Region

    Private Sub FrmQuality1_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "PurchIndent"
        LogTableName = "PurchIndent_Log"
        MainLineTableCsv = "PurchIndentDetail"
        LogLineTableCsv = "PurchIndentDetail_Log"
        AgL.GridDesign(Dgl1)
    End Sub

    Private Sub FrmQuality1_BaseFunction_FIniMast(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMast
        Dim mCondStr$
        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) &
                       " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "H.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        If IsApplyVTypePermission Then
            mCondStr = mCondStr & " And H.V_Type In (Select V_Type From User_VType_Permission VP Where VP.UserName = '" & AgL.PubUserName & "' And VP.Div_Code = '" & AgL.PubDivCode & "' And VP.Site_Code = '" & AgL.PubSiteCode & "') "
        End If

        mQry = " Select H.DocID As SearchCode " &
            " From PurchIndent H " &
            " Left Join Voucher_Type Vt On H.V_Type = Vt.V_Type  " &
            " Where IfNull(IsDeleted,0) = 0  " & mCondStr & "  Order By H.V_Date Desc "

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

        AgL.PubFindQry = " SELECT H.DocID AS SearchCode, H.V_Type AS [Cancel_Type], H.V_Date AS [Cancel_Date], " &
                            " H.ManualRefNo AS [Cancel_No], SGI.DispName AS [Order_By], " &
                            " H.Remarks, H.EntryBy AS [Entry_By], H.EntryDate AS [Entry_Date], H.EntryType AS [Entry_Type],  " &
                            " H.ApproveBy AS [Approve_By], H.ApproveDate AS [Approve_Date] " &
                            " FROM PurchIndent H " &
                            " LEFT JOIN SubGroup  SGI ON SGI.SubCode  = H.Indentor  " &
                            " LEFT JOIN Voucher_Type Vt ON Vt.V_Type = H.V_Type  " &
                            " Where 1 = 1 " & mCondStr
        AgL.PubFindQryOrdBy = "[Entry_Date]"
    End Sub

    Private Sub FrmProductionOrder_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl1, Col1ItemCode, 100, 0, Col1ItemCode, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_ItemCode")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_ItemCode")), Boolean))
            .AddAgTextColumn(Dgl1, Col1Item, 200, 0, Col1Item, True, Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_ItemName")), Boolean))
            .AddAgTextColumn(Dgl1, Col1PurchIndent, 80, 0, Col1PurchIndent, True, True)
            .AddAgTextColumn(Dgl1, Col1PurchIndentSr, 80, 0, Col1PurchIndentSr, False, True)
            .AddAgTextColumn(Dgl1, Col1MaterialPlan, 80, 0, Col1MaterialPlan, True, True)
            .AddAgTextColumn(Dgl1, Col1MaterialPlanSr, 80, 0, Col1MaterialPlanSr, False, True)
            .AddAgTextColumn(Dgl1, Col1QtyDecimalPlaces, 50, 0, Col1QtyDecimalPlaces, False, True, False)
            .AddAgNumberColumn(Dgl1, Col1Qty, 80, 8, 3, False, Col1Qty, True, False, True)
            .AddAgTextColumn(Dgl1, Col1Unit, 50, 0, Col1Unit, True, False, True)
            .AddAgNumberColumn(Dgl1, Col1Rate, 60, 8, 2, False, Col1Rate, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Rate")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_Rate")), Boolean), True)
            .AddAgTextColumn(Dgl1, Col1MeasureDecimalPlaces, 50, 0, Col1MeasureDecimalPlaces, False, True, False)
            .AddAgNumberColumn(Dgl1, Col1MeasurePerPcs, 80, 8, 4, False, Col1MeasurePerPcs, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_MeasurePerPcs")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_MeasurePerPcs")), Boolean), True)
            .AddAgTextColumn(Dgl1, Col1MeasureUnit, 50, 0, Col1MeasureUnit, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_MeasureUnit")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_MeasureUnit")), Boolean))
            .AddAgNumberColumn(Dgl1, Col1TotalMeasure, 80, 8, 4, False, Col1TotalMeasure, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Measure")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_Measure")), Boolean), True)
            .AddAgTextColumn(Dgl1, Col1Remark, 80, 255, Col1Remark, True, False)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 35
        Dgl1.AgSkipReadOnlyColumns = True
    End Sub

    Private Sub FrmPurchIndent_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As SQLiteConnection, ByVal Cmd As SQLiteCommand) Handles Me.BaseEvent_Save_InTrans
        Dim I As Integer, mSr As Integer
        Dim bSelectionQry As String = ""
        mQry = "UPDATE PurchIndent " &
                " SET " &
                " ManualRefNo = " & AgL.Chk_Text(TxtManualRefNo.Text) & ", " &
                " Indentor = " & AgL.Chk_Text(TxtIndentor.Tag) & ", " &
                " Remarks = " & AgL.Chk_Text(TxtRemarks.Text) & " " &
                " Where DocID = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        'Never Try to Serialise Sr in Line Items 
        'As Some other Entry points may updating values to this Search code and Sr

        mSr = AgL.VNull(AgL.Dman_Execute("Select Max(Sr) From PurchIndentDetail  Where DocID = '" & mSearchCode & "'", AgL.GcnRead).ExecuteScalar)
        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1Item, I).Value <> "" Then
                If Dgl1.Item(ColSNo, I).Tag Is Nothing And Dgl1.Rows(I).Visible = True Then
                    mSr += 1
                    If bSelectionQry <> "" Then bSelectionQry += " UNION ALL "
                    bSelectionQry += " Select " & AgL.Chk_Text(SearchCode) & ", " & mSr & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1Item, I).Tag) & ", " &
                            " " & -Val(Dgl1.Item(Col1Qty, I).Value) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1Unit, I).Value) & ", " &
                            " " & Val(Dgl1.Item(Col1Rate, I).Value) & ", " &
                            " " & Val(Dgl1.Item(Col1MeasurePerPcs, I).Value) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1MeasureUnit, I).Value) & ", " &
                            " " & -Val(Dgl1.Item(Col1TotalMeasure, I).Value) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1MaterialPlan, I).Tag) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1MaterialPlanSr, I).Value) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1PurchIndent, I).Tag) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1PurchIndentSr, I).Value) & ", " &
                            " " & AgTemplate.ClsMain.T_Nature.Cancellation & ", " &
                            " " & AgL.Chk_Text(mV_Nature) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1Remark, I).Value) & "  "
                Else
                    If Dgl1.Rows(I).Visible = True Then
                        If Dgl1.Rows(I).DefaultCellStyle.BackColor <> AgTemplate.ClsMain.Colours.GridRow_Locked Then
                            mQry = " UPDATE PurchIndentDetail " &
                                    " SET Item = " & AgL.Chk_Text(Dgl1.Item(Col1Item, I).Tag) & " , " &
                                    " IndentQty = " & -Val(Dgl1.Item(Col1Qty, I).Value) & ", " &
                                    " Unit = " & AgL.Chk_Text(Dgl1.Item(Col1Unit, I).Value) & ", " &
                                    " Rate = " & Val(Dgl1.Item(Col1Rate, I).Value) & ", " &
                                    " MeasurePerPcs = " & Val(Dgl1.Item(Col1MeasurePerPcs, I).Value) & ", " &
                                    " MeasureUnit = " & AgL.Chk_Text(Dgl1.Item(Col1MeasureUnit, I).Value) & ", " &
                                    " TotalIndentMeasure = " & -Val(Dgl1.Item(Col1TotalMeasure, I).Value) & ", " &
                                    " MaterialPlan = " & AgL.Chk_Text(Dgl1.Item(Col1MaterialPlan, I).Tag) & ", " &
                                    " MaterialPlanSr = " & AgL.Chk_Text(Dgl1.Item(Col1MaterialPlanSr, I).Value) & ", " &
                                    " PurchIndent = " & AgL.Chk_Text(Dgl1.Item(Col1PurchIndent, I).Tag) & ", " &
                                    " PurchIndentSr = " & AgL.Chk_Text(Dgl1.Item(Col1PurchIndentSr, I).Value) & ", " &
                                    " T_Nature = " & AgTemplate.ClsMain.T_Nature.Cancellation & ", " &
                                    " V_Nature = " & AgL.Chk_Text(mV_Nature) & ", " &
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
            End If
        Next

        If bSelectionQry <> "" Then
            mQry = " INSERT INTO PurchIndentDetail (DocId, Sr, Item, IndentQty, " &
                    " Unit,	Rate, MeasurePerPcs, MeasureUnit, TotalIndentMeasure, " &
                    " MaterialPlan, MaterialPlanSr, PurchIndent, PurchIndentSr, T_Nature, V_Nature, Remark) " & bSelectionQry
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


        mQry = " SELECT H.* , SG.DispName AS IndentorName, " &
                " L.Sr, L.Item, I.Description AS ItemDesc, I.ManualCode AS ItemCode, " &
                " U.DecimalPlaces as QtyDecimalPlaces, MU.DecimalPlaces as MeasureDecimalPlaces, " &
                " L.IndentQty ,L.Unit, L.Rate, L.MeasurePerPcs, L.MeasureUnit, L.V_Nature, " &
                " L.TotalIndentMeasure, L.MaterialPlan, L.MaterialPlanSr , " &
                " L.PurchIndent, L.PurchIndentSr, Pi.V_Type || '-' || Pi.ManualRefNo As PurchIndentNo ,L.Remark AS LineRemark, " &
                " M.V_Type || '-' || Convert(NVarchar,M.ManualRefNo) AS PlanningNo " &
                " FROM " &
                " ( " &
                "   SELECT * FROM PurchIndent WHERE DocID = '" & SearchCode & "' " &
                " ) H  " &
                " LEFT JOIN SubGroup SG ON SG.SubCode = H.Indentor " &
                " LEFT JOIN PurchIndentDetail L ON L.DocId = H.DocId " &
                " LEFT JOIN Item I ON I.Code = L.Item  " &
                " LEFT JOIN PurchIndent Pi On L.PurchIndent = Pi.DocId " &
                " LEFT JOIN MaterialPlan M ON M.DocId = L.MaterialPlan  " &
                " LEFT JOIN Unit U On L.Unit = U.Code " &
                " LEFT JOIN Unit MU ON L.MeasureUnit = MU.Code " &
                " Order By Sr "

        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                TxtManualRefNo.Text = AgL.XNull(.Rows(0)("ManualRefNo"))
                TxtIndentor.Tag = AgL.XNull(.Rows(0)("Indentor"))
                TxtIndentor.Text = AgL.XNull(.Rows(0)("IndentorName"))
                TxtRemarks.Text = AgL.XNull(.Rows(0)("Remarks"))

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
                    Dgl1.Item(Col1Qty, I).Value = Math.Abs(AgL.VNull(.Rows(I)("IndentQty")))
                    Dgl1.Item(Col1Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                    Dgl1.Item(Col1Rate, I).Value = Format(AgL.VNull(.Rows(I)("Rate")), "0.000")
                    Dgl1.Item(Col1MeasureDecimalPlaces, I).Value = AgL.VNull(.Rows(I)("MeasureDecimalPlaces"))
                    Dgl1.Item(Col1MeasurePerPcs, I).Value = Format(AgL.VNull(.Rows(I)("MeasurePerPcs")), "0.000")
                    Dgl1.Item(Col1MeasureUnit, I).Value = AgL.XNull(.Rows(I)("MeasureUnit"))
                    Dgl1.Item(Col1TotalMeasure, I).Value = Format(Math.Abs(AgL.VNull(.Rows(I)("TotalIndentMeasure"))), "0.000")
                    Dgl1.Item(Col1Remark, I).Value = AgL.XNull(.Rows(I)("LineRemark"))

                    Dgl1.Item(Col1PurchIndent, I).Tag = AgL.XNull(.Rows(I)("PurchIndent"))
                    Dgl1.Item(Col1PurchIndent, I).Value = AgL.XNull(.Rows(I)("PurchIndentNo"))
                    Dgl1.Item(Col1PurchIndentSr, I).Value = AgL.XNull(.Rows(I)("PurchIndentSr"))

                    Dgl1.Item(Col1MaterialPlan, I).Value = AgL.XNull(.Rows(I)("PlanningNo"))
                    Dgl1.Item(Col1MaterialPlan, I).Tag = AgL.XNull(.Rows(I)("MaterialPlan"))
                    Dgl1.Item(Col1MaterialPlanSr, I).Value = AgL.XNull(.Rows(I)("MaterialPlanSr"))

                    If Not AgL.StrCmp(Dgl1.Item(Col1Unit, I).Value, Dgl1.Item(Col1Unit, 0).Value) Then IsSameUnit = False
                    If Not AgL.StrCmp(Dgl1.Item(Col1MeasureUnit, I).Value, Dgl1.Item(Col1MeasureUnit, 0).Value) Then IsSameMeasureUnit = False

                    If intQtyDecimalPlaces < Val(Dgl1.Item(Col1QtyDecimalPlaces, I).Value) Then intQtyDecimalPlaces = Val(Dgl1.Item(Col1QtyDecimalPlaces, I).Value)
                    If intMeasureDecimalPlaces < Val(Dgl1.Item(Col1MeasureDecimalPlaces, I).Value) Then intMeasureDecimalPlaces = Val(Dgl1.Item(Col1MeasureDecimalPlaces, I).Value)

                    LblTotalQty.Text = Val(LblTotalQty.Text) + Val(Dgl1.Item(Col1Qty, I).Value)
                    LblTotalMeasure.Text = Val(LblTotalMeasure.Text) + Val(Dgl1.Item(Col1TotalMeasure, I).Value)
                Next I
            End If
            If Dgl1.Item(Col1Unit, 0).Value <> "" And IsSameUnit Then LblTotalQtyText.Text = "Total Qty (" & Dgl1.Item(Col1Unit, 0).Value & ") :" Else LblTotalQtyText.Text = "Total Qty :"
            If Dgl1.Item(Col1MeasureUnit, 0).Value <> "" And IsSameMeasureUnit Then LblTotalMeasureText.Text = "Total Measure (" & Dgl1.Item(Col1MeasureUnit, 0).Value & ") :" Else LblTotalMeasureText.Text = "Total Measure :"
        End With
        LblTotalQty.Text = Format(Val(LblTotalQty.Text), "0.".PadRight(intQtyDecimalPlaces + 2, "0"))
        LblTotalMeasure.Text = Format(Val(LblTotalMeasure.Text), "0.".PadRight(intMeasureDecimalPlaces + 2, "0"))
    End Sub

    Private Sub FrmProductionOrder_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Topctrl1.ChangeAgGridState(Dgl1, False)
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
                mQry = " SELECT Max(L.Item) As Code, Max(I.Description) as Description, " &
                        " Max(I.ManualCode) As ManualCode,   " &
                        " Max(H.V_Type) || '-' ||  Max(H.ManualRefNo) AS PurchIndentNo,   " &
                        " Max(H.V_Date) as Indent_Date, Sum(L.IndentQty) - IfNull(Sum(Cd.Qty), 0) as [BalQty],   " &
                        " Max(I.Unit) as Unit,  " &
                        " Sum(L.TotalIndentMeasure) - IfNull(Sum(Cd.TotalMeasure), 0) as [BalMeasure],   " &
                        " Max(I.MeasureUnit) MeasureUnit, Max(L.Rate) as Rate,   " &
                        " Max(I.SalesTaxPostingGroup) SalesTaxPostingGroup,   " &
                        " Max(L.MeasurePerPcs) as MeasurePerPcs,   " &
                        " Max(L.MaterialPlan) As MaterialPlan, Max(L.MaterialPlanSr) As MaterialPlanSr, Max(Mp.ManualRefNo) As MaterialPlanNo, " &
                        " L.PurchIndent, L.PurchIndentSr,   " &
                        " Max(U.DecimalPlaces) as QtyDecimalPlaces, Max(U1.DecimalPlaces) as MeasureDecimalPlaces " &
                        " FROM (  " &
                        " 	    SELECT DocID, V_Type, ManualRefNo, V_Date   " &
                        " 	    FROM PurchIndent    " &
                        " 	    WHERE Div_Code = '" & AgL.PubDivCode & "' " &
                        " 	    AND Site_Code = '" & AgL.PubSiteCode & "' " &
                        " 	    AND V_Date <= '" & TxtV_Date.Text & "' " &
                        " ) H   " &
                        " LEFT JOIN PurchIndentDetail L  ON H.DocID = L.PurchIndent " &
                        " LEFT JOIN MaterialPlan Mp On L.MaterialPlan = Mp.DocId " &
                        " Left Join Item I  On L.Item  = I.Code   " &
                        " LEFT JOIN Voucher_Type Vt  ON H.V_Type = Vt.V_Type    " &
                        " Left Join (   " &
                        " 	    SELECT L.PurchIndent, L.PurchIndentSr, sum (L.Qty) AS Qty,  " &
                        " 	    Sum(L.TotalMeasure) as TotalMeasure " &
                        " 	    FROM PurchOrderDetail L     " &
                        " 	    Where DocId <> '" & mInternalCode & "'   " &
                        " 	    GROUP BY L.PurchIndent, L.PurchIndentSr   " &
                        " ) AS CD ON L.DocID = CD.PurchIndent AND L.Sr = CD.PurchIndentSr   " &
                        " LEFT JOIN Unit U On L.Unit = U.Code   " &
                        " LEFT JOIN Unit U1 On L.MeasureUnit = U1.Code   " &
                        " WHERE 1=1     " &
                        " And Vt.NCat = '" & AgTemplate.ClsMain.Temp_NCat.PurchaseIndent & "'" &
                        " GROUP BY L.PurchIndent, L.PurchIndentSr   " &
                        " Having Sum(L.IndentQty) - Sum(IfNull(Cd.Qty, 0)) > 0  " &
                        " Order By Description, Indent_Date "
                Dgl1.AgHelpDataSet(Col1Item, 12) = AgL.FillData(mQry, AgL.GCn)

            Case Col1ItemCode
                mQry = " SELECT Max(L.Item) As Code, Max(I.ManualCode) As ManualCode, " &
                        " Max(I.Description) as Description, " &
                        " Max(H.V_Type) || '-' ||  Max(H.ManualRefNo) AS PurchIndentNo,   " &
                        " Max(H.V_Date) as Indent_Date, Sum(L.IndentQty) - IfNull(Sum(Cd.Qty), 0) as [BalQty],   " &
                        " Max(I.Unit) as Unit,  " &
                        " Sum(L.TotalIndentMeasure) - IfNull(Sum(Cd.TotalMeasure), 0) as [BalMeasure],   " &
                        " Max(I.MeasureUnit) MeasureUnit, Max(L.Rate) as Rate,   " &
                        " Max(I.SalesTaxPostingGroup) SalesTaxPostingGroup,   " &
                        " Max(L.MeasurePerPcs) as MeasurePerPcs,   " &
                        " L.PurchIndent, L.PurchIndentSr,   " &
                        " Max(U.DecimalPlaces) as QtyDecimalPlaces, Max(U1.DecimalPlaces) as MeasureDecimalPlaces " &
                        " FROM (  " &
                        " 	    SELECT DocID, V_Type, ManualRefNo, V_Date   " &
                        " 	    FROM PurchIndent    " &
                        " 	    WHERE Div_Code = '" & AgL.PubDivCode & "' " &
                        " 	    AND Site_Code = '" & AgL.PubSiteCode & "' " &
                        " 	    AND V_Date <= '" & TxtV_Date.Text & "' " &
                        " ) H   " &
                        " LEFT JOIN PurchIndentDetail L  ON H.DocID = L.PurchIndent " &
                        " Left Join Item I  On L.Item  = I.Code   " &
                        " LEFT JOIN Voucher_Type Vt  ON H.V_Type = Vt.V_Type    " &
                        " Left Join (   " &
                        " 	    SELECT L.PurchIndent, L.PurchIndentSr, sum (L.Qty) AS Qty,  " &
                        " 	    Sum(L.TotalMeasure) as TotalMeasure " &
                        " 	    FROM PurchOrderDetail L     " &
                        " 	    Where DocId <> '" & mInternalCode & "'   " &
                        " 	    GROUP BY L.PurchIndent, L.PurchIndentSr   " &
                        " ) AS CD ON L.DocID = CD.PurchIndent AND L.Sr = CD.PurchIndentSr   " &
                        " LEFT JOIN Unit U On L.Unit = U.Code   " &
                        " LEFT JOIN Unit U1 On L.MeasureUnit = U1.Code   " &
                        " WHERE 1=1     " &
                        " And Vt.NCat = '" & AgTemplate.ClsMain.Temp_NCat.PurchaseIndent & "'" &
                        " GROUP BY L.PurchIndent, L.PurchIndentSr   " &
                        " Having Sum(L.IndentQty) - Sum(IfNull(Cd.Qty, 0)) > 0  " &
                        " Order By Description, Indent_Date "
                Dgl1.AgHelpDataSet(Col1ItemCode) = AgL.FillData(mQry, AgL.GCn)
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
                Dgl1.Item(Col1TotalMeasure, I).Value = Format(Val(Dgl1.Item(Col1Qty, I).Value) * Val(Dgl1.Item(Col1MeasurePerPcs, I).Value), "0.00")
                'Footer Calculation
                LblTotalQty.Text = Val(LblTotalQty.Text) + Val(Dgl1.Item(Col1Qty, I).Value)
                LblTotalMeasure.Text = Val(LblTotalMeasure.Text) + Val(Dgl1.Item(Col1TotalMeasure, I).Value)
            End If
        Next
        LblTotalMeasure.Text = Format(Val(LblTotalMeasure.Text), "0.000")
        LblTotalQty.Text = Format(Val(LblTotalQty.Text), "0.000")
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        Dim I As Integer = 0

        If AgL.RequiredField(TxtIndentor, LblIndentor.Text) Then passed = False : Exit Sub
        If AgL.RequiredField(TxtManualRefNo, LblReferenceNo.Text) Then passed = False : Exit Sub

        If AgCL.AgIsBlankGrid(Dgl1, Dgl1.Columns(Col1Item).Index) Then passed = False : Exit Sub

        If AgCL.AgIsDuplicate(Dgl1, Dgl1.Columns(Col1Item).Index & "," & Dgl1.Columns(Col1MaterialPlan).Index) Then passed = False : Exit Sub

        passed = AgTemplate.ClsMain.FCheckDuplicateRefNo("ManualRefNo", "PurchIndent",
                                    TxtV_Type.Tag, TxtV_Date.Text, TxtDivision.Tag,
                                    TxtSite_Code.Tag, Topctrl1.Mode,
                                    TxtManualRefNo.Text, mInternalCode)


        With Dgl1
            For I = 0 To .Rows.Count - 1
                If .Item(Col1Item, I).Value <> "" Then
                    If Val(.Item(Col1Qty, I).Value) = 0 Then
                        MsgBox("Qty Is 0 At Row No " & Dgl1.Item(ColSNo, I).Value & "")
                        .CurrentCell = .Item(Col1Qty, I) : Dgl1.Focus()
                        passed = False : Exit Sub
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
                Dgl1.Item(Col1Qty, mRow).Value = 0
                Dgl1.Item(Col1TotalMeasure, mRow).Value = 0
                Dgl1.Item(Col1MaterialPlan, mRow).Value = ""
                Dgl1.Item(Col1MaterialPlan, mRow).Tag = ""
                Dgl1.Item(Col1MaterialPlanSr, mRow).Value = ""
                Dgl1.Item(Col1PurchIndent, mRow).Value = ""
                Dgl1.Item(Col1PurchIndent, mRow).Tag = ""
                Dgl1.Item(Col1PurchIndentSr, mRow).Value = ""
                Dgl1.Item(Col1Item, mRow).Value = ""
                Dgl1.Item(Col1Item, mRow).Tag = ""
                Dgl1.Item(Col1ItemCode, mRow).Value = ""
                Dgl1.Item(Col1ItemCode, mRow).Tag = ""
            Else
                If Dgl1.AgDataRow IsNot Nothing Then
                    Dgl1.Item(Col1Item, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("Code").Value)
                    Dgl1.Item(Col1Item, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("Description").Value)
                    Dgl1.Item(Col1ItemCode, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("Code").Value)
                    Dgl1.Item(Col1ItemCode, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("ManualCode").Value)
                    Dgl1.Item(Col1Unit, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("Unit").Value)
                    Dgl1.Item(Col1MeasurePerPcs, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("MeasurePerPcs").Value)
                    Dgl1.Item(Col1MeasureUnit, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("MeasureUnit").Value)
                    Dgl1.Item(Col1Qty, mRow).Value = Format(AgL.VNull(Dgl1.AgDataRow.Cells("BalQty").Value), "0.".PadRight(AgL.VNull(Dgl1.AgDataRow.Cells("QtyDecimalPlaces").Value) + 2, "0"))
                    Dgl1.Item(Col1MaterialPlan, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("MaterialPlan").Value)
                    Dgl1.Item(Col1MaterialPlan, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("MaterialPlanNo").Value)
                    Dgl1.Item(Col1MaterialPlanSr, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("MaterialPlanSr").Value)
                    Dgl1.Item(Col1PurchIndent, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("PurchIndent").Value)
                    Dgl1.Item(Col1PurchIndent, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("PurchIndentNo").Value)
                    Dgl1.Item(Col1PurchIndentSr, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("PurchIndentSr").Value)
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

                Case Col1ItemCode
                    Validating_Item(Dgl1.AgSelectedValue(Col1ItemCode, mRowIndex), mRowIndex, Col1ItemCode)
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

    Private Sub Txt_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtIndentor.KeyDown
        Dim strCond$ = ""
        Try
            Select Case sender.Name
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
           " Where IfNull(H.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') =  '" & AgTemplate.ClsMain.EntryStatus.Active & "'  " & strCond
        sender.AgHelpDataSet(4, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
    End Sub

    Private Sub FrmPurchIndent_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub

    Private Sub FrmPurchIndent_BaseEvent_Topctrl_tbPrn(ByVal SearchCode As String) Handles Me.BaseEvent_Topctrl_tbPrn
        mQry = " SELECT H.DocID, H.V_Type, H.V_Date, SG.DispName AS IndentorName, H.Remarks, H.EntryBy, " &
                " H.ApproveBy, H.ManualRefNo, L.Sr, I.Description AS ItemDesc, Abs(L.IndentQty) As IndentQty, L.Unit, L.Rate, L.Specification, " &
                " L.Remark AS LineRemark, U.DecimalPlaces , P.V_Type +'-' || P.ManualRefNo AS IndentNo, " &
                " MP.V_Type +'-' || MP.ManualRefNo AS PlanNo " &
                " FROM PurchIndent H " &
                " LEFT JOIN PurchIndentDetail L ON L.DocId = H.DocID  " &
                " LEFT JOIN PurchIndent P On L.PurchIndent = P.DocId " &
                " LEFT JOIN SubGroup SG ON SG.SubCode = H.Indentor  " &
                " LEFT JOIN Item I ON I.Code = L.Item  " &
                " LEFT JOIN Unit U ON U.Code = L.Unit " &
                " LEFT JOIN MaterialPlan MP ON MP.DocID = L.MaterialPlan " &
                " Where H.DocId = '" & mSearchCode & "' "
        ClsMain.FPrintThisDocument(Me, TxtV_Type.Tag, mQry, "PurchIndentCancel_Print", "Purchase Indent Cancel")
    End Sub

    Private Sub FrmPurchIndent_BaseEvent_Topctrl_tbRef() Handles Me.BaseEvent_Topctrl_tbRef
        If TxtIndentor.AgHelpDataSet IsNot Nothing Then TxtIndentor.AgHelpDataSet.Dispose() : TxtIndentor.AgHelpDataSet = Nothing
        If Dgl1.AgHelpDataSet(Col1Item) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1Item).Dispose() : Dgl1.AgHelpDataSet(Col1Item) = Nothing
        If Dgl1.AgHelpDataSet(Col1ItemCode) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1ItemCode).Dispose() : Dgl1.AgHelpDataSet(Col1ItemCode) = Nothing
    End Sub

    Private Sub Txt_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtV_Type.Validating, TxtManualRefNo.Validating
        Dim DtTemp As DataTable = Nothing
        Dim DsTemp As DataSet = Nothing
        Dim FrmObj As New FrmPurchPartyDetail
        Try
            Select Case sender.NAME
                Case TxtV_Type.Name
                    IniGrid()
                    TxtManualRefNo.Text = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "PurchIndent", TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, AgTemplate.ClsMain.ManualRefType.Max)


                Case TxtManualRefNo.Name
                    e.Cancel = Not AgTemplate.ClsMain.FCheckDuplicateRefNo("ManualRefNo", "PurchIndent",
                                    TxtV_Type.Tag, TxtV_Date.Text, TxtDivision.Tag,
                                    TxtSite_Code.Tag, Topctrl1.Mode,
                                    TxtManualRefNo.Text, mInternalCode)


            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FrmPurchIndent_BaseEvent_Topctrl_tbAdd() Handles Me.BaseEvent_Topctrl_tbAdd
        TxtManualRefNo.Text = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "PurchIndent", TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, AgTemplate.ClsMain.ManualRefType.Max)
    End Sub

    Private Sub BtnFillSaleChallan_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnFillIndentDetail.Click
        Try
            If Topctrl1.Mode = "Browse" Then Exit Sub
            Dim StrTicked As String

            StrTicked = FHPGD_PendingPurchIndent()
            If StrTicked <> "" Then
                FFillItemsForIndent(StrTicked)
            Else
                Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
            End If

            Dgl1.Focus()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function FHPGD_PendingPurchIndent() As String
        Dim FRH_Multiple As DMHelpGrid.FrmHelpGrid_Multi
        Dim StrRtn As String = ""

        Dim strCond As String = ""

        strCond = " And Div_Code = '" & TxtDivision.Tag & "'   " &
                    " AND Site_Code = '" & TxtSite_Code.Tag & "'   " &
                    " AND V_Date <= '" & TxtV_Date.Text & "'  "

        mQry = " SELECT 'o' As Tick, VMain.PurchIndent, Max(VMain.PurchIndentNo) AS PurchIndentNo, " &
                " Max(VMain.Indent_Date) AS PurchIndentDate, IfNull(Sum(VMain.BalQty), 0) As [Qty]    " &
                " FROM ( " & FRetFillItemWiseQry(strCond, " And L.DocId <> '" & mSearchCode & "'") & " ) As VMain " &
                " GROUP BY VMain.PurchIndent " &
                " Order By PurchIndentDate "

        FRH_Multiple = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(AgL.FillData(mQry, AgL.GCn).TABLES(0)), "", 400, 450, , , False)
        FRH_Multiple.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple.FFormatColumn(1, , 0, , False)
        FRH_Multiple.FFormatColumn(2, "Indent No.", 150, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(3, "Indent Date", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(4, "Balance", 70, DataGridViewContentAlignment.MiddleRight)

        FRH_Multiple.StartPosition = FormStartPosition.CenterScreen
        FRH_Multiple.ShowDialog()

        If FRH_Multiple.BytBtnValue = 0 Then
            StrRtn = FRH_Multiple.FFetchData(1, "'", "'", ",", True)
        End If
        FHPGD_PendingPurchIndent = StrRtn

        FRH_Multiple = Nothing
    End Function

    Private Sub FFillItemsForIndent(ByVal bIndentNoStr As String)
        Dim I As Integer = 0
        Dim DtTemp As DataTable = Nothing
        Try
            If bIndentNoStr = "" Then Exit Sub

            mQry = FRetFillItemWiseQry(" And DocId In (" & bIndentNoStr & ") ", " And L.DocId <> '" & mSearchCode & "'")
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
                        Dgl1.Item(Col1PurchIndent, J).Tag = AgL.XNull(.Rows(I)("PurchIndent"))
                        Dgl1.Item(Col1PurchIndent, J).Value = AgL.XNull(.Rows(I)("PurchIndentNo"))
                        Dgl1.Item(Col1PurchIndentSr, J).Value = AgL.XNull(.Rows(I)("PurchIndentSr"))

                        Dgl1.Item(Col1MaterialPlan, J).Tag = AgL.XNull(.Rows(I)("MaterialPlan"))
                        Dgl1.Item(Col1MaterialPlan, J).Value = AgL.XNull(.Rows(I)("MaterialPlanNo"))
                        Dgl1.Item(Col1MaterialPlanSr, J).Value = AgL.XNull(.Rows(I)("MaterialPlanSr"))

                        Dgl1.Item(Col1Item, J).Tag = AgL.XNull(.Rows(I)("Code"))
                        Dgl1.Item(Col1Item, J).Value = AgL.XNull(.Rows(I)("Description"))

                        Dgl1.Item(Col1Qty, J).Value = Format(AgL.VNull(.Rows(I)("BalQty")), "0.".PadRight(AgL.VNull(.Rows(I)("QtyDecimalPlaces")) + 2, "0"))
                        Dgl1.Item(Col1Unit, J).Value = AgL.XNull(.Rows(I)("Unit"))
                        Dgl1.Item(Col1MeasurePerPcs, J).Value = Format(AgL.VNull(.Rows(I)("MeasurePerPcs")), "0.".PadRight(AgL.VNull(.Rows(I)("MeasureDecimalPlaces")) + 2, "0"))
                        Dgl1.Item(Col1MeasureUnit, J).Value = AgL.XNull(.Rows(I)("MeasureUnit"))
                        Dgl1.Item(Col1Rate, J).Value = Format(AgL.VNull(.Rows(I)("Rate")), "0.00")

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
        Dim strCond As String = ""
        Dim ContraV_TypeCondStr As String = ""

        If DtV_TypeSettings.Rows.Count > 0 Then
            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")) <> "" Then
                strCond += " And CharIndex('|' || I.ItemType || '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemType")) & "') > 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemGroup")) <> "" Then
                strCond += " And CharIndex('|' || I.ItemGroup || '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_ItemGroup")) & "') > 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Item")) <> "" Then
                strCond += " And CharIndex('|' || I.Code || '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Item")) & "') > 0 "
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

        FRetFillItemWiseQry = " SELECT Max(L.Item) As Code, Max(I.Description) as Description, " &
                " Max(I.ManualCode) As ManualCode,   " &
                " Max(H.V_Type) || '-' ||  Max(H.ManualRefNo) AS PurchIndentNo,   " &
                " Max(H.V_Date) as Indent_Date, Sum(L.IndentQty) - IfNull(Sum(Cd.Qty), 0) as [BalQty],   " &
                " Max(I.Unit) as Unit,  " &
                " Sum(L.TotalIndentMeasure) - IfNull(Sum(Cd.TotalMeasure), 0) as [BalMeasure],   " &
                " Max(I.MeasureUnit) MeasureUnit, Max(L.Rate) as Rate,   " &
                " Max(I.SalesTaxPostingGroup) SalesTaxPostingGroup,   " &
                " Max(L.MeasurePerPcs) as MeasurePerPcs,   " &
                " Max(L.MaterialPlan) As MaterialPlan, Max(L.MaterialPlanSr) As MaterialPlanSr, Max(Mp.ManualRefNo) As MaterialPlanNo, " &
                " L.PurchIndent, L.PurchIndentSr,   " &
                " Max(U.DecimalPlaces) as QtyDecimalPlaces, Max(U1.DecimalPlaces) as MeasureDecimalPlaces " &
                " FROM (  " &
                " 	    SELECT DocID, V_Type, ManualRefNo, V_Date   " &
                " 	    FROM PurchIndent    " &
                " 	    WHERE 1=1 " & HeaderConStr & " " &
                " ) H   " &
                " LEFT JOIN PurchIndentDetail L  ON H.DocID = L.PurchIndent " &
                " LEFT JOIN MaterialPlan Mp On L.MaterialPlan = Mp.DocId " &
                " Left Join Item I  On L.Item  = I.Code   " &
                " LEFT JOIN Voucher_Type Vt  ON H.V_Type = Vt.V_Type    " &
                " Left Join (   " &
                " 	    SELECT L.PurchIndent, L.PurchIndentSr, sum (L.Qty) AS Qty,  " &
                " 	    Sum(L.TotalMeasure) as TotalMeasure " &
                " 	    FROM PurchOrderDetail L     " &
                " 	    Where DocId <> '" & mInternalCode & "'   " &
                " 	    GROUP BY L.PurchIndent, L.PurchIndentSr   " &
                " ) AS CD ON L.DocID = CD.PurchIndent AND L.Sr = CD.PurchIndentSr   " &
                " LEFT JOIN Unit U On L.Unit = U.Code   " &
                " LEFT JOIN Unit U1 On L.MeasureUnit = U1.Code   " &
                " WHERE 1 = 1 " & LineConStr &
                " And Vt.NCat = '" & AgTemplate.ClsMain.Temp_NCat.PurchaseIndent & "'" &
                " GROUP BY L.PurchIndent, L.PurchIndentSr   " &
                " Having Sum(L.IndentQty) - Sum(IfNull(Cd.Qty, 0)) > 0  "
    End Function
End Class
