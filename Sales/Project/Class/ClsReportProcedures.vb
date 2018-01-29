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
    Private Const SaleOrderReport As String = "SaleOrderReport"
    Private Const SaleInvoiceReport As String = "SaleInvoiceReport"
    Private Const SaleChallanReport As String = "SaleChallanReport"

    Private Const SaleOrderStatus As String = "SaleOrderStatus"
#End Region


#Region "Queries Definition"
    Dim mHelpSiteQry$ = "Select 'o' As Tick, Code, Name AS [Site / Branch] FROM SiteMast WHERE CharIndex('|' || Code || '|', (SELECT Max(SiteList) FROM UserSite WHERE User_Name = 'SA'))>0 OR '" & AgL.PubUserName & "' IN ('SA','" & AgLibrary.ClsConstant.PubSuperUserName & "')"
    Dim mHelpDivisionQry$ = "Select 'o' As Tick, Div_Code AS Code, Div_Name AS [Division] FROM Division WHERE CharIndex('|' || Div_Code || '|', (SELECT Max(DivisionList) FROM UserSite WHERE User_Name = 'SA'))>0 OR '" & AgL.PubUserName & "' IN ('SA','" & AgLibrary.ClsConstant.PubSuperUserName & "')"
    Dim mHelpCityQry$ = "Select 'o' As Tick, CityCode, CityName From City "
    Dim mHelpStateQry$ = "Select 'o' As Tick, State_Code, State_Desc From State "
    Dim mHelpCurrencyQry$ = "Select 'o' As Tick, Code, Code, Description From Currency "
    Dim mHelpUserQry$ = "Select 'o' As Tick, User_Name As Code, User_Name As [User] From UserMast "
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
    Dim mHelpBuyerQry$ = " Select 'o' As Tick,  SG.SubCode As Code, Sg.DispName AS Vendor FROM SubGroup Sg WHERE SG.Nature ='Customer'"
    Dim mHelpAgentQry$ = " Select 'o' As Tick,  SG.SubCode As Code, Sg.DispName AS Vendor FROM SubGroup Sg WHERE SG.Nature ='Agent'"
    Dim mHelpEmployeeQry$ = " Select 'o' As Tick, SG.SubCode AS Code, SG.Name AS Employee  " & _
           " FROM SubGroup Sg " & _
           " WHERE IfNull(SG.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "'" & _
           " AND SG.Div_Code ='" & AgL.PubDivCode & " ' AND SG.Site_Code = '" & AgL.PubSiteCode & "' ORDER BY SG.Name"
    Dim mHelpItemType$ = "SELECT 'o' AS Tick, Code, Name  AS ItemType  FROM ItemType ORDER BY Name "
    Dim mHelpDepartment$ = "SELECT 'o' AS Tick, Code, Description AS Department FROM Department ORDER BY Description "


    Dim mHelpSaleOrder$ = " Select 'o' As Tick, H.DocID, Max(H.V_Type || '-' || H.ReferenceNo) AS OrderNo, Max(H.V_Date) AS OrderDate,  Max(H.PartyOrderNo) AS PartyOrderNo " &
                        " FROM SaleOrderDetail L  " &
                        " LEFT JOIN SaleOrder H ON L.SaleOrder  = H.DocID " &
                        " WHERE H.Div_Code = '" & AgL.PubDivCode & "'  And H.Site_Code = '" & AgL.PubSiteCode & "' " &
                        " Group By H.DocID "

    Dim mHelpSaleChallan$ = " Select 'o' As Tick, H.DocID, Max(H.V_Type || '- ' || H.ReferenceNo) AS ChallanNo , Max(H.V_Date) AS ChallanDate " &
                " FROM SaleChallanDetail L  " &
                " LEFT JOIN SaleChallan H ON L.SaleChallan  = H.DocID " &
                " WHERE H.Div_Code = '" & AgL.PubDivCode & "'  AND H.Site_Code = '" & AgL.PubSiteCode & "' " &
                " Group By H.DocID "

    Dim mHelpSaleInvoice$ = " Select 'o' As Tick, H.DocID, Max(H.V_Type || '- ' || H.ReferenceNo) AS InvoiceNo , Max(H.V_Date) AS InvoiceDate " &
                " FROM SaleInvoiceDetail L  " &
                " LEFT JOIN SaleInvoice H ON L.SaleInvoice  = H.DocID " &
                " WHERE H.Div_Code = '" & AgL.PubDivCode & "'  AND H.Site_Code = '" & AgL.PubSiteCode & "' " &
                " Group By H.DocID "

#End Region

    Dim DsRep As DataSet = Nothing, DsRep1 As DataSet = Nothing, DsRep2 As DataSet = Nothing
    Dim mQry$ = "", RepName$ = "", RepTitle$ = "", OrderByStr$ = ""

