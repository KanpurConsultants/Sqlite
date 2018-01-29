Imports System.Data.SqlClient
Public Class FrmSaleInvoiceChallanList
    Dim mQry As String = ""

    Public WithEvents DGL1 As New AgControls.AgDataGrid
    Public Const COl1Select As String = "Select"
    Public Const COl1SaleChallanType As String = "Challan Type"
    Public Const COl1SaleChallan As String = "Challan No"
    Public Const COl1SaleChallanDate As String = "Challan Date"

    Dim DtMaster As DataTable = Nothing

    Public mOkButtonPressed As Boolean = False

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, 0)
    End Sub

    Public Sub Ini_List()
        Try
            mQry = " Select H.DocId As Code, H.ReferenceNo From SaleChallan H "
            DGL1.AgHelpDataSet(COl1SaleChallan) = AgL.FillData(mQry, AgL.GCn)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub IniGrid()
        ''==============================================================================
        ''================< Member Data Grid >====================================
        ''==============================================================================

        With AgCL
            .AddAgCheckColumn(DGL1, COl1Select, 50, COl1Select, True)
            .AddAgTextColumn(DGL1, COl1SaleChallanType, 150, 0, COl1SaleChallanType, True, True)
            .AddAgTextColumn(DGL1, COl1SaleChallan, 100, 0, COl1SaleChallan, True, True)
            .AddAgDateColumn(DGL1, COl1SaleChallanDate, 100, COl1SaleChallanDate, True, True)
        End With
        AgL.AddAgDataGrid(DGL1, Pnl1)
        DGL1.EnableHeadersVisualStyles = False
        DGL1.ColumnHeadersHeight = 25
        DGL1.AllowUserToAddRows = False
        DGL1.EnableHeadersVisualStyles = False
        DGL1.Anchor = AnchorStyles.Top + AnchorStyles.Bottom + AnchorStyles.Left + AnchorStyles.Right
        DGL1.AgSkipReadOnlyColumns = True
    End Sub

    Private Sub KeyDown_Form(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If Me.ActiveControl IsNot Nothing Then
            If Not (TypeOf (Me.ActiveControl) Is AgControls.AgDataGrid) Then
                If e.KeyCode = Keys.Return Then SendKeys.Send("{Tab}")
            End If
            If e.KeyCode = Keys.Escape Then Me.Close()
        End If
    End Sub

    Sub KeyPress_Form(ByVal Sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        If e.KeyChar = Chr(Keys.Escape) Then Exit Sub
        If Me.ActiveControl Is Nothing Then Exit Sub
        AgL.CheckQuote(e)
    End Sub

    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            AgL.GridDesign(DGL1)
            BtnOk.Anchor = AnchorStyles.Top + AnchorStyles.Bottom + AnchorStyles.Left + AnchorStyles.Right
            BtnCancel.Anchor = AnchorStyles.Top + AnchorStyles.Bottom + AnchorStyles.Left + AnchorStyles.Right
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub BlankText()
        DGL1.RowCount = 1 : DGL1.Rows.Clear()
    End Sub

    Private Function Data_Validation() As Boolean
        Dim I As Integer = 0
        Try
            Data_Validation = True
        Catch ex As Exception
            MsgBox(ex.Message)
            Data_Validation = False
        End Try
    End Function

    Private Sub BtnChargeDuw_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnOk.Click, BtnCancel.Click
        Try
            Select Case sender.Name
                Case BtnOk.Name
                    If Not Data_Validation() Then Exit Sub
                    mOkButtonPressed = True
                    Me.Close()

                Case BtnCancel.Name
                    mOkButtonPressed = False
                    Me.Close()
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Dgl1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DGL1.KeyDown
        If DGL1.Rows.Count = 0 Then Exit Sub
        If e.Control Or e.Shift Or e.Alt Then Exit Sub
        Try
            Select Case sender.Columns(sender.CurrentCell.ColumnIndex).Name
                Case COl1Select
                    If e.KeyCode = Keys.Space Then
                        Try
                            AgL.ProcSetCheckColumnCellValue(sender, sender.Columns(COl1Select).Index)
                        Catch ex As Exception
                        End Try
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Dgl1_CellMouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DGL1.CellMouseUp
        Dim mRowIndex As Integer, mColumnIndex As Integer
        Try
            If DGL1.Rows.Count = 0 Then Exit Sub

            mRowIndex = sender.CurrentCell.RowIndex
            mColumnIndex = sender.CurrentCell.ColumnIndex

            If sender.Item(mColumnIndex, mRowIndex).Value Is Nothing Then sender.Item(mColumnIndex, mRowIndex).Value = ""
            Select Case sender.Columns(sender.CurrentCell.ColumnIndex).Name
                Case COl1Select
                    Try
                        Call AgL.ProcSetCheckColumnCellValue(sender, sender.Columns(COl1Select).Index)
                    Catch ex As Exception
                    End Try
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class