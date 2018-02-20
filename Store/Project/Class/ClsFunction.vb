Public Class ClsFunction
    Dim WithEvents ObjRepFormGlobal As AgLibrary.RepFormGlobal
    Dim WithEvents ReportFrm As ReportLayout.FrmReportLayout
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
                'Master Entry
                Case MDI.MnuPhysicalStockEntry.Name
                    FrmObj = New FrmPhysicalStockEntry(StrUserPermission, DTUP, AgTemplate.ClsMain.Temp_NCat.PhysicalStockEntry)

                Case MDI.MnuPhysicalStockAdjustmentEntry.Name
                    FrmObj = New FrmPhysicalStockAdjustmentEntry(StrUserPermission, DTUP, AgTemplate.ClsMain.Temp_NCat.PhysicalStockAdjustmentEntry)

                Case MDI.MnuComputerMaster.Name
                    FrmObj = New FrmComputer(StrUserPermission, DTUP)

                Case MDI.MnuArea.Name
                    FrmObj = New FrmArea(StrUserPermission, DTUP)

                Case MDI.MnuState.Name
                    FrmObj = New FrmState(StrUserPermission, DTUP)

                Case MDI.MnuCity.Name
                    FrmObj = New FrmCity(StrUserPermission, DTUP)

                Case MDI.MnuDepartment.Name
                    FrmObj = New FrmDepartment(StrUserPermission, DTUP)

                Case MDI.MnuItemCategory.Name
                    FrmObj = New FrmItemCategory(StrUserPermission, DTUP)

                Case MDI.MnuItemGroup.Name
                    FrmObj = New FrmItemGroup(StrUserPermission, DTUP)

                Case MDI.MnuItemMaster.Name
                    FrmObj = New FrmItemMaster(StrUserPermission, DTUP)

                Case MDI.MnuItemMasterCloth.Name
                    FrmObj = New FrmItemMaster_Cloth(StrUserPermission, DTUP)

                Case MDI.MnuDimension1Master.Name
                    FrmObj = New FrmDimension1(StrUserPermission, DTUP)

                Case MDI.MnuDimension2Master.Name
                    FrmObj = New FrmDimension2(StrUserPermission, DTUP)

                Case MDI.MnuReasonMaster.Name
                    FrmObj = New FrmReasonMaster(StrUserPermission, DTUP)

                Case MDI.MnuGodown.Name
                    FrmObj = New FrmGodown(StrUserPermission, DTUP)

                Case MDI.MnuItemReportingGroup.Name
                    FrmObj = New FrmItemReportingGroup(StrUserPermission, DTUP)

                Case MDI.MnuItemInvoiceGroup.Name
                    FrmObj = New FrmItemInvoiceGroup(StrUserPermission, DTUP)

                Case MDI.MnuItemRateGroup.Name
                    FrmObj = New FrmItemRateGroup(StrUserPermission, DTUP)

                Case MDI.MnuPartyRateGroup.Name
                    FrmObj = New FrmPartyRateGroup(StrUserPermission, DTUP)

                Case MDI.MnuQCGroupMaster.Name
                    FrmObj = New FrmQCGroup(StrUserPermission, DTUP)

                Case MDI.MnuUnitConversion.Name
                    FrmObj = New FrmUnitConversion(StrUserPermission, DTUP)

                Case MDI.MnuVatCommodityCode.Name
                    FrmObj = New FrmVatCommodityCode(StrUserPermission, DTUP)

                Case MDI.MnuTariffHeading.Name
                    FrmObj = New FrmTariffHead(StrUserPermission, DTUP)

                Case MDI.MnuTermCondition.Name
                    FrmObj = New FrmTermCondition(StrUserPermission, DTUP)

                Case MDI.MnuCustomerMaster.Name
                    FrmObj = New FrmParty(StrUserPermission, DTUP)
                    CType(FrmObj, FrmParty).MasterType = AgTemplate.ClsMain.SubgroupType.Customer
                    CType(FrmObj, FrmParty).SubGroupNature = FrmParty.ESubgroupNature.Customer

                Case MDI.MnuSupplierMaster.Name
                    FrmObj = New FrmParty(StrUserPermission, DTUP)
                    CType(FrmObj, FrmParty).MasterType = AgTemplate.ClsMain.SubgroupType.Supplier
                    CType(FrmObj, FrmParty).SubGroupNature = FrmParty.ESubgroupNature.Supplier

                Case MDI.MnuAgentMaster.Name
                    FrmObj = New FrmParty(StrUserPermission, DTUP)
                    CType(FrmObj, FrmParty).MasterType = AgTemplate.ClsMain.SubgroupType.Agent
                    CType(FrmObj, FrmParty).SubGroupNature = FrmParty.ESubgroupNature.Supplier

                Case MDI.MnuShiftMaster.Name
                    FrmObj = New FrmShiftMaster(StrUserPermission, DTUP)

                    'Transaction Entry
                Case MDI.MnuItemIssueFromStore.Name
                    FrmObj = New FrmStoreIssue(StrUserPermission, DTUP, AgTemplate.ClsMain.Temp_NCat.StoreIssue)

                Case MDI.MnuItemReceiveInStore.Name
                    FrmObj = New FrmStoreReceive(StrUserPermission, DTUP, AgTemplate.ClsMain.Temp_NCat.StoreReceive)

                Case MDI.MnuInternalProcess.Name
                    FrmObj = New FrmInternalProcess(StrUserPermission, DTUP, AgTemplate.ClsMain.Temp_NCat.InternalProcess)

                Case MDI.MnuStockTransfer.Name
                    FrmObj = New FrmStockTransfer(StrUserPermission, DTUP, AgTemplate.ClsMain.Temp_NCat.StockTransfer)

                Case MDI.MnuItemRequisition.Name
                    FrmObj = New FrmRequisition(StrUserPermission, DTUP, AgTemplate.ClsMain.Temp_NCat.StoreRequisition)

                Case MDI.MnuItemRequisitionApproval.Name
                    FrmObj = New FrmRequisitionApproval()

                Case MDI.MnuGatePassEntry.Name
                    'FrmObj = New FrmGatePass(StrUserPermission, DTUP, AgTemplate.ClsMain.Temp_NCat.GatePass)

                Case MDI.MnuRateTypeMaster.Name
                    FrmObj = New FrmRateType(StrUserPermission, DTUP)

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

    Public Sub New()

    End Sub
End Class

