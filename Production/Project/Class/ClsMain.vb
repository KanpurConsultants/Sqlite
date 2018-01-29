Imports System.Data.SQLite
Imports System.IO
Imports CrystalDecisions.CrystalReports.Engine
Public Class ClsMain
    Public CFOpen As New ClsFunction
    Public Const ModuleName As String = "Production"

    Sub New(ByVal AgLibVar As AgLibrary.ClsMain)
        AgL = AgLibVar
        AgPL = New AgLibrary.ClsPrinting(AgL)
        ObjAgTemplate = New AgTemplate.ClsMain(AgL)
        AgIniVar = New AgLibrary.ClsIniVariables(AgL)
        ClsMain_ReportLayout = New ReportLayout.ClsMain(AgL)
        ReportPath = AgL.PubReportPath & "\Process"

        ClsMain.FSetDefaultGodown(PubDefaultGodownCode, PubDefaultGodownName)
        Call IniDtEnviro()
    End Sub

    Public Enum EntryPointType
        Main
        Log
    End Enum

    Public Class PaymentType
        Public Const Payment As String = "Payment"
        Public Const Advance As String = "Advance"
    End Class

    Class Temp_NCat
        Public Const FinishedItemProdPlan As String = "FPRP"

        Public Const YarnSkuStockOpening As String = "YOSTK"
        Public Const WoolStockOpening As String = "WOSTK"
        Public Const OtherMaterialStockOpening As String = "MOSTK"
        Public Const TraceMapStockOpening As String = "TOSTK"

        Public Const YarnSkuAdjustmentIssue As String = "YAISS"
        Public Const YarnSkuAdjustmentReceive As String = "YAREC"
        Public Const OtherMaterialAdjustmentIssue As String = "OSISS"
        Public Const OtherMaterialAdjustmentReceive As String = "OAREC"
        Public Const WoolAdjustmentIssue As String = "WAISS"
        Public Const WoolAdjustmentReceive As String = "WAREC"
        Public Const CarpetAdjustmentIssue As String = "CAISS"
        Public Const CarpetAdjustmentReceive As String = "CAREC"


        Public Const OtherMaterialPlan As String = "OMP"
        Public Const UndyedYarnPlan As String = "UMP"

        Public Const WoolPhysicalStockEntry As String = "WPSTK"
        Public Const YarnPhysicalStockEntry As String = "YPSTK"
        Public Const OtherMaterialPhysicalStockEntry As String = "OMPST"
        Public Const CarpetPhysicalStockEntry As String = "CPSTK"

        Public Const WoolPhysicalAdjustmentEntry As String = "WPSTA"
        Public Const YarnPhysicalAdjustmentEntry As String = "YPSTA"
        Public Const OtherMaterialPhysicalAdjustmentEntry As String = "OPSTA"
        Public Const CarpetPhysicalStockAdjustmentEntry As String = "CPSTA"


        Public Const OtherMaterialTransferIssue As String = "OTRFI"
        Public Const YarnSkuTransferIssue As String = "YTRFI"
        Public Const WoolTransferIssue As String = "WTRFI"
        Public Const TraceMapTransferIssue As String = "TTRFI"

        Public Const OtherMaterialTransferReceive As String = "OTRFR"
        Public Const YarnSkuTransferReceive As String = "YTRFR"
        Public Const WoolTransferReceive As String = "WTRFR"
        Public Const TraceMapTransferReceive As String = "TTRFR"

        Public Const WeavingFinishOrder As String = "WFORD"
        Public Const WeavingOrder As String = "WVORD"
        Public Const WeavingIssue As String = "WVISS"
        Public Const WeavingReceive As String = "WVREC"
        Public Const WeavingReturn As String = "WVRET"
        Public Const WeavingExchange As String = "WVEXC"
        Public Const WeavingPayment As String = "WVPMT"
        Public Const WeavingCancel As String = "WVCNL"
        Public Const LossConsideration As String = "LCON"
        Public Const MaterialPenalty As String = "MPNLT"
        Public Const WeavingConsumptionAdjustment As String = "WCADJ"
        Public Const PurjaTransfer As String = "PJTRF"
        Public Const PurjaTransferAmt As String = "PATRF"
        Public Const RateConversion As String = "RCON"
        Public Const WeavingReceipt As String = "WVRCT"

        Public Const DyeingOrder As String = "DYORD"
        Public Const DyeingIssue As String = "DYISS"
        Public Const DyeingMaterialIssue As String = "DMISS"
        Public Const DyeingReceive As String = "DYREC"
        Public Const DyeingReturn As String = "DYRET"
        Public Const DyeingInvoice As String = "DYINV"
        Public Const DyeingPayment As String = "DYPMT"
        Public Const DyeingCancel As String = "DYCNL"
        Public Const DyeingConsumption As String = "DYCON"
        Public Const ReDyeingOrder As String = "RDORD"
        Public Const ReDyeingReceive As String = "RDREC"
        Public Const DyeingRateConversion As String = "DRCON"

        Public Const SpinningOrder As String = "SPORD"
        Public Const SpinningIssue As String = "SPISS"
        Public Const SpinningMaterialIssue As String = "SMISS"
        Public Const SpinningReceive As String = "SPREC"
        Public Const SpinningInvoice As String = "SPINV"
        Public Const SpinningPayment As String = "SPPMT"
        Public Const SpinningCancel As String = "SPCNL"
        Public Const SpinningConsumption As String = "SPCON"

        Public Const YarnSkuPurchaseIndent As String = "YPIND"
        Public Const OtherMaterialPurchaseIndent As String = "OPIND"
        Public Const WoolPurchaseIndent As String = "WPIND"

        Public Const IndentWool As String = "WINDT"
        Public Const IndentYarn As String = "YINDT"
        Public Const IndentCarpet As String = "CINDT"
        Public Const IndentOther As String = "OINDT"

        Public Const PurchQuotationWool As String = "WPQOT"
        Public Const PurchQuotationYarn As String = "YPQOT"
        Public Const PurchQuotationCarpet As String = "CPQOT"
        Public Const PurchQuotationOther As String = "OPQOT"

        Public Const PurchQuotSelectionWool As String = "WPQS"
        Public Const PurchQuotSelectionYarn As String = "YPQS"
        Public Const PurchQuotSelectionCarpet As String = "CPQS"
        Public Const PurchQuotSelectionOther As String = "OPQS"

        Public Const PurchOrderWool As String = "WPORD"
        Public Const PurchOrderYarn As String = "YPORD"

        Public Const PurchOrderOther As String = "OPORD"

        Public Const PurchOrderConfirmationWool As String = "WPOC"
        Public Const PurchOrderConfirmationYarn As String = "YPOC"
        Public Const PurchOrderConfirmationCarpet As String = "CPOC"
        Public Const PurchOrderConfirmationOther As String = "OPOC"

        Public Const PurchChallanWool As String = "WPCHN"
        Public Const PurchChallanYarn As String = "YPCHN"
        Public Const PurchChallanOther As String = "OPCHN"
        Public Const PurchChallanTradeSample As String = "TPCHN"

        Public Const PurchOrderCancelWool As String = "WPOCN"
        Public Const PurchOrderCancelYarn As String = "YPOCN"
        Public Const PurchOrderCancelCarpet As String = "CPOCN"
        Public Const PurchOrderCancelOther As String = "OPOCN"

        Public Const QCWool As String = "WQC"
        Public Const QCYarn As String = "YQC"
        Public Const QCCarpet As String = "CQC"
        Public Const QCOther As String = "OQC"

        Public Const PurchInvoiceWool As String = "WPINV"
        Public Const PurchInvoiceYarn As String = "YPINV"
        Public Const PurchInvoiceCarpet As String = "CPINV"
        Public Const PurchInvoiceOther As String = "OPINV"

        Public Const PurchReturnWool As String = "WPR"
        Public Const PurchReturnYarn As String = "YPR"
        Public Const PurchReturnCarpet As String = "CPR"
        Public Const PurchReturnOther As String = "OPR"

        Public Const GateEntry As String = "GATEE"
        Public Const DebitNote As String = "TDEBT"
        Public Const CreditNote As String = "TCRDT"
        Public Const GrBill As String = "GBill"
        Public Const ReceiptEntry As String = "RCT"
        Public Const PaymentEntry As String = "PMT"

        Public Const TraceMapOrderIssue As String = "TMOIS"
        Public Const TraceMapInvoice As String = "TMINV"
        Public Const TraceMapPayment As String = "TMPMT"
        Public Const TraceMapIssue As String = "TMISS"
        Public Const TraceMapReceive As String = "TMREC"
        Public Const TraceMapWriteOff As String = "TMWOF"
        Public Const TraceMapTds As String = "TMTDS"
        Public Const TraceMapOrderAmendment As String = "TMOAM"

        Public Const AdhesiveOrder As String = "AORD"
        Public Const AdhesiveReceive As String = "AREC"

        Public Const Washing As String = "WASH"
        Public Const WashingMaterialIssue As String = "WSISS"
        Public Const WashingReceive As String = "WSREC"
        Public Const WashingMaterialReturn As String = "WSRET"
        Public Const WashingCancel As String = "WSCNL"
        Public Const WashingInvoice As String = "WSINV"
        Public Const WashingPayment As String = "WSPMT"
        Public Const WashingTds As String = "WSTDS"


        Public Const Latexing As String = "LATEX"
        Public Const LatexingMaterialIssue As String = "LTISS"
        Public Const LatexingReceive As String = "LTREC"
        Public Const LatexingMaterialReturn As String = "LTRET"
        Public Const LatexingCancel As String = "LTCNL"
        Public Const LatexingInvoice As String = "LTINV"
        Public Const LatexingPayment As String = "LTPMT"
        Public Const LatexingTDS As String = "LTTDS"


        Public Const LatexingOutside As String = "OLTEX"
        Public Const LatexingOutsideMaterialIssue As String = "OLISS"
        Public Const LatexingOutsideReceive As String = "OLREC"
        Public Const LatexingOutsideMaterialReturn As String = "OLRET"
        Public Const LatexingOutsideCancel As String = "OLCNL"
        Public Const LatexingOutsideInvoice As String = "OLINV"
        Public Const LatexingOutsidePayment As String = "OLPMT"
        Public Const LatexingOutsideTds As String = "OLTDS"


        Public Const Streaching As String = "STRCH"
        Public Const StreachingMaterialIssue As String = "STISS"
        Public Const StreachingReceive As String = "STREC"
        Public Const StreachingMaterialReturn As String = "STRET"
        Public Const StreachingCancel As String = "STCNL"
        Public Const StreachingInvoice As String = "STINV"
        Public Const StreachingPayment As String = "STPMT"
        Public Const StreachingTDS As String = "STTDS"

        Public Const PattiMurai As String = "PTMUR"
        Public Const PattiMuraiMaterialIssue As String = "PMISS"
        Public Const PattiMuraiReceive As String = "PMREC"
        Public Const PattiMuraiMaterialReturn As String = "PMRET"
        Public Const PattiMuraiCancel As String = "PMCNL"
        Public Const PattiMuraiInvoice As String = "PMINV"
        Public Const PattiMuraiPayment As String = "PMPMT"
        Public Const PattiMuraiTDS As String = "PMTDS"

        Public Const ThirdBacking As String = "THBCK"
        Public Const ThirdBackingMaterialIssue As String = "TBISS"
        Public Const ThirdBackingReceive As String = "TBREC"
        Public Const ThirdBackingMaterialReturn As String = "TBRET"
        Public Const ThirdBackingCancel As String = "TBCNL"
        Public Const ThirdBackingInvoice As String = "TBINV"
        Public Const ThirdBackingPayment As String = "TBPMT"
        Public Const ThirdBackingTDS As String = "TBTDS"

        Public Const ClippingEmbossing As String = "CLEMB"
        Public Const ClippingEmbossingMaterialIssue As String = "CLISS"
        Public Const ClippingEmbossingReceive As String = "CEREC"
        Public Const ClippingEmbossingMaterialReturn As String = "CLRET"
        Public Const ClippingEmbossingCancel As String = "CECNL"
        Public Const ClippingEmbossingInvoice As String = "CEINV"
        Public Const ClippingEmbossingPayment As String = "CEPMT"
        Public Const ClippingEmbossingTDS As String = "CETDS"

        Public Const TapkaRepair As String = "TPREP"
        Public Const TapkaRepairMaterialIssue As String = "TPISS"
        Public Const TapkaRepairReceive As String = "TRREC"
        Public Const TapkaRepairMaterialReturn As String = "TRRET"
        Public Const TapkaRepairCancel As String = "TRCNL"
        Public Const TapkaRepairInvoice As String = "TRINV"
        Public Const TapkaRepairPayment As String = "TRPMT"
        Public Const TapkaRepairTDS As String = "TRTDS"

        Public Const FullFinishing As String = "FFNSH"
        Public Const FullFinishingMaterialIssue As String = "FFISS"
        Public Const FullFinishingReceive As String = "FFREC"
        Public Const FullFinishingMaterialReturn As String = "FFRET"
        Public Const FullFinishingCancel As String = "FFCNL"
        Public Const FullFinishingInvoice As String = "FFINV"
        Public Const FullFinishingPayment As String = "FFPMT"
        Public Const FullFinishingTDS As String = "FFTDS"

        Public Const Packing As String = "PACK"
        Public Const PackingMaterialIssue As String = "PKISS"
        Public Const PackingReceive As String = "PKREC"
        Public Const PackingMaterialReturn As String = "PKRET"
        Public Const PackingCancel As String = "PKCNL"
        Public Const PackingInvoice As String = "PKINV"
        Public Const PackingPayment As String = "PKPMT"
        Public Const PackingTDS As String = "PKTDS"

        Public Const Binding As String = "BIND"
        Public Const BindingMaterialIssue As String = "BDISS"
        Public Const BindingReceive As String = "BDREC"
        Public Const BindingMaterialReturn As String = "BDRET"
        Public Const BindingCancel As String = "BDCNL"
        Public Const BindingInvoice As String = "BDINV"
        Public Const BindingPayment As String = "BDPMT"
        Public Const BindingTDS As String = "BDTDS"

        Public Const Safai As String = "SAFAI"
        Public Const SafaiMaterialIssue As String = "SFISS"
        Public Const SafaiReceive As String = "SFREC"
        Public Const SafaiMaterialReturn As String = "SFRET"
        Public Const SafaiCancel As String = "SFCNL"
        Public Const SafaiInvoice As String = "SFINV"
        Public Const SafaiPayment As String = "SFPMT"
        Public Const SafaiTDS As String = "SFTDS"

        Public Const Gachhai As String = "GACHH"
        Public Const GachhaiReceive As String = "GCREC"
        Public Const GachhaiCancel As String = "GCCNL"
        Public Const GachhaiPayment As String = "GCPMT"
        Public Const GachhaiTDS As String = "GCTDS"

        Public Const Silai As String = "SILAI"
        Public Const SilaiMaterialIssue As String = "SLISS"
        Public Const SilaiReceive As String = "SLREC"
        Public Const SilaiMaterialReturn As String = "SLRET"
        Public Const SilaiCancel As String = "SLCNL"
        Public Const SilaiInvoice As String = "SLINV"
        Public Const SilaiPayment As String = "SLPMT"
        Public Const SilaiTDS As String = "SLTDS"

        Public Const Thokai As String = "THOKA"
        Public Const ThokaiMaterialIssue As String = "THISS"
        Public Const ThokaiReceive As String = "THREC"
        Public Const ThokaiMaterialReturn As String = "THRET"
        Public Const ThokaiCancel As String = "THCNL"
        Public Const ThokaiInvoice As String = "THINV"
        Public Const ThokaiPayment As String = "THPMT"
        Public Const ThokaiTDS As String = "THTDS"

        Public Const Katai As String = "KATAI"
        Public Const KataiMaterialIssue As String = "KTISS"
        Public Const KataiReceive As String = "KTREC"
        Public Const KataiMaterialReturn As String = "KTRET"
        Public Const KataiCancel As String = "KTCNL"
        Public Const KataiInvoice As String = "KTINV"
        Public Const KataiPayment As String = "KTPMT"
        Public Const KataiTDS As String = "KTTDS"

        Public Const OtherProcessArea As String = "OAREA"
        Public Const OtherProcessAreaMaterialIssue As String = "OAISS"
        Public Const OtherProcessAreaReceive As String = "OAREC"
        Public Const OtherProcessAreaMaterialReturn As String = "OARET"
        Public Const OtherProcessAreaCancel As String = "OACNL"
        Public Const OtherProcessAreaInvoice As String = "OAINV"
        Public Const OtherProcessAreaPayment As String = "OAPMT"
        Public Const OtherProcessAreaTDS As String = "OATDS"

        Public Const Kholai As String = "KHOlA"
        Public Const KholaiMaterialIssue As String = "KLISS"
        Public Const KholaiReceive As String = "KLREC"
        Public Const KholaiMaterialReturn As String = "KLRET"
        Public Const KholaiCancel As String = "KLCNL"
        Public Const KholaiInvoice As String = "KLINV"
        Public Const KholaiPayment As String = "KLPMT"
        Public Const KholaiTDS As String = "KLTDS"

        Public Const PattiMuraiDurry As String = "PDMUR"
        Public Const PattiMuraiDurryMaterialIssue As String = "PDISS"
        Public Const PattiMuraiDurryReceive As String = "PDREC"
        Public Const PattiMuraiDurryMaterialReturn As String = "PDRET"
        Public Const PattiMuraiDurryCancel As String = "PDCNL"
        Public Const PattiMuraiDurryInvoice As String = "PDINV"
        Public Const PattiMuraiDurryPayment As String = "PDPMT"
        Public Const PattiMuraiDurryTDS As String = "PDTDS"

        Public Const ReadyLatexOrder As String = "RLORD"
        Public Const ReadyLatexMaterialIssue As String = "RLISS"
        Public Const ReadyLatexReceive As String = "RLREC"

        Public Const OtherMaterialAdjustment As String = "OMA"
        Public Const WeavingTDS As String = "WVTDS"

        Public Const WoolTransfer As String = "WTRF"
        Public Const YarnSkuTransfer As String = "YTRF"
        Public Const OtherMaterialTransfer As String = "OTRF"
        Public Const CarpetTransfer As String = "CTRF"
        Public Const TraceMapTransfer As String = "TTRF"

        Public Const JobFollowUpStart As String = "JFUPS"
        Public Const JobFollowUpClose As String = "JFUPC"

        Public Const PurjaPenalty As String = "PPNLT"
        Public Const WeavingChequeCancel As String = "WCCNL"
        'Public Const WeavingClothSale As String = "WCSLE"
        Public Const WeavingClothIssue As String = "WCISS"
        Public Const MoneyReceiptEntry As String = "MRCTE"
        Public Const SaleChallanTradeSample As String = "SCHTS"

        Public Const TraceMapPlan As String = "TMPLN"
        Public Const PaymentRetention As String = "PRTEN"
        Public Const OtherSaleInvoice As String = "OSINV"

        Public Const SaleOrderPlan As String = "SOPLN"
        Public Const SaleOrderPlanAmendment As String = "SOPAM"

        Public Const MaterialPlanAmendment As String = "MPLAM"

        Public Const FinishingOrder As String = "FNORD"
        Public Const FinishingMaterialIssue As String = "FNISS"
        Public Const FinishingReceive As String = "FNREC"
        Public Const FinishingMaterialReturn As String = "FNRET"
        Public Const FinishingOrderCancel As String = "FOCNL"
        Public Const FinishingInvoice As String = "FNINV"
        Public Const FinishingPayment As String = "FNPMT"
        Public Const FinishingTds As String = "FNTDS"
        Public Const FinishingOrderAmendment As String = "FNORD"

        Public Const FinishingDebitNote As String = "FDEBT"
        Public Const FinishingCreditNote As String = "FCRDT"
        Public Const JobTimeIncentive As String = "JTINC"
        Public Const JobTimePenalty As String = "JTPEN"

    End Class

    Public Class SubGroupNature
        Public Const Customer As String = "Customer"
        Public Const Supplier As String = "Supplier"
        Public Const Cash As String = "Cash"
        Public Const Bank As String = "Bank"
    End Class

    Public Class ItemType
        Public Const YarnSKU As String = "Yarn SKU"
        Public Const CarpetSKU As String = "Carpet SKU"
        Public Const CarpetSample As String = "Sample"
        Public Const OtherMaterial As String = "Other"
        Public Const Yarn As String = "Yarn"
        Public Const Wool As String = "Wool"
        Public Const Trace As String = "Trace"
        Public Const Map As String = "Map"
        Public Const Plate As String = "Plate"
        Public Const FinishedMaterial As String = "FM"
    End Class


