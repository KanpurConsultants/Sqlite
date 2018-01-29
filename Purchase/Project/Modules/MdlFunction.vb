Imports System.Data.SQLite
Module MdlFunction
    Public FrmNew As Form
    Dim gCmd As New SqliteCommand
    Public Const ConExpense As Byte = 0
    Public Const ConExpCode As Byte = 1
    Public Const ConPer As Byte = 2
    Public Const ConAccount As Byte = 3
    Public Const ConExpType As Byte = 4
    Public Const ConCalcOn As Byte = 5
    Public Const ConOnAmt As Byte = 6
    Public Const ConFormulaString As Byte = 7
    Public Const ConDrCr As Byte = 8
    Public Const ConFillAmt As Byte = 9

    Public Function FOpenIni(ByVal StrIniPath As String, ByVal StrUserName As String, ByVal StrPassword As String) As Boolean
        Dim OLECmd As New OleDb.OleDbCommand
        Dim BlnRtn As Boolean = False
        Dim ECmd As SqliteCommand

        Try
            AgL = New AgLibrary.ClsMain : AgL.AglObj = AgL
            ClsMain_ReportLayout = New ReportLayout.ClsMain(AgL)
            ObjAgTemplate = New AgTemplate.ClsMain(AgL)

            AgL.PubDBUserSQL = "sa"
            AgL.PubDBPasswordSQL = AgL.DCODIFY(AgL.INIRead(StrIniPath, "Security", "Password", ""))
            AgL.PubServerName = AgL.INIRead(StrIniPath, "Server", "Name", "")
            AgL.PubReportPath = AgL.INIRead(StrIniPath, "Reports", "Path", "")
            AgL.PubCompanyDBName = AgL.INIRead(StrIniPath, "CompanyInfo", "Path", "")
            If AgL.PubDBPasswordSQL <> "" Then AgL.PubChkPasswordSQL = "Y"
            AgL.PubChkPasswordAccess = AgL.INIRead(StrIniPath, "Security", "PasswordAccess", "")


            AgL.PubReportPath = My.Application.Info.DirectoryPath & "\Reports"
            AgL.PubReportFaPath = My.Application.Info.DirectoryPath & "\ReportFA"

            AgL.PubDivCode = "M"

            AgIniVar = New AgLibrary.ClsIniVariables(AgL)

            BlnRtn = AgIniVar.FOpenIni(StrUserName, StrPassword)


            OLECmd = Nothing
        Catch Ex As Exception
            BlnRtn = False
            MsgBox(Ex.Message, MsgBoxStyle.Information, AgLibrary.ClsMain.PubMsgTitleInfo)
        Finally
            ECmd = Nothing
            AgPL = New AgLibrary.ClsPrinting(AgL)

            FOpenIni = BlnRtn
        End Try
    End Function

    Public Sub IniDtCommon_Enviro()
        AgL.PubDtEnviro = AgL.FillData("SELECT E.* FROM Enviro E  WHERE E.Site_Code ='" & AgL.PubSiteCode & "'", AgL.GCn).Tables(0)
    End Sub
End Module