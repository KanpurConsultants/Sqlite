Public Class ClsFunction
    Dim WithEvents ObjRepFormGlobal As AgLibrary.RepFormGlobal
    Dim CRepProc As ClsReportProcedures

    Public Function FOpen(ByVal StrSender As String, ByVal StrSenderText As String, Optional ByVal IsEntryPoint As Boolean = True)
        Dim FrmObj As Form
        Dim StrUserPermission As String
        Dim DTUP As New DataTable
        Dim ADMain As OleDb.OleDbDataAdapter = Nothing
        Dim MDI As New MDIMain

        'For User Permission Open
        StrUserPermission = AgIniVar.FunGetUserPermission(ClsMain.ModuleName, StrSender, StrSenderText, DTUP)
        ''For User Permission End 

        If IsEntryPoint Then
            Select Case StrSender
                Case MDI.MnuMaterialIssueFromStore.Name
                    FrmObj = New Store.FrmStoreIssue(StrUserPermission, DTUP, AgTemplate.ClsMain.Temp_NCat.StoreIssue)

                Case MDI.MnuMaterialReceiveInStore.Name
                    FrmObj = New Store.FrmStoreReceive(StrUserPermission, DTUP, AgTemplate.ClsMain.Temp_NCat.StoreReceive)

                Case MDI.MnuDistributorQuery.Name
                    FrmObj = New FrmDIstributerQuery(StrUserPermission, DTUP)

                Case MDI.MnuItemCategoryMaster.Name
                    FrmObj = New FrmItemCategory(StrUserPermission, DTUP)

                Case MDI.MnuDistributorMaster.Name
                    FrmObj = New FrmDistributer(StrUserPermission, DTUP)

                Case MDI.MnuDifferentialIncome.Name
                    FrmObj = New FrmDifferentialIncome(StrUserPermission, DTUP)

                Case MDI.MnuItemMaster.Name
                    FrmObj = New FrmItem(StrUserPermission, DTUP)

                Case MDI.MnuItemGroupMaster.Name
                    FrmObj = New FrmItemGroup(StrUserPermission, DTUP)

                Case MDI.MnuCityMaster.Name
                    FrmObj = New FrmCity(StrUserPermission, DTUP)

                Case MDI.MnuPartyMaster.Name
                    FrmObj = New FrmParty(StrUserPermission, DTUP)
                    CType(FrmObj, FrmParty).MasterType = ClsMain.MasterType.Party


                Case MDI.MnuSaleInvoice.Name
                    FrmObj = New FrmSaleInvoice(StrUserPermission, DTUP, ClsMain.ItemType.RawMaterial)
                    Call CType(FrmObj, FrmSaleInvoice).FSetParameter(False, False, False, False, False, False, False, False, False, False, True, False, False, True, False)

                Case MDI.MnuPaymentEntry.Name
                    FrmObj = New FrmPaymentReceipt(StrUserPermission, DTUP, AgTemplate.ClsMain.Temp_NCat.Payment, FrmPaymentReceipt.TransactionType.Payment)

                Case MDI.MnuMoneyReceiptEntry.Name
                    FrmObj = New FrmPaymentReceipt(StrUserPermission, DTUP, AgTemplate.ClsMain.Temp_NCat.Receipt, FrmPaymentReceipt.TransactionType.Receipt)

                Case MDI.MnuDebitNoteEntry.Name
                    FrmObj = New FrmDebitCreditNote(StrUserPermission, DTUP, AgTemplate.ClsMain.Temp_NCat.DebitNote, FrmDebitCreditNote.TransactionType.DebitNote)

                Case MDI.MnuCreditNoteEntry.Name
                    FrmObj = New FrmDebitCreditNote(StrUserPermission, DTUP, AgTemplate.ClsMain.Temp_NCat.CreditNote, FrmDebitCreditNote.TransactionType.CreditNote)

                Case Else
                    FrmObj = Nothing
            End Select
        Else
            ObjRepFormGlobal = New AgLibrary.RepFormGlobal(AgL)
            CRepProc = New ClsReportProcedures(ObjRepFormGlobal)
            CRepProc.GRepFormName = Replace(Replace(StrSenderText, "&", ""), " ", "")
            CRepProc.Ini_Grid()
            FrmObj = ObjRepFormGlobal
        End If
        If FrmObj IsNot Nothing Then
            FrmObj.Text = StrSenderText
        End If
        Return FrmObj
    End Function

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Public Sub New()

    End Sub
End Class

