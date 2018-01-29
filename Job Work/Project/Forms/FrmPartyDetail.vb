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
        TxtPartyMobile.Enabled = Enable
        TxtPartyName.Enabled = Enable
        TxtPartyAdd1.Enabled = Enable
        TxtPartyAdd2.Enabled = Enable
        TxtPartyCity.Enabled = Enable
        TxtPartyTinNo.Enabled = Enable
        TxtPartyCSTNo.Enabled = Enable
        TxtPartyLSTNo.Enabled = Enable

        TxtPartyMobile.BackColor = Color.White
        TxtPartyName.BackColor = Color.White
        TxtPartyAdd1.BackColor = Color.White
        TxtPartyAdd2.BackColor = Color.White
        TxtPartyCity.BackColor = Color.White
        TxtPartyTinNo.BackColor = Color.White
        TxtPartyCSTNo.BackColor = Color.White
        TxtPartyLSTNo.BackColor = Color.White
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

    Private Sub TxtSaleToPartyCity_Enter(ByVal sender As Object, ByVal e As System.EventArgs) Handles TxtPartyCity.Enter
        Try
            Select Case sender.Name
                Case TxtPartyCity.Name
                    mQry = " SELECT C.CityCode AS Code, C.CityName FROM City C "
                    TxtPartyCity.AgHelpDataSet = AgL.FillData(mQry, AgL.GCn)
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub TxtSaleToPartyMobile_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles TxtPartyMobile.Validating
        Dim DtTemp As DataTable = Nothing
        Try
            Select Case sender.Name
                Case TxtPartyMobile.Name
                    If TxtPartyMobile.Text <> "" And TxtPartyName.Text = "" Then
                        mQry = " Select H.SaleToPartyName, H.SaleToPartyAddress, H.SaleToPartyCity, C.CityName As SaleToPartyCityName, H.SaleToPartyTinNo, H.SaleToPartyCSTNo, H.LSTNo " & _
                                " From SaleInvoice H " & _
                                " LEFT JOIN City C On H.SaleToPartyCity = C.CityCode " & _
                                " Where H.SaleToPartyMobile = '" & TxtPartyMobile.Text & "' "
                        DtTemp = AgL.FillData(mQry, AgL.GCn).Tables(0)
                        With DtTemp
                            If .Rows.Count > 0 Then
                                TxtPartyName.Text = AgL.XNull(.Rows(0)("SaleToPartyName"))
                                TxtPartyAdd1.Text = AgL.XNull(.Rows(0)("SaleToPartyAdd1"))
                                TxtPartyAdd2.Text = AgL.XNull(.Rows(0)("SaleToPartyAdd2"))
                                TxtPartyCity.Tag = AgL.XNull(.Rows(0)("SaleToPartyCity"))
                                TxtPartyCity.Text = AgL.XNull(.Rows(0)("SaleToPartyCityName"))
                                TxtPartyTinNo.Text = AgL.XNull(.Rows(0)("SaleToPartyTINNo"))
                                TxtPartyCSTNo.Text = AgL.XNull(.Rows(0)("SaleToPartyCSTNo"))
                                TxtPartyLSTNo.Text = AgL.XNull(.Rows(0)("LSTNo"))
                            End If
                        End With
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
End Class