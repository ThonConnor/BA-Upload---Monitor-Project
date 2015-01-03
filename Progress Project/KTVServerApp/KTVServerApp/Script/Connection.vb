Imports System.Data.SqlClient
Public Class Connection
    Private Shared instance As New Connection()
    Private con As SqlConnection

    Private Sub New()
        con = New SqlConnection()
    End Sub
    Public Overloads Shared Function GetConnection() As SqlConnection
        OpenConnection()
        Return instance.con
    End Function
    Public Overloads Shared Function GetConnection(database As String, user As String, pass As String) As SqlConnection
        Try
            Dim provider As String = "Data Source=.;Initial Catalog=" & database & ";User ID=" & user & ";Password=" & pass
            If (instance.con.State = ConnectionState.Open) Then instance.con.Close()
            instance.con.ConnectionString = provider
            instance.con.Open()
            instance.con.Close()
            Return instance.con
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Shared Sub OpenConnection()
        If (instance.con IsNot Nothing And instance.con.State = ConnectionState.Closed) Then
            instance.con.Open()
        End If
    End Sub
    Public Shared Sub CloseConnection()
        If instance.con IsNot Nothing Then instance.con.Close()
    End Sub

End Class
