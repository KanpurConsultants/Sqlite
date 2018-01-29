Public Class ClsReportProcedures

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
    Private Const ItemWiseSaleReport As String = "ItemWiseSaleReport"
    Private Const PurchaseReport As String = "PurchaseReport"
    Private Const ItemWisePurchaseReport As String = "ItemWisePurchaseReport"
#End Region

#Region "Queries Definition"
    Dim mHelpCityQry$ = "Select Convert(BIT,0) As [Select],CityCode, CityName From City "
    Dim mHelpStateQry$ = "Select Convert(BIT,0) As [Select],State_Code, State_Desc From State "
    Dim mHelpUserQry$ = "Select Convert(BIT,0) As [Select],User_Name As Code, User_Name As [User] From UserMast "
    Dim mHelpSiteQry$ = "Select Convert(BIT,0) As [Select], Code, Name As [Site] From SiteMast Where " & AgL.PubSiteCondition("Code", AgL.PubSiteCode) & " "
    Dim mHelpItemQry$ = "Select Convert(BIT,0) As [Select],Code, Description As [Item] From Item "
    Dim mHelpVendorQry$ = " Select Convert(BIT,0) As [Select], H.SubCode As Code, Sg.DispName AS Vendor FROM Vendor H LEFT JOIN SubGroup Sg ON H.SubCode = Sg.SubCode "
    Dim mHelpDivisionQry$ = "Select Convert(BIT,0) As [Select], Div_Code AS Code,Div_Name AS Division FROM Division WHERE 1=1 " & AgL.RetDivisionCondition(AgL, "Div_Code") & " "
    Dim mHelpPartyQry$ = " Select Convert(BIT,0) As [Select], Sg.SubCode As Code, Sg.Name AS Party FROM SubGroup Sg Where Sg.Nature In ('Customer','Supplier') "
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

                Case ItemWiseSaleReport
                    StrArr1 = New String() {"Summary", "Detail"}
                    ObjRFG.Ini_Grp("From Date", AgL.PubStartDate, "To Date", AgL.PubLoginDate, "Report Type", StrArr1)
                    ObjRFG.CreateHelpGrid(mHelpItemQry, "Item")
                    ObjRFG.CreateHelpGrid(mHelpPartyQry, "Party")
                    ObjRFG.CreateHelpGrid(mHelpSiteQry, "Site")

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
            Case PartyLedgerReport
                ProcPartyLedger()

            Case SaleReport
                ProcSaleReport()

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

#Region "Party Ledger"
    Private Sub ProcPartyLedger()
        Try
            Call ObjRFG.FillGridString()

            Dim bQry$ = ""
            Dim mCondStr$ = ""

            If ObjRFG.IsRequiredField(AgLibrary.ClsMain.ReportFormGlobalControls.Date1_Control) Then Exit Sub
            If ObjRFG.IsRequiredField(AgLibrary.ClsMain.ReportFormGlobalControls.Date2_Control) Then Exit Sub

            If AgL.StrCmp(ObjRFG.ParameterCmbo1_Value, "Summary") Then
                RepName = "Trade_PartyLedgerSummary" : RepTitle = "Party Ledger(Summary)"
            ElseIf AgL.StrCmp(ObjRFG.ParameterCmbo1_Value, "Detail") Then
                RepName = "Trade_PartyLedgerDetail" : RepTitle = "Party Ledger(Detail)"
            End If

            mCondStr = " Where 1=1 And Sg.Nature In ('Customer','Supplier') "

            mCondStr = mCondStr & " AND V1.V_Date Between " & AgL.ConvertDate(ObjRFG.ParameterDate1_Value) & " And " & AgL.ConvertDate(ObjRFG.ParameterDate2_Value) & " "
            mCondStr = mCondStr & " And V1.Site_Code = '" & AgL.PubSiteCode & "' "
            mCondStr = mCondStr & " And V1.DivCode= '" & AgL.PubDivCode & "' "

            mCondStr = mCondStr & ObjRFG.GetWhereCondition("V1.SubCode", 0)

            mQry = " SELECT V1.* , SG.Name AS PartyName " & _
                    " FROM ( " & ClsMain.PayableLedgerQry(ObjRFG.ParameterDate1_Value, ObjRFG.ParameterDate2_Value) & " ) V1 " & _
                    " LEFT JOIN SubGroup SG ON SG.SubCode = V1.SubCode " & _
                    " " & mCondStr & ""

            DsRep = AgL.FillData(mQry, AgL.GCn)

            If DsRep.Tables(0).Rows.Count = 0 Then Err.Raise(1, , "No Records to Print!")

            ObjRFG.PrintReport(DsRep, RepName, RepTitle, AgL.PubReportPath)
        Catch ex As Exception
            MsgBox(ex.Message)
            DsRep = Nothing
        End Try
    End Sub
