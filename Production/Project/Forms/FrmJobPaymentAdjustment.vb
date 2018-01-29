Public Class FrmJobPaymentAdjustment
    Public WithEvents DglBill As New AgControls.AgDataGrid
    Public WithEvents DglPayment As New AgControls.AgDataGrid
    Public WithEvents DglAdj As New AgControls.AgDataGrid

    Protected Const Col_Sno As String = "Sr"

    Protected Const ColBill_DocID As String = "DocID"
    Protected Const ColBill_Sr As String = "Sr"
    Protected Const ColBill_BillNo As String = "Bill No"
    Protected Const ColBill_Party As String = "Party"
    Protected Const ColBill_CostCenter As String = "Cost Center"
    Protected Const ColBill_Amount As String = "Amount"
    Protected Const ColBill_AdjAmount As String = "Adj Amount"
    Protected Const ColBill_BalAmount As String = "Bal Amount"


    Protected Const ColPA_DocID As String = "DocID"
    Protected Const ColPA_Sr As String = "Sr"
    Protected Const ColPA_PaymentNo As String = "Payment No"
    Protected Const ColPA_Party As String = "Party"
    Protected Const ColPA_CostCenter As String = "Cost Center"
    Protected Const ColPA_Amount As String = "Amount"
    Protected Const ColPA_AdjAmount As String = "Adj Amount"
    Protected Const ColPA_BalAmount As String = "Bal Amount"
    Protected Const ColPA_Adj As String = "Adj"


    Dim DtBill As DataTable
    Dim DtPayment As DataTable
    Dim DtAdj As DataTable

    Private trdSave As Threading.Thread
    Private trdFill As Threading.Thread

    Private Property ProgressStatus() As String
        Get
            Return LblStatus.Text
        End Get
        Set(ByVal value As String)
            LblStatus.Text = value
        End Set
    End Property

    Private Sub Ini_Grid()
        Try
            DglBill.EnableHeadersVisualStyles = False
            DglBill.AgSkipReadOnlyColumns = True
            DglBill.ColumnHeadersHeight = 35
            DglBill.ReadOnly = True
            DglBill.AllowUserToAddRows = False
            'DglPayment.DefaultCellStyle.WrapMode = DataGridViewTriState.True


            DglBill.Columns(ColBill_DocID).Visible = False
            DglBill.Columns(ColBill_Sr).Visible = False
            DglBill.Columns(ColBill_Amount).Visible = False
            DglBill.Columns(ColBill_AdjAmount).Visible = False
            DglBill.Columns(ColBill_CostCenter).Width = 80
            DglBill.Columns(ColBill_Party).Width = 200
            DglBill.Columns(ColBill_BillNo).Width = 105
            DglBill.Columns(ColBill_BalAmount).Width = 75

            DglPayment.EnableHeadersVisualStyles = False
            DglPayment.AgSkipReadOnlyColumns = True
            DglPayment.ColumnHeadersHeight = 35
            DglPayment.ReadOnly = True
            DglPayment.AllowUserToAddRows = False
            'DglPI.DefaultCellStyle.WrapMode = DataGridViewTriState.True


            DglPayment.Columns(ColPA_DocID).Visible = False
            DglPayment.Columns(ColPA_Sr).Visible = False
            DglPayment.Columns(ColPA_Amount).Visible = False
            DglPayment.Columns(ColPA_AdjAmount).Visible = False
            DglPayment.Columns(ColPA_CostCenter).Width = 80
            DglPayment.Columns(ColPA_Party).Width = 185
            DglPayment.Columns(ColPA_PaymentNo).Width = 105
            DglPayment.Columns(ColPA_BalAmount).Width = 80

            AgCL.AddAgButtonColumn(DglPayment, ColPA_Adj, 40, ColPA_Adj, True, False)
            DglPayment.Columns(ColPA_Adj).DisplayIndex = 0
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FrmSaleInvoiceAdj_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        AgL.WinSetting(Me, 650, 1020, 0, 0)

        AgL.GridDesign(DglBill)
        AgL.GridDesign(DglPayment)
        AgL.GridDesign(DglAdj)
        AgL.AddAgDataGrid(DglBill, Panel1)
        AgL.AddAgDataGrid(DglPayment, Panel2)
        AgL.AddAgDataGrid(DglAdj, Panel3)

        MoveRec()
        Ini_Grid()
    End Sub

    Private Sub MoveRec()
        Try

            Dim mQry As String
            mQry = "SELECT H.DocId, H.V_SNo AS Sr, CM.Name AS [Cost Center], SG.Name AS Party, H.V_Type +  '-' + H.RecId AS [Bill No], " & _
                    " IFNull(H.AmtCr, 0) - IFNull(VAdj.AdjAmount, 0) AS [Amount] ,0 AS [Adj Amount], 0 as [Bal Amount]  " & _
                    " FROM Ledger H  " & _
                    " LEFT JOIN CostCenterMast CM  ON CM.Code = H.CostCenter  " & _
                    " LEFT JOIN SubGroup SG ON SG.SubCode = H.SubCode " & _
                    " Left Join " & _
                    " ( " & _
                    " SELECT A.Adj_DocID, A.Adj_V_SNo, sum(A.Amount) AS AdjAmount " & _
                    " FROM LedgerAdj A  " & _
                    " GROUP BY A.Adj_DocID, A.Adj_V_SNo " & _
                    " ) AS VAdj ON VAdj.Adj_DocID = H.DocID AND VAdj.Adj_V_SNo = H.V_SNo " & _
                    " WHERE  H.Site_Code = '" & AgL.PubSiteCode & "' And H.DivCode = '" & AgL.PubDivCode & "'  " & _
                    " AND H.AmtCr > 0 " & _
                    " AND IFNull(H.AmtCr, 0) - IFNull(VAdj.AdjAmount, 0) > 0  " & _
                    " Order By CM.Name, SG.Name, H.V_Date, H.DocID "
            'AND H.SubCode  = 'M100002194' 

            DtBill = AgL.FillData(mQry, AgL.GCn).Tables(0)
            DtBill.Columns("Bal Amount").Expression = "Amount - [Adj Amount]"
            DglBill.DataSource = DtBill

            mQry = "SELECT H.DocId, H.V_SNo AS Sr, CM.Name AS [Cost Center], SG.Name AS Party, H.V_Type +  '-' + H.RecId AS [Payment No], " & _
                     " IFNull(H.AmtDr, 0) - IFNull(VAdj.AdjAmount, 0) AS [Amount] ,0 AS [Adj Amount], 0 as [Bal Amount]  " & _
                     " FROM Ledger H  " & _
                     " LEFT JOIN CostCenterMast CM  ON CM.Code = H.CostCenter  " & _
                     " LEFT JOIN SubGroup SG ON SG.SubCode = H.SubCode " & _
                     " Left Join " & _
                     " ( " & _
                     " SELECT A.Vr_DocId, A.Vr_V_SNo, sum(A.Amount) AS AdjAmount " & _
                     " FROM LedgerAdj A  " & _
                     " GROUP BY A.Vr_DocId, A.Vr_V_SNo " & _
                     " ) AS VAdj ON VAdj.Vr_DocId = H.DocID AND VAdj.Vr_V_SNo = H.V_SNo " & _
                     " WHERE  H.Site_Code = '" & AgL.PubSiteCode & "' And H.DivCode = '" & AgL.PubDivCode & "'  " & _
                     " AND H.AmtDr > 0 " & _
                     " AND IFNull(H.AmtDr, 0) - IFNull(VAdj.AdjAmount, 0) > 0  " & _
                     " Order By CM.Name, SG.Name, H.V_Date, H.DocID "

            DtPayment = AgL.FillData(mQry, AgL.GCn).Tables(0)
            DtPayment.Columns("Bal Amount").Expression = "Amount - [Adj Amount]"
            DglPayment.DataSource = DtPayment



            mQry = " Declare @TblTemp AS Table(BillDocID Varchar(21), BillSr INT, PaymentDocID Varchar(21), PaymentSr INT, AdjAmount FLOAT)"
            mQry += " Select * from @TblTemp "
            DtAdj = AgL.FillData(mQry, AgL.GCn).Tables(0)
            DglAdj.DataSource = DtAdj
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub DglPI_CellContentClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DglPayment.CellContentClick
        Dim DrDtAdj As DataRow = Nothing
        Select Case DglPayment.Columns(DglPayment.CurrentCell.ColumnIndex).Name
            Case ColPA_Adj
                If Val(DglBill.Item(ColBill_BalAmount, DglBill.CurrentCell.RowIndex).Value) <= 0 Then Exit Sub
                If DglBill.SelectedRows IsNot Nothing Then
                    If DglBill.Item(ColBill_CostCenter, DglBill.CurrentCell.RowIndex).Value = DglPayment.Item(ColPA_CostCenter, DglPayment.CurrentCell.RowIndex).Value And DglBill.Item(ColBill_Party, DglBill.CurrentCell.RowIndex).Value = DglPayment.Item(ColPA_Party, DglPayment.CurrentCell.RowIndex).Value Then
                        If Val(DglBill.Item(ColBill_BalAmount, DglBill.CurrentCell.RowIndex).Value) <= Val(DglPayment.Item(ColPA_BalAmount, DglPayment.CurrentCell.RowIndex).Value) Then
                            DrDtAdj = DtAdj.NewRow
                            DrDtAdj("BillDocID") = DglBill.Item(ColBill_DocID, DglBill.CurrentCell.RowIndex).Value
                            DrDtAdj("BillSr") = DglBill.Item(ColBill_Sr, DglBill.CurrentCell.RowIndex).Value
                            DrDtAdj("PaymentDocID") = DglPayment.Item(ColPA_DocID, DglPayment.CurrentCell.RowIndex).Value
                            DrDtAdj("PaymentSr") = DglPayment.Item(ColPA_Sr, DglPayment.CurrentCell.RowIndex).Value
                            DrDtAdj("AdjAmount") = DglBill.Item(ColBill_BalAmount, DglBill.CurrentCell.RowIndex).Value

                            DtAdj.Rows.Add(DrDtAdj)

                            DglBill.Item(ColBill_AdjAmount, DglBill.CurrentCell.RowIndex).Value = Val(DglBill.Item(ColBill_AdjAmount, DglBill.CurrentCell.RowIndex).Value) + Val(DglBill.Item(ColBill_BalAmount, DglBill.CurrentCell.RowIndex).Value)
                            DglPayment.Item(ColPA_AdjAmount, DglPayment.CurrentCell.RowIndex).Value = Val(DglPayment.Item(ColPA_AdjAmount, DglPayment.CurrentCell.RowIndex).Value) + Val(DglBill.Item(ColBill_BalAmount, DglBill.CurrentCell.RowIndex).Value)

                            DtPayment.AcceptChanges()
                            DtBill.AcceptChanges()
                        ElseIf Val(DglBill.Item(ColBill_BalAmount, DglBill.CurrentCell.RowIndex).Value) > Val(DglPayment.Item(ColPA_BalAmount, DglPayment.CurrentCell.RowIndex).Value) Then
                            DrDtAdj = DtAdj.NewRow
                            DrDtAdj("BillDocID") = DglBill.Item(ColBill_DocID, DglBill.CurrentCell.RowIndex).Value
                            DrDtAdj("BillSr") = DglBill.Item(ColBill_Sr, DglBill.CurrentCell.RowIndex).Value
                            DrDtAdj("PaymentDocID") = DglPayment.Item(ColPA_DocID, DglPayment.CurrentCell.RowIndex).Value
                            DrDtAdj("PaymentSr") = DglPayment.Item(ColPA_Sr, DglPayment.CurrentCell.RowIndex).Value
                            DrDtAdj("AdjAmount") = DglPayment.Item(ColPA_BalAmount, DglPayment.CurrentCell.RowIndex).Value

                            DtAdj.Rows.Add(DrDtAdj)

                            DglBill.Item(ColBill_AdjAmount, DglBill.CurrentCell.RowIndex).Value = Val(DglBill.Item(ColBill_AdjAmount, DglBill.CurrentCell.RowIndex).Value) + Val(DglPayment.Item(ColPA_BalAmount, DglPayment.CurrentCell.RowIndex).Value)
                            DglPayment.Item(ColPA_AdjAmount, DglPayment.CurrentCell.RowIndex).Value = Val(DglPayment.Item(ColPA_AdjAmount, DglPayment.CurrentCell.RowIndex).Value) + Val(DglPayment.Item(ColPA_BalAmount, DglPayment.CurrentCell.RowIndex).Value)

                            DtPayment.AcceptChanges()
                            DtBill.AcceptChanges()
                        End If
                    Else
                        MsgBox("Items of stock out and stock in doesn't match")
                    End If
                Else
                    MsgBox("Select any row in stock out")
                End If
        End Select
    End Sub

    Private Sub DglBill_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DglBill.CellEnter
        Try
            If DtPayment Is Nothing Then Exit Sub
            If DglBill.CurrentCell Is Nothing Then Exit Sub

            DtPayment.DefaultView.RowFilter = ""
            DtPayment.DefaultView.RowFilter = " [Cost Center] = '" & DglBill.Item(ColBill_CostCenter, DglBill.CurrentCell.RowIndex).Value & "' AND Party = '" & DglBill.Item(ColBill_Party, DglBill.CurrentCell.RowIndex).Value & "' and [Bal Amount]>0 "
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Sub Fill()
        Try
            Dim DrDtAdj As DataRow
            Dim intBillRowIndex As Integer
            Dim intPARowIndex As Integer
            Dim dblAdjQty As Double

            Dim objProgressbar As New AgLibrary.FrmProgressBar
            objProgressbar.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog


            For intBillRowIndex = 0 To DglBill.Rows.Count - 1
                If Val(DglBill.Item(ColBill_BalAmount, intBillRowIndex).Value) > 0 Then
                    DtPayment.DefaultView.RowFilter = Nothing
                    DtPayment.DefaultView.RowFilter = " [Cost Center] = '" & DglBill.Item(ColBill_CostCenter, intBillRowIndex).Value & "' AND Party = '" & DglBill.Item(ColBill_Party, intBillRowIndex).Value & "' and [Bal Amount]>0 "

                    For intPARowIndex = 0 To DtPayment.DefaultView.Count - 1
                        dblAdjQty = 0
                        DtPayment.DefaultView.RowFilter = Nothing
                        DtPayment.DefaultView.RowFilter = " [Cost Center] = '" & DglBill.Item(ColBill_CostCenter, intBillRowIndex).Value & "' AND Party = '" & DglBill.Item(ColBill_Party, intBillRowIndex).Value & "' and [Bal Amount]>0 "

                        If Val(DglBill.Item(ColBill_BalAmount, intBillRowIndex).Value) <= 0 Then Continue For
                        If DglBill.Item(ColBill_CostCenter, intBillRowIndex).Value = DglPayment.Item(ColPA_CostCenter, 0).Value And DglBill.Item(ColBill_Party, intBillRowIndex).Value = DglPayment.Item(ColPA_Party, 0).Value Then
                            If Val(DglBill.Item(ColBill_BalAmount, intBillRowIndex).Value) <= Val(DglPayment.Item(ColPA_BalAmount, 0).Value) Then
                                dblAdjQty = Val(DglBill.Item(ColBill_BalAmount, intBillRowIndex).Value)
                                DrDtAdj = DtAdj.NewRow
                                DrDtAdj("BillDocID") = DglBill.Item(ColBill_DocID, intBillRowIndex).Value
                                DrDtAdj("BillSr") = DglBill.Item(ColBill_Sr, intBillRowIndex).Value
                                DrDtAdj("PaymentDocID") = DglPayment.Item(ColPA_DocID, 0).Value
                                DrDtAdj("PaymentSr") = DglPayment.Item(ColPA_Sr, 0).Value
                                DrDtAdj("AdjAmount") = dblAdjQty

                                DtAdj.Rows.Add(DrDtAdj)

                                DglBill.Item(ColBill_AdjAmount, intBillRowIndex).Value = Val(DglBill.Item(ColBill_AdjAmount, intBillRowIndex).Value) + dblAdjQty
                                DglPayment.Item(ColPA_AdjAmount, 0).Value = Val(DglPayment.Item(ColPA_AdjAmount, 0).Value) + dblAdjQty

                                DtBill.AcceptChanges()
                                DtPayment.AcceptChanges()
                            ElseIf Val(DglBill.Item(ColBill_BalAmount, intBillRowIndex).Value) > Val(DglPayment.Item(ColPA_BalAmount, 0).Value) Then
                                dblAdjQty = Val(DglPayment.Item(ColPA_BalAmount, 0).Value)
                                DrDtAdj = DtAdj.NewRow
                                DrDtAdj("BillDocID") = DglBill.Item(ColBill_DocID, intBillRowIndex).Value
                                DrDtAdj("BillSr") = DglBill.Item(ColBill_Sr, intBillRowIndex).Value
                                DrDtAdj("PaymentDocID") = DglPayment.Item(ColPA_DocID, 0).Value
                                DrDtAdj("PaymentSr") = DglPayment.Item(ColPA_Sr, 0).Value
                                DrDtAdj("AdjAmount") = dblAdjQty

                                DtAdj.Rows.Add(DrDtAdj)

                                DglBill.Item(ColBill_AdjAmount, intBillRowIndex).Value = Val(DglBill.Item(ColBill_AdjAmount, intBillRowIndex).Value) + dblAdjQty
                                DglPayment.Item(ColPA_AdjAmount, 0).Value = Val(DglPayment.Item(ColPA_AdjAmount, 0).Value) + dblAdjQty

                                DtBill.AcceptChanges()
                                DtPayment.AcceptChanges()
                            End If
                        Else
                            MsgBox("Items of stock out and stock in doesn't match")
                        End If
                    Next
                End If

                objProgressbar.Show()
                objProgressbar.Text = "Adjusting : " + DglBill.Rows.Count.ToString + " \ " + (intBillRowIndex + 1).ToString
                objProgressbar.Refresh()

                DtAdj.AcceptChanges()
                'Threading.Thread.Sleep(100)
            Next


            objProgressbar.Dispose()

            If DglBill.SelectedRows IsNot Nothing Then
            Else
                MsgBox("Select any row in stock out")
            End If
        Catch Ex As Exception
            MsgBox(Ex.Message)
        End Try

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'trdFill = New Threading.Thread(AddressOf Fill)
        'trdFill.IsBackground = True
        'trdFill.Start()
        Fill()
        'DtSO.AcceptChanges()
        'DtPayment.AcceptChanges()
        'DtAdj.AcceptChanges()
    End Sub

    Private Sub BtnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSave.Click
        'trdSave = New Threading.Thread(AddressOf UpdateDb)
        'trdSave.IsBackground = True
        'trdSave.Start()
        UpdateDb()
        Me.Dispose()
    End Sub

    Private Sub BtnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCancel.Click
        Me.Dispose()
    End Sub

    Private Sub UpdateDb()
        Dim i As Integer, mQry As String

        Dim objProgressbar As New AgLibrary.FrmProgressBar
        objProgressbar.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedDialog


        AgL.ECmd = AgL.GCn.CreateCommand
        AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
        AgL.ECmd.Transaction = AgL.ETrans

        For i = 0 To DtAdj.Rows.Count - 1
            mQry = "INSERT INTO LedgerAdj(	Vr_DocId,	Vr_V_SNo,	Adj_DocID,	Adj_V_SNo,	Amount,	Site_Code,	Adj_Type	)" & _
                    " VALUES 	(" & AgL.Chk_Text(DtAdj.Rows(i)("PaymentDocID")) & ",	" & AgL.Chk_Text(DtAdj.Rows(i)("PaymentSr")) & ", " & _
                    " " & AgL.Chk_Text(DtAdj.Rows(i)("BillDocID")) & ",	" & AgL.Chk_Text(DtAdj.Rows(i)("BillSr")) & ", " & _
                    " " & AgL.Chk_Text(DtAdj.Rows(i)("AdjAmount")) & ",	" & AgL.Chk_Text(AgL.PubSiteCode) & ",	'Payment Adjustment'	) "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

            LblStatus.Text = "Saving : " + DtAdj.Rows.Count.ToString + " \ " + (i + 1).ToString

            If Not objProgressbar.Visible Then objProgressbar.Show()
            objProgressbar.Text = "Saving : " + DtAdj.Rows.Count.ToString + " \ " + (i + 1).ToString
            objProgressbar.Refresh()

            'Threading.Thread.Sleep(100)
        Next

        AgL.ETrans.Commit()
        objProgressbar.Dispose()

        'mQry = "Select Count(*) from StockAdj Where StockInDocId = StockOutDocID"
        'If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar > 0 Then
        '    AgL.Dman_ExecuteNonQry("Delete from stockadj Where StockInDocId = StockOutDocID", AgL.GCn)
        '    MsgBox("Adjustment is not completed successfully. Please do adjustment again.")
        '    Me.Dispose()
        'End If
    End Sub

End Class
