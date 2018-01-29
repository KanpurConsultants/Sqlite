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

#Region "Reports Constant"
    Private Const JobOrderReport As String = "JobOrderReport"
    Private Const JobReceiveReport As String = "JobReceiveReport"
    Private Const JobInvoiceReport As String = "JobInvoiceReport"
    Private Const ProcessOrderStatus As String = "ProcessOrderStatus"
    Private Const JobReceiveStatus As String = "JobReceiveStatus"
    Private Const ProcessBalanceReport As String = "ProcessBalanceReport"
    Private Const MaterialIssueFromJobOrderReport As String = "MaterialIssueFromJobOrderReport"
    Private Const JobQCReport As String = "JobQCReport"
    Private Const PeriodicJobOrderStatus As String = "PeriodicJobOrderStatus"
    Private Const PaymentCalculation As String = "PaymentCalculation"
    Private Const PaymentAdvise As String = "PaymentAdvise"
#End Region

#Region "Queries Definition"
    Dim mHelpCityQry$ = "Select 'o' As Tick, CityCode, CityName From City "
    Dim mHelpStateQry$ = "Select 'o' As Tick, State_Code, State_Desc From State "
    Dim mHelpUserQry$ = "Select 'o' As Tick, User_Name As Code, User_Name As [User] From UserMast "
    Dim mHelpSiteQry$ = "Select 'o' As Tick, Code, Name As [Site] From SiteMast Where " & AgL.PubSiteCondition("Code", AgL.PubSiteCode) & " "

    Dim mHelpItemQry$ = "Select 'o' As Tick, I.Code, I.Description As [Item], IG.Description as [Item Group], IC.Description as [Item Category] " & _
                    "From Item I " & _
                    "Left JOIN ItemGroup IG ON I.ItemGroup = IG.Code " & _
                    "LEFT JOIN ItemCategory IC ON IG.ItemCategory = IC.Code  " & _
                    "LEFT JOIN ItemType IT ON IC.ItemType = IT.Code "
    Dim mHelpItemGroupQry$ = "Select 'o' As Tick, IG.Code, IG.Description As [Item Group], IC.Description as [Item Category], IT.Name as [Item Type] " & _
                             "From ItemGroup IG " & _
                             "LEFT JOIN ItemCategory IC ON IG.ItemCategory = IC.Code  " & _
                             "LEFT JOIN ItemType IT ON IC.ItemType = IT.Code  "
    Dim mHelpItemCategoryQry$ = "Select 'o' As Tick, IC.Code, IC.Description As [Item Category], IT.Name as [Item Type] " & _
                                "From ItemCategory IC " & _
                                "LEFT JOIN ItemType IT ON IC.ItemType = IT.Code  "
    Dim mHelpItemTypeQry$ = "SELECT 'o' AS Tick, Code, Name  AS ItemType  FROM ItemType ORDER BY Name "
    Dim mHelpItemReportingGroupQry$ = "Select 'o' As Tick, Code, Description As [Group Name] From ItemReportingGroup "

    Dim mHelpJobOrderNoQry$ = " Select 'o' As Tick, H.DocID AS Code, H.V_Type + '-' + H.ManualRefNo AS [Order No] , P.Description AS Process, H.V_Date AS OrderDate  " & _
                                " FROM JobOrder H " & _
                                " LEFT JOIN Process P ON P.NCat = H.Process  " & _
                                " WHERE H.Div_Code ='" & AgL.PubDivCode & " ' And H.Site_Code = '" & AgL.PubSiteCode & "' "
    Dim mHelpJobReceiveNoQry$ = " Select 'o' As Tick, H.DocID AS Code, H.V_Type + '-' + H.ManualRefNo AS [Receive No] , P.Description AS Process, H.V_Date AS ReceiveDate  " & _
                                " FROM JobIssRec H " & _
                                " LEFT JOIN Process P ON P.NCat = H.Process  " & _
                                " WHERE H.Div_Code ='" & AgL.PubDivCode & " ' And H.Site_Code = '" & AgL.PubSiteCode & "' "
    Dim mHelpJobInvoiceNoQry$ = " Select 'o' As Tick, H.DocID AS Code, H.V_Type + '-' + H.ManualRefNo AS [Receive No] , P.Description AS Process, H.V_Date AS InvoiceDate  " & _
                            " FROM JobInvoice H " & _
                            " LEFT JOIN Process P ON P.NCat = H.Process  " & _
                            " WHERE H.Div_Code ='" & AgL.PubDivCode & " ' And H.Site_Code = '" & AgL.PubSiteCode & "' "
    Dim mHelpProdOrderQry$ = " Select 'o' As Tick, P.DocID AS Code, P.V_Type + '-' + P.ManualRefNo AS [Manual No] , P.V_Date AS OrderDate " & _
                             " FROM ProdOrder P " & _
                             " WHERE P.Div_Code ='" & AgL.PubDivCode & " ' And P.Site_Code = '" & AgL.PubSiteCode & "' "

    Dim mHelpMaterialPlanNo$ = " Select 'o' As Tick, S.DocID, S.V_Type +'-'+ S.ManualRefNo AS [Production Plan No.], " & _
                            " S.V_Date AS [Date] " & _
                            " FROM MaterialPlan S  " & _
                            " LEFT JOIN Voucher_Type Vt ON Vt.V_Type=S.V_Type " & _
                            " WHERE S.Div_Code = '" & AgL.PubDivCode & "'  AND S.Site_Code = '" & AgL.PubSiteCode & "' "

    Dim mHelpJobWorkerQry$ = " Select 'o' As Tick,  S.SubCode AS Code,S.Name AS Worker,C.CityName AS City " &
                         " FROM SubGroup S " &
                         " LEFT JOIN City C ON C.CityCode = S.CityCode  " &
                         " WHERE CharIndex('|' + '" & AgL.PubDivCode & "' + '|', IFNull(S.DivisionList,'|' + '" & AgL.PubDivCode & "' + '|')) > 0 " &
                         " AND S.Site_Code = '" & AgL.PubSiteCode & "' " &
                         " And IFNull(S.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "'"

    Dim mHelpGodownQry$ = "Select 'o' As Tick, Code,Description AS Godown FROM Godown WHERE Site_Code = '" & AgL.PubSiteCode & "' "

    Dim mHelpProcessQry$ = " Select 'o' As Tick,  NCat AS Code, Description FROM Process "
    Dim mHelpBillingOnQry$ = "Select 'Qty' As Code, 'Qty' As Name UNION ALL Select 'Measure' As Code, 'Measure' As Name UNION ALL Select 'Perimeter' As Code, 'Perimeter' As Name "
    Dim mHelpLotQry$ = "Select 'o' As Tick, S.LotNo AS Code, S.LotNo FROM Stock S WHERE IFNull(S.LotNo,'') <> '' GROUP BY S.LotNo "

    Dim mHelpUnitQry$ = " Select 'o' As Tick,  'Qty' AS Code, 'Qty' AS Name " &
                    " UNION ALL  " &
                    " Select 'o' As Tick,  'Measure' AS Code, 'Measure' AS Name " &
                    " UNION ALL  " &
                    " Select 'o' As Tick, 'Amount' AS Code, 'Amount' AS Name "

    Dim mHelpLotDimensionQry$ = " Select 'o' As Tick,  '" & AgTemplate.ClsMain.FGetDimension1Caption() & "' AS Code, '" & AgTemplate.ClsMain.FGetDimension1Caption() & "' AS Name " &
                            " UNION ALL  " &
                            " Select 'o' As Tick,  '" & AgTemplate.ClsMain.FGetDimension2Caption() & "' AS Code, '" & AgTemplate.ClsMain.FGetDimension2Caption() & "' AS Name " &
                            " UNION ALL  " &
                            " Select 'o' As Tick, 'Lot No' AS Code, 'Lot No' AS Name"
    Dim mHelpDimension1$ = "SELECT 'o' AS Tick, Code, Description AS Dimension1 FROM Dimension1 ORDER BY Description"
    Dim mHelpDimension2$ = "SELECT 'o' AS Tick, Code, Description AS Dimension2 FROM Dimension2 ORDER BY Description"


#End Region

    Dim DsRep As DataSet = Nothing, DsRep1 As DataSet = Nothing, DsRep2 As DataSet = Nothing
    Dim mQry$ = "", RepName$ = "", RepTitle$ = "", OrderByStr$ = ""

