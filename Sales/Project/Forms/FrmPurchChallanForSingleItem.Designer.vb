<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPurchChallanForSingleItem
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
        Me.TxtVendor = New AgControls.AgTextBox
        Me.LblBuyerName = New System.Windows.Forms.Label
        Me.TxtItem = New AgControls.AgTextBox
        Me.LblMobile = New System.Windows.Forms.Label
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.LblItemText = New System.Windows.Forms.Label
        Me.LblQty = New System.Windows.Forms.Label
        Me.TxtQty = New AgControls.AgTextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.TxtRate = New AgControls.AgTextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.TxtMRP = New AgControls.AgTextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.TxtExpiry = New AgControls.AgTextBox
        Me.TxtUnit = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.TxtSaleRate = New AgControls.AgTextBox
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'BtnOk
        '
        Me.BtnOk.BackColor = System.Drawing.Color.Transparent
        Me.BtnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnOk.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnOk.Location = New System.Drawing.Point(596, 181)
        Me.BtnOk.Name = "BtnOk"
        Me.BtnOk.Size = New System.Drawing.Size(60, 23)
        Me.BtnOk.TabIndex = 7
        Me.BtnOk.Text = "OK"
        Me.BtnOk.UseVisualStyleBackColor = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.BackColor = System.Drawing.Color.WhiteSmoke
        Me.GroupBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox2.Location = New System.Drawing.Point(5, 162)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(730, 5)
        Me.GroupBox2.TabIndex = 737
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Tag = ""
        '
        'BtnCancel
        '
        Me.BtnCancel.BackColor = System.Drawing.Color.Transparent
        Me.BtnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnCancel.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnCancel.Location = New System.Drawing.Point(661, 181)
        Me.BtnCancel.Name = "BtnCancel"
        Me.BtnCancel.Size = New System.Drawing.Size(60, 23)
        Me.BtnCancel.TabIndex = 8
        Me.BtnCancel.Text = "Close"
        Me.BtnCancel.UseVisualStyleBackColor = False
        '
        'TxtVendor
        '
        Me.TxtVendor.AgAllowUserToEnableMasterHelp = False
        Me.TxtVendor.AgLastValueTag = Nothing
        Me.TxtVendor.AgLastValueText = Nothing
        Me.TxtVendor.AgMandatory = True
        Me.TxtVendor.AgMasterHelp = False
        Me.TxtVendor.AgNumberLeftPlaces = 8
        Me.TxtVendor.AgNumberNegetiveAllow = False
        Me.TxtVendor.AgNumberRightPlaces = 2
        Me.TxtVendor.AgPickFromLastValue = False
        Me.TxtVendor.AgRowFilter = ""
        Me.TxtVendor.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtVendor.AgSelectedValue = Nothing
        Me.TxtVendor.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtVendor.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtVendor.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtVendor.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtVendor.Location = New System.Drawing.Point(72, 74)
        Me.TxtVendor.MaxLength = 0
        Me.TxtVendor.Name = "TxtVendor"
        Me.TxtVendor.Size = New System.Drawing.Size(649, 18)
        Me.TxtVendor.TabIndex = 0
        '
        'LblBuyerName
        '
        Me.LblBuyerName.AutoSize = True
        Me.LblBuyerName.BackColor = System.Drawing.Color.Transparent
        Me.LblBuyerName.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblBuyerName.Location = New System.Drawing.Point(7, 75)
        Me.LblBuyerName.Name = "LblBuyerName"
        Me.LblBuyerName.Size = New System.Drawing.Size(39, 16)
        Me.LblBuyerName.TabIndex = 742
        Me.LblBuyerName.Text = "Party"
        '
        'TxtItem
        '
        Me.TxtItem.AgAllowUserToEnableMasterHelp = False
        Me.TxtItem.AgLastValueTag = Nothing
        Me.TxtItem.AgLastValueText = Nothing
        Me.TxtItem.AgMandatory = False
        Me.TxtItem.AgMasterHelp = False
        Me.TxtItem.AgNumberLeftPlaces = 8
        Me.TxtItem.AgNumberNegetiveAllow = False
        Me.TxtItem.AgNumberRightPlaces = 2
        Me.TxtItem.AgPickFromLastValue = False
        Me.TxtItem.AgRowFilter = ""
        Me.TxtItem.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtItem.AgSelectedValue = Nothing
        Me.TxtItem.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtItem.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtItem.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtItem.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtItem.Location = New System.Drawing.Point(72, 94)
        Me.TxtItem.MaxLength = 0
        Me.TxtItem.Name = "TxtItem"
        Me.TxtItem.Size = New System.Drawing.Size(649, 18)
        Me.TxtItem.TabIndex = 1
        '
        'LblMobile
        '
        Me.LblMobile.AutoSize = True
        Me.LblMobile.BackColor = System.Drawing.Color.Transparent
        Me.LblMobile.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblMobile.Location = New System.Drawing.Point(7, 95)
        Me.LblMobile.Name = "LblMobile"
        Me.LblMobile.Size = New System.Drawing.Size(33, 16)
        Me.LblMobile.TabIndex = 748
        Me.LblMobile.Text = "Item"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.White
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.LblItemText)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(735, 52)
        Me.Panel1.TabIndex = 750
        '
        'LblItemText
        '
        Me.LblItemText.AutoSize = True
        Me.LblItemText.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblItemText.Location = New System.Drawing.Point(11, 16)
        Me.LblItemText.Name = "LblItemText"
        Me.LblItemText.Size = New System.Drawing.Size(179, 16)
        Me.LblItemText.TabIndex = 737
        Me.LblItemText.Text = "Purhcase Challan Detail"
        '
        'LblQty
        '
        Me.LblQty.AutoSize = True
        Me.LblQty.BackColor = System.Drawing.Color.Transparent
        Me.LblQty.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblQty.Location = New System.Drawing.Point(6, 115)
        Me.LblQty.Name = "LblQty"
        Me.LblQty.Size = New System.Drawing.Size(29, 16)
        Me.LblQty.TabIndex = 753
        Me.LblQty.Text = "Qty"
        '
        'TxtQty
        '
        Me.TxtQty.AgAllowUserToEnableMasterHelp = False
        Me.TxtQty.AgLastValueTag = Nothing
        Me.TxtQty.AgLastValueText = Nothing
        Me.TxtQty.AgMandatory = True
        Me.TxtQty.AgMasterHelp = False
        Me.TxtQty.AgNumberLeftPlaces = 8
        Me.TxtQty.AgNumberNegetiveAllow = False
        Me.TxtQty.AgNumberRightPlaces = 2
        Me.TxtQty.AgPickFromLastValue = False
        Me.TxtQty.AgRowFilter = ""
        Me.TxtQty.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtQty.AgSelectedValue = Nothing
        Me.TxtQty.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtQty.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtQty.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtQty.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtQty.Location = New System.Drawing.Point(71, 114)
        Me.TxtQty.MaxLength = 0
        Me.TxtQty.Name = "TxtQty"
        Me.TxtQty.Size = New System.Drawing.Size(75, 18)
        Me.TxtQty.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(187, 115)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(35, 16)
        Me.Label2.TabIndex = 755
        Me.Label2.Text = "Rate"
        '
        'TxtRate
        '
        Me.TxtRate.AgAllowUserToEnableMasterHelp = False
        Me.TxtRate.AgLastValueTag = Nothing
        Me.TxtRate.AgLastValueText = Nothing
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
        Me.TxtRate.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtRate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtRate.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtRate.Location = New System.Drawing.Point(228, 114)
        Me.TxtRate.MaxLength = 0
        Me.TxtRate.Name = "TxtRate"
        Me.TxtRate.Size = New System.Drawing.Size(59, 18)
        Me.TxtRate.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(293, 116)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(37, 16)
        Me.Label3.TabIndex = 757
        Me.Label3.Text = "MRP"
        '
        'TxtMRP
        '
        Me.TxtMRP.AgAllowUserToEnableMasterHelp = False
        Me.TxtMRP.AgLastValueTag = Nothing
        Me.TxtMRP.AgLastValueText = Nothing
        Me.TxtMRP.AgMandatory = False
        Me.TxtMRP.AgMasterHelp = False
        Me.TxtMRP.AgNumberLeftPlaces = 8
        Me.TxtMRP.AgNumberNegetiveAllow = False
        Me.TxtMRP.AgNumberRightPlaces = 2
        Me.TxtMRP.AgPickFromLastValue = False
        Me.TxtMRP.AgRowFilter = ""
        Me.TxtMRP.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtMRP.AgSelectedValue = Nothing
        Me.TxtMRP.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtMRP.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtMRP.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtMRP.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtMRP.Location = New System.Drawing.Point(336, 114)
        Me.TxtMRP.MaxLength = 0
        Me.TxtMRP.Name = "TxtMRP"
        Me.TxtMRP.Size = New System.Drawing.Size(71, 18)
        Me.TxtMRP.TabIndex = 4
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(413, 114)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(45, 16)
        Me.Label4.TabIndex = 759
        Me.Label4.Text = "Expiry"
        '
        'TxtExpiry
        '
        Me.TxtExpiry.AgAllowUserToEnableMasterHelp = False
        Me.TxtExpiry.AgLastValueTag = Nothing
        Me.TxtExpiry.AgLastValueText = Nothing
        Me.TxtExpiry.AgMandatory = False
        Me.TxtExpiry.AgMasterHelp = False
        Me.TxtExpiry.AgNumberLeftPlaces = 8
        Me.TxtExpiry.AgNumberNegetiveAllow = False
        Me.TxtExpiry.AgNumberRightPlaces = 2
        Me.TxtExpiry.AgPickFromLastValue = False
        Me.TxtExpiry.AgRowFilter = ""
        Me.TxtExpiry.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtExpiry.AgSelectedValue = Nothing
        Me.TxtExpiry.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtExpiry.AgValueType = AgControls.AgTextBox.TxtValueType.Date_Value
        Me.TxtExpiry.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtExpiry.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtExpiry.Location = New System.Drawing.Point(467, 114)
        Me.TxtExpiry.MaxLength = 0
        Me.TxtExpiry.Name = "TxtExpiry"
        Me.TxtExpiry.Size = New System.Drawing.Size(104, 18)
        Me.TxtExpiry.TabIndex = 5
        '
        'TxtUnit
        '
        Me.TxtUnit.AutoSize = True
        Me.TxtUnit.BackColor = System.Drawing.Color.Transparent
        Me.TxtUnit.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtUnit.Location = New System.Drawing.Point(150, 115)
        Me.TxtUnit.Name = "TxtUnit"
        Me.TxtUnit.Size = New System.Drawing.Size(31, 16)
        Me.TxtUnit.TabIndex = 761
        Me.TxtUnit.Text = "Unit"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label6.Location = New System.Drawing.Point(56, 81)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(10, 7)
        Me.Label6.TabIndex = 3004
        Me.Label6.Text = "Ä"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label7.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label7.Location = New System.Drawing.Point(56, 99)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(10, 7)
        Me.Label7.TabIndex = 3005
        Me.Label7.Text = "Ä"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label8.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label8.Location = New System.Drawing.Point(55, 120)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(10, 7)
        Me.Label8.TabIndex = 3006
        Me.Label8.Text = "Ä"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(579, 116)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 16)
        Me.Label1.TabIndex = 3008
        Me.Label1.Text = "Sale Rate"
        '
        'TxtSaleRate
        '
        Me.TxtSaleRate.AgAllowUserToEnableMasterHelp = False
        Me.TxtSaleRate.AgLastValueTag = Nothing
        Me.TxtSaleRate.AgLastValueText = Nothing
        Me.TxtSaleRate.AgMandatory = False
        Me.TxtSaleRate.AgMasterHelp = False
        Me.TxtSaleRate.AgNumberLeftPlaces = 8
        Me.TxtSaleRate.AgNumberNegetiveAllow = False
        Me.TxtSaleRate.AgNumberRightPlaces = 2
        Me.TxtSaleRate.AgPickFromLastValue = False
        Me.TxtSaleRate.AgRowFilter = ""
        Me.TxtSaleRate.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtSaleRate.AgSelectedValue = Nothing
        Me.TxtSaleRate.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtSaleRate.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtSaleRate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtSaleRate.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtSaleRate.Location = New System.Drawing.Point(650, 114)
        Me.TxtSaleRate.MaxLength = 0
        Me.TxtSaleRate.Name = "TxtSaleRate"
        Me.TxtSaleRate.Size = New System.Drawing.Size(71, 18)
        Me.TxtSaleRate.TabIndex = 6
        '
        'FrmPurchChallanForSingleItem
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(735, 222)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TxtSaleRate)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.TxtUnit)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TxtExpiry)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.TxtMRP)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TxtRate)
        Me.Controls.Add(Me.LblQty)
        Me.Controls.Add(Me.TxtQty)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.TxtItem)
        Me.Controls.Add(Me.LblMobile)
        Me.Controls.Add(Me.TxtVendor)
        Me.Controls.Add(Me.LblBuyerName)
        Me.Controls.Add(Me.BtnCancel)
        Me.Controls.Add(Me.BtnOk)
        Me.Controls.Add(Me.GroupBox2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(300, 300)
        Me.MaximizeBox = False
        Me.Name = "FrmPurchChallanForSingleItem"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = " "
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Public WithEvents BtnOk As System.Windows.Forms.Button
    Public WithEvents BtnCancel As System.Windows.Forms.Button
    Protected WithEvents LblBuyerName As System.Windows.Forms.Label
    Protected WithEvents LblMobile As System.Windows.Forms.Label
    Public WithEvents TxtVendor As AgControls.AgTextBox
    Public WithEvents TxtItem As AgControls.AgTextBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Public WithEvents LblItemText As System.Windows.Forms.Label
    Protected WithEvents LblQty As System.Windows.Forms.Label
    Public WithEvents TxtQty As AgControls.AgTextBox
    Protected WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents TxtRate As AgControls.AgTextBox
    Protected WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents TxtMRP As AgControls.AgTextBox
    Protected WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents TxtExpiry As AgControls.AgTextBox
    Protected WithEvents TxtUnit As System.Windows.Forms.Label
    Protected WithEvents Label6 As System.Windows.Forms.Label
    Protected WithEvents Label7 As System.Windows.Forms.Label
    Protected WithEvents Label8 As System.Windows.Forms.Label
    Protected WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents TxtSaleRate As AgControls.AgTextBox
End Class
