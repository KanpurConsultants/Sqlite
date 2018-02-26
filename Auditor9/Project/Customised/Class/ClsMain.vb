Imports System.Data.SqlClient
Imports System.IO
Imports CrystalDecisions.CrystalReports.Engine
Imports System.Data.SQLite

Public Class ClsMain
    Public CFOpen As New ClsFunction
    Public Const ModuleName As String = "Customised"

    Public Const DefaultUnit As String = "Sq.Feet"

    Sub New(ByVal AgLibVar As AgLibrary.ClsMain)
        AgL = AgLibVar
        AgPL = New AgLibrary.ClsPrinting(AgL)
        AgIniVar = New AgLibrary.ClsIniVariables(AgL)
        ClsMain_Purchase = New Purchase.ClsMain(AgL)
        ClsMain_Store = New Store.ClsMain(AgL)
        'ClsMain_Sales = New Sales.ClsMain(AgL)
        ClsMain_EMail = New EMail.ClsMain(AgL)
        ClsMain_ReportLayout = New ReportLayout.ClsMain(AgL)

        Call IniDtEnviro()
        AgL.PubDivisionList = "('" + AgL.PubDivCode + "')"
    End Sub

    Public Class PaymentMode
        Public Const Cash As String = "Cash"
        Public Const Credit As String = "Credit"
        Public Const Complementary As String = "Complementary"
    End Class

    Public Class MasterType
        Public Const Customer As String = "Customer"
        Public Const Supplier As String = "Supplier"
        Public Const Agent As String = "Agent"
    End Class

    Public Class SubGroupNature
        Public Const Customer As String = "Customer"
        Public Const Supplier As String = "Supplier"
        Public Const Cash As String = "Cash"
        Public Const Bank As String = "Bank"
    End Class

    Public Class SubGroupMasterType
        Public Const Customer As String = "Customer"
        Public Const Supplier As String = "Supplier"
    End Class

    Public Class SalesTaxGroupPartyNature
        Public Const Local As String = "Local"
        Public Const Central As String = "Central"
    End Class

    Public Class ExportOrderType
        Public Const SaleOrder As String = "Sale Order"
        Public Const CustomOrder As String = "Custom Order"
    End Class

    Public Enum EntryPointType
        Main
        Log
    End Enum

    Public Class Voucher_Category
        Public Const Purchase As String = "PURCH"
        Public Const Sale As String = "SALE"
    End Class


    Public Class LogStatus
        Public Const LogOpen As String = "Open"
        Public Const LogDiscard As String = "Discard"
        Public Const LogApproved As String = "Approved"
    End Class

    Public Class ItemType
        Public Const RawMaterial As String = "RM"
        Public Const FinishedMaterial As String = "FM"
    End Class

    Public Class ItemGroup
        Public Const Sample As String = "Sample"
    End Class

    Public Class ItemCategory
        Public Const Sample As String = "Sample"
        Public Const CarpetSKU As String = "Carpet SKU"
    End Class

    Public Class Shape
        Public Const Rectangle As String = "Rectangle"
        Public Const Circle As String = "Circle"
        Public Const Square As String = "Square"
        Public Const Others As String = "Others"
    End Class

    Public Class Temp_NCat
        Public Const ItemInvoiceGroup As String = "IIG"
        Public Const Item As String = "Item"
    End Class

    Public Class Temp_VType
        'For Purchase
        Public Const EstimateGR As String = "EGR"
        Public Const Estimate As String = "ESTMT"

        'For Sale
        Public Const TaxInvoice As String = "TINV"
        Public Const SaleEstimate As String = "SEST"
        Public Const SampleInvoice As String = "SMINV"
    End Class


#Region "Public Help Queries"

    Public Const PubStrHlpQryWashingType As String = "Select 'Normal' as Code, 'Normal' as Description " & _
                                                     " Union All Select 'Antique' as Code, 'Antique' as Description " & _
                                                     " Union All Select 'Herbal' as Code, 'Herbal' as Description " & _
                                                     " Union All Select 'N.A.' as Code, 'N.A.' as Description "


#End Region

