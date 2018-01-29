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
                Case MDI.MnuJobWorker.Name
                    FrmObj = New FrmJobWorker(StrUserPermission, DTUP)
                    CType(FrmObj, FrmJobWorker).MasterType = AgTemplate.ClsMain.SubgroupType.JobWorker
                    CType(FrmObj, FrmJobWorker).SubGroupNature = FrmJobWorker.ESubgroupNature.Supplier

                Case MDI.MnuJobOrder.Name
                    FrmObj = New FrmJobOrder(StrUserPermission, DTUP, AgTemplate.ClsMain.Temp_NCat.JobOrder)

                Case MDI.MnuJobOrderCancel.Name
                    FrmObj = New FrmJobOrderCancel(StrUserPermission, DTUP, AgTemplate.ClsMain.Temp_NCat.JobOrderCancel)

                Case MDI.MnuJobReceive.Name
                    FrmObj = New FrmJobReceive(StrUserPermission, DTUP, AgTemplate.ClsMain.Temp_NCat.JobReceive)

                Case MDI.MnuJobOrderAmendment.Name
                    FrmObj = New FrmJobOrderAmendment(StrUserPermission, DTUP, AgTemplate.ClsMain.Temp_NCat.JobOrderAmendment)

                Case MDI.MnuJobQC.Name
                    'FrmObj = New FrmJobQC(StrUserPermission, DTUP, AgTemplate.ClsMain.Temp_NCat.JobQC)
                    'FrmObj = New FrmJobQC_New(StrUserPermission, DTUP, AgTemplate.ClsMain.Temp_NCat.JobQC)

                Case MDI.MnuJobInvoice.Name
                    FrmObj = New FrmJobInvoice(StrUserPermission, DTUP, AgTemplate.ClsMain.Temp_NCat.JobInvoice)

                Case MDI.MnuJobInvoiceAmendment.Name
                    FrmObj = New FrmJobInvoiceAmendment(StrUserPermission, DTUP, AgTemplate.ClsMain.Temp_NCat.JobInvoiceAmendment)

                Case MDI.MnuJobConsumption.Name
                    FrmObj = New FrmJobConsumption(StrUserPermission, DTUP, AgTemplate.ClsMain.Temp_NCat.JobConsumption)

                Case MDI.MnuMaterialCostConversion.Name
                    FrmObj = New FrmMaterialCostConversion(StrUserPermission, DTUP)

                Case MDI.MnuJobTDS.Name
                    'FrmObj = New FrmJobTDS(StrUserPermission, DTUP, AgTemplate.ClsMain.Temp_NCat.JobTDS)

                Case MDI.MnuJobPayment.Name
                    'FrmObj = New FrmJobPayment(StrUserPermission, DTUP, AgTemplate.ClsMain.Temp_NCat.JobPayment)

                Case MDI.MnuPaymentAdjustment.Name
                    FrmObj = New FrmJobPaymentAdjustment()


                Case MDI.MnuJobDebitNote.Name
                    'FrmObj = New FrmJobDebitCreaditNote(StrUserPermission, DTUP, ClsMain.Temp_NCat.FinishingDebitNote)
                    'CType(FrmObj, FrmJobDebitCreaditNote).TransactionType = FrmJobDebitCreaditNote.EnumTransType.Payment
                    FrmObj = New FrmJobDebitCreditNote(StrUserPermission, DTUP, ClsMain.Temp_NCat.FinishingDebitNote)
                    CType(FrmObj, FrmJobDebitCreditNote).TransactionType = FrmJobDebitCreditNote.EnumTransType.Payment
                    CType(FrmObj, FrmJobDebitCreditNote).EntryType = FrmJobDebitCreditNote.EnumEntryType.DebitAndCreditNote

                Case MDI.MnuJobCreditNote.Name
                    'FrmObj = New FrmJobDebitCreaditNote(StrUserPermission, DTUP, ClsMain.Temp_NCat.FinishingCreditNote)
                    'CType(FrmObj, FrmJobDebitCreaditNote).TransactionType = FrmJobDebitCreaditNote.EnumTransType.Receipt
                    FrmObj = New FrmJobDebitCreditNote(StrUserPermission, DTUP, ClsMain.Temp_NCat.FinishingCreditNote)
                    CType(FrmObj, FrmJobDebitCreditNote).TransactionType = FrmJobDebitCreditNote.EnumTransType.Receipt
                    CType(FrmObj, FrmJobDebitCreditNote).EntryType = FrmJobDebitCreditNote.EnumEntryType.DebitAndCreditNote


                Case MDI.MnuTimeIncentive.Name
                    FrmObj = New FrmJobDebitCreditNote(StrUserPermission, DTUP, ClsMain.Temp_NCat.JobTimeIncentive)
                    CType(FrmObj, FrmJobDebitCreditNote).TransactionType = FrmJobDebitCreditNote.EnumTransType.Receipt
                    CType(FrmObj, FrmJobDebitCreditNote).EntryType = FrmJobDebitCreditNote.EnumEntryType.TimeIncentiveAndPenalty

                Case MDI.MnuTimePenalty.Name
                    'FrmObj = New FrmJobDebitCreditNote(StrUserPermission, DTUP, ClsMain.Temp_NCat.FinishingDebitNote)
                    'CType(FrmObj, FrmJobDebitCreditNote).TransactionType = FrmJobDebitCreditNote.EnumTransType.Payment
                    'CType(FrmObj, FrmJobDebitCreditNote).EntryType = FrmJobDebitCreditNote.EnumEntryType.DebitAndCreditNote

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

