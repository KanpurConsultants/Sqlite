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

    Public Sub New(ByVal mReportFrm As ReportLayout.FrmReportLayout)
        ReportFrm = mReportFrm
    End Sub

#Region "Common Reports Constant"
    Private Const CityList As String = "CityList"
    Private Const UserWiseEntryReport As String = "UserWiseEntryReport"
    Private Const UserWiseEntryTargetReport As String = "UserWiseEntryTargetReport"
#End Region

#Region "Reports Constant"
    Private Const RequisitionReport As String = "RequisitionReport"
    Private Const RequisitionStatus As String = "RequisitionStatus"
    Private Const ItemIssueReport As String = "ItemIssueReport"
    Private Const ItemReceiveReport As String = "ItemReceiveReport"
    Private Const StockTransferReport As String = "StockTransferReport"
    Private Const PhysicalStockReport As String = "PhysicalStockReport"
    Private Const StockInHand As String = "StockInHand"
    Private Const StockInProcess As String = "StockInProcess"
    Private Const StockBalance As String = "StockBalance"
    Private Const MaterialIssueSummary As String = "MaterialIssueSummary"
    Private Const MaterialReceiveSummary As String = "MaterialReceiveSummary"
    Private Const StockTransferSummary As String = "StockTransferSummary"
    Private Const StockBalanceValuation As String = "StockBalanceValuation"
    Private Const StockBalanceWithAverageRate As String = "StockBalanceWithAverageRate"
#End Region

#Region "Queries Definition"
    'Dim VtypeRestriction$ = " AND H.V_Type NOT IN " & _
    '                " ( Select L.V_Type " & _
    '                " FROM User_Exclude_VTypeDetail L  " & _
    '                " WHERE L.UserName = " & AgL.Chk_Text(AgL.PubUserName) & " ) "

    Dim VtypeRestriction$ = " AND ( H.V_Type IN " & _
                " ( Select V_Type From User_VType_Permission VP Where VP.UserName = '" & AgL.PubUserName & "' And VP.Div_Code = '" & AgL.PubDivCode & "' And VP.Site_Code = '" & AgL.PubSiteCode & "' ) OR '" & AgL.PubUserName & "' IN ('SA','SUPER')) "

    Dim mHelpCityQry$ = "Select 'o' As Tick, CityCode, CityName From City "
    Dim mHelpStateQry$ = "Select 'o' As Tick,State_Code, State_Desc From State "
    Dim mHelpUserQry$ = "Select 'o' As Tick,User_Name As Code, User_Name As [User] From UserMast "
    Dim mHelpSiteQry$ = "Select 'o' As Tick, Code, Name As [Site] From SiteMast Where " & AgL.PubSiteCondition("Code", AgL.PubSiteCode) & " "
    Dim mHelpItemQry$ = "Select 'o' As Tick, I.Code, I.Description As [Item], Ig.Description As [Item Group], Ic.Description As [Item Category], It.Name As [Item Type] From Item I LEFT JOIN ItemGroup Ig ON I.ItemGroup = Ig.Code LEFT JOIN ItemCategory Ic On I.ItemCategory = Ic.Code LEFT JOIN ItemType It On I.ItemType = It.Code "
    Dim mHelpItemGroupQry$ = "Select 'o' As Tick, IG.Code, IG.Description As [Item Group], IC.Description as [Item Category], IT.Name as [Item Type] " & _
                         "From ItemGroup IG " & _
                         "LEFT JOIN ItemCategory IC ON IG.ItemCategory = IC.Code  " & _
                         "LEFT JOIN ItemType IT ON IC.ItemType = IT.Code  "
    Dim mHelpItemCategoryQry$ = "Select 'o' As Tick, IC.Code, IC.Description As [Item Category], IT.Name as [Item Type] " & _
                                "From ItemCategory IC " & _
                                "LEFT JOIN ItemType IT ON IC.ItemType = IT.Code  "
    Dim mHelpItemReportingGroupQry$ = "Select 'o' As Tick, Code, Description As [Group Name] From ItemReportingGroup "
    Dim mHelpVendorQry$ = " Select 'o' As Tick, H.SubCode As Code, Sg.DispName AS Vendor FROM Vendor H LEFT JOIN SubGroup Sg ON H.SubCode = Sg.SubCode "
    Dim mHelpDivisionQry$ = "Select 'o' As Tick, Div_Code AS Code,Div_Name AS Division FROM Division WHERE 1=1 " & AgL.RetDivisionCondition(AgL, "Div_Code") & " "
    Dim mHelpPartyQry$ = " Select 'o' As Tick, Sg.SubCode As Code, Sg.DispName AS Party FROM SubGroup Sg Where Sg.Nature In ('Customer','Supplier') "
    Dim mHelpEmployeeQry$ = " Select 'o' As Tick, SG.SubCode AS Code, SG.Name AS Employee  " & _
       " FROM SubGroup Sg " & _
       " WHERE ISNULL(SG.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "'" & _
       " AND SG.Div_Code ='" & AgL.PubDivCode & " ' AND SG.Site_Code = '" & AgL.PubSiteCode & "' ORDER BY SG.Name"
    Dim mHelpItemType$ = "SELECT 'o' AS Tick, Code, Name  AS ItemType  FROM ItemType ORDER BY Name "
    Dim mHelpDepartment$ = "SELECT 'o' AS Tick, Code, Description AS Department FROM Department ORDER BY Description "
    Dim mHelpGodownQry$ = "Select 'o' As Tick, Code,Description AS Godown FROM Godown WHERE Site_Code = '" & AgL.PubSiteCode & "' "
    Dim mHelpRequisitionNo$ = " SELECT 'o' As Tick, H.DocID, H.ReferenceNo AS RequisitionNo, H.V_Date AS RequisitionDate   FROM Requisition H " & _
                " WHERE H.Div_Code = '" & AgL.PubDivCode & "'  AND H.Site_Code = '" & AgL.PubSiteCode & "'  " & VtypeRestriction & " "
    Dim mHelpItemDivsionQry$ = "Select 'o' As Tick, Div_Code, Div_Name AS Division FROM Division"
    Dim mHelpReasonQry$ = "Select 'o' As Tick, Code, Description AS Reason FROM Reason "

    Dim mHelpJobWorkerQry$ = " Select 'o' As Tick,  S.SubCode AS Code,S.Name AS Worker,C.CityName AS City " & _
                     " FROM SubGroup S " & _
                     " LEFT JOIN City C ON C.CityCode = S.CityCode  " & _
                     " WHERE CharIndex('|' + '" & AgL.PubDivCode & "' + '|', S.DivisionList) > 0 " & _
                     " AND S.Site_Code = '" & AgL.PubSiteCode & "' " & _
                     " And ISNULL(S.Status,'" & AgTemplate.ClsMain.EntryStatus.Active & "') = '" & AgTemplate.ClsMain.EntryStatus.Active & "'"
    Dim mHelpProcessQry$ = "Select 'o' As Tick,  NCat AS Code, Description, StockHead FROM Process "
    Dim mHelpDimension1$ = "SELECT 'o' AS Tick, Code, Description AS Dimension1 FROM Dimension1 ORDER BY Description"
    Dim mHelpDimension2$ = "SELECT 'o' AS Tick, Code, Description AS Dimension2 FROM Dimension2 ORDER BY Description"
    Dim mHelpLotQry$ = "Select 'o' As Tick, S.LotNo AS Code, S.LotNo FROM Stock S WHERE isnull(S.LotNo,'') <> '' GROUP BY S.LotNo "

    Dim mHelpLotDimensionQry$ = " Select 'o' As Tick,  '" & AgTemplate.ClsMain.FGetDimension1Caption() & "' AS Code, '" & AgTemplate.ClsMain.FGetDimension1Caption() & "' AS Name " & _
                                " UNION ALL  " & _
                                " Select 'o' As Tick,  '" & AgTemplate.ClsMain.FGetDimension2Caption() & "' AS Code, '" & AgTemplate.ClsMain.FGetDimension2Caption() & "' AS Name " & _
                                " UNION ALL  " & _
                                " Select 'o' As Tick, 'Lot No' AS Code, 'Lot No' AS Name "

    Dim mHelpLotDimensionProcessQry$ = " Select 'o' As Tick,  '" & AgTemplate.ClsMain.FGetDimension1Caption() & "' AS Code, '" & AgTemplate.ClsMain.FGetDimension1Caption() & "' AS Name " & _
                            " UNION ALL  " & _
                            " Select 'o' As Tick,  '" & AgTemplate.ClsMain.FGetDimension2Caption() & "' AS Code, '" & AgTemplate.ClsMain.FGetDimension2Caption() & "' AS Name " & _
                            " UNION ALL  " & _
                            " Select 'o' As Tick, 'Lot No' AS Code, 'Lot No' AS Name " & _
                            " UNION ALL  " & _
                            " Select 'o' As Tick, 'Process' AS Code, 'Process' AS Name "

    Dim mHelpDimensionProcessQry$ = " Select 'o' As Tick,  '" & AgTemplate.ClsMain.FGetDimension1Caption() & "' AS Code, '" & AgTemplate.ClsMain.FGetDimension1Caption() & "' AS Name " & _
                        " UNION ALL  " & _
                        " Select 'o' As Tick,  '" & AgTemplate.ClsMain.FGetDimension2Caption() & "' AS Code, '" & AgTemplate.ClsMain.FGetDimension2Caption() & "' AS Name " & _
                        " UNION ALL  " & _
                        " Select 'o' As Tick, 'Process' AS Code, 'Process' AS Name "


    Dim mHelpUnitWithAmountQry$ = " Select 'o' As Tick,  'Qty' AS Code, 'Qty' AS Name " & _
                            " UNION ALL  " & _
                            " Select 'o' As Tick,  'Measure' AS Code, 'Measure' AS Name " & _
                            " UNION ALL  " & _
                            " Select 'o' As Tick, 'Amount' AS Code, 'Amount' AS Name "

    Dim mHelpUnitQry$ = " Select 'o' As Tick,  'Qty' AS Code, 'Qty' AS Name " & _
                        " UNION ALL  " & _
                        " Select 'o' As Tick,  'Measure' AS Code, 'Measure' AS Name "

#End Region

    Dim DsRep As DataSet = Nothing, DsRep1 As DataSet = Nothing, DsRep2 As DataSet = Nothing
    Dim mQry$ = "", RepName$ = "", RepTitle$ = ""

    Private Function FGetVoucher_TypeQry(ByVal TableName As String) As String
        'FGetVoucher_TypeQry = " SELECT Distinct 'o' As Tick, H.V_Type AS Code, Vt.Description AS [Voucher Type] " & _
        '                        " FROM " & TableName & " H  " & _
        '                        " LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type  Where 1 =1 " & VtypeRestriction & " "
        FGetVoucher_TypeQry = " SELECT Distinct 'o' As Tick, H.V_Type AS Code, Vt.Description AS [Voucher Type] " & _
                        " FROM " & TableName & " H  " & _
                        " LEFT JOIN Voucher_Type Vt ON H.V_Type = Vt.V_Type  Where 1 =1 "
    End Function