#Region "Initializing Grid"
    Public Sub Ini_Grid()
        Try
            Dim I As Integer = 0
            Select Case GRepFormName
                Case JobOrderReport
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubEndDate)
                    ReportFrm.CreateHelpGrid("Report Type", "Report Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, "Select 'Detail' as Code, 'Detail' as Name Union All Select 'Month Wise Summary' as Code, 'Month Wise Summary' as Name Union All Select 'Job Worker Wise Summary' as Code, 'Job Worker Wise Summary' as Name Union All Select 'Item Wise Detail' as Code, 'Item Wise Detail' as Name Union All Select 'Item Wise Summary' as Code, 'Item Wise Summary' as Name", "Detail", , , , , False)
                    ReportFrm.CreateHelpGrid("Unit", "Unit", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpUnitQry, "Qty", 250, 250, 120)
                    ReportFrm.CreateHelpGrid("Show", "Show", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpLotDimensionQry, "None", 250, 250, 120)
                    ReportFrm.CreateHelpGrid("Voucher Type", "Voucher Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, FGetVoucher_TypeQry("JobOrder"))
                    ReportFrm.CreateHelpGrid("Process", "Process", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpProcessQry, "All", , , , , False)
                    ReportFrm.CreateHelpGrid("JobWorker", "Job Worker", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpJobWorkerQry, , 550, 600, 400)
                    ReportFrm.CreateHelpGrid("Order No", "Order No", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpJobOrderNoQry, , , 500, 150)
                    ReportFrm.CreateHelpGrid("Item", "Item", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemQry, , , 600, 270)
                    ReportFrm.CreateHelpGrid("Item Group", "Item Group", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemGroupQry, , , 530)
                    ReportFrm.CreateHelpGrid("Item Category", "Item Category", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemCategoryQry, , , 430)
                    ReportFrm.CreateHelpGrid("Item Type", "Item Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemTypeQry)
                    ReportFrm.CreateHelpGrid("Item Reporting Group", "Item Reporting Group", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemReportingGroupQry, , , 500, 360)
                    ReportFrm.CreateHelpGrid("Lot No", "Lot No", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpLotQry, "All")
                    ReportFrm.CreateHelpGrid(AgTemplate.ClsMain.FGetDimension1Caption(), AgTemplate.ClsMain.FGetDimension1Caption(), ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpDimension1)
                    ReportFrm.CreateHelpGrid(AgTemplate.ClsMain.FGetDimension2Caption(), AgTemplate.ClsMain.FGetDimension2Caption(), ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpDimension2)
                    ReportFrm.CreateHelpGrid("Process in New Page ?", "Process in New Page ?", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, "Select 'Yes' as Code, 'Yes' as Name Union All Select 'No' as Code, 'No' as Name", "No", , , , , False)

                Case JobReceiveReport
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubEndDate)
                    ReportFrm.CreateHelpGrid("Report Type", "Report Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, "Select 'Detail' as Code, 'Detail' as Name Union All Select 'Month Wise Summary' as Code, 'Month Wise Summary' as Name Union All Select 'Job Worker Wise Summary' as Code, 'Job Worker Wise Summary' as Name Union All Select 'Item Wise Detail' as Code, 'Item Wise Detail' as Name Union All Select 'Item Wise Summary' as Code, 'Item Wise Summary' as Name", "Detail", , , , , False)
                    ReportFrm.CreateHelpGrid("Unit", "Unit", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpUnitQry, "Qty", 250, 250, 120)
                    ReportFrm.CreateHelpGrid("Show", "Show", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpLotDimensionQry, "None", 250, 250, 120)
                    ReportFrm.CreateHelpGrid("Voucher Type", "Voucher Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, FGetVoucher_TypeQry("JobIssRec"))
                    ReportFrm.CreateHelpGrid("Process", "Process", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpProcessQry)
                    ReportFrm.CreateHelpGrid("JobWorker", "Job Worker", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpJobWorkerQry, , 550, 600, 400)
                    ReportFrm.CreateHelpGrid("Order No", "Order No", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpJobOrderNoQry, , , 500, 150)
                    ReportFrm.CreateHelpGrid("Receive No", "Receive No", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpJobReceiveNoQry, , , 500)
                    ReportFrm.CreateHelpGrid("Godown", "Godown", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpGodownQry)
                    ReportFrm.CreateHelpGrid("Item", "Item", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemQry, , , 600, 270)
                    ReportFrm.CreateHelpGrid("Item Group", "Item Group", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemGroupQry, , , 530)
                    ReportFrm.CreateHelpGrid("Item Category", "Item Category", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemCategoryQry, , , 430)
                    ReportFrm.CreateHelpGrid("Item Type", "Item Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemTypeQry)
                    ReportFrm.CreateHelpGrid("Item Reporting Group", "Item Reporting Group", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemReportingGroupQry, , , 500, 360)
                    ReportFrm.CreateHelpGrid("Lot No", "Lot No", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpLotQry, "All")
                    ReportFrm.CreateHelpGrid(AgTemplate.ClsMain.FGetDimension1Caption(), AgTemplate.ClsMain.FGetDimension1Caption(), ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpDimension1)
                    ReportFrm.CreateHelpGrid(AgTemplate.ClsMain.FGetDimension2Caption(), AgTemplate.ClsMain.FGetDimension2Caption(), ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpDimension2)
                    ReportFrm.CreateHelpGrid("Process in New Page ?", "Process in New Page ?", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, "Select 'Yes' as Code, 'Yes' as Name Union All Select 'No' as Code, 'No' as Name", "No", , , , , False)

                Case JobInvoiceReport
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubEndDate)
                    ReportFrm.CreateHelpGrid("Report Type", "Report Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, "Select 'Detail' as Code, 'Detail' as Name Union All Select 'Month Wise Summary' as Code, 'Month Wise Summary' as Name Union All Select 'Job Worker Wise Summary' as Code, 'Job Worker Wise Summary' as Name Union All Select 'Item Wise Detail' as Code, 'Item Wise Detail' as Name Union All Select 'Item Wise Summary' as Code, 'Item Wise Summary' as Name", "Detail", , , , , False)
                    ReportFrm.CreateHelpGrid("Unit", "Unit", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, "Select 'Amount' as Code, 'Amount' AS Name Union All Select 'Qty' as Code, 'Qty' as Name Union All Select 'Measure' as Code, 'Measure' AS Name Union All Select 'Qty & Measure' as Code, 'Qty & Measure' AS Name Union All Select 'Qty & Amount' as Code, 'Qty & Amount' AS Name Union All Select 'Measure & Amount' as Code, 'Measure & Amount' AS Name", "Amount")
                    ReportFrm.CreateHelpGrid("Show", "Show", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpLotDimensionQry, "None", 250, 250, 120)
                    ReportFrm.CreateHelpGrid("Code", "Amount Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, FGetStructureFieldsQry(AgTemplate.ClsMain.Temp_NCat.JobInvoice), "Amount|Amount", , , , , False)
                    ReportFrm.CreateHelpGrid("Voucher Type", "Voucher Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, FGetVoucher_TypeQry("JobInvoice"))
                    ReportFrm.CreateHelpGrid("Process", "Process", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpProcessQry)
                    ReportFrm.CreateHelpGrid("JobWorker", "Job Worker", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpJobWorkerQry, , 550, 650, 400)
                    ReportFrm.CreateHelpGrid("Order No", "Order No", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpJobOrderNoQry, , , 550, 150)
                    ReportFrm.CreateHelpGrid("Receive No", "Receive No", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpJobReceiveNoQry, , , 550, 150)
                    ReportFrm.CreateHelpGrid("Invoice No", "Invoice No", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpJobInvoiceNoQry, , , 550, 150)
                    ReportFrm.CreateHelpGrid("Item", "Item", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemQry, , , 600, 270)
                    ReportFrm.CreateHelpGrid("Item Group", "Item Group", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemGroupQry, , , 530)
                    ReportFrm.CreateHelpGrid("Item Category", "Item Category", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemCategoryQry, , , 430)
                    ReportFrm.CreateHelpGrid("Item Type", "Item Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemTypeQry)
                    ReportFrm.CreateHelpGrid("Item Reporting Group", "Item Reporting Group", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemReportingGroupQry, , , 500, 360)
                    ReportFrm.CreateHelpGrid("Lot No", "Lot No", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpLotQry, "All")
                    ReportFrm.CreateHelpGrid(AgTemplate.ClsMain.FGetDimension1Caption(), AgTemplate.ClsMain.FGetDimension1Caption(), ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpDimension1)
                    ReportFrm.CreateHelpGrid(AgTemplate.ClsMain.FGetDimension2Caption(), AgTemplate.ClsMain.FGetDimension2Caption(), ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpDimension2)
                    ReportFrm.CreateHelpGrid("Process in New Page ?", "Process in New Page ?", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, "Select 'Yes' as Code, 'Yes' as Name Union All Select 'No' as Code, 'No' as Name", "No", , , , , False)

                Case ProcessOrderStatus
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubEndDate)
                    ReportFrm.CreateHelpGrid("Status On", "Status On", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubLoginDate)
                    ReportFrm.CreateHelpGrid("Report Type", "Report Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, "Select 'Summary' as Code, 'Summary' as Name Union All Select 'Detail' as Code, 'Detail' as Name Union All Select 'Job Worker Wise Order Status' as Code, 'Job Worker Wise Order Status' as Name Union All Select 'Item Wise Order Status' as Code, 'Item Wise Order Status' as Name ", "Summary", , , 250, , False)
                    ReportFrm.CreateHelpGrid("Report For", "Report For", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, " SELECT 'All' AS Code, 'All' AS Name UNION ALL  SELECT 'Pending To Receive' AS Code, 'Pending To Receive' AS Name UNION ALL  SELECT 'Over Due' AS Code, 'Over Due' AS Name UNION ALL  SELECT 'Over Due And Balance' AS Code, 'Over Due And Balance' AS Name ", , , , , , False)
                    ReportFrm.CreateHelpGrid("Unit", "Unit", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpUnitQry, "Qty", 250, 250, 120)
                    ReportFrm.CreateHelpGrid("Show", "Show", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpLotDimensionQry, "None", 250, 250, 120)
                    ReportFrm.CreateHelpGrid("Voucher Type", "Voucher Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, FGetVoucher_TypeQry("JobOrder"))
                    ReportFrm.CreateHelpGrid("Process", "Process", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpProcessQry)
                    ReportFrm.CreateHelpGrid("JobWorker", "Job Worker", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpJobWorkerQry, , 550, 700, 350)
                    ReportFrm.CreateHelpGrid("Order No", "Order No", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpJobOrderNoQry, , , 500, 150)
                    ReportFrm.CreateHelpGrid("Item", "Item", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemQry, , , 600, 270)
                    ReportFrm.CreateHelpGrid("Item Group", "Item Group", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemGroupQry, , , 530)
                    ReportFrm.CreateHelpGrid("Item Category", "Item Category", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemCategoryQry, , , 430)
                    ReportFrm.CreateHelpGrid("Item Type", "Item Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemTypeQry)
                    ReportFrm.CreateHelpGrid("Item Reporting Group", "Item Reporting Group", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemReportingGroupQry, , , 500, 360)
                    ReportFrm.CreateHelpGrid("Lot No", "Lot No", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpLotQry, "All")
                    ReportFrm.CreateHelpGrid(AgTemplate.ClsMain.FGetDimension1Caption(), AgTemplate.ClsMain.FGetDimension1Caption(), ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpDimension1)
                    ReportFrm.CreateHelpGrid(AgTemplate.ClsMain.FGetDimension2Caption(), AgTemplate.ClsMain.FGetDimension2Caption(), ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpDimension2)
                    ReportFrm.CreateHelpGrid("Process in New Page ?", "Process in New Page ?", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, "Select 'Yes' as Code, 'Yes' as Name Union All Select 'No' as Code, 'No' as Name", "No", , , , , False)

                Case JobReceiveStatus
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubEndDate)
                    ReportFrm.CreateHelpGrid("Status On", "Status On", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubLoginDate)
                    ReportFrm.CreateHelpGrid("Report Type", "Report Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, "Select 'Summary' as Code, 'Summary' as Name Union All Select 'Detail' as Code, 'Detail' as Name Union All Select 'Job Worker Wise Receive Status' as Code, 'Job Worker Wise Receive Status' as Name Union All Select 'Item Wise Receive Status' as Code, 'Item Wise Receive Status' as Name ", "Summary", , , 250, , False)
                    ReportFrm.CreateHelpGrid("Report For", "Report For", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, " SELECT 'All' AS Code, 'All' AS Name UNION ALL  SELECT 'Pending To Invoice' AS Code, 'Pending To Invoice' AS Name ")
                    ReportFrm.CreateHelpGrid("Unit", "Unit", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, "Select 'Amount' as Code, 'Amount' AS Name Union All Select 'Qty' as Code, 'Qty' as Name Union All Select 'Measure' as Code, 'Measure' AS Name Union All Select 'Qty & Measure' as Code, 'Qty & Measure' AS Name Union All Select 'Qty & Amount' as Code, 'Qty & Amount' AS Name Union All Select 'Measure & Amount' as Code, 'Measure & Amount' AS Name", "Amount")
                    ReportFrm.CreateHelpGrid("Code", "Amount Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, FGetStructureFieldsQry(AgTemplate.ClsMain.Temp_NCat.JobInvoice), "Amount|Amount", , , , , False)
                    ReportFrm.CreateHelpGrid("Process", "Process", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpProcessQry)
                    ReportFrm.CreateHelpGrid("JobWorker", "Job Worker", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpJobWorkerQry, , 550, 700, 350)
                    ReportFrm.CreateHelpGrid("Receive No", "Receive No", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpJobReceiveNoQry, , , , 100)
                    ReportFrm.CreateHelpGrid("Item", "Item", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemQry, , , 600, 270)
                    ReportFrm.CreateHelpGrid("Item Group", "Item Group", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemGroupQry, , , 530)
                    ReportFrm.CreateHelpGrid("Item Category", "Item Category", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemCategoryQry, , , 430)
                    ReportFrm.CreateHelpGrid("Item Type", "Item Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemTypeQry)
                    ReportFrm.CreateHelpGrid("Item Reporting Group", "Item Reporting Group", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemReportingGroupQry, , , 500, 360)
                    ReportFrm.CreateHelpGrid("Process in New Page ?", "Process in New Page ?", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, "Select 'Yes' as Code, 'Yes' as Name Union All Select 'No' as Code, 'No' as Name", "No", , , , , False)

                Case ProcessBalanceReport
                    ReportFrm.CreateHelpGrid("From Date", "From Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", "")
                    ReportFrm.CreateHelpGrid("To Date", "To Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubLoginDate)
                    ReportFrm.CreateHelpGrid("Balance On Date", "Balance On Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubLoginDate)
                    ReportFrm.CreateHelpGrid("Report Type", "Report Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, "Select 'Detail' as Code, 'Detail' as Name Union All Select 'Worker Wise Summary' as Code, 'Worker Wise Summary' AS Name Union All Select 'Item Wise Summary' as Code, 'Item Wise Summary' AS Name Union All Select 'Worker Wise Outstanding Report' as Code, 'Worker Wise Outstanding Report' AS Name Union All Select 'With Barcode' as Code, 'With Barcode' AS Name ", "Detail", , , 300)
                    ReportFrm.CreateHelpGrid("Unit", "Unit", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpUnitQry, "Qty", 250, 250, 120)
                    ReportFrm.CreateHelpGrid("Show", "Show", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpLotDimensionQry, "None", 250, 250, 120)
                    ReportFrm.CreateHelpGrid("Voucher Type", "Voucher Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, FGetVoucher_TypeQry("JobOrder"))
                    ReportFrm.CreateHelpGrid("Code", "Amount Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, FGetStructureFieldsQry(AgTemplate.ClsMain.Temp_NCat.JobOrder), "Amount|Amount", , , , , False)
                    ReportFrm.CreateHelpGrid("Process", "Process", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpProcessQry)
                    ReportFrm.CreateHelpGrid("JobWorker", "Job Worker", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpJobWorkerQry, , 550, 700, 350)
                    ReportFrm.CreateHelpGrid("Order No", "Order No", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpJobOrderNoQry, , , 500, 150)
                    ReportFrm.CreateHelpGrid("Item", "Item", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemQry, , , 600, 270)
                    ReportFrm.CreateHelpGrid("Item Group", "Item Group", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemGroupQry, , , 530)
                    ReportFrm.CreateHelpGrid("Item Category", "Item Category", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemCategoryQry, , , 430)
                    ReportFrm.CreateHelpGrid("Item Type", "Item Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemTypeQry)
                    ReportFrm.CreateHelpGrid("Item Reporting Group", "Item Reporting Group", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemReportingGroupQry, , , 500, 360)
                    ReportFrm.CreateHelpGrid("Lot No", "Lot No", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpLotQry, "All")
                    ReportFrm.CreateHelpGrid(AgTemplate.ClsMain.FGetDimension1Caption(), AgTemplate.ClsMain.FGetDimension1Caption(), ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpDimension1)
                    ReportFrm.CreateHelpGrid(AgTemplate.ClsMain.FGetDimension2Caption(), AgTemplate.ClsMain.FGetDimension2Caption(), ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpDimension2)
                    ReportFrm.CreateHelpGrid("Process in New Page ?", "Process in New Page ?", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, "Select 'Yes' as Code, 'Yes' as Name Union All Select 'No' as Code, 'No' as Name", "No", , , , , False)

                Case MaterialIssueFromJobOrderReport
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubEndDate)
                    ReportFrm.CreateHelpGrid("Voucher Type", "Voucher Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, FGetVoucher_TypeQry("JobOrder"))
                    ReportFrm.CreateHelpGrid("Process", "Process", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpProcessQry, "All", , , , , False)
                    ReportFrm.CreateHelpGrid("JobWorker", "Job Worker", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpJobWorkerQry, , 550, 600, 400)
                    ReportFrm.CreateHelpGrid("Order No", "Order No", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpJobOrderNoQry, , , 500, 150)
                    ReportFrm.CreateHelpGrid("Item", "Item", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemQry, , , 600, 270)
                    ReportFrm.CreateHelpGrid("Item Group", "Item Group", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemGroupQry, , , 530)
                    ReportFrm.CreateHelpGrid("Item Category", "Item Category", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemCategoryQry, , , 430)
                    ReportFrm.CreateHelpGrid("Item Type", "Item Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemTypeQry)
                    ReportFrm.CreateHelpGrid("Item Reporting Group", "Item Reporting Group", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemReportingGroupQry, , , 500, 360)
                    ReportFrm.CreateHelpGrid("Lot No", "Lot No", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpLotQry, "All")
                    ReportFrm.CreateHelpGrid(AgTemplate.ClsMain.FGetDimension1Caption(), AgTemplate.ClsMain.FGetDimension1Caption(), ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpDimension1)
                    ReportFrm.CreateHelpGrid(AgTemplate.ClsMain.FGetDimension2Caption(), AgTemplate.ClsMain.FGetDimension2Caption(), ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpDimension2)
                    ReportFrm.CreateHelpGrid("Process in New Page ?", "Process in New Page ?", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, "Select 'Yes' as Code, 'Yes' as Name Union All Select 'No' as Code, 'No' as Name", "No", , , , , False)

                Case JobQCReport
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubEndDate)
                    ReportFrm.CreateHelpGrid("Process", "Process", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpProcessQry)
                    ReportFrm.CreateHelpGrid("JobWorker", "Job Worker", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpJobWorkerQry, , 550, 600, 400)
                    ReportFrm.CreateHelpGrid("Receive No", "Receive No", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpJobReceiveNoQry, , , 500)
                    ReportFrm.CreateHelpGrid("Item", "Item", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemQry, , , 600, 270)
                    ReportFrm.CreateHelpGrid("Item Group", "Item Group", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemGroupQry, , , 530)
                    ReportFrm.CreateHelpGrid("Item Category", "Item Category", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemCategoryQry, , , 430)
                    ReportFrm.CreateHelpGrid("Item Type", "Item Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemTypeQry)
                    ReportFrm.CreateHelpGrid("Item Reporting Group", "Item Reporting Group", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemReportingGroupQry, , , 500, 360)
                    ReportFrm.CreateHelpGrid(AgTemplate.ClsMain.FGetDimension1Caption(), AgTemplate.ClsMain.FGetDimension1Caption(), ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpDimension1)
                    ReportFrm.CreateHelpGrid(AgTemplate.ClsMain.FGetDimension2Caption(), AgTemplate.ClsMain.FGetDimension2Caption(), ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpDimension2)

                Case PeriodicJobOrderStatus
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubEndDate)
                    ReportFrm.CreateHelpGrid("Report Type", "Report Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, "Select 'Job Worker Wise' as Code, 'Job Worker Wise' as Name Union All Select 'Item Wise' as Code, 'Item Wise' as Name", "Job Worker Wise")
                    ReportFrm.CreateHelpGrid("Process", "Process", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpProcessQry)
                    ReportFrm.CreateHelpGrid("Voucher Type", "Voucher Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, FGetVoucher_TypeQry("JobOrder"))
                    ReportFrm.CreateHelpGrid("JobWorker", "Job Worker", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpJobWorkerQry, , 550, 600, 400)
                    ReportFrm.CreateHelpGrid("Item", "Item", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemQry, , , 600, 270)

                Case PaymentCalculation
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubEndDate)
                    ReportFrm.CreateHelpGrid("Process", "Process", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpProcessQry)
                    ReportFrm.CreateHelpGrid("Voucher Type", "Voucher Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, FGetVoucher_TypeQry("JobInvoice"))
                    ReportFrm.CreateHelpGrid("JobWorker", "Job Worker", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpJobWorkerQry, , 550, 600, 400)
                    ReportFrm.CreateHelpGrid("Item", "Item", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemQry, , , 600, 270)
                    ReportFrm.CreateHelpGrid("Process in New Page ?", "Process in New Page ?", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, "Select 'Yes' as Code, 'Yes' as Name Union All Select 'No' as Code, 'No' as Name", "No", , , , , False)

                Case PaymentAdvise
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubEndDate)
                    ReportFrm.CreateHelpGrid("Process", "Process", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpProcessQry)
                    ReportFrm.CreateHelpGrid("JobWorker", "Job Worker", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpJobWorkerQry, , 550, 600, 400)
                    ReportFrm.CreateHelpGrid("Process in New Page ?", "Process in New Page ?", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, "Select 'Yes' as Code, 'Yes' as Name Union All Select 'No' as Code, 'No' as Name", "No", , , , , False)

            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
#End Region

    Private Function FGetVoucher_TypeQry(ByVal TableName As String) As String
        FGetVoucher_TypeQry = " SELECT Distinct 'o' As Tick, H.V_Type AS Code, Vt.Description AS [Voucher Type] " &
                                " FROM " & TableName & " H  " &
                                " LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type "
    End Function

    Private Function FGetMainVoucher_TypeQry(ByVal HeaderTable As String, ByVal LineTableJoinStr As String) As String
        FGetMainVoucher_TypeQry = "Select DISTINCT 'o' As Tick, H.V_Type , Vt.Description " &
            " FROM " & HeaderTable & "  L " & LineTableJoinStr & " " &
            " LEFT JOIN Voucher_Type Vt ON Vt.V_Type = H.V_Type " &
            " WHERE IFNull(H.V_Type,'') <> '' " &
            " ORDER BY Vt.Description "
    End Function

    Private Function FGetStructureFieldsQry(ByVal NCat As String) As String
        FGetStructureFieldsQry = "Select 'Amount' as Code, 'Amount' as Description " &
                                 "Union All " &
                                 "SELECT L.LineAmtField AS Code, C.Description AS [Amount Type]  " &
                                 "FROM StructureDetail L " &
                                 "LEFT JOIN Charges C ON L.Charges = C.Code  " &
                                 "WHERE L.Code = (SELECT Structure FROM VoucherCat WHERE nCat = '" & NCat & "')"
    End Function

    Private Sub ObjRepFormGlobal_ProcessReport() Handles ReportFrm.ProcessReport
        Select Case mGRepFormName
            Case JobOrderReport
                ProcJobOrderReport()

            Case JobReceiveReport
                ProcJobReceiveReport()

            Case JobInvoiceReport
                ProcJobInvoiceReport()

            Case ProcessOrderStatus
                ProcProcessOrderStatusReport()

            Case JobReceiveStatus
                ProcJobReceiveStatusReport()

            Case ProcessBalanceReport
                ProcJobBalanceReport()

            Case MaterialIssueFromJobOrderReport
                ProcMaterialIssueFromJobOrderReport()

            Case JobQCReport
                ProcJobQCReport()

            Case PeriodicJobOrderStatus
                ProcPeriodicJobOrderStatus()

            Case PaymentCalculation
                ProcPaymentCalculation()

            Case PaymentAdvise
                ProcPaymentAdvise()

        End Select
    End Sub

    Public Sub New(ByVal mReportFrm As ReportLayout.FrmReportLayout)
        ReportFrm = mReportFrm
    End Sub

#Region "Job Order Report"
    Private Sub ProcJobOrderReport()
        Try
            Dim IsMultiProcess As Integer = 0
            Dim IsProcessinNewPage As Integer = 0
            Dim mProcessName As String = ""
            Dim mCondStr$ = ""
            Dim strGrpFld As String = "''", strGrpFldHead As String = "''"

            Dim mShowForValue1$ = "''", mShowForValue2$ = "''", mShowForValue3$ = "''"
            Dim mShowForHead1$ = "''", mShowForHead2$ = "''", mShowForHead3$ = "''"

            Dim IsUnitQty As Integer = 0
            Dim IsUnitMeasure As Integer = 0
            Dim IsUnitAmount As Integer = 0
            Dim IsGroupOnDimension1 As Integer = 0
            Dim IsGroupOnDimension2 As Integer = 0
            Dim IsGroupOnLotNo As Integer = 0

            Dim Unit1Head = "''", Unit1 As String = "''", Unit1DecimalPlace As String = "''", OrdUnit1 As String = "''", Unit1Disp As String = "''"
            Dim Unit2Head = "''", Unit2 As String = "''", Unit2DecimalPlace As String = "''", OrdUnit2 As String = "''", Unit2Disp As String = "''"
            Dim Unit3Head = "''", Unit3 As String = "''", Unit3DecimalPlace As String = "''", OrdUnit3 As String = "''", Unit3Disp As String = "''"


            If ReportFrm.FGetText(6).ToString.Contains(",") Or ReportFrm.FGetText(6) = "All" Then
                mProcessName = "Process"
                IsMultiProcess = 1
            Else
                mProcessName = ReportFrm.FGetText(6)
                IsMultiProcess = 0
            End If

            If ReportFrm.FGetText(17) = "Yes" Then
                IsProcessinNewPage = 1
            Else
                IsProcessinNewPage = 0
            End If

            If ReportFrm.FGetText(2) = "Detail" Then
                RepTitle = mProcessName & " Order Report"
                OrderByStr = "Order By P.Sr, H.V_Date, H.V_No "
                RepName = "Trade_JobOrderReport"
            ElseIf ReportFrm.FGetText(2) = "Item Wise Detail" Then
                RepTitle = mProcessName & " Order Report(" & ReportFrm.FGetText(2) & ")"
                OrderByStr = " Order By P.Sr, H.V_Date, H.V_No, I.Description "
                RepName = "Trade_JobOrderReportDetail"
            ElseIf ReportFrm.FGetText(2) = "Month Wise Summary" Then
                RepTitle = mProcessName & " Order Report(" & ReportFrm.FGetText(2) & ")"
                OrderByStr = " Order By P.Sr, H.V_Date "
                strGrpFld = "Substring(convert(nvarchar,H.V_Date,11),0,6)" : strGrpFldHead = "Month"
                RepName = "Trade_JobOrderReportSummary"
            ElseIf ReportFrm.FGetText(2) = "Item Wise Summary" Then
                RepTitle = mProcessName & " Order Report(" & ReportFrm.FGetText(2) & ")"
                OrderByStr = " Order By P.Sr, H.V_Date "
                strGrpFld = "I.Description" : strGrpFldHead = "Item"
                RepName = "Trade_JobOrderReportSummary"
            ElseIf ReportFrm.FGetText(2) = "Job Worker Wise Summary" Then
                RepTitle = mProcessName & " Order Report(" & ReportFrm.FGetText(2) & ")"
                OrderByStr = " Order By P.Sr, H.V_Date "
                strGrpFld = "Sg.Name" : strGrpFldHead = "Job Worker"
                RepName = "Trade_JobOrderReportSummary"
            End If


            If ReportFrm.FGetText(3) IsNot Nothing Then
                If ReportFrm.FGetText(3).ToString.Contains("Qty") = True Then IsUnitQty = 1
                If ReportFrm.FGetText(3).ToString.Contains("Measure") = True Then IsUnitMeasure = 1
                If ReportFrm.FGetText(3).ToString.Contains("Amount") = True Then IsUnitAmount = 1
            End If

            If IsUnitQty + IsUnitMeasure + IsUnitAmount = 3 Then
                'RepName = RepName + "_With3Unit"
                'Unit1 = "I.Unit" : Unit1Head = "'Qty'" : Unit1DecimalPlace = "U.DecimalPlaces" : RecUnit1 = "L.Qty"
                'Unit2 = "I.MeasureUnit" : Unit2Head = "'Measure'" : Unit2DecimalPlace = "UM.DecimalPlaces" : RecUnit2 = "L.TotalMeasure"
                'Unit3 = "E.DefaultCurrency" : Unit3Head = "'Currency'" : Unit3DecimalPlace = "2" : RecUnit3 = "L.Amount"
                RepName = "Report_UnderConstruction"
            ElseIf IsUnitQty + IsUnitMeasure + IsUnitAmount = 2 Then
                RepName = RepName + "_With2Unit"
                If IsUnitQty = 1 And IsUnitMeasure = 1 Then
                    Unit1 = "L.Unit" : Unit1Head = "'Qty'" : Unit1DecimalPlace = "U.DecimalPlaces" : OrdUnit1 = "L.Qty"
                    Unit2 = "L.MeasureUnit" : Unit2Head = "'Measure'" : Unit2DecimalPlace = "UM.DecimalPlaces" : OrdUnit2 = "L.TotalMeasure"
                ElseIf IsUnitQty = 1 And IsUnitAmount = 1 Then
                    Unit1 = "L.Unit" : Unit1Disp = "'Unit'" : Unit1Head = "'Qty'" : Unit1DecimalPlace = "U.DecimalPlaces" : OrdUnit1 = "L.Qty"
                    Unit2 = "E.DefaultCurrency" : Unit2Disp = "'Currency'" : Unit2Head = "'Amount'" : Unit2DecimalPlace = "2" : OrdUnit2 = "L.Amount"
                ElseIf IsUnitMeasure = 1 And IsUnitAmount = 1 Then
                    Unit1 = "L.MeasureUnit" : Unit1Disp = "'Unit'" : Unit1Head = "'Measure'" : Unit1DecimalPlace = "UM.DecimalPlaces" : OrdUnit1 = "L.TotalMeasure"
                    Unit2 = "E.DefaultCurrency" : Unit2Disp = "'Currency'" : Unit2Head = "'Amount'" : Unit2DecimalPlace = "2" : OrdUnit2 = "L.Amount"
                End If
            ElseIf IsUnitQty + IsUnitMeasure + IsUnitAmount = 1 Then
                RepName = RepName + "_With1Unit"
                If IsUnitQty = 1 Then Unit1 = "L.Unit" : Unit1Head = "'Qty'" : Unit1DecimalPlace = "U.DecimalPlaces" : OrdUnit1 = "L.Qty" : Unit1Disp = "'Unit'"
                If IsUnitMeasure = 1 Then Unit1 = "L.MeasureUnit" : Unit1Head = "'Measure'" : Unit1DecimalPlace = "UM.DecimalPlaces" : OrdUnit1 = "L.TotalMeasure" : Unit1Disp = "'Unit'"
                If IsUnitAmount = 1 Then Unit1 = "E.DefaultCurrency" : Unit1Head = "'Currency'" : Unit1DecimalPlace = "2" : OrdUnit1 = "L.Amount" : Unit1Disp = "'Currency'"
            End If



            If ReportFrm.FGetCode(4) IsNot Nothing Then
                If ReportFrm.FGetCode(4).ToString.Contains(AgTemplate.ClsMain.FGetDimension1Caption) = True Then IsGroupOnDimension1 = 1
                If ReportFrm.FGetCode(4).ToString.Contains(AgTemplate.ClsMain.FGetDimension2Caption) = True Then IsGroupOnDimension2 = 1
                If ReportFrm.FGetCode(4).ToString.Contains("Lot No") = True Then IsGroupOnLotNo = 1
            End If

            If IsGroupOnDimension1 + IsGroupOnDimension2 + IsGroupOnLotNo = 3 Then
                RepName = RepName + "_With3Dimensions"
                mShowForValue1 = "L.LotNo" : mShowForHead1 = "'Lot No'"
                mShowForValue2 = "D1.Description" : mShowForHead2 = "'" & AgTemplate.ClsMain.FGetDimension1Caption() & "'"
                mShowForValue3 = "D2.Description" : mShowForHead3 = "'" & AgTemplate.ClsMain.FGetDimension2Caption() & "'"
            ElseIf IsGroupOnDimension1 + IsGroupOnDimension2 + IsGroupOnLotNo = 2 Then
                RepName = RepName + "_With2Dimensions"
                If IsGroupOnDimension1 = 1 And IsGroupOnDimension2 = 1 Then
                    mShowForValue1 = "D1.Description" : mShowForHead1 = "'" & AgTemplate.ClsMain.FGetDimension1Caption() & "'"
                    mShowForValue2 = "D2.Description" : mShowForHead2 = "'" & AgTemplate.ClsMain.FGetDimension2Caption() & "'"
                ElseIf IsGroupOnDimension1 = 1 And IsGroupOnLotNo = 1 Then
                    mShowForValue1 = "D1.Description" : mShowForHead1 = "'" & AgTemplate.ClsMain.FGetDimension1Caption() & "'"
                    mShowForValue2 = "L.LotNo" : mShowForHead2 = "'Lot No'"
                ElseIf IsGroupOnDimension2 = 1 And IsGroupOnLotNo = 1 Then
                    mShowForValue1 = "D2.Description" : mShowForHead1 = "'" & AgTemplate.ClsMain.FGetDimension2Caption() & "'"
                    mShowForValue2 = "L.LotNo" : mShowForHead2 = "'Lot No'"
                End If
            ElseIf IsGroupOnDimension1 + IsGroupOnDimension2 + IsGroupOnLotNo = 1 Then
                'RepName = RepName + "_With1Dimensions"
                RepName = "Report_UnderConstruction"
                If IsGroupOnDimension1 = 1 Then mShowForValue1 = "D1.Description" : mShowForHead1 = "'" & AgTemplate.ClsMain.FGetDimension1Caption() & "'"
                If IsGroupOnDimension2 = 1 Then mShowForValue1 = "D2.Description" : mShowForHead1 = "'" & AgTemplate.ClsMain.FGetDimension2Caption() & "'"
                If IsGroupOnLotNo = 1 Then mShowForValue1 = "L.LotNo" : mShowForHead1 = "'Lot No'"
            End If


            mCondStr = " Where 1 = 1 "
            mCondStr = mCondStr & " AND H.V_Date Between '" & ReportFrm.FGetText(0) & "' And '" & ReportFrm.FGetText(1) & "' "
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.V_Type", 5)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.Process", 6)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.JobWorker", 7)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.DocId", 8)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Item", 9)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemGroup", 10)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemCategory", 11)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemType", 12)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.LotNo", 14)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Dimension1", 15)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Dimension2", 16)

            If ReportFrm.FGetText(13) <> "" And ReportFrm.FGetText(13) <> "All" Then
                mQry = " Select '''' +  replace(ItemList,',',''',''')  + ''''  From ItemReportingGroup Where 1=1 " & ReportFrm.GetWhereCondition("Code", 13)
                mCondStr = mCondStr & " AND I.Code In (" & AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar & ")"
            End If

            mCondStr = mCondStr & " And H.Div_Code = '" & AgL.PubDivCode & "' "
            mCondStr = mCondStr & " And H.Site_Code = '" & AgL.PubSiteCode & "' "

            mQry = " SELECT H.DocId, H.Site_Code, H.V_Date, H.DueDate, H.ManualRefNo As ManualRefNo, H.Remarks, " &
                    " L.MeasurePerPcs,  L.TotalMeasure As TotalLineMeasure,  " &
                    " L.MeasureUnit, L.PerimeterPerPcs, L.TotalPerimeter As TotalLinePerimeter, L.LotNo, Vt.Description AS VDesc,  " &
                    " E.Caption_Dimension1, E.Caption_Dimension2, D1.Description AS D1Desc,D2.Description AS D2Desc," &
                    " IFNull(H.Remarks,'') + IFNull(L.Remark,'') As LineRemark, P.Description AS ProcessDesc, P.Sr AS ProcessSr, L.Amount, " &
                    " Sg.Name AS JobWorkerName, I.Description As ItemDesc, L.Item As ItemCode," &
                    " " & Unit1Head & " as Unit1Head, " & Unit1 & " AS Unit1, " & Unit1DecimalPlace & " as Unit1DecimalPlace, " & OrdUnit1 & " as OrdUnit1, " & Unit1Disp & " as Unit1Disp, " &
                    " " & Unit2Head & " as Unit2Head, " & Unit2 & " as Unit2, " & Unit2DecimalPlace & " as Unit2DecimalPlace, " & OrdUnit2 & " as OrdUnit2, " & Unit2Disp & " as Unit2Disp, " &
                    " " & Unit3Head & " as Unit3Head, " & Unit3 & " as Unit3, " & Unit3DecimalPlace & " as Unit3DecimalPlace, " & OrdUnit3 & " as OrdUnit3, " & Unit3Disp & " as Unit3Disp," &
                    " " & mShowForValue1 & " as mShowForValue1, " & mShowForHead1 & " as mShowForHead1, " &
                    " " & mShowForValue2 & " as mShowForValue2, " & mShowForHead2 & " as mShowForHead2, " &
                    " " & mShowForValue3 & " as mShowForValue3, " & mShowForHead3 & " as mShowForHead3, " &
                    " " & strGrpFld & "  AS GroupOn, '" & strGrpFldHead & "'  AS GroupHead, " &
                    " " & IsMultiProcess & " As IsMultiProcess, " & IsProcessinNewPage & " As IsProcessinNewPage " &
                    " FROM JobOrder H  " &
                    " LEFT JOIN JobOrderDetail L ON H.DocID = L.DocId " &
                    " LEFT JOIN SubGroup Sg ON H.JobWorker = Sg.SubCode " &
                    " LEFT JOIN Voucher_Type Vt On Vt.V_Type = H.V_Type " &
                    " LEFT JOIN Item I ON L.Item = I.Code " &
                    " LEFT JOIN Process P On P.NCat = H.Process " &
                    " LEFT JOIN Enviro E ON E.Site_Code = H.Site_Code AND E.Div_Code = H.Div_Code " &
                    " LEFT JOIN Dimension1 D1 ON D1.Code = L.Dimension1  " &
                    " LEFT JOIN Dimension2 D2 ON D2.Code = L.Dimension2 " &
                    " LEFT JOIN Unit U ON U.Code = L.Unit " &
                    " LEFT JOIN Unit UM ON UM.Code = L.MeasureUnit " & mCondStr & OrderByStr
            DsRep = AgL.FillData(mQry, AgL.GCn)

            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records to Print!")

            ReportFrm.PrintReport(DsRep, RepName, RepTitle, ReportPath)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub
#End Region

#Region "Job Receive Report"
    Private Sub ProcJobReceiveReport()
        Try
            Dim mCondStr$ = "", OrderByStr$ = ""
            Dim bIsvisibleLoss As Boolean = False
            Dim IsMultiProcess As Integer = 0
            Dim IsProcessinNewPage As Integer = 0
            Dim mProcessName As String = ""
            Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"

            Dim mShowForValue1$ = "''", mShowForValue2$ = "''", mShowForValue3$ = "''"
            Dim mShowForHead1$ = "''", mShowForHead2$ = "''", mShowForHead3$ = "''"

            Dim IsUnitQty As Integer = 0
            Dim IsUnitMeasure As Integer = 0
            Dim IsUnitAmount As Integer = 0
            Dim IsGroupOnDimension1 As Integer = 0
            Dim IsGroupOnDimension2 As Integer = 0
            Dim IsGroupOnLotNo As Integer = 0
            Dim Unit1Head = "''", Unit1 As String = "''", Unit1DecimalPlace As String = "''", RecUnit1 As String = "''", LossUnit1 As String = "''"
            Dim Unit2Head = "''", Unit2 As String = "''", Unit2DecimalPlace As String = "''", RecUnit2 As String = "''", LossUnit2 As String = "''"
            Dim Unit3Head = "''", Unit3 As String = "''", Unit3DecimalPlace As String = "''", RecUnit3 As String = "''", LossUnit3 As String = "''"

            If ReportFrm.FGetText(6).ToString.Contains(",") Or ReportFrm.FGetText(6) = "All" Then
                mProcessName = "Process"
                IsMultiProcess = 1
            Else
                mProcessName = ReportFrm.FGetText(6)
                IsMultiProcess = 0
            End If

            If ReportFrm.FGetText(19) = "Yes" Then
                IsProcessinNewPage = 1
            Else
                IsProcessinNewPage = 0
            End If


            If ReportFrm.FGetText(2) = "Detail" Then
                RepTitle = mProcessName & " Receive Report"
                OrderByStr = " Order By  H.V_Date, H.V_No "
                RepName = "Trade_JobReceiveReport"
            ElseIf ReportFrm.FGetText(2) = "Item Wise Detail" Then
                RepTitle = mProcessName & " Receive Report (" & ReportFrm.FGetText(2) & ")"
                OrderByStr = " Order By  H.V_Date, H.V_No, I.Description "
                RepName = "Trade_JobReceiveReportDetail"
            ElseIf ReportFrm.FGetText(2) = "Month Wise Summary" Then
                RepTitle = mProcessName & " Receive Report (" & ReportFrm.FGetText(2) & ")"
                OrderByStr = " Order By  H.V_Date "
                strGrpFld = "Substring(convert(nvarchar,H.V_Date,11),0,6)" : strGrpFldHead = "Month"
                RepName = "Trade_JobReceiveReportSummary"
            ElseIf ReportFrm.FGetText(2) = "Item Wise Summary" Then
                RepTitle = mProcessName & " Receive Report (" & ReportFrm.FGetText(2) & ")"
                OrderByStr = " Order By  H.V_Date "
                strGrpFld = "I.Description" : strGrpFldHead = "Item"
                RepName = "Trade_JobReceiveReportSummary"
            ElseIf ReportFrm.FGetText(2) = "Job Worker Wise Summary" Then
                RepTitle = mProcessName & " Receive Report (" & ReportFrm.FGetText(2) & ")"
                OrderByStr = " Order By  H.V_Date "
                strGrpFld = "Sg.Name" : strGrpFldHead = "Job Worker"
                RepName = "Trade_JobReceiveReportSummary"
            End If


            If ReportFrm.FGetText(3) IsNot Nothing Then
                If ReportFrm.FGetText(3).ToString.Contains("Qty") = True Then IsUnitQty = 1
                If ReportFrm.FGetText(3).ToString.Contains("Measure") = True Then IsUnitMeasure = 1
                If ReportFrm.FGetText(3).ToString.Contains("Amount") = True Then IsUnitAmount = 1
            End If

            If IsUnitQty + IsUnitMeasure + IsUnitAmount = 3 Then
                'RepName = RepName + "_With3Unit"
                'Unit1 = "I.Unit" : Unit1Head = "'Qty'" : Unit1DecimalPlace = "U.DecimalPlaces" : RecUnit1 = "L.Qty"
                'Unit2 = "I.MeasureUnit" : Unit2Head = "'Measure'" : Unit2DecimalPlace = "UM.DecimalPlaces" : RecUnit2 = "L.TotalMeasure"
                'Unit3 = "E.DefaultCurrency" : Unit3Head = "'Currency'" : Unit3DecimalPlace = "2" : RecUnit3 = "L.Amount"
                RepName = "Report_UnderConstruction"
            ElseIf IsUnitQty + IsUnitMeasure + IsUnitAmount = 2 Then
                RepName = RepName + "_With2Unit"
                If IsUnitQty = 1 And IsUnitMeasure = 1 Then
                    Unit1 = "L.Unit" : Unit1Head = "'Qty'" : Unit1DecimalPlace = "U.DecimalPlaces" : RecUnit1 = "L.Qty" : LossUnit1 = "L.LossQty"
                    Unit2 = "L.MeasureUnit" : Unit2Head = "'Measure'" : Unit2DecimalPlace = "UM.DecimalPlaces" : RecUnit2 = "L.TotalMeasure"
                ElseIf IsUnitQty = 1 And IsUnitAmount = 1 Then
                    Unit1 = "L.Unit" : Unit1Head = "'Qty'" : Unit1DecimalPlace = "U.DecimalPlaces" : RecUnit1 = "L.Qty" : LossUnit1 = "L.LossQty"
                    Unit2 = "E.DefaultCurrency" : Unit2Head = "'Currency'" : Unit2DecimalPlace = "2" : RecUnit2 = "L.Amount"
                ElseIf IsUnitMeasure = 1 And IsUnitAmount = 1 Then
                    Unit1 = "L.MeasureUnit" : Unit1Head = "'Measure'" : Unit1DecimalPlace = "UM.DecimalPlaces" : RecUnit1 = "L.TotalMeasure"
                    Unit2 = "E.DefaultCurrency" : Unit2Head = "'Currency'" : Unit2DecimalPlace = "2" : RecUnit2 = "L.Amount"
                End If
            ElseIf IsUnitQty + IsUnitMeasure + IsUnitAmount = 1 Then
                RepName = RepName + "_With1Unit"
                If IsUnitQty = 1 Then Unit1 = "L.Unit" : Unit1Head = "'Qty'" : Unit1DecimalPlace = "U.DecimalPlaces" : RecUnit1 = "L.Qty" : LossUnit1 = "L.LossQty"
                If IsUnitMeasure = 1 Then Unit1 = "L.MeasureUnit" : Unit1Head = "'Measure'" : Unit1DecimalPlace = "UM.DecimalPlaces" : RecUnit1 = "L.TotalMeasure"
                If IsUnitAmount = 1 Then Unit1 = "E.DefaultCurrency" : Unit1Head = "'Currency'" : Unit1DecimalPlace = "2" : RecUnit1 = "L.Amount"
            End If

            If ReportFrm.FGetCode(4) IsNot Nothing Then
                If ReportFrm.FGetCode(4).ToString.Contains(AgTemplate.ClsMain.FGetDimension1Caption) = True Then IsGroupOnDimension1 = 1
                If ReportFrm.FGetCode(4).ToString.Contains(AgTemplate.ClsMain.FGetDimension2Caption) = True Then IsGroupOnDimension2 = 1
                If ReportFrm.FGetCode(4).ToString.Contains("Lot No") = True Then IsGroupOnLotNo = 1
            End If

            If IsGroupOnDimension1 + IsGroupOnDimension2 + IsGroupOnLotNo = 3 Then
                RepName = RepName + "_With3Dimensions"
                mShowForValue1 = "L.LotNo" : mShowForHead1 = "'Lot No'"
                mShowForValue2 = "D1.Description" : mShowForHead2 = "'" & AgTemplate.ClsMain.FGetDimension1Caption() & "'"
                mShowForValue3 = "D2.Description" : mShowForHead3 = "'" & AgTemplate.ClsMain.FGetDimension2Caption() & "'"
            ElseIf IsGroupOnDimension1 + IsGroupOnDimension2 + IsGroupOnLotNo = 2 Then
                RepName = RepName + "_With2Dimensions"
                If IsGroupOnDimension1 = 1 And IsGroupOnDimension2 = 1 Then
                    mShowForValue1 = "D1.Description" : mShowForHead1 = "'" & AgTemplate.ClsMain.FGetDimension1Caption() & "'"
                    mShowForValue2 = "D2.Description" : mShowForHead2 = "'" & AgTemplate.ClsMain.FGetDimension2Caption() & "'"
                ElseIf IsGroupOnDimension1 = 1 And IsGroupOnLotNo = 1 Then
                    mShowForValue1 = "D1.Description" : mShowForHead1 = "'" & AgTemplate.ClsMain.FGetDimension1Caption() & "'"
                    mShowForValue2 = "L.LotNo" : mShowForHead2 = "'Lot No'"
                ElseIf IsGroupOnDimension2 = 1 And IsGroupOnLotNo = 1 Then
                    mShowForValue1 = "D2.Description" : mShowForHead1 = "'" & AgTemplate.ClsMain.FGetDimension2Caption() & "'"
                    mShowForValue2 = "L.LotNo" : mShowForHead2 = "'Lot No'"
                End If
            ElseIf IsGroupOnDimension1 + IsGroupOnDimension2 + IsGroupOnLotNo = 1 Then
                RepName = RepName + "_With1Dimensions"
                If IsGroupOnDimension1 = 1 Then mShowForValue1 = "D1.Description" : mShowForHead1 = "'" & AgTemplate.ClsMain.FGetDimension1Caption() & "'"
                If IsGroupOnDimension2 = 1 Then mShowForValue1 = "D2.Description" : mShowForHead1 = "'" & AgTemplate.ClsMain.FGetDimension2Caption() & "'"
                If IsGroupOnLotNo = 1 Then mShowForValue1 = "L.LotNo" : mShowForHead1 = "'Lot No'"
            End If

            mCondStr = " Where 1 = 1 "
            mCondStr = mCondStr & " AND H.V_Date Between '" & ReportFrm.FGetText(0) & "' And '" & ReportFrm.FGetText(1) & "' "

            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.V_Type", 5)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.Process", 6)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.JobWorker", 7)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.JobOrder", 8)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.DocId", 9)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.Godown", 10)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Item", 11)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemGroup", 12)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemCategory", 13)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemType", 14)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.LotNo", 16)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Dimension1", 17)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Dimension2", 18)

            If ReportFrm.FGetText(15) <> "" And ReportFrm.FGetText(15) <> "All" Then
                mQry = " Select '''' +  replace(ItemList,',',''',''')  + ''''  From ItemReportingGroup Where 1=1 " & ReportFrm.GetWhereCondition("Code", 15)
                mCondStr = mCondStr & " AND I.Code In (" & AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar & ")"
            End If

            mCondStr = mCondStr & " And H.Div_Code = '" & AgL.PubDivCode & "' "
            mCondStr = mCondStr & " And H.Site_Code = '" & AgL.PubSiteCode & "' "

            mQry = " SELECT H.DocId, H.Site_Code, H.V_Date, H.ManualRefNo As ManualRefNo, G.Description AS GodownDesc,  " &
                    " H.Remarks, P.Description AS ProcessDesc, P.Sr AS ProcessSr, " &
                    " L.Remark As LineRemark, Vt.Description AS VDesc, IFNull(L.LossQty,0) AS LossQty, " &
                    " Sg.Name AS JobWorkerName, I.Description As ItemDesc, L.Item As ItemCode," &
                    " " & Unit1Head & " as Unit1Head, " & Unit1 & " AS Unit1, " & Unit1DecimalPlace & " as Unit1DecimalPlace, " & RecUnit1 & " as RecUnit1, " &
                    " " & Unit2Head & " as Unit2Head, " & Unit2 & " as Unit2, " & Unit2DecimalPlace & " as Unit2DecimalPlace, " & RecUnit2 & " as RecUnit2, " &
                    " " & Unit3Head & " as Unit3Head, " & Unit3 & " as Unit3, " & Unit3DecimalPlace & " as Unit3DecimalPlace, " & RecUnit3 & " as RecUnit3, " &
                    " " & mShowForValue1 & " as mShowForValue1, " & mShowForHead1 & " as mShowForHead1, " &
                    " " & mShowForValue2 & " as mShowForValue2, " & mShowForHead2 & " as mShowForHead2, " &
                    " " & mShowForValue3 & " as mShowForValue3, " & mShowForHead3 & " as mShowForHead3, " &
                    " " & strGrpFld & "  AS GroupOn, '" & strGrpFldHead & "'  AS GroupHead, " &
                    " " & IsMultiProcess & " As IsMultiProcess, " & IsProcessinNewPage & " As IsProcessinNewPage " &
                    " FROM JobReceiveDetail L  " &
                    " LEFT JOIN JobIssRec H  ON H.DocId = L.DocId " &
                    " LEFT JOIN Voucher_Type Vt  On Vt.V_Type = H.V_Type " &
                    " LEFT JOIN SubGroup Sg  ON H.JobWorker = Sg.SubCode " &
                    " LEFT JOIN Godown G  ON H.Godown = G.Code " &
                    " LEFT JOIN Process P  On P.NCat = H.Process " &
                    " LEFT JOIN Item I  ON L.Item = I.Code " &
                    " LEFT JOIN Enviro E  ON E.Site_Code = H.Site_Code AND E.Div_Code = H.Div_Code " &
                    " LEFT JOIN Dimension1 D1  ON D1.Code = L.Dimension1  " &
                    " LEFT JOIN Dimension2 D2  ON D2.Code = L.Dimension2 " &
                    " LEFT JOIN Unit U ON U.Code = L.Unit " &
                    " LEFT JOIN Unit UM  ON UM.Code = L.MeasureUnit " & mCondStr & OrderByStr
            DsRep = AgL.FillData(mQry, AgL.GCn)

            If DsRep.Tables(0).Select("LossQty > 0 ").Length > 0 Then
                RepName = RepName + "_WithLoss"
            End If

            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records to Print!")

            ReportFrm.PrintReport(DsRep, RepName, RepTitle, ReportPath)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub
#End Region

#Region "Job Invoice Report"
    Private Sub ProcJobInvoiceReport()
        Dim mCondStr$ = ""
        Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"
        Dim strQtyFld As String = "''", strQtyFldHead = "''", strUnitFld As String = "''", strUnitFldDecimalPlace As String = "''"
        Dim strQtyFld2 As String = "''", strQtyFldHead2 = "''", strUnitFld2 As String = "''", strUnitFldDecimalPlace2 As String = "''"

        Dim IsGroupOnDimension1 As Integer = 0
        Dim IsGroupOnDimension2 As Integer = 0
        Dim IsGroupOnLotNo As Integer = 0
        Dim mShowForValue1$ = "''", mShowForValue2$ = "''", mShowForValue3$ = "''"
        Dim mShowForHead1$ = "''", mShowForHead2$ = "''", mShowForHead3$ = "''"

        Try
            If ReportFrm.FGetText(3) = "Qty & Measure" Then
                strQtyFld = "L.Qty"
                strQtyFldHead = "'Qty'"
                strUnitFld = "L.Unit"
                strUnitFldDecimalPlace = "U.DecimalPlaces"
                strQtyFld2 = "L.TotalMeasure"
                strQtyFldHead2 = "'Measure'"
                strUnitFld2 = "L.MeasureUnit"
                strUnitFldDecimalPlace2 = "MU.DecimalPlaces"

                If ReportFrm.FGetText(2) = "Item Wise Detail" Then
                    RepTitle = "Item Wise Job Invoice Report"
                    RepName = "Trade_JobInvoiceReport_ItemWiseDetail_QtyMeasure"
                ElseIf ReportFrm.FGetText(2) = "Job Worker Wise Summary" Then
                    RepTitle = "Job Invoice Report ( Party Wise Summary )"
                    strGrpFld = "SG.Name"
                    strGrpFldDesc = "SG.Name + ',' + IFNull(City.CityName,'')"
                    strGrpFldHead = "'Party Name'"
                    RepName = "Trade_JobInvoiceReport_Summary_QtyMeasure"
                ElseIf ReportFrm.FGetText(2) = "Month Wise Summary" Then
                    RepTitle = "Job Invoice Report (" & ReportFrm.FGetText(2) & ")"
                    strGrpFld = "Substring(convert(nvarchar,H.V_Date,11),0,6)"
                    strGrpFldDesc = "Replace(SubString(Convert(VARCHAR,H.v_Date,6),4,6),' ','-')"
                    strGrpFldHead = "'Month'"
                    RepName = "Trade_JobInvoiceReport_Summary_QtyMeasure"
                ElseIf ReportFrm.FGetText(2) = "Item Wise Summary" Then
                    RepTitle = "Job Invoice Report (" & ReportFrm.FGetText(2) & ")"
                    strGrpFld = "I.Description"
                    strGrpFldDesc = "I.Description"
                    strGrpFldHead = "'Item'"
                    RepName = "Trade_JobInvoiceReport_Summary_QtyMeasure"
                Else
                    RepTitle = "Job Invoice Report"
                    RepName = "Trade_JobInvoiceReport_QtyMeasure"
                End If
            ElseIf ReportFrm.FGetText(3) = "Qty & Amount" Then
                strQtyFld = "L.Qty"
                strQtyFldHead = "'Qty'"
                strUnitFld = "L.Unit"
                strUnitFldDecimalPlace = "U.DecimalPlaces"
                strQtyFld2 = "L." & Replace(ReportFrm.FGetCode(5), "'", "") & ""
                strQtyFldHead2 = "'" & ReportFrm.FGetText(5) & "'"
                strUnitFld2 = "'INR'"
                strUnitFldDecimalPlace2 = "2"

                If ReportFrm.FGetText(2) = "Item Wise Detail" Then
                    RepTitle = "Item Wise Job Invoice Report"
                    RepName = "Trade_JobInvoiceReport_ItemWiseDetail_QtyMeasure_WithRate"
                ElseIf ReportFrm.FGetText(2) = "Job Worker Wise Summary" Then
                    RepTitle = "Job Invoice Report ( Party Wise Summary )"
                    strGrpFld = "SG.Name"
                    strGrpFldDesc = "SG.Name + ',' + IFNull(City.CityName,'')"
                    strGrpFldHead = "'Party Name'"
                    RepName = "Trade_JobInvoiceReport_Summary_QtyMeasure"
                ElseIf ReportFrm.FGetText(2) = "Month Wise Summary" Then
                    RepTitle = "Job Invoice Report (" & ReportFrm.FGetText(2) & ")"
                    strGrpFld = "Substring(convert(nvarchar,H.V_Date,11),0,6)"
                    strGrpFldDesc = "Replace(SubString(Convert(VARCHAR,H.v_Date,6),4,6),' ','-')"
                    strGrpFldHead = "'Month'"
                    RepName = "Trade_JobInvoiceReport_Summary_QtyMeasure"
                ElseIf ReportFrm.FGetText(2) = "Item Wise Summary" Then
                    RepTitle = "Job Invoice Report (" & ReportFrm.FGetText(2) & ")"
                    strGrpFld = "I.Description"
                    strGrpFldDesc = "I.Description"
                    strGrpFldHead = "'Item'"
                    RepName = "Trade_JobInvoiceReport_Summary_QtyMeasure"
                Else
                    RepTitle = "Job Invoice Report"
                    RepName = "Trade_JobInvoiceReport_QtyMeasure"
                End If
            ElseIf ReportFrm.FGetText(3) = "Measure & Amount" Then
                strQtyFld = "L.TotalMeasure"
                strQtyFldHead = "'Measure'"
                strUnitFld = "L.MeasureUnit"
                strUnitFldDecimalPlace = "MU.DecimalPlaces"
                strQtyFld2 = "L." & Replace(ReportFrm.FGetCode(5), "'", "") & ""
                strQtyFldHead2 = "'" & ReportFrm.FGetText(5) & "'"
                strUnitFld2 = "'INR'"
                strUnitFldDecimalPlace2 = "2"

                If ReportFrm.FGetText(2) = "Item Wise Detail" Then
                    RepTitle = "Item Wise Job Invoice Report"
                    RepName = "Trade_JobInvoiceReport_ItemWiseDetail_QtyMeasure_WithRate"
                ElseIf ReportFrm.FGetText(2) = "Job Worker Wise Summary" Then
                    RepTitle = "Job Invoice Report ( Party Wise Summary )"
                    strGrpFld = "SG.Name"
                    strGrpFldDesc = "SG.Name + ',' + IFNull(City.CityName,'')"
                    strGrpFldHead = "'Party Name'"
                    RepName = "Trade_JobInvoiceReport_Summary_QtyMeasure"
                ElseIf ReportFrm.FGetText(2) = "Month Wise Summary" Then
                    RepTitle = "Job Invoice Report (" & ReportFrm.FGetText(2) & ")"
                    strGrpFld = "Substring(convert(nvarchar,H.V_Date,11),0,6)"
                    strGrpFldDesc = "Replace(SubString(Convert(VARCHAR,H.v_Date,6),4,6),' ','-')"
                    strGrpFldHead = "'Month'"
                    RepName = "Trade_JobInvoiceReport_Summary_QtyMeasure"
                ElseIf ReportFrm.FGetText(2) = "Item Wise Summary" Then
                    RepTitle = "Job Invoice Report (" & ReportFrm.FGetText(2) & ")"
                    strGrpFld = "I.Description"
                    strGrpFldDesc = "I.Description"
                    strGrpFldHead = "'Item'"
                    RepName = "Trade_JobInvoiceReport_Summary_QtyMeasure"
                Else
                    RepTitle = "Job Invoice Report"
                    RepName = "Trade_JobInvoiceReport_QtyMeasure"
                End If
            ElseIf ReportFrm.FGetText(3) = "Measure" Then
                strQtyFld = "L.TotalMeasure"
                strQtyFldHead = "'Measure'"
                strUnitFld = "L.MeasureUnit"
                strUnitFldDecimalPlace = "MU.DecimalPlaces"

                If ReportFrm.FGetText(2) = "Item Wise Detail" Then
                    RepTitle = "Item Wise Job Invoice Report"
                    RepName = "Trade_JobInvoiceReport_ItemWiseDetail"
                ElseIf ReportFrm.FGetText(2) = "Job Worker Wise Summary" Then
                    RepTitle = "Job Invoice Report ( Party Wise Summary )"
                    strGrpFld = "SG.Name"
                    strGrpFldDesc = "SG.Name + ',' + IFNull(City.CityName,'')"
                    strGrpFldHead = "'Party Name'"
                    RepName = "Trade_JobInvoiceReport_Summary"
                ElseIf ReportFrm.FGetText(2) = "Month Wise Summary" Then
                    RepTitle = "Job Invoice Report (" & ReportFrm.FGetText(2) & ")"
                    strGrpFld = "Substring(convert(nvarchar,H.V_Date,11),0,6)"
                    strGrpFldDesc = "Replace(SubString(Convert(VARCHAR,H.v_Date,6),4,6),' ','-')"
                    strGrpFldHead = "'Month'"
                    RepName = "Trade_JobInvoiceReport_Summary"
                ElseIf ReportFrm.FGetText(2) = "Item Wise Summary" Then
                    RepTitle = "Job Invoice Report (" & ReportFrm.FGetText(2) & ")"
                    strGrpFld = "I.Description"
                    strGrpFldDesc = "I.Description"
                    strGrpFldHead = "'Item'"
                    RepName = "Trade_JobInvoiceReport_Summary"
                Else
                    RepTitle = "Job Invoice Report"
                    RepName = "Trade_JobInvoiceReport"
                End If
            ElseIf ReportFrm.FGetText(3) = "Qty" Then
                strQtyFld = "L.Qty"
                strQtyFldHead = "'Qty'"
                strUnitFld = "L.Unit"
                strUnitFldDecimalPlace = "U.DecimalPlaces"

                If ReportFrm.FGetText(2) = "Item Wise Detail" Then
                    RepTitle = "Item Wise Job Invoice Report"
                    RepName = "Trade_JobInvoiceReport_ItemWiseDetail"
                ElseIf ReportFrm.FGetText(2) = "Job Worker Wise Summary" Then
                    RepTitle = "Job Invoice Report ( Party Wise Summary )"
                    strGrpFld = "SG.Name"
                    strGrpFldDesc = "SG.Name + ',' + IFNull(City.CityName,'')"
                    strGrpFldHead = "'Party Name'"
                    RepName = "Trade_JobInvoiceReport_Summary"
                ElseIf ReportFrm.FGetText(2) = "Month Wise Summary" Then
                    RepTitle = "Job Invoice Report (" & ReportFrm.FGetText(2) & ")"
                    strGrpFld = "Substring(convert(nvarchar,H.V_Date,11),0,6)"
                    strGrpFldDesc = "Replace(SubString(Convert(VARCHAR,H.v_Date,6),4,6),' ','-')"
                    strGrpFldHead = "'Month'"
                    RepName = "Trade_JobInvoiceReport_Summary"
                ElseIf ReportFrm.FGetText(2) = "Item Wise Summary" Then
                    RepTitle = "Job Invoice Report (" & ReportFrm.FGetText(2) & ")"
                    strGrpFld = "I.Description"
                    strGrpFldDesc = "I.Description"
                    strGrpFldHead = "'Item'"
                    RepName = "Trade_JobInvoiceReport_Summary"
                Else
                    RepTitle = "Job Invoice Report"
                    RepName = "Trade_JobInvoiceReport"
                End If
            Else
                strQtyFld = "L." & Replace(ReportFrm.FGetCode(5), "'", "") & ""
                strQtyFldHead = "'" & ReportFrm.FGetText(5) & "'"
                strUnitFld = "'INR'"
                strUnitFldDecimalPlace = "2"

                If ReportFrm.FGetText(2) = "Item Wise Detail" Then
                    RepTitle = "Item Wise Job Invoice Report"
                    RepName = "Trade_JobInvoiceReport_ItemWiseDetail_WithRate"
                ElseIf ReportFrm.FGetText(2) = "Job Worker Wise Summary" Then
                    RepTitle = "Job Invoice Report ( Party Wise Summary )"
                    strGrpFld = "SG.Name"
                    strGrpFldDesc = "SG.Name + ',' + IFNull(City.CityName,'')"
                    strGrpFldHead = "'Party Name'"
                    RepName = "Trade_JobInvoiceReport_Summary"
                ElseIf ReportFrm.FGetText(2) = "Month Wise Summary" Then
                    RepTitle = "Job Invoice Report (" & ReportFrm.FGetText(2) & ")"
                    strGrpFld = "Substring(convert(nvarchar,H.V_Date,11),0,6)"
                    strGrpFldDesc = "Replace(SubString(Convert(VARCHAR,H.v_Date,6),4,6),' ','-')"
                    strGrpFldHead = "'Month'"
                    RepName = "Trade_JobInvoiceReport_Summary"
                ElseIf ReportFrm.FGetText(2) = "Item Wise Summary" Then
                    RepTitle = "Job Invoice Report (" & ReportFrm.FGetText(2) & ")"
                    strGrpFld = "I.Description"
                    strGrpFldDesc = "I.Description"
                    strGrpFldHead = "'Item'"
                    RepName = "Trade_JobInvoiceReport_Summary"
                Else
                    RepTitle = "Job Invoice Report"
                    RepName = "Trade_JobInvoiceReport"
                End If
            End If



            If ReportFrm.FGetCode(4) IsNot Nothing Then
                If ReportFrm.FGetCode(4).ToString.Contains(AgTemplate.ClsMain.FGetDimension1Caption) = True Then IsGroupOnDimension1 = 1
                If ReportFrm.FGetCode(4).ToString.Contains(AgTemplate.ClsMain.FGetDimension2Caption) = True Then IsGroupOnDimension2 = 1
                If ReportFrm.FGetCode(4).ToString.Contains("Lot No") = True Then IsGroupOnLotNo = 1
            End If

            If IsGroupOnDimension1 + IsGroupOnDimension2 + IsGroupOnLotNo = 3 Then
                RepName = RepName + "_With3Dimensions"
                mShowForValue1 = "L.LotNo" : mShowForHead1 = "'Lot No'"
                mShowForValue2 = "D1.Description" : mShowForHead2 = "'" & AgTemplate.ClsMain.FGetDimension1Caption() & "'"
                mShowForValue3 = "D2.Description" : mShowForHead3 = "'" & AgTemplate.ClsMain.FGetDimension2Caption() & "'"
            ElseIf IsGroupOnDimension1 + IsGroupOnDimension2 + IsGroupOnLotNo = 2 Then
                RepName = RepName + "_With2Dimensions"
                If IsGroupOnDimension1 = 1 And IsGroupOnDimension2 = 1 Then
                    mShowForValue1 = "D1.Description" : mShowForHead1 = "'" & AgTemplate.ClsMain.FGetDimension1Caption() & "'"
                    mShowForValue2 = "D2.Description" : mShowForHead2 = "'" & AgTemplate.ClsMain.FGetDimension2Caption() & "'"
                ElseIf IsGroupOnDimension1 = 1 And IsGroupOnLotNo = 1 Then
                    mShowForValue1 = "D1.Description" : mShowForHead1 = "'" & AgTemplate.ClsMain.FGetDimension1Caption() & "'"
                    mShowForValue2 = "L.LotNo" : mShowForHead2 = "'Lot No'"
                ElseIf IsGroupOnDimension2 = 1 And IsGroupOnLotNo = 1 Then
                    mShowForValue1 = "D2.Description" : mShowForHead1 = "'" & AgTemplate.ClsMain.FGetDimension2Caption() & "'"
                    mShowForValue2 = "L.LotNo" : mShowForHead2 = "'Lot No'"
                End If
            ElseIf IsGroupOnDimension1 + IsGroupOnDimension2 + IsGroupOnLotNo = 1 Then
                RepName = RepName + "_With1Dimensions"
                If IsGroupOnDimension1 = 1 Then mShowForValue1 = "D1.Description" : mShowForHead1 = "'" & AgTemplate.ClsMain.FGetDimension1Caption() & "'"
                If IsGroupOnDimension2 = 1 Then mShowForValue1 = "D2.Description" : mShowForHead1 = "'" & AgTemplate.ClsMain.FGetDimension2Caption() & "'"
                If IsGroupOnLotNo = 1 Then mShowForValue1 = "L.LotNo" : mShowForHead1 = "'Lot No'"
            End If

            mCondStr = mCondStr & " And H.V_Date Between " & AgL.Chk_Text(ReportFrm.FGetText(0)) & " And " & AgL.Chk_Text(ReportFrm.FGetText(1)) & " "
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.V_Type", 6)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.Process", 7)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.JobWorker", 8)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.JobOrder", 9)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.JobReceive", 10)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.JobInvoice", 11)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Item", 12)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemGroup", 13)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemCategory", 14)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemType ", 15)

            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.LotNo", 17)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Dimension1", 18)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Dimension2", 19)

            If ReportFrm.FGetText(16) <> "" And ReportFrm.FGetText(16) <> "All" Then
                mQry = " Select '''' +  replace(ItemList,',',''',''')  + ''''  From ItemReportingGroup Where 1=1 " & ReportFrm.GetWhereCondition("Code", 16)
                mCondStr = mCondStr & " AND I.Code In (" & AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar & ")"
            End If

            mCondStr = mCondStr & " And H.Site_Code = '" & AgL.PubSiteCode & "' "
            mCondStr = mCondStr & " And H.Div_Code= '" & AgL.PubDivCode & "' "

            mQry = " SELECT H.DocID, H.V_Type, H.V_Date, H.ManualrefNo, SG.DispName + (Case When City.CityName Is Not Null then ', ' +  City.CityName Else '' End) AS JobWorkerName, " &
                    " L.Sr, L.JobReceive, L.Item, L.Qty, L.Unit, U.DecimalPlaces AS QtyDecimalplace, MU.DecimalPlaces AS MeasureDecimalplace, L.MeasurePerPcs , L.MeasureUnit, L.TotalMeasure,  " &
                    " " & strGrpFld & " as GrpField, " & strGrpFldDesc & " as GrpFieldDesc, " & strGrpFldHead & " as GrpFieldHead, " &
                    " " & strQtyFld & " as PrnQtyField, " & strQtyFldHead & " as PrnQtyFieldHead, " & strUnitFld & " as PrnUnitField, " & strUnitFldDecimalPlace & " as PrnDecimalPlaces, " &
                    " " & strQtyFld2 & " as PrnQtyField2, " & strQtyFldHead2 & " as PrnQtyFieldHead2, " & strUnitFld2 & " as PrnUnitField2, " & strUnitFldDecimalPlace2 & " as PrnDecimalPlaces2, " &
                    " " & mShowForValue1 & " as mShowForValue1, " & mShowForHead1 & " as mShowForHead1, " &
                    " " & mShowForValue2 & " as mShowForValue2, " & mShowForHead2 & " as mShowForHead2, " &
                    " " & mShowForValue3 & " as mShowForValue3, " & mShowForHead3 & " as mShowForHead3, " &
                    " L.Rate , L.JobReceiveSr, L." & Replace(ReportFrm.FGetCode(5), "'", "") & " as Amount, '" & ReportFrm.FGetText(5) & "' as AmountTitle, IFNull(L." & Replace(ReportFrm.FGetCode(5), "'", "") & ",0)/L.Qty AS NetAmtRate, I.Description AS ItemDesc,  " &
                    " Vt.Description AS VoucherTypeDesc, PC.V_Type + '- ' + PC.ManualrefNo AS ManualrefNo, H.Remarks as H_Remarks, L.Remark as LineRemarks " &
                    " FROM JobInvoice H  " &
                    " LEFT JOIN SubGroup SG  ON SG.SubCode = H.JobWorker " &
                    " LEFT JOIN City  On SG.CityCode = City.CityCode " &
                    " LEFT JOIN JobInvoiceDetail L  ON L.DocId = H.DocID  " &
                    " LEFT JOIN JobOrder SO  ON L.JobOrder = SO.DocID  " &
                    " LEFT JOIN Item I  ON I.Code = L.Item   " &
                    " LEFT JOIN Voucher_Type Vt  ON Vt.V_Type = H.V_Type " &
                    " LEFT JOIN JobIssRec PC  ON PC.DocID = L.JobReceive " &
                    " LEFT JOIN Enviro E  ON E.Site_Code = H.Site_Code AND E.Div_Code = H.Div_Code " &
                    " LEFT JOIN Dimension1 D1  ON D1.Code = L.Dimension1  " &
                    " LEFT JOIN Dimension2 D2  ON D2.Code = L.Dimension2 " &
                    " LEFT JOIN Unit U  ON U.Code = L.Unit  " &
                    " LEFT JOIN Unit MU  ON MU.Code = L.MeasureUnit  " &
                    " LEFT JOIN Unit PU  ON PU.Code = " & strUnitFld & "  " &
                    " WHERE 1=1 " & mCondStr & " Order By H.V_Date "

            DsRep = AgL.FillData(mQry, AgL.GCn)

            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records to Print!")

            ReportFrm.PrintReport(DsRep, RepName, RepTitle, ReportPath)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub
#End Region

#Region "Process Order Status Report"
    Private Sub ProcProcessOrderStatusReport()
        Dim mCondStr$ = ""
        Dim mCondStr1$ = ""
        Dim IsMultiProcess As Integer = 0
        Dim IsProcessinNewPage As Integer = 0
        Dim mProcessName As String
        Dim bIsvisibleLoss As Boolean = False

        Dim mCondStrMain$ = ""
        Dim GroupOn As String = "''", GroupOnHead As String = "''"

        Dim IsUnitQty As Integer = 0
        Dim IsUnitMeasure As Integer = 0
        Dim IsUnitAmount As Integer = 0

        Dim IsGroupOnDimension1 As Integer = 0
        Dim IsGroupOnDimension2 As Integer = 0
        Dim IsGroupOnLotNo As Integer = 0

        Dim TotalOrdUnit1 As String = "''", TotalReceiveUnit1 = "''", TotalLossUnit1 = "''", TotalBalUnit1 As String = "''", Unit1Desc As String = "''"
        Dim TotalOrdUnit2 As String = "''", TotalReceiveUnit2 = "''", TotalLossUnit2 = "''", TotalBalUnit2 As String = "''", Unit2Desc As String = "''"

        Dim OrdUnit1 As String = "''", CancelUnit1 = "''", AmdUnit1 As String = "''", ReceiveUnit1 As String = "''", LossUnit1 As String = "''"
        Dim OrdUnit2 As String = "''", CancelUnit2 = "''", AmdUnit2 As String = "''", ReceiveUnit2 As String = "''", LossUnit2 As String = "''"

        Dim Unit1Head = "''", Unit1 As String = "''", Unit1DecimalPlace As String = "''"
        Dim Unit2Head = "''", Unit2 As String = "''", Unit2DecimalPlace As String = "''"

        Dim mShowForValue1$ = "''", mShowForValue2$ = "''", mShowForValue3$ = "''"
        Dim mShowForHead1$ = "''", mShowForHead2$ = "''", mShowForHead3$ = "''"

        mCondStr1 = mCondStr1 & ReportFrm.GetWhereCondition("H.Process", 8)
        mCondStr1 = mCondStr1 & ReportFrm.GetWhereCondition("H.JobWorker", 9)
        mCondStr1 = mCondStr1 & ReportFrm.GetWhereCondition("L.JobOrder ", 10)
        mCondStr1 = mCondStr1 & ReportFrm.GetWhereCondition("L.Item", 11)
        mCondStr1 = mCondStr1 & ReportFrm.GetWhereCondition("I.ItemGroup", 12)
        mCondStr1 = mCondStr1 & ReportFrm.GetWhereCondition("I.ItemCategory", 13)
        mCondStr1 = mCondStr1 & ReportFrm.GetWhereCondition("I.ItemType ", 14)
        mCondStr1 = mCondStr1 & ReportFrm.GetWhereCondition("L.LotNo", 16)
        mCondStr1 = mCondStr1 & ReportFrm.GetWhereCondition("L.Dimension1", 17)
        mCondStr1 = mCondStr1 & ReportFrm.GetWhereCondition("L.Dimension1", 18)

        If ReportFrm.FGetText(15) <> "" And ReportFrm.FGetText(15) <> "All" Then
            mQry = " Select '''' +  replace(ItemList,',',''',''')  + ''''  From ItemReportingGroup Where 1=1 " & ReportFrm.GetWhereCondition("Code", 15)
            mCondStr = mCondStr & " AND I.Code In (" & AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar & ")"
        End If

        mCondStr1 = mCondStr1 & " And H.Site_Code = '" & AgL.PubSiteCode & "' "
        mCondStr1 = mCondStr1 & " And H.Div_Code= '" & AgL.PubDivCode & "' "

        mCondStrMain = mCondStrMain & ReportFrm.GetWhereCondition("H.Process", 8)
        mCondStrMain = mCondStrMain & ReportFrm.GetWhereCondition("H.JobWorker", 9)
        mCondStrMain = mCondStrMain & " And H.Site_Code = '" & AgL.PubSiteCode & "'"
        mCondStrMain = mCondStrMain & " And H.Div_Code= '" & AgL.PubDivCode & "' "

        Dim mQryJobReceive$ = " SELECT L.JobOrder, L.JObOrderSr , " &
                    " Max(H.ManualRefNo) AS ReceiveNo, Max(H.JobWorkerDocNo) AS JobWorkerDocNo, Max(H.V_Date) AS ReceiveDate, Sum(L.Qty) AS ReceiveQty, IFNull(Sum(L.LossQty),0) AS LossQty, Sum(L.TotalMeasure) AS ReceiveMeasure " &
                    " FROM JobReceiveDetail L  " &
                    " LEFT JOIN JobIssRec H  ON H.DocID = L.DocId  " &
                    " LEFT JOIN Item I  ON I.Code = L.Item  " &
                    " WHERE IFNull( L.JobOrder,'') <> '' " &
                    " AND H.V_Date <= '" & ReportFrm.FGetText(2) & "' " & mCondStr1 &
                    " Group BY L.JobOrder, L.JObOrderSr, H.DocId "

        Dim mQryJobReceiveSummury$ = " SELECT L.JobOrder, L.JObOrderSr ,  sum(L.Qty) AS TotalReceiveQty, IFNull(sum(L.LossQty),0) AS TotalLossQty, sum(L.TotalMeasure) AS TotalReceiveMeasure, sum(L.Amount) AS TotalReceiveAmount, " &
                    " max(H.V_Date) AS MaxRecDate " &
                    " FROM JobReceiveDetail L  " &
                    " LEFT JOIN JobIssRec H  ON H.DocID = L.DocId  " &
                    " LEFT JOIN Item I  ON I.Code = L.Item  " &
                    " WHERE IFNull( L.JobOrder,'') <> '' " &
                    " AND H.V_Date <= '" & ReportFrm.FGetText(2) & "' " & mCondStr1 & " " &
                    " GROUP BY L.JobOrder, L.JObOrderSr "

        Dim mQryJobOrder$ = " SELECT L.JobOrder, L.JObOrderSr, sum(L.Qty) AS BalOrdQty, sum(L.TotalMeasure) AS BalOrdMeasure, sum(L.Amount) AS BalOrdAmount, " &
                            " Sum(Case When IFNull(L.T_Nature,0) = " & AgTemplate.ClsMain.T_Nature.NA & " Then L.Qty Else 0 End) as OrdQty, " &
                            " Sum(Case When IFNull(L.T_Nature,0) = " & AgTemplate.ClsMain.T_Nature.Cancellation & " Then L.Qty Else 0 End) as CanQty, " &
                            " Sum(Case When IFNull(L.T_Nature,0) = " & AgTemplate.ClsMain.T_Nature.Amendment & " Then L.Qty Else 0 End) as AmdQty, " &
                            " Sum(Case When IFNull(L.T_Nature,0) = " & AgTemplate.ClsMain.T_Nature.NA & " Then L.TotalMeasure Else 0 End) as OrdMeasure, " &
                            " Sum(Case When IFNull(L.T_Nature,0) = " & AgTemplate.ClsMain.T_Nature.Cancellation & " Then L.TotalMeasure Else 0 End) as CanMeasure, " &
                            " Sum(Case When IFNull(L.T_Nature,0) = " & AgTemplate.ClsMain.T_Nature.Amendment & " Then L.TotalMeasure Else 0 End) as AmdMeasure, " &
                            " Sum(Case When IFNull(L.T_Nature,0) = " & AgTemplate.ClsMain.T_Nature.NA & " Then L.Amount Else 0 End) as OrdAmount, " &
                            " Sum(Case When IFNull(L.T_Nature,0) = " & AgTemplate.ClsMain.T_Nature.Cancellation & " Then L.Amount Else 0 End) as CanAmount, " &
                            " Sum(Case When IFNull(L.T_Nature,0) = " & AgTemplate.ClsMain.T_Nature.Amendment & " Then L.Amount Else 0 End) as AmdAmount " &
                            " FROM JobOrderDetail L   " &
                            " LEFT JOIN JobOrder H  ON H.DocId = L.DocId " &
                            " LEFT JOIN Item I  ON I.Code = L.Item  " &
                            " WHERE IFNull(L.JobOrder,'') <> ''  " &
                            " AND H.V_Date <= '" & ReportFrm.FGetText(2) & "' " & mCondStr1 & " " &
                            " GROUP BY L.JobOrder, L.JObOrderSr "

        If ReportFrm.FGetText(8).ToString.Contains(",") Or ReportFrm.FGetText(8) = "All" Then
            mProcessName = "Process"
            IsMultiProcess = 1
        Else
            mProcessName = ReportFrm.FGetText(8)
            IsMultiProcess = 0
        End If

        If ReportFrm.FGetText(19) = "Yes" Then
            IsProcessinNewPage = 1
        Else
            IsProcessinNewPage = 0
        End If

        Try
            If ReportFrm.FGetText(3) = "Detail" Then
                RepTitle = mProcessName & " Order Status Report"
                RepName = "Production_JobOrderStatusReport_Detail"
            ElseIf ReportFrm.FGetText(3) = "Job Worker Wise Order Status" Then
                RepTitle = "Job Worker Wise " & mProcessName & " Order Status"
                RepName = "Production_JobOrderStatus_Summary"
                GroupOn = "SG.DispName" : GroupOnHead = "'Job Worker'"
            ElseIf ReportFrm.FGetText(3) = "Item Wise Order Status" Then
                RepTitle = "Item Wise " & mProcessName & " Order Status"
                RepName = "Production_JobOrderStatus_Summary"
                GroupOn = "I.Description" : GroupOnHead = "'Item'"
            Else
                RepTitle = mProcessName & " Order Status Report"
                RepName = "Production_JobOrderStatusReport"
            End If


            If ReportFrm.FGetText(5) IsNot Nothing Then
                If ReportFrm.FGetText(5).ToString.Contains("Qty") = True Then IsUnitQty = 1
                If ReportFrm.FGetText(5).ToString.Contains("Measure") = True Then IsUnitMeasure = 1
                If ReportFrm.FGetText(5).ToString.Contains("Amount") = True Then IsUnitAmount = 1
            End If

            If IsUnitQty + IsUnitMeasure + IsUnitAmount = 3 Then
                'RepName = RepName + "_With3Unit"
                'Unit1 = "I.Unit" : Unit1Head = "'Qty'" : Unit1DecimalPlace = "U.DecimalPlaces" : InvoiceUnit1 = "L.Qty" : Unit1Type = "'Unit'"
                'Unit2 = "I.MeasureUnit" : Unit2Head = "'Measure'" : Unit2DecimalPlace = "UM.DecimalPlaces" : InvoiceUnit2 = "L.TotalMeasure" : Unit1Type = "'Unit'"
                'Unit3 = "C.Description" : Unit3Head = "'" & Replace(ReportFrm.FGetCode(9), "'", "") & "'" : Unit3DecimalPlace = "2" : InvoiceUnit3 = "L." & Replace(ReportFrm.FGetCode(9), "'", "") & "" : Unit3Type = "'Currency'"
                RepName = "Report_UnderConstruction"
            ElseIf IsUnitQty + IsUnitMeasure + IsUnitAmount = 2 Then
                RepName = RepName + "_With2Unit"
                If IsUnitQty = 1 And IsUnitMeasure = 1 Then
                    Unit1Desc = "'Unit'" : Unit1Head = "'Qty'" : Unit1 = "L.Unit" : Unit1DecimalPlace = "U.DecimalPlaces"
                    TotalOrdUnit1 = "IFNull(VPO.BalOrdQty,0)" : TotalReceiveUnit1 = "IFNull(VPCS.TotalReceiveQty,0)" : TotalLossUnit1 = "IFNull(VPCS.TotalLossQty,0)" : TotalBalUnit1 = "IFNull(VPO.BalOrdQty,0) - IFNull(VPCS.TotalReceiveQty,0) - IFNull(VPCS.TotalLossQty,0)"
                    OrdUnit1 = "IFNull(VPO.OrdQty,0)" : CancelUnit1 = "IFNull(VPO.CanQty,0)" : AmdUnit1 = "IFNull(VPO.AmdQty,0)" : ReceiveUnit1 = "IFNull(VPC.ChallanQty,0)" : LossUnit1 = "IFNull(VPC.LossQty,0)"

                    Unit2Desc = "'Unit'" : Unit2Head = "'Measure'" : Unit2 = "L.MeasureUnit" : Unit2DecimalPlace = "MU.DecimalPlaces"
                    TotalOrdUnit2 = "IFNull(VPO.BalOrdQty,0)*IFNull(I.Measure,0)" : TotalReceiveUnit2 = "IFNull(VPCS.TotalReceiveQty,0)*IFNull(I.Measure,0)" : TotalLossUnit2 = "IFNull(VPCS.TotalLossQty,0)*IFNull(I.Measure,0)" : TotalBalUnit2 = "(IFNull(VPO.BalOrdQty,0) - IFNull(VPCS.TotalReceiveQty,0) - IFNull(VPCS.TotalLossQty,0))*IFNull(I.Measure,0)"
                    OrdUnit2 = "IFNull(VPO.OrdQty,0)*IFNull(I.Measure,0)" : CancelUnit2 = "IFNull(VPO.CanQty,0)*IFNull(I.Measure,0)" : AmdUnit2 = "IFNull(VPO.AmdQty,0)*IFNull(I.Measure,0)" : ReceiveUnit2 = "IFNull(VPC.ChallanQty,0)*IFNull(I.Measure,0)" : LossUnit2 = "IFNull(VPC.LossQty,0)*IFNull(I.Measure,0)"

                ElseIf IsUnitQty = 1 And IsUnitAmount = 1 Then

                    Unit1Desc = "'Unit'" : Unit1Head = "'Qty'" : Unit1 = "L.Unit" : Unit1DecimalPlace = "U.DecimalPlaces"
                    TotalOrdUnit1 = "IFNull(VPO.BalOrdAmount,0)" : TotalReceiveUnit1 = "IFNull(VPCS.TotalReceiveAmount,0)" : TotalLossUnit1 = "IFNull(VPCS.TotalLossQty,0)" : TotalBalUnit1 = "IFNull(VPO.BalOrdAmount,0) - IFNull(VPCS.TotalReceiveAmount,0)"
                    OrdUnit1 = "IFNull(VPO.OrdQty,0)" : CancelUnit1 = "IFNull(VPO.CanQty,0)" : AmdUnit1 = "IFNull(VPO.AmdQty,0)" : ReceiveUnit1 = "IFNull(VPC.ChallanQty,0)"

                    Unit2Desc = "'Currency'" : Unit2Head = "'Amount'" : Unit2 = "E.DefaultCurrency" : Unit2DecimalPlace = "2"
                    TotalOrdUnit2 = "IFNull(VPO.BalOrdAmount,0)" : TotalReceiveUnit2 = "IFNull(VPCS.TotalReceiveAmount,0)" : TotalLossUnit2 = "IFNull(VPCS.TotalLossQty,0)" : TotalBalUnit2 = "IFNull(VPO.BalOrdAmount,0) - IFNull(VPCS.TotalReceiveAmount,0)"
                    OrdUnit2 = "IFNull(VPO.OrdAmount,0)" : CancelUnit2 = "IFNull(VPO.CanAmount,0)" : AmdUnit2 = "IFNull(VPO.AmdAmount,0)" : ReceiveUnit2 = "IFNull(VPC.ReceiveAmount,0)" : LossUnit2 = "IFNull(VPC.LossQty,0)"

                    'RepName = RepName.Replace("With2Unit", "With1Unit_WithAmount")
                ElseIf IsUnitMeasure = 1 And IsUnitAmount = 1 Then
                    Unit1Desc = "'Unit'" : Unit1Head = "'Measure'" : Unit1 = "L.MeasureUnit" : Unit1DecimalPlace = "MU.DecimalPlaces"
                    TotalOrdUnit1 = "IFNull(VPO.BalOrdQty,0)*IFNull(I.Measure,0)" : TotalReceiveUnit1 = "IFNull(VPCS.TotalReceiveQty,0)*IFNull(I.Measure,0)" : TotalLossUnit1 = "IFNull(VPCS.TotalLossQty,0)*IFNull(I.Measure,0)" : TotalBalUnit1 = "(IFNull(VPO.BalOrdQty,0) - IFNull(VPCS.TotalReceiveQty,0) - IFNull(VPCS.TotalLossQty,0))*IFNull(I.Measure,0)"
                    OrdUnit1 = "IFNull(VPO.OrdQty,0)*IFNull(I.Measure,0)" : CancelUnit1 = "IFNull(VPO.CanQty,0)*IFNull(I.Measure,0)" : AmdUnit1 = "IFNull(VPO.AmdQty,0)*IFNull(I.Measure,0)" : ReceiveUnit1 = "IFNull(VPC.ChallanQty,0)*IFNull(I.Measure,0)" : LossUnit1 = "IFNull(VPC.LossQty,0)*IFNull(I.Measure,0)"

                    Unit2Desc = "'Currency'" : Unit2Head = "'Amount'" : Unit2 = "E.DefaultCurrency" : Unit2DecimalPlace = "2"
                    TotalOrdUnit2 = "IFNull(VPO.BalOrdAmount,0)" : TotalReceiveUnit2 = "IFNull(VPCS.TotalReceiveAmount,0)" : TotalLossUnit2 = "IFNull(VPCS.TotalLossQty,0)" : TotalBalUnit2 = "IFNull(VPO.BalOrdAmount,0) - IFNull(VPCS.TotalReceiveAmount,0)"
                    OrdUnit2 = "IFNull(VPO.OrdAmount,0)" : CancelUnit2 = "IFNull(VPO.CanAmount,0)" : AmdUnit2 = "IFNull(VPO.AmdAmount,0)" : ReceiveUnit2 = "IFNull(VPC.ReceiveAmount,0)" : LossUnit2 = "IFNull(VPC.LossQty,0)"

                End If
                'RepName = "Report_UnderConstruction"
            ElseIf IsUnitQty + IsUnitMeasure + IsUnitAmount = 1 Then
                RepName = RepName + "_With1Unit"
                If IsUnitQty = 1 Then
                    Unit1Desc = "'Unit'" : Unit1Head = "'Qty'" : Unit1 = "L.Unit" : Unit1DecimalPlace = "U.DecimalPlaces"
                    TotalOrdUnit1 = "IFNull(VPO.BalOrdQty,0)" : TotalReceiveUnit1 = "IFNull(VPCS.TotalReceiveQty,0)" : TotalLossUnit1 = "IFNull(VPCS.TotalLossQty,0)" : TotalBalUnit1 = "IFNull(VPO.BalOrdQty,0) - IFNull(VPCS.TotalReceiveQty,0) - IFNull(VPCS.TotalLossQty,0)"
                    OrdUnit1 = "IFNull(VPO.OrdQty,0)" : CancelUnit1 = "IFNull(VPO.CanQty,0)" : AmdUnit1 = "IFNull(VPO.AmdQty,0)" : ReceiveUnit1 = "IFNull(VPC.ReceiveQty,0)" : LossUnit1 = "IFNull(VPC.LossQty,0)"
                End If

                If IsUnitMeasure = 1 Then
                    Unit1Desc = "'Unit'" : Unit1Head = "'Measure'" : Unit1 = "L.MeasureUnit" : Unit1DecimalPlace = "MU.DecimalPlaces"
                    TotalOrdUnit1 = "IFNull(VPO.BalOrdMeasure,0)" : TotalReceiveUnit1 = "IFNull(VPCS.TotalReceiveMeasure,0)" : TotalLossUnit1 = "IFNull(VPCS.TotalLossQty,0)" : TotalBalUnit1 = "IFNull(VPO.BalOrdMeasure,0) - IFNull(VPCS.TotalReceiveMeasure,0) - IFNull(VPCS.TotalLossQty,0)"
                    OrdUnit1 = "IFNull(VPO.OrdMeasure,0)" : CancelUnit1 = "IFNull(VPO.CanMeasure,0)" : AmdUnit1 = "IFNull(VPO.AmdMeasure,0)" : ReceiveUnit1 = "IFNull(VPC.ReceiveMeasure,0)" : LossUnit1 = "IFNull(VPC.LossQty,0)"
                End If

                If IsUnitAmount = 1 Then
                    Unit1Desc = "'Currency'" : Unit1Head = "'Amount'" : Unit1 = "E.DefaultCurrency" : Unit1DecimalPlace = "2"
                    TotalOrdUnit1 = "IFNull(VPO.BalOrdAmount,0)" : TotalReceiveUnit1 = "IFNull(VPCS.TotalReceiveAmount,0)" : TotalLossUnit1 = "IFNull(VPCS.TotalLossQty,0)" : TotalBalUnit1 = "IFNull(VPO.BalOrdAmount,0) - IFNull(VPCS.TotalReceiveAmount,0)"
                    OrdUnit1 = "IFNull(VPO.OrdAmount,0)" : CancelUnit1 = "IFNull(VPO.CanAmount,0)" : AmdUnit1 = "IFNull(VPO.AmdAmount,0)" : ReceiveUnit1 = "IFNull(VPC.ReceiveAmount,0)" : LossUnit1 = "IFNull(VPC.LossQty,0)"
                End If
            End If


            If ReportFrm.FGetCode(6) IsNot Nothing Then
                If ReportFrm.FGetCode(6).ToString.Contains(AgTemplate.ClsMain.FGetDimension1Caption) = True Then IsGroupOnDimension1 = 1
                If ReportFrm.FGetCode(6).ToString.Contains(AgTemplate.ClsMain.FGetDimension2Caption) = True Then IsGroupOnDimension2 = 1
                If ReportFrm.FGetCode(6).ToString.Contains("Lot No") = True Then IsGroupOnLotNo = 1
            End If

            If IsGroupOnDimension1 + IsGroupOnDimension2 + IsGroupOnLotNo = 3 Then
                RepName = RepName + "_With3Dimensions"
                mShowForValue1 = "L.LotNo" : mShowForHead1 = "'Lot No'"
                mShowForValue2 = "D1.Description" : mShowForHead2 = "'" & AgTemplate.ClsMain.FGetDimension1Caption() & "'"
                mShowForValue3 = "D2.Description" : mShowForHead3 = "'" & AgTemplate.ClsMain.FGetDimension2Caption() & "'"
            ElseIf IsGroupOnDimension1 + IsGroupOnDimension2 + IsGroupOnLotNo = 2 Then
                RepName = RepName + "_With2Dimensions"
                If IsGroupOnDimension1 = 1 And IsGroupOnDimension2 = 1 Then
                    mShowForValue1 = "D1.Description" : mShowForHead1 = "'" & AgTemplate.ClsMain.FGetDimension1Caption() & "'"
                    mShowForValue2 = "D2.Description" : mShowForHead2 = "'" & AgTemplate.ClsMain.FGetDimension2Caption() & "'"
                ElseIf IsGroupOnDimension1 = 1 And IsGroupOnLotNo = 1 Then
                    mShowForValue1 = "D1.Description" : mShowForHead1 = "'" & AgTemplate.ClsMain.FGetDimension1Caption() & "'"
                    mShowForValue2 = "L.LotNo" : mShowForHead2 = "'Lot No'"
                ElseIf IsGroupOnDimension2 = 1 And IsGroupOnLotNo = 1 Then
                    mShowForValue1 = "D2.Description" : mShowForHead1 = "'" & AgTemplate.ClsMain.FGetDimension2Caption() & "'"
                    mShowForValue2 = "L.LotNo" : mShowForHead2 = "'Lot No'"
                End If
            ElseIf IsGroupOnDimension1 + IsGroupOnDimension2 + IsGroupOnLotNo = 1 Then
                'RepName = RepName + "_With1Dimensions"
                RepName = "Report_UnderConstruction"
                If IsGroupOnDimension1 = 1 Then mShowForValue1 = "D1.Description" : mShowForHead1 = "'" & AgTemplate.ClsMain.FGetDimension1Caption() & "'"
                If IsGroupOnDimension2 = 1 Then mShowForValue1 = "D2.Description" : mShowForHead1 = "'" & AgTemplate.ClsMain.FGetDimension2Caption() & "'"
                If IsGroupOnLotNo = 1 Then mShowForValue1 = "L.LotNo" : mShowForHead1 = "'Lot No'"
            End If




            mCondStr = mCondStr & " And H.V_Date Between " & AgL.Chk_Text(ReportFrm.FGetText(0)) & " And " & AgL.Chk_Text(ReportFrm.FGetText(1)) & " "
            'mCondStr = mCondStr & " AND Vt.NCat = '" & AgTemplate.ClsMain.Temp_NCat.JobOrder & "' "
            mCondStr = mCondStr & " And H.Site_Code = '" & AgL.PubSiteCode & "' "
            mCondStr = mCondStr & " And H.Div_Code= '" & AgL.PubDivCode & "' "

            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.V_Type", 7)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.Process", 8)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.JobWorker", 9)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.JobOrder ", 10)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Item", 11)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemGroup", 12)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemCategory", 13)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemType ", 14)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.LotNo", 16)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Dimension1", 17)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Dimension2", 18)
            mCondStr = mCondStr & "AND IFNull(L.T_Nature,0) = 0 "

            If ReportFrm.FGetText(15) <> "" And ReportFrm.FGetText(15) <> "All" Then
                mQry = " Select '''' +  replace(ItemList,',',''',''')  + ''''  From ItemReportingGroup Where 1=1 " & ReportFrm.GetWhereCondition("Code", 15)
                mCondStr = mCondStr & " AND I.Code In (" & AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar & ")"
            End If

            If ReportFrm.FGetText(4) = "Pending To Receive" Then
                mCondStr = mCondStr & " AND Round(IFNull(VPO.BalORDQty,0)- IFNull(VPCS.TotalReceiveQty,0),4) - Round(IFNull(VPCS.TotalLossQty,0),4) > 0 "
            ElseIf ReportFrm.FGetText(4) = "Over Due" Then
                mCondStr = mCondStr & "  AND H.DueDate < IFNull(VPC.ReceiveDate,'" & ReportFrm.FGetText(2) & "') "
            ElseIf ReportFrm.FGetText(4) = "Over Due And Balance" Then
                mCondStr = mCondStr & " AND Round(IFNull(VPO.BalORDQty,0)- IFNull(VPCS.TotalReceiveQty,0),4) - Round(IFNull(VPCS.TotalLossQty,0),4) > 0 "
                mCondStr = mCondStr & "  AND H.DueDate < IFNull(VPC.ReceiveDate,'" & ReportFrm.FGetText(2) & "') "
            End If

            If ReportFrm.FGetText(3) = "Detail" Then
                mQry = " SELECT H.DocID, H.V_Type, H.V_Date, H.ManualRefNo , SG.DispName AS JobWorkerName, H.DueDate AS DueDate, " &
                        " H.Remarks, L.Sr, P.Description AS ProcessDesc, P.Sr AS ProcessSr, " &
                        " L.Item, L.Remark AS LineRemark, I.Description AS ItemDesc, Vt.Description AS VoucherTypeDesc,  " &
                        " CASE WHEN IFNull(VPO.BalOrdQty,0) > 0 THEN IFNull(VPCS.TotalLossQty,0)*100/IFNull(VPO.BalOrdQty,0) ELSE 0 END AS TotalLossPer, " &
                        " CASE WHEN IFNull(VPO.BalOrdQty,0) - IFNull(VPCS.TotalReceiveQty,0) - IFNull(VPCS.TotalLossQty,0) > 0 THEN  datediff(Day,H.DueDate,'" & ReportFrm.FGetText(2) & "') ELSE datediff(Day,H.DueDate,VPCS.MaxRecDate) END AS Ageing, " &
                        " " & TotalOrdUnit1 & " AS TotalOrdUnit1, " & TotalOrdUnit2 & " AS TotalOrdUnit2, " &
                        " " & TotalReceiveUnit1 & " AS TotalReceiveUnit1, " & TotalReceiveUnit2 & " AS TotalReceiveUnit2, " &
                        " " & TotalLossUnit1 & " AS TotalLossUnit1, " & TotalLossUnit2 & " AS TotalLossUnit2, " &
                        " CASE When " & TotalBalUnit1 & " < 0 Then 0 Else " & TotalBalUnit1 & " END AS TotalBalUnit1, " &
                        " CASE WHEN " & TotalBalUnit2 & " < 0 THEN 0 ELSE " & TotalBalUnit2 & " END AS TotalBalUnit2, " &
                        " " & Unit1Desc & " AS Unit1Desc, " & Unit2Desc & " AS Unit2Desc, " &
                        " " & Unit1Head & " as Unit1Head, " & Unit1 & " AS Unit1, " & Unit1DecimalPlace & " as Unit1DecimalPlace, " &
                        " " & Unit2Head & " as Unit2Head, " & Unit2 & " as Unit2, " & Unit2DecimalPlace & " as Unit2DecimalPlace, " &
                        " " & OrdUnit1 & " AS OrdUnit1, " & CancelUnit1 & " AS CancelUnit1, " & AmdUnit1 & " AS AmdUnit1, " &
                        " " & OrdUnit2 & " AS OrdUnit2, " & CancelUnit2 & " AS CancelUnit2, " & AmdUnit2 & " AS AmdUnit2, " &
                        " " & mShowForHead1 & " AS mShowForHead1, " & mShowForValue1 & " AS mShowForValue1, " &
                        " " & mShowForHead2 & " AS mShowForHead2, " & mShowForValue2 & " AS mShowForValue2, " &
                        " " & mShowForHead3 & " AS mShowForHead3, " & mShowForValue3 & " AS mShowForValue3, " &
                        " " & GroupOn & " AS GroupOn, " & GroupOnHead & " AS GroupOnHead, " &
                        " VPC.ReceiveNo, VPC.ReceiveDate, VPC.JobWorkerDocNo, " & ReceiveUnit1 & " AS ReceiveUnit1,   " & ReceiveUnit2 & " AS ReceiveUnit2, " & LossUnit1 & " AS LossUnit1,   " & LossUnit2 & " AS LossUnit2, " &
                        " " & IsMultiProcess & " As IsMultiProcess, " & IsProcessinNewPage & " As IsProcessinNewPage " &
                        " FROM ( Select H.* From JobOrder H  Where 1= 1 " & mCondStrMain & " ) AS H " &
                        " LEFT JOIN SUBGROUP SG  ON SG.SubCode = H.JobWorker " &
                        " LEFT JOIN JobOrderDetail L  ON L.DocId = H.DocID  " &
                        " LEFT JOIN Item I  ON I.Code = L.Item  " &
                        " LEFT JOIN Unit U  ON U.Code = L.Unit " &
                        " LEFT JOIN Unit MU  ON MU.Code = L.MeasureUnit " &
                        " LEFT JOIN Voucher_Type Vt  ON Vt.V_Type = H.V_Type " &
                        " LEFT JOIN Enviro E  ON E.Site_Code = H.Site_Code AND E.Div_Code = H.Div_Code " &
                        " LEFT JOIN Dimension1 D1  ON D1.Code = L.Dimension1  " &
                        " LEFT JOIN Dimension2 D2  ON D2.Code = L.Dimension2 " &
                        " LEFT JOIN Process P  On H.Process = P.NCat   " &
                        " LEFT JOIN ( " & mQryJobReceiveSummury & " ) VPCS ON VPCS.JobOrder = L.DocId AND VPCS.JObOrderSr = L.JObOrderSr " &
                        " LEFT JOIN ( " & mQryJobReceive & " ) VPC ON VPC.JobOrder = L.DocId AND VPC.JObOrderSr = L.JObOrderSr " &
                        " LEFT JOIN ( " & mQryJobOrder & " ) VPO ON VPO.JobOrder = L.DocId AND VPO.JObOrderSr = L.JObOrderSr " &
                        " WHERE 1=1 " & mCondStr & " "

                DsRep = AgL.FillData(mQry, AgL.GCn)
                If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records to Print!")

            Else
                mQry = " SELECT H.DocID, H.V_Type, H.V_Date, H.ManualRefNo , SG.DispName AS JobWorkerName, H.DueDate AS DueDate, " &
                        " H.Remarks, L.Sr, P.Description AS ProcessDesc, P.Sr AS ProcessSr, " &
                        " L.Item, L.Remark AS LineRemark, I.Description AS ItemDesc, Vt.Description AS VoucherTypeDesc,  " &
                        " CASE WHEN IFNull(VPO.BalOrdQty,0) > 0 THEN IFNull(VPCS.TotalLossQty,0)*100/IFNull(VPO.BalOrdQty,0) ELSE 0 END AS TotalLossPer, " &
                        " CASE WHEN IFNull(VPO.BalOrdQty,0) - IFNull(VPCS.TotalReceiveQty,0) - IFNull(VPCS.TotalLossQty,0) > 0 THEN  datediff(Day,H.DueDate,'" & ReportFrm.FGetText(2) & "') ELSE datediff(Day,H.DueDate,VPCS.MaxRecDate) END AS Ageing, " &
                        " " & TotalOrdUnit1 & " AS TotalOrdUnit1, " & TotalOrdUnit2 & " AS TotalOrdUnit2, " &
                        " " & TotalReceiveUnit1 & " AS TotalReceiveUnit1, " & TotalReceiveUnit2 & " AS TotalReceiveUnit2, " &
                        " " & TotalLossUnit1 & " AS TotalLossUnit1, " & TotalLossUnit2 & " AS TotalLossUnit2, " &
                        " CASE When " & TotalBalUnit1 & " < 0 Then 0 Else " & TotalBalUnit1 & " END AS TotalBalUnit1, " &
                        " CASE WHEN " & TotalBalUnit2 & " < 0 THEN 0 ELSE " & TotalBalUnit2 & " END AS TotalBalUnit2, " &
                        " " & Unit1Desc & " AS Unit1Desc, " & Unit2Desc & " AS Unit2Desc, " &
                        " " & Unit1Head & " as Unit1Head, " & Unit1 & " AS Unit1, " & Unit1DecimalPlace & " as Unit1DecimalPlace, " &
                        " " & Unit2Head & " as Unit2Head, " & Unit2 & " as Unit2, " & Unit2DecimalPlace & " as Unit2DecimalPlace, " &
                        " " & OrdUnit1 & " AS OrdUnit1, " & CancelUnit1 & " AS CancelUnit1, " & AmdUnit1 & " AS AmdUnit1, " &
                        " " & OrdUnit2 & " AS OrdUnit2, " & CancelUnit2 & " AS CancelUnit2, " & AmdUnit2 & " AS AmdUnit2, " &
                        " " & mShowForHead1 & " AS mShowForHead1, " & mShowForValue1 & " AS mShowForValue1, " &
                        " " & mShowForHead2 & " AS mShowForHead2, " & mShowForValue2 & " AS mShowForValue2, " &
                        " " & mShowForHead3 & " AS mShowForHead3, " & mShowForValue3 & " AS mShowForValue3, " &
                        " " & GroupOn & " AS GroupOn, " & GroupOnHead & " AS GroupOnHead, " &
                        " " & IsMultiProcess & " As IsMultiProcess, " & IsProcessinNewPage & " As IsProcessinNewPage " &
                        " FROM ( Select H.* From JobOrder H  Where 1= 1 " & mCondStrMain & " ) AS H " &
                        " LEFT JOIN SUBGROUP SG  ON SG.SubCode = H.JobWorker " &
                        " LEFT JOIN JobOrderDetail L  ON L.DocId = H.DocID  " &
                        " LEFT JOIN Item I  ON I.Code = L.Item  " &
                        " LEFT JOIN Unit U  ON U.Code = L.Unit " &
                        " LEFT JOIN Unit MU  ON MU.Code = L.MeasureUnit " &
                        " LEFT JOIN Voucher_Type Vt  ON Vt.V_Type = H.V_Type " &
                        " LEFT JOIN Enviro E  ON E.Site_Code = H.Site_Code AND E.Div_Code = H.Div_Code " &
                        " LEFT JOIN Dimension1 D1  ON D1.Code = L.Dimension1  " &
                        " LEFT JOIN Dimension2 D2  ON D2.Code = L.Dimension2 " &
                        " LEFT JOIN Process P  On H.Process = P.NCat   " &
                        " LEFT JOIN ( " & mQryJobReceiveSummury & " ) VPCS ON VPCS.JobOrder = L.DocId AND VPCS.JObOrderSr = L.JObOrderSr " &
                        " LEFT JOIN ( " & mQryJobOrder & " ) VPO ON VPO.JobOrder = L.DocId AND VPO.JObOrderSr = L.JObOrderSr " &
                        " WHERE 1=1 " & mCondStr & " "

                DsRep = AgL.FillData(mQry, AgL.GCn)
                If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records to Print!")
            End If


            If DsRep.Tables(0).Select("TotalLossUnit1 > 0").Length > 0 Then
                If RepName <> "Report_UnderConstruction" Then RepName = RepName + "_WithLoss"
            End If

            ReportFrm.PrintReport(DsRep, RepName, RepTitle, ReportPath)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub
