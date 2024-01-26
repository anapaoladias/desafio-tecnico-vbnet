Imports DesafioTecnicoVbNet.Core

Namespace Associados
    Public Class Cadastro
        Inherits System.Web.UI.Page

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            If Not IsPostBack Then
                PopularListaEmpresas()

                If Request.QueryString("id") IsNot Nothing Then
                    Dim associadoId As Integer
                    If Integer.TryParse(Request.QueryString("id"), associadoId) Then
                        CarregarDadosParaEdicao(associadoId)
                    End If
                End If
            End If
        End Sub

        Private Sub CarregarDadosParaEdicao(associadoId As Integer)
            btnVoltar.Visible = True

            Dim associadoRepo As New AssociadoRepo()
            Dim dataTable As DataTable = associadoRepo.ObterPorID(associadoId)

            If dataTable.Rows.Count > 0 Then
                txtNome.Text = dataTable.Rows(0)("Nome").ToString()
                txtCPF.Text = dataTable.Rows(0)("CPF").ToString()
                txtDataNascimento.Text = Convert.ToDateTime(dataTable.Rows(0)("DataNascimento")).ToString("dd/MM/yyyy")

                Dim empresasVinculadas As DataTable = associadoRepo.ObterEmpresasVinculadas(associadoId)

                For Each item As ListItem In lstEmpresas.Items
                    Dim empresaId As Integer = Integer.Parse(item.Value)
                    Dim possuiVinculo As Boolean = empresasVinculadas.AsEnumerable().Any(Function(row) row.Field(Of Integer)("Id") = empresaId)
                    item.Selected = possuiVinculo
                Next
            End If
        End Sub

        Private Sub PopularListaEmpresas()
            Dim empresaRepo As New EmpresaRepo()
            Dim dataTable As DataTable = empresaRepo.ObterTodos()

            lstEmpresas.DataSource = dataTable
            lstEmpresas.DataTextField = "Nome"
            lstEmpresas.DataValueField = "Id"
            lstEmpresas.DataBind()
        End Sub

        Protected Sub btnSalvar_Click(sender As Object, e As EventArgs)
            Dim associadoRepo As New AssociadoRepo()

            Dim nome As String = txtNome.Text
            Dim cpf As String = txtCPF.Text
            Dim dataNascimento As DateTime

            DateTime.TryParseExact(txtDataNascimento.Text, "dd/MM/yyyy", Nothing, Globalization.DateTimeStyles.None, dataNascimento)

            Dim empresasSelecionadas As New List(Of Integer)()
            For Each item As ListItem In lstEmpresas.Items
                If item.Selected Then
                    empresasSelecionadas.Add(Integer.Parse(item.Value))
                End If
            Next

            If Request.QueryString("id") IsNot Nothing Then
                Dim associadoId As Integer = Integer.Parse(Request.QueryString("id"))
                associadoRepo.Atualizar(associadoId, nome, cpf, dataNascimento, empresasSelecionadas)
            Else
                associadoRepo.Adicionar(nome, cpf, dataNascimento, empresasSelecionadas)
            End If

            Response.Redirect("Lista.aspx")
        End Sub

        Protected Sub btnVoltar_Click(sender As Object, e As EventArgs)
            Response.Redirect("Lista.aspx")
        End Sub

    End Class
End Namespace