#Region "Initializing Grid"
    Public Sub Ini_Grid()
        Try
            Dim I As Integer = 0
            Select Case GRepFormName

                Case RequisitionReport
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubEndDate)
                    ReportFrm.CreateHelpGrid("Requisition By", "Requisition By", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpEmployeeQry, , , , 280)
                    ReportFrm.CreateHelpGrid("Department", "Department", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpDepartment)
                    ReportFrm.CreateHelpGrid("Item Group", "Item Group", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemGroupQry)
                    ReportFrm.CreateHelpGrid("Item Type", "Item Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemType)
                    ReportFrm.CreateHelpGrid("Item", "Item", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemQry, , 500, 650, 200)
                    ReportFrm.CreateHelpGrid(AgTemplate.ClsMain.FGetDimension1Caption(), AgTemplate.ClsMain.FGetDimension1Caption(), ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpDimension1)
                    ReportFrm.CreateHelpGrid(AgTemplate.ClsMain.FGetDimension2Caption(), AgTemplate.ClsMain.FGetDimension2Caption(), ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpDimension2)

                Case RequisitionStatus
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubEndDate)
                    ReportFrm.CreateHelpGrid("Status On", "Status On", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubLoginDate)
                    ReportFrm.CreateHelpGrid("Report Type", "Report Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, "Select 'Summary' as Code, 'Summary' as Name Union All Select 'Detail' as Code, 'Detail' as Name ", "Summary", , , , , False)
                    ReportFrm.CreateHelpGrid("Report For", "Report For", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, " SELECT 'All' AS Code, 'All' AS Name UNION ALL  SELECT 'Pending To Issue' AS Code, 'Pending To Issue' AS Name ", , , , , , False)
                    ReportFrm.CreateHelpGrid("Requisition By", "Requisition By", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpEmployeeQry, , , , 280)
                    ReportFrm.CreateHelpGrid("Department", "Department", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpDepartment)
                    ReportFrm.CreateHelpGrid("Requisition No", "Requisition No", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpRequisitionNo)
                    ReportFrm.CreateHelpGrid("Item Group", "Item Group", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemGroupQry)
                    ReportFrm.CreateHelpGrid("Item Type", "Item Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemType)
                    ReportFrm.CreateHelpGrid("Item", "Item", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemQry, , 500, 650, 200)
                    ReportFrm.CreateHelpGrid(AgTemplate.ClsMain.FGetDimension1Caption(), AgTemplate.ClsMain.FGetDimension1Caption(), ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpDimension1)
                    ReportFrm.CreateHelpGrid(AgTemplate.ClsMain.FGetDimension2Caption(), AgTemplate.ClsMain.FGetDimension2Caption(), ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpDimension2)

                Case ItemIssueReport
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubEndDate)
                    ReportFrm.CreateHelpGrid("Voucher Type", "Voucher Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, FGetVoucher_TypeQry("StockHead"), , , , 280)
                    ReportFrm.CreateHelpGrid("Unit", "Unit", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, "Select 'Qty' as Code, 'Qty' as Name Union All Select 'Measure' as Code, 'Measure' AS Name Union All Select 'Qty & Measure' as Code, 'Qty & Measure' AS Name ", "Qty")
                    ReportFrm.CreateHelpGrid("Party", "Party", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpPartyQry, , , 450, 320)
                    ReportFrm.CreateHelpGrid("From Godown", "From Godown", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpGodownQry)
                    ReportFrm.CreateHelpGrid("Item", "Item", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemQry, , 500, 650, 200)
                    ReportFrm.CreateHelpGrid("Item Group", "Item Group", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemGroupQry, , , 530)
                    ReportFrm.CreateHelpGrid("Item Category", "Item Category", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemCategoryQry, , , 420)
                    ReportFrm.CreateHelpGrid("Item Type", "Item Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemType)
                    ReportFrm.CreateHelpGrid("Item Reporting Group", "Item Reporting Group", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemReportingGroupQry, , , 500, 360)
                    ReportFrm.CreateHelpGrid("Lot No Wise", "Lot No Wise", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, " SELECT 'Yes' AS Code, 'Yes' AS Name UNION ALL  SELECT 'No' AS Code, 'No' AS Name ", "No", 200, 200, 130, , False)
                    ReportFrm.CreateHelpGrid("Reason", "Reason", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpReasonQry)
                    ReportFrm.CreateHelpGrid(AgTemplate.ClsMain.FGetDimension1Caption(), AgTemplate.ClsMain.FGetDimension1Caption(), ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpDimension1)
                    ReportFrm.CreateHelpGrid(AgTemplate.ClsMain.FGetDimension2Caption(), AgTemplate.ClsMain.FGetDimension2Caption(), ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpDimension2)

                Case MaterialIssueSummary
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", IIf(CDate(AgL.PubLoginDate) > CDate(AgL.PubEndDate), AgL.PubEndDate, AgL.PubLoginDate))
                    mQry = " Select 'Item Wise Summary' as Code, 'Item Wise Summary' as Name " & _
                             "Union All Select 'Date Wise Summary' as Code, 'Date Wise Summary' as Name " & _
                             "Union All Select 'Month Wise Summary' as Code, 'Month Wise Summary' as Name " & _
                             "Union All Select 'Party Wise Summary' as Code, 'Party Wise Summary' as Name  " & _
                             "Union All Select 'Item Category Wise Summary' as Code, 'Item Category Wise Summary' as Name "
                    ReportFrm.CreateHelpGrid("Report Type", "Report Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, mQry, "Item Wise Summary")
                    ReportFrm.CreateHelpGrid("Unit", "Unit", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpUnitWithAmountQry, "Qty", 250, 250, 120)
                    ReportFrm.CreateHelpGrid("Show", "Show", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpLotDimensionProcessQry, "None", 250, 250, 120)
                    ReportFrm.CreateHelpGrid("Voucher Type", "Voucher Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, FGetVoucher_TypeQry("StockHead"), , , , 280)
                    ReportFrm.CreateHelpGrid("Party", "Party", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpPartyQry, , , 450, 320)
                    ReportFrm.CreateHelpGrid("From Godown", "From Godown", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpGodownQry)
                    ReportFrm.CreateHelpGrid("Item", "Item", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemQry, , 500, 650, 200)
                    ReportFrm.CreateHelpGrid("Item Group", "Item Group", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemGroupQry, , , 530)
                    ReportFrm.CreateHelpGrid("Item Category", "Item Category", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemCategoryQry, , , 420)
                    ReportFrm.CreateHelpGrid("Item Type", "Item Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemType)
                    ReportFrm.CreateHelpGrid("Item Reporting Group", "Item Reporting Group", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemReportingGroupQry, , , 500, 360)
                    ReportFrm.CreateHelpGrid(AgTemplate.ClsMain.FGetDimension1Caption(), AgTemplate.ClsMain.FGetDimension1Caption(), ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpDimension1)
                    ReportFrm.CreateHelpGrid(AgTemplate.ClsMain.FGetDimension2Caption(), AgTemplate.ClsMain.FGetDimension2Caption(), ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpDimension2)


                Case ItemReceiveReport
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubEndDate)
                    ReportFrm.CreateHelpGrid("Voucher Type", "Voucher Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, FGetVoucher_TypeQry("StockHead"))
                    ReportFrm.CreateHelpGrid("Unit", "Unit", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, "Select 'Qty' as Code, 'Qty' as Name Union All Select 'Measure' as Code, 'Measure' AS Name Union All Select 'Qty & Measure' as Code, 'Qty & Measure' AS Name ", "Qty")
                    ReportFrm.CreateHelpGrid("Party", "Party", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpPartyQry, , , 450, 320)
                    ReportFrm.CreateHelpGrid("To Godown", "To Godown", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpGodownQry)
                    ReportFrm.CreateHelpGrid("Item", "Item", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemQry, , 500, 650, 200)
                    ReportFrm.CreateHelpGrid("Item Group", "Item Group", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemGroupQry, , , 530)
                    ReportFrm.CreateHelpGrid("Item Category", "Item Category", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemCategoryQry, , , 420)
                    ReportFrm.CreateHelpGrid("Item Type", "Item Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemType)
                    ReportFrm.CreateHelpGrid("Item Reporting Group", "Item Reporting Group", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemReportingGroupQry, , , 500, 360)
                    ReportFrm.CreateHelpGrid("Lot No Wise", "Lot No Wise", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, " SELECT 'Yes' AS Code, 'Yes' AS Name UNION ALL  SELECT 'No' AS Code, 'No' AS Name ", "No", 200, 200, 130, , False)
                    ReportFrm.CreateHelpGrid("Reason", "Reason", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpReasonQry)
                    ReportFrm.CreateHelpGrid(AgTemplate.ClsMain.FGetDimension1Caption(), AgTemplate.ClsMain.FGetDimension1Caption(), ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpDimension1)
                    ReportFrm.CreateHelpGrid(AgTemplate.ClsMain.FGetDimension2Caption(), AgTemplate.ClsMain.FGetDimension2Caption(), ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpDimension2)

                Case MaterialReceiveSummary
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", IIf(CDate(AgL.PubLoginDate) > CDate(AgL.PubEndDate), AgL.PubEndDate, AgL.PubLoginDate))
                    mQry = " Select 'Item Wise Summary' as Code, 'Item Wise Summary' as Name " & _
                             "Union All Select 'Date Wise Summary' as Code, 'Date Wise Summary' as Name " & _
                             "Union All Select 'Month Wise Summary' as Code, 'Month Wise Summary' as Name " & _
                             "Union All Select 'Party Wise Summary' as Code, 'Party Wise Summary' as Name  " & _
                             "Union All Select 'Item Category Wise Summary' as Code, 'Item Category Wise Summary' as Name "
                    ReportFrm.CreateHelpGrid("Report Type", "Report Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, mQry, "Item Wise Summary")
                    ReportFrm.CreateHelpGrid("Unit", "Unit", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpUnitWithAmountQry, "Qty", 250, 250, 120)
                    ReportFrm.CreateHelpGrid("Show", "Show", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpLotDimensionQry, "None", 250, 250, 120)
                    ReportFrm.CreateHelpGrid("Voucher Type", "Voucher Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, FGetVoucher_TypeQry("StockHead"), , , , 280)
                    ReportFrm.CreateHelpGrid("Party", "Party", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpPartyQry, , , 450, 320)
                    ReportFrm.CreateHelpGrid("To Godown", "To Godown", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpGodownQry)
                    ReportFrm.CreateHelpGrid("Item", "Item", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemQry, , 500, 650, 200)
                    ReportFrm.CreateHelpGrid("Item Group", "Item Group", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemGroupQry, , , 530)
                    ReportFrm.CreateHelpGrid("Item Category", "Item Category", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemCategoryQry, , , 420)
                    ReportFrm.CreateHelpGrid("Item Type", "Item Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemType)
                    ReportFrm.CreateHelpGrid("Item Reporting Group", "Item Reporting Group", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemReportingGroupQry, , , 500, 360)
                    ReportFrm.CreateHelpGrid(AgTemplate.ClsMain.FGetDimension1Caption(), AgTemplate.ClsMain.FGetDimension1Caption(), ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpDimension1)
                    ReportFrm.CreateHelpGrid(AgTemplate.ClsMain.FGetDimension2Caption(), AgTemplate.ClsMain.FGetDimension2Caption(), ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpDimension2)

                Case StockTransferSummary
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", IIf(CDate(AgL.PubLoginDate) > CDate(AgL.PubEndDate), AgL.PubEndDate, AgL.PubLoginDate))
                    mQry = " Select 'Item Wise Summary' as Code, 'Item Wise Summary' as Name " & _
                             "Union All Select 'Month Wise Summary' as Code, 'Month Wise Summary' as Name " & _
                             "Union All Select 'Party Wise Summary' as Code, 'Party Wise Summary' as Name  " & _
                             "Union All Select 'Item Category Wise Summary' as Code, 'Item Category Wise Summary' as Name "
                    ReportFrm.CreateHelpGrid("Report Type", "Report Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, mQry, "Item Wise Summary")
                    ReportFrm.CreateHelpGrid("Unit", "Unit", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpUnitQry, "Qty", 250, 250, 120)
                    ReportFrm.CreateHelpGrid("Voucher Type", "Voucher Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, FGetVoucher_TypeQry("StockHead"), , , , 280)
                    ReportFrm.CreateHelpGrid("Party", "Party", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpPartyQry, , , 450, 320)
                    ReportFrm.CreateHelpGrid("From Godown", "From Godown", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpGodownQry)
                    ReportFrm.CreateHelpGrid("To Godown", "To Godown", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpGodownQry)
                    ReportFrm.CreateHelpGrid("Item", "Item", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemQry, , 500, 650, 200)
                    ReportFrm.CreateHelpGrid("Item Group", "Item Group", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemGroupQry, , , 530)
                    ReportFrm.CreateHelpGrid("Item Category", "Item Category", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemCategoryQry, , , 420)
                    ReportFrm.CreateHelpGrid("Item Type", "Item Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemType)
                    ReportFrm.CreateHelpGrid("Item Reporting Group", "Item Reporting Group", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemReportingGroupQry, , , 500, 360)
                    ReportFrm.CreateHelpGrid(AgTemplate.ClsMain.FGetDimension1Caption(), AgTemplate.ClsMain.FGetDimension1Caption(), ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpDimension1)
                    ReportFrm.CreateHelpGrid(AgTemplate.ClsMain.FGetDimension2Caption(), AgTemplate.ClsMain.FGetDimension2Caption(), ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpDimension2)


                Case StockTransferReport
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubEndDate)
                    ReportFrm.CreateHelpGrid("Voucher Type", "Voucher Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, FGetVoucher_TypeQry("StockHead"))
                    ReportFrm.CreateHelpGrid("Unit", "Unit", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, "Select 'Qty' as Code, 'Qty' as Name Union All Select 'Measure' as Code, 'Measure' AS Name Union All Select 'Qty & Measure' as Code, 'Qty & Measure' AS Name ", "Qty")
                    ReportFrm.CreateHelpGrid("From Godown", "From Godown", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpGodownQry)
                    ReportFrm.CreateHelpGrid("To Godown", "To Godown", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpGodownQry)
                    ReportFrm.CreateHelpGrid("Item", "Item", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemQry, , 500, 650, 200)
                    ReportFrm.CreateHelpGrid("Item Group", "Item Group", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemGroupQry, , , 530)
                    ReportFrm.CreateHelpGrid("Item Category", "Item Category", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemCategoryQry, , , 420)
                    ReportFrm.CreateHelpGrid("Item Type", "Item Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemType)
                    ReportFrm.CreateHelpGrid("Item Reporting Group", "Item Reporting Group", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemReportingGroupQry, , , 500, 360)
                    ReportFrm.CreateHelpGrid(AgTemplate.ClsMain.FGetDimension1Caption(), AgTemplate.ClsMain.FGetDimension1Caption(), ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpDimension1)
                    ReportFrm.CreateHelpGrid(AgTemplate.ClsMain.FGetDimension2Caption(), AgTemplate.ClsMain.FGetDimension2Caption(), ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpDimension2)

                Case PhysicalStockReport
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubEndDate)
                    mQry = " Select 'Item Wise Detail' as Code, 'Item Wise Detail' as Name " & _
                            "Union All Select 'Process Wise Summary' as Code, 'Process Wise Summary' as Name " & _
                            "Union All Select 'Construction Wise Summary' as Code, 'Construction Wise Summary' as Name " & _
                            "Union All Select 'Category Wise Summary' as Code, 'Category Wise Summary' as Name "
                    ReportFrm.CreateHelpGrid("Report Type", "Report Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, mQry, "Item Wise Detail")
                    ReportFrm.CreateHelpGrid("Voucher Type", "Voucher Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, FGetVoucher_TypeQry("StockHead"))
                    ReportFrm.CreateHelpGrid("Unit", "Unit", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, "Select 'Qty' as Code, 'Qty' as Name Union All Select 'Measure' as Code, 'Measure' AS Name Union All Select 'Qty & Measure' as Code, 'Qty & Measure' AS Name ", "Qty")
                    ReportFrm.CreateHelpGrid("Godown", "Godown", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpGodownQry)
                    ReportFrm.CreateHelpGrid("Item", "Item", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemQry, , 500, 650, 200)
                    ReportFrm.CreateHelpGrid("Item Group", "Item Group", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemGroupQry, , , 530)
                    ReportFrm.CreateHelpGrid("Item Category", "Item Category", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemCategoryQry, , , 420)
                    ReportFrm.CreateHelpGrid("Item Type", "Item Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemType)
                    ReportFrm.CreateHelpGrid("Item Reporting Group", "Item Reporting Group", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemReportingGroupQry, , , 500, 360)
                    ReportFrm.CreateHelpGrid("Item Division", "Item Division", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemDivsionQry)
                    ReportFrm.CreateHelpGrid(AgTemplate.ClsMain.FGetDimension1Caption(), AgTemplate.ClsMain.FGetDimension1Caption(), ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpDimension1)
                    ReportFrm.CreateHelpGrid(AgTemplate.ClsMain.FGetDimension2Caption(), AgTemplate.ClsMain.FGetDimension2Caption(), ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpDimension2)
                    ReportFrm.CreateHelpGrid("Process", "Process", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpProcessQry, "All", 500, 550, 250, )

                Case StockInHand
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", IIf(CDate(AgL.PubLoginDate) > CDate(AgL.PubEndDate), AgL.PubEndDate, AgL.PubLoginDate))
                    ReportFrm.CreateHelpGrid("Report Type", "Report Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, "Select 'Stock Summary' as Code, 'Stock Summary' as Name Union All Select 'Stock Ledger' as Code, 'Stock Ledger' as Name  Union All Select 'Stock Summary (Voucher Type Wise)' as Code, 'Stock Summary (Voucher Type Wise)' as Name ", "Stock Summary", , , 300, , False)
                    ReportFrm.CreateHelpGrid("Report On", "Report On", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, " SELECT 'Qty' AS Code, 'Qty' AS Name UNION ALL  SELECT 'Measure' AS Code, 'Measure' AS Name ", "Qty", , , , , False)
                    ReportFrm.CreateHelpGrid("Group On", "Group On", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, " SELECT 'None' AS Code, 'None' AS Name UNION ALL  SELECT 'Godown' AS Code, 'Godown' AS Name UNION ALL  SELECT 'Godown and Process' AS Code, 'Godown and Process' AS Name ", "Godown", , , , , False)
                    ReportFrm.CreateHelpGrid("Show", "Show", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpLotDimensionQry, "None", 250, 250, 120)
                    mQry = " SELECT 'All' AS Code, 'All' AS Name " & _
                            " UNION ALL " & _
                            " SELECT 'Not Zero' AS Code, 'Not Zero' AS Name " & _
                            " UNION ALL   " & _
                            " SELECT 'Greater Than Zero' AS Code, 'Greater Than Zero' AS Name " & _
                            " UNION ALL  " & _
                            " SELECT 'Less Than Zero' AS Code, 'Less Than Zero' AS Name " & _
                            " UNION ALL  " & _
                            " SELECT 'Period Negative' AS Code, 'Period Negative' AS Name " & _
                            " UNION ALL  " & _
                            " SELECT 'Zero' AS Code, 'Zero' AS Name "
                    ReportFrm.CreateHelpGrid("Show Balances", "Show Balances", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, mQry, "All")
                    ReportFrm.CreateHelpGrid("Godown", "Godown", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpGodownQry)
                    ReportFrm.CreateHelpGrid("Item", "Item", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemQry, , 500, 650, 200)
                    ReportFrm.CreateHelpGrid("Item Group", "Item Group", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemGroupQry, , , 530)
                    ReportFrm.CreateHelpGrid("Item Category", "Item Category", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemCategoryQry, , , 420)
                    ReportFrm.CreateHelpGrid("Item Type", "Item Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemType)
                    ReportFrm.CreateHelpGrid("Item Division", "Item Division", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemDivsionQry)
                    ReportFrm.CreateHelpGrid("Process", "Process", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpProcessQry, "All", 500, 550, 250, )
                    ReportFrm.CreateHelpGrid("Lot No", "Lot No", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpLotQry, "All")
                    ReportFrm.CreateHelpGrid(AgTemplate.ClsMain.FGetDimension1Caption(), AgTemplate.ClsMain.FGetDimension1Caption(), ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpDimension1)
                    ReportFrm.CreateHelpGrid(AgTemplate.ClsMain.FGetDimension2Caption(), AgTemplate.ClsMain.FGetDimension2Caption(), ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpDimension2)
                    ReportFrm.CreateHelpGrid("Exclude", "Exclude", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpDimensionProcessQry, "None", 250, 250, 120)
                    ReportFrm.CreateHelpGrid("Item Reporting Group", "Item Reporting Group", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemReportingGroupQry, , , 500, 360)

                Case StockBalance, StockBalanceWithAverageRate
                    ReportFrm.CreateHelpGrid("AsOnDate", "As On Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", IIf(CDate(AgL.PubLoginDate) > CDate(AgL.PubEndDate), AgL.PubEndDate, AgL.PubLoginDate))
                    ReportFrm.CreateHelpGrid("Report On", "Report On", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, " SELECT 'Qty' AS Code, 'Qty' AS Name UNION ALL  SELECT 'Measure' AS Code, 'Measure' AS Name ", "Qty", , , , , False)
                    ReportFrm.CreateHelpGrid("Group On", "Group On", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, " SELECT 'None' AS Code, 'None' AS Name UNION ALL  SELECT 'Process' AS Code, 'Process' AS Name ", "None", , , , , False)
                    ReportFrm.CreateHelpGrid("Show", "Show", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpLotDimensionQry, "None", 250, 250, 120)
                    mQry = " SELECT 'All' AS Code, 'All' AS Name " & _
                            " UNION ALL   " & _
                            " SELECT 'Greater Than Zero' AS Code, 'Greater Than Zero' AS Name " & _
                            " UNION ALL  " & _
                            " SELECT 'Less Than Zero' AS Code, 'Less Than Zero' AS Name "
                    ReportFrm.CreateHelpGrid("Show Balances", "Show Balances", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, mQry, "All")
                    ReportFrm.CreateHelpGrid("Godown", "Godown", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpGodownQry)
                    ReportFrm.CreateHelpGrid("Item", "Item", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemQry, , 500, 650, 200)
                    ReportFrm.CreateHelpGrid("Item Group", "Item Group", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemGroupQry, , , 530)
                    ReportFrm.CreateHelpGrid("Item Category", "Item Category", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemCategoryQry, , , 420)
                    ReportFrm.CreateHelpGrid("Item Type", "Item Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemType)
                    ReportFrm.CreateHelpGrid("Item Division", "Item Division", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemDivsionQry)
                    ReportFrm.CreateHelpGrid("Process", "Process", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpProcessQry, "All", 500, 550, 250, )
                    ReportFrm.CreateHelpGrid("Lot No", "Lot No", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpLotQry, "All")
                    ReportFrm.CreateHelpGrid(AgTemplate.ClsMain.FGetDimension1Caption(), AgTemplate.ClsMain.FGetDimension1Caption(), ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpDimension1)
                    ReportFrm.CreateHelpGrid(AgTemplate.ClsMain.FGetDimension2Caption(), AgTemplate.ClsMain.FGetDimension2Caption(), ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpDimension2)
                    ReportFrm.CreateHelpGrid("Exclude", "Exclude", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpDimensionProcessQry, "None", 250, 250, 120)
                    ReportFrm.CreateHelpGrid("Item Reporting Group", "Item Reporting Group", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemReportingGroupQry, , , 500, 360)

                Case StockBalanceValuation
                    ReportFrm.CreateHelpGrid("AsOnDate", "As On Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", IIf(CDate(AgL.PubLoginDate) > CDate(AgL.PubEndDate), AgL.PubEndDate, AgL.PubLoginDate))
                    ReportFrm.CreateHelpGrid("Report Type", "Report Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, "Select 'Summary' as Code, 'Summary' as Name Union All Select 'Detail' as Code, 'Detail' as Name", "Summary", , , , , False)
                    ReportFrm.CreateHelpGrid("Godown", "Godown", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpGodownQry)
                    ReportFrm.CreateHelpGrid("Item", "Item", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemQry, , 500, 650, 200)
                    ReportFrm.CreateHelpGrid("Item Group", "Item Group", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemGroupQry, , , 530)
                    ReportFrm.CreateHelpGrid("Item Category", "Item Category", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemCategoryQry, , , 420)
                    ReportFrm.CreateHelpGrid("Item Type", "Item Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemType)
                    ReportFrm.CreateHelpGrid("Item Division", "Item Division", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemDivsionQry)


                Case StockInProcess
                    ReportFrm.CreateHelpGrid("FromDate", "From Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", AgL.PubStartDate)
                    ReportFrm.CreateHelpGrid("ToDate", "To Date", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.DateType, "", IIf(CDate(AgL.PubLoginDate) > CDate(AgL.PubEndDate), AgL.PubEndDate, AgL.PubLoginDate))
                    ReportFrm.CreateHelpGrid("Report Type", "Report Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, "Select 'Summary' as Code, 'Summary' as Name Union All Select 'Detail' as Code, 'Detail' as Name Union All Select 'Voucher Type Wise Summary' as Code, 'Voucher Type Wise Summary' as Name ", "Summary", , , 300, , False)
                    ReportFrm.CreateHelpGrid("Report On", "Report On", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, " SELECT 'Qty' AS Code, 'Qty' AS Name UNION ALL  SELECT 'Measure' AS Code, 'Measure' AS Name ", "Qty", , , , , False)
                    ReportFrm.CreateHelpGrid("Group On", "Group On", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, " SELECT 'None' AS Code, 'None' AS Name UNION ALL  SELECT 'Process' AS Code, 'Process' AS Name UNION ALL  SELECT 'Process and Person' AS Code, 'Process and Person' AS Name ", "Process", , , , , False)
                    ReportFrm.CreateHelpGrid("Show", "Show", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpLotDimensionQry, "None", 250, 250, 120)
                    mQry = " SELECT 'All' AS Code, 'All' AS Name " & _
                            " UNION ALL " & _
                            " SELECT 'Not Zero' AS Code, 'Not Zero' AS Name " & _
                            " UNION ALL   " & _
                            " SELECT 'Greater Than Zero' AS Code, 'Greater Than Zero' AS Name " & _
                            " UNION ALL  " & _
                            " SELECT 'Less Than Zero' AS Code, 'Less Than Zero' AS Name " & _
                            " UNION ALL  " & _
                            " SELECT 'Zero' AS Code, 'Zero' AS Name "
                    ReportFrm.CreateHelpGrid("Show Balances", "Show Balances", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.SingleSelection, mQry, "All", , , , , False)
                    ReportFrm.CreateHelpGrid("Process", "Process", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpProcessQry, "All", , , , , False)
                    ReportFrm.CreateHelpGrid("JobWorker", "Job Worker", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpJobWorkerQry, , 550, 600, 400)
                    ReportFrm.CreateHelpGrid("Item", "Item", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemQry, , 500, 650, 200)
                    ReportFrm.CreateHelpGrid("Item Group", "Item Group", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemGroupQry, , , 530)
                    ReportFrm.CreateHelpGrid("Item Category", "Item Category", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemCategoryQry, , , 420)
                    ReportFrm.CreateHelpGrid("Item Type", "Item Type", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemType)
                    ReportFrm.CreateHelpGrid("Item Division", "Item Division", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemDivsionQry)
                    ReportFrm.CreateHelpGrid(AgTemplate.ClsMain.FGetDimension1Caption(), AgTemplate.ClsMain.FGetDimension1Caption(), ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpDimension1)
                    ReportFrm.CreateHelpGrid(AgTemplate.ClsMain.FGetDimension2Caption(), AgTemplate.ClsMain.FGetDimension2Caption(), ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpDimension2)
                    ReportFrm.CreateHelpGrid("Lot No", "Lot No", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpLotQry, "All")
                    ReportFrm.CreateHelpGrid("Division", "Division", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemDivsionQry, AgL.PubDivName & "|" & AgL.PubDivCode)
                    ReportFrm.CreateHelpGrid("Item Reporting Group", "Item Reporting Group", ReportLayout.FrmReportLayout.FieldFilterDataType.StringType, ReportLayout.FrmReportLayout.FieldDataType.MultiSelection, mHelpItemReportingGroupQry, , , 500, 360)

            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
#End Region


    Private Sub ObjRepFormGlobal_ProcessReport() Handles ReportFrm.ProcessReport
        Select Case mGRepFormName
            Case RequisitionReport
                ProcRequisitionReport()

            Case RequisitionStatus
                ProcRequisitionStatusReport()

            Case ItemIssueReport
                ProcItemIssueReport()

            Case ItemReceiveReport
                ProcItemReceiveReport()

            Case StockTransferReport
                ProcStockTransferReport()

            Case PhysicalStockReport
                ProcPhysicalStockReport()

            Case StockInHand
                ProcStockInHand()

            Case StockBalance
                ProcStockBalance()

            Case StockInProcess
                ProcStockInProcess()

            Case MaterialIssueSummary
                ProcMaterialIssueSummary()

            Case MaterialReceiveSummary
                ProcMaterialReceiveSummary()

            Case StockTransferSummary
                ProcStockTransferSummary()

            Case StockBalanceValuation
                ProcStockBalanceValuation()

            Case StockBalanceWithAverageRate
                ProcStockBalanceWithAverageRate()

        End Select
    End Sub

#Region "Requisition Report"
    Private Sub ProcRequisitionReport()
        Dim mCondStr$ = ""
        RepName = "Store_RequisitionReport"
        RepTitle = "Requisition Report"

        Try
            mCondStr = mCondStr & " And H.V_Date Between " & AgL.Chk_Text(ReportFrm.FGetText(0)) & " And " & AgL.Chk_Text(ReportFrm.FGetText(1)) & " "
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.RequisitionBy", 2)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.Department", 3)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemGroup", 4)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemType ", 5)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Item", 6)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Dimension1", 7)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Dimension2", 8)
            mCondStr = mCondStr & " And H.Site_Code = '" & AgL.PubSiteCode & "' "
            mCondStr = mCondStr & " And H.Div_Code= '" & AgL.PubDivCode & "' "

            'mCondStr = mCondStr & VtypeRestriction

            mQry = " SELECT H.DocID, H.V_Date, H.ReferenceNo, H.Remarks, D.Description AS DepartmentName, " & _
                    " E.Caption_Dimension1, E.Caption_Dimension2, D1.Description AS D1Desc,D2.Description AS D2Desc," & _
                    " SG.DispName AS ReqByName, L.Sr, L.Qty, L.Unit, L.RequireDate, L.Remark AS LineRemark , I.Description AS  ItemDesc  " & _
                    " FROM Requisition H " & _
                    " LEFT JOIN RequisitionDetail L ON L.DocId = H.DocID  " & _
                    " LEFT JOIN Department D ON D.Code = H.Department  " & _
                    " LEFT JOIN SubGroup SG ON SG.SubCode = H.RequisitionBy  " & _
                    " LEFT JOIN Item I ON I.Code = L.Item " & _
                    " LEFT JOIN Enviro E ON E.Site_Code = H.Site_Code AND E.Div_Code = H.Div_Code " & _
                    " LEFT JOIN Dimension1 D1 ON D1.Code = L.Dimension1  " & _
                    " LEFT JOIN Dimension2 D2 ON D2.Code = L.Dimension2 " & _
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

#Region "Requisition Status Report"
    Private Sub ProcRequisitionStatusReport()
        Dim mCondStr$ = ""

        Dim mQryReqIssue$ = " SELECT S.Requisition , S.RequisitionSr , SH.V_Date AS RecDate, SH.ManualRefNo AS RecNo , S.Qty AS RecQty " & _
                    " FROM StockHeadDetail S " & _
                    " LEFT JOIN StockHead SH ON SH.DocID = S.DocID " & _
                    " WHERE isnull(S.Requisition,'') <>'' " & _
                    " AND SH.V_Date <= '" & ReportFrm.FGetText(2) & "' "

        Dim mQryReqIssueSummary$ = " SELECT S.Requisition , S.RequisitionSr , sum(S.Qty) AS RecQty FROM StockHeadDetail S " & _
                    " LEFT JOIN StockHead SH ON SH.DocID = S.DocID " & _
                    " WHERE isnull(S.Requisition,'') <> ''  " & _
                    " AND SH.V_Date <= '" & ReportFrm.FGetText(2) & "' " & _
                    " GROUP BY S.Requisition , S.RequisitionSr "

        Dim mQryPurchIndent$ = " SELECT L.Requisition, L.RequisitionSr, sum(L.Qty) AS IndQty, max(H.ManualRefNo) AS IndentNo , max(H.V_Date) AS IndentDate " & _
                    " FROM PurchIndentReq L " & _
                    " LEFT JOIN PurchIndent H ON H.DocID = L.DocId  " & _
                    " GROUP BY L.Requisition, L.RequisitionSr "
        Try

            If ReportFrm.FGetText(3) = "Detail" Then
                RepName = "Store_RequisitionStatusReportDetail"
                RepTitle = "Requisition Status Report"
            Else
                RepName = "Store_RequisitionStatusReport"
                RepTitle = "Requisition Status Report"
            End If

            'mCondStr = mCondStr & VtypeRestriction

            mCondStr = mCondStr & " And H.V_Date Between " & AgL.Chk_Text(ReportFrm.FGetText(0)) & " And " & AgL.Chk_Text(ReportFrm.FGetText(1)) & " "

            If ReportFrm.FGetText(4) = "Pending To Issue" Then
                mCondStr = mCondStr & " AND L.ApproveQty - isnull(VIS.RecQty,0) > 0 "
            End If

            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.RequisitionBy", 5)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.Department", 6)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.DocId", 7)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemGroup", 8)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemType ", 9)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Item", 10)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Dimension1", 11)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Dimension2", 12)

            mCondStr = mCondStr & " And H.Site_Code = '" & AgL.PubSiteCode & "' "
            mCondStr = mCondStr & " And H.Div_Code= '" & AgL.PubDivCode & "' "


            mQry = " SELECT H.DocID, H.V_Date, H.ReferenceNo, H.Remarks, D.Description AS DepartmentName, " & _
                    " SG.DispName AS ReqByName, L.Sr, L.Qty, L.Unit, L.RequireDate, L.Remark AS LineRemark , L.ApproveQty, I.Description AS  ItemDesc , " & _
                    " E.Caption_Dimension1, E.Caption_Dimension2, D1.Description AS D1Desc,D2.Description AS D2Desc," & _
                    " VI.RecNo, VI.RecDate, isnull(VI.RecQty,0) AS RecQty, L.ApproveQty - isnull(VIS.RecQty,0) AS TotalBalQty, VPI.IndentNo, VPI.IndentDate, VPI.IndQty " & _
                    " FROM Requisition H " & _
                    " LEFT JOIN RequisitionDetail L ON L.DocId = H.DocID  " & _
                    " LEFT JOIN Department D ON D.Code = H.Department  " & _
                    " LEFT JOIN SubGroup SG ON SG.SubCode = H.RequisitionBy  " & _
                    " LEFT JOIN Item I ON I.Code = L.Item " & _
                    " LEFT JOIN Enviro E ON E.Site_Code = H.Site_Code AND E.Div_Code = H.Div_Code " & _
                    " LEFT JOIN Dimension1 D1 ON D1.Code = L.Dimension1  " & _
                    " LEFT JOIN Dimension2 D2 ON D2.Code = L.Dimension2 " & _
                    " LEFT JOIN ( " & mQryReqIssue & " ) VI ON VI.Requisition = L.DocId AND VI.RequisitionSr = L.Sr " & _
                    " LEFT JOIN ( " & mQryReqIssueSummary & " ) VIS ON VIS.Requisition = L.DocId AND VIS.RequisitionSr = L.Sr " & _
                    " LEFT JOIN ( " & mQryPurchIndent & " ) VPI ON VPI.Requisition = L.DocId AND VPI.RequisitionSr = L.Sr " & _
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

#Region "Item Issue Report"
    Private Sub ProcItemIssueReport()
        Dim mCondStr$ = ""
        RepTitle = "Item Issue Report"

        Try
            mCondStr = mCondStr & " And H.V_Date Between " & AgL.Chk_Text(ReportFrm.FGetText(0)) & " And " & AgL.Chk_Text(ReportFrm.FGetText(1)) & " "
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.V_Type", 2)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.SubCode ", 4)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.FromGodown", 5)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Item", 6)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemGroup", 7)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemCategory ", 8)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemType ", 9)

            ' mCondStr = mCondStr & " AND Vt.NCat = '" & AgTemplate.ClsMain.Temp_NCat.StoreIssue & "' "

            If ReportFrm.FGetText(10) <> "" And ReportFrm.FGetText(10) <> "All" Then
                mQry = " Select '''' +  replace(ItemList,',',''',''')  + ''''  From ItemReportingGroup Where 1=1 " & ReportFrm.GetWhereCondition("Code", 10)
                mCondStr = mCondStr & " AND I.Code In (" & AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar & ")"
            End If

            ' mCondStr = mCondStr & VtypeRestriction

            mCondStr = mCondStr & " And H.Site_Code = '" & AgL.PubSiteCode & "' "
            mCondStr = mCondStr & " And H.Div_Code= '" & AgL.PubDivCode & "' "

            If ReportFrm.FGetText(3) = "Measure" Then
                If AgL.StrCmp(ReportFrm.FGetText(11), "Yes") Then
                    RepName = "Store_ItemIssueReport_Measure_LotWise"
                Else
                    RepName = "Store_ItemIssueReport_Measure"
                End If
            ElseIf ReportFrm.FGetText(3) = "Qty & Measure" Then
                If AgL.StrCmp(ReportFrm.FGetText(11), "Yes") Then
                    RepName = "Store_ItemIssueReport_QtyMeasure_LotWise"
                Else
                    RepName = "Store_ItemIssueReport_QtyMeasure"
                End If
            Else
                If AgL.StrCmp(ReportFrm.FGetText(11), "Yes") Then
                    RepName = "Store_ItemIssueReport_LotWise"
                Else
                    RepName = "Store_ItemIssueReport"
                End If
            End If

            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.Reason", 12)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Dimension1", 13)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Dimension2", 14)

            mQry = " SELECT H.DocID, H.V_Type, H.V_Date, H.ManualRefNo, Sg.DispName + (Case When IsNull(Sg.CityCode,'')<>'' Then ', ' + C.CityName Else '' End) as PartyName, H.Remarks + L.Remarks AS Remarks, " & _
                    " E.Caption_Dimension1, E.Caption_Dimension2, D1.Description AS D1Desc,D2.Description AS D2Desc," & _
                    " P.Description as ProcessDesc, H.VoucherTypeDesc, I.Description AS ItemDesc, L.Unit, L.LotNo, U.DecimalPlaces, UM.DecimalPlaces AS MeasureDecimalPlace, L.Qty, L.TotalMeasure, L.MeasureUnit, H.Remarks AS HeaderRemark, L.Remarks AS LineRemark  " & _
                    " FROM ( SELECT SH.*, Vt.Description as VoucherTypeDesc FROM StockHead SH WITH (nolock) LEFT JOIN Voucher_Type Vt WITH (nolock) On Vt.V_Type = SH.V_Type WHERE Vt.NCat = '" & AgTemplate.ClsMain.Temp_NCat.StoreIssue & "' ) H " & _
                    " LEFT JOIN StockHeadDetail L WITH (nolock) ON L.DocID = H.DocID  " & _
                    " Left Join Subgroup Sg WITH (nolock) on H.SubCode = Sg.SubCode " & _
                    " Left Join City C WITH (nolock) on Sg.CityCode = C.CityCode  " & _
                    " Left Join Process P WITH (nolock) on H.Process = P.NCat " & _
                    " LEFT JOIN Item I WITH (nolock) ON I.Code = L.Item  " & _
                    " LEFT JOIN Unit U on U.Code = L.Unit " & _
                    " LEFT JOIN Unit UM on UM.Code = L.MeasureUnit " & _
                    " LEFT JOIN Enviro E ON E.Site_Code = H.Site_Code AND E.Div_Code = H.Div_Code " & _
                    " LEFT JOIN Dimension1 D1 ON D1.Code = L.Dimension1  " & _
                    " LEFT JOIN Dimension2 D2 ON D2.Code = L.Dimension2 " & _
                    " WHERE 1=1 " & mCondStr & " Order By H.V_Date  "

            DsRep = AgL.FillData(mQry, AgL.GCn)

            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records to Print!")

            ReportFrm.PrintReport(DsRep, RepName, RepTitle, ReportPath)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub
#End Region

#Region "Material Issue Summary"
    Private Sub ProcMaterialIssueSummary()
        RepName = "Store_MaterialIssue_Summary"

        Dim mCondStr$ = ""
        Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"
        Dim mShowForValue1$ = "''", mShowForValue2$ = "''", mShowForValue3$ = "''", mShowForValue4$ = "''"
        Dim mShowForHead1$ = "''", mShowForHead2$ = "''", mShowForHead3$ = "''", mShowForHead4$ = "''"

        Dim IsUnitQty As Integer = 0
        Dim IsUnitMeasure As Integer = 0
        Dim IsUnitAmount As Integer = 0

        Dim IsGroupOnProcess As Integer = 0
        Dim IsGroupOnDimension1 As Integer = 0
        Dim IsGroupOnDimension2 As Integer = 0
        Dim IsGroupOnLotNo As Integer = 0

        Dim Unit1Head = "''", Unit1 As String = "''", Unit1DecimalPlace As String = "''", IssueUnit1 As String = "''"
        Dim Unit2Head = "''", Unit2 As String = "''", Unit2DecimalPlace As String = "''", IssueUnit2 As String = "''"
        Dim Unit3Head = "''", Unit3 As String = "''", Unit3DecimalPlace As String = "''", IssueUnit3 As String = "''"

        Try
            If ReportFrm.FGetText(3) IsNot Nothing Then
                If ReportFrm.FGetText(3).ToString.Contains("Qty") = True Then IsUnitQty = 1
                If ReportFrm.FGetText(3).ToString.Contains("Measure") = True Then IsUnitMeasure = 1
                If ReportFrm.FGetText(3).ToString.Contains("Amount") = True Then IsUnitAmount = 1
            End If

            If IsUnitQty + IsUnitMeasure + IsUnitAmount = 3 Then
                RepName = RepName + "_With3Unit"
                Unit1 = "I.Unit" : Unit1Head = "'Qty'" : Unit1DecimalPlace = "U.DecimalPlaces" : IssueUnit1 = "L.Qty"
                Unit2 = "I.MeasureUnit" : Unit2Head = "'Measure'" : Unit2DecimalPlace = "UM.DecimalPlaces" : IssueUnit2 = "L.TotalMeasure"
                Unit3 = "E.DefaultCurrency" : Unit3Head = "'Currency'" : Unit3DecimalPlace = "2" : IssueUnit3 = "L.Amount"
            ElseIf IsUnitQty + IsUnitMeasure + IsUnitAmount = 2 Then
                RepName = RepName + "_With2Unit"
                If IsUnitQty = 1 And IsUnitMeasure = 1 Then
                    Unit1 = "I.Unit" : Unit1Head = "'Qty'" : Unit1DecimalPlace = "U.DecimalPlaces" : IssueUnit1 = "L.Qty"
                    Unit2 = "I.MeasureUnit" : Unit2Head = "'Measure'" : Unit2DecimalPlace = "UM.DecimalPlaces" : IssueUnit2 = "L.TotalMeasure"
                ElseIf IsUnitQty = 1 And IsUnitAmount = 1 Then
                    Unit1 = "I.Unit" : Unit1Head = "'Qty'" : Unit1DecimalPlace = "U.DecimalPlaces" : IssueUnit1 = "L.Qty"
                    Unit2 = "E.DefaultCurrency" : Unit2Head = "'Currency'" : Unit2DecimalPlace = "2" : IssueUnit2 = "L.Amount"
                ElseIf IsUnitMeasure = 1 And IsUnitAmount = 1 Then
                    Unit1 = "I.MeasureUnit" : Unit1Head = "'Measure'" : Unit1DecimalPlace = "UM.DecimalPlaces" : IssueUnit1 = "L.TotalMeasure"
                    Unit2 = "E.DefaultCurrency" : Unit2Head = "'Currency'" : Unit2DecimalPlace = "2" : IssueUnit2 = "L.Amount"
                End If
            ElseIf IsUnitQty + IsUnitMeasure + IsUnitAmount = 1 Then
                RepName = RepName + "_With1Unit"
                If IsUnitQty = 1 Then Unit1 = "I.Unit" : Unit1Head = "'Qty'" : Unit1DecimalPlace = "U.DecimalPlaces" : IssueUnit1 = "L.Qty"
                If IsUnitMeasure = 1 Then Unit1 = "I.MeasureUnit" : Unit1Head = "'Measure'" : Unit1DecimalPlace = "UM.DecimalPlaces" : IssueUnit1 = "L.TotalMeasure"
                If IsUnitAmount = 1 Then Unit1 = "E.DefaultCurrency" : Unit1Head = "'Currency'" : Unit1DecimalPlace = "2" : IssueUnit1 = "L.Amount"
            End If

            If ReportFrm.FGetText(2) = "Item Wise Summary" Then
                strGrpFld = "I.Description"
                strGrpFldDesc = "I.Description"
                strGrpFldHead = "'Item'"
                RepTitle = "Materail Issue Summary (Item Wise)"
            ElseIf ReportFrm.FGetText(2) = "Party Wise Summary" Then
                RepTitle = "Materail Issue Summary (Party Wise)"
                strGrpFld = "SG.Name"
                strGrpFldDesc = "SG.Name + ',' + IsNull(C.CityName,'')"
                strGrpFldHead = "'Party Name'"
            ElseIf ReportFrm.FGetText(2) = "Date Wise Summary" Then
                RepTitle = "Materail Issue Summary (Date Wise)"
                strGrpFld = "H.V_Date"
                strGrpFldDesc = "replace( convert(NVARCHAR,H.V_Date,106),' ','/')"
                strGrpFldHead = "'Date'"
            ElseIf ReportFrm.FGetText(2) = "Month Wise Summary" Then
                RepTitle = "Materail Issue Summary (Month Wise)"
                strGrpFld = "Substring(convert(nvarchar,H.V_Date,11),0,6)"
                strGrpFldDesc = "Replace(SubString(Convert(VARCHAR,H.v_Date,6),4,6),' ','-')"
                strGrpFldHead = "'Month'"
            ElseIf ReportFrm.FGetText(2) = "Item Category Wise Summary" Then
                strGrpFld = "IC.Description"
                strGrpFldDesc = "IC.Description"
                strGrpFldHead = "'Item Category'"
                RepTitle = "Materail Issue Summary (Item Category Wise)"
            End If

            If ReportFrm.FGetCode(4) IsNot Nothing Then
                If ReportFrm.FGetCode(4).ToString.Contains("Process") = True Then IsGroupOnProcess = 1
                If ReportFrm.FGetCode(4).ToString.Contains(AgTemplate.ClsMain.FGetDimension1Caption) = True Then IsGroupOnDimension1 = 1
                If ReportFrm.FGetCode(4).ToString.Contains(AgTemplate.ClsMain.FGetDimension2Caption) = True Then IsGroupOnDimension2 = 1
                If ReportFrm.FGetCode(4).ToString.Contains("Lot No") = True Then IsGroupOnLotNo = 1
            End If

            If IsGroupOnProcess + IsGroupOnDimension1 + IsGroupOnDimension2 + IsGroupOnLotNo = 4 Then
                RepName = RepName + "_With4Dimensions"
                mShowForValue1 = "P.Description"
                mShowForValue2 = "D1.Description"
                mShowForValue3 = "D2.Description"
                mShowForValue4 = "L.LotNo"
                mShowForHead1 = "'Process'"
                mShowForHead2 = "'" & AgTemplate.ClsMain.FGetDimension1Caption() & "'"
                mShowForHead3 = "'" & AgTemplate.ClsMain.FGetDimension2Caption() & "'"
                mShowForHead4 = "'Lot No'"
            ElseIf IsGroupOnProcess + IsGroupOnDimension1 + IsGroupOnDimension2 + IsGroupOnLotNo = 3 Then
                RepName = RepName + "_With3Dimensions"
                mShowForValue1 = "D1.Description"
                mShowForValue2 = "D2.Description"
                mShowForValue3 = "L.LotNo"
                mShowForHead1 = "'" & AgTemplate.ClsMain.FGetDimension1Caption() & "'"
                mShowForHead2 = "'" & AgTemplate.ClsMain.FGetDimension2Caption() & "'"
                mShowForHead3 = "'Lot No'"
            ElseIf IsGroupOnProcess + IsGroupOnDimension1 + IsGroupOnDimension2 + IsGroupOnLotNo = 2 Then
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
                ElseIf IsGroupOnProcess = 1 And IsGroupOnLotNo = 1 Then
                    mShowForValue1 = "P.Descriptionn" : mShowForHead1 = "'Process'"
                    mShowForValue2 = "L.LotNo" : mShowForHead2 = "'Lot No'"
                ElseIf IsGroupOnProcess = 1 And IsGroupOnDimension1 = 1 Then
                    mShowForValue1 = "P.Descriptionn" : mShowForHead1 = "'Process'"
                    mShowForValue2 = "D1.Description" : mShowForHead2 = "'" & AgTemplate.ClsMain.FGetDimension1Caption() & "'"
                ElseIf IsGroupOnProcess = 1 And IsGroupOnDimension2 = 1 Then
                    mShowForValue1 = "P.Descriptionn" : mShowForHead1 = "'Process'"
                    mShowForValue2 = "D2.Description" : mShowForHead2 = "'" & AgTemplate.ClsMain.FGetDimension2Caption() & "'"
                End If
            ElseIf IsGroupOnProcess + IsGroupOnDimension1 + IsGroupOnDimension2 + IsGroupOnLotNo = 1 Then
                RepName = RepName + "_With1Dimensions"
                If IsGroupOnDimension1 = 1 Then mShowForValue1 = "D1.Description" : mShowForHead1 = "'" & AgTemplate.ClsMain.FGetDimension1Caption() & "'"
                If IsGroupOnDimension2 = 1 Then mShowForValue1 = "D2.Description" : mShowForHead1 = "'" & AgTemplate.ClsMain.FGetDimension2Caption() & "'"
                If IsGroupOnLotNo = 1 Then mShowForValue1 = "L.LotNo" : mShowForHead1 = "'Lot No'"
                If IsGroupOnProcess = 1 Then mShowForValue1 = "P.Description" : mShowForHead1 = "'Process'"
            End If


            mCondStr = mCondStr & " And H.V_Date Between " & AgL.Chk_Text(ReportFrm.FGetText(0)) & " And " & AgL.Chk_Text(ReportFrm.FGetText(1)) & " "
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.V_Type", 5)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.SubCode ", 6)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.FromGodown", 7)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Item", 8)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemGroup", 9)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemCategory ", 10)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemType ", 11)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Dimension1", 13)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Dimension2", 14)

            If ReportFrm.FGetText(12) <> "" And ReportFrm.FGetText(12) <> "All" Then
                mQry = " Select '''' +  replace(ItemList,',',''',''')  + ''''  From ItemReportingGroup Where 1=1 " & ReportFrm.GetWhereCondition("Code", 12)
                mCondStr = mCondStr & " AND I.Code In (" & AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar & ")"
            End If

            ' mCondStr = mCondStr & VtypeRestriction
            mCondStr = mCondStr & " And H.Site_Code = '" & AgL.PubSiteCode & "' "
            mCondStr = mCondStr & " And H.Div_Code= '" & AgL.PubDivCode & "' "

            mQry = " SELECT H.V_Date, H.VoucherTypeDesc, G.Description AS GodownDesc,  " & _
                    " " & strGrpFld & " as GrpField, " & strGrpFldDesc & " as GrpFieldDesc, " & strGrpFldHead & " as GrpFieldHead, " & _
                    " " & Unit1Head & " as Unit1Head, " & Unit1 & " AS Unit1, " & Unit1DecimalPlace & " as Unit1DecimalPlace, " & IssueUnit1 & " as IssueUnit1, " & _
                    " " & Unit2Head & " as Unit2Head, " & Unit2 & " as Unit2, " & Unit2DecimalPlace & " as Unit2DecimalPlace, " & IssueUnit2 & " as IssueUnit2, " & _
                    " " & Unit3Head & " as Unit3Head, " & Unit3 & " as Unit3, " & Unit3DecimalPlace & " as Unit3DecimalPlace, " & IssueUnit3 & " as IssueUnit3, " & _
                    " " & mShowForValue1 & " as mShowForValue1, " & mShowForHead1 & " as mShowForHead1, " & _
                    " " & mShowForValue2 & " as mShowForValue2, " & mShowForHead2 & " as mShowForHead2, " & _
                    " " & mShowForValue3 & " as mShowForValue3, " & mShowForHead3 & " as mShowForHead3, " & _
                    " " & mShowForValue4 & " as mShowForValue4, " & mShowForHead4 & " as mShowForHead4 " & _
                    " FROM ( SELECT SH.*, Vt.Description as VoucherTypeDesc " & _
                    " FROM StockHead SH WITH (nolock) " & _
                    " LEFT JOIN Voucher_Type Vt WITH (nolock) On Vt.V_Type = SH.V_Type " & _
                    " WHERE Vt.NCat = '" & AgTemplate.ClsMain.Temp_NCat.StoreIssue & "' ) H " & _
                    " LEFT JOIN StockHeadDetail L WITH (nolock) ON L.DocID = H.DocID  " & _
                    " Left Join Subgroup Sg WITH (nolock) on H.SubCode = Sg.SubCode " & _
                    " Left Join City C WITH (nolock) on Sg.CityCode = C.CityCode  " & _
                    " LEFT JOIN Godown G WITH (nolock) ON G.Code = H.FromGodown  " & _
                    " Left Join Process P WITH (nolock) on H.Process = P.NCat " & _
                    " LEFT JOIN Item I WITH (nolock) ON I.Code = L.Item  " & _
                    " LEFT JOIN ItemGroup IG ON I.ItemGroup = IG.Code  " & _
                    " LEFT JOIN ItemCategory IC ON IG.ItemCategory = IC.Code " & _
                    " LEFT JOIN Unit U on U.Code = L.Unit " & _
                    " LEFT JOIN Unit UM on UM.Code = L.MeasureUnit " & _
                    " LEFT JOIN Enviro E ON E.Site_Code = H.Site_Code AND E.Div_Code = H.Div_Code " & _
                    " LEFT JOIN Dimension1 D1 ON D1.Code = L.Dimension1  " & _
                    " LEFT JOIN Dimension2 D2 ON D2.Code = L.Dimension2 " & _
                    " WHERE 1=1 " & mCondStr & " Order By H.V_Date  "

            DsRep = AgL.FillData(mQry, AgL.GCn)

            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records to Print!")

            ReportFrm.PrintReport(DsRep, RepName, RepTitle, ReportPath)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub
#End Region

#Region "Item Receive Report"
    Private Sub ProcItemReceiveReport()
        Dim mCondStr$ = ""
        RepTitle = "Item Receive Report"

        Try
            mCondStr = mCondStr & " And H.V_Date Between " & AgL.Chk_Text(ReportFrm.FGetText(0)) & " And " & AgL.Chk_Text(ReportFrm.FGetText(1)) & " "
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.V_Type", 2)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.SubCode ", 4)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.FromGodown", 5)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Item", 6)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemGroup", 7)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemCategory ", 8)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemType ", 9)

            ' mCondStr = mCondStr & " AND Vt.NCat = '" & AgTemplate.ClsMain.Temp_NCat.StoreReceive & "' "
            'mCondStr = mCondStr & VtypeRestriction

            If ReportFrm.FGetText(10) <> "" And ReportFrm.FGetText(10) <> "All" Then
                mQry = " Select '''' +  replace(ItemList,',',''',''')  + ''''  From ItemReportingGroup Where 1=1 " & ReportFrm.GetWhereCondition("Code", 10)
                mCondStr = mCondStr & " AND I.Code In (" & AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar & ")"
            End If

            mCondStr = mCondStr & " And H.Site_Code = '" & AgL.PubSiteCode & "' "
            mCondStr = mCondStr & " And H.Div_Code= '" & AgL.PubDivCode & "' "

            If ReportFrm.FGetText(3) = "Measure" Then
                If AgL.StrCmp(ReportFrm.FGetText(11), "Yes") Then
                    RepName = "Store_ItemReceiveReport_Measure_LotWise"
                Else
                    RepName = "Store_ItemReceiveReport_Measure"
                End If
            ElseIf ReportFrm.FGetText(3) = "Qty & Measure" Then
                If AgL.StrCmp(ReportFrm.FGetText(11), "Yes") Then
                    RepName = "Store_ItemReceiveReport_QtyMeasure_LotWise"
                Else
                    RepName = "Store_ItemReceiveReport_QtyMeasure"
                End If
            Else
                If AgL.StrCmp(ReportFrm.FGetText(11), "Yes") Then
                    RepName = "Store_ItemReceiveReport_LotWise"
                Else
                    RepName = "Store_ItemReceiveReport"
                End If
            End If


            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.Reason", 12)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Dimension1", 13)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Dimension2", 14)

            mQry = " SELECT H.DocID, H.V_Type, H.V_Date, H.ManualRefNo, Sg.DispName + (Case When IsNull(Sg.CityCode,'')<>'' Then ', ' + C.CityName Else '' End) as PartyName, H.Remarks + L.Remarks AS Remarks, " & _
                    " E.Caption_Dimension1, E.Caption_Dimension2, D1.Description AS D1Desc,D2.Description AS D2Desc," & _
                    " P.Description as ProcessDesc, H.VoucherTypeDesc, I.Description AS ItemDesc, L.Unit, L.LotNo, U.DecimalPlaces, UM.DecimalPlaces AS MeasureDecimalPlace, L.Qty, L.TotalMeasure, L.MeasureUnit, H.Remarks AS HeaderRemark, L.Remarks AS LineRemark  " & _
                    " FROM ( SELECT SH.*, Vt.Description as VoucherTypeDesc FROM StockHead SH WITH (nolock) LEFT JOIN Voucher_Type Vt WITH (nolock) On Vt.V_Type = SH.V_Type WHERE Vt.NCat = '" & AgTemplate.ClsMain.Temp_NCat.StoreReceive & "' ) H " & _
                    " LEFT JOIN StockHeadDetail L WITH (nolock) ON L.DocID = H.DocID  " & _
                    " Left Join Subgroup Sg WITH (nolock) on H.SubCode = Sg.SubCode " & _
                    " Left Join City C WITH (nolock) on Sg.CityCode = C.CityCode  " & _
                    " Left Join Process P WITH (nolock) on H.Process = P.NCat " & _
                    " LEFT JOIN Item I WITH (nolock) ON I.Code = L.Item  " & _
                    " LEFT JOIN Unit U on U.Code = L.Unit " & _
                    " LEFT JOIN Unit UM on UM.Code = L.MeasureUnit " & _
                    " LEFT JOIN Enviro E ON E.Site_Code = H.Site_Code AND E.Div_Code = H.Div_Code " & _
                    " LEFT JOIN Dimension1 D1 ON D1.Code = L.Dimension1  " & _
                    " LEFT JOIN Dimension2 D2 ON D2.Code = L.Dimension2 " & _
                    " WHERE 1=1 " & mCondStr & " Order By H.V_Date  "

            DsRep = AgL.FillData(mQry, AgL.GCn)

            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records to Print!")

            ReportFrm.PrintReport(DsRep, RepName, RepTitle, ReportPath)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub
#End Region

#Region "Material Receive Summary"
    Private Sub ProcMaterialReceiveSummary()
        RepName = "Store_MaterialReceive_Summary"

        Dim mCondStr$ = ""
        Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"
        Dim mShowForValue1$ = "''", mShowForValue2$ = "''", mShowForValue3$ = "''", mShowForValue4$ = "''"
        Dim mShowForHead1$ = "''", mShowForHead2$ = "''", mShowForHead3$ = "''", mShowForHead4$ = "''"

        Dim IsUnitQty As Integer = 0
        Dim IsUnitMeasure As Integer = 0
        Dim IsUnitAmount As Integer = 0

        Dim IsGroupOnProcess As Integer = 0
        Dim IsGroupOnDimension1 As Integer = 0
        Dim IsGroupOnDimension2 As Integer = 0
        Dim IsGroupOnLotNo As Integer = 0

        Dim Unit1Head = "''", Unit1 As String = "''", Unit1DecimalPlace As String = "''", ReceiveUnit1 As String = "''"
        Dim Unit2Head = "''", Unit2 As String = "''", Unit2DecimalPlace As String = "''", ReceiveUnit2 As String = "''"
        Dim Unit3Head = "''", Unit3 As String = "''", Unit3DecimalPlace As String = "''", ReceiveUnit3 As String = "''"

        Try
            If ReportFrm.FGetText(3) IsNot Nothing Then
                If ReportFrm.FGetText(3).ToString.Contains("Qty") = True Then IsUnitQty = 1
                If ReportFrm.FGetText(3).ToString.Contains("Measure") = True Then IsUnitMeasure = 1
                If ReportFrm.FGetText(3).ToString.Contains("Amount") = True Then IsUnitAmount = 1
            End If

            If IsUnitQty + IsUnitMeasure + IsUnitAmount = 3 Then
                RepName = RepName + "_With3Unit"
                Unit1 = "I.Unit" : Unit1Head = "'Qty'" : Unit1DecimalPlace = "U.DecimalPlaces" : ReceiveUnit1 = "L.Qty"
                Unit2 = "I.MeasureUnit" : Unit2Head = "'Measure'" : Unit2DecimalPlace = "UM.DecimalPlaces" : ReceiveUnit2 = "L.TotalMeasure"
                Unit3 = "E.DefaultCurrency" : Unit3Head = "'Currency'" : Unit3DecimalPlace = "2" : ReceiveUnit3 = "L.Amount"
            ElseIf IsUnitQty + IsUnitMeasure + IsUnitAmount = 2 Then
                RepName = RepName + "_With2Unit"
                If IsUnitQty = 1 And IsUnitMeasure = 1 Then
                    Unit1 = "I.Unit" : Unit1Head = "'Qty'" : Unit1DecimalPlace = "U.DecimalPlaces" : ReceiveUnit1 = "L.Qty"
                    Unit2 = "I.MeasureUnit" : Unit2Head = "'Measure'" : Unit2DecimalPlace = "UM.DecimalPlaces" : ReceiveUnit2 = "L.TotalMeasure"
                ElseIf IsUnitQty = 1 And IsUnitAmount = 1 Then
                    Unit1 = "I.Unit" : Unit1Head = "'Qty'" : Unit1DecimalPlace = "U.DecimalPlaces" : ReceiveUnit1 = "L.Qty"
                    Unit2 = "E.DefaultCurrency" : Unit2Head = "'Currency'" : Unit2DecimalPlace = "2" : ReceiveUnit2 = "L.Amount"
                ElseIf IsUnitMeasure = 1 And IsUnitAmount = 1 Then
                    Unit1 = "I.MeasureUnit" : Unit1Head = "'Measure'" : Unit1DecimalPlace = "UM.DecimalPlaces" : ReceiveUnit1 = "L.TotalMeasure"
                    Unit2 = "E.DefaultCurrency" : Unit2Head = "'Currency'" : Unit2DecimalPlace = "2" : ReceiveUnit2 = "L.Amount"
                End If
            ElseIf IsUnitQty + IsUnitMeasure + IsUnitAmount = 1 Then
                RepName = RepName + "_With1Unit"
                If IsUnitQty = 1 Then Unit1 = "I.Unit" : Unit1Head = "'Qty'" : Unit1DecimalPlace = "U.DecimalPlaces" : ReceiveUnit1 = "L.Qty"
                If IsUnitMeasure = 1 Then Unit1 = "I.MeasureUnit" : Unit1Head = "'Measure'" : Unit1DecimalPlace = "UM.DecimalPlaces" : ReceiveUnit1 = "L.TotalMeasure"
                If IsUnitAmount = 1 Then Unit1 = "E.DefaultCurrency" : Unit1Head = "'Currency'" : Unit1DecimalPlace = "2" : ReceiveUnit1 = "L.Amount"
            End If

            If ReportFrm.FGetText(2) = "Item Wise Summary" Then
                strGrpFld = "I.Description"
                strGrpFldDesc = "I.Description"
                strGrpFldHead = "'Item'"
                RepTitle = "Materail Receive Summary (Item Wise)"
            ElseIf ReportFrm.FGetText(2) = "Party Wise Summary" Then
                RepTitle = "Materail Receive Summary (Party Wise)"
                strGrpFld = "SG.Name"
                strGrpFldDesc = "SG.Name + ',' + IsNull(C.CityName,'')"
                strGrpFldHead = "'Party Name'"
            ElseIf ReportFrm.FGetText(2) = "Date Wise Summary" Then
                RepTitle = "Materail Receive Summary (Date Wise)"
                strGrpFld = "H.V_Date"
                strGrpFldDesc = "replace( convert(NVARCHAR,H.V_Date,106),' ','/')"
                strGrpFldHead = "'Date'"
            ElseIf ReportFrm.FGetText(2) = "Month Wise Summary" Then
                RepTitle = "Materail Receive Summary (Month Wise)"
                strGrpFld = "Substring(convert(nvarchar,H.V_Date,11),0,6)"
                strGrpFldDesc = "Replace(SubString(Convert(VARCHAR,H.v_Date,6),4,6),' ','-')"
                strGrpFldHead = "'Month'"
            ElseIf ReportFrm.FGetText(2) = "Item Category Wise Summary" Then
                strGrpFld = "IC.Description"
                strGrpFldDesc = "IC.Description"
                strGrpFldHead = "'Item Category'"
                RepTitle = "Materail Receive Summary (Item Category Wise)"
            End If

            If ReportFrm.FGetCode(4) IsNot Nothing Then
                If ReportFrm.FGetCode(4).ToString.Contains("Process") = True Then IsGroupOnProcess = 1
                If ReportFrm.FGetCode(4).ToString.Contains(AgTemplate.ClsMain.FGetDimension1Caption) = True Then IsGroupOnDimension1 = 1
                If ReportFrm.FGetCode(4).ToString.Contains(AgTemplate.ClsMain.FGetDimension2Caption) = True Then IsGroupOnDimension2 = 1
                If ReportFrm.FGetCode(4).ToString.Contains("Lot No") = True Then IsGroupOnLotNo = 1
            End If

            If IsGroupOnProcess + IsGroupOnDimension1 + IsGroupOnDimension2 + IsGroupOnLotNo = 4 Then
                RepName = RepName + "_With4Dimensions"
                mShowForValue1 = "P.Description"
                mShowForValue2 = "D1.Description"
                mShowForValue3 = "D2.Description"
                mShowForValue4 = "L.LotNo"
                mShowForHead1 = "'Process'"
                mShowForHead2 = "'" & AgTemplate.ClsMain.FGetDimension1Caption() & "'"
                mShowForHead3 = "'" & AgTemplate.ClsMain.FGetDimension2Caption() & "'"
                mShowForHead4 = "'Lot No'"
            ElseIf IsGroupOnProcess + IsGroupOnDimension1 + IsGroupOnDimension2 + IsGroupOnLotNo = 3 Then
                RepName = RepName + "_With3Dimensions"
                mShowForValue1 = "D1.Description"
                mShowForValue2 = "D2.Description"
                mShowForValue3 = "L.LotNo"
                mShowForHead1 = "'" & AgTemplate.ClsMain.FGetDimension1Caption() & "'"
                mShowForHead2 = "'" & AgTemplate.ClsMain.FGetDimension2Caption() & "'"
                mShowForHead3 = "'Lot No'"
            ElseIf IsGroupOnProcess + IsGroupOnDimension1 + IsGroupOnDimension2 + IsGroupOnLotNo = 2 Then
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
                ElseIf IsGroupOnProcess = 1 And IsGroupOnLotNo = 1 Then
                    mShowForValue1 = "P.Descriptionn" : mShowForHead1 = "'Process'"
                    mShowForValue2 = "L.LotNo" : mShowForHead2 = "'Lot No'"
                ElseIf IsGroupOnProcess = 1 And IsGroupOnDimension1 = 1 Then
                    mShowForValue1 = "P.Descriptionn" : mShowForHead1 = "'Process'"
                    mShowForValue2 = "D1.Description" : mShowForHead2 = "'" & AgTemplate.ClsMain.FGetDimension1Caption() & "'"
                ElseIf IsGroupOnProcess = 1 And IsGroupOnDimension2 = 1 Then
                    mShowForValue1 = "P.Descriptionn" : mShowForHead1 = "'Process'"
                    mShowForValue2 = "D2.Description" : mShowForHead2 = "'" & AgTemplate.ClsMain.FGetDimension2Caption() & "'"
                End If
            ElseIf IsGroupOnProcess + IsGroupOnDimension1 + IsGroupOnDimension2 + IsGroupOnLotNo = 1 Then
                RepName = RepName + "_With1Dimensions"
                If IsGroupOnDimension1 = 1 Then mShowForValue1 = "D1.Description" : mShowForHead1 = "'" & AgTemplate.ClsMain.FGetDimension1Caption() & "'"
                If IsGroupOnDimension2 = 1 Then mShowForValue1 = "D2.Description" : mShowForHead1 = "'" & AgTemplate.ClsMain.FGetDimension2Caption() & "'"
                If IsGroupOnLotNo = 1 Then mShowForValue1 = "L.LotNo" : mShowForHead1 = "'Lot No'"
                If IsGroupOnProcess = 1 Then mShowForValue1 = "P.Description" : mShowForHead1 = "'Process'"
            End If


            mCondStr = mCondStr & " And H.V_Date Between " & AgL.Chk_Text(ReportFrm.FGetText(0)) & " And " & AgL.Chk_Text(ReportFrm.FGetText(1)) & " "
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.V_Type", 5)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.SubCode ", 6)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.FromGodown", 7)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Item", 8)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemGroup", 9)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemCategory ", 10)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemType ", 11)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Dimension1", 13)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Dimension2", 14)

            If ReportFrm.FGetText(12) <> "" And ReportFrm.FGetText(12) <> "All" Then
                mQry = " Select '''' +  replace(ItemList,',',''',''')  + ''''  From ItemReportingGroup Where 1=1 " & ReportFrm.GetWhereCondition("Code", 12)
                mCondStr = mCondStr & " AND I.Code In (" & AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar & ")"
            End If

            ' mCondStr = mCondStr & VtypeRestriction
            mCondStr = mCondStr & " And H.Site_Code = '" & AgL.PubSiteCode & "' "
            mCondStr = mCondStr & " And H.Div_Code= '" & AgL.PubDivCode & "' "

            mQry = " SELECT H.V_Date, H.VoucherTypeDesc, G.Description AS GodownDesc,  " & _
                    " " & strGrpFld & " as GrpField, " & strGrpFldDesc & " as GrpFieldDesc, " & strGrpFldHead & " as GrpFieldHead, " & _
                    " " & Unit1Head & " as Unit1Head, " & Unit1 & " AS Unit1, " & Unit1DecimalPlace & " as Unit1DecimalPlace, " & ReceiveUnit1 & " as ReceiveUnit1, " & _
                    " " & Unit2Head & " as Unit2Head, " & Unit2 & " as Unit2, " & Unit2DecimalPlace & " as Unit2DecimalPlace, " & ReceiveUnit2 & " as ReceiveUnit2, " & _
                    " " & Unit3Head & " as Unit3Head, " & Unit3 & " as Unit3, " & Unit3DecimalPlace & " as Unit3DecimalPlace, " & ReceiveUnit3 & " as ReceiveUnit3, " & _
                    " " & mShowForValue1 & " as mShowForValue1, " & mShowForHead1 & " as mShowForHead1, " & _
                    " " & mShowForValue2 & " as mShowForValue2, " & mShowForHead2 & " as mShowForHead2, " & _
                    " " & mShowForValue3 & " as mShowForValue3, " & mShowForHead3 & " as mShowForHead3, " & _
                    " " & mShowForValue4 & " as mShowForValue4, " & mShowForHead4 & " as mShowForHead4 " & _
                    " FROM ( SELECT SH.*, Vt.Description as VoucherTypeDesc " & _
                    " FROM StockHead SH WITH (nolock) " & _
                    " LEFT JOIN Voucher_Type Vt WITH (nolock) On Vt.V_Type = SH.V_Type " & _
                    " WHERE Vt.NCat = '" & AgTemplate.ClsMain.Temp_NCat.StoreReceive & "' ) H " & _
                    " LEFT JOIN StockHeadDetail L WITH (nolock) ON L.DocID = H.DocID  " & _
                    " Left Join Subgroup Sg WITH (nolock) on H.SubCode = Sg.SubCode " & _
                    " Left Join City C WITH (nolock) on Sg.CityCode = C.CityCode  " & _
                    " LEFT JOIN Godown G WITH (nolock) ON G.Code = H.FromGodown  " & _
                    " Left Join Process P WITH (nolock) on H.Process = P.NCat " & _
                    " LEFT JOIN Item I WITH (nolock) ON I.Code = L.Item  " & _
                    " LEFT JOIN ItemGroup IG ON I.ItemGroup = IG.Code  " & _
                    " LEFT JOIN ItemCategory IC ON IG.ItemCategory = IC.Code " & _
                    " LEFT JOIN Unit U on U.Code = L.Unit " & _
                    " LEFT JOIN Unit UM on UM.Code = L.MeasureUnit " & _
                    " LEFT JOIN Enviro E ON E.Site_Code = H.Site_Code AND E.Div_Code = H.Div_Code " & _
                    " LEFT JOIN Dimension1 D1 ON D1.Code = L.Dimension1  " & _
                    " LEFT JOIN Dimension2 D2 ON D2.Code = L.Dimension2 " & _
                    " WHERE 1=1 " & mCondStr & " Order By H.V_Date  "

            DsRep = AgL.FillData(mQry, AgL.GCn)

            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records to Print!")

            ReportFrm.PrintReport(DsRep, RepName, RepTitle, ReportPath)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub
