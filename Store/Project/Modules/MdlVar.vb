Module MdlVar
    Public StrDocID As String       'Holds DocId Or Key Field On Save And Is Free After Save Is Executed    
    Public StrPath As String = My.Application.Info.DirectoryPath + "\"
    Public IniName As String = "Store.ini"
    Public StrDBPasswordSQL As String = ""
    Public StrDBPasswordAccess As String = "jai"
    Public AgL As AgLibrary.ClsMain
    Public AgCL As New AgControls.AgLib()
    Public AgPL As AgLibrary.ClsPrinting
    Public ObjAgTemplate As AgTemplate.ClsMain
    Public AgIniVar As AgLibrary.ClsIniVariables
    Public ClsMain_ReportLayout As ReportLayout.ClsMain
    Public RowLockedColour As Color = Color.AliceBlue

    Public DtCommon_Enviro As DataTable = Nothing
    Public ClsMain_Structure As AgStructure.ClsMain
    Public ReportPath As String
    Public PubDefaultGodownCode As String
    Public PubDefaultGodownName As String

    Public Enum StockFormType
        Opening = 0
        Transfer_Issue = 1
        Transfer_Receive = 2
    End Enum

    Public Enum StockTransferType
        Transfer_Issue = 0
        Transfer_Receive = 1
    End Enum
End Module
