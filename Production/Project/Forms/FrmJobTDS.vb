Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data.SQLite
Public Class FrmJobTDS
    Inherits AgTemplate.TempTransaction
    Dim mQry$

    Protected Const ColSNo As String = "S.No."
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Protected Const Col1SubCode As String = "Party"
    Protected Const Col1TDSAdvise As String = "TDS Advise"
    Protected Const Col1TDSAmount As String = "TDS Amount"
    Protected WithEvents TxtCostCenter As AgControls.AgTextBox
    Protected Const Col1Remark As String = "Remark"


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
        Me.Pnl2 = New System.Windows.Forms.Panel
        Me.TxtRemarks = New AgControls.AgTextBox
        Me.Label30 = New System.Windows.Forms.Label
        Me.LblPaymentDetail = New System.Windows.Forms.LinkLabel
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.LblTotalCurrentBalance = New System.Windows.Forms.Label
        Me.LblTotalCurrentBalanceText = New System.Windows.Forms.Label
        Me.LblTotalAmount = New System.Windows.Forms.Label
        Me.LblTotalAmountText = New System.Windows.Forms.Label
        Me.LblNetAmount = New System.Windows.Forms.Label
        Me.LblNetAmtText = New System.Windows.Forms.Label
        Me.LblTotalDiscount = New System.Windows.Forms.Label
        Me.LblTotalDiscountText = New System.Windows.Forms.Label
        Me.TxtPaymentFor = New AgControls.AgTextBox
        Me.LblAdvancePaymentFor = New System.Windows.Forms.Label
        Me.TxtProcess = New AgControls.AgTextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.TxtManualRefNo = New AgControls.AgTextBox
        Me.TxtAcNature = New AgControls.AgTextBox
        Me.TxtCostCenter = New AgControls.AgTextBox
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
        Me.LblV_No.Location = New System.Drawing.Point(485, 34)
        Me.LblV_No.Size = New System.Drawing.Size(84, 16)
        Me.LblV_No.Tag = ""
        Me.LblV_No.Text = "Payment No."
        '
        'TxtV_No
        '
        Me.TxtV_No.AgSelectedValue = ""
        Me.TxtV_No.BackColor = System.Drawing.Color.White
        Me.TxtV_No.Location = New System.Drawing.Point(809, 28)
        Me.TxtV_No.Size = New System.Drawing.Size(123, 18)
        Me.TxtV_No.TabIndex = 3
        Me.TxtV_No.Tag = ""
        Me.TxtV_No.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.TxtV_No.Visible = False
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(293, 39)
        Me.Label2.Tag = ""
        '
        'LblV_Date
        '
        Me.LblV_Date.BackColor = System.Drawing.Color.Transparent
        Me.LblV_Date.Location = New System.Drawing.Point(196, 34)
        Me.LblV_Date.Size = New System.Drawing.Size(91, 16)
        Me.LblV_Date.Tag = ""
        Me.LblV_Date.Text = "Payment Date"
        '
        'LblV_TypeReq
        '
        Me.LblV_TypeReq.Location = New System.Drawing.Point(577, 19)
        Me.LblV_TypeReq.Tag = ""
        '
        'TxtV_Date
        '
        Me.TxtV_Date.AgSelectedValue = ""
        Me.TxtV_Date.BackColor = System.Drawing.Color.White
        Me.TxtV_Date.Location = New System.Drawing.Point(309, 33)
        Me.TxtV_Date.Size = New System.Drawing.Size(170, 18)
        Me.TxtV_Date.TabIndex = 2
        Me.TxtV_Date.Tag = ""
        '
        'LblV_Type
        '
        Me.LblV_Type.Location = New System.Drawing.Point(485, 15)
        Me.LblV_Type.Size = New System.Drawing.Size(91, 16)
        Me.LblV_Type.Tag = ""
        Me.LblV_Type.Text = "Payment Type"
        '
        'TxtV_Type
        '
        Me.TxtV_Type.AgSelectedValue = ""
        Me.TxtV_Type.BackColor = System.Drawing.Color.White
        Me.TxtV_Type.Location = New System.Drawing.Point(593, 13)
        Me.TxtV_Type.Size = New System.Drawing.Size(193, 18)
        Me.TxtV_Type.TabIndex = 1
        Me.TxtV_Type.Tag = ""
        '
        'LblSite_CodeReq
        '
        Me.LblSite_CodeReq.Location = New System.Drawing.Point(293, 19)
        Me.LblSite_CodeReq.Tag = ""
        '
        'LblSite_Code
        '
        Me.LblSite_Code.BackColor = System.Drawing.Color.Transparent
        Me.LblSite_Code.Location = New System.Drawing.Point(196, 14)
        Me.LblSite_Code.Size = New System.Drawing.Size(87, 16)
        Me.LblSite_Code.Tag = ""
        Me.LblSite_Code.Text = "Branch Name"
        '
        'TxtSite_Code
        '
        Me.TxtSite_Code.AgSelectedValue = ""
        Me.TxtSite_Code.BackColor = System.Drawing.Color.White
        Me.TxtSite_Code.Location = New System.Drawing.Point(309, 13)
        Me.TxtSite_Code.Size = New System.Drawing.Size(170, 18)
        Me.TxtSite_Code.TabIndex = 0
        Me.TxtSite_Code.Tag = ""
        '
        'LblDocId
        '
        Me.LblDocId.Tag = ""
        '
        'LblPrefix
        '
        Me.LblPrefix.Location = New System.Drawing.Point(832, 10)
        Me.LblPrefix.Tag = ""
        Me.LblPrefix.Visible = False
        '
        'TabControl1
        '
        Me.TabControl1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl1.Location = New System.Drawing.Point(-3, 18)
        Me.TabControl1.Size = New System.Drawing.Size(991, 155)
        Me.TabControl1.TabIndex = 0
        '
        'TP1
        '
        Me.TP1.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.TP1.Controls.Add(Me.TxtCostCenter)
        Me.TP1.Controls.Add(Me.TxtAcNature)
        Me.TP1.Controls.Add(Me.TxtManualRefNo)
        Me.TP1.Controls.Add(Me.TxtProcess)
        Me.TP1.Controls.Add(Me.Label4)
        Me.TP1.Controls.Add(Me.Label5)
        Me.TP1.Controls.Add(Me.TxtPaymentFor)
        Me.TP1.Controls.Add(Me.LblAdvancePaymentFor)
        Me.TP1.Controls.Add(Me.TxtRemarks)
        Me.TP1.Controls.Add(Me.Label30)
        Me.TP1.Location = New System.Drawing.Point(4, 22)
        Me.TP1.Size = New System.Drawing.Size(983, 129)
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
        Me.TP1.Controls.SetChildIndex(Me.LblAdvancePaymentFor, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtPaymentFor, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label5, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label4, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtProcess, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtManualRefNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtAcNature, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtCostCenter, 0)
        '
        'Topctrl1
        '
        Me.Topctrl1.Size = New System.Drawing.Size(984, 41)
        Me.Topctrl1.TabIndex = 2
        '
        'Pnl2
        '
        Me.Pnl2.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Pnl2.Location = New System.Drawing.Point(7, 197)
        Me.Pnl2.Name = "Pnl2"
        Me.Pnl2.Size = New System.Drawing.Size(969, 342)
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
        Me.TxtRemarks.Location = New System.Drawing.Point(309, 73)
        Me.TxtRemarks.MaxLength = 255
        Me.TxtRemarks.Name = "TxtRemarks"
        Me.TxtRemarks.Size = New System.Drawing.Size(477, 18)
        Me.TxtRemarks.TabIndex = 8
        '
        'Label30
        '
        Me.Label30.AutoSize = True
        Me.Label30.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label30.Location = New System.Drawing.Point(196, 74)
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
        Me.LblPaymentDetail.Location = New System.Drawing.Point(7, 176)
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
        Me.Panel1.Controls.Add(Me.LblTotalCurrentBalance)
        Me.Panel1.Controls.Add(Me.LblTotalCurrentBalanceText)
        Me.Panel1.Controls.Add(Me.LblTotalAmount)
        Me.Panel1.Controls.Add(Me.LblTotalAmountText)
        Me.Panel1.Controls.Add(Me.LblNetAmount)
        Me.Panel1.Controls.Add(Me.LblNetAmtText)
        Me.Panel1.Controls.Add(Me.LblTotalDiscount)
        Me.Panel1.Controls.Add(Me.LblTotalDiscountText)
        Me.Panel1.Location = New System.Drawing.Point(7, 539)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(969, 23)
        Me.Panel1.TabIndex = 734
        '
        'LblTotalCurrentBalance
        '
        Me.LblTotalCurrentBalance.AutoSize = True
        Me.LblTotalCurrentBalance.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalCurrentBalance.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalCurrentBalance.Location = New System.Drawing.Point(135, 4)
        Me.LblTotalCurrentBalance.Name = "LblTotalCurrentBalance"
        Me.LblTotalCurrentBalance.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalCurrentBalance.TabIndex = 675
        Me.LblTotalCurrentBalance.Text = "."
        Me.LblTotalCurrentBalance.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTotalCurrentBalanceText
        '
        Me.LblTotalCurrentBalanceText.AutoSize = True
        Me.LblTotalCurrentBalanceText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalCurrentBalanceText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalCurrentBalanceText.Location = New System.Drawing.Point(10, 4)
        Me.LblTotalCurrentBalanceText.Name = "LblTotalCurrentBalanceText"
        Me.LblTotalCurrentBalanceText.Size = New System.Drawing.Size(119, 16)
        Me.LblTotalCurrentBalanceText.TabIndex = 674
        Me.LblTotalCurrentBalanceText.Text = "Current Balance :"
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
        'LblNetAmount
        '
        Me.LblNetAmount.AutoSize = True
        Me.LblNetAmount.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblNetAmount.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblNetAmount.Location = New System.Drawing.Point(879, 3)
        Me.LblNetAmount.Name = "LblNetAmount"
        Me.LblNetAmount.Size = New System.Drawing.Size(12, 16)
        Me.LblNetAmount.TabIndex = 671
        Me.LblNetAmount.Text = "."
        Me.LblNetAmount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblNetAmtText
        '
        Me.LblNetAmtText.AutoSize = True
        Me.LblNetAmtText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblNetAmtText.ForeColor = System.Drawing.Color.Maroon
        Me.LblNetAmtText.Location = New System.Drawing.Point(774, 3)
        Me.LblNetAmtText.Name = "LblNetAmtText"
        Me.LblNetAmtText.Size = New System.Drawing.Size(90, 16)
        Me.LblNetAmtText.TabIndex = 669
        Me.LblNetAmtText.Text = "Net Amount :"
        '
        'LblTotalDiscount
        '
        Me.LblTotalDiscount.AutoSize = True
        Me.LblTotalDiscount.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalDiscount.ForeColor = System.Drawing.SystemColors.ControlDarkDark
        Me.LblTotalDiscount.Location = New System.Drawing.Point(647, 3)
        Me.LblTotalDiscount.Name = "LblTotalDiscount"
        Me.LblTotalDiscount.Size = New System.Drawing.Size(12, 16)
        Me.LblTotalDiscount.TabIndex = 668
        Me.LblTotalDiscount.Text = "."
        Me.LblTotalDiscount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'LblTotalDiscountText
        '
        Me.LblTotalDiscountText.AutoSize = True
        Me.LblTotalDiscountText.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTotalDiscountText.ForeColor = System.Drawing.Color.Maroon
        Me.LblTotalDiscountText.Location = New System.Drawing.Point(534, 3)
        Me.LblTotalDiscountText.Name = "LblTotalDiscountText"
        Me.LblTotalDiscountText.Size = New System.Drawing.Size(105, 16)
        Me.LblTotalDiscountText.TabIndex = 667
        Me.LblTotalDiscountText.Text = "Total Discount :"
        '
        'TxtPaymentFor
        '
        Me.TxtPaymentFor.AgAllowUserToEnableMasterHelp = False
        Me.TxtPaymentFor.AgLastValueTag = Nothing
        Me.TxtPaymentFor.AgLastValueText = Nothing
        Me.TxtPaymentFor.AgMandatory = False
        Me.TxtPaymentFor.AgMasterHelp = False
        Me.TxtPaymentFor.AgNumberLeftPlaces = 0
        Me.TxtPaymentFor.AgNumberNegetiveAllow = False
        Me.TxtPaymentFor.AgNumberRightPlaces = 0
        Me.TxtPaymentFor.AgPickFromLastValue = False
        Me.TxtPaymentFor.AgRowFilter = ""
        Me.TxtPaymentFor.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtPaymentFor.AgSelectedValue = Nothing
        Me.TxtPaymentFor.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtPaymentFor.AgValueType = AgControls.AgTextBox.TxtValueType.Date_Value
        Me.TxtPaymentFor.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtPaymentFor.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPaymentFor.Location = New System.Drawing.Point(593, 53)
        Me.TxtPaymentFor.MaxLength = 10
        Me.TxtPaymentFor.Name = "TxtPaymentFor"
        Me.TxtPaymentFor.Size = New System.Drawing.Size(193, 18)
        Me.TxtPaymentFor.TabIndex = 6
        '
        'LblAdvancePaymentFor
        '
        Me.LblAdvancePaymentFor.AutoSize = True
        Me.LblAdvancePaymentFor.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblAdvancePaymentFor.Location = New System.Drawing.Point(485, 54)
        Me.LblAdvancePaymentFor.Name = "LblAdvancePaymentFor"
        Me.LblAdvancePaymentFor.Size = New System.Drawing.Size(83, 16)
        Me.LblAdvancePaymentFor.TabIndex = 748
        Me.LblAdvancePaymentFor.Text = "Payment For"
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
        Me.TxtProcess.Location = New System.Drawing.Point(309, 53)
        Me.TxtProcess.MaxLength = 20
        Me.TxtProcess.Name = "TxtProcess"
        Me.TxtProcess.Size = New System.Drawing.Size(170, 18)
        Me.TxtProcess.TabIndex = 4
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(196, 54)
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
        Me.Label5.Location = New System.Drawing.Point(293, 59)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(10, 7)
        Me.Label5.TabIndex = 770
        Me.Label5.Text = "Ä"
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
        Me.TxtManualRefNo.Location = New System.Drawing.Point(593, 33)
        Me.TxtManualRefNo.MaxLength = 50
        Me.TxtManualRefNo.Name = "TxtManualRefNo"
        Me.TxtManualRefNo.Size = New System.Drawing.Size(193, 18)
        Me.TxtManualRefNo.TabIndex = 3
        '
        'TxtAcNature
        '
        Me.TxtAcNature.AgAllowUserToEnableMasterHelp = False
        Me.TxtAcNature.AgLastValueTag = Nothing
        Me.TxtAcNature.AgLastValueText = Nothing
        Me.TxtAcNature.AgMandatory = False
        Me.TxtAcNature.AgMasterHelp = False
        Me.TxtAcNature.AgNumberLeftPlaces = 0
        Me.TxtAcNature.AgNumberNegetiveAllow = False
        Me.TxtAcNature.AgNumberRightPlaces = 0
        Me.TxtAcNature.AgPickFromLastValue = False
        Me.TxtAcNature.AgRowFilter = ""
        Me.TxtAcNature.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtAcNature.AgSelectedValue = Nothing
        Me.TxtAcNature.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtAcNature.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtAcNature.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtAcNature.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtAcNature.Location = New System.Drawing.Point(811, 53)
        Me.TxtAcNature.MaxLength = 255
        Me.TxtAcNature.Name = "TxtAcNature"
        Me.TxtAcNature.Size = New System.Drawing.Size(103, 18)
        Me.TxtAcNature.TabIndex = 774
        Me.TxtAcNature.Text = "TxtAcNature"
        Me.TxtAcNature.Visible = False
        '
        'TxtCostCenter
        '
        Me.TxtCostCenter.AgAllowUserToEnableMasterHelp = False
        Me.TxtCostCenter.AgLastValueTag = Nothing
        Me.TxtCostCenter.AgLastValueText = Nothing
        Me.TxtCostCenter.AgMandatory = False
        Me.TxtCostCenter.AgMasterHelp = False
        Me.TxtCostCenter.AgNumberLeftPlaces = 8
        Me.TxtCostCenter.AgNumberNegetiveAllow = False
        Me.TxtCostCenter.AgNumberRightPlaces = 2
        Me.TxtCostCenter.AgPickFromLastValue = False
        Me.TxtCostCenter.AgRowFilter = ""
        Me.TxtCostCenter.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtCostCenter.AgSelectedValue = Nothing
        Me.TxtCostCenter.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtCostCenter.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtCostCenter.BackColor = System.Drawing.Color.PowderBlue
        Me.TxtCostCenter.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtCostCenter.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtCostCenter.Location = New System.Drawing.Point(811, 77)
        Me.TxtCostCenter.MaxLength = 20
        Me.TxtCostCenter.Name = "TxtCostCenter"
        Me.TxtCostCenter.Size = New System.Drawing.Size(98, 18)
        Me.TxtCostCenter.TabIndex = 775
        Me.TxtCostCenter.Text = "TxtCostCenter"
        Me.TxtCostCenter.Visible = False
        Me.TxtCostCenter.WordWrap = False
        '
        'FrmJobTDS
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.BackColor = System.Drawing.SystemColors.ButtonShadow
        Me.ClientSize = New System.Drawing.Size(984, 622)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.LblPaymentDetail)
        Me.Controls.Add(Me.Pnl2)
        Me.Name = "FrmJobTDS"
        Me.Text = "Finishing Payment"
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
    Protected WithEvents LblNetAmount As System.Windows.Forms.Label
    Protected WithEvents LblNetAmtText As System.Windows.Forms.Label
    Protected WithEvents LblTotalDiscount As System.Windows.Forms.Label
    Protected WithEvents LblTotalDiscountText As System.Windows.Forms.Label
    Protected WithEvents LblTotalAmount As System.Windows.Forms.Label
    Protected WithEvents LblTotalAmountText As System.Windows.Forms.Label
    Protected WithEvents LblTotalCurrentBalance As System.Windows.Forms.Label
    Protected WithEvents LblTotalCurrentBalanceText As System.Windows.Forms.Label
    Protected WithEvents TxtPaymentFor As AgControls.AgTextBox
    Protected WithEvents LblAdvancePaymentFor As System.Windows.Forms.Label
    Protected WithEvents TxtProcess As AgControls.AgTextBox
    Protected WithEvents Label4 As System.Windows.Forms.Label
    Protected WithEvents Label5 As System.Windows.Forms.Label
    Protected WithEvents TxtManualRefNo As AgControls.AgTextBox
    Protected WithEvents TxtAcNature As AgControls.AgTextBox