#End Region

#Region "Stock Transfer Summary"
    Private Sub ProcStockTransferSummary()
        'RepName = "Store_StockTransfer_Summary"

        Dim mCondStr$ = ""
        Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"

        Try

            If ReportFrm.FGetText(2) = "Item Wise Summary" Then
                strGrpFld = "I.Description"
                strGrpFldDesc = "I.Description"
                strGrpFldHead = "'Item'"
                RepTitle = "Stock Transfer Summary (Item Wise)"
            ElseIf ReportFrm.FGetText(2) = "Party Wise Summary" Then
                RepTitle = "Stock Transfer Summary (Party Wise)"
                strGrpFld = "SG.Name"
                strGrpFldDesc = "SG.Name + ',' + IsNull(C.CityName,'')"
                strGrpFldHead = "'Party Name'"
            ElseIf ReportFrm.FGetText(2) = "Month Wise Summary" Then
                RepTitle = "Stock Transfer Summary (Month Wise)"
                strGrpFld = "Substring(convert(nvarchar,H.V_Date,11),0,6)"
                strGrpFldDesc = "Replace(SubString(Convert(VARCHAR,H.v_Date,6),4,6),' ','-')"
                strGrpFldHead = "'Month'"
            ElseIf ReportFrm.FGetText(2) = "Item Category Wise Summary" Then
                strGrpFld = "IC.Description"
                strGrpFldDesc = "IC.Description"
                strGrpFldHead = "'Item Category'"
                RepTitle = "Stock Transfer Summary (Item Category Wise)"
            End If

            If ReportFrm.FGetText(3).ToString = "Qty" Then
                RepName = "Store_StockTransferSummary"
            Else
                RepName = "Store_StockTransferSummary_QtyMeasure"
            End If


            mCondStr = mCondStr & " And H.V_Date Between " & AgL.Chk_Text(ReportFrm.FGetText(0)) & " And " & AgL.Chk_Text(ReportFrm.FGetText(1)) & " "
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.V_Type", 4)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.SubCode ", 5)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.FromGodown", 6)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.ToGodown", 7)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Item", 8)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemGroup", 9)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemCategory ", 10)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemType ", 11)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Dimension1", 13)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Dimension2", 14)

            If ReportFrm.FGetText(12) <> "" And ReportFrm.FGetText(12) <> "All" Then
                mQry = " Select '''' +  replace(ItemList,',',''',''')  + ''''  From ItemReportingGroup Where 1=1 " & ReportFrm.GetWhereCondition("Code", 12)
                mCondStr = mCondStr & " AND I.Code In (" & AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar & ")"
            End If

            ' mCondStr = mCondStr & VtypeRestriction
            'mCondStr = mCondStr & " And H.Site_Code = '" & AgL.PubSiteCode & "' "
            'mCondStr = mCondStr & " And H.Div_Code= '" & AgL.PubDivCode & "' "

            mQry = " SELECT H.V_Date, H.VoucherTypeDesc, FG.Description AS FromGodownDesc, TG.Description AS ToGodownDesc,  " & _
                    " L.Unit, U.DecimalPlaces, L.Qty, L.TotalMeasure,  L.MeasureUnit, UM.DecimalPlaces AS MeasureDecimalPlaces, " & _
                    " " & strGrpFld & " as GrpField, " & strGrpFldDesc & " as GrpFieldDesc, " & strGrpFldHead & " as GrpFieldHead " & _
                    " FROM ( SELECT SH.*, Vt.Description as VoucherTypeDesc " & _
                    " FROM StockHead SH WITH (nolock) " & _
                    " LEFT JOIN Voucher_Type Vt WITH (nolock) On Vt.V_Type = SH.V_Type " & _
                    " WHERE Vt.NCat IN ('" & AgTemplate.ClsMain.Temp_NCat.StockTransfer & "','YITP' ) ) H " & _
                    " LEFT JOIN StockHeadDetail L WITH (nolock) ON L.DocID = H.DocID  " & _
                    " Left Join Subgroup Sg WITH (nolock) on H.SubCode = Sg.SubCode " & _
                    " Left Join City C WITH (nolock) on Sg.CityCode = C.CityCode  " & _
                    " LEFT JOIN Godown FG WITH (nolock) ON FG.Code = H.FromGodown  " & _
                    " LEFT JOIN Godown TG WITH (nolock) ON TG.Code = H.ToGodown " & _
                    " Left Join Process P WITH (nolock) on H.Process = P.NCat " & _
                    " LEFT JOIN Item I WITH (nolock) ON I.Code = L.Item  " & _
                    " LEFT JOIN ItemGroup IG ON I.ItemGroup = IG.Code  " & _
                    " LEFT JOIN ItemCategory IC ON IG.ItemCategory = IC.Code " & _
                    " LEFT JOIN Unit U on U.Code = L.Unit " & _
                    " LEFT JOIN Unit UM on UM.Code = L.MeasureUnit " & _
                    " LEFT JOIN Enviro E ON E.Site_Code = H.Site_Code AND E.Div_Code = H.Div_Code " & _
                    " LEFT JOIN Dimension1 D1 ON D1.Code = L.Dimension1  " & _
                    " LEFT JOIN Dimension2 D2 ON D2.Code = L.Dimension2 " & _
                    " WHERE 1=1 " & mCondStr & " Order By H.V_Date  "

            DsRep = AgL.FillData(mQry, AgL.GCn)

            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records to Print!")

            ReportFrm.PrintReport(DsRep, RepName, RepTitle, ReportPath)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub
