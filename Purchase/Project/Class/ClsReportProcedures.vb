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
    Private Const PurchaseIndentReport As String = "PurchaseIndentReport"
    Private Const PurchaseIndentStatus As String = "PurchaseIndentStatus"
    Private Const PurchaseOrderReport As String = "PurchaseOrderReport"
    Private Const PurchaseOrderStatus As String = "PurchaseOrderStatus"
    Private Const PurchaseChallanReport As String = "PurchaseChallanReport"
    Private Const PurchaseChallanStatus As String = "PurchaseChallanStatus"
    Private Const PurchaseInvoiceReport As String = "PurchaseInvoiceReport"
    Private Const PurchaseReturnReport As String = "PurchaseReturnReport"
#End Region

#Region "Queries Definition"
    'Dim VtypeRestriction$ = " AND H.V_Type NOT IN " & _
    '                    " ( Select L.V_Type " & _
    '                    " FROM User_Exclude_VTypeDetail L  " & _
    '                    " WHERE L.UserName = " & AgL.Chk_Text(AgL.PubUserName) & " ) "
    Dim VtypeRestriction$ = "  "

    Dim mHelpSiteQry$ = "Select 'o' As Tick, Code, Name AS [Site / Branch] FROM SiteMast WHERE CharIndex('|' || Code || '|', (SELECT Max(SiteList) FROM UserSite WHERE User_Name = 'SA'))>0 OR '" & AgL.PubUserName & "' IN ('SA','" & AgLibrary.ClsConstant.PubSuperUserName & "')"
    Dim mHelpDivisionQry$ = "Select 'o' As Tick, Div_Code AS Code, Div_Name AS [Division] FROM Division WHERE CharIndex('|' || Div_Code || '|', (SELECT Max(DivisionList) FROM UserSite WHERE User_Name = 'SA'))>0 OR '" & AgL.PubUserName & "' IN ('SA','" & AgLibrary.ClsConstant.PubSuperUserName & "')"
    Dim mHelpCityQry$ = "Select 'o' As Tick, CityCode, CityName From City "
    Dim mHelpStateQry$ = "Select 'o' As Tick, State_Code, State_Desc From State "
    Dim mHelpUserQry$ = "Select 'o' As Tick, User_Name As Code, User_Name As [User] From UserMast "
    Dim mHelpCurrencyQry$ = "Select 'o' As Tick, Code, Code, Description From Currency "
    Dim mHelpGodownQry$ = "Select 'o' As Tick, Code, Description FROM Godown WHERE Status = 'Active' "
    Dim mHelpSalesTaxGroupParty$ = "Select 'o' As Tick, Description AS Code, Description FROM PostingGroupSalesTaxParty "
    Dim mHelpSalesTaxGroupItem$ = "Select 'o' As Tick, Description AS Code, Description FROM PostingGroupSalesTaxItem "
    Dim mHelpItemQry$ = "Select 'o' As Tick, I.Code, I.Description As [Item], IG.Description as [Item Group], IC.Description as [Item Category] " &
                        "From Item I " &
                        "Left JOIN ItemGroup IG ON I.ItemGroup = IG.Code " &
                        "LEFT JOIN ItemCategory IC ON IG.ItemCategory = IC.Code  " &
                        "LEFT JOIN ItemType IT ON IC.ItemType = IT.Code  "

    Dim mHelpItemGroupQry$ = "Select 'o' As Tick, IG.Code, IG.Description As [Item Group], IC.Description as [Item Category], IT.Name as [Item Type] " &
                             "From ItemGroup IG " &
                             "LEFT JOIN ItemCategory IC ON IG.ItemCategory = IC.Code  " &
                             "LEFT JOIN ItemType IT ON IC.ItemType = IT.Code  "
    Dim mHelpItemCategoryQry$ = "Select 'o' As Tick, IC.Code, IC.Description As [Item Category], IT.Name as [Item Type] " &
                                "From ItemCategory IC " &
                                "LEFT JOIN ItemType IT ON IC.ItemType = IT.Code  "
    Dim mHelpItemReportingGroupQry$ = "Select 'o' As Tick, Code, Description As [Group Name] From ItemReportingGroup "

    Dim mHelpVendorQry$ = " Select 'o' As Tick,  SG.SubCode As Code, SG.Name AS Vendor, C.CityName AS City FROM SubGroup Sg " &
                            " LEFT JOIN City C ON C.CityCode = SG.CityCode  " &
                            " WHERE SG.Nature ='Supplier' " &
                            " AND SG.Site_Code = '" & AgL.PubSiteCode & "' " &
                            " And IfNull(SG.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "'"

    Dim mHelpEmployeeQry$ = " Select 'o' As Tick, SG.SubCode AS Code, SG.Name AS Employee  " &
           " FROM SubGroup Sg " &
           " WHERE IfNull(SG.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "'" &
           " AND SG.Div_Code ='" & AgL.PubDivCode & " ' AND SG.Site_Code = '" & AgL.PubSiteCode & "' ORDER BY SG.Name"
    Dim mHelpItemType$ = "SELECT 'o' AS Tick, Code, Name  AS ItemType  FROM ItemType ORDER BY Name "
    Dim mHelpDepartment$ = "SELECT 'o' AS Tick, Code, Description AS Department FROM Department ORDER BY Description "

    Dim mHelpIndentNo$ = " Select 'o' As Tick, H.DocID, Max(H.V_Type || '- ' || H.ManualRefNo) AS IndentNo , Max(H.V_Date) AS IndentDate " &
                        " FROM PurchIndentDetail L  " &
                        " LEFT JOIN PurchIndent H ON L.PurchIndent  = H.DocID " &
                        " WHERE H.Div_Code = '" & AgL.PubDivCode & "'  AND H.Site_Code = '" & AgL.PubSiteCode & "' " &
                        " " & VtypeRestriction & " Group By H.DocID "

    Dim mHelpPurchaseOrder$ = " Select 'o' As Tick, H.DocID, Max(H.V_Type || '- ' || H.ReferenceNo) AS OrderNo , Max(H.V_Date) AS OrderDate " &
                        " FROM PurchOrderDetail L  " &
                        " LEFT JOIN PurchOrder H ON L.PurchOrder  = H.DocID " &
                        " WHERE H.Div_Code = '" & AgL.PubDivCode & "'  And H.Site_Code = '" & AgL.PubSiteCode & "' " &
                        " " & VtypeRestriction & " Group By H.DocID "

    Dim mHelpPurchaseChallan$ = " Select 'o' As Tick, H.DocID, Max(H.V_Type || '- ' || H.ReferenceNo) AS ChallanNo , Max(H.V_Date) AS ChallanDate " &
                " FROM PurchChallanDetail L  " &
                " LEFT JOIN PurchChallan H ON L.PurchChallan  = H.DocID " &
                " WHERE H.Div_Code = '" & AgL.PubDivCode & "'  AND H.Site_Code = '" & AgL.PubSiteCode & "' " &
                " " & VtypeRestriction & " Group By H.DocID "

    Dim mHelpPurchaseInvoice$ = " Select 'o' As Tick, H.DocID, Max(H.V_Type || '- ' || H.ReferenceNo) AS InvoiceNo , Max(H.V_Date) AS InvoiceDate " &
                " FROM PurchInvoiceDetail L  " &
                " LEFT JOIN PurchInvoice H ON L.PurchInvoice  = H.DocID " &
                " WHERE H.Div_Code = '" & AgL.PubDivCode & "'  AND H.Site_Code = '" & AgL.PubSiteCode & "' " &
                " " & VtypeRestriction & " Group By H.DocID "

    Dim mHelpProdOrderQry$ = " Select 'o' As Tick, H.DocID AS Code, H.V_Type || '-' || H.ManualRefNo AS [Manual No] , H.V_Date AS OrderDate " &
         " FROM ProdOrder H " &
         " WHERE H.Div_Code ='" & AgL.PubDivCode & " ' And H.Site_Code = '" & AgL.PubSiteCode & "' " & VtypeRestriction & " "

    Dim mHelpMaterialPlanNo$ = " Select 'o' As Tick, H.DocID, H.V_Type +'-'+ H.ManualRefNo AS [Production Plan No.], " &
                            " H.V_Date AS [Date] " &
                            " FROM MaterialPlan H  " &
                            " LEFT JOIN Voucher_Type Vt ON Vt.V_Type=H.V_Type " &
                            " WHERE H.Div_Code = '" & AgL.PubDivCode & "'  AND H.Site_Code = '" & AgL.PubSiteCode & "' " & VtypeRestriction & " "

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
                Case PurchaseIndentReport
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubEndDate)
                    ReportFrm.CreateHelpGrid("Unit", "Unit", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, "Select 'Qty' as Code, 'Qty' as Name Union All Select 'Measure' as Code, 'Measure' AS Name Union All Select 'Qty & Measure' as Code, 'Qty & Measure' AS Name ", "Qty")
                    ReportFrm.CreateHelpGrid("Voucher Type", "Voucher Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, FGetVoucher_TypeQry("PurchIndent"))
                    ReportFrm.CreateHelpGrid("Indentor", "Indentor", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpEmployeeQry, , , , 280)
                    ReportFrm.CreateHelpGrid("Department", "Department", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpDepartment)
                    ReportFrm.CreateHelpGrid("Material Plan No", "Material Plan No", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpMaterialPlanNo, , , 450)
                    ReportFrm.CreateHelpGrid("Indent No", "Indent No", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpIndentNo, , , 450)
                    ReportFrm.CreateHelpGrid("Item", "Item", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemQry, , , 600, 270)
                    ReportFrm.CreateHelpGrid("Item Group", "Item Group", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemGroupQry, , , 530)
                    ReportFrm.CreateHelpGrid("Item Category", "Item Category", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemCategoryQry, , , 430)
                    ReportFrm.CreateHelpGrid("Item Type", "Item Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemType)
                    ReportFrm.CreateHelpGrid("Item Reporting Group", "Item Reporting Group", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemReportingGroupQry, , , 500, 360)
                    ReportFrm.CreateHelpGrid(ClsMain.FGetDimension1Caption(), ClsMain.FGetDimension1Caption(), ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpDimension1)
                    ReportFrm.CreateHelpGrid(ClsMain.FGetDimension2Caption(), ClsMain.FGetDimension2Caption(), ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpDimension2)

                Case PurchaseIndentStatus
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubEndDate)
                    ReportFrm.CreateHelpGrid("Status On", "Status On", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubLoginDate)
                    ReportFrm.CreateHelpGrid("Report Type", "Report Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, "Select 'Order Summary' as Code, 'Order Summary' as Name Union All Select 'Order Detail' as Code, 'Order Detail' as Name Union All Select 'Order & Challan Summary' as Code, 'Order & Challan Summary' as Name Union All Select 'Order & Challan Detail' as Code, 'Order & Challan Detail' as Name", "Order Summary")
                    ReportFrm.CreateHelpGrid("Report For", "Report For", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, " SELECT 'All' AS Code, 'All' AS Name UNION ALL  SELECT 'Pending To Purchase Order' AS Code, 'Pending To Purchase Order' AS Name ")
                    ReportFrm.CreateHelpGrid("Unit", "Unit", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, "Select 'Qty' as Code, 'Qty' as Name Union All Select 'Measure' as Code, 'Measure' AS Name Union All Select 'Qty & Measure' as Code, 'Qty & Measure' AS Name ", "Qty")
                    ReportFrm.CreateHelpGrid("Voucher Type", "Voucher Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, FGetMainVoucher_TypeQry("PurchIndentDetail", "LEFT JOIN PurchIndent H ON H.DocID = L.PurchIndent"))
                    ReportFrm.CreateHelpGrid("Indentor", "Indentor", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpEmployeeQry, , , , 280)
                    ReportFrm.CreateHelpGrid("Department", "Department", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpDepartment)
                    ReportFrm.CreateHelpGrid("Material Plan No", "Material Plan No", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpMaterialPlanNo, , , 450)
                    ReportFrm.CreateHelpGrid("Indent No", "Indent No", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpIndentNo, , , 450)
                    ReportFrm.CreateHelpGrid("Item", "Item", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemQry, , , 600, 270)
                    ReportFrm.CreateHelpGrid("Item Group", "Item Group", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemGroupQry, , , 530)
                    ReportFrm.CreateHelpGrid("Item Category", "Item Category", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemCategoryQry, , , 430)
                    ReportFrm.CreateHelpGrid("Item Type", "Item Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemType)
                    ReportFrm.CreateHelpGrid("Item Reporting Group", "Item Reporting Group", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemReportingGroupQry, , , 500, 360)
                    ReportFrm.CreateHelpGrid(ClsMain.FGetDimension1Caption(), ClsMain.FGetDimension1Caption(), ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpDimension1)
                    ReportFrm.CreateHelpGrid(ClsMain.FGetDimension2Caption(), ClsMain.FGetDimension2Caption(), ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpDimension2)

                Case PurchaseOrderReport
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubEndDate)
                    ReportFrm.CreateHelpGrid("Report Type", "Report Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, "Select 'Detail' as Code, 'Detail' as Name Union All Select 'Item Wise Detail' as Code, 'Item Wise Detail' as Name Union All Select 'Item Wise Detail with Amount' as Code, 'Item Wise Detail with Amount' as Name Union All Select 'Item Wise Summary' as Code, 'Item Wise Summary' as Name Union All Select 'Item Wise Summary with Amount' as Code, 'Item Wise Summary with Amount' as Name Union All Select 'Month Wise Summary' as Code, 'Month Wise Summary' as Name Union All Select 'Party Wise Summary' as Code, 'Party Wise Summary' as Name", "Detail", , , 250, , False)
                    ReportFrm.CreateHelpGrid("Unit", "Unit", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, "Select 'Qty' as Code, 'Qty' as Name Union All Select 'Measure' as Code, 'Measure' AS Name Union All Select 'Qty & Measure' as Code, 'Qty & Measure' AS Name ", "Qty")
                    ReportFrm.CreateHelpGrid("Voucher Type", "Voucher Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, FGetVoucher_TypeQry("PurchOrder"))
                    ReportFrm.CreateHelpGrid("Party", "Party", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpVendorQry, , , 550, 300)
                    ReportFrm.CreateHelpGrid("Code", "Amount Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, FGetStructureFieldsQry(AgTemplate.ClsMain.Temp_NCat.PurchaseOrder), "Amount|Amount", , , , , False)
                    ReportFrm.CreateHelpGrid("Indent No", "Indent No", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpIndentNo, , , 450)
                    ReportFrm.CreateHelpGrid("Purchase Order No", "Purchase Order No", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpPurchaseOrder, , , 450)
                    ReportFrm.CreateHelpGrid("Item", "Item", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemQry, , , 600, 270)
                    ReportFrm.CreateHelpGrid("Item Group", "Item Group", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemGroupQry, , , 530)
                    ReportFrm.CreateHelpGrid("Item Category", "Item Category", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemCategoryQry, , , 430)
                    ReportFrm.CreateHelpGrid("Item Type", "Item Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemType)
                    ReportFrm.CreateHelpGrid("Item Reporting Group", "Item Reporting Group", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemReportingGroupQry, , , 500, 360)
                    ReportFrm.CreateHelpGrid("Currency", "Currency", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpCurrencyQry, , , 600, 270)
                    ReportFrm.CreateHelpGrid(ClsMain.FGetDimension1Caption(), ClsMain.FGetDimension1Caption(), ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpDimension1)
                    ReportFrm.CreateHelpGrid(ClsMain.FGetDimension2Caption(), ClsMain.FGetDimension2Caption(), ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpDimension2)

                Case PurchaseOrderStatus
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubEndDate)
                    ReportFrm.CreateHelpGrid("Status On", "Status On", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubLoginDate)
                    ReportFrm.CreateHelpGrid("Report Type", "Report Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, "Select 'Summary' as Code, 'Summary' as Name Union All Select 'Detail' as Code, 'Detail' as Name  Union All Select 'Item Wise Order Status' as Code, 'Item Wise Order Status' as Name ", "Summary", , , , , False)
                    ReportFrm.CreateHelpGrid("Report For", "Report For", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, " SELECT 'All' AS Code, 'All' AS Name UNION ALL  SELECT 'Pending To Challan' AS Code, 'Pending To Challan' AS Name UNION ALL  SELECT 'Over Due' AS Code, 'Over Due' AS Name UNION ALL  SELECT 'Over Due And Balance' AS Code, 'Over Due And Balance' AS Name ", , , , , , False)
                    ReportFrm.CreateHelpGrid("Unit", "Unit", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, "Select 'Qty' as Code, 'Qty' as Name Union All Select 'Measure' as Code, 'Measure' AS Name Union All Select 'Qty & Measure' as Code, 'Qty & Measure' AS Name ", "Qty")
                    ReportFrm.CreateHelpGrid("Sort On", "Sort On", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, " SELECT 'Order Date' AS Code, 'Order Date' AS Name UNION ALL  SELECT 'Due Date' AS Code, 'Due Date' AS Name UNION ALL SELECT 'Over Due Days' AS Code, 'Over Due Days' AS Name UNION ALL  SELECT 'Balance Qty' AS Code, 'Balance Qty' AS Name ", "Order Date", , , , , False)
                    ReportFrm.CreateHelpGrid("Voucher Type", "Voucher Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, FGetMainVoucher_TypeQry("PurchOrderDetail", "LEFT JOIN PurchOrder H ON H.DocID = L.PurchOrder"))
                    ReportFrm.CreateHelpGrid("Party", "Party", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpVendorQry, , , 550, 300)
                    ReportFrm.CreateHelpGrid("Purchase Order No", "Purchase Order No", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpPurchaseOrder, , , 450)
                    ReportFrm.CreateHelpGrid("Item", "Item", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemQry, , , 600, 270)
                    ReportFrm.CreateHelpGrid("Item Group", "Item Group", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemGroupQry, , , 530)
                    ReportFrm.CreateHelpGrid("Item Category", "Item Category", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemCategoryQry, , , 430)
                    ReportFrm.CreateHelpGrid("Item Type", "Item Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemType)
                    ReportFrm.CreateHelpGrid("Item Reporting Group", "Item Reporting Group", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemReportingGroupQry, , , 500, 360)
                    ReportFrm.CreateHelpGrid(ClsMain.FGetDimension1Caption(), ClsMain.FGetDimension1Caption(), ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpDimension1)
                    ReportFrm.CreateHelpGrid(ClsMain.FGetDimension2Caption(), ClsMain.FGetDimension2Caption(), ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpDimension2)

                Case PurchaseChallanReport
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubEndDate)
                    ReportFrm.CreateHelpGrid("Report Type", "Report Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, "Select 'Detail' as Code, 'Detail' as Name Union All Select 'Item Wise Summary' as Code, 'Item Wise Summary' as Name ", "Detail", , , , , False)
                    ReportFrm.CreateHelpGrid("Unit", "Unit", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, "Select 'Qty' as Code, 'Qty' as Name Union All Select 'Measure' as Code, 'Measure' AS Name Union All Select 'Qty & Measure' as Code, 'Qty & Measure' AS Name ", "Qty")
                    ReportFrm.CreateHelpGrid("Voucher Type", "Voucher Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, FGetVoucher_TypeQry("PurchChallan"))
                    ReportFrm.CreateHelpGrid("Godown", "Godown", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpGodownQry, , , , )
                    ReportFrm.CreateHelpGrid("Party", "Party", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpVendorQry, , , 550, 300)
                    ReportFrm.CreateHelpGrid("Order No", "Order No", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpPurchaseOrder, , , 450)
                    ReportFrm.CreateHelpGrid("Item", "Item", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemQry, , , 600, 270)
                    ReportFrm.CreateHelpGrid("Item Group", "Item Group", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemGroupQry, , , 530)
                    ReportFrm.CreateHelpGrid("Item Category", "Item Category", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemCategoryQry, , , 430)
                    ReportFrm.CreateHelpGrid("Item Type", "Item Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemType)
                    ReportFrm.CreateHelpGrid("Item Reporting Group", "Item Reporting Group", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemReportingGroupQry, , , 500, 360)
                    ReportFrm.CreateHelpGrid(ClsMain.FGetDimension1Caption(), ClsMain.FGetDimension1Caption(), ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpDimension1)
                    ReportFrm.CreateHelpGrid(ClsMain.FGetDimension2Caption(), ClsMain.FGetDimension2Caption(), ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpDimension2)

                Case PurchaseChallanStatus
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubEndDate)
                    ReportFrm.CreateHelpGrid("Status On", "Status On", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubLoginDate)
                    ReportFrm.CreateHelpGrid("Report Type", "Report Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, "Select 'Summary' as Code, 'Summary' as Name Union All Select 'Detail' as Code, 'Detail' as Name  ", "Summary", , , , , False)
                    ReportFrm.CreateHelpGrid("Report For", "Report For", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, " SELECT 'All' AS Code, 'All' AS Name UNION ALL  SELECT 'Pending To Invoice' AS Code, 'Pending To Invoice' AS Name ", , , , , , False)
                    ReportFrm.CreateHelpGrid("Unit", "Unit", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, "Select 'Qty' as Code, 'Qty' as Name Union All Select 'Measure' as Code, 'Measure' AS Name Union All Select 'Qty & Measure' as Code, 'Qty & Measure' AS Name ", "Qty")
                    ReportFrm.CreateHelpGrid("Voucher Type", "Voucher Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, FGetMainVoucher_TypeQry("PurchChallanDetail", "LEFT JOIN PurchChallan H ON H.DocID = L.PurchChallan"))
                    ReportFrm.CreateHelpGrid("Party", "Party", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpVendorQry, , , 550, 300)
                    ReportFrm.CreateHelpGrid("Purchase Order No", "Purchase Order No", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpPurchaseOrder, , , 450)
                    ReportFrm.CreateHelpGrid("Item", "Item", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemQry, , , 600, 270)
                    ReportFrm.CreateHelpGrid("Item Group", "Item Group", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemGroupQry, , , 530)
                    ReportFrm.CreateHelpGrid("Item Category", "Item Category", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemCategoryQry, , , 430)
                    ReportFrm.CreateHelpGrid("Item Type", "Item Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemType)
                    ReportFrm.CreateHelpGrid("Item Reporting Group", "Item Reporting Group", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemReportingGroupQry, , , 500, 360)
                    ReportFrm.CreateHelpGrid(ClsMain.FGetDimension1Caption(), ClsMain.FGetDimension1Caption(), ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpDimension1)
                    ReportFrm.CreateHelpGrid(ClsMain.FGetDimension2Caption(), ClsMain.FGetDimension2Caption(), ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpDimension2)

                Case PurchaseInvoiceReport
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubEndDate)
                    ReportFrm.CreateHelpGrid("Report Type", "Report Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, "Select 'Detail' as Code, 'Detail' as Name Union All Select 'Item Wise Detail' as Code, 'Item Wise Detail' as Name Union All Select 'Item Wise Summary' as Code, 'Item Wise Summary' as Name Union All Select 'Month Wise Summary' as Code, 'Month Wise Summary' as Name Union All Select 'Party Wise Summary' as Code, 'Party Wise Summary' as Name", "Detail", , , , , False)
                    ReportFrm.CreateHelpGrid("Voucher Type", "Voucher Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, FGetVoucher_TypeQry("PurchInvoice"))
                    ReportFrm.CreateHelpGrid("Party", "Party", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpVendorQry, , , 550, 300)
                    ReportFrm.CreateHelpGrid("Code", "Amount Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, FGetStructureFieldsQry(AgTemplate.ClsMain.Temp_NCat.PurchaseInvoice), "Amount|Amount", , , , , False)
                    ReportFrm.CreateHelpGrid("SalesTax Group Party", "SalesTax Group Party", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpSalesTaxGroupParty, , , , 280)
                    ReportFrm.CreateHelpGrid("SalesTax Group Item", "SalesTax Group Item", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpSalesTaxGroupItem, , , , 280)
                    ReportFrm.CreateHelpGrid("Purchase Order", "Purchase Order", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpPurchaseOrder, , , 450)
                    ReportFrm.CreateHelpGrid("Purchase Challan", "Purchase Challan", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpPurchaseChallan, , , 450)
                    ReportFrm.CreateHelpGrid("Purchase Invoice", "Purchase Invoice", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpPurchaseInvoice, , , 450)
                    ReportFrm.CreateHelpGrid("Item", "Item", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemQry, , , 600, 270)
                    ReportFrm.CreateHelpGrid("Item Group", "Item Group", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemGroupQry, , , 530)
                    ReportFrm.CreateHelpGrid("Item Category", "Item Category", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemCategoryQry, , , 430)
                    ReportFrm.CreateHelpGrid("Item Type", "Item Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemType)
                    ReportFrm.CreateHelpGrid("Item Reporting Group", "Item Reporting Group", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemReportingGroupQry, , , 500, 360)
                    ReportFrm.CreateHelpGrid(ClsMain.FGetDimension1Caption(), ClsMain.FGetDimension1Caption(), ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpDimension1)
                    ReportFrm.CreateHelpGrid(ClsMain.FGetDimension2Caption(), ClsMain.FGetDimension2Caption(), ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpDimension2)
                    ReportFrm.CreateHelpGrid("Division", "Division", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpDivisionQry, "[DIVISIONCODE]")
                    ReportFrm.CreateHelpGrid("SiteBranch", "Site / Branch", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpSiteQry, "[SITECODE]")

                Case PurchaseReturnReport
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubEndDate)
                    ReportFrm.CreateHelpGrid("Report Type", "Report Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, "Select 'Detail' as Code, 'Detail' as Name Union All Select 'Item Wise Summary' as Code, 'Item Wise Summary' as Name ", "Detail", , , , , False)
                    ReportFrm.CreateHelpGrid("Voucher Type", "Voucher Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, FGetVoucher_TypeQry("PurchInvoice"))
                    ReportFrm.CreateHelpGrid("Party", "Party", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpVendorQry, , , 550, 300)
                    ReportFrm.CreateHelpGrid("Challan No", "Challan No", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpPurchaseChallan, , , 450)
                    ReportFrm.CreateHelpGrid("Invoice No", "Invoice No", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpPurchaseInvoice, , , 450)
                    ReportFrm.CreateHelpGrid("Item Group", "Item Group", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemGroupQry)
                    ReportFrm.CreateHelpGrid("Item Type", "Item Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemType)
                    ReportFrm.CreateHelpGrid("Item", "Item", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemQry, , , 600, 270)
                    ReportFrm.CreateHelpGrid("Item Reporting Group", "Item Reporting Group", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemReportingGroupQry, , , 500, 360)
                    ReportFrm.CreateHelpGrid(ClsMain.FGetDimension1Caption(), ClsMain.FGetDimension1Caption(), ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpDimension1)
                    ReportFrm.CreateHelpGrid(ClsMain.FGetDimension2Caption(), ClsMain.FGetDimension2Caption(), ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpDimension2)

            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
#End Region

    Private Function FGetVoucher_TypeQry(ByVal TableName As String) As String
        FGetVoucher_TypeQry = " SELECT Distinct 'o' As Tick, H.V_Type AS Code, Vt.Description AS [Voucher Type] " &
                                " FROM " & TableName & " H  " &
                                " LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type " &
                                " Where 1 =1 " & VtypeRestriction & " "
    End Function

    Private Function FGetStructureFieldsQry(ByVal NCat As String) As String
        FGetStructureFieldsQry = "Select 'Amount' as Code, 'Amount' as Description " &
                                 "Union All " &
                                 "SELECT L.LineAmtField AS Code, C.Description AS [Amount Type]  " &
                                 "FROM StructureDetail L " &
                                 "LEFT JOIN Charges C ON L.Charges = C.Code  " &
                                 "WHERE L.Code = (SELECT Structure FROM VoucherCat WHERE nCat = '" & NCat & "')"
    End Function

    Private Function FGetMainVoucher_TypeQry(ByVal HeaderTable As String, ByVal LineTableJoinStr As String) As String
        FGetMainVoucher_TypeQry = "Select DISTINCT 'o' As Tick, H.V_Type , Vt.Description " &
            " FROM " & HeaderTable & "  L " & LineTableJoinStr & " " &
            " LEFT JOIN Voucher_Type Vt ON Vt.V_Type = H.V_Type " &
            " WHERE IfNull(H.V_Type,'') <> '' " &
            " " & VtypeRestriction & " " &
            " ORDER BY Vt.Description "
    End Function

    Private Sub ObjRepFormGlobal_ProcessReport() Handles ReportFrm.ProcessReport
        Select Case mGRepFormName
            Case PurchaseIndentReport
                ProcPurchaseIndentReport()

            Case PurchaseIndentStatus
                ProcPurchaseIndentStatusReport()

            Case PurchaseOrderReport
                ProcPurchaseOrderReport()

            Case PurchaseOrderStatus
                ProcPurchaseOrderStatusReport()

            Case PurchaseChallanReport
                ProcPurchaseChallanReport()

            Case PurchaseChallanStatus
                ProcPurchaseChallanStatusReport()

            Case PurchaseInvoiceReport
                ProcPurchaseInvoiceReport()

            Case PurchaseReturnReport
                ProcPurchaseReturnReport()

        End Select
    End Sub

    Public Sub New(ByVal mReportFrm As ReportLayout.FrmReportLayout)
        ReportFrm = mReportFrm
    End Sub


#Region "Purchase Indent Report"
    Private Sub ProcPurchaseIndentReport()
        Dim mCondStr$ = ""

        RepTitle = "Purchase Indent Report"

        If ReportFrm.FGetText(2) = "Measure" Then
            RepName = "Purchase_PurchaseIndentReport_Measure"
        ElseIf ReportFrm.FGetText(2) = "Qty & Measure" Then
            RepName = "Purchase_PurchaseIndentReport_QtyMeasure"
        Else
            RepName = "Purchase_PurchaseIndentReport"
        End If
        Try
            mCondStr = mCondStr & " And H.V_Date Between " & AgL.Chk_Text(CDate(ReportFrm.FGetText(0)).ToString("u")) & " And " & AgL.Chk_Text(CDate(ReportFrm.FGetText(1)).ToString("u")) & " "
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.V_Type", 3)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.Indentor", 4)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.Department", 5)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.MaterialPlan", 6)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.PurchIndent", 7)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Item", 8)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemGroup", 9)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemCategory", 10)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemType", 11)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Dimension1", 13)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Dimension2", 14)

            If ReportFrm.FGetText(12) <> "" And ReportFrm.FGetText(12) <> "All" Then
                mQry = " Select '''' ||  replace(ItemList,',',''',''')  || ''''  From ItemReportingGroup Where 1=1 " & ReportFrm.GetWhereCondition("Code", 12)
                mCondStr = mCondStr & " AND I.Code In (" & AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar & ")"
            End If

            mCondStr = mCondStr & VtypeRestriction

            mCondStr = mCondStr & " And H.Site_Code = '" & AgL.PubSiteCode & "' "
            mCondStr = mCondStr & " And H.Div_Code= '" & AgL.PubDivCode & "' "

            mQry = " SELECT H.DocID, H.V_Type, H.V_Date, H.V_No, H.Remarks, H.ManualRefNo, U.DecimalPlaces, Vt.Description AS VoucherTypeDesc, H.ManualRefNo AS IndentNo," &
                    " D.Description AS DepartmentName, SG.DispName AS IndentBy , L.Sr, L.ReqQty, L.IndentQty, L.Unit, L.Rate, L.MeasurePerPcs, L.MeasureUnit,  UM.DecimalPlaces AS MeasureDecimalPlace, " &
                    " E.Caption_Dimension1, E.Caption_Dimension2, D1.Description AS D1Desc,D2.Description AS D2Desc," &
                    " L.TotalReqMeasure, L.TotalIndentMeasure, L.RequireDate, I.Description AS ItemDesc, MP.V_Type || '-' || MP.ManualRefNo AS MaterialPlanNo  " &
                    " FROM PurchIndent  H " &
                    " LEFT JOIN PurchIndentDetail L ON L.DocId = H.DocID  " &
                    " LEFT JOIN Department  D ON D.Code = H.Department  " &
                    " LEFT JOIN SubGroup SG ON SG.SubCode = H.Indentor  " &
                    " LEFT JOIN Item I ON I.Code = L.Item  " &
                    " LEFT JOIN MaterialPlan MP ON MP.DocID = L.MaterialPlan " &
                    " LEFT JOIN Voucher_Type Vt ON Vt.V_Type = H.V_Type " &
                    " LEFT JOIN Unit U ON U.Code = L.Unit " &
                    " LEFT JOIN Unit UM ON UM.Code = L.MeasureUnit " &
                    " LEFT JOIN Enviro E ON E.Site_Code = H.Site_Code AND E.Div_Code = H.Div_Code " &
                    " LEFT JOIN Dimension1 D1 ON D1.Code = L.Dimension1  " &
                    " LEFT JOIN Dimension2 D2 ON D2.Code = L.Dimension2 " &
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

#Region "Purchase Indent Status Report"
    Private Sub ProcPurchaseIndentStatusReport()
        Dim mCondStr$ = ""

        Dim mQryPurchOrder$ = " SELECT POD.PurchIndent, POD.PurchIndentSr, PO.ReferenceNo AS OrderNo, PO.V_Date AS PODate,  POD.Qty  AS OrderQty , POD.TotalMeasure AS OrderMeasure, " &
                    " PC.ReferenceNo AS ChallanNo, PC.V_Date AS PCDate,  PCD.Qty  AS ChallanQty , PCD.TotalMeasure AS ChallanMeasure " &
                    " FROM PurchOrderDetail POD " &
                    " LEFT JOIN PurchOrder PO ON PO.DocId = POD.DocId " &
                    " LEFT JOIN PurchChallanDetail PCD ON PCD.PurchOrder = POD.DocId AND PCD.PurchOrderSr = POD.Sr " &
                    " LEFT JOIN PurchChallan PC ON PC.DocId = PCD.DocId " &
                    " WHERE IfNull(POD.PurchIndent,'') <> '' " &
                    " AND PO.V_Date <= '" & ReportFrm.FGetText(2) & "' "

        Dim mQryPurchOrderSummury$ = " SELECT POD.PurchIndent, POD.PurchIndentSr, sum(POD.Qty)  AS OrderQty , sum(POD.TotalMeasure)  AS OrderMeasure, " &
                    " IfNull(sum(PCD.Qty),0) AS ChallanQty, IfNull(sum(PCD.TotalMeasure),0)  AS ChallanMeasure, Max(PO.V_Date) AS MaxOrdDate " &
                    " FROM PurchOrderDetail POD " &
                    " LEFT JOIN PurchOrder PO ON PO.DocId = POD.DocId " &
                    " LEFT JOIN PurchChallanDetail PCD ON PCD.PurchOrder = POD.DocId AND PCD.PurchOrderSr = POD.Sr " &
                    " WHERE IfNull(POD.PurchIndent,'') <> '' " &
                    " AND PO.V_Date <= '" & ReportFrm.FGetText(2) & "' " &
                    " GROUP BY POD.PurchIndent, POD.PurchIndentSr "

        Dim mQryPurchIndent$ = " SELECT PID.PurchIndent, PID.PurchIndentSr, sum(PID.IndentQty) AS BalIndQty, sum(PID.TotalIndentMeasure) AS BalIndMeasure   " &
                            " FROM PurchIndentDetail PID  " &
                            " WHERE IfNull(PID.PurchIndent,'') <> '' " &
                            " GROUP BY PID.PurchIndent, PID.PurchIndentSr "
        Try
            If ReportFrm.FGetText(3) = "Order Detail" Then
                RepTitle = "Purchase Indent Order Status Detail"
                If ReportFrm.FGetText(5) = "Measure" Then
                    RepName = "Purchase_PurchaseIndentStatusReport_Detail_Measure"
                ElseIf ReportFrm.FGetText(5) = "Qty & Measure" Then
                    RepName = "Purchase_PurchaseIndentStatusReport_Detail_QtyMeasure"
                Else
                    RepName = "Purchase_PurchaseIndentStatusReport_Detail"
                End If
            ElseIf ReportFrm.FGetText(3) = "Order & Challan Summary" Then
                RepTitle = "Purchase Indent Order & Challan Status Summary"
                If ReportFrm.FGetText(5) = "Measure" Then
                    RepName = "Purchase_PurchaseIndentStatusSummary_Measure"
                ElseIf ReportFrm.FGetText(5) = "Qty & Measure" Then
                    RepName = "Purchase_PurchaseIndentStatusSummary_QtyMeasure"
                Else
                    RepName = "Purchase_PurchaseIndentStatusSummary"
                End If
            ElseIf ReportFrm.FGetText(3) = "Order & Challan Detail" Then
                RepTitle = "Purchase Indent Order & Challan Status Detail"
                If ReportFrm.FGetText(5) = "Measure" Then
                    RepName = "Purchase_PurchaseIndentStatusDetail_Measure"
                ElseIf ReportFrm.FGetText(5) = "Qty & Measure" Then
                    RepName = "Purchase_PurchaseIndentStatusSummary_QtyMeasure"
                Else
                    RepName = "Purchase_PurchaseIndentStatusDetail"
                End If
            ElseIf ReportFrm.FGetText(3) = "Order Summary" Then
                RepTitle = "Purchase Indent Order Status Summary"
                If ReportFrm.FGetText(5) = "Measure" Then
                    RepName = "Purchase_PurchaseIndentStatusReport_Measure"
                ElseIf ReportFrm.FGetText(5) = "Qty & Measure" Then
                    RepName = "Purchase_PurchaseIndentStatusReport_QtyMeasure"
                Else
                    RepName = "Purchase_PurchaseIndentStatusReport"
                End If
            End If


            mCondStr = mCondStr & " And H.V_Date Between " & AgL.Chk_Text(CDate(ReportFrm.FGetText(0)).ToString("u")) & " And " & AgL.Chk_Text(CDate(ReportFrm.FGetText(1)).ToString("u")) & " "

            If ReportFrm.FGetText(4) = "Pending To Purchase Order" Then
                mCondStr = mCondStr & " AND IfNull(VPInd.BalIndQty,0) - IfNull(VPOrdS.OrderQty,0) > 0 "
            End If

            mCondStr = mCondStr & VtypeRestriction

            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.V_Type", 6)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.Indentor", 7)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.Department", 8)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.MaterialPlan", 9)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.PurchIndent", 10)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Item", 11)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemGroup", 12)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemCategory ", 13)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemType ", 14)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Dimension1", 16)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Dimension2", 17)

            If ReportFrm.FGetText(15) <> "" And ReportFrm.FGetText(15) <> "All" Then
                mQry = " Select '''' ||  replace(ItemList,',',''',''')  || ''''  From ItemReportingGroup Where 1=1 " & ReportFrm.GetWhereCondition("Code", 15)
                mCondStr = mCondStr & " AND I.Code In (" & AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar & ")"
            End If

            mCondStr = mCondStr & " And H.Site_Code = '" & AgL.PubSiteCode & "' "
            mCondStr = mCondStr & " And H.Div_Code= '" & AgL.PubDivCode & "' "

            mQry = " SELECT H.DocID, H.V_Type, H.V_Date, H.ManualRefNo, Vt.Description AS VoucherTypeDesc, H.ManualRefNo AS IndentNo, U.DecimalPlaces, UM.DecimalPlaces AS MeasureDecimalPlace," &
                    " L.Sr, L.Unit, L.Rate, L.MeasurePerPcs, L.MeasureUnit, IfNull(L.RequireDate,H.V_Date) AS RequireDate, I.Description AS ItemDesc, MP.V_Type || '-' || MP.ManualRefNo AS MaterialPlanNo , " &
                    " E.Caption_Dimension1, E.Caption_Dimension2, D1.Description AS D1Desc,D2.Description AS D2Desc," &
                    " IfNull(VPInd.BalIndQty,0) AS TotalIndQty, IfNull(VPInd.BalIndMeasure,0) AS TotalIndMeasure,  " &
                    " VPOrd.OrderNo, VPOrd.PODate, IfNull(VPOrd.OrderQty,0) AS OrderQty,  IfNull(VPOrd.OrderMeasure,0) AS OrderMeasure, " &
                    " VPOrd.ChallanNo, VPOrd.PCDate, IfNull(VPOrd.ChallanQty,0) AS ChallanQty, IfNull(VPOrd.ChallanMeasure,0) AS ChallanMeasure, " &
                    " IfNull(VPOrdS.OrderQty,0) AS TotalOrdQty, IfNull(VPOrdS.OrderMeasure,0) AS TotalOrdMeasure, IfNull(VPInd.BalIndQty,0) - IfNull(VPOrdS.OrderQty,0) AS TotalBalToOrdQty, IfNull(VPInd.BalIndMeasure,0) - IfNull(VPOrdS.OrderMeasure,0) AS TotalBalToOrdMeasure, " &
                    " IfNull(VPOrdS.ChallanQty,0) AS TotalChallanQty, IfNull(VPOrdS.ChallanMeasure,0) AS TotalChallanMeasure, IfNull(VPOrdS.OrderQty,0)-IfNull(VPOrdS.ChallanQty,0) AS TotalBalToChallanQty, IfNull(VPOrdS.OrderMeasure,0)-IfNull(VPOrdS.ChallanMeasure,0) AS TotalBalToChallanMeasure, " &
                    " IfNull(VPInd.BalIndQty,0) - IfNull(VPOrdS.ChallanQty,0) AS NetIndentOrderBalQty,  IfNull(VPInd.BalIndMeasure,0) - IfNull(VPOrdS.ChallanMeasure,0) AS NetIndentOrderBalMeasure, " &
                    " CASE WHEN IfNull(VPInd.BalIndQty,0) - IfNull(VPOrdS.OrderQty,0) > 0 THEN  datediff(Day,IfNull(L.RequireDate,H.V_Date),'" & ReportFrm.FGetText(2) & "') ELSE datediff(Day,IfNull(L.RequireDate,H.V_Date),VPOrdS.MaxOrdDate) END AS Ageing " &
                    " FROM PurchIndentDetail L " &
                    " LEFT JOIN PurchIndent H ON L.PurchIndent  = H.DocID  " &
                    " LEFT JOIN Item I ON I.Code = L.Item  " &
                    " LEFT JOIN MaterialPlan MP ON MP.DocID = L.MaterialPlan  " &
                    " LEFT JOIN Voucher_Type Vt ON Vt.V_Type = H.V_Type  " &
                    " LEFT JOIN Unit U ON U.Code = L.Unit " &
                    " LEFT JOIN Unit UM ON UM.Code = L.MeasureUnit " &
                    " LEFT JOIN Enviro E ON E.Site_Code = H.Site_Code AND E.Div_Code = H.Div_Code " &
                    " LEFT JOIN Dimension1 D1 ON D1.Code = L.Dimension1  " &
                    " LEFT JOIN Dimension2 D2 ON D2.Code = L.Dimension2 " &
                    " LEFT JOIN ( " & mQryPurchOrder & " ) VPOrd ON VPOrd.PurchIndent = L.DocId AND VPord.PurchIndentSr = L.Sr " &
                    " LEFT JOIN ( " & mQryPurchOrderSummury & " ) VPOrdS ON VPOrdS.PurchIndent = L.DocId AND VPOrdS.PurchIndentSr = L.Sr " &
                    " LEFT JOIN ( " & mQryPurchIndent & " ) VPInd ON VPInd.PurchIndent = L.DocId AND VPInd.PurchIndentSr = L.Sr " &
                    " WHERE 1=1 AND IfNull(VPInd.BalIndQty,0) > 0 " & mCondStr & " Order By H.V_Date "
            DsRep = AgL.FillData(mQry, AgL.GCn)

            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records to Print!")

            ReportFrm.PrintReport(DsRep, RepName, RepTitle, AgL.PubReportPath)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub
#End Region

#Region "Purchase Order Report"
    Private Sub ProcPurchaseOrderReport()
        Dim mCondStr$ = ""
        Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"

        Try
            If ReportFrm.FGetText(2) = "Detail" Then
                RepName = "PurchaseOrderReport"
                RepTitle = "Purchase Order Report"
            Else
                RepName = "ItemWisePurchaseOrderSummary"
                RepTitle = "Item Wise Purchase Order Summary"
            End If

            If ReportFrm.FGetText(2) = "Party Wise Summary" Then
                RepTitle = "Purchase Order Report (Party Wise Summary)"
                strGrpFld = "SG.Name"
                strGrpFldDesc = "SG.Name || ',' || IfNull(City.CityName,'')"
                strGrpFldHead = "'Party Name'"
                RepName = "Purchase_PurchaseOrderReport_Summary"
            ElseIf ReportFrm.FGetText(2) = "Month Wise Summary" Then
                RepTitle = "Purchase Order Report (Month Wise Summary)"
                strGrpFld = "Substring(convert(nvarchar,H.V_Date,11),0,6)"
                strGrpFldDesc = "Replace(SubString(Convert(VARCHAR,H.v_Date,6),4,6),' ','-')"
                strGrpFldHead = "'Month'"
                RepName = "Purchase_PurchaseOrderReport_Summary"
            ElseIf ReportFrm.FGetText(2) = "Item Wise Detail" Then
                RepTitle = "Purchase Order Report (Item Wise Detail)"
                If ReportFrm.FGetText(3) = "Measure" Then
                    RepName = "Purchase_PurchaseOrderReport_ItemWiseDetail_Measure"
                ElseIf ReportFrm.FGetText(3) = "Qty & Measure" Then
                    RepName = "Purchase_PurchaseOrderReport_ItemWiseDetail_QtyMeasure"
                Else
                    RepName = "Purchase_PurchaseOrderReport_ItemWiseDetail"
                End If
            ElseIf ReportFrm.FGetText(2) = "Item Wise Detail with Amount" Then
                RepTitle = "Purchase Order Report (Item Wise Detail)"
                If ReportFrm.FGetText(3) = "Measure" Then
                    RepName = "Purchase_PurchaseOrderReport_ItemWiseDetailwithAmount_Measure"
                ElseIf ReportFrm.FGetText(3) = "Qty & Measure" Then
                    RepName = "Purchase_PurchaseOrderReport_ItemWiseDetailwithAmount_QtyMeasure"
                Else
                    RepName = "Purchase_PurchaseOrderReport_ItemWiseDetailwithAmount"
                End If
            ElseIf ReportFrm.FGetText(2) = "Item Wise Summary" Then
                RepTitle = "Purchase Order Report (Item Wise Summary)"
                If ReportFrm.FGetText(3) = "Measure" Then
                    RepName = "Purchase_PurchaseOrderReport_ItemWiseSummary_Measure"
                ElseIf ReportFrm.FGetText(3) = "Qty & Measure" Then
                    RepName = "Purchase_PurchaseOrderReport_ItemWiseSummary_QtyMeasure"
                Else
                    RepName = "Purchase_PurchaseOrderReport_ItemWiseSummary"
                End If
            ElseIf ReportFrm.FGetText(2) = "Item Wise Summary with Amount" Then
                RepTitle = "Purchase Order Report (Item Wise Summary)"
                If ReportFrm.FGetText(3) = "Measure" Then
                    RepName = "Purchase_PurchaseOrderReport_ItemWiseSummarywithAmount_Measure"
                ElseIf ReportFrm.FGetText(3) = "Qty & Measure" Then
                    RepName = "Purchase_PurchaseOrderReport_ItemWiseSummarywithAmount_QtyMeasure"
                Else
                    RepName = "Purchase_PurchaseOrderReport_ItemWiseSummarywithAmount"
                End If
            Else
                RepTitle = "Purchase Order Report"
                RepName = "Purchase_PurchaseOrderReport"
            End If

            mCondStr = mCondStr & VtypeRestriction


            mCondStr = mCondStr & " And H.V_Date Between " & AgL.Chk_Text(CDate(ReportFrm.FGetText(0)).ToString("u")) & " And " & AgL.Chk_Text(CDate(ReportFrm.FGetText(1)).ToString("u")) & " "
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.V_Type", 4)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.Vendor", 5)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.PurchIndent ", 7)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.DocId ", 8)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Item", 9)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemGroup", 10)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemCategory", 11)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemType ", 12)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.Currency ", 14)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Dimension1", 15)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Dimension2", 16)

            If ReportFrm.FGetText(13) <> "" And ReportFrm.FGetText(13) <> "All" Then
                mQry = " Select '''' ||  replace(ItemList,',',''',''')  || ''''  From ItemReportingGroup Where 1=1 " & ReportFrm.GetWhereCondition("Code", 13)
                mCondStr = mCondStr & " AND I.Code In (" & AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar & ")"
            End If

            mCondStr = mCondStr & " And H.Site_Code = '" & AgL.PubSiteCode & "' "
            mCondStr = mCondStr & " And H.Div_Code= '" & AgL.PubDivCode & "' "

            mQry = " SELECT H.DocID, H.V_Type, H.V_Date, H.ReferenceNo, H.VendorName, H.Currency, U.DecimalPlaces, UM.DecimalPlaces AS MeasureDecimalPlace, " &
                    " H.VendorOrderNo, H.VendorOrderDate, H.Remarks, L.Sr, L.PurchIndent, L.PurchIndentSr, " &
                    " H.VendorDeliveryDate, " & strGrpFld & " as GrpField, " & strGrpFldDesc & " as GrpFieldDesc, " & strGrpFldHead & " as GrpFieldHead, " &
                    " E.Caption_Dimension1, E.Caption_Dimension2, D1.Description AS D1Desc,D2.Description AS D2Desc," &
                    " L.Item, L.Qty, L.Unit, L.MeasurePerPcs, L.MeasureUnit, L.TotalMeasure, L.Rate, L.Amount, L.Net_Amount, " &
                    " L." & Replace(ReportFrm.FGetCode(6), "'", "") & " as Amount, '" & ReportFrm.FGetText(6) & "' as AmountTitle, " &
                    " L.Remark AS LineRemark, L.RateType, I.Description AS ItemDesc, Vt.Description AS VoucherTypeDesc,  PI.ManualRefNo AS IndentNo  " &
                    " FROM PurchOrder H " &
                    " LEFT JOIN SubGroup SG ON SG.SubCode = H.Vendor  " &
                    " LEFT JOIN City On SG.CityCode = City.CityCode " &
                    " LEFT JOIN PurchOrderDetail L ON L.DocId = H.DocID  " &
                    " LEFT JOIN Item I ON I.Code = L.Item  " &
                    " LEFT JOIN Voucher_Type Vt ON Vt.V_Type = H.V_Type " &
                    " LEFT JOIN PurchIndent PI ON PI.DocID = L.PurchIndent " &
                    " LEFT JOIN Unit U ON U.Code = L.Unit " &
                    " LEFT JOIN Unit UM ON UM.Code = L.MeasureUnit " &
                    " LEFT JOIN Enviro E ON E.Site_Code = H.Site_Code AND E.Div_Code = H.Div_Code " &
                    " LEFT JOIN Dimension1 D1 ON D1.Code = L.Dimension1  " &
                    " LEFT JOIN Dimension2 D2 ON D2.Code = L.Dimension2 " &
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

#Region "Purchase Order Status Report"
    Private Sub ProcPurchaseOrderStatusReport()
        Dim mCondStr$ = ""
        Dim mStrSortOn$ = ""
        Dim mQryPurchChallan$ = " SELECT PCD.PurchOrder, PCD.PurchOrderSr , PC.ReferenceNo AS ChallanNo, PC.V_Date AS ChallanDate, PCD.Qty AS ChallanQty, PCD.TotalMeasure AS ChallanMeasure, " &
                    " PC.VendorDocNo, PC.VendorDocDate " &
                    " FROM PurchChallanDetail PCD " &
                    " LEFT JOIN PurchChallan PC ON PC.DocID = PCD.DocId  " &
                    " WHERE IfNull( PCD.PurchOrder,'') <> '' " &
                    " AND PC.V_Date <= '" & ReportFrm.FGetText(2) & "' "

        Dim mQryPurchChallanSummury$ = " SELECT PCD.PurchOrder, PCD.PurchOrderSr ,  sum(PCD.Qty) AS TotalChallanQty, sum(PCD.TotalMeasure) AS TotalChallanMeasure , max(PC.V_Date) AS MaxChallanDate" &
                    " FROM PurchChallanDetail PCD " &
                    " LEFT JOIN PurchChallan PC ON PC.DocID = PCD.DocId  " &
                    " WHERE IfNull( PCD.PurchOrder,'') <> '' " &
                    " AND PC.V_Date <= '" & ReportFrm.FGetText(2) & "' " &
                    " GROUP BY PCD.PurchOrder, PCD.PurchOrderSr "

        Dim mQryPurchOrder$ = " SELECT POD.PurchOrder, POD.PurchOrderSr, sum(POD.Qty) AS BalOrdQty, sum(POD.TotalMeasure) AS BalOrdMeasure   " &
                            " FROM PurchOrderDetail POD  " &
                            " LEFT JOIN PurchOrder PO ON PO.DocId = POD.DocId " &
                            " WHERE IfNull(POD.PurchOrder,'') <> '' " &
                            " AND PO.V_Date <= '" & ReportFrm.FGetText(2) & "' " &
                            " GROUP BY POD.PurchOrder, POD.PurchOrderSr "
        Try

            If ReportFrm.FGetText(3) = "Detail" Then
                RepTitle = "Purchase Order Status Report"
                If ReportFrm.FGetText(5) = "Measure" Then
                    RepName = "Purchase_PurchaseOrderStatusReport_Detail_Measure"
                ElseIf ReportFrm.FGetText(5) = "Qty & Measure" Then
                    RepName = "Purchase_PurchaseOrderStatusReport_Detail_QtyMeasure"
                Else
                    RepName = "Purchase_PurchaseOrderStatusReport_Detail"
                End If
            ElseIf ReportFrm.FGetText(3) = "Item Wise Order Status" Then
                RepTitle = "Item Wise Purchase Order Status"
                If ReportFrm.FGetText(5) = "Measure" Then
                    RepName = "Purchase_PurchaseOrderStatusReport_ItemWise_Measure"
                ElseIf ReportFrm.FGetText(5) = "Qty & Measure" Then
                    RepName = "Purchase_PurchaseOrderStatusReport_ItemWise_QtyMeasure"
                Else
                    RepName = "Purchase_PurchaseOrderStatusReport_ItemWise"
                End If
            Else
                RepTitle = "Purchase Order Status Report"
                If ReportFrm.FGetText(5) = "Measure" Then
                    RepName = "Purchase_PurchaseOrderStatusReport_Measure"
                ElseIf ReportFrm.FGetText(5) = "Qty & Measure" Then
                    RepName = "Purchase_PurchaseOrderStatusReport_QtyMeasure"
                Else
                    RepName = "Purchase_PurchaseOrderStatusReport"
                End If
            End If

            If ReportFrm.FGetText(6) = "Order Date" Then
                mStrSortOn = "H.V_Date"
            ElseIf ReportFrm.FGetText(6) = "Due Date" Then
                mStrSortOn = "H.VendorDeliveryDate"
            ElseIf ReportFrm.FGetText(6) = "Over Due Days" Then
                mStrSortOn = " CASE WHEN IfNull(VPO.BalOrdQty,0) - IfNull(VPCS.TotalChallanQty,0) > 0 THEN  datediff(Day,H.VendorDeliveryDate,'" & ReportFrm.FGetText(2) & "') ELSE datediff(Day,H.VendorDeliveryDate,VPCS.MaxChallanDate) END "
            ElseIf ReportFrm.FGetText(6) = "Balance Qty" Then
                mStrSortOn = " IfNull(VPO.BalOrdQty,0) - IfNull(VPCS.TotalChallanQty,0) "
            End If

            mCondStr = mCondStr & " And H.V_Date Between " & AgL.Chk_Text(CDate(ReportFrm.FGetText(0)).ToString("u")) & " And " & AgL.Chk_Text(CDate(ReportFrm.FGetText(1)).ToString("u")) & " "

            If ReportFrm.FGetText(4) = "Pending To Challan" Then
                mCondStr = mCondStr & " AND IfNull(VPO.BalOrdQty,0) - IfNull(VPCS.TotalChallanQty,0) > 0 "
            ElseIf ReportFrm.FGetText(4) = "Over Due" Then
                mCondStr = mCondStr & "  AND H.VendorDeliveryDate < IfNull(VPC.ChallanDate,'" & ReportFrm.FGetText(2) & "') "
            ElseIf ReportFrm.FGetText(4) = "Over Due And Balance" Then
                mCondStr = mCondStr & " AND IfNull(VPO.BalOrdQty,0) - IfNull(VPCS.TotalChallanQty,0) > 0 "
                mCondStr = mCondStr & "  AND H.VendorDeliveryDate < IfNull(VPC.ChallanDate,'" & ReportFrm.FGetText(2) & "') "
            End If

            mCondStr = mCondStr & VtypeRestriction

            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.V_Type", 7)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.Vendor", 8)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.PurchOrder ", 9)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Item", 10)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemGroup", 11)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemCategory ", 12)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemType ", 13)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Dimension1", 15)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Dimension2", 16)

            If ReportFrm.FGetText(14) <> "" And ReportFrm.FGetText(14) <> "All" Then
                mQry = " Select '''' ||  replace(ItemList,',',''',''')  || ''''  From ItemReportingGroup Where 1=1 " & ReportFrm.GetWhereCondition("Code", 14)
                mCondStr = mCondStr & " AND I.Code In (" & AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar & ")"
            End If

            mCondStr = mCondStr & " And H.Site_Code = '" & AgL.PubSiteCode & "' "
            mCondStr = mCondStr & " And H.Div_Code= '" & AgL.PubDivCode & "' "
            mCondStr = mCondStr & " And L.DocId = L.PurchOrder "

            mQry = " SELECT H.DocID, H.V_Type, H.V_Date, H.ReferenceNo, H.Currency, U.DecimalPlaces, UM.DecimalPlaces AS MeasureDecimalPlace, " &
                    " SG.DispName AS PartyName, VPC.VendorDocNo, VPC.VendorDocDate, " &
                    " CASE WHEN IsNumeric(H.ReferenceNo) > 0 THEN Convert(INT, H.ReferenceNo) ELSE 0 END AS  OrderNo," &
                    " H.VendorOrderNo, H.VendorOrderDate, H.VendorDeliveryDate, H.Remarks, L.Sr, L.PurchIndent, L.PurchIndentSr, " &
                    " L.Item, L.Qty, L.Unit, L.MeasurePerPcs, L.MeasureUnit, L.TotalMeasure, L.Rate, L.Amount,  " &
                    " E.Caption_Dimension1, E.Caption_Dimension2, D1.Description AS D1Desc,D2.Description AS D2Desc," &
                    " L.Remark AS LineRemark, L.RateType, I.Description AS ItemDesc, Vt.Description AS VoucherTypeDesc,  " &
                    " VPC.ChallanNo, VPC.ChallanDate, IfNull(VPC.ChallanQty,0) AS ChallanQty,  IfNull(VPC.ChallanMeasure,0) AS ChallanMeasure ,   " &
                    " IfNull(VPO.BalOrdQty,0) AS TotalOrdQty, IfNull(VPO.BalOrdMeasure,0) AS TotalOrdMeasure, " & mStrSortOn & " AS OrderOn, " &
                    " CASE WHEN IfNull(VPO.BalOrdQty,0) - IfNull(VPCS.TotalChallanQty,0) > 0 THEN  datediff(Day,H.VendorDeliveryDate,'" & ReportFrm.FGetText(2) & "') ELSE datediff(Day,H.VendorDeliveryDate,VPCS.MaxChallanDate) END AS Ageing, " &
                    " IfNull(VPO.BalOrdQty,0) - IfNull(VPCS.TotalChallanQty,0) AS TotalBalQty   ,  IfNull(VPO.BalOrdMeasure,0) - IfNull(VPCS.TotalChallanMeasure,0) AS TotalBalMeasure  " &
                    " FROM PurchOrder H " &
                    " LEFT JOIN PurchOrderDetail L ON L.DocId = H.DocID  " &
                    " LEFT JOIN SubGroup SG ON SG.SubCode = H.Vendor  " &
                    " LEFT JOIN City On SG.CityCode = City.CityCode " &
                    " LEFT JOIN Item I ON I.Code = L.Item  " &
                    " LEFT JOIN Voucher_Type Vt ON Vt.V_Type = H.V_Type " &
                    " LEFT JOIN Unit U ON U.Code = L.Unit " &
                    " LEFT JOIN Unit UM ON UM.Code = L.MeasureUnit " &
                    " LEFT JOIN Enviro E ON E.Site_Code = H.Site_Code AND E.Div_Code = H.Div_Code " &
                    " LEFT JOIN Dimension1 D1 ON D1.Code = L.Dimension1  " &
                    " LEFT JOIN Dimension2 D2 ON D2.Code = L.Dimension2 " &
                    " LEFT JOIN ( " & mQryPurchChallan & " ) VPC ON VPC.PurchOrder = L.DocId AND VPC.PurchOrderSr = L.Sr " &
                    " LEFT JOIN ( " & mQryPurchChallanSummury & " ) VPCS ON VPCS.PurchOrder = L.DocId AND VPCS.PurchOrderSr = L.Sr " &
                    " LEFT JOIN ( " & mQryPurchOrder & " ) VPO ON VPO.PurchOrder = L.DocId AND VPO.PurchOrderSr = L.Sr " &
                    " WHERE 1=1 " & mCondStr & "  Order By H.V_Date "

            DsRep = AgL.FillData(mQry, AgL.GCn)
            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records to Print!")

            ReportFrm.PrintReport(DsRep, RepName, RepTitle, AgL.PubReportPath)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub
