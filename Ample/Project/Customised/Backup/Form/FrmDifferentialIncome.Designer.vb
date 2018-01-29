<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmDifferentialIncome
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmDifferentialIncome))
        Me.Label1 = New System.Windows.Forms.Label
        Me.Topctrl1 = New Topctrl.Topctrl
        Me.TxtDate_From = New AgControls.AgTextBox
        Me.LblDate_From = New System.Windows.Forms.Label
        Me.LblDate_FromReq = New System.Windows.Forms.Label
        Me.TxtDate_To = New AgControls.AgTextBox
        Me.LblDate_To = New System.Windows.Forms.Label
        Me.TxtV_Date = New AgControls.AgTextBox
        Me.LblV_Date = New System.Windows.Forms.Label
        Me.LblV_DateReq = New System.Windows.Forms.Label
        Me.TxtV_No = New AgControls.AgTextBox
        Me.LblV_No = New System.Windows.Forms.Label
        Me.TxtRemark = New AgControls.AgTextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.BtnApproved = New System.Windows.Forms.Button
        Me.TxtApproved = New System.Windows.Forms.TextBox
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.TxtModified = New System.Windows.Forms.TextBox
        Me.GrpUP = New System.Windows.Forms.GroupBox
        Me.TxtPrepared = New System.Windows.Forms.TextBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.BtnFillDetail = New System.Windows.Forms.Button
        Me.TxtPVMultiplier = New AgControls.AgTextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Pnl_DifferentialIncome = New System.Windows.Forms.Panel
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TP_Differential = New System.Windows.Forms.TabPage
        Me.TP_SaphireBonus = New System.Windows.Forms.TabPage
        Me.Pnl_SaphireBonus = New System.Windows.Forms.Panel
        Me.TxtNationalBV = New AgControls.AgTextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.TxtNationalPV = New AgControls.AgTextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GrpUP.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TP_Differential.SuspendLayout()
        Me.TP_SaphireBonus.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(622, 76)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(10, 7)
        Me.Label1.TabIndex = 117
        Me.Label1.Text = "Ä"
        '
        'Topctrl1
        '
        Me.Topctrl1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Comprehensive
        Me.Topctrl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Topctrl1.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.Topctrl1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Topctrl1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Topctrl1.Location = New System.Drawing.Point(0, 0)
        Me.Topctrl1.Mode = "Browse"
        Me.Topctrl1.Name = "Topctrl1"
        Me.Topctrl1.Size = New System.Drawing.Size(992, 41)
        Me.Topctrl1.TabIndex = 109
        Me.Topctrl1.tAdd = True
        Me.Topctrl1.tCancel = True
        Me.Topctrl1.tDel = True
        Me.Topctrl1.tDiscard = False
        Me.Topctrl1.tEdit = True
        Me.Topctrl1.tExit = True
        Me.Topctrl1.tFind = True
        Me.Topctrl1.tFirst = True
        Me.Topctrl1.tLast = True
        Me.Topctrl1.tNext = True
        Me.Topctrl1.tPrev = True
        Me.Topctrl1.tPrn = True
        Me.Topctrl1.tRef = True
        Me.Topctrl1.tSave = False
        Me.Topctrl1.tSite = True
        '
        'TxtDate_From
        '
        Me.TxtDate_From.AgAllowUserToEnableMasterHelp = False
        Me.TxtDate_From.AgMandatory = False
        Me.TxtDate_From.AgMasterHelp = False
        Me.TxtDate_From.AgNumberLeftPlaces = 0
        Me.TxtDate_From.AgNumberNegetiveAllow = False
        Me.TxtDate_From.AgNumberRightPlaces = 0
        Me.TxtDate_From.AgPickFromLastValue = False
        Me.TxtDate_From.AgRowFilter = ""
        Me.TxtDate_From.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtDate_From.AgSelectedValue = Nothing
        Me.TxtDate_From.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtDate_From.AgValueType = AgControls.AgTextBox.TxtValueType.Date_Value
        Me.TxtDate_From.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDate_From.Location = New System.Drawing.Point(374, 69)
        Me.TxtDate_From.Name = "TxtDate_From"
        Me.TxtDate_From.Size = New System.Drawing.Size(100, 21)
        Me.TxtDate_From.TabIndex = 123
        Me.TxtDate_From.Text = "TxtDate_From"
        '
        'LblDate_From
        '
        Me.LblDate_From.AutoSize = True
        Me.LblDate_From.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDate_From.Location = New System.Drawing.Point(252, 73)
        Me.LblDate_From.Name = "LblDate_From"
        Me.LblDate_From.Size = New System.Drawing.Size(67, 13)
        Me.LblDate_From.TabIndex = 106
        Me.LblDate_From.Text = "Date From"
        '
        'LblDate_FromReq
        '
        Me.LblDate_FromReq.AutoSize = True
        Me.LblDate_FromReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblDate_FromReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblDate_FromReq.Location = New System.Drawing.Point(357, 76)
        Me.LblDate_FromReq.Name = "LblDate_FromReq"
        Me.LblDate_FromReq.Size = New System.Drawing.Size(10, 7)
        Me.LblDate_FromReq.TabIndex = 108
        Me.LblDate_FromReq.Text = "Ä"
        '
        'TxtDate_To
        '
        Me.TxtDate_To.AgAllowUserToEnableMasterHelp = False
        Me.TxtDate_To.AgMandatory = False
        Me.TxtDate_To.AgMasterHelp = False
        Me.TxtDate_To.AgNumberLeftPlaces = 0
        Me.TxtDate_To.AgNumberNegetiveAllow = False
        Me.TxtDate_To.AgNumberRightPlaces = 0
        Me.TxtDate_To.AgPickFromLastValue = False
        Me.TxtDate_To.AgRowFilter = ""
        Me.TxtDate_To.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtDate_To.AgSelectedValue = Nothing
        Me.TxtDate_To.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtDate_To.AgValueType = AgControls.AgTextBox.TxtValueType.Date_Value
        Me.TxtDate_To.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDate_To.Location = New System.Drawing.Point(638, 69)
        Me.TxtDate_To.Name = "TxtDate_To"
        Me.TxtDate_To.Size = New System.Drawing.Size(100, 21)
        Me.TxtDate_To.TabIndex = 124
        Me.TxtDate_To.Text = "TxtDate_To"
        '
        'LblDate_To
        '
        Me.LblDate_To.AutoSize = True
        Me.LblDate_To.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDate_To.Location = New System.Drawing.Point(543, 73)
        Me.LblDate_To.Name = "LblDate_To"
        Me.LblDate_To.Size = New System.Drawing.Size(52, 13)
        Me.LblDate_To.TabIndex = 107
        Me.LblDate_To.Text = "Date To"
        '
        'TxtV_Date
        '
        Me.TxtV_Date.AgAllowUserToEnableMasterHelp = False
        Me.TxtV_Date.AgMandatory = False
        Me.TxtV_Date.AgMasterHelp = False
        Me.TxtV_Date.AgNumberLeftPlaces = 0
        Me.TxtV_Date.AgNumberNegetiveAllow = False
        Me.TxtV_Date.AgNumberRightPlaces = 0
        Me.TxtV_Date.AgPickFromLastValue = False
        Me.TxtV_Date.AgRowFilter = ""
        Me.TxtV_Date.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtV_Date.AgSelectedValue = Nothing
        Me.TxtV_Date.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtV_Date.AgValueType = AgControls.AgTextBox.TxtValueType.Date_Value
        Me.TxtV_Date.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtV_Date.Location = New System.Drawing.Point(374, 46)
        Me.TxtV_Date.Name = "TxtV_Date"
        Me.TxtV_Date.Size = New System.Drawing.Size(100, 21)
        Me.TxtV_Date.TabIndex = 121
        Me.TxtV_Date.Text = "TxtV_Date"
        '
        'LblV_Date
        '
        Me.LblV_Date.AutoSize = True
        Me.LblV_Date.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblV_Date.Location = New System.Drawing.Point(252, 50)
        Me.LblV_Date.Name = "LblV_Date"
        Me.LblV_Date.Size = New System.Drawing.Size(85, 13)
        Me.LblV_Date.TabIndex = 120
        Me.LblV_Date.Text = "Voucher Date"
        '
        'LblV_DateReq
        '
        Me.LblV_DateReq.AutoSize = True
        Me.LblV_DateReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblV_DateReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblV_DateReq.Location = New System.Drawing.Point(357, 53)
        Me.LblV_DateReq.Name = "LblV_DateReq"
        Me.LblV_DateReq.Size = New System.Drawing.Size(10, 7)
        Me.LblV_DateReq.TabIndex = 118
        Me.LblV_DateReq.Text = "Ä"
        '
        'TxtV_No
        '
        Me.TxtV_No.AgAllowUserToEnableMasterHelp = False
        Me.TxtV_No.AgMandatory = False
        Me.TxtV_No.AgMasterHelp = False
        Me.TxtV_No.AgNumberLeftPlaces = 0
        Me.TxtV_No.AgNumberNegetiveAllow = False
        Me.TxtV_No.AgNumberRightPlaces = 0
        Me.TxtV_No.AgPickFromLastValue = False
        Me.TxtV_No.AgRowFilter = ""
        Me.TxtV_No.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtV_No.AgSelectedValue = Nothing
        Me.TxtV_No.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtV_No.AgValueType = AgControls.AgTextBox.TxtValueType.Number_Value
        Me.TxtV_No.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtV_No.Location = New System.Drawing.Point(638, 46)
        Me.TxtV_No.Name = "TxtV_No"
        Me.TxtV_No.Size = New System.Drawing.Size(100, 21)
        Me.TxtV_No.TabIndex = 122
        Me.TxtV_No.Text = "TxtV_No"
        Me.TxtV_No.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'LblV_No
        '
        Me.LblV_No.AutoSize = True
        Me.LblV_No.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblV_No.Location = New System.Drawing.Point(543, 50)
        Me.LblV_No.Name = "LblV_No"
        Me.LblV_No.Size = New System.Drawing.Size(77, 13)
        Me.LblV_No.TabIndex = 119
        Me.LblV_No.Text = "Voucher No."
        '
        'TxtRemark
        '
        Me.TxtRemark.AgAllowUserToEnableMasterHelp = False
        Me.TxtRemark.AgMandatory = False
        Me.TxtRemark.AgMasterHelp = False
        Me.TxtRemark.AgNumberLeftPlaces = 0
        Me.TxtRemark.AgNumberNegetiveAllow = False
        Me.TxtRemark.AgNumberRightPlaces = 0
        Me.TxtRemark.AgPickFromLastValue = False
        Me.TxtRemark.AgRowFilter = ""
        Me.TxtRemark.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtRemark.AgSelectedValue = Nothing
        Me.TxtRemark.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtRemark.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtRemark.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtRemark.Location = New System.Drawing.Point(374, 92)
        Me.TxtRemark.MaxLength = 255
        Me.TxtRemark.Name = "TxtRemark"
        Me.TxtRemark.Size = New System.Drawing.Size(364, 21)
        Me.TxtRemark.TabIndex = 125
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(252, 96)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(52, 13)
        Me.Label2.TabIndex = 123
        Me.Label2.Text = "Remark"
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.BtnApproved)
        Me.GroupBox1.Controls.Add(Me.TxtApproved)
        Me.GroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox1.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.GroupBox1.ForeColor = System.Drawing.Color.Maroon
        Me.GroupBox1.Location = New System.Drawing.Point(764, 563)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(216, 51)
        Me.GroupBox1.TabIndex = 207
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Tag = "UP"
        Me.GroupBox1.Text = "Approved By "
        '
        'BtnApproved
        '
        Me.BtnApproved.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnApproved.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnApproved.Font = New System.Drawing.Font("Arial", 10.25!)
        Me.BtnApproved.Image = CType(resources.GetObject("BtnApproved.Image"), System.Drawing.Image)
        Me.BtnApproved.Location = New System.Drawing.Point(13, 19)
        Me.BtnApproved.Name = "BtnApproved"
        Me.BtnApproved.Size = New System.Drawing.Size(23, 23)
        Me.BtnApproved.TabIndex = 36
        Me.BtnApproved.UseVisualStyleBackColor = True
        '
        'TxtApproved
        '
        Me.TxtApproved.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.TxtApproved.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtApproved.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtApproved.Location = New System.Drawing.Point(43, 21)
        Me.TxtApproved.Name = "TxtApproved"
        Me.TxtApproved.Size = New System.Drawing.Size(158, 18)
        Me.TxtApproved.TabIndex = 0
        Me.TxtApproved.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'GroupBox4
        '
        Me.GroupBox4.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GroupBox4.Controls.Add(Me.TxtModified)
        Me.GroupBox4.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox4.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.GroupBox4.ForeColor = System.Drawing.Color.Maroon
        Me.GroupBox4.Location = New System.Drawing.Point(403, 562)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(186, 51)
        Me.GroupBox4.TabIndex = 205
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Tag = "TR"
        Me.GroupBox4.Text = "Modified By "
        Me.GroupBox4.Visible = False
        '
        'TxtModified
        '
        Me.TxtModified.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.TxtModified.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtModified.Enabled = False
        Me.TxtModified.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtModified.Location = New System.Drawing.Point(15, 21)
        Me.TxtModified.Name = "TxtModified"
        Me.TxtModified.Size = New System.Drawing.Size(158, 18)
        Me.TxtModified.TabIndex = 0
        Me.TxtModified.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'GrpUP
        '
        Me.GrpUP.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GrpUP.Controls.Add(Me.TxtPrepared)
        Me.GrpUP.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GrpUP.Font = New System.Drawing.Font("Arial", 9.75!)
        Me.GrpUP.ForeColor = System.Drawing.Color.Maroon
        Me.GrpUP.Location = New System.Drawing.Point(12, 562)
        Me.GrpUP.Name = "GrpUP"
        Me.GrpUP.Size = New System.Drawing.Size(186, 51)
        Me.GrpUP.TabIndex = 204
        Me.GrpUP.TabStop = False
        Me.GrpUP.Tag = "TR"
        Me.GrpUP.Text = "Prepared By "
        '
        'TxtPrepared
        '
        Me.TxtPrepared.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.TxtPrepared.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtPrepared.Enabled = False
        Me.TxtPrepared.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPrepared.Location = New System.Drawing.Point(15, 21)
        Me.TxtPrepared.Name = "TxtPrepared"
        Me.TxtPrepared.Size = New System.Drawing.Size(158, 18)
        Me.TxtPrepared.TabIndex = 0
        Me.TxtPrepared.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.BackColor = System.Drawing.Color.WhiteSmoke
        Me.GroupBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox2.Location = New System.Drawing.Point(12, 548)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(968, 9)
        Me.GroupBox2.TabIndex = 206
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Tag = ""
        '
        'BtnFillDetail
        '
        Me.BtnFillDetail.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnFillDetail.Font = New System.Drawing.Font("Lucida Console", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnFillDetail.Location = New System.Drawing.Point(519, 115)
        Me.BtnFillDetail.Name = "BtnFillDetail"
        Me.BtnFillDetail.Size = New System.Drawing.Size(219, 21)
        Me.BtnFillDetail.TabIndex = 209
        Me.BtnFillDetail.Text = "Calculate Income"
        Me.BtnFillDetail.UseVisualStyleBackColor = True
        '
        'TxtPVMultiplier
        '
        Me.TxtPVMultiplier.AgAllowUserToEnableMasterHelp = False
        Me.TxtPVMultiplier.AgMandatory = False
        Me.TxtPVMultiplier.AgMasterHelp = False
        Me.TxtPVMultiplier.AgNumberLeftPlaces = 5
        Me.TxtPVMultiplier.AgNumberNegetiveAllow = False
        Me.TxtPVMultiplier.AgNumberRightPlaces = 2
        Me.TxtPVMultiplier.AgPickFromLastValue = False
        Me.TxtPVMultiplier.AgRowFilter = ""
        Me.TxtPVMultiplier.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtPVMultiplier.AgSelectedValue = Nothing
        Me.TxtPVMultiplier.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtPVMultiplier.AgValueType = AgControls.AgTextBox.TxtValueType.Number_Value
        Me.TxtPVMultiplier.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPVMultiplier.Location = New System.Drawing.Point(374, 115)
        Me.TxtPVMultiplier.Name = "TxtPVMultiplier"
        Me.TxtPVMultiplier.Size = New System.Drawing.Size(100, 21)
        Me.TxtPVMultiplier.TabIndex = 213
        Me.TxtPVMultiplier.Text = "AgTextBox1"
        Me.TxtPVMultiplier.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(252, 119)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(85, 13)
        Me.Label4.TabIndex = 212
        Me.Label4.Text = "P.V. Multiplier"
        '
        'Pnl_DifferentialIncome
        '
        Me.Pnl_DifferentialIncome.Location = New System.Drawing.Point(7, 6)
        Me.Pnl_DifferentialIncome.Name = "Pnl_DifferentialIncome"
        Me.Pnl_DifferentialIncome.Size = New System.Drawing.Size(942, 340)
        Me.Pnl_DifferentialIncome.TabIndex = 214
        '
        'TabControl1
        '
        Me.TabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons
        Me.TabControl1.Controls.Add(Me.TP_Differential)
        Me.TabControl1.Controls.Add(Me.TP_SaphireBonus)
        Me.TabControl1.Location = New System.Drawing.Point(12, 165)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(968, 381)
        Me.TabControl1.TabIndex = 215
        '
        'TP_Differential
        '
        Me.TP_Differential.Controls.Add(Me.Pnl_DifferentialIncome)
        Me.TP_Differential.Location = New System.Drawing.Point(4, 25)
        Me.TP_Differential.Name = "TP_Differential"
        Me.TP_Differential.Padding = New System.Windows.Forms.Padding(3)
        Me.TP_Differential.Size = New System.Drawing.Size(960, 352)
        Me.TP_Differential.TabIndex = 0
        Me.TP_Differential.Text = "Differential Income"
        Me.TP_Differential.UseVisualStyleBackColor = True
        '
        'TP_SaphireBonus
        '
        Me.TP_SaphireBonus.Controls.Add(Me.Pnl_SaphireBonus)
        Me.TP_SaphireBonus.Location = New System.Drawing.Point(4, 25)
        Me.TP_SaphireBonus.Name = "TP_SaphireBonus"
        Me.TP_SaphireBonus.Padding = New System.Windows.Forms.Padding(3)
        Me.TP_SaphireBonus.Size = New System.Drawing.Size(960, 352)
        Me.TP_SaphireBonus.TabIndex = 1
        Me.TP_SaphireBonus.Text = "Saphire Bonus"
        Me.TP_SaphireBonus.UseVisualStyleBackColor = True
        '
        'Pnl_SaphireBonus
        '
        Me.Pnl_SaphireBonus.Location = New System.Drawing.Point(7, 6)
        Me.Pnl_SaphireBonus.Name = "Pnl_SaphireBonus"
        Me.Pnl_SaphireBonus.Size = New System.Drawing.Size(942, 340)
        Me.Pnl_SaphireBonus.TabIndex = 215
        '
        'TxtNationalBV
        '
        Me.TxtNationalBV.AgAllowUserToEnableMasterHelp = False
        Me.TxtNationalBV.AgMandatory = False
        Me.TxtNationalBV.AgMasterHelp = False
        Me.TxtNationalBV.AgNumberLeftPlaces = 5
        Me.TxtNationalBV.AgNumberNegetiveAllow = False
        Me.TxtNationalBV.AgNumberRightPlaces = 2
        Me.TxtNationalBV.AgPickFromLastValue = False
        Me.TxtNationalBV.AgRowFilter = ""
        Me.TxtNationalBV.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtNationalBV.AgSelectedValue = Nothing
        Me.TxtNationalBV.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtNationalBV.AgValueType = AgControls.AgTextBox.TxtValueType.Number_Value
        Me.TxtNationalBV.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtNationalBV.Location = New System.Drawing.Point(638, 138)
        Me.TxtNationalBV.Name = "TxtNationalBV"
        Me.TxtNationalBV.Size = New System.Drawing.Size(100, 21)
        Me.TxtNationalBV.TabIndex = 217
        Me.TxtNationalBV.Text = "AgTextBox1"
        Me.TxtNationalBV.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(516, 142)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(81, 13)
        Me.Label3.TabIndex = 216
        Me.Label3.Text = "National B.V."
        '
        'TxtNationalPV
        '
        Me.TxtNationalPV.AgAllowUserToEnableMasterHelp = False
        Me.TxtNationalPV.AgMandatory = False
        Me.TxtNationalPV.AgMasterHelp = False
        Me.TxtNationalPV.AgNumberLeftPlaces = 5
        Me.TxtNationalPV.AgNumberNegetiveAllow = False
        Me.TxtNationalPV.AgNumberRightPlaces = 2
        Me.TxtNationalPV.AgPickFromLastValue = False
        Me.TxtNationalPV.AgRowFilter = ""
        Me.TxtNationalPV.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtNationalPV.AgSelectedValue = Nothing
        Me.TxtNationalPV.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtNationalPV.AgValueType = AgControls.AgTextBox.TxtValueType.Number_Value
        Me.TxtNationalPV.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtNationalPV.Location = New System.Drawing.Point(374, 138)
        Me.TxtNationalPV.Name = "TxtNationalPV"
        Me.TxtNationalPV.Size = New System.Drawing.Size(100, 21)
        Me.TxtNationalPV.TabIndex = 219
        Me.TxtNationalPV.Text = "AgTextBox1"
        Me.TxtNationalPV.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(252, 142)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(80, 13)
        Me.Label5.TabIndex = 218
        Me.Label5.Text = "National P.V."
        '
        'FrmDifferentialIncome
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(992, 616)
        Me.Controls.Add(Me.TxtNationalPV)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.TxtNationalBV)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.TxtPVMultiplier)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.BtnFillDetail)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GrpUP)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.TxtRemark)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TxtV_Date)
        Me.Controls.Add(Me.LblV_Date)
        Me.Controls.Add(Me.LblV_DateReq)
        Me.Controls.Add(Me.TxtV_No)
        Me.Controls.Add(Me.LblV_No)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Topctrl1)
        Me.Controls.Add(Me.TxtDate_From)
        Me.Controls.Add(Me.LblDate_From)
        Me.Controls.Add(Me.LblDate_FromReq)
        Me.Controls.Add(Me.TxtDate_To)
        Me.Controls.Add(Me.LblDate_To)
        Me.KeyPreview = True
        Me.Name = "FrmDifferentialIncome"
        Me.Text = "Binary Income"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.GrpUP.ResumeLayout(False)
        Me.GrpUP.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TP_Differential.ResumeLayout(False)
        Me.TP_SaphireBonus.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Topctrl1 As Topctrl.Topctrl
    Friend WithEvents TxtDate_From As AgControls.AgTextBox
    Friend WithEvents LblDate_From As System.Windows.Forms.Label
    Friend WithEvents LblDate_FromReq As System.Windows.Forms.Label
    Friend WithEvents TxtDate_To As AgControls.AgTextBox
    Friend WithEvents LblDate_To As System.Windows.Forms.Label
    Friend WithEvents TxtV_Date As AgControls.AgTextBox
    Friend WithEvents LblV_Date As System.Windows.Forms.Label
    Friend WithEvents LblV_DateReq As System.Windows.Forms.Label
    Friend WithEvents TxtV_No As AgControls.AgTextBox
    Friend WithEvents LblV_No As System.Windows.Forms.Label
    Friend WithEvents TxtRemark As AgControls.AgTextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents BtnApproved As System.Windows.Forms.Button
    Friend WithEvents TxtApproved As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents TxtModified As System.Windows.Forms.TextBox
    Friend WithEvents GrpUP As System.Windows.Forms.GroupBox
    Friend WithEvents TxtPrepared As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents BtnFillDetail As System.Windows.Forms.Button
    Friend WithEvents TxtPVMultiplier As AgControls.AgTextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Pnl_DifferentialIncome As System.Windows.Forms.Panel
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TP_Differential As System.Windows.Forms.TabPage
    Friend WithEvents TP_SaphireBonus As System.Windows.Forms.TabPage
    Friend WithEvents Pnl_SaphireBonus As System.Windows.Forms.Panel
    Friend WithEvents TxtNationalBV As AgControls.AgTextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TxtNationalPV As AgControls.AgTextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
End Class
