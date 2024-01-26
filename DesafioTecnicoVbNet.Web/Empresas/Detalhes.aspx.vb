Imports DesafioTecnicoVbNet.Core

Namespace Empresas
    Public Class Detalhes
        Inherits System.Web.UI.Page

        Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            If Not IsPostBack Then

                If Request.QueryString("id") Is Nothing Then
                    Throw New InvalidOperationException("ID da Empresa não informado")
                End If

                Dim empresaId As Integer = Convert.ToInt32(Request.QueryString("id"))

                CarregarDetalhes(empresaId)
                CarregarAssociadosVinculados(empresaId)
            End If
        End Sub

        Private Sub CarregarDetalhes(empresaId As Integer)
            Dim empresaRepo As New EmpresaRepo()
            Dim detalhesEmpresa As DataTable = empresaRepo.ObterPorID(empresaId)

            If detalhesEmpresa.Rows.Count > 0 Then
                lblNome.Text = detalhesEmpresa.Rows(0)("Nome").ToString()
                lblCNPJ.Text = detalhesEmpresa.Rows(0)("CNPJ").ToString()
            End If
        End Sub

        Private Sub CarregarAssociadosVinculados(empresaId As Integer)
            Dim empresaRepo As New EmpresaRepo()
            Dim associadosVinculados As DataTable = empresaRepo.ObterAssociadosVinculados(empresaId)

            If associadosVinculados.Rows.Count > 0 Then
                GridViewAssociados.DataSource = associadosVinculados
                GridViewAssociados.DataBind()
            End If
        End Sub

        Protected Sub btnVoltar_Click(sender As Object, e As EventArgs)
            Response.Redirect("Lista.aspx")
        End Sub

    End Class
End Namespace