#Region " Structure Update Code "

    Public Sub UpdateTableStructure()
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

        If AgL.FillData("Select * from Company limit 1", AgL.GcnMain).tables(0).Rows.Count = 0 Then
            mQry = " INSERT INTO Company
                (Comp_Code, Div_Code, Comp_Name, CentralData_Path, PrevDBName, DbPrefix, Repo_Path, Start_Dt, End_Dt, address1, address2, city, pin, phone, fax, lstno, lstdate, cstno, cstdate, cyear, pyear, SerialKeyNo, SName, EMail, Gram, Desc1, Desc2, Desc3, ECCCode, ExDivision, ExRegNo, ExColl, ExRange, Desc4, VatNo, VatDate, TinNo, Site_Code, LogSiteCode, PANNo, State, U_Name, U_EntDt, U_AE, DeletedYN, Country, V_Prefix, NotificationNo, WorkAddress1, WorkAddress2, WorkCity, WorkCountry, WorkPin, WorkPhone, WorkFax, WebServer, WebUser, WebPassword, Webdatabase, RowId, UpLoadDate, UseSiteNameAsCompanyName, FileDbName, ImageDbName)
                VALUES('1', 'D', 'Auditor9 Solutions', 'D:\KC\Data\Auditor9', NULL, 'Cloth', NULL, '2017-04-01 00:00:00', '2018-03-31 00:00:00', '13/152 Parmat, Civil Lines', NULL, 'Kanpur', '208001', '05414226864', '-', NULL, NULL, '-', '12/Nov/2017', '2017-2018', '2016-2017', 'RA96082587', 'AAMC', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', NULL, '2010-11-12 00:00:00', '09815400794', NULL, NULL, '-', 'U.P.', 'SA', '2008-04-01 00:00:00', 'E', 'N', 'INDIA', '2010', '-', '-', '-', '-', '-', '-', '-', '-', NULL, NULL, NULL, NULL, 1, NULL, 0, 'MedicalFiles', NULL);
                "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If



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
                   PRIMARY KEY ([Div_Code])
                );

                CREATE UNIQUE INDEX [IX_Division]
                ON [Division]
                ([Div_Name]);
            "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If


        If AgL.FillData("Select * from Division limit 1", AgL.GcnMain).tables(0).Rows.Count = 0 Then
            mQry = " INSERT INTO Division
                    (Div_Code, Div_Name, DataPath, address1, address2, address3, city, pin, PreparedBy, U_EntDt, U_AE, Edit_Date, ModifiedBy, SitewiseV_No, RowId, UpLoadDate, ApprovedBy, ApprovedDate, GPX1, GPX2, GPN1, GPN2)
                    VALUES('D', 'Main', 'MEDICAL_1', '-', '-', '-', 'Kanpur', '-', 'SA', '2008-04-01 00:00:00', 'E', '2010-05-21 00:00:00', 'sa', 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
                "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If


        If Not AgL.IsTableExist("City", AgL.GcnMain) Then
            mQry = "
                CREATE TABLE [City] (
                   [CityCode] nvarchar(6) NOT NULL COLLATE NOCASE,
                   [CityName] nvarchar(50) COLLATE NOCASE,
                   [State] nvarchar(50) COLLATE NOCASE,
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

        If AgL.FillData("Select * from City limit 1", AgL.GcnMain).tables(0).Rows.Count = 0 Then
            mQry = " INSERT INTO City
                    (CityCode, CityName, State, IsDeleted, Country, EntryBy, EntryDate, EntryType, EntryStatus, ApproveBy, ApproveDate, MoveToLog, MoveToLogDate, Status, Div_Code, UID, STDCode, U_EntDt, U_Name, U_AE, Transfered)
                    VALUES('10', 'Kanpur', 'UTTAR PRADESH', 'INDIA', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, Null, 'SUPER', 'A', NULL);
                "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If

        If Not AgL.IsTableExist("SiteMast", AgL.GcnMain) Then
            mQry = "
                CREATE TABLE [SiteMast] (
                   [Code] nvarchar(2) NOT NULL COLLATE NOCASE,
                   [Name] nvarchar(50) COLLATE NOCASE,
                   [HO_YN] nvarchar(1) COLLATE NOCASE,
                   [Add1] nvarchar(50) COLLATE NOCASE,
                   [Add2] nvarchar(50) COLLATE NOCASE,
                   [Add3] nvarchar(50) COLLATE NOCASE,
                   [City_Code] nvarchar(7) COLLATE NOCASE,
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


        If AgL.FillData("Select * from SiteMast limit 1", AgL.GcnMain).tables(0).Rows.Count = 0 Then
            mQry = " 
                    INSERT INTO SiteMast
                    (Code, Name, HO_YN, Add1, Add2, Add3, City_Code, Phone, Mobile, PinNo, U_Name, U_EntDt, U_AE, Edit_Date, ModifiedBy, ManualCode, RowId, UpLoadDate, Active, AcCode, SqlServer, DataPath, DataPathMain, SqlUser, SqlPassword, CreditLimit, ApprovedBy, ApprovedDate, GPX1, GPX2, GPN1, GPN2, Photo, LastNarration, IEC, TIN, Director, ExciseDivision, DrugLicenseNo, PAN)
                    VALUES('1', 'Auditor9 Solutions', 'N', '13/152 Parmat, Civil Lines', NULL, NULL, '10', '9335671971', NULL, '208001', 'SA', '2008-08-06 00:00:00', 'E', '2013-03-30 00:00:00', 'SA', 'HO', 1, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, 0.0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '---', NULL);
                   "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If


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

        If AgL.FillData("Select * from UserMast limit 1", AgL.GcnMain).tables(0).Rows.Count = 0 Then
            mQry = " 
                    INSERT INTO UserMast
                    (USER_NAME, Code, PASSWD, Description, Admin, RowId, UpLoadDate, ModuleList, SeniorName, MainStreamCode, EMail, Mobile, IsActive, InActiveDate)
                    VALUES('SA', '1', '@', 'CEO', 'Y', 1, NULL, NULL, NULL, '010', NULL, NULL, 1, NULL);

                   "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If


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


        If AgL.FillData("Select * from UserSite limit 1", AgL.GcnMain).tables(0).Rows.Count = 0 Then
            mQry = " 
                    INSERT INTO UserSite
                    (User_Name, CompCode, Sitelist, UpLoadDate, DivisionList)
                    VALUES('SA', '1', '|1|', NULL, '|D|');

                   "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If


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

        If AgL.FillData("Select * from Enviro limit 1", AgL.GcnMain).tables(0).Rows.Count = 0 Then
            mQry = " INSERT INTO Enviro
                    (ID, Site_Code, Div_Code, CashAc, BankAc, TdsAc, AdditionAc, DeductionAc, ServiceTaxAc, ECessAc, RoundOffAc, HECessAc, ServiceTaxPer, ECessPer, HECessPer, RowId, UpLoadDate, PreparedBy, U_EntDt, U_AE, Edit_Date, ModifiedBy, ApprovedBy, ApprovedDate, GPX1, GPX2, GPN1, GPN2, DefaultSalesTaxGroupParty, DefaultSalesTaxGroupItem, PurchOrderShowIndentInLine, IsLinkWithFA, IsNegativeStockAllowed, IsLotNoApplicable, DefaultDueDays, IsNegetiveStockAllowed, SaleAc, PostingAc, PurchaseAc, DefaultCurrency, DefaultVatCommodityCode, IsVisible_PurchOrder, IsVisible_PurchChallan, Caption_Dimension1, Caption_Dimension2, UrgentList, UrgentItemList)
                    VALUES('1', '1', 'D', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'Local', 'General', 0, NULL, 1, 1, NULL, NULL, 'Sale', '111', NULL, 'Rs.', '2A079001', NULL, NULL, 'D1', 'D2', NULL, NULL);
                "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        End If



    End Sub

    Public Sub UpdateTableInitialiser()
        Dim mQry$
        Try
            Call CreateVType()

            Call TB_PostingGroupSalesTaxItem()

            Call TB_PostingGroupSalesTaxParty()

            Call TB_PostingGroupSalesTax()

            Call TB_Enviro()

            mQry = "Update Stock Set EType_IR = 'I' Where IfNull(Qty_Iss,0)>0 And EType_IR is Null "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

            mQry = "Update Stock Set EType_IR = 'R' Where IfNull(Qty_Rec,0)>0 And EType_IR is Null "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn)

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub TB_PostingGroupSalesTaxItem()
        Dim mQry$ = ""
        Try
            If AgL.Dman_Execute(" Select Count(*) From PostingGroupSalesTaxItem Where Description = 'General'", AgL.GCn).ExecuteScalar = 0 Then
                mQry = " INSERT INTO dbo.PostingGroupSalesTaxItem (Description, Active) VALUES ('General', 1) "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If
        Catch ex As Exception
            MsgBox(ex.Message + ".On TB_PostingGroupSalesTaxItem")
        End Try
    End Sub

    Private Sub TB_PostingGroupSalesTaxParty()
        Dim mQry$ = ""
        Try
            If AgL.Dman_Execute(" Select Count(*) From PostingGroupSalesTaxParty Where Description = 'Central'", AgL.GCn).ExecuteScalar = 0 Then
                mQry = " INSERT INTO PostingGroupSalesTaxParty (Description, Active) VALUES ('Central', 1)"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If

            If AgL.Dman_Execute(" Select Count(*) From PostingGroupSalesTaxParty Where Description = 'Local'", AgL.GCn).ExecuteScalar = 0 Then
                mQry = " INSERT INTO PostingGroupSalesTaxParty (Description, Active) VALUES ('Local', 1)"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If
        Catch ex As Exception
            MsgBox(ex.Message + ".On TB_PostingGroupSalesTaxParty")
        End Try
    End Sub

    Private Sub TB_PostingGroupSalesTax()
        Dim mQry$ = ""
        Try
            If AgL.Dman_Execute(" Select Count(*) From PostingGroupSalesTax Where PostingGroupSalesTaxParty = 'Central' And PostingGroupSalesTaxItem = 'General'", AgL.GCn).ExecuteScalar = 0 Then
                mQry = " INSERT INTO dbo.PostingGroupSalesTax (PostingGroupSalesTaxItem, PostingGroupSalesTaxParty, PurchaseSaleAc, SalesTax, SalesTaxAc, VAT, VatAc, AdditionalTax, AdditionalTaxAc, Cst, CstAc, CustomDuty, CustomDutyAc, CustomDutyECess, CustomDutyECessAc, CustomDutyHECess, CustomDutyHECessAc, CustomAdditionalDuty, CustomAdditionalDutyAc, Site_Code, Div_Code, WEF) " & _
                        " VALUES ('General', 'Central', NULL, 0, NULL, 0, NULL, 0, NULL, 2, NULL, 0, NULL, 0, NULL, 0, NULL, 0, NULL, '1', 'D', '2012-04-01')"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If

            If AgL.Dman_Execute(" Select Count(*) From PostingGroupSalesTax Where PostingGroupSalesTaxParty = 'Local' And PostingGroupSalesTaxItem = 'General'", AgL.GCn).ExecuteScalar = 0 Then
                mQry = " INSERT INTO dbo.PostingGroupSalesTax (PostingGroupSalesTaxItem, PostingGroupSalesTaxParty, PurchaseSaleAc, SalesTax, SalesTaxAc, VAT, VatAc, AdditionalTax, AdditionalTaxAc, Cst, CstAc, CustomDuty, CustomDutyAc, CustomDutyECess, CustomDutyECessAc, CustomDutyHECess, CustomDutyHECessAc, CustomAdditionalDuty, CustomAdditionalDutyAc, Site_Code, Div_Code, WEF) " & _
                        " VALUES ('General', 'Local', NULL, 0, NULL, 12.5, NULL, 1, NULL, 0, NULL, 0, NULL, 0, NULL, 0, NULL, 0, NULL, '1', 'D', '2012-04-01')"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If
        Catch ex As Exception
            MsgBox(ex.Message + ".On TB_PostingGroupSalesTax")
        End Try
    End Sub

    Private Sub TB_Enviro()
        Dim mQry$ = ""
        Try
            If AgL.Dman_Execute(" Select Count(*) From Enviro Where Site_Code = '" & AgL.PubSiteCode & "'", AgL.GCn).ExecuteScalar = 0 Then
                mQry = " INSERT INTO dbo.Enviro (ID, Site_Code, Div_Code, DefaultSalesTaxGroupParty, DefaultSalesTaxGroupItem, PurchOrderShowIndentInLine, IsLinkWithFA, IsNegativeStockAllowed, IsLotNoApplicable, DefaultDueDays, SaleAc, PostingAc, CashAc, BankAc, TdsAc, AdditionAc, DeductionAc, ServiceTaxAc, ECessAc, RoundOffAc, HECessAc, ServiceTaxPer, ECessPer, HECessPer, UpLoadDate, PreparedBy, U_EntDt, U_AE, Edit_Date, ModifiedBy, ApprovedBy, ApprovedDate, GPX1, GPX2, GPN1, GPN2, IsNegetiveStockAllowed) " & _
                        " VALUES ('1', '1', 'D', 'Local', 'General', 0, NULL, 1, 1, NULL, 'Sale', '111', 'cash', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If
        Catch ex As Exception
            MsgBox(ex.Message + ".On TB_Enviro")
        End Try
    End Sub

    Private Sub CreateVType()
        Try
            '===================================================< Estimate Purchase Invoice V_Type >===================================================
            Try
                AgL.CreateVType(AgL.GCn, AgTemplate.ClsMain.Temp_NCat.PurchaseInvoice, AgTemplate.ClsMain.Temp_NCat.PurchaseInvoice, Temp_VType.Estimate, "Estimate", Temp_VType.Estimate, AgL.PubUserName, AgL.PubLoginDate, AgL.PubStartDate, AgL.PubEndDate, AgL.PubSiteCode, AgL.PubDivCode, False, AgL.PubSitewiseV_No)
            Catch ex As Exception
                MsgBox(ex.Message & " In CreateVType of " & Temp_VType.Estimate)
            End Try

            '===================================================< Tax Invoice V_Type (Sale)>===================================================
            Try
                AgL.CreateVType(AgL.GCn, AgTemplate.ClsMain.Temp_NCat.SaleInvoice, AgTemplate.ClsMain.Temp_NCat.SaleInvoice, Temp_VType.TaxInvoice, "Tax Invoice", Temp_VType.TaxInvoice, AgL.PubUserName, AgL.PubLoginDate, AgL.PubStartDate, AgL.PubEndDate, AgL.PubSiteCode, AgL.PubDivCode, False, AgL.PubSitewiseV_No)
            Catch ex As Exception
                MsgBox(ex.Message & " In CreateVType of " & AgTemplate.ClsMain.Temp_NCat.SaleInvoice)
            End Try

            '===================================================< Estinate Sale Invoice V_Type >===================================================
            Try
                AgL.CreateVType(AgL.GCn, AgTemplate.ClsMain.Temp_NCat.SaleInvoice, AgTemplate.ClsMain.Temp_NCat.SaleInvoice, Temp_VType.SaleEstimate, "Sale Estimate", Temp_VType.SaleEstimate, AgL.PubUserName, AgL.PubLoginDate, AgL.PubStartDate, AgL.PubEndDate, AgL.PubSiteCode, AgL.PubDivCode, False, AgL.PubSitewiseV_No)
            Catch ex As Exception
                MsgBox(ex.Message & " In CreateVType of " & Temp_VType.SaleEstimate)
            End Try

            '===================================================< Sample Invoice V_Type (Sale)>===================================================
            Try
                AgL.CreateVType(AgL.GCn, AgTemplate.ClsMain.Temp_NCat.SaleInvoice, AgTemplate.ClsMain.Temp_NCat.SaleInvoice, Temp_VType.SampleInvoice, "Sample Invoice", Temp_VType.SampleInvoice, AgL.PubUserName, AgL.PubLoginDate, AgL.PubStartDate, AgL.PubEndDate, AgL.PubSiteCode, AgL.PubDivCode, False, AgL.PubSitewiseV_No)
            Catch ex As Exception
                MsgBox(ex.Message & " In CreateVType of " & AgTemplate.ClsMain.Temp_NCat.SaleInvoice)
            End Try
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Sub FIni_ItemType()
        Dim mQry$
        Dim strData$ = ""
        mQry = "Select Count(*) from ItemType Where Code = 'RM'"
        If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar = 0 Then
            If strData <> "" Then strData += " Union All "
            strData += " Select 'RM' CODE, 'Raw Material' as Name "
        End If

        mQry = "Select Count(*) from ItemType Where Code = 'FM'"
        If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar = 0 Then
            If strData <> "" Then strData += " Union All "
            strData += " Select 'FM' CODE, 'Finish Material' as Name "
        End If

        strData = "Insert Into ItemType (Code,Name ) " + _
                  "( " & strData & ") x "

    End Sub

    Private Sub FPurchInvoiceDetail(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)
        AgL.FSetColumnValue(MdlTable, "Specification", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)
        AgL.FSetColumnValue(MdlTable, "PurchIndent", AgLibrary.ClsMain.SQLDataType.nVarChar, 21)
    End Sub

    Private Sub FPurchInvoice(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "VendorName", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "VendorAddress", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "VendorCity", AgLibrary.ClsMain.SQLDataType.nVarChar, 6)
        AgL.FSetColumnValue(MdlTable, "VendorMobile", AgLibrary.ClsMain.SQLDataType.nVarChar, 35)
        AgL.FSetFKeyValue(MdlTable, "VendorCity", "CityCode", "City")

        AgL.FSetColumnValue(MdlTable, "BillToParty", AgLibrary.ClsMain.SQLDataType.VarChar, 10)
    End Sub

    Private Sub FSubGroup(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DispName", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "MasterType", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "Currency", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "SalesTaxPostingGroup", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "DrugLicenseNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "EntryBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "EntryType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryStatus", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "MoveToLog", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MoveToLogDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "IsDeleted", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))
    End Sub

    Private Sub FCurrency(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "IsDeleted", AgLibrary.ClsMain.SQLDataType.Bit)
    End Sub

    Private Sub FDuesEnviro(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "V_Type", AgLibrary.ClsMain.SQLDataType.nVarChar, 5, True)
        AgL.FSetColumnValue(MdlTable, "DiscountAc", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "CashAc", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "BankAc", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "DebitNoteAc", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "CreditNoteAc", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
    End Sub

    Private Sub FVoucher_Type(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DivisionWise", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "SiteWise", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "Number_Method", AgLibrary.ClsMain.SQLDataType.nVarChar, 9)
        AgL.FSetColumnValue(MdlTable, "Saperate_Narr", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Separate_Narr", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Common_Narr", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "ChqNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "ChqDt", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "ClgDt", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Affect_FA", AgLibrary.ClsMain.SQLDataType.Bit, , , , 1)
    End Sub

    Private Sub FEnviro(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DefaultSalesTaxGroupParty", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "DefaultSalesTaxGroupItem", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "PurchOrderShowIndentInLine", AgLibrary.ClsMain.SQLDataType.Bit, , , , 0)
        AgL.FSetColumnValue(MdlTable, "SaleAc", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "PostingAc", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "CashAc", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)

        AgL.FSetColumnValue(MdlTable, "IsLinkWithFA", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "IsNegativeStockAllowed", AgLibrary.ClsMain.SQLDataType.Bit, , , , 1)
        AgL.FSetColumnValue(MdlTable, "IsLotNoApplicable", AgLibrary.ClsMain.SQLDataType.Bit, , , , 1)
        AgL.FSetColumnValue(MdlTable, "DefaultDueDays", AgLibrary.ClsMain.SQLDataType.Float)

        AgL.FSetFKeyValue(MdlTable, "Site_Code", "Code", "SiteMast")
    End Sub

    Private Sub FItemType(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 20, True)
    End Sub

    Private Sub FSiteMast(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DrugLicenseNo", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "PAN", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
    End Sub

    Private Sub FSaleChallan(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "PaidAmt", AgLibrary.ClsMain.SQLDataType.Float)
    End Sub

    Private Sub FItemCategory(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, True)
        AgL.FSetColumnValue(MdlTable, "Description", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "ItemType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "EntryType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryStatus", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "MoveToLog", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MoveToLogDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "IsDeleted", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)

        AgL.FSetColumnValue(MdlTable, "PreparedBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "U_EntDt", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "U_AE", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Edit_Date", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "ModifiedBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)

        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        AgL.FSetFKeyValue(MdlTable, "ItemType", "Code", "ItemType")
    End Sub

    Private Sub FItem(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "ProfitMarginPer", AgLibrary.ClsMain.SQLDataType.Float, )
        'AgL.FSetNCIndexValue(MdlTable, "Description")
    End Sub

    Private Sub FItemGroup(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, True)
        AgL.FSetColumnValue(MdlTable, "Description", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "ItemType", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "ItemCategory", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "EntryType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryStatus", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "MoveToLog", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MoveToLogDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "IsDeleted", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)

        AgL.FSetColumnValue(MdlTable, "PreparedBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "U_EntDt", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "U_AE", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Edit_Date", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "ModifiedBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        AgL.FSetFKeyValue(MdlTable, "ItemCategory", "Code", "ItemCategory")
        AgL.FSetFKeyValue(MdlTable, "ItemType", "Code", "ItemType")
    End Sub

    Private Sub FSaleInvoice(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "TableCode", AgLibrary.ClsMain.SQLDataType.VarChar, 10)
        AgL.FSetColumnValue(MdlTable, "PaymentMode", AgLibrary.ClsMain.SQLDataType.VarChar, 20)
        AgL.FSetColumnValue(MdlTable, "PostingAc", AgLibrary.ClsMain.SQLDataType.VarChar, 10)
        AgL.FSetColumnValue(MdlTable, "PaidAmt", AgLibrary.ClsMain.SQLDataType.Float)

        AgL.FSetFKeyValue(MdlTable, "TableCode", "Code", "Ht_Table")
    End Sub

    Private Sub FSaleChallanDetail(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "TransactionType", AgLibrary.ClsMain.SQLDataType.VarChar, 10)
    End Sub

    Private Sub FSaleInvoiceDetail(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "TransactionType", AgLibrary.ClsMain.SQLDataType.VarChar, 10)
    End Sub


    Private Sub FBom(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "Description", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "ForQty", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)
        AgL.FSetColumnValue(MdlTable, "ForWeight", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)
        AgL.FSetColumnValue(MdlTable, "ForUnit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "TotalQty", AgLibrary.ClsMain.SQLDataType.Float, , , , 0)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "IsDeleted", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "EntryBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "EntryType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryStatus", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "MoveToLog", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MoveToLogDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Uid", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, IIf(EntryType = EntryPointType.Log, True, False))
    End Sub

    Private Sub FBomDetail(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int)
        AgL.FSetColumnValue(MdlTable, "Process", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Item", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Qty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "ConsumptionPer", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "ApplyIn", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "Uid", AgLibrary.ClsMain.SQLDataType.uniqueidentifier)

        If EntryType = EntryPointType.Log Then
            AgL.FSetFKeyValue(MdlTable, "UID", "UID", "Bom_Log")
        Else
            AgL.FSetFKeyValue(MdlTable, "Code", "Code", "Bom")
        End If
        AgL.FSetFKeyValue(MdlTable, "Item", "Code", "Item")
        AgL.FSetFKeyValue(MdlTable, "Process", "NCat", "Process")
    End Sub

    Private Sub FRUG_SampleSku(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "Size", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Construction", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "PileQuality", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "CostPerSqFeet", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        If EntryType = EntryPointType.Main Then
            AgL.FSetFKeyValue(MdlTable, "Code", "Code", "Item")
        Else
            AgL.FSetFKeyValue(MdlTable, "UID", "UID", "Item_Log")
        End If
    End Sub

    Private Sub FUnitConversion(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "FromUnit", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ToUnit", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "Multiplier", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Rounding", AgLibrary.ClsMain.SQLDataType.Int)

        AgL.FSetColumnValue(MdlTable, "EntryBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "EntryType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryStatus", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "MoveToLog", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MoveToLogDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "IsDeleted", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
    End Sub

    Private Sub FRUG_SampleSizeAvailable(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int, , True)
        AgL.FSetColumnValue(MdlTable, "Size", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        If EntryType = EntryPointType.Log Then
            AgL.FSetFKeyValue(MdlTable, "UID", "UID", "RUG_SampleSku_Log")
        Else
            AgL.FSetFKeyValue(MdlTable, "Code", "Code", "RUG_SampleSku")
        End If
        AgL.FSetFKeyValue(MdlTable, "Size", "Code", "Rug_Size")
    End Sub

    Private Sub FRUG_SampleContent(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int, , True)
        AgL.FSetColumnValue(MdlTable, "Item", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        If EntryType = EntryPointType.Log Then
            AgL.FSetFKeyValue(MdlTable, "UID", "UID", "RUG_SampleSku_Log")
        Else
            AgL.FSetFKeyValue(MdlTable, "Code", "Code", "RUG_SampleSku")
        End If
        AgL.FSetFKeyValue(MdlTable, "Item", "Code", "Item")
    End Sub

    Private Sub FRUG_SampleShade(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int, , True)
        AgL.FSetColumnValue(MdlTable, "Shade", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        If EntryType = EntryPointType.Log Then
            AgL.FSetFKeyValue(MdlTable, "UID", "UID", "RUG_SampleSku_Log")
        Else
            AgL.FSetFKeyValue(MdlTable, "Code", "Code", "RUG_SampleSku")
        End If
        AgL.FSetFKeyValue(MdlTable, "Shade", "Code", "Rug_Shade")
    End Sub

    Private Sub FRug_Size(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "Description", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "Shape", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "FeetLength", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "FeetWidth", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "FeetArea", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "FeetDiameter", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "MeterLength", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "MeterWidth", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "MeterArea", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "YardLength", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "YardWidth", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "YardArea", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "LFeet", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "LInch", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "WFeet", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "WInch", AgLibrary.ClsMain.SQLDataType.Float)

        AgL.FSetColumnValue(MdlTable, "IsDeleted", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "EntryBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "EntryType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryStatus", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "MoveToLog", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MoveToLogDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))
    End Sub

    Private Sub FRUG_Quality(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "ManualCode", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "Description", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "Construction", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "StdRugWeight", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "PileWeight", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "PileHeight", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "TuftPerSqrInch", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "WashingType", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "Clipping", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "Fringes", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "TotalQty", AgLibrary.ClsMain.SQLDataType.Float)
        AgL.FSetColumnValue(MdlTable, "Weight", AgLibrary.ClsMain.SQLDataType.Float)

        AgL.FSetColumnValue(MdlTable, "EntryBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "EntryType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryStatus", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "MoveToLog", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MoveToLogDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "IsDeleted", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        AgL.FSetFKeyValue(MdlTable, "Construction", "Code", "RUG_Construction")
    End Sub

    Private Sub FRUG_Shade(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "Description", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "Colour", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "Pantone", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "IsDeleted", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "EntryBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "EntryType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryStatus", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "MoveToLog", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MoveToLogDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))
    End Sub

    Private Sub FRUG_Collection(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "Description", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "Construction", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Quality", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "IsDeleted", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "EntryBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "EntryType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryStatus", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "MoveToLog", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MoveToLogDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))
    End Sub

    Private Sub FRUG_CollectionRateList(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "WEF", AgLibrary.ClsMain.SQLDataType.SmallDateTime)

        AgL.FSetColumnValue(MdlTable, "RateListCode", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)

        AgL.FSetColumnValue(MdlTable, "IsDeleted", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "EntryBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "EntryType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryStatus", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "MoveToLog", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MoveToLogDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, IIf(EntryType = EntryPointType.Log, True, False))
    End Sub

    Private Sub FRUG_CollectionRateListDetail(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "Sr", AgLibrary.ClsMain.SQLDataType.Int, , True)
        AgL.FSetColumnValue(MdlTable, "WEF", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "Collection", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Rate", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, IIf(EntryType = EntryPointType.Log, True, False))
    End Sub

    Private Sub FRUG_Construction(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "Description", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)

        AgL.FSetColumnValue(MdlTable, "PreparedBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "U_EntDt", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "U_AE", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Edit_Date", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "ModifiedBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
    End Sub

    Private Sub FPurchChallanDetail(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Sale_Rate", AgLibrary.ClsMain.SQLDataType.Float)
    End Sub

    Private Sub FRUG_Design(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 10, IIf(EntryType = EntryPointType.Main, True, False))
        AgL.FSetColumnValue(MdlTable, "ManualCode", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "Description", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "Construction", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "Carpet_Collection", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "Carpet_Style", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "PileQuality", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "Sample", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Colour", AgLibrary.ClsMain.SQLDataType.nVarChar, 50)
        AgL.FSetColumnValue(MdlTable, "Collection", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)

        AgL.FSetColumnValue(MdlTable, "EntryBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "EntryType", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "EntryStatus", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveBy", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "ApproveDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "MoveToLog", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "MoveToLogDate", AgLibrary.ClsMain.SQLDataType.SmallDateTime)
        AgL.FSetColumnValue(MdlTable, "IsDeleted", AgLibrary.ClsMain.SQLDataType.Bit)
        AgL.FSetColumnValue(MdlTable, "Div_Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 1)
        AgL.FSetColumnValue(MdlTable, "Status", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        AgL.FSetFKeyValue(MdlTable, "Construction", "Code", "RUG_Construction")
    End Sub

    Private Sub FRUG_DesignImage(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "Code", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "Photo", AgLibrary.ClsMain.SQLDataType.image)
        AgL.FSetColumnValue(MdlTable, "UID", AgLibrary.ClsMain.SQLDataType.uniqueidentifier, , IIf(EntryType = EntryPointType.Log, True, False))

        If EntryType = EntryPointType.Log Then
            AgL.FSetFKeyValue(MdlTable, "UID", "UID", "RUg_Design_Log")
        Else
            AgL.FSetFKeyValue(MdlTable, "Code", "Code", "RUg_Design")
        End If
    End Sub
#End Region

    Public Shared Sub FPrepareContraText(ByVal BlnOverWrite As Boolean, ByRef StrContraTextVar As String, _
                                         ByVal StrContraName As String, ByVal DblAmount As Double, ByVal StrDrCr As String)
        Dim IntNameMaxLen As Integer = 35, IntAmtMaxLen As Integer = 18, IntSpaceNeeded As Integer = 2
        StrContraName = AgL.XNull(AgL.Dman_Execute("Select Name from Subgroup  Where SubCode = '" & StrContraName & "'  ", AgL.GcnRead).ExecuteScalar)

        If BlnOverWrite Then
            StrContraTextVar = Mid(Trim(StrContraName), 1, IntNameMaxLen) & Space((IntNameMaxLen + IntSpaceNeeded) - Len(Mid(Trim(StrContraName), 1, IntNameMaxLen))) & Space(IntAmtMaxLen - Len(Format(Val(DblAmount), "##,##,##,##,##0.00"))) & Format(Val(DblAmount), "##,##,##,##,##0.00") & " " & Trim(StrDrCr)
        Else
            StrContraTextVar += Mid(Trim(StrContraName), 1, IntNameMaxLen) & Space((IntNameMaxLen + IntSpaceNeeded) - Len(Mid(Trim(StrContraName), 1, IntNameMaxLen))) & Space(IntAmtMaxLen - Len(Format(Val(DblAmount), "##,##,##,##,##0.00"))) & Format(Val(DblAmount), "##,##,##,##,##0.00") & " " & Trim(StrDrCr)
        End If
    End Sub

    Public Shared Sub PostStructureToAccounts(ByVal FGMain As AgStructure.AgCalcGrid, ByVal mNarr As String, ByVal mDocID As String, ByVal mDiv_Code As String,
                                              ByVal mSite_Code As String, ByVal Div_Code As String, ByVal mV_Type As String, ByVal mV_Prefix As String, ByVal mV_No As Integer,
                                              ByVal mRecID As String, ByVal PostingPartyAc As String, ByVal mV_Date As String,
                                              ByVal Conn As SQLiteConnection, ByVal Cmd As SQLiteCommand)
        Dim StrContraTextJV As String = ""
        Dim mPostSubCode = ""
        Dim I As Integer
        Dim mQry$ = "", bSelectionQry$ = ""
        Dim DtTemp As DataTable = Nothing


        For I = 0 To FGMain.Rows.Count - 1
            If Trim(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_PostAc, I).Value) <> "" Then
                If bSelectionQry = "" Then
                    bSelectionQry = " Select '" & FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_PostAc, I).Value & "' As PostAc, " &
                    " Case When " & AgL.Chk_Text(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_DrCr, I).Value) & " = 'Dr' Then " & Val(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Amount, I).Value) & "  " &
                    "      When " & AgL.Chk_Text(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_DrCr, I).Value) & " = 'Cr' Then " & -Val(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Amount, I).Value) & " End As Amount "
                Else
                    bSelectionQry += " UNION ALL "
                    bSelectionQry += " Select '" & FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_PostAc, I).Value & "' As PostAc, " &
                    " Case When " & AgL.Chk_Text(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_DrCr, I).Value) & " = 'Dr' Then " & Val(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Amount, I).Value) & "  " &
                    "      When " & AgL.Chk_Text(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_DrCr, I).Value) & " = 'Cr' Then " & -Val(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Amount, I).Value) & " End As Amount "

                End If
            End If
        Next

        If bSelectionQry = "" Then Exit Sub


        mQry = " Select Count(*)  " &
                " From (" & bSelectionQry & ") As V1 " &
                " Having Sum(Case When IfNull(V1.Amount,0) > 0 Then IfNull(V1.Amount,0) Else 0 End) <> abs(Sum(Case When IfNull(V1.Amount,0) < 0 Then IfNull(V1.Amount,0) Else 0 End))  "
        DtTemp = AgL.FillData(mQry, AgL.GcnRead).Tables(0)
        If DtTemp.Rows.Count > 0 Then
            If AgL.VNull(DtTemp.Rows(0)(0)) > 0 Then
                Err.Raise(1, , "Error In Ledger Posting. Debit and Credit balances are not equal.")
            End If
        End If



        mQry = " Select V1.PostAc, IfNull(Sum(V1.Amount),0) As Amount, " &
                " Case When IfNull(Sum(V1.Amount),0) > 0 Then 'Dr' " &
                "      When IfNull(Sum(V1.Amount),0) < 0 Then 'Cr' End As DrCr " &
                " From (" & bSelectionQry & ") As V1 " &
                " Group BY V1.PostAc "
        DtTemp = AgL.FillData(mQry, AgL.GcnRead).Tables(0)

        With DtTemp
            For I = 0 To .Rows.Count - 1
                If Trim(AgL.XNull(.Rows(I)("PostAc"))) <> "" Then
                    If AgL.StrCmp(AgL.XNull(.Rows(I)("PostAc")), "|PARTY|") Then
                        If AgL.VNull(.Rows(I)("Amount")) <> 0 And AgL.XNull(.Rows(I)("DrCr")) <> "" Then
                            If StrContraTextJV <> "" Then StrContraTextJV += vbCrLf
                            FPrepareContraText(False, StrContraTextJV, PostingPartyAc, Math.Abs(AgL.VNull(.Rows(I)("Amount"))), AgL.XNull(.Rows(I)("DrCr")))
                        End If
                    Else
                        If AgL.VNull(.Rows(I)("Amount")) <> 0 And AgL.XNull(.Rows(I)("DrCr")) <> "" Then
                            If StrContraTextJV <> "" Then StrContraTextJV += vbCrLf
                            FPrepareContraText(False, StrContraTextJV, AgL.XNull(.Rows(I)("PostAc")), Math.Abs(Val(AgL.VNull(.Rows(I)("Amount")))), AgL.XNull(.Rows(I)("DrCr")))
                        End If
                    End If
                End If
            Next
        End With

        Dim mSrl As Integer = 0, mDebit As Double, mCredit As Double
        mQry = "Delete from Ledger where docId='" & mDocID & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        With DtTemp
            For I = 0 To .Rows.Count - 1
                If Trim(AgL.XNull(.Rows(I)("PostAc"))) <> "" And Val(AgL.VNull(.Rows(I)("Amount"))) <> 0 Then
                    mSrl += 1

                    mDebit = 0 : mCredit = 0
                    If AgL.StrCmp(AgL.XNull(.Rows(I)("PostAc")), "|PARTY|") Then
                        mPostSubCode = PostingPartyAc
                    Else
                        mPostSubCode = AgL.XNull(.Rows(I)("PostAc"))
                    End If

                    If AgL.StrCmp(AgL.XNull(.Rows(I)("DrCr")), "Dr") Then
                        mDebit = Math.Abs(AgL.VNull(.Rows(I)("Amount")))
                    ElseIf AgL.StrCmp(AgL.XNull(.Rows(I)("DrCr")), "Cr") Then
                        mCredit = Math.Abs(AgL.VNull(.Rows(I)("Amount")))
                    End If

                    mQry = "Insert Into Ledger(DocId,RecId,V_SNo,V_Date,SubCode,ContraSub,AmtDr,AmtCr," &
                         " Narration,V_Type,V_No,V_Prefix,Site_Code,DivCode,Chq_No,Chq_Date,TDSCategory,TDSOnAmt,TDSDesc," &
                         " TDSPer,TDS_Of_V_SNo,System_Generated,FormulaString,ContraText) Values " &
                         " ('" & mDocID & "','" & mRecID & "'," & mSrl & "," & AgL.ConvertDate(mV_Date) & "," & AgL.Chk_Text(mPostSubCode) & "," & AgL.Chk_Text("") & ", " &
                         " " & mDebit & "," & mCredit & ", " &
                         " " & AgL.Chk_Text(mNarr) & ",'" & mV_Type & "','" & mV_No & "','" & mV_Prefix & "'," &
                         " '" & mSite_Code & "','" & mDiv_Code & "','" & AgL.Chk_Text("") & "'," &
                         " " & AgL.ConvertDate("") & "," & AgL.Chk_Text("") & "," &
                         " " & Val("") & "," & AgL.Chk_Text("") & "," & Val("") & "," & 0 & ",'Y','" & "" & "','" & StrContraTextJV & "')"
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                End If
            Next I
        End With
    End Sub

    Public Shared Sub PostStructureLineToAccounts(ByVal FGMain As AgStructure.AgCalcGrid, ByVal mNarr As String, ByVal mDocID As String, ByVal mDiv_Code As String,
                                              ByVal mSite_Code As String, ByVal Div_Code As String, ByVal mV_Type As String, ByVal mV_Prefix As String, ByVal mV_No As Integer,
                                              ByVal mRecID As String, ByVal PostingPartyAc As String, ByVal mV_Date As String,
                                              ByVal Conn As SQLiteConnection, ByVal Cmd As SQLiteCommand)
        Dim StrContraTextJV As String = ""
        Dim mPostSubCode = ""
        Dim I As Integer, J As Integer
        Dim mQry$ = "", bSelectionQry$ = ""
        Dim DtTemp As DataTable = Nothing

        bSelectionQry = ""
        For I = 0 To FGMain.Rows.Count - 1
            For J = 0 To FGMain.AgLineGrid.Rows.Count - 1
                If FGMain.AgChargesValue(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag, J, AgStructure.AgCalcGrid.LineColumnType.PostAc) <> "" Then
                    If bSelectionQry <> "" Then bSelectionQry += " UNION ALL "

                    bSelectionQry += " Select '" & FGMain.AgChargesValue(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag, J, AgStructure.AgCalcGrid.LineColumnType.PostAc) & "' As PostAc, " &
                    " Case When " & AgL.Chk_Text(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_DrCr, I).Value) & " = 'Dr' Then " & Val(FGMain.AgChargesValue(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag, J, AgStructure.AgCalcGrid.LineColumnType.Amount)) & "  " &
                    "      When " & AgL.Chk_Text(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_DrCr, I).Value) & " = 'Cr' Then " & -Val(FGMain.AgChargesValue(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag, J, AgStructure.AgCalcGrid.LineColumnType.Amount)) & " End As Amount "
                ElseIf Trim(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_PostAc, I).Value) <> "" Then
                    If bSelectionQry <> "" Then bSelectionQry += " UNION ALL "

                    bSelectionQry += " Select '" & FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_PostAc, I).Value & "' As PostAc, " &
                    " Case When " & AgL.Chk_Text(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_DrCr, I).Value) & " = 'Dr' Then " & Val(FGMain.AgChargesValue(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag, J, AgStructure.AgCalcGrid.LineColumnType.Amount)) & "  " &
                    "      When " & AgL.Chk_Text(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_DrCr, I).Value) & " = 'Cr' Then " & -Val(FGMain.AgChargesValue(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Charges, I).Tag, J, AgStructure.AgCalcGrid.LineColumnType.Amount)) & " End As Amount "

                End If
            Next
        Next

        If bSelectionQry = "" Then Exit Sub


        mQry = " Select Count(*)  " &
                " From (" & bSelectionQry & ") As V1 " &
                " Having Sum(Case When IfNull(V1.Amount,0) > 0 Then IfNull(V1.Amount,0) Else 0 End) <> abs(Sum(Case When IfNull(V1.Amount,0) < 0 Then IfNull(V1.Amount,0) Else 0 End))  "
        DtTemp = AgL.FillData(mQry, AgL.GcnRead).Tables(0)
        If DtTemp.Rows.Count > 0 Then
            If AgL.VNull(DtTemp.Rows(0)(0)) > 0 Then
                Err.Raise(1, , "Error In Ledger Posting. Debit and Credit balances are not equal.")
            End If
        End If



        mQry = " Select V1.PostAc, IfNull(Sum(V1.Amount),0) As Amount, " &
                " Case When IfNull(Sum(V1.Amount),0) > 0 Then 'Dr' " &
                "      When IfNull(Sum(V1.Amount),0) < 0 Then 'Cr' End As DrCr " &
                " From (" & bSelectionQry & ") As V1 " &
                " Group BY V1.PostAc "
        DtTemp = AgL.FillData(mQry, AgL.GcnRead).Tables(0)

        With DtTemp
            For I = 0 To .Rows.Count - 1
                If Trim(AgL.XNull(.Rows(I)("PostAc"))) <> "" Then
                    If AgL.StrCmp(AgL.XNull(.Rows(I)("PostAc")), "|PARTY|") Then
                        If AgL.VNull(.Rows(I)("Amount")) <> 0 And AgL.XNull(.Rows(I)("DrCr")) <> "" Then
                            If StrContraTextJV <> "" Then StrContraTextJV += vbCrLf
                            FPrepareContraText(False, StrContraTextJV, PostingPartyAc, Math.Abs(AgL.VNull(.Rows(I)("Amount"))), AgL.XNull(.Rows(I)("DrCr")))
                        End If
                    Else
                        If AgL.VNull(.Rows(I)("Amount")) <> 0 And AgL.XNull(.Rows(I)("DrCr")) <> "" Then
                            If StrContraTextJV <> "" Then StrContraTextJV += vbCrLf
                            FPrepareContraText(False, StrContraTextJV, AgL.XNull(.Rows(I)("PostAc")), Math.Abs(Val(AgL.VNull(.Rows(I)("Amount")))), AgL.XNull(.Rows(I)("DrCr")))
                        End If
                    End If
                End If
            Next
        End With

        Dim mSrl As Integer = 0, mDebit As Double, mCredit As Double
        mQry = "Delete from Ledger where docId='" & mDocID & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        With DtTemp
            For I = 0 To .Rows.Count - 1
                If Trim(AgL.XNull(.Rows(I)("PostAc"))) <> "" And Val(AgL.VNull(.Rows(I)("Amount"))) <> 0 Then
                    mSrl += 1

                    mDebit = 0 : mCredit = 0
                    If AgL.StrCmp(AgL.XNull(.Rows(I)("PostAc")), "|PARTY|") Then
                        mPostSubCode = PostingPartyAc
                    Else
                        mPostSubCode = AgL.XNull(.Rows(I)("PostAc"))
                    End If

                    If AgL.StrCmp(AgL.XNull(.Rows(I)("DrCr")), "Dr") Then
                        mDebit = Math.Abs(AgL.VNull(.Rows(I)("Amount")))
                    ElseIf AgL.StrCmp(AgL.XNull(.Rows(I)("DrCr")), "Cr") Then
                        mCredit = Math.Abs(AgL.VNull(.Rows(I)("Amount")))
                    End If

                    mQry = "Insert Into Ledger(DocId,RecId,V_SNo,V_Date,SubCode,ContraSub,AmtDr,AmtCr," &
                         " Narration,V_Type,V_No,V_Prefix,Site_Code,DivCode,Chq_No,Chq_Date,TDSCategory,TDSOnAmt,TDSDesc," &
                         " TDSPer,TDS_Of_V_SNo,System_Generated,FormulaString,ContraText) Values " &
                         " ('" & mDocID & "','" & mRecID & "'," & mSrl & "," & AgL.ConvertDate(mV_Date) & "," & AgL.Chk_Text(mPostSubCode) & "," & AgL.Chk_Text("") & ", " &
                         " " & mDebit & "," & mCredit & ", " &
                         " " & AgL.Chk_Text(mNarr) & ",'" & mV_Type & "','" & mV_No & "','" & mV_Prefix & "'," &
                         " '" & mSite_Code & "','" & mDiv_Code & "','" & AgL.Chk_Text("") & "'," &
                         " " & AgL.ConvertDate("") & "," & AgL.Chk_Text("") & "," &
                         " " & Val("") & "," & AgL.Chk_Text("") & "," & Val("") & "," & 0 & ",'Y','" & "" & "','" & StrContraTextJV & "')"
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                End If
            Next I
        End With
    End Sub

    Public Shared Sub ProcCreateLink(ByVal DGL As DataGridView, ByVal ColumnName As String)
        Try
            DGL.Columns(ColumnName).CellTemplate.Style.Font = New Font(DGL.DefaultCellStyle.Font.FontFamily, DGL.DefaultCellStyle.Font.Size, FontStyle.Underline)
            DGL.Columns(ColumnName).CellTemplate.Style.ForeColor = Color.Blue

            If DGL.Rows.Count > 0 Then
                DGL.Item(ColumnName, 0).Style.Font = New Font(DGL.DefaultCellStyle.Font.FontFamily, DGL.DefaultCellStyle.Font.Size, FontStyle.Underline)
                DGL.Item(ColumnName, 0).Style.ForeColor = Color.Blue
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Shared Sub ProcOpenLinkForm(ByVal Mnu As System.Windows.Forms.ToolStripItem, ByVal SearchCode As String, ByVal Parent As Form)
        Dim FrmObj As AgTemplate.TempTransaction
        Dim CFOpen As New ClsFunction
        Try
            FrmObj = CFOpen.FOpen(Mnu.Name, Mnu.Text, True)
            If FrmObj IsNot Nothing Then
                FrmObj.MdiParent = Parent
                FrmObj.Show()
                FrmObj.FindMove(SearchCode)
                FrmObj = Nothing
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Shared Sub FSaveInMailOutBox(ByVal V_Type As String, ByVal GenDocId As String,
            ByVal Party As String, ByVal PartyName As String,
            ByVal Agent As String, ByVal AgentName As String,
            ByVal Supplier As String, ByVal SupplierName As String,
            ByVal V_Date As String, ByVal ReferenceNo As String,
            ByVal Conn As SQLiteConnection, ByVal Cmd As SQLiteCommand,
            Optional ByVal Attachment As String = "")

        Dim mQry$ = "", bSubject$ = "", bDescription$ = "", bRecepientEMail$ = "", bRecepient$ = "", Code$ = ""
        Dim DtTemp As DataTable = Nothing
        Dim I As Integer = 0, mSr As Integer = 0

        mQry = " SELECT * FROM MailEnviro Where V_Type = '" & V_Type & "'"
        DtTemp = AgL.FillData(mQry, AgL.GcnRead).Tables(0)

        If DtTemp.Rows.Count = 0 Then Exit Sub

        bSubject = DtTemp.Rows(0)("Subject")
        bDescription = Replace(Replace(Replace(Replace(Replace(DtTemp.Rows(0)("Message"), "<Party>", PartyName), "<Agent>", AgentName), "<Date>", V_Date), "<ReferenceNo>", ReferenceNo), "<Supplier>", SupplierName)

        Code = AgL.GetMaxId("MailOutbox", "Code", AgL.GCn, AgL.PubDivCode, AgL.PubSiteCode, 8, True, True, AgL.ECmd, AgL.Gcn_ConnectionString)

        mQry = " Delete From MailOutBoxDetail Where Code = (Select Code From MailOutbox Where GenDocId = '" & GenDocId & "')"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mQry = " Delete From MailOutbox Where GenDocId = '" & GenDocId & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        If DtTemp.Rows.Count > 0 Then
            mQry = " INSERT INTO MailOutBox(Code, GenDocId, V_Type, Sender, Subject, Description, IsSend, " &
                    " EntryBy, EntryDate, Div_Code) " &
                    " VALUES('" & Code & "', '" & GenDocId & "', " & AgL.Chk_Text(V_Type) & ", " &
                    " " & AgL.Chk_Text(DtTemp.Rows(0)("Sender")) & ", " &
                    " " & AgL.Chk_Text(DtTemp.Rows(0)("Subject")) & ", " &
                    " " & AgL.Chk_Text(bDescription) & ", 0, " &
                    " '" & AgL.PubUserName & "', '" & AgL.GetDateTime(AgL.GcnRead) & "', '" & AgL.PubDivCode & "')"
            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
        End If

        mQry = " SELECT L.* " &
                " FROM MailEnviroDetail L " &
                " LEFT JOIN MailEnviro H On L.Code = H.Code " &
                " Where H.V_Type = '" & V_Type & "'"
        DtTemp = AgL.FillData(mQry, AgL.GcnRead).Tables(0)

        With DtTemp
            If .Rows.Count > 0 Then
                For I = 0 To DtTemp.Rows.Count - 1
                    mSr += 1
                    If AgL.XNull(.Rows(I)("Recepient")) = "<Party>" Then
                        bRecepientEMail = FRetMailId(Party)
                        bRecepient = Party
                    ElseIf AgL.XNull(.Rows(I)("Recepient")) = "<Agent>" Then
                        bRecepientEMail = FRetMailId(Agent)
                        bRecepient = Agent
                    ElseIf AgL.XNull(.Rows(I)("Recepient")) = "<Supplier>" Then
                        bRecepientEMail = FRetMailId(Supplier)
                        bRecepient = Supplier
                    Else
                        bRecepientEMail = FRetMailId(AgL.XNull(.Rows(I)("Recepient")))
                        bRecepient = AgL.XNull(.Rows(I)("Recepient"))
                    End If
                    mQry = " INSERT INTO MailOutBoxDetail(Code, Sr, RecepientType, Recepient, " &
                            " RecepientEMail) " &
                            " VALUES ('" & Code & "', " & Val(mSr) & ", " &
                            " " & AgL.Chk_Text(AgL.XNull(.Rows(I)("RecepientType"))) & ", " &
                            " " & AgL.Chk_Text(bRecepient) & ",	" &
                            " " & AgL.Chk_Text(bRecepientEMail) & ")"
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                Next
            End If
        End With

        If Attachment <> "" Then
            FSaveAttachments(Code, Attachment)
        End If
    End Sub

    Public Shared Sub FSaveAttachments(ByVal Code As String, ByVal FileName As String)
        Dim I As Integer = 0
        Dim mFileToUpload$ = ""
        Dim Extension$ = ""
        Dim mSr As Integer = 0
        Dim mQry$ = ""

        Dim Conn As SQLiteConnection = ClsMain.FCreateFileDbConn()
        Dim Cmd As SQLiteCommand = New SQLiteCommand
        Cmd.Connection = Conn

        mQry = " Delete From MailOutBoxAttachments Where Code = '" & Code & "'"
        AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)

        mFileToUpload = FileName
        Extension = System.IO.Path.GetExtension(FileName)
        mSr = 1

        If StrComp(Extension, ".bmp", CompareMethod.Text) = 0 Or
                    StrComp(Extension, ".jpg", CompareMethod.Text) = 0 Or
                    StrComp(Extension, ".jpeg", CompareMethod.Text) = 0 Or
                    StrComp(Extension, ".png", CompareMethod.Text) = 0 Or
                    StrComp(Extension, ".gif", CompareMethod.Text) = 0 Then
            UploadImageOrFile(mFileToUpload, "Image", Code, mSr)
        Else
            UploadImageOrFile(mFileToUpload, Extension, Code, mSr)
        End If
    End Sub

    Public Shared Sub UploadImageOrFile(ByVal sFilePath As String, ByVal sFileType As String, ByVal Code As String, ByVal Sr As Integer)
        Dim SqlCom As SQLiteCommand
        Dim FileContent As Byte()
        Dim sFileName As String
        Dim qry As String

        Try
            Dim Conn As SQLiteConnection = ClsMain.FCreateFileDbConn()
            Dim Cmd As SQLiteCommand = New SQLiteCommand
            Cmd.Connection = Conn

            FileContent = ReadFile(sFilePath)
            sFileName = System.IO.Path.GetFileName(sFilePath)

            qry = "Insert into MailOutBoxAttachments (Code, Sr, FileName,FileContent," &
                    " FileType) values(@Code, @Sr, @FileName, @FileContent," &
                    " @FileType)"

            SqlCom = New SQLiteCommand(qry, Conn)

            SqlCom.Parameters.Add(New SQLiteParameter("@Code", Code))
            SqlCom.Parameters.Add(New SQLiteParameter("@Sr", Sr))
            SqlCom.Parameters.Add(New SQLiteParameter("@FileName", sFileName))
            SqlCom.Parameters.Add(New SQLiteParameter("@FileContent", DirectCast(FileContent, Object)))
            SqlCom.Parameters.Add(New SQLiteParameter("@FileType", sFileType))
            SqlCom.ExecuteNonQuery()

        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try
    End Sub

    Public Shared Function ReadFile(ByVal sPath As String) As Byte()
        Dim data As Byte() = Nothing
        Dim fInfo As New FileInfo(sPath)
        Dim numBytes As Long = fInfo.Length
        Dim fStream As New FileStream(sPath, FileMode.Open, FileAccess.Read)
        Dim br As New BinaryReader(fStream)
        data = br.ReadBytes(CInt(numBytes))
        Return data
    End Function

    Public Shared Function FRetMailId(ByVal SubCode As String)
        Dim mQry$ = ""
        mQry = " Select EMail From SubGroup Sg  Where SubCode = '" & SubCode & "' "
        FRetMailId = AgL.XNull(AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar)
    End Function

    Public Shared Function FCreateFileDbConn() As SQLiteConnection
        Dim mQry$ = ""
        Try
            Dim DatabaseName$ = ""
            Dim DsTemp As DataSet = Nothing
            mQry = " Select FileDbName From Company Where Comp_Code = '" & AgL.PubCompCode & "' "
            DatabaseName = AgL.XNull(AgL.Dman_Execute(mQry, AgL.GcnRead).ExecuteScalar)
            Dim Cs As String = "Persist Security Info=False;User ID='" & AgL.PubDBUserSQL & "';pwd=" & AgL.PubDBPasswordSQL & ";Initial Catalog=" & DatabaseName & ";Data Source=" & AgL.PubServerName

            Dim Conn As SQLiteConnection = New SQLiteConnection(Cs)
            If Conn.State = ConnectionState.Closed Then Conn.Open()

            FCreateFileDbConn = Conn
        Catch ex As Exception
            FCreateFileDbConn = Nothing
            MsgBox(ex.Message)
        End Try
    End Function

    Public Shared Function FSendEMail(ByVal SearchCode As String) As Boolean
        Dim MLDFrom As System.Net.Mail.MailAddress
        Dim MLMMain As System.Net.Mail.MailMessage
        Dim SMTPMain As System.Net.Mail.SmtpClient
        Dim I As Integer
        Dim DtFromEmail As DataTable = Nothing
        Dim DtRecepients As DataTable = Nothing
        Dim DtAttachments As DataTable = Nothing
        Dim SmtpHost$ = "", SmtpPort$ = ""
        Dim bBlnEnableSsl As Boolean = False
        Dim mQry$ = ""


        Try
            'If AgL.PubDtEnviro_EMail.Rows.Count > 0 Then
            '    bBlnEnableSsl = AgL.VNull(AgL.PubDtEnviro_EMail.Rows(0)("EnableSsl"))
            'End If

            mQry = " SELECT H.*, S.FromEmailAddress, S.FromEmailPassword, S.SMTPHost, S.SMTPPort " &
                    " FROM MailOutBox H  " &
                    " LEFT JOIN MailSender S  On H.Sender = S.Code " &
                    " WHERE H.Code = '" & SearchCode & "'"
            DtFromEmail = AgL.FillData(mQry, AgL.GcnRead).Tables(0)

            If DtFromEmail.Rows.Count > 0 Then
                SmtpHost = AgL.XNull(DtFromEmail.Rows(0)("SmtpHost"))
                SmtpPort = AgL.XNull(DtFromEmail.Rows(0)("SmtpPort"))

                MLDFrom = New System.Net.Mail.MailAddress(AgL.XNull(DtFromEmail.Rows(0)("FromEMailAddress")))
                MLMMain = New System.Net.Mail.MailMessage()
                MLMMain.From = MLDFrom
                SMTPMain = New System.Net.Mail.SmtpClient(SmtpHost, SmtpPort)
                MLMMain.Body = AgL.XNull(DtFromEmail.Rows(0)("Description"))
                MLMMain.Subject = AgL.XNull(DtFromEmail.Rows(0)("Subject"))

                mQry = " SELECT * FROM MailOutBoxDetail  WHERE Code = '" & SearchCode & "'"
                DtRecepients = AgL.FillData(mQry, AgL.GcnRead).Tables(0)
                With DtRecepients
                    If .Rows.Count > 0 Then
                        For I = 0 To .Rows.Count - 1
                            If AgL.XNull(.Rows(I)("RecepientType")) = "To" Then
                                MLMMain.To.Add(AgL.XNull(.Rows(I)("RecepientEMail")))
                            ElseIf AgL.XNull(.Rows(I)("RecepientType")) = "Cc" Then
                                MLMMain.CC.Add(AgL.XNull(.Rows(I)("RecepientEMail")))
                            ElseIf AgL.XNull(.Rows(I)("RecepientType")) = "Cc" Then
                                MLMMain.Bcc.Add(AgL.XNull(.Rows(I)("RecepientEMail")))
                            End If
                        Next
                    End If
                End With

                Dim Conn As SQLiteConnection = ClsMain.FCreateFileDbConn()
                Dim Cmd As SQLiteCommand = New SQLiteCommand
                Cmd.Connection = Conn

                mQry = " Select * From MailOutBoxAttachments  Where Code = '" & SearchCode & "' "
                DtAttachments = AgL.FillData(mQry, Conn).Tables(0)

                With DtAttachments
                    If .Rows.Count > 0 Then
                        For I = 0 To .Rows.Count - 1
                            Dim ByteData As Byte() = DirectCast(.Rows(I)("FileContent"), Byte())
                            Dim MS As MemoryStream = New System.IO.MemoryStream(ByteData)
                            MLMMain.Attachments.Add(New System.Net.Mail.Attachment(MS, AgL.XNull(.Rows(I)("FileName")).ToString))
                        Next
                    End If
                End With

                SMTPMain.Credentials = New Net.NetworkCredential(DtFromEmail.Rows(0)("FromEmailAddress"), DtFromEmail.Rows(0)("FromEmailPassword"))
                SMTPMain.EnableSsl = True
                SMTPMain.Send(MLMMain)
                MLMMain.Dispose()
                FSendEMail = True


            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function

    'Public Shared Sub FPrintThisDocument(ByVal objFrm As Object, ByVal V_Type As String, _
    '        Optional ByVal Report_QueryList As String = "", Optional ByVal Report_NameList As String = "", _
    '        Optional ByVal Report_TitleList As String = "", Optional ByVal Report_FormatList As String = "", _
    '        Optional ByVal SubReport_QueryList As String = "", _
    '        Optional ByVal SubReport_NameList As String = "")

    '    Dim DtVTypeSetting As DataTable = Nothing
    '    Dim mQry As String = ""
    '    Dim mCrd As New ReportDocument
    '    Dim ReportView As New AgLibrary.RepView
    '    Dim DsRep As New DataSet
    '    Dim strQry As String = ""

    '    Dim RepName As String = ""
    '    Dim RepTitle As String = ""
    '    Dim RepQry As String = ""

    '    Dim RetIndex As Integer = 0

    '    Dim Report_QryArr() As String = Nothing
    '    Dim Report_NameArr() As String = Nothing
    '    Dim Report_TitleArr() As String = Nothing
    '    Dim Report_FormatArr() As String = Nothing

    '    Dim SubReport_QryArr() As String = Nothing
    '    Dim SubReport_NameArr() As String = Nothing
    '    Dim SubReport_DataSetArr() As DataSet = Nothing

    '    Dim I As Integer = 0

    '    Try
    '        mQry = "Select * from Voucher_Type_Settings  " & _
    '                   "Where V_Type = '" & V_Type & "' " & _
    '                   "And Site_Code = '" & AgL.PubSiteCode & "' " & _
    '                   "And Div_Code  = '" & AgL.PubDivCode & "' "
    '        DtVTypeSetting = AgL.FillData(mQry, AgL.GcnRead).Tables(0)
    '        If DtVTypeSetting.Rows.Count <> 0 Then
    '            If AgL.XNull(DtVTypeSetting.Rows(0)("Query")) <> "" Then
    '                Report_QueryList = AgL.XNull(DtVTypeSetting.Rows(0)("Query"))
    '                Report_QueryList = Replace(Report_QueryList.ToString.ToUpper, "<SEARCHCODE>", objFrm.mSearchCode)
    '            End If

    '            If AgL.XNull(DtVTypeSetting.Rows(0)("Report_Name")) <> "" Then
    '                Report_NameList = AgL.XNull(DtVTypeSetting.Rows(0)("Report_Name"))
    '            End If

    '            If AgL.XNull(DtVTypeSetting.Rows(0)("Report_Heading")) <> "" Then
    '                Report_TitleList = AgL.XNull(DtVTypeSetting.Rows(0)("Report_Heading"))
    '            End If

    '            If AgL.XNull(DtVTypeSetting.Rows(0)("Report_Format")) <> "" Then
    '                Report_FormatList = AgL.XNull(DtVTypeSetting.Rows(0)("Report_Format"))
    '            End If

    '            If AgL.XNull(DtVTypeSetting.Rows(0)("SubReport_QueryList")) <> "" Then
    '                SubReport_QueryList = AgL.XNull(DtVTypeSetting.Rows(0)("SubReport_QueryList"))
    '                SubReport_QueryList = Replace(SubReport_QueryList.ToString.ToUpper, "<SEARCHCODE>", objFrm.mSearchCode)
    '            End If

    '            If AgL.XNull(DtVTypeSetting.Rows(0)("SubReport_NameList")) <> "" Then
    '                SubReport_NameList = AgL.XNull(DtVTypeSetting.Rows(0)("SubReport_NameList"))
    '            End If
    '        End If

    '        If Report_QueryList <> "" Then Report_QryArr = Split(Report_QueryList, "|")
    '        If Report_TitleList <> "" Then Report_TitleArr = Split(Report_TitleList, "|")
    '        If Report_NameList <> "" Then Report_NameArr = Split(Report_NameList, "|")

    '        If Report_FormatList <> "" Then
    '            Report_FormatArr = Split(Report_FormatList, "|")

    '            For I = 0 To Report_FormatArr.Length - 1
    '                If strQry <> "" Then strQry += " UNION ALL "
    '                strQry += " Select " & I & " As Code, '" & Report_FormatArr(I) & "' As Name "
    '            Next

    '            Dim FRH_Single As DMHelpGrid.FrmHelpGrid
    '            FRH_Single = New DMHelpGrid.FrmHelpGrid(New DataView(AgL.FillData(strQry, AgL.GCn).TABLES(0)), "", 300, 350, , , False)
    '            FRH_Single.FFormatColumn(0, , 0, , False)
    '            FRH_Single.FFormatColumn(1, "Report Format", 250, DataGridViewContentAlignment.MiddleLeft)
    '            FRH_Single.StartPosition = FormStartPosition.CenterScreen
    '            FRH_Single.ShowDialog()

    '            If FRH_Single.BytBtnValue = 0 Then
    '                RetIndex = FRH_Single.DRReturn("Code")
    '            End If

    '            If Report_NameArr.Length = Report_FormatArr.Length Then RepName = Report_NameArr(RetIndex) Else RepName = Report_NameArr(0)
    '            If Report_TitleArr.Length = Report_FormatArr.Length Then RepTitle = Report_TitleArr(RetIndex) Else RepTitle = Report_TitleArr(0)
    '            If Report_QryArr.Length = Report_FormatArr.Length Then RepQry = Report_QryArr(RetIndex) Else RepQry = Report_QryArr(0)
    '        Else
    '            RepName = Report_NameArr(0)
    '            RepTitle = Report_TitleArr(0)
    '            RepQry = Report_QryArr(0)
    '        End If

    '        AgL.ADMain = New SqlClient.SqlDataAdapter(RepQry, AgL.GCn)
    '        AgL.ADMain.Fill(DsRep)
    '        AgPL.CreateFieldDefFile1(DsRep, AgL.PubReportPath & "\" & RepName & ".ttx", True)



    '        If SubReport_QueryList <> "" Then SubReport_QueryList = Replace(SubReport_QueryList.ToString.ToUpper, "<SEARCHCODE>", objFrm.mSearchCode)
    '        If SubReport_QueryList <> "" Then SubReport_QryArr = Split(SubReport_QueryList, "|")
    '        If SubReport_NameList <> "" Then SubReport_NameArr = Split(SubReport_NameList, "|")

    '        If SubReport_QryArr IsNot Nothing And SubReport_NameArr IsNot Nothing Then
    '            If SubReport_QryArr.Length <> SubReport_NameArr.Length Then
    '                MsgBox("Number Of SubReport Qries And SubReport Names Are Not Equal.", MsgBoxStyle.Information)
    '                Exit Sub
    '            End If

    '            For I = 0 To SubReport_QryArr.Length - 1
    '                AgL.ADMain = New SqlClient.SqlDataAdapter(SubReport_QryArr(I).ToString, AgL.GCn)
    '                ReDim Preserve SubReport_DataSetArr(I)
    '                SubReport_DataSetArr(I) = New DataSet
    '                AgL.ADMain.Fill(SubReport_DataSetArr(I))
    '                AgPL.CreateFieldDefFile1(SubReport_DataSetArr(I), AgL.PubReportPath & "\" & RepName & (I + 1).ToString & ".ttx", True)
    '            Next
    '        End If

    '        mCrd.Load(AgL.PubReportPath & "\" & RepName & ".rpt")
    '        mCrd.SetDataSource(DsRep.Tables(0))

    '        If SubReport_QryArr IsNot Nothing And SubReport_NameArr IsNot Nothing Then
    '            For I = 0 To SubReport_NameArr.Length - 1
    '                Try
    '                    mCrd.OpenSubreport(SubReport_NameArr(I).ToString).Database.Tables(0).SetDataSource(SubReport_DataSetArr(I).Tables(0))
    '                Catch ex As Exception
    '                End Try
    '            Next
    '        End If

    '        CType(ReportView.Controls("CrvReport"), CrystalDecisions.Windows.Forms.CrystalReportViewer).ReportSource = mCrd
    '        AgPL.Formula_Set(mCrd, RepTitle)
    '        AgPL.Show_Report(ReportView, "* " & RepTitle & " *", objFrm.MdiParent)

    '        Call AgL.LogTableEntry(objFrm.mSearchCode, objFrm.Text, "P", AgL.PubMachineName, AgL.PubUserName, AgL.PubLoginDate, AgL.GCn, AgL.ECmd)
    '    Catch Ex As Exception
    '        MsgBox(Ex.Message)
    '    End Try
    'End Sub

    Public Shared Sub FPrintThisDocument(ByVal objFrm As Object, ByVal V_Type As String,
         Optional ByVal Report_QueryList As String = "", Optional ByVal Report_NameList As String = "",
         Optional ByVal Report_TitleList As String = "", Optional ByVal Report_FormatList As String = "",
         Optional ByVal SubReport_QueryList As String = "",
         Optional ByVal SubReport_NameList As String = "", Optional ByVal PartyCode As String = "", Optional ByVal V_Date As String = "")

        Dim DtVTypeSetting As DataTable = Nothing
        Dim mQry As String = ""
        Dim mCrd As New ReportDocument
        Dim ReportView As New AgLibrary.RepView
        Dim DsRep As New DataSet
        Dim strQry As String = ""

        Dim RepName As String = ""
        Dim RepTitle As String = ""
        Dim RepQry As String = ""

        Dim RetIndex As Integer = 0

        Dim Report_QryArr() As String = Nothing
        Dim Report_NameArr() As String = Nothing
        Dim Report_TitleArr() As String = Nothing
        Dim Report_FormatArr() As String = Nothing

        Dim SubReport_QryArr() As String = Nothing
        Dim SubReport_NameArr() As String = Nothing
        Dim SubReport_DataSetArr() As DataSet = Nothing

        Dim I As Integer = 0

        Try
            mQry = "Select * from Voucher_Type_Settings  " &
                       "Where V_Type = '" & V_Type & "' " &
                       "And Site_Code = '" & AgL.PubSiteCode & "' " &
                       "And Div_Code  = '" & AgL.PubDivCode & "' "
            DtVTypeSetting = AgL.FillData(mQry, AgL.GcnRead).Tables(0)
            If DtVTypeSetting.Rows.Count <> 0 Then
                If AgL.XNull(DtVTypeSetting.Rows(0)("Query")) <> "" Then
                    Report_QueryList = AgL.XNull(DtVTypeSetting.Rows(0)("Query"))
                    Report_QueryList = Replace(Report_QueryList.ToString.ToUpper, "`<SEARCHCODE>`", "'" & objFrm.mSearchCode & "'")
                    Report_QueryList = Replace(Report_QueryList.ToString.ToUpper, "`<PARTYCODE>`", "'" & PartyCode & "'")
                    Report_QueryList = Replace(Report_QueryList.ToString.ToUpper, "`<VOUCHERDATE>`", "'" & V_Date & "'")
                    Report_QueryList = Replace(Report_QueryList.ToString.ToUpper, "`", "'")
                End If

                If AgL.XNull(DtVTypeSetting.Rows(0)("Report_Name")) <> "" Then
                    Report_NameList = AgL.XNull(DtVTypeSetting.Rows(0)("Report_Name"))
                End If

                If AgL.XNull(DtVTypeSetting.Rows(0)("Report_Heading")) <> "" Then
                    Report_TitleList = AgL.XNull(DtVTypeSetting.Rows(0)("Report_Heading"))
                End If

                If AgL.XNull(DtVTypeSetting.Rows(0)("Report_Format")) <> "" Then
                    Report_FormatList = AgL.XNull(DtVTypeSetting.Rows(0)("Report_Format"))
                End If

                If AgL.XNull(DtVTypeSetting.Rows(0)("SubReport_QueryList")) <> "" Then
                    SubReport_QueryList = AgL.XNull(DtVTypeSetting.Rows(0)("SubReport_QueryList"))
                    SubReport_QueryList = Replace(SubReport_QueryList.ToString.ToUpper, "`<SEARCHCODE>`", "'" & objFrm.mSearchCode & "'")
                    SubReport_QueryList = Replace(SubReport_QueryList.ToString.ToUpper, "`<PARTYCODE>`", "'" & PartyCode & "'")
                    SubReport_QueryList = Replace(SubReport_QueryList.ToString.ToUpper, "`<VOUCHERDATE>`", "'" & V_Date & "'")
                End If

                If AgL.XNull(DtVTypeSetting.Rows(0)("SubReport_NameList")) <> "" Then
                    SubReport_NameList = AgL.XNull(DtVTypeSetting.Rows(0)("SubReport_NameList"))
                End If
            End If

            If Report_QueryList <> "" Then Report_QryArr = Split(Report_QueryList, "|")
            If Report_TitleList <> "" Then Report_TitleArr = Split(Report_TitleList, "|")
            If Report_NameList <> "" Then Report_NameArr = Split(Report_NameList, "|")

            If Report_FormatList <> "" Then
                Report_FormatArr = Split(Report_FormatList, "|")

                For I = 0 To Report_FormatArr.Length - 1
                    If strQry <> "" Then strQry += " UNION ALL "
                    strQry += " Select " & I & " As Code, '" & Report_FormatArr(I) & "' As Name "
                Next

                Dim FRH_Single As DMHelpGrid.FrmHelpGrid
                FRH_Single = New DMHelpGrid.FrmHelpGrid(New DataView(AgL.FillData(strQry, AgL.GCn).TABLES(0)), "", 300, 350, , , False)
                FRH_Single.FFormatColumn(0, , 0, , False)
                FRH_Single.FFormatColumn(1, "Report Format", 250, DataGridViewContentAlignment.MiddleLeft)
                FRH_Single.StartPosition = FormStartPosition.CenterScreen
                FRH_Single.ShowDialog()

                If FRH_Single.BytBtnValue = 0 Then
                    RetIndex = FRH_Single.DRReturn("Code")
                End If

                If Report_NameArr.Length = Report_FormatArr.Length Then RepName = Report_NameArr(RetIndex) Else RepName = Report_NameArr(0)
                If Report_TitleArr.Length = Report_FormatArr.Length Then RepTitle = Report_TitleArr(RetIndex) Else RepTitle = Report_TitleArr(0)
                If Report_QryArr.Length = Report_FormatArr.Length Then RepQry = Report_QryArr(RetIndex) Else RepQry = Report_QryArr(0)
            Else
                RepName = Report_NameArr(0)
                RepTitle = Report_TitleArr(0)
                RepQry = Report_QryArr(0)
            End If

            AgL.ADMain = New SQLiteDataAdapter(RepQry, AgL.GCn)
            AgL.ADMain.Fill(DsRep)
            AgPL.CreateFieldDefFile1(DsRep, AgL.PubReportPath & "\" & RepName & ".ttx", True)

            If SubReport_QueryList <> "" Then SubReport_QryArr = Split(SubReport_QueryList, "|")
            If SubReport_NameList <> "" Then SubReport_NameArr = Split(SubReport_NameList, "|")

            If SubReport_QryArr IsNot Nothing And SubReport_NameArr IsNot Nothing Then
                If SubReport_QryArr.Length <> SubReport_NameArr.Length Then
                    MsgBox("Number Of SubReport Qries And SubReport Names Are Not Equal.", MsgBoxStyle.Information)
                    Exit Sub
                End If

                For I = 0 To SubReport_QryArr.Length - 1
                    AgL.ADMain = New SQLiteDataAdapter(SubReport_QryArr(I).ToString, AgL.GCn)
                    ReDim Preserve SubReport_DataSetArr(I)
                    SubReport_DataSetArr(I) = New DataSet
                    AgL.ADMain.Fill(SubReport_DataSetArr(I))
                    AgPL.CreateFieldDefFile1(SubReport_DataSetArr(I), AgL.PubReportPath & "\" & Report_NameList & (I + 1).ToString & ".ttx", True)
                Next
            End If

            mCrd.Load(AgL.PubReportPath & "\" & RepName & ".rpt")
            mCrd.SetDataSource(DsRep.Tables(0))

            If SubReport_QryArr IsNot Nothing And SubReport_NameArr IsNot Nothing Then
                For I = 0 To SubReport_NameArr.Length - 1
                    mCrd.OpenSubreport(SubReport_NameArr(I).ToString).Database.Tables(0).SetDataSource(SubReport_DataSetArr(I).Tables(0))
                Next
            End If

            CType(ReportView.Controls("CrvReport"), CrystalDecisions.Windows.Forms.CrystalReportViewer).ReportSource = mCrd
            AgPL.Formula_Set(mCrd, RepTitle)
            AgPL.Show_Report(ReportView, "* " & RepTitle & " *", objFrm.MdiParent)

            Call AgL.LogTableEntry(objFrm.mSearchCode, objFrm.Text, "P", AgL.PubMachineName, AgL.PubUserName, AgL.PubLoginDate, AgL.GCn, AgL.ECmd)
        Catch Ex As Exception
            MsgBox(Ex.Message)
        End Try
    End Sub

    Public Shared Sub FPrintThisDocument(ByVal objFrm As Object, ByVal V_Type As String,
        Optional ByVal Report_QueryList As String = "", Optional ByVal Report_NameList As String = "",
        Optional ByVal Report_TitleList As String = "", Optional ByVal Report_FormatList As String = "",
        Optional ByVal SubReport_QueryList As String = "",
        Optional ByVal SubReport_NameList As String = "")

        Dim DtVTypeSetting As DataTable = Nothing
        Dim mQry As String = ""
        Dim mCrd As New ReportDocument
        Dim ReportView As New AgLibrary.RepView
        Dim DsRep As New DataSet
        Dim strQry As String = ""

        Dim RepName As String = ""
        Dim RepTitle As String = ""
        Dim RepQry As String = ""

        Dim RetIndex As Integer = 0

        Dim Report_QryArr() As String = Nothing
        Dim Report_NameArr() As String = Nothing
        Dim Report_TitleArr() As String = Nothing
        Dim Report_FormatArr() As String = Nothing

        Dim SubReport_QryArr() As String = Nothing
        Dim SubReport_NameArr() As String = Nothing
        Dim SubReport_DataSetArr() As DataSet = Nothing

        Dim I As Integer = 0

        Try
            mQry = "Select * from Voucher_Type_Settings  " &
                       "Where V_Type = '" & V_Type & "' " &
                       "And Site_Code = '" & AgL.PubSiteCode & "' " &
                       "And Div_Code  = '" & AgL.PubDivCode & "' "
            DtVTypeSetting = AgL.FillData(mQry, AgL.GcnRead).Tables(0)
            If DtVTypeSetting.Rows.Count <> 0 Then
                If AgL.XNull(DtVTypeSetting.Rows(0)("Query")) <> "" Then
                    Report_QueryList = AgL.XNull(DtVTypeSetting.Rows(0)("Query"))
                    Report_QueryList = Replace(Report_QueryList.ToString.ToUpper, "`", "'")
                    Report_QueryList = Replace(Report_QueryList.ToString.ToUpper, "<SEARCHCODE>", objFrm.mSearchCode)
                End If

                If AgL.XNull(DtVTypeSetting.Rows(0)("Report_Name")) <> "" Then
                    Report_NameList = AgL.XNull(DtVTypeSetting.Rows(0)("Report_Name"))
                End If

                If AgL.XNull(DtVTypeSetting.Rows(0)("Report_Heading")) <> "" Then
                    Report_TitleList = AgL.XNull(DtVTypeSetting.Rows(0)("Report_Heading"))
                End If

                If AgL.XNull(DtVTypeSetting.Rows(0)("Report_Format")) <> "" Then
                    Report_FormatList = AgL.XNull(DtVTypeSetting.Rows(0)("Report_Format"))
                End If

                If AgL.XNull(DtVTypeSetting.Rows(0)("SubReport_QueryList")) <> "" Then
                    SubReport_QueryList = AgL.XNull(DtVTypeSetting.Rows(0)("SubReport_QueryList"))
                    SubReport_QueryList = Replace(SubReport_QueryList.ToString.ToUpper, "`", "'")
                    SubReport_QueryList = Replace(SubReport_QueryList.ToString.ToUpper, "<SEARCHCODE>", objFrm.mSearchCode)
                End If

                If AgL.XNull(DtVTypeSetting.Rows(0)("SubReport_NameList")) <> "" Then
                    SubReport_NameList = AgL.XNull(DtVTypeSetting.Rows(0)("SubReport_NameList"))
                End If
            End If

            If Report_QueryList <> "" Then Report_QryArr = Split(Report_QueryList, "|")
            If Report_TitleList <> "" Then Report_TitleArr = Split(Report_TitleList, "|")
            If Report_NameList <> "" Then Report_NameArr = Split(Report_NameList, "|")

            If Report_FormatList <> "" Then
                Report_FormatArr = Split(Report_FormatList, "|")

                For I = 0 To Report_FormatArr.Length - 1
                    If strQry <> "" Then strQry += " UNION ALL "
                    strQry += " Select " & I & " As Code, '" & Report_FormatArr(I) & "' As Name "
                Next

                Dim FRH_Single As DMHelpGrid.FrmHelpGrid
                FRH_Single = New DMHelpGrid.FrmHelpGrid(New DataView(AgL.FillData(strQry, AgL.GCn).TABLES(0)), "", 300, 350, , , False)
                FRH_Single.FFormatColumn(0, , 0, , False)
                FRH_Single.FFormatColumn(1, "Report Format", 250, DataGridViewContentAlignment.MiddleLeft)
                FRH_Single.StartPosition = FormStartPosition.CenterScreen
                FRH_Single.ShowDialog()

                If FRH_Single.BytBtnValue = 0 Then
                    RetIndex = FRH_Single.DRReturn("Code")
                End If

                If Report_NameArr.Length = Report_FormatArr.Length Then RepName = Report_NameArr(RetIndex) Else RepName = Report_NameArr(0)
                If Report_TitleArr.Length = Report_FormatArr.Length Then RepTitle = Report_TitleArr(RetIndex) Else RepTitle = Report_TitleArr(0)
                If Report_QryArr.Length = Report_FormatArr.Length Then RepQry = Report_QryArr(RetIndex) Else RepQry = Report_QryArr(0)
            Else
                RepName = Report_NameArr(0)
                RepTitle = Report_TitleArr(0)
                RepQry = Report_QryArr(0)
            End If

            AgL.ADMain = New SQLiteDataAdapter(RepQry, AgL.GCn)
            AgL.ADMain.Fill(DsRep)
            AgPL.CreateFieldDefFile1(DsRep, AgL.PubReportPath & "\" & RepName & ".ttx", True)

            If SubReport_QueryList <> "" Then SubReport_QryArr = Split(SubReport_QueryList, "|")
            If SubReport_NameList <> "" Then SubReport_NameArr = Split(SubReport_NameList, "|")

            If SubReport_QryArr IsNot Nothing And SubReport_NameArr IsNot Nothing Then
                If SubReport_QryArr.Length <> SubReport_NameArr.Length Then
                    MsgBox("Number Of SubReport Qries And SubReport Names Are Not Equal.", MsgBoxStyle.Information)
                    Exit Sub
                End If

                For I = 0 To SubReport_QryArr.Length - 1
                    AgL.ADMain = New SqliteDataAdapter(SubReport_QryArr(I).ToString, AgL.GCn)
                    ReDim Preserve SubReport_DataSetArr(I)
                    SubReport_DataSetArr(I) = New DataSet
                    AgL.ADMain.Fill(SubReport_DataSetArr(I))
                    AgPL.CreateFieldDefFile1(SubReport_DataSetArr(I), AgL.PubReportPath & "\" & RepName & (I + 1).ToString & ".ttx", True)
                Next
            End If

            mCrd.Load(AgL.PubReportPath & "\" & RepName & ".rpt")
            mCrd.SetDataSource(DsRep.Tables(0))

            If SubReport_QryArr IsNot Nothing And SubReport_NameArr IsNot Nothing Then
                For I = 0 To SubReport_NameArr.Length - 1
                    Try
                        mCrd.OpenSubreport(SubReport_NameArr(I).ToString).Database.Tables(0).SetDataSource(SubReport_DataSetArr(I).Tables(0))
                    Catch ex As Exception
                    End Try
                Next
            End If

            CType(ReportView.Controls("CrvReport"), CrystalDecisions.Windows.Forms.CrystalReportViewer).ReportSource = mCrd
            AgPL.Formula_Set(mCrd, RepTitle)
            AgPL.Show_Report(ReportView, "* " & RepTitle & " *", objFrm.MdiParent)

            Call AgL.LogTableEntry(objFrm.mSearchCode, objFrm.Text, "P", AgL.PubMachineName, AgL.PubUserName, AgL.PubLoginDate, AgL.GCn, AgL.ECmd)
        Catch Ex As Exception
            MsgBox(Ex.Message)
        End Try
    End Sub



    Public Shared Sub FGetItemRate(ByVal ItemCode As String, ByVal RateType As String, ByVal V_Date As String, _
                                    ByVal Party As String, ByVal Supplier As String, _
                                    ByRef Rate As Double, ByRef RatePerQty As Double, ByRef RatePerMeasure As Double, _
                                    Optional ByRef QuotationDocId As String = "", _
                                    Optional ByRef QuotationNo As String = "", _
                                    Optional ByRef QuotationSr As String = "", _
                                    Optional ByRef Qty As Double = 0)
        Dim mQry$ = ""
        Dim DtTemp As DataTable = Nothing
        Dim DtTempERateLIst As DataTable = Nothing
        Try
            mQry = " SELECT TOP 1 L.Rate, L.DocId As QuotationDocId, H.V_Type || '-' || H.ReferenceNo As QuotationNo, " & _
                    " L.Sr As QuotationSr, L.Qty, L.RatePerQty, L.RatePerMeasure " & _
                    " FROM SaleQuotationDetail L  " & _
                    " LEFT JOIN SaleQuotation H ON L.DocId = H.DocID " & _
                    " WHERE H.SaleToParty = '" & Party & "' AND IfNull(L.Supplier,'') = '" & Supplier & "' " & _
                    " AND L.Item = '" & ItemCode & "'  " & _
                    " AND H.V_Date <= '" & V_Date & "' " & _
                    " ORDER BY H.V_Date DESC "
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
            If DtTemp.Rows.Count > 0 Then
                Rate = AgL.VNull(DtTemp.Rows(0)("Rate"))
                RatePerQty = AgL.VNull(DtTemp.Rows(0)("RatePerQty"))
                RatePerMeasure = AgL.VNull(DtTemp.Rows(0)("RatePerMeasure"))
                QuotationDocId = AgL.XNull(DtTemp.Rows(0)("QuotationDocId"))
                QuotationNo = AgL.XNull(DtTemp.Rows(0)("QuotationNo"))
                QuotationSr = AgL.VNull(DtTemp.Rows(0)("QuotationSr"))
                Qty = AgL.VNull(DtTemp.Rows(0)("Qty"))
            Else
                mQry = " SELECT TOP 1 L.Rate FROM RateListDetail L WHERE L.Item = '" & ItemCode & "'  AND IfNull(L.RateType,'') = '" & RateType & "' And WEF <= '" & V_Date & "'  ORDER BY L.WEF DESC "
                DtTempERateLIst = AgL.FillData(mQry, AgL.GCn).Tables(0)
                If DtTemp.Rows.Count > 0 Then
                    Rate = AgL.VNull(DtTempERateLIst.Rows(0)("Rate"))
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " In FGetItemRate")
        End Try
    End Sub

    Public Shared Function FCheckDuplicatePartyDocNo(ByVal FieldName As String, ByVal TableName As String, ByVal V_Type As String, _
                                      ByVal PartyDocNo As String, ByVal SearchCode As String) As Boolean
        Dim mQry$ = ""
        mQry = " Select Count(*) From " & TableName & " " & _
                " Where " & FieldName & " = '" & PartyDocNo & "' " & _
                " And V_Type = '" & V_Type & "' " & _
                " And DocId <> '" & SearchCode & "'"
        If AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar) > 0 Then
            FCheckDuplicatePartyDocNo = False
            MsgBox("Supplier Doc No Is Duplicate.", MsgBoxStyle.Information)
        Else
            FCheckDuplicatePartyDocNo = True
        End If
    End Function

    Public Shared Sub FReleaseObjects(ByVal obj As Object)
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            obj = Nothing
        Catch ex As Exception
            obj = Nothing
        Finally
            GC.Collect()
        End Try
    End Sub


    Public Shared Function IsScopeOfWorkContains(ScopePart As String) As Boolean
        If AgL.FillData("Select * from Division Where Div_Code='" & AgL.PubDivCode & "' And ScopeOfWork Like '%+" & ScopePart & "%'", AgL.GcnMain).tables(0).Rows.Count = 1 Then
            IsScopeOfWorkContains = True
        End If
    End Function



    Public Shared Function FCheckDuplicatePartyDocNo(ByVal FieldName As String, ByVal TableName As String, ByVal V_Type As String,
                                      ByVal PartyDocNo As String, ByVal SearchCode As String, ByVal FieldParty As String, ByVal Party As String) As Boolean
        Dim mQry$ = ""
        mQry = " Select Count(*) From " & TableName & " " &
                " Where " & FieldName & " = '" & PartyDocNo & "' " &
                " AND " & FieldParty & " = '" & Party & "' " &
                " And V_Type = '" & V_Type & "' " &
                " And DocId <> '" & SearchCode & "'"
        If AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar) > 0 Then
            mQry = "SELECT V_Date FROM  " & TableName & " " &
                    " Where " & FieldName & " = '" & PartyDocNo & "' " &
                    " AND " & FieldParty & " = '" & Party & "' " &
                    " And V_Type = '" & V_Type & "' " &
                    " And DocId <> '" & SearchCode & "'"
            If MsgBox("Supplier Document No. " & PartyDocNo & " Is Already Feeded in Date " & AgL.XNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar) & ". Do You Want To Continue ?", MsgBoxStyle.Information + MsgBoxStyle.YesNo) = MsgBoxResult.No Then
                FCheckDuplicatePartyDocNo = False
            Else
                FCheckDuplicatePartyDocNo = True
            End If
            'MsgBox("Supplier Doc No Is Duplicate.", MsgBoxStyle.Information)
        Else
            FCheckDuplicatePartyDocNo = True
        End If
    End Function


    Public Shared Sub FGetTransactionHistory(ByVal FrmObj As Form, ByVal mSearchCode As String, ByVal mQry As String,
                                             ByVal DGL As AgControls.AgDataGrid, ByVal DtV_TypeSettings As DataTable, ByVal Item As String)
        Dim DtTemp As DataTable = Nothing
        Dim CSV_Qry As String = ""
        Dim CSV_QryArr() As String = Nothing
        Dim I As Integer, J As Integer
        Dim IGridWidth As Integer = 0
        Try
            'If DtV_TypeSettings.Rows.Count <> 0 Then
            '    If AgL.XNull(DtV_TypeSettings.Rows(0)("TransactionHistory_SqlQuery")) <> "" Then
            '        mQry = AgL.XNull(DtV_TypeSettings.Rows(0)("TransactionHistory_SqlQuery"))
            '        mQry = Replace(mQry.ToString.ToUpper, "`<ITEMCODE>`", "'" & Item & "'")
            '        mQry = Replace(mQry.ToString.ToUpper, "`<SEARCHCODE>`", "'" & mSearchCode & "'")
            '    End If

            '    If AgL.XNull(DtV_TypeSettings.Rows(0)("TransactionHistory_ColumnWidthCsv")) <> "" Then
            '        CSV_Qry = AgL.XNull(DtV_TypeSettings.Rows(0)("TransactionHistory_ColumnWidthCsv"))
            '    End If
            'End If

            If CSV_Qry <> "" Then CSV_QryArr = Split(CSV_Qry, ",")
            DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)

            If DtTemp.Rows.Count = 0 Then DGL.DataSource = Nothing : DGL.Visible = False : Exit Sub

            DGL.DataSource = DtTemp
            DGL.Visible = True
            FrmObj.Controls.Add(DGL)
            DGL.Left = FrmObj.Left + 3
            DGL.Top = FrmObj.Bottom - DGL.Height - 130
            DGL.Height = 130
            DGL.Width = 450
            DGL.ColumnHeadersHeight = 40
            DGL.AllowUserToAddRows = False

            If DGL.Columns.Count > 0 Then
                If CSV_Qry <> "" Then J = CSV_QryArr.Length
                For I = 0 To DGL.ColumnCount - 1
                    If CSV_Qry <> "" Then
                        If I < J Then
                            If Val(CSV_QryArr(I)) > 0 Then
                                DGL.Columns(I).Width = Val(CSV_QryArr(I))
                            Else
                                DGL.Columns(I).Width = 100
                            End If
                        Else
                            DGL.Columns(I).Width = 100
                        End If
                    Else
                        DGL.Columns(I).Width = 100
                    End If
                    DGL.Columns(I).SortMode = DataGridViewColumnSortMode.NotSortable
                    IGridWidth += DGL.Columns(I).Width
                Next

                DGL.ScrollBars = ScrollBars.None
                DGL.Width = IGridWidth - 50
                DGL.RowHeadersVisible = False
                DGL.EnableHeadersVisualStyles = False
                DGL.AllowUserToResizeRows = False
                DGL.ReadOnly = True
                DGL.AutoResizeRows()
                DGL.AutoResizeColumnHeadersHeight()
                DGL.BackgroundColor = Color.Cornsilk
                DGL.ColumnHeadersDefaultCellStyle.BackColor = Color.Cornsilk
                DGL.DefaultCellStyle.BackColor = Color.Cornsilk
                DGL.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None
                DGL.CellBorderStyle = DataGridViewCellBorderStyle.None
                DGL.Font = New Font(New FontFamily("Verdana"), 8)
                DGL.ColumnHeadersDefaultCellStyle.Font = New Font(New FontFamily("Verdana"), 8, FontStyle.Bold)
                DGL.BringToFront()
                DGL.Show()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Public Shared Function FGetDimension1Caption() As String
        If AgL.XNull(AgL.PubDtEnviro.Rows(0)("Caption_Dimension1")) = "" Then
            FGetDimension1Caption = "Dimension1Desc"
        Else
            FGetDimension1Caption = AgL.XNull(AgL.PubDtEnviro.Rows(0)("Caption_Dimension1"))
        End If
    End Function

    Public Shared Function FGetDimension2Caption() As String
        If AgL.XNull(AgL.PubDtEnviro.Rows(0)("Caption_Dimension2")) = "" Then
            FGetDimension2Caption = "Dimension2Desc"
        Else
            FGetDimension2Caption = AgL.XNull(AgL.PubDtEnviro.Rows(0)("Caption_Dimension2"))
        End If
    End Function
End Class