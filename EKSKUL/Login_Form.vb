Public Class Login_Form
    Dim db As New Database
    Public role As String
    Private Sub Login_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        db.conn()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Or TextBox2.Text = "" Then
            MsgBox("All data must be fill", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        db.login(TextBox1.Text, TextBox2.Text)
        If role = "admin" Then
            Admin_Nav.Show()
            Me.Close()
        ElseIf role = "guru"
            Guru_Nav.guruid = TextBox1.text
            Guru_Nav.Show()
            Me.Close()
        ElseIf role = "siswa"
            Siswa_Nav.idsiswa = TextBox1.text
            Siswa_Nav.Show()
            Me.Close()
        End If
    End Sub
End Class
