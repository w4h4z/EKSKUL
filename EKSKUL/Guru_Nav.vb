Imports System.Data.SqlClient
Imports System.IO
Imports System.Text

Public Class Guru_Nav
    Dim dbcomm As New SqlCommand
    Dim sql As String
    Dim dbread As SqlDataReader
    Dim db As New Database
    Public guruid As String
    Private Sub Guru_Nav_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'EKSKULDataSet7.Soal' table. You can move, or remove it, as needed.
        Me.SoalTableAdapter.Fill(Me.EKSKULDataSet7.Soal)
        'TODO: This line of code loads data into the 'EKSKULDataSet6.NilaiEkskul' table. You can move, or remove it, as needed.
        Me.NilaiEkskulTableAdapter.Fill(Me.EKSKULDataSet6.NilaiEkskul)
        'TODO: This line of code loads data into the 'EKSKULDataSet5.Siswa' table. You can move, or remove it, as needed.
        Me.SiswaTableAdapter.Fill(Me.EKSKULDataSet5.Siswa)
        TabControl1.ItemSize = New Size(0, 1)
        TabControl1.SizeMode = TabSizeMode.Fixed
        db.conn()
        pembina()
        anggotaEkskul()
        nilaiAnggota()
        dataNilai()
        Nilai()
        PictureBox1.Image = My.Resources.profil
        akhir()
    End Sub

    Public Sub anggotaEkskul()
        sql = "select * from Siswa as s join Ekskul as e on s.id_ekskul=e.id_ekskul where e.nama_ekskul='" & Label5.Text & "'"

        Try
            dbcomm = New SqlCommand(sql, db.conn)
            dbread = dbcomm.ExecuteReader
            While dbread.Read
                DataGridView1.Rows.Add(dbread("id_siswa"), dbread("nama_siswa"), dbread("tgl_lahir_siswa").toshortdatestring, dbread("telp_siswa"), dbread("jenis_kelamin"), dbread("foto_siswa"))
            End While
            dbread.Close()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try

        'dbread = db.crud(sql)

        'While dbread.Read
        '    DataGridView1.Rows.Add(dbread("id_siswa"), dbread("nama_siswa"), dbread("tgl_lahir_siswa").toshortdatestring, dbread("telp_siswa"), dbread("jenis_kelamin"), dbread("foto_siswa"))
        'End While
        'dbread.Close()
    End Sub

    Private Sub akhir()
        For i As Integer = 0 To DataGridView2.RowCount - 1
            Dim nilaiMid As Integer = DataGridView2.Item(3, i).Value
            Dim nilaiFinal As Integer = DataGridView2.Item(4, i).Value
            Dim nilaiAkhir As Double = (nilaiMid + nilaiFinal) / 2
            DataGridView2.Item(5, i).Value = nilaiAkhir
        Next
    End Sub

    Public Sub nilaiAnggota()
        sql = "select * from Siswa as s join Ekskul as e on s.id_ekskul=e.id_ekskul where e.nama_ekskul='" & Label5.Text & "'"

        Try
            dbcomm = New SqlCommand(sql, db.conn)
            dbread = dbcomm.ExecuteReader
            cbSiswa.ValueMember = "Value"
            cbSiswa.DisplayMember = "Text"
            While dbread.Read
                cbSiswa.Items.Add(New With {Key .Text = dbread("nama_siswa"), Key .Value = dbread("id_siswa")})
            End While
            dbread.Close()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Public Sub pembina()
        sql = "select * from Ekskul where id_guru='" & guruid & "'"

        Try
            dbcomm = New SqlCommand(sql, db.conn)
            dbread = dbcomm.ExecuteReader
            While dbread.Read
                Label5.Text = dbread("nama_ekskul")
            End While
            dbread.Close()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Public Sub dataNilai()
        DataGridView5.Rows.Clear()
        sql = "select * from NilaiEkskul as ne join Siswa as s on ne.id_siswa=s.id_siswa join Guru as g on ne.id_guru=g.id_guru"

        Try
            dbcomm = New SqlCommand(sql, db.conn)
            dbread = dbcomm.ExecuteReader
            While dbread.Read
                DataGridView5.Rows.Add(dbread("id_nilai"), dbread("id_siswa"), dbread("nama_siswa"), dbread("jenis_kelamin"), dbread("tgl_lahir_siswa").toshortdatestring)
            End While
            dbread.Close()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub DataGridView1_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseClick
        Dim i As Integer = DataGridView1.CurrentRow.Index

        Try
            Dim gambar As Byte() = DataGridView1.Item(5, i).Value
            Dim blobGambar As New MemoryStream(gambar)
            PictureBox1.Image = Image.FromStream(blobGambar)
            blobGambar.Dispose()
        Catch ex As Exception
            PictureBox1.Image = My.Resources.profil
        End Try
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Login_Form.Show()
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        TabControl1.SelectTab(0)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        TabControl1.SelectTab(1)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        TabControl1.SelectTab(2)
    End Sub

    Private Sub Button30_Click(sender As Object, e As EventArgs) Handles Button30.Click
        OpenFileDialog1.Reset()
        OpenFileDialog1.Filter = "DOCX Files(*.docx)|*.docx|PDF Files(*.pdf)|*.pdf"
        OpenFileDialog1.ShowDialog()
        If OpenFileDialog1.FileName = "" Then
            MsgBox("Empty", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        txtMid.Text = OpenFileDialog1.FileName
    End Sub

    Private Sub Button31_Click(sender As Object, e As EventArgs) Handles Button31.Click
        OpenFileDialog2.Reset()
        OpenFileDialog2.Filter = "DOCX Files(*.docx)|*.docx|PDF Files(*.pdf)|*.pdf"
        OpenFileDialog2.ShowDialog()
        If OpenFileDialog2.FileName = "" Then
            MsgBox("Empty", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        txtFinal.Text = OpenFileDialog2.FileName
    End Sub

    Private Sub Button28_Click(sender As Object, e As EventArgs) Handles Button28.Click
        If cbSiswa.Text = "Select" Then
            MsgBox("Select student fisrt!", MsgBoxStyle.Exclamation)
        End If
        If txtFinal.Text = "" And txtMid.Text = "" Then
            MsgBox("Data empty!", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        If txtMid.Text = "" And Not txtFinal.Text = "" Then
            MsgBox("Final exam can't upload before mid exam!", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        insertSoal(txtMid.Text)

        If Not txtFinal.Text = "" Then
            insertSoalFinal(txtFinal.Text)
        End If

        MsgBox("Insert data success", MsgBoxStyle.Information)
        dataNilai()
    End Sub

    Private Sub insertSoal(MidExam As String)
        Dim msMid As MemoryStream = New MemoryStream()
        Dim fsMid As FileStream = New FileStream(MidExam, FileMode.Open, FileAccess.Read)

        fsMid.CopyTo(msMid)

        db.insertSoal(cbSiswa.SelectedItem.Value, guruid, msMid.ToArray())

        fsMid.Close()
        msMid.Close()
    End Sub

    Private Sub insertSoalFinal(FinalExam As String)
        Dim msFinal As MemoryStream = New MemoryStream()
        Dim fsFinal As FileStream = New FileStream(FinalExam, FileMode.Open, FileAccess.Read)

        fsFinal.CopyTo(msFinal)

        db.insertSoalFinal(cbSiswa.SelectedItem.Value, msFinal.ToArray)

        fsFinal.Close()
        msFinal.Close()
    End Sub

    Private Sub DataGridView5_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView5.CellClick
        Dim i As Integer = DataGridView5.CurrentRow.Index

        If e.ColumnIndex = 5 Then
            getMid()
        ElseIf e.ColumnIndex = 6
            getFinal()
        End If
    End Sub

    Private Sub getMid()
        Dim i As Integer = DataGridView5.CurrentRow.Index
        SaveFileDialog1.Reset()
        If SaveFileDialog1.ShowDialog = DialogResult.OK Then
            Dim SaveLocation As String = SaveFileDialog1.FileName
            dbcomm = New SqlCommand("select soal_mid from NilaiEkskul where id_nilai='" & DataGridView5.Item(0, i).Value & "'", db.conn)
            dbread = dbcomm.ExecuteReader
            If dbread.HasRows Then

                Do While dbread.Read
                    Dim x As Byte() = CType(dbread("soal_mid"), Byte()).ToArray()
                    Dim fs As New FileStream(SaveLocation, FileMode.OpenOrCreate, FileAccess.ReadWrite)
                    fs.Write(x, 0, x.Length)
                    fs.Close()
                    MsgBox("Download success", MsgBoxStyle.Information)
                    Process.Start(SaveLocation)
                    Exit Do
                Loop
            Else
                MsgBox("Soal belum diupload", MsgBoxStyle.Exclamation)
            End If
            dbread.Close()
        End If
    End Sub

    Private Sub getFinal()
        Dim i As Integer = DataGridView5.CurrentRow.Index
        SaveFileDialog1.Reset()
        If SaveFileDialog1.ShowDialog = DialogResult.OK Then
            Dim SaveLocation As String = SaveFileDialog1.FileName
            dbcomm = New SqlCommand("select soal_final from NilaiEkskul where id_nilai='" & DataGridView5.Item(0, i).Value & "'", db.conn)
            dbread = dbcomm.ExecuteReader
            If dbread.HasRows Then

                Do While dbread.Read
                    Dim x As Byte() = CType(dbread("soal_final"), Byte()).ToArray()
                    Dim fs As New FileStream(SaveLocation, FileMode.OpenOrCreate, FileAccess.ReadWrite)
                    fs.Write(x, 0, x.Length)
                    fs.Close()
                    MsgBox("Download success", MsgBoxStyle.Information)
                    Process.Start(SaveLocation)
                    Exit Do
                Loop
            Else
                MsgBox("Soal belum diupload", MsgBoxStyle.Exclamation)
            End If
            dbread.Close()
        End If
    End Sub

    'Private Function UploadGambar(Tabel As String, Kolom As String, Path As String) As Integer
    '    Dim ms As MemoryStream = New MemoryStream()
    '    Image.FromFile(Path).Save(ms, Imaging.ImageFormat.Png)
    '    Dim upload As New SqlCommand(String.Format("INSERT INTO [{0}]({1}) OUTPUT INSERTED.id VALUES(@Gambar)", Tabel, Kolom), ServerConnection)
    '    upload.Parameters.Add("@Gambar", SqlDbType.Binary, ms.Length).Value = ms.ToArray()
    '    Dim ret As Integer = upload.ExecuteScalar()
    '    Return ret
    'End Function

    'Private Function UploadGambar(Tabel As String, Kolom As String, KolomID As String, ID As String, Path As String) As Integer
    '    Dim ms As MemoryStream = New MemoryStream()
    '    Image.FromFile(Path).Save(ms, Imaging.ImageFormat.Png)
    '    Dim upload As New SqlCommand(String.Format("UPDATE [{0}] SET {1} = @Gambar WHERE {2} = {3}", Tabel, Kolom, KolomID, Kolom), ServerConnection)
    '    upload.Parameters.Add("@Gambar", SqlDbType.Binary, ms.Length).Value = ms.ToArray()
    '    Dim ret As Integer = upload.ExecuteNonQuery()
    '    Return ret
    'End Function

    Private Sub Nilai()
        sql = "select * from NilaiEkskul as ne join Siswa as s on ne.id_siswa=s.id_siswa join Guru as g on ne.id_guru=g.id_guru"

        Try
            dbcomm = New SqlCommand(sql, db.conn)
            dbread = dbcomm.ExecuteReader
            While dbread.Read
                DataGridView2.Rows.Add(dbread("id_nilai"), dbread("id_siswa"), dbread("nama_siswa"), dbread("nilai_mid"), dbread("nilai_final"))
            End While
            dbread.Close()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try
    End Sub

    Private Sub DataGridView2_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellEndEdit
        Dim i As Integer = DataGridView2.CurrentRow.Index
        db.nilaiMid(DataGridView2.Item(3, i).Value, DataGridView2.Item(0, i).Value)
        Try
            db.nilaifinal(DataGridView2.Item(4, i).Value, DataGridView2.Item(0, i).Value)
        Catch ex As Exception

        End Try

        nilaiAkhir()
    End Sub

    Private Sub nilaiAkhir()
        Dim i As Integer = DataGridView2.CurrentRow.Index
        DataGridView2.Item(5, i).Value = ""
        Dim nilaiMid As Integer = DataGridView2.Item(3, i).Value
        Dim nilaiFinal As Integer = DataGridView2.Item(4, i).Value
        Dim nilaiAkhir As Double = (nilaiMid + nilaiFinal) / 2
        DataGridView2.Item(5, i).Value = nilaiAkhir
    End Sub

    Private Sub DataGridView2_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellClick
        Dim i As Integer = DataGridView2.CurrentRow.Index

        If e.ColumnIndex = 6 Then
            gMid()
        ElseIf e.ColumnIndex = 7
            gFinal()
        End If
    End Sub

    Private Sub gMid()
        Dim i As Integer = DataGridView2.CurrentRow.Index
        SaveFileDialog1.Reset()
        If SaveFileDialog1.ShowDialog = DialogResult.OK Then
            Dim SaveLocation As String = SaveFileDialog1.FileName
            dbcomm = New SqlCommand("select jawaban_mid from Jawaban as j join NilaiEkskul as ne on j.id_siswa=ne.id_siswa where id_nilai='" & DataGridView2.Item(0, i).Value & "'", db.conn)
            dbread = dbcomm.ExecuteReader
            If dbread.HasRows() Then
                Do While dbread.Read
                    Dim x As Byte() = CType(dbread("jawaban_mid"), Byte()).ToArray()
                    Dim fs As New FileStream(SaveLocation, FileMode.OpenOrCreate, FileAccess.ReadWrite)
                    fs.Write(x, 0, x.Length)
                    fs.Close()
                    MsgBox("Download success", MsgBoxStyle.Information)
                    Process.Start(SaveLocation)
                    Exit Do
                Loop
            Else
                MsgBox("Jawaban belum di upload", MsgBoxStyle.Exclamation)
            End If
            dbread.Close()
        End If
    End Sub

    Private Sub gFinal()
        Dim i As Integer = DataGridView2.CurrentRow.Index
        SaveFileDialog1.Reset()
        If SaveFileDialog1.ShowDialog = DialogResult.OK Then
            Dim SaveLocation As String = SaveFileDialog1.FileName
            dbcomm = New SqlCommand("select jawaban_final from Jawaban as j join NilaiEkskul as ne on j.id_siswa=ne.id_siswa where id_nilai='" & DataGridView2.Item(0, i).Value & "'", db.conn)
            dbread = dbcomm.ExecuteReader
            If dbread.HasRows() Then

                Do While dbread.Read
                    Dim x As Byte() = CType(dbread("jawaban_final"), Byte()).ToArray()
                    Dim fs As New FileStream(SaveLocation, FileMode.OpenOrCreate, FileAccess.ReadWrite)
                    fs.Write(x, 0, x.Length)
                    fs.Close()
                    MsgBox("Download success", MsgBoxStyle.Information)
                    Process.Start(SaveLocation)
                    Exit Do
                Loop
            Else
                MsgBox("Jawaban belum diupload", MsgBoxStyle.Exclamation)
            End If
            dbread.Close()
        End If
    End Sub

    Private Sub Button27_Click(sender As Object, e As EventArgs) Handles Button27.Click
        Dim i As Integer = DataGridView5.CurrentRow.Index
        db.deleteSoal(DataGridView5.Item(0, i).Value)
        dataNilai()
    End Sub

    Private Sub Button29_Click(sender As Object, e As EventArgs) Handles Button29.Click
        If txtFinal.Text = "" And txtMid.Text = "" Then
            MsgBox("Data Empty", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        Dim i As Integer = DataGridView5.CurrentRow.Index

        If Not txtMid.Text = "" Then
            Dim locMid As String = txtMid.Text
            Dim msMid As New MemoryStream
            Dim fsMid As FileStream = New FileStream(locMid, FileMode.Open, FileAccess.Read)
            fsMid.CopyTo(msMid)
            db.editSoalMid(msMid.ToArray, DataGridView5.Item(0, i).Value)
            fsMid.Close()
            msMid.Close()
        End If

        If Not txtFinal.Text = "" Then
            Dim locFinal As String = txtFinal.Text
            Dim msFinal As New MemoryStream
            Dim fsFinal As FileStream = New FileStream(locFinal, FileMode.Open, FileAccess.Read)
            fsFinal.CopyTo(msFinal)
            db.editSoalFinal(msFinal.ToArray, DataGridView5.Item(0, i).Value)
            fsFinal.Close()
            msFinal.Close()
        End If

        MsgBox("Edit data success", MsgBoxStyle.Information)
        dataNilai()
    End Sub

    Private Sub DataGridView5_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView5.CellMouseClick
        Dim i As Integer = DataGridView5.CurrentRow.Index

        cbSiswa.Text = DataGridView5.Item(2, i).Value
    End Sub


    'Private Sub DataGridView2_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView2.CellMouseClick
    '    Dim i As Integer = DataGridView2.CurrentRow.Index
    '    If DataGridView2.Item(3, i).Value Is  Or DataGridView2.Item(4, i).Value = "" Then
    '        TextBox2.Text = ""
    '        TextBox1.Text = ""
    '        Exit Sub
    '    End If
    '    TextBox2.Text = DataGridView2.Item(3, i).Value
    '    TextBox1.Text = DataGridView2.Item(4, i).Value
    'End Sub

    'Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
    '    Dim i As Integer = DataGridView2.CurrentRow.Index
    '    db.nilaiMid(TextBox2.Text, DataGridView2.Item(1, i).Value)
    '    Nilai()
    'End Sub

    'Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
    '    Dim i As Integer = DataGridView2.CurrentRow.Index
    '    db.nilaiMid(TextBox1.Text, DataGridView2.Item(1, i).Value)
    '    Nilai()
    'End Sub

End Class