#End Region

#Region "Job Receive Status Report"
    Private Sub ProcJobReceiveStatusReport()
        Dim mCondStr$ = ""
        Dim mCondStr1$ = ""
        Dim IsMultiProcess As Integer = 0
        Dim IsProcessinNewPage As Integer = 0
        Dim mProcessName As String

        Dim strGrpFld As String = "''", strGrpFldHead As String = "''"
        Dim strTotalBalFeild1$ = "", strTotalRecFeild1$ = "", strTotalInvFeild1$ = "", strFeild1Head$ = "", strFeild1Unit$ = "", strFeild1DecimalPlaces$ = "", strInvFeild1$ = ""
        Dim strTotalBalFeild2$ = "", strTotalRecFeild2$ = "", strTotalInvFeild2$ = "", strFeild2Head$ = "", strFeild2Unit$ = "", strFeild2DecimalPlaces$ = "", strInvFeild2$ = ""

        'mCondStr1 = mCondStr1 & ReportFrm.GetWhereCondition("H.Process", 6)
        'mCondStr1 = mCondStr1 & ReportFrm.GetWhereCondition("H.JobWorker", 7)
        'mCondStr1 = mCondStr1 & ReportFrm.GetWhereCondition("L.JobOrder ", 8)
        'mCondStr1 = mCondStr1 & ReportFrm.GetWhereCondition("L.Item", 9)
        'mCondStr1 = mCondStr1 & ReportFrm.GetWhereCondition("I.ItemGroup", 10)
        'mCondStr1 = mCondStr1 & ReportFrm.GetWhereCondition("I.ItemCategory", 11)
        'mCondStr1 = mCondStr1 & ReportFrm.GetWhereCondition("I.ItemType ", 12)
        'mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.LotNo", 14)

        'If ReportFrm.FGetText(13) <> "" And ReportFrm.FGetText(13) <> "All" Then
        '    mQry = " Select '''' +  replace(ItemList,',',''',''')  + ''''  From ItemReportingGroup Where 1=1 " & ReportFrm.GetWhereCondition("Code", 13)
        '    mCondStr = mCondStr & " AND I.Code In (" & AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar & ")"
        'End If

        mCondStr1 = mCondStr1 & " And H.Site_Code = '" & AgL.PubSiteCode & "' "
        mCondStr1 = mCondStr1 & " And H.Div_Code= '" & AgL.PubDivCode & "' "

        Dim mQryJobInvoice$ = " SELECT L.JobReceive, L.JobReceiveSr, " &
                    " Max(H.ManualRefNo) AS InvoiceNo, Max(H.V_Date) AS InvoiceDate, Sum(L.Qty) AS InvoiceQty, Sum(L.TotalMeasure) AS InvoiceMeasure " &
                    " FROM JobInvoiceDetail L  " &
                    " LEFT JOIN JobInvoice H  ON H.DocID = L.DocId  " &
                    " LEFT JOIN Item I  ON I.Code = L.Item  " &
                    " WHERE IFNull( L.JobReceive,'') <> '' " &
                    " AND H.V_Date <= '" & ReportFrm.FGetText(2) & "' " & mCondStr1 &
                    " Group BY L.JobReceive, L.JobReceiveSr, H.DocId "

        Dim mQryJobInvoiceSummury$ = " SELECT L.JobReceive, L.JobReceiveSr,  sum(L.Qty) AS TotalInvoiceQty, sum(L.TotalMeasure) AS TotalInvoiceMeasure " &
                    " FROM JobInvoiceDetail L  " &
                    " LEFT JOIN JobInvoice H  ON H.DocID = L.DocId  " &
                    " LEFT JOIN Item I  ON I.Code = L.Item  " &
                    " WHERE IFNull( L.JobReceive,'') <> '' " &
                    " AND H.V_Date <= '" & ReportFrm.FGetText(2) & "' " & mCondStr1 & " " &
                    " GROUP BY L.JobReceive, L.JobReceiveSr "

        Dim mQryJobReceive$ = " SELECT L.JobReceive, L.JobReceiveSr, sum(L.Qty) AS BalRecQty, sum(L.TotalMeasure) AS BalRecMeasure   " &
                            " FROM JobReceiveDetail L   " &
                            " LEFT JOIN JobIssRec H  ON H.DocId = L.DocId " &
                            " LEFT JOIN Item I  ON I.Code = L.Item  " &
                            " WHERE IFNull(L.JobReceive,'') <> ''  " &
                            " AND H.V_Date <= '" & ReportFrm.FGetText(2) & "' " & mCondStr1 & " " &
                            " GROUP BY L.JobReceive, L.JobReceiveSr "

        If ReportFrm.FGetText(6).ToString.Contains(",") Or ReportFrm.FGetText(6) = "All" Then
            mProcessName = "Job"
            IsMultiProcess = 1
        Else
            mProcessName = ReportFrm.FGetText(6)
            IsMultiProcess = 0
        End If

        'If ReportFrm.FGetText(15) = "Yes" Then
        '    IsProcessinNewPage = 1
        'Else
        '    IsProcessinNewPage = 0
        'End If

        Try
            If ReportFrm.FGetText(5) = "Qty & Measure" Then
                strFeild1Head = "'Qty'"
                strFeild1Unit = "L.Unit"
                strFeild1DecimalPlaces = "U.DecimalPlaces"
                strTotalBalFeild1 = "IFNull(VPO.BalRecQty,0) - IFNull(VPCS.TotalInvoiceQty,0)"
                strTotalRecFeild1 = "IFNull(VPO.BalRecQty,0)"
                strTotalInvFeild1 = "IFNull(VPCS.TotalInvoiceQty,0)"
                strInvFeild1 = "IFNull(VPC.InvoiceQty,0)"

                strFeild2Head = "'Measure'"
                strFeild2Unit = "L.MeasureUnit"
                strFeild2DecimalPlaces = "UM.DecimalPlaces"
                strTotalBalFeild2 = "IFNull(VPO.BalRecMeasure,0) - IFNull(VPCS.TotalInvoiceMeasure,0)"
                strTotalRecFeild2 = "IFNull(VPO.BalRecMeasure,0)"
                strTotalInvFeild2 = "IFNull(VPCS.TotalInvoiceMeasure,0)"
                strInvFeild2 = "IFNull(VPC.InvoiceMeasure,0)"

                If ReportFrm.FGetText(3) = "Detail" Then
                    RepTitle = "Job Receive Status"
                    RepName = "Trade_JobReceiveStatusReport_Detail_QtyMeasure"
                ElseIf ReportFrm.FGetText(3) = "Item Wise Receive Status" Then
                    RepTitle = "Item Wise Job Receive Status"
                    RepName = "Trade_JobReceiveStatusReport_Summary_QtyMeasure"
                    strGrpFld = "I.Description"
                    strGrpFldHead = "'Item'"
                ElseIf ReportFrm.FGetText(3) = "Job Worker Wise Receive Status" Then
                    RepTitle = "Item Wise Job Receive Status"
                    RepName = "Trade_JobReceiveStatusReport_Summary_QtyMeasure"
                    strGrpFld = "SG.DispName"
                    strGrpFldHead = "'Job Worker'"
                Else
                    RepTitle = "Job Receive Status"
                    RepName = "Trade_JobReceiveStatusReport_QtyMeasure"
                End If
            ElseIf ReportFrm.FGetText(5) = "Qty & Amount" Then
                strFeild1Head = "'Qty'"
                strFeild1Unit = "L.Unit"
                strFeild1DecimalPlaces = "U.DecimalPlaces"
                strTotalBalFeild1 = "IFNull(VPO.BalRecQty,0) - IFNull(VPCS.TotalInvoiceQty,0)"
                strTotalRecFeild1 = "IFNull(VPO.BalRecQty,0)"
                strTotalInvFeild1 = "IFNull(VPCS.TotalInvoiceQty,0)"
                strInvFeild1 = "IFNull(VPC.InvoiceQty,0)"

                strFeild2Head = "'" & ReportFrm.FGetText(6) & "'"
                strFeild2Unit = "'INR'"
                strFeild2DecimalPlaces = "2"
                strTotalBalFeild2 = "(IFNull(VPO.BalRecQty,0) - IFNull(VPCS.TotalInvoiceQty,0))*IFNull(L." & Replace(ReportFrm.FGetCode(6), "'", "") & ",0)/L.Qty"
                strTotalRecFeild2 = "IFNull(VPO.BalRecQty,0)*IFNull(L." & Replace(ReportFrm.FGetCode(6), "'", "") & ",0)/L.Qty"
                strTotalInvFeild2 = "IFNull(VPCS.TotalInvoiceQty,0)*IFNull(L." & Replace(ReportFrm.FGetCode(6), "'", "") & ",0)/L.Qty"
                strInvFeild2 = "IFNull(VPC.InvoiceQty,0)*IFNull(L." & Replace(ReportFrm.FGetCode(6), "'", "") & ",0)/L.Qty"

                If ReportFrm.FGetText(3) = "Detail" Then
                    RepTitle = "Job Receive Status"
                    RepName = "Trade_JobReceiveStatusReport_Detail_QtyMeasure"
                ElseIf ReportFrm.FGetText(3) = "Item Wise Receive Status" Then
                    RepTitle = "Item Wise Job Receive Status"
                    RepName = "Trade_JobReceiveStatusReport_Summary_QtyMeasure"
                    strGrpFld = "I.Description"
                    strGrpFldHead = "'Item'"
                ElseIf ReportFrm.FGetText(3) = "Job Worker Wise Receive Status" Then
                    RepTitle = "Item Wise Job Receive Status"
                    RepName = "Trade_JobReceiveStatusReport_Summary_QtyMeasure"
                    strGrpFld = "SG.DispName"
                    strGrpFldHead = "'Job Worker'"
                Else
                    RepTitle = "Job Receive Status"
                    RepName = "Trade_JobReceiveStatusReport_QtyMeasure"
                End If
            ElseIf ReportFrm.FGetText(5) = "Measure" Then
                strFeild1Head = "'Measure'"
                strFeild1Unit = "L.MeasureUnit"
                strFeild1DecimalPlaces = "UM.DecimalPlaces"
                strTotalBalFeild1 = "IFNull(VPO.BalRecMeasure,0) - IFNull(VPCS.TotalInvoiceMeasure,0)"
                strTotalRecFeild1 = "IFNull(VPO.BalRecMeasure,0)"
                strTotalInvFeild1 = "IFNull(VPCS.TotalInvoiceMeasure,0)"
                strInvFeild1 = "IFNull(VPC.InvoiceMeasure,0)"

                strFeild2Head = "''"
                strFeild2Unit = "''"
                strFeild2DecimalPlaces = "0"
                strTotalInvFeild2 = "0"
                strTotalBalFeild2 = "0"
                strTotalRecFeild2 = "0"
                strInvFeild2 = "0"

                If ReportFrm.FGetText(3) = "Detail" Then
                    RepTitle = "Job Receive Status"
                    RepName = "Trade_JobReceiveStatusReport_Detail"
                ElseIf ReportFrm.FGetText(3) = "Item Wise Receive Status" Then
                    RepTitle = "Item Wise Job Receive Status"
                    RepName = "Trade_JobReceiveStatusReport_Summary"
                    strGrpFld = "I.Description"
                    strGrpFldHead = "'Item'"
                ElseIf ReportFrm.FGetText(3) = "Job Worker Wise Receive Status" Then
                    RepTitle = "Item Wise Job Receive Status"
                    RepName = "Trade_JobReceiveStatusReport_Summary"
                    strGrpFld = "SG.DispName"
                    strGrpFldHead = "'Job Worker'"
                Else
                    RepTitle = "Job Receive Status"
                    RepName = "Trade_JobReceiveStatusReport"
                End If
            ElseIf ReportFrm.FGetText(5) = "Qty" Then
                strFeild1Head = "'Qty'"
                strFeild1Unit = "L.Unit"
                strFeild1DecimalPlaces = "U.DecimalPlaces"
                strTotalBalFeild1 = "IFNull(VPO.BalRecQty,0) - IFNull(VPCS.TotalInvoiceQty,0)"
                strTotalRecFeild1 = "IFNull(VPO.BalRecQty,0)"
                strTotalInvFeild1 = "IFNull(VPCS.TotalInvoiceQty,0)"
                strInvFeild1 = "IFNull(VPC.InvoiceQty,0)"

                strFeild2Head = "''"
                strFeild2Unit = "''"
                strFeild2DecimalPlaces = "0"
                strTotalInvFeild2 = "0"
                strTotalBalFeild2 = "0"
                strTotalRecFeild2 = "0"
                strInvFeild2 = "0"

                If ReportFrm.FGetText(3) = "Detail" Then
                    RepTitle = "Job Receive Status"
                    RepName = "Trade_JobReceiveStatusReport_Detail"
                ElseIf ReportFrm.FGetText(3) = "Item Wise Receive Status" Then
                    RepTitle = "Item Wise Job Receive Status"
                    RepName = "Trade_JobReceiveStatusReport_Summary"
                    strGrpFld = "I.Description"
                    strGrpFldHead = "'Item'"
                ElseIf ReportFrm.FGetText(3) = "Job Worker Wise Receive Status" Then
                    RepTitle = "Item Wise Job Receive Status"
                    RepName = "Trade_JobReceiveStatusReport_Summary"
                    strGrpFld = "SG.DispName"
                    strGrpFldHead = "'Job Worker'"
                Else
                    RepTitle = "Job Receive Status"
                    RepName = "Trade_JobReceiveStatusReport"
                End If
            Else
                strFeild1Head = "'" & ReportFrm.FGetText(6) & "'"
                strFeild1Unit = "'INR'"
                strFeild1DecimalPlaces = "2"
                strTotalBalFeild1 = "(IFNull(VPO.BalRecQty,0) - IFNull(VPCS.TotalInvoiceQty,0))*IFNull(L." & Replace(ReportFrm.FGetCode(6), "'", "") & ",0)/L.Qty"
                strTotalRecFeild1 = "IFNull(VPO.BalRecQty,0)*IFNull(L." & Replace(ReportFrm.FGetCode(6), "'", "") & ",0)/L.Qty"
                strTotalInvFeild1 = "IFNull(VPCS.TotalInvoiceQty,0)*IFNull(L." & Replace(ReportFrm.FGetCode(6), "'", "") & ",0)/L.Qty"
                strInvFeild1 = "IFNull(VPC.InvoiceQty,0)*IFNull(L." & Replace(ReportFrm.FGetCode(6), "'", "") & ",0)/L.Qty"

                strFeild2Head = "''"
                strFeild2Unit = "''"
                strFeild2DecimalPlaces = "0"
                strTotalInvFeild2 = "0"
                strTotalBalFeild2 = "0"
                strTotalRecFeild2 = "0"
                strInvFeild2 = "0"

                If ReportFrm.FGetText(3) = "Detail" Then
                    RepTitle = "Job Receive Status"
                    RepName = "Trade_JobReceiveStatusReport_Detail"
                ElseIf ReportFrm.FGetText(3) = "Item Wise Receive Status" Then
                    RepTitle = "Item Wise Job Receive Status"
                    RepName = "Trade_JobReceiveStatusReport_Summary"
                    strGrpFld = "I.Description"
                    strGrpFldHead = "'Item'"
                ElseIf ReportFrm.FGetText(3) = "Job Worker Wise Receive Status" Then
                    RepTitle = "Item Wise Job Receive Status"
                    RepName = "Trade_JobReceiveStatusReport_Summary"
                    strGrpFld = "SG.DispName"
                    strGrpFldHead = "'Job Worker'"
                Else
                    RepTitle = "Job Receive Status"
                    RepName = "Trade_JobReceiveStatusReport"
                End If
            End If

            mCondStr = mCondStr & " And H.V_Date Between " & AgL.Chk_Text(ReportFrm.FGetText(0)) & " And " & AgL.Chk_Text(ReportFrm.FGetText(1)) & " "
            mCondStr = mCondStr & " And H.Site_Code = '" & AgL.PubSiteCode & "' "
            mCondStr = mCondStr & " And H.Div_Code= '" & AgL.PubDivCode & "' "

            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.Process", 7)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.JobWorker", 8)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.JobReceive ", 9)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Item", 10)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemGroup", 11)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemCategory", 12)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemType ", 13)

            If ReportFrm.FGetText(14) <> "" And ReportFrm.FGetText(14) <> "All" Then
                mQry = " Select '''' +  replace(ItemList,',',''',''')  + ''''  From ItemReportingGroup Where 1=1 " & ReportFrm.GetWhereCondition("Code", 14)
                mCondStr = mCondStr & " AND I.Code In (" & AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar & ")"
            End If




            'If ReportFrm.FGetText(4) = "Pending To Receive" Then
            '    mCondStr = mCondStr & "  AND IFNull(VPO.BalORDQty,0) - IFNull(VPCS.TotalReceiveQty,0) - IFNull(VPCS.TotalLossQty,0) > 0 "
            'ElseIf ReportFrm.FGetText(4) = "Over Due" Then
            '    mCondStr = mCondStr & "  AND H.DueDate < IFNull(VPC.ReceiveDate,'" & ReportFrm.FGetText(2) & "') "
            'ElseIf ReportFrm.FGetText(4) = "Over Due And Balance" Then
            '    mCondStr = mCondStr & "  AND IFNull(VPO.BalORDQty,0) - IFNull(VPCS.TotalReceiveQty,0) - IFNull(VPCS.TotalLossQty,0) > 0 "
            '    mCondStr = mCondStr & "  AND H.DueDate < IFNull(VPC.ReceiveDate,'" & ReportFrm.FGetText(2) & "') "
            'End If




            mQry = " SELECT H.DocID, H.V_Type, H.V_Date, H.ManualRefNo , SG.DispName AS JobWorkerName, " &
                    " H.Remarks, L.Sr, P.Description AS ProcessDesc, P.Sr AS ProcessSr, IU.Item_UID, " &
                    " L.Item, L.Qty, L.Unit, L.MeasurePerPcs , L.MeasureUnit, L.TotalMeasure, L.Rate, L.Amount, L.LotNo, " &
                    " L.Remark AS LineRemark, I.Description AS ItemDesc, Vt.Description AS VoucherTypeDesc, U.DecimalPlaces AS DecimalPlaces, UM.DecimalPlaces AS MeasureDecimalPlace,  " &
                    " E.Caption_Dimension1, E.Caption_Dimension2, D1.Description AS D1Desc,D2.Description AS D2Desc," &
                    " VPC.InvoiceNo, VPC.InvoiceDate, " & strInvFeild1 & " AS InvFeild1, " & strInvFeild2 & " AS InvFeild2, " &
                    " " & strFeild1Head & " AS Feild1Head, " & strFeild1Unit & " AS Feild1Unit, " & strFeild1DecimalPlaces & " AS Feild1DecimalPlaces, " &
                    " " & strTotalRecFeild1 & " AS TotalRecFeild1, " & strTotalInvFeild1 & " AS TotalInvFeild1, " & strTotalBalFeild1 & " AS TotalBalFeild1, " &
                    " " & strFeild2Head & " AS Feild2Head, " & strFeild2Unit & " AS Feild2Unit, " & strFeild2DecimalPlaces & " AS Feild2DecimalPlaces, " &
                    " " & strTotalRecFeild2 & " AS TotalRecFeild2, " & strTotalInvFeild2 & " AS TotalInvFeild2, " & strTotalBalFeild2 & " AS TotalBalFeild2, " &
                    "  " & strGrpFld & " AS GroupOn, " & strGrpFldHead & " AS GroupOnHead, " &
                    " " & IsMultiProcess & " As IsMultiProcess, " & IsProcessinNewPage & " As IsProcessinNewPage " &
                    " FROM JobIssRec H  " &
                    " LEFT JOIN SUBGROUP SG  ON SG.SubCode = H.JobWorker " &
                    " LEFT JOIN JobReceiveDetail L  ON L.DocId = H.DocID  " &
                    " LEFT JOIN Item I  ON I.Code = L.Item  " &
                    " LEFT JOIN Unit U  ON U.Code = L.Unit " &
                    " LEFT JOIN Unit UM  ON UM.Code = L.MeasureUnit " &
                    " LEFT JOIN Voucher_Type Vt  ON Vt.V_Type = H.V_Type " &
                    " LEFT JOIN Enviro E ON E.Site_Code = H.Site_Code AND E.Div_Code = H.Div_Code " &
                    " LEFT JOIN Dimension1 D1 ON D1.Code = L.Dimension1  " &
                    " LEFT JOIN Dimension2 D2 ON D2.Code = L.Dimension2 " &
                    " LEFT JOIN Process P  On H.Process = P.NCat   " &
                    " LEFT JOIN Item_UID IU ON IU.Code = L.Item_UID " &
                    " LEFT JOIN ( " & mQryJobInvoice & " ) VPC ON VPC.JobReceive = L.DocId AND VPC.JObReceiveSr = L.JObReceiveSr " &
                    " LEFT JOIN ( " & mQryJobInvoiceSummury & " ) VPCS ON VPCS.JobReceive = L.DocId AND VPCS.JObReceiveSr = L.JObReceiveSr " &
                    " LEFT JOIN ( " & mQryJobReceive & " ) VPO ON VPO.JobReceive = L.DocId AND VPO.JObReceiveSr = L.JObReceiveSr " &
                    " WHERE 1=1 " & mCondStr & " "

            DsRep = AgL.FillData(mQry, AgL.GCn)
            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records to Print!")

            ReportFrm.PrintReport(DsRep, RepName, RepTitle, ReportPath)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub
