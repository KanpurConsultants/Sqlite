Public Class FrmUnitConversion
    Inherits AgTemplate.TempMaster
    Dim mQry$

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
        Me.LblShapeReq = New System.Windows.Forms.Label
        Me.TxtFromUnit = New AgControls.AgTextBox
        Me.LblFromUnit = New System.Windows.Forms.Label
        Me.TxtToUnit = New AgControls.AgTextBox
        Me.LblToUnit = New System.Windows.Forms.Label
        Me.TxtMultiplier = New AgControls.AgTextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.TxtRounding = New AgControls.AgTextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
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
        Me.Topctrl1.Size = New System.Drawing.Size(869, 41)
        Me.Topctrl1.TabIndex = 4
        Me.Topctrl1.tAdd = False
        Me.Topctrl1.tDel = False
        Me.Topctrl1.tEdit = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Location = New System.Drawing.Point(0, 263)
        Me.GroupBox1.Size = New System.Drawing.Size(911, 4)
        '
        'GrpUP
        '
        Me.GrpUP.Location = New System.Drawing.Point(14, 267)
        '
        'TxtEntryBy
        '
        Me.TxtEntryBy.Tag = ""
        Me.TxtEntryBy.Text = ""
        '
        'GBoxEntryType
        '
        Me.GBoxEntryType.Location = New System.Drawing.Point(163, 267)
        '
        'TxtEntryType
        '
        Me.TxtEntryType.Tag = ""
        '
        'GBoxMoveToLog
        '
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(633, 267)
        '
        'TxtMoveToLog
        '
        Me.TxtMoveToLog.Tag = ""
        '
        'GBoxApprove
        '
        Me.GBoxApprove.Location = New System.Drawing.Point(459, 267)
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
        Me.GroupBox2.Location = New System.Drawing.Point(802, 267)
        '
        'GBoxDivision
        '
        Me.GBoxDivision.Location = New System.Drawing.Point(308, 267)
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
        Me.Label1.Location = New System.Drawing.Point(284, 108)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(10, 7)
        Me.Label1.TabIndex = 666
        Me.Label1.Text = "Ä"
        '
        'LblShapeReq
        '
        Me.LblShapeReq.AutoSize = True
        Me.LblShapeReq.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.LblShapeReq.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.LblShapeReq.Location = New System.Drawing.Point(284, 88)
        Me.LblShapeReq.Name = "LblShapeReq"
        Me.LblShapeReq.Size = New System.Drawing.Size(10, 7)
        Me.LblShapeReq.TabIndex = 664
        Me.LblShapeReq.Text = "Ä"
        '
        'TxtFromUnit
        '
        Me.TxtFromUnit.AgAllowUserToEnableMasterHelp = False
        Me.TxtFromUnit.AgMandatory = True
        Me.TxtFromUnit.AgMasterHelp = False
        Me.TxtFromUnit.AgNumberLeftPlaces = 0
        Me.TxtFromUnit.AgNumberNegetiveAllow = False
        Me.TxtFromUnit.AgNumberRightPlaces = 0
        Me.TxtFromUnit.AgPickFromLastValue = False
        Me.TxtFromUnit.AgRowFilter = ""
        Me.TxtFromUnit.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtFromUnit.AgSelectedValue = Nothing
        Me.TxtFromUnit.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtFromUnit.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtFromUnit.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtFromUnit.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtFromUnit.Location = New System.Drawing.Point(300, 80)
        Me.TxtFromUnit.MaxLength = 20
        Me.TxtFromUnit.Multiline = True
        Me.TxtFromUnit.Name = "TxtFromUnit"
        Me.TxtFromUnit.Size = New System.Drawing.Size(311, 20)
        Me.TxtFromUnit.TabIndex = 0
        '
        'LblFromUnit
        '
        Me.LblFromUnit.AutoSize = True
        Me.LblFromUnit.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblFromUnit.Location = New System.Drawing.Point(214, 83)
        Me.LblFromUnit.Name = "LblFromUnit"
        Me.LblFromUnit.Size = New System.Drawing.Size(65, 16)
        Me.LblFromUnit.TabIndex = 660
        Me.LblFromUnit.Text = "From Unit"
        '
        'TxtToUnit
        '
        Me.TxtToUnit.AgAllowUserToEnableMasterHelp = False
        Me.TxtToUnit.AgMandatory = True
        Me.TxtToUnit.AgMasterHelp = True
        Me.TxtToUnit.AgNumberLeftPlaces = 0
        Me.TxtToUnit.AgNumberNegetiveAllow = False
        Me.TxtToUnit.AgNumberRightPlaces = 0
        Me.TxtToUnit.AgPickFromLastValue = False
        Me.TxtToUnit.AgRowFilter = ""
        Me.TxtToUnit.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtToUnit.AgSelectedValue = Nothing
        Me.TxtToUnit.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtToUnit.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtToUnit.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtToUnit.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtToUnit.Location = New System.Drawing.Point(300, 102)
        Me.TxtToUnit.MaxLength = 50
        Me.TxtToUnit.Name = "TxtToUnit"
        Me.TxtToUnit.Size = New System.Drawing.Size(311, 18)
        Me.TxtToUnit.TabIndex = 1
        '
        'LblToUnit
        '
        Me.LblToUnit.AutoSize = True
        Me.LblToUnit.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblToUnit.Location = New System.Drawing.Point(214, 103)
        Me.LblToUnit.Name = "LblToUnit"
        Me.LblToUnit.Size = New System.Drawing.Size(49, 16)
        Me.LblToUnit.TabIndex = 661
        Me.LblToUnit.Text = "To Unit"
        '
        'TxtMultiplier
        '
        Me.TxtMultiplier.AgAllowUserToEnableMasterHelp = False
        Me.TxtMultiplier.AgMandatory = True
        Me.TxtMultiplier.AgMasterHelp = True
        Me.TxtMultiplier.AgNumberLeftPlaces = 10
        Me.TxtMultiplier.AgNumberNegetiveAllow = False
        Me.TxtMultiplier.AgNumberRightPlaces = 9
        Me.TxtMultiplier.AgPickFromLastValue = False
        Me.TxtMultiplier.AgRowFilter = ""
        Me.TxtMultiplier.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtMultiplier.AgSelectedValue = Nothing
        Me.TxtMultiplier.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtMultiplier.AgValueType = AgControls.AgTextBox.TxtValueType.Number_Value
        Me.TxtMultiplier.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtMultiplier.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtMultiplier.Location = New System.Drawing.Point(300, 122)
        Me.TxtMultiplier.MaxLength = 20
        Me.TxtMultiplier.Name = "TxtMultiplier"
        Me.TxtMultiplier.Size = New System.Drawing.Size(117, 18)
        Me.TxtMultiplier.TabIndex = 2
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(214, 122)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(60, 16)
        Me.Label4.TabIndex = 692
        Me.Label4.Text = "Multiplier"
        '
        'TxtRounding
        '
        Me.TxtRounding.AgAllowUserToEnableMasterHelp = False
        Me.TxtRounding.AgMandatory = True
        Me.TxtRounding.AgMasterHelp = True
        Me.TxtRounding.AgNumberLeftPlaces = 2
        Me.TxtRounding.AgNumberNegetiveAllow = False
        Me.TxtRounding.AgNumberRightPlaces = 0
        Me.TxtRounding.AgPickFromLastValue = False
        Me.TxtRounding.AgRowFilter = ""
        Me.TxtRounding.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtRounding.AgSelectedValue = Nothing
        Me.TxtRounding.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtRounding.AgValueType = AgControls.AgTextBox.TxtValueType.Number_Value
        Me.TxtRounding.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtRounding.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtRounding.Location = New System.Drawing.Point(516, 122)
        Me.TxtRounding.MaxLength = 20
        Me.TxtRounding.Name = "TxtRounding"
        Me.TxtRounding.Size = New System.Drawing.Size(95, 18)
        Me.TxtRounding.TabIndex = 3
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(426, 123)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(62, 16)
        Me.Label7.TabIndex = 698
        Me.Label7.Text = "Rounding"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(284, 129)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(10, 7)
        Me.Label2.TabIndex = 699
        Me.Label2.Text = "Ä"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(494, 128)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(10, 7)
        Me.Label3.TabIndex = 700
        Me.Label3.Text = "Ä"
        '
        'FrmUnitConversion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.BackColor = System.Drawing.Color.Silver
        Me.ClientSize = New System.Drawing.Size(869, 311)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TxtRounding)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.TxtMultiplier)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.LblShapeReq)
        Me.Controls.Add(Me.TxtFromUnit)
        Me.Controls.Add(Me.LblFromUnit)
        Me.Controls.Add(Me.TxtToUnit)
        Me.Controls.Add(Me.LblToUnit)
        Me.Name = "FrmUnitConversion"
        Me.Text = "Quality Master"
        Me.Controls.SetChildIndex(Me.GBoxDivision, 0)
        Me.Controls.SetChildIndex(Me.GroupBox2, 0)
        Me.Controls.SetChildIndex(Me.Topctrl1, 0)
        Me.Controls.SetChildIndex(Me.GroupBox1, 0)
        Me.Controls.SetChildIndex(Me.GrpUP, 0)
        Me.Controls.SetChildIndex(Me.GBoxEntryType, 0)
        Me.Controls.SetChildIndex(Me.GBoxApprove, 0)
        Me.Controls.SetChildIndex(Me.GBoxMoveToLog, 0)
        Me.Controls.SetChildIndex(Me.LblToUnit, 0)
        Me.Controls.SetChildIndex(Me.TxtToUnit, 0)
        Me.Controls.SetChildIndex(Me.LblFromUnit, 0)
        Me.Controls.SetChildIndex(Me.TxtFromUnit, 0)
        Me.Controls.SetChildIndex(Me.LblShapeReq, 0)
        Me.Controls.SetChildIndex(Me.Label1, 0)
        Me.Controls.SetChildIndex(Me.Label4, 0)
        Me.Controls.SetChildIndex(Me.TxtMultiplier, 0)
        Me.Controls.SetChildIndex(Me.Label7, 0)
        Me.Controls.SetChildIndex(Me.TxtRounding, 0)
        Me.Controls.SetChildIndex(Me.Label2, 0)
        Me.Controls.SetChildIndex(Me.Label3, 0)
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

    Friend WithEvents LblToUnit As System.Windows.Forms.Label
    Friend WithEvents TxtToUnit As AgControls.AgTextBox
    Friend WithEvents LblFromUnit As System.Windows.Forms.Label
    Friend WithEvents TxtFromUnit As AgControls.AgTextBox
    Friend WithEvents LblShapeReq As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TxtMultiplier As AgControls.AgTextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents TxtRounding As AgControls.AgTextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label