#End Region

    Private Sub FrmFinishingPaymentMultipleParty_BaseEvent_ApproveDeletion_InTrans(ByVal SearchCode As String, ByVal Conn As SqliteConnection, ByVal Cmd As SqliteCommand) Handles Me.BaseEvent_ApproveDeletion_InTrans
        mQry = "Delete from TdsLedger Where DocID = '" & mInternalCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        mQry = "Delete from Ledger Where DocID = '" & mInternalCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    End Sub

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
                " Where IFNull(IsDeleted,0) = 0 " & mCondStr & "  Order By H.V_Date, H.V_No "
        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub FrmSaleOrder_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mCondStr$

        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) &
                        " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "H.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat In ('" & EntryNCat & "')"

        AgL.PubFindQry = "SELECT H.DocId as SearchCode, Vt.Description AS [Payment_Type], " &
                            " H.V_Date AS [Payment_Date], H.ManualRefNo AS [Payment_No] " &
                            " FROM DuesPayment H " &
                            " LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type " &
                            " Where 1=1 " & mCondStr

        AgL.PubFindQryOrdBy = "[Entry Date]"
    End Sub

    Private Sub FrmSaleOrder_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl1, Col1SubCode, 300, 5, "Job Worker", True, False)
            .AddAgNumberColumn(Dgl1, Col1TDSAdvise, 70, 5, 2, False, Col1TDSAdvise, True, True)
            .AddAgNumberColumn(Dgl1, Col1TDSAmount, 70, 8, 2, False, Col1TDSAmount, True, False)
            .AddAgTextColumn(Dgl1, Col1Remark, 200, 255, Col1Remark, True, False)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl2)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 35
        Dgl1.AgSkipReadOnlyColumns = True

        AgCL.GridSetiingShowXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1, False)
    End Sub

    Private Sub FrmSaleOrder_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As SQLiteConnection, ByVal Cmd As SQLiteCommand) Handles Me.BaseEvent_Save_InTrans
        Dim I As Integer, mSr As Integer

        mQry = " Update DuesPayment " &
                " SET  " &
                " TransactionType = 'Payment', " &
                " ManualRefNo = " & AgL.Chk_Text(TxtManualRefNo.Text) & ", " &
                " Process = " & AgL.Chk_Text(TxtProcess.Tag) & ", " &
                " CostCenter = " & AgL.Chk_Text(TxtCostCenter.Tag) & ", " &
                " CurrBalance = " & Val(LblTotalCurrentBalance.Text) & " , " &
                " PaidAmount = " & Val(LblTotalAmount.Text) & " , " &
                " Discount = " & Val(LblTotalDiscount.Text) & " , " &
                " NetAmount = " & Val(LblNetAmount.Text) & " , " &
                " Remark = " & AgL.Chk_Text(TxtRemarks.Text) & " , " &
                " PaymentFor = " & AgL.Chk_Text(TxtPaymentFor.Text) & " " &
                " Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Delete From DuesPaymentDetail Where DocId = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        'Never Try to Serialise Sr in Line Items 
        'As Some other Entry points may updating values to this Search code and Sr
        With Dgl1
            For I = 0 To .RowCount - 1
                If .Item(Col1SubCode, I).Value <> "" Then
                    mSr += 1
                    mQry = "Insert Into DuesPaymentDetail(DocId, Sr, " &
                            " SubCode, PartyName, CurrBalance,  " &
                            " PaidAmount, NetAmount, CashBank, Remark) " &
                            " Values( " &
                            " " & AgL.Chk_Text(mSearchCode) & ", " &
                            " " & mSr & ", " & AgL.Chk_Text(.AgSelectedValue(Col1SubCode, I)) & ", " &
                            " " & AgL.Chk_Text(.Item(Col1SubCode, I).Value) & ", " &
                            " " & Val(.Item(Col1TDSAdvise, I).Value) & ", " &
                            " " & AgL.Chk_Text(.Item(Col1TDSAmount, I).Value) & ", " &
                            " " & Val(.Item(Col1TDSAmount, I).Value) & ", " &
                            " " & AgL.Chk_Text(TxtAcNature.Text) & ", " &
                            " " & AgL.Chk_Text(.Item(Col1Remark, I).Value) & ")"
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                End If
            Next
        End With

        Call AgL.LedgerUnPost(AgL.GCn, AgL.ECmd, mInternalCode)
        AccountPosting()

        mQry = " Delete From TdsLedger Where DocId = '" & mInternalCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mSr = 0
        With Dgl1
            For I = 0 To .Rows.Count - 1
                If .Item(Col1SubCode, I).Value <> "" Then
                    mSr += 1
                    mQry = " INSERT INTO TdsLedger(DocID, V_Type, V_Prefix, V_Date, V_No, Sr, ReferenceNo, Div_Code, " &
                            " Site_Code, SubCode, PaidAmount, Remark) " &
                            " VALUES ('" & mInternalCode & "', " & AgL.Chk_Text(TxtV_Type.AgSelectedValue) & ",  " &
                            " " & AgL.Chk_Text(LblPrefix.Text) & ",	" & AgL.Chk_Text(TxtV_Date.Text) & ",	" &
                            " " & Val(TxtV_No.Text) & ", " & Val(mSr) & ",	" & AgL.Chk_Text(TxtV_Type.AgSelectedValue + TxtV_No.Text.ToString) & ", " &
                            " " & AgL.Chk_Text(TxtDivision.AgSelectedValue) & ", " &
                            " " & AgL.Chk_Text(TxtSite_Code.AgSelectedValue) & ",	" &
                            " " & AgL.Chk_Text(.AgSelectedValue(Col1SubCode, I)) & ", " &
                            " " & Val(.Item(Col1TDSAmount, I).Value) & ", " & AgL.Chk_Text(TxtRemarks.Text) & ") "
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                End If
            Next
        End With


        If AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Or AgL.StrCmp(AgL.PubUserName, "sa") Then
            AgCL.GridSetiingWriteXml(Me.Text & Dgl1.Name & AgL.PubCompCode & AgL.PubDivCode & AgL.PubSiteCode, Dgl1)
        End If
    End Sub

    Private Sub FrmSaleOrder_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim I As Integer

        Dim DsTemp As DataSet

        mQry = "Select H.*, P.Description As ProcessDesc, CCM.Name as CostCenterDesc   " &
                " From DuesPayment H " &
                " LEFT JOIN Process P On H.Process = P.NCat " &
                " LEFT JOIN CostCenterMast CCM  On H.CostCenter = CCM.Code " &
                " Where H.DocID ='" & SearchCode & "'"
        DsTemp = AgL.FillData(mQry, AgL.GCn)
        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                TxtRemarks.Text = AgL.XNull(.Rows(0)("Remark"))
                TxtManualRefNo.Text = AgL.XNull(.Rows(0)("ManualRefNo"))
                TxtProcess.Tag = AgL.XNull(.Rows(0)("Process"))
                TxtProcess.Text = AgL.XNull(.Rows(0)("ProcessDesc"))

                TxtCostCenter.Tag = AgL.XNull(.Rows(0)("CostCenter"))
                TxtCostCenter.Text = AgL.XNull(.Rows(0)("CostCenterDesc"))

                LblTotalCurrentBalance.Text = AgL.VNull(.Rows(0)("CurrBalance"))
                LblTotalAmount.Text = AgL.VNull(.Rows(0)("PaidAmount"))
                LblTotalDiscount.Text = AgL.VNull(.Rows(0)("Discount"))
                LblNetAmount.Text = AgL.VNull(.Rows(0)("NetAmount"))
                TxtPaymentFor.Text = AgL.XNull(.Rows(0)("PaymentFor"))



                'Line Records are showing in Grid
                '-------------------------------------------------------------
                mQry = "Select L.*, Sg1.DispName As CashBankAcName  " &
                        " From DuesPaymentDetail L " &
                        " LEFT JOIN SubGroup Sg1 On L.CashBankAc = Sg1.SubCode " &
                        " Where L.DocId = '" & SearchCode & "' " &
                        " Order By L.Sr"
                DsTemp = AgL.FillData(mQry, AgL.GCn)
                With DsTemp.Tables(0)
                    Dgl1.RowCount = 1
                    Dgl1.Rows.Clear()
                    If .Rows.Count > 0 Then

                        TxtAcNature.Text = AgL.XNull(.Rows(I)("CashBank"))

                        For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                            Dgl1.Rows.Add()
                            Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count - 1
                            Dgl1.Item(Col1SubCode, I).Tag = AgL.XNull(.Rows(I)("SubCode"))
                            Dgl1.Item(Col1SubCode, I).Value = AgL.XNull(.Rows(I)("PartyName"))
                            Dgl1.Item(Col1TDSAdvise, I).Value = AgL.XNull(.Rows(I)("CurrBalance"))
                            Dgl1.Item(Col1TDSAmount, I).Value = AgL.VNull(.Rows(I)("PaidAmount"))
                            Dgl1.Item(Col1Remark, I).Value = AgL.XNull(.Rows(I)("Remark"))
                        Next I
                    End If
                End With
                Calculation()
                '-------------------------------------------------------------
            End If
        End With
    End Sub

    Private Sub Dgl1_EditingControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.EditingControl_KeyDown
        Try
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1SubCode
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1SubCode) Is Nothing Then
                            FCreateHelpSubgroup()
                        End If
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
        Try
            mRowIndex = Dgl1.CurrentCell.RowIndex
            mColumnIndex = Dgl1.CurrentCell.ColumnIndex
            If Dgl1.Item(mColumnIndex, mRowIndex).Value Is Nothing Then Dgl1.Item(mColumnIndex, mRowIndex).Value = ""
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1SubCode
                    Dgl1.Item(Col1TDSAdvise, mRowIndex).Value = FGetLedgerBalance(sender.tag, TxtV_Date.Text, TxtSite_Code.AgSelectedValue, TxtCostCenter.Tag)
            End Select
            Call Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FrmSaleOrder_BaseFunction_Calculation() Handles Me.BaseFunction_Calculation
        Dim I As Integer = 0

        LblTotalCurrentBalance.Text = 0 : LblTotalAmount.Text = 0
        LblTotalDiscount.Text = 0 : LblNetAmount.Text = 0

        With Dgl1
            For I = 0 To Dgl1.RowCount - 1
                If .Item(Col1SubCode, I).Value <> "" Then
                    'Footer Calculation
                    LblTotalCurrentBalance.Text = Val(LblTotalCurrentBalance.Text) + Val(Dgl1.Item(Col1TDSAdvise, I).Value)
                    LblTotalAmount.Text = Val(LblTotalAmount.Text) + Val(Dgl1.Item(Col1TDSAmount, I).Value)
                    LblNetAmount.Text = Val(LblNetAmount.Text) + Val(Dgl1.Item(Col1TDSAmount, I).Value)
                End If
            Next
        End With

        LblTotalCurrentBalance.Text = Format(Val(LblTotalCurrentBalance.Text), "0.00")
        LblTotalAmount.Text = Format(Val(LblTotalAmount.Text), "0.00")
        LblTotalDiscount.Text = Format(Val(LblTotalDiscount.Text), "0.00")
        LblNetAmount.Text = Format(Val(LblNetAmount.Text), "0.00")
    End Sub

    Private Sub FrmSaleOrder_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        Dim I As Integer = 0
        If passed = False Then Exit Sub
        If AgCL.AgIsBlankGrid(Dgl1, Dgl1.Columns(Col1SubCode).Index) Then passed = False : Exit Sub

        If CDate(TxtPaymentFor.Text) > CDate(TxtV_Date.Text) Then
            MsgBox("Payment For Date can not be Greater than Payment Date !") : TxtPaymentFor.Focus() : passed = False : Exit Sub
        End If

        With Dgl1
            If .Item(Col1SubCode, I).Value <> "" Then
                If Val(.Item(Col1TDSAmount, I).Value) = 0 Then
                    MsgBox("Paid Amount Is 0 At Row No. " & .Item(ColSNo, I).Value & "", MsgBoxStyle.Information + MsgBoxStyle.Exclamation)
                    Dgl1.CurrentCell = Dgl1.Item(Col1TDSAmount, I) : Dgl1.Focus()
                    passed = False : Exit Sub
                End If
            End If
        End With
    End Sub

    Private Sub FrmSaleOrder_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
        Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
        LblTotalCurrentBalance.Text = 0 : LblTotalAmount.Text = 0
        LblTotalDiscount.Text = 0 : LblNetAmount.Text = 0
    End Sub

    Private Sub Dgl1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.KeyDown
        If Topctrl1.Mode <> "Browse" Then
            If e.Control And e.KeyCode = Keys.D Then
                sender.CurrentRow.Selected = True
            End If
        End If
        If e.Control Or e.Shift Or e.Alt Then Exit Sub
    End Sub

    Private Sub DGL1_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles Dgl1.RowsAdded, Dgl1.RowsAdded
        sender(ColSNo, e.RowIndex).Value = e.RowIndex + 1
    End Sub

    Public Function FGetLedgerBalance(ByVal StrSubCode As String, ByVal V_Date As String, ByVal Site_Code As String, ByVal CostCenter As String) As Double
        Dim DblRtn As Double
        Dim DTTemp As DataTable
        Try

            Dim mCondStr$ = ""
            Dim mCondStrDebitCredit$ = ""
            Dim strStartDate As String
            Dim strMonthStartDate As String
            Dim dblMonthTdsLimit As Double
            Dim dblYearTdsLimit As Double

            mCondStr = mCondStr & " AND H.JobWorker = '" & StrSubCode & "' "
            mCondStr = mCondStr & " And H.Site_Code = '" & AgL.PubSiteCode & "' "

            mCondStrDebitCredit = mCondStrDebitCredit & " AND L.SubCode = '" & StrSubCode & "' "
            mCondStrDebitCredit = mCondStrDebitCredit & " And L.Site_Code = '" & AgL.PubSiteCode & "' "

            If CDate(TxtPaymentFor.Text) < CDate("01/Apr/13") Then
                strStartDate = "01/Apr/2012"
                dblMonthTdsLimit = 20000
                dblYearTdsLimit = 75000
            Else
                strStartDate = AgL.PubStartDate
                dblMonthTdsLimit = 30000
                dblYearTdsLimit = 75000
            End If

            Dim mDueQry As String = ""
            Dim mDueQry1 As String = ""

            strMonthStartDate = AgL.ConvertMonthStartDateField(TxtPaymentFor.Text)


            mDueQry = " SELECT V1.JobWorker, V1.V_Type, Max(V1.NCatDescription) AS NCatDescription, Sum(V1.DueAmt) AS DueAmt, Sum(V1.DueAmtMonth) AS DueAmtMonth " &
                    " FROM  " &
                    " ( " &
                    " SELECT H.JobWorker, Max(Vt.V_Type) as V_Type, Vt.Description NCatDescription, Sum(L.NetAmount) AS DueAmt,  " &
                    " Sum(CASE WHEN H.V_Date BETWEEN " & AgL.ConvertDate(strMonthStartDate) & " And " & AgL.ConvertDate(TxtPaymentFor.Text) & " THEN L.NetAmount ELSE 0 End) AS DueAmtMonth   " &
                    " FROM JobInvoiceDetail l   " &
                    " LEFT JOIN JobInvoice H ON L.Jobinvoice = H.DocID  " &
                    " LEFT JOIN Voucher_Type Vt ON H.Process = Vt.V_Type   " &
                    " WHERE H.V_Date Between '" & strStartDate & "' And " & AgL.ConvertDate(TxtPaymentFor.Text) & " " &
                    " And H.Div_Code= '" & AgL.PubDivCode & "' " & mCondStr & " " &
                    " GROUP BY H.JobWorker, Vt.Description  " &
                    " UNION ALL  " &
                    " SELECT L.SubCode AS JobWorker, max(L.CostCenter) AS V_Type, Vt.Description AS NCatDescription, -Sum(L.AmtDr) AS DueAmt, " &
                    " Sum(CASE WHEN L.V_Date Between " & AgL.ConvertDate(strMonthStartDate) & " And " & AgL.ConvertDate(TxtPaymentFor.Text) & " THEN -L.AmtDr ELSE 0 End) AS DueAmtMonth " &
                    " FROM Ledger L  " &
                    " LEFT JOIN Voucher_Type Vt  ON L.CostCenter = Vt.V_Type  " &
                    " WHERE L.V_Type IN ('FDEBT','FCRDT') " &
                    " AND L.V_Date Between '" & strStartDate & "' And " & AgL.ConvertDate(TxtPaymentFor.Text) & " " &
                    " And L.DivCode = '" & AgL.PubDivCode & "' " & mCondStrDebitCredit & " " &
                    " GROUP BY L.SubCode,  Vt.Description " &
                    " ) V1 " &
                    " GROUP BY V1.JobWorker, V1.V_Type "

            mDueQry1 = "SELECT V2.JobWorker, sum(V2.DueAmt) AS DueAmt, sum(V2.DueAmtMonth) AS DueAmtMonth " &
                        " FROM  " &
                        " ( " &
                        " SELECT H.JobWorker, Sum(L.NetAmount) AS DueAmt, Sum(CASE WHEN H.V_Date Between " & AgL.ConvertDate(strMonthStartDate) & " And " & AgL.ConvertDate(TxtPaymentFor.Text) & " THEN L.NetAmount ELSE 0 End) AS DueAmtMonth " &
                        " FROM JobInvoiceDetail l   " &
                        " LEFT JOIN JobInvoice H   ON L.Jobinvoice = H.DocID  " &
                        " LEFT JOIN Voucher_Type Vt   ON H.Process = Vt.V_Type   " &
                        " WHERE H.V_Date Between '" & strStartDate & "' And " & AgL.ConvertDate(TxtPaymentFor.Text) & "  " &
                        " " & mCondStr & " " &
                        " GROUP BY H.JobWorker   " &
                        " UNION ALL  " &
                        " SELECT L.SubCode AS JobWorker, -Sum(L.AmtDr) AS DueAmt,  Sum(CASE WHEN L.V_Date Between " & AgL.ConvertDate(strMonthStartDate) & " And " & AgL.ConvertDate(TxtPaymentFor.Text) & " THEN -L.AmtDr ELSE 0 End) AS DueAmtMonth   " &
                        " FROM Ledger L    " &
                        " LEFT JOIN Voucher_Type Vt  ON L.CostCenter = Vt.V_Type  " &
                        " WHERE L.V_Type IN ('FDEBT','FCRDT')   " &
                        " AND L.V_Date Between '" & strStartDate & "' And " & AgL.ConvertDate(TxtPaymentFor.Text) & "   " &
                        " " & mCondStrDebitCredit & " " &
                        " GROUP BY L.SubCode " &
                        " ) V2  " &
                        " GROUP BY V2.JobWorker "


            mQry = " SELECT Due.JobWorker, Due.NCatDescription, S.DispName, S.PAN, Due.DueAmt, Due.DueAmtMonth, (CASE WHEN PDue.DueAmt>" & dblYearTdsLimit & " OR PDue.DueAmtMonth>" & dblMonthTdsLimit & "  Or IFNull(PDed.TdsOnAmt,0) > 0  THEN 1 ELSE 0 END) AS IsTdsApply, Due.DueAmt - IFNull(Ded.TdsOnAmt,0) AS TdsOnAmt, IFNull(TdsCat_Det.Percentage,0) AS Percentage, " &
                    " Round(CASE WHEN (PDue.DueAmt > " & dblYearTdsLimit & " OR PDue.DueAmtMonth > " & dblMonthTdsLimit & " Or IFNull(PDed.TdsOnAmt,0) > 0) THEN (Due.DueAmt-IFNull(Ded.TdsOnAmt,0))*IFNull(TdsCat_Det.Percentage,0)/100 ELSE 0 END,0) AS TdsAmt " &
                    " FROM " &
                    " ( " & mDueQry & " ) AS Due " &
                    " LEFT JOIN  " &
                    " ( " &
                    " SELECT L.SubCode, Sum(L.TdsOnAmt) AS TdsOnAmt, vT.V_Type FROM  " &
                    " DuesPaymentDetail L " &
                    " LEFT JOIN DuesPayment H ON L.DocID = H.DocID  " &
                    " LEFT JOIN Voucher_Type Vt ON H.Process = Vt.V_Type  " &
                    " WHERE IFNull(H.PaymentFor,H.V_Date) between '" & strStartDate & "'  And " & AgL.ConvertDate(TxtPaymentFor.Text) & " " &
                    " And H.Div_Code = '" & AgL.PubDivCode & "' " &
                    " GROUP BY L.SubCode, VT.V_type  " &
                    " ) AS Ded ON Due.JobWorker = Ded.SubCode and Due.V_Type = Ded.V_Type  " &
                    " LEFT JOIN  " &
                    " ( " &
                    " SELECT L.SubCode, Sum(L.TdsOnAmt) AS TdsOnAmt FROM  " &
                    " DuesPaymentDetail L " &
                    " LEFT JOIN DuesPayment H ON L.DocID = H.DocID  " &
                    " WHERE IFNull(H.PaymentFor,H.V_Date) between '" & strStartDate & "' And " & AgL.ConvertDate(TxtPaymentFor.Text) & " " &
                    " GROUP BY L.SubCode  " &
                    " ) AS PDed ON Due.JobWorker = PDed.SubCode " &
                    " Left Join " &
                    "( " & mDueQry1 & " ) AS PDue On Due.JobWorker = PDue.Jobworker " &
                    " LEFT JOIN SubGroup S ON Due.JobWorker = S.SubCode " &
                    " LEFT JOIN TdsCat_Det ON S.TDS_Catg = TdsCat_Det.Code AND S.TdsCat_Description = TdsCat_Det.TdsDesc   " &
                    " WHERE Due.NCatDescription IS NOT Null And (CASE WHEN PDue.DueAmt>" & dblYearTdsLimit & " OR PDue.DueAmtMonth>" & dblMonthTdsLimit & " Or IFNull(PDed.TdsOnAmt,0) > 0 THEN 1 ELSE 0 END)=1 " &
                    " AND Due.DueAmt - IFNull(Ded.TdsOnAmt,0)>0 "


            DTTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
            DblRtn = DTTemp.Rows(0).Item("TdsAmt")
            DTTemp.Dispose()
        Catch ex As Exception
            DblRtn = 0
        End Try
        DTTemp = Nothing
        FGetLedgerBalance = DblRtn
    End Function

    Private Function AccountPosting() As Boolean
        Dim LedgAry() As AgLibrary.ClsMain.LedgRec
        Dim I As Integer, J As Integer = 0
        Dim DsTemp As DataSet = Nothing
        Dim mNarr As String = "", mCommonNarr$ = ""
        Dim mNetAmount As Double, mRoundOff As Double = 0
        Dim mTDSAC As String = ""

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

        ReDim Preserve LedgAry(I)

        mTDSAC = AgL.XNull(AgL.PubDtEnviro.Rows(0)("TdsAc"))

        With Dgl1
            For J = 0 To .Rows.Count - 1
                I = UBound(LedgAry) + 1
                ReDim Preserve LedgAry(I)
                LedgAry(I).SubCode = .AgSelectedValue(Col1SubCode, J)
                LedgAry(I).ContraSub = mTDSAC
                LedgAry(I).AmtCr = 0
                LedgAry(I).AmtDr = Val(.Item(Col1TDSAmount, J).Value)
                'LedgAry(I).ChqNo = .Item(Col1ChqNo, J).Value
                'LedgAry(I).ChqDt = .Item(Col1ChqDate, J).Value
                mNarr = TxtRemarks.Text
                LedgAry(I).Narration = mNarr





                I = UBound(LedgAry) + 1
                ReDim Preserve LedgAry(I)
                LedgAry(I).SubCode = mTDSAC
                LedgAry(I).ContraSub = .AgSelectedValue(Col1SubCode, J)
                LedgAry(I).AmtCr = Val(.Item(Col1TDSAmount, J).Value)
                LedgAry(I).AmtDr = 0
                LedgAry(I).Narration = mNarr
            Next
        End With

        If AgL.PubManageOfflineData Then
            If AgL.LedgerPost(AgL.MidStr(Topctrl1.Mode, 0, 1), LedgAry, AgL.GcnSite, AgL.ECmdSite, mSearchCode, CDate(TxtV_Date.Text), AgL.PubUserName, AgL.PubLoginDate, mCommonNarr, , AgL.GcnSite_ConnectionString) = False Then
                AccountPosting = False : Err.Raise(1, , "Error in Ledger Posting")
            Else
            End If
        End If

        If AgL.LedgerPost(AgL.MidStr(Topctrl1.Mode, 0, 1), LedgAry, AgL.GCn, AgL.ECmd, mInternalCode, CDate(TxtV_Date.Text), AgL.PubUserName, AgL.PubLoginDate, mCommonNarr, , AgL.Gcn_ConnectionString) = False Then
            AccountPosting = False : Err.Raise(1, , "Error in Ledger Posting")
        End If
        GcnRead.Close()
        GcnRead.Dispose()

        mQry = " UPDATE Ledger Set CostCenter = '" & TxtCostCenter.Tag & "' Where DocId = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
    End Function

    Private Sub FrmYarnSKUOpeningStock_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Topctrl1.ChangeAgGridState(Dgl1, False)
        AgL.WinSetting(Me, 650, 990)
    End Sub

    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub

    Private Sub FrmFinishingPaymentMultipleParty_BaseEvent_Topctrl_tbPrn(ByVal SearchCode As String) Handles Me.BaseEvent_Topctrl_tbPrn
        Dim mCrd As New ReportDocument
        Dim ReportView As New AgLibrary.RepView
        Dim DsRep As New DataSet
        Dim RepName As String = "", RepTitle As String = ""
        Try
            Me.Cursor = Cursors.WaitCursor

            AgL.PubReportTitle = TxtProcess.Text & " TDS"
            RepName = "Production_TDS_Print" : RepTitle = TxtProcess.Text & " TDS"

            mQry = " SELECT DP.DocID, DP.V_Type, DP.V_Date, DP.ManualRefNo, DP.NetAmount, DP.V_No, DP.ApproveBy, DP.EntryBy, " &
                    " DPD.DocID, DPD.Sr, DPD.Amount, DPD.SubCode, DPD.PartyName AS PartyDispName, SG.Name AS PartyName, DPD.PartyAddress, DPD.PartyCity, " &
                    " DPD.CurrBalance, DPD.PaidAmount, DPD.Discount, DPD.NetAmount, DPD.CashBank, DPD.CashBankAc, CASE WHEN DPD.CashBank ='Cash' THEN DPD.CashBank ELSE  DPD.ChqNo END AS ChqNo , " &
                    " DPD.ChqDate, DPD.Remark, DPD.TDSPer, DPD.TDSAmt, DPD.TransactionType, DPD.AmtPendingForTds, DPD.TdsOnAmt, " &
                    " DPD.WeavingOrderDocId, JO.ManualRefNo AS orderno, jo.PurjaNo  " &
                    " FROM DuesPayment DP " &
                    " LEFT JOIN DuesPaymentDetail DPD ON DPD.DocID=DP.DocID " &
                    " LEFT JOIN JobOrder JO ON JO.DocID = DPD.WeavingOrderDocId  " &
                    " LEFT JOIN SubGroup SG ON SG.SubCode=DPD.SubCode " &
                    " WHERE DP.DocId = '" & mSearchCode & "'"


            AgL.ADMain = New SQLiteDataAdapter(mQry, AgL.GCn)
            AgL.ADMain.Fill(DsRep)
            AgPL.CreateFieldDefFile1(DsRep, AgL.PubReportPath & "\" & RepName & ".ttx", True)
            mCrd.Load(AgL.PubReportPath & "\" & RepName & ".rpt")
            mCrd.SetDataSource(DsRep.Tables(0))
            CType(ReportView.Controls("CrvReport"), CrystalDecisions.Windows.Forms.CrystalReportViewer).ReportSource = mCrd
            AgPL.Formula_Set(mCrd, RepTitle)
            AgPL.Show_Report(ReportView, "* " & RepTitle & " *", Me.MdiParent)
            Call AgL.LogTableEntry(mSearchCode, Me.Text, "P", AgL.PubMachineName, AgL.PubUserName, AgL.PubLoginDate, AgL.GCn, AgL.ECmd)
        Catch Ex As Exception
            MsgBox(Ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub TxtRemarks_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtRemarks.Validating, TxtV_Type.Validating, TxtPaymentFor.Validating
        Dim DtTemp As DataTable = Nothing
        Dim DrTemp As DataRow() = Nothing
        Try
            Select Case sender.name
                Case TxtV_Type.Name
                    If TxtV_Type.Tag <> "" Then
                        mQry = "Select H.* from Voucher_Type_Settings H Where H.V_Type = '" & TxtV_Type.Tag & "' And H.Div_Code = '" & AgL.PubDivCode & "' And H.Site_Code ='" & AgL.PubSiteCode & "' "
                        DtV_TypeSettings = AgL.FillData(mQry, AgL.GCn).Tables(0)

                        If Topctrl1.Mode = "Add" Then
                            TxtManualRefNo.Text = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "DuesPayment", TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, AgTemplate.ClsMain.ManualRefType.Max)
                        End If

                        TxtProcess.Enabled = False

                        If DtV_TypeSettings.Rows.Count <> 0 Then
                            If CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Process")), Boolean) Then
                                If InStr(",", AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Process"))) <= 0 Then
                                    mQry = "Select P.NCat, P.Description, P.CostCenter, Cm.Name As CostCenterName from Process P LEFT JOIN CostCenterMast Cm On P.CostCenter = Cm.Code Where P.NCat= '" & Replace(AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_Process")), "|", "") & "'  "
                                    DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
                                    If DtTemp.Rows.Count > 0 Then
                                        TxtProcess.Tag = AgL.XNull(DtTemp.Rows(0)("NCat"))
                                        TxtProcess.Text = AgL.XNull(DtTemp.Rows(0)("Description"))
                                        TxtCostCenter.Tag = AgL.XNull(DtTemp.Rows(0)("CostCenter"))
                                        TxtCostCenter.Text = AgL.XNull(DtTemp.Rows(0)("CostCenterName"))
                                        TxtProcess.Enabled = False
                                    End If
                                Else
                                    TxtProcess.Enabled = True
                                End If
                            End If
                        Else
                            MsgBox("Please set Voucher Type Settings Of " & TxtV_Type.Text, MsgBoxStyle.Information)
                            e.Cancel = True : Exit Sub
                        End If
                    End If
                Case TxtPaymentFor.Name
                    If TxtPaymentFor.Text <> "" Then
                        TxtPaymentFor.Text = AgL.RetMonthEndDate(TxtPaymentFor.Text)
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
                strCond += " And CharIndex('|' + Sg.GroupCode + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_AcGroup")) & "') > 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_AcGroup")) <> "" Then
                strCond += " And CharIndex('|' + Sg.GroupCode + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterExclude_AcGroup")) & "') <= 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_SubgroupDivision")) <> "" Then
                'strCond += " And CharIndex('|' + SG.DivisionList + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_subGroupDivision")) & "') > 0 "
                strCond += " And CharIndex('" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_subGroupDivision")) & "', SG.DivisionList ) > 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_SubgroupSite")) <> "" Then
                strCond += " And CharIndex('|' + Sg.Site_Code + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_subGroupSite")) & "') > 0 "
            End If
        End If

        mQry = " SELECT J.SubCode AS Code, Sg.Name AS JobWorker " &
                " FROM JobWorker J " &
                " LEFT JOIN JobWorkerProcess Jwp On J.SubCode = Jwp.SubCode  " &
                " LEFT JOIN SubGroup Sg ON J.SubCode = Sg.SubCode " &
                " Where IFNull(Sg.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " &
                " And Jwp.Process = '" & TxtProcess.Tag & "' " & strCond
        Dgl1.AgHelpDataSet(Col1SubCode) = AgL.FillData(mQry, AgL.GCn)
    End Sub

    Private Sub FrmRugFinishingPayment_BaseEvent_Topctrl_tbRef() Handles Me.BaseEvent_Topctrl_tbRef
        If Dgl1.AgHelpDataSet(Col1SubCode) IsNot Nothing Then Dgl1.AgHelpDataSet(Col1SubCode) = Nothing
    End Sub
End Class
