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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MDIMain))
        Me.MnuMain = New System.Windows.Forms.MenuStrip()
        Me.MnuInventory = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuStoreMaster = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuItemMaster = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuItemMasterCloth = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuItemGroup = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuItemCategory = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuGodown = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuItemReportingGroup = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuItemInvoiceGroup = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuItemRateGroup = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuPartyRateGroup = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuQCGroupMaster = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuUnitConversion = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuCustomerMaster = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuSupplierMaster = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuAgentMaster = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuVatCommodityCode = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuTariffHeading = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuTermCondition = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuRateList = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuDimension1Master = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuDimension2Master = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuShiftMaster = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuComputerMaster = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuReasonMaster = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuRateTypeMaster = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuStoreTransactions = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuItemRequisition = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuItemRequisitionApproval = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuItemIssueFromStore = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuItemReceiveInStore = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuInternalProcess = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuStockTransfer = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuPhysicalStockEntry = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuPhysicalStockAdjustmentEntry = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuGatePassEntry = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuStoreReports = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuRequisitionReport = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuRequisitionStatus = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuItemIssueReport = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuItemReceiveReport = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuStockTransferReport = New System.Windows.Forms.ToolStripMenuItem()
        Me.PhysicalStockReportToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuStockInHand = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuStockInProcess = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuStockBalance = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuMaterialIssueSummary = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuMaterialReceiveSummary = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuStockTransferSummary = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuStockBalanceValuation = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuStockBalanceWitAverageRate = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuUpdateTableStructure = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuDepartment = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'MnuMain
        '
        Me.MnuMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuInventory})
        Me.MnuMain.Location = New System.Drawing.Point(0, 0)
        Me.MnuMain.Name = "MnuMain"
        Me.MnuMain.Size = New System.Drawing.Size(965, 24)
        Me.MnuMain.TabIndex = 1
        Me.MnuMain.Text = "MenuStrip1"
        '
        'MnuInventory
        '
        Me.MnuInventory.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuStoreMaster, Me.MnuStoreTransactions, Me.MnuStoreReports, Me.MnuUpdateTableStructure})
        Me.MnuInventory.Name = "MnuInventory"
        Me.MnuInventory.Size = New System.Drawing.Size(46, 20)
        Me.MnuInventory.Text = "Store"
        '
        'MnuStoreMaster
        '
        Me.MnuStoreMaster.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuItemMaster, Me.MnuItemMasterCloth, Me.MnuItemGroup, Me.MnuItemCategory, Me.MnuGodown, Me.MnuItemReportingGroup, Me.MnuItemInvoiceGroup, Me.MnuItemRateGroup, Me.MnuPartyRateGroup, Me.MnuQCGroupMaster, Me.MnuUnitConversion, Me.MnuCustomerMaster, Me.MnuSupplierMaster, Me.MnuAgentMaster, Me.MnuVatCommodityCode, Me.MnuTariffHeading, Me.MnuTermCondition, Me.MnuRateList, Me.MnuDimension1Master, Me.MnuDimension2Master, Me.MnuShiftMaster, Me.MnuComputerMaster, Me.MnuReasonMaster, Me.MnuRateTypeMaster, Me.MnuDepartment})
        Me.MnuStoreMaster.Name = "MnuStoreMaster"
        Me.MnuStoreMaster.Size = New System.Drawing.Size(195, 22)
        Me.MnuStoreMaster.Text = "Master"
        '
        'MnuItemMaster
        '
        Me.MnuItemMaster.Name = "MnuItemMaster"
        Me.MnuItemMaster.Size = New System.Drawing.Size(210, 22)
        Me.MnuItemMaster.Text = "Item Master"
        '
        'MnuItemMasterCloth
        '
        Me.MnuItemMasterCloth.Name = "MnuItemMasterCloth"
        Me.MnuItemMasterCloth.Size = New System.Drawing.Size(210, 22)
        Me.MnuItemMasterCloth.Text = "Item Master (Cloth)"
        '
        'MnuItemGroup
        '
        Me.MnuItemGroup.Name = "MnuItemGroup"
        Me.MnuItemGroup.Size = New System.Drawing.Size(210, 22)
        Me.MnuItemGroup.Text = "Item Group"
        '
        'MnuItemCategory
        '
        Me.MnuItemCategory.Name = "MnuItemCategory"
        Me.MnuItemCategory.Size = New System.Drawing.Size(210, 22)
        Me.MnuItemCategory.Text = "Item Category"
        '
        'MnuGodown
        '
        Me.MnuGodown.Name = "MnuGodown"
        Me.MnuGodown.Size = New System.Drawing.Size(210, 22)
        Me.MnuGodown.Text = "Godown"
        '
        'MnuItemReportingGroup
        '
        Me.MnuItemReportingGroup.Name = "MnuItemReportingGroup"
        Me.MnuItemReportingGroup.Size = New System.Drawing.Size(210, 22)
        Me.MnuItemReportingGroup.Text = "Item Reporting Group"
        '
        'MnuItemInvoiceGroup
        '
        Me.MnuItemInvoiceGroup.Name = "MnuItemInvoiceGroup"
        Me.MnuItemInvoiceGroup.Size = New System.Drawing.Size(210, 22)
        Me.MnuItemInvoiceGroup.Text = "Item Invoice Group"
        '
        'MnuItemRateGroup
        '
        Me.MnuItemRateGroup.Name = "MnuItemRateGroup"
        Me.MnuItemRateGroup.Size = New System.Drawing.Size(210, 22)
        Me.MnuItemRateGroup.Text = "Item Rate Group"
        '
        'MnuPartyRateGroup
        '
        Me.MnuPartyRateGroup.Name = "MnuPartyRateGroup"
        Me.MnuPartyRateGroup.Size = New System.Drawing.Size(210, 22)
        Me.MnuPartyRateGroup.Text = "Party Rate Group"
        '
        'MnuQCGroupMaster
        '
        Me.MnuQCGroupMaster.Name = "MnuQCGroupMaster"
        Me.MnuQCGroupMaster.Size = New System.Drawing.Size(210, 22)
        Me.MnuQCGroupMaster.Text = "QC Group Master"
        '
        'MnuUnitConversion
        '
        Me.MnuUnitConversion.Name = "MnuUnitConversion"
        Me.MnuUnitConversion.Size = New System.Drawing.Size(210, 22)
        Me.MnuUnitConversion.Text = "Unit Conversion"
        '
        'MnuCustomerMaster
        '
        Me.MnuCustomerMaster.Name = "MnuCustomerMaster"
        Me.MnuCustomerMaster.Size = New System.Drawing.Size(210, 22)
        Me.MnuCustomerMaster.Text = "Customer Master"
        '
        'MnuSupplierMaster
        '
        Me.MnuSupplierMaster.Name = "MnuSupplierMaster"
        Me.MnuSupplierMaster.Size = New System.Drawing.Size(210, 22)
        Me.MnuSupplierMaster.Text = "Supplier Master"
        '
        'MnuAgentMaster
        '
        Me.MnuAgentMaster.Name = "MnuAgentMaster"
        Me.MnuAgentMaster.Size = New System.Drawing.Size(210, 22)
        Me.MnuAgentMaster.Text = "Agent Master"
        '
        'MnuVatCommodityCode
        '
        Me.MnuVatCommodityCode.Name = "MnuVatCommodityCode"
        Me.MnuVatCommodityCode.Size = New System.Drawing.Size(210, 22)
        Me.MnuVatCommodityCode.Text = "Vat Commodity Code"
        '
        'MnuTariffHeading
        '
        Me.MnuTariffHeading.Name = "MnuTariffHeading"
        Me.MnuTariffHeading.Size = New System.Drawing.Size(210, 22)
        Me.MnuTariffHeading.Text = "Tariff Heading"
        '
        'MnuTermCondition
        '
        Me.MnuTermCondition.Name = "MnuTermCondition"
        Me.MnuTermCondition.Size = New System.Drawing.Size(210, 22)
        Me.MnuTermCondition.Text = "Term && Condition Master"
        '
        'MnuRateList
        '
        Me.MnuRateList.Name = "MnuRateList"
        Me.MnuRateList.Size = New System.Drawing.Size(210, 22)
        Me.MnuRateList.Text = "Rate List"
        '
        'MnuDimension1Master
        '
        Me.MnuDimension1Master.Name = "MnuDimension1Master"
        Me.MnuDimension1Master.Size = New System.Drawing.Size(210, 22)
        Me.MnuDimension1Master.Text = "Dimension1 Master"
        '
        'MnuDimension2Master
        '
        Me.MnuDimension2Master.Name = "MnuDimension2Master"
        Me.MnuDimension2Master.Size = New System.Drawing.Size(210, 22)
        Me.MnuDimension2Master.Text = "Dimension2 Master"
        '
        'MnuShiftMaster
        '
        Me.MnuShiftMaster.Name = "MnuShiftMaster"
        Me.MnuShiftMaster.Size = New System.Drawing.Size(210, 22)
        Me.MnuShiftMaster.Text = "Shift Master"
        '
        'MnuComputerMaster
        '
        Me.MnuComputerMaster.Name = "MnuComputerMaster"
        Me.MnuComputerMaster.Size = New System.Drawing.Size(210, 22)
        Me.MnuComputerMaster.Text = "Computer Master"
        '
        'MnuReasonMaster
        '
        Me.MnuReasonMaster.Name = "MnuReasonMaster"
        Me.MnuReasonMaster.Size = New System.Drawing.Size(210, 22)
        Me.MnuReasonMaster.Text = "Reason Master"
        '
        'MnuRateTypeMaster
        '
        Me.MnuRateTypeMaster.Name = "MnuRateTypeMaster"
        Me.MnuRateTypeMaster.Size = New System.Drawing.Size(210, 22)
        Me.MnuRateTypeMaster.Text = "Rate Type Master"
        '
        'MnuStoreTransactions
        '
        Me.MnuStoreTransactions.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuItemRequisition, Me.MnuItemRequisitionApproval, Me.MnuItemIssueFromStore, Me.MnuItemReceiveInStore, Me.MnuInternalProcess, Me.MnuStockTransfer, Me.MnuPhysicalStockEntry, Me.MnuPhysicalStockAdjustmentEntry, Me.MnuGatePassEntry})
        Me.MnuStoreTransactions.Name = "MnuStoreTransactions"
        Me.MnuStoreTransactions.Size = New System.Drawing.Size(195, 22)
        Me.MnuStoreTransactions.Text = "Transactions"
        '
        'MnuItemRequisition
        '
        Me.MnuItemRequisition.Name = "MnuItemRequisition"
        Me.MnuItemRequisition.Size = New System.Drawing.Size(244, 22)
        Me.MnuItemRequisition.Text = "Item Requisition"
        '
        'MnuItemRequisitionApproval
        '
        Me.MnuItemRequisitionApproval.Name = "MnuItemRequisitionApproval"
        Me.MnuItemRequisitionApproval.Size = New System.Drawing.Size(244, 22)
        Me.MnuItemRequisitionApproval.Text = "Item Requisition Approval"
        '
        'MnuItemIssueFromStore
        '
        Me.MnuItemIssueFromStore.Name = "MnuItemIssueFromStore"
        Me.MnuItemIssueFromStore.Size = New System.Drawing.Size(244, 22)
        Me.MnuItemIssueFromStore.Text = "Item Issue From Store"
        '
        'MnuItemReceiveInStore
        '
        Me.MnuItemReceiveInStore.Name = "MnuItemReceiveInStore"
        Me.MnuItemReceiveInStore.Size = New System.Drawing.Size(244, 22)
        Me.MnuItemReceiveInStore.Text = "Item Receive In Store"
        '
        'MnuInternalProcess
        '
        Me.MnuInternalProcess.Name = "MnuInternalProcess"
        Me.MnuInternalProcess.Size = New System.Drawing.Size(244, 22)
        Me.MnuInternalProcess.Text = "Internal Process"
        '
        'MnuStockTransfer
        '
        Me.MnuStockTransfer.Name = "MnuStockTransfer"
        Me.MnuStockTransfer.Size = New System.Drawing.Size(244, 22)
        Me.MnuStockTransfer.Text = "Stock Transfer"
        '
        'MnuPhysicalStockEntry
        '
        Me.MnuPhysicalStockEntry.Name = "MnuPhysicalStockEntry"
        Me.MnuPhysicalStockEntry.Size = New System.Drawing.Size(244, 22)
        Me.MnuPhysicalStockEntry.Text = "Physical Stock Entry"
        '
        'MnuPhysicalStockAdjustmentEntry
        '
        Me.MnuPhysicalStockAdjustmentEntry.Name = "MnuPhysicalStockAdjustmentEntry"
        Me.MnuPhysicalStockAdjustmentEntry.Size = New System.Drawing.Size(244, 22)
        Me.MnuPhysicalStockAdjustmentEntry.Text = "Physical Stock Adjustment Entry"
        '
        'MnuGatePassEntry
        '
        Me.MnuGatePassEntry.Name = "MnuGatePassEntry"
        Me.MnuGatePassEntry.Size = New System.Drawing.Size(244, 22)
        Me.MnuGatePassEntry.Text = "Gate Pass Entry"
        '
        'MnuStoreReports
        '
        Me.MnuStoreReports.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuRequisitionReport, Me.MnuRequisitionStatus, Me.MnuItemIssueReport, Me.MnuItemReceiveReport, Me.MnuStockTransferReport, Me.PhysicalStockReportToolStripMenuItem, Me.MnuStockInHand, Me.MnuStockInProcess, Me.MnuStockBalance, Me.MnuMaterialIssueSummary, Me.MnuMaterialReceiveSummary, Me.MnuStockTransferSummary, Me.MnuStockBalanceValuation, Me.MnuStockBalanceWitAverageRate})
        Me.MnuStoreReports.Name = "MnuStoreReports"
        Me.MnuStoreReports.Size = New System.Drawing.Size(195, 22)
        Me.MnuStoreReports.Tag = ""
        Me.MnuStoreReports.Text = "Reports"
        '
        'MnuRequisitionReport
        '
        Me.MnuRequisitionReport.Name = "MnuRequisitionReport"
        Me.MnuRequisitionReport.Size = New System.Drawing.Size(247, 22)
        Me.MnuRequisitionReport.Tag = "Report"
        Me.MnuRequisitionReport.Text = "Requisition Report"
        '
        'MnuRequisitionStatus
        '
        Me.MnuRequisitionStatus.Name = "MnuRequisitionStatus"
        Me.MnuRequisitionStatus.Size = New System.Drawing.Size(247, 22)
        Me.MnuRequisitionStatus.Tag = "Report"
        Me.MnuRequisitionStatus.Text = "Requisition Status"
        '
        'MnuItemIssueReport
        '
        Me.MnuItemIssueReport.Name = "MnuItemIssueReport"
        Me.MnuItemIssueReport.Size = New System.Drawing.Size(247, 22)
        Me.MnuItemIssueReport.Tag = "Report"
        Me.MnuItemIssueReport.Text = "Item Issue Report"
        '
        'MnuItemReceiveReport
        '
        Me.MnuItemReceiveReport.Name = "MnuItemReceiveReport"
        Me.MnuItemReceiveReport.Size = New System.Drawing.Size(247, 22)
        Me.MnuItemReceiveReport.Tag = "Report"
        Me.MnuItemReceiveReport.Text = "Item Receive Report"
        '
        'MnuStockTransferReport
        '
        Me.MnuStockTransferReport.Name = "MnuStockTransferReport"
        Me.MnuStockTransferReport.Size = New System.Drawing.Size(247, 22)
        Me.MnuStockTransferReport.Tag = "Report"
        Me.MnuStockTransferReport.Text = "Stock Transfer Report"
        '
        'PhysicalStockReportToolStripMenuItem
        '
        Me.PhysicalStockReportToolStripMenuItem.Name = "PhysicalStockReportToolStripMenuItem"
        Me.PhysicalStockReportToolStripMenuItem.Size = New System.Drawing.Size(247, 22)
        Me.PhysicalStockReportToolStripMenuItem.Tag = "Report"
        Me.PhysicalStockReportToolStripMenuItem.Text = "Physical Stock Report"
        '
        'MnuStockInHand
        '
        Me.MnuStockInHand.Name = "MnuStockInHand"
        Me.MnuStockInHand.Size = New System.Drawing.Size(247, 22)
        Me.MnuStockInHand.Tag = "Report"
        Me.MnuStockInHand.Text = "Stock In Hand"
        '
        'MnuStockInProcess
        '
        Me.MnuStockInProcess.Name = "MnuStockInProcess"
        Me.MnuStockInProcess.Size = New System.Drawing.Size(247, 22)
        Me.MnuStockInProcess.Tag = "Report"
        Me.MnuStockInProcess.Text = "Stock In Process"
        '
        'MnuStockBalance
        '
        Me.MnuStockBalance.Name = "MnuStockBalance"
        Me.MnuStockBalance.Size = New System.Drawing.Size(247, 22)
        Me.MnuStockBalance.Tag = "Report"
        Me.MnuStockBalance.Text = "Stock Balance"
        '
        'MnuMaterialIssueSummary
        '
        Me.MnuMaterialIssueSummary.Name = "MnuMaterialIssueSummary"
        Me.MnuMaterialIssueSummary.Size = New System.Drawing.Size(247, 22)
        Me.MnuMaterialIssueSummary.Tag = "Report"
        Me.MnuMaterialIssueSummary.Text = "Material Issue Summary"
        '
        'MnuMaterialReceiveSummary
        '
        Me.MnuMaterialReceiveSummary.Name = "MnuMaterialReceiveSummary"
        Me.MnuMaterialReceiveSummary.Size = New System.Drawing.Size(247, 22)
        Me.MnuMaterialReceiveSummary.Tag = "Report"
        Me.MnuMaterialReceiveSummary.Text = "Material Receive Summary"
        '
        'MnuStockTransferSummary
        '
        Me.MnuStockTransferSummary.Name = "MnuStockTransferSummary"
        Me.MnuStockTransferSummary.Size = New System.Drawing.Size(247, 22)
        Me.MnuStockTransferSummary.Tag = "Report"
        Me.MnuStockTransferSummary.Text = "Stock Transfer Summary"
        '
        'MnuStockBalanceValuation
        '
        Me.MnuStockBalanceValuation.Name = "MnuStockBalanceValuation"
        Me.MnuStockBalanceValuation.Size = New System.Drawing.Size(247, 22)
        Me.MnuStockBalanceValuation.Tag = "Report"
        Me.MnuStockBalanceValuation.Text = "Stock Balance Valuation"
        '
        'MnuStockBalanceWitAverageRate
        '
        Me.MnuStockBalanceWitAverageRate.Name = "MnuStockBalanceWitAverageRate"
        Me.MnuStockBalanceWitAverageRate.Size = New System.Drawing.Size(247, 22)
        Me.MnuStockBalanceWitAverageRate.Tag = "Report"
        Me.MnuStockBalanceWitAverageRate.Text = "Stock Balance With Average Rate"
        '
        'MnuUpdateTableStructure
        '
        Me.MnuUpdateTableStructure.Name = "MnuUpdateTableStructure"
        Me.MnuUpdateTableStructure.Size = New System.Drawing.Size(195, 22)
        Me.MnuUpdateTableStructure.Text = "Update Table Structure"
        '
        'MnuDepartment
        '
        Me.MnuDepartment.Name = "MnuDepartment"
        Me.MnuDepartment.Size = New System.Drawing.Size(210, 22)
        Me.MnuDepartment.Text = "Department"
        '
        'MDIMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.ClientSize = New System.Drawing.Size(965, 661)
        Me.Controls.Add(Me.MnuMain)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.IsMdiContainer = True
        Me.KeyPreview = True
        Me.MainMenuStrip = Me.MnuMain
        Me.Name = "MDIMain"
        Me.Text = "Store"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.MnuMain.ResumeLayout(False)
        Me.MnuMain.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStripMenuItem10 As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents MnuMain As System.Windows.Forms.MenuStrip
    Friend WithEvents MnuInventory As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuStoreMaster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuItemGroup As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuItemCategory As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuGodown As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuStoreTransactions As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuItemRequisition As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuItemRequisitionApproval As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuStoreReports As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuItemReportingGroup As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuItemIssueFromStore As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuItemReceiveInStore As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuStockTransfer As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuRequisitionReport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuItemRateGroup As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuPartyRateGroup As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuRequisitionStatus As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuItemIssueReport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuItemReceiveReport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuItemInvoiceGroup As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuQCGroupMaster As System.Windows.Forms.ToolStripMenuItem
    Public WithEvents MnuItemMaster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuStockTransferReport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PhysicalStockReportToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuStockInHand As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuUnitConversion As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuCustomerMaster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuSupplierMaster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuAgentMaster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuVatCommodityCode As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuTariffHeading As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuTermCondition As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuRateList As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuInternalProcess As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuStockInProcess As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuDimension1Master As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuDimension2Master As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuStockBalance As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuPhysicalStockEntry As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuPhysicalStockAdjustmentEntry As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuShiftMaster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuMaterialIssueSummary As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuMaterialReceiveSummary As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuComputerMaster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuGatePassEntry As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuStockBalanceValuation As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuReasonMaster As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuStockTransferSummary As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuStockBalanceWitAverageRate As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuUpdateTableStructure As ToolStripMenuItem
    Friend WithEvents MnuRateTypeMaster As ToolStripMenuItem
    Friend WithEvents MnuItemMasterCloth As ToolStripMenuItem
    Friend WithEvents MnuDepartment As ToolStripMenuItem
End Class
