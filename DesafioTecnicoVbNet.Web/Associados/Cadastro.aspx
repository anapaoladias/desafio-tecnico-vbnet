<%@ Page Title="Cadastro de Associados" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Cadastro.aspx.vb" Inherits="DesafioTecnicoVbNet.Web.Associados.Cadastro" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">
        <h2 class="mb-4">Incluir Associado</h2>
        <div class="form-group">
            <label for="txtNome">Nome:</label>
            <asp:TextBox ID="txtNome" runat="server" CssClass="form-control" MaxLength="200"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtNome" ErrorMessage="O campo Nome é obrigatório." Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
        </div>
        <div class="form-group">
            <label for="txtCPF">CPF:</label>
            <asp:TextBox ID="txtCPF" runat="server" CssClass="form-control" MaxLength="11"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCPF" ErrorMessage="O campo CPF é obrigatório." Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
        </div>
        <div class="form-group">
            <label for="txtDataNascimento">Data de Nascimento:</label>
            <div class="input-group">
                <asp:TextBox ID="txtDataNascimento" runat="server" CssClass="form-control" placeholder="DD/MM/AAAA"></asp:TextBox>
                <div class="input-group-append">
                    <span class="input-group-text" id="basic-addon2"><i class="far fa-calendar-alt"></i></span>
                </div>
            </div>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDataNascimento" ErrorMessage="O campo Data de Nascimento é obrigatório." Display="Dynamic" ForeColor="Red"></asp:RequiredFieldValidator>
            <asp:RegularExpressionValidator ID="revDataNascimento" runat="server"
                ControlToValidate="txtDataNascimento"
                ValidationExpression="^\d{2}/\d{2}/\d{4}$"
                ErrorMessage="Formato inválido. Use DD/MM/AAAA" CssClass="text-danger"></asp:RegularExpressionValidator>
        </div>
        <div class="form-group">
            <label for="lstEmpresas">Empresas:</label>
            <asp:ListBox ID="lstEmpresas" runat="server" CssClass="form-control" SelectionMode="Multiple" />
        </div>
        <asp:Label ID="lblErro" runat="server" CssClass="text-danger"></asp:Label>
        <div class="mt-3">
            <asp:Button ID="btnIncluir" runat="server" Text="Salvar" CssClass="btn btn-primary" OnClick="btnSalvar_Click" />
        </div>
        <br />
        <asp:Button ID="btnVoltar" Visible="false" runat="server" Text="<< Voltar para Lista de Associados" CssClass="btn btn-dark mb-3" OnClick="btnVoltar_Click" />
    </div>
</asp:Content>
