Module MdlVar
    Public StrDocID As String       'Holds DocId Or Key Field On Save And Is Free After Save Is Executed    
    Public StrPath As String = My.Application.Info.DirectoryPath + "\"
    Public IniName As String = "Purchase.ini"
    Public StrDBPasswordSQL As String = ""
    Public StrDBPasswordAccess As String = "jai"
    Public AgL As AgLibrary.ClsMain
    Public AgCL As New AgControls.AgLib()
    Public AgPL As AgLibrary.ClsPrinting
    Public ClsMain_ReportLayout As ReportLayout.ClsMain
    Public ObjAgTemplate As AgTemplate.ClsMain
    Public AgIniVar As AgLibrary.ClsIniVariables
    Public DtPurhcaseEnviro As DataTable = Nothing


    Public IsApplicable_Indent As Boolean
    Public IsApplicable_IndentCancellation As Boolean
    Public IsApplicable_Enquiry As Boolean
    Public IsApplicable_Quotation As Boolean
    Public IsApplicable_Order As Boolean
    Public IsApplicable_OrderCancellation As Boolean
    Public IsApplicable_OrderAmendment As Boolean
    Public IsApplicable_QC As Boolean
    Public IsApplicable_Challan As Boolean
    Public IsApplicable_SupplimentaryInvoice As Boolean
    Public IsApplicable_GoodsReturn As Boolean
End Module
