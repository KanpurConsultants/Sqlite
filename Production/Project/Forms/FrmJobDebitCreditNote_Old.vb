Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data.SQLite
Public Class FrmJobDebitCreaditNote
    Inherits AgTemplate.TempTransaction
    Dim mQry$

    Dim DtDuesPaymentEnviro As DataTable = Nothing

    Protected Const ColSNo As String = "S.No."
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Protected Const Col1CostCenterSubCode As String = "Cost Center Sub Code"
    Protected Const Col1JobOrder As String = "Job Order"
    Protected Const Col1SubCode As String = "Party"
    Protected Const Col1Amount As String = "Amount"
    Protected Const Col1ReferenceDocId As String = "Bill No"
    Protected Const Col1BillType As String = "Bill Type"
    Protected Const Col1Remark As String = "Narration"

    Dim bBankAc$ = "", bCashAc$ = ""
    Dim mTransactionType As EnumTransType = EnumTransType.Payment
    Protected WithEvents TxtCostcenter As AgControls.AgTextBox
    Protected WithEvents Label6 As System.Windows.Forms.Label
    Protected WithEvents Label4 As System.Windows.Forms.Label
    Protected WithEvents TxtProcess As AgControls.AgTextBox
    Protected WithEvents Label5 As System.Windows.Forms.Label
    Public RowLockedColour As Color = Color.AliceBlue

    Enum EnumTransType
        Payment = 1
        Receipt = 2
    End Enum

    Public Property TransactionType() As EnumTransType
        Get
            TransactionType = mTransactionType
        End Get
        Set(ByVal value As EnumTransType)
            mTransactionType = value
        End Set
    End Property

    Public Sub New(ByVal StrUPVar As String, ByVal DTUP As DataTable, ByVal bEntryNCat As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Topctrl1.FSetParent(Me, StrUPVar, DTUP)
        Topctrl1.SetDisp(True)

        EntryNCat = bEntryNCat

        mQry = "Select H.* from Voucher_Type_Settings H Left Join Voucher_Type Vt On H.V_Type = Vt.V_Type  Where Vt.NCat In ('" & EntryNCat & "') And H.Div_Code = '" & AgL.PubDivCode & "' And H.Site_Code ='" & AgL.PubSiteCode & "' "
        DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)

        mQry = "Select H.* from DuesPaymentEnviro H Left Join Voucher_Type Vt On H.V_Type = Vt.V_Type  Where Vt.NCat In ('" & EntryNCat & "') "
        DtDuesPaymentEnviro = AgL.FillData(mQry, AgL.GCn).Tables(0)
    End Sub