#Region "Initializing Grid"
    Public Sub Ini_Grid()
        Try
            Dim I As Integer = 0
            Select Case GRepFormName
                Case SaleOrderReport
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubEndDate)
                    ReportFrm.CreateHelpGrid("Report Type", "Report Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, "Select 'Detail' as Code, 'Detail' as Name Union All Select 'Item Wise Detail' as Code, 'Item Wise Detail' as Name Union All Select 'Item Wise Detail with Amount' as Code, 'Item Wise Detail with Amount' as Name Union All Select 'Item Wise Summary' as Code, 'Item Wise Summary' as Name Union All Select 'Item Wise Summary with Amount' as Code, 'Item Wise Summary with Amount' as Name Union All Select 'Month Wise Summary' as Code, 'Month Wise Summary' as Name Union All Select 'Party Wise Summary' as Code, 'Party Wise Summary' as Name", "Detail", , , 250, , False)
                    ReportFrm.CreateHelpGrid("Voucher Type", "Voucher Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, FGetVoucher_TypeQry("SaleOrder"))
                    ReportFrm.CreateHelpGrid("Party", "Party", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpBuyerQry, , , , 280)
                    ReportFrm.CreateHelpGrid("Agent", "Agent", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpAgentQry, , , , 280)
                    ReportFrm.CreateHelpGrid("Code", "Amount Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, FGetStructureFieldsQry(AgTemplate.ClsMain.Temp_NCat.SaleOrder), "Amount|Amount", , , , , False)
                    ReportFrm.CreateHelpGrid("Sale Order No", "Sale Order No", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpSaleOrder, , , 450)
                    ReportFrm.CreateHelpGrid("Item", "Item", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemQry, , , 600, 270)
                    ReportFrm.CreateHelpGrid("Item Group", "Item Group", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemGroupQry, , , 530)
                    ReportFrm.CreateHelpGrid("Item Category", "Item Category", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemCategoryQry, , , 430)
                    ReportFrm.CreateHelpGrid("Item Type", "Item Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemType)
                    ReportFrm.CreateHelpGrid("Item Reporting Group", "Item Reporting Group", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemReportingGroupQry, , , 500, 360)
                    ReportFrm.CreateHelpGrid("Currency", "Currency", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpCurrencyQry, , , 600, 270)

                Case SaleChallanReport
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubEndDate)
                    ReportFrm.CreateHelpGrid("Report Type", "Report Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, "Select 'Detail' as Code, 'Detail' as Name Union All Select 'Item Wise Summary' as Code, 'Item Wise Summary' as Name ", "Detail", , , , , False)
                    ReportFrm.CreateHelpGrid("Unit", "Unit", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, "Select 'Qty' as Code, 'Qty' as Name Union All Select 'Measure' as Code, 'Measure' AS Name Union All Select 'Qty & Measure' as Code, 'Qty & Measure' AS Name ", "Qty")
                    ReportFrm.CreateHelpGrid("Voucher Type", "Voucher Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, FGetMainVoucher_TypeQry("SaleChallanDetail", "LEFT JOIN SaleChallan H ON H.DocID = L.SaleChallan"))
                    ReportFrm.CreateHelpGrid("Party", "Party", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpBuyerQry, , , , 280)
                    ReportFrm.CreateHelpGrid("Order No", "Order No", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpSaleOrder, , , 450)
                    ReportFrm.CreateHelpGrid("Item", "Item", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemQry, , , 600, 270)
                    ReportFrm.CreateHelpGrid("Item Group", "Item Group", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemGroupQry, , , 530)
                    ReportFrm.CreateHelpGrid("Item Category", "Item Category", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemCategoryQry, , , 430)
                    ReportFrm.CreateHelpGrid("Item Type", "Item Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemType)
                    ReportFrm.CreateHelpGrid("Item Reporting Group", "Item Reporting Group", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemReportingGroupQry, , , 500, 360)

                Case SaleInvoiceReport
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubEndDate)
                    ReportFrm.CreateHelpGrid("Report Type", "Report Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, "Select 'Detail' as Code, 'Detail' as Name Union All Select 'Item Wise Detail' as Code, 'Item Wise Detail' as Name Union All Select 'Item Wise Summary' as Code, 'Item Wise Summary' as Name Union All Select 'Month Wise Summary' as Code, 'Month Wise Summary' as Name Union All Select 'Party Wise Summary' as Code, 'Party Wise Summary' as Name Union All Select 'Agent Wise Summary' as Code, 'Agent Wise Summary' as Name Union All Select 'Agent-Item Wise Summary' as Code, 'Agent-Item Wise Summary' as Name", "Detail", , , , , False)
                    ReportFrm.CreateHelpGrid("Voucher Type", "Voucher Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, FGetVoucher_TypeQry("SaleInvoice"))
                    ReportFrm.CreateHelpGrid("Party", "Party", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpBuyerQry, , , , 280)
                    ReportFrm.CreateHelpGrid("Agent", "Agent", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpAgentQry, , , , 280)
                    ReportFrm.CreateHelpGrid("Team/Individual", "Team/Individual", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, "Select 'Individual' as Code, 'Individual' as Name Union All Select 'Team' as Code, 'Team' as Name", "Team", , , , , False)
                    ReportFrm.CreateHelpGrid("CashCredit", "Cash/Credit", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, "Select 'Cash' as Code, 'Cash' as Name Union All Select 'Credit' as Code, 'Credit' as Name Union All Select 'Both' as Code, 'Both' as Name", "Both", , , , , False)
                    ReportFrm.CreateHelpGrid("Code", "Amount Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, FGetStructureFieldsQry(AgTemplate.ClsMain.Temp_NCat.PurchaseInvoice), "Amount|Amount", , , , , False)
                    ReportFrm.CreateHelpGrid("Sale Order", "Sale Order", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpSaleOrder, , , 450)
                    ReportFrm.CreateHelpGrid("Sale Challan", "Sale Challan", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpSaleChallan, , , 450)
                    ReportFrm.CreateHelpGrid("Sale Invoice", "Sale Invoice", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpSaleInvoice, , , 450)
                    ReportFrm.CreateHelpGrid("Item", "Item", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemQry, , , 600, 270)
                    ReportFrm.CreateHelpGrid("Item Group", "Item Group", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemGroupQry, , , 530)
                    ReportFrm.CreateHelpGrid("Item Category", "Item Category", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemCategoryQry, , , 430)
                    ReportFrm.CreateHelpGrid("Item Type", "Item Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemType)
                    ReportFrm.CreateHelpGrid("Item Reporting Group", "Item Reporting Group", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemReportingGroupQry, , , 500, 360)

                Case SaleOrderStatus
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubEndDate)
                    ReportFrm.CreateHelpGrid("Status On", "Status On", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubLoginDate)
                    ReportFrm.CreateHelpGrid("Report Type", "Report Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, "Select 'Summary' as Code, 'Summary' as Name Union All Select 'Detail' as Code, 'Detail' as Name  Union All Select 'Item Wise Order Status' as Code, 'Item Wise Order Status' as Name ", "Summary", , , , , False)
                    ReportFrm.CreateHelpGrid("Report For", "Report For", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, "SELECT 'All' AS Code, 'All' AS Name UNION ALL  SELECT 'Pending To Dispatch' AS Code, 'Pending To Dispatch' AS Name UNION ALL  SELECT 'Dispatched' AS Code, 'Dispatched' AS Name UNION ALL  SELECT 'Over Due' AS Code, 'Over Due' AS Name UNION ALL  SELECT 'Over Due And Balance' AS Code, 'Over Due And Balance' AS Name UNION ALL  SELECT 'Timely Dispatched' AS Code, 'Timely Dispatched' AS Name ")
                    ReportFrm.CreateHelpGrid("Unit", "Unit", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, "Select 'Qty' as Code, 'Qty' as Name Union All Select 'Measure' as Code, 'Measure' AS Name Union All Select 'Qty & Measure' as Code, 'Qty & Measure' AS Name ", "Qty")
                    ReportFrm.CreateHelpGrid("Sort On", "Sort On", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, " SELECT 'Order Date' AS Code, 'Order Date' AS Name UNION ALL  SELECT 'Due Date' AS Code, 'Due Date' AS Name UNION ALL SELECT 'Over Due Days' AS Code, 'Over Due Days' AS Name UNION ALL  SELECT 'Balance Qty' AS Code, 'Balance Qty' AS Name ", "Order Date", , , , , False)
                    ReportFrm.CreateHelpGrid("Voucher Type", "Voucher Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, FGetMainVoucher_TypeQry("SaleOrderDetail", "LEFT JOIN SaleOrder H ON H.DocID = L.SaleOrder"))
                    ReportFrm.CreateHelpGrid("Party", "Party", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpBuyerQry, , , , 280)
                    ReportFrm.CreateHelpGrid("Agent", "Agent", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpAgentQry, , , , 280)
                    ReportFrm.CreateHelpGrid("Sale Order No", "Sale Order No", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpSaleOrder, , , 450)
                    ReportFrm.CreateHelpGrid("Item", "Item", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemQry, , , 600, 270)
                    ReportFrm.CreateHelpGrid("Item Group", "Item Group", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemGroupQry, , , 530)
                    ReportFrm.CreateHelpGrid("Item Category", "Item Category", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemCategoryQry, , , 430)
                    ReportFrm.CreateHelpGrid("Item Type", "Item Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemType)
                    ReportFrm.CreateHelpGrid("Item Reporting Group", "Item Reporting Group", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemReportingGroupQry, , , 500, 360)

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
            " ORDER BY Vt.Description "

    End Function

    Private Sub ObjRepFormGlobal_ProcessReport() Handles ReportFrm.ProcessReport
        Select Case mGRepFormName
            Case SaleOrderReport
                ProcSaleOrderReport()

            Case SaleOrderStatus
                ProcSaleOrderStatusReport()

            Case SaleChallanReport
                ProcSaleChallanReport()

            Case SaleInvoiceReport
                ProcSaleInvoiceReport()

        End Select
    End Sub

    Public Sub New(ByVal mReportFrm As ReportLayout.FrmReportLayout)
        ReportFrm = mReportFrm
    End Sub


#Region "Sale Order Report"
    Private Sub ProcSaleOrderReport()
        Dim mCondStr$ = ""
        Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"

        Try
          If ReportFrm.FGetText(2) = "Party Wise Summary" Then
                RepName = "Sales_SaleOrderReport_Summary" : RepTitle = "Sale Order Report (" & ReportFrm.FGetText(2) & ")"
                strGrpFld = "SG.Name"
                strGrpFldDesc = "SG.Name || ',' || IfNull(City.CityName,'')"
                strGrpFldHead = "'Party Name'"
            ElseIf ReportFrm.FGetText(2) = "Month Wise Summary" Then
                RepName = "Sales_SaleOrderReport_Summary" : RepTitle = "Sale Order Report (" & ReportFrm.FGetText(2) & ")"
                strGrpFld = "Substring(convert(nvarchar,H.V_Date,11),0,6)"
                strGrpFldDesc = "Replace(SubString(Convert(VARCHAR,H.v_Date,6),4,6),' ','-')"
                strGrpFldHead = "'Month'"
            ElseIf ReportFrm.FGetText(2) = "Item Wise Detail" Then
                RepName = "Sales_SaleOrderReport_ItemWiseDetail"
                RepTitle = "Item Wise Sale Order Report"
            ElseIf ReportFrm.FGetText(2) = "Item Wise Detail with Amount" Then
                RepName = "Sales_SaleOrderReport_ItemWiseDetailwithAmount"
                RepTitle = "Item Wise Sale Order Report"
            ElseIf ReportFrm.FGetText(2) = "Item Wise Summary" Then
                RepName = "Sales_SaleOrderReport_ItemWiseSummary"
                RepTitle = "Item Wise Sale Order Summary"
            ElseIf ReportFrm.FGetText(2) = "Item Wise Summary with Amount" Then
                RepName = "Sales_SaleOrderReport_ItemWiseSummarywithAmount"
                RepTitle = "Item Wise Sale Order Summary"
            Else
                RepName = "Sales_SaleOrderReport"
                RepTitle = "Sale Order Report"
            End If

            mCondStr = mCondStr & " And H.V_Date Between " & AgL.Chk_Text(CDate(ReportFrm.FGetText(0)).ToString("u")) & " And " & AgL.Chk_Text(CDate(ReportFrm.FGetText(1)).ToString("u")) & " "
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.V_Type", 3)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.SaleToParty", 4)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.Agent", 5)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.SaleOrder", 7)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Item", 8)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemGroup", 9)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemCategory", 10)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemType ", 11)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.Currency ", 13)

            If ReportFrm.FGetText(12) <> "" And ReportFrm.FGetText(12) <> "All" Then
                mQry = " Select '''' ||  replace(ItemList,',',''',''')  || ''''  From ItemReportingGroup Where 1=1 " & ReportFrm.GetWhereCondition("Code", 10)
                mCondStr = mCondStr & " AND I.Code In (" & AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar & ")"
            End If

            mCondStr = mCondStr & " And H.Site_Code = '" & AgL.PubSiteCode & "' "
            mCondStr = mCondStr & " And H.Div_Code= '" & AgL.PubDivCode & "' "

            mQry = " SELECT H.DocID, H.V_Type, H.V_Date, H.ReferenceNo, C.Description AS Currency, U.DecimalPlaces,  H.PartyOrderNo, " &
                    " SG.DispName + (Case When City.CityName Is Not Null then ', ' ||  City.CityName Else '' End) AS SaleToPartyName, " &
                    " H.PartyDeliveryDate, " & strGrpFld & " as GrpField, " & strGrpFldDesc & " as GrpFieldDesc, " & strGrpFldHead & " as GrpFieldHead, " &
                    " H.PartyOrderDate, H.Remarks, L.Remarks AS LineRemark, Convert(SmallDateTime,'01 ' || SubString(Convert(Varchar,H.V_Date,6),4,6)) as V_Month, " &
                    " L.Sr, L.Item, L.Qty, L.Unit, L.MeasurePerPcs, L.MeasureUnit, L.TotalMeasure, L.Rate, " &
                    " L." & Replace(ReportFrm.FGetCode(6), "'", "") & " as Amount, '" & ReportFrm.FGetText(6) & "' as AmountTitle, " &
                    " L.Landed_Value, L.RateType, I.Description AS ItemDesc, Vt.Description AS VoucherTypeDesc  " &
                    " FROM SaleOrder H " &
                    " LEFT JOIN SubGroup SG ON SG.SubCode = H.SaleToParty  " &
                    " LEFT JOIN City On SG.CityCode = City.CityCode " &
                    " LEFT JOIN SaleOrderDetail L ON L.DocId = H.DocID  " &
                    " LEFT JOIN Item I ON I.Code = L.Item  " &
                    " LEFT JOIN Voucher_Type Vt ON Vt.V_Type = H.V_Type  " &
                    " LEFT JOIN Unit U ON U.Code = L.Unit  " &
                    " LEFT JOIN Currency C ON C.Code = H.Currency  " &
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

#Region "Sale Challan Report"
    Private Sub ProcSaleChallanReport()
        Dim mCondStr$ = ""
        Try
            If ReportFrm.FGetText(2) = "Detail" Then
                RepTitle = "Sale Challan Report"
                If ReportFrm.FGetText(3) = "Measure" Then
                    RepName = "Sales_SaleChallanReport_ItemWiseDetail_Measure"
                ElseIf ReportFrm.FGetText(3) = "Qty & Measure" Then
                    RepName = "Sales_SaleChallanReport_ItemWiseDetail_QtyMeasure"
                Else
                    RepName = "Sales_SaleChallanReport_ItemWiseDetail"
                End If
            Else
                RepTitle = "Item Wise Sale Challan Summary"
                If ReportFrm.FGetText(3) = "Measure" Then
                    RepName = "Sales_SaleChallanReport_ItemWiseSummary_Measure"
                ElseIf ReportFrm.FGetText(3) = "Qty & Measure" Then
                    RepName = "Sales_SaleChallanReport_ItemWiseSummary_QtyMeasure"
                Else
                    RepName = "Sales_SaleChallanReport_ItemWiseSummary"
                End If
            End If

            mCondStr = mCondStr & " And H.V_Date Between " & AgL.Chk_Text(CDate(ReportFrm.FGetText(0)).ToString("u")) & " And " & AgL.Chk_Text(CDate(ReportFrm.FGetText(1)).ToString("u")) & " "
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.V_Type", 4)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.SaleToParty", 5)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.SaleOrder", 6)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Item", 7)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemGroup", 8)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemCategory", 9)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemType ", 10)

            If ReportFrm.FGetText(11) <> "" And ReportFrm.FGetText(11) <> "All" Then
                mQry = " Select '''' ||  replace(ItemList,',',''',''')  || ''''  From ItemReportingGroup Where 1=1 " & ReportFrm.GetWhereCondition("Code", 11)
                mCondStr = mCondStr & " AND I.Code In (" & AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar & ")"
            End If

            mCondStr = mCondStr & " And H.Site_Code = '" & AgL.PubSiteCode & "' "
            mCondStr = mCondStr & " And H.Div_Code= '" & AgL.PubDivCode & "' "

            mQry = " SELECT H.DocID, H.V_Type, H.V_Date, H.V_No, H.ReferenceNo, G.Description AS GodownDesc,  H.Remarks, U.DecimalPlaces, " &
                    " L.Sr, L.Item , L.DocQty, L.RejQty, L.Qty, L.Unit, L.MeasurePerPcs, L.MeasureUnit, L.TotalMeasure, UM.DecimalPlaces AS MeasureDecimalPlace, " &
                    " L.Rate, L.Amount, L.Remark AS LineRemark, L.SaleOrder, L.SaleOrderSr , I.Description AS ItemDesc, " &
                    " SG.DispName + (Case When City.CityName Is Not Null then ', ' ||  City.CityName Else '' End) AS PartyName, " &
                    " SO.ReferenceNo AS SaleOrderNo ,  Vt.Description AS VoucherTypeDesc " &
                    " FROM SaleChallan H " &
                    " LEFT JOIN Godown G ON G.Code = H.Godown  " &
                    " LEFT JOIN SubGroup SG ON SG.SubCode = H.SaleToParty " &
                    " LEFT JOIN City On SG.CityCode = City.CityCode " &
                    " LEFT JOIN SaleChallanDetail L ON L.DocId = H.DocID  " &
                    " LEFT JOIN Item I ON I.Code = L.Item  " &
                    " LEFT JOIN SaleOrder SO ON SO.DocID = L.SaleOrder " &
                    " LEFT JOIN Voucher_Type Vt ON Vt.V_Type = H.V_Type " &
                    " LEFT JOIN Unit U ON U.Code = L.Unit " &
                    " LEFT JOIN Unit UM ON UM.Code = L.MeasureUnit " &
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

#Region "Sale Invoice Report"
    Private Sub ProcSaleInvoiceReport()
        Dim mCondStr$ = ""
        Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"
        Try
            If ReportFrm.FGetText(2) = "Detail" Then
                RepName = "Sales_SaleInvoiceReport" : RepTitle = "Sale Invoice Report"
            ElseIf ReportFrm.FGetText(2) = "Item Wise Detail" Then
                RepName = "Sales_SaleInvoiceReport_ItemWiseDetail" : RepTitle = "Item Wise Sale Invoice Report"
            ElseIf ReportFrm.FGetText(2) = "Party Wise Summary" Then
                RepName = "Sales_SaleInvoiceReport_Summary" : RepTitle = "Sale Invoice Report (" & ReportFrm.FGetText(2) & ")"
                strGrpFld = "SG.Name"
                strGrpFldDesc = "SG.Name || ',' || IfNull(City.CityName,'')"
                strGrpFldHead = "'Party Name'"
            ElseIf ReportFrm.FGetText(2) = "Agent Wise Summary" Then
                RepName = "Sales_SaleInvoiceReport_Summary" : RepTitle = "Sale Invoice Report (" & ReportFrm.FGetText(2) & ")"
                strGrpFld = "Agent.Name"
                strGrpFldDesc = "Agent.Name || ',' || IfNull(CI.CityName,'')"
                strGrpFldHead = "'Agent Name'"
            ElseIf ReportFrm.FGetText(2) = "Agent-Item Wise Summary" Then
                RepName = "Sales_SaleInvoiceReport_AgentItemWiseSummary" : RepTitle = "Sale Invoice Report (" & ReportFrm.FGetText(2) & ")"
            ElseIf ReportFrm.FGetText(2) = "Month Wise Summary" Then
                RepName = "Sales_SaleInvoiceReport_Summary" : RepTitle = "Sale Invoice Report (" & ReportFrm.FGetText(2) & ")"
                strGrpFld = "Substring(convert(nvarchar,H.V_Date,11),0,6)"
                strGrpFldDesc = "Replace(SubString(Convert(VARCHAR,H.v_Date,6),4,6),' ','-')"
                strGrpFldHead = "'Month'"
            ElseIf ReportFrm.FGetText(2) = "Item Wise Summary" Then
                RepName = "Sales_SaleInvoiceReport_ItemWiseSummary" : RepTitle = "Sale Invoice Report (" & ReportFrm.FGetText(2) & ")"
            End If

            mCondStr = mCondStr & " And H.V_Date Between " & AgL.Chk_Text(CDate(ReportFrm.FGetText(0)).ToString("u")) & " And " & AgL.Chk_Text(CDate(ReportFrm.FGetText(1)).ToString("u")) & " "
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.V_Type", 3)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.SaleToParty", 4)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.Agent", 5)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.SaleOrder", 9)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.SaleChallan", 10)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.SaleInvoice", 11)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Item", 12)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemGroup", 13)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemCategory", 14)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemType ", 15)

            If ReportFrm.FGetText(7) = "Cash" Then
                mCondStr = mCondStr & " AND Sg.Nature = 'Cash'"
            ElseIf ReportFrm.FGetText(7) = "Credit" Then
                mCondStr = mCondStr & " AND Sg.Nature <> 'Cash'"
            End If

            If ReportFrm.FGetText(5) <> "All" Then
                If ReportFrm.FGetText(6) = "Team" Then
                    mCondStr += " And CharIndex('" & AgL.XNull(ReportFrm.FGetCode(8)) & "',H.Upline) > 0 "
                Else
                    mCondStr += " And H.Agent = '" & ReportFrm.FGetCode(8) & "'"
                End If
            End If

            If ReportFrm.FGetText(16) <> "" And ReportFrm.FGetText(16) <> "All" Then
                mQry = " Select '''' ||  replace(ItemList,',',''',''')  || ''''  From ItemReportingGroup Where 1=1 " & ReportFrm.GetWhereCondition("Code", 16)
                mCondStr = mCondStr & " AND I.Code In (" & AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar & ")"
            End If

            mCondStr = mCondStr & " And H.Site_Code = '" & AgL.PubSiteCode & "' "
            mCondStr = mCondStr & " And H.Div_Code= '" & AgL.PubDivCode & "' "

            mQry = " SELECT " & strGrpFld & " as GrpField, " & strGrpFldDesc & " as GrpFieldDesc, " & strGrpFldHead & " as GrpFieldHead, C.Description AS Currency, " &
                    " H.DocID, H.V_Type, H.V_Date, H.ReferenceNo, SG.DispName + (Case When City.CityName Is Not Null then ', ' ||  City.CityName Else '' End) AS SaleToPartyName, " &
                    " Agent.DispName + (Case When CI.CityName Is Not Null then ', ' ||  CI.CityName Else '' End) AS AgentName, " &
                    " L.Sr, L.SaleChallan, L.Item, L.Qty, L.Unit, U.DecimalPlaces AS QtyDecimalplace, L.MeasurePerPcs , L.MeasureUnit, L.TotalMeasure,  " &
                    " L.Rate , L.SaleChallanSr, L." & Replace(ReportFrm.FGetCode(8), "'", "") & " as Amount, '" & ReportFrm.FGetText(8) & "' as AmountTitle, IfNull(L." & Replace(ReportFrm.FGetCode(8), "'", "") & ",0)/L.Qty AS NetAmtRate, I.Description AS ItemDesc,  " &
                    " Vt.Description AS VoucherTypeDesc, PC.V_Type || '- ' || PC.ReferenceNo AS ChallanNo, H.Remarks as H_Remarks, L.Remark as LineRemarks  " &
                    " FROM SaleInvoice H " &
                    " LEFT JOIN SubGroup SG ON SG.SubCode = H.SaleToParty " &
                    " Left Join SubGroup Agent On H.Agent = Agent.SubCode " &
                    " LEFT JOIN City CI On Agent.CityCode = CI.CityCode " &
                    " LEFT JOIN City On SG.CityCode = City.CityCode " &
                    " LEFT JOIN SaleInvoiceDetail L ON L.DocId = H.DocID  " &
                    " LEFT JOIN Item I ON I.Code = L.Item   " &
                    " LEFT JOIN Voucher_Type Vt ON Vt.V_Type = H.V_Type " &
                    " LEFT JOIN SaleChallan PC ON PC.DocID = L.SaleChallan " &
                    " LEFT JOIN Unit U ON U.Code = L.Unit  " &
                    " LEFT JOIN Currency C ON C.Code = H.Currency  " &
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

