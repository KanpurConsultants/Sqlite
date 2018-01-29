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
        Me.MnuJobWork = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuWorkOrder = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuWorkOrderCancellation = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuWorkOrderAmendment = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuWorkOrderDispatch = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuJobWorkReport = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuWorkOrderReport = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuWorkDispatchReport = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuWorkOrderStatus = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuWorkOrderInvoice = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuWorkInvoiceReport = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'MnuMain
        '
        Me.MnuMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuJobWork})
        Me.MnuMain.Location = New System.Drawing.Point(0, 0)
        Me.MnuMain.Name = "MnuMain"
        Me.MnuMain.Size = New System.Drawing.Size(804, 24)
        Me.MnuMain.TabIndex = 1
        Me.MnuMain.Text = "MenuStrip1"
        '
        'MnuJobWork
        '
        Me.MnuJobWork.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuWorkOrder, Me.MnuWorkOrderCancellation, Me.MnuWorkOrderAmendment, Me.MnuWorkOrderDispatch, Me.MnuWorkOrderInvoice, Me.MnuJobWorkReport})
        Me.MnuJobWork.Name = "MnuJobWork"
        Me.MnuJobWork.Size = New System.Drawing.Size(68, 20)
        Me.MnuJobWork.Text = "Job Work"
        '
        'MnuWorkOrder
        '
        Me.MnuWorkOrder.Name = "MnuWorkOrder"
        Me.MnuWorkOrder.Size = New System.Drawing.Size(205, 22)
        Me.MnuWorkOrder.Text = "Work Order"
        '
        'MnuWorkOrderCancellation
        '
        Me.MnuWorkOrderCancellation.Name = "MnuWorkOrderCancellation"
        Me.MnuWorkOrderCancellation.Size = New System.Drawing.Size(205, 22)
        Me.MnuWorkOrderCancellation.Text = "Work Order Cancellation"
        '
        'MnuWorkOrderAmendment
        '
        Me.MnuWorkOrderAmendment.Name = "MnuWorkOrderAmendment"
        Me.MnuWorkOrderAmendment.Size = New System.Drawing.Size(205, 22)
        Me.MnuWorkOrderAmendment.Text = "Work Order Amendment"
        '
        'MnuWorkOrderDispatch
        '
        Me.MnuWorkOrderDispatch.Name = "MnuWorkOrderDispatch"
        Me.MnuWorkOrderDispatch.Size = New System.Drawing.Size(205, 22)
        Me.MnuWorkOrderDispatch.Text = "Work Order Dispatch"
        '
        'MnuJobWorkReport
        '
        Me.MnuJobWorkReport.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuWorkOrderReport, Me.MnuWorkDispatchReport, Me.MnuWorkInvoiceReport, Me.MnuWorkOrderStatus})
        Me.MnuJobWorkReport.Name = "MnuJobWorkReport"
        Me.MnuJobWorkReport.Size = New System.Drawing.Size(205, 22)
        Me.MnuJobWorkReport.Text = "Reports"
        '
        'MnuWorkOrderReport
        '
        Me.MnuWorkOrderReport.Name = "MnuWorkOrderReport"
        Me.MnuWorkOrderReport.Size = New System.Drawing.Size(189, 22)
        Me.MnuWorkOrderReport.Tag = "Report"
        Me.MnuWorkOrderReport.Text = "Work Order Report"
        '
        'MnuWorkDispatchReport
        '
        Me.MnuWorkDispatchReport.Name = "MnuWorkDispatchReport"
        Me.MnuWorkDispatchReport.Size = New System.Drawing.Size(189, 22)
        Me.MnuWorkDispatchReport.Tag = "Report"
        Me.MnuWorkDispatchReport.Text = "Work Dispatch Report"
        '
        'MnuWorkOrderStatus
        '
        Me.MnuWorkOrderStatus.Name = "MnuWorkOrderStatus"
        Me.MnuWorkOrderStatus.Size = New System.Drawing.Size(189, 22)
        Me.MnuWorkOrderStatus.Tag = "Report"
        Me.MnuWorkOrderStatus.Text = "Work Order Status"
        '
        'MnuWorkOrderInvoice
        '
        Me.MnuWorkOrderInvoice.Name = "MnuWorkOrderInvoice"
        Me.MnuWorkOrderInvoice.Size = New System.Drawing.Size(205, 22)
        Me.MnuWorkOrderInvoice.Text = "Work Order Invoice"
        '
        'MnuWorkInvoiceReport
        '
        Me.MnuWorkInvoiceReport.Name = "MnuWorkInvoiceReport"
        Me.MnuWorkInvoiceReport.Size = New System.Drawing.Size(189, 22)
        Me.MnuWorkInvoiceReport.Tag = "Report"
        Me.MnuWorkInvoiceReport.Text = "Work Invoice Report"
        '
        'MDIMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.ClientSize = New System.Drawing.Size(804, 578)
        Me.Controls.Add(Me.MnuMain)
        Me.IsMdiContainer = True
        Me.KeyPreview = True
        Me.MainMenuStrip = Me.MnuMain
        Me.Name = "MDIMain"
        Me.Text = "Job Work"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.MnuMain.ResumeLayout(False)
        Me.MnuMain.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MnuMain As System.Windows.Forms.MenuStrip
    Friend WithEvents MnuJobWork As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuWorkOrder As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuWorkOrderCancellation As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuWorkOrderAmendment As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuJobWorkReport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuWorkOrderReport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuWorkOrderStatus As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuWorkOrderDispatch As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuWorkDispatchReport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuWorkOrderInvoice As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuWorkInvoiceReport As System.Windows.Forms.ToolStripMenuItem

End Class
