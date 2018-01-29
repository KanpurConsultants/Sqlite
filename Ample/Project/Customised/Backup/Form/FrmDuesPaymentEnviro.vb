Imports CrystalDecisions.CrystalReports.Engine
Public Class FrmDuesPaymentEnviro

    Private DTMaster As New DataTable()
    Public BMBMaster As BindingManagerBase
    Private KEAMainKeyCode As System.Windows.Forms.KeyEventArgs
    Private DTStruct As New DataTable


    Dim mQry As String = "", mSearchCode As String = ""
    Dim mNCat As String = ""

    Public Sub New(ByVal StrUPVar As String, ByVal DTUP As DataTable)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        Topctrl1.FSetParent(Me, StrUPVar, DTUP)
        Topctrl1.SetDisp(True)
    End Sub

    Public Property EntryNCat() As String
        Get
            Return Replace(Replace(mNCAT, " ", ""), ",", "','")
        End Get
        Set(ByVal value As String)
            mNCAT = value
        End Set
    End Property

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
            AgL.WinSetting(Me, 300, 880, 0, 0)
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
        mQry = "Select H.V_Type As SearchCode " & _
        " From DuesPaymentEnviro H Left Join Voucher_Type V On H.V_Type = V.V_Type " & _
        " Where V.NCat in (" & EntryNCat & ")"
        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)

    End Sub


    Sub Ini_List()
        mQry = "Select V_Type, Description from Voucher_Type V where NCat In (" & EntryNCat & ")"
        TxtDescription.AgHelpDataSet = AgL.FillData(mQry, AgL.GcnRead)

        mQry = "Select Subcode, DispName as Name From Subgroup H Where H.Nature ='Cash' "
        TxtCashAc.AgHelpDataSet = AgL.FillData(mQry, AgL.GcnRead)

        mQry = "Select Subcode, DispName as Name From Subgroup H Where H.Nature ='Bank' "
        TxtBankAc.AgHelpDataSet = AgL.FillData(mQry, AgL.GcnRead)

        mQry = "Select Subcode, DispName as Name From Subgroup H Where H.Nature Not In ('Customer', 'Supplier', 'Cash', 'Bank') "
        TxtDebitNoteAc.AgHelpDataSet = AgL.FillData(mQry, AgL.GcnRead)
        TxtCreditNoteAc.AgHelpDataSet = AgL.FillData(mQry, AgL.GcnRead)

        mQry = "Select Subcode, DispName as Name From Subgroup H Where H.Nature ='Others' "
        TxtDiscountAc.AgHelpDataSet = AgL.FillData(mQry, AgL.GcnRead)
    End Sub

    Private Sub Topctrl1_tbAdd() Handles Topctrl1.tbAdd
        BlankText()
        DispText()
        TxtDescription.Focus()
    End Sub

    Private Sub Topctrl1_tbDel() Handles Topctrl1.tbDel
        Dim BlnTrans As Boolean = False
        Dim GCnCmd As New SqlClient.SqlCommand
        Dim MastPos As Long
        Dim mTrans As Boolean = False


        Try
            MastPos = BMBMaster.Position


            If DTMaster.Rows.Count > 0 Then
                If MsgBox("Are You Sure To Delete This Record?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, AgLibrary.ClsMain.PubMsgTitleInfo) = vbYes Then


                    AgL.ECmd = AgL.GCn.CreateCommand
                    AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
                    AgL.ECmd.Transaction = AgL.ETrans
                    mTrans = True


                    AgL.Dman_ExecuteNonQry("Delete From DuesPaymentEnviro Where V_Type='" & mSearchCode & "'", AgL.GCn, AgL.ECmd)

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
        DispText()
        TxtDescription.Focus()
    End Sub


    Private Sub Topctrl1_tbFind() Handles Topctrl1.tbFind
        If DTMaster.Rows.Count <= 0 Then MsgBox("No Records To Search.", vbInformation, AgLibrary.ClsMain.PubMsgTitleInfo) : Exit Sub
        Try

            AgL.PubFindQry = "Select  H.V_Type As SearchCode,  V.Description  " & _
                             "From  DuesPaymentEnviro H  " & _
                             "Left Join Voucher_Type V On H.V_Type = V.V_Type "

            AgL.PubFindQryOrdBy = "[Description]"

            '*************** common code start *****************
            AgL.PubObjFrmFind = New AgLibrary.frmFind(AgL)
            AgL.PubObjFrmFind.ShowDialog()
            AgL.PubObjFrmFind = Nothing
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

    Private Sub Topctrl1_tbSave() Handles Topctrl1.tbSave
        Dim MastPos As Long
        Dim mTrans As Boolean = False
        Try
            MastPos = BMBMaster.Position

            If AgCL.AgCheckMandatory(Me) = False Then Exit Sub


            If Topctrl1.Mode = "Add" Then
                AgL.ECmd = AgL.Dman_Execute("Select count(*) From DuesPaymentEnviro Where V_Type='" & TxtDescription.Tag & "' ", AgL.GCn)
                If AgL.ECmd.ExecuteScalar() > 0 Then MsgBox("Voucher Type Already Exist!") : TxtDescription.Focus() : Exit Sub

                mSearchCode = TxtDescription.Tag  'AgL.GetMaxId("DuesPaymentEnviro", "Code", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 4, True, True, , AgL.Gcn_ConnectionString)
            Else
                AgL.ECmd = AgL.Dman_Execute("Select count(*) From DuesPaymentEnviro Where V_Type='" & TxtDescription.Tag & "' And V_Type<>'" & mSearchCode & "' ", AgL.GCn)
                If AgL.ECmd.ExecuteScalar() > 0 Then MsgBox("Voucher Type Already Exist!") : TxtDescription.Focus() : Exit Sub
            End If


            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = True


            If Topctrl1.Mode = "Add" Then
                mQry = "Insert Into DuesPaymentEnviro (V_Type, CashAc, BankAc, DebitNoteAc, CreditNoteAc, DiscountAc, PrintOnAddSave, PrintOnEditSave, Remark) " & _
                        " Values('" & mSearchCode & "', " & AgL.Chk_Text(TxtCashAc.AgSelectedValue) & ", " & AgL.Chk_Text(TxtBankAc.AgSelectedValue) & ", " & AgL.Chk_Text(TxtDebitNoteAc.AgSelectedValue) & ", " & AgL.Chk_Text(TxtCreditNoteAc.AgSelectedValue) & ", " & _
                        " " & AgL.Chk_Text(TxtDiscountAc.AgSelectedValue) & ", " & _
                        " " & IIf(AgL.StrCmp(TxtPrintOnAddSave.Text, "Yes"), 1, 0) & ", " & _
                        " " & IIf(AgL.StrCmp(TxtPrintOnEditSave.Text, "Yes"), 1, 0) & ", " & _
                        " " & AgL.Chk_Text(TxtRemark.Text) & "  ) "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            Else
                mQry = "Update DuesPaymentEnviro Set  CashAc = " & AgL.Chk_Text(TxtCashAc.AgSelectedValue) & ",  BankAc = " & AgL.Chk_Text(TxtBankAc.AgSelectedValue) & ",  DebitNoteAc = " & AgL.Chk_Text(TxtDebitNoteAc.AgSelectedValue) & ",  CreditNoteAc = " & AgL.Chk_Text(TxtCreditNoteAc.AgSelectedValue) & ", " & _
                        " DiscountAc = " & AgL.Chk_Text(TxtDiscountAc.AgSelectedValue) & ", " & _
                        " PrintOnAddSave = " & IIf(AgL.StrCmp(TxtPrintOnAddSave.Text, "Yes"), 1, 0) & ", " & _
                        " PrintOnEditSave = " & IIf(AgL.StrCmp(TxtPrintOnEditSave.Text, "Yes"), 1, 0) & ", " & _
                        " Remark = " & AgL.Chk_Text(TxtRemark.Text) & " " & _
                        " Where V_Type='" & mSearchCode & "' "
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
            If DTMaster.Rows.Count > 0 Then
                MastPos = BMBMaster.Position
                mSearchCode = DTMaster.Rows(MastPos)("SearchCode")
                mQry = "Select DuesPaymentEnviro.* " & _
                    " From DuesPaymentEnviro Where V_Type='" & mSearchCode & "'"
                DsTemp = AgL.FillData(mQry, AgL.GCn)
                With DsTemp.Tables(0)
                    If .Rows.Count > 0 Then
                        TxtDescription.AgSelectedValue = AgL.XNull(.Rows(0)("V_Type"))
                        TxtCashAc.AgSelectedValue = AgL.XNull(.Rows(0)("CashAc"))
                        TxtBankAc.AgSelectedValue = AgL.XNull(.Rows(0)("BankAc"))
                        TxtDebitNoteAc.AgSelectedValue = AgL.XNull(.Rows(0)("DebitNoteAc"))
                        TxtCreditNoteAc.AgSelectedValue = AgL.XNull(.Rows(0)("CreditNoteAc"))
                        TxtDiscountAc.AgSelectedValue = AgL.XNull(.Rows(0)("DiscountAc"))
                        TxtPrintOnAddSave.Text = IIf(AgL.VNull(.Rows(0)("PrintOnAddSave")) = 0, "No", "Yes")
                        TxtPrintOnEditSave.Text = IIf(AgL.VNull(.Rows(0)("PrintOnEditSave")) = 0, "No", "Yes")
                        TxtRemark.Text = AgL.XNull(.Rows(0)("Remark"))
                    End If
                End With
            Else
                BlankText()
            End If
            Topctrl1.FSetDispRec(BMBMaster)
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            DsTemp = Nothing
        End Try
    End Sub

    Private Sub BlankText()
        If Topctrl1.Mode <> "Add" Then Topctrl1.BlankTextBoxes()
        mSearchCode = ""

    End Sub

    Private Sub DispText(Optional ByVal Enb As Boolean = False)
        'Coding To Enable/Disable Controls
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

End Class
