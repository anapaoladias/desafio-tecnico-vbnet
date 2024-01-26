<%@ Page Title="Cadastro de Associados" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Detalhes.aspx.vb" Inherits="DesafioTecnicoVbNet.Web.Associados.Detalhes" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">

        <h2>Detalhes do Associado</h2>

        <div class="row">
            <div class="col-md-6">
                <label for="lblNome">Nome:</label>
                <asp:Label ID="lblNome" runat="server" CssClass="form-control" Text=""></asp:Label>
            </div>
            <div class="col-md-6">
                <label for="lblCPF">CPF:</label>
                <asp:Label ID="lblCPF" runat="server" CssClass="form-control" Text=""></asp:Label>
            </div>
            <div class="col-md-6">
                <label for="lblDataNascimento">Data de Nascimento:</label>
                <asp:Label ID="lblDataNascimento" runat="server" CssClass="form-control" Text=""></asp:Label>
            </div>
        </div>

        <hr />

        <h4>Empresas Vinculadas</h4>
        <asp:GridView ID="GridViewEmpresas" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridViewEmpresas_RowDataBound" RowStyle-BorderWidth="1">
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="ID" SortExpression="Id" />
                <asp:BoundField DataField="Nome" HeaderText="Nome" SortExpression="Nome" />
                <asp:BoundField DataField="CNPJ" HeaderText="CNPJ" SortExpression="CNPJ" />
            </Columns>
        </asp:GridView>

        <br />
        <asp:Button ID="btnVoltar" runat="server" Text="<< Voltar para Lista de Associados" CssClass="btn btn-dark mb-3" OnClick="btnVoltar_Click" />
    </div>
</asp:Content>
