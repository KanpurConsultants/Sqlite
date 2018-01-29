Imports System.Data.SqlClient
Imports CrystalDecisions.CrystalReports.Engine

Public Class ClsMain
    Public CFOpen As New ClsFunction
    Public Const ModuleName As String = "Customised"

    Sub New(ByVal AgLibVar As AgLibrary.ClsMain)
        AgL = AgLibVar
        AgPL = New AgLibrary.ClsPrinting(AgL)
        AgIniVar = New AgLibrary.ClsIniVariables(AgL)

        ClsMain_Structure = New AgStructure.ClsMain(AgL)
        ClsMain_Purchase = New Purchase.ClsMain(AgL)
        ClsMain_Store = New Store.ClsMain(AgL)
        ClsMain_Accounts = New AgAccounts.ClsMain(AgL)

        Call IniDtEnviro()
        AgL.PubDivisionList = "('" + AgL.PubDivCode + "')"
    End Sub

    Public Class PaymentMode
        Public Const Cash As String = "Cash"
        Public Const Credit As String = "Credit"
        Public Const Complementary As String = "Complementary"
    End Class

    Public Enum EntryPointType
        Main
        Log
    End Enum

    Public Class SubgroupMasterType
        Public Const Party As String = "Party"
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
    End Class

    Public Class SubGroupType
        Public Const Distributer As String = "Distributer"
    End Class

    Public Class Shape
        Public Const Rectangle As String = "Rectangle"
        Public Const Circle As String = "Circle"
        Public Const Square As String = "Square"
        Public Const Others As String = "Others"
    End Class

    Public Class Temp_NCat
        Public Const DifferentialIncome As String = "DIFFI"
        Public Const SaphireBonus As String = "SAPB"
        Public Const SaphireOverridingBonus As String = "SAPOB"
    End Class

    Public Class MasterType
        Public Const Party As String = "Party"
    End Class


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

    Public Shared Function FGetItemRate(ByVal ItemCode As String, ByVal RateType As String, ByVal V_Date As String)
        Dim mQry$ = ""
        Try
            mQry = " SELECT TOP 1 L.Rate FROM RateListDetail L WHERE L.Item = '" & ItemCode & "'  AND IsNull(L.RateType,'') = '" & RateType & "' And WEF <= '" & V_Date & "'  ORDER BY L.WEF DESC "
            FGetItemRate = AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar)
        Catch ex As Exception
            FGetItemRate = 0
            MsgBox(ex.Message & " In FGetItemRate")
        End Try
    End Function


#Region "Public Help Queries"

    Public Const PubStrHlpQryWashingType As String = "Select 'Normal' as Code, 'Normal' as Description " & _
                                                     " Union All Select 'Antique' as Code, 'Antique' as Description " & _
                                                     " Union All Select 'Herbal' as Code, 'Herbal' as Description " & _
                                                     " Union All Select 'N.A.' as Code, 'N.A.' as Description "


#End Region