#Region "Sale Order Status Report"
    Private Sub ProcSaleOrderStatusReport()
        Dim mCondStr$ = ""
        Dim mStrSortOn$ = ""
        Dim mQrySaleChallan$ = " SELECT PCD.SaleOrder, PCD.SaleOrderSr , PC.ReferenceNo AS ChallanNo, PC.V_Date AS ChallanDate, PCD.Qty AS ChallanQty, PCD.TotalMeasure AS ChallanMeasure " &
                    " FROM SaleChallanDetail PCD " &
                    " LEFT JOIN SaleChallan PC ON PC.DocID = PCD.DocId  " &
                    " WHERE IfNull( PCD.SaleOrder,'') <> '' " &
                    " AND PC.V_Date <= '" & ReportFrm.FGetText(2) & "' "

        Dim mQrySaleChallanSummury$ = " SELECT PCD.SaleOrder, PCD.SaleOrderSr ,  sum(PCD.Qty) AS TotalChallanQty, sum(PCD.TotalMeasure) AS TotalChallanMeasure, max(PC.V_Date) AS MaxChallanDate " &
                    " FROM SaleChallanDetail PCD " &
                    " LEFT JOIN SaleChallan PC ON PC.DocID = PCD.DocId  " &
                    " WHERE IfNull( PCD.SaleOrder,'') <> '' " &
                    " AND PC.V_Date <= '" & ReportFrm.FGetText(2) & "' " &
                    " GROUP BY PCD.SaleOrder, PCD.SaleOrderSr "

        Dim mQrySaleOrder$ = " SELECT POD.SaleOrder, POD.SaleOrderSr, sum(POD.Qty) AS BalOrdQty, sum(POD.TotalMeasure) AS BalOrdMeasure   " &
                            " FROM SaleOrderDetail POD  " &
                            " LEFT JOIN SaleOrder PO ON PO.DocId = POD.DocId " &
                            " WHERE IfNull(POD.SaleOrder,'') <> '' " &
                            " AND PO.V_Date <= '" & ReportFrm.FGetText(2) & "' " &
                            " GROUP BY POD.SaleOrder, POD.SaleOrderSr "
        Try

            If ReportFrm.FGetText(3) = "Detail" Then
                RepTitle = "Sale Order Status Report"
                If ReportFrm.FGetText(5) = "Measure" Then
                    RepName = "Sales_SaleOrderStatusReport_Detail_Measure"
                ElseIf ReportFrm.FGetText(5) = "Qty & Measure" Then
                    RepName = "Sales_SaleOrderStatusReport_Detail_QtyMeasure"
                Else
                    RepName = "Sales_SaleOrderStatusReport_Detail"
                End If
            ElseIf ReportFrm.FGetText(3) = "Item Wise Order Status" Then
                RepTitle = "Item Wise Sale Order Status"
                If ReportFrm.FGetText(5) = "Measure" Then
                    RepName = "Sales_SaleOrderStatusReport_ItemWise_Measure"
                ElseIf ReportFrm.FGetText(5) = "Qty & Measure" Then
                    RepName = "Sales_SaleOrderStatusReport_ItemWise_QtyMeasure"
                Else
                    RepName = "Sales_SaleOrderStatusReport_ItemWise"
                End If
            Else
                RepTitle = "Sale Order Status Report"
                If ReportFrm.FGetText(5) = "Measure" Then
                    RepName = "Sales_SaleOrderStatusReport_Measure"
                ElseIf ReportFrm.FGetText(5) = "Qty & Measure" Then
                    RepName = "Sales_SaleOrderStatusReport_QtyMeasure"
                Else
                    RepName = "Sales_SaleOrderStatusReport"
                End If
            End If

            If ReportFrm.FGetText(6) = "Order Date" Then
                mStrSortOn = "H.V_Date"
            ElseIf ReportFrm.FGetText(6) = "Due Date" Then
                mStrSortOn = "H.PartyDeliveryDate"
            ElseIf ReportFrm.FGetText(6) = "Over Due Days" Then
                mStrSortOn = " CASE WHEN IfNull(VPO.BalOrdQty,0) - IfNull(VPCS.TotalChallanQty,0) > 0 THEN  datediff(Day,H.PartyDeliveryDate,'" & ReportFrm.FGetText(2) & "') ELSE datediff(Day,H.PartyDeliveryDate,VPCS.MaxChallanDate) END "
            ElseIf ReportFrm.FGetText(6) = "Balance Qty" Then
                mStrSortOn = " IfNull(VPO.BalOrdQty,0) - IfNull(VPCS.TotalChallanQty,0) "
            End If

            mCondStr = mCondStr & " And H.V_Date Between " & AgL.Chk_Text(CDate(ReportFrm.FGetText(0)).ToString("u")) & " And " & AgL.Chk_Text(CDate(ReportFrm.FGetText(1)).ToString("u")) & " "

            If ReportFrm.FGetText(4) = "Pending To Dispatch" Then
                mCondStr = mCondStr & " AND IfNull(VPO.BalOrdQty,0) - IfNull(VPCS.TotalChallanQty,0) > 0 "
            ElseIf ReportFrm.FGetText(4) = "Dispatched" Then
                mCondStr = mCondStr & " AND IfNull(VPO.BalOrdQty,0) - IfNull(VPCS.TotalChallanQty,0) <= 0 "
            ElseIf ReportFrm.FGetText(4) = "Over Due" Then
                mCondStr = mCondStr & "  AND H.PartyDeliveryDate < IfNull(VPC.ChallanDate,'" & ReportFrm.FGetText(2) & "') "
            ElseIf ReportFrm.FGetText(4) = "Over Due And Balance" Then
                mCondStr = mCondStr & " AND IfNull(VPO.BalOrdQty,0) - IfNull(VPCS.TotalChallanQty,0) > 0 "
                mCondStr = mCondStr & "  AND H.PartyDeliveryDate < IfNull(VPC.ChallanDate,'" & ReportFrm.FGetText(2) & "') "
            ElseIf ReportFrm.FGetText(4) = "Timely Dispatched" Then
                mCondStr = mCondStr & " AND IfNull(VPO.BalOrdQty,0) - IfNull(VPCS.TotalChallanQty,0) <= 0 "
                mCondStr = mCondStr & " AND H.PartyDeliveryDate >= " &
                                        " ( SELECT Max(SC.V_Date)  " &
                                        " FROM SaleChallanDetail SCD " &
                                        " LEFT JOIN SaleChallan SC ON SC.DocID = SCD.DocId  " &
                                        " WHERE SCD.SaleOrder = H.DocId " &
                                        " GROUP BY SCD.SaleOrder  " &
                                        " ) "
            End If

            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.V_Type", 7)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.SaleToParty", 8)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.Agent", 9)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.SaleOrder ", 10)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Item", 11)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemGroup", 12)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemCategory ", 13)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemType ", 14)


            If ReportFrm.FGetText(15) <> "" And ReportFrm.FGetText(15) <> "All" Then
                mQry = " Select '''' ||  replace(ItemList,',',''',''')  || ''''  From ItemReportingGroup Where 1=1 " & ReportFrm.GetWhereCondition("Code", 15)
                mCondStr = mCondStr & " AND I.Code In (" & AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar & ")"
            End If

            mCondStr = mCondStr & " And H.Site_Code = '" & AgL.PubSiteCode & "' "
            mCondStr = mCondStr & " And H.Div_Code= '" & AgL.PubDivCode & "' "

            mQry = " SELECT H.DocID, H.V_Type, H.V_Date, H.ReferenceNo, C.Description AS Currency, U.DecimalPlaces, UM.DecimalPlaces AS MeasureDecimalPlace, H.PartyDeliveryDate," &
                    " SG.DispName + (Case When City.CityName Is Not Null then ', ' ||  City.CityName Else '' End) AS SaleToPartyName, " &
                    " H.PartyOrderNo, H.PartyOrderDate, H.Remarks, L.Sr, CASE WHEN IsNumeric(H.ReferenceNo) > 0 THEN Convert(INT, H.ReferenceNo) ELSE 0 END AS  OrderNo," &
                    " L.Item, L.Qty, L.Unit, L.MeasurePerPcs, L.MeasureUnit, L.TotalMeasure, L.Rate, L.Amount,  " &
                    " L.Remarks AS LineRemark, L.RateType, I.Description AS ItemDesc, Vt.Description AS VoucherTypeDesc,  " &
                    " VPC.ChallanNo, VPC.ChallanDate, IfNull(VPC.ChallanQty,0) AS ChallanQty,  IfNull(VPC.ChallanMeasure,0) AS ChallanMeasure ,   " &
                    " CASE WHEN IfNull(VPO.BalOrdQty,0) - IfNull(VPCS.TotalChallanQty,0) > 0 THEN  datediff(Day,H.PartyDeliveryDate,'" & ReportFrm.FGetText(2) & "') ELSE datediff(Day,H.PartyDeliveryDate,VPCS.MaxChallanDate) END AS Ageing, " &
                    " IfNull(VPO.BalOrdQty,0) AS TotalOrdQty, IfNull(VPO.BalOrdMeasure,0) AS TotalOrdMeasure, " & mStrSortOn & " AS OrderOn, " &
                    " IfNull(VPO.BalOrdQty,0) - IfNull(VPCS.TotalChallanQty,0) AS TotalBalQty   ,  IfNull(VPO.BalOrdMeasure,0) - IfNull(VPCS.TotalChallanMeasure,0) AS TotalBalMeasure  " &
                    " FROM SaleOrder H " &
                    " LEFT JOIN SaleOrderDetail L ON L.DocId = H.DocID  " &
                    " LEFT JOIN SubGroup SG ON SG.SubCode = H.SaleToParty  " &
                    " LEFT JOIN City On SG.CityCode = City.CityCode " &
                    " LEFT JOIN Item I ON I.Code = L.Item  " &
                    " LEFT JOIN Voucher_Type Vt ON Vt.V_Type = H.V_Type " &
                    " LEFT JOIN Unit U ON U.Code = L.Unit " &
                    " LEFT JOIN Unit UM ON UM.Code = L.MeasureUnit " &
                    " LEFT JOIN Currency C ON C.Code = H.Currency  " &
                    " LEFT JOIN ( " & mQrySaleChallan & " ) VPC ON VPC.SaleOrder = L.DocId AND VPC.SaleOrderSr = L.Sr " &
                    " LEFT JOIN ( " & mQrySaleChallanSummury & " ) VPCS ON VPCS.SaleOrder = L.DocId AND VPCS.SaleOrderSr = L.Sr " &
                    " LEFT JOIN ( " & mQrySaleOrder & " ) VPO ON VPO.SaleOrder = L.DocId AND VPO.SaleOrderSr = L.Sr " &
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