#End Region

#Region "Purchase Challan Report"
    Private Sub ProcPurchaseChallanReport()
        Dim mCondStr$ = ""

        Try
            If ReportFrm.FGetText(2) = "Detail" Then
                RepTitle = "Purchase Challan Report"
                If ReportFrm.FGetText(3) = "Measure" Then
                    RepName = "Purchase_PurchaseChallanReport_ItemWiseDetail_Measure"
                ElseIf ReportFrm.FGetText(3) = "Qty & Measure" Then
                    RepName = "Purchase_PurchaseChallanReport_ItemWiseDetail_QtyMeasure"
                Else
                    RepName = "Purchase_PurchaseChallanReport_ItemWiseDetail"
                End If
            Else
                RepTitle = "Item Wise Purchase Challan Summary"
                If ReportFrm.FGetText(3) = "Measure" Then
                    RepName = "Purchase_PurchaseChallanReport_ItemWiseSummary_Measure"
                ElseIf ReportFrm.FGetText(3) = "Qty & Measure" Then
                    RepName = "Purchase_PurchaseChallanReport_ItemWiseSummary_QtyMeasure"
                Else
                    RepName = "Purchase_PurchaseChallanReport_ItemWiseSummary"
                End If
            End If

            mCondStr = mCondStr & " And H.V_Date Between " & AgL.Chk_Text(CDate(ReportFrm.FGetText(0)).ToString("u")) & " And " & AgL.Chk_Text(CDate(ReportFrm.FGetText(1)).ToString("u")) & " "
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.V_Type", 4)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.Godown", 5)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.Vendor", 6)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.PurchOrder", 7)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Item", 8)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemGroup", 9)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemCategory", 10)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemType ", 11)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Dimension1", 13)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Dimension2", 14)

            If ReportFrm.FGetText(12) <> "" And ReportFrm.FGetText(12) <> "All" Then
                mQry = " Select '''' ||  replace(ItemList,',',''',''')  || ''''  From ItemReportingGroup Where 1=1 " & ReportFrm.GetWhereCondition("Code", 12)
                mCondStr = mCondStr & " AND I.Code In (" & AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar & ")"
            End If

            mCondStr = mCondStr & VtypeRestriction

            mCondStr = mCondStr & " And H.Site_Code = '" & AgL.PubSiteCode & "' "
            mCondStr = mCondStr & " And H.Div_Code= '" & AgL.PubDivCode & "' "

            mQry = " SELECT H.DocID, H.V_Type, H.V_Date, H.V_No, H.ReferenceNo, G.Description AS GodownDesc,  H.Remarks, U.DecimalPlaces, " &
                    " L.Sr, L.Item , L.DocQty, L.RejQty, L.Qty, L.Unit, L.MeasurePerPcs, L.MeasureUnit, UM.DecimalPlaces AS MeasureDecimalPlace, " &
                    " E.Caption_Dimension1, E.Caption_Dimension2, D1.Description AS D1Desc,D2.Description AS D2Desc, " &
                    " L.Rate, L.Amount, L.Net_Amount, L.TotalMeasure, L.Remark AS LineRemark, L.PurchOrder, L.PurchOrderSr , I.Description AS ItemDesc, " &
                    " PO.ManualRefNo AS PONo ,  Vt.Description AS VoucherTypeDesc, SG.DispName AS VendorName, VendorDocNo , VendorDocDate  " &
                    " FROM PurchChallan H " &
                    " LEFT JOIN Godown G ON G.Code = H.Godown  " &
                    " LEFT JOIN SubGroup SG ON SG.SubCode = H.Vendor " &
                    " LEFT JOIN PurchChallanDetail L ON L.DocId = H.DocID  " &
                    " LEFT JOIN Item I ON I.Code = L.Item  " &
                    " LEFT JOIN PurchOrder PO ON PO.DocID = L.PurchOrder " &
                    " LEFT JOIN Voucher_Type Vt ON Vt.V_Type = H.V_Type " &
                    " LEFT JOIN Unit U ON U.Code = L.Unit " &
                    " LEFT JOIN Unit UM ON UM.Code = L.MeasureUnit " &
                    " LEFT JOIN Enviro E ON E.Site_Code = H.Site_Code AND E.Div_Code = H.Div_Code " &
                    " LEFT JOIN Dimension1 D1 ON D1.Code = L.Dimension1  " &
                    " LEFT JOIN Dimension2 D2 ON D2.Code = L.Dimension2 " &
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

