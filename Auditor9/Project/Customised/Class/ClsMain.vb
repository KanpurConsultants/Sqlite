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
        'ClsMain_EMail = New EMail.ClsMain(AgL)
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
        FCreateTable_SubgroupTypeSetting()
        FCreateTable_AcGroup()
        FCreateTable_Subgroup()
        FCreateTable_Ledger()
        FCreateTable_Charges()
        FCreateTable_Structure()
        FCreateTable_StructureDetail()
        FCreateTable_PostingGroupSalesTax()
        FCreateTable_Structure_AcPosting()
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
        FCreateTable_PurchaseInvoiceSetting()
        FCreateTable_PurchInvoice()
        FCreateTable_PurchInvoiceTransport()
        FCreateTable_PurchInvoiceDetail()
        FCreateTable_PurchInvoiceDimensionDetail()
        FCreateTable_SaleInvoiceSetting()
        FCreateTable_SaleInvoice()
        FCreateTable_SaleInvoiceTransport()
        FCreateTable_SaleInvoiceDetail()
        FCreateTable_SaleInvoiceDimensionDetail()
        FCreateTable_SaleInvoiceLastTransactionValues()


        FCreateView_ViewHelpSubgroup()
        FCreateView_ViewStockHeadSetting()
        FCreateView_User_VType_Permission()
        FCreateView_ViewPurchaseInvoiceSetting()
        FCreateView_ViewSaleInvoiceSetting()


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
        FSeedTable_SubgroupType()
        FSeedTable_SubgroupTypeSetting()
        FSeedTable_Subgroup()
        FSeedTable_Enviro()
        FSeedTable_Unit()
        FSeedTable_ItemType()
        FSeedTable_RateType()
        FSeedTable_Voucher_Type()
        FSeedTable_Charges()
        FSeedTable_Structure()
        FSeedTable_PostingGroupSalesTax()
        FSeedTable_PurchaseInvoiceSetting()
        FSeedTable_SaleInvoiceSetting()
    End Sub


