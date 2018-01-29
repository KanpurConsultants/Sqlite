Public Class FrmPurchOrderDelivery
    Dim mQry As String = ""
    Public mOkButtonPressed As Boolean

    Public Const ColSNo As String = "S.No."
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Public Const Col1Qty As String = "Qty"
    Public Const Col1Unit As String = "Unit"
    Public Const Col1MeasurePerPcs As String = "Area Per Pcs"
    Public Const Col1MeasureUnit As String = "Area Unit"
    Public Const Col1TotalMeasure As String = "Total Area"
    Public Const Col1DeliveryDate As String = "Delivery Date"
    Public Const Col1DeliveryInstruction As String = "Delivery Instruction"

    Dim mUnit$ = ""
    Dim mMeasurePerPcs As Double = 0
    Dim mMeasureUnit$ = ""
    Dim mEntryMode$ = ""

    Public Property EntryMode() As String
        Get
            EntryMode = mEntryMode
        End Get
        Set(ByVal value As String)
            mEntryMode = value
        End Set
    End Property

    Public Property Unit() As String
        Get
            Unit = mUnit
        End Get
        Set(ByVal value As String)
            mUnit = value
        End Set
    End Property

    Public Property MeasureUnit() As String
        Get
            MeasureUnit = mMeasureUnit
        End Get
        Set(ByVal value As String)
            mMeasureUnit = value
        End Set
    End Property

    Public Property MeasurePerPcs() As Double
        Get
            MeasurePerPcs = mMeasurePerPcs
        End Get
        Set(ByVal value As Double)
            mMeasurePerPcs = value
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
            .AddAgNumberColumn(Dgl1, Col1Qty, 100, 5, 4, False, Col1Qty, True, False, True)
            .AddAgTextColumn(Dgl1, Col1Unit, 80, 20, Col1Unit, True, True)
            .AddAgNumberColumn(Dgl1, Col1MeasurePerPcs, 65, 5, 4, False, Col1MeasurePerPcs, False, True, True)
            .AddAgNumberColumn(Dgl1, Col1TotalMeasure, 65, 5, 4, False, Col1TotalMeasure, True, True, True)
            .AddAgTextColumn(Dgl1, Col1MeasureUnit, 80, 20, Col1MeasureUnit, False, True)
            .AddAgDateColumn(Dgl1, Col1DeliveryDate, 80, Col1DeliveryDate, True, False, False)
            .AddAgTextColumn(Dgl1, Col1DeliveryInstruction, 300, 255, Col1DeliveryInstruction, True, False)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 35
        Dgl1.AgSkipReadOnlyColumns = True
    End Sub

    Function FData_Validation() As Boolean
        Dim I As Integer


        For I = 0 To Dgl1.Rows.Count - 1
            If Dgl1.Item(Col1DeliveryDate, I).Value < LblOrderDate.Text Then
                MsgBox("Delivery date is less than order date at row no. " & I & ". can't continue.")
                Exit Function
            End If
        Next
        FData_Validation = True
    End Function


    Sub KeyPress_Form(ByVal Sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        If e.KeyChar = Chr(Keys.Escape) Then
            If AgL.StrCmp(EntryMode, "Browse") Then Me.Close() : Exit Sub
            If Val(LblTotalQty.Text) > Val(LblQty.Text) Then
                MsgBox("Delivery schedule qty is greater than ordered qty. can't continue.")
                Exit Sub
            Else
                Me.Close()
            End If
        End If
    End Sub

    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            AgL.GridDesign(Dgl1)
            Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub Calculation()
        Dim I As Integer
        Dim bTotalMeasure As Double = 0, bTotalQty As Double = 0

        For I = 0 To Dgl1.RowCount - 1
            If Val(Dgl1.Item(Col1Qty, I).Value) <> 0 Then
                Dgl1.Item(Col1TotalMeasure, I).Value = Val(Dgl1.Item(Col1MeasurePerPcs, I).Value) * Val(Dgl1.Item(Col1Qty, I).Value)
                bTotalQty += Val(Dgl1.Item(Col1Qty, I).Value)
                bTotalMeasure += Val(Dgl1.Item(Col1TotalMeasure, I).Value)
            End If
        Next
        LblTotalQty.Text = bTotalQty
        LblTotalMeasure.Text = bTotalMeasure
    End Sub

    Private Sub DGL1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.KeyDown
        If e.Control And e.KeyCode = Keys.D Then
            sender.CurrentRow.Selected = True
        End If
        If e.Control Or e.Shift Or e.Alt Then Exit Sub
    End Sub

    Private Sub DGL1_EditingControl_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Dgl1.EditingControl_Validating
        Dim DrTemp As DataRow() = Nothing
        Dim mRowIndex As Integer, mColumnIndex As Integer
        Try
            mRowIndex = Dgl1.CurrentCell.RowIndex
            mColumnIndex = Dgl1.CurrentCell.ColumnIndex
            If Dgl1.Item(mColumnIndex, mRowIndex).Value Is Nothing Then Dgl1.Item(mColumnIndex, mRowIndex).Value = ""
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1Qty
                    Call Validate_Qty(mRowIndex)
            End Select
            Calculation()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub Validate_Qty(ByVal mRowIndex As Integer)
        If Dgl1.Item(Col1DeliveryDate, mRowIndex).Value = "" Then Dgl1.Item(Col1DeliveryDate, mRowIndex).Value = LblDeliveryDate.Text
        If Val(Dgl1.Item(Col1Qty, mRowIndex).Value) > Val(LblQty.Text) Then
            MsgBox("Qty can not be greater than total qty")
            Exit Sub
        End If
        Dgl1.Item(Col1Unit, mRowIndex).Value = mUnit
        Dgl1.Item(Col1MeasurePerPcs, mRowIndex).Value = mMeasurePerPcs
        Dgl1.Item(Col1MeasureUnit, mRowIndex).Value = mMeasureUnit
    End Sub

    Private Sub BtnChargeDuw_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnOk.Click
        Dim I As Integer = 0
        Select Case sender.Name
            Case BtnOk.Name
                If AgL.StrCmp(EntryMode, "Browse") Then Me.Close() : Exit Sub
                If Dgl1.ReadOnly = False Then
                    If Val(LblTotalQty.Text) <> Val(LblQty.Text) Then
                        MsgBox("Delivery Schedule Qty Is Not Equal To Ordered Qty.Can't Continue.", MsgBoxStyle.Information)
                        Exit Sub
                    End If

                    For I = 0 To Dgl1.Rows.Count - 1
                        If Val(Dgl1.Item(Col1Qty, I).Value) <> 0 Then
                            If Dgl1.Item(Col1DeliveryDate, I).Value = "" Then
                                MsgBox("Delivery Date Is Blank At Row No " & Dgl1.Item(ColSNo, I).Value & "", MsgBoxStyle.Information)
                                Exit Sub
                            End If
                        End If
                    Next
                End If
                mOkButtonPressed = True
                Me.Close()
        End Select
    End Sub

    Private Sub DGL1_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles Dgl1.RowsAdded, Dgl1.RowsAdded
        'sender(ColSNo, sender.Rows.Count - 1).Value = Trim(sender.Rows.Count)
        sender(ColSNo, e.RowIndex).Value = e.RowIndex + 1
    End Sub
End Class