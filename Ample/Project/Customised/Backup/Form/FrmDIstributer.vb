Imports CrystalDecisions.CrystalReports.Engine
Public Class FrmDistributer
    Private DTMaster As New DataTable()
    Public BMBMaster As BindingManagerBase
    Private KEAMainKeyCode As System.Windows.Forms.KeyEventArgs
    Private DTStruct As New DataTable
    Dim mQry As String = "", mSearchCode As String = ""
    Dim mGroupNature As String = "", mNature As String = "", mMainTable$ = "", mLogTable$ = ""

    Public Sub New(ByVal StrUPVar As String, ByVal DTUP As DataTable)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Topctrl1.FSetParent(Me, StrUPVar, DTUP)
        Topctrl1.SetDisp(True)
    End Sub

    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, Topctrl1.Height)
    End Sub

    Private Sub Form_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        DTMaster = Nothing
    End Sub

    Private Sub IniGrid()
    End Sub

    Private Sub KeyDown_Form(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F2 Or e.KeyCode = Keys.F3 Or e.KeyCode = Keys.F4 Or e.KeyCode = (Keys.F And e.Control) Or e.KeyCode = (Keys.P And e.Control) _
        Or e.KeyCode = (Keys.S And e.Control) Or e.KeyCode = Keys.Escape Or e.KeyCode = Keys.F5 Or e.KeyCode = Keys.F10 _
        Or e.KeyCode = Keys.Home Or e.KeyCode = Keys.PageUp Or e.KeyCode = Keys.PageDown Or e.KeyCode = Keys.End Then
            Topctrl1.TopKey_Down(e)
        End If
        If Me.ActiveControl IsNot Nothing Then
            If Me.ActiveControl.Name <> Topctrl1.Name And _
                Not (TypeOf (Me.ActiveControl) Is AgControls.AgDataGrid) Then
                If e.KeyCode = Keys.Return Then SendKeys.Send("{Tab}")
            End If
        End If
    End Sub

    Sub KeyPress_Form(ByVal Sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        If e.KeyChar = Chr(Keys.Escape) Then Exit Sub
        If Me.ActiveControl Is Nothing Then Exit Sub
        AgL.CheckQuote(e)
    End Sub

    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            AgL.WinSetting(Me, 583, 880, 0, 0)
            IniGrid()
            FIniMaster()
            Ini_List()
            DispText()
            MoveRec()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FIniMaster(Optional ByVal BytDel As Byte = 0, Optional ByVal BytRefresh As Byte = 1)
        mQry = "Select I.SubCode As SearchCode " & _
                " From SubGroup I " & _
                " Where I.SubGroupType = '" & ClsMain.SubGroupType.Distributer & "'"
        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub

    Sub Ini_List()
        Try
            mQry = " Select Sg.SubCode As Code, Sg.ManualCode  " & _
                    " From SubGroup Sg " & _
                    " Where Sg.SubGroupType = '" & ClsMain.SubGroupType.Distributer & "'" & _
                    " Order By Sg.ManualCode "
            TxtManualCode.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)

            mQry = " Select Sg.SubCode As Code, Sg.ManualCode, Sg.DispName As Distributer " & _
                    " From SubGroup Sg " & _
                    " Where Sg.SubGroupType = '" & ClsMain.SubGroupType.Distributer & "'" & _
                    " Order By Sg.DispName "
            TxtParentDistributer.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)
            TxtDistributerGroup.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)

            mQry = " Select Sg.SubCode As Code, Sg.DispName As Distributer " & _
                    " From SubGroup Sg " & _
                    " Where Sg.SubGroupType = '" & ClsMain.SubGroupType.Distributer & "'" & _
                    " Order By Sg.DispName "
            TxtDistributerName.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)

            mQry = " Select C.CityCode As Code, C.CityName, C.State From City C "
            TxtCity.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)
            TxtOffCity.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)

            mQry = " Select G.GroupCode As Code, G.GroupName, G.GroupNature, G.Nature From AcGroup G "
            TxtAcGroup.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)

            mQry = " SELECT Description AS Code, Description, IsNull(Active,0) AS Active  FROM PostingGroupSalesTaxParty"
            TxtSalesTaxGroup.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Topctrl1_tbAdd() Handles Topctrl1.tbAdd
        BlankText()
        DispText(True)
        TxtManualCode.Focus()

        Try
            mQry = " Select IsNull(Max(Convert(BigInt,RIGHT(ManualCode,5))),0) As DistributerId From SubGroup Sg Where SubGroupType = '" & ClsMain.SubGroupType.Distributer & "'"
            TxtManualCode.Text = "512" & (AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar) + 1).ToString.PadLeft(5, "0")
        Catch ex As Exception
            TxtManualCode.Text = "51200001"
        End Try
        TxtDistributerName.Focus()
        TxtAcGroup.AgSelectedValue = "0020"
        Dim DrTemp As DataRow() = Nothing
        If TxtAcGroup.Text.ToString.Trim = "" Or TxtAcGroup.AgSelectedValue.Trim = "" Then
            mGroupNature = ""
            mNature = ""
        Else
            If TxtAcGroup.AgHelpDataSet IsNot Nothing Then
                DrTemp = TxtAcGroup.AgHelpDataSet.Tables(0).Select("Code = " & AgL.Chk_Text(TxtAcGroup.AgSelectedValue) & "")
                mGroupNature = AgL.XNull(DrTemp(0)("GroupNature"))
                mNature = AgL.XNull(DrTemp(0)("Nature"))
            End If
        End If
        TxtDateOfEnrollment.Text = AgL.PubLoginDate
    End Sub

    Private Sub Topctrl1_tbDel() Handles Topctrl1.tbDel
        Dim BlnTrans As Boolean = False
        Dim GCnCmd As New SqlClient.SqlCommand
        Dim MastPos As Long
        Dim mTrans As Boolean = False
        Dim DtTemp As DataTable
        Dim strMessage As String
        Dim I As Integer

        Try
            MastPos = BMBMaster.Position


            If String.Compare(mSearchCode.ToUpper, "COMPANY") = 0 Then
                MsgBox("Can't Delete this entry. It is system defined.")
                Exit Sub
            End If


            strMessage = ""
            DtTemp = AgL.FillData("Select Name From SubGroup Where ParentDistributor = '" & mSearchCode & "' ", AgL.GCn).tables(0)
            If DtTemp.Rows.Count > 0 Then
                strMessage = TxtDistributerName.Text + " is parent of following distributors "
                For I = 0 To DtTemp.Rows.Count - 1
                    strMessage += vbCrLf + DtTemp.Rows(I)("Name")
                Next
                strMessage += vbCrLf + "It can not be deleted."
            End If
            If strMessage <> "" Then
                MsgBox(strMessage)
                Exit Sub
            End If


            If AgL.PubMoveRecApplicable And BMBMaster.Position >= 0 Then
                If MsgBox("Are You Sure To Delete This Record?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, AgLibrary.ClsMain.PubMsgTitleInfo) = vbYes Then
                    AgL.ECmd = AgL.GCn.CreateCommand
                    AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
                    AgL.ECmd.Transaction = AgL.ETrans
                    mTrans = True

                    AgL.Dman_ExecuteNonQry("Delete From SubGroup Where SubCode ='" & mSearchCode & "'", AgL.GCn, AgL.ECmd)

                    Call AgL.LogTableEntry(mSearchCode, Me.Text, "D", AgL.PubMachineName, AgL.PubUserName, AgL.PubLoginDate, AgL.GCn, AgL.ECmd)

                    AgL.SynchroniseSiteOnLineData(AgL, AgL.GCn, AgL.Gcn_ConnectionString, AgL.GcnSite_ConnectionString, AgL.ECmd)
                    AgL.ETrans.Commit()
                    mTrans = False

                    FIniMaster(1)
                    Topctrl1_tbRef()
                    MoveRec()
                End If
            End If
        Catch Ex As Exception
            If mTrans = True Then AgL.ETrans.Rollback()
            MsgBox(Ex.Message, MsgBoxStyle.Information, AgLibrary.ClsMain.PubMsgTitleInfo)
        End Try
    End Sub

    Private Sub Topctrl1_tbDiscard() Handles Topctrl1.tbDiscard
        FIniMaster(0, 0)
        Topctrl1.Focus()
    End Sub

    Private Sub Topctrl1_tbEdit() Handles Topctrl1.tbEdit


        DispText(True)
        TxtManualCode.Focus()
    End Sub

    Private Sub Topctrl1_tbFind() Handles Topctrl1.tbFind
        'If DTMaster.Rows.Count <= 0 Then MsgBox("No Records To Search.", vbInformation, AgLibrary.ClsMain.PubMsgTitleInfo) : Exit Sub
        Try
            AgL.PubFindQry = " Select Sg.SubCode As SearchCode, Sg.ManualCode, Sg.DispName, Sg.Area, Sg.DistributerLevel, Sg.OfficeAddress,  " & _
                        " Sg.DateOfEnrollment, Sg.ValidTillDate, Sg.DistributerGroup, Sg.CoDistributer " & _
                        " From SubGroup Sg " & _
                        " Where Sg.SubGroupType = '" & ClsMain.SubGroupType.Distributer & "'"
            AgL.PubFindQryOrdBy = "[DispName]"


            '*************** common code start *****************
            Dim Frmbj As AgTemplate.FrmReportWindow = New AgTemplate.FrmReportWindow(AgL.PubFindQry, Me.Text & " Find")
            Frmbj.ShowDialog()
            AgL.PubSearchRow = Frmbj.DGL1.Item(0, Frmbj.DGL1.CurrentRow.Index).Value.ToString
            If AgL.PubSearchRow <> "" Then
                AgL.PubDRFound = DTMaster.Rows.Find(AgL.PubSearchRow)
                BMBMaster.Position = DTMaster.Rows.IndexOf(AgL.PubDRFound)
                MoveRec()
            End If
            '*************** common code end  *****************
        Catch Ex As Exception
            MsgBox(Ex.Message)
        End Try
    End Sub

    Private Sub Topctrl1_tbRef() Handles Topctrl1.tbRef
        Ini_List()
    End Sub

    Private Sub Topctrl1_tbPrn() Handles Topctrl1.tbPrn
    End Sub

    Private Sub Topctrl1_tbSave() Handles Topctrl1.tbSave
        Dim MastPos As Long
        Dim mTrans As Boolean = False
        Dim bName$ = ""
        Try
            MastPos = BMBMaster.Position

            If AgL.RequiredField(TxtManualCode, LblDistributerId.Text) Then Exit Sub
            If AgL.RequiredField(TxtDistributerName, LblDistributerName.Text) Then Exit Sub
            bName = TxtDistributerName.Text + " {" + TxtManualCode.Text + "}"


            If Topctrl1.Mode = "Add" Then
                AgL.ECmd = AgL.Dman_Execute("Select count(*) From SubGroup Where ManualCode ='" & TxtManualCode.Text & "' ", AgL.GCn)
                If AgL.ECmd.ExecuteScalar() > 0 Then MsgBox("Manual Code Already Exist!") : TxtManualCode.Focus() : Exit Sub

                AgL.ECmd = AgL.Dman_Execute("Select count(*) From SubGroup Where Name ='" & bName & "' ", AgL.GCn)
                If AgL.ECmd.ExecuteScalar() > 0 Then MsgBox("Distributer Name Already Exist!") : TxtManualCode.Focus() : Exit Sub

                mSearchCode = AgL.GetMaxId("SubGroup", "SubCode", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 4, True, True, , AgL.Gcn_ConnectionString)
            Else
                AgL.ECmd = AgL.Dman_Execute("Select count(*) From SubGroup Where ManualCode ='" & TxtManualCode.Text & "' And SubCode <> '" & mSearchCode & "' ", AgL.GCn)
                If AgL.ECmd.ExecuteScalar() > 0 Then MsgBox("Manual Code Already Exist!") : TxtManualCode.Focus() : Exit Sub

                AgL.ECmd = AgL.Dman_Execute("Select count(*) From SubGroup Where Name ='" & bName & "' And SubCode <> '" & mSearchCode & "' ", AgL.GCn)
                If AgL.ECmd.ExecuteScalar() > 0 Then MsgBox("Distributer Name Already Exist!") : TxtManualCode.Focus() : Exit Sub
            End If

            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = True


            If Topctrl1.Mode = "Add" Then
                mQry = "INSERT INTO SubGroup(SubCode, Site_Code, Name, DispName, " & _
                        " GroupCode, GroupNature,	ManualCode,	Nature,	Add1,	CityCode, " & _
                        " Phone, FAX,	EMail, " & _
                        " EntryBy, EntryDate,  EntryType, Div_Code, Status, " & _
                        " U_Name, U_EntDt, U_AE, SubGroupType, Area, DistributerLevel, OfficeAddress, OfficeCity, " & _
                        " DateOfEnrollment, ValidTillDate, DistributerGroup, ParentDistributer, CoDistributer, CoDistributerDOB, DOB, Remark, " & _
                        " BankName, BankAcNo, PAN, SalesTaxPostingGroup) " & _
                        " VALUES(" & AgL.Chk_Text(mSearchCode) & ", " & _
                        " '" & AgL.PubSiteCode & "', " & AgL.Chk_Text(bName) & ",	" & _
                        " " & AgL.Chk_Text(TxtDistributerName.Text) & ", " & AgL.Chk_Text(TxtAcGroup.AgSelectedValue) & ", " & _
                        " " & AgL.Chk_Text(mGroupNature) & ", " & AgL.Chk_Text(TxtManualCode.Text) & ", " & _
                        " " & AgL.Chk_Text(mNature) & ", " & AgL.Chk_Text(TxtAddress.Text) & ", " & _
                        " " & AgL.Chk_Text(TxtCity.AgSelectedValue) & ", " & _
                        " " & AgL.Chk_Text(TxtPhone.Text) & ", " & _
                        " " & AgL.Chk_Text(TxtFax.Text) & ", " & AgL.Chk_Text(TxtEMail.Text) & ", " & _
                        " " & AgL.Chk_Text(AgL.PubUserName) & ", " & AgL.Chk_Text(AgL.GetDateTime(AgL.GcnRead)) & ", " & _
                        " " & AgL.Chk_Text(Topctrl1.Mode) & ", " & _
                        " " & AgL.Chk_Text(AgL.PubDivCode) & ", " & AgL.Chk_Text(AgTemplate.ClsMain.EntryStatus.Active) & ", " & _
                        " '" & AgL.PubUserName & "','" & Format(AgL.PubLoginDate, "Short Date") & "', 'A', " & _
                        " " & AgL.Chk_Text(ClsMain.SubGroupType.Distributer) & ", " & _
                        " " & AgL.Chk_Text(TxtArea.Text) & ", " & _
                        " " & Val(TxtLevel.Text) & ", " & _
                        " " & AgL.Chk_Text(TxtOfficeAdd.Text) & ", " & _
                        " " & AgL.Chk_Text(TxtOffCity.AgSelectedValue) & ", " & _
                        " " & AgL.Chk_Text(TxtDateOfEnrollment.Text) & ", " & _
                        " " & AgL.Chk_Text(TxtValidTillDate.Text) & ", " & _
                        " " & AgL.Chk_Text(TxtDistributerGroup.AgSelectedValue) & ", " & _
                        " " & AgL.Chk_Text(TxtParentDistributer.AgSelectedValue) & ", " & _
                        " " & AgL.Chk_Text(TxtCODistributer.Text) & ", " & _
                        " " & AgL.Chk_Text(TxtCoDistributerDOB.Text) & ", " & _
                        " " & AgL.Chk_Text(TxtDistributerDOB.Text) & ", " & _
                        " " & AgL.Chk_Text(TxtRemark.Text) & ", " & _
                        " " & AgL.Chk_Text(TxtBankName.Text) & ", " & _
                        " " & AgL.Chk_Text(TxtBankAcNo.Text) & ", " & _
                        " " & AgL.Chk_Text(TxtPanNo.Text) & ", " & _
                        " " & AgL.Chk_Text(TxtSalesTaxGroup.Text) & " " & _
                        " ) "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            Else
                mQry = "UPDATE SubGroup " & _
                        " SET " & _
                        " Name = " & AgL.Chk_Text(bName) & ", " & _
                        " DispName = " & AgL.Chk_Text(TxtDistributerName.Text) & ", " & _
                        " GroupCode = " & AgL.Chk_Text(TxtAcGroup.AgSelectedValue) & ", " & _
                        " GroupNature = " & AgL.Chk_Text(mGroupNature) & ", " & _
                        " ManualCode = " & AgL.Chk_Text(TxtManualCode.Text) & ", " & _
                        " Nature = " & AgL.Chk_Text(mNature) & ", " & _
                        " Add1 = " & AgL.Chk_Text(TxtAddress.Text) & ", " & _
                        " CityCode = " & AgL.Chk_Text(TxtCity.AgSelectedValue) & ", " & _
                        " Phone = " & AgL.Chk_Text(TxtPhone.Text) & ", " & _
                        " FAX = " & AgL.Chk_Text(TxtFax.Text) & ", " & _
                        " EMail = " & AgL.Chk_Text(TxtEMail.Text) & ", " & _
                        " EntryBy = " & AgL.Chk_Text(AgL.PubUserName) & ", " & _
                        " EntryDate = " & AgL.Chk_Text(AgL.GetDateTime(AgL.GcnRead)) & ", " & _
                        " EntryType = " & AgL.Chk_Text(Topctrl1.Mode) & ", " & _
                        " Div_Code = " & AgL.Chk_Text(AgL.PubDivCode) & ", " & _
                        " U_AE = 'E', " & _
                        " Edit_Date = '" & Format(AgL.PubLoginDate, "Short Date") & "', " & _
                        " ModifiedBy = '" & AgL.PubUserName & "', " & _
                        " SubGroupType = " & AgL.Chk_Text(ClsMain.SubGroupType.Distributer) & ", " & _
                        " Area = " & AgL.Chk_Text(TxtArea.Text) & ", " & _
                        " DistributerLevel = " & Val(TxtLevel.Text) & ", " & _
                        " OfficeAddress = " & AgL.Chk_Text(TxtOfficeAdd.Text) & ", " & _
                        " OfficeCity = " & AgL.Chk_Text(TxtOffCity.AgSelectedValue) & ", " & _
                        " DateOfEnrollment = " & AgL.Chk_Text(TxtDateOfEnrollment.Text) & ", " & _
                        " ValidTillDate = " & AgL.Chk_Text(TxtValidTillDate.Text) & ", " & _
                        " DistributerGroup= " & AgL.Chk_Text(TxtDistributerGroup.AgSelectedValue) & ", " & _
                        " ParentDistributer = " & AgL.Chk_Text(TxtParentDistributer.AgSelectedValue) & ", " & _
                        " DOB = " & AgL.Chk_Text(TxtDistributerDOB.Text) & ", " & _
                        " CoDistributer = " & AgL.Chk_Text(TxtCODistributer.Text) & ", " & _
                        " CoDistributerDOB = " & AgL.Chk_Text(TxtCoDistributerDOB.Text) & ", " & _
                        " Remark = " & AgL.Chk_Text(TxtRemark.Text) & ", " & _
                        " PAN = " & AgL.Chk_Text(TxtPanNo.Text) & ", " & _
                        " BankName = " & AgL.Chk_Text(TxtBankName.Text) & ", " & _
                        " BankAcNo = " & AgL.Chk_Text(TxtBankAcNo.Text) & ", " & _
                        " SalesTaxPostingGroup = " & AgL.Chk_Text(TxtSalesTaxGroup.AgSelectedValue) & " " & _
                        " Where SubCode = " & AgL.Chk_Text(mSearchCode) & "  "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If

            Call AgL.LogTableEntry(mSearchCode, Me.Text, AgL.MidStr(Topctrl1.Mode, 0, 1), AgL.PubMachineName, AgL.PubUserName, AgL.PubLoginDate, AgL.GCn, AgL.ECmd)

            AgL.SynchroniseSiteOnLineData(AgL, AgL.GCn, AgL.Gcn_ConnectionString, AgL.GcnSite_ConnectionString, AgL.ECmd)
            AgL.ETrans.Commit()
            mTrans = False
            FIniMaster(0, 1)
            Topctrl1_tbRef()
            If Topctrl1.Mode = "Add" Then
                Topctrl1.LblDocId.Text = mSearchCode
                Topctrl1.FButtonClick(0)
                Exit Sub
            Else
                Topctrl1.SetDisp(True)
                MoveRec()
            End If
        Catch ex As Exception
            If mTrans = True Then AgL.ETrans.Rollback()
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub MoveRec()
        Dim DsTemp As DataSet = Nothing
        Dim MastPos As Long
        Try
            FClear()
            BlankText()

            If AgL.PubMoveRecApplicable Then
                If BMBMaster.Position < 0 Then Exit Sub
                MastPos = BMBMaster.Position
                mSearchCode = DTMaster.Rows(MastPos)("SearchCode")
            Else
                If AgL.PubSearchRow <> "" Then mSearchCode = AgL.PubSearchRow
            End If

            If mSearchCode.Trim <> "" Then
                mQry = "Select Sg.*, C.State, C1.State As OffState " & _
                        " From SubGroup Sg " & _
                        " LEFT JOIN City C On Sg.CityCode = C.CityCode " & _
                        " LEFT JOIN City C1 On Sg.OfficeCity = C1.CityCode " & _
                        " Where Sg.SubCode = '" & mSearchCode & "'"
                DsTemp = AgL.FillData(mQry, AgL.GCn)
                With DsTemp.Tables(0)
                    If .Rows.Count > 0 Then
                        TxtManualCode.Text = AgL.XNull(.Rows(0)("ManualCode"))
                        TxtDistributerName.Text = AgL.XNull(.Rows(0)("DispName"))
                        TxtAcGroup.AgSelectedValue = AgL.XNull(.Rows(0)("GroupCode"))
                        TxtAddress.Text = AgL.XNull(.Rows(0)("Add1"))
                        TxtCity.AgSelectedValue = AgL.XNull(.Rows(0)("CityCode"))
                        TxtPhone.Text = AgL.XNull(.Rows(0)("Phone"))
                        TxtFax.Text = AgL.XNull(.Rows(0)("Fax"))
                        TxtEMail.Text = AgL.XNull(.Rows(0)("EMail"))
                        TxtArea.Text = AgL.XNull(.Rows(0)("Area"))
                        TxtLevel.Text = AgL.VNull(.Rows(0)("DistributerLevel"))
                        TxtOfficeAdd.Text = AgL.XNull(.Rows(0)("OfficeAddress"))
                        TxtOffCity.AgSelectedValue = AgL.XNull(.Rows(0)("OfficeCity"))
                        TxtDateOfEnrollment.Text = AgL.XNull(.Rows(0)("DateOfEnrollment"))
                        TxtValidTillDate.Text = AgL.XNull(.Rows(0)("ValidTillDate"))
                        TxtDistributerGroup.AgSelectedValue = AgL.XNull(.Rows(0)("DistributerGroup"))
                        TxtParentDistributer.AgSelectedValue = AgL.XNull(.Rows(0)("ParentDistributer"))
                        TxtCODistributer.Text = AgL.XNull(.Rows(0)("CoDistributer"))
                        TxtDistributerDOB.Text = AgL.XNull(.Rows(0)("DOB"))
                        TxtState.Text = AgL.XNull(.Rows(0)("State"))
                        TxtOffState.Text = AgL.XNull(.Rows(0)("OffState"))
                        TxtCoDistributerDOB.Text = AgL.XNull(.Rows(0)("CoDistributerDOB"))
                        TxtRemark.Text = AgL.XNull(.Rows(0)("Remark"))
                        TxtPanNo.Text = AgL.XNull(.Rows(0)("PAN"))
                        TxtBankName.Text = AgL.XNull(.Rows(0)("BankName"))
                        TxtBankAcNo.Text = AgL.XNull(.Rows(0)("BankAcNo"))
                        TxtSalesTaxGroup.AgSelectedValue = AgL.XNull(.Rows(0)("SalesTaxPostingGroup"))

                        Dim DrTemp As DataRow() = Nothing
                        If TxtParentDistributer.Text.ToString.Trim = "" Or TxtParentDistributer.AgSelectedValue.Trim = "" Then
                            TxtParentDistributerName.Text = ""
                        Else
                            If TxtParentDistributer.AgHelpDataSet IsNot Nothing Then
                                DrTemp = TxtParentDistributer.AgHelpDataSet.Tables(0).Select("Code = " & AgL.Chk_Text(TxtParentDistributer.AgSelectedValue) & "")
                                TxtParentDistributerName.Text = AgL.XNull(DrTemp(0)("Distributer"))
                            End If
                        End If

                        If TxtDistributerGroup.Text.ToString.Trim = "" Or TxtDistributerGroup.AgSelectedValue.Trim = "" Then
                            TxtDistributerGroupName.Text = ""
                        Else
                            If TxtDistributerGroup.AgHelpDataSet IsNot Nothing Then
                                DrTemp = TxtDistributerGroup.AgHelpDataSet.Tables(0).Select("Code = " & AgL.Chk_Text(TxtDistributerGroup.AgSelectedValue) & "")
                                TxtDistributerGroupName.Text = AgL.XNull(DrTemp(0)("Distributer"))
                            End If
                        End If
                    End If
                End With
                DsTemp = Nothing
            Else
                BlankText()
            End If
            If AgL.PubMoveRecApplicable Then Topctrl1.FSetDispRec(BMBMaster)
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            DsTemp = Nothing
        End Try
    End Sub

    Private Sub BlankText()
        If Topctrl1.Mode <> "Add" Then Topctrl1.BlankTextBoxes(Me)
        mSearchCode = ""
    End Sub

    Private Sub DispText(Optional ByVal Enb As Boolean = False)
        'Coding To Enable/Disable Controls
        TxtState.Enabled = False
        TxtOffState.Enabled = False
        TxtParentDistributerName.Enabled = False
        TxtDistributerGroupName.Enabled = False
    End Sub

    Private Sub FClear()
        DTStruct.Clear()
    End Sub

    Private Sub FAddRowStructure()
        Dim DRStruct As DataRow
        Try
            DRStruct = DTStruct.NewRow
            DTStruct.Rows.Add(DRStruct)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Control_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtCity.Validating, TxtAcGroup.Validating, TxtOffCity.Validating, TxtParentDistributer.Validating, TxtDistributerGroup.Validating
        Dim DtTemp As DataTable = Nothing
        Dim DrTemp As DataRow() = Nothing
        Try
            Select Case sender.NAME
                Case TxtCity.Name
                    If sender.text.ToString.Trim = "" Or sender.AgSelectedValue.Trim = "" Then
                        TxtState.Text = ""
                    Else
                        If sender.AgHelpDataSet IsNot Nothing Then
                            DrTemp = sender.AgHelpDataSet.Tables(0).Select("Code = " & AgL.Chk_Text(sender.AgSelectedValue) & "")
                            TxtState.Text = AgL.XNull(DrTemp(0)("State"))
                        End If
                    End If

                Case TxtOffCity.Name
                    If sender.text.ToString.Trim = "" Or sender.AgSelectedValue.Trim = "" Then
                        TxtOffState.Text = ""
                    Else
                        If sender.AgHelpDataSet IsNot Nothing Then
                            DrTemp = sender.AgHelpDataSet.Tables(0).Select("Code = " & AgL.Chk_Text(sender.AgSelectedValue) & "")
                            TxtOffState.Text = AgL.XNull(DrTemp(0)("State"))
                        End If
                    End If


                Case TxtAcGroup.Name
                    If sender.text.ToString.Trim = "" Or sender.AgSelectedValue.Trim = "" Then
                        mGroupNature = ""
                        mNature = ""
                    Else
                        If sender.AgHelpDataSet IsNot Nothing Then
                            DrTemp = TxtAcGroup.AgHelpDataSet.Tables(0).Select("Code = " & AgL.Chk_Text(TxtAcGroup.AgSelectedValue) & "")
                            mGroupNature = AgL.XNull(DrTemp(0)("GroupNature"))
                            mNature = AgL.XNull(DrTemp(0)("Nature"))
                        End If
                    End If

                Case TxtParentDistributer.Name
                    If sender.text.ToString.Trim = "" Or sender.AgSelectedValue.Trim = "" Then
                        TxtParentDistributerName.Text = ""
                    Else
                        If sender.AgHelpDataSet IsNot Nothing Then
                            DrTemp = TxtParentDistributer.AgHelpDataSet.Tables(0).Select("Code = " & AgL.Chk_Text(TxtParentDistributer.AgSelectedValue) & "")
                            TxtParentDistributerName.Text = AgL.XNull(DrTemp(0)("Distributer"))
                            If TxtDistributerGroup.AgSelectedValue = "" Then TxtDistributerGroup.AgSelectedValue = TxtParentDistributer.AgSelectedValue
                            If TxtDistributerGroupName.AgSelectedValue = "" Then TxtDistributerGroupName.Text = TxtParentDistributerName.Text
                        End If
                    End If

                Case TxtDistributerGroup.Name
                    If sender.text.ToString.Trim = "" Or sender.AgSelectedValue.Trim = "" Then
                        TxtDistributerGroupName.Text = ""
                    Else
                        If sender.AgHelpDataSet IsNot Nothing Then
                            DrTemp = TxtDistributerGroup.AgHelpDataSet.Tables(0).Select("Code = " & AgL.Chk_Text(TxtDistributerGroup.AgSelectedValue) & "")
                            TxtDistributerGroupName.Text = AgL.XNull(DrTemp(0)("Distributer"))
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class