#Region "Update Table Structure Functions"



    Private Sub FSeedTable_Charges()
        Dim mQry As String
        Try

            If AgL.FillData("Select * from Charges Where Description='Gross Amount'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    INSERT INTO Charges (Code, Description, ManualCode, Div_Code, FieldName)
                    VALUES ('GAMT', 'Gross Amount', 'GAMT', 'D',  'Gross_Amount')
                "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If


            If AgL.FillData("Select * from Charges Where Description='Taxable Amount'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    INSERT INTO Charges (Code, Description, ManualCode, Div_Code,   FieldName)
                    VALUES ('STTA', 'Taxable Amount', 'STTA', 'D', 'Sales_Tax_Taxable_Amt')
                "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If

            If AgL.FillData("Select * from Charges Where Description='IGST'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    INSERT INTO Charges (Code, Description, ManualCode, Div_Code,  FieldName)
                    VALUES ('IGST', 'IGST', 'IGST', 'D',   'CST')
                "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If

            If AgL.FillData("Select * from Charges Where Description='CGST'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    INSERT INTO Charges (Code, Description, ManualCode, Div_Code,  FieldName)
                    VALUES('CGST', 'CGST', 'CGST', 'D',   'Vat')
                "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If

            If AgL.FillData("Select * from Charges Where Description='SGST'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    INSERT INTO Charges (Code, Description, ManualCode, Div_Code,   FieldName)
                    VALUES ('SGST', 'SGST', 'SGST', 'D', 'Sat')
                "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If


            If AgL.FillData("Select * from Charges Where Description='Sub Total 1'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    INSERT INTO Charges (Code, Description, ManualCode, Div_Code,  FieldName)
                    VALUES ('STOT1', 'Sub Total 1', 'STOT1', 'D',   'Sub_Total1')                "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If

            If AgL.FillData("Select * from Charges Where Description='Deduction'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                        INSERT INTO Charges (Code, Description, ManualCode, Div_Code,   FieldName)
                        VALUES('DED', 'Deduction', 'DED', 'D',  'Deduction')
                   "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If


            If AgL.FillData("Select * from Charges Where Description='Other Charges'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    INSERT INTO Charges (Code, Description, ManualCode, Div_Code,  FieldName)
                    VALUES ('OC', 'Other Charges', 'OC', 'D',   'Other_Charges')
                   "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If



            If AgL.FillData("Select * from Charges Where Description='Sub Total 2'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    INSERT INTO Charges (Code, Description, ManualCode, Div_Code,  FieldName)
                    VALUES ('STOT2', 'Sub Total 2', 'STOT2', 'D',   'Sub_Total2')
                "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If


            If AgL.FillData("Select * from Charges Where Description='Round Off'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    INSERT INTO Charges (Code, Description, ManualCode, Div_Code,   FieldName)
                    VALUES ('RO', 'Round Off', 'RO', 'D',  'Round_Off')
                "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If


            If AgL.FillData("Select * from Charges Where Description='Net Amount'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    INSERT INTO Charges (Code, Description, ManualCode, Div_Code,  FieldName)
                    VALUES ('NAMT', 'Net Amount', 'NAMT', 'D',  'Net_Amount')
                "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If

        Catch ex As Exception
            MsgBox(ex.Message & " [FSeedTable_Charges] ")
        End Try

    End Sub


    Private Sub FSeedTable_Structure()
        Dim mQry As String

        Try

            If AgL.FillData("Select * from Structure Where Code='GstSaleMrp'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                        INSERT INTO Structure (Code, Description, HeaderTable, LineTable, Div_Code, Site_Code, PreparedBy, U_EntDt, U_AE, ModifiedBy, Edit_Date, UpLoadDate)
                        VALUES ('GstSaleMrp', 'UP GST MRP ', NULL, NULL, 'D', '1', 'sa', '2012-05-08', 'E', 'sa', '2017-07-23', NULL);


                        INSERT INTO StructureDetail (Code, Sr, Charges, Charge_Type, Value_Type, Value, Calculation, BaseColumn, PostAc, PostAcFromColumn, DrCr, LineItem, AffectCost, Active, Percentage, Amount, VisibleInMaster, VisibleInMasterLine, VisibleInTransactionLine, VisibleInTransactionFooter, HeaderPerField, HeaderAmtField, LineAmtField, LinePerField, GridDisplayIndex, UpLoadDate, WEF, InactiveDate)
                        VALUES ('GstSaleMrp', 10, 'GAMT', 'Charges', 'FixedValue', NULL, '|AMOUNT|', NULL, Null, NULL, Null, 0, 1, NULL, 0, 0, 0, 0, 0, 1, NULL, 'Gross_Amount', 'Gross_Amount', NULL, 0, NULL, '2017-07-01', NULL);


                        INSERT INTO StructureDetail (Code, Sr, Charges, Charge_Type, Value_Type, Value, Calculation, BaseColumn, PostAc, PostAcFromColumn, DrCr, LineItem, AffectCost, Active, Percentage, Amount, VisibleInMaster, VisibleInMasterLine, VisibleInTransactionLine, VisibleInTransactionFooter, HeaderPerField, HeaderAmtField, LineAmtField, LinePerField, GridDisplayIndex, UpLoadDate, WEF, InactiveDate)
                        VALUES ('GstSaleMrp', 15, 'STTA', 'Taxable Amount', 'FixedValue', NULL, '{GAMT}*100/(100+{CGST@}+{SGST@}+{IGST@})', NULL, 'SALE', NULL, 'Cr', 1, NULL, NULL, 0, 0, 1, 1, 1, 1, NULL, 'Taxable_Amount', 'Taxable_Amount', NULL, 0, NULL, '2017-07-01', NULL);


                        INSERT INTO StructureDetail (Code, Sr, Charges, Charge_Type, Value_Type, Value, Calculation, BaseColumn, PostAc, PostAcFromColumn, DrCr, LineItem, AffectCost, Active, Percentage, Amount, VisibleInMaster, VisibleInMasterLine, VisibleInTransactionLine, VisibleInTransactionFooter, HeaderPerField, HeaderAmtField, LineAmtField, LinePerField, GridDisplayIndex, UpLoadDate, WEF, InactiveDate)
                        VALUES ('GstSaleMrp', 20, 'IGST', 'Tax1', 'Percentage Changeable', NULL, '{STTA}*{IGST}/100', NULL, NULL, NULL, 'Cr', 1, 1, NULL, 0, 0, 0, 0, 1, 1, 'Tax1_Per', 'Tax1', 'Tax1', 'Tax1_Per', 0, NULL, '2017-07-01', NULL);


                        INSERT INTO StructureDetail (Code, Sr, Charges, Charge_Type, Value_Type, Value, Calculation, BaseColumn, PostAc, PostAcFromColumn, DrCr, LineItem, AffectCost, Active, Percentage, Amount, VisibleInMaster, VisibleInMasterLine, VisibleInTransactionLine, VisibleInTransactionFooter, HeaderPerField, HeaderAmtField, LineAmtField, LinePerField, GridDisplayIndex, UpLoadDate, WEF, InactiveDate)
                        VALUES ('GstSaleMrp', 25, 'CGST', 'Tax2', 'Percentage Changeable', NULL, '{STTA}*{CGST}/100', NULL, NULL, NULL, 'Cr', 1, 1, NULL, 0, 0, 0, 0, 1, 1, 'Tax2_Per', 'Tax2', 'Tax2', 'Tax2_Per', 0, NULL, '2017-07-01', NULL);


                        INSERT INTO StructureDetail (Code, Sr, Charges, Charge_Type, Value_Type, Value, Calculation, BaseColumn, PostAc, PostAcFromColumn, DrCr, LineItem, AffectCost, Active, Percentage, Amount, VisibleInMaster, VisibleInMasterLine, VisibleInTransactionLine, VisibleInTransactionFooter, HeaderPerField, HeaderAmtField, LineAmtField, LinePerField, GridDisplayIndex, UpLoadDate, WEF, InactiveDate)
                        VALUES ('GstSaleMrp', 30, 'SGST', 'Tax3', 'Percentage Changeable', NULL, '{STTA}*{SGST}/100', NULL, NULL, NULL, 'Cr', 1, 1, NULL, 0, 0, 0, 0, 1, 1, 'Tax3_Per', 'Tax3', 'Tax3', 'Tax3_Per', 0, NULL, '2017-07-01', NULL);



                        INSERT INTO StructureDetail (Code, Sr, Charges, Charge_Type, Value_Type, Value, Calculation, BaseColumn, PostAc, PostAcFromColumn, DrCr, LineItem, AffectCost, Active, Percentage, Amount, VisibleInMaster, VisibleInMasterLine, VisibleInTransactionLine, VisibleInTransactionFooter, HeaderPerField, HeaderAmtField, LineAmtField, LinePerField, GridDisplayIndex, UpLoadDate, WEF, InactiveDate)
                        VALUES ('GstSaleMrp', 35, 'STOT1', 'Charges', 'FixedValue', NULL, '{STTA}+{IGST}+{CGST}+{SGST}', NULL, NULL, NULL, NULL, 1, 1, NULL, 0, 0, 1, 0, 1, 1, NULL, 'SubTotal1', 'SubTotal1', NULL, 0, NULL, '2017-07-01', NULL);


                        INSERT INTO StructureDetail (Code, Sr, Charges, Charge_Type, Value_Type, Value, Calculation, BaseColumn, PostAc, PostAcFromColumn, DrCr, LineItem, AffectCost, Active, Percentage, Amount, VisibleInMaster, VisibleInMasterLine, VisibleInTransactionLine, VisibleInTransactionFooter, HeaderPerField, HeaderAmtField, LineAmtField, LinePerField, GridDisplayIndex, UpLoadDate, WEF, InactiveDate)
                        VALUES ('GstSaleMrp', 40, 'OC', 'Charges', 'Percentage Or Amount', NULL, '{STOT1}*{OC}/100', 'AMOUNT', 'OCHARGE', NULL, 'Cr', 0, 1, NULL, 0, 0, 0, 0, 0, 1, 'Other_Charge_Per', 'Other_Charge', 'Other_Charge', 'Other_Charge_Per', 0, NULL, '2017-07-01', NULL);


                        INSERT INTO StructureDetail (Code, Sr, Charges, Charge_Type, Value_Type, Value, Calculation, BaseColumn, PostAc, PostAcFromColumn, DrCr, LineItem, AffectCost, Active, Percentage, Amount, VisibleInMaster, VisibleInMasterLine, VisibleInTransactionLine, VisibleInTransactionFooter, HeaderPerField, HeaderAmtField, LineAmtField, LinePerField, GridDisplayIndex, UpLoadDate, WEF, InactiveDate)
                        VALUES ('GstSaleMrp', 45, 'DED', 'Charges', 'Percentage Or Amount', NULL, '{STOT1}*{DED}/100', 'AMOUNT', 'DEDUCTION', NULL, 'Dr', 0, 0, NULL, 0, 0, 0, 0, 0, 1, 'Deduction_Per', 'Deduction', 'Deduction', 'Deduction_Per', 0, NULL, '2017-07-01', NULL);


                        INSERT INTO StructureDetail (Code, Sr, Charges, Charge_Type, Value_Type, Value, Calculation, BaseColumn, PostAc, PostAcFromColumn, DrCr, LineItem, AffectCost, Active, Percentage, Amount, VisibleInMaster, VisibleInMasterLine, VisibleInTransactionLine, VisibleInTransactionFooter, HeaderPerField, HeaderAmtField, LineAmtField, LinePerField, GridDisplayIndex, UpLoadDate, WEF, InactiveDate)
                        VALUES ('GstSaleMrp', 50, 'RO', 'Charges', 'Round_Off', NULL, 'ROUND(({STOT1}+{OC}-{DED}),0)-({STOT1}+{OC}-{DED})', 'NET AMOUNT', 'ROFF', NULL, 'Cr', 0, 1, NULL, 0, 0, 0, 0, 0, 1, NULL, 'Round_Off', 'Round_Off', NULL, 0, NULL, '2017-07-01', NULL);


                        INSERT INTO StructureDetail (Code, Sr, Charges, Charge_Type, Value_Type, Value, Calculation, BaseColumn, PostAc, PostAcFromColumn, DrCr, LineItem, AffectCost, Active, Percentage, Amount, VisibleInMaster, VisibleInMasterLine, VisibleInTransactionLine, VisibleInTransactionFooter, HeaderPerField, HeaderAmtField, LineAmtField, LinePerField, GridDisplayIndex, UpLoadDate, WEF, InactiveDate)
                        VALUES ('GstSaleMrp', 55, 'NAMT', 'Charges', 'FixedValue', NULL, '{STOT1}+{OC}-{DED}+{RO}', NULL, '|PARTY|', NULL, 'Dr', 0, 1, NULL, 0, 0, 0, 0, 0, 1, NULL, 'Net_Amount', 'Net_Amount', NULL, 0, NULL, '2017-07-01', NULL);
                "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If


            If AgL.FillData("Select * from Structure Where Code='GstSale'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                        INSERT INTO Structure (Code, Description, HeaderTable, LineTable, Div_Code, Site_Code, PreparedBy, U_EntDt, U_AE, ModifiedBy, Edit_Date, UpLoadDate)
                        VALUES ('GstSale', 'UP GST MRP ', NULL, NULL, 'D', '1', 'sa', '2012-05-08', 'E', 'sa', '2017-07-23', NULL);


                        INSERT INTO StructureDetail (Code, Sr, Charges, Charge_Type, Value_Type, Value, Calculation, BaseColumn, PostAc, PostAcFromColumn, DrCr, LineItem, AffectCost, Active, Percentage, Amount, VisibleInMaster, VisibleInMasterLine, VisibleInTransactionLine, VisibleInTransactionFooter, HeaderPerField, HeaderAmtField, LineAmtField, LinePerField, GridDisplayIndex, UpLoadDate, WEF, InactiveDate)
                        VALUES ('GstSale', 10, 'GAMT', 'Charges', 'FixedValue', NULL, '|AMOUNT|', NULL, Null, NULL, Null, 0, 1, NULL, 0, 0, 0, 0, 0, 1, NULL, 'Gross_Amount', 'Gross_Amount', NULL, 0, NULL, '2017-07-01', NULL);


                        INSERT INTO StructureDetail (Code, Sr, Charges, Charge_Type, Value_Type, Value, Calculation, BaseColumn, PostAc, PostAcFromColumn, DrCr, LineItem, AffectCost, Active, Percentage, Amount, VisibleInMaster, VisibleInMasterLine, VisibleInTransactionLine, VisibleInTransactionFooter, HeaderPerField, HeaderAmtField, LineAmtField, LinePerField, GridDisplayIndex, UpLoadDate, WEF, InactiveDate)
                        VALUES ('GstSale', 15, 'STTA', 'Taxable Amount', 'FixedValue', NULL, '|AMOUNT|', NULL, 'SALE', NULL, 'Cr', 1, NULL, NULL, 0, 0, 1, 1, 1, 1, NULL, 'Taxable_Amount', 'Taxable_Amount', NULL, 0, NULL, '2017-07-01', NULL);


                        INSERT INTO StructureDetail (Code, Sr, Charges, Charge_Type, Value_Type, Value, Calculation, BaseColumn, PostAc, PostAcFromColumn, DrCr, LineItem, AffectCost, Active, Percentage, Amount, VisibleInMaster, VisibleInMasterLine, VisibleInTransactionLine, VisibleInTransactionFooter, HeaderPerField, HeaderAmtField, LineAmtField, LinePerField, GridDisplayIndex, UpLoadDate, WEF, InactiveDate)
                        VALUES ('GstSale', 20, 'IGST', 'Tax1', 'Percentage Changeable', NULL, '{STTA}*{IGST}/100', NULL, NULL, NULL, 'Cr', 1, 1, NULL, 0, 0, 0, 0, 1, 1, 'Tax1_Per', 'Tax1', 'Tax1', 'Tax1_Per', 0, NULL, '2017-07-01', NULL);


                        INSERT INTO StructureDetail (Code, Sr, Charges, Charge_Type, Value_Type, Value, Calculation, BaseColumn, PostAc, PostAcFromColumn, DrCr, LineItem, AffectCost, Active, Percentage, Amount, VisibleInMaster, VisibleInMasterLine, VisibleInTransactionLine, VisibleInTransactionFooter, HeaderPerField, HeaderAmtField, LineAmtField, LinePerField, GridDisplayIndex, UpLoadDate, WEF, InactiveDate)
                        VALUES ('GstSale', 25, 'CGST', 'Tax2', 'Percentage Changeable', NULL, '{STTA}*{CGST}/100', NULL, NULL, NULL, 'Cr', 1, 1, NULL, 0, 0, 0, 0, 1, 1, 'Tax2_Per', 'Tax2', 'Tax2', 'Tax2_Per', 0, NULL, '2017-07-01', NULL);


                        INSERT INTO StructureDetail (Code, Sr, Charges, Charge_Type, Value_Type, Value, Calculation, BaseColumn, PostAc, PostAcFromColumn, DrCr, LineItem, AffectCost, Active, Percentage, Amount, VisibleInMaster, VisibleInMasterLine, VisibleInTransactionLine, VisibleInTransactionFooter, HeaderPerField, HeaderAmtField, LineAmtField, LinePerField, GridDisplayIndex, UpLoadDate, WEF, InactiveDate)
                        VALUES ('GstSale', 30, 'SGST', 'Tax3', 'Percentage Changeable', NULL, '{STTA}*{SGST}/100', NULL, NULL, NULL, 'Cr', 1, 1, NULL, 0, 0, 0, 0, 1, 1, 'Tax3_Per', 'Tax3', 'Tax3', 'Tax3_Per', 0, NULL, '2017-07-01', NULL);



                        INSERT INTO StructureDetail (Code, Sr, Charges, Charge_Type, Value_Type, Value, Calculation, BaseColumn, PostAc, PostAcFromColumn, DrCr, LineItem, AffectCost, Active, Percentage, Amount, VisibleInMaster, VisibleInMasterLine, VisibleInTransactionLine, VisibleInTransactionFooter, HeaderPerField, HeaderAmtField, LineAmtField, LinePerField, GridDisplayIndex, UpLoadDate, WEF, InactiveDate)
                        VALUES ('GstSale', 35, 'STOT1', 'Charges', 'FixedValue', NULL, '{STTA}+{IGST}+{CGST}+{SGST}', NULL, NULL, NULL, NULL, 1, 1, NULL, 0, 0, 1, 0, 1, 1, NULL, 'SubTotal1', 'SubTotal1', NULL, 0, NULL, '2017-07-01', NULL);


                        INSERT INTO StructureDetail (Code, Sr, Charges, Charge_Type, Value_Type, Value, Calculation, BaseColumn, PostAc, PostAcFromColumn, DrCr, LineItem, AffectCost, Active, Percentage, Amount, VisibleInMaster, VisibleInMasterLine, VisibleInTransactionLine, VisibleInTransactionFooter, HeaderPerField, HeaderAmtField, LineAmtField, LinePerField, GridDisplayIndex, UpLoadDate, WEF, InactiveDate)
                        VALUES ('GstSale', 40, 'OC', 'Charges', 'Percentage Or Amount', NULL, '{STOT1}*{OC}/100', 'AMOUNT', 'OCHARGE', NULL, 'Cr', 0, 1, NULL, 0, 0, 0, 0, 0, 1, 'Other_Charge_Per', 'Other_Charge', 'Other_Charge', 'Other_Charge_Per', 0, NULL, '2017-07-01', NULL);


                        INSERT INTO StructureDetail (Code, Sr, Charges, Charge_Type, Value_Type, Value, Calculation, BaseColumn, PostAc, PostAcFromColumn, DrCr, LineItem, AffectCost, Active, Percentage, Amount, VisibleInMaster, VisibleInMasterLine, VisibleInTransactionLine, VisibleInTransactionFooter, HeaderPerField, HeaderAmtField, LineAmtField, LinePerField, GridDisplayIndex, UpLoadDate, WEF, InactiveDate)
                        VALUES ('GstSale', 45, 'DED', 'Charges', 'Percentage Or Amount', NULL, '{STOT1}*{DED}/100', 'AMOUNT', 'DEDUCTION', NULL, 'Dr', 0, 0, NULL, 0, 0, 0, 0, 0, 1, 'Deduction_Per', 'Deduction', 'Deduction', 'Deduction_Per', 0, NULL, '2017-07-01', NULL);


                        INSERT INTO StructureDetail (Code, Sr, Charges, Charge_Type, Value_Type, Value, Calculation, BaseColumn, PostAc, PostAcFromColumn, DrCr, LineItem, AffectCost, Active, Percentage, Amount, VisibleInMaster, VisibleInMasterLine, VisibleInTransactionLine, VisibleInTransactionFooter, HeaderPerField, HeaderAmtField, LineAmtField, LinePerField, GridDisplayIndex, UpLoadDate, WEF, InactiveDate)
                        VALUES ('GstSale', 50, 'RO', 'Charges', 'Round_Off', NULL, 'ROUND(({STOT1}+{OC}-{DED}),0)-({STOT1}+{OC}-{DED})', 'NET AMOUNT', 'ROFF', NULL, 'Cr', 0, 1, NULL, 0, 0, 0, 0, 0, 1, NULL, 'Round_Off', 'Round_Off', NULL, 0, NULL, '2017-07-01', NULL);


                        INSERT INTO StructureDetail (Code, Sr, Charges, Charge_Type, Value_Type, Value, Calculation, BaseColumn, PostAc, PostAcFromColumn, DrCr, LineItem, AffectCost, Active, Percentage, Amount, VisibleInMaster, VisibleInMasterLine, VisibleInTransactionLine, VisibleInTransactionFooter, HeaderPerField, HeaderAmtField, LineAmtField, LinePerField, GridDisplayIndex, UpLoadDate, WEF, InactiveDate)
                        VALUES ('GstSale', 55, 'NAMT', 'Charges', 'FixedValue', NULL, '{STOT1}+{OC}-{DED}+{RO}', NULL, '|PARTY|', NULL, 'Dr', 0, 1, NULL, 0, 0, 0, 0, 0, 1, NULL, 'Net_Amount', 'Net_Amount', NULL, 0, NULL, '2017-07-01', NULL);
                "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If


            If AgL.FillData("Select * from Structure Where Code='GstPur'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                        INSERT INTO Structure (Code, Description, HeaderTable, LineTable, Div_Code, Site_Code, PreparedBy, U_EntDt, U_AE, ModifiedBy, Edit_Date, UpLoadDate)
                        VALUES ('GstPur', 'UP GST MRP ', NULL, NULL, 'D', '1', 'sa', '2012-05-08', 'E', 'sa', '2017-07-23', NULL);


                        INSERT INTO StructureDetail (Code, Sr, Charges, Charge_Type, Value_Type, Value, Calculation, BaseColumn, PostAc, PostAcFromColumn, DrCr, LineItem, AffectCost, Active, Percentage, Amount, VisibleInMaster, VisibleInMasterLine, VisibleInTransactionLine, VisibleInTransactionFooter, HeaderPerField, HeaderAmtField, LineAmtField, LinePerField, GridDisplayIndex, UpLoadDate, WEF, InactiveDate)
                        VALUES ('GstPur', 10, 'GAMT', 'Charges', 'FixedValue', NULL, '|AMOUNT|', NULL, Null, NULL, Null, 0, 1, NULL, 0, 0, 0, 0, 0, 1, NULL, 'Gross_Amount', 'Gross_Amount', NULL, 0, NULL, '2017-07-01', NULL);


                        INSERT INTO StructureDetail (Code, Sr, Charges, Charge_Type, Value_Type, Value, Calculation, BaseColumn, PostAc, PostAcFromColumn, DrCr, LineItem, AffectCost, Active, Percentage, Amount, VisibleInMaster, VisibleInMasterLine, VisibleInTransactionLine, VisibleInTransactionFooter, HeaderPerField, HeaderAmtField, LineAmtField, LinePerField, GridDisplayIndex, UpLoadDate, WEF, InactiveDate)
                        VALUES ('GstPur', 15, 'STTA', 'Taxable Amount', 'FixedValue', NULL, '{GAMT}*100/(100+{CGST@}+{SGST@}+{IGST@})', NULL, 'PURCH', NULL, 'Dr', 1, NULL, NULL, 0, 0, 1, 1, 1, 1, NULL, 'Taxable_Amount', 'Taxable_Amount', NULL, 0, NULL, '2017-07-01', NULL);


                        INSERT INTO StructureDetail (Code, Sr, Charges, Charge_Type, Value_Type, Value, Calculation, BaseColumn, PostAc, PostAcFromColumn, DrCr, LineItem, AffectCost, Active, Percentage, Amount, VisibleInMaster, VisibleInMasterLine, VisibleInTransactionLine, VisibleInTransactionFooter, HeaderPerField, HeaderAmtField, LineAmtField, LinePerField, GridDisplayIndex, UpLoadDate, WEF, InactiveDate)
                        VALUES ('GstPur', 20, 'IGST', 'Tax1', 'Percentage Changeable', NULL, '{STTA}*{IGST}/100', NULL, NULL, NULL, 'Dr', 1, 1, NULL, 0, 0, 0, 0, 1, 1, 'Tax1_Per', 'Tax1', 'Tax1', 'Tax1_Per', 0, NULL, '2017-07-01', NULL);


                        INSERT INTO StructureDetail (Code, Sr, Charges, Charge_Type, Value_Type, Value, Calculation, BaseColumn, PostAc, PostAcFromColumn, DrCr, LineItem, AffectCost, Active, Percentage, Amount, VisibleInMaster, VisibleInMasterLine, VisibleInTransactionLine, VisibleInTransactionFooter, HeaderPerField, HeaderAmtField, LineAmtField, LinePerField, GridDisplayIndex, UpLoadDate, WEF, InactiveDate)
                        VALUES ('GstPur', 25, 'CGST', 'Tax2', 'Percentage Changeable', NULL, '{STTA}*{CGST}/100', NULL, NULL, NULL, 'Dr', 1, 1, NULL, 0, 0, 0, 0, 1, 1, 'Tax2_Per', 'Tax2', 'Tax2', 'Tax2_Per', 0, NULL, '2017-07-01', NULL);


                        INSERT INTO StructureDetail (Code, Sr, Charges, Charge_Type, Value_Type, Value, Calculation, BaseColumn, PostAc, PostAcFromColumn, DrCr, LineItem, AffectCost, Active, Percentage, Amount, VisibleInMaster, VisibleInMasterLine, VisibleInTransactionLine, VisibleInTransactionFooter, HeaderPerField, HeaderAmtField, LineAmtField, LinePerField, GridDisplayIndex, UpLoadDate, WEF, InactiveDate)
                        VALUES ('GstPur', 30, 'SGST', 'Tax3', 'Percentage Changeable', NULL, '{STTA}*{SGST}/100', NULL, NULL, NULL, 'Dr', 1, 1, NULL, 0, 0, 0, 0, 1, 1, 'Tax3_Per', 'Tax3', 'Tax3', 'Tax3_Per', 0, NULL, '2017-07-01', NULL);



                        INSERT INTO StructureDetail (Code, Sr, Charges, Charge_Type, Value_Type, Value, Calculation, BaseColumn, PostAc, PostAcFromColumn, DrCr, LineItem, AffectCost, Active, Percentage, Amount, VisibleInMaster, VisibleInMasterLine, VisibleInTransactionLine, VisibleInTransactionFooter, HeaderPerField, HeaderAmtField, LineAmtField, LinePerField, GridDisplayIndex, UpLoadDate, WEF, InactiveDate)
                        VALUES ('GstPur', 35, 'STOT1', 'Charges', 'FixedValue', NULL, '{STTA}+{IGST}+{CGST}+{SGST}', NULL, NULL, NULL, NULL, 1, 1, NULL, 0, 0, 1, 0, 1, 1, NULL, 'SubTotal1', 'SubTotal1', NULL, 0, NULL, '2017-07-01', NULL);


                        INSERT INTO StructureDetail (Code, Sr, Charges, Charge_Type, Value_Type, Value, Calculation, BaseColumn, PostAc, PostAcFromColumn, DrCr, LineItem, AffectCost, Active, Percentage, Amount, VisibleInMaster, VisibleInMasterLine, VisibleInTransactionLine, VisibleInTransactionFooter, HeaderPerField, HeaderAmtField, LineAmtField, LinePerField, GridDisplayIndex, UpLoadDate, WEF, InactiveDate)
                        VALUES ('GstPur', 40, 'OC', 'Charges', 'Percentage Or Amount', NULL, '{STOT1}*{OC}/100', 'AMOUNT', 'OCHARGE', NULL, 'Dr', 0, 1, NULL, 0, 0, 0, 0, 0, 1, 'Other_Charge_Per', 'Other_Charge', 'Other_Charge', 'Other_Charge_Per', 0, NULL, '2017-07-01', NULL);


                        INSERT INTO StructureDetail (Code, Sr, Charges, Charge_Type, Value_Type, Value, Calculation, BaseColumn, PostAc, PostAcFromColumn, DrCr, LineItem, AffectCost, Active, Percentage, Amount, VisibleInMaster, VisibleInMasterLine, VisibleInTransactionLine, VisibleInTransactionFooter, HeaderPerField, HeaderAmtField, LineAmtField, LinePerField, GridDisplayIndex, UpLoadDate, WEF, InactiveDate)
                        VALUES ('GstPur', 45, 'DED', 'Charges', 'Percentage Or Amount', NULL, '{STOT1}*{DED}/100', 'AMOUNT', 'DEDUCTION', NULL, 'Cr', 0, 0, NULL, 0, 0, 0, 0, 0, 1, 'Deduction_Per', 'Deduction', 'Deduction', 'Deduction_Per', 0, NULL, '2017-07-01', NULL);


                        INSERT INTO StructureDetail (Code, Sr, Charges, Charge_Type, Value_Type, Value, Calculation, BaseColumn, PostAc, PostAcFromColumn, DrCr, LineItem, AffectCost, Active, Percentage, Amount, VisibleInMaster, VisibleInMasterLine, VisibleInTransactionLine, VisibleInTransactionFooter, HeaderPerField, HeaderAmtField, LineAmtField, LinePerField, GridDisplayIndex, UpLoadDate, WEF, InactiveDate)
                        VALUES ('GstPur', 50, 'RO', 'Charges', 'Round_Off', NULL, 'ROUND(({STOT1}+{OC}-{DED}),0)-({STOT1}+{OC}-{DED})', 'NET AMOUNT', 'ROFF', NULL, 'Dr', 0, 1, NULL, 0, 0, 0, 0, 0, 1, NULL, 'Round_Off', 'Round_Off', NULL, 0, NULL, '2017-07-01', NULL);


                        INSERT INTO StructureDetail (Code, Sr, Charges, Charge_Type, Value_Type, Value, Calculation, BaseColumn, PostAc, PostAcFromColumn, DrCr, LineItem, AffectCost, Active, Percentage, Amount, VisibleInMaster, VisibleInMasterLine, VisibleInTransactionLine, VisibleInTransactionFooter, HeaderPerField, HeaderAmtField, LineAmtField, LinePerField, GridDisplayIndex, UpLoadDate, WEF, InactiveDate)
                        VALUES ('GstPur', 55, 'NAMT', 'Charges', 'FixedValue', NULL, '{STOT1}+{OC}-{DED}+{RO}', NULL, '|PARTY|', NULL, 'Cr', 0, 1, NULL, 0, 0, 0, 0, 0, 1, NULL, 'Net_Amount', 'Net_Amount', NULL, 0, NULL, '2017-07-01', NULL);
                "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If
        Catch ex As Exception
            MsgBox(ex.Message & "  [FSeedTable_Structure]")
        End Try

    End Sub


    Private Sub FCreateTable_PurchInvoiceDimensionDetail()
        Dim mQry As String
        Try
            If Not AgL.IsTableExist("PurchInvoiceDimensionDetail", AgL.GcnMain) Then
                mQry = " CREATE TABLE [PurchInvoiceDimensionDetail] ([DocID] nVarchar(21) NOT NULL, 
                   [TSr] int,
                   [Sr] int,
                   PRIMARY KEY ([DocID],[Tsr],[Sr]) ); "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If
            AgL.AddFieldSqlite("PurchInvoiceDimensionDetail", "Specification", "nVarchar(255)", "", True)
            AgL.AddFieldSqlite("PurchInvoiceDimensionDetail", "Pcs", "Float", "0", False)
            AgL.AddFieldSqlite("PurchInvoiceDimensionDetail", "Qty", "Float", "0", False)
            AgL.AddFieldSqlite("PurchInvoiceDimensionDetail", "TotalQty", "Float", "0", False)
            AgL.AddFieldSqlite("PurchInvoiceDimensionDetail", "Unit", "nVarchar(10)", "", True, "References Unit(Code)")
            AgL.AddFieldSqlite("PurchInvoiceDimensionDetail", "UploadDate", "DateTime", "", True)
        Catch ex As Exception
            MsgBox(ex.Message & "  [FCreateTable_PurchInvoiceDimensionDetail]")
        End Try

    End Sub


    Private Sub FCreateTable_Dimension1()
        Dim mQry As String
        Try
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
        Catch ex As Exception
            MsgBox(ex.Message & "  [FCreateTable_Dimension1]")
        End Try

    End Sub

    Private Sub FCreateTable_Dimension4()
        Try
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
        Catch ex As Exception
            MsgBox(ex.Message & "  [FCreateTable_Dimension4]")
        End Try

    End Sub

    Private Sub FCreateTable_Dimension3()
        Dim mQry As String
        Try
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
        Catch ex As Exception
            MsgBox(ex.Message & "  [FCreateTable_Dimension3]")
        End Try

    End Sub

    Private Sub FCreateTable_Dimension2()
        Dim mQry As String
        Try
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
        Catch ex As Exception
            MsgBox(ex.Message & "  [FCreateTable_Dimension2]")
        End Try

    End Sub

    Private Sub FCreateTable_Charges()
        Dim mQry As String
        Try
            If Not AgL.IsTableExist("Charges", AgL.GcnMain) Then
                mQry = " CREATE TABLE [Charges] ([Code] nVarchar(10) NOT NULL, PRIMARY KEY ([Code]) ); "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If
            AgL.AddFieldSqlite("Charges", "Description", "nVarchar(50)", "", True)
            AgL.AddFieldSqlite("Charges", "ManualCode", "nVarchar(20)", "", True)
            AgL.AddFieldSqlite("Charges", "FieldName", "nVarchar(50)", "", True)
            AgL.AddFieldSqlite("Charges", "EntryBy", "nVarchar(10)", "", True)
            AgL.AddFieldSqlite("Charges", "EntryDate", "DateTime", "", True)
            AgL.AddFieldSqlite("Charges", "EntryType", "nVarchar(10)", "", True)
            AgL.AddFieldSqlite("Charges", "EntryStatus", "nVarchar(10)", "", True)
            AgL.AddFieldSqlite("Charges", "ApproveBy", "nVarchar(10)", "", True)
            AgL.AddFieldSqlite("Charges", "ApproveDate", "DateTime", "", True)
            AgL.AddFieldSqlite("Charges", "MoveToLog", "nVarchar(10)", "", True)
            AgL.AddFieldSqlite("Charges", "MoveToLogDate", "DateTime", "", True)
            AgL.AddFieldSqlite("Charges", "Status", "nVarchar(10)", "", True)
            AgL.AddFieldSqlite("Charges", "Div_Code", "nVarchar(10)", "", True)
        Catch ex As Exception
            MsgBox(ex.Message & "  [FCreateTable_Charges]")
        End Try

    End Sub
    Private Sub FCreateTable_StockProcess()
        Dim mQry As String
        Try
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
        Catch ex As Exception
            MsgBox(ex.Message & "  [FCreateTable_StockProcess]")
        End Try

    End Sub

    Private Sub FCreateTable_Stock()
        Dim mQry As String
        Try

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
        Catch ex As Exception
            MsgBox(ex.Message & " [FCreateTable_Stock] ")
        End Try

    End Sub

    Private Sub FCreateTable_StockHeadDetail()
        Dim mQry As String
        Try

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
        Catch ex As Exception
            MsgBox(ex.Message & " [FCreateTable_StockHeadDetail] ")
        End Try

    End Sub


    Private Sub FCreateTable_StockHead()
        Dim mQry As String
        Try

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
        Catch ex As Exception
            MsgBox(ex.Message & "  [FCreateTable_StockHead]")
        End Try

    End Sub

    Private Sub FSeedTable_RateType()
        Dim mQry As String
        Try

            If AgL.FillData("Select * from RateType limit 1", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    INSERT INTO RateType
                    (Code, Description, IsDeleted, EntryBy, EntryDate, EntryType, EntryStatus, ApproveBy, ApproveDate, MoveToLog, MoveToLogDate, Status, Div_Code, UID, Margin, Sr)
                    VALUES('SUPER', 'Super Nett Rate', NULL, 'SUPER', '2018-02-16', 'Edit', 'Open', NULL, NULL, NULL, NULL, 'Active', 'D', NULL, 10.0, 0);
                    INSERT INTO RateType
                    (Code, Description, IsDeleted, EntryBy, EntryDate, EntryType, EntryStatus, ApproveBy, ApproveDate, MoveToLog, MoveToLogDate, Status, Div_Code, UID, Margin, Sr)
                    VALUES('NETT', 'Nett Rate', NULL, 'SUPER', '2018-02-16', 'Edit', 'Open', NULL, NULL, NULL, NULL, 'Active', 'D', NULL, 15.0, 0);
                    INSERT INTO RateType
                    (Code, Description, IsDeleted, EntryBy, EntryDate, EntryType, EntryStatus, ApproveBy, ApproveDate, MoveToLog, MoveToLogDate, Status, Div_Code, UID, Margin, Sr)
                    VALUES('DHARA', 'Dhara Rate', NULL, 'SUPER', '2018-02-16', 'Edit', 'Open', NULL, NULL, NULL, NULL, 'Active', 'D', NULL, 20.0, 0);
                "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " [FSeedTable_RateType] ")
        End Try

    End Sub


    Private Sub FSeedTable_Voucher_Type()
        Dim mQry As String

        Try

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


            If AgL.FillData("Select * from Voucher_Type Where V_Type='SINV'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    INSERT INTO Voucher_Type
                    (NCat, Category, V_Type, Description, Short_Name, SystemDefine, DivisionWise, SiteWise, PreparedBy, U_EntDt, U_AE, ModifiedBy, Edit_Date, IssRec, Description_Help, Description_BiLang, Short_Name_BiLang, Report_Index, Number_Method, Start_No, Last_Ent_Date, Form_Name, Saperate_Narr, Common_Narr, Narration, Print_VNo, Header_Desc, Term_Desc, Footer_Desc, Exclude_Ac_Grp, SerialNo_From_Table, U_Name, ChqNo, ChqDt, ClgDt, DefaultCrAc, DefaultDrAc, FirstDrCr, TrnType, TdsDed, ContraNarr, TdsOnAmt, Contra_Narr, Separate_Narr, MnuAttachedInModule, AuditAllowed, UpLoadDate, Affect_FA, IsShowVoucherReference, MnuName, MnuText, SerialNo, HeaderTable, LogHeaderTable, DefaultAc, CustomFields, ContraV_Type)
                    VALUES('SINV', 'OTHER', 'SINV', 'Sale Invoice', 'SINV', 'Y', 0, 1, 'sa', '2012-10-11 00:00:00', 'A', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'Automatic', NULL, NULL, NULL, 'N', 'Y', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'Y', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
                "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)

                If AgL.FillData("Select * from Voucher_Prefix Where V_Type='SINV' And Prefix = '2017' ", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                    mQry = " 
                    INSERT INTO Voucher_Prefix
                    (V_Type, Date_From, Prefix, Start_Srl_No, Date_To, Comp_Code, Site_Code, Div_Code, UpLoadDate, Status_Add, Status_Edit, Status_Delete, Status_Print, Ref_Prefix, Ref_PadLength)
                    VALUES('SINV', '2017-04-01', '2017', 0, '2018-03-31', '1', '1', 'D', NULL, NULL, NULL, NULL, NULL, '17-', 5);
                    "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
                End If

            End If

            mQry = "Update Voucher_Type SET Structure = 'GstSale' Where V_Type = 'SINV'"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)



            If AgL.FillData("Select * from Voucher_Type Where V_Type='PINV'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    INSERT INTO Voucher_Type
                    (NCat, Category, V_Type, Description, Short_Name, SystemDefine, DivisionWise, SiteWise, PreparedBy, U_EntDt, U_AE, ModifiedBy, Edit_Date, IssRec, Description_Help, Description_BiLang, Short_Name_BiLang, Report_Index, Number_Method, Start_No, Last_Ent_Date, Form_Name, Saperate_Narr, Common_Narr, Narration, Print_VNo, Header_Desc, Term_Desc, Footer_Desc, Exclude_Ac_Grp, SerialNo_From_Table, U_Name, ChqNo, ChqDt, ClgDt, DefaultCrAc, DefaultDrAc, FirstDrCr, TrnType, TdsDed, ContraNarr, TdsOnAmt, Contra_Narr, Separate_Narr, MnuAttachedInModule, AuditAllowed, UpLoadDate, Affect_FA, IsShowVoucherReference, MnuName, MnuText, SerialNo, HeaderTable, LogHeaderTable, DefaultAc, CustomFields, ContraV_Type)
                    VALUES('PINV', 'OTHER', 'PINV', 'Purchase Invoice', 'PINV', 'Y', 0, 1, 'sa', '2012-10-11 00:00:00', 'A', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'Automatic', NULL, NULL, NULL, 'N', 'Y', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'Y', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
                "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)

                If AgL.FillData("Select * from Voucher_Prefix Where V_Type='PINV' And Prefix = '2017' ", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                    mQry = " 
                    INSERT INTO Voucher_Prefix
                    (V_Type, Date_From, Prefix, Start_Srl_No, Date_To, Comp_Code, Site_Code, Div_Code, UpLoadDate, Status_Add, Status_Edit, Status_Delete, Status_Print, Ref_Prefix, Ref_PadLength)
                    VALUES('PINV', '2017-04-01', '2017', 0, '2018-03-31', '1', '1', 'D', NULL, NULL, NULL, NULL, NULL, '17-', 5);
                    "
                    AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
                End If
            End If

            mQry = "Update Voucher_Type SET Structure = 'GstPur' Where V_Type = 'PINV'"
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)


        Catch ex As Exception
            MsgBox(ex.Message & " [FSeedTable_Voucher_Type] ")
        End Try

    End Sub

    Private Sub FCreateTable_Structure_AcPosting()
        Dim mQry As String
        Try
            If Not AgL.IsTableExist("Structure_AcPosting", AgL.GcnMain) Then
                mQry = " CREATE TABLE [Structure_AcPosting] ([NCAT] nVarchar(10) NOT NULL, [Sr] int Not Null, PRIMARY KEY ([NCAT],[Sr]) ); "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If
            AgL.AddFieldSqlite("Structure_AcPosting", "Structure", "nVarchar(10)", "", True)
            AgL.AddFieldSqlite("Structure_AcPosting", "Charges", "nVarchar(8)", "", True)
            AgL.AddFieldSqlite("Structure_AcPosting", "PostAc", "nVarchar(10)", "", True, "references Subgroup(subcode)")
            AgL.AddFieldSqlite("Structure_AcPosting", "ContraSub", "nVarchar(10)", "", True, "references Subgroup(subcode)")
            AgL.AddFieldSqlite("Structure_AcPosting", "PostAcFromColumn", "nVarchar(50)", "", True)
            AgL.AddFieldSqlite("Structure_AcPosting", "DrCr", "nVarchar(2)", "", True)
            AgL.AddFieldSqlite("Structure_AcPosting", "UploadDate", "DateTime", "", True)
        Catch ex As Exception
            MsgBox(ex.Message & "  [FCreateTable_Structure_AcPosting]")
        End Try


    End Sub
    Private Sub FCreateTable_SaleInvoice()
        Dim mQry As String
        Try

            If Not AgL.IsTableExist("SaleInvoice", AgL.GcnMain) Then
                mQry = " CREATE TABLE [SaleInvoice] ([DocID] nVarchar(21) NOT NULL, 
                   [V_Type] nvarchar(5) References Voucher_Type(V_Type) COLLATE NOCASE,
                   [V_Prefix] nvarchar(5) COLLATE NOCASE,
                   [V_Date] datetime,
                   [V_No] bigint,
                   [Div_Code] nvarchar(1) References Division(Div_Code) COLLATE NOCASE,
                   [Site_Code] nvarchar(2) References SiteMast(Code) COLLATE NOCASE,
                   PRIMARY KEY ([DocID]) ); "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If
            AgL.AddFieldSqlite("SaleInvoice", "ReferenceNo", "nVarchar(20)", "", True)
            AgL.AddFieldSqlite("SaleInvoice", "SaleToParty", "nVarchar(10)", "", True, "References Subgroup(Subcode)")
            AgL.AddFieldSqlite("SaleInvoice", "BillToParty", "nVarchar(10)", "", True, "References Subgroup(Subcode)")
            AgL.AddFieldSqlite("SaleInvoice", "Agent", "nVarchar(10)", "", True, "References Subgroup(Subcode)")
            AgL.AddFieldSqlite("SaleInvoice", "SaleToPartyName", "nVarchar(100)", "", True)
            AgL.AddFieldSqlite("SaleInvoice", "SaleToPartyAddress", "nVarchar(255)", "", True)
            AgL.AddFieldSqlite("SaleInvoice", "SaleToPartyCity", "nVarchar(6)", "", True, "references City(CityCode)")
            AgL.AddFieldSqlite("SaleInvoice", "SaleToPartyMobile", "nVarchar(10)", "", True)
            AgL.AddFieldSqlite("SaleInvoice", "SaleToPartySalesTaxNo", "nVarchar(50)", "", True)
            AgL.AddFieldSqlite("SaleInvoice", "ShipToAddress", "nVarchar(255)", "", True)
            AgL.AddFieldSqlite("SaleInvoice", "RateType", "nVarchar(6)", "", True, "References RateType(Code)")
            AgL.AddFieldSqlite("SaleInvoice", "SalesTaxGroupParty", "nVarchar(20)", "", True)
            AgL.AddFieldSqlite("SaleInvoice", "PlaceOfSupply", "nVarchar(20)", "", True)
            AgL.AddFieldSqlite("SaleInvoice", "Structure", "nVarchar(8)", "", True, "References Structure (Code) ")
            AgL.AddFieldSqlite("SaleInvoice", "CustomFields", "nVarchar(8)", "", True, "References CustomFields (Code) ")
            AgL.AddFieldSqlite("SaleInvoice", "SaleToPartyDocNo", "nVarchar(50)", "", True)
            AgL.AddFieldSqlite("SaleInvoice", "SaleToPartyDocDate", "DateTime", "", True)
            AgL.AddFieldSqlite("SaleInvoice", "ReferenceDocId", "nVarchar(21)", "", True)
            AgL.AddFieldSqlite("SaleInvoice", "Remarks", "nVarchar(255)", "", True)
            AgL.AddFieldSqlite("SaleInvoice", "TermsAndConditions", "nVarchar(1000)", "", True)
            AgL.AddFieldSqlite("SaleInvoice", "Gross_Amount", "Float", "0", False)
            AgL.AddFieldSqlite("SaleInvoice", "Taxable_Amount", "Float", "0", False)
            AgL.AddFieldSqlite("SaleInvoice", "Tax1_Per", "Float", "0", False)
            AgL.AddFieldSqlite("SaleInvoice", "Tax1", "Float", "0", False)
            AgL.AddFieldSqlite("SaleInvoice", "Tax2_Per", "Float", "0", False)
            AgL.AddFieldSqlite("SaleInvoice", "Tax2", "Float", "0", False)
            AgL.AddFieldSqlite("SaleInvoice", "Tax3_Per", "Float", "0", False)
            AgL.AddFieldSqlite("SaleInvoice", "Tax3", "Float", "0", False)
            AgL.AddFieldSqlite("SaleInvoice", "Tax4_Per", "Float", "0", False)
            AgL.AddFieldSqlite("SaleInvoice", "Tax4", "Float", "0", False)
            AgL.AddFieldSqlite("SaleInvoice", "Tax5_Per", "Float", "0", False)
            AgL.AddFieldSqlite("SaleInvoice", "Tax5", "Float", "0", False)
            AgL.AddFieldSqlite("SaleInvoice", "SubTotal1", "Float", "0", False)
            AgL.AddFieldSqlite("SaleInvoice", "Deduction_Per", "Float", "0", False)
            AgL.AddFieldSqlite("SaleInvoice", "Deduction", "Float", "0", False)
            AgL.AddFieldSqlite("SaleInvoice", "Other_Charge_Per", "Float", "0", False)
            AgL.AddFieldSqlite("SaleInvoice", "Other_Charge", "Float", "0", False)
            AgL.AddFieldSqlite("SaleInvoice", "SubTotal1", "Float", "0", False)
            AgL.AddFieldSqlite("SaleInvoice", "Round_Off", "Float", "0", False)
            AgL.AddFieldSqlite("SaleInvoice", "Net_Amount", "Float", "0", False)
            AgL.AddFieldSqlite("SaleInvoice", "PaidAmt", "Float", "0", False)
            AgL.AddFieldSqlite("SaleInvoice", "CreditLimit", "Float", "0", False)
            AgL.AddFieldSqlite("SaleInvoice", "CreditDays", "Float", "0", False)
            AgL.AddFieldSqlite("SaleInvoice", "Div_Code", "nVarchar(1)", "", True)
            AgL.AddFieldSqlite("SaleInvoice", "Site_Code", "nVarchar(2)", "", True)
            AgL.AddFieldSqlite("SaleInvoice", "Status", "nVarchar(20)", "", True)

            AgL.AddFieldSqlite("SaleInvoice", "EntryBy", "nVarchar(10)", "", True)
            AgL.AddFieldSqlite("SaleInvoice", "EntryDate", "DateTime", "", True)
            AgL.AddFieldSqlite("SaleInvoice", "ApproveBy", "nVarchar(10)", "", True)
            AgL.AddFieldSqlite("SaleInvoice", "ApproveDate", "DateTime", "", True)
            AgL.AddFieldSqlite("SaleInvoice", "MoveToLog", "nVarchar(10)", "", True)
            AgL.AddFieldSqlite("SaleInvoice", "MoveToLogDate", "DateTime", "", True)
            AgL.AddFieldSqlite("SaleInvoice", "UploadDate", "DateTime", "", True)

        Catch ex As Exception
            MsgBox(ex.Message & "  [FCreateTable_SaleInvoice]")
        End Try

    End Sub

    Private Sub FCreateTable_SaleInvoiceDetail()
        Dim mQry As String
        Try
            If Not AgL.IsTableExist("SaleInvoiceDetail", AgL.GcnMain) Then
                mQry = " CREATE TABLE [SaleInvoiceDetail] ([DocID] nVarchar(21) NOT NULL, 
                   [Sr] int,
                   PRIMARY KEY ([DocID],[Sr]) ); "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If
            AgL.AddFieldSqlite("SaleInvoiceDetail", "V_Nature", "nVarchar(20)", "", True)
            AgL.AddFieldSqlite("SaleInvoiceDetail", "ReferenceNo", "nVarchar(20)", "", True)
            AgL.AddFieldSqlite("SaleInvoiceDetail", "Item", "nVarchar(10)", "", True, "References Item(Code)")
            AgL.AddFieldSqlite("SaleInvoiceDetail", "Specification", "nVarchar(255)", "", True)
            AgL.AddFieldSqlite("SaleInvoiceDetail", "Dimension1", "nVarchar(10)", "", True, "References Dimension1(Code)")
            AgL.AddFieldSqlite("SaleInvoiceDetail", "Dimension2", "nVarchar(10)", "", True, "References Dimension2(Code)")
            AgL.AddFieldSqlite("SaleInvoiceDetail", "Dimension3", "nVarchar(10)", "", True, "References Dimension3(Code)")
            AgL.AddFieldSqlite("SaleInvoiceDetail", "Dimension4", "nVarchar(10)", "", True, "References Dimension4(Code)")
            AgL.AddFieldSqlite("SaleInvoiceDetail", "SalesTaxGroupItem", "nVarchar(20)", "", True)
            AgL.AddFieldSqlite("SaleInvoiceDetail", "LotNo", "nVarchar(50)", "", True)
            AgL.AddFieldSqlite("SaleInvoiceDetail", "BaleNo", "nVarchar(50)", "", True)
            AgL.AddFieldSqlite("SaleInvoiceDetail", "Deal", "nVarchar(10)", "", True)
            AgL.AddFieldSqlite("SaleInvoiceDetail", "ExpiryDate", "DateTime", "", True)
            AgL.AddFieldSqlite("SaleInvoiceDetail", "LrNo", "nVarchar(50)", "", True)
            AgL.AddFieldSqlite("SaleInvoiceDetail", "LrDate", "DateTime", "", True)
            AgL.AddFieldSqlite("SaleInvoiceDetail", "DocQty", "Float", "0", False)
            AgL.AddFieldSqlite("SaleInvoiceDetail", "FreeQty", "Float", "0", False)
            AgL.AddFieldSqlite("SaleInvoiceDetail", "Qty", "Float", "0", False)
            AgL.AddFieldSqlite("SaleInvoiceDetail", "RejQty", "Float", "0", False)
            AgL.AddFieldSqlite("SaleInvoiceDetail", "Unit", "nVarchar(10)", "", True, "References Unit(Code)")
            AgL.AddFieldSqlite("SaleInvoiceDetail", "UnitMultiplier", "Float", "0", False)
            AgL.AddFieldSqlite("SaleInvoiceDetail", "DocDealQty", "Float", "0", False)
            AgL.AddFieldSqlite("SaleInvoiceDetail", "FreeDealQty", "Float", "0", False)
            AgL.AddFieldSqlite("SaleInvoiceDetail", "DealQty", "Float", "0", False)
            AgL.AddFieldSqlite("SaleInvoiceDetail", "RejDealQty", "Float", "0", False)
            AgL.AddFieldSqlite("SaleInvoiceDetail", "DealUnit", "nVarchar(10)", "", True, "References Unit(Code)")
            AgL.AddFieldSqlite("SaleInvoiceDetail", "Rate", "Float", "0", False)
            AgL.AddFieldSqlite("SaleInvoiceDetail", "MRP", "Float", "0", False)
            AgL.AddFieldSqlite("SaleInvoiceDetail", "DiscountPer", "Float", "0", False)
            AgL.AddFieldSqlite("SaleInvoiceDetail", "DiscountAmount", "Float", "0", False)
            AgL.AddFieldSqlite("SaleInvoiceDetail", "AdditionalDiscountPer", "Float", "0", False)
            AgL.AddFieldSqlite("SaleInvoiceDetail", "AdditionalDiscountAmount", "Float", "0", False)
            AgL.AddFieldSqlite("SaleInvoiceDetail", "Amount", "Float", "0", False)
            AgL.AddFieldSqlite("SaleInvoiceDetail", "ProfitMarginPer", "Float", "0", False)
            AgL.AddFieldSqlite("SaleInvoiceDetail", "Sale_Rate", "Float", "0", False)
            AgL.AddFieldSqlite("SaleInvoiceDetail", "ReferenceDocId", "nVarchar(21)", "", True)
            AgL.AddFieldSqlite("SaleInvoiceDetail", "ReferenceDocIdSr", "int", "", True)
            AgL.AddFieldSqlite("SaleInvoiceDetail", "SaleInvoice", "nVarchar(21)", "", True)
            AgL.AddFieldSqlite("SaleInvoiceDetail", "SaleInvoiceSr", "int", "", True)
            AgL.AddFieldSqlite("SaleInvoiceDetail", "Godown", "nVarchar(10)", "", True, "References Subgroup(Subcode)")
            AgL.AddFieldSqlite("SaleInvoiceDetail", "Remark", "nVarchar(255)", "", True)

            AgL.AddFieldSqlite("SaleInvoiceDetail", "Gross_Amount", "Float", "0", False)
            AgL.AddFieldSqlite("SaleInvoiceDetail", "Taxable_Amount", "Float", "0", False)
            AgL.AddFieldSqlite("SaleInvoiceDetail", "Tax1_Per", "Float", "0", False)
            AgL.AddFieldSqlite("SaleInvoiceDetail", "Tax1", "Float", "0", False)
            AgL.AddFieldSqlite("SaleInvoiceDetail", "Tax2_Per", "Float", "0", False)
            AgL.AddFieldSqlite("SaleInvoiceDetail", "Tax2", "Float", "0", False)
            AgL.AddFieldSqlite("SaleInvoiceDetail", "Tax3_Per", "Float", "0", False)
            AgL.AddFieldSqlite("SaleInvoiceDetail", "Tax3", "Float", "0", False)
            AgL.AddFieldSqlite("SaleInvoiceDetail", "Tax4_Per", "Float", "0", False)
            AgL.AddFieldSqlite("SaleInvoiceDetail", "Tax4", "Float", "0", False)
            AgL.AddFieldSqlite("SaleInvoiceDetail", "Tax5_Per", "Float", "0", False)
            AgL.AddFieldSqlite("SaleInvoiceDetail", "Tax5", "Float", "0", False)
            AgL.AddFieldSqlite("SaleInvoiceDetail", "SubTotal1", "Float", "0", False)
            AgL.AddFieldSqlite("SaleInvoiceDetail", "Deduction_Per", "Float", "0", False)
            AgL.AddFieldSqlite("SaleInvoiceDetail", "Deduction", "Float", "0", False)
            AgL.AddFieldSqlite("SaleInvoiceDetail", "Other_Charge_Per", "Float", "0", False)
            AgL.AddFieldSqlite("SaleInvoiceDetail", "Other_Charge", "Float", "0", False)
            AgL.AddFieldSqlite("SaleInvoiceDetail", "SubTotal1", "Float", "0", False)
            AgL.AddFieldSqlite("SaleInvoiceDetail", "Round_Off", "Float", "0", False)
            AgL.AddFieldSqlite("SaleInvoiceDetail", "Net_Amount", "Float", "0", False)
            AgL.AddFieldSqlite("SaleInvoiceDetail", "UploadDate", "DateTime", "", True)
        Catch ex As Exception
            MsgBox(ex.Message & "  [FCreateTable_SaleInvoiceDetail]")
        End Try

    End Sub

    Private Sub FCreateTable_SaleInvoiceTransport()
        Dim mQry As String
        Try
            If Not AgL.IsTableExist("SaleInvoiceTransport", AgL.GcnMain) Then
                mQry = " CREATE TABLE [SaleInvoiceTransport] ([DocID] nVarchar(21) NOT NULL References SaleInvoice(DocId), 
                   PRIMARY KEY ([DocID]) ); "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If
            AgL.AddFieldSqlite("SaleInvoiceTransport", "Transporter", "nVarchar(10)", "", True, "References Subgroup(Subcode)")
            AgL.AddFieldSqlite("SaleInvoiceTransport", "LrNo", "nVarchar(50)", "", True)
            AgL.AddFieldSqlite("SaleInvoiceTransport", "LrDate", "DateTime", "", True)
            AgL.AddFieldSqlite("SaleInvoiceTransport", "PrivateMark", "nVarchar(50)", "", True)
            AgL.AddFieldSqlite("SaleInvoiceTransport", "Weight", "Float", "0", False)
            AgL.AddFieldSqlite("SaleInvoiceTransport", "Freight", "Float", "0", False)
            AgL.AddFieldSqlite("SaleInvoiceTransport", "PaymentType", "nVarchar(20)", "", True)
            AgL.AddFieldSqlite("SaleInvoiceTransport", "RoadPermitNo", "nVarchar(50)", "", True)
            AgL.AddFieldSqlite("SaleInvoiceTransport", "RoadPermitDate", "DateTime", "", True)
            AgL.AddFieldSqlite("SaleInvoiceTransport", "UploadDate", "DateTime", "", True)
        Catch ex As Exception
            MsgBox(ex.Message & "  [FCreateTable_SaleInvoiceTransport]")
        End Try

    End Sub


    Private Sub FCreateTable_SaleInvoiceDimensionDetail()
        Dim mQry As String
        Try
            If Not AgL.IsTableExist("SaleInvoiceDimensionDetail", AgL.GcnMain) Then
                mQry = " CREATE TABLE [SaleInvoiceDimensionDetail] ([DocID] nVarchar(21) NOT NULL, 
                   [TSr] int,
                   [Sr] int,
                   PRIMARY KEY ([DocID],[Tsr],[Sr]) ); "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If
            AgL.AddFieldSqlite("SaleInvoiceDimensionDetail", "Specification", "nVarchar(255)", "", True)
            AgL.AddFieldSqlite("SaleInvoiceDimensionDetail", "Pcs", "Float", "0", False)
            AgL.AddFieldSqlite("SaleInvoiceDimensionDetail", "Qty", "Float", "0", False)
            AgL.AddFieldSqlite("SaleInvoiceDimensionDetail", "TotalQty", "Float", "0", False)
            AgL.AddFieldSqlite("SaleInvoiceDimensionDetail", "Unit", "nVarchar(10)", "", True, "References Unit(Code)")
            AgL.AddFieldSqlite("SaleInvoiceDimensionDetail", "UploadDate", "DateTime", "", True)
        Catch ex As Exception
            MsgBox(ex.Message & "  [FCreateTable_SaleInvoiceDimensionDetail]")
        End Try

    End Sub


    Private Sub FCreateTable_Waybill()
        Dim mQry As String
        Try
            If Not AgL.IsTableExist("SaleInvoice_Waybill", AgL.GcnMain) Then
                mQry = " CREATE TABLE [SaleInvoice_Waybill] ([DocID] nVarchar(21) NOT NULL, 
                   [V_Type] nvarchar(5) References Voucher_Type(V_Type) COLLATE NOCASE,
                   [V_Prefix] nvarchar(5) COLLATE NOCASE,
                   [V_Date] datetime,
                   [V_No] bigint,
                   [Div_Code] nvarchar(1) References Division(Div_Code) COLLATE NOCASE,
                   [Site_Code] nvarchar(2) References SiteMast(Code) COLLATE NOCASE,
                   PRIMARY KEY ([DocID]) ); "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If
            AgL.AddFieldSqlite("SaleInvoice_Waybill", "ReferenceNo", "nVarchar(20)", "", True)
            AgL.AddFieldSqlite("SaleInvoice_Waybill", "SaleToParty", "nVarchar(10)", "", True, "References Subgroup(Subcode)")
            AgL.AddFieldSqlite("SaleInvoice_Waybill", "ReferenceDocId", "nVarchar(21)", "", True)
            AgL.AddFieldSqlite("SaleInvoice_Waybill", "Remarks", "nVarchar(255)", "", True)
            AgL.AddFieldSqlite("SaleInvoice_Waybill", "Transporter", "nVarchar(10)", "", True, "References Subgroup(Subcode)")
            AgL.AddFieldSqlite("SaleInvoice_Waybill", "Net_Amount", "Float", "0", False)
            AgL.AddFieldSqlite("SaleInvoice_Waybill", "Div_Code", "nVarchar(1)", "", True)
            AgL.AddFieldSqlite("SaleInvoice_Waybill", "Site_Code", "nVarchar(2)", "", True)
            AgL.AddFieldSqlite("SaleInvoice_Waybill", "Status", "nVarchar(20)", "", True)

            AgL.AddFieldSqlite("SaleInvoice_Waybill", "EntryBy", "nVarchar(10)", "", True)
            AgL.AddFieldSqlite("SaleInvoice_Waybill", "EntryDate", "DateTime", "", True)
            AgL.AddFieldSqlite("SaleInvoice_Waybill", "ApproveBy", "nVarchar(10)", "", True)
            AgL.AddFieldSqlite("SaleInvoice_Waybill", "ApproveDate", "DateTime", "", True)
            AgL.AddFieldSqlite("SaleInvoice_Waybill", "MoveToLog", "nVarchar(10)", "", True)
            AgL.AddFieldSqlite("SaleInvoice_Waybill", "MoveToLogDate", "DateTime", "", True)
            AgL.AddFieldSqlite("SaleInvoice_Waybill", "UploadDate", "DateTime", "", True)
        Catch ex As Exception
            MsgBox(ex.Message & "  [FCreateTable_Waybill]")
        End Try


    End Sub


    Private Sub FCreateTable_PurchInvoice()
        Dim mQry As String
        Try
            If Not AgL.IsTableExist("PurchInvoice", AgL.GcnMain) Then
                mQry = " CREATE TABLE [PurchInvoice] ([DocID] nVarchar(21) NOT NULL, 
                   [V_Type] nvarchar(5) References Voucher_Type(V_Type) COLLATE NOCASE,
                   [V_Prefix] nvarchar(5) COLLATE NOCASE,
                   [V_Date] datetime,
                   [V_No] bigint,
                   [Div_Code] nvarchar(1) References Division(Div_Code) COLLATE NOCASE,
                   [Site_Code] nvarchar(2) References SiteMast(Code) COLLATE NOCASE,
                   PRIMARY KEY ([DocID]) ); "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If
            AgL.AddFieldSqlite("PurchInvoice", "ReferenceNo", "nVarchar(20)", "", True)
            AgL.AddFieldSqlite("PurchInvoice", "Vendor", "nVarchar(10)", "", True, "References Subgroup(Subcode)")
            AgL.AddFieldSqlite("PurchInvoice", "BillToParty", "nVarchar(10)", "", True, "References Subgroup(Subcode)")
            AgL.AddFieldSqlite("PurchInvoice", "SalesTaxGroupParty", "nVarchar(20)", "", True)
            AgL.AddFieldSqlite("PurchInvoice", "PlaceOfSupply", "nVarchar(20)", "", True)
            AgL.AddFieldSqlite("PurchInvoice", "Structure", "nVarchar(8)", "", True, "References Structure (Code) ")
            AgL.AddFieldSqlite("PurchInvoice", "CustomFields", "nVarchar(8)", "", True, "References CustomFields (Code) ")
            AgL.AddFieldSqlite("PurchInvoice", "VendorDocNo", "nVarchar(50)", "", True)
            AgL.AddFieldSqlite("PurchInvoice", "VendorDocDate", "DateTime", "", True)
            AgL.AddFieldSqlite("PurchInvoice", "ReferenceDocId", "nVarchar(21)", "", True)
            AgL.AddFieldSqlite("PurchInvoice", "Remarks", "nVarchar(255)", "", True)
            AgL.AddFieldSqlite("PurchInvoice", "Process", "nVarchar(10)", "", True, "References Process(NCat)")
            AgL.AddFieldSqlite("PurchInvoice", "Agent", "nVarchar(10)", "", True, "References Subgroup(Subcode)")
            AgL.AddFieldSqlite("PurchInvoice", "VendorName", "nVarchar(100)", "", True)
            AgL.AddFieldSqlite("PurchInvoice", "VendorAddress", "nVarchar(100)", "", True)
            AgL.AddFieldSqlite("PurchInvoice", "VendorCity", "nVarchar(6)", "", True, "references City(CityCode)")
            AgL.AddFieldSqlite("PurchInvoice", "VendorMobile", "nVarchar(10)", "", True)
            AgL.AddFieldSqlite("PurchInvoice", "VendorSalesTaxNo", "nVarchar(50)", "", True)
            AgL.AddFieldSqlite("PurchInvoice", "Gross_Amount", "Float", "0", False)
            AgL.AddFieldSqlite("PurchInvoice", "Taxable_Amount", "Float", "0", False)
            AgL.AddFieldSqlite("PurchInvoice", "Tax1_Per", "Float", "0", False)
            AgL.AddFieldSqlite("PurchInvoice", "Tax1", "Float", "0", False)
            AgL.AddFieldSqlite("PurchInvoice", "Tax2_Per", "Float", "0", False)
            AgL.AddFieldSqlite("PurchInvoice", "Tax2", "Float", "0", False)
            AgL.AddFieldSqlite("PurchInvoice", "Tax3_Per", "Float", "0", False)
            AgL.AddFieldSqlite("PurchInvoice", "Tax3", "Float", "0", False)
            AgL.AddFieldSqlite("PurchInvoice", "Tax4_Per", "Float", "0", False)
            AgL.AddFieldSqlite("PurchInvoice", "Tax4", "Float", "0", False)
            AgL.AddFieldSqlite("PurchInvoice", "Tax5_Per", "Float", "0", False)
            AgL.AddFieldSqlite("PurchInvoice", "Tax5", "Float", "0", False)
            AgL.AddFieldSqlite("PurchInvoice", "SubTotal1", "Float", "0", False)
            AgL.AddFieldSqlite("PurchInvoice", "Deduction_Per", "Float", "0", False)
            AgL.AddFieldSqlite("PurchInvoice", "Deduction", "Float", "0", False)
            AgL.AddFieldSqlite("PurchInvoice", "Other_Charge_Per", "Float", "0", False)
            AgL.AddFieldSqlite("PurchInvoice", "Other_Charge", "Float", "0", False)
            AgL.AddFieldSqlite("PurchInvoice", "SubTotal1", "Float", "0", False)
            AgL.AddFieldSqlite("PurchInvoice", "Round_Off", "Float", "0", False)
            AgL.AddFieldSqlite("PurchInvoice", "Net_Amount", "Float", "0", False)
            AgL.AddFieldSqlite("PurchInvoice", "Div_Code", "nVarchar(1)", "", True)
            AgL.AddFieldSqlite("PurchInvoice", "Site_Code", "nVarchar(2)", "", True)
            AgL.AddFieldSqlite("PurchInvoice", "Status", "nVarchar(20)", "", True)

            AgL.AddFieldSqlite("PurchInvoice", "EntryBy", "nVarchar(10)", "", True)
            AgL.AddFieldSqlite("PurchInvoice", "EntryDate", "DateTime", "", True)
            AgL.AddFieldSqlite("PurchInvoice", "ApproveBy", "nVarchar(10)", "", True)
            AgL.AddFieldSqlite("PurchInvoice", "ApproveDate", "DateTime", "", True)
            AgL.AddFieldSqlite("PurchInvoice", "MoveToLog", "nVarchar(10)", "", True)
            AgL.AddFieldSqlite("PurchInvoice", "MoveToLogDate", "DateTime", "", True)
            AgL.AddFieldSqlite("PurchInvoice", "UploadDate", "DateTime", "", True)

        Catch ex As Exception
            MsgBox(ex.Message & "  [FCreateTable_PurchInvoice]")
        End Try

    End Sub

    Private Sub FCreateTable_PurchInvoiceTransport()
        Dim mQry As String
        Try
            If Not AgL.IsTableExist("PurchInvoiceTransport", AgL.GcnMain) Then
                mQry = " CREATE TABLE [PurchInvoiceTransport] ([DocID] nVarchar(21) NOT NULL References PurchInvoice(DocId), 
                   PRIMARY KEY ([DocID]) ); "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If
            AgL.AddFieldSqlite("PurchInvoiceTransport", "Transporter", "nVarchar(10)", "", True, "References Subgroup(Subcode)")
            AgL.AddFieldSqlite("PurchInvoiceTransport", "LrNo", "nVarchar(50)", "", True)
            AgL.AddFieldSqlite("PurchInvoiceTransport", "LrDate", "DateTime", "", True)
            AgL.AddFieldSqlite("PurchInvoiceTransport", "PrivateMark", "nVarchar(50)", "", True)
            AgL.AddFieldSqlite("PurchInvoiceTransport", "Weight", "Float", "0", False)
            AgL.AddFieldSqlite("PurchInvoiceTransport", "Freight", "Float", "0", False)
            AgL.AddFieldSqlite("PurchInvoiceTransport", "PaymentType", "nVarchar(20)", "", True)
            AgL.AddFieldSqlite("PurchInvoiceTransport", "UploadDate", "DateTime", "", True)
        Catch ex As Exception
            MsgBox(ex.Message & "  [FCreateTable_PurchInvoiceTransport]")
        End Try

    End Sub

    Private Sub FCreateTable_PurchInvoiceDetail()
        Dim mQry As String
        Try
            If Not AgL.IsTableExist("PurchInvoiceDetail", AgL.GcnMain) Then
                mQry = " CREATE TABLE [PurchInvoiceDetail] ([DocID] nVarchar(21) NOT NULL, 
                   [Sr] int,
                   PRIMARY KEY ([DocID],[Sr]) ); "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If
            AgL.AddFieldSqlite("PurchInvoiceDetail", "ReferenceNo", "nVarchar(20)", "", True)
            AgL.AddFieldSqlite("PurchInvoiceDetail", "Item", "nVarchar(10)", "", True, "References Item(Code)")
            AgL.AddFieldSqlite("PurchInvoiceDetail", "Specification", "nVarchar(255)", "", True)
            AgL.AddFieldSqlite("PurchInvoiceDetail", "Dimension1", "nVarchar(10)", "", True, "References Dimension1(Code)")
            AgL.AddFieldSqlite("PurchInvoiceDetail", "Dimension2", "nVarchar(10)", "", True, "References Dimension2(Code)")
            AgL.AddFieldSqlite("PurchInvoiceDetail", "Dimension3", "nVarchar(10)", "", True, "References Dimension3(Code)")
            AgL.AddFieldSqlite("PurchInvoiceDetail", "Dimension4", "nVarchar(10)", "", True, "References Dimension4(Code)")
            AgL.AddFieldSqlite("PurchInvoiceDetail", "SalesTaxGroupItem", "nVarchar(20)", "", True)
            AgL.AddFieldSqlite("PurchInvoiceDetail", "LotNo", "nVarchar(50)", "", True)
            AgL.AddFieldSqlite("PurchInvoiceDetail", "BaleNo", "nVarchar(50)", "", True)
            AgL.AddFieldSqlite("PurchInvoiceDetail", "Deal", "nVarchar(10)", "", True)
            AgL.AddFieldSqlite("PurchInvoiceDetail", "ExpiryDate", "DateTime", "", True)
            AgL.AddFieldSqlite("PurchInvoiceDetail", "LrNo", "nVarchar(50)", "", True)
            AgL.AddFieldSqlite("PurchInvoiceDetail", "LrDate", "DateTime", "", True)
            AgL.AddFieldSqlite("PurchInvoiceDetail", "DocQty", "Float", "0", False)
            AgL.AddFieldSqlite("PurchInvoiceDetail", "FreeQty", "Float", "0", False)
            AgL.AddFieldSqlite("PurchInvoiceDetail", "Qty", "Float", "0", False)
            AgL.AddFieldSqlite("PurchInvoiceDetail", "RejQty", "Float", "0", False)
            AgL.AddFieldSqlite("PurchInvoiceDetail", "Unit", "nVarchar(10)", "", True, "References Unit(Code)")
            AgL.AddFieldSqlite("PurchInvoiceDetail", "UnitMultiplier", "Float", "0", False)
            AgL.AddFieldSqlite("PurchInvoiceDetail", "DocDealQty", "Float", "0", False)
            AgL.AddFieldSqlite("PurchInvoiceDetail", "DealQty", "Float", "0", False)
            AgL.AddFieldSqlite("PurchInvoiceDetail", "RejDealQty", "Float", "0", False)
            AgL.AddFieldSqlite("PurchInvoiceDetail", "DealUnit", "nVarchar(10)", "", True, "References Unit(Code)")
            AgL.AddFieldSqlite("PurchInvoiceDetail", "Rate", "Float", "0", False)
            AgL.AddFieldSqlite("PurchInvoiceDetail", "MRP", "Float", "0", False)
            AgL.AddFieldSqlite("PurchInvoiceDetail", "DiscountPer", "Float", "0", False)
            AgL.AddFieldSqlite("PurchInvoiceDetail", "DiscountAmount", "Float", "0", False)
            AgL.AddFieldSqlite("PurchInvoiceDetail", "AdditionalDiscountPer", "Float", "0", False)
            AgL.AddFieldSqlite("PurchInvoiceDetail", "AdditionalDiscountAmount", "Float", "0", False)
            AgL.AddFieldSqlite("PurchInvoiceDetail", "Amount", "Float", "0", False)
            AgL.AddFieldSqlite("PurchInvoiceDetail", "ProfitMarginPer", "Float", "0", False)
            AgL.AddFieldSqlite("PurchInvoiceDetail", "Sale_Rate", "Float", "0", False)
            AgL.AddFieldSqlite("PurchInvoiceDetail", "ReferenceDocId", "nVarchar(21)", "", True)
            AgL.AddFieldSqlite("PurchInvoiceDetail", "ReferenceDocIdSr", "int", "", True)
            AgL.AddFieldSqlite("PurchInvoiceDetail", "PurchInvoice", "nVarchar(21)", "", True)
            AgL.AddFieldSqlite("PurchInvoiceDetail", "PurchInvoiceSr", "int", "", True)
            AgL.AddFieldSqlite("PurchInvoiceDetail", "Godown", "nVarchar(10)", "", True, "References Subgroup(Subcode)")
            AgL.AddFieldSqlite("PurchInvoiceDetail", "Remark", "nVarchar(255)", "", True)

            AgL.AddFieldSqlite("PurchInvoiceDetail", "Gross_Amount", "Float", "0", False)
            AgL.AddFieldSqlite("PurchInvoiceDetail", "Taxable_Amount", "Float", "0", False)
            AgL.AddFieldSqlite("PurchInvoiceDetail", "Tax1_Per", "Float", "0", False)
            AgL.AddFieldSqlite("PurchInvoiceDetail", "Tax1", "Float", "0", False)
            AgL.AddFieldSqlite("PurchInvoiceDetail", "Tax2_Per", "Float", "0", False)
            AgL.AddFieldSqlite("PurchInvoiceDetail", "Tax2", "Float", "0", False)
            AgL.AddFieldSqlite("PurchInvoiceDetail", "Tax3_Per", "Float", "0", False)
            AgL.AddFieldSqlite("PurchInvoiceDetail", "Tax3", "Float", "0", False)
            AgL.AddFieldSqlite("PurchInvoiceDetail", "Tax4_Per", "Float", "0", False)
            AgL.AddFieldSqlite("PurchInvoiceDetail", "Tax4", "Float", "0", False)
            AgL.AddFieldSqlite("PurchInvoiceDetail", "Tax5_Per", "Float", "0", False)
            AgL.AddFieldSqlite("PurchInvoiceDetail", "Tax5", "Float", "0", False)
            AgL.AddFieldSqlite("PurchInvoiceDetail", "SubTotal1", "Float", "0", False)
            AgL.AddFieldSqlite("PurchInvoiceDetail", "Deduction_Per", "Float", "0", False)
            AgL.AddFieldSqlite("PurchInvoiceDetail", "Deduction", "Float", "0", False)
            AgL.AddFieldSqlite("PurchInvoiceDetail", "Other_Charge_Per", "Float", "0", False)
            AgL.AddFieldSqlite("PurchInvoiceDetail", "Other_Charge", "Float", "0", False)
            AgL.AddFieldSqlite("PurchInvoiceDetail", "SubTotal1", "Float", "0", False)
            AgL.AddFieldSqlite("PurchInvoiceDetail", "Round_Off", "Float", "0", False)
            AgL.AddFieldSqlite("PurchInvoiceDetail", "Net_Amount", "Float", "0", False)
            AgL.AddFieldSqlite("PurchInvoiceDetail", "UploadDate", "DateTime", "", True)
        Catch ex As Exception
            MsgBox(ex.Message & "  [FCreateTable_PurchInvoiceDetail]")
        End Try

    End Sub
    Private Sub FCreateTable_PostingGroupSalesTaxParty()
        Dim mQry As String
        Try
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
        Catch ex As Exception
            MsgBox(ex.Message & "  [FCreateTable_PostingGroupSalesTaxParty]")
        End Try

    End Sub

    Private Sub FCreateTable_RateListDetail()
        Dim mQry As String
        Try
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
        Catch ex As Exception
            MsgBox(ex.Message & "  [FCreateTable_RateListDetail]")
        End Try

    End Sub

    Private Sub FCreateTable_Process()
        Dim mQry As String
        Try
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
        Catch ex As Exception
            MsgBox(ex.Message & "  [FCreateTable_Process]")
        End Try

    End Sub

    Private Sub FCreateTable_RateList()
        Dim mQry As String
        Try
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
        Catch ex As Exception
            MsgBox(ex.Message & "  [FCreateTable_RateList]")
        End Try

    End Sub

    Private Sub FCreateTable_RateType()
        Dim mQry As String
        Try
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
        Catch ex As Exception
            MsgBox(ex.Message & "  [FCreateTable_RateType]")
        End Try

    End Sub

    Private Sub FCreateTable_Ledger()
        Dim mQry As String
        Try
            If Not AgL.IsTableExist("Ledger", AgL.GcnMain) Then
                mQry = "
                    CREATE TABLE [Ledger] (
                       [DocId] nvarchar(21) NOT NULL COLLATE NOCASE,
                       [V_SNo] int NOT NULL,
                       [V_No] bigint,
                       [V_Type] nvarchar(5) COLLATE NOCASE,
                       [V_Prefix] nvarchar(5) COLLATE NOCASE,
                       [V_Date] datetime,
                       [SubCode] nvarchar(10) COLLATE NOCASE,
                       [ContraSub] nvarchar(10) COLLATE NOCASE,
                       [AmtDr] float,
                       [AmtCr] float,
                       [Chq_No] nvarchar(20) COLLATE NOCASE,
                       [Chq_Date] datetime,
                       [Clg_Date] datetime,
                       [TDSCategory] nvarchar(6) COLLATE NOCASE,
                       [TdsDesc] nvarchar(4) COLLATE NOCASE,
                       [TdsOnAmt] float,
                       [TdsPer] float,
                       [Tds_Of_V_Sno] int,
                       [Narration] nvarchar(255) COLLATE NOCASE,
                       [Site_Code] nvarchar(2) COLLATE NOCASE,
                       [U_Name] nvarchar(10) COLLATE NOCASE,
                       [U_EntDt] datetime,
                       [U_AE] nvarchar(1) COLLATE NOCASE,
                       [DivCode] nvarchar(1) COLLATE NOCASE,
                       [PQty] float,
                       [SQty] float,
                       [AgRefNo] nvarchar(21) COLLATE NOCASE,
                       [GroupCode] nvarchar(4) COLLATE NOCASE,
                       [GroupNature] nvarchar(30) COLLATE NOCASE,
                       [RowId] bigint,
                       [UpLoadDate] datetime,
                       [AddBy] nvarchar(10) COLLATE NOCASE,
                       [AddDate] datetime,
                       [ModifyBy] nvarchar(10) COLLATE NOCASE,
                       [ModifyDate] datetime,
                       [ApprovedBy] nvarchar(10) COLLATE NOCASE,
                       [ApprovedDate] datetime,
                       [GPX1] nvarchar(255) COLLATE NOCASE,
                       [GPX2] nvarchar(255) COLLATE NOCASE,
                       [GPN1] float,
                       [GPN2] float,
                       [OldDocid] nvarchar(21) COLLATE NOCASE,
                       [CostCenter] nvarchar(10) COLLATE NOCASE,
                       [System_Generated] nvarchar(1) COLLATE NOCASE,
                       [FarmulaString] nvarchar(100) COLLATE NOCASE,
                       [ContraText] varchar(2147483647) COLLATE NOCASE,
                       [RecId] nvarchar(20) COLLATE NOCASE,
                       [FormulaString] nvarchar(100) COLLATE NOCASE,
                       [OrignalAmt] float,
                       [TDSDeductFrom] nvarchar(10) COLLATE NOCASE,
                       [ReferenceDocId] nvarchar(21) COLLATE NOCASE,
                       [ReferenceDocIdSr] int,
                       [CreditDays] float,
                       PRIMARY KEY ([DocId], [V_SNo]),
                       CONSTRAINT [FK_Ledger_SubGroup_ContraSub] FOREIGN KEY ([ContraSub])
                          REFERENCES [SubGroup]([SubCode]) ON DELETE NO ACTION ON UPDATE NO ACTION,
                       CONSTRAINT [FK_Ledger_AcGroup_GroupCode] FOREIGN KEY ([GroupCode])
                          REFERENCES [AcGroup]([GroupCode]) ON DELETE NO ACTION ON UPDATE NO ACTION,
                       CONSTRAINT [FK_Ledger_SiteMast_Site_Code] FOREIGN KEY ([Site_Code])
                          REFERENCES [SiteMast]([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION,
                       CONSTRAINT [FK_Ledger_SubGroup_SubCode] FOREIGN KEY ([SubCode])
                          REFERENCES [SubGroup]([SubCode]) ON DELETE NO ACTION ON UPDATE NO ACTION,
                       CONSTRAINT [FK_Ledger_Voucher_Type_V_Type] FOREIGN KEY ([V_Type])
                          REFERENCES [Voucher_Type]([V_Type]) ON DELETE NO ACTION ON UPDATE NO ACTION
                    );

                "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If
        Catch ex As Exception
            MsgBox(ex.Message & "  [FCreateTable_Ledger]")
        End Try

    End Sub


    Private Sub FCreateTable_PostingGroupSalesTax()
        Dim mQry As String
        Try
            If Not AgL.IsTableExist("PostingGroupSalesTax", AgL.GcnMain) Then
                mQry = "
                CREATE TABLE [PostingGroupSalesTax] (
                   [Site_Code] nvarchar(2) NOT NULL COLLATE NOCASE,
                   [Div_Code] nvarchar(1) NOT NULL COLLATE NOCASE,
                   [PostingGroupSalesTaxItem] nvarchar(20) NOT NULL COLLATE NOCASE,
                   [PostingGroupSalesTaxParty] nvarchar(20) NOT NULL COLLATE NOCASE,
                   [WEF] datetime NOT NULL,
                   [PurchaseSaleAc] nvarchar(10) COLLATE NOCASE,
                   [SalesTax] float,
                   [SalesTaxAc] nvarchar(10) COLLATE NOCASE,
                   [VAT] float,
                   [VatAc] nvarchar(10) COLLATE NOCASE,
                   [AdditionalTax] float,
                   [AdditionalTaxAc] nvarchar(10) COLLATE NOCASE,
                   [Cst] float,
                   [CstAc] nvarchar(10) COLLATE NOCASE,
                   [CustomDuty] float,
                   [CustomDutyAc] nvarchar(10) COLLATE NOCASE,
                   [CustomDutyECess] float,
                   [CustomDutyECessAc] nvarchar(10) COLLATE NOCASE,
                   [CustomDutyHECess] float,
                   [CustomDutyHECessAc] nvarchar(10) COLLATE NOCASE,
                   [CustomAdditionalDuty] float,
                   [CustomAdditionalDutyAc] nvarchar(10) COLLATE NOCASE,
                   [PurchaseAc] nvarchar(10) COLLATE NOCASE,
                   [SalesAc] nvarchar(10) COLLATE NOCASE,
                   [SalesTaxOnPurchaseAc] nvarchar(10) COLLATE NOCASE,
                   [SalesTaxOnSalesAc] nvarchar(10) COLLATE NOCASE,
                   [AdditionalTaxOnPurchaseAc] nvarchar(10) COLLATE NOCASE,
                   [AdditionalTaxOnSalesAc] nvarchar(10) COLLATE NOCASE,                   
                   CONSTRAINT [FK_PostingGroupSalesTax_SubGroup_AdditionalTaxOnPurchaseAc] FOREIGN KEY ([AdditionalTaxOnPurchaseAc])
                      REFERENCES [SubGroup]([SubCode]) ON DELETE NO ACTION ON UPDATE NO ACTION,
                   CONSTRAINT [FK_PostingGroupSalesTax_SubGroup_AdditionalTaxOnSalesAc] FOREIGN KEY ([AdditionalTaxOnSalesAc])
                      REFERENCES [SubGroup]([SubCode]) ON DELETE NO ACTION ON UPDATE NO ACTION,
                   CONSTRAINT [FK_PostingGroupSalesTax_Division_Div_Code] FOREIGN KEY ([Div_Code])
                      REFERENCES [Division]([Div_Code]) ON DELETE NO ACTION ON UPDATE NO ACTION,
                   CONSTRAINT [FK_PostingGroupSalesTax_PostingGroupSalesTaxItem_PostingGroupSalesTaxItem] FOREIGN KEY ([PostingGroupSalesTaxItem])
                      REFERENCES [PostingGroupSalesTaxItem]([Description]) ON DELETE NO ACTION ON UPDATE NO ACTION,
                   CONSTRAINT [FK_PostingGroupSalesTax_PostingGroupSalesTaxParty_PostingGroupSalesTaxParty] FOREIGN KEY ([PostingGroupSalesTaxParty])
                      REFERENCES [PostingGroupSalesTaxParty]([Description]) ON DELETE NO ACTION ON UPDATE NO ACTION,
                   CONSTRAINT [FK_PostingGroupSalesTax_SubGroup_PurchaseAc] FOREIGN KEY ([PurchaseAc])
                      REFERENCES [SubGroup]([SubCode]) ON DELETE NO ACTION ON UPDATE NO ACTION,
                   CONSTRAINT [FK_PostingGroupSalesTax_SubGroup_SalesAc] FOREIGN KEY ([SalesAc])
                      REFERENCES [SubGroup]([SubCode]) ON DELETE NO ACTION ON UPDATE NO ACTION,
                   CONSTRAINT [FK_PostingGroupSalesTax_SubGroup_SalesTaxOnPurchaseAc] FOREIGN KEY ([SalesTaxOnPurchaseAc])
                      REFERENCES [SubGroup]([SubCode]) ON DELETE NO ACTION ON UPDATE NO ACTION,
                   CONSTRAINT [FK_PostingGroupSalesTax_SubGroup_SalesTaxOnSalesAc] FOREIGN KEY ([SalesTaxOnSalesAc])
                      REFERENCES [SubGroup]([SubCode]) ON DELETE NO ACTION ON UPDATE NO ACTION,
                   CONSTRAINT [FK_PostingGroupSalesTax_SiteMast_Site_Code] FOREIGN KEY ([Site_Code])
                      REFERENCES [SiteMast]([Code]) ON DELETE NO ACTION ON UPDATE NO ACTION
                );
                "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
                'PRIMARY KEY ([Site_Code], [Div_Code], [PostingGroupSalesTaxItem], [PostingGroupSalesTaxParty],[PlaceOfSupply],[Process],[ChargeType], [WEF]),
            End If
            AgL.AddFieldSqlite("PostingGroupSalesTax", "PlaceOfSupply", "nVarchar(20)", "", True)
            AgL.AddFieldSqlite("PostingGroupSalesTax", "Process", "nVarchar(10)", "", True)
            AgL.AddFieldSqlite("PostingGroupSalesTax", "ChargeType", "nVarchar(30)", "", True)
            AgL.AddFieldSqlite("PostingGroupSalesTax", "Percentage", "Float", "", True)
            AgL.AddFieldSqlite("PostingGroupSalesTax", "LedgerAc", "nVarchar(10)", "", True)

        Catch ex As Exception
            MsgBox(ex.Message & "  [FCreateTable_PostingGroupSalesTax]")
        End Try

    End Sub

    Private Sub FCreateTable_StockAdj()
        Dim mQry As String
        Try
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
        Catch ex As Exception
            MsgBox(ex.Message & "  [FCreateTable_StockAdj]")
        End Try

    End Sub

    Private Sub FCreateTable_Structure()
        Dim mQry As String
        Try
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
        Catch ex As Exception
            MsgBox(ex.Message & "  [FCreateTable_Structure]")
        End Try


    End Sub

    Private Sub FCreateTable_StructureDetail()
        Dim mQry As String
        Try
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
            AgL.AddFieldSqlite("StructureDetail", "LineItem", "Bit", "0", True)
            AgL.AddFieldSqlite("StructureDetail", "AffectCost", "Bit", "0", True)
            AgL.AddFieldSqlite("StructureDetail", "InactiveDate", "DateTime", "", True)
            AgL.AddFieldSqlite("StructureDetail", "Percentage", "Float", "0", True)
            AgL.AddFieldSqlite("StructureDetail", "Amount", "Float", "0", True)
            AgL.AddFieldSqlite("StructureDetail", "VisibleInMaster", "Bit", "0", True)
            AgL.AddFieldSqlite("StructureDetail", "VisibleInMasterLine", "Bit", "0", True)
            AgL.AddFieldSqlite("StructureDetail", "VisibleInTransactionLine", "Bit", "0", True)
            AgL.AddFieldSqlite("StructureDetail", "VisibleInTransactionFooter", "Bit", "0", True)
            AgL.AddFieldSqlite("StructureDetail", "HeaderPerField", "nVarchar(50)", "", True)
            AgL.AddFieldSqlite("StructureDetail", "HeaderAmtField", "nVarchar(50)", "", True)
            AgL.AddFieldSqlite("StructureDetail", "LinePerField", "nVarchar(50)", "", True)
            AgL.AddFieldSqlite("StructureDetail", "LineAmtField", "nVarchar(50)", "", True)
            AgL.AddFieldSqlite("StructureDetail", "GridDisplayIndex", "Int", "0", True)
            AgL.AddFieldSqlite("StructureDetail", "UploadDate", "DateTime", "", True)
            AgL.AddFieldSqlite("StructureDetail", "Active", "nVarchar(10)", "", True)
            AgL.AddFieldSqlite("StructureDetail", "InactiveDate", "DateTime", "", True)
        Catch ex As Exception
            MsgBox(ex.Message & "  [FCreateTable_StructureDetail]")
        End Try

    End Sub
    Private Sub FSeedTable_Enviro()
        Dim mQry As String
        Try
            If AgL.FillData("Select * from Enviro limit 1", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " INSERT INTO Enviro
                    (ID, Site_Code, Div_Code, CashAc, BankAc, TdsAc, AdditionAc, DeductionAc, ServiceTaxAc, ECessAc, RoundOffAc, HECessAc, ServiceTaxPer, ECessPer, HECessPer, RowId, UpLoadDate, PreparedBy, U_EntDt, U_AE, Edit_Date, ModifiedBy, ApprovedBy, ApprovedDate, GPX1, GPX2, GPN1, GPN2, DefaultSalesTaxGroupParty, DefaultSalesTaxGroupItem, PurchOrderShowIndentInLine, IsLinkWithFA, IsNegativeStockAllowed, IsLotNoApplicable, DefaultDueDays, IsNegetiveStockAllowed, SaleAc, PostingAc, PurchaseAc, DefaultCurrency, DefaultVatCommodityCode, IsVisible_PurchOrder, IsVisible_PurchChallan, Caption_Dimension1, Caption_Dimension2, UrgentList, UrgentItemList)
                    VALUES('1', '1', 'D', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'Unregistered', 'GST 5%', 0, NULL, 1, 1, NULL, NULL, 'Sale', '111', NULL, 'Rs.', '2A079001', NULL, NULL, 'D1', 'D2', NULL, NULL);
                "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " [FSeedTable_Enviro]")
        End Try

    End Sub

    Private Sub FCreateTable_Unit()
        Dim mQry As String
        Try
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

            AgL.AddFieldSqlite("Unit", "ShowDimensionDetailInPurchase", "bit", "0", True)
            AgL.AddFieldSqlite("Unit", "ShowDimensionDetailInSales", "bit", "0", True)

        Catch ex As Exception
            MsgBox(ex.Message & " [FCreateTable_Unit]")
        End Try
    End Sub

    Private Sub FSeedTable_Unit()
        Dim mQry As String
        Try
            If AgL.FillData("Select * from Unit limit 1", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    INSERT INTO Unit
                    (Code, IsActive, DecimalPlaces,ShowDimensionDetailInPurchase, ShowDimensionDetailInSales)
                    VALUES('Kg', 1, 1,0,0);
                    INSERT INTO Unit
                    (Code, IsActive, DecimalPlaces,ShowDimensionDetailInPurchase, ShowDimensionDetailInSales)
                    VALUES('Meter', 1, 1,1,1);
                    INSERT INTO Unit
                    (Code, IsActive, DecimalPlaces, ShowDimensionDetailInPurchase, ShowDimensionDetailInSales)
                    VALUES('Pcs', 1, 1,0,0);
                   "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " [FSeedTable_Unit]")
        End Try

    End Sub

    Private Sub FCreateTable_Department()
        Dim mQry As String
        Try
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
        Catch ex As Exception
            MsgBox(ex.Message & "  [FCreateTable_Department]")
        End Try

    End Sub

    Private Sub FCreateTable_ItemType()
        Dim mQry As String
        Try
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
        Catch ex As Exception
            MsgBox(ex.Message & "  [FCreateTable_ItemType]")
        End Try

    End Sub

    Private Sub FSeedTable_ItemType()
        Dim mQry As String
        Try
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
        Catch ex As Exception
            MsgBox(ex.Message & "  [FSeedTable_ItemType]")
        End Try

    End Sub

    Private Sub FCreateTable_ItemCategory()
        Dim mQry As String
        Try
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

                mQry = "
                    INSERT INTO ItemCategory
                    (Code, Description, ItemType, EntryBy, EntryDate, EntryType, EntryStatus, ApproveBy, ApproveDate, MoveToLog, MoveToLogDate, IsDeleted, Status, Div_Code, PreparedBy, U_EntDt, U_AE, Edit_Date, ModifiedBy, UID, IsSystemDefine, SalesTaxGroup, Unit, Department, HSN)
                    VALUES('N/A', 'N/A', 'FM', 'SUPER', '2018-02-26', 'Add', 'Open', NULL, NULL, NULL, NULL, NULL, 'Active', 'D', NULL, NULL, NULL, NULL, NULL, NULL, 1, 'GST 5%', 'Pcs', NULL, '1');
                   "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If
        Catch ex As Exception
            MsgBox(ex.Message & "  [FCreateTable_ItemCategory]")
        End Try

    End Sub

    Private Sub FCreateTable_ItemGroup()
        Dim mQry As String
        Try
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

                mQry = "
                    INSERT INTO ItemGroup
                    (Code, Description, ItemType, ItemCategory, EntryBy, EntryDate, EntryType, EntryStatus, ApproveBy, ApproveDate, MoveToLog, MoveToLogDate, IsDeleted, Status, Div_Code, PreparedBy, U_EntDt, U_AE, Edit_Date, ModifiedBy, UID, IsSystemDefine)
                    VALUES('N/A', 'N/A', 'FM', 'N/A', 'SUPER', '2018-02-26', 'Add', 'Open', NULL, NULL, NULL, NULL, NULL, 'Active', 'D', NULL, NULL, NULL, NULL, NULL, NULL, 1);
                   "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)

            End If
        Catch ex As Exception
            MsgBox(ex.Message & "  [FCreateTable_ItemGroup]")
        End Try

    End Sub

    Private Sub FCreateTable_Item()
        Dim mQry As String
        Try
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

                mQry = "
                    INSERT INTO Item
                    (Code, ManualCode, Description, DisplayName, Unit, Measure, MeasureUnit, ItemGroup, ItemCategory, ItemType, Godown, GodownSection, QcGroup, CurrentStock, CurrentIssued, CurrentRequisition, IsDeleted, UpcCode, Bom, Rate, ItemImportExportGroup, EntryBy, EntryDate, EntryType, EntryStatus, ApproveBy, ApproveDate, MoveToLog, MoveToLogDate, Status, Div_Code, UID, SalesTaxPostingGroup, ExcisePostingGroup, EntryTaxPostingGroup, LastPurchaseRate, LastPurchaseDate, LastPurchaseInvoice, Specification, ProcessSequence, ItemInvoiceGroup, StockYN, StockOn, PcsPerMeasure, Prod_Measure, Colour, Quality, Construction, Collection, BillingOn, Manufacturer, VatCommodityCode, ReorderLevel, Design, Size, Deal, ProfitMarginPer, ServiceTaxYN, DeliveryMeasure, TariffHead, ItemNature, ProcessList, ProdBatchQty, ProdBatchUnit, SubCode, CostCenter, CustomFields, GenTable, GenCode, Operators_Required, Gross_Weight, IsSystemDefine, IsRestricted_InTransaction, IsMandatory_UnitConversion, HSN)
                    VALUES('1stItem', '1', 'Sample Item', NULL, 'Pcs', NULL, NULL, 'N/A', 'N/A', 'FM', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 100.0, NULL, 'SUPER', '2018-02-26', 'Add', 'Open', NULL, NULL, NULL, NULL, 'Active', 'D', NULL, 'GST 5%', NULL, NULL, NULL, NULL, NULL, 'Sample', NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'N', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0.0, 0, 0, 0, '1');
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If
        Catch ex As Exception
            MsgBox(ex.Message & "  [FCreateTable_Item")
        End Try

    End Sub

    Private Sub FCreateTable_SubGroupType()
        Dim mQry As String

        Try
            If Not AgL.IsTableExist("SubGroupType", AgL.GcnMain) Then
                mQry = "
                        CREATE TABLE [SubGroupType] (
                           [SubgroupType] nVarchar(20) NOT NULL,
                           [IsActive] bit default 0 Not Null, 
                           [UpLoadDate] datetime,
                           PRIMARY KEY ([SubgroupType])
                        );
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " [FCreateTable_SubgroupType]")
        End Try
    End Sub

    Private Sub FCreateTable_CostCenterMast()
        Dim mQry As String
        Try
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
        Catch ex As Exception
            MsgBox(ex.Message & "  [FCreateTable_CostCenterMast]")
        End Try

    End Sub

    Private Sub FCreateTable_AcGroup()
        Dim mQry As String
        Try
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
        Catch ex As Exception
            MsgBox(ex.Message & "  [FCreateTable_AcGroup]")
        End Try

    End Sub

    Private Sub FSeedTable_AcGroup()
        Dim mQry As String
        Try

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

            If AgL.FillData("Select * from AcGroup Where GroupName='Employee'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    Insert Into AcGroup (GroupCode, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE)
                    Values('0031', 'Employee', 'Employee', '0016','L', 'Supplier','N', 'SUPER','2018-03-01','A')
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If

            If AgL.FillData("Select * from AcGroup Where GroupName='GST'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                        Insert Into AcGroup (GroupCode, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE)
                        Values('0032', 'GST', 'GST', '0014','L', 'Expenses','N', 'SUPER','2018-03-01','A')
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If

            If AgL.FillData("Select * from AcGroup Where GroupName='Transporter'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                        Insert Into AcGroup (GroupCode, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE)
                        Values('0033', 'Transporter', 'Transporter', '0016','L', 'Supplier','N', 'SUPER','2018-03-01','A')
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If

            If AgL.FillData("Select * from AcGroup Where GroupName='Agent'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    Insert Into AcGroup (GroupCode, GroupName, ContraGroupName, GroupUnder, GroupNature, Nature, SysGroup, U_Name, U_EntDt, U_AE)
                    Values('0034', 'Agent', 'Agent', '0016','L', 'Supplier','N', 'SUPER','2018-03-01','A')
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If
        Catch ex As Exception
            MsgBox(ex.Message & " [FSeedTable_AcGroup] ")
        End Try


    End Sub

    Private Sub FSeedTable_PostingGroupSalesTax()
        Dim mQry As String
        Try
            If AgL.FillData("Select * from PostingGroupSalesTax limit 1", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    insert into PostingGroupSalesTax(WEF,Site_Code,Div_Code,PostingGroupSalesTaxItem, PostingGroupSalesTaxParty,ChargeType, Process, PlaceOfSupply, Percentage,LedgerAc)
                    VALUES('2017-04-01', '1','D','GST 0%','Unregistered','TAXABLE AMOUNT','PURCH','Within State',Null,'PURWU0');
                    insert into PostingGroupSalesTax(WEF,Site_Code,Div_Code,PostingGroupSalesTaxItem, PostingGroupSalesTaxParty,ChargeType, Process, PlaceOfSupply, Percentage,LedgerAc)
                    VALUES('2017-04-01', '1','D','GST 0%','Unregistered','TAXABLE AMOUNT','PURCH','Outside State',Null,'PUROU0');
                    insert into PostingGroupSalesTax(WEF,Site_Code,Div_Code,PostingGroupSalesTaxItem, PostingGroupSalesTaxParty,ChargeType, Process, PlaceOfSupply, Percentage,LedgerAc)
                    VALUES('2017-04-01', '1','D','GST 0%','Registered','TAXABLE AMOUNT','PURCH','Outside State',Null,'PUROR0');
                    insert into PostingGroupSalesTax(WEF,Site_Code,Div_Code,PostingGroupSalesTaxItem, PostingGroupSalesTaxParty,ChargeType, Process, PlaceOfSupply, Percentage,LedgerAc)
                    VALUES('2017-04-01', '1','D','GST 0%','Registered','TAXABLE AMOUNT','PURCH','Within State',Null,'PURWR0');


                    insert into PostingGroupSalesTax(WEF,Site_Code,Div_Code,PostingGroupSalesTaxItem, PostingGroupSalesTaxParty,ChargeType, Process, PlaceOfSupply, Percentage,LedgerAc)
                    VALUES('2017-04-01', '1','D','GST 5%','Unregistered','TAXABLE AMOUNT','PURCH','Within State',Null,'PURWU5');
                    insert into PostingGroupSalesTax(WEF,Site_Code,Div_Code,PostingGroupSalesTaxItem, PostingGroupSalesTaxParty,ChargeType, Process, PlaceOfSupply, Percentage,LedgerAc)
                    VALUES('2017-04-01', '1','D','GST 5%','Unregistered','TAXABLE AMOUNT','PURCH','Outside State',Null,'PUROU5');
                    insert into PostingGroupSalesTax(WEF,Site_Code,Div_Code,PostingGroupSalesTaxItem, PostingGroupSalesTaxParty,ChargeType, Process, PlaceOfSupply, Percentage,LedgerAc)
                    VALUES('2017-04-01', '1','D','GST 5%','Registered','TAXABLE AMOUNT','PURCH','Outside State',Null,'PUROR5');
                    insert into PostingGroupSalesTax(WEF,Site_Code,Div_Code,PostingGroupSalesTaxItem, PostingGroupSalesTaxParty,ChargeType, Process, PlaceOfSupply, Percentage,LedgerAc)
                    VALUES('2017-04-01', '1','D','GST 5%','Registered','TAXABLE AMOUNT','PURCH','Within State',Null,'PURWR5');
                    insert into PostingGroupSalesTax(WEF,Site_Code,Div_Code,PostingGroupSalesTaxItem, PostingGroupSalesTaxParty,ChargeType, Process, PlaceOfSupply, Percentage,LedgerAc)
                    VALUES('2017-04-01', '1','D','GST 5%','Registered','TAX1','PURCH','Outside State',5,'IIGST5');
                    insert into PostingGroupSalesTax(WEF,Site_Code,Div_Code,PostingGroupSalesTaxItem, PostingGroupSalesTaxParty,ChargeType, Process, PlaceOfSupply, Percentage,LedgerAc)
                    VALUES('2017-04-01', '1','D','GST 5%','Registered','TAX2','PURCH','Within State',2.5,'ICGST5');
                    insert into PostingGroupSalesTax(WEF,Site_Code,Div_Code,PostingGroupSalesTaxItem, PostingGroupSalesTaxParty,ChargeType, Process, PlaceOfSupply, Percentage,LedgerAc)
                    VALUES('2017-04-01', '1','D','GST 5%','Registered','TAX3','PURCH','Within State',2.5,'ISGST5');


                    insert into PostingGroupSalesTax(WEF,Site_Code,Div_Code,PostingGroupSalesTaxItem, PostingGroupSalesTaxParty,ChargeType, Process, PlaceOfSupply, Percentage,LedgerAc)
                    VALUES('2017-04-01', '1','D','GST 12%','Unregistered','TAXABLE AMOUNT','PURCH','Within State',Null,'PURWU12');
                    insert into PostingGroupSalesTax(WEF,Site_Code,Div_Code,PostingGroupSalesTaxItem, PostingGroupSalesTaxParty,ChargeType, Process, PlaceOfSupply, Percentage,LedgerAc)
                    VALUES('2017-04-01', '1','D','GST 12%','Unregistered','TAXABLE AMOUNT','PURCH','Outside State',Null,'PUROU12');
                    insert into PostingGroupSalesTax(WEF,Site_Code,Div_Code,PostingGroupSalesTaxItem, PostingGroupSalesTaxParty,ChargeType, Process, PlaceOfSupply, Percentage,LedgerAc)
                    VALUES('2017-04-01', '1','D','GST 12%','Registered','TAXABLE AMOUNT','PURCH','Outside State',Null,'PUROR12');
                    insert into PostingGroupSalesTax(WEF,Site_Code,Div_Code,PostingGroupSalesTaxItem, PostingGroupSalesTaxParty,ChargeType, Process, PlaceOfSupply, Percentage,LedgerAc)
                    VALUES('2017-04-01', '1','D','GST 12%','Registered','TAXABLE AMOUNT','PURCH','Within State',Null,'PURWR12');
                    insert into PostingGroupSalesTax(WEF,Site_Code,Div_Code,PostingGroupSalesTaxItem, PostingGroupSalesTaxParty,ChargeType, Process, PlaceOfSupply, Percentage,LedgerAc)
                    VALUES('2017-04-01', '1','D','GST 12%','Registered','TAX1','PURCH','Outside State',12,'IIGST12');
                    insert into PostingGroupSalesTax(WEF,Site_Code,Div_Code,PostingGroupSalesTaxItem, PostingGroupSalesTaxParty,ChargeType, Process, PlaceOfSupply, Percentage,LedgerAc)
                    VALUES('2017-04-01', '1','D','GST 12%','Registered','TAX2','PURCH','Within State',6,'ICGST12');
                    insert into PostingGroupSalesTax(WEF,Site_Code,Div_Code,PostingGroupSalesTaxItem, PostingGroupSalesTaxParty,ChargeType, Process, PlaceOfSupply, Percentage,LedgerAc)
                    VALUES('2017-04-01', '1','D','GST 12%','Registered','TAX3','PURCH','Within State',6,'ISGST12');


                    insert into PostingGroupSalesTax(WEF,Site_Code,Div_Code,PostingGroupSalesTaxItem, PostingGroupSalesTaxParty,ChargeType, Process, PlaceOfSupply, Percentage,LedgerAc)
                    VALUES('2017-04-01', '1','D','GST 18%','Unregistered','TAXABLE AMOUNT','PURCH','Within State',Null,'PURWU18');
                    insert into PostingGroupSalesTax(WEF,Site_Code,Div_Code,PostingGroupSalesTaxItem, PostingGroupSalesTaxParty,ChargeType, Process, PlaceOfSupply, Percentage,LedgerAc)
                    VALUES('2017-04-01', '1','D','GST 18%','Unregistered','TAXABLE AMOUNT','PURCH','Outside State',Null,'PUROU18');
                    insert into PostingGroupSalesTax(WEF,Site_Code,Div_Code,PostingGroupSalesTaxItem, PostingGroupSalesTaxParty,ChargeType, Process, PlaceOfSupply, Percentage,LedgerAc)
                    VALUES('2017-04-01', '1','D','GST 18%','Registered','TAXABLE AMOUNT','PURCH','Outside State',Null,'PUROR18');
                    insert into PostingGroupSalesTax(WEF,Site_Code,Div_Code,PostingGroupSalesTaxItem, PostingGroupSalesTaxParty,ChargeType, Process, PlaceOfSupply, Percentage,LedgerAc)
                    VALUES('2017-04-01', '1','D','GST 18%','Registered','TAXABLE AMOUNT','PURCH','Within State',Null,'PURWR18');
                    insert into PostingGroupSalesTax(WEF,Site_Code,Div_Code,PostingGroupSalesTaxItem, PostingGroupSalesTaxParty,ChargeType, Process, PlaceOfSupply, Percentage,LedgerAc)
                    VALUES('2017-04-01', '1','D','GST 18%','Registered','TAX1','PURCH','Outside State',18,'IIGST18');
                    insert into PostingGroupSalesTax(WEF,Site_Code,Div_Code,PostingGroupSalesTaxItem, PostingGroupSalesTaxParty,ChargeType, Process, PlaceOfSupply, Percentage,LedgerAc)
                    VALUES('2017-04-01', '1','D','GST 18%','Registered','TAX2','PURCH','Within State',9,'ICGST18');
                    insert into PostingGroupSalesTax(WEF,Site_Code,Div_Code,PostingGroupSalesTaxItem, PostingGroupSalesTaxParty,ChargeType, Process, PlaceOfSupply, Percentage,LedgerAc)
                    VALUES('2017-04-01', '1','D','GST 18%','Registered','TAX3','PURCH','Within State',9,'ISGST18');


                    insert into PostingGroupSalesTax(WEF,Site_Code,Div_Code,PostingGroupSalesTaxItem, PostingGroupSalesTaxParty,ChargeType, Process, PlaceOfSupply, Percentage,LedgerAc)
                    VALUES('2017-04-01', '1','D','GST 28%','Unregistered','TAXABLE AMOUNT','PURCH','Within State',Null,'PURWU28');
                    insert into PostingGroupSalesTax(WEF,Site_Code,Div_Code,PostingGroupSalesTaxItem, PostingGroupSalesTaxParty,ChargeType, Process, PlaceOfSupply, Percentage,LedgerAc)
                    VALUES('2017-04-01', '1','D','GST 28%','Unregistered','TAXABLE AMOUNT','PURCH','Outside State',Null,'PUROU28');
                    insert into PostingGroupSalesTax(WEF,Site_Code,Div_Code,PostingGroupSalesTaxItem, PostingGroupSalesTaxParty,ChargeType, Process, PlaceOfSupply, Percentage,LedgerAc)
                    VALUES('2017-04-01', '1','D','GST 28%','Registered','TAXABLE AMOUNT','PURCH','Outside State',Null,'PUROR28');
                    insert into PostingGroupSalesTax(WEF,Site_Code,Div_Code,PostingGroupSalesTaxItem, PostingGroupSalesTaxParty,ChargeType, Process, PlaceOfSupply, Percentage,LedgerAc)
                    VALUES('2017-04-01', '1','D','GST 28%','Registered','TAXABLE AMOUNT','PURCH','Within State',Null,'PURWR28');
                    insert into PostingGroupSalesTax(WEF,Site_Code,Div_Code,PostingGroupSalesTaxItem, PostingGroupSalesTaxParty,ChargeType, Process, PlaceOfSupply, Percentage,LedgerAc)
                    VALUES('2017-04-01', '1','D','GST 28%','Registered','TAX1','PURCH','Outside State',28,'IIGST28');
                    insert into PostingGroupSalesTax(WEF,Site_Code,Div_Code,PostingGroupSalesTaxItem, PostingGroupSalesTaxParty,ChargeType, Process, PlaceOfSupply, Percentage,LedgerAc)
                    VALUES('2017-04-01', '1','D','GST 28%','Registered','TAX2','PURCH','Within State',14,'ICGST28');
                    insert into PostingGroupSalesTax(WEF,Site_Code,Div_Code,PostingGroupSalesTaxItem, PostingGroupSalesTaxParty,ChargeType, Process, PlaceOfSupply, Percentage,LedgerAc)
                    VALUES('2017-04-01', '1','D','GST 28%','Registered','TAX3','PURCH','Within State',14,'ISGST28');















                    insert into PostingGroupSalesTax(WEF,Site_Code,Div_Code,PostingGroupSalesTaxItem, PostingGroupSalesTaxParty,ChargeType, Process, PlaceOfSupply, Percentage,LedgerAc)
                    VALUES('2017-04-01', '1','D','GST 0%','Unregistered','TAXABLE AMOUNT','SALES','Within State',Null,'SALEWU0');
                    insert into PostingGroupSalesTax(WEF,Site_Code,Div_Code,PostingGroupSalesTaxItem, PostingGroupSalesTaxParty,ChargeType, Process, PlaceOfSupply, Percentage,LedgerAc)
                    VALUES('2017-04-01', '1','D','GST 0%','Unregistered','TAXABLE AMOUNT','SALES','Outside State',Null,'SALEOU0');
                    insert into PostingGroupSalesTax(WEF,Site_Code,Div_Code,PostingGroupSalesTaxItem, PostingGroupSalesTaxParty,ChargeType, Process, PlaceOfSupply, Percentage,LedgerAc)
                    VALUES('2017-04-01', '1','D','GST 0%','Registered','TAXABLE AMOUNT','SALES','Outside State',Null,'SALEOR0');
                    insert into PostingGroupSalesTax(WEF,Site_Code,Div_Code,PostingGroupSalesTaxItem, PostingGroupSalesTaxParty,ChargeType, Process, PlaceOfSupply, Percentage,LedgerAc)
                    VALUES('2017-04-01', '1','D','GST 0%','Registered','TAXABLE AMOUNT','SALES','Within State',Null,'SALEWR0');


                    insert into PostingGroupSalesTax(WEF,Site_Code,Div_Code,PostingGroupSalesTaxItem, PostingGroupSalesTaxParty,ChargeType, Process, PlaceOfSupply, Percentage,LedgerAc)
                    VALUES('2017-04-01', '1','D','GST 5%','Unregistered','TAXABLE AMOUNT','SALES','Within State',Null,'SALEWU5');
                    insert into PostingGroupSalesTax(WEF,Site_Code,Div_Code,PostingGroupSalesTaxItem, PostingGroupSalesTaxParty,ChargeType, Process, PlaceOfSupply, Percentage,LedgerAc)
                    VALUES('2017-04-01', '1','D','GST 5%','Unregistered','TAXABLE AMOUNT','SALES','Outside State',Null,'SALEOU5');
                    insert into PostingGroupSalesTax(WEF,Site_Code,Div_Code,PostingGroupSalesTaxItem, PostingGroupSalesTaxParty,ChargeType, Process, PlaceOfSupply, Percentage,LedgerAc)
                    VALUES('2017-04-01', '1','D','GST 5%','Registered','TAXABLE AMOUNT','SALES','Outside State',Null,'SALEOR5');
                    insert into PostingGroupSalesTax(WEF,Site_Code,Div_Code,PostingGroupSalesTaxItem, PostingGroupSalesTaxParty,ChargeType, Process, PlaceOfSupply, Percentage,LedgerAc)
                    VALUES('2017-04-01', '1','D','GST 5%','Registered','TAXABLE AMOUNT','SALES','Within State',Null,'SALEWR5');
                    insert into PostingGroupSalesTax(WEF,Site_Code,Div_Code,PostingGroupSalesTaxItem, PostingGroupSalesTaxParty,ChargeType, Process, PlaceOfSupply, Percentage,LedgerAc)
                    VALUES('2017-04-01', '1','D','GST 5%','Registered','TAX1','SALES','Outside State',5,'ROIGST5');
                    insert into PostingGroupSalesTax(WEF,Site_Code,Div_Code,PostingGroupSalesTaxItem, PostingGroupSalesTaxParty,ChargeType, Process, PlaceOfSupply, Percentage,LedgerAc)
                    VALUES('2017-04-01', '1','D','GST 5%','Registered','TAX2','SALES','Within State',2.5,'ROCGST5');
                    insert into PostingGroupSalesTax(WEF,Site_Code,Div_Code,PostingGroupSalesTaxItem, PostingGroupSalesTaxParty,ChargeType, Process, PlaceOfSupply, Percentage,LedgerAc)
                    VALUES('2017-04-01', '1','D','GST 5%','Registered','TAX3','SALES','Within State',2.5,'ROSGST5');
                    insert into PostingGroupSalesTax(WEF,Site_Code,Div_Code,PostingGroupSalesTaxItem, PostingGroupSalesTaxParty,ChargeType, Process, PlaceOfSupply, Percentage,LedgerAc)
                    VALUES('2017-04-01', '1','D','GST 5%','Unregistered','TAX1','SALES','Outside State',5,'UOIGST5');
                    insert into PostingGroupSalesTax(WEF,Site_Code,Div_Code,PostingGroupSalesTaxItem, PostingGroupSalesTaxParty,ChargeType, Process, PlaceOfSupply, Percentage,LedgerAc)
                    VALUES('2017-04-01', '1','D','GST 5%','Unregistered','TAX2','SALES','Within State',2.5,'UOCGST5');
                    insert into PostingGroupSalesTax(WEF,Site_Code,Div_Code,PostingGroupSalesTaxItem, PostingGroupSalesTaxParty,ChargeType, Process, PlaceOfSupply, Percentage,LedgerAc)
                    VALUES('2017-04-01', '1','D','GST 5%','Unregistered','TAX3','SALES','Within State',2.5,'UOSGST5');


                    insert into PostingGroupSalesTax(WEF,Site_Code,Div_Code,PostingGroupSalesTaxItem, PostingGroupSalesTaxParty,ChargeType, Process, PlaceOfSupply, Percentage,LedgerAc)
                    VALUES('2017-04-01', '1','D','GST 12%','Unregistered','TAXABLE AMOUNT','SALES','Within State',Null,'SALEWU12');
                    insert into PostingGroupSalesTax(WEF,Site_Code,Div_Code,PostingGroupSalesTaxItem, PostingGroupSalesTaxParty,ChargeType, Process, PlaceOfSupply, Percentage,LedgerAc)
                    VALUES('2017-04-01', '1','D','GST 12%','Unregistered','TAXABLE AMOUNT','SALES','Outside State',Null,'SALEOU12');
                    insert into PostingGroupSalesTax(WEF,Site_Code,Div_Code,PostingGroupSalesTaxItem, PostingGroupSalesTaxParty,ChargeType, Process, PlaceOfSupply, Percentage,LedgerAc)
                    VALUES('2017-04-01', '1','D','GST 12%','Registered','TAXABLE AMOUNT','SALES','Outside State',Null,'SALEOR12');
                    insert into PostingGroupSalesTax(WEF,Site_Code,Div_Code,PostingGroupSalesTaxItem, PostingGroupSalesTaxParty,ChargeType, Process, PlaceOfSupply, Percentage,LedgerAc)
                    VALUES('2017-04-01', '1','D','GST 12%','Registered','TAXABLE AMOUNT','SALES','Within State',Null,'SALEWR12');
                    insert into PostingGroupSalesTax(WEF,Site_Code,Div_Code,PostingGroupSalesTaxItem, PostingGroupSalesTaxParty,ChargeType, Process, PlaceOfSupply, Percentage,LedgerAc)
                    VALUES('2017-04-01', '1','D','GST 12%','Registered','TAX1','SALES','Outside State',12,'ROIGST12');
                    insert into PostingGroupSalesTax(WEF,Site_Code,Div_Code,PostingGroupSalesTaxItem, PostingGroupSalesTaxParty,ChargeType, Process, PlaceOfSupply, Percentage,LedgerAc)
                    VALUES('2017-04-01', '1','D','GST 12%','Registered','TAX2','SALES','Within State',6,'ROCGST12');
                    insert into PostingGroupSalesTax(WEF,Site_Code,Div_Code,PostingGroupSalesTaxItem, PostingGroupSalesTaxParty,ChargeType, Process, PlaceOfSupply, Percentage,LedgerAc)
                    VALUES('2017-04-01', '1','D','GST 12%','Registered','TAX3','SALES','Within State',6,'ROSGST12');
                    insert into PostingGroupSalesTax(WEF,Site_Code,Div_Code,PostingGroupSalesTaxItem, PostingGroupSalesTaxParty,ChargeType, Process, PlaceOfSupply, Percentage,LedgerAc)
                    VALUES('2017-04-01', '1','D','GST 12%','Unregistered','TAX1','SALES','Outside State',12,'UOIGST12');
                    insert into PostingGroupSalesTax(WEF,Site_Code,Div_Code,PostingGroupSalesTaxItem, PostingGroupSalesTaxParty,ChargeType, Process, PlaceOfSupply, Percentage,LedgerAc)
                    VALUES('2017-04-01', '1','D','GST 12%','Unregistered','TAX2','SALES','Within State',6,'UOCGST12');
                    insert into PostingGroupSalesTax(WEF,Site_Code,Div_Code,PostingGroupSalesTaxItem, PostingGroupSalesTaxParty,ChargeType, Process, PlaceOfSupply, Percentage,LedgerAc)
                    VALUES('2017-04-01', '1','D','GST 12%','Unregistered','TAX3','SALES','Within State',6,'UOSGST12');



                    insert into PostingGroupSalesTax(WEF,Site_Code,Div_Code,PostingGroupSalesTaxItem, PostingGroupSalesTaxParty,ChargeType, Process, PlaceOfSupply, Percentage,LedgerAc)
                    VALUES('2017-04-01', '1','D','GST 18%','Unregistered','TAXABLE AMOUNT','SALES','Within State',Null,'SALEWU18');
                    insert into PostingGroupSalesTax(WEF,Site_Code,Div_Code,PostingGroupSalesTaxItem, PostingGroupSalesTaxParty,ChargeType, Process, PlaceOfSupply, Percentage,LedgerAc)
                    VALUES('2017-04-01', '1','D','GST 18%','Unregistered','TAXABLE AMOUNT','SALES','Outside State',Null,'SALEOU18');
                    insert into PostingGroupSalesTax(WEF,Site_Code,Div_Code,PostingGroupSalesTaxItem, PostingGroupSalesTaxParty,ChargeType, Process, PlaceOfSupply, Percentage,LedgerAc)
                    VALUES('2017-04-01', '1','D','GST 18%','Registered','TAXABLE AMOUNT','SALES','Outside State',Null,'SALEOR18');
                    insert into PostingGroupSalesTax(WEF,Site_Code,Div_Code,PostingGroupSalesTaxItem, PostingGroupSalesTaxParty,ChargeType, Process, PlaceOfSupply, Percentage,LedgerAc)
                    VALUES('2017-04-01', '1','D','GST 18%','Registered','TAXABLE AMOUNT','SALES','Within State',Null,'SALEWR18');
                    insert into PostingGroupSalesTax(WEF,Site_Code,Div_Code,PostingGroupSalesTaxItem, PostingGroupSalesTaxParty,ChargeType, Process, PlaceOfSupply, Percentage,LedgerAc)
                    VALUES('2017-04-01', '1','D','GST 18%','Registered','TAX1','SALES','Outside State',18,'ROIGST18');
                    insert into PostingGroupSalesTax(WEF,Site_Code,Div_Code,PostingGroupSalesTaxItem, PostingGroupSalesTaxParty,ChargeType, Process, PlaceOfSupply, Percentage,LedgerAc)
                    VALUES('2017-04-01', '1','D','GST 18%','Registered','TAX2','SALES','Within State',9,'ROCGST18');
                    insert into PostingGroupSalesTax(WEF,Site_Code,Div_Code,PostingGroupSalesTaxItem, PostingGroupSalesTaxParty,ChargeType, Process, PlaceOfSupply, Percentage,LedgerAc)
                    VALUES('2017-04-01', '1','D','GST 18%','Registered','TAX3','SALES','Within State',9,'ROSGST18');
                    insert into PostingGroupSalesTax(WEF,Site_Code,Div_Code,PostingGroupSalesTaxItem, PostingGroupSalesTaxParty,ChargeType, Process, PlaceOfSupply, Percentage,LedgerAc)
                    VALUES('2017-04-01', '1','D','GST 18%','Unregistered','TAX1','SALES','Outside State',18,'UOIGST18');
                    insert into PostingGroupSalesTax(WEF,Site_Code,Div_Code,PostingGroupSalesTaxItem, PostingGroupSalesTaxParty,ChargeType, Process, PlaceOfSupply, Percentage,LedgerAc)
                    VALUES('2017-04-01', '1','D','GST 18%','Unregistered','TAX2','SALES','Within State',9,'UOCGST18');
                    insert into PostingGroupSalesTax(WEF,Site_Code,Div_Code,PostingGroupSalesTaxItem, PostingGroupSalesTaxParty,ChargeType, Process, PlaceOfSupply, Percentage,LedgerAc)
                    VALUES('2017-04-01', '1','D','GST 18%','Unregistered','TAX3','SALES','Within State',9,'UOSGST18');


                    insert into PostingGroupSalesTax(WEF,Site_Code,Div_Code,PostingGroupSalesTaxItem, PostingGroupSalesTaxParty,ChargeType, Process, PlaceOfSupply, Percentage,LedgerAc)
                    VALUES('2017-04-01', '1','D','GST 28%','Unregistered','TAXABLE AMOUNT','SALES','Within State',Null,'SALEWU28');
                    insert into PostingGroupSalesTax(WEF,Site_Code,Div_Code,PostingGroupSalesTaxItem, PostingGroupSalesTaxParty,ChargeType, Process, PlaceOfSupply, Percentage,LedgerAc)
                    VALUES('2017-04-01', '1','D','GST 28%','Unregistered','TAXABLE AMOUNT','SALES','Outside State',Null,'SALEOU28');
                    insert into PostingGroupSalesTax(WEF,Site_Code,Div_Code,PostingGroupSalesTaxItem, PostingGroupSalesTaxParty,ChargeType, Process, PlaceOfSupply, Percentage,LedgerAc)
                    VALUES('2017-04-01', '1','D','GST 28%','Registered','TAXABLE AMOUNT','SALES','Outside State',Null,'SALEOR28');
                    insert into PostingGroupSalesTax(WEF,Site_Code,Div_Code,PostingGroupSalesTaxItem, PostingGroupSalesTaxParty,ChargeType, Process, PlaceOfSupply, Percentage,LedgerAc)
                    VALUES('2017-04-01', '1','D','GST 28%','Registered','TAXABLE AMOUNT','SALES','Within State',Null,'SALEWR28');
                    insert into PostingGroupSalesTax(WEF,Site_Code,Div_Code,PostingGroupSalesTaxItem, PostingGroupSalesTaxParty,ChargeType, Process, PlaceOfSupply, Percentage,LedgerAc)
                    VALUES('2017-04-01', '1','D','GST 28%','Registered','TAX1','SALES','Outside State',28,'ROIGST28');
                    insert into PostingGroupSalesTax(WEF,Site_Code,Div_Code,PostingGroupSalesTaxItem, PostingGroupSalesTaxParty,ChargeType, Process, PlaceOfSupply, Percentage,LedgerAc)
                    VALUES('2017-04-01', '1','D','GST 28%','Registered','TAX2','SALES','Within State',14,'ROCGST28');
                    insert into PostingGroupSalesTax(WEF,Site_Code,Div_Code,PostingGroupSalesTaxItem, PostingGroupSalesTaxParty,ChargeType, Process, PlaceOfSupply, Percentage,LedgerAc)
                    VALUES('2017-04-01', '1','D','GST 28%','Registered','TAX3','SALES','Within State',14,'ROSGST28');
                    insert into PostingGroupSalesTax(WEF,Site_Code,Div_Code,PostingGroupSalesTaxItem, PostingGroupSalesTaxParty,ChargeType, Process, PlaceOfSupply, Percentage,LedgerAc)
                    VALUES('2017-04-01', '1','D','GST 28%','Unregistered','TAX1','SALES','Outside State',28,'UOIGST28');
                    insert into PostingGroupSalesTax(WEF,Site_Code,Div_Code,PostingGroupSalesTaxItem, PostingGroupSalesTaxParty,ChargeType, Process, PlaceOfSupply, Percentage,LedgerAc)
                    VALUES('2017-04-01', '1','D','GST 28%','Unregistered','TAX2','SALES','Within State',14,'UOCGST28');
                    insert into PostingGroupSalesTax(WEF,Site_Code,Div_Code,PostingGroupSalesTaxItem, PostingGroupSalesTaxParty,ChargeType, Process, PlaceOfSupply, Percentage,LedgerAc)
                    VALUES('2017-04-01', '1','D','GST 28%','Unregistered','TAX3','SALES','Within State',14,'UOSGST28');



                   "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If
        Catch ex As Exception
            MsgBox(ex.Message & "  [FSeedTable_PostingGroupSalesTax]")
        End Try

    End Sub

    Private Sub FCreateTable_Subgroup()
        Dim mQry As String
        Try

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
            AgL.AddFieldSqlite("Subgroup", "SubgroupType", "nVarchar(20)", "", True, " references SubgroupType(SubgroupType) ")

        Catch ex As Exception
            MsgBox(ex.Message & " [FCreateTable_Subgroup]")
        End Try
    End Sub

    Private Sub FSeedTable_SubgroupType()
        Dim mQry As String
        Try
            If AgL.FillData("Select * from SubGroupType Where SubgroupType='Customer'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                        Insert Into SubGroupType (SubgroupType,IsActive)
                        Values ('Customer',1);                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If

            If AgL.FillData("Select * from SubGroupType Where SubgroupType='Supplier'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                        Insert Into SubGroupType (SubgroupType,IsActive)
                        Values ('Supplier',1);                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If


            If AgL.FillData("Select * from SubGroupType Where SubgroupType='Agent'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                        Insert Into SubGroupType (SubgroupType,IsActive)
                        Values ('Agent',1);                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If

            If AgL.FillData("Select * from SubGroupType Where SubgroupType='Transporter'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                        Insert Into SubGroupType (SubgroupType,IsActive)
                        Values ('Transporter',1);                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If

        Catch ex As Exception
            MsgBox(ex.Message & " [FSeedTable_Subgroup]")
        End Try


    End Sub

    Private Sub FSeedTable_SubgroupTypeSetting()
        Dim mQry As String
        Try
            If AgL.FillData("Select * from SubGroupTypeSetting Where SubGroupType='Customer'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                        INSERT INTO SubgroupTypeSetting
                        (SubgroupType, Div_Code, Site_Code, IsVisible_ContactPerson, IsVisible_SalesTaxNo, IsVisible_PanNo, IsVisible_AadharNo, IsVisible_Parent, Caption_Parent, IsVisible_CreditLimit, AcGroupCode)
                        VALUES('Customer', Null, Null, '1', '1', '1', '1', '1', Null,1,'0020');
                        "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If

            If AgL.FillData("Select * from SubGroupTypeSetting Where SubGroupType='Supplier'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                        INSERT INTO SubgroupTypeSetting
                        (SubgroupType, Div_Code, Site_Code, IsVisible_ContactPerson, IsVisible_SalesTaxNo, IsVisible_PanNo, IsVisible_AadharNo, IsVisible_Parent, Caption_Parent, IsVisible_CreditLimit, AcGroupCode)
                        VALUES('Supplier', Null, '1', '1', '1', '1', '1', '1', Null,1,'0016');
                        "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If

            If AgL.FillData("Select * from SubGroupTypeSetting Where SubGroupType='Agent'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                        INSERT INTO SubgroupTypeSetting
                        (SubgroupType, Div_Code, Site_Code, IsVisible_ContactPerson, IsVisible_SalesTaxNo, IsVisible_PanNo, IsVisible_AadharNo, IsVisible_Parent, Caption_Parent, IsVisible_CreditLimit, AcGroupCode)
                        VALUES('Agent', Null, Null, '0', '0', '0', '0', '0', Null,0,'0034');
                        "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If


            If AgL.FillData("Select * from SubGroupTypeSetting Where SubGroupType='Transporter'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                        INSERT INTO SubgroupTypeSetting
                        ( SubgroupType, Div_Code, Site_Code, IsVisible_ContactPerson, IsVisible_SalesTaxNo, IsVisible_PanNo, IsVisible_AadharNo, IsVisible_Parent, Caption_Parent, IsVisible_CreditLimit, AcGroupCode)
                        VALUES('Transporter', Null, Null,  '0', '0', '0', '0', '0', Null,0,'0033');
                        "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If





        Catch ex As Exception
            MsgBox(ex.Message & " [FSeedTable_Subgroup]")
        End Try


    End Sub


    Private Sub FSeedTable_Subgroup()
        Dim mQry As String
        Try
            If AgL.FillData("Select * from SubGroup Where SubCode='|PARTY|'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    INSERT INTO SubGroup
                    (SubCode, Site_Code, Div_Code, SiteList, NamePrefix, Name, DispName, GroupCode, GroupNature, ManualCode, Nature, Add1, Add2, Add3, CityCode, CountryCode, PIN, Phone, Mobile, FAX, EMail, CSTNo, LSTNo, TINNo, PAN, TDS_Catg, ActiveYN, CreditLimit, CreditDays, DueDays, ContactPerson, Party_Type, PAdd1, PAdd2, PAdd3, PCityCode, PCountryCode, PPin, PPhone, PMobile, PFax, Curr_Bal, OpBal_DocId, FatherName, FatherNamePrefix, HusbandName, HusbandNamePrefix, DOB, Remark, Location, U_Name, U_EntDt, U_AE, Edit_Date, ModifiedBy, ApprovedBy, StCategory, SiteStr, STRegNo, ECCNo, EXREGNO, CEXRANGE, CEXDIV, COMMRATE, VATNo, CommonAc, RowId, UpLoadDate, ChequeReport, EntryBy, EntryDate, EntryType, EntryStatus, ApproveBy, ApproveDate, MoveToLog, IsDeleted, MoveToLogDate, Status, SisterConcernYn, UID, SalesTaxPostingGroup, ExcisePostingGroup, EntryTaxPostingGroup, TDSCat_Description, MasterType, Currency, SisterConcernSite, CostCenter, Parent, LedgerGroup, Zone, DuplicateTIN, Distributor, STNo, IECCode, Range, Division, PartyType, PartyCat, ECCCode, Excise, FBTOnPer, FBTPer, PolicyNo, Transfered, DivisionList, Upline, Department, Designation, Guarantor, InsideOutside, DrugLicenseNo, PartyRateGroup)
                    VALUES('|PARTY|', '1', 'D', '|1|', NULL, '|PARTY|', '|PARTY|', NULL, 'A', 'Party', 'Others', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'SA', '2008-10-21 00:00:00', 'A', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'Rs.', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If

            If AgL.FillData("Select * from SubGroup Where SubCode='SALE'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    INSERT INTO SubGroup
                    (SubCode, Site_Code, Div_Code, SiteList, NamePrefix, Name, DispName, GroupCode, GroupNature, ManualCode, Nature, Add1, Add2, Add3, CityCode, CountryCode, PIN, Phone, Mobile, FAX, EMail, CSTNo, LSTNo, TINNo, PAN, TDS_Catg, ActiveYN, CreditLimit, CreditDays, DueDays, ContactPerson, Party_Type, PAdd1, PAdd2, PAdd3, PCityCode, PCountryCode, PPin, PPhone, PMobile, PFax, Curr_Bal, OpBal_DocId, FatherName, FatherNamePrefix, HusbandName, HusbandNamePrefix, DOB, Remark, Location, U_Name, U_EntDt, U_AE, Edit_Date, ModifiedBy, ApprovedBy, StCategory, SiteStr, STRegNo, ECCNo, EXREGNO, CEXRANGE, CEXDIV, COMMRATE, VATNo, CommonAc, RowId, UpLoadDate, ChequeReport, EntryBy, EntryDate, EntryType, EntryStatus, ApproveBy, ApproveDate, MoveToLog, IsDeleted, MoveToLogDate, Status, SisterConcernYn, UID, SalesTaxPostingGroup, ExcisePostingGroup, EntryTaxPostingGroup, TDSCat_Description, MasterType, Currency, SisterConcernSite, CostCenter, Parent, LedgerGroup, Zone, DuplicateTIN, Distributor, STNo, IECCode, Range, Division, PartyType, PartyCat, ECCCode, Excise, FBTOnPer, FBTPer, PolicyNo, Transfered, DivisionList, Upline, Department, Designation, Guarantor, InsideOutside, DrugLicenseNo, PartyRateGroup)
                    VALUES('SALE', '1', 'D', '|1|', NULL, 'SALE A/C', 'SALE A/C', '0023', '', 'SALE', 'Others', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'Rs.', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If


            If AgL.FillData("Select * from SubGroup Where SubCode='PURCH'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    INSERT INTO SubGroup
                    (SubCode, Site_Code, Div_Code, SiteList, NamePrefix, Name, DispName, GroupCode, GroupNature, ManualCode, Nature, Add1, Add2, Add3, CityCode, CountryCode, PIN, Phone, Mobile, FAX, EMail, CSTNo, LSTNo, TINNo, PAN, TDS_Catg, ActiveYN, CreditLimit, CreditDays, DueDays, ContactPerson, Party_Type, PAdd1, PAdd2, PAdd3, PCityCode, PCountryCode, PPin, PPhone, PMobile, PFax, Curr_Bal, OpBal_DocId, FatherName, FatherNamePrefix, HusbandName, HusbandNamePrefix, DOB, Remark, Location, U_Name, U_EntDt, U_AE, Edit_Date, ModifiedBy, ApprovedBy, StCategory, SiteStr, STRegNo, ECCNo, EXREGNO, CEXRANGE, CEXDIV, COMMRATE, VATNo, CommonAc, RowId, UpLoadDate, ChequeReport, EntryBy, EntryDate, EntryType, EntryStatus, ApproveBy, ApproveDate, MoveToLog, IsDeleted, MoveToLogDate, Status, SisterConcernYn, UID, SalesTaxPostingGroup, ExcisePostingGroup, EntryTaxPostingGroup, TDSCat_Description, MasterType, Currency, SisterConcernSite, CostCenter, Parent, LedgerGroup, Zone, DuplicateTIN, Distributor, STNo, IECCode, Range, Division, PartyType, PartyCat, ECCCode, Excise, FBTOnPer, FBTPer, PolicyNo, Transfered, DivisionList, Upline, Department, Designation, Guarantor, InsideOutside, DrugLicenseNo, PartyRateGroup)
                    VALUES('PURCH', '1', 'D', '|1|', NULL, 'Purchase A/C', 'Purchase A/C', '0024', '', 'Purchase', 'Purchase', '', '', NULL, NULL, NULL, '', '', '', '', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0.0, NULL, 0, '', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'SA', '2013-04-23 00:00:00', 'E', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'Rs.', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0.0, 0.0, NULL, 'N', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If

            If AgL.FillData("Select * from SubGroup Where SubCode='ROFF'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    INSERT INTO SubGroup
                    (SubCode, Site_Code, Div_Code, SiteList, NamePrefix, Name, DispName, GroupCode, GroupNature, ManualCode, Nature, Add1, Add2, Add3, CityCode, CountryCode, PIN, Phone, Mobile, FAX, EMail, CSTNo, LSTNo, TINNo, PAN, TDS_Catg, ActiveYN, CreditLimit, CreditDays, DueDays, ContactPerson, Party_Type, PAdd1, PAdd2, PAdd3, PCityCode, PCountryCode, PPin, PPhone, PMobile, PFax, Curr_Bal, OpBal_DocId, FatherName, FatherNamePrefix, HusbandName, HusbandNamePrefix, DOB, Remark, Location, U_Name, U_EntDt, U_AE, Edit_Date, ModifiedBy, ApprovedBy, StCategory, SiteStr, STRegNo, ECCNo, EXREGNO, CEXRANGE, CEXDIV, COMMRATE, VATNo, CommonAc, RowId, UpLoadDate, ChequeReport, EntryBy, EntryDate, EntryType, EntryStatus, ApproveBy, ApproveDate, MoveToLog, IsDeleted, MoveToLogDate, Status, SisterConcernYn, UID, SalesTaxPostingGroup, ExcisePostingGroup, EntryTaxPostingGroup, TDSCat_Description, MasterType, Currency, SisterConcernSite, CostCenter, Parent, LedgerGroup, Zone, DuplicateTIN, Distributor, STNo, IECCode, Range, Division, PartyType, PartyCat, ECCCode, Excise, FBTOnPer, FBTPer, PolicyNo, Transfered, DivisionList, Upline, Department, Designation, Guarantor, InsideOutside, DrugLicenseNo, PartyRateGroup)
                    VALUES('ROFF', '1', 'D', '|1|', NULL, 'Round Off A/c', 'Round Off A/c', '0029', '', 'ROFF', 'Others', '', '', NULL, NULL, NULL, '', '', '', '', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0.0, NULL, 0, '', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'sa', '2015-08-12 00:00:00', 'E', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'Rs.', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0.0, 0.0, NULL, 'N', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If

            If AgL.FillData("Select * from SubGroup Where SubCode='DEDUCTION'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    INSERT INTO SubGroup
                    (SubCode, Site_Code, Div_Code, SiteList, NamePrefix, Name, DispName, GroupCode, GroupNature, ManualCode, Nature, Add1, Add2, Add3, CityCode, CountryCode, PIN, Phone, Mobile, FAX, EMail, CSTNo, LSTNo, TINNo, PAN, TDS_Catg, ActiveYN, CreditLimit, CreditDays, DueDays, ContactPerson, Party_Type, PAdd1, PAdd2, PAdd3, PCityCode, PCountryCode, PPin, PPhone, PMobile, PFax, Curr_Bal, OpBal_DocId, FatherName, FatherNamePrefix, HusbandName, HusbandNamePrefix, DOB, Remark, Location, U_Name, U_EntDt, U_AE, Edit_Date, ModifiedBy, ApprovedBy, StCategory, SiteStr, STRegNo, ECCNo, EXREGNO, CEXRANGE, CEXDIV, COMMRATE, VATNo, CommonAc, RowId, UpLoadDate, ChequeReport, EntryBy, EntryDate, EntryType, EntryStatus, ApproveBy, ApproveDate, MoveToLog, IsDeleted, MoveToLogDate, Status, SisterConcernYn, UID, SalesTaxPostingGroup, ExcisePostingGroup, EntryTaxPostingGroup, TDSCat_Description, MasterType, Currency, SisterConcernSite, CostCenter, Parent, LedgerGroup, Zone, DuplicateTIN, Distributor, STNo, IECCode, Range, Division, PartyType, PartyCat, ECCCode, Excise, FBTOnPer, FBTPer, PolicyNo, Transfered, DivisionList, Upline, Department, Designation, Guarantor, InsideOutside, DrugLicenseNo, PartyRateGroup)
                    VALUES('DEDUCTION', '1', 'D', '|1|', NULL, 'Deduction A/c', 'Deduction A/c', '0025', 'L', 'DEDUCTION', 'Others', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'SA', '2008-10-21 00:00:00', 'A', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'Rs.', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If

            If AgL.FillData("Select * from SubGroup Where SubCode='OCHARGE'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    INSERT INTO SubGroup
                    (SubCode, Site_Code, Div_Code, SiteList, NamePrefix, Name, DispName, GroupCode, GroupNature, ManualCode, Nature, Add1, Add2, Add3, CityCode, CountryCode, PIN, Phone, Mobile, FAX, EMail, CSTNo, LSTNo, TINNo, PAN, TDS_Catg, ActiveYN, CreditLimit, CreditDays, DueDays, ContactPerson, Party_Type, PAdd1, PAdd2, PAdd3, PCityCode, PCountryCode, PPin, PPhone, PMobile, PFax, Curr_Bal, OpBal_DocId, FatherName, FatherNamePrefix, HusbandName, HusbandNamePrefix, DOB, Remark, Location, U_Name, U_EntDt, U_AE, Edit_Date, ModifiedBy, ApprovedBy, StCategory, SiteStr, STRegNo, ECCNo, EXREGNO, CEXRANGE, CEXDIV, COMMRATE, VATNo, CommonAc, RowId, UpLoadDate, ChequeReport, EntryBy, EntryDate, EntryType, EntryStatus, ApproveBy, ApproveDate, MoveToLog, IsDeleted, MoveToLogDate, Status, SisterConcernYn, UID, SalesTaxPostingGroup, ExcisePostingGroup, EntryTaxPostingGroup, TDSCat_Description, MasterType, Currency, SisterConcernSite, CostCenter, Parent, LedgerGroup, Zone, DuplicateTIN, Distributor, STNo, IECCode, Range, Division, PartyType, PartyCat, ECCCode, Excise, FBTOnPer, FBTPer, PolicyNo, Transfered, DivisionList, Upline, Department, Designation, Guarantor, InsideOutside, DrugLicenseNo, PartyRateGroup)
                    VALUES('OCHARGE', '1', 'D', '|1|', NULL, 'Other Charge A/c', 'Other Charge A/c', '0026', 'L', 'OCHARGE', 'Others', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'SA', '2008-10-21 00:00:00', 'A', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'Rs.', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If


            If AgL.FillData("Select * from SubGroup Where SubCode='CASH'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    INSERT INTO SubGroup
                    (SubCode, Site_Code, Div_Code, SiteList, NamePrefix, Name, DispName, GroupCode, GroupNature, ManualCode, Nature, Add1, Add2, Add3, CityCode, CountryCode, PIN, Phone, Mobile, FAX, EMail, CSTNo, LSTNo, TINNo, PAN, TDS_Catg, ActiveYN, CreditLimit, CreditDays, DueDays, ContactPerson, Party_Type, PAdd1, PAdd2, PAdd3, PCityCode, PCountryCode, PPin, PPhone, PMobile, PFax, Curr_Bal, OpBal_DocId, FatherName, FatherNamePrefix, HusbandName, HusbandNamePrefix, DOB, Remark, Location, U_Name, U_EntDt, U_AE, Edit_Date, ModifiedBy, ApprovedBy, StCategory, SiteStr, STRegNo, ECCNo, EXREGNO, CEXRANGE, CEXDIV, COMMRATE, VATNo, CommonAc, RowId, UpLoadDate, ChequeReport, EntryBy, EntryDate, EntryType, EntryStatus, ApproveBy, ApproveDate, MoveToLog, IsDeleted, MoveToLogDate, Status, SisterConcernYn, UID, SalesTaxPostingGroup, ExcisePostingGroup, EntryTaxPostingGroup, TDSCat_Description, MasterType, Currency, SisterConcernSite, CostCenter, Parent, LedgerGroup, Zone, DuplicateTIN, Distributor, STNo, IECCode, Range, Division, PartyType, PartyCat, ECCCode, Excise, FBTOnPer, FBTPer, PolicyNo, Transfered, DivisionList, Upline, Department, Designation, Guarantor, InsideOutside, DrugLicenseNo, PartyRateGroup)
                    VALUES('CASH', '1', 'D', '|1|', NULL, 'CASH A/C', 'CASH A/C', '0021', '', 'CASH', 'CASH', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'Rs.', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If


            If AgL.FillData("Select * from SubGroup Where SubCode='BANK'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    INSERT INTO SubGroup
                    (SubCode, Site_Code, Div_Code, SiteList, NamePrefix, Name, DispName, GroupCode, GroupNature, ManualCode, Nature, Add1, Add2, Add3, CityCode, CountryCode, PIN, Phone, Mobile, FAX, EMail, CSTNo, LSTNo, TINNo, PAN, TDS_Catg, ActiveYN, CreditLimit, CreditDays, DueDays, ContactPerson, Party_Type, PAdd1, PAdd2, PAdd3, PCityCode, PCountryCode, PPin, PPhone, PMobile, PFax, Curr_Bal, OpBal_DocId, FatherName, FatherNamePrefix, HusbandName, HusbandNamePrefix, DOB, Remark, Location, U_Name, U_EntDt, U_AE, Edit_Date, ModifiedBy, ApprovedBy, StCategory, SiteStr, STRegNo, ECCNo, EXREGNO, CEXRANGE, CEXDIV, COMMRATE, VATNo, CommonAc, RowId, UpLoadDate, ChequeReport, EntryBy, EntryDate, EntryType, EntryStatus, ApproveBy, ApproveDate, MoveToLog, IsDeleted, MoveToLogDate, Status, SisterConcernYn, UID, SalesTaxPostingGroup, ExcisePostingGroup, EntryTaxPostingGroup, TDSCat_Description, MasterType, Currency, SisterConcernSite, CostCenter, Parent, LedgerGroup, Zone, DuplicateTIN, Distributor, STNo, IECCode, Range, Division, PartyType, PartyCat, ECCCode, Excise, FBTOnPer, FBTPer, PolicyNo, Transfered, DivisionList, Upline, Department, Designation, Guarantor, InsideOutside, DrugLicenseNo, PartyRateGroup)
                    VALUES('BANK', '1', 'D', '|1|', NULL, 'Bank A/c', 'Bank A/c', '0022', '', 'BANK', 'BANK', '', '', NULL, NULL, NULL, '', '', '', '', NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, NULL, 0, '', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'SA', '2013-04-04 00:00:00', 'E', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'Rs.', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0, 0, NULL, 'N', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If


            If AgL.FillData("Select * from SubGroup Where SubCode='1stCUST'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    INSERT INTO SubGroup
                    (SubCode, Site_Code, Div_Code, SiteList, NamePrefix, Name, DispName, GroupCode, GroupNature, ManualCode, Nature, Add1, Add2, Add3, CityCode, CountryCode, PIN, Phone, Mobile, FAX, EMail, CSTNo, LSTNo, TINNo, PAN, TDS_Catg, ActiveYN, CreditLimit, CreditDays, DueDays, ContactPerson, Party_Type, PAdd1, PAdd2, PAdd3, PCityCode, PCountryCode, PPin, PPhone, PMobile, PFax, Curr_Bal, OpBal_DocId, FatherName, FatherNamePrefix, HusbandName, HusbandNamePrefix, DOB, Remark, Location, U_Name, U_EntDt, U_AE, Edit_Date, ModifiedBy, ApprovedBy, StCategory, SiteStr, STRegNo, ECCNo, EXREGNO, CEXRANGE, CEXDIV, COMMRATE, VATNo, CommonAc, RowId, UpLoadDate, ChequeReport, EntryBy, EntryDate, EntryType, EntryStatus, ApproveBy, ApproveDate, MoveToLog, IsDeleted, MoveToLogDate, Status, SisterConcernYn, UID, SalesTaxPostingGroup, ExcisePostingGroup, EntryTaxPostingGroup, TDSCat_Description, MasterType, Currency, SisterConcernSite, CostCenter, Parent, LedgerGroup, Zone, DuplicateTIN, Distributor, STNo, IECCode, Range, Division, PartyType, PartyCat, ECCCode, Excise, FBTOnPer, FBTPer, PolicyNo, Transfered, DivisionList, Upline, Department, Designation, Guarantor, InsideOutside, DrugLicenseNo, PartyRateGroup)
                    VALUES('1stCUST', '1', 'D', '|1|', NULL, 'Sample Customer', 'Sample Customer', '0020', 'A', '1', 'Customer', 'Address', NULL, NULL, 'D10001', NULL, '208001', NULL, '9935099350', NULL, 'info@Auditor9.com', NULL, NULL, NULL, NULL, NULL, NULL, 0.0, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'SUPER', '2008-10-21 00:00:00', 'A', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL, 'SUPER', '2018-03-04', 'Add', 'Open', NULL, NULL, NULL, NULL, NULL, 'Active', NULL, NULL, 'Unregistered', NULL, NULL, NULL, 'Customer', NULL, NULL, NULL, '1stCUST', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '|1stCUST|', NULL, NULL, NULL, NULL, NULL, NULL);
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If


            If AgL.FillData("Select * from SubGroup Where SubCode='1stSUPP'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    INSERT INTO SubGroup
                    (SubCode, Site_Code, Div_Code, SiteList, NamePrefix, Name, DispName, GroupCode, GroupNature, ManualCode, Nature, Add1, Add2, Add3, CityCode, CountryCode, PIN, Phone, Mobile, FAX, EMail, CSTNo, LSTNo, TINNo, PAN, TDS_Catg, ActiveYN, CreditLimit, CreditDays, DueDays, ContactPerson, Party_Type, PAdd1, PAdd2, PAdd3, PCityCode, PCountryCode, PPin, PPhone, PMobile, PFax, Curr_Bal, OpBal_DocId, FatherName, FatherNamePrefix, HusbandName, HusbandNamePrefix, DOB, Remark, Location, U_Name, U_EntDt, U_AE, Edit_Date, ModifiedBy, ApprovedBy, StCategory, SiteStr, STRegNo, ECCNo, EXREGNO, CEXRANGE, CEXDIV, COMMRATE, VATNo, CommonAc, RowId, UpLoadDate, ChequeReport, EntryBy, EntryDate, EntryType, EntryStatus, ApproveBy, ApproveDate, MoveToLog, IsDeleted, MoveToLogDate, Status, SisterConcernYn, UID, SalesTaxPostingGroup, ExcisePostingGroup, EntryTaxPostingGroup, TDSCat_Description, MasterType, Currency, SisterConcernSite, CostCenter, Parent, LedgerGroup, Zone, DuplicateTIN, Distributor, STNo, IECCode, Range, Division, PartyType, PartyCat, ECCCode, Excise, FBTOnPer, FBTPer, PolicyNo, Transfered, DivisionList, Upline, Department, Designation, Guarantor, InsideOutside, DrugLicenseNo, PartyRateGroup)
                    VALUES('1stSUPP', '2', 'D', '|1|', NULL, 'Sample Supplier', 'Sample Supplier', '0016', 'A', '1', 'Supplier', 'Address', NULL, NULL, 'D10001', NULL, '208001', NULL, '9935099350', NULL, 'info@Auditor9.com', NULL, NULL, NULL, NULL, NULL, NULL, 0.0, 0, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'SUPER', '2008-10-21 00:00:00', 'A', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL, 'SUPER', '2018-03-04', 'Add', 'Open', NULL, NULL, NULL, NULL, NULL, 'Active', NULL, NULL, 'Unregistered', NULL, NULL, NULL, 'Supplier', NULL, NULL, NULL, '1stSUPP', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, '|1stSUPP|', NULL, NULL, NULL, NULL, NULL, NULL);
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If


            If AgL.FillData("Select * from SubGroup Where SubCode='MAINGODOWN'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    INSERT INTO SubGroup
                    (SubCode, Site_Code, Div_Code, SiteList, NamePrefix, Name, DispName, GroupCode, GroupNature, ManualCode, Nature, Add1, Add2, Add3, CityCode, CountryCode, PIN, Phone, Mobile, FAX, EMail, CSTNo, LSTNo, TINNo, PAN, TDS_Catg, ActiveYN, CreditLimit, CreditDays, DueDays, ContactPerson, Party_Type, PAdd1, PAdd2, PAdd3, PCityCode, PCountryCode, PPin, PPhone, PMobile, PFax, Curr_Bal, OpBal_DocId, FatherName, FatherNamePrefix, HusbandName, HusbandNamePrefix, DOB, Remark, Location, U_Name, U_EntDt, U_AE, Edit_Date, ModifiedBy, ApprovedBy, StCategory, SiteStr, STRegNo, ECCNo, EXREGNO, CEXRANGE, CEXDIV, COMMRATE, VATNo, CommonAc, RowId, UpLoadDate, ChequeReport, EntryBy, EntryDate, EntryType, EntryStatus, ApproveBy, ApproveDate, MoveToLog, IsDeleted, MoveToLogDate, Status, SisterConcernYn, UID, SalesTaxPostingGroup, ExcisePostingGroup, EntryTaxPostingGroup, TDSCat_Description, MasterType, Currency, SisterConcernSite, CostCenter, Parent, LedgerGroup, Zone, DuplicateTIN, Distributor, STNo, IECCode, Range, Division, PartyType, PartyCat, ECCCode, Excise, FBTOnPer, FBTPer, PolicyNo, Transfered, DivisionList, Upline, Department, Designation, Guarantor, InsideOutside, DrugLicenseNo, PartyRateGroup)
                    VALUES('MAINGODOWN', '1', 'D', '|1|', NULL, 'Main Godown', 'Main Godown', '0017', '', 'MG', 'STOCK', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 'Rs.', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL);
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If


            If AgL.FillData("Select * from SubGroup Where SubCode='IIGST5'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    Insert into SubGroup(SubCode, ManualCode, Site_Code, Div_Code, SiteList, Name, DispName, GroupCode, GroupNature, Nature)
                    VALUES('IIGST5','IIGST5','1','D','|1|','Input IGST 5%','Input IGST 5%','0014', 'L','TAX');
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If

            If AgL.FillData("Select * from SubGroup Where SubCode='IIGST12'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    Insert into SubGroup(SubCode, ManualCode, Site_Code, Div_Code, SiteList, Name, DispName, GroupCode, GroupNature, Nature)
                    VALUES('IIGST12','IIGST12','1','D','|1|','Input IGST 12%','Input IGST 12%','0014', 'L','TAX');
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If

            If AgL.FillData("Select * from SubGroup Where SubCode='IIGST18'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    Insert into SubGroup(SubCode, ManualCode, Site_Code, Div_Code, SiteList, Name, DispName, GroupCode, GroupNature, Nature)
                    VALUES('IIGST18','IIGST18','1','D','|1|','Input IGST 18%','Input IGST 18%','0014', 'L','TAX');
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If


            If AgL.FillData("Select * from SubGroup Where SubCode='IIGST28'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    Insert into SubGroup(SubCode, ManualCode, Site_Code, Div_Code, SiteList, Name, DispName, GroupCode, GroupNature, Nature)
                    VALUES('IIGST28','IIGST28','1','D','|1|','Input IGST 28%','Input IGST 28%','0014', 'L','TAX');
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If


            If AgL.FillData("Select * from SubGroup Where SubCode='ICGST5'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    Insert into SubGroup(SubCode, ManualCode, Site_Code, Div_Code, SiteList, Name, DispName, GroupCode, GroupNature, Nature)
                    VALUES('ICGST5','ICGST5','1','D','|1|','Input CGST 5%','Input CGST 5%','0014', 'L','TAX');
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If

            If AgL.FillData("Select * from SubGroup Where SubCode='ICGST12'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    Insert into SubGroup(SubCode, ManualCode, Site_Code, Div_Code, SiteList, Name, DispName, GroupCode, GroupNature, Nature)
                    VALUES('ICGST12','ICGST12','1','D','|1|','Input CGST 12%','Input CGST 12%','0014', 'L','TAX');
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If

            If AgL.FillData("Select * from SubGroup Where SubCode='ICGST18'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    Insert into SubGroup(SubCode, ManualCode, Site_Code, Div_Code, SiteList, Name, DispName, GroupCode, GroupNature, Nature)
                    VALUES('ICGST18','ICGST18','1','D','|1|','Input CGST 18%','Input CGST 18%','0014', 'L','TAX');
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If

            If AgL.FillData("Select * from SubGroup Where SubCode='ICGST28'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    Insert into SubGroup(SubCode, ManualCode, Site_Code, Div_Code, SiteList, Name, DispName, GroupCode, GroupNature, Nature)
                    VALUES('ICGST28','ICGST28','1','D','|1|','Input CGST 28%','Input CGST 28%','0014', 'L','TAX');
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If


            If AgL.FillData("Select * from SubGroup Where SubCode='ISGST5'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    Insert into SubGroup(SubCode, ManualCode, Site_Code, Div_Code, SiteList, Name, DispName, GroupCode, GroupNature, Nature)
                    VALUES('ISGST5','ISGST5','1','D','|1|','Input SGST 5%','Input SGST 5%','0014', 'L','TAX');
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If

            If AgL.FillData("Select * from SubGroup Where SubCode='ISGST12'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    Insert into SubGroup(SubCode, ManualCode, Site_Code, Div_Code, SiteList, Name, DispName, GroupCode, GroupNature, Nature)
                    VALUES('ISGST12','ISGST12','1','D','|1|','Input SGST 12%','Input SGST 12%','0014', 'L','TAX');
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If

            If AgL.FillData("Select * from SubGroup Where SubCode='ISGST18'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    Insert into SubGroup(SubCode, ManualCode, Site_Code, Div_Code, SiteList, Name, DispName, GroupCode, GroupNature, Nature)
                    VALUES('ISGST18','ISGST18','1','D','|1|','Input SGST 18%','Input SGST 18%','0014', 'L','TAX');
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If

            If AgL.FillData("Select * from SubGroup Where SubCode='ISGST28'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    Insert into SubGroup(SubCode, ManualCode, Site_Code, Div_Code, SiteList, Name, DispName, GroupCode, GroupNature, Nature)
                    VALUES('ISGST28','ISGST28','1','D','|1|','Input SGST 28%','Input SGST 28%','0014', 'L','TAX');
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If








            If AgL.FillData("Select * from SubGroup Where SubCode='ROIGST5'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    Insert into SubGroup(SubCode, ManualCode, Site_Code, Div_Code, SiteList, Name, DispName, GroupCode, GroupNature, Nature)
                    VALUES('ROIGST5','ROIGST5','1','D','|1|','Output IGST 5% Registered','Output IGST 5% Registered','0014', 'L','TAX');
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If

            If AgL.FillData("Select * from SubGroup Where SubCode='ROIGST12'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    Insert into SubGroup(SubCode, ManualCode, Site_Code, Div_Code, SiteList, Name, DispName, GroupCode, GroupNature, Nature)
                    VALUES('ROIGST12','ROIGST12','1','D','|1|','Output IGST 12% Registered','Output IGST 12% Registered','0014', 'L','TAX');
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If

            If AgL.FillData("Select * from SubGroup Where SubCode='ROIGST18'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    Insert into SubGroup(SubCode, ManualCode, Site_Code, Div_Code, SiteList, Name, DispName, GroupCode, GroupNature, Nature)
                    VALUES('ROIGST18','ROIGST18','1','D','|1|','Output IGST 18% Registered','Output IGST 18% Registered','0014', 'L','TAX');
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If

            If AgL.FillData("Select * from SubGroup Where SubCode='ROIGST28'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    Insert into SubGroup(SubCode, ManualCode, Site_Code, Div_Code, SiteList, Name, DispName, GroupCode, GroupNature, Nature)
                    VALUES('ROIGST28','ROIGST28','1','D','|1|','Output IGST 28% Registered','Output IGST 28% Registered','0014', 'L','TAX');
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If

            If AgL.FillData("Select * from SubGroup Where SubCode='ROCGST5'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    Insert into SubGroup(SubCode, ManualCode, Site_Code, Div_Code, SiteList, Name, DispName, GroupCode, GroupNature, Nature)
                    VALUES('ROCGST5','ROCGST5','1','D','|1|','Output CGST 5% Registered','Output CGST 5% Registered','0014', 'L','TAX');
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If


            If AgL.FillData("Select * from SubGroup Where SubCode='ROCGST12'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    Insert into SubGroup(SubCode, ManualCode, Site_Code, Div_Code, SiteList, Name, DispName, GroupCode, GroupNature, Nature)
                    VALUES('ROCGST12','ROCGST12','1','D','|1|','Output CGST 12% Registered','Output CGST 12% Registered','0014', 'L','TAX');
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If

            If AgL.FillData("Select * from SubGroup Where SubCode='ROCGST18'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    Insert into SubGroup(SubCode, ManualCode, Site_Code, Div_Code, SiteList, Name, DispName, GroupCode, GroupNature, Nature)
                    VALUES('ROCGST18','ROCGST18','1','D','|1|','Output CGST 18% Registered','Output CGST 18% Registered','0014', 'L','TAX');
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If


            If AgL.FillData("Select * from SubGroup Where SubCode='ROCGST28'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                        Insert into SubGroup(SubCode, ManualCode, Site_Code, Div_Code, SiteList, Name, DispName, GroupCode, GroupNature, Nature)
                        VALUES('ROCGST28','ROCGST28','1','D','|1|','Output CGST 28% Registered','Output CGST 28% Registered','0014', 'L','TAX');
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If


            If AgL.FillData("Select * from SubGroup Where SubCode='ROSGST5'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    Insert into SubGroup(SubCode, ManualCode, Site_Code, Div_Code, SiteList, Name, DispName, GroupCode, GroupNature, Nature)
                    VALUES('ROSGST5','ROSGST5','1','D','|1|','Output SGST 5% Registered','Output SGST 5% Registered','0014', 'L','TAX');
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If

            If AgL.FillData("Select * from SubGroup Where SubCode='ROSGST12'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    Insert into SubGroup(SubCode, ManualCode, Site_Code, Div_Code, SiteList, Name, DispName, GroupCode, GroupNature, Nature)
                    VALUES('ROSGST12','ROSGST12','1','D','|1|','Output SGST 12% Registered','Output SGST 12% Registered','0014', 'L','TAX');
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If

            If AgL.FillData("Select * from SubGroup Where SubCode='ROSGST18'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    Insert into SubGroup(SubCode, ManualCode, Site_Code, Div_Code, SiteList, Name, DispName, GroupCode, GroupNature, Nature)
                    VALUES('ROSGST18','ROSGST18','1','D','|1|','Output SGST 18% Registered','Output SGST 18% Registered','0014', 'L','TAX');
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If

            If AgL.FillData("Select * from SubGroup Where SubCode='ROSGST28'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    Insert into SubGroup(SubCode, ManualCode, Site_Code, Div_Code, SiteList, Name, DispName, GroupCode, GroupNature, Nature)
                    VALUES('ROSGST28','ROSGST28','1','D','|1|','Output SGST 28% Registered','Output SGST 28% Registered','0014', 'L','TAX');
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If










            If AgL.FillData("Select * from SubGroup Where SubCode='UOIGST5'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    Insert into SubGroup(SubCode, ManualCode, Site_Code, Div_Code, SiteList, Name, DispName, GroupCode, GroupNature, Nature)
                    VALUES('UOIGST5','UOIGST5','1','D','|1|','Output IGST 5% Unregistered','Output IGST 5% Unregistered','0014', 'L','TAX');
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If

            If AgL.FillData("Select * from SubGroup Where SubCode='UOIGST12'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    Insert into SubGroup(SubCode, ManualCode, Site_Code, Div_Code, SiteList, Name, DispName, GroupCode, GroupNature, Nature)
                    VALUES('UOIGST12','UOIGST12','1','D','|1|','Output IGST 12% Unregistered','Output IGST 12% Unregistered','0014', 'L','TAX');
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If

            If AgL.FillData("Select * from SubGroup Where SubCode='UOIGST18'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    Insert into SubGroup(SubCode, ManualCode, Site_Code, Div_Code, SiteList, Name, DispName, GroupCode, GroupNature, Nature)
                    VALUES('UOIGST18','UOIGST18','1','D','|1|','Output IGST 18% Unregistered','Output IGST 18% Unregistered','0014', 'L','TAX');
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If

            If AgL.FillData("Select * from SubGroup Where SubCode='UOIGST28'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    Insert into SubGroup(SubCode, ManualCode, Site_Code, Div_Code, SiteList, Name, DispName, GroupCode, GroupNature, Nature)
                    VALUES('UOIGST28','UOIGST28','1','D','|1|','Output IGST 28% Unregistered','Output IGST 28% Unregistered','0014', 'L','TAX');
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If

            If AgL.FillData("Select * from SubGroup Where SubCode='UOCGST5'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    Insert into SubGroup(SubCode, ManualCode, Site_Code, Div_Code, SiteList, Name, DispName, GroupCode, GroupNature, Nature)
                    VALUES('UOCGST5','UOCGST5','1','D','|1|','Output CGST 5% Unregistered','Output CGST 5% Unregistered','0014', 'L','TAX');
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If


            If AgL.FillData("Select * from SubGroup Where SubCode='UOCGST12'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    Insert into SubGroup(SubCode, ManualCode, Site_Code, Div_Code, SiteList, Name, DispName, GroupCode, GroupNature, Nature)
                    VALUES('UOCGST12','UOCGST12','1','D','|1|','Output CGST 12% Unregistered','Output CGST 12% Unregistered','0014', 'L','TAX');
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If

            If AgL.FillData("Select * from SubGroup Where SubCode='UOCGST18'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    Insert into SubGroup(SubCode, ManualCode, Site_Code, Div_Code, SiteList, Name, DispName, GroupCode, GroupNature, Nature)
                    VALUES('UOCGST18','UOCGST18','1','D','|1|','Output CGST 18% Unregistered','Output CGST 18% Unregistered','0014', 'L','TAX');
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If


            If AgL.FillData("Select * from SubGroup Where SubCode='UOCGST28'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                        Insert into SubGroup(SubCode, ManualCode, Site_Code, Div_Code, SiteList, Name, DispName, GroupCode, GroupNature, Nature)
                        VALUES('UOCGST28','UOCGST28','1','D','|1|','Output CGST 28% Unregistered','Output CGST 28% Unregistered','0014', 'L','TAX');
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If


            If AgL.FillData("Select * from SubGroup Where SubCode='UOSGST5'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    Insert into SubGroup(SubCode, ManualCode, Site_Code, Div_Code, SiteList, Name, DispName, GroupCode, GroupNature, Nature)
                    VALUES('UOSGST5','UOSGST5','1','D','|1|','Output SGST 5% Unregistered','Output SGST 5% Unregistered','0014', 'L','TAX');
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If

            If AgL.FillData("Select * from SubGroup Where SubCode='UOSGST12'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    Insert into SubGroup(SubCode, ManualCode, Site_Code, Div_Code, SiteList, Name, DispName, GroupCode, GroupNature, Nature)
                    VALUES('UOSGST12','UOSGST12','1','D','|1|','Output SGST 12% Unregistered','Output SGST 12% Unregistered','0014', 'L','TAX');
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If

            If AgL.FillData("Select * from SubGroup Where SubCode='UOSGST18'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    Insert into SubGroup(SubCode, ManualCode, Site_Code, Div_Code, SiteList, Name, DispName, GroupCode, GroupNature, Nature)
                    VALUES('UOSGST18','UOSGST18','1','D','|1|','Output SGST 18% Unregistered','Output SGST 18% Unregistered','0014', 'L','TAX');
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If

            If AgL.FillData("Select * from SubGroup Where SubCode='UOSGST28'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    Insert into SubGroup(SubCode, ManualCode, Site_Code, Div_Code, SiteList, Name, DispName, GroupCode, GroupNature, Nature)
                    VALUES('UOSGST28','UOSGST28','1','D','|1|','Output SGST 28% Unregistered','Output SGST 28% Unregistered','0014', 'L','TAX');
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If












            If AgL.FillData("Select * from SubGroup Where SubCode='PURWR0'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    Insert into SubGroup(SubCode, ManualCode, Site_Code, Div_Code, SiteList, Name, DispName, GroupCode, GroupNature, Nature)
                    VALUES('PURWR0','PURWR0','1','D','|1|','Purchase A/c Within State 0% Registered','Purchase A/c Within State 0% Registered','0024', Null,'Purchase');
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If

            If AgL.FillData("Select * from SubGroup Where SubCode='PURWR5'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    Insert into SubGroup(SubCode, ManualCode, Site_Code, Div_Code, SiteList, Name, DispName, GroupCode, GroupNature, Nature)
                    VALUES('PURWR5','PURWR5','1','D','|1|','Purchase A/c Within State 5% Registered','Purchase A/c Within State 5% Registered','0024', Null,'Purchase');
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If

            If AgL.FillData("Select * from SubGroup Where SubCode='PURWR12'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    Insert into SubGroup(SubCode, ManualCode, Site_Code, Div_Code, SiteList, Name, DispName, GroupCode, GroupNature, Nature)
                    VALUES('PURWR12','PURWR12','1','D','|1|','Purchase A/c Within State 12% Registered','Purchase A/c Within State 12% Registered','0024', Null,'Purchase');
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If

            If AgL.FillData("Select * from SubGroup Where SubCode='PURWR18'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    Insert into SubGroup(SubCode, ManualCode, Site_Code, Div_Code, SiteList, Name, DispName, GroupCode, GroupNature, Nature)
                    VALUES('PURWR18','PURWR18','1','D','|1|','Purchase A/c Within State 18% Registered','Purchase A/c Within State 18% Registered','0024', Null,'Purchase');
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If


            If AgL.FillData("Select * from SubGroup Where SubCode='PURWR28'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    Insert into SubGroup(SubCode, ManualCode, Site_Code, Div_Code, SiteList, Name, DispName, GroupCode, GroupNature, Nature)
                    VALUES('PURWR28','PURWR28','1','D','|1|','Purchase A/c Within State 28% Registered','Purchase A/c Within State 28% Registered','0024', Null,'Purchase');
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If


            If AgL.FillData("Select * from SubGroup Where SubCode='PUROR0'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    Insert into SubGroup(SubCode, ManualCode, Site_Code, Div_Code, SiteList, Name, DispName, GroupCode, GroupNature, Nature)
                    VALUES('PUROR0','PUROR0','1','D','|1|','Purchase A/c Outside State 0% Registered','Purchase A/c Outside State 0% Registered','0024', Null,'Purchase');
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If

            If AgL.FillData("Select * from SubGroup Where SubCode='PUROR5'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    Insert into SubGroup(SubCode, ManualCode, Site_Code, Div_Code, SiteList, Name, DispName, GroupCode, GroupNature, Nature)
                    VALUES('PUROR5','PUROR5','1','D','|1|','Purchase A/c Outside State 5% Registered','Purchase A/c Outside State 5% Registered','0024', Null,'Purchase');
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If

            If AgL.FillData("Select * from SubGroup Where SubCode='PUROR12'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    Insert into SubGroup(SubCode, ManualCode, Site_Code, Div_Code, SiteList, Name, DispName, GroupCode, GroupNature, Nature)
                    VALUES('PUROR12','PUROR12','1','D','|1|','Purchase A/c Outside State 12% Registered','Purchase A/c Outside State 12% Registered','0024', Null,'Purchase');
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If

            If AgL.FillData("Select * from SubGroup Where SubCode='PUROR18'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    Insert into SubGroup(SubCode, ManualCode, Site_Code, Div_Code, SiteList, Name, DispName, GroupCode, GroupNature, Nature)
                    VALUES('PUROR18','PUROR18','1','D','|1|','Purchase A/c Outside State 18% Registered','Purchase A/c Outside State 18% Registered','0024', Null,'Purchase');
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If

            If AgL.FillData("Select * from SubGroup Where SubCode='PUROR28'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    Insert into SubGroup(SubCode, ManualCode, Site_Code, Div_Code, SiteList, Name, DispName, GroupCode, GroupNature, Nature)
                    VALUES('PUROR28','PUROR28','1','D','|1|','Purchase A/c Outside State 28% Registered','Purchase A/c Outside State 28% Registered','0024', Null,'Purchase');
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If

            If AgL.FillData("Select * from SubGroup Where SubCode='PURWU0'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    Insert into SubGroup(SubCode, ManualCode, Site_Code, Div_Code, SiteList, Name, DispName, GroupCode, GroupNature, Nature)
                    VALUES('PURWU0','PURWU0','1','D','|1|','Purchase A/c Within State 0% Unregistered','Purchase A/c Within State 0% Unregistered','0024', Null,'Purchase');
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If


            If AgL.FillData("Select * from SubGroup Where SubCode='PURWU5'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    Insert into SubGroup(SubCode, ManualCode, Site_Code, Div_Code, SiteList, Name, DispName, GroupCode, GroupNature, Nature)
                    VALUES('PURWU5','PURWU5','1','D','|1|','Purchase A/c Within State 5% Unregistered','Purchase A/c Within State 5% Unregistered','0024', Null,'Purchase');
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If

            If AgL.FillData("Select * from SubGroup Where SubCode='PURWU12'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                        Insert into SubGroup(SubCode, ManualCode, Site_Code, Div_Code, SiteList, Name, DispName, GroupCode, GroupNature, Nature)
                        VALUES('PURWU12','PURWU12','1','D','|1|','Purchase A/c Within State 12% Unregistered','Purchase A/c Within State 12% Unregistered','0024', Null,'Purchase');
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If

            If AgL.FillData("Select * from SubGroup Where SubCode='PURWU18'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    Insert into SubGroup(SubCode, ManualCode, Site_Code, Div_Code, SiteList, Name, DispName, GroupCode, GroupNature, Nature)
                    VALUES('PURWU18','PURWU18','1','D','|1|','Purchase A/c Within State 18% Unregistered','Purchase A/c Within State 18% Unregistered','0024', Null,'Purchase');
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If

            If AgL.FillData("Select * from SubGroup Where SubCode='PURWU28'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    Insert into SubGroup(SubCode, ManualCode, Site_Code, Div_Code, SiteList, Name, DispName, GroupCode, GroupNature, Nature)
                    VALUES('PURWU28','PURWU28','1','D','|1|','Purchase A/c Within State 28% Unregistered','Purchase A/c Within State 28% Unregistered','0024', Null,'Purchase');
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If

            If AgL.FillData("Select * from SubGroup Where SubCode='PUROU0'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    Insert into SubGroup(SubCode, ManualCode, Site_Code, Div_Code, SiteList, Name, DispName, GroupCode, GroupNature, Nature)
                    VALUES('PUROU0','PUROU0','1','D','|1|','Purchase A/c Outside State 0% Unregistered','Purchase A/c Outside State 0% Unregistered','0024', Null,'Purchase');
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If

            If AgL.FillData("Select * from SubGroup Where SubCode='PUROU5'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    Insert into SubGroup(SubCode, ManualCode, Site_Code, Div_Code, SiteList, Name, DispName, GroupCode, GroupNature, Nature)
                    VALUES('PUROU5','PUROU5','1','D','|1|','Purchase A/c Outside State 5% Unregistered','Purchase A/c Outside State 5% Unregistered','0024', Null,'Purchase');
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If


            If AgL.FillData("Select * from SubGroup Where SubCode='PUROU12'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    Insert into SubGroup(SubCode, ManualCode, Site_Code, Div_Code, SiteList, Name, DispName, GroupCode, GroupNature, Nature)
                    VALUES('PUROU12','PUROU12','1','D','|1|','Purchase A/c Outside State 12% Unregistered','Purchase A/c Outside State 12% Unregistered','0024', Null,'Purchase');
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If

            If AgL.FillData("Select * from SubGroup Where SubCode='PUROU18'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    Insert into SubGroup(SubCode, ManualCode, Site_Code, Div_Code, SiteList, Name, DispName, GroupCode, GroupNature, Nature)
                    VALUES('PUROU18','PUROU18','1','D','|1|','Purchase A/c Outside State 18% Unregistered','Purchase A/c Outside State 18% Unregistered','0024', Null,'Purchase');
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If

            If AgL.FillData("Select * from SubGroup Where SubCode='PUROU28'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    Insert into SubGroup(SubCode, ManualCode, Site_Code, Div_Code, SiteList, Name, DispName, GroupCode, GroupNature, Nature)
                    VALUES('PUROU28','PUROU28','1','D','|1|','Purchase A/c Outside State 28% Unregistered','Purchase A/c Outside State 28% Unregistered','0024', Null,'Purchase');
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If


            If AgL.FillData("Select * from SubGroup Where SubCode='SALEWR0'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    Insert into SubGroup(SubCode, ManualCode, Site_Code, Div_Code, SiteList, Name, DispName, GroupCode, GroupNature, Nature)
                    VALUES('SALEWR0','SALEWR0','1','D','|1|','Sales A/c Within State 0% Registered','Sales A/c Within State 0% Registered','0023', Null,'Sales');
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If

            If AgL.FillData("Select * from SubGroup Where SubCode='SALEWR5'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    Insert into SubGroup(SubCode, ManualCode, Site_Code, Div_Code, SiteList, Name, DispName, GroupCode, GroupNature, Nature)
                    VALUES('SALEWR5','SALEWR5','1','D','|1|','Sales A/c Within State 5% Registered','Sales A/c Within State 5% Registered','0023', Null,'Sales');
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If

            If AgL.FillData("Select * from SubGroup Where SubCode='SALEWR12'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    Insert into SubGroup(SubCode, ManualCode, Site_Code, Div_Code, SiteList, Name, DispName, GroupCode, GroupNature, Nature)
                    VALUES('SALEWR12','SALEWR12','1','D','|1|','Sales A/c Within State 12% Registered','Sales A/c Within State 12% Registered','0023', Null,'Sales');
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If

            If AgL.FillData("Select * from SubGroup Where SubCode='SALEWR18'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    Insert into SubGroup(SubCode, ManualCode, Site_Code, Div_Code, SiteList, Name, DispName, GroupCode, GroupNature, Nature)
                    VALUES('SALEWR18','SALEWR18','1','D','|1|','Sales A/c Within State 18% Registered','Sales A/c Within State 18% Registered','0023', Null,'Sales');
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If

            If AgL.FillData("Select * from SubGroup Where SubCode='SALEWR28'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    Insert into SubGroup(SubCode, ManualCode, Site_Code, Div_Code, SiteList, Name, DispName, GroupCode, GroupNature, Nature)
                    VALUES('SALEWR28','SALEWR28','1','D','|1|','Sales A/c Within State 28% Registered','Sales A/c Within State 28% Registered','0023', Null,'Sales');
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If


            If AgL.FillData("Select * from SubGroup Where SubCode='SALEOR0'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    Insert into SubGroup(SubCode, ManualCode, Site_Code, Div_Code, SiteList, Name, DispName, GroupCode, GroupNature, Nature)
                    VALUES('SALEOR0','SALEOR0','1','D','|1|','Sales A/c Outside State 0% Registered','Sales A/c Outside State 0% Registered','0023', Null,'Sales');
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If

            If AgL.FillData("Select * from SubGroup Where SubCode='SALEOR5'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    Insert into SubGroup(SubCode, ManualCode, Site_Code, Div_Code, SiteList, Name, DispName, GroupCode, GroupNature, Nature)
                    VALUES('SALEOR5','SALEOR5','1','D','|1|','Sales A/c Outside State 5% Registered','Sales A/c Outside State 5% Registered','0023', Null,'Sales');
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If

            If AgL.FillData("Select * from SubGroup Where SubCode='SALEOR12'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    Insert into SubGroup(SubCode, ManualCode, Site_Code, Div_Code, SiteList, Name, DispName, GroupCode, GroupNature, Nature)
                    VALUES('SALEOR12','SALEOR12','1','D','|1|','Sales A/c Outside State 12% Registered','Sales A/c Outside State 12% Registered','0023', Null,'Sales');
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If

            If AgL.FillData("Select * from SubGroup Where SubCode='SALEOR18'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    Insert into SubGroup(SubCode, ManualCode, Site_Code, Div_Code, SiteList, Name, DispName, GroupCode, GroupNature, Nature)
                    VALUES('SALEOR18','SALEOR18','1','D','|1|','Sales A/c Outside State 18% Registered','Sales A/c Outside State 18% Registered','0023', Null,'Sales');
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If

            If AgL.FillData("Select * from SubGroup Where SubCode='SALEOR28'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    Insert into SubGroup(SubCode, ManualCode, Site_Code, Div_Code, SiteList, Name, DispName, GroupCode, GroupNature, Nature)
                    VALUES('SALEOR28','SALEOR28','1','D','|1|','Sales A/c Outside State 28% Registered','Sales A/c Outside State 28% Registered','0023', Null,'Sales');
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If

            If AgL.FillData("Select * from SubGroup Where SubCode='SALEWU0'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    Insert into SubGroup(SubCode, ManualCode, Site_Code, Div_Code, SiteList, Name, DispName, GroupCode, GroupNature, Nature)
                    VALUES('SALEWU0','SALEWU0','1','D','|1|','Sales A/c Within State 0% Unregistered','Sales A/c Within State 0% Unregistered','0023', Null,'Sales');
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If

            If AgL.FillData("Select * from SubGroup Where SubCode='SALEWU5'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    Insert into SubGroup(SubCode, ManualCode, Site_Code, Div_Code, SiteList, Name, DispName, GroupCode, GroupNature, Nature)
                    VALUES('SALEWU5','SALEWU5','1','D','|1|','Sales A/c Within State 5% Unregistered','Sales A/c Within State 5% Unregistered','0023', Null,'Sales');
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If

            If AgL.FillData("Select * from SubGroup Where SubCode='SALEWU12'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    Insert into SubGroup(SubCode, ManualCode, Site_Code, Div_Code, SiteList, Name, DispName, GroupCode, GroupNature, Nature)
                    VALUES('SALEWU12','SALEWU12','1','D','|1|','Sales A/c Within State 12% Unregistered','Sales A/c Within State 12% Unregistered','0023', Null,'Sales');
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If

            If AgL.FillData("Select * from SubGroup Where SubCode='SALEWU18'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    Insert into SubGroup(SubCode, ManualCode, Site_Code, Div_Code, SiteList, Name, DispName, GroupCode, GroupNature, Nature)
                    VALUES('SALEWU18','SALEWU18','1','D','|1|','Sales A/c Within State 18% Unregistered','Sales A/c Within State 18% Unregistered','0023', Null,'Sales');
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If

            If AgL.FillData("Select * from SubGroup Where SubCode='SALEWU28'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    Insert into SubGroup(SubCode, ManualCode, Site_Code, Div_Code, SiteList, Name, DispName, GroupCode, GroupNature, Nature)
                    VALUES('SALEWU28','SALEWU28','1','D','|1|','Sales A/c Within State 28% Unregistered','Sales A/c Within State 28% Unregistered','0023', Null,'Sales');
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If

            If AgL.FillData("Select * from SubGroup Where SubCode='SALEOU0'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    Insert into SubGroup(SubCode, ManualCode, Site_Code, Div_Code, SiteList, Name, DispName, GroupCode, GroupNature, Nature)
                    VALUES('SALEOU0','SALEOU0','1','D','|1|','Sales A/c Outside State 0% Unregistered','Sales A/c Outside State 0% Unregistered','0023', Null,'Sales');
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If

            If AgL.FillData("Select * from SubGroup Where SubCode='SALEOU5'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    Insert into SubGroup(SubCode, ManualCode, Site_Code, Div_Code, SiteList, Name, DispName, GroupCode, GroupNature, Nature)
                    VALUES('SALEOU5','SALEOU5','1','D','|1|','Sales A/c Outside State 5% Unregistered','Sales A/c Outside State 5% Unregistered','0023', Null,'Sales');
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If

            If AgL.FillData("Select * from SubGroup Where SubCode='SALEOU12'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    Insert into SubGroup(SubCode, ManualCode, Site_Code, Div_Code, SiteList, Name, DispName, GroupCode, GroupNature, Nature)
                    VALUES('SALEOU12','SALEOU12','1','D','|1|','Sales A/c Outside State 12% Unregistered','Sales A/c Outside State 12% Unregistered','0023', Null,'Sales');
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If

            If AgL.FillData("Select * from SubGroup Where SubCode='SALEOU18'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    Insert into SubGroup(SubCode, ManualCode, Site_Code, Div_Code, SiteList, Name, DispName, GroupCode, GroupNature, Nature)
                    VALUES('SALEOU18','SALEOU18','1','D','|1|','Sales A/c Outside State 18% Unregistered','Sales A/c Outside State 18% Unregistered','0023', Null,'Sales');
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If

            If AgL.FillData("Select * from SubGroup Where SubCode='SALEOU28'", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    Insert into SubGroup(SubCode, ManualCode, Site_Code, Div_Code, SiteList, Name, DispName, GroupCode, GroupNature, Nature)
                    VALUES('SALEOU28','SALEOU28','1','D','|1|','Sales A/c Outside State 28% Unregistered','Sales A/c Outside State 28% Unregistered','0023', Null,'Sales');
                    "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If

        Catch ex As Exception
            MsgBox(ex.Message & " [FSeedTable_Subgroup]")
        End Try


    End Sub

    Private Sub FCreateTable_Enviro()
        Dim mQry As String
        Try
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

        Catch ex As Exception
            MsgBox(ex.Message & " [FCreateTable_Enviro]")
        End Try
    End Sub

    Private Sub FCreateTable_Area()
        Dim mQry As String
        Try
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
        Catch ex As Exception
            MsgBox(ex.Message & "  [FCreateTable_Area]")
        End Try

    End Sub

    Private Sub FCreateTable_Reason()
        Dim mQry As String
        Try
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
        Catch ex As Exception
            MsgBox(ex.Message & "  [FCreateTable_Reason]")
        End Try

    End Sub


    Private Sub FCreateTable_SiteMast()
        Dim mQry As String
        Try
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
        Catch ex As Exception
            MsgBox(ex.Message & "  [FCreateTable_SiteMast]")
        End Try
    End Sub

    Private Sub FSeedTable_SiteMast()
        Dim mQry As String
        Try
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
        Catch ex As Exception
            MsgBox(ex.Message & "  [FSeedTable_SiteMast]")
        End Try

    End Sub

    Private Sub FCreateTable_UserMast()
        Dim mQry As String
        Try
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
        Catch ex As Exception
            MsgBox(ex.Message & "  [FCreateTable_UserMast]")
        End Try

    End Sub

    Private Sub FSeedTable_UserMast()
        Dim mQry As String
        Try
            If AgL.FillData("Select * from UserMast limit 1", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    INSERT INTO UserMast
                    (USER_NAME, Code, PASSWD, Description, Admin, RowId, UpLoadDate, ModuleList, SeniorName, MainStreamCode, EMail, Mobile, IsActive, InActiveDate)
                    VALUES('SA', '1', '@', 'CEO', 'Y', 1, NULL, NULL, NULL, '010', NULL, NULL, 1, NULL);

                   "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If
        Catch ex As Exception
            MsgBox(ex.Message & "  [FSeedTable_UserMast]")
        End Try

    End Sub

    Private Sub FCreateTable_UserSite()
        Dim mQry As String
        Try
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
        Catch ex As Exception
            MsgBox(ex.Message & "  [FCreateTable_UserSite]")
        End Try

    End Sub

    Private Sub FSeedTable_UserSite()
        Dim mQry As String
        Try
            If AgL.FillData("Select * from UserSite limit 1", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                    INSERT INTO UserSite
                    (User_Name, CompCode, Sitelist, UpLoadDate, DivisionList)
                    VALUES('SA', '1', '|1|', NULL, '|D|');

                   "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If
        Catch ex As Exception
            MsgBox(ex.Message & "  [FSeedTable_UserSite]")
        End Try

    End Sub

    Private Sub FCreateTable_User_Permission()
        Dim mQry As String
        Try
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
        Catch ex As Exception
            MsgBox(ex.Message & "  [FCreateTable_User_Permission]")
        End Try

    End Sub

    Private Sub FSeedTable_User_Permission()
        Dim mQry As String
        Try
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
        Catch ex As Exception
            MsgBox(ex.Message & "  [FSeedTable_User_Permission]")
        End Try

    End Sub

    Private Sub FCreateTable_User_Control_Permission()
        Dim mQry As String
        Try
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
        Catch ex As Exception
            MsgBox(ex.Message & "  [FCreateTable_User_Control_Permission]")
        End Try

    End Sub


    Private Sub FSeedTable_SaleInvoiceSetting()
        Dim mQry As String
        Try
            If AgL.FillData("Select * from SaleInvoiceSetting limit 1", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                            INSERT INTO SaleInvoiceSetting
                            (Code, V_Type, Div_Code, Site_Code, IsPostedInStock, 
                            IsPostedInLedger, IsVisible_ItemCode, IsVisible_ItemCategory, IsVisible_ItemGroup, IsVisible_Specification, 
                            IsVisible_Dimension1, IsVisible_Dimension2, IsVisible_Dimension3, IsVisible_Dimension4, IsVisible_Manufacturer, 
                            IsVisible_LotNo, IsVisible_BaleNo, IsVisible_Process, IsVisible_UnitMultiplier, IsEditable_DealUnitMultiplier, 
                            IsVisible_DealQty, IsEditable_DealQty, IsVisible_DealUnit, IsEditable_DealUnit, IsVisible_Qty, 
                            IsVisible_FreeQty, IsVisible_Rate, IsEditable_Rate, ItemHelpType, FilterInclude_Process, 
                            FilterExclude_AcGroup, FilterInclude_ItemType, FilterInclude_ItemGroup, FilterExclude_ItemGroup, FilterInclude_Item, 
                            FilterExclude_Item, FilterInclude_AcGroup, Default_RateType)
                            VALUES('M0001', 'SINV', Null, Null, '1', 
                            '1', '0', '1', '1', '0', 
                            '0', '0', '0', '0', '0', 
                            '0', '0', '0', '0', '0', 
                            '0', '0', '0', '1', '0', 
                            '0', '1', '1', 'SALE', Null, 
                            Null, Null, Null, Null, Null, 
                            Null, Null, 'DHARA');
                "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If
        Catch ex As Exception
            MsgBox(ex.Message & "  [FSeedTable_SaleInvoiceSetting]")
        End Try

    End Sub

    Private Sub FSeedTable_PostingGroupSalesTaxItem()
        Dim mQry As String
        Try
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
        Catch ex As Exception
            MsgBox(ex.Message & "  [FSeedTable_PostingGroupSalesTaxItem]")
        End Try

    End Sub


    Private Sub FCreateTable_StockHeadSetting()
        Dim mQry As String
        Try
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

        Catch ex As Exception
            MsgBox(ex.Message & "  [FCreateTable_StockHeadSetting]")
        End Try

    End Sub

    Private Sub FCreateTable_PurchaseInvoiceSetting()
        Dim mQry As String
        Try
            If Not AgL.IsTableExist("PurchaseInvoiceSetting", AgL.GcnMain) Then
                mQry = " CREATE TABLE [PurchaseInvoiceSetting] ( Code nVarchar(10) PRIMARY KEY); "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If
            AgL.AddFieldSqlite("PurchaseInvoiceSetting", "V_Type", "nVarchar(5)", "", True, " references Voucher_Type(V_Type) ")
            AgL.AddFieldSqlite("PurchaseInvoiceSetting", "Div_Code", "nVarchar(1)", "", True, " references Division(Div_Code) ")
            AgL.AddFieldSqlite("PurchaseInvoiceSetting", "Site_Code", "nVarchar(2)", "", True, " references SiteMast(Code) ")
            AgL.AddFieldSqlite("PurchaseInvoiceSetting", "IsPostedInStock", "bit", "1", False)
            AgL.AddFieldSqlite("PurchaseInvoiceSetting", "IsPostedInLedger", "bit", "1", False)
            AgL.AddFieldSqlite("PurchaseInvoiceSetting", "IsVisible_ItemCode", "bit", "0", False)
            AgL.AddFieldSqlite("PurchaseInvoiceSetting", "IsVisible_ItemCategory", "bit", "0", False)
            AgL.AddFieldSqlite("PurchaseInvoiceSetting", "IsVisible_ItemGroup", "bit", "0", False)
            AgL.AddFieldSqlite("PurchaseInvoiceSetting", "IsVisible_Specification", "bit", "0", False)
            AgL.AddFieldSqlite("PurchaseInvoiceSetting", "IsVisible_Dimension1", "bit", "0", False)
            AgL.AddFieldSqlite("PurchaseInvoiceSetting", "IsVisible_Dimension2", "bit", "0", False)
            AgL.AddFieldSqlite("PurchaseInvoiceSetting", "IsVisible_Dimension3", "bit", "0", False)
            AgL.AddFieldSqlite("PurchaseInvoiceSetting", "IsVisible_Dimension4", "bit", "0", False)
            AgL.AddFieldSqlite("PurchaseInvoiceSetting", "IsVisible_Qty", "bit", "1", False)
            AgL.AddFieldSqlite("PurchaseInvoiceSetting", "IsVisible_FreeQty", "bit", "0", False)
            AgL.AddFieldSqlite("PurchaseInvoiceSetting", "IsVisible_RejQty", "bit", "0", False)
            AgL.AddFieldSqlite("PurchaseInvoiceSetting", "IsVisible_Manufacturer", "bit", "0", False)
            AgL.AddFieldSqlite("PurchaseInvoiceSetting", "IsVisible_LotNo", "bit", "0", False)
            AgL.AddFieldSqlite("PurchaseInvoiceSetting", "IsVisible_BaleNo", "bit", "0", False)
            AgL.AddFieldSqlite("PurchaseInvoiceSetting", "IsVisible_Process", "bit", "0", False)
            AgL.AddFieldSqlite("PurchaseInvoiceSetting", "IsVisible_UnitMultiplier", "bit", "0", False)
            AgL.AddFieldSqlite("PurchaseInvoiceSetting", "IsEditable_DealUnitMultiplier", "bit", "0", False)
            AgL.AddFieldSqlite("PurchaseInvoiceSetting", "IsVisible_DealQty", "bit", "0", False)
            AgL.AddFieldSqlite("PurchaseInvoiceSetting", "IsEditable_DealQty", "bit", "0", False)
            AgL.AddFieldSqlite("PurchaseInvoiceSetting", "IsVisible_DealUnit", "bit", "0", False)
            AgL.AddFieldSqlite("PurchaseInvoiceSetting", "IsEditable_DealUnit", "bit", "1", False)
            AgL.AddFieldSqlite("PurchaseInvoiceSetting", "IsVisible_Rate", "bit", "1", False)
            AgL.AddFieldSqlite("PurchaseInvoiceSetting", "IsEditable_Rate", "bit", "1", False)
            AgL.AddFieldSqlite("PurchaseInvoiceSetting", "IsVisible_MRP", "bit", "0", False)
            AgL.AddFieldSqlite("PurchaseInvoiceSetting", "IsVisible_ProfitMarginPer", "bit", "0", False)
            AgL.AddFieldSqlite("PurchaseInvoiceSetting", "IsEditable_ProfitMarginPer", "bit", "1", False)
            AgL.AddFieldSqlite("PurchaseInvoiceSetting", "IsVisible_SaleRate", "bit", "0", False)
            AgL.AddFieldSqlite("PurchaseInvoiceSetting", "IsVisible_Deal", "bit", "0", False)
            AgL.AddFieldSqlite("PurchaseInvoiceSetting", "IsVisible_ExpiryDate", "bit", "0", False)
            AgL.AddFieldSqlite("PurchaseInvoiceSetting", "ItemHelpType", "nVarchar(20)", "", True)
            AgL.AddFieldSqlite("PurchaseInvoiceSetting", "FilterInclude_Process", "nVarchar(255)", "", True)
            AgL.AddFieldSqlite("PurchaseInvoiceSetting", "FilterExclude_AcGroup", "nVarchar(255)", "", True)
            AgL.AddFieldSqlite("PurchaseInvoiceSetting", "FilterInclude_ItemType", "nVarchar(255)", "", True)
            AgL.AddFieldSqlite("PurchaseInvoiceSetting", "FilterInclude_ItemGroup", "nVarchar(255)", "", True)
            AgL.AddFieldSqlite("PurchaseInvoiceSetting", "FilterExclude_ItemGroup", "nVarchar(255)", "", True)
            AgL.AddFieldSqlite("PurchaseInvoiceSetting", "FilterInclude_Item", "nVarchar(255)", "", True)
            AgL.AddFieldSqlite("PurchaseInvoiceSetting", "FilterExclude_Item", "nVarchar(255)", "", True)
            AgL.AddFieldSqlite("PurchaseInvoiceSetting", "FilterInclude_AcGroup", "nVarchar(255)", "", True)
            AgL.AddFieldSqlite("PurchaseInvoiceSetting", "FilterInclude_AcGroup", "nVarchar(255)", "", True)

        Catch ex As Exception
            MsgBox(ex.Message & "  [FCreateTable_PurchaseInvoiceSetting]")
        End Try

    End Sub

    Private Sub FCreateTable_SaleInvoiceLastTransactionValues()
        Dim mQry As String
        Try
            If Not AgL.IsTableExist("SaleInvoiceLastTransactionValues", AgL.GcnMain) Then
                mQry = " CREATE TABLE [SaleInvoiceLastTransactionValues] (SubCode nVarchar(10) PRIMARY KEY); "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If
            AgL.AddFieldSqlite("SaleInvoiceLastTransactionValues", "V_Type", "nVarchar(5)", "", True, " references Voucher_Type(V_Type) ")
            AgL.AddFieldSqlite("SaleInvoiceLastTransactionValues", "Div_Code", "nVarchar(1)", "", True, " references Division(Div_Code) ")
            AgL.AddFieldSqlite("SaleInvoiceLastTransactionValues", "Site_Code", "nVarchar(2)", "", True, " references SiteMast(Code) ")
            AgL.AddFieldSqlite("SaleInvoiceLastTransactionValues", "RateType", "nVarchar(6)", "", True, " references RateType(Code) ")
            AgL.AddFieldSqlite("SaleInvoiceLastTransactionValues", "Transporter", "nVarchar(6)", "", True, " references RateType(Code) ")
            AgL.AddFieldSqlite("SaleInvoiceLastTransactionValues", "TermsAndConditions", "nVarchar(1000)", "", True, " references RateType(Code)")

        Catch ex As Exception
            MsgBox(ex.Message & "  [FCreateTable_SaleInvoiceLastTransactionValues]")
        End Try

    End Sub

    Private Sub FCreateTable_SaleInvoiceSetting()
        Dim mQry As String
        Try
            If Not AgL.IsTableExist("SaleInvoiceSetting", AgL.GcnMain) Then
                mQry = " CREATE TABLE [SaleInvoiceSetting] ( Code nVarchar(10) PRIMARY KEY); "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If
            AgL.AddFieldSqlite("SaleInvoiceSetting", "V_Type", "nVarchar(5)", "", True, " references Voucher_Type(V_Type) ")
            AgL.AddFieldSqlite("SaleInvoiceSetting", "Div_Code", "nVarchar(1)", "", True, " references Division(Div_Code) ")
            AgL.AddFieldSqlite("SaleInvoiceSetting", "Site_Code", "nVarchar(2)", "", True, " references SiteMast(Code) ")
            AgL.AddFieldSqlite("SaleInvoiceSetting", "IsPostedInStock", "bit", "1", False)
            AgL.AddFieldSqlite("SaleInvoiceSetting", "IsPostedInLedger", "bit", "1", False)
            AgL.AddFieldSqlite("SaleInvoiceSetting", "IsVisible_ItemCode", "bit", "0", False)
            AgL.AddFieldSqlite("SaleInvoiceSetting", "IsVisible_ItemCategory", "bit", "0", False)
            AgL.AddFieldSqlite("SaleInvoiceSetting", "IsVisible_ItemGroup", "bit", "0", False)
            AgL.AddFieldSqlite("SaleInvoiceSetting", "IsVisible_Specification", "bit", "0", False)
            AgL.AddFieldSqlite("SaleInvoiceSetting", "IsVisible_Dimension1", "bit", "0", False)
            AgL.AddFieldSqlite("SaleInvoiceSetting", "IsVisible_Dimension2", "bit", "0", False)
            AgL.AddFieldSqlite("SaleInvoiceSetting", "IsVisible_Dimension3", "bit", "0", False)
            AgL.AddFieldSqlite("SaleInvoiceSetting", "IsVisible_Dimension4", "bit", "0", False)
            AgL.AddFieldSqlite("SaleInvoiceSetting", "IsVisible_Manufacturer", "bit", "0", False)
            AgL.AddFieldSqlite("SaleInvoiceSetting", "IsVisible_LotNo", "bit", "0", False)
            AgL.AddFieldSqlite("SaleInvoiceSetting", "IsVisible_BaleNo", "bit", "0", False)
            AgL.AddFieldSqlite("SaleInvoiceSetting", "IsVisible_Process", "bit", "0", False)
            AgL.AddFieldSqlite("SaleInvoiceSetting", "IsVisible_UnitMultiplier", "bit", "0", False)
            AgL.AddFieldSqlite("SaleInvoiceSetting", "IsEditable_DealUnitMultiplier", "bit", "0", False)
            AgL.AddFieldSqlite("SaleInvoiceSetting", "IsVisible_DealQty", "bit", "0", False)
            AgL.AddFieldSqlite("SaleInvoiceSetting", "IsEditable_DealQty", "bit", "0", False)
            AgL.AddFieldSqlite("SaleInvoiceSetting", "IsVisible_DealUnit", "bit", "0", False)
            AgL.AddFieldSqlite("SaleInvoiceSetting", "IsEditable_DealUnit", "bit", "1", False)
            AgL.AddFieldSqlite("SaleInvoiceSetting", "IsVisible_Qty", "bit", "0", False)
            AgL.AddFieldSqlite("SaleInvoiceSetting", "IsVisible_FreeQty", "bit", "0", False)
            AgL.AddFieldSqlite("SaleInvoiceSetting", "IsVisible_Rate", "bit", "1", False)
            AgL.AddFieldSqlite("SaleInvoiceSetting", "IsEditable_Rate", "bit", "1", False)
            AgL.AddFieldSqlite("SaleInvoiceSetting", "ItemHelpType", "nVarchar(20)", "SALE", True)
            AgL.AddFieldSqlite("SaleInvoiceSetting", "FilterInclude_Process", "nVarchar(255)", "", True)
            AgL.AddFieldSqlite("SaleInvoiceSetting", "FilterExclude_AcGroup", "nVarchar(255)", "", True)
            AgL.AddFieldSqlite("SaleInvoiceSetting", "FilterInclude_ItemType", "nVarchar(255)", "", True)
            AgL.AddFieldSqlite("SaleInvoiceSetting", "FilterInclude_ItemGroup", "nVarchar(255)", "", True)
            AgL.AddFieldSqlite("SaleInvoiceSetting", "FilterExclude_ItemGroup", "nVarchar(255)", "", True)
            AgL.AddFieldSqlite("SaleInvoiceSetting", "FilterInclude_Item", "nVarchar(255)", "", True)
            AgL.AddFieldSqlite("SaleInvoiceSetting", "FilterExclude_Item", "nVarchar(255)", "", True)
            AgL.AddFieldSqlite("SaleInvoiceSetting", "FilterInclude_AcGroup", "nVarchar(255)", "", True)
            AgL.AddFieldSqlite("SaleInvoiceSetting", "FilterInclude_AcGroup", "nVarchar(255)", "", True)
            AgL.AddFieldSqlite("SaleInvoiceSetting", "Default_RateType", "nVarchar(6)", "", True, " References RateType(Code) ")
            AgL.AddFieldSqlite("SaleInvoiceSetting", "Default_TermsAndConditions", "nVarchar(1000)", "", True)

        Catch ex As Exception
            MsgBox(ex.Message & "  [FCreateTable_SaleInvoiceSetting]")
        End Try

    End Sub

    Private Sub FCreateTable_SubgroupTypeSetting()
        Dim mQry As String
        Try
            If Not AgL.IsTableExist("SubgroupTypeSetting", AgL.GcnMain) Then
                mQry = " CREATE TABLE [SubgroupTypeSetting] (SubgroupType nVarchar(20) PRIMARY KEY); "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If
            'AgL.AddFieldSqlite("SubgroupTypeSetting", "SubgroupType", "nVarchar(20)", "", True, " references SubgroupType(SubgroupType) ")
            AgL.AddFieldSqlite("SubgroupTypeSetting", "Div_Code", "nVarchar(1)", "", True, " references Division(Div_Code) ")
            AgL.AddFieldSqlite("SubgroupTypeSetting", "Site_Code", "nVarchar(4)", "", True, " references SiteMast(Code) ")
            AgL.AddFieldSqlite("SubgroupTypeSetting", "IsVisible_ContactPerson", "bit", "0", True)
            AgL.AddFieldSqlite("SubgroupTypeSetting", "IsVisible_SalesTaxNo", "bit", "0", True)
            AgL.AddFieldSqlite("SubgroupTypeSetting", "IsVisible_PanNo", "bit", "0", True)
            AgL.AddFieldSqlite("SubgroupTypeSetting", "IsVisible_AadharNo", "bit", "0", True)
            AgL.AddFieldSqlite("SubgroupTypeSetting", "IsVisible_Parent", "bit", "0", True)
            AgL.AddFieldSqlite("SubgroupTypeSetting", "IsVisible_CreditLimit", "bit", "0", True)
            AgL.AddFieldSqlite("SubgroupTypeSetting", "Caption_Parent", "nVarchar(50)", "", True)
            AgL.AddFieldSqlite("SubgroupTypeSetting", "AcGroupCode", "nVarchar(4)", "", True, " References AcGroup(GroupCode) ")
        Catch ex As Exception
            MsgBox(ex.Message & "  [FCreateTable_SubgroupTypeSetting]")
        End Try

    End Sub


    Private Sub FCreateTable_Company()
        Dim mQry As String
        Try
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
        Catch ex As Exception
            MsgBox(ex.Message & "  [FCreateTable_Company]")
        End Try

    End Sub

    Private Sub FSeedTable_Company()
        Dim mQry As String
        Try
            If AgL.FillData("Select * from Company limit 1", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " INSERT INTO Company
                (Comp_Code, Div_Code, Comp_Name, CentralData_Path, PrevDBName, DbPrefix, Repo_Path, Start_Dt, End_Dt, address1, address2, city, pin, phone, fax, lstno, lstdate, cstno, cstdate, cyear, pyear, SerialKeyNo, SName, EMail, Gram, Desc1, Desc2, Desc3, ECCCode, ExDivision, ExRegNo, ExColl, ExRange, Desc4, VatNo, VatDate, TinNo, Site_Code, LogSiteCode, PANNo, State, U_Name, U_EntDt, U_AE, DeletedYN, Country, V_Prefix, NotificationNo, WorkAddress1, WorkAddress2, WorkCity, WorkCountry, WorkPin, WorkPhone, WorkFax, WebServer, WebUser, WebPassword, Webdatabase, RowId, UpLoadDate, UseSiteNameAsCompanyName, FileDbName, ImageDbName)
                VALUES('1', 'D', 'Auditor9 Solutions', 'D:\KC\Data\Auditor9', NULL, 'Cloth', NULL, '2017-04-01 00:00:00', '2018-03-31 00:00:00', '13/152 Parmat, Civil Lines', NULL, 'Kanpur', '208001', '05414226864', '-', NULL, NULL, '-', '12/Nov/2017', '2017-2018', '2016-2017', 'RA96082587', 'AAMC', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', '-', NULL, '2010-11-12 00:00:00', '09815400794', NULL, NULL, '-', 'U.P.', 'SA', '2008-04-01 00:00:00', 'E', 'N', 'INDIA', '2010', '-', '-', '-', '-', '-', '-', '-', '-', NULL, NULL, NULL, NULL, 1, NULL, 0, 'MedicalFiles', NULL);
                "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If
        Catch ex As Exception
            MsgBox(ex.Message & "  [FSeedTable_Company]")
        End Try

    End Sub

    Private Sub FSeedTable_PostingGroupSalesTaxParty()
        Dim mQry As String
        Try
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
        Catch ex As Exception
            MsgBox(ex.Message & "  [FSeedTable_PostingGroupSalesTaxParty]")
        End Try

    End Sub


    Private Sub FCreateTable_Division()
        Dim mQry As String
        Try
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
        Catch ex As Exception
            MsgBox(ex.Message & "  [FCreateTable_Division]")
        End Try

    End Sub

    Private Sub FSeedTable_Division()
        Dim mQry As String
        Try
            If AgL.FillData("Select * from Division limit 1", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " INSERT INTO Division
                    (Div_Code, Div_Name, DataPath, address1, address2, address3, city, pin, PreparedBy, U_EntDt, U_AE, Edit_Date, ModifiedBy, SitewiseV_No, RowId, UpLoadDate, ApprovedBy, ApprovedDate, GPX1, GPX2, GPN1, GPN2, ScopeOfWork)
                    VALUES('D', 'Main', 'MEDICAL_1', '-', '-', '-', 'Kanpur', '-', 'SA', '2008-04-01 00:00:00', 'E', '2010-05-21 00:00:00', 'sa', 1, 1, NULL, NULL, NULL, NULL, NULL, NULL, NULL,'+CLOTH');
                "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If
        Catch ex As Exception
            MsgBox(ex.Message & "  [FSeedTable_Division]")
        End Try

    End Sub

    Private Sub FCreateTable_City()
        Dim mQry As String
        Try
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
        Catch ex As Exception
            MsgBox(ex.Message & "  [FCreateTable_City]")
        End Try

    End Sub
    Private Sub FCreateTable_CustomFieldsHead()
        Dim mQry As String
        Try
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
        Catch ex As Exception
            MsgBox(ex.Message & "  [FCreateTable_CustomFieldsHead]")
        End Try

    End Sub

    Private Sub FCreateTable_CustomFieldsDetail()
        Dim mQry As String
        Try
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
        Catch ex As Exception
            MsgBox(ex.Message & "  [FCreateTable_CustomFieldsDetail]")
        End Try

    End Sub

    Private Sub FCreateTable_CustomFields_Trans()
        Dim mQry As String
        Try
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
        Catch ex As Exception
            MsgBox(ex.Message & "  [FCreateTable_CustomFields_Trans]")
        End Try

    End Sub

    Private Sub FCreateTable_Voucher_Type()
        Dim mQry As String
        Try
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

            AgL.AddFieldSqlite("Voucher_Type", "Structure", "nVarchar(10)", "", True, " references Structure(Code) ")
        Catch ex As Exception
            MsgBox(ex.Message & "  [FCreateTable_Vouhcer_Type]")
        End Try

    End Sub

    Private Sub FCreateTable_Voucher_Prefix()
        Dim mQry As String
        Try
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
        Catch ex As Exception
            MsgBox(ex.Message & "  [FCreateTable_Voucher_Prefix]")
        End Try

    End Sub


    Private Sub FCreateTable_LogTable()
        Dim mQry As String
        Try
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
        Catch ex As Exception
            MsgBox(ex.Message & "  [FCreateTable_LogTable]")
        End Try

    End Sub

    Private Sub FCreateTable_PostingGroupSalesTaxItem()
        Dim mQry As String
        Try
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
        Catch ex As Exception
            MsgBox(ex.Message & "  [FCreateTable_PostingGroupSalesTaxItem]")
        End Try

    End Sub

    Private Sub FSeedTable_PurchaseInvoiceSetting()
        Dim mQry As String
        Try
            If AgL.FillData("Select * from PurchaseInvoiceSetting limit 1", AgL.GcnMain).tables(0).Rows.Count = 0 Then
                mQry = " 
                        INSERT INTO PurchaseInvoiceSetting
                        (Code, V_Type, Div_Code, Site_Code, IsPostedInStock, 
                        IsPostedInLedger, IsVisible_ItemCode, IsVisible_ItemCategory, IsVisible_ItemGroup, IsVisible_Specification, 
                        IsVisible_Dimension1, IsVisible_Dimension2, IsVisible_Dimension3, IsVisible_Dimension4, IsVisible_Qty, 
                        IsVisible_FreeQty, IsVisible_RejQty, IsVisible_Manufacturer, IsVisible_LotNo, IsVisible_BaleNo, 
                        IsVisible_Process, IsVisible_UnitMultiplier, IsEditable_DealUnitMultiplier, IsVisible_DealQty, IsEditable_DealQty, 
                        IsVisible_DealUnit, IsEditable_DealUnit, IsVisible_Rate, IsEditable_Rate, IsVisible_MRP, 
                        IsVisible_ProfitMarginPer, IsEditable_ProfitMarginPer, IsVisible_SaleRate, IsVisible_Deal, IsVisible_ExpiryDate, 
                        ItemHelpType, FilterInclude_Process, FilterExclude_AcGroup, FilterInclude_ItemType, FilterInclude_ItemGroup, 
                        FilterExclude_ItemGroup, FilterInclude_Item, FilterExclude_Item, FilterInclude_AcGroup)
                        VALUES('M0001', 'PINV', Null, Null, '1', 
                        '1', '0', '1', '1', '0', 
                        '0', '0', '0', '0', '1', 
                        '0', '0', '0', '0', '0', 
                        '0', '0', '0', '0', '0', 
                        '0', '1', '1', '1', '0', 
                        '0', '1', '0', '0', '0', 
                        Null, Null, Null, Null, Null, 
                        Null, Null, Null, Null);
                "
                AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
            End If
        Catch ex As Exception
            MsgBox(ex.Message & "  [FSeedTable_PurchaseInvoiceSetting]")
        End Try

    End Sub

    Private Sub FCreateView_ViewHelpSubgroup()
        Dim mQry As String
        Try
            AgL.Dman_ExecuteNonQry("Drop View IF Exists ViewHelpSubgroup;", AgL.GcnMain)

            mQry = "
                CREATE VIEW ViewHelpSubgroup AS
                SELECT Subcode as Code, Name || ' [' || ManualCode ||']' as Name, Nature, SubgroupType  From SubGroup                
               "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        Catch ex As Exception
            MsgBox(ex.Message & "  [FCrateView_ViewHelpSubgroup]")
        End Try
    End Sub

    Private Sub FCreateView_ViewStockHeadSetting()
        Dim mQry As String
        Try
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
        Catch ex As Exception
            MsgBox(ex.Message & "  [FCrateView_ViewStockHeadSetting]")
        End Try
    End Sub

    Private Sub FCreateView_ViewPurchaseInvoiceSetting()
        Dim mQry As String
        Try
            AgL.Dman_ExecuteNonQry("Drop View IF Exists viewPurchaseInvoiceSetting;", AgL.GcnMain)

            mQry = "
                CREATE VIEW viewPurchaseInvoiceSetting AS
                Select  VT.V_Type as Voucher_Type, D.Div_Code as Division, S.Code as Site, 
                IfNull(H.IsPostedInStock,0) IsPostedInStock, 
                IfNull(H.IsVisible_ItemCode,0) IsVisible_ItemCode, IfNull(H.IsVisible_Specification,1) IsVisible_Specification, 
                IfNull(H.IsVisible_ItemCategory,1) IsVisible_ItemCategory, IfNull(H.IsVisible_ItemGroup,1) IsVisible_ItemGroup, 
                IfNull(H.IsVisible_Dimension1,0) IsVisible_Dimension1, IfNull(H.IsVisible_Dimension2,0) IsVisible_Dimension2, 
                IfNull(H.IsVisible_Dimension3,0) IsVisible_Dimension3, IfNull(H.IsVisible_Dimension4,0) IsVisible_Dimension4, 
                IfNull(H.IsVisible_Qty,0) IsVisible_Qty, IfNull(H.IsVisible_FreeQty,0) IsVisible_FreeQty, IfNull(H.IsVisible_RejQty,0) IsVisible_RejQty, 
                IfNull(H.IsVisible_MRP,0) IsVisible_MRP, IfNull(H.IsVisible_ProfitMarginPer,1) IsVisible_ProfitMarginPer,IfNull(H.IsEditable_ProfitMarginPer,1) IsEditable_ProfitMarginPer,
                IfNull(H.IsVisible_SaleRate,1) IsVisible_SaleRate, IfNull(H.IsVisible_Deal,0) IsVisible_Deal,IfNull(H.IsVisible_ExpiryDate,0) IsVisible_ExpiryDate,                
                IfNull(H.IsVisible_Manufacturer,0) IsVisible_Manufacturer, IfNull(H.IsVisible_LotNo,0) IsVisible_LotNo, IfNull(H.IsVisible_BaleNo,0) IsVisible_BaleNo, 
                IfNull(H.IsVisible_Process,0) IsVisible_Process, IfNull(H.IsVisible_MeasurePerPcs,0) IsVisible_MeasurePerPcs, 
                IfNull(H.IsEditable_MeasurePerPcs,0) IsEditable_MeasurePerPcs, IfNull(H.IsVisible_Measure,0) IsVisible_Measure, 
                IfNull(H.IsEditable_Measure,0) IsEditable_Measure, IfNull(H.IsVisible_MeasureUnit,0) IsVisible_MeasureUnit, 
                IfNull(H.IsEditable_MeasureUnit,0) IsEditable_MeasureUnit, IfNull(H.IsVisible_Rate,1) IsVisible_Rate, 
                IfNull(H.IsEditable_Rate,1) IsEditable_Rate, H.ItemHelpType ItemHelpType, H.FilterInclude_Process FilterInclude_Process, 
                H.FilterExclude_AcGroup FilterExclude_AcGroup, H.FilterInclude_ItemType FilterInclude_ItemType, 
                H.FilterInclude_ItemGroup FilterInclude_ItemGroup, H.FilterExclude_ItemGroup FilterExclude_ItemGroup, 
                H.FilterInclude_Item FilterInclude_Item, H.FilterExclude_Item FilterExclude_Item, H.FilterInclude_AcGroup FilterInclude_AcGroup
                from Voucher_Type Vt, Division D, SiteMast S  
                Left join PurchaseInvoiceSetting H On  H.V_Type = Vt.V_Type And H.Div_Code=D.Div_Code And H.Site_Code = S.Code
                "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        Catch ex As Exception
            MsgBox(ex.Message & "  [FCrateView_ViewPurchaseInvoiceSetting]")
        End Try
    End Sub

    Private Sub FCreateView_ViewSaleInvoiceSetting()
        Dim mQry As String
        Try
            AgL.Dman_ExecuteNonQry("Drop View IF Exists viewSaleInvoiceSetting;", AgL.GcnMain)

            mQry = "
                CREATE VIEW viewSaleInvoiceSetting AS
                Select  VT.V_Type as Voucher_Type, D.Div_Code as Division, S.Code as Site, 
                IfNull(H.IsPostedInStock,0) IsPostedInStock, 
                IfNull(H.IsVisible_ItemCategory,1) IsVisible_ItemCategory, IfNull(H.IsVisible_ItemGroup,1) IsVisible_ItemGroup, 
                IfNull(H.IsVisible_ItemCode,0) IsVisible_ItemCode, IfNull(H.IsVisible_Specification,1) IsVisible_Specification, 
                IfNull(H.IsVisible_Dimension1,0) IsVisible_Dimension1, IfNull(H.IsVisible_Dimension2,0) IsVisible_Dimension2, 
                IfNull(H.IsVisible_Dimension3,0) IsVisible_Dimension3, IfNull(H.IsVisible_Dimension4,0) IsVisible_Dimension4, 
                IfNull(H.IsVisible_Manufacturer,0) IsVisible_Manufacturer, IfNull(H.IsVisible_LotNo,0) IsVisible_LotNo, IfNull(H.IsVisible_BaleNo,0) IsVisible_BaleNo, 
                IfNull(H.IsVisible_Process,0) IsVisible_Process, IfNull(H.IsVisible_MeasurePerPcs,0) IsVisible_MeasurePerPcs,
                IFNull(H.IsVisible_Qty,0) IsVisible_Qty, IFNull(H.IsVisible_FreeQty,0) IsVisible_FreeQty, 
                IfNull(H.IsEditable_MeasurePerPcs,0) IsEditable_MeasurePerPcs, IfNull(H.IsVisible_Measure,0) IsVisible_Measure, 
                IfNull(H.IsEditable_Measure,0) IsEditable_Measure, IfNull(H.IsVisible_MeasureUnit,0) IsVisible_MeasureUnit, 
                IfNull(H.IsEditable_MeasureUnit,0) IsEditable_MeasureUnit, IfNull(H.IsVisible_Rate,1) IsVisible_Rate, 
                IfNull(H.IsEditable_Rate,1) IsEditable_Rate, H.ItemHelpType ItemHelpType, H.FilterInclude_Process FilterInclude_Process, 
                H.FilterExclude_AcGroup FilterExclude_AcGroup, H.FilterInclude_ItemType FilterInclude_ItemType, 
                H.FilterInclude_ItemGroup FilterInclude_ItemGroup, H.FilterExclude_ItemGroup FilterExclude_ItemGroup, 
                H.FilterInclude_Item FilterInclude_Item, H.FilterExclude_Item FilterExclude_Item, H.FilterInclude_AcGroup FilterInclude_AcGroup,
                IFNull(H.ItemHelpType,'SALE') as ItemHelpType, H.Default_RateType
                from Voucher_Type Vt, Division D, SiteMast S  
                Left join SaleInvoiceSetting H On  H.V_Type = Vt.V_Type And H.Div_Code=D.Div_Code And H.Site_Code = S.Code
                "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        Catch ex As Exception
            MsgBox(ex.Message & "  [FCrateView_ViewSaleInvoiceSetting]")
        End Try
    End Sub

    Private Sub FCreateView_User_VType_Permission()
        Dim mQry As String
        Try
            AgL.Dman_ExecuteNonQry("Drop View IF Exists User_VType_Permission;", AgL.GcnMain)

            mQry = "
                CREATE VIEW User_VType_Permission AS
                SELECT H.UserName, H.Div_Code, H.Site_Code,L.V_Type 
                FROM [User_Exclude_VType] H
                LEFT JOIN User_Exclude_VTypeDetail L ON H.Code = L.Code;
                "
            AgL.Dman_ExecuteNonQry(mQry, AgL.GcnMain)
        Catch ex As Exception
            MsgBox(ex.Message & "  [FCrateView_User_VType_Permission]")
        End Try

    End Sub

    Private Sub FCreateTable_User_Exclude_VType()
        Dim mQry As String
        Try
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
        Catch ex As Exception
            MsgBox(ex.Message & "  [FCrateTable_User_Exclude_VType]")
        End Try

    End Sub

    Private Sub FCreateTable_User_Exclude_VTypeDetail()
        Dim mQry As String
        Try
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
        Catch ex As Exception
            MsgBox(ex.Message & "  [FCrateTable_User_Exclude_VTypeDetail]")
        End Try

    End Sub

    Private Sub FCreateTable_State()
        Dim mQry As String
        Try
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

        Catch ex As Exception
            MsgBox(ex.Message & "  [FCrateTable_State]")
        End Try

    End Sub

    Private Sub FSeedTable_State()
        Dim mQry As String
        Try
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
        Catch ex As Exception
            MsgBox(ex.Message & "  [FSeedTable_State]")
        End Try

    End Sub

    Private Sub FCreateTable_BomDetail()
        Dim mQry As String
        Try
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
        Catch ex As Exception
            MsgBox(ex.Message & "  [FCreateTable_BOM]")
        End Try

    End Sub

    Private Sub FCreateTable_UnitConversion()
        Dim mQry As String
        Try
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
        Catch ex As Exception
            MsgBox(ex.Message & "  [FCreateTable_UnitConversion]")
        End Try

    End Sub

    Private Sub FCreateTable_CustomFields()
        Dim mQry As String
        Try
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
        Catch ex As Exception
            MsgBox(ex.Message & "  [FCreateTable_CustomFields]")
        End Try

    End Sub
#End Region



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