#Region "Form Designer Code"
    Private Sub InitializeComponent()
        Me.Pnl2 = New System.Windows.Forms.Panel
        Me.TxtRemarks = New AgControls.AgTextBox
        Me.Label30 = New System.Windows.Forms.Label
        Me.LblPaymentDetail = New System.Windows.Forms.LinkLabel
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.LblTotalAmount = New System.Windows.Forms.Label
        Me.LblTotalAmountText = New System.Windows.Forms.Label
        Me.TxtSubCode = New AgControls.AgTextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.TxtManualRefNo = New AgControls.AgTextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.TxtProcess = New AgControls.AgTextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.TxtCostcenter = New AgControls.AgTextBox
        Me.GroupBox2.SuspendLayout()
        Me.GBoxMoveToLog.SuspendLayout()
        Me.GBoxApprove.SuspendLayout()
        Me.GBoxEntryType.SuspendLayout()
        Me.GrpUP.SuspendLayout()
        Me.GBoxDivision.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TP1.SuspendLayout()
        CType(Me.DTMaster, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.Location = New System.Drawing.Point(807, 573)
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
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(638, 573)
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
        Me.GBoxApprove.Location = New System.Drawing.Point(469, 573)
        Me.GBoxApprove.Size = New System.Drawing.Size(148, 40)
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
        Me.GBoxEntryType.Location = New System.Drawing.Point(160, 573)
        Me.GBoxEntryType.Size = New System.Drawing.Size(119, 40)
        '
        'TxtEntryType
        '
        Me.TxtEntryType.Location = New System.Drawing.Point(3, 19)
        Me.TxtEntryType.Tag = ""
        '
        'GrpUP
        '
        Me.GrpUP.Location = New System.Drawing.Point(20, 573)
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
        Me.GroupBox1.Location = New System.Drawing.Point(2, 565)
        Me.GroupBox1.Size = New System.Drawing.Size(1002, 4)
        '
        'GBoxDivision
        '
        Me.GBoxDivision.Location = New System.Drawing.Point(300, 573)
        Me.GBoxDivision.Size = New System.Drawing.Size(148, 40)
        '
        'TxtDivision
        '
        Me.TxtDivision.AgSelectedValue = ""
        Me.TxtDivision.Location = New System.Drawing.Point(3, 19)
        Me.TxtDivision.Size = New System.Drawing.Size(142, 18)
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
        Me.LblV_No.Location = New System.Drawing.Point(495, 34)
        Me.LblV_No.Tag = ""
        '
        'TxtV_No
        '
        Me.TxtV_No.AgSelectedValue = ""
        Me.TxtV_No.BackColor = System.Drawing.Color.White
        Me.TxtV_No.Location = New System.Drawing.Point(848, 60)
        Me.TxtV_No.Size = New System.Drawing.Size(123, 18)
        Me.TxtV_No.TabIndex = 3
        Me.TxtV_No.Tag = ""
        Me.TxtV_No.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.TxtV_No.Visible = False
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(369, 39)
        Me.Label2.Tag = ""
        '
        'LblV_Date
        '
        Me.LblV_Date.BackColor = System.Drawing.Color.Transparent
        Me.LblV_Date.Location = New System.Drawing.Point(272, 34)
        Me.LblV_Date.Tag = ""
        '
        'LblV_TypeReq
        '
        Me.LblV_TypeReq.Location = New System.Drawing.Point(585, 19)
        Me.LblV_TypeReq.Tag = ""
        '
        'TxtV_Date
        '
        Me.TxtV_Date.AgSelectedValue = ""
        Me.TxtV_Date.BackColor = System.Drawing.Color.White
        Me.TxtV_Date.Location = New System.Drawing.Point(387, 33)
        Me.TxtV_Date.TabIndex = 2
        Me.TxtV_Date.Tag = ""
        '
        'LblV_Type
        '
        Me.LblV_Type.Location = New System.Drawing.Point(495, 15)
        Me.LblV_Type.Tag = ""
        '
        'TxtV_Type
        '
        Me.TxtV_Type.AgSelectedValue = ""
        Me.TxtV_Type.BackColor = System.Drawing.Color.White
        Me.TxtV_Type.Location = New System.Drawing.Point(601, 13)
        Me.TxtV_Type.Size = New System.Drawing.Size(123, 18)
        Me.TxtV_Type.TabIndex = 1
        Me.TxtV_Type.Tag = ""
        '
        'LblSite_CodeReq
        '
        Me.LblSite_CodeReq.Location = New System.Drawing.Point(369, 19)
        Me.LblSite_CodeReq.Tag = ""
        '
        'LblSite_Code
        '
        Me.LblSite_Code.BackColor = System.Drawing.Color.Transparent
        Me.LblSite_Code.Location = New System.Drawing.Point(272, 14)
        Me.LblSite_Code.Size = New System.Drawing.Size(87, 16)
        Me.LblSite_Code.Tag = ""
        Me.LblSite_Code.Text = "Branch Name"
        '
        'TxtSite_Code
        '
        Me.TxtSite_Code.AgSelectedValue = ""
        Me.TxtSite_Code.BackColor = System.Drawing.Color.White
        Me.TxtSite_Code.Location = New System.Drawing.Point(387, 13)
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
        Me.LblPrefix.Location = New System.Drawing.Point(852, 19)
        Me.LblPrefix.Tag = ""
        Me.LblPrefix.Visible = False
        '
        'TabControl1
        '
        Me.TabControl1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl1.Location = New System.Drawing.Point(-3, 18)
        Me.TabControl1.Size = New System.Drawing.Size(991, 155)
        Me.TabControl1.TabIndex = 1
        '
        'TP1
        '
        Me.TP1.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.TP1.Controls.Add(Me.TxtCostcenter)
        Me.TP1.Controls.Add(Me.Label6)
        Me.TP1.Controls.Add(Me.Label4)
        Me.TP1.Controls.Add(Me.TxtProcess)
        Me.TP1.Controls.Add(Me.Label5)
        Me.TP1.Controls.Add(Me.TxtManualRefNo)
        Me.TP1.Controls.Add(Me.Label3)
        Me.TP1.Controls.Add(Me.TxtSubCode)
        Me.TP1.Controls.Add(Me.Label1)
        Me.TP1.Controls.Add(Me.TxtRemarks)
        Me.TP1.Controls.Add(Me.Label30)
        Me.TP1.Location = New System.Drawing.Point(4, 22)
        Me.TP1.Size = New System.Drawing.Size(983, 129)
        Me.TP1.Text = "Document Detail"
        Me.TP1.Controls.SetChildIndex(Me.Label30, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtRemarks, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label1, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtSubCode, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label3, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtManualRefNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label5, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtProcess, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label4, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label6, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtCostcenter, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_TypeReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtV_Type, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_Type, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblPrefix, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblSite_CodeReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtV_Date, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_No, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label2, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtV_No, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtSite_Code, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_Date, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblDocId, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtDocId, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblSite_Code, 0)
        '
        'Topctrl1
        '
        Me.Topctrl1.Size = New System.Drawing.Size(984, 41)
        Me.Topctrl1.TabIndex = 0
        '
        'Pnl2
        '
        Me.Pnl2.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Pnl2.Location = New System.Drawing.Point(7, 207)
        Me.Pnl2.Name = "Pnl2"
        Me.Pnl2.Size = New System.Drawing.Size(969, 331)
        Me.Pnl2.TabIndex = 2
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
        Me.TxtRemarks.Location = New System.Drawing.Point(387, 93)
        Me.TxtRemarks.MaxLength = 255
        Me.TxtRemarks.Name = "TxtRemarks"
        Me.TxtRemarks.Size = New System.Drawing.Size(337, 18)
        Me.TxtRemarks.TabIndex = 7
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(272, 94)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(60, 16)
        Me.Label30.TabIndex = 744
        Me.Label30.Text = "Remarks"
        '
        'LblPaymentDetail
        '
        Me.LblPaymentDetail.BackColor = System.Drawing.Color.SteelBlue
        Me.LblPaymentDetail.DisabledLinkColor = System.Drawing.Color.White
        Me.LblPaymentDetail.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblPaymentDetail.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LblPaymentDetail.LinkColor = System.Drawing.Color.White
        Me.LblPaymentDetail.Location = New System.Drawing.Point(7, 185)
        Me.LblPaymentDetail.Name = "LblPaymentDetail"
        Me.LblPaymentDetail.Size = New System.Drawing.Size(119, 20)
        Me.LblPaymentDetail.TabIndex = 733
        Me.LblPaymentDetail.TabStop = True
        Me.LblPaymentDetail.Text = "Payment Detail"
        Me.LblPaymentDetail.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Cornsilk
        Me.Panel1.Controls.Add(Me.LblTotalAmount)
        Me.Panel1.Controls.Add(Me.LblTotalAmountText)
        Me.Panel1.Location = New System.Drawing.Point(7, 539)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(969, 23)
        Me.Panel1.TabIndex = 734
        '
        'LblTotalAmount
        '
        Me.LblTotalAmount.AutoSize = True
        Me.LblTotalAmount.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalAmount.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalAmount.Location = New System.Drawing.Point(397, 3)
        Me.LblTotalAmount.Name = "LblTotalAmount"
        Me.LblTotalAmount.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalAmount.TabIndex = 673
        Me.LblTotalAmount.Text = "."
        Me.LblTotalAmount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTotalAmountText
        '
        Me.LblTotalAmountText.AutoSize = True
        Me.LblTotalAmountText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalAmountText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalAmountText.Location = New System.Drawing.Point(291, 3)
        Me.LblTotalAmountText.Name = "LblTotalAmountText"
        Me.LblTotalAmountText.Size = New System.Drawing.Size(100, 16)
        Me.LblTotalAmountText.TabIndex = 672
        Me.LblTotalAmountText.Text = "Total Amount :"
        '
        'TxtSubCode
        '
        Me.TxtSubCode.AgAllowUserToEnableMasterHelp = False
        Me.TxtSubCode.AgLastValueTag = Nothing
        Me.TxtSubCode.AgLastValueText = Nothing
        Me.TxtSubCode.AgMandatory = True
        Me.TxtSubCode.AgMasterHelp = False
        Me.TxtSubCode.AgNumberLeftPlaces = 0
        Me.TxtSubCode.AgNumberNegetiveAllow = False
        Me.TxtSubCode.AgNumberRightPlaces = 0
        Me.TxtSubCode.AgPickFromLastValue = False
        Me.TxtSubCode.AgRowFilter = ""
        Me.TxtSubCode.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtSubCode.AgSelectedValue = Nothing
        Me.TxtSubCode.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtSubCode.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtSubCode.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtSubCode.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtSubCode.Location = New System.Drawing.Point(387, 53)
        Me.TxtSubCode.MaxLength = 255
        Me.TxtSubCode.Name = "TxtSubCode"
        Me.TxtSubCode.Size = New System.Drawing.Size(337, 18)
        Me.TxtSubCode.TabIndex = 4
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(272, 54)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(75, 16)
        Me.Label1.TabIndex = 746
        Me.Label1.Text = "Reason A/c"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(369, 60)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(10, 7)
        Me.Label3.TabIndex = 747
        Me.Label3.Text = "Ä"
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
        Me.TxtManualRefNo.Location = New System.Drawing.Point(601, 33)
        Me.TxtManualRefNo.MaxLength = 50
        Me.TxtManualRefNo.Name = "TxtManualRefNo"
        Me.TxtManualRefNo.Size = New System.Drawing.Size(123, 18)
        Me.TxtManualRefNo.TabIndex = 3
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(272, 74)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(56, 16)
        Me.Label5.TabIndex = 749
        Me.Label5.Text = "Process"
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
        Me.TxtProcess.Location = New System.Drawing.Point(387, 73)
        Me.TxtProcess.MaxLength = 20
        Me.TxtProcess.Name = "TxtProcess"
        Me.TxtProcess.Size = New System.Drawing.Size(100, 18)
        Me.TxtProcess.TabIndex = 5
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(369, 80)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(10, 7)
        Me.Label4.TabIndex = 750
        Me.Label4.Text = "Ä"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(495, 74)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(77, 16)
        Me.Label6.TabIndex = 752
        Me.Label6.Text = "Cost Center"
        '
        'TxtCostcenter
        '
        Me.TxtCostcenter.AgAllowUserToEnableMasterHelp = False
        Me.TxtCostcenter.AgLastValueTag = Nothing
        Me.TxtCostcenter.AgLastValueText = Nothing
        Me.TxtCostcenter.AgMandatory = True
        Me.TxtCostcenter.AgMasterHelp = False
        Me.TxtCostcenter.AgNumberLeftPlaces = 8
        Me.TxtCostcenter.AgNumberNegetiveAllow = False
        Me.TxtCostcenter.AgNumberRightPlaces = 2
        Me.TxtCostcenter.AgPickFromLastValue = False
        Me.TxtCostcenter.AgRowFilter = ""
        Me.TxtCostcenter.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtCostcenter.AgSelectedValue = Nothing
        Me.TxtCostcenter.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtCostcenter.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtCostcenter.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtCostcenter.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCostcenter.Location = New System.Drawing.Point(601, 73)
        Me.TxtCostcenter.MaxLength = 20
        Me.TxtCostcenter.Name = "TxtCostcenter"
        Me.TxtCostcenter.Size = New System.Drawing.Size(123, 18)
        Me.TxtCostcenter.TabIndex = 6
        '
        'FrmJobDebitCreaditNote
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.BackColor = System.Drawing.SystemColors.ButtonShadow
        Me.ClientSize = New System.Drawing.Size(984, 622)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.LblPaymentDetail)
        Me.Controls.Add(Me.Pnl2)
        Me.Name = "FrmJobDebitCreaditNote"
        Me.Text = "Template Goods Receive"
        Me.Controls.SetChildIndex(Me.TabControl1, 0)
        Me.Controls.SetChildIndex(Me.Pnl2, 0)
        Me.Controls.SetChildIndex(Me.Topctrl1, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.GrpUP, 0)
        Me.Controls.SetChildIndex(Me.GBoxEntryType, 0)
        Me.Controls.SetChildIndex(Me.GBoxApprove, 0)
        Me.Controls.SetChildIndex(Me.GBoxMoveToLog, 0)
        Me.Controls.SetChildIndex(Me.GroupBox2, 0)
        Me.Controls.SetChildIndex(Me.GBoxDivision, 0)
        Me.Controls.SetChildIndex(Me.LblPaymentDetail, 0)
        Me.Controls.SetChildIndex(Me.Panel1, 0)
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
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Protected WithEvents Pnl2 As System.Windows.Forms.Panel
    Protected WithEvents TxtRemarks As AgControls.AgTextBox
    Protected WithEvents Label30 As System.Windows.Forms.Label
    Protected WithEvents LblPaymentDetail As System.Windows.Forms.LinkLabel
    Protected WithEvents Panel1 As System.Windows.Forms.Panel
    Protected WithEvents LblTotalAmount As System.Windows.Forms.Label
    Protected WithEvents LblTotalAmountText As System.Windows.Forms.Label
    Protected WithEvents TxtSubCode As AgControls.AgTextBox
    Protected WithEvents Label1 As System.Windows.Forms.Label
    Protected WithEvents Label3 As System.Windows.Forms.Label
    Protected WithEvents TxtManualRefNo As AgControls.AgTextBox