#End Region

#Region "Job Balance Report "
    Private Sub ProcJobBalanceReport()
        Dim IsMultiProcess As Integer = 0
        Dim mOrderBarcode As String = ""
        Dim mCondStr$ = ""

        Dim IsUnitQty As Integer = 0
        Dim IsUnitMeasure As Integer = 0
        Dim IsUnitAmount As Integer = 0

        Dim IsGroupOnLotNo As Integer = 0
        Dim IsGroupOnDimension1 As Integer = 0
        Dim IsGroupOnDimension2 As Integer = 0

        Dim BalUnit1 As String = "0", Unit1Desc As String = "''"
        Dim BalUnit2 As String = "0", Unit2Desc As String = "''"

        Dim Unit1Head = "''", Unit1 As String = "''", Unit1DecimalPlace As String = "''"
        Dim Unit2Head = "''", Unit2 As String = "''", Unit2DecimalPlace As String = "''"

        Dim mShowForValue1$ = "''", mShowForValue2$ = "''", mShowForValue3$ = "''"
        Dim mShowForHead1$ = "''", mShowForHead2$ = "''", mShowForHead3$ = "''"

        Dim GroupOn As String = "''", GroupOnValue As String = "''"

        Try
            If ReportFrm.FGetText(3) = "Detail" Then
                RepName = "Trade_JobBalanceReport_Detail"
            ElseIf ReportFrm.FGetText(3) = "Worker Wise Summary" Then
                RepName = "Trade_JobBalanceReport_Summary"
                GroupOnValue = "SG.DispName" : GroupOn = "'Worker'"
            ElseIf ReportFrm.FGetText(3) = "Item Wise Summary" Then
                RepName = "Trade_JobBalanceReport_Summary"
                GroupOnValue = "I.Description" : GroupOn = "'Item'"

                'RepName = "Trade_JobBalanceReport_Summary"
                'GroupOnValue = "D.Construction" : GroupOn = "'Construction'"
            Else
                RepName = "Trade_JobWorkerWiseOutstandingReport"
            End If

            If ReportFrm.FGetText(4) IsNot Nothing Then
                If ReportFrm.FGetText(4).ToString.Contains("Qty") = True Then IsUnitQty = 1
                If ReportFrm.FGetText(4).ToString.Contains("Measure") = True Then IsUnitMeasure = 1
                If ReportFrm.FGetText(4).ToString.Contains("Amount") = True Then IsUnitAmount = 1
            End If

            If IsUnitQty + IsUnitMeasure + IsUnitAmount = 3 Then
                'RepName = RepName + "_With3Unit"
                'Unit1 = "I.Unit" : Unit1Head = "'Qty'" : Unit1DecimalPlace = "U.DecimalPlaces" : InvoiceUnit1 = "L.Qty" : Unit1Type = "'Unit'"
                'Unit2 = "I.MeasureUnit" : Unit2Head = "'Measure'" : Unit2DecimalPlace = "UM.DecimalPlaces" : InvoiceUnit2 = "L.TotalMeasure" : Unit1Type = "'Unit'"
                'Unit3 = "C.Description" : Unit3Head = "'" & Replace(ReportFrm.FGetCode(9), "'", "") & "'" : Unit3DecimalPlace = "2" : InvoiceUnit3 = "L." & Replace(ReportFrm.FGetCode(9), "'", "") & "" : Unit3Type = "'Currency'"
                RepName = "Report_UnderConstruction"
            ElseIf IsUnitQty + IsUnitMeasure + IsUnitAmount = 2 Then
                RepName = RepName + "_With2Unit"
                If IsUnitQty = 1 And IsUnitMeasure = 1 Then

                    Unit1Desc = "'Unit'" : BalUnit1 = "IFNull(VOrd.OrdQty,0)-IFNull(VRec.RecQty,0)-IFNull(VRec.LossQty,0)"
                    Unit1Head = "'Qty'" : Unit1 = "VOrd.Unit" : Unit1DecimalPlace = "U.DecimalPlaces"

                    Unit2Desc = "'Unit'" : BalUnit2 = "(IFNull(VOrd.OrdMeasure,0) - IFNull(VRec.RecQty,0))"
                    Unit2Head = "'Measure'" : Unit2 = "VOrd.MeasureUnit" : Unit2DecimalPlace = "UM.DecimalPlaces"

                ElseIf IsUnitQty = 1 And IsUnitAmount = 1 Then
                    Unit1Desc = "'Unit'" : BalUnit1 = "IFNull(VOrd.OrdQty,0)-IFNull(VRec.RecQty,0)-IFNull(VRec.LossQty,0)"
                    Unit1Head = "'Qty'" : Unit1 = "VOrd.Unit" : Unit1DecimalPlace = "U.DecimalPlaces"

                    BalUnit2 = "IFNull(VOrd.OrdAmount,0) - IFNull(VRec.RecAmount,0)"
                    Unit2Desc = "'Currency'" : Unit2Head = "'" & ReportFrm.FGetText(7) & "'" : Unit2 = "C.Description" : Unit2DecimalPlace = "2"
                    RepName = RepName.Replace("With1Unit", "WithAmount")

                ElseIf IsUnitMeasure = 1 And IsUnitAmount = 1 Then
                    Unit1Desc = "'Unit'" : BalUnit1 = "IFNull(VOrd.OrdQty,0)-IFNull(VRec.RecQty,0)-IFNull(VRec.LossQty,0)"
                    Unit1Head = "'Qty'" : Unit1 = "VOrd.Unit" : Unit1DecimalPlace = "U.DecimalPlaces"

                    BalUnit2 = "IFNull(VOrd.OrdAmount,0) - IFNull(VRec.RecAmount,0)"
                    Unit2Desc = "'Currency'" : Unit2Head = "'" & ReportFrm.FGetText(7) & "'" : Unit2 = "C.Description" : Unit2DecimalPlace = "2"
                    RepName = RepName.Replace("With1Unit", "WithAmount")
                End If
            ElseIf IsUnitQty + IsUnitMeasure + IsUnitAmount = 1 Then
                RepName = RepName + "_With1Unit"
                If IsUnitQty = 1 Then
                    Unit1Desc = "'Unit'" : BalUnit1 = "IFNull(VOrd.OrdQty,0)-IFNull(VRec.RecQty,0)-IFNull(VRec.LossQty,0)"
                    Unit1Head = "'Qty'" : Unit1 = "VOrd.Unit" : Unit1DecimalPlace = "U.DecimalPlaces"
                End If

                If IsUnitMeasure = 1 Then
                    Unit1Desc = "'Unit'" : BalUnit1 = "(IFNull(VOrd.OrdMeasure,0) - IFNull(VRec.RecQty,0))"
                    Unit1Head = "'Measure'" : Unit1 = "VOrd.MeasureUnit" : Unit1DecimalPlace = "UM.DecimalPlaces"
                End If

                If IsUnitAmount = 1 Then
                    'StrAmtPerQty = "( WOD." & Replace(ReportFrm.FGetCode(4), "'", "") & "/ CASE WHEN IFNull(WOD.Qty,0) = 0 THEN 1 ELSE IFNull(WOD.Qty,0) END )"
                    BalUnit1 = "IFNull(VOrd.OrdAmount,0) - IFNull(VRec.RecAmount,0)"
                    Unit1Desc = "'Currency'" : Unit1Head = "'" & ReportFrm.FGetText(7) & "'" : Unit1 = "C.Description" : Unit1DecimalPlace = "2"
                    RepName = RepName.Replace("With1Unit", "WithAmount")
                End If
            End If

            If ReportFrm.FGetCode(5) IsNot Nothing Then
                If ReportFrm.FGetCode(5).ToString.Contains("Lot No") = True Then IsGroupOnLotNo = 1
                If ReportFrm.FGetCode(5).ToString.Contains(AgTemplate.ClsMain.FGetDimension1Caption) = True Then IsGroupOnDimension1 = 1
                If ReportFrm.FGetCode(5).ToString.Contains(AgTemplate.ClsMain.FGetDimension2Caption) = True Then IsGroupOnDimension2 = 1
            End If
            If IsGroupOnLotNo + IsGroupOnDimension1 + IsGroupOnDimension2 = 3 Then
                RepName = RepName + "_With3Dimensions"
                mShowForValue1 = "VOrd.LotNo" : mShowForHead1 = "'Lot No'"
                mShowForValue2 = "D1.Description" : mShowForHead2 = "'" & AgTemplate.ClsMain.FGetDimension1Caption() & "'"
                mShowForValue3 = "D2.Description" : mShowForHead3 = "'" & AgTemplate.ClsMain.FGetDimension2Caption() & "'"
            ElseIf IsGroupOnLotNo + IsGroupOnDimension1 + IsGroupOnDimension2 = 2 Then
                RepName = RepName + "_With2Dimensions"
                mShowForValue1 = "D1.Description" : mShowForHead1 = "'" & AgTemplate.ClsMain.FGetDimension1Caption() & "'"
                mShowForValue2 = "D2.Description" : mShowForHead2 = "'" & AgTemplate.ClsMain.FGetDimension2Caption() & "'"
            ElseIf IsGroupOnLotNo + IsGroupOnDimension1 + IsGroupOnDimension2 = 1 Then
                ' RepName = RepName + "_With1Dimensions"
                RepName = "Report_UnderConstruction"
                If IsGroupOnDimension1 = 1 Then mShowForValue1 = "D1.Description" : mShowForHead1 = "'" & AgTemplate.ClsMain.FGetDimension1Caption() & "'"
                If IsGroupOnDimension2 = 1 Then mShowForValue1 = "D2.Description" : mShowForHead1 = "'" & AgTemplate.ClsMain.FGetDimension2Caption() & "'"
            End If


            If ReportFrm.FGetText(8).ToString.Contains(",") Or ReportFrm.FGetText(8) = "All" Then
                If ReportFrm.FGetText(3) = "Worker Wise Outstanding Report" Then
                    RepTitle = "Worker Wise Outstanding Report"
                    'RepName = "Trade_JobWorkerWiseOutstandingReport"
                ElseIf ReportFrm.FGetText(3) = "With Barcode" Then
                    RepTitle = "Process Balance Report (With Barcode)"
                    ' RepName = "Trade_ProcessBalanceReport_WithBarcode"
                Else
                    RepTitle = "Process Balance Report"
                End If
                IsMultiProcess = 1
            Else
                If ReportFrm.FGetText(3) = "Worker Wise Outstanding Report" Then
                    RepTitle = ReportFrm.FGetText(8) & " Worker Wise Outstanding Report"
                    'RepName = "Trade_JobWorkerWiseOutstandingReport"
                ElseIf ReportFrm.FGetText(3) = "With Barcode" Then
                    RepTitle = ReportFrm.FGetText(8) & " Balance Report (With Barcode)"
                    'RepName = "Trade_ProcessBalanceReport_WithBarcode"
                Else
                    RepTitle = ReportFrm.FGetText(8) & " Balance Report"
                End If
                IsMultiProcess = 0
            End If

            mCondStr = mCondStr & " Where H.Site_Code = '" & AgL.PubSiteCode & "' "
            mCondStr = mCondStr & " And H.Div_Code= '" & AgL.PubDivCode & "' "
            'mCondStr = mCondStr & " And I.Div_Code= '" & AgL.PubDivCode & "' "

            If AgL.StrCmp(ReportFrm.FGetText(0), "") Then
                mCondStr = mCondStr & " And H.V_Date <= " & AgL.Chk_Text(ReportFrm.FGetText(1)) & " "
            Else
                mCondStr = mCondStr & " And H.V_Date Between " & AgL.Chk_Text(ReportFrm.FGetText(0)) & " And " & AgL.Chk_Text(ReportFrm.FGetText(1)) & " "
            End If

            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.V_Type", 6)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.Process", 8)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.JobWorker", 9)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.DocId", 10)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Item", 11)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemGroup", 12)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemCategory", 13)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemType", 14)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Dimension1", 17)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Dimension2", 18)


            If ReportFrm.FGetText(15) <> "" And ReportFrm.FGetText(15) <> "All" Then
                mQry = " Select '''' +  replace(ItemList,',',''',''')  + ''''  From ItemReportingGroup Where 1=1 " & ReportFrm.GetWhereCondition("Code", 15)
                mCondStr = mCondStr & " AND I.Code In (" & AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar & ")"
            End If


            If ReportFrm.FGetText(3) = "Worker Wise Outstanding Report" Then
                Dim mTmpTblBalance$ = "#" + AgL.GetGUID(AgL.GCn).ToString

                Dim mStrQry1$ = ""
                mStrQry1 = " SELECT VOrd.JobWorker, SG.DispName AS WorkerName, P.Description AS ProcessDesc, P.Sr AS ProcessSr, VOrd.Process, VOrd.VDate AS V_Date, " &
                        " IFNull(VOrd.OrdQty,0)-IFNull(VRec.RecQty,0)-IFNull(VRec.LossQty,0) AS BalQty " &
                        " Into [" & mTmpTblBalance & "]" &
                        " FROM " &
                        " ( " &
                        " SELECT L.JobOrder, L.JobOrderSr, Max(H.ManualRefNo) AS ManualRefNo, Max(L.Item) AS Item, Max(L.Unit) AS Unit, Max(L.MeasureUnit) AS MeasureUnit, " &
                        " Max(H.JobWorker) AS JobWorker, Max(H.Process) AS Process, Max(H.DueDate) AS DueDate, Max(H.V_Date) AS VDate, " &
                        " sum(L.Qty) AS OrdQty, sum(L.TotalMeasure) AS OrdMeasure, SUM(L." & Replace(ReportFrm.FGetCode(7), "'", "") & ") AS OrdAmount " &
                        " FROM JobOrderDetail L  " &
                        " LEFT JOIN JobOrder H  ON H.DocID = L.JobOrder " &
                        " LEFT JOIN Item I  ON I.Code  = L.Item " &
                        " " & mCondStr & " " &
                        " GROUP BY L.JobOrder, L.JobOrderSr " &
                        " ) VOrd " &
                        " LEFT JOIN " &
                        " ( " &
                        " SELECT L.JobOrder, L.JobOrderSr, Max(JIR.V_Date) AS MaxRecDate, " &
                        " sum(L.Qty) AS RecQty, sum(L.LossQty) AS LossQty, sum(L.TotalMeasure) AS RecMeasure  " &
                        " FROM JobReceiveDetail L  " &
                        " LEFT JOIN JobIssRec JIR  ON JIR.DocId = L.DocId " &
                        " Where JIR.V_Date <= " & AgL.Chk_Text(ReportFrm.FGetText(2)) & " " &
                        " GROUP BY L.JobOrder, L.JobOrderSr " &
                        " ) VRec ON VRec.JobOrder = VOrd.JobOrder AND VRec.JobOrderSr = VOrd.JobOrderSr " &
                        " LEFT JOIN SubGroup SG  ON SG.SubCode = VOrd.JobWorker " &
                        " LEFT JOIN Process P  ON P.NCat = VOrd.Process " &
                        " Where round(IFNull(VOrd.OrdQty,0)-IFNull(VRec.RecQty,0)-IFNull(VRec.LossQty,0),4) > 0 "
                AgL.Dman_ExecuteNonQry(mStrQry1, AgL.GCn)

                mOrderBarcode = " SELECT S.SubCode, P.NCat As Process, " &
                            " (SELECT Convert(NVARCHAR,H1.V_Date,3) + ', ' " &
                            " FROM  [" & mTmpTblBalance & "] H1 Where 1=1 And H1.JobWorker = S.SubCode And H1.Process = P.NCat  " &
                            " GROUP BY H1.JobWorker, H1.Process, H1.V_Date " &
                            " Having Sum(H1.BalQty) > 0 " &
                            " FOR XML Path ('')) AS DateList FROM SubGroup S, Process P  "

                Dim mStrQry2$ = ""
                mStrQry2 = " SELECT VOrd.JobWorker, SG.DispName AS WorkerName, P.Description AS ProcessDesc, P.Sr AS ProcessSr, VOrd.Process, VOrd.VDate AS V_Date, Sg.Phone As Mobile, " &
                            " " & BalUnit1 & " AS Field1Value, " & Unit1DecimalPlace & " AS Field1DecimalPlaces, " & Unit1 & " AS Field1Unit, " & Unit1Head & " AS Field1Head, " &
                            " " & BalUnit2 & " AS Field2Value, " & Unit2DecimalPlace & " AS Field2DecimalPlaces, " & Unit2 & " AS Field2Unit, " & Unit2Head & " AS Field2Head " &
                            " FROM " &
                            " ( " &
                            " SELECT L.JobOrder, L.JobOrderSr, Max(H.ManualRefNo) AS ManualRefNo, Max(L.Item) AS Item, Max(L.Unit) AS Unit, Max(L.MeasureUnit) AS MeasureUnit, " &
                            " Max(H.JobWorker) AS JobWorker, Max(H.Process) AS Process, Max(H.DueDate) AS DueDate, Max(H.V_Date) AS VDate, " &
                            " sum(L.Qty) AS OrdQty, sum(L.TotalMeasure) AS OrdMeasure, SUM(L." & Replace(ReportFrm.FGetCode(7), "'", "") & ") AS OrdAmount " &
                            " FROM JobOrderDetail L  " &
                            " LEFT JOIN JobOrder H  ON H.DocID = L.JobOrder " &
                            " LEFT JOIN Item I  ON I.Code  = L.Item " &
                            " " & mCondStr & " " &
                            " GROUP BY L.JobOrder, L.JobOrderSr " &
                            " ) VOrd " &
                            " LEFT JOIN " &
                            " ( " &
                            " SELECT L.JobOrder, L.JobOrderSr, Max(JIR.V_Date) AS MaxRecDate, " &
                            " sum(L.Qty) AS RecQty, sum(L.LossQty) AS LossQty, sum(L.TotalMeasure) AS RecMeasure  " &
                            " FROM JobReceiveDetail L  " &
                            " LEFT JOIN JobIssRec JIR  ON JIR.DocId = L.DocId " &
                            " Where JIR.V_Date <= " & AgL.Chk_Text(ReportFrm.FGetText(2)) & " " &
                            " GROUP BY L.JobOrder, L.JobOrderSr " &
                            " ) VRec ON VRec.JobOrder = VOrd.JobOrder AND VRec.JobOrderSr = VOrd.JobOrderSr " &
                            " LEFT JOIN SubGroup SG  ON SG.SubCode = VOrd.JobWorker " &
                            " LEFT JOIN Process P  ON P.NCat = VOrd.Process " &
                            " LEFT JOIN Unit U  ON U.Code = VOrd.Unit " &
                            " LEFT JOIN Unit UM  ON UM.Code = VOrd.MeasureUnit " &
                            " Where round(IFNull(VOrd.OrdQty,0)-IFNull(VRec.RecQty,0)-IFNull(VRec.LossQty,0),4) > 0 "

                mQry = " SELECT VBal.JobWorker, VBal.Process, Max(VBal.WorkerName) As WorkerName, Max(VBal.Mobile) As Mobile, Max(VBal.ProcessDesc) As ProcessDesc,  " & IsMultiProcess & " As IsMultiProcess, Max(VBal.ProcessSr) As ProcessSr,  " &
                        " Sum(IFNull(VBal.Field1Value,0)) AS Field1Value, Max(IFNull(VBal.Field1DecimalPlaces,0)) AS Field1DecimalPlaces, Max(IFNull(VBal.Field1Unit,'')) AS Field1Unit, Max(IFNull(VBal.Field1Head,'')) AS Field1Head, " &
                        " Sum(IFNull(VBal.Field2Value,0)) AS Field2Value, Max(IFNull(VBal.Field2DecimalPlaces,0)) AS Field2DecimalPlaces, Max(IFNull(VBal.Field2Unit,'')) AS Field2Unit , Max(IFNull(VBal.Field2Head,'')) AS Field2Head, " &
                        " Max(VOrd.DateList) As OrdList " &
                        " FROM  (" & mStrQry2 & ") VBal  " &
                        " LEFT JOIN (" & mOrderBarcode & ") VOrd On VBal.JobWorker = VOrd.SubCode And VBal.Process = VOrd.Process " &
                        " GROUP BY VBal.JobWorker, VBal.Process "

            ElseIf ReportFrm.FGetText(3) = "With Barcode" Then
                Dim mTmpTblBalance$ = "#" + AgL.GetGUID(AgL.GCn).ToString

                Dim mStrQry1$ = ""

                RepName = "Trade_ProcessBalanceReport_WithBarcode"
                mStrQry1 = " SELECT VOrd.JobWorker, SG.DispName AS WorkerName, P.Description AS ProcessDesc, P.Sr AS ProcessSr, VOrd.Process, VOrd.VDate AS V_Date, VOrd.Item_UID AS Item_UID, " &
                        " VOrd.JobOrder, VOrd.ManualrefNo AS OrderNo,  VOrd.ItemDesc AS ItemDesc, VOrd.Item AS Item, IFNull(VOrd.OrdQty,0)-IFNull(VRec.RecQty,0)-IFNull(VRec.LossQty,0) AS BalQty " &
                        " Into [" & mTmpTblBalance & "]" &
                        " FROM " &
                        " ( " &
                        " SELECT L.JobOrder, L.JobOrderSr, Max(H.ManualRefNo) AS ManualRefNo, Max(L.Item) AS Item, Max(I.Description) AS ItemDesc, Max(L.Unit) AS Unit, Max(L.MeasureUnit) AS MeasureUnit, " &
                        " Max(H.JobWorker) AS JobWorker, Max(H.Process) AS Process, Max(H.DueDate) AS DueDate, Max(H.V_Date) AS VDate, Max(IU.Item_UID) AS Item_UID, " &
                        " sum(L.Qty) AS OrdQty, sum(L.TotalMeasure) AS OrdMeasure, SUM(L." & Replace(ReportFrm.FGetCode(7), "'", "") & ") AS OrdAmount " &
                        " FROM JobOrderDetail L  " &
                        " LEFT JOIN JobOrder H  ON H.DocID = L.JobOrder " &
                        " LEFT JOIN Item I  ON I.Code  = L.Item " &
                        " LEFT JOIN Item_UID IU  ON IU.Code  = L.Item_UID " &
                        " " & mCondStr & " " &
                        " GROUP BY L.JobOrder, L.JobOrderSr " &
                        " ) VOrd " &
                        " LEFT JOIN " &
                        " ( " &
                        " SELECT L.JobOrder, L.JobOrderSr, Max(JIR.V_Date) AS MaxRecDate, " &
                        " sum(L.Qty) AS RecQty, sum(L.LossQty) AS LossQty, sum(L.TotalMeasure) AS RecMeasure  " &
                        " FROM JobReceiveDetail L   " &
                        " LEFT JOIN JobIssRec JIR  ON JIR.DocId = L.DocId " &
                        " Where JIR.V_Date <= " & AgL.Chk_Text(ReportFrm.FGetText(2)) & " " &
                        " GROUP BY L.JobOrder, L.JobOrderSr " &
                        " ) VRec ON VRec.JobOrder = VOrd.JobOrder AND VRec.JobOrderSr = VOrd.JobOrderSr " &
                        " LEFT JOIN SubGroup SG  ON SG.SubCode = VOrd.JobWorker " &
                        " LEFT JOIN Process P  ON P.NCat = VOrd.Process " &
                        " Where round(IFNull(VOrd.OrdQty,0)-IFNull(VRec.RecQty,0)-IFNull(VRec.LossQty,0),4) > 0 "
                AgL.Dman_ExecuteNonQry(mStrQry1, AgL.GCn)

                mOrderBarcode = " SELECT S.SubCode, P.NCat As Process, JO.DocId AS JobOrder, I.Code AS Item, " &
                            " (SELECT H1.Item_UID + ', ' " &
                            " FROM  [" & mTmpTblBalance & "] H1 Where 1=1 And H1.JobWorker = S.SubCode And H1.Process = P.NCat And H1.JobOrder = JO.DocId And H1.Item = I.Code " &
                            " GROUP BY H1.JobWorker, H1.Process, H1.V_Date, H1.Item_UID " &
                            " Having Sum(H1.BalQty) > 0 " &
                            " FOR XML Path ('')) AS BarcodeList FROM SubGroup S, Process P, JobOrder JO, Item I "

                Dim mStrQry2$ = ""
                mStrQry2 = " SELECT VOrd.JobWorker, VOrd.JobOrder, VOrd.ManualRefNo AS OrderNo, VOrd.VDate AS OrderDate, SG.DispName AS WorkerName, P.Description AS ProcessDesc, P.Sr AS ProcessSr, VOrd.Process, VOrd.Item, VOrd.ItemDesc, VOrd.VDate AS V_Date, " &
                            " " & BalUnit1 & " AS Field1Value, " & Unit1DecimalPlace & " AS Field1DecimalPlaces, " & Unit1 & " AS Field1Unit, " & Unit1Head & " AS Field1Head, " &
                            " " & BalUnit2 & " AS Field2Value, " & Unit2DecimalPlace & " AS Field2DecimalPlaces, " & Unit2 & " AS Field2Unit, " & Unit2Head & " AS Field2Head " &
                            " FROM " &
                            " ( " &
                            " SELECT L.JobOrder, L.JobOrderSr, Max(H.ManualRefNo) AS ManualRefNo, Max(L.Item) AS Item, Max(I.Description) AS ItemDesc, Max(L.Unit) AS Unit, Max(L.MeasureUnit) AS MeasureUnit, " &
                            " Max(H.JobWorker) AS JobWorker, Max(H.Process) AS Process, Max(H.DueDate) AS DueDate, Max(H.V_Date) AS VDate, " &
                            " sum(L.Qty) AS OrdQty, sum(L.TotalMeasure) AS OrdMeasure, SUM(L." & Replace(ReportFrm.FGetCode(7), "'", "") & ") AS OrdAmount " &
                            " FROM JobOrderDetail L  " &
                            " LEFT JOIN JobOrder H  ON H.DocID = L.JobOrder " &
                            " LEFT JOIN Item I  ON I.Code  = L.Item " &
                            " " & mCondStr & " " &
                            " GROUP BY L.JobOrder, L.JobOrderSr " &
                            " ) VOrd " &
                            " LEFT JOIN " &
                            " ( " &
                            " SELECT L.JobOrder, L.JobOrderSr, Max(JIR.V_Date) AS MaxRecDate, " &
                            " sum(L.Qty) AS RecQty, sum(L.LossQty) AS LossQty, sum(L.TotalMeasure) AS RecMeasure  " &
                            " FROM JobReceiveDetail L  " &
                            " LEFT JOIN JobIssRec JIR  ON JIR.DocId = L.DocId " &
                            " Where JIR.V_Date <= " & AgL.Chk_Text(ReportFrm.FGetText(2)) & " " &
                            " GROUP BY L.JobOrder, L.JobOrderSr " &
                            " ) VRec ON VRec.JobOrder = VOrd.JobOrder AND VRec.JobOrderSr = VOrd.JobOrderSr " &
                            " LEFT JOIN SubGroup SG  ON SG.SubCode = VOrd.JobWorker " &
                            " LEFT JOIN Process P  ON P.NCat = VOrd.Process " &
                            " LEFT JOIN Unit U  ON U.Code = VOrd.Unit " &
                            " LEFT JOIN Unit UM  ON UM.Code = VOrd.MeasureUnit " &
                            " Where round(IFNull(VOrd.OrdQty,0)-IFNull(VRec.RecQty,0)-IFNull(VRec.LossQty,0),4) > 0 "

                mQry = " SELECT VBal.JobWorker, VBal.Process, Max(VBal.OrderNo) AS OrderNo, Max(VBal.OrderDate) AS OrderDate , Max(VBal.WorkerName) As WorkerName, Max(VBal.ProcessDesc) As ProcessDesc,  " & IsMultiProcess & " As IsMultiProcess, Max(VBal.ProcessSr) As ProcessSr,  VBal.ItemDesc, " &
                        " Sum(IFNull(VBal.Field1Value,0)) AS Field1Value, Max(IFNull(VBal.Field1DecimalPlaces,0)) AS Field1DecimalPlaces, Max(IFNull(VBal.Field1Unit,'')) AS Field1Unit, Max(IFNull(VBal.Field1Head,'')) AS Field1Head, " &
                        " Sum(IFNull(VBal.Field2Value,0)) AS Field2Value, Max(IFNull(VBal.Field2DecimalPlaces,0)) AS Field2DecimalPlaces, Max(IFNull(VBal.Field2Unit,'')) AS Field2Unit , Max(IFNull(VBal.Field2Head,'')) AS Field2Head, " &
                        " Max(VOrd.BarcodeList) As OrdList " &
                        " FROM  (" & mStrQry2 & ") VBal  " &
                        " LEFT JOIN (" & mOrderBarcode & ") VOrd On VBal.JobWorker = VOrd.SubCode And VBal.Process = VOrd.Process And VBal.JobOrder = VOrd.JobOrder And VBal.Item = VOrd.Item " &
                        " GROUP BY VBal.JobWorker, VBal.Process, VBal.JobOrder, VBal.ItemDesc "
            Else
                mQry = " SELECT VOrd.JobOrder, VOrd.JobOrderSr, VOrd.DueDate, VOrd.Unit, VOrd.MeasureUnit, P.Sr As ProcessSr, VOrd.ManualRefNo, VOrd.VDate, " &
                        " SG.DispName AS JobWorkerName, P.Description AS ProcessDesc, I.Description AS ItemDesc, " & IsMultiProcess & " As IsMultiProcess, " &
                        " IFNull(VOrd.OrdQty,0) AS OrdQty, IFNull(VOrd.OrdMeasure,0) AS OrdMeasure, " &
                        " IFNull(VRec.RecQty,0) AS RecQty, IFNull(VRec.RecMeasure,0) AS RecMeasure, " &
                        " " & GroupOn & " AS GroupOn, " & GroupOnValue & " AS GroupOnValue, " &
                        " " & BalUnit1 & " AS BalUnit1, " & Unit1DecimalPlace & " AS Unit1DecimalPlace, " & Unit1 & " AS Unit1, " & Unit1Head & " AS Unit1Head, " &
                        " " & BalUnit2 & " AS BalUnit2, " & Unit2DecimalPlace & " AS Unit2DecimalPlace, " & Unit2 & " AS Unit2, " & Unit2Head & " AS Unit2Head, " &
                        " " & mShowForHead1 & " AS mShowForHead1, " & mShowForValue1 & " AS mShowForValue1, " & Unit1Desc & " AS Unit1Desc, " &
                        " " & mShowForHead2 & " AS mShowForHead2, " & mShowForValue2 & " AS mShowForValue2, " & Unit2Desc & " AS Unit2Desc, " &
                        " " & mShowForHead3 & " AS mShowForHead3, " & mShowForValue3 & " AS mShowForValue3, " &
                        " CASE WHEN IFNull(VOrd.OrdQty,0)-IFNull(VRec.RecQty,0)-IFNull(VRec.LossQty,0) > 0 THEN  datediff(Day,VOrd.DueDate,'" & ReportFrm.FGetText(2) & "') ELSE datediff(Day,VOrd.DueDate,VRec.MaxRecDate) END AS Ageing " &
                        " FROM " &
                        " ( " &
                        " SELECT L.JobOrder, L.JobOrderSr, Max(H.ManualRefNo) AS ManualRefNo, Max(L.Item) AS Item, Max(L.LotNo) AS LotNo,Max(L.Unit) AS Unit, Max(L.MeasureUnit) AS MeasureUnit, " &
                        " Max(H.JobWorker) AS JobWorker, Max(H.Process) AS Process, Max(H.DueDate) AS DueDate, Max(H.V_Date) AS VDate, " &
                        " sum(L.Qty) AS OrdQty, sum(L.TotalMeasure) AS OrdMeasure, SUM(L." & Replace(ReportFrm.FGetCode(7), "'", "") & ") AS OrdAmount " &
                        " FROM JobOrderDetail L  " &
                        " LEFT JOIN JobOrder H  ON H.DocID = L.JobOrder " &
                        " LEFT JOIN Item I  ON I.Code  = L.Item " &
                        " " & mCondStr & " " &
                        " GROUP BY L.JobOrder, L.JobOrderSr " &
                        " ) VOrd " &
                        " LEFT JOIN " &
                        " ( " &
                        " SELECT L.JobOrder, L.JobOrderSr, Max(JIR.V_Date) AS MaxRecDate, " &
                        " sum(L.Qty) AS RecQty, sum(L.LossQty) AS LossQty, sum(L.TotalMeasure) AS RecMeasure  " &
                        " FROM JobReceiveDetail L  " &
                        " LEFT JOIN JobIssRec JIR  ON JIR.DocId = L.DocId " &
                        " Where JIR.V_Date <= " & AgL.Chk_Text(ReportFrm.FGetText(2)) & " " &
                        " GROUP BY L.JobOrder, L.JobOrderSr " &
                        " ) VRec ON VRec.JobOrder = VOrd.JobOrder AND VRec.JobOrderSr = VOrd.JobOrderSr " &
                        " LEFT JOIN JobOrderDetail JOD  ON JOD.DocID = VOrd.JobOrder  AND JOD.Sr = VOrd.JobOrderSr  " &
                        " LEFT JOIN SubGroup SG  ON SG.SubCode = VOrd.JobWorker " &
                        " LEFT JOIN Process P  ON P.NCat = VOrd.Process " &
                        " LEFT JOIN Item I  ON I.Code  = VOrd.Item " &
                        " LEFT JOIN Unit U  ON U.Code = VOrd.Unit " &
                        " LEFT JOIN Unit UM  ON UM.Code = VOrd.MeasureUnit " &
                        " LEFT JOIN Dimension1 D1  ON D1.Code = JOD.Dimension1  " &
                        " LEFT JOIN Dimension2 D2  ON D2.Code = JOD.Dimension2  " &
                        " Where round(IFNull(VOrd.OrdQty,0)-IFNull(VRec.RecQty,0)-IFNull(VRec.LossQty,0),4) > 0 "

                '                " LEFT JOIN Rug_CarpetSKU CS ON I.Code = CS.Code " & _
                '" LEFT JOIN Rug_Design D ON D.Code = CS.Design " & _
            End If

            DsRep = AgL.FillData(mQry, AgL.GCn)
            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records to Print!")
            ReportFrm.PrintReport(DsRep, RepName, RepTitle, ReportPath)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub
