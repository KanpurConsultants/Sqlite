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
        Me.MnuPurchase = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuPurchaseIndent = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuPurchaseIndentCancel = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuPurchaseOrder = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuPurchaseOrderCancel = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuPurchaseOrderAmendment = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuPurchaseChallan = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuPurchaseChallanReturn = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuPurchaseInvoice = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuPurchaseSupplimentaryInvoice = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuPurchaseReturn = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuReports = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuPurchaseIndentReport = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuPurchaseIndentStatus = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuPurchaseOrderReport = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuPurchaseOrderStatus = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuPurchaseChallanReport = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuPurchaseChallanStatus = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuPurchaseInvoiceReport = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuUpdateTableStructure = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuPurchaseInvoiceDirect = New System.Windows.Forms.ToolStripMenuItem()
        Me.MnuMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'MnuMain
        '
        Me.MnuMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuPurchase})
        Me.MnuMain.Location = New System.Drawing.Point(0, 0)
        Me.MnuMain.Name = "MnuMain"
        Me.MnuMain.Size = New System.Drawing.Size(804, 24)
        Me.MnuMain.TabIndex = 1
        Me.MnuMain.Text = "MenuStrip1"
        '
        'MnuPurchase
        '
        Me.MnuPurchase.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuPurchaseIndent, Me.MnuPurchaseIndentCancel, Me.MnuPurchaseOrder, Me.MnuPurchaseOrderCancel, Me.MnuPurchaseOrderAmendment, Me.MnuPurchaseChallan, Me.MnuPurchaseChallanReturn, Me.MnuPurchaseInvoice, Me.MnuPurchaseInvoiceDirect, Me.MnuPurchaseSupplimentaryInvoice, Me.MnuPurchaseReturn, Me.MnuReports, Me.MnuUpdateTableStructure})
        Me.MnuPurchase.Name = "MnuPurchase"
        Me.MnuPurchase.Size = New System.Drawing.Size(67, 20)
        Me.MnuPurchase.Text = "Purchase"
        '
        'MnuPurchaseIndent
        '
        Me.MnuPurchaseIndent.Name = "MnuPurchaseIndent"
        Me.MnuPurchaseIndent.Size = New System.Drawing.Size(243, 22)
        Me.MnuPurchaseIndent.Text = "Purchase Indent"
        '
        'MnuPurchaseIndentCancel
        '
        Me.MnuPurchaseIndentCancel.Name = "MnuPurchaseIndentCancel"
        Me.MnuPurchaseIndentCancel.Size = New System.Drawing.Size(243, 22)
        Me.MnuPurchaseIndentCancel.Text = "Purchase Indent Cancel"
        '
        'MnuPurchaseOrder
        '
        Me.MnuPurchaseOrder.Name = "MnuPurchaseOrder"
        Me.MnuPurchaseOrder.Size = New System.Drawing.Size(243, 22)
        Me.MnuPurchaseOrder.Text = "Purchase Order"
        '
        'MnuPurchaseOrderCancel
        '
        Me.MnuPurchaseOrderCancel.Name = "MnuPurchaseOrderCancel"
        Me.MnuPurchaseOrderCancel.Size = New System.Drawing.Size(243, 22)
        Me.MnuPurchaseOrderCancel.Text = "Purchase Order Cancel"
        '
        'MnuPurchaseOrderAmendment
        '
        Me.MnuPurchaseOrderAmendment.Name = "MnuPurchaseOrderAmendment"
        Me.MnuPurchaseOrderAmendment.Size = New System.Drawing.Size(243, 22)
        Me.MnuPurchaseOrderAmendment.Text = "Purchase Order Amendment"
        '
        'MnuPurchaseChallan
        '
        Me.MnuPurchaseChallan.Name = "MnuPurchaseChallan"
        Me.MnuPurchaseChallan.Size = New System.Drawing.Size(243, 22)
        Me.MnuPurchaseChallan.Text = "Purchase Challan"
        '
        'MnuPurchaseChallanReturn
        '
        Me.MnuPurchaseChallanReturn.Name = "MnuPurchaseChallanReturn"
        Me.MnuPurchaseChallanReturn.Size = New System.Drawing.Size(243, 22)
        Me.MnuPurchaseChallanReturn.Text = "Purchase Challan Return"
        '
        'MnuPurchaseInvoice
        '
        Me.MnuPurchaseInvoice.Name = "MnuPurchaseInvoice"
        Me.MnuPurchaseInvoice.Size = New System.Drawing.Size(243, 22)
        Me.MnuPurchaseInvoice.Text = "Purchase Invoice"
        '
        'MnuPurchaseSupplimentaryInvoice
        '
        Me.MnuPurchaseSupplimentaryInvoice.Name = "MnuPurchaseSupplimentaryInvoice"
        Me.MnuPurchaseSupplimentaryInvoice.Size = New System.Drawing.Size(243, 22)
        Me.MnuPurchaseSupplimentaryInvoice.Text = "Purchase Supplimentary Invoice"
        '
        'MnuPurchaseReturn
        '
        Me.MnuPurchaseReturn.Name = "MnuPurchaseReturn"
        Me.MnuPurchaseReturn.Size = New System.Drawing.Size(243, 22)
        Me.MnuPurchaseReturn.Text = "Purchase Return"
        '
        'MnuReports
        '
        Me.MnuReports.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuPurchaseIndentReport, Me.MnuPurchaseIndentStatus, Me.MnuPurchaseOrderReport, Me.MnuPurchaseOrderStatus, Me.MnuPurchaseChallanReport, Me.MnuPurchaseChallanStatus, Me.MnuPurchaseInvoiceReport})
        Me.MnuReports.Name = "MnuReports"
        Me.MnuReports.Size = New System.Drawing.Size(243, 22)
        Me.MnuReports.Text = "Reports"
        '
        'MnuPurchaseIndentReport
        '
        Me.MnuPurchaseIndentReport.Name = "MnuPurchaseIndentReport"
        Me.MnuPurchaseIndentReport.Size = New System.Drawing.Size(203, 22)
        Me.MnuPurchaseIndentReport.Tag = "Report"
        Me.MnuPurchaseIndentReport.Text = "Purchase Indent Report"
        '
        'MnuPurchaseIndentStatus
        '
        Me.MnuPurchaseIndentStatus.Name = "MnuPurchaseIndentStatus"
        Me.MnuPurchaseIndentStatus.Size = New System.Drawing.Size(203, 22)
        Me.MnuPurchaseIndentStatus.Tag = "Report"
        Me.MnuPurchaseIndentStatus.Text = "Purchase Indent Status"
        '
        'MnuPurchaseOrderReport
        '
        Me.MnuPurchaseOrderReport.Name = "MnuPurchaseOrderReport"
        Me.MnuPurchaseOrderReport.Size = New System.Drawing.Size(203, 22)
        Me.MnuPurchaseOrderReport.Tag = "Report"
        Me.MnuPurchaseOrderReport.Text = "Purchase Order Report"
        '
        'MnuPurchaseOrderStatus
        '
        Me.MnuPurchaseOrderStatus.Name = "MnuPurchaseOrderStatus"
        Me.MnuPurchaseOrderStatus.Size = New System.Drawing.Size(203, 22)
        Me.MnuPurchaseOrderStatus.Tag = "Report"
        Me.MnuPurchaseOrderStatus.Text = "Purchase Order Status"
        '
        'MnuPurchaseChallanReport
        '
        Me.MnuPurchaseChallanReport.Name = "MnuPurchaseChallanReport"
        Me.MnuPurchaseChallanReport.Size = New System.Drawing.Size(203, 22)
        Me.MnuPurchaseChallanReport.Tag = "Report"
        Me.MnuPurchaseChallanReport.Text = "Purchase Challan Report"
        '
        'MnuPurchaseChallanStatus
        '
        Me.MnuPurchaseChallanStatus.Name = "MnuPurchaseChallanStatus"
        Me.MnuPurchaseChallanStatus.Size = New System.Drawing.Size(203, 22)
        Me.MnuPurchaseChallanStatus.Tag = "Report"
        Me.MnuPurchaseChallanStatus.Text = "Purchase Challan Status"
        '
        'MnuPurchaseInvoiceReport
        '
        Me.MnuPurchaseInvoiceReport.Name = "MnuPurchaseInvoiceReport"
        Me.MnuPurchaseInvoiceReport.Size = New System.Drawing.Size(203, 22)
        Me.MnuPurchaseInvoiceReport.Tag = "Report"
        Me.MnuPurchaseInvoiceReport.Text = "Purchase Invoice Report"
        '
        'MnuUpdateTableStructure
        '
        Me.MnuUpdateTableStructure.Name = "MnuUpdateTableStructure"
        Me.MnuUpdateTableStructure.Size = New System.Drawing.Size(243, 22)
        Me.MnuUpdateTableStructure.Text = "Update Table Structure"
        '
        'MnuPurchaseInvoiceDirect
        '
        Me.MnuPurchaseInvoiceDirect.Name = "MnuPurchaseInvoiceDirect"
        Me.MnuPurchaseInvoiceDirect.Size = New System.Drawing.Size(243, 22)
        Me.MnuPurchaseInvoiceDirect.Text = "Purchase Invoice Direct"
        '
        'MDIMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.ClientSize = New System.Drawing.Size(804, 578)
        Me.Controls.Add(Me.MnuMain)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.IsMdiContainer = True
        Me.KeyPreview = True
        Me.MainMenuStrip = Me.MnuMain
        Me.Name = "MDIMain"
        Me.Text = "Purchase"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.MnuMain.ResumeLayout(False)
        Me.MnuMain.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MnuMain As System.Windows.Forms.MenuStrip
    Friend WithEvents MnuPurchase As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuPurchaseIndent As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuPurchaseOrder As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuPurchaseOrderCancel As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuPurchaseChallan As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuPurchaseInvoice As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuPurchaseReturn As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuPurchaseOrderAmendment As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuPurchaseSupplimentaryInvoice As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuReports As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuPurchaseIndentReport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuPurchaseIndentStatus As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuPurchaseOrderReport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuPurchaseOrderStatus As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuPurchaseChallanReport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuPurchaseChallanStatus As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuPurchaseInvoiceReport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuPurchaseIndentCancel As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuPurchaseChallanReturn As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuUpdateTableStructure As ToolStripMenuItem
    Friend WithEvents MnuPurchaseInvoiceDirect As ToolStripMenuItem
End Class
