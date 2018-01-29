Public Class FrmJobReceiveBom
    Dim mQry As String = ""
    Public mOkButtonPressed As Boolean

    Public Const ColSNo As String = "S.No."
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Public Const Col1Item As String = "Item"
    Public Const Col1LotNo As String = "Lot No"
    Public Const Col1Dimension1 As String = "Dimension1"
    Public Const Col1Dimension2 As String = "Dimension2"
    Public Const Col1DocQty As String = "DocQty"
    Public Const Col1LossQty As String = "Loss Qty"
    Public Const Col1Qty As String = "Qty"
    Public Const Col1Unit As String = "Unit"
    Dim mJobWorker As String = ""
    Dim mProcess As String = ""
    Dim mDate As String = ""
    Dim mInternalCode As String = ""

    Public Sub New(ByVal JobWorker As String, ByVal Process As String, ByVal V_Date As String, ByVal Internalcode As String)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.

        mJobWorker = JobWorker
        mProcess = Process
        mDate = V_Date
        mInternalCode = Internalcode
    End Sub

    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, 0)
    End Sub

    Public Sub IniGrid()
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 35, 5, ColSNo, True, True, False)
            .AddAgTextColumn(Dgl1, Col1Item, 240, 20, Col1Item, True, False)
            .AddAgTextColumn(Dgl1, Col1LotNo, 100, 20, Col1LotNo, True, False)
            .AddAgTextColumn(Dgl1, Col1Dimension1, 100, 20, AgL.XNull(AgL.PubDtEnviro.Rows(0)("Caption_Dimension1")), True, False)
            .AddAgTextColumn(Dgl1, Col1Dimension2, 100, 20, AgL.XNull(AgL.PubDtEnviro.Rows(0)("Caption_Dimension2")), True, False)
            .AddAgNumberColumn(Dgl1, Col1DocQty, 80, 5, 4, False, Col1DocQty, True, False, True)
            .AddAgNumberColumn(Dgl1, Col1LossQty, 80, 5, 4, False, Col1LossQty, True, False, True)
            .AddAgNumberColumn(Dgl1, Col1Qty, 80, 5, 4, False, Col1Qty, False, False, True)
            .AddAgTextColumn(Dgl1, Col1Unit, 80, 20, Col1Unit, True, True)
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
            If AgL.StrCmp(CType(Me.Owner, FrmJobReceive).Topctrl1.Mode, "Browse") Then Me.Close() : Exit Sub
            Me.Close()
        End If
    End Sub

    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            AgL.GridDesign(Dgl1)
            Calculation()
            If AgL.StrCmp(CType(Me.Owner, FrmJobReceive).Topctrl1.Mode, "Browse") Then
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

        For I = 0 To Dgl1.RowCount - 1
            Dgl1.Item(Col1Qty, I).Value = Val(Dgl1.Item(Col1DocQty, I).Value) + Val(Dgl1.Item(Col1LossQty, I).Value)

            If Val(Dgl1.Item(Col1Qty, I).Value) <> 0 Then
                LblTotalQty.Text = Val(LblTotalQty.Text) + Val(Dgl1.Item(Col1Qty, I).Value)
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
                    Validating_Item(Col1Item, Dgl1.CurrentCell.RowIndex)

                Case Col1Dimension1
                    Validating_Item(Col1Dimension1, Dgl1.CurrentCell.RowIndex)

                Case Col1Dimension2
                    Validating_Item(Col1Dimension2, Dgl1.CurrentCell.RowIndex)

            End Select
            Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    'Private Sub Validating_Item(ByVal Code As String, ByVal mRow As Integer)
    '    Dim DrTemp As DataRow() = Nothing
    '    Dim DtTemp As DataTable = Nothing
    '    Try
    '        If Dgl1.Item(Col1Item, mRow).Value.ToString.Trim = "" Or Dgl1.AgSelectedValue(Col1Item, mRow).ToString.Trim = "" Then
    '            Dgl1.Item(Col1Dimension1, mRow).Value = ""
    '            Dgl1.Item(Col1Dimension2, mRow).Value = ""
    '            Dgl1.Item(Col1Qty, mRow).Value = 0
    '            Dgl1.Item(Col1DocQty, mRow).Value = 0
    '            Dgl1.Item(Col1Unit, mRow).Value = ""
    '            Dgl1.Item(Col1Dimension1, mRow).Tag = ""
    '            Dgl1.Item(Col1Dimension2, mRow).Tag = ""
    '        Else
    '            If Dgl1.AgDataRow IsNot Nothing Then
    '                Dgl1.Item(Col1Dimension1, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("Dimension1").Value)
    '                Dgl1.Item(Col1Dimension1, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells(AgTemplate.ClsMain.FGetDimension1Caption()).Value)
    '                Dgl1.Item(Col1Dimension2, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("Dimension2").Value)
    '                Dgl1.Item(Col1Dimension2, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells(AgTemplate.ClsMain.FGetDimension2Caption()).Value)
    '                Dgl1.Item(Col1Qty, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("BalQty").Value)
    '                Dgl1.Item(Col1DocQty, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("BalQty").Value)
    '                Dgl1.Item(Col1Unit, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("Unit").Value)
    '            End If
    '        End If
    '    Catch ex As Exception
    '        MsgBox(ex.Message & " On Validating_Item Function ")
    '    End Try
    'End Sub

    Private Sub Validating_Item(ByVal ColoumCode As String, ByVal mRow As Integer)
        Dim DrTemp As DataRow() = Nothing
        Dim DtTemp As DataTable = Nothing
        Try
            If Dgl1.Item(ColoumCode, mRow).Value.ToString.Trim = "" Or Dgl1.AgSelectedValue(ColoumCode, mRow).ToString.Trim = "" Then
                Dgl1.Item(Col1Item, mRow).Value = ""
                Dgl1.Item(Col1Dimension1, mRow).Value = ""
                Dgl1.Item(Col1Dimension2, mRow).Value = ""
                Dgl1.Item(Col1Qty, mRow).Value = 0
                Dgl1.Item(Col1DocQty, mRow).Value = 0
                Dgl1.Item(Col1Unit, mRow).Value = ""
                Dgl1.Item(Col1Item, mRow).Tag = ""
                Dgl1.Item(Col1Dimension1, mRow).Tag = ""
                Dgl1.Item(Col1Dimension2, mRow).Tag = ""
                Dgl1.Item(Col1LotNo, mRow).Tag = ""
            Else
                If Dgl1.AgDataRow IsNot Nothing Then
                    Dgl1.Item(Col1Item, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("ItemCode").Value)
                    Dgl1.Item(Col1Item, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("Item").Value)
                    Dgl1.Item(Col1Dimension1, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("Dimension1").Value)
                    Dgl1.Item(Col1Dimension1, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells(AgTemplate.ClsMain.FGetDimension1Caption()).Value)
                    Dgl1.Item(Col1Dimension2, mRow).Tag = AgL.XNull(Dgl1.AgDataRow.Cells("Dimension2").Value)
                    Dgl1.Item(Col1Dimension2, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells(AgTemplate.ClsMain.FGetDimension2Caption()).Value)
                    Dgl1.Item(Col1Qty, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("BalQty").Value)
                    Dgl1.Item(Col1DocQty, mRow).Value = AgL.VNull(Dgl1.AgDataRow.Cells("BalQty").Value)
                    Dgl1.Item(Col1Unit, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("Unit").Value)
                    Dgl1.Item(Col1LotNo, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("LotNo").Value)
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " On Validating_Item Function ")
        End Try
    End Sub

    Private Sub DGL1_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles Dgl1.RowsAdded, Dgl1.RowsAdded
        sender(ColSNo, e.RowIndex).Value = e.RowIndex + 1
    End Sub

    Private Sub Dgl1_CellEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellEnter
        Try
            If Dgl1.CurrentCell Is Nothing Then Exit Sub
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Item
                    If RbtForStock.Checked = True Then
                        If Dgl1.AgHelpDataSet(Col1Item) IsNot Nothing Then
                            Dgl1.AgRowFilter(Dgl1.Columns(Col1Item).Index) = FFilterUsedItems()
                        End If
                    Else
                        Dgl1.AgRowFilter(Dgl1.Columns(Col1Item).Index) = ""
                    End If

                Case Col1Dimension1
                    If RbtForStock.Checked = True Then
                        If Dgl1.AgHelpDataSet(Col1Dimension1) IsNot Nothing Then
                            Dgl1.AgRowFilter(Dgl1.Columns(Col1Dimension1).Index) = FFilterUsedItems()
                        End If
                    Else
                        Dgl1.AgRowFilter(Dgl1.Columns(Col1Dimension1).Index) = ""
                    End If

                Case Col1Dimension2
                    If RbtForStock.Checked = True Then
                        If Dgl1.AgHelpDataSet(Col1Dimension2) IsNot Nothing Then
                            Dgl1.AgRowFilter(Dgl1.Columns(Col1Dimension2).Index) = FFilterUsedItems()
                        End If
                    Else
                        Dgl1.AgRowFilter(Dgl1.Columns(Col1Dimension2).Index) = ""
                    End If

            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function FFilterUsedItems() As String
        Dim I As Integer = 0
        FFilterUsedItems = " 1=1 "

        Try
            With Dgl1
                For I = 0 To .Rows.Count - 1
                    If .Item(Col1Item, I).Value <> "" Then
                        FFilterUsedItems += " And ItemCode +  IFNull(Dimension1,'') +  IFNull(Dimension2,'') <> '" & Dgl1.AgSelectedValue(Col1Item, I) & "' + '" & Dgl1.Item(Col1Dimension1, I).Tag & "' + '" & Dgl1.Item(Col1Dimension2, I).Tag & "'"
                    End If
                Next
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function

    Private Sub Dgl1_EditingControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.EditingControl_KeyDown
        Try
            If AgL.StrCmp(CType(Me.Owner, FrmJobReceive).Topctrl1.Mode, "Browse") Then Exit Sub
            If Dgl1.CurrentCell Is Nothing Then Exit Sub
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Item
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1Item) Is Nothing Then
                            FCreateHelpItem()
                        End If
                    End If

                Case Col1Dimension1
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1Dimension1) Is Nothing Then
                            FCreateHelpDimension1()
                        End If
                    End If

                Case Col1Dimension2
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1Dimension2) Is Nothing Then
                            FCreateHelpDimension2()
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FCreateHelpItem()
        If RbtForStock.Checked Then
            mQry = " SELECT H.Item AS ItemCode, Max(I.Description) AS Item, H.LotNo, " & _
                    " Max(D1.Description) AS " & AgTemplate.ClsMain.FGetDimension1Caption() & ", Max(D2.Description) AS " & AgTemplate.ClsMain.FGetDimension2Caption() & ", Round(IFNull(sum(H.Qty_Rec),0) - IFNull(sum(H.Qty_Iss),0),4) AS BalQty, " & _
                    " Max(I.Unit) AS Unit, H.Dimension1, H.Dimension2 " & _
                    " FROM StockProcess H  " & _
                    " LEFT JOIN Item I  ON I.Code = H.Item  " & _
                    " LEFT JOIN Dimension1 D1  ON D1.Code = H.Dimension1 " & _
                    " LEFT JOIN Dimension2 D2  ON D2.Code = H.Dimension2  " & _
                    " WHERE IFNull(H.SubCode,'') <> ''  " & _
                    " AND H.SubCode = " & AgL.Chk_Text(mJobWorker) & " AND H.DocID <> " & AgL.Chk_Text(mInternalCode) & " " & _
                    " AND H.V_Date <= " & AgL.Chk_Text(mDate) & " AND H.Process = " & AgL.Chk_Text(mProcess) & " " & _
                    " And IFNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "'  " & _
                    " AND H.Site_Code = '" & AgL.PubSiteCode & "' AND H.Div_Code = '" & AgL.PubDivCode & "' " & _
                    " GROUP BY H.SubCode, H.Item, H.LotNo, H.Process, H.Dimension1, H.Dimension2    " & _
                    " HAVING Round(IFNull(sum(H.Qty_Rec),0) - IFNull(sum(H.Qty_Iss),0),4) > 0 " & _
                    " Order By Max(I.Description) "
            Dgl1.AgHelpDataSet(Col1Item, 3) = AgL.FillData(mQry, AgL.GCn)
        Else
            mQry = " Select I.Code AS ItemCode, I.Description AS Item, I.Unit, NULL AS LotNo, NULL AS Dimension1, NULL AS Dimension2, " & _
                    " NULL AS " & AgTemplate.ClsMain.FGetDimension1Caption() & ", NULL AS " & AgTemplate.ClsMain.FGetDimension2Caption() & ", 0 AS BalQty " & _
                    " From Item I  Where IFNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' Order By I.Description "
            Dgl1.AgHelpDataSet(Col1Item, 6) = AgL.FillData(mQry, AgL.GCn)
        End If
    End Sub

    Private Sub FCreateHelpDimension1()
        If RbtForStock.Checked Then
            mQry = " SELECT H.Dimension1, " & _
                    " Max(D1.Description) AS " & AgTemplate.ClsMain.FGetDimension1Caption() & ", Max(D2.Description) AS " & AgTemplate.ClsMain.FGetDimension2Caption() & ", Max(I.Description) AS Item, H.LotNo, Round(IFNull(sum(H.Qty_Rec),0) - IFNull(sum(H.Qty_Iss),0),4) AS BalQty, " & _
                    " Max(I.Unit) AS Unit, H.Item AS ItemCode, H.Dimension2 " & _
                    " FROM StockProcess H  " & _
                    " LEFT JOIN Item I  ON I.Code = H.Item  " & _
                    " LEFT JOIN Dimension1 D1  ON D1.Code = H.Dimension1 " & _
                    " LEFT JOIN Dimension2 D2  ON D2.Code = H.Dimension2  " & _
                    " WHERE IFNull(H.SubCode,'') <> ''  " & _
                    " AND H.SubCode = " & AgL.Chk_Text(mJobWorker) & " AND H.DocID <> " & AgL.Chk_Text(mInternalCode) & " " & _
                    " AND H.V_Date <= " & AgL.Chk_Text(mDate) & " AND H.Process = " & AgL.Chk_Text(mProcess) & " " & _
                    " And IFNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "'  " & _
                    " AND H.Site_Code = '" & AgL.PubSiteCode & "' AND H.Div_Code = '" & AgL.PubDivCode & "' " & _
                    " GROUP BY H.SubCode, H.Item, H.LotNo, H.Process, H.Dimension1, H.Dimension2    " & _
                    " HAVING Round(IFNull(sum(H.Qty_Rec),0) - IFNull(sum(H.Qty_Iss),0),4) > 0 " & _
                    " Order By Max(D1.Description) "
            Dgl1.AgHelpDataSet(Col1Dimension1, 3) = AgL.FillData(mQry, AgL.GCn)
        Else
            mQry = " Select I.Code AS Dimension1, I.Description AS " & AgTemplate.ClsMain.FGetDimension1Caption() & " , NULL AS Unit,  NULL AS ItemCode, NULL AS Dimension2, " & _
                    " NULL AS Item, NULL AS LotNo, NULL AS " & AgTemplate.ClsMain.FGetDimension2Caption() & ", 0 AS BalQty " & _
                    " FROM Dimension1 I "
            Dgl1.AgHelpDataSet(Col1Dimension1, 5) = AgL.FillData(mQry, AgL.GCn)
        End If
    End Sub


    Private Sub FCreateHelpDimension2()
        If RbtForStock.Checked Then
            mQry = " SELECT H.Dimension2, " & _
                    " Max(D2.Description) AS " & AgTemplate.ClsMain.FGetDimension2Caption() & ", Max(D1.Description) AS " & AgTemplate.ClsMain.FGetDimension1Caption() & ", Max(I.Description) AS Item, Round(IFNull(sum(H.Qty_Rec),0) - IFNull(sum(H.Qty_Iss),0),4) AS BalQty, " & _
                    " Max(I.Unit) AS Unit, H.Item AS ItemCode, H.LotNo, H.Dimension1 " & _
                    " FROM StockProcess H  " & _
                    " LEFT JOIN Item I  ON I.Code = H.Item  " & _
                    " LEFT JOIN Dimension1 D1  ON D1.Code = H.Dimension1 " & _
                    " LEFT JOIN Dimension2 D2  ON D2.Code = H.Dimension2  " & _
                    " WHERE IFNull(H.SubCode,'') <> ''  " & _
                    " AND H.SubCode = " & AgL.Chk_Text(mJobWorker) & " AND H.DocID <> " & AgL.Chk_Text(mInternalCode) & " " & _
                    " AND H.V_Date <= " & AgL.Chk_Text(mDate) & " AND H.Process = " & AgL.Chk_Text(mProcess) & " " & _
                    " And IFNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "'  " & _
                    " AND H.Site_Code = '" & AgL.PubSiteCode & "' AND H.Div_Code = '" & AgL.PubDivCode & "' " & _
                    " GROUP BY H.SubCode, H.Item, H.LotNo, H.Process, H.Dimension1, H.Dimension2    " & _
                    " HAVING Round(IFNull(sum(H.Qty_Rec),0) - IFNull(sum(H.Qty_Iss),0),4) > 0 " & _
                    " Order By Max(D2.Description) "
            Dgl1.AgHelpDataSet(Col1Dimension2, 3) = AgL.FillData(mQry, AgL.GCn)
        Else
            mQry = " Select I.Code AS Dimension2, I.Description AS " & AgTemplate.ClsMain.FGetDimension2Caption() & " , NULL AS Unit,  NULL AS ItemCode, NULL AS Dimension1, " & _
                    " NULL AS Item, NULL AS LotNo, NULL AS " & AgTemplate.ClsMain.FGetDimension1Caption() & ", 0 AS BalQty " & _
                    " FROM Dimension2 I "
            Dgl1.AgHelpDataSet(Col1Dimension2, 6) = AgL.FillData(mQry, AgL.GCn)
        End If
    End Sub

    Private Sub BtnOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnOk.Click, BtnCancel.Click
        Select Case sender.Name
            Case BtnOk.Name
                If RbtForStock.Checked Then
                    If FCheckAcsessStockProcess() = True Then
                        mOkButtonPressed = True
                        Me.Close()
                    End If
                Else
                    mOkButtonPressed = True
                    Me.Close()
                End If

            Case BtnCancel.Name
                mOkButtonPressed = False
                Me.Close()
        End Select
    End Sub

    Private Function FCheckAcsessStockProcess() As Boolean
        Dim I As Integer
        Dim DsTemp As DataSet
        Dim msgStr As String = ""
        FCheckAcsessStockProcess = True

        Dim mTempQry As String = ""
        With Dgl1
            For I = 0 To .Rows.Count - 1
                If .Item(Col1Item, I).Value <> "" Then
                    If mTempQry = "" Then
                        mTempQry = " Select " & AgL.Chk_Text(mProcess) & " AS Process, " & AgL.Chk_Text(mJobWorker) & " AS JobWorker, '" & .Item(Col1Item, I).Tag & "' AS Item, '" & .Item(Col1LotNo, I).Value & "' AS LotNo,  '" & .Item(Col1Dimension1, I).Tag & "' AS Dimension1, '" & .Item(Col1Dimension2, I).Tag & "' AS Dimension2, " & .Item(Col1DocQty, I).Value & " AS Qty "
                    Else
                        mTempQry = mTempQry & "Union All Select " & AgL.Chk_Text(mProcess) & " AS Process, " & AgL.Chk_Text(mJobWorker) & " AS JobWorker, '" & .Item(Col1Item, I).Tag & "' AS Item, '" & .Item(Col1LotNo, I).Value & "' AS LotNo,  '" & .Item(Col1Dimension1, I).Tag & "' AS Dimension1, '" & .Item(Col1Dimension2, I).Tag & "' AS Dimension2, " & .Item(Col1DocQty, I).Value & " AS Qty "
                    End If
                End If
            Next
        End With

        mQry = "SELECT P.Description AS Process, SG.Name AS JobWorker, VRec.Item, I.Description AS ItemName, D1.Description AS D1Desc, D2.Description AS D2Desc, VRec.LotNo, IFNull(VStock.BalStockQty,0) AS BalQty " & _
                " FROM " & _
                " ( " & _
                " SELECT V1.Process, V1.JobWorker, V1.Item, V1.LotNo, V1.Dimension1, V1.Dimension2, sum(V1.Qty) AS RecQty  " & _
                " FROM ( " & mTempQry & " ) V1 " & _
                " GROUP BY V1.Process, V1.JobWorker, V1.Item, V1.LotNo, V1.Dimension1, V1.Dimension2 " & _
                " )VRec " & _
                " Left Join Process P on P.NCat = VRec.Process " & _
                " Left Join SubGroup SG on SG.SubCode = VRec.JobWorker " & _
                " Left Join Item I on I.Code = VRec.Item   " & _
                " Left JOIN Dimension1 D1 on D1.Code = VRec.Dimension1  " & _
                " Left Join Dimension2 D2 on D2.Code = VRec.Dimension2  " & _
                " LEFT JOIN  " & _
                " ( " & _
                " SELECT P.Process, P.SubCode AS JobWorker, P.Item, P.LotNo, P.Dimension1, P.Dimension2, Round(IFNull(Sum(P.Qty_Rec),0) - IFNull(Sum(P.Qty_Iss),0),4)  AS BalStockQty " & _
                " FROM StockProcess P " & _
                " WHERE P.SubCode = " & AgL.Chk_Text(mJobWorker) & " AND P.Process = " & AgL.Chk_Text(mProcess) & " " & _
                " AND P.DocID <> " & AgL.Chk_Text(mInternalCode) & " " & _
                " GROUP BY P.Process, P.SubCode, P.Item, P.LotNo, P.Dimension1, P.Dimension2   " & _
                " HAVING Round(IFNull(Sum(P.Qty_Rec),0) - IFNull(Sum(P.Qty_Iss),0),4) > 0 " & _
                " ) VStock ON VRec.Process = VStock.Process AND VRec.JobWorker = VStock.JobWorker AND VRec.Item = VStock.Item " & _
                " AND IFNull(VRec.LotNo,'') = IFNull(VStock.LotNo,'') AND IFNull(VRec.Dimension1,'') = IFNull(VStock.Dimension1,'') AND IFNull(VRec.Dimension2,'') = IFNull(VStock.Dimension2,'') " & _
                " WHERE IFNull(VRec.RecQty,0) > IFNull(VStock.BalStockQty,0)  "
        DsTemp = AgL.FillData(mQry, AgL.GCn)

        With DsTemp.Tables(0)
            If .Rows.Count > 0 Then
                For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                    msgStr = msgStr & "Consumption Qty is greater than  " & AgL.VNull(.Rows(I)("BalQty")) & " For Item : " & AgL.XNull(.Rows(I)("ItemName")) & " & " & AgTemplate.ClsMain.FGetDimension1Caption() & " : " & AgL.XNull(.Rows(I)("D1Desc")) & " & " & AgTemplate.ClsMain.FGetDimension2Caption() & " : " & AgL.XNull(.Rows(I)("D2Desc")) & " With Lot No : " & AgL.XNull(.Rows(I)("LotNo")) & vbCrLf
                Next
            End If
        End With

        If msgStr <> "" Then
            If MsgBox(msgStr & "Do you want to continue?", MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                FCheckAcsessStockProcess = False
            Else
                FCheckAcsessStockProcess = True
            End If
        End If

    End Function

    Private Sub RbtForAllItem_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RbtForAllItem.CheckedChanged, RbtForStock.CheckedChanged
        Dgl1.AgHelpDataSet(Col1Item) = Nothing
        Dgl1.AgHelpDataSet(Col1Dimension1) = Nothing
        Dgl1.AgHelpDataSet(Col1Dimension2) = Nothing
    End Sub
End Class