#Region "Purchase Challan Status Report"
    Private Sub ProcPurchaseChallanStatusReport()
        Dim mCondStr$ = ""

        Dim mQryPurchInvoice$ = " SELECT PID.PurchChallan, PID.PurchChallanSr , PI.ReferenceNo AS InvoiceNo, PI.V_Date AS InvoiceDate, PID.Qty AS InvoiceQty, PID.TotalMeasure AS InvoiceMeasure " &
                    " FROM PurchInvoiceDetail PID " &
                    " LEFT JOIN PurchInvoice PI ON PI.DocID = PID.DocId  " &
                    " WHERE IfNull( PID.PurchChallan,'') <> '' " &
                    " AND PI.V_Date <= '" & ReportFrm.FGetText(2) & "' "

        Dim mQryPurchInvoiceSummury$ = " SELECT PID.PurchChallan, PID.PurchChallanSr ,  sum(PID.Qty) AS TotalInvoiceQty, sum(PID.TotalMeasure) AS TotalInvoiceMeasure " &
                    " FROM PurchInvoiceDetail PID " &
                    " LEFT JOIN PurchInvoice PI ON PI.DocID = PID.DocId  " &
                    " WHERE IfNull( PID.PurchChallan,'') <> '' " &
                    " AND PI.V_Date <= '" & ReportFrm.FGetText(2) & "' " &
                    " GROUP BY PID.PurchChallan, PID.PurchChallanSr "

        Dim mQryPurchChallan$ = " SELECT PCD.PurchChallan, PCD.PurchChallanSr, sum(PCD.Qty) AS BalChallanQty, sum(PCD.TotalMeasure) AS BalChallanMeasure   " &
                            " FROM PurchChallanDetail PCD  " &
                            " LEFT JOIN PurchChallan PC ON PC.DocId = PCD.DocId " &
                            " WHERE IfNull(PCD.PurchChallan,'') <> '' " &
                            " AND PC.V_Date <= '" & ReportFrm.FGetText(2) & "' " &
                            " GROUP BY PCD.PurchChallan, PCD.PurchChallanSr "
        Try

            If ReportFrm.FGetText(3) = "Detail" Then
                RepTitle = "Purchase Challan Status Report"
                If ReportFrm.FGetText(5) = "Measure" Then
                    RepName = "Purchase_PurchaseChallanStatusReport_Detail_Measure"
                ElseIf ReportFrm.FGetText(5) = "Qty & Measure" Then
                    RepName = "Purchase_PurchaseChallanStatusReport_Detail_QtyMeasure"
                Else
                    RepName = "Purchase_PurchaseChallanStatusReport_Detail"
                End If
            Else
                RepTitle = "Purchase Challan Status Report"
                If ReportFrm.FGetText(5) = "Measure" Then
                    RepName = "Purchase_PurchaseChallanStatusReport_Measure"
                ElseIf ReportFrm.FGetText(5) = "Qty & Measure" Then
                    RepName = "Purchase_PurchaseChallanStatusReport_QtyMeasure"
                Else
                    RepName = "Purchase_PurchaseChallanStatusReport"
                End If
            End If


            mCondStr = mCondStr & " And H.V_Date Between " & AgL.Chk_Text(CDate(ReportFrm.FGetText(0)).ToString("u")) & " And " & AgL.Chk_Text(CDate(ReportFrm.FGetText(1)).ToString("u")) & " "
            If ReportFrm.FGetText(4) = "Pending To Invoice" Then
                mCondStr = mCondStr & " AND IfNull(VPC.BalChallanQty,0) - IfNull(VPI.InvoiceQty,0) > 0 "
            End If

            mCondStr = mCondStr & VtypeRestriction

            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.V_Type", 6)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.Vendor", 7)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.PurchOrder", 8)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Item", 9)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemGroup", 10)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.ItemCategory", 11)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemType ", 12)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Dimension1", 14)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Dimension2", 15)

            If ReportFrm.FGetText(13) <> "" And ReportFrm.FGetText(13) <> "All" Then
                mQry = " Select '''' ||  replace(ItemList,',',''',''')  || ''''  From ItemReportingGroup Where 1=1 " & ReportFrm.GetWhereCondition("Code", 13)
                mCondStr = mCondStr & " AND I.Code In (" & AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar & ")"
            End If

            mCondStr = mCondStr & " And H.Site_Code = '" & AgL.PubSiteCode & "' "
            mCondStr = mCondStr & " And H.Div_Code= '" & AgL.PubDivCode & "' "

            mQry = " SELECT H.DocID, H.V_Type, H.V_Date, H.V_No, H.ReferenceNo, U.DecimalPlaces,  UM.DecimalPlaces AS MeasureDecimalPlace, G.Description AS GodownDesc, H.Remarks, SG.DispName AS VendorName, " &
                    " L.Sr, L.Item , L.DocQty, L.RejQty, L.Unit, L.MeasurePerPcs,  L.MeasureUnit, Vt.Description AS VoucherTypeDesc, " &
                    " E.Caption_Dimension1, E.Caption_Dimension2, D1.Description AS D1Desc,D2.Description AS D2Desc," &
                    " L.Rate, L.Amount, L.Remark AS LineRemark, L.PurchOrder, L.PurchOrderSr , I.Description AS ItemDesc, PO.ManualRefNo AS PONo, " &
                    " VPI.InvoiceNo, VPI.InvoiceDate, IfNull(VPI.InvoiceQty,0) AS InvoiceQty,  IfNull(VPI.InvoiceMeasure,0) AS InvoiceMeasure, " &
                    " IfNull(VPC.BalChallanQty,0) AS TotalChallanQty, IfNull(VPC.BalChallanMeasure,0) AS TotalChallanMeasure,  " &
                    " IfNull(VPC.BalChallanQty,0) - IfNull(VPIS.TotalInvoiceQty,0) AS TotalBalQty   ,  IfNull(VPC.BalChallanMeasure,0) - IfNull(VPIS.TotalInvoiceMeasure,0) AS TotalBalMeasure  " &
                    " FROM PurchChallan H " &
                    " LEFT JOIN Godown G ON G.Code = H.Godown  " &
                    " LEFT JOIN SubGroup SG ON SG.SubCode = H.Vendor " &
                    " LEFT JOIN PurchChallanDetail L ON L.DocId = H.DocID  " &
                    " LEFT JOIN Item I ON I.Code = L.Item  " &
                    " LEFT JOIN PurchOrder PO ON PO.DocID = L.PurchOrder " &
                    " LEFT JOIN Voucher_Type Vt ON Vt.V_Type = H.V_Type " &
                    " LEFT JOIN Unit U ON U.Code = L.Unit " &
                    " LEFT JOIN Unit UM ON UM.Code = L.MeasureUnit " &
                    " LEFT JOIN Enviro E ON E.Site_Code = H.Site_Code AND E.Div_Code = H.Div_Code " &
                    " LEFT JOIN Dimension1 D1 ON D1.Code = L.Dimension1  " &
                    " LEFT JOIN Dimension2 D2 ON D2.Code = L.Dimension2 " &
                    " LEFT JOIN ( " & mQryPurchInvoice & " ) VPI ON VPI.PurchChallan = L.DocId AND VPI.PurchChallanSr = L.Sr " &
                    " LEFT JOIN ( " & mQryPurchInvoiceSummury & " ) VPIS ON VPIS.PurchChallan = L.DocId AND VPIS.PurchChallanSr = L.Sr " &
                    " LEFT JOIN ( " & mQryPurchChallan & " ) VPC ON VPC.PurchChallan = L.DocId AND VPC.PurchChallanSr = L.Sr " &
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

