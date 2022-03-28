Imports System.Data.SqlClient

Public Class Rent
    Dim Con = New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DAMA\Documents\DvdRentalVbDb.mdf;Integrated Security=True;Connect Timeout=30")
    Private Sub fillCustomer()
        Con.Open()
        Dim sql = "select * from customerTbl"
        Dim cmd As New SqlCommand(sql, Con)
        Dim adapter As New SqlDataAdapter(cmd)
        Dim Tbl As New DataTable()
        adapter.Fill(Tbl)
        CustIdCb.DataSource = Tbl
        CustIdCb.DisplayMember = "CustId"
        CustIdCb.ValueMember = "CustId"
        Con.Close()
    End Sub
    Private Sub fillNum()
        Dim Status = "Yes"
        Con.Open()
        Dim sql = "select * from DVDTbl where Available='" & Status & "'"
        Dim cmd As New SqlCommand(sql, Con)
        Dim adapter As New SqlDataAdapter(cmd)
        Dim Tbl As New DataTable()
        adapter.Fill(Tbl)
        NumCb.DataSource = Tbl
        NumCb.DisplayMember = "No"
        NumCb.ValueMember = "No"
        Con.Close()
    End Sub

    Private Sub UpdateDvd()
        Dim Status = "No"
        Try
            Con.Open()
            Dim query = "update DVDTbl set Available='" & Status & "' where No='" & NumCb.SelectedValue.ToString() & "'"
            Dim cmd As SqlCommand
            cmd = New SqlCommand(query, Con)
            cmd.ExecuteNonQuery()
            'MsgBox("DVD Successfully Updated")
            Con.Close()
            'Clear()
            populate()
        Catch ex As Exception
            'MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub GetCustName()
        Con.Open()
        Dim sql = "select * from customerTbl where CustId=" & CustIdCb.SelectedValue.ToString() & ""
        Dim cmd As New SqlCommand(sql, Con)
        Dim dt As New DataTable
        Dim reader As SqlDataReader
        reader = cmd.ExecuteReader
        While reader.Read
            CustNameTb.Text = reader(1).ToString()
        End While
        Con.Close()
    End Sub

    Dim Cost = 0
    Private Sub GetDVDRate()
        Con.Open()
        Dim sql = "select * from DVDTbl where No=" & NumCb.SelectedValue.ToString() & ""
        Dim cmd As New SqlCommand(sql, Con)
        Dim dt As New DataTable
        Dim reader As SqlDataReader
        reader = cmd.ExecuteReader
        While reader.Read
            Cost = Convert.ToInt32(reader(4).ToString())
        End While
        Con.Close()
    End Sub
    Private Sub Rent_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        fillCustomer()
        fillNum()
        populate()
    End Sub

    Private Sub CustIdCb_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles CustIdCb.SelectionChangeCommitted
        GetCustName()
    End Sub
    Private Sub Clear()
        CustNameTb.Text = ""
        FeesTb.Text = ""
    End Sub
    Private Sub populate()
        Con.Open()
        Dim sql = "select * from RentTbl"
        Dim cmd = New SqlCommand(sql, Con)
        Dim adapter As SqlDataAdapter
        adapter = New SqlDataAdapter(cmd)
        Dim builder As SqlCommandBuilder
        builder = New SqlCommandBuilder(adapter)
        Dim ds As DataSet
        ds = New DataSet
        adapter.Fill(ds)
        RentDgv.DataSource = ds.Tables(0)
        Con.Close()

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If CustNameTb.Text = "" Or NumCb.SelectedIndex = -1 Or FeesTb.Text = "" Then
            MsgBox("Missing Data")
        Else
            Try
                Con.Open()
                Dim query = "insert into RentTbl values('" & NumCb.SelectedValue.ToString() & "', '" & CustIdCb.SelectedValue.ToString() & "', '" & CustNameTb.Text & "','" & RentDate.Value.Date & "', '" & ReturnDate.Value.Date & "', " & FeesTb.Text & ") "
                Dim cmd As SqlCommand
                cmd = New SqlCommand(query, Con)
                cmd.ExecuteNonQuery()
                MsgBox("DVD Successfully Rented.")
                Con.Close()
                UpdateDvd()
                Clear()
                populate()
                fillNum()
            Catch ex As Exception

            End Try
        End If
    End Sub

    Private Sub NumCb_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles NumCb.SelectionChangeCommitted
        GetDVDRate()
    End Sub

    Private Sub Label10_Click(sender As Object, e As EventArgs) Handles Label10.Click
        Dim Obj = New dvd
        Obj.Show()
        Me.Hide()
    End Sub

    Private Sub Label11_Click(sender As Object, e As EventArgs) Handles Label11.Click
        Dim Obj = New Customers
        Obj.Show()
        Me.Hide()
    End Sub

    Private Sub Label13_Click(sender As Object, e As EventArgs) Handles Label13.Click
        Dim Obj = New Returns
        Obj.Show()
        Me.Hide()
    End Sub

    Private Sub Label12_Click(sender As Object, e As EventArgs) Handles Label12.Click
        Dim Obj = New Rent
        Obj.Show()
        Me.Hide()
    End Sub
End Class