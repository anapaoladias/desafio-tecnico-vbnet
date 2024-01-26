Imports DesafioTecnicoVbNet.Core

Namespace Empresas
    Public Class Cadastro
        Inherits System.Web.UI.Page

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            If Not IsPostBack Then
                PopularListaAssociados()

                If Request.QueryString("id") IsNot Nothing Then
                    Dim empresaId As Integer
                    If Integer.TryParse(Request.QueryString("id"), empresaId) Then
                        CarregarDadosParaEdicao(empresaId)
                    End If
                End If
            End If
        End Sub

        Private Sub CarregarDadosParaEdicao(empresaId As Integer)
            btnVoltar.Visible = True

            Dim empresaRepo As New EmpresaRepo()
            Dim dataTable As DataTable = empresaRepo.ObterPorID(empresaId)

            If dataTable.Rows.Count > 0 Then
                txtNome.Text = dataTable.Rows(0)("Nome").ToString()
                txtCNPJ.Text = dataTable.Rows(0)("CNPJ").ToString()

                Dim associadosVinculados As DataTable = empresaRepo.ObterAssociadosVinculados(empresaId)

                For Each item As ListItem In lstAssociados.Items
                    Dim associadoId As Integer = Integer.Parse(item.Value)
                    Dim possuiVinculo As Boolean = associadosVinculados.AsEnumerable().Any(Function(row) row.Field(Of Integer)("Id") = associadoId)
                    item.Selected = possuiVinculo
                Next
            End If
        End Sub

        Private Sub PopularListaAssociados()
            Dim associadoRepo As New AssociadoRepo()
            Dim dataTable As DataTable = associadoRepo.ObterTodos()

            lstAssociados.DataSource = dataTable
            lstAssociados.DataTextField = "Nome"
            lstAssociados.DataValueField = "Id"
            lstAssociados.DataBind()
        End Sub

        Protected Sub btnSalvar_Click(sender As Object, e As EventArgs)
            Dim empresaRepo As New EmpresaRepo()

            Dim nome As String = txtNome.Text
            Dim CNPJ As String = txtCNPJ.Text

            Dim associadosSelecionados As New List(Of Integer)()
            For Each item As ListItem In lstAssociados.Items
                If item.Selected Then
                    associadosSelecionados.Add(Integer.Parse(item.Value))
                End If
            Next

            If Request.QueryString("id") IsNot Nothing Then
                Dim empresaId As Integer = Integer.Parse(Request.QueryString("id"))
                empresaRepo.Atualizar(empresaId, nome, CNPJ, associadosSelecionados)
            Else
                empresaRepo.Adicionar(nome, CNPJ, associadosSelecionados)
            End If

            Response.Redirect("Lista.aspx")
        End Sub

        Protected Sub btnVoltar_Click(sender As Object, e As EventArgs)
            Response.Redirect("Lista.aspx")
        End Sub

    End Class
End Namespace
