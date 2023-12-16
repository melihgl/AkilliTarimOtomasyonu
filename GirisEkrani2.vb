﻿Imports System.Data.SqlClient
Public Class GirisEkrani2
    Dim connection As New SqlConnection("Data Source=.\melih;Initial Catalog=AkilliTarimOtomasyonu;Integrated Security=True")
    Private Sub girisDugmesi_Click(sender As Object, e As EventArgs) Handles girisDugmesi.Click
        If String.IsNullOrWhiteSpace(txtkullaniciAdi.Text) OrElse String.IsNullOrWhiteSpace(txtkullaniciSifresi.Text) Then
            MessageBox.Show("Kullanıcı adı ve şifre boş olamaz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Return
        End If
        If AuthenticateUser(txtkullaniciAdi.Text, txtkullaniciSifresi.Text) Then
            ProgressBar2.Visible = True
            Timer2.Enabled = True
            Timer2.Start()
        Else
            MessageBox.Show("Kullanıcı adı veya şifre yanlış!")
        End If
    End Sub
    Private Function AuthenticateUser(username As String, password As String) As Boolean
        Try
            connection.Open()
            Dim query As String = "SELECT COUNT(*) FROM Kullanici WHERE kullaniciAdi=@kullaniciAdi AND kullaniciSifresi=@kullaniciSifresi"
            Dim command As New SqlCommand(query, connection)
            command.Parameters.AddWithValue("@kullaniciAdi", username)
            command.Parameters.AddWithValue("@kullaniciSifresi", password)
            Dim result As Integer = Convert.ToInt32(command.ExecuteScalar())
            Return result > 0
        Catch ex As Exception
            MessageBox.Show("Veritabanı hatası: " & ex.Message)
            Return False
        Finally
            connection.Close()
        End Try
    End Function
    Private Sub Timer2_Tick(sender As Object, e As EventArgs) Handles Timer2.Tick
        ProgressBar2.Minimum = 0
        ProgressBar2.Maximum = 500
        ProgressBar2.Value += 100
        If ProgressBar2.Value >= ProgressBar2.Maximum Then
            Timer2.Stop()
            ProgressBar2.Value = ProgressBar2.Maximum
            BilgiEkrani1.Show()
            Me.Hide()
        End If
    End Sub
End Class