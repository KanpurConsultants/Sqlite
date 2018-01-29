Public Class ClsReportProceduresOld

#Region "Danger Zone"
    Dim StrArr1() As String = Nothing, StrArr2() As String = Nothing, StrArr3() As String = Nothing, StrArr4() As String = Nothing, StrArr5() As String = Nothing

    Dim mGRepFormName As String = ""
    Dim WithEvents ObjRFG As AgLibrary.RepFormGlobal

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
    Private Const PartyLedgerReport As String = "PartyLedgerReport"
    Private Const SaleReport As String = "SaleReport"
    Private Const RepurchaseBonusReport As String = "RepurchaseBonusReport"
    Private Const DistribuerBusinessReport As String = "DistributerBusinessReport"
    Private Const ItemWiseSaleReport As String = "ItemWiseSaleReport"
    Private Const PurchaseReport As String = "PurchaseReport"
    Private Const ItemWisePurchaseReport As String = "ItemWisePurchaseReport"
#End Region

#Region "Queries Definition"
    Dim mHelpCityQry$ = "Select Convert(BIT,0) As [Select],CityCode, CityName From City "
    Dim mHelpGodownQry$ = "Select Convert(BIT,0) As [Select], Code, Description From Godown "
    Dim mHelpStateQry$ = "Select Convert(BIT,0) As [Select],State_Code, State_Desc From State "
    Dim mHelpUserQry$ = "Select Convert(BIT,0) As [Select],User_Name As Code, User_Name As [User] From UserMast "
    Dim mHelpSiteQry$ = "Select Convert(BIT,0) As [Select], Code, Name As [Site] From SiteMast Where " & AgL.PubSiteCondition("Code", AgL.PubSiteCode) & " "
    Dim mHelpItemQry$ = "Select Convert(BIT,0) As [Select],Code, Description As [Item] From Item "
    Dim mHelpVendorQry$ = " Select Convert(BIT,0) As [Select], H.SubCode As Code, Sg.DispName AS Vendor FROM Vendor H LEFT JOIN SubGroup Sg ON H.SubCode = Sg.SubCode "
    Dim mHelpDivisionQry$ = "Select Convert(BIT,0) As [Select], Div_Code AS Code,Div_Name AS Division FROM Division WHERE 1=1 " & AgL.RetDivisionCondition(AgL, "Div_Code") & " "
    Dim mHelpPartyQry$ = " Select Convert(BIT,0) As [Select], Sg.SubCode As Code, Sg.Name AS Party FROM SubGroup Sg Where Sg.Nature In ('Customer','Supplier') "
    Dim mHelpDistributerQry$ = " Select Convert(BIT,0) As [Select], Sg.SubCode As Code, Sg.Name AS Distributer FROM SubGroup Sg Where Sg.SubgroupType='Distributer' "
#End Region

    Dim DsRep As DataSet = Nothing, DsRep1 As DataSet = Nothing, DsRep2 As DataSet = Nothing
    Dim mQry$ = "", RepName$ = "", RepTitle$ = ""

