Imports System.Data.SqlClient
Public Class dvd
    Dim Con = New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DAMA\Documents\DvdRentalVbDb.mdf;Integrated Security=True;Connect Timeout=30")
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Try
            Con.Open()
            Dim query = "insert into DVDTbl values('" & NumTbl.Text & "', '" & CategorizedCb.SelectedItem.ToString() & "', '" & GenreTb.Text & "', '" & PriceTb.Text & "', '" & LanguageTb.Text & "', '" & AvailableCb.SelectedItem.ToString() & "')"
            Dim cmd As SqlCommand
            cmd = New SqlCommand(query, Con)
            cmd.ExecuteNonQuery()
            MsgBox("DVD Successfully Saved")
            Con.Close()
            Clear()
            populate()
        Catch ex As Exception

        End Try
    End Sub
    Private Sub Clear()
        NumTbl.Text = ""
        CategorizedCb.SelectedIndex = -1
        GenreTb.Text = ""
        PriceTb.Text = ""
        LanguageTb.Text = ""
        AvailableCb.SelectedIndex = -1
        Key = 0
    End Sub
    Private Sub populate()
        Con.Open()
        Dim sql = "select * from DVDTbl"
        Dim cmd = New SqlCommand(sql, Con)
        Dim adapter As SqlDataAdapter
        adapter = New SqlDataAdapter(cmd)
        Dim builder As SqlCommandBuilder
        builder = New SqlCommandBuilder(adapter)
        Dim ds As DataSet
        ds = New DataSet
        adapter.Fill(ds)
        CardDgv.DataSource = ds.Tables(0)
        Con.Close()

    End Sub
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Clear()
    End Sub

    Private Sub GunaDataGridView1_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles CardDgv.CellMouseClick
        Dim row As DataGridViewRow = CardDgv.Rows(e.RowIndex)
        NumTbl.Text = row.Cells(1).Value.ToString
        CategorizedCb.SelectedItem = row.Cells(2).Value.ToString
        GenreTb.Text = row.Cells(3).Value.ToString
        PriceTb.Text = row.Cells(4).Value.ToString
        LanguageTb.Text = row.Cells(5).Value.ToString
        AvailableCb.SelectedItem = row.Cells(6).Value.ToString
        If NumTbl.Text = "" Then
            Key = 0
        Else
            Key = Convert.ToInt32(row.Cells(0).Value.ToString)
        End If
    End Sub

    Private Sub dvd_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        populate()
    End Sub
    Dim Key = 0
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If Key = 0 Then
            MsgBox("Select The DVD")
        Else
            Try
                Con.Open()
                Dim query = "delete from DVDTbl where CId=" & Key & ""
                Dim cmd As SqlCommand
                cmd = New SqlCommand(query, Con)
                cmd.ExecuteNonQuery()
                MsgBox("DVD Successfully Deleted")
                Con.Close()
                Clear()
                populate()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
    End Sub

    Private Sub CardDgv_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles CardDgv.CellContentClick

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If NumTbl.Text = "" Or CategorizedCb.SelectedIndex = -1 Or GenreTb.Text = "" Or PriceTb.Text = "" Or LanguageTb.Text = "" Or AvailableCb.SelectedIndex = -1 Then
            MsgBox("Missing Information")
        Else
            Try
                Con.Open()
                Dim query = "update DVDTbl set No= '" & NumTbl.Text & "', Categorized='" & CategorizedCb.SelectedItem.ToString() & "', Language= '" & LanguageTb.Text & "', Price= " & PriceTb.Text & ", Genre= '" & GenreTb.Text & "', Available='" & AvailableCb.SelectedItem.ToString() & "' where CId=" & Key & ""
                Dim cmd As SqlCommand
                cmd = New SqlCommand(query, Con)
                cmd.ExecuteNonQuery()
                MsgBox("DVD Successfully Updated")
                Con.Close()
                Clear()
                populate()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End If
    End Sub

    Private Sub Label11_Click(sender As Object, e As EventArgs) Handles Label11.Click
        Dim Obj = New Customers
        Obj.Show()
        Me.Hide()
    End Sub

    Private Sub Label12_Click(sender As Object, e As EventArgs) Handles Label12.Click
        Dim Obj = New Rent
        Obj.Show()
        Me.Hide()
    End Sub

    Private Sub Label13_Click(sender As Object, e As EventArgs) Handles Label13.Click
        Dim Obj = New Returns
        Obj.Show()
        Me.Hide()
    End Sub
End Class