#End Region

#Region "Material Issue From Job Order Report"
    Private Sub ProcMaterialIssueFromJobOrderReport()
        Try
            Dim IsMultiProcess As Integer = 0
            Dim IsProcessinNewPage As Integer = 0
            Dim mProcessName As String = ""
            Dim mCondStr$ = ""

            If ReportFrm.FGetText(3).ToString.Contains(",") Or ReportFrm.FGetText(3) = "All" Then
                mProcessName = "Process"
                IsMultiProcess = 1
            Else
                mProcessName = ReportFrm.FGetText(3)
                IsMultiProcess = 0
            End If

            If ReportFrm.FGetText(14) = "Yes" Then
                IsProcessinNewPage = 1
            Else
                IsProcessinNewPage = 0
            End If

            RepTitle = "Material Issue For " & mProcessName
            OrderByStr = "Order By P.Sr, H.V_Date, H.V_No "
            RepName = "Production_MaterialIssueFromJobOrderReport"


            mCondStr = mCondStr & " AND H.V_Date Between '" & ReportFrm.FGetText(0) & "' And '" & ReportFrm.FGetText(1) & "' "
            mCondStr = mCondStr & " AND IFNull(L.Qty,0) <> 0 "

            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.V_Type", 2)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.Process", 3)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.JobWorker", 4)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.DocId", 5)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Item", 6)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemGroup", 7)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemCategory", 8)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemType", 9)

            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.LotNo", 11)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Dimension1", 12)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Dimension2", 13)

            If ReportFrm.FGetText(10) <> "" And ReportFrm.FGetText(10) <> "All" Then
                mQry = " Select '''' +  replace(ItemList,',',''',''')  + ''''  From ItemReportingGroup Where 1=1 " & ReportFrm.GetWhereCondition("Code", 10)
                mCondStr = mCondStr & " AND I.Code In (" & AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar & ")"
            End If

            mCondStr = mCondStr & " And H.Div_Code = '" & AgL.PubDivCode & "' "
            mCondStr = mCondStr & " And H.Site_Code = '" & AgL.PubSiteCode & "' "


            mQry = "SELECT H.DocID, H.V_Type, H.V_Date, H.ManualRefNo, H.Process, H.JobWorker, L.Qty, L.LotNo, P.Sr AS ProcessSr, P.Description AS ProcessDesc, " &
                    " SG.DispName AS JobWorkerName, I.Description AS ItemDesc, L.Sr, FP.Description AS FromProcessDesc, " &
                    " E.Caption_Dimension1, E.Caption_Dimension2, D1.Description AS D1Desc,D2.Description AS D2Desc," &
                    " " & IsMultiProcess & " As IsMultiProcess, " & IsProcessinNewPage & " As IsProcessinNewPage " &
                    " FROM JobIssRec H " &
                    " LEFT JOIN Voucher_Type Vt ON Vt.V_Type = H.V_Type  " &
                    " LEFT JOIN JobIssueDetail L ON L.DocId = H.DocID  " &
                    " LEFT JOIN SubGroup SG ON SG.SubCode = H.JobWorker " &
                    " LEFT JOIN Item I ON I.Code = L.Item  " &
                    " LEFT JOIN Process P On P.NCat = H.Process " &
                    " LEFT JOIN Process FP On FP.NCat = L.PrevProcess " &
                    " LEFT JOIN Enviro E ON E.Site_Code = H.Site_Code AND E.Div_Code = H.Div_Code " &
                    " LEFT JOIN Dimension1 D1 ON D1.Code = L.Dimension1 " &
                    " LEFT JOIN Dimension2 D2 ON D2.Code = L.Dimension2 " &
                    " WHERE 1=1 " & mCondStr
            DsRep = AgL.FillData(mQry, AgL.GCn)

            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records to Print!")

            ReportFrm.PrintReport(DsRep, RepName, RepTitle, ReportPath)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub
