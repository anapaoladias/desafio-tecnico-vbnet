<%@ Page Title="Cadastro de Empresas" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Detalhes.aspx.vb" Inherits="DesafioTecnicoVbNet.Web.Empresas.Detalhes" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container mt-5">

        <h2>Detalhes do Empresa</h2>

        <div class="row">
            <div class="col-md-6">
                <label for="lblNome">Nome:</label>
                <asp:Label ID="lblNome" runat="server" CssClass="form-control" Text=""></asp:Label>
            </div>
            <div class="col-md-6">
                <label for="lblCNPJ">CNPJ:</label>
                <asp:Label ID="lblCNPJ" runat="server" CssClass="form-control" Text=""></asp:Label>
            </div>
        </div>

        <hr />

        <h4>Associados Vinculados</h4>
        <asp:GridView ID="GridViewAssociados" runat="server" AutoGenerateColumns="False" RowStyle-BorderWidth="1">
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="ID" SortExpression="Id" />
                <asp:BoundField DataField="Nome" HeaderText="Nome" SortExpression="Nome" />
                <asp:BoundField DataField="CPF" HeaderText="CPF" SortExpression="CPF" />
                <asp:BoundField DataField="DataNascimento" HeaderText="DataNascimento" SortExpression="DataNascimento" />
            </Columns>
        </asp:GridView>

        <br />
        <asp:Button ID="btnVoltar" runat="server" Text="<< Voltar para Lista de Empresas" CssClass="btn btn-dark mb-3" OnClick="btnVoltar_Click" />
    </div>
</asp:Content>
