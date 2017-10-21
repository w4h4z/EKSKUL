Imports System.Data.SqlClient
Imports System.IO
Public Class Siswa_Nav
    Dim db As New Database
    Dim sql As String
    Dim dbcomm As New SqlCommand
    Dim dbread As SqlDataReader
    Public idsiswa As String
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        TabControl1.SelectTab(0)
    End Sub

    Private Sub Siswa_Nav_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TabControl1.ItemSize = New Size(0, 1)
        TabControl1.SizeMode = TabSizeMode.Fixed
        TabControl1.SelectTab(1)
        db.conn()
        Ekskul()
        jadwalEkskul()
    End Sub

    Private Sub jadwalEkskul()
        sql = "select * from JadwalEkskul As je join Ekskul As e On je.id_ekskul=e.id_ekskul join Guru As g On e.id_guru=g.id_guru where e.nama_ekskul='" & Label5.Text & "'"

        Try
            dbcomm = New SqlCommand(sql, db.conn)
            dbread = dbcomm.ExecuteReader
            While dbread.Read()
                DataGridView1.Rows.Add(dbread("id_ekskul"), dbread("nama_ekskul"), dbread("hari_ekskul"), dbread("jam_ekskul"), dbread("nama_guru"))
            End While
            dbread.Close()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub Ekskul()
        sql = "Select * from Ekskul As e join Siswa As s On e.id_ekskul=s.id_ekskul where id_siswa='" & idsiswa & "'"
        Try
            dbcomm = New SqlCommand(sql, db.conn)
            dbread = dbcomm.ExecuteReader
            dbread.Read()
            If dbread.HasRows Then
                Label5.Text = dbread("nama_ekskul")
            Else
                Label5.Text = "Belum Memilih"
            End If
            dbread.Close()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Dim fileName As String
        SaveFileDialog1.Reset()
        If SaveFileDialog1.ShowDialog = DialogResult.OK Then
            fileName = SaveFileDialog1.FileName

            sql = "select soal_mid from NilaiEkskul as ne join Siswa as s on ne.id_siswa=s.id_siswa join Guru as g on ne.id_guru=g.id_guru where ne.id_siswa='" & idsiswa & "'"

            Try
                dbcomm = New SqlCommand(sql, db.conn)
                dbread = dbcomm.ExecuteReader
                dbread.Read()
                If dbread.HasRows Then
                    Dim x As Byte() = CType(dbread("soal_mid"), Byte()).ToArray
                    Dim fs As New FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite)
                    fs.Write(x, 0, x.Length)
                    fs.Close()
                    MsgBox("Download success", MsgBoxStyle.Information)
                    Process.Start(fileName)
                Else
                    MsgBox("Soal belum diupload", MsgBoxStyle.Exclamation)
                End If
                dbread.Close()
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical)
                dbread.Close()
            End Try
        End If
    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        Dim fileName As String
        SaveFileDialog1.Reset()
        If SaveFileDialog1.ShowDialog = DialogResult.OK Then
            fileName = SaveFileDialog1.FileName

            sql = "select soal_final from NilaiEkskul as ne join Siswa as s on ne.id_siswa=s.id_siswa join Guru as g on ne.id_guru=g.id_guru where ne.id_siswa='" & idsiswa & "'"

            Try
                dbcomm = New SqlCommand(sql, db.conn)
                dbread = dbcomm.ExecuteReader
                dbread.Read()
                If dbread.HasRows Then
                    Dim x As Byte() = CType(dbread("soal_final"), Byte()).ToArray
                    Dim fs As New FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite)
                    fs.Write(x, 0, x.Length)
                    fs.Close()
                    MsgBox("Download success", MsgBoxStyle.Information)
                    Process.Start(fileName)
                Else
                    MsgBox("Soal belum diupload", MsgBoxStyle.Exclamation)
                End If
                dbread.Close()
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Critical)
                dbread.Close()
            End Try
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        OpenFileDialog1.Reset()
        OpenFileDialog1.Filter = "DOCX Files(*.docx)|*.docx|PDF Files(*.pdf)|*.pdf"
        OpenFileDialog1.ShowDialog()
        If OpenFileDialog1.FileName = "" Then
            MsgBox("Empty", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        txtMid.Text = OpenFileDialog1.FileName
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        insertMid(txtMid.Text)
        txtMid.Text = ""
    End Sub

    Private Sub insertMid(MidExam As String)
        Dim msMid As MemoryStream = New MemoryStream()
        Dim fsMid As FileStream = New FileStream(MidExam, FileMode.Open, FileAccess.Read)

        fsMid.CopyTo(msMid)

        db.insertMid(idsiswa, msMid.ToArray())

        fsMid.Close()
        msMid.Close()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        OpenFileDialog1.Reset()
        OpenFileDialog1.Filter = "DOCX Files(*.docx)|*.docx|PDF Files(*.pdf)|*.pdf"
        OpenFileDialog1.ShowDialog()
        If OpenFileDialog1.FileName = "" Then
            MsgBox("Empty", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        txtFinal.Text = OpenFileDialog1.FileName
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        insertFinal(txtFinal.Text)
        txtFinal.Text = ""
    End Sub

    Private Sub insertFinal(FinalExam As String)
        Dim msFinal As MemoryStream = New MemoryStream()
        Dim fsFinal As FileStream = New FileStream(FinalExam, FileMode.Open, FileAccess.Read)

        fsFinal.CopyTo(msFinal)

        db.insertFinal(idsiswa, msFinal.ToArray())

        fsfinal.Close()
        msFinal.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Login_Form.Show()
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        TabControl1.SelectTab(1)
    End Sub
End Class