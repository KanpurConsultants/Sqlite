Public Class FrmJobOrderMaterialIssue
    Dim mQry As String = ""
    Public mOkButtonPressed As Boolean

    Public Const ColSNo As String = "S.No."
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Public Const Col1LotNo As String = "Lot No"
    Public Const Col1Item As String = "Item"
    Public Const Col1FromProcess As String = "From Process"
    Public Const Col1Dimension1 As String = "Dimension1"
    Public Const Col1Dimension2 As String = "Dimension2"
    Public Const Col1Qty As String = "Qty"
    Public Const Col1Unit As String = "Unit"
    Public Const Col1Rate As String = "Rate"
    Public Const Col1Amount As String = "Amount"
    Dim mDate As String = ""
    Dim mGodown As String = ""
    Dim mInternalCode As String = ""

    Public Sub New(ByVal V_Date As String, ByVal Godown As String, ByVal Internalcode As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
        mDate = V_Date
        mInternalCode = Internalcode
        mGodown = Godown
    End Sub

    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, 0)
    End Sub

    Public Sub IniGrid()
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 35, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl1, Col1LotNo, 250, 0, Col1LotNo, True, False, False)
            .AddAgTextColumn(Dgl1, Col1Item, 180, 20, Col1Item, True, False)
            .AddAgTextColumn(Dgl1, Col1FromProcess, 100, 20, Col1FromProcess, True, False)
            .AddAgNumberColumn(Dgl1, Col1Qty, 80, 5, 4, False, Col1Qty, True, False, True)
            .AddAgTextColumn(Dgl1, Col1Unit, 70, 20, Col1Unit, True, True)
            .AddAgTextColumn(Dgl1, Col1Dimension1, 100, 0, AgTemplate.ClsMain.FGetDimension1Caption(), True, False)
            .AddAgTextColumn(Dgl1, Col1Dimension2, 100, 0, AgTemplate.ClsMain.FGetDimension2Caption(), True, False)
            .AddAgNumberColumn(Dgl1, Col1Rate, 80, 5, 2, False, Col1Rate, True, False, True)
            .AddAgNumberColumn(Dgl1, Col1Amount, 80, 5, 2, False, Col1Amount, True, False, True)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 35
        Dgl1.AgSkipReadOnlyColumns = True
    End Sub

    Function FData_Validation() As Boolean
        Dim I As Integer

        For I = 0 To Dgl1.Rows.Count - 1

        Next
        FData_Validation = True
    End Function

    Sub KeyPress_Form(ByVal Sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        If e.KeyChar = Chr(Keys.Escape) Then
            If AgL.StrCmp(CType(Me.Owner, FrmJobOrder).Topctrl1.Mode, "Browse") Then Me.Close() : Exit Sub
            Me.Close()
        End If
    End Sub

    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            AgL.GridDesign(Dgl1)
            Calculation()
            If AgL.StrCmp(CType(Me.Owner, FrmJobOrder).Topctrl1.Mode, "Browse") Then
                Dgl1.ReadOnly = True
            Else
                Dgl1.ReadOnly = False
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub Calculation()
        Dim I As Integer
        LblTotalQty.Text = 0
        LblTotalAmountValue.Text = 0
        For I = 0 To Dgl1.RowCount - 1
            If Val(Dgl1.Item(Col1Qty, I).Value) <> 0 Then
                Dgl1.Item(Col1Amount, I).Value = Val(Dgl1.Item(Col1Qty, I).Value) * Val(Dgl1.Item(Col1Rate, I).Value)
                LblTotalQty.Text = Val(LblTotalQty.Text) + Val(Dgl1.Item(Col1Qty, I).Value)
                LblTotalAmountValue.Text = Val(LblTotalAmountValue.Text) + Val(Dgl1.Item(Col1Amount, I).Value)
            End If
        Next
    End Sub

    Private Sub DGL1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.KeyDown
        If e.Control And e.KeyCode = Keys.D Then
            sender.CurrentRow.Selected = True
        End If
        If e.Control Or e.Shift Or e.Alt Then Exit Sub
    End Sub

    Private Sub DGL1_EditingControl_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Dgl1.EditingControl_Validating
        Try
            If Dgl1.Item(Dgl1.CurrentCell.ColumnIndex, Dgl1.CurrentCell.RowIndex).Value Is Nothing Then Dgl1.Item(Dgl1.CurrentCell.ColumnIndex, Dgl1.CurrentCell.RowIndex).Value = ""
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Item
                    Validating_Item(Dgl1.Item(Col1Item, Dgl1.CurrentCell.RowIndex).Tag, Dgl1.CurrentCell.RowIndex)

                Case Col1LotNo
                    Validating_LotNo(Dgl1.Item(Col1Item, Dgl1.CurrentCell.RowIndex).Tag, Dgl1.CurrentCell.RowIndex)
            End Select
            Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Validating_Item(ByVal Code As String, ByVal mRow As Integer)
        Dim DrTemp As DataRow() = Nothing
        Dim DtTemp As DataTable = Nothing
        Try
            If Dgl1.Item(Col1Item, mRow).Value.ToString.Trim = "" Or Dgl1.AgSelectedValue(Col1Item, mRow).ToString.Trim = "" Then
                Dgl1.Item(Col1FromProcess, mRow).Value = ""
                Dgl1.Item(Col1FromProcess, mRow).Tag = ""
                Dgl1.Item(Col1Dimension1, mRow).Value = ""
                Dgl1.Item(Col1Dimension2, mRow).Value = ""
                Dgl1.Item(Col1Dimension1, mRow).Tag = ""
                Dgl1.Item(Col1Dimension2, mRow).Tag = ""
                Dgl1.Item(Col1Qty, mRow).Value = 0
                Dgl1.Item(Col1Rate, mRow).Value = 0
                Dgl1.Item(Col1Unit, mRow).Value = ""
                Dgl1.Item(Col1LotNo, mRow).Value = ""
            Else
                If Dgl1.AgDataRow IsNot Nothing Then
                    Dgl1.Item(Col1Unit, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("Unit").Value)
                    Dgl1.Item(Col1FromProcess, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("Process").Value)
                    Dgl1.Item(Col1LotNo, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("LotNo").Value)
                    Dgl1.Item(Col1FromProcess, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("ProcessCode").Value)
                    Dgl1.Item(Col1Dimension1, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("Dimension1").Value)
                    Dgl1.Item(Col1Dimension1, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells(AgTemplate.ClsMain.FGetDimension1Caption()).Value)
                    Dgl1.Item(Col1Dimension2, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("Dimension2").Value)
                    Dgl1.Item(Col1Dimension2, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells(AgTemplate.ClsMain.FGetDimension2Caption()).Value)
                    Dgl1.Item(Col1Qty, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("BalQty").Value)
                    Dgl1.Item(Col1Rate, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("Rate").Value)
                End If
            End If
            Call Calculation()
        Catch ex As Exception
            MsgBox(ex.Message & " On Validating_Item Function ")
        End Try
    End Sub

    Private Sub DGL1_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles Dgl1.RowsAdded, Dgl1.RowsAdded
        sender(ColSNo, e.RowIndex).Value = e.RowIndex + 1
    End Sub

    Private Sub Dgl1_EditingControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.EditingControl_KeyDown
        Try
            If AgL.StrCmp(CType(Me.Owner, FrmJobOrder).Topctrl1.Mode, "Browse") Then Exit Sub
            If Dgl1.CurrentCell Is Nothing Then Exit Sub
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Item
                    'If e.KeyCode <> Keys.Enter Then
                    '    If Dgl1.AgHelpDataSet(Col1Item) Is Nothing Then
                    '        mQry = " Select I.Code, I.Description, I.Unit From Item I  Order By I.Description "
                    '        Dgl1.AgHelpDataSet(Col1Item) = AgL.FillData(mQry, AgL.GCn)
                    '    End If
                    'End If

                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1Item) Is Nothing Then
                            FCreateHelpItem()
                        End If
                    End If

                Case Col1LotNo
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Dgl1.CurrentCell.ColumnIndex) Is Nothing Then
                            FCreateHelpLotNo()
                        End If
                    End If

                Case Col1Dimension1
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1Dimension1) Is Nothing Then
                            mQry = " SELECT Code, Description  FROM Dimension1  "
                            Dgl1.AgHelpDataSet(Col1Dimension1) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If

                Case Col1Dimension2
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1Dimension2) Is Nothing Then
                            mQry = " SELECT Code, Description  FROM Dimension2  "
                            Dgl1.AgHelpDataSet(Col1Dimension2) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If

                Case Col1FromProcess
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1FromProcess) Is Nothing Then
                            mQry = " SELECT P.NCat AS Code, P.Description FROM Process P  "
                            Dgl1.AgHelpDataSet(Col1FromProcess) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If

            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FCreateHelpItem()
        If RbtForStock.Checked Then
            mQry = " SELECT H.Item AS Code, Max(I.Description) AS Item, H.LotNo, " & _
                    " Max(D1.Description) AS " & AgTemplate.ClsMain.FGetDimension1Caption() & ", Max(D2.Description) AS " & AgTemplate.ClsMain.FGetDimension2Caption() & ", Max(P.Description) AS Process, Round(IFNull(sum(H.Qty_Rec),0) - IFNull(sum(H.Qty_Iss),0),4) AS BalQty, " & _
                    " H.process AS ProcessCode, Max(I.Unit) AS Unit, H.Dimension1, H.Dimension2 , Max(H.Rate) AS Rate " & _
                    " FROM Stock H  " & _
                    " LEFT JOIN Item I  ON I.Code = H.Item  " & _
                    " LEFT JOIN Process P ON P.NCat = H.Process " & _
                    " LEFT JOIN Dimension1 D1  ON D1.Code = H.Dimension1 " & _
                    " LEFT JOIN Dimension2 D2  ON D2.Code = H.Dimension2  " & _
                    " WHERE IFNull(H.Item,'') <> ''  " & _
                    " AND H.V_Date <= " & AgL.Chk_Text(mDate) & " AND H.DocID <> " & AgL.Chk_Text(mInternalCode) & " AND H.Godown = " & AgL.Chk_Text(mGodown) & " " & _
                    " And IFNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "'  " & _
                    " GROUP BY H.Item, H.LotNo, H.Process, H.Dimension1, H.Dimension2    " & _
                    " HAVING Round(IFNull(sum(H.Qty_Rec),0) - IFNull(sum(H.Qty_Iss),0),4) > 0 " & _
                    " Order By Max(I.Description) "
            Dgl1.AgHelpDataSet(Col1Item, 4) = AgL.FillData(mQry, AgL.GCn)
        Else
            mQry = " Select I.Code, I.Description AS Item, I.Unit,  NULL AS Dimension1, NULL AS Dimension2, NULL AS Process, NULL AS ProcessCode, NULL AS LotNo," & _
                    " NULL AS " & AgTemplate.ClsMain.FGetDimension1Caption() & ", NULL AS " & AgTemplate.ClsMain.FGetDimension2Caption() & ", 0 AS BalQty, NULL AS Rate " & _
                    " From Item I  Where IFNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' Order By I.Description "
            Dgl1.AgHelpDataSet(Col1Item, 7) = AgL.FillData(mQry, AgL.GCn)
        End If
    End Sub

    Private Sub BtnOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnOk.Click, BtnCancel.Click
        Select Case sender.Name
            Case BtnOk.Name
                mOkButtonPressed = True
                Me.Close()

            Case BtnCancel.Name
                mOkButtonPressed = False
                Me.Close()
        End Select
    End Sub

    Private Sub FCreateHelpLotNo()
        Dim strCond As String = ""
        mQry = " SELECT H.LotNo AS Code, H.LotNo, Max(I.Description) AS Item,  " & _
            " Max(D1.Description) AS " & AgTemplate.ClsMain.FGetDimension1Caption() & ", Max(D2.Description) AS " & AgTemplate.ClsMain.FGetDimension2Caption() & ", Max(P.Description) AS Process, Round(IFNull(sum(H.Qty_Rec),0) - IFNull(sum(H.Qty_Iss),0),4) AS BalQty, " & _
            " H.process AS ProcessCode, Max(I.Unit) AS Unit, H.Dimension1, H.Dimension2 , H.Item AS ItemCode, Max(H.Rate) AS Rate " & _
            " FROM Stock H  " & _
            " LEFT JOIN Item I  ON I.Code = H.Item  " & _
            " LEFT JOIN Process P ON P.NCat = H.Process " & _
            " LEFT JOIN Dimension1 D1  ON D1.Code = H.Dimension1 " & _
            " LEFT JOIN Dimension2 D2  ON D2.Code = H.Dimension2  " & _
            " WHERE IFNull(H.Item,'') <> ''  " & _
            " AND H.V_Date <= " & AgL.Chk_Text(mDate) & " AND H.DocID <> " & AgL.Chk_Text(mInternalCode) & " AND H.Godown = " & AgL.Chk_Text(mGodown) & " " & _
            " And IFNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "'  " & _
            " GROUP BY H.Item, H.LotNo, H.Process, H.Dimension1, H.Dimension2    " & _
            " HAVING Round(IFNull(sum(H.Qty_Rec),0) - IFNull(sum(H.Qty_Iss),0),4) > 0 " & _
            " Order By Max(I.Description) "
        Dgl1.AgHelpDataSet(Col1LotNo, 4) = AgL.FillData(mQry, AgL.GCn)
    End Sub

    Private Sub Validating_LotNo(ByVal Code As String, ByVal mRow As Integer)
        Dim DrTemp As DataRow() = Nothing
        Dim DtTemp As DataTable = Nothing

        Try
            If Dgl1.Item(Col1LotNo, mRow).Value.ToString.Trim = "" Or Dgl1.AgSelectedValue(Col1LotNo, mRow).ToString.Trim = "" Then
                Dgl1.Item(Col1Item, mRow).Value = ""
                Dgl1.Item(Col1Item, mRow).Tag = ""
                Dgl1.Item(Col1FromProcess, mRow).Value = ""
                Dgl1.Item(Col1FromProcess, mRow).Tag = ""
                Dgl1.Item(Col1Dimension1, mRow).Value = ""
                Dgl1.Item(Col1Dimension2, mRow).Value = ""
                Dgl1.Item(Col1Dimension1, mRow).Tag = ""
                Dgl1.Item(Col1Dimension2, mRow).Tag = ""
                Dgl1.Item(Col1Qty, mRow).Value = 0
                Dgl1.Item(Col1Rate, mRow).Value = 0
                Dgl1.Item(Col1Unit, mRow).Value = ""
                Dgl1.Item(Col1LotNo, mRow).Value = ""
            Else
                If Dgl1.AgDataRow IsNot Nothing Then
                    Dgl1.Item(Col1Unit, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("Unit").Value)
                    Dgl1.Item(Col1Item, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("ItemCode").Value)
                    Dgl1.Item(Col1Item, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("Item").Value)
                    Dgl1.Item(Col1FromProcess, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("Process").Value)
                    Dgl1.Item(Col1LotNo, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("LotNo").Value)
                    Dgl1.Item(Col1FromProcess, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("ProcessCode").Value)
                    Dgl1.Item(Col1Dimension1, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("Dimension1").Value)
                    Dgl1.Item(Col1Dimension1, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells(AgTemplate.ClsMain.FGetDimension1Caption()).Value)
                    Dgl1.Item(Col1Dimension2, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("Dimension2").Value)
                    Dgl1.Item(Col1Dimension2, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells(AgTemplate.ClsMain.FGetDimension2Caption()).Value)
                    Dgl1.Item(Col1Qty, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("BalQty").Value)
                    Dgl1.Item(Col1Rate, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("Rate").Value)
                End If
            End If
            Call Calculation()
        Catch ex As Exception
            MsgBox(ex.Message & " On Validating_LotNo Function ")
        End Try
    End Sub

    Private Sub RbtForAllItem_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RbtForAllItem.CheckedChanged, RbtForStock.CheckedChanged
        Dgl1.AgHelpDataSet(Col1Item) = Nothing
    End Sub

End Class