#End Region


#Region "Stock Transfer Report"
    Private Sub ProcStockTransferReport()
        Dim mCondStr$ = ""
        RepTitle = "Stock Transfer Report"

        Try
            mCondStr = mCondStr & " And H.V_Date Between " & AgL.Chk_Text(ReportFrm.FGetText(0)) & " And " & AgL.Chk_Text(ReportFrm.FGetText(1)) & " "
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.V_Type", 2)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.FromGodown", 4)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.ToGodown", 5)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Item", 6)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemGroup", 7)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemCategory ", 8)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemType ", 9)

            'mCondStr = mCondStr & VtypeRestriction

            If ReportFrm.FGetText(10) <> "" And ReportFrm.FGetText(10) <> "All" Then
                mQry = " Select '''' +  replace(ItemList,',',''',''')  + ''''  From ItemReportingGroup Where 1=1 " & ReportFrm.GetWhereCondition("Code", 10)
                mCondStr = mCondStr & " AND I.Code In (" & AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar & ")"
            End If

            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Dimension1", 11)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Dimension2", 12)

            If ReportFrm.FGetText(3) = "Measure" Then
                RepName = "Store_GodownTransferReport_Measure"
            ElseIf ReportFrm.FGetText(3) = "Qty & Measure" Then
                RepName = "Store_GodownTransferReport_QtyMeasure"
            Else
                RepName = "Store_GodownTransferReport"
            End If

            mQry = " SELECT H.DocID, H.V_Type, H.V_Date, H.ManualRefNo, Sg.DispName + (Case When IsNull(Sg.CityCode,'')<>'' Then ', ' + C.CityName Else '' End) as PartyName, IsNull(H.Remarks,'') + IsNull(L.Remarks,'') AS Remarks,  GF.Description AS FromGoodownDesc ,GT.Description AS ToGoodownDesc, " & _
                    " E.Caption_Dimension1, E.Caption_Dimension2, D1.Description AS D1Desc,D2.Description AS D2Desc," & _
                    " P.Description as ProcessDesc, H.VoucherTypeDesc, I.Description AS ItemDesc, IG.Description as ItemGroupDesc, L.Unit, U.DecimalPlaces, UM.DecimalPlaces AS MeasureDecimalPlace, L.Qty, L.TotalMeasure, L.MeasureUnit, H.Remarks AS HeaderRemark, L.Remarks AS LineRemark  " & _
                    " FROM ( SELECT SH.*, Vt.Description as VoucherTypeDesc FROM StockHead SH WITH (nolock) LEFT JOIN Voucher_Type Vt WITH (nolock) On Vt.V_Type = SH.V_Type WHERE Vt.NCat IN ('" & AgTemplate.ClsMain.Temp_NCat.StockTransfer & "','YITP' ) ) H " & _
                    " LEFT JOIN StockHeadDetail L WITH (nolock) ON L.DocID = H.DocID  " & _
                    " Left Join Subgroup Sg WITH (nolock) on H.SubCode = Sg.SubCode " & _
                    " Left Join City C WITH (nolock) on Sg.CityCode = C.CityCode  " & _
                    " Left Join Process P WITH (nolock) on H.Process = P.NCat " & _
                    " LEFT JOIN Item I WITH (nolock) ON I.Code = L.Item  " & _
                    " LEFT JOIN ItemGroup IG WITH (nolock) ON I.ItemGroup = IG.Code  " & _
                    " LEFT JOIN Unit U on U.Code = L.Unit " & _
                    " LEFT JOIN Unit UM on UM.Code = L.MeasureUnit " & _
                    " LEFT JOIN Godown GF ON GF.Code=H.FromGodown  " & _
                    " LEFT JOIN Godown GT ON GT.Code=H.ToGodown  " & _
                    " LEFT JOIN Enviro E ON E.Site_Code = H.Site_Code AND E.Div_Code = H.Div_Code " & _
                    " LEFT JOIN Dimension1 D1 ON D1.Code = L.Dimension1  " & _
                    " LEFT JOIN Dimension2 D2 ON D2.Code = L.Dimension2 " & _
                    " WHERE 1=1 " & mCondStr & " Order By H.V_Date  "

            DsRep = AgL.FillData(mQry, AgL.GCn)

            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records to Print!")

            ReportFrm.PrintReport(DsRep, RepName, RepTitle, ReportPath)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub
