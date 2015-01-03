Imports System.Data.SqlClient
Imports System.Data
Public Class SqlControl
    Private Shared instance As New SqlControl()
    Private apdater As SqlDataAdapter
    Private command As SqlCommand
    Private reader As SqlDataReader
    Public Overloads Shared Function SelectDataAdapter(sql As String, con As SqlConnection) As SqlDataAdapter
        Try
            con.Open()
            instance.apdater = New SqlDataAdapter(sql, con)
            con.Close()
            Return instance.apdater
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Overloads Shared Function SelectDataReader(sql As String, con As SqlConnection) As SqlDataReader
        Try
            If con.State = ConnectionState.Closed Then con.Open()
            instance.command = New SqlCommand(sql, con)
            instance.reader = instance.command.ExecuteReader()
            Return instance.reader
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Shared Function InsertData(sql As String, con As SqlConnection) As Integer
        Try
            If con.State = ConnectionState.Closed Then con.Open()
            instance.command = New SqlCommand(sql, con)
            
            Return instance.command.ExecuteNonQuery
        Catch ex As Exception
            Return -1
        End Try
    End Function
    Public Shared Function UpdateData(sql As String, con As SqlConnection) As Integer
        Try
            If con.State = ConnectionState.Closed Then con.Open()
            instance.command = New SqlCommand(sql, con)
            Return instance.command.ExecuteNonQuery
        Catch ex As Exception
            Return -1
        End Try
    End Function
    Public Shared Function DeleteData(table As String, condition As String, con As SqlConnection) As Integer
        Try
            If con.State = ConnectionState.Closed Then con.Open()
            Dim sql As String = "DELET FROM " & table & " WHERE " & condition
            instance.command = New SqlCommand(sql, con)

            Return instance.command.ExecuteNonQuery
        Catch ex As Exception
            Return -1
        End Try
    End Function
End Class