#Region "Purchase Invoice Report"
    Private Sub ProcPurchaseInvoiceReport()
        Dim mCondStr$ = ""
        Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"
        Try
            If ReportFrm.FGetText(2) = "Detail" Then
                RepName = "Purchase_PurchaseInvoiceReport" : RepTitle = "Purchase Invoice Report"
            ElseIf ReportFrm.FGetText(2) = "Item Wise Detail" Then
                RepName = "Purchase_PurchaseInvoiceReport_ItemWiseDetail" : RepTitle = "Item Wise Purchase Invoice Report"
            ElseIf ReportFrm.FGetText(2) = "Party Wise Summary" Then
                RepName = "Purchase_PurchaseInvoiceReport_Summary" : RepTitle = "Purchase Invoice Report (" & ReportFrm.FGetText(2) & ")"
                strGrpFld = "SG.Name"
                strGrpFldDesc = "SG.Name || ',' || IfNull(City.CityName,'')"
                strGrpFldHead = "'Party Name'"
            ElseIf ReportFrm.FGetText(2) = "Month Wise Summary" Then
                RepName = "Purchase_PurchaseInvoiceReport_Summary" : RepTitle = "Purchase Invoice Report (" & ReportFrm.FGetText(2) & ")"
                strGrpFld = "Substring(convert(nvarchar,H.V_Date,11),0,6)"
                strGrpFldDesc = "Replace(SubString(Convert(VARCHAR,H.v_Date,6),4,6),' ','-')"
                strGrpFldHead = "'Month'"
            ElseIf ReportFrm.FGetText(2) = "Item Wise Summary" Then
                RepName = "Purchase_PurchaseInvoiceReport_ItemWiseSummary" : RepTitle = "Purchase Invoice Report (" & ReportFrm.FGetText(2) & ")"
            End If

            mCondStr = mCondStr & " And H.V_Date Between " & AgL.Chk_Text(CDate(ReportFrm.FGetText(0)).ToString("u")) & " And " & AgL.Chk_Text(CDate(ReportFrm.FGetText(1)).ToString("u")) & " "
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.V_Type", 3)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.Vendor", 4)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.SalesTaxGroupParty", 6)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.SalesTaxGroupItem", 7)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.PurchOrder", 8)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.PurchChallan", 9)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.PurchInvoice", 10)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Item", 11)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemGroup", 12)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemCategory", 13)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemType ", 14)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Dimension1", 16)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Dimension2", 17)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.Div_Code", 18)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.Site_Code", 19)

            mCondStr = mCondStr & VtypeRestriction

            If ReportFrm.FGetText(15) <> "" And ReportFrm.FGetText(15) <> "All" Then
                mQry = " Select '''' ||  replace(ItemList,',',''',''')  || ''''  From ItemReportingGroup Where 1=1 " & ReportFrm.GetWhereCondition("Code", 15)
                mCondStr = mCondStr & " AND I.Code In (" & AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar & ")"
            End If


            mQry = " SELECT " & strGrpFld & " as GrpField, " & strGrpFldDesc & " as GrpFieldDesc, " & strGrpFldHead & " as GrpFieldHead, " &
                    " H.DocID, H.V_Type, H.V_Date, H.VendorDocNo, H.ReferenceNo, SG.DispName + (Case When City.CityName Is Not Null then ', ' ||  City.CityName Else '' End) AS VendorName, " &
                    " L.Sr, L.PurchChallan, L.Item, L.Qty, L.Unit, U.DecimalPlaces AS QtyDecimalplace, UM.DecimalPlaces AS MeasureDecimalplace, I.Measure AS MeasurePerPcs , I.MeasureUnit, IfNull(L.Qty,0)*IfNull(I.Measure,0) AS TotalMeasure , " &
                    " E.Caption_Dimension1, E.Caption_Dimension2, D1.Description AS D1Desc,D2.Description AS D2Desc," &
                    " L.Rate , L.PurchChallanSr, L." & Replace(ReportFrm.FGetCode(5), "'", "") & " as Amount, '" & ReportFrm.FGetText(5) & "' as AmountTitle, IfNull(L." & Replace(ReportFrm.FGetCode(5), "'", "") & ",0)/L.Qty AS NetAmtRate, I.Description AS ItemDesc,  " &
                    " Vt.Description AS VoucherTypeDesc, PC.V_Type || '- ' || PC.ReferenceNo AS ChallanNo, H.Remarks as H_Remarks, L.Remark as L_Remarks  " &
                    " FROM PurchInvoice H " &
                    " LEFT JOIN SubGroup SG ON SG.SubCode = H.Vendor  " &
                    " LEFT JOIN City On SG.CityCode = City.CityCode " &
                    " LEFT JOIN PurchInvoiceDetail L ON L.DocId = H.DocID  " &
                    " LEFT JOIN Item I ON I.Code = L.Item   " &
                    " LEFT JOIN Voucher_Type Vt ON Vt.V_Type = H.V_Type " &
                    " LEFT JOIN PurchChallan PC ON PC.DocID = L.PurchChallan " &
                    " LEFT JOIN Unit U ON U.Code = I.Unit " &
                    " LEFT JOIN Unit UM ON UM.Code = I.MeasureUnit " &
                    " LEFT JOIN Enviro E ON E.Site_Code = H.Site_Code AND E.Div_Code = H.Div_Code " &
                    " LEFT JOIN Dimension1 D1 ON D1.Code = L.Dimension1  " &
                    " LEFT JOIN Dimension2 D2 ON D2.Code = L.Dimension2 " &
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

