Imports DesafioTecnicoVbNet.Core

Namespace Associados

    Public Class Lista
        Inherits System.Web.UI.Page

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            If Not IsPostBack Then
                CarregarAssociados()
            End If
        End Sub

        Private Sub CarregarAssociados()
            Dim associadoRepo As New AssociadoRepo()
            Dim dataTable As DataTable = associadoRepo.ObterTodos()

            GridViewAssociados.DataSource = dataTable
            GridViewAssociados.DataBind()
        End Sub

        Protected Sub btnPesquisar_Click(sender As Object, e As EventArgs)
            Dim nome As String = txtFiltroNome.Text
            Dim cpf As String = txtFiltroCPF.Text
            Dim dataNascimentoTxt As String = txtFiltroDataNascimento.Text
            Dim dataNascimento As DateTime? = DateTime.MinValue
            If Not String.IsNullOrWhiteSpace(dataNascimentoTxt) Then
                If Not DateTime.TryParse(dataNascimentoTxt, dataNascimento) Then
                    dataNascimento = Nothing
                End If
            End If
            If dataNascimento.Value = DateTime.MinValue Then
                dataNascimento = Nothing
            End If

            Dim associadoRepo As New AssociadoRepo()
            Dim dataTable As DataTable = associadoRepo.ObterTodos(nome, cpf, dataNascimento)

            GridViewAssociados.DataSource = dataTable
            GridViewAssociados.DataBind()
        End Sub

        Protected Sub btnNovo_Click(sender As Object, e As EventArgs)
            Response.Redirect("Cadastro.aspx")
        End Sub

        Protected Sub GridViewAssociados_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GridViewAssociados.RowCommand
            Dim associadoID As Integer = Convert.ToInt32(e.CommandArgument)
            Select Case e.CommandName
                Case "Editar"
                    Response.Redirect($"Cadastro.aspx?id={associadoID}")

                Case "Detalhes"
                    Response.Redirect($"Detalhes.aspx?id={associadoID}")

                Case "Excluir"
                    Dim associadoRepo As New AssociadoRepo()
                    associadoRepo.Excluir(associadoID)

                    CarregarAssociados()
            End Select
        End Sub

    End Class

End Namespace