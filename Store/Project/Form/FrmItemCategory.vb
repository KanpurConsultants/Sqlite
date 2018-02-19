Imports System.Data.SQLite
Public Class FrmItemCategory
    Inherits AgTemplate.TempMaster

    Dim mQry$

#Region "Designer Code"
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TxtDescription = New AgControls.AgTextBox()
        Me.LblDescription = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TxtItemType = New AgControls.AgTextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.LblIsSystemDefine = New System.Windows.Forms.Label()
        Me.ChkIsSystemDefine = New System.Windows.Forms.CheckBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TxtSalesTaxGroup = New AgControls.AgTextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TxtUnit = New AgControls.AgTextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TxtHsn = New AgControls.AgTextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.TxtDepartment = New AgControls.AgTextBox()
        Me.Label11 = New System.Windows.Forms.Label()
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
        Me.Topctrl1.TabIndex = 2
        Me.Topctrl1.tAdd = False
        Me.Topctrl1.tDel = False
        Me.Topctrl1.tEdit = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Location = New System.Drawing.Point(0, 273)
        Me.GroupBox1.Size = New System.Drawing.Size(904, 4)
        '
        'GrpUP
        '
        Me.GrpUP.Location = New System.Drawing.Point(14, 277)
        '
        'TxtEntryBy
        '
        Me.TxtEntryBy.Tag = ""
        Me.TxtEntryBy.Text = ""
        '
        'GBoxEntryType
        '
        Me.GBoxEntryType.Location = New System.Drawing.Point(148, 277)
        '
        'TxtEntryType
        '
        Me.TxtEntryType.Tag = ""
        '
        'GBoxMoveToLog
        '
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(554, 277)
        '
        'TxtMoveToLog
        '
        Me.TxtMoveToLog.Tag = ""
        '
        'GBoxApprove
        '
        Me.GBoxApprove.Location = New System.Drawing.Point(401, 277)
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
        Me.GroupBox2.Location = New System.Drawing.Point(704, 277)
        '
        'GBoxDivision
        '
        Me.GBoxDivision.Location = New System.Drawing.Point(278, 277)
        '
        'TxtDivision
        '
        Me.TxtDivision.AgSelectedValue = ""
        Me.TxtDivision.Tag = ""
        '
        'TxtStatus
        '
        Me.TxtStatus.AgSelectedValue = ""
        Me.TxtStatus.Tag = ""
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(272, 102)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(10, 7)
        Me.Label1.TabIndex = 666
        Me.Label1.Text = "Ä"
        '
        'TxtDescription
        '
        Me.TxtDescription.AgAllowUserToEnableMasterHelp = False
        Me.TxtDescription.AgLastValueTag = Nothing
        Me.TxtDescription.AgLastValueText = Nothing
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
        Me.TxtDescription.Location = New System.Drawing.Point(288, 94)
        Me.TxtDescription.MaxLength = 50
        Me.TxtDescription.Name = "TxtDescription"
        Me.TxtDescription.Size = New System.Drawing.Size(385, 18)
        Me.TxtDescription.TabIndex = 0
        '
        'LblDescription
        '
        Me.LblDescription.AutoSize = True
        Me.LblDescription.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDescription.Location = New System.Drawing.Point(169, 95)
        Me.LblDescription.Name = "LblDescription"
        Me.LblDescription.Size = New System.Drawing.Size(89, 16)
        Me.LblDescription.TabIndex = 661
        Me.LblDescription.Text = "Item Category"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(272, 123)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(10, 7)
        Me.Label2.TabIndex = 674
        Me.Label2.Text = "Ä"
        '
        'TxtItemType
        '
        Me.TxtItemType.AgAllowUserToEnableMasterHelp = False
        Me.TxtItemType.AgLastValueTag = Nothing
        Me.TxtItemType.AgLastValueText = Nothing
        Me.TxtItemType.AgMandatory = True
        Me.TxtItemType.AgMasterHelp = False
        Me.TxtItemType.AgNumberLeftPlaces = 0
        Me.TxtItemType.AgNumberNegetiveAllow = False
        Me.TxtItemType.AgNumberRightPlaces = 0
        Me.TxtItemType.AgPickFromLastValue = False
        Me.TxtItemType.AgRowFilter = ""
        Me.TxtItemType.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtItemType.AgSelectedValue = Nothing
        Me.TxtItemType.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtItemType.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtItemType.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtItemType.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtItemType.Location = New System.Drawing.Point(288, 115)
        Me.TxtItemType.MaxLength = 50
        Me.TxtItemType.Name = "TxtItemType"
        Me.TxtItemType.Size = New System.Drawing.Size(385, 18)
        Me.TxtItemType.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(169, 116)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(64, 16)
        Me.Label3.TabIndex = 673
        Me.Label3.Text = "Item Type"
        '
        'LblIsSystemDefine
        '
        Me.LblIsSystemDefine.AutoSize = True
        Me.LblIsSystemDefine.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblIsSystemDefine.ForeColor = System.Drawing.Color.Red
        Me.LblIsSystemDefine.Location = New System.Drawing.Point(707, 94)
        Me.LblIsSystemDefine.Name = "LblIsSystemDefine"
        Me.LblIsSystemDefine.Size = New System.Drawing.Size(96, 15)
        Me.LblIsSystemDefine.TabIndex = 1063
        Me.LblIsSystemDefine.Text = "IsSystemDefine"
        '
        'ChkIsSystemDefine
        '
        Me.ChkIsSystemDefine.AutoSize = True
        Me.ChkIsSystemDefine.BackColor = System.Drawing.Color.Transparent
        Me.ChkIsSystemDefine.Font = New System.Drawing.Font("Arial", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkIsSystemDefine.ForeColor = System.Drawing.Color.Red
        Me.ChkIsSystemDefine.Location = New System.Drawing.Point(693, 95)
        Me.ChkIsSystemDefine.Name = "ChkIsSystemDefine"
        Me.ChkIsSystemDefine.Size = New System.Drawing.Size(15, 14)
        Me.ChkIsSystemDefine.TabIndex = 1062
        Me.ChkIsSystemDefine.UseVisualStyleBackColor = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(272, 165)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(10, 7)
        Me.Label4.TabIndex = 1066
        Me.Label4.Text = "Ä"
        '
        'TxtSalesTaxGroup
        '
        Me.TxtSalesTaxGroup.AgAllowUserToEnableMasterHelp = False
        Me.TxtSalesTaxGroup.AgLastValueTag = Nothing
        Me.TxtSalesTaxGroup.AgLastValueText = Nothing
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
        Me.TxtSalesTaxGroup.Location = New System.Drawing.Point(288, 157)
        Me.TxtSalesTaxGroup.MaxLength = 50
        Me.TxtSalesTaxGroup.Name = "TxtSalesTaxGroup"
        Me.TxtSalesTaxGroup.Size = New System.Drawing.Size(385, 18)
        Me.TxtSalesTaxGroup.TabIndex = 3
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(169, 158)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(104, 16)
        Me.Label5.TabIndex = 1065
        Me.Label5.Text = "Sales Tax Group"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label6.Location = New System.Drawing.Point(272, 144)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(10, 7)
        Me.Label6.TabIndex = 1069
        Me.Label6.Text = "Ä"
        '
        'TxtUnit
        '
        Me.TxtUnit.AgAllowUserToEnableMasterHelp = False
        Me.TxtUnit.AgLastValueTag = Nothing
        Me.TxtUnit.AgLastValueText = Nothing
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
        Me.TxtUnit.Location = New System.Drawing.Point(288, 136)
        Me.TxtUnit.MaxLength = 50
        Me.TxtUnit.Name = "TxtUnit"
        Me.TxtUnit.Size = New System.Drawing.Size(385, 18)
        Me.TxtUnit.TabIndex = 2
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(169, 137)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(31, 16)
        Me.Label7.TabIndex = 1068
        Me.Label7.Text = "Unit"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label8.Location = New System.Drawing.Point(272, 186)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(10, 7)
        Me.Label8.TabIndex = 1072
        Me.Label8.Text = "Ä"
        '
        'TxtHsn
        '
        Me.TxtHsn.AgAllowUserToEnableMasterHelp = False
        Me.TxtHsn.AgLastValueTag = Nothing
        Me.TxtHsn.AgLastValueText = Nothing
        Me.TxtHsn.AgMandatory = True
        Me.TxtHsn.AgMasterHelp = False
        Me.TxtHsn.AgNumberLeftPlaces = 8
        Me.TxtHsn.AgNumberNegetiveAllow = False
        Me.TxtHsn.AgNumberRightPlaces = 0
        Me.TxtHsn.AgPickFromLastValue = False
        Me.TxtHsn.AgRowFilter = ""
        Me.TxtHsn.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtHsn.AgSelectedValue = Nothing
        Me.TxtHsn.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtHsn.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtHsn.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtHsn.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtHsn.Location = New System.Drawing.Point(288, 178)
        Me.TxtHsn.MaxLength = 8
        Me.TxtHsn.Name = "TxtHsn"
        Me.TxtHsn.Size = New System.Drawing.Size(385, 18)
        Me.TxtHsn.TabIndex = 4
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.Location = New System.Drawing.Point(169, 179)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(69, 16)
        Me.Label9.TabIndex = 1071
        Me.Label9.Text = "HSN Code"
        '
        'TxtDepartment
        '
        Me.TxtDepartment.AgAllowUserToEnableMasterHelp = False
        Me.TxtDepartment.AgLastValueTag = Nothing
        Me.TxtDepartment.AgLastValueText = Nothing
        Me.TxtDepartment.AgMandatory = False
        Me.TxtDepartment.AgMasterHelp = False
        Me.TxtDepartment.AgNumberLeftPlaces = 0
        Me.TxtDepartment.AgNumberNegetiveAllow = False
        Me.TxtDepartment.AgNumberRightPlaces = 0
        Me.TxtDepartment.AgPickFromLastValue = False
        Me.TxtDepartment.AgRowFilter = ""
        Me.TxtDepartment.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtDepartment.AgSelectedValue = Nothing
        Me.TxtDepartment.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtDepartment.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtDepartment.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtDepartment.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtDepartment.Location = New System.Drawing.Point(288, 199)
        Me.TxtDepartment.MaxLength = 50
        Me.TxtDepartment.Name = "TxtDepartment"
        Me.TxtDepartment.Size = New System.Drawing.Size(385, 18)
        Me.TxtDepartment.TabIndex = 5
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.Location = New System.Drawing.Point(169, 200)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(75, 16)
        Me.Label11.TabIndex = 1074
        Me.Label11.Text = "Department"
        '
        'FrmItemCategory
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(862, 321)
        Me.Controls.Add(Me.TxtDepartment)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.TxtHsn)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.TxtUnit)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TxtSalesTaxGroup)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.LblIsSystemDefine)
        Me.Controls.Add(Me.ChkIsSystemDefine)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TxtItemType)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TxtDescription)
        Me.Controls.Add(Me.LblDescription)
        Me.Name = "FrmItemCategory"
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
        Me.Controls.SetChildIndex(Me.Label3, 0)
        Me.Controls.SetChildIndex(Me.TxtItemType, 0)
        Me.Controls.SetChildIndex(Me.Label2, 0)
        Me.Controls.SetChildIndex(Me.ChkIsSystemDefine, 0)
        Me.Controls.SetChildIndex(Me.LblIsSystemDefine, 0)
        Me.Controls.SetChildIndex(Me.Label5, 0)
        Me.Controls.SetChildIndex(Me.TxtSalesTaxGroup, 0)
        Me.Controls.SetChildIndex(Me.Label4, 0)
        Me.Controls.SetChildIndex(Me.Label7, 0)
        Me.Controls.SetChildIndex(Me.TxtUnit, 0)
        Me.Controls.SetChildIndex(Me.Label6, 0)
        Me.Controls.SetChildIndex(Me.Label9, 0)
        Me.Controls.SetChildIndex(Me.TxtHsn, 0)
        Me.Controls.SetChildIndex(Me.Label8, 0)
        Me.Controls.SetChildIndex(Me.Label11, 0)
        Me.Controls.SetChildIndex(Me.TxtDepartment, 0)
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

    Public WithEvents LblDescription As System.Windows.Forms.Label
    Public WithEvents TxtDescription As AgControls.AgTextBox
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents TxtItemType As AgControls.AgTextBox
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents LblIsSystemDefine As System.Windows.Forms.Label
    Friend WithEvents ChkIsSystemDefine As System.Windows.Forms.CheckBox
    Public WithEvents Label4 As Label
    Public WithEvents TxtSalesTaxGroup As AgControls.AgTextBox
    Public WithEvents Label5 As Label
    Public WithEvents Label6 As Label
    Public WithEvents TxtUnit As AgControls.AgTextBox
    Public WithEvents Label7 As Label
    Public WithEvents Label8 As Label
    Public WithEvents TxtHsn As AgControls.AgTextBox
    Public WithEvents Label9 As Label
    Public WithEvents TxtDepartment As AgControls.AgTextBox
    Public WithEvents Label11 As Label
    Public WithEvents Label1 As System.Windows.Forms.Label
