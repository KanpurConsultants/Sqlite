Imports CrystalDecisions.CrystalReports.Engine
Public Class FrmDIstributerQuery
    Dim mQry As String = ""

    Public WithEvents Dgl1 As New AgControls.AgDataGrid
    Public WithEvents Dgl2 As New AgControls.AgDataGrid

    Public Sub New(ByVal StrUPVar As String, ByVal DTUP As DataTable)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, 0)
    End Sub

    Private Sub KeyDown_Form(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.F2 Or e.KeyCode = Keys.F3 Or e.KeyCode = Keys.F4 Or e.KeyCode = (Keys.F And e.Control) Or e.KeyCode = (Keys.P And e.Control) _
        Or e.KeyCode = (Keys.S And e.Control) Or e.KeyCode = Keys.Escape Or e.KeyCode = Keys.F5 Or e.KeyCode = Keys.F10 _
        Or e.KeyCode = Keys.Home Or e.KeyCode = Keys.PageUp Or e.KeyCode = Keys.PageDown Or e.KeyCode = Keys.End Then
        End If


        If Me.ActiveControl IsNot Nothing Then
            If Not (TypeOf (Me.ActiveControl) Is AgControls.AgDataGrid) Then
                If e.KeyCode = Keys.Return Then SendKeys.Send("{Tab}")
            End If
        End If
    End Sub

    Sub KeyPress_Form(ByVal Sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        If e.KeyChar = Chr(Keys.Escape) Then Exit Sub
        If Me.ActiveControl Is Nothing Then Exit Sub
        AgL.CheckQuote(e)
    End Sub

    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            AgL.WinSetting(Me, 510, 888, 0, 0)
            AgL.GridDesign(Dgl1)
            AgL.GridDesign(Dgl2)
            IniGrid()
            Ini_List()
            DispText()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Sub Ini_List()
        Try
            mQry = " Select SubCode As Code, ManualCode As Distributer From SubGroup Where SubGroupType = '" & ClsMain.SubGroupType.Distributer & "' "
            TxtDistributerId.AgHelpDataSet(0) = AgL.FillData(mQry, AgL.GCn)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Topctrl1_tbAdd()
        BlankText()
        DispText(True)
    End Sub

    Private Sub BlankText()
        TcEnviro.SelectedTab = TpDistributer
    End Sub

    Private Sub DispText(Optional ByVal Enb As Boolean = False)
        'Coding To Enable/Disable Controls
    End Sub

    Private Sub ProcFillDistributerDetails()
        Dim DsTemp As DataSet = Nothing
        Try
            mQry = " Select Sg.DispName, Sg.DOB, Sg.DateOfEnrollment, Sg1.ManualCode  as UpLine, Sg.CoDistributer, Sg.CoDistributerDOB, Sg.Add1, " & _
                        " C.CityName, C.State, Sg.DistributerGroup, Dg.ManualCode as DirectorCode, Dg.DispName as DirectorName, Sg.Phone, Sg.EMail, Sg.PAN, Sg.EMail, Sg.BankName, Sg.BankAcNo, Sg.Area " & _
                        " from SubGroup Sg  " & _
                        " left join SubGroup Sg1 On Sg.ParentDistributer = Sg1.SubCode" & _
                        " left join SubGroup DG On Sg.DistributerGroup = DG.SubCode" & _
                        " left join City C on Sg.CityCode = C.CityCode " & _
                        " where Sg.SubCode = '" & TxtDistributerId.AgSelectedValue & "' "
            DsTemp = AgL.FillData(mQry, AgL.GCn)
            With DsTemp.Tables(0)
                If .Rows.Count > 0 Then
                    TxtDistributerName.Text = AgL.XNull(.Rows(0)("DispName"))
                    TxtDistributerDOB.Text = AgL.XNull(.Rows(0)("DOB"))
                    TxtAddress.Text = AgL.XNull(.Rows(0)("Add1"))
                    TxtCity.Text = AgL.XNull(.Rows(0)("CityName"))
                    TxtPhone.Text = AgL.XNull(.Rows(0)("Phone"))
                    TxtEMail.Text = AgL.XNull(.Rows(0)("EMail"))                    
                    TxtDateOfEnrollment.Text = AgL.XNull(.Rows(0)("DateOfEnrollment"))
                    TxtDirectorCode.Text = AgL.XNull(.Rows(0)("DirectorCode"))
                    TxtDirectorGroup.Text = AgL.XNull(.Rows(0)("DirectorName"))
                    TxtParentDistributer.Text = AgL.XNull(.Rows(0)("UpLine"))
                    TxtCODistributer.Text = AgL.XNull(.Rows(0)("CoDistributer"))
                    TxtState.Text = AgL.XNull(.Rows(0)("State"))
                    TxtCoDistributerDOB.Text = AgL.XNull(.Rows(0)("CoDistributerDOB"))
                    TxtPanNo.Text = AgL.XNull(.Rows(0)("PAN"))
                    TxtBankName.Text = AgL.XNull(.Rows(0)("BankName"))
                    TxtBankAcNo.Text = AgL.XNull(.Rows(0)("BankAcNo"))
                End If
            End With

            TxtSelfPV.Text = ClsProj.FGetSelfPV(TxtDistributerId.AgSelectedValue, True)
            TxtGroupPV.Text = ClsProj.FGetGroupPV(TxtDistributerId.AgSelectedValue, True)
            TxtTotalPV.Text = Val(TxtSelfPV.Text) + Val(TxtGroupPV.Text)
            TxtTotalCumPV.Text = ClsProj.FGetTotalComPV(TxtDistributerId.AgSelectedValue, True)
            TxtTotalBV.Text = ClsProj.FGetGroupBV(TxtDistributerId.AgSelectedValue, True)
            TxtPVPer.Text = ClsProj.FGetPVPer(ClsProj.FGetTotalComBV(TxtDistributerId.AgSelectedValue, True))


            TxtSelfPVLastMonth.Text = ClsProj.FGetSelfPV(TxtDistributerId.AgSelectedValue, False)
            TxtGroupPVLastMonth.Text = ClsProj.FGetGroupPV(TxtDistributerId.AgSelectedValue, False)
            TxtTotalPVLastMonth.Text = Val(TxtSelfPVLastMonth.Text) + Val(TxtGroupPVLastMonth.Text)
            TxtTotalBVLastMonth.Text = ClsProj.FGetGroupBV(TxtDistributerId.AgSelectedValue, False)
            TxtPVPerLastMonth.Text = ClsProj.FGetPVPer(ClsProj.FGetTotalComBV(TxtDistributerId.AgSelectedValue, False))

            TxtPrevCumPV.Text = ClsProj.FGetTotalComPV(TxtDistributerId.AgSelectedValue, False)


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub TxtDistributerId_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtDistributerId.Validating
        Try
            Call ProcFillDistributerDetails()
            Call FillDownLinePVDetails(TxtDistributerId.AgSelectedValue)
            Call FillDistributorBusinessDetails(TxtDistributerId.AgSelectedValue)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub



    Private Sub IniGrid()
        AgL.AddAgDataGrid(Dgl1, Pnl1)
        Dgl1.EnableHeadersVisualStyles = False
        Dgl1.ColumnHeadersHeight = 35
        Dgl1.ReadOnly = True


        AgL.AddAgDataGrid(Dgl2, Pnl2)
        Dgl2.EnableHeadersVisualStyles = False
        Dgl2.ColumnHeadersHeight = 35
        Dgl2.ReadOnly = True

    End Sub

    Private Sub FillDownLinePVDetails(ByVal Distributer As String)
        Dim I As Integer = 0
        Dim bQry$ = ""
        Dim bTempTable$ = ""
        Dim DtTemp As DataTable = Nothing
        Dim bSelfPV, bGroupPV, bTotalPV, bPVPer, bCumPV, bTotalBV As Double
        Try
            bTempTable = AgL.GetGUID(AgL.GCn).ToString
            mQry = " CREATE TABLE [#" & bTempTable & "] " & _
                    " (JoiningDate  NVARCHAR(20), Distributer NVARCHAR(20), DistributerId NVARCHAR(100), " & _
                    " DistributerName NVARCHAR(100), " & _
                    " SelfPV Float, GroupPV Float, TotalPV Float, PVPer Float, CumPV Float, TotalBV Float)  "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

            mQry = " WITH DirectReports (ParentDistributer, SubCode, Level)  " & _
                        " AS   " & _
                        " (   " & _
                        " SELECT Sg.ParentDistributer , Sg.SubCode, 0 AS Level   " & _
                        " FROM SubGroup Sg    " & _
                        " WHERE Sg.ParentDistributer = '" & Distributer & "' " & _
                        " UNION ALL   " & _
                        " SELECT D.ParentDistributer, Sg.SubCode, Level + 1   " & _
                        " FROM (SELECT ParentDistributer, SubCode FROM SubGroup    " & _
                        " WHERE SubGroupType = '" & ClsMain.SubGroupType.Distributer & "')   " & _
                        " AS  Sg   " & _
                        " INNER JOIN DirectReports d ON Sg.ParentDistributer = d.SubCode   " & _
                        " )   " & _
                        " " & _
                        " INSERT INTO [#" & bTempTable & "] (JoiningDate, Distributer, DistributerId, DistributerName) " & _
                        " SELECT Sg.DateOfEnrollment, D.SubCode, Sg.ManualCode, Sg.DispName " & _
                        " FROM DirectReports  D " & _
                        " LEFT JOIN SubGroup Sg ON D.SubCode = Sg.SubCode "

            mQry = "INSERT INTO [#" & bTempTable & "] (JoiningDate, Distributer, DistributerId, DistributerName) " & _
                        " SELECT Sg.DateOfEnrollment, Sg.SubCode, Sg.ManualCode, Sg.DispName " & _
                        " From SubGroup Sg Where ParentDistributer = '" & Distributer & "' "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

            mQry = " Select Distributer From [#" & bTempTable & "] "
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

            With DtTemp
                For I = 0 To .Rows.Count - 1
                    If .Rows.Count > 0 Then
                        bSelfPV = ClsProj.FGetSelfPV(AgL.XNull(DtTemp.Rows(I)("Distributer")), True)
                        bGroupPV = ClsProj.FGetGroupPV(AgL.XNull(.Rows(I)("Distributer")), True)
                        bTotalPV = bSelfPV + bGroupPV
                        bCumPV = ClsProj.FGetTotalComPV(AgL.XNull(.Rows(I)("Distributer")), True)
                        bTotalBV = ClsProj.FGetGroupBV(AgL.XNull(.Rows(I)("Distributer")), True)
                        bPVPer = ClsProj.FGetPVPer(ClsProj.FGetTotalComPV(AgL.XNull(.Rows(I)("Distributer")), False))

                        'TxtSelfPV.Text = ClsProj.FGetSelfPV(TxtDistributerId.AgSelectedValue, True)
                        'TxtGroupPV.Text = ClsProj.FGetGroupPV(TxtDistributerId.AgSelectedValue, True)
                        'TxtTotalPV.Text = Val(TxtSelfPV.Text) + Val(TxtGroupPV.Text)
                        'TxtTotalCumPV.Text = ClsProj.FGetTotalComPV(TxtDistributerId.AgSelectedValue, True)
                        'TxtTotalBV.Text = ClsProj.FGetGroupBV(TxtDistributerId.AgSelectedValue, True)
                        'TxtPVPer.Text = ClsProj.FGetPVPer(TxtTotalBV.Text)



                        mQry = " INSERT INTO [#" & bTempTable & "] (Distributer, SelfPV, GroupPV, TotalPV, " & _
                                " PVPer, CumPV, TotalBV) " & _
                                " SELECT '" & AgL.XNull(.Rows(I)("Distributer")) & "', " & _
                                " " & bSelfPV & " ," & bGroupPV & " ," & bTotalPV & ", " & _
                                " " & bPVPer & ", " & bCumPV & ", " & bTotalBV & ""
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
                    End If
                Next
            End With

            mQry = "Select Distributer As DistributerCode, Convert(Varchar,Max(JoiningDate),106) As [Joining Date] , Max(DistributerId) As  [Distributer], " & _
                    " Max(DistributerName) As [Distributer Name] , " & _
                    " Sum(SelfPV) As [Self PV] , Sum(GroupPV) As [Group PV], Sum(TotalPV) As [Total PV], " & _
                    " Sum(PVPer) As [PV Per], Sum(CumPV) As [Cum PV], Sum(TotalBV) As [Total BV] " & _
                    " From [#" & bTempTable & "] Group By Distributer  "
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
            Dgl1.DataSource = DtTemp

            Dgl1.Columns("DistributerCode").Visible = False
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub FillDistributorBusinessDetails(ByVal Distributer As String)
        Dim I As Integer = 0
        Dim bQry$ = ""
        Dim bTempTable$ = ""
        Dim DtTemp As DataTable = Nothing
        Dim bPrevCumPV As Double, bSelfPV As Double, bGroupPV As Double, bTotalPV As Double, bPVPer As Double, bCumPV As Double, bTotalBV As Double
        Dim bDistributer$ = ""
        Try
            bTempTable = AgL.GetGUID(AgL.GCn).ToString
            mQry = " CREATE TABLE [#" & bTempTable & "] " & _
                    " (JoiningDate  NVARCHAR(20), Distributer NVARCHAR(20), DistributerId NVARCHAR(100), " & _
                    " DistributerName NVARCHAR(100), " & _
                    "  PrevCumPV Float, SelfPV Float, GroupPV Float, TotalPV Float, PVPer Float, CumPV Float, TotalBV Float, Level INT)  "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

            mQry = " WITH DirectReports (ParentDistributer, SubCode, Level)  " & _
                        " AS   " & _
                        " (   " & _
                        " SELECT Sg.ParentDistributer , Sg.SubCode, 0 AS Level   " & _
                        " FROM SubGroup Sg    " & _
                        " WHERE Sg.ParentDistributer = '" & Distributer & "' " & _
                        " UNION ALL   " & _
                        " SELECT D.ParentDistributer, Sg.SubCode, Level + 1   " & _
                        " FROM (SELECT ParentDistributer, SubCode FROM SubGroup    " & _
                        " WHERE SubGroupType = '" & ClsMain.SubGroupType.Distributer & "') " & _
                        " AS  Sg   " & _
                        " INNER JOIN DirectReports d ON Sg.ParentDistributer = d.SubCode " & _
                        " )   " & _
                        " " & _
                        " INSERT INTO [#" & bTempTable & "] (JoiningDate, Distributer, DistributerId, DistributerName, Level) " & _
                        " SELECT Sg.DateOfEnrollment, D.SubCode, Sg.ManualCode, Sg.DispName, D.Level " & _
                        " FROM DirectReports  D " & _
                        " LEFT JOIN SubGroup Sg ON D.SubCode = Sg.SubCode " & _
                        " Union All " & _
                        " SELECT Sg.DateOfEnrollment, Sg.SubCode, Sg.ManualCode, Sg.DispName, 1  " & _
                        " FROM SubGroup Sg where Sg.SubCode = '" & TxtDistributerId.Tag & "' "

            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)



            mQry = " Select Distributer, Level From [#" & bTempTable & "] "
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

            With DtTemp
                For I = 0 To .Rows.Count - 1
                    If .Rows.Count > 0 Then

                        bPrevCumPV = ClsProj.FGetTotalComPV(AgL.XNull(.Rows(I)("Distributer")), False)
                        bSelfPV = ClsProj.FGetSelfPV(AgL.XNull(DtTemp.Rows(I)("Distributer")), True)
                        bGroupPV = ClsProj.FGetGroupPV(AgL.XNull(.Rows(I)("Distributer")), True)
                        bTotalPV = bSelfPV + bGroupPV
                        bCumPV = ClsProj.FGetTotalComPV(AgL.XNull(.Rows(I)("Distributer")), True)
                        bTotalBV = ClsProj.FGetGroupBV(AgL.XNull(.Rows(I)("Distributer")), True)
                        bPVPer = ClsProj.FGetPVPer(ClsProj.FGetTotalComBV(AgL.XNull(.Rows(I)("Distributer")), True))

                        mQry = " INSERT INTO [#" & bTempTable & "] (Distributer, PrevCumPV, SelfPV, GroupPV, TotalPV, " & _
                                " PVPer, CumPV, TotalBV) " & _
                                " SELECT '" & AgL.XNull(.Rows(I)("Distributer")) & "', " & _
                                " " & bPrevCumPV & " ," & bSelfPV & " ," & bGroupPV & " ," & bTotalPV & ", " & _
                                " " & bPVPer & ", " & bCumPV & ", " & bTotalBV & ""
                        AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)
                    End If
                Next
            End With

            mQry = "Select H.Distributer As DistributerCode, Convert(Varchar,Max(H.JoiningDate),106) As [Joining Date], Max(Sg1.ManualCode) As [Parent Distributer],  " & _
                    " Max(H.DistributerId) As  [Distributer], " & _
                    " Max(Space(H.Level)) + Max(H.DistributerName) As [Distributer Name] , " & _
                    " Sum(H.PrevCumPV) As [Prev Cum PV] , Sum(H.SelfPV) As [Self PV] , Sum(H.GroupPV) As [Group PV], Sum(H.TotalPV) As [Total PV], " & _
                    " Sum(H.PVPer) As [PV Per], Sum(H.CumPV) As [Cum PV], Sum(H.TotalBV) As [Total BV] " & _
                    " From [#" & bTempTable & "] H  " & _
                    " LEFT JOIN SubGroup Sg On H.Distributer = Sg.SubCode " & _
                    " LEFT JOIN SubGroup Sg1 On Sg.ParentDistributer = Sg1.SubCode " & _
                    " Group By Distributer  "
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
            Dgl2.DataSource = DtTemp

            Dgl2.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells)

            Dgl2.Columns("DistributerCode").Visible = False
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub Dgl1_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles Dgl1.CellDoubleClick
        Try
            TxtDistributerId.AgSelectedValue = Dgl1.Item("DistributerCode", Dgl1.CurrentCell.RowIndex).Value
            Call ProcFillDistributerDetails()
            TcEnviro.SelectedTab = TpDistributer
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub PrintReport()
        Dim mCrd As New ReportDocument
        Dim I As Integer = 0
        Dim ReportView As New AgLibrary.RepView
        Dim DsRep As New DataSet
        Dim strQry As String = "", RepName As String = "", RepTitle As String = ""
        Dim bTableName As String = "", bSecTableName As String = "", bCondstr As String = ""
        Dim bStructJoin As String = ""

        Try
            Me.Cursor = Cursors.WaitCursor
            AgL.PubReportTitle = "Distributer Business Report"
            RepName = "AMPLE_BusinessReport" : RepTitle = "Distributer Business Report"

            mQry = " DECLARE @DistributerBusiness As Table " & _
                    " ( " & _
                    " DistributerId          NVARCHAR (100), " & _
                    " DistributerName        NVARCHAR (Max), " & _
                    " ParentId               NVARCHAR (100), " & _
                    " PrevCumPV              Float NULL, " & _
                    " SelfPV                 Float NULL, " & _
                    " GroupPV                Float NULL, " & _
                    " TotalPV                Float NULL, " & _
                    " PVPer                  Float NULL, " & _
                    " CUMPV                  Float NULL, " & _
                    " TotalBV                Float NULL " & _
                    " ) "

            With Dgl2
                For I = 0 To .Rows.Count - 1
                    If .Item("Distributer", I).Value <> "" Then
                        mQry += " INSERT INTO @DistributerBusiness ( " & _
                                 " DistributerId, " & _
                                 " DistributerName, " & _
                                 " ParentID, " & _
                                 " PrevCumPV, " & _
                                 " SelfPV, " & _
                                 " GroupPV, " & _
                                 " TotalPV, " & _
                                 " PVPer, " & _
                                 " CUMPV, " & _
                                 " TotalBV " & _
                                 " ) " & _
                                 " VALUES (" & AgL.Chk_Text(.Item("Distributer", I).Value) & ", " & _
                                 " " & AgL.Chk_Text(.Item("Distributer Name", I).Value) & ", " & _
                                 " " & AgL.Chk_Text(.Item("Parent Distributer", I).Value) & ", " & _
                                 " " & Val(.Item("Prev Cum PV", I).Value) & ", " & _
                                 " " & Val(.Item("Self PV", I).Value) & ", " & _
                                 " " & Val(.Item("Group PV", I).Value) & ", " & _
                                 " " & Val(.Item("Total PV", I).Value) & ", " & _
                                 " " & Val(.Item("PV Per", I).Value) & ", " & _
                                 " " & Val(.Item("CUM PV", I).Value) & ", " & _
                                 " " & Val(.Item("Total PV", I).Value) & ")"
                    End If
                Next
            End With

            mQry += " SELECT DistributerId, " & _
                     " DistributerName, ParentID, " & _
                     " PrevCumPv, SelfPV, " & _
                     " GroupPV, " & _
                     " TotalPV, " & _
                     " PVPer, " & _
                     " CUMPV, " & _
                     " TotalBV " & _
                     " FROM @DistributerBusiness  "

            AgL.ADMain = New SqlClient.SqlDataAdapter(mQry, AgL.GCn)
            AgL.ADMain.Fill(DsRep)
            AgPL.CreateFieldDefFile1(DsRep, AgL.PubReportPath & "\" & RepName & ".ttx", True)
            mCrd.Load(AgL.PubReportPath & "\" & RepName & ".rpt")
            mCrd.SetDataSource(DsRep.Tables(0))
            CType(ReportView.Controls("CrvReport"), CrystalDecisions.Windows.Forms.CrystalReportViewer).ReportSource = mCrd
            AgPL.Formula_Set(mCrd, RepTitle)
            AgPL.Show_Report(ReportView, "* " & RepTitle & " *", Me.MdiParent)

        Catch Ex As Exception
            MsgBox(Ex.Message)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub BtnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnPreview.Click
        PrintReport()
    End Sub
End Class
