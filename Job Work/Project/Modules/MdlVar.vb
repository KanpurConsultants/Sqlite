Module MdlVar
    Public StrDocID As String       'Holds DocId Or Key Field On Save And Is Free After Save Is Executed    
    Public StrPath As String = My.Application.Info.DirectoryPath + "\"
    Public IniName As String = "JobWork.ini"
    Public StrDBPasswordSQL As String = ""
    Public StrDBPasswordAccess As String = "jai"
    Public AgL As AgLibrary.ClsMain
    Public AgCL As New AgControls.AgLib()
    Public AgPL As AgLibrary.ClsPrinting
    Public ClsMain_ReportLayout As ReportLayout.ClsMain
    Public ObjAgTemplate As AgTemplate.ClsMain
    Public AgIniVar As AgLibrary.ClsIniVariables
    Public RowLockedColour As Color = Color.AliceBlue
End Module
