Imports System.Data.SqlClient
Public Class Returns
    Dim Con = New SqlConnection("Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\DAMA\Documents\DvdRentalVbDb.mdf;Integrated Security=True;Connect Timeout=30")
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
        RentalsDgv.DataSource = ds.Tables(0)
        Con.Close()

    End Sub
    Private Sub Returns_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        populate()
    End Sub
    Dim Delay = 0
    Private Sub CalculateDelay()
        Dim diff As System.TimeSpan = DateTime.Today.Date.Subtract(Convert.ToDateTime(ReturnDate.Value))
        Dim Days = diff.TotalDays
        If Days < 0 Then
            Days = 0
        Else
            DelayTb.Text = Days
        End If
        Dim Fine = Days * 5
    End Sub

    Private Sub RentalsDgv_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles RentalsDgv.CellMouseClick
        Dim row As DataGridViewRow = RentalsDgv.Rows(e.RowIndex)
        NumbTb.Text = row.Cells(1).Value.ToString
        CustName.Text = row.Cells(3).Value.ToString
        ReturnDate.Value = row.Cells(5).Value.ToString
    End Sub
    Private Sub ReturnDate_ValueChanged_1(sender As Object, e As EventArgs) Handles ReturnDate.ValueChanged
        CalculateDelay()
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

    Private Sub Label12_Click(sender As Object, e As EventArgs) Handles Label12.Click
        Dim Obj = New Rent
        Obj.Show()
        Me.Hide()
    End Sub
End Class