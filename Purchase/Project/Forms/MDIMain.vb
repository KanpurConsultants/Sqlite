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
                'If FOpenIni(StrPath + IniName, "an", "ganesha23") Then
                AgIniVar.FOpenConnection("3", "1", False)
            End If
            AgL.PubDivCode = "D"
            AgL.PubDivName = AgL.Dman_Execute("Select Div_Name From Division Where Div_Code = '" & AgL.PubDivCode & "'", AgL.GcnRead).ExecuteScalar

            IniDtCommon_Enviro()

            'Dim ClsObj As New ClsMain(AgL)
            'Dim ClsObj_AgTemplate As New AgTemplate.ClsMain(AgL)

            'ClsObj.UpdateTableStructure(AgL.PubMdlTable)
            'ClsObj_AgTemplate.UpdateTableStructure(AgL.PubMdlTable)
            'ClsObj_AgTemplate.UpdateTableInitialiser()
            'AgL.FExecuteDBScript(AgL.PubMdlTable, AgL.GCn)
        End If
    End Sub

    Private Sub MnuMaster_DropDownItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles MnuPurchase.DropDownItemClicked, MnuReports.DropDownItemClicked
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

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public Sub FAllowedModules(ByRef objMDI As MDIMain, ByVal mIndent As Boolean, ByVal mIndentCancellation As Boolean, _
                              ByVal mEnquiry As Boolean, ByVal mQuotation As Boolean, ByVal mOrder As Boolean, _
                              ByVal mOrderCancellation As Boolean, ByVal mOrderAmendment As Boolean, _
                              ByVal mQC As Boolean, ByVal mChallan As Boolean, _
                              ByVal mSupplimentaryInvoice As Boolean, ByVal mGoodsReturn As Boolean)
        IsApplicable_Indent = mIndent
        IsApplicable_IndentCancellation = mIndentCancellation
        IsApplicable_Enquiry = mEnquiry
        IsApplicable_Quotation = mQuotation
        IsApplicable_Order = mOrder
        IsApplicable_OrderCancellation = mOrderCancellation
        IsApplicable_QC = mQC
        IsApplicable_Challan = mChallan
        IsApplicable_SupplimentaryInvoice = mSupplimentaryInvoice
        IsApplicable_GoodsReturn = mGoodsReturn

        If Not IsApplicable_Indent Then
            objMDI.MnuPurchaseIndent.AccessibleRole = Windows.Forms.AccessibleRole.None
            objMDI.MnuPurchaseIndentCancel.AccessibleRole = Windows.Forms.AccessibleRole.None
            objMDI.MnuPurchaseIndentReport.AccessibleRole = Windows.Forms.AccessibleRole.None
            objMDI.MnuPurchaseIndentStatus.AccessibleRole = Windows.Forms.AccessibleRole.None
        End If

        If Not IsApplicable_Order Then
            objMDI.MnuPurchaseOrder.AccessibleRole = Windows.Forms.AccessibleRole.None
            objMDI.MnuPurchaseOrderReport.AccessibleRole = Windows.Forms.AccessibleRole.None
            objMDI.MnuPurchaseOrderStatus.AccessibleRole = Windows.Forms.AccessibleRole.None
        End If

        If Not IsApplicable_OrderCancellation Then
            objMDI.MnuPurchaseOrderCancel.AccessibleRole = Windows.Forms.AccessibleRole.None            
        End If

        If Not IsApplicable_OrderAmendment Then
            objMDI.MnuPurchaseOrderAmendment.AccessibleRole = Windows.Forms.AccessibleRole.None
        End If

        If Not IsApplicable_Challan Then
            objMDI.MnuPurchaseChallan.AccessibleRole = Windows.Forms.AccessibleRole.None
            objMDI.MnuPurchaseChallanReport.AccessibleRole = Windows.Forms.AccessibleRole.None
            objMDI.MnuPurchaseChallanStatus.AccessibleRole = Windows.Forms.AccessibleRole.None
        End If

        If Not IsApplicable_SupplimentaryInvoice Then
            objMDI.MnuPurchaseSupplimentaryInvoice.AccessibleRole = Windows.Forms.AccessibleRole.None
        End If

        If Not IsApplicable_GoodsReturn Then
            objMDI.MnuPurchaseChallanReturn.AccessibleRole = Windows.Forms.AccessibleRole.None
            objMDI.MnuPurchaseReturn.AccessibleRole = Windows.Forms.AccessibleRole.None
        Else
            If Not IsApplicable_Challan Then
                objMDI.MnuPurchaseChallanReturn.AccessibleRole = Windows.Forms.AccessibleRole.None
            End If
        End If

    End Sub

    Private Sub MnuUpdateTableStructure_Click(sender As Object, e As EventArgs) Handles MnuUpdateTableStructure.Click
        Dim cm As New ClsMain(AgL)
        cm.UpdateTableStructure()
    End Sub
End Class
