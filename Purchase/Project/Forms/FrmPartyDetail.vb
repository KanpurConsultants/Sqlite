Imports System.Data.SqlClient
Public Class FrmPartyDetail
    Dim mQry As String = ""

    Dim DtMaster As DataTable = Nothing

    Public mOkButtonPressed As Boolean = False

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub Form_Paint(ByVal sender As Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles Me.Paint
        AgL.FPaintForm(Me, e, 0)
    End Sub

    Private Sub KeyDown_Form(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If Me.ActiveControl IsNot Nothing Then
            If Not (TypeOf (Me.ActiveControl) Is AgControls.AgDataGrid) Then
                If e.KeyCode = Keys.Return Then SendKeys.Send("{Tab}")
            End If
            If e.KeyCode = Keys.Escape Then Me.Close()
        End If
    End Sub

    Sub KeyPress_Form(ByVal Sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        If e.KeyChar = Chr(Keys.Escape) Then Exit Sub
        If Me.ActiveControl Is Nothing Then Exit Sub
        AgL.CheckQuote(e)
    End Sub

    Private Sub Form_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            BtnOk.Anchor = AnchorStyles.Top + AnchorStyles.Bottom + AnchorStyles.Left + AnchorStyles.Right
            BtnCancel.Anchor = AnchorStyles.Top + AnchorStyles.Bottom + AnchorStyles.Left + AnchorStyles.Right
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub BlankText()

    End Sub

    Public Sub DispText(ByVal Enable As Boolean)
        TxtMobile.Enabled = Enable
        TxtName.Enabled = Enable
        TxtAdd1.Enabled = Enable
        TxtAdd2.Enabled = Enable
        TxtCity.Enabled = Enable

        TxtMobile.BackColor = Color.White
        TxtName.BackColor = Color.White
        TxtAdd1.BackColor = Color.White
        TxtAdd2.BackColor = Color.White
        TxtCity.BackColor = Color.White
    End Sub

    Private Function Data_Validation() As Boolean
        Dim I As Integer = 0
        Try
            Data_Validation = True
        Catch ex As Exception
            MsgBox(ex.Message)
            Data_Validation = False
        End Try
    End Function

    Private Sub BtnChargeDuw_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnOk.Click, BtnCancel.Click
        Try
            Select Case sender.Name
                Case BtnOk.Name
                    If Not Data_Validation() Then Exit Sub
                    mOkButtonPressed = True
                    Me.Close()

                Case BtnCancel.Name
                    mOkButtonPressed = False
                    Me.Close()
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub TxtSaleToPartyCity_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtCity.Enter
        Try
            Select Case sender.Name
                Case TxtCity.Name
                    mQry = " SELECT C.CityCode AS Code, C.CityName FROM City C "
                    TxtCity.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub TxtSaleToPartyMobile_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtMobile.Validating
        Dim DtTemp As DataTable = Nothing
        Try
            Select Case sender.Name
                Case TxtMobile.Name
                    If TxtMobile.Text <> "" And TxtName.Text = "" Then
                        mQry = " Select H.SaleToPartyName, H.SaleToPartyAddress, H.SaleToPartyCity, C.CityName As SaleToPartyCityName " & _
                                " From SaleInvoice H " & _
                                " LEFT JOIN City C On H.SaleToPartyCity = C.CityCode " & _
                                " Where H.SaleToPartyMobile = '" & TxtMobile.Text & "' "
                        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
                        With DtTemp
                            If .Rows.Count > 0 Then
                                TxtName.Text = AgL.XNull(.Rows(0)("SaleToPartyName"))
                                TxtAdd1.Text = AgL.XNull(.Rows(0)("SaleToPartyAdd1"))
                                TxtAdd2.Text = AgL.XNull(.Rows(0)("SaleToPartyAdd2"))
                                TxtCity.Tag = AgL.XNull(.Rows(0)("SaleToPartyCity"))
                                TxtCity.Text = AgL.XNull(.Rows(0)("SaleToPartyCityName"))
                            End If
                        End With
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class