#Region "Initializing Grid"
    Public Sub Ini_Grid()
        Try
            Dim I As Integer = 0
            Select Case GRepFormName
                Case PartyLedgerReport
                    StrArr1 = New String() {"Summary", "Detail"}
                    ObjRFG.Ini_Grp("From Date", AgL.PubStartDate, "To Date", AgL.PubLoginDate, "Report Type", StrArr1)
                    ObjRFG.CreateHelpGrid(mHelpPartyQry, "Party")
                    ObjRFG.CreateHelpGrid(mHelpSiteQry, "Site")
                    ObjRFG.CreateHelpGrid(mHelpDivisionQry, "Division")

                Case SaleReport
                    ObjRFG.Ini_Grp("From Date", AgL.PubStartDate, "To Date", AgL.PubLoginDate)
                    ObjRFG.CreateHelpGrid(mHelpPartyQry, "Party")
                    ObjRFG.CreateHelpGrid(mHelpSiteQry, "Site")

                Case RepurchaseBonusReport
                    ObjRFG.Ini_Grp("From Date", AgL.PubStartDate, "To Date", AgL.PubLoginDate)
                    ObjRFG.CreateHelpGrid(mHelpPartyQry, "Party")
                    ObjRFG.CreateHelpGrid(mHelpSiteQry, "Site")


                Case DistribuerBusinessReport
                    'ObjRFG.Ini_Grp("From Date", AgL.PubStartDate, "To Date", AgL.PubLoginDate)
                    ObjRFG.CreateHelpGrid(mHelpDistributerQry, "Distributer")
                    ObjRFG.CreateHelpGrid(mHelpSiteQry, "Site")



                Case ItemWiseSaleReport
                    StrArr1 = New String() {"Summary", "Detail", "Godown Wise Summary", "Stock Point Wise Summary"}
                    ObjRFG.Ini_Grp("From Date", AgL.PubStartDate, "To Date", AgL.PubLoginDate, "Report Type", StrArr1)
                    ObjRFG.CreateHelpGrid(mHelpItemQry, "Item")
                    ObjRFG.CreateHelpGrid(mHelpPartyQry, "Party")
                    ObjRFG.CreateHelpGrid(mHelpSiteQry, "Site")
                    ObjRFG.CreateHelpGrid(mHelpGodownQry, "Godown")

                Case PurchaseReport
                    ObjRFG.Ini_Grp("From Date", AgL.PubStartDate, "To Date", AgL.PubLoginDate)
                    ObjRFG.CreateHelpGrid(mHelpPartyQry, "Party")
                    ObjRFG.CreateHelpGrid(mHelpSiteQry, "Site")

                Case ItemWisePurchaseReport
                    ObjRFG.Ini_Grp("From Date", AgL.PubStartDate, "To Date", AgL.PubLoginDate)
                    ObjRFG.CreateHelpGrid(mHelpItemQry, "Item")
                    ObjRFG.CreateHelpGrid(mHelpPartyQry, "Party")
                    ObjRFG.CreateHelpGrid(mHelpSiteQry, "Site")
            End Select
            Call ObjRFG.Arrange_Grid()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
#End Region


    Private Sub ObjRepFormGlobal_ProcessReport() Handles ObjRFG.ProcessReport
        Select Case mGRepFormName

            Case SaleReport
                ProcSaleReport()

            Case RepurchaseBonusReport
                ProcRepurchaseBonusReport()

            Case DistribuerBusinessReport
                ProcDistributerQuery()

            Case ItemWiseSaleReport
                ProcItemWiseSaleReport()

            Case PurchaseReport
                ProcPurchaseReport()

            Case ItemWisePurchaseReport
                ProcItemWisePurchaseReport()
        End Select
    End Sub

    Public Sub New(ByVal mObjRepFormGlobal As AgLibrary.RepFormGlobal)
        ObjRFG = mObjRepFormGlobal
    End Sub

