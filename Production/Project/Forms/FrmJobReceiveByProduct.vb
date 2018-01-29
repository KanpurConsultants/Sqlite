Public Class FrmJobReceiveByProduct
    Dim mQry As String = ""
    Public mOkButtonPressed As Boolean

    Public Const ColSNo As String = "S.No."
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Public Const Col1Item As String = "Item"
    Public Const Col1LotNo As String = "Lot No"
    Public Const Col1Qty As String = "Qty"
    Public Const Col1Unit As String = "Unit"
    Public Const Col1JobOrder As String = "Job Order"

    Dim mJobOrderList$ = ""

    Public Property JobOrderList() As String
        Get
            JobOrderList = mJobOrderList
        End Get
        Set(ByVal value As String)
            mJobOrderList = value
        End Set
    End Property

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
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
            .AddAgNumberColumn(Dgl1, Col1Qty, 80, 5, 4, False, Col1Qty, True, False, True)
            .AddAgTextColumn(Dgl1, Col1Unit, 80, 20, Col1Unit, True, True)
            .AddAgTextColumn(Dgl1, Col1JobOrder, 100, 20, Col1JobOrder, True, False)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 35
        Dgl1.AgSkipReadOnlyColumns = True
    End Sub

    Function FData_Validation() As Boolean
        Dim I As Integer

        For I = 0 To Dgl1.Rows.Count - 1
            If Dgl1.Item(Col1Item, I).Value <> "" Then
                If Dgl1.Item(Col1JobOrder, I).Value = "" Then
                    MsgBox("Job Order Is Balnk At Row No " & Dgl1.Item(ColSNo, I).Value & "")
                    FData_Validation = False : Exit Function
                End If
            End If
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
                    Validating_Item(Dgl1.Item(Col1Item, Dgl1.CurrentCell.RowIndex).Tag, Dgl1.CurrentCell.RowIndex)
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
                Dgl1.Item(Col1Unit, mRow).Value = ""
            Else
                If Dgl1.AgDataRow IsNot Nothing Then
                    Dgl1.Item(Col1Unit, mRow).Value = AgL.XNull(Dgl1.AgDataRow.Cells("Unit").Value)
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " On Validating_Item Function ")
        End Try
    End Sub

    Private Sub DGL1_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles Dgl1.RowsAdded, Dgl1.RowsAdded
        sender(ColSNo, e.RowIndex).Value = e.RowIndex + 1
    End Sub

    Private Sub Dgl1_EditingControl_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.EditingControl_KeyDown
        Try
            If AgL.StrCmp(CType(Me.Owner, FrmJobReceive).Topctrl1.Mode, "Browse") Then Exit Sub
            If Dgl1.CurrentCell Is Nothing Then Exit Sub
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Item
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1Item) Is Nothing Then
                            mQry = " Select I.Code, I.Description, I.Unit From Item I  Order By I.Description "
                            Dgl1.AgHelpDataSet(Col1Item) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If

                Case Col1JobOrder
                    If e.KeyCode <> Keys.Enter Then
                        If Dgl1.AgHelpDataSet(Col1JobOrder) Is Nothing Then
                            mQry = " Select DocId As JobOrder, ManualRefNo As JObOrderNo From JObOrder " & _
                                    " Where DocId In (" & mJobOrderList & ")"
                            Dgl1.AgHelpDataSet(Col1JobOrder) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub BtnOk_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnOk.Click, BtnCancel.Click
        Select Case sender.Name
            Case BtnOk.Name
                If FData_Validation() = False Then Exit Sub
                mOkButtonPressed = True
                Me.Close()

            Case BtnCancel.Name
                mOkButtonPressed = False
                Me.Close()
        End Select
    End Sub
End Class