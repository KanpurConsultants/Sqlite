Imports System.Data.SQLite
Public Class FrmGodown
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
        Me.TxtSite_Code = New AgControls.AgTextBox
        Me.LblSite_Code = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.TxtDescription = New AgControls.AgTextBox
        Me.LblDescription = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
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
        Me.Topctrl1.tAdd = False
        Me.Topctrl1.tDel = False
        Me.Topctrl1.tEdit = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Location = New System.Drawing.Point(0, 304)
        Me.GroupBox1.Size = New System.Drawing.Size(904, 4)
        '
        'GrpUP
        '
        Me.GrpUP.Location = New System.Drawing.Point(6, 308)
        '
        'TxtEntryBy
        '
        Me.TxtEntryBy.Tag = ""
        Me.TxtEntryBy.Text = ""
        '
        'GBoxEntryType
        '
        Me.GBoxEntryType.Location = New System.Drawing.Point(142, 308)
        '
        'TxtEntryType
        '
        Me.TxtEntryType.Tag = ""
        '
        'GBoxMoveToLog
        '
        Me.GBoxMoveToLog.Location = New System.Drawing.Point(556, 308)
        '
        'TxtMoveToLog
        '
        Me.TxtMoveToLog.Tag = ""
        '
        'GBoxApprove
        '
        Me.GBoxApprove.Location = New System.Drawing.Point(400, 308)
        Me.GBoxApprove.Size = New System.Drawing.Size(147, 44)
        Me.GBoxApprove.Text = "Approved By"
        '
        'TxtApproveBy
        '
        Me.TxtApproveBy.Location = New System.Drawing.Point(3, 23)
        Me.TxtApproveBy.Size = New System.Drawing.Size(141, 18)
        Me.TxtApproveBy.Tag = ""
        '
        'CmdDiscard
        '
        Me.CmdDiscard.Location = New System.Drawing.Point(118, 18)
        '
        'GroupBox2
        '
        Me.GroupBox2.Location = New System.Drawing.Point(702, 308)
        '
        'GBoxDivision
        '
        Me.GBoxDivision.Location = New System.Drawing.Point(271, 308)
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
        'TxtSite_Code
        '
        Me.TxtSite_Code.AgAllowUserToEnableMasterHelp = False
        Me.TxtSite_Code.AgMandatory = False
        Me.TxtSite_Code.AgMasterHelp = False
        Me.TxtSite_Code.AgNumberLeftPlaces = 0
        Me.TxtSite_Code.AgNumberNegetiveAllow = False
        Me.TxtSite_Code.AgNumberRightPlaces = 0
        Me.TxtSite_Code.AgPickFromLastValue = False
        Me.TxtSite_Code.AgRowFilter = ""
        Me.TxtSite_Code.AgSearchMethod = AgControls.AgLib.TxtSearchMethod.Simple
        Me.TxtSite_Code.AgSelectedValue = Nothing
        Me.TxtSite_Code.AgTxtCase = AgControls.AgTextBox.TxtCase.None
        Me.TxtSite_Code.AgValueType = AgControls.AgTextBox.TxtValueType.Text_Value
        Me.TxtSite_Code.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.TxtSite_Code.Font = New System.Drawing.Font("Arial", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtSite_Code.Location = New System.Drawing.Point(337, 109)
        Me.TxtSite_Code.MaxLength = 20
        Me.TxtSite_Code.Name = "TxtSite_Code"
        Me.TxtSite_Code.Size = New System.Drawing.Size(129, 18)
        Me.TxtSite_Code.TabIndex = 672
        '
        'LblSite_Code
        '
        Me.LblSite_Code.AutoSize = True
        Me.LblSite_Code.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblSite_Code.Location = New System.Drawing.Point(222, 110)
        Me.LblSite_Code.Name = "LblSite_Code"
        Me.LblSite_Code.Size = New System.Drawing.Size(87, 16)
        Me.LblSite_Code.TabIndex = 681
        Me.LblSite_Code.Text = "Branch Name"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(321, 135)
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
        Me.TxtDescription.Location = New System.Drawing.Point(337, 129)
        Me.TxtDescription.MaxLength = 50
        Me.TxtDescription.Name = "TxtDescription"
        Me.TxtDescription.Size = New System.Drawing.Size(345, 18)
        Me.TxtDescription.TabIndex = 665
        '
        'LblDescription
        '
        Me.LblDescription.AutoSize = True
        Me.LblDescription.Font = New System.Drawing.Font("Arial", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblDescription.Location = New System.Drawing.Point(222, 130)
        Me.LblDescription.Name = "LblDescription"
        Me.LblDescription.Size = New System.Drawing.Size(93, 16)
        Me.LblDescription.TabIndex = 661
        Me.LblDescription.Text = "Godown Name"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Wingdings 2", 5.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(2, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(227, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(321, 115)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(10, 7)
        Me.Label2.TabIndex = 682
        Me.Label2.Text = "Ä"
        '
        'FrmGodown
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.ClientSize = New System.Drawing.Size(862, 352)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TxtSite_Code)
        Me.Controls.Add(Me.LblSite_Code)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TxtDescription)
        Me.Controls.Add(Me.LblDescription)
        Me.Name = "FrmGodown"
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
        Me.Controls.SetChildIndex(Me.LblSite_Code, 0)
        Me.Controls.SetChildIndex(Me.TxtSite_Code, 0)
        Me.Controls.SetChildIndex(Me.Label2, 0)
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
    Protected WithEvents LblSite_Code As System.Windows.Forms.Label
    Protected WithEvents Label2 As System.Windows.Forms.Label
    Protected WithEvents TxtSite_Code As AgControls.AgTextBox



