Imports DesafioTecnicoVbNet.Core

Namespace Associados
    Public Class Detalhes
        Inherits System.Web.UI.Page

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            If Not IsPostBack Then

                If Request.QueryString("id") Is Nothing Then
                    Throw New InvalidOperationException("ID do Associado não informado")
                End If

                Dim associadoID As Integer = Convert.ToInt32(Request.QueryString("id"))

                CarregarDetalhesAssociado(associadoID)
                CarregarEmpresasVinculadas(associadoID)
            End If
        End Sub

        Private Sub CarregarDetalhesAssociado(associadoID As Integer)
            Dim associadoRepo As New AssociadoRepo()
            Dim detalhesAssociado As DataTable = associadoRepo.ObterPorID(associadoID)

            If detalhesAssociado.Rows.Count > 0 Then
                lblNome.Text = detalhesAssociado.Rows(0)("Nome").ToString()
                lblCPF.Text = detalhesAssociado.Rows(0)("CPF").ToString()
                lblDataNascimento.Text = Convert.ToDateTime(detalhesAssociado.Rows(0)("DataNascimento")).ToString("dd/MM/yyyy")
            End If
        End Sub

        Private Sub CarregarEmpresasVinculadas(associadoID As Integer)
            Dim associadoRepo As New AssociadoRepo()
            Dim empresasVinculadas As DataTable = associadoRepo.ObterEmpresasVinculadas(associadoID)

            If empresasVinculadas.Rows.Count > 0 Then
                GridViewEmpresas.DataSource = empresasVinculadas
                GridViewEmpresas.DataBind()
            End If
        End Sub

        Protected Sub btnVoltar_Click(sender As Object, e As EventArgs)
            Response.Redirect("Lista.aspx")
        End Sub

        Protected Sub GridViewEmpresas_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GridViewEmpresas.RowDataBound
            '
        End Sub

    End Class
End Namespace