#Region "Sale Report"
    Private Sub ProcSaleReport()
        Try
            Call ObjRFG.FillGridString()

            RepName = "Trade_SaleReport" : RepTitle = "Sale Report"


            Dim mCondStr$ = ""
            mCondStr = " Where 1=1"

            mCondStr = mCondStr & " AND H.V_Date Between " & AgL.ConvertDate(CDate(ObjRFG.ParameterDate1_Value).ToString("u")) & " And " & AgL.ConvertDate(CDate(ObjRFG.ParameterDate2_Value).ToString("u")) & " "
            mCondStr = mCondStr & ObjRFG.GetWhereCondition("H.SaleToParty", 0)
            mCondStr = mCondStr & ObjRFG.GetWhereCondition("H.Site_Code", 1)

            mQry = " SELECT H.DocID, H.V_Type, H.V_Prefix, H.V_Date, H.V_No, H.Div_Code, H.Site_Code, H.ReferenceNo, H.SaleToParty, " &
                        " BP.DispName as BillToPartyName, Sg.DispName As SaleToPartyName, Sg.ManualCode as SaleToPartyManualCode, Sg.Add1, Sg.Add2, Sg.Add3, C.CityName As SaleToPartyCityName , H.SaleToPartyMobile, H.ShipToParty, H.ShipToPartyName,  " &
                        " H.ShipToPartyAddress, H.ShipToPartyCity, H.ShipToPartyMobile, H.SaleOrder, H.SaleChallan, H.Currency,  " &
                        " H.SalesTaxGroupParty, H.Structure, H.BillingType, H.Form, H.FormNo, H.ReferenceDocId, H.Remarks, H.TotalQty,  " &
                        " H.TotalMeasure, H.TotalAmount, H.EntryBy, H.EntryDate, H.EntryType, H.EntryStatus, H.ApproveBy, H.ApproveDate,  " &
                        " H.MoveToLog, H.MoveToLogDate, H.IsDeleted, H.Status, H.UID, H.TableCode, H.PaymentMode, H.PostingAc, H.Godown, H.Vendor,  " &
                        " H.SaleToPartyTinNo, H.SaleToPartyCstNo, H.Transporter, H.Vehicle, H.VehicleDescription, H.Driver, H.DriverName,  " &
                        " H.DriverContactNo, H.LrNo, H.LrDate, H.PrivateMark, H.PortOfLoading, H.DestinationPort, H.FinalPlaceOfDelivery,  " &
                        " H.PreCarriageBy, H.PlaceOfPreCarriage, H.ShipmentThrough, H.CreditDays,  " &
                        " H.Gross_Amount,  " &
                        " H.Sales_Tax_Taxable_Amt, H.Vat_Per, H.Vat, H.Discount_Per, H.Discount, H.Other_Charges_Per,  " &
                        " H.Other_Charges, H.Round_Off, H.Net_Amount " &
                        " FROM SaleInvoice H  " &
                        " LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type " &
                        " LEFT JOIN SubGroup Sg On H.SaleToParty = Sg.SubCode " &
                        " LEFT JOIN SubGroup BP On H.BillToParty = BP.SubCode " &
                        " LEFT JOIN City C On Sg.CityCode = C.CityCode " & mCondStr

            DsRep = AgL.FillData(mQry, AgL.GCn)

            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records to Print!")

            ObjRFG.PrintReport(DsRep, RepName, RepTitle, AgL.PubReportPath)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub
#End Region


#Region "Distributer Query"
    Private Sub ProcDistributerQuery()
        Try
            Call ObjRFG.FillGridString()

            RepName = "Trade_DistributerQuery" : RepTitle = "Distributer Business Detail"


            Dim mCondStr$ = ""
            mCondStr = " Where 1=1"

            mCondStr = mCondStr & " AND H.V_Date Between " & AgL.Chk_Text(CDate(ObjRFG.ParameterDate1_Value).ToString("u")) & " And " & AgL.Chk_Text(CDate(ObjRFG.ParameterDate2_Value).ToString("u")) & " "
            mCondStr = mCondStr & ObjRFG.GetWhereCondition("H.SaleToParty", 0)
            mCondStr = mCondStr & ObjRFG.GetWhereCondition("H.Site_Code", 1)
            Debug.Print(ObjRFG.GetCodeString(0))



            If InStr(ObjRFG.GetCodeString(0), ",") > 0 Then
                MsgBox("Only one ditributer selection is allowed.")
                Exit Sub
            End If


            mQry = "EXEC ProcRepDistributerQuery " & Replace(ObjRFG.GetCodeString(0), "''", "'")

            DsRep = AgL.FillData(mQry, AgL.GCn)

            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records to Print!")

            ObjRFG.PrintReport(DsRep, RepName, RepTitle, AgL.PubReportPath)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub
