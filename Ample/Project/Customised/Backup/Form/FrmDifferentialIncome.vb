Imports CrystalDecisions.CrystalReports.Engine
Public Class FrmDifferentialIncome
    Private DTMaster As New DataTable()
    Public BMBMaster As BindingManagerBase
    Private KEAMainKeyCode As System.Windows.Forms.KeyEventArgs
    Private DTStruct As New DataTable

    Dim mQry As String = "", mSearchCode As String = ""
    Dim mV_Type As String = ClsMain.Temp_NCat.DifferentialIncome, mV_Prefix As String = ""
    Dim Col1Policy_No_Qry As String = "", Col1InstallmentNo_Qry As String = "", Col1InstallmentDate_Qry As String = "", Col1OC_Main_Qry As String = "", Col1OC_Qry As String = ""

    Private Const Col_SNo As Byte = 0
    Public WithEvents DGL1 As New AgControls.AgDataGrid


    Private Const Col1Distributor As String = "Distributor"
    Private Const Col1DistributorPer As String = "Distributor Per"
    Private Const Col1DownlineDistributor As String = "Downline Distributor"
    Private Const Col1DownlineDistributorPer As String = "Downline Distributor Per"
    Private Const Col1DownlineDistributorPV As String = "Downline Distributor PV"
    Private Const Col1DownlineDistributorBV As String = "Downline Distributor BV"
    Private Const Col1DifferentialIncomePV As String = "Differential Income PV"
    Private Const Col1DifferentialIncomeBV As String = "Differential Income BV"
    Private Const Col1DifferentialIncome As String = "Differential Income"

    Public WithEvents DGL2 As New AgControls.AgDataGrid
    Private Const Col2Distributor As String = "Distributor"
    Private Const Col2DownlineSuphire As String = "Downline Suphire"
    Private Const Col2DownlineSuphirePV As String = "Downline Suphire PV"
    Private Const Col2DownlineSuphireBV As String = "Downline Suphire BV"
    Private Const Col2DownlineSuphirePer As String = "Downline Suphire %"
    Private Const Col2DownlineDistributorTargetPV As String = "Downline Distributor Target PV"
    Private Const Col2DownlineDistributorTargetBV As String = "Downline Distributor Target BV"
    Private Const Col2DownlineDistributorPV As String = "Downline Distributor PV"
    Private Const Col2DownlineDistributorBV As String = "Downline Distributor BV"
    Private Const Col2DownlineDistributorPer As String = "Downline Distributor Per"
    Private Const Col2SuphireBonusIncomePV As String = "Suphire Bonus Income PV"
    Private Const Col2SuphireBonusIncomeBV As String = "Suphire Bonus Income BV"
    Private Const Col2SuphireBonusIncome As String = "Suphire Bonus Income"



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
        'Dgl1.Rows.Clear()
        'Dgl1.Columns.Clear()

        Dgl1.Height = Pnl_DifferentialIncome.Height
        Dgl1.Width = Pnl_DifferentialIncome.Width
        DGL1.Top = TabControl1.Top + TP_Differential.Top + Pnl_DifferentialIncome.Top
        DGL1.Left = TabControl1.Left + TP_Differential.Left + Pnl_DifferentialIncome.Left
        Dgl1.ColumnHeadersHeight = 40
        Pnl_DifferentialIncome.Visible = False
        Controls.Add(Dgl1)
        Dgl1.Visible = True
        Dgl1.BringToFront()

        With AgCL
            .AddAgTextColumn(DGL1, "Sr", 40, 5, "Sr", True, True, False)
            .AddAgTextColumn(DGL1, Col1Distributor, 180, 0, Col1Distributor, True, True, False)
            .AddAgNumberColumn(DGL1, Col1DistributorPer, 60, 8, 2, True, Col1DistributorPer, True, True, False)
            .AddAgTextColumn(DGL1, Col1DownlineDistributor, 180, 0, Col1DownlineDistributor, True, True, False)
            .AddAgNumberColumn(DGL1, Col1DownlineDistributorPer, 60, 8, 2, True, Col1DownlineDistributorPer, True, True, False)
            .AddAgNumberColumn(DGL1, Col1DownlineDistributorPV, 60, 8, 2, True, Col1DownlineDistributorPV, True, True, False)
            .AddAgNumberColumn(DGL1, Col1DownlineDistributorBV, 60, 8, 2, True, Col1DownlineDistributorBV, True, True, False)
            .AddAgNumberColumn(DGL1, Col1DifferentialIncomePV, 60, 8, 2, True, Col1DifferentialIncomePV, True, True, False)
            .AddAgNumberColumn(DGL1, Col1DifferentialIncomeBV, 60, 8, 2, True, Col1DifferentialIncomeBV, True, True, False)
            .AddAgNumberColumn(DGL1, Col1DifferentialIncome, 60, 8, 2, True, Col1DifferentialIncome, True, True, False)
        End With
        Dgl1.Anchor = (AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Bottom)
        AgL.FSetSNo(Dgl1, Col_SNo)
        Dgl1.TabIndex = Pnl_DifferentialIncome.TabIndex
        DGL1.ColumnHeadersDefaultCellStyle.Font = New Font(New FontFamily("Arial"), 8)
        DGL1.DefaultCellStyle.Font = New Font(New FontFamily("Arial"), 8)
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

            AgL.WinSetting(Me, 650, 1000, 0, 0)
            AgL.GridDesign(Dgl1)
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
        Dim CondStr As String = ""

        mQry = "Select Bi.Docid As SearchCode " & _
        " From DifferentialIncome Bi " & CondStr
        Topctrl1.FIniForm(DTMaster, AgL.GCn, mQry, , , , , BytDel, BytRefresh)
    End Sub


    Sub Ini_List()
        '' Initialization of Help Grid
    End Sub


    Private Sub Topctrl1_tbAdd() Handles Topctrl1.tbAdd
        BlankText()
        DispText()
        TxtV_Date.Focus()
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

                    AgL.Dman_ExecuteNonQry("Delete From DifferentialIncome1 Where DocId='" & mSearchCode & "'", AgL.GCn, AgL.ECmd)
                    AgL.Dman_ExecuteNonQry("Delete From DifferentialIncome Where DocId='" & mSearchCode & "'", AgL.GCn, AgL.ECmd)

                    Call AgL.LogTableEntry(mSearchCode, Me.Text, "D", AgL.PubMachineName, AgL.PubUserName, AgL.PubLoginDate, AgL.GCn, AgL.ECmd)

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
        TxtV_Date.Focus()
    End Sub

    Private Sub Topctrl1_tbFind() Handles Topctrl1.tbFind
        If DTMaster.Rows.Count <= 0 Then MsgBox("No Records To Search.", vbInformation, AgLibrary.ClsMain.PubMsgTitleInfo) : Exit Sub
        Try


            AgL.PubFindQry = "Select Bi.DocId As SearchCode, Convert(varchar,Bi.V_Date,3) As [Voucher Date], Bi.V_No As [Voucher No.], " & _
                                    " Convert(varchar,Bi.Date_From,3) As [Date From],Convert(varchar,Bi.Date_To,3) As [Date To], Bi.Remark From DifferentialIncome Bi"


            AgL.PubFindQryOrdBy = "SearchCode"



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
        Dim I As Integer, Sr As Integer
        Dim mTrans As Boolean = False
        Try
            MastPos = BMBMaster.Position

            If Not Data_Validation() Then Exit Sub

            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = True


            If Topctrl1.Mode = "Add" Then
                mQry = "Insert Into DifferentialIncome (DocId, V_Date, V_Type, V_Prefix, V_No, Date_From, Date_To,PvMultiplier," & _
                        " Div_Code, Site_Code, EntryDate, EntryBy, Status) Values(" & _
                        " '" & mSearchCode & "', " & AgL.ConvertDate(TxtV_Date.Text) & ", '" & mV_Type & "', '" & mV_Prefix & "'," & _
                        " " & Val(TxtV_No.Text) & "," & AgL.ConvertDate(TxtDate_From.Text) & ", " & AgL.ConvertDate(TxtDate_To.Text) & ", " & Val(TxtPVMultiplier.Text) & "," & _
                        " '" & AgL.PubDivCode & "', '" & AgL.PubSiteCode & "', '" & Format(AgL.PubLoginDate, "Short Date") & "', '" & AgL.PubUserName & "', 'Active') "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            Else
                mQry = "Update DifferentialIncome Set V_Date=" & AgL.ConvertDate(TxtV_Date.Text) & ",Date_From=" & AgL.ConvertDate(TxtDate_From.Text) & ", " & _
                        " Date_To=" & AgL.ConvertDate(TxtDate_To.Text) & ", PvMultiplier = " & Val(TxtPVMultiplier.Text) & ", " & _
                        " ModifyDate='" & Format(AgL.PubLoginDate, "Short Date") & "', ModifyBy='" & AgL.PubUserName & "' " & _
                        " Where DocId='" & mSearchCode & "'"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If


            mQry = "Delete From DifferentialIncome1 Where DocId = '" & mSearchCode & "'"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            Sr = 0
            With Dgl1
                For I = 0 To .Rows.Count - 1
                    If .Item(Col1Distributor, I).Value IsNot Nothing Then
                        If .Item(Col1Distributor, I).Value <> "" Then
                            Sr = Sr + 1
                            mQry = "Insert Into DifferentialIncome1(DocId, Sr, " & _
                                    "Date_From, " & _
                                    "Date_To, " & _
                                    "Distributor, " & _
                                    "DistributorPer, " & _
                                    "ChildDistributor, " & _
                                    "ChildDistributorPer, " & _
                                    "ChildDistributorPV, " & _
                                    "ChildDistributorBV, " & _
                                    "PVMultiplier, " & _
                                    "DifferentialPer, " & _
                                    "DifferentialIncomePV, " & _
                                    "DifferentialIncomeBV, " & _
                                    "DifferentialIncome) " & _
                                    "Values( " & _
                                    "'" & mSearchCode & "', " & Sr & ", " & _
                                    "" & AgL.Chk_Text(TxtDate_From.Text) & ", " & _
                                    "" & AgL.Chk_Text(TxtDate_To.Text) & ", " & _
                                    "" & AgL.Chk_Text(.Item(Col1Distributor, I).Tag) & ", " & _
                                    "" & Val(.Item(Col1DistributorPer, I).Value) & ", " & _
                                    "" & AgL.Chk_Text(.Item(Col1DownlineDistributor, I).Tag) & ", " & _
                                    "" & Val(.Item(Col1DownlineDistributorPer, I).Value) & ", " & _
                                    "" & Val(.Item(Col1DownlineDistributorPV, I).Value) & ", " & _
                                    "" & Val(.Item(Col1DownlineDistributorBV, I).Value) & ", " & _
                                    "" & Val(TxtPVMultiplier.Text) & ", " & _
                                    "" & Val(.Item(Col1DistributorPer, I).Value) - Val(.Item(Col1DownlineDistributorPer, I).Value) & ", " & _
                                    "" & Val(.Item(Col1DifferentialIncomePV, I).Value) & ", " & _
                                    "" & Val(.Item(Col1DifferentialIncomeBV, I).Value) & ", " & _
                                    "" & Val(.Item(Col1DifferentialIncome, I).Value) & " " & _
                                    ") "
                            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
                        End If
                    End If
                Next I
            End With

            AgL.UpdateVoucherCounter(mSearchCode, CDate(TxtV_Date.Text), AgL.GCn, AgL.ECmd, AgL.PubDivCode, AgL.PubSiteCode)
            Call AgL.LogTableEntry(mSearchCode, Me.Text, AgL.MidStr(Topctrl1.Mode, 0, 1), AgL.PubMachineName, AgL.PubUserName, AgL.PubLoginDate, AgL.GCn, AgL.ECmd)


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
        Dim DTTemp As DataTable = Nothing
        Dim I As Integer
        Dim MastPos As Long
        Try
            FClear()
            BlankText()
            If DTMaster.Rows.Count > 0 Then
                MastPos = BMBMaster.Position
                mSearchCode = DTMaster.Rows(MastPos)("SearchCode")
                mQry = "Select Bi.* " & _
                    " From DifferentialIncome Bi Where Bi.DocId='" & mSearchCode & "'"
                DTTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
                With DTTemp
                    If .Rows.Count > 0 Then
                        mV_Prefix = AgL.XNull(.Rows(0)("V_Prefix"))
                        mV_Type = AgL.XNull(.Rows(0)("V_Type"))
                        TxtV_No.Text = AgL.VNull(.Rows(0)("V_No"))
                        TxtV_Date.Text = AgL.RetDate(AgL.XNull(.Rows(0)("Date_From")))
                        TxtDate_From.Text = AgL.RetDate(AgL.XNull(.Rows(0)("Date_From")))
                        TxtDate_To.Text = AgL.RetDate(AgL.XNull(.Rows(0)("Date_To")))
                        TxtPVMultiplier.Text = AgL.VNull(.Rows(0)("PVMultiplier"))
                        TxtRemark.Text = AgL.XNull(.Rows(0)("Remark"))

                        TxtPrepared.Text = AgL.XNull(.Rows(0)("EntryBy"))
                        'TxtApproved.Text = AgL.XNull(.Rows(0)("ApprovedBy"))
                        TxtModified.Text = AgL.XNull(.Rows(0)("ModifyBy"))
                        GroupBox4.Visible = IIf(TxtModified.Text.Trim <> "", True, False)
                    End If
                End With



                mQry = "Select D.Name as DistributorName, D.ManualCode as DistributorCode, " & _
                       "CD.Name as ChildDistributorName, CD.ManualCode as ChildDistributorCode,  " & _
                    "S.* " & _
                    "From DifferentialIncome1 S " & _
                    "Left Join SubGroup D On S.Distributor = D.SubCode " & _
                    "Left Join SubGroup CD On S.ChildDistributor = CD.SubCode " & _
                    "Where S.DocId='" & mSearchCode & "'"
                DTTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
                'Dgl1.DataSource = DsTemp.Tables(0)

                'Dgl1.Columns(0).Visible = False
                'Dgl1.Columns("SubCode").Visible = False

                DGL1.RowCount = 1
                DGL1.Rows.Clear()
                With DtTemp
                    If .Rows.Count > 0 Then
                        For I = 0 To DtTemp.Rows.Count - 1
                            DGL1.Rows.Add()
                            DGL1.Item(Col_SNo, I).Value = DGL1.Rows.Count - 1
                            DGL1.Item(Col1Distributor, I).Tag = AgL.XNull(.Rows(I)("Distributor"))
                            DGL1.Item(Col1Distributor, I).Value = AgL.XNull(.Rows(I)("DistributorName"))
                            DGL1.Item(Col1DistributorPer, I).Value = Format(AgL.VNull(.Rows(I)("DistributorPer")), "0.00")
                            DGL1.Item(Col1DownlineDistributor, I).Tag = AgL.XNull(.Rows(I)("ChildDistributor"))
                            DGL1.Item(Col1DownlineDistributor, I).Value = AgL.XNull(.Rows(I)("ChildDistributorName"))
                            DGL1.Item(Col1DownlineDistributorPer, I).Value = Format(AgL.VNull(.Rows(I)("ChildDistributorPer")), "0.00")
                            DGL1.Item(Col1DownlineDistributorPV, I).Value = Format(AgL.VNull(.Rows(I)("ChildDistributorPV")), "0.00")
                            DGL1.Item(Col1DownlineDistributorBV, I).Value = Format(AgL.VNull(.Rows(I)("ChildDistributorBV")), "0.00")
                            DGL1.Item(Col1DifferentialIncomePV, I).Value = Format(AgL.VNull(.Rows(I)("DifferentialIncomePV")), "0.00")
                            DGL1.Item(Col1DifferentialIncomeBV, I).Value = Format(AgL.VNull(.Rows(I)("DifferentialIncomeBV")), "0.00")
                            DGL1.Item(Col1DifferentialIncome, I).Value = Format(AgL.VNull(.Rows(I)("DifferentialIncome")), "0.00")
                        Next I
                    End If
                End With

            Else
                BlankText()
            End If
            Topctrl1.FSetDispRec(BMBMaster)
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            If DTTemp IsNot Nothing Then DTTemp.Dispose()
        End Try
    End Sub
    Private Sub BlankText()
        If Topctrl1.Mode <> "Add" Then Topctrl1.BlankTextBoxes(Me)
        mSearchCode = ""

        Dgl1.DataSource = Nothing

        Dgl1.RowCount = 1

        Dgl1.ReadOnly = True
        Dgl1.Rows.Clear()
    End Sub
    Private Sub DispText(Optional ByVal Enb As Boolean = False)
        'Coding To Enable/Disable Controls
        TxtV_No.Enabled = False
        TxtNationalBV.Enabled = False
        TxtNationalPV.Enabled = False
        TxtApproved.Enabled = False : TxtPrepared.Enabled = False : TxtModified.Enabled = False
    End Sub

    Private Sub DGL1_CellValidated(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        If Topctrl1.Mode = "Browse" Then Exit Sub
        Try
            Select Case sender.CurrentCell.ColumnIndex
                'Case Col1Rank_Code
                '    DGL1.Item(Col1Rank_Code, DGL1.CurrentCell.RowIndex).Value = DGL1.Item(Col1Rank_Code, DGL1.CurrentCell.RowIndex).Value
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub


    Private Sub DGL1_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs)
        If Topctrl1.Mode = "Browse" Then Exit Sub
        If TypeOf e.Control Is ComboBox Then
            e.Control.Text = "" : CType(e.Control, ComboBox).SelectedIndex = -1
        End If
    End Sub

    Private Sub DGL1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If Topctrl1.Mode = "Browse" Then Exit Sub
        If e.Control And e.KeyCode = Keys.D Then sender.CurrentRow.Selected = True
        If e.Control Or e.Shift Or e.Alt Then Exit Sub
        If e.KeyCode = Keys.Delete Then DGL1.Item(sender.CurrentCell.ColumnIndex, sender.CurrentCell.rowindex).value = ""

        Try
            Select Case sender.CurrentCell.ColumnIndex
                'Case <Dgl_Column>
                '    <Executable Code>
            End Select

        Catch Ex As NullReferenceException
        Catch Ex As Exception
            MsgBox(Ex.Message)
        End Try
    End Sub


    Private Sub DGL1_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) 'Handles DGL1.RowsAdded
        sender(Col_SNo, sender.Rows.Count - 1).Value = Trim(sender.Rows.Count)
    End Sub


    Private Sub DGL1_RowsRemoved(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs) 'Handles DGL1.RowsRemoved
        Try
            DTStruct.Rows.Remove(DTStruct.Rows.Item(e.RowIndex))
        Catch ex As Exception
        End Try

        AgL.FSetSNo(sender, Col_SNo)
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

    Private Sub Control_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) _
              Handles TxtV_Date.Validating
        Dim DsTemp As DataSet
        Try
            Select Case sender.NAME
                Case TxtV_Date.Name
                    If Topctrl1.Mode = "Add" Then
                        TxtV_No.Text = ""
                        If TxtV_Date.Text.Trim <> "" Then
                            mSearchCode = AgL.GetDocId(mV_Type, CStr(TxtV_No.Text), CDate(TxtV_Date.Text), AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode)
                            If mSearchCode.Trim <> "" Then
                                TxtV_No.Text = Val(AgL.DeCodeDocID(mSearchCode, AgLibrary.ClsMain.DocIdPart.VoucherNo))
                            End If
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            DsTemp = Nothing
        End Try
    End Sub
    Private Function Data_Validation(Optional ByVal IsSaveTimeValidation As Boolean = True) As Boolean
        Dim mCondStr As String
        Dim I As Integer = 0, J As Integer = 0
        Dim mDataExists As Boolean = False
        Try
            If AgL.RequiredField(TxtV_Date) Then Exit Function
            If AgL.RequiredField(TxtDate_From) Then Exit Function
            If AgL.RequiredField(TxtDate_To) Then Exit Function

            If CDate(TxtDate_From.Text) > CDate(TxtDate_To.Text) Then
                MsgBox("Date To Can't be Less Than From Date From") : TxtDate_To.Focus() : Exit Function
            End If

            mCondStr = IIf(Topctrl1.Mode = "Add", "", " And DocId<>'" & mSearchCode & "'")
            If TxtDate_To.Text.Trim <> "" Then
                mCondStr = mCondStr & " And (" & AgL.ConvertDate(TxtDate_From.Text) & " Between Date_From And Date_To Or " & AgL.ConvertDate(TxtDate_To.Text) & " Between Date_From And Date_To) "
            End If

            mQry = "Select IsNull(Count(*),0) Cnt From DifferentialIncome " & _
                    " Where 1=1 " & mCondStr
            AgL.ECmd = AgL.Dman_Execute(mQry, AgL.GCn)
            If AgL.ECmd.ExecuteScalar > 0 Then
                MsgBox("Binary Income Already Generated For Selected Period!...") : TxtDate_From.Focus() : Exit Function
            End If

            If IsSaveTimeValidation Then
                'With DGL1
                '    For I = 0 To .Rows.Count - 1
                '        If .Item(Col1Policy_No, I).Value IsNot Nothing And .Item(Col1InstallmentNo, I).Value IsNot Nothing Then
                '            If .Item(Col1PvLeftOpening, I).Value Is Nothing Then .Item(Col1PvLeftOpening, I).Value = ""
                '            If .Item(Col1PvRightOpening, I).Value Is Nothing Then .Item(Col1PvRightOpening, I).Value = ""
                '            If .Item(Col1PvLeft, I).Value Is Nothing Then .Item(Col1PvLeft, I).Value = ""
                '            If .Item(Col1PvRight, I).Value Is Nothing Then .Item(Col1PvRight, I).Value = ""
                '            If .Item(Col1PvLeftClosing, I).Value Is Nothing Then .Item(Col1PvLeftClosing, I).Value = ""
                '            If .Item(Col1PvRightClosing, I).Value Is Nothing Then .Item(Col1PvRightClosing, I).Value = ""
                '            If .Item(Col1PvMultiplierForComm, I).Value Is Nothing Then .Item(Col1PvMultiplierForComm, I).Value = ""
                '            If .Item(Col1Income, I).Value Is Nothing Then .Item(Col1Income, I).Value = ""

                '            If .Item(Col1Policy_No, I).Value <> "" And .Item(Col1InstallmentNo, I).Value <> "" And _
                '                (Val(.Item(Col1PvLeft, I).Value) > 0 Or Val(.Item(Col1PvRight, I).Value) > 0) And _
                '                Val(.Item(Col1Income, I).Value) > 0 Then
                '                For J = I + 1 To .Rows.Count - 1
                '                    If .Item(Col1Policy_No, J).Value IsNot Nothing And .Item(Col1InstallmentNo, J).Value IsNot Nothing Then
                '                        If .Item(Col1Policy_No, I).Value = .Item(Col1Policy_No, J).Value And .Item(Col1InstallmentNo, I).Value = .Item(Col1InstallmentNo, J).Value Then
                '                            MsgBox("Duplicate ""Installment No."" At Row No. " & Val(.Item(Col_SNo, J).Value) & "") : .CurrentCell = DGL1(Col1InstallmentNo, J) : .Focus() : Exit Function
                '                        End If
                '                    End If
                '                Next
                '            End If

                '            If mDataExists = False And _
                '                (.Item(Col1Policy_No, I).Value <> "" And .Item(Col1InstallmentNo, I).Value <> "" And _
                '                (Val(.Item(Col1PvLeft, I).Value) > 0 Or Val(.Item(Col1PvRight, I).Value) > 0) And _
                '                Val(.Item(Col1Income, I).Value) > 0) Then

                '                mDataExists = True
                '            End If

                '        End If
                '    Next I
                '    If mDataExists = False Then MsgBox("Income Detail Grid Can't be Blank!...") : .CurrentCell = DGL1(Col1InstallmentDate, 0) : .Focus() : Exit Function Else mDataExists = False
                'End With

                If Topctrl1.Mode = "Add" Then
                    If mSearchCode.Trim <> "" Then
                        AgL.ECmd = AgL.Dman_Execute("Select IsNull(count(*),0) From DifferentialIncome Where DocId='" & mSearchCode & "' ", AgL.GCn)
                        If AgL.ECmd.ExecuteScalar() > 0 Then MsgBox("DocId Already Exist!") : TxtV_Date.Focus() : Exit Function
                    Else
                        mSearchCode = AgL.GetDocId(mV_Type, CStr(TxtV_No.Text), CDate(TxtV_Date.Text), AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode)
                        If mSearchCode.Trim <> "" Then TxtV_No.Text = Val(AgL.DeCodeDocID(mSearchCode, AgLibrary.ClsMain.DocIdPart.VoucherNo))
                    End If
                    If mSearchCode.Trim = "" Then MsgBox("Fater Error!..." & vbCrLf & "Problem in Docid Generation") : TxtV_Date.Focus() : Exit Function

                    mV_Prefix = AgL.DeCodeDocID(mSearchCode, AgLibrary.ClsMain.DocIdPart.VoucherPrefix)
                    mV_Type = AgL.DeCodeDocID(mSearchCode, AgLibrary.ClsMain.DocIdPart.VoucherType)
                End If
            End If
            Data_Validation = True
        Catch ex As Exception
            MsgBox(ex.Message)
            Data_Validation = False
        Finally

        End Try
    End Function


    Private Function AccountPosting() As Boolean
        Dim LedgAry() As AgLibrary.ClsMain.LedgRec
        Dim I As Integer, J As Long
        Dim mNarr As String = ""
        Dim mAmt As Double = 0
        Dim TblTemp As DataTable
        Dim mDifferentialIncomeAc$
        Dim mVNo As Long = Val(AgL.DeCodeDocID(mSearchCode, AgLibrary.ClsMain.DocIdPart.VoucherNo))

        AccountPosting = True

        mDifferentialIncomeAc = AgL.FillData("Select IsNull(DifferentialIncomeAc,'') From Enviro", AgL.GCn).Tables(0).Rows(0)(0)
        If mDifferentialIncomeAc = "" Then MsgBox("Binary Income A/C Not Defined In Environment Settings ") : Exit Function

        mQry = "Select SubCode, Income From DifferentialIncome1 Where DocId='" & mSearchCode & "' Order By Sr "
        TblTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

        I = 0
        For J = 0 To TblTemp.Rows.Count - 1
            ReDim Preserve LedgAry(I)

            LedgAry(I).SubCode = AgL.XNull(TblTemp.Rows(J)("SubCode"))
            LedgAry(I).ContraSub = mDifferentialIncomeAc
            LedgAry(I).AmtDr = 0
            LedgAry(I).AmtCr = AgL.VNull(TblTemp.Rows(J)("Income"))
            LedgAry(I).Narration = mNarr
            mAmt += AgL.VNull(TblTemp.Rows(J)("Income"))

            I = UBound(LedgAry) + 1
        Next

        ReDim Preserve LedgAry(I)
        LedgAry(I).SubCode = mDifferentialIncomeAc
        LedgAry(I).ContraSub = ""
        LedgAry(I).AmtDr = mAmt
        LedgAry(I).AmtCr = 0
        LedgAry(I).Narration = mNarr



        If AgL.LedgerPost(AgL.MidStr(Topctrl1.Mode, 0, 1), LedgAry, AgL.GCn, AgL.ECmd, mSearchCode, CDate(TxtV_Date.Text), AgL.PubUserName, AgL.PubLoginDate) = False Then
            MsgBox("Error in Ledger Posting", vbOKOnly, "Validation") : AccountPosting = False
        End If

    End Function


    'Private Sub BtnFillDetail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnFillDetail.Click
    '    Dim sTblPvOpBal$
    '    Dim DsTemp As DataSet
    '    Dim mActivePlanCode As String = ""
    '    Dim mBinaryFYC_Cap As Double
    '    Dim mBinaryRc_Cap_Min As Double
    '    Dim mBinaryRc_Cap_Max As Double
    '    Dim mIncomeForDays As Integer

    '    Dim PLO$, PRO$, PLC$, PRC$, PT$, ActIncome$

    '    If Data_Validation(False) = False Then Exit Sub





    '    mIncomeForDays = DateDiff(DateInterval.Day, CDate(TxtDate_To.Text), CDate(TxtDate_From.Text))
    '    mQry = "SELECT B.Plan_Code, B.Max30DayIncome, B.Max30DayRenewalIncome , B.MinRenewalComm " & _
    '           "FROM BinaryPlan B  " & _
    '           "WHERE B.Date_From <= " & AgL.ConvertDate(TxtV_Date.Text.ToString) & " And (B.Date_To >= " & AgL.ConvertDate(TxtV_Date.Text.ToString) & " Or B.Date_To Is Null) "
    '    DsTemp = AgL.FillData(mQry, AgL.Gcn)
    '    With DsTemp.Tables(0)
    '        If DsTemp.Tables(0).Rows.Count > 0 Then
    '            mActivePlanCode = AgL.XNull(.Rows(0)("Plan_Code"))
    '            mBinaryFYC_Cap = (AgL.VNull(.Rows(0)("Max30DayIncome")) / 30) * mIncomeForDays
    '            mBinaryRc_Cap_Max = (AgL.VNull(.Rows(0)("Max30DayRenewalIncome")) / 30) * mIncomeForDays
    '            mBinaryRc_Cap_Min = (AgL.VNull(.Rows(0)("MinRenewalComm")) / 30) * mIncomeForDays
    '        End If
    '    End With


    '    sTblPvOpBal = "Select S.SubCode, S.PremiumType, Sum(PV_Left)-Sum(PV_Right) As PV_Balance " & _
    '                   "From DifferentialIncome1 S " & _
    '                   "Left Join DifferentialIncome B On S.DocId = B.DocId " & _
    '                   "Where B.V_Date < " & AgL.ConvertDate(TxtV_Date.Text) & " " & _
    '                   "Group By S.SubCode, S.PremiumType "

    '    PLO = " (Case When Max(Op.Pv_Balance) > 0 Then Max(Op.Pv_Balance) Else 0  End) "
    '    PRO = " (Case When Max(Op.Pv_Balance) < 0 Then -Max(Op.Pv_Balance) Else 0  End) "
    '    PLC = " Sum(PV_Left) "
    '    PRC = " Sum(PV_Right) "
    '    PT = "(Case When P.Installment_Year = 1 Then 'FYC' Else 'RC' End)"
    '    ActIncome = "(Case When (" & PLO & " + " & PLC & ")<(" & PRO & " + " & PRC & ") Then (" & PLO & " + " & PLC & ") Else (" & PRO & " + " & PRC & ") End)"

    '    mQry = "Select Max(S.Name) As Name, " & _
    '        "" & PT & " As PremiumType, " & _
    '        "" & PLO & " As [Op. Left PV], " & _
    '        "" & PRO & " As [Op. Right PV], " & _
    '        "" & PLC & " As [Curr. Left PV], " & _
    '        "" & PRC & " As [Curr. Right PV], " & _
    '        "" & PLO & " + " & PLC & " As [Total Left PV],  " & _
    '        "" & PRO & " + " & PRC & " As [Total Right PV], " & _
    '        "(Case When (" & PLO & "+" & PLC & ")-(" & PRO & "+" & PRC & ")>0 Then (" & PLO & "+" & PLC & ")-(" & PRO & "+" & PRC & ") Else 0 End) As [Cl. Left PV], " & _
    '        "(Case When (" & PLO & "+" & PLC & ")-(" & PRO & "+" & PRC & ")<0 Then (" & PRO & "+" & PRC & ")-(" & PLO & "+" & PLC & ") Else 0 End) As [Cl. Right PV], " & _
    '        "(Case When " & PT & "='RC' Then  (Case When " & ActIncome & " > Max(B.BinaryRenewalCeiling)  Then Max(B.BinaryRenewalCeiling) When " & ActIncome & " <  Max(B.BinaryRenewalMinimum)  Then 0 Else " & ActIncome & " End ) Else (Case When " & ActIncome & " >  Max(B.BinaryCeiling)  Then Max(B.BinaryCeiling) Else " & ActIncome & " End ) End) As [Income], " & _
    '        " P.SubCode, " & PLib.PubPV_MultiplierForCommission & " As PVMultiplier " & _
    '        "From PremiumPosting_Binary P " & _
    '        "Left Join PremiumPosting Pm On P.DocId = Pm.DocId " & _
    '        "Left Join Subgroup S On S.SubCode = P.SubCode " & _
    '        "Left Join BinaryPlan_DirectSpilIncome B On B.SubScheme = S.SubScheme And B.Plan_Code = '" & mActivePlanCode & "' " & _
    '        "Left Join (" & sTblPvOpBal & ") As Op On P.SubCode = Op.SubCode And (Case When P.Installment_Year = 1 Then 'FYC' Else 'RC' End) = Op.PremiumType " & _
    '        "Where Pm.V_Date Between " & AgL.ConvertDate(TxtDate_From.Text.ToString) & " And " & AgL.ConvertDate(TxtDate_To.Text.ToString) & " And Pm.ApprovedBy Is Not Null and Pm.ApprovedBy<>'' " & _
    '        "Group By P.SubCode, " & PT & ""
    '    DsTemp = AgL.FillData(mQry, AgL.Gcn)
    '    Dgl1.DataSource = DsTemp.Tables(0)
    '    Dgl1.Columns(0).Visible = False
    '    Dgl1.Columns("SubCode").Visible = False


    'End Sub


    Private Sub BtnFillDetail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnFillDetail.Click
        Dim bTmpTblIncome As String
        Dim bTemptable$, Distributor$
        Dim DtTemp As DataTable
        Dim DrTemp As DataRow()
        Dim DtDistributor As DataTable
        Dim I As Integer
        Dim bParentPvPer As Double, bSelfPV As Double, bSelfBV As Double, bGroupPv As Double, bTotalPv As Double, bPVPer As Double, bCumPV As Double, bTotalBV As Double

        mQry = "SELECT Sum(L.BusinessVolume) AS BusinessVolume, Sum(L.PointValue ) AS PointValue " & _
               "FROM SaleInvoiceDetail L With (NoLock) " & _
               "LEFT JOIN SaleInvoice H  With (NoLock) ON l.DocId = h.DocID " & _
               "WHERE H.V_Date BETWEEN " & AgL.Chk_Text(TxtDate_From.Text) & " and " & AgL.Chk_Text(TxtDate_To.Text) & "  "
        DtTemp = AgL.FillData(mQry, AgL.GCn).tables(0)

        If DtTemp.Rows.Count > 0 Then
            TxtNationalPV.Text = AgL.XNull(DtTemp.Rows(0)("PointValue"))
            TxtNationalBV.Text = AgL.XNull(DtTemp.Rows(0)("BusinessVolume"))
        End If


        Distributor = "COMPANY"

        bTmpTblIncome = "#" & AgL.GetGUID(AgL.GCn).ToString

        mQry = "Create Table [" & bTmpTblIncome & "] " & _
             "(Distributor nVarchar(10), " & _
             "DistributorPer Float, " & _
             "DownlineDistributor nVarchar(10), " & _
             "DownlineDistributorPer Float, " & _
             "DownlineDistributorPV Float, " & _
             "DownlineDistributorBV Float, " & _
             "DistributorIncomePV Float, " & _
             "DistributorIncomeBV Float, " & _
             "DistributorIncome Float) "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

        bTemptable = AgL.GetGUID(AgL.GCn).ToString
        mQry = " CREATE TABLE [#" & bTemptable & "] " & _
                " (Distributer NVARCHAR(20), ParentID nVarchar(10), ParentPvPer Float, DistributerId NVARCHAR(100), " & _
                " DistributerName NVARCHAR(100), " & _
                " SelfPV Float, SelfBV Float, GroupPV Float, TotalPV Float, PVPer Float, CumPV Float, TotalBV Float)  "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

        mQry = " WITH DirectReports (ParentDistributer, SubCode, Level) " & _
                    " AS   " & _
                    " (   " & _
                    " SELECT Sg.ParentDistributer, Sg.SubCode, 0 AS Level   " & _
                    " FROM SubGroup Sg    " & _
                    " WHERE Sg.ParentDistributer = '" & Distributor & "'  " & _
                    " UNION ALL   " & _
                    " SELECT Sg.ParentDistributer, Sg.SubCode, Level + 1   " & _
                    " FROM (SELECT ParentDistributer, SubCode FROM SubGroup    " & _
                    " WHERE SubGroupType = '" & ClsMain.SubGroupType.Distributer & "')   " & _
                    " AS  Sg   " & _
                    " INNER JOIN DirectReports d ON Sg.ParentDistributer = d.SubCode   " & _
                    " )   " & _
                    " " & _
                    " INSERT INTO [#" & bTemptable & "] (ParentID, Distributer, DistributerId, DistributerName) " & _
                    " SELECT D.ParentDistributer, D.SubCode, Sg.ManualCode, Sg.DispName " & _
                    " FROM DirectReports  D " & _
                    " LEFT JOIN SubGroup Sg ON D.SubCode = Sg.SubCode " & _
                    " Union All " & _
                    " SELECT Sg.ParentDistributer, Sg.SubCode, Sg.ManualCode, Sg.DispName " & _
                    " FROM SubGroup Sg where Sg.SubCode = '" & Distributor & "' "

        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

        mQry = " Select Distributer, ParentID From [#" & bTemptable & "] "
        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

        With DtTemp
            For I = 0 To .Rows.Count - 1
                If .Rows.Count > 0 Then

                    bSelfPV = ClsProj.FGetSelfPV(AgL.XNull(.Rows(I)("Distributer")), True, TxtDate_From.Text, TxtDate_To.Text)
                    bSelfBV = ClsProj.FGetSelfBV(AgL.XNull(.Rows(I)("Distributer")), True, TxtDate_From.Text, TxtDate_To.Text)
                    bGroupPv = ClsProj.FGetGroupPV(AgL.XNull(.Rows(I)("Distributer")), True, TxtDate_From.Text, TxtDate_To.Text)
                    bTotalPv = bSelfPV + bGroupPv
                    bPVPer = ClsProj.FGetPVPer(ClsProj.FGetTotalComBV(AgL.XNull(.Rows(I)("Distributer")), True, TxtDate_From.Text, TxtDate_To.Text))
                    bParentPvPer = ClsProj.FGetPVPer(ClsProj.FGetTotalComPV(AgL.XNull(.Rows(I)("ParentID")), True, TxtDate_From.Text, TxtDate_To.Text))
                    bCumPV = ClsProj.FGetTotalComPV(AgL.XNull(.Rows(I)("Distributer")), True, TxtDate_From.Text, TxtDate_To.Text)
                    bTotalBV = ClsProj.FGetTotalBV(AgL.XNull(.Rows(I)("Distributer")), True, TxtDate_From.Text, TxtDate_To.Text)

                    mQry = "Update  [#" & bTemptable & "]  set " & _
                    "ParentPVPer =" & bParentPvPer & ", " & _
                    "SelfPV =" & bSelfPV & ", " & _
                    "SelfBV =" & bSelfBV & ", " & _
                    "GroupPV = " & bGroupPv & ", " & _
                    "TotalPV = " & bTotalPv & ", " & _
                    "PvPer = " & bPVPer & ", " & _
                    "CumPV = " & bCumPV & ", " & _
                    "TotalBv = " & bTotalBV & "  " & _
                    "Where Distributer = '" & AgL.XNull(.Rows(I)("Distributer")) & "' "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
                    'mQry = " INSERT INTO [#" & bTemptable & "] (Distributer, ParentID, SelfPV, GroupPV, TotalPV, " & _
                    '        " PVPer, CumPV, TotalBV) " & _
                    '        " SELECT '" & AgL.XNull(.Rows(I)("Distributer")) & "', '" & AgL.XNull(.Rows(I)("ParentId")) & "', " & _
                    '        " " & bSelfPV & " ," & bGroupPv & " ," & bTotalPv & ", " & _
                    '        " " & bPVPer & ", " & bCumPV & ", " & bTotalBV & ""
                    'AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
                End If
            Next
        End With


        mQry = " Select * From [#" & bTemptable & "] where PvPer>0 "
        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)


        DtDistributor = DtTemp.Copy
        Dim mParentID As String
        For I = 0 To DtDistributor.Rows.Count - 1

            mQry = "Insert Into [" & bTmpTblIncome & "] " & _
                   "(Distributor, " & _
                   "DistributorPer, " & _
                   "DownlineDistributor, " & _
                   "DownlineDistributorPer, " & _
                   "DownlineDistributorPV, " & _
                   "DownlineDistributorBV, " & _
                   "DistributorIncomePV, " & _
                   "DistributorIncomeBV, " & _
                   "DistributorIncome) " & _
                   "Values " & _
                   "( " & _
                   "'" & DtDistributor.Rows(I)("Distributer") & "', " & _
                   "" & AgL.VNull(DtDistributor.Rows(I)("PVPer")) & ", " & _
                   "'" & DtDistributor.Rows(I)("Distributer") & "', " & _
                   "0, " & _
                   "" & AgL.VNull(DtDistributor.Rows(I)("SelfPV")) & ", " & _
                   "" & AgL.VNull(DtDistributor.Rows(I)("SelfBV")) & ", " & _
                   "" & AgL.VNull(DtDistributor.Rows(I)("SelfPV")) * AgL.VNull(DtDistributor.Rows(I)("PVPer")) / 100 & ", " & _
                   "" & AgL.VNull(DtDistributor.Rows(I)("SelfBV")) * AgL.VNull(DtDistributor.Rows(I)("PVPer")) / 100 & ", " & _
                   "" & (AgL.VNull(DtDistributor.Rows(I)("SelfPV")) * AgL.VNull(DtDistributor.Rows(I)("PVPer")) / 100) * Val(TxtPVMultiplier.Text) & " " & _
                   ")"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)



            mParentID = AgL.XNull(DtDistributor.Rows(I)("ParentID"))
            'While mParentID <> ""
            If mParentID <> "" Then
                DrTemp = DtTemp.Select(" Distributer = '" & mParentID & "' ")
                mParentID = AgL.XNull(DrTemp(0)("ParentID"))
                If (AgL.VNull(DrTemp(0)("PVPer")) - AgL.VNull(DtDistributor.Rows(I)("PVPer"))) > 0 Then
                    mQry = "Insert Into [" & bTmpTblIncome & "] " & _
                           "(Distributor, " & _
                           "DistributorPer, " & _
                           "DownlineDistributor, " & _
                           "DownlineDistributorPer, " & _
                           "DownlineDistributorPV, " & _
                           "DownlineDistributorBV, " & _
                           "DistributorIncomePV, " & _
                           "DistributorIncomeBV, " & _
                           "DistributorIncome) " & _
                           "Values " & _
                           "( " & _
                           "'" & DrTemp(0)("Distributer") & "', " & _
                           "" & AgL.VNull(DrTemp(0)("PVPer")) & ", " & _
                           "'" & DtDistributor.Rows(I)("Distributer") & "', " & _
                           "" & AgL.VNull(DtDistributor.Rows(I)("PVPer")) & ", " & _
                           "" & AgL.VNull(DtDistributor.Rows(I)("TotalPV")) & ", " & _
                           "" & AgL.VNull(DtDistributor.Rows(I)("SelfBV")) & ", " & _
                           "" & AgL.VNull(DtDistributor.Rows(I)("TotalPV")) * (AgL.VNull(DrTemp(0)("PVPer")) - AgL.VNull(DtDistributor.Rows(I)("PVPer"))) / 100 & ", " & _
                           "" & AgL.VNull(DtDistributor.Rows(I)("SelfBV")) * (AgL.VNull(DrTemp(0)("PVPer")) - AgL.VNull(DtDistributor.Rows(I)("PVPer"))) / 100 & "," & _
                           "" & (AgL.VNull(DtDistributor.Rows(I)("TotalPV")) * (AgL.VNull(DrTemp(0)("PVPer")) - AgL.VNull(DtDistributor.Rows(I)("PVPer"))) / 100) * Val(TxtPVMultiplier.Text) & " " & _
                           ")"
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
                End If
            End If
            'End While
        Next


        mQry = "Select Ds.Name as DistributorName, DDS.DispName as DownlineDistributorName, L.* from [" & bTmpTblIncome & "] L " & _
               "Left Join Subgroup DS On Ds.Subcode = L.Distributor " & _
               "Left Join SubGroup DDS on DDS.SubCode = L.DownlineDistributor "

        'Dgl1.Columns("DistributerCode").Visible = False
        DtTemp = AgL.FillData(mQry, AgL.GCn).tables(0)
        'Dgl1.DataSource = DtTemp

        DGL1.RowCount = 1
        DGL1.Rows.Clear()
        With DtTemp
            If .Rows.Count > 0 Then
                For I = 0 To DtTemp.Rows.Count - 1
                    DGL1.Rows.Add()
                    DGL1.Item(Col_SNo, I).Value = DGL1.Rows.Count - 1
                    DGL1.Item(Col1Distributor, I).Tag = AgL.XNull(.Rows(I)("Distributor"))
                    DGL1.Item(Col1Distributor, I).Value = AgL.XNull(.Rows(I)("DistributorName"))
                    DGL1.Item(Col1DistributorPer, I).Value = Format(AgL.VNull(.Rows(I)("DistributorPer")), "0.00")
                    DGL1.Item(Col1DownlineDistributor, I).Tag = AgL.XNull(.Rows(I)("DownLineDistributor"))
                    DGL1.Item(Col1DownlineDistributor, I).Value = AgL.XNull(.Rows(I)("DownlineDistributorName"))
                    DGL1.Item(Col1DownlineDistributorPer, I).Value = Format(AgL.VNull(.Rows(I)("DownLineDistributorPer")), "0.00")
                    DGL1.Item(Col1DownlineDistributorPV, I).Value = Format(AgL.VNull(.Rows(I)("DownLineDistributorPV")), "0.00")
                    DGL1.Item(Col1DownlineDistributorBV, I).Value = Format(AgL.VNull(.Rows(I)("DownLineDistributorBV")), "0.00")
                    DGL1.Item(Col1DifferentialIncomePV, I).Value = Format(AgL.VNull(.Rows(I)("DistributorIncomePV")), "0.00")
                    DGL1.Item(Col1DifferentialIncomeBV, I).Value = Format(AgL.VNull(.Rows(I)("DistributorIncomeBV")), "0.00")
                    DGL1.Item(Col1DifferentialIncome, I).Value = Format(AgL.VNull(.Rows(I)("DistributorIncome")), "0.00")
                Next I
            End If
        End With


    End Sub

    Private Sub FillSaphireBonus()
        Dim strExQry
        Dim DtSubCode As DataTable = Nothing
        Dim DtSaphire As DataTable = Nothing
        Dim DtTemp As DataTable = Nothing
        Dim DtChild As DataTable = Nothing
        Dim DtSaphireConst As DataTable = Nothing
        Dim I As Integer
        Dim J As Integer
        Dim intSaphireLegs As Integer
        Dim dblPeriodPv As Double
        Dim dblPeriodPvSelf As Double
        Dim isCountable As String = "Y"
        Dim strTemp As String





        mQry = "Select * from Ample_Rank Where Sr = 1 "
        DtSaphireConst = AgL.FillData(mQry, AgL.GCn).Tables(0)


        mQry = "Select SubCode From SubGroup Where SubGroupType = '" & ClsMain.SubGroupType.Distributer & "' And IsNull(IsInSapphireRace,'N') = 'N' "
        DtSubCode = AgL.FillData(mQry, AgL.GCn)
        strTemp = ""
        For I = 0 To DtSubCode.Rows.Count - 1
            If ClsProj.FGetTotalComPV(AgL.XNull(DtSubCode.Rows(I)("Subcode")), False) > 6000 Then
                strTemp += IIf(strTemp <> "", ",", "") & AgL.XNull(DtSubCode.Rows(I)("Subcode"))
            End If
        Next

        If strTemp <> "" Then
            mQry = "Update SubGroup Set IsInSapphireRace='Y' Where SubCode In ('" & Replace(strTemp, ",", "','") & "')"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        End If




        strExQry = " DECLARE @TmpTable As Table " & _
                " ( " & _
                " SubCode          NVARCHAR (10), " & _
                " Parent           NVARCHAR (10), " & _
                " IsInSapphireRace NVARCHAR (1), " & _
                " IsCountable      NVARCHAR (1), " & _
                " SaphireLegs      Float, " & _
                " PV               Float " & _
                " ) "

        mQry = " Create #TmpTable" & _
                " ( " & _
                " SubCode                   NVARCHAR (10), " & _
                " Parent                    NVARCHAR (10), " & _
                " IsInSapphireRace          NVARCHAR (1), " & _
                " IsCountable               NVARCHAR (1), " & _
                " IsEligibleForIncome       NVARCHAR (1), " & _
                " SaphireLegs               Float, " & _
                " SelfPV                    Float " & _
                " GroupPV                       Float " & _
                " ) "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

        mQry = "Select SubCode, IsNull(IsInSaphireRace,'N') as IsInSapphireRace, ParentDistributer From Subgroup WHERE SubGroupType = '" & ClsMain.SubGroupType.Distributer & "'"
        DtSubCode = AgL.FillData(mQry, AgL.GCn)
        For I = 0 To DtSubCode.Rows.Count - 1
            dblPeriodPvSelf = ClsProj.FGetSelfPV(AgL.XNull(DtSubCode.Rows(I)("Subcode")), True, TxtDate_From.Text, TxtDate_To.Text)
            dblPeriodPv = ClsProj.FGetGroupPV(AgL.XNull(DtSubCode.Rows(I)("Subcode")), True, TxtDate_From.Text, TxtDate_To.Text)
            isCountable = "Y"
            If dblPeriodPv > AgL.VNull(DtSaphireConst.Rows(0)("PeriodPV")) Then
                isCountable = "N"
            End If
            strExQry += "Insert Into #TmpTable (SubCode, Parent, IsInSapphireRace, IsCountable, SelfPv, GroupPV) " & _
                        "Values ('" & AgL.XNull(DtSubCode.Rows(I)("Subcode")) & "', '" & AgL.XNull(DtSubCode.Rows(I)("ParentDistributer")) & "', '" & AgL.XNull(DtSubCode.Rows(I)("IsInSapphireRace")) & "', '" & isCountable & "', " & dblPeriodPvSelf & ", " & dblPeriodPv & ")"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
        Next



        mQry = "Select H.Subcode, Sum(Case When H1.IsCountable='Y' Then H1.GroupPV Else 0  End) as GroupPV, " & _
               "Count(Case When H1.IsCountable='N' Then 1 Else 0 End) as Total20Per " & _
               "from #TmpTable H " & _
               "Left Join #TmpTable H1 On H.SubCode = H1.Parent " & _
               "Group By H.SubCode"
        mQry = "Select X.Subcode, X.Parent, X.GroupPV, X.Total20Per " & _
               "From (" & mQry & ") as X " & _
               "Left Join "
        DtSubCode = AgL.FillData(mQry, AgL.GCn).Tables(0)
        For I = 0 To DtSubCode.Rows.Count - 1

        Next





        mQry = strExQry + " Select * from @TmpTable "
        DtSaphire = AgL.FillData(mQry, AgL.GCn).Tables(0)
        For I = 0 To DtSaphire.Rows.Count - 1
            mQry = "Select SubCode From Subgroup Where ParentDistributer = '" & DtSaphire.Rows(I)("SubCode") & "' "
            DtChild = AgL.FillData(mQry, AgL.GCn)
            intSaphireLegs = 0
            For J = 0 To DtChild.Rows.Count - 1
                mQry = strExQry + " Select Count(*) from @TmpTable Where SubCode ='" & DtChild.Rows(J)("SubCode") & "' "
                If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then
                    intSaphireLegs += 1
                Else
                    mQry = " WITH DirectReports (ParentDistributer, SubCode, Level)  " & _
                        " AS   " & _
                        " (   " & _
                        " SELECT Sg.ParentDistributer , Sg.SubCode, 0 AS Level   " & _
                        " FROM SubGroup Sg    " & _
                        " WHERE Sg.ParentDistributer = '" & DtChild.Rows(J)("SubCode") & "' " & _
                        " UNION ALL   " & _
                        " SELECT D.ParentDistributer, Sg.SubCode, Level + 1   " & _
                        " FROM (SELECT ParentDistributer, SubCode FROM SubGroup    " & _
                        " WHERE SubGroupType = '" & ClsMain.SubGroupType.Distributer & "') " & _
                        " AS  Sg   " & _
                        " INNER JOIN DirectReports d ON Sg.ParentDistributer = d.SubCode " & _
                        " )   " & _
                        " " & _
                        " SELECT D.SubCode " & _
                        " FROM DirectReports  D "
                    mQry = strExQry + " Select Count(*) from @TmpTable Where SubCode In (" & mQry & ") "
                    If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then
                        intSaphireLegs += 1
                    End If
                End If
            Next
            If intSaphireLegs > 0 Then
                strExQry = strExQry & " Update @TmpTable Set SaphireLegs = " & intSaphireLegs & " Where SubCode = '" & DtSaphire.Rows(I)("SubCode") & "' "
            End If
        Next

        mQry = strExQry & " Select * from @TmpTable "
        DtSaphire = AgL.FillData(mQry, AgL.GCn)
        For I = 0 To DtSaphire.Rows.Count - 1
            If AgL.VNull(DtSaphire.Rows(I)("SaphireLegs")) = 1 Then
                mQry = "Select * from Ample_Rank Where Sr=2 "
                DtSaphireConst = AgL.FillData(mQry, AgL.GCn)
                dblPeriodPv = ClsProj.FGetTotalComPV(AgL.XNull(DtSaphire.Rows(I)("Subcode")), False, TxtDate_From.Text, TxtDate_To.Text)
                If dblPeriodPv >= DtSaphireConst.Rows(0)("PeriodPv") Then
                    strExQry = strExQry & " Update @TmpTable Set PV = " & dblPeriodPv & " Where SubCode = '" & DtSaphire.Rows(I)("SubCode") & "' "
                End If
            ElseIf AgL.VNull(DtSaphire.Rows(I)("SaphireLegs")) = 2 Then
                mQry = "Select * from Ample_Rank Where Sr = 3 "
                DtSaphireConst = AgL.FillData(mQry, AgL.GCn)
                dblPeriodPv = ClsProj.FGetTotalComPV(AgL.XNull(DtSaphire.Rows(I)("Subcode")), False, TxtDate_From.Text, TxtDate_To.Text)
                If dblPeriodPv >= DtSaphireConst.Rows(0)("PeriodPv") Then
                    strExQry = strExQry & " Update @TmpTable Set PV = " & dblPeriodPv & " Where SubCode = '" & DtSaphire.Rows(I)("SubCode") & "' "
                End If
            ElseIf AgL.VNull(DtSaphire.Rows(I)("SaphireLegs")) = 3 Then
                mQry = "Select * from Ample_Rank Where Sr = 4 "
                DtSaphireConst = AgL.FillData(mQry, AgL.GCn)
                dblPeriodPv = ClsProj.FGetTotalComPV(AgL.XNull(DtSaphire.Rows(I)("Subcode")), False, TxtDate_From.Text, TxtDate_To.Text)
                If dblPeriodPv >= DtSaphireConst.Rows(0)("PeriodPv") Then
                    strExQry = strExQry & " Update @TmpTable Set PV = " & dblPeriodPv & " Where SubCode = '" & DtSaphire.Rows(I)("SubCode") & "' "
                End If
            ElseIf AgL.VNull(DtSaphire.Rows(I)("SaphireLegs")) = 4 Then
                mQry = "Select * from Ample_Rank Where Sr = 4 "
                DtSaphireConst = AgL.FillData(mQry, AgL.GCn)
                dblPeriodPv = ClsProj.FGetTotalComPV(AgL.XNull(DtSaphire.Rows(I)("Subcode")), False, TxtDate_From.Text, TxtDate_To.Text)
                If dblPeriodPv >= DtSaphireConst.Rows(0)("PeriodPv") Then
                    strExQry = strExQry & " Update @TmpTable Set PV = " & dblPeriodPv & " Where SubCode = '" & DtSaphire.Rows(I)("SubCode") & "' "
                End If
            ElseIf AgL.VNull(DtSaphire.Rows(I)("SaphireLegs")) = 5 Then
                mQry = "Select * from Ample_Rank Where Sr = 5 "
                DtSaphireConst = AgL.FillData(mQry, AgL.GCn)
                dblPeriodPv = ClsProj.FGetTotalComPV(AgL.XNull(DtSaphire.Rows(I)("Subcode")), False, TxtDate_From.Text, TxtDate_To.Text)
                If dblPeriodPv >= DtSaphireConst.Rows(0)("PeriodPv") Then
                    strExQry = strExQry & " Update @TmpTable Set PV = " & dblPeriodPv & " Where SubCode = '" & DtSaphire.Rows(I)("SubCode") & "' "
                End If

            ElseIf AgL.VNull(DtSaphire.Rows(I)("SaphireLegs")) > 5 Then
                mQry = "Select * from Ample_Rank Where Sr = 6 "
                DtSaphireConst = AgL.FillData(mQry, AgL.GCn)
                dblPeriodPv = ClsProj.FGetTotalComPV(AgL.XNull(DtSaphire.Rows(I)("Subcode")), False, TxtDate_From.Text, TxtDate_To.Text)
                If dblPeriodPv >= DtSaphireConst.Rows(0)("PeriodPv") Then
                    strExQry = strExQry & " Update @TmpTable Set PV = " & dblPeriodPv & " Where SubCode = '" & DtSaphire.Rows(I)("SubCode") & "' "
                End If
            End If
        Next


    End Sub
End Class
