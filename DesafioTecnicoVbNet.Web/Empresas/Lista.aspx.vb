Imports DesafioTecnicoVbNet.Core

Namespace Empresas
    Public Class Lista
        Inherits System.Web.UI.Page

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            If Not IsPostBack Then
                CarregarEmpresas()
            End If
        End Sub

        Private Sub CarregarEmpresas()
            Dim empresaRepo As New EmpresaRepo()
            Dim dataTable As DataTable = empresaRepo.ObterTodos()

            GridViewEmpresas.DataSource = dataTable
            GridViewEmpresas.DataBind()
        End Sub

        Protected Sub btnPesquisar_Click(sender As Object, e As EventArgs)
            Dim nome As String = txtFiltroNome.Text
            Dim cnpj As String = txtFiltroCNPJ.Text

            Dim empresaRepo As New EmpresaRepo()
            Dim dataTable As DataTable = empresaRepo.ObterTodos(nome, cnpj)

            GridViewEmpresas.DataSource = dataTable
            GridViewEmpresas.DataBind()
        End Sub

        Protected Sub btnNovo_Click(sender As Object, e As EventArgs)
            Response.Redirect("Cadastro.aspx")
        End Sub

        Protected Sub GridViewEmpresas_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridViewEmpresas.RowCommand
            Dim id As Integer = Convert.ToInt32(e.CommandArgument)
            Select Case e.CommandName
                Case "Editar"
                    Response.Redirect($"Cadastro.aspx?id={id}")

                Case "Detalhes"
                    Response.Redirect($"Detalhes.aspx?id={id}")

                Case "Excluir"
                    Dim empresaRepo As New EmpresaRepo()
                    empresaRepo.Excluir(id)

                    CarregarEmpresas()
            End Select
        End Sub

    End Class
End Namespace