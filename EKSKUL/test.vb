Imports System.Text

Public Class test
    Dim db As New Database
    Private Sub test_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'EKSKULDataSet11.View_1' table. You can move, or remove it, as needed.
        Me.View_1TableAdapter.Fill(Me.EKSKULDataSet11.View_1)
        'TODO: This line of code loads data into the 'EKSKULDataSet10.test' table. You can move, or remove it, as needed.
        Me.TestTableAdapter.Fill(Me.EKSKULDataSet10.test)

        db.conn()
        captcha()
    End Sub


    Private Sub Button1_Click(sender As Object, e As EventArgs)

        'db.insertTest(varid, TextBox1.Text)

        'Me.TestTableAdapter.Fill(Me.EKSKULDataSet10.test)
    End Sub

    Dim varid As String = "S0001"
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim huruf As String = varid.Substring(0, 1)
        Dim id As String = varid.Substring(1)
        Dim angka As Integer = Integer.Parse(id)
        angka += 1
        Label1.Text = huruf & angka.ToString("D" & id.Length)
        varid = huruf & angka.ToString("D" & id.Length)
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim huruf As String = varid.Substring(0, 1)
        Dim id As String = varid.Substring(1)
        Dim angka As Integer = Integer.Parse(id)
        angka -= 1
        Label1.Text = huruf & angka.ToString("D" & id.Length)
        varid = huruf & angka.ToString("D" & id.Length)
    End Sub

    Private Sub captcha()
        Dim randomText As New StringBuilder
        Dim r As New Random
        Dim alphabet As String = "qweriopasdfghjklzxcvbnm123890!@#$%^&*()QWERTYUIOPASHJKLZXCVBNM"
        For i As Integer = 1 To 5
            randomText.Append(alphabet(r.[Next](alphabet.Length)))
        Next
        Label2.Text = randomText.ToString
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles Button1.Click
        If Label2.Text = TextBox1.Text Then
            MsgBox("OK")
        Else
            MsgBox("Salah Bos!", MsgBoxStyle.Exclamation)
        End If
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        captcha()
    End Sub

End Class