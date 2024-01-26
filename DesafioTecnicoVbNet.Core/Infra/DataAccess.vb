Imports System.Configuration
Imports System.Data.SqlClient

Public Class DataAccess
    Implements IDisposable

    Private connectionString As String
    Private connection As SqlConnection
    Private transaction As SqlTransaction

    Public Sub New()
        Me.connectionString = ConfigurationManager.ConnectionStrings("Default").ConnectionString
    End Sub

    Public Sub OpenConnection()
        connection = New SqlConnection(connectionString)
        connection.Open()
    End Sub

    Public Sub BeginTransaction()
        If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
            transaction = connection.BeginTransaction()
        Else
            Throw New InvalidOperationException("A conexão com o banco de dados não está aberta.")
        End If
    End Sub

    Public Sub CommitTransaction()
        If transaction IsNot Nothing Then
            transaction.Commit()
        End If
    End Sub

    Public Sub RollbackTransaction()
        If transaction IsNot Nothing Then
            transaction.Rollback()
        End If
    End Sub
    'Public Function GetDataSet(sql As String) As DataSet
    '    Dim dataSet As New DataSet()
    '    Using adapter As New SqlDataAdapter(sql, connection)
    '        adapter.Fill(dataSet)
    '    End Using
    '    Return dataSet
    'End Function

    Public Function GetDataSet(sql As String, parametros As List(Of SqlParameter)) As DataSet
        Dim dataSet As New DataSet()
        Using adapter As New SqlDataAdapter(sql, connection)
            adapter.SelectCommand.Parameters.AddRange(parametros.ToArray())
            adapter.Fill(dataSet)
        End Using
        Return dataSet
    End Function

    Public Function GetDataTable(sql As String) As DataTable
        Dim dataTable As New DataTable()
        Using adapter As New SqlDataAdapter(sql, connection)
            adapter.Fill(dataTable)
        End Using
        Return dataTable
    End Function

    Public Function GetDataTable(sql As String, parametros As List(Of SqlParameter)) As DataTable
        Dim dataTable As New DataTable()
        Using adapter As New SqlDataAdapter(sql, connection)
            adapter.SelectCommand.Parameters.AddRange(parametros.ToArray())
            adapter.Fill(dataTable)
        End Using
        Return dataTable
    End Function


    'Public Function ExecuteNonQuery(sql As String) As Integer
    '    Using command As New SqlCommand(sql, connection, transaction)
    '        Return command.ExecuteNonQuery()
    '    End Using
    'End Function

    Public Function ExecuteNonQuery(sql As String, parametros As List(Of SqlParameter)) As Integer
        Using command As New SqlCommand(sql, connection, transaction)
            For Each parametro In parametros
                command.Parameters.Add(New SqlParameter(parametro.ParameterName, parametro.Value))
            Next
            Return command.ExecuteNonQuery()
        End Using
    End Function

    'Public Function ExecuteScalar(sql As String) As Object
    '    Using command As New SqlCommand(sql, connection, transaction)
    '        Return command.ExecuteScalar()
    '    End Using
    'End Function

    Public Function ExecuteScalar(sql As String, parametros As List(Of SqlParameter)) As Integer
        Using command As New SqlCommand(sql, connection, transaction)
            command.Parameters.AddRange(parametros.ToArray())
            Return command.ExecuteScalar()
        End Using
    End Function

    'Public Function ExecuteReader(sql As String) As SqlDataReader
    '    Dim command As New SqlCommand(sql, connection, transaction)
    '    Return command.ExecuteReader()
    'End Function

    Public Sub CloseConnection()
        If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
            connection.Close()
        End If
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

    Protected Overridable Sub Dispose(disposing As Boolean)
        If disposing Then
            If transaction IsNot Nothing Then
                transaction.Dispose()
            End If

            If connection IsNot Nothing AndAlso connection.State = ConnectionState.Open Then
                connection.Close()
                connection.Dispose()
            End If
        End If
    End Sub

End Class