#End Region

#Region "Job QC Report"
    Private Sub ProcJobQCReport()
        Try
            Dim mCondStr$ = ""

            Dim IsMultiProcess As Integer = 0
            Dim mProcessName As String = ""

            RepName = "Production_JobQCReport"
            RepTitle = "Job QC Report"

            If ReportFrm.FGetText(3).ToString.Contains(",") Or ReportFrm.FGetText(3) = "All" Then
                mProcessName = "Process"
                IsMultiProcess = 1
            Else
                mProcessName = ReportFrm.FGetText(3)
                IsMultiProcess = 0
            End If

            mCondStr = " Where 1 = 1 "
            mCondStr = mCondStr & " AND H.V_Date Between '" & ReportFrm.FGetText(0) & "' And '" & ReportFrm.FGetText(1) & "' "

            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.Process", 2)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.JobWorker", 3)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.JobReceive", 4)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("Jrd.Item", 5)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemGroup", 6)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemCategory", 7)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemType", 8)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Dimension1", 10)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Dimension2", 11)

            If ReportFrm.FGetText(9) <> "" And ReportFrm.FGetText(9) <> "All" Then
                mQry = " Select '''' +  replace(ItemList,',',''',''')  + ''''  From ItemReportingGroup Where 1=1 " & ReportFrm.GetWhereCondition("Code", 9)
                mCondStr = mCondStr & " AND I.Code In (" & AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar & ")"
            End If

            mCondStr = mCondStr & " And H.Div_Code = '" & AgL.PubDivCode & "' "
            mCondStr = mCondStr & " And H.Site_Code = '" & AgL.PubSiteCode & "' "


            mQry = " SELECT H.DocID, H.V_Type, H.V_Date, H.Remarks, H.ManualrefNo, " &
                    " L.Sr, L.QCQty, L.CheckedQty, L.PassedQty, L.Remarks AS LineRemark, E.Caption_Dimension1, E.Caption_Dimension2, " &
                    " Sg1.DispName As JobWorkerName, Sg2.DispName As QcByName, Sg3.DispName As PartyName, P.Description As ProcessDesc,  " &
                    " Jrd.Item, I.Description As ItemDesc, I.Unit, S.V_Type + '-' +  S.ManualRefNo As JobReceiveRefNo, " &
                    " U.DecimalPlaces As UnitDecimalPlaces,  D1.Description As D1Desc, D2.Description As D2Desc " &
                    " FROM JobQc H " &
                    " LEFT JOIN SubGroup Sg1 ON H.JobWorker = Sg1.SubCode " &
                    " LEFT JOIN SubGroup Sg2 On H.QCBy = Sg2.SubCode " &
                    " LEFT JOIN SubGroup Sg3 On H.Party = Sg3.SubCode " &
                    " LEFT JOIN Process P ON H.Process = P.NCat " &
                    " LEFT JOIN Enviro E ON E.Site_Code = H.Site_Code AND E.Div_Code = H.Div_Code " &
                    " LEFT JOIN JobQcDetail L ON L.DocId = H.DocID " &
                    " LEFT JOIN JobIssRec S  On L.JobReceive = S.DocId " &
                    " LEFT JOIN JobReceiveDetail Jrd  ON L.JobReceive = Jrd.DocId ANd L.JobReceiveSr = Jrd.Sr " &
                    " LEFT JOIN Item I  ON Jrd.Item = I.Code " &
                    " LEFT JOIN Unit U  On I.Unit = U.Code " &
                    " Left Join Dimension1 D1   On L.Dimension1 = D1.Code " &
                    " Left Join Dimension2 D2   On L.Dimension2 = D2.Code " & mCondStr
            DsRep = AgL.FillData(mQry, AgL.GCn)

            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records to Print!")

            ReportFrm.PrintReport(DsRep, RepName, RepTitle, ReportPath)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub
