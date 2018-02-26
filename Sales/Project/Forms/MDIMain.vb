Public Class MDIMain

    Private Sub MDIMain_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Dim mCount As Integer = 0
        If e.KeyCode = Keys.Escape Then
            For Each ChildForm As Form In Me.MdiChildren
                mCount = mCount + 1
            Next

            If mCount = 0 Then
                If MsgBox("Do You Want to Exit?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    'End
                End If
            End If
        End If
    End Sub

    Private Sub MDIMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If AgL Is Nothing Then

            If FOpenIni(StrPath + IniName, AgLibrary.ClsConstant.PubSuperUserName, AgLibrary.ClsConstant.PubSuperUserPassword) Then
                AgIniVar.FOpenConnection("3", "1", False)
            End If
            AgL.PubDivCode = "D"
            AgL.PubDivName = AgL.Dman_Execute("Select Div_Name From Division Where Div_Code = '" & AgL.PubDivCode & "'", AgL.GcnRead).ExecuteScalar

            IniDtCommon_Enviro()


            Dim x As New ClsMain(AgL)
            x.UpdateTableStructure()
            'AgL.FExecuteDBScript(AgL.PubMdlTable, AgL.GCn)


        End If
    End Sub

    Private Sub MnuReports_DropDownItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs)
        Dim FrmObj As AgLibrary.RepFormGlobal
        Dim CFOpen As New ClsFunction

        FrmObj = CFOpen.FOpen(e.ClickedItem.Name, e.ClickedItem.Text, False)
        If FrmObj IsNot Nothing Then
            FrmObj.MdiParent = Me
            FrmObj.Show()
            FrmObj = Nothing
        End If
    End Sub

    Private Sub MnuMaster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub MnuMaster_DropDownItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles MnuSales.DropDownItemClicked, MnuReports.DropDownItemClicked, MnuStatusReports.DropDownItemClicked
        Dim FrmObj As Form
        Dim CFOpen As New ClsFunction
        Dim bIsEntryPoint As Boolean

        If e.ClickedItem.Tag Is Nothing Then e.ClickedItem.Tag = ""
        If e.ClickedItem.Tag.Trim = "" Then
            bIsEntryPoint = True
        Else
            bIsEntryPoint = False
        End If

        FrmObj = CFOpen.FOpen(e.ClickedItem.Name, e.ClickedItem.Text, bIsEntryPoint)
        If FrmObj IsNot Nothing Then
            FrmObj.MdiParent = Me
            FrmObj.Show()
            FrmObj = Nothing
        End If

    End Sub

    Public Sub FAllowedModules(ByRef objMDI As MDIMain, ByVal mEnquiry As Boolean, ByVal mQuotation As Boolean, ByVal mOrder As Boolean, _
                                ByVal mOrderCancellation As Boolean, ByVal mOrderAmendment As Boolean, ByVal mOrderDeliverySchedule As Boolean, _
                                ByVal mQcRequest As Boolean, ByVal mQC As Boolean, ByVal mDeliveryOrder As Boolean, ByVal mChallan As Boolean, _
                                ByVal mShippingBill As Boolean, ByVal mSupplimentaryInvoice As Boolean, ByVal mSaleReturn As Boolean)
        IsApplicable_Enquiry = mEnquiry
        IsApplicable_Quotation = mQuotation
        IsApplicable_Order = mOrder
        IsApplicable_OrderCancellation = mOrderCancellation
        IsApplicable_OrderAmendment = mOrderAmendment
        IsApplicable_DeliveryScheduleChange = mOrderDeliverySchedule
        IsApplicable_QcRequest = mQcRequest
        IsApplicable_Qc = mQC
        IsApplicable_DeliveryOrder = mDeliveryOrder
        IsApplicable_Challan = mChallan
        IsApplicable_ShippingBill = mShippingBill
        IsApplicable_SupplimentaryInvoice = mSupplimentaryInvoice
        IsApplicable_GoodsReturn = mSaleReturn

        If Not IsApplicable_Enquiry Then
            objMDI.MnuSaleEnquiry.AccessibleRole = Windows.Forms.AccessibleRole.None
            objMDI.MnuSaleEnquiryReport.AccessibleRole = Windows.Forms.AccessibleRole.None
            objMDI.MnuSaleEnquiryStatus.AccessibleRole = Windows.Forms.AccessibleRole.None
        End If

        If Not IsApplicable_Quotation Then
            objMDI.MnuSaleQuotation.AccessibleRole = Windows.Forms.AccessibleRole.None
            objMDI.MnuSaleQuotationAmendment.AccessibleRole = Windows.Forms.AccessibleRole.None
            objMDI.MnuSaleQuotationReport.AccessibleRole = Windows.Forms.AccessibleRole.None
            objMDI.MnuSaleQuotationStatus.AccessibleRole = Windows.Forms.AccessibleRole.None
        End If

        If Not IsApplicable_Order Then
            objMDI.MnuSaleOrder.AccessibleRole = Windows.Forms.AccessibleRole.None
            objMDI.MnuSaleOrderReport.AccessibleRole = Windows.Forms.AccessibleRole.None
            objMDI.MnuSaleOrderStatus.AccessibleRole = Windows.Forms.AccessibleRole.None
        End If

        If Not IsApplicable_OrderCancellation Then
            objMDI.MnuSaleOrderCancellation.AccessibleRole = Windows.Forms.AccessibleRole.None
        End If

        If Not IsApplicable_OrderAmendment Then
            objMDI.MnuSaleOrderAmendment.AccessibleRole = Windows.Forms.AccessibleRole.None
        End If

        If Not IsApplicable_DeliveryScheduleChange Then
            objMDI.MnuSaleOrderDeliveryScheduleChange.AccessibleRole = Windows.Forms.AccessibleRole.None
        End If

        If Not IsApplicable_QcRequest Then
            objMDI.MnuSaleQCRequest.AccessibleRole = Windows.Forms.AccessibleRole.None
            objMDI.MnuSaleQCRequestReport.AccessibleRole = Windows.Forms.AccessibleRole.None
            objMDI.MnuSaleQCRequestStatus.AccessibleRole = Windows.Forms.AccessibleRole.None
        End If

        If Not IsApplicable_Qc Then
            objMDI.MnuSaleQC.AccessibleRole = Windows.Forms.AccessibleRole.None
            objMDI.MnuSaleQCReport.AccessibleRole = Windows.Forms.AccessibleRole.None
        End If

        If Not IsApplicable_DeliveryOrder Then
            objMDI.MnuDeliveryOrder.AccessibleRole = Windows.Forms.AccessibleRole.None
            objMDI.MnuDeliveryOrderReport.AccessibleRole = Windows.Forms.AccessibleRole.None
            objMDI.MnuDeliveryOrderStatus.AccessibleRole = Windows.Forms.AccessibleRole.None
        End If

        If Not IsApplicable_Challan Then
            objMDI.MnuSaleChallan.AccessibleRole = Windows.Forms.AccessibleRole.None
            objMDI.MnuSaleChallanReport.AccessibleRole = Windows.Forms.AccessibleRole.None
            objMDI.MnuSaleChallanStatus.AccessibleRole = Windows.Forms.AccessibleRole.None
        End If

        If Not IsApplicable_ShippingBill Then
            objMDI.MnuShippingBill.AccessibleRole = Windows.Forms.AccessibleRole.None
            objMDI.MnuShippingBillReport.AccessibleRole = Windows.Forms.AccessibleRole.None
        End If

        If Not IsApplicable_SupplimentaryInvoice Then
            objMDI.MnuSaleSupplimentaryInvoice.AccessibleRole = Windows.Forms.AccessibleRole.None
        End If

        If Not IsApplicable_GoodsReturn Then
            objMDI.MnuSaleReturn.AccessibleRole = Windows.Forms.AccessibleRole.None
        End If
    End Sub
End Class
