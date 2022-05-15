Public Class Form1
    Dim con As New OleDb.OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\joken\Documents\TechJobs.accdb")
    Dim sql As String
    Dim cmd As New OleDb.OleDbCommand
    Dim i As Integer

    Dim da As New OleDb.OleDbDataAdapter
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            'open the connection
            con.Open()
            'check using if statement if the connection is open
            If con.State = ConnectionState.Open Then
                'Display a message box if successfully connected or Not
                MsgBox("Connected")
            Else
                MsgBox("Not Connected!")

            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            'close the connection
            con.Close()

        End Try

    End Sub

    Sub loadrecord()
        Try
            Dim sql As String
            Dim cmd As New OleDb.OleDbCommand
            Dim dt As New DataTable
            Dim da As New OleDb.OleDbDataAdapter
            con.Open()
            sql = "Select * from TechJobs"
            cmd.Connection = con
            cmd.CommandText = sql
            da.SelectCommand = cmd

            da.Fill(dt)

            DataGridView1.DataSource = dt
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            con.Close()

        End Try


    End Sub
    Private Sub btnloadrecord_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnloadrecord.Click
        loadrecord()

    End Sub

    Private Sub btnsave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnsave.Click

        Try
            con.Open()
            sql = "INSERT INTO TechJobs (Placement,JobName,MedianSalary) values ('" & jobplacement.Text & "', '" & jobtitle.Text & "'," & Val(mediansalary.Text) & ");"
            cmd.Connection = con
            cmd.CommandText = sql
           
            i = cmd.ExecuteNonQuery
            If i > 0 Then
                MsgBox("New record has been inserted successfully!")


            Else
                MsgBox("No record has been inserted successfully!")
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            con.Close()
            loadrecord()
        End Try

    End Sub

    Private Sub DataGridView1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Me.Text = DataGridView1.CurrentRow.Cells(0).Value
        jobplacement.Text = DataGridView1.CurrentRow.Cells(1).Value
        jobtitle.Text = DataGridView1.CurrentRow.Cells(2).Value
        mediansalary.Text = DataGridView1.CurrentRow.Cells(3).Value
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub btnupdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnupdate.Click
       

        Try
            con.Open()
            sql = "UPDATE TechJobs SET JobName='" & jobtitle.Text & "', Placement='" & jobplacement.Text & "', MedianSalary=" & Val(mediansalary.Text) & " WHERE Placement=" & Val(Me.Text) & ""
            cmd.Connection = con
            cmd.CommandText = sql

            i = cmd.ExecuteNonQuery
            If i > 0 Then
                MsgBox("Record has been UPDATED successfully!")

            Else
                MsgBox("No record has been UPDATED!")
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            con.Close()
            loadrecord()
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

        Try
            con.Open()
            sql = "Delete * from TechJobs WHERE Placement=" & Val(Me.Text) & ""
            cmd.Connection = con
            cmd.CommandText = sql

            i = cmd.ExecuteNonQuery
            If i > 0 Then
                MsgBox("Record has been deleted successfully!")

            Else
                MsgBox("No record has been deleted!")
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            con.Close()
            loadrecord()
        End Try
    End Sub
End Class
