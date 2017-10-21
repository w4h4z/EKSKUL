Imports System.IO
Imports System.Data.SqlClient
Public Class Admin_Nav
    Dim dbcomm As New SqlCommand
    Dim dbread As SqlDataReader
    Dim sql As String
    Dim db As New Database
    Dim pathFile As String
    Dim destFile As String
    Dim fileName As String
    Private Sub Admin_Nav_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'EKSKULDataSet9.DataEkskul' table. You can move, or remove it, as needed.
        Me.DataEkskulTableAdapter.Fill(Me.EKSKULDataSet9.DataEkskul)
        'TODO: This line of code loads data into the 'EKSKULDataSet8.Jadwal' table. You can move, or remove it, as needed.
        Me.JadwalTableAdapter1.Fill(Me.EKSKULDataSet8.Jadwal)
        'TODO: This line of code loads data into the 'EKSKULDataSet5.Siswa' table. You can move, or remove it, as needed.
        Me.SiswaTableAdapter1.Fill(Me.EKSKULDataSet5.Siswa)
        'TODO: This line of code loads data into the 'EKSKULDataSet4.Ekskul' table. You can move, or remove it, as needed.
        Me.EkskulTableAdapter1.Fill(Me.EKSKULDataSet4.Ekskul)
        'TODO: This line of code loads data into the 'EKSKULDataSet3.Guru' table. You can move, or remove it, as needed.
        Me.GuruTableAdapter1.Fill(Me.EKSKULDataSet3.Guru)
        'TODO: This line of code loads data into the 'EKSKULDataSet2.Jadwal' table. You can move, or remove it, as needed.
        'Me.JadwalTableAdapter.Fill(Me.EKSKULDataSet2.Jadwal)
        'TODO: This line of code loads data into the 'EKSKULDataSet.Ekskul' table. You can move, or remove it, as needed.
        'Me.EkskulTableAdapter.Fill(Me.EKSKULDataSet.Ekskul)
        'TODO: This line of code loads data into the 'EKSKULDataSet1.ViewEkskul' table. You can move, or remove it, as needed.
        'Me.ViewEkskulTableAdapter.Fill(Me.EKSKULDataSet1.ViewEkskul)
        'TODO: This line of code loads data into the 'EKSKULDataSet.Guru' table. You can move, or remove it, as needed.
        'Me.GuruTableAdapter.Fill(Me.EKSKULDataSet.Guru)

        TabControl1.ItemSize = New Size(0, 1)
        TabControl1.SizeMode = TabSizeMode.Fixed
        db.conn()
        siswa()
        siswaNull()
        siswaNotNull()
    End Sub

    Public Sub siswa()
        DataGridView3.Rows.Clear()
        sql = "select * from Siswa"

        Try
            dbcomm = New SqlCommand(sql, db.conn)
            dbread = dbcomm.ExecuteReader
            While dbread.Read()
                DataGridView3.Rows.Add(dbread("id_siswa"), dbread("nama_siswa"), dbread("tgl_lahir_siswa").toshortdatestring, dbread("telp_siswa"), dbread("jenis_kelamin"), dbread("asal_sekolah"), dbread("foto_siswa"))
            End While
            dbread.Close()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
            dbread.Close()
        End Try
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

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        TabControl1.SelectTab(3)
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) 
        TabControl1.SelectTab(4)
    End Sub

    Private Sub Button6_Click_1(sender As Object, e As EventArgs) Handles Button6.Click
        Login_Form.Show()
        Me.Close()
    End Sub

    Private Sub Button32_Click(sender As Object, e As EventArgs) Handles Button32.Click
        TabControl1.SelectTab(5)
    End Sub

    Private Sub btnAddSiswa_Click(sender As Object, e As EventArgs) Handles btnAddSiswa.Click
        Dim jk As String
        Dim b As Boolean
        If rbLkSiswa.Checked = True Then
            jk = "Laki-Laki"
        ElseIf rbPrSiswa.Checked = True
            jk = "Perempuan"
        End If

        If rbLkSiswa.Checked = True Or rbPrSiswa.Checked = True Then
            b = True
        End If

        If txtNamaSiswa.Text = "" Or txtHpSiswa.Text = "" Or txtAsalSiswa.Text = "" Or txtAsalSiswa.Text = "" Or b = False Then
            MsgBox("All data must be fill", MsgBoxStyle.Exclamation)
            Exit Sub
        End If


        db.insertSiswa(txtNamaSiswa.Text, DateTimePickerSiswa.Value.ToString("yyyy/MM/dd"), txtHpSiswa.Text, jk, txtAsalSiswa.Text, txtFotoSiswa.Text)
        Dim id As String = "id_siswa"
        Dim pw As String = "siswa"
        Dim role As String = "siswa"
        db.insertAkun(id, pw, role)

        siswa()
        resetSiswa()
    End Sub

    Private Sub btnUploadSiswa_Click(sender As Object, e As EventArgs) Handles btnUploadSiswa.Click
        OpenFileDialog1.Filter = "JPG Files(*.jpg)|*.jpg|JPEG Files(*.jpeg)|*.jpeg|PNG Files(*.png)|*.png"
        'If OpenFileDialog1.ShowDialog = DialogResult.OK Then
        '    pathFile = OpenFileDialog1.FileName
        '    txtFotoSiswa.Text = pathFile
        '    fileName = pathFile.Substring(pathFile.LastIndexOf("\") + 1)
        '    destFile = "D:\New folder (2)\EKSKUL\EKSKUL\Resources\" + fileName
        '    If File.Exists(destFile) Then
        '        File.Delete(destFile)
        '        File.Copy(pathFile, destFile)
        '    Else
        '        File.Copy(pathFile, destFile)
        '    End If
        '    PictureBoxSiswa.ImageLocation = destFile
        'End If
        OpenFileDialog1.ShowDialog()
        If OpenFileDialog1.FileName = "" Then
            MsgBox("Empty", MsgBoxStyle.Exclamation)
            Exit Sub
        Else
            txtFotoSiswa.Text = OpenFileDialog1.FileName
            PictureBoxSiswa.ImageLocation = OpenFileDialog1.FileName
        End If

    End Sub

    Private Sub txtHpSiswa_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtHpSiswa.KeyPress
        If Asc(e.KeyChar) <> 8 Then
            If Asc(e.KeyChar) < 48 Or Asc(e.KeyChar) > 57 Then
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub DataGridView3_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView3.CellMouseClick
        Dim i As Integer = DataGridView3.CurrentRow.Index

        txtIdSiswa.Text = DataGridView3.Item(0, i).Value
        txtNamaSiswa.Text = DataGridView3.Item(1, i).Value
        DateTimePickerSiswa.Value = DataGridView3.Item(2, i).Value
        txtHpSiswa.Text = DataGridView3.Item(3, i).Value
        If DataGridView3.Item(4, i).Value = "Laki-Laki" Then
            rbLkSiswa.Checked = True
        ElseIf DataGridView3.Item(4, i).Value = "Perempuan" Then
            rbPrSiswa.Checked = True
        End If
        txtAsalSiswa.Text = DataGridView3.Item(5, i).Value

        Try
            Dim gambar As Byte() = DataGridView3.Item(6, i).Value
            Dim blobGambar As New MemoryStream(gambar)
            PictureBoxSiswa.Image = Image.FromStream(blobGambar)
            blobGambar.Dispose()
        Catch ex As Exception
            PictureBoxSiswa.Image = My.Resources.profil
        End Try

    End Sub

    Private Sub btnEditSiswa_Click(sender As Object, e As EventArgs) Handles btnEditSiswa.Click
        Dim jk As String
        Dim b As Boolean
        If rbLkSiswa.Checked = True Then
            jk = "Laki-Laki"
        ElseIf rbPrSiswa.Checked = True
            jk = "Perempuan"
        End If

        If rbLkSiswa.Checked = True Or rbPrSiswa.Checked = True Then
            b = True
        End If

        If txtNamaSiswa.Text = "" Or txtHpSiswa.Text = "" Or txtAsalSiswa.Text = "" Or txtAsalSiswa.Text = "" Or b = False Then
            MsgBox("All data must be fill", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        If txtIdSiswa.Text = "" Then
            MsgBox("Select data first", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        pathFile = txtFotoSiswa.Text
        fileName = pathFile.Substring(pathFile.LastIndexOf("\") + 1)
        destFile = "D:\New folder (2)\EKSKUL\EKSKUL\Resources\" + fileName
        db.updateSiswa(txtNamaSiswa.Text, DateTimePickerSiswa.Value.ToString("yyyy/MM/dd"), txtHpSiswa.Text, jk, txtAsalSiswa.Text, destFile, txtIdSiswa.Text)

        siswa()
        resetSiswa()
    End Sub

    Private Sub btnHapusSiswa_Click(sender As Object, e As EventArgs) Handles btnHapusSiswa.Click
        If txtIdSiswa.Text = "" Then
            MsgBox("Select data first", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        Dim id As String = "id_siswa"
        db.deleteAkun(id, txtIdSiswa.Text)

        db.deleteSiswa(txtIdSiswa.Text)
        siswa()
        resetSiswa()
    End Sub

    Private Sub btnCancelSiswa_Click(sender As Object, e As EventArgs) Handles btnCancelSiswa.Click
        resetSiswa()
    End Sub

    Public Sub resetSiswa()
        txtIdSiswa.Text = ""
        txtNamaSiswa.Text = ""
        DateTimePickerSiswa.Value = Date.Now
        txtHpSiswa.Text = ""
        rbLkSiswa.Checked = False
        rbPrSiswa.Checked = False
        txtAsalSiswa.Text = ""
        txtFotoSiswa.Text = ""
    End Sub

    Public Sub resetGuru()
        txtIdGuru.Text = ""
        txtNamaGuru.Text = ""
        txtFotoGuru.Text = ""
    End Sub

    Private Sub btnAddGuru_Click(sender As Object, e As EventArgs) Handles btnAddGuru.Click
        If txtNamaGuru.Text = "" Or txtFotoGuru.Text = "" Then
            MsgBox("All data must be fill", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        db.insertGuru(txtNamaGuru.Text, txtFotoGuru.Text)
        Dim id As String = "id_guru"
        Dim pw As String = "guru"
        Dim role As String = "guru"
        db.insertAkun(id, pw, role)

        Me.GuruTableAdapter1.Fill(Me.EKSKULDataSet3.Guru)
        resetGuru()
    End Sub

    Private Sub btnUploadGuru_Click(sender As Object, e As EventArgs) Handles btnUploadGuru.Click
        OpenFileDialog2.Filter = "JPG Files(*.jpg)|*.jpg|JPEG Files(*.jpeg)|*.jpeg|PNG Files(*.png)|*.png"
        'If OpenFileDialog2.ShowDialog = DialogResult.OK Then
        '    pathFile = OpenFileDialog2.FileName
        '    txtFotoGuru.Text = pathFile
        '    fileName = pathFile.Substring(pathFile.LastIndexOf("\") + 1)
        '    destFile = "D:\New folder (2)\EKSKUL\EKSKUL\Resources\" + fileName
        '    If File.Exists(destFile) Then
        '        File.Delete(destFile)
        '        File.Copy(pathFile, destFile)
        '    Else
        '        File.Copy(pathFile, destFile)
        '    End If
        '    PictureBoxGuru.ImageLocation = destFile
        'End If
        OpenFileDialog2.ShowDialog()
        If OpenFileDialog2.FileName = "" Then
            MsgBox("Empty", MsgBoxStyle.Exclamation)
            Exit Sub
        Else
            txtFotoGuru.Text = OpenFileDialog2.FileName
            PictureBoxGuru.ImageLocation = txtFotoGuru.Text
        End If
    End Sub

    Private Sub btnCancelGuru_Click(sender As Object, e As EventArgs) Handles btnCancelGuru.Click
        resetGuru()
    End Sub

    Private Sub DataGridView4_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView4.CellMouseClick
        Dim i As Integer = DataGridView4.CurrentRow.Index

        txtIdGuru.Text = DataGridView4.Item(0, i).Value
        txtNamaGuru.Text = DataGridView4.Item(1, i).Value
        Try
            Dim gambar As Byte() = DataGridView4.Item(2, i).Value
            Dim blobGambar As New MemoryStream(gambar)
            PictureBoxGuru.Image = Image.FromStream(blobGambar)
            blobGambar.Dispose()
        Catch ex As Exception
            PictureBoxGuru.Image = My.Resources.profil
        End Try
    End Sub

    Private Sub btnEditGuru_Click(sender As Object, e As EventArgs) Handles btnEditGuru.Click
        If txtNamaGuru.Text = "" Then
            MsgBox("All data must be fill", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        If txtIdGuru.Text = "" Then
            MsgBox("Select data first", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        'pathFile = txtFotoGuru.Text
        'fileName = pathFile.Substring(pathFile.LastIndexOf("\") + 1)
        'destFile = "D:\New folder (2)\EKSKUL\EKSKUL\Resources\" + fileName
        db.updateGuru(txtNamaGuru.Text, txtFotoGuru.Text, txtIdGuru.Text)

        Me.GuruTableAdapter1.Fill(Me.EKSKULDataSet3.Guru)
        resetGuru()
    End Sub

    Private Sub btnDeleteGuru_Click(sender As Object, e As EventArgs) Handles btnDeleteGuru.Click
        If txtIdSiswa.Text = "" Then
            MsgBox("Select data first", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        Dim id As String = "id_guru"
        db.deleteAkun(id, txtIdGuru.Text)

        db.deleteGuru(txtIdGuru.Text)
        Me.GuruTableAdapter1.Fill(Me.EKSKULDataSet3.Guru)
        resetGuru()
    End Sub

    Public Sub resetEkskul()
        txtIdEkskul.Text = ""
        txtNamaEkskul.Text = ""
        txtFotoEkskul.Text = ""
    End Sub

    Private Sub btnAddEkskul_Click(sender As Object, e As EventArgs) Handles btnAddEkskul.Click
        If txtNamaEkskul.Text = "" Or txtFotoEkskul.Text = "" Then
            MsgBox("All data must be fill", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        For i = 0 To DataGridView2.RowCount - 1
            If (DataGridView2.Item(1, i).Value = cbGuruEkskul.SelectedValue) Then
                MsgBox("Guru tidak boleh membimbing lebih dari 1 ekskul", MsgBoxStyle.Exclamation)
                Exit Sub
            End If
        Next

        db.insertEkskul(cbGuruEkskul.SelectedValue, txtNamaEkskul.Text, txtFotoEkskul.Text)
        'Me.ViewEkskulTableAdapter.Fill(Me.EKSKULDataSet1.ViewEkskul)
        'Me.EkskulTableAdapter1.Fill(Me.EKSKULDataSet4.Ekskul)
        Me.DataEkskulTableAdapter.Fill(Me.EKSKULDataSet9.DataEkskul)
        resetEkskul()
    End Sub

    Private Sub btnUploadEkskul_Click(sender As Object, e As EventArgs) Handles btnUploadEkskul.Click
        OpenFileDialog3.Reset()
        OpenFileDialog3.Filter = "JPG Files(*.jpg)|*.jpg|JPEG Files(*.jpeg)|*.jpeg|PNG Files(*.png)|*.png"

        OpenFileDialog3.ShowDialog()
        If OpenFileDialog3.FileName = "" Then
            MsgBox("Empty", MsgBoxStyle.Exclamation)
            PictureBoxEkskul.Image = My.Resources.profil
            Exit Sub
        Else
            txtFotoEkskul.Text = OpenFileDialog3.FileName
            PictureBoxEkskul.ImageLocation = txtFotoEkskul.Text
        End If
    End Sub

    Private Sub btnCancelEkskul_Click(sender As Object, e As EventArgs) Handles btnCancelEkskul.Click
        resetEkskul()
    End Sub

    Private Sub btnEditEkskul_Click(sender As Object, e As EventArgs) Handles btnEditEkskul.Click
        If txtIdEkskul.Text = "" Then
            MsgBox("Select data first", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        If txtNamaEkskul.Text = "" Then
            MsgBox("All data must be fill", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        pathFile = txtFotoEkskul.Text
        fileName = pathFile.Substring(pathFile.LastIndexOf("\") + 1)
        destFile = "D:\New folder (2)\EKSKUL\EKSKUL\Resources\" + fileName

        db.updateEkskul(cbGuruEkskul.SelectedValue, txtNamaEkskul.Text, txtFotoEkskul.Text, txtIdEkskul.Text)
        'Me.EkskulTableAdapter1.Fill(Me.EKSKULDataSet4.Ekskul)
        Me.DataEkskulTableAdapter.Fill(Me.EKSKULDataSet9.DataEkskul)
        resetEkskul()
    End Sub

    Private Sub DataGridView2_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView2.CellMouseClick
        Dim i As Integer = DataGridView2.CurrentRow.Index

        txtIdEkskul.Text = DataGridView2.Item(0, i).Value
        cbGuruEkskul.SelectedValue = DataGridView2.Item(1, i).Value
        txtNamaEkskul.Text = DataGridView2.Item(2, i).Value

        Try
            Dim gambar As Byte() = DataGridView2.Item(3, i).Value
            Dim blobGambar As New MemoryStream(gambar)
            PictureBoxEkskul.Image = Image.FromStream(blobGambar)
            blobGambar.Dispose()
        Catch ex As Exception
            PictureBoxEkskul.Image = My.Resources.profil
        End Try
    End Sub

    Private Sub btnDeleteEkskul_Click(sender As Object, e As EventArgs) Handles btnDeleteEkskul.Click
        If txtIdEkskul.Text = "" Then
            MsgBox("Select data first!", MsgBoxStyle.Exclamation)
            Exit Sub
        End If
        db.deleteEkskul(txtIdEkskul.Text)

        'Me.ViewEkskulTableAdapter.Fill(Me.EKSKULDataSet1.ViewEkskul)
        'Me.EkskulTableAdapter1.Fill(Me.EKSKULDataSet4.Ekskul)
        Me.DataEkskulTableAdapter.Fill(Me.EKSKULDataSet9.DataEkskul)
        resetEkskul()
    End Sub

    Public Sub siswaNull()
        DataGridView6.Rows.Clear()
        sql = "select * from Siswa where id_ekskul is NULL"

        Try
            dbcomm = New SqlCommand(sql, db.conn)
            dbread = dbcomm.ExecuteReader
            While dbread.Read
                DataGridView6.Rows.Add(dbread("id_siswa"), dbread("nama_siswa"))
            End While
            dbread.Close()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
            dbread.Close()
        End Try
    End Sub

    Public Sub siswaNotNull()
        DataGridView7.Rows.Clear()
        sql = "select * from Siswa where id_ekskul ='" & ComboBox5.SelectedValue & "'"

        Try
            dbcomm = New SqlCommand(sql, db.conn)
            dbread = dbcomm.ExecuteReader
            While dbread.Read
                DataGridView7.Rows.Add(dbread("id_siswa"), dbread("nama_siswa"))
            End While
            dbread.Close()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
            dbread.Close()
        End Try
    End Sub

    Private Sub ComboBox5_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox5.SelectedIndexChanged
        siswaNotNull()
    End Sub

    Private Sub Button33_Click(sender As Object, e As EventArgs) Handles Button33.Click
        Dim i As Integer = DataGridView6.CurrentRow.Index
        db.siswaToEkskul(ComboBox5.SelectedValue, DataGridView6.Item(0, i).Value)
        siswaNotNull()
        siswaNull()
    End Sub

    Private Sub Button34_Click(sender As Object, e As EventArgs) Handles Button34.Click
        Dim i As Integer = DataGridView6.CurrentRow.Index
        db.ekskulToSiswa(DataGridView7.Item(0, i).Value)
        siswaNotNull()
        siswaNull()
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles btnAddJadwal.Click
        If cbHariJadwal.Text = "Pilih" Or txtJamJadwal.Text = "" Then
            MsgBox("All data must be fill", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        db.insertJadwal(cbEkskulJadwal.SelectedValue, cbHariJadwal.Text, txtJamJadwal.Text)
        Me.JadwalTableAdapter1.Fill(Me.EKSKULDataSet8.Jadwal)
        resetJadwal()
    End Sub

    Private Sub btnEditJadwal_Click(sender As Object, e As EventArgs) Handles btnEditJadwal.Click
        If cbHariJadwal.Text = "Pilih" Or txtJamJadwal.Text = "" Then
            MsgBox("All data must be fill", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        If txtIdJadwal.Text = "" Then
            MsgBox("Select data first!", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        db.updateJadwal(cbEkskulJadwal.SelectedValue, cbHariJadwal.Text, txtJamJadwal.Text, txtIdJadwal.Text)
        Me.JadwalTableAdapter1.Fill(Me.EKSKULDataSet8.Jadwal)
        resetJadwal()
    End Sub

    Private Sub btnDeleteJadwal_Click(sender As Object, e As EventArgs) Handles btnDeleteJadwal.Click
        If txtIdJadwal.Text = "" Then
            MsgBox("Select data first!", MsgBoxStyle.Exclamation)
            Exit Sub
        End If

        db.deleteJadwal(txtIdJadwal.Text)
        Me.JadwalTableAdapter1.Fill(Me.EKSKULDataSet8.Jadwal)
        resetJadwal()
    End Sub

    Private Sub btnCancelJadwal_Click(sender As Object, e As EventArgs) Handles btnCancelJadwal.Click
        resetJadwal()
    End Sub

    Private Sub DataGridView1_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseClick
        Dim i As Integer = DataGridView1.CurrentRow.Index

        txtIdJadwal.Text = DataGridView1.Item(0, i).Value
        cbEkskulJadwal.SelectedValue = DataGridView1.Item(1, i).Value
        cbHariJadwal.Text = DataGridView1.Item(3, i).Value
        txtJamJadwal.Text = DataGridView1.Item(4, i).Value
    End Sub

    Public Sub resetJadwal()
        txtIdJadwal.Text = "" '
        cbEkskulJadwal.SelectedIndex = 0
        cbHariJadwal.SelectedIndex = -1
        cbHariJadwal.Text = "Pilih"
        txtJamJadwal.Text = ""
    End Sub

End Class