#Region " Structure Update Code "

    Public Sub UpdateTableStructure(ByRef MdlTable() As AgLibrary.ClsMain.LITable)
        FBomDetail(MdlTable, "BOMDetail", EntryPointType.Main)
        FBomDetail(MdlTable, "BOMDetail_Log", EntryPointType.Log)

        FSaleInvoice(MdlTable, "SaleInvoice", EntryPointType.Main)
        FSaleInvoice(MdlTable, "SaleInvoice_Log", EntryPointType.Log)

        FPurchInvoice(MdlTable, "PurchInvoice", EntryPointType.Main)
        FPurchInvoice(MdlTable, "PurchInvoice_Log", EntryPointType.Log)

        FPurchInvoiceDetail(MdlTable, "PurchInvoiceDetail", EntryPointType.Main)
        FPurchInvoiceDetail(MdlTable, "PurchInvoiceDetail_Log", EntryPointType.Log)

        FItemType(MdlTable, "ItemType", EntryPointType.Main)

        FItemCategory(MdlTable, "ItemCategory", EntryPointType.Main)
        FItemCategory(MdlTable, "ItemCategory_Log", EntryPointType.Log)

        FItemGroup(MdlTable, "ItemGroup", EntryPointType.Main)
        FItemGroup(MdlTable, "ItemGroup_Log", EntryPointType.Log)

        FItem(MdlTable, "Item", EntryPointType.Main)
        FItem(MdlTable, "Item_Log", EntryPointType.Log)

        FSubGroup(MdlTable, "SubGroup", EntryPointType.Main)
        FSubGroup(MdlTable, "SubGroup_Log", EntryPointType.Log)

        FCurrency(MdlTable, "Currency", EntryPointType.Main)

        FVoucher_Type(MdlTable, "Voucher_Type")

        FEnviro(MdlTable, "Enviro")

        FDuesEnviro(MdlTable, "DuesPaymentEnviro")
    End Sub

    Public Sub UpdateTableInitialiser()
        Try
            Call CreateVType()

            Call TB_PostingGroupSalesTaxItem()

            Call TB_PostingGroupSalesTaxParty()

            Call TB_PostingGroupSalesTax()

            Call TB_Structure()

            Call TB_AcGroup()

            Call TB_SubGroup()

            Call TB_VoucherCat()

            Call TB_ItemType()

            Call TB_Enviro()

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

    Private Sub TB_Structure()
        Dim mQry$ = ""
        Try
            If AgL.Dman_Execute(" Select Count(*) From Structure Where Code = 'PURCH'", AgL.GCn).ExecuteScalar = 0 Then
                mQry = " INSERT INTO dbo.Structure (Code, Description, HeaderTable, LineTable, Div_Code, Site_Code, PreparedBy, U_EntDt, U_AE, ModifiedBy, Edit_Date, UpLoadDate)  " & _
                        " VALUES ('PURCH', 'PURCH', NULL, NULL, 'M', '1', 'sa', '2012-01-15', 'A', NULL, NULL, NULL)  "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                mQry = " INSERT INTO dbo.StructureDetail (Code, Sr, Charges, Charge_Type, Value_Type, Value, Calculation, BaseColumn, PostAc, PostAcFromColumn, DrCr, LineItem, AffectCost, Percentage, Amount, VisibleInMaster, VisibleInMasterLine, VisibleInTransactionLine, VisibleInTransactionFooter, HeaderPerField, HeaderAmtField, LineAmtField, LinePerField, GridDisplayIndex, UpLoadDate, WEF, InactiveDate) " & _
                        " VALUES ('PURCH', 10, 'GAMT', 'Charges', 'FixedValue', NULL, '|AMOUNT|', NULL, NULL, NULL, NULL, 0, 1, 1, 0, 1, 0, 0, 1, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL) " & _
                        " INSERT INTO dbo.StructureDetail (Code, Sr, Charges, Charge_Type, Value_Type, Value, Calculation, BaseColumn, PostAc, PostAcFromColumn, DrCr, LineItem, AffectCost, Percentage, Amount, VisibleInMaster, VisibleInMasterLine, VisibleInTransactionLine, VisibleInTransactionFooter, HeaderPerField, HeaderAmtField, LineAmtField, LinePerField, GridDisplayIndex, UpLoadDate, WEF, InactiveDate) " & _
                        " VALUES ('PURCH', 20, 'DIS', 'Charges', 'Percentage Or Amount', NULL, NULL, 'AMOUNT', NULL, NULL, NULL, 0, 0, 0, 0, 1, 0, 0, 1, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL) " & _
                        " INSERT INTO dbo.StructureDetail (Code, Sr, Charges, Charge_Type, Value_Type, Value, Calculation, BaseColumn, PostAc, PostAcFromColumn, DrCr, LineItem, AffectCost, Percentage, Amount, VisibleInMaster, VisibleInMasterLine, VisibleInTransactionLine, VisibleInTransactionFooter, HeaderPerField, HeaderAmtField, LineAmtField, LinePerField, GridDisplayIndex, UpLoadDate, WEF, InactiveDate) " & _
                        " VALUES ('PURCH', 30, 'OC', 'Charges', 'Percentage Or Amount', NULL, NULL, 'AMOUNT', NULL, NULL, NULL, 0, 1, 0, 0, 1, 0, 0, 1, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL) " & _
                        " INSERT INTO dbo.StructureDetail (Code, Sr, Charges, Charge_Type, Value_Type, Value, Calculation, BaseColumn, PostAc, PostAcFromColumn, DrCr, LineItem, AffectCost, Percentage, Amount, VisibleInMaster, VisibleInMasterLine, VisibleInTransactionLine, VisibleInTransactionFooter, HeaderPerField, HeaderAmtField, LineAmtField, LinePerField, GridDisplayIndex, UpLoadDate, WEF, InactiveDate) " & _
                        " VALUES ('PURCH', 40, 'NAMT', 'Charges', 'FixedValue', NULL, '{GAMT}-{DIS}+{OC}', NULL, NULL, NULL, NULL, 0, NULL, 1, 0, 0, 0, 0, 1, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL) " & _
                        " INSERT INTO dbo.StructureDetail (Code, Sr, Charges, Charge_Type, Value_Type, Value, Calculation, BaseColumn, PostAc, PostAcFromColumn, DrCr, LineItem, AffectCost, Percentage, Amount, VisibleInMaster, VisibleInMasterLine, VisibleInTransactionLine, VisibleInTransactionFooter, HeaderPerField, HeaderAmtField, LineAmtField, LinePerField, GridDisplayIndex, UpLoadDate, WEF, InactiveDate) " & _
                        " VALUES ('PURCH', 50, 'LV', 'Cost', 'FixedValue', NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, 1, 0, 0, 0, 0, 0, NULL, NULL, NULL, NULL, 0, NULL, NULL, NULL) "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If

            If AgL.Dman_Execute(" Select Count(*) From Structure Where Code = 'SALE'", AgL.GCn).ExecuteScalar = 0 Then
                mQry = " INSERT INTO dbo.Structure (Code, Description, HeaderTable, LineTable, Div_Code, Site_Code, PreparedBy, U_EntDt, U_AE, ModifiedBy, Edit_Date, UpLoadDate)  " & _
                        " VALUES ('SALE', 'SALE', NULL, NULL, 'M', '1', 'sa', '2002-01-01', 'A', NULL, NULL, NULL)  "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

                mQry = " INSERT INTO dbo.StructureDetail (Code, Sr, Charges, Charge_Type, Value_Type, Value, Calculation, BaseColumn, PostAc, PostAcFromColumn, DrCr, LineItem, AffectCost, Percentage, Amount, VisibleInMaster, VisibleInMasterLine, VisibleInTransactionLine, VisibleInTransactionFooter, HeaderPerField, HeaderAmtField, LineAmtField, LinePerField, GridDisplayIndex, UpLoadDate, WEF, InactiveDate) " & _
                            " VALUES ('SALE', 10, 'GAMT', 'Charges', 'FixedValue', NULL, '|AMOUNT|', NULL, NULL, NULL, NULL, 0, 1, NULL, 0, 1, 0, 0, 1, NULL, 'Gross_Amount', 'Gross_Amount', NULL, 0, NULL, '2012-04-01', NULL) " & _
                            " INSERT INTO dbo.StructureDetail (Code, Sr, Charges, Charge_Type, Value_Type, Value, Calculation, BaseColumn, PostAc, PostAcFromColumn, DrCr, LineItem, AffectCost, Percentage, Amount, VisibleInMaster, VisibleInMasterLine, VisibleInTransactionLine, VisibleInTransactionFooter, HeaderPerField, HeaderAmtField, LineAmtField, LinePerField, GridDisplayIndex, UpLoadDate, WEF, InactiveDate) " & _
                            " VALUES ('SALE', 12, 'DPTAX', 'Charges', 'Percentage Or Amount', NULL, '{GAMT}*{DPTAX}/100', 'AMOUNT', NULL, NULL, NULL, 0, 0, NULL, 0, 1, 0, 0, 1, 'Discount_Pre_Tax_Per', 'Discount_Pre_Tax', 'Discount_Pre_Tax', 'Discount_Pre_Tax_Per', 0, NULL, '2012-04-01', NULL) " & _
                            " INSERT INTO dbo.StructureDetail (Code, Sr, Charges, Charge_Type, Value_Type, Value, Calculation, BaseColumn, PostAc, PostAcFromColumn, DrCr, LineItem, AffectCost, Percentage, Amount, VisibleInMaster, VisibleInMasterLine, VisibleInTransactionLine, VisibleInTransactionFooter, HeaderPerField, HeaderAmtField, LineAmtField, LinePerField, GridDisplayIndex, UpLoadDate, WEF, InactiveDate) " & _
                            " VALUES ('SALE', 14, 'OAPTAX', 'Charges', 'Percentage Or Amount', NULL, '{GAMT}*{OAPTAX}/100', 'AMOUNT', NULL, NULL, NULL, 0, 1, NULL, 0, 1, 0, 0, 1, 'Other_Additions_Pre_Tax_Per', 'Other_Additions_Pre_Tax', 'Other_Additions_Pre_Tax', 'Other_Additions_Pre_Tax_Per', 0, NULL, '2012-04-01', NULL) " & _
                            " INSERT INTO dbo.StructureDetail (Code, Sr, Charges, Charge_Type, Value_Type, Value, Calculation, BaseColumn, PostAc, PostAcFromColumn, DrCr, LineItem, AffectCost, Percentage, Amount, VisibleInMaster, VisibleInMasterLine, VisibleInTransactionLine, VisibleInTransactionFooter, HeaderPerField, HeaderAmtField, LineAmtField, LinePerField, GridDisplayIndex, UpLoadDate, WEF, InactiveDate) " & _
                            " VALUES ('SALE', 16, 'STTA', 'Charges', 'FixedValue', NULL, '{GAMT}-{DPTAX}+{OAPTAX}', NULL, NULL, NULL, NULL, 0, NULL, NULL, 0, 1, 0, 0, 1, NULL, 'Sales_Tax_Taxable_Amt', 'Sales_Tax_Taxable_Amt', NULL, 0, NULL, '2012-04-01', NULL) " & _
                            " INSERT INTO dbo.StructureDetail (Code, Sr, Charges, Charge_Type, Value_Type, Value, Calculation, BaseColumn, PostAc, PostAcFromColumn, DrCr, LineItem, AffectCost, Percentage, Amount, VisibleInMaster, VisibleInMasterLine, VisibleInTransactionLine, VisibleInTransactionFooter, HeaderPerField, HeaderAmtField, LineAmtField, LinePerField, GridDisplayIndex, UpLoadDate, WEF, InactiveDate) " & _
                            " VALUES ('SALE', 18, 'VAT', 'VAT', 'Percentage', NULL, '{STTA}*{VAT}/100', NULL, NULL, NULL, NULL, 0, NULL, NULL, 0, 1, 0, 1, 1, 'Vat_Per', 'Vat', 'Vat', 'Vat_Per', 0, NULL, '2012-04-01', NULL) " & _
                            " INSERT INTO dbo.StructureDetail (Code, Sr, Charges, Charge_Type, Value_Type, Value, Calculation, BaseColumn, PostAc, PostAcFromColumn, DrCr, LineItem, AffectCost, Percentage, Amount, VisibleInMaster, VisibleInMasterLine, VisibleInTransactionLine, VisibleInTransactionFooter, HeaderPerField, HeaderAmtField, LineAmtField, LinePerField, GridDisplayIndex, UpLoadDate, WEF, InactiveDate) " & _
                            " VALUES ('SALE', 19, 'SAT', 'SAT', 'Percentage', NULL, '{STTA}*{SAT}/100', NULL, NULL, NULL, NULL, 0, NULL, NULL, 0, 1, 0, 1, 1, 'Sat_Per', 'Sat', 'Sat', 'Sat_Per', 0, NULL, '2012-04-01', NULL) " & _
                            " INSERT INTO dbo.StructureDetail (Code, Sr, Charges, Charge_Type, Value_Type, Value, Calculation, BaseColumn, PostAc, PostAcFromColumn, DrCr, LineItem, AffectCost, Percentage, Amount, VisibleInMaster, VisibleInMasterLine, VisibleInTransactionLine, VisibleInTransactionFooter, HeaderPerField, HeaderAmtField, LineAmtField, LinePerField, GridDisplayIndex, UpLoadDate, WEF, InactiveDate) " & _
                            " VALUES ('SALE', 20, 'DIS', 'Charges', 'Percentage Or Amount', NULL, '({STTA}+{VAT}+{SAT}) *{DIS}/100', 'AMOUNT', NULL, NULL, NULL, 0, 0, NULL, 0, 1, 0, 0, 1, 'Discount_Per', 'Discount', 'Discount', 'Discount_Per', 0, NULL, '2012-04-01', NULL) " & _
                            " INSERT INTO dbo.StructureDetail (Code, Sr, Charges, Charge_Type, Value_Type, Value, Calculation, BaseColumn, PostAc, PostAcFromColumn, DrCr, LineItem, AffectCost, Percentage, Amount, VisibleInMaster, VisibleInMasterLine, VisibleInTransactionLine, VisibleInTransactionFooter, HeaderPerField, HeaderAmtField, LineAmtField, LinePerField, GridDisplayIndex, UpLoadDate, WEF, InactiveDate) " & _
                            " VALUES ('SALE', 30, 'OC', 'Charges', 'Percentage Or Amount', NULL, '({STTA}+{VAT}+{SAT}) *{OC}/100', 'AMOUNT', NULL, NULL, NULL, 0, 1, NULL, 0, 1, 0, 0, 1, 'Other_Charges_Per', 'Other_Charges', 'Other_Charges', 'Other_Charges_Per', 0, NULL, '2012-04-01', NULL) " & _
                            " INSERT INTO dbo.StructureDetail (Code, Sr, Charges, Charge_Type, Value_Type, Value, Calculation, BaseColumn, PostAc, PostAcFromColumn, DrCr, LineItem, AffectCost, Percentage, Amount, VisibleInMaster, VisibleInMasterLine, VisibleInTransactionLine, VisibleInTransactionFooter, HeaderPerField, HeaderAmtField, LineAmtField, LinePerField, GridDisplayIndex, UpLoadDate, WEF, InactiveDate) " & _
                            " VALUES ('SALE', 35, 'RO', 'Charges', 'FixedValue', NULL, '({STTA}+{VAT}+{SAT}-{DIS}+{OC}) -ROUND({STTA}+{VAT}+{SAT}-{DIS}+{OC},0)', NULL, NULL, NULL, NULL, 0, NULL, NULL, 0, 1, 0, 0, 1, NULL, 'Round_Off', 'Round_Off', NULL, 0, NULL, '2012-04-01', NULL) " & _
                            " INSERT INTO dbo.StructureDetail (Code, Sr, Charges, Charge_Type, Value_Type, Value, Calculation, BaseColumn, PostAc, PostAcFromColumn, DrCr, LineItem, AffectCost, Percentage, Amount, VisibleInMaster, VisibleInMasterLine, VisibleInTransactionLine, VisibleInTransactionFooter, HeaderPerField, HeaderAmtField, LineAmtField, LinePerField, GridDisplayIndex, UpLoadDate, WEF, InactiveDate) " & _
                            " VALUES ('SALE', 40, 'NAMT', 'Charges', 'FixedValue', NULL, '{STTA}+{VAT}+{SAT}-{DIS}+{OC}+{RO}', NULL, NULL, NULL, NULL, 0, NULL, NULL, 0, 1, 0, 0, 1, NULL, 'Net_Amount', 'Net_Amount', NULL, 0, NULL, '2012-04-01', NULL) " & _
                            " INSERT INTO dbo.StructureDetail (Code, Sr, Charges, Charge_Type, Value_Type, Value, Calculation, BaseColumn, PostAc, PostAcFromColumn, DrCr, LineItem, AffectCost, Percentage, Amount, VisibleInMaster, VisibleInMasterLine, VisibleInTransactionLine, VisibleInTransactionFooter, HeaderPerField, HeaderAmtField, LineAmtField, LinePerField, GridDisplayIndex, UpLoadDate, WEF, InactiveDate) " & _
                            " VALUES ('SALE', 50, 'LV', 'Cost', 'FixedValue', NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, NULL, 0, 1, 0, 0, 0, NULL, 'Landed_Value', 'Landed_Value', NULL, 0, NULL, '2012-04-01', NULL) "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If
        Catch ex As Exception
            MsgBox(ex.Message + ".On TB_Structure")
        End Try
    End Sub

    Private Sub TB_AcGroup()
        Dim mQry$ = ""
        Try
            If AgL.Dman_Execute(" Select Count(*) From AcGroup ", AgL.GCn).ExecuteScalar = 0 Then
                mQry = " INSERT INTO dbo.AcGroup (GroupCode, SNo, GroupName, GroupUnder, Nature, SysGroup, GroupNature, ContraGroupName, U_Name, U_EntDt, U_AE, TradingYn, MainGrCode, BlOrd, MainGrLen, ID, Site_Code, GroupNameBiLang, GroupLevel, CurrentCount, CurrentBalance, SubLedYn, AliasYn, GroupHelp, LastYearBalance, UpLoadDate)" & _
                            " VALUES ('0001', NULL, 'Capital Account', NULL, 'Others', 'Y', 'L', 'Capital Account', 'SA', '2011-04-09', 'E',  NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL) " & _
                            " INSERT INTO dbo.AcGroup (GroupCode, SNo, GroupName, GroupUnder, Nature, SysGroup, GroupNature, ContraGroupName, U_Name, U_EntDt, U_AE, TradingYn, MainGrCode, BlOrd, MainGrLen, ID, Site_Code, GroupNameBiLang, GroupLevel, CurrentCount, CurrentBalance, SubLedYn, AliasYn, GroupHelp, LastYearBalance, UpLoadDate) " & _
                            " VALUES ('0002', NULL, 'Loan (Liability)', NULL, 'Others', 'Y', 'L', 'Loan (Liability)', 'SA', '2011-04-09', 'E',  NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL) " & _
                            " INSERT INTO dbo.AcGroup (GroupCode, SNo, GroupName, GroupUnder, Nature, SysGroup, GroupNature, ContraGroupName, U_Name, U_EntDt, U_AE, TradingYn, MainGrCode, BlOrd, MainGrLen, ID, Site_Code, GroupNameBiLang, GroupLevel, CurrentCount, CurrentBalance, SubLedYn, AliasYn, GroupHelp, LastYearBalance, UpLoadDate) " & _
                            " VALUES ('0003', NULL, 'Current Liabilities', NULL, 'Others', 'Y', 'L', 'Current Liabilities', 'SA', '2011-04-09', 'E',  NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL) " & _
                            " INSERT INTO dbo.AcGroup (GroupCode, SNo, GroupName, GroupUnder, Nature, SysGroup, GroupNature, ContraGroupName, U_Name, U_EntDt, U_AE, TradingYn, MainGrCode, BlOrd, MainGrLen, ID, Site_Code, GroupNameBiLang, GroupLevel, CurrentCount, CurrentBalance, SubLedYn, AliasYn, GroupHelp, LastYearBalance, UpLoadDate) " & _
                            " VALUES ('0004', NULL, 'Fixed Assets', NULL, 'Others', 'Y', 'A', 'Fixed Assets', 'SA', '2011-04-09', 'E',  NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL) " & _
                            " INSERT INTO dbo.AcGroup (GroupCode, SNo, GroupName, GroupUnder, Nature, SysGroup, GroupNature, ContraGroupName, U_Name, U_EntDt, U_AE, TradingYn, MainGrCode, BlOrd, MainGrLen, ID, Site_Code, GroupNameBiLang, GroupLevel, CurrentCount, CurrentBalance, SubLedYn, AliasYn, GroupHelp, LastYearBalance, UpLoadDate) " & _
                            " VALUES ('0005', NULL, 'Investments', NULL, 'Others', 'Y', 'A', 'Investments', 'SA', '2011-04-09', 'E',  NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL) " & _
                            " INSERT INTO dbo.AcGroup (GroupCode, SNo, GroupName, GroupUnder, Nature, SysGroup, GroupNature, ContraGroupName, U_Name, U_EntDt, U_AE, TradingYn, MainGrCode, BlOrd, MainGrLen, ID, Site_Code, GroupNameBiLang, GroupLevel, CurrentCount, CurrentBalance, SubLedYn, AliasYn, GroupHelp, LastYearBalance, UpLoadDate) " & _
                            " VALUES ('0006', NULL, 'Current Assets', NULL, 'Others', 'Y', 'A', 'Current Assets', 'SA', '2011-04-09', 'E',  NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL) " & _
                            " INSERT INTO dbo.AcGroup (GroupCode, SNo, GroupName, GroupUnder, Nature, SysGroup, GroupNature, ContraGroupName, U_Name, U_EntDt, U_AE, TradingYn, MainGrCode, BlOrd, MainGrLen, ID, Site_Code, GroupNameBiLang, GroupLevel, CurrentCount, CurrentBalance, SubLedYn, AliasYn, GroupHelp, LastYearBalance, UpLoadDate) " & _
                            " VALUES ('0007', NULL, 'Branch/Divisions', NULL, 'Others', 'Y', 'A', 'Branch/Divisions', 'SA', '2011-04-09', 'E',  NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL) " & _
                            " INSERT INTO dbo.AcGroup (GroupCode, SNo, GroupName, GroupUnder, Nature, SysGroup, GroupNature, ContraGroupName, U_Name, U_EntDt, U_AE, TradingYn, MainGrCode, BlOrd, MainGrLen, ID, Site_Code, GroupNameBiLang, GroupLevel, CurrentCount, CurrentBalance, SubLedYn, AliasYn, GroupHelp, LastYearBalance, UpLoadDate) " & _
                            " VALUES ('0008', NULL, 'Misc. Expences (Asset)', NULL, 'Expenses', 'Y', 'A', 'Misc. Expences (Asset)', 'SA', '2011-04-09', 'E',  NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL) " & _
                            " INSERT INTO dbo.AcGroup (GroupCode, SNo, GroupName, GroupUnder, Nature, SysGroup, GroupNature, ContraGroupName, U_Name, U_EntDt, U_AE, TradingYn, MainGrCode, BlOrd, MainGrLen, ID, Site_Code, GroupNameBiLang, GroupLevel, CurrentCount, CurrentBalance, SubLedYn, AliasYn, GroupHelp, LastYearBalance, UpLoadDate) " & _
                            " VALUES ('0009', NULL, 'Suspense A/c', NULL, 'Others', 'Y', 'A', 'Suspense A/c', 'SA', '2011-04-09', 'E',  NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL) " & _
                            " INSERT INTO dbo.AcGroup (GroupCode, SNo, GroupName, GroupUnder, Nature, SysGroup, GroupNature, ContraGroupName, U_Name, U_EntDt, U_AE, TradingYn, MainGrCode, BlOrd, MainGrLen, ID, Site_Code, GroupNameBiLang, GroupLevel, CurrentCount, CurrentBalance, SubLedYn, AliasYn, GroupHelp, LastYearBalance, UpLoadDate) " & _
                            " VALUES ('0010', NULL, 'Reserves & Surplus', '0001', 'Others', 'Y', 'L', 'Reserves & Surplus', 'SA', '2011-04-09', 'E',  NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL) " & _
                            " INSERT INTO dbo.AcGroup (GroupCode, SNo, GroupName, GroupUnder, Nature, SysGroup, GroupNature, ContraGroupName, U_Name, U_EntDt, U_AE, TradingYn, MainGrCode, BlOrd, MainGrLen, ID, Site_Code, GroupNameBiLang, GroupLevel, CurrentCount, CurrentBalance, SubLedYn, AliasYn, GroupHelp, LastYearBalance, UpLoadDate) " & _
                            " VALUES ('0011', NULL, 'Bank OD A/c', '0002', 'Bank', 'Y', 'L', 'Bank OD A/c', 'SA', '2011-04-09', 'E',  NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL) " & _
                            " INSERT INTO dbo.AcGroup (GroupCode, SNo, GroupName, GroupUnder, Nature, SysGroup, GroupNature, ContraGroupName, U_Name, U_EntDt, U_AE, TradingYn, MainGrCode, BlOrd, MainGrLen, ID, Site_Code, GroupNameBiLang, GroupLevel, CurrentCount, CurrentBalance, SubLedYn, AliasYn, GroupHelp, LastYearBalance, UpLoadDate) " & _
                            " VALUES ('0012', NULL, 'Secured Loans', NULL, 'Others', 'Y', 'L', 'Secured Loans', 'SA', '2011-04-09', 'E',  NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL) " & _
                            " INSERT INTO dbo.AcGroup (GroupCode, SNo, GroupName, GroupUnder, Nature, SysGroup, GroupNature, ContraGroupName, U_Name, U_EntDt, U_AE, TradingYn, MainGrCode, BlOrd, MainGrLen, ID, Site_Code, GroupNameBiLang, GroupLevel, CurrentCount, CurrentBalance, SubLedYn, AliasYn, GroupHelp, LastYearBalance, UpLoadDate) " & _
                            " VALUES ('0013', NULL, 'Unsecured Loans', '0002', 'Others', 'Y', 'L', 'Unsecured Loans', 'SA', '2011-04-09', 'E',  NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL) " & _
                            " INSERT INTO dbo.AcGroup (GroupCode, SNo, GroupName, GroupUnder, Nature, SysGroup, GroupNature, ContraGroupName, U_Name, U_EntDt, U_AE, TradingYn, MainGrCode, BlOrd, MainGrLen, ID, Site_Code, GroupNameBiLang, GroupLevel, CurrentCount, CurrentBalance, SubLedYn, AliasYn, GroupHelp, LastYearBalance, UpLoadDate) " & _
                            " VALUES ('0014', NULL, 'Duties & Taxes', '0003', 'Expenses', 'Y', 'L', 'Duties & Taxes', 'SA', '2011-04-09', 'E',  NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL) " & _
                            " INSERT INTO dbo.AcGroup (GroupCode, SNo, GroupName, GroupUnder, Nature, SysGroup, GroupNature, ContraGroupName, U_Name, U_EntDt, U_AE, TradingYn, MainGrCode, BlOrd, MainGrLen, ID, Site_Code, GroupNameBiLang, GroupLevel, CurrentCount, CurrentBalance, SubLedYn, AliasYn, GroupHelp, LastYearBalance, UpLoadDate) " & _
                            " VALUES ('0015', NULL, 'Provisions', '0003', 'Expenses', 'Y', 'L', 'Provisions', 'SA', '2011-04-09', 'E',  NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL) " & _
                            " INSERT INTO dbo.AcGroup (GroupCode, SNo, GroupName, GroupUnder, Nature, SysGroup, GroupNature, ContraGroupName, U_Name, U_EntDt, U_AE, TradingYn, MainGrCode, BlOrd, MainGrLen, ID, Site_Code, GroupNameBiLang, GroupLevel, CurrentCount, CurrentBalance, SubLedYn, AliasYn, GroupHelp, LastYearBalance, UpLoadDate) " & _
                            " VALUES ('0016', NULL, 'Sundry Creditors', '0003', 'Supplier', 'Y', 'L', 'Sundry Creditors', 'SA', '2011-04-09', 'E',  NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL) " & _
                            " INSERT INTO dbo.AcGroup (GroupCode, SNo, GroupName, GroupUnder, Nature, SysGroup, GroupNature, ContraGroupName, U_Name, U_EntDt, U_AE, TradingYn, MainGrCode, BlOrd, MainGrLen, ID, Site_Code, GroupNameBiLang, GroupLevel, CurrentCount, CurrentBalance, SubLedYn, AliasYn, GroupHelp, LastYearBalance, UpLoadDate) " & _
                            " VALUES ('0017', NULL, 'Opening Stock', NULL, 'Direct', 'Y', 'E', 'Opening Stock', 'SA', '2011-04-09', 'E',  NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL) " & _
                            " INSERT INTO dbo.AcGroup (GroupCode, SNo, GroupName, GroupUnder, Nature, SysGroup, GroupNature, ContraGroupName, U_Name, U_EntDt, U_AE, TradingYn, MainGrCode, BlOrd, MainGrLen, ID, Site_Code, GroupNameBiLang, GroupLevel, CurrentCount, CurrentBalance, SubLedYn, AliasYn, GroupHelp, LastYearBalance, UpLoadDate) " & _
                            " VALUES ('0018', NULL, 'Deposits (Asset)', '0006', 'Others', 'Y', 'A', 'Deposits (Asset)', 'SA', '2011-04-09', 'E',  NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL) " & _
                            " INSERT INTO dbo.AcGroup (GroupCode, SNo, GroupName, GroupUnder, Nature, SysGroup, GroupNature, ContraGroupName, U_Name, U_EntDt, U_AE, TradingYn, MainGrCode, BlOrd, MainGrLen, ID, Site_Code, GroupNameBiLang, GroupLevel, CurrentCount, CurrentBalance, SubLedYn, AliasYn, GroupHelp, LastYearBalance, UpLoadDate) " & _
                            " VALUES ('0019', NULL, 'Loans & Advances (Asset)', '0006', 'Others', 'Y', 'A', 'Loans & Advances (Asset)', 'SA', '2011-04-09', 'E',  NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL) " & _
                            " INSERT INTO dbo.AcGroup (GroupCode, SNo, GroupName, GroupUnder, Nature, SysGroup, GroupNature, ContraGroupName, U_Name, U_EntDt, U_AE, TradingYn, MainGrCode, BlOrd, MainGrLen, ID, Site_Code, GroupNameBiLang, GroupLevel, CurrentCount, CurrentBalance, SubLedYn, AliasYn, GroupHelp, LastYearBalance, UpLoadDate) " & _
                            " VALUES ('0020', NULL, 'Sundry Debtors', '0006', 'Customer', 'Y', 'A', 'Sundry Debtors', 'SA', '2011-04-09', 'E',  NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL) " & _
                            " INSERT INTO dbo.AcGroup (GroupCode, SNo, GroupName, GroupUnder, Nature, SysGroup, GroupNature, ContraGroupName, U_Name, U_EntDt, U_AE, TradingYn, MainGrCode, BlOrd, MainGrLen, ID, Site_Code, GroupNameBiLang, GroupLevel, CurrentCount, CurrentBalance, SubLedYn, AliasYn, GroupHelp, LastYearBalance, UpLoadDate) " & _
                            " VALUES ('0021', NULL, 'Cash-in-Hand', '0006', 'Cash', 'Y', 'A', 'Cash-In-Hand', 'SA', '2011-04-09', 'E',  NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL) " & _
                            " INSERT INTO dbo.AcGroup (GroupCode, SNo, GroupName, GroupUnder, Nature, SysGroup, GroupNature, ContraGroupName, U_Name, U_EntDt, U_AE, TradingYn, MainGrCode, BlOrd, MainGrLen, ID, Site_Code, GroupNameBiLang, GroupLevel, CurrentCount, CurrentBalance, SubLedYn, AliasYn, GroupHelp, LastYearBalance, UpLoadDate) " & _
                            " VALUES ('0022', NULL, 'Bank Accounts', '0006', 'Bank', 'Y', 'A', 'Bank Accounts', 'SA', '2011-04-09', 'E',  NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL) " & _
                            " INSERT INTO dbo.AcGroup (GroupCode, SNo, GroupName, GroupUnder, Nature, SysGroup, GroupNature, ContraGroupName, U_Name, U_EntDt, U_AE, TradingYn, MainGrCode, BlOrd, MainGrLen, ID, Site_Code, GroupNameBiLang, GroupLevel, CurrentCount, CurrentBalance, SubLedYn, AliasYn, GroupHelp, LastYearBalance, UpLoadDate) " & _
                            " VALUES ('0023', NULL, 'Sales Accounts', NULL, 'Sales', 'Y', 'R', 'Sales Accounts', 'DEENA', '2011-07-13', 'E',  NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL) " & _
                            " INSERT INTO dbo.AcGroup (GroupCode, SNo, GroupName, GroupUnder, Nature, SysGroup, GroupNature, ContraGroupName, U_Name, U_EntDt, U_AE, TradingYn, MainGrCode, BlOrd, MainGrLen, ID, Site_Code, GroupNameBiLang, GroupLevel, CurrentCount, CurrentBalance, SubLedYn, AliasYn, GroupHelp, LastYearBalance, UpLoadDate) " & _
                            " VALUES ('0024', NULL, 'Purchase Accounts', NULL, 'Purchase', 'Y', 'E', 'Purchase Accounts', 'SA', '2011-04-09', 'E',  NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL) " & _
                            " INSERT INTO dbo.AcGroup (GroupCode, SNo, GroupName, GroupUnder, Nature, SysGroup, GroupNature, ContraGroupName, U_Name, U_EntDt, U_AE, TradingYn, MainGrCode, BlOrd, MainGrLen, ID, Site_Code, GroupNameBiLang, GroupLevel, CurrentCount, CurrentBalance, SubLedYn, AliasYn, GroupHelp, LastYearBalance, UpLoadDate) " & _
                            " VALUES ('0025', NULL, 'Direct Incomes', NULL, 'Direct', 'Y', 'R', 'Direct Incomes', 'SA', '2011-04-09', 'E',  NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL) " & _
                            " INSERT INTO dbo.AcGroup (GroupCode, SNo, GroupName, GroupUnder, Nature, SysGroup, GroupNature, ContraGroupName, U_Name, U_EntDt, U_AE, TradingYn, MainGrCode, BlOrd, MainGrLen, ID, Site_Code, GroupNameBiLang, GroupLevel, CurrentCount, CurrentBalance, SubLedYn, AliasYn, GroupHelp, LastYearBalance, UpLoadDate) " & _
                            " VALUES ('0026', NULL, 'Direct Expenses', NULL, 'Direct', 'Y', 'E', 'Direct Expenses', 'SA', '2011-04-09', 'E',  NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL) " & _
                            " INSERT INTO dbo.AcGroup (GroupCode, SNo, GroupName, GroupUnder, Nature, SysGroup, GroupNature, ContraGroupName, U_Name, U_EntDt, U_AE, TradingYn, MainGrCode, BlOrd, MainGrLen, ID, Site_Code, GroupNameBiLang, GroupLevel, CurrentCount, CurrentBalance, SubLedYn, AliasYn, GroupHelp, LastYearBalance, UpLoadDate) " & _
                            " VALUES ('0027', NULL, 'Indirect Incomes', NULL, 'Indirect', 'Y', 'R', 'Indirect Incomes', 'SA', '2011-04-09', 'E',  NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL) " & _
                            " INSERT INTO dbo.AcGroup (GroupCode, SNo, GroupName, GroupUnder, Nature, SysGroup, GroupNature, ContraGroupName, U_Name, U_EntDt, U_AE, TradingYn, MainGrCode, BlOrd, MainGrLen, ID, Site_Code, GroupNameBiLang, GroupLevel, CurrentCount, CurrentBalance, SubLedYn, AliasYn, GroupHelp, LastYearBalance, UpLoadDate) " & _
                            " VALUES ('0028', NULL, 'Indirect Expenses', NULL, 'Indirect', 'Y', 'E', 'Indirect Expenses', 'SA', '2011-04-09', 'E',  NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL) " & _
                            " INSERT INTO dbo.AcGroup (GroupCode, SNo, GroupName, GroupUnder, Nature, SysGroup, GroupNature, ContraGroupName, U_Name, U_EntDt, U_AE, TradingYn, MainGrCode, BlOrd, MainGrLen, ID, Site_Code, GroupNameBiLang, GroupLevel, CurrentCount, CurrentBalance, SubLedYn, AliasYn, GroupHelp, LastYearBalance, UpLoadDate) " & _
                            " VALUES ('0029', NULL, 'Profit & Loss A/c', NULL, 'Others', 'Y', 'L', 'Profit & Loss A/c', 'SA', '2011-04-09', 'E',  NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL) " & _
                            " INSERT INTO dbo.AcGroup (GroupCode, SNo, GroupName, GroupUnder, Nature, SysGroup, GroupNature, ContraGroupName, U_Name, U_EntDt, U_AE, TradingYn, MainGrCode, BlOrd, MainGrLen, ID, Site_Code, GroupNameBiLang, GroupLevel, CurrentCount, CurrentBalance, SubLedYn, AliasYn, GroupHelp, LastYearBalance, UpLoadDate) " & _
                            " VALUES ('0030', NULL, 'Closing Stock', NULL, 'Direct', 'Y', 'R', 'Closing Stock', 'SA', '2011-04-09', 'E',  NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)  "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If
        Catch ex As Exception
            MsgBox(ex.Message + ".On TB_Enviro")
        End Try
    End Sub


    Private Sub TB_ItemType()
        Dim mQry$ = ""
        Try
            If AgL.Dman_Execute(" Select Count(*) From ItemType ", AgL.GCn).ExecuteScalar = 0 Then
                mQry = " INSERT INTO dbo.ItemType (Code, Name) VALUES ('CL', 'Coal') " & _
                        " INSERT INTO dbo.ItemType (Code, Name) VALUES ('CM', 'Chemical') " & _
                        " INSERT INTO dbo.ItemType (Code, Name) VALUES ('FL', 'Fuel') " & _
                        " INSERT INTO dbo.ItemType (Code, Name) VALUES ('FM', 'Finished Mtrl.') " & _
                        " INSERT INTO dbo.ItemType (Code, Name) VALUES ('OT', 'Others') " & _
                        " INSERT INTO dbo.ItemType (Code, Name) VALUES ('PM', 'Packing Mtrl.') " & _
                        " INSERT INTO dbo.ItemType (Code, Name) VALUES ('RM', 'Raw Mtrl.') " & _
                        " INSERT INTO dbo.ItemType (Code, Name) VALUES ('SF', 'Semi Finished') " & _
                        " INSERT INTO dbo.ItemType (Code, Name) VALUES ('SM', 'Store Mtrl.')"
            End If



        Catch ex As Exception

        End Try
    End Sub


    Private Sub TB_SubGroup()
        Dim mQry$ = ""
        Try
            If AgL.Dman_Execute(" Select Count(*) From SubGroup Where SubCode = 'Cash' ", AgL.GCn).ExecuteScalar = 0 Then
                mQry = " INSERT INTO dbo.SubGroup (SubCode, SiteList, DispName, Name, GroupCode, GroupNature, ManualCode, Nature) " & _
                        " VALUES ('CASH', '|1|', 'CASH A/C', 'CASH A/C', '0021', '', 'CASH', 'CASH')"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If

            If AgL.Dman_Execute(" Select Count(*) From SubGroup Where SubCode = 'SALE' ", AgL.GCn).ExecuteScalar = 0 Then
                mQry = " INSERT INTO dbo.SubGroup (SubCode, SiteList, DispName, Name, GroupCode, GroupNature, ManualCode, Nature) " & _
                        " VALUES ('SALE', '|1|', 'SALE A/C', 'SALE A/C', '0023', '', 'SALE', 'Customer')"
                AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
            End If
        Catch ex As Exception
            MsgBox(ex.Message + ".On TB_Enviro")
        End Try
    End Sub

    Private Sub TB_VoucherCat()
        Dim mQry$ = ""
        Try
            mQry = " UPDATE VoucherCat " & _
                    " SET Structure = 'SALE',  " & _
                    " HeaderTable = (SELECT object_id FROM sys.Objects WHERE name = 'SaleInvoice'), " & _
                    " LineTable = (SELECT object_id FROM sys.Objects WHERE name = 'SaleInvoiceDetail') " & _
                    " WHERE NCat = 'SI' And Structure Is Null  "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)


            mQry = " UPDATE VoucherCat " & _
                    " SET Structure = 'SALE',  " & _
                    " HeaderTable = (SELECT object_id FROM sys.Objects WHERE name = 'SaleInvoice'), " & _
                    " LineTable = (SELECT object_id FROM sys.Objects WHERE name = 'SaleInvoiceDetail') " & _
                    " WHERE NCat = 'SRET'  And Structure Is Null  "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

            mQry = " UPDATE VoucherCat " & _
                    " SET Structure = 'PURCH',  " & _
                    " HeaderTable = (SELECT object_id FROM sys.Objects WHERE name = 'PurchInvoice'), " & _
                    " LineTable = (SELECT object_id FROM sys.Objects WHERE name = 'PurchInvoiceDetail') " & _
                    " WHERE NCat = 'PINV'  And Structure Is Null "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

            mQry = " UPDATE VoucherCat " & _
                    " SET Structure = 'PURCH',  " & _
                    " HeaderTable = (SELECT object_id FROM sys.Objects WHERE name = 'PurchInvoice'), " & _
                    " LineTable = (SELECT object_id FROM sys.Objects WHERE name = 'PurchInvoiceDetail') " & _
                    " WHERE NCat = 'PRET'  And Structure Is Null "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)

            mQry = " UPDATE VoucherCat " & _
                    " SET Structure = 'SALE',  " & _
                    " HeaderTable = (SELECT object_id FROM sys.Objects WHERE name = 'SaleOrder'), " & _
                    " LineTable = (SELECT object_id FROM sys.Objects WHERE name = 'SaleOrderDetail') " & _
                    " WHERE NCat = 'SO'  And Structure Is Null "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GCn, AgL.ECmd)
        Catch ex As Exception
            MsgBox(ex.Message + ".On TB_VoucherCat")
        End Try
    End Sub

    Private Sub CreateVType()
        Try
            '===================================================< KOT V_Type >===================================================
            'Try
            '    AgL.CreateNCat(AgL.GCn, Temp_NCat.KOT, Temp_NCat.KOT, "KOT", AgL.PubSiteCode)
            '    AgL.CreateVType(AgL.GCn, Temp_NCat.KOT, Temp_NCat.KOT, Temp_NCat.KOT, "KOT", Temp_NCat.KOT, AgL.PubUserName, AgL.PubLoginDate, AgL.PubStartDate, AgL.PubEndDate, AgL.PubSiteCode, AgL.PubDivCode, False, AgL.PubSitewiseV_No)
            'Catch ex As Exception
            '    MsgBox(ex.Message & " In CreateVType of " & Temp_NCat.KOT)
            'End Try
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
    End Sub

    Private Sub FPurchInvoice(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "VendorName", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "VendorAddress", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "VendorCity", AgLibrary.ClsMain.SQLDataType.nVarChar, 6)
        AgL.FSetColumnValue(MdlTable, "VendorMobile", AgLibrary.ClsMain.SQLDataType.nVarChar, 35)
        AgL.FSetFKeyValue(MdlTable, "VendorCity", "CityCode", "City")
    End Sub

    Private Sub FSubGroup(ByRef MdlTable() As AgLibrary.ClsMain.LITable, ByVal StrTableName As String, ByVal EntryType As EntryPointType)
        AgL.FAddTable(MdlTable, StrTableName, ModuleName)

        AgL.FSetColumnValue(MdlTable, "DispName", AgLibrary.ClsMain.SQLDataType.nVarChar, 100)
        AgL.FSetColumnValue(MdlTable, "MasterType", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
        AgL.FSetColumnValue(MdlTable, "Currency", AgLibrary.ClsMain.SQLDataType.nVarChar, 10)
        AgL.FSetColumnValue(MdlTable, "SalesTaxPostingGroup", AgLibrary.ClsMain.SQLDataType.nVarChar, 20)
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

        AgL.FSetColumnValue(MdlTable, "Specification", AgLibrary.ClsMain.SQLDataType.nVarChar, 255)

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

        AgL.FSetFKeyValue(MdlTable, "TableCode", "Code", "Ht_Table")
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
    End Sub
#End Region

    Public Shared Sub FPrepareContraText(ByVal BlnOverWrite As Boolean, ByRef StrContraTextVar As String, _
                                         ByVal StrContraName As String, ByVal DblAmount As Double, ByVal StrDrCr As String)
        Dim IntNameMaxLen As Integer = 35, IntAmtMaxLen As Integer = 18, IntSpaceNeeded As Integer = 2
        StrContraName = AgL.XNull(AgL.Dman_Execute("Select Name from Subgroup With (NoLock) Where SubCode = '" & StrContraName & "'  ", AgL.GcnRead).ExecuteScalar)

        If BlnOverWrite Then
            StrContraTextVar = Mid(Trim(StrContraName), 1, IntNameMaxLen) & Space((IntNameMaxLen + IntSpaceNeeded) - Len(Mid(Trim(StrContraName), 1, IntNameMaxLen))) & Space(IntAmtMaxLen - Len(Format(Val(DblAmount), "##,##,##,##,##0.00"))) & Format(Val(DblAmount), "##,##,##,##,##0.00") & " " & Trim(StrDrCr)
        Else
            StrContraTextVar += Mid(Trim(StrContraName), 1, IntNameMaxLen) & Space((IntNameMaxLen + IntSpaceNeeded) - Len(Mid(Trim(StrContraName), 1, IntNameMaxLen))) & Space(IntAmtMaxLen - Len(Format(Val(DblAmount), "##,##,##,##,##0.00"))) & Format(Val(DblAmount), "##,##,##,##,##0.00") & " " & Trim(StrDrCr)
        End If
    End Sub


    Public Shared Sub FPrintThisDocument(ByVal objFrm As Object, ByVal V_Type As String, _
    Optional ByVal PrintQuery As String = "", Optional ByVal RepName As String = "", Optional ByVal RepTitle As String = "")
        Dim DtVTypeSetting As DataTable = Nothing
        Dim mQry As String
        Dim mCrd As New ReportDocument
        Dim ReportView As New AgLibrary.RepView
        Dim DsRep As New DataSet
        Dim strQry As String = ""

        Try
            If PrintQuery = "" Then
                mQry = "Select * from Voucher_Type_Print_Settings With (NoLock) " & _
                       "Where V_Type = '" & V_Type & "' " & _
                       "And Site_Code = '" & AgL.PubSiteCode & "' " & _
                       "And Div_Code  = '" & AgL.PubDivCode & "' "
                DtVTypeSetting = AgL.FillData(mQry, AgL.GcnRead).Tables(0)
                If DtVTypeSetting.Rows.Count = 0 Then
                    MsgBox("Voucher type print settings are not defined, Can't continue.", MsgBoxStyle.OkOnly, "Validation")
                    Exit Sub
                End If

                If AgL.XNull(DtVTypeSetting.Rows(0)("Query")) = "" Then
                    MsgBox("Query Field is blank in Voucher type print settings, Can't continue.", MsgBoxStyle.OkOnly, "Validation")
                    Exit Sub
                Else
                    PrintQuery = AgL.XNull(DtVTypeSetting.Rows(0)("Query"))
                End If

                If AgL.XNull(DtVTypeSetting.Rows(0)("Report_Name")) = "" Then
                    MsgBox("Report_Name Field is blank in Voucher type print settings, Can't continue.", MsgBoxStyle.OkOnly, "Validation")
                    Exit Sub
                End If

                If AgL.XNull(DtVTypeSetting.Rows(0)("Report_Heading")) = "" Then
                    MsgBox("Report_Heading Field is blank in Voucher type print settings, Can't continue.", MsgBoxStyle.OkOnly, "Validation")
                    Exit Sub
                End If

                AgL.PubReportTitle = AgL.XNull(DtVTypeSetting.Rows(0)("Report_Heading"))
                RepName = AgL.XNull(DtVTypeSetting.Rows(0)("Report_Name")) : RepTitle = AgL.XNull(DtVTypeSetting.Rows(0)("Report_Heading"))

                PrintQuery = Replace(PrintQuery.ToString.ToUpper, "<SEARCHCODE>", objFrm.mSearchCode)
            End If


            AgL.ADMain = New SqlClient.SqlDataAdapter(PrintQuery, AgL.GCn)
            AgL.ADMain.Fill(DsRep)
            AgPL.CreateFieldDefFile1(DsRep, AgL.PubReportPath & "\" & RepName & ".ttx", True)
            mCrd.Load(AgL.PubReportPath & "\" & RepName & ".rpt")
            mCrd.SetDataSource(DsRep.Tables(0))
            CType(ReportView.Controls("CrvReport"), CrystalDecisions.Windows.Forms.CrystalReportViewer).ReportSource = mCrd
            AgPL.Formula_Set(mCrd, RepTitle)
            AgPL.Show_Report(ReportView, "* " & RepTitle & " *", objFrm.MdiParent)



            'AgPL.Generate_Report(PrintQuery, AgL.GCn, mCrd, New AgLibrary.RepView, AgL.PubReportPath, RepName, RepTitle, objFrm, objFrm.parent)

            Call AgL.LogTableEntry(objFrm.mSearchCode, objFrm.Text, "P", AgL.PubMachineName, AgL.PubUserName, AgL.PubLoginDate, AgL.GCn, AgL.ECmd)
        Catch Ex As Exception
            MsgBox(Ex.Message)
        Finally

        End Try
    End Sub



    'Public Shared Sub PostStructureToAccounts(ByVal FGMain As AgStructure.AgCalcGrid, ByVal mNarr As String, ByVal mDocID As String, ByVal mDiv_Code As String, _
    '                                          ByVal mSite_Code As String, ByVal mV_Type As String, ByVal mV_Prefix As String, ByVal mV_No As Integer, _
    '                                          ByVal mRecID As String, ByVal PostingPartyAc As String, ByVal mV_Date As String, _
    '                                          ByVal Conn As SqlClient.SqlConnection, ByVal Cmd As SqlClient.SqlCommand)
    '    Dim StrContraTextJV As String = ""
    '    Dim mPostSubCode = ""
    '    Dim I As Integer

    '    For I = 0 To FGMain.Rows.Count - 1
    '        If Trim(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_PostAc, I).Value) <> "" Then
    '            If AgL.StrCmp(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_PostAc, I).Value, "|PARTY|") Then
    '                If Val(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Amount, I).Value) > 0 And FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_DrCr, I).Value <> "" Then
    '                    If StrContraTextJV <> "" Then StrContraTextJV += vbCrLf
    '                    FPrepareContraText(False, StrContraTextJV, PostingPartyAc, FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Amount, I).Value, FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_DrCr, I).Value)
    '                End If
    '            Else
    '                If Val(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Amount, I).Value) > 0 And FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_DrCr, I).Value <> "" Then
    '                    If StrContraTextJV <> "" Then StrContraTextJV += vbCrLf
    '                    FPrepareContraText(False, StrContraTextJV, FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_PostAc, I).Value, FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Amount, I).Value, FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_DrCr, I).Value)
    '                End If
    '            End If
    '        End If
    '    Next

    '    Dim mQry$
    '    Dim mSrl As Integer = 0, mDebit As Double, mCredit As Double
    '    mQry = "Delete from Ledger where docId='" & mDocID & "'"
    '    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    '    For I = 0 To FGMain.Rows.Count - 1
    '        If Trim(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_PostAc, I).Value) <> "" And Val(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Amount, I).Value) <> 0 Then
    '            mSrl += 1

    '            mDebit = 0 : mCredit = 0
    '            If AgL.StrCmp(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_PostAc, I).Value, "|PARTY|") Then
    '                mPostSubCode = PostingPartyAc
    '            Else
    '                mPostSubCode = FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_PostAc, I).Value
    '            End If

    '            If AgL.StrCmp(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_DrCr, I).Value, "Dr") Then
    '                If Val(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Amount, I).Value) > 0 Then
    '                    mDebit = Math.Abs(Val(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Amount, I).Value))
    '                ElseIf Val(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Amount, I).Value) < 0 Then
    '                    mCredit = Math.Abs(Val(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Amount, I).Value))
    '                End If
    '            ElseIf AgL.StrCmp(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_DrCr, I).Value, "Cr") Then
    '                If Val(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Amount, I).Value) > 0 Then
    '                    mCredit = Math.Abs(Val(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Amount, I).Value))
    '                ElseIf Val(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Amount, I).Value) < 0 Then
    '                    mDebit = Math.Abs(Val(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Amount, I).Value))
    '                End If
    '            End If

    '            mQry = "Insert Into Ledger(DocId,RecId,V_SNo,V_Date,SubCode,ContraSub,AmtDr,AmtCr," & _
    '                 " Narration,V_Type,V_No,V_Prefix,Site_Code,Chq_No,Chq_Date,TDSCategory,TDSOnAmt,TDSDesc,TDSPer,TDS_Of_V_SNo,System_Generated,FormulaString,ContraText) Values " & _
    '                 " ('" & mDocID & "','" & mRecID & "'," & mSrl & "," & AgL.ConvertDate(mV_Date) & "," & AgL.Chk_Text(mPostSubCode) & "," & AgL.Chk_Text("") & ", " & _
    '                 " " & mDebit & "," & mCredit & ", " & _
    '                 " " & AgL.Chk_Text(mNarr) & ",'" & mV_Type & "','" & mV_No & "','" & mV_Prefix & "'," & _
    '                 " '" & mSite_Code & "','" & AgL.Chk_Text("") & "'," & _
    '                 " " & AgL.ConvertDate("") & "," & AgL.Chk_Text("") & "," & _
    '                 " " & Val("") & "," & AgL.Chk_Text("") & "," & Val("") & "," & 0 & ",'Y','" & "" & "','" & StrContraTextJV & "')"
    '            AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
    '        End If
    '    Next I
    'End Sub

    Public Shared Sub PostStructureToAccounts(ByVal FGMain As AgStructure.AgCalcGrid, ByVal mNarr As String, ByVal mDocID As String, ByVal mDiv_Code As String, _
                                              ByVal mSite_Code As String, ByVal Div_Code As String, ByVal mV_Type As String, ByVal mV_Prefix As String, ByVal mV_No As Integer, _
                                              ByVal mRecID As String, ByVal PostingPartyAc As String, ByVal mV_Date As String, _
                                              ByVal Conn As SqlClient.SqlConnection, ByVal Cmd As SqlClient.SqlCommand)
        Dim StrContraTextJV As String = ""
        Dim mPostSubCode = ""
        Dim I As Integer
        Dim mQry$ = "", bSelectionQry$ = ""
        Dim DtTemp As DataTable = Nothing


        For I = 0 To FGMain.Rows.Count - 1
            If Trim(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_PostAc, I).Value) <> "" Then
                If bSelectionQry = "" Then
                    bSelectionQry = " Select '" & FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_PostAc, I).Value & "' As PostAc, " & _
                    " Case When " & AgL.Chk_Text(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_DrCr, I).Value) & " = 'Dr' Then " & Val(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Amount, I).Value) & "  " & _
                    "      When " & AgL.Chk_Text(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_DrCr, I).Value) & " = 'Cr' Then " & -Val(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Amount, I).Value) & " End As Amount "
                Else
                    bSelectionQry += " UNION ALL "
                    bSelectionQry += " Select '" & FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_PostAc, I).Value & "' As PostAc, " & _
                    " Case When " & AgL.Chk_Text(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_DrCr, I).Value) & " = 'Dr' Then " & Val(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Amount, I).Value) & "  " & _
                    "      When " & AgL.Chk_Text(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_DrCr, I).Value) & " = 'Cr' Then " & -Val(FGMain(AgStructure.AgCalcGrid.AgCalcGridColumn.Col_Amount, I).Value) & " End As Amount "

                End If
            End If
        Next



        mQry = " Select V1.PostAc, IsNull(Sum(V1.Amount),0) As Amount, " & _
                " Case When IsNull(Sum(V1.Amount),0) > 0 Then 'Dr' " & _
                "      When IsNull(Sum(V1.Amount),0) < 0 Then 'Cr' End As DrCr " & _
                " From (" & bSelectionQry & ") As V1 " & _
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

                    mQry = "Insert Into Ledger(DocId,RecId,V_SNo,V_Date,SubCode,ContraSub,AmtDr,AmtCr," & _
                         " Narration,V_Type,V_No,V_Prefix,Site_Code,DivCode,Chq_No,Chq_Date,TDSCategory,TDSOnAmt,TDSDesc," & _
                         " TDSPer,TDS_Of_V_SNo,System_Generated,FormulaString,ContraText) Values " & _
                         " ('" & mDocID & "','" & mRecID & "'," & mSrl & "," & AgL.ConvertDate(mV_Date) & "," & AgL.Chk_Text(mPostSubCode) & "," & AgL.Chk_Text("") & ", " & _
                         " " & mDebit & "," & mCredit & ", " & _
                         " " & AgL.Chk_Text(mNarr) & ",'" & mV_Type & "','" & mV_No & "','" & mV_Prefix & "'," & _
                         " '" & mSite_Code & "','" & mDiv_Code & "','" & AgL.Chk_Text("") & "'," & _
                         " " & AgL.ConvertDate("") & "," & AgL.Chk_Text("") & "," & _
                         " " & Val("") & "," & AgL.Chk_Text("") & "," & Val("") & "," & 0 & ",'Y','" & "" & "','" & StrContraTextJV & "')"
                    AgL.Dman_ExecuteNonQry(mQry, Conn, Cmd)
                End If
            Next I
        End With
    End Sub

    Public Shared Function PayableLedgerQry(ByVal mFromDate As String, ByVal mToDate As String)
        PayableLedgerQry = " SELECT  'Opening' AS DocId, 'Opening' AS V_Type , " & AgL.ConvertDate(mFromDate) & " AS V_Date, " & _
                            " H.SubCode, CASE WHEN sum(H.AmtDr) > sum(H.AmtCr) THEN sum(H.AmtDr)-sum(H.AmtCr) ELSE 0 END AS AmtDr, " & _
                            " CASE WHEN sum(H.AmtDr) < sum(H.AmtCr) THEN sum(H.AmtCr)-sum(H.AmtDr) ELSE 0 END AS AmtCr, " & _
                            " 'Opening' AS TransactionType, 'Opening' AS Narration,H.Site_Code, H.DivCode, 'Opening' As RecId, 'Opening' As Particular " & _
                            " FROM Ledger H " & _
                            " LEFT JOIN Voucher_Type Vt ON Vt.V_Type = H.V_Type " & _
                            " WHERE H.V_Date < " & AgL.ConvertDate(mFromDate) & " " & _
                            " GROUP BY H.SubCode,H.Site_Code, H.DivCode "

        PayableLedgerQry = PayableLedgerQry & " UNION ALL " & _
                " SELECT  H.DocId, H.V_Type, H.V_Date, " & _
                " H.SubCode, H.AmtDr, H.AmtCr, Vt.Description As TransactionType,  " & _
                " Case When H.Chq_No IS NOT NULL THEN isnull(H.Narration,'') +' Cheque No. : '+ isnull(H.Chq_No,'') + ' Date : ' +isnull(convert(nvarchar,H.Chq_Date,3),'') ELSE isnull(H.Narration,'') END AS Narration, " & _
                " H.Site_Code, H.DivCode, H.RecId, H.V_Type + '-' + Convert(nVarChar,H.V_No) As Particular " & _
                " FROM Ledger H " & _
                " LEFT JOIN Voucher_Type Vt ON Vt.V_Type = H.V_Type " & _
                " WHERE H.V_Date BETWEEN " & AgL.ConvertDate(mFromDate) & " And " & AgL.ConvertDate(mToDate) & " "

    End Function
End Class