#End Region

#Region "Physical Stock Report"
    Private Sub ProcPhysicalStockReport()
        Dim mCondStr$ = ""
        RepTitle = "Physical Stock Report"

        Try
            Dim strGrpFld As String = "''", strGrpFldHead As String = "''", strGrpFldDesc As String = "''"

            mCondStr = mCondStr & " And H.V_Date Between " & AgL.Chk_Text(ReportFrm.FGetText(0)) & " And " & AgL.Chk_Text(ReportFrm.FGetText(1)) & " "
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.V_Type", 3)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("H.FromGodown", 5)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Item", 6)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemGroup", 7)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemCategory ", 8)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.ItemType ", 9)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("I.Div_Code ", 11)

            If ReportFrm.FGetText(10) <> "" And ReportFrm.FGetText(10) <> "All" Then
                mQry = " Select '''' +  replace(ItemList,',',''',''')  + ''''  From ItemReportingGroup Where 1=1 " & ReportFrm.GetWhereCondition("Code", 10)
                mCondStr = mCondStr & " AND I.Code In (" & AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar & ")"
            End If

            'mCondStr = mCondStr & VtypeRestriction

            mCondStr = mCondStr & " And H.Site_Code = '" & AgL.PubSiteCode & "' "
            'mCondStr = mCondStr & " And H.Div_Code= '" & AgL.PubDivCode & "' "


            If ReportFrm.FGetText(2) = "Process Wise Summary" Then
                strGrpFld = "P.Sr"
                strGrpFldDesc = "P.Description"
                strGrpFldHead = "'Process'"
                RepTitle = "Process Wise Physical Stock Summary"

                If ReportFrm.FGetText(4) = "Measure" Then
                    RepName = "Store_PhysicalStockSummary_Measure"
                ElseIf ReportFrm.FGetText(4) = "Qty & Measure" Then
                    RepName = "Store_PhysicalStockSummary_QtyMeasure"
                Else
                    RepName = "Store_PhysicalStockSummary"
                End If
            ElseIf ReportFrm.FGetText(2) = "Category Wise Summary" Then
                strGrpFld = "IC.Description"
                strGrpFldDesc = "IC.Description"
                strGrpFldHead = "'Item Category'"
                RepTitle = "Item Category Wise Physical Stock Summary"
                If ReportFrm.FGetText(4) = "Measure" Then
                    RepName = "Store_PhysicalStockSummary_Measure"
                ElseIf ReportFrm.FGetText(4) = "Qty & Measure" Then
                    RepName = "Store_PhysicalStockSummary_QtyMeasure"
                Else
                    RepName = "Store_PhysicalStockSummary"
                End If
            ElseIf ReportFrm.FGetText(2) = "Construction Wise Summary" Then
                strGrpFld = "D.Construction"
                strGrpFldDesc = "D.Construction"
                strGrpFldHead = "'Construction'"
                RepTitle = "Construction Wise Physical Stock Summary"
                If ReportFrm.FGetText(4) = "Measure" Then
                    RepName = "Store_PhysicalStockSummary_Measure"
                ElseIf ReportFrm.FGetText(4) = "Qty & Measure" Then
                    RepName = "Store_PhysicalStockSummary_QtyMeasure"
                Else
                    RepName = "Store_PhysicalStockSummary"
                End If
            Else
                strGrpFld = "IC.Description"
                strGrpFldDesc = "IC.Description"
                strGrpFldHead = "'Item Category'"

                If ReportFrm.FGetText(4) = "Measure" Then
                    RepName = "Store_PhysicalStockReport_Measure"
                ElseIf ReportFrm.FGetText(4) = "Qty & Measure" Then
                    RepName = "Store_PhysicalStockReport_QtyMeasure"
                Else
                    RepName = "Store_PhysicalStockReport"
                End If
            End If



            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Dimension1", 12)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Dimension2", 13)
            mCondStr = mCondStr & ReportFrm.GetWhereCondition("L.Process", 14)

            mQry = " SELECT H.DocID, H.V_Type, H.V_Date, H.ManualRefNo, Sg.DispName + (Case When IsNull(Sg.CityCode,'')<>'' Then ', ' + C.CityName Else '' End) as PartyName, H.Remarks + L.Remarks AS Remarks, " & _
                    " P.Sr AS ProcessSr, P.Description as ProcessDesc, IC.Description as ItemCategoryDesc, H.VoucherTypeDesc, I.Description AS ItemDesc, L.Unit, U.DecimalPlaces, UM.DecimalPlaces AS MeasureDecimalPlace, L.Qty, L.Qty*I.Prod_Measure AS TotalMeasure, L.MeasureUnit, H.Remarks AS HeaderRemark, L.Remarks AS LineRemark,  " & _
                    " " & strGrpFld & " as GrpField, " & strGrpFldDesc & " as GrpFieldDesc, " & strGrpFldHead & " as GrpFieldHead " & _
                    " FROM ( SELECT SH.*, Vt.Description as VoucherTypeDesc FROM StockHead SH WITH (nolock) LEFT JOIN Voucher_Type Vt WITH (nolock) On Vt.V_Type = SH.V_Type WHERE Vt.NCat = '" & AgTemplate.ClsMain.Temp_NCat.PhysicalStockEntry & "' ) H " & _
                    " LEFT JOIN StockHeadDetail L WITH (nolock) ON L.DocID = H.DocID  " & _
                    " Left Join Subgroup Sg WITH (nolock) on H.SubCode = Sg.SubCode " & _
                    " Left Join City C WITH (nolock) on Sg.CityCode = C.CityCode  " & _
                    " Left Join Process P WITH (nolock) on L.Process = P.NCat " & _
                    " LEFT JOIN Item I WITH (nolock) ON I.Code = L.Item  " & _
                    " LEFT JOIN Rug_CarpetSKU CS ON CS.Code = I.Code " & _
                    " LEFT JOIN Rug_Design D ON D.Code = CS.Design " & _
                    " LEFT JOIN Unit U on U.Code = L.Unit " & _
                    " LEFT JOIN Unit UM on UM.Code = L.MeasureUnit " & _
                    " LEFT JOIN Enviro E ON E.Site_Code = H.Site_Code AND E.Div_Code = H.Div_Code " & _
                    " LEFT JOIN Dimension1 D1 ON D1.Code = L.Dimension1  " & _
                    " LEFT JOIN Dimension2 D2 ON D2.Code = L.Dimension2 " & _
                    " LEFT JOIN ItemCategory IC ON IC.Code  = I.ItemCategory " & _
                    " WHERE 1=1 " & mCondStr & " Order By H.V_Date  "

            DsRep = AgL.FillData(mQry, AgL.GCn)

            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records to Print!")

            ReportFrm.PrintReport(DsRep, RepName, RepTitle, ReportPath)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub
#End Region

#Region "Stock In Hand"
    Private Sub ProcStockInHand()
        Dim mParentQry$ = ""
        Dim mStockCondStr$ = ""
        Dim mStockQry$ = ""
        Dim mItemQry$ = ""

        Dim mGroupOnValue$ = ""
        Dim mGroupOn1$ = ""
        Dim mGroupOn1Value$ = ""
        Dim mGroupOnHeading$ = ""

        Dim mLotNoFieldName$

        Dim IsGroupOnGodown As Integer = 0
        Dim IsGroupOnProcess As Integer = 0
        Dim IsGroupOnDimension1 As Integer = 0
        Dim IsGroupOnDimension2 As Integer = 0
        Dim IsGroupOnLotNo As Integer = 0

        Dim mShowForValue1$ = "''", mShowForValue2$ = "''", mShowForValue3$ = "''"
        Dim mShowForHead1$ = "''", mShowForHead2$ = "''", mShowForHead3$ = "''"

        Dim Unit1Head = "''", Unit1 As String = "''", Unit1DecimalPlace As String = "''"
        Dim OpeningUnit1 = "''", ReceiveUnit1 As String = "''", IssueUnit1 As String = "''"


        Dim IsExcludeDimension1 As Integer = 0
        Dim IsExcludeDimension2 As Integer = 0
        Dim IsExcludeProcess As Integer = 0


        Try
            If AgL.StrCmp(ReportFrm.FGetText(2), "Stock Ledger") Then
                RepTitle = "Stock Ledger" : RepName = "Stock_StockLedger"
            ElseIf AgL.StrCmp(ReportFrm.FGetText(2), "Stock Summary (Voucher Type Wise)") Then
                RepTitle = "Stock Summary" : RepName = "Stock_StockSummary_WithVoucherType"
            Else
                RepTitle = "Stock Summary" : RepName = "Stock_StockSummary"
            End If

            If AgL.StrCmp(ReportFrm.FGetText(3), "Measure") Then
                Unit1Head = "'Measure'"
                Unit1 = "I.MeasureUnit"
                Unit1DecimalPlace = "MU.DecimalPlaces"

                OpeningUnit1 = "Sum(IsNull(S.Measure_Rec,0)) - Sum(IsNull(S.Measure_Iss,0))"
                ReceiveUnit1 = "IsNull(S.Measure_Rec,0)"
                IssueUnit1 = "isnull(S.Measure_Iss,0)"
            Else
                Unit1Head = "'Qty'"
                Unit1 = "I.Unit"
                Unit1DecimalPlace = "U.DecimalPlaces"

                OpeningUnit1 = "Sum(IsNull(S.Qty_Rec,0)) - Sum(IsNull(S.Qty_Iss,0))"
                ReceiveUnit1 = "IsNull(S.Qty_Rec,0)"
                IssueUnit1 = "IsNull(S.Qty_Iss,0)"
            End If

            mStockCondStr = mStockCondStr & " And S.Site_Code = '" & AgL.PubSiteCode & "' "
            mStockCondStr = mStockCondStr & ReportFrm.GetWhereCondition("S.Godown", 7)
            mStockCondStr = mStockCondStr & ReportFrm.GetWhereCondition("S.Item", 8)
            mStockCondStr = mStockCondStr & ReportFrm.GetWhereCondition("I.ItemGroup", 9)
            mStockCondStr = mStockCondStr & ReportFrm.GetWhereCondition("I.ItemCategory", 10)
            mStockCondStr = mStockCondStr & ReportFrm.GetWhereCondition("I.ItemType", 11)
            mStockCondStr = mStockCondStr & ReportFrm.GetWhereCondition("I.Div_Code", 12)
            mStockCondStr = mStockCondStr & ReportFrm.GetWhereCondition("S.Process", 13)
            mStockCondStr = mStockCondStr & ReportFrm.GetWhereCondition("S.LotNo", 14)
            mStockCondStr = mStockCondStr & ReportFrm.GetWhereCondition("S.Dimension1", 15)
            mStockCondStr = mStockCondStr & ReportFrm.GetWhereCondition("S.Dimension2", 16)

            If ReportFrm.FGetText(18) <> "" And ReportFrm.FGetText(18) <> "All" Then
                mQry = " Select '''' +  replace(ItemList,',',''',''')  + ''''  From ItemReportingGroup Where 1=1 " & ReportFrm.GetWhereCondition("Code", 18)
                mStockCondStr = mStockCondStr & " AND I.Code In (" & AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar & ")"
            End If



            IsGroupOnGodown = 0
            If ReportFrm.FGetText(4) = "Godown" Then
                'mGroupOnValue = "G.Description"
                'mGroupOn1Value = "Null"

                mGroupOnValue = "''"
                mGroupOn1Value = "Null"
                IsGroupOnGodown = 1
                mGroupOnHeading = "'Godown'"
            ElseIf ReportFrm.FGetText(4) = "Godown and Process" Then
                mGroupOnValue = "isnull(P.StockHead,P.Description)"
                mGroupOn1Value = "S.Process"
                mGroupOn1 = " ,S.Process"
                IsGroupOnProcess = 1
                IsGroupOnGodown = 1
                mGroupOnHeading = "'Process'"
            Else
                mGroupOnValue = "''"
                mGroupOn1Value = "Null"
                mGroupOnHeading = "''"
            End If

            If ReportFrm.FGetCode(5) IsNot Nothing Then
                If ReportFrm.FGetCode(5).ToString.Contains("Lot No") = True Then IsGroupOnLotNo = 1
                If ReportFrm.FGetCode(5).ToString.Contains(AgTemplate.ClsMain.FGetDimension1Caption) = True Then IsGroupOnDimension1 = 1
                If ReportFrm.FGetCode(5).ToString.Contains(AgTemplate.ClsMain.FGetDimension2Caption) = True Then IsGroupOnDimension2 = 1
            End If

            If ReportFrm.FGetCode(17) IsNot Nothing Then
                If ReportFrm.FGetCode(17).ToString.Contains("Process") = True Then IsExcludeProcess = 1
                If ReportFrm.FGetCode(17).ToString.Contains(AgTemplate.ClsMain.FGetDimension1Caption) = True Then IsExcludeDimension1 = 1
                If ReportFrm.FGetCode(17).ToString.Contains(AgTemplate.ClsMain.FGetDimension2Caption) = True Then IsExcludeDimension2 = 1
            End If

            If IsExcludeDimension1 = 1 Then mStockCondStr = mStockCondStr & " AND ISNULL(S.Dimension1,'') ='' "
            If IsExcludeDimension2 = 1 Then mStockCondStr = mStockCondStr & " AND ISNULL(S.Dimension2,'') ='' "
            If IsExcludeProcess = 1 Then mStockCondStr = mStockCondStr & " AND ISNULL(S.Process,'') ='' "


            If IsGroupOnLotNo + IsGroupOnDimension1 + IsGroupOnDimension2 = 3 Then
                RepName = RepName + "_WithDimensions3"
                mShowForValue1 = "D1.Description"
                mShowForValue2 = "D2.Description"
                mShowForValue3 = "V1.LotNo"
                mShowForHead1 = "'" & AgTemplate.ClsMain.FGetDimension1Caption() & "'"
                mShowForHead2 = "'" & AgTemplate.ClsMain.FGetDimension2Caption() & "'"
                mShowForHead3 = "'Lot No'"
            ElseIf IsGroupOnLotNo + IsGroupOnDimension1 + IsGroupOnDimension2 = 2 Then
                RepName = RepName + "_WithDimensions2"
                If IsGroupOnDimension1 = 1 And IsGroupOnDimension2 = 1 Then
                    mShowForValue1 = "D1.Description" : mShowForHead1 = "'" & AgTemplate.ClsMain.FGetDimension1Caption() & "'"
                    mShowForValue2 = "D2.Description" : mShowForHead2 = "'" & AgTemplate.ClsMain.FGetDimension2Caption() & "'"
                ElseIf IsGroupOnDimension1 = 1 And IsGroupOnLotNo = 1 Then
                    mShowForValue1 = "D1.Description" : mShowForHead1 = "'" & AgTemplate.ClsMain.FGetDimension1Caption() & "'"
                    mShowForValue2 = "V1.LotNo" : mShowForHead2 = "'Lot No'"
                ElseIf IsGroupOnDimension2 = 1 And IsGroupOnLotNo = 1 Then
                    mShowForValue1 = "D2.Description" : mShowForHead1 = "'" & AgTemplate.ClsMain.FGetDimension2Caption() & "'"
                    mShowForValue2 = "V1.LotNo" : mShowForHead2 = "'Lot No'"
                End If
            ElseIf IsGroupOnLotNo + IsGroupOnDimension1 + IsGroupOnDimension2 = 1 Then
                RepName = RepName + "_WithDimensions1"
                If IsGroupOnDimension1 = 1 Then mShowForValue1 = "D1.Description" : mShowForHead1 = "'" & AgTemplate.ClsMain.FGetDimension1Caption() & "'"
                If IsGroupOnDimension2 = 1 Then mShowForValue1 = "D2.Description" : mShowForHead1 = "'" & AgTemplate.ClsMain.FGetDimension2Caption() & "'"
                If IsGroupOnLotNo = 1 Then mShowForValue1 = "V1.LotNo" : mShowForHead1 = "'Lot No'"
            End If

            mLotNoFieldName = "Case When " & IsGroupOnLotNo & " = 1 And IsNull(Isd.IsRequired_LotNo,0) <> 0 Then S.LotNo Else Null End "

            If AgL.StrCmp(ReportFrm.FGetText(6), "All") Then
                mParentQry = " Stock S "
            Else
                Dim mBalcondStr As String = ""
                If AgL.StrCmp(ReportFrm.FGetText(6), "Not Zero") Then
                    mBalcondStr = " AND Round(clg.closingQty,4) <> 0 "
                ElseIf AgL.StrCmp(ReportFrm.FGetText(6), "Greater Than Zero") Then
                    mBalcondStr = " AND Round(clg.closingQty,4) > 0 "
                ElseIf AgL.StrCmp(ReportFrm.FGetText(6), "Less Than Zero") Then
                    mBalcondStr = " AND Round(clg.closingQty,4) < 0 "
                ElseIf AgL.StrCmp(ReportFrm.FGetText(6), "Period Negative") Then
                    mBalcondStr = " AND Round(clg.closingQty,4) < 0 " & _
                                  " AND Round(isnull(clg.closingQty,0),4) < Round(isnull(OP.OpeningQty,0),4) "
                ElseIf AgL.StrCmp(ReportFrm.FGetText(6), "Zero") Then
                    mBalcondStr = " AND Round(clg.closingQty,4) = 0 "
                End If
                Dim TUID As String = ""

                TUID = "#" & AgL.GetGUID(AgL.GCn).ToString

                If AgL.StrCmp(ReportFrm.FGetText(6), "Period Negative") Then
                    mItemQry = " SELECT clg.Item, " & _
                                " (CASE WHEN " & IsGroupOnGodown & " = 1 THEN clg.Godown ELSE NULL End) AS Godown, " & _
                                " (CASE WHEN " & IsGroupOnProcess & " = 1 THEN clg.process ELSE NULL End) AS Process, " & _
                                " (CASE WHEN " & IsGroupOnLotNo & " = 1 THEN clg.LotNo ELSE NULL End) AS LotNo, " & _
                                " (CASE WHEN " & IsGroupOnDimension1 & " = 1 THEN clg.Dimension1 ELSE NULL End) AS Dimension1, " & _
                                " (CASE WHEN " & IsGroupOnDimension2 & " = 1 THEN clg.Dimension2 ELSE NULL End) AS Dimension2, " & _
                                " clg.closingQty AS Closing, OP.OpeningQty AS Opening " & _
                                " into [" & TUID & "] " & _
                                " FROM " & _
                                " ( " & _
                                " SELECT S.Item,  " & _
                                " (CASE WHEN " & IsGroupOnGodown & " = 1 THEN S.Godown ELSE NULL End) AS Godown, " & _
                                " (CASE WHEN " & IsGroupOnProcess & " = 1 THEN S.process ELSE NULL End) AS Process, " & _
                                " (CASE WHEN " & IsGroupOnLotNo & " = 1 THEN S.LotNo ELSE NULL End) AS LotNo, " & _
                                " (CASE WHEN " & IsGroupOnDimension1 & " = 1 THEN S.Dimension1 ELSE NULL End) AS Dimension1, " & _
                                " (CASE WHEN " & IsGroupOnDimension2 & " = 1 THEN S.Dimension2 ELSE NULL End) AS Dimension2, " & _
                                " isnull(Sum(S.Qty_Rec),0)-isnull(Sum(S.Qty_Iss),0) AS closingQty " & _
                                " FROM Stock S WITH (nolock) " & _
                                " LEFT JOIN Item I WITH (nolock) ON S.Item = I.Code " & _
                                " Where S.V_Date <= " & AgL.Chk_Text(ReportFrm.FGetText(1)) & "  " & mStockCondStr & _
                                " GROUP BY S.Item,  " & _
                                " (CASE WHEN " & IsGroupOnGodown & " = 1 THEN S.Godown ELSE NULL End), " & _
                                " (CASE WHEN " & IsGroupOnProcess & " = 1 THEN S.process ELSE NULL End), " & _
                                " (CASE WHEN " & IsGroupOnLotNo & " = 1 THEN S.LotNo ELSE NULL End), " & _
                                " (CASE WHEN " & IsGroupOnDimension1 & " = 1 THEN S.Dimension1 ELSE NULL End), " & _
                                " (CASE WHEN " & IsGroupOnDimension2 & " = 1 THEN S.Dimension2 ELSE NULL End) " & _
                                " ) AS clg " & _
                                " LEFT JOIN " & _
                                " ( " & _
                                " SELECT S.Item,  " & _
                                " (CASE WHEN " & IsGroupOnGodown & " = 1 THEN S.Godown ELSE NULL End) AS Godown, " & _
                                " (CASE WHEN " & IsGroupOnProcess & " = 1 THEN S.process ELSE NULL End) AS Process, " & _
                                " (CASE WHEN " & IsGroupOnLotNo & " = 1 THEN S.LotNo ELSE NULL End) AS LotNo, " & _
                                " (CASE WHEN " & IsGroupOnDimension1 & " = 1 THEN S.Dimension1 ELSE NULL End) AS Dimension1, " & _
                                " (CASE WHEN " & IsGroupOnDimension2 & " = 1 THEN S.Dimension2 ELSE NULL End) AS Dimension2, " & _
                                " isnull(Sum(S.Qty_Rec),0)-isnull(Sum(S.Qty_Iss),0) AS OpeningQty " & _
                                " FROM Stock S WITH (nolock) " & _
                                " LEFT JOIN Item I WITH (nolock) ON S.Item = I.Code " & _
                                " Where S.V_Date < " & AgL.Chk_Text(ReportFrm.FGetText(0)) & "  " & mStockCondStr & _
                                " GROUP BY S.Item,  " & _
                                " (CASE WHEN " & IsGroupOnGodown & " = 1 THEN S.Godown ELSE NULL End), " & _
                                " (CASE WHEN " & IsGroupOnProcess & " = 1 THEN S.process ELSE NULL End), " & _
                                " (CASE WHEN " & IsGroupOnLotNo & " = 1 THEN S.LotNo ELSE NULL End), " & _
                                " (CASE WHEN " & IsGroupOnDimension1 & " = 1 THEN S.Dimension1 ELSE NULL End), " & _
                                " (CASE WHEN " & IsGroupOnDimension2 & " = 1 THEN S.Dimension2 ELSE NULL End) " & _
                                " ) AS OP ON clg.Item = OP.Item " & _
                                " AND isnull(clg.Godown,'') = isnull(OP.Godown,'') " & _
                                " AND isnull(clg.process,'') = isnull(OP.process,'') " & _
                                " AND isnull(clg.LotNo,'') = isnull(OP.LotNo,'') " & _
                                " AND isnull(clg.Dimension1,'') = isnull(OP.Dimension1,'') " & _
                                " AND isnull(clg.Dimension2,'') = isnull(OP.Dimension2,'') " & _
                                " Where 1= 1 " & mBalcondStr
                Else
                    mItemQry = " SELECT clg.Item, " & _
                                " (CASE WHEN " & IsGroupOnGodown & " = 1 THEN clg.Godown ELSE NULL End) AS Godown, " & _
                                " (CASE WHEN " & IsGroupOnProcess & " = 1 THEN clg.process ELSE NULL End) AS Process, " & _
                                " (CASE WHEN " & IsGroupOnLotNo & " = 1 THEN clg.LotNo ELSE NULL End) AS LotNo, " & _
                                " (CASE WHEN " & IsGroupOnDimension1 & " = 1 THEN clg.Dimension1 ELSE NULL End) AS Dimension1, " & _
                                " (CASE WHEN " & IsGroupOnDimension2 & " = 1 THEN clg.Dimension2 ELSE NULL End) AS Dimension2, " & _
                                " clg.closingQty AS Closing " & _
                                " into [" & TUID & "] " & _
                                " FROM " & _
                                " ( " & _
                                " SELECT S.Item,  " & _
                                " (CASE WHEN " & IsGroupOnGodown & " = 1 THEN S.Godown ELSE NULL End) AS Godown, " & _
                                " (CASE WHEN " & IsGroupOnProcess & " = 1 THEN S.process ELSE NULL End) AS Process, " & _
                                " (CASE WHEN " & IsGroupOnLotNo & " = 1 THEN S.LotNo ELSE NULL End) AS LotNo, " & _
                                " (CASE WHEN " & IsGroupOnDimension1 & " = 1 THEN S.Dimension1 ELSE NULL End) AS Dimension1, " & _
                                " (CASE WHEN " & IsGroupOnDimension2 & " = 1 THEN S.Dimension2 ELSE NULL End) AS Dimension2, " & _
                                " isnull(Sum(S.Qty_Rec),0)-isnull(Sum(S.Qty_Iss),0) AS closingQty " & _
                                " FROM Stock S WITH (nolock) " & _
                                " LEFT JOIN Item I WITH (nolock) ON S.Item = I.Code " & _
                                " Where S.V_Date <= " & AgL.Chk_Text(ReportFrm.FGetText(1)) & "  " & mStockCondStr & _
                                " GROUP BY S.Item,  " & _
                                " (CASE WHEN " & IsGroupOnGodown & " = 1 THEN S.Godown ELSE NULL End), " & _
                                " (CASE WHEN " & IsGroupOnProcess & " = 1 THEN S.process ELSE NULL End), " & _
                                " (CASE WHEN " & IsGroupOnLotNo & " = 1 THEN S.LotNo ELSE NULL End), " & _
                                " (CASE WHEN " & IsGroupOnDimension1 & " = 1 THEN S.Dimension1 ELSE NULL End), " & _
                                " (CASE WHEN " & IsGroupOnDimension2 & " = 1 THEN S.Dimension2 ELSE NULL End) " & _
                                " ) AS clg " & _
                                " Where 1= 1 " & mBalcondStr
                End If
                AgL.Dman_ExecuteNonQry(mItemQry, AgL.GCn)

                mParentQry = " [" & TUID & "] As VItem " & _
                            " LEFT JOIN Stock S ON S.Item = VItem.Item " & _
                            " AND " & IIf(IsGroupOnGodown = 1, "ISNULL(S.Godown,'')", "''") & " = ISNULL(VItem.Godown,'') " & _
                            " AND " & IIf(IsGroupOnProcess = 1, "ISNULL(S.Process,'')", "''") & " = ISNULL(VItem.Process,'') " & _
                            " AND " & IIf(IsGroupOnLotNo = 1, "ISNULL(S.LotNo,'')", "''") & " = ISNULL(VItem.LotNo,'') " & _
                            " AND " & IIf(IsGroupOnDimension1 = 1, "ISNULL(S.Dimension1,'')", "''") & " = ISNULL(VItem.Dimension1,'') " & _
                            " AND " & IIf(IsGroupOnDimension2 = 1, "ISNULL(S.Dimension2,'')", "''") & " = ISNULL(VItem.Dimension2,'') "
            End If

            Dim StrOpening As String = ""
            Dim StrReceive As String = ""
            Dim StrIssue As String = ""

            StrOpening = " SELECT S.Site_Code, '' AS SubCode, S.Godown, " & mGroupOn1Value & " AS Process, " & _
                        " " & AgL.Chk_Text(ReportFrm.FGetText(0)) & " As V_Date, 'Opening' As DocID,  " & _
                        " 1 AS V_No, '1' as RecID, 'Opening' As  V_Type, 'Opening' As  TransactionType, Null As PartyName, " & _
                        " S.Item ,S.Dimension1, S.Dimension2, max(I.Unit) AS Unit, " & _
                        " " & mLotNoFieldName & " As LotNo, " & _
                        " " & OpeningUnit1 & " AS OpeningUnit1, 0 AS ReceiveUnit1, 0 AS IssueUnit1, " & _
                        " 0 AS Sr " & _
                        " FROM " & mParentQry & " " & _
                        " LEFT JOIN Item I On S.Item = I.Code " & _
                        " LEFT JOIN SubGroup Sg On S.SubCode = Sg.SubCode " & _
                        " LEFT JOIN ItemSiteDetail Isd On S.Item = Isd.Code And Isd.Div_Code = S.Div_Code And Isd.Site_Code = S.Site_Code " & _
                        " WHERE S.V_Date < " & AgL.Chk_Text(ReportFrm.FGetText(0)) & " " & _
                        " " & mStockCondStr & " " & _
                        " GROUP BY S.Item, " & mLotNoFieldName & ", S.Site_Code, S.Godown, S.Dimension1, S.Dimension2 " & _
                        " " & mGroupOn1 & " " & _
                        " Having Sum(IsNull(S.Qty_Rec,0)) - Sum(IsNull(S.Qty_Iss,0)) <> 0 "



            StrReceive = " SELECT S.Site_Code, S.SubCode, S.Godown, S.Process, " & _
                    " S.V_Date, S.DocID, S.V_No, S.RecID, S.V_Type, Vt.Description As TransactionType, isnull(Sg.Name,'From Godown - ' + G.Description) AS Name, " & _
                    " S.Item , S.Dimension1, S.Dimension2, I.Unit AS Unit, " & _
                    " " & mLotNoFieldName & " As LotNo, " & _
                    " 0 AS OpeningUnit1, " & ReceiveUnit1 & " AS ReceiveUnit1, 0 AS IssueUnit1, " & _
                    " 1 AS Sr " & _
                    " FROM " & mParentQry & " " & _
                    " LEFT JOIN Item I On S.Item = I.Code " & _
                    " LEFT JOIN SubGroup Sg On S.SubCode = Sg.SubCode " & _
                    " LEFT JOIN StockHead SH WITH (Nolock) ON SH.DocID = S.DocID " & _
                    " LEFT JOIN Godown G WITH (Nolock) ON G.Code = SH.FromGodown " & _
                    " LEFT JOIN ItemSiteDetail Isd On S.Item = Isd.Code And Isd.Div_Code = S.Div_Code And Isd.Site_Code = S.Site_Code " & _
                    " LEFT JOIN Voucher_Type Vt On S.V_Type = Vt.V_Type " & _
                    " WHERE S.V_Date BETWEEN " & AgL.Chk_Text(ReportFrm.FGetText(0)) & " And " & AgL.Chk_Text(ReportFrm.FGetText(1)) & " AND isnull(S.Qty_Rec,0) <> 0 " & _
                    " " & mStockCondStr & " "



            StrIssue = " SELECT S.Site_Code, S.SubCode, S.Godown, S.Process, " & _
                    " S.V_Date, S.DocID , S.V_No, S.RecID, S.V_Type , Vt.Description As TransactionType, isnull(Sg.Name,'To Godown - ' + G.Description) AS Name, " & _
                    " S.Item , S.Dimension1, S.Dimension2, I.Unit AS Unit, " & _
                    " " & mLotNoFieldName & " As LotNo, " & _
                    " 0 AS OpeningUnit1, 0 AS ReceiveUnit1, " & IssueUnit1 & " AS IssueUnit1, " & _
                    " 2 AS Sr " & _
                    " FROM " & mParentQry & " " & _
                    " LEFT JOIN Item I On S.Item = I.Code " & _
                    " LEFT JOIN SubGroup  Sg On S.SubCode = Sg.SubCode " & _
                    " LEFT JOIN StockHead SH WITH (Nolock) ON SH.DocID = S.DocID " & _
                    " LEFT JOIN Godown G WITH (Nolock) ON G.Code = SH.ToGodown " & _
                    " LEFT JOIN ItemSiteDetail Isd On S.Item = Isd.Code And Isd.Div_Code = S.Div_Code And Isd.Site_Code = S.Site_Code " & _
                    " LEFT JOIN Voucher_Type Vt On S.V_Type = Vt.V_Type " & _
                    " WHERE S.V_Date BETWEEN " & AgL.Chk_Text(ReportFrm.FGetText(0)) & " And " & AgL.Chk_Text(ReportFrm.FGetText(1)) & " AND isnull(S.Qty_Iss,0) <> 0  " & _
                    " " & mStockCondStr & " "




            If AgL.StrCmp(ReportFrm.FGetText(2), "Stock Summary (Voucher Type Wise)") Then
                mStockQry = " SELECT 1 AS Sr, Case When " & IsGroupOnGodown & " = 1 THEN VM.Godown ELSE NULL END AS Godown, Case When " & IsGroupOnProcess & " = 1 THEN VM.Process ELSE NULL END AS Process, VM.Unit, " & _
                            " Case When sum(VM.OpeningUnit1) > 0 Then convert(NVARCHAR,VM.TransactionType) else NULL End AS Rec_V_Type, Case When sum(VM.OpeningUnit1) > 0 Then sum(VM.OpeningUnit1) else 0 End AS Rec_Qty, " & _
                            " Case When sum(VM.OpeningUnit1) < 0 Then convert(NVARCHAR,VM.TransactionType) else NULL End AS Iss_V_Type, Case When sum(VM.OpeningUnit1) < 0 Then sum(VM.OpeningUnit1) else 0 End AS Iss_Qty " & _
                          " FROM " & _
                          " ( " & StrOpening & " ) VM " & _
                          " GROUP BY VM.TransactionType, Case When " & IsGroupOnGodown & " = 1 THEN VM.Godown ELSE NULL END , Case When " & IsGroupOnProcess & " = 1 THEN VM.Process ELSE NULL END, VM.Unit "

                mStockQry = mStockQry & " UNION ALL " & _
                            " SELECT row_number() OVER ( PARTITION BY VM.Unit, Case When " & IsGroupOnGodown & " = 1 THEN VM.Godown ELSE NULL END, Case When " & IsGroupOnProcess & " = 1 THEN VM.Process ELSE NULL  END ORDER BY VM.TransactionType  )+1 AS Sr, Case When " & IsGroupOnGodown & " = 1 THEN VM.Godown ELSE NULL END AS Godown, Case When " & IsGroupOnProcess & " = 1 THEN VM.Process ELSE NULL END AS Process, VM.Unit, convert(NVARCHAR,VM.TransactionType) AS Rec_V_Type, sum(VM.ReceiveUnit1) AS Rec_Qty, NULL  AS  Iss_V_Type,   0 AS Iss_Qty " & _
                            " FROM " & _
                            " ( " & StrReceive & " ) VM " & _
                            " GROUP BY Case When " & IsGroupOnGodown & " = 1 THEN VM.Godown ELSE NULL END, Case When " & IsGroupOnProcess & " = 1 THEN VM.Process ELSE NULL END, VM.Unit, VM.TransactionType "

                mStockQry = mStockQry & " UNION ALL " & _
                            " SELECT row_number() OVER ( PARTITION BY VM.Unit, Case When " & IsGroupOnGodown & " = 1 THEN VM.Godown ELSE NULL END, Case When " & IsGroupOnProcess & " = 1 THEN VM.Process ELSE NULL END ORDER BY VM.TransactionType  )+1 AS Sr, Case When " & IsGroupOnGodown & " = 1 THEN VM.Godown ELSE NULL END AS Godown, Case When " & IsGroupOnProcess & " = 1 THEN VM.Process ELSE NULL END AS Process, VM.Unit, NULL  AS Rec_V_Type, 0 AS Rec_Qty, convert(NVARCHAR,VM.TransactionType)  AS  Iss_V_Type,   sum(VM.IssueUnit1) AS Iss_Qty   " & _
                            " FROM " & _
                            " ( " & StrIssue & " ) VM " & _
                            " GROUP BY  Case When " & IsGroupOnGodown & " = 1 THEN VM.Godown ELSE NULL END , Case When " & IsGroupOnProcess & " = 1 THEN VM.Process ELSE NULL END, VM.Unit, VM.TransactionType "

                mQry = " SELECT  V1.Godown, V1.Process, V1.Unit As Unit1, Max(U.DecimalPlaces) AS  Unit1DecimalPlace, max(Case When " & IsGroupOnGodown & " = 1 THEN G.Description ELSE NULL END ) AS GodownDesc, max(Case When " & IsGroupOnProcess & " = 1 THEN P.Description ELSE NULL END ) AS ProcessDesc, V1.Sr, max(V1.Rec_V_Type) AS Rec_V_Type, sum(isnull(V1.Rec_Qty,0)) AS Rec_Unit1, max(V1.Iss_V_Type) AS Iss_V_Type, sum(isnull(V1.Iss_Qty,0)) AS Iss_Unit1, " & _
                        " Case When " & IsGroupOnGodown & " = 1 THEN 'Yes' ELSE 'No' END  AS IsGroupOnGodown, Case When " & IsGroupOnProcess & " = 1 THEN 'Yes' ELSE 'No' END  AS IsGroupOnProcess " & _
                        " FROM (  " & mStockQry & " ) As V1  " & _
                        " LEFT JOIN Godown G ON G.Code = V1.Godown " & _
                        " LEFT JOIN Unit U On V1.Unit = U.Code " & _
                        " LEFT JOIN Process P ON P.NCat = V1.Process " & _
                        " GROUP BY  V1.Godown,V1.Process, V1.Unit, V1.Sr  "

            Else
                mStockQry = StrOpening & _
                            " UNION ALL " & StrReceive & _
                            " UNION ALL " & StrIssue

                mQry = " SELECT V1.*,SM.Name AS Site_Name, Case When " & IsGroupOnGodown & " = 1 THEN G.Description ELSE NULL END AS GodownDesc,P.Description AS ProcessDesc, I.Description AS ItemDesc, " & _
                        " " & Unit1Head & " AS Unit1Head, " & Unit1DecimalPlace & " AS Unit1DecimalPlace, " & Unit1 & " AS Unit1, " & _
                        " '" & ReportFrm.FGetText(6) & "' As ShowZeroBalances , " & mGroupOnHeading & " As GroupOn, " & mGroupOnValue & " AS GroupOnValue, " & _
                        " " & IsGroupOnGodown & " As IsGroupOnGodown, " & IsGroupOnProcess & " As IsGroupOnProcess,  " & _
                        " " & IsGroupOnLotNo & " As IsGroupOnLotNo, " & IsGroupOnDimension1 & " As IsGroupOnDimension1, " & IsGroupOnDimension2 & " As IsGroupOnDimension2, " & _
                        " " & mShowForHead1 & " AS mShowForHead1, " & mShowForHead2 & " AS mShowForHead2, " & mShowForHead3 & " AS mShowForHead3, " & _
                        " " & mShowForValue1 & " AS mShowForValue1, " & mShowForValue2 & " AS mShowForValue2, " & mShowForValue3 & " AS mShowForValue3 " & _
                        " FROM ( " & mStockQry & " ) As V1 " & _
                        " LEFT JOIN SiteMast SM ON SM.Code = V1.Site_Code " & _
                        " LEFT JOIN Godown G ON G.Code = V1.Godown " & _
                        " LEFT JOIN Process P ON P.NCat = V1.Process " & _
                        " LEFT JOIN Dimension1 D1 ON D1.Code = V1.Dimension1  " & _
                        " LEFT JOIN Dimension2 D2 ON D2.Code = V1.Dimension2 " & _
                        " LEFT JOIN Item I ON I.Code = V1.Item " & _
                        " LEFT JOIN Unit U On I.Unit = U.Code " & _
                        " LEFT JOIN Unit MU On I.MeasureUnit = MU.Code "
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

