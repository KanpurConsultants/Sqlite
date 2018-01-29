Imports System.IO
Public Class FrmPurchImportFromExcel
    Public WithEvents Dgl1 As New AgControls.AgDataGrid

    Dim MyConnection As System.Data.OleDb.OleDbConnection

    Dim mImportFor As FrmPurchChallan
    Dim mQry$ = ""

    Public Property ImportFor() As FrmPurchChallan
        Get
            ImportFor = mImportFor
        End Get
        Set(ByVal value As FrmPurchChallan)
            mImportFor = value
        End Set
    End Property

    Private Sub Ini_Grid()
        AgL.AddAgDataGrid(Dgl1, Panel2)
        Dgl1.ColumnHeadersHeight = 40
        Dgl1.EnableHeadersVisualStyles = False
        AgL.GridDesign(Dgl1)

        mQry = "Select  '' as Srl,'Item' as [Field Name], 'Text' as [Data Type], 255 as [Length] "
        mQry = mQry + "Union All Select  '' as Srl,'Qty' as [Field Name], 'Number' as [Data Type], '' as [Length] "
        mQry = mQry + "Union All Select  '' as Srl,'Deal' as [Field Name], 'Text' as [Data Type], 255 as [Length] "
        mQry = mQry + "Union All Select  '' as Srl,'MRP' as [Field Name], 'Number' as [Data Type], '' as [Length] "
        mQry = mQry + "Union All Select  '' as Srl,'Rate' as [Field Name], 'Number' as [Data Type], '' as [Length] "
        mQry = mQry + "Union All Select  '' as Srl,'SaleRate' as [Field Name], 'Number' as [Data Type], '' as [Length] "
        mQry = mQry + "Union All Select  '' as Srl,'Manufacturer' as [Field Name], 'Text' as [Data Type], 30 as [Length] "
        mQry = mQry + "Union All Select  '' as Srl,'BatchNo' as [Field Name], 'Text' as [Data Type], 30 as [Length] "
        mQry = mQry + "Union All Select  '' as Srl,'Expiry' as [Field Name], 'Text' as [Data Type], 30 as [Length] "
        mQry = mQry + "Union All Select  '' as Srl,'Vendor' as [Field Name], 'Text' as [Data Type], 30 as [Length] "
        mQry = mQry + "Union All Select  '' as Srl,'VendorDocNo' as [Field Name], 'Text' as [Data Type], 30 as [Length] "
        mQry = mQry + "Union All Select  '' as Srl,'VendorDocDate' as [Field Name], 'Text' as [Data Type], 30 as [Length] "

        Dgl1.DataSource = AgL.FillData(mQry, AgL.GCn).Tables(0)
        Dgl1.Columns(0).Width = 40
        Dgl1.Columns(1).Width = 150
        Dgl1.Columns(2).Width = 80
        Dgl1.Columns(3).Width = 80
        Dgl1.ReadOnly = True
        Dgl1.AllowUserToAddRows = False

        AgCL.AddAgTextColumn(Dgl1, "CFieldName", 100, 0, "CFieldName", False)
    End Sub

    Private Sub FrmImportFromExcel_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Ini_Grid()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSelectExcelFile.Click
        Dim MyCommand As System.Data.OleDb.OleDbDataAdapter = Nothing
        Dim DsTemp As New DataSet
        Dim myExcelFilePath As String

        Opn.Filter = "Excel Files (*.xls)|*.xls"
        Opn.ShowDialog()
        myExcelFilePath = Opn.FileName
        TxtExcelPath.Text = myExcelFilePath
        MyConnection = New System.Data.OleDb.OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0; " & _
                       "data source='" & myExcelFilePath & " '; " & "Extended Properties=Excel 8.0;")
        MyConnection.Open()

        FCheckExcelFile(MyConnection)
    End Sub

    Private Sub BtnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnOK.Click, BtnCancel.Click
        Dim MyCommand As OleDb.OleDbDataAdapter = Nothing
        Select Case sender.name
            Case BtnOK.Name
                ProcImportFromExcel()

            Case BtnCancel.Name
                Me.Dispose()
        End Select
    End Sub

    Private Sub FCheckExcelFile(ByVal mConn As OleDb.OleDbConnection)
        Dim MyCommand As System.Data.OleDb.OleDbDataAdapter = Nothing
        Dim DsTemp As New DataSet
        Dim I As Integer, J As Integer
        Dim mFieldExist As Boolean = False
        Try
            MyCommand = New System.Data.OleDb.OleDbDataAdapter("select *  from [sheet1$] Where 1=2", mConn)
            MyCommand.Fill(DsTemp)

            For I = 0 To Dgl1.Rows.Count - 1
                If AgL.StrCmp(Dgl1.Item(4, I).Value, "Yes") Then
                    mFieldExist = False
                    For J = 0 To DsTemp.Tables(0).Columns.Count - 1

                        If AgL.StrCmp(Dgl1.Item(1, I).Value, DsTemp.Tables(0).Columns(J).ColumnName) Then
                            mFieldExist = True
                            Exit For
                        End If

                    Next

                    If mFieldExist = False Then
                        Dgl1.Item("CFieldName", I).Value = "1"
                    End If
                Else
                    Dgl1.Item("CFieldName", I).Value = ""
                End If
            Next

            Dim StrMsg$ = ""
            For I = 0 To Dgl1.Rows.Count - 1
                If AgL.StrCmp(Dgl1.Item("CFieldName", I).Value, "1") Then
                    If StrMsg.ToString.Length <> 0 Then StrMsg += ", "
                    StrMsg += Dgl1.Item(1, I).Value
                End If
            Next
            If StrMsg.ToString.Length > 0 Then
                MsgBox(StrMsg & " - Fields not found in excel file. Please Select Correct File. ")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            'DsTemp.Dispose()
        End Try
    End Sub

    Private Sub ProcImportFromExcel()
        Dim MyCommand As OleDb.OleDbDataAdapter = Nothing
        Dim DtMain As New DataTable
        Dim DtLine As New DataTable
        Dim DtTemp As New DataTable
        Dim DtItem As New DataTable
        Dim mQry$ = "", ErrorLog$ = "", bFileName$ = ""
        Dim I As Integer, J As Integer = 0
        Dim ShowErrMsg As Boolean = False
        Dim StrErrLog As String = ""
        Try
            MyCommand = New System.Data.OleDb.OleDbDataAdapter("Select Max(Vendor) As Vendor, Max(PurchaseDate) As PurchaseDate, Max(VendorDocDate) As VendorDocDate, VendorDocNo  from [sheet1$] Group BY  VendorDocNo ", MyConnection)
            MyCommand.Fill(DtMain)

            MyCommand = New System.Data.OleDb.OleDbDataAdapter("Select *  from [sheet1$] ", MyConnection)
            MyCommand.Fill(DtLine)

            bFileName = TxtExcelPath.Text

            For I = 0 To DtMain.Rows.Count - 1
                If AgL.XNull(DtMain.Rows(I)("VendorDocNo")) <> "" Then
                    If AgL.XNull(DtMain.Rows(I)("Vendor")) <> "" Then
                        mQry = " Select Count(*) From SubGroup Where DispName = " & AgL.Chk_Text(AgL.XNull(DtMain.Rows(I)("Vendor"))) & ""
                        If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar = 0 Then
                            If ErrorLog = "" Then
                                ErrorLog = "These Parties Are Not Present In Master" & vbCrLf
                                ErrorLog += AgL.XNull(DtMain.Rows(I)("Vendor")) & ", "
                            Else
                                ErrorLog += AgL.XNull(DtMain.Rows(I)("Vendor")) & ", "
                            End If
                        End If
                    End If
                End If
            Next

            For I = 0 To DtMain.Rows.Count - 1
                If AgL.XNull(DtMain.Rows(I)("VendorDocNo")) <> "" Then
                    If AgL.XNull(DtLine.Rows(I)("Item")) <> "" Then
                        mQry = " Select Count(*) From Item Where Description = " & AgL.Chk_Text(AgL.XNull(DtLine.Rows(I)("Item"))) & " "
                        If AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar = 0 Then
                            If ErrorLog = "" Then
                                ErrorLog = vbCrLf & "These Items Are Not Present In Master" & vbCrLf
                                ErrorLog += AgL.XNull(DtLine.Rows(I)("Item")) & ", "
                            Else
                                ErrorLog += AgL.XNull(DtLine.Rows(I)("Item")) & ", "
                            End If
                        End If
                    End If
                End If
            Next

            'For I = 0 To DtMain.Rows.Count - 1
            '    If AgL.XNull(DtMain.Rows(I)("VendorDocNo")) <> "" Then
            '        If CDate(DtMain.Rows(I)("PurchaseDate")) < CDate(AgL.PubStartDate) Then
            '            ErrorLog += "Purchase Date is Less than Current Year Start Date For Vendor Doc No " & DtMain.Rows(I)("VendorDocNo") & "." & vbCrLf
            '        End If
            '    End If
            'Next

            For I = 0 To DtLine.Rows.Count - 1
                If AgL.XNull(DtLine.Rows(I)("VendorDocNo")) <> "" Then
                    If AgL.VNull(DtLine.Rows(I)("Qty")) = 0 Then
                        ErrorLog += "Qty is 0 at row no " & (I + 1).ToString & "" & vbCrLf
                        Exit Sub
                    End If
                End If
            Next


            If ErrorLog <> "" Then
                If File.Exists(My.Application.Info.DirectoryPath + " \ " + "ErrorLog.txt") Then
                    My.Computer.FileSystem.WriteAllText(My.Application.Info.DirectoryPath + "\" + "ErrorLog.txt", ErrorLog, False)
                Else
                    File.Create(My.Application.Info.DirectoryPath + " \ " + "ErrorLog.txt")
                    My.Computer.FileSystem.WriteAllText(My.Application.Info.DirectoryPath + " \ " + "ErrorLog.txt", ErrorLog, False)
                End If
                System.Diagnostics.Process.Start("notepad.exe", My.Application.Info.DirectoryPath + "\" + "ErrorLog.txt")
                Exit Sub
            End If

            For I = 0 To DtMain.Rows.Count - 1
                If AgL.XNull(DtMain.Rows(I)("VendorDocNo")) <> "" Then
                    mImportFor.Topctrl1.FButtonClick(0)
                    mImportFor.TxtV_Type.AgSelectedValue = mImportFor.TxtV_Type.AgHelpDataSet.Tables(0).Rows(0)("Code")
                    mImportFor.LblV_Type.Tag = AgL.XNull(mImportFor.TxtV_Type.AgHelpDataSet.Tables(0).Rows(0)("NCat"))
                    mImportFor.TxtStructure.AgSelectedValue = AgStructure.ClsMain.FGetStructureFromNCat(mImportFor.LblV_Type.Tag, AgL.GcnRead)
                    mImportFor.AgCalcGrid1.AgStructure = mImportFor.TxtStructure.AgSelectedValue
                    mImportFor.IniGrid()
                    mImportFor.TxtReferenceNo.Text = AgTemplate.ClsMain.FGetManualRefNo("ReferenceNo", "PurchChallan", mImportFor.TxtV_Type.Tag, mImportFor.TxtV_Date.Text, mImportFor.TxtDivision.Tag, mImportFor.TxtSite_Code.Tag, AgTemplate.ClsMain.ManualRefType.Max)
                    mImportFor.TxtVendor.Text = AgL.XNull(DtMain.Rows(I)("Vendor"))

                    mQry = " Select Sg.SubCode, Mobile As SaleToPartyMobile, DispName As SaleToPartyName, " & _
                            " IfNull(Add1,'') || ' ' || IfNull(Add2,'')  || ' ' || IfNull(Add3,'')  As SaleToPartyAddress, " & _
                            " Sg.CityCode As SaleToPartyCity, C.CityName As SaleToPartyCityName, " & _
                            " Sg.Currency, Cu.Description As CurrencyDesc, Sg.SalesTaxPostingGroup, Sg.Nature  " & _
                            " From SubGroup Sg " & _
                            " LEFT JOIN City C ON Sg.CityCode = C.CityCode " & _
                            " LEFT JOIN Currency Cu On Sg.Currency = Cu.Code " & _
                            " Where Sg.DispName = '" & mImportFor.TxtVendor.Text & "'  "
                    DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
                    With DtTemp
                        If DtTemp.Rows.Count > 0 Then
                            mImportFor.TxtVendor.Tag = AgL.XNull(.Rows(0)("SubCode"))
                            mImportFor.TxtCurrency.Tag = AgL.XNull(.Rows(0)("Currency"))
                            mImportFor.TxtCurrency.Text = AgL.XNull(.Rows(0)("CurrencyDesc"))
                            mImportFor.TxtSalesTaxGroupParty.Tag = AgL.XNull(.Rows(0)("SalesTaxPostingGroup"))
                            mImportFor.TxtSalesTaxGroupParty.Text = AgL.XNull(.Rows(0)("SalesTaxPostingGroup"))
                        End If
                    End With

                    mImportFor.TxtVendorDocNo.Text = AgL.XNull(DtMain.Rows(I)("VendorDocNo"))
                    mImportFor.TxtVendorDocDate.Text = AgL.XNull(DtMain.Rows(I)("VendorDocDate"))
                    mImportFor.TxtV_Date.Text = AgL.XNull(DtMain.Rows(I)("PurchaseDate"))

                    DtTemp = DtLine
                    DtTemp.DefaultView.RowFilter = " VendorDocNo = '" & mImportFor.TxtVendorDocNo.Text & "' "
                    DtTemp = DtTemp.DefaultView.ToTable()

                    For J = 0 To DtTemp.Rows.Count - 1
                        mImportFor.Dgl1.Rows.Add()
                        mImportFor.Dgl1.Item(FrmPurchChallan.ColSNo, J).Value = mImportFor.Dgl1.Rows.Count - 1

                        mImportFor.Dgl1.Item(FrmPurchChallan.Col1Item, J).Value = AgL.XNull(DtTemp.Rows(J)("Item"))
                        mQry = " Select I.Code As ItemCode From Item I Where I.Description = '" & AgL.XNull(DtTemp.Rows(J)("Item")) & "'"
                        mImportFor.Dgl1.Item(FrmPurchChallan.Col1Item, J).Tag = AgL.XNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar)
                        mImportFor.Dgl1.Item(FrmPurchChallan.Col1ItemCode, J).Tag = AgL.XNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar)

                        mQry = " Select ManualCode From Item Where Code = '" & mImportFor.Dgl1.Item(FrmPurchChallan.Col1ItemCode, J).Tag & "'"
                        mImportFor.Dgl1.Item(FrmPurchChallan.Col1ItemCode, J).Value = AgL.XNull(AgL.Dman_Execute(mQry, AgL.GCn).ExecuteScalar)

                        mImportFor.Dgl1.Item(FrmPurchChallan.Col1DocQty, J).Value = AgL.VNull(DtTemp.Rows(J)("Qty"))
                        mImportFor.Dgl1.Item(FrmPurchChallan.Col1Qty, J).Value = AgL.VNull(DtTemp.Rows(J)("Qty"))

                        mImportFor.Dgl1.Item(FrmPurchChallan.Col1LotNo, J).Value = AgL.XNull(DtTemp.Rows(J)("Batch"))

                        mImportFor.Dgl1.Item(FrmPurchChallan.Col1ExpiryDate, J).Value = AgL.XNull(DtTemp.Rows(J)("Expiry"))

                        mImportFor.Dgl1.Item(FrmPurchChallan.Col1Rate, J).Value = AgL.VNull(DtTemp.Rows(J)("Rate"))
                        mImportFor.Dgl1.Item(FrmPurchChallan.Col1SaleRate, J).Value = AgL.VNull(DtTemp.Rows(J)("SaleRate"))
                        mImportFor.Dgl1.Item(FrmPurchChallan.Col1MRP, J).Value = AgL.VNull(DtTemp.Rows(J)("MRP"))

                        mQry = "SELECT I.Unit, I.SalesTaxPostingGroup, I.Measure As MeasurePerPcs, " & _
                                " I.MeasureUnit, I.Rate, " & _
                                " U.DecimalPlaces As QtyDecimalPlaces, U1.DecimalPlaces As MeasureDecimalPlaces, I.BillingOn " & _
                                " FROM Item I " & _
                                " LEFT JOIN Unit U On I.Unit = U.Code " & _
                                " LEFT JOIN Unit U1 On I.MeasureUnit = U1.Code " & _
                                " Where I.Code = '" & mImportFor.Dgl1.Item(FrmPurchChallan.Col1Item, J).Tag & "' "
                        DtItem = AgL.FillData(mQry, AgL.GCn).Tables(0)

                        With DtItem
                            If .Rows.Count > 0 Then
                                mImportFor.Dgl1.Item(FrmPurchChallan.Col1Unit, J).Value = AgL.XNull(DtItem.Rows(0)("Unit"))
                                mImportFor.Dgl1.Item(FrmPurchChallan.Col1SalesTaxGroup, J).Value = AgL.XNull(DtItem.Rows(0)("SalesTaxPostingGroup"))
                                mImportFor.Dgl1.Item(FrmPurchChallan.Col1SalesTaxGroup, J).Tag = AgL.XNull(DtItem.Rows(0)("SalesTaxPostingGroup"))
                                mImportFor.Dgl1.Item(FrmPurchChallan.Col1MeasurePerPcs, J).Value = AgL.VNull(DtItem.Rows(0)("MeasurePerPcs"))
                                mImportFor.Dgl1.Item(FrmPurchChallan.Col1MeasureUnit, J).Value = AgL.XNull(DtItem.Rows(0)("MeasureUnit"))
                                mImportFor.Dgl1.Item(FrmPurchChallan.Col1QtyDecimalPlaces, J).Value = AgL.VNull(DtItem.Rows(0)("QtyDecimalPlaces"))
                                mImportFor.Dgl1.Item(FrmPurchChallan.Col1MeasureDecimalPlaces, J).Value = AgL.VNull(DtItem.Rows(0)("MeasureDecimalPlaces"))
                                mImportFor.Dgl1.Item(FrmPurchChallan.Col1BillingType, J).Value = AgL.XNull(DtItem.Rows(0)("BillingOn"))
                            End If
                        End With
                    Next
                    mImportFor.Calculation()
                    mImportFor.Topctrl1.FButtonClick(13)
                End If
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class