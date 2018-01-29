Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data.SQLite

Public Class FrmBarCodePrint
    Dim mQry$ = ""
    Public mSearchCode$ = ""

    Private Const Col1Select As String = "Select"
    Private Const Col1ItemUid As String = "Carpet Id"
    Private Const Col1Item As String = "Carpet"
    Private Const Col1ProdOrder As String = "PO No."
    Private Const Col1Design As String = "Design"
    Private Const Col1Colour As String = "Colour"
    Private Const Col1Size As String = "Size"
    Private Const Col1Quality As String = "Quality"
    Private Const Col1RecDocID As String = "Slip No"

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub FrmBarCodePrint_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            IniGrid()
            AgL.GridDesign(Dgl1)
            Dgl1.MultiSelect = True
            Call FFillItem_Uid()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, 0)
    End Sub

    Private Sub IniGrid()
        With AgCL
            .AddAgCheckColumn(Dgl1, Col1Select, 60, Col1Select, True)
            .AddAgTextColumn(Dgl1, Col1ItemUid, 70, 0, Col1ItemUid, True, False)
            .AddAgTextColumn(Dgl1, Col1Item, 100, 0, Col1Item, True, True)
            .AddAgTextColumn(Dgl1, Col1ProdOrder, 100, 0, Col1ProdOrder, True, True)
            .AddAgTextColumn(Dgl1, Col1Design, 150, 0, Col1Design, True, True)
            .AddAgTextColumn(Dgl1, Col1Colour, 80, 0, Col1Colour, True, True)
            .AddAgTextColumn(Dgl1, Col1Size, 100, 0, Col1Size, True, True)
            .AddAgTextColumn(Dgl1, Col1Quality, 100, 0, Col1Quality, True, True)
            .AddAgTextColumn(Dgl1, Col1RecDocID, 120, 0, Col1RecDocID, True, True)
        End With
        Dgl1.EnableHeadersVisualStyles = False
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

    Private Sub FFillItem_Uid()
        Dim DtTemp As DataTable
        Dim I As Integer = 0
        Try
            If mSearchCode.Trim <> "" Then
                mQry = " SELECT Id.Code As Item_UID, Id.Item_Uid As Item_UidDesc , " & _
                        " Id.Item, I.Description As ItemDesc, " & _
                        " Id.ProdOrder, Po.ManualRefNo as ProdOrderNo, " & _
                        " D.Description AS Design, S.Description AS Size, D.Carpet_Colour As Colour, ID.RecDocID, " & _
                        " JIR.ReferenceNo AS SlipNo, JIR.V_Date AS ReceiveDate, Q.Description As Quality  " & _
                        " FROM (Select * From Item_UID Where GenDocId = '" & mSearchCode & "') As Id  " & _
                        " LEFT JOIN Item I ON Id.Item = I.Code " & _
                        " LEFT JOIN ProdOrder Po ON Id.ProdOrder = Po.DocId " & _
                        " LEFT JOIN RUG_CarpetSku C ON Id.Item  = C.Code " & _
                        " LEFT JOIN RUG_Design D ON C.Design = D.Code " & _
                        " LEFT JOIN Rug_Size S ON C.Size = S.Code " & _
                        " LEFT JOIN RUG_Quality Q On D.QualityCode = Q.Code " & _
                        " LEFT JOIN PurchChallan JIR ON JIR.DocID = ID.RecDocID " & _
                        " Where Id.GenDocId ='" & mSearchCode & "' " & _
                        " And CancelDocId Is Null " & _
                        " Order By Convert(BigInt,Id.Item_Uid) "
                DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

                With DtTemp
                    Dgl1.RowCount = 1 : Dgl1.Rows.Clear()
                    For I = 0 To .Rows.Count - 1
                        Dgl1.Rows.Add()
                        Dgl1.Item(Col1Select, I).Value = AgLibrary.ClsConstant.StrCheckedValue
                        Dgl1.Item(Col1ItemUid, I).Tag = AgL.XNull(.Rows(I)("Item_Uid"))
                        Dgl1.Item(Col1ItemUid, I).Value = AgL.XNull(.Rows(I)("Item_UidDesc"))
                        Dgl1.Item(Col1Item, I).Tag = AgL.XNull(.Rows(I)("Item"))
                        Dgl1.Item(Col1Item, I).Value = AgL.XNull(.Rows(I)("ItemDesc"))
                        Dgl1.Item(Col1ProdOrder, I).Tag = AgL.XNull(.Rows(I)("ProdOrder"))
                        Dgl1.Item(Col1ProdOrder, I).Value = AgL.XNull(.Rows(I)("ProdOrderNo"))
                        Dgl1.Item(Col1Design, I).Value = AgL.XNull(.Rows(I)("Design"))
                        Dgl1.Item(Col1Colour, I).Value = AgL.XNull(.Rows(I)("Colour"))
                        Dgl1.Item(Col1Size, I).Value = AgL.XNull(.Rows(I)("Size"))
                        Dgl1.Item(Col1Quality, I).Value = AgL.XNull(.Rows(I)("Quality"))
                        Dgl1.Item(Col1RecDocID, I).Value = AgL.XNull(.Rows(I)("SlipNo"))
                    Next
                End With
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            DtTemp = Nothing
        End Try
    End Sub

    Private Sub BtnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPrint.Click, BtnExit.Click
        Try
            Select Case sender.name
                Case BtnPrint.Name
                    If CheckPrintBarCode(mSearchCode) = False Then Exit Sub
                    'Call PrintReport()
                    Call PrintImageBarCode()

                Case BtnExit.Name
                    Me.Dispose()
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub PrintReport()
        Dim mCrd As New ReportDocument
        Dim ReportView As New AgLibrary.RepView
        Dim strQry As String = "", RepName As String = "", RepTitle As String = ""
        Dim DsRep As DataSet = Nothing
        Dim I As Integer
        Try
            Me.Cursor = Cursors.WaitCursor


            RepName = "RepBarCode" : RepTitle = "Item Barcode"

            If Val(TxtSkipLables.Text) > 0 Then
                For I = 1 To Val(TxtSkipLables.Text)
                    If strQry.Trim <> "" Then strQry = strQry & " UNION ALL "
                    strQry = strQry & " Select null As [Item_Uid], " & _
                            " Null As CarpetSku, Null As Design, Null As Colour, Null As Size, Null As Quality "
                Next
            End If

            With Dgl1
                For I = 0 To .Rows.Count - 1
                    If AgL.StrCmp(.Item(Col1Select, I).Value, AgLibrary.ClsConstant.StrCheckedValue) _
                        And .Item(Col1ItemUid, I).Value <> "" Then
                        If strQry.Trim <> "" Then strQry = strQry & " UNION ALL "
                        strQry = strQry & " Select " & AgL.Chk_Text(.Item(Col1ItemUid, I).Value) & " As [Item_Uid], " & _
                                " " & AgL.Chk_Text(.Item(Col1Item, I).Value) & " As CarpetSku , " & _
                                " " & AgL.Chk_Text(.Item(Col1Design, I).Value) & " As Design , " & _
                                " " & AgL.Chk_Text(.Item(Col1Colour, I).Value) & " As Colour , " & _
                                " " & AgL.Chk_Text(.Item(Col1Size, I).Value) & " As Size, " & _
                                " " & AgL.Chk_Text(.Item(Col1Quality, I).Value) & " As Quality "
                    End If
                Next
            End With

            If strQry.Trim <> "" Then
                DsRep = AgL.FillData(strQry, AgL.GCn)
                AgPL.CreateFieldDefFile1(DsRep, AgL.PubReportPath & "\" & RepName & ".ttx", True)
                mCrd.Load(AgL.PubReportPath & "\" & RepName & ".rpt")
                mCrd.SetDataSource(DsRep.Tables(0))
                CType(ReportView.Controls("CrvReport"), CrystalDecisions.Windows.Forms.CrystalReportViewer).ReportSource = mCrd
                AgPL.Formula_Set(mCrd, RepTitle)
                AgPL.Show_Report(ReportView, "* " & RepTitle & " *", Me.MdiParent)
                Call AgL.LogTableEntry(mSearchCode, Me.Text, "P", AgL.PubMachineName, AgL.PubUserName, AgL.PubLoginDate, AgL.GCn, AgL.ECmd)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            DsRep = Nothing
        End Try
    End Sub

    Private Sub DGL1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Dgl1.KeyDown
        If e.Control And e.KeyCode = Keys.D Then
            sender.CurrentRow.Selected = True
        End If
        If e.Control Or e.Shift Or e.Alt Then Exit Sub
        Try
            Select Case sender.Columns(sender.CurrentCell.ColumnIndex).Name
                Case Col1Select
                    If e.KeyCode = Keys.Space Then
                        AgL.ProcSetCheckColumnCellValue(sender, sender.Columns(sender.Columns(sender.CurrentCell.ColumnIndex).Name).Index)
                    End If
            End Select
        Catch ex As Exception
        End Try
    End Sub

    Private Sub DGL1_CellMouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles Dgl1.CellMouseUp
        Try
            Select Case sender.Columns(sender.CurrentCell.ColumnIndex).Name
                Case Col1Select
                    Call AgL.ProcSetCheckColumnCellValue(sender, sender.CurrentCell.ColumnIndex)
            End Select
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Dgl1_RowsAdded(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles Dgl1.RowsAdded
        Dgl1.Item(Col1Select, e.RowIndex).Value = AgLibrary.ClsConstant.StrCheckedValue
    End Sub

    Private Sub Dgl1_EditingControl_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Dgl1.EditingControl_Validating
        Dim mRowIndex As Integer, mColumnIndex As Integer
        Dim DrTemp As DataRow() = Nothing
        Dim DtTemp As DataTable = Nothing
        Try
            mRowIndex = Dgl1.CurrentCell.RowIndex
            mColumnIndex = Dgl1.CurrentCell.ColumnIndex
            If Dgl1.Item(mColumnIndex, mRowIndex).Value Is Nothing Then Dgl1.Item(mColumnIndex, mRowIndex).Value = ""
            Select Case Dgl1.Columns(Dgl1.CurrentCell.ColumnIndex).Name
                Case Col1ItemUid
                    If Dgl1.Item(Col1ItemUid, mRowIndex).Value <> "" Then
                        mQry = " SELECT Id.Code As Code, Id.Item_UID As CarpetId,  " & _
                                 " Id.Item AS ItemCode, I.Description as ItemDesc , " & _
                                 " D.Description AS Design, S.Description AS Size, D.Carpet_Colour As Colour, Id.RecDocID, " & _
                                 " Id.SubCode, Id.GenDocId, Q.Description As Quality  " & _
                                 " FROM Item_UID Id " & _
                                 " LEFT JOIN RUG_CarpetSku C ON Id.Item  = C.Code " & _
                                 " LEFT JOIN Item I ON C.Code = I.Code " & _
                                 " LEFT JOIN RUG_Design D ON C.Design = D.Code " & _
                                 " LEFT JOIN Rug_Size S ON C.Size = S.Code " & _
                                 " LEFT JOIN RUG_Quality Q On D.QualityCode = Q.Code " & _
                                 " Where Id.Item_Uid = '" & Dgl1.Item(Col1ItemUid, mRowIndex).Value & "'"
                        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

                        If DtTemp.Rows.Count > 0 Then
                            Dgl1.Item(Col1ItemUid, mRowIndex).Tag = AgL.XNull(DtTemp.Rows(0)("Code"))
                            Dgl1.Item(Col1Item, mRowIndex).Value = AgL.XNull(DtTemp.Rows(0)("ItemDesc"))
                            Dgl1.Item(Col1Design, mRowIndex).Value = AgL.XNull(DtTemp.Rows(0)("Design"))
                            Dgl1.Item(Col1Colour, mRowIndex).Value = AgL.XNull(DtTemp.Rows(0)("Colour"))
                            Dgl1.Item(Col1Quality, mRowIndex).Value = AgL.XNull(DtTemp.Rows(0)("Quality"))
                            Dgl1.Item(Col1Size, mRowIndex).Value = AgL.XNull(DtTemp.Rows(0)("Size"))
                        Else
                            MsgBox("Bar Code Is Not Valid", MsgBoxStyle.Information)
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function CheckPrintBarCode(ByVal JobOrder As String) As Boolean
        Dim IsPrintingAgain As Byte = 0

        mQry = "SELECT Count(*) FROM LogTable L  WHERE L.EntryPoint =  '" & Me.Text & "' AND L.DocId = '" & mSearchCode & "'"
        IsPrintingAgain = AgL.VNull(AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar)

        If IsPrintingAgain > 0 Then
            If ChkExcessBomAllowed.Checked Then
                If MsgBox("You Are Printing BarCode Again For This Weaving Order.Do You Want To Continue ?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    CheckPrintBarCode = True
                Else
                    CheckPrintBarCode = False
                End If
            Else
                MsgBox("You Are Printing BarCode Again For This Weaving Order.Permission Denied.", MsgBoxStyle.Exclamation)
                CheckPrintBarCode = False
            End If
        Else
            CheckPrintBarCode = True
        End If
    End Function

    Private Sub PrintImageBarCode()
        Dim DtTemp As DataTable = Nothing
        Dim I As Integer = 0
        Dim bTempTable$ = ""
        Dim StrCondBale As String = ""
        Dim mCrd As New ReportDocument
        Dim ReportView As New AgLibrary.RepView
        Dim DsRep As New DataSet
        Dim RepName As String = "", RepTitle As String = ""

        Try
            RepName = "RepBarCodeImage" : RepTitle = "Item Barcode"
            bTempTable = AgL.GetGUID(AgL.GCn).ToString

            mQry = "CREATE TABLE [#" & bTempTable & "] " & _
                    " (Item_UID nVarChar(100), CarpetSku nVarChar(100), Design nVarChar(100), " & _
                    " Size nVarChar(100), Colour nVarChar(100), Quality nVarChar(100), BarCodeImg Image)"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

            mQry = ""

            If Val(TxtSkipLables.Text) > 0 Then
                For I = 1 To Val(TxtSkipLables.Text)
                    If mQry.Trim <> "" Then mQry = mQry & " UNION ALL "
                    mQry = mQry & " Select null As [Item_Uid], " & _
                            " Null As CarpetSku, Null As Design, Null As Colour, Null As Size, Null As Quality "
                Next
            End If

            With Dgl1
                For I = 0 To .Rows.Count - 1
                    If AgL.StrCmp(.Item(Col1Select, I).Value, AgLibrary.ClsConstant.StrCheckedValue) _
                        And .Item(Col1ItemUid, I).Value <> "" Then
                        If mQry.Trim <> "" Then mQry = mQry & " UNION ALL "
                        mQry = mQry & " Select " & AgL.Chk_Text(.Item(Col1ItemUid, I).Value) & " As [Item_Uid], " & _
                                " " & AgL.Chk_Text(.Item(Col1Item, I).Value) & " As CarpetSku , " & _
                                " " & AgL.Chk_Text(.Item(Col1Design, I).Value) & " As Design , " & _
                                " " & AgL.Chk_Text(.Item(Col1Colour, I).Value) & " As Colour , " & _
                                " " & AgL.Chk_Text(.Item(Col1Size, I).Value) & " As Size, " & _
                                " " & AgL.Chk_Text(.Item(Col1Quality, I).Value) & " As Quality "
                    End If
                Next
            End With

            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

            If DtTemp.Rows.Count > 0 Then
                For I = 0 To DtTemp.Rows.Count - 1
                    Dim sSQL As String = "Insert Into [#" & bTempTable & "] (Item_UID, CarpetSku, Design, " & _
                           " Size, Colour, Quality, BarCodeImg) " & _
                           " Values(@Item_UID, @CarpetSku, @Design, " & _
                           " @Size, @Colour, @Quality, @BarCodeImg)"

                    Dim cmd As SQLiteCommand = New SQLiteCommand(sSQL, AgL.GCn)

                    Dim Item_UID As SQLiteParameter = New SQLiteParameter("@Item_UID", SqlDbType.VarChar)
                    Dim CarpetSku As SQLiteParameter = New SQLiteParameter("@CarpetSku", SqlDbType.VarChar)
                    Dim Design As SQLiteParameter = New SQLiteParameter("@Design", SqlDbType.VarChar)
                    Dim Size As SQLiteParameter = New SQLiteParameter("@Size", SqlDbType.VarChar)
                    Dim Colour As SQLiteParameter = New SQLiteParameter("@Colour", SqlDbType.VarChar)
                    Dim Quality As SQLiteParameter = New SQLiteParameter("@Quality", SqlDbType.VarChar)
                    Dim BarCodeImg As SQLiteParameter = New SQLiteParameter("@BarCodeImg", SqlDbType.Image)


                    Item_UID.Value = DtTemp.Rows(I)("Item_UID")
                    CarpetSku.Value = DtTemp.Rows(I)("CarpetSku")
                    Design.Value = DtTemp.Rows(I)("Design")
                    Size.Value = DtTemp.Rows(I)("Size")
                    Colour.Value = DtTemp.Rows(I)("Colour")
                    Quality.Value = DtTemp.Rows(I)("Quality")



                    If AgL.XNull(DtTemp.Rows(I)("Item_UID")) <> "" Then
                        BarCodeImg.Value = ClsMain.PrintToBarCode(AgL.XNull(DtTemp.Rows(I)("Item_UID")), 600, 200)
                    Else
                        BarCodeImg.Value = ClsMain.PrintToBarCode("0", 400, 150)
                    End If


                    cmd.Parameters.Add(Item_UID)
                    cmd.Parameters.Add(CarpetSku)
                    cmd.Parameters.Add(Design)
                    cmd.Parameters.Add(Size)
                    cmd.Parameters.Add(Colour)
                    cmd.Parameters.Add(Quality)
                    cmd.Parameters.Add(BarCodeImg)
                    cmd.ExecuteNonQuery()
                Next

                mQry = " Select Item_UID, CarpetSku, Design, " & _
                           " Size, Colour, Quality, BarCodeImg " & _
                           " From [#" & bTempTable & "] H "

                If mQry.Trim <> "" Then
                    DsRep = AgL.FillData(mQry, AgL.GCn)
                    AgPL.CreateFieldDefFile1(DsRep, AgL.PubReportPath & "\" & RepName & ".ttx", True)
                    mCrd.Load(AgL.PubReportPath & "\" & RepName & ".rpt")
                    mCrd.SetDataSource(DsRep.Tables(0))
                    CType(ReportView.Controls("CrvReport"), CrystalDecisions.Windows.Forms.CrystalReportViewer).ReportSource = mCrd
                    AgPL.Formula_Set(mCrd, RepTitle)
                    AgPL.Show_Report(ReportView, "* " & RepTitle & " *", Me.MdiParent)
                    Call AgL.LogTableEntry(mSearchCode, Me.Text, "P", AgL.PubMachineName, AgL.PubUserName, AgL.PubLoginDate, AgL.GCn, AgL.ECmd)
                End If
            Else
                If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records to Print!")
            End If
        Catch Ex As Exception
            MsgBox(Ex.Message)
        End Try
    End Sub
End Class