#Region "Stock In Process"
    Private Sub ProcStockInProcess()
        Dim mStockCondStr$ = ""
        Dim mStockQry$ = ""
        Dim mItemQry$ = ""
        Dim mGroupOnValue$ = ""
        Dim mParentQry$ = ""
        Dim mLotNoFieldName$

        Dim mProcessName$ = ""

        Dim mShowForValue1$ = "''", mShowForValue2$ = "''", mShowForValue3$ = "''"
        Dim mShowForHead1$ = "''", mShowForHead2$ = "''", mShowForHead3$ = "''"

        Dim IsGroupOnJobWorker As Integer = 0
        Dim IsGroupOnProcess As Integer = 0
        Dim IsGroupOnLotNo As Integer = 0
        Dim IsGroupOnDimension1 As Integer = 0
        Dim IsGroupOnDimension2 As Integer = 0

        If ReportFrm.FGetText(7).ToString.Contains(",") Or ReportFrm.FGetText(7) = "All" Then
            mProcessName = "Process"
        Else
            mProcessName = ReportFrm.FGetText(7)
        End If

        Try
            If AgL.StrCmp(ReportFrm.FGetText(2), "Detail") Then
                If AgL.StrCmp(ReportFrm.FGetText(3), "Measure") Then
                    RepTitle = "Stock In " & mProcessName : RepName = "Store_StockInProcess_Measure"
                Else
                    RepTitle = "Stock In " & mProcessName : RepName = "Store_StockInProcess"
                End If
            ElseIf AgL.StrCmp(ReportFrm.FGetText(2), "Summary") Then
                If AgL.StrCmp(ReportFrm.FGetText(3), "Measure") Then
                    RepTitle = "Stock In " & mProcessName : RepName = "Store_StockInProcessSummary_Measure"
                Else
                    RepTitle = "Stock In " & mProcessName : RepName = "Store_StockInProcessSummary"
                End If
            End If

            IsGroupOnJobWorker = 0
            IsGroupOnProcess = 0

            If ReportFrm.FGetText(4) = "Process" Then
                IsGroupOnProcess = 1
            ElseIf ReportFrm.FGetText(4) = "Process and Person" Then
                IsGroupOnJobWorker = 1
                IsGroupOnProcess = 1
            End If

            If ReportFrm.FGetCode(5) IsNot Nothing Then
                If ReportFrm.FGetCode(5).ToString.Contains("Lot No") = True Then IsGroupOnLotNo = 1
                If ReportFrm.FGetCode(5).ToString.Contains(AgTemplate.ClsMain.FGetDimension1Caption) = True Then IsGroupOnDimension1 = 1
                If ReportFrm.FGetCode(5).ToString.Contains(AgTemplate.ClsMain.FGetDimension2Caption) = True Then IsGroupOnDimension2 = 1
            End If

            If IsGroupOnLotNo + IsGroupOnDimension1 + IsGroupOnDimension2 = 3 Then
                RepName = RepName + "_With3Dimensions"
                mShowForValue1 = "V1.LotNo" : mShowForHead1 = "'Lot No'"
                mShowForValue2 = "D1.Description" : mShowForHead2 = "'" & AgTemplate.ClsMain.FGetDimension1Caption() & "'"
                mShowForValue3 = "D2.Description" : mShowForHead3 = "'" & AgTemplate.ClsMain.FGetDimension2Caption() & "'"
            ElseIf IsGroupOnLotNo + IsGroupOnDimension1 + IsGroupOnDimension2 = 2 Then
                RepName = RepName + "_With2Dimensions"
                If IsGroupOnDimension1 = 1 And IsGroupOnDimension2 = 1 Then
                    mShowForValue1 = "D1.Description" : mShowForHead1 = "'" & AgTemplate.ClsMain.FGetDimension1Caption() & "'"
                    mShowForValue2 = "D2.Description" : mShowForHead2 = "'" & AgTemplate.ClsMain.FGetDimension2Caption() & "'"
                End If
            ElseIf IsGroupOnLotNo + IsGroupOnDimension1 + IsGroupOnDimension2 = 1 Then
                RepName = RepName + "_With1Dimensions"
                If IsGroupOnDimension1 = 1 Then mShowForValue1 = "D1.Description" : mShowForHead1 = "'" & AgTemplate.ClsMain.FGetDimension1Caption() & "'"
                If IsGroupOnDimension2 = 1 Then mShowForValue1 = "D2.Description" : mShowForHead1 = "'" & AgTemplate.ClsMain.FGetDimension2Caption() & "'"
            End If

            mStockCondStr = mStockCondStr & " And S.Site_Code = '" & AgL.PubSiteCode & "' "
            mStockCondStr = mStockCondStr & ReportFrm.GetWhereCondition("S.Process", 7)
            mStockCondStr = mStockCondStr & ReportFrm.GetWhereCondition("S.SubCode", 8)
            mStockCondStr = mStockCondStr & ReportFrm.GetWhereCondition("S.Item", 9)
            mStockCondStr = mStockCondStr & ReportFrm.GetWhereCondition("I.ItemGroup", 10)
            mStockCondStr = mStockCondStr & ReportFrm.GetWhereCondition("I.ItemCategory", 11)
            mStockCondStr = mStockCondStr & ReportFrm.GetWhereCondition("I.ItemType", 12)
            mStockCondStr = mStockCondStr & ReportFrm.GetWhereCondition("I.Div_Code", 13)

            mStockCondStr = mStockCondStr & ReportFrm.GetWhereCondition("S.Dimension1", 14)
            mStockCondStr = mStockCondStr & ReportFrm.GetWhereCondition("S.Dimension2", 15)
            mStockCondStr = mStockCondStr & ReportFrm.GetWhereCondition("S.LotNo", 16)
            mStockCondStr = mStockCondStr & ReportFrm.GetWhereCondition("S.Div_Code", 17)

            If ReportFrm.FGetText(18) <> "" And ReportFrm.FGetText(18) <> "All" Then
                mQry = " Select '''' +  replace(ItemList,',',''',''')  + ''''  From ItemReportingGroup Where 1=1 " & ReportFrm.GetWhereCondition("Code", 18)
                mStockCondStr = mStockCondStr & " AND I.Code In (" & AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar & ")"
            End If

            mGroupOnValue = "V1.Name"

            'mLotNoFieldName = " Case When '" & ReportFrm.FGetText(12) & "' = 'Yes' And IsNull(Isd.IsRequired_LotNo,0) <> 0 Then S.LotNo Else Null End "
            mLotNoFieldName = " Case When  " & IsGroupOnLotNo & " = 1 AND IsNull(Isd.IsRequired_LotNo,0) <> 0 Then S.LotNo Else Null End "

            If AgL.StrCmp(ReportFrm.FGetText(6), "All") Then
                mParentQry = " StockProcess S "
            Else
                Dim strCond As String = ""
                If AgL.StrCmp(ReportFrm.FGetText(6), "Not Zero") Then
                    strCond = " Having Round(IsNull(Sum(S.Qty_Rec),0) - IsNull(Sum(S.Qty_Iss),0),4) <> 0 "
                ElseIf AgL.StrCmp(ReportFrm.FGetText(6), "Greater Than Zero") Then
                    strCond = " Having Round(IsNull(Sum(S.Qty_Rec),0) - IsNull(Sum(S.Qty_Iss),0),4) > 0 "
                ElseIf AgL.StrCmp(ReportFrm.FGetText(6), "Less Than Zero") Then
                    strCond = " Having Round(IsNull(Sum(S.Qty_Rec),0) - IsNull(Sum(S.Qty_Iss),0),4) < 0 "
                ElseIf AgL.StrCmp(ReportFrm.FGetText(6), "Zero") Then
                    strCond = " Having Round(IsNull(Sum(S.Qty_Rec),0) - IsNull(Sum(S.Qty_Iss),0),4) = 0 "
                End If

                If IsGroupOnLotNo = 1 Then

                    mItemQry = " Select S.SubCode ,  S.Item , ISNULL(S.Dimension1,'') AS Dimension1, isnull(S.Dimension2,'') AS Dimension2, " & _
                               " " & mLotNoFieldName & " As LotNo " & _
                               " From StockProcess S " & _
                               " LEFT JOIN Item I ON S.Item = I.Code " & _
                               " LEFT JOIN ItemSiteDetail Isd On S.Item = Isd.Code And Isd.Div_Code = S.Div_Code And Isd.Site_Code = S.Site_Code " & _
                               " Where S.V_Date <= " & AgL.Chk_Text(ReportFrm.FGetText(1)) & "  " & mStockCondStr & _
                               " Group By S.SubCode, S.Item, " & mLotNoFieldName & ", S.Dimension1, S.Dimension2 " & strCond
                    mParentQry = " (" & mItemQry & ") As VItem " & _
                                 " LEFT JOIN StockProcess S ON S.Item = VItem.Item AND S.SubCode = VItem.SubCode AND ISNULL( S.LotNo ,'') = ISNULL(VItem.LotNo,'') AND ISNULL(S.Dimension1,'') = ISNULL(VItem.Dimension1,'') AND ISNULL(S.Dimension2,'') = ISNULL(VItem.Dimension2,'') "

                Else
                    mItemQry = " Select S.SubCode ,  S.Item , ISNULL(S.Dimension1,'') AS Dimension1, isnull(S.Dimension2,'') AS Dimension2 " & _
                               " From StockProcess S " & _
                               " LEFT JOIN Item I ON S.Item = I.Code " & _
                               " Where S.V_Date <= " & AgL.Chk_Text(ReportFrm.FGetText(1)) & "  " & mStockCondStr & _
                               " Group By S.SubCode, S.Item, S.Dimension1, S.Dimension2 " & strCond
                    mParentQry = " (" & mItemQry & ") As VItem LEFT JOIN StockProcess S ON S.Item = VItem.Item AND S.SubCode = VItem.SubCode AND ISNULL(S.Dimension1,'') = ISNULL(VItem.Dimension1,'') AND ISNULL(S.Dimension2,'') = ISNULL(VItem.Dimension2,'') "


                End If


            End If

            Dim StrOpening As String = ""
            Dim StrReceive As String = ""
            Dim StrIssue As String = ""

            StrOpening = " SELECT S.Site_Code, S.SubCode, S.Process," & _
                        " " & AgL.Chk_Text(ReportFrm.FGetText(0)) & " As V_Date, 'Opening' As DocID,  " & _
                        " 1 AS V_No, '1' as RecID, 'Opening' As  V_Type, 'Opening' As  TransactionType, Max(Sg.Name) as Name, " & _
                        " S.Item ,S.Dimension1,S.Dimension2, Max(I.Unit) AS Unit," & _
                        " " & mLotNoFieldName & " As LotNo, " & _
                        " Sum(IsNull(S.Qty_Rec,0)) - Sum(IsNull(S.Qty_Iss,0)) AS Opening, 0 AS ReceiveQty, 0 AS IssueQty, " & _
                        " Sum(IsNull(S.Measure_Rec,0)) - Sum(IsNull(S.Measure_Iss,0)) AS OpeningMeasure, 0 AS ReceiveMeasure, 0 AS IssueMeasure, " & _
                        " 0 AS Sr " & _
                        " FROM " & mParentQry & " " & _
                        " LEFT JOIN Item I On S.Item = I.Code " & _
                        " LEFT JOIN SubGroup Sg On S.SubCode = Sg.SubCode " & _
                        " LEFT JOIN ItemSiteDetail Isd On S.Item = Isd.Code And Isd.Div_Code = S.Div_Code And Isd.Site_Code = S.Site_Code " & _
                        " WHERE S.V_Date < " & AgL.Chk_Text(ReportFrm.FGetText(0)) & " " & _
                        " " & mStockCondStr & " " & _
                        " GROUP BY S.Item, " & mLotNoFieldName & " , S.Site_Code, S.Process, S.SubCode,S.Dimension1,S.Dimension2 " & _
                        " Having Round(Sum(IsNull(S.Qty_Rec,0)) - Sum(IsNull(S.Qty_Iss,0)),4) <> 0 "

            StrReceive = " SELECT S.Site_Code, S.SubCode, S.Process, " & _
                    " S.V_Date, S.DocID, S.V_No, S.RecID, S.V_Type, Vt.Description As TransactionType, Sg.Name, " & _
                    " S.Item ,S.Dimension1,S.Dimension2, I.Unit AS Unit, " & _
                    " " & mLotNoFieldName & " As LotNo, " & _
                    " 0 AS Opening,IsNull(S.Qty_Rec,0)AS ReceiveQty,0 AS IssueQty,   " & _
                    " 0 AS OpeningMeasure,IsNull(S.Measure_Rec,0) AS ReceiveMeasure,isnull(S.Measure_Iss,0) AS IssueMeasure,   " & _
                    " 1 AS Sr " & _
                    " FROM " & mParentQry & " " & _
                    " LEFT JOIN Item I On S.Item = I.Code " & _
                    " LEFT JOIN SubGroup Sg On S.SubCode = Sg.SubCode " & _
                    " LEFT JOIN ItemSiteDetail Isd On S.Item = Isd.Code And Isd.Div_Code = S.Div_Code And Isd.Site_Code = S.Site_Code " & _
                    " LEFT JOIN Voucher_Type Vt On S.V_Type = Vt.V_Type " & _
                    " WHERE S.V_Date BETWEEN " & AgL.Chk_Text(ReportFrm.FGetText(0)) & " And " & AgL.Chk_Text(ReportFrm.FGetText(1)) & " AND isnull(S.Qty_Rec,0) <> 0 " & _
                    " " & mStockCondStr & " "

            StrIssue = " SELECT S.Site_Code, S.SubCode, S.Process, " & _
                    " S.V_Date, S.DocID , S.V_No, S.RecID, S.V_Type , Vt.Description As TransactionType, Sg.Name, " & _
                    " S.Item ,S.Dimension1,S.Dimension2, I.Unit AS Unit, " & _
                    " " & mLotNoFieldName & " As LotNo, " & _
                    " 0 AS Opening,0 AS ReceiveQty,IsNull(S.Qty_Iss,0) AS IssueQty,   " & _
                    " 0 AS OpeningMeasure,IsNull(S.Measure_Rec,0)AS ReceiveMeasure,isnull(S.Measure_Iss,0) AS IssueMeasure,   " & _
                    " 2 AS Sr " & _
                    " FROM " & mParentQry & " " & _
                    " LEFT JOIN Item I On S.Item = I.Code " & _
                    " LEFT JOIN SubGroup  Sg On S.SubCode = Sg.SubCode " & _
                    " LEFT JOIN ItemSiteDetail Isd On S.Item = Isd.Code And Isd.Div_Code = S.Div_Code And Isd.Site_Code = S.Site_Code " & _
                    " LEFT JOIN Voucher_Type Vt On S.V_Type = Vt.V_Type " & _
                    " WHERE S.V_Date BETWEEN " & AgL.Chk_Text(ReportFrm.FGetText(0)) & " And " & AgL.Chk_Text(ReportFrm.FGetText(1)) & " AND isnull(S.Qty_Iss,0) <> 0  " & _
                    " " & mStockCondStr & " "

            If AgL.StrCmp(ReportFrm.FGetText(2), "Voucher Type Wise Summary") Then
                RepTitle = "Stock in Process Summary"
                RepName = "Store_StockInProcess_VoucherTypeSummary"

                mStockQry = " SELECT 1 AS Sr, Case When " & IsGroupOnJobWorker & " = 1 THEN VM.Name ELSE NULL END AS JobWorker, Case When " & IsGroupOnProcess & " = 1 THEN VM.Process ELSE NULL END AS Process, VM.Unit, " & _
                              " Case When sum(VM.Opening) > 0 Then convert(NVARCHAR,VM.TransactionType) else NULL End  AS Rec_V_Type, Case When sum(VM.Opening) > 0 Then sum(VM.Opening) else 0 End  AS Rec_Qty, " & _
                              " Case When sum(VM.Opening) < 0 Then convert(NVARCHAR,VM.TransactionType) else NULL End  AS  Iss_V_Type,   Case When sum(VM.Opening) < 0 Then sum(VM.Opening) else 0 End AS Iss_Qty " & _
                              " FROM " & _
                              " ( " & StrOpening & " ) VM " & _
                              " GROUP BY VM.TransactionType,   Case When " & IsGroupOnJobWorker & " = 1 THEN VM.Name ELSE NULL END , Case When " & IsGroupOnProcess & " = 1 THEN VM.Process ELSE NULL END, VM.Unit "

                mStockQry = mStockQry & " UNION ALL " & _
                            " SELECT row_number() OVER ( PARTITION BY VM.Unit,  Case When " & IsGroupOnJobWorker & " = 1 THEN VM.Name ELSE NULL END , Case When " & IsGroupOnProcess & " = 1 THEN VM.Process ELSE NULL END  ORDER BY VM.TransactionType  )+1 AS Sr,  " & _
                            " Case When " & IsGroupOnJobWorker & " = 1 THEN VM.Name ELSE NULL END AS JobWorker, Case When " & IsGroupOnProcess & " = 1 THEN VM.Process ELSE NULL END AS Process, VM.Unit, convert(NVARCHAR,VM.TransactionType) AS Rec_V_Type, sum(VM.ReceiveQty) AS Rec_Qty, NULL  AS  Iss_V_Type,   0 AS Iss_Qty " & _
                            " FROM " & _
                            " ( " & StrReceive & " ) VM " & _
                            " GROUP BY  Case When " & IsGroupOnJobWorker & " = 1 THEN VM.Name ELSE NULL END , Case When " & IsGroupOnProcess & " = 1 THEN VM.Process ELSE NULL END , VM.Unit, VM.TransactionType "

                mStockQry = mStockQry & " UNION ALL " & _
                            " SELECT row_number() OVER ( PARTITION BY VM.Unit,  Case When " & IsGroupOnJobWorker & " = 1 THEN VM.Name ELSE NULL END , Case When " & IsGroupOnProcess & " = 1 THEN VM.Process ELSE NULL END  ORDER BY VM.TransactionType  )+1 AS Sr, " & _
                            " Case When " & IsGroupOnJobWorker & " = 1 THEN VM.Name ELSE NULL END AS JobWorker, Case When " & IsGroupOnProcess & " = 1 THEN VM.Process ELSE NULL END AS Process, VM.Unit, NULL  AS Rec_V_Type, 0 AS Rec_Qty, convert(NVARCHAR,VM.TransactionType)  AS  Iss_V_Type,   sum(VM.IssueQty) AS Iss_Qty   " & _
                            " FROM " & _
                            " ( " & StrIssue & " ) VM " & _
                            " GROUP BY  Case When " & IsGroupOnJobWorker & " = 1 THEN VM.Name ELSE NULL END , Case When " & IsGroupOnProcess & " = 1 THEN VM.Process ELSE NULL END , VM.Unit, VM.TransactionType "

                mQry = " SELECT   V1.Process, V1.Unit As Unit1, Max(U.DecimalPlaces) AS  Unit1DecimalPlace,  max(Case When " & IsGroupOnJobWorker & " = 1 THEN V1.JobWorker ELSE NULL END ) AS JobWorker, max(Case When " & IsGroupOnProcess & " = 1 THEN P.Description ELSE NULL END ) AS ProcessDesc, " & _
                        " V1.Sr, max(V1.Rec_V_Type) AS Rec_V_Type, sum(isnull(V1.Rec_Qty,0)) AS Rec_Unit1, max(V1.Iss_V_Type) AS Iss_V_Type, sum(isnull(V1.Iss_Qty,0)) AS Iss_Unit1, " & _
                        " Case When " & IsGroupOnJobWorker & " = 1 THEN 'Yes' ELSE 'No' END  AS IsGroupOnGodown, Case When " & IsGroupOnProcess & " = 1 THEN 'Yes' ELSE 'No' END  AS IsGroupOnProcess " & _
                        " FROM (  " & mStockQry & " ) As V1  " & _
                        " LEFT JOIN Unit U On V1.Unit = U.Code " & _
                        " LEFT JOIN Process P ON P.NCat = V1.Process " & _
                        " GROUP BY  V1.Process, V1.JobWorker, V1.Unit, V1.Sr  "

            Else
                mStockQry = StrOpening & _
                         " UNION ALL " & StrReceive & _
                         " UNION ALL " & StrIssue

                mQry = " SELECT V1.*,SM.Name AS Site_Name,P.Description AS ProcessDesc, P.Sr AS ProcessSr, I.Description AS ItemDesc, IG.Description AS ItemGroupDesc, I.Unit, '" & ReportFrm.FGetText(12) & "' As LotWiseYesNo, " & _
                        " '" & AgTemplate.ClsMain.FGetDimension1Caption() & "' AS Caption_Dimension1, '" & AgTemplate.ClsMain.FGetDimension2Caption() & "' AS Caption_Dimension2, D1.Description AS D1Desc,D2.Description AS D2Desc," & _
                        " U.DecimalPlaces As UnitDecimalPlaces, MU.DecimalPlaces As MeasureUnitDecimalPlaces, " & _
                        " '" & ReportFrm.FGetText(4) & "' As ShowZeroBalances , 'Job Worker' As GroupOn, " & mGroupOnValue & " AS GroupOnValue, " & _
                        " " & mShowForHead1 & " AS mShowForHead1, " & mShowForHead2 & " AS mShowForHead2, " & mShowForHead3 & " AS mShowForHead3, " & _
                        " " & mShowForValue1 & " AS mShowForValue1, " & mShowForValue2 & " AS mShowForValue2, " & mShowForValue3 & " AS mShowForValue3 " & _
                        " FROM ( " & mStockQry & " ) As V1 " & _
                        " LEFT JOIN SiteMast SM ON SM.Code = V1.Site_Code " & _
                        " LEFT JOIN Process P ON P.NCat = V1.Process " & _
                        " LEFT JOIN Item I ON I.Code = V1.Item " & _
                        " LEFT JOIN Dimension1 D1 ON D1.Code = V1.Dimension1  " & _
                        " LEFT JOIN Dimension2 D2 ON D2.Code = V1.Dimension2 " & _
                        " LEFT JOIN ItemGroup IG ON IG.Code = I.ItemGroup " & _
                        " LEFT JOIN Unit U On I.Unit = U.Code " & _
                        " LEFT JOIN Unit MU On I.MeasureUnit = MU.Code "
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