#End Region

    Private Sub FrmQuality1_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "DuesPayment"
        LogTableName = "DuesPayment_Log"
        MainLineTableCsv = "DuesPaymentDetail"
        LogLineTableCsv = "DuesPaymentDetail_Log"

        AgL.GridDesign(Dgl1)
    End Sub

    Private Sub FrmQuality1_BaseFunction_FIniMast(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMast
        Dim mCondStr$

        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) & _
                        " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "H.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat In ('" & EntryNCat & "')"

        mQry = "Select DocID As SearchCode " &
                " From DuesPayment H " &
                " Left Join Voucher_Type Vt On H.V_Type = Vt.V_Type  " &
                " Where IFNull(IsDeleted,0) = 0 " &
                " " & mCondStr & "  Order By V_Date Desc "
        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub FrmSaleOrder_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mCondStr$

        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) &
                        " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "H.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat In ('" & EntryNCat & "')"

        AgL.PubFindQry = "SELECT H.DocId as SearchCode, Vt.Description AS [Entry_Type], " &
                            " H.V_Date AS [Entry_Date], H.ManualRefNo AS [Entry_No], " &
                            " Sg.Name As Reason_Account, H.Remark  " &
                            " FROM DuesPayment H " &
                            " LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type " &
                            " LEFT JOIN SubGroup Sg On H.SubCode = Sg.SubCode " &
                            " Where 1=1 " & mCondStr

        AgL.PubFindQryOrdBy = "[Entry Date]"
    End Sub

    Private Sub FrmSaleOrder_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl1, Col1CostCenterSubCode, 70, 5, Col1CostCenterSubCode, False, True)
            .AddAgTextColumn(Dgl1, Col1JobOrder, 250, 5, Col1JobOrder, False, False)
            .AddAgTextColumn(Dgl1, Col1SubCode, 250, 5, Col1SubCode, True, False)
            .AddAgNumberColumn(Dgl1, Col1Amount, 100, 8, 2, False, Col1Amount, True, False)
            .AddAgTextColumn(Dgl1, Col1ReferenceDocId, 120, 5, Col1ReferenceDocId, True, False)
            .AddAgTextColumn(Dgl1, Col1BillType, 150, 5, Col1BillType, True, True)
            .AddAgTextColumn(Dgl1, Col1Remark, 180, 255, Col1Remark, True, False)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl2)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 35
        Dgl1.AgSkipReadOnlyColumns = True
        Dgl1.AllowUserToOrderColumns = True

        AgCL.GridSetiingShowXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1, False)
    End Sub

    Private Sub FrmSaleOrder_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As SQLiteConnection, ByVal Cmd As SQLiteCommand) Handles Me.BaseEvent_Save_InTrans
        Dim I As Integer, mSr As Integer
        Dim bSelectionQry$ = ""
        Dim mHeaderAmt As Double = 0
        Dim mLineAmt As Double = 0

        If mTransactionType = EnumTransType.Payment Then
            mHeaderAmt = Val(LblTotalAmount.Text)
        Else
            mHeaderAmt = -Val(LblTotalAmount.Text)
        End If

        mQry = " Update DuesPayment " &
                " SET  " &
                " TransactionType = " & AgL.Chk_Text(mTransactionType) & " , " &
                " ManualRefNo = " & AgL.Chk_Text(TxtManualRefNo.Text) & " , " &
                " SubCode = " & AgL.Chk_Text(TxtSubCode.Tag) & " , " &
                " Process = " & AgL.Chk_Text(TxtProcess.Tag) & " , " &
                " CostCenter = " & AgL.Chk_Text(TxtCostcenter.Tag) & " , " &
                " NetAmount = " & mHeaderAmt & " , " &
                " Remark = " & AgL.Chk_Text(TxtRemarks.Text) & " " &
                " Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mSr = AgL.VNull(AgL.Dman_Execute("Select Max(Sr) From DuesPaymentDetail  Where DocID = '" & mSearchCode & "'", AgL.GcnRead).ExecuteScalar)

        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1SubCode, I).Value <> "" Then
                If Dgl1.Item(ColSNo, I).Tag Is Nothing And Dgl1.Rows(I).Visible = True Then
                    mSr += 1
                    If bSelectionQry <> "" Then bSelectionQry += " UNION ALL "

                    If mTransactionType = EnumTransType.Payment Then
                        mLineAmt = Val(Dgl1.Item(Col1Amount, I).Value)
                    Else
                        mLineAmt = -Val(Dgl1.Item(Col1Amount, I).Value)
                    End If

                    bSelectionQry += " Select " & AgL.Chk_Text(mSearchCode) & ", " &
                            " " & mSr & ", " & AgL.Chk_Text(TxtCostcenter.Tag) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1JobOrder, I).Tag) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1SubCode, I).Tag) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1SubCode, I).Value) & ", " &
                            " " & mLineAmt & ", " &
                            " " & mLineAmt & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1ReferenceDocId, I).Tag) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1Remark, I).Value) & ""
                Else
                    If Dgl1.Rows(I).Visible = True Then

                        If mTransactionType = EnumTransType.Payment Then
                            mLineAmt = Val(Dgl1.Item(Col1Amount, I).Value)
                        Else
                            mLineAmt = -Val(Dgl1.Item(Col1Amount, I).Value)
                        End If

                        mQry = " UPDATE DuesPaymentDetail " &
                                    " SET " &
                                    " CostCenter = " & AgL.Chk_Text(TxtCostcenter.Tag) & ", " &
                                    " WeavingOrderDocID = " & AgL.Chk_Text(Dgl1.Item(Col1JobOrder, I).Tag) & ", " &
                                    " SubCode = " & AgL.Chk_Text(Dgl1.Item(Col1SubCode, I).Tag) & ", " &
                                    " PartyName = " & AgL.Chk_Text(Dgl1.Item(Col1SubCode, I).Value) & ", " &
                                    " Amount = " & mLineAmt & ", " &
                                    " NetAmount = " & mLineAmt & ", " &
                                    " ReferenceDocId = " & AgL.Chk_Text(Dgl1.Item(Col1ReferenceDocId, I).Tag) & ", " &
                                    " Remark = " & AgL.Chk_Text(Dgl1.Item(Col1Remark, I).Value) & " " &
                                    " Where DocId = '" & mSearchCode & "' " &
                                    " And Sr = " & Dgl1.Item(ColSNo, I).Tag & " "
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                    Else
                        mQry = " Delete From DuesPaymentDetail Where DocId = '" & mSearchCode & "' And Sr = " & Val(Dgl1.Item(ColSNo, I).Tag) & "  "
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                    End If
                End If
            End If
        Next

        If bSelectionQry <> "" Then
            mQry = "Insert Into DuesPaymentDetail(DocId, Sr, CostCenter, WeavingOrderDocID, SubCode, PartyName, Amount, " &
                    " NetAmount, ReferenceDocId, Remark) " + bSelectionQry
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If

        Call AccountPosting(Conn, Cmd)

        If AgL.PubUserName.ToUpper = AgLibrary.ClsConstant.PubSuperUserName.ToUpper Then
            AgCL.GridSetiingWriteXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1)
        End If
    End Sub

    Private Sub FrmSaleOrder_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim I As Integer

        Dim DsTemp As DataSet

        mQry = "Select H.*, Sg.Name As ReasonAcName, P.Description  AS ProcessDesc, C.Name As CostCenterName " &
                " From DuesPayment H " &
                " LEFT JOIN SubGroup Sg On H.SubCode = Sg.SubCode " &
                " LEFT JOIN Process P On P.NCat = H.Process " &
                " LEFT JOIN CostCenterMast C On H.CostCenter = C.Code " &
                " Where H.DocID ='" & SearchCode & "'"
        DsTemp = AgL.FillData(mQry, AgL.GCn)
        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                TxtSubCode.Tag = AgL.XNull(.Rows(0)("SubCode"))
                TxtSubCode.Text = AgL.XNull(.Rows(0)("ReasonAcName"))
                TxtManualRefNo.Text = AgL.XNull(.Rows(0)("ManualRefNo"))
                TxtRemarks.Text = AgL.XNull(.Rows(0)("Remark"))
                LblTotalAmount.Text = AgL.VNull(.Rows(0)("PaidAmount"))

                TxtProcess.Tag = AgL.XNull(.Rows(0)("Process"))
                TxtProcess.Text = AgL.XNull(.Rows(0)("ProcessDesc"))
                TxtCostcenter.Tag = AgL.XNull(.Rows(I)("CostCenter"))
                TxtCostcenter.Text = AgL.XNull(.Rows(I)("CostCenterName"))

                'Line Records are showing in Grid
                '-------------------------------------------------------------
                mQry = "Select L.*, C.Name As CostCenterName, " &
                        " Led.V_Type + '-' + Led.RecId As BillNo, Vt.Description As BillType, C.SubCode As CostCenterSubCode, Sg.Name As CostCenterSubCodeName, JO.ManualRefNo as JobOrderNo " &
                        " from DuesPaymentDetail L " &
                        " LEFT JOIN CostCenterMast C On L.CostCenter = C.Code " &
                        " LEFT JOIN JobOrder JO  On L.WeavingOrderDocId = JO.DocID " &
                        " LEFT JOIN (Select Distinct DocId, V_Type, RecId From Ledger) As Led On L.ReferenceDocId = Led.DocId " &
                        " LEFT JOIN Voucher_Type Vt On Led.V_Type = Vt.V_Type " &
                        " LEFT JOIN SubGroup Sg On C.SubCode = Sg.SubCode " &
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

                            Dgl1.Item(Col1JobOrder, I).Tag = AgL.XNull(.Rows(I)("WeavingOrderDocID"))
                            Dgl1.Item(Col1JobOrder, I).Value = AgL.XNull(.Rows(I)("JobOrderNo"))

                            Dgl1.Item(Col1CostCenterSubCode, I).Tag = AgL.XNull(.Rows(I)("CostCenterSubCode"))
                            Dgl1.Item(Col1CostCenterSubCode, I).Value = AgL.XNull(.Rows(I)("CostCenterSubCodeName"))

                            Dgl1.Item(Col1SubCode, I).Tag = AgL.XNull(.Rows(I)("SubCode"))
                            Dgl1.Item(Col1SubCode, I).Value = AgL.XNull(.Rows(I)("PartyName"))

                            Dgl1.Item(Col1ReferenceDocId, I).Tag = AgL.XNull(.Rows(I)("ReferenceDocId"))
                            Dgl1.Item(Col1ReferenceDocId, I).Value = AgL.XNull(.Rows(I)("BillNo"))

                            Dgl1.Item(Col1BillType, I).Value = AgL.XNull(.Rows(I)("BillType"))

                            Dgl1.Item(Col1Amount, I).Value = Math.Abs(AgL.VNull(.Rows(I)("Amount")))
                            Dgl1.Item(Col1Remark, I).Value = AgL.XNull(.Rows(I)("Remark"))
                        Next I
                    End If
                End With
                Calculation()
                '-------------------------------------------------------------
            End If
        End With
    End Sub

    Private Sub FrmSaleOrder_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Topctrl1.ChangeAgGridState(Dgl1, False)
        AgL.WinSetting(Me, 654, 990)
    End Sub

    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub

    Private Sub Dgl1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellEnter
        Try
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1ReferenceDocId
                    If Dgl1.AgHelpDataSet(Col1ReferenceDocId) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1ReferenceDocId) = Nothing

                Case Col1SubCode
                    If Dgl1.Item(Col1CostCenterSubCode, Dgl1.CurrentCell.RowIndex).Tag <> "" Then
                        Dgl1.Item(Col1SubCode, Dgl1.CurrentCell.RowIndex).ReadOnly = True
                    Else
                        Dgl1.Item(Col1SubCode, Dgl1.CurrentCell.RowIndex).ReadOnly = False
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Dgl1_EditingControl_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Dgl1.EditingControl_Validating
        If Topctrl1.Mode = "Browse" Then Exit Sub
        Dim mRowIndex As Integer, mColumnIndex As Integer
        Dim DrTemp As DataRow() = Nothing
        ' Dim DtTemp As DataTable
        Try
            mRowIndex = Dgl1.CurrentCell.RowIndex
            mColumnIndex = Dgl1.CurrentCell.ColumnIndex
            If Dgl1.Item(mColumnIndex, mRowIndex).Value Is Nothing Then Dgl1.Item(mColumnIndex, mRowIndex).Value = ""
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                'Case Col1CostCenter
                '    If Dgl1.Item(Col1CostCenter, mRowIndex).Value = "" Then
                '        Dgl1.Item(Col1CostCenterSubCode, mRowIndex).Tag = ""
                '        Dgl1.Item(Col1CostCenterSubCode, mRowIndex).Value = ""
                '    Else
                '        If Dgl1.AgDataRow IsNot Nothing Then
                '            Dgl1.Item(Col1CostCenterSubCode, mRowIndex).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("SubCode").Value)
                '            Dgl1.Item(Col1CostCenterSubCode, mRowIndex).Value = AgL.XNull(Dgl1.AgDataRow.Cells("PartyName").Value)

                '            Dgl1.Item(Col1SubCode, mRowIndex).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("SubCode").Value)
                '            Dgl1.Item(Col1SubCode, mRowIndex).Value = AgL.XNull(Dgl1.AgDataRow.Cells("PartyName").Value)

                '            DtTemp = AgL.FillData("Select DocID, ManualRefNo From JobOrder  Where CostCenter = '" & Dgl1.Item(Col1CostCenter, mRowIndex).Tag & "'  ", AgL.GCn).Tables(0)
                '            If DtTemp.Rows.Count > 0 Then
                '                Dgl1.Item(Col1JobOrder, mRowIndex).Tag = AgL.XNull(DtTemp.Rows(0)("DocID"))
                '                Dgl1.Item(Col1JobOrder, mRowIndex).Value = AgL.XNull(DtTemp.Rows(0)("ManualRefNo"))
                '            End If
                '        End If
                '    End If
                '    If Dgl1.AgHelpDataSet(Col1ReferenceDocId) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1ReferenceDocId) = Nothing

                Case Col1ReferenceDocId
                    If Dgl1.AgDataRow IsNot Nothing Then
                        Dgl1.Item(Col1BillType, mRowIndex).Value = AgL.XNull(Dgl1.AgDataRow.Cells("BillType").Value)
                    End If

                Case Col1SubCode
                    If Dgl1.AgHelpDataSet(Col1ReferenceDocId) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1ReferenceDocId) = Nothing
            End Select
            Call Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FrmSaleOrder_BaseFunction_Calculation() Handles Me.BaseFunction_Calculation
        Dim I As Integer = 0
        LblTotalAmount.Text = 0
        With Dgl1
            For I = 0 To Dgl1.RowCount - 1
                If .Item(Col1SubCode, I).Value <> "" Then
                    LblTotalAmount.Text = Val(LblTotalAmount.Text) + Val(Dgl1.Item(Col1Amount, I).Value)
                End If
            Next
        End With
        LblTotalAmount.Text = Format(Val(LblTotalAmount.Text), "0.00")
    End Sub

    Private Sub FrmSaleOrder_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        Dim I As Integer = 0, J As Integer = 0
        If AgCL.AgIsBlankGrid(Dgl1, Dgl1.Columns(Col1SubCode).Index) Then passed = False : Exit Sub
        Dim CostCenterStr$ = ""
        Dim DtTemp As DataTable = Nothing
        Dim DrTemp As DataRow() = Nothing

        passed = FCheckDuplicateRefNo()

        With Dgl1
            For I = 0 To .RowCount - 1
                If .Item(Col1SubCode, I).Value <> "" Then
                    If Val(.Item(Col1Amount, I).Value) = 0 Then
                        MsgBox("Amount Is 0 At Row No. " & .Item(ColSNo, I).Value & "", MsgBoxStyle.Information + MsgBoxStyle.Exclamation)
                        Dgl1.CurrentCell = Dgl1.Item(Col1Amount, I) : Dgl1.Focus()
                        passed = False : Exit Sub
                    End If
                End If
            Next I
        End With
    End Sub

    Private Sub FrmSaleOrder_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
        Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
        LblTotalAmount.Text = 0
    End Sub

    Private Sub Dgl1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.KeyDown
        If Topctrl1.Mode <> "Browse" Then
            If Dgl1.CurrentCell IsNot Nothing Then
                If Dgl1.Rows(Dgl1.CurrentCell.RowIndex).DefaultCellStyle.BackColor <> RowLockedColour Then
                    If e.Control And e.KeyCode = Keys.D Then
                        sender.CurrentRow.Selected = True
                        sender.CurrentRow.Visible = False
                    End If
                End If
            End If
            If e.Control Or e.Shift Or e.Alt Then Exit Sub
        End If
    End Sub

    Private Sub DGL1_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles Dgl1.RowsAdded, Dgl1.RowsAdded
        sender(ColSNo, e.RowIndex).Value = e.RowIndex + 1
    End Sub

    Private Sub Dgl1_EditingControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.EditingControl_KeyDown
        Try
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1SubCode
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) Is Nothing Then
                            If TxtProcess.Tag <> "" Then
                                mQry = " SELECT Distinct Sg.SubCode, Sg.Name FROM SubGroup Sg " &
                                        " Left Join JobworkerProcess JP On SG.SubCode = JP.SubCode" &
                                        " Where CharIndex('|' + '" & AgL.PubDivCode & "' + '|', Sg.DivisionList) > 0 " &
                                        " And CharIndex('|' + '" & TxtSite_Code.AgSelectedValue & "' + '|', Sg.SiteList) > 0 " &
                                        " AND IFNull(Sg.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " &
                                        " And JP.Process = '" & TxtProcess.Tag & "' " &
                                        " AND Sg.Nature In ('" & ClsMain.SubGroupNature.Customer & "','" & ClsMain.SubGroupNature.Supplier & "')"
                            Else
                                mQry = " SELECT Distinct Sg.SubCode, Sg.Name FROM SubGroup Sg " &
                                        " Left Join JobworkerProcess JP On SG.SubCode = JP.SubCode" &
                                        " Where CharIndex('|' + '" & AgL.PubDivCode & "' + '|', Sg.DivisionList) > 0 " &
                                        " And CharIndex('|' + '" & TxtSite_Code.AgSelectedValue & "' + '|', Sg.SiteList) > 0 " &
                                        " AND IFNull(Sg.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " &
                                        " AND Sg.Nature In ('" & ClsMain.SubGroupNature.Customer & "','" & ClsMain.SubGroupNature.Supplier & "')"
                            End If
                            Dgl1.AgHelpDataSet(Col1SubCode, 0) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If

                Case Col1ReferenceDocId
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) Is Nothing Then
                            mQry = " SELECT DISTINCT L.DocId,  L.V_Type  + '-' + L.RecId As BillNo, Vt.Description As BillType " &
                                    " FROM Ledger L  " &
                                    " LEFT JOIN Voucher_Type Vt On L.V_Type = Vt.V_Type " &
                                    " WHERE L.SubCode = '" & Dgl1.Item(Col1SubCode, Dgl1.CurrentCell.RowIndex).Tag & "' " &
                                    " And IFNull(L.AmtCr,0) > 0 " &
                                    " And L.DivCode = '" & AgL.PubDivCode & "' AND L.Site_Code = '" & AgL.PubSiteCode & "'"
                            Dgl1.AgHelpDataSet(Col1ReferenceDocId) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FrmWeavingPayment_BaseEvent_ApproveDeletion_InTrans(ByVal SearchCode As String, ByVal Conn As SQLiteConnection, ByVal Cmd As SQLiteCommand) Handles Me.BaseEvent_ApproveDeletion_InTrans
        AgL.LedgerUnPost(Conn, Cmd, mInternalCode)
    End Sub

    Private Function AccountPosting(ByVal Conn As SQLiteConnection, ByVal Cmd As SQLiteCommand) As Boolean
        Dim LedgAry() As AgLibrary.ClsMain.LedgRec
        Dim I As Integer, mSr As Integer = 0
        Dim DsTemp As DataSet = Nothing
        Dim mNarr As String = "", mCommonNarr$ = ""
        Dim mNetAmount As Double, mRoundOff As Double = 0
        Dim mDebitCreditNoteAc$ = ""

        Dim AmtDr As Double = 0, AmtCr As Double = 0

        Dim GcnRead As SQLiteConnection
        GcnRead = New SQLiteConnection
        GcnRead.ConnectionString = AgL.Gcn_ConnectionString
        GcnRead.Open()

        mNetAmount = 0
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
                " " & AgL.Chk_Text(TxtV_Date.Text) & ",	" & AgL.Chk_Text(bBankAc) & ",	" &
                " " & AgL.Chk_Text(TxtRemarks.Text) & ",	" & AgL.Chk_Text(AgL.PubUserName) & ",	" &
                " " & AgL.Chk_Text(AgL.PubLoginDate) & ", " & AgL.Chk_Text(AgL.MidStr(Topctrl1.Mode, 0, 1)) & ") "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        With Dgl1
            For I = 0 To .Rows.Count - 1
                If .Item(Col1SubCode, I).Value <> "" Then
                    mSr += 1

                    If mTransactionType = EnumTransType.Receipt Then
                        AmtDr = 0
                        AmtCr = Val(.Item(Col1Amount, I).Value)
                        mDebitCreditNoteAc = AgL.XNull(DtDuesPaymentEnviro.Rows(0)("CreditNoteAc"))
                    Else
                        AmtDr = Val(.Item(Col1Amount, I).Value)
                        AmtCr = 0
                        mDebitCreditNoteAc = AgL.XNull(DtDuesPaymentEnviro.Rows(0)("DebitNoteAc"))
                    End If

                    mQry = " INSERT INTO Ledger(DocId, V_SNo, V_No, V_Type, RecId, V_Prefix, V_Date, SubCode, ContraSub, " &
                                " AmtDr, AmtCr, Narration,	Site_Code,U_Name,	U_EntDt,	U_AE,	" &
                                " DivCode, CostCenter, JobOrder) " &
                                " VALUES ('" & mInternalCode & "', " & Val(mSr) & ", " & Val(TxtV_No.Text) & ",	" &
                                " " & AgL.Chk_Text(TxtV_Type.AgSelectedValue) & ",	" & AgL.Chk_Text(TxtManualRefNo.Text) & ", " & AgL.Chk_Text(LblPrefix.Text) & ", " &
                                " " & AgL.Chk_Text(TxtV_Date.Text) & ",	" & AgL.Chk_Text(.Item(Col1SubCode, I).Tag) & ", " &
                                " " & AgL.Chk_Text(mDebitCreditNoteAc) & ",	" & AmtDr & ", " & AmtCr & ", " &
                                " " & AgL.Chk_Text(.Item(Col1Remark, I).Value) & ",	" & AgL.Chk_Text(AgL.PubSiteCode) & ", " &
                                " '" & AgL.PubUserName & "', '" & AgL.PubLoginDate & "', " &
                                " " & AgL.Chk_Text(AgL.MidStr(Topctrl1.Mode, 0, 1)) & ", " &
                                " '" & AgL.PubDivCode & "', " &
                                " " & AgL.Chk_Text(TxtCostcenter.Tag) & ", " &
                                " " & AgL.Chk_Text(.Item(Col1JobOrder, I).Tag) & " " &
                                " ) "
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                    mSr += 1

                    If mTransactionType = EnumTransType.Receipt Then
                        AmtDr = Val(.Item(Col1Amount, I).Value)
                        AmtCr = 0
                        mDebitCreditNoteAc = AgL.XNull(DtDuesPaymentEnviro.Rows(0)("CreditNoteAc"))
                    Else
                        AmtDr = 0
                        AmtCr = Val(.Item(Col1Amount, I).Value)
                        mDebitCreditNoteAc = AgL.XNull(DtDuesPaymentEnviro.Rows(0)("DebitNoteAc"))
                    End If

                    mQry = " INSERT INTO Ledger(DocId, V_SNo, V_No, V_Type, RecId, V_Prefix, V_Date, SubCode, ContraSub, " &
                            " AmtDr, AmtCr, Narration,	Site_Code, U_Name,	U_EntDt, U_AE, DivCode) " &
                            " VALUES ('" & mInternalCode & "', " & Val(mSr) & ", " & Val(TxtV_No.Text) & ",	" &
                            " " & AgL.Chk_Text(TxtV_Type.AgSelectedValue) & ",	" & AgL.Chk_Text(TxtManualRefNo.Text) & ", " & AgL.Chk_Text(LblPrefix.Text) & ", " &
                            " " & AgL.Chk_Text(TxtV_Date.Text) & ",	" & AgL.Chk_Text(mDebitCreditNoteAc) & ", " &
                            " " & AgL.Chk_Text(.Item(Col1SubCode, I).Tag) & ", " &
                            " " & AmtDr & ", " & AmtCr & ", " & AgL.Chk_Text(TxtRemarks.Text) & ",	" & AgL.Chk_Text(AgL.PubSiteCode) & ", " &
                            " '" & AgL.PubUserName & "', '" & AgL.PubLoginDate & "', " &
                            " " & AgL.Chk_Text(AgL.MidStr(Topctrl1.Mode, 0, 1)) & ", " &
                            " '" & AgL.PubDivCode & "') "
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                    'mQry = " INSERT INTO LedgerAdj(Vr_DocId, Vr_V_SNo, Adj_DocID, Adj_V_SNo, Amount, Site_Code) " & _
                    '        " VALUES(@vr_docid, @vr_v_sno, @adj_docid, @adj_v_sno, @amount, '" & AgL.PubSiteCode & "')"
                    'AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                End If
            Next
        End With
    End Function

    Private Sub FrmWeavingPayment120213_BaseEvent_Topctrl_tbRef() Handles Me.BaseEvent_Topctrl_tbRef
        If TxtSubCode.AgHelpDataSet IsNot Nothing Then TxtSubCode.AgHelpDataSet = Nothing
        If Dgl1.AgHelpDataSet(Col1SubCode) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1SubCode) = Nothing
        If TxtProcess.AgHelpDataSet IsNot Nothing Then TxtProcess.AgHelpDataSet = Nothing
        If Dgl1.AgHelpDataSet(Col1ReferenceDocId) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1ReferenceDocId) = Nothing
    End Sub

    Private Sub TxtCashBankAc_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtSubCode.KeyDown, TxtProcess.KeyDown
        Try
            Select Case sender.Name
                Case TxtSubCode.Name
                    If e.KeyCode <> Keys.Enter Then
                        If TxtSubCode.AgHelpDataSet Is Nothing Then
                            FCreateHelpSubgroup()
                        End If
                    End If

                Case TxtProcess.Name
                    If e.KeyCode <> Keys.Enter Then
                        If sender.AgHelpDataSet Is Nothing Then
                            mQry = " SELECT H.NCat AS Code, H.Description AS Process, H.CostCenter, C.Name AS CostCenterName " &
                                    " FROM Process H " &
                                    " LEFT JOIN CostCenterMast C ON C.Code = H.CostCenter "
                            sender.AgHelpDataSet(2, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If

            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Txt_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtProcess.Validating
        Dim DrTemp As DataRow() = Nothing
        Dim DtTemp As DataTable = Nothing
        Dim I As Integer = 0
        Try
            Select Case sender.name
                Case TxtProcess.Name
                    If sender.AgHelpDataSet IsNot Nothing Then
                        If TxtProcess.AgSelectedValue <> "" Then
                            DrTemp = sender.AgHelpDataSet.Tables(0).Select("Code = " & AgL.Chk_Text(sender.AgSelectedValue) & "")
                            TxtCostcenter.Text = AgL.XNull(DrTemp(0)("CostCenterName"))
                            TxtCostcenter.Tag = AgL.XNull(DrTemp(0)("CostCenter"))
                        End If
                    End If

            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FCreateHelpSubgroup()
        Dim strCond As String = ""
        If DtV_TypeSettings.Rows.Count > 0 Then
            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_AcGroup")) <> "" Then
                strCond += " And CharIndex('|' + H.GroupCode + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_AcGroup")) & "') > 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_SubGroup")) <> "" Then
                strCond += " And CharIndex('|' + H.SubCode + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_SubGroup")) & "') > 0 "
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

        mQry = " SELECT H.SubCode AS Code, H.Name AS JobWorker, City.CityName as City_Name " &
                " FROM Subgroup H   " &
                " Left Join City On H.CityCode = City.CityCode" &
                " Where IFNull(H.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') ='" & AgTemplate.ClsMain.EntryStatus.Active & "'  " &
                " And H.Nature = 'Others'" & strCond
        TxtSubCode.AgHelpDataSet(0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
    End Sub

    Private Sub FrmDebitCreaditNote_BaseEvent_Topctrl_tbAdd() Handles Me.BaseEvent_Topctrl_tbAdd
        TxtManualRefNo.Text = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "DuesPayment", TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, AgTemplate.ClsMain.ManualRefType.Max)
    End Sub

    Private Function FCheckDuplicateRefNo() As Boolean
        FCheckDuplicateRefNo = True
        If Topctrl1.Mode = "Add" Then
            mQry = " SELECT COUNT(*) FROM JobIssRec WHERE ManualRefNo = '" & TxtManualRefNo.Text & "'   " &
                    " AND V_Type ='" & TxtV_Type.AgSelectedValue & "'  And Div_Code = '" & TxtDivision.AgSelectedValue & "' And Site_Code = '" & TxtSite_Code.AgSelectedValue & "' And IFNull(IsDeleted,0) = 0  "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then FCheckDuplicateRefNo = False : MsgBox("Reference No. Already Exists") : TxtManualRefNo.Focus()
        Else
            mQry = " SELECT COUNT(*) FROM JobIssRec WHERE ManualRefNo = '" & TxtManualRefNo.Text & "'  " &
                    " AND V_Type ='" & TxtV_Type.AgSelectedValue & "'  And Div_Code = '" & TxtDivision.AgSelectedValue & "' And Site_Code = '" & TxtSite_Code.AgSelectedValue & "' And IFNull(IsDeleted,0) = 0 AND DocID <>'" & mInternalCode & "'  "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then FCheckDuplicateRefNo = False : MsgBox("Reference No. Already Exists") : TxtManualRefNo.Focus()
        End If
    End Function

    Private Sub FrmDebitCreaditNote_BaseEvent_Topctrl_tbPrn(ByVal SearchCode As String) Handles Me.BaseEvent_Topctrl_tbPrn
        mQry = "SELECT H.DocID, H.V_Type, H.V_Prefix, H.V_Date, H.ManualRefNo, H.V_No, H.SubCode,  H.Remark, Sg.Name As ReasonAcName, " &
                " L.Sr, L.ReferenceDocID, L.Reference_Sr, abs(L.Amount) AS Amount, L.PaidAmount, CM.Name AS CostCenter, SCM.DispName AS CostCenterName, SCM.Add1, SCM.Add2, C.CityName, " &
                " Led.V_Type + '-' + Led.RecId As BillNo, Led.V_Date AS BillDate, Vt.Description As BillType, L.Remark AS LineRemark, '" & TxtV_Type.Text & "' + ' No' AS EntryNoHead,  " &
                " PI.Currency, PI.VendorDocNo AS PartyDocNo, PI.VendorDocDate AS PartyDocDate " &
                " FROM ( SELECT * FROM DuesPayment WHERE DocID = '" & mSearchCode & "' ) AS H  " &
                " LEFT JOIN DuesPaymentDetail L ON L.DocID = H.DocID  " &
                " LEFT JOIN SubGroup Sg On H.SubCode = Sg.SubCode   " &
                " LEFT JOIN CostCenterMast CM ON CM.Code = L.CostCenter  " &
                " LEFT JOIN SubGroup SCM ON SCM.SubCode =  L.Subcode  " &
                " LEFT JOIN City C ON C.CityCode = SCM.CityCode " &
                " LEFT JOIN (Select Distinct DocId, V_Date, V_Type, RecId From Ledger  ) As Led On L.ReferenceDocId = Led.DocId  " &
                " LEFT JOIN PurchInvoice PI ON PI.DocId = L.ReferenceDocId " &
                " LEFT JOIN Voucher_Type Vt On Led.V_Type = Vt.V_Type "
        ClsMain.FPrintThisDocument(Me, TxtV_Type.Tag, mQry, "Production_DebitCreditNote_Print", TxtV_Type.Text)
    End Sub

    Private Sub FrmFinishingDebitCreaditNote_BaseFunction_DispText() Handles Me.BaseFunction_DispText
        TxtCostcenter.Enabled = False
    End Sub
End Class
