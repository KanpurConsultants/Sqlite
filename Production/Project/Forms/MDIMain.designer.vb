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
        Me.MnuMain = New System.Windows.Forms.MenuStrip
        Me.MnuProduction = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuMasterProduction = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuJobWorker = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuJobOrder = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuJobOrderCancel = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuJobOrderAmendment = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuJobReceive = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuJobQC = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuJobInvoice = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuJobInvoiceAmendment = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuJobTDS = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuJobPayment = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuPaymentAdjustment = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuJobDebitNote = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuJobCreditNote = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuJobConsumption = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuMaterialCostConversion = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuReportsProduction = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuJobOrderReport = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuJobReceiveReport = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuJobInvoiceReport = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuProcessOrderStatus = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuJobReceiveStatus = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuProcessBalanceReport = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuMaterialIssueFromJobOrderReport = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuJobQCReport = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuPeriodicJobOrderStatus = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuPaymentCalculation = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuPaymentAdvise = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuTimeIncentive = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuTimePenalty = New System.Windows.Forms.ToolStripMenuItem
        Me.MnuMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'MnuMain
        '
        Me.MnuMain.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuProduction})
        Me.MnuMain.Location = New System.Drawing.Point(0, 0)
        Me.MnuMain.Name = "MnuMain"
        Me.MnuMain.Size = New System.Drawing.Size(804, 24)
        Me.MnuMain.TabIndex = 1
        Me.MnuMain.Text = "MenuStrip1"
        '
        'MnuProduction
        '
        Me.MnuProduction.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuMasterProduction, Me.MnuJobOrder, Me.MnuJobOrderCancel, Me.MnuJobOrderAmendment, Me.MnuJobReceive, Me.MnuJobQC, Me.MnuJobInvoice, Me.MnuJobInvoiceAmendment, Me.MnuJobTDS, Me.MnuJobPayment, Me.MnuPaymentAdjustment, Me.MnuJobDebitNote, Me.MnuJobCreditNote, Me.MnuTimeIncentive, Me.MnuTimePenalty, Me.MnuJobConsumption, Me.MnuMaterialCostConversion, Me.MnuReportsProduction})
        Me.MnuProduction.Name = "MnuProduction"
        Me.MnuProduction.Size = New System.Drawing.Size(78, 20)
        Me.MnuProduction.Text = "Production"
        '
        'MnuMasterProduction
        '
        Me.MnuMasterProduction.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuJobWorker})
        Me.MnuMasterProduction.Name = "MnuMasterProduction"
        Me.MnuMasterProduction.Size = New System.Drawing.Size(207, 22)
        Me.MnuMasterProduction.Text = "Master"
        '
        'MnuJobWorker
        '
        Me.MnuJobWorker.Name = "MnuJobWorker"
        Me.MnuJobWorker.Size = New System.Drawing.Size(133, 22)
        Me.MnuJobWorker.Text = "Job Worker"
        '
        'MnuJobOrder
        '
        Me.MnuJobOrder.Name = "MnuJobOrder"
        Me.MnuJobOrder.Size = New System.Drawing.Size(207, 22)
        Me.MnuJobOrder.Text = "Job Order"
        '
        'MnuJobOrderCancel
        '
        Me.MnuJobOrderCancel.Name = "MnuJobOrderCancel"
        Me.MnuJobOrderCancel.Size = New System.Drawing.Size(207, 22)
        Me.MnuJobOrderCancel.Text = "Job Order Cancel"
        '
        'MnuJobOrderAmendment
        '
        Me.MnuJobOrderAmendment.Name = "MnuJobOrderAmendment"
        Me.MnuJobOrderAmendment.Size = New System.Drawing.Size(207, 22)
        Me.MnuJobOrderAmendment.Text = "Job Order Amendment"
        '
        'MnuJobReceive
        '
        Me.MnuJobReceive.Name = "MnuJobReceive"
        Me.MnuJobReceive.Size = New System.Drawing.Size(207, 22)
        Me.MnuJobReceive.Text = "Job Receive"
        '
        'MnuJobQC
        '
        Me.MnuJobQC.Name = "MnuJobQC"
        Me.MnuJobQC.Size = New System.Drawing.Size(207, 22)
        Me.MnuJobQC.Text = "Job QC"
        '
        'MnuJobInvoice
        '
        Me.MnuJobInvoice.Name = "MnuJobInvoice"
        Me.MnuJobInvoice.Size = New System.Drawing.Size(207, 22)
        Me.MnuJobInvoice.Text = "Job Invoice"
        '
        'MnuJobInvoiceAmendment
        '
        Me.MnuJobInvoiceAmendment.Name = "MnuJobInvoiceAmendment"
        Me.MnuJobInvoiceAmendment.Size = New System.Drawing.Size(207, 22)
        Me.MnuJobInvoiceAmendment.Text = "Job Invoice Amendment"
        '
        'MnuJobTDS
        '
        Me.MnuJobTDS.Name = "MnuJobTDS"
        Me.MnuJobTDS.Size = New System.Drawing.Size(207, 22)
        Me.MnuJobTDS.Text = "Job TDS"
        '
        'MnuJobPayment
        '
        Me.MnuJobPayment.Name = "MnuJobPayment"
        Me.MnuJobPayment.Size = New System.Drawing.Size(207, 22)
        Me.MnuJobPayment.Text = "Job Payment"
        '
        'MnuPaymentAdjustment
        '
        Me.MnuPaymentAdjustment.Name = "MnuPaymentAdjustment"
        Me.MnuPaymentAdjustment.Size = New System.Drawing.Size(207, 22)
        Me.MnuPaymentAdjustment.Text = "Payment Adjustment"
        '
        'MnuJobDebitNote
        '
        Me.MnuJobDebitNote.Name = "MnuJobDebitNote"
        Me.MnuJobDebitNote.Size = New System.Drawing.Size(207, 22)
        Me.MnuJobDebitNote.Text = "Debit Note"
        '
        'MnuJobCreditNote
        '
        Me.MnuJobCreditNote.Name = "MnuJobCreditNote"
        Me.MnuJobCreditNote.Size = New System.Drawing.Size(207, 22)
        Me.MnuJobCreditNote.Text = "Credit Note"
        '
        'MnuJobConsumption
        '
        Me.MnuJobConsumption.Name = "MnuJobConsumption"
        Me.MnuJobConsumption.Size = New System.Drawing.Size(207, 22)
        Me.MnuJobConsumption.Text = "Job Consumption"
        '
        'MnuMaterialCostConversion
        '
        Me.MnuMaterialCostConversion.Name = "MnuMaterialCostConversion"
        Me.MnuMaterialCostConversion.Size = New System.Drawing.Size(207, 22)
        Me.MnuMaterialCostConversion.Text = "Material Cost Conversion"
        '
        'MnuReportsProduction
        '
        Me.MnuReportsProduction.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MnuJobOrderReport, Me.MnuJobReceiveReport, Me.MnuJobInvoiceReport, Me.MnuProcessOrderStatus, Me.MnuJobReceiveStatus, Me.MnuProcessBalanceReport, Me.MnuMaterialIssueFromJobOrderReport, Me.MnuJobQCReport, Me.MnuPeriodicJobOrderStatus, Me.MnuPaymentCalculation, Me.MnuPaymentAdvise})
        Me.MnuReportsProduction.Name = "MnuReportsProduction"
        Me.MnuReportsProduction.Size = New System.Drawing.Size(207, 22)
        Me.MnuReportsProduction.Text = "Reports"
        '
        'MnuJobOrderReport
        '
        Me.MnuJobOrderReport.Name = "MnuJobOrderReport"
        Me.MnuJobOrderReport.Size = New System.Drawing.Size(269, 22)
        Me.MnuJobOrderReport.Tag = "Report"
        Me.MnuJobOrderReport.Text = "Job Order Report"
        '
        'MnuJobReceiveReport
        '
        Me.MnuJobReceiveReport.Name = "MnuJobReceiveReport"
        Me.MnuJobReceiveReport.Size = New System.Drawing.Size(269, 22)
        Me.MnuJobReceiveReport.Tag = "Report"
        Me.MnuJobReceiveReport.Text = "Job Receive Report"
        '
        'MnuJobInvoiceReport
        '
        Me.MnuJobInvoiceReport.Name = "MnuJobInvoiceReport"
        Me.MnuJobInvoiceReport.Size = New System.Drawing.Size(269, 22)
        Me.MnuJobInvoiceReport.Tag = "Report"
        Me.MnuJobInvoiceReport.Text = "Job Invoice Report"
        '
        'MnuProcessOrderStatus
        '
        Me.MnuProcessOrderStatus.Name = "MnuProcessOrderStatus"
        Me.MnuProcessOrderStatus.Size = New System.Drawing.Size(269, 22)
        Me.MnuProcessOrderStatus.Tag = "Report"
        Me.MnuProcessOrderStatus.Text = "Process Order Status"
        '
        'MnuJobReceiveStatus
        '
        Me.MnuJobReceiveStatus.Name = "MnuJobReceiveStatus"
        Me.MnuJobReceiveStatus.Size = New System.Drawing.Size(269, 22)
        Me.MnuJobReceiveStatus.Tag = "Report"
        Me.MnuJobReceiveStatus.Text = "Job Receive Status"
        '
        'MnuProcessBalanceReport
        '
        Me.MnuProcessBalanceReport.Name = "MnuProcessBalanceReport"
        Me.MnuProcessBalanceReport.Size = New System.Drawing.Size(269, 22)
        Me.MnuProcessBalanceReport.Tag = "Report"
        Me.MnuProcessBalanceReport.Text = "Process Balance Report"
        '
        'MnuMaterialIssueFromJobOrderReport
        '
        Me.MnuMaterialIssueFromJobOrderReport.Name = "MnuMaterialIssueFromJobOrderReport"
        Me.MnuMaterialIssueFromJobOrderReport.Size = New System.Drawing.Size(269, 22)
        Me.MnuMaterialIssueFromJobOrderReport.Tag = "Report"
        Me.MnuMaterialIssueFromJobOrderReport.Text = "Material Issue From Job Order Report"
        '
        'MnuJobQCReport
        '
        Me.MnuJobQCReport.Name = "MnuJobQCReport"
        Me.MnuJobQCReport.Size = New System.Drawing.Size(269, 22)
        Me.MnuJobQCReport.Tag = "Report"
        Me.MnuJobQCReport.Text = "Job QC Report"
        '
        'MnuPeriodicJobOrderStatus
        '
        Me.MnuPeriodicJobOrderStatus.Name = "MnuPeriodicJobOrderStatus"
        Me.MnuPeriodicJobOrderStatus.Size = New System.Drawing.Size(269, 22)
        Me.MnuPeriodicJobOrderStatus.Tag = "Report"
        Me.MnuPeriodicJobOrderStatus.Text = "Periodic Job Order Status"
        '
        'MnuPaymentCalculation
        '
        Me.MnuPaymentCalculation.Name = "MnuPaymentCalculation"
        Me.MnuPaymentCalculation.Size = New System.Drawing.Size(269, 22)
        Me.MnuPaymentCalculation.Tag = "Report"
        Me.MnuPaymentCalculation.Text = "Payment Calculation"
        '
        'MnuPaymentAdvise
        '
        Me.MnuPaymentAdvise.Name = "MnuPaymentAdvise"
        Me.MnuPaymentAdvise.Size = New System.Drawing.Size(269, 22)
        Me.MnuPaymentAdvise.Tag = "Report"
        Me.MnuPaymentAdvise.Text = "Payment Advise"
        '
        'MnuTimeIncentive
        '
        Me.MnuTimeIncentive.Name = "MnuTimeIncentive"
        Me.MnuTimeIncentive.Size = New System.Drawing.Size(207, 22)
        Me.MnuTimeIncentive.Text = "Time Incentive"
        '
        'MnuTimePenalty
        '
        Me.MnuTimePenalty.Name = "MnuTimePenalty"
        Me.MnuTimePenalty.Size = New System.Drawing.Size(207, 22)
        Me.MnuTimePenalty.Text = "Time Penalty"
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
        Me.Text = "Production"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.MnuMain.ResumeLayout(False)
        Me.MnuMain.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MnuMain As System.Windows.Forms.MenuStrip
    Friend WithEvents MnuProduction As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuJobOrder As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuJobOrderCancel As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuJobReceive As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuJobInvoice As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuJobOrderAmendment As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuReportsProduction As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuJobOrderReport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuJobReceiveReport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuProcessBalanceReport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuProcessOrderStatus As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuJobReceiveStatus As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuJobInvoiceReport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuJobConsumption As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuMaterialCostConversion As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuMasterProduction As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuJobWorker As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuJobInvoiceAmendment As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuJobQC As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuMaterialIssueFromJobOrderReport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuJobQCReport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuPeriodicJobOrderStatus As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuPaymentCalculation As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuPaymentAdvise As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuJobPayment As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuJobTDS As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuPaymentAdjustment As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuJobDebitNote As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuJobCreditNote As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuTimeIncentive As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MnuTimePenalty As System.Windows.Forms.ToolStripMenuItem

End Class