#Region "Stock Balance"
    Private Sub ProcStockBalance()
        Dim mStockCondStr$ = ""
        Dim mStockQry$ = ""
        Dim mGroupOn1$ = ""
        Dim mGroupOnValue$ = ""
        Dim mGroupOn1Value$ = ""

        Dim mLotNoFieldName$

        Dim IsGroupOnGodown As Integer = 0
        Dim IsGroupOnProcess As Integer = 0
        Dim IsGroupOnDimension1 As Integer = 0
        Dim IsGroupOnDimension2 As Integer = 0
        Dim IsGroupOnLotNo As Integer = 0

        Dim mShowForValue1$ = "''", mShowForValue2$ = "''", mShowForValue3$ = "''"
        Dim mShowForHead1$ = "''", mShowForHead2$ = "''", mShowForHead3$ = "''"
        Dim Unit1Head = "''", Unit1 As String = "''", Unit1DecimalPlace As String = "''"
        Dim BalanceUnit1 = "''"

        Dim IsExcludeDimension1 As Integer = 0
        Dim IsExcludeDimension2 As Integer = 0
        Dim IsExcludeProcess As Integer = 0


        Try
            RepTitle = "Stock Balance"
            RepName = "Stock_StockBalance"
            If ReportFrm.FGetText(1) = "Measure" Then
                Unit1Head = "'Measure'"
                Unit1 = "I.MeasureUnit"
                Unit1DecimalPlace = "MU.DecimalPlaces"
                BalanceUnit1 = "Sum(IsNull(S.Measure_Rec,0)) - Sum(IsNull(S.Measure_Iss,0))"
            Else
                Unit1Head = "'Qty'"
                Unit1 = "I.Unit"
                Unit1DecimalPlace = "U.DecimalPlaces"
                BalanceUnit1 = "Sum(IsNull(S.Qty_Rec,0)) - Sum(IsNull(S.Qty_Iss,0))"
            End If

            mStockCondStr = mStockCondStr & " And S.Site_Code = '" & AgL.PubSiteCode & "' "
            mStockCondStr = mStockCondStr & ReportFrm.GetWhereCondition("S.Godown", 5)
            mStockCondStr = mStockCondStr & ReportFrm.GetWhereCondition("S.Item", 6)
            mStockCondStr = mStockCondStr & ReportFrm.GetWhereCondition("I.ItemGroup", 7)
            mStockCondStr = mStockCondStr & ReportFrm.GetWhereCondition("I.ItemCategory", 8)
            mStockCondStr = mStockCondStr & ReportFrm.GetWhereCondition("I.ItemType", 9)
            mStockCondStr = mStockCondStr & ReportFrm.GetWhereCondition("I.Div_Code", 10)
            mStockCondStr = mStockCondStr & ReportFrm.GetWhereCondition("S.Process", 11)
            mStockCondStr = mStockCondStr & ReportFrm.GetWhereCondition("S.LotNo", 12)
            mStockCondStr = mStockCondStr & ReportFrm.GetWhereCondition("S.Dimension1", 13)
            mStockCondStr = mStockCondStr & ReportFrm.GetWhereCondition("S.Dimension2", 14)

            If ReportFrm.FGetCode(15) IsNot Nothing Then
                If ReportFrm.FGetCode(15).ToString.Contains("Process") = True Then IsExcludeProcess = 1
                If ReportFrm.FGetCode(15).ToString.Contains(AgTemplate.ClsMain.FGetDimension1Caption) = True Then IsExcludeDimension1 = 1
                If ReportFrm.FGetCode(15).ToString.Contains(AgTemplate.ClsMain.FGetDimension2Caption) = True Then IsExcludeDimension2 = 1
            End If

            If ReportFrm.FGetText(16) <> "" And ReportFrm.FGetText(16) <> "All" Then
                mQry = " Select '''' +  replace(ItemList,',',''',''')  + ''''  From ItemReportingGroup Where 1=1 " & ReportFrm.GetWhereCondition("Code", 16)
                mStockCondStr = mStockCondStr & " AND I.Code In (" & AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar & ")"
            End If

            If IsExcludeDimension1 = 1 Then mStockCondStr = mStockCondStr & " AND ISNULL(S.Dimension1,'') ='' "
            If IsExcludeDimension2 = 1 Then mStockCondStr = mStockCondStr & " AND ISNULL(S.Dimension2,'') ='' "
            If IsExcludeProcess = 1 Then mStockCondStr = mStockCondStr & " AND ISNULL(S.Process,'') ='' "

            IsGroupOnGodown = 1
            If ReportFrm.FGetText(2) = "Process" Then
                mGroupOn1Value = "isnull(P.StockHead,P.Description)"
                mGroupOnValue = "S.Process"
                mGroupOn1 = ",S.Process"
                IsGroupOnProcess = 1
            Else
                mGroupOn1Value = "''"
                mGroupOnValue = "Null"
            End If

            If ReportFrm.FGetCode(3) IsNot Nothing Then
                If ReportFrm.FGetCode(3).ToString.Contains("Lot No") = True Then IsGroupOnLotNo = 1
                If ReportFrm.FGetCode(3).ToString.Contains(AgTemplate.ClsMain.FGetDimension1Caption) = True Then IsGroupOnDimension1 = 1
                If ReportFrm.FGetCode(3).ToString.Contains(AgTemplate.ClsMain.FGetDimension2Caption) = True Then IsGroupOnDimension2 = 1
            End If

            Dim mBalcondStr As String = ""
            If AgL.StrCmp(ReportFrm.FGetText(4), "Greater Than Zero") Then
                mBalcondStr = " Having Round(IsNull(Sum(S.Qty_Rec),0),4) - Round(IsNull(Sum(S.Qty_Iss),0),4) > 0 "
            ElseIf AgL.StrCmp(ReportFrm.FGetText(4), "Less Than Zero") Then
                mBalcondStr = " Having Round(IsNull(Sum(S.Qty_Rec),0),4) - Round(IsNull(Sum(S.Qty_Iss),0),4) < 0 "
            Else
                mBalcondStr = " Having Round(IsNull(Sum(S.Qty_Rec),0),4) - Round(IsNull(Sum(S.Qty_Iss),0),4) <> 0 "
            End If

            If IsGroupOnLotNo + IsGroupOnDimension1 + IsGroupOnDimension2 = 3 Then
                RepName = RepName + "_WithDimensions3"
                mShowForValue1 = "D1.Description"
                mShowForValue2 = "D2.Description"
                mShowForValue3 = "V1.LotNo"
                mShowForHead1 = "'" & AgTemplate.ClsMain.FGetDimension1Caption() & "'"
                mShowForHead2 = "'" & AgTemplate.ClsMain.FGetDimension2Caption() & "'"
                mShowForHead3 = "'Lot No'"
            ElseIf IsGroupOnLotNo + IsGroupOnDimension1 + IsGroupOnDimension2 = 2 Then
                RepName = RepName + "_WithDimensions2"
                If IsGroupOnDimension1 = 1 And IsGroupOnDimension2 = 1 Then
                    mShowForValue1 = "D1.Description" : mShowForHead1 = "'" & AgTemplate.ClsMain.FGetDimension1Caption() & "'"
                    mShowForValue2 = "D2.Description" : mShowForHead2 = "'" & AgTemplate.ClsMain.FGetDimension2Caption() & "'"
                ElseIf IsGroupOnDimension1 = 1 And IsGroupOnLotNo = 1 Then
                    mShowForValue1 = "D1.Description" : mShowForHead1 = "'" & AgTemplate.ClsMain.FGetDimension1Caption() & "'"
                    mShowForValue2 = "V1.LotNo" : mShowForHead2 = "'Lot No'"
                ElseIf IsGroupOnDimension2 = 1 And IsGroupOnLotNo = 1 Then
                    mShowForValue1 = "D2.Description" : mShowForHead1 = "'" & AgTemplate.ClsMain.FGetDimension2Caption() & "'"
                    mShowForValue2 = "V1.LotNo" : mShowForHead2 = "'Lot No'"
                End If
            ElseIf IsGroupOnLotNo + IsGroupOnDimension1 + IsGroupOnDimension2 = 1 Then
                RepName = RepName + "_WithDimensions1"
                If IsGroupOnDimension1 = 1 Then mShowForValue1 = "D1.Description" : mShowForHead1 = "'" & AgTemplate.ClsMain.FGetDimension1Caption() & "'"
                If IsGroupOnDimension2 = 1 Then mShowForValue1 = "D2.Description" : mShowForHead1 = "'" & AgTemplate.ClsMain.FGetDimension2Caption() & "'"
                If IsGroupOnLotNo = 1 Then mShowForValue1 = "V1.LotNo" : mShowForHead1 = "'Lot No'"
            End If

            mLotNoFieldName = "Case When " & IsGroupOnLotNo & " = 1 And IsNull(max(convert(INT,Isd.IsRequired_LotNo)),0) <> 0 Then max(S.LotNo) Else Null End "

            mStockQry = " SELECT S.Item, S.Site_Code,  " & _
                        " (CASE WHEN " & IsGroupOnGodown & " = 1 THEN S.Godown ELSE NULL End) AS Godown, " & _
                        " (CASE WHEN " & IsGroupOnProcess & " = 1 THEN S.process ELSE NULL End) AS Process, " & _
                        " (CASE WHEN " & IsGroupOnLotNo & " = 1 THEN S.LotNo ELSE NULL End) AS LotNo, " & _
                        " (CASE WHEN " & IsGroupOnDimension1 & " = 1 THEN S.Dimension1 ELSE NULL End) AS Dimension1, " & _
                        " (CASE WHEN " & IsGroupOnDimension2 & " = 1 THEN S.Dimension2 ELSE NULL End) AS Dimension2, " & _
                        "  " & BalanceUnit1 & " AS BalUnit1 " & _
                        " FROM Stock S WITH (nolock) " & _
                        " LEFT JOIN Item I WITH (nolock) ON S.Item = I.Code " & _
                        " Where S.V_Date <= " & AgL.Chk_Text(ReportFrm.FGetText(0)) & "  " & mStockCondStr & _
                        " GROUP BY S.Item, S.Site_Code, " & _
                        " (CASE WHEN " & IsGroupOnGodown & " = 1 THEN S.Godown ELSE NULL End), " & _
                        " (CASE WHEN " & IsGroupOnProcess & " = 1 THEN S.process ELSE NULL End), " & _
                        " (CASE WHEN " & IsGroupOnLotNo & " = 1 THEN S.LotNo ELSE NULL End), " & _
                        " (CASE WHEN " & IsGroupOnDimension1 & " = 1 THEN S.Dimension1 ELSE NULL End), " & _
                        " (CASE WHEN " & IsGroupOnDimension2 & " = 1 THEN S.Dimension2 ELSE NULL End) " & mBalcondStr

            mQry = " SELECT V1.*,SM.Name AS Site_Name, G.Description AS GodownDesc,  P.Description AS ProcessDesc, I.Description AS ItemDesc, " & _
                   " " & Unit1Head & " AS Unit1Head, " & Unit1DecimalPlace & " AS Unit1DecimalPlace, " & Unit1 & " AS Unit1, " & _
                   " '" & ReportFrm.FGetText(2) & "' As GroupOn1Head, " & mGroupOn1Value & " AS GroupOnValue1, " & _
                   " " & mShowForHead1 & " AS mShowForHead1, " & mShowForHead2 & " AS mShowForHead2, " & mShowForHead3 & " AS mShowForHead3, " & _
                   " " & mShowForValue1 & " AS mShowForValue1, " & mShowForValue2 & " AS mShowForValue2, " & mShowForValue3 & " AS mShowForValue3 " & _
                   " FROM ( " & mStockQry & " ) As V1 " & _
                   " LEFT JOIN SiteMast SM ON SM.Code = V1.Site_Code " & _
                   " LEFT JOIN Godown G ON G.Code = V1.Godown " & _
                   " LEFT JOIN Process P ON P.NCat = V1.Process " & _
                   " LEFT JOIN Dimension1 D1 ON D1.Code = V1.Dimension1  " & _
                   " LEFT JOIN Dimension2 D2 ON D2.Code = V1.Dimension2 " & _
                   " LEFT JOIN Item I ON I.Code = V1.Item " & _
                   " LEFT JOIN Unit U On I.Unit = U.Code " & _
                   " LEFT JOIN Unit MU On I.MeasureUnit = MU.Code "

            DsRep = AgL.FillData(mQry, AgL.GCn)
            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records to Print!")
            ReportFrm.PrintReport(DsRep, RepName, RepTitle, ReportPath)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub
