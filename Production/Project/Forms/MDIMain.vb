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
                AgIniVar.FOpenConnection("5", "1", False)
            End If
            AgL.PubDivCode = "W"
            AgL.PubDivName = AgL.Dman_Execute("Select Div_Name From Division Where Div_Code = '" & AgL.PubDivCode & "'", AgL.GcnRead).ExecuteScalar

            IniDtCommon_Enviro()

            Dim ClsObj As New ClsMain(AgL)
            'Dim ClsObj_AgTemplate As New AgTemplate.ClsMain(AgL)

            'ClsObj.UpdateTableStructure(AgL.PubMdlTable)
            'ClsObj_AgTemplate.UpdateTableStructure(AgL.PubMdlTable)
            'ClsObj_AgTemplate.UpdateTableStructureJob(AgL.PubMdlTable)
            'AgL.FExecuteDBScript(AgL.PubMdlTable, AgL.GCn)

            'ClsObj_AgTemplate.UpdateTableInitialiser()
            'ClsObj.UpdateTableInitialiser()

        End If
    End Sub

    Private Sub MnuMaster_DropDownItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles MnuProduction.DropDownItemClicked, MnuMasterProduction.DropDownItemClicked, MnuReportsProduction.DropDownItemClicked
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

        'If Not IsApplicable_Indent Then
        '    objMDI.MnuPurchaseIndent.AccessibleRole = Windows.Forms.AccessibleRole.None
        '    objMDI.MnuPurchaseIndentReport.AccessibleRole = Windows.Forms.AccessibleRole.None
        '    objMDI.MnuPurchaseIndentStatus.AccessibleRole = Windows.Forms.AccessibleRole.None
        'End If

        'If Not IsApplicable_Order Then
        '    objMDI.MnuJobOrder.AccessibleRole = Windows.Forms.AccessibleRole.None
        '    objMDI.MnuPurchaseOrderReport.AccessibleRole = Windows.Forms.AccessibleRole.None
        '    objMDI.MnuPurchaseOrderStatus.AccessibleRole = Windows.Forms.AccessibleRole.None
        'End If

        'If Not IsApplicable_OrderCancellation Then
        '    objMDI.MnuJobOrderCancel.AccessibleRole = Windows.Forms.AccessibleRole.None
        'End If

        'If Not IsApplicable_OrderAmendment Then
        '    objMDI.MnuJobOrderAmendment.AccessibleRole = Windows.Forms.AccessibleRole.None
        'End If

        'If Not IsApplicable_Challan Then
        '    objMDI.MnuJobReceive.AccessibleRole = Windows.Forms.AccessibleRole.None
        '    objMDI.MnuPurchaseChallanReport.AccessibleRole = Windows.Forms.AccessibleRole.None
        '    objMDI.MnuPurchaseChallanStatus.AccessibleRole = Windows.Forms.AccessibleRole.None
        'End If

        'If Not IsApplicable_SupplimentaryInvoice Then
        '    objMDI.MnuPurchaseSupplimentaryInvoice.AccessibleRole = Windows.Forms.AccessibleRole.None
        'End If

        'If Not IsApplicable_GoodsReturn Then
        '    objMDI.MnuPurchaseReturn.AccessibleRole = Windows.Forms.AccessibleRole.None
        'End If

    End Sub

End Class
