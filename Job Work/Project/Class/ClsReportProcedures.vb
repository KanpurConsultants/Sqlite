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
    Private Const WorkOrderReport As String = "WorkOrderReport"
    Private Const WorkDispatchReport As String = "WorkDispatchReport"
    Private Const WorkInvoiceReport As String = "WorkInvoiceReport"
    Private Const WorkOrderStatus As String = "WorkOrderStatus"
#End Region

#Region "Queries Definition"
    Dim VtypeRestriction$ = " AND H.V_Type NOT IN " & _
                        " ( Select L.V_Type " & _
                        " FROM User_Exclude_VTypeDetail L  " & _
                        " WHERE L.UserName = " & AgL.Chk_Text(AgL.PubUserName) & " ) "


    Dim mHelpSiteQry$ = "Select 'o' As Tick, Code, Name AS [Site / Branch] FROM SiteMast WHERE CharIndex('|' + Code + '|', (SELECT Max(SiteList) FROM UserSite WHERE User_Name = 'SA'))>0 OR '" & AgL.PubUserName & "' IN ('SA','" & AgLibrary.ClsConstant.PubSuperUserName & "')"
    Dim mHelpDivisionQry$ = "Select 'o' As Tick, Div_Code AS Code, Div_Name AS [Division] FROM Division WHERE CharIndex('|' + Div_Code + '|', (SELECT Max(DivisionList) FROM UserSite WHERE User_Name = 'SA'))>0 OR '" & AgL.PubUserName & "' IN ('SA','" & AgLibrary.ClsConstant.PubSuperUserName & "')"
    Dim mHelpCityQry$ = "Select 'o' As Tick, CityCode, CityName From City "
    Dim mHelpStateQry$ = "Select 'o' As Tick, State_Code, State_Desc From State "
    Dim mHelpUserQry$ = "Select 'o' As Tick, User_Name As Code, User_Name As [User] From UserMast "
    Dim mHelpCurrencyQry$ = "Select 'o' As Tick, Code, Code, Description From Currency "
    Dim mHelpGodownQry$ = "Select 'o' As Tick, Code, Description FROM Godown WHERE Status = 'Active' "
    Dim mHelpSalesTaxGroupParty$ = "Select 'o' As Tick, Description AS Code, Description FROM PostingGroupSalesTaxParty "
    Dim mHelpSalesTaxGroupItem$ = "Select 'o' As Tick, Description AS Code, Description FROM PostingGroupSalesTaxItem "
    Dim mHelpItemQry$ = "Select 'o' As Tick, I.Code, I.Description As [Item], IG.Description as [Item Group], IC.Description as [Item Category] " & _
                        "From Item I " & _
                        "Left JOIN ItemGroup IG ON I.ItemGroup = IG.Code " & _
                        "LEFT JOIN ItemCategory IC ON IG.ItemCategory = IC.Code  " & _
                        "LEFT JOIN ItemType IT ON IC.ItemType = IT.Code  "

    Dim mHelpItemGroupQry$ = "Select 'o' As Tick, IG.Code, IG.Description As [Item Group], IC.Description as [Item Category], IT.Name as [Item Type] " & _
                             "From ItemGroup IG " & _
                             "LEFT JOIN ItemCategory IC ON IG.ItemCategory = IC.Code  " & _
                             "LEFT JOIN ItemType IT ON IC.ItemType = IT.Code  "
    Dim mHelpItemCategoryQry$ = "Select 'o' As Tick, IC.Code, IC.Description As [Item Category], IT.Name as [Item Type] " & _
                                "From ItemCategory IC " & _
                                "LEFT JOIN ItemType IT ON IC.ItemType = IT.Code  "
    Dim mHelpItemReportingGroupQry$ = "Select 'o' As Tick, Code, Description As [Group Name] From ItemReportingGroup "

    Dim mHelpBuyerQry$ = " Select 'o' As Tick,  SG.SubCode As Code, SG.Name AS Vendor, C.CityName AS City FROM SubGroup Sg " & _
                            " LEFT JOIN City C ON C.CityCode = SG.CityCode  " & _
                            " WHERE SG.Nature ='Customer' " & _
                            " AND SG.Site_Code = '" & AgL.PubSiteCode & "' " & _
                            " And IfNull(SG.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "'"

    Dim mHelpItemType$ = "SELECT 'o' AS Tick, Code, Name  AS ItemType  FROM ItemType ORDER BY Name "
    Dim mHelpDepartment$ = "SELECT 'o' AS Tick, Code, Description AS Department FROM Department ORDER BY Description "


    Dim mHelpWorkOrder$ = " Select 'o' As Tick, H.DocID, Max(H.V_Type + '- ' + H.ManualRefNo) AS OrderNo , Max(H.V_Date) AS OrderDate " & _
                        " FROM WorkOrderDetail L  " & _
                        " LEFT JOIN WorkOrder H ON L.WorkOrder  = H.DocID " & _
                        " WHERE H.Div_Code = '" & AgL.PubDivCode & "'  And H.Site_Code = '" & AgL.PubSiteCode & "' " & _
                        " " & VtypeRestriction & " Group By H.DocID "

    Dim mHelpWorkDispatch$ = " Select 'o' As Tick, H.DocID, Max(H.V_Type + '- ' + H.ManualRefNo) AS DispatchNo , Max(H.V_Date) AS DispatchDate " & _
                " FROM WorkDispatchDetail L  " & _
                " LEFT JOIN WorkDispatch H ON L.WorkDispatch  = H.DocID " & _
                " WHERE H.Div_Code = '" & AgL.PubDivCode & "'  AND H.Site_Code = '" & AgL.PubSiteCode & "' " & _
                " " & VtypeRestriction & " Group By H.DocID "

    Dim mHelpWorkInvoice$ = " Select 'o' As Tick, H.DocID, Max(H.V_Type + '- ' + H.ManualRefNo) AS InvoiceNo , Max(H.V_Date) AS InvoiceDate " & _
            " FROM WorkInvoiceDetail L  " & _
            " LEFT JOIN WorkInvoice H ON L.WorkInvoice  = H.DocID " & _
            " WHERE H.Div_Code = '" & AgL.PubDivCode & "'  AND H.Site_Code = '" & AgL.PubSiteCode & "' " & _
            " " & VtypeRestriction & " Group By H.DocID "

    Dim mHelpAgentQry$ = " Select 'o' As Tick,  SG.SubCode As Code, Sg.DispName AS Vendor FROM SubGroup Sg WHERE SG.Nature ='Agent'"

#End Region

    Dim DsRep As DataSet = Nothing, DsRep1 As DataSet = Nothing, DsRep2 As DataSet = Nothing
    Dim mQry$ = "", RepName$ = "", RepTitle$ = "", OrderByStr$ = ""