#End Region

#Region "Stock Balance Valuation"
    Private Sub ProcStockBalanceValuation()
        Dim mStockCondStr$ = ""

        Try
            RepTitle = "Stock Balance Valuation"
            If AgL.StrCmp(ReportFrm.FGetText(1), "Detail") Then
                RepName = "Stock_StockBalanceValuation_Detail"
            Else
                RepName = "Stock_StockBalanceValuation"
            End If

            mStockCondStr = mStockCondStr & " And H.Site_Code = '" & AgL.PubSiteCode & "' "
            mStockCondStr = mStockCondStr & ReportFrm.GetWhereCondition("H.Godown", 2)
            mStockCondStr = mStockCondStr & ReportFrm.GetWhereCondition("H.Item", 3)
            mStockCondStr = mStockCondStr & ReportFrm.GetWhereCondition("I.ItemGroup", 4)
            mStockCondStr = mStockCondStr & ReportFrm.GetWhereCondition("I.ItemCategory", 5)
            mStockCondStr = mStockCondStr & ReportFrm.GetWhereCondition("I.ItemType", 6)
            mStockCondStr = mStockCondStr & ReportFrm.GetWhereCondition("I.Div_Code", 7)

            mQry = "SELECT VMain.*, I.Unit, U.DecimalPlaces, IG.Description AS ItemGroup, " & _
                    " SUM(NetQty) OVER( PARTITION BY Item ORDER BY V_Date DESC , DocId, Sr  ) sum_stock1 , " & _
                    " CASE WHEN VMain.NetQty = 0 THEN  VMain.BalQty - SUM(NetQty) OVER( PARTITION BY Item ORDER BY V_Date DESC , DocId, Sr )  ELSE VMain.NetQty END AS NetBalQty " & _
                    " FROM " & _
                    " ( " & _
                    " SELECT P.ItemName, P.BalQty, SD.*, " & _
                    " CASE WHEN P.BalQty > SD.sum_stock THEN SD.Qty ELSE 0 END AS NetQty " & _
                    " FROM (  " & _
                    " SELECT I.Code AS Item, max(I.Description) AS ItemName, isnull(sum(H.Qty_Rec),0)- isnull(sum(H.Qty_Iss),0) AS BalQty " & _
                    " FROM Stock H " & _
                    " LEFT JOIN Item I ON I.Code = H.Item  " & _
                    " Where H.V_Date <= " & AgL.Chk_Text(ReportFrm.FGetText(0)) & "  " & mStockCondStr & _
                    " GROUP BY I.Code  " & _
                    " Having isnull(sum(H.Qty_Rec),0)- isnull(sum(H.Qty_Iss),0) > 0 " & _
                    " ) As p   " & _
                    " LEFT JOIN  " & _
                    " (  " & _
                    " SELECT s.*, SUM(Qty) OVER( PARTITION BY Item ORDER BY V_Date DESC , DocId, Sr  ) sum_stock   " & _
                    " FROM   " & _
                    " ( " & _
                    " SELECT H.DocID, H.RecId, H.V_TYpe, H.Sr, H.V_Date, H.Rate, H.Qty_Rec AS Qty, I.Code AS Item " & _
                    " FROM Stock H " & _
                    " LEFT JOIN Item I ON I.Code = H.Item  " & _
                    " WHERE isnull(H.Qty_Rec,0) <> 0 AND H.V_Type in ('GR','PINV')" & _
                    " AND H.V_Date <= " & AgL.Chk_Text(ReportFrm.FGetText(0)) & "  " & mStockCondStr & _
                    " )  s   " & _
                    " ) SD ON SD.Item = p.Item " & _
                    " AND IsNull(p.BalQty,0)  > IsNull(SD.sum_stock,0)  - IsNull(SD.Qty,0) " & _
                    " ) VMain " & _
                    " LEFT JOIN Item I ON I.Code = VMain.Item  " & _
                    " LEFT JOIN ItemGroup IG ON IG.Code = I.ItemGroup  " & _
                    " LEFT JOIN Unit U on U.Code = I.Unit " & _
                    " ORDER BY  V_Date DESC, DocId, Sr  "

            DsRep = AgL.FillData(mQry, AgL.GCn)
            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records to Print!")
            ReportFrm.PrintReport(DsRep, RepName, RepTitle, ReportPath)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub
#End Region


#Region "Stock Balance With Average Rate"
    Private Sub ProcStockBalanceWithAverageRate()
        Dim mStockCondStr$ = ""
        Dim mStockQry$ = ""
        Dim mGroupOn1$ = ""
        Dim mGroupOnValue$ = ""
        Dim mGroupOn1Value$ = ""

        Dim mLotNoFieldName$

        Dim IsGroupOnGodown As Integer = 0
        Dim IsGroupOnProcess As Integer = 0
        Dim IsGroupOnDimension1 As Integer = 0
        Dim IsGroupOnDimension2 As Integer = 0
        Dim IsGroupOnLotNo As Integer = 0

        Dim mShowForValue1$ = "''", mShowForValue2$ = "''", mShowForValue3$ = "''"
        Dim mShowForHead1$ = "''", mShowForHead2$ = "''", mShowForHead3$ = "''"
        Dim Unit1Head = "''", Unit1 As String = "''", Unit1DecimalPlace As String = "''"
        Dim BalanceUnit1 = "''"

        Dim IsExcludeDimension1 As Integer = 0
        Dim IsExcludeDimension2 As Integer = 0
        Dim IsExcludeProcess As Integer = 0


        Try
            RepTitle = "Stock Balance"
            RepName = "Stock_StockBalance"
            If ReportFrm.FGetText(1) = "Measure" Then
                Unit1Head = "'Measure'"
                Unit1 = "I.MeasureUnit"
                Unit1DecimalPlace = "MU.DecimalPlaces"
                BalanceUnit1 = "Sum(IsNull(S.Measure_Rec,0)) - Sum(IsNull(S.Measure_Iss,0))"
            Else
                Unit1Head = "'Qty'"
                Unit1 = "I.Unit"
                Unit1DecimalPlace = "U.DecimalPlaces"
                BalanceUnit1 = "Sum(IsNull(S.Qty_Rec,0)) - Sum(IsNull(S.Qty_Iss,0))"
            End If

            mStockCondStr = mStockCondStr & " And S.Site_Code = '" & AgL.PubSiteCode & "' "
            mStockCondStr = mStockCondStr & ReportFrm.GetWhereCondition("S.Godown", 5)
            mStockCondStr = mStockCondStr & ReportFrm.GetWhereCondition("S.Item", 6)
            mStockCondStr = mStockCondStr & ReportFrm.GetWhereCondition("I.ItemGroup", 7)
            mStockCondStr = mStockCondStr & ReportFrm.GetWhereCondition("I.ItemCategory", 8)
            mStockCondStr = mStockCondStr & ReportFrm.GetWhereCondition("I.ItemType", 9)
            mStockCondStr = mStockCondStr & ReportFrm.GetWhereCondition("I.Div_Code", 10)
            mStockCondStr = mStockCondStr & ReportFrm.GetWhereCondition("S.Process", 11)
            mStockCondStr = mStockCondStr & ReportFrm.GetWhereCondition("S.LotNo", 12)
            mStockCondStr = mStockCondStr & ReportFrm.GetWhereCondition("S.Dimension1", 13)
            mStockCondStr = mStockCondStr & ReportFrm.GetWhereCondition("S.Dimension2", 14)

            If ReportFrm.FGetCode(15) IsNot Nothing Then
                If ReportFrm.FGetCode(15).ToString.Contains("Process") = True Then IsExcludeProcess = 1
                If ReportFrm.FGetCode(15).ToString.Contains(AgTemplate.ClsMain.FGetDimension1Caption) = True Then IsExcludeDimension1 = 1
                If ReportFrm.FGetCode(15).ToString.Contains(AgTemplate.ClsMain.FGetDimension2Caption) = True Then IsExcludeDimension2 = 1
            End If

            If ReportFrm.FGetText(16) <> "" And ReportFrm.FGetText(16) <> "All" Then
                mQry = " Select '''' +  replace(ItemList,',',''',''')  + ''''  From ItemReportingGroup Where 1=1 " & ReportFrm.GetWhereCondition("Code", 16)
                mStockCondStr = mStockCondStr & " AND I.Code In (" & AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar & ")"
            End If

            If IsExcludeDimension1 = 1 Then mStockCondStr = mStockCondStr & " AND ISNULL(S.Dimension1,'') ='' "
            If IsExcludeDimension2 = 1 Then mStockCondStr = mStockCondStr & " AND ISNULL(S.Dimension2,'') ='' "
            If IsExcludeProcess = 1 Then mStockCondStr = mStockCondStr & " AND ISNULL(S.Process,'') ='' "

            IsGroupOnGodown = 1
            If ReportFrm.FGetText(2) = "Process" Then
                mGroupOn1Value = "isnull(P.StockHead,P.Description)"
                mGroupOnValue = "S.Process"
                mGroupOn1 = ",S.Process"
                IsGroupOnProcess = 1
            Else
                mGroupOn1Value = "''"
                mGroupOnValue = "Null"
            End If

            If ReportFrm.FGetCode(3) IsNot Nothing Then
                If ReportFrm.FGetCode(3).ToString.Contains("Lot No") = True Then IsGroupOnLotNo = 1
                If ReportFrm.FGetCode(3).ToString.Contains(AgTemplate.ClsMain.FGetDimension1Caption) = True Then IsGroupOnDimension1 = 1
                If ReportFrm.FGetCode(3).ToString.Contains(AgTemplate.ClsMain.FGetDimension2Caption) = True Then IsGroupOnDimension2 = 1
            End If

            Dim mBalcondStr As String = ""
            If AgL.StrCmp(ReportFrm.FGetText(4), "Greater Than Zero") Then
                mBalcondStr = " Having Round(IsNull(Sum(S.Qty_Rec),0),4) - Round(IsNull(Sum(S.Qty_Iss),0),4) > 0 "
            ElseIf AgL.StrCmp(ReportFrm.FGetText(4), "Less Than Zero") Then
                mBalcondStr = " Having Round(IsNull(Sum(S.Qty_Rec),0),4) - Round(IsNull(Sum(S.Qty_Iss),0),4) < 0 "
            Else
                mBalcondStr = " Having Round(IsNull(Sum(S.Qty_Rec),0),4) - Round(IsNull(Sum(S.Qty_Iss),0),4) <> 0 "
            End If

            If IsGroupOnLotNo + IsGroupOnDimension1 + IsGroupOnDimension2 = 3 Then
                RepName = RepName + "_WithDimensions3"
                mShowForValue1 = "D1.Description"
                mShowForValue2 = "D2.Description"
                mShowForValue3 = "V1.LotNo"
                mShowForHead1 = "'" & AgTemplate.ClsMain.FGetDimension1Caption() & "'"
                mShowForHead2 = "'" & AgTemplate.ClsMain.FGetDimension2Caption() & "'"
                mShowForHead3 = "'Lot No'"
            ElseIf IsGroupOnLotNo + IsGroupOnDimension1 + IsGroupOnDimension2 = 2 Then
                RepName = RepName + "_WithDimensions2"
                If IsGroupOnDimension1 = 1 And IsGroupOnDimension2 = 1 Then
                    mShowForValue1 = "D1.Description" : mShowForHead1 = "'" & AgTemplate.ClsMain.FGetDimension1Caption() & "'"
                    mShowForValue2 = "D2.Description" : mShowForHead2 = "'" & AgTemplate.ClsMain.FGetDimension2Caption() & "'"
                ElseIf IsGroupOnDimension1 = 1 And IsGroupOnLotNo = 1 Then
                    mShowForValue1 = "D1.Description" : mShowForHead1 = "'" & AgTemplate.ClsMain.FGetDimension1Caption() & "'"
                    mShowForValue2 = "V1.LotNo" : mShowForHead2 = "'Lot No'"
                ElseIf IsGroupOnDimension2 = 1 And IsGroupOnLotNo = 1 Then
                    mShowForValue1 = "D2.Description" : mShowForHead1 = "'" & AgTemplate.ClsMain.FGetDimension2Caption() & "'"
                    mShowForValue2 = "V1.LotNo" : mShowForHead2 = "'Lot No'"
                End If
            ElseIf IsGroupOnLotNo + IsGroupOnDimension1 + IsGroupOnDimension2 = 1 Then
                RepName = RepName + "_WithDimensions1"
                If IsGroupOnDimension1 = 1 Then mShowForValue1 = "D1.Description" : mShowForHead1 = "'" & AgTemplate.ClsMain.FGetDimension1Caption() & "'"
                If IsGroupOnDimension2 = 1 Then mShowForValue1 = "D2.Description" : mShowForHead1 = "'" & AgTemplate.ClsMain.FGetDimension2Caption() & "'"
                If IsGroupOnLotNo = 1 Then mShowForValue1 = "V1.LotNo" : mShowForHead1 = "'Lot No'"
            End If

            mLotNoFieldName = "Case When " & IsGroupOnLotNo & " = 1 And IsNull(max(convert(INT,Isd.IsRequired_LotNo)),0) <> 0 Then max(S.LotNo) Else Null End "

            mStockQry = " SELECT S.Item, S.Site_Code,  " & _
                        " (CASE WHEN " & IsGroupOnGodown & " = 1 THEN S.Godown ELSE NULL End) AS Godown, " & _
                        " (CASE WHEN " & IsGroupOnProcess & " = 1 THEN S.process ELSE NULL End) AS Process, " & _
                        " (CASE WHEN " & IsGroupOnLotNo & " = 1 THEN S.LotNo ELSE NULL End) AS LotNo, " & _
                        " (CASE WHEN " & IsGroupOnDimension1 & " = 1 THEN S.Dimension1 ELSE NULL End) AS Dimension1, " & _
                        " (CASE WHEN " & IsGroupOnDimension2 & " = 1 THEN S.Dimension2 ELSE NULL End) AS Dimension2, " & _
                        "  " & BalanceUnit1 & " AS BalUnit1 " & _
                        " FROM Stock S WITH (nolock) " & _
                        " LEFT JOIN Item I WITH (nolock) ON S.Item = I.Code " & _
                        " Where S.V_Date <= " & AgL.Chk_Text(ReportFrm.FGetText(0)) & "  " & mStockCondStr & _
                        " GROUP BY S.Item, S.Site_Code, " & _
                        " (CASE WHEN " & IsGroupOnGodown & " = 1 THEN S.Godown ELSE NULL End), " & _
                        " (CASE WHEN " & IsGroupOnProcess & " = 1 THEN S.process ELSE NULL End), " & _
                        " (CASE WHEN " & IsGroupOnLotNo & " = 1 THEN S.LotNo ELSE NULL End), " & _
                        " (CASE WHEN " & IsGroupOnDimension1 & " = 1 THEN S.Dimension1 ELSE NULL End), " & _
                        " (CASE WHEN " & IsGroupOnDimension2 & " = 1 THEN S.Dimension2 ELSE NULL End) " & mBalcondStr

            'mQry = " SELECT V1.*, VInv.AvgRate, isnull(VInv.AvgRate,0)*V1.BalUnit1 AS AvgAmount, SM.Name AS Site_Name, G.Description AS GodownDesc,  P.Description AS ProcessDesc, I.Description AS ItemDesc, " & _
            '       " " & Unit1Head & " AS Unit1Head, " & Unit1DecimalPlace & " AS Unit1DecimalPlace, " & Unit1 & " AS Unit1, " & _
            '       " '" & ReportFrm.FGetText(2) & "' As GroupOn1Head, " & mGroupOn1Value & " AS GroupOnValue1, " & _
            '       " " & mShowForHead1 & " AS mShowForHead1, " & mShowForHead2 & " AS mShowForHead2, " & mShowForHead3 & " AS mShowForHead3, " & _
            '       " " & mShowForValue1 & " AS mShowForValue1, " & mShowForValue2 & " AS mShowForValue2, " & mShowForValue3 & " AS mShowForValue3 " & _
            '       " FROM ( " & mStockQry & " ) As V1 " & _
            '       " LEFT JOIN SiteMast SM ON SM.Code = V1.Site_Code " & _
            '       " LEFT JOIN Godown G ON G.Code = V1.Godown " & _
            '       " LEFT JOIN Process P ON P.NCat = V1.Process " & _
            '       " LEFT JOIN Dimension1 D1 ON D1.Code = V1.Dimension1  " & _
            '       " LEFT JOIN Dimension2 D2 ON D2.Code = V1.Dimension2 " & _
            '       " LEFT JOIN Item I ON I.Code = V1.Item " & _
            '       " LEFT JOIN Unit U On I.Unit = U.Code " & _
            '       " LEFT JOIN Unit MU On I.MeasureUnit = MU.Code " & _
            '       " Left Join " & _
            '       " ( SELECT L.Item, sum(L.Qty) AS Qty,sum(L.Net_Amount) AS Amount, Round(sum(L.Net_Amount)/sum(L.Qty),2)   AS AvgRate  " & _
            '       " FROM PurchInvoice H " & _
            '       " LEFT JOIN PurchInvoiceDetail L ON L.DocId = H.DocID  " & _
            '       " WHERE H.V_Date BETWEEN '01/Apr/2014' AND '31/Mar/2015' " & _
            '       " GROUP BY L.Item " & _
            '       " ) VInv on VInv.Item =  V1.Item "

            mQry = " SELECT V1.*, VInv.PurchRate, isnull(VInv.PurchRate,0)*V1.BalUnit1 AS PurchAmount, SM.Name AS Site_Name, G.Description AS GodownDesc,  P.Description AS ProcessDesc, I.Description AS ItemDesc, " & _
           " " & Unit1Head & " AS Unit1Head, " & Unit1DecimalPlace & " AS Unit1DecimalPlace, " & Unit1 & " AS Unit1, " & _
           " '" & ReportFrm.FGetText(2) & "' As GroupOn1Head, " & mGroupOn1Value & " AS GroupOnValue1, " & _
           " " & mShowForHead1 & " AS mShowForHead1, " & mShowForHead2 & " AS mShowForHead2, " & mShowForHead3 & " AS mShowForHead3, " & _
           " " & mShowForValue1 & " AS mShowForValue1, " & mShowForValue2 & " AS mShowForValue2, " & mShowForValue3 & " AS mShowForValue3 " & _
           " FROM ( " & mStockQry & " ) As V1 " & _
           " LEFT JOIN SiteMast SM ON SM.Code = V1.Site_Code " & _
           " LEFT JOIN Godown G ON G.Code = V1.Godown " & _
           " LEFT JOIN Process P ON P.NCat = V1.Process " & _
           " LEFT JOIN Dimension1 D1 ON D1.Code = V1.Dimension1  " & _
           " LEFT JOIN Dimension2 D2 ON D2.Code = V1.Dimension2 " & _
           " LEFT JOIN Item I ON I.Code = V1.Item " & _
           " LEFT JOIN Unit U On I.Unit = U.Code " & _
           " LEFT JOIN Unit MU On I.MeasureUnit = MU.Code " & _
           " Left Join " & _
           " ( SELECT X.Item, X.Rate AS PurchRate " & _
            " FROM " & _
            " ( " & _
            " SELECT  L.Item, L.Rate, H.V_Date, row_number() OVER (PARTITION BY L.Item ORDER BY H.V_Date DESC ) AS Sr " & _
            " FROM PurchInvoice H " & _
            " LEFT JOIN PurchInvoiceDetail L ON L.DocId = H.DocID  " & _
            " WHERE H.V_Date <= " & AgL.Chk_Text(ReportFrm.FGetText(0)) & " " & _
            " ) X " & _
            " WHERE X.Sr =1 " & _
            " ) VInv on VInv.Item =  V1.Item "

            RepName = RepName & "_WithAvgRate"
            RepTitle = "Stock Balance With Last Purchase Rate"
            DsRep = AgL.FillData(mQry, AgL.GCn)
            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records to Print!")
            ReportFrm.PrintReport(DsRep, RepName, RepTitle, ReportPath)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub
#End Region

End Class
