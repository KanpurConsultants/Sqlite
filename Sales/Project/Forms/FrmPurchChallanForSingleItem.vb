Imports System.Data.SqlClient
Public Class FrmPurchChallanForSingleItem
    Dim mQry As String = ""

    Dim DtMaster As DataTable = Nothing

    Public mOkButtonPressed As Boolean = False

    Public mReferenceDocId$ = ""
    Public mReferenceDocIdSr As Integer = 0

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, 0)
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
            BtnOk.Anchor = AnchorStyles.Top + AnchorStyles.Bottom + AnchorStyles.Left + AnchorStyles.Right
            BtnCancel.Anchor = AnchorStyles.Top + AnchorStyles.Bottom + AnchorStyles.Left + AnchorStyles.Right

            TxtVendor.Focus()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub BlankText()
    End Sub

    Public Sub DispText(ByVal Enable As Boolean)

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

    Private Sub Calculation()
        Try
            TxtMRP.Text = Val(TxtQty.Text) * Val(TxtRate.Text)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub BtnChargeDuw_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnOk.Click, BtnCancel.Click
        Try
            Select Case sender.Name
                Case BtnOk.Name
                    If Not Data_Validation() Then Exit Sub

                    FPostInPurchChallan()
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

    Private Sub TxtSaleToPartyCity_Enter(ByVal sender As Object, ByVal e As System.EventArgs)
        Try
            Select Case sender.Name
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub TxtSaleToPartyMobile_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtItem.Validating
        Dim DtTemp As DataTable = Nothing
        Try
            Select Case sender.Name
                Case TxtItem.Name
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub TxtVendor_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TxtVendor.KeyDown, TxtItem.KeyDown
        Try
            Select Case sender.name
                Case TxtVendor.Name
                    If TxtVendor.AgHelpDataSet Is Nothing Then
                        If e.KeyCode <> Keys.Enter Then
                            mQry = "SELECT Sg.SubCode As Code, Sg.DispName AS [Name], Sg.SalesTaxPostingGroup, " & _
                                    " Sg.SalesTaxPostingGroup, " & _
                                    " IfNull(Sg.IsDeleted,0) As IsDeleted,  Sg.Currency, " & _
                                    " IfNull(Sg.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') As Status, Sg.Div_Code " & _
                                    " FROM SubGroup Sg " & _
                                    " LEFT JOIN City C ON Sg.CityCode = C.CityCode  " & _
                                    " Where Sg.Nature in ('Customer','Supplier') " & _
                                    " And IfNull(Sg.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' "
                            TxtVendor.AgHelpDataSet(6) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If

                Case TxtItem.Name                    
                    If e.KeyCode = Keys.Insert Then
                        TxtItem.Tag = AgTemplate.ClsMain.FOpenMaster(Me.Owner, "Item Master", "TxtV_Type.Tag")
                        TxtItem.Text = AgL.XNull(AgL.Dman_Execute("Select Description From Item Where Code = '" & TxtItem.Tag & "'", AgL.GCn).ExecuteScalar)
                        SendKeys.Send("{Enter}")
                    End If

                    If TxtItem.AgHelpDataSet Is Nothing Then
                        If e.KeyCode <> Keys.Enter Then
                            mQry = "SELECT I.Code, I.Description, I.ManualCode, I.Unit, I.ItemType, I.SalesTaxPostingGroup , " & _
                                  " IfNull(I.IsDeleted ,0) AS IsDeleted, I.Div_Code, " & _
                                  " I.MeasureUnit, I.Measure As MeasurePerPcs, I.Rate As Rate, 1 As PendingQty, I.Status, " & _
                                  " U.DecimalPlaces as QtyDecimalPlaces, U1.DecimalPlaces as MeasureDecimalPlaces " & _
                                  " FROM Item I " & _
                                  " LEFT JOIN Unit U On I.Unit = U.Code " & _
                                  " LEFT JOIN Unit U1 On I.MeasureUnit = U1.Code " & _
                                  " Where IfNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' "
                            TxtItem.AgHelpDataSet(13) = AgL.FillData(mQry, AgL.GCn)
                        End If
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Txt_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtVendor.Validating, TxtItem.Validating
        Dim DrTemp As DataRow() = Nothing
        Dim DtTemp As DataTable = Nothing
        Try
            Select Case sender.NAME
                Case TxtItem.Name
                    If TxtItem.AgHelpDataSet Is Nothing Then
                        TxtUnit.Text = ""
                    Else
                        If TxtItem.Tag <> "" Then
                            If TxtItem.AgHelpDataSet.Tables(0).Rows.Count > 0 Then
                                DrTemp = sender.AgHelpDataSet.Tables(0).Select("Code = " & AgL.Chk_Text(sender.tag) & "")
                                TxtUnit.Text = AgL.XNull(DrTemp(0)("Unit"))

                                mQry = " Select  L.Rate, L.Sale_Rate, L.MRP From PurchChallanDetail L LEFT JOIN PurchChallan H ON L.DocId = H.DocId Where L.Item = '" & TxtItem.Tag & "' Order By H.V_Date Desc Limit 1"
                                DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

                                If DtTemp.Rows.Count > 0 Then
                                    TxtMRP.Text = AgL.VNull(DtTemp.Rows(0)("MRP"))
                                    TxtRate.Text = AgL.VNull(DtTemp.Rows(0)("Rate"))
                                    TxtSaleRate.Text = AgL.VNull(DtTemp.Rows(0)("Sale_Rate"))
                                End If
                            End If
                        End If
                    End If

            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FPostInPurchChallan()
        Dim mTrans As String = ""
        Dim DtJobOrder As DataTable = Nothing
        Dim DtJobOrderDetail As DataTable = Nothing
        Dim I As Integer = 0, J As Integer = 0, mSr As Integer = 0

        Dim V_Type$ = "", DocId$ = "", V_Date$ = "", V_Prefix$ = "", ManualRefNo$ = ""
        Dim V_No As Integer = 0

        Try
            AgL.ECmd = AgL.GCn.CreateCommand
            AgL.ETrans = AgL.GCn.BeginTransaction(IsolationLevel.ReadCommitted)
            AgL.ECmd.Transaction = AgL.ETrans
            mTrans = "Begin"

            V_Type = AgL.XNull(AgL.Dman_Execute("Select V_Type From Voucher_Type Vt Where Vt.NCat = '" & AgTemplate.ClsMain.Temp_NCat.GoodsReceipt & "'", AgL.GcnRead).ExecuteScalar)
            V_Date = CType(Me.Owner, FrmSaleInvoice).TxtV_Date.Text
            DocId = AgL.GetDocId(V_Type, CStr(V_No), CDate(V_Date), AgL.GcnRead, AgL.PubDivCode, AgL.PubSiteCode)
            AgL.UpdateVoucherCounter(DocId, CDate(V_Date), AgL.GcnRead, AgL.ECmd, AgL.PubDivCode, AgL.PubSiteCode)
            V_No = Val(AgL.DeCodeDocID(DocId, AgLibrary.ClsMain.DocIdPart.VoucherNo))
            V_Prefix = AgL.DeCodeDocID(DocId, AgLibrary.ClsMain.DocIdPart.VoucherPrefix)
            ManualRefNo = V_No.ToString    'AgTemplate.ClsMain.FGetManualRefNo("ReferenceNo", "PurchChallan", V_Type, V_Date, AgL.PubDivCode, AgL.PubSiteCode, AgTemplate.ClsMain.ManualRefType.Max)

            mQry = " INSERT INTO PurchChallan " & _
                        " ( " & _
                        " DocID, " & _
                        " V_Type, " & _
                        " V_Prefix, " & _
                        " V_Date, " & _
                        " V_No, " & _
                        " Div_Code, " & _
                        " Site_Code, " & _
                        " ReferenceNo, " & _
                        " Vendor, " & _
                        " EntryBy, " & _
                        " EntryStatus, " & _
                        " EntryDate) " & _
                        " Values( " & _
                        " " & AgL.Chk_Text(DocId) & ", " & _
                        " " & AgL.Chk_Text(V_Type) & ", " & _
                        " " & AgL.Chk_Text(V_Prefix) & ", " & _
                        " " & AgL.Chk_Text(V_Date) & ", " & _
                        " " & Val(V_No) & ", " & _
                        " " & AgL.Chk_Text(AgL.PubDivCode) & ", " & _
                        " " & AgL.Chk_Text(AgL.PubSiteCode) & ", " & _
                        " " & AgL.Chk_Text(ManualRefNo) & ", " & _
                        " " & AgL.Chk_Text(TxtVendor.Tag) & ", " & _
                        " '" & AgL.PubUserName & "', " & _
                        " 'Open', " & _
                        " '" & AgL.PubLoginDate & "') "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

            mSr += 1
            mQry = " INSERT INTO PurchChallanDetail(DocId, Sr, Item, DocQty, Qty, Unit, MeasurePerPcs, TotalMeasure, MeasureUnit, Rate, MRP, Sale_Rate, ExpiryDate, " & _
                    " PurchChallan, PurchChallanSr, Landed_Value) " & _
                    " Values('" & DocId & "', " & mSr & ", " & _
                    " " & AgL.Chk_Text(TxtItem.Tag) & ", " & _
                    " " & Val(TxtQty.Text) & ", " & _
                    " " & Val(TxtQty.Text) & ", " & _
                    " " & AgL.Chk_Text(TxtUnit.Text) & ", " & _
                    " 1, " & Val(TxtQty.Text) & ", " & _
                    " " & AgL.Chk_Text(TxtUnit.Text) & ", " & _
                    " " & Val(TxtRate.Text) & ", " & _
                    " " & Val(TxtMRP.Text) & ", " & _
                    " " & Val(TxtSaleRate.Text) & ", " & _
                    " " & AgL.Chk_Text(TxtExpiry.Text) & ", " & _
                    " '" & DocId & "', " & mSr & ", " & _
                    " " & Val(TxtQty.Text) * Val(TxtRate.Text) & ") "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

            mQry = "Insert Into Stock(DocID, Sr, V_Type, V_Prefix, V_Date, V_No, RecID, Div_Code, Site_Code, " & _
                  " SubCode, Item, Qty_Rec, Unit, MeasurePerPcs, Measure_Rec, MeasureUnit, " & _
                  " ExpiryDate, ReferenceDocID, ReferenceDocIDSr, Landed_Value) " & _
                  " Values(" & AgL.Chk_Text(DocId) & ", " & _
                  " " & mSr & ", " & _
                  " " & AgL.Chk_Text(V_Type) & ", " & _
                  " " & AgL.Chk_Text(V_Prefix) & ", " & _
                  " " & AgL.Chk_Text(V_Date) & ", " & _
                  " " & Val(V_No) & ", " & _
                  " " & Val(ManualRefNo) & ", " & _
                  " " & AgL.Chk_Text(AgL.PubDivCode) & ", " & _
                  " " & AgL.Chk_Text(AgL.PubSiteCode) & ", " & _
                  " " & AgL.Chk_Text(TxtVendor.Tag) & ", " & _
                  " " & AgL.Chk_Text(TxtItem.Tag) & ", " & _
                  " " & AgL.Chk_Text(TxtQty.Text) & ", " & _
                  " " & AgL.Chk_Text(TxtUnit.Text) & ", " & _
                  " " & AgL.Chk_Text(TxtQty.Text) & ", " & _
                  " " & AgL.Chk_Text(TxtQty.Text) & ", " & _
                  " " & AgL.Chk_Text(TxtUnit.Text) & ", " & _
                  " " & AgL.Chk_Text(TxtExpiry.Text) & ", " & _
                  " " & AgL.Chk_Text(DocId) & ", " & _
                  " " & mSr & ", " & Val(TxtQty.Text) * Val(TxtRate.Text) & " )"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

            mReferenceDocId = DocId
            mReferenceDocIdSr = mSr


            AgL.ETrans.Commit()
            mTrans = "Commit"

        Catch ex As Exception
            If mTrans = "Begin" Then
                AgL.ETrans.Rollback()
            End If
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FOpenItemMaster()
        Dim FrmObj As Object = Nothing
        Dim CFOpen As New ClsFunction
        Dim MDI As New MDIMain
        Dim DrTemp As DataRow() = Nothing
        Dim DtTemp As DataTable = Nothing
        Dim bRowIndex As Integer = 0, bColumnIndex As Integer = 0
        Dim bItemCode$ = ""
        Try
            FrmObj = CFOpen.FOpen("MnuItemMaster", "Item Master", True)
            If FrmObj IsNot Nothing Then
                FrmObj.StartPosition = FormStartPosition.Manual
                FrmObj.IsReturnValue = True
                FrmObj.Top = 50
                FrmObj.ShowDialog()
                bItemCode = FrmObj.mItemCode
                FrmObj = Nothing

                mQry = "SELECT I.Description, I.Unit " & _
                          " FROM Item I " & _
                          " Where IfNull(I.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "' "
                DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

                If DtTemp.Rows.Count > 0 Then
                    TxtItem.Text = AgL.XNull(DtTemp.Rows(0)("Description"))
                    TxtUnit.Text = AgL.XNull(DtTemp.Rows(0)("Unit"))
                End If
                TxtQty.Focus()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class