#Region "Initializing Grid"
    Public Sub Ini_Grid()
        Try
            Dim I As Integer = 0
            Select Case GRepFormName
                Case WorkOrderReport
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubEndDate)
                    mQry = "Select 'Detail' as Code, 'Detail' as Name " & _
                             "Union All Select 'Item Wise Detail' as Code, 'Item Wise Detail' as Name " & _
                             "Union All Select 'Item Wise Summary' as Code, 'Item Wise Summary' as Name " & _
                             "Union All Select 'Month Wise Summary' as Code, 'Month Wise Summary' as Name " & _
                             "Union All Select 'Party Wise Summary' as Code, 'Party Wise Summary' as Name  " & _
                             "Union All Select 'Item Category Wise Summary' as Code, 'Item Category Wise Summary' as Name "
                    ReportFrm.CreateHelpGrid("Report Type", "Report Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, mQry, "Detail", , , 250)
                    ReportFrm.CreateHelpGrid("Unit", "Unit", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, "Select 'Amount' as Code, 'Amount' as Name Union All Select 'Qty' as Code, 'Qty' as Name Union All Select 'Measure' as Code, 'Measure' as Name Union All Select 'Qty & Amount' as Code, 'Qty & Amount' as Name Union All Select 'Qty & Measure' as Code, 'Qty & Measure' as Name Union All Select 'Measure & Amount' as Code, 'Measure & Amount' as Name", "Qty & Amount", , , 250, , False)
                    ReportFrm.CreateHelpGrid("Voucher Type", "Voucher Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, FGetVoucher_TypeQry("SaleOrder"))
                    ReportFrm.CreateHelpGrid("Party", "Party", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpBuyerQry, , , 550, 300)
                    ReportFrm.CreateHelpGrid("Code", "Amount Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, FGetStructureFieldsQry(AgTemplate.ClsMain.Temp_NCat.WorkOrder), "Amount|Amount")
                    ReportFrm.CreateHelpGrid("Work Order No", "Work Order No", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpWorkOrder, , , 550)
                    ReportFrm.CreateHelpGrid("Item", "Item", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemQry, , , 600, 270)
                    ReportFrm.CreateHelpGrid("Item Group", "Item Group", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemGroupQry, , , 530)
                    ReportFrm.CreateHelpGrid("Item Category", "Item Category", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemCategoryQry, , , 430)
                    ReportFrm.CreateHelpGrid("Item Type", "Item Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemType)
                    ReportFrm.CreateHelpGrid("Item Reporting Group", "Item Reporting Group", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemReportingGroupQry, , , 500, 360)
                    ReportFrm.CreateHelpGrid("Currency", "Currency", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpCurrencyQry, , , 600, 270)
                    ReportFrm.CreateHelpGrid("Group On Item Division", "Group On Item Division", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, "Select 'Yes' as Code, 'Yes' as Name Union All Select 'No' as Code, 'No' as Name ", "No")
                    ReportFrm.CreateHelpGrid("Group On Voucher Type", "Group On Voucher Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, "Select 'Yes' as Code, 'Yes' as Name Union All Select 'No' as Code, 'No' as Name ", "No")
                    ReportFrm.CreateHelpGrid("Division", "Division", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpDivisionQry)
                    ReportFrm.CreateHelpGrid("Site", "Site", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpSiteQry)

                Case WorkDispatchReport
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubEndDate)
                    ReportFrm.CreateHelpGrid("Report Type", "Report Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, "Select 'Detail' as Code, 'Detail' as Name Union All Select 'Item Wise Summary' as Code, 'Item Wise Summary' as Name Union All Select 'Item Nature Wise Summary' as Code, 'Item Nature Wise Summary' as Name ", "Detail")
                    ReportFrm.CreateHelpGrid("Unit", "Unit", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, "Select 'Amount' as Code, 'Amount' as Name Union All Select 'Qty' as Code, 'Qty' as Name Union All Select 'Measure' as Code, 'Measure' as Name Union All Select 'Qty & Amount' as Code, 'Qty & Amount' as Name Union All Select 'Qty & Measure' as Code, 'Qty & Measure' as Name Union All Select 'Measure & Amount' as Code, 'Measure & Amount' as Name", "Qty & Amount", , , 250, , False)
                    ReportFrm.CreateHelpGrid("Voucher Type", "Voucher Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, FGetMainVoucher_TypeQry("SaleChallanDetail", "LEFT JOIN SaleChallan H ON H.DocID = L.SaleChallan"))
                    ReportFrm.CreateHelpGrid("Party", "Party", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpBuyerQry, , , , 280)
                    ReportFrm.CreateHelpGrid("Order No", "Order No", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpWorkOrder, , , 450)
                    ReportFrm.CreateHelpGrid("Item", "Item", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemQry, , , 600, 270)
                    ReportFrm.CreateHelpGrid("Item Group", "Item Group", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemGroupQry, , , 530)
                    ReportFrm.CreateHelpGrid("Item Category", "Item Category", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemCategoryQry, , , 430)
                    ReportFrm.CreateHelpGrid("Item Type", "Item Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemType)
                    ReportFrm.CreateHelpGrid("Item Reporting Group", "Item Reporting Group", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemReportingGroupQry, , , 500, 360)
                    ReportFrm.CreateHelpGrid("Division", "Division", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpDivisionQry)
                    ReportFrm.CreateHelpGrid("Site", "Site", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpSiteQry)


                Case WorkInvoiceReport
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubEndDate)
                    ReportFrm.CreateHelpGrid("Report Type", "Report Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, "Select 'Detail' as Code, 'Detail' as Name Union All Select 'Item Wise Detail' as Code, 'Item Wise Detail' as Name Union All Select 'Item Wise Summary' as Code, 'Item Wise Summary' as Name Union All Select 'Month Wise Summary' as Code, 'Month Wise Summary' as Name Union All Select 'Party Wise Summary' as Code, 'Party Wise Summary' as Name", "Detail", , , , , False)
                    ReportFrm.CreateHelpGrid("Unit", "Unit", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, "Select 'Amount' as Code, 'Amount' as Name Union All Select 'Qty' as Code, 'Qty' as Name Union All Select 'Measure' as Code, 'Measure' as Name Union All Select 'Qty & Amount' as Code, 'Qty & Amount' as Name Union All Select 'Qty & Measure' as Code, 'Qty & Measure' as Name Union All Select 'Measure & Amount' as Code, 'Measure & Amount' as Name", "Qty & Amount", , , 250, , False)
                    ReportFrm.CreateHelpGrid("Voucher Type", "Voucher Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, FGetVoucher_TypeQry("PurchInvoice"))
                    ReportFrm.CreateHelpGrid("Party", "Party", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpBuyerQry, , , , 280)
                    ReportFrm.CreateHelpGrid("Code", "Amount Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, FGetStructureFieldsQry(AgTemplate.ClsMain.Temp_NCat.WorkInvoice), "Amount|Amount", , , , , False)
                    ReportFrm.CreateHelpGrid("Work Order", "Work Order", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpWorkOrder, , , 450)
                    ReportFrm.CreateHelpGrid("Work Dispatch", "Work Dispatch", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpWorkDispatch, , , 450)
                    ReportFrm.CreateHelpGrid("Work Invoice", "Work Invoice", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpWorkInvoice, , , 450)
                    ReportFrm.CreateHelpGrid("Item", "Item", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemQry, , , 600, 270)
                    ReportFrm.CreateHelpGrid("Item Group", "Item Group", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemGroupQry, , , 530)
                    ReportFrm.CreateHelpGrid("Item Category", "Item Category", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemCategoryQry, , , 430)
                    ReportFrm.CreateHelpGrid("Item Type", "Item Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemType)
                    ReportFrm.CreateHelpGrid("Item Reporting Group", "Item Reporting Group", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemReportingGroupQry, , , 500, 360)

                Case WorkOrderStatus
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubEndDate)
                    ReportFrm.CreateHelpGrid("Status On", "Status On", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubLoginDate)
                    ReportFrm.CreateHelpGrid("Report Type", "Report Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, "Select 'Summary' as Code, 'Summary' as Name Union All Select 'Summary with Amount' as Code, 'Summary with Amount' as Name Union All Select 'Detail' as Code, 'Detail' as Name Union All Select 'Item Wise Order Status' as Code, 'Item Wise Order Status' as Name Union All Select 'Work Order Wise Detail' as Code, 'Work Order Wise Detail' as Name ", "Summary")
                    mQry = " SELECT 'All' AS Code, 'All' AS Name " & _
                            " UNION ALL  SELECT 'Pending For Dispatch' AS Code, 'Pending For Dispatch' AS Name " & _
                            " UNION ALL  SELECT 'Dispatched' AS Code, 'Dispatched' AS Name " & _
                            " UNION ALL  SELECT 'Over Due' AS Code, 'Over Due' AS Name " & _
                            " UNION ALL  SELECT 'Over Due And Pending' AS Code, 'Over Due And Pending' AS Name " & _
                            " UNION ALL  SELECT 'Timely Dispatched' AS Code, 'Timely Dispatched' AS Name "
                    ReportFrm.CreateHelpGrid("Report For", "Report For", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, mQry)
                    ReportFrm.CreateHelpGrid("Unit", "Unit", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, "Select 'Qty' as Code, 'Qty' as Name Union All Select 'Measure' as Code, 'Measure' AS Name Union All Select 'Qty & Measure' as Code, 'Qty & Measure' AS Name ", "Qty")
                    ReportFrm.CreateHelpGrid("Sort On", "Sort On", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, " SELECT 'Order Date' AS Code, 'Order Date' AS Name UNION ALL  SELECT 'Due Date' AS Code, 'Due Date' AS Name UNION ALL SELECT 'Over Due Days' AS Code, 'Over Due Days' AS Name UNION ALL  SELECT 'Balance Qty' AS Code, 'Balance Qty' AS Name ", "Order Date")
                    ReportFrm.CreateHelpGrid("Voucher Type", "Voucher Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, FGetMainVoucher_TypeQry("SaleOrderDetail", "LEFT JOIN SaleOrder H ON H.DocID = L.SaleOrder"))
                    ReportFrm.CreateHelpGrid("Party", "Party", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpBuyerQry, , , , 280)
                    ReportFrm.CreateHelpGrid("Agent", "Agent", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpAgentQry, , , , 280)
                    ReportFrm.CreateHelpGrid("Work Order No", "Work Order No", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpWorkOrder, , , 450)
                    ReportFrm.CreateHelpGrid("Item", "Item", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemQry, , , 600, 270)
                    ReportFrm.CreateHelpGrid("Item Group", "Item Group", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemGroupQry, , , 530)
                    ReportFrm.CreateHelpGrid("Item Category", "Item Category", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemCategoryQry, , , 430)
                    ReportFrm.CreateHelpGrid("Item Type", "Item Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemType)
                    ReportFrm.CreateHelpGrid("Item Reporting Group", "Item Reporting Group", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemReportingGroupQry, , , 500, 360)
                    ReportFrm.CreateHelpGrid("Division", "Division", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpDivisionQry)
                    ReportFrm.CreateHelpGrid("Site", "Site", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpSiteQry)

            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
#End Region

    Private Function FGetVoucher_TypeQry(ByVal TableName As String) As String
        FGetVoucher_TypeQry = " SELECT Distinct 'o' As Tick, H.V_Type AS Code, Vt.Description AS [Voucher Type] " & _
                                " FROM " & TableName & " H  " & _
                                " LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type " & _
                                " Where 1 =1 " & VtypeRestriction & " "
    End Function

    Private Function FGetStructureFieldsQry(ByVal NCat As String) As String
        FGetStructureFieldsQry = "Select 'Amount' as Code, 'Amount' as Description " & _
                                 "Union All " & _
                                 "SELECT L.LineAmtField AS Code, C.Description AS [Amount Type]  " & _
                                 "FROM StructureDetail L " & _
                                 "LEFT JOIN Charges C ON L.Charges = C.Code  " & _
                                 "WHERE L.Code = (SELECT Structure FROM VoucherCat WHERE nCat = '" & NCat & "')"
    End Function

    Private Function FGetMainVoucher_TypeQry(ByVal HeaderTable As String, ByVal LineTableJoinStr As String) As String
        FGetMainVoucher_TypeQry = "Select DISTINCT 'o' As Tick, H.V_Type , Vt.Description " & _
            " FROM " & HeaderTable & "  L " & LineTableJoinStr & " " & _
            " LEFT JOIN Voucher_Type Vt ON Vt.V_Type = H.V_Type " & _
            " WHERE IfNull(H.V_Type,'') <> '' " & _
            " " & VtypeRestriction & " " & _
            " ORDER BY Vt.Description "
    End Function

    Private Sub ObjRepFormGlobal_ProcessReport() Handles ReportFrm.ProcessReport
        Select Case mGRepFormName
            Case WorkOrderReport
                ProcWorkOrderReport()

            Case WorkDispatchReport
                ProcWorkDispatchReport()

            Case WorkInvoiceReport
                ProcWorkInvoiceReport()

            Case WorkOrderStatus
                ProcWorkOrderStatusReport()

        End Select
    End Sub

    Public Sub New(ByVal mReportFrm As ReportLayout.FrmReportLayout)
        ReportFrm = mReportFrm
    End Sub

#Region "Work Order Report"
    Private Sub ProcWorkOrderReport()
        Dim mCondStr$ = ""
        Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"
        Dim strQtyFld As String = "''", strQtyFldHead = "''", strUnitFld As String = "''", strUnitFldDecimalPlace As String = "''"
        Dim strQtyFld2 As String = "''", strQtyFldHead2 = "''", strUnitFld2 As String = "''", strUnitFldDecimalPlace2 As String = "''"
        Dim mGroupItemDivision As String = "", mGroupVoucherType As String = ""

        Try
            If ReportFrm.FGetText(2) = "Item Wise Detail" Then
                RepTitle = "Item Wise Job Work Order Report"
                If ReportFrm.FGetText(3) = "Qty & Measure" Then
                    strQtyFld = "L.Qty"
                    strQtyFldHead = "'Qty'"
                    strUnitFld = "I.Unit"
                    strUnitFldDecimalPlace = "U.DecimalPlaces"
                    strQtyFld2 = "I.Measure*L.Qty"
                    strQtyFldHead2 = "'Measure'"
                    strUnitFld2 = "I.MeasureUnit"
                    strUnitFldDecimalPlace2 = "MU.DecimalPlaces"
                    RepName = "Work_WorkOrderReport_ItemWiseDetail_QtyMeasure"
                ElseIf ReportFrm.FGetText(3) = "Qty & Amount" Then
                    strQtyFld = "L.Qty"
                    strQtyFldHead = "'Qty'"
                    strUnitFld = "I.Unit"
                    strUnitFldDecimalPlace = "U.DecimalPlaces"
                    strQtyFld2 = "L." & Replace(ReportFrm.FGetCode(6), "'", "") & ""
                    strQtyFldHead2 = "'" & ReportFrm.FGetText(6) & "'"
                    strUnitFld2 = "C.Description"
                    strUnitFldDecimalPlace2 = "2"
                    RepName = "Work_WorkOrderReport_ItemWiseDetail_QtyMeasure"
                ElseIf ReportFrm.FGetText(3) = "Measure & Amount" Then
                    strQtyFld = "I.Measure*L.Qty"
                    strQtyFldHead = "'Measure'"
                    strUnitFld = "I.MeasureUnit"
                    strUnitFldDecimalPlace = "MU.DecimalPlaces"
                    strQtyFld2 = "L." & Replace(ReportFrm.FGetCode(6), "'", "") & ""
                    strQtyFldHead2 = "'" & ReportFrm.FGetText(6) & "'"
                    strUnitFld2 = "C.Description"
                    strUnitFldDecimalPlace2 = "2"
                    RepName = "Work_WorkOrderReport_ItemWiseDetail_QtyMeasure"
                ElseIf ReportFrm.FGetText(3) = "Measure" Then
                    strQtyFld = "I.Measure*L.Qty"
                    strQtyFldHead = "'Measure'"
                    strUnitFld = "I.MeasureUnit"
                    strUnitFldDecimalPlace = "MU.DecimalPlaces"
                    RepName = "Work_WorkOrderReport_ItemWiseDetail"
                ElseIf ReportFrm.FGetText(3) = "Qty" Then
                    strQtyFld = "L.Qty"
                    strQtyFldHead = "'Qty'"
                    strUnitFld = "I.Unit"
                    strUnitFldDecimalPlace = "U.DecimalPlaces"
                    RepName = "Work_WorkOrderReport_ItemWiseDetail"
                Else
                    strQtyFld = "L." & Replace(ReportFrm.FGetCode(6), "'", "") & ""
                    strQtyFldHead = "'" & ReportFrm.FGetText(6) & "'"
                    strUnitFld = "C.Description"
                    strUnitFldDecimalPlace = "2"
                    RepName = "Work_WorkOrderReport_ItemWiseDetail"
                End If
            ElseIf ReportFrm.FGetText(2) = "Item Wise Summary" Then
                strGrpFld = "I.Description"
                strGrpFldDesc = "I.Description"
                strGrpFldHead = "'Item'"
                RepTitle = "Job Work Order Report (Item Wise Summary)"
                If ReportFrm.FGetText(3) = "Qty & Measure" Then
                    strQtyFld = "L.Qty"
                    strQtyFldHead = "'Qty'"
                    strUnitFld = "I.Unit"
                    strUnitFldDecimalPlace = "U.DecimalPlaces"
                    strQtyFld2 = "I.Measure*L.Qty"
                    strQtyFldHead2 = "'Measure'"
                    strUnitFld2 = "I.MeasureUnit"
                    strUnitFldDecimalPlace2 = "MU.DecimalPlaces"
                    RepName = "Work_WorkOrderReport_Summary_QtyMeasure"
                ElseIf ReportFrm.FGetText(3) = "Qty & Amount" Then
                    strQtyFld = "L.Qty"
                    strQtyFldHead = "'Qty'"
                    strUnitFld = "I.Unit"
                    strUnitFldDecimalPlace = "U.DecimalPlaces"
                    strQtyFld2 = "L." & Replace(ReportFrm.FGetCode(6), "'", "") & ""
                    strQtyFldHead2 = "'" & ReportFrm.FGetText(6) & "'"
                    strUnitFld2 = "C.Description"
                    strUnitFldDecimalPlace2 = "2"
                    RepName = "Work_WorkOrderReport_Summary_QtyMeasure"
                ElseIf ReportFrm.FGetText(3) = "Measure & Amount" Then
                    strQtyFld = "I.Measure*L.Qty"
                    strQtyFldHead = "'Measure'"
                    strUnitFld = "I.MeasureUnit"
                    strUnitFldDecimalPlace = "MU.DecimalPlaces"
                    strQtyFld2 = "L." & Replace(ReportFrm.FGetCode(6), "'", "") & ""
                    strQtyFldHead2 = "'" & ReportFrm.FGetText(6) & "'"
                    strUnitFld2 = "C.Description"
                    strUnitFldDecimalPlace2 = "2"
                    RepName = "Work_WorkOrderReport_Summary_QtyMeasure"
                ElseIf ReportFrm.FGetText(3) = "Measure" Then
                    strQtyFld = "I.Measure*L.Qty"
                    strQtyFldHead = "'Measure'"
                    strUnitFld = "I.MeasureUnit"
                    strUnitFldDecimalPlace = "MU.DecimalPlaces"
                    RepName = "Work_WorkOrderReport_Summary"
                ElseIf ReportFrm.FGetText(3) = "Qty" Then
                    strQtyFld = "L.Qty"
                    strQtyFldHead = "'Qty'"
                    strUnitFld = "I.Unit"
                    strUnitFldDecimalPlace = "U.DecimalPlaces"
                    RepName = "Work_WorkOrderReport_Summary"
                Else
                    strQtyFld = "L." & Replace(ReportFrm.FGetCode(6), "'", "") & ""
                    strQtyFldHead = "'" & ReportFrm.FGetText(6) & "'"
                    strUnitFld = "C.Description"
                    strUnitFldDecimalPlace = "2"
                    RepName = "Work_WorkOrderReport_Summary"
                End If
            ElseIf ReportFrm.FGetText(2) = "Party Wise Summary" Then
                RepTitle = "Job Work Order Report ( Party Wise Summary )"
                strGrpFld = "SG.Name"
                strGrpFldDesc = "SG.Name + ',' + IfNull(City.CityName,'')"
                strGrpFldHead = "'Party Name'"

                If ReportFrm.FGetText(3) = "Qty & Measure" Then
                    strQtyFld = "L.Qty"
                    strQtyFldHead = "'Qty'"
                    strUnitFld = "I.Unit"
                    strUnitFldDecimalPlace = "U.DecimalPlaces"
                    strQtyFld2 = "I.Measure*L.Qty"
                    strQtyFldHead2 = "'Measure'"
                    strUnitFld2 = "I.MeasureUnit"
                    strUnitFldDecimalPlace2 = "MU.DecimalPlaces"
                    RepName = "Work_WorkOrderReport_Summary_QtyMeasure"
                ElseIf ReportFrm.FGetText(3) = "Qty & Amount" Then
                    strQtyFld = "L.Qty"
                    strQtyFldHead = "'Qty'"
                    strUnitFld = "I.Unit"
                    strUnitFldDecimalPlace = "U.DecimalPlaces"
                    strQtyFld2 = "L." & Replace(ReportFrm.FGetCode(6), "'", "") & ""
                    strQtyFldHead2 = "'" & ReportFrm.FGetText(6) & "'"
                    strUnitFld2 = "C.Description"
                    strUnitFldDecimalPlace2 = "2"
                    RepName = "Work_WorkOrderReport_Summary_QtyMeasure"
                ElseIf ReportFrm.FGetText(3) = "Measure & Amount" Then
                    strQtyFld = "I.Measure*L.Qty"
                    strQtyFldHead = "'Measure'"
                    strUnitFld = "I.MeasureUnit"
                    strUnitFldDecimalPlace = "MU.DecimalPlaces"
                    strQtyFld2 = "L." & Replace(ReportFrm.FGetCode(6), "'", "") & ""
                    strQtyFldHead2 = "'" & ReportFrm.FGetText(6) & "'"
                    strUnitFld2 = "C.Description"
                    strUnitFldDecimalPlace2 = "2"
                    RepName = "Work_WorkOrderReport_Summary_QtyMeasure"
                ElseIf ReportFrm.FGetText(3) = "Measure" Then
                    strQtyFld = "I.Measure*L.Qty"
                    strQtyFldHead = "'Measure'"
                    strUnitFld = "I.MeasureUnit"
                    strUnitFldDecimalPlace = "MU.DecimalPlaces"
                    RepName = "Work_WorkOrderReport_Summary"
                ElseIf ReportFrm.FGetText(3) = "Qty" Then
                    strQtyFld = "L.Qty"
                    strQtyFldHead = "'Qty'"
                    strUnitFld = "I.Unit"
                    strUnitFldDecimalPlace = "U.DecimalPlaces"
                    RepName = "Work_WorkOrderReport_Summary"
                Else
                    strQtyFld = "L." & Replace(ReportFrm.FGetCode(6), "'", "") & ""
                    strQtyFldHead = "'" & ReportFrm.FGetText(6) & "'"
                    strUnitFld = "C.Description"
                    strUnitFldDecimalPlace = "2"
                    RepName = "Work_WorkOrderReport_Summary"
                End If

            ElseIf ReportFrm.FGetText(2) = "Month Wise Summary" Then
                RepTitle = "Job Work Order Report (" & ReportFrm.FGetText(2) & ")"
                strGrpFld = "Substring(convert(nvarchar,H.V_Date,11),0,6)"
                strGrpFldDesc = "Replace(SubString(Convert(VARCHAR,H.v_Date,6),4,6),' ','-')"
                strGrpFldHead = "'Month'"
                If ReportFrm.FGetText(3) = "Qty & Measure" Then
                    strQtyFld = "L.Qty"
                    strQtyFldHead = "'Qty'"
                    strUnitFld = "I.Unit"
                    strUnitFldDecimalPlace = "U.DecimalPlaces"
                    strQtyFld2 = "I.Measure*L.Qty"
                    strQtyFldHead2 = "'Measure'"
                    strUnitFld2 = "I.MeasureUnit"
                    strUnitFldDecimalPlace2 = "MU.DecimalPlaces"
                    RepName = "Work_WorkOrderReport_Summary_QtyMeasure"
                ElseIf ReportFrm.FGetText(3) = "Qty & Amount" Then
                    strQtyFld = "L.Qty"
                    strQtyFldHead = "'Qty'"
                    strUnitFld = "I.Unit"
                    strUnitFldDecimalPlace = "U.DecimalPlaces"
                    strQtyFld2 = "L." & Replace(ReportFrm.FGetCode(6), "'", "") & ""
                    strQtyFldHead2 = "'" & ReportFrm.FGetText(6) & "'"
                    strUnitFld2 = "C.Description"
                    strUnitFldDecimalPlace2 = "2"
                    RepName = "Work_WorkOrderReport_Summary_QtyMeasure"
                ElseIf ReportFrm.FGetText(3) = "Measure & Amount" Then
                    strQtyFld = "I.Measure*L.Qty"
                    strQtyFldHead = "'Measure'"
                    strUnitFld = "I.MeasureUnit"
                    strUnitFldDecimalPlace = "MU.DecimalPlaces"
                    strQtyFld2 = "L." & Replace(ReportFrm.FGetCode(6), "'", "") & ""
                    strQtyFldHead2 = "'" & ReportFrm.FGetText(6) & "'"
                    strUnitFld2 = "C.Description"
                    strUnitFldDecimalPlace2 = "2"
                    RepName = "Work_WorkOrderReport_Summary_QtyMeasure"
                ElseIf ReportFrm.FGetText(3) = "Measure" Then
                    strQtyFld = "I.Measure*L.Qty"
                    strQtyFldHead = "'Measure'"
                    strUnitFld = "I.MeasureUnit"
                    strUnitFldDecimalPlace = "MU.DecimalPlaces"
                    RepName = "Work_WorkOrderReport_Summary"
                ElseIf ReportFrm.FGetText(3) = "Qty" Then
                    strQtyFld = "L.Qty"
                    strQtyFldHead = "'Qty'"
                    strUnitFld = "I.Unit"
                    strUnitFldDecimalPlace = "U.DecimalPlaces"
                    RepName = "Work_WorkOrderReport_Summary"
                Else
                    strQtyFld = "L." & Replace(ReportFrm.FGetCode(6), "'", "") & ""
                    strQtyFldHead = "'" & ReportFrm.FGetText(6) & "'"
                    strUnitFld = "C.Description"
                    strUnitFldDecimalPlace = "2"
                    RepName = "Work_WorkOrderReport_Summary"
                End If

            ElseIf ReportFrm.FGetText(2) = "Item Category Wise Summary" Then
                strGrpFld = "IC.Description"
                strGrpFldDesc = "IC.Description"
                strGrpFldHead = "'Item Category'"
                RepTitle = "Job Work Order Summary (Item Category Wise)"
                If ReportFrm.FGetText(3) = "Qty & Measure" Then
                    strQtyFld = "L.Qty"
                    strQtyFldHead = "'Qty'"
                    strUnitFld = "I.Unit"
                    strUnitFldDecimalPlace = "U.DecimalPlaces"
                    strQtyFld2 = "I.Measure*L.Qty"
                    strQtyFldHead2 = "'Measure'"
                    strUnitFld2 = "I.MeasureUnit"
                    strUnitFldDecimalPlace2 = "MU.DecimalPlaces"
                    RepName = "Work_WorkOrderReport_Summary_QtyMeasure"
                ElseIf ReportFrm.FGetText(3) = "Qty & Amount" Then
                    strQtyFld = "L.Qty"
                    strQtyFldHead = "'Qty'"
                    strUnitFld = "I.Unit"
                    strUnitFldDecimalPlace = "U.DecimalPlaces"
                    strQtyFld2 = "L." & Replace(ReportFrm.FGetCode(6), "'", "") & ""
                    strQtyFldHead2 = "'" & ReportFrm.FGetText(6) & "'"
                    strUnitFld2 = "C.Description"
                    strUnitFldDecimalPlace2 = "2"
                    RepName = "Work_WorkOrderReport_Summary_QtyMeasure"
                ElseIf ReportFrm.FGetText(3) = "Measure & Amount" Then
                    strQtyFld = "I.Measure*L.Qty"
                    strQtyFldHead = "'Measure'"
                    strUnitFld = "I.MeasureUnit"
                    strUnitFldDecimalPlace = "MU.DecimalPlaces"
                    strQtyFld2 = "L." & Replace(ReportFrm.FGetCode(6), "'", "") & ""
                    strQtyFldHead2 = "'" & ReportFrm.FGetText(6) & "'"
                    strUnitFld2 = "C.Description"
                    strUnitFldDecimalPlace2 = "2"
                    RepName = "Work_WorkOrderReport_Summary_QtyMeasure"
                ElseIf ReportFrm.FGetText(3) = "Measure" Then
                    strQtyFld = "I.Measure*L.Qty"
                    strQtyFldHead = "'Measure'"
                    strUnitFld = "I.MeasureUnit"
                    strUnitFldDecimalPlace = "MU.DecimalPlaces"
                    RepName = "Work_WorkOrderReport_Summary"
                ElseIf ReportFrm.FGetText(3) = "Qty" Then
                    strQtyFld = "L.Qty"
                    strQtyFldHead = "'Qty'"
                    strUnitFld = "I.Unit"
                    strUnitFldDecimalPlace = "U.DecimalPlaces"
                    RepName = "Work_WorkOrderReport_Summary"
                Else
                    strQtyFld = "L." & Replace(ReportFrm.FGetCode(6), "'", "") & ""
                    strQtyFldHead = "'" & ReportFrm.FGetText(6) & "'"
                    strUnitFld = "C.Description"
                    strUnitFldDecimalPlace = "2"
                    RepName = "Work_WorkOrderReport_Summary"
                End If
            Else
                RepTitle = "Job Work Order Report"
                If ReportFrm.FGetText(3) = "Qty & Measure" Then
                    strQtyFld = "L.Qty"
                    strQtyFldHead = "'Qty'"
                    strUnitFld = "I.Unit"
                    strUnitFldDecimalPlace = "U.DecimalPlaces"
                    strQtyFld2 = "I.Measure*L.Qty"
                    strQtyFldHead2 = "'Measure'"
                    strUnitFld2 = "I.MeasureUnit"
                    strUnitFldDecimalPlace2 = "MU.DecimalPlaces"
                    RepName = "Work_WorkOrderReport_QtyMeasure"
                ElseIf ReportFrm.FGetText(3) = "Qty & Amount" Then
                    strQtyFld = "L.Qty"
                    strQtyFldHead = "'Qty'"
                    strUnitFld = "I.Unit"
                    strUnitFldDecimalPlace = "U.DecimalPlaces"
                    strQtyFld2 = "L." & Replace(ReportFrm.FGetCode(6), "'", "") & ""
                    strQtyFldHead2 = "'" & ReportFrm.FGetText(6) & "'"
                    strUnitFld2 = "C.Description"
                    strUnitFldDecimalPlace2 = "2"
                    RepName = "Work_WorkOrderReport_QtyMeasure"
                ElseIf ReportFrm.FGetText(3) = "Measure & Amount" Then
                    strQtyFld = "I.Measure*L.Qty"
                    strQtyFldHead = "'Measure'"
                    strUnitFld = "I.MeasureUnit"
                    strUnitFldDecimalPlace = "MU.DecimalPlaces"
                    strQtyFld2 = "L." & Replace(ReportFrm.FGetCode(6), "'", "") & ""
                    strQtyFldHead2 = "'" & ReportFrm.FGetText(6) & "'"
                    strUnitFld2 = "C.Description"
                    strUnitFldDecimalPlace2 = "2"
                    RepName = "Work_WorkOrderReport_QtyMeasure"
                ElseIf ReportFrm.FGetText(3) = "Measure" Then
                    strQtyFld = "I.Measure*L.Qty"
                    strQtyFldHead = "'Measure'"
                    strUnitFld = "I.MeasureUnit"
                    strUnitFldDecimalPlace = "MU.DecimalPlaces"
                    RepName = "Work_WorkOrderReport"
                ElseIf ReportFrm.FGetText(3) = "Qty" Then
                    strQtyFld = "L.Qty"
                    strQtyFldHead = "'Qty'"
                    strUnitFld = "I.Unit"
                    strUnitFldDecimalPlace = "U.DecimalPlaces"
                    RepName = "Work_WorkOrderReport"
                Else
                    strQtyFld = "L." & Replace(ReportFrm.FGetCode(6), "'", "") & ""
                    strQtyFldHead = "'" & ReportFrm.FGetText(6) & "'"
                    strUnitFld = "C.Description"
                    strUnitFldDecimalPlace = "2"
                    RepName = "Work_WorkOrderReport"
                End If
            End If

            mCondStr = mCondStr & " And H.V_Date Between " & AgL.Chk_Text(ReportFrm.FGetText(0)) & " And " & AgL.Chk_Text(ReportFrm.FGetText(1)) & " "
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.V_Type", 4)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.WorkToParty", 5)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.WorkOrder", 7)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Item", 8)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemGroup", 9)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemCategory", 10)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemType ", 11)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("SO.Currency ", 15)

            If ReportFrm.FGetText(12) <> "" And ReportFrm.FGetText(12) <> "All" Then
                mQry = " Select '''' +  replace(ItemList,',',''',''')  + ''''  From ItemReportingGroup Where 1=1 " & ReportFrm.GetWhereCondition("Code", 12)
                mCondStr = mCondStr & " AND I.Code In (" & AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar & ")"
            End If

            If ReportFrm.FGetText(14) = "Yes" Then
                mGroupItemDivision = "ID.Div_Name"
            Else
                mGroupItemDivision = "''"
            End If

            If ReportFrm.FGetText(15) = "Yes" Then
                mGroupVoucherType = "Vt.serialNo"
            Else
                mGroupVoucherType = "0"
            End If

            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.Div_Code ", 16)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.Site_Code ", 17)


            mQry = " SELECT H.DocID, H.V_Type, H.V_Date, H.ManualRefNo, C.Description AS Currency, U.DecimalPlaces, MU.DecimalPlaces as MeasureDecimalPlaces,  SO.PartyOrderNo, P.Description AS ProcessDesc,  P.Sr AS ProcessSr, " & _
                    " SG.DispName + (Case When City.CityName Is Not Null then ', ' +  City.CityName Else '' End) AS WorkToPartyName,  SO.PartyDeliveryDate, " & _
                    " " & strGrpFld & " as GrpField, " & strGrpFldDesc & " as GrpFieldDesc, " & strGrpFldHead & " as GrpFieldHead, Vt.SerialNo AS VTypeSr, " & _
                    " " & strQtyFld & " as PrnQtyField, " & strQtyFldHead & " as PrnQtyFieldHead, " & strUnitFld & " as PrnUnitField, " & strUnitFldDecimalPlace & " as PrnDecimalPlaces, " & _
                    " " & strQtyFld2 & " as PrnQtyField2, " & strQtyFldHead2 & " as PrnQtyFieldHead2, " & strUnitFld2 & " as PrnUnitField2, " & strUnitFldDecimalPlace2 & " as PrnDecimalPlaces2, " & _
                    " SO.PartyOrderDate, H.Remarks, L.Remark AS LineRemark, Convert(SmallDateTime,'01 ' + SubString(Convert(Varchar,H.V_Date,6),4,6)) as V_Month, " & _
                    " L.Sr, L.Item, L.Qty, I.Unit, I.Measure as MeasurePerPcs, I.MeasureUnit, I.Measure*L.Qty as TotalMeasure, L.Rate, " & _
                    " L." & Replace(ReportFrm.FGetCode(6), "'", "") & " as Amount, '" & ReportFrm.FGetText(6) & "' as AmountTitle, " & _
                    " L.RateType, I.Description AS ItemDesc, Vt.Description AS VoucherTypeDesc, ID.Div_Name as Item_Div_Name,  " & _
                    " " & mGroupItemDivision & " AS mGroupItemDivision, " & mGroupVoucherType & " AS mGroupVoucherType " & _
                    " FROM WorkOrder H " & _
                    " LEFT JOIN SubGroup SG ON SG.SubCode = H.Party  " & _
                    " LEFT JOIN City On SG.CityCode = City.CityCode " & _
                    " LEFT JOIN WorkOrderDetail L ON L.DocId = H.DocID  " & _
                    " LEFT JOIN WorkOrder SO ON L.WorkOrder = SO.DocID  " & _
                    " LEFT JOIN Process P ON P.NCat = H.Process " & _
                    " LEFT JOIN Item I ON I.Code = L.Item  " & _
                    " LEFT JOIN ItemGroup IG ON I.ItemGroup = IG.Code  " & _
                    " LEFT JOIN ItemCategory IC ON I.ItemCategory = IC.Code  " & _
                    " LEFT JOIN Division ID ON I.Div_Code = ID.Div_Code  " & _
                    " LEFT JOIN Voucher_Type Vt ON Vt.V_Type = H.V_Type  " & _
                    " LEFT JOIN Currency C ON C.Code = SO.Currency  " & _
                    " LEFT JOIN Unit U ON U.Code = I.Unit  " & _
                    " LEFT JOIN Unit MU ON MU.Code = I.MeasureUnit  " & _
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

#Region "Work Dispatch Report"
    Private Sub ProcWorkDispatchReport()
        Dim mCondStr$ = ""
        Try
            If ReportFrm.FGetText(2) = "Detail" Then
                RepTitle = "Job Work Dispatch Report"
                If ReportFrm.FGetText(3) = "Measure" Then
                    RepName = "Work_WorkDispatchReport_ItemWiseDetail_Measure"
                ElseIf ReportFrm.FGetText(3) = "Qty & Measure" Then
                    RepName = "Work_WorkDispatchReport_ItemWiseDetail_QtyMeasure"
                Else
                    RepName = "Work_WorkDispatchReport_ItemWiseDetail"
                End If
            Else
                RepTitle = "Item Wise Job Work Dispatch Summary"
                If ReportFrm.FGetText(3) = "Measure" Then
                    RepName = "Work_WorkDispatchReport_ItemWiseSummary_Measure"
                ElseIf ReportFrm.FGetText(3) = "Qty & Measure" Then
                    RepName = "Work_WorkDispatchReport_ItemWiseSummary_QtyMeasure"
                Else
                    RepName = "Work_WorkDispatchReport_ItemWiseSummary"
                End If
            End If

            mCondStr = mCondStr & " And H.V_Date Between " & AgL.Chk_Text(ReportFrm.FGetText(0)) & " And " & AgL.Chk_Text(ReportFrm.FGetText(1)) & " "
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.V_Type", 4)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.Party", 5)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.WorkOrder", 6)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Item", 7)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemGroup", 8)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemCategory", 9)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemType ", 10)

            If ReportFrm.FGetText(11) <> "" And ReportFrm.FGetText(11) <> "All" Then
                mQry = " Select '''' +  replace(ItemList,',',''',''')  + ''''  From ItemReportingGroup Where 1=1 " & ReportFrm.GetWhereCondition("Code", 11)
                mCondStr = mCondStr & " AND I.Code In (" & AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar & ")"
            End If


            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.Div_Code ", 12)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.Site_Code ", 13)


            mQry = " SELECT H.DocID, H.V_Type, H.V_Date, H.V_No, H.ManualRefNo, G.Description AS GodownDesc,  H.Remarks, U.DecimalPlaces, " & _
                    " L.Sr, L.Item , L.DocQty, L.Qty, L.Unit, L.MeasurePerPcs, L.MeasureUnit, UM.DecimalPlaces AS MeasureDecimalPlace, " & _
                    " L.Rate, L.Amount, L.Remark AS LineRemark, L.TotalDeliveryMeasure, L.WorkOrder, L.WorkOrderSr , I.Description AS ItemDesc, " & _
                    " SG.DispName + (Case When City.CityName Is Not Null then ', ' +  City.CityName Else '' End) AS PartyName, " & _
                    " SO.ManualRefNo AS WorkOrderNo ,  Vt.Description AS VoucherTypeDesc " & _
                    " FROM WorkDispatch H " & _
                    " LEFT JOIN Godown G ON G.Code = H.Godown  " & _
                    " LEFT JOIN SubGroup SG ON SG.SubCode = H.Party " & _
                    " LEFT JOIN City On SG.CityCode = City.CityCode " & _
                    " LEFT JOIN WorkDispatchDetail L ON L.DocId = H.DocID  " & _
                    " LEFT JOIN Item I ON I.Code = L.Item  " & _
                    " LEFT JOIN WorkOrder SO ON SO.DocID = L.WorkOrder " & _
                    " LEFT JOIN Voucher_Type Vt ON Vt.V_Type = H.V_Type " & _
                    " LEFT JOIN Unit U ON U.Code = L.Unit " & _
                    " LEFT JOIN Unit UM ON UM.Code = L.MeasureUnit " & _
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

#Region "Work Invoice Report"
    Private Sub ProcWorkInvoiceReport()
        Dim mCondStr$ = ""
        Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"
        Dim strQtyFld As String = "''", strQtyFldHead = "''", strUnitFld As String = "''", strUnitFldDecimalPlace As String = "''"
        Dim strQtyFld2 As String = "''", strQtyFldHead2 = "''", strUnitFld2 As String = "''", strUnitFldDecimalPlace2 As String = "''"

        Try
            If ReportFrm.FGetText(3) = "Qty & Amount" Then
                strQtyFld = "L.Qty"
                strQtyFldHead = "'Qty'"
                strUnitFld = "I.Unit"
                strUnitFldDecimalPlace = "U.DecimalPlaces"
                strQtyFld2 = "L." & Replace(ReportFrm.FGetCode(6), "'", "") & ""
                strQtyFldHead2 = "'" & ReportFrm.FGetText(6) & "'"
                strUnitFld2 = "C.Description"
                strUnitFldDecimalPlace2 = "2"
                If ReportFrm.FGetText(2) = "Detail" Then
                    RepName = "Work_WorkInvoiceReport_QtyMeasure" : RepTitle = "Job Work Invoice Report"
                ElseIf ReportFrm.FGetText(2) = "Item Wise Detail" Then
                    RepName = "Work_WorkInvoiceReport_ItemWiseDetail_QtyMeasure" : RepTitle = "Job Work Invoice Report (Item Wise Detail)"
                ElseIf ReportFrm.FGetText(2) = "Party Wise Summary" Then
                    RepName = "Work_WorkInvoiceReport_Summary_QtyMeasure" : RepTitle = "Job Work Invoice Report (" & ReportFrm.FGetText(2) & ")"
                    strGrpFld = "SG.Name"
                    strGrpFldDesc = "SG.Name + ',' + IfNull(City.CityName,'')"
                    strGrpFldHead = "'Party Name'"
                ElseIf ReportFrm.FGetText(2) = "Month Wise Summary" Then
                    RepName = "Work_WorkInvoiceReport_Summary_QtyMeasure" : RepTitle = " Job Work Invoice Report (" & ReportFrm.FGetText(2) & ")"
                    strGrpFld = "Substring(convert(nvarchar,H.V_Date,11),0,6)"
                    strGrpFldDesc = "Replace(SubString(Convert(VARCHAR,H.v_Date,6),4,6),' ','-')"
                    strGrpFldHead = "'Month'"
                ElseIf ReportFrm.FGetText(2) = "Item Wise Summary" Then
                    RepName = "Work_WorkInvoiceReport_Summary_QtyMeasure" : RepTitle = " Job Work Invoice Report (" & ReportFrm.FGetText(2) & ")"
                    strGrpFld = "I.Description"
                    strGrpFldDesc = "I.Description"
                    strGrpFldHead = "'Item'"
                End If
            ElseIf ReportFrm.FGetText(3) = "Measure & Amount" Then
                strQtyFld = "I.Measure*L.Qty"
                strQtyFldHead = "'Measure'"
                strUnitFld = "I.MeasureUnit"
                strUnitFldDecimalPlace = "MU.DecimalPlaces"
                strQtyFld2 = "L." & Replace(ReportFrm.FGetCode(6), "'", "") & ""
                strQtyFldHead2 = "'" & ReportFrm.FGetText(6) & "'"
                strUnitFld2 = "C.Description"
                strUnitFldDecimalPlace2 = "2"
                If ReportFrm.FGetText(2) = "Detail" Then
                    RepName = "Work_WorkInvoiceReport_QtyMeasure" : RepTitle = "Job Work Invoice Report"
                ElseIf ReportFrm.FGetText(2) = "Item Wise Detail" Then
                    RepName = "Work_WorkInvoiceReport_ItemWiseDetail_QtyMeasure" : RepTitle = "Job Work Invoice Report (Item Wise Detail)"
                ElseIf ReportFrm.FGetText(2) = "Party Wise Summary" Then
                    RepName = "Work_WorkInvoiceReport_Summary_QtyMeasure" : RepTitle = "Work Invoice Report (" & ReportFrm.FGetText(2) & ")"
                    strGrpFld = "SG.Name"
                    strGrpFldDesc = "SG.Name + ',' + IfNull(City.CityName,'')"
                    strGrpFldHead = "'Party Name'"
                ElseIf ReportFrm.FGetText(2) = "Month Wise Summary" Then
                    RepName = "Work_WorkInvoiceReport_Summary_QtyMeasure" : RepTitle = " Job Work Invoice Report (" & ReportFrm.FGetText(2) & ")"
                    strGrpFld = "Substring(convert(nvarchar,H.V_Date,11),0,6)"
                    strGrpFldDesc = "Replace(SubString(Convert(VARCHAR,H.v_Date,6),4,6),' ','-')"
                    strGrpFldHead = "'Month'"
                ElseIf ReportFrm.FGetText(2) = "Item Wise Summary" Then
                    RepName = "Work_WorkInvoiceReport_Summary_QtyMeasure" : RepTitle = " Job Work Invoice Report (" & ReportFrm.FGetText(2) & ")"
                    strGrpFld = "I.Description"
                    strGrpFldDesc = "I.Description"
                    strGrpFldHead = "'Item'"
                End If
            ElseIf ReportFrm.FGetText(3) = "Qty & Measure" Then
                strQtyFld = "L.Qty"
                strQtyFldHead = "'Qty'"
                strUnitFld = "I.Unit"
                strUnitFldDecimalPlace = "U.DecimalPlaces"
                strQtyFld2 = "I.Measure*L.Qty"
                strQtyFldHead2 = "'Measure'"
                strUnitFld2 = "I.MeasureUnit"
                strUnitFldDecimalPlace2 = "MU.DecimalPlaces"
                If ReportFrm.FGetText(2) = "Detail" Then
                    RepName = "Work_WorkInvoiceReport_QtyMeasure" : RepTitle = "Job Work Invoice Report"
                ElseIf ReportFrm.FGetText(2) = "Item Wise Detail" Then
                    RepName = "Work_WorkInvoiceReport_ItemWiseDetail_QtyMeasure" : RepTitle = "Job Work Invoice Report (Item Wise Detail)"
                ElseIf ReportFrm.FGetText(2) = "Party Wise Summary" Then
                    RepName = "Work_WorkInvoiceReport_Summary_QtyMeasure" : RepTitle = "Work Invoice Report (" & ReportFrm.FGetText(2) & ")"
                    strGrpFld = "SG.Name"
                    strGrpFldDesc = "SG.Name + ',' + IfNull(City.CityName,'')"
                    strGrpFldHead = "'Party Name'"
                ElseIf ReportFrm.FGetText(2) = "Month Wise Summary" Then
                    RepName = "Work_WorkInvoiceReport_Summary_QtyMeasure" : RepTitle = " Job Work Invoice Report (" & ReportFrm.FGetText(2) & ")"
                    strGrpFld = "Substring(convert(nvarchar,H.V_Date,11),0,6)"
                    strGrpFldDesc = "Replace(SubString(Convert(VARCHAR,H.v_Date,6),4,6),' ','-')"
                    strGrpFldHead = "'Month'"
                ElseIf ReportFrm.FGetText(2) = "Item Wise Summary" Then
                    RepName = "Work_WorkInvoiceReport_Summary_QtyMeasure" : RepTitle = " Job Work Invoice Report (" & ReportFrm.FGetText(2) & ")"
                    strGrpFld = "I.Description"
                    strGrpFldDesc = "I.Description"
                    strGrpFldHead = "'Item'"
                End If

            ElseIf ReportFrm.FGetText(3) = "Qty" Then
                strQtyFld = "L.Qty"
                strQtyFldHead = "'Qty'"
                strUnitFld = "I.Unit"
                strUnitFldDecimalPlace = "U.DecimalPlaces"
                strQtyFld2 = "I.Measure*L.Qty"
                strQtyFldHead2 = "'Measure'"
                strUnitFld2 = "I.MeasureUnit"
                strUnitFldDecimalPlace2 = "MU.DecimalPlaces"
                If ReportFrm.FGetText(2) = "Detail" Then
                    RepName = "Work_WorkInvoiceReport" : RepTitle = "Job Work Invoice Report"
                ElseIf ReportFrm.FGetText(2) = "Item Wise Detail" Then
                    RepName = "Work_WorkInvoiceReport_ItemWiseDetail" : RepTitle = "Item Wise Job Work Invoice Report"
                ElseIf ReportFrm.FGetText(2) = "Party Wise Summary" Then
                    RepName = "Work_WorkInvoiceReport_Summary" : RepTitle = "Work Invoice Report (" & ReportFrm.FGetText(2) & ")"
                    strGrpFld = "SG.Name"
                    strGrpFldDesc = "SG.Name + ',' + IfNull(City.CityName,'')"
                    strGrpFldHead = "'Party Name'"
                ElseIf ReportFrm.FGetText(2) = "Month Wise Summary" Then
                    RepName = "Work_WorkInvoiceReport_Summary" : RepTitle = " Job Work Invoice Report (" & ReportFrm.FGetText(2) & ")"
                    strGrpFld = "Substring(convert(nvarchar,H.V_Date,11),0,6)"
                    strGrpFldDesc = "Replace(SubString(Convert(VARCHAR,H.v_Date,6),4,6),' ','-')"
                    strGrpFldHead = "'Month'"
                ElseIf ReportFrm.FGetText(2) = "Item Wise Summary" Then
                    RepName = "Work_WorkInvoiceReport_Summary" : RepTitle = " Job Work Invoice Report (" & ReportFrm.FGetText(2) & ")"
                    strGrpFld = "I.Description"
                    strGrpFldDesc = "I.Description"
                    strGrpFldHead = "'Item'"
                End If

            ElseIf ReportFrm.FGetText(3) = "Measure" Then
                strQtyFld = "I.Measure*L.Qty"
                strQtyFldHead = "'Measure'"
                strUnitFld = "I.MeasureUnit"
                strUnitFldDecimalPlace = "MU.DecimalPlaces"
                strQtyFld2 = "L." & Replace(ReportFrm.FGetCode(6), "'", "") & ""
                strQtyFldHead2 = "'" & ReportFrm.FGetText(6) & "'"
                strUnitFld2 = "C.Description"
                strUnitFldDecimalPlace2 = "2"
                If ReportFrm.FGetText(2) = "Detail" Then
                    RepName = "Work_WorkInvoiceReport" : RepTitle = "Job Work Invoice Report"
                ElseIf ReportFrm.FGetText(2) = "Item Wise Detail" Then
                    RepName = "Work_WorkInvoiceReport_ItemWiseDetail" : RepTitle = "Job Work Invoice Report (Item Wise Detail)"
                ElseIf ReportFrm.FGetText(2) = "Party Wise Summary" Then
                    RepName = "Work_WorkInvoiceReport_Summary" : RepTitle = "Work Invoice Report (" & ReportFrm.FGetText(2) & ")"
                    strGrpFld = "SG.Name"
                    strGrpFldDesc = "SG.Name + ',' + IfNull(City.CityName,'')"
                    strGrpFldHead = "'Party Name'"
                ElseIf ReportFrm.FGetText(2) = "Month Wise Summary" Then
                    RepName = "Work_WorkInvoiceReport_Summary" : RepTitle = " Job Work Invoice Report (" & ReportFrm.FGetText(2) & ")"
                    strGrpFld = "Substring(convert(nvarchar,H.V_Date,11),0,6)"
                    strGrpFldDesc = "Replace(SubString(Convert(VARCHAR,H.v_Date,6),4,6),' ','-')"
                    strGrpFldHead = "'Month'"
                ElseIf ReportFrm.FGetText(2) = "Item Wise Summary" Then
                    RepName = "Work_WorkInvoiceReport_Summary" : RepTitle = " Job Work Invoice Report (" & ReportFrm.FGetText(2) & ")"
                    strGrpFld = "I.Description"
                    strGrpFldDesc = "I.Description"
                    strGrpFldHead = "'Item'"
                End If
            ElseIf ReportFrm.FGetText(3) = "Amount" Then
                strQtyFld = "L." & Replace(ReportFrm.FGetCode(6), "'", "") & ""
                strQtyFldHead = "'" & ReportFrm.FGetText(6) & "'"
                strUnitFld = "C.Description"
                strUnitFldDecimalPlace = "2"
                strQtyFld2 = "I.Measure*L.Qty"
                strQtyFldHead2 = "'Measure'"
                strUnitFld2 = "I.MeasureUnit"
                strUnitFldDecimalPlace2 = "MU.DecimalPlaces"
                If ReportFrm.FGetText(2) = "Detail" Then
                    RepName = "Work_WorkInvoiceReport" : RepTitle = "Job Work Invoice Report"
                ElseIf ReportFrm.FGetText(2) = "Item Wise Detail" Then
                    RepName = "Work_WorkInvoiceReport_ItemWiseDetail" : RepTitle = "Job Work Invoice Report (Item Wise Detail)"
                ElseIf ReportFrm.FGetText(2) = "Party Wise Summary" Then
                    RepName = "Work_WorkInvoiceReport_Summary" : RepTitle = "Work Invoice Report (" & ReportFrm.FGetText(2) & ")"
                    strGrpFld = "SG.Name"
                    strGrpFldDesc = "SG.Name + ',' + IfNull(City.CityName,'')"
                    strGrpFldHead = "'Party Name'"
                ElseIf ReportFrm.FGetText(2) = "Month Wise Summary" Then
                    RepName = "Work_WorkInvoiceReport_Summary" : RepTitle = " Job Work Invoice Report (" & ReportFrm.FGetText(2) & ")"
                    strGrpFld = "Substring(convert(nvarchar,H.V_Date,11),0,6)"
                    strGrpFldDesc = "Replace(SubString(Convert(VARCHAR,H.v_Date,6),4,6),' ','-')"
                    strGrpFldHead = "'Month'"
                ElseIf ReportFrm.FGetText(2) = "Item Wise Summary" Then
                    RepName = "Work_WorkInvoiceReport_Summary" : RepTitle = " Job Work Invoice Report (" & ReportFrm.FGetText(2) & ")"
                    strGrpFld = "I.Description"
                    strGrpFldDesc = "I.Description"
                    strGrpFldHead = "'Item'"
                End If
            End If


            mCondStr = mCondStr & " And H.V_Date Between " & AgL.Chk_Text(ReportFrm.FGetText(0)) & " And " & AgL.Chk_Text(ReportFrm.FGetText(1)) & " "
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.V_Type", 4)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.Party", 5)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.WorkOrder", 7)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.WorkDispatch", 8)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.WorkInvoice", 9)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Item", 10)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemGroup", 11)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemCategory", 12)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemType ", 13)

            mCondStr = mCondStr & VtypeRestriction

            If ReportFrm.FGetText(14) <> "" And ReportFrm.FGetText(14) <> "All" Then
                mQry = " Select '''' +  replace(ItemList,',',''',''')  + ''''  From ItemReportingGroup Where 1=1 " & ReportFrm.GetWhereCondition("Code", 14)
                mCondStr = mCondStr & " AND I.Code In (" & AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar & ")"
            End If

            mCondStr = mCondStr & " And H.Site_Code = '" & AgL.PubSiteCode & "' "
            mCondStr = mCondStr & " And H.Div_Code= '" & AgL.PubDivCode & "' "

            mQry = " SELECT " & strGrpFld & " as GrpField, " & strGrpFldDesc & " as GrpFieldDesc, " & strGrpFldHead & " as GrpFieldHead, C.Description AS CurrencDesc," & _
                    " H.DocID, H.V_Type, H.V_Date, H.ManualrefNo, SG.DispName + (Case When City.CityName Is Not Null then ', ' +  City.CityName Else '' End) AS VendorName, " & _
                    " L.Sr, L.WorkDispatch, L.Item, L.Qty, L.Unit, U.DecimalPlaces AS QtyDecimalplace, L.MeasurePerPcs , L.MeasureUnit, L.TotalDeliveryMeasure AS TotalMeasure,  " & _
                    " L.Rate , L.WorkDispatchSr, L." & Replace(ReportFrm.FGetCode(6), "'", "") & " as Amount, '" & ReportFrm.FGetText(6) & "' as AmountTitle, IfNull(L." & Replace(ReportFrm.FGetCode(6), "'", "") & ",0)/L.Qty AS NetAmtRate, I.Description AS ItemDesc,  " & _
                    " " & strQtyFld & " as PrnQtyField, " & strQtyFldHead & " as PrnQtyFieldHead, " & strUnitFld & " as PrnUnitField, " & strUnitFldDecimalPlace & " as PrnDecimalPlaces, " & _
                    " " & strQtyFld2 & " as PrnQtyField2, " & strQtyFldHead2 & " as PrnQtyFieldHead2, " & strUnitFld2 & " as PrnUnitField2, " & strUnitFldDecimalPlace2 & " as PrnDecimalPlaces2, " & _
                    " Vt.Description AS VoucherTypeDesc, PC.V_Type + '- ' + PC.ManualrefNo AS ChallanNo, H.Remarks as H_Remarks, L.Remark as L_Remarks  " & _
                    " FROM WorkInvoice H " & _
                    " LEFT JOIN SubGroup SG ON SG.SubCode = H.Party  " & _
                    " LEFT JOIN City On SG.CityCode = City.CityCode " & _
                    " LEFT JOIN WorkInvoiceDetail L ON L.DocId = H.DocID  " & _
                    " LEFT JOIN Item I ON I.Code = L.Item " & _
                    " LEFT Join Currency C On C.Code = H.Currency " & _
                    " LEFT JOIN Voucher_Type Vt ON Vt.V_Type = H.V_Type " & _
                    " LEFT JOIN WorkDispatch PC ON PC.DocID = L.WorkDispatch " & _
                    " LEFT JOIN Unit U ON U.Code = I.Unit " & _
                    " LEFT JOIN Unit MU ON MU.Code = I.MeasureUnit  " & _
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