#Region "Purchase Return Report"
    Private Sub ProcPurchaseReturnReport()
        Dim mCondStr$ = ""


        Try
            If ReportFrm.FGetText(2) = "Detail" Then
                RepName = "PurchaseReturnReport"
                RepTitle = "Purchase Return Report"
            Else
                RepName = "ItemWisePurchaseReturnSummary"
                RepTitle = "Item Wise Purchase Return Summary"
            End If

            'mCondStr = mCondStr & " And VT.NCat In ( '" & ClsMain.Temp_NCat.Purch & "','" & ClsMain.Temp_NCat.PurchReturnOther & "','" & ClsMain.Temp_NCat.PurchReturnWool & "','" & ClsMain.Temp_NCat.PurchReturnYarn & "')  "

            mCondStr = mCondStr & " And H.V_Date Between " & AgL.Chk_Text(CDate(ReportFrm.FGetText(0)).ToString("u")) & " And " & AgL.Chk_Text(CDate(ReportFrm.FGetText(1)).ToString("u")) & " "
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.V_Type", 3)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.Vendor", 4)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.PurchChallan", 5)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemGroup", 6)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemType ", 7)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Item", 8)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Dimension1", 10)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Dimension2", 11)

            If ReportFrm.FGetText(9) <> "" And ReportFrm.FGetText(9) <> "All" Then
                mQry = " Select '''' ||  replace(ItemList,',',''',''')  || ''''  From ItemReportingGroup Where 1=1 " & ReportFrm.GetWhereCondition("Code", 9)
                mCondStr = mCondStr & " AND I.Code In (" & AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar & ")"
            End If

            mCondStr = mCondStr & " And H.Site_Code = '" & AgL.PubSiteCode & "' "
            mCondStr = mCondStr & " And H.Div_Code= '" & AgL.PubDivCode & "' "

            mCondStr = mCondStr & VtypeRestriction

            mQry = " SELECT H.DocID, H.V_Type, H.V_Prefix, H.V_Date, H.V_No, H.ReferenceNo, SG.DispName AS VendorName, " &
                    " L.Sr, L.PurchChallan , L.Item , L.Qty, L.Unit , L.MeasurePerPcs , L.MeasureUnit, L.TotalMeasure,  " &
                    " E.Caption_Dimension1, E.Caption_Dimension2, D1.Description AS D1Desc,D2.Description AS D2Desc," &
                    " L.Rate , L.Amount, L.PurchChallanSr, L.Net_Amount, I.Description AS ItemDesc, U.DecimalPlaces,  " &
                    " Vt.Description AS VoucherTypeDesc,  CASE WHEN IfNull(L.PurchInvoice,'') <> '' THEN PI.V_Type +'- ' || PI.ReferenceNo ELSE   PC.V_Type || '- ' || PC.ReferenceNo END AS ChallanNo " &
                    " FROM PurchInvoice H " &
                    " LEFT JOIN SubGroup SG ON SG.SubCode = H.Vendor  " &
                    " LEFT JOIN PurchInvoiceDetail L ON L.DocId = H.DocID  " &
                    " LEFT JOIN Item I ON I.Code = L.Item   " &
                    " LEFT JOIN Voucher_Type Vt ON Vt.V_Type = H.V_Type " &
                    " LEFT JOIN PurchChallan PC ON PC.DocID = L.PurchChallan " &
                    " LEFT JOIN PurchInvoice PI ON PI.DocID = L.PurchInvoice " &
                    " LEFT JOIN Unit U ON U.Code = L.Unit " &
                    " LEFT JOIN Enviro E ON E.Site_Code = H.Site_Code AND E.Div_Code = H.Div_Code " &
                    " LEFT JOIN Dimension1 D1 ON D1.Code = L.Dimension1  " &
                    " LEFT JOIN Dimension2 D2 ON D2.Code = L.Dimension2 " &
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