#End Region

    Private Sub FrmShade_BaseEvent_Data_Validation(ByRef passed As Boolean) Handles Me.BaseEvent_Data_Validation
        If AgL.RequiredField(TxtDescription, "Description") Then passed = False : Exit Sub

        If Topctrl1.Mode = "Add" Then
            mQry = "Select count(*) From Godown Where Description='" & TxtDescription.Text & "' "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Godown Name Already Exists")
        Else
            mQry = "Select count(*) From Godown Where Description='" & TxtDescription.Text & "' And Code<>'" & mInternalCode & "' "
            If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then Err.Raise(1, , "Godown Name Already Exists")
        End If
    End Sub

    Private Sub FrmShade_BaseEvent_FindMain() Handles Me.BaseEvent_FindMain
        AgL.PubFindQry = " SELECT H.Code AS SearchCode, H.Description AS [Godown],  H.EntryBy AS [Entry By], H.EntryDate AS [Entry Date], H.EntryType AS [Entry Type], " &
                            " SM.Name AS [Site Name],D.Div_Name AS Division  " &
                            " FROM  Godown H  " &
                            " LEFT JOIN SiteMast SM ON SM.Code=H.Site_Code  " &
                            " LEFT JOIN Division D ON D.Div_Code =H.Div_Code  " &
                            " LEFT JOIN City C ON C.CityCode =H.City  " &
                            " WHERE  IfNull(H.IsDeleted,0)=0 "
        AgL.PubFindQryOrdBy = "[Godown]"
    End Sub

    Private Sub FrmShade_BaseEvent_Form_PreLoad() Handles Me.BaseEvent_Form_PreLoad
        MainTableName = "Godown"
        LogTableName = "Godown_LOG"
    End Sub

    Private Sub FrmShade_BaseEvent_Save_InTrans(ByVal SearchCode As String, ByVal Conn As SQLiteConnection, ByVal Cmd As SQLiteCommand) Handles Me.BaseEvent_Save_InTrans
        mQry = "Update Godown " &
                "   SET  " &
                "	Description = " & AgL.Chk_Text(TxtDescription.Text) & ", " &
                "	Site_Code = " & AgL.Chk_Text(TxtSite_Code.AgSelectedValue) & " " &
                "   Where Code = '" & SearchCode & "' "

        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
    End Sub

    Private Sub FrmQuality1_BaseFunction_DispText() Handles Me.BaseFunction_DispText
        TxtSite_Code.Enabled = False
    End Sub

    Public Overridable Sub Topctrl_tbAdd() Handles Topctrl1.tbAdd
        TxtSite_Code.AgSelectedValue = AgL.PubSiteCode
    End Sub

    Private Sub FrmQuality1_BaseFunction_FIniList() Handles Me.BaseFunction_FIniList
        mQry = "Select Code, Description As Name " &
            " From Godown  " &
            " Order By Description"
        TxtDescription.AgHelpDataSet() = AgL.FillData(mQry, AgL.GCn)

        mQry = "Select Code AS Code, ManualCode AS Name " &
                "  From SiteMast " &
                "  Order By ManualCode"
        TxtSite_Code.AgHelpDataSet() = AgL.FillData(mQry, AgL.GCn)
    End Sub

    Private Sub FrmShade_BaseFunction_FIniMast(ByVal BytDel As Byte, ByVal BytRefresh As Byte) Handles Me.BaseFunction_FIniMast
        mQry = "Select Code As SearchCode " &
            " From Godown " &
            " WHERE IfNull(IsDeleted,0)=0 Order By Description "

        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Private Sub FrmQuality1_BaseFunction_MoveRec(ByVal SearchCode As String) Handles Me.BaseFunction_MoveRec
        Dim DsTemp As DataSet

        mQry = "Select * " &
            " From Godown Where Code='" & SearchCode & "'"
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                mInternalCode = AgL.XNull(.Rows(0)("Code"))
                TxtDescription.Text = AgL.XNull(.Rows(0)("Description"))
                TxtSite_Code.AgSelectedValue = AgL.XNull(.Rows(0)("Site_Code"))
            End If
        End With
    End Sub

    Private Sub Topctrl1_tbAdd() Handles Topctrl1.tbAdd
        TxtDescription.Focus()
    End Sub

    Private Sub Topctrl1_tbEdit() Handles Topctrl1.tbEdit
        TxtDescription.Focus()
    End Sub

    Private Sub Topctrl1_tbPrn() Handles Topctrl1.tbPrn
    End Sub

    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub

    Private Sub FrmItemCategory_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AgL.WinSetting(Me, 384, 868)
    End Sub
End Class