#End Region

#Region "Periodic Job Order Status"
    Private Sub ProcPeriodicJobOrderStatus()
        Dim mCondStr$ = ""
        Dim StrQry As String = ""
        Dim GroupOn As String = "", GroupOnTitle As String = ""

        Try

            RepName = "Production_PeriodicJobOrderStatus"
            mCondStr = mCondStr & " AND H.Site_Code = " & AgL.Chk_Text(AgL.PubSiteCode) & " AND H.Div_Code = " & AgL.Chk_Text(AgL.PubDivCode) & " "
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.Process", 3)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.V_Type", 4)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.JobWorker", 5)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Item", 6)


            If ReportFrm.FGetText(2) = "Item Wise" Then
                GroupOn = "L.Item" : GroupOnTitle = "'Item'"
            Else
                GroupOn = "H.JobWorker" : GroupOnTitle = "'Job Worker'"
            End If

            If ReportFrm.FGetText(3).ToString.Contains(",") Or ReportFrm.FGetText(3) = "All" Then
                RepTitle = "Periodic Job Order Status"
            Else
                RepTitle = "Periodic " & ReportFrm.FGetText(3) & " Status"
            End If

            StrQry = " SELECT " & GroupOn & " AS GroupOn, sum(VOpen.BalQty) AS OpeningIndent, Max(VOpen.DecimalPlaces) AS DecimalPlaces, 0 AS NewIndent, 0 AS DispOpen ,0 AS DispNew  " &
                    " FROM " &
                    " ( " &
                    " SELECT VOrd.JobOrder, VOrd.JobOrderSr, VOrd.DecimalPlaces, VOrd.OrdQty, VDisp.DispQty, Round(IFNull(VOrd.OrdQty,0) - IFNull(VDisp.DispQty,0),4) AS BalQty " &
                    " FROM " &
                    " ( " &
                    " SELECT L.JobOrder, L.JobOrderSr, Max(U.DecimalPlaces) AS DecimalPlaces, sum(L.Qty) AS OrdQty  " &
                    " FROM JobOrderDetail L  " &
                    " LEFT JOIN JobOrder H ON H.DocID = L.JobOrder " &
                    " LEFT JOIN Unit U On U.Code = L.Unit " &
                    " WHERE H.V_Date < '" & ReportFrm.FGetText(0) & "' " &
                    " " & mCondStr & " " &
                    " GROUP BY L.JobOrder, L.JobOrderSr " &
                    " ) VOrd " &
                    " LEFT JOIN " &
                    " ( " &
                    " SELECT L.JobOrder, L.JobOrderSr, IFNull(sum(L.Qty),0)+IFNull(sum(L.LossQty),0) AS DispQty " &
                    " FROM JobReceiveDetail L " &
                    " LEFT JOIN JobIssRec H ON H.DocID = L.DocId " &
                    " WHERE H.V_Date < '" & ReportFrm.FGetText(0) & "' " &
                    " " & mCondStr & " " &
                    " GROUP BY L.JobOrder, L.JobOrderSr " &
                    " ) VDisp ON VOrd.JobOrder = VDisp.JobOrder AND VOrd.JobOrderSr = VDisp.JobOrderSr " &
                    " WHERE Round(IFNull(VOrd.OrdQty,0) - IFNull(VDisp.DispQty,0),4) > 0 " &
                    " ) VOpen " &
                    " LEFT JOIN JobOrder H ON H.DocID = VOpen.JobOrder " &
                    " LEFT JOIN JobOrderDetail L ON L.DocId = VOpen.JobOrder AND L.Sr = VOpen.JobOrderSr " &
                    " GROUP BY " & GroupOn & " " &
                    " UNION ALL  " &
                    " SELECT " & GroupOn & " AS GroupOn,  0 AS OpeningIndent, Max(U.DecimalPlaces) AS DecimalPlaces, sum(L.Qty) AS NewIndent  , 0 AS DispOpen ,0 AS DispNew   " &
                    " FROM JobOrderDetail L  " &
                    " LEFT JOIN JobOrder H ON H.DocID = L.DocID   " &
                    " LEFT JOIN Unit U On U.Code = L.Unit " &
                    " WHERE H.V_Date BETWEEN  '" & ReportFrm.FGetText(0) & "' AND '" & ReportFrm.FGetText(1) & "' " &
                    " " & mCondStr & " " &
                    " GROUP BY " & GroupOn & " " &
                    " UNION ALL " &
                    " SELECT " & GroupOn & " AS GroupOn, 0 AS OpeningIndent, Max(U.DecimalPlaces) AS DecimalPlaces, 0 AS NewIndent, IFNull(sum(L.Qty),0)+IFNull(sum(L.LossQty),0) AS DispOpen ,0 AS DispNew   " &
                    " FROM JobReceiveDetail L " &
                    " LEFT JOIN JobIssRec H ON H.DocID = L.DocId  " &
                    " LEFT JOIN JobOrder W ON W.DocID = L.JobOrder  " &
                    " LEFT JOIN Unit U On U.Code = L.Unit " &
                    " WHERE H.V_Date BETWEEN  '" & ReportFrm.FGetText(0) & "' AND '" & ReportFrm.FGetText(1) & "' " &
                    " AND W.V_Date < '" & ReportFrm.FGetText(0) & "' " &
                    " " & mCondStr & " " &
                    " GROUP BY " & GroupOn & " " &
                    " UNION ALL " &
                    " SELECT " & GroupOn & " AS GroupOn, 0 AS OpeningIndent, Max(U.DecimalPlaces) AS DecimalPlaces, 0 AS NewIndent, 0 AS DispOpen, IFNull(sum(L.Qty),0)+IFNull(sum(L.LossQty),0) AS DispNew   " &
                    " FROM JobReceiveDetail L " &
                    " LEFT JOIN JobIssRec H ON H.DocID = L.DocId  " &
                    " LEFT JOIN JobOrder W ON W.DocID = L.JobOrder  " &
                    " LEFT JOIN Unit U On U.Code = L.Unit " &
                    " WHERE H.V_Date BETWEEN  '" & ReportFrm.FGetText(0) & "' AND '" & ReportFrm.FGetText(1) & "' " &
                    " AND W.V_Date BETWEEN  '" & ReportFrm.FGetText(0) & "' AND '" & ReportFrm.FGetText(1) & "' " &
                    " " & mCondStr & " " &
                    " GROUP BY " & GroupOn & " "


            If ReportFrm.FGetText(2) = "Item Wise" Then
                mQry = " SELECT VMain.GroupOn, Max(I.Description) AS GroupOnDesc, Max(VMain.DecimalPlaces) AS DecimalPlaces, " & GroupOnTitle & " AS GroupOnTitle, SUM(VMain.OpeningIndent) AS OpeningIndent, SUM(VMain.NewIndent) AS NewIndent , Sum(VMain.DispOpen) AS DispOpen , sum(VMain.DispNew) AS DispNew, " &
                        " SUM(VMain.OpeningIndent)-Sum(VMain.DispOpen) AS OpenBal, SUM(VMain.NewIndent)- sum(VMain.DispNew) AS  NewBal, " &
                        " Case When SUM(VMain.OpeningIndent) <> 0 Then round((SUM(VMain.OpeningIndent)-Sum(VMain.DispOpen))*100/SUM(VMain.OpeningIndent),2) Else 0 End AS OpenBalPer, Case When SUM(VMain.NewIndent) <> 0 Then round((SUM(VMain.NewIndent)- sum(VMain.DispNew))*100/SUM(VMain.NewIndent),2) Else 0 END AS  NewBalPer " &
                        " FROM " &
                        " ( " & StrQry & "  ) VMain " &
                        " LEFT JOIN Item I ON I.Code = VMain.GroupOn " &
                        " GROUP BY VMain.GroupOn "
            Else
                mQry = " SELECT VMain.GroupOn, Max(SG.DispName) AS GroupOnDesc, Max(VMain.DecimalPlaces) AS DecimalPlaces, " & GroupOnTitle & " AS GroupOnTitle, SUM(VMain.OpeningIndent) AS OpeningIndent, SUM(VMain.NewIndent) AS NewIndent , Sum(VMain.DispOpen) AS DispOpen , sum(VMain.DispNew) AS DispNew, " &
                        " SUM(VMain.OpeningIndent)-Sum(VMain.DispOpen) AS OpenBal, SUM(VMain.NewIndent)- sum(VMain.DispNew) AS  NewBal, " &
                        " case When SUM(VMain.OpeningIndent) <> 0 Then round((SUM(VMain.OpeningIndent)-Sum(VMain.DispOpen))*100/SUM(VMain.OpeningIndent),2) Else 0 End AS OpenBalPer, Case When SUM(VMain.NewIndent) <> 0 Then round((SUM(VMain.NewIndent)- sum(VMain.DispNew))*100/SUM(VMain.NewIndent),2) Else 0 END AS  NewBalPer " &
                        " FROM " &
                        " ( " & StrQry & "  ) VMain " &
                        " LEFT JOIN SubGroup SG ON SG.SubCode = VMain.GroupOn " &
                        " GROUP BY VMain.GroupOn "
            End If

            DsRep = AgL.FillData(mQry, AgL.GCn)
            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records to Print!")


            ReportFrm.PrintReport(DsRep, RepName, RepTitle, ReportPath)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub
#End Region

#Region "Payment Calculation"
    Private Sub ProcPaymentCalculation()
        Dim IsProcessinNewPage As Integer = 0
        Dim IsMultiProcess As Integer = 0
        Dim mProcessName As String

        Try

            If ReportFrm.FGetText(2).ToString.Contains(",") Or ReportFrm.FGetText(2) = "All" Then
                mProcessName = "Process"
                IsMultiProcess = 1
            Else
                mProcessName = ReportFrm.FGetText(2)
                IsMultiProcess = 0
            End If

            If ReportFrm.FGetText(6) = "Yes" Then
                IsProcessinNewPage = 1
            Else
                IsProcessinNewPage = 0
            End If

            RepName = "Production_PaymentCalculation" : RepTitle = mProcessName & " Payment Calculation"


            Dim mCondStr$ = ""

            mCondStr += " AND H.V_Date Between " & AgL.Chk_Text(ReportFrm.FGetText(0)) & " And " & AgL.Chk_Text(ReportFrm.FGetText(1)) & " "
            mCondStr += " And H.Site_Code = '" & AgL.PubSiteCode & "' "
            mCondStr += " And H.Div_Code= '" & AgL.PubDivCode & "' "
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.Process", 2)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.V_Type", 3)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("IFNull(H.JobWorker,L.JobWorker)", 4)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Item", 5)

            mQry = " SELECT H.DocID, H.V_Type, JI.V_Date, JI.ManualRefNo, " &
                    " (Case When IsNumeric(Replace(H.ManualRefNo,'-',''))>0 Then Convert(Numeric,Replace(H.ManualRefNo,'-','')) Else 0 End) as ManualRefNoForOrderBy, " &
                    " H.Remarks, " & IsMultiProcess & " As IsMultiProcess, " & IsProcessinNewPage & " As IsProcessinNewPage, " &
                    " L.Sr, L.Item, L.Qty, IFNull(L.BillQty,0) AS BillQty, IFNull(L.BillMeasure,0) AS BillMeasure, L.TotalPerimeter, L.BillPerimeter, L.Unit, L.MeasurePerPcs, L.TotalMeasure, " &
                    " L.MeasureUnit, L.Rate, L.Amount, L.NetAmount, L.Remark AS LineRemark, SM.Name AS SiteName, U.DecimalPlaces, " &
                    " SG.Name AS JobWorkerName, SG.Add1, SG.Add2, SG.Add3, C.CityName, SG.Mobile,SG.Phone, SG.PAN,   " &
                    " P.Description AS ProcessDesc, P.Sr AS ProcessSr, G.Description AS GodownDesc,  I.Description AS ItemDesc, " &
                    " JR.V_Date AS JobReceiveDate, JR.ManualRefNo AS JobReciveNo, JOD.ProcessOnPerimeter AS ProcessOnPerimeter, " &
                    " IG.Description AS ItemGroup, L.Penalty, L.Incentive, L.NetAmount, L.JobOrder " &
                    " FROM JobInvoiceDetail L   " &
                    " LEFT JOIN JobInvoice H ON H.DocID = L.JobInvoice  " &
                    " LEFT JOIN JobInvoice JI ON JI.DocID = L.DocId " &
                    " LEFT JOIN SiteMast Sm ON H.Site_Code = Sm.Code " &
                    " LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type   " &
                    " LEFT JOIN SubGroup SG ON IFNull(H.JobWorker,L.JobWorker) = SG.SubCode   " &
                    " LEFT JOIN City C ON SG.CityCode = C.CityCode   " &
                    " LEFT JOIN Process P ON H.Process = P.NCat   " &
                    " LEFT JOIN Godown G ON H.Godown = G.Code   " &
                    " LEFT JOIN Item I ON L.Item = I.Code   " &
                    " LEFT JOIN ItemGroup IG ON IG.Code = I.ItemGroup " &
                    " LEFT JOIN Unit U ON U.Code = L.Unit " &
                    " LEFT JOIN JobIssRec JR ON L.JobReceive = JR.DocID   " &
                    " LEFT JOIN JobOrderDetail JOD ON L.JobOrder = JOD.DocID AND L.JobOrderSr = JOD.Sr  " &
                    " LEFT JOIN Voucher_Type V ON JR.V_Type = V.V_Type    " &
                    " Where 1=1 " & mCondStr & " "

            DsRep = AgL.FillData(mQry, AgL.GCn)
            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records to Print!")

            ReportFrm.PrintReport(DsRep, RepName, RepTitle)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub
#End Region

#Region "Job Payment Advise"
    Private Sub ProcPaymentAdvise()
        Dim DtTemp As DataTable = Nothing
        Dim mOpeningQry$ = ""
        Dim mBillQry$ = ""
        Dim mTDSQry$ = ""
        Dim mAdvanceQry$ = ""
        Dim mDebitCreditQry$ = ""
        'Dim mProcessOn$ = ""
        Dim IsMultiProcess As Integer = 0

        Try
            RepName = "Production_ProcessPaymentAdvise"

            If ReportFrm.FGetText(2).ToString.Contains(",") Or ReportFrm.FGetText(2) = "All" Then
                RepTitle = "Process Payment Advise"
                IsMultiProcess = 1
            Else
                RepTitle = ReportFrm.FGetText(2) & " Payment Advise"
                IsMultiProcess = 0
            End If

            Dim mCondStr$ = ""
            mCondStr = mCondStr & " And H.Site_Code = '" & AgL.PubSiteCode & "' "
            mCondStr = mCondStr & " And H.DivCode = '" & AgL.PubDivCode & "' "
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("P.NCat", 2)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.SubCode", 3)


            mBillQry = "SELECT H.CostCenter,Max(P.sr) AS ProcessSr, Max(P.Description) AS ProcessDesc, " &
                        " H.SubCode, Max(SG.Name) AS JobWorkerName, IFNull(Sum(H.AmtCr),0) AS BillAmount, sum(JI.BillQty) AS BillQty, Max(JI.Unit) AS Unit , Max(JI.DecimalPlaces) AS DecimalPlaces  " &
                        " FROM Ledger H  " &
                        " LEFT JOIN Voucher_Type Vt  ON Vt.V_Type  = H.V_Type " &
                        " LEFT JOIN SubGroup SG  ON SG.SubCode = H.SubCode " &
                        " LEFT JOIN Process P  ON P.CostCenter = H.CostCenter " &
                        " Left Join " &
                        " ( " &
                        " SELECT H.DocID, sum(L.BillQty) AS BillQty, Max(L.Unit) AS Unit,Max(U.DecimalPlaces) AS DecimalPlaces, Round(Sum(L.NetAmount),0) AS NetAmount " &
                        " FROM JobInvoice H  " &
                        " LEFT JOIN JobInvoiceDetail L ON L.DocId = H.DocID " &
                        " LEFT JOIN Unit U ON U.Code = L.Unit " &
                        " GROUP BY H.DocID  " &
                        " ) JI ON JI.DocID = H.DocId  " &
                        " WHERE Vt.NCat = '" & AgTemplate.ClsMain.Temp_NCat.JobInvoice & " ' AND H.AmtCr > 0 " &
                        " And H.V_Date Between '" & ReportFrm.FGetText(0) & "' And '" & ReportFrm.FGetText(1) & "' " &
                        " " & mCondStr & " " &
                        " GROUP BY H.CostCenter, H.SubCode "

            mOpeningQry = "SELECT H.CostCenter, H.SubCode, IFNull(Sum(H.Amtcr),0)-IFNull(Sum(VA.AdjAmount),0)  AS OpeningAmount " &
                        " FROM Ledger H   " &
                        " LEFT JOIN Process P  ON P.CostCenter = H.CostCenter " &
                        " Left Join " &
                        " ( " &
                        " SELECT A.Adj_DocID , A.Adj_V_SNo, sum(A.Amount) AS AdjAmount " &
                        " FROM LedgerAdj A GROUP BY A.Adj_DocID , A.Adj_V_SNo " &
                        " ) VA ON VA.Adj_DocID = H.DocId AND VA.Adj_V_SNo = H.V_Sno " &
                        " WHERE H.V_Date < '" & ReportFrm.FGetText(0) & "' " &
                        " " & mCondStr & " " &
                        " GROUP BY H.CostCenter, H.SubCode "

            'mTDSQry = "SELECT H.CostCenter,  H.SubCode, IFNull(Sum(H.AmtDr),0) AS TDSAmount " & _
            '            " FROM Ledger H  " & _
            '            " LEFT JOIN Voucher_Type Vt  ON Vt.V_Type  = H.V_Type " & _
            '            " LEFT JOIN Process P  ON P.CostCenter = H.CostCenter " & _
            '            " WHERE  Vt.NCat = '" & AgTemplate.ClsMain.Temp_NCat.JobTDS & " ' " & _
            '            " And H.V_Date Between '" & ReportFrm.FGetText(0) & "' And '" & ReportFrm.FGetText(1) & "' " & _
            '            " " & mCondStr & " " & _
            '            " GROUP BY H.CostCenter, H.SubCode "

            'mAdvanceQry = "SELECT H.CostCenter, H.SubCode, IFNull(Sum(H.Amtdr),0)-IFNull(Sum(VA.AdjAmount),0)   AS AdvanceAmount " & _
            '            " FROM Ledger H   " & _
            '            " LEFT JOIN Process P  ON P.CostCenter = H.CostCenter " & _
            '            " LEFT JOIN Voucher_Type Vt  ON Vt.V_Type  = H.V_Type " & _
            '            " Left Join " & _
            '            " ( " & _
            '            " SELECT A.Vr_DocId, Vr_V_SNo, sum(A.Amount) AS AdjAmount " & _
            '            " FROM LedgerAdj A GROUP BY A.Vr_DocId , A.Vr_V_SNo " & _
            '            " ) VA ON VA.Vr_DocId = H.DocId AND VA.Vr_V_SNo = H.V_Sno " & _
            '            " WHERE H.V_Date <= '" & ReportFrm.FGetText(1) & "' " & _
            '            " AND  Vt.NCat <> '" & AgTemplate.ClsMain.Temp_NCat.JobTDS & " ' " & _
            '            " " & mCondStr & " " & _
            '            " GROUP BY H.CostCenter, H.SubCode "

            'mDebitCreditQry = "SELECT H.CostCenter,  H.SubCode, IFNull(Sum(H.AmtCr),0)-IFNull(Sum(H.AmtDr),0) AS DbtcrdAmount " & _
            '        " FROM Ledger H  " & _
            '        " LEFT JOIN Voucher_Type Vt  ON Vt.V_Type  = H.V_Type " & _
            '        " LEFT JOIN Process P  ON P.CostCenter = H.CostCenter " & _
            '        " WHERE  Vt.NCat in ( '" & AgTemplate.ClsMain.Temp_NCat.JobCreditNote & "', '" & AgTemplate.ClsMain.Temp_NCat.JobDebitNote & "','FDEBT','FCRDT') " & _
            '        " And H.V_Date Between '" & ReportFrm.FGetText(0) & "' And '" & ReportFrm.FGetText(1) & "' " & _
            '        " " & mCondStr & " " & _
            '        " GROUP BY H.CostCenter, H.SubCode "

            mQry = "Select H.* , " & IsMultiProcess & " As IsMultiProcess, " &
                    " IFNull(VOpen.OpeningAmount,0) AS OpeningAmount, IFNull(VTDS.TDSAmount,0) AS TDSAmount, " &
                    " IFNull(VAdvance.AdvanceAmount,0) AS AdvanceAmount, IFNull(VDC.DbtcrdAmount,0)  AS DbtcrdAmount, " &
                    " H.BillAmount + IFNull(VOpen.OpeningAmount,0)-IFNull(VTDS.TDSAmount,0)-IFNull(VAdvance.AdvanceAmount,0)+IFNull(VDC.DbtcrdAmount,0)  AS PaybleAmt " &
                    " From ( " & mBillQry & " ) H " &
                    " LEFT JOIN ( " & mOpeningQry & " ) VOpen ON VOpen.CostCenter = H.CostCenter AND VOpen.SubCode = H.SubCode " &
                    " LEFT JOIN ( " & mTDSQry & " ) VTDS ON VTDS.CostCenter = H.CostCenter AND VTDS.SubCode = H.SubCode " &
                    " LEFT JOIN ( " & mAdvanceQry & " ) VAdvance ON VAdvance.CostCenter = H.CostCenter AND VAdvance.SubCode = H.SubCode " &
                    " LEFT JOIN ( " & mDebitCreditQry & " ) VDC ON VDC.CostCenter = H.CostCenter AND VDC.SubCode = H.SubCode " &
                    " Order By H.JobWorkerName "


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
