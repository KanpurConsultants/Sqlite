Public Class FrmItem
    Inherits AgTemplate.TempMaster

    Dim mQry$ = ""

    Public Sub New(ByVal StrUPVar As String, ByVal DTUP As DataTable)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Topctrl1.FSetParent(Me, StrUPVar, DTUP)
        Topctrl1.SetDisp(True)
    End Sub

#Region "Designer Code"
Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label
        Me.TxtDescription = New AgControls.AgTextBox
        Me.LblDescription = New System.Windows.Forms.Label
        Me.TxtUnit = New AgControls.AgTextBox
        Me.LblUnit = New System.Windows.Forms.Label
        Me.LblManualCodeReq = New System.Windows.Forms.Label
        Me.TxtManualCode = New AgControls.AgTextBox
        Me.LblManualCode = New System.Windows.Forms.Label
        Me.TxtItemGroup = New AgControls.AgTextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.TxtRate = New AgControls.AgTextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.TxtMeasureUnit = New AgControls.AgTextBox
        Me.LblMeasureUnit = New System.Windows.Forms.Label
        Me.TxtMeasure = New AgControls.AgTextBox
        Me.LblMeasure = New System.Windows.Forms.Label
        Me.TxtPointValue = New AgControls.AgTextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.TxtBusinessVolume = New AgControls.AgTextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label10 = New System.Windows.Forms.Label
        Me.TxtSalesTaxGroup = New AgControls.AgTextBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.GrpUP.SuspendLayout()
        Me.GBoxEntryType.SuspendLayout()
        Me.GBoxMoveToLog.SuspendLayout()
        Me.GBoxApprove.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GBoxDivision.SuspendLayout()
        CType(Me.DTMaster, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Topctrl1
        '
        Me.Topctrl1.Size = New System.Drawing.Size(862, 41)
        Me.Topctrl1.TabIndex = 10
        Me.Topctrl1.tAdd = False
        Me.Topctrl1.tDel = False
        Me.Topctrl1.tEdit = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Location = New System.Drawing.Point(0, 276)
        Me.GroupBox1.Size = New System.Drawing.Size(904, 4)
        '
        'GrpUP
        '
        Me.GrpUP.Location = New System.Drawing.Point(14, 280)
        '
        'TxtEntryBy
        '
        Me.TxtEntryBy.Tag = ""
        Me.TxtEntryBy.Text = ""
        '
        'GBoxEntryType
        '
        Me.GBoxEntryType.Location = New System.Drawing.Point(148, 280)
        '
        'TxtEntryType
        '
        Me.TxtEntryType.Tag = ""
        '
        'GBoxMoveToLog
        '
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(554, 280)
        '
        'TxtMoveToLog
        '
        Me.TxtMoveToLog.Tag = ""
        '
        'GBoxApprove
        '
        Me.GBoxApprove.Location = New System.Drawing.Point(401, 280)
        Me.GBoxApprove.Text = "Approved By"
        '
        'TxtApproveBy
        '
        Me.TxtApproveBy.Location = New System.Drawing.Point(3, 23)
        Me.TxtApproveBy.Size = New System.Drawing.Size(136, 18)
        Me.TxtApproveBy.Tag = ""
        '
        'GroupBox2
        '
        Me.GroupBox2.Location = New System.Drawing.Point(704, 280)
        '
        'GBoxDivision
        '
        Me.GBoxDivision.Location = New System.Drawing.Point(278, 280)
        Me.GBoxDivision.Text = "`"
        '
        'TxtDivision
        '
        Me.TxtDivision.AgSelectedValue = ""
        Me.TxtDivision.Tag = ""
        '
        'TxtStatus
        '
        Me.TxtStatus.AgSelectedValue = ""
        Me.TxtStatus.Location = New System.Drawing.Point(3, 23)
        Me.TxtStatus.Size = New System.Drawing.Size(142, 18)
        Me.TxtStatus.Tag = ""
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(292, 108)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(10, 7)
        Me.Label1.TabIndex = 666
        Me.Label1.Text = "Ä"
        '
        'TxtDescription
        '
        Me.TxtDescription.AgAllowUserToEnableMasterHelp = False
        Me.TxtDescription.AgMandatory = True
        Me.TxtDescription.AgMasterHelp = True
        Me.TxtDescription.AgNumberLeftPlaces = 0
        Me.TxtDescription.AgNumberNegetiveAllow = False
        Me.TxtDescription.AgNumberRightPlaces = 0
        Me.TxtDescription.AgPickFromLastValue = False
        Me.TxtDescription.AgRowFilter = ""
        Me.TxtDescription.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtDescription.AgSelectedValue = Nothing
        Me.TxtDescription.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtDescription.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtDescription.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtDescription.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDescription.Location = New System.Drawing.Point(308, 100)
        Me.TxtDescription.MaxLength = 50
        Me.TxtDescription.Name = "TxtDescription"
        Me.TxtDescription.Size = New System.Drawing.Size(351, 18)
        Me.TxtDescription.TabIndex = 1
        '
        'LblDescription
        '
        Me.LblDescription.AutoSize = True
        Me.LblDescription.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDescription.Location = New System.Drawing.Point(212, 101)
        Me.LblDescription.Name = "LblDescription"
        Me.LblDescription.Size = New System.Drawing.Size(71, 16)
        Me.LblDescription.TabIndex = 661
        Me.LblDescription.Text = "Item Name"
        '
        'TxtUnit
        '
        Me.TxtUnit.AgAllowUserToEnableMasterHelp = False
        Me.TxtUnit.AgMandatory = True
        Me.TxtUnit.AgMasterHelp = False
        Me.TxtUnit.AgNumberLeftPlaces = 0
        Me.TxtUnit.AgNumberNegetiveAllow = False
        Me.TxtUnit.AgNumberRightPlaces = 0
        Me.TxtUnit.AgPickFromLastValue = False
        Me.TxtUnit.AgRowFilter = ""
        Me.TxtUnit.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtUnit.AgSelectedValue = Nothing
        Me.TxtUnit.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtUnit.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtUnit.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtUnit.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtUnit.Location = New System.Drawing.Point(308, 120)
        Me.TxtUnit.MaxLength = 20
        Me.TxtUnit.Name = "TxtUnit"
        Me.TxtUnit.Size = New System.Drawing.Size(112, 18)
        Me.TxtUnit.TabIndex = 2
        '
        'LblUnit
        '
        Me.LblUnit.AutoSize = True
        Me.LblUnit.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblUnit.Location = New System.Drawing.Point(212, 120)
        Me.LblUnit.Name = "LblUnit"
        Me.LblUnit.Size = New System.Drawing.Size(31, 16)
        Me.LblUnit.TabIndex = 685
        Me.LblUnit.Text = "Unit"
        '
        'LblManualCodeReq
        '
        Me.LblManualCodeReq.AutoSize = True
        Me.LblManualCodeReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblManualCodeReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblManualCodeReq.Location = New System.Drawing.Point(292, 88)
        Me.LblManualCodeReq.Name = "LblManualCodeReq"
        Me.LblManualCodeReq.Size = New System.Drawing.Size(10, 7)
        Me.LblManualCodeReq.TabIndex = 690
        Me.LblManualCodeReq.Text = "Ä"
        '
        'TxtManualCode
        '
        Me.TxtManualCode.AgAllowUserToEnableMasterHelp = False
        Me.TxtManualCode.AgMandatory = True
        Me.TxtManualCode.AgMasterHelp = True
        Me.TxtManualCode.AgNumberLeftPlaces = 0
        Me.TxtManualCode.AgNumberNegetiveAllow = False
        Me.TxtManualCode.AgNumberRightPlaces = 0
        Me.TxtManualCode.AgPickFromLastValue = False
        Me.TxtManualCode.AgRowFilter = ""
        Me.TxtManualCode.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtManualCode.AgSelectedValue = Nothing
        Me.TxtManualCode.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtManualCode.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtManualCode.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtManualCode.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtManualCode.Location = New System.Drawing.Point(308, 80)
        Me.TxtManualCode.MaxLength = 20
        Me.TxtManualCode.Name = "TxtManualCode"
        Me.TxtManualCode.Size = New System.Drawing.Size(351, 18)
        Me.TxtManualCode.TabIndex = 0
        '
        'LblManualCode
        '
        Me.LblManualCode.AutoSize = True
        Me.LblManualCode.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblManualCode.Location = New System.Drawing.Point(212, 81)
        Me.LblManualCode.Name = "LblManualCode"
        Me.LblManualCode.Size = New System.Drawing.Size(67, 16)
        Me.LblManualCode.TabIndex = 689
        Me.LblManualCode.Text = "Item Code"
        '
        'TxtItemGroup
        '
        Me.TxtItemGroup.AgAllowUserToEnableMasterHelp = False
        Me.TxtItemGroup.AgMandatory = True
        Me.TxtItemGroup.AgMasterHelp = False
        Me.TxtItemGroup.AgNumberLeftPlaces = 0
        Me.TxtItemGroup.AgNumberNegetiveAllow = False
        Me.TxtItemGroup.AgNumberRightPlaces = 0
        Me.TxtItemGroup.AgPickFromLastValue = False
        Me.TxtItemGroup.AgRowFilter = ""
        Me.TxtItemGroup.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtItemGroup.AgSelectedValue = Nothing
        Me.TxtItemGroup.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtItemGroup.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtItemGroup.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtItemGroup.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtItemGroup.Location = New System.Drawing.Point(544, 120)
        Me.TxtItemGroup.MaxLength = 20
        Me.TxtItemGroup.Name = "TxtItemGroup"
        Me.TxtItemGroup.Size = New System.Drawing.Size(115, 18)
        Me.TxtItemGroup.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(427, 122)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(72, 16)
        Me.Label2.TabIndex = 697
        Me.Label2.Text = "Item Group"
        '
        'TxtRate
        '
        Me.TxtRate.AgAllowUserToEnableMasterHelp = False
        Me.TxtRate.AgMandatory = False
        Me.TxtRate.AgMasterHelp = False
        Me.TxtRate.AgNumberLeftPlaces = 8
        Me.TxtRate.AgNumberNegetiveAllow = False
        Me.TxtRate.AgNumberRightPlaces = 2
        Me.TxtRate.AgPickFromLastValue = False
        Me.TxtRate.AgRowFilter = ""
        Me.TxtRate.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtRate.AgSelectedValue = Nothing
        Me.TxtRate.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtRate.AgValueType = AgControls.AgTextBox.TxtValueType.Number_Value
        Me.TxtRate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtRate.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtRate.Location = New System.Drawing.Point(308, 140)
        Me.TxtRate.MaxLength = 20
        Me.TxtRate.Name = "TxtRate"
        Me.TxtRate.Size = New System.Drawing.Size(112, 18)
        Me.TxtRate.TabIndex = 4
        Me.TxtRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(212, 140)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(35, 16)
        Me.Label3.TabIndex = 700
        Me.Label3.Text = "Rate"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(532, 128)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(10, 7)
        Me.Label4.TabIndex = 701
        Me.Label4.Text = "Ä"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(292, 126)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(10, 7)
        Me.Label5.TabIndex = 702
        Me.Label5.Text = "Ä"
        '
        'TxtMeasureUnit
        '
        Me.TxtMeasureUnit.AgAllowUserToEnableMasterHelp = False
        Me.TxtMeasureUnit.AgMandatory = False
        Me.TxtMeasureUnit.AgMasterHelp = False
        Me.TxtMeasureUnit.AgNumberLeftPlaces = 0
        Me.TxtMeasureUnit.AgNumberNegetiveAllow = False
        Me.TxtMeasureUnit.AgNumberRightPlaces = 0
        Me.TxtMeasureUnit.AgPickFromLastValue = False
        Me.TxtMeasureUnit.AgRowFilter = ""
        Me.TxtMeasureUnit.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtMeasureUnit.AgSelectedValue = Nothing
        Me.TxtMeasureUnit.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtMeasureUnit.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtMeasureUnit.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtMeasureUnit.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtMeasureUnit.Location = New System.Drawing.Point(544, 160)
        Me.TxtMeasureUnit.MaxLength = 20
        Me.TxtMeasureUnit.Name = "TxtMeasureUnit"
        Me.TxtMeasureUnit.Size = New System.Drawing.Size(115, 18)
        Me.TxtMeasureUnit.TabIndex = 7
        '
        'LblMeasureUnit
        '
        Me.LblMeasureUnit.AutoSize = True
        Me.LblMeasureUnit.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblMeasureUnit.Location = New System.Drawing.Point(427, 162)
        Me.LblMeasureUnit.Name = "LblMeasureUnit"
        Me.LblMeasureUnit.Size = New System.Drawing.Size(85, 16)
        Me.LblMeasureUnit.TabIndex = 704
        Me.LblMeasureUnit.Text = "Measure Unit"
        '
        'TxtMeasure
        '
        Me.TxtMeasure.AgAllowUserToEnableMasterHelp = False
        Me.TxtMeasure.AgMandatory = False
        Me.TxtMeasure.AgMasterHelp = False
        Me.TxtMeasure.AgNumberLeftPlaces = 8
        Me.TxtMeasure.AgNumberNegetiveAllow = False
        Me.TxtMeasure.AgNumberRightPlaces = 2
        Me.TxtMeasure.AgPickFromLastValue = False
        Me.TxtMeasure.AgRowFilter = ""
        Me.TxtMeasure.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtMeasure.AgSelectedValue = Nothing
        Me.TxtMeasure.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtMeasure.AgValueType = AgControls.AgTextBox.TxtValueType.Number_Value
        Me.TxtMeasure.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtMeasure.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtMeasure.Location = New System.Drawing.Point(308, 160)
        Me.TxtMeasure.MaxLength = 20
        Me.TxtMeasure.Name = "TxtMeasure"
        Me.TxtMeasure.Size = New System.Drawing.Size(112, 18)
        Me.TxtMeasure.TabIndex = 6
        Me.TxtMeasure.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'LblMeasure
        '
        Me.LblMeasure.AutoSize = True
        Me.LblMeasure.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblMeasure.Location = New System.Drawing.Point(212, 162)
        Me.LblMeasure.Name = "LblMeasure"
        Me.LblMeasure.Size = New System.Drawing.Size(58, 16)
        Me.LblMeasure.TabIndex = 706
        Me.LblMeasure.Text = "Measure"
        '
        'TxtPointValue
        '
        Me.TxtPointValue.AgAllowUserToEnableMasterHelp = False
        Me.TxtPointValue.AgMandatory = True
        Me.TxtPointValue.AgMasterHelp = False
        Me.TxtPointValue.AgNumberLeftPlaces = 8
        Me.TxtPointValue.AgNumberNegetiveAllow = False
        Me.TxtPointValue.AgNumberRightPlaces = 4
        Me.TxtPointValue.AgPickFromLastValue = False
        Me.TxtPointValue.AgRowFilter = ""
        Me.TxtPointValue.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtPointValue.AgSelectedValue = Nothing
        Me.TxtPointValue.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtPointValue.AgValueType = AgControls.AgTextBox.TxtValueType.Number_Value
        Me.TxtPointValue.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtPointValue.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPointValue.Location = New System.Drawing.Point(308, 180)
        Me.TxtPointValue.MaxLength = 20
        Me.TxtPointValue.Name = "TxtPointValue"
        Me.TxtPointValue.Size = New System.Drawing.Size(112, 18)
        Me.TxtPointValue.TabIndex = 8
        Me.TxtPointValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(212, 182)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(74, 16)
        Me.Label6.TabIndex = 710
        Me.Label6.Text = "Point Value"
        '
        'TxtBusinessVolume
        '
        Me.TxtBusinessVolume.AgAllowUserToEnableMasterHelp = False
        Me.TxtBusinessVolume.AgMandatory = True
        Me.TxtBusinessVolume.AgMasterHelp = False
        Me.TxtBusinessVolume.AgNumberLeftPlaces = 8
        Me.TxtBusinessVolume.AgNumberNegetiveAllow = False
        Me.TxtBusinessVolume.AgNumberRightPlaces = 4
        Me.TxtBusinessVolume.AgPickFromLastValue = False
        Me.TxtBusinessVolume.AgRowFilter = ""
        Me.TxtBusinessVolume.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtBusinessVolume.AgSelectedValue = Nothing
        Me.TxtBusinessVolume.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtBusinessVolume.AgValueType = AgControls.AgTextBox.TxtValueType.Number_Value
        Me.TxtBusinessVolume.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtBusinessVolume.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtBusinessVolume.Location = New System.Drawing.Point(544, 180)
        Me.TxtBusinessVolume.MaxLength = 20
        Me.TxtBusinessVolume.Name = "TxtBusinessVolume"
        Me.TxtBusinessVolume.Size = New System.Drawing.Size(115, 18)
        Me.TxtBusinessVolume.TabIndex = 9
        Me.TxtBusinessVolume.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(427, 182)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(109, 16)
        Me.Label7.TabIndex = 709
        Me.Label7.Text = "Business Volume"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label8.Location = New System.Drawing.Point(532, 188)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(10, 7)
        Me.Label8.TabIndex = 711
        Me.Label8.Text = "Ä"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label9.Location = New System.Drawing.Point(292, 188)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(10, 7)
        Me.Label9.TabIndex = 712
        Me.Label9.Text = "Ä"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label10.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label10.Location = New System.Drawing.Point(532, 148)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(10, 7)
        Me.Label10.TabIndex = 715
        Me.Label10.Text = "Ä"
        '
        'TxtSalesTaxGroup
        '
        Me.TxtSalesTaxGroup.AgAllowUserToEnableMasterHelp = False
        Me.TxtSalesTaxGroup.AgMandatory = True
        Me.TxtSalesTaxGroup.AgMasterHelp = False
        Me.TxtSalesTaxGroup.AgNumberLeftPlaces = 0
        Me.TxtSalesTaxGroup.AgNumberNegetiveAllow = False
        Me.TxtSalesTaxGroup.AgNumberRightPlaces = 0
        Me.TxtSalesTaxGroup.AgPickFromLastValue = False
        Me.TxtSalesTaxGroup.AgRowFilter = ""
        Me.TxtSalesTaxGroup.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtSalesTaxGroup.AgSelectedValue = Nothing
        Me.TxtSalesTaxGroup.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtSalesTaxGroup.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtSalesTaxGroup.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtSalesTaxGroup.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtSalesTaxGroup.Location = New System.Drawing.Point(544, 140)
        Me.TxtSalesTaxGroup.MaxLength = 20
        Me.TxtSalesTaxGroup.Name = "TxtSalesTaxGroup"
        Me.TxtSalesTaxGroup.Size = New System.Drawing.Size(115, 18)
        Me.TxtSalesTaxGroup.TabIndex = 5
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(427, 142)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(104, 16)
        Me.Label11.TabIndex = 714
        Me.Label11.Text = "Sales Tax Group"
        '
        'FrmItem
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(862, 324)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.TxtSalesTaxGroup)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.TxtPointValue)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.TxtBusinessVolume)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.TxtMeasure)
        Me.Controls.Add(Me.LblMeasure)
        Me.Controls.Add(Me.TxtMeasureUnit)
        Me.Controls.Add(Me.LblMeasureUnit)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TxtRate)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TxtItemGroup)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.LblManualCodeReq)
        Me.Controls.Add(Me.TxtManualCode)
        Me.Controls.Add(Me.LblManualCode)
        Me.Controls.Add(Me.TxtUnit)
        Me.Controls.Add(Me.LblUnit)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TxtDescription)
        Me.Controls.Add(Me.LblDescription)
        Me.Name = "FrmItem"
        Me.Text = "Quality Master"
        Me.Controls.SetChildIndex(Me.GBoxDivision, 0)
        Me.Controls.SetChildIndex(Me.GroupBox2, 0)
        Me.Controls.SetChildIndex(Me.Topctrl1, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.GrpUP, 0)
        Me.Controls.SetChildIndex(Me.GBoxEntryType, 0)
        Me.Controls.SetChildIndex(Me.GBoxApprove, 0)
        Me.Controls.SetChildIndex(Me.GBoxMoveToLog, 0)
        Me.Controls.SetChildIndex(Me.LblDescription, 0)
        Me.Controls.SetChildIndex(Me.TxtDescription, 0)
        Me.Controls.SetChildIndex(Me.Label1, 0)
        Me.Controls.SetChildIndex(Me.LblUnit, 0)
        Me.Controls.SetChildIndex(Me.TxtUnit, 0)
        Me.Controls.SetChildIndex(Me.LblManualCode, 0)
        Me.Controls.SetChildIndex(Me.TxtManualCode, 0)
        Me.Controls.SetChildIndex(Me.LblManualCodeReq, 0)
        Me.Controls.SetChildIndex(Me.Label2, 0)
        Me.Controls.SetChildIndex(Me.TxtItemGroup, 0)
        Me.Controls.SetChildIndex(Me.Label3, 0)
        Me.Controls.SetChildIndex(Me.TxtRate, 0)
        Me.Controls.SetChildIndex(Me.Label4, 0)
        Me.Controls.SetChildIndex(Me.Label5, 0)
        Me.Controls.SetChildIndex(Me.LblMeasureUnit, 0)
        Me.Controls.SetChildIndex(Me.TxtMeasureUnit, 0)
        Me.Controls.SetChildIndex(Me.LblMeasure, 0)
        Me.Controls.SetChildIndex(Me.TxtMeasure, 0)
        Me.Controls.SetChildIndex(Me.Label7, 0)
        Me.Controls.SetChildIndex(Me.TxtBusinessVolume, 0)
        Me.Controls.SetChildIndex(Me.Label6, 0)
        Me.Controls.SetChildIndex(Me.TxtPointValue, 0)
        Me.Controls.SetChildIndex(Me.Label8, 0)
        Me.Controls.SetChildIndex(Me.Label9, 0)
        Me.Controls.SetChildIndex(Me.Label11, 0)
        Me.Controls.SetChildIndex(Me.TxtSalesTaxGroup, 0)
        Me.Controls.SetChildIndex(Me.Label10, 0)
        Me.GrpUP.ResumeLayout(False)
        Me.GrpUP.PerformLayout()
        Me.GBoxEntryType.ResumeLayout(False)
        Me.GBoxEntryType.PerformLayout()
        Me.GBoxMoveToLog.ResumeLayout(False)
        Me.GBoxMoveToLog.PerformLayout()
        Me.GBoxApprove.ResumeLayout(False)
        Me.GBoxApprove.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GBoxDivision.ResumeLayout(False)
        Me.GBoxDivision.PerformLayout()
        CType(Me.DTMaster, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Protected WithEvents LblDescription As System.Windows.Forms.Label
    Protected WithEvents TxtDescription As AgControls.AgTextBox
    Protected WithEvents Label1 As System.Windows.Forms.Label
    Protected WithEvents TxtUnit As AgControls.AgTextBox
    Protected WithEvents LblManualCodeReq As System.Windows.Forms.Label
    Protected WithEvents TxtManualCode As AgControls.AgTextBox
    Protected WithEvents LblManualCode As System.Windows.Forms.Label
    Protected WithEvents LblUnit As System.Windows.Forms.Label
    Protected WithEvents TxtItemGroup As AgControls.AgTextBox
    Protected WithEvents Label2 As System.Windows.Forms.Label
    Protected WithEvents TxtRate As AgControls.AgTextBox
    Protected WithEvents Label3 As System.Windows.Forms.Label
    Protected WithEvents Label4 As System.Windows.Forms.Label
    Protected WithEvents TxtMeasureUnit As AgControls.AgTextBox
    Protected WithEvents LblMeasureUnit As System.Windows.Forms.Label
    Protected WithEvents TxtMeasure As AgControls.AgTextBox
    Protected WithEvents LblMeasure As System.Windows.Forms.Label
    Protected WithEvents TxtPointValue As AgControls.AgTextBox
    Protected WithEvents Label6 As System.Windows.Forms.Label
    Protected WithEvents TxtBusinessVolume As AgControls.AgTextBox
    Protected WithEvents Label7 As System.Windows.Forms.Label
    Protected WithEvents Label8 As System.Windows.Forms.Label
    Protected WithEvents Label9 As System.Windows.Forms.Label
    Protected WithEvents Label10 As System.Windows.Forms.Label
    Protected WithEvents TxtSalesTaxGroup As AgControls.AgTextBox
    Protected WithEvents Label11 As System.Windows.Forms.Label
Protected WithEvents Label5 As System.Windows.Forms.Label
#End Region

    Public Sub FrmYarn_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mConStr$ = ""
        mConStr = "WHERE 1=1  "
        AgL.PubFindQry = "SELECT I.Code, I.ManualCode as [Item_Code], I.Description [Item_Description], I.Unit, I.Rate, G.Description as Item_Group, C.Description as Item_Category " & _
                        " FROM Item I " & _
                        " Left Join ItemGroup G On I.ItemGroup = G.Code " & _
                        " Left Join ItemCategory C On G.ItemCategory = C.Code " & _
                        "  " & mConStr & _
                        " And I.ItemType = '" & ClsMain.ItemType.RawMaterial & "'   "
        AgL.PubFindQryOrdBy = "[Item Description]"
    End Sub

    Private Sub FrmYarn_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "Item"
        MainLineTableCsv = "ItemBuyer"
        LogTableName = "Item_Log"
        LogLineTableCsv = "ItemBuyer_Log"
    End Sub


    Private Sub FrmQuality1_BaseFunction_FIniList() Handles Me.BaseFunction_FIniList
        mQry = "Select Code, ManualCode As ItemCode, Div_Code ,ItemType " & _
                " From Item " & _
                " Order By ManualCode "
        TxtManualCode.AgHelpDataSet(2) = AgL.FillData(mQry, AgL.GCn)

        mQry = "Select Code, Description As Name , Div_Code, ItemType " & _
                " From Item " & _
                " Order By Description"
        TxtDescription.AgHelpDataSet(2) = AgL.FillData(mQry, AgL.GCn)

        mQry = "SELECT Code, Code AS Unit FROM Unit "
        TxtUnit.AgHelpDataSet() = AgL.FillData(mQry, AgL.GCn)

        mQry = "SELECT Code, Code AS Unit FROM Unit "
        TxtMeasureUnit.AgHelpDataSet() = TxtUnit.AgHelpDataSet
    End Sub

    Private Sub FrmYarn_BaseFunction_FIniMast(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMast
        Dim mConStr$ = ""
        mConStr = " WHERE 1=1  "
        mQry = " Select I.Code As SearchCode " & _
            " From Item I " & mConStr & _
            " And I.ItemType = '" & ClsMain.ItemType.RawMaterial & "' Order By I.Description "

        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub FrmQuality1_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim DsTemp As DataSet

        mQry = "Select I.Code, I.ManualCode, I.Description, I.Unit, I.Rate, I.ItemGroup, I.SalesTaxPostingGroup, " & _
            " G.Description as ItemGroupDesc, I.MeasureUnit, I.Measure,I.PointValue, I.BusinessVolume " & _
            " From Item I " & _
            " Left Join ItemGroup G On I.ItemGroup = G.Code  " & _
            " Where I.Code='" & SearchCode & "'"
        DsTemp = AgL.FillData(mQry, AgL.GCn)


        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                mInternalCode = AgL.XNull(.Rows(0)("Code"))
                TxtManualCode.Text = AgL.XNull(.Rows(0)("ManualCode"))
                TxtDescription.Text = AgL.XNull(.Rows(0)("Description"))
                TxtUnit.Text = AgL.XNull(.Rows(0)("Unit"))
                TxtItemGroup.Tag = AgL.XNull(.Rows(0)("ItemGroup"))
                TxtItemGroup.Text = AgL.XNull(.Rows(0)("ItemGroupDesc"))
                TxtMeasure.Text = AgL.XNull(.Rows(0)("Measure"))
                TxtMeasureUnit.Text = AgL.XNull(.Rows(0)("MeasureUnit"))
                TxtSalesTaxGroup.Tag = AgL.XNull(.Rows(0)("SalesTaxPostingGroup"))
                TxtSalesTaxGroup.Text = AgL.XNull(.Rows(0)("SalesTaxPostingGroup"))
                TxtRate.Text = AgL.VNull(.Rows(0)("Rate"))
                TxtPointValue.Text = AgL.VNull(.Rows(0)("PointValue"))
                TxtBusinessVolume.Text = AgL.VNull(.Rows(0)("BusinessVolume"))

                Calculation()
            End If
        End With
    End Sub

    Private Sub Control_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtDescription.Enter, TxtManualCode.Enter
        Try
            Select Case sender.name
            End Select
        Catch ex As Exception
        End Try
    End Sub

    Private Sub FrmYarn_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        If TxtDescription.Text.Trim = "" Then Err.Raise(1, , "Item Description Is Required!")

        If Topctrl1.Mode = "Add" Then
            mQry = "Select count(*) From Item Where Description='" & TxtDescription.Text & "' And " & AgTemplate.ClsMain.RetDivFilterStr & "  "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Item Description Already Exist!")
        Else
            mQry = "Select count(*) From Item Where Description='" & TxtDescription.Text & "' And Code<>'" & mInternalCode & "' And " & AgTemplate.ClsMain.RetDivFilterStr & "  "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Item Description Already Exist!")
        End If
    End Sub

    Private Sub FrmYarn_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As System.Data.SqlClient.SqlConnection, ByVal Cmd As System.Data.SqlClient.SqlCommand) Handles Me.BaseEvent_Save_InTrans
        mQry = "UPDATE Item " & _
                " SET " & _
                " ManualCode = " & AgL.Chk_Text(TxtManualCode.Text) & ", " & _
                " Description = " & AgL.Chk_Text(TxtDescription.Text) & ", " & _
                " Unit = " & AgL.Chk_Text(TxtUnit.Text) & ", " & _
                " ItemGroup = " & AgL.Chk_Text(TxtItemGroup.Tag) & ", " & _
                " ItemType = " & AgL.Chk_Text(ClsMain.ItemType.RawMaterial) & ", " & _
                " Measure = " & Val(TxtMeasure.Text) & ", " & _
                " MeasureUnit = " & AgL.Chk_Text(TxtMeasureUnit.Text) & ", " & _
                " SalesTaxPostingGroup = " & AgL.Chk_Text(TxtSalesTaxGroup.Tag) & ", " & _
                " PointValue = " & Val(TxtPointValue.Text) & ", " & _
                " BusinessVolume = " & Val(TxtBusinessVolume.Text) & ", " & _
                " Rate = " & Val(TxtRate.Text) & " " & _
                " Where Code = '" & SearchCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
    End Sub

    Private Sub Topctrl1_tbAdd() Handles Topctrl1.tbAdd
        TxtManualCode.Focus()
    End Sub

    Private Sub Topctrl1_tbEdit() Handles Topctrl1.tbEdit
        TxtManualCode.Focus()
    End Sub

    Private Sub Topctrl1_tbPrn() Handles Topctrl1.tbPrn
    End Sub


    Private Sub Control_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtDescription.Validating

        Dim DtTemp As DataTable = Nothing
        Dim DrTemp As DataRow() = Nothing
        Try
            Select Case sender.NAME

            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Private Sub FrmQuality1_BaseFunction_BlankText() Handles Me.BaseFunction_BlankText
    End Sub

    Private Sub TxtItemGroup_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtItemGroup.Enter
        Select Case sender.name
            Case TxtItemGroup.Name
        End Select
    End Sub

    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub

    Private Sub FrmItemGroup_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AgL.WinSetting(Me, 356, 868)
    End Sub

    Private Sub TxtDescription_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtMeasureUnit.KeyDown

    End Sub

    Private Sub TxtSalesTaxGroup_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtSalesTaxGroup.KeyDown, TxtItemGroup.KeyDown, TxtBusinessVolume.KeyDown
        Select Case sender.name
            Case TxtSalesTaxGroup.Name
                If CType(sender, AgControls.AgTextBox).AgHelpDataSet Is Nothing Then
                    If e.KeyCode <> Keys.Enter Then
                        mQry = "SELECT Description as  Code, Description AS PostingGroupSalesTaxItem " & _
                               "FROM PostingGroupSalesTaxItem Where IsNull(Active,0)=1 "
                        CType(sender, AgControls.AgTextBox).AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)
                    End If
                End If
            Case TxtItemGroup.Name
                If CType(sender, AgControls.AgTextBox).AgHelpDataSet Is Nothing Then
                    If e.KeyCode <> Keys.Enter Then
                        mQry = "Select H.Code, H.Description as Item_Group, C.Description as Item_Category " & _
                               "From ItemGroup H " & _
                               "Left Join ItemCategory C On H.ItemCategory = C.Code " & _
                               "Where H.ItemType = '" & ClsMain.ItemType.RawMaterial & "' " & _
                               "And  IsNull(H.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "'  " & _
                               "Order By H.Description "
                        TxtItemGroup.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)
                    End If
                End If
            Case TxtBusinessVolume.Name
                If e.KeyCode = Keys.Enter Then
                    If MsgBox("Do you want to save?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "Save") = MsgBoxResult.Yes Then
                        Topctrl1.FButtonClick(13)
                    End If
                End If
        End Select
    End Sub
End Class
