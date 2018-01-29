Imports CrystalDecisions.CrystalReports.Engine
Public Class FrmDebitCreditNote
    Inherits AgTemplate.TempTransaction
    Dim mQry$

    Public Event BaseFunction_MoveRecLine(ByVal SearchCode As String, ByVal Sr As Integer, ByVal mGridRow As Integer)
    Public Event BaseEvent_Save_InTransLine(ByVal SearchCode As String, ByVal Sr As Integer, ByVal mGridRow As Integer, ByVal Conn As SqlClient.SqlConnection, ByVal Cmd As SqlClient.SqlCommand)



    Dim DtDuesPaymentEnviro As DataTable
    Dim mTransactionType As TransactionType = TransactionType.CreditNote

    Public Sub New(ByVal StrUPVar As String, ByVal DTUP As DataTable, ByVal NCat As String, ByVal TransType As TransactionType)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Topctrl1.FSetParent(Me, StrUPVar, DTUP)
        Topctrl1.SetDisp(True)

        EntryNCat = NCat
        mTransactionType = TransType
    End Sub

  

    Enum TransactionType
        CreditNote = 0
        DebitNote = 1
    End Enum


#Region "Form Designer Code"
    Private Sub InitializeComponent()
        Me.LblSubCodeReq = New System.Windows.Forms.Label
        Me.TxtSubCode = New AgControls.AgTextBox
        Me.LblSUbCode = New System.Windows.Forms.Label
        Me.TxtRemarks = New AgControls.AgTextBox
        Me.Label30 = New System.Windows.Forms.Label
        Me.TxtPaidAmount = New AgControls.AgTextBox
        Me.LblPaidAmount = New System.Windows.Forms.Label
        Me.TxtCurrBalance = New AgControls.AgTextBox
        Me.lblCurrBalance = New System.Windows.Forms.Label
        Me.TxtCashBankAc = New AgControls.AgTextBox
        Me.LblAdjustmentAc = New System.Windows.Forms.Label
        Me.LblPaidAmountReq = New System.Windows.Forms.Label
        Me.TxtManualRefNo = New AgControls.AgTextBox
        Me.LblManualRefNo = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.GroupBox2.SuspendLayout()
        Me.GBoxMoveToLog.SuspendLayout()
        Me.GBoxApprove.SuspendLayout()
        Me.GBoxEntryType.SuspendLayout()
        Me.GrpUP.SuspendLayout()
        Me.GBoxDivision.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TP1.SuspendLayout()
        CType(Me.DTMaster, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox2
        '
        Me.GroupBox2.Location = New System.Drawing.Point(723, 259)
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
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(653, 259)
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
        Me.GBoxApprove.Location = New System.Drawing.Point(466, 259)
        Me.GBoxApprove.Size = New System.Drawing.Size(148, 40)
        Me.GBoxApprove.Text = "Approved By"
        '
        'TxtApproveBy
        '
        Me.TxtApproveBy.Location = New System.Drawing.Point(3, 19)
        Me.TxtApproveBy.Size = New System.Drawing.Size(142, 18)
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
        Me.GBoxEntryType.Location = New System.Drawing.Point(241, 259)
        Me.GBoxEntryType.Size = New System.Drawing.Size(119, 40)
        '
        'TxtEntryType
        '
        Me.TxtEntryType.Location = New System.Drawing.Point(3, 19)
        Me.TxtEntryType.Tag = ""
        '
        'GrpUP
        '
        Me.GrpUP.Location = New System.Drawing.Point(16, 259)
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
        Me.GroupBox1.Location = New System.Drawing.Point(2, 255)
        Me.GroupBox1.Size = New System.Drawing.Size(897, 4)
        '
        'GBoxDivision
        '
        Me.GBoxDivision.Location = New System.Drawing.Point(496, 259)
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
        Me.LblV_No.Location = New System.Drawing.Point(678, 137)
        Me.LblV_No.Size = New System.Drawing.Size(40, 16)
        Me.LblV_No.Tag = ""
        Me.LblV_No.Text = "V_No"
        Me.LblV_No.Visible = False
        '
        'TxtV_No
        '
        Me.TxtV_No.AgSelectedValue = ""
        Me.TxtV_No.BackColor = System.Drawing.Color.White
        Me.TxtV_No.Location = New System.Drawing.Point(786, 136)
        Me.TxtV_No.Size = New System.Drawing.Size(123, 18)
        Me.TxtV_No.TabIndex = 3
        Me.TxtV_No.Tag = ""
        Me.TxtV_No.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.TxtV_No.Visible = False
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(314, 62)
        Me.Label2.Tag = ""
        '
        'LblV_Date
        '
        Me.LblV_Date.BackColor = System.Drawing.Color.Transparent
        Me.LblV_Date.Location = New System.Drawing.Point(211, 57)
        Me.LblV_Date.Tag = ""
        '
        'LblV_TypeReq
        '
        Me.LblV_TypeReq.Location = New System.Drawing.Point(528, 42)
        Me.LblV_TypeReq.Tag = ""
        '
        'TxtV_Date
        '
        Me.TxtV_Date.AgSelectedValue = ""
        Me.TxtV_Date.BackColor = System.Drawing.Color.White
        Me.TxtV_Date.Location = New System.Drawing.Point(330, 56)
        Me.TxtV_Date.TabIndex = 2
        Me.TxtV_Date.Tag = ""
        '
        'LblV_Type
        '
        Me.LblV_Type.Location = New System.Drawing.Point(436, 38)
        Me.LblV_Type.Tag = ""
        '
        'TxtV_Type
        '
        Me.TxtV_Type.AgSelectedValue = ""
        Me.TxtV_Type.BackColor = System.Drawing.Color.White
        Me.TxtV_Type.Location = New System.Drawing.Point(544, 36)
        Me.TxtV_Type.Size = New System.Drawing.Size(123, 18)
        Me.TxtV_Type.TabIndex = 1
        Me.TxtV_Type.Tag = ""
        '
        'LblSite_CodeReq
        '
        Me.LblSite_CodeReq.Location = New System.Drawing.Point(314, 42)
        Me.LblSite_CodeReq.Tag = ""
        '
        'LblSite_Code
        '
        Me.LblSite_Code.BackColor = System.Drawing.Color.Transparent
        Me.LblSite_Code.Location = New System.Drawing.Point(211, 37)
        Me.LblSite_Code.Size = New System.Drawing.Size(87, 16)
        Me.LblSite_Code.Tag = ""
        Me.LblSite_Code.Text = "Branch Name"
        '
        'TxtSite_Code
        '
        Me.TxtSite_Code.AgSelectedValue = ""
        Me.TxtSite_Code.BackColor = System.Drawing.Color.White
        Me.TxtSite_Code.Location = New System.Drawing.Point(330, 36)
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
        Me.LblPrefix.Location = New System.Drawing.Point(738, 137)
        Me.LblPrefix.Tag = ""
        Me.LblPrefix.Visible = False
        '
        'TabControl1
        '
        Me.TabControl1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl1.Location = New System.Drawing.Point(-3, 17)
        Me.TabControl1.Size = New System.Drawing.Size(886, 237)
        Me.TabControl1.TabIndex = 0
        '
        'TP1
        '
        Me.TP1.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.TP1.Controls.Add(Me.Label1)
        Me.TP1.Controls.Add(Me.TxtManualRefNo)
        Me.TP1.Controls.Add(Me.LblManualRefNo)
        Me.TP1.Controls.Add(Me.LblPaidAmountReq)
        Me.TP1.Controls.Add(Me.TxtCashBankAc)
        Me.TP1.Controls.Add(Me.LblAdjustmentAc)
        Me.TP1.Controls.Add(Me.TxtPaidAmount)
        Me.TP1.Controls.Add(Me.LblPaidAmount)
        Me.TP1.Controls.Add(Me.TxtCurrBalance)
        Me.TP1.Controls.Add(Me.lblCurrBalance)
        Me.TP1.Controls.Add(Me.LblSubCodeReq)
        Me.TP1.Controls.Add(Me.TxtSubCode)
        Me.TP1.Controls.Add(Me.LblSUbCode)
        Me.TP1.Controls.Add(Me.TxtRemarks)
        Me.TP1.Controls.Add(Me.Label30)
        Me.TP1.Location = New System.Drawing.Point(4, 22)
        Me.TP1.Size = New System.Drawing.Size(878, 211)
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
        Me.TP1.Controls.SetChildIndex(Me.LblSUbCode, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtSubCode, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblSubCodeReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.lblCurrBalance, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtCurrBalance, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblPaidAmount, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtPaidAmount, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblAdjustmentAc, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtCashBankAc, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblPaidAmountReq, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblManualRefNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtManualRefNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label1, 0)
        '
        'Topctrl1
        '
        Me.Topctrl1.Size = New System.Drawing.Size(879, 41)
        Me.Topctrl1.TabIndex = 1
        '
        'LblSubCodeReq
        '
        Me.LblSubCodeReq.AutoSize = True
        Me.LblSubCodeReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblSubCodeReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblSubCodeReq.Location = New System.Drawing.Point(314, 82)
        Me.LblSubCodeReq.Name = "LblSubCodeReq"
        Me.LblSubCodeReq.Size = New System.Drawing.Size(10, 7)
        Me.LblSubCodeReq.TabIndex = 746
        Me.LblSubCodeReq.Text = "Ä"
        '
        'TxtSubCode
        '
        Me.TxtSubCode.AgAllowUserToEnableMasterHelp = False
        Me.TxtSubCode.AgMandatory = True
        Me.TxtSubCode.AgMasterHelp = False
        Me.TxtSubCode.AgNumberLeftPlaces = 8
        Me.TxtSubCode.AgNumberNegetiveAllow = False
        Me.TxtSubCode.AgNumberRightPlaces = 2
        Me.TxtSubCode.AgPickFromLastValue = False
        Me.TxtSubCode.AgRowFilter = ""
        Me.TxtSubCode.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtSubCode.AgSelectedValue = Nothing
        Me.TxtSubCode.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtSubCode.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtSubCode.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtSubCode.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtSubCode.Location = New System.Drawing.Point(330, 76)
        Me.TxtSubCode.MaxLength = 20
        Me.TxtSubCode.Name = "TxtSubCode"
        Me.TxtSubCode.Size = New System.Drawing.Size(337, 18)
        Me.TxtSubCode.TabIndex = 4
        '
        'LblSUbCode
        '
        Me.LblSUbCode.AutoSize = True
        Me.LblSUbCode.BackColor = System.Drawing.Color.Transparent
        Me.LblSUbCode.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblSUbCode.Location = New System.Drawing.Point(211, 77)
        Me.LblSUbCode.Name = "LblSUbCode"
        Me.LblSUbCode.Size = New System.Drawing.Size(39, 16)
        Me.LblSUbCode.TabIndex = 745
        Me.LblSUbCode.Text = "Party"
        '
        'TxtRemarks
        '
        Me.TxtRemarks.AgAllowUserToEnableMasterHelp = False
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
        Me.TxtRemarks.Location = New System.Drawing.Point(330, 137)
        Me.TxtRemarks.MaxLength = 255
        Me.TxtRemarks.Name = "TxtRemarks"
        Me.TxtRemarks.Size = New System.Drawing.Size(337, 18)
        Me.TxtRemarks.TabIndex = 8
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(211, 138)
        Me.Label30.Name = "Label30"
        Me.Label30.Size = New System.Drawing.Size(60, 16)
        Me.Label30.TabIndex = 744
        Me.Label30.Text = "Remarks"
        '
        'TxtPaidAmount
        '
        Me.TxtPaidAmount.AgAllowUserToEnableMasterHelp = False
        Me.TxtPaidAmount.AgMandatory = True
        Me.TxtPaidAmount.AgMasterHelp = False
        Me.TxtPaidAmount.AgNumberLeftPlaces = 8
        Me.TxtPaidAmount.AgNumberNegetiveAllow = False
        Me.TxtPaidAmount.AgNumberRightPlaces = 2
        Me.TxtPaidAmount.AgPickFromLastValue = False
        Me.TxtPaidAmount.AgRowFilter = ""
        Me.TxtPaidAmount.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtPaidAmount.AgSelectedValue = Nothing
        Me.TxtPaidAmount.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtPaidAmount.AgValueType = AgControls.AgTextBox.TxtValueType.Number_Value
        Me.TxtPaidAmount.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtPaidAmount.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPaidAmount.Location = New System.Drawing.Point(544, 96)
        Me.TxtPaidAmount.MaxLength = 20
        Me.TxtPaidAmount.Name = "TxtPaidAmount"
        Me.TxtPaidAmount.Size = New System.Drawing.Size(123, 18)
        Me.TxtPaidAmount.TabIndex = 6
        Me.TxtPaidAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'LblPaidAmount
        '
        Me.LblPaidAmount.AutoSize = True
        Me.LblPaidAmount.BackColor = System.Drawing.Color.Transparent
        Me.LblPaidAmount.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblPaidAmount.Location = New System.Drawing.Point(436, 97)
        Me.LblPaidAmount.Name = "LblPaidAmount"
        Me.LblPaidAmount.Size = New System.Drawing.Size(83, 16)
        Me.LblPaidAmount.TabIndex = 754
        Me.LblPaidAmount.Text = "Paid Amount"
        '
        'TxtCurrBalance
        '
        Me.TxtCurrBalance.AgAllowUserToEnableMasterHelp = False
        Me.TxtCurrBalance.AgMandatory = False
        Me.TxtCurrBalance.AgMasterHelp = False
        Me.TxtCurrBalance.AgNumberLeftPlaces = 8
        Me.TxtCurrBalance.AgNumberNegetiveAllow = False
        Me.TxtCurrBalance.AgNumberRightPlaces = 2
        Me.TxtCurrBalance.AgPickFromLastValue = False
        Me.TxtCurrBalance.AgRowFilter = ""
        Me.TxtCurrBalance.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtCurrBalance.AgSelectedValue = Nothing
        Me.TxtCurrBalance.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtCurrBalance.AgValueType = AgControls.AgTextBox.TxtValueType.Number_Value
        Me.TxtCurrBalance.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtCurrBalance.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCurrBalance.Location = New System.Drawing.Point(330, 96)
        Me.TxtCurrBalance.MaxLength = 20
        Me.TxtCurrBalance.Name = "TxtCurrBalance"
        Me.TxtCurrBalance.Size = New System.Drawing.Size(100, 18)
        Me.TxtCurrBalance.TabIndex = 5
        Me.TxtCurrBalance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblCurrBalance
        '
        Me.lblCurrBalance.AutoSize = True
        Me.lblCurrBalance.BackColor = System.Drawing.Color.Transparent
        Me.lblCurrBalance.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCurrBalance.Location = New System.Drawing.Point(211, 97)
        Me.lblCurrBalance.Name = "lblCurrBalance"
        Me.lblCurrBalance.Size = New System.Drawing.Size(83, 16)
        Me.lblCurrBalance.TabIndex = 753
        Me.lblCurrBalance.Text = "Curr Balance"
        '
        'TxtCashBankAc
        '
        Me.TxtCashBankAc.AgAllowUserToEnableMasterHelp = False
        Me.TxtCashBankAc.AgMandatory = False
        Me.TxtCashBankAc.AgMasterHelp = False
        Me.TxtCashBankAc.AgNumberLeftPlaces = 8
        Me.TxtCashBankAc.AgNumberNegetiveAllow = False
        Me.TxtCashBankAc.AgNumberRightPlaces = 2
        Me.TxtCashBankAc.AgPickFromLastValue = False
        Me.TxtCashBankAc.AgRowFilter = ""
        Me.TxtCashBankAc.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtCashBankAc.AgSelectedValue = Nothing
        Me.TxtCashBankAc.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtCashBankAc.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtCashBankAc.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtCashBankAc.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCashBankAc.Location = New System.Drawing.Point(330, 117)
        Me.TxtCashBankAc.MaxLength = 20
        Me.TxtCashBankAc.Name = "TxtCashBankAc"
        Me.TxtCashBankAc.Size = New System.Drawing.Size(337, 18)
        Me.TxtCashBankAc.TabIndex = 7
        '
        'LblAdjustmentAc
        '
        Me.LblAdjustmentAc.AutoSize = True
        Me.LblAdjustmentAc.BackColor = System.Drawing.Color.Transparent
        Me.LblAdjustmentAc.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblAdjustmentAc.Location = New System.Drawing.Point(211, 118)
        Me.LblAdjustmentAc.Name = "LblAdjustmentAc"
        Me.LblAdjustmentAc.Size = New System.Drawing.Size(98, 16)
        Me.LblAdjustmentAc.TabIndex = 759
        Me.LblAdjustmentAc.Text = "Adjustment A/c"
        '
        'LblPaidAmountReq
        '
        Me.LblPaidAmountReq.AutoSize = True
        Me.LblPaidAmountReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblPaidAmountReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblPaidAmountReq.Location = New System.Drawing.Point(528, 103)
        Me.LblPaidAmountReq.Name = "LblPaidAmountReq"
        Me.LblPaidAmountReq.Size = New System.Drawing.Size(10, 7)
        Me.LblPaidAmountReq.TabIndex = 761
        Me.LblPaidAmountReq.Text = "Ä"
        '
        'TxtManualRefNo
        '
        Me.TxtManualRefNo.AgAllowUserToEnableMasterHelp = False
        Me.TxtManualRefNo.AgMandatory = False
        Me.TxtManualRefNo.AgMasterHelp = False
        Me.TxtManualRefNo.AgNumberLeftPlaces = 0
        Me.TxtManualRefNo.AgNumberNegetiveAllow = False
        Me.TxtManualRefNo.AgNumberRightPlaces = 0
        Me.TxtManualRefNo.AgPickFromLastValue = False
        Me.TxtManualRefNo.AgRowFilter = ""
        Me.TxtManualRefNo.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtManualRefNo.AgSelectedValue = Nothing
        Me.TxtManualRefNo.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtManualRefNo.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtManualRefNo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtManualRefNo.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtManualRefNo.Location = New System.Drawing.Point(544, 56)
        Me.TxtManualRefNo.MaxLength = 255
        Me.TxtManualRefNo.Name = "TxtManualRefNo"
        Me.TxtManualRefNo.Size = New System.Drawing.Size(123, 18)
        Me.TxtManualRefNo.TabIndex = 3
        '
        'LblManualRefNo
        '
        Me.LblManualRefNo.AutoSize = True
        Me.LblManualRefNo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblManualRefNo.Location = New System.Drawing.Point(436, 57)
        Me.LblManualRefNo.Name = "LblManualRefNo"
        Me.LblManualRefNo.Size = New System.Drawing.Size(59, 16)
        Me.LblManualRefNo.TabIndex = 763
        Me.LblManualRefNo.Text = "Entry No"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(528, 62)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(10, 7)
        Me.Label1.TabIndex = 764
        Me.Label1.Text = "Ä"
        '
        'FrmDebitCreditNote
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.BackColor = System.Drawing.SystemColors.ButtonShadow
        Me.ClientSize = New System.Drawing.Size(879, 300)
        Me.Name = "FrmDebitCreditNote"
        Me.Text = "Debit Credit Note"
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
        Me.ResumeLayout(False)

    End Sub
    Protected WithEvents LblSubCodeReq As System.Windows.Forms.Label
    Protected WithEvents TxtSubCode As AgControls.AgTextBox
    Protected WithEvents LblSUbCode As System.Windows.Forms.Label
    Protected WithEvents TxtRemarks As AgControls.AgTextBox
    Protected WithEvents Label30 As System.Windows.Forms.Label
    Protected WithEvents TxtPaidAmount As AgControls.AgTextBox
    Protected WithEvents LblPaidAmount As System.Windows.Forms.Label
    Protected WithEvents TxtCurrBalance As AgControls.AgTextBox
    Protected WithEvents lblCurrBalance As System.Windows.Forms.Label
    Protected WithEvents TxtCashBankAc As AgControls.AgTextBox
    Protected WithEvents LblAdjustmentAc As System.Windows.Forms.Label
    Protected WithEvents LblPaidAmountReq As System.Windows.Forms.Label
    Protected WithEvents Label1 As System.Windows.Forms.Label
    Protected WithEvents TxtManualRefNo As AgControls.AgTextBox
    Protected WithEvents LblManualRefNo As System.Windows.Forms.Label
#End Region

    Private Sub AccountPosting(ByVal SearchCode As String, ByVal Conn As System.Data.SqlClient.SqlConnection, ByVal Cmd As System.Data.SqlClient.SqlCommand)
        Dim mContraText As String = "", mCashBankAc As String, mNarration$

        mCashBankAc = TxtCashBankAc.AgSelectedValue


        mNarration = TxtRemarks.Text

        mQry = "Delete from Ledger Where DocID ='" & mInternalCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        ClsMain.FPrepareContraText(True, mContraText, TxtSubCode.AgSelectedValue, TxtPaidAmount.Text, IIf(mTransactionType = TransactionType.DebitNote, "Dr", "Cr"))
        mQry = "Insert Into Ledger(DocId,RecId,V_SNo,V_Date,SubCode,ContraSub,AmtDr,AmtCr," & _
                 " Narration,V_Type,V_No,V_Prefix,Site_Code, DivCode,Chq_No,Chq_Date,TDSCategory,TDSOnAmt,TDSDesc,TDSPer,TDS_Of_V_SNo,System_Generated,FormulaString,ContraText) Values " & _
                 " ('" & mInternalCode & "','" & TxtV_No.Text & "', 1 ," & AgL.ConvertDate(TxtV_Date.Text) & "," & AgL.Chk_Text(mCashBankAc) & "," & AgL.Chk_Text("") & ", " & _
                 " " & IIf(Not mTransactionType = TransactionType.DebitNote, Val(TxtPaidAmount.Text), 0) & "," & IIf(mTransactionType = TransactionType.DebitNote, Val(TxtPaidAmount.Text), 0) & ", " & _
                 " " & AgL.Chk_Text(TxtRemarks.Text) & ",'" & TxtV_Type.AgSelectedValue & "','" & Val(TxtV_No.Text) & "','" & LblPrefix.Text & "'," & _
                 " '" & TxtSite_Code.AgSelectedValue & "','" & TxtDivision.AgSelectedValue & "','" & AgL.Chk_Text("") & "'," & _
                 " " & AgL.ConvertDate("") & "," & AgL.Chk_Text("") & "," & _
                 " " & Val("") & "," & AgL.Chk_Text("") & "," & Val("") & "," & 0 & ",'Y','" & "" & "','" & mContraText & "')"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        ClsMain.FPrepareContraText(True, mContraText, mCashBankAc, TxtPaidAmount.Text, IIf(mTransactionType = TransactionType.DebitNote, "Cr", "Dr"))
        mQry = "Insert Into Ledger(DocId,RecId,V_SNo,V_Date,SubCode,ContraSub,AmtDr,AmtCr," & _
                 " Narration,V_Type,V_No,V_Prefix,Site_Code, DivCode,Chq_No,Chq_Date,TDSCategory,TDSOnAmt,TDSDesc,TDSPer,TDS_Of_V_SNo,System_Generated,FormulaString,ContraText) Values " & _
                 " ('" & mInternalCode & "','" & TxtV_No.Text & "', 3 ," & AgL.ConvertDate(TxtV_Date.Text) & "," & AgL.Chk_Text(TxtSubCode.AgSelectedValue) & "," & AgL.Chk_Text("") & ", " & _
                 " " & IIf(mTransactionType = TransactionType.DebitNote, Val(TxtPaidAmount.Text), 0) & "," & IIf(Not mTransactionType = TransactionType.DebitNote, Val(TxtPaidAmount.Text), 0) & ", " & _
                 " " & AgL.Chk_Text(TxtRemarks.Text) & ",'" & TxtV_Type.AgSelectedValue & "','" & Val(TxtV_No.Text) & "','" & LblPrefix.Text & "'," & _
                 " '" & TxtSite_Code.AgSelectedValue & "','" & TxtDivision.AgSelectedValue & "','" & AgL.Chk_Text("") & "'," & _
                 " " & AgL.ConvertDate("") & "," & AgL.Chk_Text("") & "," & _
                 " " & Val("") & "," & AgL.Chk_Text("") & "," & Val("") & "," & 0 & ",'Y','" & "" & "','" & mContraText & "')"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    End Sub

    Private Sub FrmPaymentReceipt_BaseEvent_ApproveDeletion_InTrans(ByVal SearchCode As String, ByVal Conn As System.Data.SqlClient.SqlConnection, ByVal Cmd As System.Data.SqlClient.SqlCommand) Handles Me.BaseEvent_ApproveDeletion_InTrans
        AgL.LedgerUnPost(Conn, Cmd, mSearchCode)
    End Sub

    Private Sub FrmQuality1_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "DuesPayment"
        MainLineTableCsv = "DuesPaymentDetail"
    End Sub

    Private Sub FrmQuality1_BaseFunction_FIniMast(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMast
        Dim mCondStr$

        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) & _
                        " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "H.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat In ('" & EntryNCat & "')"

        mQry = "Select DocID As SearchCode " & _
                " From DuesPayment H " & _
                " Left Join Voucher_Type Vt On H.V_Type = Vt.V_Type  " & _
                " Where IsNull(IsDeleted,0)=0 And IsNull(H.TransactionType,'Debit Note')='" & IIf(mTransactionType = TransactionType.DebitNote, "Debit Note", "Credit Note") & "'  " & mCondStr & "  Order By V_Date Desc "
        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub FrmSaleOrder_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mCondStr$

        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) & _
                        " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "H.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat In ('" & EntryNCat & "')"

        AgL.PubFindQry = " SELECT H.DocId AS SearchCode, H.V_Type AS [Payment Type], H.V_Prefix AS [Prefix], H.V_Date AS Date, H.V_No AS [Payment No], " & _
                            " H.TransactionType AS [TRANSACTION Type], H.PartyName AS [Party Name], H.PartyAddress AS [Party Address], H.PartyCity AS [Party City],  " & _
                            " H.CurrBalance AS [Currunt Balance], H.PaidAmount AS [Paid Amount], H.Discount, H.NetAmount AS [Net Amount], H.CashBank AS [Cash/Bank],  " & _
                            " H.CashBankAc AS [Cash/Bank A/c], H.ChqNo AS [Cheque No], H.ChqDate AS [Cheque Date], H.Remark, H.EntryBy AS [Entry By], H.EntryDate AS [Entry Date],  " & _
                            " H.EntryType AS [Entry Type], H.EntryStatus AS [Entry Status], H.ApproveBy AS [Approve By], H.ApproveDate AS [Approve Date], H.MoveToLog AS [Move To Log],  " & _
                            " H.MoveToLogDate AS [Move To Log Date], H.Status, H.TDSPer AS [TDS %], H.TDSAmt AS [TDS Amount], " & _
                            " D.Div_Name AS Division, SM.Name AS [Site Name]  " & _
                            " FROM  DuesPayment H " & _
                            " LEFT JOIN Division D ON D.Div_Code =H.Div_Code   " & _
                            " LEFT JOIN SiteMast SM ON SM.Code=H.Site_Code   " & _
                            " LEFT JOIN voucher_type Vt ON H.V_Type = vt.V_Type  " & _
                            " Where 1=1 " & mCondStr
        AgL.PubFindQryOrdBy = "[Entry Date]"
    End Sub


    Private Sub FrmSaleOrder_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As System.Data.SqlClient.SqlConnection, ByVal Cmd As System.Data.SqlClient.SqlCommand) Handles Me.BaseEvent_Save_InTrans
        mQry = " Update DuesPayment " & _
                " SET  " & _
                " TransactionType = " & AgL.Chk_Text(IIf(mTransactionType = TransactionType.DebitNote, "Debit Note", "Credit Note")) & ", " & _
                " ManualRefNo = " & AgL.Chk_Text(TxtManualRefNo.Text) & " " & _
                " Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " Delete From DuesPaymentDetail Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " INSERT INTO DuesPaymentDetail(DocID, Sr, TransactionType, SubCode, " & _
                " CurrBalance, PaidAmount, CashBankAc, Remark) " & _
                " VALUES('" & mSearchCode & "',	1,	" & _
                " " & AgL.Chk_Text(IIf(mTransactionType = TransactionType.DebitNote, "Debit Note", "Credit Note")) & ",	" & _
                " " & AgL.Chk_Text(TxtSubCode.AgSelectedValue) & ",	" & _
                " " & Val(TxtCurrBalance.Text) & ",	" & Val(TxtPaidAmount.Text) & ", " & _
                " " & AgL.Chk_Text(TxtCashBankAc.AgSelectedValue) & ", " & AgL.Chk_Text(TxtRemarks.Text) & ") "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        AccountPosting(SearchCode, Conn, Cmd)
    End Sub

    Private Sub FrmSaleOrder_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim DsTemp As DataSet

        mQry = "Select H.* " & _
                " From DuesPayment H " & _
                " Where H.DocID='" & SearchCode & "'"
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                TxtManualRefNo.Text = AgL.XNull(.Rows(0)("ManualRefNo"))
                TxtSubCode.AgSelectedValue = AgL.XNull(.Rows(0)("SubCode"))
                TxtCurrBalance.Text = Format(AgL.VNull(.Rows(0)("CurrBalance")), "0.00")
                TxtPaidAmount.Text = Format(AgL.VNull(.Rows(0)("PaidAmount")), "0.00")
                TxtCashBankAc.AgSelectedValue = AgL.XNull(.Rows(0)("CashBankAc"))
                TxtRemarks.Text = AgL.XNull(.Rows(0)("Remark"))

                FFillEnviro(TxtV_Type.AgSelectedValue)
            End If
        End With

        mQry = "Select H.* " & _
                " From DuesPaymentDetail H " & _
                " Where H.DocID='" & SearchCode & "'"
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                TxtSubCode.AgSelectedValue = AgL.XNull(.Rows(0)("SubCode"))
                TxtCurrBalance.Text = Format(AgL.VNull(.Rows(0)("CurrBalance")), "0.00")
                TxtPaidAmount.Text = Format(AgL.VNull(.Rows(0)("PaidAmount")), "0.00")
                TxtCashBankAc.AgSelectedValue = AgL.XNull(.Rows(0)("CashBankAc"))
                TxtRemarks.Text = AgL.XNull(.Rows(0)("Remark"))
            End If
        End With
    End Sub

    Private Sub FrmSaleOrder_BaseFunction_FIniList() Handles Me.BaseFunction_FIniList
        mQry = " SELECT Sg.SubCode AS Code, Sg.DispName AS Name " & _
                " FROM SubGroup Sg  Order By Sg.DispName"
        TxtSubCode.AgHelpDataSet(, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)


        mQry = " SELECT Sg.SubCode AS Code , Sg.DispName AS Name, Sg.Nature " & _
                " FROM SubGroup Sg "

        '" WHERE Sg.Nature IN ('Bank', 'Direct', 'Indirect')"
        TxtCashBankAc.AgHelpDataSet(0, TabControl1.Top + TP1.Top, TabControl1.Left + TP1.Left) = AgL.FillData(mQry, AgL.GCn)
    End Sub

    Private Sub FrmSaleOrder_BaseFunction_Calculation() Handles Me.BaseFunction_Calculation
        If AgL.StrCmp(Topctrl1.Mode, "Browse") Then Exit Sub
    End Sub

    Private Sub FrmSaleOrder_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        Dim I As Integer = 0
        If AgL.RequiredField(TxtSubCode, LblSUbCode.Text) Then passed = False : Exit Sub
    End Sub

    Private Sub FrmSaleOrder_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
    End Sub

    Public Function FGetLedgerBalance(ByVal StrSubCode As String, ByVal V_Date As String, ByVal Site_Code As String) As Double
        Dim DblRtn As Double
        Dim DTTemp As DataTable

        Try
            DTTemp = AgL.FillData("Select (IsNull(Sum(LG.AmtDr),0)-IsNull(Sum(LG.AmtCr),0)) As Balance From Ledger LG Where LG.SubCode='" & StrSubCode & "' And LG.Site_Code='" & Site_Code & "' And LG.V_Date <= '" & V_Date & "' ", AgL.GCn).Tables(0)
            DblRtn = DTTemp.Rows(0).Item("Balance")
            DTTemp.Dispose()
        Catch ex As Exception
            DblRtn = 0
        End Try
        DTTemp = Nothing
        FGetLedgerBalance = DblRtn
    End Function

    Private Sub Txt_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtSubCode.Validating, TxtCashBankAc.Validating, TxtCurrBalance.Validating, TxtDocId.Validating, TxtPaidAmount.Validating
        Dim DrTemp As DataRow() = Nothing
        Dim bConStr$ = ""
        Try
            Select Case sender.NAME
                Case TxtSubCode.Name
                    If sender.Text <> "" Then
                        TxtCurrBalance.Text = FGetLedgerBalance(sender.tag, TxtV_Date.Text, TxtSite_Code.AgSelectedValue)
                    End If
            End Select
            Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub TempPayment_BaseFunction_DispText() Handles Me.BaseFunction_DispText
        TxtCurrBalance.Enabled = False
    End Sub

    Private Sub TxtAmount_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtSubCode.Validating, TxtV_Type.Validating
        Dim DrTemp As DataRow() = Nothing
        Try
            Select Case sender.Name
                Case TxtSubCode.Name
                Case TxtV_Type.Name
                    FFillEnviro(TxtV_Type.AgSelectedValue)
            End Select
            Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub TempPayment_BaseEvent_Topctrl_tbAdd() Handles Me.BaseEvent_Topctrl_tbAdd
        FFillEnviro(TxtV_Type.AgSelectedValue)
        If DtDuesPaymentEnviro.Rows.Count > 0 Then
            If mTransactionType = TransactionType.DebitNote Then TxtCashBankAc.AgSelectedValue = AgL.XNull(DtDuesPaymentEnviro.Rows(0)("DebitNoteAc"))
            If mTransactionType = TransactionType.CreditNote Then TxtCashBankAc.AgSelectedValue = AgL.XNull(DtDuesPaymentEnviro.Rows(0)("CreditNoteAc"))
        End If
        TxtManualRefNo.Text = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "DuesPayment", TxtV_Type.AgSelectedValue, TxtV_Date.Text, AgL.PubDivCode, AgL.PubSiteCode, AgTemplate.ClsMain.ManualRefType.Max)
        TxtSubCode.Focus()
    End Sub

    Private Sub FFillEnviro(ByVal V_Type As String)
        mQry = "Select * from DuesPaymentEnviro Where V_Type = '" & V_Type & "'"
        DtDuesPaymentEnviro = AgL.FillData(mQry, AgL.GcnRead).Tables(0)

        If DtDuesPaymentEnviro.Rows.Count = 0 Then
            MsgBox("Please set environment settings of " & TxtV_Type.Text)
        End If
    End Sub

    Private Sub FrmPaymentReceipt_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AgL.WinSetting(Me, 332, 885)
    End Sub

    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub

    Private Sub Topctrl1_tbSite() Handles Topctrl1.tbSite
        Dim FrmObj As Form
        Dim DTUP As New DataTable

        FrmObj = New FrmDuesPaymentEnviro(Topctrl1.Tag, DTUP)
        CType(FrmObj, FrmDuesPaymentEnviro).EntryNCat = "'" + Me.EntryNCat + "'"
        If FrmObj IsNot Nothing Then
            FrmObj.MdiParent = Me.MdiParent
            FrmObj.Show()
            FrmObj = Nothing
        End If
    End Sub

    Private Sub TxtItemCategory_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtRemarks.KeyDown
        If e.KeyCode = Keys.Enter Then
            If MsgBox("Do you want to save?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "Save") = MsgBoxResult.Yes Then
                Topctrl1.FButtonClick(13)
            End If
        End If
    End Sub
End Class
