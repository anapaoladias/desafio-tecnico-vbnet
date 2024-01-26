Imports System.Data.SqlClient

Public Class EmpresaRepo
    Public Function ObterTodos(Optional nome As String = Nothing, Optional cnpj As String = Nothing) As DataTable
        Using dataAccess As New DataAccess()
            dataAccess.OpenConnection()

            Dim sql As String = "SELECT * FROM Empresa WHERE 1 = 1"

            If Not String.IsNullOrEmpty(nome) Then
                nome = $"%{nome}%"
                sql &= " AND Nome like @Nome"
            End If

            If Not String.IsNullOrEmpty(cnpj) Then
                sql &= " AND CNPJ = @CNPJ"
            End If

            Dim parametros As New List(Of SqlParameter)()

            If Not String.IsNullOrEmpty(nome) Then
                parametros.Add(New SqlParameter("@Nome", nome))
            End If

            If Not String.IsNullOrEmpty(cnpj) Then
                parametros.Add(New SqlParameter("@CNPJ", cnpj))
            End If

            Return dataAccess.GetDataTable(sql, parametros)
        End Using
    End Function

    Public Function ObterPorID(id As Integer) As DataTable
        Using dataAccess As New DataAccess()
            dataAccess.OpenConnection()

            Dim sql As String = "SELECT * FROM Empresa WHERE Id = @EmpresaID"
            Dim parametro As New SqlParameter("@EmpresaID", id)
            Return dataAccess.GetDataTable(sql, New List(Of SqlParameter)() From {parametro})
        End Using
    End Function

    Public Sub Adicionar(nome As String, CNPJ As String, associados As List(Of Integer))
        Using dataAccess As New DataAccess()
            Try
                dataAccess.OpenConnection()
                dataAccess.BeginTransaction()

                Dim sqlEmpresa As String = "INSERT INTO Empresa (Nome, CNPJ) VALUES (@Nome, @CNPJ); SELECT SCOPE_IDENTITY()"

                Dim parametrosEmpresa As New List(Of SqlParameter)()
                parametrosEmpresa.Add(New SqlParameter("@Nome", nome))
                parametrosEmpresa.Add(New SqlParameter("@CNPJ", CNPJ))

                Dim empresaID As Integer = CInt(dataAccess.ExecuteScalar(sqlEmpresa, parametrosEmpresa))

                If associados IsNot Nothing AndAlso associados.Count > 0 Then
                    Dim sqlVincularAssociado As String = "INSERT INTO Empresa_Associado (empresaId, associadoId) VALUES (@EmpresaID, @AssociadoID)"

                    Dim parametroEmpresaID As New SqlParameter("@EmpresaID", empresaID)

                    For Each associadoID As Integer In associados
                        Dim parametroAssociadoID As New SqlParameter("@AssociadoID", associadoID)
                        dataAccess.ExecuteNonQuery(sqlVincularAssociado, New List(Of SqlParameter)() From {parametroEmpresaID, parametroAssociadoID})
                    Next
                End If

                dataAccess.CommitTransaction()
            Catch ex As Exception
                dataAccess.RollbackTransaction()
                Throw
            Finally
                dataAccess.CloseConnection()
            End Try
        End Using
    End Sub

    Public Sub Atualizar(id As Integer, nome As String, CNPJ As String, associados As List(Of Integer))
        Using dataAccess As New DataAccess()
            Try
                dataAccess.OpenConnection()
                dataAccess.BeginTransaction()

                Dim sql As String = "UPDATE Empresa SET Nome = @Nome, CNPJ = @CNPJ WHERE Id = @EmpresaID"

                Dim parametros As New List(Of SqlParameter)()
                parametros.Add(New SqlParameter("@Nome", nome))
                parametros.Add(New SqlParameter("@CNPJ", CNPJ))
                parametros.Add(New SqlParameter("@EmpresaID", id))

                dataAccess.ExecuteNonQuery(sql, parametros)

                If associados IsNot Nothing AndAlso associados.Count > 0 Then
                    Dim sqlDesvincularTodos As String = "DELETE From Empresa_Associado Where EmpresaId = @EmpresaID"
                    Dim parametroEmpresaID As New SqlParameter("@EmpresaID", id)
                    dataAccess.ExecuteNonQuery(sqlDesvincularTodos, New List(Of SqlParameter)() From {parametroEmpresaID})

                    Dim sqlVincular As String = "INSERT INTO Empresa_Associado (empresaId, associadoId) VALUES (@EmpresaID, @AssociadoID)"

                    For Each associadoID As Integer In associados
                        Dim parametroAssociadoID As New SqlParameter("@AssociadoID", associadoID)
                        dataAccess.ExecuteNonQuery(sqlVincular, New List(Of SqlParameter)() From {parametroEmpresaID, parametroAssociadoID})
                    Next
                End If

                dataAccess.CommitTransaction()
            Catch ex As Exception
                dataAccess.RollbackTransaction()
                Throw
            Finally
                dataAccess.CloseConnection()
            End Try
        End Using
    End Sub

    Public Sub Excluir(id As Integer)
        Using dataAccess As New DataAccess()
            Try
                dataAccess.OpenConnection()
                dataAccess.BeginTransaction()

                Dim paramId As New SqlParameter("@Id", id)

                Dim sqlExcluir As String = "
                    DELETE From Empresa_Associado Where EmpresaId = @Id;
                    DELETE From Empresa Where Id = @Id"

                dataAccess.ExecuteNonQuery(sqlExcluir, New List(Of SqlParameter)() From {paramId})

                dataAccess.CommitTransaction()
            Catch ex As Exception
                dataAccess.RollbackTransaction()
                Throw
            Finally
                dataAccess.CloseConnection()
            End Try
        End Using
    End Sub

    Public Function ObterAssociadosVinculados(EmpresaID As Integer) As DataTable
        Using dataAccess As New DataAccess()
            dataAccess.OpenConnection()

            Dim sql As String = "
                select a.Id, a.Nome, a.CPF, a.DataNascimento
                from Associado a
                join Empresa_Empresa ea on a.Id = ea.AssociadoId
                where ea.EmpresaId = @EmpresaId
                "
            Dim parametro As New SqlParameter("@EmpresaID", EmpresaID)
            Return dataAccess.GetDataTable(sql, New List(Of SqlParameter)() From {parametro})
        End Using
    End Function
End Class
