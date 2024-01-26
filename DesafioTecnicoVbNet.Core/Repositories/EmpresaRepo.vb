Imports System.Data.SqlClient

Public Class EmpresaRepo
    Public Function ObterTodos() As DataTable
        Using dataAccess As New DataAccess()
            dataAccess.OpenConnection()

            Dim sql As String = "SELECT * FROM Empresa"
            Return dataAccess.GetDataTable(sql)
        End Using
    End Function

End Class
