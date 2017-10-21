Imports System.Data.SqlClient
Imports System.IO

Public Class Database
    Dim dbconn As New SqlConnection
    Dim dbcomm As New SqlCommand
    Dim dbread As SqlDataReader
    Dim sql As String
    Dim lastid As String

    Public Function conn()
        dbconn = New SqlConnection("data source=.\SQLEXPRESS;database=EKSKUL;integrated security=true")

        Try
            dbconn.Open()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try

        Return dbconn
    End Function

    Public Function crud(query As String)
        Try
            dbcomm = New SqlCommand(query, conn)
            dbread = dbcomm.ExecuteReader
            dbread.Read()
            Return dbread
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
            dbread.Close()
        End Try
    End Function

    Public Function crud(sqlCommand As SqlCommand)
        Try
            dbread = sqlCommand.ExecuteReader
            dbread.Read()
            Return dbread
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
            dbread.Close()
        End Try
    End Function

    Public Function crudid(sql)
        Try
            dbcomm = New SqlCommand(sql, conn)
            lastid = dbcomm.ExecuteScalar
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical)
        End Try

        Return lastid
    End Function

    Public Sub login(uname, pw)
        sql = "select * from UserAccount where username='" & uname & "' and password='" & pw & "'"

        crud(sql)
        If dbread.HasRows Then
            Login_Form.role = dbread("Role")
        Else
            MsgBox("Login Failed, check your username and password", MsgBoxStyle.Exclamation)
        End If
        dbread.Close()
    End Sub

    Public Sub insertAkun(id, pw, role)
        sql = "insert into UserAccount(" & id & ",username,password,role) values('" & lastid & "','" & lastid & "','" & pw & "','" & role & "')"

        crud(sql)
        MsgBox("Insert data success", MsgBoxStyle.Information)
        dbread.Close()
    End Sub

    Public Sub deleteAkun(id, val_id)
        sql = "delete UserAccount where " & id & "=" & val_id & ""

        crud(sql)
        MsgBox("Delete data success", MsgBoxStyle.Information)
        dbread.Close()
    End Sub

    Public Sub insertSiswa(nama, tgl, hp, jk, asal, foto)
        sql = "insert into Siswa(nama_siswa,tgl_lahir_siswa,telp_siswa,jenis_kelamin,asal_sekolah) output inserted.id_siswa values('" & nama & "','" & tgl & "','" & hp & "','" & jk & "','" & asal & "')"

        Dim idInsert As Integer = crudid(sql)

        If Not foto = "" Then
            Dim file As Byte()
            Using Stream As New FileStream(foto, FileMode.Open, FileAccess.Read)
                Using reader As New BinaryReader(Stream)
                    file = reader.ReadBytes(CType(Stream.Length, Integer))
                End Using
            End Using

            Dim uploadImage As New SqlCommand("UPDATE [Siswa] SET foto_siswa = @Gambar WHERE id_siswa = " & idInsert, dbconn)
            uploadImage.Parameters.Add("@Gambar", SqlDbType.Binary, file.Length).Value = file
            uploadImage.ExecuteNonQuery()
        End If

    End Sub

    Public Sub updateSiswa(nama, tgl, hp, jk, asal, foto, id)
        sql = "update Siswa set nama_siswa='" & nama & "', tgl_lahir_siswa='" & tgl & "',telp_siswa='" & hp & "',jenis_kelamin='" & jk & "',asal_sekolah='" & asal & "' where id_siswa='" & id & "'"

        crud(sql)

        dbread.Close()

        Try
            If Not foto = "" Then
                Dim file As Byte()
                Using Stream As New FileStream(foto, FileMode.Open, FileAccess.Read)
                    Using reader As New BinaryReader(Stream)
                        file = reader.ReadBytes(CType(Stream.Length, Integer))
                    End Using
                End Using

                Dim uploadImage As New SqlCommand("UPDATE [Siswa] SET foto_siswa = @Gambar WHERE id_siswa = " & id, dbconn)
                uploadImage.Parameters.Add("@Gambar", SqlDbType.Binary, file.Length).Value = file
                uploadImage.ExecuteNonQuery()
            End If
        Catch ex As Exception

        End Try

        MsgBox("Update data success", MsgBoxStyle.Information)
    End Sub

    Public Sub deleteSiswa(id)
        sql = "delete from Siswa where id_siswa='" & id & "'"

        crud(sql)
        dbread.Close()
    End Sub

    Public Sub insertGuru(nama, foto)
        sql = "insert into Guru(nama_guru) output inserted.id_guru values('" & nama & "')"

        Dim insertId As Integer = crudid(sql)

        Try
            If Not foto = "" Then
                Dim file As Byte()
                Using Stream As New FileStream(foto, FileMode.Open, FileAccess.Read)
                    Using reader As New BinaryReader(Stream)
                        file = reader.ReadBytes(CType(Stream.Length, Integer))
                    End Using
                End Using

                dbcomm = New SqlCommand("update Guru set foto_guru=@Gambar where id_guru=" & insertId, dbconn)
                dbcomm.Parameters.Add("@Gambar", SqlDbType.Binary, file.Length).Value = file
                dbcomm.ExecuteNonQuery()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub updateGuru(nama, foto, id)
        sql = "update Guru set nama_guru='" & nama & "' where id_guru='" & id & "'"

        crud(sql)
        dbread.Close()

        Try
            If Not foto = "" Then
                Dim file As Byte()
                Using Stream As New FileStream(foto, FileMode.Open, FileAccess.Read)
                    Using reader As New BinaryReader(Stream)
                        file = reader.ReadBytes(CType(Stream.Length, Integer))
                    End Using
                End Using

                Dim uploadImage As New SqlCommand("UPDATE Guru SET foto_guru = @Gambar WHERE id_guru = " & id, dbconn)
                uploadImage.Parameters.Add("@Gambar", SqlDbType.Binary, file.Length).Value = file
                uploadImage.ExecuteNonQuery()
            End If
        Catch ex As Exception

        End Try

        MsgBox("Update data success", MsgBoxStyle.Information)
    End Sub

    Public Sub deleteGuru(id)
        sql = "delete guru where id_guru='" & id & "'"

        crud(sql)
        dbread.Close()
    End Sub

    Public Sub insertEkskul(id, nama, foto)
        sql = "insert into Ekskul(id_guru,nama_ekskul) output inserted.id_ekskul values('" & id & "','" & nama & "')"

        Dim insertId As Integer = crudid(sql)

        Try
            If Not foto = "" Then
                Dim file As Byte()
                Using Stream As New FileStream(foto, FileMode.Open, FileAccess.Read)
                    Using reader As New BinaryReader(Stream)
                        file = reader.ReadBytes(CType(Stream.Length, Integer))
                    End Using
                End Using

                Dim uploadImage As New SqlCommand("UPDATE Ekskul SET foto_ekskul = @Gambar WHERE id_ekskul = " & insertId, dbconn)
                uploadImage.Parameters.Add("@Gambar", SqlDbType.Binary, file.Length).Value = file
                uploadImage.ExecuteNonQuery()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        MsgBox("Insert data success", MsgBoxStyle.Information)
    End Sub

    Public Sub updateEkskul(guru, nama, foto, id)
        sql = "update Ekskul set id_guru='" & guru & "',nama_ekskul='" & nama & "',foto_ekskul='" & foto & "' where id_ekskul='" & id & "'"

        crud(sql)

        dbread.Close()

        Try
            If Not foto = "" Then
                Dim file As Byte()
                Using Stream As New FileStream(foto, FileMode.Open, FileAccess.Read)
                    Using reader As New BinaryReader(Stream)
                        file = reader.ReadBytes(CType(Stream.Length, Integer))
                    End Using
                End Using

                Dim uploadImage As New SqlCommand("UPDATE Ekskul SET foto_ekskul = @Gambar WHERE id_ekskul = " & id, dbconn)
                uploadImage.Parameters.Add("@Gambar", SqlDbType.Binary, file.Length).Value = file
                uploadImage.ExecuteNonQuery()
            End If
        Catch ex As Exception

        End Try

        MsgBox("Update data success", MsgBoxStyle.Information)
    End Sub

    Public Sub deleteEkskul(id)
        sql = "delete Ekskul where id_ekskul='" & id & "'"

        crud(sql)
        MsgBox("Delete data success", MsgBoxStyle.Information)
        dbread.Close()
    End Sub

    Public Sub siswaToEkskul(eks, sis)
        sql = "update Siswa set id_ekskul='" & eks & "' where id_siswa='" & sis & "'"

        crud(sql)
        MsgBox("Insert data success", MsgBoxStyle.Information)
        dbread.Close()
    End Sub

    Public Sub ekskulToSiswa(id)
        sql = "update Siswa set id_ekskul=NULL where id_siswa='" & id & "'"

        crud(sql)
        MsgBox("Delete data success", MsgBoxStyle.Information)
        dbread.Close()
    End Sub

    Public Sub insertJadwal(id, hari, jam)
        sql = "insert into JadwalEkskul(id_ekskul,hari_ekskul,jam_ekskul) values('" & id & "','" & hari & "','" & jam & "')"

        crud(sql)
        MsgBox("Insert data success", MsgBoxStyle.Information)
        dbread.Close()
    End Sub

    Public Sub updateJadwal(eks, hari, jam, id)
        sql = "update JadwalEkskul set id_ekskul='" & eks & "',hari_ekskul='" & hari & "',jam_ekskul='" & jam & "' where id_jadwal='" & id & "'"

        crud(sql)
        MsgBox("Update data success", MsgBoxStyle.Information)
        dbread.Close()
    End Sub

    Public Sub deleteJadwal(id)
        sql = "delete JadwalEkskul where id_jadwal='" & id & "'"

        crud(sql)
        MsgBox("Delete data success", MsgBoxStyle.Information)
        dbread.Close()
    End Sub

    Public Sub insertSoal(id As Integer, idg As Integer, mid As Byte())
        Dim UploadSQL As SqlCommand = New SqlCommand("INSERT into NilaiEkskul(id_siswa,id_guru,soal_mid) VALUES(@idSiswa,@idguru,@soalMid)", conn)
        UploadSQL.Parameters.Add("@idSiswa", SqlDbType.Int).Value = id
        UploadSQL.Parameters.Add("@idguru", SqlDbType.Int).Value = idg
        UploadSQL.Parameters.Add("@soalMid", SqlDbType.Binary, mid.Length).Value = mid
        crud(UploadSQL)
        'MsgBox("Insert data success", MsgBoxStyle.Information)
        dbread.Close()
    End Sub

    Public Sub insertSoalFinal(id As Integer, final As Byte())
        Dim UploadSQL As SqlCommand = New SqlCommand("update NilaiEkskul set soal_final=@soalFinal where id_siswa=@idSiswa", conn)
        UploadSQL.Parameters.Add("@idSiswa", SqlDbType.Int).Value = id
        UploadSQL.Parameters.Add("@soalFinal", SqlDbType.Binary, final.Length).Value = final
        crud(UploadSQL)
        'MsgBox("Insert data success", MsgBoxStyle.Information)
        dbread.Close()
    End Sub

    Public Sub editSoalMid(mid As Byte(), id As Integer)
        dbcomm = New SqlCommand("update NilaiEkskul set soal_mid=@soalmid where id_nilai=@idnilai", conn)
        dbcomm.Parameters.Add("@soalmid", SqlDbType.Binary, mid.Length).Value = mid
        dbcomm.Parameters.Add("@idnilai", SqlDbType.Int).Value = id
        crud(dbcomm)
        dbread.Close()
    End Sub

    Public Sub editSoalFinal(final As Byte(), id As Integer)
        dbcomm = New SqlCommand("update NilaiEkskul set soal_final=@soalfinal where id_nilai=@idnilai", conn)
        dbcomm.Parameters.Add("@soalfinal", SqlDbType.Binary, final.Length).Value = final
        dbcomm.Parameters.Add("@idnilai", SqlDbType.Int).Value = id
        crud(dbcomm)
        dbread.Close()
    End Sub

    Public Sub insertMid(id As Integer, mid As Byte())
        Dim UploadSQL As SqlCommand = New SqlCommand("INSERT into Jawaban(id_siswa,jawaban_mid) VALUES(@idSiswa,@jawabanmid)", conn)
        UploadSQL.Parameters.Add("@idSiswa", SqlDbType.Int).Value = id
        UploadSQL.Parameters.Add("@jawabanmid", SqlDbType.Binary, mid.Length).Value = mid
        crud(UploadSQL)
        MsgBox("Insert data success", MsgBoxStyle.Information)
        dbread.Close()
    End Sub

    Public Sub insertFinal(id As Integer, final As Byte())
        Dim UploadSQL As SqlCommand = New SqlCommand("update Jawaban set jawaban_final=@final where id_siswa=@siswa", conn)
        UploadSQL.Parameters.Add("@siswa", SqlDbType.Int).Value = id
        UploadSQL.Parameters.Add("@final", SqlDbType.Binary, final.Length).Value = final
        crud(UploadSQL)
        MsgBox("Insert data success", MsgBoxStyle.Information)
        dbread.Close()
    End Sub

    Public Sub nilaiMid(mid, id)
        sql = "update NilaiEkskul set nilai_mid='" & mid & "' where id_nilai='" & id & "'"

        crud(sql)
        dbread.Close()
    End Sub

    Public Sub nilaifinal(final, id)
        sql = "update NilaiEkskul set nilai_final='" & final & "' where id_nilai='" & id & "'"

        crud(sql)
        dbread.Close()
    End Sub

    Public Sub deleteSoal(id)
        sql = "delete NilaiEkskul where id_nilai='" & id & " '"

        crud(sql)
        MsgBox("Delete data success", MsgBoxStyle.Information)
        dbread.Close()
    End Sub

    Public Sub insertTest(varid, nama)
        sql = "insert into test(id,nama) values('" & varid & "','" & nama & "')"

        crud(sql)
        dbread.Close()
    End Sub

End Class