#End Region



#Region "Repurchase Bonus Report"
    Private Sub ProcRepurchaseBonusReport()
        Try
            Call ObjRFG.FillGridString()

            RepName = "Trade_RepurchaseBonusReport" : RepTitle = "Repurchase Bonus Report"


            Dim mCondStr$ = ""
            mCondStr = " Where 1=1"

            mCondStr = mCondStr & " AND H.V_Date Between " & AgL.Chk_Text(CDate(ObjRFG.ParameterDate1_Value).ToString("u")) & " And " & AgL.ConvertDate(CDate(ObjRFG.ParameterDate2_Value).ToString("u")) & " "
            mCondStr = mCondStr & ObjRFG.GetWhereCondition("H.SaleToParty", 0)
            mCondStr = mCondStr & ObjRFG.GetWhereCondition("H.Site_Code", 1)

            mQry = "SELECT Max(Sg.Name) as Name, Max(Sg.ManualCode) as DistributorCode,   H.SaleToParty, Max(X.LastMonthPurchase) AS LastMonthPurchase, sum(L.Net_Amount) AS CurrentMonthPurchase, sum(L.BusinessVolume) AS BusinessVolume, " &
                    "CASE WHEN Sum(L.Net_Amount) >= 5000 THEN 8 WHEN Sum(L.Net_Amount) >= 2000 THEN 6 Else 4 END AS RepurchaseBonusPer, " &
                    "sum(L.BusinessVolume)*(CASE WHEN Sum(L.Net_Amount) >= 5000 THEN 8 WHEN Sum(L.Net_Amount) >= 2000 THEN 6 Else 4 END)/100 AS BonusAmount " &
                    "FROM " &
                    "(SELECT  H.SaleToParty, sum(h.Net_Amount) AS LastMonthPurchase  " &
                    "FROM SaleInvoice H " &
                    "WHERE H.V_Date BETWEEN DateAdd(Month,-1," & AgL.ConvertDate(CDate(ObjRFG.ParameterDate1_Value).ToString("u")) & ") And DateAdd(Month,-1," & AgL.ConvertDate(CDate(ObjRFG.ParameterDate2_Value).ToString("u")) & ")  " &
                    "GROUP BY H.SaleToParty  " &
                    "HAVING sum(h.Net_Amount) >=500) AS X " &
                    "LEFT JOIN SaleInvoice H ON X.SaleToParty = H.saletoParty " &
                    "LEFT JOIN SaleInvoiceDetail L ON H.DocID = l.DocId  " &
                    "Left Join Subgroup sg on H.SaleToParty = Sg.SubCode " &
                    "WHERE H.V_Date BETWEEN " & AgL.ConvertDate(CDate(ObjRFG.ParameterDate1_Value).ToString("u")) & " And " & AgL.ConvertDate(CDate(ObjRFG.ParameterDate2_Value).ToString("u")) & "  " &
                    "GROUP BY H.SaleToParty  " &
                    "HAVING sum(h.Net_Amount) >=500"


            DsRep = AgL.FillData(mQry, AgL.GCn)

            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records to Print!")

            ObjRFG.PrintReport(DsRep, RepName, RepTitle, AgL.PubReportPath)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub
#End Region


