Imports System.Data.SqlClient
Public Class FrmRequisitionApproval
    Dim mQry As String = ""
    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Protected Const ColSNo As String = "S.No."
    Protected Const Col1Select As String = "Select"
    Protected Const Col1ReqNo As String = "Req. No"
    Protected Const Col1ReqSr As String = "Req. Sr"
    Protected Const Col1ReqDate As String = "Req. Date"
    Protected Const Col1ReqBy As String = "Req. By"
    Protected Const Col1Item As String = "Item"
    Protected Const Col1Qty As String = "Qty"
    Protected Const Col1Unit As String = "Unit"
    Protected Const Col1ApprovedQty As String = "Approved Qty"

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        'AgL.FPaintForm(Me, e, 0)
    End Sub

    Public Sub IniGrid()
        Dgl1.ColumnCount = 0
        With AgCL
            .AddAgTextColumn(Dgl1, ColSNo, 40, 5, ColSNo, True, True, False)
            .AddAgCheckColumn(Dgl1, Col1Select, 45, Col1Select, True)
            .AddAgTextColumn(Dgl1, Col1ReqNo, 100, 0, Col1ReqNo, True, True)
            .AddAgNumberColumn(Dgl1, Col1ReqSr, 50, 4, 0, False, Col1ReqSr, False)
            .AddAgDateColumn(Dgl1, Col1ReqDate, 80, Col1ReqDate, True, True)
            .AddAgTextColumn(Dgl1, Col1ReqBy, 150, 0, Col1ReqBy, True, True)
            .AddAgTextColumn(Dgl1, Col1Item, 200, 0, Col1Item, True, True)
            .AddAgNumberColumn(Dgl1, Col1Qty, 70, 8, 4, False, Col1Qty, True, True, True)
            .AddAgTextColumn(Dgl1, Col1Unit, 60, 0, Col1Unit, True, True)
            .AddAgNumberColumn(Dgl1, Col1ApprovedQty, 70, 8, 4, False, Col1ApprovedQty, True, False, True)
        End With
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 35
        Dgl1.AllowUserToAddRows = False
        Dgl1.AgSkipReadOnlyColumns = True
        Dgl1.MultiSelect = True
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

    Private Sub Txt_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtItemCategory.KeyDown, TxtRequisitionBy.KeyDown, TxtItemGroup.KeyDown
        Dim strCond$ = ""
        Try
            If e.KeyCode = Keys.Enter Then Exit Sub
            Select Case sender.Name
                Case TxtItemCategory.Name
                    If TxtItemCategory.AgHelpDataSet Is Nothing Then
                        FHPGD_ItemCategory()
                    End If

                Case TxtItemGroup.Name
                    If TxtItemGroup.AgHelpDataSet Is Nothing Then
                        FHPGD_ItemGroup()
                    End If

                Case TxtRequisitionBy.Name
                    If TxtRequisitionBy.AgHelpDataSet Is Nothing Then
                        FHPGD_RequisitionBy()
                    End If

            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FHPGD_ItemCategory()
        Dim mQry$
        Dim FRH_Multiple As DMHelpGrid.FrmHelpGrid_Multi
        Dim StrRtn As String = ""
        Dim strCond As String = ""

        mQry = " SELECT 'o' AS Tick, Code, Description FROM ItemCategory " & _
                " Order By Description "

        FRH_Multiple = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(AgL.FillData(mQry, AgL.GCn).TABLES(0)), "", 400, 500, , , False)
        FRH_Multiple.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple.FFormatColumn(1, , 0, , False)
        FRH_Multiple.FFormatColumn(2, "Name", 150, DataGridViewContentAlignment.MiddleLeft)

        FRH_Multiple.StartPosition = FormStartPosition.CenterScreen
        FRH_Multiple.ShowDialog()

        If FRH_Multiple.BytBtnValue = 0 Then
            TxtItemCategory.Tag = FRH_Multiple.FFetchData(1, "'", "'", ",", True)
            TxtItemCategory.Text = FRH_Multiple.FFetchData(2, "", "", ",", True)
        End If

        FRH_Multiple = Nothing
    End Sub

    Private Sub FHPGD_ItemGroup()
        Dim mQry$
        Dim FRH_Multiple As DMHelpGrid.FrmHelpGrid_Multi
        Dim StrRtn As String = ""
        Dim strCond As String = ""

        mQry = " SELECT 'o' AS Tick, Code, Description FROM ItemGroup " & _
                " Order By Description "

        FRH_Multiple = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(AgL.FillData(mQry, AgL.GCn).TABLES(0)), "", 400, 500, , , False)
        FRH_Multiple.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple.FFormatColumn(1, , 0, , False)
        FRH_Multiple.FFormatColumn(2, "Name", 150, DataGridViewContentAlignment.MiddleLeft)

        FRH_Multiple.StartPosition = FormStartPosition.CenterScreen
        FRH_Multiple.ShowDialog()

        If FRH_Multiple.BytBtnValue = 0 Then
            TxtItemGroup.Tag = FRH_Multiple.FFetchData(1, "'", "'", ",", True)
            TxtItemGroup.Text = FRH_Multiple.FFetchData(2, "", "", ",", True)
        End If

        FRH_Multiple = Nothing
    End Sub

    Private Sub FHPGD_RequisitionBy()
        Dim mQry$
        Dim FRH_Multiple As DMHelpGrid.FrmHelpGrid_Multi
        Dim StrRtn As String = ""
        Dim strCond As String = ""

        mQry = " SELECT 'o' AS Tick, SG.SubCode AS Code, SG.DispName , SG.ManualCode FROM Subgroup SG WHERE SG.MasterType = '" & ClsMain.MasterType.Employee & "' " & _
                " Order By SG.DispName "

        FRH_Multiple = New DMHelpGrid.FrmHelpGrid_Multi(New DataView(AgL.FillData(mQry, AgL.GCn).TABLES(0)), "", 400, 500, , , False)
        FRH_Multiple.FFormatColumn(0, "Tick", 40, DataGridViewContentAlignment.MiddleCenter, True)
        FRH_Multiple.FFormatColumn(1, , 0, , False)
        FRH_Multiple.FFormatColumn(2, "Name", 150, DataGridViewContentAlignment.MiddleLeft)
        FRH_Multiple.FFormatColumn(3, "Emp Code", 150, DataGridViewContentAlignment.MiddleLeft)

        FRH_Multiple.StartPosition = FormStartPosition.CenterScreen
        FRH_Multiple.ShowDialog()

        If FRH_Multiple.BytBtnValue = 0 Then
            TxtRequisitionBy.Tag = FRH_Multiple.FFetchData(1, "'", "'", ",", True)
            TxtRequisitionBy.Text = FRH_Multiple.FFetchData(2, "", "", ",", True)
        End If

        FRH_Multiple = Nothing
    End Sub

    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            AgL.GridDesign(Dgl1)
            IniGrid()
            DispText()
            BlankText()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub BlankText()
        TxtFromDate.Text = "" : TxtToDate.Text = "" : TxtRequisitionBy.Text = "" : TxtRequisitionBy.Tag = ""
        TxtItemCategory.Text = "" : TxtItemCategory.Tag = "" : TxtItemGroup.Text = "" : TxtItemGroup.Tag = ""
        Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
        TxtApproved.Text = "No"
        TxtToDate.Text = AgL.PubLoginDate
    End Sub

    Private Sub DispText(Optional ByVal Enb As Boolean = False)
        'Coding To Enable/Disable Controls
        ' Dgl1.ReadOnly = True
    End Sub

    Private Sub Dgl1_CellMouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles Dgl1.CellMouseUp
        Dim mRowIndex As Integer, mColumnIndex As Integer
        Try
            If Dgl1.Rows.Count = 0 Then Exit Sub

            mRowIndex = sender.CurrentCell.RowIndex
            mColumnIndex = sender.CurrentCell.ColumnIndex

            If sender.Item(mColumnIndex, mRowIndex).Value Is Nothing Then sender.Item(mColumnIndex, mRowIndex).Value = ""
            Select Case sender.Columns(sender.CurrentCell.ColumnIndex).Name
                Case Col1Select
                    Try
                        Call AgL.ProcSetCheckColumnCellValue(sender, sender.Columns(Col1Select).Index)
                    Catch ex As Exception
                    End Try
            End Select
        Catch ex As Exception
        End Try
    End Sub

    Private Sub DGL1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles DGL1.KeyDown
        If e.Control And e.KeyCode = Keys.D Then
            sender.CurrentRow.Selected = True
        End If
        If e.Control Or e.Shift Or e.Alt Then Exit Sub
        Try
            Select Case sender.Columns(sender.CurrentCell.ColumnIndex).Name
                Case Col1Select
                    If e.KeyCode = Keys.Space Then
                        Try
                            AgL.ProcSetCheckColumnCellValue(sender, sender.Columns(Col1Select).Index)
                        Catch ex As Exception
                        End Try
                    End If
            End Select
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ProcFill()
        Dim I As Integer = 0
        Dim DsTemp As DataSet = Nothing
        Dim mcondStr As String = ""
        Dim VtypeRestriction$ = " AND H.V_Type NOT IN " & _
                " ( Select L.V_Type " & _
                " FROM User_Exclude_VTypeDetail L  " & _
                " WHERE L.UserName = " & AgL.Chk_Text(AgL.PubUserName) & " ) "

        mcondStr = " Where H.Site_Code = '" & AgL.PubSiteCode & "' AND H.Div_Code = '" & AgL.PubDivCode & "' "

        If TxtFromDate.Text <> "" Then
            mcondStr = mcondStr & " AND  H.V_Date >= '" & TxtFromDate.Text & "' "
        End If

        If TxtToDate.Text <> "" Then
            mcondStr = mcondStr & " AND  H.V_Date <= '" & TxtToDate.Text & "' "
        End If

        If TxtItemCategory.Tag <> "" Then
            mcondStr = mcondStr & " AND I.ItemCategory IN ( " & TxtItemCategory.Tag & "  ) "
        End If

        If TxtItemGroup.Tag <> "" Then
            mcondStr = mcondStr & " AND I.ItemGroup IN ( " & TxtItemGroup.Tag & "  ) "
        End If

        If TxtRequisitionBy.Tag <> "" Then
            mcondStr = mcondStr & " AND H.RequisitionBy IN ( " & TxtRequisitionBy.Tag & "  ) "
        End If

        If TxtApproved.Text = "No" Then
            mcondStr = mcondStr & " AND ISNULL(L.ApproveBy,'') = '' "
        Else
            mcondStr = mcondStr & " AND ISNULL(L.ApproveBy,'') <> '' "
        End If

        mcondStr = mcondStr & VtypeRestriction

        Try
            mQry = " SELECT L.DocId, L.Item, L.Qty, L.Unit, L.Sr, H.V_Date, H.ReferenceNo, SG.DispName AS RequisitionBy, " & _
                    " I.Description AS ItemName, Case When ISNULL(L.ApproveQty,0) = 0 then L.Qty Else L.ApproveQty END AS ApproveQty  " & _
                    " FROM RequisitionDetail L " & _
                    " LEFT JOIN Requisition H ON H.DocID = L.DocId  " & _
                    " LEFT JOIN SubGroup SG ON SG.SubCode = H.RequisitionBy  " & _
                    " LEFT JOIN Item I ON I.Code = L.Item " & mcondStr & _
                    " Order By  H.V_Date, H.ReferenceNo "
            DsTemp = AgL.FillData(mQry, AgL.GCn)
            Dgl1.RowCount = 1 : Dgl1.Rows.Clear()

            With DsTemp.Tables(0)
                If .Rows.Count > 0 Then
                    For I = 0 To DsTemp.Tables(0).Rows.Count - 1
                        Dgl1.Rows.Add()
                        Dgl1.Item(ColSNo, I).Value = Dgl1.Rows.Count
                        Dgl1.Item(Col1Select, I).Value = AgLibrary.ClsConstant.StrCheckedValue
                        Dgl1.Item(Col1ReqNo, I).Value = AgL.XNull(.Rows(I)("ReferenceNo"))
                        Dgl1.Item(Col1ReqNo, I).Tag = AgL.XNull(.Rows(I)("DocId"))
                        Dgl1.Item(Col1ReqSr, I).Value = AgL.VNull(.Rows(I)("Sr"))
                        Dgl1.Item(Col1ReqDate, I).Value = AgL.XNull(.Rows(I)("V_Date"))
                        Dgl1.Item(Col1ReqBy, I).Value = AgL.XNull(.Rows(I)("RequisitionBy"))
                        Dgl1.Item(Col1Item, I).Value = AgL.XNull(.Rows(I)("ItemName"))
                        Dgl1.Item(Col1Item, I).Tag = AgL.XNull(.Rows(I)("Item"))
                        Dgl1.Item(Col1Qty, I).Value = AgL.VNull(.Rows(I)("Qty"))
                        Dgl1.Item(Col1ApprovedQty, I).Value = AgL.VNull(.Rows(I)("ApproveQty"))
                        Dgl1.Item(Col1Unit, I).Value = AgL.XNull(.Rows(I)("Unit"))
                    Next
                Else
                    MsgBox("No Records Find !")
                End If
            End With
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub BtnClose_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnClose.Click
        Me.Close()
    End Sub

    Private Sub BtnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSave.Click
        If AgCL.AgIsBlankGrid(Dgl1, Dgl1.Columns(Col1Item).Index) Then Exit Sub

        Dim I As Integer
        For I = 0 To Dgl1.RowCount - 1
            If Dgl1.Item(Col1Item, I).Value <> "" And AgL.StrCmp(Dgl1.Item(Col1Select, I).Value, AgLibrary.ClsConstant.StrCheckedValue) Then
                If Dgl1.Rows(I).DefaultCellStyle.BackColor <> RowLockedColour Then
                    mQry = " UPDATE RequisitionDetail SET ApproveQty = " & Val(Dgl1.Item(Col1ApprovedQty, I).Value) & ", " & _
                            " ApproveBy = " & IIf(Val(Dgl1.Item(Col1ApprovedQty, I).Value) > 0, AgL.Chk_Text(AgL.PubUserName), "NULL") & ", ApproveDate =  " & IIf(Val(Dgl1.Item(Col1ApprovedQty, I).Value) > 0, AgL.Chk_Text(AgL.PubLoginDate), "NULL") & " " & _
                            " Where DocId = " & AgL.Chk_Text(Dgl1.Item(Col1ReqNo, I).Tag) & " " & _
                            " And Sr = " & Dgl1.Item(Col1ReqSr, I).Value & " "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
                    Call AgL.LogTableEntry(Dgl1.Item(Col1ReqNo, I).Tag, Me.Text, "P", AgL.PubMachineName, AgL.PubUserName, AgL.PubLoginDate, AgL.GCn, AgL.ECmd)
                End If
            End If
        Next

        MsgBox("Requisition is Approved !")
        BlankText()
    End Sub

    Private Sub BtnFill_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnFill.Click
        ProcFill()
    End Sub
End Class