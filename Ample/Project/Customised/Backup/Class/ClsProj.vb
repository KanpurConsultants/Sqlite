Public Class ClsProj



    Public Shared Function FGetGroupBV(ByVal Distributer As String, ByVal IsCurrentMonth As Boolean, Optional ByVal FromDate As String = "", Optional ByVal ToDate As String = "") As Double
        Dim bStartDate$ = "", bEndDate$ = "", mQry$
        Try
            If IsCurrentMonth Then
                bStartDate = AgL.RetMonthStartDate(AgL.PubLoginDate)
                bEndDate = AgL.RetMonthEndDate(AgL.PubLoginDate)
            Else
                bStartDate = AgL.RetMonthStartDate(DateAdd(DateInterval.Month, -1, CDate(AgL.PubLoginDate)))
                bEndDate = AgL.RetMonthEndDate(DateAdd(DateInterval.Month, -1, CDate(AgL.PubLoginDate)))
            End If

            If FromDate <> "" And ToDate <> "" Then
                bStartDate = AgL.RetDate(FromDate)
                bEndDate = AgL.RetDate(ToDate)
            End If


            mQry = " WITH DirectReports(ParentDistributer, SubCode, Level) " & _
                        " AS " & _
                        " ( " & _
                        "   SELECT Sg.ParentDistributer , Sg.SubCode, 0 AS Level " & _
                        "   FROM SubGroup Sg " & _
                        "   WHERE Sg.ParentDistributer = '" & Distributer & "' " & _
                        "   UNION ALL " & _
                        "   SELECT D.ParentDistributer, Sg.SubCode, Level + 1 " & _
                        "   FROM (SELECT ParentDistributer, SubCode FROM SubGroup  " & _
                        "   WHERE SubGroupType = '" & ClsMain.SubGroupType.Distributer & "') " & _
                        " 	AS  Sg " & _
                        "   INNER JOIN DirectReports d ON Sg.ParentDistributer = d.SubCode " & _
                        " ) " & _
                        " SELECT IsNull(Sum(L.BusinessVolume),0) As GroupBusinessVolume  " & _
                        " FROM SaleInvoice H  " & _
                        " LEFT JOIN SaleInvoiceDetail L ON H.DocID = L.DocId " & _
                        " WHERE (SaleToParty IN  " & _
                        " ( " & _
                        " SELECT SubCode FROM DirectReports " & _
                        " ) or SaleToParty = '" & Distributer & "') " & _
                        " AND H.V_Date BETWEEN '" & bStartDate & "' And '" & bEndDate & "'  "
            FGetGroupBV = AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar)




        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function


    Public Shared Function FGetGroupPV(ByVal Distributer As String, ByVal IsCurrentMonth As Boolean, Optional ByVal FromDate As String = "", Optional ByVal ToDate As String = "") As Double
        Dim bStartDate$ = "", bEndDate$ = "", mQry$
        Try
            If IsCurrentMonth Then
                bStartDate = AgL.RetMonthStartDate(AgL.PubLoginDate)
                bEndDate = AgL.RetMonthEndDate(AgL.PubLoginDate)
            Else
                bStartDate = AgL.RetMonthStartDate(DateAdd(DateInterval.Month, -1, CDate(AgL.PubLoginDate)))
                bEndDate = AgL.RetMonthEndDate(DateAdd(DateInterval.Month, -1, CDate(AgL.PubLoginDate)))
            End If


            If FromDate <> "" And ToDate <> "" Then
                bStartDate = AgL.RetDate(FromDate)
                bEndDate = AgL.RetDate(ToDate)
            End If



            mQry = " WITH DirectReports (ParentDistributer, SubCode, Level) " & _
                        " AS " & _
                        " ( " & _
                        "   SELECT Sg.ParentDistributer , Sg.SubCode, 0 AS Level " & _
                        "   FROM SubGroup Sg  " & _
                        "   WHERE Sg.ParentDistributer = '" & Distributer & "' " & _
                        "   UNION ALL " & _
                        "   SELECT D.ParentDistributer, Sg.SubCode, Level + 1 " & _
                        "   FROM (SELECT ParentDistributer, SubCode FROM SubGroup  " & _
                        "   WHERE SubGroupType = '" & ClsMain.SubGroupType.Distributer & "') " & _
                        " 	AS  Sg " & _
                        "   INNER JOIN DirectReports d ON Sg.ParentDistributer = d.SubCode " & _
                        " ) " & _
                        " SELECT IsNull(Sum(L.PointValue),0) As ExclPV  " & _
                        " FROM SaleInvoice H  " & _
                        " LEFT JOIN SaleInvoiceDetail L ON H.DocID = L.DocId " & _
                        " WHERE SaleToParty IN  " & _
                        " ( " & _
                        " SELECT SubCode FROM DirectReports " & _
                        " ) " & _
                        " AND H.V_Date BETWEEN '" & bStartDate & "' And '" & bEndDate & "'  "
            FGetGroupPV = AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar)




        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function

    Public Shared Function FGetSelfPV(ByVal Distributer As String, ByVal IsCurrentMonth As Boolean, Optional ByVal FromDate As String = "", Optional ByVal ToDate As String = "") As Double
        Dim bStartDate$ = "", bEndDate$ = "", mQry$
        Try
            If IsCurrentMonth Then
                bStartDate = AgL.RetMonthStartDate(AgL.PubLoginDate)
                bEndDate = AgL.RetMonthEndDate(AgL.PubLoginDate)
            Else
                bStartDate = AgL.RetMonthStartDate(DateAdd(DateInterval.Month, -1, CDate(AgL.PubLoginDate)))
                bEndDate = AgL.RetMonthEndDate(DateAdd(DateInterval.Month, -1, CDate(AgL.PubLoginDate)))
            End If

            If FromDate <> "" And ToDate <> "" Then
                bStartDate = AgL.RetDate(FromDate)
                bEndDate = AgL.RetDate(ToDate)
            End If


            mQry = " SELECT IsNull(Sum(L.PointValue),0) " & _
                    " FROM SaleInvoice H  " & _
                    " LEFT JOIN SaleInvoiceDetail L ON H.DocID = L.DocId " & _
                    " WHERE H.SaleToParty = '" & Distributer & "' " & _
                    " AND H.V_Date BETWEEN '" & bStartDate & "' And '" & bEndDate & "'  "
            FGetSelfPV = AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function

    Public Shared Function FGetSelfBV(ByVal Distributer As String, ByVal IsCurrentMonth As Boolean, Optional ByVal FromDate As String = "", Optional ByVal ToDate As String = "") As Double
        Dim bStartDate$ = "", bEndDate$ = "", mQry$
        Try
            If IsCurrentMonth Then
                bStartDate = AgL.RetMonthStartDate(AgL.PubLoginDate)
                bEndDate = AgL.RetMonthEndDate(AgL.PubLoginDate)
            Else
                bStartDate = AgL.RetMonthStartDate(DateAdd(DateInterval.Month, -1, CDate(AgL.PubLoginDate)))
                bEndDate = AgL.RetMonthEndDate(DateAdd(DateInterval.Month, -1, CDate(AgL.PubLoginDate)))
            End If

            If FromDate <> "" And ToDate <> "" Then
                bStartDate = AgL.RetDate(FromDate)
                bEndDate = AgL.RetDate(ToDate)
            End If


            mQry = " SELECT IsNull(Sum(L.BusinessVolume),0) " & _
                    " FROM SaleInvoice H  " & _
                    " LEFT JOIN SaleInvoiceDetail L ON H.DocID = L.DocId " & _
                    " WHERE H.SaleToParty = '" & Distributer & "' " & _
                    " AND H.V_Date BETWEEN '" & bStartDate & "' And '" & bEndDate & "'  "
            FGetSelfBV = AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function


    Public Shared Function FGetTotalBV(ByVal Distributer As String, ByVal IsCurrentMonth As Boolean, Optional ByVal FromDate As String = "", Optional ByVal ToDate As String = "") As Double
        Dim bStartDate$ = "", bEndDate$ = "", mQry$
        Try
            If IsCurrentMonth Then
                bStartDate = AgL.RetMonthStartDate(AgL.PubLoginDate)
                bEndDate = AgL.RetMonthEndDate(AgL.PubLoginDate)
            Else
                bStartDate = AgL.RetMonthStartDate(DateAdd(DateInterval.Month, -1, CDate(AgL.PubLoginDate)))
                bEndDate = AgL.RetMonthEndDate(DateAdd(DateInterval.Month, -1, CDate(AgL.PubLoginDate)))
            End If


            If FromDate <> "" And ToDate <> "" Then
                bStartDate = AgL.RetDate(FromDate)
                bEndDate = AgL.RetDate(ToDate)
            End If


            mQry = " SELECT IsNull(Sum(L.BusinessVolume),0) " & _
                    " FROM SaleInvoice H  " & _
                    " LEFT JOIN SaleInvoiceDetail L ON H.DocID = L.DocId " & _
                    " WHERE H.SaleToParty = '" & Distributer & "' " & _
                    " AND H.V_Date BETWEEN '" & bStartDate & "' And '" & bEndDate & "'  "
            FGetTotalBV = AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function

    Public Shared Function FGetTotalComPV(ByVal Distributer As String, ByVal IsCurrentMonth As Boolean, Optional ByVal FromDate As String = "", Optional ByVal ToDate As String = "") As Double
        Dim bStartDate$ = "", bEndDate$ = "", mQry$
        Dim bSelfPV As Double = 0, bDownLinePV As Double = 0
        Try

            If IsCurrentMonth Then
                bStartDate = AgL.RetMonthStartDate(AgL.PubLoginDate)
                bEndDate = AgL.RetMonthEndDate(AgL.PubLoginDate)
            Else
                bStartDate = AgL.RetMonthStartDate(DateAdd(DateInterval.Month, -1, CDate(AgL.PubLoginDate)))
                bEndDate = AgL.RetMonthEndDate(DateAdd(DateInterval.Month, -1, CDate(AgL.PubLoginDate)))
            End If

            If FromDate <> "" And ToDate <> "" Then
                bStartDate = AgL.RetDate(FromDate)
                bEndDate = AgL.RetDate(ToDate)
            End If


            mQry = " WITH DirectReports (ParentDistributer, SubCode, Level) " & _
                        " AS " & _
                        " ( " & _
                        "   SELECT Sg.ParentDistributer , Sg.SubCode, 0 AS Level " & _
                        "   FROM SubGroup Sg  " & _
                        "   WHERE Sg.ParentDistributer = '" & Distributer & "' " & _
                        "   UNION ALL " & _
                        "   SELECT D.ParentDistributer, Sg.SubCode, Level + 1 " & _
                        "   FROM (SELECT ParentDistributer, SubCode FROM SubGroup  " & _
                        "   WHERE SubGroupType = '" & ClsMain.SubGroupType.Distributer & "') " & _
                        " 	AS  Sg " & _
                        "   INNER JOIN DirectReports d ON Sg.ParentDistributer = d.SubCode " & _
                        " ) " & _
                        " SELECT IsNull(Sum(L.PointValue),0) As ExclPV  " & _
                        " FROM SaleInvoice H  " & _
                        " LEFT JOIN SaleInvoiceDetail L ON H.DocID = L.DocId " & _
                        " WHERE SaleToParty IN  " & _
                        " ( " & _
                        " SELECT SubCode FROM DirectReports" & _
                        " )  " & _
                        " AND H.V_Date <= '" & bEndDate & "'  "
            bDownLinePV = AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar)

            mQry = " SELECT IsNull(Sum(L.PointValue),0) " & _
                    " FROM SaleInvoice H  " & _
                    " LEFT JOIN SaleInvoiceDetail L ON H.DocID = L.DocId " & _
                    " WHERE H.SaleToParty = '" & Distributer & "' " & _
                    " AND H.V_Date <= '" & bEndDate & "'  "
            bSelfPV = AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar)

            FGetTotalComPV = bDownLinePV + bSelfPV
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function

    Public Shared Function FGetTotalComBV(ByVal Distributer As String, ByVal IsCurrentMonth As Boolean, Optional ByVal FromDate As String = "", Optional ByVal ToDate As String = "") As Double
        Dim bStartDate$ = "", bEndDate$ = "", mQry$
        Dim bSelfPV As Double = 0, bDownLinePV As Double = 0
        Try

            If IsCurrentMonth Then
                bStartDate = AgL.RetMonthStartDate(AgL.PubLoginDate)
                bEndDate = AgL.RetMonthEndDate(AgL.PubLoginDate)
            Else
                bStartDate = AgL.RetMonthStartDate(DateAdd(DateInterval.Month, -1, CDate(AgL.PubLoginDate)))
                bEndDate = AgL.RetMonthEndDate(DateAdd(DateInterval.Month, -1, CDate(AgL.PubLoginDate)))
            End If

            If FromDate <> "" And ToDate <> "" Then
                bStartDate = AgL.RetDate(FromDate)
                bEndDate = AgL.RetDate(ToDate)
            End If


            mQry = " WITH DirectReports (ParentDistributer, SubCode, Level) " & _
                        " AS " & _
                        " ( " & _
                        "   SELECT Sg.ParentDistributer , Sg.SubCode, 0 AS Level " & _
                        "   FROM SubGroup Sg  " & _
                        "   WHERE Sg.ParentDistributer = '" & Distributer & "' " & _
                        "   UNION ALL " & _
                        "   SELECT D.ParentDistributer, Sg.SubCode, Level + 1 " & _
                        "   FROM (SELECT ParentDistributer, SubCode FROM SubGroup  " & _
                        "   WHERE SubGroupType = '" & ClsMain.SubGroupType.Distributer & "') " & _
                        " 	AS  Sg " & _
                        "   INNER JOIN DirectReports d ON Sg.ParentDistributer = d.SubCode " & _
                        " ) " & _
                        " SELECT IsNull(Sum(L.BusinessVolume),0) As ExclPV  " & _
                        " FROM SaleInvoice H  " & _
                        " LEFT JOIN SaleInvoiceDetail L ON H.DocID = L.DocId " & _
                        " WHERE SaleToParty IN  " & _
                        " ( " & _
                        " SELECT SubCode FROM DirectReports" & _
                        " )  " & _
                        " AND H.V_Date <= '" & bEndDate & "'  "
            bDownLinePV = AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar)

            mQry = " SELECT IsNull(Sum(L.BusinessVolume),0) " & _
                    " FROM SaleInvoice H  " & _
                    " LEFT JOIN SaleInvoiceDetail L ON H.DocID = L.DocId " & _
                    " WHERE H.SaleToParty = '" & Distributer & "' " & _
                    " AND H.V_Date <= '" & bEndDate & "'  "
            bSelfPV = AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar)

            FGetTotalComBV = bDownLinePV + bSelfPV
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function


    Public Shared Function FGetPVPer(ByVal PV As Double) As Double
        Dim bStartDate$ = "", bEndDate$ = "", mQry$
        Try
            mQry = " SELECT CommPer FROM PVCommission WHERE " & PV & "  BETWEEN  FromBV AND ToBV "
            FGetPVPer = AgL.VNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function
End Class
