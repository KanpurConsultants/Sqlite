Public Class ClsReportProcedures

#Region "Danger Zone"
    Dim StrArr1() As String = Nothing, StrArr2() As String = Nothing, StrArr3() As String = Nothing, StrArr4() As String = Nothing, StrArr5() As String = Nothing

    Dim mGRepFormName As String = ""

    Dim WithEvents ReportFrm As ReportLayout.FrmReportLayout

    Public Property GRepFormName() As String
        Get
            GRepFormName = mGRepFormName
        End Get
        Set(ByVal value As String)
            mGRepFormName = value
        End Set
    End Property

#End Region

#Region "Common Reports Constant"
    Private Const CityList As String = "CityList"
    Private Const UserWiseEntryReport As String = "UserWiseEntryReport"
    Private Const UserWiseEntryTargetReport As String = "UserWiseEntryTargetReport"
#End Region

#Region "Reports Constant"
    Private Const ProductionOrderReport As String = "ProductionOrderReport"
    Private Const ProductionOrderStatus As String = "ProductionOrderStatus"
#End Region

#Region "Queries Definition"
    Dim mHelpCityQry$ = "Select 'o' As Tick, CityCode, CityName From City "
    Dim mHelpStateQry$ = "Select 'o' As Tick, State_Code, State_Desc From State "
    Dim mHelpUserQry$ = "Select 'o' As Tick, User_Name As Code, User_Name As [User] From UserMast "
    Dim mHelpSiteQry$ = "Select 'o' As Tick, Code, Name As [Site] From SiteMast Where " & AgL.PubSiteCondition("Code", AgL.PubSiteCode) & " "
    Dim mHelpItemQry$ = "Select 'o' As Tick, Code, Description As [Item] From Item "
    Dim mHelpVendorQry$ = " Select 'o' As Tick,  H.SubCode As Code, Sg.DispName AS Vendor FROM Vendor H LEFT JOIN SubGroup Sg ON H.SubCode = Sg.SubCode "
    Dim mHelpTableQry$ = "Select 'o' As Tick, H.Code, H.Description AS [Table] FROM HT_Table H "
    Dim mHelpOutletQry$ = "Select 'o' As Tick, H.Code, H.Description AS [Table] FROM Outlet H "
    Dim mHelpStewardQry$ = "Select 'o' As Tick,  Sg.SubCode AS Code, Sg.DispName AS Steward FROM SubGroup Sg  "
    Dim mHelpPartyQry$ = " Select 'o' As Tick,  Sg.SubCode As Code, Sg.DispName AS Party FROM SubGroup Sg Where Sg.Nature In ('Customer','Supplier','Cash') "
    Dim mHelpBuyerQry$ = " Select 'o' As Tick,  Sg.SubCode As Code, Sg.DispName + ', ' + C.CityName AS Party FROM SubGroup Sg LEFT JOIN City C ON C.CityCode = SG.CityCode  Where Sg.MasterType ='Customer' "
    Dim mHelpSaleOrderQry$ = " Select 'o' As Tick,  H.DocID AS Code, H.V_Type + '-' + H.ReferenceNo  FROM SaleOrder H "

    Dim mHelpItemGroupQry$ = "Select 'o' As Tick, IG.Code, IG.Description As [Item Group], IC.Description as [Item Category], IT.Name as [Item Type] " & _
                         "From ItemGroup IG " & _
                         "LEFT JOIN ItemCategory IC ON IG.ItemCategory = IC.Code  " & _
                         "LEFT JOIN ItemType IT ON IC.ItemType = IT.Code  "
    Dim mHelpItemCategoryQry$ = "Select 'o' As Tick, IC.Code, IC.Description As [Item Category], IT.Name as [Item Type] " & _
                                "From ItemCategory IC " & _
                                "LEFT JOIN ItemType IT ON IC.ItemType = IT.Code  "

    Dim mHelpItemReportingGroupQry$ = "Select 'o' As Tick, Code, Description As [Group Name] From ItemReportingGroup "

    Dim mHelpEmployeeQry$ = " Select 'o' As Tick, SG.SubCode AS Code, SG.Name AS Employee  " & _
           " FROM SubGroup Sg " & _
           " WHERE ISNULL(SG.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "'" & _
           " AND SG.Div_Code ='" & AgL.PubDivCode & " ' AND SG.Site_Code = '" & AgL.PubSiteCode & "' ORDER BY SG.Name"

    Dim mHelpItemType$ = "SELECT 'o' AS Tick, Code, Name  AS ItemType  FROM ItemType ORDER BY Name "
    Dim mHelpDepartment$ = "SELECT 'o' AS Tick, Code, Description AS Department FROM Department ORDER BY Description "


    Dim mHelpJobOrderNoQry$ = " Select 'o' As Tick, H.DocID AS Code, H.V_Type + '-' + H.ManualRefNo AS [Order No] , P.Description AS Process, H.V_Date AS OrderDate  " & _
                " FROM JobOrder H " & _
                " LEFT JOIN Process P ON P.NCat = H.Process  " & _
                " WHERE H.Div_Code ='" & AgL.PubDivCode & " ' And H.Site_Code = '" & AgL.PubSiteCode & "' "

    Dim mHelpJobReceiveNoQry$ = " Select 'o' As Tick, H.DocID AS Code, H.V_Type + '-' + H.ManualRefNo AS [Receive No] , P.Description AS Process, H.V_Date AS ReceiveDate  " & _
            " FROM JobIssRec H " & _
            " LEFT JOIN Process P ON P.NCat = H.Process  " & _
            " WHERE H.Div_Code ='" & AgL.PubDivCode & " ' And H.Site_Code = '" & AgL.PubSiteCode & "' "

    Dim mHelpSaleOrderNoQry$ = " Select 'o' As Tick, S.DocID AS Code, S.ReferenceNo AS [Manual No] , S.V_Date AS OrderDate " & _
     " FROM SaleOrder S " & _
     " WHERE S.Div_Code ='" & AgL.PubDivCode & " ' And S.Site_Code = '" & AgL.PubSiteCode & "' "

    Dim mHelpIndentNo$ = " Select 'o' As Tick, H.DocID, Max(H.V_Type + '- ' + H.ManualRefNo) AS IndentNo , Max(H.V_Date) AS IndentDate " & _
                        " FROM PurchIndentDetail L  " & _
                        " LEFT JOIN PurchIndent H ON L.PurchIndent  = H.DocID " & _
                        " WHERE H.Div_Code = '" & AgL.PubDivCode & "'  AND H.Site_Code = '" & AgL.PubSiteCode & "' " & _
                        " Group By H.DocID "

    Dim mHelpPurchOrderNo$ = " Select 'o' As Tick, H.DocID, Max(H.V_Type + '- ' + H.ManualRefNo) AS OrderNo , Max(H.V_Date) AS OrderDate " & _
                        " FROM PurchOrderDetail L  " & _
                        " LEFT JOIN PurchOrder H ON L.PurchOrder  = H.DocID " & _
                        " WHERE H.Div_Code = '" & AgL.PubDivCode & "'  And H.Site_Code = '" & AgL.PubSiteCode & "' " & _
                        " Group By H.DocID "

    Dim mHelpChallanNo$ = " Select 'o' As Tick, H.DocID, Max(H.V_Type + '- ' + H.ReferenceNo) AS ChallanNo , Max(H.V_Date) AS ChallanDate " & _
                " FROM PurchChallanDetail L  " & _
                " LEFT JOIN PurchChallan H ON L.PurchChallan  = H.DocID " & _
                " WHERE H.Div_Code = '" & AgL.PubDivCode & "'  AND H.Site_Code = '" & AgL.PubSiteCode & "' " & _
                " Group By H.DocID "

    Dim mHelpRequisitionNo$ = " SELECT 'o' As Tick, H.DocID, H.ReferenceNo AS RequisitionNo, H.V_Date AS RequisitionDate   FROM Requisition H " & _
                    " WHERE H.Div_Code = '" & AgL.PubDivCode & "'  AND H.Site_Code = '" & AgL.PubSiteCode & "' "


    Dim mHelpInvoiceNo$ = " Select 'o' As Tick, H.DocID, Max(H.V_Type + '- ' + H.ReferenceNo) AS InvoiceNo , Max(H.V_Date) AS InvoiceDate " & _
                " FROM PurchInvoiceDetail L  " & _
                " LEFT JOIN PurchInvoice H ON L.PurchInvoice  = H.DocID " & _
                " WHERE H.Div_Code = '" & AgL.PubDivCode & "'  AND H.Site_Code = '" & AgL.PubSiteCode & "' " & _
                " Group By H.DocID "

    Dim mHelpProdOrderQry$ = " Select 'o' As Tick, P.DocID AS Code, P.V_Type + '-' + P.ManualRefNo AS [Manual No] , P.V_Date AS OrderDate " & _
         " FROM ProdOrder P " & _
         " WHERE P.Div_Code ='" & AgL.PubDivCode & " ' And P.Site_Code = '" & AgL.PubSiteCode & "' "

    Dim mHelpMaterialPlanNo$ = " Select 'o' As Tick, S.DocID, S.V_Type +'-'+ S.ManualRefNo AS [Production Plan No.], " & _
                            " S.V_Date AS [Date] " & _
                            " FROM MaterialPlan S  " & _
                            " LEFT JOIN Voucher_Type Vt ON Vt.V_Type=S.V_Type " & _
                            " WHERE S.Div_Code = '" & AgL.PubDivCode & "'  AND S.Site_Code = '" & AgL.PubSiteCode & "' "

    Dim mHelpJobWorkerQry$ = " Select 'o' As Tick,  S.SubCode AS Code,S.Name AS Worker,C.CityName AS City " & _
                         " FROM SubGroup S " & _
                         " LEFT JOIN City C ON C.CityCode = S.CityCode  " & _
                         " WHERE CharIndex('|' + '" & AgL.PubDivCode & "' + '|', S.DivisionList) > 0 " & _
                         " AND S.Site_Code = '" & AgL.PubSiteCode & "' " & _
                         " And ISNULL(S.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "'"

    Dim mHelpGodownQry$ = "Select 'o' As Tick, Code,Description AS Godown FROM Godown WHERE Div_Code ='" & AgL.PubDivCode & " ' AND Site_Code = '" & AgL.PubSiteCode & "' "

    Dim mHelpProcessQry$ = " Select 'o' As Tick,  NCat AS Code, Description FROM Process "
    Dim mHelpBillingOnQry$ = "Select 'Qty' As Code, 'Qty' As Name UNION ALL Select 'Measure' As Code, 'Measure' As Name UNION ALL Select 'Perimeter' As Code, 'Perimeter' As Name "
