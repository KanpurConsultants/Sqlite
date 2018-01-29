Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data.SQLite
Public Class FrmJobDebitCreditNote
    Inherits AgTemplate.TempTransaction
    Dim mQry$

    Dim DtDuesPaymentEnviro As DataTable = Nothing

    Protected Const ColSNo As String = "S.No."
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    'Protected Const Col1JobOrder As String = "Order No"
    Protected Const Col1BillingType As String = "BillingType"
    Protected Const Col1SubCode As String = "Party"
    Protected Const Col1Qty As String = "Qty"
    Protected Const Col1Measure As String = "Measure"
    Protected Const Col1Rate As String = "Rate"
    Protected Const Col1Amount As String = "Amount"
    Protected Const Col1AdjustedAmount As String = "Adjusted Amount"
    Protected Const Col1ReferenceDocId As String = "Bill No"
    Protected Const Col1BillType As String = "Bill Type"
    Protected Const Col1Remark As String = "Narration"

    Dim bBankAc$ = "", bCashAc$ = ""
    Dim mTransactionType As EnumTransType = EnumTransType.Payment
    Dim mEntryType As EnumEntryType
    Protected WithEvents BtnFillOrderNo As System.Windows.Forms.Button
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
    Enum EnumEntryType
        DebitAndCreditNote = 1
        TimeIncentiveAndPenalty = 2
    End Enum

    Public Property TransactionType() As EnumTransType
        Get
            TransactionType = mTransactionType
        End Get
        Set(ByVal value As EnumTransType)
            mTransactionType = value
        End Set
    End Property

    Public Property EntryType() As EnumEntryType
        Get
            EntryType = mEntryType
        End Get
        Set(ByVal value As EnumEntryType)
            mEntryType = value
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
        Me.BtnFillOrderNo = New System.Windows.Forms.Button
        Me.TxtCostcenter = New AgControls.AgTextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.TxtProcess = New AgControls.AgTextBox
        Me.Label5 = New System.Windows.Forms.Label
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
        Me.LblV_No.Location = New System.Drawing.Point(455, 34)
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
        Me.Label2.Location = New System.Drawing.Point(330, 39)
        Me.Label2.Tag = ""
        '
        'LblV_Date
        '
        Me.LblV_Date.BackColor = System.Drawing.Color.Transparent
        Me.LblV_Date.Location = New System.Drawing.Point(232, 34)
        Me.LblV_Date.Tag = ""
        '
        'LblV_TypeReq
        '
        Me.LblV_TypeReq.Location = New System.Drawing.Point(545, 19)
        Me.LblV_TypeReq.Tag = ""
        '
        'TxtV_Date
        '
        Me.TxtV_Date.AgSelectedValue = ""
        Me.TxtV_Date.BackColor = System.Drawing.Color.White
        Me.TxtV_Date.Location = New System.Drawing.Point(347, 33)
        Me.TxtV_Date.TabIndex = 2
        Me.TxtV_Date.Tag = ""
        '
        'LblV_Type
        '
        Me.LblV_Type.Location = New System.Drawing.Point(455, 15)
        Me.LblV_Type.Tag = ""
        '
        'TxtV_Type
        '
        Me.TxtV_Type.AgSelectedValue = ""
        Me.TxtV_Type.BackColor = System.Drawing.Color.White
        Me.TxtV_Type.Location = New System.Drawing.Point(561, 13)
        Me.TxtV_Type.Size = New System.Drawing.Size(190, 18)
        Me.TxtV_Type.TabIndex = 1
        Me.TxtV_Type.Tag = ""
        '
        'LblSite_CodeReq
        '
        Me.LblSite_CodeReq.Location = New System.Drawing.Point(330, 19)
        Me.LblSite_CodeReq.Tag = ""
        '
        'LblSite_Code
        '
        Me.LblSite_Code.BackColor = System.Drawing.Color.Transparent
        Me.LblSite_Code.Location = New System.Drawing.Point(232, 14)
        Me.LblSite_Code.Size = New System.Drawing.Size(87, 16)
        Me.LblSite_Code.Tag = ""
        Me.LblSite_Code.Text = "Branch Name"
        '
        'TxtSite_Code
        '
        Me.TxtSite_Code.AgSelectedValue = ""
        Me.TxtSite_Code.BackColor = System.Drawing.Color.White
        Me.TxtSite_Code.Location = New System.Drawing.Point(347, 13)
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
        Me.TabControl1.Size = New System.Drawing.Size(991, 149)
        Me.TabControl1.TabIndex = 0
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
        Me.TP1.Size = New System.Drawing.Size(983, 123)
        Me.TP1.Text = "Document Detail"
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
        '
        'Topctrl1
        '
        Me.Topctrl1.Size = New System.Drawing.Size(984, 41)
        Me.Topctrl1.TabIndex = 2
        '
        'Pnl2
        '
        Me.Pnl2.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Pnl2.Location = New System.Drawing.Point(7, 193)
        Me.Pnl2.Name = "Pnl2"
        Me.Pnl2.Size = New System.Drawing.Size(969, 345)
        Me.Pnl2.TabIndex = 1
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
        Me.TxtRemarks.Location = New System.Drawing.Point(347, 93)
        Me.TxtRemarks.MaxLength = 255
        Me.TxtRemarks.Name = "TxtRemarks"
        Me.TxtRemarks.Size = New System.Drawing.Size(404, 18)
        Me.TxtRemarks.TabIndex = 7
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(232, 94)
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
        Me.LblPaymentDetail.Location = New System.Drawing.Point(7, 170)
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
        Me.TxtSubCode.Location = New System.Drawing.Point(347, 53)
        Me.TxtSubCode.MaxLength = 255
        Me.TxtSubCode.Name = "TxtSubCode"
        Me.TxtSubCode.Size = New System.Drawing.Size(404, 18)
        Me.TxtSubCode.TabIndex = 4
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(232, 54)
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
        Me.Label3.Location = New System.Drawing.Point(330, 60)
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
        Me.TxtManualRefNo.Location = New System.Drawing.Point(561, 33)
        Me.TxtManualRefNo.MaxLength = 50
        Me.TxtManualRefNo.Name = "TxtManualRefNo"
        Me.TxtManualRefNo.Size = New System.Drawing.Size(190, 18)
        Me.TxtManualRefNo.TabIndex = 3
        '
        'BtnFillOrderNo
        '
        Me.BtnFillOrderNo.BackColor = System.Drawing.Color.Transparent
        Me.BtnFillOrderNo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnFillOrderNo.Font = New System.Drawing.Font("Lucida Console", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnFillOrderNo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnFillOrderNo.Location = New System.Drawing.Point(138, 171)
        Me.BtnFillOrderNo.Margin = New System.Windows.Forms.Padding(0)
        Me.BtnFillOrderNo.Name = "BtnFillOrderNo"
        Me.BtnFillOrderNo.Size = New System.Drawing.Size(38, 20)
        Me.BtnFillOrderNo.TabIndex = 752
        Me.BtnFillOrderNo.Text = "..."
        Me.BtnFillOrderNo.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnFillOrderNo.UseVisualStyleBackColor = False
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
        Me.TxtCostcenter.Location = New System.Drawing.Point(561, 73)
        Me.TxtCostcenter.MaxLength = 20
        Me.TxtCostcenter.Name = "TxtCostcenter"
        Me.TxtCostcenter.Size = New System.Drawing.Size(190, 18)
        Me.TxtCostcenter.TabIndex = 6
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(455, 74)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(77, 16)
        Me.Label6.TabIndex = 757
        Me.Label6.Text = "Cost Center"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(329, 80)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(10, 7)
        Me.Label4.TabIndex = 756
        Me.Label4.Text = "Ä"
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
        Me.TxtProcess.Location = New System.Drawing.Point(347, 73)
        Me.TxtProcess.MaxLength = 20
        Me.TxtProcess.Name = "TxtProcess"
        Me.TxtProcess.Size = New System.Drawing.Size(100, 18)
        Me.TxtProcess.TabIndex = 5
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(232, 74)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(56, 16)
        Me.Label5.TabIndex = 755
        Me.Label5.Text = "Process"
        '
        'FrmJobIncentivePenalty
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.BackColor = System.Drawing.SystemColors.ButtonShadow
        Me.ClientSize = New System.Drawing.Size(984, 622)
        Me.Controls.Add(Me.BtnFillOrderNo)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.LblPaymentDetail)
        Me.Controls.Add(Me.Pnl2)
        Me.Name = "FrmJobIncentivePenalty"
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
        Me.Controls.SetChildIndex(Me.BtnFillOrderNo, 0)
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
                            " Sg.Name As Reason_Account, H.EntryBy, H.EntryDate, P.Description AS Process ,H.Remark  " &
                            " FROM DuesPayment H " &
                            " LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type " &
                            " LEFT JOIN process P ON P.NCat = H.Process " &
                            " LEFT JOIN SubGroup Sg On H.SubCode = Sg.SubCode " &
                            " Where 1=1 " & mCondStr

        AgL.PubFindQryOrdBy = "[Entry Date]"
    End Sub

    Private Sub FrmSaleOrder_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl1, Col1SubCode, 250, 5, Col1SubCode, True, False)
            '.AddAgTextColumn(Dgl1, Col1JobOrder, 70, 5, Col1JobOrder, True, False)
            .AddAgTextColumn(Dgl1, Col1BillingType, 100, 5, Col1BillingType, False, True)
            .AddAgNumberColumn(Dgl1, Col1Qty, 100, 8, 4, False, Col1Qty, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Qty")), Boolean), True)
            .AddAgNumberColumn(Dgl1, Col1Measure, 100, 8, 4, False, Col1Measure, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Measure")), Boolean), True)
            .AddAgNumberColumn(Dgl1, Col1Rate, 50, 8, 2, False, Col1Rate, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Rate")), Boolean), True)
            .AddAgNumberColumn(Dgl1, Col1Amount, 100, 8, 2, False, Col1Amount, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Amount")), Boolean), False)
            .AddAgNumberColumn(Dgl1, Col1AdjustedAmount, 100, 8, 2, False, Col1AdjustedAmount, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Amount")), Boolean), True)
            .AddAgTextColumn(Dgl1, Col1ReferenceDocId, 100, 5, Col1ReferenceDocId, True, False)
            .AddAgTextColumn(Dgl1, Col1BillType, 100, 5, Col1BillType, True, True)
            .AddAgTextColumn(Dgl1, Col1Remark, 150, 255, Col1Remark, True, False)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl2)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 35
        Dgl1.AgSkipReadOnlyColumns = True
        Dgl1.AllowUserToOrderColumns = True

        If mEntryType = EnumEntryType.DebitAndCreditNote Then
            'Dgl1.Columns(Col1JobOrder).Visible = False
            Dgl1.Columns(Col1AdjustedAmount).Visible = False
        ElseIf mEntryType = EnumEntryType.TimeIncentiveAndPenalty Then
            Dgl1.Columns(Col1BillType).Visible = False
            'Dgl1.Columns(Col1ReferenceDocId).Visible = False
        End If

        AgCL.GridSetiingShowXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1, False)
    End Sub

    Private Sub FrmSaleOrder_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As SQLiteConnection, ByVal Cmd As SQLiteCommand) Handles Me.BaseEvent_Save_InTrans
        Dim I As Integer, mSr As Integer
        Dim bSelectionQry$ = ""
        Dim mHeaderAmt As Double = 0
        Dim mLineAmt As Double = 0
        Dim mLineAdjustedAmt As Double = 0
        Dim mLineQty As Double = 0
        Dim mLineTotalMeasure As Double = 0

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
                        mLineAdjustedAmt = Val(Dgl1.Item(Col1AdjustedAmount, I).Value)
                        mLineAmt = Val(Dgl1.Item(Col1Amount, I).Value)
                        mLineQty = Val(Dgl1.Item(Col1Qty, I).Value)
                        mLineTotalMeasure = Val(Dgl1.Item(Col1Measure, I).Value)
                    Else
                        mLineAdjustedAmt = -Val(Dgl1.Item(Col1AdjustedAmount, I).Value)
                        mLineAmt = -Val(Dgl1.Item(Col1Amount, I).Value)
                        mLineQty = -Val(Dgl1.Item(Col1Qty, I).Value)
                        mLineTotalMeasure = -Val(Dgl1.Item(Col1Measure, I).Value)
                    End If

                    bSelectionQry += " Select " & AgL.Chk_Text(mSearchCode) & ", " &
                            " " & mSr & ", " & AgL.Chk_Text(TxtCostcenter.Tag) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1SubCode, I).Tag) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1SubCode, I).Value) & ", " &
                            " " & mLineQty & ", " & mLineTotalMeasure & ", " &
                            " " & Val(Dgl1.Item(Col1Rate, I).Value) & ", " &
                            " " & mLineAdjustedAmt & ", " &
                            " " & mLineAmt & ", " &
                            " " & mLineAmt & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1ReferenceDocId, I).Tag) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1Remark, I).Value) & ""
                Else
                    If Dgl1.Rows(I).Visible = True Then

                        If mTransactionType = EnumTransType.Payment Then
                            mLineAdjustedAmt = Val(Dgl1.Item(Col1AdjustedAmount, I).Value)
                            mLineAmt = Val(Dgl1.Item(Col1Amount, I).Value)
                            mLineQty = Val(Dgl1.Item(Col1Qty, I).Value)
                            mLineTotalMeasure = Val(Dgl1.Item(Col1Measure, I).Value)
                        Else
                            mLineAdjustedAmt = -Val(Dgl1.Item(Col1AdjustedAmount, I).Value)
                            mLineAmt = -Val(Dgl1.Item(Col1Amount, I).Value)
                            mLineQty = -Val(Dgl1.Item(Col1Qty, I).Value)
                            mLineTotalMeasure = -Val(Dgl1.Item(Col1Measure, I).Value)
                        End If

                        mQry = " UPDATE DuesPaymentDetail " &
                                    " SET " &
                                    " CostCenter = " & AgL.Chk_Text(TxtCostcenter.Tag) & ", " &
                                    " SubCode = " & AgL.Chk_Text(Dgl1.Item(Col1SubCode, I).Tag) & ", " &
                                    " PartyName = " & AgL.Chk_Text(Dgl1.Item(Col1SubCode, I).Value) & ", " &
                                    " Qty = " & mLineQty & ", " &
                                    " Rate = " & Val(Dgl1.Item(Col1Rate, I).Value) & ", " &
                                    " AdjustedAmount = " & mLineAdjustedAmt & ", " &
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
            mQry = "Insert Into DuesPaymentDetail(DocId, Sr, CostCenter, SubCode, PartyName, Qty, TotalMeasure, Rate, AdjustedAmount, Amount, " &
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

        mQry = "Select H.*, Sg.Name As ReasonAcName , P.Description  AS ProcessDesc, C.Name As CostCenterName " &
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
                mQry = "Select L.*, JO.BillingType, " &
                        " IFNull(JO.ManualRefNo,Led.V_Type + '-' + Led.RecId) As BillNo, Vt.Description As BillType " &
                        " from DuesPaymentDetail L " &
                        " LEFT JOIN JobOrder JO  On L.ReferenceDocId = JO.DocID " &
                        " LEFT JOIN (Select Distinct DocId, V_Type, RecId From Ledger) As Led On L.ReferenceDocId = Led.DocId " &
                        " LEFT JOIN Voucher_Type Vt On Led.V_Type = Vt.V_Type " &
                        " LEFT JOIN SubGroup Sg On JO.JobWorker = Sg.SubCode " &
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


                            'Dgl1.Item(Col1JobOrder, I).Tag = AgL.XNull(.Rows(I)("ReferenceDocId"))
                            'Dgl1.Item(Col1JobOrder, I).Value = AgL.XNull(.Rows(I)("JobOrderNo"))



                            Dgl1.Item(Col1SubCode, I).Tag = AgL.XNull(.Rows(I)("SubCode"))
                            Dgl1.Item(Col1SubCode, I).Value = AgL.XNull(.Rows(I)("PartyName"))

                            Dgl1.Item(Col1ReferenceDocId, I).Tag = AgL.XNull(.Rows(I)("ReferenceDocId"))
                            Dgl1.Item(Col1ReferenceDocId, I).Value = AgL.XNull(.Rows(I)("BillNo"))

                            Dgl1.Item(Col1BillType, I).Value = AgL.XNull(.Rows(I)("BillType"))
                            Dgl1.Item(Col1BillingType, I).Value = AgL.XNull(.Rows(I)("BillingType"))
                            Dgl1.Item(Col1Qty, I).Value = Math.Abs(AgL.VNull(.Rows(I)("Qty")))
                            Dgl1.Item(Col1Measure, I).Value = Math.Abs(AgL.VNull(.Rows(I)("TotalMeasure")))
                            Dgl1.Item(Col1Rate, I).Value = AgL.VNull(.Rows(I)("Rate"))
                            Dgl1.Item(Col1Amount, I).Value = Math.Abs(AgL.VNull(.Rows(I)("Amount")))
                            Dgl1.Item(Col1AdjustedAmount, I).Value = Math.Abs(AgL.VNull(.Rows(I)("AdjustedAmount")))
                            Dgl1.Item(Col1Remark, I).Value = AgL.XNull(.Rows(I)("Remark"))

                            If Dgl1.Item(Col1Amount, I).Value <> Dgl1.Item(Col1AdjustedAmount, I).Value And mEntryType = EnumEntryType.TimeIncentiveAndPenalty Then
                                Dgl1.Rows(I).DefaultCellStyle.ForeColor = Color.Red
                            End If

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

                    'Case Col1SubCode
                    '    If Dgl1.Item(Col1CostCenterSubCode, Dgl1.CurrentCell.RowIndex).Tag <> "" Then
                    '        Dgl1.Item(Col1SubCode, Dgl1.CurrentCell.RowIndex).ReadOnly = True
                    '    Else
                    '        Dgl1.Item(Col1SubCode, Dgl1.CurrentCell.RowIndex).ReadOnly = False
                    '    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Dgl1_EditingControl_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Dgl1.EditingControl_Validating
        If Topctrl1.Mode = "Browse" Then Exit Sub
        Dim I As Integer, mColumnIndex As Integer
        Dim DrTemp As DataRow() = Nothing
        Try
            I = Dgl1.CurrentCell.RowIndex
            mColumnIndex = Dgl1.CurrentCell.ColumnIndex
            If Dgl1.Item(mColumnIndex, I).Value Is Nothing Then Dgl1.Item(mColumnIndex, I).Value = ""
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1ReferenceDocId
                    If mEntryType = EnumEntryType.TimeIncentiveAndPenalty Then
                        If Dgl1.Item(Col1ReferenceDocId, I).Value = "" Then
                            Dgl1.Item(Col1Qty, I).Value = ""
                            Dgl1.Item(Col1Rate, I).Value = ""
                        Else
                            If Dgl1.AgDataRow IsNot Nothing Then
                                Dgl1.Item(Col1SubCode, I).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("SubCode").Value)
                                Dgl1.Item(Col1SubCode, I).Value = AgL.XNull(Dgl1.AgDataRow.Cells("PartyName").Value)
                                Dgl1.Item(Col1Qty, I).Value = AgL.VNull(Dgl1.AgDataRow.Cells("RecQty").Value)
                                Dgl1.Item(Col1Measure, I).Value = AgL.VNull(Dgl1.AgDataRow.Cells("RecMeasure").Value)
                                Dgl1.Item(Col1Rate, I).Value = AgL.VNull(Dgl1.AgDataRow.Cells("Rate").Value)

                                Dgl1.Item(Col1ReferenceDocId, I).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("JobOrder").Value)
                                Dgl1.Item(Col1ReferenceDocId, I).Value = AgL.XNull(Dgl1.AgDataRow.Cells("JobOrderNo").Value)
                            End If
                        End If

                    ElseIf mEntryType = EnumEntryType.DebitAndCreditNote Then
                        Dgl1.Item(Col1BillType, I).Value = AgL.XNull(Dgl1.AgDataRow.Cells("BillType").Value)
                    End If


                Case Col1SubCode
                    If Dgl1.AgHelpDataSet(Col1ReferenceDocId) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1ReferenceDocId) = Nothing
            End Select
            Call Calculation()
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

    Private Sub FrmSaleOrder_BaseFunction_Calculation() Handles Me.BaseFunction_Calculation
        Dim I As Integer = 0
        LblTotalAmount.Text = 0
        With Dgl1
            For I = 0 To Dgl1.RowCount - 1
                If .Item(Col1SubCode, I).Value <> "" Then
                    If mEntryType = EnumEntryType.DebitAndCreditNote Then
                        If Val(Dgl1.Item(Col1Rate, I).Value) > 0 Then
                            If Dgl1.Item(Col1BillingType, I).Value <> "Qty" Then
                                Dgl1.Item(Col1Amount, I).Value = Math.Round(Val(Dgl1.Item(Col1Measure, I).Value) * Val(Dgl1.Item(Col1Rate, I).Value), 0)
                                Dgl1.Item(Col1AdjustedAmount, I).Value = Math.Round(Val(Dgl1.Item(Col1Measure, I).Value) * Val(Dgl1.Item(Col1Rate, I).Value), 0)
                            Else
                                Dgl1.Item(Col1Amount, I).Value = Math.Round(Val(Dgl1.Item(Col1Qty, I).Value) * Val(Dgl1.Item(Col1Rate, I).Value), 0)
                                Dgl1.Item(Col1AdjustedAmount, I).Value = Math.Round(Val(Dgl1.Item(Col1Qty, I).Value) * Val(Dgl1.Item(Col1Rate, I).Value), 0)
                            End If
                        End If
                    End If
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

        'If mTransactionType = EnumTransType.Payment Then
        '    If TxtV_Date.Text <> AgL.RetMonthEndDate(TxtV_Date.Text) Then
        '        MsgBox("Entry Date Should be last Date of Month !")
        '        TxtV_Date.Focus()
        '        passed = False : Exit Sub
        '    End If
        '    mQry = "SELECT count(*) AS Cnt FROM DuesPayment H WHERE H.V_Type = 'WTPNL' AND H.Site_Code ='" & AgL.PubSiteCode & "' AND H.Div_Code ='" & AgL.PubDivCode & "' AND H.V_Date BETWEEN '" & AgL.RetMonthStartDate(TxtV_Date.Text) & "' AND '" & AgL.RetMonthEndDate(TxtV_Date.Text) & "' AND  H.DocId <> '" & mInternalCode & "'"
        '    If AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar()) > 0 Then
        '        MsgBox("Time Penalty is already given for this month !")
        '        TxtV_Date.Focus()
        '        passed = False : Exit Sub
        '    End If
        'End If

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
                            If mEntryType = EnumEntryType.DebitAndCreditNote Then
                                mQry = " SELECT DISTINCT L.DocId,  L.V_Type  + '-' + L.RecId As BillNo, Vt.Description As BillType " &
                                        " FROM Ledger L  " &
                                        " LEFT JOIN Voucher_Type Vt On L.V_Type = Vt.V_Type " &
                                        " WHERE L.SubCode = '" & Dgl1.Item(Col1SubCode, Dgl1.CurrentCell.RowIndex).Tag & "' " &
                                        " And IFNull(L.AmtCr,0) > 0 " &
                                        " And L.DivCode = '" & AgL.PubDivCode & "' AND L.Site_Code = '" & AgL.PubSiteCode & "'"
                                Dgl1.AgHelpDataSet(Col1ReferenceDocId) = AgL.FillData(mQry, AgL.GCn)
                            ElseIf mEntryType = EnumEntryType.TimeIncentiveAndPenalty Then
                                mQry = FFillOrderNo("")
                                Dgl1.AgHelpDataSet(Col1ReferenceDocId, 6) = AgL.FillData(mQry, AgL.GCn)
                            End If

                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function FFillOrderNo(ByVal HeaderConStr As String) As String

        If mTransactionType = EnumTransType.Payment Then
            FFillOrderNo = ""
            '' Total Bazar Over Due Date of this month
            'Dim mBazarOverDueDate As String = "SELECT H.DocId AS JObOrder, max(DATEADD(Day, H.TimePenaltyDays, H.DueDate)) AS OverDueDate, case When (SELECT Count (*) From JobOrderDetail Where JobOrder = H.DocId and Qty < 0 ) > 0 then 1 else 0 end AS Cancelled, max(H.TimePenalty) AS TimePenalty, sum(L.Qty) AS Qty, sum(L.TotalMeasure) AS  TotalMeasure, sum(L.TotalMeasure)*max(H.TimePenalty) AS  TotalPenalty " & _
            '                " FROM ( SELECT * FROM JobIssRec H   WHERE H.V_Type = 'WVREC' AND H.Site_Code = '" & AgL.PubSiteCode & "' AND H.Div_Code = '" & AgL.PubDivCode & "'  AND H.V_Date BETWEEN '" & AgL.RetMonthStartDate(TxtV_Date.Text) & "' AND '" & AgL.RetMonthEndDate(TxtV_Date.Text) & "')  R " & _
            '                " LEFT JOIN JobReceiveDetail L ON L.DocId = R.DocId  " & _
            '                " LEFT JOIN JobOrder H ON H.DocId = L.JobOrder " & _
            '                " WHERE(IFNull(H.TimePenalty, 0) > 0) " & _
            '                " " & HeaderConStr & " " & _
            '                " AND  DATEADD(Day, H.TimePenaltyDays, H.DueDate)  < R.V_Date " & _
            '                " GROUP BY H.DocId "

            '' Total Pending Qty till this month & Over Due Date
            'Dim mTotalPendingTillthisMonth As String = "SELECT VOrd.JobOrder, VOrd.LastDueDate AS OverDueDate, VOrd.Cancelled, Vord.TimePenalty, IFNull(VOrd.OrdQty,0) - IFNull(VRec.RecQty,0) AS Qty, IFNull(VOrd.OrdMeasure,0) - IFNull(VRec.RecMeasure,0) AS TotalMeasure, (IFNull(VOrd.OrdMeasure,0) - IFNull(VRec.RecMeasure,0))*Vord.TimePenalty AS TotalPenalty  FROM " & _
            '                " ( " & _
            '                " SELECT L.JobOrder, sum(L.Qty) AS OrdQty, sum(L.TotalMeasure) AS OrdMeasure,  Max(H.DueDate) AS DueDate, max(IFNull(H.TimePenalty,0)) AS TimePenalty,  CASE WHEN Min(L.Qty) < 0 THEN 1 ELSE 0 END AS Cancelled,  max(IFNull(H.TimePenaltyDays,0)) AS TimePenaltyDays, " & _
            '                " DATEADD(Day, Max(H.TimePenaltyDays), Max(H.DueDate)) AS LastDueDate  " & _
            '                " FROM JobOrderDetail L    " & _
            '                " LEFT JOIN JobOrder H  ON H.DocID = L.JobOrder  " & _
            '                " LEFT JOIN Voucher_Type Vt  ON Vt.V_Type = H.V_Type  " & _
            '                " WHERE Vt.NCat ='WVORD'  AND H.Div_Code = '" & AgL.PubDivCode & "' And H.Site_Code = '" & AgL.PubSiteCode & "'  " & _
            '                " AND  IFNull(H.TimePenalty,0) > 0 AND DATEADD(Day, H.TimePenaltyDays, H.DueDate)  <='" & AgL.RetMonthEndDate(TxtV_Date.Text) & "' " & _
            '                " " & HeaderConStr & " " & _
            '                " GROUP BY L.JobOrder " & _
            '                " HAVING sum(L.Qty) > 0 " & _
            '                " ) Vord " & _
            '                " LEFT JOIN  " & _
            '                " ( " & _
            '                " SELECT L.JobOrder, sum(L.Qty) AS RecQty, sum(L.TotalMeasure) AS RecMeasure " & _
            '                " FROM JobReceiveDetail L  " & _
            '                " LEFT JOIN JobIssRec H  ON H.DocID = L.DocId " & _
            '                " LEFT JOIN JobOrder JO  ON JO.DocID = L.JobOrder " & _
            '                " WHERE H.V_Date <='" & AgL.RetMonthEndDate(TxtV_Date.Text) & "' AND H.Site_Code ='" & AgL.PubSiteCode & "' AND H.Div_Code ='" & AgL.PubDivCode & "' AND H.V_Type ='WVREC' " & _
            '                " AND  IFNull(JO.TimePenalty,0) > 0 AND DATEADD(Day, JO.TimePenaltyDays, JO.DueDate)  <='" & AgL.RetMonthEndDate(TxtV_Date.Text) & "'" & _
            '                " GROUP BY L.JobOrder  " & _
            '                " ) VRec ON VRec.JobOrder = VOrd.JobOrder  " & _
            '                " WHERE IFNull(VOrd.OrdQty,0) - IFNull(VRec.RecQty,0) > 0 "



            'FRetFillPurjaNo = "Select Max(JO.CostCenter) AS CostCenter, Max(CCM.Name) AS PurjaNo, max(JO.V_Date) AS OrderDate, Max(JO.DueDate) AS DueDate, Max(JO.TimePenaltyDays) AS TimePenaltyDays , Max(JO.TimePenalty) AS Rate, " & _
            '                " Max(H.Cancelled) AS Cancelled, Max(SG.DispName) AS PartyName, Max(CCM.SubCode) AS SubCode, H.JobOrder, Max(JO.ManualrefNo) AS JobOrderNo, SUM(H.Qty) AS Qty, SUM(H.TotalMeasure) AS TotalMeasure " & _
            '                " From ( " & mBazarOverDueDate & " Union All " & mTotalPendingTillthisMonth & " ) H " & _
            '                " LEFT JOIN JobOrder JO on JO.DocId = H.JobOrder " & _
            '                " LEFT JOIN CostCenterMast CCM  ON CCM.Code = JO.CostCenter " & _
            '                " LEFT JOIN SubGroup Sg  On JO.JobWorker = Sg.SubCode " & _
            '                "  WHERE IFNull(CCM.Status,'Active') = 'Active' " & _
            '                "  AND IFNull(SG.SisterConcernYn,0) =0  " & _
            '                " Group By H.JobOrder "
        Else
            FFillOrderNo = " SELECT JO.DocId AS JobOrder,  max(JO.ManualrefNo) AS JobOrderNo, Max(JO.V_Date) AS OrderDate, Max(JO.DueDate) AS DueDate, Max(IFNull(VMain.MaxRecDate,'')) AS MaxRecDate,  max(SG.DispName) AS PartyName,  " &
                        " Max(Sg.SubCode) AS SubCode, sum(VMain.RecQty) AS RecQty, sum(VMain.RecMeasure) AS RecMeasure,  Max(IFNull(VMain.BillingType,'')) AS BillingType, Max(VMain.TimeIncentive) AS Rate, Max(IFNull(VMain.Cancelled,'')) AS Cancelled " &
                        " FROM " &
                        " ( " &
                        "   SELECT VO.JobOrder, VO.Cancelled, IFNull(VO.TimeIncentive,0) AS TimeIncentive, IFNull(VO.BillingType,0) AS BillingType, IFNull(VC.RecQty,0) AS RecQty, IFNull(VC.RecMeasure,0) AS RecMeasure, IFNull(VC.RecDate,'') AS MaxRecDate " &
                        "   FROM " &
                        "   ( " &
                        "       SELECT L.JobOrder, sum(L.Qty) AS OrdQty, sum(L.TotalMeasure) AS OrdMeasure, max(IFNull(H.BillingType,0)) AS BillingType, " &
                        "       Max(JO.V_Date) AS MaxOrdDate, Max(H.DueDate) AS DueDate, max(IFNull(H.TimeIncentive,0)) AS TimeIncentive, " &
                        "       CASE WHEN Min(L.Qty) < 0 THEN 1 ELSE 0 END AS Cancelled " &
                        "       FROM JobOrderDetail L  " &
                        "       LEFT JOIN JobOrder H  ON H.DocID = L.JobOrder  " &
                        "       LEFT JOIN JobOrder JO  ON JO.DocID = L.DocId " &
                        "       LEFT JOIN Voucher_Type Vt  ON Vt.V_Type = H.V_Type  " &
                        "       WHERE Vt.NCat ='" & AgTemplate.ClsMain.Temp_NCat.JobOrder & "' AND H.Div_Code = '" & AgL.PubDivCode & "' And H.Site_Code = '" & AgL.PubSiteCode & "' " &
                        "       AND H.V_Date <= '" & TxtV_Date.Text & "' AND H.Process = '" & TxtProcess.Tag & "' AND IFNull(H.TimeIncentive,0) > 0  " & HeaderConStr & " " &
                        "       GROUP BY L.JobOrder " &
                        "       Having max(IFNull(H.TimeIncentive,0)) > 0 AND Max(JO.V_Date) <= Max(H.DueDate) " &
                        "   ) VO  " &
                        "   LEFT JOIN  " &
                        "   ( " &
                        "       SELECT L.JobOrder, sum(L.Qty) AS RecQty , sum(L.TotalMeasure) AS RecMeasure, Max(H.V_Date) AS RecDate " &
                        "       FROM JobReceiveDetail L  " &
                        "       LEFT JOIN JobIssRec H  ON H.DocID = L.DocId  " &
                        "       LEFT JOIN Voucher_Type Vt  ON Vt.V_Type = H.V_Type " &
                        "       WHERE Vt.NCat in ( '" & AgTemplate.ClsMain.Temp_NCat.JobReceive & "','" & AgTemplate.ClsMain.Temp_NCat.JobInvoice & "') AND H.Div_Code = '" & AgL.PubDivCode & "' And H.Site_Code = '" & AgL.PubSiteCode & "' " &
                        "       AND H.Process = '" & TxtProcess.Tag & "' AND H.V_Date <= '" & TxtV_Date.Text & "' " &
                        "       GROUP BY L.JobOrder " &
                        "   ) VC ON VC.JobOrder = VO.JobOrder " &
                        " LEFT JOIN ( " &
                        "   SELECT H.ReferenceDocID, IFNull(Count(*),0) AS Cnt " &
                        "   FROM DuesPaymentDetail H  " &
                        "   LEFT JOIN DuesPayment DP ON DP.DocId = H.DocId " &
                        "   LEFT JOIN Voucher_Type Vt  ON Vt.V_Type = DP.V_Type " &
                        "   WHERE IFNull(H.ReferenceDocID,'') <>''  " &
                        "   AND DP.Div_Code = '" & AgL.PubDivCode & "' And DP.Site_Code = '" & AgL.PubSiteCode & "' " &
                        "   GROUP BY H.ReferenceDocID " &
                        " ) VP ON VP.ReferenceDocID = VO.JobOrder " &
                        "   WHERE IFNull(VO.OrdQty,0) - IFNull(VC.RecQty,0) <=0 AND IFNull(VC.RecQty,0) > 0 " &
                        " AND IFNull(VP.Cnt,0) = 0 " &
                        " ) VMain  " &
                        " LEFT JOIN JobOrder JO  ON JO.DocId = VMain.JobOrder " &
                        " LEFT JOIN SubGroup Sg  On JO.JobWorker = Sg.SubCode  " &
                        " GROUP BY JO.DocId " &
                        " Having Max(JO.DueDate) >= Max(IFNull(VMain.MaxRecDate,'')) "

        End If
    End Function

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

        Dim strNarration As String = ""

        With Dgl1
            For I = 0 To .Rows.Count - 1
                If .Item(Col1SubCode, I).Value <> "" Then

                    If mTransactionType = EnumTransType.Receipt Then
                        AmtDr = 0
                        AmtCr = Val(.Item(Col1Amount, I).Value)
                        mDebitCreditNoteAc = AgL.XNull(DtDuesPaymentEnviro.Rows(0)("CreditNoteAc"))
                        If mEntryType = EnumEntryType.DebitAndCreditNote Then
                            strNarration = ""
                        ElseIf mEntryType = EnumEntryType.TimeIncentiveAndPenalty Then
                            strNarration = "Time Incentive"
                        End If

                    Else
                        AmtDr = Val(.Item(Col1Amount, I).Value)
                        AmtCr = 0
                        mDebitCreditNoteAc = AgL.XNull(DtDuesPaymentEnviro.Rows(0)("DebitNoteAc"))
                        If mEntryType = EnumEntryType.DebitAndCreditNote Then
                            strNarration = ""
                        ElseIf mEntryType = EnumEntryType.TimeIncentiveAndPenalty Then
                            strNarration = "Time Penalty"
                        End If
                    End If

                    mSr += 1
                    mQry = " INSERT INTO Ledger(DocId, V_SNo, V_No, V_Type, V_Prefix, V_Date, RecId, SubCode, ContraSub, " &
                                " AmtDr, AmtCr, Narration,	Site_Code,U_Name,	U_EntDt,	U_AE,	" &
                                " DivCode, CostCenter, JobOrder) " &
                                " VALUES ('" & mInternalCode & "', " & Val(mSr) & ", " & Val(TxtV_No.Text) & ",	" &
                                " " & AgL.Chk_Text(TxtV_Type.AgSelectedValue) & ",	" & AgL.Chk_Text(LblPrefix.Text) & ", " &
                                " " & AgL.Chk_Text(TxtV_Date.Text) & ",	" & AgL.Chk_Text(TxtManualRefNo.Text) & ", " & AgL.Chk_Text(.Item(Col1SubCode, I).Tag) & ", " &
                                " " & AgL.Chk_Text(mDebitCreditNoteAc) & ",	" & AmtDr & ", " & AmtCr & ", " &
                                " '" & strNarration + .Item(Col1Remark, I).Value & "',	" & AgL.Chk_Text(AgL.PubSiteCode) & ", " &
                                " '" & AgL.PubUserName & "', '" & AgL.PubLoginDate & "', " &
                                " " & AgL.Chk_Text(AgL.MidStr(Topctrl1.Mode, 0, 1)) & ", " &
                                " '" & AgL.PubDivCode & "', " &
                                " " & AgL.Chk_Text(TxtCostcenter.Tag) & ", " &
                                " " & AgL.Chk_Text(.Item(Col1ReferenceDocId, I).Tag) & " " &
                                " ) "
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                    'mSr += 1
                    'mQry = " INSERT INTO Ledger(DocId, V_SNo, V_No, V_Type, V_Prefix, V_Date, SubCode, ContraSub, " & _
                    '        " AmtDr, AmtCr, Narration,	Site_Code,U_Name,	U_EntDt,	U_AE,	" & _
                    '        " DivCode, CostCenter, JobOrder) " & _
                    '        " VALUES ('" & mInternalCode & "', " & Val(mSr) & ", " & Val(TxtV_No.Text) & ",	" & _
                    '        " " & AgL.Chk_Text(TxtV_Type.AgSelectedValue) & ",	" & AgL.Chk_Text(LblPrefix.Text) & ", " & _
                    '        " " & AgL.Chk_Text(TxtV_Date.Text) & ",	" & AgL.Chk_Text(.Item(Col1SubCode, I).Tag) & ", " & _
                    '        " " & AgL.Chk_Text(mDebitCreditNoteAc) & ",	" & AmtCr & ", " & AmtDr & ", " & _
                    '        " 'Time Incentive Payment' + '" & .Item(Col1Remark, I).Value & "',	" & AgL.Chk_Text(AgL.PubSiteCode) & ", " & _
                    '        " '" & AgL.PubUserName & "', '" & AgL.PubLoginDate & "', " & _
                    '        " " & AgL.Chk_Text(AgL.MidStr(Topctrl1.Mode, 0, 1)) & ", " & _
                    '        " '" & AgL.PubDivCode & "', " & _
                    '        " " & AgL.Chk_Text(.Item(Col1CostCenter, I).Tag) & ", " & _
                    '        " " & AgL.Chk_Text(.Item(Col1JobOrder, I).Tag) & " " & _
                    '        " ) "
                    'AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)



                    If mTransactionType = EnumTransType.Receipt Then
                        AmtDr = Val(.Item(Col1Amount, I).Value)
                        AmtCr = 0
                        mDebitCreditNoteAc = AgL.XNull(DtDuesPaymentEnviro.Rows(0)("CreditNoteAc"))
                    Else
                        AmtDr = 0
                        AmtCr = Val(.Item(Col1Amount, I).Value)
                        mDebitCreditNoteAc = AgL.XNull(DtDuesPaymentEnviro.Rows(0)("DebitNoteAc"))
                    End If

                    mSr += 1
                    mQry = " INSERT INTO Ledger(DocId, V_SNo, V_No, V_Type, V_Prefix, V_Date, RecId, SubCode, ContraSub, " &
                            " AmtDr, AmtCr, Narration,	Site_Code, U_Name,	U_EntDt, U_AE, DivCode) " &
                            " VALUES ('" & mInternalCode & "', " & Val(mSr) & ", " & Val(TxtV_No.Text) & ",	" &
                            " " & AgL.Chk_Text(TxtV_Type.AgSelectedValue) & ",	" & AgL.Chk_Text(LblPrefix.Text) & ", " &
                            " " & AgL.Chk_Text(TxtV_Date.Text) & ",	" & AgL.Chk_Text(TxtManualRefNo.Text) & ", " & AgL.Chk_Text(mDebitCreditNoteAc) & ", " &
                            " " & AgL.Chk_Text(.Item(Col1SubCode, I).Tag) & ", " &
                            " " & AmtDr & ", " & AmtCr & ", " & AgL.Chk_Text(TxtRemarks.Text) & ",	" & AgL.Chk_Text(AgL.PubSiteCode) & ", " &
                            " '" & AgL.PubUserName & "', '" & AgL.PubLoginDate & "', " &
                            " " & AgL.Chk_Text(AgL.MidStr(Topctrl1.Mode, 0, 1)) & ", " &
                            " '" & AgL.PubDivCode & "') "
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

                    'mSr += 1
                    'mQry = " INSERT INTO Ledger(DocId, V_SNo, V_No, V_Type, V_Prefix, V_Date, SubCode, ContraSub, " & _
                    '        " AmtDr, AmtCr, Narration,	Site_Code, U_Name,	U_EntDt, U_AE, DivCode) " & _
                    '        " VALUES ('" & mInternalCode & "', " & Val(mSr) & ", " & Val(TxtV_No.Text) & ",	" & _
                    '        " " & AgL.Chk_Text(TxtV_Type.AgSelectedValue) & ",	" & AgL.Chk_Text(LblPrefix.Text) & ", " & _
                    '        " " & AgL.Chk_Text(TxtV_Date.Text) & ",	" & AgL.Chk_Text(mDebitCreditNoteAc) & ", " & _
                    '        " " & AgL.Chk_Text(.Item(Col1SubCode, I).Tag) & ", " & _
                    '        " " & AmtCr & ", " & AmtDr & ", " & AgL.Chk_Text(TxtRemarks.Text) & ",	" & AgL.Chk_Text(AgL.PubSiteCode) & ", " & _
                    '        " '" & AgL.PubUserName & "', '" & AgL.PubLoginDate & "', " & _
                    '        " " & AgL.Chk_Text(AgL.MidStr(Topctrl1.Mode, 0, 1)) & ", " & _
                    '        " '" & AgL.PubDivCode & "') "
                    'AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

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
        'If Dgl1.AgHelpDataSet(Col1JobOrder) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1JobOrder) = Nothing
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

        mQry = " SELECT H.SubCode AS Code, H.Name AS Name " &
                " FROM Subgroup H   " &
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

        If mEntryType = EnumEntryType.DebitAndCreditNote Then
            mQry = "SELECT H.DocID, H.V_Type, H.V_Prefix, H.V_Date, H.ManualRefNo, H.V_No, H.SubCode,  H.Remark, Sg.Name As ReasonAcName, P.Description AS ProcessDesc, " &
                    " L.Sr, L.ReferenceDocID, L.Reference_Sr, abs(L.Amount) AS Amount, L.PaidAmount, CM.Name AS CostCenter, SCM.DispName AS CostCenterName, SCM.Add1, SCM.Add2, C.CityName, " &
                    " IFNull(JO.ManualRefNo,Led.V_Type + '-' + Led.RecId) As BillNo, Led.V_Date AS BillDate, Vt.Description As BillType, L.Remark AS LineRemark, '" & TxtV_Type.Text & "' + ' No' AS EntryNoHead,  " &
                    " PI.Currency, PI.VendorDocNo AS PartyDocNo, PI.VendorDocDate AS PartyDocDate " &
                    " FROM ( SELECT * FROM DuesPayment WHERE DocID = '" & mSearchCode & "' ) AS H  " &
                    " LEFT JOIN Process P ON P.NCat = H.Process " &
                    " LEFT JOIN DuesPaymentDetail L ON L.DocID = H.DocID  " &
                    " LEFT JOIN SubGroup Sg On H.SubCode = Sg.SubCode   " &
                    " LEFT JOIN CostCenterMast CM ON CM.Code = L.CostCenter  " &
                    " LEFT JOIN SubGroup SCM ON SCM.SubCode =  L.Subcode  " &
                    " LEFT JOIN City C ON C.CityCode = SCM.CityCode " &
                    " LEFT JOIN (Select Distinct DocId, V_Date, V_Type, RecId From Ledger  ) As Led On L.ReferenceDocId = Led.DocId  " &
                    " LEFT JOIN JobOrder JO  On L.ReferenceDocId = JO.DocID " &
                    " LEFT JOIN PurchInvoice PI ON PI.DocId = L.ReferenceDocId " &
                    " LEFT JOIN Voucher_Type Vt On Led.V_Type = Vt.V_Type "
            ClsMain.FPrintThisDocument(Me, TxtV_Type.Tag, mQry, "Production_DebitCreditNote_Print", TxtV_Type.Text)
        ElseIf mEntryType = EnumEntryType.TimeIncentiveAndPenalty Then
            If mTransactionType = EnumTransType.Payment Then
                'mQry = "SELECT H.DocID, H.V_Type, H.V_Prefix, H.V_Date, H.ManualRefNo, H.V_No, H.SubCode,  H.Remark, " & _
                '        " Sg.Name As ReasonAcName,L.Sr, L.ReferenceDocID, L.Reference_Sr, L.Rate, L.Qty, L.Amount, L.PaidAmount, CM.Name AS CostCenter, " & _
                '        " SCM.DispName AS CostCenterName ,Led.V_Type + '-' + Led.RecId As BillNo, Vt.Description As BillType, L.Remark AS LineRemark,  " & _
                '        " VDate.OrdDate, VDate.DueDate, VDate.MaxRecDate  " & _
                '        " FROM ( SELECT * FROM DuesPayment WHERE DocID = '" & mSearchCode & "' ) AS H  " & _
                '        " LEFT JOIN DuesPaymentDetail L ON L.DocID = H.DocID  " & _
                '        " LEFT JOIN SubGroup Sg On H.SubCode = Sg.SubCode   " & _
                '        " LEFT JOIN CostCenterMast CM ON CM.Code = L.CostCenter  " & _
                '        " LEFT JOIN SubGroup SCM ON SCM.SubCode = CM.Subcode  " & _
                '        " LEFT JOIN (Select Distinct DocId, V_Type, RecId From Ledger) As Led On L.ReferenceDocId = Led.DocId  " & _
                '        " LEFT JOIN Voucher_Type Vt On Led.V_Type = Vt.V_Type " & _
                '        " LEFT JOIN " & _
                '        " ( " & _
                '        " SELECT H.CostCenter, max(H.V_Date) AS OrdDate, max(H.DueDate) AS DueDate, max(RH.V_Date) AS MaxRecDate " & _
                '        " FROM JobOrder H  " & _
                '        " LEFT JOIN JobReceiveDetail R  ON R.JobOrder = H.DocID " & _
                '        " LEFT JOIN JobIssRec RH  ON RH.DocID = R.DocId  " & _
                '        " WHERE H.V_Type ='WVORD' AND RH.V_Type ='WVREC' AND H.Div_Code = '" & AgL.PubDivCode & "' AND H.Site_Code ='" & AgL.PubSiteCode & "' " & _
                '        " GROUP BY H.CostCenter  " & _
                '        " ) VDate ON VDate.CostCenter = L.CostCenter "

                'ClsMain.FPrintThisDocument(Me, TxtV_Type.Tag, mQry, "Production_TimePenalty_Print", "Time Penalty")
            Else
                mQry = "SELECT H.DocID, H.V_Type, H.V_Prefix, H.V_Date, H.ManualRefNo, H.V_No, H.SubCode,  H.Remark, JO.ManualRefNo AS JObOrderNo, P.Description AS ProcessDesc, " &
                        " Sg.Name As ReasonAcName,L.Sr, L.ReferenceDocID, L.Reference_Sr, L.Rate, L.Qty, L.TotalMeasure, L.Amount, L.PaidAmount, CM.Name AS CostCenter, L.PartyName, " &
                        " SCM.DispName AS CostCenterName ,Led.V_Type + '-' + Led.RecId As BillNo, Vt.Description As BillType, L.Remark AS LineRemark,  " &
                        " VDate.OrdDate, VDate.DueDate, VDate.MaxRecDate  " &
                        " FROM ( SELECT * FROM DuesPayment WHERE DocID = '" & mSearchCode & "' ) AS H  " &
                        " LEFT JOIN Process P ON P.NCat = H.Process " &
                        " LEFT JOIN DuesPaymentDetail L ON L.DocID = H.DocID  " &
                        " LEFT JOIN SubGroup Sg On H.SubCode = Sg.SubCode   " &
                        " LEFT JOIN CostCenterMast CM ON CM.Code = L.CostCenter  " &
                        " LEFT JOIN SubGroup SCM ON SCM.SubCode = CM.Subcode  " &
                        " LEFT JOIN JobOrder JO ON JO.DocId = L.ReferenceDocId " &
                        " LEFT JOIN (Select Distinct DocId, V_Type, RecId From Ledger) As Led On L.ReferenceDocId = Led.DocId  " &
                        " LEFT JOIN Voucher_Type Vt On Led.V_Type = Vt.V_Type " &
                        " LEFT JOIN " &
                        " ( " &
                        " SELECT H.DocId, max(H.V_Date) AS OrdDate, max(H.DueDate) AS DueDate, max(RH.V_Date) AS MaxRecDate " &
                        " FROM JobOrder H  " &
                        " LEFT JOIN JobReceiveDetail R  ON R.JobOrder = H.DocID " &
                        " LEFT JOIN JobIssRec RH  ON RH.DocID = R.DocId  " &
                        " WHERE H.Process =" & AgL.Chk_Text(TxtProcess.Tag) & " AND H.Div_Code = '" & AgL.PubDivCode & "' AND H.Site_Code ='" & AgL.PubSiteCode & "' " &
                        " GROUP BY H.DocId  " &
                        " ) VDate ON VDate.DocId = L.ReferenceDocId "
                ClsMain.FPrintThisDocument(Me, TxtV_Type.Tag, mQry, "Production_TimeIncentive_Print", "Time Incentive")
            End If
        End If
 

    End Sub

    Private Sub BtnFillPurjaNo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnFillOrderNo.Click
        Try
            If Topctrl1.Mode = "Browse" Then Exit Sub
            Dim StrTicked As String = ""
            StrTicked = FHPGD_PendingJobOrder()
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

    Private Function FHPGD_PendingJobOrder() As String
        Dim FRH_Multiple As DMHelpGrid.FrmHelpGrid_Multi
        Dim StrRtn As String = ""
        Dim strCond As String = ""

        mQry = " SELECT 'o' As Tick, VMain.JobOrder As Code, VMain.JobOrderNo, VMain.OrderDate AS OrderDate, VMain.DueDate AS DueDate, VMain.MaxRecDate AS MaxRecDate, VMain.PartyName " & _
                " FROM ( " & FFillOrderNo("") & " ) As VMain Order BY VMain.DueDate "
        FRH_Multiple = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(AgL.FillData(mQry, AgL.GCn).TABLES(0)), "", 500, 750, , , False)
        FRH_Multiple.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple.FFormatColumn(1, , 0, , False)
        FRH_Multiple.FFormatColumn(2, "Order No.", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(3, "Order Date", 100, DataGridViewContentAlignment.MiddleRight)
        FRH_Multiple.FFormatColumn(4, "Due Date", 100, DataGridViewContentAlignment.MiddleRight)
        FRH_Multiple.FFormatColumn(5, "Last Rec Date", 100, DataGridViewContentAlignment.MiddleRight)
        FRH_Multiple.FFormatColumn(6, "Party Name", 200, DataGridViewContentAlignment.MiddleLeft)

        FRH_Multiple.StartPosition = FormStartPosition.CenterScreen
        FRH_Multiple.ShowDialog()

        If FRH_Multiple.BytBtnValue = 0 Then
            StrRtn = FRH_Multiple.FFetchData(1, "'", "'", ",", True)
        End If
        FHPGD_PendingJobOrder = StrRtn

        FRH_Multiple = Nothing
    End Function

    Private Sub FFillItemsForOrder(ByVal bCostcenterStr As String)
        Dim I As Integer = 0
        Dim DtTemp As DataTable = Nothing
        Try
            If bCostcenterStr = "" Then Exit Sub
            mQry = FFillOrderNo(" And H.DocID In (" & bCostcenterStr & ") ") & " Order by PartyName, JobOrderNo "


            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

            With DtTemp
                Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
                If .Rows.Count > 0 Then
                    For I = 0 To .Rows.Count - 1
                        Dgl1.Rows.Add()
                        Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count - 1

                        Dgl1.Item(Col1ReferenceDocId, I).Tag = AgL.XNull(.Rows(I)("JobOrder"))
                        Dgl1.Item(Col1ReferenceDocId, I).Value = AgL.XNull(.Rows(I)("JobOrderNo"))
                        'Dgl1.Item(Col1CostCenter, I).Tag = AgL.XNull(.Rows(I)("CostCenter"))
                        'Dgl1.Item(Col1CostCenter, I).Value = AgL.XNull(.Rows(I)("PurjaNo"))
                        'Dgl1.Item(Col1CostCenterSubCode, I).Tag = AgL.XNull(.Rows(I)("SubCode"))
                        'Dgl1.Item(Col1CostCenterSubCode, I).Value = AgL.XNull(.Rows(I)("PartyName"))

                        Dgl1.Item(Col1BillingType, I).Value = AgL.XNull(.Rows(I)("BillingType"))
                        Dgl1.Item(Col1SubCode, I).Tag = AgL.XNull(.Rows(I)("SubCode"))
                        Dgl1.Item(Col1SubCode, I).Value = AgL.XNull(.Rows(I)("PartyName"))
                        Dgl1.Item(Col1Qty, I).Value = AgL.VNull(.Rows(I)("RecQty"))
                        Dgl1.Item(Col1Measure, I).Value = AgL.VNull(.Rows(I)("RecMeasure"))
                        Dgl1.Item(Col1Rate, I).Value = AgL.VNull(.Rows(I)("Rate"))


                        If Dgl1.Item(Col1BillingType, I).Value <> "Qty" Then
                            Dgl1.Item(Col1Amount, I).Value = Math.Round(Val(Dgl1.Item(Col1Measure, I).Value) * Val(Dgl1.Item(Col1Rate, I).Value), 0)
                            Dgl1.Item(Col1AdjustedAmount, I).Value = Math.Round(Val(Dgl1.Item(Col1Measure, I).Value) * Val(Dgl1.Item(Col1Rate, I).Value), 0)
                        Else
                            Dgl1.Item(Col1Amount, I).Value = Math.Round(Val(Dgl1.Item(Col1Qty, I).Value) * Val(Dgl1.Item(Col1Rate, I).Value), 0)
                            Dgl1.Item(Col1AdjustedAmount, I).Value = Math.Round(Val(Dgl1.Item(Col1Qty, I).Value) * Val(Dgl1.Item(Col1Rate, I).Value), 0)
                        End If

                        If AgL.VNull(.Rows(I)("Cancelled")) = 1 Then
                            Dgl1.Rows(I).DefaultCellStyle.ForeColor = Color.Red
                        End If
                    Next I
                End If
            End With
            Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FrmJobIncentivePenalty_BaseFunction_DispText() Handles Me.BaseFunction_DispText
        If mEntryType = EnumEntryType.DebitAndCreditNote Then
            BtnFillOrderNo.Visible = False
        End If
    End Sub
End Class