#End Region

#Region "Sale Report"
    Private Sub ProcSaleReport()
        Try
            Call ObjRFG.FillGridString()

            RepName = "Trade_SaleReport" : RepTitle = "Sale Report"


            Dim mCondStr$ = ""
            mCondStr = " Where 1=1"

            mCondStr = mCondStr & " AND H.V_Date Between " & AgL.ConvertDate(ObjRFG.ParameterDate1_Value) & " And " & AgL.ConvertDate(ObjRFG.ParameterDate2_Value) & " "
            mCondStr = mCondStr & ObjRFG.GetWhereCondition("H.SaleToParty", 0)
            mCondStr = mCondStr & ObjRFG.GetWhereCondition("H.Site_Code", 1)

            mQry = " SELECT H.DocID, H.V_Type, H.V_Prefix, H.V_Date, H.V_No, H.Div_Code, H.Site_Code, H.ReferenceNo, H.SaleToParty, " & _
                        " BP.DispName as BillToPartyName, Sg.DispName As SaleToPartyName, Sg.ManualCode as SaleToPartyManualCode, Sg.Add1, Sg.Add2, Sg.Add3, C.CityName As SaleToPartyCityName , H.SaleToPartyMobile, H.ShipToParty, H.ShipToPartyName,  " & _
                        " H.ShipToPartyAddress, H.ShipToPartyCity, H.ShipToPartyMobile, H.SaleOrder, H.SaleChallan, H.Currency,  " & _
                        " H.SalesTaxGroupParty, H.Structure, H.BillingType, H.Form, H.FormNo, H.ReferenceDocId, H.Remarks, H.TotalQty,  " & _
                        " H.TotalMeasure, H.TotalAmount, H.EntryBy, H.EntryDate, H.EntryType, H.EntryStatus, H.ApproveBy, H.ApproveDate,  " & _
                        " H.MoveToLog, H.MoveToLogDate, H.IsDeleted, H.Status, H.UID, H.TableCode, H.PaymentMode, H.PostingAc, H.Godown, H.Vendor,  " & _
                        " H.SaleToPartyTinNo, H.SaleToPartyCstNo, H.Transporter, H.Vehicle, H.VehicleDescription, H.Driver, H.DriverName,  " & _
                        " H.DriverContactNo, H.LrNo, H.LrDate, H.PrivateMark, H.PortOfLoading, H.DestinationPort, H.FinalPlaceOfDelivery,  " & _
                        " H.PreCarriageBy, H.PlaceOfPreCarriage, H.ShipmentThrough, H.CreditDays,  " & _
                        " H.Gross_Amount,  " & _
                        " H.Sales_Tax_Taxable_Amt, H.Vat_Per, H.Vat, H.Discount_Per, H.Discount, H.Other_Charges_Per,  " & _
                        " H.Other_Charges, H.Round_Off, H.Net_Amount " & _
                        " FROM SaleInvoice H  " & _
                        " LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type " & _
                        " LEFT JOIN SubGroup Sg On H.SaleToParty = Sg.SubCode " & _
                        " LEFT JOIN SubGroup BP On H.BillToParty = BP.SubCode " & _
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