#End Region

    Dim DsRep As DataSet = Nothing, DsRep1 As DataSet = Nothing, DsRep2 As DataSet = Nothing
    Dim mQry$ = "", RepName$ = "", RepTitle$ = "", OrderByStr$ = ""

#Region "Initializing Grid"
    Public Sub Ini_Grid()
        Try
            Dim I As Integer = 0
            Select Case GRepFormName
                Case ProductionOrderReport
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubEndDate)
                    ReportFrm.CreateHelpGrid("VoucherType", "Voucher Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, FGetVoucher_TypeQry("ProdOrder"))
                    ReportFrm.CreateHelpGrid("Material Plan No", "Material Plan No", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpMaterialPlanNo, , , 450)
                    ReportFrm.CreateHelpGrid("Item", "Item", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemQry, , , 600, 270)
                    ReportFrm.CreateHelpGrid("Item Group", "Item Group", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemGroupQry, , , 530)
                    ReportFrm.CreateHelpGrid("Item Category", "Item Category", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemCategoryQry, , , 430)
                    ReportFrm.CreateHelpGrid("Item Type", "Item Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemType)
                    ReportFrm.CreateHelpGrid("Item Reporting Group", "Item Reporting Group", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemReportingGroupQry, , , 500, 360)

                Case ProductionOrderStatus
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubEndDate)
                    ReportFrm.CreateHelpGrid("Status On", "Status On", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubLoginDate)
                    ReportFrm.CreateHelpGrid("Report Type", "Report Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, "Select 'Summary' as Code, 'Summary' as Name Union All Select 'Detail' as Code, 'Detail' as Name Union All Select 'Item Wise Order Status' as Code, 'Item Wise Order Status' as Name ", "Summary", , , 250, , False)
                    ReportFrm.CreateHelpGrid("Report For", "Report For", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, " SELECT 'All' AS Code, 'All' AS Name UNION ALL  SELECT 'To Be Issue' AS Code, 'To Be Issue' AS Name UNION ALL  SELECT 'Over Due' AS Code, 'Over Due' AS Name UNION ALL  SELECT 'Over Due And To Be Issue' AS Code, 'Over Due And To Be Issue' AS Name ", , , , , , False)
                    ReportFrm.CreateHelpGrid("Unit", "Unit", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, "Select 'Qty' as Code, 'Qty' as Name Union All Select 'Measure' as Code, 'Measure' AS Name Union All Select 'Qty & Measure' as Code, 'Qty & Measure' AS Name ", "Qty")
                    ReportFrm.CreateHelpGrid("Voucher Type", "Voucher Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, FGetVoucher_TypeQry("ProdOrder"))
                    ReportFrm.CreateHelpGrid("Process", "Process", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpProcessQry, , , , , , False)
                    ReportFrm.CreateHelpGrid("Prod Order No", "Prod Order No", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpProdOrderQry, , , 420)
                    ReportFrm.CreateHelpGrid("Material Plan No", "Material Plan No", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpMaterialPlanNo, , , 420)
                    ReportFrm.CreateHelpGrid("Item", "Item", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemQry, , , 600, 270)
                    ReportFrm.CreateHelpGrid("Item Group", "Item Group", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemGroupQry, , , 530)
                    ReportFrm.CreateHelpGrid("Item Category", "Item Category", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemCategoryQry, , , 430)
                    ReportFrm.CreateHelpGrid("Item Type", "Item Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemType)
                    ReportFrm.CreateHelpGrid("Item Reporting Group", "Item Reporting Group", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemReportingGroupQry, , , 500, 360)
                    ReportFrm.CreateHelpGrid("Process in New Page ?", "Process in New Page ?", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, "Select 'Yes' as Code, 'Yes' as Name Union All Select 'No' as Code, 'No' as Name", "No", , , , , False)

            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
#End Region

    Private Function FGetVoucher_TypeQry(ByVal TableName As String) As String
        FGetVoucher_TypeQry = " SELECT Distinct 'o' As Tick, H.V_Type AS Code, Vt.Description AS [Voucher Type] " & _
                                " FROM " & TableName & " H  " & _
                                " LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type "
    End Function

    Private Function FGetMainVoucher_TypeQry(ByVal HeaderTable As String, ByVal LineTableJoinStr As String) As String
        FGetMainVoucher_TypeQry = "Select DISTINCT 'o' As Tick, H.V_Type , Vt.Description " & _
            " FROM " & HeaderTable & "  L " & LineTableJoinStr & " " & _
            " LEFT JOIN Voucher_Type Vt ON Vt.V_Type = H.V_Type " & _
            " WHERE IsNull(H.V_Type,'') <> '' " & _
            " ORDER BY Vt.Description "
    End Function

    Private Sub ObjRepFormGlobal_ProcessReport() Handles ReportFrm.ProcessReport
        Select Case mGRepFormName
            Case ProductionOrderReport
                ProcProductionOrderReport()

            Case ProductionOrderStatus
                ProcProductionOrderStatusReport()
        End Select
    End Sub

    Public Sub New(ByVal mReportFrm As ReportLayout.FrmReportLayout)
        ReportFrm = mReportFrm
    End Sub

#Region "Production Order Report"
    Private Sub ProcProductionOrderReport()
        Dim mCondStr$ = ""
        RepName = "Plan_ProductionOrderReport"
        RepTitle = "Production Order Report"

        Try
            mCondStr = mCondStr & " And PO.V_Date Between " & AgL.Chk_Text(ReportFrm.FGetText(0)) & " And " & AgL.Chk_Text(ReportFrm.FGetText(1)) & " "
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("PO.V_Type", 2)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("PO1.MaterialPlan", 3)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("PO1.Item", 4)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemGroup", 5)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.ItemCategory", 6)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemType ", 7)

            If ReportFrm.FGetText(8) <> "" And ReportFrm.FGetText(8) <> "All" Then
                mQry = " Select '''' +  replace(ItemList,',',''',''')  + ''''  From ItemReportingGroup Where 1=1 " & ReportFrm.GetWhereCondition("Code", 8)
                mCondStr = mCondStr & " AND I.Code In (" & AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar & ")"
            End If

            mCondStr = mCondStr & " And PO.Site_Code = '" & AgL.PubSiteCode & "' "
            mCondStr = mCondStr & " And PO.Div_Code= '" & AgL.PubDivCode & "' "

            mQry = " SELECT  PO.DocID, PO.V_Type,PO.V_Date, PO.DueDate, PO.TotalQty, PO.TotalMeasure, PO.Remarks, PO.ManualRefNo,  " & _
                    " PO.EntryBy, PO.ApproveBy, PO1.Sr, PO1.Qty, PO1.Unit, PO1.MeasurePerPcs, PO1.TotalMeasure As TotalMeasureLine, Vt.Description AS VtDesc, " & _
                    " PO1.MeasureUnit, SM.Name AS SiteName,I.Description AS ItemDesc, MP.V_Type + '-' + MP.ManualRefNo  AS PlanNo, PO1.MaterialPlanSr  " & _
                    " FROM ProdOrder PO  " & _
                    " LEFT JOIN Voucher_Type Vt ON Vt.V_Type = PO.V_Type " & _
                    " LEFT JOIN ProdOrderDetail PO1 ON PO1.DocID =PO.DocID   " & _
                    " LEFT JOIN SiteMast SM ON  SM.Code=PO.Site_Code  " & _
                    " LEFT JOIN Item I ON I.Code=PO1.Item  " & _
                    " LEFT JOIN MaterialPlan MP ON MP.DocID=PO1.MaterialPlan  " & _
                    " WHERE 1=1 " & mCondStr & " Order By PO.V_Date "
            DsRep = AgL.FillData(mQry, AgL.GCn)

            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records to Print!")

            ReportFrm.PrintReport(DsRep, RepName, RepTitle, AgL.PubReportPath)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub
#End Region

#Region "Production Order Status Report"
    Private Sub ProcProductionOrderStatusReport()
        Dim mCondStr$ = ""
        Dim IsMultiProcess As Integer = 0
        Dim IsProcessinNewPage As Integer = 0

        Dim mQryJobOrder$ = " SELECT JOD.ProdOrder, JOD.ProdOrderSr , JO.ManualRefNo AS OrderNo, JO.V_Date AS OrderDate, JOD.Qty AS OrderQty, JOD.TotalMeasure AS OrderMeasure " & _
                                " FROM JobOrderDetail JOD " & _
                                " LEFT JOIN JobOrder JO ON JO.DocID = JOD.DocId " & _
                                " WHERE isnull(JOD.ProdOrder,'') <>'' " & _
                                " AND JO.V_Date <= '" & ReportFrm.FGetText(2) & "' "

        Dim mQryJobOrderSummury$ = " SELECT JOD.ProdOrder, JOD.ProdOrderSr, sum(JOD.Qty) AS OrderQty, sum(JOD.TotalMeasure) AS OrderMeasure, Max(JO.V_Date) AS MaxOrdDate " & _
                                " FROM JobOrderDetail JOD " & _
                                " LEFT JOIN JobOrder JO ON JO.DocID = JOD.DocId " & _
                                " WHERE isnull(JOD.ProdOrder,'') <>'' " & _
                                " AND JO.V_Date <= '" & ReportFrm.FGetText(2) & "' " & _
                                " GROUP BY JOD.ProdOrder, JOD.ProdOrderSr "

        If ReportFrm.FGetText(7).ToString.Contains(",") Or ReportFrm.FGetText(7) = "All" Then
            IsMultiProcess = 1
        Else
            IsMultiProcess = 0
        End If

        If ReportFrm.FGetText(15) = "Yes" Then
            IsProcessinNewPage = 1
        Else
            IsProcessinNewPage = 0
        End If

        Try

            If ReportFrm.FGetText(3) = "Detail" Then
                RepTitle = "Production Order Status Detail"
                If ReportFrm.FGetText(5) = "Measure" Then
                    RepName = "Plan_ProductionOrderStatusDetail_Measure"
                ElseIf ReportFrm.FGetText(5) = "Qty & Measure" Then
                    RepName = "Plan_ProductionOrderStatusDetail_QtyMeasure"
                Else
                    RepName = "Plan_ProductionOrderStatusDetail"
                End If
            ElseIf ReportFrm.FGetText(3) = "Item Wise Order Status" Then
                RepTitle = "Item Wise Production Order Status"
                If ReportFrm.FGetText(5) = "Measure" Then
                    RepName = "Plan_ProductionOrderStatusSummary_ItemWise_Measure"
                ElseIf ReportFrm.FGetText(5) = "Qty & Measure" Then
                    RepName = "Plan_ProductionOrderStatusSummary_ItemWise_QtyMeasure"
                Else
                    RepName = "Plan_ProductionOrderStatusSummary_ItemWise"
                End If
            Else
                RepTitle = "Production Order Status Summary"
                If ReportFrm.FGetText(5) = "Measure" Then
                    RepName = "Plan_ProductionOrderStatusSummary_Measure"
                ElseIf ReportFrm.FGetText(5) = "Qty & Measure" Then
                    RepName = "Plan_ProductionOrderStatusSummary_QtyMeasure"
                Else
                    RepName = "Plan_ProductionOrderStatusSummary"
                End If
            End If

            mCondStr = mCondStr & " And H.V_Date Between " & AgL.Chk_Text(ReportFrm.FGetText(0)) & " And " & AgL.Chk_Text(ReportFrm.FGetText(1)) & " "

            If ReportFrm.FGetText(4) = "To Be Issue" Then
                mCondStr = mCondStr & " AND L.Qty - isnull(JOS.OrderQty,0) > 0 "
            End If

            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.V_Type", 6)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Process", 7)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.DocId", 8)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.MaterialPlan ", 9)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Item", 10)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemGroup", 11)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.ItemCategory", 12)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemType ", 13)

            If ReportFrm.FGetText(14) <> "" And ReportFrm.FGetText(14) <> "All" Then
                mQry = " Select '''' +  replace(ItemList,',',''',''')  + ''''  From ItemReportingGroup Where 1=1 " & ReportFrm.GetWhereCondition("Code", 14)
                mCondStr = mCondStr & " AND I.Code In (" & AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar & ")"
            End If

            mCondStr = mCondStr & " And H.Site_Code = '" & AgL.PubSiteCode & "' "
            mCondStr = mCondStr & " And H.Div_Code= '" & AgL.PubDivCode & "' "

            mQry = " SELECT H.DocID, H.V_Type, H.V_Date, H.DueDate, H.Remarks, H.V_Type + ' - ' + H.ManualRefNo AS ProdOrderNo, Vt.Description AS VtDesc, " & _
                    " L.Sr, L.Item, L.Qty, L.Unit, I.Description AS ItemName, isnull(JOS.OrderQty,0) AS  TotalOrderQty, isnull(JOS.OrderMeasure,0) AS  TotalOrderMeasure, P.Description AS ProcessDesc, P.Sr AS ProcessSr, " & _
                    " JO.OrderNo, JO.OrderDate, isnull(JO.OrderQty,0) AS OrderQty, isnull(JO.OrderMeasure,0) AS OrderMeasure, U.DecimalPlaces AS DecimalPlaces, UM.DecimalPlaces AS MeasureDecimalPlace,   " & _
                    " L.Qty - isnull(JOS.OrderQty,0) AS BalToOrderQty, L.TotalMeasure - isnull(JOS.OrderMeasure,0) AS BalToOrderMeasure, L.TotalMeasure, L.MeasureUnit,  " & _
                    " CASE WHEN L.Qty - isnull(JOS.OrderQty,0) > 0 THEN  datediff(Day,H.DueDate,'" & ReportFrm.FGetText(2) & "') ELSE datediff(Day,H.DueDate,JOS.MaxOrdDate) END AS Ageing, " & _
                    " " & IsMultiProcess & " As IsMultiProcess, " & IsProcessinNewPage & " As IsProcessinNewPage " & _
                    " FROM ProdOrder H " & _
                    " LEFT JOIN ProdOrderDetail L ON L.DocId = H.DocID  " & _
                    " LEFT JOIN Voucher_Type Vt ON Vt.V_Type = H.V_Type  " & _
                    " LEFT JOIN Process P WITH (nolock) On L.Process = P.NCat   " & _
                    " LEFT JOIN Item I ON I.Code = L.Item " & _
                    " LEFT JOIN Unit U WITH (nolock) ON U.Code = L.Unit " & _
                    " LEFT JOIN Unit UM WITH (nolock) ON UM.Code = L.MeasureUnit " & _
                    " LEFT JOIN ( " & mQryJobOrder & " ) JO ON JO.ProdOrder = L.DocId AND JO.ProdOrderSr = L.Sr " & _
                    " LEFT JOIN ( " & mQryJobOrderSummury & " )  JOS ON JOS.ProdOrder = L.DocId AND JOS.ProdOrderSr = L.Sr " & _
                    " WHERE 1=1 " & mCondStr & " Order By H.V_Date "

            DsRep = AgL.FillData(mQry, AgL.GCn)
            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records to Print!")

            ReportFrm.PrintReport(DsRep, RepName, RepTitle, AgL.PubReportPath)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub
#End Region

End Class
