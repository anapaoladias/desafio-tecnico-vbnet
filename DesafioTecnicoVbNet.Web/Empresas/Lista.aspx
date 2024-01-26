<%@ Page Title="Empresas" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Lista.aspx.vb" Inherits="DesafioTecnicoVbNet.Web.Empresas.Lista" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <h2>Lista de Empresas</h2>

        <div class="row mb-3">
            <div class="col-md-4">
                <label for="txtFiltroNome">Nome:</label>
                <asp:TextBox ID="txtFiltroNome" runat="server" CssClass="form-control" MaxLength="200"></asp:TextBox>
            </div>
            <div class="col-md-4">
                <label for="txtFiltroCNPJ">CNPJ:</label>
                <asp:TextBox ID="txtFiltroCNPJ" runat="server" CssClass="form-control" MaxLength="14"></asp:TextBox>
            </div>

        </div>

        <div class="row mb-3">
            <div class="col-md-4 mt-3">
                <asp:Button ID="btnPesquisar" runat="server" Text="Pesquisar" CssClass="btn btn-primary" OnClick="btnPesquisar_Click" />
                <asp:Button ID="btnNovo" runat="server" Text="Novo" CssClass="btn btn-outline-dark" OnClick="btnNovo_Click" />
            </div>
        </div>

        <asp:GridView ID="GridViewEmpresas" runat="server"
            AutoGenerateColumns="False"
            OnRowCommand="GridViewEmpresas_RowCommand">
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="ID" SortExpression="Id" />
                <asp:BoundField DataField="Nome" HeaderText="Nome" SortExpression="Nome" />
                <asp:BoundField DataField="CNPJ" HeaderText="CNPJ" SortExpression="CNPJ" />
                <asp:TemplateField HeaderText="Ações">
                    <ItemTemplate>
                        <asp:Button ID="btnEditar" runat="server" Text="Editar" CssClass="btn btn-warning btn-sm mr-1" CommandName="Editar" CommandArgument='<%# Eval("Id") %>' />
                        <asp:Button ID="btnDetalhes" runat="server" Text="Detalhar" CssClass="btn btn-info btn-sm mr-1" CommandName="Detalhes" CommandArgument='<%# Eval("Id") %>' />
                        <asp:Button ID="btnExcluir" runat="server" Text="Excluir" CssClass="btn btn-danger btn-sm" OnClientClick="return confirm('Tem certeza que deseja excluir?');" CommandName="Excluir" CommandArgument='<%# Eval("Id") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
