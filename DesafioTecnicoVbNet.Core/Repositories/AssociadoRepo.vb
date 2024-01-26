Imports System.Data.SqlClient

Public Class AssociadoRepo
    Public Function ObterTodos(Optional nome As String = Nothing, Optional cpf As String = Nothing, Optional dataNascimento As DateTime? = Nothing) As DataTable
        Using dataAccess As New DataAccess()
            dataAccess.OpenConnection()

            Dim sql As String = "SELECT * FROM Associado WHERE 1 = 1"

            If Not String.IsNullOrEmpty(nome) Then
                nome = $"%{nome}%"
                sql &= " AND Nome like @Nome"
            End If

            If Not String.IsNullOrEmpty(cpf) Then
                sql &= " AND CPF = @CPF"
            End If

            If dataNascimento.HasValue Then
                sql &= " AND DataNascimento = @DataNascimento"
            End If

            Dim parametros As New List(Of SqlParameter)()

            If Not String.IsNullOrEmpty(nome) Then
                parametros.Add(New SqlParameter("@Nome", nome))
            End If

            If Not String.IsNullOrEmpty(cpf) Then
                parametros.Add(New SqlParameter("@CPF", cpf))
            End If

            If dataNascimento.HasValue Then
                parametros.Add(New SqlParameter("@DataNascimento", dataNascimento.Value))
            End If

            Return dataAccess.GetDataTable(sql, parametros)
        End Using
    End Function

    Public Function ObterPorID(id As Integer) As DataTable
        Using dataAccess As New DataAccess()
            dataAccess.OpenConnection()

            Dim sql As String = "SELECT * FROM Associado WHERE Id = @AssociadoID"
            Dim parametro As New SqlParameter("@AssociadoID", id)
            Return dataAccess.GetDataTable(sql, New List(Of SqlParameter)() From {parametro})
        End Using
    End Function

    Public Sub Adicionar(nome As String, CPF As String, dataNascimento As DateTime, empresas As List(Of Integer))
        Using dataAccess As New DataAccess()
            Try
                dataAccess.OpenConnection()
                dataAccess.BeginTransaction()

                Dim sqlAssociado As String = "INSERT INTO Associado (Nome, CPF, DataNascimento) VALUES (@Nome, @CPF, @DataNascimento); SELECT SCOPE_IDENTITY()"

                Dim parametrosAssociado As New List(Of SqlParameter)()
                parametrosAssociado.Add(New SqlParameter("@Nome", nome))
                parametrosAssociado.Add(New SqlParameter("@CPF", CPF))
                parametrosAssociado.Add(New SqlParameter("@DataNascimento", dataNascimento))

                Dim associadoID As Integer = CInt(dataAccess.ExecuteScalar(sqlAssociado, parametrosAssociado))

                If empresas IsNot Nothing AndAlso empresas.Count > 0 Then
                    Dim sqlAssociarEmpresa As String = "INSERT INTO Empresa_Associado (empresaId, associadoId) VALUES (@EmpresaID, @AssociadoID)"

                    Dim parametroAssociadoID As New SqlParameter("@AssociadoID", associadoID)

                    For Each empresaID As Integer In empresas
                        Dim parametroEmpresaID As New SqlParameter("@EmpresaID", empresaID)
                        dataAccess.ExecuteNonQuery(sqlAssociarEmpresa, New List(Of SqlParameter)() From {parametroEmpresaID, parametroAssociadoID})
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

    Public Sub Atualizar(id As Integer, nome As String, CPF As String, dataNascimento As DateTime, empresas As List(Of Integer))
        Using dataAccess As New DataAccess()
            Try
                dataAccess.OpenConnection()
                dataAccess.BeginTransaction()

                Dim sql As String = "UPDATE Associado SET Nome = @Nome, CPF = @CPF, DataNascimento = @DataNascimento WHERE Id = @AssociadoID"

                Dim parametros As New List(Of SqlParameter)()
                parametros.Add(New SqlParameter("@Nome", nome))
                parametros.Add(New SqlParameter("@CPF", CPF))
                parametros.Add(New SqlParameter("@DataNascimento", dataNascimento))
                parametros.Add(New SqlParameter("@AssociadoID", id))

                dataAccess.ExecuteNonQuery(sql, parametros)

                If empresas IsNot Nothing AndAlso empresas.Count > 0 Then
                    Dim sqlDesassociarTodasEmpresas As String = "DELETE From Empresa_Associado Where associadoId = @AssociadoId"
                    Dim parametroAssociadoID As New SqlParameter("@AssociadoID", id)
                    dataAccess.ExecuteNonQuery(sqlDesassociarTodasEmpresas, New List(Of SqlParameter)() From {parametroAssociadoID})

                    Dim sqlAssociarEmpresa As String = "INSERT INTO Empresa_Associado (empresaId, associadoId) VALUES (@EmpresaID, @AssociadoID)"

                    For Each empresaID As Integer In empresas
                        Dim parametroEmpresaID As New SqlParameter("@EmpresaID", empresaID)
                        dataAccess.ExecuteNonQuery(sqlAssociarEmpresa, New List(Of SqlParameter)() From {parametroEmpresaID, parametroAssociadoID})
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
                    DELETE From Empresa_Associado Where associadoId = @Id;
                    DELETE From Associado Where Id = @Id"

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

    Public Function ObterEmpresasVinculadas(associadoID As Integer) As DataTable
        Using dataAccess As New DataAccess()
            dataAccess.OpenConnection()

            Dim sql As String = "
                select e.Id, e.Nome, e.CNPJ 
                from Empresa e
                join Empresa_Associado ea on e.Id = ea.EmpresaId
                where ea.AssociadoId = @associadoId
                "
            Dim parametro As New SqlParameter("@AssociadoID", associadoID)
            Return dataAccess.GetDataTable(sql, New List(Of SqlParameter)() From {parametro})
        End Using
    End Function
End Class
