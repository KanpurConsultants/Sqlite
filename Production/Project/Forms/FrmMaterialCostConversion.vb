Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data.SQLite

Public Class FrmMaterialCostConversion
    Inherits AgTemplate.TempTransaction
    Dim mQry$

    Protected WithEvents Dgl1 As New AgControls.AgDataGrid
    Protected Const ColSNo As String = "S.No."
    Protected Const Col1CostCenter As String = "Cost Center"
    Protected Const Col1JobWorker As String = "Job Worker"
    Protected Const Col1Item As String = "Item"
    Protected Const Col1Qty As String = "Qty"
    Protected Const Col1Unit As String = "Unit"
    Protected Const Col1MeasurePerPcs As String = "Measure Per Pcs"
    Protected Const Col1TotalMeasure As String = "Total Measure"
    Protected Const Col1MeasureUnit As String = "Measure Unit"
    Protected Const Col1JobOrder As String = "Job Order No"
    Protected Const Col1Rate As String = "Rate"
    Protected Const Col1Amount As String = "Amount"
    Protected Const Col1Remark As String = "Remark"

    Protected Const AgCalc_NetAmount As String = "NAMT"
    Public WithEvents Label5 As System.Windows.Forms.Label
    Dim mBillPosting As AgTemplate.ClsMain.JobReceiveBillPosting = AgTemplate.ClsMain.JobReceiveBillPosting.Dues

    Public Sub New(ByVal StrUPVar As String, ByVal DTUP As DataTable)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Topctrl1.FSetParent(Me, StrUPVar, DTUP)
        Topctrl1.SetDisp(True)

        Me.EntryNCat = AgTemplate.ClsMain.Temp_NCat.JobRateConversion

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
        Me.TxtProcess = New AgControls.AgTextBox
        Me.LblProcess = New System.Windows.Forms.Label
        Me.LblJobReceiveDetail = New System.Windows.Forms.LinkLabel
        Me.TxtForJobOrder = New AgControls.AgTextBox
        Me.LblForJobOrder = New System.Windows.Forms.Label
        Me.BtnFillJobOrder = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
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
        Me.LblV_No.Location = New System.Drawing.Point(793, 48)
        Me.LblV_No.Size = New System.Drawing.Size(101, 16)
        Me.LblV_No.Tag = ""
        Me.LblV_No.Text = "Job Receive No."
        Me.LblV_No.Visible = False
        '
        'TxtV_No
        '
        Me.TxtV_No.AgSelectedValue = ""
        Me.TxtV_No.BackColor = System.Drawing.Color.White
        Me.TxtV_No.Location = New System.Drawing.Point(831, 76)
        Me.TxtV_No.Size = New System.Drawing.Size(125, 18)
        Me.TxtV_No.TabIndex = 3
        Me.TxtV_No.Tag = ""
        Me.TxtV_No.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.TxtV_No.Visible = False
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(309, 40)
        Me.Label2.Tag = ""
        '
        'LblV_Date
        '
        Me.LblV_Date.BackColor = System.Drawing.Color.Transparent
        Me.LblV_Date.Location = New System.Drawing.Point(197, 34)
        Me.LblV_Date.Size = New System.Drawing.Size(102, 16)
        Me.LblV_Date.Tag = ""
        Me.LblV_Date.Text = "Conversion Date"
        '
        'LblV_TypeReq
        '
        Me.LblV_TypeReq.Location = New System.Drawing.Point(542, 20)
        Me.LblV_TypeReq.Tag = ""
        '
        'TxtV_Date
        '
        Me.TxtV_Date.AgSelectedValue = ""
        Me.TxtV_Date.BackColor = System.Drawing.Color.White
        Me.TxtV_Date.Location = New System.Drawing.Point(327, 34)
        Me.TxtV_Date.TabIndex = 2
        Me.TxtV_Date.Tag = ""
        '
        'LblV_Type
        '
        Me.LblV_Type.Location = New System.Drawing.Point(433, 16)
        Me.LblV_Type.Size = New System.Drawing.Size(102, 16)
        Me.LblV_Type.Tag = ""
        Me.LblV_Type.Text = "Conversion Type"
        '
        'TxtV_Type
        '
        Me.TxtV_Type.AgSelectedValue = ""
        Me.TxtV_Type.BackColor = System.Drawing.Color.White
        Me.TxtV_Type.Location = New System.Drawing.Point(558, 14)
        Me.TxtV_Type.Size = New System.Drawing.Size(207, 18)
        Me.TxtV_Type.TabIndex = 1
        Me.TxtV_Type.Tag = ""
        '
        'LblSite_CodeReq
        '
        Me.LblSite_CodeReq.Location = New System.Drawing.Point(309, 20)
        Me.LblSite_CodeReq.Tag = ""
        '
        'LblSite_Code
        '
        Me.LblSite_Code.BackColor = System.Drawing.Color.Transparent
        Me.LblSite_Code.Location = New System.Drawing.Point(197, 15)
        Me.LblSite_Code.Size = New System.Drawing.Size(87, 16)
        Me.LblSite_Code.Tag = ""
        Me.LblSite_Code.Text = "Branch Name"
        '
        'TxtSite_Code
        '
        Me.TxtSite_Code.AgSelectedValue = ""
        Me.TxtSite_Code.BackColor = System.Drawing.Color.White
        Me.TxtSite_Code.Location = New System.Drawing.Point(327, 14)
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
        Me.LblPrefix.Location = New System.Drawing.Point(853, 32)
        Me.LblPrefix.Tag = ""
        Me.LblPrefix.Visible = False
        '
        'TabControl1
        '
        Me.TabControl1.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabControl1.Location = New System.Drawing.Point(-4, 18)
        Me.TabControl1.Size = New System.Drawing.Size(970, 130)
        Me.TabControl1.TabIndex = 0
        '
        'TP1
        '
        Me.TP1.BackColor = System.Drawing.Color.FromArgb(CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer), CType(CType(234, Byte), Integer))
        Me.TP1.Controls.Add(Me.Label5)
        Me.TP1.Controls.Add(Me.Label1)
        Me.TP1.Controls.Add(Me.TxtForJobOrder)
        Me.TP1.Controls.Add(Me.LblForJobOrder)
        Me.TP1.Controls.Add(Me.TxtManualRefNo)
        Me.TP1.Controls.Add(Me.LblManualRefNo)
        Me.TP1.Controls.Add(Me.TxtProcess)
        Me.TP1.Controls.Add(Me.LblProcess)
        Me.TP1.Controls.Add(Me.TxtRemarks)
        Me.TP1.Location = New System.Drawing.Point(4, 22)
        Me.TP1.Size = New System.Drawing.Size(962, 104)
        Me.TP1.Text = "Document Detail"
        Me.TP1.Controls.SetChildIndex(Me.LblV_Date, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtRemarks, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblV_No, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtV_No, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblProcess, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtProcess, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblManualRefNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtManualRefNo, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblForJobOrder, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtForJobOrder, 0)
        Me.TP1.Controls.SetChildIndex(Me.LblPrefix, 0)
        Me.TP1.Controls.SetChildIndex(Me.TxtSite_Code, 0)
        Me.TP1.Controls.SetChildIndex(Me.Label2, 0)
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
        '
        'Topctrl1
        '
        Me.Topctrl1.Size = New System.Drawing.Size(965, 41)
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
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Cornsilk
        Me.Panel1.Controls.Add(Me.LblTotalAmount)
        Me.Panel1.Controls.Add(Me.LblTotalAmountText)
        Me.Panel1.Controls.Add(Me.LblTotalRecMeasure)
        Me.Panel1.Controls.Add(Me.LblTotalMeasureText)
        Me.Panel1.Controls.Add(Me.LblTotalRecQty)
        Me.Panel1.Controls.Add(Me.LblTotalQtyText)
        Me.Panel1.Location = New System.Drawing.Point(8, 547)
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
        Me.Pnl1.Location = New System.Drawing.Point(8, 175)
        Me.Pnl1.Name = "Pnl1"
        Me.Pnl1.Size = New System.Drawing.Size(949, 372)
        Me.Pnl1.TabIndex = 1
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
        Me.TxtRemarks.Location = New System.Drawing.Point(327, 74)
        Me.TxtRemarks.MaxLength = 255
        Me.TxtRemarks.Name = "TxtRemarks"
        Me.TxtRemarks.Size = New System.Drawing.Size(438, 18)
        Me.TxtRemarks.TabIndex = 5
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
        Me.TxtManualRefNo.Location = New System.Drawing.Point(558, 34)
        Me.TxtManualRefNo.MaxLength = 50
        Me.TxtManualRefNo.Name = "TxtManualRefNo"
        Me.TxtManualRefNo.Size = New System.Drawing.Size(207, 18)
        Me.TxtManualRefNo.TabIndex = 3
        '
        'LblManualRefNo
        '
        Me.LblManualRefNo.AutoSize = True
        Me.LblManualRefNo.BackColor = System.Drawing.Color.Transparent
        Me.LblManualRefNo.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblManualRefNo.Location = New System.Drawing.Point(433, 35)
        Me.LblManualRefNo.Name = "LblManualRefNo"
        Me.LblManualRefNo.Size = New System.Drawing.Size(99, 16)
        Me.LblManualRefNo.TabIndex = 726
        Me.LblManualRefNo.Text = "Conversion. No."
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
        Me.TxtProcess.Location = New System.Drawing.Point(327, 54)
        Me.TxtProcess.MaxLength = 20
        Me.TxtProcess.Name = "TxtProcess"
        Me.TxtProcess.Size = New System.Drawing.Size(438, 18)
        Me.TxtProcess.TabIndex = 4
        '
        'LblProcess
        '
        Me.LblProcess.AutoSize = True
        Me.LblProcess.BackColor = System.Drawing.Color.Transparent
        Me.LblProcess.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblProcess.Location = New System.Drawing.Point(197, 54)
        Me.LblProcess.Name = "LblProcess"
        Me.LblProcess.Size = New System.Drawing.Size(56, 16)
        Me.LblProcess.TabIndex = 737
        Me.LblProcess.Text = "Process"
        '
        'LblJobReceiveDetail
        '
        Me.LblJobReceiveDetail.BackColor = System.Drawing.Color.SteelBlue
        Me.LblJobReceiveDetail.DisabledLinkColor = System.Drawing.Color.White
        Me.LblJobReceiveDetail.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblJobReceiveDetail.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline
        Me.LblJobReceiveDetail.LinkColor = System.Drawing.Color.White
        Me.LblJobReceiveDetail.Location = New System.Drawing.Point(8, 154)
        Me.LblJobReceiveDetail.Name = "LblJobReceiveDetail"
        Me.LblJobReceiveDetail.Size = New System.Drawing.Size(123, 20)
        Me.LblJobReceiveDetail.TabIndex = 733
        Me.LblJobReceiveDetail.TabStop = True
        Me.LblJobReceiveDetail.Text = "Job Return Detail"
        Me.LblJobReceiveDetail.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TxtForJobOrder
        '
        Me.TxtForJobOrder.AgAllowUserToEnableMasterHelp = False
        Me.TxtForJobOrder.AgLastValueTag = Nothing
        Me.TxtForJobOrder.AgLastValueText = Nothing
        Me.TxtForJobOrder.AgMandatory = False
        Me.TxtForJobOrder.AgMasterHelp = False
        Me.TxtForJobOrder.AgNumberLeftPlaces = 0
        Me.TxtForJobOrder.AgNumberNegetiveAllow = False
        Me.TxtForJobOrder.AgNumberRightPlaces = 0
        Me.TxtForJobOrder.AgPickFromLastValue = False
        Me.TxtForJobOrder.AgRowFilter = ""
        Me.TxtForJobOrder.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtForJobOrder.AgSelectedValue = Nothing
        Me.TxtForJobOrder.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtForJobOrder.AgValueType = AgControls.AgTextBox.TxtValueType.YesNo_Value
        Me.TxtForJobOrder.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtForJobOrder.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtForJobOrder.Location = New System.Drawing.Point(890, 11)
        Me.TxtForJobOrder.MaxLength = 255
        Me.TxtForJobOrder.Name = "TxtForJobOrder"
        Me.TxtForJobOrder.Size = New System.Drawing.Size(63, 18)
        Me.TxtForJobOrder.TabIndex = 748
        Me.TxtForJobOrder.Visible = False
        '
        'LblForJobOrder
        '
        Me.LblForJobOrder.AutoSize = True
        Me.LblForJobOrder.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblForJobOrder.Location = New System.Drawing.Point(804, 12)
        Me.LblForJobOrder.Name = "LblForJobOrder"
        Me.LblForJobOrder.Size = New System.Drawing.Size(87, 16)
        Me.LblForJobOrder.TabIndex = 749
        Me.LblForJobOrder.Text = "For Job Order"
        Me.LblForJobOrder.Visible = False
        '
        'BtnFillJobOrder
        '
        Me.BtnFillJobOrder.BackColor = System.Drawing.Color.Transparent
        Me.BtnFillJobOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnFillJobOrder.Font = New System.Drawing.Font("Lucida Console", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnFillJobOrder.ForeColor = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.BtnFillJobOrder.Location = New System.Drawing.Point(134, 154)
        Me.BtnFillJobOrder.Margin = New System.Windows.Forms.Padding(0)
        Me.BtnFillJobOrder.Name = "BtnFillJobOrder"
        Me.BtnFillJobOrder.Size = New System.Drawing.Size(38, 20)
        Me.BtnFillJobOrder.TabIndex = 751
        Me.BtnFillJobOrder.Text = "..."
        Me.BtnFillJobOrder.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.BtnFillJobOrder.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(197, 75)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(60, 16)
        Me.Label1.TabIndex = 750
        Me.Label1.Text = "Remarks"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(309, 61)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(10, 7)
        Me.Label5.TabIndex = 771
        Me.Label5.Text = "Ä"
        '
        'FrmMaterialCostConversion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.BackColor = System.Drawing.SystemColors.ButtonShadow
        Me.ClientSize = New System.Drawing.Size(965, 616)
        Me.Controls.Add(Me.BtnFillJobOrder)
        Me.Controls.Add(Me.LblJobReceiveDetail)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.Pnl1)
        Me.Name = "FrmMaterialCostConversion"
        Me.Text = "Template Job Receive"
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
        Me.Controls.SetChildIndex(Me.LblJobReceiveDetail, 0)
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
    Protected WithEvents Panel1 As System.Windows.Forms.Panel
    Protected WithEvents LblTotalRecQty As System.Windows.Forms.Label
    Protected WithEvents LblTotalQtyText As System.Windows.Forms.Label
    Protected WithEvents Pnl1 As System.Windows.Forms.Panel
    Protected WithEvents LblTotalRecMeasure As System.Windows.Forms.Label
    Protected WithEvents TxtRemarks As AgControls.AgTextBox
    Protected WithEvents LblTotalMeasureText As System.Windows.Forms.Label
    Protected WithEvents TxtManualRefNo As AgControls.AgTextBox
    Protected WithEvents LblManualRefNo As System.Windows.Forms.Label
    Protected WithEvents TxtProcess As AgControls.AgTextBox
    Protected WithEvents LblProcess As System.Windows.Forms.Label
    Protected WithEvents LblJobReceiveDetail As System.Windows.Forms.LinkLabel
    Protected WithEvents TxtForJobOrder As AgControls.AgTextBox
    Protected WithEvents LblForJobOrder As System.Windows.Forms.Label
    Protected WithEvents LblTotalAmount As System.Windows.Forms.Label
    Protected WithEvents LblTotalAmountText As System.Windows.Forms.Label
    Protected WithEvents BtnFillJobOrder As System.Windows.Forms.Button
    Protected WithEvents Label1 As System.Windows.Forms.Label
