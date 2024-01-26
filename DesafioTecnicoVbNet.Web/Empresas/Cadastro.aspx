<%@ Page Title="Cadastro de Empresas" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.vb" Inherits="DesafioTecnicoVbNet.Web.Empresas.Cadastro" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <h2 class="mb-4">Incluir Empresa</h2>
        <div class="form-group">
            <label for="txtNome">Nome:</label>
            <asp:TextBox ID="txtNome" runat="server" CssClass="form-control" MaxLength="200"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtNome" ErrorMessage="O campo Nome é obrigatório." Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
        </div>
        <div class="form-group">
            <label for="txtCNPJ">CNPJ:</label>
            <asp:TextBox ID="txtCNPJ" runat="server" CssClass="form-control" MaxLength="14"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCNPJ" ErrorMessage="O campo CNPJ é obrigatório." Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
        </div>
        <div class="form-group">
            <label for="lstAssociados">Associados:</label>
            <asp:ListBox ID="lstAssociados" runat="server" CssClass="form-control" SelectionMode="Multiple" />
        </div>
        <asp:Label ID="lblErro" runat="server" CssClass="text-danger"></asp:Label>
        <div class="mt-3">
            <asp:Button ID="btnIncluir" runat="server" Text="Salvar" CssClass="btn btn-primary" OnClick="btnSalvar_Click" />
        </div>
        <br />
        <asp:Button ID="btnVoltar" Visible="false" runat="server" Text="<< Voltar para Lista de Empresas" CssClass="btn btn-dark mb-3" OnClick="btnVoltar_Click" />
    </div>
</asp:Content>
