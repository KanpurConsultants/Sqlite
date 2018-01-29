Public Class MDIMain

    Private Sub MDIMain_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Dim mCount As Integer = 0
        If e.KeyCode = Keys.Escape Then
            For Each ChildForm As Form In Me.MdiChildren
                mCount = mCount + 1
            Next

            If mCount = 0 Then
                If MsgBox("Do You Want to Exit?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                    'End
                End If
            End If
        End If
    End Sub

    Private Sub MDIMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If AgL Is Nothing Then
            If FOpenIni(StrPath + IniName, AgLibrary.ClsConstant.PubSuperUserName, AgLibrary.ClsConstant.PubSuperUserPassword) Then
                AgIniVar.FOpenConnection("1", "1", False)
            End If
            AgL.PubDivCode = "W"
            AgL.PubDivName = AgL.Dman_Execute("Select Div_Name From Division Where Div_Code = '" & AgL.PubDivCode & "'", AgL.GcnRead).ExecuteScalar

            IniDtCommon_Enviro()

            'Dim x As New ClsMain(AgL)
            'Dim CLsObj_AgStructure As New AgStructure.ClsMain(AgL)
            'Dim CLsObj_AgTemplate As New AgTemplate.ClsMain(AgL)
            'CLsObj_AgStructure.UpdateTableStructure(AgL.PubMdlTable)
            'CLsObj_AgTemplate.UpdateTableStructureWorkOrder(AgL.PubMdlTable)

            'x.UpdateTableStructure(AgL.PubMdlTable)
            'AgL.FExecuteDBScript(AgL.PubMdlTable, AgL.GCn)
        End If
    End Sub

    Private Sub MnuMaster_DropDownItemClicked(ByVal sender As Object, ByVal e As System.Windows.Forms.ToolStripItemClickedEventArgs) Handles MnuJobWork.DropDownItemClicked, MnuJobWorkReport.DropDownItemClicked
        Dim FrmObj As Form
        Dim CFOpen As New ClsFunction
        Dim bIsEntryPoint As Boolean

        If e.ClickedItem.Tag Is Nothing Then e.ClickedItem.Tag = ""
        If e.ClickedItem.Tag.Trim = "" Then
            bIsEntryPoint = True
        Else
            bIsEntryPoint = False
        End If

        FrmObj = CFOpen.FOpen(e.ClickedItem.Name, e.ClickedItem.Text, bIsEntryPoint)
        If FrmObj IsNot Nothing Then
            FrmObj.MdiParent = Me
            FrmObj.Show()
            FrmObj = Nothing
        End If

    End Sub
End Class