#End Region

    Private Sub TempJobReturn_BaseEvent_Approve_PostTrans(ByVal SearchCode As String) Handles Me.BaseEvent_Approve_PostTrans

    End Sub

    Private Sub Frm_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "JobIssRec"
        LogTableName = "JobIssRec_Log"
        MainLineTableCsv = "JobReceiveDetail,JobIssueDetail,Structure_TransFooter,Structure_TransLine"
        LogLineTableCsv = "JobReceiveDetail_Log,JobIssueDetail_Log,Structure_TransFooter_Log,Structure_TransLine_Log"
        AgL.GridDesign(Dgl1)

    End Sub

    Private Sub FrmProductionOrder_BaseEvent_FindLog() Handles Me.BaseEvent_FindLog
        Dim mCondStr$
        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) &
                        " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "H.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        'AgL.PubFindQry = " SELECT J.UID as SearchCode, Vt.Description AS [Entry Type], " & _
        '                    " J.V_Date AS [Entry Date], J.V_No AS [Entry No], " & _
        '                    " J.ManualRefNo, J.DueDate " & _
        '                    " FROM JobIssRec_Log J " & _
        '                    " LEFT JOIN voucher_type Vt ON J.V_Type = Vt.V_Type " & _
        '                    " Where J.EntryStatus = '" & ClsMain.LogStatus.LogOpen & "'  " & mCondStr

        AgL.PubFindQry = " SELECT H.UID AS SearchCode, H.V_Type AS [Return Type], H.V_Prefix AS Prefix, H.V_Date AS Date, H.V_No AS [Return No], " &
                " H.ManualRefNo AS [Manual No], J.PurjaNo, H.Process, H.DueDate AS [Due Date], H.IssQty AS [Issue Qty], H.IssMeasure AS [Issue Measure],  " &
                " H.RecQty AS [Rec Qty], H.RecMeasure AS [Rec Measure], H.JobReceiveFor AS [Job Receive For], H.Remarks, H.Structure, H.EntryBy AS [Entry By],  " &
                " H.EntryDate AS [Entry Date], H.EntryType AS [Entry Type], H.EntryStatus AS [Entry Status], H.ApproveBy AS [Approve By], H.ApproveDate AS [Approve Date],  " &
                " H.MoveToLog AS [Move To Log], H.MoveToLogDate AS [Move To Log Date], H.Status, H.BillingType AS [Billing Type], H.OrderBy AS [ORDER By],  " &
                " H.TotalWeight AS [Total Weight], H.JobWorkerDocNo AS [Job Worker DocNo], H.TotalConsumptionQty AS [Total Consumption Qty], H.TotalConsumptionMeasure AS [Total Consumption Measure],  " &
                " H.TotalByProductQty AS [Total By Product Qty], H.TotalByProductMeasure AS [Total By Product Measure], " &
                " D.Div_Name AS Division, SM.Name AS [Site Name], SGJ.DispName AS [Job Worker Name], G.Description AS Godown, JO.ManualRefNo AS [Job ORDER No] " &
                " FROM JobIssRec_Log H " &
                " LEFT JOIN JobReceiveDetail_Log L On H.UID = L.UID " &
                " LEFT JOIN JobOrder J On L.JobOrder = J.DocId " &
                " LEFT JOIN Division D ON D.Div_Code =H.Div_Code   " &
                " LEFT JOIN SiteMast SM ON SM.Code=H.Site_Code   " &
                " LEFT JOIN voucher_type Vt ON H.V_Type = vt.V_Type  " &
                " LEFT JOIN SubGroup SGJ ON SGJ.SubCode=H.JobWorker  " &
                " LEFT JOIN Godown G ON G.Code = H.Godown   " &
                " LEFT JOIN JobOrder  JO ON H.JobOrder   =JO.DocID  " &
                " Where H.EntryStatus = '" & AgTemplate.ClsMain.LogStatus.LogOpen & "'  " & mCondStr

        AgL.PubFindQryOrdBy = "[Entry Date]"
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mCondStr$
        mCondStr = " " & AgL.CondStrFinancialYear("H.V_Date", AgL.PubStartDate, AgL.PubEndDate) &
                        " And " & AgL.PubSiteCondition("H.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "H.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"

        'AgL.PubFindQry = " SELECT J.DocID as SearchCode, Vt.Description AS [Entry Type], " & _
        '                    " J.V_Date AS [Entry Date], J.V_No AS [Entry No], " & _
        '                    " J.ManualRefNo, J.DueDate " & _
        '                    " FROM JobIssRec J " & _
        '                    " LEFT JOIN voucher_type Vt ON J.V_Type = Vt.V_Type " & _
        '                    " Where 1=1  " & mCondStr

        If IsApplyVTypePermission Then
            mCondStr = mCondStr & " And H.V_Type In (Select V_Type From User_VType_Permission VP Where VP.UserName = '" & AgL.PubUserName & "' And VP.Div_Code = '" & AgL.PubDivCode & "' And VP.Site_Code = '" & AgL.PubSiteCode & "') "
        End If


        AgL.PubFindQry = " SELECT H.DocID AS SearchCode, H.V_Type AS [Return Type], H.V_Prefix AS Prefix, H.V_Date AS Date, H.V_No AS [Return No], " &
            " H.ManualRefNo AS [Manual No], J.PurjaNo, H.Process, H.DueDate AS [Due Date], H.IssQty AS [Issue Qty], H.IssMeasure AS [Issue Measure],  " &
            " H.RecQty AS [Rec Qty], H.RecMeasure AS [Rec Measure], H.JobReceiveFor AS [Job Receive For], H.Remarks, H.Structure, H.EntryBy AS [Entry By],  " &
            " H.EntryDate AS [Entry Date], H.EntryType AS [Entry Type], H.EntryStatus AS [Entry Status], H.ApproveBy AS [Approve By], H.ApproveDate AS [Approve Date],  " &
            " H.MoveToLog AS [Move To Log], H.MoveToLogDate AS [Move To Log Date], H.Status, H.BillingType AS [Billing Type], H.OrderBy AS [ORDER By],  " &
            " H.TotalWeight AS [Total Weight], H.JobWorkerDocNo AS [Job Worker DocNo], H.TotalConsumptionQty AS [Total Consumption Qty], H.TotalConsumptionMeasure AS [Total Consumption Measure],  " &
            " H.TotalByProductQty AS [Total By Product Qty], H.TotalByProductMeasure AS [Total By Product Measure], " &
            " D.Div_Name AS Division, SM.Name AS [Site Name], SGJ.DispName AS [Job Worker Name], G.Description AS Godown, JO.ManualRefNo AS [Job ORDER No] " &
            " FROM JobIssRec H " &
            " LEFT JOIN JobReceiveDetail L On H.DocId = L.DocId " &
            " LEFT JOIN JobOrder J On L.JobOrder = J.DocId " &
            " LEFT JOIN Division D ON D.Div_Code =H.Div_Code   " &
            " LEFT JOIN SiteMast SM ON SM.Code=H.Site_Code   " &
            " LEFT JOIN voucher_type Vt ON H.V_Type = vt.V_Type  " &
            " LEFT JOIN SubGroup SGJ ON SGJ.SubCode=H.JobWorker  " &
            " LEFT JOIN Godown G ON G.Code = H.Godown   " &
            " LEFT JOIN JobOrder  JO ON H.JobOrder   =JO.DocID  " &
            " Where 1=1  " & mCondStr


        AgL.PubFindQryOrdBy = "[Entry Date]"
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
                " Where IFNull(IsDeleted,0) = 0  " & mCondStr & "  Order By J.V_Date, J.V_No "

        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub FrmQuality1_BaseFunction_FIniMastLog(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMastLog
        Dim mCondStr$

        mCondStr = " " & AgL.CondStrFinancialYear("J.V_Date", AgL.PubStartDate, AgL.PubEndDate) &
                        " And " & AgL.PubSiteCondition("J.Site_Code", AgL.PubSiteCode) & " " & AgL.RetDivisionCondition(AgL, "J.Div_Code")
        mCondStr = mCondStr & " And Vt.NCat in ('" & EntryNCat & "')"
        mCondStr = mCondStr & " And J.EntryStatus='" & LogStatus.LogOpen & "' "

        mQry = " Select J.UID As SearchCode " &
            " From JobIssRec_Log J " &
            " Left Join Voucher_Type Vt On J.V_Type = Vt.V_Type  " &
            " Where 1=1  " & mCondStr & "  Order By J.V_Date Desc "

        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub FrmSaleOrder_BaseFunction_IniGrid() Handles Me.BaseFunction_IniGrid
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl1, Col1JobWorker, 100, 0, Col1JobWorker, True, False)
            .AddAgTextColumn(Dgl1, Col1Item, 200, 0, Col1Item, True, False)
            .AddAgNumberColumn(Dgl1, Col1Qty, 70, 8, 4, True, Col1Qty, True, False, True)
            .AddAgTextColumn(Dgl1, Col1Unit, 50, 0, Col1Unit, True, True)
            .AddAgNumberColumn(Dgl1, Col1MeasurePerPcs, 100, 8, 4, False, Col1MeasurePerPcs, True, True, True)
            .AddAgNumberColumn(Dgl1, Col1TotalMeasure, 100, 8, 4, False, Col1TotalMeasure, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_Measure")), Boolean), Not CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsEditable_Measure")), Boolean), True)
            .AddAgTextColumn(Dgl1, Col1MeasureUnit, 70, 0, Col1MeasureUnit, True, True)
            .AddAgTextColumn(Dgl1, Col1CostCenter, 100, 0, Col1CostCenter, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_CostCenter")), Boolean), False)
            .AddAgTextColumn(Dgl1, Col1JobOrder, 100, 0, Col1JobOrder, CType(AgL.VNull(DtV_TypeSettings.Rows(0)("IsVisible_CostCenter")), Boolean), False)
            .AddAgNumberColumn(Dgl1, Col1Rate, 70, 8, 2, False, Col1Rate, True, False, True)
            .AddAgNumberColumn(Dgl1, Col1Amount, 70, 8, 2, False, Col1Amount, True, True, True)
            .AddAgTextColumn(Dgl1, Col1Remark, 200, 255, Col1Remark, True, False)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 35
        Dgl1.AgSkipReadOnlyColumns = True


        Dgl1.Columns(Col1MeasurePerPcs).Visible = False
        ' Dgl1.Columns(Col1TotalMeasure).Visible = False
        Dgl1.Columns(Col1MeasureUnit).Visible = False
        Dgl1.Columns(Col1JobOrder).ReadOnly = True
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As SQLiteConnection, ByVal Cmd As SQLiteCommand) Handles Me.BaseEvent_Save_InTrans
        Dim I As Integer, mSr As Integer
        Dim Stock As AgTemplate.ClsMain.StructStock = Nothing, StockProcess As AgTemplate.ClsMain.StructStock = Nothing

        mQry = "UPDATE JobIssRec " &
                " SET " &
                " ManualRefNo = " & AgL.Chk_Text(TxtManualRefNo.Text) & ", " &
                " Process = " & AgL.Chk_Text(TxtProcess.AgSelectedValue) & ", " &
                " Remarks = " & AgL.Chk_Text(TxtRemarks.Text) & ", " &
                " RecQty = " & Val(LblTotalRecQty.Text) & ", " &
                " RecMeasure = " & Val(LblTotalRecMeasure.Text) & ", " &
                " TotalAmount = " & Val(LblTotalAmount.Text) & " " &
                " Where DocID = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)


        mQry = "Delete From JobReceiveDetail Where DocId = '" & SearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)


        'Never Try to Serialise Sr in Line Items 
        'As Some other Entry points may updating values to this Search code and Sr
        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1Item, I).Value <> "" Then
                mSr += 1

                mQry = "INSERT INTO JobReceiveDetail(DocId, Sr, JobWorker, Item, Qty, Unit, MeasurePerPcs, TotalMeasure, " &
                        " MeasureUnit, CostCenter, JobOrder, Rate, Amount, Remark) " &
                        " Values (" & AgL.Chk_Text(mSearchCode) & ", " &
                        " " & mSr & ", " & AgL.Chk_Text(Dgl1.Item(Col1JobWorker, I).Tag) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1Item, I).Tag) & ", " &
                        " " & Val(Dgl1.Item(Col1Qty, I).Value) & ", " & AgL.Chk_Text(Dgl1.Item(Col1Unit, I).Value) & ", " &
                        " " & Val(Dgl1.Item(Col1MeasurePerPcs, I).Value) & ", " &
                        " " & Val(Dgl1.Item(Col1TotalMeasure, I).Value) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1MeasureUnit, I).Value) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1CostCenter, I).Tag) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1JobOrder, I).Tag) & ", " &
                        " " & Val(Dgl1.Item(Col1Rate, I).Value) & ", " &
                        " " & Val(Dgl1.Item(Col1Amount, I).Value) & ", " &
                        " " & AgL.Chk_Text(Dgl1.Item(Col1Remark, I).Value) & " ) "
                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
            End If
        Next



        mSr = 0


        mQry = "Delete From StockProcess Where DocId = '" & mInternalCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1Item, I).Value <> "" Then
                mSr += 1

                With StockProcess
                    .DocID = mSearchCode
                    .Sr = mSr
                    .V_Type = TxtV_Type.AgSelectedValue
                    .V_Prefix = LblPrefix.Text
                    .V_Date = TxtV_Date.Text
                    .V_No = TxtV_No.Text
                    .RecID = TxtManualRefNo.Text
                    .Div_Code = TxtDivision.AgSelectedValue
                    .Site_Code = TxtSite_Code.AgSelectedValue
                    .CostCenter = Dgl1.Item(Col1CostCenter, I).Tag
                    .SubCode = Dgl1.Item(Col1JobWorker, I).Tag
                    .ReferenceDocID = Dgl1.Item(Col1JobOrder, I).Tag
                    .Item = Dgl1.AgSelectedValue(Col1Item, I)
                    .Godown = ""
                    .Qty_Iss = Val(Dgl1.Item(Col1Qty, I).Value)
                    .Unit = Dgl1.Item(Col1Unit, I).Value
                    .MeasurePerPcs = Val(Dgl1.Item(Col1MeasurePerPcs, I).Value)
                    .Measure_Iss = Val(Dgl1.Item(Col1TotalMeasure, I).Value)
                    .MeasureUnit = Dgl1.Item(Col1MeasureUnit, I).Value
                    .Status = AgTemplate.ClsMain.StockStatus.Standard
                    .Process = TxtProcess.AgSelectedValue
                    .Rate = Val(Dgl1.Item(Col1Rate, I).Value)
                    .Amount = Val(Dgl1.Item(Col1Amount, I).Value)
                End With
                Call AgTemplate.ClsMain.ProcStockPost("StockProcess", StockProcess, Conn, Cmd)
            End If
        Next

        Call ProcPostInPayment(Conn, Cmd)
    End Sub

    Private Sub FrmProductionOrder_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim I As Integer
        Dim DsTemp As DataSet




        mQry = "Select J.*, P.Description As ProcessDesc " &
            " From JobIssRec J " &
            " LEFT JOIN Process P ON J.Process = P.NCat " &
            " Where J.DocID='" & SearchCode & "'"
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then


                TxtManualRefNo.Text = AgL.XNull(.Rows(0)("ManualRefNo"))
                TxtProcess.Tag = AgL.XNull(.Rows(0)("Process"))
                TxtProcess.Text = AgL.XNull(.Rows(0)("ProcessDesc"))

                TxtRemarks.Text = AgL.XNull(.Rows(0)("Remarks"))
                LblTotalRecQty.Text = AgL.VNull(.Rows(0)("IssQty"))
                LblTotalRecMeasure.Text = AgL.VNull(.Rows(0)("IssMeasure"))
                LblTotalAmount.Text = AgL.VNull(.Rows(0)("TotalAmount"))


                If Dgl1.Item(Col1JobOrder, 0).Value <> "" Then
                    TxtForJobOrder.Text = "Yes"
                Else
                    TxtForJobOrder.Text = "No"
                End If

                IniGrid()

                '-------------------------------------------------------------
                'Line Records are showing in Grid
                '-------------------------------------------------------------
                mQry = "Select L.*, CC.Name as CostCenterName, JO.ManualRefNo as JobOrderNo, I.Description as ItemDescription, Sg.Name as JobWorkerName " &
                       "From JobReceiveDetail L " &
                       "Left Join CostCenterMast CC On L.CostCenter = CC.Code " &
                       "Left Join Subgroup Sg on L.JobWorker = Sg.SubCode " &
                       "Left Join Item I On L.Item = I.Code " &
                       "Left Join JobOrder JO On L.JobOrder = JO.DocId " &
                       "Where L.DocId = '" & SearchCode & "' Order By Sr "

                DsTemp = AgL.FillData(mQry, AgL.GCn)
                With DsTemp.Tables(0)
                    Dgl1.RowCount = 1
                    Dgl1.Rows.Clear()
                    If .Rows.Count > 0 Then
                        For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                            Dgl1.Rows.Add()
                            Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count - 1
                            Dgl1.Item(Col1JobWorker, I).Tag = AgL.XNull(.Rows(I)("JobWorker"))
                            Dgl1.Item(Col1JobWorker, I).Value = AgL.XNull(.Rows(I)("JobWorkerName"))
                            Dgl1.Item(Col1Item, I).Tag = AgL.XNull(.Rows(I)("Item"))
                            Dgl1.Item(Col1Item, I).Value = AgL.XNull(.Rows(I)("ItemDescription"))
                            Dgl1.Item(Col1Qty, I).Value = AgL.VNull(.Rows(I)("Qty"))
                            Dgl1.Item(Col1Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                            Dgl1.Item(Col1MeasurePerPcs, I).Value = AgL.VNull(.Rows(I)("MeasurePerPcs"))
                            Dgl1.Item(Col1TotalMeasure, I).Value = AgL.VNull(.Rows(I)("TotalMeasure"))
                            Dgl1.Item(Col1MeasureUnit, I).Value = AgL.XNull(.Rows(I)("MeasureUnit"))
                            Dgl1.Item(Col1JobOrder, I).Tag = AgL.XNull(.Rows(I)("JobOrder"))
                            Dgl1.Item(Col1JobOrder, I).Value = AgL.XNull(.Rows(I)("JobOrderNO"))
                            Dgl1.Item(Col1CostCenter, I).Tag = AgL.XNull(.Rows(I)("CostCenter"))
                            Dgl1.Item(Col1CostCenter, I).Value = AgL.XNull(.Rows(I)("CostCenterName"))
                            Dgl1.Item(Col1Rate, I).Value = Format(AgL.VNull(.Rows(I)("Rate")), "0.00")
                            Dgl1.Item(Col1Amount, I).Value = Format(AgL.VNull(.Rows(I)("Amount")), "0.00")
                            Dgl1.Item(Col1Remark, I).Value = AgL.XNull(.Rows(I)("Remark"))

                        Next I
                    End If
                End With
                Calculation()
                '-------------------------------------------------------------
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
                TxtManualRefNo.Text = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "JobIssRec", TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, AgTemplate.ClsMain.ManualRefType.Max)
                FAsignProcess()
                IniGrid()

            Case TxtManualRefNo.Name
                e.Cancel = Not AgTemplate.ClsMain.FCheckDuplicateRefNo("ManualRefNo", "JobIssRec", TxtV_Type.Tag, TxtV_Date.Text, TxtDivision.Tag, TxtSite_Code.Tag, Topctrl1.Mode, TxtManualRefNo.Text, mSearchCode)
        End Select
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_Topctrl_tbAdd() Handles Me.BaseEvent_Topctrl_tbAdd
        'If mBillPosting = ClsMain.JobReceiveBillPosting.None Then MsgBox("Bill Posting Property Is Not Set", MsgBoxStyle.Exclamation) : Topctrl1.FButtonClick(14, True)
        TxtProcess.AgSelectedValue = AgL.Dman_Execute(" SELECT H.NCat FROM Process H WHERE H.ProcessReturnNCat = '" & EntryNCat & "' ", AgL.GCn).ExecuteScalar
        TxtManualRefNo.Text = AgTemplate.ClsMain.FGetManualRefNo("ManualRefNo", "JobIssRec", TxtV_Type.AgSelectedValue, TxtV_Date.Text, TxtDivision.AgSelectedValue, TxtSite_Code.AgSelectedValue, AgTemplate.ClsMain.ManualRefType.Max)
        TxtForJobOrder.Text = "No"
        'TxtProcess.AgSelectedValue = ClsMain.Temp_NCat.WeavingOrder

    End Sub

    Private Sub Dgl1_EditingControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.EditingControl_KeyDown
        Try
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Item
                    If AgL.StrCmp(TxtForJobOrder.Text, "Yes") Then
                        Dgl1.AgRowFilter(Dgl1.Columns(Col1Item).Index) = ""
                        mQry = " SELECT I.Code AS Code, I.Description AS Item, H.ManualRefNo As JobOrderNo, " &
                                " IFNull(L.Qty,0) - IFNull(L.ReturnQty,0) AS BalanceQty, " &
                                " L.DocId AS JobOrder, L.Unit, L.MeasurePerPcs AS Measure, L.MeasureUnit, I.Rate  " &
                                " FROM JobOrderBom L  " &
                                " LEFT JOIN Item I ON L.Item = I.Code " &
                                " LEFT JOIN JobOrder H ON L.DocId = H.DocID " &
                                " LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type " &
                                " Where IFNull(H.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " &
                                " And H.Div_Code = '" & TxtDivision.Tag & "' And H.Site_Code = '" & TxtSite_Code.Tag & "' " &
                                " And H.JobWorker = '" & Dgl1.Item(Col1JobWorker, Dgl1.CurrentCell.RowIndex).Tag & "' "
                        Dgl1.AgHelpDataSet(Col1Item, 5) = AgL.FillData(mQry, AgL.GCn)
                    Else
                        If Dgl1.AgHelpDataSet(Col1Item) Is Nothing Then
                            If e.KeyCode <> Keys.Enter Then
                                Dgl1.AgRowFilter(Dgl1.Columns(Col1Item).Index) = ""
                                mQry = " SELECT H.Code , H.Description AS Item, H.ItemType, H.Unit, " &
                                        " H.Measure, H.MeasureUnit, '' As JobOrder, " &
                                        " 0 As  BalanceQty, H.Rate " &
                                        " FROM Item H  " &
                                        " Left Join ItemGroup IG On H.ItemGroup = IG.Code  " &
                                        " Where IFNull(H.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' "
                                Dgl1.AgHelpDataSet(Col1Item, 5) = AgL.FillData(mQry, AgL.GCn)
                            End If
                        End If
                    End If

                Case Col1CostCenter
                    If Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) Is Nothing Then
                        If e.KeyCode <> Keys.Enter Then
                            'mQry = "SELECT CC.Code, CC.Name as CostCenter, H.ManualRefNo as Job_Order_No, Sg.Name + (Case When IFNull(Sg.CityCode,'')<>'' Then ',' + City.CityName Else '' End) as Job_Worker_Name, H.Jobworker, H.DocID as Job_Order  " & _
                            '" FROM JobOrder H " & _
                            '" Left Join CostCenterMast CC On H.CostCenter = CC.Code " & _
                            '" Left Join Subgroup Sg On H.Jobworker = Sg.SubCode " & _
                            '" Left Join City On Sg.CityCode = City.CityCode " & _
                            '" LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type " & _
                            '" Where IFNull(H.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "'  " & _
                            '" And H.Div_Code = '" & AgL.PubDivCode & "' And H.Site_Code = '" & AgL.PubSiteCode & "' "

                            Dim mConstr As String
                            mConstr = ""

                            If TxtProcess.Tag <> "" Then
                                mConstr = " AND H.Process =  '" & TxtProcess.Tag & "' "
                            End If

                            mQry = "SELECT CC.Code, CC.Name as CostCenter, H.ManualRefNo as Job_Order_No, Sg.Name + (Case When IFNull(Sg.CityCode,'')<>'' Then ',' + City.CityName Else '' End) as Job_Worker_Name, H.Jobworker, H.DocID as Job_Order  " &
                                    " FROM JobOrder H " &
                                    " Left Join CostCenterMast CC On H.CostCenter = CC.Code " &
                                    " Left Join Subgroup Sg On H.Jobworker = Sg.SubCode " &
                                    " Left Join City On Sg.CityCode = City.CityCode " &
                                    " LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type " &
                                    " Where H.Div_Code = '" & AgL.PubDivCode & "' And H.Site_Code = '" & AgL.PubSiteCode & "' " &
                                    mConstr
                            Dgl1.AgHelpDataSet(Col1CostCenter, 2) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If

                Case Col1JobWorker
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1JobWorker) Is Nothing Then
                            FCreateHelpSubgroup()
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub DGL1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.KeyDown
        If e.Control And e.KeyCode = Keys.D Then
            sender.CurrentRow.Selected = True
        End If
        If e.Control Or e.Shift Or e.Alt Then Exit Sub
    End Sub

    Private Sub Dgl1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellEnter
        If Dgl1.CurrentCell Is Nothing Then Exit Sub
        Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
            Case Col1Item

            Case Col1JobWorker
                If Dgl1.Item(Col1CostCenter, Dgl1.CurrentCell.RowIndex).Value <> "" Then Dgl1.Item(Col1JobWorker, Dgl1.CurrentCell.RowIndex).ReadOnly = True
                If Dgl1.CurrentCell.RowIndex <> 0 Then
                    If Dgl1.Item(Col1JobWorker, Dgl1.CurrentCell.RowIndex).Value = "" Then
                        Dgl1.Item(Col1JobWorker, Dgl1.CurrentCell.RowIndex).Tag = Dgl1.Item(Col1JobWorker, Dgl1.CurrentCell.RowIndex - 1).Tag
                        Dgl1.Item(Col1JobWorker, Dgl1.CurrentCell.RowIndex).Value = Dgl1.Item(Col1JobWorker, Dgl1.CurrentCell.RowIndex - 1).Value
                    End If
                End If

        End Select
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

                Case Col1CostCenter
                    If Dgl1.Item(Col1CostCenter, mRowIndex).Value.ToString.Trim = "" Or Dgl1.AgSelectedValue(Col1CostCenter, mRowIndex).Trim = "" Then
                        Dgl1.Item(Col1JobOrder, mRowIndex).Tag = ""
                        Dgl1.Item(Col1JobOrder, mRowIndex).Value = ""
                        Dgl1.Item(Col1JobWorker, mRowIndex).Value = ""
                        Dgl1.Item(Col1JobWorker, mRowIndex).Tag = ""
                    Else
                        If Dgl1.AgDataRow IsNot Nothing Then
                            Dgl1.Item(Col1JobOrder, mRowIndex).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("Job_Order").Value)
                            Dgl1.Item(Col1JobOrder, mRowIndex).Value = AgL.XNull(Dgl1.AgDataRow.Cells("Job_Order_No").Value)
                            Dgl1.Item(Col1JobWorker, mRowIndex).Value = AgL.XNull(Dgl1.AgDataRow.Cells("Job_Worker_Name").Value)
                            Dgl1.Item(Col1JobWorker, mRowIndex).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("JobWorker").Value)
                        End If
                    End If

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
                Dgl1.Item(Col1MeasurePerPcs, mRow).Value = 0
                Dgl1.Item(Col1MeasureUnit, mRow).Value = ""
                Dgl1.AgSelectedValue(Col1JobOrder, mRow) = ""
            Else
                If Dgl1.AgDataRow IsNot Nothing Then
                    Dgl1.Item(Col1Qty, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("BalanceQty").Value)
                    Dgl1.Item(Col1Rate, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("Rate").Value)
                    Dgl1.Item(Col1Unit, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("Unit").Value)
                    Dgl1.Item(Col1MeasurePerPcs, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("Measure").Value)
                    Dgl1.Item(Col1MeasureUnit, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("MeasureUnit").Value)
                    'Dgl1.AgSelectedValue(Col1JobOrder, mRow) = AgL.XNull(Dgl1.AgDataRow.Cells("JobOrder").Value)
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
                Dgl1.Item(Col1TotalMeasure, I).Value = Format(Val(Dgl1.Item(Col1Qty, I).Value) * Val(Dgl1.Item(Col1MeasurePerPcs, I).Value), "0.000")
                If Dgl1.Item(Col1MeasurePerPcs, I).Value > 0 Then
                    Dgl1.Item(Col1Amount, I).Value = Format(Val(Dgl1.Item(Col1TotalMeasure, I).Value) * Val(Dgl1.Item(Col1Rate, I).Value), "0")
                Else
                    Dgl1.Item(Col1Amount, I).Value = Format(Val(Dgl1.Item(Col1Qty, I).Value) * Val(Dgl1.Item(Col1Rate, I).Value), "0")
                End If

                LblTotalRecQty.Text = Val(LblTotalRecQty.Text) + Val(Dgl1.Item(Col1Qty, I).Value)
                LblTotalRecMeasure.Text = Val(LblTotalRecMeasure.Text) + Val(Dgl1.Item(Col1TotalMeasure, I).Value)
                LblTotalAmount.Text = Val(LblTotalAmount.Text) + Val(Dgl1.Item(Col1Amount, I).Value)
            End If
        Next

        LblTotalRecQty.Text = Format(Val(LblTotalRecQty.Text), "0.000")
        LblTotalRecMeasure.Text = Format(Val(LblTotalRecMeasure.Text), "0.000")
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        Dim I As Integer = 0

        If AgCL.AgIsBlankGrid(Dgl1, Dgl1.Columns(Col1Item).Index) = True Then passed = False : Exit Sub

        passed = AgTemplate.ClsMain.FCheckDuplicateRefNo("ManualRefNo", "JobIssRec", TxtV_Type.Tag, TxtV_Date.Text, TxtDivision.Tag, TxtSite_Code.Tag, Topctrl1.Mode, TxtManualRefNo.Text, mSearchCode)

        With Dgl1
            For I = 0 To .Rows.Count - 1
                If .Item(Col1Item, I).Value <> "" Then
                    If Val(.Item(Col1Qty, I).Value) = 0 Then
                        MsgBox("Qty Is 0 At Row No " & Dgl1.Item(ColSNo, I).Value & "")
                        .CurrentCell = .Item(Col1Qty, I) : Dgl1.Focus()
                        passed = False : Exit Sub
                    End If

                    If Val(.Item(Col1Rate, I).Value) = 0 Then
                        MsgBox("Rate is 0 At Row No " & Dgl1.Item(ColSNo, I).Value & "", MsgBoxStyle.Information)
                        passed = False
                        Dgl1.CurrentCell = Dgl1.Item(Col1Rate, I) : Dgl1.Focus() : Exit Sub
                    End If
                End If
            Next
        End With
    End Sub



    Private Sub FrmProductionOrder_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
        Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
        LblTotalRecMeasure.Text = 0 : LblTotalRecQty.Text = 0
    End Sub

    Private Sub FrmProductionOrder_BaseEvent_Approve_InTrans(ByVal SearchCode As String, ByVal Conn As SQLiteConnection, ByVal Cmd As SQLiteCommand) Handles Me.BaseEvent_Approve_InTrans
    End Sub

    Private Sub TempJobOrder_BaseEvent_ApproveDeletion_InTrans(ByVal SearchCode As String, ByVal Conn As SQLiteConnection, ByVal Cmd As SQLiteCommand) Handles Me.BaseEvent_ApproveDeletion_InTrans
        mQry = "Delete From StockProcess Where DocId = '" & mInternalCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Delete From DuesPaymentDetail Where DocID = '" & mInternalCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Delete From DuesPayment Where DocID = '" & mInternalCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Delete From Ledger Where DocId = '" & mInternalCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Delete From LedgerM Where DocId = '" & mInternalCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    End Sub

    Private Sub ProcPostInPayment(ByVal Conn As SQLiteConnection, ByVal Cmd As SQLiteCommand)
        Dim I As Integer = 0, bSr As Integer = 0

        mQry = "Delete From DuesPaymentDetail Where DocID = '" & mInternalCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Delete From DuesPayment Where DocID = '" & mInternalCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " INSERT INTO DuesPayment(DocID, V_Type, V_Prefix, V_Date, V_No, Div_Code, Site_Code, " &
                " ManualRefNo, TransactionType,  NetAmount,  " &
                " Remark, EntryBy, EntryDate, EntryType, EntryStatus, " &
                " Status ) " &
                " VALUES ('" & mInternalCode & "',	" & AgL.Chk_Text(TxtV_Type.AgSelectedValue) & ", " &
                " " & AgL.Chk_Text(LblPrefix.Text) & ",	" & AgL.Chk_Text(TxtV_Date.Text) & ",	" &
                " " & Val(TxtV_No.Text) & ", " & AgL.Chk_Text(TxtDivision.AgSelectedValue) & ", " &
                " " & AgL.Chk_Text(TxtSite_Code.AgSelectedValue) & ", " &
                " " & AgL.Chk_Text(TxtManualRefNo.Text) & ", '" & AgTemplate.ClsMain.PaymentReceiptType.DebitNote & "' , " &
                " 0, " &
                " " & AgL.Chk_Text(TxtRemarks.Text) & ", " &
                " " & AgL.Chk_Text(AgL.PubUserName) & ", " & AgL.Chk_Text(AgL.GetDateTime(AgL.GcnRead)) & ", " &
                " " & AgL.Chk_Text(Topctrl1.Mode) & ",	" & AgL.Chk_Text(LogStatus.LogOpen) & ", " &
                " " & AgL.Chk_Text(TxtStatus.Text) & " ) "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        With Dgl1
            For I = 0 To .Rows.Count - 1
                If .Item(Col1Item, I).Value <> "" Then
                    bSr += 1
                    mQry = " Insert Into DuesPaymentDetail( " &
                            "DocId, " &
                            "Sr, " &
                            "TransactionType, " &
                            "Subcode, " &
                            "Amount, " &
                            "NetAmount, " &
                            "WeavingOrderDocId, " &
                            "Remark " &
                            ") " &
                            " Values( " &
                            " " & AgL.Chk_Text(mInternalCode) & ", " &
                            " " & bSr & ", " &
                            " '" & AgTemplate.ClsMain.PaymentReceiptType.DebitNote & "' , " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1JobWorker, I).Tag) & ", " &
                            " " & Val(Dgl1.Item(Col1Amount, I).Value) & ", " &
                            " " & Val(Dgl1.Item(Col1Amount, I).Value) & ", " &
                            " " & AgL.Chk_Text(Dgl1.AgSelectedValue(Col1JobOrder, I)) & ", " &
                            " " & AgL.Chk_Text(TxtRemarks.Text) & " " &
                            " )"
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                End If
            Next
        End With


        'Call AccountPosting()
        Call AccountPosting(Conn, Cmd)
    End Sub

    'Private Function AccountPosting() As Boolean
    '    Dim LedgAry() As AgLibrary.ClsMain.LedgRec
    '    Dim I As Integer, J As Integer = 0
    '    Dim DsTemp As DataSet = Nothing
    '    Dim mNarr As String = "", mCommonNarr$ = ""
    '    Dim mNetAmount As Double, mRoundOff As Double = 0
    '    Dim bDebitNoteAc$ = ""

    '    mQry = " Select DebitNoteAc From DuesPaymentEnviro  "
    '    bDebitNoteAc = AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar


    '    Dim GcnRead As SqliteConnection
    '    GcnRead = New SqliteConnection
    '    GcnRead.ConnectionString = AgL.Gcn_ConnectionString
    '    GcnRead.Open()

    '    mNetAmount = 0
    '    mCommonNarr = ""
    '    mCommonNarr = TxtRemarks.Text
    '    If mCommonNarr.Length > 255 Then mCommonNarr = AgL.MidStr(mCommonNarr, 0, 255)
    '    mNarr = TxtRemarks.Text
    '    If mNarr.Length > 255 Then mNarr = AgL.MidStr(mNarr, 0, 255)

    '    ReDim Preserve LedgAry(I)

    '    I = UBound(LedgAry) + 1
    '    ReDim Preserve LedgAry(I)
    '    LedgAry(I).SubCode = TxtJobWorker.AgSelectedValue
    '    LedgAry(I).ContraSub = bDebitNoteAc
    '    LedgAry(I).AmtCr = 0
    '    'LedgAry(I).AmtDr = Val(AgCalcGrid1.AgChargesValue(AgCalc_NetAmount, AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Amount))
    '    LedgAry(I).AmtDr = Val(Dgl1.Item(Col1Amount, 0).Value)
    '    LedgAry(I). = Val(Dgl1.Item(Col1Amount, 0).Value)
    '    mNarr = TxtRemarks.Text
    '    LedgAry(I).Narration = mNarr

    '    I = UBound(LedgAry) + 1
    '    ReDim Preserve LedgAry(I)
    '    LedgAry(I).SubCode = bDebitNoteAc
    '    LedgAry(I).ContraSub = TxtJobWorker.AgSelectedValue
    '    'LedgAry(I).AmtCr = Val(AgCalcGrid1.AgChargesValue(AgCalc_NetAmount, AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Amount))
    '    LedgAry(I).AmtCr = Val(Dgl1.Item(Col1Amount, 0).Value)
    '    LedgAry(I).AmtDr = 0
    '    LedgAry(I).Narration = mNarr

    '    If AgL.LedgerPost(AgL.MidStr(Topctrl1.Mode, 0, 1), LedgAry, AgL.GCn, AgL.ECmd, mInternalCode, CDate(TxtV_Date.Text), AgL.PubUserName, AgL.PubLoginDate, mCommonNarr, , AgL.Gcn_ConnectionString) = False Then
    '        AccountPosting = False : Err.Raise(1, , "Error in Ledger Posting")
    '    End If
    '    GcnRead.Close()
    '    GcnRead.Dispose()
    'End Function


    Private Function AccountPosting(ByVal Conn As SQLiteConnection, ByVal Cmd As SQLiteCommand) As Boolean
        Dim LedgAry() As AgLibrary.ClsMain.LedgRec
        Dim I As Integer, mSr As Integer = 0
        Dim DtTemp As DataTable
        Dim mNarr As String = "", mCommonNarr$ = ""
        Dim mNetAmount As Double, mRoundOff As Double = 0
        Dim bDebitNoteAc$ = ""

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

        'mQry = " Select BankAc From DuesPaymentEnviro E  Where E.V_Type = '" & TxtV_Type.AgSelectedValue & "' "
        'bBankAc = AgL.XNull(AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar)

        mQry = " Select DebitNoteAc From DuesPaymentEnviro  "
        bDebitNoteAc = AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar

        ReDim Preserve LedgAry(I)

        mQry = "Delete From Ledger Where DocId = '" & mInternalCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Delete From LedgerM Where DocId = '" & mInternalCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)


        mQry = " INSERT INTO LedgerM(	DocId,	Site_Code,	V_No,	V_Type,	V_Prefix,	V_Date, " &
                " SubCode,	Narration,	U_Name,	U_EntDt,	U_AE) " &
                " VALUES ('" & mInternalCode & "', " & AgL.Chk_Text(TxtSite_Code.AgSelectedValue) & ",	" & Val(TxtV_No.Text) & "," &
                " " & AgL.Chk_Text(TxtV_Type.AgSelectedValue) & ",	" & AgL.Chk_Text(LblPrefix.Text) & ", " &
                " " & AgL.Chk_Text(TxtV_Date.Text) & ",	" & AgL.Chk_Text(bDebitNoteAc) & ",	" &
                " " & AgL.Chk_Text(TxtRemarks.Text) & ",	" & AgL.Chk_Text(AgL.PubUserName) & ",	" &
                " " & AgL.Chk_Text(AgL.PubLoginDate) & ", " & AgL.Chk_Text(AgL.MidStr(Topctrl1.Mode, 0, 1)) & ") "
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = "Select L.JobWorker as JobWorker, L.CostCenter, Sum(L.Amount) as Amount " &
               "From JobReceiveDetail L  " &
               "Where L.DocID ='" & mInternalCode & "' " &
               "Group By L.JobWorker, L.CostCenter " &
               "Having Sum(L.Amount)<>0 "
        DtTemp = AgL.FillData(mQry, AgL.GcnRead).tables(0)

        If DtTemp.Rows.Count > 0 Then
            For I = 0 To DtTemp.Rows.Count - 1
                If AgL.XNull(DtTemp.Rows(I)("JobWorker")) = "" Then Err.Raise(1, , "Jobworker is blank at row " & I & ". Can't Continue.")
                If AgL.VNull(DtTemp.Rows(I)("Amount")) > 0 Then
                    With Dgl1
                        mSr += 1
                        mQry = " INSERT INTO Ledger(DocId, V_SNo, V_No, V_Type, RecID, V_Prefix, V_Date, SubCode, ContraSub, " &
                                    " AmtDr, AmtCr, Narration,	Site_Code,U_Name,	U_EntDt,	U_AE,	DivCode, JobOrder, CostCenter) " &
                                    " VALUES ('" & mInternalCode & "', " & Val(mSr) & ", " & Val(TxtV_No.Text) & ",	" &
                                    " " & AgL.Chk_Text(TxtV_Type.AgSelectedValue) & ", " & AgL.Chk_Text(TxtManualRefNo.Text) & ",	" & AgL.Chk_Text(LblPrefix.Text) & ", " &
                                    " " & AgL.Chk_Text(TxtV_Date.Text) & ",	" & AgL.Chk_Text(AgL.XNull(DtTemp.Rows(I)("JobWorker"))) & ", " &
                                    " " & AgL.Chk_Text(bDebitNoteAc) & ",	" & Val(AgL.VNull(DtTemp.Rows(I)("Amount"))) & ", 0, " &
                                    " " & AgL.Chk_Text(Dgl1.Item(Col1Remark, I).Value) & ",	" & AgL.Chk_Text(AgL.PubSiteCode) & ", " &
                                    " '" & AgL.PubUserName & "', '" & AgL.PubLoginDate & "', " &
                                    " " & AgL.Chk_Text(AgL.MidStr(Topctrl1.Mode, 0, 1)) & ", " &
                                    " '" & AgL.PubDivCode & "', " &
                                    " " & AgL.Chk_Text(Dgl1.AgSelectedValue(Col1JobOrder, I)) & ", " & AgL.Chk_Text(Dgl1.Item(Col1CostCenter, I).Tag) & " ) "
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                    End With

                    mSr += 1
                    mQry = " INSERT INTO Ledger(DocId, V_SNo, V_No, V_Type, RecID, V_Prefix, V_Date, SubCode, ContraSub, " &
                            " AmtDr, AmtCr, Narration,	Site_Code,U_Name,	U_EntDt,	U_AE,	DivCode, CostCenter) " &
                            " VALUES ('" & mInternalCode & "', " & Val(mSr) & ", " & Val(TxtV_No.Text) & ",	" &
                            " " & AgL.Chk_Text(TxtV_Type.AgSelectedValue) & ", " & AgL.Chk_Text(TxtManualRefNo.Text) & ",	" & AgL.Chk_Text(LblPrefix.Text) & ", " &
                            " " & AgL.Chk_Text(TxtV_Date.Text) & ",	" & AgL.Chk_Text(bDebitNoteAc) & ",	" &
                            " Null, " &
                            " 0, " & Val(AgL.VNull(DtTemp.Rows(I)("Amount"))) & ", " &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1Remark, I).Value) & ",	" & AgL.Chk_Text(AgL.PubSiteCode) & ", " &
                            " '" & AgL.PubUserName & "', '" & AgL.PubLoginDate & "', " &
                            " " & AgL.Chk_Text(AgL.MidStr(Topctrl1.Mode, 0, 1)) & ", " &
                            " '" & AgL.PubDivCode & "', " & AgL.Chk_Text(Dgl1.Item(Col1CostCenter, I).Tag) & ") "
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                ElseIf AgL.VNull(DtTemp.Rows(I)("Amount")) < 0 Then
                    With Dgl1
                        mSr += 1
                        mQry = " INSERT INTO Ledger(DocId, V_SNo, V_No, V_Type, RecID, V_Prefix, V_Date, SubCode, ContraSub, " &
                                    " AmtDr, AmtCr, Narration,	Site_Code,U_Name,	U_EntDt,	U_AE,	DivCode, JobOrder, CostCenter) " &
                                    " VALUES ('" & mInternalCode & "', " & Val(mSr) & ", " & Val(TxtV_No.Text) & ",	" &
                                    " " & AgL.Chk_Text(TxtV_Type.AgSelectedValue) & ", " & AgL.Chk_Text(TxtManualRefNo.Text) & ",	" & AgL.Chk_Text(LblPrefix.Text) & ", " &
                                    " " & AgL.Chk_Text(TxtV_Date.Text) & ",	" & AgL.Chk_Text(AgL.XNull(DtTemp.Rows(I)("JobWorker"))) & ", " &
                                    " " & AgL.Chk_Text(bDebitNoteAc) & ", 0, " & -Val(AgL.VNull(DtTemp.Rows(I)("Amount"))) & ",  " &
                                    " " & AgL.Chk_Text(Dgl1.Item(Col1Remark, I).Value) & ",	" & AgL.Chk_Text(AgL.PubSiteCode) & ", " &
                                    " '" & AgL.PubUserName & "', '" & AgL.PubLoginDate & "', " &
                                    " " & AgL.Chk_Text(AgL.MidStr(Topctrl1.Mode, 0, 1)) & ", " &
                                    " '" & AgL.PubDivCode & "', " &
                                    " " & AgL.Chk_Text(Dgl1.AgSelectedValue(Col1JobOrder, I)) & ", " & AgL.Chk_Text(Dgl1.Item(Col1CostCenter, I).Tag) & " ) "
                        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                    End With

                    mSr += 1
                    mQry = " INSERT INTO Ledger(DocId, V_SNo, V_No, V_Type, RecID, V_Prefix, V_Date, SubCode, ContraSub, " &
                            " AmtDr, AmtCr, Narration,	Site_Code,U_Name,	U_EntDt,	U_AE,	DivCode, CostCenter) " &
                            " VALUES ('" & mInternalCode & "', " & Val(mSr) & ", " & Val(TxtV_No.Text) & ",	" &
                            " " & AgL.Chk_Text(TxtV_Type.AgSelectedValue) & ", " & AgL.Chk_Text(TxtManualRefNo.Text) & ",	" & AgL.Chk_Text(LblPrefix.Text) & ", " &
                            " " & AgL.Chk_Text(TxtV_Date.Text) & ",	" & AgL.Chk_Text(bDebitNoteAc) & ",	" &
                            " Null, " &
                            " " & -Val(AgL.VNull(DtTemp.Rows(I)("Amount"))) & " , 0," &
                            " " & AgL.Chk_Text(Dgl1.Item(Col1Remark, I).Value) & ",	" & AgL.Chk_Text(AgL.PubSiteCode) & ", " &
                            " '" & AgL.PubUserName & "', '" & AgL.PubLoginDate & "', " &
                            " " & AgL.Chk_Text(AgL.MidStr(Topctrl1.Mode, 0, 1)) & ", " &
                            " '" & AgL.PubDivCode & "', " & AgL.Chk_Text(Dgl1.Item(Col1CostCenter, I).Tag) & ") "
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                End If

            Next

        End If


    End Function

    Private Sub FrmRateConversionEntryNew_BaseEvent_Topctrl_tbPrn(ByVal SearchCode As String) Handles Me.BaseEvent_Topctrl_tbPrn
        Dim mCrd As New ReportDocument
        Dim ReportView As New AgLibrary.RepView
        Dim DsRep As New DataSet
        Dim strQry As String = "", RepName As String = "", RepTitle As String = ""
        Dim bTableName As String = "", bSecTableName As String = "", bCondstr As String = ""
        Dim mOtherFields$ = ""
        Try
            Me.Cursor = Cursors.WaitCursor
            If FrmType = ClsMain.EntryPointType.Main Then
                AgL.PubReportTitle = "Rate Conversion Entry"
                bTableName = "JobIssRec" : bSecTableName = "JobReceiveDetail L ON L.DocId = H.DocID"
                RepName = "Rug_RateConversion_Print" : RepTitle = "Rate Conversion Entry"
                bCondstr = "WHERE H.DocID='" & SearchCode & "'"
            Else
                AgL.PubReportTitle = "Rate Conversion Entry Log"
                bTableName = "JobIssRec_Log" : bSecTableName = "JobReceiveDetail_Log L ON L.UID = H.UID"
                RepName = "Rug_RateConversion_Print" : RepTitle = "Rate Conversion Entry Log"
                bCondstr = "WHERE H.UID='" & SearchCode & "'"
            End If

            strQry = " SELECT H.DocID, H.V_Date, H.V_Type, H.ManualRefNo, H.Godown, H.RecQty, H.JobWorker, H.Remarks, " &
                    " L.Item, L.Qty, L.Unit, L.JobOrder, L.Rate, L.Amount, L.NetAmount, CC.Name AS CostCenterName, L.CostCenter, " &
                    " SG.Name AS WorkerName, J.ManualRefNo AS OrderNo, I.Description AS ItemDec ,U.DecimalPlaces  " &
                    " FROM " & bTableName & " H " &
                    " LEFT JOIN " & bSecTableName & " " &
                    " LEFT Join CostCenterMast CC On L.CostCenter = CC.Code " &
                    " LEFT Join Subgroup Sg on L.JobWorker = Sg.SubCode " &
                    " LEFT JOIN JobOrder J ON J.DocID = L.JobOrder  " &
                    " LEFT JOIN CostCenterMast C ON C.Code = L.CostCenter " &
                    " LEFT JOIN Item I ON I.Code = L.Item  " &
                    " LEFT JOIN Unit U ON U.Code = I.Unit " &
                    " " & bCondstr & " "
            AgL.ADMain = New SQLiteDataAdapter(strQry, AgL.GCn)
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

    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub

    Private Sub FrmRateConversionEntryNew_BaseFunction_DispText() Handles Me.BaseFunction_DispText
        TxtProcess.Enabled = False
    End Sub

    Private Sub TxtJobWorker_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)
        Dgl1.AgHelpDataSet(Col1CostCenter) = Nothing
    End Sub

    Private Sub BtnFill_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnFillJobOrder.Click
        Try
            If Topctrl1.Mode = "Browse" Then Exit Sub
            Dim StrTicked As String

            StrTicked = FHPGD_PurjaList()
            If StrTicked <> "" Then
                FFillMaterialOnLoom(StrTicked)
            Else
                Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
            End If

            Dgl1.Focus()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function FHPGD_PurjaList() As String
        Dim FRH_Multiple As DMHelpGrid.FrmHelpGrid_Multi
        Dim StrRtn As String = ""
        Dim mConStr$ = ""

        If Not AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Then
            mConStr = mConStr & " And C.Status = '" & AgTemplate.ClsMain.EntryStatus.Active & "' "
        End If

        mQry = " SELECT 'o' As Tick, L.CostCenter, Max(C.Name) AS CostCenterName, Max(Sg.Name) AS JobWorkerName, Max(C.Status) As Status " &
                " FROM StockProcess L  " &
                " LEFT JOIN CostCenterMast C ON L.CostCenter = C.Code " &
                " LEFT JOIN SubGroup Sg ON L.SubCode = Sg.SubCode " &
                " WHERE L.CostCenter Is Not NULL " &
                " AND L.Div_Code = '" & AgL.PubDivCode & "' AND L.Site_Code = '" & AgL.PubSiteCode & "' " &
                " And L.DocId <> '" & mSearchCode & "'" & mConStr &
                " GROUP BY L.CostCenter " &
                " Having Round(IFNull(Sum(L.Qty_Rec),0) - IFNull(Sum(L.Qty_Iss),0),3) <>  0 " &
                " Order By (Case When IsNumeric(Max(C.Name)) > 0 Then Convert(Numeric,Max(C.Name)) Else 0 End) "

        FRH_Multiple = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(AgL.FillData(mQry, AgL.GCn).TABLES(0)), "", 500, 700, , , False)
        FRH_Multiple.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple.FFormatColumn(1, , 0, , False)
        FRH_Multiple.FFormatColumn(2, "Cost Center", 100, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(3, "Job Worker", 380, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(4, "Status", 80, DataGridViewContentAlignment.MiddleLeft)

        FRH_Multiple.StartPosition = FormStartPosition.CenterScreen
        FRH_Multiple.ShowDialog()

        If FRH_Multiple.BytBtnValue = 0 Then
            StrRtn = FRH_Multiple.FFetchData(1, "'", "'", ",", True)
        End If
        FHPGD_PurjaList = StrRtn

        FRH_Multiple = Nothing
    End Function

    Private Sub FFillMaterialOnLoom(ByVal bCostCenterStr As String)
        Dim I As Integer = 0
        Dim DtTemp As DataTable = Nothing
        Try
            If bCostCenterStr = "" Then Exit Sub

            mQry = " SELECT L.CostCenter, Max(L.SubCode) As SubCode, L.Item,  " &
                        " Round(IFNull(Sum(L.Qty_Rec),0) - IFNull(Sum(L.Qty_Iss),0),3) AS Qty, Max(L.Unit) As Unit, " &
                        " Max(C.Name) as CostCenterName, Max(Sg.Name) as JobWorkerName, Max(I.Description) as ItemName, " &
                        " Max(H.DocID) as JobOrder, Max(H.V_Type) + '-' +  Max(H.ManualRefNo) as JobOrderNo, " &
                        " (CASE WHEN (SELECT IFNull(Sum(Qty_Rec),0) -  IFNull(Sum(Qty_Iss),0) FROM STockProcess WHERE CostCenter = L.CostCenter) >0 THEN (SELECT Max(Item.Rate) FROM StockProcess LEFT JOIN Item ON StockProcess.Item=Item.Code WHERE stockprocess.CostCenter = L.CostCenter  ) ELSE (SELECT Min(Item.Rate) FROM StockProcess LEFT JOIN Item ON StockProcess.Item=Item.Code WHERE stockprocess.CostCenter = L.CostCenter AND Item.Rate >0 ) End) as Rate  " &
                        " FROM StockProcess L  " &
                        " LEFT JOIN CostCenterMast C ON L.CostCenter = C.Code " &
                        " Left Join JobOrder H On L.CostCenter = H.CostCenter " &
                        " LEFT JOIN SubGroup Sg ON L.SubCode = Sg.SubCode " &
                        " LEFT JOIN Item I ON L.Item = I.Code " &
                        " WHERE L.CostCenter In (" & bCostCenterStr & ") And L.DocID <> '" & mSearchCode & "'  " &
                        " AND L.Div_Code = '" & AgL.PubDivCode & "' AND L.Site_Code = '" & AgL.PubSiteCode & "' " &
                        " GROUP BY L.CostCenter, L.Item " &
                        " Having Round(IFNull(Sum(L.Qty_Rec),0) - IFNull(Sum(L.Qty_Iss),0),3) <>  0 " &
                        " Order By L.CostCenter, L.Item "


            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

            With DtTemp
                Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
                If .Rows.Count > 0 Then
                    For I = 0 To .Rows.Count - 1
                        Dgl1.Rows.Add()
                        Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count - 1

                        Dgl1.Item(Col1CostCenter, I).Tag = AgL.XNull(.Rows(I)("CostCenter"))
                        Dgl1.Item(Col1CostCenter, I).Value = AgL.XNull(.Rows(I)("CostCenterName"))
                        Dgl1.Item(Col1JobWorker, I).Tag = AgL.XNull(.Rows(I)("SubCode"))
                        Dgl1.Item(Col1JobWorker, I).Value = AgL.XNull(.Rows(I)("JobWorkerName"))
                        Dgl1.Item(Col1Item, I).Tag = AgL.XNull(.Rows(I)("Item"))
                        Dgl1.Item(Col1Item, I).Value = AgL.XNull(.Rows(I)("ItemName"))
                        Dgl1.Item(Col1JobOrder, I).Tag = AgL.XNull(.Rows(I)("JobOrder"))
                        Dgl1.Item(Col1JobOrder, I).Value = AgL.XNull(.Rows(I)("JobOrderNo"))

                        Dgl1.Item(Col1Qty, I).Value = AgL.VNull(.Rows(I)("Qty"))
                        Dgl1.Item(Col1Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                        Dgl1.Item(Col1Rate, I).Value = AgL.VNull(.Rows(I)("Rate"))
                    Next I
                End If
            End With
            Calculation()
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

    Private Sub FrmJobConsumption_BaseEvent_Topctrl_tbEdit(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbEdit
        FAsignProcess()
    End Sub

    Private Sub FCreateHelpSubgroup()
        Dim strCond As String = ""
        If DtV_TypeSettings.Rows.Count > 0 Then
            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_AcGroup")) <> "" Then
                strCond += " And CharIndex('|' + Sg.GroupCode + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_AcGroup")) & "') > 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_SubgroupDivision")) <> "" Then
                strCond += " And CharIndex('|' + Sg.Div_Code + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_subGroupDivision")) & "') > 0 "
            End If

            If AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_SubgroupSite")) <> "" Then
                strCond += " And CharIndex('|' + Sg.Site_Code + '|','" & AgL.XNull(DtV_TypeSettings.Rows(0)("FilterInclude_subGroupSite")) & "') > 0 "
            End If
        End If

        mQry = " SELECT Sg.SubCode AS Code, Sg.Name AS JobWorker, H.Process, " &
                 " IFNull(Sg.IsDeleted,0) AS IsDeleted,  SG.Div_Code, " &
                 " IFNull(Sg.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') As Status " &
                 " FROM SubGroup Sg  " &
                 " LEFT JOIN JobWorkerProcess H   On Sg.SubCode = H.SubCode  " &
                 " Where IFNull(Sg.IsDeleted,0) = 0 " &
                 " And Sg.Status = '" & AgTemplate.ClsMain.EntryStatus.Active & "' " &
                 " And CharIndex('|' + '" & TxtDivision.Tag & "' + '|', IFNull(Sg.DivisionList,'|' + '" & TxtDivision.Tag & "' + '|')) > 0 " &
                 " And H.Process = '" & TxtProcess.Tag & "' " & strCond
        Dgl1.AgHelpDataSet(Col1JobWorker, 4) = AgL.FillData(mQry, AgL.GCn)
    End Sub
End Class
