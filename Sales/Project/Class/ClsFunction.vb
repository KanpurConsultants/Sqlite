Public Class ClsFunction
    Dim WithEvents ObjRepFormGlobal As AgLibrary.RepFormGlobal

    Dim MDI As New MDIMain
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
                Case MDI.MnuSaleOrder.Name
                    FrmObj = New FrmSaleOrder(StrUserPermission, DTUP, AgTemplate.ClsMain.Temp_NCat.SaleOrder)

                Case MDI.MnuSaleOrderCancellation.Name
                    FrmObj = New FrmSaleOrderCancel(StrUserPermission, DTUP, AgTemplate.ClsMain.Temp_NCat.SaleOrderCancel)

                Case MDI.MnuSaleOrderAmendment.Name
                    FrmObj = New FrmSaleOrderAmendment(StrUserPermission, DTUP, AgTemplate.ClsMain.Temp_NCat.SaleOrderAmendment)

                Case MDI.MnuSaleChallan.Name
                    FrmObj = New FrmSaleChallan(StrUserPermission, DTUP, AgTemplate.ClsMain.Temp_NCat.SaleChallan)

                Case MDI.MnuSaleInvoice.Name
                    FrmObj = New FrmSaleInvoice(StrUserPermission, DTUP, AgTemplate.ClsMain.Temp_NCat.SaleInvoice)

                Case MDI.MnuSaleInvoiceNew.Name
                    FrmObj = New FrmSaleInvoiceNew(StrUserPermission, DTUP, AgTemplate.ClsMain.Temp_NCat.SaleInvoice)

                Case MDI.MnuSaleReturn.Name
                    FrmObj = New FrmSaleReturn(StrUserPermission, DTUP, AgTemplate.ClsMain.Temp_NCat.SaleReturn)

                Case Else
                    FrmObj = Nothing
            End Select
        Else
            ReportFrm = New ReportLayout.FrmReportLayout("", "", StrSenderText, "")
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