#End Region

    Private Sub FrmYarn_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        If TxtDescription.Text.Trim = "" Then Err.Raise(1, , "Description Is Required!")

        If Topctrl1.Mode = "Add" Then
            mQry = "Select count(*) From ItemCategory Where Description='" & TxtDescription.Text & "' And " & AgTemplate.ClsMain.RetDivFilterStr & "  "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Description Already Exist!")
        Else
            mQry = "Select count(*) From ItemCategory Where Description='" & TxtDescription.Text & "' And Code<>'" & mInternalCode & "' And " & AgTemplate.ClsMain.RetDivFilterStr & "  "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Description Already Exist!")
        End If
    End Sub

    Public Overridable Sub FrmYarn_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mConStr$ = ""
        AgL.PubFindQry = "SELECT I.Code, I.Description, T.Name AS ItemType  " &
                        " FROM ItemCategory I " &
                        " Left Join ItemType T On I.ItemType = T.Code "
        AgL.PubFindQryOrdBy = "[Description]"
    End Sub

    Private Sub FrmYarn_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "ItemCategory"
        LogTableName = "ItemCategory_Log"
        'MainLineTableCsv = "ItemBuyer"
        'LogLineTableCsv = "ItemBuyer_Log"
    End Sub

    Private Sub FrmYarn_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As SqliteConnection, ByVal Cmd As SqliteCommand) Handles Me.BaseEvent_Save_InTrans
        mQry = "UPDATE ItemCategory 
                Set 
                Description = " & AgL.Chk_Text(TxtDescription.Text) & ", 
                IsSystemDefine = " & Val(IIf(ChkIsSystemDefine.Checked, 1, 0)) & ", 
                ItemType = " & AgL.Chk_Text(TxtItemType.AgSelectedValue) & ", 
                SalesTaxGroup = " & AgL.Chk_Text(TxtSalesTaxGroup.AgSelectedValue) & ", 
                HSN = " & AgL.Chk_Text(TxtHsn.Text) & ", 
                Department = " & AgL.Chk_Text(TxtDepartment.AgSelectedValue) & ", 
                Unit = " & AgL.Chk_Text(TxtUnit.AgSelectedValue) & " 
                Where Code = '" & SearchCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        mQry = " UPDATE ItemGroup Set ItemType = '" & TxtItemType.AgSelectedValue & "' Where ItemCategory = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

        mQry = " UPDATE Item Set ItemType = '" & TxtItemType.AgSelectedValue & "' Where ItemCategory = '" & mSearchCode & "'"
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
    End Sub

    Private Sub FrmQuality1_BaseFunction_FIniList() Handles Me.BaseFunction_FIniList
        mQry = "Select Code, Description As Name " &
                " From ItemCategory " &
                " Order By Description "
        TxtDescription.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)

        mQry = " SELECT Code, Name  FROM ItemType "
        TxtItemType.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)

        mQry = " SELECT Description as  Code, Description as Name  FROM PostingGroupSalesTaxItem where Active=1 Order By Description"
        TxtSalesTaxGroup.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)

        mQry = " SELECT Code, Code as Name  FROM Unit where IsActive=1 Order By Code "
        TxtUnit.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)

        mQry = "SELECT Code, Description as Name  FROM Department where Status='Active' Order By Code"
        TxtDepartment.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)
    End Sub

    Private Sub FrmQuality1_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim DsTemp As DataSet

        mQry = "Select H.*, D.Description as DepartmentName 
                 From ItemCategory H 
                 Left Join Department D On H.Department = D.Code
                 Where H.Code='" & SearchCode & "'"
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                mInternalCode = AgL.XNull(DsTemp.Tables(0).Rows(0)("Code"))
                TxtDescription.Text = AgL.XNull(DsTemp.Tables(0).Rows(0)("Description"))
                TxtItemType.AgSelectedValue = AgL.XNull(DsTemp.Tables(0).Rows(0)("ItemType"))
                TxtSalesTaxGroup.AgSelectedValue = AgL.XNull(DsTemp.Tables(0).Rows(0)("SalesTaxGroup"))
                TxtUnit.AgSelectedValue = AgL.XNull(DsTemp.Tables(0).Rows(0)("Unit"))
                TxtDepartment.Tag = AgL.XNull(DsTemp.Tables(0).Rows(0)("Department"))
                TxtDepartment.Text = AgL.XNull(DsTemp.Tables(0).Rows(0)("DepartmentName"))
                TxtHsn.Text = AgL.XNull(DsTemp.Tables(0).Rows(0)("HSN"))
                ChkIsSystemDefine.Checked = AgL.VNull(DsTemp.Tables(0).Rows(0)("IsSystemDefine"))
                LblIsSystemDefine.Text = IIf(AgL.VNull(DsTemp.Tables(0).Rows(0)("IsSystemDefine")) = 0, "User Define", "System Define")
                ChkIsSystemDefine.Enabled = False
            End If
        End With
    End Sub

    Private Function FGetRelationalData() As Boolean
        Try
            mQry = " Select Count(*) From Item Where ItemCategory = '" & mSearchCode & "'"
            If AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar) > 0 Then
                MsgBox(" Data Exists For ItemCategory " & TxtDescription.Text & " In Item Master . Can't Delete Entry", MsgBoxStyle.Information)
                FGetRelationalData = True
                Exit Function
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " in FGetRelationalData")
            FGetRelationalData = True
        End Try
    End Function

    Private Sub Topctrl1_tbAdd() Handles Topctrl1.tbAdd
        TxtDescription.Focus()
    End Sub

    Private Sub Topctrl1_tbEdit() Handles Topctrl1.tbEdit
        TxtDescription.Focus()
    End Sub

    Private Sub TxtDescription_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtDepartment.KeyDown
        If e.KeyCode = Keys.Enter Then
            If MsgBox("Do you want to save?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "Save") = MsgBoxResult.Yes Then
                Topctrl1.FButtonClick(13)
            End If
        End If
    End Sub

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Public Sub New(ByVal StrUPVar As String, ByVal DTUP As DataTable)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Topctrl1.FSetParent(Me, StrUPVar, DTUP)
        Topctrl1.SetDisp(True)

    End Sub

    Private Sub FrmYarn_BaseFunction_FIniMast(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMast
        Dim mConStr$ = ""
        mQry = "Select I.Code As SearchCode " &
            " From ItemCategory I " &
            " Order By I.Description "

        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub

    Private Sub FrmItemCategory_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AgL.WinSetting(Me, 360, 885)
        FManageSystemDefine()
    End Sub

    Private Sub FrmItemMaster_BaseEvent_Topctrl_tbEdit(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbEdit
        Passed = FRestrictSystemDefine()
    End Sub

    Private Sub FrmItemMaster_BaseEvent_Topctrl_tbDel(ByRef Passed As Boolean) Handles Me.BaseEvent_Topctrl_tbDel
        Passed = FRestrictSystemDefine()
        Passed = Not FGetRelationalData()
    End Sub
    Private Sub ChkIsSystemDefine_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ChkIsSystemDefine.Click
        FManageSystemDefine()
    End Sub

    Private Sub FManageSystemDefine()
        If AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Then
            ChkIsSystemDefine.Visible = True
            ChkIsSystemDefine.Enabled = True
        Else
            ChkIsSystemDefine.Visible = False
            ChkIsSystemDefine.Enabled = False
        End If

        If ChkIsSystemDefine.Checked Then
            LblIsSystemDefine.Text = "System Define"
        Else
            LblIsSystemDefine.Text = "User Define"
        End If
    End Sub

    Private Function FRestrictSystemDefine() As Boolean
        If ChkIsSystemDefine.Checked = True Then
            If AgL.StrCmp(AgL.PubUserName, AgLibrary.ClsConstant.PubSuperUserName) Then
                If MsgBox("This is a System Define Item.Do You Want To Proceed...?", MsgBoxStyle.Question + MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                    Topctrl1.FButtonClick(14, True)
                    FRestrictSystemDefine = False
                    Exit Function
                End If
            Else
                MsgBox("Can't Edit System Define Items...!", MsgBoxStyle.Information) : Topctrl1.FButtonClick(14, True)
                FRestrictSystemDefine = False
                Exit Function
            End If
        End If
        FManageSystemDefine()
        FRestrictSystemDefine = True
    End Function

    Private Sub FrmItemCategory_BaseEvent_Topctrl_tbAdd() Handles Me.BaseEvent_Topctrl_tbAdd
        ChkIsSystemDefine.Checked = False
        FManageSystemDefine()
    End Sub
End Class
