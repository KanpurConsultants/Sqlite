Public Class ClsFunction
    Dim WithEvents ObjRepFormGlobal As AgLibrary.RepFormGlobal
    Dim WithEvents ReportFrm As ReportLayout.FrmReportLayout
    Dim CRepProc As ClsReportProcedures

    Public Function FOpen(ByVal StrSender As String, ByVal StrSenderText As String, Optional ByVal IsEntryPoint As Boolean = True, Optional ByVal StrSenderModule As String = "")
        Dim FrmObj As Form
        Dim StrUserPermission As String
        Dim DTUP As New DataTable
        Dim ADMain As OleDb.OleDbDataAdapter = Nothing
        Dim MDI As New MDIMain

        'For User Permission Open
        If StrSenderModule <> "" Then
            StrUserPermission = AgIniVar.FunGetUserPermission(StrSenderModule, StrSender, StrSenderText, DTUP)
        Else
            StrUserPermission = AgIniVar.FunGetUserPermission(ClsMain.ModuleName, StrSender, StrSenderText, DTUP)
        End If
        ''For User Permission End 

        If IsEntryPoint Then
            Select Case StrSender
                Case MDI.MnuAdjustmentIssueEntry.Name
                    FrmObj = New FrmAdjustmentIssue(StrUserPermission, DTUP, "ADISS")

                Case MDI.MnuAdjustSaleInvoices.Name
                    FrmObj = New FrmSaleInvoiceAdj

                Case MDI.MnuGodownMaster.Name
                    FrmObj = New Store.FrmGodown(StrUserPermission, DTUP)


                Case MDI.MnuSaleInvoiceDetailEntry.Name
                    FrmObj = New FrmSaleInvoiceDetail(StrUserPermission, DTUP)

                Case MDI.MnuSaleInvoice.Name
                    FrmObj = New FrmSaleInvoiceDirect(StrUserPermission, DTUP, AgTemplate.ClsMain.Temp_NCat.SaleInvoice)

                Case MDI.MnuOpeningStockEntry.Name
                    FrmObj = New FrmStoreReceive(StrUserPermission, DTUP, AgTemplate.ClsMain.Temp_NCat.StockOpening)

                Case MDI.MnuDimension1.Name
                    FrmObj = New FrmDimension1(StrUserPermission, DTUP)

                Case MDI.MnuManufacturerMaster.Name
                    FrmObj = New FrmManufacturer(StrUserPermission, DTUP)

                Case MDI.MnuRateList.Name
                    FrmObj = New FrmRateList(StrUserPermission, DTUP)

                Case MDI.MnuItemGroupMaster.Name
                    FrmObj = New FrmItemGroup(StrUserPermission, DTUP)

                Case MDI.MnuItemCategoryMaster.Name
                    FrmObj = New FrmItemCategory(StrUserPermission, DTUP)

                Case MDI.MnuItemMaster.Name
                    If ClsMain.IsScopeOfWorkContains("CLOTH") Then
                        FrmObj = New FrmItemMaster_Cloth(StrUserPermission, DTUP)
                    Else
                        FrmObj = New FrmItem(StrUserPermission, DTUP)
                    End If

                Case MDI.MnuCityMaster.Name
                    FrmObj = New FrmCity(StrUserPermission, DTUP)

                Case MDI.MnuStateMaster.Name
                    FrmObj = New FrmState(StrUserPermission, DTUP)

                Case MDI.MnuGodownMaster.Name
                    FrmObj = New FrmGodown(StrUserPermission, DTUP)

                Case MDI.MnuAreaMaster.Name
                    FrmObj = New FrmArea(StrUserPermission, DTUP)

                Case MDI.MnuDepartmentMaster.Name
                    FrmObj = New FrmDepartment(StrUserPermission, DTUP)

                Case MDI.MnuRateTypeMaster.Name
                    FrmObj = New FrmRateType(StrUserPermission, DTUP)

                Case MDI.MnuAgentMaster.Name
                    FrmObj = New FrmParty(StrUserPermission, DTUP)
                    CType(FrmObj, FrmParty).MasterType = ClsMain.MasterType.Agent
                    CType(FrmObj, FrmParty).SubGroupNature = FrmParty.ESubgroupNature.Supplier

                Case MDI.MnuCustomerMaster.Name
                    FrmObj = New FrmParty(StrUserPermission, DTUP)
                    CType(FrmObj, FrmParty).MasterType = ClsMain.MasterType.Customer
                    CType(FrmObj, FrmParty).SubGroupNature = FrmParty.ESubgroupNature.Customer

                Case MDI.MnuSupplierMaster.Name
                    FrmObj = New FrmParty(StrUserPermission, DTUP)
                    CType(FrmObj, FrmParty).MasterType = ClsMain.MasterType.Supplier
                    CType(FrmObj, FrmParty).SubGroupNature = FrmParty.ESubgroupNature.Supplier

                Case Else
                    FrmObj = Nothing
            End Select
        Else
            ReportFrm = New ReportLayout.FrmReportLayout("", "", StrSenderText, "")
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



    Public Sub UpdateTableStructure()

        FCreateTable_Company()
        FCreateTable_Division()
        FCreateTable_Area()
        FCreateTable_State()
        FCreateTable_City()
        FCreateTable_SiteMast()
        FCreateTable_UserMast()
        FCreateTable_UserSite()
        FCreateTable_User_Permission()
        FCreateTable_User_Control_Permission()
        FCreateTable_PostingGroupSalesTaxItem()
        FCreateTable_PostingGroupSalesTaxParty()
        FCreateTable_Voucher_Type()
        FCreateTable_User_Exclude_VType()
        FCreateTable_User_Exclude_VTypeDetail()
        FCreateTable_Voucher_Prefix()
        FCreateTable_Reason()
        FCreateTable_CustomFields()
        FCreateTable_CustomFieldsHead()
        FCreateTable_CustomFieldsDetail()
        FCreateTable_CustomFields_Trans()
        FCreateTable_LogTable()
        FCreateTable_Process()
        FCreateTable_CostCenterMast()
        FCreateTable_SubGroupType()
        FCreateTable_AcGroup()
        FCreateTable_Subgroup()
        FCreateTable_Enviro()
        FCreateTable_Unit()
        FCreateTable_Department()
        FCreateTable_ItemType()
        FCreateTable_ItemCategory()
        FCreateTable_ItemGroup()
        FCreateTable_Item()
        FCreateTable_Dimension1()
        FCreateTable_Dimension2()
        FCreateTable_Dimension3()
        FCreateTable_Dimension4()
        FCreateTable_UnitConversion()
        FCreateTable_BomDetail()
        FCreateTable_RateType()
        FCreateTable_RateList()
        FCreateTable_RateListDetail()
        FCreateTable_StockHeadSetting()
        FCreateTable_StockHead()
        FCreateTable_StockHeadDetail()
        FCreateTable_Stock()
        FCreateTable_StockProcess()
        FCreateTable_StockAdj()




        FCreateView_ViewStockHeadSetting()
        FCreateView_User_VType_Permission()




        FSeedTable_Company()
        FSeedTable_State()
        FSeedTable_Division()
        FSeedTable_SiteMast()
        FSeedTable_UserMast()
        FSeedTable_UserSite()
        FSeedTable_User_Permission()
        FSeedTable_PostingGroupSalesTaxItem()
        FSeedTable_PostingGroupSalesTaxParty()
        FSeedTable_AcGroup()
        FSeedTable_Subgroup()
        FSeedTable_Enviro()
        FSeedTable_Unit()
        FSeedTable_ItemType()
        FSeedTable_RateType()
        FSeedTable_Voucher_Type()

    End Sub

    Private Sub FCreateView_ViewStockHeadSetting()
        Dim mQry As String
        AgL.Dman_ExecuteNonQry("Drop View IF Exists viewStockHeadSetting;", AgL.GcnMain)

        mQry = "
                CREATE VIEW viewStockHeadSetting AS
                Select  VT.V_Type as Voucher_Type, D.Div_Code as Division, S.Code as Site, 
                IfNull(H.IsPostedInStock,0) IsPostedInStock, IfNull(H.IsPostedInStockProcess,0) IsPostedInStockProcess, 
                IfNull(H.IsVisible_ItemCode,0) IsVisible_ItemCode, IfNull(H.IsVisible_Specification,1) IsVisible_Specification, 
                IfNull(H.IsVisible_Dimension1,0) IsVisible_Dimension1, IfNull(H.IsVisible_Dimension2,0) IsVisible_Dimension2, 
                IfNull(H.IsVisible_Dimension3,0) IsVisible_Dimension3, IfNull(H.IsVisible_Dimension4,0) IsVisible_Dimension4, 
                IfNull(H.IsVisible_Manufacturer,0) IsVisible_Manufacturer, IfNull(H.IsVisible_LotNo,0) IsVisible_LotNo, IfNull(H.IsVisible_BaleNo,0) IsVisible_BaleNo, 
                IfNull(H.IsVisible_Process,0) IsVisible_Process, IfNull(H.IsVisible_ProcessLine,0) IsVisible_ProcessLine, 
                IfNull(H.IsEditable_ProcessLine,0) IsEditable_ProcessLine, IfNull(H.IsMandatory_ProcessLine,0) IsMandatory_ProcessLine, IfNull(H.IsVisible_MeasurePerPcs,0) IsVisible_MeasurePerPcs, 
                IfNull(H.IsEditable_MeasurePerPcs,0) IsEditable_MeasurePerPcs, IfNull(H.IsVisible_Measure,0) IsVisible_Measure, 
                IfNull(H.IsEditable_Measure,0) IsEditable_Measure, IfNull(H.IsVisible_MeasureUnit,0) IsVisible_MeasureUnit, 
                IfNull(H.IsEditable_MeasureUnit,0) IsEditable_MeasureUnit, IfNull(H.IsVisible_Rate,1) IsVisible_Rate, 
                IfNull(H.IsEditable_Rate,1) IsEditable_Rate, H.ItemHelpType ItemHelpType, H.FilterInclude_Process FilterInclude_Process, 
                H.FilterExclude_AcGroup FilterExclude_AcGroup, H.FilterInclude_ItemType FilterInclude_ItemType, 
                H.FilterInclude_ItemGroup FilterInclude_ItemGroup, H.FilterExclude_ItemGroup FilterExclude_ItemGroup, 
                H.FilterInclude_Item FilterInclude_Item, H.FilterExclude_Item FilterExclude_Item, H.FilterInclude_AcGroup FilterInclude_AcGroup
                from Voucher_Type Vt, Division D, SiteMast S  
                Left join StockHeadSetting H On  H.V_Type = Vt.V_Type And H.Div_Code=D.Div_Code And H.Site_Code = S.Code
                "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
    End Sub

    Private Sub FCreateView_User_VType_Permission()
        Dim mQry As String
        AgL.Dman_ExecuteNonQry("Drop View IF Exists User_VType_Permission;", AgL.GcnMain)

        mQry = "
                CREATE VIEW User_VType_Permission AS
                SELECT H.UserName, H.Div_Code, H.Site_Code,L.V_Type 
                FROM [User_Exclude_VType] H
                LEFT JOIN User_Exclude_VTypeDetail L ON H.Code = L.Code;
                "
        AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
    End Sub
    Private Sub FCreateTable_User_Exclude_VType()
        Dim mQry As String
        If Not AgL.IsTableExist("User_Exclude_VType", AgL.GcnMain) Then
            mQry = "
                    CREATE TABLE [User_Exclude_VType] (
                       [Code] nvarchar(10) NOT NULL COLLATE NOCASE,
                       [UserName] nvarchar(10) COLLATE NOCASE,
                       [IsDeleted] bit,
                       [EntryBy] nvarchar(10) COLLATE NOCASE,
                       [EntryDate] datetime,
                       [EntryType] nvarchar(10) COLLATE NOCASE,
                       [EntryStatus] nvarchar(10) COLLATE NOCASE,
                       [ApproveBy] nvarchar(10) COLLATE NOCASE,
                       [ApproveDate] datetime,
                       [MoveToLog] nvarchar(10) COLLATE NOCASE,
                       [MoveToLogDate] datetime,
                       [Status] nvarchar(10) COLLATE NOCASE,
                       [Div_Code] nvarchar(1) COLLATE NOCASE,
                       [Site_Code] nvarchar(2) COLLATE NOCASE,
                       [UID] uniqueidentifier COLLATE NOCASE,
                       PRIMARY KEY ([Code])
                    );          
                    "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If
    End Sub

    Private Sub FCreateTable_User_Exclude_VTypeDetail()
        Dim mQry As String
        If Not AgL.IsTableExist("User_Exclude_VTypeDetail", AgL.GcnMain) Then
            mQry = "
                    CREATE TABLE [User_Exclude_VTypeDetail] (
                       [Code] nvarchar(10) NOT NULL COLLATE NOCASE,
                       [Sr] int NOT NULL,
                       [V_Type] nvarchar(5) COLLATE NOCASE,
                       [UID] uniqueidentifier COLLATE NOCASE,
                       [UserName] nvarchar(10) COLLATE NOCASE,
                       PRIMARY KEY ([Code], [Sr]),
                       CONSTRAINT [FK_User_Exclude_VTypeDetail_Voucher_Type_V_Type] FOREIGN KEY ([V_Type])
                          REFERENCES [Voucher_Type]([V_Type]) ON DELETE NO ACTION ON UPDATE NO ACTION
                    );       
                    "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If
    End Sub

    Private Sub FCreateTable_State()
        Dim mQry As String
        If Not AgL.IsTableExist("State", AgL.GcnMain) Then
            mQry = "
                    CREATE TABLE [State] (
                       [Code] nvarchar(10) NOT NULL COLLATE NOCASE,
                       [Description] nvarchar(50) COLLATE NOCASE,
                       [IsDeleted] bit,
                       [EntryBy] nvarchar(10) COLLATE NOCASE,
                       [EntryDate] datetime,
                       [EntryType] nvarchar(10) COLLATE NOCASE,
                       [EntryStatus] nvarchar(10) COLLATE NOCASE,
                       [ApproveBy] nvarchar(10) COLLATE NOCASE,
                       [ApproveDate] datetime,
                       [MoveToLog] nvarchar(10) COLLATE NOCASE,
                       [MoveToLogDate] datetime,
                       [Status] nvarchar(10) COLLATE NOCASE,
                       [Div_Code] nvarchar(1) COLLATE NOCASE,
                       [UID] uniqueidentifier COLLATE NOCASE,
                       [ManualCode] nvarchar(20) COLLATE NOCASE,
                       PRIMARY KEY ([Code])
                    );

          
                    "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If
    End Sub

    Private Sub FSeedTable_State()
        Dim mQry As String

        If AgL.FillData("Select * from State limit 1", AgL.GcnMain).tables(0).Rows.Count = 0 Then
            mQry = " 
                    INSERT INTO State
                    (Code, Description, IsDeleted, EntryBy, EntryDate, EntryType, EntryStatus, ApproveBy, ApproveDate, MoveToLog, MoveToLogDate, Status, Div_Code, UID, ManualCode)
                    VALUES('D10001', 'JAMMU AND KASHMIR', NULL, 'SUPER', '2018-02-25', 'Add', 'Open', NULL, NULL, NULL, NULL, 'Active', 'D', NULL, '01');

                    INSERT INTO State
                    (Code, Description, IsDeleted, EntryBy, EntryDate, EntryType, EntryStatus, ApproveBy, ApproveDate, MoveToLog, MoveToLogDate, Status, Div_Code, UID, ManualCode)
                    VALUES('D10002', 'HIMACHAL PRADESH', NULL, 'SUPER', '2018-02-25', 'Add', 'Open', NULL, NULL, NULL, NULL, 'Active', 'D', NULL, '02');

                    INSERT INTO State
                    (Code, Description, IsDeleted, EntryBy, EntryDate, EntryType, EntryStatus, ApproveBy, ApproveDate, MoveToLog, MoveToLogDate, Status, Div_Code, UID, ManualCode)
                    VALUES('D10003', 'PUNJAB', NULL, 'SUPER', '2018-02-25', 'Add', 'Open', NULL, NULL, NULL, NULL, 'Active', 'D', NULL, '03');

                    INSERT INTO State
                    (Code, Description, IsDeleted, EntryBy, EntryDate, EntryType, EntryStatus, ApproveBy, ApproveDate, MoveToLog, MoveToLogDate, Status, Div_Code, UID, ManualCode)
                    VALUES('D10004', 'CHANDIGARH', NULL, 'SUPER', '2018-02-25', 'Add', 'Open', NULL, NULL, NULL, NULL, 'Active', 'D', NULL, '04');

                    INSERT INTO State
                    (Code, Description, IsDeleted, EntryBy, EntryDate, EntryType, EntryStatus, ApproveBy, ApproveDate, MoveToLog, MoveToLogDate, Status, Div_Code, UID, ManualCode)
                    VALUES('D10005', 'UTTARAKHAND', NULL, 'SUPER', '2018-02-25', 'Add', 'Open', NULL, NULL, NULL, NULL, 'Active', 'D', NULL, '05');

                    INSERT INTO State
                    (Code, Description, IsDeleted, EntryBy, EntryDate, EntryType, EntryStatus, ApproveBy, ApproveDate, MoveToLog, MoveToLogDate, Status, Div_Code, UID, ManualCode)
                    VALUES('D10006', 'HARYANA', NULL, 'SUPER', '2018-02-25', 'Add', 'Open', NULL, NULL, NULL, NULL, 'Active', 'D', NULL, '06');

                    INSERT INTO State
                    (Code, Description, IsDeleted, EntryBy, EntryDate, EntryType, EntryStatus, ApproveBy, ApproveDate, MoveToLog, MoveToLogDate, Status, Div_Code, UID, ManualCode)
                    VALUES('D10007', 'DELHI', NULL, 'SUPER', '2018-02-25', 'Add', 'Open', NULL, NULL, NULL, NULL, 'Active', 'D', NULL, '07');

                    INSERT INTO State
                    (Code, Description, IsDeleted, EntryBy, EntryDate, EntryType, EntryStatus, ApproveBy, ApproveDate, MoveToLog, MoveToLogDate, Status, Div_Code, UID, ManualCode)
                    VALUES('D10008', 'RAJASTHAN', NULL, 'SUPER', '2018-02-25', 'Add', 'Open', NULL, NULL, NULL, NULL, 'Active', 'D', NULL, '08');

                    INSERT INTO State
                    (Code, Description, IsDeleted, EntryBy, EntryDate, EntryType, EntryStatus, ApproveBy, ApproveDate, MoveToLog, MoveToLogDate, Status, Div_Code, UID, ManualCode)
                    VALUES('D10009', 'UTTAR PRADESH', NULL, 'SUPER', '2018-02-25', 'Add', 'Open', NULL, NULL, NULL, NULL, 'Active', 'D', NULL, '09');

                    INSERT INTO State
                    (Code, Description, IsDeleted, EntryBy, EntryDate, EntryType, EntryStatus, ApproveBy, ApproveDate, MoveToLog, MoveToLogDate, Status, Div_Code, UID, ManualCode)
                    VALUES('D10010', 'BIHAR', NULL, 'SUPER', '2018-02-25', 'Add', 'Open', NULL, NULL, NULL, NULL, 'Active', 'D', NULL, '10');

                    INSERT INTO State
                    (Code, Description, IsDeleted, EntryBy, EntryDate, EntryType, EntryStatus, ApproveBy, ApproveDate, MoveToLog, MoveToLogDate, Status, Div_Code, UID, ManualCode)
                    VALUES('D10011', 'SIKKIM', NULL, 'SUPER', '2018-02-25', 'Add', 'Open', NULL, NULL, NULL, NULL, 'Active', 'D', NULL, '11');

                    INSERT INTO State
                    (Code, Description, IsDeleted, EntryBy, EntryDate, EntryType, EntryStatus, ApproveBy, ApproveDate, MoveToLog, MoveToLogDate, Status, Div_Code, UID, ManualCode)
                    VALUES('D10012', 'ARUNACHAL PRADESH', NULL, 'SUPER', '2018-02-25', 'Add', 'Open', NULL, NULL, NULL, NULL, 'Active', 'D', NULL, '12');

                    INSERT INTO State
                    (Code, Description, IsDeleted, EntryBy, EntryDate, EntryType, EntryStatus, ApproveBy, ApproveDate, MoveToLog, MoveToLogDate, Status, Div_Code, UID, ManualCode)
                    VALUES('D10013', 'NAGALAND', NULL, 'SUPER', '2018-02-25', 'Add', 'Open', NULL, NULL, NULL, NULL, 'Active', 'D', NULL, '13');

                    INSERT INTO State
                    (Code, Description, IsDeleted, EntryBy, EntryDate, EntryType, EntryStatus, ApproveBy, ApproveDate, MoveToLog, MoveToLogDate, Status, Div_Code, UID, ManualCode)
                    VALUES('D10014', 'MANIPUR', NULL, 'SUPER', '2018-02-25', 'Add', 'Open', NULL, NULL, NULL, NULL, 'Active', 'D', NULL, '14');

                    INSERT INTO State
                    (Code, Description, IsDeleted, EntryBy, EntryDate, EntryType, EntryStatus, ApproveBy, ApproveDate, MoveToLog, MoveToLogDate, Status, Div_Code, UID, ManualCode)
                    VALUES('D10015', 'MIZORAM', NULL, 'SUPER', '2018-02-25', 'Add', 'Open', NULL, NULL, NULL, NULL, 'Active', 'D', NULL, '15');

                    INSERT INTO State
                    (Code, Description, IsDeleted, EntryBy, EntryDate, EntryType, EntryStatus, ApproveBy, ApproveDate, MoveToLog, MoveToLogDate, Status, Div_Code, UID, ManualCode)
                    VALUES('D10016', 'TRIPURA', NULL, 'SUPER', '2018-02-25', 'Add', 'Open', NULL, NULL, NULL, NULL, 'Active', 'D', NULL, '16');

                    INSERT INTO State
                    (Code, Description, IsDeleted, EntryBy, EntryDate, EntryType, EntryStatus, ApproveBy, ApproveDate, MoveToLog, MoveToLogDate, Status, Div_Code, UID, ManualCode)
                    VALUES('D10017', 'MEGHLAYA', NULL, 'SUPER', '2018-02-25', 'Add', 'Open', NULL, NULL, NULL, NULL, 'Active', 'D', NULL, '17');

                    INSERT INTO State
                    (Code, Description, IsDeleted, EntryBy, EntryDate, EntryType, EntryStatus, ApproveBy, ApproveDate, MoveToLog, MoveToLogDate, Status, Div_Code, UID, ManualCode)
                    VALUES('D10018', 'ASSAM', NULL, 'SUPER', '2018-02-25', 'Add', 'Open', NULL, NULL, NULL, NULL, 'Active', 'D', NULL, '18');

                    INSERT INTO State
                    (Code, Description, IsDeleted, EntryBy, EntryDate, EntryType, EntryStatus, ApproveBy, ApproveDate, MoveToLog, MoveToLogDate, Status, Div_Code, UID, ManualCode)
                    VALUES('D10019', 'WEST BENGAL', NULL, 'SUPER', '2018-02-25', 'Add', 'Open', NULL, NULL, NULL, NULL, 'Active', 'D', NULL, '19');

                    INSERT INTO State
                    (Code, Description, IsDeleted, EntryBy, EntryDate, EntryType, EntryStatus, ApproveBy, ApproveDate, MoveToLog, MoveToLogDate, Status, Div_Code, UID, ManualCode)
                    VALUES('D10020', 'JHARKHAND', NULL, 'SUPER', '2018-02-25', 'Add', 'Open', NULL, NULL, NULL, NULL, 'Active', 'D', NULL, '20');

                    INSERT INTO State
                    (Code, Description, IsDeleted, EntryBy, EntryDate, EntryType, EntryStatus, ApproveBy, ApproveDate, MoveToLog, MoveToLogDate, Status, Div_Code, UID, ManualCode)
                    VALUES('D10021', 'ODISHA', NULL, 'SUPER', '2018-02-25', 'Add', 'Open', NULL, NULL, NULL, NULL, 'Active', 'D', NULL, '21');

                    INSERT INTO State
                    (Code, Description, IsDeleted, EntryBy, EntryDate, EntryType, EntryStatus, ApproveBy, ApproveDate, MoveToLog, MoveToLogDate, Status, Div_Code, UID, ManualCode)
                    VALUES('D10022', 'CHATTISGARH', NULL, 'SUPER', '2018-02-25', 'Add', 'Open', NULL, NULL, NULL, NULL, 'Active', 'D', NULL, '22');

                    INSERT INTO State
                    (Code, Description, IsDeleted, EntryBy, EntryDate, EntryType, EntryStatus, ApproveBy, ApproveDate, MoveToLog, MoveToLogDate, Status, Div_Code, UID, ManualCode)
                    VALUES('D10023', 'MADHYA PRADESH', NULL, 'SUPER', '2018-02-25', 'Add', 'Open', NULL, NULL, NULL, NULL, 'Active', 'D', NULL, '23');

                    INSERT INTO State
                    (Code, Description, IsDeleted, EntryBy, EntryDate, EntryType, EntryStatus, ApproveBy, ApproveDate, MoveToLog, MoveToLogDate, Status, Div_Code, UID, ManualCode)
                    VALUES('D10024', 'GUJARAT', NULL, 'SUPER', '2018-02-25', 'Add', 'Open', NULL, NULL, NULL, NULL, 'Active', 'D', NULL, '24');

                    INSERT INTO State
                    (Code, Description, IsDeleted, EntryBy, EntryDate, EntryType, EntryStatus, ApproveBy, ApproveDate, MoveToLog, MoveToLogDate, Status, Div_Code, UID, ManualCode)
                    VALUES('D10025', 'DAMAN AND DIU', NULL, 'SUPER', '2018-02-25', 'Add', 'Open', NULL, NULL, NULL, NULL, 'Active', 'D', NULL, '25');

                    INSERT INTO State
                    (Code, Description, IsDeleted, EntryBy, EntryDate, EntryType, EntryStatus, ApproveBy, ApproveDate, MoveToLog, MoveToLogDate, Status, Div_Code, UID, ManualCode)
                    VALUES('D10026', 'DADRA AND NAGAR HAVELI', NULL, 'SUPER', '2018-02-25', 'Add', 'Open', NULL, NULL, NULL, NULL, 'Active', 'D', NULL, '26');

                    INSERT INTO State
                    (Code, Description, IsDeleted, EntryBy, EntryDate, EntryType, EntryStatus, ApproveBy, ApproveDate, MoveToLog, MoveToLogDate, Status, Div_Code, UID, ManualCode)
                    VALUES('D10027', 'MAHARASHTRA', NULL, 'SUPER', '2018-02-25', 'Add', 'Open', NULL, NULL, NULL, NULL, 'Active', 'D', NULL, '27');

                    INSERT INTO State
                    (Code, Description, IsDeleted, EntryBy, EntryDate, EntryType, EntryStatus, ApproveBy, ApproveDate, MoveToLog, MoveToLogDate, Status, Div_Code, UID, ManualCode)
                    VALUES('D10028', 'ANDHRA PRADESH(BEFORE DIVISION)', NULL, 'SUPER', '2018-02-25', 'Add', 'Open', NULL, NULL, NULL, NULL, 'Active', 'D', NULL, '28');

                    INSERT INTO State
                    (Code, Description, IsDeleted, EntryBy, EntryDate, EntryType, EntryStatus, ApproveBy, ApproveDate, MoveToLog, MoveToLogDate, Status, Div_Code, UID, ManualCode)
                    VALUES('D10029', 'KARNATAKA', NULL, 'SUPER', '2018-02-25', 'Add', 'Open', NULL, NULL, NULL, NULL, 'Active', 'D', NULL, '29');

                    INSERT INTO State
                    (Code, Description, IsDeleted, EntryBy, EntryDate, EntryType, EntryStatus, ApproveBy, ApproveDate, MoveToLog, MoveToLogDate, Status, Div_Code, UID, ManualCode)
                    VALUES('D10030', 'GOA', NULL, 'SUPER', '2018-02-25', 'Add', 'Open', NULL, NULL, NULL, NULL, 'Active', 'D', NULL, '30');

                    INSERT INTO State
                    (Code, Description, IsDeleted, EntryBy, EntryDate, EntryType, EntryStatus, ApproveBy, ApproveDate, MoveToLog, MoveToLogDate, Status, Div_Code, UID, ManualCode)
                    VALUES('D10031', 'LAKSHWADEEP', NULL, 'SUPER', '2018-02-25', 'Add', 'Open', NULL, NULL, NULL, NULL, 'Active', 'D', NULL, '31');

                    INSERT INTO State
                    (Code, Description, IsDeleted, EntryBy, EntryDate, EntryType, EntryStatus, ApproveBy, ApproveDate, MoveToLog, MoveToLogDate, Status, Div_Code, UID, ManualCode)
                    VALUES('D10032', 'KERALA', NULL, 'SUPER', '2018-02-25', 'Add', 'Open', NULL, NULL, NULL, NULL, 'Active', 'D', NULL, '32');

                    INSERT INTO State
                    (Code, Description, IsDeleted, EntryBy, EntryDate, EntryType, EntryStatus, ApproveBy, ApproveDate, MoveToLog, MoveToLogDate, Status, Div_Code, UID, ManualCode)
                    VALUES('D10033', 'TAMIL NADU', NULL, 'SUPER', '2018-02-25', 'Add', 'Open', NULL, NULL, NULL, NULL, 'Active', 'D', NULL, '33');

                    INSERT INTO State
                    (Code, Description, IsDeleted, EntryBy, EntryDate, EntryType, EntryStatus, ApproveBy, ApproveDate, MoveToLog, MoveToLogDate, Status, Div_Code, UID, ManualCode)
                    VALUES('D10034', 'PUDUCHERRY', NULL, 'SUPER', '2018-02-25', 'Add', 'Open', NULL, NULL, NULL, NULL, 'Active', 'D', NULL, '34');

                    INSERT INTO State
                    (Code, Description, IsDeleted, EntryBy, EntryDate, EntryType, EntryStatus, ApproveBy, ApproveDate, MoveToLog, MoveToLogDate, Status, Div_Code, UID, ManualCode)
                    VALUES('D10035', 'ANDAMAN AND NICOBAR ISLANDS', NULL, 'SUPER', '2018-02-25', 'Add', 'Open', NULL, NULL, NULL, NULL, 'Active', 'D', NULL, '35');

                    INSERT INTO State
                    (Code, Description, IsDeleted, EntryBy, EntryDate, EntryType, EntryStatus, ApproveBy, ApproveDate, MoveToLog, MoveToLogDate, Status, Div_Code, UID, ManualCode)
                    VALUES('D10036', 'TELANGANA', NULL, 'SUPER', '2018-02-25', 'Add', 'Open', NULL, NULL, NULL, NULL, 'Active', 'D', NULL, '36');

                    INSERT INTO State
                    (Code, Description, IsDeleted, EntryBy, EntryDate, EntryType, EntryStatus, ApproveBy, ApproveDate, MoveToLog, MoveToLogDate, Status, Div_Code, UID, ManualCode)
                    VALUES('D10037', 'ANDHRA PRADESH (NEW)', NULL, 'SUPER', '2018-02-25', 'Add', 'Open', NULL, NULL, NULL, NULL, 'Active', 'D', NULL, '37');
                "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If
    End Sub

    Private Sub FCreateTable_BomDetail()
        Dim mQry As String
        If Not AgL.IsTableExist("BOMDetail", AgL.GcnMain) Then
            mQry = "
                    CREATE TABLE [BOMDetail] (
                       [Code] nvarchar(10) COLLATE NOCASE,
                       [Sr] int,
                       [Process] nvarchar(10) COLLATE NOCASE,
                       [Item] nvarchar(10) COLLATE NOCASE,
                       [Qty] float,
                       [ConsumptionPer] float,
                       [ApplyIn] nvarchar(20) COLLATE NOCASE,
                       [Uid] uniqueidentifier COLLATE NOCASE,
                       [Unit] nvarchar(10) COLLATE NOCASE,
                       [WastagePer] float,
                       [FromProcess] nvarchar(10) COLLATE NOCASE,
                       [BaseItem] nvarchar(10) COLLATE NOCASE,
                       [BatchQty] float,
                       [BatchUnit] nvarchar(10) COLLATE NOCASE,
                       [Specification] nvarchar(100) COLLATE NOCASE,
                       [IsMarkedForMainItem] bit,
                       [Dimension1] nvarchar(10) COLLATE NOCASE,
                       [Dimension2] nvarchar(10) COLLATE NOCASE,
                       CONSTRAINT [FK_BOMDetail_Bom_Code] FOREIGN KEY ([Code])
                          REFERENCES [BOM]([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION,
                       CONSTRAINT [FK_BOMDetail_Process_FromProcess] FOREIGN KEY ([FromProcess])
                          REFERENCES [Process]([NCat]) ON DELETE NO ACTION ON UPDATE NO ACTION,
                       CONSTRAINT [FK_BOMDetail_Item_Item] FOREIGN KEY ([Item])
                          REFERENCES [Item]([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION,
                       CONSTRAINT [FK_BOMDetail_Process_Process] FOREIGN KEY ([Process])
                          REFERENCES [Process]([NCat]) ON DELETE NO ACTION ON UPDATE NO ACTION,
                       CONSTRAINT [FK_BOMDetail_Unit_Unit] FOREIGN KEY ([Unit])
                          REFERENCES [Unit]([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION
                    );

          
                    "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If
    End Sub


    Private Sub FCreateTable_UnitConversion()
        Dim mQry As String
        If Not AgL.IsTableExist("UnitConversion", AgL.GcnMain) Then
            mQry = "
                    CREATE TABLE [UnitConversion] (
                       [Code] nvarchar(10) COLLATE NOCASE,
                       [FromUnit] nvarchar(10) COLLATE NOCASE,
                       [ToUnit] nvarchar(20) COLLATE NOCASE,
                       [Multiplier] float,
                       [Rounding] int,
                       [EntryBy] nvarchar(10) COLLATE NOCASE,
                       [EntryDate] datetime,
                       [EntryType] nvarchar(10) COLLATE NOCASE,
                       [EntryStatus] nvarchar(10) COLLATE NOCASE,
                       [ApproveBy] nvarchar(10) COLLATE NOCASE,
                       [ApproveDate] datetime,
                       [MoveToLog] nvarchar(10) COLLATE NOCASE,
                       [MoveToLogDate] datetime,
                       [IsDeleted] bit,
                       [Status] nvarchar(20) COLLATE NOCASE,
                       [Div_Code] nvarchar(1) COLLATE NOCASE,
                       [Item] nvarchar(10) COLLATE NOCASE,
                       [FromQty] float,
                       [ToQty] float,
                       CONSTRAINT [FK_UnitConversion_Unit_FromUnit] FOREIGN KEY ([FromUnit])
                          REFERENCES [Unit]([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION,
                       CONSTRAINT [FK_UnitConversion_Item_Item] FOREIGN KEY ([Item])
                          REFERENCES [Item]([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION
                    );
          
                    "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If
    End Sub


    Private Sub FCreateTable_CustomFields()
        Dim mQry As String
        If Not AgL.IsTableExist("CustomFields", AgL.GcnMain) Then
            mQry = "
                    CREATE TABLE [CustomFields] (
                       [Code] nvarchar(10) NOT NULL COLLATE NOCASE,
                       [Description] nvarchar(50) COLLATE NOCASE,
                       [Type] nvarchar(10) COLLATE NOCASE,
                       [HeaderTable] nvarchar(50) COLLATE NOCASE,
                       [Div_Code] nvarchar(1) COLLATE NOCASE,
                       [Site_Code] nvarchar(2) COLLATE NOCASE,
                       [PreparedBy] nvarchar(10) COLLATE NOCASE,
                       [U_EntDt] datetime,
                       [U_AE] nvarchar(1) COLLATE NOCASE,
                       [ModifiedBy] nvarchar(10) COLLATE NOCASE,
                       [Edit_Date] datetime,
                       [UpLoadDate] datetime,
                       [TableName] nvarchar(100) COLLATE NOCASE,
                       [PrimaryField] nvarchar(100) COLLATE NOCASE,
                       PRIMARY KEY ([Code])
                    );
          
                    "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If
    End Sub


    Private Sub FCreateTable_CustomFieldsHead()
        Dim mQry As String
        If Not AgL.IsTableExist("CustomFieldsHead", AgL.GcnMain) Then
            mQry = "
                    CREATE TABLE [CustomFieldsHead] (
                       [Code] nvarchar(10) NOT NULL COLLATE NOCASE,
                       [Description] nvarchar(50) COLLATE NOCASE,
                       [ManualCode] nvarchar(20) COLLATE NOCASE,
                       [Div_Code] nvarchar(1) COLLATE NOCASE,
                       [Site_Code] nvarchar(2) COLLATE NOCASE,
                       [PreparedBy] nvarchar(10) COLLATE NOCASE,
                       [U_EntDt] datetime,
                       [U_AE] nvarchar(1) COLLATE NOCASE,
                       [ModifiedBy] nvarchar(10) COLLATE NOCASE,
                       [Edit_Date] datetime,
                       [UpLoadDate] datetime,
                       PRIMARY KEY ([Code])
                    );
          
                    "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If
    End Sub

    Private Sub FCreateTable_CustomFieldsDetail()
        Dim mQry As String
        If Not AgL.IsTableExist("CustomFieldsDetail", AgL.GcnMain) Then
            mQry = "
                        CREATE TABLE [CustomFieldsDetail] (
                           [Code] nvarchar(10) NOT NULL COLLATE NOCASE,
                           [Sr] int NOT NULL,
                           [Heads] nvarchar(8) COLLATE NOCASE,
                           [Value_Type] nvarchar(30) COLLATE NOCASE,
                           [FLength] nvarchar(10) COLLATE NOCASE DEFAULT '0',
                           [Value] varchar(2147483647) COLLATE NOCASE,
                           [Default_Value] varchar(2147483647) COLLATE NOCASE,
                           [Active] bit DEFAULT '0',
                           [IsMandatory] bit DEFAULT '0',
                           [Head] nvarchar(100) COLLATE NOCASE,
                           [TableName] nvarchar(100) COLLATE NOCASE,
                           [PrimaryField] nvarchar(100) COLLATE NOCASE,
                           [UpdateField] nvarchar(100) COLLATE NOCASE,
                           [UpdateFieldType] nvarchar(100) COLLATE NOCASE,
                           [HeaderField] nvarchar(100) COLLATE NOCASE,
                           [HeaderFieldDataType] int,
                           [HeaderFieldLength] int,
                           PRIMARY KEY ([Code], [Sr]),
                           CONSTRAINT [FK_CustomFieldsDetail_CustomFields_Code] FOREIGN KEY ([Code])
                              REFERENCES [CustomFields]([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION
                        );
          
                    "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If
    End Sub

    Private Sub FCreateTable_CustomFields_Trans()
        Dim mQry As String
        If Not AgL.IsTableExist("CustomFields_Trans", AgL.GcnMain) Then
            mQry = "

                    CREATE TABLE [CustomFields_Trans] (
                       [UID] uniqueidentifier COLLATE NOCASE,
                       [DocID] nvarchar(21) NOT NULL COLLATE NOCASE,
                       [CustomFields] nvarchar(10) NOT NULL COLLATE NOCASE,
                       [Sr] int NOT NULL,
                       [TSr] int NOT NULL,
                       [Head] nvarchar(8) COLLATE NOCASE,
                       [Value] varchar(2147483647) COLLATE NOCASE,
                       [MnuText] nvarchar(100) COLLATE NOCASE,
                       [Data] varchar(2147483647) COLLATE NOCASE,
                       [Value_Type] nvarchar(30) COLLATE NOCASE,
                       [FLength] nvarchar(10) COLLATE NOCASE,
                       [TableName] nvarchar(100) COLLATE NOCASE,
                       [PrimaryField] nvarchar(100) COLLATE NOCASE,
                       [HeaderField] nvarchar(100) COLLATE NOCASE,
                       [UpdateField] nvarchar(100) COLLATE NOCASE,
                       [UpdateFieldType] nvarchar(30) COLLATE NOCASE,
                       PRIMARY KEY ([DocID], [CustomFields], [Sr], [TSr])
                    );

          
                    "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If
    End Sub

    Private Sub FCreateTable_Voucher_Type()
        Dim mQry As String
        If Not AgL.IsTableExist("Voucher_Type", AgL.GcnMain) Then
            mQry = "
                    CREATE TABLE [Voucher_Type] (
                       [NCat] nvarchar(10) COLLATE NOCASE,
                       [Category] nvarchar(5) COLLATE NOCASE,
                       [V_Type] nvarchar(5) NOT NULL COLLATE NOCASE,
                       [Description] nvarchar(50) COLLATE NOCASE,
                       [Short_Name] nvarchar(10) COLLATE NOCASE,
                       [SystemDefine] nvarchar(1) COLLATE NOCASE,
                       [DivisionWise] bit,
                       [SiteWise] bit,
                       [PreparedBy] nvarchar(10) COLLATE NOCASE,
                       [U_EntDt] datetime,
                       [U_AE] nvarchar(1) COLLATE NOCASE,
                       [ModifiedBy] nvarchar(10) COLLATE NOCASE,
                       [Edit_Date] datetime,
                       [IssRec] nvarchar(3) COLLATE NOCASE,
                       [Description_Help] nvarchar(30) COLLATE NOCASE,
                       [Description_BiLang] nvarchar(30) COLLATE NOCASE,
                       [Short_Name_BiLang] nvarchar(10) COLLATE NOCASE,
                       [Report_Index] nvarchar(3) COLLATE NOCASE,
                       [Number_Method] nvarchar(9) COLLATE NOCASE,
                       [Start_No] float,
                       [Last_Ent_Date] datetime,
                       [Form_Name] nvarchar(1) COLLATE NOCASE,
                       [Saperate_Narr] nvarchar(1) COLLATE NOCASE,
                       [Common_Narr] nvarchar(1) COLLATE NOCASE,
                       [Narration] nvarchar(255) COLLATE NOCASE,
                       [Print_VNo] nvarchar(1) COLLATE NOCASE,
                       [Header_Desc] nvarchar(80) COLLATE NOCASE,
                       [Term_Desc] nvarchar(150) COLLATE NOCASE,
                       [Footer_Desc] nvarchar(150) COLLATE NOCASE,
                       [Exclude_Ac_Grp] nvarchar(100) COLLATE NOCASE,
                       [SerialNo_From_Table] nvarchar(50) COLLATE NOCASE,
                       [U_Name] nvarchar(10) COLLATE NOCASE,
                       [ChqNo] nvarchar(1) COLLATE NOCASE,
                       [ChqDt] nvarchar(1) COLLATE NOCASE,
                       [ClgDt] nvarchar(1) COLLATE NOCASE,
                       [DefaultCrAc] nvarchar(10) COLLATE NOCASE,
                       [DefaultDrAc] nvarchar(10) COLLATE NOCASE,
                       [FirstDrCr] nvarchar(10) COLLATE NOCASE,
                       [TrnType] nvarchar(50) COLLATE NOCASE,
                       [TdsDed] nvarchar(3) COLLATE NOCASE,
                       [ContraNarr] nvarchar(255) COLLATE NOCASE,
                       [TdsOnAmt] nvarchar(50) COLLATE NOCASE,
                       [Contra_Narr] nvarchar(1) COLLATE NOCASE,
                       [Separate_Narr] nvarchar(1) COLLATE NOCASE,
                       [MnuAttachedInModule] nvarchar(100) COLLATE NOCASE,
                       [AuditAllowed] nvarchar(1) COLLATE NOCASE,
                       [UpLoadDate] datetime,
                       [Affect_FA] bit DEFAULT '1',
                       [IsShowVoucherReference] bit,
                       [MnuName] nvarchar(100) COLLATE NOCASE,
                       [MnuText] nvarchar(100) COLLATE NOCASE,
                       [SerialNo] int,
                       [HeaderTable] int,
                       [LogHeaderTable] int,
                       [DefaultAc] nvarchar(10) COLLATE NOCASE,
                       [CustomFields] nvarchar(10) COLLATE NOCASE,
                       [ContraV_Type] nvarchar(5) COLLATE NOCASE,
                       PRIMARY KEY ([V_Type])
                    );
          
                    "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If
    End Sub

    Private Sub FCreateTable_Voucher_Prefix()
        Dim mQry As String
        If Not AgL.IsTableExist("Voucher_Prefix", AgL.GcnMain) Then
            mQry = "
                    CREATE TABLE [Voucher_Prefix] (
                       [V_Type] nvarchar(5) COLLATE NOCASE,
                       [Date_From] datetime,
                       [Prefix] nvarchar(5) COLLATE NOCASE,
                       [Start_Srl_No] bigint,
                       [Date_To] datetime,
                       [Comp_Code] nvarchar(2) COLLATE NOCASE,
                       [Site_Code] nvarchar(2) COLLATE NOCASE,
                       [Div_Code] nvarchar(1) COLLATE NOCASE,
                       [UpLoadDate] datetime,
                       [Status_Add] nvarchar(20) COLLATE NOCASE,
                       [Status_Edit] nvarchar(20) COLLATE NOCASE,
                       [Status_Delete] nvarchar(20) COLLATE NOCASE,
                       [Status_Print] nvarchar(20) COLLATE NOCASE,
                       [Ref_Prefix] nvarchar(5) COLLATE NOCASE,
                       [Ref_PadLength] int,
                       CONSTRAINT [FK_Voucher_Prefix_SiteMast_Site_Code] FOREIGN KEY ([Site_Code])
                          REFERENCES [SiteMast]([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION,
                       CONSTRAINT [FK_Voucher_Prefix_Voucher_Type_V_Type] FOREIGN KEY ([V_Type])
                          REFERENCES [Voucher_Type]([V_Type]) ON DELETE NO ACTION ON UPDATE NO ACTION
                    );          
                    "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If
    End Sub


    Private Sub FCreateTable_LogTable()
        Dim mQry As String
        If Not AgL.IsTableExist("LogTable", AgL.GcnMain) Then
            mQry = "
                    CREATE TABLE [LogTable] (
                       [DocId] nvarchar(36) COLLATE NOCASE,
                       [EntryPoint] nvarchar(100) COLLATE NOCASE,
                       [MachineName] nvarchar(50) COLLATE NOCASE,
                       [U_Name] nvarchar(10) COLLATE NOCASE,
                       [U_EntDt] datetime,
                       [U_AE] nvarchar(1) COLLATE NOCASE,
                       [Remark] nvarchar(255) COLLATE NOCASE,
                       [V_Date] datetime,
                       [SubCode] nvarchar(10) COLLATE NOCASE,
                       [PartyDetail] nvarchar(255) COLLATE NOCASE,
                       [Amount] float,
                       [Site_Code] nvarchar(2) COLLATE NOCASE,
                       [Div_Code] nvarchar(1) COLLATE NOCASE,
                       [UpLoadDate] datetime
                    );
          
                    "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If
    End Sub

    Private Sub FCreateTable_PostingGroupSalesTaxItem()
        Dim mQry As String
        If Not AgL.IsTableExist("PostingGroupSalesTaxItem", AgL.GcnMain) Then
            mQry = "
                    CREATE TABLE [PostingGroupSalesTaxItem] (
                       [Description] nvarchar(20) NOT NULL COLLATE NOCASE,
                       [Active] bit DEFAULT '1',
                       PRIMARY KEY ([Description])
                    );            
                    "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If
    End Sub

    Private Sub FSeedTable_PostingGroupSalesTaxItem()
        Dim mQry As String

        If AgL.FillData("Select * from PostingGroupSalesTaxItem limit 1", AgL.GcnMain).tables(0).Rows.Count = 0 Then
            mQry = " 
                    INSERT INTO PostingGroupSalesTaxItem
                    (Description, Active)
                    VALUES('GST 0%', 1);
                    INSERT INTO PostingGroupSalesTaxItem
                    (Description, Active)
                    VALUES('GST 5%', 1);
                    INSERT INTO PostingGroupSalesTaxItem
                    (Description, Active)
                    VALUES('GST 12%', 1);
                    INSERT INTO PostingGroupSalesTaxItem
                    (Description, Active)
                    VALUES('GST 18%', 1);
                    INSERT INTO PostingGroupSalesTaxItem
                    (Description, Active)
                    VALUES('GST 28%', 1);
                "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If
    End Sub


    Private Sub FCreateTable_StockHeadSetting()
        Dim mQry As String
        If Not AgL.IsTableExist("StockHeadSetting", AgL.GcnMain) Then
            mQry = " CREATE TABLE [StockHeadSetting] ( Code nVarchar(10) PRIMARY KEY); "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If
        AgL.AddFieldSqlite("StockHeadSetting", "V_Type", "nVarchar(5)", "", True, " references Voucher_Type(V_Type) ")
        AgL.AddFieldSqlite("StockHeadSetting", "Div_Code", "nVarchar(1)", "", True, " references Division(Div_Code) ")
        AgL.AddFieldSqlite("StockHeadSetting", "Site_Code", "nVarchar(2)", "", True, " references SiteMast(Code) ")
        AgL.AddFieldSqlite("StockHeadSetting", "IsPostedInStock", "bit", "1", False)
        AgL.AddFieldSqlite("StockHeadSetting", "IsPostedInStockProcess", "bit", "0", False)
        AgL.AddFieldSqlite("StockHeadSetting", "IsVisible_ItemCode", "bit", "0", False)
        AgL.AddFieldSqlite("StockHeadSetting", "IsVisible_Specification", "bit", "0", False)
        AgL.AddFieldSqlite("StockHeadSetting", "IsVisible_Dimension1", "bit", "0", False)
        AgL.AddFieldSqlite("StockHeadSetting", "IsVisible_Dimension2", "bit", "0", False)
        AgL.AddFieldSqlite("StockHeadSetting", "IsVisible_Dimension3", "bit", "0", False)
        AgL.AddFieldSqlite("StockHeadSetting", "IsVisible_Dimension4", "bit", "0", False)
        AgL.AddFieldSqlite("StockHeadSetting", "IsVisible_Manufacturer", "bit", "0", False)
        AgL.AddFieldSqlite("StockHeadSetting", "IsVisible_LotNo", "bit", "0", False)
        AgL.AddFieldSqlite("StockHeadSetting", "IsVisible_BaleNo", "bit", "0", False)
        AgL.AddFieldSqlite("StockHeadSetting", "IsVisible_Process", "bit", "0", False)
        AgL.AddFieldSqlite("StockHeadSetting", "IsVisible_ProcessLine", "bit", "0", False)
        AgL.AddFieldSqlite("StockHeadSetting", "IsEditable_ProcessLine", "bit", "0", False)
        AgL.AddFieldSqlite("StockHeadSetting", "IsMandatory_ProcessLine", "bit", "0", False)
        AgL.AddFieldSqlite("StockHeadSetting", "IsVisible_MeasurePerPcs", "bit", "0", False)
        AgL.AddFieldSqlite("StockHeadSetting", "IsEditable_MeasurePerPcs", "bit", "0", False)
        AgL.AddFieldSqlite("StockHeadSetting", "IsVisible_Measure", "bit", "0", False)
        AgL.AddFieldSqlite("StockHeadSetting", "IsEditable_Measure", "bit", "0", False)
        AgL.AddFieldSqlite("StockHeadSetting", "IsVisible_MeasureUnit", "bit", "0", False)
        AgL.AddFieldSqlite("StockHeadSetting", "IsEditable_MeasureUnit", "bit", "0", False)
        AgL.AddFieldSqlite("StockHeadSetting", "IsVisible_Rate", "bit", "0", False)
        AgL.AddFieldSqlite("StockHeadSetting", "IsEditable_Rate", "bit", "0", False)
        AgL.AddFieldSqlite("StockHeadSetting", "ItemHelpType", "nVarchar(20)", "", True)
        AgL.AddFieldSqlite("StockHeadSetting", "FilterInclude_Process", "nVarchar(255)", "", True)
        AgL.AddFieldSqlite("StockHeadSetting", "FilterExclude_AcGroup", "nVarchar(255)", "", True)
        AgL.AddFieldSqlite("StockHeadSetting", "FilterInclude_ItemType", "nVarchar(255)", "", True)
        AgL.AddFieldSqlite("StockHeadSetting", "FilterInclude_ItemGroup", "nVarchar(255)", "", True)
        AgL.AddFieldSqlite("StockHeadSetting", "FilterExclude_ItemGroup", "nVarchar(255)", "", True)
        AgL.AddFieldSqlite("StockHeadSetting", "FilterInclude_Item", "nVarchar(255)", "", True)
        AgL.AddFieldSqlite("StockHeadSetting", "FilterExclude_Item", "nVarchar(255)", "", True)
        AgL.AddFieldSqlite("StockHeadSetting", "FilterInclude_AcGroup", "nVarchar(255)", "", True)
        AgL.AddFieldSqlite("StockHeadSetting", "FilterInclude_AcGroup", "nVarchar(255)", "", True)


    End Sub


    Private Sub FCreateTable_Company()
        Dim mQry As String
        If Not AgL.IsTableExist("Company", AgL.GcnMain) Then
            mQry = "
                CREATE TABLE [Company] (
                   [Comp_Code] nvarchar(5) NOT NULL COLLATE NOCASE,
                   [Div_Code] nvarchar(1) COLLATE NOCASE,
                   [Comp_Name] nvarchar(100) COLLATE NOCASE,
                   [CentralData_Path] nvarchar(100) COLLATE NOCASE,
                   [PrevDBName] varchar(50) COLLATE NOCASE,
                   [DbPrefix] varchar(50) COLLATE NOCASE,
                   [Repo_Path] nvarchar(100) COLLATE NOCASE,
                   [Start_Dt] datetime,
                   [End_Dt] datetime,
                   [address1] nvarchar(35) COLLATE NOCASE,
                   [address2] nvarchar(35) COLLATE NOCASE,
                   [city] nvarchar(35) COLLATE NOCASE,
                   [pin] nvarchar(6) COLLATE NOCASE,
                   [phone] nvarchar(30) COLLATE NOCASE,
                   [fax] nvarchar(25) COLLATE NOCASE,
                   [lstno] nvarchar(35) COLLATE NOCASE,
                   [lstdate] nvarchar(12) COLLATE NOCASE,
                   [cstno] nvarchar(35) COLLATE NOCASE,
                   [cstdate] nvarchar(12) COLLATE NOCASE,
                   [cyear] nvarchar(9) COLLATE NOCASE,
                   [pyear] nvarchar(9) COLLATE NOCASE,
                   [SerialKeyNo] nvarchar(25) COLLATE NOCASE,
                   [SName] nvarchar(15) COLLATE NOCASE,
                   [EMail] varchar(30) COLLATE NOCASE,
                   [Gram] varchar(15) COLLATE NOCASE,
                   [Desc1] varchar(100) COLLATE NOCASE,
                   [Desc2] varchar(100) COLLATE NOCASE,
                   [Desc3] varchar(50) COLLATE NOCASE,
                   [ECCCode] varchar(15) COLLATE NOCASE,
                   [ExDivision] varchar(30) COLLATE NOCASE,
                   [ExRegNo] varchar(30) COLLATE NOCASE,
                   [ExColl] varchar(30) COLLATE NOCASE,
                   [ExRange] varchar(30) COLLATE NOCASE,
                   [Desc4] varchar(150) COLLATE NOCASE,
                   [VatNo] varchar(20) COLLATE NOCASE,
                   [VatDate] datetime,
                   [TinNo] varchar(12) COLLATE NOCASE,
                   [Site_Code] varchar(2) COLLATE NOCASE,
                   [LogSiteCode] varchar(2) COLLATE NOCASE,
                   [PANNo] varchar(25) COLLATE NOCASE,
                   [State] varchar(35) COLLATE NOCASE,
                   [U_Name] varchar(35) COLLATE NOCASE,
                   [U_EntDt] datetime,
                   [U_AE] nvarchar(1) COLLATE NOCASE,
                   [DeletedYN] nvarchar(1) COLLATE NOCASE,
                   [Country] nvarchar(50) COLLATE NOCASE,
                   [V_Prefix] nvarchar(5) COLLATE NOCASE,
                   [NotificationNo] nvarchar(10) COLLATE NOCASE,
                   [WorkAddress1] nvarchar(35) COLLATE NOCASE,
                   [WorkAddress2] nvarchar(35) COLLATE NOCASE,
                   [WorkCity] nvarchar(35) COLLATE NOCASE,
                   [WorkCountry] nvarchar(50) COLLATE NOCASE,
                   [WorkPin] nvarchar(6) COLLATE NOCASE,
                   [WorkPhone] nvarchar(30) COLLATE NOCASE,
                   [WorkFax] nvarchar(25) COLLATE NOCASE,
                   [WebServer] nvarchar(50) COLLATE NOCASE,
                   [WebUser] nvarchar(50) COLLATE NOCASE,
                   [WebPassword] nvarchar(50) COLLATE NOCASE,
                   [Webdatabase] nvarchar(50) COLLATE NOCASE,
                   [RowId] bigint NOT NULL,
                   [UpLoadDate] datetime,
                   [UseSiteNameAsCompanyName] bit,
                   [FileDbName] nvarchar(50) COLLATE NOCASE,
                   [ImageDbName] nvarchar(50) COLLATE NOCASE,
                   PRIMARY KEY ([Comp_Code])
                );               
            "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If
    End Sub

    Private Sub FSeedTable_Company()
        Dim mQry As String

        If AgL.FillData("Select * from Company limit 1", AgL.GcnMain).tables(0).Rows.Count = 0 Then
            mQry = " INSERT INTO Company
                (Comp_Code, Div_Code, Comp_Name, CentralData_Path, PrevDBName, DbPrefix, Repo_Path, Start_Dt, End_Dt, address1, address2, city, pin, phone, fax, lstno, lstdate, cstno, cstdate, cyear, pyear, SerialKeyNo, SName, EMail, Gram, Desc1, Desc2, Desc3, ECCCode, ExDivision, ExRegNo, ExColl, ExRange, Desc4, VatNo, VatDate, TinNo, Site_Code, LogSiteCode, PANNo, State, U_Name, U_EntDt, U_AE, DeletedYN, Country, V_Prefix, NotificationNo, WorkAddress1, WorkAddress2, WorkCity, WorkCountry, WorkPin, WorkPhone, WorkFax, WebServer, WebUser, WebPassword, Webdatabase, RowId, UpLoadDate, UseSiteNameAsCompanyName, FileDbName, ImageDbName)
                VALUES('1', 'D', 'Auditor9 Solutions', 'D:\KC\Data\Auditor9', NULL, 'Cloth', NULL, '2017-04-01 00:00:00', '2018-03-31 00:00:00', '13/152 Parmat, Civil Lines', NULL, 'Kanpur', '208001', '05414226864', '-', NULL, NULL, '-', '12/Nov/2017', '2017-2018', '2016-2017', 'RA96082587', 'AAMC', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', NULL, '2010-11-12 00:00:00', '09815400794', NULL, NULL, '-', 'U.P.', 'SA', '2008-04-01 00:00:00', 'E', 'N', 'INDIA', '2010', '-', '-', '-', '-', '-', '-', '-', '-', NULL, NULL, NULL, NULL, 1, NULL, 0, 'MedicalFiles', NULL);
                "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If
    End Sub

    Private Sub FSeedTable_PostingGroupSalesTaxParty()
        Dim mQry As String

        If AgL.FillData("Select * from PostingGroupSalesTaxParty limit 1", AgL.GcnMain).tables(0).Rows.Count = 0 Then

            mQry = " 
                    INSERT INTO PostingGroupSalesTaxParty
                    (Description, Active, Nature)
                    VALUES('Registered', 1, Null);
                    INSERT INTO PostingGroupSalesTaxParty
                    (Description, Active, Nature)
                    VALUES('Unregistered', 1, Null);
                "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If
    End Sub


    Private Sub FCreateTable_Division()
        Dim mQry As String
        If Not AgL.IsTableExist("Division", AgL.GcnMain) Then
            mQry = "
                CREATE TABLE [Division] (
                   [Div_Code] nvarchar(1) NOT NULL COLLATE NOCASE,
                   [Div_Name] nvarchar(100) COLLATE NOCASE,
                   [DataPath] nvarchar(50) COLLATE NOCASE,
                   [address1] nvarchar(35) COLLATE NOCASE,
                   [address2] nvarchar(35) COLLATE NOCASE,
                   [address3] nvarchar(35) COLLATE NOCASE,
                   [city] nvarchar(35) COLLATE NOCASE,
                   [pin] nvarchar(6) COLLATE NOCASE,
                   [PreparedBy] nvarchar(10) COLLATE NOCASE,
                   [U_EntDt] datetime,
                   [U_AE] nvarchar(1) COLLATE NOCASE,
                   [Edit_Date] datetime,
                   [ModifiedBy] nvarchar(10) COLLATE NOCASE,
                   [SitewiseV_No] bit DEFAULT '0',
                   [RowId] bigint NOT NULL,
                   [UpLoadDate] datetime,
                   [ApprovedBy] nvarchar(10) COLLATE NOCASE,
                   [ApprovedDate] datetime,
                   [GPX1] nvarchar(255) COLLATE NOCASE,
                   [GPX2] nvarchar(255) COLLATE NOCASE,
                   [GPN1] float,
                   [GPN2] float,
                   ScopeOfWork nVarchar(1000), 
                   PRIMARY KEY ([Div_Code])
                );

                CREATE UNIQUE INDEX [IX_Division]
                ON [Division]
                ([Div_Name]);
            "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If
    End Sub

    Private Sub FSeedTable_Division()
        Dim mQry As String

        If AgL.FillData("Select * from Division limit 1", AgL.GcnMain).tables(0).Rows.Count = 0 Then
            mQry = " INSERT INTO Division
                    (Div_Code, Div_Name, DataPath, address1, address2, address3, city, pin, PreparedBy, U_EntDt, U_AE, Edit_Date, ModifiedBy, SitewiseV_No, RowId, UpLoadDate, ApprovedBy, ApprovedDate, GPX1, GPX2, GPN1, GPN2, ScopeOfWork)
                    VALUES('D', 'Main', 'MEDICAL_1', '-', '-', '-', 'Kanpur', '-', 'SA', '2008-04-01 00:00:00', 'E', '2010-05-21 00:00:00', 'sa', 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL,'+CLOTH');
                "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If
    End Sub

    Private Sub FCreateTable_City()
        Dim mQry As String
        If Not AgL.IsTableExist("City", AgL.GcnMain) Then
            mQry = "
                CREATE TABLE [City] (
                   [CityCode] nvarchar(6) NOT NULL COLLATE NOCASE,
                   [CityName] nvarchar(50) COLLATE NOCASE,
                   [State] nvarchar(10) REFERENCES State(Code) COLLATE NOCASE,
                   [IsDeleted] bit,
                   [Country] nvarchar(50) COLLATE NOCASE,
                   [EntryBy] nvarchar(10) COLLATE NOCASE,
                   [EntryDate] datetime,
                   [EntryType] nvarchar(10) COLLATE NOCASE,
                   [EntryStatus] nvarchar(10) COLLATE NOCASE,
                   [ApproveBy] nvarchar(10) COLLATE NOCASE,
                   [ApproveDate] datetime,
                   [MoveToLog] nvarchar(10) COLLATE NOCASE,
                   [MoveToLogDate] datetime,
                   [Status] nvarchar(10) COLLATE NOCASE,
                   [Div_Code] nvarchar(1) COLLATE NOCASE,
                   [UID] uniqueidentifier COLLATE NOCASE,
                   [STDCode] nvarchar(15) COLLATE NOCASE,
                   [U_EntDt] datetime,
                   [U_Name] varchar(10) COLLATE NOCASE,
                   [U_AE] varchar(1) COLLATE NOCASE,
                   [Transfered] nvarchar(1) COLLATE NOCASE,
                   PRIMARY KEY ([CityCode])
                );

                CREATE UNIQUE INDEX [IX_City]
                ON [City]
                ([CityName]);
            "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If
    End Sub

    Private Sub FCreateTable_Area()
        Dim mQry As String
        If Not AgL.IsTableExist("Area", AgL.GcnMain) Then
            mQry = "
                    CREATE TABLE [Area] (
                   [Code] nvarchar(10) NOT NULL COLLATE NOCASE,
                   [Description] nvarchar(50)  COLLATE NOCASE,
                   [IsDeleted] bit,
                   [EntryBy] nvarchar(10) COLLATE NOCASE,
                   [EntryDate] datetime,
                   [EntryType] nvarchar(10) COLLATE NOCASE,
                   [EntryStatus] nvarchar(10) COLLATE NOCASE,
                   [ApproveBy] nvarchar(10) COLLATE NOCASE,
                   [ApproveDate] datetime,
                   [MoveToLog] nvarchar(10) COLLATE NOCASE,
                   [MoveToLogDate] datetime,
                   [Status] nvarchar(10) COLLATE NOCASE,
                   [Div_Code] nvarchar(1) COLLATE NOCASE,
                   [UID] uniqueidentifier COLLATE NOCASE,   
                   PRIMARY KEY ([Code])
                    );

                CREATE UNIQUE INDEX [IX_Area]
                ON [Area]
                ([Description]);
            "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If
    End Sub

    Private Sub FCreateTable_Reason()
        Dim mQry As String
        If Not AgL.IsTableExist("Reason", AgL.GcnMain) Then
            mQry = "
                    CREATE TABLE [Reason] (
                   [Code] nvarchar(10) NOT NULL COLLATE NOCASE,
                   [Description] nvarchar(50)  COLLATE NOCASE,
                   [NCAT] nvarchar(10)  COLLATE NOCASE,
                   [IsDeleted] bit,
                   [EntryBy] nvarchar(10) COLLATE NOCASE,
                   [EntryDate] datetime,
                   [EntryType] nvarchar(10) COLLATE NOCASE,
                   [EntryStatus] nvarchar(10) COLLATE NOCASE,
                   [ApproveBy] nvarchar(10) COLLATE NOCASE,
                   [ApproveDate] datetime,
                   [MoveToLog] nvarchar(10) COLLATE NOCASE,
                   [MoveToLogDate] datetime,
                   [Status] nvarchar(10) COLLATE NOCASE,
                   [Div_Code] nvarchar(1) COLLATE NOCASE,
                   [UID] uniqueidentifier COLLATE NOCASE,   
                   PRIMARY KEY ([Code])
                    );

                CREATE UNIQUE INDEX [IX_Reason]
                ON [Reason]
                ([Description],[NCAT]);
            "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If
    End Sub


    Private Sub FCreateTable_SiteMast()
        Dim mQry As String
        If Not AgL.IsTableExist("SiteMast", AgL.GcnMain) Then
            mQry = "
                CREATE TABLE [SiteMast] (
                   [Code] nvarchar(2) NOT NULL COLLATE NOCASE,
                   [Name] nvarchar(50) COLLATE NOCASE,
                   [HO_YN] nvarchar(1) COLLATE NOCASE,
                   [Add1] nvarchar(50) COLLATE NOCASE,
                   [Add2] nvarchar(50) COLLATE NOCASE,
                   [Add3] nvarchar(50) COLLATE NOCASE,
                   [City_Code] nvarchar(6) References City(CityCode),
                   [Phone] nvarchar(50) COLLATE NOCASE,
                   [Mobile] nvarchar(50) COLLATE NOCASE,
                   [PinNo] nvarchar(15) COLLATE NOCASE,
                   [U_Name] nvarchar(10) COLLATE NOCASE,
                   [U_EntDt] datetime,
                   [U_AE] nvarchar(1) COLLATE NOCASE,
                   [Edit_Date] datetime,
                   [ModifiedBy] nvarchar(10) COLLATE NOCASE,
                   [ManualCode] nvarchar(20) COLLATE NOCASE,
                   [RowId] bigint NOT NULL,
                   [UpLoadDate] datetime,
                   [Active] bit,
                   [AcCode] nvarchar(10) COLLATE NOCASE,
                   [SqlServer] nvarchar(50) COLLATE NOCASE,
                   [DataPath] nvarchar(50) COLLATE NOCASE,
                   [DataPathMain] nvarchar(50) COLLATE NOCASE,
                   [SqlUser] nvarchar(50) COLLATE NOCASE,
                   [SqlPassword] nvarchar(50) COLLATE NOCASE,
                   [CreditLimit] float,
                   [ApprovedBy] nvarchar(10) COLLATE NOCASE,
                   [ApprovedDate] datetime,
                   [GPX1] nvarchar(255) COLLATE NOCASE,
                   [GPX2] nvarchar(255) COLLATE NOCASE,
                   [GPN1] float,
                   [GPN2] float,
                   [Photo] image(2147483647),
                   [LastNarration] nvarchar(255) COLLATE NOCASE,
                   [IEC] nvarchar(20) COLLATE NOCASE,
                   [TIN] nvarchar(20) COLLATE NOCASE,
                   [Director] nvarchar(100) COLLATE NOCASE,
                   [ExciseDivision] nvarchar(50) COLLATE NOCASE,
                   [DrugLicenseNo] nvarchar(50) COLLATE NOCASE,
                   [PAN] nvarchar(20) COLLATE NOCASE,
                   PRIMARY KEY ([Code]),
                   CONSTRAINT [FK_SiteMast_City_City_Code] FOREIGN KEY ([City_Code])
                      REFERENCES [City]([CityCode]) ON DELETE NO ACTION ON UPDATE NO ACTION   
   
                );

                CREATE UNIQUE INDEX [IX_SiteMast]
                ON [SiteMast]
                ([Name]);
            "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If
    End Sub

    Private Sub FSeedTable_SiteMast()
        Dim mQry As String

        If AgL.FillData("Select * from SiteMast limit 1", AgL.GcnMain).tables(0).Rows.Count = 0 Then

            mQry = " INSERT INTO City
                    (CityCode, CityName, State, IsDeleted, Country, EntryBy, EntryDate, EntryType, EntryStatus, ApproveBy, ApproveDate, MoveToLog, MoveToLogDate, Status, Div_Code, UID, STDCode, U_EntDt, U_Name, U_AE, Transfered)
                    VALUES('D10001', 'Kanpur', 'D10009', 0, Null, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, Null, 'SUPER', 'A', NULL);
                "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)


            mQry = " 
                    INSERT INTO SiteMast
                    (Code, Name, HO_YN, Add1, Add2, Add3, City_Code, Phone, Mobile, PinNo, U_Name, U_EntDt, U_AE, Edit_Date, ModifiedBy, ManualCode, RowId, UpLoadDate, Active, AcCode, SqlServer, DataPath, DataPathMain, SqlUser, SqlPassword, CreditLimit, ApprovedBy, ApprovedDate, GPX1, GPX2, GPN1, GPN2, Photo, LastNarration, IEC, TIN, Director, ExciseDivision, DrugLicenseNo, PAN)
                    VALUES('1', 'Auditor9 Solutions', 'N', '13/152 Parmat, Civil Lines', NULL, NULL, 'D10001', '9335671971', NULL, '208001', 'SA', '2008-08-06 00:00:00', 'E', '2013-03-30 00:00:00', 'SA', 'HO', 1, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, 0.0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '---', NULL);
                   "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If
    End Sub

    Private Sub FCreateTable_UserMast()
        Dim mQry As String
        If Not AgL.IsTableExist("UserMast", AgL.GcnMain) Then
            mQry = "
                CREATE TABLE [UserMast] (
                [USER_NAME] nvarchar(10) NOT NULL COLLATE NOCASE,
                [Code] nvarchar(15) COLLATE NOCASE,
                [PASSWD] nvarchar(16) COLLATE NOCASE,
                [Description] nvarchar(50) COLLATE NOCASE,
                [Admin] nvarchar(1) COLLATE NOCASE,
                [RowId] bigint NOT NULL,
                [UpLoadDate] datetime,
                [ModuleList] nvarchar(2147483647) COLLATE NOCASE,
                [SeniorName] nvarchar(10) COLLATE NOCASE,
                [MainStreamCode] nvarchar(2147483647) COLLATE NOCASE,
                [EMail] nvarchar(100) COLLATE NOCASE,
                [Mobile] nvarchar(10) COLLATE NOCASE,
                [IsActive] bit,
                [InActiveDate] datetime,
                PRIMARY KEY ([USER_NAME])
                );

                CREATE UNIQUE INDEX [IX_UserMast]
                ON [UserMast]
                ([USER_NAME]);

                "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If
    End Sub

    Private Sub FSeedTable_UserMast()
        Dim mQry As String

        If AgL.FillData("Select * from UserMast limit 1", AgL.GcnMain).tables(0).Rows.Count = 0 Then
            mQry = " 
                    INSERT INTO UserMast
                    (USER_NAME, Code, PASSWD, Description, Admin, RowId, UpLoadDate, ModuleList, SeniorName, MainStreamCode, EMail, Mobile, IsActive, InActiveDate)
                    VALUES('SA', '1', '@', 'CEO', 'Y', 1, NULL, NULL, NULL, '010', NULL, NULL, 1, NULL);

                   "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If
    End Sub

    Private Sub FCreateTable_UserSite()
        Dim mQry As String
        If Not AgL.IsTableExist("UserSite", AgL.GcnMain) Then
            mQry = "
                    CREATE TABLE [UserSite] (
                       [User_Name] nvarchar(10) NOT NULL COLLATE NOCASE,
                       [CompCode] nvarchar(5) NOT NULL COLLATE NOCASE,
                       [Sitelist] nvarchar(250) COLLATE NOCASE,
                       [UpLoadDate] datetime,
                       [DivisionList] nvarchar(250) COLLATE NOCASE,
                       PRIMARY KEY ([User_Name], [CompCode]),
                       CONSTRAINT [FK_UserSite_Company_CompCode] FOREIGN KEY ([CompCode])
                          REFERENCES [Company]([Comp_Code]) ON DELETE NO ACTION ON UPDATE NO ACTION
                    );

            "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If
    End Sub

    Private Sub FSeedTable_UserSite()
        Dim mQry As String

        If AgL.FillData("Select * from UserSite limit 1", AgL.GcnMain).tables(0).Rows.Count = 0 Then
            mQry = " 
                    INSERT INTO UserSite
                    (User_Name, CompCode, Sitelist, UpLoadDate, DivisionList)
                    VALUES('SA', '1', '|1|', NULL, '|D|');

                   "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If
    End Sub

    Private Sub FCreateTable_User_Permission()
        Dim mQry As String
        If Not AgL.IsTableExist("User_Permission", AgL.GcnMain) Then
            mQry = "
                CREATE TABLE  [User_Permission] (
                   [UserName] nvarchar(10) NOT NULL COLLATE NOCASE,
                   [MnuModule] nvarchar(50) NOT NULL COLLATE NOCASE,
                   [MnuName] nvarchar(100) NOT NULL COLLATE NOCASE,
                   [MnuText] nvarchar(100) COLLATE NOCASE,
                   [SNo] int,
                   [MnuLevel] int,
                   [Parent] nvarchar(50) COLLATE NOCASE,
                   [Permission] nvarchar(4) COLLATE NOCASE,
                   [ReportFor] nvarchar(50) COLLATE NOCASE,
                   [Active] nvarchar(1) COLLATE NOCASE,
                   [RowId] bigint NOT NULL,
                   [UpLoadDate] datetime,
                   [MainStreamCode] varchar(2147483647) COLLATE NOCASE,
                   [GroupLevel] float,
                   [ControlPermissionGroups] varchar(2147483647) COLLATE NOCASE,
                   [LogSystem] bit,
                   [IsParent] bit,
                   PRIMARY KEY ([UserName], [MnuModule], [MnuName])
                );
            
                CREATE INDEX [IX_User_Permission]
                ON [User_Permission]
                ([UserName], [MnuModule], [Parent]);
            
               "

            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If

    End Sub

    Private Sub FSeedTable_User_Permission()
        Dim mQry As String

        If AgL.FillData("Select * from User_Permission limit 1", AgL.GcnMain).tables(0).Rows.Count = 0 Then
            mQry = " 
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Accounts', 'AccountsToolStripMenuItem', 'Accounts', 1, 0, '', 'AEDP', NULL, 'Y', 4256, NULL, '001', 55.0, NULL, NULL, 1);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Accounts', 'MnuDisplay', 'Display', 14, 13, 'AccountsToolStripMenuItem', 'AEDP', NULL, 'Y', 4269, NULL, '001013', 6.0, NULL, NULL, 1);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Accounts', 'MnuMaster', 'Master', 2, 1, 'AccountsToolStripMenuItem', 'AEDP', NULL, 'Y', 4257, NULL, '001001', 7.0, NULL, NULL, 1);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Accounts', 'MnuReports', 'Reports I', 20, 19, 'AccountsToolStripMenuItem', 'AEDP', NULL, 'Y', 4275, NULL, '001019', 24.0, NULL, NULL, 1);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Accounts', 'MnuReportsII', 'Reports II', 44, 43, 'AccountsToolStripMenuItem', 'AEDP', NULL, 'Y', 4299, NULL, '001043', 12.0, NULL, NULL, 1);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Accounts', 'MnuTransactions', 'Transactions', 9, 8, 'AccountsToolStripMenuItem', 'AEDP', NULL, 'Y', 4264, NULL, '001008', 5.0, NULL, NULL, 1);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Accounts', 'MnuBalanceSheet_Disp', 'Balance Sheet', 18, 17, 'MnuDisplay', 'AEDP', NULL, 'Y', 4273, NULL, '001013017', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Accounts', 'MnuDetailTrialBalance_Disp', 'Detail Trial Balance', 16, 15, 'MnuDisplay', 'AEDP', NULL, 'Y', 4271, NULL, '001013015', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Accounts', 'MnuProfitAndLoss_Disp', 'Profit And Loss', 17, 16, 'MnuDisplay', 'AEDP', NULL, 'Y', 4272, NULL, '001013016', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Accounts', 'MnuStockReport', 'Stock Report', 19, 18, 'MnuDisplay', 'AEDP', NULL, 'Y', 4274, NULL, '001013018', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Accounts', 'MnuTrialBalance_Disp', 'Trial Balance', 15, 14, 'MnuDisplay', 'AEDP', NULL, 'Y', 4270, NULL, '001013014', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Accounts', 'MnuAccountGroup', 'Account Group', 6, 5, 'MnuMaster', 'AEDP', NULL, 'Y', 4261, NULL, '001001005', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Accounts', 'MnuAccountMaster', 'Account Master', 7, 6, 'MnuMaster', 'AEDP', NULL, 'Y', 4262, NULL, '001001006', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Accounts', 'MnuCityMaster', 'City Master', 3, 2, 'MnuMaster', 'AEDP', NULL, 'Y', 4258, NULL, '001001002', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Accounts', 'MnuDefineCostCenter', 'Define Cost Center', 5, 4, 'MnuMaster', 'AEDP', NULL, 'Y', 4260, NULL, '001001004', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Accounts', 'MnuLedgerGroup', 'Ledger Group', 8, 7, 'MnuMaster', 'AEDP', NULL, 'Y', 4263, NULL, '001001007', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Accounts', 'MnuNarrationMaster', 'Narration Master', 4, 3, 'MnuMaster', 'AEDP', NULL, 'Y', 4259, NULL, '001001003', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Accounts', 'MnuAccountGroupMergeLedger', 'Account Group Merge Ledger', 28, 27, 'MnuReports', 'AEDP', NULL, 'Y', 4283, NULL, '001019027', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Accounts', 'MnuAnnexure', 'Annexure', 32, 31, 'MnuReports', 'AEDP', NULL, 'Y', 4287, NULL, '001019031', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Accounts', 'MnuBankBook', 'Bank Book', 23, 22, 'MnuReports', 'AEDP', NULL, 'Y', 4278, NULL, '001019022', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Accounts', 'MnuCashBook', 'Cash Book', 24, 23, 'MnuReports', 'AEDP', NULL, 'Y', 4279, NULL, '001019023', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Accounts', 'MnuCashFlowStatement', 'Cash Flow Statement', 33, 32, 'MnuReports', 'AEDP', NULL, 'Y', 4288, NULL, '001019032', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Accounts', 'MnuDailyTransactionSummary', 'Daily Transaction Summary', 21, 20, 'MnuReports', 'AEDP', NULL, 'Y', 4276, NULL, '001019020', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Accounts', 'MnuDayBook', 'DayBook', 22, 21, 'MnuReports', 'AEDP', NULL, 'Y', 4277, NULL, '001019021', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Accounts', 'MnuFBTReport', 'FBT Report', 36, 35, 'MnuReports', 'AEDP', NULL, 'Y', 4291, NULL, '001019035', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Accounts', 'MnuFundFlowStatement', 'Fund Flow Statement', 34, 33, 'MnuReports', 'AEDP', NULL, 'Y', 4289, NULL, '001019033', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Accounts', 'MnuInterestCalculationForDebtors', 'Interest Calculation For Debtors', 40, 39, 'MnuReports', 'AEDP', NULL, 'Y', 4295, NULL, '001019039', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Accounts', 'MnuInterestLedger', 'Interest Ledger', 41, 40, 'MnuReports', 'AEDP', NULL, 'Y', 4296, NULL, '001019040', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Accounts', 'MnuJournalBook', 'Journal Book', 25, 24, 'MnuReports', 'AEDP', NULL, 'Y', 4280, NULL, '001019024', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Accounts', 'MnuLedger', 'Ledger', 26, 25, 'MnuReports', 'AEDP', NULL, 'Y', 4281, NULL, '001019025', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Accounts', 'MnuLedgerGroupMergeLedger', 'Ledger Group Merge Ledger', 27, 26, 'MnuReports', 'AEDP', NULL, 'Y', 4282, NULL, '001019026', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Accounts', 'MnuMonthlyExpenseChart', 'Monthly Expense Chart', 35, 34, 'MnuReports', 'AEDP', NULL, 'Y', 4290, NULL, '001019034', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Accounts', 'MnuMonthlyLedgerSummaryFull', 'Monthly Ledger Summary (Full)', 43, 42, 'MnuReports', 'AEDP', NULL, 'Y', 4298, NULL, '001019042', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Accounts', 'MnuMonthyLedgerSummary', 'Monthy Ledger Summary', 42, 41, 'MnuReports', 'AEDP', NULL, 'Y', 4297, NULL, '001019041', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Accounts', 'MnuPartyWiseTDS', 'Party Wise TDS', 37, 36, 'MnuReports', 'AEDP', NULL, 'Y', 4292, NULL, '001019036', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Accounts', 'MnuTDSCertificate', 'TDS Certificate', 39, 38, 'MnuReports', 'AEDP', NULL, 'Y', 4294, NULL, '001019038', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Accounts', 'MnuTDSReport', 'TDS Report', 38, 37, 'MnuReports', 'AEDP', NULL, 'Y', 4293, NULL, '001019037', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Accounts', 'MnuTrialDetail', 'Trial Detail', 30, 29, 'MnuReports', 'AEDP', NULL, 'Y', 4285, NULL, '001019029', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Accounts', 'MnuTrialDetailDrCr', 'Trial Detail (Dr/Cr)', 31, 30, 'MnuReports', 'AEDP', NULL, 'Y', 4286, NULL, '001019030', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Accounts', 'MnuTrialGroup', 'Trial Group', 29, 28, 'MnuReports', 'AEDP', NULL, 'Y', 4284, NULL, '001019028', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Accounts', 'MnuAccountGroupWiseAgeingAnalysis', 'Account Group Wise Ageing Analysis', 47, 46, 'MnuReportsII', 'AEDP', NULL, 'Y', 4302, NULL, '001043046', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Accounts', 'MnuAgeingAnalysisBillWise', 'Ageing Analysis Bill Wise', 46, 45, 'MnuReportsII', 'AEDP', NULL, 'Y', 4301, NULL, '001043045', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Accounts', 'MnuAgeingAnalysisFIFO', 'Ageing Analysis FIFO', 45, 44, 'MnuReportsII', 'AEDP', NULL, 'Y', 4300, NULL, '001043044', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Accounts', 'MnuBillWiseAdjustmentRegister', 'Bill Wise Adjustment Register', 50, 49, 'MnuReportsII', 'AEDP', NULL, 'Y', 4305, NULL, '001043049', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Accounts', 'MnuBillWiseOutstandingCreditors', 'Bill Wise Outstanding (Creditors)', 49, 48, 'MnuReportsII', 'AEDP', NULL, 'Y', 4304, NULL, '001043048', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Accounts', 'MnuBillWiseOutstandingDebtors', 'Bill Wise Outstanding (Debtors)', 48, 47, 'MnuReportsII', 'AEDP', NULL, 'Y', 4303, NULL, '001043047', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Accounts', 'MnuDailyCollectionRegister', 'Daily Collection Register', 54, 53, 'MnuReportsII', 'AEDP', NULL, 'Y', 4309, NULL, '001043053', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Accounts', 'MnuDailyExpenseRegister', 'Daily Expense Register', 55, 54, 'MnuReportsII', 'AEDP', NULL, 'Y', 4310, NULL, '001043054', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Accounts', 'MnuOutstandinDebtorsFIFO', 'Outstanding Debtors FIFO', 51, 50, 'MnuReportsII', 'AEDP', NULL, 'Y', 4306, NULL, '001043050', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Accounts', 'MnuOutstandingCreditorsFIFO', 'Outstanding Creditors FIFO', 52, 51, 'MnuReportsII', 'AEDP', NULL, 'Y', 4307, NULL, '001043051', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Accounts', 'MnuStockValuation', 'Stock Valuation', 53, 52, 'MnuReportsII', 'AEDP', NULL, 'Y', 4308, NULL, '001043052', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Accounts', 'MnuBankReconsilationEntry', 'Bank Reconciliation Entry', 11, 10, 'MnuTransactions', 'AEDP', NULL, 'Y', 4266, NULL, '001008010', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Accounts', 'MnuOpeningBalanceEntry', 'Opening Balance Entry', 12, 11, 'MnuTransactions', 'AEDP', NULL, 'Y', 4267, NULL, '001008011', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Accounts', 'MnuSalesTaxClubbing', 'Sales Tax Clubbing', 13, 12, 'MnuTransactions', 'AEDP', NULL, 'Y', 4268, NULL, '001008012', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Accounts', 'MnuVoucherEntry', 'Voucher Entry', 10, 9, 'MnuTransactions', 'AEDP', NULL, 'Y', 4265, NULL, '001008009', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Customised', 'MnuCustomized', 'Customized', 68, 0, '', 'AEDP', NULL, 'Y', 4323, NULL, '003', 16.0, NULL, NULL, 1);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Customised', 'MnuMaster', 'Master', 56, 0, '', 'AEDP', NULL, 'Y', 4311, NULL, '002', 12.0, NULL, NULL, 1);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Customised', 'ItemExpiryReportToolStripMenuItem', 'Item Expiry Report', 73, 5, 'MnnReports', 'AEDP', 'Report', 'Y', 4328, NULL, '003004005', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Customised', 'MnuBillWiseProfitability', 'Bill Wise Profitability', 78, 10, 'MnnReports', 'AEDP', 'REPORT', 'Y', 4333, NULL, '003004010', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Customised', 'MnuCurrentStockReport', 'Current Stock Report', 81, 13, 'MnnReports', 'AEDP', 'Report', 'Y', 4336, NULL, '003004013', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Customised', 'MnuDebtorsOutstandingOverDue', 'Debtors Outstanding Over Due', 79, 11, 'MnnReports', 'AEDP', 'Report', 'Y', 4334, NULL, '003004011', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Customised', 'MnuPartyOutstandingReport', 'Party Outstanding Report', 77, 9, 'MnnReports', 'AEDP', 'Report', 'Y', 4332, NULL, '003004009', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Customised', 'MnuPurchaseIndentReport', 'Purchase Indent Report', 75, 7, 'MnnReports', 'AEDP', 'Report', 'Y', 4330, NULL, '003004007', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Customised', 'MnuVATReports', 'VAT Reports', 76, 8, 'MnnReports', 'AEDP', 'Report', 'Y', 4331, NULL, '003004008', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Customised', 'MnuWeavingOrderRatio', 'Weaving Order Ratio', 80, 12, 'MnnReports', 'AEDP', 'Report', 'Y', 4335, NULL, '003004012', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Customised', 'PurchaseAdviseReportToolStripMenuItem', 'Purchase Advise Report', 74, 6, 'MnnReports', 'AEDP', 'Report', 'Y', 4329, NULL, '003004006', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Customised', 'MnnReports', 'Reports', 72, 4, 'MnuCustomized', 'AEDP', NULL, 'Y', 4327, NULL, '003004', 10.0, NULL, NULL, 1);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Customised', 'MnuAdjustmentIssueEntry', 'Adjustment Issue Entry', 71, 3, 'MnuCustomized', 'AEDP', NULL, 'Y', 4326, NULL, '003003', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Customised', 'MnuOpeningStockEntry', 'Opening Stock Entry', 70, 2, 'MnuCustomized', 'AEDP', NULL, 'Y', 4325, NULL, '003002', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Customised', 'MnuSaleInvoiceDetailEntry', 'Sale Invoice Detail Entry', 69, 1, 'MnuCustomized', 'AEDP', NULL, 'Y', 4324, NULL, '003001', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Customised', 'MnuTools', 'Tools', 82, 14, 'MnuCustomized', 'AEDP', NULL, 'Y', 4337, NULL, '003014', 2.0, NULL, NULL, 1);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Customised', 'MnuAgentMaster', 'Agent Master', 62, 6, 'MnuMaster', 'AEDP', NULL, 'Y', 4317, NULL, '002006', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Customised', 'MnuCustomerMaster', 'Customer Master', 60, 4, 'MnuMaster', 'AEDP', NULL, 'Y', 4315, NULL, '002004', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Customised', 'MnuGodownMaster', 'Godown Master', 67, 11, 'MnuMaster', 'AEDP', NULL, 'Y', 4322, NULL, '002011', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Customised', 'MnuItemCategoryMaster', 'Item Category Master', 57, 1, 'MnuMaster', 'AEDP', NULL, 'Y', 4312, NULL, '002001', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Customised', 'MnuItemGroupMaster', 'Item Group Master', 58, 2, 'MnuMaster', 'AEDP', NULL, 'Y', 4313, NULL, '002002', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Customised', 'MnuItemMaster', 'Item Master', 59, 3, 'MnuMaster', 'AEDP', NULL, 'Y', 4314, NULL, '002003', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Customised', 'MnuManufacturerMaster', 'Manufacturer Master', 65, 9, 'MnuMaster', 'AEDP', NULL, 'Y', 4320, NULL, '002009', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Customised', 'MnuRateList', 'Rate List', 64, 8, 'MnuMaster', 'AEDP', NULL, 'Y', 4319, NULL, '002008', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Customised', 'MnuRateTypeMaster', 'Rate Type Master', 63, 7, 'MnuMaster', 'AEDP', NULL, 'Y', 4318, NULL, '002007', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Customised', 'MnuSupplierMaster', 'Supplier Master', 61, 5, 'MnuMaster', 'AEDP', NULL, 'Y', 4316, NULL, '002005', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Customised', 'MnuVatCommodityCodeMaster', 'Vat Commodity Code Master', 66, 10, 'MnuMaster', 'AEDP', NULL, 'Y', 4321, NULL, '002010', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Customised', 'MnuAdjustSaleInvoices', 'Adjust Sale Invoices', 83, 15, 'MnuTools', 'AEDP', NULL, 'Y', 4338, NULL, '003014015', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Purchase', 'MnuPurchase', 'Purchase', 84, 0, '', 'AEDP', NULL, 'Y', 4339, NULL, '004', 5.0, NULL, NULL, 1);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Purchase', 'MnuPurchaseInvoice', 'Purchase Invoice', 85, 1, 'MnuPurchase', 'AEDP', NULL, 'Y', 4340, NULL, '004001', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Purchase', 'MnuPurchaseReturn', 'Purchase Return', 86, 2, 'MnuPurchase', 'AEDP', NULL, 'Y', 4341, NULL, '004002', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Purchase', 'MnuReports', 'Reports', 87, 3, 'MnuPurchase', 'AEDP', NULL, 'Y', 4342, NULL, '004003', 2.0, NULL, NULL, 1);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Purchase', 'MnuPurchaseInvoiceReport', 'Purchase Invoice Report', 88, 4, 'MnuReports', 'AEDP', 'Report', 'Y', 4343, NULL, '004003004', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'SALES', 'MnuSales', 'Sales', 89, 0, '', 'AEDP', NULL, 'Y', 4344, NULL, '005', 10.0, NULL, NULL, 1);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'SALES', 'MnuSaleInvoiceReport', 'Sale Invoice Report', 93, 4, 'MnuReports', 'AEDP', 'REPORT', 'Y', 4348, NULL, '005003004', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'SALES', 'MnuSaleInvoiceSummary', 'Sale Invoice Summary', 95, 6, 'MnuReports', 'AEDP', 'Report', 'Y', 4350, NULL, '005003006', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'SALES', 'MnuSaleOrderSummary', 'Sale Order Summary', 94, 5, 'MnuReports', 'AEDP', 'Report', 'Y', 4349, NULL, '005003005', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'SALES', 'MnuReports', 'Reports', 92, 3, 'MnuSales', 'AEDP', NULL, 'Y', 4347, NULL, '005003', 4.0, NULL, NULL, 1);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'SALES', 'MnuSaleInvoice', 'Sale Invoice', 90, 1, 'MnuSales', 'AEDP', NULL, 'Y', 4345, NULL, '005001', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'SALES', 'MnuSaleReturn', 'Sale Return', 91, 2, 'MnuSales', 'AEDP', NULL, 'Y', 4346, NULL, '005002', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'SALES', 'MnuStatusReports', 'Status Reports', 96, 7, 'MnuSales', 'AEDP', NULL, 'Y', 4351, NULL, '005007', 3.0, NULL, NULL, 1);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'SALES', 'MnuSaleOrderBalance', 'Sale Order Balance', 97, 8, 'MnuStatusReports', 'AEDP', 'Report', 'Y', 4352, NULL, '005007008', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'SALES', 'MnuSaleOrderInvoiceSummary', 'Sale Order  Invoice Summary', 98, 9, 'MnuStatusReports', 'AEDP', 'Report', 'Y', 4353, NULL, '005007009', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Store', 'MnuInventory', 'Store', 99, 0, '', 'AEDP', NULL, 'Y', 4354, NULL, '006', 43.0, NULL, NULL, 1);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Store', 'MnuStoreMaster', 'Master', 100, 1, 'MnuInventory', 'AEDP', NULL, 'Y', 4355, NULL, '006001', 21.0, NULL, NULL, 1);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Store', 'MnuStoreReports', 'Reports', 130, 31, 'MnuInventory', 'AEDP', NULL, 'Y', 4385, NULL, '006031', 12.0, NULL, NULL, 1);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Store', 'MnuStoreTransactions', 'Transactions', 121, 22, 'MnuInventory', 'AEDP', NULL, 'Y', 4376, NULL, '006022', 9.0, NULL, NULL, 1);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Store', 'MnuAgentMaster', 'Agent Master', 113, 14, 'MnuStoreMaster', 'AEDP', NULL, 'Y', 4368, NULL, '006001014', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Store', 'MnuCustomerMaster', 'Customer Master', 111, 12, 'MnuStoreMaster', 'AEDP', NULL, 'Y', 4366, NULL, '006001012', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Store', 'MnuDimension1Master', 'Dimension1 Master', 118, 19, 'MnuStoreMaster', 'AEDP', NULL, 'Y', 4373, NULL, '006001019', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Store', 'MnuDimension2Master', 'Dimension2 Master', 119, 20, 'MnuStoreMaster', 'AEDP', NULL, 'Y', 4374, NULL, '006001020', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Store', 'MnuGodown', 'Godown', 104, 5, 'MnuStoreMaster', 'AEDP', NULL, 'Y', 4359, NULL, '006001005', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Store', 'MnuItemCategory', 'Item Category', 103, 4, 'MnuStoreMaster', 'AEDP', NULL, 'Y', 4358, NULL, '006001004', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Store', 'MnuItemGroup', 'Item Group', 102, 3, 'MnuStoreMaster', 'AEDP', NULL, 'Y', 4357, NULL, '006001003', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Store', 'MnuItemInvoiceGroup', 'Item Invoice Group', 106, 7, 'MnuStoreMaster', 'AEDP', NULL, 'Y', 4361, NULL, '006001007', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Store', 'MnuItemMaster', 'Item Master', 101, 2, 'MnuStoreMaster', 'AEDP', NULL, 'Y', 4356, NULL, '006001002', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Store', 'MnuItemRateGroup', 'Item Rate Group', 107, 8, 'MnuStoreMaster', 'AEDP', NULL, 'Y', 4362, NULL, '006001008', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Store', 'MnuItemReportingGroup', 'Item Reporting Group', 105, 6, 'MnuStoreMaster', 'AEDP', NULL, 'Y', 4360, NULL, '006001006', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Store', 'MnuPartyRateGroup', 'Party Rate Group', 108, 9, 'MnuStoreMaster', 'AEDP', NULL, 'Y', 4363, NULL, '006001009', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Store', 'MnuQCGroupMaster', 'QC Group Master', 109, 10, 'MnuStoreMaster', 'AEDP', NULL, 'Y', 4364, NULL, '006001010', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Store', 'MnuRateList', 'Rate List', 117, 18, 'MnuStoreMaster', 'AEDP', NULL, 'Y', 4372, NULL, '006001018', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Store', 'MnuShiftMaster', 'Shift Master', 120, 21, 'MnuStoreMaster', 'AEDP', NULL, 'Y', 4375, NULL, '006001021', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Store', 'MnuSupplierMaster', 'Supplier Master', 112, 13, 'MnuStoreMaster', 'AEDP', NULL, 'Y', 4367, NULL, '006001013', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Store', 'MnuTariffHeading', 'Tariff Heading', 115, 16, 'MnuStoreMaster', 'AEDP', NULL, 'Y', 4370, NULL, '006001016', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Store', 'MnuTermCondition', 'Term  Condition Master', 116, 17, 'MnuStoreMaster', 'AEDP', NULL, 'Y', 4371, NULL, '006001017', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Store', 'MnuUnitConversion', 'Unit Conversion', 110, 11, 'MnuStoreMaster', 'AEDP', NULL, 'Y', 4365, NULL, '006001011', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Store', 'MnuVatCommodityCode', 'Vat Commodity Code', 114, 15, 'MnuStoreMaster', 'AEDP', NULL, 'Y', 4369, NULL, '006001015', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Store', 'MnuItemIssueReport', 'Item Issue Report', 133, 34, 'MnuStoreReports', 'AEDP', 'Report', 'Y', 4388, NULL, '006031034', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Store', 'MnuItemReceiveReport', 'Item Receive Report', 134, 35, 'MnuStoreReports', 'AEDP', 'Report', 'Y', 4389, NULL, '006031035', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Store', 'MnuMaterialIssueSummary', 'Material Issue Summary', 140, 41, 'MnuStoreReports', 'AEDP', 'Report', 'Y', 4395, NULL, '006031041', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Store', 'MnuMaterialReceiveSummary', 'Material Receive Summary', 141, 42, 'MnuStoreReports', 'AEDP', 'Report', 'Y', 4396, NULL, '006031042', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Store', 'MnuRequisitionReport', 'Requisition Report', 131, 32, 'MnuStoreReports', 'AEDP', 'Report', 'Y', 4386, NULL, '006031032', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Store', 'MnuRequisitionStatus', 'Requisition Status', 132, 33, 'MnuStoreReports', 'AEDP', 'Report', 'Y', 4387, NULL, '006031033', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Store', 'MnuStockBalance', 'Stock Balance', 139, 40, 'MnuStoreReports', 'AEDP', 'Report', 'Y', 4394, NULL, '006031040', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Store', 'MnuStockInHand', 'Stock In Hand', 137, 38, 'MnuStoreReports', 'AEDP', 'Report', 'Y', 4392, NULL, '006031038', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Store', 'MnuStockInProcess', 'Stock In Process', 138, 39, 'MnuStoreReports', 'AEDP', 'Report', 'Y', 4393, NULL, '006031039', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Store', 'MnuStockTransferReport', 'Stock Transfer Report', 135, 36, 'MnuStoreReports', 'AEDP', 'Report', 'Y', 4390, NULL, '006031036', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Store', 'PhysicalStockReportToolStripMenuItem', 'Physical Stock Report', 136, 37, 'MnuStoreReports', 'AEDP', 'Report', 'Y', 4391, NULL, '006031037', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Store', 'MnuInternalProcess', 'Internal Process', 126, 27, 'MnuStoreTransactions', 'AEDP', NULL, 'Y', 4381, NULL, '006022027', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Store', 'MnuItemIssueFromStore', 'Item Issue From Store', 124, 25, 'MnuStoreTransactions', 'AEDP', NULL, 'Y', 4379, NULL, '006022025', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Store', 'MnuItemReceiveInStore', 'Item Receive In Store', 125, 26, 'MnuStoreTransactions', 'AEDP', NULL, 'Y', 4380, NULL, '006022026', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Store', 'MnuItemRequisition', 'Item Requisition', 122, 23, 'MnuStoreTransactions', 'AEDP', NULL, 'Y', 4377, NULL, '006022023', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Store', 'MnuItemRequisitionApproval', 'Item Requisition Approval', 123, 24, 'MnuStoreTransactions', 'AEDP', NULL, 'Y', 4378, NULL, '006022024', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Store', 'MnuPhysicalStockAdjustmentEntry', 'Physical Stock Adjustment Entry', 129, 30, 'MnuStoreTransactions', 'AEDP', NULL, 'Y', 4384, NULL, '006022030', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Store', 'MnuPhysicalStockEntry', 'Physical Stock Entry', 128, 29, 'MnuStoreTransactions', 'AEDP', NULL, 'Y', 4383, NULL, '006022029', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Store', 'MnuStockTransfer', 'Stock Transfer', 127, 28, 'MnuStoreTransactions', 'AEDP', NULL, 'Y', 4382, NULL, '006022028', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Utility', 'MnuUtility', 'Utility', 142, 0, '', 'AEDP', NULL, 'Y', 4397, NULL, '007', 24.0, NULL, NULL, 1);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Utility', 'MnuCompanyMaster', 'Company Master', 163, 21, 'MnuCompanyHierarchy', 'AEDP', NULL, 'Y', 4418, NULL, '007020021', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Utility', 'MnuDivisionMaster', 'Division Master', 165, 23, 'MnuCompanyHierarchy', 'AEDP', NULL, 'Y', 4420, NULL, '007020023', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Utility', 'MnuSiteBranchMaster', 'Site / Branch Master', 164, 22, 'MnuCompanyHierarchy', 'AEDP', NULL, 'Y', 4419, NULL, '007020022', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Utility', 'MnuCustomFieldHeadMaster', 'Head Master', 156, 14, 'MnuCustomFields', 'AEDP', NULL, 'Y', 4411, NULL, '007013014', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Utility', 'MnuCustomFieldMaster', 'Custom Fields Master', 157, 15, 'MnuCustomFields', 'AEDP', NULL, 'Y', 4412, NULL, '007013015', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Utility', 'MnuBackup', 'Backup', 154, 12, 'MnuDatabase', 'AEDP', NULL, 'Y', 4409, NULL, '007011012', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Utility', 'MnuNCatMapping', 'NCat Mapping', 147, 5, 'MnuStructure', 'AEDP', NULL, 'Y', 4402, NULL, '007001005', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Utility', 'MnuStructureHeadMaster', 'Head Master', 144, 2, 'MnuStructure', 'AEDP', NULL, 'Y', 4399, NULL, '007001002', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Utility', 'MnuStructureMaster', 'Structure Master', 145, 3, 'MnuStructure', 'AEDP', NULL, 'Y', 4400, NULL, '007001003', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Utility', 'MnuTaxRateMaster', 'Tax Rate Master', 146, 4, 'MnuStructure', 'AEDP', NULL, 'Y', 4401, NULL, '007001004', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Utility', 'MnuUserControlPermission', 'User Control Permission', 151, 9, 'MnuUser', 'AEDP', NULL, 'Y', 4406, NULL, '007006009', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Utility', 'MnuUserMaster', 'User Master', 149, 7, 'MnuUser', 'AEDP', NULL, 'Y', 4404, NULL, '007006007', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Utility', 'MnuUserPermission', 'User Permission', 150, 8, 'MnuUser', 'AEDP', NULL, 'Y', 4405, NULL, '007006008', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Utility', 'MnuUserVoucherTypeRestriction', 'User Voucher Type Restriction', 152, 10, 'MnuUser', 'AEDP', NULL, 'Y', 4407, NULL, '007006010', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Utility', 'MnuCompanyHierarchy', 'Company Hierarchy', 162, 20, 'MnuUtility', 'AEDP', NULL, 'Y', 4417, NULL, '007020', 4.0, NULL, NULL, 1);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Utility', 'MnuCustomFields', 'Custom Fields', 155, 13, 'MnuUtility', 'AEDP', NULL, 'Y', 4410, NULL, '007013', 3.0, NULL, NULL, 1);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Utility', 'MnuDatabase', 'Database', 153, 11, 'MnuUtility', 'AEDP', NULL, 'Y', 4408, NULL, '007011', 2.0, NULL, NULL, 1);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Utility', 'MnuStructure', 'Structure', 143, 1, 'MnuUtility', 'AEDP', NULL, 'Y', 4398, NULL, '007001', 5.0, NULL, NULL, 1);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Utility', 'MnuUser', 'User', 148, 6, 'MnuUtility', 'AEDP', NULL, 'Y', 4403, NULL, '007006', 5.0, NULL, NULL, 1);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Utility', 'MnuVoucherType', 'Voucher Type', 158, 16, 'MnuUtility', 'AEDP', NULL, 'Y', 4413, NULL, '007016', 4.0, NULL, NULL, 1);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Utility', 'MnuVoucherTypeMaster', 'Voucher Type Master', 159, 17, 'MnuVoucherType', 'AEDP', NULL, 'Y', 4414, NULL, '007016017', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Utility', 'MnuVoucherTypePrintSetting', 'Voucher Type Print Setting', 160, 18, 'MnuVoucherType', 'AEDP', NULL, 'Y', 4415, NULL, '007016018', 1.0, NULL, NULL, NULL);
                    INSERT INTO User_Permission
                    (UserName, MnuModule, MnuName, MnuText, SNo, MnuLevel, Parent, Permission, ReportFor, Active, RowId, UpLoadDate, MainStreamCode, GroupLevel, ControlPermissionGroups, LogSystem, IsParent)
                    VALUES('SA', 'Utility', 'MnuVoucherTypeSetting', 'Voucher Type Setting', 161, 19, 'MnuVoucherType', 'AEDP', NULL, 'Y', 4416, NULL, '007016019', 1.0, NULL, NULL, NULL);
                   "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If
    End Sub

    Private Sub FCreateTable_User_Control_Permission()
        Dim mQry As String
        If Not AgL.IsTableExist("User_Control_Permission", AgL.GcnMain) Then
            mQry = "
                    CREATE TABLE [User_Control_Permission] (
                       [UserName] nvarchar(10) NOT NULL COLLATE NOCASE,
                       [MnuModule] nvarchar(50) NOT NULL COLLATE NOCASE,
                       [MnuName] nvarchar(100) NOT NULL COLLATE NOCASE,
                       [MnuText] nvarchar(100) COLLATE NOCASE,
                       [GroupText] nvarchar(100) NOT NULL COLLATE NOCASE,
                       [GroupName] nvarchar(100) NOT NULL COLLATE NOCASE,
                       [UpLoadDate] datetime,
                       PRIMARY KEY ([UserName], [MnuModule], [MnuName], [GroupText], [GroupName])
                    );
                   "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If
    End Sub

    Private Sub FCreateTable_AcGroup()
        Dim mQry As String
        If Not AgL.IsTableExist("AcGroup", AgL.GcnMain) Then
            mQry = "

                CREATE TABLE [AcGroup] (
	                `GroupCode`	nvarchar ( 4 ) NOT NULL COLLATE NOCASE,
	                `SNo`	tinyint,
	                `GroupName`	nvarchar ( 50 ) COLLATE NOCASE,
	                `ContraGroupName`	nvarchar ( 50 ) COLLATE NOCASE,
	                `GroupUnder`	nvarchar ( 4 ) COLLATE NOCASE,
	                `GroupNature`	nvarchar ( 1 ) COLLATE NOCASE,
	                `Nature`	nvarchar ( 15 ) COLLATE NOCASE,
	                `SysGroup`	nvarchar ( 1 ) COLLATE NOCASE,
	                `U_Name`	nvarchar ( 10 ) COLLATE NOCASE,
	                `U_EntDt`	datetime,
	                `U_AE`	nvarchar ( 1 ) COLLATE NOCASE,
	                `TradingYn`	nvarchar ( 1 ) COLLATE NOCASE,
	                `MainGrCode`	nvarchar ( 255 ) COLLATE NOCASE,
	                `BlOrd`	float,
	                `MainGrLen`	int,
	                `ID`	float,
	                `Site_Code`	nvarchar ( 2 ) COLLATE NOCASE,
	                `GroupNameBiLang`	nvarchar ( 50 ) COLLATE NOCASE,
	                `GroupLevel`	float,
	                `CurrentCount`	float,
	                `CurrentBalance`	float,
	                `SubLedYn`	nvarchar ( 1 ) COLLATE NOCASE,
	                `AliasYn`	nvarchar ( 1 ) COLLATE NOCASE,
	                `GroupHelp`	nvarchar ( 50 ) COLLATE NOCASE,
	                `LastYearBalance`	float,
	                `RowId`	bigint,
	                `UpLoadDate`	datetime,
	                `Transfered`	nvarchar ( 1 ) COLLATE NOCASE,
	                CONSTRAINT `FK_AcGroup_AcGroup_GroupUnder` FOREIGN KEY(`GroupUnder`) REFERENCES `AcGroup`(`GroupCode`) ON DELETE NO ACTION ON UPDATE NO ACTION,
	                PRIMARY KEY(`GroupCode`),
	                CONSTRAINT `FK_AcGroup_SiteMast_Site_Code` FOREIGN KEY(`Site_Code`) REFERENCES `SiteMast`(`Code`) ON DELETE NO ACTION ON UPDATE NO ACTION
                );


                "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If
    End Sub

    Private Sub FSeedTable_AcGroup()
        Dim mQry As String

        If AgL.FillData("Select * from AcGroup limit 1", AgL.GcnMain).tables(0).Rows.Count = 0 Then
            mQry = " 
                    INSERT INTO AcGroup
                    (GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE, TradingYn, MainGrCode, BlOrd, MainGrLen, ID, Site_Code, GroupNameBiLang, GroupLevel, CurrentCount, CurrentBalance, SubLedYn, AliasYn, GroupHelp, LastYearBalance, RowId, UpLoadDate, Transfered)
                    VALUES('0001', NULL, 'Capital Account', 'Capital Account', NULL, 'L', 'Others', 'Y', 'SA', '2011-04-09 00:00:00', 'E', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, NULL, NULL);
                    INSERT INTO AcGroup
                    (GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE, TradingYn, MainGrCode, BlOrd, MainGrLen, ID, Site_Code, GroupNameBiLang, GroupLevel, CurrentCount, CurrentBalance, SubLedYn, AliasYn, GroupHelp, LastYearBalance, RowId, UpLoadDate, Transfered)
                    VALUES('0002', NULL, 'Loan (Liability)', 'Loan (Liability)', NULL, 'L', 'Others', 'Y', 'SA', '2011-04-09 00:00:00', 'E', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 2, NULL, NULL);
                    INSERT INTO AcGroup
                    (GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE, TradingYn, MainGrCode, BlOrd, MainGrLen, ID, Site_Code, GroupNameBiLang, GroupLevel, CurrentCount, CurrentBalance, SubLedYn, AliasYn, GroupHelp, LastYearBalance, RowId, UpLoadDate, Transfered)
                    VALUES('0003', NULL, 'Current Liabilities', 'Current Liabilities', NULL, 'L', 'Others', 'Y', 'SA', '2011-04-09 00:00:00', 'E', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 3, NULL, NULL);
                    INSERT INTO AcGroup
                    (GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE, TradingYn, MainGrCode, BlOrd, MainGrLen, ID, Site_Code, GroupNameBiLang, GroupLevel, CurrentCount, CurrentBalance, SubLedYn, AliasYn, GroupHelp, LastYearBalance, RowId, UpLoadDate, Transfered)
                    VALUES('0004', NULL, 'Fixed Assets', 'Fixed Assets', NULL, 'A', 'Others', 'Y', 'SA', '2011-04-09 00:00:00', 'E', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 4, NULL, NULL);
                    INSERT INTO AcGroup
                    (GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE, TradingYn, MainGrCode, BlOrd, MainGrLen, ID, Site_Code, GroupNameBiLang, GroupLevel, CurrentCount, CurrentBalance, SubLedYn, AliasYn, GroupHelp, LastYearBalance, RowId, UpLoadDate, Transfered)
                    VALUES('0005', NULL, 'Investments', 'Investments', NULL, 'A', 'Others', 'Y', 'SA', '2011-04-09 00:00:00', 'E', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 5, NULL, NULL);
                    INSERT INTO AcGroup
                    (GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE, TradingYn, MainGrCode, BlOrd, MainGrLen, ID, Site_Code, GroupNameBiLang, GroupLevel, CurrentCount, CurrentBalance, SubLedYn, AliasYn, GroupHelp, LastYearBalance, RowId, UpLoadDate, Transfered)
                    VALUES('0006', NULL, 'Current Assets', 'Current Assets', NULL, 'A', 'Others', 'Y', 'SA', '2011-04-09 00:00:00', 'E', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 6, NULL, NULL);
                    INSERT INTO AcGroup
                    (GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE, TradingYn, MainGrCode, BlOrd, MainGrLen, ID, Site_Code, GroupNameBiLang, GroupLevel, CurrentCount, CurrentBalance, SubLedYn, AliasYn, GroupHelp, LastYearBalance, RowId, UpLoadDate, Transfered)
                    VALUES('0007', NULL, 'Branch/Divisions', 'Branch/Divisions', NULL, 'A', 'Others', 'Y', 'SA', '2011-04-09 00:00:00', 'E', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 7, NULL, NULL);
                    INSERT INTO AcGroup
                    (GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE, TradingYn, MainGrCode, BlOrd, MainGrLen, ID, Site_Code, GroupNameBiLang, GroupLevel, CurrentCount, CurrentBalance, SubLedYn, AliasYn, GroupHelp, LastYearBalance, RowId, UpLoadDate, Transfered)
                    VALUES('0008', NULL, 'Misc. Expences (Asset)', 'Misc. Expences (Asset)', NULL, 'A', 'Expenses', 'Y', 'SA', '2011-04-09 00:00:00', 'E', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 8, NULL, NULL);
                    INSERT INTO AcGroup
                    (GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE, TradingYn, MainGrCode, BlOrd, MainGrLen, ID, Site_Code, GroupNameBiLang, GroupLevel, CurrentCount, CurrentBalance, SubLedYn, AliasYn, GroupHelp, LastYearBalance, RowId, UpLoadDate, Transfered)
                    VALUES('0009', NULL, 'Suspense A/c', 'Suspense A/c', NULL, 'A', 'Others', 'Y', 'SA', '2011-04-09 00:00:00', 'E', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 9, NULL, NULL);
                    INSERT INTO AcGroup
                    (GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE, TradingYn, MainGrCode, BlOrd, MainGrLen, ID, Site_Code, GroupNameBiLang, GroupLevel, CurrentCount, CurrentBalance, SubLedYn, AliasYn, GroupHelp, LastYearBalance, RowId, UpLoadDate, Transfered)
                    VALUES('0010', NULL, 'Reserves & Surplus', 'Reserves & Surplus', '0001', 'L', 'Others', 'Y', 'SA', '2011-04-09 00:00:00', 'E', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 10, NULL, NULL);
                    INSERT INTO AcGroup
                    (GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE, TradingYn, MainGrCode, BlOrd, MainGrLen, ID, Site_Code, GroupNameBiLang, GroupLevel, CurrentCount, CurrentBalance, SubLedYn, AliasYn, GroupHelp, LastYearBalance, RowId, UpLoadDate, Transfered)
                    VALUES('0011', NULL, 'Bank OD A/c', 'Bank OD A/c', '0002', 'L', 'Bank', 'Y', 'SA', '2011-04-09 00:00:00', 'E', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 11, NULL, NULL);
                    INSERT INTO AcGroup
                    (GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE, TradingYn, MainGrCode, BlOrd, MainGrLen, ID, Site_Code, GroupNameBiLang, GroupLevel, CurrentCount, CurrentBalance, SubLedYn, AliasYn, GroupHelp, LastYearBalance, RowId, UpLoadDate, Transfered)
                    VALUES('0012', NULL, 'Secured Loans', 'Secured Loans', NULL, 'L', 'Others', 'Y', 'SA', '2011-04-09 00:00:00', 'E', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 12, NULL, NULL);
                    INSERT INTO AcGroup
                    (GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE, TradingYn, MainGrCode, BlOrd, MainGrLen, ID, Site_Code, GroupNameBiLang, GroupLevel, CurrentCount, CurrentBalance, SubLedYn, AliasYn, GroupHelp, LastYearBalance, RowId, UpLoadDate, Transfered)
                    VALUES('0013', NULL, 'Unsecured Loans', 'Unsecured Loans', '0002', 'L', 'Others', 'Y', 'sa', '2013-02-28 00:00:00', 'E', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 13, NULL, 'N');
                    INSERT INTO AcGroup
                    (GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE, TradingYn, MainGrCode, BlOrd, MainGrLen, ID, Site_Code, GroupNameBiLang, GroupLevel, CurrentCount, CurrentBalance, SubLedYn, AliasYn, GroupHelp, LastYearBalance, RowId, UpLoadDate, Transfered)
                    VALUES('0014', NULL, 'Duties & Taxes', 'Duties & Taxes', '0003', 'L', 'Expenses', 'Y', 'SA', '2011-04-09 00:00:00', 'E', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 14, NULL, NULL);
                    INSERT INTO AcGroup
                    (GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE, TradingYn, MainGrCode, BlOrd, MainGrLen, ID, Site_Code, GroupNameBiLang, GroupLevel, CurrentCount, CurrentBalance, SubLedYn, AliasYn, GroupHelp, LastYearBalance, RowId, UpLoadDate, Transfered)
                    VALUES('0015', NULL, 'Provisions', 'Provisions', '0003', 'L', 'Expenses', 'Y', 'SA', '2011-04-09 00:00:00', 'E', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 15, NULL, NULL);
                    INSERT INTO AcGroup
                    (GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE, TradingYn, MainGrCode, BlOrd, MainGrLen, ID, Site_Code, GroupNameBiLang, GroupLevel, CurrentCount, CurrentBalance, SubLedYn, AliasYn, GroupHelp, LastYearBalance, RowId, UpLoadDate, Transfered)
                    VALUES('0016', NULL, 'Sundry Creditors', 'Sundry Creditors', '0003', 'L', 'Supplier', 'Y', 'SA', '2011-04-09 00:00:00', 'E', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 16, NULL, NULL);
                    INSERT INTO AcGroup
                    (GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE, TradingYn, MainGrCode, BlOrd, MainGrLen, ID, Site_Code, GroupNameBiLang, GroupLevel, CurrentCount, CurrentBalance, SubLedYn, AliasYn, GroupHelp, LastYearBalance, RowId, UpLoadDate, Transfered)
                    VALUES('0017', NULL, 'Opening Stock', 'Opening Stock', NULL, 'E', 'Direct', 'Y', 'SA', '2011-04-09 00:00:00', 'E', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 17, NULL, NULL);
                    INSERT INTO AcGroup
                    (GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE, TradingYn, MainGrCode, BlOrd, MainGrLen, ID, Site_Code, GroupNameBiLang, GroupLevel, CurrentCount, CurrentBalance, SubLedYn, AliasYn, GroupHelp, LastYearBalance, RowId, UpLoadDate, Transfered)
                    VALUES('0018', NULL, 'Deposits (Asset)', 'Deposits (Asset)', '0006', 'A', 'Others', 'Y', 'SA', '2011-04-09 00:00:00', 'E', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 18, NULL, NULL);
                    INSERT INTO AcGroup
                    (GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE, TradingYn, MainGrCode, BlOrd, MainGrLen, ID, Site_Code, GroupNameBiLang, GroupLevel, CurrentCount, CurrentBalance, SubLedYn, AliasYn, GroupHelp, LastYearBalance, RowId, UpLoadDate, Transfered)
                    VALUES('0019', NULL, 'Loans & Advances (Asset)', 'Loans & Advances (Asset)', '0006', 'A', 'Others', 'Y', 'SA', '2011-04-09 00:00:00', 'E', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 19, NULL, NULL);
                    INSERT INTO AcGroup
                    (GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE, TradingYn, MainGrCode, BlOrd, MainGrLen, ID, Site_Code, GroupNameBiLang, GroupLevel, CurrentCount, CurrentBalance, SubLedYn, AliasYn, GroupHelp, LastYearBalance, RowId, UpLoadDate, Transfered)
                    VALUES('0020', NULL, 'Sundry Debtors', 'Sundry Debtors', '0006', 'A', 'Customer', 'Y', 'SA', '2011-04-09 00:00:00', 'E', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 20, NULL, NULL);
                    INSERT INTO AcGroup
                    (GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE, TradingYn, MainGrCode, BlOrd, MainGrLen, ID, Site_Code, GroupNameBiLang, GroupLevel, CurrentCount, CurrentBalance, SubLedYn, AliasYn, GroupHelp, LastYearBalance, RowId, UpLoadDate, Transfered)
                    VALUES('0021', NULL, 'Cash-in-Hand', 'Cash-In-Hand', '0006', 'A', 'Cash', 'Y', 'SA', '2011-04-09 00:00:00', 'E', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 21, NULL, NULL);
                    INSERT INTO AcGroup
                    (GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE, TradingYn, MainGrCode, BlOrd, MainGrLen, ID, Site_Code, GroupNameBiLang, GroupLevel, CurrentCount, CurrentBalance, SubLedYn, AliasYn, GroupHelp, LastYearBalance, RowId, UpLoadDate, Transfered)
                    VALUES('0022', NULL, 'Bank Accounts', 'Bank Accounts', '0006', 'A', 'Bank', 'Y', 'SA', '2011-04-09 00:00:00', 'E', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 22, NULL, NULL);
                    INSERT INTO AcGroup
                    (GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE, TradingYn, MainGrCode, BlOrd, MainGrLen, ID, Site_Code, GroupNameBiLang, GroupLevel, CurrentCount, CurrentBalance, SubLedYn, AliasYn, GroupHelp, LastYearBalance, RowId, UpLoadDate, Transfered)
                    VALUES('0023', NULL, 'Sales Accounts', 'Sales Accounts', NULL, 'R', 'Sales', 'Y', 'DEENA', '2011-07-13 00:00:00', 'E', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 23, NULL, NULL);
                    INSERT INTO AcGroup
                    (GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE, TradingYn, MainGrCode, BlOrd, MainGrLen, ID, Site_Code, GroupNameBiLang, GroupLevel, CurrentCount, CurrentBalance, SubLedYn, AliasYn, GroupHelp, LastYearBalance, RowId, UpLoadDate, Transfered)
                    VALUES('0024', NULL, 'Purchase Accounts', 'Purchase Accounts', NULL, 'E', 'Purchase', 'Y', 'SA', '2011-04-09 00:00:00', 'E', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 24, NULL, NULL);
                    INSERT INTO AcGroup
                    (GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE, TradingYn, MainGrCode, BlOrd, MainGrLen, ID, Site_Code, GroupNameBiLang, GroupLevel, CurrentCount, CurrentBalance, SubLedYn, AliasYn, GroupHelp, LastYearBalance, RowId, UpLoadDate, Transfered)
                    VALUES('0025', NULL, 'Direct Incomes', 'Direct Incomes', NULL, 'R', 'Direct', 'Y', 'SA', '2011-04-09 00:00:00', 'E', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 25, NULL, NULL);
                    INSERT INTO AcGroup
                    (GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE, TradingYn, MainGrCode, BlOrd, MainGrLen, ID, Site_Code, GroupNameBiLang, GroupLevel, CurrentCount, CurrentBalance, SubLedYn, AliasYn, GroupHelp, LastYearBalance, RowId, UpLoadDate, Transfered)
                    VALUES('0026', NULL, 'Direct Expenses', 'Direct Expenses', NULL, 'E', 'Direct', 'Y', 'SA', '2011-04-09 00:00:00', 'E', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 26, NULL, NULL);
                    INSERT INTO AcGroup
                    (GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE, TradingYn, MainGrCode, BlOrd, MainGrLen, ID, Site_Code, GroupNameBiLang, GroupLevel, CurrentCount, CurrentBalance, SubLedYn, AliasYn, GroupHelp, LastYearBalance, RowId, UpLoadDate, Transfered)
                    VALUES('0027', NULL, 'Indirect Incomes', 'Indirect Incomes', NULL, 'R', 'Indirect', 'Y', 'SA', '2011-04-09 00:00:00', 'E', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 27, NULL, NULL);
                    INSERT INTO AcGroup
                    (GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE, TradingYn, MainGrCode, BlOrd, MainGrLen, ID, Site_Code, GroupNameBiLang, GroupLevel, CurrentCount, CurrentBalance, SubLedYn, AliasYn, GroupHelp, LastYearBalance, RowId, UpLoadDate, Transfered)
                    VALUES('0028', NULL, 'Indirect Expenses', 'Indirect Expenses', NULL, 'E', 'Indirect', 'Y', 'SA', '2011-04-09 00:00:00', 'E', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 28, NULL, NULL);
                    INSERT INTO AcGroup
                    (GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE, TradingYn, MainGrCode, BlOrd, MainGrLen, ID, Site_Code, GroupNameBiLang, GroupLevel, CurrentCount, CurrentBalance, SubLedYn, AliasYn, GroupHelp, LastYearBalance, RowId, UpLoadDate, Transfered)
                    VALUES('0029', NULL, 'Profit & Loss A/c', 'Profit & Loss A/c', NULL, 'L', 'Others', 'Y', 'SA', '2011-04-09 00:00:00', 'E', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 29, NULL, NULL);
                    INSERT INTO AcGroup
                    (GroupCode, SNo, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE, TradingYn, MainGrCode, BlOrd, MainGrLen, ID, Site_Code, GroupNameBiLang, GroupLevel, CurrentCount, CurrentBalance, SubLedYn, AliasYn, GroupHelp, LastYearBalance, RowId, UpLoadDate, Transfered)
                    VALUES('0030', NULL, 'Closing Stock', 'Closing Stock', NULL, 'R', 'Direct', 'Y', 'SA', '2011-04-09 00:00:00', 'E', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 30, NULL, NULL);
                   "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If
    End Sub

    Private Sub FCreateTable_Subgroup()
        Dim mQry As String

        If Not AgL.IsTableExist("SubGroup", AgL.GcnMain) Then
            mQry = "

                CREATE TABLE [SubGroup] (
                   [SubCode] nvarchar(10) NOT NULL COLLATE NOCASE,
                   [Site_Code] nvarchar(2) COLLATE NOCASE,
                   [Div_Code] nvarchar(1) COLLATE NOCASE,
                   [SiteList] nvarchar(500) COLLATE NOCASE,
                   [NamePrefix] nvarchar(10) COLLATE NOCASE,
                   [Name] nvarchar(123) COLLATE NOCASE,
                   [DispName] nvarchar(100) COLLATE NOCASE,
                   [GroupCode] nvarchar(4) COLLATE NOCASE,
                   [GroupNature] nvarchar(1) COLLATE NOCASE,
                   [ManualCode] nvarchar(20) COLLATE NOCASE,
                   [Nature] nvarchar(11) COLLATE NOCASE,
                   [Add1] nvarchar(50) COLLATE NOCASE,
                   [Add2] nvarchar(50) COLLATE NOCASE,
                   [Add3] nvarchar(50) COLLATE NOCASE,
                   [CityCode] nvarchar(6) COLLATE NOCASE,
                   [CountryCode] nvarchar(6) COLLATE NOCASE,
                   [PIN] nvarchar(6) COLLATE NOCASE,
                   [Phone] nvarchar(35) COLLATE NOCASE,
                   [Mobile] nvarchar(35) COLLATE NOCASE,
                   [FAX] nvarchar(35) COLLATE NOCASE,
                   [EMail] nvarchar(100) COLLATE NOCASE,
                   [CSTNo] nvarchar(40) COLLATE NOCASE,
                   [LSTNo] nvarchar(40) COLLATE NOCASE,
                   [TINNo] nvarchar(20) COLLATE NOCASE,
                   [PAN] nvarchar(20) COLLATE NOCASE,
                   [TDS_Catg] nvarchar(6) COLLATE NOCASE,
                   [ActiveYN] nvarchar(1) COLLATE NOCASE,
                   [CreditLimit] float,
                   [CreditDays] smallint,
                   [DueDays] int,
                   [ContactPerson] nvarchar(100) COLLATE NOCASE,
                   [Party_Type] int,
                   [PAdd1] nvarchar(50) COLLATE NOCASE,
                   [PAdd2] nvarchar(50) COLLATE NOCASE,
                   [PAdd3] nvarchar(50) COLLATE NOCASE,
                   [PCityCode] nvarchar(6) COLLATE NOCASE,
                   [PCountryCode] nvarchar(7) COLLATE NOCASE,
                   [PPin] nvarchar(6) COLLATE NOCASE,
                   [PPhone] nvarchar(35) COLLATE NOCASE,
                   [PMobile] nvarchar(35) COLLATE NOCASE,
                   [PFax] nvarchar(35) COLLATE NOCASE,
                   [Curr_Bal] float,
                   [OpBal_DocId] nvarchar(21) COLLATE NOCASE,
                   [FatherName] nvarchar(100) COLLATE NOCASE,
                   [FatherNamePrefix] nvarchar(10) COLLATE NOCASE,
                   [HusbandName] nvarchar(100) COLLATE NOCASE,
                   [HusbandNamePrefix] nvarchar(10) COLLATE NOCASE,
                   [DOB] datetime,
                   [Remark] nvarchar(1) COLLATE NOCASE,
                   [Location] nvarchar(1) COLLATE NOCASE,
                   [U_Name] nvarchar(10) COLLATE NOCASE,
                   [U_EntDt] datetime,
                   [U_AE] nvarchar(1) COLLATE NOCASE,
                   [Edit_Date] datetime,
                   [ModifiedBy] nvarchar(10) COLLATE NOCASE,
                   [ApprovedBy] nvarchar(10) COLLATE NOCASE,
                   [StCategory] nvarchar(6) COLLATE NOCASE,
                   [SiteStr] nvarchar(50) COLLATE NOCASE,
                   [STRegNo] nvarchar(25) COLLATE NOCASE,
                   [ECCNo] nvarchar(35) COLLATE NOCASE,
                   [EXREGNO] nvarchar(25) COLLATE NOCASE,
                   [CEXRANGE] nvarchar(25) COLLATE NOCASE,
                   [CEXDIV] nvarchar(25) COLLATE NOCASE,
                   [COMMRATE] nvarchar(25) COLLATE NOCASE,
                   [VATNo] nvarchar(35) COLLATE NOCASE,
                   [CommonAc] bit DEFAULT '1',
                   [RowId] bigint,
                   [UpLoadDate] datetime,
                   [ChequeReport] nvarchar(50) COLLATE NOCASE,
                   [EntryBy] nvarchar(10) COLLATE NOCASE,
                   [EntryDate] datetime,
                   [EntryType] nvarchar(10) COLLATE NOCASE,
                   [EntryStatus] nvarchar(10) COLLATE NOCASE,
                   [ApproveBy] nvarchar(10) COLLATE NOCASE,
                   [ApproveDate] datetime,
                   [MoveToLog] nvarchar(10) COLLATE NOCASE,
                   [IsDeleted] bit,
                   [MoveToLogDate] datetime,
                   [Status] nvarchar(20) COLLATE NOCASE,
                   [SisterConcernYn] bit,
                   [UID] uniqueidentifier COLLATE NOCASE,
                   [SalesTaxPostingGroup] nvarchar(20) COLLATE NOCASE,
                   [ExcisePostingGroup] nvarchar(20) COLLATE NOCASE,
                   [EntryTaxPostingGroup] nvarchar(20) COLLATE NOCASE,
                   [TDSCat_Description] nvarchar(6) COLLATE NOCASE,
                   [MasterType] nvarchar(20) COLLATE NOCASE,
                   [Currency] nvarchar(10) COLLATE NOCASE,
                   [SisterConcernSite] nvarchar(2) COLLATE NOCASE,
                   [CostCenter] varchar(6) COLLATE NOCASE,
                   [Parent] varchar(10) COLLATE NOCASE,
                   [LedgerGroup] varchar(10) COLLATE NOCASE,
                   [Zone] varchar(6) COLLATE NOCASE,
                   [DuplicateTIN] varchar(1) COLLATE NOCASE,
                   [Distributor] varchar(10) COLLATE NOCASE,
                   [STNo] varchar(40) COLLATE NOCASE,
                   [IECCode] varchar(35) COLLATE NOCASE,
                   [Range] varchar(35) COLLATE NOCASE,
                   [Division] varchar(35) COLLATE NOCASE,
                   [PartyType] varchar(1) COLLATE NOCASE,
                   [PartyCat] varchar(1) COLLATE NOCASE,
                   [ECCCode] varchar(35) COLLATE NOCASE,
                   [Excise] varchar(35) COLLATE NOCASE,
                   [FBTOnPer] float,
                   [FBTPer] float,
                   [PolicyNo] varchar(50) COLLATE NOCASE,
                   [Transfered] varchar(1) COLLATE NOCASE,
                   [DivisionList] nvarchar(500) COLLATE NOCASE,
                   [Upline] varchar(2147483647) COLLATE NOCASE,
                   [Department] varchar(10) COLLATE NOCASE,
                   [Designation] varchar(10) COLLATE NOCASE,
                   [Guarantor] varchar(100) COLLATE NOCASE,
                   [InsideOutside] varchar(10) COLLATE NOCASE,
                   [DrugLicenseNo] nvarchar(50) COLLATE NOCASE,
                   [PartyRateGroup] varchar(10) COLLATE NOCASE,
                   PRIMARY KEY ([SubCode]),
                   CONSTRAINT [FK_SubGroup_City_CityCode] FOREIGN KEY ([CityCode])
                      REFERENCES [City]([CityCode]) ON DELETE NO ACTION ON UPDATE NO ACTION,
                   CONSTRAINT [FK_SubGroup_AcGroup_GroupCode] FOREIGN KEY ([GroupCode])
                      REFERENCES [AcGroup]([GroupCode]) ON DELETE NO ACTION ON UPDATE NO ACTION,
                   CONSTRAINT [FK_SubGroup_City_PCityCode] FOREIGN KEY ([PCityCode])
                      REFERENCES [City]([CityCode]) ON DELETE NO ACTION ON UPDATE NO ACTION,
                   CONSTRAINT [FK_SubGroup_SiteMast_SisterConcernSite] FOREIGN KEY ([SisterConcernSite])
                      REFERENCES [SiteMast]([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION,
                   CONSTRAINT [FK_SubGroup_SiteMast_Site_Code] FOREIGN KEY ([Site_Code])
                      REFERENCES [SiteMast]([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION
                );

            "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If
    End Sub

    Private Sub FSeedTable_Subgroup()
        Dim mQry As String

        If AgL.FillData("Select * from SubGroup limit 1", AgL.GcnMain).tables(0).Rows.Count = 0 Then
            mQry = " 
                    INSERT INTO SubGroup
                    (SubCode, Site_Code, Div_Code, SiteList, NamePrefix, Name, DispName, GroupCode, GroupNature, ManualCode, Nature, Add1, Add2, Add3, CityCode, CountryCode, PIN, Phone, Mobile, FAX, EMail, CSTNo, LSTNo, TINNo, PAN, TDS_Catg, ActiveYN, CreditLimit, CreditDays, DueDays, ContactPerson, Party_Type, PAdd1, PAdd2, PAdd3, PCityCode, PCountryCode, PPin, PPhone, PMobile, PFax, Curr_Bal, OpBal_DocId, FatherName, FatherNamePrefix, HusbandName, HusbandNamePrefix, DOB, Remark, Location, U_Name, U_EntDt, U_AE, Edit_Date, ModifiedBy, ApprovedBy, StCategory, SiteStr, STRegNo, ECCNo, EXREGNO, CEXRANGE, CEXDIV, COMMRATE, VATNo, CommonAc, RowId, UpLoadDate, ChequeReport, EntryBy, EntryDate, EntryType, EntryStatus, ApproveBy, ApproveDate, MoveToLog, IsDeleted, MoveToLogDate, Status, SisterConcernYn, UID, SalesTaxPostingGroup, ExcisePostingGroup, EntryTaxPostingGroup, TDSCat_Description, MasterType, Currency, SisterConcernSite, CostCenter, Parent, LedgerGroup, Zone, DuplicateTIN, Distributor, STNo, IECCode, Range, Division, PartyType, PartyCat, ECCCode, Excise, FBTOnPer, FBTPer, PolicyNo, Transfered, DivisionList, Upline, Department, Designation, Guarantor, InsideOutside, DrugLicenseNo, PartyRateGroup)
                    VALUES('|PARTY|', '1', 'D', '|1|', NULL, '|PARTY|', '|PARTY|', NULL, 'A', 'Party', 'Others', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'SA', '2008-10-21 00:00:00', 'A', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'Rs.', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
                    INSERT INTO SubGroup
                    (SubCode, Site_Code, Div_Code, SiteList, NamePrefix, Name, DispName, GroupCode, GroupNature, ManualCode, Nature, Add1, Add2, Add3, CityCode, CountryCode, PIN, Phone, Mobile, FAX, EMail, CSTNo, LSTNo, TINNo, PAN, TDS_Catg, ActiveYN, CreditLimit, CreditDays, DueDays, ContactPerson, Party_Type, PAdd1, PAdd2, PAdd3, PCityCode, PCountryCode, PPin, PPhone, PMobile, PFax, Curr_Bal, OpBal_DocId, FatherName, FatherNamePrefix, HusbandName, HusbandNamePrefix, DOB, Remark, Location, U_Name, U_EntDt, U_AE, Edit_Date, ModifiedBy, ApprovedBy, StCategory, SiteStr, STRegNo, ECCNo, EXREGNO, CEXRANGE, CEXDIV, COMMRATE, VATNo, CommonAc, RowId, UpLoadDate, ChequeReport, EntryBy, EntryDate, EntryType, EntryStatus, ApproveBy, ApproveDate, MoveToLog, IsDeleted, MoveToLogDate, Status, SisterConcernYn, UID, SalesTaxPostingGroup, ExcisePostingGroup, EntryTaxPostingGroup, TDSCat_Description, MasterType, Currency, SisterConcernSite, CostCenter, Parent, LedgerGroup, Zone, DuplicateTIN, Distributor, STNo, IECCode, Range, Division, PartyType, PartyCat, ECCCode, Excise, FBTOnPer, FBTPer, PolicyNo, Transfered, DivisionList, Upline, Department, Designation, Guarantor, InsideOutside, DrugLicenseNo, PartyRateGroup)
                    VALUES('SALE', '1', 'D', '|1|', NULL, 'SALE A/C', 'SALE A/C', '0023', '', 'SALE', 'Customer', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'Rs.', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
                    INSERT INTO SubGroup
                    (SubCode, Site_Code, Div_Code, SiteList, NamePrefix, Name, DispName, GroupCode, GroupNature, ManualCode, Nature, Add1, Add2, Add3, CityCode, CountryCode, PIN, Phone, Mobile, FAX, EMail, CSTNo, LSTNo, TINNo, PAN, TDS_Catg, ActiveYN, CreditLimit, CreditDays, DueDays, ContactPerson, Party_Type, PAdd1, PAdd2, PAdd3, PCityCode, PCountryCode, PPin, PPhone, PMobile, PFax, Curr_Bal, OpBal_DocId, FatherName, FatherNamePrefix, HusbandName, HusbandNamePrefix, DOB, Remark, Location, U_Name, U_EntDt, U_AE, Edit_Date, ModifiedBy, ApprovedBy, StCategory, SiteStr, STRegNo, ECCNo, EXREGNO, CEXRANGE, CEXDIV, COMMRATE, VATNo, CommonAc, RowId, UpLoadDate, ChequeReport, EntryBy, EntryDate, EntryType, EntryStatus, ApproveBy, ApproveDate, MoveToLog, IsDeleted, MoveToLogDate, Status, SisterConcernYn, UID, SalesTaxPostingGroup, ExcisePostingGroup, EntryTaxPostingGroup, TDSCat_Description, MasterType, Currency, SisterConcernSite, CostCenter, Parent, LedgerGroup, Zone, DuplicateTIN, Distributor, STNo, IECCode, Range, Division, PartyType, PartyCat, ECCCode, Excise, FBTOnPer, FBTPer, PolicyNo, Transfered, DivisionList, Upline, Department, Designation, Guarantor, InsideOutside, DrugLicenseNo, PartyRateGroup)
                    VALUES('PURCH', '1', 'D', '|1|', NULL, 'Purchase A/C', 'Purchase A/C', '0024', '', 'Purchase', 'Purchase', '', '', NULL, NULL, NULL, '', '', '', '', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0.0, NULL, 0, '', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'SA', '2013-04-23 00:00:00', 'E', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'Rs.', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0.0, 0.0, NULL, 'N', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
                    INSERT INTO SubGroup
                    (SubCode, Site_Code, Div_Code, SiteList, NamePrefix, Name, DispName, GroupCode, GroupNature, ManualCode, Nature, Add1, Add2, Add3, CityCode, CountryCode, PIN, Phone, Mobile, FAX, EMail, CSTNo, LSTNo, TINNo, PAN, TDS_Catg, ActiveYN, CreditLimit, CreditDays, DueDays, ContactPerson, Party_Type, PAdd1, PAdd2, PAdd3, PCityCode, PCountryCode, PPin, PPhone, PMobile, PFax, Curr_Bal, OpBal_DocId, FatherName, FatherNamePrefix, HusbandName, HusbandNamePrefix, DOB, Remark, Location, U_Name, U_EntDt, U_AE, Edit_Date, ModifiedBy, ApprovedBy, StCategory, SiteStr, STRegNo, ECCNo, EXREGNO, CEXRANGE, CEXDIV, COMMRATE, VATNo, CommonAc, RowId, UpLoadDate, ChequeReport, EntryBy, EntryDate, EntryType, EntryStatus, ApproveBy, ApproveDate, MoveToLog, IsDeleted, MoveToLogDate, Status, SisterConcernYn, UID, SalesTaxPostingGroup, ExcisePostingGroup, EntryTaxPostingGroup, TDSCat_Description, MasterType, Currency, SisterConcernSite, CostCenter, Parent, LedgerGroup, Zone, DuplicateTIN, Distributor, STNo, IECCode, Range, Division, PartyType, PartyCat, ECCCode, Excise, FBTOnPer, FBTPer, PolicyNo, Transfered, DivisionList, Upline, Department, Designation, Guarantor, InsideOutside, DrugLicenseNo, PartyRateGroup)
                    VALUES('ROF', '1', 'D', '|1|', NULL, 'Round Off A/c {CNOTE}', 'Round Off A/c {CNOTE}', '0029', '', 'ROF', 'Others', '', '', NULL, NULL, NULL, '', '', '', '', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0.0, NULL, 0, '', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'sa', '2015-08-12 00:00:00', 'E', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'Rs.', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0.0, 0.0, NULL, 'N', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
                    INSERT INTO SubGroup
                    (SubCode, Site_Code, Div_Code, SiteList, NamePrefix, Name, DispName, GroupCode, GroupNature, ManualCode, Nature, Add1, Add2, Add3, CityCode, CountryCode, PIN, Phone, Mobile, FAX, EMail, CSTNo, LSTNo, TINNo, PAN, TDS_Catg, ActiveYN, CreditLimit, CreditDays, DueDays, ContactPerson, Party_Type, PAdd1, PAdd2, PAdd3, PCityCode, PCountryCode, PPin, PPhone, PMobile, PFax, Curr_Bal, OpBal_DocId, FatherName, FatherNamePrefix, HusbandName, HusbandNamePrefix, DOB, Remark, Location, U_Name, U_EntDt, U_AE, Edit_Date, ModifiedBy, ApprovedBy, StCategory, SiteStr, STRegNo, ECCNo, EXREGNO, CEXRANGE, CEXDIV, COMMRATE, VATNo, CommonAc, RowId, UpLoadDate, ChequeReport, EntryBy, EntryDate, EntryType, EntryStatus, ApproveBy, ApproveDate, MoveToLog, IsDeleted, MoveToLogDate, Status, SisterConcernYn, UID, SalesTaxPostingGroup, ExcisePostingGroup, EntryTaxPostingGroup, TDSCat_Description, MasterType, Currency, SisterConcernSite, CostCenter, Parent, LedgerGroup, Zone, DuplicateTIN, Distributor, STNo, IECCode, Range, Division, PartyType, PartyCat, ECCCode, Excise, FBTOnPer, FBTPer, PolicyNo, Transfered, DivisionList, Upline, Department, Designation, Guarantor, InsideOutside, DrugLicenseNo, PartyRateGroup)
                    VALUES('DISCOUNT', '1', 'D', '|1|', NULL, 'Discount A/c {DISCOUNT}', 'Discount A/c', '0026', 'L', 'DISCOUNT', 'Others', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'SA', '2008-10-21 00:00:00', 'A', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'Rs.', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
                "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If
    End Sub

    Private Sub FCreateTable_Enviro()
        Dim mQry As String
        If Not AgL.IsTableExist("Enviro", AgL.GcnMain) Then
            mQry = "
                CREATE TABLE [Enviro] (
                   [ID] nvarchar(4) NOT NULL COLLATE NOCASE,
                   [Site_Code] nvarchar(2) COLLATE NOCASE,
                   [Div_Code] nvarchar(1) COLLATE NOCASE,
                   [CashAc] nvarchar(10) COLLATE NOCASE,
                   [BankAc] nvarchar(10) COLLATE NOCASE,
                   [TdsAc] nvarchar(10) COLLATE NOCASE,
                   [AdditionAc] nvarchar(10) COLLATE NOCASE,
                   [DeductionAc] nvarchar(10) COLLATE NOCASE,
                   [ServiceTaxAc] nvarchar(10) COLLATE NOCASE,
                   [ECessAc] nvarchar(10) COLLATE NOCASE,
                   [RoundOffAc] nvarchar(10) COLLATE NOCASE,
                   [HECessAc] nvarchar(10) COLLATE NOCASE,
                   [ServiceTaxPer] float,
                   [ECessPer] float,
                   [HECessPer] float,
                   [RowId] bigint NOT NULL,
                   [UpLoadDate] datetime,
                   [PreparedBy] nvarchar(10) COLLATE NOCASE,
                   [U_EntDt] datetime,
                   [U_AE] nvarchar(1) COLLATE NOCASE,
                   [Edit_Date] datetime,
                   [ModifiedBy] nvarchar(10) COLLATE NOCASE,
                   [ApprovedBy] nvarchar(10) COLLATE NOCASE,
                   [ApprovedDate] datetime,
                   [GPX1] nvarchar(255) COLLATE NOCASE,
                   [GPX2] nvarchar(255) COLLATE NOCASE,
                   [GPN1] float,
                   [GPN2] float,
                   [DefaultSalesTaxGroupParty] nvarchar(20) COLLATE NOCASE,
                   [DefaultSalesTaxGroupItem] nvarchar(20) COLLATE NOCASE,
                   [PurchOrderShowIndentInLine] bit DEFAULT '0',
                   [IsLinkWithFA] bit,
                   [IsNegativeStockAllowed] bit DEFAULT '1',
                   [IsLotNoApplicable] bit DEFAULT '1',
                   [DefaultDueDays] float,
                   [IsNegetiveStockAllowed] bit,
                   [SaleAc] nvarchar(10) COLLATE NOCASE,
                   [PostingAc] nvarchar(10) COLLATE NOCASE,
                   [PurchaseAc] nvarchar(10) COLLATE NOCASE,
                   [DefaultCurrency] nvarchar(10) COLLATE NOCASE,
                   [DefaultVatCommodityCode] nvarchar(10) COLLATE NOCASE,
                   [IsVisible_PurchOrder] bit DEFAULT '0',
                   [IsVisible_PurchChallan] bit DEFAULT '0',
                   [Caption_Dimension1] nvarchar(20) COLLATE NOCASE,
                   [Caption_Dimension2] nvarchar(20) COLLATE NOCASE,
                   [UrgentList] nvarchar(500) COLLATE NOCASE,
                   [UrgentItemList] varchar(2147483647) COLLATE NOCASE,
                   PRIMARY KEY ([ID]),
                   CONSTRAINT [FK_Enviro_SubGroup_AdditionAc] FOREIGN KEY ([AdditionAc])
                      REFERENCES [SubGroup]([SubCode]) ON DELETE NO ACTION ON UPDATE NO ACTION,
                   CONSTRAINT [FK_Enviro_SubGroup_BankAc] FOREIGN KEY ([BankAc])
                      REFERENCES [SubGroup]([SubCode]) ON DELETE NO ACTION ON UPDATE NO ACTION,
                   CONSTRAINT [FK_Enviro_SubGroup_CashAc] FOREIGN KEY ([CashAc])
                      REFERENCES [SubGroup]([SubCode]) ON DELETE NO ACTION ON UPDATE NO ACTION,
                   CONSTRAINT [FK_Enviro_SubGroup_DeductionAc] FOREIGN KEY ([DeductionAc])
                      REFERENCES [SubGroup]([SubCode]) ON DELETE NO ACTION ON UPDATE NO ACTION,
                   CONSTRAINT [FK_Enviro_SubGroup_ECessAc] FOREIGN KEY ([ECessAc])
                      REFERENCES [SubGroup]([SubCode]) ON DELETE NO ACTION ON UPDATE NO ACTION,
                   CONSTRAINT [FK_Enviro_SubGroup_HECessAc] FOREIGN KEY ([HECessAc])
                      REFERENCES [SubGroup]([SubCode]) ON DELETE NO ACTION ON UPDATE NO ACTION,
                   CONSTRAINT [FK_Enviro_SubGroup_RoundOffAc] FOREIGN KEY ([RoundOffAc])
                      REFERENCES [SubGroup]([SubCode]) ON DELETE NO ACTION ON UPDATE NO ACTION,
                   CONSTRAINT [FK_Enviro_SubGroup_ServiceTaxAc] FOREIGN KEY ([ServiceTaxAc])
                      REFERENCES [SubGroup]([SubCode]) ON DELETE NO ACTION ON UPDATE NO ACTION,
                   CONSTRAINT [FK_Enviro_SiteMast_Site_Code] FOREIGN KEY ([Site_Code])
                      REFERENCES [SiteMast]([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION,
                   CONSTRAINT [FK_Enviro_SubGroup_TdsAc] FOREIGN KEY ([TdsAc])
                      REFERENCES [SubGroup]([SubCode]) ON DELETE NO ACTION ON UPDATE NO ACTION
                );

            "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If


    End Sub

    Private Sub FSeedTable_Enviro()
        Dim mQry As String

        If AgL.FillData("Select * from Enviro limit 1", AgL.GcnMain).tables(0).Rows.Count = 0 Then
            mQry = " INSERT INTO Enviro
                    (ID, Site_Code, Div_Code, CashAc, BankAc, TdsAc, AdditionAc, DeductionAc, ServiceTaxAc, ECessAc, RoundOffAc, HECessAc, ServiceTaxPer, ECessPer, HECessPer, RowId, UpLoadDate, PreparedBy, U_EntDt, U_AE, Edit_Date, ModifiedBy, ApprovedBy, ApprovedDate, GPX1, GPX2, GPN1, GPN2, DefaultSalesTaxGroupParty, DefaultSalesTaxGroupItem, PurchOrderShowIndentInLine, IsLinkWithFA, IsNegativeStockAllowed, IsLotNoApplicable, DefaultDueDays, IsNegetiveStockAllowed, SaleAc, PostingAc, PurchaseAc, DefaultCurrency, DefaultVatCommodityCode, IsVisible_PurchOrder, IsVisible_PurchChallan, Caption_Dimension1, Caption_Dimension2, UrgentList, UrgentItemList)
                    VALUES('1', '1', 'D', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'Local', 'General', 0, NULL, 1, 1, NULL, NULL, 'Sale', '111', NULL, 'Rs.', '2A079001', NULL, NULL, 'D1', 'D2', NULL, NULL);
                "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If
    End Sub

    Private Sub FCreateTable_Unit()
        Dim mQry As String
        If Not AgL.IsTableExist("Unit", AgL.GcnMain) Then
            mQry = "
                     CREATE TABLE [Unit] (
                       [Code] nvarchar(10) NOT NULL COLLATE NOCASE,
                       [IsActive] bit,
                       [DecimalPlaces] int,
                       PRIMARY KEY ([Code])
                    );
                   "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If
    End Sub

    Private Sub FSeedTable_Unit()
        Dim mQry As String

        If AgL.FillData("Select * from Unit limit 1", AgL.GcnMain).tables(0).Rows.Count = 0 Then
            mQry = " 
                    INSERT INTO Unit
                    (Code, IsActive, DecimalPlaces)
                    VALUES('Kg', 1, 1);
                    INSERT INTO Unit
                    (Code, IsActive, DecimalPlaces)
                    VALUES('Meter', 1, 1);
                    INSERT INTO Unit
                    (Code, IsActive, DecimalPlaces)
                    VALUES('Pcs', 1, 1);
                   "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If
    End Sub

    Private Sub FCreateTable_Department()
        Dim mQry As String
        If Not AgL.IsTableExist("Department", AgL.GcnMain) Then
            mQry = "
                    CREATE TABLE [Department] (
                       [Code] nvarchar(10) NOT NULL COLLATE NOCASE,
                       [Description] nvarchar(50) COLLATE NOCASE,
                       [IsDeleted] bit,
                       [EntryBy] nvarchar(10) COLLATE NOCASE,
                       [EntryDate] datetime,
                       [EntryType] nvarchar(10) COLLATE NOCASE,
                       [EntryStatus] nvarchar(10) COLLATE NOCASE,
                       [ApproveBy] nvarchar(10) COLLATE NOCASE,
                       [ApproveDate] datetime,
                       [MoveToLog] nvarchar(10) COLLATE NOCASE,
                       [MoveToLogDate] datetime,
                       [Status] nvarchar(10) COLLATE NOCASE,
                       [Div_Code] nvarchar(1) COLLATE NOCASE,
                       [UID] uniqueidentifier COLLATE NOCASE,
                       [ManualCode] nvarchar(20) COLLATE NOCASE,
                       PRIMARY KEY ([Code])
                    );

                    CREATE UNIQUE INDEX [IX_Department]
                    ON [Department]
                    ([Description]);
                   "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If
    End Sub

    Private Sub FCreateTable_ItemType()
        Dim mQry As String
        If Not AgL.IsTableExist("ItemType", AgL.GcnMain) Then
            mQry = "
                        CREATE TABLE [ItemType] (
                           [Code] nvarchar(20) NOT NULL COLLATE NOCASE,
                           [Name] nvarchar(20) COLLATE NOCASE,
                           [MnuName] nvarchar(100) COLLATE NOCASE,
                           [MnuText] nvarchar(100) COLLATE NOCASE,
                           PRIMARY KEY ([Code])
                        );

                        CREATE UNIQUE INDEX [IX_ITEMTYPE]
                        ON [ItemType]
                        ([NAME]);
                   "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If
    End Sub

    Private Sub FSeedTable_ItemType()
        Dim mQry As String

        If AgL.FillData("Select * from ItemType limit 1", AgL.GcnMain).tables(0).Rows.Count = 0 Then
            mQry = " INSERT INTO ItemType
                    (Code, Name, MnuName, MnuText)
                    VALUES('FM', 'FInished Material', 'MnuFinishedMaterialMaster', 'Finished Material Master');
                    INSERT INTO ItemType
                    (Code, Name, MnuName, MnuText)
                    VALUES('RM', 'Raw Material', 'MnuFinishedMaterialMaster', 'Finished Material Master');
                "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If
    End Sub

    Private Sub FCreateTable_ItemCategory()
        Dim mQry As String
        If Not AgL.IsTableExist("ItemCategory", AgL.GcnMain) Then
            mQry = "
                    CREATE TABLE [ItemCategory] (
                       [Code] nvarchar(10) NOT NULL COLLATE NOCASE,
                       [Description] nvarchar(50) COLLATE NOCASE,
                       [ItemType] nvarchar(10) References ItemType(Code) COLLATE NOCASE,
                       [EntryBy] nvarchar(10) COLLATE NOCASE,
                       [EntryDate] datetime,
                       [EntryType] nvarchar(10) COLLATE NOCASE,
                       [EntryStatus] nvarchar(10) COLLATE NOCASE,
                       [ApproveBy] nvarchar(10) COLLATE NOCASE,
                       [ApproveDate] datetime,
                       [MoveToLog] nvarchar(10) COLLATE NOCASE,
                       [MoveToLogDate] datetime,
                       [IsDeleted] bit,
                       [Status] nvarchar(10) COLLATE NOCASE,
                       [Div_Code] nvarchar(1) COLLATE NOCASE,
                       [PreparedBy] nvarchar(10) COLLATE NOCASE,
                       [U_EntDt] datetime,
                       [U_AE] nvarchar(1) COLLATE NOCASE,
                       [Edit_Date] datetime,
                       [ModifiedBy] nvarchar(10) COLLATE NOCASE,
                       [UID] uniqueidentifier COLLATE NOCASE, IsSystemDefine bit  Default  '0', SalesTaxGroup nVarchar(20)  Default  'Null'   references PostingGroupSalesTaxItem(Description), Unit nVarchar(10)  Default  'Null'   references Unit(code), Department nVarchar(10)  Default  ''   references Department(code), HSN nVarchar(8)  Default  '',
                       PRIMARY KEY ([Code])
                    );

                    CREATE UNIQUE INDEX [IX_ITEMCATEGORY]
                    ON [ItemCategory]
                    ([Description]);
                    "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If
    End Sub

    Private Sub FCreateTable_ItemGroup()
        Dim mQry As String
        If Not AgL.IsTableExist("ItemGroup", AgL.GcnMain) Then
            mQry = "
                CREATE TABLE [ItemGroup] (
                   [Code] nvarchar(10) NOT NULL COLLATE NOCASE,
                   [Description] nvarchar(50) COLLATE NOCASE,
                   [ItemType] nvarchar(20) COLLATE NOCASE,
                   [ItemCategory] nvarchar(10) COLLATE NOCASE,
                   [EntryBy] nvarchar(10) COLLATE NOCASE,
                   [EntryDate] datetime,
                   [EntryType] nvarchar(10) COLLATE NOCASE,
                   [EntryStatus] nvarchar(10) COLLATE NOCASE,
                   [ApproveBy] nvarchar(10) COLLATE NOCASE,
                   [ApproveDate] datetime,
                   [MoveToLog] nvarchar(10) COLLATE NOCASE,
                   [MoveToLogDate] datetime,
                   [IsDeleted] bit,
                   [Status] nvarchar(10) COLLATE NOCASE,
                   [Div_Code] nvarchar(1) COLLATE NOCASE,
                   [PreparedBy] nvarchar(10) COLLATE NOCASE,
                   [U_EntDt] datetime,
                   [U_AE] nvarchar(1) COLLATE NOCASE,
                   [Edit_Date] datetime,
                   [ModifiedBy] nvarchar(10) COLLATE NOCASE,
                   [UID] uniqueidentifier COLLATE NOCASE, IsSystemDefine bit  Default  '0',
                   PRIMARY KEY ([Code]),
                   CONSTRAINT [FK_ItemGroup_ItemCategory_ItemCategory] FOREIGN KEY ([ItemCategory])
                      REFERENCES [ItemCategory]([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION,
                   CONSTRAINT [FK_ItemGroup_ItemType_ItemType] FOREIGN KEY ([ItemType])
                      REFERENCES [ItemType]([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION
                );

                    CREATE UNIQUE INDEX [IX_ITEMGroup]
                    ON [ItemGroup]
                    ([Description]);
                "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If
    End Sub

    Private Sub FCreateTable_Item()
        Dim mQry As String
        If Not AgL.IsTableExist("Item", AgL.GcnMain) Then
            mQry = "
                CREATE TABLE [Item] (
                   [Code] nvarchar(10) NOT NULL COLLATE NOCASE,
                   [ManualCode] nvarchar(100) COLLATE NOCASE,
                   [Description] nvarchar(255) COLLATE NOCASE,
                   [DisplayName] nvarchar(100) COLLATE NOCASE,
                   [Unit] nvarchar(10) COLLATE NOCASE,
                   [Measure] float,
                   [MeasureUnit] nvarchar(10) COLLATE NOCASE,
                   [ItemGroup] nvarchar(10) COLLATE NOCASE,
                   [ItemCategory] nvarchar(10) COLLATE NOCASE,
                   [ItemType] nvarchar(20) COLLATE NOCASE,
                   [Godown] nvarchar(10) COLLATE NOCASE,
                   [GodownSection] nvarchar(20) COLLATE NOCASE,
                   [QcGroup] nvarchar(10) COLLATE NOCASE,
                   [CurrentStock] float,
                   [CurrentIssued] float,
                   [CurrentRequisition] float,
                   [IsDeleted] bit,
                   [UpcCode] nvarchar(20) COLLATE NOCASE,
                   [Bom] nvarchar(10) COLLATE NOCASE,
                   [Rate] float,
                   [ItemImportExportGroup] nvarchar(50) COLLATE NOCASE,
                   [EntryBy] nvarchar(10) COLLATE NOCASE,
                   [EntryDate] datetime,
                   [EntryType] nvarchar(10) COLLATE NOCASE,
                   [EntryStatus] nvarchar(10) COLLATE NOCASE,
                   [ApproveBy] nvarchar(10) COLLATE NOCASE,
                   [ApproveDate] datetime,
                   [MoveToLog] nvarchar(10) COLLATE NOCASE,
                   [MoveToLogDate] datetime,
                   [Status] nvarchar(10) COLLATE NOCASE,
                   [Div_Code] nvarchar(1) COLLATE NOCASE,
                   [UID] uniqueidentifier COLLATE NOCASE,
                   [SalesTaxPostingGroup] nvarchar(20) COLLATE NOCASE,
                   [ExcisePostingGroup] nvarchar(20) COLLATE NOCASE,
                   [EntryTaxPostingGroup] nvarchar(20) COLLATE NOCASE,
                   [LastPurchaseRate] float,
                   [LastPurchaseDate] datetime,
                   [LastPurchaseInvoice] nvarchar(21) COLLATE NOCASE,
                   [Specification] nvarchar(255) COLLATE NOCASE,
                   [ProcessSequence] nvarchar(10) COLLATE NOCASE,
                   [ItemInvoiceGroup] nvarchar(10) COLLATE NOCASE,
                   [StockYN] bit,
                   [StockOn] nvarchar(10) COLLATE NOCASE,
                   [PcsPerMeasure] float,
                   [Prod_Measure] float,
                   [Colour] nvarchar(50) COLLATE NOCASE,
                   [Quality] nvarchar(10) COLLATE NOCASE,
                   [Construction] nvarchar(10) COLLATE NOCASE,
                   [Collection] nvarchar(10) COLLATE NOCASE,
                   [BillingOn] nvarchar(20) COLLATE NOCASE,
                   [Manufacturer] nvarchar(10) COLLATE NOCASE,
                   [VatCommodityCode] nvarchar(10) COLLATE NOCASE,
                   [ReorderLevel] float,
                   [Design] nvarchar(10) COLLATE NOCASE,
                   [Size] nvarchar(10) COLLATE NOCASE,
                   [Deal] nvarchar(20) COLLATE NOCASE,
                   [ProfitMarginPer] float,
                   [ServiceTaxYN] varchar(1) COLLATE NOCASE,
                   [DeliveryMeasure] nvarchar(10) COLLATE NOCASE,
                   [TariffHead] nvarchar(10) COLLATE NOCASE,
                   [ItemNature] nvarchar(10) COLLATE NOCASE,
                   [ProcessList] varchar(2147483647) COLLATE NOCASE,
                   [ProdBatchQty] float,
                   [ProdBatchUnit] nvarchar(10) COLLATE NOCASE,
                   [SubCode] nvarchar(10) COLLATE NOCASE,
                   [CostCenter] nvarchar(21) COLLATE NOCASE,
                   [CustomFields] nvarchar(10) COLLATE NOCASE,
                   [GenTable] nvarchar(100) COLLATE NOCASE,
                   [GenCode] nvarchar(10) COLLATE NOCASE,
                   [Operators_Required] smallint, Gross_Weight Float  Default  '0', IsSystemDefine bit  Default  '0', IsRestricted_InTransaction bit  Default  '0', IsMandatory_UnitConversion bit  Default  '0', HSN nVarchar(8)  Default  '',
                   PRIMARY KEY ([Code]),
                   CONSTRAINT [FK_Item_BOM_Bom] FOREIGN KEY ([Bom])
                      REFERENCES [BOM]([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION,
                   CONSTRAINT [FK_Item_Division_Div_Code] FOREIGN KEY ([Div_Code])
                      REFERENCES [Division]([Div_Code]) ON DELETE NO ACTION ON UPDATE NO ACTION,
                   CONSTRAINT [FK_Item_Godown_Godown] FOREIGN KEY ([Godown])
                      REFERENCES [Godown]([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION,
                   CONSTRAINT [FK_Item_ItemGroup_ItemGroup] FOREIGN KEY ([ItemGroup])
                      REFERENCES [ItemGroup]([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION,
                   CONSTRAINT [FK_Item_Unit_MeasureUnit] FOREIGN KEY ([MeasureUnit])
                      REFERENCES [Unit]([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION,
                   CONSTRAINT [FK_Item_ProcessSequence_ProcessSequence] FOREIGN KEY ([ProcessSequence])
                      REFERENCES [ProcessSequence]([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION,
                   CONSTRAINT [FK_Item_QcGroup_QcGroup] FOREIGN KEY ([QcGroup])
                      REFERENCES [QcGroup]([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION,
                   CONSTRAINT [FK_Item_PostingGroupSalesTaxItem_SalesTaxPostingGroup] FOREIGN KEY ([SalesTaxPostingGroup])
                      REFERENCES [PostingGroupSalesTaxItem]([Description]) ON DELETE NO ACTION ON UPDATE NO ACTION,
                   CONSTRAINT [FK_Item_TariffHead_TariffHead] FOREIGN KEY ([TariffHead])
                      REFERENCES [TariffHead]([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION,
                   CONSTRAINT [FK_Item_Unit_Unit] FOREIGN KEY ([Unit])
                      REFERENCES [Unit]([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION
                );


                CREATE INDEX [IX_Item_Description]
                ON [Item]
                ([Description]);

                "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If
    End Sub

    Private Sub FCreateTable_SubGroupType()
        Dim mQry As String
        If Not AgL.IsTableExist("SubGroupType", AgL.GcnMain) Then
            mQry = "
                        CREATE TABLE [SubGroupType] (
                           [Party_Type] int NOT NULL,
                           [Description] nvarchar(50) COLLATE NOCASE,
                           [Div_Code] nvarchar(1) COLLATE NOCASE,
                           [Site_Code] nvarchar(2) COLLATE NOCASE,
                           [U_Name] nvarchar(10) COLLATE NOCASE,
                           [U_EntDt] datetime,
                           [U_AE] nvarchar(1) COLLATE NOCASE,
                           [UpLoadDate] datetime,
                           PRIMARY KEY ([Party_Type])
                        );
                    "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If
    End Sub

    Private Sub FCreateTable_CostCenterMast()
        Dim mQry As String
        If Not AgL.IsTableExist("CostCenterMast", AgL.GcnMain) Then
            mQry = "
                    CREATE TABLE [CostCenterMast] (
                       [Code] varchar(21) NOT NULL COLLATE NOCASE,
                       [Name] varchar(30) NOT NULL COLLATE NOCASE,
                       [U_Name] varchar(10) COLLATE NOCASE,
                       [U_EntDt] datetime,
                       [U_AE] varchar(1) COLLATE NOCASE,
                       [Transfered] varchar(1) COLLATE NOCASE
                    );
                    "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If
    End Sub

    Private Sub FCreateTable_PostingGroupSalesTaxParty()
        Dim mQry As String
        If Not AgL.IsTableExist("PostingGroupSalesTaxParty", AgL.GcnMain) Then
            mQry = "
                        CREATE TABLE [PostingGroupSalesTaxParty] (
                           [Description] nvarchar(20) NOT NULL COLLATE NOCASE,
                           [Active] bit DEFAULT '1',
                           [Nature] varchar(10) COLLATE NOCASE,
                           PRIMARY KEY ([Description])
                        );
                    "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If
    End Sub

    Private Sub FCreateTable_RateListDetail()
        Dim mQry As String
        If Not AgL.IsTableExist("RateListDetail", AgL.GcnMain) Then
            mQry = "
                    CREATE TABLE [RateListDetail] (
                       [Code] nvarchar(10) NOT NULL References RateList(Code) COLLATE NOCASE,
                       [Sr] int NOT NULL,
                       [WEF] datetime,
                       [Item] nvarchar(10) References Item(Code) COLLATE NOCASE,
                       [RateType] nvarchar(10) References RateType(Code) COLLATE NOCASE,
                       [Rate] nvarchar(10) COLLATE NOCASE,
                       [UID] uniqueidentifier COLLATE NOCASE,
                       [RatePerQty] nvarchar(10) COLLATE NOCASE,
                       [Process] nvarchar(10) References Process(NCAT) COLLATE NOCASE,
                       [SubCode] nvarchar(10) References Subgroup(Subcode) COLLATE NOCASE,
                       PRIMARY KEY ([Code], [Sr])
                    );
                "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If
    End Sub

    Private Sub FCreateTable_Process()
        Dim mQry As String
        If Not AgL.IsTableExist("Process", AgL.GcnMain) Then
            mQry = "
                    CREATE TABLE [Process] (
                       [NCat] nvarchar(10) NOT NULL COLLATE NOCASE,
                       [ProcessGroup] nvarchar(10) COLLATE NOCASE,
                       [SubCode] nvarchar(10) COLLATE NOCASE,
                       [QcGroup] nvarchar(10) COLLATE NOCASE,
                       [InsideOutside] varchar(10) COLLATE NOCASE,
                       [DefaultJobOrderFor] varchar(20) COLLATE NOCASE,
                       [DefaultBillingType] varchar(20) COLLATE NOCASE,
                       [JobOn] varchar(20) COLLATE NOCASE,
                       [PrevProcess] varchar(10) COLLATE NOCASE,
                       [ProcessIssueNCat] varchar(10) COLLATE NOCASE,
                       [ProcessReceiveNCat] varchar(10) COLLATE NOCASE,
                       [ProcessReturnNCat] varchar(10) COLLATE NOCASE,
                       [ProcessCancelNCat] varchar(10) COLLATE NOCASE,
                       [ProcessInvoiceNCat] varchar(10) COLLATE NOCASE,
                       [Sr] int,
                       [Div_Code] nvarchar(1) COLLATE NOCASE,
                       [Description] nvarchar(50) COLLATE NOCASE,
                       [Code] nvarchar(10) NOT NULL COLLATE NOCASE,
                       [ParentProcess] nvarchar(5) COLLATE NOCASE,
                       [RateGroupTable] varchar(100) COLLATE NOCASE,
                       [MeasureFieldStr] varchar(255) COLLATE NOCASE,
                       [CostCenter] varchar(21) COLLATE NOCASE,
                       [StockHead] varchar(50) COLLATE NOCASE,
                       [IsDeleted] bit,
                       [EntryBy] nvarchar(10) COLLATE NOCASE,
                       [EntryDate] datetime,
                       [EntryType] nvarchar(10) COLLATE NOCASE,
                       [EntryStatus] nvarchar(10) COLLATE NOCASE,
                       [ApproveBy] nvarchar(10) COLLATE NOCASE,
                       [ApproveDate] datetime,
                       [MoveToLog] nvarchar(10) COLLATE NOCASE,
                       [MoveToLogDate] datetime,
                       [Status] nvarchar(10) COLLATE NOCASE,
                       [UID] uniqueidentifier COLLATE NOCASE,
                       PRIMARY KEY ([NCat]),
                       CONSTRAINT [FK_Process_Division_Div_Code] FOREIGN KEY ([Div_Code])
                          REFERENCES [Division]([Div_Code]) ON DELETE NO ACTION ON UPDATE NO ACTION,
                       CONSTRAINT [FK_Process_SubGroup_SubCode] FOREIGN KEY ([SubCode])
                          REFERENCES [SubGroup]([SubCode]) ON DELETE NO ACTION ON UPDATE NO ACTION
                    );

                "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If
    End Sub

    Private Sub FCreateTable_RateList()
        Dim mQry As String
        If Not AgL.IsTableExist("RateList", AgL.GcnMain) Then
            mQry = "
                    CREATE TABLE [RateList] (
                       [Code] nvarchar(10) NOT NULL COLLATE NOCASE,
                       [WEF] datetime,
                       [RateType] nvarchar(10) COLLATE NOCASE,
                       [IsDeleted] bit,
                       [EntryBy] nvarchar(10) COLLATE NOCASE,
                       [EntryDate] datetime,
                       [EntryType] nvarchar(10) COLLATE NOCASE,
                       [EntryStatus] nvarchar(10) COLLATE NOCASE,
                       [ApproveBy] nvarchar(10) COLLATE NOCASE,
                       [ApproveDate] datetime,
                       [MoveToLog] nvarchar(10) COLLATE NOCASE,
                       [MoveToLogDate] datetime,
                       [Status] nvarchar(10) COLLATE NOCASE,
                       [Div_Code] nvarchar(1) COLLATE NOCASE,
                       [UID] uniqueidentifier COLLATE NOCASE,
                       [Site_Code] nvarchar(2) COLLATE NOCASE,
                       [RateInside] float,
                       [RateOutside] float,
                       [SubCode] nvarchar(10) COLLATE NOCASE,
                       [MasterType] nvarchar(20) COLLATE NOCASE,
                       PRIMARY KEY ([Code])
                    );

                "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If
    End Sub

    Private Sub FCreateTable_RateType()
        Dim mQry As String
        If Not AgL.IsTableExist("RateType", AgL.GcnMain) Then
            mQry = "
                    CREATE TABLE [RateType] (
                       [Code] nvarchar(6) NOT NULL COLLATE NOCASE,
                       [Description] nvarchar(50) COLLATE NOCASE,
                       [IsDeleted] bit,
                       [EntryBy] nvarchar(10) COLLATE NOCASE,
                       [EntryDate] datetime,
                       [EntryType] nvarchar(10) COLLATE NOCASE,
                       [EntryStatus] nvarchar(10) COLLATE NOCASE,
                       [ApproveBy] nvarchar(10) COLLATE NOCASE,
                       [ApproveDate] datetime,
                       [MoveToLog] nvarchar(10) COLLATE NOCASE,
                       [MoveToLogDate] datetime,
                       [Status] nvarchar(10) COLLATE NOCASE,
                       [Div_Code] nvarchar(1) COLLATE NOCASE,
                       [UID] uniqueidentifier COLLATE NOCASE, Margin Float  Default  '0', Sr Int  Default  '0',
                       PRIMARY KEY ([Code])
                    );

                    CREATE UNIQUE INDEX [IX_RateType]
                    ON [RateType]
                    ([Description]);
                "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If
    End Sub

    Private Sub FCreateTable_StockAdj()
        Dim mQry As String
        If Not AgL.IsTableExist("StockAdj", AgL.GcnMain) Then
            mQry = "
                    CREATE TABLE [StockAdj] (
                       [StockInDocID] varchar(21) NOT NULL COLLATE NOCASE,
                       [StockInSr] int NOT NULL,
                       [StockOutDocID] varchar(21) NOT NULL COLLATE NOCASE,
                       [StockOutSr] int NOT NULL,
                       [Site_Code] varchar(2) NOT NULL COLLATE NOCASE,
                       [Div_Code] varchar(1) NOT NULL COLLATE NOCASE,
                       [AdjQty] float,
                       PRIMARY KEY ([StockInDocID], [StockInSr], [StockOutDocID], [StockOutSr]),
                       CONSTRAINT [FK_StockAdj_Stock_StockOutDocId_StockOutSr] FOREIGN KEY ([StockOutDocID],[StockOutSr])
                          REFERENCES [Stock]([DocID],[Sr]) ON DELETE NO ACTION ON UPDATE NO ACTION
                    );
                "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If
    End Sub

    Private Sub FCreateTable_Structure()
        Dim mQry As String
        If Not AgL.IsTableExist("Structure", AgL.GcnMain) Then
            mQry = " CREATE TABLE [Structure] ([Code] nVarchar(10) NOT NULL, PRIMARY KEY ([Code]) ); "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If
        AgL.AddFieldSqlite("Structure", "Description", "nVarchar(50)", "", True)
        AgL.AddFieldSqlite("Structure", "HeaderTable", "nVarchar(50)", "", True)
        AgL.AddFieldSqlite("Structure", "LineTable", "nVarchar(50)", "", True)
        AgL.AddFieldSqlite("Structure", "Div_Code", "nVarchar(1)", "", True)
        AgL.AddFieldSqlite("Structure", "Site_Code", "nVarchar(2)", "", True)
        AgL.AddFieldSqlite("Structure", "PreparedBy", "nVarchar(10)", "", True)
        AgL.AddFieldSqlite("Structure", "U_EntDt", "DateTime", "", True)
        AgL.AddFieldSqlite("Structure", "U_AE", "nVarchar(10)", "", True)
        AgL.AddFieldSqlite("Structure", "ModifiedBy", "nVarchar(10)", "", True)
        AgL.AddFieldSqlite("Structure", "Edit_Date", "DateTime", "", True)
        AgL.AddFieldSqlite("Structure", "UploadDate", "DateTime", "", True)


    End Sub

    Private Sub FCreateTable_StructureDetail()
        Dim mQry As String
        If Not AgL.IsTableExist("StructureDetail", AgL.GcnMain) Then
            mQry = " CREATE TABLE [StructureDetail] ([Code] nVarchar(10) NOT NULL, [Sr] int Not Null, PRIMARY KEY ([Code],[Sr]) ); "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If
        AgL.AddFieldSqlite("StructureDetail", "WEF", "DateTime", "", True)
        AgL.AddFieldSqlite("StructureDetail", "Charges", "nVarchar(8)", "", True)
        AgL.AddFieldSqlite("StructureDetail", "Charge_Type", "nVarchar(30)", "", True)
        AgL.AddFieldSqlite("StructureDetail", "Value_Type", "nVarchar(30)", "", True)
        AgL.AddFieldSqlite("StructureDetail", "Value", "nVarchar(50)", "", True)
        AgL.AddFieldSqlite("StructureDetail", "Calculation", "nVarchar(4000)", "", True)
        AgL.AddFieldSqlite("StructureDetail", "BaseColumn", "nVarchar(50)", "", True)
        AgL.AddFieldSqlite("StructureDetail", "PostAc", "nVarchar(10)", "", True, "references Subgroup(subcode)")
        AgL.AddFieldSqlite("StructureDetail", "PostAcFromColumn", "nVarchar(50)", "", True)
        AgL.AddFieldSqlite("StructureDetail", "DrCr", "nVarchar(2)", "", True)
        AgL.AddFieldSqlite("StructureDetail", "LineItem", "Bit", "0", False)
        AgL.AddFieldSqlite("StructureDetail", "AffectCost", "Bit", "0", False)
        AgL.AddFieldSqlite("StructureDetail", "InactiveDate", "DateTime", "", True)
        AgL.AddFieldSqlite("StructureDetail", "Percentage", "Float", "0", False)
        AgL.AddFieldSqlite("StructureDetail", "Amount", "Float", "0", False)
        AgL.AddFieldSqlite("StructureDetail", "VisibleInMaster", "Bit", "0", False)
        AgL.AddFieldSqlite("StructureDetail", "VisibleInMasterLine", "Bit", "0", False)
        AgL.AddFieldSqlite("StructureDetail", "VisibleInTransactionLine", "Bit", "0", False)
        AgL.AddFieldSqlite("StructureDetail", "VisibleInTransactionFooter", "Bit", "0", False)
        AgL.AddFieldSqlite("StructureDetail", "HeaderPerField", "nVarchar(50)", "", True)
        AgL.AddFieldSqlite("StructureDetail", "HeaderAmtField", "nVarchar(50)", "", True)
        AgL.AddFieldSqlite("StructureDetail", "LinePerField", "nVarchar(50)", "", True)
        AgL.AddFieldSqlite("StructureDetail", "LineAmtField", "nVarchar(50)", "", True)
        AgL.AddFieldSqlite("StructureDetail", "GridDisplayIndex", "Int", "0", False)
        AgL.AddFieldSqlite("StructureDetail", "UploadDate", "DateTime", "", True)


    End Sub

    Private Sub FCreateTable_Structure_AcPosting()
        Dim mQry As String
        If Not AgL.IsTableExist("Structure_AcPosting", AgL.GcnMain) Then
            mQry = " CREATE TABLE [Structure_AcPosting] ([NCAT] nVarchar(10) NOT NULL, [Sr] int Not Null, PRIMARY KEY ([Code],[Sr]) ); "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If
        AgL.AddFieldSqlite("Structure_AcPosting", "Structure", "nVarchar(10)", "", True)
        AgL.AddFieldSqlite("Structure_AcPosting", "Charges", "nVarchar(8)", "", True)
        AgL.AddFieldSqlite("Structure_AcPosting", "PostAc", "nVarchar(10)", "", True, "references Subgroup(subcode)")
        AgL.AddFieldSqlite("Structure_AcPosting", "ContraSub", "nVarchar(10)", "", True, "references Subgroup(subcode)")
        AgL.AddFieldSqlite("Structure_AcPosting", "PostAcFromColumn", "nVarchar(50)", "", True)
        AgL.AddFieldSqlite("Structure_AcPosting", "DrCr", "nVarchar(2)", "", True)
        AgL.AddFieldSqlite("Structure_AcPosting", "UploadDate", "DateTime", "", True)


    End Sub

    Private Sub FCreateTable_Dimension1()
        Dim mQry As String
        If Not AgL.IsTableExist("Dimension1", AgL.GcnMain) Then
            mQry = " CREATE TABLE [Dimension1] ([Code] nVarchar(10) NOT NULL, PRIMARY KEY ([Code]) ); "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If
        AgL.AddFieldSqlite("Dimension1", "Description", "nVarchar(50)", "", True)
        AgL.AddFieldSqlite("Dimension1", "EntryBy", "nVarchar(10)", "", True)
        AgL.AddFieldSqlite("Dimension1", "EntryDate", "DateTime", "", True)
        AgL.AddFieldSqlite("Dimension1", "EntryType", "nVarchar(10)", "", True)
        AgL.AddFieldSqlite("Dimension1", "EntryStatus", "nVarchar(10)", "", True)
        AgL.AddFieldSqlite("Dimension1", "ApproveBy", "nVarchar(10)", "", True)
        AgL.AddFieldSqlite("Dimension1", "ApproveDate", "DateTime", "", True)
        AgL.AddFieldSqlite("Dimension1", "MoveToLog", "nVarchar(10)", "", True)
        AgL.AddFieldSqlite("Dimension1", "MoveToLogDate", "DateTime", "", True)
        AgL.AddFieldSqlite("Dimension1", "Status", "nVarchar(10)", "", True)
        AgL.AddFieldSqlite("Dimension1", "Div_Code", "nVarchar(10)", "", True)
    End Sub

    Private Sub FCreateTable_Dimension4()
        Dim mQry As String
        If Not AgL.IsTableExist("Dimension4", AgL.GcnMain) Then
            mQry = " CREATE TABLE [Dimension4] ([Code] nVarchar(10) NOT NULL, PRIMARY KEY ([Code]) ); "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If
        AgL.AddFieldSqlite("Dimension4", "Description", "nVarchar(50)", "", True)
        AgL.AddFieldSqlite("Dimension4", "EntryBy", "nVarchar(10)", "", True)
        AgL.AddFieldSqlite("Dimension4", "EntryDate", "DateTime", "", True)
        AgL.AddFieldSqlite("Dimension4", "EntryType", "nVarchar(10)", "", True)
        AgL.AddFieldSqlite("Dimension4", "EntryStatus", "nVarchar(10)", "", True)
        AgL.AddFieldSqlite("Dimension4", "ApproveBy", "nVarchar(10)", "", True)
        AgL.AddFieldSqlite("Dimension4", "ApproveDate", "DateTime", "", True)
        AgL.AddFieldSqlite("Dimension4", "MoveToLog", "nVarchar(10)", "", True)
        AgL.AddFieldSqlite("Dimension4", "MoveToLogDate", "DateTime", "", True)
        AgL.AddFieldSqlite("Dimension4", "Status", "nVarchar(10)", "", True)
        AgL.AddFieldSqlite("Dimension4", "Div_Code", "nVarchar(10)", "", True)
    End Sub

    Private Sub FCreateTable_Dimension3()
        Dim mQry As String
        If Not AgL.IsTableExist("Dimension3", AgL.GcnMain) Then
            mQry = " CREATE TABLE [Dimension3] ([Code] nVarchar(10) NOT NULL, PRIMARY KEY ([Code]) ); "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If
        AgL.AddFieldSqlite("Dimension3", "Description", "nVarchar(50)", "", True)
        AgL.AddFieldSqlite("Dimension3", "EntryBy", "nVarchar(10)", "", True)
        AgL.AddFieldSqlite("Dimension3", "EntryDate", "DateTime", "", True)
        AgL.AddFieldSqlite("Dimension3", "EntryType", "nVarchar(10)", "", True)
        AgL.AddFieldSqlite("Dimension3", "EntryStatus", "nVarchar(10)", "", True)
        AgL.AddFieldSqlite("Dimension3", "ApproveBy", "nVarchar(10)", "", True)
        AgL.AddFieldSqlite("Dimension3", "ApproveDate", "DateTime", "", True)
        AgL.AddFieldSqlite("Dimension3", "MoveToLog", "nVarchar(10)", "", True)
        AgL.AddFieldSqlite("Dimension3", "MoveToLogDate", "DateTime", "", True)
        AgL.AddFieldSqlite("Dimension3", "Status", "nVarchar(10)", "", True)
        AgL.AddFieldSqlite("Dimension3", "Div_Code", "nVarchar(10)", "", True)
    End Sub

    Private Sub FCreateTable_Dimension2()
        Dim mQry As String
        If Not AgL.IsTableExist("Dimension2", AgL.GcnMain) Then
            mQry = " CREATE TABLE [Dimension2] ([Code] nVarchar(10) NOT NULL, PRIMARY KEY ([Code]) ); "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If
        AgL.AddFieldSqlite("Dimension2", "Description", "nVarchar(50)", "", True)
        AgL.AddFieldSqlite("Dimension2", "EntryBy", "nVarchar(10)", "", True)
        AgL.AddFieldSqlite("Dimension2", "EntryDate", "DateTime", "", True)
        AgL.AddFieldSqlite("Dimension2", "EntryType", "nVarchar(10)", "", True)
        AgL.AddFieldSqlite("Dimension2", "EntryStatus", "nVarchar(10)", "", True)
        AgL.AddFieldSqlite("Dimension2", "ApproveBy", "nVarchar(10)", "", True)
        AgL.AddFieldSqlite("Dimension2", "ApproveDate", "DateTime", "", True)
        AgL.AddFieldSqlite("Dimension2", "MoveToLog", "nVarchar(10)", "", True)
        AgL.AddFieldSqlite("Dimension2", "MoveToLogDate", "DateTime", "", True)
        AgL.AddFieldSqlite("Dimension2", "Status", "nVarchar(10)", "", True)
        AgL.AddFieldSqlite("Dimension2", "Div_Code", "nVarchar(10)", "", True)
    End Sub


    Private Sub FCreateTable_StockProcess()
        Dim mQry As String
        If Not AgL.IsTableExist("StockProcess", AgL.GcnMain) Then
            mQry = "
                    CREATE TABLE [StockProcess] (
                       [DocID] varchar(21) NOT NULL COLLATE NOCASE,
                       [Sr] int NOT NULL,
                       [V_Type] nvarchar(5) COLLATE NOCASE,
                       [V_Prefix] nvarchar(5) COLLATE NOCASE,
                       [V_Date] datetime,
                       [V_No] bigint,
                       [Div_Code] nvarchar(1) COLLATE NOCASE,
                       [Site_Code] nvarchar(2) COLLATE NOCASE,
                       [SubCode] nvarchar(10) COLLATE NOCASE,
                       [Currency] nvarchar(10) COLLATE NOCASE,
                       [SalesTaxGroupParty] nvarchar(20) COLLATE NOCASE,
                       [Structure] nvarchar(8) COLLATE NOCASE,
                       [BillingType] nvarchar(10) COLLATE NOCASE,
                       [Item] nvarchar(10) COLLATE NOCASE,
                       [Item_UID] nvarchar(20) COLLATE NOCASE,
                       [LotNo] nvarchar(20) COLLATE NOCASE,
                       [ProcessGroup] nvarchar(10) COLLATE NOCASE,
                       [Godown] nvarchar(10) COLLATE NOCASE,
                       [Qty_Iss] float,
                       [Qty_Rec] float,
                       [Unit] nvarchar(10) COLLATE NOCASE,
                       [MeasurePerPcs] float,
                       [Measure_Iss] float,
                       [Measure_Rec] float,
                       [MeasureUnit] nvarchar(10) COLLATE NOCASE,
                       [Rate] float,
                       [Amount] float,
                       [Addition] float,
                       [Deduction] float,
                       [NetAmount] float,
                       [Remarks] varchar(255) COLLATE NOCASE,
                       [Process] nvarchar(10) COLLATE NOCASE,
                       [Status] nvarchar(20) COLLATE NOCASE,
                       [RecId] varchar(20) COLLATE NOCASE,
                       [UID] uniqueidentifier COLLATE NOCASE,
                       [FIFORate] float,
                       [FIFOAmt] float,
                       [AVGRate] float,
                       [AVGAmt] float,
                       [Cost] float,
                       [Doc_Qty] float,
                       [ReferenceDocID] varchar(21) COLLATE NOCASE,
                       [BaleNo] nvarchar(20) COLLATE NOCASE,
                       [FIFOValue] float,
                       [ProdOrder] nvarchar(21) COLLATE NOCASE,
                       [CurrentStock] float,
                       [ReferenceDocIDSr] int,
                       [MRP] float,
                       [NDP] float,
                       [ExpiryDate] datetime,
                       [EType_IR] nvarchar(1) COLLATE NOCASE,
                       [Landed_Value] float,
                       [OtherAdjustment] float,
                       [CostCenter] varchar(21) COLLATE NOCASE,
                       [Sale_Rate] float,
                       [Specification] nvarchar(50) COLLATE NOCASE,
                       [Manufacturer] nvarchar(20) COLLATE NOCASE,
                       [Dimension1] nvarchar(10) COLLATE NOCASE,
                       [Dimension2] nvarchar(10) COLLATE NOCASE,
                       PRIMARY KEY ([DocID], [Sr]),
                       CONSTRAINT [FK_StockProcess_Division_Div_Code] FOREIGN KEY ([Div_Code])
                          REFERENCES [Division]([Div_Code]) ON DELETE NO ACTION ON UPDATE NO ACTION,
                       CONSTRAINT [FK_StockProcess_Godown_Godown] FOREIGN KEY ([Godown])
                          REFERENCES [Godown]([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION,
                       CONSTRAINT [FK_StockProcess_Item_Item] FOREIGN KEY ([Item])
                          REFERENCES [Item]([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION,
                       CONSTRAINT [FK_StockProcess_Unit_MeasureUnit] FOREIGN KEY ([MeasureUnit])
                          REFERENCES [Unit]([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION,
                       CONSTRAINT [FK_StockProcess_Process_Process] FOREIGN KEY ([Process])
                          REFERENCES [Process]([NCat]) ON DELETE NO ACTION ON UPDATE NO ACTION,
                       CONSTRAINT [FK_StockProcess_ProcessGroup_ProcessGroup] FOREIGN KEY ([ProcessGroup])
                          REFERENCES [ProcessGroup]([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION,
                       CONSTRAINT [FK_StockProcess_SiteMast_Site_Code] FOREIGN KEY ([Site_Code])
                          REFERENCES [SiteMast]([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION,
                       CONSTRAINT [FK_StockProcess_SubGroup_SubCode] FOREIGN KEY ([SubCode])
                          REFERENCES [SubGroup]([SubCode]) ON DELETE NO ACTION ON UPDATE NO ACTION,
                       CONSTRAINT [FK_StockProcess_Unit_Unit] FOREIGN KEY ([Unit])
                          REFERENCES [Unit]([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION,
                       CONSTRAINT [FK_StockProcess_Voucher_Type_V_Type] FOREIGN KEY ([V_Type])
                          REFERENCES [Voucher_Type]([V_Type]) ON DELETE NO ACTION ON UPDATE NO ACTION
                    );
                "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If
    End Sub

    Private Sub FCreateTable_Stock()
        Dim mQry As String
        If Not AgL.IsTableExist("Stock", AgL.GcnMain) Then
            mQry = "

                    CREATE TABLE [Stock] (
                       [DocID] varchar(21) NOT NULL COLLATE NOCASE,
                       [Sr] int NOT NULL,
                       [V_Type] nvarchar(5) COLLATE NOCASE,
                       [V_Prefix] nvarchar(5) COLLATE NOCASE,
                       [V_Date] datetime,
                       [V_No] bigint,
                       [Div_Code] nvarchar(1) COLLATE NOCASE,
                       [Site_Code] nvarchar(2) COLLATE NOCASE,
                       [SubCode] nvarchar(10) COLLATE NOCASE,
                       [Currency] nvarchar(10) COLLATE NOCASE,
                       [SalesTaxGroupParty] nvarchar(20) COLLATE NOCASE,
                       [Structure] nvarchar(8) COLLATE NOCASE,
                       [BillingType] nvarchar(10) COLLATE NOCASE,
                       [Item] nvarchar(10) COLLATE NOCASE,
                       [Item_UID] nvarchar(20) COLLATE NOCASE,
                       [LotNo] nvarchar(20) COLLATE NOCASE,
                       [ProcessGroup] nvarchar(10) COLLATE NOCASE,
                       [Godown] nvarchar(10) COLLATE NOCASE,
                       [Qty_Iss] float,
                       [Qty_Rec] float,
                       [Unit] nvarchar(10) COLLATE NOCASE,
                       [MeasurePerPcs] float,
                       [Measure_Iss] float,
                       [Measure_Rec] float,
                       [MeasureUnit] nvarchar(10) COLLATE NOCASE,
                       [Rate] float,
                       [Amount] float,
                       [Addition] float,
                       [Deduction] float,
                       [NetAmount] float,
                       [Remarks] varchar(255) COLLATE NOCASE,
                       [Process] nvarchar(10) COLLATE NOCASE,
                       [Status] nvarchar(20) COLLATE NOCASE,
                       [RecId] varchar(20) COLLATE NOCASE,
                       [UID] uniqueidentifier COLLATE NOCASE,
                       [FIFORate] float,
                       [FIFOAmt] float,
                       [AVGRate] float,
                       [AVGAmt] float,
                       [Cost] float,
                       [Doc_Qty] float,
                       [ReferenceDocID] varchar(21) COLLATE NOCASE,
                       [FIFOValue] float,
                       [BaleNo] nvarchar(20) COLLATE NOCASE,
                       [ProdOrder] nvarchar(21) COLLATE NOCASE,
                       [ReferenceDocIDSr] int,
                       [ExpiryDate] datetime,
                       [MRP] float,
                       [NDP] float,
                       [CurrentStock] float,
                       [EType_IR] nvarchar(1) COLLATE NOCASE,
                       [Landed_Value] float,
                       [OtherAdjustment] float,
                       [CostCenter] varchar(21) COLLATE NOCASE,
                       [Sale_Rate] float,
                       [Specification] nvarchar(50) COLLATE NOCASE,
                       [Manufacturer] nvarchar(20) COLLATE NOCASE,
                       [Dimension1] nvarchar(10) COLLATE NOCASE,
                       [Dimension2] nvarchar(10) COLLATE NOCASE,
                       PRIMARY KEY ([DocID], [Sr]),
                       CONSTRAINT [FK_Stock_Division_Div_Code] FOREIGN KEY ([Div_Code])
                          REFERENCES [Division]([Div_Code]) ON DELETE NO ACTION ON UPDATE NO ACTION,
                       CONSTRAINT [FK_Stock_Godown_Godown] FOREIGN KEY ([Godown])
                          REFERENCES [Godown]([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION,
                       CONSTRAINT [FK_Stock_Item_Item] FOREIGN KEY ([Item])
                          REFERENCES [Item]([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION,
                       CONSTRAINT [FK_Stock_Unit_MeasureUnit] FOREIGN KEY ([MeasureUnit])
                          REFERENCES [Unit]([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION,
                       CONSTRAINT [FK_Stock_Process_Process] FOREIGN KEY ([Process])
                          REFERENCES [Process]([NCat]) ON DELETE NO ACTION ON UPDATE NO ACTION,
                       CONSTRAINT [FK_Stock_ProcessGroup_ProcessGroup] FOREIGN KEY ([ProcessGroup])
                          REFERENCES [ProcessGroup]([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION,
                       CONSTRAINT [FK_Stock_SiteMast_Site_Code] FOREIGN KEY ([Site_Code])
                          REFERENCES [SiteMast]([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION,
                       CONSTRAINT [FK_Stock_SubGroup_SubCode] FOREIGN KEY ([SubCode])
                          REFERENCES [SubGroup]([SubCode]) ON DELETE NO ACTION ON UPDATE NO ACTION,
                       CONSTRAINT [FK_Stock_Unit_Unit] FOREIGN KEY ([Unit])
                          REFERENCES [Unit]([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION,
                       CONSTRAINT [FK_Stock_Voucher_Type_V_Type] FOREIGN KEY ([V_Type])
                          REFERENCES [Voucher_Type]([V_Type]) ON DELETE NO ACTION ON UPDATE NO ACTION
                    );


                    CREATE INDEX 'IX_Stock_Item' On Stock (Item Asc);
                    CREATE INDEX 'IX_Stock_RefDocId_RefSr' On Stock(ReferenceDocId, ReferenceDocIdSr);

                "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If
    End Sub

    Private Sub FCreateTable_StockHeadDetail()
        Dim mQry As String
        If Not AgL.IsTableExist("StockHeadDetail", AgL.GcnMain) Then
            mQry = "
                CREATE TABLE [StockHeadDetail] (
                   [DocID] nvarchar(21) NOT NULL COLLATE NOCASE,
                   [Sr] int NOT NULL,
                   [Item] nvarchar(10) COLLATE NOCASE,
                   [Item_UID] nvarchar(20) COLLATE NOCASE,
                   [LotNo] nvarchar(20) COLLATE NOCASE,
                   [BaleNo] nvarchar(20) COLLATE NOCASE,
                   [Godown] nvarchar(10) COLLATE NOCASE,
                   [Qty] float,
                   [Unit] nvarchar(10) COLLATE NOCASE,
                   [MeasurePerPcs] float,
                   [TotalMeasure] float,
                   [MeasureUnit] nvarchar(10) COLLATE NOCASE,
                   [Rate] float,
                   [Amount] float,
                   [Remarks] varchar(255) COLLATE NOCASE,
                   [Process] nvarchar(10) COLLATE NOCASE,
                   [Status] nvarchar(20) COLLATE NOCASE,
                   [CostCenter] nvarchar(21) COLLATE NOCASE,
                   [CurrentStock] float,
                   [CurrentStockMeasure] float,
                   [SubCode] nvarchar(10) COLLATE NOCASE,
                   [JobOrder] nvarchar(21) COLLATE NOCASE,
                   [DiffernceQty] float,
                   [DiffernceMeasure] float,
                   [UID] uniqueidentifier COLLATE NOCASE,
                   [ReferenceDocID] nvarchar(21) COLLATE NOCASE,
                   [ReferenceDocIDSr] int,
                   [DifferenceQty] float,
                   [DifferenceMeasure] float,
                   [V_Nature] nvarchar(20) COLLATE NOCASE,
                   [Requisition] nvarchar(21) COLLATE NOCASE,
                   [RequisitionSr] int,
                   [Manufacturer] nvarchar(10) COLLATE NOCASE,
                   PRIMARY KEY ([DocID], [Sr]),
                   CONSTRAINT [FK_StockHeadDetail_Godown_Godown] FOREIGN KEY ([Godown])
                      REFERENCES [Godown]([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION,
                   CONSTRAINT [FK_StockHeadDetail_Item_Item] FOREIGN KEY ([Item])
                      REFERENCES [Item]([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION,
                   CONSTRAINT [FK_StockHeadDetail_Subgroup_Manufacturer] FOREIGN KEY ([Manufacturer])
                      REFERENCES [SubGroup]([SubCode]) ON DELETE NO ACTION ON UPDATE NO ACTION,
                   CONSTRAINT [FK_StockHeadDetail_Unit_MeasureUnit] FOREIGN KEY ([MeasureUnit])
                      REFERENCES [Unit]([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION,
                   CONSTRAINT [FK_StockHeadDetail_Process_Process] FOREIGN KEY ([Process])
                      REFERENCES [Process]([NCat]) ON DELETE NO ACTION ON UPDATE NO ACTION,
                   CONSTRAINT [FK_StockHeadDetail_Unit_Unit] FOREIGN KEY ([Unit])
                      REFERENCES [Unit]([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION
                );

                "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If
        AgL.AddFieldSqlite("StockHeadDetail", "Specification", "nVarchar(255)", "", True)
        AgL.AddFieldSqlite("StockHeadDetail", "Dimension1", "nVarchar(10)", "", True, " references Dimension1(Code) ")
        AgL.AddFieldSqlite("StockHeadDetail", "Dimension2", "nVarchar(10)", "", True, " references Dimension2(Code) ")
        AgL.AddFieldSqlite("StockHeadDetail", "Dimension3", "nVarchar(10)", "", True, " references Dimension3(Code) ")
        AgL.AddFieldSqlite("StockHeadDetail", "Dimension4", "nVarchar(10)", "", True, " references Dimension4(Code) ")
    End Sub


    Private Sub FCreateTable_StockHead()
        Dim mQry As String
        If Not AgL.IsTableExist("StockHead", AgL.GcnMain) Then
            mQry = "
                    CREATE TABLE [StockHead] (
                       [DocID] nvarchar(21) NOT NULL COLLATE NOCASE,
                       [V_Type] nvarchar(5) COLLATE NOCASE,
                       [V_Prefix] nvarchar(5) COLLATE NOCASE,
                       [V_Date] datetime,
                       [V_No] bigint,
                       [Div_Code] nvarchar(1) COLLATE NOCASE,
                       [Site_Code] nvarchar(2) COLLATE NOCASE,
                       [ManualRefNo] nvarchar(50) COLLATE NOCASE,
                       [OrderBy] nvarchar(10) COLLATE NOCASE,
                       [SubCode] nvarchar(10) COLLATE NOCASE,
                       [FromProcess] nvarchar(10) COLLATE NOCASE,
                       [ToProcess] nvarchar(10) COLLATE NOCASE,
                       [FromGodown] nvarchar(10) COLLATE NOCASE,
                       [ToGodown] nvarchar(10) COLLATE NOCASE,
                       [TotalQty] float,
                       [TotalMeasure] float,
                       [Amount] float,
                       [Addition] float,
                       [Deduction] float,
                       [NetAmount] float,
                       [Remarks] varchar(255) COLLATE NOCASE,
                       [IsDeleted] bit,
                       [EntryBy] nvarchar(10) COLLATE NOCASE,
                       [EntryDate] datetime,
                       [EntryType] nvarchar(10) COLLATE NOCASE,
                       [EntryStatus] nvarchar(10) COLLATE NOCASE,
                       [ApproveBy] nvarchar(10) COLLATE NOCASE,
                       [ApproveDate] datetime,
                       [MoveToLog] nvarchar(10) COLLATE NOCASE,
                       [MoveToLogDate] datetime,
                       [Status] nvarchar(10) COLLATE NOCASE,
                       [UID] uniqueidentifier COLLATE NOCASE,
                       [ReferenceDocID] nvarchar(21) COLLATE NOCASE,
                       [Structure] nvarchar(8) COLLATE NOCASE,
                       [Process] nvarchar(10) COLLATE NOCASE,
                       PRIMARY KEY ([DocID]),
                       CONSTRAINT [FK_StockHead_Division_Div_Code] FOREIGN KEY ([Div_Code])
                          REFERENCES [Division]([Div_Code]) ON DELETE NO ACTION ON UPDATE NO ACTION,
                       CONSTRAINT [FK_StockHead_Godown_FromGodown] FOREIGN KEY ([FromGodown])
                          REFERENCES [Godown]([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION,
                       CONSTRAINT [FK_StockHead_Process_FromProcess] FOREIGN KEY ([FromProcess])
                          REFERENCES [Process]([NCat]) ON DELETE NO ACTION ON UPDATE NO ACTION,
                       CONSTRAINT [FK_StockHead_SubGroup_OrderBy] FOREIGN KEY ([OrderBy])
                          REFERENCES [SubGroup]([SubCode]) ON DELETE NO ACTION ON UPDATE NO ACTION,
                       CONSTRAINT [FK_StockHead_SiteMast_Site_Code] FOREIGN KEY ([Site_Code])
                          REFERENCES [SiteMast]([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION,
                       CONSTRAINT [FK_StockHead_SubGroup_SubCode] FOREIGN KEY ([SubCode])
                          REFERENCES [SubGroup]([SubCode]) ON DELETE NO ACTION ON UPDATE NO ACTION,
                       CONSTRAINT [FK_StockHead_Godown_ToGodown] FOREIGN KEY ([ToGodown])
                          REFERENCES [Godown]([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION,
                       CONSTRAINT [FK_StockHead_Process_ToProcess] FOREIGN KEY ([ToProcess])
                          REFERENCES [Process]([NCat]) ON DELETE NO ACTION ON UPDATE NO ACTION,
                       CONSTRAINT [FK_StockHead_Voucher_Type_V_Type] FOREIGN KEY ([V_Type])
                          REFERENCES [Voucher_Type]([V_Type]) ON DELETE NO ACTION ON UPDATE NO ACTION
                    );
                "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If
        AgL.AddFieldSqlite("StockHead", "Reason", "nVarchar(10)", "", True, " references Reason(Code) ")

    End Sub

    Private Sub FSeedTable_RateType()
        Dim mQry As String
        If AgL.FillData("Select * from RateType limit 1", AgL.GcnMain).tables(0).Rows.Count = 0 Then
            mQry = " 
                    INSERT INTO RateType
                    (Code, Description, IsDeleted, EntryBy, EntryDate, EntryType, EntryStatus, ApproveBy, ApproveDate, MoveToLog, MoveToLogDate, Status, Div_Code, UID, Margin, Sr)
                    VALUES('D10001', 'Super Nett Rate', NULL, 'SUPER', '2018-02-16', 'Edit', 'Open', NULL, NULL, NULL, NULL, 'Active', 'D', NULL, 10.0, 0);
                    INSERT INTO RateType
                    (Code, Description, IsDeleted, EntryBy, EntryDate, EntryType, EntryStatus, ApproveBy, ApproveDate, MoveToLog, MoveToLogDate, Status, Div_Code, UID, Margin, Sr)
                    VALUES('D10002', 'Nett Rate', NULL, 'SUPER', '2018-02-16', 'Edit', 'Open', NULL, NULL, NULL, NULL, 'Active', 'D', NULL, 15.0, 0);
                    INSERT INTO RateType
                    (Code, Description, IsDeleted, EntryBy, EntryDate, EntryType, EntryStatus, ApproveBy, ApproveDate, MoveToLog, MoveToLogDate, Status, Div_Code, UID, Margin, Sr)
                    VALUES('D10003', 'Dhara Rate', NULL, 'SUPER', '2018-02-16', 'Edit', 'Open', NULL, NULL, NULL, NULL, 'Active', 'D', NULL, 20.0, 0);
                "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If
    End Sub


    Private Sub FSeedTable_Voucher_Type()
        Dim mQry As String

        If AgL.FillData("Select * from Voucher_Type Where V_Type='OPSTK'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
            mQry = " 
                    INSERT INTO Voucher_Type
                    (NCat, Category, V_Type, Description, Short_Name, SystemDefine, DivisionWise, SiteWise, PreparedBy, U_EntDt, U_AE, ModifiedBy, Edit_Date, IssRec, Description_Help, Description_BiLang, Short_Name_BiLang, Report_Index, Number_Method, Start_No, Last_Ent_Date, Form_Name, Saperate_Narr, Common_Narr, Narration, Print_VNo, Header_Desc, Term_Desc, Footer_Desc, Exclude_Ac_Grp, SerialNo_From_Table, U_Name, ChqNo, ChqDt, ClgDt, DefaultCrAc, DefaultDrAc, FirstDrCr, TrnType, TdsDed, ContraNarr, TdsOnAmt, Contra_Narr, Separate_Narr, MnuAttachedInModule, AuditAllowed, UpLoadDate, Affect_FA, IsShowVoucherReference, MnuName, MnuText, SerialNo, HeaderTable, LogHeaderTable, DefaultAc, CustomFields, ContraV_Type)
                    VALUES('OPSTK', 'OTHER', 'OPSTK', 'Stock Opening', 'OPSTK', 'Y', 0, 1, 'sa', '2012-10-11 00:00:00', 'A', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'Automatic', NULL, NULL, NULL, 'N', 'Y', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'Y', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
                "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)

            If AgL.FillData("Select * from Voucher_Prefix Where V_Type='OPSTK' And Prefix = '2017' ", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    INSERT INTO Voucher_Prefix
                    (V_Type, Date_From, Prefix, Start_Srl_No, Date_To, Comp_Code, Site_Code, Div_Code, UpLoadDate, Status_Add, Status_Edit, Status_Delete, Status_Print, Ref_Prefix, Ref_PadLength)
                    VALUES('OPSTK', '2017-04-01', '2017', 0, '2018-03-31', '1', '1', 'D', NULL, NULL, NULL, NULL, NULL, '17-', 5);
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If
        End If
    End Sub

End Class

