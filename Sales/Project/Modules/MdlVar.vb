Module MdlVar
    Public StrDocID As String       'Holds DocId Or Key Field On Save And Is Free After Save Is Executed    
    Public StrPath As String = My.Application.Info.DirectoryPath + "\"
    Public IniName As String = "Sales.ini"
    Public StrDBPasswordSQL As String = ""
    Public StrDBPasswordAccess As String = "jai"
    Public AgL As AgLibrary.ClsMain
    Public AgCL As New AgControls.AgLib()
    Public AgPL As AgLibrary.ClsPrinting
    Public ClsMain_ReportLayout As ReportLayout.ClsMain
    Public ObjAgTemplate As AgTemplate.ClsMain
    Public AgIniVar As AgLibrary.ClsIniVariables
    Public RowLockedColour As Color = Color.AliceBlue


    Public IsApplicable_Enquiry As Boolean = True
    Public IsApplicable_Quotation As Boolean = True
    Public IsApplicable_Order As Boolean = True
    Public IsApplicable_OrderCancellation As Boolean = True
    Public IsApplicable_OrderAmendment As Boolean = True
    Public IsApplicable_DeliveryScheduleChange As Boolean = True
    Public IsApplicable_QcRequest As Boolean = True
    Public IsApplicable_Qc As Boolean = True
    Public IsApplicable_DeliveryOrder As Boolean = True
    Public IsApplicable_Challan As Boolean = True
    Public IsApplicable_ShippingBill As Boolean = True
    Public IsApplicable_SupplimentaryInvoice As Boolean = True
    Public IsApplicable_GoodsReturn As Boolean = True
End Module
