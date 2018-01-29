<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmSaleInvoicePartyDetail
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
        Me.TxtSaleToPartyName = New AgControls.AgTextBox
        Me.LblBuyerName = New System.Windows.Forms.Label
        Me.TxtSaleToPartyAdd1 = New AgControls.AgTextBox
        Me.LblAddress = New System.Windows.Forms.Label
        Me.TxtSaleToPartyCity = New AgControls.AgTextBox
        Me.LblCity = New System.Windows.Forms.Label
        Me.TxtSaleToPartyMobile = New AgControls.AgTextBox
        Me.LblMobile = New System.Windows.Forms.Label
        Me.TxtSaleToPartyAdd2 = New AgControls.AgTextBox
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
        Me.GroupBox2.Size = New System.Drawing.Size(416, 5)
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
        'TxtSaleToPartyName
        '
        Me.TxtSaleToPartyName.AgAllowUserToEnableMasterHelp = False
        Me.TxtSaleToPartyName.AgMandatory = True
        Me.TxtSaleToPartyName.AgMasterHelp = False
        Me.TxtSaleToPartyName.AgNumberLeftPlaces = 8
        Me.TxtSaleToPartyName.AgNumberNegetiveAllow = False
        Me.TxtSaleToPartyName.AgNumberRightPlaces = 2
        Me.TxtSaleToPartyName.AgPickFromLastValue = False
        Me.TxtSaleToPartyName.AgRowFilter = ""
        Me.TxtSaleToPartyName.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtSaleToPartyName.AgSelectedValue = Nothing
        Me.TxtSaleToPartyName.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtSaleToPartyName.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtSaleToPartyName.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtSaleToPartyName.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtSaleToPartyName.Location = New System.Drawing.Point(102, 42)
        Me.TxtSaleToPartyName.MaxLength = 0
        Me.TxtSaleToPartyName.Name = "TxtSaleToPartyName"
        Me.TxtSaleToPartyName.Size = New System.Drawing.Size(300, 18)
        Me.TxtSaleToPartyName.TabIndex = 1
        '
        'LblBuyerName
        '
        Me.LblBuyerName.AutoSize = True
        Me.LblBuyerName.BackColor = System.Drawing.Color.Transparent
        Me.LblBuyerName.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblBuyerName.Location = New System.Drawing.Point(19, 43)
        Me.LblBuyerName.Name = "LblBuyerName"
        Me.LblBuyerName.Size = New System.Drawing.Size(77, 16)
        Me.LblBuyerName.TabIndex = 742
        Me.LblBuyerName.Text = "Party Name"
        '
        'TxtSaleToPartyAdd1
        '
        Me.TxtSaleToPartyAdd1.AgAllowUserToEnableMasterHelp = False
        Me.TxtSaleToPartyAdd1.AgMandatory = True
        Me.TxtSaleToPartyAdd1.AgMasterHelp = False
        Me.TxtSaleToPartyAdd1.AgNumberLeftPlaces = 8
        Me.TxtSaleToPartyAdd1.AgNumberNegetiveAllow = False
        Me.TxtSaleToPartyAdd1.AgNumberRightPlaces = 2
        Me.TxtSaleToPartyAdd1.AgPickFromLastValue = False
        Me.TxtSaleToPartyAdd1.AgRowFilter = ""
        Me.TxtSaleToPartyAdd1.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtSaleToPartyAdd1.AgSelectedValue = Nothing
        Me.TxtSaleToPartyAdd1.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtSaleToPartyAdd1.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtSaleToPartyAdd1.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtSaleToPartyAdd1.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtSaleToPartyAdd1.Location = New System.Drawing.Point(102, 62)
        Me.TxtSaleToPartyAdd1.MaxLength = 0
        Me.TxtSaleToPartyAdd1.Name = "TxtSaleToPartyAdd1"
        Me.TxtSaleToPartyAdd1.Size = New System.Drawing.Size(300, 18)
        Me.TxtSaleToPartyAdd1.TabIndex = 2
        '
        'LblAddress
        '
        Me.LblAddress.AutoSize = True
        Me.LblAddress.BackColor = System.Drawing.Color.Transparent
        Me.LblAddress.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblAddress.Location = New System.Drawing.Point(19, 63)
        Me.LblAddress.Name = "LblAddress"
        Me.LblAddress.Size = New System.Drawing.Size(56, 16)
        Me.LblAddress.TabIndex = 744
        Me.LblAddress.Text = "Address"
        '
        'TxtSaleToPartyCity
        '
        Me.TxtSaleToPartyCity.AgAllowUserToEnableMasterHelp = False
        Me.TxtSaleToPartyCity.AgMandatory = True
        Me.TxtSaleToPartyCity.AgMasterHelp = False
        Me.TxtSaleToPartyCity.AgNumberLeftPlaces = 8
        Me.TxtSaleToPartyCity.AgNumberNegetiveAllow = False
        Me.TxtSaleToPartyCity.AgNumberRightPlaces = 2
        Me.TxtSaleToPartyCity.AgPickFromLastValue = False
        Me.TxtSaleToPartyCity.AgRowFilter = ""
        Me.TxtSaleToPartyCity.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtSaleToPartyCity.AgSelectedValue = Nothing
        Me.TxtSaleToPartyCity.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtSaleToPartyCity.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtSaleToPartyCity.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtSaleToPartyCity.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtSaleToPartyCity.Location = New System.Drawing.Point(102, 102)
        Me.TxtSaleToPartyCity.MaxLength = 0
        Me.TxtSaleToPartyCity.Name = "TxtSaleToPartyCity"
        Me.TxtSaleToPartyCity.Size = New System.Drawing.Size(126, 18)
        Me.TxtSaleToPartyCity.TabIndex = 3
        '
        'LblCity
        '
        Me.LblCity.AutoSize = True
        Me.LblCity.BackColor = System.Drawing.Color.Transparent
        Me.LblCity.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCity.Location = New System.Drawing.Point(19, 103)
        Me.LblCity.Name = "LblCity"
        Me.LblCity.Size = New System.Drawing.Size(31, 16)
        Me.LblCity.TabIndex = 746
        Me.LblCity.Text = "City"
        '
        'TxtSaleToPartyMobile
        '
        Me.TxtSaleToPartyMobile.AgAllowUserToEnableMasterHelp = False
        Me.TxtSaleToPartyMobile.AgMandatory = True
        Me.TxtSaleToPartyMobile.AgMasterHelp = False
        Me.TxtSaleToPartyMobile.AgNumberLeftPlaces = 8
        Me.TxtSaleToPartyMobile.AgNumberNegetiveAllow = False
        Me.TxtSaleToPartyMobile.AgNumberRightPlaces = 2
        Me.TxtSaleToPartyMobile.AgPickFromLastValue = False
        Me.TxtSaleToPartyMobile.AgRowFilter = ""
        Me.TxtSaleToPartyMobile.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtSaleToPartyMobile.AgSelectedValue = Nothing
        Me.TxtSaleToPartyMobile.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtSaleToPartyMobile.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtSaleToPartyMobile.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtSaleToPartyMobile.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtSaleToPartyMobile.Location = New System.Drawing.Point(102, 22)
        Me.TxtSaleToPartyMobile.MaxLength = 0
        Me.TxtSaleToPartyMobile.Name = "TxtSaleToPartyMobile"
        Me.TxtSaleToPartyMobile.Size = New System.Drawing.Size(300, 18)
        Me.TxtSaleToPartyMobile.TabIndex = 0
        '
        'LblMobile
        '
        Me.LblMobile.AutoSize = True
        Me.LblMobile.BackColor = System.Drawing.Color.Transparent
        Me.LblMobile.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblMobile.Location = New System.Drawing.Point(19, 23)
        Me.LblMobile.Name = "LblMobile"
        Me.LblMobile.Size = New System.Drawing.Size(46, 16)
        Me.LblMobile.TabIndex = 748
        Me.LblMobile.Text = "Mobile"
        '
        'TxtSaleToPartyAdd2
        '
        Me.TxtSaleToPartyAdd2.AgAllowUserToEnableMasterHelp = False
        Me.TxtSaleToPartyAdd2.AgMandatory = True
        Me.TxtSaleToPartyAdd2.AgMasterHelp = False
        Me.TxtSaleToPartyAdd2.AgNumberLeftPlaces = 8
        Me.TxtSaleToPartyAdd2.AgNumberNegetiveAllow = False
        Me.TxtSaleToPartyAdd2.AgNumberRightPlaces = 2
        Me.TxtSaleToPartyAdd2.AgPickFromLastValue = False
        Me.TxtSaleToPartyAdd2.AgRowFilter = ""
        Me.TxtSaleToPartyAdd2.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtSaleToPartyAdd2.AgSelectedValue = Nothing
        Me.TxtSaleToPartyAdd2.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtSaleToPartyAdd2.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtSaleToPartyAdd2.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtSaleToPartyAdd2.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtSaleToPartyAdd2.Location = New System.Drawing.Point(102, 82)
        Me.TxtSaleToPartyAdd2.MaxLength = 0
        Me.TxtSaleToPartyAdd2.Name = "TxtSaleToPartyAdd2"
        Me.TxtSaleToPartyAdd2.Size = New System.Drawing.Size(300, 18)
        Me.TxtSaleToPartyAdd2.TabIndex = 749
        '
        'FrmSaleInvoicePartyDetail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(421, 193)
        Me.ControlBox = False
        Me.Controls.Add(Me.TxtSaleToPartyAdd2)
        Me.Controls.Add(Me.TxtSaleToPartyMobile)
        Me.Controls.Add(Me.LblMobile)
        Me.Controls.Add(Me.TxtSaleToPartyCity)
        Me.Controls.Add(Me.LblCity)
        Me.Controls.Add(Me.TxtSaleToPartyAdd1)
        Me.Controls.Add(Me.LblAddress)
        Me.Controls.Add(Me.TxtSaleToPartyName)
        Me.Controls.Add(Me.LblBuyerName)
        Me.Controls.Add(Me.BtnCancel)
        Me.Controls.Add(Me.BtnOk)
        Me.Controls.Add(Me.GroupBox2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(300, 300)
        Me.MaximizeBox = False
        Me.Name = "FrmSaleInvoicePartyDetail"
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
    Public WithEvents TxtSaleToPartyName As AgControls.AgTextBox
    Public WithEvents TxtSaleToPartyAdd1 As AgControls.AgTextBox
    Public WithEvents TxtSaleToPartyCity As AgControls.AgTextBox
    Public WithEvents TxtSaleToPartyMobile As AgControls.AgTextBox
    Public WithEvents TxtSaleToPartyAdd2 As AgControls.AgTextBox
End Class