#End Region

    Private Sub FrmYarn_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        If Topctrl1.Mode = "Add" Then
            mQry = "Select count(*) From UnitConversion Where FromUnit ='" & TxtFromUnit.Text & "' And  ToUnit = '" & TxtToUnit.Text & "'  "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Conversion Master For " & TxtFromUnit.Text & " And " & TxtToUnit.Text & " Already Exist!")
        Else
            mQry = "Select count(*) From UnitConversion Where FromUnit ='" & TxtFromUnit.Text & "' And  ToUnit = '" & TxtToUnit.Text & "'  And Code<>'" & mInternalCode & "'  "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Conversion Master For " & TxtFromUnit.Text & " And " & TxtToUnit.Text & " Already Exist!")
        End If
    End Sub

    Private Sub FrmYarn_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        Dim mConStr$ = ""
        AgL.PubFindQry = " SELECT H.Code AS SearchCode,  H.FromUnit, H.ToUnit " & _
                            " FROM UnitConversion H  " & mConStr
        AgL.PubFindQryOrdBy = "[FromUuit]"
    End Sub

    Private Sub FrmYarn_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "UnitConversion"
    End Sub

    Private Sub FrmYarn_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As System.Data.SqlClient.SqlConnection, ByVal Cmd As System.Data.SqlClient.SqlCommand) Handles Me.BaseEvent_Save_InTrans
        mQry = "UPDATE UnitConversion " & _
            " SET FromUnit = " & AgL.Chk_Text(TxtFromUnit.Text) & ", " & _
            " ToUnit = " & AgL.Chk_Text(TxtToUnit.Text) & ", " & _
            " Multiplier = " & Val(TxtMultiplier.Text) & ", " & _
            " Rounding = " & Val(TxtRounding.Text) & " " & _
            " Where Code = '" & SearchCode & "' "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
    End Sub

    Private Sub FrmQuality1_BaseFunction_DispText() Handles Me.BaseFunction_DispText
    End Sub

    Private Sub FrmQuality1_BaseFunction_FIniList() Handles Me.BaseFunction_FIniList
        mQry = " SELECT U.Code, U.Code AS Description  FROM Unit U "
        TxtFromUnit.AgHelpDataSet() = AgL.FillData(mQry, AgL.GCn)
        TxtToUnit.AgHelpDataSet() = TxtFromUnit.AgHelpDataSet
    End Sub

    Private Sub FrmYarn_BaseFunction_FIniMast(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMast
        mQry = "Select Code As SearchCode " & _
                " From UnitConversion "
        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub FrmQuality1_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim DsTemp As DataSet

        mQry = "Select * From UnitConversion Where Code='" & SearchCode & "'"
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                mInternalCode = AgL.XNull(.Rows(0)("Code"))
                TxtFromUnit.Text = AgL.XNull(.Rows(0)("FromUnit"))
                TxtToUnit.Text = AgL.XNull(.Rows(0)("ToUnit"))
                TxtMultiplier.Text = AgL.VNull(.Rows(0)("Multiplier"))
                TxtRounding.Text = AgL.VNull(.Rows(0)("Rounding"))
            End If
        End With
        Topctrl1.tPrn = False
    End Sub

    Private Sub Topctrl1_tbAdd() Handles Topctrl1.tbAdd
        TxtFromUnit.Focus()
    End Sub

    Private Sub Topctrl1_tbEdit() Handles Topctrl1.tbEdit
        TxtFromUnit.Focus()
    End Sub

    Private Sub FrmYarn_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AgL.WinSetting(Me, 343, 875, 0, 0)
    End Sub

    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub

    Private Sub TxtDescription_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtRounding.KeyDown
        If e.KeyCode = Keys.Enter Then
            If MsgBox("Do you want to save?", MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "Save") = MsgBoxResult.Yes Then
                Topctrl1.FButtonClick(13)
            End If
        End If
    End Sub
End Class
