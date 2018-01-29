<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPartyDetail
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
        Me.BtnOk = New System.Windows.Forms.Button
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.BtnCancel = New System.Windows.Forms.Button
        Me.TxtPartyName = New AgControls.AgTextBox
        Me.LblBuyerName = New System.Windows.Forms.Label
        Me.TxtPartyAdd1 = New AgControls.AgTextBox
        Me.LblAddress = New System.Windows.Forms.Label
        Me.TxtPartyCity = New AgControls.AgTextBox
        Me.LblCity = New System.Windows.Forms.Label
        Me.TxtPartyMobile = New AgControls.AgTextBox
        Me.LblMobile = New System.Windows.Forms.Label
        Me.TxtPartyAdd2 = New AgControls.AgTextBox
        Me.TxtPartyTinNo = New AgControls.AgTextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.TxtPartyLSTNo = New AgControls.AgTextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.TxtPartyCSTNo = New AgControls.AgTextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'BtnOk
        '
        Me.BtnOk.BackColor = System.Drawing.Color.Transparent
        Me.BtnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnOk.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnOk.Location = New System.Drawing.Point(277, 161)
        Me.BtnOk.Name = "BtnOk"
        Me.BtnOk.Size = New System.Drawing.Size(60, 23)
        Me.BtnOk.TabIndex = 4
        Me.BtnOk.Text = "OK"
        Me.BtnOk.UseVisualStyleBackColor = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.BackColor = System.Drawing.Color.WhiteSmoke
        Me.GroupBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox2.Location = New System.Drawing.Point(5, 141)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(430, 5)
        Me.GroupBox2.TabIndex = 737
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Tag = ""
        '
        'BtnCancel
        '
        Me.BtnCancel.BackColor = System.Drawing.Color.Transparent
        Me.BtnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnCancel.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnCancel.Location = New System.Drawing.Point(342, 161)
        Me.BtnCancel.Name = "BtnCancel"
        Me.BtnCancel.Size = New System.Drawing.Size(60, 23)
        Me.BtnCancel.TabIndex = 5
        Me.BtnCancel.Text = "Close"
        Me.BtnCancel.UseVisualStyleBackColor = False
        '
        'TxtPartyName
        '
        Me.TxtPartyName.AgAllowUserToEnableMasterHelp = False
        Me.TxtPartyName.AgLastValueTag = Nothing
        Me.TxtPartyName.AgLastValueText = Nothing
        Me.TxtPartyName.AgMandatory = True
        Me.TxtPartyName.AgMasterHelp = False
        Me.TxtPartyName.AgNumberLeftPlaces = 8
        Me.TxtPartyName.AgNumberNegetiveAllow = False
        Me.TxtPartyName.AgNumberRightPlaces = 2
        Me.TxtPartyName.AgPickFromLastValue = False
        Me.TxtPartyName.AgRowFilter = ""
        Me.TxtPartyName.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtPartyName.AgSelectedValue = Nothing
        Me.TxtPartyName.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtPartyName.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtPartyName.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtPartyName.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPartyName.Location = New System.Drawing.Point(109, 34)
        Me.TxtPartyName.MaxLength = 0
        Me.TxtPartyName.Name = "TxtPartyName"
        Me.TxtPartyName.Size = New System.Drawing.Size(300, 18)
        Me.TxtPartyName.TabIndex = 1
        '
        'LblBuyerName
        '
        Me.LblBuyerName.AutoSize = True
        Me.LblBuyerName.BackColor = System.Drawing.Color.Transparent
        Me.LblBuyerName.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblBuyerName.Location = New System.Drawing.Point(26, 34)
        Me.LblBuyerName.Name = "LblBuyerName"
        Me.LblBuyerName.Size = New System.Drawing.Size(77, 16)
        Me.LblBuyerName.TabIndex = 742
        Me.LblBuyerName.Text = "Party Name"
        '
        'TxtPartyAdd1
        '
        Me.TxtPartyAdd1.AgAllowUserToEnableMasterHelp = False
        Me.TxtPartyAdd1.AgLastValueTag = Nothing
        Me.TxtPartyAdd1.AgLastValueText = Nothing
        Me.TxtPartyAdd1.AgMandatory = False
        Me.TxtPartyAdd1.AgMasterHelp = False
        Me.TxtPartyAdd1.AgNumberLeftPlaces = 8
        Me.TxtPartyAdd1.AgNumberNegetiveAllow = False
        Me.TxtPartyAdd1.AgNumberRightPlaces = 2
        Me.TxtPartyAdd1.AgPickFromLastValue = False
        Me.TxtPartyAdd1.AgRowFilter = ""
        Me.TxtPartyAdd1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtPartyAdd1.AgSelectedValue = Nothing
        Me.TxtPartyAdd1.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtPartyAdd1.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtPartyAdd1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtPartyAdd1.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPartyAdd1.Location = New System.Drawing.Point(109, 54)
        Me.TxtPartyAdd1.MaxLength = 0
        Me.TxtPartyAdd1.Name = "TxtPartyAdd1"
        Me.TxtPartyAdd1.Size = New System.Drawing.Size(300, 18)
        Me.TxtPartyAdd1.TabIndex = 2
        '
        'LblAddress
        '
        Me.LblAddress.AutoSize = True
        Me.LblAddress.BackColor = System.Drawing.Color.Transparent
        Me.LblAddress.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblAddress.Location = New System.Drawing.Point(26, 54)
        Me.LblAddress.Name = "LblAddress"
        Me.LblAddress.Size = New System.Drawing.Size(56, 16)
        Me.LblAddress.TabIndex = 744
        Me.LblAddress.Text = "Address"
        '
        'TxtPartyCity
        '
        Me.TxtPartyCity.AgAllowUserToEnableMasterHelp = False
        Me.TxtPartyCity.AgLastValueTag = Nothing
        Me.TxtPartyCity.AgLastValueText = Nothing
        Me.TxtPartyCity.AgMandatory = False
        Me.TxtPartyCity.AgMasterHelp = False
        Me.TxtPartyCity.AgNumberLeftPlaces = 8
        Me.TxtPartyCity.AgNumberNegetiveAllow = False
        Me.TxtPartyCity.AgNumberRightPlaces = 2
        Me.TxtPartyCity.AgPickFromLastValue = False
        Me.TxtPartyCity.AgRowFilter = ""
        Me.TxtPartyCity.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtPartyCity.AgSelectedValue = Nothing
        Me.TxtPartyCity.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtPartyCity.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtPartyCity.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtPartyCity.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPartyCity.Location = New System.Drawing.Point(109, 94)
        Me.TxtPartyCity.MaxLength = 0
        Me.TxtPartyCity.Name = "TxtPartyCity"
        Me.TxtPartyCity.Size = New System.Drawing.Size(118, 18)
        Me.TxtPartyCity.TabIndex = 3
        '
        'LblCity
        '
        Me.LblCity.AutoSize = True
        Me.LblCity.BackColor = System.Drawing.Color.Transparent
        Me.LblCity.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCity.Location = New System.Drawing.Point(26, 94)
        Me.LblCity.Name = "LblCity"
        Me.LblCity.Size = New System.Drawing.Size(31, 16)
        Me.LblCity.TabIndex = 746
        Me.LblCity.Text = "City"
        '
        'TxtPartyMobile
        '
        Me.TxtPartyMobile.AgAllowUserToEnableMasterHelp = False
        Me.TxtPartyMobile.AgLastValueTag = Nothing
        Me.TxtPartyMobile.AgLastValueText = Nothing
        Me.TxtPartyMobile.AgMandatory = False
        Me.TxtPartyMobile.AgMasterHelp = False
        Me.TxtPartyMobile.AgNumberLeftPlaces = 8
        Me.TxtPartyMobile.AgNumberNegetiveAllow = False
        Me.TxtPartyMobile.AgNumberRightPlaces = 2
        Me.TxtPartyMobile.AgPickFromLastValue = False
        Me.TxtPartyMobile.AgRowFilter = ""
        Me.TxtPartyMobile.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtPartyMobile.AgSelectedValue = Nothing
        Me.TxtPartyMobile.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtPartyMobile.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtPartyMobile.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtPartyMobile.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPartyMobile.Location = New System.Drawing.Point(109, 14)
        Me.TxtPartyMobile.MaxLength = 0
        Me.TxtPartyMobile.Name = "TxtPartyMobile"
        Me.TxtPartyMobile.Size = New System.Drawing.Size(300, 18)
        Me.TxtPartyMobile.TabIndex = 0
        '
        'LblMobile
        '
        Me.LblMobile.AutoSize = True
        Me.LblMobile.BackColor = System.Drawing.Color.Transparent
        Me.LblMobile.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblMobile.Location = New System.Drawing.Point(26, 14)
        Me.LblMobile.Name = "LblMobile"
        Me.LblMobile.Size = New System.Drawing.Size(46, 16)
        Me.LblMobile.TabIndex = 748
        Me.LblMobile.Text = "Mobile"
        '
        'TxtPartyAdd2
        '
        Me.TxtPartyAdd2.AgAllowUserToEnableMasterHelp = False
        Me.TxtPartyAdd2.AgLastValueTag = Nothing
        Me.TxtPartyAdd2.AgLastValueText = Nothing
        Me.TxtPartyAdd2.AgMandatory = False
        Me.TxtPartyAdd2.AgMasterHelp = False
        Me.TxtPartyAdd2.AgNumberLeftPlaces = 8
        Me.TxtPartyAdd2.AgNumberNegetiveAllow = False
        Me.TxtPartyAdd2.AgNumberRightPlaces = 2
        Me.TxtPartyAdd2.AgPickFromLastValue = False
        Me.TxtPartyAdd2.AgRowFilter = ""
        Me.TxtPartyAdd2.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtPartyAdd2.AgSelectedValue = Nothing
        Me.TxtPartyAdd2.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtPartyAdd2.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtPartyAdd2.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtPartyAdd2.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPartyAdd2.Location = New System.Drawing.Point(109, 74)
        Me.TxtPartyAdd2.MaxLength = 0
        Me.TxtPartyAdd2.Name = "TxtPartyAdd2"
        Me.TxtPartyAdd2.Size = New System.Drawing.Size(300, 18)
        Me.TxtPartyAdd2.TabIndex = 749
        '
        'TxtPartyTinNo
        '
        Me.TxtPartyTinNo.AgAllowUserToEnableMasterHelp = False
        Me.TxtPartyTinNo.AgLastValueTag = Nothing
        Me.TxtPartyTinNo.AgLastValueText = Nothing
        Me.TxtPartyTinNo.AgMandatory = False
        Me.TxtPartyTinNo.AgMasterHelp = False
        Me.TxtPartyTinNo.AgNumberLeftPlaces = 8
        Me.TxtPartyTinNo.AgNumberNegetiveAllow = False
        Me.TxtPartyTinNo.AgNumberRightPlaces = 2
        Me.TxtPartyTinNo.AgPickFromLastValue = False
        Me.TxtPartyTinNo.AgRowFilter = ""
        Me.TxtPartyTinNo.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtPartyTinNo.AgSelectedValue = Nothing
        Me.TxtPartyTinNo.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtPartyTinNo.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtPartyTinNo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtPartyTinNo.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPartyTinNo.Location = New System.Drawing.Point(290, 94)
        Me.TxtPartyTinNo.MaxLength = 0
        Me.TxtPartyTinNo.Name = "TxtPartyTinNo"
        Me.TxtPartyTinNo.Size = New System.Drawing.Size(119, 18)
        Me.TxtPartyTinNo.TabIndex = 750
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(233, 94)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(45, 16)
        Me.Label1.TabIndex = 751
        Me.Label1.Text = "Tin No"
        '
        'TxtPartyLSTNo
        '
        Me.TxtPartyLSTNo.AgAllowUserToEnableMasterHelp = False
        Me.TxtPartyLSTNo.AgLastValueTag = Nothing
        Me.TxtPartyLSTNo.AgLastValueText = Nothing
        Me.TxtPartyLSTNo.AgMandatory = False
        Me.TxtPartyLSTNo.AgMasterHelp = False
        Me.TxtPartyLSTNo.AgNumberLeftPlaces = 8
        Me.TxtPartyLSTNo.AgNumberNegetiveAllow = False
        Me.TxtPartyLSTNo.AgNumberRightPlaces = 2
        Me.TxtPartyLSTNo.AgPickFromLastValue = False
        Me.TxtPartyLSTNo.AgRowFilter = ""
        Me.TxtPartyLSTNo.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtPartyLSTNo.AgSelectedValue = Nothing
        Me.TxtPartyLSTNo.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtPartyLSTNo.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtPartyLSTNo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtPartyLSTNo.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPartyLSTNo.Location = New System.Drawing.Point(290, 114)
        Me.TxtPartyLSTNo.MaxLength = 0
        Me.TxtPartyLSTNo.Name = "TxtPartyLSTNo"
        Me.TxtPartyLSTNo.Size = New System.Drawing.Size(119, 18)
        Me.TxtPartyLSTNo.TabIndex = 754
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(233, 114)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(51, 16)
        Me.Label2.TabIndex = 755
        Me.Label2.Text = "LST No"
        '
        'TxtPartyCSTNo
        '
        Me.TxtPartyCSTNo.AgAllowUserToEnableMasterHelp = False
        Me.TxtPartyCSTNo.AgLastValueTag = Nothing
        Me.TxtPartyCSTNo.AgLastValueText = Nothing
        Me.TxtPartyCSTNo.AgMandatory = False
        Me.TxtPartyCSTNo.AgMasterHelp = False
        Me.TxtPartyCSTNo.AgNumberLeftPlaces = 8
        Me.TxtPartyCSTNo.AgNumberNegetiveAllow = False
        Me.TxtPartyCSTNo.AgNumberRightPlaces = 2
        Me.TxtPartyCSTNo.AgPickFromLastValue = False
        Me.TxtPartyCSTNo.AgRowFilter = ""
        Me.TxtPartyCSTNo.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtPartyCSTNo.AgSelectedValue = Nothing
        Me.TxtPartyCSTNo.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtPartyCSTNo.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtPartyCSTNo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtPartyCSTNo.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtPartyCSTNo.Location = New System.Drawing.Point(109, 114)
        Me.TxtPartyCSTNo.MaxLength = 0
        Me.TxtPartyCSTNo.Name = "TxtPartyCSTNo"
        Me.TxtPartyCSTNo.Size = New System.Drawing.Size(118, 18)
        Me.TxtPartyCSTNo.TabIndex = 752
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(26, 114)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(53, 16)
        Me.Label3.TabIndex = 753
        Me.Label3.Text = "CST No"
        '
        'FrmPartyDetail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(435, 193)
        Me.ControlBox = False
        Me.Controls.Add(Me.TxtPartyLSTNo)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TxtPartyCSTNo)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TxtPartyTinNo)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TxtPartyAdd2)
        Me.Controls.Add(Me.TxtPartyMobile)
        Me.Controls.Add(Me.LblMobile)
        Me.Controls.Add(Me.TxtPartyCity)
        Me.Controls.Add(Me.LblCity)
        Me.Controls.Add(Me.TxtPartyAdd1)
        Me.Controls.Add(Me.LblAddress)
        Me.Controls.Add(Me.TxtPartyName)
        Me.Controls.Add(Me.LblBuyerName)
        Me.Controls.Add(Me.BtnCancel)
        Me.Controls.Add(Me.BtnOk)
        Me.Controls.Add(Me.GroupBox2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(300, 300)
        Me.MaximizeBox = False
        Me.Name = "FrmPartyDetail"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Party Detail"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Public WithEvents BtnOk As System.Windows.Forms.Button
    Public WithEvents BtnCancel As System.Windows.Forms.Button
    Protected WithEvents LblBuyerName As System.Windows.Forms.Label
    Protected WithEvents LblAddress As System.Windows.Forms.Label
    Protected WithEvents LblCity As System.Windows.Forms.Label
    Protected WithEvents LblMobile As System.Windows.Forms.Label
    Public WithEvents TxtPartyName As AgControls.AgTextBox
    Public WithEvents TxtPartyAdd1 As AgControls.AgTextBox
    Public WithEvents TxtPartyCity As AgControls.AgTextBox
    Public WithEvents TxtPartyMobile As AgControls.AgTextBox
    Public WithEvents TxtPartyAdd2 As AgControls.AgTextBox
    Public WithEvents TxtPartyTinNo As AgControls.AgTextBox
    Protected WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents TxtPartyLSTNo As AgControls.AgTextBox
    Protected WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents TxtPartyCSTNo As AgControls.AgTextBox
    Protected WithEvents Label3 As System.Windows.Forms.Label
End Class
