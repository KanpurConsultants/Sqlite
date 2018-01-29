<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MDIMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.MnuMain = New System.Windows.Forms.MenuStrip
        Me.MnuMaster = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuItemCategoryMaster = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuItemGroupMaster = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuItemMaster = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuCityMaster = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuPartyMaster = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuDistributorMaster = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuTransaction = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuPurchaseInvoice = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuSale = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuPaymentEntry = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuMoneyReceiptEntry = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuDebitNoteEntry = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuCreditNoteEntry = New System.Windows.Forms.ToolStripMenuItem
        Me.MnnReports = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuSaleRegister = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuItemWiseSaleReport = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuPurchaseReport = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuItemWisePurchaseReport = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuDistributorQuery = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuSaleInvoice = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuMaterialIssueFromStore = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuMaterialReceiveInStore = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuDifferentialIncome = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'MnuMain
        '
        Me.MnuMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuMaster, Me.MnuTransaction, Me.MnuSale, Me.MnnReports})
        Me.MnuMain.Location = New System.Drawing.Point(0, 0)
        Me.MnuMain.Name = "MnuMain"
        Me.MnuMain.Size = New System.Drawing.Size(965, 24)
        Me.MnuMain.TabIndex = 1
        Me.MnuMain.Text = "MenuStrip1"
        '
        'MnuMaster
        '
        Me.MnuMaster.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuItemCategoryMaster, Me.MnuItemGroupMaster, Me.MnuItemMaster, Me.MnuCityMaster, Me.MnuPartyMaster, Me.MnuDistributorMaster})
        Me.MnuMaster.Name = "MnuMaster"
        Me.MnuMaster.Size = New System.Drawing.Size(55, 20)
        Me.MnuMaster.Text = "Master"
        '
        'MnuItemCategoryMaster
        '
        Me.MnuItemCategoryMaster.Name = "MnuItemCategoryMaster"
        Me.MnuItemCategoryMaster.Size = New System.Drawing.Size(188, 22)
        Me.MnuItemCategoryMaster.Text = "Item Category Master"
        '
        'MnuItemGroupMaster
        '
        Me.MnuItemGroupMaster.Name = "MnuItemGroupMaster"
        Me.MnuItemGroupMaster.Size = New System.Drawing.Size(188, 22)
        Me.MnuItemGroupMaster.Text = "Item Group Master"
        '
        'MnuItemMaster
        '
        Me.MnuItemMaster.Name = "MnuItemMaster"
        Me.MnuItemMaster.Size = New System.Drawing.Size(188, 22)
        Me.MnuItemMaster.Text = "Item Master"
        '
        'MnuCityMaster
        '
        Me.MnuCityMaster.Name = "MnuCityMaster"
        Me.MnuCityMaster.Size = New System.Drawing.Size(188, 22)
        Me.MnuCityMaster.Text = "City Master"
        '
        'MnuPartyMaster
        '
        Me.MnuPartyMaster.Name = "MnuPartyMaster"
        Me.MnuPartyMaster.Size = New System.Drawing.Size(188, 22)
        Me.MnuPartyMaster.Text = "Party Master"
        '
        'MnuDistributorMaster
        '
        Me.MnuDistributorMaster.Name = "MnuDistributorMaster"
        Me.MnuDistributorMaster.Size = New System.Drawing.Size(188, 22)
        Me.MnuDistributorMaster.Text = "Distributor Master"
        '
        'MnuTransaction
        '
        Me.MnuTransaction.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuPurchaseInvoice, Me.MnuSaleInvoice, Me.MnuMaterialIssueFromStore, Me.MnuMaterialReceiveInStore, Me.MnuDifferentialIncome})
        Me.MnuTransaction.Name = "MnuTransaction"
        Me.MnuTransaction.Size = New System.Drawing.Size(81, 20)
        Me.MnuTransaction.Text = "Transaction"
        '
        'MnuPurchaseInvoice
        '
        Me.MnuPurchaseInvoice.Name = "MnuPurchaseInvoice"
        Me.MnuPurchaseInvoice.Size = New System.Drawing.Size(205, 22)
        Me.MnuPurchaseInvoice.Text = "Purchase Invoice"
        '
        'MnuSale
        '
        Me.MnuSale.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuPaymentEntry, Me.MnuMoneyReceiptEntry, Me.MnuDebitNoteEntry, Me.MnuCreditNoteEntry})
        Me.MnuSale.Name = "MnuSale"
        Me.MnuSale.Size = New System.Drawing.Size(63, 20)
        Me.MnuSale.Text = "Voucher"
        '
        'MnuPaymentEntry
        '
        Me.MnuPaymentEntry.Name = "MnuPaymentEntry"
        Me.MnuPaymentEntry.Size = New System.Drawing.Size(183, 22)
        Me.MnuPaymentEntry.Text = "Payment Entry"
        '
        'MnuMoneyReceiptEntry
        '
        Me.MnuMoneyReceiptEntry.Name = "MnuMoneyReceiptEntry"
        Me.MnuMoneyReceiptEntry.Size = New System.Drawing.Size(183, 22)
        Me.MnuMoneyReceiptEntry.Text = "Money Receipt Entry"
        '
        'MnuDebitNoteEntry
        '
        Me.MnuDebitNoteEntry.Name = "MnuDebitNoteEntry"
        Me.MnuDebitNoteEntry.Size = New System.Drawing.Size(183, 22)
        Me.MnuDebitNoteEntry.Text = "Debit Note Entry"
        '
        'MnuCreditNoteEntry
        '
        Me.MnuCreditNoteEntry.Name = "MnuCreditNoteEntry"
        Me.MnuCreditNoteEntry.Size = New System.Drawing.Size(183, 22)
        Me.MnuCreditNoteEntry.Text = "Credit Note Entry"
        '
        'MnnReports
        '
        Me.MnnReports.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuSaleRegister, Me.MnuItemWiseSaleReport, Me.MnuPurchaseReport, Me.MnuItemWisePurchaseReport, Me.MnuDistributorQuery})
        Me.MnnReports.Name = "MnnReports"
        Me.MnnReports.Size = New System.Drawing.Size(59, 20)
        Me.MnnReports.Text = "Reports"
        '
        'MnuSaleRegister
        '
        Me.MnuSaleRegister.Name = "MnuSaleRegister"
        Me.MnuSaleRegister.Size = New System.Drawing.Size(215, 22)
        Me.MnuSaleRegister.Tag = "Reports"
        Me.MnuSaleRegister.Text = "Sale Report"
        '
        'MnuItemWiseSaleReport
        '
        Me.MnuItemWiseSaleReport.Name = "MnuItemWiseSaleReport"
        Me.MnuItemWiseSaleReport.Size = New System.Drawing.Size(215, 22)
        Me.MnuItemWiseSaleReport.Tag = "Report"
        Me.MnuItemWiseSaleReport.Text = "Item Wise Sale Report"
        '
        'MnuPurchaseReport
        '
        Me.MnuPurchaseReport.Name = "MnuPurchaseReport"
        Me.MnuPurchaseReport.Size = New System.Drawing.Size(215, 22)
        Me.MnuPurchaseReport.Tag = "Report"
        Me.MnuPurchaseReport.Text = "Purchase Report"
        '
        'MnuItemWisePurchaseReport
        '
        Me.MnuItemWisePurchaseReport.Name = "MnuItemWisePurchaseReport"
        Me.MnuItemWisePurchaseReport.Size = New System.Drawing.Size(215, 22)
        Me.MnuItemWisePurchaseReport.Tag = "Report"
        Me.MnuItemWisePurchaseReport.Text = "Item Wise Purchase Report"
        '
        'MnuDistributorQuery
        '
        Me.MnuDistributorQuery.Name = "MnuDistributorQuery"
        Me.MnuDistributorQuery.Size = New System.Drawing.Size(215, 22)
        Me.MnuDistributorQuery.Text = "Distributor Query"
        '
        'MnuSaleInvoice
        '
        Me.MnuSaleInvoice.Name = "MnuSaleInvoice"
        Me.MnuSaleInvoice.Size = New System.Drawing.Size(205, 22)
        Me.MnuSaleInvoice.Text = "Sale Invoice"
        '
        'MnuMaterialIssueFromStore
        '
        Me.MnuMaterialIssueFromStore.Name = "MnuMaterialIssueFromStore"
        Me.MnuMaterialIssueFromStore.Size = New System.Drawing.Size(205, 22)
        Me.MnuMaterialIssueFromStore.Text = "Material Issue from Store"
        '
        'MnuMaterialReceiveInStore
        '
        Me.MnuMaterialReceiveInStore.Name = "MnuMaterialReceiveInStore"
        Me.MnuMaterialReceiveInStore.Size = New System.Drawing.Size(205, 22)
        Me.MnuMaterialReceiveInStore.Text = "Material Receive In Store"
        '
        'MnuDifferentialIncome
        '
        Me.MnuDifferentialIncome.Name = "MnuDifferentialIncome"
        Me.MnuDifferentialIncome.Size = New System.Drawing.Size(205, 22)
        Me.MnuDifferentialIncome.Text = "Differential Income"
        '
        'MDIMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.ClientSize = New System.Drawing.Size(965, 661)
        Me.Controls.Add(Me.MnuMain)
        Me.IsMdiContainer = True
        Me.KeyPreview = True
        Me.MainMenuStrip = Me.MnuMain
        Me.Name = "MDIMain"
        Me.Text = "Customise"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.MnuMain.ResumeLayout(False)
        Me.MnuMain.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStripMenuItem10 As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents MnuMain As System.Windows.Forms.MenuStrip
    Friend WithEvents MnuTransaction As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuMaster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnnReports As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuSaleRegister As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuPurchaseInvoice As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuSale As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuPurchaseReport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuItemCategoryMaster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuItemGroupMaster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuItemMaster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuPartyMaster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuCityMaster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuPaymentEntry As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuMoneyReceiptEntry As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuDebitNoteEntry As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuCreditNoteEntry As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuItemWiseSaleReport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuItemWisePurchaseReport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuDistributorMaster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuDistributorQuery As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuSaleInvoice As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuMaterialIssueFromStore As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuMaterialReceiveInStore As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuDifferentialIncome As System.Windows.Forms.ToolStripMenuItem

End Class
