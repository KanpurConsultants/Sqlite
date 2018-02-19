Public Class ClsFunction
    Dim MDI As New MDIMain
    Dim WithEvents ObjRepFormGlobal As AgLibrary.RepFormGlobal
    Dim WithEvents ReportFrm As ReportLayout.FrmReportLayout
    Dim CRepProc As ClsReportProcedures

    Public Function FOpen(ByVal StrSender As String, ByVal StrSenderText As String, Optional ByVal IsEntryPoint As Boolean = True)
        Dim mQry As String = ""
        Dim FrmObj As Form = Nothing
        Dim StrUserPermission As String
        Dim DTUP As New DataTable
        Dim ADMain As OleDb.OleDbDataAdapter = Nothing
        Dim strNCat As String = ""

        'For User Permission Open

        StrUserPermission = AgIniVar.FunGetUserPermission(ClsMain.ModuleName, StrSender, StrSenderText, DTUP)

        If AgL.StrCmp(AgL.PubUserName, "SA") Then
            StrUserPermission = "AEDP"
        End If

        If AgL.PubDivisionList = "('')" Then AgL.PubDivisionList = "('" + AgL.PubDivCode + "')"

        If IsEntryPoint Then
            Select Case StrSender
                Case MDI.MnuDesignationMaster.Name
                    FrmObj = New FrmDesignation(StrUserPermission, DTUP)

                Case MDI.MnuDepartmentMaster.Name
                    FrmObj = New FrmDepartment(StrUserPermission, DTUP)

                Case MDI.MnuEmployeeMaster.Name
                    FrmObj = New FrmEmployee(StrUserPermission, DTUP)
                    CType(FrmObj, FrmEmployee).MasterType = AgTemplate.ClsMain.SubgroupType.Employee

                Case Else
                    FrmObj = Nothing
            End Select
        Else
            ReportFrm = New ReportLayout.FrmReportLayout(ClsMain.ModuleName, StrSender, StrSenderText, AgL.PubReportPath)
            CRepProc = New ClsReportProcedures(ReportFrm)
            CRepProc.GRepFormName = Replace(Replace(Replace(Replace(StrSenderText, "&", ""), " ", ""), "(", ""), ")", "")
            CRepProc.Ini_Grid()
            FrmObj = ReportFrm
        End If

        If FrmObj IsNot Nothing Then
            FrmObj.Text = StrSenderText
        End If
        Return FrmObj
    End Function

    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub
End Class