#Region "Item Wise Sale Invoice Report"
    Private Sub ProcItemWiseSaleReport()
        Try
            Call ObjRFG.FillGridString()

            If ObjRFG.Cmbo1.Text = "Summary" Then
                RepName = "Trade_ItemWiseSaleReportSummary" : RepTitle = "Item Wise Sale Summary Report"
            Else
                RepName = "Trade_ItemWiseSaleReport" : RepTitle = "Item Wise Sale Report"
            End If


            Dim mCondStr$ = ""
            mCondStr = " Where 1=1"

            mCondStr = mCondStr & " AND H.V_Date Between " & AgL.ConvertDate(ObjRFG.ParameterDate1_Value) & " And " & AgL.ConvertDate(ObjRFG.ParameterDate2_Value) & " "
            mCondStr = mCondStr & ObjRFG.GetWhereCondition("L.Item", 0)
            mCondStr = mCondStr & ObjRFG.GetWhereCondition("H.SaleToParty", 1)
            mCondStr = mCondStr & ObjRFG.GetWhereCondition("H.Site_Code", 2)


            mQry = " SELECT L.DocId, L.Sr, L.SaleOrder, L.SaleOrderSr, L.SaleChallan, L.SaleChallanSr, L.BaleNo, L.Item, I.ManualCode AS ItemManualCode, L.SalesTaxGroupItem, L.DocQty, " & _
                        " L.Qty, L.Unit, L.MeasurePerPcs, L.MeasureUnit, L.TotalDocMeasure, L.TotalMeasure, L.Rate, L.Amount, L.ReferenceDocId,  " & _
                        " L.LotNo, L.UID, L.Specification, L.Gross_Amount, L.Sales_Tax_Taxable_Amt, L.Vat_Per, L.Vat, L.Cst_Per, L.Cst, L.Total_Price,  " & _
                        " L.Discount_Per, L.Discount, L.Other_Charges_Per, L.Other_Charges, L.Round_Off, L.Net_Amount, " & _
                        " I.Description AS ItemDesc, H.V_Date, H.ReferenceNo, Sg.DispName As SaleToPartyName, L.Remark " & _
                        " FROM SaleInvoiceDetail L " & _
                        " LEFT JOIN SaleInvoice H ON L.DocId = H.DocId " & _
                        " LEFT JOIN Item I ON L.Item = I.Code " & _
                        " LEFT JOIN SubGroup Sg On H.SaleToParty = Sg.SubCode " & mCondStr

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

            mCondStr = mCondStr & " AND H.V_Date Between " & AgL.ConvertDate(ObjRFG.ParameterDate1_Value) & " And " & AgL.ConvertDate(ObjRFG.ParameterDate2_Value) & " "
            mCondStr = mCondStr & ObjRFG.GetWhereCondition("H.Vendor", 0)
            mCondStr = mCondStr & ObjRFG.GetWhereCondition("H.Site_Code", 1)

            mQry = " SELECT H.DocID, H.V_Type, H.V_Prefix, H.V_Date, H.V_No, H.Div_Code, H.Site_Code, H.ReferenceNo, H.Vendor, " & _
                        " Sg.DispName As VendorName, Sg.Add1, Sg.Add2, Sg.Add3, C.CityName As VendorCityName , H.PurchOrder, " & _
                        " H.PurchChallan, H.Currency,  " & _
                        " H.SalesTaxGroupParty, H.Structure, H.BillingType, H.Form, H.FormNo, H.ReferenceDocId, H.Remarks, H.TotalQty,  " & _
                        " H.TotalMeasure, H.TotalAmount, H.EntryBy, H.EntryDate, H.EntryType, H.EntryStatus, H.ApproveBy, H.ApproveDate,  " & _
                        " H.MoveToLog, H.MoveToLogDate, H.IsDeleted, H.Status, H.UID, H.Godown, H.Vendor,  " & _
                        " H.Gross_Amount, " & _
                        " H.Discount_Per, H.Discount, H.Other_Charges_Per,  " & _
                        " H.Other_Charges, H.Round_Off, H.Net_Amount " & _
                        " FROM PurchInvoice H  " & _
                        " LEFT JOIN Voucher_Type Vt On H.V_Type = Vt.V_Type " & _
                        " LEFT JOIN SubGroup Sg On H.Vendor = Sg.SubCode " & _
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

            mCondStr = mCondStr & " AND H.V_Date Between " & AgL.ConvertDate(ObjRFG.ParameterDate1_Value) & " And " & AgL.ConvertDate(ObjRFG.ParameterDate2_Value) & " "
            mCondStr = mCondStr & ObjRFG.GetWhereCondition("L.Item", 0)
            mCondStr = mCondStr & ObjRFG.GetWhereCondition("H.Vendor", 1)
            mCondStr = mCondStr & ObjRFG.GetWhereCondition("H.Site_Code", 2)


            mQry = " SELECT L.DocId, L.Sr, L.PurchOrder, L.PurchChallan, L.PurchChallanSr, L.BaleNo, L.Item, I.ManualCode AS ItemManualCode, " & _
                        " L.SalesTaxGroupItem, L.DocQty, " & _
                        " L.Qty, L.Unit, L.MeasurePerPcs, L.MeasureUnit, L.TotalDocMeasure, L.TotalMeasure, L.Rate, L.Amount, L.ReferenceDocId,  " & _
                        " L.LotNo, L.UID, L.Specification, L.Gross_Amount, " & _
                        " L.Discount_Per, L.Discount, L.Other_Charges_Per, L.Other_Charges, L.Round_Off, L.Net_Amount, " & _
                        " I.Description AS ItemDesc, H.V_Date, H.ReferenceNo, Sg.DispName As VendorName, L.Remark " & _
                        " FROM PurchInvoiceDetail L " & _
                        " LEFT JOIN PurchInvoice H ON L.DocId = H.DocId " & _
                        " LEFT JOIN Item I ON L.Item = I.Code " & _
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