#Region "Public Help Queries"
    Public Class HelpQueries
        Public Const DeliveryMeasure As String = "Select 'Feet' as Code, 'Feet' as Description " & _
                                                 " Union All Select 'Meter' as Code, 'Meter' as Description " & _
                                                 " Union All Select 'Yard' as Code, 'Yard' as Description " & _
                                                 " Union All Select 'Cms' as Code, 'Cms' as Description "

        Public Const BillingType As String = "Select 'Qty' as Code, 'Qty' as Description " & _
                                            " Union All Select 'Measure' as Code, 'Measure' as Description "
    End Class
#End Region

#Region " Structure Update Code "
    Public Sub UpdateTableStructure(ByRef MdlTable() As AgLibrary.ClsMain.LITable)
        Try
            Call CreateDatabase(MdlTable)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub UpdateTableInitialiser()
        Call DeleteField()
        Call CreateVType()
        Call CreateView()
    End Sub

    Sub DeleteField()
        Try
            'If AgL.IsFieldExist("Design", "RUG_DesignImage", AgL.GCn) Then
            '    AgL.Dman_ExecuteNonQry("ALTER TABLE RUG_DesignImage DROP COLUMN Design", AgL.GCn)
            'End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub CreateView()
        Dim mQry$ = ""
        '' Note Write Each View in Separate <Try---Catch> Section

        Try
            'mQry = "CREATE VIEW dbo.ViewSch_SessionProgramme AS " & _
            '        " SELECT  SP.*, S.ManualCode AS SessionManualCode, S.Description AS SessionDescription, S.StartDate AS SessionStartDate, S.EndDate AS SessionEndDate, P.Description AS ProgrammeDescription, P.ManualCode AS ProgrammeManualCode, P.ProgrammeDuration, P.Semesters AS ProgrammeSemesters, P.SemesterDuration AS ProgrammeSemesterDuration, P.ProgrammeNature , PN.Description AS ProgrammeNatureDescription  , P.ManualCode  +'/' + S.ManualCode   AS SessionProgramme " & _
            '        " FROM Sch_SessionProgramme SP " & _
            '        " LEFT JOIN Sch_Session S ON sp.Session =S.Code  " & _
            '        " LEFT JOIN Sch_Programme P ON SP.Programme =P.Code " & _
            '        " LEFT JOIN Sch_ProgrammeNature PN ON P.ProgrammeNature =PN.Code "

            'AgL.IsViewExist("ViewSch_SessionProgramme", AgL.GCn, True)
            'AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

            'If AgL.PubOfflineApplicable Then
            '    AgL.IsViewExist("ViewSch_SessionProgramme", AgL.GcnSite, True)
            '    AgL.Dman_ExecuteNonQry(mQry, AgL.GcnSite)
            'End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub CreateVType()
        Try
            '===================================================< Sale Order V_Type >===================================================
            'AgL.CreateNCat(AgL.GCn, Carpet_ProjLib.ClsMain.NCat_CarpetSaleOrder, Carpet_ProjLib.ClsMain.Cat_CarpetSaleOrder, "Sale Order", AgL.PubSiteCode)
            'AgL.CreateVType(AgL.GCn, Carpet_ProjLib.ClsMain.NCat_CarpetSaleOrder, Carpet_ProjLib.ClsMain.Cat_CarpetSaleOrder, Carpet_ProjLib.ClsMain.NCat_CarpetSaleOrder, "Sale Order", Carpet_ProjLib.ClsMain.NCat_CarpetSaleOrder, AgL.PubUserName, AgL.PubLoginDate, AgL.PubStartDate, AgL.PubEndDate, AgL.PubSiteCode, AgL.PubDivCode, False, AgL.PubSitewiseV_No)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub CreateDatabase(ByRef MdlTable() As AgLibrary.ClsMain.LITable)
        FVoucher_Type(MdlTable, "Voucher_Type")

        FCustomFieldsHead(MdlTable, "CustomFieldsHead", EntryPointType.Main)

        FCustomFields(MdlTable, "CustomFields", EntryPointType.Main)

        FCustomFieldsDetail(MdlTable, "CustomFieldsDetail", EntryPointType.Main)

        FCustomFields_Trans(MdlTable, "CustomFields_Trans_Log", EntryPointType.Log)
        FCustomFields_Trans(MdlTable, "CustomFields_Trans", EntryPointType.Main)

        FAddStructureFields(MdlTable)
    End Sub

    Private Sub FVoucher_Type(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "CustomFields", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetFKeyValue(MdlTable, "CustomFields", "Code", "CustomFields")
    End Sub

    Private Sub FCustomFieldsHead(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Description", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "ManualCode", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Site_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 2)
        AgL.FSetColumnValue(MdlTable, "PreparedBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "U_EntDt", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "U_AE", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "ModifiedBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Edit_Date", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "UpLoadDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
    End Sub

    Private Sub FCustomFields(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Description", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "Type", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "HeaderTable", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Site_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 2)
        AgL.FSetColumnValue(MdlTable, "PreparedBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "U_EntDt", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "U_AE", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "ModifiedBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Edit_Date", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "UpLoadDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "TableName", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "PrimaryField", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
    End Sub

    Private Sub FCustomFieldsDetail(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int)
        AgL.FSetColumnValue(MdlTable, "Head", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "Value_Type", AgLibrary.ClsMain.SQLDataType.nVarChar, 30)
        AgL.FSetColumnValue(MdlTable, "FLength", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Value", AgLibrary.ClsMain.SQLDataType.VarCharMax)
        AgL.FSetColumnValue(MdlTable, "Default_Value", AgLibrary.ClsMain.SQLDataType.VarCharMax)
        AgL.FSetColumnValue(MdlTable, "Active", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "IsMandatory", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "TableName", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "PrimaryField", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "UpdateField", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "UpdateFieldType", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "HeaderField", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "HeaderField", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "HeaderFieldDataType", AgLibrary.ClsMain.SQLDataType.Int)
        AgL.FSetColumnValue(MdlTable, "HeaderFieldLength", AgLibrary.ClsMain.SQLDataType.Int)

        AgL.FSetFKeyValue(MdlTable, "Code", "Code", "CustomFields")
        AgL.FSetFKeyValue(MdlTable, "Heads", "Code", "CustomFildsHead")
    End Sub

    Private Sub FCustomFields_Trans(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))
        AgL.FSetColumnValue(MdlTable, "DocID", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "CustomFields", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int, , True)
        AgL.FSetColumnValue(MdlTable, "Head", AgLibrary.ClsMain.SQLDataType.nVarChar, 8)
        AgL.FSetColumnValue(MdlTable, "Value", AgLibrary.ClsMain.SQLDataType.VarCharMax)
        AgL.FSetColumnValue(MdlTable, "MnuText", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "Data", AgLibrary.ClsMain.SQLDataType.VarCharMax)
        AgL.FSetColumnValue(MdlTable, "Value_Type", AgLibrary.ClsMain.SQLDataType.nVarChar, 30)
        AgL.FSetColumnValue(MdlTable, "FLength", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "TableName", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "PrimaryField", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "HeaderField", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "UpdateField", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "UpdateFieldType", AgLibrary.ClsMain.SQLDataType.nVarChar, 30)

        AgL.FSetFKeyValue(MdlTable, "Head", "Code", "CustomFieldsHead")
    End Sub

    'Private Sub FCustomFieldDetail(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
    '    AgL.FAddTable(MdlTable, StrTableName, ModuleName)

    '    AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, True)
    '    AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int, , True)
    '    AgL.FSetColumnValue(MdlTable, "Heads", AgLibrary.ClsMain.SQLDataType.nVarChar, 8)
    '    AgL.FSetColumnValue(MdlTable, "Value_Type", AgLibrary.ClsMain.SQLDataType.nVarChar, 30)
    '    AgL.FSetColumnValue(MdlTable, "FLength", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, , , "0")
    '    AgL.FSetColumnValue(MdlTable, "Value", AgLibrary.ClsMain.SQLDataType.VarCharMax)
    '    AgL.FSetColumnValue(MdlTable, "Default_Value", AgLibrary.ClsMain.SQLDataType.VarCharMax)
    '    AgL.FSetColumnValue(MdlTable, "Active", AgLibrary.ClsMain.SQLDataType.Bit, , , , 0)
    '    AgL.FSetColumnValue(MdlTable, "RowID", AgLibrary.ClsMain.SQLDataType.IDENTITY)


    '    AgL.FSetFKeyValue(MdlTable, "Code", "Code", "CustomFields")
    '    AgL.FSetFKeyValue(MdlTable, "Heads", "Code", "CustomFildsHead")
    'End Sub

    'Private Sub FCustomFields_Trans(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
    '    AgL.FAddTable(MdlTable, StrTableName, ModuleName)

    '    AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))
    '    AgL.FSetColumnValue(MdlTable, "DocID", AgLibrary.ClsMain.SQLDataType.nVarChar, 21, IIf(EntryType = EntryPointType.Main, True, False))
    '    AgL.FSetColumnValue(MdlTable, "CustomFields", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, True)
    '    AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int, , True)
    '    AgL.FSetColumnValue(MdlTable, "TSr", AgLibrary.ClsMain.SQLDataType.Int, , True)
    '    AgL.FSetColumnValue(MdlTable, "Head", AgLibrary.ClsMain.SQLDataType.nVarChar, 8)
    '    AgL.FSetColumnValue(MdlTable, "Value", AgLibrary.ClsMain.SQLDataType.VarCharMax)
    '    AgL.FSetColumnValue(MdlTable, "RowID", AgLibrary.ClsMain.SQLDataType.IDENTITY, )


    '    AgL.FSetFKeyValue(MdlTable, "Head", "Code", "CustomFieldsHead")
    'End Sub

#End Region

    Public Sub IniDtEnviro()
        Call IniDtCommon_Enviro()
    End Sub

    Public Sub IniDtCommon_Enviro()
        If AgL.GCn IsNot Nothing Then
            AgL.PubDtEnviro = AgL.FillData("SELECT E.* FROM Enviro E  WHERE E.Site_Code ='" & AgL.PubSiteCode & "'", AgL.GCn).Tables(0)
        End If
    End Sub

    Public Shared Function FGetCustomFieldFromV_Type(ByVal V_Type As String, ByVal Conn As SQLiteConnection) As String
        Dim DtTemp As DataTable = Nothing
        Dim Agl As New AgLibrary.ClsMain
        Dim mCustomField$ = ""
        Try
            DtTemp = Agl.FillData("Select * From Voucher_Type Where V_Type = '" & V_Type & "'", Conn).Tables(0)
            If DtTemp.Rows.Count > 0 Then
                mCustomField = Agl.XNull(DtTemp.Rows(0)("CustomFields"))
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " In FGetCustomFieldFromV_Type Function")
        Finally
            FGetCustomFieldFromV_Type = mCustomField
            If DtTemp IsNot Nothing Then DtTemp.Dispose()
        End Try
    End Function

    Private Sub FAddStructureFields(ByRef MdlTable() As AgLibrary.ClsMain.LITable)
        Try
            Dim DtVType As DataTable
            Dim DtCustomFields As DataTable
            Dim mQry$
            Dim I As Integer
            Dim J As Integer
            mQry = "Select CustomFields, HeaderTable, Ht.Name as HeaderTableName, LHT.Name as LogHeaderTableName  " &
                   " From Voucher_Type " &
                   " Left Join Sys.Objects HT On Voucher_Type.HeaderTable = HT.Object_ID " &
                   " Left Join Sys.Objects LHT On Voucher_Type.LogHeaderTable = LHT.Object_ID " &
                   " Where CustomFields Is Not Null "
            DtVType = AgL.FillData(mQry, AgL.GCn).Tables(0)


            For I = 0 To DtVType.Rows.Count - 1
                mQry = "Select HeaderField, HeaderFieldDataType, HeaderFieldLength From CustomFieldsDetail  Where Code = '" & DtVType.Rows(I)("CustomFields") & "' And Head Is Not Null  "
                DtCustomFields = AgL.FillData(mQry, AgL.GCn).Tables(0)

                If DtCustomFields.Rows.Count > 0 Then
                    '===========ADD FIELDS IN HEADER TABLE========================
                    If AgL.XNull(DtVType.Rows(I)("HeaderTableName")) <> "" Then
                        AgL.FAddTable(MdlTable, DtVType.Rows(I)("HeaderTableName"), ModuleName)
                        For J = 0 To DtCustomFields.Rows.Count - 1
                            If AgL.XNull(DtCustomFields.Rows(J)("HeaderField")) <> "" Then
                                AgL.FSetColumnValue(MdlTable, AgL.XNull(DtCustomFields.Rows(J)("HeaderField")), AgL.VNull(DtCustomFields.Rows(J)("HeaderFieldDataType")), AgL.VNull(DtCustomFields.Rows(J)("HeaderFieldLength")))
                            End If
                        Next
                    End If
                    '===========================================================

                    '===========ADD FIELDS IN LOG HEADER TABLE========================
                    If AgL.XNull(DtVType.Rows(I)("LogHeaderTableName")) <> "" Then
                        AgL.FAddTable(MdlTable, DtVType.Rows(I)("LogHeaderTableName"), ModuleName)
                        For J = 0 To DtCustomFields.Rows.Count - 1
                            If AgL.XNull(DtCustomFields.Rows(J)("HeaderField")) <> "" Then
                                AgL.FSetColumnValue(MdlTable, AgL.XNull(DtCustomFields.Rows(J)("HeaderField")), AgL.VNull(DtCustomFields.Rows(J)("HeaderFieldDataType")), AgL.VNull(DtCustomFields.Rows(J)("HeaderFieldLength")))
                            End If
                        Next
                    End If
                    '=================================================================
                End If
            Next
        Catch ex As Exception
        End Try
    End Sub

    Public Shared Sub FPrintThisDocument(ByVal objFrm As Object, ByVal V_Type As String,
            Optional ByVal Report_QueryList As String = "", Optional ByVal Report_NameList As String = "",
            Optional ByVal Report_TitleList As String = "", Optional ByVal Report_FormatList As String = "",
            Optional ByVal SubReport_QueryList As String = "",
            Optional ByVal SubReport_NameList As String = "")

        Dim DtVTypeSetting As DataTable = Nothing
        Dim mQry As String = ""
        Dim mCrd As New ReportDocument
        Dim ReportView As New AgLibrary.RepView
        Dim DsRep As New DataSet
        Dim strQry As String = ""

        Dim RepName As String = ""
        Dim RepTitle As String = ""
        Dim RepQry As String = ""

        Dim RetIndex As Integer = 0

        Dim Report_QryArr() As String = Nothing
        Dim Report_NameArr() As String = Nothing
        Dim Report_TitleArr() As String = Nothing
        Dim Report_FormatArr() As String = Nothing

        Dim SubReport_QryArr() As String = Nothing
        Dim SubReport_NameArr() As String = Nothing
        Dim SubReport_DataSetArr() As DataSet = Nothing

        Dim I As Integer = 0

        Try
            mQry = "Select * from Voucher_Type_Settings  " &
                       "Where V_Type = '" & V_Type & "' " &
                       "And Site_Code = '" & AgL.PubSiteCode & "' " &
                       "And Div_Code  = '" & AgL.PubDivCode & "' "
            DtVTypeSetting = AgL.FillData(mQry, AgL.GcnRead).Tables(0)
            If DtVTypeSetting.Rows.Count <> 0 Then
                If AgL.XNull(DtVTypeSetting.Rows(0)("Query")) <> "" Then
                    Report_QueryList = AgL.XNull(DtVTypeSetting.Rows(0)("Query"))
                    Report_QueryList = Replace(Report_QueryList.ToString.ToUpper, "`", "'")
                    Report_QueryList = Replace(Report_QueryList.ToString.ToUpper, "<SEARCHCODE>", objFrm.mSearchCode)
                End If

                If AgL.XNull(DtVTypeSetting.Rows(0)("Report_Name")) <> "" Then
                    Report_NameList = AgL.XNull(DtVTypeSetting.Rows(0)("Report_Name"))
                End If

                If AgL.XNull(DtVTypeSetting.Rows(0)("Report_Heading")) <> "" Then
                    Report_TitleList = AgL.XNull(DtVTypeSetting.Rows(0)("Report_Heading"))
                End If

                If AgL.XNull(DtVTypeSetting.Rows(0)("Report_Format")) <> "" Then
                    Report_FormatList = AgL.XNull(DtVTypeSetting.Rows(0)("Report_Format"))
                End If

                If AgL.XNull(DtVTypeSetting.Rows(0)("SubReport_QueryList")) <> "" Then
                    SubReport_QueryList = AgL.XNull(DtVTypeSetting.Rows(0)("SubReport_QueryList"))
                    SubReport_QueryList = Replace(SubReport_QueryList.ToString.ToUpper, "`", "'")
                    SubReport_QueryList = Replace(SubReport_QueryList.ToString.ToUpper, "<SEARCHCODE>", objFrm.mSearchCode)
                End If

                If AgL.XNull(DtVTypeSetting.Rows(0)("SubReport_NameList")) <> "" Then
                    SubReport_NameList = AgL.XNull(DtVTypeSetting.Rows(0)("SubReport_NameList"))
                End If
            End If

            If Report_QueryList <> "" Then Report_QryArr = Split(Report_QueryList, "|")
            If Report_TitleList <> "" Then Report_TitleArr = Split(Report_TitleList, "|")
            If Report_NameList <> "" Then Report_NameArr = Split(Report_NameList, "|")

            If Report_FormatList <> "" Then
                Report_FormatArr = Split(Report_FormatList, "|")

                For I = 0 To Report_FormatArr.Length - 1
                    If strQry <> "" Then strQry += " UNION ALL "
                    strQry += " Select " & I & " As Code, '" & Report_FormatArr(I) & "' As Name "
                Next

                Dim FRH_Single As DMHelpGrid.FrmHelpGrid
                FRH_Single = New DMHelpGrid.FrmHelpGrid(New DataView(AgL.FillData(strQry, AgL.GCn).TABLES(0)), "", 300, 350, , , False)
                FRH_Single.FFormatColumn(0, , 0, , False)
                FRH_Single.FFormatColumn(1, "Report Format", 250, DataGridViewContentAlignment.MiddleLeft)
                FRH_Single.StartPosition = FormStartPosition.CenterScreen
                FRH_Single.ShowDialog()

                If FRH_Single.BytBtnValue = 0 Then
                    RetIndex = FRH_Single.DRReturn("Code")
                End If

                If Report_NameArr.Length = Report_FormatArr.Length Then RepName = Report_NameArr(RetIndex) Else RepName = Report_NameArr(0)
                If Report_TitleArr.Length = Report_FormatArr.Length Then RepTitle = Report_TitleArr(RetIndex) Else RepTitle = Report_TitleArr(0)
                If Report_QryArr.Length = Report_FormatArr.Length Then RepQry = Report_QryArr(RetIndex) Else RepQry = Report_QryArr(0)
            Else
                RepName = Report_NameArr(0)
                RepTitle = Report_TitleArr(0)
                RepQry = Report_QryArr(0)
            End If

            AgL.ADMain = New SQLiteDataAdapter(RepQry, AgL.GCn)
            AgL.ADMain.Fill(DsRep)
            AgPL.CreateFieldDefFile1(DsRep, AgL.PubReportPath & "\" & RepName & ".ttx", True)

            If SubReport_QueryList <> "" Then SubReport_QryArr = Split(SubReport_QueryList, "|")
            If SubReport_NameList <> "" Then SubReport_NameArr = Split(SubReport_NameList, "|")

            If SubReport_QryArr IsNot Nothing And SubReport_NameArr IsNot Nothing Then
                If SubReport_QryArr.Length <> SubReport_NameArr.Length Then
                    MsgBox("Number Of SubReport Qries And SubReport Names Are Not Equal.", MsgBoxStyle.Information)
                    Exit Sub
                End If

                For I = 0 To SubReport_QryArr.Length - 1
                    AgL.ADMain = New SQLiteDataAdapter(SubReport_QryArr(I).ToString, AgL.GCn)
                    ReDim Preserve SubReport_DataSetArr(I)
                    SubReport_DataSetArr(I) = New DataSet
                    AgL.ADMain.Fill(SubReport_DataSetArr(I))
                    AgPL.CreateFieldDefFile1(SubReport_DataSetArr(I), AgL.PubReportPath & "\" & RepName & (I + 1).ToString & ".ttx", True)
                Next
            End If

            mCrd.Load(AgL.PubReportPath & "\" & RepName & ".rpt")
            mCrd.SetDataSource(DsRep.Tables(0))

            If SubReport_QryArr IsNot Nothing And SubReport_NameArr IsNot Nothing Then
                For I = 0 To SubReport_NameArr.Length - 1
                    Try
                        mCrd.OpenSubreport(SubReport_NameArr(I).ToString).Database.Tables(0).SetDataSource(SubReport_DataSetArr(I).Tables(0))
                    Catch ex As Exception
                    End Try
                Next
            End If

            CType(ReportView.Controls("CrvReport"), CrystalDecisions.Windows.Forms.CrystalReportViewer).ReportSource = mCrd
            AgPL.Formula_Set(mCrd, RepTitle)
            AgPL.Show_Report(ReportView, "* " & RepTitle & " *", objFrm.MdiParent)

            Call AgL.LogTableEntry(objFrm.mSearchCode, objFrm.Text, "P", AgL.PubMachineName, AgL.PubUserName, AgL.PubLoginDate, AgL.GCn, AgL.ECmd)
        Catch Ex As Exception
            MsgBox(Ex.Message)
        End Try
    End Sub

    Public Shared Function FCheckDuplicatePartyDocNo(ByVal FieldName As String, ByVal TableName As String, ByVal V_Type As String,
                                      ByVal PartyDocNo As String, ByVal SearchCode As String, ByVal FieldParty As String, ByVal Party As String) As Boolean
        Dim mQry$ = ""
        mQry = " Select Count(*) From " & TableName & " " &
                " Where " & FieldName & " = '" & PartyDocNo & "' " &
                " AND " & FieldParty & " = '" & Party & "' " &
                " And V_Type = '" & V_Type & "' " &
                " And DocId <> '" & SearchCode & "'"
        If AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar) > 0 Then
            mQry = "SELECT V_Date FROM  " & TableName & " " &
                    " Where " & FieldName & " = '" & PartyDocNo & "' " &
                    " AND " & FieldParty & " = '" & Party & "' " &
                    " And V_Type = '" & V_Type & "' " &
                    " And DocId <> '" & SearchCode & "'"
            If MsgBox("Supplier Document No. " & PartyDocNo & " Is Already Feeded in Date " & AgL.XNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar) & ". Do You Want To Continue ?", MsgBoxStyle.Information + MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                FCheckDuplicatePartyDocNo = False
            Else
                FCheckDuplicatePartyDocNo = True
            End If
            'MsgBox("Supplier Doc No Is Duplicate.", MsgBoxStyle.Information)
        Else
            FCheckDuplicatePartyDocNo = True
        End If
    End Function

    Public Shared Sub PostStructureLineToAccounts(ByVal FGMain As AgStructure.AgCalcGrid, ByVal mNarr As String, ByVal mDocID As String, ByVal mDiv_Code As String,
                                              ByVal mSite_Code As String, ByVal Div_Code As String, ByVal mV_Type As String, ByVal mV_Prefix As String, ByVal mV_No As Integer,
                                              ByVal mRecID As String, ByVal PostingPartyAc As String, ByVal mV_Date As String,
                                              ByVal Conn As SQLiteConnection, ByVal Cmd As SQLiteCommand)
        Dim StrContraTextJV As String = ""
        Dim mPostSubCode = ""
        Dim I As Integer, J As Integer
        Dim mQry$ = "", bSelectionQry$ = ""
        Dim DtTemp As DataTable = Nothing

        bSelectionQry = ""
        For I = 0 To FGMain.Rows.Count - 1
            For J = 0 To FGMain.AgLineGrid.Rows.Count - 1
                If FGMain.AgChargesValue(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag, J, AgStructure.AgCalcGrid.LineColumnType.PostAc) <> "" Then
                    If bSelectionQry <> "" Then bSelectionQry += " UNION ALL "

                    bSelectionQry += " Select '" & FGMain.AgChargesValue(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag, J, AgStructure.AgCalcGrid.LineColumnType.PostAc) & "' As PostAc, " &
                    " Case When " & AgL.Chk_Text(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_DrCr, I).Value) & " = 'Dr' Then " & Val(FGMain.AgChargesValue(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag, J, AgStructure.AgCalcGrid.LineColumnType.Amount)) & "  " &
                    "      When " & AgL.Chk_Text(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_DrCr, I).Value) & " = 'Cr' Then " & -Val(FGMain.AgChargesValue(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag, J, AgStructure.AgCalcGrid.LineColumnType.Amount)) & " End As Amount "
                ElseIf Trim(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_PostAc, I).Value) <> "" Then
                    If bSelectionQry <> "" Then bSelectionQry += " UNION ALL "

                    bSelectionQry += " Select '" & FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_PostAc, I).Value & "' As PostAc, " &
                    " Case When " & AgL.Chk_Text(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_DrCr, I).Value) & " = 'Dr' Then " & Val(FGMain.AgChargesValue(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag, J, AgStructure.AgCalcGrid.LineColumnType.Amount)) & "  " &
                    "      When " & AgL.Chk_Text(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_DrCr, I).Value) & " = 'Cr' Then " & -Val(FGMain.AgChargesValue(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag, J, AgStructure.AgCalcGrid.LineColumnType.Amount)) & " End As Amount "

                End If
            Next
        Next

        If bSelectionQry = "" Then Exit Sub


        mQry = " Select Count(*)  " &
                " From (" & bSelectionQry & ") As V1 " &
                " Having Sum(Case When IFNull(V1.Amount,0) > 0 Then IFNull(V1.Amount,0) Else 0 End) <> abs(Sum(Case When IFNull(V1.Amount,0) < 0 Then IFNull(V1.Amount,0) Else 0 End))  "
        DtTemp = AgL.FillData(mQry, AgL.GcnRead).Tables(0)
        If DtTemp.Rows.Count > 0 Then
            If AgL.VNull(DtTemp.Rows(0)(0)) > 0 Then
                Err.Raise(1, , "Error In Ledger Posting. Debit and Credit balances are not equal.")
            End If
        End If



        mQry = " Select V1.PostAc, IFNull(Sum(V1.Amount),0) As Amount, " &
                " Case When IFNull(Sum(V1.Amount),0) > 0 Then 'Dr' " &
                "      When IFNull(Sum(V1.Amount),0) < 0 Then 'Cr' End As DrCr " &
                " From (" & bSelectionQry & ") As V1 " &
                " Group BY V1.PostAc "
        DtTemp = AgL.FillData(mQry, AgL.GcnRead).Tables(0)

        With DtTemp
            For I = 0 To .Rows.Count - 1
                If Trim(AgL.XNull(.Rows(I)("PostAc"))) <> "" Then
                    If AgL.StrCmp(AgL.XNull(.Rows(I)("PostAc")), "|PARTY|") Then
                        If AgL.VNull(.Rows(I)("Amount")) <> 0 And AgL.XNull(.Rows(I)("DrCr")) <> "" Then
                            If StrContraTextJV <> "" Then StrContraTextJV += vbCrLf
                            FPrepareContraText(False, StrContraTextJV, PostingPartyAc, Math.Abs(AgL.VNull(.Rows(I)("Amount"))), AgL.XNull(.Rows(I)("DrCr")))
                        End If
                    Else
                        If AgL.VNull(.Rows(I)("Amount")) <> 0 And AgL.XNull(.Rows(I)("DrCr")) <> "" Then
                            If StrContraTextJV <> "" Then StrContraTextJV += vbCrLf
                            FPrepareContraText(False, StrContraTextJV, AgL.XNull(.Rows(I)("PostAc")), Math.Abs(Val(AgL.VNull(.Rows(I)("Amount")))), AgL.XNull(.Rows(I)("DrCr")))
                        End If
                    End If
                End If
            Next
        End With

        Dim mSrl As Integer = 0, mDebit As Double, mCredit As Double
        mQry = "Delete from Ledger where docId='" & mDocID & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        With DtTemp
            For I = 0 To .Rows.Count - 1
                If Trim(AgL.XNull(.Rows(I)("PostAc"))) <> "" And Val(AgL.VNull(.Rows(I)("Amount"))) <> 0 Then
                    mSrl += 1

                    mDebit = 0 : mCredit = 0
                    If AgL.StrCmp(AgL.XNull(.Rows(I)("PostAc")), "|PARTY|") Then
                        mPostSubCode = PostingPartyAc
                    Else
                        mPostSubCode = AgL.XNull(.Rows(I)("PostAc"))
                    End If

                    If AgL.StrCmp(AgL.XNull(.Rows(I)("DrCr")), "Dr") Then
                        mDebit = Math.Abs(AgL.VNull(.Rows(I)("Amount")))
                    ElseIf AgL.StrCmp(AgL.XNull(.Rows(I)("DrCr")), "Cr") Then
                        mCredit = Math.Abs(AgL.VNull(.Rows(I)("Amount")))
                    End If

                    mQry = "Insert Into Ledger(DocId,RecId,V_SNo,V_Date,SubCode,ContraSub,AmtDr,AmtCr," &
                         " Narration,V_Type,V_No,V_Prefix,Site_Code,DivCode,Chq_No,Chq_Date,TDSCategory,TDSOnAmt,TDSDesc," &
                         " TDSPer,TDS_Of_V_SNo,System_Generated,FormulaString,ContraText) Values " &
                         " ('" & mDocID & "','" & mRecID & "'," & mSrl & "," & AgL.ConvertDate(mV_Date) & "," & AgL.Chk_Text(mPostSubCode) & "," & AgL.Chk_Text("") & ", " &
                         " " & mDebit & "," & mCredit & ", " &
                         " " & AgL.Chk_Text(mNarr) & ",'" & mV_Type & "','" & mV_No & "','" & mV_Prefix & "'," &
                         " '" & mSite_Code & "','" & mDiv_Code & "','" & AgL.Chk_Text("") & "'," &
                         " " & AgL.ConvertDate("") & "," & AgL.Chk_Text("") & "," &
                         " " & Val("") & "," & AgL.Chk_Text("") & "," & Val("") & "," & 0 & ",'Y','" & "" & "','" & StrContraTextJV & "')"
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                End If
            Next I
        End With
    End Sub

    Public Shared Sub FPrepareContraText(ByVal BlnOverWrite As Boolean, ByRef StrContraTextVar As String,
                                         ByVal StrContraName As String, ByVal DblAmount As Double, ByVal StrDrCr As String)
        Dim IntNameMaxLen As Integer = 35, IntAmtMaxLen As Integer = 18, IntSpaceNeeded As Integer = 2
        StrContraName = AgL.XNull(AgL.Dman_Execute("Select Name from Subgroup  Where SubCode = '" & StrContraName & "'  ", AgL.GcnRead).ExecuteScalar)

        If BlnOverWrite Then
            StrContraTextVar = Mid(Trim(StrContraName), 1, IntNameMaxLen) & Space((IntNameMaxLen + IntSpaceNeeded) - Len(Mid(Trim(StrContraName), 1, IntNameMaxLen))) & Space(IntAmtMaxLen - Len(Format(Val(DblAmount), "##,##,##,##,##0.00"))) & Format(Val(DblAmount), "##,##,##,##,##0.00") & " " & Trim(StrDrCr)
        Else
            StrContraTextVar += Mid(Trim(StrContraName), 1, IntNameMaxLen) & Space((IntNameMaxLen + IntSpaceNeeded) - Len(Mid(Trim(StrContraName), 1, IntNameMaxLen))) & Space(IntAmtMaxLen - Len(Format(Val(DblAmount), "##,##,##,##,##0.00"))) & Format(Val(DblAmount), "##,##,##,##,##0.00") & " " & Trim(StrDrCr)
        End If
    End Sub

    Public Shared Function FGetItemRate(ByVal ItemCode As String, ByVal RateType As String, ByVal V_Date As String)
        Dim mQry$ = ""
        Try
            mQry = " SELECT L.Rate FROM RateListDetail L WHERE L.Item = '" & ItemCode & "'  AND IFNull(L.RateType,'') = '" & RateType & "' And WEF <= '" & V_Date & "'  ORDER BY L.WEF DESC Limit 1"
            FGetItemRate = AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar)
        Catch ex As Exception
            FGetItemRate = 0
            MsgBox(ex.Message & " In FGetItemRate")
        End Try
    End Function

    Public Shared Sub ProcOpenLinkForm(ByVal Mnu As System.Windows.Forms.ToolStripItem, ByVal SearchCode As String, ByVal Parent As Form)
        Dim FrmObj As AgTemplate.TempTransaction
        Dim CFOpen As New ClsFunction
        Try
            FrmObj = CFOpen.FOpen(Mnu.Name, Mnu.Text, True)
            If FrmObj IsNot Nothing Then
                FrmObj.MdiParent = Parent
                FrmObj.Show()
                FrmObj.FindMove(SearchCode)
                FrmObj = Nothing
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    'Public Shared Sub PostStructureToAccounts(ByVal FGMain As AgStructure.AgCalcGrid, ByVal mNarr As String, ByVal mDocID As String, ByVal mDiv_Code As String, _
    '                                          ByVal mSite_Code As String, ByVal Div_Code As String, ByVal mV_Type As String, ByVal mV_Prefix As String, ByVal mV_No As Integer, _
    '                                          ByVal mRecID As String, ByVal PostingPartyAc As String, ByVal mV_Date As String, _
    '                                          ByVal Conn As SqliteConnection, ByVal Cmd As SqliteCommand)
    '    Dim StrContraTextJV As String = ""
    '    Dim mPostSubCode = ""
    '    Dim I As Integer
    '    Dim mQry$ = "", bSelectionQry$ = ""
    '    Dim DtTemp As DataTable = Nothing


    '    For I = 0 To FGMain.Rows.Count - 1
    '        If Trim(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_PostAc, I).Value) <> "" Then
    '            If bSelectionQry = "" Then
    '                bSelectionQry = " Select '" & FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_PostAc, I).Value & "' As PostAc, " & _
    '                " Case When " & AgL.Chk_Text(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_DrCr, I).Value) & " = 'Dr' Then " & Val(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Amount, I).Value) & "  " & _
    '                "      When " & AgL.Chk_Text(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_DrCr, I).Value) & " = 'Cr' Then " & -Val(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Amount, I).Value) & " End As Amount "
    '            Else
    '                bSelectionQry += " UNION ALL "
    '                bSelectionQry += " Select '" & FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_PostAc, I).Value & "' As PostAc, " & _
    '                " Case When " & AgL.Chk_Text(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_DrCr, I).Value) & " = 'Dr' Then " & Val(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Amount, I).Value) & "  " & _
    '                "      When " & AgL.Chk_Text(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_DrCr, I).Value) & " = 'Cr' Then " & -Val(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Amount, I).Value) & " End As Amount "

    '            End If
    '        End If
    '    Next

    '    If bSelectionQry = "" Then Exit Sub



    '    mQry = " Select V1.PostAc, IFNull(Sum(V1.Amount),0) As Amount, " & _
    '            " Case When IFNull(Sum(V1.Amount),0) > 0 Then 'Dr' " & _
    '            "      When IFNull(Sum(V1.Amount),0) < 0 Then 'Cr' End As DrCr " & _
    '            " From (" & bSelectionQry & ") As V1 " & _
    '            " Group BY V1.PostAc "
    '    DtTemp = AgL.FillData(mQry, AgL.GcnRead).Tables(0)

    '    With DtTemp
    '        For I = 0 To .Rows.Count - 1
    '            If Trim(AgL.XNull(.Rows(I)("PostAc"))) <> "" Then
    '                If AgL.StrCmp(AgL.XNull(.Rows(I)("PostAc")), "|PARTY|") Then
    '                    If AgL.VNull(.Rows(I)("Amount")) <> 0 And AgL.XNull(.Rows(I)("DrCr")) <> "" Then
    '                        If StrContraTextJV <> "" Then StrContraTextJV += vbCrLf
    '                        FPrepareContraText(False, StrContraTextJV, PostingPartyAc, Math.Abs(AgL.VNull(.Rows(I)("Amount"))), AgL.XNull(.Rows(I)("DrCr")))
    '                    End If
    '                Else
    '                    If AgL.VNull(.Rows(I)("Amount")) <> 0 And AgL.XNull(.Rows(I)("DrCr")) <> "" Then
    '                        If StrContraTextJV <> "" Then StrContraTextJV += vbCrLf
    '                        FPrepareContraText(False, StrContraTextJV, AgL.XNull(.Rows(I)("PostAc")), Math.Abs(Val(AgL.VNull(.Rows(I)("Amount")))), AgL.XNull(.Rows(I)("DrCr")))
    '                    End If
    '                End If
    '            End If
    '        Next
    '    End With

    '    Dim mSrl As Integer = 0, mDebit As Double, mCredit As Double
    '    mQry = "Delete from Ledger where docId='" & mDocID & "'"
    '    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

    '    With DtTemp
    '        For I = 0 To .Rows.Count - 1
    '            If Trim(AgL.XNull(.Rows(I)("PostAc"))) <> "" And Val(AgL.VNull(.Rows(I)("Amount"))) <> 0 Then
    '                mSrl += 1

    '                mDebit = 0 : mCredit = 0
    '                If AgL.StrCmp(AgL.XNull(.Rows(I)("PostAc")), "|PARTY|") Then
    '                    mPostSubCode = PostingPartyAc
    '                Else
    '                    mPostSubCode = AgL.XNull(.Rows(I)("PostAc"))
    '                End If

    '                If AgL.StrCmp(AgL.XNull(.Rows(I)("DrCr")), "Dr") Then
    '                    mDebit = Math.Abs(AgL.VNull(.Rows(I)("Amount")))
    '                ElseIf AgL.StrCmp(AgL.XNull(.Rows(I)("DrCr")), "Cr") Then
    '                    mCredit = Math.Abs(AgL.VNull(.Rows(I)("Amount")))
    '                End If

    '                mQry = "Insert Into Ledger(DocId,RecId,V_SNo,V_Date,SubCode,ContraSub,AmtDr,AmtCr," & _
    '                     " Narration,V_Type,V_No,V_Prefix,Site_Code,DivCode,Chq_No,Chq_Date,TDSCategory,TDSOnAmt,TDSDesc," & _
    '                     " TDSPer,TDS_Of_V_SNo,System_Generated,FormulaString,ContraText) Values " & _
    '                     " ('" & mDocID & "','" & mRecID & "'," & mSrl & "," & AgL.ConvertDate(mV_Date) & "," & AgL.Chk_Text(mPostSubCode) & "," & AgL.Chk_Text("") & ", " & _
    '                     " " & mDebit & "," & mCredit & ", " & _
    '                     " " & AgL.Chk_Text(mNarr) & ",'" & mV_Type & "','" & mV_No & "','" & mV_Prefix & "'," & _
    '                     " '" & mSite_Code & "','" & mDiv_Code & "','" & AgL.Chk_Text("") & "'," & _
    '                     " " & AgL.ConvertDate("") & "," & AgL.Chk_Text("") & "," & _
    '                     " " & Val("") & "," & AgL.Chk_Text("") & "," & Val("") & "," & 0 & ",'Y','" & "" & "','" & StrContraTextJV & "')"
    '                AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    '            End If
    '        Next I
    '    End With
    'End Sub

    'Function To Lock Old Entry In New Entry Point
    Public Shared Function FLockOldEntryInNewEntryPoint(ByVal JobProcess As String, ByVal V_Date As String) As Boolean
        Try
            Select Case JobProcess
                Case ClsMain.Temp_NCat.ThirdBacking
                    If CDate(V_Date) < CDate("28/Aug/2013") Then
                        MsgBox("Can't Edit Entry.You Can Edit This Entry In Old Entry Point.", MsgBoxStyle.Information)
                        FLockOldEntryInNewEntryPoint = True
                    End If

                Case ClsMain.Temp_NCat.PattiMurai
                    If CDate(V_Date) < CDate("30/Sep/2013") Then
                        MsgBox("Can't Edit Entry.You Can Edit This Entry In Old Entry Point.", MsgBoxStyle.Information)
                        FLockOldEntryInNewEntryPoint = True
                    End If

                Case ClsMain.Temp_NCat.Latexing, ClsMain.Temp_NCat.LatexingOutside
                    If CDate(V_Date) < CDate("15/Oct/2013") Then
                        MsgBox("Can't Edit Entry.You Can Edit This Entry In Old Entry Point.", MsgBoxStyle.Information)
                        FLockOldEntryInNewEntryPoint = True
                    End If

                Case ClsMain.Temp_NCat.Washing, ClsMain.Temp_NCat.ClippingEmbossing
                    If CDate(V_Date) < CDate("23/Jan/2014") Then
                        MsgBox("Can't Edit Entry.You Can Edit This Entry In Old Entry Point.", MsgBoxStyle.Information)
                        FLockOldEntryInNewEntryPoint = True
                    End If

                Case ClsMain.Temp_NCat.Streaching, ClsMain.Temp_NCat.TapkaRepair,
                        ClsMain.Temp_NCat.Safai, ClsMain.Temp_NCat.PattiMuraiDurry,
                        ClsMain.Temp_NCat.Thokai, ClsMain.Temp_NCat.OtherProcessArea
                    If CDate(V_Date) < CDate("20/Feb/2014") Then
                        MsgBox("Can't Edit Entry.You Can Edit This Entry In Old Entry Point.", MsgBoxStyle.Information)
                        FLockOldEntryInNewEntryPoint = True
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
            FLockOldEntryInNewEntryPoint = True
        End Try
    End Function

    Public Shared Function FGetJobRate(ByVal mJobRateHelpDataSet As DataSet, ByVal FlatRate As Double,
                                ByVal JobProcess As String, ByVal InsideOutside As String, ByVal V_Date As String,
                                ByVal ItemCode As String, ByVal ItemGroup As String, ByVal ItemCategory As String,
                                ByVal MeasurePerPcs As Double)
        Dim DrTemp As DataRow() = Nothing

        If FlatRate > 0 Then
            FGetJobRate = FlatRate
        Else
            DrTemp = mJobRateHelpDataSet.Tables(0).Select("Process ='" & JobProcess & "' And ( IFNull(AreaGE,0)<" & MeasurePerPcs & " OR IFNull(" & MeasurePerPcs & ",0) =0) And  IFNull(Item,'') = '" & ItemCode & "' And WEF <= '" & V_Date & "' ", "WEF Desc, AreaGe Desc")
            If DrTemp.Length <= 0 Then
                'Find record of selected item and selected Jobworker of selected process and selected jobprocessgroup 
                DrTemp = mJobRateHelpDataSet.Tables(0).Select("Process ='" & JobProcess & "' And IFNull(AreaGE,0)<" & MeasurePerPcs & " And  IFNull(ItemGroup,'') = '" & ItemGroup & "' And WEF <= '" & V_Date & "' ", "WEF Desc, AreaGe Desc")
                If DrTemp.Length <= 0 Then
                    'Find record of selected item and selected Jobworker of selected process 
                    DrTemp = mJobRateHelpDataSet.Tables(0).Select("Process ='" & JobProcess & "' And IFNull(AreaGE,0)<" & MeasurePerPcs & " And  IFNull(ItemCategory,'') = '" & ItemCategory & "' And WEF <= '" & V_Date & "' ", "WEF Desc, AreaGe Desc")
                    If DrTemp.Length <= 0 Then
                        'Find record of selected item Category of selected process
                        DrTemp = mJobRateHelpDataSet.Tables(0).Select("Process ='" & JobProcess & "' And IFNull(AreaGE,0)<" & MeasurePerPcs & " And  IFNull(Item,'') = ''  And  IFNull(ItemGroup,'') = '' And  IFNull(ItemCategory,'') = '' And WEF <= '" & V_Date & "' ", "WEF Desc, AreaGe Desc")
                    End If
                End If
            End If

            If DrTemp.Length > 0 Then
                FGetJobRate = AgL.VNull(DrTemp(0)("Rate"))
            Else
                FGetJobRate = 0
            End If
        End If
    End Function

    Public Shared Function FGetJobIncentiveRate(ByVal mJobRateHelpDataSet As DataSet,
                                ByVal JobProcess As String, ByVal InsideOutside As String, ByVal V_Date As String,
                                ByVal ItemCode As String, ByVal ItemGroup As String, ByVal ItemCategory As String,
                                ByVal MeasurePerPcs As Double)
        Dim DrTemp As DataRow() = Nothing
        FGetJobIncentiveRate = 0
        DrTemp = mJobRateHelpDataSet.Tables(0).Select("Process ='" & JobProcess & "' And IFNull(AreaGE,0)<" & MeasurePerPcs & " And  IFNull(Item,'') = '" & ItemCode & "' And WEF <= '" & V_Date & "' ", "WEF Desc, AreaGe Desc")
        If DrTemp.Length <= 0 Then
            'Find record of selected item and selected Jobworker of selected process and selected jobprocessgroup 
            DrTemp = mJobRateHelpDataSet.Tables(0).Select("Process ='" & JobProcess & "' And IFNull(AreaGE,0)<" & MeasurePerPcs & " And  IFNull(ItemGroup,'') = '" & ItemGroup & "' And WEF <= '" & V_Date & "' ", "WEF Desc, AreaGe Desc")
            If DrTemp.Length <= 0 Then
                'Find record of selected item and selected Jobworker of selected process 
                DrTemp = mJobRateHelpDataSet.Tables(0).Select("Process ='" & JobProcess & "' And IFNull(AreaGE,0)<" & MeasurePerPcs & " And  IFNull(ItemCategory,'') = '" & ItemCategory & "' And WEF <= '" & V_Date & "' ", "WEF Desc, AreaGe Desc")
                If DrTemp.Length <= 0 Then
                    'Find record of selected item Category of selected process
                    DrTemp = mJobRateHelpDataSet.Tables(0).Select("Process ='" & JobProcess & "' And IFNull(AreaGE,0)<" & MeasurePerPcs & " And  IFNull(Item,'') = ''  And  IFNull(ItemGroup,'') = '' And  IFNull(ItemCategory,'') = '' And WEF <= '" & V_Date & "' ", "WEF Desc, AreaGe Desc")
                End If
            End If

            If DrTemp.Length > 0 Then
                FGetJobIncentiveRate = AgL.VNull(DrTemp(0)("IncentiveRate"))
            Else
                FGetJobIncentiveRate = 0
            End If
        End If
    End Function

    Public Shared Sub FSetDefaultGodown(ByRef GodownCode As String, ByRef GodownName As String)
        Dim mQry As String
        Dim DtTemp As DataTable = Nothing
        mQry = "Select Default_Godown As GodownCode, G.Description As GodownName From Computer C " & _
                " LEFT JOIN Godown G ON C.Default_Godown = G.Code " & _
                " WHere C.Description = '" & My.Computer.Name & "'"
        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
        If DtTemp.Rows.Count > 0 Then
            GodownCode = AgL.XNull(DtTemp.Rows(0)("GodownCode"))
            GodownName = AgL.XNull(DtTemp.Rows(0)("GodownName"))
        End If
    End Sub
End Class