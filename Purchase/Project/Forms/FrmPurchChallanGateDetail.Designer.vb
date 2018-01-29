<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmPurchChallanGateDetail
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
        Me.TxtVehicleNo = New AgControls.AgTextBox
        Me.LblBuyerName = New System.Windows.Forms.Label
        Me.TxtTransporter = New AgControls.AgTextBox
        Me.LblAddress = New System.Windows.Forms.Label
        Me.TxtLRDate = New AgControls.AgTextBox
        Me.LblCity = New System.Windows.Forms.Label
        Me.TxtVehicleType = New AgControls.AgTextBox
        Me.LblMobile = New System.Windows.Forms.Label
        Me.TxtLRNo = New AgControls.AgTextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'BtnOk
        '
        Me.BtnOk.BackColor = System.Drawing.Color.Transparent
        Me.BtnOk.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnOk.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnOk.Location = New System.Drawing.Point(277, 144)
        Me.BtnOk.Name = "BtnOk"
        Me.BtnOk.Size = New System.Drawing.Size(60, 23)
        Me.BtnOk.TabIndex = 5
        Me.BtnOk.Text = "OK"
        Me.BtnOk.UseVisualStyleBackColor = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.BackColor = System.Drawing.Color.WhiteSmoke
        Me.GroupBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox2.Location = New System.Drawing.Point(5, 125)
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
        Me.BtnCancel.Location = New System.Drawing.Point(342, 144)
        Me.BtnCancel.Name = "BtnCancel"
        Me.BtnCancel.Size = New System.Drawing.Size(60, 23)
        Me.BtnCancel.TabIndex = 6
        Me.BtnCancel.Text = "Close"
        Me.BtnCancel.UseVisualStyleBackColor = False
        '
        'TxtVehicleNo
        '
        Me.TxtVehicleNo.AgAllowUserToEnableMasterHelp = False
        Me.TxtVehicleNo.AgMandatory = False
        Me.TxtVehicleNo.AgMasterHelp = False
        Me.TxtVehicleNo.AgNumberLeftPlaces = 8
        Me.TxtVehicleNo.AgNumberNegetiveAllow = False
        Me.TxtVehicleNo.AgNumberRightPlaces = 2
        Me.TxtVehicleNo.AgPickFromLastValue = False
        Me.TxtVehicleNo.AgRowFilter = ""
        Me.TxtVehicleNo.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtVehicleNo.AgSelectedValue = Nothing
        Me.TxtVehicleNo.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtVehicleNo.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtVehicleNo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtVehicleNo.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtVehicleNo.Location = New System.Drawing.Point(105, 52)
        Me.TxtVehicleNo.MaxLength = 0
        Me.TxtVehicleNo.Name = "TxtVehicleNo"
        Me.TxtVehicleNo.Size = New System.Drawing.Size(300, 18)
        Me.TxtVehicleNo.TabIndex = 1
        '
        'LblBuyerName
        '
        Me.LblBuyerName.AutoSize = True
        Me.LblBuyerName.BackColor = System.Drawing.Color.Transparent
        Me.LblBuyerName.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblBuyerName.Location = New System.Drawing.Point(15, 53)
        Me.LblBuyerName.Name = "LblBuyerName"
        Me.LblBuyerName.Size = New System.Drawing.Size(71, 16)
        Me.LblBuyerName.TabIndex = 742
        Me.LblBuyerName.Text = "Vehicle No"
        '
        'TxtTransporter
        '
        Me.TxtTransporter.AgAllowUserToEnableMasterHelp = False
        Me.TxtTransporter.AgMandatory = False
        Me.TxtTransporter.AgMasterHelp = False
        Me.TxtTransporter.AgNumberLeftPlaces = 8
        Me.TxtTransporter.AgNumberNegetiveAllow = False
        Me.TxtTransporter.AgNumberRightPlaces = 2
        Me.TxtTransporter.AgPickFromLastValue = False
        Me.TxtTransporter.AgRowFilter = ""
        Me.TxtTransporter.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtTransporter.AgSelectedValue = Nothing
        Me.TxtTransporter.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtTransporter.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtTransporter.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtTransporter.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtTransporter.Location = New System.Drawing.Point(105, 72)
        Me.TxtTransporter.MaxLength = 0
        Me.TxtTransporter.Name = "TxtTransporter"
        Me.TxtTransporter.Size = New System.Drawing.Size(300, 18)
        Me.TxtTransporter.TabIndex = 2
        '
        'LblAddress
        '
        Me.LblAddress.AutoSize = True
        Me.LblAddress.BackColor = System.Drawing.Color.Transparent
        Me.LblAddress.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblAddress.Location = New System.Drawing.Point(15, 73)
        Me.LblAddress.Name = "LblAddress"
        Me.LblAddress.Size = New System.Drawing.Size(73, 16)
        Me.LblAddress.TabIndex = 744
        Me.LblAddress.Text = "Transporter"
        '
        'TxtLRDate
        '
        Me.TxtLRDate.AgAllowUserToEnableMasterHelp = False
        Me.TxtLRDate.AgMandatory = False
        Me.TxtLRDate.AgMasterHelp = False
        Me.TxtLRDate.AgNumberLeftPlaces = 8
        Me.TxtLRDate.AgNumberNegetiveAllow = False
        Me.TxtLRDate.AgNumberRightPlaces = 2
        Me.TxtLRDate.AgPickFromLastValue = False
        Me.TxtLRDate.AgRowFilter = ""
        Me.TxtLRDate.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtLRDate.AgSelectedValue = Nothing
        Me.TxtLRDate.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtLRDate.AgValueType = AgControls.AgTextBox.TxtValueType.Date_Value
        Me.TxtLRDate.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtLRDate.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtLRDate.Location = New System.Drawing.Point(298, 92)
        Me.TxtLRDate.MaxLength = 0
        Me.TxtLRDate.Name = "TxtLRDate"
        Me.TxtLRDate.Size = New System.Drawing.Size(107, 18)
        Me.TxtLRDate.TabIndex = 4
        '
        'LblCity
        '
        Me.LblCity.AutoSize = True
        Me.LblCity.BackColor = System.Drawing.Color.Transparent
        Me.LblCity.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblCity.Location = New System.Drawing.Point(235, 93)
        Me.LblCity.Name = "LblCity"
        Me.LblCity.Size = New System.Drawing.Size(59, 16)
        Me.LblCity.TabIndex = 746
        Me.LblCity.Text = "L.R.Date"
        '
        'TxtVehicleType
        '
        Me.TxtVehicleType.AgAllowUserToEnableMasterHelp = False
        Me.TxtVehicleType.AgMandatory = False
        Me.TxtVehicleType.AgMasterHelp = False
        Me.TxtVehicleType.AgNumberLeftPlaces = 8
        Me.TxtVehicleType.AgNumberNegetiveAllow = False
        Me.TxtVehicleType.AgNumberRightPlaces = 2
        Me.TxtVehicleType.AgPickFromLastValue = False
        Me.TxtVehicleType.AgRowFilter = ""
        Me.TxtVehicleType.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtVehicleType.AgSelectedValue = Nothing
        Me.TxtVehicleType.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtVehicleType.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtVehicleType.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtVehicleType.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtVehicleType.Location = New System.Drawing.Point(105, 32)
        Me.TxtVehicleType.MaxLength = 0
        Me.TxtVehicleType.Name = "TxtVehicleType"
        Me.TxtVehicleType.Size = New System.Drawing.Size(300, 18)
        Me.TxtVehicleType.TabIndex = 0
        '
        'LblMobile
        '
        Me.LblMobile.AutoSize = True
        Me.LblMobile.BackColor = System.Drawing.Color.Transparent
        Me.LblMobile.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblMobile.Location = New System.Drawing.Point(15, 33)
        Me.LblMobile.Name = "LblMobile"
        Me.LblMobile.Size = New System.Drawing.Size(83, 16)
        Me.LblMobile.TabIndex = 748
        Me.LblMobile.Text = "Vehicle Type"
        '
        'TxtLRNo
        '
        Me.TxtLRNo.AgAllowUserToEnableMasterHelp = False
        Me.TxtLRNo.AgMandatory = False
        Me.TxtLRNo.AgMasterHelp = False
        Me.TxtLRNo.AgNumberLeftPlaces = 8
        Me.TxtLRNo.AgNumberNegetiveAllow = False
        Me.TxtLRNo.AgNumberRightPlaces = 2
        Me.TxtLRNo.AgPickFromLastValue = False
        Me.TxtLRNo.AgRowFilter = ""
        Me.TxtLRNo.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtLRNo.AgSelectedValue = Nothing
        Me.TxtLRNo.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtLRNo.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtLRNo.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtLRNo.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtLRNo.Location = New System.Drawing.Point(105, 92)
        Me.TxtLRNo.MaxLength = 0
        Me.TxtLRNo.Name = "TxtLRNo"
        Me.TxtLRNo.Size = New System.Drawing.Size(126, 18)
        Me.TxtLRNo.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(15, 93)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(48, 16)
        Me.Label1.TabIndex = 750
        Me.Label1.Text = "L.R.No"
        '
        'FrmPurchChallanGateDetail
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(421, 177)
        Me.ControlBox = False
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TxtLRNo)
        Me.Controls.Add(Me.TxtVehicleType)
        Me.Controls.Add(Me.LblMobile)
        Me.Controls.Add(Me.TxtLRDate)
        Me.Controls.Add(Me.LblCity)
        Me.Controls.Add(Me.TxtTransporter)
        Me.Controls.Add(Me.LblAddress)
        Me.Controls.Add(Me.TxtVehicleNo)
        Me.Controls.Add(Me.LblBuyerName)
        Me.Controls.Add(Me.BtnCancel)
        Me.Controls.Add(Me.BtnOk)
        Me.Controls.Add(Me.GroupBox2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(300, 300)
        Me.MaximizeBox = False
        Me.Name = "FrmPurchChallanGateDetail"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Gate Detail"
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
    Public WithEvents TxtVehicleNo As AgControls.AgTextBox
    Public WithEvents TxtTransporter As AgControls.AgTextBox
    Public WithEvents TxtLRDate As AgControls.AgTextBox
    Public WithEvents TxtVehicleType As AgControls.AgTextBox
    Public WithEvents TxtLRNo As AgControls.AgTextBox
    Protected WithEvents Label1 As System.Windows.Forms.Label
End Class