#Region "Work Order Status Report"
    Private Sub ProcWorkOrderStatusReport()
        Dim mCondStr$ = ""
        Dim mStrSortOn$ = ""

        Dim mQryWorkDispatch$ = " SELECT PCD.WorkOrder, PCD.WorkOrderSr , Max(PC.ManualrefNo) AS ChallanNo, Max(PC.V_Date) AS ChallanDate, Sum(PCD.Qty) AS ChallanQty, Sum(PCD.TotalDeliveryMeasure) AS ChallanMeasure " & _
                    " FROM WorkDispatchDetail PCD " & _
                    " LEFT JOIN WorkDispatch PC ON PC.DocID = PCD.DocId  " & _
                    " WHERE IfNull( PCD.WorkOrder,'') <> '' " & _
                    " AND PC.V_Date <= '" & ReportFrm.FGetText(2) & "' " & _
                    " Group By PCD.WorkOrder, PCD.WorkOrderSr, PCD.WorkDispatch, PCD.LotNo "

        Dim mQryWorkDispatchSummury$ = " SELECT PCD.WorkOrder, PCD.WorkOrderSr ,  sum(PCD.Qty) AS TotalChallanQty, sum(PCD.TotalDeliveryMeasure) AS TotalChallanMeasure, max(PC.V_Date) AS MaxChallanDate " & _
                    " FROM WorkDispatchDetail PCD " & _
                    " LEFT JOIN WorkDispatch PC ON PC.DocID = PCD.DocId  " & _
                    " WHERE IfNull( PCD.WorkOrder,'') <> '' " & _
                    " AND PC.V_Date <= '" & ReportFrm.FGetText(2) & "' " & _
                    " GROUP BY PCD.WorkOrder, PCD.WorkOrderSr "

        Dim mQryWorkOrder$ = " SELECT POD.WorkOrder, POD.WorkOrderSr, sum(POD.Qty) AS BalOrdQty, sum(POD.TotalDeliveryMeasure) AS BalOrdMeasure  " & _
                            " FROM WorkOrderDetail POD  " & _
                            " LEFT JOIN WorkOrder PO ON PO.DocId = POD.DocId " & _
                            " WHERE IfNull(POD.WorkOrder,'') <> '' " & _
                            " AND PO.V_Date <= '" & ReportFrm.FGetText(2) & "' " & _
                            " GROUP BY POD.WorkOrder, POD.WorkOrderSr "
        Try

            If ReportFrm.FGetText(3) = "Detail" Then
                RepTitle = "Job Work Order Status Report"
                If ReportFrm.FGetText(5) = "Measure" Then
                    RepName = "Work_WorkOrderStatusReport_Detail_Measure"
                ElseIf ReportFrm.FGetText(5) = "Qty & Measure" Then
                    RepName = "Work_WorkOrderStatusReport_Detail_QtyMeasure"
                Else
                    RepName = "Work_WorkOrderStatusReport_Detail"
                End If
            ElseIf ReportFrm.FGetText(3) = "Item Wise Order Status" Then
                RepTitle = "Item Wise Job Work Order Status"
                If ReportFrm.FGetText(5) = "Measure" Then
                    RepName = "Work_WorkOrderStatusReport_ItemWise_Measure"
                ElseIf ReportFrm.FGetText(5) = "Qty & Measure" Then
                    RepName = "Work_WorkOrderStatusReport_ItemWise_QtyMeasure"
                Else
                    RepName = "Work_WorkOrderStatusReport_ItemWise"
                End If
            ElseIf ReportFrm.FGetText(3) = "Summary with Amount" Then
                RepTitle = "Item Wise Job Work Order Status"
                If ReportFrm.FGetText(5) = "Measure" Then
                    RepName = "Work_WorkOrderStatuswithAmount_Measure"
                ElseIf ReportFrm.FGetText(5) = "Qty & Measure" Then
                    RepName = "Work_WorkOrderStatuswithAmount_QtyMeasure"
                Else
                    RepName = "Work_WorkOrderStatuswithAmount"
                End If

            ElseIf ReportFrm.FGetText(3) = "Work Order Wise Detail" Then
                RepTitle = "Job Work Order Status"
                If ReportFrm.FGetText(5) = "Measure" Then
                    RepName = "Work_WorkOrderStatus_Measure_WorkOrderWise"
                ElseIf ReportFrm.FGetText(5) = "Qty & Measure" Then
                    RepName = "Work_WorkOrderStatus_QtyMeasure_WorkOrderWise"
                Else
                    RepName = "Work_WorkOrderStatus_WorkOrderWise"
                End If
            Else
                RepTitle = "Work Order Status Report"
                If ReportFrm.FGetText(5) = "Measure" Then
                    RepName = "Work_WorkOrderStatusReport_Measure"
                ElseIf ReportFrm.FGetText(5) = "Qty & Measure" Then
                    RepName = "Work_WorkOrderStatusReport_QtyMeasure"
                Else
                    RepName = "Work_WorkOrderStatusReport"
                End If
            End If

            If ReportFrm.FGetText(6) = "Order Date" Then
                mStrSortOn = "H.V_Date"
            ElseIf ReportFrm.FGetText(6) = "Due Date" Then
                mStrSortOn = "H.PartyDeliveryDate"
            ElseIf ReportFrm.FGetText(6) = "Over Due Days" Then
                mStrSortOn = " CASE WHEN Round(IfNull(VPO.BalOrdQty,0),4) - Round(IfNull(VPCS.TotalChallanQty,0),4) > 0 THEN  datediff(Day,H.PartyDeliveryDate,'" & ReportFrm.FGetText(2) & "') ELSE datediff(Day,H.PartyDeliveryDate,VPCS.MaxChallanDate) END "
            ElseIf ReportFrm.FGetText(6) = "Balance Qty" Then
                mStrSortOn = " Round(IfNull(VPO.BalOrdQty,0),4) - Round(IfNull(VPCS.TotalChallanQty,0),4) "
            End If

            mCondStr = mCondStr & "And H.V_Date Between " & AgL.Chk_Text(ReportFrm.FGetText(0)) & " And " & AgL.Chk_Text(ReportFrm.FGetText(1)) & " "
            mCondStr = mCondStr & " And Vt.NCat = " & AgL.Chk_Text(AgTemplate.ClsMain.Temp_NCat.WorkOrder) & " "

            If ReportFrm.FGetText(4) = "Pending For Dispatch" Then
                mCondStr = mCondStr & " AND Round(IfNull(VPO.BalOrdQty,0),4) - Round(IfNull(VPCS.TotalChallanQty,0),4) > 0 "
            ElseIf ReportFrm.FGetText(4) = "Dispatched" Then
                mCondStr = mCondStr & " AND Round(IfNull(VPO.BalOrdQty,0),4) - Round(IfNull(VPCS.TotalChallanQty,0),4) <= 0 "
            ElseIf ReportFrm.FGetText(4) = "Over Due" Then
                mCondStr = mCondStr & "  AND H.PartyDeliveryDate < IfNull(VPC.ChallanDate,'" & ReportFrm.FGetText(2) & "') "
            ElseIf ReportFrm.FGetText(4) = "Over Due And Pending" Then
                mCondStr = mCondStr & " AND Round(IfNull(VPO.BalOrdQty,0),4) - Round(IfNull(VPCS.TotalChallanQty,0),4) > 0 "
                mCondStr = mCondStr & " AND H.PartyDeliveryDate < IfNull(VPC.ChallanDate,'" & ReportFrm.FGetText(2) & "') "
            ElseIf ReportFrm.FGetText(4) = "Timely Dispatched" Then
                mCondStr = mCondStr & " AND Round(IfNull(VPO.BalOrdQty,0),4) - Round(IfNull(VPCS.TotalChallanQty,0),4) <= 0 "
                mCondStr = mCondStr & " AND H.PartyDeliveryDate >= " & _
                                        " ( SELECT Max(SC.V_Date)  " & _
                                        " FROM WorkDispatchDetail SCD " & _
                                        " LEFT JOIN WorkDispatch SC ON SC.DocID = SCD.DocId  " & _
                                        " WHERE SCD.WorkOrder = H.DocId " & _
                                        " GROUP BY SCD.WorkOrder  " & _
                                        " ) "
            End If

            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.V_Type", 7)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.WorkToParty", 8)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.Agent", 9)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.WorkOrder ", 10)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Item", 11)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemGroup", 12)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemCategory ", 13)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemType ", 14)


            If ReportFrm.FGetText(15) <> "" And ReportFrm.FGetText(15) <> "All" Then
                mQry = " Select '''' +  replace(ItemList,',',''',''')  + ''''  From ItemReportingGroup Where 1=1 " & ReportFrm.GetWhereCondition("Code", 15)
                mCondStr = mCondStr & " AND I.Code In (" & AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar & ")"
            End If


            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.Div_Code ", 16)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.Site_Code ", 17)

            mQry = " SELECT H.DocID, H.V_Type, H.V_Date, H.ManualRefNo, C.Description AS Currency, U.DecimalPlaces, UM.DecimalPlaces AS MeasureDecimalPlace, H.PartyDeliveryDate," & _
                    " SG.DispName + (Case When City.CityName Is Not Null then ', ' +  City.CityName Else '' End) AS WorkToPartyName, " & _
                    " H.PartyOrderNo, H.PartyOrderDate, H.Remarks, L.Sr, CASE WHEN IsNumeric(H.ReferenceNo) > 0 THEN Convert(INT, H.ReferenceNo) ELSE 0 END AS  OrderNo," & _
                    " L.Item, L.Qty, L.Unit, L.MeasurePerPcs, L.MeasureUnit, L.TotalDeliveryMeasure AS TotalMeasure, L.Rate, L.Amount, L.RatePerQty, " & _
                    " L.Remark AS LineRemark, L.RateType, I.Description AS ItemDesc, Vt.Description AS VoucherTypeDesc,  " & _
                    " VPC.ChallanNo, VPC.ChallanDate, IfNull(VPC.ChallanQty,0) AS ChallanQty,  IfNull(VPC.ChallanMeasure,0) AS ChallanMeasure , " & _
                    " CASE WHEN IfNull(VPO.BalOrdQty,0) - IfNull(VPCS.TotalChallanQty,0) > 0 THEN  datediff(Day,H.PartyDeliveryDate,'" & ReportFrm.FGetText(2) & "') ELSE datediff(Day,H.PartyDeliveryDate,VPCS.MaxChallanDate) END AS Ageing, " & _
                    " IfNull(VPO.BalOrdQty,0) AS TotalOrdQty, IfNull(VPO.BalOrdMeasure,0) AS TotalOrdMeasure,  " & mStrSortOn & " AS OrderOn, " & _
                    " IfNull(VPO.BalOrdQty,0) - IfNull(VPCS.TotalChallanQty,0) AS TotalBalQty,  IfNull(VPO.BalOrdMeasure,0) - IfNull(VPCS.TotalChallanMeasure,0) AS TotalBalMeasure, (IfNull(VPO.BalOrdQty,0) - IfNull(VPCS.TotalChallanQty,0)) * IfNull(L.RatePerQty,0) AS TotalBalAmount " & _
                    " FROM WorkOrder H " & _
                    " LEFT JOIN WorkOrderDetail L ON L.DocId = H.DocID  " & _
                    " LEFT JOIN SubGroup SG ON SG.SubCode = H.Party  " & _
                    " LEFT JOIN City On SG.CityCode = City.CityCode " & _
                    " LEFT JOIN Item I ON I.Code = L.Item  " & _
                    " LEFT JOIN Voucher_Type Vt ON Vt.V_Type = H.V_Type " & _
                    " LEFT JOIN Unit U ON U.Code = L.Unit " & _
                    " LEFT JOIN Unit UM ON UM.Code = L.MeasureUnit " & _
                    " LEFT JOIN Currency C ON C.Code = H.Currency  " & _
                    " LEFT JOIN ( " & mQryWorkDispatch & " ) VPC ON VPC.WorkOrder = L.DocId AND VPC.WorkOrderSr = L.Sr " & _
                    " LEFT JOIN ( " & mQryWorkDispatchSummury & " ) VPCS ON VPCS.WorkOrder = L.DocId AND VPCS.WorkOrderSr = L.Sr " & _
                    " LEFT JOIN ( " & mQryWorkOrder & " ) VPO ON VPO.WorkOrder = L.DocId AND VPO.WorkOrderSr = L.Sr " & _
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