#Region "Item Wise Sale Invoice Report"
    Private Sub ProcItemWiseSaleReport()
        Try
            Call ObjRFG.FillGridString()

            If ObjRFG.Cmbo1.Text = "Summary" Then
                RepName = "Trade_ItemWiseSaleReportSummary" : RepTitle = "Item Wise Sale Summary Report"
            ElseIf ObjRFG.Cmbo1.Text = "Godown Wise Summary" Then
                RepName = "Trade_ItemWiseSaleReportSummaryGodown" : RepTitle = "Godown Wise Sale Summary Report"
            ElseIf ObjRFG.Cmbo1.Text = "Stock Point Wise Summary" Then
                RepName = "Trade_StockPointWiseSaleReportSummary" : RepTitle = "Stock Point Wise Sale Summary Report"
            Else
                RepName = "Trade_ItemWiseSaleReport" : RepTitle = "Item Wise Sale Report"
            End If


            Dim mCondStr$ = ""
            mCondStr = " Where 1=1"

            mCondStr = mCondStr & " AND H.V_Date Between " & AgL.ConvertDate(CDate(ObjRFG.ParameterDate1_Value).ToString("u")) & " And " & AgL.ConvertDate(CDate(ObjRFG.ParameterDate2_Value).ToString("u")) & " "
            mCondStr = mCondStr & ObjRFG.GetWhereCondition("L.Item", 0)
            mCondStr = mCondStr & ObjRFG.GetWhereCondition("H.SaleToParty", 1)
            mCondStr = mCondStr & ObjRFG.GetWhereCondition("H.Site_Code", 2)
            mCondStr = mCondStr & ObjRFG.GetWhereCondition("H.Godown", 3)


            mQry = " SELECT L.DocId, L.Sr, L.SaleOrder, L.SaleOrderSr, L.SaleChallan, L.SaleChallanSr, L.BaleNo, L.Item, I.ManualCode AS ItemManualCode, L.SalesTaxGroupItem, L.DocQty, " &
                        " (Case When L.Amount>0 Then L.Qty else 0 End) as Qty, (Case When L.Amount=0 Then L.Qty else 0 End) as FreeQty, L.Unit, L.MeasurePerPcs, L.MeasureUnit, L.TotalDocMeasure, L.TotalMeasure, L.Rate, L.Amount, L.ReferenceDocId,  " &
                        " L.LotNo, L.UID, L.Specification, L.Gross_Amount, L.Sales_Tax_Taxable_Amt, L.Vat_Per, L.Vat, L.Sat_Per, L.Sat, L.Cst_Per, L.Cst, L.Total_Price,  " &
                        " L.Discount_Per, L.Discount, L.Other_Charges_Per, L.Other_Charges, L.Round_Off, L.Net_Amount, " &
                        " I.Description AS ItemDesc, H.V_Date, H.ReferenceNo, Sg.DispName As SaleToPartyName, L.Remark, G.Description as GodownName, L.PointValue, L.BusinessVolume, SP.Name as StockPointName " &
                        " FROM SaleInvoiceDetail L " &
                        " LEFT JOIN SaleInvoice H ON L.DocId = H.DocId " &
                        " LEFT JOIN Item I ON L.Item = I.Code " &
                        " LEFT JOIN Godown G ON H.Godown = G.Code " &
                        " LEFT JOIN SubGroup Sg On H.SaleToParty = Sg.SubCode  
                          Left Join SubGroup SP on H.StockPoint = SP.SubCode  
                        " & mCondStr

            DsRep = AgL.FillData(mQry, AgL.GCn)

            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records to Print!")

            ObjRFG.PrintReport(DsRep, RepName, RepTitle, AgL.PubReportPath)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub
#End Region

