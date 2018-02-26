Public Class ClsFunction
    Dim MDI As New MDIMain
    Dim WithEvents ObjRepFormGlobal As AgLibrary.RepFormGlobal
    Dim WithEvents ReportFrm As ReportLayout.FrmReportLayout
    Dim CRepProc As ClsReportProcedures

    Public Function FOpen(ByVal StrSender As String, ByVal StrSenderText As String, Optional ByVal IsEntryPoint As Boolean = True)
        Dim mQry As String = ""
        Dim FrmObj As Form = Nothing
        Dim StrUserPermission As String
        Dim DTUP As New DataTable
        Dim ADMain As OleDb.OleDbDataAdapter = Nothing
        Dim strNCat As String = ""

        'For User Permission Open

        StrUserPermission = AgIniVar.FunGetUserPermission(ClsMain.ModuleName, StrSender, StrSenderText, DTUP)

        If AgL.StrCmp(AgL.PubUserName, "SA") Then
            StrUserPermission = "AEDP"
        End If

        If AgL.PubDivisionList = "('')" Then AgL.PubDivisionList = "('" + AgL.PubDivCode + "')"

        If IsEntryPoint Then
            Select Case StrSender
                Case MDI.MnuPurchaseIndent.Name
                    FrmObj = New FrmPurchIndent(StrUserPermission, DTUP, AgTemplate.ClsMain.Temp_NCat.PurchaseIndent)

                Case MDI.MnuPurchaseIndentCancel.Name
                    FrmObj = New FrmPurchIndentCancel(StrUserPermission, DTUP, AgTemplate.ClsMain.Temp_NCat.PurchaseIndentCancel)

                Case MDI.MnuPurchaseOrder.Name
                    FrmObj = New FrmPurchOrder(StrUserPermission, DTUP, AgTemplate.ClsMain.Temp_NCat.PurchaseOrder)

                Case MDI.MnuPurchaseOrderCancel.Name
                    FrmObj = New FrmPurchOrderCancel(StrUserPermission, DTUP, AgTemplate.ClsMain.Temp_NCat.PurchaseOrderCancel)

                Case MDI.MnuPurchaseOrderAmendment.Name
                    FrmObj = New FrmPurchOrderAmendment(StrUserPermission, DTUP, AgTemplate.ClsMain.Temp_NCat.PurchaseOrderAmendment)

                Case MDI.MnuPurchaseChallan.Name
                    FrmObj = New FrmPurchChallan(StrUserPermission, DTUP, AgTemplate.ClsMain.Temp_NCat.GoodsReceipt)

                Case MDI.MnuPurchaseChallanReturn.Name
                    FrmObj = New FrmPurchChallanReturn(StrUserPermission, DTUP, AgTemplate.ClsMain.Temp_NCat.PurchaseChallanReturn)

                Case MDI.MnuPurchaseInvoice.Name
                    FrmObj = New FrmPurchInvoice(StrUserPermission, DTUP, AgTemplate.ClsMain.Temp_NCat.PurchaseInvoice)

                Case MDI.MnuPurchaseInvoiceDirect.Name
                    FrmObj = New FrmPurchInvoiceDirect(StrUserPermission, DTUP, AgTemplate.ClsMain.Temp_NCat.PurchaseInvoice)

                Case MDI.MnuPurchaseSupplimentaryInvoice.Name
                    FrmObj = New FrmPurchInvoiceAmendment(StrUserPermission, DTUP, AgTemplate.ClsMain.Temp_NCat.PurchaseInvoice)

                Case MDI.MnuPurchaseReturn.Name
                    FrmObj = New FrmPurchReturn(StrUserPermission, DTUP, AgTemplate.ClsMain.Temp_NCat.PurchaseReturn)

                Case Else
                    FrmObj = Nothing
            End Select
        Else
            ReportFrm = New ReportLayout.FrmReportLayout(ClsMain.ModuleName, StrSender, StrSenderText, AgL.PubReportPath)
            CRepProc = New ClsReportProcedures(ReportFrm)
            CRepProc.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
            CRepProc.Ini_Grid()
            FrmObj = ReportFrm
        End If

        If FrmObj IsNot Nothing Then
            FrmObj.Text = StrSenderText
        End If
        Return FrmObj
    End Function

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
End Class