#Region "Purchase Report"
    Private Sub ProcPurchaseReport()
        Try
            Call ObjRFG.FillGridString()

            RepName = "Trade_PurchaseReport" : RepTitle = "Material Receive Report"

            Dim mCondStr$ = ""
            mCondStr = " Where 1=1"

            mCondStr = mCondStr & " AND H.V_Date Between " & AgL.ConvertDate(CDate(ObjRFG.ParameterDate1_Value).ToString("u")) & " And " & AgL.ConvertDate(CDate(ObjRFG.ParameterDate2_Value).ToString("u")) & " "
            mCondStr = mCondStr & ObjRFG.GetWhereCondition("H.Vendor", 0)
            mCondStr = mCondStr & ObjRFG.GetWhereCondition("H.Site_Code", 1)

            mQry = " SELECT H.DocID, H.V_Type, H.V_Prefix, H.V_Date, H.V_No, H.Div_Code, H.Site_Code, H.ReferenceNo, H.Vendor, " &
                        " Sg.DispName As VendorName, Sg.Add1, Sg.Add2, Sg.Add3, C.CityName As VendorCityName , H.PurchOrder, " &
                        " H.PurchChallan, H.Currency,  " &
                        " H.SalesTaxGroupParty, H.Structure, H.BillingType, H.Form, H.FormNo, H.ReferenceDocId, H.Remarks, H.TotalQty,  " &
                        " H.TotalMeasure, H.TotalAmount, H.EntryBy, H.EntryDate, H.EntryType, H.EntryStatus, H.ApproveBy, H.ApproveDate,  " &
                        " H.MoveToLog, H.MoveToLogDate, H.IsDeleted, H.Status, H.UID, H.Godown, H.Vendor,  " &
                        " H.Gross_Amount, " &
                        " H.Discount_Per, H.Discount, H.Other_Charges_Per,  " &
                        " H.Other_Charges, H.Round_Off, H.Net_Amount " &
                        " FROM PurchInvoice H  " &
                        " LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type " &
                        " LEFT JOIN SubGroup Sg On H.Vendor = Sg.SubCode " &
                        " LEFT JOIN City C On Sg.CityCode = C.CityCode " & mCondStr

            DsRep = AgL.FillData(mQry, AgL.GCn)

            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records to Print!")

            ObjRFG.PrintReport(DsRep, RepName, RepTitle, AgL.PubReportPath)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub
#End Region

#Region "Item Wise Purchase Report"
    Private Sub ProcItemWisePurchaseReport()
        Try
            Call ObjRFG.FillGridString()

            RepName = "Trade_ItemWisePurchaseReport" : RepTitle = "Item Wise Receive Report"

            Dim mCondStr$ = ""
            mCondStr = " Where 1=1"

            mCondStr = mCondStr & " AND H.V_Date Between " & AgL.ConvertDate(CDate(ObjRFG.ParameterDate1_Value).ToString("u")) & " And " & AgL.ConvertDate(CDate(ObjRFG.ParameterDate2_Value).ToString("u")) & " "
            mCondStr = mCondStr & ObjRFG.GetWhereCondition("L.Item", 0)
            mCondStr = mCondStr & ObjRFG.GetWhereCondition("H.Vendor", 1)
            mCondStr = mCondStr & ObjRFG.GetWhereCondition("H.Site_Code", 2)


            mQry = " SELECT L.DocId, L.Sr, L.PurchOrder, L.PurchChallan, L.PurchChallanSr, L.BaleNo, L.Item, I.ManualCode AS ItemManualCode, " &
                        " L.SalesTaxGroupItem, L.DocQty, " &
                        " L.Qty, L.Unit, L.MeasurePerPcs, L.MeasureUnit, L.TotalDocMeasure, L.TotalMeasure, L.Rate, L.Amount, L.ReferenceDocId,  " &
                        " L.LotNo, L.UID, L.Specification, L.Gross_Amount, " &
                        " L.Discount_Per, L.Discount, L.Other_Charges_Per, L.Other_Charges, L.Round_Off, L.Net_Amount, " &
                        " I.Description AS ItemDesc, H.V_Date, H.ReferenceNo, Sg.DispName As VendorName, L.Remark " &
                        " FROM PurchInvoiceDetail L " &
                        " LEFT JOIN PurchInvoice H ON L.DocId = H.DocId " &
                        " LEFT JOIN Item I ON L.Item = I.Code " &
                        " LEFT JOIN SubGroup Sg On H.Vendor = Sg.SubCode " & mCondStr

            DsRep = AgL.FillData(mQry, AgL.GCn)

            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records to Print!")

            ObjRFG.PrintReport(DsRep, RepName, RepTitle, AgL.PubReportPath)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub
#End Region
End Class
